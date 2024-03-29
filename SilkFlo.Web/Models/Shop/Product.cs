/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20220411 004
       Author .  .  .  .  .  .  .  .  Delaney

                      ,        ,--,_
                       \ _ ___/ /\|
                       ( )__, )
                      |/_  '--,
                        \ `  / '
 
 Note: Create this object,
       populate from properties from the Core.Domain classes
       and send to a view.

Object Models
-------------
What can this object do.

* GetPrices()
  Two overrides to get Prices children for this object.

* GetModels()
  Return a model containing properties populated with the objects values

* GetCreatedAndUpdated()
  Two overrides to get Users who created updated this object
  and assign them to CreatedBy and UpdatedBy properties.
 
 *********************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Shop
{
    public partial class Product : Abstract
    {
        private SilkFlo.Data.Core.Domain.Shop.Product _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Product ()
        {
            _core = new SilkFlo.Data.Core.Domain.Shop.Product();
        }
        public Product(SilkFlo.Data.Core.Domain.Shop.Product core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Shop.Product cannot be null");
        }
        #endregion

        #region Properties
        private string _displayText;
        public string DisplayText
        { 
            get => string.IsNullOrWhiteSpace(_displayText) ? Name : _displayText;
            set => _displayText = value;
        }
        public SilkFlo.Data.Core.Domain.Shop.Product GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Shop.Product core)
        {
            _core = core;
        }

        public bool IsNew => string.IsNullOrWhiteSpace(Id);

        /// <summary>
        /// Primary Key
        /// </summary>
        #region Id
        [StringLength(30,
                      MinimumLength = 0,
                      ErrorMessage = "Id cannot be greater than 30 characters in length.")]
        [DisplayName("Id")]
        public string Id
        {
            get => _core.Id;
            set
            {
                if (_core.Id == value)
                    return;

                _core.Id = value;
            }
        }

        public string Id_Error { get; set; }
        public string Id_ErrorMessage { get; set; } = "Required";
        public bool Id_IsInValid { get; set; }
        #endregion

        #region Admin User Limit
        [DisplayName("Admin User Limit")]
        public int? AdminUserLimit
        {
            get => _core.AdminUserLimit;
            set
            {
                if (_core.AdminUserLimit == value)
                    return;

                _core.AdminUserLimit = value;
            }
        }

        public string AdminUserLimit_ErrorMessage { get; set; }
        public bool AdminUserLimit_IsInValid { get; set; }
        #endregion

        #region Admin User Text
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Admin User Text cannot be greater than 100 characters in length.")]
        [DisplayName("Admin User Text")]
        public string AdminUserText
        {
            get => _core.AdminUserText;
            set
            {
                if (_core.AdminUserText == value)
                    return;

                _core.AdminUserText = value;
            }
        }

        public string AdminUserText_ErrorMessage { get; set; }
        public bool AdminUserText_IsInValid { get; set; }
        #endregion

        #region Collaborator Limit
        [DisplayName("Collaborator Limit")]
        public int? CollaboratorLimit
        {
            get => _core.CollaboratorLimit;
            set
            {
                if (_core.CollaboratorLimit == value)
                    return;

                _core.CollaboratorLimit = value;
            }
        }

        public string CollaboratorLimit_ErrorMessage { get; set; }
        public bool CollaboratorLimit_IsInValid { get; set; }
        #endregion

        #region Collaborator Text
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Collaborator Text cannot be greater than 100 characters in length.")]
        [DisplayName("Collaborator Text")]
        public string CollaboratorText
        {
            get => _core.CollaboratorText;
            set
            {
                if (_core.CollaboratorText == value)
                    return;

                _core.CollaboratorText = value;
            }
        }

        public string CollaboratorText_ErrorMessage { get; set; }
        public bool CollaboratorText_IsInValid { get; set; }
        #endregion

        #region Idea Limit
        [DisplayName("Idea Limit")]
        public int? IdeaLimit
        {
            get => _core.IdeaLimit;
            set
            {
                if (_core.IdeaLimit == value)
                    return;

                _core.IdeaLimit = value;
            }
        }

        public string IdeaLimit_ErrorMessage { get; set; }
        public bool IdeaLimit_IsInValid { get; set; }
        #endregion

        #region Idea Text
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Idea Text cannot be greater than 100 characters in length.")]
        [DisplayName("Idea Text")]
        public string IdeaText
        {
            get => _core.IdeaText;
            set
            {
                if (_core.IdeaText == value)
                    return;

                _core.IdeaText = value;
            }
        }

        public string IdeaText_ErrorMessage { get; set; }
        public bool IdeaText_IsInValid { get; set; }
        #endregion

        #region Is Live
        [Required]
        [DisplayName("Is Live")]
        public bool IsLive
        {
            get => _core.IsLive;
            set
            {
                if (_core.IsLive == value)
                    return;

                _core.IsLive = value;
            }
        }

        public string IsLive_ErrorMessage { get; set; } = "Required";
        public bool IsLive_IsInValid { get; set; }
        #endregion

        #region Is Visible
        [Required]
        [DisplayName("Is Visible")]
        public bool IsVisible
        {
            get => _core.IsVisible;
            set
            {
                if (_core.IsVisible == value)
                    return;

                _core.IsVisible = value;
            }
        }

        public string IsVisible_ErrorMessage { get; set; } = "Required";
        public bool IsVisible_IsInValid { get; set; }
        #endregion

        #region Name
        [Required]
        [StringLength(50,
                      ErrorMessage = "Name must be between 1 and 50 characters in length.")]
        [DisplayName("Name")]
        public string Name
        {
            get => _core.Name;
            set
            {
                if (_core.Name == value)
                    return;

                _core.Name = value;
            }
        }

        public string Name_ErrorMessage { get; set; } = "Required";
        public bool Name_IsInValid { get; set; }
        #endregion

        #region No Price
        [Required]
        [DisplayName("No Price")]
        public bool NoPrice
        {
            get => _core.NoPrice;
            set
            {
                if (_core.NoPrice == value)
                    return;

                _core.NoPrice = value;
            }
        }

        public string NoPrice_ErrorMessage { get; set; } = "Required";
        public bool NoPrice_IsInValid { get; set; }
        #endregion

        #region Note
        [StringLength(255,
                      MinimumLength = 0,
                      ErrorMessage = "Note cannot be greater than 255 characters in length.")]
        [DisplayName("Note")]
        public string Note
        {
            get => _core.Note;
            set
            {
                if (_core.Note == value)
                    return;

                _core.Note = value;
            }
        }

        public string Note_ErrorMessage { get; set; }
        public bool Note_IsInValid { get; set; }
        #endregion

        #region Sort
        [Required]
        [DisplayName("Sort")]
        public int Sort
        {
            get => _core.Sort;
            set
            {
                if (_core.Sort == value)
                    return;

                _core.Sort = value;
            }
        }

        public string Sort_ErrorMessage { get; set; } = "Required";
        public bool Sort_IsInValid { get; set; }
        #endregion

        #region Standard User Limit
        [DisplayName("Standard User Limit")]
        public int? StandardUserLimit
        {
            get => _core.StandardUserLimit;
            set
            {
                if (_core.StandardUserLimit == value)
                    return;

                _core.StandardUserLimit = value;
            }
        }

        public string StandardUserLimit_ErrorMessage { get; set; }
        public bool StandardUserLimit_IsInValid { get; set; }
        #endregion

        #region Standard User Text
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Standard User Text cannot be greater than 100 characters in length.")]
        [DisplayName("Standard User Text")]
        public string StandardUserText
        {
            get => _core.StandardUserText;
            set
            {
                if (_core.StandardUserText == value)
                    return;

                _core.StandardUserText = value;
            }
        }

        public string StandardUserText_ErrorMessage { get; set; }
        public bool StandardUserText_IsInValid { get; set; }
        #endregion

        #region Storage Limit
        [DisplayName("Storage Limit")]
        public int? StorageLimit
        {
            get => _core.StorageLimit;
            set
            {
                if (_core.StorageLimit == value)
                    return;

                _core.StorageLimit = value;
            }
        }

        public string StorageLimit_ErrorMessage { get; set; }
        public bool StorageLimit_IsInValid { get; set; }
        #endregion

        #region Storage Text
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Storage Text cannot be greater than 100 characters in length.")]
        [DisplayName("Storage Text")]
        public string StorageText
        {
            get => _core.StorageText;
            set
            {
                if (_core.StorageText == value)
                    return;

                _core.StorageText = value;
            }
        }

        public string StorageText_ErrorMessage { get; set; }
        public bool StorageText_IsInValid { get; set; }
        #endregion

        private List<Models.Shop.Price> _prices; 
        public List<Models.Shop.Price> Prices 
        {
            get
            {
                if (_prices != null)
                    return _prices;

                _prices = new List<Shop.Price>();

                if (_core.Prices == null)
                    return _prices;

                foreach (var core in _core.Prices)
                    _prices.Add(new Models.Shop.Price(core));

                return _prices;
            }
            set => _prices = value;
        }

        #region OwnerId
        public string OwnerId => UpdatedById ?? CreatedById;
        #endregion

        #region Owner  
        public string Owner => UpdatedBy ?? CreatedBy;
        #endregion
        

        #region CreatedBy
        [DisplayName("Created By")]
        public string CreatedBy => _core.CreatedBy == null ? "" : _core.CreatedBy.ToString();
        #endregion

        #region CreatedDate
        [DisplayName("Created Date")]
        public DateTime? CreatedDate
        {
            get => _core.CreatedDate;
            set
            {
                if (_core.CreatedDate == value)
                    return;

                _core.CreatedDate = value;
            }
        }
        #endregion

        [DisplayName("Created Date")]
        public string CreatedDateString => CreatedDate?.ToString(Data.Core.Settings.DateFormatShort);

        #region CreatedById
        [DisplayName("Created By Id")]
        public string CreatedById
        {
            get => _core.CreatedById;
            set => _core.CreatedById = value;
        }
        #endregion

        #region UpdatedById
        [DisplayName("Updated By Id")]
        public string UpdatedById
        {
            get => _core.UpdatedById;
            set => _core.UpdatedById = value;
        }
        #endregion

        #region UpdatedBy
        [DisplayName("Updated By")]
        public string UpdatedBy => _core.UpdatedBy == null ? "" : _core.UpdatedBy.ToString();
        #endregion

        #region UpdatedDate
        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate
        {
            get => _core.UpdatedDate;
            set
            {
                if (_core.UpdatedDate == value)
                    return;

                _core.UpdatedDate = value;
            }
        }
        #endregion

        [DisplayName("Updated Date")]
        public string UpdatedDateString => UpdatedDate?.ToString(Data.Core.Settings.DateTimeFormatShort);


        [DisplayName("Date")]
        public string DateDisplayed => UpdatedDate == null ? CreatedDate?.ToString("yyyy-MM-dd") : UpdatedDate?.ToString("yyyy-MM-dd");

        [DisplayName("Date Time")]
        public string DateTimeDisplayed => UpdatedDate == null ? CreatedDate?.ToString("yyyy-MM-dd HH:mm:ss") : UpdatedDate?.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #region IsDeleted
        [DisplayName("Is Deleted")]
        public bool IsDeleted
        {
            get => _core.IsDeleted;
            set
            {
                if (_core.IsDeleted == value)
                    return;

                _core.IsDeleted = value;
            }
        }
        #endregion

        #region IsSaved
        [DisplayName("Is Saved")]
        public bool IsSaved => _core.IsSaved;
        #endregion

        public async Task GetCreatedAndUpdated(Data.Core.IUnitOfWork unitOfWork)
        {
            _core.CreatedBy = await unitOfWork.Users.GetAsync(_core.CreatedById);
            _core.UpdatedBy = await unitOfWork.Users.GetAsync(_core.UpdatedById);
        }

        /// <summary>
        /// Check unique key constraints.
        /// </summary>
        /// <returns>ViewModels.Feedback</returns>
        public async Task<ViewModels.Feedback> CheckUniqueAsync(
        Data.Core.IUnitOfWork unitOfWork,
        ViewModels.Feedback feedback)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("Data.Core.IUnitOfWork cannot be null");

            // Check unique
            var message = await Data.Persistence.UnitOfWork.IsUniqueAsync(GetCore());// Data.Persistence.UnitOfWork.IsUniqueAsync(GetCore());

            if (string.IsNullOrWhiteSpace(message)) 
                return feedback;


             // We have a conflict, give feedback.
            if (feedback.Elements.ContainsKey("IsLive"))
                feedback.Elements["IsLive"] = message;
            else
                feedback.Elements.Add("IsLive", message);

            if (feedback.Elements.ContainsKey("Name"))
                feedback.Elements["Name"] = message;
            else
                feedback.Elements.Add("Name", message);


            feedback.IsValid = false;

            return feedback;
        }

        public override string ToString()
        {
            if(!string.IsNullOrWhiteSpace(DisplayText))
                return DisplayText;

            return Name;
        }
        public static List<Models.Shop.Product> Create(IEnumerable<Data.Core.Domain.Shop.Product> cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
            {
                var models = new List<Models.Shop.Product>();
                models.Add(new Models.Shop.Product
                {
                    DisplayText = "<Empty>"
                });

                models.AddRange(cores.Select(core => new Models.Shop.Product(core)));
                return models;
            }

            return cores.Select(core => new Models.Shop.Product(core)).ToList();
        }

        public static Models.Shop.Product[] Create(Data.Core.Domain.Shop.Product[] cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
                return Create(cores.ToList(), true).ToArray();

            return cores.Select(core => new Models.Shop.Product(core)).ToArray();
        }
    }
}