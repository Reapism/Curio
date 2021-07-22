using System.IO;
using System.Reflection;

namespace Curio.Persistence.Tools
{
    public class ProjectPathRetriever
    {
        public static string GetWebApiPath()
        {
            var webApiProjectName = "Curio.WebApi";
            var srcDirectoryName = "src";
            var thisAssemblyPath = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            var srcPath = thisAssemblyPath.Parent.Parent.Parent.Parent.Parent.Parent.FullName;
            var webApiPath = Path.Combine(srcPath, srcDirectoryName, webApiProjectName);

            return webApiPath;
        }
    }
}
