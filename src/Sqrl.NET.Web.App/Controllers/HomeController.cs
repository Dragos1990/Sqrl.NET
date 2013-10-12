using System.Web.Mvc;
using Sqrl.NET.Web.App.Models;

namespace Sqrl.NET.Web.App.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			return View(new LoginModel());
		}
	}
}
