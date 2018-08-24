using ActionPlugins.Services;
using CorePlugin;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Linq;


namespace ActionPlugins.StartUp
{
    public class Bootstrapper
    {
        private IKernel _kernel;
        private IDirectoryService _directoryService;
        private IPathService _pathService;
        private IAssemblyService _assemblyService;

        public IKernel Start()
        {
            _kernel = new StandardKernel();

            bindAllCorePlugins();
            bindServices();
           // bindAdditionalPlugins();


            return _kernel;
        }

        private void bindAllCorePlugins()
        {
            _kernel.Bind( x => x
                            .FromThisAssembly()
                            .SelectAllClasses()
                            .InheritedFrom<IPlugin>()
                            .BindDefaultInterfaces()
                            .Configure( y => y.InSingletonScope() ) );
        }

        private void bindAdditionalPlugins()
        {
            _assemblyService = _kernel.Get<IAssemblyService>();
            _directoryService = _kernel.Get<IDirectoryService>();
            _pathService = _kernel.Get<IPathService>();

            var executableLocation = _assemblyService.GetEntryAssembly().Location;
            var additionalPluginsPath = _pathService.Combine( _pathService.GetDirectoryName( executableLocation ), "Plugins" );

            if( !_directoryService.Exists( additionalPluginsPath ) )
            {
                _directoryService.CreateDirectory( additionalPluginsPath );
            }

            _kernel.Bind( x => x
                            .FromAssembliesInPath( additionalPluginsPath )
                            .SelectAllClasses()
                            .InheritedFrom<IPlugin>()
                            .BindDefaultInterfaces()
                            .Configure( y => y.InTransientScope() ) );
        }

        private void bindServices()
        {
            _kernel.Bind( x => x.FromThisAssembly()
                            .SelectAllClasses()
                            .Where( classTypeHasNotAlreadyBeenBound )
                            .BindAllInterfaces()
                            .Configure( y => y.InSingletonScope() ) );
        }

        private bool classTypeHasNotAlreadyBeenBound( Type classType )
        {
            var classTypesInterfaces = classType.GetInterfaces();
            return !classTypesInterfaces.All( interfaceTypeHasBinding );
        }

        private bool interfaceTypeHasBinding( Type interfaceType )
        {
            return _kernel != null && _kernel.GetBindings( interfaceType ).Any();
        }
    }
}
