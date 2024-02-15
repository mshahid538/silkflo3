namespace SilkFlo.Web.ViewModels.Settings
{
    public class APIAccess
    {
        public APIAccess(string tab, bool isPractice)
        {
            Tab = tab;
            IsPractice = isPractice;
        }

        public string Tab { get; }
        public bool IsPractice { get; }
    }
}

