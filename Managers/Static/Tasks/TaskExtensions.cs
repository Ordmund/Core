using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Managers
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Runs the specified task and handles any exceptions that occur during the process
        /// </summary>
        /// <param name="task">A task that needs to be started from synchronous code</param>
        public static async void Forget(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception error)
            {
                Debug.LogError(error.Message);
            }
        }

        /// <summary>
        /// Runs the specified task and handles any exceptions that occur during the process
        /// </summary>
        /// <param name="task">A task that needs to be started from synchronous code</param>
        /// <typeparam name="T">The type of result of successful task completion</typeparam>
        public static async void Forget<T>(this Task<T> task)
        {
            try
            {
                await task;
            }
            catch (Exception error)
            {
                Debug.LogError(error.Message);
            }
        }

        /// <summary>
        /// Call the specified action if the task is completed Successfully
        /// </summary>
        /// <param name="task">A task that needs to be started from the main thread</param>
        /// <param name="onComplete">Action to call on task completed</param>
        /// <returns>Task with subscription</returns>
        /// <remarks>
        /// ⚠️ WARNING: This method must be called only from main thread, not from asynchronous code
        /// </remarks>

        public static Task OnComplete(this Task task, Action onComplete)
        {
            return task.ContinueWith(CallOnComplete, TaskScheduler.FromCurrentSynchronizationContext());

            void CallOnComplete(Task completedTask)
            {
                if(completedTask.IsCompletedSuccessfully)
                {
                    onComplete.Invoke();
                }
            }
        }
        
        /// <summary>
        /// Call the specified action if the task is canceled
        /// </summary>
        /// <param name="task">A task that needs to be started from the main thread</param>
        /// <param name="onCanceled">Action to call on task canceled</param>
        /// <returns>Task with subscription</returns>
        /// <remarks>
        /// ⚠️ WARNING: This method must be called only from main thread, not from asynchronous code
        /// </remarks>

        public static Task OnCanceled(this Task task, Action onCanceled)
        {
            return task.ContinueWith(CallOnAbort, TaskScheduler.FromCurrentSynchronizationContext());
            
            void CallOnAbort(Task cancelledTask)
            {
                if(cancelledTask.IsCanceled)
                {
                    onCanceled.Invoke();
                }
            }
        }
    }
}