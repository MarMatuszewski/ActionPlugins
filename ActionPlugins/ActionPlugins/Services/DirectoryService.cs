using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ActionPlugins.Services
{
    public class DirectoryService : IDirectoryService
    {
        public bool Exists( string path )
        {
            return Directory.Exists( path );
        }

        public void CreateDirectory( string path )
        {
            Directory.CreateDirectory( path );
        }
    }
}
