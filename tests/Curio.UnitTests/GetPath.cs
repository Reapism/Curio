using System.IO;
using System.Reflection;
using Xunit;

namespace Curio.UnitTests
{
    public class GetPath
    {
        [Fact]
        public void GetWebApiPathTest()
        {
            var path = GetWebApiPath();
            var c = 0;
        }

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
