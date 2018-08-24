using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ActionPlugins.Services
{
    public interface IAssemblyService
    {
        Assembly GetEntryAssembly();
    }
}
