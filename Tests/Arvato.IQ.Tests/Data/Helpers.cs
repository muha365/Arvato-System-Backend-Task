using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arvato.IQ.Tests.Data
{
    public static class DbTestHelper
    {
        public static void SetupDatabase<TDbContext>(string dataDirectory) where TDbContext : DbContext
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
            Database.SetInitializer(new DropCreateDatabaseAlways<TDbContext>());
        }

        internal static void VerifyTableSchema(DbContext context,
            string tblName,
            string[] tblFields,
            string[] indexes = null)
        {
            var sqlConn = context.Database.Connection as SqlConnection;
            // Give up and assume its ok if its not a sql connection
            if (sqlConn == null)
            {
                Assert.True(false, "Expected a sql connection");
            }
            try
            {
                if (sqlConn.State != System.Data.ConnectionState.Open)
                {
                    sqlConn.Open();
                }
                Assert.True(VerifyColumns(sqlConn, tblName, tblFields));
                if (indexes != null && indexes.Length > 0)
                {
                    for (int i = 0; i < indexes.Length; i++)
                    {
                       Assert.True(VerifyIndex(sqlConn, tblName, indexes[i]));
                    }
                }

            }
            finally
            {
                sqlConn.Close();
            }

        }

        internal static bool VerifyColumns(SqlConnection conn, string table, params string[] columns)
        {
            var count = 0;
            using (
                var command =
                    new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table", conn))
            {
                command.Parameters.Add(new SqlParameter("Table", table));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count++;
                        if (!columns.Contains(reader.GetString(0)))
                        {
                            return false;
                        }
                    }
                    return count == columns.Length;
                }
            }
        }

        internal static bool VerifyIndex(SqlConnection conn, string table, string index)
        {
            using (var command = new SqlCommand("SELECT COUNT(*) FROM sys.indexes where NAME=@Index AND object_id = OBJECT_ID(@Table)", conn))
            {
                command.Parameters.Add(new SqlParameter("Index", index));
                command.Parameters.Add(new SqlParameter("Table", table));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (reader.GetInt32(0) > 0);
                    }
                    return false;
                }
            }
        }

    }

}
