using ColoredLogger;

namespace Core.Managers.Test
{
    public class Mananger1 : I1
    {
        private I2 _mananager;

        public int InitializationGeneration => 1;


        public void Initialize(Distributor distributor)
        {
            _mananager = distributor.Get<I2>();
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