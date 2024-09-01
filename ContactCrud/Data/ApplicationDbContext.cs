using ContactCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactCrud.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        //Agregar los modelos aqui (cada modelo corresponde a una tabla en la bd)

        //Se agrega el modelo Contact <Contact> y lo nombramos
        public DbSet<Contact> Contact { get; set;}
    }
}
