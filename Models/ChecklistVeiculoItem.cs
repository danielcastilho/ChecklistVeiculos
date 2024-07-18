namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculoItem : IModelBase
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public ChecklistStatusEnum Status { get; set; }
        public ICollection<ChecklistVeiculoObservacao>? Observacoes { get; set; }
        public int ChecklistVeiculoId { get; set; }
        public ChecklistVeiculo? ChecklistVeiculo { get; set; }
    }
}