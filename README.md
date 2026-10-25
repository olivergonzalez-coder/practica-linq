# Práctica de LINQ

Programación III · Docente Jaime Acosta

Proyecto de consola en C# (.NET 10) con un menú que ejecuta ocho problemas resueltos con LINQ.
Opciones del **1 al 8** para ejecutar cada problema y **0** para salir. El menú se vuelve a mostrar después de cada ejecución.

## Cómo ejecutar

```bash
dotnet run --project PracticaLinq
```

O abre `PracticaLinq.sln` en Visual Studio y presiona F5.

## Estructura

- `Program.cs`: `Main` (menú) y los métodos `Problema1()` a `Problema8()`.
- `Cliente.cs`, `Ticket.cs`, `Pais.cs`, `Categoria.cs`, `Producto.cs`: clases de los problemas con objetos.

---

## Enunciados

### 1. Sucursales con existencias suficientes

```csharp
List<int> existencias = new List<int> { 85, 60, 70, 95, 70, 40 };
int cantidadSolicitada = 70;
```

Cada número representa las unidades disponibles de un producto en una sucursal. Obtén las cantidades de las sucursales que pueden atender por sí solas el pedido completo y guárdalas en otra lista. Muestra los valores y cuántas sucursales cumplen. Conserva el orden original y las repeticiones. Si ninguna puede atenderlo, muestra un mensaje.

**Comprueba:** utiliza pedidos de 70, 100 y 40 unidades.

### 2. Precios dentro del presupuesto

```csharp
List<decimal> precios = new List<decimal> { 25m, 10m, 40m, 15m, 10m, 30m };
decimal presupuesto = 25m;
```

La letra `m` indica un valor de tipo `decimal`.

Obtén los precios que no superen el presupuesto y guárdalos en otra lista, de menor a mayor. Muestra los valores y su cantidad. Conserva las repeticiones y muestra un mensaje si no hay resultados.

**Comprueba:** utiliza los presupuestos 25m, 5m y 40m.

### 3. Nombres de clientes para un reporte

```csharp
List<Cliente> clientes = new List<Cliente>
{
    new Cliente(101, "Ana"),
    new Cliente(102, "Roberto"),
    new Cliente(103, "Luis"),
    new Cliente(104, "Ana")
};
```

Un reporte necesita únicamente los nombres de los clientes. Obtén una nueva lista de textos, en el orden original, sin los identificadores. Debe haber un nombre por cada cliente, aunque varios tengan el mismo nombre. Muestra el resultado y la cantidad.

**Comprueba:** agrega un cliente con identificador 105 y nombre "María José". Después repite con una lista de clientes vacía.

### 4. Tickets pendientes

```csharp
List<Ticket> tickets = new List<Ticket>
{
    new Ticket("Instalar editor", "Pendiente", 1),
    new Ticket("Restablecer red", "Pendiente", 3),
    new Ticket("Revisar impresora", "Cerrado", 3),
    new Ticket("Configurar correo", "Pendiente", 2)
};
string estadoBuscado = "Pendiente";
```

Obtén una lista con los títulos de los tickets del estado buscado, de mayor a menor prioridad. La prioridad 1 es baja, 2 es media y 3 es alta. Muestra los títulos y la cantidad; si no hay coincidencias, muestra un mensaje.

**Comprueba:** agrega un ticket pendiente de prioridad alta y otro cerrado. Después busca el estado "En proceso", que no aparece en los datos.

### 5. Búsqueda de un código de producto

```csharp
List<string> codigos = new List<string> { "P-100", "P-200", "P-300", "P-100" };
string codigoBuscado = "P-100";
```

Una importación puede contener códigos repetidos. Construye dos versiones de una búsqueda que devuelva un solo código que coincida exactamente con el buscado:

- En la primera, la ausencia de coincidencias debe producir una excepción. Maneja esa situación y muestra un mensaje sin que el programa termine inesperadamente.
- En la segunda, la ausencia debe poder comprobarse sin provocar una excepción por falta de coincidencias. Muestra el código o un mensaje, según corresponda.

**Comprueba:** busca "P-100", busca "P-999" y repite ambas búsquedas con una colección vacía. Que haya dos entradas "P-100" no debe impedir obtener un único resultado.

### 6. Preparar resultados para dos componentes

```csharp
List<int> edades = new List<int> { 15, 22, 17, 30, 18 };
```

Un sistema de inscripciones utiliza dos componentes: uno recibe una lista de edades que después puede ampliarse y otro recibe un arreglo. Obtén las edades mayores o iguales a 18 y guarda el resultado en ambas estructuras. Muestra los valores y la cantidad de elementos de cada una.

Agrega 40 a la lista resultante. Cambia a 23 el primer elemento del arreglo. Muestra nuevamente ambas estructuras y la colección original.

**Comprueba:** repite el proceso con una fuente sin personas mayores de edad. En ese caso, evita acceder a una posición inexistente del arreglo.

### 7. Países por código

```csharp
List<Pais> paises = new List<Pais>
{
    new Pais("PA", "Panamá"),
    new Pais("PE", "Perú"),
    new Pais("CL", "Chile")
};
```

A partir de la lista, crea un diccionario cuya clave sea el código y cuyo valor sea el nombre del país. Consulta un código y muestra su nombre. Si el código no existe, informa esa situación sin que el programa termine inesperadamente.

**Comprueba:** consulta "PA" y "MX".

### 8. Productos y categorías

```csharp
List<Categoria> categorias = new List<Categoria>
{
    new Categoria(1, "Papelería"),
    new Categoria(2, "Tecnología"),
    new Categoria(3, "Hogar")
};

List<Producto> productos = new List<Producto>
{
    new Producto("Cuaderno", 1),
    new Producto("Ratón", 2),
    new Producto("Lápiz", 1),
    new Producto("Artículo especial", 99)
};
```

Relaciona ambas colecciones para obtener una lista de textos con el nombre del producto y el nombre de su categoría. Incluye únicamente las parejas con identificadores coincidentes. Mantén el orden de los productos.

**Comprueba:** agrega la categoría 99 con el nombre "Especiales" y ejecuta de nuevo. Después agrega otra entrada de categoría con identificador 1 y nombre "Útiles": muestra todas las parejas coincidentes, sin eliminar resultados.
