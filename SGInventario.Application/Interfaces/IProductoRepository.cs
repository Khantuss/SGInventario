using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGInventario.Domain.Entities;

namespace SGInventario.Application.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ListarAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Producto producto);
        Task<bool> ActualizarAsync(Producto producto);
        Task<bool> EliminarAsync(int id);
    }
}