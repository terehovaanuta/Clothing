namespace Clothing_v2._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class realName1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Realname", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Realname");
        }
    }
}
