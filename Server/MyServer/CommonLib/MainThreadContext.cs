using System;
using System.Collections.Concurrent;
using System.Threading;

namespace CommonLib
{
	public class MainThreadContext : SynchronizationContext
	{
		public static MainThreadContext Instance { get; } = new MainThreadContext();

		private readonly int mainThreadId = Thread.CurrentThread.ManagedThreadId;

		// 线程同步队列,发送接收socket回调都放到该队列,由poll线程统一执行
		private readonly ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();

		private Action a;

		public void Update()
		{
			while (true)
			{
				if (!this.queue.TryDequeue(out a))
				{
					return;
				}
				a();
			}
		}

		public override void Post(SendOrPostCallback callback, object state=null)
		{
			if (Thread.CurrentThread.ManagedThreadId == this.mainThreadId)
			{
				try
				{
					callback(state);
				}
				catch (Exception ex)
				{
					Logger.LogError(ex);
				}
				return;
			}
			
			this.queue.Enqueue(() => {
                try
                {
                    callback(state);
                }
                catch(Exception ex)
                {
                    Logger.LogError(ex);
                }
            });
		}
	}
}
