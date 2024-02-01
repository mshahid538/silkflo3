using PetaPoco;
using PetaPoco.Providers;
using SilkFlo.Extensions.Helpers;
using System.Linq.Expressions;

namespace SilkFlo.Extensions
{
    public class ListExtended<T>
    {
        private IDatabase _db;
        public ListExtended()
        {
            _db = DatabaseConfiguration.Build()
                 .UsingConnectionString("Server=tcp:silkflo-dev.database.windows.net,1433;Initial Catalog=silkflo-prod;Persist Security Info=False;User ID=sfadmin;Password=Sf-admin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                 .UsingProvider<SqlServerDatabaseProvider>()
                 .UsingDefaultMapper<ConventionMapper>(m =>
                 {
                     m.InflectTableName = (inflector, s) => inflector.Pluralise(s);
                     //m.InflectColumnName = (inflector, s) => inflector.Underscore(s);
                 })
             .Create();
        }

        public T Add(T entity)
        {
            var tableName = typeof(T).Name;
            _db.Insert(tableName, "Id", entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            var tableName = typeof(T).Name;
            await _db.InsertAsync(tableName, "Id", entity).ConfigureAwait(false);
            return entity;
        }

        public T Remove(T entity)
        {
            var tableName = typeof(T).Name;
            _db.Delete(tableName, "Id", entity);
            return entity;
        }

        public async Task<T> RemoveAsync(T entity)
        {
            var tableName = typeof(T).Name;
            await _db.DeleteAsync(tableName, "Id", entity).ConfigureAwait(false);
            return entity;
        }

        public T SingleOrDefault<T>(Func<T, bool> predicate) 
        {
            var whereClause = PredicateTranslator.Translate(predicate);
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
            return _db.SingleOrDefault<T>(query);
        }

        public async Task<List<T>> FindAsync<T>(Func<T, bool> predicate)
        {
            var whereClause = PredicateTranslator.Translate(predicate);
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName}";

            if (whereClause != null)
            {
                query += " WHERE " + whereClause;
                var result = await _db.FetchAsync<T>(query).ConfigureAwait(false);
                return result;
            }
            else
            {
                return await _db.FetchAsync<T>(query).ConfigureAwait(false);
            }
        }

        public List<T> Find<T>(Func<T, bool> predicate)
        {
            var whereClause = PredicateTranslator.Translate(predicate);
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName}";

            if (whereClause != null)
            {
                query += " WHERE " + whereClause;
                var result = _db.Fetch<T>(query);
                return result;
            }
            else
            {
                return _db.Fetch<T>(query);
            }
        }

        public List<T> Where<T>(Func<T, bool> predicate)
        {
            var whereClause = PredicateTranslator.Translate(predicate);
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName}";

            if (whereClause != null)
            {
                query += " WHERE " + whereClause;
                var result = _db.Fetch<T>(query);
                return result;
            }
            else
            {
                return _db.Fetch<T>(query);
            }
        }

        public async Task<T> SingleOrDefaultAsync(Func<T, bool> predicate)
        {
            var whereClause = PredicateTranslator.Translate(predicate);
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
            return await _db.SingleOrDefaultAsync<T>(query).ConfigureAwait(false);
        }
        
