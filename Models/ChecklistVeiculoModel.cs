namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculoModel : IModelBase
    {
        public int Id { get; set; }
        public string? PlacaVeiculo { get; set; }
        public string? DescricaoVeiculo { get; set; }
        public ChecklistStatusEnum Status { get; set; }
        public string? Executor { get; set; }
        public ICollection<ChecklistVeiculoItemModel>? Itens { get; set; }
    }
}