namespace SalaReunioes.Domain.Entities;

public class Agendamento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = string.Empty;
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public string CorHex { get; set; } = "#3F51B5";
    
    public Guid SalaId { get; set; }
    public Sala? Sala { get; set; }

    // Impede que dois agendamentos ocupem o mesmo espaço de tempo na mesma sala
    public bool ConflitaCom(DateTime outroInicio, DateTime outroFim)
    {
        return Inicio < outroFim && Fim > outroInicio;
    }
}