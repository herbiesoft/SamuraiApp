using System;
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
            InsertSamurai();


            Console.ReadKey();
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
                        
                    }
                }

            }
        }
    }
}
