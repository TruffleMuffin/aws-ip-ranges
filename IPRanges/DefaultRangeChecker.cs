using System;

namespace AWSIPRanges
{
    public class DefaultRangeChecker
    {
    }

	/// <summary>
	/// Describes a CIDR IP Range checker
	/// </summary>
	public interface ICIDRChecker
	{
		/// <summary>
		/// Iterates the CIDR ranges from the aws ip range data. Returns true if one of the ranges is an exact match to the input.
		/// </summary>
		/// <param name="cidrRange">The CIDR range to check against the aws ip data</param>
		/// <param name="service">An optional service name to limit the scope of the check. By default all services are checked</param>
		/// <returns>True if the range is in the aws ip data</returns>
		bool HasExactMatch(string cidrRange, string service = null);
	}
}
