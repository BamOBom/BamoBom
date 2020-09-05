using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    Caption = table.Column<string>(maxLength: 3000, nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: false),
                    Image = table.Column<string>(maxLength: 150, nullable: false),
                    Keywords = table.Column<string>(nullable: true),
                    DescriptionSeo = table.Column<string>(nullable: true),
                    SetSliderHome = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contactuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Subject = table.Column<string>(maxLength: 150, nullable: true),
                    Content = table.Column<string>(maxLength: 1000, nullable: false),
                    IsVisited = table.Column<bool>(nullable: false),
                    IsReplied = table.Column<bool>(nullable: false),
                    ReplyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Flag = table.Column<string>(nullable: true),
                    ISO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 120, nullable: false),
                    Question = table.Column<string>(maxLength: 500, nullable: false),
                    Answer = table.Column<string>(maxLength: 1500, nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    IsRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ApplicationTitle = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "ntext", maxLength: 1000, nullable: true),
                    KeyWords = table.Column<string>(maxLength: 250, nullable: true),
                    Favicon = table.Column<string>(maxLength: 250, nullable: true),
                    Logo = table.Column<string>(maxLength: 250, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    FaxNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Link = table.Column<string>(maxLength: 1000, nullable: false),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PageTitle = table.Column<string>(maxLength: 300, nullable: false),
                    Caption = table.Column<string>(maxLength: 3000, nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: false),
                    Url = table.Column<string>(maxLength: 300, nullable: false),
                    Keywords = table.Column<string>(nullable: true),
                    DescriptionSeo = table.Column<string>(nullable: true),
                    StaticPageType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadCenters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    File = table.Column<string>(maxLength: 300, nullable: true),
                    Url = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    Surname = table.Column<string>(maxLength: 100, nullable: false),
                    NationalIdentity = table.Column<string>(maxLength: 100, nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    MilitaryServiceStatus = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    CooprationStatus = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Representer = table.Column<string>(maxLength: 150, nullable: true),
                    EnglishSkill = table.Column<string>(nullable: true),
                    RequestedSalary = table.Column<decimal>(nullable: false),
                    ResumeFileName = table.Column<string>(maxLength: 100, nullable: true),
                    JobTitleId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    JobStatus = table.Column<byte>(nullable: false),
                    IsVisited = table.Column<bool>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequests_Jobs_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CvJobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobRequestId = table.Column<int>(nullable: false),
                    CvSummary = table.Column<string>(nullable: true),
                    Url = table.Column<string>(maxLength: 500, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvJobs_JobRequests_JobRequestId",
                        column: x => x.JobRequestId,
                        principalTable: "JobRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastLoginDate = table.Column<DateTimeOffset>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    UserType = table.Column<byte>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    ProfileImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districs_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CvJobCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    GetPlace = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "ntext", maxLength: 1000, nullable: true),
                    Date = table.Column<string>(nullable: true),
                    CvJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvJobCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvJobCertificates_CvJobs_CvJobId",
                        column: x => x.CvJobId,
                        principalTable: "CvJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CvJobEducations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Educations = table.Column<byte>(nullable: false),
                    Reshte = table.Column<string>(maxLength: 100, nullable: false),
                    Unvercity = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "ntext", maxLength: 300, nullable: true),
                    Iseducation = table.Column<bool>(nullable: false),
                    CvJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvJobEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvJobEducations_CvJobs_CvJobId",
                        column: x => x.CvJobId,
                        principalTable: "CvJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CvJobResearches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "ntext", maxLength: 100, nullable: true),
                    Date = table.Column<string>(nullable: true),
                    CvJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvJobResearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvJobResearches_CvJobs_CvJobId",
                        column: x => x.CvJobId,
                        principalTable: "CvJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CvJobSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Skill = table.Column<byte>(nullable: false),
                    CvJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvJobSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvJobSkills_CvJobs_CvJobId",
                        column: x => x.CvJobId,
                        principalTable: "CvJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CvJobWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Post = table.Column<string>(maxLength: 100, nullable: false),
                    Workplace = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "ntext", maxLength: 100, nullable: true),
                    IsWorking = table.Column<bool>(nullable: false),
                    CvJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvJobWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvJobWorks_CvJobs_CvJobId",
                        column: x => x.CvJobId,
                        principalTable: "CvJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IP = table.Column<string>(nullable: true),
                    HttpForward = table.Column<string>(nullable: true),
                    UrlCurrent = table.Column<string>(nullable: true),
                    UrlReferrer = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    DeviceBrand = table.Column<string>(nullable: true),
                    DeviceModel = table.Column<string>(nullable: true),
                    IsCrawler = table.Column<bool>(nullable: false),
                    OsName = table.Column<string>(nullable: true),
                    OsVersion = table.Column<string>(nullable: true),
                    BrowserName = table.Column<string>(nullable: true),
                    BrowserVersion = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CvJobCertificates_CvJobId",
                table: "CvJobCertificates",
                column: "CvJobId");

            migrationBuilder.CreateIndex(
                name: "IX_CvJobEducations_CvJobId",
                table: "CvJobEducations",
                column: "CvJobId");

            migrationBuilder.CreateIndex(
                name: "IX_CvJobResearches_CvJobId",
                table: "CvJobResearches",
                column: "CvJobId");

            migrationBuilder.CreateIndex(
                name: "IX_CvJobs_JobRequestId",
                table: "CvJobs",
                column: "JobRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CvJobSkills_CvJobId",
                table: "CvJobSkills",
                column: "CvJobId");

            migrationBuilder.CreateIndex(
                name: "IX_CvJobWorks_CvJobId",
                table: "CvJobWorks",
                column: "CvJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Districs_CityId",
                table: "Districs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_JobTitleId",
                table: "JobRequests",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_UserId",
                table: "Visitors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Contactuses");

            migrationBuilder.DropTable(
                name: "CvJobCertificates");

            migrationBuilder.DropTable(
                name: "CvJobEducations");

            migrationBuilder.DropTable(
                name: "CvJobResearches");

            migrationBuilder.DropTable(
                name: "CvJobSkills");

            migrationBuilder.DropTable(
                name: "CvJobWorks");

            migrationBuilder.DropTable(
                name: "Districs");

            migrationBuilder.DropTable(
                name: "FAQS");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SocialApplications");

            migrationBuilder.DropTable(
                name: "StaticPages");

            migrationBuilder.DropTable(
                name: "UploadCenters");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CvJobs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "JobRequests");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
