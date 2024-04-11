using API.Contracts;

namespace Client.Pages
{
    public partial class Home
    {

        // Used for simple test, XUnit does not exist yet in the project
        public static Country GenerateRandomCountryTyp(String countryName)
        {
            DataPoint point = new DataPoint
            {
                Key = "2023",
                Value = new Random().Next(200,400)
            };

            Data data = new Data() {
            Name = "Water",
            Unit = "L",
            Points = [point]
            };
            
            Country country = new Country() { 
                Code = "Lall",
                Name = countryName,
                Description = "Bestest country",
                Data = [data]
            };
            
            return country;
        }

    }
    
  
}
