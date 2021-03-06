﻿using System;
using ImageMagick;
using LegoCatalog.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

// https://www.nuget.org/packages/Magick.NET-Q8-x64/
// https://github.com/dlemstra/Magick.NET/
// https://imagemagick.org/Usage/resize/
// dotnet add package Magick.NET-Q8-x64 --version 7.15.4
namespace LegoCatalog.ImageResize
{
    class Program
    {
        public static IConfiguration _config; 

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
 
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Console.WriteLine("Begin Conversion");
            Console.WriteLine(_config["ConnectionStrings:LegoCatalogDatabase"]);

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
                    imageResizer.ResizeSetOfLegoImages(10000);
                }

                Thread.Sleep(10000);
            }
            while (i <= 20);

            Console.WriteLine($"Exited after {i} iterations");

            // For each image in lego database
            // Download image to local image folder
            // Convert image to smaller size
            // Write to storage folder
            // Write url to database
        }
    }
}
