using api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace api.DataAccess
{
    public class ConXContext : DbContext
    {
        public ConXContext() : base("ConXContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Token> Tokens { get; set; }
        public DbSet<BranchGroup> BranchGroups { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRoleFunctionAuthorization> UserRoleFunctionAuthorizations { get; set; }
        public DbSet<UserRoleFunctionAccess> UserRoleFunctionAccesses { get; set; }
        public DbSet<UserBranchGroupPrvlg> UserBranchGroupPrvlgs { get; set; }
        public DbSet<UserBranchPrvlg> UserBranchPrvlgs { get; set; }
        public DbSet<MenuFunctionGroup> MenuFunctionGroups { get; set; }
        public DbSet<MenuFunction> MenuFunctions { get; set; }
        public DbSet<MenuFunctionAction> MenuFunctionAction { get; set; }

        public DbSet<DocControl> DocControls { get; set; }
        public DbSet<DocIdRunning> DocIdRunnings { get; set; }

        public DbSet<AttachFileType> AttachFileTypes { get; set; }
        public DbSet<AttachFile> AttachFiles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        /********* BeSpoke Data **********/
        public DbSet<COLOR_OF_FONT_MAST> ColorFontMasts { get; set; }
        public DbSet<EmbMast> EmbMasts { get; set; }
        public DbSet<CatalogMast> CatalogMasts { get; set; }
        public DbSet<CatalogColor> CatalogColors { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            // Set Quantity Precision ===================================================

            //modelBuilder.Entity<Item>().Property(r => r.minimumStockQty).HasPrecision(18, 4);

            //=========================================================================


            // Create Index ===========================================================

            //=== Branch Group ========================================================
            modelBuilder.Entity<BranchGroup>()
                .HasIndex("IX_BranchGroup_BranchGroupCode",
                e => e.Property(x => x.branchGroupCode));

            modelBuilder.Entity<Branch>()
                .HasIndex("IX_Branch_BranchCode",
                e => e.Property(x => x.branchCode));

            modelBuilder.Entity<Department>()
                .HasIndex("IX_Department_DepartmentCode",
                e => e.Property(x => x.departmentCode));


            //=========================================================================

        }
    }
}