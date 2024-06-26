using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Mystore.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();


        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-F12I18V\\SQLEXPRESS01;Initial Catalog=MYSTORE;Integrated Security=True";
                using (SqlConnection Connection = new SqlConnection(connectionString)) 
                {
                    Connection.Open();
                    String sql = "select * from Client";
                    using (SqlCommand command = new SqlCommand(sql, Connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                              ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.address = reader.GetString(3);
                                clientInfo.phone = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                                listClients.Add(clientInfo);
                            }
                            if (listClients.Count == 0)
                            {
                            }

                        }
                    }
                 
                
                
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }


    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}
