using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Services
{
    public interface IConsoleService
    {
        void WriteLine( string message );

        void Write( string message );

        string ReadLine();
    }
}
