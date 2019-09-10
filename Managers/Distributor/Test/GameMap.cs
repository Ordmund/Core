namespace Core.Managers.Test
{
    public class GameMap : DistributionMapBase
    {
        protected override void Map()
        {
            Add<I1, Mananger1>();
            Add<I2, Manager2>();
        }
    }
}