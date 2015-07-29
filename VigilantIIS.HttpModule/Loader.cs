using Microsoft.Web.Infrastructure.DynamicModuleHelper;
namespace VigilantIIS.HttpModule
{
    public class Loader
    {
        public static void LoadModule()
        {
            DynamicModuleUtility.RegisterModule(typeof(HttpModule));
        }         
    }
}