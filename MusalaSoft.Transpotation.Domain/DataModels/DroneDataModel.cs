using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusalaSoft.Transpotation.Domain.Models
{
    [Table("Drone")]
    public class DroneDataModel
    {
        public DroneDataModel()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Serial number is required.")]
        [StringLength(100, ErrorMessage = "Serial number can't be longer than 100 characters.")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model id is required.")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Battery percentage is required.")]
        [Range(0, 100, ErrorMessage = "Battery percentage should be between 0 to 100.")]
        public decimal BatteryPercentage { get; set; }

        [Required(ErrorMessage = "State id is required.")]
        public int StateId { get; set; }

        public int? CurrentTripId { get; set; }
    }
}