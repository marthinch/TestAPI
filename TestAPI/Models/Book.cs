namespace TestAPI.Models
{
    public class Book : BaseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public int CarDriverId { get; set; }

        public string From { get; set; }
        public double FromLongitude { get; set; }
        public double FromLatitude { get; set; }

        public string To { get; set; }
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }

        public double Distance { get; set; }
    }

    public class BookRequest
    {
        public int UserId { get; set; }
        public int CarDriverId { get; set; }

        public string From { get; set; }
        public double FromLongitude { get; set; }
        public double FromLatitude { get; set; }

        public string To { get; set; }
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
    }
}
