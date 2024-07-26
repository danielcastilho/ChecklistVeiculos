using System.ComponentModel.DataAnnotations;

namespace ChecklistVeiculos.Dto.Request
{
    public class UpdateCheckListObservacaoDTO
    {
        [Required(ErrorMessage = "Observação é obrigatória", AllowEmptyStrings = false)]
        public string? Observacao { get; set; }
    }
}