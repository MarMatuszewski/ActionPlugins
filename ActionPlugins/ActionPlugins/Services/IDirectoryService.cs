using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.Services
{
    public interface IDirectoryService
    {
        bool Exists( string path );

        void CreateDirectory( string path );
    }
}
