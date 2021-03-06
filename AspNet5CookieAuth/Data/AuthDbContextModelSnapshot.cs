// <auto-generated />
using AspNet5CookieAuth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspNet5CookieAuth.Data
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("AspNet5CookieAuth.Data.AppUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Mobile")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("NameIdentifier")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Provider")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Roles")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "tyaa@ukr.net",
                            Firstname = "Bill",
                            Lastname = "Gates",
                            Mobile = "911-00-00-000",
                            Password = "1",
                            Provider = "Cookies",
                            Roles = "Admin",
                            Username = "Bill"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
