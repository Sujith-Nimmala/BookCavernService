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
        [Route("Book_id/{id}")]
        public IActionResult GetBook(int id)
        {
            BookDetail book=BookService.GetBookById(id);
            if (book != null) return Ok(book);
            return NotFound(0);
        }
        [HttpGet]
        [Route("Book_name/{name}")]
        public ObjectResult GetBookName(string name)
        {
            return Ok(BookService.GetBookByName(name));
        }
        [HttpGet]
        [Route("Book_author/{name}")]
        public ObjectResult GetBookAuthor(string name)
        {
            return Ok(BookService.GetBookByAuthor(name));
        }
        [HttpGet]
        [Route("Book_category/{name}")]
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
        [Route("Del_book/{id}")]
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
        [Route("U_id/{name}")]
        public IActionResult GetUser(string name)
        {
            int user = UserService.GetUserByName(name);
            if (user != null)
                return Ok(user);
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
        [HttpPost]
        [Route("Updt_User/{userName}/{addr}")]
        public IActionResult UpdateUser(int id, string addr)
        {
            bool res = UserService.UpdateAddress(id ,addr);
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
        [Route("Order/{cid}")]
        public IActionResult PostOrder(PlaceOrderModel[] x, int cid)    
        {
            var res = 0;
            //foreach(var i in x)
                res= OrderService.generateOrder(x, cid);
            if(res==1)
                return Ok();
            return BadRequest();
        }
        
        [HttpPost]
        [Route("SignUp/{name}/{email}/{password}/{contact}")]
        public IActionResult SignUp(string name, string email, string password, string contact)    //ask
        {
            int res = UserService.AddUser(name, email, password, contact);
            if (res == 1) return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("Order_id/{Cust_id}")]
        public ObjectResult GetOrder(int Cust_id)
        {
            return Ok(OrderService.getOrderIds(Cust_id));
        }

        [HttpGet]
        [Route("Order_de/{OrderId}")]
        public ObjectResult GetOrderDetails(int OrderId)
        {
            return Ok(OrderService.getOrderDetails(OrderId));
        }

    }

}
