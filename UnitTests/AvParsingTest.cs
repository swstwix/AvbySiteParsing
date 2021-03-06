﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using AvByApi;
using Domain.Api;
using NUnit.Framework;
using WpfApplication1;
using WpfApplication1.Static;

namespace UnitTests
{
    [TestFixture]
    public class AvParsingTest
    {
        private ICarsApi carsApi = new AvParser();

        [Test]
        public void WebClientWorks()
        {
            var x = new WebClient().DownloadString("http://google.by");
            Assert.NotNull(x);
        }

        [Test]
        public void WebClientWorksWithAvBy()
        {
            var x = new WebClient().DownloadString("http://av.by");
            Assert.NotNull(x);
        }

        [Test]
        public void AvBySiteContainsModelWorld()
        {
            var client = new WebClient() { Encoding = Encoding.GetEncoding("windows-1251") };
            var s = client.DownloadString("http://av.by/?event=Show_Main");
            StringAssert.Contains("Модель", s, "Не работает с русскими");
        }

        [Test]
        public void AlfaRomeoModelsDataExists()
        {
            CollectionAssert.Contains(carsApi.Models("Alfa Romeo"), new KeyValuePair<string, int>("146", 3));
        }

        [Test]
        public void ReclamTest()
        {
            Assert.IsTrue(carsApi.Brands().Any());
        }

        [Test]
        public void BmwModelsDataExists()
        {
            CollectionAssert.Contains(carsApi.Models("BMW"),
                                      new KeyValuePair<string, int>("3-reihe (E36 Touring)", 1666));
        }

        [Test]
        public void MazdaMoeldDataExists()
        {
            CollectionAssert.Contains(carsApi.Models("Mazda"),
                                      new KeyValuePair<string, int>("323", 638));
        }

        [Test]
        public void UralModelsDataExists()
        {
            CollectionAssert.Contains(carsApi.Models("ВАЗ"),
                                      new KeyValuePair<string, int>("2106", 1287));
        }

        [Test]
        public void UralDataExists()
        {
            CollectionAssert.Contains(carsApi.Brands(), new KeyValuePair<string, int>("Урал", 1569));
        }

        [TestCase("Alfa Romeo", "145", 4)]
        [TestCase("Mazda", "323", 100)]
        public void PageCorrectData(string brandName, string modelName, int minCount)
        {
            Assert.That(carsApi.Selling(brandName, modelName).Length, Is.GreaterThanOrEqualTo(minCount));
        }

        [Test]
        public void TestImageUrl()
        {
            var car = carsApi.Selling("Alfa Romeo", "145").First();
            StringAssert.Contains("http://static.av.by/public", car.ImageHref);
        }

        [Test]
        public void TestAllLoadedCarsIsNew()
        {
            var cars = carsApi.Selling("Alfa Romeo", "145");
            foreach (var car in cars)
            {
                Assert.AreEqual(CarState.New, car.State);
            }
        }
    }
}