        public async Task<T> FirstOrDefaultAsync(Func<T, bool> predicate)
        {
            var whereClause = PredicateTranslator.Translate(predicate);
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
            return await _db.FirstOrDefaultAsync<T>(query).ConfigureAwait(false);
        }
        
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            var whereClause = PredicateTranslator.Translate(predicate);
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
            return _db.FirstOrDefault<T>(query);
        }

        public async Task<List<T>> ToListAsync<T>()
        {
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName}";

            return await _db.FetchAsync<T>(query).ConfigureAwait(false);
        }

        public List<T> ToList<T>()
        {
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName}";

            return _db.Fetch<T>(query);
        }
    }

    //public static class ListExtendedExtensions
    //{
    //    public static async Task<List<T>> FindAsync<T>(this ListExtended<T> listExtended, Func<T, bool> predicate) where T : new()
    //    {
    //        var whereClause = PredicateTranslator.Translate(predicate);
    //        var tableName = typeof(T).Name;
    //        var query = $"SELECT * FROM {tableName}";

    //        if (whereClause != null)
    //        {
    //            query += " WHERE " + whereClause;
    //            var result = await listExtended._db.FetchAsync<T>(query).ConfigureAwait(false);
    //            return result;
    //        }
    //        else
    //        {
    //            return await listExtended._db.FetchAsync<T>(query).ConfigureAwait(false);
    //        }
    //    }
    //    public static List<T> Find<T>(this ListExtended<T> listExtended, Func<T, bool> predicate) where T : new()
    //    {
    //        var whereClause = PredicateTranslator.Translate(predicate);
    //        var tableName = typeof(T).Name;
    //        var query = $"SELECT * FROM {tableName}";

    //        if (whereClause != null)
    //        {
    //            query += " WHERE " + whereClause;
    //            var result = listExtended._db.Fetch<T>(query); //.ConfigureAwait(false);
    //            return result;
    //        }
    //        else
    //        {
    //            return listExtended._db.Fetch<T>(query); //.ConfigureAwait(false);
    //        }
    //    }

    //    public static List<T> Where<T>(this ListExtended<T> listExtended, Func<T, bool> predicate) where T : new()
    //    {
    //        var whereClause = PredicateTranslator.Translate(predicate);
    //        var tableName = typeof(T).Name;
    //        var query = $"SELECT * FROM {tableName}";

    //        if (whereClause != null)
    //        {
    //            query += " WHERE " + whereClause;
    //            var result = listExtended._db.Fetch<T>(query); //.ConfigureAwait(false);
    //            return result;
    //        }
    //        else
    //        {
    //            return listExtended._db.Fetch<T>(query); //.ConfigureAwait(false);
    //        }
    //    }


    //    public static async Task<T> SingleOrDefaultAsync<T>(this ListExtended<T> listExtended, Func<T, bool> predicate) where T : new()
    //    {
    //        var whereClause = PredicateTranslator.Translate(predicate);
    //        var tableName = typeof(T).Name;
    //        var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
    //        return await listExtended._db.SingleOrDefaultAsync<T>(query).ConfigureAwait(false);       
    //    }
    //    //{
    //    //    var tableName = typeof(T).Name;
    //    //    var query = $"SELECT * FROM {tableName} WHERE {predicate.ToSql()}";
    //    //    return await _db.QuerySingleOrDefaultAsync<T>(query).ConfigureAwait(false);
    //    //}
    //    public static T SingleOrDefault<T>(this ListExtended<T> listExtended, Func<T, bool> predicate) where T : new()
    //    {
    //        var whereClause = PredicateTranslator.Translate(predicate);
    //        var tableName = typeof(T).Name;
    //        var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
    //        return listExtended._db.SingleOrDefault<T>(query); //.ConfigureAwait(false);
    //    }

    //    public static async Task<T> FirstOrDefaultAsync<T>(this ListExtended<T> listExtended, Func<T, bool> predicate) where T : new()
    //    {
    //        var whereClause = PredicateTranslator.Translate(predicate);
    //        var tableName = typeof(T).Name;
    //        var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
    //        return await listExtended._db.FirstOrDefaultAsync<T>(query).ConfigureAwait(false);
    //    }
    //    //{
    //    //    var tableName = typeof(T).Name;
    //    //    var query = $"SELECT TOP 1 * FROM {tableName}";
    //    //    return await _db.QueryFirstOrDefaultAsync<T>(query).ConfigureAwait(false);
    //    //}
    //    public static T FirstOrDefault<T>(this ListExtended<T> listExtended, Func<T, bool> predicate) where T : new()
    //    {
    //        var whereClause = PredicateTranslator.Translate(predicate);
    //        var tableName = typeof(T).Name;
    //        var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
    //        return listExtended._db.FirstOrDefault<T>(query); //.ConfigureAwait(false);
    //    }
    //}
}