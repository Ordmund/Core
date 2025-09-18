using System;
using System.Threading.Tasks;

namespace Core.Tasks
{
    public abstract class AsyncTask
    {
        private event Action OnCompleted;
        private event Action OnCanceled;
        private event Action OnFaulted;
        
        public abstract Task Execute();

        public AsyncTask OnComplete(Action action)
        {
            OnCompleted += action;

            return this;
        }
        
        public AsyncTask OnCancel(Action action)
        {
            OnCanceled += action;

            return this;
        }
        
        public AsyncTask OnFault(Action action)
        {
            OnFaulted += action;

            return this;
        }

        public void InvokeOnCompleted()
        {
            OnCompleted?.Invoke();
        }
        
        public void InvokeOnCanceled()
        {
            OnCanceled?.Invoke();
        }
        
        public void InvokeOnFaulted()
        {
            OnFaulted?.Invoke();
        }
    }
    
    public abstract class AsyncTask<T>
    {
        private event Action OnCompleted;
        private event Action OnCanceled;
        private event Action OnFaulted;
        
        public abstract Task<T> Execute();

        public AsyncTask<T> OnComplete(Action action)
        {
            OnCompleted += action;

            return this;
        }
        
        public AsyncTask<T> OnCancel(Action action)
        {
            OnCanceled += action;

            return this;
        }
        
        public AsyncTask<T> OnFault(Action action)
        {
            OnFaulted += action;

            return this;
        }

        public void InvokeOnCompleted()
        {
            OnCompleted?.Invoke();
        }
        
        public void InvokeOnCanceled()
        {
            OnCanceled?.Invoke();
        }
        
        public void InvokeOnFaulted()
        {
            OnFaulted?.Invoke();
        }
    }
}