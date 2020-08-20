﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ResidentContactApi.V1.Infrastructure;

namespace ResidentContactApi.V1.Infrastructure.Migrations
{
    [DbContext(typeof(ResidentContactContext))]
    [Migration("20200819090703_RenameContactAndContactSubTypeLookupTables")]
    partial class RenameContactAndContactSubTypeLookupTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddedBy")
                        .HasColumnName("added_by")
                        .HasColumnType("text");

                    b.Property<int?>("ContactSubTypeLookupId")
                        .HasColumnName("contact_subtype_lookup_id")
                        .HasColumnType("integer");

                    b.Property<int>("ContactTypeLookupId")
                        .HasColumnName("contact_type_lookup_id")
                        .HasColumnType("integer");

                    b.Property<string>("ContactValue")
                        .HasColumnName("contact_details_value")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnName("date_added")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateLastModified")
                        .HasColumnName("date_modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDefault")
                        .HasColumnName("is_default")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("text");

                    b.Property<int>("ResidentId")
                        .HasColumnName("resident_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ContactSubTypeLookupId");

                    b.HasIndex("ContactTypeLookupId");

                    b.HasIndex("ResidentId");

                    b.ToTable("contact_details");
                });

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.ContactSubTypeLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("contact_sub_type_lookup");
                });

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.ContactTypeLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("contact_type_lookup");
                });

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.ExternalSystemId", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ExternalIdName")
                        .IsRequired()
                        .HasColumnName("external_id_name")
                        .HasColumnType("text");

                    b.Property<string>("ExternalIdValue")
                        .IsRequired()
                        .HasColumnName("external_id_value")
                        .HasColumnType("text");

                    b.Property<int>("ExternalSystemLookupId")
                        .HasColumnName("external_system_lookup_id")
                        .HasColumnType("integer");

                    b.Property<int>("ResidentId")
                        .HasColumnName("resident_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExternalSystemLookupId");

                    b.HasIndex("ResidentId");

                    b.ToTable("external_system_ids");
                });

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.ExternalSystemLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("external_system_lookup");
                });

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.Resident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnName("date_of_birth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("text");

                    b.Property<char?>("Gender")
                        .HasColumnName("gender")
                        .HasColumnType("character(1)");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("residents");
                });

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.Contact", b =>
                {
                    b.HasOne("ResidentContactApi.V1.Infrastructure.ContactSubTypeLookup", "ContactSubTypeLookup")
                        .WithMany()
                        .HasForeignKey("ContactSubTypeLookupId");

                    b.HasOne("ResidentContactApi.V1.Infrastructure.ContactTypeLookup", "ContactTypeLookup")
                        .WithMany()
                        .HasForeignKey("ContactTypeLookupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResidentContactApi.V1.Infrastructure.Resident", "Resident")
                        .WithMany("Contacts")
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ResidentContactApi.V1.Infrastructure.ExternalSystemId", b =>
                {
                    b.HasOne("ResidentContactApi.V1.Infrastructure.ExternalSystemLookup", "ExternalSystem")
                        .WithMany()
                        .HasForeignKey("ExternalSystemLookupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResidentContactApi.V1.Infrastructure.Resident", "Resident")
                        .WithMany()
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
