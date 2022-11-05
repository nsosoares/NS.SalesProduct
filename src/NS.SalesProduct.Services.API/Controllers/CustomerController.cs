using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;
using NS.SalesProduct.Services.API.ViewModels;

namespace NS.SalesProduct.Services.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerDao _customerDao;
        private readonly IMapper _mapper;

        public CustomerController(
            INotificationHandler notificationHandler,
            ICustomerService customerService,
            ICustomerDao customerDao,
            IMapper mapper)
            : base(notificationHandler)
        {
            _customerService = customerService;
            _customerDao = customerDao;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> GetAsync()
            => _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(await _customerDao.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<CustomerViewModel> GetByIdAsync(Guid id)
            => _mapper.Map<Customer, CustomerViewModel>(await _customerDao.GetByIdAsync(id));

        [HttpPost]
        public async Task<ActionResult<ResponseApiViewModel>> PostAsync([FromBody] CustomerViewModel customerViewModel)
        {
            var customer = _mapper.Map<CustomerViewModel, Customer>(customerViewModel);
            await _customerService.RegisterAsync(customer, new CustomerValidation());
            return CustomResponse(customer);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseApiViewModel>> PutAsync([FromBody] CustomerViewModel customerViewModel, Guid id)
        {
            if (id != customerViewModel.Id) return NotFound();

            var customer = _mapper.Map<CustomerViewModel, Customer>(customerViewModel);
            await _customerService.UpdateAsync(customer, new CustomerValidation(), id);
            return CustomResponse(customer);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseApiViewModel>> DeleteAsync(Guid id)
        {
            await _customerService.DeleteAsync(id);
            return CustomResponse(id);
        }
    }
}
