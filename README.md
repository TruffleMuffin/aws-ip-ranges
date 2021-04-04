# ip-ranges

Pulls the latest IP Ranges produced by AWS and provides a simple client on top to verify ranges for particular services.

## NuGet

Install: `Install-Package TruffleIPRanges`

## Usage

The package is designed to support any NET Standard setup.

Recommendation for NET Core is:

```
using TruffleIPRanges;

...

/// <inheritdoc />
public override void ConfigureServices(IServiceCollection services)
{
	services.AddTransient<ICIDRChecker, AWSCIDRChecker>();
}

```
This way when the class is initiated the latest version of the IP Range data from AWS will automatically be loaded.

There is an internal throttle of 1 call per minute for the remote load on https://ip-ranges.amazonaws.com/ip-ranges.json. This is designed to prevent high concurrency/bad configuration causing large amounts of http traffic.