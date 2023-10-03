using System.Linq.Expressions;

namespace Server.Net.Common.Specifications
{
    public abstract class Specification<T>
    {
        public static readonly Specification<T> True = new TrueSpecification<T>();

        public static readonly Specification<T> False = new FalseSpecification<T>();

        public abstract Expression<Func<T, bool>> ToExpression();

        public virtual Func<T, bool> ToPredicate()
        {
            return ToExpression().Compile();
        }

        public bool IsSatisfiedBy(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return ToPredicate()(obj);
        }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
        {
            return spec?.ToExpression() 
                ?? throw new ArgumentNullException("spec");
        }

        public static bool operator false(Specification<T> spec)
        {
            return false;
        }

        public static bool operator true(Specification<T> spec)
        {
            return false;
        }

        public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
        {
            return new AndSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
        {
            return new OrSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator !(Specification<T> spec)
        {
            return new NotSpecification<T>(spec);
        }
    }
}
