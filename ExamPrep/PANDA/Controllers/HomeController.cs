namespace PANDA.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
            //    var items = this.productsService.GetAllProducts();

            //    var viewModel = new ListAllProductsViewModel
            //    {
            //        Items = items,
            //    };

                return this.View(/*viewModel, */"Index");
            }

            return this.View("guest-home");
        }
    }
}
