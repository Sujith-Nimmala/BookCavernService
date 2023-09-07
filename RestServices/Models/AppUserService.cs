using RestServices.Models.Db;

namespace RestServices
{
    public class AppUserService
    {
        BookStoreContext context;
        public AppUserService(BookStoreContext context)
        {
            this.context = context;
        }

        public AppUser GetUserById(int id)
        {
            return context.AppUsers.Find(id)!;
        }

        public AppUser GetUserByEmail(string email)
        {
            return context.AppUsers.FirstOrDefault(x => x.UserEmail == email)!;
        }

        public bool UpdateAddress(int id, string address){
            var user = context.AppUsers.Find(id);
            user.UserAddress = address;
            context.SaveChanges();
            return true;
        }

        public int AddUser(string address, string name, string email, string password, string contact)
        {
            var user = new AppUser();
            user.UserAddress = address;
            user.UserName = name;
            user.UserEmail = email;
            user.UserPass = password;
            user.UserContactNo = contact;
            user.UserRole = "Customer";
            context.AppUsers.Add(user);
            return context.SaveChanges();
            
        }

        //public bool checkUser(string email, string password){
        //    var user = context.AppUsers.FirstOrDefault(x => x.UserEmail == email && x.UserPass == password);
        //    return user != null;
        //}
        public bool DeleteUser(int id)
        {
            var user = context.AppUsers.SingleOrDefault(x => x.UserId == id);
            if(user != null)
            {
                context.AppUsers.Remove(user);
                context.SaveChanges();
                return true;
            }
            return false;
        }
             
       

    }
}
