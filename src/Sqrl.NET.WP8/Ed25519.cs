namespace Sqrl.NET {
	public class Ed25519 {
		public byte[] PublicKey { get; set; }
		public byte[] Signature { get; set; }

		public Ed25519(string nonce) {
//			Sodium.PublicKeyAuth.Sign("hello", )
			PublicKey = new byte[] {1, 2, 3, 4, 5};
			Signature = new byte[] {1, 2, 3, 4, 5};
		}
	}
}