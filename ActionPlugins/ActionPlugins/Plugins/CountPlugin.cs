using CorePlugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Plugins
{
    public class CountPlugin : IPlugin
    {
        public string Execute( string input )
        {
            return "Output: " + input.Length.ToString();
        }

        public string Description => "This plugin will count all characters in provided input";
    }
}
