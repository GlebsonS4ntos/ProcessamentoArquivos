namespace ProcessamentoArquivos.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        
        public Entity()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }
    }
}