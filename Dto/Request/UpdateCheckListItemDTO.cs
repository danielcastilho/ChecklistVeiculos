using System.ComponentModel.DataAnnotations;

namespace ChecklistVeiculos.Dto.Request
{
    public class UpdateCheckListItemDTO
    {
        [Required(ErrorMessage = "Descrição é obrigatória", AllowEmptyStrings = false)]
        public string Descricao { get; internal set; } = string.Empty;
    }
}