using ActionPlugins.Plugins;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Tests.Plugins
{
    [TestFixture]
    public class ReversePluginTests
    {
        private ReversePlugin SUT;

        public ReversePluginTests()
        {
            SUT = new ReversePlugin();
        }

        [TestCase( "radAr", "rAdar" )]
        [TestCase( "Marcin", "nicraM" )]
        [TestCase( "1234567", "7654321" )]
        [TestCase( "tattarrattat", "tattarrattat" )]
        [TestCase( "9D8ee", "ee8D9" )]
        public void Should_Return_String_With_Reversed_Provided_Input( string input, string expectedReversedInput )
        {
            var result = SUT.Execute( input );

            var returnString = $"Reversed input: {expectedReversedInput}";

            Assert.AreEqual( returnString, result );
        }

        [Test]
        public void Desricption_Property_Should_Display_Proper_Information()
        {
            var expectedDescription = "This plugin will reverse provided input";

            Assert.AreEqual( expectedDescription, SUT.Description );
        }
    }
}
