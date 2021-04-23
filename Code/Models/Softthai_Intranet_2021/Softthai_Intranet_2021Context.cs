using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
#nullable disable

namespace SoftthaiIntranet.Models.Softthai_Intranet_2021
{
    public partial class Softthai_Intranet_2021Context : DbContext
    {
        public Softthai_Intranet_2021Context()
        {
        }

        public Softthai_Intranet_2021Context(DbContextOptions<Softthai_Intranet_2021Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TM_CategoryKM> TM_CategoryKM { get; set; }
        public virtual DbSet<TM_Condition_AcceptWorkTime> TM_Condition_AcceptWorkTime { get; set; }
        public virtual DbSet<TM_Condition_Config> TM_Condition_Config { get; set; }
        public virtual DbSet<TM_Destination> TM_Destination { get; set; }
        public virtual DbSet<TM_Expense> TM_Expense { get; set; }
        public virtual DbSet<TM_Floor> TM_Floor { get; set; }
        public virtual DbSet<TM_FlowProcess> TM_FlowProcess { get; set; }
        public virtual DbSet<TM_Holiday> TM_Holiday { get; set; }
        public virtual DbSet<TM_Leave> TM_Leave { get; set; }
        public virtual DbSet<TM_Location> TM_Location { get; set; }
        public virtual DbSet<TM_MeetingRoom> TM_MeetingRoom { get; set; }
        public virtual DbSet<TM_Menu> TM_Menu { get; set; }
        public virtual DbSet<TM_OverTime> TM_OverTime { get; set; }
        public virtual DbSet<TM_Position> TM_Position { get; set; }
        public virtual DbSet<TM_Project> TM_Project { get; set; }
        public virtual DbSet<TM_ProjectMember> TM_ProjectMember { get; set; }
        public virtual DbSet<TM_ProjectProgress> TM_ProjectProgress { get; set; }
        public virtual DbSet<TM_ProjectType> TM_ProjectType { get; set; }
        public virtual DbSet<TM_RequestType> TM_RequestType { get; set; }
        public virtual DbSet<TM_Role> TM_Role { get; set; }
        public virtual DbSet<TM_TaskType> TM_TaskType { get; set; }
        public virtual DbSet<TM_Team> TM_Team { get; set; }
        public virtual DbSet<TM_Travel> TM_Travel { get; set; }
        public virtual DbSet<T_Emp_LeaveQuota> T_Emp_LeaveQuota { get; set; }
        public virtual DbSet<T_Emp_Token> T_Emp_Token { get; set; }
        public virtual DbSet<T_Employee> T_Employee { get; set; }
        public virtual DbSet<T_HR_Config> T_HR_Config { get; set; }
        public virtual DbSet<T_Leave> T_Leave { get; set; }
        public virtual DbSet<T_LogMail> T_LogMail { get; set; }
        public virtual DbSet<T_OT> T_OT { get; set; }
        public virtual DbSet<T_Request_AcceptWorkTime> T_Request_AcceptWorkTime { get; set; }
        public virtual DbSet<T_Request_AcceptWorkTime_Flow> T_Request_AcceptWorkTime_Flow { get; set; }
        public virtual DbSet<T_Request_Allowance> T_Request_Allowance { get; set; }
        public virtual DbSet<T_Request_Allowance_Flow> T_Request_Allowance_Flow { get; set; }
        public virtual DbSet<T_Request_Allowance_Item> T_Request_Allowance_Item { get; set; }
        public virtual DbSet<T_Request_Travel> T_Request_Travel { get; set; }
        public virtual DbSet<T_Request_Travel_Flow> T_Request_Travel_Flow { get; set; }
        public virtual DbSet<T_Request_Travel_Item> T_Request_Travel_Item { get; set; }
        public virtual DbSet<T_ReserveMeetingRoom> T_ReserveMeetingRoom { get; set; }
        public virtual DbSet<T_WorkingTime> T_WorkingTime { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Softthai_Intranet_2021_ConnectionStrings"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<TM_CategoryKM>(entity =>
            {
                entity.HasKey(e => e.nCategoryKMID)
                    .HasName("PK_TM_MasterData_Sub");

                entity.Property(e => e.nCategoryKMID).ValueGeneratedNever();

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nOrder).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.sCategoryName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.sDescription).HasMaxLength(2000);
            });

