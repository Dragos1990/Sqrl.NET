using System;
using ZXing;

namespace Sqrl.NET.WP8.App {
	public class PhotoLuminanceSource : LuminanceSource {
		public byte[] PreviewBufferY { get; set; }

		public override byte[] Matrix {
			get { return (byte[]) (Array) PreviewBufferY; }
		}

		public PhotoLuminanceSource(int width, int height)
			: base(width, height) {
			PreviewBufferY = new byte[width*height];
		}

		public override byte[] getRow(int y, byte[] row) {
			if (row == null || row.Length < Width) {
				row = new byte[Width];
			}

			for (var i = 0; i < Height; i++) {
				row[i] = (byte) PreviewBufferY[i*Width + y];
			}

			return row;
		}
	}
}