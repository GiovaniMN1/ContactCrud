using System.ComponentModel.DataAnnotations;

namespace ContactCrud.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="El correo es obligatorio")]
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        //Este modelo se va se a agregar a ApplicationDbContext, para hacer uso de el
    }
}
