using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
