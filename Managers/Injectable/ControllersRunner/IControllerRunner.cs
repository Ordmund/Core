namespace Core.Managers.Injectable
{
    public interface IControllerRunner
    {
        TControllerType CreateAndRun<TControllerType>() where TControllerType : IController, new();
    }
}