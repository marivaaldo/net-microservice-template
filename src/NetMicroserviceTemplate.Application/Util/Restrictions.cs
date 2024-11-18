using DataQI.Commons.Query.Support;
using DataQI.Commons.Query;
using System.Linq.Expressions;

namespace NetMicroserviceTemplate.Application.Util;

public static class Restrictions<T>
{
    public static ICriterion Between(Expression<Func<T, object?>> predicate, object starts, object ends)
    {
        return new BetweenExpression(GetPropertyName(predicate), starts, ends);
    }

    public static ICriterion Containing(Expression<Func<T, object?>> predicate, object value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.Containing, value);
    }

    public static ICriterion EndingWith(Expression<Func<T, object?>> predicate, string value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.EndingWith, value);
    }

    public static ICriterion Equal(Expression<Func<T, object?>> predicate, object value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.Equal, value);
    }

    public static ICriterion GreaterThan(Expression<Func<T, object?>> predicate, object value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.GreaterThan, value);
    }

    public static ICriterion GreaterThanEqual(Expression<Func<T, object?>> predicate, object value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.GreaterThanEqual, value);
    }

    public static ICriterion In(Expression<Func<T, object?>> predicate, object[] values)
    {
        return new InExpression(GetPropertyName(predicate), values);
    }

    public static ICriterion LessThan(Expression<Func<T, object?>> predicate, object value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.LessThan, value);
    }

    public static ICriterion LessThanEqual(Expression<Func<T, object?>> predicate, object value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.LessThanEqual, value);
    }

    public static ICriterion Like(Expression<Func<T, object?>> predicate, string value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.Like, value);
    }

    public static ICriterion Not(ICriterion criterion)
    {
        return new NotExpression(criterion);
    }

    public static ICriterion Null(Expression<Func<T, object?>> predicate)
    {
        return new NullExpression(GetPropertyName(predicate));
    }

    public static ICriterion StartingWith(Expression<Func<T, object?>> predicate, object value)
    {
        return new SimpleExpression(GetPropertyName(predicate), WhereOperator.StartingWith, value);
    }

    public static IJunction Conjunction()
    {
        return new Conjunction();
    }

    public static IJunction Disjunction()
    {
        return new Disjunction();
    }

    private static string GetPropertyName(Expression<Func<T, object?>> predicate)
    {
        // e.g., x => x.Property > 0
        if (predicate.Body is BinaryExpression binaryExpression)
        {
            if (binaryExpression.Left is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (binaryExpression.Left is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                return unaryMemberExpression.Member.Name;
            }
        }
        // e.g., x => x.Property
        else if (predicate.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        // e.g., x => !x.Property
        else if (predicate.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
        {
            return unaryMemberExpression.Member.Name;
        }

        throw new ArgumentException("Property name not found");
    }
}
