using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Mystore.Pages.Client
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try{
                String ConnectionString = "Data Source=DESKTOP-F12I18V\\SQLEXPRESS01;Initial Catalog=MYSTORE;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String Sql = "select * from client where id=@id";
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name =  reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }

                    }
                }

            }
            catch(Exception e){
                errorMessage = e.Message;

            }

        }

        public void OnPost() 
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.id.Length==0||clientInfo.name.Length==0|| clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length==0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                String ConnectionString = "Data Source=DESKTOP-F12I18V\\SQLEXPRESS01;Initial Catalog=MYSTORE;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String Sql = "UPDATE client " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address " +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", clientInfo.id);
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();

                    }
                }

            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;

            }
            Response.Redirect("/Client/Index");

        }
    }
}
