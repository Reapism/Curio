using BenchmarkDotNet.Attributes;

namespace Curio.Benchmarks
{
    [BenchmarkCategory("WebAPI")]
    public class LoginApiCall
    {
        [Benchmark]
        public void Login()
        {

        }
    }
}
