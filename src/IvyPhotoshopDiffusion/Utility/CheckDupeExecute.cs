using System.Threading;

namespace Invary.Utility
{

	/// <summary>
	/// Check for duplicate execution
	/// </summary>
	/// 
	///<example>
	///static void Main()
	///{
	///	var check = CheckDupeExecute.StartAndCheck("TestApplication");
	///	if (check == false)
	///	{
	///		MessageBox.Show("already executed!", "dupe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	///		return;
	///	}
	///
	///	try
	///	{
	///		Application.EnableVisualStyles();
	///		Application.Run(new Form1());
	///	}
	///	catch
	///	{
	///	}
	///	finally
	///	{
	///	}
	///
	///	CheckDupeExecute.Release();
	///}
	///</example>
	///
	internal class CheckDupeExecute
	{
		static readonly object _locker = new object();
		static Mutex _mutex = null;


		/// <summary>
		/// Check for duplicate execution
		/// 
		/// all user:    @"Global\TestApplication"
		/// single user: @"TestApplication"
		/// </summary>
		public static bool StartAndCheck(string strMutexName)
		{
			lock (_locker)
			{
				if (_mutex != null)
					return true;

				bool bCreatedNew;

				var tmp = new Mutex(true, strMutexName, out bCreatedNew);
				if (bCreatedNew == false)
				{
					tmp.Close();
					tmp.Dispose();
					return false;
				}
				_mutex = tmp;
				return true;
			}
		}


		public static void Release()
		{
			lock (_locker)
			{
				_mutex.ReleaseMutex();
				_mutex.Close();
				_mutex.Dispose();
				_mutex = null;
			}
		}




	}
}
