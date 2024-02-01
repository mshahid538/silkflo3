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

* GetClients()
  Two overrides to get Clients children for this object.
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
    public partial class Currency : Abstract
    {
        private SilkFlo.Data.Core.Domain.Shop.Currency _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Currency ()
        {
            _core = new SilkFlo.Data.Core.Domain.Shop.Currency();
        }
        public Currency(SilkFlo.Data.Core.Domain.Shop.Currency core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Shop.Currency cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.Shop.Currency GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Shop.Currency core)
        {
            _core = core;
        }

        public bool IsNew => string.IsNullOrWhiteSpace(Id);

        /// <summary>
        /// Primary Key
        /// </summary>
        #region Id
        [StringLength(3,
                      MinimumLength = 0,
                      ErrorMessage = "Id cannot be greater than 3 characters in length.")]
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

        #region Symbol
        [Required]
        [StringLength(1,
                      ErrorMessage = "Symbol must be between 1 and 1 characters in length.")]
        [DisplayName("Symbol")]
        public string Symbol
        {
            get => _core.Symbol;
            set
            {
                if (_core.Symbol == value)
                    return;

                _core.Symbol = value;
            }
        }

        public string Symbol_ErrorMessage { get; set; } = "Required";
        public bool Symbol_IsInValid { get; set; }
        #endregion

        private List<Models.Business.Client> _clients; 
        public List<Models.Business.Client> Clients 
        {
            get
            {
                if (_clients != null)
                    return _clients;

                _clients = new List<Business.Client>();

                if (_core.Clients == null)
                    return _clients;

                foreach (var core in _core.Clients)
                    _clients.Add(new Models.Business.Client(core));

                return _clients;
            }
            set => _clients = value;
        }

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
            if (feedback.Elements.ContainsKey("Id"))
                feedback.Elements["Id"] = message;
            else
                feedback.Elements.Add("Id", message);

            if (feedback.Elements.ContainsKey("Symbol"))
                feedback.Elements["Symbol"] = message;
            else
                feedback.Elements.Add("Symbol", message);


            feedback.IsValid = false;

            return feedback;
        }

        #region Name Property
        public string Name => Id + ", " + Symbol;
        #endregion

        public override string ToString()
        {
            return Name;
        }
        public static List<Models.Shop.Currency> Create(IEnumerable<Data.Core.Domain.Shop.Currency> cores)
        {
            return cores.Select(core => new Models.Shop.Currency(core)).ToList();
        }
        
        public static Models.Shop.Currency[] Create(Data.Core.Domain.Shop.Currency[] cores)
        {
            return cores.Select(core => new Models.Shop.Currency(core)).ToArray();
        }
    }
}