using System;

namespace Core.Managers
{
    public class DistributorContext
    {
        private static Distributor _distributor;
        
        public void Setup<TMap>() where TMap : DistributionMapBase
        {
            if(_distributor == null)
                _distributor = new Distributor();

            var distributionMap = Activator.CreateInstance<TMap>();
            var map = distributionMap.GetMap();

            _distributor.UpdateInstances(map);
        }

        public Distributor GetDistributor()
        {
            if (_distributor == null)
                throw new NotInitializedException("Distributor is not initialized! Call Setup<Map> method first!");
            
            return _distributor;
        }

        private class NotInitializedException : Exception
        {
            public NotInitializedException(string message) : base(message)
            {
            }
        }
    }
}