﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ocuda.Ops.DataProvider.SqlServer.Promenade;

namespace Ocuda.Ops.DataProvider.SqlServer.Promenade.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20200219235619_prom_v1.0.0.2.cs")]
    partial class prom_v1002cs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FriendlyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Xml")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.CategoryText", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("CategoryId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CategoryTexts");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Emedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("RedirectUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Stub")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Emedia");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.EmediaCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("EmediaId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "EmediaId");

                    b.HasIndex("EmediaId");

                    b.ToTable("EmediaCategories");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.EmediaGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("SegmentId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SegmentId");

                    b.ToTable("EmediaGroups");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.EmediaText", b =>
                {
                    b.Property<int>("EmediaId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("EmediaId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("EmediaTexts");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.ExternalResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("ExternalResources");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BodyText")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(48)")
                        .HasMaxLength(48);

                    b.Property<string>("IconText")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("Stub")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("GroupType")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsLocationRegion")
                        .HasColumnType("bit");

                    b.Property<string>("MapImage")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Stub")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("SubscriptionUrl")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AddressType")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("AdministrativeArea")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("AreaServedName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("AreaServedType")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("ContactType")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("DescriptionSegmentId")
                        .HasColumnType("int");

                    b.Property<int?>("DisplayGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("EventLink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("GeoLocation")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("HasEvents")
                        .HasColumnType("bit");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsAccessibleForFree")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAlwaysOpen")
                        .HasColumnType("bit");

                    b.Property<string>("LocatorName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LocatorNotes")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MapImagePath")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MapLink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("PAbbreviation")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ParentOrganization")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int?>("PostFeatureSegmentId")
                        .HasColumnType("int");

                    b.Property<int?>("PreFeatureSegmentId")
                        .HasColumnType("int");

                    b.Property<string>("PriceRange")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("SocialCardId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Stub")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("SubscriptionLink")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("DisplayGroupId");

                    b.HasIndex("SocialCardId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationFeature", b =>
                {
                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<bool>("NewTab")
                        .HasColumnType("bit");

                    b.Property<string>("RedirectUrl")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FeatureId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("LocationFeatures");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("HasSubscription")
                        .HasColumnType("bit");

                    b.HasKey("GroupId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("LocationGroups");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationHours", b =>
                {
                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Open")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OpenTime")
                        .HasColumnType("datetime2");

                    b.HasKey("DayOfWeek", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("LocationHours");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationHoursOverride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<bool>("Open")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OpenTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("LocationHoursOverrides");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Navigation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ChangeToLinkWhenExtraSmall")
                        .HasColumnType("bit");

                    b.Property<bool>("HideTextWhenExtraSmall")
                        .HasColumnType("bit");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("NavigationId")
                        .HasColumnType("int");

                    b.Property<int?>("NavigationTextId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<bool>("TargetNewWindow")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("NavigationId");

                    b.ToTable("Navigations");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.NavigationText", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("AriaLabel")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id", "LanguageId");

                    b.ToTable("NavigationTexts");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Page", b =>
                {
                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int>("PageHeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<int?>("SocialCardId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("LanguageId", "PageHeaderId");

                    b.HasIndex("PageHeaderId");

                    b.HasIndex("SocialCardId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.PageHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Stub")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PageHeaders");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Segment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.SegmentText", b =>
                {
                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("SegmentId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("SegmentId")
                        .IsUnique();

                    b.ToTable("SegmentTexts");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.SiteSetting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("SiteSettings");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.SocialCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("ImageAlt")
                        .HasColumnType("nvarchar(420)")
                        .HasMaxLength(420);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("SocialCards");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.UrlRedirect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPermanent")
                        .HasColumnType("bit");

                    b.Property<string>("RequestPath")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("UrlRedirects");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.UrlRedirectAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AccessDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UrlRedirectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UrlRedirectId");

                    b.ToTable("UrlRedirectAccesses");
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.CategoryText", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Emedia", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.EmediaGroup", "Group")
                        .WithMany("Emedias")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.EmediaCategory", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.Emedia", "Emedia")
                        .WithMany()
                        .HasForeignKey("EmediaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.EmediaGroup", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Segment", "Segment")
                        .WithMany()
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.EmediaText", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Emedia", "Emedia")
                        .WithMany()
                        .HasForeignKey("EmediaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Location", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Group", "DisplayGroup")
                        .WithMany()
                        .HasForeignKey("DisplayGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ocuda.Promenade.Models.Entities.SocialCard", "SocialCard")
                        .WithMany()
                        .HasForeignKey("SocialCardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationFeature", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationGroup", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationHours", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.LocationHoursOverride", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Navigation", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Navigation", null)
                        .WithMany("Navigations")
                        .HasForeignKey("NavigationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.Page", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.PageHeader", "PageHeader")
                        .WithMany()
                        .HasForeignKey("PageHeaderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.SocialCard", "SocialCard")
                        .WithMany()
                        .HasForeignKey("SocialCardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.SegmentText", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ocuda.Promenade.Models.Entities.Segment", null)
                        .WithOne("SegmentText")
                        .HasForeignKey("Ocuda.Promenade.Models.Entities.SegmentText", "SegmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ocuda.Promenade.Models.Entities.UrlRedirectAccess", b =>
                {
                    b.HasOne("Ocuda.Promenade.Models.Entities.UrlRedirect", "UrlRedirect")
                        .WithMany()
                        .HasForeignKey("UrlRedirectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
