using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Invary.Utility
{
	class ResultEventArgs : EventArgs
	{
		public bool Success { get; set; } = false;
		public object Data { get; set; } = null;

		public ResultEventArgs()
		{
		}

		public ResultEventArgs(bool result, object data)
		{
			Success = result;
			Data = data;
		}
	}



	class Uty
	{
		public static void OpenURL(string url)
		{
			ProcessStartInfo pi = new ProcessStartInfo()
			{
				FileName = url,
				UseShellExecute = true,
			};
			using (Process.Start(pi))
			{
			}
		}




		static readonly char[] _pInvalidPathChar = new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };


		/// <summary>
		/// replace invalid path char to cb
		/// </summary>
		public static string ReplaceInvalidPathChar(string path, char cb)
		{
			if (string.IsNullOrEmpty(path))
				return path;

			foreach (var item in _pInvalidPathChar)
			{
				path = path.Replace(item, cb);
			}

			return path;
		}



		public static string CreateNewGuid()
		{
			return Guid.NewGuid().ToString();
		}








		public static async Task<string> DownloadTextAsync(string url, double dTimeoutSec, CancellationToken ct)
		{
			try
			{
				using (var wc = new HttpClient())
				{
					wc.DefaultRequestHeaders.Add(
						"User-Agent",
						"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");

					//wc.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");

					if (dTimeoutSec > 0)
						wc.Timeout = TimeSpan.FromSeconds(dTimeoutSec);

					return await wc.GetStringAsync(url);
					//return await wc.GetStringAsync(url, ct);
				}
			}
			catch (Exception)
			{
			}
			return "";
		}



		public static async Task<string> SendPostAsync(string url, Dictionary<string, string> payload, EventHandler<ResultEventArgs> OnCompleted, CancellationToken ct)
		{
			bool result = false;
			string ret = null;
			try
			{
				using (HttpClient client = new HttpClient())
				using (var request = new HttpRequestMessage(HttpMethod.Post, url))
				using (var data = new FormUrlEncodedContent(payload))
				{
					//client.DefaultRequestHeaders
					client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml,text/javascript");
					client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
					client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");

					var response = await client.SendAsync(request, ct);
					if (response == null)
						return null;

					if (response.IsSuccessStatusCode)
					{
						ret = await response.Content.ReadAsStringAsync();
						//ret = await response.Content.ReadAsStringAsync(ct);
						result = true;
					}

					response.Dispose();
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
			finally
			{
				OnCompleted?.Invoke(null, new ResultEventArgs(result, ret));
			}
		}




	}
}
