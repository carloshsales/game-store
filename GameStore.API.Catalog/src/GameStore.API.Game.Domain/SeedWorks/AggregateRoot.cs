using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.API.Game.Domain.SeedWorks
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot() : base() { }
    }
}
