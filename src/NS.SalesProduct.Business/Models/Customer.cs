namespace NS.SalesProduct.Business.Models
{
    public class Customer : Entity
    {
        public Customer(Guid id, string name, string cpf, DateTime birthDate)
            : base(id)
        {
            Name = name;
            Cpf = cpf;
            BirthDate = birthDate;
        }

        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        // NAVIGATION PROPERTIES
        public IReadOnlyCollection<Sale> Sales { get; private set; }
    }
}
