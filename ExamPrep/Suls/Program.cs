namespace Suls.App
{
    using SIS.MvcFramework;
    using System.Threading.Tasks;

    public class Program
    {
        static async Task Main()
        {
            await WebHost.StartAsync(new Startup());
        }
    }
}