using ColoredLogger;

namespace Core.Managers.Test
{
    public class Mananger1 : I1
    {
        private I2 _mananager;

        public int InitializationGeneration => 1;
        
        public void Initialize()
        {
            1.Log();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}