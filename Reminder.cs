using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetCare
{
    // Klasa reprezentująca przypomnienie
    public class Reminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Identyfikator przypomnienia
        public int PetId { get; set; } // Identyfikator zwierzęcia
        public string Message { get; set; } // Wiadomość przypomnienia
        public DateTime ReminderDate { get; set; } // Data przypomnienia
        public bool IsSent { get; set; } // Status wysłania przypomnienia
    }

    // DTO do tworzenia przypomnienia
    public class ReminderCreateDto
    {
        [Required]
        public int PetId { get; set; } // Identyfikator zwierzęcia
        public string Message { get; set; } // Wiadomość przypomnienia
        public DateTime ReminderDate { get; set; } // Data przypomnienia
        public bool IsSent { get; set; } // Status wysłania przypomnienia
    }
}