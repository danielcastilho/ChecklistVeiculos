using ChecklistVeiculos.Models;

namespace ChecklistVeiculos.Dto.Response
{
    public class CheckListCreatedDTO
    {
        public int Id { get; set; }
        public string? PlacaVeiculo { get; set; }
        public string? DescricaoVeiculo { get; set; }
        public string? Executor { get; set; }
        public List<CheckListItemDTO>? Itens { get; set; }
        public ChecklistStatusEnum Status { get; set; }
        public class CheckListItemDTO
        {
            public string? Descricao { get; set; }
            public List<CheckListObservacaoDTO>? Observacoes { get; set; }
            public class CheckListObservacaoDTO
            {
                public string? Observacao { get; set; }
            }
        }
    }
}