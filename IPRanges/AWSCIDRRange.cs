using Newtonsoft.Json;

namespace TruffleIPRanges
{
	/// <summary>
	/// Describes the data format of a AWS IP Prefix
	/// </summary>
	public class AWSCIDRRange
	{
		/*
		  {
			"ip_prefix": "3.5.140.0/22",
			"region": "ap-northeast-2",
			"service": "AMAZON",
			"network_border_group": "ap-northeast-2"
		  }
		 */

		/// <summary>
		/// Gets or sets the cidr range
		/// </summary>
		[JsonProperty("ip_prefix")]
		public string CIDRRange { get; set; }

		/// <summary>
		/// Gets or sets the service
		/// </summary>
		[JsonProperty("service")]
		public string Service { get; set; }
	}
}