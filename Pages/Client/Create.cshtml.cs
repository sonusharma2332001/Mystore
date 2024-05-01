using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Mystore.Pages.Client
{
    public class CreateModel : PageModel

    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 

        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length==0 || clientInfo.email.Length==0
                || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                string connectionString = "Data Source=DESKTOP-F12I18V\\SQLEXPRESS01;Initial Catalog=MYSTORE;Integrated Security=True";
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();
                    String sql = "insert into client"+"(name,email,phone,address) values"+"(@name,@email,@phone,@address);";
                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                errorMessage = ex.ToString();
            }

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMessage = "Client Detail Added Succesfully";
            Response.Redirect("/Client/Index");



        }
        


    }
}
