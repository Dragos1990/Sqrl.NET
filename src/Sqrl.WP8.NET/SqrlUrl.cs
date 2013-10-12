using System;
using System.Collections.Generic;
using System.Net;

namespace Sqrl.NET {
	public class HttpUtils {
		public static IDictionary<string, string> ParseQueryString(string query) {
			var variables = query.Split('&');
			var dictionary = new Dictionary<string, string>();
			foreach (var variable in variables) {
				var parts = variable.Split('=');
				// TODO: Need to UrlDecode the name/value
				dictionary.Add(parts[0], parts[1]);
			}
			return dictionary;
		}
	}
	public class SqrlUrl {
		public int Depth { get; private set; }
		public string Domain { get; private set; }
		public IPAddress IpAddress { get; private set; }
		public IDictionary<string, string> Options { get; private set; }
		public string Nonce { get; private set; }
		public string PublicKey { get; private set; }
		public string Scheme { get; private set; }
		public string Signature { get; private set; }
		public string Version { get; private set; }
		public string WebNonce { get; private set; }

		private Uri Uri { get; set; }

		public SqrlUrl(string url) {
			Uri = new Uri(url);
			var parameters = HttpUtils.ParseQueryString(Uri.Query);

			var ip = parameters["ip"];
			if (!string.IsNullOrEmpty(ip)) {
				IpAddress = IPAddress.Parse(ip);
			}

			Depth = 1;
			if (!string.IsNullOrEmpty(parameters["d"])) {
				Depth = int.Parse(parameters["d"]);
			}
			Domain = Uri.Host + Uri.AbsolutePath.Substring(0, Depth);
			Nonce = parameters["sqrlnon"];
			PublicKey = parameters["sqrlkey"];
			Scheme = Uri.Scheme;
			Signature = parameters["sqrlsig"];
			Version = parameters["sqrlver"];
			WebNonce = parameters["webnonce"];

			var options = parameters["sqrlopt"];
			Options = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(options)) {
				var optionsArray = options.Split(';');
				foreach (var option in optionsArray) {
					var parts = option.Split('=');
					if (parts.Length == 1) {
						Options.Add(parts[0], null);
					} else {
						Options.Add(parts[0], parts[1]);
					}
				}
			}
		}

		public string GetClientResponse(byte[] nonce) {
			var encodedNonce = EncodingUtils.ToBase64Url(nonce);
			var url = Uri.Host + Uri.PathAndQuery;
			url = UrlUtils.AddParameter(url, "sqrlnon", encodedNonce);

			var key = new Ed25519(url);

			url = UrlUtils.AddParameter(url, "sqlrsig", EncodingUtils.ToBase64Url(key.Signature));
			url = UrlUtils.AddParameter(url, "sqlrkey", EncodingUtils.ToBase64Url(key.PublicKey));

			return url;
		}
	}
}