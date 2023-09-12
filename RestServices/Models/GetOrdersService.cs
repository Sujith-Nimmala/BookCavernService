using RestServices.Models.Db;

namespace RestServices
{
    public class GetOrdersService
    {
        BookStoreContext context;
        public GetOrdersService(BookStoreContext context)
        {
            this.context = context;
        }
        public List<CustOrder> getOrderDetails(int OrderID)
        {
            List<CustOrder> orderDetails = new List<CustOrder>();
            //var OrderIds = context.Orders.Where(x => x.CustomerId == userID).Select(x => x.OrderId).ToList();
            //foreach (var orderId in OrderIds)
            //{
            //    var orderDetail = context.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            //    orderDetails.AddRange(orderDetail);
            //}
            var bookIds = context.OrderDetails.Where(x => x.OrderId == OrderID).Select(x => x.BookId).ToList();
            foreach (var bookId in bookIds)
            {
                var book = context.BookDetails.Find(bookId);
                var o = context.OrderDetails.Where(x => x.OrderId == OrderID && x.BookId == bookId).FirstOrDefault();
                orderDetails.Add(new CustOrder
                {
                    BookId = (int)bookId,
                    BookName = book.BookName,
                    AuthorName = book.AuthorName,
                    Category = book.Category,
                    Quantity = o.Quantity,
                    Cost = o.Cost
                });
            }
            return orderDetails;
        }
        public List<int> getOrderIds(int userID)
        {
            var OrderIds = context.Orders.Where(x => x.CustomerId == userID).Select(x => x.OrderId).ToList();
            return OrderIds;
        }
       
        public int generateOrder(PlaceOrderModel[] x, int cid)
        {

            
            
            //AppUser user = context.AppUsers.FirstOrDefault(x => x.UserId == cid);
            Order order = new Order()
            {
                CustomerId =cid,
                PlacedOn = DateTime.Now,
            };
            context.Orders.Add(order);
            
            context.SaveChanges();
            foreach (var i in x)
            {
                BookDetail book = context.BookDetails.Find(i.bid)!;
                decimal? bookPrice = book.BookPrice;
                OrderDetail details = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    BookId = i.bid,
                    Quantity = i.quantity,
                    Cost = (bookPrice * i.quantity)

                };
                book.Stock -= i.quantity;
                //Console.WriteLine(details.OrderId+" "+details.BookId+" "+details.Quantity+" "+details.Cost);
                context.OrderDetails.Add(details);
            }
            return context.SaveChanges();
            //return context.SaveChanges();

        }

                
    }

    public class PlaceOrderModel
    {
        //public Dictionary<BookDetail, int> items;
        public int bid { get; set; }
        public int quantity { get; set; }

    }

    public class CustOrder
    {
        public int BookId { get; set; }

        public string? BookName { get; set; }

        public string? AuthorName { get; set; }

        public decimal? BookPrice { get; set; }

        public string? Category { get; set; }

        public int? Quantity { get; set; }

        public decimal? Cost { get; set; }

    }
}
