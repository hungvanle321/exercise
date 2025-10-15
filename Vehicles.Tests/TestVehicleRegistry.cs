namespace Vehicles.Tests
{
   [TestClass]
   public class TestVehicleRegistry
   {
      private readonly VehicleRegistry registry;

      public TestVehicleRegistry()
      {
         registry = new VehicleRegistry(new VehicleDatabase());
      }

      [TestMethod]
      public void ShouldLoadData()
      {
         Assert.AreEqual(11060, registry.GetVehicles().Count(), "Unexpected number of unique vehicles in db");
         Assert.AreEqual(181458, registry.GetRegistrations().Count(), "Unexpected number of registrations in db");
      }

      [TestMethod]
      public void ShouldCalculate2023TaxCorrectly()
      {
         foreach(var v in registry.GetVehicles())
         {
            registry.UpdateTax(v, 2023);
         }
         Assert.AreEqual(531530.0m, registry.GetVehicles().Sum(v => v.Tax));
      }

      [TestMethod]
      public void ShouldCalculate2024TaxCorrectly()
      {
         foreach (var v in registry.GetVehicles())
         {
            registry.UpdateTax(v, 2024);
         }
         Assert.AreEqual(1205890.0m, registry.GetVehicles().Sum(v => v.Tax));
      }

      [TestMethod]
      public void ShouldCalculate2025TaxCorrectly()
      {
         Assert.Fail("TODO! 2025 tax code isn't implemented");
      }

      [TestMethod]
      public void ShouldFindMostPopularModel()
      {
         Assert.AreEqual("TESLA MODEL S", registry.GetMostPopularModel());
      }
   }
}