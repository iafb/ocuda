﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ocuda.Ops.Data
{
    public abstract class OpsContext : Utility.Data.DbContextBase, IMigratableContext
    {
        public OpsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // turn off cascading deletes
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // configure composite keys
            // https://docs.microsoft.com/en-us/ef/core/modeling/keys
            modelBuilder.Entity<Models.UserMetadata>()
                .HasKey(_ => new { _.UserId, _.UserMetadataTypeId });

            /*
            var categoryConverter = new EnumToNumberConverter<Models.CategoryType, int>();
            modelBuilder
                .Entity<Models.Category>()
                .Property(_ => _.CategoryType)
                .HasConversion(categoryConverter);
            */
        }

        #region IMigratableContext
        public new void Migrate() => Database.Migrate();
        public new IEnumerable<string> GetPendingMigrationList() => Database.GetPendingMigrations();
        public new string GetCurrentMigration() => Database.GetAppliedMigrations().Last();
        #endregion IMigratableContext

        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.ClaimGroup> ClaimGroups { get; set; }
        public DbSet<Models.CategoryFileType> CategoryFileTypes { get; set; }
        public DbSet<Models.ExternalResource> ExternalResources { get; set; }
        public DbSet<Models.File> Files { get; set; }
        public DbSet<Models.FileType> FileTypes { get; set; }
        public DbSet<Models.Link> Links { get; set; }
        public DbSet<Models.Page> Pages { get; set; }
        public DbSet<Models.Post> Posts { get; set; }
        public DbSet<Models.RosterHeader> RosterHeaders { get; set; }
        public DbSet<Models.RosterDetail> RosterDetails { get; set; }
        public DbSet<Models.Section> Sections { get; set; }
        public DbSet<Models.SectionManagerGroup> SectionManagerGroups { get; set; }
        public DbSet<Models.SiteSetting> SiteSettings { get; set; }
        public DbSet<Models.Thumbnail> Thumbnails { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.UserMetadata> UserMetadata { get; set; }
        public DbSet<Models.UserMetadataType> UserMetadataTypes { get; set; }
    }
}
