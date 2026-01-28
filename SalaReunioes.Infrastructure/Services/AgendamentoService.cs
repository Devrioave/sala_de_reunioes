using Microsoft.EntityFrameworkCore;
using SalaReunioes.Domain.Entities;
using SalaReunioes.Infrastructure.Data;

namespace SalaReunioes.Infrastructure.Services; // Este namespace deve ser exato

public class AgendamentoService // Deve ser PUBLIC
{
    private readonly AppDbContext _context;

    public AgendamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Sala>> ObterSalasEAgendamentosAsync()
    {
        return await _context.Salas
            .Include(s => s.Agendamentos)
            .OrderBy(s => s.Nome)
            .ToListAsync();
    }

    public async Task<(bool Sucesso, string Mensagem)> ReservarAsync(Agendamento novo)
    {
        var conflito = await _context.Agendamentos
            .AnyAsync(a => a.SalaId == novo.SalaId && 
                      a.Inicio < novo.Fim && a.Fim > novo.Inicio);

        if (conflito)
            return (false, "Já existe uma reunião agendada para este horário.");

        _context.Agendamentos.Add(novo);
        await _context.SaveChangesAsync();
        return (true, "Agendamento realizado!");
    }
}