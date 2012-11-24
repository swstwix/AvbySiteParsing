using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WpfApplication1.Static;

namespace UnitTests
{
    [TestFixture]
    public class AvParsingTest
    {
        [Test]
        public void AlfaRomeoModelsDataExists()
        {
            CollectionAssert.Contains(AvParser.Models("Alfa Romeo"), new KeyValuePair<string, int>("146", 3));
        }

        [Test]
        public void BmwModelsDataExists()
        {
            CollectionAssert.Contains(AvParser.Models("BMW"),
                                      new KeyValuePair<string, int>("3-reihe (E36 Touring)", 1666));
        }

        [Test]
        public void UralDataExists()
        {
            CollectionAssert.Contains(AvParser.Brands(), new KeyValuePair<string, int>("Урал", 1569));
        }

        [TestCase("Alfa Romeo", "145", Result = 9)]
        [TestCase("Audi", "100", Result = 328)]
        public int PageCorrectData(string brandName, string modelName)
        {
            return AvParser.Selling(brandName, modelName).Count();
        }

        [Test]
        public void TestImageUrl()
        {
            var car = AvParser.Selling("Alfa Romeo", "145").First();
            Assert.AreEqual("http://static.av.by/public/public_image_icon/003/79/33/public_3793365_1i_7add9c38b879.jpg",
                            car.ImageHref);
        }
    }
}