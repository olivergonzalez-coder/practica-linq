namespace PracticaLinq;

public class Ticket
{
    public string Titulo { get; }
    public string Estado { get; }
    public int Prioridad { get; }

    public Ticket(string titulo, string estado, int prioridad)
    {
        Titulo = titulo;
        Estado = estado;
        Prioridad = prioridad;
    }
}
