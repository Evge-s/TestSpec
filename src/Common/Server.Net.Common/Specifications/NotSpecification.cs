using System.Linq.Expressions;

namespace Server.Net.Common.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> spec;

        public NotSpecification(Specification<T> spec)
        {
            this.spec = spec ?? throw new ArgumentNullException("spec");
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = spec.ToExpression();
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)            
                return false;            

            if (this == obj)            
                return true;            

            NotSpecification<T> notSpecification = obj as NotSpecification<T>;

            if (notSpecification != null)            
                return spec.Equals(notSpecification.spec);            

            return false;
        }

        public override int GetHashCode()
        {
            return spec.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}
