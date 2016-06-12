using System;
using System.Drawing;

namespace DSensor
{
	class DSensor
	{
		public static void Main(string[] args)
		{
			var g = new GratingPrinter();
			//Q1
			try
			{
				Console.WriteLine("Printing one dimensional image...");
				g.OneDimensionalPrinter();
				Console.WriteLine("Finished!");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error printing one dimensional printer.");
				Console.WriteLine(e.Message);
				return;
			}

			//Q2
			try
			{
				Console.WriteLine("Printing two dimensional image...");
				g.TwoDimensionalPrinter();
				Console.WriteLine("Finished!");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error printing two dimensional printer.");
				Console.WriteLine(e.Message);
				return;
			}

			//Q3
			try
			{
				Console.WriteLine("Removing test.bmp noise with simple algorithm...");
				var testImage = new Bitmap("test.bmp");
				var noiseReducer = new NoiseReducer(testImage);
				noiseReducer.Fix(false);
				Console.WriteLine("Finished!");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error with simple noise removal.");
				Console.WriteLine(e.Message);
				return;
			}

			//Q4
			try
			{
				Console.WriteLine("Removing test.bmp noise with advanced algorithm....");
				var testImage = new Bitmap("test.bmp");
				var noiseReducer = new NoiseReducer(testImage);
				noiseReducer.Fix(true);
				Console.WriteLine("Finished!");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error with advanced noise removal.");
				Console.WriteLine(e.Message);
				return;
			}
		}
	}
}
