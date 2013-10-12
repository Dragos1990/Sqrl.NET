using System;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;

namespace Sqrl.NET.WP8.Http {
	public class Client {
		public WebResponse<T> Delete<T>(string url) {
			return Response<T>("delete", url, null);
		}

		public WebResponse Delete(string url) {
			return Response("delete", url, null);
		}

		public WebResponse<T> Get<T>(string url) {
			return Response<T>("get", url, null);
		}

		public WebResponse Get(string url) {
			return Response("get", url, null);
		}

		public WebResponse Post(string url, object model = null) {
			return Response("post", url, model);
		}

		public WebResponse<T> Post<T>(string url, object model = null) {
			return Response<T>("post", url, model);
		}

		public WebResponse Put(string url, object model = null) {
			return Response("put", url, model);
		}

		public WebResponse<T> Put<T>(string url, object model = null) {
			return Response<T>("put", url, model);
		}

		private WebResponse<T> Response<T>(string method, string url, object model) {
			var result = new WebResponse<T>();
			Response(method, url, model, response => {
				result.Response = response;
				try {
					var json = response.Content.ReadAsStringAsync().Result;
					result.Model = JsonConvert.DeserializeObject<T>(json);
					// ReSharper disable EmptyGeneralCatchClause
				} catch {
					// ReSharper restore EmptyGeneralCatchClause
				}
			});
			return result;
		}

		private WebResponse Response(string method, string url, object model) {
			var result = new WebResponse();
			Response(method, url, model, response => { result.Response = response; });
			return result;
		}

		private void Response(string method, string url, object model, Action<HttpResponseMessage> action) {
			using (var client = new HttpClient()) {
				switch (method.ToLower()) {
					case "delete":
						using (var response = client.DeleteAsync(url, CancellationToken.None).Result) {
							action(response);
						}
						break;
					case "get":
						using (var response = client.GetAsync(url, CancellationToken.None).Result) {
							action(response);
						}
						break;
					case "post":

						using (var response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model)), CancellationToken.None).Result) {
							action(response);
						}
						break;
					case "put":
						using (var response = client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(model)), CancellationToken.None).Result) {
							action(response);
						}
						break;
				}
			}
		}
	}
}