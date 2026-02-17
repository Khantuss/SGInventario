using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGInventario.Domain.Entities;

namespace SGInventario.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorUsernameAsync(string username);
}
