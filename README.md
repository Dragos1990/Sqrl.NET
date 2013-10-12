Sqrl.NET
========

GRC SQRL Library for .NET

# Directory Structure
```
  |- src/
  |  |- Sqrl.NET
  |  |- Sqrl.NET.Tests
  |  |- Sqrl.NET.Web.App
  |  |- Sqrl.NET.Web.App.Tests
  |  |- Sqrl.NET.WP8
  |  |- Sqrl.NET.WP8.Tests
  |  |- Sqrl.NET.WP8.App
  |  |- Sqrl.NET.WP8.App.Tests
  |- Sqrl.NET.sln
```

- Sqrl.NET - This is the main library for SQRL. This library includes all the code necessary
  to use the SQRL protocol in either a client or server.
- Sqrl.NET.Web.App - This is a reference web application that implements the SQRL protocol.
- Sqrl.NET.WP8 - This is the main WP8 library to use the SQRL protocol on Windows Phone 8.
- Sqrl.NET.WP8.App - This is a reference WP8 SQRL client used to login to SQRL enabled websites.

# Test Data Needed

We need to define some test cases for the encoding/decoding of data as well as example 
data for the encryption/decryption values.

- 72-bit nonce
- 256-bit public key
- 512-bit signature
- example web challenge
- example client response

By creating this test data, we can verify that all code is using the algorithms correctly.
