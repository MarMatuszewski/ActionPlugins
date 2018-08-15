using ActionPlugins.Plugins;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Tests.Plugins
{
    [TestFixture]
    public class SumPluginTests
    {
        private SumPlugin SUT;

        public SumPluginTests()
        {
            SUT = new SumPlugin();
        }

        [TestCase( "", 0 )]
        [TestCase( "sad678asdhc11", 23 )]
        [TestCase( "12345", 15 )]
        [TestCase( "asdacr", 0 )]
        [TestCase( "9d8d7e6d7scc", 37 )]
        public void Should_Return_String_With_Expected_Sum_Of_Numbers_In_Provided_Input( string input, int expectedSum )
        {
            var result = SUT.Execute( input );

            var returnString = $"Sum of all numbers in input: {expectedSum}";

            Assert.AreEqual( returnString, result );
        }

        [Test]
        public void Desricption_Property_Should_Display_Proper_Information()
        {
            var expectedDescription = "This plugin will parse all numbers in provided input and then sum them up";

            Assert.AreEqual( expectedDescription, SUT.Description );
        }
    }
}
