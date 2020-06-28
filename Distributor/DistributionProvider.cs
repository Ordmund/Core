using System;

namespace Core.Managers
{
    public static class DistributionProvider
    {
        private static Distributor _distributor;

        public static void Setup<TMap>() where TMap : DistributionMapBase
        {
            if (_distributor == null)
                _distributor = new Distributor();

            var distributionMap = Activator.CreateInstance<TMap>();
            
            _distributor.UpdateInstances(distributionMap);
        }

        public static Distributor GetDistributor()
        {
            if (_distributor == null)
                throw new NotInitializedException("Distributor is not initialized! Call Setup<TMap> method first!");

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