using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooEventManager
    {
        Task ProcessFooEventAsync(IFooGooEvent item);
    }
}