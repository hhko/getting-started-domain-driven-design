﻿// <auto-generated />
using System;
using GymManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    [DbContext(typeof(GymManagementDbContext))]
    partial class GymManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("GymManagement.Domain.AdminAggregate.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("GymManagement.Domain.GymAggregate.Gym", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("_maxRooms")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxRooms");

                    b.Property<string>("_roomIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("RoomIds");

                    b.Property<string>("_trainerIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("TrainerIds");

                    b.HasKey("Id");

                    b.ToTable("Gyms");
                });

            modelBuilder.Entity("GymManagement.Domain.SubscriptionAggregate.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("_adminId")
                        .HasColumnType("TEXT")
                        .HasColumnName("AdminId");

                    b.Property<string>("_gymIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("GymIds");

                    b.Property<int>("_maxGyms")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxGyms");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("GymManagement.Infrastructure.IntegrationEvents.OutboxIntegrationEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EventContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OutboxIntegrationEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
