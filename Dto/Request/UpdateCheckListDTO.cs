using ChecklistVeiculos.Models;

namespace ChecklistVeiculos.Dto.Request
{
    public class UpdateCheckListDTO
    {
        public string? Placa { get; set; }
        public string? Descricao { get; set; }
        public ChecklistStatusEnum Status { get; set; }
        public IList<UpdateCheckListItemDTO>? Itens { get; set; }

        public class UpdateCheckListItemDTO
        {
            public string? Descricao { get; set; }
            public ChecklistStatusEnum Status { get; set; }
            public IList<UpdateCheckListObservacaoDTO>? Observacoes { get; set; }

            public class UpdateCheckListObservacaoDTO
            {
                public int? Id { get; set; }
                public string? Observacao { get; set; }
            }
        }
    }
}