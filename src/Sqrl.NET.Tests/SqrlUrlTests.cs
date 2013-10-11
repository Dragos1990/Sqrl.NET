using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sqrl.NET.Tests {
	[TestClass]
	// Testing VS Git Integration
	public class SqrlUrlTests {
		private string ValidChallengeUri = "sqrl://domain.com/path/sqlrauth.ext?" +
		                                   "d=5" +
		                                   "&ip=127.0.0.1" +
		                                   "&webnonce=session-tag";


		private string ValidResponseUri = "sqrl://domain.com/path/sqlrauth.ext?" +
		                                  "d=5" +
		                                  "&ip=127.0.0.1" +
		                                  "&webnonce=session-tag" +
		                                  "&sqrlnon=client-nonce" +
		                                  "&sqrlsig=crypt-signat" +
		                                  "&sqrlkey=public-keyid" +
		                                  "&sqrlver=sqrl-version" +
		                                  // %3D - "="
		                                  // %3B - ";"
		                                  "&sqrlopt=opt1%3D1%3Bopt2%3D2";

		[TestMethod]
		public void Constructor() {
			var url = new SqrlUrl(ValidResponseUri); 
			Assert.AreEqual(5, url.Depth);
			Assert.AreEqual("domain.com/path", url.Domain);
			Assert.AreEqual("127.0.0.1", url.IpAddress.ToString());
			Assert.AreEqual("client-nonce", url.Nonce);
			Assert.AreEqual("1", url.Options["opt1"]);
			Assert.AreEqual("2", url.Options["opt2"]);
			Assert.AreEqual("public-keyid", url.PublicKey);
			Assert.AreEqual("sqrl", url.Scheme);
			Assert.AreEqual("crypt-signat", url.Signature);
			Assert.AreEqual("sqrl-version", url.Version);
			Assert.AreEqual("session-tag", url.WebNonce);
		}

		[TestMethod]
		public void CreateNonce() {
			var timestamp = 100000;			
			Assert.AreEqual(24, SqrlUtils.CreateClientNonce(timestamp)[0]);
		}

		[TestMethod]
		public void CreateNonce_Length() {
			var timestamp = 100000;
			Assert.AreEqual(9, SqrlUtils.CreateClientNonce(timestamp).Length);
		}

		[TestMethod]
		public void GetClientResponse() {
			var url = new SqrlUrl(ValidChallengeUri);
			Assert.AreEqual("", url.GetClientResponse(SqrlUtils.CreateClientNonce()));
		}
	}
}