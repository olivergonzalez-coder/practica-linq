namespace PracticaLinq;

public class Producto
{
    public string Nombre { get; }
    public int CategoriaId { get; }

    public Producto(string nombre, int categoriaId)
    {
        Nombre = nombre;
        CategoriaId = categoriaId;
    }
}
