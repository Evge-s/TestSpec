using System.Linq.Expressions;

namespace Server.Net.Common.Specifications
{
    [Serializable]
    public sealed class TrueSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return (T x) => true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            return GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}
