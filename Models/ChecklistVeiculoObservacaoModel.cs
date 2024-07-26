namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculoObservacaoModel : IModelBase
    {
        public int Id { get; set; }
        public string? Observacao { get; set; }
        public int ChecklistVeiculoItemId { get; set; }
        public ChecklistVeiculoItemModel? ChecklistVeiculoItem { get; set; }
    }
}