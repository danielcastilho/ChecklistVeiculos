namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculoItemModel : IModelBase
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public ChecklistStatusEnum Status { get; set; }
        public ICollection<ChecklistVeiculoObservacaoModel>? Observacoes { get; set; }
        public int ChecklistVeiculoId { get; set; }
        public ChecklistVeiculoModel? ChecklistVeiculo { get; set; }
    }
}