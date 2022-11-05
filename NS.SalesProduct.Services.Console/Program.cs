using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Infra.Data.Repositorys;

var product = new Product("TESTE", 10.5M, 10.8M);

var productRepo = new ProductDao();

productRepo.RegisterAsync(product);

var testes = productRepo.GetAllAsync().Result;

var idSalva = Guid.Empty;
foreach (var test in testes)
{
    var testeAchado = productRepo.GetByIdAsync(test.Id).Result;
    idSalva = test.Id;
}


var ytesteEditar = productRepo.GetByIdAsync(idSalva).Result;


ytesteEditar.ChangePrice(59);


productRepo.UpdateAsync(ytesteEditar);

var ytesteEditar2 = productRepo.GetByIdAsync(idSalva).Result;

productRepo.DeleteAsync(ytesteEditar2);

var ytu = "";

var testews = productRepo.GetAllAsync().Result;


var fgdf = "";