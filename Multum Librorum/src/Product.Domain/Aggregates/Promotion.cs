using CQRS.Core.Domain;
using Product.Domain.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Aggregates
{
    public  class Promotion: AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public virtual List<Book> PromotedBooks { get; private set; }
        public DateTime RegDate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Guid UserId { get; private set; }
    }
}
