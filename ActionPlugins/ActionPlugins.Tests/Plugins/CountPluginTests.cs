using ActionPlugins.Plugins;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Tests.Plugins
{
    [TestFixture]
    public class CountPluginTests
    {
        private CountPlugin SUT;

        [SetUp]
        public void SetUp()
        {
            SUT = new CountPlugin();
        }

        [TestCase( "", 0 )]
        [TestCase( "sad678asdh1", 11 )]
        [TestCase( "1234567", 7 )]
        [TestCase( "asdacrgrt", 9 )]
        [TestCase( "9d8", 3 )]
        public void Should_Return_String_With_Expected_Number_Of_Characters_In_Provided_Input( string input, int expectedNumber )
        {
            var result = SUT.Execute( input );

            var returnString = $"Input length: {expectedNumber}";

            Assert.AreEqual( returnString, result );
        }

        [Test]
        public void Desricption_Property_Should_Display_Proper_Information()
        {
            var expectedDescription = "This plugin will count all characters in provided input";

            Assert.AreEqual( expectedDescription, SUT.Description );
        }
    }
}
