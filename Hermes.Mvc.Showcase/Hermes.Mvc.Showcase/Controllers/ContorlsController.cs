using System.Web.Mvc;

namespace Hermes.Mvc.Showcase.Controllers
{
    public class ControlsController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Tiles");
        }

                public ActionResult Tiles()
        {
            return View();
        }

                public ActionResult Carousel()
        {
            return View();
        }

        

    }
}