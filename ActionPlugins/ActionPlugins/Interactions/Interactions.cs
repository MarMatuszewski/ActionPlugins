using ActionPlugins.Services;
using CorePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActionPlugins.Interactions
{
    public class Interactions : IInteractions
    {
        private readonly IEnumerable<IPlugin> _plugins;
        private readonly IConsoleService _consoleService;

        public Interactions( IEnumerable<IPlugin> plugins,
            IConsoleService consoleService )
        {
            _plugins = plugins;
            _consoleService = consoleService;
        }

        public void Start()
        {
            _consoleService.WriteLine( "Welcome in my plugginable application." +
                            "This is list of all Plugins that are available to use: \n" );

            listAllPlugins();

            var execute = true;

            while( execute )
            {
                _consoleService.WriteLine( "\nTo display desriptions please type - \"desc\"" +
                    "\nTo execute any plugin please type - \"exe\"" +
                    "\nTo exit from program please type - \"exit\"" );

                _consoleService.Write( "Action to perform: " );

                var interaction = _consoleService.ReadLine();

                if( interaction.Equals( "desc" ) )
                {
                    pluginsWithDesriptions();
                }
                else if( interaction.Equals( "exe" ) )
                {
                    _consoleService.Write( "Available plugins: " );
                    foreach( var plugin in _plugins )
                    {
                        _consoleService.Write( $"{plugin.GetType().Name} " );
                    }
                    _consoleService.Write( "\nPlease type plugin name to execute: " );

                    var name = _consoleService.ReadLine();

                    var pluginToExecute = _plugins.FirstOrDefault( p => p.GetType().Name.Equals( name ) );

                    if( pluginToExecute != null )
                    {
                        _consoleService.Write( "Please type input value: " );

                        var input = _consoleService.ReadLine();

                        _consoleService.WriteLine( pluginToExecute.Execute( input ) );
                    }
                    else
                    {
                        _consoleService.WriteLine( "Sorry, but there is not such plugin" );
                    }
                }
                else if( interaction.Equals( "exit" ) )
                {
                    execute = false;
                }
                else
                {
                    _consoleService.WriteLine( "Sorry, but there us not such command - please select from \"desc\", \"exe\" or \"exit\"" );
                }
            }
        }

        private void listAllPlugins()
        {
            var counter = 1;

            foreach( var plugin in _plugins )
            {
                _consoleService.WriteLine( $"{counter++}) {plugin.GetType().Name}" );
            }
        }

        private void pluginsWithDesriptions()
        {
            foreach( var plugin in _plugins )
            {
                _consoleService.WriteLine( $"{plugin.GetType().Name} - {plugin.Description}" );
            }
        }
    }
}
