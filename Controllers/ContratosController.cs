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
    public class ContratosController : Controller
    {
        private readonly BdTvXcableContext _context;

        public ContratosController(BdTvXcableContext context)
        {
            _context = context;
        }

        // GET: Contratoes
        public async Task<IActionResult> Index()
        {
            var bdTvXcableContext = _context.Contratos.Include(c => c.IdClienteNavigation).Include(c => c.IdServicioNavigation);
            return View(await bdTvXcableContext.ToListAsync());
        }

        // GET: Contratoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contratos
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdContrato == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // GET: Contratoes/Create
        public IActionResult Create()
        {
            var cliente = _context.Clientes.ToList();
            var clientes = cliente.Select(c => new SelectListItem { Text = c.Nombre, Value = c.IdCliente.ToString() }).ToList();
            ViewData["IdCliente"] = clientes;

            var servicios = _context.Servicios.ToList().Select(s => new SelectListItem
            {
                Text = $"{s.Nombre} - ${s.Precio}", // Concatenamos el nombre y el precio del servicio
                Value = s.IdServicio.ToString()
            }).ToList();

            foreach ( var servicio in servicios)
            {
                Console.WriteLine(servicio.Text);
            }

            ViewData["IdServicio"] = servicios;
          

            //ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            //ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio");
            return View();
        }

        // POST: Contratoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContrato,IdServicio,IdCliente")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", contrato.IdCliente);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", contrato.IdServicio);
            return View(contrato);
        }

        // GET: Contratoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", contrato.IdCliente);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", contrato.IdServicio);
            return View(contrato);
        }

        // POST: Contratoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContrato,IdServicio,IdCliente")] Contrato contrato)
        {
            if (id != contrato.IdContrato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratoExists(contrato.IdContrato))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", contrato.IdCliente);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", contrato.IdServicio);
            return View(contrato);
        }

        // GET: Contratoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contratos
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdContrato == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // POST: Contratoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato != null)
            {
                _context.Contratos.Remove(contrato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratoExists(int id)
        {
            return _context.Contratos.Any(e => e.IdContrato == id);
        }
    }
}
