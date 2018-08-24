using ActionPlugins.StartUp;
using CorePlugin;
using Ninject;
using NUnit.Framework;
using System.Linq;
using ActionPlugins.Services;

namespace ActionPlugins.Tests.Startup
{
    [TestFixture]
    public class BootstrapperTests
    {
        private IKernel _testKernel;
        private Bootstrapper SUT;

        [SetUp]
        public void Setup()
        {
            _testKernel = new StandardKernel();

            SUT = new Bootstrapper();

            _testKernel = SUT.Start();
        }

        [Test]
        public void Three_Plugins_Should_Be_Bound()
        {
            var bindings = _testKernel.GetBindings( typeof (IPlugin) );
            Assert.That( bindings.Count(), Is.EqualTo( 3 ) );
        }

        [Test]
        public void ConsoleService_Should_Have_A_Binding()
        {
            var consoleServiceInstance = _testKernel.Get<IConsoleService>();
            Assert.IsInstanceOf<ConsoleService>( consoleServiceInstance );
        }

        [Test]
        public void ConsoleService_Should_Be_Bound_As_Singleton()
        {
            var testConsoleServiceInstanceOne = _testKernel.Get<IConsoleService>();
            var testConsoleServiceInstanceTwo = _testKernel.Get<IConsoleService>();

            Assert.AreSame( testConsoleServiceInstanceOne, testConsoleServiceInstanceTwo );
        }

        [Test]
        public void DirectoryService_Should_Have_A_Binding()
        {
            var directoryServiceInstance = _testKernel.Get<IDirectoryService>();
            Assert.IsInstanceOf<DirectoryService>( directoryServiceInstance );
        }

        [Test]
        public void DirectoryService_Should_Be_Bound_As_Singleton()
        {
            var testDirectoryServiceInstanceOne = _testKernel.Get<IDirectoryService>();
            var testDirectoryServiceInstanceTwo = _testKernel.Get<IDirectoryService>();

            Assert.AreSame( testDirectoryServiceInstanceOne, testDirectoryServiceInstanceTwo );
        }

        [Test]
        public void PathService_Should_Have_A_Binding()
        {
            var pathServiceInstance = _testKernel.Get<IPathService>();
            Assert.IsInstanceOf<PathService>( pathServiceInstance );
        }

        [Test]
        public void PathService_Should_Be_Bound_As_Singleton()
        {
            var testPathServiceInstanceOne = _testKernel.Get<IPathService>();
            var testPathServiceInstanceTwo = _testKernel.Get<IPathService>();

            Assert.AreSame( testPathServiceInstanceOne, testPathServiceInstanceTwo );
        }

        [Test]
        public void AssemblyService_Should_Have_A_Binding()
        {
            var pathServiceInstance = _testKernel.Get<IAssemblyService>();
            Assert.IsInstanceOf<AssemblyService>( pathServiceInstance );
        }

        [Test]
        public void AssemblyService_Should_Be_Bound_As_Singleton()
        {
            var testAssemblyServiceInstanceOne = _testKernel.Get<IAssemblyService>();
            var testAssemblyServiceInstanceTwo = _testKernel.Get<IAssemblyService>();

            Assert.AreSame( testAssemblyServiceInstanceOne, testAssemblyServiceInstanceTwo );
        }


        [Test]
        public void AssemblySerkvice_Should_Be_Bound_As_Singleton()
        {
            var testAssemblyServiceInstanceOne = _testKernel.Get<IAssemblyService>();
            var testAssemblyServiceInstanceTwo = _testKernel.Get<IAssemblyService>();

            Assert.AreSame( testAssemblyServiceInstanceOne, testAssemblyServiceInstanceTwo );
        }
    }

}
