using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusalaSoft.Transpotation.Domain.Models
{
    [Table("DroneState")]
    public class DroneStateDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(100, ErrorMessage = "State can't be longer than 100 characters.")]
        public string State { get; set; } = string.Empty;
    }
}
