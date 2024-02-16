using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmpresaTVxCable.Models;

namespace EmpresaTVxCable.Controllers
{
    public class ClientesController : Controller
    {
        private readonly BdTvXcableContext _context;

        public ClientesController(BdTvXcableContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var bdTvXcableContext = _context.Clientes.Include(c => c.IdZonaNavigation);
            return View(await bdTvXcableContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            //ViewData["IdZona"] = new SelectList(_context.ZonaGeograficas, "IdZona", "IdZona");
            
            // Obtener la lista de zonas disponibles
            var zonas = _context.ZonaGeograficas.ToList();

            // Crear una lista de SelectListItem con solo los nombres de las zonas
            var items = zonas.Select(z => new SelectListItem { Text = z.Nombre, Value = z.IdZona.ToString() }).ToList();

            // Asignar la lista de zonas al ViewData
            ViewData["IdZona"] = items;
            

            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nombre,Dni,Activo,IdZona")] Cliente cliente)
        {
            var zona = await _context.ZonaGeograficas.FindAsync(cliente.IdZona);

            // Verificar si la zona se encontró
            if (zona != null)
            {
                // Establecer la propiedad de navegación IdZonaNavigation
                cliente.IdZonaNavigation = zona;
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            /*
             //if (ModelState.IsValid)
             //{
                 Console.WriteLine("entro en model state is valid: ");
                 _context.Add(cliente);
                 Console.WriteLine("context_add ");
                 await _context.SaveChangesAsync();
                 Console.WriteLine("aca ya entro a la bd: ");
                 return RedirectToAction(nameof(Index));
             //}

             var errors = ModelState.Values.SelectMany(v => v.Errors);
             foreach (var error in errors)
             {
                 Console.WriteLine(error.ErrorMessage);
             }
            */

            //Console.WriteLine("problemillas");
             ViewData["IdZona"] = new SelectList(_context.ZonaGeograficas, "IdZona", "IdZona", cliente.IdZona);
             ModelState.AddModelError("IdZona", "Si llegamos aquí, significa que hay un error en el modelo o el valor de IdZona es nulo");
             //Console.WriteLine("probleeemoon");
             return View(cliente);
             
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            //ViewData["IdZona"] = new SelectList(_context.ZonaGeograficas, "IdZona", "IdZona", cliente.IdZona);

            var zonas = _context.ZonaGeograficas.ToList();

            var items = zonas.Select(z => new SelectListItem { Text = z.Nombre, Value = z.IdZona.ToString() }).ToList();

            ViewData["IdZona"] = items;

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nombre,Dni,Activo,IdZona")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    var zona = await _context.ZonaGeograficas.FindAsync(cliente.IdZona);

                    // Verificar si la zona se encontró
                    if (zona != null)
                    {
                        cliente.IdZonaNavigation = zona;
                        _context.Update(cliente);
                        await _context.SaveChangesAsync();

                    }
                
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["IdZona"] = new SelectList(_context.ZonaGeograficas, "IdZona", "IdZona", cliente.IdZona);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
