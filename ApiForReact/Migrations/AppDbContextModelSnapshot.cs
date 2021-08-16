﻿// <auto-generated />
using System;
using ApiForReact.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiForReact.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("ApiForReact.Data.Dto.Dialog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserCompanionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserOwnerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserCompanionId");

                    b.HasIndex("UserOwnerId");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DialogId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserCompanionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserOwnerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.HasIndex("UserCompanionId");

                    b.HasIndex("UserOwnerId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("LikesCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Info")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLookingForAJob")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResumeText")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("UserContactsId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserContactsId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.UserContacts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Facebook")
                        .HasColumnType("TEXT");

                    b.Property<string>("GitHub")
                        .HasColumnType("TEXT");

                    b.Property<string>("Instagram")
                        .HasColumnType("TEXT");

                    b.Property<string>("Twitter")
                        .HasColumnType("TEXT");

                    b.Property<string>("Vk")
                        .HasColumnType("TEXT");

                    b.Property<string>("Youtube")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.UserUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriberUserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberUserId", "SubscriptionUserId")
                        .IsUnique();

                    b.ToTable("UsersUsers");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.Dialog", b =>
                {
                    b.HasOne("ApiForReact.Data.Dto.User", "UserCompanion")
                        .WithMany()
                        .HasForeignKey("UserCompanionId");

                    b.HasOne("ApiForReact.Data.Dto.User", "UserOwner")
                        .WithMany()
                        .HasForeignKey("UserOwnerId");

                    b.Navigation("UserCompanion");

                    b.Navigation("UserOwner");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.Message", b =>
                {
                    b.HasOne("ApiForReact.Data.Dto.Dialog", null)
                        .WithMany("Messages")
                        .HasForeignKey("DialogId");

                    b.HasOne("ApiForReact.Data.Dto.User", "UserCompanion")
                        .WithMany()
                        .HasForeignKey("UserCompanionId");

                    b.HasOne("ApiForReact.Data.Dto.User", "UserOwner")
                        .WithMany()
                        .HasForeignKey("UserOwnerId");

                    b.Navigation("UserCompanion");

                    b.Navigation("UserOwner");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.Post", b =>
                {
                    b.HasOne("ApiForReact.Data.Dto.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.User", b =>
                {
                    b.HasOne("ApiForReact.Data.Dto.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("ApiForReact.Data.Dto.UserContacts", "UserContacts")
                        .WithMany()
                        .HasForeignKey("UserContactsId");

                    b.Navigation("Location");

                    b.Navigation("UserContacts");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.Dialog", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ApiForReact.Data.Dto.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
