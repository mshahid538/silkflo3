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

* GetIdea()
  Two overrides to get the parent idea for the object.
  This is used to display parent in a summary table.



* GetInvitedBy()
  Two overrides to get the parent invitedBy for the object.
  This is used to display parent in a summary table.

* GetInvitedBies()
  Two overrides to get a list of alternative parent invitedBies.

* GetUser()
  Two overrides to get the parent user for the object.
  This is used to display parent in a summary table.

* GetUsers()
  Two overrides to get a list of alternative parent users.* GetModels()
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

namespace SilkFlo.Web.Models.Business
{
    public partial class Collaborator : Abstract
    {
        private SilkFlo.Data.Core.Domain.Business.Collaborator _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Collaborator ()
        {
            _core = new SilkFlo.Data.Core.Domain.Business.Collaborator();
        }
        public Collaborator(SilkFlo.Data.Core.Domain.Business.Collaborator core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Business.Collaborator cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.Business.Collaborator GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Business.Collaborator core)
        {
            _core = core;
        }

        public bool IsNew => string.IsNullOrWhiteSpace(Id);

        /// <summary>
        /// Primary Key
        /// </summary>
        #region Id
        [StringLength(255,
                      MinimumLength = 0,
                      ErrorMessage = "Id cannot be greater than 255 characters in length.")]
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

        #region Invitation Confirmed
        [Required]
        [DisplayName("Invitation Confirmed")]
        public bool IsInvitationConfirmed
        {
            get => _core.IsInvitationConfirmed;
            set
            {
                if (_core.IsInvitationConfirmed == value)
                    return;

                _core.IsInvitationConfirmed = value;
            }
        }

        public string IsInvitationConfirmed_ErrorMessage { get; set; } = "Required";
        public bool IsInvitationConfirmed_IsInValid { get; set; }
        #endregion

        [Required]
        [DisplayName("Idea")]
        public string IdeaId
        {
            get => _core.IdeaId;
            set
            {
                if (_core.IdeaId == value)
                    return;

                _core.IdeaId = value;
                _idea = null;
            }
        }
        private Models.Business.Idea _idea;
        public Models.Business.Idea Idea
        {
            get
            {
                if (_idea != null)
                    return _idea;

                if (_core.Idea != null)
                    _idea = new Models.Business.Idea(_core.Idea);

                return _idea;
            }
            set
            {
                if (_idea == value)
                    return;

                _idea = value;

                if (_idea == null)
                    _core.Idea = null;
                else
                {
                    if (_core.IdeaId != _idea.Id)
                        _core.Idea = _idea.GetCore();

                    _core.IdeaId = _idea.Id;
                }
            }
        }

        [DisplayName("Idea")]
        public string IdeaString => Idea?.ToString();

        public string IdeaId_ErrorMessage { get; set; } = "Required";
        public bool IdeaId_IsInValid { get; set; }

        [DisplayName("Invited By")]
        public string InvitedById
        {
            get => _core.InvitedById;
            set
            {
                if (_core.InvitedById == value)
                    return;

                _core.InvitedById = value;
                _invitedBy = null;
            }
        }
        private Models.User _invitedBy;
        public Models.User InvitedBy
        {
            get
            {
                if (_invitedBy != null)
                    return _invitedBy;

                if (_core.InvitedBy != null)
                    _invitedBy = new Models.User(_core.InvitedBy);

                return _invitedBy;
            }
            set
            {
                if (_invitedBy == value)
                    return;

                _invitedBy = value;

                if (_invitedBy == null)
                    _core.InvitedBy = null;
                else
                {
                    if (_core.InvitedById != _invitedBy.Id)
                        _core.InvitedBy = null;

                    _core.InvitedById = _invitedBy.Id;
                }
            }
        }

        [DisplayName("Invited By")]
        public string InvitedByString => InvitedBy?.ToString();

        public string InvitedById_ErrorMessage { get; set; }
        public bool InvitedById_IsInValid { get; set; }

        [Required]
        [DisplayName("Collaborator")]
        public string UserId
        {
            get => _core.UserId;
            set
            {
                if (_core.UserId == value)
                    return;

                _core.UserId = value;
                _user = null;
            }
        }
        private Models.User _user;
        public Models.User User
        {
            get
            {
                if (_user != null)
                    return _user;

                if (_core.User != null)
                    _user = new Models.User(_core.User);

                return _user;
            }
            set
            {
                if (_user == value)
                    return;

                _user = value;

                if (_user == null)
                    _core.User = null;
                else
                {
                    if (_core.UserId != _user.Id)
                        _core.User = _user.GetCore();

                    _core.UserId = _user.Id;
                }
            }
        }

        [DisplayName("Collaborator")]
        public string UserString => User?.ToString();

        public string UserId_ErrorMessage { get; set; } = "Required";
        public bool UserId_IsInValid { get; set; }

        private List<Models.Business.Idea> _ideas;
        public List<Models.Business.Idea> Ideas
        {
            get => _ideas ??= new List<Models.Business.Idea>();
            set => _ideas = value;
        }

        private List<Models.User> _invitedBies;
        public List<Models.User> InvitedBies
        {
            get => _invitedBies ??= new List<Models.User>();
            set => _invitedBies = value;
        }

        private List<Models.User> _users;
        public List<Models.User> Users
        {
            get => _users ??= new List<Models.User>();
            set => _users = value;
        }

        private List<Models.Business.CollaboratorRole> _collaboratorRoles; 
        public List<Models.Business.CollaboratorRole> CollaboratorRoles 
        {
            get
            {
                if (_collaboratorRoles != null)
                    return _collaboratorRoles;

                _collaboratorRoles = new List<Business.CollaboratorRole>();

                if (_core.CollaboratorRoles == null)
                    return _collaboratorRoles;

                foreach (var core in _core.CollaboratorRoles)
                    _collaboratorRoles.Add(new Models.Business.CollaboratorRole(core));

                return _collaboratorRoles;
            }
            set => _collaboratorRoles = value;
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

        public List<Models.Selector> AllRoles_For_CollaboratorRoles { get; set; } = new List<Models.Selector>();

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
            if (feedback.Elements.ContainsKey("IdeaId"))
                feedback.Elements["IdeaId"] = message;
            else
                feedback.Elements.Add("IdeaId", message);

            if (feedback.Elements.ContainsKey("UserId"))
                feedback.Elements["UserId"] = message;
            else
                feedback.Elements.Add("UserId", message);


            feedback.IsValid = false;

            return feedback;
        }

        public static List<Models.Business.Collaborator> Create(IEnumerable<Data.Core.Domain.Business.Collaborator> cores)
        {
            return cores.Select(core => new Models.Business.Collaborator(core)).ToList();
        }
        
        public static Models.Business.Collaborator[] Create(Data.Core.Domain.Business.Collaborator[] cores)
        {
            return cores.Select(core => new Models.Business.Collaborator(core)).ToArray();
        }
    }
}