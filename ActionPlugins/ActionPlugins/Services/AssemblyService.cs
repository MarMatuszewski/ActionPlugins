using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ActionPlugins.Services
{
    public class AssemblyService : IAssemblyService
    {
        public Assembly GetEntryAssembly()
        {
            return Assembly.GetEntryAssembly();
        }
    }
}
