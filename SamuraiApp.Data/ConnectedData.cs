﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class ConnectedData
    {
        private SamuraiContext _context;

        public ConnectedData()
        {
            _context = new SamuraiContext();
            _context.Database.Migrate();
        }

        public Samurai CreateNewSamurai()
        {
            var samurai = new Samurai
            {
                Name = "New Samurai"
            };
            _context.Samurais.Add(samurai);
            return samurai;
        }

    }
}
