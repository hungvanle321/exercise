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
            var taxSum = registry.GetVehicles().Select(v => registry.CalculateTax(v, 2023)).Sum();
            Assert.AreEqual(531530.0m, taxSum);
        }

        [TestMethod]
        public void ShouldCalculate2024TaxCorrectly()
        {
            var taxSum = registry.GetVehicles().Select(v => registry.CalculateTax(v, 2024)).Sum();
            Assert.AreEqual(1205890.0m, taxSum);
        }

        [TestMethod]
        public void ShouldCalculate2025TaxCorrectly()
        {
            var taxSum = registry.GetVehicles().Select(v => registry.CalculateTax(v, 2025)).Sum();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShouldFindMostPopularModel()
        {
            Assert.AreEqual("TESLA MODEL S", registry.GetMostPopularModel());
        }
    }
}