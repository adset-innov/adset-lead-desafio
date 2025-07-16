using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSET.DESAFIO.DOMAIN.Entities
{
    [Table("tb_optional")]
    public class Optional
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<CarOptional> CarOptionals { get; set; } = new List<CarOptional>();

        public Optional() { }

        public Optional(string name)
        {
            Name = name;
            CarOptionals = new List<CarOptional>();
        }
    }
}