using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FA.JustBlog.Core.Migrations
{
    /// <inheritdoc />
    public partial class HasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.CreateTable(
                name: "PostTagMap",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTagMap", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostTagMap_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTagMap_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "UrlSlug" },
                values: new object[,]
                {
                    { 1, "All post in Entity Framework", "Entity Framework", "entity-framework" },
                    { 2, "All posts in ASP.NET Core", "ASP.NET Core", "asp-net-core" },
                    { 3, "All posts in ASP.NET Core entity", "ASP.NET Entity", "asp-net-core-entity" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Count", "Description", "Name", "UrlSlug" },
                values: new object[,]
                {
                    { 1, 100, "Entity Framework", "Entity Framework", "entity-framework" },
                    { 2, 50, "Microsoft MVC", "MVC", "mvc" },
                    { 3, 1, "Posts about C#", "C#", "c-sharp" },
                    { 4, 1, "Posts about ASP.NET Core", "ASP.NET Core", "asp-net-core" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Meta", "Modified", "PostContent", "Posted On", "Published", "RateCount", "Short Description", "Title", "TotalRate", "UrlSlug", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, "EF, MVC", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8340), "In the above example, the Course and Teacher entities have two one-to-many relationships...", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8358), true, 10, "The InverseProperty attribute is used when two entities have more than one relationship.", "Data Annotations - InverseProperty Attribute in EF 6 & EF Core", 45, "data-annotation-inverse-property-attribule-in-ef-6", 100 },
                    { 2, 2, "ASP.NET Core, .NET", null, "Content of the post", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8366), true, null, "An introduction to ASP.NET Core", "Introduction to ASP.NET Core", null, "intro-asp-net-core", null },
                    { 3, 3, "C#, .NET", null, "Content of the post", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8368), true, null, "A look at advanced C# techniques", "Advanced C# Techniques", null, "advanced-c-sharp", null },
                    { 4, 3, "C#, .NET", null, "Content of the post", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8370), true, null, "A look at advanced C# techniques", "Latest Post", null, "advanced-c-sharp", null }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentHeader", "CommentText", "CommentTime", "Email", "Name", "PostId" },
                values: new object[,]
                {
                    { 1, "This is a sample comment", "This is a sample comment with multiple lines", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8812), "tutb@live.com", "Scott Trinh", 1 },
                    { 2, "Very Informative", "I learned a lot from this post.", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8817), "jane@example.com", "Jane Smith", 2 },
                    { 3, "Excellent Read", "Thanks for the great tips.", new DateTime(2024, 8, 6, 22, 4, 50, 760, DateTimeKind.Local).AddTicks(8819), "bob@example.com", "Bob Johnson", 3 }
                });

            migrationBuilder.InsertData(
                table: "PostTagMap",
                columns: new[] { "PostId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTagMap_TagId",
                table: "PostTagMap",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTagMap");

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                column: "TagsId");
        }
    }
}
