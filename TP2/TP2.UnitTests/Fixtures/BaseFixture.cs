using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.UnitTests.Fixtures
{
    public class BaseFixture
    {
        protected Fixture Fixture;
        public BaseFixture()
        {
            Fixture = new Fixture();
        }
    }
}
