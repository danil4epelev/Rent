using Microsoft.AspNetCore.Mvc;

namespace RentApplication.Controllers
{
	public class RentItemController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
