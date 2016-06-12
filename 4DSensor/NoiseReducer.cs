using System;
using System.Drawing;

class NoiseReducer
{
	private Bitmap originalImage;

	public NoiseReducer(Bitmap b)
	{
		originalImage = b;
	}

	public Bitmap Fix(bool advanced)
	{
		int width = originalImage.Width;
		int height = originalImage.Height;

		Bitmap newImage = new Bitmap(width, height);
		using (Graphics graphics = Graphics.FromImage(newImage))
		{
			graphics.DrawImage(originalImage, 0, 0);

			double[][] colors = new double[height][];

			for (int y = 0; y < height; y++)
			{
				colors[y] = new double[width];
				for (int x = 0; x < width; x++)
				{
					Color c = originalImage.GetPixel(x, y);
					double brightness = (c.G + c.B + c.R) / 3;
					colors[y][x] = brightness;
				}
			}

			for (int y = 1; y < height - 1; y++)
			{
				for (int x = 1; x < width - 1; x++)
				{
					if (advanced)
						ChangeBrightnessAdvanced(graphics, colors, y, x);
					else
						ChangeBrightness(graphics, colors, y, x);
				}
			}
		}

		return newImage;
	}

	void ChangeBrightness(Graphics graphics, double[][] colors, int y, int x)
	{
		double currentBrightness = colors[y][x];
		double surroundingBrightness = (colors[y - 1][x] + colors[y][x - 1] + colors[y][x + 1] + colors[y + 1][x]) / 4;

		if (Math.Abs(currentBrightness - surroundingBrightness) > 10)
		{
			Color c1 = originalImage.GetPixel(x, y - 1);
			Color c2 = originalImage.GetPixel(x - 1, y);
			Color c3 = originalImage.GetPixel(x + 1, y);
			Color c4 = originalImage.GetPixel(x, y + 1);

			int red = (c1.R + c2.R + c3.R + c4.R) / 4;
			int green = (c1.G + c2.G + c3.G + c4.G) / 4;
			int blue = (c1.B + c2.B + c3.B + c4.B) / 4;

			Color newColor = Color.FromArgb(red, green, blue);

			graphics.FillRectangle(new SolidBrush(newColor), x, y, 1, 1);
		}
	}

	void ChangeBrightnessAdvanced(Graphics graphics, double[][] colors, int y, int x)
	{
		double currentBrightness = colors[y][x];

		double surroundingBrightness = (colors[y-1][x-1] + colors[y-1][x] + colors[y-1][x+1] + 
		                                colors[y][x-1] + colors[y][x] + colors[y][x+1] +
		                               	colors[y+1][x-1] +colors[y+1][x] +colors[y+1][x+1]) / 9;

		if (Math.Abs(currentBrightness - surroundingBrightness) > 10)
		{
			double[] brightness = new double[] {colors[y-1][x-1] , colors[y-1][x] , colors[y-1][x+1] ,
										colors[y][x-1] , colors[y][x] , colors[y][x+1] ,
										   colors[y+1][x-1] , colors[y+1][x] , colors[y+1][x+1]};
			Tuple<int, int>[] positions = new Tuple<int, int>[] {
				Tuple.Create(x-1, y-1),Tuple.Create(x, y - 1), Tuple.Create(x+1, y - 1),
				     Tuple.Create(x-1, y),Tuple.Create(x, y),Tuple.Create(x+1, y),
				     Tuple.Create(x-1, y+1),Tuple.Create(x, y+1),Tuple.Create(x+1, y+1)};

			Color newColor = FifthBrightness(brightness, positions);

			graphics.FillRectangle(new SolidBrush(newColor), x, y, 1, 1);
		}
	}

	Color FifthBrightness(double[] brightness, Tuple<int, int>[] positions)
	{
		double tempBrightness;
		Tuple<int,int> tempPositions;
		for (int i = 1; i < brightness.Length; i++)
		{
			tempBrightness = brightness[i];
			tempPositions = positions[i];
			int j = i - 1;
			while (j >= 0 && brightness[j] > tempBrightness)
			{
				brightness[j + 1] = brightness[j];
				positions[j + 1] = positions[j];
				j--;
			}
			brightness[j + 1] = tempBrightness;
			positions[j + 1] = tempPositions;
		}

		return originalImage.GetPixel(positions[5].Item1, positions[5].Item2);
	}
}