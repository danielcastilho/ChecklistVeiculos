namespace ChecklistVeiculos.Dto.Request
{
    public class NewCheckListDTO
    {
        public string? PlacaVeiculo { get; set; }
        public string? DescricaoVeiculo { get; set; }
        public string? Executor { get; set; }
        public List<NewCheckListItemDTO>? Itens { get; set; }
        public class NewCheckListItemDTO
        {
            public string? Descricao { get; set; }
        }
    }
}