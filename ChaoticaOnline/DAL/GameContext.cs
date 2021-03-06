﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using ChaoticaOnline.GameDBModels;

namespace ChaoticaOnline.DAL
{
    public class GameContext : DbContext
    {
        public GameContext(): base("GameDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<Dwelling>()
                     .HasRequired(d => d.Tile)
                     .WithMany(t => t.Dwellings)
                     .HasForeignKey(d => d.TileId)
                     .WillCascadeOnDelete(false);
            modelBuilder.Entity<Dungeon>()
                     .HasRequired(d => d.Tile)
                     .WithMany(t => t.Dungeons)
                     .HasForeignKey(d => d.TileId)
                     .WillCascadeOnDelete(false);
            modelBuilder.Entity<Party>()
                     .HasOptional(p => p.Tile)
                     .WithMany(t => t.Parties)
                     .HasForeignKey(p => p.TileId)
                     .WillCascadeOnDelete(false);
            modelBuilder.Entity<Party>()
                     .HasOptional(p => p.Game)
                     .WithMany(g => g.Parties)
                     .HasForeignKey(p => p.GameId)
                     .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Dwelling> Dwellings { get; set; }
        public DbSet<Dungeon> Dungeons { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Bonus> Bonuses { get; set; }
        public DbSet<WorldItem> WorldItems { get; set; }
        public DbSet<TileCard> TileCards { get; set; }

    }
}