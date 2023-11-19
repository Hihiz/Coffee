using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Coffee.Client.Models
{
    public class PostRequestUser
    {
        public LoginModel GetAuthResponse(ISession session)
        {
            if (session != null)
            {
                LoginModel loginModel = new LoginModel
                {
                    Email = session.GetString("userLogin"),
                    Password = session.GetString("userPassword")
                };

                return loginModel;
            }

            return null;
        }

        public void GetInfoCurrentAuthUser(ViewDataDictionary viewData, ISession session)
        {
            viewData["userLastName"] = session.GetString("userLastName");
            viewData["userFirstName"] = session.GetString("userFirstName");

            viewData["userLogin"] = session.GetString("userLogin");
            viewData["userRole"] = session.GetString("userRole");
        }
    }
}
