using ChecklistVeiculos.Dto.Response;
using ChecklistVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChecklistVeiculos.Persistence.Repositories
{
    public class ChecklistRepository(ApplicationDbContext context)
        : Repository<ChecklistVeiculoModel, ApplicationDbContext>(context)
    {
        public override async Task<ChecklistVeiculoModel?> GetById(int id)
        {
            var result = await this
                .DbSet.Include(c => c.Itens!)
                .ThenInclude(i => i.Observacoes)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (result == null || result.Id == 0)
            {
                return null;
            }
            return result;
        }

        public async Task<IList<ChecklistVeiculoModel>?> GetByPlaca(string placa)
        {
            var result = await this
                .DbSet.Include(c => c.Itens!)
                .ThenInclude(i => i.Observacoes)
                .Where(c => c.PlacaVeiculo == placa)
                .OrderByDescending(c => c.DataAtualizacao)
                .ToListAsync();
            if (result == null || result.Count == 0)
            {
                return null;
            }
            return result;
        }

        public async Task<IEnumerable<ChecklistVeiculoModel>?> GetByStatus(
            ChecklistStatusEnum? status,
            int takeLast
        )
        {
            var query = this
                .DbSet.Include(c => c.Itens!)
                .ThenInclude(i => i.Observacoes)
                .AsQueryable();
            if (status != null)
            {
                query = query.Where(c => c.Status == status);
            }
            var result = await query
                .OrderByDescending(c => c.DataAtualizacao)
                .Take(takeLast)
                .ToListAsync();
            if (result == null || result.Count == 0)
            {
                return null;
            }
            return result;
        }

        internal async Task<IList<ChecklistVeiculoModel>?> GetByExecutor(string executor)
        {
            var result = await this
                .DbSet.Include(c => c.Itens!)
                .ThenInclude(i => i.Observacoes)
                .Where(c => c.Executor == executor)
                .OrderByDescending(c => c.DataAtualizacao)
                .ToListAsync();
            if (result == null || result.Count == 0)
            {
                return new List<ChecklistVeiculoModel>();
            }
            return result;
        }

        internal async Task<IList<ChecklistVeiculoModel>?> GetByPlacaAndExecutor(string placa, string executor)
        {
            var result = await this
                .DbSet.Include(c => c.Itens!)
                .ThenInclude(i => i.Observacoes)
                .Where(c => c.PlacaVeiculo == placa && c.Executor == executor)
                .OrderByDescending(c => c.DataAtualizacao)
                .ToListAsync();
            if (result == null || result.Count == 0)
            {
                return null;
            }
            return result;
        }
    }
}
