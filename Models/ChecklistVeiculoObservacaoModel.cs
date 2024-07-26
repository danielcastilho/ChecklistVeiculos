namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculoObservacaoModel : ModelBase
    {
        public string? Observacao { get; set; }
        public int ChecklistVeiculoItemId { get; set; }
        public ChecklistVeiculoItemModel? ChecklistVeiculoItem { get; set; }
    }
}