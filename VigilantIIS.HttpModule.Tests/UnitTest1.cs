using System;
using System.Web;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace VigilantIIS.HttpModule.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestMethod1()
        {
            var mongoclient = A.Fake<IMongoClient>();
            var context = A.Fake<HttpApplication>();
            A.CallTo(() => mongoclient.GetDatabase("dashboard", null)).Throws(new Exception("fail"));

            var underTest = new HttpModule(mongoclient);

            underTest.Init(context);
        }
    }
}
