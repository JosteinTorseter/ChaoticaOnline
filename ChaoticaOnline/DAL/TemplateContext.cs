using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using ChaoticaOnline.TemplateModels;

namespace ChaoticaOnline.DAL
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(): base("TemplateDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TDBTerrain> TDBTerrain { get; set; }
        public DbSet<TDBRace> TDBRaces { get; set; }
        public DbSet<TDBDwelling> TDBDwellings { get; set; }
        public DbSet<TDBDungeon> TDBDungeons { get; set; }
        public DbSet<TDBUnit> TDBUnits { get; set; }
        public DbSet<TDBClass> TDBClasses { get; set; }
    }
}