using Microsoft.EntityFrameworkCore;

namespace Model.Entitys
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class TemplateContext : DbContext
    {
        public TemplateContext() { }
        public TemplateContext(DbContextOptions<TemplateContext> options) : base(options) { }

        // ******************************** //
        // 该地方将实体类 注册为数据库表    //
        // ******************************** //

        public DbSet<TUser> Users { get; set; }
        public DbSet<TUserPassWord> UserPassWords { get; set; }


        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //dotnet ef migrations add InitialCreate
            //dotnet ef migrations remove
            //dotnet ef database update
            if (!optionsBuilder.IsConfigured)
            {
                string ConnectionStr = "server=localhost;database=LemonDB;user=root;password=123456;";
                //optionsBuilder.UseMySql($"{AppConfig.Configs["PrjectConfig"]["ConnectionString"]}");
                optionsBuilder.UseMySql(ConnectionStr);
            }
        }

        /// <summary>
        /// 模型创建配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ************************************** //
            // 该地方将 Fluent API 配置 Code First    //
            // ************************************** //

            modelBuilder.Entity<TUser>(entity =>
            {
                //表名
                entity.ToTable("t_user");
                //主键
                entity.HasKey(o => o.Id);
                entity.Ignore(o => o.EncryptId);

                //id 设置为自增长
                entity.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

                entity.Property(o => o.OpenId)
                .HasColumnName("open_id")
                .HasMaxLength(200);

                entity.Property(o => o.UnionId)
                .HasColumnName("union_id")
                .HasMaxLength(200);

                entity.Property(o => o.AppId)
                .HasColumnName("app_id")
                .HasMaxLength(200);

                entity.Property(o => o.NickName)
                .HasColumnName("user_name")
                .HasMaxLength(200);

                entity.Property(o => o.Account)
                .HasColumnName("account")
                .HasMaxLength(200);

                entity.Property(o => o.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(200);

                entity.Property(o => o.Gender)
                .HasColumnName("gender")
                .HasColumnType("int(11)");

                entity.Property(o => o.City)
                .HasColumnName("city");

                entity.Property(o => o.Province)
                .HasColumnName("province");

                entity.Property(o => o.Country)
                .HasColumnName("country");

                entity.Property(o => o.AvatarUrl)
                .HasColumnName("avatar_url");

                entity.Property(o => o.Birthday)
                .HasColumnName("birthday");

                entity.Property(o => o.CreateTime)
                .HasColumnName("create_time");
            });
            //TUserPassWord 用户表配置模型
            modelBuilder.Entity<TUserPassWord>(entity =>
            {
                //表名
                entity.ToTable("t_user_password");

                //主键
                entity.HasKey(o => o.Id);
                entity.Ignore(o => o.EncryptId);

                //id 设置为自增长
                entity.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

                entity.Property(o => o.UserId)
                .HasColumnName("user_id");

                entity.Property(o => o.PassWord)
                .HasColumnName("password");

                entity.Property(o => o.State)
                .HasColumnName("state")
                .HasColumnType("int(11)");
            });
        }
    }
}
