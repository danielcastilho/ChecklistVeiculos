namespace ChecklistVeiculos.Models
{
    public class ModelBase : IModelBase
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public string? UsuarioAtualizacao { get; set; }
    }
}