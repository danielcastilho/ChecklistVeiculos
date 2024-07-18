namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculoObservacao : IModelBase
    {
        public int Id { get; set; }
        public string? Observacao { get; set; }
        public int ChecklistVeiculoItemId { get; set; }
        public ChecklistVeiculoItem? ChecklistVeiculoItem { get; set; }
    }
}