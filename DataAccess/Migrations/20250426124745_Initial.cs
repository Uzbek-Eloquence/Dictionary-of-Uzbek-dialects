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
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 536, DateTimeKind.Local).AddTicks(1128)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialect", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part_of_speech",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 537, DateTimeKind.Local).AddTicks(3343)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_of_speech", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_of_inflectional_affix",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 537, DateTimeKind.Local).AddTicks(5173)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_of_inflectional_affix", x => x.id);
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
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 533, DateTimeKind.Local).AddTicks(3916)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_derivational_affixes", x => x.id);
                    table.ForeignKey(
                        name: "FK_derivational_affixes_part_of_speech_FirstPartOfSpeachId",
                        column: x => x.FirstPartOfSpeachId,
                        principalSchema: "public",
                        principalTable: "part_of_speech",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_derivational_affixes_part_of_speech_LastPartOfSpeachId",
                        column: x => x.LastPartOfSpeachId,
                        principalSchema: "public",
                        principalTable: "part_of_speech",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "literary_word",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(256)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false),
                    PartOfSpeechId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 537, DateTimeKind.Local).AddTicks(1283)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_literary_word", x => x.id);
                    table.ForeignKey(
                        name: "FK_literary_word_part_of_speech_PartOfSpeechId",
                        column: x => x.PartOfSpeechId,
                        principalSchema: "public",
                        principalTable: "part_of_speech",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inflectional_affix",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(16)", nullable: false),
                    IsSuffix = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstPartOfSpeachId = table.Column<long>(type: "INTEGER", nullable: false),
                    TypesOfInflectionalAffixesId = table.Column<long>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 536, DateTimeKind.Local).AddTicks(9361)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inflectional_affix", x => x.id);
                    table.ForeignKey(
                        name: "FK_inflectional_affix_part_of_speech_FirstPartOfSpeachId",
                        column: x => x.FirstPartOfSpeachId,
                        principalSchema: "public",
                        principalTable: "part_of_speech",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inflectional_affix_type_of_inflectional_affix_TypesOfInflectionalAffixesId",
                        column: x => x.TypesOfInflectionalAffixesId,
                        principalSchema: "public",
                        principalTable: "type_of_inflectional_affix",
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
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 536, DateTimeKind.Local).AddTicks(3258)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
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
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 536, DateTimeKind.Local).AddTicks(7299)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialectal_word", x => x.id);
                    table.ForeignKey(
                        name: "FK_dialectal_word_dialect_DialectsId",
                        column: x => x.DialectsId,
                        principalSchema: "public",
                        principalTable: "dialect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dialectal_word_literary_word_LiteraryWordsId",
                        column: x => x.LiteraryWordsId,
                        principalSchema: "public",
                        principalTable: "literary_word",
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
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2025, 4, 26, 17, 47, 43, 536, DateTimeKind.Local).AddTicks(5253)),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    status = table.Column<byte>(type: "INTEGER", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialectal_inflectional_affix", x => x.id);
                    table.ForeignKey(
                        name: "FK_dialectal_inflectional_affix_dialect_DialectsId",
                        column: x => x.DialectsId,
                        principalSchema: "public",
                        principalTable: "dialect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dialectal_inflectional_affix_inflectional_affix_InflectionalAffixesId",
                        column: x => x.InflectionalAffixesId,
                        principalSchema: "public",
                        principalTable: "inflectional_affix",
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
                name: "IX_derivational_affixes_Title",
                schema: "public",
                table: "derivational_affixes",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_dialect_Title",
                schema: "public",
                table: "dialect",
                column: "Title");

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
                name: "IX_dialectal_derivational_affix_Title",
                schema: "public",
                table: "dialectal_derivational_affix",
                column: "Title");

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
                name: "IX_dialectal_inflectional_affix_Title",
                schema: "public",
                table: "dialectal_inflectional_affix",
                column: "Title");

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
                name: "IX_dialectal_word_Title",
                schema: "public",
                table: "dialectal_word",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_inflectional_affix_FirstPartOfSpeachId",
                schema: "public",
                table: "inflectional_affix",
                column: "FirstPartOfSpeachId");

            migrationBuilder.CreateIndex(
                name: "IX_inflectional_affix_Title",
                schema: "public",
                table: "inflectional_affix",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_inflectional_affix_TypesOfInflectionalAffixesId",
                schema: "public",
                table: "inflectional_affix",
                column: "TypesOfInflectionalAffixesId");

            migrationBuilder.CreateIndex(
                name: "IX_literary_word_PartOfSpeechId",
                schema: "public",
                table: "literary_word",
                column: "PartOfSpeechId");

            migrationBuilder.CreateIndex(
                name: "IX_literary_word_Title",
                schema: "public",
                table: "literary_word",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_part_of_speech_Title",
                schema: "public",
                table: "part_of_speech",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_sessions_user_id",
                schema: "auth",
                table: "sessions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_type_of_inflectional_affix_Title",
                schema: "public",
                table: "type_of_inflectional_affix",
                column: "Title");
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
                name: "inflectional_affix",
                schema: "public");

            migrationBuilder.DropTable(
                name: "dialect",
                schema: "public");

            migrationBuilder.DropTable(
                name: "literary_word",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "type_of_inflectional_affix",
                schema: "public");

            migrationBuilder.DropTable(
                name: "part_of_speech",
                schema: "public");
        }
    }
}
