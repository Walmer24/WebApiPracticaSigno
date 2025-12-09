 use PracticaSigno;

 --Obtener los 5 productos más caros por cada categoría.
SELECT 
    p.IdProducto,
    p.Descripcion,
    p.Precio,
    c.Descripcion AS Categoria
FROM Productos p
INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
INNER JOIN (
    SELECT IdCategoria, MAX(Precio) AS MaxPrecio
    FROM Productos
    GROUP BY IdCategoria
) m ON p.IdCategoria = m.IdCategoria AND p.Precio = m.MaxPrecio;

--Sumar stock agrupado por categoría.
select SUM(p.Stock) as SumaStock, c.Descripcion as Categoria from Productos p
INNER JOIN Categorias c on p.IdCategoria = c.IdCategoria 
GROUP BY c.Descripcion;

--Listar productos no vendidos (JOIN con tabla ventas).
SELECT p.IdProducto, p.Descripcion
FROM Productos p
LEFT JOIN DetalleVenta d 
    ON d.IdIdProducto = p.IdProducto
WHERE d.IdIdProducto IS NULL;

/*Usar un CTE (Common Table Expression) para calcular el promedio de precios y devolver
productos cuyo precio lo supere.*/
WITH Promedio AS (
    SELECT AVG(Precio) AS PrecioPromedio
    FROM Productos
)
SELECT *
FROM Productos p, Promedio
WHERE p.Precio > Promedio.PrecioPromedio;