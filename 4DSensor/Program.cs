using System;
using System.Drawing;

namespace DSensor
{
	class DSensor
	{
		public static void Main(string[] args)
		{
			GratingPrinter g = new GratingPrinter();
			g.OneDimensionalPrinter();
			g.TwoDimensionalPrinter();
		}


	}
}
