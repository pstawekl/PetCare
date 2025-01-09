using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetCare
{
    public class Reminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PetId { get; set; }
        public string Message { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsSent { get; set; }
    }

    public class ReminderCreateDto
    {
        [Required]
        public int PetId { get; set; }
        public string Message { get; set; }
        public DateTime ReminderDate { get; set; }

        public bool IsSent { get; set; }

    }
}