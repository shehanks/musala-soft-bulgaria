using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.Domain.Models
{
    [Table("Medication")]
    public class MedicationDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_-]*$")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Weight is required.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [StringLength(100, ErrorMessage = "Code can't be longer than 100 characters.")]
        [RegularExpression(@"^[A-Z0-9-]*$")]
        public string Code { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        public int DroneTripId { get; set; }
    }
}
