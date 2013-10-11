using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sqrl.NET.Tests {
	[TestClass]
	public class EncodingUtilsTests {
		[TestMethod]
		public void FromBase64Url() {
			Assert.AreEqual(1, EncodingUtils.FromBase64Url("AQIDBA==")[0]);
			Assert.AreEqual(2, EncodingUtils.FromBase64Url("AQIDBA==")[1]);
			Assert.AreEqual(3, EncodingUtils.FromBase64Url("AQIDBA==")[2]);
			Assert.AreEqual(4, EncodingUtils.FromBase64Url("AQIDBA==")[3]);

			Assert.AreEqual(1, EncodingUtils.FromBase64Url("AQIDBAUG")[0]);
			Assert.AreEqual(2, EncodingUtils.FromBase64Url("AQIDBAUG")[1]);
			Assert.AreEqual(3, EncodingUtils.FromBase64Url("AQIDBAUG")[2]);
			Assert.AreEqual(4, EncodingUtils.FromBase64Url("AQIDBAUG")[3]);
			Assert.AreEqual(5, EncodingUtils.FromBase64Url("AQIDBAUG")[4]);
			Assert.AreEqual(6, EncodingUtils.FromBase64Url("AQIDBAUG")[5]);
		}

		[TestMethod]
		public void FromBase64Url_Dashes() {
			Assert.AreEqual(250, EncodingUtils.FromBase64Url("-g==")[0]);
		}

		[TestMethod]
		public void GetHashSha256() {
			Assert.AreEqual(124, EncodingUtils.GetHashSha256(1, 1)[0]);
		}

		[TestMethod]
		public void GetHashSha256_Length() {
			Assert.AreEqual(0, EncodingUtils.GetHashSha256(1, 0).Length);
			Assert.AreEqual(1, EncodingUtils.GetHashSha256(1, 1).Length);
			Assert.AreEqual(2, EncodingUtils.GetHashSha256(1, 2).Length);
			Assert.AreEqual(32, EncodingUtils.GetHashSha256(1, 100).Length);
		}

		[TestMethod]
		public void ToBase64Url() {
			Assert.AreEqual("AQIDBA==", EncodingUtils.ToBase64Url(new byte[] {1, 2, 3, 4}));
			Assert.AreEqual("AQIDBAUG", EncodingUtils.ToBase64Url(new byte[] {1, 2, 3, 4, 5, 6}));
		}

		[TestMethod]
		public void ToBase64Url_DashReplacesPlus() {
			Assert.AreEqual("-g==", EncodingUtils.ToBase64Url(new byte[] {250}));
		}

		[TestMethod]
		public void ToBase64Url_UnderscoreReplacesSlash() {
			Assert.AreEqual(255, EncodingUtils.FromBase64Url("_w==")[0]);
		}
	}
}