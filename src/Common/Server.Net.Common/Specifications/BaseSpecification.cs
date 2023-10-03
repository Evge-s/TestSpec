using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Server.Net.Common.Specifications
{
    public class BaseSpecification<T> where T : class
    {
        public BaseSpecification(IEnumerable<FilterCriteria> filterCriteria, PaginationCriteria pagination)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            Expression combinedExpression = null;

            if (filterCriteria != null)
            {
                foreach (var criteria in filterCriteria)
                {
                    var property = typeof(T).GetProperties()
                        .FirstOrDefault(p => p.Name.ToLowerInvariant() == criteria.PropertyName.ToLowerInvariant());

                    if (property != null)
                    {
                        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                        var constant = Expression.Constant(criteria.Value);
                        Expression condition = null;

                        switch (criteria.CompareMethod)
                        {
                            case StringCompareMethod.Contains:
                                condition = Expression.Call(propertyAccess, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant);
                                break;
                            case StringCompareMethod.StartsWith:
                                condition = Expression.Call(propertyAccess, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant);
                                break;
                            case StringCompareMethod.EndsWith:
                                condition = Expression.Call(propertyAccess, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant);
                                break;
                            case StringCompareMethod.Equals:
                                condition = Expression.Equal(propertyAccess, constant);
                                break;
                            default:
                                break;
                        }

                        if (combinedExpression == null)
                            combinedExpression = condition;
                        else
                            combinedExpression = Expression.AndAlso(combinedExpression, condition);
                    }
                    else
                    {
                        combinedExpression = Expression.Constant(false);
                    }
                }
            }

            if (combinedExpression != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
                ToExpression = lambda;
            }
            else
            {
                ToExpression = e => true;
            }

            if (pagination != null)
            {
                Skip = pagination.Skip;
                Take = pagination.Take;
            }
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            query = query.Where(ToExpression);

            if (Take != 0)
            {
                query = query.Skip(Skip).Take(Take);
            }

            return query;
        }

        public Expression<Func<T, bool>> ToExpression { get; }
        public int Skip { get; }
        public int Take { get; }
    }

    public class FilterCriteria
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public StringCompareMethod CompareMethod { get; set; }
    }

    public class PaginationCriteria
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    // TODO: use SmartEnums
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StringCompareMethod
    {
        Contains,
        StartsWith,
        EndsWith,
        Equals
    }
}
