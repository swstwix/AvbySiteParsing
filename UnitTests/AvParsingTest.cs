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
        public void MazdaMoeldDataExists()
        {
            CollectionAssert.Contains(AvParser.Models("Mazda"),
                                      new KeyValuePair<string, int>("323", 638));
        }

        [Test]
        public void UralDataExists()
        {
            CollectionAssert.Contains(AvParser.Brands(), new KeyValuePair<string, int>("Урал", 1569));
        }

        [TestCase("Alfa Romeo", "145", 4)]
        [TestCase("Mazda", "323", 100)]
        public void PageCorrectData(string brandName, string modelName, int minCount)
        {
            Assert.That(AvParser.Selling(brandName, modelName).Length, Is.GreaterThanOrEqualTo(minCount));
        }

        [Test]
        public void TestImageUrl()
        {
            var car = AvParser.Selling("Alfa Romeo", "145").First();
            StringAssert.Contains("http://static.av.by/public/public_image_icon", car.ImageHref);
        }

        [Test]
        public void TestAllLoadedCarsIsNew()
        {
            var cars = AvParser.Selling("Alfa Romeo", "145");
            foreach (var car in cars)
            {
                Assert.AreEqual(CarState.New, car.State);
            }
        }
    }
}