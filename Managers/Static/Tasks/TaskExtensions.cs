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
        /// <param name="task">The task that needs to be started from synchronous code</param>
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
        /// <param name="task">The task that needs to be started from synchronous code</param>
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
    }
}