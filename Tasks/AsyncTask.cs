using System.Threading.Tasks;

namespace Core.Tasks
{
    public abstract class AsyncTask
    {
        public abstract Task Execute();
    }
    
    public abstract class AsyncTask<T>
    {
        public abstract Task<T> Execute();
    }
}