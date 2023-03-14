using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wayPointLatest.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminLogin",
                columns: table => new
                {
                    AdminLoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemRoll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkypeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TryCount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminLogin", x => x.AdminLoginId);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    campaignname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sourcename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    list = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    list_id = table.Column<int>(type: "int", nullable: true),
                    customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    auth_key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalleads = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostponeDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deliverythrottle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    leadperday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    validresponses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exclusivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deliverymethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rejectedtoday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    asignedtoday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalasign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalremaining = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datetime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    todayremaining = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    espa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    espb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    espc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    espd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    espe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Labels = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "forccgcosmolead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datecreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    auth_key = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forccgcosmolead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    auth_key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sendoncosmolead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datecreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    auth_key = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sendoncosmolead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "source",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    auth_key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datecreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dupe_check = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dupcount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dupcounttoday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalleads = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sourcecompanyname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    asignto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sourcelead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    source_id = table.Column<int>(type: "int", nullable: true),
                    zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    optin_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timezone_dst_flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    added_source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lu_carrier_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lu_international_format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lu_local_format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lu_carrier_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lu_carrier_error_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    list = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datecreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    auth_key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    asignto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    archieve = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sourcelead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sourcelist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    campaignname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sourcename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    list = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sourcelist", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminLogin");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "forccgcosmolead");

            migrationBuilder.DropTable(
                name: "provider");

            migrationBuilder.DropTable(
                name: "sendoncosmolead");

            migrationBuilder.DropTable(
                name: "source");

            migrationBuilder.DropTable(
                name: "sourcelead");

            migrationBuilder.DropTable(
                name: "sourcelist");
        }
    }
}
