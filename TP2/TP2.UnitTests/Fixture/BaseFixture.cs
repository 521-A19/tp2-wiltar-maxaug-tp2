using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.UnitTests.Fixture
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
