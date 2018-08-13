using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlugin
{
    public interface IPlugin
    {
        string Execute( string input );
        string Description { get; }
    }
}
