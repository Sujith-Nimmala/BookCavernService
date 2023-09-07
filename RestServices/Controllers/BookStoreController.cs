using Microsoft.AspNetCore.Mvc;
using RestServices.Models.Db;
namespace RestServices.Controllers
{
    [ApiController ]
    public class BookStoreController: ControllerBase
    {
        public AppUserService UserService;
        public BookDetailsService BookService;
        public GetOrdersService OrderService;
        public BookStoreController(AppUserService userservice, BookDetailsService bookService, GetOrdersService orderService)
        {
            UserService = userservice;
            BookService = bookService;
            OrderService = orderService;
        }
        //----------------------------------Books Services------------------------------------------------
        [HttpGet]
        [Route("Books")]
        public ObjectResult GetAllBooks()
        {
            return Ok(BookService.GetAllBooks());            
        }
        [HttpGet]
        [Route("Book_id")]
        public IActionResult GetBook(int id)
        {
            BookDetail book=BookService.GetBookById(id);
            if (book != null) return Ok(book);
            return NotFound(0);
        }
        [HttpGet]
        [Route("Book_name")]
        public IActionResult GetBookName(string name)
        {
            BookDetail book = BookService.GetBookByName(name);
            if (book != null) return Ok(book);
            return NotFound(0);
        }
        [HttpGet]
        [Route("Book_author")]
        public IActionResult GetBookAuthor(string name)
        {
            BookDetail book = BookService.GetBookByAuthor(name);
            if (book != null) return Ok(book);
            return NotFound(0);
        }
        [HttpGet]
        [Route("Book_category")]
        public ObjectResult GetBookCategory(string name)
        {
            return Ok(BookService.GetBookByCategory(name));           
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult PostBook(BookDetail book)
        {
            int res=BookService.AddBooks(book);
            if(res==1) return Ok();
            return BadRequest();    
        }
        [HttpDelete]
        [Route("Del_book")]
        public IActionResult DeleteBook(int id)
        {
            bool res= BookService.DeleteBooks(id);
            if (res) return Ok();
            return NotFound();
        }
        [HttpPut]
        [Route("Updt_book")]
        public IActionResult UpdateBook(BookDetail book)
        {
            bool res= BookService.UpdateBook(book);
            if(res) return Ok();
            return NotFound();
        }

        //--------------------------------------- User Services ----------------------------------------
        [HttpGet]
        [Route("U_id")]
        public IActionResult GetUser(int id)
        {
            AppUser user = UserService.GetUserById(id);
            if (user != null) return Ok(user);
            return NotFound(0);
        }


        [HttpGet]
        [Route("U_mail")]
        public IActionResult GetUserMail(string email)
        {
            AppUser user = UserService.GetUserByEmail(email);
            if (user != null) return Ok(user);
            return NotFound(0);
        }
        [HttpPut]
        [Route("Updt_User")]
        public IActionResult UpdateUser(int id, string addr)
        {
            bool res = UserService.UpdateAddress(id,addr);
            if (res) return Ok();
            return NotFound();
        }
        [HttpDelete]
        [Route("Del_user")]
        public IActionResult DeleteUser(int id)
        {
            bool res = UserService.DeleteUser(id);
            if (res) return Ok();
            return NotFound();
        }
        [HttpPost]
        [Route("Order")]
        public IActionResult PostOrder(int bid, int quantity, int cid)    //ask
        {
            int res= OrderService.generateOrder(bid, quantity, cid);
            if(res==1)
            return Ok();
            return BadRequest();
        }
        
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(string address, string name, string email, string password, string contact)    //ask
        {
            int res = UserService.AddUser(address, name, email, password, contact);
            if (res == 1) return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("Order_de")]
        public ObjectResult GetOrder(int Cust_id)
        {
            return Ok(OrderService.getOrderDetails(Cust_id));
        }

    }

}
