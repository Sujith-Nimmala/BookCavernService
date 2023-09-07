using RestServices.Models.Db;

namespace RestServices
{
    public class BookDetailsService
    {
        BookStoreContext context;
        public BookDetailsService(BookStoreContext context){
            this.context = context;
        }
        public BookDetail GetBookByName(string name){
            return context.BookDetails.FirstOrDefault(x => x.BookName == name);
        }

        public BookDetail GetBookByAuthor(string author){ 
            return context.BookDetails.FirstOrDefault(x => x.AuthorName == author);
        }

        public List<BookDetail> GetBookByCategory(string category){ 
            return context.BookDetails.Where( x => x.Category == category).ToList();
        }
        public List<BookDetail> GetAllBooks()
        {
            return context.BookDetails.ToList();
           
        }
        public BookDetail GetBookById(int id)
        {
            return context.BookDetails.FirstOrDefault(x => x.BookId == id);
        }
        public int AddBooks(BookDetail NewBook)
        {
            context.BookDetails.Add(NewBook);
            return context.SaveChanges();
        }
        public bool DeleteBooks(int id)
        {
            var book = context.BookDetails.SingleOrDefault(x => x.BookId == id);
            if (book != null)
            {
                context.BookDetails.Remove(book);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateBook(BookDetail book)
        {
            var b = context.BookDetails.SingleOrDefault(x => x.BookId == book.BookId);
            if (b != null)
            {
                b.BookName = book.BookName;
                b.AuthorName = book.AuthorName;
                b.BookPrice = book.BookPrice;
                b.Category = book.Category;
                b.BookPrice = book.BookPrice;
                b.Stock = book.Stock;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
