using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Settings
{
    public class APIAccess
    {
        public APIAccess(string tab, bool isPractice, List<Data.Core.Domain.ClientApplicationInterfaceSession> details)
        {
            Tab = tab;
            IsPractice = isPractice;
            Details = details;
        }

        public string Tab { get; }
        public bool IsPractice { get; }

        public List<Data.Core.Domain.ClientApplicationInterfaceSession> Details { get; set; }
    }
}

