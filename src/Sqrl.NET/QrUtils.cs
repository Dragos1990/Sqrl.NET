using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Sqrl.NET {
	public static class QrUtils {
		public static byte[] GenerateQr(string value, ImageFormat imageFormat) {
			var encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
			var bitmap = encoder.Encode(value);
			using (MemoryStream stream = new MemoryStream()) {
				bitmap.Save(stream, imageFormat);
				return stream.ToArray();
			}
		}
		
	}
}