using Microsoft.EntityFrameworkCore;
using SalaReunioes.Domain.Entities;
using SalaReunioes.Infrastructure.Data;

namespace SalaReunioes.Infrastructure.Services;

public class AgendamentoService
{
    private readonly AppDbContext _context;

    public AgendamentoService(AppDbContext context) => _context = context;

    public async Task<(bool Sucesso, string Mensagem)> CriarAgendamentoAsync(Agendamento novo)
    {
        // Validação Sênior: Verifica se existe algum agendamento que sobrepõe o horário
        var conflito = await _context.Agendamentos
            .AnyAsync(a => a.SalaId == novo.SalaId && 
                      a.Inicio < novo.Fim && a.Fim > novo.Inicio);

        if (conflito)
            return (false, "Já existe uma reunião agendada para este horário nesta sala.");

        _context.Agendamentos.Add(novo);
        await _context.SaveChangesAsync();
        return (true, "Agendamento realizado com sucesso!");
    }

    public async Task<List<Sala>> ObterAgendaCompletaAsync()
    {
        return await _context.Salas
            .Include(s => s.Agendamentos)
            .OrderBy(s => s.Nome)
            .ToListAsync();
    }
}