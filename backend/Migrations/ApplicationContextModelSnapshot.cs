﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using healthy_lifestyle_web_app.ContextModels;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MusclePhysicalActivity", b =>
                {
                    b.Property<int>("MusclesId")
                        .HasColumnType("int");

                    b.Property<int>("PhysicalActivitiesId")
                        .HasColumnType("int");

                    b.HasKey("MusclesId", "PhysicalActivitiesId");

                    b.HasIndex("PhysicalActivitiesId");

                    b.ToTable("MusclePhysicalActivity");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Day", b =>
                {
                    b.Property<int>("ProfileId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(1);

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<int>("WaterIntake")
                        .HasColumnType("int");

                    b.HasKey("ProfileId", "Date");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.DayFood", b =>
                {
                    b.Property<int>("ProfileId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(1);

                    b.Property<int>("FoodId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("Grams")
                        .HasColumnType("int");

                    b.HasKey("ProfileId", "Date", "FoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("DayFoods");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.DayPhysicalActivity", b =>
                {
                    b.Property<int>("ProfileId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(1);

                    b.Property<int>("PhysicalActivityId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.HasKey("ProfileId", "Date", "PhysicalActivityId");

                    b.HasIndex("PhysicalActivityId");

                    b.ToTable("DayPhysicalActivities");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("float");

                    b.Property<double>("Fats")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Proteins")
                        .HasColumnType("float");

                    b.Property<bool>("Public")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Muscle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Muscles");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.PhysicalActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PhysicalActivities");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<int>("Goal")
                        .HasColumnType("int");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.RecipeFood", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("FoodId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("Grams")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "FoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("RecipeFoods");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodId")
                        .IsUnique();

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.WeightEvolution", b =>
                {
                    b.Property<int>("EvolutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EvolutionId"));

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("EvolutionId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("WeightEvolutions");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusclePhysicalActivity", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.Muscle", null)
                        .WithMany()
                        .HasForeignKey("MusclesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("healthy_lifestyle_web_app.Entities.PhysicalActivity", null)
                        .WithMany()
                        .HasForeignKey("PhysicalActivitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Day", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.Profile", "Profile")
                        .WithMany("Days")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.DayFood", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.Food", "Food")
                        .WithMany("DayFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("healthy_lifestyle_web_app.Entities.Day", null)
                        .WithMany("DayFoods")
                        .HasForeignKey("ProfileId", "Date")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.DayPhysicalActivity", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.PhysicalActivity", "PhysicalActivity")
                        .WithMany("DayPhysicalActivities")
                        .HasForeignKey("PhysicalActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("healthy_lifestyle_web_app.Entities.Day", null)
                        .WithMany("DayPhysicalActivities")
                        .HasForeignKey("ProfileId", "Date")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhysicalActivity");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Food", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("Foods")
                        .HasForeignKey("ApplicationUserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Profile", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("Profile")
                        .HasForeignKey("healthy_lifestyle_web_app.Entities.Profile", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.RecipeFood", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.Food", null)
                        .WithMany("RecipeFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("healthy_lifestyle_web_app.Entities.Recipe", null)
                        .WithMany("RecipeFoods")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Request", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.Food", "Food")
                        .WithOne("Request")
                        .HasForeignKey("healthy_lifestyle_web_app.Entities.Request", "FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.WeightEvolution", b =>
                {
                    b.HasOne("healthy_lifestyle_web_app.Entities.Profile", "Profile")
                        .WithMany("WeightEvolutions")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Day", b =>
                {
                    b.Navigation("DayFoods");

                    b.Navigation("DayPhysicalActivities");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Food", b =>
                {
                    b.Navigation("DayFoods");

                    b.Navigation("RecipeFoods");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.PhysicalActivity", b =>
                {
                    b.Navigation("DayPhysicalActivities");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Profile", b =>
                {
                    b.Navigation("Days");

                    b.Navigation("WeightEvolutions");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.Recipe", b =>
                {
                    b.Navigation("RecipeFoods");
                });

            modelBuilder.Entity("healthy_lifestyle_web_app.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Foods");

                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}
