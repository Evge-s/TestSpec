using System.Linq.Expressions;

namespace Server.Net.Common.Specifications
{
    [Serializable]
    internal class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> spec1;

        private readonly Specification<T> spec2;

        public AndSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            this.spec1 = spec1 ?? throw new ArgumentNullException("spec1");
            this.spec2 = spec2 ?? throw new ArgumentNullException("spec2");
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> first = spec1.ToExpression();
            Expression<Func<T, bool>> second = spec2.ToExpression();

            BinaryExpression andExp = Expression.AndAlso(first.Body, second.Body);

            return Expression.Lambda<Func<T, bool>>(andExp, first.Parameters);
        }

        public override bool Equals(object other)
        {
            if (other == null)            
                return false;            

            if (this == other)            
                return true;            

            AndSpecification<T> andSpecification = other as AndSpecification<T>;
            if (andSpecification != null)
            {
                if (spec1.Equals(andSpecification.spec1))                
                    return spec2.Equals(andSpecification.spec2);                

                return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return spec1.GetHashCode() ^ spec2.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}
