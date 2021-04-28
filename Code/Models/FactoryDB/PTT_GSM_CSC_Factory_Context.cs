using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PTT_GSM_CSC_Factory.Models.FactoryDB
{
    public partial class PTT_GSM_CSC_Factory_Context : DbContext
    {
        public PTT_GSM_CSC_Factory_Context()
        {
        }

        public PTT_GSM_CSC_Factory_Context(DbContextOptions<PTT_GSM_CSC_Factory_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<SAP_CUST_GENERAL_VIEW> SAP_CUST_GENERAL_VIEW { get; set; }
        public virtual DbSet<SAP_CUST_PARTNER_VIEW> SAP_CUST_PARTNER_VIEW { get; set; }
        public virtual DbSet<SAP_CUST_SALE_VIEW> SAP_CUST_SALE_VIEW { get; set; }
        public virtual DbSet<TH_District> TH_District { get; set; }
        public virtual DbSet<TH_Province> TH_Province { get; set; }
        public virtual DbSet<TH_Subdistrict> TH_Subdistrict { get; set; }
        public virtual DbSet<TM_AD_Connection> TM_AD_Connection { get; set; }
        public virtual DbSet<TM_CMS> TM_CMS { get; set; }
        public virtual DbSet<TM_ContactUs> TM_ContactUs { get; set; }
        public virtual DbSet<TM_Data> TM_Data { get; set; }
        public virtual DbSet<TM_DataType> TM_DataType { get; set; }
        public virtual DbSet<TM_Distributor> TM_Distributor { get; set; }
        public virtual DbSet<TM_Factory> TM_Factory { get; set; }
        public virtual DbSet<TM_FactoryProduct> TM_FactoryProduct { get; set; }
        public virtual DbSet<TM_News> TM_News { get; set; }
        public virtual DbSet<TM_Staff> TM_Staff { get; set; }
        public virtual DbSet<TM_User> TM_User { get; set; }
        public virtual DbSet<T_AD_Connection> T_AD_Connection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=27.254.172.163;User ID=cscfactoryusr; database=PTT_GSM_CSC_Factory;Password=ccscfactoryusr;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<SAP_CUST_GENERAL_VIEW>(entity =>
            {
                entity.HasKey(e => e.CUST_NUMBER)
                    .HasName("PK_SAP_CUST_GENERAL_VEIW");

                entity.Property(e => e.CUST_NUMBER)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CITY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CUST_NAME)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DIFFERENT_CITY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DISTRICT)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HOUSE_NUMBER)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.POSTAL_CODE)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SEARCH_TERM2)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.STREET4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.dUpdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SAP_CUST_PARTNER_VIEW>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BUS_PART_NO)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CUST_NAME)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CUST_NUMBER)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PARTNER_FUNC)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SEARCH_TERM2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.dUpdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SAP_CUST_SALE_VIEW>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CUST_GR)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.CUST_GR_DESC)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CUST_NAME)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CUST_NUMBER)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DIST_CHANNEL)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.DIVISION)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SALES_DISTRICT)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.SALES_DISTRICT_DESC)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SALES_GR)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SALES_OFFICE)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.dUpdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TH_District>(entity =>
            {
                entity.HasKey(e => e.sDistrictID)
                    .HasName("PK_TM_DISTRICT");

                entity.HasComment("ตารางเก็บเขต/อำเภอ");

                entity.Property(e => e.sDistrictID)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComment("รหัสอำเภอ/เขต");

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("สถานะการใช้งาน [Y]Active ,[N]Inactive");

                entity.Property(e => e.sDistrictName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("ชื่ออำเภอ/เขต");

                entity.Property(e => e.sProvinceID)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("รหัสจังหวัด");
            });

            modelBuilder.Entity<TH_Province>(entity =>
            {
                entity.HasKey(e => e.sProvinceID)
                    .HasName("PK_TM_PROVINCE");

                entity.HasComment("ตารางเก็บจังหวัด");

                entity.Property(e => e.sProvinceID)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("รหัสจังหวัด");

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("สถานะการใช้งาน [Y]Active ,[N]Inactive");

                entity.Property(e => e.sProvinceCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("ตัวย่อจังหวัด");

                entity.Property(e => e.sProvinceName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("ชื่อจังหวัด");
            });

            modelBuilder.Entity<TH_Subdistrict>(entity =>
            {
                entity.HasKey(e => e.sSubdistrictID)
                    .HasName("PK_TM_AREA");

                entity.HasComment("ตารางเก็บข้อมูลตำบล/แขวง");

                entity.Property(e => e.sSubdistrictID)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("รหัสแขวง/ตำบล");

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("สถานะการใช้งาน Y ใช้งาน ,N ไม่ใช้งาน");

                entity.Property(e => e.sDistrictID)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComment("รหัสอำเภอ/เขต");

                entity.Property(e => e.sProvinceID)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("รหัสจังหวัด");

                entity.Property(e => e.sSubdistrictName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("ชื่อเขต/ตำบล");

                entity.HasOne(d => d.sDistrict)
                    .WithMany(p => p.TH_Subdistrict)
                    .HasForeignKey(d => d.sDistrictID)
                    .HasConstraintName("FK_TM_AREA_REFERENCE_TM_DISTR");

                entity.HasOne(d => d.sProvince)
                    .WithMany(p => p.TH_Subdistrict)
                    .HasForeignKey(d => d.sProvinceID)
                    .HasConstraintName("FK_TM_AREA_REFERENCE_TM_PROVI");
            });

            modelBuilder.Entity<TM_AD_Connection>(entity =>
            {
                entity.HasKey(e => e.ServerName);

                entity.Property(e => e.ServerName).HasMaxLength(50);

                entity.Property(e => e.DC).HasMaxLength(500);

                entity.Property(e => e.Server).HasMaxLength(500);

                entity.Property(e => e.SubDomain).IsUnicode(false);

                entity.Property(e => e.Userdomain).HasMaxLength(50);

                entity.Property(e => e.ldap).HasMaxLength(50);
            });

            modelBuilder.Entity<TM_CMS>(entity =>
            {
                entity.HasKey(e => e.nMenuID);

                entity.Property(e => e.nMenuID).ValueGeneratedNever();

                entity.Property(e => e.cDel)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.cNewTab)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.cPRMS)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.dAddDate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sFullContentEN).IsUnicode(false);

                entity.Property(e => e.sFullContentTH).IsUnicode(false);

                entity.Property(e => e.sMenuNameEN)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.sMenuNameTH)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.sShortContentEN).IsUnicode(false);

                entity.Property(e => e.sShortContentTH).IsUnicode(false);

                entity.Property(e => e.sURL)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_ContactUs>(entity =>
            {
                entity.HasKey(e => e.nID)
                    .HasName("PK_TM_Suggestion");

                entity.Property(e => e.nID).ValueGeneratedNever();

                entity.Property(e => e.cReplied)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dAdd).HasColumnType("datetime");

                entity.Property(e => e.dUpdateBy).HasColumnType("datetime");

                entity.Property(e => e.sAdminResponse)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.sEmail)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sFileNameAdmin)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSubject)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysFileNameAdmin)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysFilePathAdmin)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sTel)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sUserDetail)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.sUserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Data>(entity =>
            {
                entity.HasKey(e => e.nDataID);

                entity.Property(e => e.nDataID).ValueGeneratedNever();

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.cCanDelete)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.cDel)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.cRequired)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.dAdd).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sDataEN)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sDataTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_DataType>(entity =>
            {
                entity.HasKey(e => e.nTypeID);

                entity.Property(e => e.nTypeID).ValueGeneratedNever();

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.cDel)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.cManage)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.sTypeNameEN)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sTypeNameTH)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Distributor>(entity =>
            {
                entity.HasKey(e => e.nDistributorID)
                    .HasName("PK_TM_PowerPantInfo");

                entity.Property(e => e.nDistributorID).ValueGeneratedNever();

                entity.Property(e => e.cDel)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cPTT)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dAdd).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nCustomerGroupID)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sABDistributorName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sAddressShipTo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sAddressSoldTo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sComapanyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sDetail)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.sDistributorCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sDistributorName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sDistrict)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sDistrictDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sEmail)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sLatitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sLogoName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sLogoPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sLongitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sMap).IsUnicode(false);

                entity.Property(e => e.sMeterName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sOrgChartName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sOrgChartPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sShipTo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sSoldTo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sSysLogoName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysOrgChartName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sTitleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sWebsite)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Factory>(entity =>
            {
                entity.HasKey(e => e.nFactoryID);

                entity.Property(e => e.nFactoryID).ValueGeneratedNever();

                entity.Property(e => e.cDel)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dAdd).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sAddressShipTo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sDetail)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.sDistributorName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sDistrict)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sDistrictDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sImageName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sLatitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sLogoName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sLogoPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sLongitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sMap).IsUnicode(false);

                entity.Property(e => e.sMeterName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sShipTo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sSysImageName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysLogoName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sTitleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_FactoryProduct>(entity =>
            {
                entity.HasKey(e => e.nID);

                entity.Property(e => e.nID).ValueGeneratedNever();

                entity.Property(e => e.cDel)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dAdd).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sMaterialCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_News>(entity =>
            {
                entity.HasKey(e => e.nNewsID);

                entity.Property(e => e.nNewsID).ValueGeneratedNever();

                entity.Property(e => e.cDel).HasMaxLength(1);

                entity.Property(e => e.cEndless)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cPin)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cStatus).HasMaxLength(1);

                entity.Property(e => e.dAdd).HasColumnType("datetime");

                entity.Property(e => e.dEndDate).HasColumnType("datetime");

                entity.Property(e => e.dStartDate).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sDetailEN).IsUnicode(false);

                entity.Property(e => e.sDetailTH).IsUnicode(false);

                entity.Property(e => e.sFileImageNameEN)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sFileImageNameTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sFileImagePathTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sFileThumbnailNameEN)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sFileThumbnailNameTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sHeadEN)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sHeadTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sShortDesEN)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.sShortDesTH)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.sSource)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysFileImageNameTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysFileThumbnailNameTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysImageFileNameEN)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sSysThumbnailFileNameEN)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sThumbnailPathTH)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Staff>(entity =>
            {
                entity.HasKey(e => e.nStaffID);

                entity.Property(e => e.sAvatarName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sAvatarPath)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.sAvatarSysFileName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sEmail)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.sPassword).IsUnicode(false);

                entity.Property(e => e.sUserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_User>(entity =>
            {
                entity.HasKey(e => e.nUserID);

                entity.Property(e => e.nUserID).ValueGeneratedNever();

                entity.Property(e => e.cDel).HasMaxLength(1);

                entity.Property(e => e.cStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dAdd).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dLastLogin).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sEmail).HasMaxLength(150);

                entity.Property(e => e.sFax).HasMaxLength(50);

                entity.Property(e => e.sFileName).HasMaxLength(50);

                entity.Property(e => e.sFirstNameEN).HasMaxLength(150);

                entity.Property(e => e.sFirstNameTH).HasMaxLength(150);

                entity.Property(e => e.sMobile)
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.Property(e => e.sPassword).HasMaxLength(200);

                entity.Property(e => e.sPathFile)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sPosition).HasMaxLength(200);

                entity.Property(e => e.sSurNameEN).HasMaxLength(150);

                entity.Property(e => e.sSurNameTH).HasMaxLength(150);

                entity.Property(e => e.sSysFileName).HasMaxLength(50);

                entity.Property(e => e.sTel).HasMaxLength(100);

                entity.Property(e => e.sUserCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sUsername)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_AD_Connection>(entity =>
            {
                entity.HasKey(e => e.ServerName);

                entity.Property(e => e.ServerName).HasMaxLength(50);

                entity.Property(e => e.DC).HasMaxLength(500);

                entity.Property(e => e.Server).HasMaxLength(500);

                entity.Property(e => e.SubDomain).IsUnicode(false);

                entity.Property(e => e.Userdomain).HasMaxLength(50);

                entity.Property(e => e.ldap).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
