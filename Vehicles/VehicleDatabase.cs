using System.Reflection;

namespace Vehicles;

public class VehicleDatabase
{
   private List<List<Vehicle>> vehicles;

   public VehicleDatabase()
   {
      vehicles = new List<List<Vehicle>>();
      string csvPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ev.csv");
      Load(csvPath);
   }

   /// <summary>
   /// Loads the vehicle data from disk. 
   /// NOTE: the registrations are in chronological order with newest first. That is, the first occurrence of a vehicle
   ///       will be the current registration for that vehicle. Any subsequent occurrence is a previous owner etc.
   /// </summary>
   /// <param name="csvPath"></param>
   private void Load(string csvPath)
   {
      var lines = File.ReadAllLines(csvPath);
      foreach (string row in lines.Skip(1))
      {
         var columns = row.Split(',');
         string id = columns[0];
         string county = columns[1];
         string city = columns[2];
         string state = columns[3];
         int modelYear = int.Parse(columns[5]);
         string make = columns[6];
         string model = columns[7];
         string evType = columns[8];
         int evRange = int.Parse(columns[10]);
         var entry = new Vehicle { Id = id, State = state, City = city, County = county, Make = make, Model = model, ModelYear = modelYear, EvType = evType, EvRange = evRange };

         bool added = false;
         foreach(var list in vehicles)
         {
            if (list[0].Id == id)
            {
               list.Add(entry);
               added = true;
               break;
            }
         }
         if (!added)
         {
            List<Vehicle> list = new List<Vehicle>();
            list.Add(entry);
            vehicles.Add(list);
         }
      }
   }

   public List<List<Vehicle>> GetRegistrations() => vehicles;

   public IEnumerable<Vehicle> GetVehicles() => vehicles.Select(x => x[0]);
}
