using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;
[Table(name:"contacts")]
public class ContactEntity
{
   
    public int Id{ get; set; }
    [Required]
    [MaxLength(length: 20)]
    
  
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(length: 50)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
    [Display(Name = "Adres e-mail",Order = 4)]
    public string Email { get; set; }
    
    [Column(name:"phone")]
    
   
    public string phoneNumber { get; set; }
    
   
   
    public DateOnly Birthday { get; set; }
    
    public Category Category { get; set; }
    public DateTime Created { get; set; }
}
