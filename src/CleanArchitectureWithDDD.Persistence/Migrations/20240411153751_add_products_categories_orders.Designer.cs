﻿// <auto-generated />
using System;
using CleanArchitectureWithDDD.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchitectureWithDDD.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240411153751_add_products_categories_orders")]
    partial class add_products_categories_orders
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.COAs.COA", b =>
                {
                    b.Property<string>("HeadCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<int>("HeadLevel")
                        .HasColumnType("int");

                    b.Property<string>("HeadName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGl")
                        .HasColumnType("bit");

                    b.Property<string>("ParentHeadCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("HeadCode");

                    b.HasIndex("ParentHeadCode");

                    b.ToTable("Coas", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Invoices.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("InvoiceAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("InvoiceDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InvoiceSerial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InvoiceStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("InvoiceTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InvoiceTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoices", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Journals.Journal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsOpening")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JournalDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Journals", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.LineItems.LineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("LineItems", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Transactions.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("COAId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<double>("Credit")
                        .HasColumnType("float");

                    b.Property<double>("Debit")
                        .HasColumnType("float");

                    b.Property<Guid>("JournalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TransactionId");

                    b.HasIndex("COAId");

                    b.HasIndex("JournalId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Persistence.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id", "Name");

                    b.ToTable("OutboxMessageConsumers", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.COAs.COA", b =>
                {
                    b.HasOne("CleanArchitectureWithDDD.Domain.Entities.COAs.COA", "ParentCOA")
                        .WithMany("COAs")
                        .HasForeignKey("ParentHeadCode");

                    b.Navigation("ParentCOA");
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Invoices.Invoice", b =>
                {
                    b.HasOne("CleanArchitectureWithDDD.Domain.Entities.Customers.Customer", null)
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.LineItems.LineItem", b =>
                {
                    b.HasOne("CleanArchitectureWithDDD.Domain.Entities.Orders.Order", null)
                        .WithMany("LineItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CleanArchitectureWithDDD.Domain.ValueObjects.Price", "UnitPrice", b1 =>
                        {
                            b1.Property<Guid>("LineItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("UnitPriceAmount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("UnitPriceCurrency");

                            b1.HasKey("LineItemId");

                            b1.ToTable("LineItems");

                            b1.WithOwner()
                                .HasForeignKey("LineItemId");
                        });

                    b.Navigation("UnitPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Orders.Order", b =>
                {
                    b.HasOne("CleanArchitectureWithDDD.Domain.Entities.Customers.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Products.Product", b =>
                {
                    b.HasOne("CleanArchitectureWithDDD.Domain.Entities.Categories.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CleanArchitectureWithDDD.Domain.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PriceAmount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PriceCurrency");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Transactions.Transaction", b =>
                {
                    b.HasOne("CleanArchitectureWithDDD.Domain.Entities.COAs.COA", "COA")
                        .WithMany("Transactions")
                        .HasForeignKey("COAId");

                    b.HasOne("CleanArchitectureWithDDD.Domain.Entities.Journals.Journal", "Journal")
                        .WithMany("Transactions")
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("COA");

                    b.Navigation("Journal");
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.COAs.COA", b =>
                {
                    b.Navigation("COAs");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Categories.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Customers.Customer", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Journals.Journal", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("CleanArchitectureWithDDD.Domain.Entities.Orders.Order", b =>
                {
                    b.Navigation("LineItems");
                });
#pragma warning restore 612, 618
        }
    }
}
