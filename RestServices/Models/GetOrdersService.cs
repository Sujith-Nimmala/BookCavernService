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
        public List<OrderDetail> getOrderDetails(int userID)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            var OrderIds = context.Orders.Where(x => x.CustomerId == userID).Select(x => x.OrderId).ToList();
            foreach (var orderId in OrderIds)
            {
                var orderDetail = context.OrderDetails.Where(x => x.OrderId == orderId).ToList();
                orderDetails.AddRange(orderDetail);
            }
            return orderDetails;
        }
       
        public int generateOrder(int bid, int quantity, int cid)
        {

            decimal? bookPrice = context.BookDetails.FirstOrDefault(x => x.BookId == bid).BookPrice;
            //AppUser user = context.AppUsers.FirstOrDefault(x => x.UserId == cid);
            Order order = new Order()
            {
                CustomerId =cid,
                PlacedOn = DateTime.Now,
            };
            context.Orders.Add(order);
            
            context.SaveChanges();

            OrderDetail details = new OrderDetail()
            {
                OrderId = order.OrderId,
                BookId = bid,
                Quantity = quantity,
                Cost = (bookPrice * quantity)
              
            }; 
            //Console.WriteLine(details.OrderId+" "+details.BookId+" "+details.Quantity+" "+details.Cost);
            context.OrderDetails.Add(details);
            return context.SaveChanges();
            //return context.SaveChanges();

        }
        
    }
}
