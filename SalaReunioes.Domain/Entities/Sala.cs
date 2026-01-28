
namespace SalaReunioes.Domain.Entities;

public class Sala
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public int Capacidade { get; set; }
    public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
}