using ChecklistVeiculos.Models;
namespace ChecklistVeiculos.Dto.Response
{
    public class CheckListItemCreatedDTO
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }

        public ChecklistStatusEnum Status { get; set; }

        public int ChecklistVeiculoId { get; set; }

        public IList<CheckListObservacaoCreatedDTO>? Observacoes { get; set; }

        public int CheckListId { get; set; }
    }
}
