using NS.SalesProduct.Business.DTOs;

namespace NS.SalesProduct.Services.API.ViewModels
{
    public class CustomerViewModel : Dto
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
