using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PracticaSigno.Models;
using System.Data;

namespace PracticaSigno.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly string _con;

        public ProductosController(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("SignoConexion");
        }

        [HttpGet]
        public IActionResult GetProducto()
        {
            List<Producto> productos = new();

            try
            {
                using (SqlConnection conexion = new(_con))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new("sp_ObtenerProducto", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    IdCategoria = Convert.ToInt32(reader["IdCategoria"])
                                };

                                productos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error " + ex.Message);
            }
            return Ok(productos);
        }

        [HttpGet("{IdProducto}")]
        public IActionResult GetProductoPorId(int IdProducto)
        {
            var producto = new Producto();

            try
            {
                using (SqlConnection conexion = new(_con))
                {
                    var query = @"select * from Productos where IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);

                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            IdCategoria = Convert.ToInt32(reader["IdCategoria"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error " + ex.Message);
            }

            return Ok(producto);
        }

        [HttpPost]
        public IActionResult AgregarProducto([FromBody] Producto producto)
        {
            var nuevoId = 0;

            try
            {
                using (SqlConnection conexion = new SqlConnection(_con))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarProducto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("IdCategoria", producto.IdCategoria);

                        var outputId = new SqlParameter("@NuevoId", SqlDbType.Int);

                        outputId.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputId);

                        cmd.ExecuteNonQuery();

                        nuevoId = Convert.ToInt32(outputId.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar producto " + ex.Message);
            }

            return Ok("Producto creado con el codigo: " + nuevoId);
        }

        [HttpPut("{IdProducto}")]
        public IActionResult ActualizarProducto(int IdProducto, Producto producto)
        {
            if (IdProducto != producto.IdProducto)
            {
                return BadRequest("Este producto no se encontro");
            }

            try
            {
                using (SqlConnection conexion = new SqlConnection(_con))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarProducto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("IdCategoria", producto.IdCategoria);
                        cmd.Parameters.AddWithValue("IdProducto", producto.IdProducto);

                        var filas = cmd.ExecuteNonQuery();

                        if (filas == 0)
                        {
                            return NotFound("No se encontro productos");
                        }
                    }
                }
                return Ok("Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error" + ex.Message);
            }
        }

        [HttpDelete("{IdProducto}")]
        public ActionResult EliminarProducto(int IdProducto)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_con))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_EliminarProducto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdProducto", IdProducto);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var resultado = Convert.ToInt32(reader["resultado"]);
                                var mensaje = reader["mensaje"].ToString();

                                if (resultado == 1)
                                {
                                    return BadRequest(mensaje);
                                }
                                else if (resultado == 2)
                                {
                                    return NotFound(mensaje);
                                }
                                else if (resultado == 0)
                                {
                                    return Ok(mensaje);
                                }
                            }

                            return StatusCode(500, "Error" );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error" + ex.Message);
            }
        }
    }
}
