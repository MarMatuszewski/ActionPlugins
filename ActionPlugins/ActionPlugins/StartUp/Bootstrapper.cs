using CorePlugin;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ActionPlugins.StartUp
{
    public class Bootstrapper
    {
        private IKernel _kernel;


        public IKernel Start()
        {
            _kernel = new StandardKernel();

            bindAllCorePlugins();
            bindAdditionalPlugins();
            bindRemainingClasses();

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
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var additionalPluginsPath = Path.Combine( Path.GetDirectoryName( executableLocation ), "Plugins" );

            if( !Directory.Exists( additionalPluginsPath ) )
            {
                Directory.CreateDirectory( additionalPluginsPath );
            }

            _kernel.Bind( x => x
                            .FromAssembliesInPath( additionalPluginsPath )
                            .SelectAllClasses()
                            .InheritedFrom<IPlugin>()
                            .BindDefaultInterfaces()
                            .Configure( y => y.InTransientScope() ) );
        }

        private void bindRemainingClasses()
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
