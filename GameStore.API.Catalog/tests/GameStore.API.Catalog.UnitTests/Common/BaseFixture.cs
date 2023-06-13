using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.API.Game.UnitTests.Common
{
    public abstract class BaseFixture
    {
        public Faker Faker { get; private set; }

        public BaseFixture() => Faker = new Faker("pt_BR");
    }
}
