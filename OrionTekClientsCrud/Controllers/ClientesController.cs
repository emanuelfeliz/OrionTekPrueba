using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionTekClientsCrud.Models;
using OrionTekClientsCrud.ViewModel;

namespace OrionTekClientsCrud.Controllers
{
    public class ClientesController : Controller
    {
        private readonly OrionTekContext _context;

        public ClientesController(OrionTekContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ClienteViewModel cliente = new ClienteViewModel();
            
            return View(cliente);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                // Mapear el ViewModel a la entidad Cliente
                var cliente = new Cliente
                {
                    Nombre = clienteViewModel.Nombre,
                };

                // Agregar el cliente al contexto y guardar cambios
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                List<Direccion> direcciones = clienteViewModel.Direcciones.Select(d => new Direccion
                {
                    ClienteId = cliente.ClienteId,
                    Calle = d.Calle,
                    Ciudad = d.Ciudad,
                    Estado = d.Estado,
                    CodigoPostal = d.CodigoPostal
                }).ToList();
                _context.Direcciones.AddRange(direcciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteViewModel); // Devolver el ViewModel con los errores de validación
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Buscar el cliente por ID
            var cliente = await _context.Clientes
                .Include(c => c.Direcciones) // Incluir direcciones
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra
            }

            // Mapear la entidad Cliente a ClienteViewModel
            var clienteViewModel = new ClienteViewModel
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Direcciones = cliente.Direcciones.Select(d => new DireccionViewModel
                {
                    DireccionId = d.DireccionId,
                    Calle = d.Calle,
                    Ciudad = d.Ciudad,
                    Estado = d.Estado,
                    CodigoPostal = d.CodigoPostal
                }).ToList()
            };

            return View(clienteViewModel); // Pasar el ViewModel a la vista
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.ClienteId) // Validar que el ID coincida
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Buscar el cliente existente
                var cliente = await _context.Clientes
                    .Include(c => c.Direcciones) // Incluir direcciones para poder actualizarlas
                    .FirstOrDefaultAsync(c => c.ClienteId == id);

                if (cliente == null)
                {
                    return NotFound(); // Retornar 404 si no se encuentra
                }

                // Actualizar los campos del cliente
                cliente.Nombre = clienteViewModel.Nombre;

                // Actualizar las direcciones
                cliente.Direcciones.Clear();
                cliente.Direcciones = clienteViewModel.Direcciones.Select(d => new Direccion
                {
                    DireccionId = d.DireccionId, // Mantener el ID para evitar duplicados
                    Calle = d.Calle,
                    Ciudad = d.Ciudad,
                    Estado = d.Estado,
                    CodigoPostal = d.CodigoPostal
                }).ToList();

                // Guardar los cambios
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(clienteViewModel); // Devolver el ViewModel con errores de validación
        }


        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
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
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
