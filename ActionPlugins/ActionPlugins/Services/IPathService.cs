using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Services
{
    public interface IPathService
    {
        string Combine( string pathOne, string pathTwo );

        string GetDirectoryName( string path );
    }
}
