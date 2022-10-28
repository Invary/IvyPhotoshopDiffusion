using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Invary.IvyPhotoshopDiffusion
{
	internal static class TaskManager
	{
		static List<Thread> Threads = new List<Thread>();



		// copy from
		// https://stackoverflow.com/questions/16720496/set-apartmentstate-on-a-task
		public static Task StartSTATask(Action func)
		{
			Check();

			var tcs = new TaskCompletionSource<object>();
			var thread = new Thread(() =>
			{
				try
				{
					func();
					tcs.SetResult(null);
				}
				catch (Exception e)
				{
					tcs.SetException(e);
				}
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			Threads.Add(thread);
			return tcs.Task;
		}



		/// <summary>
		/// remove thread from list, if thread finished
		/// </summary>
		static void Check()
		{
			for (int i = Threads.Count -1; i >= 0; i--)
			{
				var thread = Threads[i];
				if (thread.ThreadState == ThreadState.Running)
					continue;
				if (thread.ThreadState == ThreadState.Unstarted)
					continue;

				Threads.RemoveAt(i);
			}
		}



		public static void AbortAll()
		{
			for (int i = Threads.Count - 1; i >= 0; i--)
			{
				try
				{
					var thread = Threads[i];
					if (thread.ThreadState == ThreadState.Running || thread.ThreadState == ThreadState.Unstarted)
					{
						thread.Abort();
					}
				}
				catch(Exception)
				{
				}
			}
		}


	}
}
