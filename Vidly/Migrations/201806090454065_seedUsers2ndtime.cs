namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers2ndtime : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'1e6456a3-0a99-481f-ae34-843fd6b0aef6', N'tushar', N'AECoyZx6G+pP4rtu/N5G8LL0aQi9TGBXnc922kEzvaqobKv6wtyO8v9Uos0EF3T4Kg==', N'6c6d064a-d88b-4a0e-9ba2-c69c55a35ff8', N'ApplicationUser')
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'ba212948-e9df-47ef-b9d6-13d74c0553e6', N'admin2', N'AK98Y9T2yLr3fuVp8pmjR9Ehc/EQ5wXebfAbgSV92FZnVRhTeofq0HlZEtMWLyaT1Q==', N'24abc7ed-1c16-413f-a265-6b467fac511b', N'ApplicationUser')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c22b26ba-adfb-4d30-80b5-a101c5debe6b', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ba212948-e9df-47ef-b9d6-13d74c0553e6', N'c22b26ba-adfb-4d30-80b5-a101c5debe6b')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
