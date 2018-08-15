using ActionPlugins.CLI;
using CorePlugin;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ActionPlugins
{
    class Program
    {
        private static IKernel _kernel;

        static void Main( string[] args )
        {
            _kernel = new StandardKernel();

            bindAllCorePlugins();
            bindAdditionalPlugins();
            bindRemainingClasses();

            var interactions = _kernel.Get<IInteractions>();
            interactions.Start();
        }

        private static void bindAllCorePlugins()
        {
            _kernel.Bind( x => x
                            .FromThisAssembly()
                            .SelectAllClasses()
                            .InheritedFrom<IPlugin>()
                            .BindDefaultInterfaces()
                            .Configure( y => y.InSingletonScope() ) );
        }

        private static void bindAdditionalPlugins()
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

        private static void bindRemainingClasses()
        {
            _kernel.Bind( x => x.FromThisAssembly()
                            .SelectAllClasses()
                            .Where( classTypeHasNotAlreadyBeenBound )
                            .BindAllInterfaces()
                            .Configure( y => y.InSingletonScope() ) );
        }

        private static bool classTypeHasNotAlreadyBeenBound( Type classType )
        {
            var classTypesInterfaces = classType.GetInterfaces();
            return !classTypesInterfaces.All( interfaceTypeHasBinding );
        }

        private static bool interfaceTypeHasBinding( Type interfaceType )
        {
            return _kernel != null && _kernel.GetBindings( interfaceType ).Any();
        }
    }
}
