namespace ChecklistVeiculos.Models
{
    public class ChecklistVeiculo : IModelBase
    {
        public int Id { get; set; }
        public string? PlacaVeiculo { get; set; }
        public string? DescricaoVeiculo { get; set; }
        public ChecklistStatusEnum Status { get; set; }
        public string? Executor { get; set; }
        public ICollection<ChecklistVeiculoItem>? Itens { get; set; }
    }
}