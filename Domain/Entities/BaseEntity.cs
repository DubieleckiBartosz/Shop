using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
