using GeekShooping.Web.Models;
using GeekShooping.Web.Services.IServices;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShooping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = await _productService.FindProductById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Details")]       
        public async Task<IActionResult> DetailsPost(ProductViewModel model)
        {
            //var token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cart = new();
            //{
            //    CartHeader = new CartHeaderViewModel
            //    {
            //        UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
            //    }
            //};

            /*criei um objeto CartDetailViewModel com os dados necessários, adicionei a uma lista cartDetails 
             * e atribuiu essa lista ao objeto cart. 
             * Em seguida, chamei o método _cartService.AddItemToCart(cart) para adicionar o item ao carrinho.*/

            CartDetailViewModel cartDetail = new CartDetailViewModel()
            {
                Count = model.Count,
                ProductId = model.Id,
                Product = await _productService.FindProductById(model.Id)
            };

            List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>();
            cartDetails.Add(cartDetail);
            cart.CartDetails = cartDetails;

            try
            {
                var response = await _cartService.AddItemToCart(cart);
                if (response != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // Tratar ou registrar o erro aqui
                // Exemplo: Console.WriteLine(ex.Message);
                return View(model);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}