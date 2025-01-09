using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime BirthDate { get; set; }
    public string Owner { get; set; }
}

public class PetCreateDto
{
    [Required]
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime BirthDate { get; set; }
    public string Owner { get; set; }
}