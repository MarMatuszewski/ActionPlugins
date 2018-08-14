
namespace CorePlugin
{
    public interface IPlugin
    {
        string Execute( string input );
        string Description { get; }
    }
}
