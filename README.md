//API REST .NET con ADO.NET y SQL Serve.

Este proyecto es una API REST desarrollada en ASP.NET Core, utilizando ADO.NET para el acceso directo a datos en SQL Server.
La API implementa operaciones CRUD completas para las entidades:

-Producto

-Categoría

-DetalleVenta

La arquitectura se basa en el uso de controladores, modelos, manejo de excepciones y 
configuración centralizada mediante appsettings.json. Todo el manejo de datos se realiza con consultas SQL parametrizadas 
para asegurar seguridad y rendimiento.

//Tecnologías y Herramientas Utilizadas.

ASP.NET Core Web API (.NET)	=> Framework principal para la API REST
ADO.NET (SqlConnection, SqlCommand, SqlDataReader)	=> Acceso directo a SQL Server
SQL Server	=> Base de datos relacional
C#	=> Lenguaje del backend
appsettings.json => Configuración de cadena de conexión

//Configuracion de la base de datos.

La API utiliza una conexión configurada desde appsettings.json:

"ConnectionStrings": {
  "SignoConexion": "Server=(localdb)\\MSSQLLocalDB; Database=PracticaSigno; Trusted_connection=true; Encrypt=false;"
}

La cadena de conexion se obtiene mediante un IConfiguration y el constructor, creando una variable privada en donde se insertara la conexion
  
private readonly string _con;

public ProductosController(IConfiguration configuration)
{
  _con = configuration.GetConnectionString("SignoConexion");
}

//Modelos del Sistema.

    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
    }

    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
    }

    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int PrecioTotal { get; set; }
    }

//Implementacion con Adonet

SqlConnection => abrir conexión

SqlCommand => ejecutar instrucciones SQL

AddWithValue() => agregar parámetros

SqlDataReader => lectura de filas

ExecuteScalar() => obtener ID insertado

ExecuteNonQuery() => UPDATE, DELETE

try/catch => control de errores

//Endpoints Implementados.

GET	/api/producto   =>  Devuelve todos los productos
GET	/api/producto/{id}  =>  Devuelve un producto por ID
POST	/api/producto   =>  Crea un nuevo producto
PUT	/api/producto/{id}  =>  Actualiza un producto
DELETE	/api/producto/{id}  =>  Elimina un producto

//Como Ejecutarlo paso por paso.
