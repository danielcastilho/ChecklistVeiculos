namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculoItemModel : ModelBase
    {
        public string? Descricao { get; set; }
        public ChecklistStatusEnum Status { get; set; }
        public ICollection<ChecklistVeiculoObservacaoModel>? Observacoes { get; set; }
        public int ChecklistVeiculoId { get; set; }
        public ChecklistVeiculoModel? ChecklistVeiculo { get; set; }
    }
}