namespace ChecklistVeiculos.Dto.Request
{
    public partial class NewCheckListDTO
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Placa do veículo é obrigatória", AllowEmptyStrings = false)]
        public string? PlacaVeiculo { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Descrição do veículo é obrigatória", AllowEmptyStrings = false)]
        public string? DescricaoVeiculo { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Executor é obrigatório", AllowEmptyStrings = false)]
        public string? Executor { get; set; }
        public List<NewCheckListItemDTO>? Itens { get; set; }
    }
}