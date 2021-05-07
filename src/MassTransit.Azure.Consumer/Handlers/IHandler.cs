using System.Threading.Tasks;

namespace MassTransit.Azure.Consumer.Handlers
{
    // 扩展自 IConsumer 接口
    public interface IHandler<in T> : IConsumer<T>, IConsumer<Fault<T>>
        where T : class
    { }
}