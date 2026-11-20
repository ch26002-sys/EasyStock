using EasyStock.Data;
using EasyStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyStock.Controllers
{
    public class ProductosController : Controller
    {
        private readonly EasyStockContext _context;

        public ProductosController(EasyStockContext context)
        {
            _context = context;
        }

        // LEER: Mostrar todos los productos
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .ToListAsync();

            return View(productos);
        }

        // CREAR: Mostrar formulario
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREAR: Guardar producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nombre,Descripcion,Precio,Stock")] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return View(producto);
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDITAR: Mostrar formulario
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // EDITAR: Guardar cambios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Nombre,Descripcion,Precio,Stock")] Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(producto);
            }

            var productoActual = await _context.Productos.FindAsync(id);

            if (productoActual == null) return NotFound();

            productoActual.Nombre = producto.Nombre;
            productoActual.Descripcion = producto.Descripcion;
            productoActual.Precio = producto.Precio;
            productoActual.Stock = producto.Stock;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR: Mostrar confirmación
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // ELIMINAR: Confirmar eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}