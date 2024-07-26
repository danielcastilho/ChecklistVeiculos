using ChecklistVeiculos.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets (tabelas)
    public DbSet<ChecklistVeiculoModel> ChecklistVeiculos { get; set; }
    public DbSet<ChecklistVeiculoItemModel> ChecklistVeiculoItens { get; set; }
    public DbSet<ChecklistVeiculoObservacaoModel> ChecklistVeiculoObservacoes { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações de relacionamento
        modelBuilder.Entity<ChecklistVeiculoModel>()
            .HasKey(c => c.Id).HasName("PK_ChecklistVeiculos");
        modelBuilder.Entity<ChecklistVeiculoModel>()
            .HasMany(c => c.Itens)
            .WithOne(i => i.ChecklistVeiculo)
            .HasForeignKey(i => i.ChecklistVeiculoId);

        modelBuilder.Entity<ChecklistVeiculoItemModel>()
            .HasMany(i => i.Observacoes)
                .WithOne(o => o.ChecklistVeiculoItem)
                .HasForeignKey(o => o.ChecklistVeiculoItemId);
    }
            
            

}