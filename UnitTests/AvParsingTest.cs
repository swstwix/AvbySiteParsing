using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApplication1.Static;

namespace UnitTests
{
    [TestFixture]
    public class AvParsingTest
    {
        [Test]
        public void AlfaRomeoDataExists()
        {
            CollectionAssert.Contains(AvParser.Brands(), new KeyValuePair<string, int>("Урал", 1569));
        }
    }
}
