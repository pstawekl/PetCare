using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetCare
{
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PetId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class VisitCreateDto
    {
        [Required]
        public int PetId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
