using ActionPlugins.CLI;
using ActionPlugins.Services;
using CorePlugin;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ActionPlugins.Tests.CLI
{
    [TestFixture]
    public class InteractionsTests
    {
        private Interactions SUT;
        private IReadOnlyCollection<IPlugin> _mockPlugins;
        private IPlugin _mockPluginOne;
        private IPlugin _mockPluginTwo;
        private IConsoleService _mockConsoleService;

        public InteractionsTests()
        {
            _mockPluginOne = Substitute.For<IPlugin>();
            _mockPluginTwo = Substitute.For<IPlugin>();
            _mockConsoleService = Substitute.For<IConsoleService>();

            _mockPlugins = new[] { _mockPluginOne, _mockPluginTwo };

            SUT = new Interactions(
                _mockPlugins,
                _mockConsoleService );
        }

        [Test]
        public void On_Start_Welcome_Prompt_Should_Be_Displayed()
        {
            var expectedWelcomePrompt = "Welcome in my plugginable application." +
                            "This is list of all Plugins that are available to use: \n";

            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exit" );
            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( expectedWelcomePrompt ) );
        }

        [Test]
        public void On_Start_First_Plugin_Should_Be_Displayed_With_1()
        {
            var firstPlugin = $"1) {_mockPluginOne.GetType().Name}";

            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exit" );
            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( firstPlugin ) );
        }

        [Test]
        public void On_Start_Second_Plugin_Should_Be_Displayed_With_2()
        {
            var secondPlugin = $"2) {_mockPluginTwo.GetType().Name}";

            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exit" );
            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( secondPlugin ) );
        }

        [Test]
        public void On_Start_Instructions_Should_Be_Displayed()
        {
            var instructions = "\nTo display desriptions please type - \"desc\"" +
                    "\nTo execute any plugin please type - \"exe\"" +
                    "\nTo exit from program please type - \"exit\"";

            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exit" );
            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( instructions ) );
        }

        [Test]
        public void On_Start_ActionToPerform_Prompt_Should_Be_Displayed()
        {
            var actionToPerform = "Action to perform: ";

            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exit" );
            SUT.Start();

            _mockConsoleService.Received( 1 ).Write( Arg.Is( actionToPerform ) );
        }

        [Test]
        public void All_Plugins_With_Descriptions_Should_Be_Displayed_When_User_Will_Type_desc()
        {
            var pluginOneNameWithDescription = $"{_mockPluginOne.GetType().Name} - {_mockPluginOne.Description}";
            var pluginTwoNameWithDescription = $"{_mockPluginTwo.GetType().Name} - {_mockPluginTwo.Description}";
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "desc", "exit" );
            _mockPluginOne.Description.Returns( "MockPluginOne" );
            _mockPluginOne.Description.Returns( "MockPluginTwo" );

            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( pluginOneNameWithDescription ) );
            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( pluginTwoNameWithDescription ) );

        }

        [Test]
        public void Available_Plugins_Prompt_Should_Be_Displayed_When_User_Will_Type_exe()
        {
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exe", "exit" );
            SUT.Start();

            _mockConsoleService.Received( 1 ).Write( Arg.Is( "Available plugins: " ) );
        }

        [Test]
        public void All_Available_Plugins_Should_Be_Displayed_After_AvailablePlugins_Prompt()
        {
            var availablePlugins = $"{_mockPluginOne.GetType().Name} {_mockPluginTwo.GetType().Name}";
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exe", "exit" );
            SUT.Start();

            foreach( var plugin in _mockPlugins )
            {
                _mockConsoleService.Received( 2 ).Write( Arg.Is( $"{plugin.GetType().Name} " ) );
            }
        }

        [Test]
        public void Type_Plugin_Name_Prompt_Should_Be_Displayed_After_List_Of_Available_Plugins()
        {
            var typePluginName = "\nPlease type plugin name to execute: ";
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exe", "exit" );

            SUT.Start();

            _mockConsoleService.Received( 1 ).Write( Arg.Is( typePluginName ) );
        }

        [Test]
        public void Type_Input_Value_Prompt_Should_Be_Displayed_When_User_Will_Enter_Correct_Name()
        {
            var typeInputValue = "Please type input value: ";
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exe", "IPluginProxy", "exit" );

            SUT.Start();

            _mockConsoleService.Received( 1 ).Write( Arg.Is( typeInputValue ) );
        }

        [Test]
        public void Plugin_Should_Be_Executed_When_User_Will_Enter_Input_Value()
        {
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exe", "IPluginProxy", "123", "exit" );

            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( _mockPluginTwo.Execute( Arg.Any<string>() ) ) );
        }

        [Test]
        public void Sorry_No_Such_Plugin_Prompt_Should_Be_Displayed_When_User_Will_Enter_Incorrect_Name()
        {
            var noSuchPlugin = "Sorry, but there is not such plugin";
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "exe", "IPlugin", "exit" );

            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( noSuchPlugin ) );
        }

        [Test]
        public void Sorry_No_Such_Command_Prompt_Should_Be_Displayed_When_User_Will_Enter_Incorrect_Command()
        {
            var wrongCommand = "Sorry, but there us not such command - please select from \"desc\", \"exe\" or \"exit\"";
            _mockConsoleService.ClearReceivedCalls();
            _mockConsoleService.ReadLine().Returns( "run", "exit" );

            SUT.Start();

            _mockConsoleService.Received( 1 ).WriteLine( Arg.Is( wrongCommand ) );
        }
    }
}
