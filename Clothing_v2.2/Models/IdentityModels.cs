using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data;
using System.Data.SqlClient;

namespace Clothing_v2._2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Realname { get; set; }
        public static string GetRealName()
        {
            //string userID = User.Identity.GetUserId();
            //string connStr = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-Clothing_v2.2-20170510013353.mdf;Initial Catalog=aspnet-Clothing_v2.2-20170510013353;Integrated Security=True";
            //SqlConnection conn = new SqlConnection(connStr);
            //try
            //{
            //    //пробуем подключится
            //    conn.Open();
            //}
            //catch (SqlException se)
            //{
            //    return "BAD Connection";
            //}
            //string query = "SELECT Realname FROM Profile WHERE Id=@userId";
            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlParameter param = new SqlParameter();
            //param.ParameterName = "@userId";
            //param.Value = userID;
            //param.SqlDbType = SqlDbType.NVarChar;
            //cmd.Parameters.Add(param);
            //try
            //{
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    return "Bad Query";
            //}
            //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //ProfileModels profile = new ProfileModels();
            //if (dr.Read())
            //{
            //    return dr.GetValue(0).ToString();
            //}
          
            return "Anon";
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}