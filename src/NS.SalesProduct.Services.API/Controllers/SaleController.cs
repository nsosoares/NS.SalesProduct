using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;
using NS.SalesProduct.Services.API.ViewModels;

namespace NS.SalesProduct.Services.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SaleController : BaseController
    {
        private readonly ISaleService _saleService;
        private readonly ISaleDao _saleDao;
        private readonly IMapper _mapper;

        public SaleController(
            INotificationHandler notificationHandler,
            ISaleService saleService,
            ISaleDao saleDao,
            IMapper mapper)
            : base(notificationHandler)
        {
            _saleService = saleService;
            _saleDao = saleDao;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SaleViewModel>> GetAsync()
            => _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleViewModel>>(await _saleDao.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<SaleViewModel> GetByIdAsync(Guid id)
            => _mapper.Map<Sale, SaleViewModel>(await _saleDao.GetByIdAsync(id));

        [HttpPost]
        public async Task<ActionResult<ResponseApiViewModel>> PostAsync([FromBody] SaleViewModel saleViewModel)
        {
            var sale = _mapper.Map<SaleViewModel, Sale>(saleViewModel);
            var saleItems = _mapper.Map<List<SaleItemViewModel>, List<SaleItem>>(saleViewModel.SaleItems);
            sale.AddItems(saleItems);
            await _saleService.RegisterAsync(sale, new SaleValidation());
            return CustomResponse(sale);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseApiViewModel>> PutAsync([FromBody] SaleViewModel saleViewModel, Guid id)
        {
            if (id != saleViewModel.Id) return NotFound();

            var sale = _mapper.Map<SaleViewModel, Sale>(saleViewModel);
            var saleItems = _mapper.Map<List<SaleItemViewModel>, List<SaleItem>>(saleViewModel.SaleItems);
            sale.AddItems(saleItems);
            await _saleService.UpdateAsync(sale, new SaleValidation(), id);
            return CustomResponse(sale);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseApiViewModel>> DeleteAsync(Guid id)
        {
            await _saleService.DeleteAsync(id);
            return CustomResponse(id);
        }
    }
}
