﻿// <auto-generated />
using System;
using ApiForReact.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiForReact.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210811064259_UserUser added")]
    partial class UserUseradded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

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

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<Guid>("SubscriberUserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("SubscriberUserId", "SubscriptionUserId");

                    b.HasIndex("SubscriptionUserId");

                    b.ToTable("UserUser");
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

                    b.Navigation("Location");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("ApiForReact.Data.Dto.User", null)
                        .WithMany()
                        .HasForeignKey("SubscriberUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiForReact.Data.Dto.User", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
