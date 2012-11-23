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
        public void UralDataExists()
        {
            CollectionAssert.Contains(AvParser.Brands(), new KeyValuePair<string, int>("Урал", 1569));
        }

        [Test]
        public void AlfaRomeoModelsDataExists()
        {
            CollectionAssert.Contains(AvParser.Models("Alfa Romeo"), new KeyValuePair<string, int>("146", 3));
        }

        [Test]
        public void BmwModelsDataExists()
        {
            CollectionAssert.Contains(AvParser.Models("BMW"), new KeyValuePair<string, int>("3-reihe (E36 Touring)", 1666));
        }
    }
}
