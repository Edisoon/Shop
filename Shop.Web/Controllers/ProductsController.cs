using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Helpers;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly IUserHelper _userHelper;

		public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
		{
			_productRepository = productRepository;
			_userHelper = userHelper;
		}

		// GET: Products
		public IActionResult Index()
		{
			return View(_productRepository.GetAll());
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Products/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Product product)
		{
			if (ModelState.IsValid)
			{
				// TODO: Pending to change to: this.User.Identity.Name
				product.User = await _userHelper.GetUserByEmailAsync("edisonlopez1992@gmail.com");
				product.Name = product.Name.ToUpperInvariant();
				await _productRepository.CreateAsync(product);
				return RedirectToAction(nameof(Index));
			}

			return View(product);
		}

		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// TODO: Pending to change to: this.User.Identity.Name
					product.User = await _userHelper.GetUserByEmailAsync("edisonlopez1992@gmail.com");
					await _productRepository.UpdateAsync(product);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _productRepository.ExistAsync(product.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			return View(product);
		}

		// GET: Products/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			await _productRepository.DeleteAsync(product);
			return RedirectToAction(nameof(Index));
		}
	}
}

