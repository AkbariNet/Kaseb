using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ConvertCore = KasebCore.Services.Converting.Convert;

namespace KasebCore.Models.Element
{
    public class AdElementModel
    {
        public GetResoultInfo IsValidToUpload()
        {
            if (
                !string.IsNullOrWhiteSpace(Title)
                && Title.Count() >= 5
                && !string.IsNullOrWhiteSpace(Content)
                && Content.Count() >= 10
                && !string.IsNullOrWhiteSpace(City)
                && Category != null
                && Category.Value != Element.Category.IsNull
                && Price > 1000
                && Price < 500000
                && AreaValue > 10
                && AreaValue < 999000
   )
            {
                return new GetResoultInfo()
                {
                    IsSuccess = true,
                    Message = "Ad validation completed successfully.",
                    StatusCode = 400
                };
            }
            else
            {
                string ErrorMessage = "";

                if (string.IsNullOrWhiteSpace(Title))
                {
                    ErrorMessage += "Please enter a title.\n";
                }
                else if (Title.Length < 5)
                {
                    ErrorMessage += "Title must be at least 5 characters long.\n";
                }




                if (string.IsNullOrWhiteSpace(Content))
                {
                    ErrorMessage += "Please enter a description.\n";
                }

                else if (Content.Length < 10)
                {
                    ErrorMessage += "Description must be at least 10 characters long.\n";
                }


                if (string.IsNullOrWhiteSpace(City))
                {
                    ErrorMessage += "Please select a city.\n";
                }

                if (Category == null || Category.Value == Element.Category.IsNull)
                {
                    ErrorMessage += "Please select a category.\n";
                }

                if (Price <= 1 || Price >= 5000000)
                {
                    ErrorMessage += "Price must be between 1 and 5,000,000.";
                }

                if (AreaValue <= 10 || AreaValue >= 999000)
                {
                    ErrorMessage += "وزن باید بین 10 و 999000 کیلوگرم باشد.\n";
                }

                #region  //-----------------------------FOR DEBUGING---------------------------------//
                bool a = Title != null;
                bool aB = string.IsNullOrWhiteSpace(Title);
                if (aB)
                {
                    bool aa = Title.Count() >= 5;

                }
                bool awa = Content != null;
                if (awa)
                {

                    bool aawd = Content.Count() >= 10;
                }
                bool aasd = string.IsNullOrWhiteSpace(Content);
                bool asdf = City != null;
                bool aaasd = string.IsNullOrWhiteSpace(City);
                bool aas = Category != null;
                if (aas)
                {
                    bool aw2e2 = Category.Value != Element.Category.IsNull;

                }
                bool awser3 = Price > 1000;
                bool a43 = Price < 500000;
                bool aasdc = AreaValue > 10;
                bool aasd3 = AreaValue < 999000;
                #endregion

                return new GetResoultInfo()
                {
                    IsSuccess = false,
                    Message = $"شکست در بررسی آگهی! متن خطا:\n{ErrorMessage}",
                    StatusCode = 400
                };
            }

        }
        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int? Id { get; set; }


        [Required(ErrorMessage = "انتخاب دسته بندی الزامی ست.")]

        //دسته بندی باید چک شود و مقدار داشته باشد
        [JsonPropertyName("category")]
        public Category? Category { get; set; }
        public string? CategoryString { get; set; }

