namespace PracticaLinq;

public class Categoria
{
    public int Id { get; }
    public string Nombre { get; }

    public Categoria(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
