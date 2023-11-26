using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entitites
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
    }
}
