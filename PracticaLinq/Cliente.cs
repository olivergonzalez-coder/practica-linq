namespace PracticaLinq;

public class Cliente
{
    public int Id { get; }
    public string Nombre { get; }

    public Cliente(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
