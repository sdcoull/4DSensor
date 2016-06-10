using System;
using System.Drawing;

namespace DSensor
{
	class DSensor
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello Steven!");
			question1();
			question2();
		}

		private static void question1()
		{
			Console.WriteLine("Drawing Q1 image...");

			int width = 3800; //roughly 10cm in pixels, assuming the dpi is 96
			int height = 3800;

			using (Image output = new Bitmap(width, height))
			{
				Graphics graphics = Graphics.FromImage(output);
				graphics.FillRectangle(Brushes.White, 0, 0, width, height);

				int x = 0;
				while (x < width)
				{
					graphics.FillRectangle(Brushes.Black, x, 0, mmToPixels(1, graphics.DpiX), height);
					x += mmToPixels(2, graphics.DpiX);
				}

				output.Save("output - q1.bmp");
			}

			Console.WriteLine("Q1 - done!");
		}

		private static void question2()
		{
			Console.WriteLine("Drawing Q2 image...");

			int width = 3800;
			int height = 3800;

			using (Image output = new Bitmap(width, height))
			{
				Graphics graphics = Graphics.FromImage(output);
				graphics.FillRectangle(Brushes.White, 0, 0, width, height);

				int x = 0;
				int y = 0;
				//drawing the horizontal lines first to replicate example image, where vertical lines are on top
				while (y < width)
				{
					graphics.FillRectangle(Brushes.Black, 0, y, width, mmToPixels(1, graphics.DpiX));
					//draws a second grey rectangle at an offset of y + 0.1mm and height of 0.8mm
					graphics.FillRectangle(Brushes.Gray, 0, y + mmToPixels(0.1, graphics.DpiY),
										   width, mmToPixels(0.8, graphics.DpiY));
					y += mmToPixels(2, graphics.DpiY);
				}
				while (x < height)
				{
					graphics.FillRectangle(Brushes.Black, x, 0, mmToPixels(1, graphics.DpiX), height);
					graphics.FillRectangle(Brushes.Gray, x + mmToPixels(0.1, graphics.DpiX), mmToPixels(0.1, graphics.DpiX),
										   mmToPixels(0.8, graphics.DpiX), height - mmToPixels(0.1, graphics.DpiX));
					x += mmToPixels(2, graphics.DpiX);
				}

				output.Save("output - q2.bmp");
			}
			Console.WriteLine("Q2 - done!");
		}

		private static int mmToPixels(double mm, double dpi)
		{
			return (int)Math.Round(mm * dpi / 2.54d);
		}
	}
}
