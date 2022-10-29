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

	// api documents
	// request json is ducumented, but response is undocumented (2022/10/30)
	// http://127.0.0.1:7860/docs#/default/img2imgapi_sdapi_v1_img2img_post
	// https://github.com/AUTOMATIC1111/stable-diffusion-webui/wiki/API



	internal class Automatic1111
	{


		public static JsonResponseBase Send(JsonRequestBase request)
		{
			if (request.GetType() == typeof(JsonRequestImg2Img))
			{
				var reqImg2Img = request as JsonRequestImg2Img;
				return SendImg2Img(reqImg2Img);
			}

			if (request.GetType() == typeof(JsonRequestTxt2Img))
			{
				var reqTxt2Img = request as JsonRequestTxt2Img;
				return SendTxt2Img(reqTxt2Img);
			}

			return null;
		}


		public static JsonResponseTxt2Img SendTxt2Img(JsonRequestTxt2Img objJson)
		{
			string jsonString = JsonSerializer.Serialize(objJson);

			var url = $"{XmlSetting.Current.Automatic1111ApiUrl}/sdapi/v1/txt2img";

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

						return JsonSerializer.Deserialize<JsonResponseTxt2Img>(jsonresponse);
					}
				}
			}
		}


		public static JsonResponseImg2Img SendImg2Img(JsonRequestImg2Img objJson)
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




	public class JsonRequestBase
	{
		public string prompt { get; set; } = "";

		public string[] styles { get; set; }
		public float denoising_strength { get; set; } = 0.75f;

		public decimal seed { get; set; } = -1;
		public decimal subseed { get; set; } = -1;
		public float subseed_strength { get; set; } = 0.0f;
		public int seed_resize_from_h { get; set; } = -1;
		public int seed_resize_from_w { get; set; } = -1;

		public int batch_size { get; set; } = 1;
		public int n_iter { get; set; } = 1;
		public int steps { get; set; } = 20;
		public float cfg_scale { get; set; } = 7.0f;
		public int width { get; set; } = 512;
		public int height { get; set; } = 512;


		public bool restore_faces { set; get; } = false;
		public bool tiling { set; get; } = false;

		public string negative_prompt { get; set; } = "";

		public int eta { get; set; } = 0;

		public int s_churn { get; set; } = 0;
		public int s_tmax { get; set; } = 0;
		public int s_tmin { get; set; } = 0;
		public int s_noise { get; set; } = 1;

		public Override_Settings override_settings { get; set; }

		public string sampler_index { get; set; } = "Euler";

	}


	public class Override_Settings
	{
	}




	public class JsonRequestTxt2Img : JsonRequestBase
	{
		public bool enable_hr { set; get; } = false;
		public int firstphase_width { get; set; } = 0;
		public int firstphase_height { get; set; } = 0;

	}



	public class JsonRequestImg2Img : JsonRequestBase
	{
		public string[] init_images { get; set; }

		public int resize_mode { get; set; } = 0;


		public string mask { get; set; }

		public int mask_blur { get; set; } = 4;


		public int inpainting_fill { get; set; } = 0;
		public bool inpaint_full_res { get; set; } = false;
		public int inpaint_full_res_padding { get; set; } = 0;
		public int inpainting_mask_invert { get; set; } = 0;


		public bool include_init_images { get; set; } = false;
	}




	public class JsonResponseBase
	{
		public string[] images { get; set; }
		public Info info { get; set; }
	}



	public class JsonResponseTxt2Img : JsonResponseBase
	{
		public ParametersTxt2Img parameters { get; set; }
	}




	public class ParametersBase
	{
		public float denoising_strength { get; set; }

		public string prompt { get; set; }
		public object styles { get; set; }
		public decimal seed { get; set; }
		public decimal subseed { get; set; }
		public float subseed_strength { get; set; }
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
		public object override_settings { get; set; }
		public string sampler_index { get; set; }
	}



	public class ParametersTxt2Img : ParametersBase
	{
		public bool enable_hr { get; set; }
		public int firstphase_width { get; set; }
		public int firstphase_height { get; set; }
	}


	public class Info
	{
		public string prompt { get; set; }
		public string[] all_prompts { get; set; }
		public string negative_prompt { get; set; }
		public decimal seed { get; set; }
		public decimal[] all_seeds { get; set; }
		public decimal subseed { get; set; }
		public decimal[] all_subseeds { get; set; }
		public float subseed_strength { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public int sampler_index { get; set; }
		public string sampler { get; set; }
		public float cfg_scale { get; set; }
		public int steps { get; set; }
		public int batch_size { get; set; }
		public bool restore_faces { get; set; }
		public object face_restoration_model { get; set; }
		public string sd_model_hash { get; set; }
		public int seed_resize_from_w { get; set; }
		public int seed_resize_from_h { get; set; }
		public float denoising_strength { get; set; }
		public Extra_Generation_Params extra_generation_params { get; set; }
		public int index_of_first_image { get; set; }
		public string[] infotexts { get; set; }
		public object[] styles { get; set; }
		public string job_timestamp { get; set; }
		public int clip_skip { get; set; }
	}












	public class JsonResponseImg2Img : JsonResponseBase
	{
		public ParametersResponseImg2Img parameters { get; set; }
	}

	public class ParametersResponseImg2Img : ParametersBase
	{
		public object init_images { get; set; }
		public int resize_mode { get; set; }
		public object mask { get; set; }
		public int mask_blur { get; set; }
		public int inpainting_fill { get; set; }
		public bool inpaint_full_res { get; set; }
		public int inpaint_full_res_padding { get; set; }
		public int inpainting_mask_invert { get; set; }

		public bool include_init_images { get; set; }
	}




	public class Extra_Generation_Params
	{
	}








}
