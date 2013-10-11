using System.Web;

namespace Sqrl.NET {
	public static class UrlUtils {
		public static string AddParameter(string url, string name, string value) {
			if (!url.Contains("?")) {
				url += "?";
			}
			if (url.Contains("&") && !url.EndsWith("&")) {
				url += "&";
			}

			return url + name + "=" + value;
		}
	}
}