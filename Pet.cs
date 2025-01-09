using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Klasa reprezentująca zwierzę
public class Pet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Identyfikator zwierzęcia
    [Required]
    public string Name { get; set; } // Nazwa zwierzęcia
    public string Type { get; set; } // Typ zwierzęcia (np. pies, kot)
    public DateTime BirthDate { get; set; } // Data urodzenia zwierzęcia
    public string Owner { get; set; } // Właściciel zwierzęcia
}

// Klasa DTO do tworzenia nowego zwierzęcia
public class PetCreateDto
{
    [Required]
    public string Name { get; set; } // Nazwa zwierzęcia
    public string Type { get; set; } // Typ zwierzęcia
    public DateTime BirthDate { get; set; } // Data urodzenia zwierzęcia
    public string Owner { get; set; } // Właściciel zwierzęcia
}