namespace ChecklistVeiculos.Dto.Request
{
    public class UpdateCheckListDTO
    {
        public string? Placa { get; set; }
        public string? Descricao { get; set; }
        public bool Concluido { get; set; }
    }
}