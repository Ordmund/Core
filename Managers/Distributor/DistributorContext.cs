namespace Core.Managers
{
    public static class DistributorContext
    {
        private static Distributor _distributor;
        
        public static void Setup(DistributionMapBase map)
        {
            if(_distributor == null)
                _distributor = new Distributor();
            
            _distributor.Initialize(map);
        }
    }
}