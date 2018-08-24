using ActionPlugins.Services;
using CorePlugin;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActionPlugins.StartUp
{
    public class LoadOtherPlugins
    {
        private readonly IAssemblyService _assemblyService;
        private readonly IPathService _pathService;
        private readonly IDirectoryService _directoryService;

        public LoadOtherPlugins(
            IAssemblyService assemblyService,
            IPathService pathService,
            IDirectoryService directoryService )
        {
            _assemblyService = assemblyService;
            _pathService = pathService;
            _directoryService = directoryService;
        }

        public IKernel Load( IKernel kernel )
        {
            var executableLocation = _assemblyService.GetEntryAssembly().Location;
            var additionalPluginsPath = _pathService.Combine( _pathService.GetDirectoryName( executableLocation ), "Plugins" );

            if( !_directoryService.Exists( additionalPluginsPath ) )
            {
                _directoryService.CreateDirectory( additionalPluginsPath );
            }

            kernel.Bind( x => x
                            .FromAssembliesInPath( additionalPluginsPath )
                            .SelectAllClasses()
                            .InheritedFrom<IPlugin>()
                            .BindDefaultInterfaces()
                            .Configure( y => y.InTransientScope() ) );

            return kernel;
        }
    }
}
