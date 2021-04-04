using NUnit.Framework;

namespace IPRanges.Tests
{
	public class AWS_CIDR_Remote_Tests
	{
		private ICIDRChecker target;

		[SetUp]
		public void Setup()
		{
			target = new AWSCIDRChecker();
		}

		[TestCase("35.180.0.0/16", true)]
		[TestCase("15.180.0.0/16", false)]
		public void NonServiceSpecific_Matches(string cidrRange, bool expected)
		{
			Assert.That(target.HasExactMatch(cidrRange), Is.EqualTo(expected), $"{cidrRange} didn't match expectation");
		}

		[TestCase("35.180.0.0/16", "AMAZON", true)]
		[TestCase("35.180.0.0/16", "CLOUDFRONT", false)]
		public void ServiceSpecific_Matches(string cidrRange, string service, bool expected)
		{
			Assert.That(target.HasExactMatch(cidrRange, service), Is.EqualTo(expected), $"{cidrRange} didn't match expectation");
		}
	}
}