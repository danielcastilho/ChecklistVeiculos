using ChecklistVeiculos.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets (tabelas)
    public DbSet<ChecklistVeiculo> ChecklistVeiculos { get; set; }
    public DbSet<ChecklistVeiculoItem> ChecklistVeiculoItens { get; set; }
    public DbSet<ChecklistVeiculoObservacao> ChecklistVeiculoObservacoes { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações de relacionamento
        modelBuilder.Entity<ChecklistVeiculo>()
            .HasKey(c => c.Id).HasName("PK_ChecklistVeiculos");
        modelBuilder.Entity<ChecklistVeiculo>()
            .HasMany(c => c.Itens)
            .WithOne(i => i.ChecklistVeiculo)
            .HasForeignKey(i => i.ChecklistVeiculoId);

        modelBuilder.Entity<ChecklistVeiculoItem>()
            .HasMany(i => i.Observacoes)
                .WithOne(o => o.ChecklistVeiculoItem)
                .HasForeignKey(o => o.ChecklistVeiculoItemId);
    }
            
            

}