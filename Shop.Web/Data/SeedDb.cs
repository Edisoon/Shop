using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

public class SeedDb
{
	private readonly DataContext _context;
	private readonly IUserHelper _userHelper;
	private readonly Random random;

	public SeedDb(DataContext context, IUserHelper userHelper)
	{
		_context = context;
		_userHelper = userHelper;
		random = new Random();
	}

	public async Task SeedAsync()
	{
		await _context.Database.EnsureCreatedAsync();

		User user = await _userHelper.GetUserByEmailAsync("edisonlopez1992@gmail.com");
		if (user == null)
		{
			user = new User
			{
				FirstName = "Edison",
				LastName = "Lopez",
				Email = "edisonlopez1992@gmail.com",
				UserName = "edisonlopez1992@gmail.com",
				PhoneNumber = "809-272-7840"

			};

			IdentityResult result = await _userHelper.AddUserAsync(user, "123456");
			if (result != IdentityResult.Success)
			{
				throw new InvalidOperationException("Could not create the user in seeder");
			}
		}


		if (!_context.Products.Any())
		{
			AddProduct("Iphone X", user);
			AddProduct("MacBook", user);
			AddProduct("Apple Watch Series 4", user);
			await _context.SaveChangesAsync();
		}
	}

	private void AddProduct(string name, User user)
	{
		_context.Products.Add(new Product
		{
			Name = name,
			Price = random.Next(1000),
			IsAvailabe = true,
			Stock = random.Next(100),
			User = user
		});
	}
}
