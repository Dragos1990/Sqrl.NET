using System;
using System.Security.Cryptography;

namespace Sqrl.NET {
	public static class EncodingUtils {
		public static byte[] FromBase64Url(string value) {
			var encoded = value;
			encoded = encoded.Replace("-", "+");
			encoded = encoded.Replace("_", "/");
			return Convert.FromBase64String(encoded);
		}

		public static byte[] GetHashSha256(long value, int maxLength) {
			var valueBytes = BitConverter.GetBytes(value);
			var hashBytes = SHA256.Create().ComputeHash(valueBytes);
			if (hashBytes.Length > maxLength) {
				var newBytes = new byte[maxLength];
				Array.Copy(hashBytes, newBytes, maxLength);
				hashBytes = newBytes;
			}
			return hashBytes;
		}

		public static string ToBase64Url(byte[] value) {
			var encoded = Convert.ToBase64String(value);
			encoded = encoded.Replace("+", "-");
			encoded = encoded.Replace("/", "_");
			return encoded;
		}
	}
}