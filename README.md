# ip-ranges

Pulls the latest IP Ranges produced by AWS and provides a simple client on top to verify ranges for particular services.

## NuGet

Install: `Install-Package TruffleIPRanges`

## Usage

The package is designed to support any NET Standard setup.

Recommendation for NET Core is:

```
		using IPRanges;

		...

		/// <inheritdoc />
		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<ICIDRChecker, AWSCIDRChecker>();
		}


```
This way when the class is initiated the latest version of the IP Range data from AWS will automatically be loaded.