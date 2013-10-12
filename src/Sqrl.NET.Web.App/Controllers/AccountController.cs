using System.Drawing.Imaging;
using System.Web.Mvc;
using System.Web.Security;
using Sqrl.NET.Web.App.Models;

namespace Sqrl.NET.Web.App.Controllers {
	public static class MimeType {
		public const string Default = "application/octet-stream";
		public const string Excel = "application/vnd.ms-excel";
		public const string ExcelX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		public const string Gif = "image/gif";
		public const string Html = "text/html";
		public const string JavaScript = "text/javascript";
		public const string Jpeg = "image/jpeg";
		public const string MsWord = "application/msword";
		public const string MsWordX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
		public const string Pdf = "application/pdf";
		public const string Png = "image/png";
		public const string Powerpoint = "application/vnd.ms-powerpoint";
		public const string PowerpointX = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
		public const string Text = "text/plain";
		public const string Tiff = "image/tiff";
		public const string Xml = "text/xml";
	}
	public class AccountController : Controller {
		[HttpPost]
		public ActionResult Login(LoginModel model) {
			FormsAuthentication.SetAuthCookie(model.Username, false);

			return View("LoggedIn", model);
		}

		public ActionResult QrCode() {
			var qrCode = QrUtils.GenerateQr("http://www.sciencetrax.com", ImageFormat.Png);
			return File(qrCode, MimeType.Pdf, "login.png");
		}
	}
}