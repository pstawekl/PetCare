using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetCare
{
    // Klasa reprezentująca użytkownika
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Identyfikator użytkownika
        public string Username { get; set; } // Nazwa użytkownika
        public string Email { get; set; } // Email użytkownika
        public string PasswordHash { get; set; } // Hash hasła użytkownika
        public string FirstName { get; set; } // Imię użytkownika
        public string LastName { get; set; } // Nazwisko użytkownika
    }

}

// DTO do tworzenia użytkownika
public class UserCreateDto
{
    [Required]
    public string Username { get; set; } // Nazwa użytkownika
    [Required]
    public string Email { get; set; } // Email użytkownika
    [Required]
    public string PasswordHash { get; set; } // Hash hasła użytkownika
    [Required]
    public string FirstName { get; set; } // Imię użytkownika
    [Required]
    public string LastName { get; set; } // Nazwisko użytkownika
}