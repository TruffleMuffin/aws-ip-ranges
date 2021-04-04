using System;
using System.IO;
using System.Linq;

namespace TruffleIPRanges
{
	/// <summary>
	/// Internal helper for retrieving data from this package
	/// </summary>
	internal static class DataHelper
	{
		private static readonly string[] WHITELIST = { "aws-ip-ranges.json" };

		/// <summary>
		/// Loads internal data from this package
		/// </summary>
		/// <param name="name">The name of the data to load</param>
		/// <returns>The string representation of the data</returns>
		public static string Load(string name)
		{
			// Internal protection against arbitrary resource load
			if (WHITELIST.Contains(name) == false)
			{
				return null;
			}

			try
			{
				using (var stream = typeof(DataHelper).Assembly.GetManifestResourceStream($"TruffleIPRanges.Data.{name}"))
				{
					if (stream == null)
					{
						return string.Empty;
					}

					using (var reader = new StreamReader(stream))
					{
						return reader.ReadToEnd();
					}
				}
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}