            modelBuilder.Entity<TM_Condition_AcceptWorkTime>(entity =>
            {
                entity.HasKey(e => e.nAwtConditionID)
                    .HasName("PK_TM_ACCEPTWORKTIME_CONDITION");

                entity.HasComment("ตารางเก็บเงื่อนไขเวลารับรองการทำงาน");

                entity.Property(e => e.nAwtConditionID).HasComment("รหัสตาราง");

                entity.Property(e => e.IsActive).HasComment("1 = Active , 0 = Not Active");

                entity.Property(e => e.IsDelete).HasComment("1 = Delete , 0 = Not Delete");

                entity.Property(e => e.dCreate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่สร้างรายการ");

                entity.Property(e => e.dDelete)
                    .HasColumnType("datetime")
                    .HasComment("วันที่ลบรายการ");

                entity.Property(e => e.dUpdate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่แก้ไขรายการ");

                entity.Property(e => e.nCertificateHour).HasComment("ชั่วโมงที่ได้รับรอง");

                entity.Property(e => e.nCertificateMinute).HasComment("นาทีที่ได้รับรอง");

                entity.Property(e => e.nConditionCertificateMinute).HasComment("เงื่อนไขที่ได้รับรอง");

                entity.Property(e => e.nConditionOverMinute).HasComment("เงื่อนไขที่นั่งเกิน");

                entity.Property(e => e.nCreate).HasComment("คนสร้างรายการ");

                entity.Property(e => e.nDelete).HasComment("คนที่ลบรายการ");

                entity.Property(e => e.nOrder).HasComment("ลำดับ");

                entity.Property(e => e.nOverHour).HasComment("ชั่วโมงที่นั่งเกิน");

                entity.Property(e => e.nOverMinute).HasComment("นาทีที่นั่งเกิน");

                entity.Property(e => e.nUpdate).HasComment("คนที่แก้ไขรายการ");

                entity.Property(e => e.nVersion).HasComment("เวอร์ชั่นที่มีการเปลี่ยนแปลง");

                entity.Property(e => e.sAwtConditionName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("หัวข้อกรณีนั่งเกินเวลา");

                entity.Property(e => e.sCertificateTitle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("หัวข้อได้รับรองการทำงาน");
            });

            modelBuilder.Entity<TM_Condition_Config>(entity =>
            {
                entity.HasKey(e => e.nConfigID)
                    .HasName("PK_TM_CONFIG_CONDITION");

                entity.HasComment("ตารางเก็บเงื่อนไขโดยรวม");

                entity.Property(e => e.nConfigID).HasComment("รหัสตาราง ");

                entity.Property(e => e.IsActive).HasComment("1 = Active , 0 = Not Active");

                entity.Property(e => e.dCreate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่สร้างรายการ");

                entity.Property(e => e.dUpdate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่แก้ไขรายการ");

                entity.Property(e => e.dValue).HasColumnType("datetime");

                entity.Property(e => e.nCreate).HasComment("คนสร้างรายการ");

                entity.Property(e => e.nUpdate).HasComment("คนที่แก้ไขรายการ");

                entity.Property(e => e.nValue).HasComment("ค่าที่เป็นตัวเลข");

                entity.Property(e => e.sDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("รายละเอียดของข้อมูล");

                entity.Property(e => e.sValue)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("ค่าที่เป็นตัวอักษร");
            });

            modelBuilder.Entity<TM_Destination>(entity =>
            {
                entity.HasKey(e => e.nDestinationID)
                    .HasName("PK_TM_DESTINATION");

                entity.HasComment("ตารางประเภทจุดหมายปลายทาง");

                entity.Property(e => e.nDestinationID).HasComment("รหัสตาราง ");

                entity.Property(e => e.IsActive).HasComment("1 = Active , 0 = Not Active");

                entity.Property(e => e.IsDelete).HasComment("1 = Delete , 0 = Not Delete");

                entity.Property(e => e.dCreate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่สร้างรายการ");

                entity.Property(e => e.dDelete)
                    .HasColumnType("datetime")
                    .HasComment("วันที่ลบรายการ");

                entity.Property(e => e.dUpdate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่แก้ไขรายการ");

                entity.Property(e => e.nCreate).HasComment("คนสร้างรายการ");

                entity.Property(e => e.nDelete).HasComment("คนที่ลยรายการ");

                entity.Property(e => e.nUpdate).HasComment("คนที่แก้ไขรายการ");

                entity.Property(e => e.sDestinationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("ชื่อตำแหน่ง");
            });

            modelBuilder.Entity<TM_Expense>(entity =>
            {
                entity.HasKey(e => e.nExpenseID)
                    .HasName("PK_TM_EXPENSE");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.sExpenseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Floor>(entity =>
            {
                entity.HasKey(e => e.nFloorID)
                    .HasName("PK_TM_FLOOR");

                entity.HasComment("ตารางเก็บชั้น");

                entity.Property(e => e.nFloorID).HasComment("รหัสตาราง ");

                entity.Property(e => e.IsActive).HasComment("1 = Active , 0 = Not Active");

                entity.Property(e => e.IsDelete).HasComment("1 = Delete , 0 = Not Delete");

                entity.Property(e => e.dCreate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่สร้างรายการ");

                entity.Property(e => e.dDelete)
                    .HasColumnType("datetime")
                    .HasComment("วันที่ลบรายการ");

                entity.Property(e => e.dUpdate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่แก้ไขรายการ");

                entity.Property(e => e.nCreate).HasComment("คนสร้างรายการ");

                entity.Property(e => e.nDelete).HasComment("คนที่ลยรายการ");

                entity.Property(e => e.nOrder).HasComment("ลำดับ");

                entity.Property(e => e.nUpdate).HasComment("คนที่แก้ไขรายการ");

                entity.Property(e => e.sFloorName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("ชื่อตำแหน่ง");
            });

            modelBuilder.Entity<TM_FlowProcess>(entity =>
            {
                entity.HasKey(e => e.nFlowProcessID)
                    .HasName("PK_TM_FLOWPROCESS");

                entity.Property(e => e.nFlowProcessID).ValueGeneratedNever();

                entity.Property(e => e.sFlowProcessName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Holiday>(entity =>
            {
                entity.HasKey(e => e.nHolidayID)
                    .HasName("PK_TM_HOLIDAY");

                entity.HasComment("ตารางเก็บวันหยุด");

                entity.Property(e => e.nHolidayID).HasComment("รหัสตาราง ");

                entity.Property(e => e.IsActive).HasComment("1 = Active , 0 = Not Active");

                entity.Property(e => e.IsDelete).HasComment("1 = Delete , 0 = Not Delete");

                entity.Property(e => e.IsSubstitutionDay).HasComment("1 = วันหยุดชดเฉย , 0 = วันหยุดนักขัตฤกษ์");

                entity.Property(e => e.dCreate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่สร้างรายการ");

                entity.Property(e => e.dDelete)
                    .HasColumnType("datetime")
                    .HasComment("วันที่ลบรายการ");

                entity.Property(e => e.dHolidayDate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่หยุด");

                entity.Property(e => e.dUpdate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่แก้ไขรายการ");

                entity.Property(e => e.nCreate).HasComment("คนสร้างรายการ");

                entity.Property(e => e.nDelete).HasComment("คนที่ลยรายการ");

                entity.Property(e => e.nUpdate).HasComment("คนที่แก้ไขรายการ");

                entity.Property(e => e.sHolidayName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("ชื่อการลา");
            });

            modelBuilder.Entity<TM_Leave>(entity =>
            {
                entity.HasKey(e => e.nLeaveID)
                    .HasName("PK_TM_LEAVE");

                entity.HasComment("ตารางประเภทการลา");

                entity.Property(e => e.nLeaveID).HasComment("รหัสตาราง ");

                entity.Property(e => e.IsActive).HasComment("1 = Active , 0 = Not Active");

                entity.Property(e => e.IsDelete).HasComment("1 = Delete , 0 = Not Delete");

                entity.Property(e => e.dCreate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่สร้างรายการ");

                entity.Property(e => e.dDelete)
                    .HasColumnType("datetime")
                    .HasComment("วันที่ลบรายการ");

                entity.Property(e => e.dUpdate)
                    .HasColumnType("datetime")
                    .HasComment("วันที่แก้ไขรายการ");

                entity.Property(e => e.nCreate).HasComment("คนสร้างรายการ");

                entity.Property(e => e.nDay).HasComment("จำนวนวัน");

                entity.Property(e => e.nDelete).HasComment("คนที่ลยรายการ");

                entity.Property(e => e.nOrder).HasComment("ลำดับ");

                entity.Property(e => e.nUpdate).HasComment("คนที่แก้ไขรายการ");

                entity.Property(e => e.sLeaveName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("ชื่อตำแหน่ง");
            });

            modelBuilder.Entity<TM_Location>(entity =>
            {
                entity.HasKey(e => e.nLocationID)
                    .HasName("PK_TM_LOCATION");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sLocationName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_MeetingRoom>(entity =>
            {
                entity.HasKey(e => e.nMeetingRoomID)
                    .HasName("PK_TM_MEETINGROOM");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sMeetingRoomName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Menu>(entity =>
            {
                entity.HasKey(e => e.nMenuID)
                    .HasName("PK_TM_MENU");

                entity.Property(e => e.sIcon)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.sMenuName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sRounter)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_OverTime>(entity =>
            {
                entity.HasKey(e => e.nOverTimeID)
                    .HasName("PK_TM_OverTime_1");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nRate).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.sOverTimeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Position>(entity =>
            {
                entity.HasKey(e => e.nPositionID)
                    .HasName("PK_TM_POSITION");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sPositionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Project>(entity =>
            {
                entity.HasKey(e => e.nProjectID)
                    .HasName("PK_TM_PROJECT");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dEndDate).HasColumnType("datetime");

                entity.Property(e => e.dStartDate).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sProjectAbbr)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.sProjectName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_ProjectMember>(entity =>
            {
                entity.HasKey(e => new { e.nMemberID, e.nProjectID })
                    .HasName("PK_TM_PROJECTMEMBER");

                entity.Property(e => e.nMemberID).ValueGeneratedOnAdd();

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TM_ProjectProgress>(entity =>
            {
                entity.HasKey(e => e.nProjectProgressID);

                entity.Property(e => e.nProjectProgressID).ValueGeneratedNever();

                entity.Property(e => e.cStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.sProjectProgressName).HasMaxLength(100);
            });

            modelBuilder.Entity<TM_ProjectType>(entity =>
            {
                entity.HasKey(e => e.nProjectTypeID)
                    .HasName("PK_TM_PROJECTTYPE");

                entity.HasComment("ตารางเก็บ Project  Master Type");

                entity.Property(e => e.nProjectTypeID).HasComment("รหัสตาราง");

                entity.Property(e => e.IsActive).HasComment("1 = Active , 0 = Not Active");

                entity.Property(e => e.nOrder).HasComment("ลำดับ");

                entity.Property(e => e.sProjectTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("ชื่อ");
            });

            modelBuilder.Entity<TM_RequestType>(entity =>
            {
                entity.HasKey(e => e.nRequestTypeID)
                    .HasName("PK_TM_REQUESTTYPE");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sRequestTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Role>(entity =>
            {
                entity.HasKey(e => e.nRoleID)
                    .HasName("PK_TM_ROLE");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sRoleAbbr)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.sRoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_TaskType>(entity =>
            {
                entity.HasKey(e => e.nTaskTypeID);

                entity.Property(e => e.nTaskTypeID).ValueGeneratedNever();

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nOrder).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.sTaskTypeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Team>(entity =>
            {
                entity.HasKey(e => e.nTeamID)
                    .HasName("PK_TM_TEAM");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sTeamAbbr)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sTeamName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TM_Travel>(entity =>
            {
                entity.HasKey(e => e.nTravelID)
                    .HasName("PK_TM_TRAVEL");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nPricePerKilometer).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.sTravelName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_Emp_LeaveQuota>(entity =>
            {
                entity.HasKey(e => new { e.nEmployeeID, e.nLeaveID, e.nYear });
            });

            modelBuilder.Entity<T_Emp_Token>(entity =>
            {
                entity.HasKey(e => e.nEmpTokenID);

                entity.Property(e => e.nEmpTokenID).ValueGeneratedNever();

                entity.Property(e => e.cActive)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.dLastLogin).HasColumnType("datetime");

                entity.Property(e => e.sPlatform)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sSessionID).HasMaxLength(50);
            });

            modelBuilder.Entity<T_Employee>(entity =>
            {
                entity.HasKey(e => e.nEmployeeID)
                    .HasName("PK_TB_Employee");

                entity.Property(e => e.dCreate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.dDisplacement).HasColumnType("date");

                entity.Property(e => e.dStartWork)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.dUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.sAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.sContactMobile)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.sEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sEmployeeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sFileName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.sFirstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sGender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.sLastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sLineID)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.sMobile)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sNickname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.sPassword).IsUnicode(false);

                entity.Property(e => e.sPath)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.sSysFileName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.sUsername)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_HR_Config>(entity =>
            {
                entity.HasKey(e => e.nEmployeeID)
                    .HasName("PK_TM_HR_CONFIG");

                entity.Property(e => e.nEmployeeID).ValueGeneratedNever();

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<T_Leave>(entity =>
            {
                entity.HasKey(e => e.nLeaveID);

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dEnd).HasColumnType("datetime");

                entity.Property(e => e.dStart).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sRemark).IsUnicode(false);
            });

            modelBuilder.Entity<T_LogMail>(entity =>
            {
                entity.HasKey(e => e.nLogMailID)
                    .HasName("PK_T_LOGMAIL");

                entity.Property(e => e.dSend).HasColumnType("datetime");

                entity.Property(e => e.sBCC).IsUnicode(false);

                entity.Property(e => e.sCC).IsUnicode(false);

                entity.Property(e => e.sContent).IsUnicode(false);

                entity.Property(e => e.sDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.sErrorMessage).IsUnicode(false);

                entity.Property(e => e.sFrom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.sStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.sSubject)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.sTo).IsUnicode(false);
            });

            modelBuilder.Entity<T_OT>(entity =>
            {
                entity.HasKey(e => e.nOTID);

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDate).HasColumnType("date");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nActual).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.nPlan).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.sCause).IsUnicode(false);

                entity.Property(e => e.sDetail).IsUnicode(false);
            });

            modelBuilder.Entity<T_Request_AcceptWorkTime>(entity =>
            {
                entity.HasKey(e => e.nAcceptWorkTimeID)
                    .HasName("PK_T_REQUEST_ACCEPTWORKTIME");

                entity.Property(e => e.dCertification).HasColumnType("datetime");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dEndOverTime).HasColumnType("datetime");

                entity.Property(e => e.dRequest).HasColumnType("datetime");

                entity.Property(e => e.dStartOverTime).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sDetailOfWork)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sEndTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.sStartTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_Request_AcceptWorkTime_Flow>(entity =>
            {
                entity.HasKey(e => e.nFlowID)
                    .HasName("PK_T_REQUEST_ACCEPTWORKTIME_FL");

                entity.Property(e => e.cApprove)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dApprove).HasColumnType("datetime");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sTypeFlow)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.nAcceptWorkTime)
                    .WithMany(p => p.T_Request_AcceptWorkTime_Flow)
                    .HasForeignKey(d => d.nAcceptWorkTimeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Request_AcceptWorkTime_Flow");
            });

            modelBuilder.Entity<T_Request_Allowance>(entity =>
            {
                entity.HasKey(e => e.nAllowanceID)
                    .HasName("PK_T_REQUEST_ALLOWANCE");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dMeetingEnd).HasColumnType("datetime");

                entity.Property(e => e.dMeetingStart).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sMeetingDetail)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_Request_Allowance_Flow>(entity =>
            {
                entity.HasKey(e => e.nFlowID)
                    .HasName("PK_T_REQUEST_ALLOWANCE_FLOW");

                entity.Property(e => e.cApprove)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dApprove).HasColumnType("datetime");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sTypeFlow)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.nAllowance)
                    .WithMany(p => p.T_Request_Allowance_Flow)
                    .HasForeignKey(d => d.nAllowanceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Request_Allowance_Flow");
            });

            modelBuilder.Entity<T_Request_Allowance_Item>(entity =>
            {
                entity.HasKey(e => new { e.nItemAllowanceID, e.nAllowanceID })
                    .HasName("PK_T_REQUEST_ALLOWANCE_ITEM");

                entity.Property(e => e.nItemAllowanceID).ValueGeneratedOnAdd();

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dMeetingt).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.nAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.sEndTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.sStartTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.nAllowance)
                    .WithMany(p => p.T_Request_Allowance_Item)
                    .HasForeignKey(d => d.nAllowanceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Request_Allowance_Item");
            });

            modelBuilder.Entity<T_Request_Travel>(entity =>
            {
                entity.HasKey(e => e.nReqTravelID)
                    .HasName("PK_T_REQUEST_TRAVEL");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dRequest).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<T_Request_Travel_Flow>(entity =>
            {
                entity.HasKey(e => new { e.nReqTravelID, e.nFlowID })
                    .HasName("PK_T_REQUEST_TRAVEL_FLOW");

                entity.Property(e => e.cApprove)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.dApprove).HasColumnType("datetime");

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sTypeFlow)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<T_Request_Travel_Item>(entity =>
            {
                entity.HasKey(e => new { e.nReqTravelID, e.nItemNo })
                    .HasName("PK_T_REQUEST_TRAVEL_ITEM");

                entity.Property(e => e.dTravelDate).HasColumnType("datetime");

                entity.Property(e => e.nAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.nDistance).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.nPriceKilometer).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.nTollway).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<T_ReserveMeetingRoom>(entity =>
            {
                entity.HasKey(e => e.nReserveID);

                entity.Property(e => e.nReserveID).ValueGeneratedNever();

                entity.Property(e => e.dCreate).HasColumnType("datetime");

                entity.Property(e => e.dDateMeeting).HasColumnType("datetime");

                entity.Property(e => e.dDelete).HasColumnType("datetime");

                entity.Property(e => e.dUpdate).HasColumnType("datetime");

                entity.Property(e => e.sEndTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.sMeetingDetail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.sStartTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_WorkingTime>(entity =>
            {
                entity.HasKey(e => e.nAttendID)
                    .HasName("PK_T_WORKINGTIME");

                entity.Property(e => e.dDateWork).HasColumnType("date");

                entity.Property(e => e.dSignIn).HasColumnType("datetime");

                entity.Property(e => e.dSignOut).HasColumnType("datetime");

                entity.Property(e => e.nAttendType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.nLeaveID).HasComment("รหัสตาราง ");

                entity.Property(e => e.sEmployeeCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.sRemark)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
