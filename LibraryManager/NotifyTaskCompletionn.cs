using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LibraryManager
{
    /// <summary>
    /// Creates a Data-Binding wrapper for Task<TResult>
    /// </summary>
    /// <typeparam name="TResult">The type of the result of the operation</typeparam>
    /// <remarks>
    /// Credit goes to Stephen Cleary from his blog post, "Async Programming : Patterns for Asynchronous MVVM Applications: Data Binding"
    /// </remarks>
    public sealed class NotifyTaskCompletion<TResult> : NotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }
        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch
            {
            }
            if (!HasHandlers()) return;
            ForcePropertyChanged("Status");
            ForcePropertyChanged("IsCompleted");
            ForcePropertyChanged("IsNotCompleted");
            if (task.IsCanceled)
            {
                ForcePropertyChanged("IsCanceled");
            }
            else if (task.IsFaulted)
            {
                ForcePropertyChanged("IsFaulted");
                ForcePropertyChanged("Exception");
                ForcePropertyChanged("InnerException");
                ForcePropertyChanged("ErrorMessage");
            }
            else
            {
                ForcePropertyChanged("IsSuccessfullyCompleted");
                ForcePropertyChanged("Result");
            }
        }
        public Task<TResult> Task { get; private set; }
        public TResult Result
        {
            get
            {
                return (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);
            }
        }
        public TaskStatus Status { get { return Task.Status; } }
        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsNotCompleted { get { return !Task.IsCompleted; } }
        public bool IsSuccessfullyCompleted
        {
            get
            {
                return Task.Status == TaskStatus.RanToCompletion;
            }
        }
        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }
        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException
        {
            get
            {
                return Exception?.InnerException;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return InnerException?.Message;
            }
        }
    }
}