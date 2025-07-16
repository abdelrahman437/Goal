using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Goal.Core.Common;

namespace Goal.Core.Models
{
    public class Brand : ISoftDeleteable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters.")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        [JsonIgnore]
        public virtual List<Product> Product { get; set; } = new();
    }
}
