using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TruffleIPRanges
{
	/// <summary>
	/// An implementation of <see cref="ICIDRChecker"/> for AWS IP Ranges
	/// </summary>
	public class AWSCIDRChecker : ICIDRChecker
	{
		// Statically create this client so it is only created once for this package to prevent thread exhaustion
		private static readonly HttpClient RAW_CLIENT = new HttpClient();
		private static AWSCIDRRange[] remoteDataRanges;
		private static DateTime lastRemoteLoadDate;

		/// <summary>
		/// Initializes a new instance of the <see cref="AWSCIDRChecker"/>
		/// </summary>
		/// <param name="allowRemoteLoad">Optionally you can turn off remote loading, this package will remotely load the latest aws ip range data by default</param>
		/// <param name="client">Optionally you can provide your own http client to make the request, this package will use a single static http client by default</param>
		/// <param name="remoteLoadLocation">Optionally, you can override this location to be a different source as long as the data format is compatible</param>
		public AWSCIDRChecker(bool allowRemoteLoad = true, string remoteLoadLocation = "https://ip-ranges.amazonaws.com/ip-ranges.json", HttpClient client = null)
		{
			client = client ?? RAW_CLIENT;
			remoteLoadLocation = remoteLoadLocation ?? "https://ip-ranges.amazonaws.com/ip-ranges.json";

			if (allowRemoteLoad)
			{
				if (lastRemoteLoadDate.AddMinutes(1) < DateTime.UtcNow)
				{
					var getTask = Task.Run(() => client.GetStringAsync(remoteLoadLocation));
					getTask.Wait();
					remoteDataRanges = GetData(getTask.Result).ToArray();
					lastRemoteLoadDate = DateTime.UtcNow;
				}
			}
			else
			{
				remoteDataRanges = GetData().ToArray();
			}
		}

		/// <inheritdoc />
		public bool HasExactMatch(string cidrRange, string service = null)
		{
			if (service != null)
			{
				return remoteDataRanges
					.Where(x => x.Service.Equals(service, StringComparison.OrdinalIgnoreCase))
					.Any(x => x.CIDRRange.Equals(cidrRange, StringComparison.OrdinalIgnoreCase));
			}

			return remoteDataRanges
				.Any(x => x.CIDRRange.Equals(cidrRange, StringComparison.OrdinalIgnoreCase));
		}

		private static IEnumerable<AWSCIDRRange> GetData()
		{
			return GetData(DataHelper.Load("aws-ip-ranges.json"));
		}

		private static IEnumerable<AWSCIDRRange> GetData(string raw)
		{
			var data = JObject.Parse(raw);
			foreach (var value in data.Value<JArray>("prefixes").Values<JObject>())
			{
				yield return value.ToObject<AWSCIDRRange>();
			}
		}
	}
}
