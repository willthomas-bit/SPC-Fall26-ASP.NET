using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using System.Xml.Linq;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private Repository<Book> data { get; set; }
        private IWebHostEnvironment env { get; set; }
        private IConfiguration config { get; set; }

        public BookController(BookstoreContext ctx, IWebHostEnvironment env, IConfiguration config)
        {
            data = new Repository<Book>(ctx);
            this.env = env;
            this.config = config;
        }

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(BookGridData values)
        {
            // create options for querying books
            var options = new QueryOptions<Book> { 
                Includes = "Authors, Genre",
                OrderByDirection = values.SortDirection,
                PageNumber = values.PageNumber,
                PageSize = values.PageSize
            };
            if (values.IsSortByGenre) 
                options.OrderBy = b => b.GenreId;
            else if (values.IsSortByPrice) 
                options.OrderBy = b => b.Price;
            else 
                options.OrderBy = b => b.Title;

            // create view model
            var vm = new BookListViewModel { 
                Books = data.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var book = data.Get(new QueryOptions<Book> {
                Where = b => b.BookId == id,
                Includes = "Authors, Genre"
            }) ?? new Book();
            return View(book);
        }

        [HttpPost]
        public RedirectToActionResult PageSize(BookGridData currentRoute)
        {
            return RedirectToAction("List", currentRoute.ToDictionary());
        }

        public RedirectToActionResult ExportToXml()
        {
            var options = new QueryOptions<Book> {
                Includes = "Authors, Genre",
                OrderBy = b => b.Title
            };
            var books = data.List(options);

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Books",
                    books.Select(b =>
                    {
                        string authorNames = "";
                        foreach (Author author in b.Authors)
                        {
                            if (authorNames.Length > 0)
                                authorNames += ", ";
                            authorNames += author.FullName;
                        }

                        return new XElement("Book",
                            new XElement("ID", b.BookId),
                            new XElement("Title", b.Title),
                            new XElement("Author", authorNames),
                            new XElement("Price", b.Price),
                            new XElement("Genre", b.Genre != null ? b.Genre.Name : "")
                        );
                    })
                )
            );

            string relativePath = config["XmlExport:BooksPath"] ?? "data/books.xml";
            string fullPath = Path.Combine(env.WebRootPath, relativePath);
            string? folder = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(folder))
                Directory.CreateDirectory(folder);
            doc.Save(fullPath);

            TempData["message"] = "Books exported to XML.";
            return RedirectToAction("List");
        }
    }   
}
