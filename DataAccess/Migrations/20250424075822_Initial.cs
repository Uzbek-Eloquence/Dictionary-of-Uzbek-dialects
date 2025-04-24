using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "dialect",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialect", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeeches",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeeches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfInflectionalAffixes",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfInflectionalAffixes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    password = table.Column<string>(type: "TEXT", nullable: true),
                    phone = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "derivational_affixes",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(16)", nullable: false),
                    IsSuffix = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstPartOfSpeachId = table.Column<long>(type: "INTEGER", nullable: false),
                    LastPartOfSpeachId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_derivational_affixes", x => x.id);
                    table.ForeignKey(
                        name: "FK_derivational_affixes_PartOfSpeeches_FirstPartOfSpeachId",
                        column: x => x.FirstPartOfSpeachId,
                        principalTable: "PartOfSpeeches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_derivational_affixes_PartOfSpeeches_LastPartOfSpeachId",
                        column: x => x.LastPartOfSpeachId,
                        principalTable: "PartOfSpeeches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiteraryWords",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(256)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false),
                    PartOfSpeechId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteraryWords", x => x.id);
                    table.ForeignKey(
                        name: "FK_LiteraryWords_PartOfSpeeches_PartOfSpeechId",
                        column: x => x.PartOfSpeechId,
                        principalTable: "PartOfSpeeches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InflectionalAffixes",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(16)", nullable: false),
                    IsSuffix = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstPartOfSpeachId = table.Column<long>(type: "INTEGER", nullable: false),
                    TypesOfInflectionalAffixesId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InflectionalAffixes", x => x.id);
                    table.ForeignKey(
                        name: "FK_InflectionalAffixes_PartOfSpeeches_FirstPartOfSpeachId",
                        column: x => x.FirstPartOfSpeachId,
                        principalTable: "PartOfSpeeches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InflectionalAffixes_TypeOfInflectionalAffixes_TypesOfInflectionalAffixesId",
                        column: x => x.TypesOfInflectionalAffixesId,
                        principalTable: "TypeOfInflectionalAffixes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<long>(type: "INTEGER", nullable: false),
                    code = table.Column<string>(type: "TEXT", nullable: false),
                    expire_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_sessions_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dialectal_derivational_affix",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(16)", nullable: false),
                    IsSuffix = table.Column<bool>(type: "INTEGER", nullable: false),
                    DerivationalAffixesId = table.Column<long>(type: "INTEGER", nullable: false),
                    DialectsId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialectal_derivational_affix", x => x.id);
                    table.ForeignKey(
                        name: "FK_dialectal_derivational_affix_derivational_affixes_DerivationalAffixesId",
                        column: x => x.DerivationalAffixesId,
                        principalSchema: "public",
                        principalTable: "derivational_affixes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dialectal_derivational_affix_dialect_DialectsId",
                        column: x => x.DialectsId,
                        principalSchema: "public",
                        principalTable: "dialect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dialectal_word",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(16)", nullable: false),
                    LiteraryWordsId = table.Column<long>(type: "INTEGER", nullable: false),
                    DialectsId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialectal_word", x => x.id);
                    table.ForeignKey(
                        name: "FK_dialectal_word_LiteraryWords_LiteraryWordsId",
                        column: x => x.LiteraryWordsId,
                        principalTable: "LiteraryWords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dialectal_word_dialect_DialectsId",
                        column: x => x.DialectsId,
                        principalSchema: "public",
                        principalTable: "dialect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dialectal_inflectional_affix",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(16)", nullable: false),
                    IsSuffix = table.Column<bool>(type: "INTEGER", nullable: false),
                    InflectionalAffixesId = table.Column<long>(type: "INTEGER", nullable: false),
                    DialectsId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialectal_inflectional_affix", x => x.id);
                    table.ForeignKey(
                        name: "FK_dialectal_inflectional_affix_InflectionalAffixes_InflectionalAffixesId",
                        column: x => x.InflectionalAffixesId,
                        principalTable: "InflectionalAffixes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dialectal_inflectional_affix_dialect_DialectsId",
                        column: x => x.DialectsId,
                        principalSchema: "public",
                        principalTable: "dialect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_derivational_affixes_FirstPartOfSpeachId",
                schema: "public",
                table: "derivational_affixes",
                column: "FirstPartOfSpeachId");

            migrationBuilder.CreateIndex(
                name: "IX_derivational_affixes_LastPartOfSpeachId",
                schema: "public",
                table: "derivational_affixes",
                column: "LastPartOfSpeachId");

            migrationBuilder.CreateIndex(
                name: "IX_dialectal_derivational_affix_DerivationalAffixesId",
                schema: "public",
                table: "dialectal_derivational_affix",
                column: "DerivationalAffixesId");

            migrationBuilder.CreateIndex(
                name: "IX_dialectal_derivational_affix_DialectsId",
                schema: "public",
                table: "dialectal_derivational_affix",
                column: "DialectsId");

            migrationBuilder.CreateIndex(
                name: "IX_dialectal_inflectional_affix_DialectsId",
                schema: "public",
                table: "dialectal_inflectional_affix",
                column: "DialectsId");

            migrationBuilder.CreateIndex(
                name: "IX_dialectal_inflectional_affix_InflectionalAffixesId",
                schema: "public",
                table: "dialectal_inflectional_affix",
                column: "InflectionalAffixesId");

            migrationBuilder.CreateIndex(
                name: "IX_dialectal_word_DialectsId",
                schema: "public",
                table: "dialectal_word",
                column: "DialectsId");

            migrationBuilder.CreateIndex(
                name: "IX_dialectal_word_LiteraryWordsId",
                schema: "public",
                table: "dialectal_word",
                column: "LiteraryWordsId");

            migrationBuilder.CreateIndex(
                name: "IX_InflectionalAffixes_FirstPartOfSpeachId",
                table: "InflectionalAffixes",
                column: "FirstPartOfSpeachId");

            migrationBuilder.CreateIndex(
                name: "IX_InflectionalAffixes_TypesOfInflectionalAffixesId",
                table: "InflectionalAffixes",
                column: "TypesOfInflectionalAffixesId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteraryWords_PartOfSpeechId",
                table: "LiteraryWords",
                column: "PartOfSpeechId");

            migrationBuilder.CreateIndex(
                name: "IX_sessions_user_id",
                schema: "auth",
                table: "sessions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dialectal_derivational_affix",
                schema: "public");

            migrationBuilder.DropTable(
                name: "dialectal_inflectional_affix",
                schema: "public");

            migrationBuilder.DropTable(
                name: "dialectal_word",
                schema: "public");

            migrationBuilder.DropTable(
                name: "sessions",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "derivational_affixes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "InflectionalAffixes");

            migrationBuilder.DropTable(
                name: "LiteraryWords");

            migrationBuilder.DropTable(
                name: "dialect",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "TypeOfInflectionalAffixes");

            migrationBuilder.DropTable(
                name: "PartOfSpeeches");
        }
    }
}
