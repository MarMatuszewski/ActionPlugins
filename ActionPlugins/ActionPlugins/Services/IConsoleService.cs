
namespace ActionPlugins.Services
{
    public interface IConsoleService
    {
        void WriteLine( string message );

        void Write( string message );

        string ReadLine();
    }
}
