using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MCE_Electronic_Orders.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommdivView> CommdivViews { get; set; }
        public virtual DbSet<MarketBranchDtl> MarketBranchDtls { get; set; }
        public virtual DbSet<MarketBranchHd> MarketBranchHds { get; set; }
        public virtual DbSet<OrdarsSerial> OrdarsSerials { get; set; }
        public virtual DbSet<OrdersLookupTable> OrdersLookupTables { get; set; }
        public virtual DbSet<OrdersMarket> OrdersMarkets { get; set; }
        public virtual DbSet<StoreView> StoreViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.30.15)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=mce)));Persist Security Info=True;User Id=COMMDIV;Password=COMMDIVPASS;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("COMMDIV");

            modelBuilder.Entity<CommdivView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("COMMDIV_VIEW");

                entity.Property(e => e.AggSbstr)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("AGG_SBSTR");

                entity.Property(e => e.AggStatus)
                    .HasColumnType("int")
                    .HasColumnName("AGG_STATUS");

                entity.Property(e => e.AgrmntCompCode)
                    .HasColumnType("int")
                    .HasColumnName("AGRMNT_COMP_CODE");

                entity.Property(e => e.AgrmntEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("AGRMNT_END_DATE");

                entity.Property(e => e.AgrmntNo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("AGRMNT_NO");

                entity.Property(e => e.AgrmntNote)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("AGRMNT_NOTE");

                entity.Property(e => e.AgrmntProfitRatio)
                    .HasColumnType("int")
                    .HasColumnName("AGRMNT_PROFIT_RATIO");

                entity.Property(e => e.AgrmntReciveType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("AGRMNT_RECIVE_TYPE");

                entity.Property(e => e.AgrmntStartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("AGRMNT_START_DATE");

                entity.Property(e => e.AgrmntTarget)
                    .HasColumnType("int")
                    .HasColumnName("AGRMNT_TARGET");

                entity.Property(e => e.AgrmntTypeCode)
                    .HasColumnType("int")
                    .HasColumnName("AGRMNT_TYPE_CODE");

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BARCODE");

                entity.Property(e => e.CompName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMP_NAME");

                entity.Property(e => e.CompStartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("COMP_START_DATE");

                entity.Property(e => e.CompStatus)
                    .HasColumnType("int")
                    .HasColumnName("COMP_STATUS");

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ITEM_CODE");

                entity.Property(e => e.ItemPurcahsePrice)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ITEM_PURCAHSE_PRICE");

                entity.Property(e => e.ItemSalesPrice)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ITEM_SALES_PRICE");

                entity.Property(e => e.ItmesStatus)
                    .HasColumnType("int")
                    .HasColumnName("ITMES_STATUS");
            });

            modelBuilder.Entity<MarketBranchDtl>(entity =>
            {
                entity.HasKey(e => new { e.OrderNo, e.ItemNo })
                    .HasName("MARKET_BRANCH_DTL_PK");

                entity.ToTable("MARKET_BRANCH_DTL");

                entity.Property(e => e.OrderNo)
                    .HasColumnType("INT")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDER_NO");

                entity.Property(e => e.ItemNo)
                    .HasColumnType("Int")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM_NO");

                entity.Property(e => e.BranchIdNo)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BRANCH_ID_NO");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("DATE")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CREATED_DATE");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CREATED_USER");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM_NAME");

                entity.Property(e => e.ItemPrice)
                    .HasColumnType("FLOAT")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM_PRICE");

                entity.Property(e => e.ItemQty)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM_QTY");

                entity.Property(e => e.ItemStatus)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM_STATUS");

                entity.Property(e => e.ItemTotalPrice)
                    .HasColumnType("FLOAT")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM_TOTAL_PRICE");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("DATE")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("MODIFIED_DATE");

                entity.Property(e => e.ModifiedUser)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("MODIFIED_USER");

                entity.HasOne(d => d.OrderNoNavigation)
                    .WithMany(p => p.MarketBranchDtls)
                    .HasForeignKey(d => d.OrderNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MARKET_BRANCH_DTL_FK");
            });

            modelBuilder.Entity<MarketBranchHd>(entity =>
            {
                entity.HasKey(e => e.OrderNo)
                    .HasName("MARKET_BRANCH_HD_PK");

                entity.ToTable("MARKET_BRANCH_HD");

                entity.Property(e => e.OrderNo)
                    .HasColumnType("int")
                    .HasColumnName("ORDER_NO");

                entity.Property(e => e.AggNo)
                    .HasColumnType("int")
                    .HasColumnName("AGG_NO");

                entity.Property(e => e.BranchNo)
                    .HasColumnType("int")
                    .HasColumnName("BRANCH_NO");

                entity.Property(e => e.CompName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("COMP_NAME");

                entity.Property(e => e.CompNo)
                    .HasColumnType("int")
                    .HasColumnName("COMP_NO");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_DATE");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("CREATED_USER");

                entity.Property(e => e.Flag)
                    .HasColumnType("int")
                    .HasColumnName("FLAG")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("DATE")
                    .HasColumnName("MODIFIED_DATE");

                entity.Property(e => e.ModifiedUser)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("MODIFIED_USER");

                entity.Property(e => e.ReceiptSer)
                    .HasColumnType("int")
                    .HasColumnName("RECEIPT_SER");

                entity.Property(e => e.RequsetDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REQUSET_DATE");

                entity.Property(e => e.Status)
                    .HasColumnType("INT")
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<OrdarsSerial>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ORDARS_SERIAL");

                entity.Property(e => e.SerUnit)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SER_UNIT");

                entity.Property(e => e.SerV2serial)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SER_V2SERIAL");

                entity.Property(e => e.SerV3serial)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SER_V3SERIAL");

                entity.Property(e => e.SerV4serial)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SER_V4SERIAL");

                entity.Property(e => e.SerV5serial)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SER_V5SERIAL");
            });

            modelBuilder.Entity<OrdersLookupTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ORDERS_LOOKUP_TABLE");

                entity.Property(e => e.Code)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CODE");

                entity.Property(e => e.Descertion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCERTION");

                entity.Property(e => e.Type)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<OrdersMarket>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ORDERS_MARKETS");

                entity.Property(e => e.ActiveFlag)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ACTIVE_FLAG");

                entity.Property(e => e.DbLink)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DB_LINK");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IP_ADDRESS");

                entity.Property(e => e.IsInvest)
                    .HasColumnType("NUMBER")
                    .HasColumnName("IS_INVEST");

                entity.Property(e => e.MarketId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MARKET_ID");

                entity.Property(e => e.MarketName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("MARKET_NAME");

                entity.Property(e => e.NielsenCode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NIELSEN_CODE");

                entity.Property(e => e.RegionId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REGION_ID");

                entity.Property(e => e.RegionName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("REGION_NAME");

                entity.Property(e => e.StoreCode)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STORE_CODE");

                entity.Property(e => e.TnsName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TNS_NAME");
            });

            modelBuilder.Entity<StoreView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("STORE_VIEW");

                entity.Property(e => e.Exdate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXDATE");

                entity.Property(e => e.MssCqty)
                    .HasPrecision(10)
                    .HasColumnName("MSS_CQTY");

                entity.Property(e => e.MssEnterDate)
                    .HasColumnType("DATE")
                    .HasColumnName("MSS_ENTER_DATE");

                entity.Property(e => e.MssExdate)
                    .HasColumnType("DATE")
                    .HasColumnName("MSS_EXDATE");

                entity.Property(e => e.MssFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MSS_FLAG");

                entity.Property(e => e.MssLinkno)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("MSS_LINKNO");

                entity.Property(e => e.MssOrder)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("MSS_ORDER");

                entity.Property(e => e.MssProd)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("MSS_PROD");

                entity.Property(e => e.MssQty)
                    .HasPrecision(10)
                    .HasColumnName("MSS_QTY");

                entity.Property(e => e.MssStat)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MSS_STAT");

                entity.Property(e => e.MssStdate)
                    .HasColumnType("DATE")
                    .HasColumnName("MSS_STDATE");

                entity.Property(e => e.MssStno)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("MSS_STNO");

                entity.Property(e => e.MssStop)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MSS_STOP");

                entity.Property(e => e.MssStore)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("MSS_STORE");

                entity.Property(e => e.MssTtyno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MSS_TTYNO");

                entity.Property(e => e.MssType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MSS_TYPE");
            });

            modelBuilder.HasSequence("AGREEMENTS_HISTORY_SRL_NO_SEQ");

            modelBuilder.HasSequence("DTL_SEQ");

            modelBuilder.HasSequence("ITEM_SRL_CODE_SEQ");

            modelBuilder.HasSequence("MCEPOS_ITEMS_SEQ");

            modelBuilder.HasSequence("OFFERS_SEQ");

            modelBuilder.HasSequence("PRICE_SEQ");

            modelBuilder.HasSequence("SEQ_ID");

            modelBuilder.HasSequence("SQ_AspNetRoleClaims");

            modelBuilder.HasSequence("SQ_AspNetUserClaims");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
