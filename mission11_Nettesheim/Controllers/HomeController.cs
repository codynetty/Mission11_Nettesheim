using Microsoft.AspNetCore.Mvc;
using mission11_Nettesheim.Models;
using mission11_Nettesheim.Models.ViewModels;
using System.Diagnostics;
using mission11_Nettesheim.Models;
using mission11_Nettesheim.Models.ViewModels;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _repo;
        public HomeController(IBookRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(int pageNum)
        {
            int pageSize = 10;
            var blah = new BooksListViewModel
            {
                Books = _repo.Books
                .OrderBy(x => x.Title)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Books.Count()
                }
            };


            return View(blah);
        }
    }
}
