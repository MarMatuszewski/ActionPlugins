using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Services
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine( string message )
        {
            Console.WriteLine( message );
        }

        public void Write( string message )
        {
            Console.Write( message );
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
