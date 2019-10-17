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
        private static  SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurai();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //InsertBattle();
            //QueryAndUpdateBattle_Disconnected();
            //AddSomeMoreSamurais();
            //DeleteWhileTracked();
            //DeleteWhileNotTracked();
            //InsertNewPkFkGraph();
            InsertNewPkMultipleChildren();

            Console.ReadKey();
        }

        private static void InsertNewPkMultipleChildren()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote{Text = "Watch out for my sharp sword!"},
                    new Quote{Text = "I told you to watch out for the sharp sword! Oh well!"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote{Text = "I've come to save you"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddSomeMoreSamurais()
        {
            _context.AddRange(
                new Samurai {Name = "Kambei Shimada"},
                new Samurai { Name = "Shichiroji" },
                new Samurai { Name = "Katsushiro Okamoto" },
                new Samurai { Name = "Heihachi Hayashida" },
                new Samurai { Name = "Kyuzo" },
                new Samurai { Name = "Gorobei Katayama" },
                new Samurai { Name = "Obiwan Kinobi" }
                );
            _context.SaveChanges();
        }

        private static void DeleteWhileNotTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Heihachi Hayashida");
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Remove(samurai);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shimada");
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle{ Name = "Battle of Hiro", StartDate = new DateTime(1560, 05, 01), EndDate = new DateTime(1560, 06, 15) });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1560,06,30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Where(s => !EF.Functions.Like(s.Name,"% San")).ToList();
            samurais.ForEach(s => s.Name += " San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += " San";
            _context.SaveChanges();
        }

        private static void MoreQueries()
        {
            var name = "Sampson";
            //var samurais = _context.Samurais.FirstOrDefault(s => s.Name == name);
            var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name,"H%")).ToList();

        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();
                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai.Name);
                }

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
