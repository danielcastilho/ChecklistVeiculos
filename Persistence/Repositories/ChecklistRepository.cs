using ChecklistVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChecklistVeiculos.Persistence.Repositories
{
    public class ChecklistRepository : Repository<ChecklistVeiculoModel, ApplicationDbContext>
    {
        public ChecklistRepository(ApplicationDbContext context) : base(context)
        {

        }
        override public async Task<ChecklistVeiculoModel?> GetById(int id)
        {
            var result = await this.DbSet.Include(c => c.Itens!).ThenInclude(i => i.Observacoes).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null || result.Id == 0)
            {
                return null;
            }
            return result;
        }

        public async Task<ChecklistVeiculoModel?> GetByPlaca(string placa)
        {
            var result = await this.DbSet.Include(c => c.Itens!).ThenInclude(i => i.Observacoes).FirstOrDefaultAsync(c => c.PlacaVeiculo == placa);
            if (result == null || result.Id == 0)
            {
                return null;
            }
            return result;
        }
    }
}