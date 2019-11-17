
namespace Api
{
    public class A : IA
    {
        public string Name => "A";
    }

    public interface IA
    {
        string Name { get; }
    }
}