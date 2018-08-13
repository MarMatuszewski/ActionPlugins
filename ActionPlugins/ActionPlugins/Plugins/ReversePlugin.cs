using CorePlugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Plugins
{
    public class ReversePlugin : IPlugin
    {
        public string Execute( string input )
        {
            var arrayToReverse = input.ToCharArray();
            Array.Reverse( arrayToReverse );

            return "Reversed input: " + new string( arrayToReverse );
        }

        public string Description => "This plugin will reverse provided input";
    }
}
