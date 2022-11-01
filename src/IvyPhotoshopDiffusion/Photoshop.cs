using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invary.IvyPhotoshopDiffusion
{

	// https://github.com/Adobe-CEP/CEP-Resources/tree/master/Documentation/Product%20specific%20Documentation/Photoshop%20Scripting


	internal class Photoshop
	{
		public static dynamic CreateInstance()
		{
			var type = Type.GetTypeFromProgID("Photoshop.Application");
			if (type == null)
				return null;
			dynamic appRef = Activator.CreateInstance(type);
			if (appRef != null)
				appRef.Visible = true;

			return appRef;
		}

		public static void SetUnit(dynamic appRef)
		{
			appRef.Preferences.RulerUnits = 1;		//PsUnits:  psPixels				(pdf p.173)
			appRef.Preferences.TypeUnits = 1;       //PsTypeUnits:  psTypePixels		(pdf p.173)
			appRef.DisplayDialogs = 3;              //PsDialogModes: psDisplayNoDialogs	(pdf p.161)
		}



		public static void Copy(dynamic appRef,  bool bMergeLayers)
		{
			appRef.ActiveDocument.Selection.Copy(bMergeLayers);
		}



		public static bool SetSelection(dynamic appRef, RectangleDouble rect)
		{
			return SetSelection(appRef, rect.X, rect.Y, rect.Width, rect.Height);
		}


		public static bool SetSelection(dynamic appRef, double x, double y, double w, double h)
		{
			var selRegion = new object[] { new object[] { x, y }, new object[] { x + w, y }, new object[] { x + w, y + h }, new object[] { x, y + h } };

			appRef.ActiveDocument.Selection.Select(selRegion);
			return true;
		}


		public static Color GetForegroundColor(dynamic appRef)
		{
			//SolidColor
			var color = appRef.ForegroundColor;

			//to RGBColor
			var rgb = color.RGB;

			return Color.FromArgb((int)rgb.Red, (int)rgb.Green, (int)rgb.Blue);
		}


		public static Color GetBackgroundColor(dynamic appRef)
		{
			//SolidColor
			var color = appRef.BackgroundColor;

			//to RGBColor
			var rgb = color.RGB;

			return Color.FromArgb((int)rgb.Red, (int)rgb.Green, (int)rgb.Blue);
		}

		public static void SetForegroundColor(dynamic appRef, Color color)
		{
			var tmp = appRef.ForegroundColor;
			tmp.RGB.Red = color.R;
			tmp.RGB.Green = color.G;
			tmp.RGB.Blue = color.B;
			appRef.ForegroundColor = tmp;
		}

		public static void SetBackgroundColor(dynamic appRef, Color color)
		{
			var tmp = appRef.BackgroundColor;
			tmp.RGB.Red = color.R;
			tmp.RGB.Green = color.G;
			tmp.RGB.Blue = color.B;
			appRef.BackgroundColor = tmp;
		}





		public static RectangleDouble GetCurrentSelection(dynamic appRef)
		{
			try
			{
				var current = appRef.ActiveDocument.Selection.Bounds();
				if (current == null)
					return null;

				List<double> points = new List<double>();

				foreach (var item in current)
				{
					double? value = item;
					if (value == null)
						return null;

					points.Add((double)value);

					if (points.Count > 4)
						return null;
				}

				if (points.Count != 4)
					return null;

				return new RectangleDouble(points[0], points[1], points[2] - points[0], points[3] - points[1]);
			}
			catch (Exception)
			{
				return null;
			}
		}



		public static void Wait(dynamic appRef)
		{
			var eventWait = appRef.charIDToTypeID("Wait");
			var enumRedrawComplete = appRef.charIDToTypeID("RdCm");
			var typeState = appRef.charIDToTypeID("Stte");
			var keyState = appRef.charIDToTypeID("Stte");
			dynamic desc = Activator.CreateInstance(Type.GetTypeFromProgID("Photoshop.ActionDescriptor"));
			desc.putEnumerated(keyState, typeState, enumRedrawComplete);

			appRef.executeAction(eventWait, desc, 3);   //PsDialogModes: psDisplayNoDialogs	(pdf p.161)
		}
	}
}
