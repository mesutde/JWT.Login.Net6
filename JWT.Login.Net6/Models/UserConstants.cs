namespace JWT.Login.Net6.Models
{
    public class UserConstants
    {

        public static List<UserModel> Users = new List<UserModel>()
        {

            new UserModel ()
            {
                Username="mesut61",
                EmailAdress="mesut2513@hotmail.com",
                Password="123",
                Name="mesut",
                Surname="demirci",
                Role="Administrator"
            },
             new UserModel ()
            {
                Username="halit15",
                EmailAdress="halit@hotmail.com",
                Password="15265",
                Name="halit",
                Surname="damlarca",
                Role="Seller"
            },


        };


    }
}
