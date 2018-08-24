using ActionPlugins.CLI;
using ActionPlugins.Services;
using ActionPlugins.StartUp;
using Ninject;

namespace ActionPlugins
{
    class Program
    {
        private static IKernel _kernel;

        static void Main( string[] args )
        {
            var bootstrapper = new Bootstrapper();


            _kernel = bootstrapper.Start();

            var otherPlugins = new LoadOtherPlugins(
                _kernel.Get<IAssemblyService>(),
                _kernel.Get<IPathService>(),
                _kernel.Get<IDirectoryService>() );

            _kernel = otherPlugins.Load( _kernel );

            var interactions = _kernel.Get<IInteractions>();
            interactions.Start();
        }
    }
}
