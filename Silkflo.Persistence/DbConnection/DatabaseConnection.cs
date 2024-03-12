using PetaPoco;
using PetaPoco.Providers;
using Silkflo.Persistence.Abstractions;

namespace Silkflo.Persistence.DbConnection
{
    public class DatabaseConnection : IDatabaseConnection
    { 
        public async Task<IDatabase> GetDbConnectionAsync()
        {
            return DatabaseConfiguration.Build()
                 .UsingConnectionString("Server=tcp:silkflo-dev.database.windows.net,1433;Initial Catalog=silkflo-test;Persist Security Info=False;User ID=sfadmin;Password=Sf-admin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                 .UsingProvider<SqlServerDatabaseProvider>()
                 .UsingDefaultMapper<ConventionMapper>(m =>
                 {
                     m.InflectTableName = (inflector, s) => inflector.Pluralise(s);
                     //m.InflectColumnName = (inflector, s) => inflector.Underscore(s);
                 })
             .Create();
        }
    }
}