        [Required(ErrorMessage = "نوشتن عنوان الزامی ست.")]
        //عنوان باید حداقل 10 کارکتر داشته باشد
        [StringLength(500, ErrorMessage = "عنوان نمی تواند بیشتر از 100 کارکتر باشد.")]
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "نوشتن توضیحات الزامی ست.")]

        //توضیحات باید حداقل 10 کارکتر داشته باشد
        [StringLength(500, ErrorMessage = "توضیحات نمی تواند بیشتر از 500 کارکتر باشد.")]
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "انتخاب شهر الزامی ست.")]
        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;


        public Cities? _cities;
        public Cities? Cities
        {
            get { return _cities; }

            set
            {
                _cities = value;
                City = ConvertCore.ConvertCityListToCityString(_cities);
            }
        }

        [JsonPropertyName("images")]
        public List<AdImage>? Images { get; set; } = new();

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "وارد کردن مبلغ الزامی ست.")]
        //باید مبلغ از 1000 تومان تا 500 میلیون تومان محدود شود

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        public string PriceString {
            get
            {
                return ConvertCore.ConvertPriceDecimalToPriceStringWithSeprator(Price);
            }
        }

        [JsonPropertyName("isUrgent")]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("isUrgentRequest")]
        public bool IsUrgentRequest { get; set; }

        [Required(ErrorMessage = "you must enter the Area Value.")]
        
        [JsonPropertyName("areaValue")]
        public decimal AreaValue { get; set; }

        [JsonPropertyName("valueOfTag1")]
        public string ValueOfTag1 { get; set; } = string.Empty;

        [JsonPropertyName("valueOfTag2")]
        public string ValueOfTag2 { get; set; } = string.Empty;

        [JsonPropertyName("nonCash")]
        public bool NonCash { get; set; }

        [JsonPropertyName("someOfCashMostPayed")]
        public bool SomeOfCashMostPayed { get; set; }

        [JsonPropertyName("monthForNonCash")]
        public int MonthForNonCash { get; set; }  // حداکثر 120

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("inventoryGuarantee")]
        public int InventoryGuarantee { get; set; }  // 1 تا 30 }


        [JsonPropertyName("ImagePaths")]
        public List<string> ImagePaths { get; set; } = new();

        public List<string> ImageLinks { get; set; } = new();

        private string _mainImageLink = string.Empty;
        public string MainImageLink
        {
            get
            {
                if (string.IsNullOrEmpty(_mainImageLink))
                {
                    return "image_splash.png";
                }
                else
                {
                    return _mainImageLink;
                }
            }
            set
            {
                _mainImageLink = value;
            }
        }

        public string ValueSummery
        {
            get => KasebCore.Services.Combining.Combine.CombineDateAndCity(Date, City);
        }

        public AdElementModel()
        {

        }


        public bool IsShowAreaMeter
        {
            get
            {

                if (AreaValue != 0)
                {
                    return true;
                }
                else
                    return false;

            }
        }


        //Propery For Tag1
        public bool IsShowTag1
        {
            get
            {

                if (ValueOfTag1 != null && ValueOfTag1 != string.Empty
                    && ValueOfTag1 != "")
                {
                    return true;
                }
                else
                    return false;

            }
        }

        //Propery For Tag2
        public bool IsShowTag2
        {
            get
            {

                if (ValueOfTag2 != null && ValueOfTag2 != string.Empty
                    && ValueOfTag2 != "")
                {
                    return true;
                }
                else
                    return false;

            }
        }

        //Property For Showing String AreaValue

        public string AreaValueString
        {
            get
            {
                return ConvertCore.ConvertMeterDecimalToMeterString(AreaValue);
            }
        }

    }



    public enum Category
    {
        IsNull,
        house,
        apartment,
        Rent,
        Worn_texture,
        Land,


    }
    public enum Cities
    {
        IsNull,
        Yerevan,
        Gyumri,
        Vanadzor,
        Abovyan,
        Vagharshapat,
        Hrazdan,
        Kapan,
        Armavir,
        Artashat,
        Ijevan,
        Gavar,
        Goris,
        Charentsavan,
        Masis,
        Ashtarak,
        Sevan,
        Dilijan,
        Spitak,
        Sisian,
        Stepanavan,
        Martuni,
        Vardenis,
        Yeghvard,
        Byureghavan,
        NorHachn,
        Aparan,
        Berd,
        Tashir,
        Alaverdi,
        Noyemberyan,
        Jermuk,
        Chambarak,
        Metsamor,
        Vedi,
        Maralik,
        Talin,
        Tumanyan,
        Meghri,
        Agarak,
        Kajaran,
        Dastakert,
        Shamlugh,
        Ayrum,
        Tsaghkadzor,
        Ararat



    }
}
