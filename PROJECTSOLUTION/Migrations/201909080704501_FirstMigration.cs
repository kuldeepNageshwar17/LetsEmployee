namespace PROJECTSOLUTION.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.USER_ACCOUNT",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ROLE = c.Int(nullable: false),
                        EMAIL = c.String(),
                        PASSWORD = c.String(),
                        PASSWORD_HASH = c.String(),
                        FIRST_NAME = c.String(),
                        MIDDLE_NAME = c.String(),
                        LAST_NAME = c.String(),
                        FULL_NAME = c.String(),
                        GENDER = c.String(),
                        IS_ACTIVE = c.Boolean(nullable: false),
                        MOBILE = c.String(),
                        SMS_NOTIFICATION = c.Boolean(nullable: false),
                        EMAIL_NOTIFICATION = c.Boolean(nullable: false),
                        REGISTRATION_DATE = c.DateTime(nullable: false),
                        MODIFICATION_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.USER_ACCOUNT");
        }
    }
}
