using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ActionPlugins.Services
{
    public class PathService : IPathService
    {
        public string Combine( string pathOne, string pathTwo )
        {
            return Path.Combine( pathOne, pathTwo );
        }

        public string GetDirectoryName( string path )
        {
            return Path.GetDirectoryName( path );
        }
    }
}
