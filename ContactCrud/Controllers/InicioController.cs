using ContactCrud.Data;
using ContactCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ContactCrud.Controllers
{
    public class InicioController : Controller
    {

        private readonly ApplicationDbContext _contexto;
        // Con _context accedemos a la BD (llamamos al contexto)

        // Constructor del controlador donde se inyecta el contexto de la base de datos
        public InicioController(ApplicationDbContext contexto)
        {
            // Asignamos el contexto inyectado a la variable privada _context para usarlo en los métodos del controlador
            _contexto = contexto;
        }

        //public IActionResult Index()
        //{
        //    //Obtiene todos los registros de la tabla Contact de la base de datos, los convierte en una lista, y luego pasa esta lista a una vista

        //    return View(_contexto.Contact.ToList());
        //}

        //Usando metodos asincronos
        public async Task<IActionResult> Index()
        {
            //Obtiene todos los registros de la tabla Contact de la base de datos, los convierte en una lista, y luego pasa esta lista a una vista

            return View(await _contexto.Contact.ToListAsync());
        }

        //Creacion del metodo Crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        //Crecion del metodo Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Se pasa como parametro el modelo
        public async Task <IActionResult> Crear(Contact contact)
        {
            //Se hace una validacion sobre el modelo 
            if (ModelState.IsValid)
            {
                //Agregamos la fecha y hora actual
                contact.CreationDate = DateTime.Now;
                 //Guardamos en la Base de Datos
                //pasando el contacto desde la vista Crear
                _contexto.Contact.Add(contact);
                //Usaremos metodos asincronos
                await _contexto.SaveChangesAsync();
                //Una vez guardado el contacto nos regresamos a la vista Index
                return RedirectToAction("Index");
            }
            return View();
        }
        //Creacion del netodo para Editar
        [HttpGet]
        public IActionResult Editar(int? id) 
        {
            if (id==null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contact.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contact contact)
        {
           
            if (ModelState.IsValid)
            {
                //Ya no usaremos la fecha, pero pudieramos agregar un campo fecha de edicion
                //contact.CreationDate = DateTime.Now;
                
                //Usamos el metodo Update
                _contexto.Contact.Update(contact);
                
                await _contexto.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return View();
        }

        //Metodo para Detalle 
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contact.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        //Metodo para llamar a la vista Borrar
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contact.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }
            public IActionResult Privacy()
        {
            return View();
        }
        //metodo que elimina el Contacto, usaremos el ActionName
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarContacto(int? id)
        {
            var contacto = await _contexto.Contact.FindAsync(id);
            if (contacto == null)
            {
                return View();
            }
            //Borrado
            _contexto.Contact.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
