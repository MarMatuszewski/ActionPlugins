using ActionPlugins.CLI;
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

            var interactions = _kernel.Get<IInteractions>();
            interactions.Start();
        }
    }
}
