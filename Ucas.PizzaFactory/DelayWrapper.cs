using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public class DelayWrapper : IDelayWrapper
    {
        public async Task Delay(int millisecondsToDelay)
        {
            await Task.Delay(millisecondsToDelay);
        }
    }
}
