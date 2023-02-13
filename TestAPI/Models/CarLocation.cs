namespace TestAPI.Models
{
    public class CarLocation : BaseModel
    {
        public int CarDriverId { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class CarLocationRequest
    {
        public int CarDriverId { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
