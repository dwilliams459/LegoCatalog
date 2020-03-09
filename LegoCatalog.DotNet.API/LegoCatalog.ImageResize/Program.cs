using System;
using ImageMagick;
using LegoCatalog.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

// https://www.nuget.org/packages/Magick.NET-Q8-x64/
// https://github.com/dlemstra/Magick.NET/
// https://imagemagick.org/Usage/resize/
// dotnet add package Magick.NET-Q8-x64 --version 7.15.4
namespace LegoCatalog.ImageResize
{
    class Program
    {
        static void Main(string[] args)
        {
            var imageResizer = new ImageResize();
            int i = 0;

            do
            {
                Console.WriteLine($"Iteration {++i}");
                if (args.Length > 0 && int.TryParse(args[0], out int numberToResize))
                {
                    imageResizer.ResizeSetOfLegoImages(numberToResize);
                }
                else
                {
                    imageResizer.ResizeSetOfLegoImages(100);
                }

                Thread.Sleep(5000);
            }
            while (i <= 100);

            Console.WriteLine($"Exit after {i} iterations");

            // For each image in lego database
            // Download image to local image folder
            // Convert image to smaller size
            // Write to storage folder
            // Write url to database
        }
    }
}
