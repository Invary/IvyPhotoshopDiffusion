using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invary.IvyPhotoshopDiffusion
{
	internal class Automatic1111
	{




		public static JsonResponseImg2Img Send(JsonRequestImg2Img objJson)
		{
			string jsonString = JsonSerializer.Serialize(objJson);

			var url = $"{XmlSetting.Current.Automatic1111ApiUrl}/sdapi/v1/img2img";

			var request = WebRequest.Create(url);
			request.Method = "POST";

			string json = jsonString;
			byte[] byteArray = Encoding.UTF8.GetBytes(json);

			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;

			using (var reqStream = request.GetRequestStream())
			{
				reqStream.Write(byteArray, 0, byteArray.Length);

				using (var response = request.GetResponse())
				{
					Debug.WriteLine(((HttpWebResponse)response).StatusDescription);

					using (var respStream = response.GetResponseStream())
					using (var reader = new StreamReader(respStream))
					{

						string jsonresponse = reader.ReadToEnd();
						//Debug.WriteLine(data);

						return JsonSerializer.Deserialize<JsonResponseImg2Img>(jsonresponse);
					}
				}
			}
		}




		public static string Image2String(Image image)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				ms.Seek(0, SeekOrigin.Begin);

				var rawdata = new byte[ms.Length];
				ms.Read(rawdata, 0, rawdata.Length);

				return Convert.ToBase64String(rawdata);
			}
		}




		public static bool SaveBase64EncodingData(string file, string text)
		{
			var rawdata = Convert.FromBase64String(text);

			using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
			{
				fs.SetLength(0);
				fs.Write(rawdata, 0, rawdata.Length);
				return true;
			}
		}



	}









	public class JsonRequestTxt2Img
	{
		//public object sd_model { get; set; } = null;
		//public string outpath_samples { get; set; }
		//public string outpath_grids { get; set; }
		//public string prompt_for_display { get; set; }
		public string prompt { get; set; } = "";
		public string negative_prompt { get; set; } = "";
		//public string[] styles { get; set; }
		public int seed { get; set; } = -1;
		//public float subseed_strength { get; set; } = 0.0f;
		//public int subseed { get; set; } = -1;
		//public int seed_resize_from_h { get; set; }
		//public int seed_resize_from_w { get; set; }
		public string sampler_index { get; set; } = "Euler";
		public int batch_size { get; set; } = 1;
		public int n_iter { get; set; } = 1;
		public int steps { get; set; } = 20;
		public float cfg_scale { get; set; } = 7.0f;
		public int width { get; set; } = 512;
		public int height { get; set; } = 512;
		public int restore_faces { get; set; } = 0;
		public int tiling { get; set; } = 0;
		//public int do_not_save_samples { get; set; }
		//public int do_not_save_grid { get; set; }

		//public int inpainting_fill { get; set; }
		//public bool inpaint_full_res { get; set; }
		//public int inpaint_full_res_padding { get; set; }
		public int inpainting_mask_invert { get; set; } = 0;
	}

	public class JsonRequestImg2Img : JsonRequestTxt2Img
	{
		public string[] init_images { get; set; }
		public string mask { get; set; }

		public float denoising_strength { get; set; } = 0.75f;
		public int mask_blur { get; set; } = 4;
	}




	public class JsonResponseTxt2Img
	{
		public string[] images { get; set; }
		public Parameters parameters { get; set; }
		public string info { get; set; }
	}

	public class Parameters
	{
		public bool enable_hr { get; set; }
		public int denoising_strength { get; set; }
		public int firstphase_width { get; set; }
		public int firstphase_height { get; set; }
		public string prompt { get; set; }
		public object styles { get; set; }
		public int seed { get; set; }
		public int subseed { get; set; }
		public int subseed_strength { get; set; }
		public int seed_resize_from_h { get; set; }
		public int seed_resize_from_w { get; set; }
		public int batch_size { get; set; }
		public int n_iter { get; set; }
		public int steps { get; set; }
		public float cfg_scale { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public bool restore_faces { get; set; }
		public bool tiling { get; set; }
		public string negative_prompt { get; set; }
		public object eta { get; set; }
		public float s_churn { get; set; }
		public object s_tmax { get; set; }
		public float s_tmin { get; set; }
		public float s_noise { get; set; }
		public string sampler_index { get; set; }
	}





	public class JsonResponseImg2Img
	{
		public string[] images { get; set; }
		public ParametersResponseImg2Img parameters { get; set; }
		public string info { get; set; }
	}

	public class ParametersResponseImg2Img
	{
		public string[] init_images { get; set; }
		public int resize_mode { get; set; }
		public float denoising_strength { get; set; }
		public object mask { get; set; }
		public int mask_blur { get; set; }
		public int inpainting_fill { get; set; }
		public bool inpaint_full_res { get; set; }
		public int inpaint_full_res_padding { get; set; }
		public int inpainting_mask_invert { get; set; }
		public string prompt { get; set; }
		public object styles { get; set; }
		public int seed { get; set; }
		public int subseed { get; set; }
		public int subseed_strength { get; set; }
		public int seed_resize_from_h { get; set; }
		public int seed_resize_from_w { get; set; }
		public int batch_size { get; set; }
		public int n_iter { get; set; }
		public int steps { get; set; }
		public float cfg_scale { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public bool restore_faces { get; set; }
		public bool tiling { get; set; }
		public string negative_prompt { get; set; }
		public object eta { get; set; }
		public float s_churn { get; set; }
		public object s_tmax { get; set; }
		public float s_tmin { get; set; }
		public float s_noise { get; set; }
		public string sampler_index { get; set; }
	}














}
