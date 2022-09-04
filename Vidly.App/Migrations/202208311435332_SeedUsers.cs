namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c7c963e4-c6d4-4675-baff-b803aaf7e57a', N'admin@vidly.com', 0, N'ABGcq1xX82sQwXQICzqPkOEWxZNYsdP/CtUKFzDWCMCg0+I86TEjKB5kFog+k91+yA==', N'5e978104-b498-4d88-a087-5df2ee1c37d2', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f3d95852-3a0b-4723-ba66-a08f604292ba', N'guest@vidly.com', 0, N'AJoGeoEVXU3X1yQrlm6TW0hbj0GDYKU6FDJXdcN6c+YFap0+I4plI13qikPSwoKZqQ==', N'5d9f955a-168c-41ba-9713-ea4fa43a59e9', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')           
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'cbe4bd75-024e-4241-ba1c-60c393889fc5', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c7c963e4-c6d4-4675-baff-b803aaf7e57a', N'cbe4bd75-024e-4241-ba1c-60c393889fc5')
            ");
        }

        public override void Down()
        {
        }
    }
}
