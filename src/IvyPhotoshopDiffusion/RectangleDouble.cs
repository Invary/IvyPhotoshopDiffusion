using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invary.IvyPhotoshopDiffusion
{
	public class RectangleDouble
	{
		public double X { set; get; }
		public double Y { set; get; }
		public double Width { set; get; }
		public double Height { set; get; }

		public RectangleDouble()
		{
		}

		public RectangleDouble(double x, double y, double w, double h)
		{
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}
	}
}
