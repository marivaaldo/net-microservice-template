using DataQI.Commons.Query;
using System.Linq.Expressions;

namespace NetMicroserviceTemplate.Application.Extensions;

internal static class CriteriaExtensions
{
    public static ICriteria AddLikeArray<T>(this ICriteria criteria, Expression<Func<T, object?>> predicate, string[] values)
    {
        if (values == null || values.Length == 0)
            return criteria;

        var junctionAnd = Restrictions<T>.Conjunction();
        var junctionOr = Restrictions<T>.Disjunction();

        foreach (var value in values)
            junctionOr.Add(Restrictions<T>.Like(predicate, value));

        junctionAnd.Add(junctionOr);
        criteria.Add(junctionAnd);

        return criteria;
    }
}
