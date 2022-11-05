namespace NS.SalesProduct.Business.Models
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; protected set; }

        public void Generate() => Id = Guid.NewGuid();
    }
}
