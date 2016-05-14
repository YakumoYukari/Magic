﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public static class ImageResizer
	{
		public static Bitmap ResizeImage(Bitmap originalImage, int maxWidth, int maxHeight)
		{
			//Caluate new Size
			int newWidth = originalImage.Width;
			int newHeight = originalImage.Height;
			double aspectRatio = (double)originalImage.Width / (double)originalImage.Height;
			if (aspectRatio <= 1 && originalImage.Width > maxWidth)
			{
				newWidth = maxWidth;
				newHeight = (int)Math.Round(newWidth / aspectRatio);
			}
			else if (aspectRatio > 1 && originalImage.Height > maxHeight)
			{
				newHeight = maxHeight;
				newWidth = (int)Math.Round(newHeight * aspectRatio);
			}
			Bitmap newImage = new Bitmap(newWidth, newHeight);
			using (Graphics g = Graphics.FromImage(newImage))
			{
				//--Quality Settings Adjust to fit your application
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.DrawImage(originalImage, 0, 0, newImage.Width, newImage.Height);
				return newImage;
			}
		}

		public static Bitmap ResizeImage(String filename, int maxWidth, int maxHeight)
		{
			using (System.Drawing.Image originalImage = System.Drawing.Image.FromFile(filename))
			{
				//Caluate new Size
				int newWidth = originalImage.Width;
				int newHeight = originalImage.Height;
				double aspectRatio = (double)originalImage.Width / (double)originalImage.Height;
				if (aspectRatio <= 1 && originalImage.Width > maxWidth)
				{
					newWidth = maxWidth;
					newHeight = (int)Math.Round(newWidth / aspectRatio);
				}
				else if (aspectRatio > 1 && originalImage.Height > maxHeight)
				{
					newHeight = maxHeight;
					newWidth = (int)Math.Round(newHeight * aspectRatio);
				}
				Bitmap newImage = new Bitmap(newWidth, newHeight);
				using (Graphics g = Graphics.FromImage(newImage))
				{
					//--Quality Settings Adjust to fit your application
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
					g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
					g.DrawImage(originalImage, 0, 0, newImage.Width, newImage.Height);
					return newImage;
				}
			}
		}
	}
}
