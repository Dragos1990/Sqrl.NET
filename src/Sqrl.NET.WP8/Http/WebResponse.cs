using System.Net.Http;

namespace Sqrl.NET.WP8.Http {
	public class WebResponse {
		public HttpResponseMessage Response { get; set; }

		public WebResponse() {
		}

		public WebResponse(HttpResponseMessage response) {
			Response = response;
		}
	}

	public class WebResponse<T> : WebResponse {
		public T Model { get; set; }

		public WebResponse(HttpResponseMessage response) : base(response) {
		}

		public WebResponse() {
		}

		public WebResponse(HttpResponseMessage response, T model) {
			Response = response;
			Model = model;
		}
	}
}