﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using SamuraiApp.Data;

namespace SomeUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurai();
            SimpleSamuraiQuery();

            Console.ReadKey();
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();

            }
        }

        private static void InsertMultipleSamurai()
        {
            var samurai1 = new Samurai { Name = "Herb1" };
            var samurai2 = new Samurai { Name = "Herb2" };
            var samurai3 = new Samurai { Name = "Herb3" };

            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai1,samurai2, samurai3);
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            
            var samurai = new Samurai {Name = "Herb"};

            using (var context = new SamuraiContext())
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Samurais.Add(samurai);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        
                    }
                }

            }
        }
    }
}
