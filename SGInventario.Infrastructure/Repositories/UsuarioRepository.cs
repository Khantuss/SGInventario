using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SGInventario.Application.Interfaces;
using SGInventario.Domain.Entities;
using System.Data;

namespace SGInventario.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString;

    public UsuarioRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    private IDbConnection Connection()
        => new SqlConnection(_connectionString);

    public async Task<Usuario?> ObtenerPorUsernameAsync(string username)
    {
        using var connection = Connection();

        return await connection.QueryFirstOrDefaultAsync<Usuario>(
            "sp_usuario_obtener_por_username",
            new { Username = username },
            commandType: CommandType.StoredProcedure
        );
    }
}
