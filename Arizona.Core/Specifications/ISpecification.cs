using Arizona.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Specifications
{
    public interface ISpecification <T> where T : BaseEntity
    {
        //property signature for each and every spec 

        public Expression<Func<T , bool>> Criteria { get; set; } //Where 

        public List<Expression<Func<T , object>>> Includes { get; set; } //includes

        public Expression<Func<T , object>> OrderBy { get; set; }
        public Expression<Func<T , object>> OrderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
