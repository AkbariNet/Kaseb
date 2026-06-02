using static KasebAPI.Models.Profile.ProfileModel;

namespace KasebAPI.Interfaces
{
    public interface IProfile
    {
        /// <summary>
        /// Unique identifier for the user profile.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// User's first name.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// User's last name.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Timestamp of when the user registered.
        /// </summary>
        DateTime RegistrationTime { get; set; }

        /// <summary>
        /// Timestamp of the user's last visit to the application.
        /// Nullable to account for new users or users who have not logged in.
        /// </summary>
        DateTime? LastVisitTime { get; set; }

        /// <summary>
        /// User's city of residence.
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// User's mobile phone number.  **IMPORTANT: Consider encryption for security.**
        /// </summary>
        string MobileNumber { get; set; }

        /// <summary>
        /// Indicates whether the user's identity has been verified.
        /// </summary>
        bool IsVerified { get; set; }

        /// <summary>
        /// User's national identification number. **IMPORTANT: Consider encryption for security.**
        /// </summary>
        string NationalId { get; set; }

        /// <summary>
        /// URL to the user's profile picture.
        /// </summary>
        string ProfilePictureUrl { get; set; }

        /// <summary>
        /// List of advertisements the user has viewed.
        /// \n By ID.
        /// </summary>
        ICollection<string> ViewedAdvertisements { get; set; }

        /// <summary>
        /// List of advertisements the user has posted.
        /// \n By ID.
        /// </summary>
        ICollection<string> PostedAdvertisements { get; set; }

        /// <summary>
        /// List of advertisements currently under review by administrators.
        /// \n By ID.
        /// </summary>
        ICollection<string> AdvertisementsUnderReview { get; set; }

        /// <summary>
        /// List of advertisements that have been rejected.
        /// \n By ID.
        /// </summary>
        ICollection<string> RejectedAdvertisements { get; set; }

        /// <summary>
        /// The role of the user within the application.
        /// </summary>
        UserRole Role { get; set; }

        /// <summary>
        ///  A description of the user (similar to دیوار).  Allows users to add additional information.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// User's location (e.g., for map display).
        /// </summary>
        string Location { get; set; }
    }
}
