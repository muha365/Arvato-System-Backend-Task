using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Data.Helpers
{
    public static class DbMigrationExtensions
    {
        public static void CreateFullTextIndex(
            this DbMigration migration,
            string table,
            string index,
            string[] columns)
        {
            var op = new CreateFullTextIndexOperation
            {
                Table = table,
                Index = index,
                Columns = columns
            };

            ((IDbMigration)migration).AddOperation(op);
        }
    }
}
