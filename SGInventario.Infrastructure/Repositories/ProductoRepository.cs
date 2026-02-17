using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using SGInventario.Domain.Entities;
using SGInventario.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace SGInventario.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ProductoRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
    }

    private IDbConnection Connection()
        => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Producto>> ListarAsync()
    {
        using var connection = Connection();

        return await connection.QueryAsync<Producto>(
            "sp_producto_listar",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Producto?> ObtenerPorIdAsync(int id)
    {
        using var connection = Connection();

        var sql = "SELECT * FROM Producto WHERE Id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Producto>(sql, new { Id = id });
    }

    public async Task<int> CrearAsync(Producto producto)
    {
        using var connection = Connection();

        var sql = @"
        INSERT INTO Producto (Nombre, Descripcion, Precio, Stock, Activo)
        VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Activo);
        SELECT CAST(SCOPE_IDENTITY() as int);";

        return await connection.ExecuteScalarAsync<int>(sql, producto);
    }

    public async Task<bool> ActualizarAsync(Producto producto)
    {
        using var connection = Connection();

        var sql = @"
        UPDATE Producto
        SET Nombre = @Nombre,
            Descripcion = @Descripcion,
            Precio = @Precio,
            Stock = @Stock,
            Activo = @Activo
        WHERE Id = @Id";

        var filas = await connection.ExecuteAsync(sql, producto);

        return filas > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        using var connection = Connection();

        var sql = "DELETE FROM Producto WHERE Id = @Id";

        var filas = await connection.ExecuteAsync(sql, new { Id = id });

        return filas > 0;
    }

}

