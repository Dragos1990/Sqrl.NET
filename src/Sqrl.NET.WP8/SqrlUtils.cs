using System;
using System.Diagnostics;

namespace Sqrl.NET {
	public static class SqrlUtils {
		public static byte[] CreateClientNonce(long? timestamp = null) {
			if (timestamp == null) {
				timestamp = DateTime.Now.ToUniversalTime().Ticks;
			}
			return EncodingUtils.GetHashSha256(timestamp.Value, 9);
		}

		/*
		public static byte[] CreatePrivateKey(string masterPassword) {

			var hash = SHA256.Create().ComputeHash(value);
		}
		 * */

		public static byte[] CreateSignature(byte[] privateKey) {
			Debug.Assert(privateKey.Length == 32);
//			return PublicKeyAuth.Sign("hello", privateKey);
			return null;
		}

		public static byte[] CreatePublicKey(byte[] privateKey) {
			Debug.Assert(privateKey.Length == 32);
//			var result = Sodium.PublicKeyAuth.Sign("hello", privateKey);
//			result.
			return null;
		}
	}
}