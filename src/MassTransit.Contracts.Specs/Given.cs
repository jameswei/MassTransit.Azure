using System;

namespace MassTransit.Contracts.Specs
{
    public class Given
    {
        // Action 是无参数无返回的 delegate 类型，作为“方法指针”，执行传进来的 method
        public void And(string given, Action action)
        {
            Console.WriteLine("Given: {0}", given);
            action();
        }
    }
}