using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetCare
{
    // Klasa reprezentująca wizytę
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Identyfikator wizyty
        public int PetId { get; set; } // Identyfikator zwierzęcia
        public DateTime VisitDate { get; set; } // Data wizyty
        public string Description { get; set; } // Opis wizyty
        public bool IsCompleted { get; set; } // Status ukończenia wizyty
        public string VetName { get; set; } // Nazwa weterynarza
    }

    // DTO do tworzenia wizyty
    public class VisitCreateDto
    {
        [Required]
        public int PetId { get; set; } // Identyfikator zwierzęcia
        public DateTime VisitDate { get; set; } // Data wizyty
        public string Description { get; set; } // Opis wizyty
        public bool IsCompleted { get; set; } // Status ukończenia wizyty
        public string VetName { get; set; } // Nazwa weterynarza
    }
}
