﻿using CorePlugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Plugins
{
    public class SumPlugin : IPlugin
    {
        public string Execute( string input )
        {
            var sum = 0;
            foreach( var sign in input )
            {
                if( int.TryParse( sign.ToString(), out int value ) )
                {
                    sum += value;
                }
            }

            return "Output: " + sum.ToString();
        }

        public string Description => "This plugin will parse all numbers in provided input and then sum them up";
    }
}