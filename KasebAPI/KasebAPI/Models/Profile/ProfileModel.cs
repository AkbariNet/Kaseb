using KasebAPI.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KasebAPI.Models.Profile
{
    public class ProfileModel : IdentityUser
    {
        /// <summary>
        /// Defines the possible roles a user can have within the application.
        /// </summary>
        public enum UserRole
        {
            Seller,
            Buyer,
            trader,
            Producer // Farmer - representing agricultural producers
        }

        /// <summary>
        /// Represents a user profile within the agricultural product marketplace application.
        /// Mimics some features found in platforms like دیوار (Divar).
        /// </summary>
        public class UserProfile : IProfile
        {
            /// <summary>
            /// Unique identifier for the user profile.
            /// </summary>
            [Required]
            public int Id { get; set; }

            /// <summary>
            /// User's first name.
            /// </summary>
            public string FirstName { get; set; } = string.Empty;

            /// <summary>
            /// User's last name.
            /// </summary>
            public string LastName { get; set; } = string.Empty;

            /// <summary>
            /// Timestamp of when the user registered.
            /// </summary>
            public DateTime RegistrationTime { get; set; }

            /// <summary>
            /// Timestamp of the user's last visit to the application.
            /// Nullable to account for new users or users who have not logged in.
            /// </summary>
            public DateTime? LastVisitTime { get; set; }

            /// <summary>
            /// User's city of residence.
            /// </summary>
            public string City { get; set; } = string.Empty;

            /// <summary>
            /// User's mobile phone number.  **IMPORTANT: Consider encryption for security.**
            /// </summary>
            public string MobileNumber { get; set; } = string.Empty;

            /// <summary>
            /// Indicates whether the user's identity has been verified.
            /// </summary>
            public bool IsVerified { get; set; }

            /// <summary>
            /// User's national identification number. **IMPORTANT: Consider encryption for security.**
            /// </summary>
            public string NationalId { get; set; } = string.Empty;

            /// <summary>
            /// URL to the user's profile picture.
            /// </summary>
            public string ProfilePictureUrl { get; set; } = string.Empty;

            /// <summary>
            /// List of advertisements the user has viewed.
            /// \n By ID.
            /// </summary>
            public ICollection<string> ViewedAdvertisements { get; set; } = new List<string>();

            /// <summary>
            /// List of advertisements the user has posted.
            /// \n By ID.
            /// </summary>
            public ICollection<string> PostedAdvertisements { get; set; } = new List<string>();

            /// <summary>
            /// List of advertisements currently under review by administrators.
            /// \n By ID.
            /// </summary>
            public ICollection<string> AdvertisementsUnderReview { get; set; } = new List<string>();

            /// <summary>
            /// List of advertisements that have been rejected.
            /// \n By ID.
            /// </summary>
            public ICollection<string> RejectedAdvertisements { get; set; } = new List<string>();

            /// <summary>
            /// The role of the user within the application.
            /// </summary>
            public UserRole Role { get; set; }

            /// <summary>
            ///  A description of the user (similar to دیوار).  Allows users to add additional information.
            /// </summary>
            public string Description { get; set; } = string.Empty;

            /// <summary>
            /// User's location (e.g., for map display).
            /// </summary>
            public string Location { get; set; } = string.Empty;

         
        }
    }

}

