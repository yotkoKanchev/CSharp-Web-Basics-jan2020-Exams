namespace PANDA.App
{
    using Microsoft.EntityFrameworkCore;
    using PANDA.Data;
    using PANDA.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Collections.Generic;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> serverRoutingTable)
        {
            using (var db = new PandaDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IReceiptsService, ReceiptsService>();
            serviceCollection.Add<IPackagesService, PackagesService>();
        }
    }
}
