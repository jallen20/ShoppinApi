using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShoppinAPICore.Models
{
    public partial class ShoppinDbContext : DbContext
    {
        public ShoppinDbContext()
        {
        }

        public ShoppinDbContext(DbContextOptions<ShoppinDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DeliveryType> DeliveryType { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<InventoryItem> InventoryItem { get; set; }
        public virtual DbSet<InventoryItemStatus> InventoryItemStatus { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<ShoppinAccount> ShoppinAccount { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionItem> TransactionItem { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=34.69.56.101;user id=root;password=password;persistsecurityinfo=True;database=shoppin_db;allow user variables=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State).IsRequired();

                entity.Property(e => e.StreetAddress1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.Property(e => e.DeliveryTypeId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasIndex(e => e.InventoryItemId)
                    .HasName("Inventory_Fk_ItemId");

                entity.HasIndex(e => e.StoreId)
                    .HasName("Inventory_Fk_StoreId");

                entity.Property(e => e.InventoryId).HasColumnType("int(11)");

                entity.Property(e => e.InventoryItemId).HasColumnType("int(11)");

                entity.Property(e => e.StoreId).HasColumnType("int(11)");


            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.HasIndex(e => e.InventoryItemStatus)
                    .HasName("InventoryItem_fk_Status");

                entity.HasIndex(e => e.ProductId)
                    .HasName("IventoryItem_fk_ProductId");

                entity.Property(e => e.InventoryItemId).HasColumnType("int(11)");

                entity.Property(e => e.InventoryItemStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnType("int(11)");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");


            });

            modelBuilder.Entity<InventoryItemStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PRIMARY");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("Product_Fk_CatId");

                entity.Property(e => e.ProductId).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10,0)");

            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.SessionToken)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Email)
                    .HasName("Email")
                    .IsUnique();

                entity.Property(e => e.SessionToken)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<ShoppinAccount>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PRIMARY");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasIndex(e => e.AddressId)
                    .HasName("Store_fk_address");

                entity.HasIndex(e => e.OwnerEmail)
                    .HasName("Store_fk_Email");

                entity.Property(e => e.StoreId).HasColumnType("int(11)");

                entity.Property(e => e.AddressId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.BuyerEmail)
                    .HasName("Trans_Fk_BEmail");

                entity.HasIndex(e => e.DeliveryTypeId)
                    .HasName("Trans_Fk_DType");

                entity.HasIndex(e => e.SellerEmail)
                    .HasName("Trans_Fk_SEmail");

                entity.HasIndex(e => e.StoreId)
                    .HasName("Trans_Fk_Store");

                entity.HasIndex(e => e.TransactionTypeId)
                    .HasName("Trans_Fk_TransType");

                entity.Property(e => e.TransactionId).HasColumnType("int(11)");

                entity.Property(e => e.BuyerEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryTypeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SellerEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnType("int(11)");

                entity.Property(e => e.TransactionTotal).HasColumnType("decimal(10,0)");

                entity.Property(e => e.TransactionTypeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<TransactionItem>(entity =>
            {
                entity.HasIndex(e => e.InventoryItemId)
                    .HasName("TransI_Fk_IvnId");

                entity.HasIndex(e => e.TransactionId)
                    .HasName("TransI_Fk_TransId");

                entity.Property(e => e.TransactionItemId).HasColumnType("int(11)");

                entity.Property(e => e.InventoryItemId).HasColumnType("int(11)");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.TransactionId).HasColumnType("int(11)");

            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.Property(e => e.TransactionTypeId)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.AddressId)
                    .HasName("User_fk_address");

                entity.HasIndex(e => e.Email)
                    .HasName("User_fk_Email");

                entity.HasIndex(e => e.UserTypeId)
                    .HasName("UserTypeId");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.AddressId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserTypeId)
                    .HasMaxLength(20)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.UserTypeId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
