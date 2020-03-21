using ImageMagick;
using LegoCatalog.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.Threading;

// https://www.nuget.org/packages/Magick.NET-Q8-x64/
// https://github.com/dlemstra/Magick.NET/
// https://imagemagick.org/Usage/resize/
// dotnet add package Magick.NET-Q8-x64 --version 7.15.4

namespace LegoCatalog.ImageResize
{
    public class ImageResize
    {
        private PartsCatalogDbContext db;
        private const string LegoCatalogConnectionString = @"Server=localhost;Database=LegoCatalog;Trusted_Connection=True;";

        public ImageResize()
        {

            //Program._config

            var contextBuilder = new DbContextOptionsBuilder<PartsCatalogDbContext>();
            contextBuilder.UseSqlServer(LegoCatalogConnectionString);
            db = new PartsCatalogDbContext(contextBuilder.Options);
        }

        public void ResizeSetOfLegoImages(int totalImagesToConvert = 100)
        {
            string LegoImagesWriteLocation = "c:\\LegoCatalog\\ImagesJpeg";

            int i = 1;
            foreach (var part in db.Parts.Where(p => string.IsNullOrWhiteSpace(p.IconLinkJpeg)).Take(totalImagesToConvert).OrderBy(p => p.ItemId).ToList())
            //foreach (var part in db.Parts.Where(p => p.IconLinkJpeg == "error").Take(totalImagesToConvert).OrderBy(p => p.ItemId).ToList())
            {
                byte[] imageBytes;
                string imageFilename = $"{part.ItemId}.jpg";

                try
                {
                    using (var webClient = new WebClient())
                    {
                        var imageLink = part.ImageLink.Replace("https://", "");
                        imageBytes = webClient.DownloadData(part.ImageLink);
                    }

                    if (imageBytes != null)
                    {
                        MagickImage image = new MagickImage(imageBytes);
                        image.Format = MagickFormat.Jpeg;
                        image.Quality = 75;

                        image.Resize(80, 80);
                        image.Write(Path.Combine(LegoImagesWriteLocation, imageFilename));

                        part.IconLinkJpeg = imageFilename;
                        db.SaveChanges();

                        Console.WriteLine($"Converted image {i++} to {imageFilename}");
                    }
                }
                catch (Exception ex)
                {
                    part.IconLinkJpeg = "error";
                    db.SaveChanges();

                    Console.WriteLine($"Error resizing image {imageFilename} from {part.ImageLink}: {ex.Message}");
                    Thread.Sleep(1000);  // pause incase the issue is network related
                }
            }
        }

        private void ResizeImage(string filename)
        {
            using (MagickImage image = new MagickImage("filename.png"))
            {
                MagickGeometry size = new MagickGeometry(80, 80);
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.
                size.IgnoreAspectRatio = true;

                image.Resize(size);

                // Save the result
                image.Write("C:/output/" + "Snakeware.100x100.png");
            }
        }
    }
}