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
			Bitmap newImage = n.Fix(false);
			newImage.Save("output.bmp");

			Bitmap newImage2 = n.Fix(true);
			newImage2.Save("outputadv.bmp");

			Console.WriteLine("Done!");
		}
	}
}
