﻿using gibddDBcore;
using gibddDBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gibddDb
{
    class Program
    {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<gibddDbContext> _optionsBuilder;
        static void Main(string[] args)
        {
            BuildOptions();
            //         Console.WriteLine(_configuration.GetConnectionString("gibddDBManager"));
            BuildOptions();
            InsertDrivers();
            ListInventory();

        }
        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<gibddDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("gibddDBManager"));
        }
        static void ListInventory()
        {
            using (var db = new gibddDbContext(_optionsBuilder.Options))
            {
                var items = db.Drivers.Take(3).OrderBy(x => x.firstName).ToList();
                items.ForEach(x => Console.WriteLine($"New Driver: {x.firstName}"));
            }
        }

        static void InsertDrivers()
        {
            var Drivers = new List<Driver>() {
                new Driver() {firstName = "Alonso", lastName = "Hopkins", middleName = "Caleb",},
                new Driver() {firstName = "Kaydence", lastName = "Sellers", middleName = "Susannah",},
                new Driver() {firstName = "Finnegan", lastName = "Bray", middleName = "Brighton",},
            };

            using (var db = new gibddDbContext(_optionsBuilder.Options))
            {
                db.AddRange(Drivers);
                db.SaveChanges();
            }
        }
    }
}
