using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;
using NS.SalesProduct.Services.API.ViewModels;

namespace NS.SalesProduct.Services.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductDao _productDao;
        private readonly IMapper _mapper;

        public ProductController(
            INotificationHandler notificationHandler,
            IProductService productService,
            IProductDao productDao, 
            IMapper mapper)
            : base(notificationHandler)
        {
            _productService = productService;
            _productDao = productDao;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAsync()
            => _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(await _productDao.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<ProductViewModel> GetByIdAsync(Guid id)
            => _mapper.Map<Product, ProductViewModel>(await _productDao.GetByIdAsync(id));

        [HttpPost]
        public async Task<ActionResult<ResponseApiViewModel>> PostAsync([FromBody] ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            await _productService.RegisterAsync(product, new ProductValidation());
            return CustomResponse(product);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseApiViewModel>> PutAsync([FromBody] ProductViewModel productViewModel, Guid id)
        {
            if(id != productViewModel.Id) return NotFound();

            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            await _productService.UpdateAsync(product, new ProductValidation(), id);
            return CustomResponse(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseApiViewModel>> DeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);
            return CustomResponse(id);
        }
    }
}
