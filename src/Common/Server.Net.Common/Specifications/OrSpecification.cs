using System.Linq.Expressions;

namespace Server.Net.Common.Specifications
{
    [Serializable]
    internal class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> spec1;

        private readonly Specification<T> spec2;

        public OrSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            this.spec1 = spec1 ?? throw new ArgumentNullException("spec1");
            this.spec2 = spec2 ?? throw new ArgumentNullException("spec2");
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> first = spec1.ToExpression();
            Expression<Func<T, bool>> second = spec2.ToExpression();

            BinaryExpression orExp = Expression.OrElse(
                Expression.Invoke(first, first.Parameters),
                Expression.Invoke(second, second.Parameters)
            );

            return Expression.Lambda<Func<T, bool>>(orExp, first.Parameters);
        }

        public override bool Equals(object other)
        {
            if (other == null)            
                return false;

            if (this == other)            
                return true;            

            OrSpecification<T> orSpecification = other as OrSpecification<T>;
            if (orSpecification != null)
            {
                if (spec1.Equals(orSpecification.spec1))                
                    return spec2.Equals(orSpecification.spec2);                

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
