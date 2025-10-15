namespace Vehicles;

/// <summary>
/// Contains vehicle registrations for plug-in and battery electric vehicles in Washington state.
/// Each vehicle has a unique Id, and each vehicle can be registered multiple times (People moving, or the vehicle being sold).
/// </summary>
public class VehicleRegistry
{
    private readonly VehicleDatabase db;

    public VehicleRegistry(VehicleDatabase db)
    {
        this.db = db;
    }

    public IEnumerable<Vehicle> GetVehicles() => db.GetVehicles();

    public IEnumerable<Vehicle> GetRegistrations() => db.GetRegistrations().SelectMany(list => list);

    /// <summary>
    /// Updates the tax for a given vehicle and year.
    /// </summary>
    /// <param name="vehicle">The vehicle whose tax should be calculated.</param>
    /// <param name="year">The tax year.</param>
    /// <returns>The tax amount for the year.</returns>
    public decimal CalculateTax(Vehicle vehicle, int year)
    {
        return year switch
        {
            2023 => Calculate2023Tax(vehicle),
            2024 => Calculate2024Tax(vehicle),
            2025 => Calculate2025Tax(vehicle),
            _ => throw new ArgumentOutOfRangeException(nameof(year),
                $"Taxes cannot be calculated for year {year}.")
        };
    }

    private static decimal Calculate2024Tax(Vehicle vehicle)
    {
        return (vehicle.EvType, vehicle.EvRange) switch
        {
            ("Plug-in Hybrid Electric Vehicle (PHEV)", _) => 200.0m,
            ("Battery Electric Vehicle (BEV)", >= 100) => 20.0m,
            ("Battery Electric Vehicle (BEV)", < 100) => 50.0m,
            _ => throw new ArgumentException(
                $"Can't calculate legacy tax for {vehicle.Id} ({vehicle.EvType}, 2024)")
        };
    }

    private static decimal Calculate2023Tax(Vehicle vehicle)
    {
        return (vehicle.EvType) switch
        {
            "Plug-in Hybrid Electric Vehicle (PHEV)" => 100.0m,
            "Battery Electric Vehicle (BEV)" => 10.0m,
            _ => throw new ArgumentException(
                $"Can't calculate legacy tax for {vehicle.Id} ({vehicle.EvType}, 2023)")
        };
    }

    private decimal Calculate2025Tax(Vehicle vehicle)
    {
        decimal baseTax = (vehicle.EvType, vehicle.IsCleanAlternativeFuelEligible) switch
        {
            ("Battery Electric Vehicle (BEV)", true) => 15.0m,
            ("Battery Electric Vehicle (BEV)", false) => 30.0m,
            ("Plug-in Hybrid Electric Vehicle (PHEV)", true) => 50.0m,
            ("Plug-in Hybrid Electric Vehicle (PHEV)", false) => 150.0m,
            _ => throw new ArgumentException(
                $"Can't calculate legacy tax for {vehicle.Id} ({vehicle.EvType}, 2025)")
        };

        if (vehicle.City.Equals("Seattle", StringComparison.OrdinalIgnoreCase))
            baseTax += 7.0m;

        if (db.GetVehicleCount(vehicle.Id) > 1)
            baseTax -= 10.0m;

        return baseTax;
    }

    /// <summary>
    /// Gets the most popular car model.
    /// </summary>
    /// <returns>The name (Make and model) of the most popular car.</returns>
    public string GetMostPopularModel(string? county = null)
    {
        var vehicles = db.GetVehicles();

        if (!string.IsNullOrEmpty(county))
        {
            vehicles = [.. vehicles.Where(v => string.Equals(v.County, county, StringComparison.OrdinalIgnoreCase))];
            if (!vehicles.Any())
                return $"The county '{county}' likely doesn’t exist.";
        }

        var counts = vehicles
                    .GroupBy(v => v.MakeAndModel)
                    .Select(g => new { Model = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .FirstOrDefault();

        if (counts == null)
            return "No vehicles found.";
        return counts.Model;
    }
}