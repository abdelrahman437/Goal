using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Goal.Core.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Path is required.")]
        public string Path { get; set; }

        public bool IsMain { get; set; } = false;
        [Required(ErrorMessage = "Public ID is required.")]

        public string PublicId { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
