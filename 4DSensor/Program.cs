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

			Console.WriteLine("Fixing image...");

			Bitmap b = new Bitmap("test.bmp");
			NoiseReducer n = new NoiseReducer(b);
			Bitmap newImage = n.Fix();
			newImage.Save("output.bmp");

			Console.WriteLine("Done!");
		}


	}
}
