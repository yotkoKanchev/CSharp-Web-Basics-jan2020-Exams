namespace PANDA.App
{
    using System.Threading.Tasks;
    using SIS.MvcFramework;

    public class Launcher
    {
        static async Task Main()
        {
            await WebHost.StartAsync(new Startup());
        }
    }
}
