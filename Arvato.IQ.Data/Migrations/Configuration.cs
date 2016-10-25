namespace Arvato.IQ.Data.Migrations
{
    using Helpers;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.SqlServer;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DbStore>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations";
            SetSqlGenerator(SqlProviderServices.ProviderInvariantName, new CustomMigrationSqlGenerator());
        }

        protected override void Seed(DbStore context)
        {
            
        }
    }
}
