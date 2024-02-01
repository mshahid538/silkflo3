using PetaPoco;
using PetaPoco.Providers;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Application;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Caching;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SilkFlo.Data.Persistence
{
    public class DataSet
    {
        public ListExtended<Analytic> Analytics { get; set; } = new ListExtended<Analytic>();

        public ListExtended<Log> Logs { get; set; } = new ListExtended<Log>();

        public ListExtended<Message> Messages { get; set; } = new ListExtended<Message>();

        public ListExtended<SilkFlo.Data.Core.Domain.Role> Roles { get; set; } = new ListExtended<SilkFlo.Data.Core.Domain.Role>();

        public ListExtended<User> Users { get; set; } = new ListExtended<User>();

        public ListExtended<UserAchievement> UserAchievements { get; set; } = new ListExtended<UserAchievement>();

        public ListExtended<UserBadge> UserBadges { get; set; } = new ListExtended<UserBadge>();

        public ListExtended<UserRole> UserRoles { get; set; } = new ListExtended<UserRole>();

        public ListExtended<WebHookLog> WebHookLogs { get; set; } = new ListExtended<WebHookLog>();

        public ListExtended<ManageTenant> AgencyManageTenants { get; set; } = new ListExtended<ManageTenant>();

        public ListExtended<HotSpot> ApplicationHotSpots { get; set; } = new ListExtended<HotSpot>();

        public ListExtended<Page> ApplicationPages { get; set; } = new ListExtended<Page>();

        public ListExtended<Setting> ApplicationSettings { get; set; } = new ListExtended<Setting>();

        public ListExtended<SilkFlo.Data.Core.Domain.Business.Application> BusinessApplications { get; set; } = new ListExtended<SilkFlo.Data.Core.Domain.Business.Application>();

        public ListExtended<Client> BusinessClients { get; set; } = new ListExtended<Client>();

        public ListExtended<Collaborator> BusinessCollaborators { get; set; } = new ListExtended<Collaborator>();

        public ListExtended<CollaboratorRole> BusinessCollaboratorRoles { get; set; } = new ListExtended<CollaboratorRole>();

        public ListExtended<Comment> BusinessComments { get; set; } = new ListExtended<Comment>();

        public ListExtended<Department> BusinessDepartments { get; set; } = new ListExtended<Department>();

        public ListExtended<Document> BusinessDocuments { get; set; } = new ListExtended<Document>();

        public ListExtended<Follow> BusinessFollows { get; set; } = new ListExtended<Follow>();

        public ListExtended<Idea> BusinessIdeas { get; set; } = new ListExtended<Idea>();

        public ListExtended<IdeaApplicationVersion> BusinessIdeaApplicationVersions { get; set; } = new ListExtended<IdeaApplicationVersion>();

        public ListExtended<IdeaOtherRunningCost> BusinessIdeaOtherRunningCosts { get; set; } = new ListExtended<IdeaOtherRunningCost>();

        public ListExtended<IdeaRunningCost> BusinessIdeaRunningCosts { get; set; } = new ListExtended<IdeaRunningCost>();

        public ListExtended<IdeaStage> BusinessIdeaStages { get; set; } = new ListExtended<IdeaStage>();

        public ListExtended<IdeaStageStatus> BusinessIdeaStageStatuses { get; set; } = new ListExtended<IdeaStageStatus>();

        public ListExtended<ImplementationCost> BusinessImplementationCosts { get; set; } = new ListExtended<ImplementationCost>();

        public ListExtended<Location> BusinessLocations { get; set; } = new ListExtended<Location>();

        public ListExtended<OtherRunningCost> BusinessOtherRunningCosts { get; set; } = new ListExtended<OtherRunningCost>();

        public ListExtended<Process> BusinessProcesses { get; set; } = new ListExtended<Process>();

        public ListExtended<Recipient> BusinessRecipients { get; set; } = new ListExtended<Recipient>();

        public ListExtended<SilkFlo.Data.Core.Domain.Business.Role> BusinessRoles { get; set; } = new ListExtended<SilkFlo.Data.Core.Domain.Business.Role>();

        public ListExtended<RoleCost> BusinessRoleCosts { get; set; } = new ListExtended<RoleCost>();

        public ListExtended<RoleIdeaAuthorisation> BusinessRoleIdeaAuthorisations { get; set; } = new ListExtended<RoleIdeaAuthorisation>();

        public ListExtended<RunningCost> BusinessRunningCosts { get; set; } = new ListExtended<RunningCost>();

        public ListExtended<SoftwareVender> BusinessSoftwareVenders { get; set; } = new ListExtended<SoftwareVender>();

        public ListExtended<Team> BusinessTeams { get; set; } = new ListExtended<Team>();

        public ListExtended<UserAuthorisation> BusinessUserAuthorisations { get; set; } = new ListExtended<UserAuthorisation>();

        public ListExtended<Core.Domain.Business.Version> BusinessVersions { get; set; } = new ListExtended<Core.Domain.Business.Version>();

        public ListExtended<Vote> BusinessVotes { get; set; } = new ListExtended<Vote>();

        public ListExtended<CompanySize> CRMCompanySizes { get; set; } = new ListExtended<CompanySize>();

        public ListExtended<JobLevel> CRMJobLevels { get; set; } = new ListExtended<JobLevel>();

        public ListExtended<Prospect> CRMProspects { get; set; } = new ListExtended<Prospect>();

        public ListExtended<Achievement> SharedAchievements { get; set; } = new ListExtended<Achievement>();

        public ListExtended<ApplicationStability> SharedApplicationStabilities { get; set; } = new ListExtended<ApplicationStability>();

        public ListExtended<AutomationGoal> SharedAutomationGoals { get; set; } = new ListExtended<AutomationGoal>();

        public ListExtended<AutomationType> SharedAutomationTypes { get; set; } = new ListExtended<AutomationType>();

        public ListExtended<AverageNumberOfStep> SharedAverageNumberOfSteps { get; set; } = new ListExtended<AverageNumberOfStep>();

        public ListExtended<Badge> SharedBadges { get; set; } = new ListExtended<Badge>();

        public ListExtended<ClientType> SharedClientTypes { get; set; } = new ListExtended<ClientType>();

        public ListExtended<CostType> SharedCostTypes { get; set; } = new ListExtended<CostType>();

        public ListExtended<Country> SharedCountries { get; set; } = new ListExtended<Country>();

        public ListExtended<DataInputPercentOfStructured> SharedDataInputPercentOfStructureds { get; set; } = new ListExtended<DataInputPercentOfStructured>();

        public ListExtended<DecisionCount> SharedDecisionCounts { get; set; } = new ListExtended<DecisionCount>();

        public ListExtended<DecisionDifficulty> SharedDecisionDifficulties { get; set; } = new ListExtended<DecisionDifficulty>();

        public ListExtended<DocumentationPresent> SharedDocumentationPresents { get; set; } = new ListExtended<DocumentationPresent>();

        public ListExtended<IdeaAuthorisation> SharedIdeaAuthorisations { get; set; } = new ListExtended<IdeaAuthorisation>();

        public ListExtended<IdeaStatus> SharedIdeaStatuses { get; set; } = new ListExtended<IdeaStatus>();

        public ListExtended<Industry> SharedIndustries { get; set; } = new ListExtended<Industry>();

        public ListExtended<Input> SharedInputs { get; set; } = new ListExtended<Input>();

        public ListExtended<InputDataStructure> SharedInputDataStructures { get; set; } = new ListExtended<InputDataStructure>();

        public ListExtended<Language> SharedLanguages { get; set; } = new ListExtended<Language>();

        public ListExtended<NumberOfWaysToCompleteProcess> SharedNumberOfWaysToCompleteProcesses { get; set; } = new ListExtended<NumberOfWaysToCompleteProcess>();

        public ListExtended<Period> SharedPeriods { get; set; } = new ListExtended<Period>();

        public ListExtended<ProcessPeak> SharedProcessPeaks { get; set; } = new ListExtended<ProcessPeak>();

        public ListExtended<ProcessStability> SharedProcessStabilities { get; set; } = new ListExtended<ProcessStability>();

        public ListExtended<Rule> SharedRules { get; set; } = new ListExtended<Rule>();

        public ListExtended<Stage> SharedStages { get; set; } = new ListExtended<Stage>();

        public ListExtended<StageGroup> SharedStageGroups { get; set; } = new ListExtended<StageGroup>();

        public ListExtended<SubmissionPath> SharedSubmissionPaths { get; set; } = new ListExtended<SubmissionPath>();

        public ListExtended<TaskFrequency> SharedTaskFrequencies { get; set; } = new ListExtended<TaskFrequency>();

        public ListExtended<Core.Domain.Shop.Coupon> ShopCoupons { get; set; } = new ListExtended<Core.Domain.Shop.Coupon>();

        public ListExtended<Currency> ShopCurrencies { get; set; } = new ListExtended<Currency>();

        public ListExtended<Core.Domain.Shop.Discount> ShopDiscounts { get; set; } = new ListExtended<Core.Domain.Shop.Discount>();

        public ListExtended<Core.Domain.Shop.Price> ShopPrices { get; set; } = new ListExtended<Core.Domain.Shop.Price>();

        public ListExtended<Core.Domain.Shop.Product> ShopProducts { get; set; } = new ListExtended<Core.Domain.Shop.Product>();

        public ListExtended<Core.Domain.Shop.Subscription> ShopSubscriptions { get; set; } = new ListExtended<Core.Domain.Shop.Subscription>();
    }

    public class ListExtended<T>
    {
        private IDatabase _db;
        private DataRepository _dbRepository;

        public ListExtended()
        {
            _db = GetDbConnection();
            _dbRepository = new DataRepository();
        }

        private IDatabase GetDbConnection()
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

        public bool IsValueInList(List<T> listT, string propertyName, object valueToCheck)
        {
            if (listT == null)
            {
                throw new ArgumentNullException(nameof(listT));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
            }

            foreach (var entity in listT)
            {
                PropertyInfo property = typeof(T).GetProperty(propertyName);

                if (property == null)
                {
                    throw new ArgumentException($"Property {propertyName} not found on type {typeof(T).FullName}");
                }

                object propertyValue = property.GetValue(entity);

                if (propertyValue != null && propertyValue.Equals(valueToCheck))
                {
                    return true; // Value found in the list
                }
            }

            return false; // Value not found in the list
        }

        public T Add(T entity)
        {
            var tableName = GetTableName(typeof(T));

            if (tableName.ToLower().Equals("Logs".ToLower()) || tableName.ToLower().Equals("Analytics".ToLower()))
            {
                var donotSave = true;
                return default;
            }

            if (entity is not null)
            {
                var listT = _dbRepository.GetAll<T>(tableName);
                if (listT is not null && listT.Count > 0)
                {
                    PropertyInfo property = typeof(T).GetProperty("Id");
                    var entityId = property.GetValue(entity);

                    //var isExist = IsValueInList(listT, "Id", entityId);// listT.Any(item => item.Equals(entity));
                    //if (isExist)
                    //    return default;

                    var obj = _db.SingleOrDefault<T>("WHERE Id = @0", entityId);
                    if (obj is not null)
                    {
                        return default;
                    }
                }


                if (_db is null)
                    _db = GetDbConnection();

                _db.Insert(tableName, "Id", entity);

                //Invalidate cache
                _dbRepository.RefreshSingleton<T>(tableName);

                return entity;
            }

            return default;
        }


        public T Update(T entity)
        {
            var tableName = GetTableName(typeof(T));

            if (tableName.ToLower().Equals("Logs".ToLower()) || tableName.ToLower().Equals("Analytics".ToLower()))
            {
                var donotSave = true;
                return default;
            }

            if (entity is not null)
            {
                if (_db is null)
                    _db = GetDbConnection();

                _db.Update(tableName, "Id", entity);

                //Invalidate cache
                _dbRepository.RefreshSingleton<T>(tableName);

                return entity;
            }

            return default;
        }

        public async Task<T> AddAsync(T entity)
        {
            var tableName = GetTableName(typeof(T));

            if (entity is not null)
            {
                if (_db is null)
                    _db = GetDbConnection();

                await _db.InsertAsync(tableName, "Id", entity).ConfigureAwait(false);
                return entity;
            }

            return default;
        }

        public T Remove(T entity)
        {
            var tableName = GetTableName(typeof(T));

            if (entity is not null)
            {
                if (_db is null)
                    _db = GetDbConnection();

                _db.Delete(tableName, "Id", entity);

                //Invalidate cache
                _dbRepository.RefreshSingleton<T>(tableName);

                return entity;
            }

            return default;
        }

        public async Task<T> RemoveAsync(T entity)
        {
            var tableName = GetTableName(typeof(T));

            if (entity is not null)
            {
                if (_db is null)
                    _db = GetDbConnection();

                await _db.DeleteAsync(tableName, "Id", entity).ConfigureAwait(false);
                return entity;
            }

            return default;
        }


        public T SingleOrDefault<T>(Func<T, bool> predicate)
        {
            var tableName = GetTableName(typeof(T));

            var list = _dbRepository.GetAll<T>(tableName);
            return list.SingleOrDefault(predicate);
        }

        public async Task<List<T>> FindAsync<T>(Func<T, bool> predicate)
        {
            var tableName = GetTableName(typeof(T));

            var list = _dbRepository.GetAll<T>(tableName);
            return list.Where(predicate).ToList();
        }

        public List<T> Find<T>(Func<T, bool> predicate)
        {
            var tableName = GetTableName(typeof(T));

            var list = _dbRepository.GetAll<T>(tableName);
            return list.Where(predicate).ToList();
        }

        public List<T> Where<T>(Func<T, bool> predicate)
        {
            var tableName = GetTableName(typeof(T));

            var list = _dbRepository.GetAll<T>(tableName);
            return list.Where(predicate).ToList();
        }

        public async Task<T> SingleOrDefaultAsync(Func<T, bool> predicate)
        {
            return default;
            //using (var db = GetDbConnection())
            //{
            //    var result = await db.FetchAsync<T>();
            //    return result.SingleOrDefault(predicate);
            //}

            //Expression<Func<T, bool>> expressionFunc = x => predicate(x);
            //var whereClause = ""; // PredicateTranslator.Parse(expressionFunc);
            //var tableName = GetTableName(typeof(T));
            //var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
            //return await _db.SingleOrDefaultAsync<T>(query).ConfigureAwait(false);
        }

        public async Task<T> FirstOrDefaultAsync(Func<T, bool> predicate)
        {
            return default;
            //using (var db = GetDbConnection())
            //{
            //    var result = db.Fetch<T>();
            //    return result.FirstOrDefault(predicate);
            //}

            //Expression<Func<T, bool>> expressionFunc = x => predicate(x);
            //var whereClause = ""; // PredicateTranslator.Parse(expressionFunc);
            //var tableName = GetTableName(typeof(T));
            //var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
            //return await _db.FirstOrDefaultAsync<T>(query).ConfigureAwait(false);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return default;

            //using (var db = GetDbConnection())
            //{
            //    var result = db.Fetch<T>();
            //    return result.FirstOrDefault(predicate);
            //}

            //Expression<Func<T, bool>> expressionFunc = x => predicate(x);
            //var whereClause = ""; // PredicateTranslator.Parse(expressionFunc);
            //var tableName = GetTableName(typeof(T));
            //var query = $"SELECT * FROM {tableName} WHERE {whereClause}";
            //return _db.FirstOrDefault<T>(query);
        }

        public async Task<List<T>> ToListAsync<T>()
        {
            var tableName = GetTableName(typeof(T));

            var list = _dbRepository.GetAll<T>(tableName);
            return list;

            //using (var db = GetDbConnection())
            //    return await db.FetchAsync<T>();

            //var tableName = GetTableName(typeof(T));
            //var query = $"SELECT * FROM {tableName}";

            //return await _db.FetchAsync<T>(query).ConfigureAwait(false);
        }

        public List<T> ToList<T>()
        {
            //var tableName = GetTableName(typeof(T));
            //var query = $"SELECT * FROM {tableName}";

            //if (_db is null)
            //    _db = GetDbConnection();

            var tableName = GetTableName(typeof(T));

            var list = _dbRepository.GetAll<T>(tableName);
            return list;

            //using (var db = GetDbConnection())
            //    return _db.Fetch<T>();
        }

        private string GetTableName(Type type)
        {
            TableNameAttribute tableNameAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(type, typeof(TableNameAttribute));

            if (tableNameAttribute != null)
            {
                return tableNameAttribute.Value;
            }

            throw new Exception($"Some internal error occurred, Please contact customer support!");
        }


        #region ExpressionConversions
        public static string GetWhereClause<T>(Expression<Func<T, bool>> predicate)
        {
            StringBuilder whereClause = new StringBuilder();
            BuildWhereClause(predicate.Body, whereClause);
            return whereClause.ToString();
        }

        private static void BuildWhereClause(Expression expression, StringBuilder whereClause)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                BuildWhereClause(binaryExpression.Left, whereClause);
                whereClause.Append(" ").Append(GetSqlOperator(binaryExpression.NodeType)).Append(" ");
                BuildWhereClause(binaryExpression.Right, whereClause);
            }
            else if (expression is MemberExpression memberExpression)
            {
                whereClause.Append(GetColumnName(memberExpression.Member.Name)).Append(" ");
            }
            else if (expression is ConstantExpression constantExpression)
            {
                whereClause.Append(FormatParameterValue(constantExpression.Value)).Append(" ");
            }
            else if (expression is MethodCallExpression methodCallExpression)
            {
                BuildMethodCallExpression(methodCallExpression, whereClause);
            }
            else if (expression is UnaryExpression unaryExpression)
            {
                BuildUnaryExpression(unaryExpression, whereClause);
            }
            else if (expression is ConditionalExpression conditionalExpression)
            {
                BuildConditionalExpression(conditionalExpression, whereClause);
            }
            else
            {
                throw new NotSupportedException($"Unsupported expression type: {expression.GetType().Name}");
            }
        }

        private static void BuildMethodCallExpression(MethodCallExpression methodCallExpression, StringBuilder whereClause)
        {
            var methodName = methodCallExpression.Method.Name;
            var arguments = methodCallExpression.Arguments;

            switch (methodName)
            {
                case "ToLower":
                    BuildMethodCallToLower(arguments[0], whereClause);
                    break;
                // Add handling for other methods as needed
                default:
                    throw new NotSupportedException($"Unsupported method: {methodName}");
            }
        }

        private static void BuildMethodCallToLower(Expression argument, StringBuilder whereClause)
        {
            // Handle string method ToLower() for case-insensitive comparisons
            BuildWhereClause(argument, whereClause);
            whereClause.Append(" COLLATE NOCASE "); // Example for SQL Server
        }

        private static void BuildUnaryExpression(UnaryExpression unaryExpression, StringBuilder whereClause)
        {
            whereClause.Append(GetSqlOperator(unaryExpression.NodeType));
            BuildWhereClause(unaryExpression.Operand, whereClause);
        }

        private static void BuildConditionalExpression(ConditionalExpression conditionalExpression, StringBuilder whereClause)
        {
            whereClause.Append("(");
            BuildWhereClause(conditionalExpression.Test, whereClause);
            whereClause.Append(" ? ");
            BuildWhereClause(conditionalExpression.IfTrue, whereClause);
            whereClause.Append(" : ");
            BuildWhereClause(conditionalExpression.IfFalse, whereClause);
            whereClause.Append(")");
        }

        private static string GetSqlOperator(ExpressionType nodeType)
        {
            // Map .NET expression types to SQL operators
            switch (nodeType)
            {
                case ExpressionType.Equal: return "=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                // ... Add mappings for other operators
                default: throw new ArgumentException("Unsupported operator");
            }
        }

        private static string GetColumnName(string propertyName)
        {
            // Map property names to column names (if necessary)
            return propertyName; // Assuming direct mapping here
        }

        private static string FormatParameterValue(object value)
        {
            // Format parameter values appropriately for SQL (e.g., string literals)
            return $"'{value}'"; // Example for string literals
        }

        // ... Other helper methods (GetSqlOperator, GetColumnName, FormatParameterValue)
        #endregion

    }
    #region Caching
    public class DataRepository
    {
        private readonly ObjectCache _cache;

        public DataRepository()
        {
            _cache = MemoryCache.Default;
            LoadAll();
        }

        public List<T> GetAll<T>(string cacheKey)
        {
            List<T> cachedData = _cache.Get(cacheKey) as List<T>;

            if (cachedData == null)
            {
                // Data is not in cache, fetch all data from the database
                using (var db = GetDbConnection())
                {
                    cachedData = db.Fetch<T>();
                }

                // Store all data in the cache with a specific expiration time (e.g., 10 minutes)
                _cache.Add(cacheKey, cachedData, DateTimeOffset.Now.AddMinutes(10));
            }

            return cachedData;
        }

        public List<T> RefreshSingleton<T>(string cacheKey)
        {
            List<T> cachedData = _cache.Get(cacheKey) as List<T>;

            if (cachedData is not null)
            {
                _cache.Remove(cacheKey);

                // Data is not in cache, fetch all data from the database
                using (var db = GetDbConnection())
                {
                    cachedData = db.Fetch<T>();
                }

                // Store all data in the cache with a specific expiration time (e.g., 10 minutes)
                _cache.Add(cacheKey, cachedData, DateTimeOffset.Now.AddMinutes(10));
            }
            else
            {
                using (var db = GetDbConnection())
                {
                    cachedData = db.Fetch<T>();
                }

                _cache.Add(cacheKey, cachedData, DateTimeOffset.Now.AddMinutes(10));
            }

            return cachedData;
        }


        private void LoadAll()
        {
            // Get the assembly where your PetaPoco entities are defined
            Assembly assembly = Assembly.GetAssembly(typeof(Core.Domain.Shop.Coupon)); //.GetExecutingAssembly();

            // Find all types with TableNameAttribute in the assembly
            var typesWithTableNameAttribute = assembly.GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(TableNameAttribute)));

            // Store the types in a collection
            List<Type> entityTypes = typesWithTableNameAttribute.ToList();


            using (var db = GetDbConnection())
            {
                // Iterate through each type and get the TableName attribute value
                foreach (var type in entityTypes)
                {
                    var tableNameAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(type, typeof(TableNameAttribute));

                    if (_cache.Any(x => x.Key.Equals(tableNameAttribute.Value)))
                        continue;

                    if (tableNameAttribute != null)
                    {
                        // Construct the Fetch<T> method using reflection
                        MethodInfo fetchMethod = typeof(IDatabase).GetInterfaces()
                            .SelectMany(i => i.GetMethods())
                            .FirstOrDefault(m => m.Name == "Fetch" && m.IsGenericMethod)
                            ?.MakeGenericMethod(type);

                        // Invoke the Fetch<T> method
                        var result = fetchMethod.Invoke(db, null);
                        _cache.Add(tableNameAttribute.Value, result, DateTimeOffset.Now.AddMinutes(10));
                    }
                }
            }
        }

        private IDatabase GetDbConnection()
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
    #endregion
}