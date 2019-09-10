using ColoredLogger;

namespace Core.Managers.Test
{
    public class Manager2 : I2
    {
        private I1 _mananager;
        
        public int InitializationGeneration => 2;

        public void Initialize(Distributor distributor)
        {
            _mananager = distributor.Get<I1>();
        }

        public void Restart()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}