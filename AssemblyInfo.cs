using System.Reflection;
using System.Runtime.InteropServices;
using Hydrogen.Application;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyTitle("BlockchainSQL.Server")]
[assembly: AssemblyDescription("BlockchainSQL Server gives you the blockchain as an SQL database. Easily build your analytics and BI platform without ever worrying about protocols again.")]
[assembly: AssemblyCopyright("Copyright © Sphere 10 Software 2008 - {CurrentYear}")]
[assembly: AssemblyProductDistribution(ProductDistribution.ReleaseCandidate)]
[assembly: AssemblyCompanyNumber("ABN 39600596316")]
[assembly: AssemblyCompany("Sphere 10 Software Pty Ltd")]
[assembly: AssemblyProduct("BlockchainSQL Server")]
[assembly: AssemblyCompanyLink("https://sphere10.com")]
[assembly: AssemblyProductCode("6F7A705B-FC8C-4AD7-908E-503335F0F78C")]
[assembly: AssemblyProductLink("https://sphere10.com/products/blockchainsql")]
[assembly: AssemblyProductPurchaseLink("https://sphere10.com/products/blockchainsql")]
[assembly: AssemblyProductSecret("86161ec29378ab8685ced76027d51e488c0ac39c7ad7192371214a703e22c29f")]

#if DEBUG
[assembly: AssemblyProductDrmApi("http://localhost:5000/api/drm")]
#else
[assembly: AssemblyProductDrmApi("https://sphere10.com/api/drm")]
#endif
[assembly: AssemblyProductLicense(
	"""
	{
	  "authority": {
	    "name": "Sphere 10 Software General Software Products",
	    "dss": "ecdsa-secp256k1",
	    "publicKey": "A0xL8HSZ7Cl9IYUx92/e34NPhYZHkQEaWcyU2BuJx/2T"
	  },
	  "license": {
	    "Item": {
	      "name": "BlockchainSQL Server v1 Trial",
	      "productKey": "0000-0000-0000-0004",
	      "productCode": "6f7a705b-fc8c-4ad7-908e-503335f0f78c",
	      "featureLevel": "free",
	      "expirationPolicy": "disable",
	      "majorVersionApplicable": 1,
	      "expirationDays": 90
	    },
	    "Signature": "MEQCIF7sBuxPsG+oTWlBZpc/JHkCz+8qb86DU4mxi/U5SruGAiAksP3GwGbg0s5MIzMqqkdfX2PwbsXts9zccNg1KKCJYQ=="
	  },
	  "command": {
	    "Item": {
	      "productKey": "0000-0000-0000-0004",
	      "action": "enable"
	    },
	    "Signature": "MEQCIEQE23qgIOSWuPhP/BVckJ3i5+84x3vdG3e55fsZEA94AiA1TSdPcScUdWZfzpIt2vkMcS/7/1eo2/JEkoAEh6+R7w=="
	  }
	}
	""")]


// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("AB05E2BD-F9EF-4088-A9D1-A47049A20146")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
