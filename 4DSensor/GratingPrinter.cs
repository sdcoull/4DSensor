using System;
using System.Drawing;

namespace DSensor
{
	public class GratingPrinter
	{
		public void OneDimensionalPrinter()
		{
			int width = 3800; //roughly 10cm in pixels, assuming the dpi is 96
			int height = 3800;

			using (Image output = new Bitmap(width, height))
			{
				Graphics graphics = Graphics.FromImage(output);
				graphics.FillRectangle(Brushes.White, 0, 0, width, height);

				int x = 0;
				while (x < width)
				{
					graphics.FillRectangle(Brushes.Black, x, 0, MmToPixels(1, graphics.DpiX), height);
					x += MmToPixels(2, graphics.DpiX);
				}

				output.Save("output - q1.bmp");
			}
		}

		public void TwoDimensionalPrinter()
		{
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
					graphics.FillRectangle(Brushes.Black, 0, y, width, MmToPixels(1, graphics.DpiX));
					//draws a second grey rectangle at an offset of y + 0.1mm and height of 0.8mm
					graphics.FillRectangle(Brushes.Gray, 0, y + MmToPixels(0.1, graphics.DpiY),
										   width, MmToPixels(0.8, graphics.DpiY));
					y += MmToPixels(2, graphics.DpiY);
				}
				while (x < height)
				{
					graphics.FillRectangle(Brushes.Black, x, 0, MmToPixels(1, graphics.DpiX), height);
					graphics.FillRectangle(Brushes.Gray, x + MmToPixels(0.1, graphics.DpiX), MmToPixels(0.1, graphics.DpiX),
										   MmToPixels(0.8, graphics.DpiX), height - MmToPixels(0.1, graphics.DpiX));
					x += MmToPixels(2, graphics.DpiX);
				}

				output.Save("output - q2.bmp");
			}
		}

		private int MmToPixels(double mm, double dpi)
		{
			return (int)Math.Round(mm * dpi / 2.54d);
		}
	}
}

