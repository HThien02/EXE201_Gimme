using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXE.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumberOfPage",
                columns: table => new
                {
                    numberID = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    number = table.Column<int>(type: "int", nullable: true),
                    money = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfPage", x => x.numberID);
                });

            migrationBuilder.CreateTable(
                name: "paper",
                columns: table => new
                {
                    paper_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    money = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paper", x => x.paper_id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    size_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    money = table.Column<decimal>(type: "money", nullable: true),
                    ratio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.size_id);
                });

            migrationBuilder.CreateTable(
                name: "spring",
                columns: table => new
                {
                    Spring_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    money = table.Column<decimal>(type: "money", nullable: true),
                    color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spring", x => x.Spring_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    avatar = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    facebook_id = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    google_id = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    role = table.Column<int>(type: "int", nullable: true),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    gmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "money", nullable: true),
                    payment_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    payment_method = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    transaction_id = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_Payments_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    describe = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Size_id = table.Column<int>(type: "int", nullable: true),
                    paper_id = table.Column<int>(type: "int", nullable: true),
                    spring_id = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<decimal>(type: "money", nullable: true),
                    numberID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_Projects_Size",
                        column: x => x.Size_id,
                        principalTable: "Size",
                        principalColumn: "size_id");
                    table.ForeignKey(
                        name: "FK_Projects_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Projects_paper",
                        column: x => x.paper_id,
                        principalTable: "paper",
                        principalColumn: "paper_id");
                    table.ForeignKey(
                        name: "FK_Projects_spring",
                        column: x => x.spring_id,
                        principalTable: "spring",
                        principalColumn: "Spring_id");
                    table.ForeignKey(
                        name: "FK_users_numberID",
                        column: x => x.numberID,
                        principalTable: "NumberOfPage",
                        principalColumn: "numberID");
                });

            migrationBuilder.CreateTable(
                name: "User_Sessions",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    access_token = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    expire_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Sessions", x => x.session_id);
                    table.ForeignKey(
                        name: "FK_User_Sessions_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_user_id",
                table: "Payments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_numberID",
                table: "Projects",
                column: "numberID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_paper_id",
                table: "Projects",
                column: "paper_id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Size_id",
                table: "Projects",
                column: "Size_id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_spring_id",
                table: "Projects",
                column: "spring_id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_user_id",
                table: "Projects",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Sessions_user_id",
                table: "User_Sessions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "User_Sessions");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "paper");

            migrationBuilder.DropTable(
                name: "spring");

            migrationBuilder.DropTable(
                name: "NumberOfPage");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
