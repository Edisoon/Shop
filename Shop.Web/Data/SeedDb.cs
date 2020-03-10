using System;
using System.Linq;
using System.Threading.Tasks;

public class SeedDb
{
	private readonly DataContext _context;
	private readonly Random random;

	public SeedDb(DataContext context)
	{
		_context = context;
		random = new Random();
	}

	public async Task SeedAsync()
	{
		await _context.Database.EnsureCreatedAsync();

		if (!_context.Products.Any())
		{
			AddProduct("Iphone X");
			AddProduct("MacBook");
			AddProduct("Apple Watch Series 4");
			await _context.SaveChangesAsync();
		}
	}

	private void AddProduct(string name)
	{
		_context.Products.Add(new Product
		{
			Name = name,
			Price = random.Next(1000),
			IsAvailabe = true,
			Stock = random.Next(100)
		});
	}
}
