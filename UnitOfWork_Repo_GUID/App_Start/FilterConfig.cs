using System.Web;
using System.Web.Mvc;

namespace UnitOfWork_Repo_GUID
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
