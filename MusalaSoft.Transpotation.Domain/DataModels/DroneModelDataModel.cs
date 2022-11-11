using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusalaSoft.Transpotation.Domain.Models
{
    [Table("DroneModel")]
    public class DroneModelDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(100, ErrorMessage = "Model can't be longer than 100 characters.")]
        public string Model { get; set; } = string.Empty;
    }
}
