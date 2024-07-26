namespace ChecklistVeiculos.Dto.Request
{
    public class NewCheckListItemDTO
    {
        public string? Descricao { get; set; }
        public List<NewCheckListItemObservacaoDTO>? Observacoes { get; set; }
    }

}