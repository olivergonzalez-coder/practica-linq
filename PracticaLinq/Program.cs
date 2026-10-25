using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticaLinq;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=========== PRÁCTICA DE LINQ ===========");
            Console.WriteLine("1. Sucursales con existencias suficientes");
            Console.WriteLine("2. Precios dentro del presupuesto");
            Console.WriteLine("3. Nombres de clientes para un reporte");
            Console.WriteLine("4. Tickets pendientes");
            Console.WriteLine("5. Búsqueda de un código de producto");
            Console.WriteLine("6. Preparar resultados para dos componentes");
            Console.WriteLine("7. Países por código");
            Console.WriteLine("8. Productos y categorías");
            Console.WriteLine("0. Salir");
            Console.Write("Elija una opción: ");

            string? entrada = Console.ReadLine();
            if (entrada == null)
            {
                // Fin de la entrada estándar: salir sin error.
                break;
            }

            if (!int.TryParse(entrada.Trim(), out int opcion))
            {
                Console.WriteLine("Opción no válida. Escriba un número del 0 al 8.");
                continue;
            }

            Console.WriteLine();
            switch (opcion)
            {
                case 1: Problema1(); break;
                case 2: Problema2(); break;
                case 3: Problema3(); break;
                case 4: Problema4(); break;
                case 5: Problema5(); break;
                case 6: Problema6(); break;
                case 7: Problema7(); break;
                case 8: Problema8(); break;
                case 0:
                    Console.WriteLine("Hasta luego.");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Escriba un número del 0 al 8.");
                    break;
            }
        }
    }

    // ------------------------------------------------------------------
    // Problema 1: Sucursales con existencias suficientes
    // Operación clave: Where (filtrar) + ToList (materializar).
    // ------------------------------------------------------------------
    static void Problema1()
    {
        Console.WriteLine("--- Problema 1: Sucursales con existencias suficientes ---");

        List<int> existencias = new List<int> { 85, 60, 70, 95, 70, 40 };
        int[] pedidos = { 70, 100, 40 }; // caso inicial (70) y comprobaciones (100 y 40)

        Console.WriteLine("Existencias originales: " + string.Join(", ", existencias));

        foreach (int cantidadSolicitada in pedidos)
        {
            // Where conserva el orden original y las repeticiones.
            List<int> suficientes = existencias
                .Where(e => e >= cantidadSolicitada)
                .ToList();

            Console.WriteLine();
            Console.WriteLine($"Pedido de {cantidadSolicitada} unidades:");
            if (suficientes.Any())
            {
                Console.WriteLine("  Cantidades que atienden el pedido: " + string.Join(", ", suficientes));
                Console.WriteLine($"  Sucursales que cumplen: {suficientes.Count}");
            }
            else
            {
                Console.WriteLine("  Ninguna sucursal puede atender el pedido completo.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Existencias originales (sin cambios): " + string.Join(", ", existencias));
    }

    // ------------------------------------------------------------------
    // Problema 2: Precios dentro del presupuesto
    // Operación clave: Where + OrderBy + ToList.
    // ------------------------------------------------------------------
    static void Problema2()
    {
        Console.WriteLine("--- Problema 2: Precios dentro del presupuesto ---");

        List<decimal> precios = new List<decimal> { 25m, 10m, 40m, 15m, 10m, 30m };
        decimal[] presupuestos = { 25m, 5m, 40m }; // caso inicial (25) y comprobaciones (5 y 40)

        Console.WriteLine("Precios originales: " + string.Join(", ", precios));

        foreach (decimal presupuesto in presupuestos)
        {
            List<decimal> dentro = precios
                .Where(p => p <= presupuesto)
                .OrderBy(p => p)
                .ToList();

            Console.WriteLine();
            Console.WriteLine($"Presupuesto de {presupuesto}:");
            if (dentro.Any())
            {
                Console.WriteLine("  Precios dentro del presupuesto (de menor a mayor): " + string.Join(", ", dentro));
                Console.WriteLine($"  Cantidad: {dentro.Count}");
            }
            else
            {
                Console.WriteLine("  No hay precios dentro del presupuesto.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Precios originales (sin cambios): " + string.Join(", ", precios));
    }

    // ------------------------------------------------------------------
    // Problema 3: Nombres de clientes para un reporte
    // Operación clave: Select (proyección) + ToList.
    // ------------------------------------------------------------------
    static void Problema3()
    {
        Console.WriteLine("--- Problema 3: Nombres de clientes para un reporte ---");

        List<Cliente> clientes = new List<Cliente>
        {
            new Cliente(101, "Ana"),
            new Cliente(102, "Roberto"),
            new Cliente(103, "Luis"),
            new Cliente(104, "Ana")
        };

        // Caso inicial
        MostrarNombresClientes("Caso inicial", clientes);

        // Comprobación 1: cliente 105 "María José" (sobre una copia para conservar los datos originales)
        List<Cliente> conMaria = new List<Cliente>(clientes)
        {
            new Cliente(105, "María José")
        };
        MostrarNombresClientes("Con el cliente 105 \"María José\"", conMaria);

        // Comprobación 2: lista vacía
        MostrarNombresClientes("Lista de clientes vacía", new List<Cliente>());

        Console.WriteLine();
        Console.WriteLine($"Clientes originales (sin cambios): {clientes.Count}");

        // Función local: la consulta LINQ y la presentación del resultado.
        static void MostrarNombresClientes(string titulo, List<Cliente> origen)
        {
            // Select no descarta repetidos y conserva el orden original.
            List<string> nombres = origen.Select(c => c.Nombre).ToList();

            Console.WriteLine();
            Console.WriteLine(titulo + ":");
            if (nombres.Any())
            {
                Console.WriteLine("  Nombres: " + string.Join(", ", nombres));
            }
            else
            {
                Console.WriteLine("  No hay clientes para el reporte.");
            }
            Console.WriteLine($"  Cantidad: {nombres.Count}");
        }
    }

    // ------------------------------------------------------------------
    // Problema 4: Tickets pendientes
    // Operación clave: Where + OrderByDescending + Select + ToList.
    // ------------------------------------------------------------------
    static void Problema4()
    {
        Console.WriteLine("--- Problema 4: Tickets pendientes ---");

        List<Ticket> tickets = new List<Ticket>
        {
            new Ticket("Instalar editor", "Pendiente", 1),
            new Ticket("Restablecer red", "Pendiente", 3),
            new Ticket("Revisar impresora", "Cerrado", 3),
            new Ticket("Configurar correo", "Pendiente", 2)
        };
        string estadoBuscado = "Pendiente";

        // Caso inicial
        MostrarTickets($"Caso inicial (estado \"{estadoBuscado}\")", tickets, estadoBuscado);

        // Comprobación 1: un ticket pendiente de prioridad alta y otro cerrado (sobre una copia)
        List<Ticket> ampliada = new List<Ticket>(tickets)
        {
            new Ticket("Actualizar antivirus", "Pendiente", 3),
            new Ticket("Cambiar teclado", "Cerrado", 2)
        };
        MostrarTickets($"Con un ticket pendiente de prioridad alta y otro cerrado (estado \"{estadoBuscado}\")",
            ampliada, estadoBuscado);

        // Comprobación 2: estado que no aparece en los datos
        MostrarTickets("Búsqueda del estado \"En proceso\"", ampliada, "En proceso");

        Console.WriteLine();
        Console.WriteLine($"Tickets originales (sin cambios): {tickets.Count}");

        static void MostrarTickets(string titulo, List<Ticket> origen, string estado)
        {
            // OrderByDescending es estable: los empates mantienen el orden original.
            List<string> titulos = origen
                .Where(t => t.Estado == estado)
                .OrderByDescending(t => t.Prioridad)
                .Select(t => t.Titulo)
                .ToList();

            Console.WriteLine();
            Console.WriteLine(titulo + ":");
            if (titulos.Any())
            {
                Console.WriteLine("  Títulos (de mayor a menor prioridad):");
                foreach (string tituloTicket in titulos)
                {
                    Console.WriteLine("   - " + tituloTicket);
                }
                Console.WriteLine($"  Cantidad: {titulos.Count}");
            }
            else
            {
                Console.WriteLine($"  No hay tickets con el estado \"{estado}\".");
            }
        }
    }

    // ------------------------------------------------------------------
    // Problema 5: Búsqueda de un código de producto
    // Versión 1: First  -> lanza InvalidOperationException si no hay coincidencias.
    // Versión 2: FirstOrDefault -> devuelve null (valor por defecto) sin excepción.
    // Se usa First/FirstOrDefault y no Single, porque Single también lanzaría
    // excepción cuando el código está repetido ("P-100" aparece dos veces).
    // ------------------------------------------------------------------
    static void Problema5()
    {
        Console.WriteLine("--- Problema 5: Búsqueda de un código de producto ---");

        List<string> codigos = new List<string> { "P-100", "P-200", "P-300", "P-100" };
        List<string> vacia = new List<string>();

        string[] busquedas = { "P-100", "P-999" };

        Console.WriteLine("Códigos originales: " + string.Join(", ", codigos));

        foreach (string codigoBuscado in busquedas)
        {
            Console.WriteLine();
            Console.WriteLine($"=== Colección con datos, buscando \"{codigoBuscado}\" ===");
            BuscarConExcepcion(codigos, codigoBuscado);
            BuscarSinExcepcion(codigos, codigoBuscado);
        }

        foreach (string codigoBuscado in busquedas)
        {
            Console.WriteLine();
            Console.WriteLine($"=== Colección vacía, buscando \"{codigoBuscado}\" ===");
            BuscarConExcepcion(vacia, codigoBuscado);
            BuscarSinExcepcion(vacia, codigoBuscado);
        }

        static void BuscarConExcepcion(List<string> origen, string buscado)
        {
            try
            {
                string encontrado = origen.First(c => c == buscado);
                Console.WriteLine($"  [Versión 1 - First] Código encontrado: {encontrado}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"  [Versión 1 - First] Se produjo una excepción: no existe el código \"{buscado}\".");
            }
        }

        static void BuscarSinExcepcion(List<string> origen, string buscado)
        {
            string? encontrado = origen.FirstOrDefault(c => c == buscado);
            if (encontrado != null)
            {
                Console.WriteLine($"  [Versión 2 - FirstOrDefault] Código encontrado: {encontrado}");
            }
            else
            {
                Console.WriteLine($"  [Versión 2 - FirstOrDefault] No se encontró el código \"{buscado}\".");
            }
        }
    }

    // ------------------------------------------------------------------
    // Problema 6: Preparar resultados para dos componentes
    // Operación clave: Where + ToList (lista ampliable) + ToArray (arreglo).
    // ------------------------------------------------------------------
    static void Problema6()
    {
        Console.WriteLine("--- Problema 6: Preparar resultados para dos componentes ---");

        List<int> edades = new List<int> { 15, 22, 17, 30, 18 };
        List<int> sinMayores = new List<int> { 15, 17, 12 };

        Procesar("Caso inicial", edades);
        Procesar("Fuente sin personas mayores de edad", sinMayores);

        static void Procesar(string titulo, List<int> origen)
        {
            Console.WriteLine();
            Console.WriteLine($"=== {titulo} ===");
            Console.WriteLine("Colección original: " + string.Join(", ", origen));

            var mayores = origen.Where(e => e >= 18);
            List<int> lista = mayores.ToList();   // componente 1: lista que puede ampliarse
            int[] arreglo = mayores.ToArray();    // componente 2: arreglo

            Console.WriteLine();
            Console.WriteLine("Resultados iniciales:");
            MostrarEstructuras(lista, arreglo);

            // Modificaciones: afectan solo a las estructuras nuevas.
            lista.Add(40);
            if (arreglo.Length > 0)
            {
                arreglo[0] = 23;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("El arreglo está vacío: no se puede cambiar su primer elemento.");
            }

            Console.WriteLine();
            Console.WriteLine("Después de agregar 40 a la lista y cambiar el primer elemento del arreglo a 23:");
            MostrarEstructuras(lista, arreglo);

            Console.WriteLine();
            Console.WriteLine("Colección original (sin cambios): " + string.Join(", ", origen));
        }

        static void MostrarEstructuras(List<int> lista, int[] arreglo)
        {
            Console.WriteLine($"  Lista   ({lista.Count} elementos): " + (lista.Count > 0 ? string.Join(", ", lista) : "(vacía)"));
            Console.WriteLine($"  Arreglo ({arreglo.Length} elementos): " + (arreglo.Length > 0 ? string.Join(", ", arreglo) : "(vacío)"));
        }
    }

    // ------------------------------------------------------------------
    // Problema 7: Países por código
    // Operación clave: ToDictionary + TryGetValue.
    // ------------------------------------------------------------------
    static void Problema7()
    {
        Console.WriteLine("--- Problema 7: Países por código ---");

        List<Pais> paises = new List<Pais>
        {
            new Pais("PA", "Panamá"),
            new Pais("PE", "Perú"),
            new Pais("CL", "Chile")
        };

        Dictionary<string, string> diccionario = paises.ToDictionary(p => p.Codigo, p => p.Nombre);

        Console.WriteLine("Diccionario creado:");
        foreach (KeyValuePair<string, string> par in diccionario)
        {
            Console.WriteLine($"  {par.Key} -> {par.Value}");
        }

        string[] consultas = { "PA", "MX" };
        foreach (string codigo in consultas)
        {
            Console.WriteLine();
            // TryGetValue evita la excepción KeyNotFoundException.
            if (diccionario.TryGetValue(codigo, out string? nombre))
            {
                Console.WriteLine($"Consulta \"{codigo}\": {nombre}");
            }
            else
            {
                Console.WriteLine($"Consulta \"{codigo}\": el código no existe en el diccionario.");
            }
        }
    }

    // ------------------------------------------------------------------
    // Problema 8: Productos y categorías
    // Operación clave: Join (unión interna) + Select de texto.
    // ------------------------------------------------------------------
    static void Problema8()
    {
        Console.WriteLine("--- Problema 8: Productos y categorías ---");

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

        // Caso inicial
        MostrarParejas("Caso inicial", productos, categorias);

        // Comprobación 1: agregar la categoría 99 "Especiales" (sobre una copia)
        List<Categoria> conEspeciales = new List<Categoria>(categorias)
        {
            new Categoria(99, "Especiales")
        };
        MostrarParejas("Con la categoría 99 \"Especiales\"", productos, conEspeciales);

        // Comprobación 2: además, otra categoría con identificador 1 "Útiles"
        List<Categoria> conRepetida = new List<Categoria>(conEspeciales)
        {
            new Categoria(1, "Útiles")
        };
        MostrarParejas("Con otra categoría de identificador 1 \"Útiles\"", productos, conRepetida);

        Console.WriteLine();
        Console.WriteLine($"Colecciones originales (sin cambios): {productos.Count} productos, {categorias.Count} categorías");

        static void MostrarParejas(string titulo, List<Producto> prods, List<Categoria> cats)
        {
            // Join solo conserva identificadores coincidentes, respeta el orden de los
            // productos y, si una clave se repite en categorías, devuelve todas las parejas.
            List<string> parejas = prods
                .Join(cats,
                      p => p.CategoriaId,
                      c => c.Id,
                      (p, c) => $"{p.Nombre} - {c.Nombre}")
                .ToList();

            Console.WriteLine();
            Console.WriteLine(titulo + ":");
            if (parejas.Any())
            {
                foreach (string pareja in parejas)
                {
                    Console.WriteLine("   - " + pareja);
                }
                Console.WriteLine($"  Cantidad de parejas: {parejas.Count}");
            }
            else
            {
                Console.WriteLine("  No hay parejas coincidentes.");
            }
        }
    }
}
