using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface TEntity
    {
        public Guid GetId();
        public void SetId(Guid id);
    }
}
