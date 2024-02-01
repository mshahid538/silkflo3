using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SilkFlo.Extensions.Helpers
{
    public static class PredicateTranslator
    {
        public static string Translate<T>(Func<T, bool> predicate)
        {
            return ""; // PredicateParser<T>.Parse(predicate.ToString()).ToString();
        }
        //public static SqlWhereClause Translate<T>(Func<T, bool> predicate)
        //{
        //    var visitor = new PredicateTranslatorVisitor<T>();
        //    visitor.Visit(predicate);
        //    return visitor.GetSql();
        //}
    }

    //public class PredicateTranslatorVisitor<T>
    //{
    //    private readonly List<object> parameters = new List<object>();
    //    private readonly StringBuilder sqlBuilder = new StringBuilder();

    //    public void Visit(Func<T, bool> predicate)
    //    {
    //        var parameter = Expression.Parameter(typeof(T));
    //        var body = predicate.Invoke(parameter);

    //        Visit(body);
    //    }

    //    public void Visit(Expression expression)
    //    {
    //        if (expression is BinaryExpression binaryExpression)
    //        {
    //            VisitBinaryExpression(binaryExpression);
    //        }
    //        else if (expression is MemberExpression memberExpression)
    //        {
    //            VisitMemberExpression(memberExpression);
    //        }
    //        // Add more cases as needed

    //        // Handle constant expressions (e.g., true, false)
    //        else if (expression is ConstantExpression constantExpression)
    //        {
    //            parameters.Add(constantExpression.Value);
    //            sqlBuilder.Append($"@p{parameters.Count - 1}");
    //        }
    //    }

    //    private void VisitBinaryExpression(BinaryExpression binaryExpression)
    //    {
    //        Visit(binaryExpression.Left);

    //        switch (binaryExpression.NodeType)
    //        {
    //            case ExpressionType.Equal:
    //                sqlBuilder.Append(" = ");
    //                break;
    //            case ExpressionType.GreaterThan:
    //                sqlBuilder.Append(" > ");
    //                break;
    //            // Add more cases as needed
    //            default:
    //                throw new NotSupportedException($"Unsupported binary expression: {binaryExpression.NodeType}");
    //        }

    //        Visit(binaryExpression.Right);
    //    }

    //    private void VisitMemberExpression(MemberExpression memberExpression)
    //    {
    //        if (memberExpression.Expression is ConstantExpression constantExpression)
    //        {
    //            var value = Expression.Lambda(memberExpression).Compile().DynamicInvoke();
    //            parameters.Add(value);
    //            sqlBuilder.Append($"@p{parameters.Count - 1}");
    //        }
    //        else
    //        {
    //            sqlBuilder.Append(memberExpression.Member.Name);
    //        }
    //    }

    //    public SqlWhereClause GetSql()
    //    {
    //        return new SqlWhereClause
    //        {
    //            Sql = sqlBuilder.ToString(),
    //            Arguments = parameters.ToArray()
    //        };
    //    }
    //}

    //public class SqlWhereClause
    //{
    //    public string Sql { get; set; }
    //    public object[] Arguments { get; set; }
    //}
}
