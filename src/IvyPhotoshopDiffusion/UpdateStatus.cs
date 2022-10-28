using Invary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Invary.IvyPhotoshopDiffusion
{
	public class UpdateEventArgs : EventArgs
	{
		public bool Success { set; get; } = false;
		public bool IsNewVersiionExists { set; get; } = false;
		public SoftInfo SoftwareInfo { set; get; } = null;
	}



	public class UpdateStatus
	{
		public List<SoftInfo> listSoftware { set; get; } = new List<SoftInfo>();
		public DonationInfo Donation { set; get; } = new DonationInfo();



		public static UpdateStatus FromJson(string json)
		{
			try
			{
				return JsonSerializer.Deserialize<UpdateStatus>(json);
			}
			catch (Exception)
			{
			}
			return null;
		}

		public string ToJson()
		{
			try
			{
				return JsonSerializer.Serialize(this);
			}
			catch (Exception)
			{
			}
			return "";
		}


		public static void CheckUpdate(EventHandler<UpdateEventArgs> OnCheckCompleted, double dTimeoutSec, CancellationToken ct)
		{
			//Task.Factory.StartNew(async () =>
			TaskManager.StartSTATask(async () =>
			{
				UpdateEventArgs args = new UpdateEventArgs();
				args.Success = false;
				args.IsNewVersiionExists = false;

				try
				{
					var json = await Uty.DownloadTextAsync(XmlSetting.UpdateCheckUrl, dTimeoutSec, ct);
					if (string.IsNullOrEmpty(json))
						return;

					var stat = UpdateStatus.FromJson(json);
					if (stat == null)
						return;

					var query = stat.listSoftware.Where(x => x.strGuid == XmlSetting.ProductGuid);
					if (query == null || query.Count() == 0)
						return;

					var item = query.First();
					if (item == null)
						return;

					args.Success = true;
					args.SoftwareInfo = item;
					if (item.nVer <= XmlSetting.nVersion)
					{
						//curren version is latest.
						return;
					}

					//current version is old. need to update
					args.IsNewVersiionExists = true;
				}
				catch (Exception)
				{
				}
				finally
				{
					OnCheckCompleted?.Invoke(null, args);
				}
			});

		}
	}




	public class SoftInfo
	{
		public string strGuid { set; get; } = "";
		public string strName { set; get; } = "";
		public string strVer { set; get; } = "";
		public int nVer { set; get; } = 100;

		public DateTime dtUTC { set; get; } = DateTime.MinValue;

		[JsonIgnore]
		public DateTime dtDate
		{
			set
			{
				dtUTC = value.ToUniversalTime();
			}
			get
			{
				return dtUTC.ToLocalTime();
			}
		}
	}


	public class DonationInfo
	{
		public decimal dUsd { set; get; } = 0;

		public int nCount { set; get; } = 0;

		public DateTime dtUTC { set; get; } = DateTime.MinValue;


		[JsonIgnore]
		public DateTime dtDate
		{
			set
			{
				dtUTC = value.ToUniversalTime();
			}
			get
			{
				return dtUTC.ToLocalTime();
			}
		}
	}


}
