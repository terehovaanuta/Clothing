using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Clothing_v2._2.Models;

namespace Clothing_v2._2.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProfileForES()
        {
            string userID = User.Identity.GetUserId();
            string connStr = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-Clothing_v2.2-20170510013353.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                ModelState.AddModelError("", "can't open connection" + se);
                ViewData["Message"] = "no";
                return View();
            }
            string query = "SELECT * FROM Profile WHERE Id=@userId";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@userId";
            param.Value = userID;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ViewData["SkinColor"] = null;
            ViewData["HairColor"] = null;
            if (dr.Read())
            {
                ProfileModels model = new ProfileModels();
                model.Growth = float.Parse(dr.GetValue(1).ToString());
                model.Weight = float.Parse(dr.GetValue(2).ToString());
                model.Bust = float.Parse(dr.GetValue(3).ToString());
                model.Waist = float.Parse(dr.GetValue(4).ToString());
                model.Hip= float.Parse(dr.GetValue(5).ToString());
                model.Shoes_size = float.Parse(dr.GetValue(6).ToString());
                ViewData["SkinColor"] = dr.GetValue(8).ToString();
                ViewData["HairColor"] = dr.GetValue(9).ToString();
                dr.Close();
                conn.Close();
                return View(model);
            }
            dr.Close();
            conn.Close();
            return View();
        }

        [HttpPost]
        public ActionResult ProfileForES(ProfileModels model)
        {
            //ViewData["Message"] = "selected" + model.Select_action;
            string userID = User.Identity.GetUserId();
            string connStr = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-Clothing_v2.2-20170510013353.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                ModelState.AddModelError("", "can't open connection" + se);
                ViewData["Message"] = "no";
                return View(model);
            }
            string query = "SELECT * FROM Profile WHERE Id=@userId";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@userId";
            param.Value = userID;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dr.Read();
            string Blah = "No rows";
            if (dr.HasRows)
            {
                dr.Close();
                conn.Open();
                string query1 = "UPDATE Profile SET Growth=@Growth, Weight=@Weight, Bust = @Bust, " +
                    "Waist = @Waist, Hip = @Hip, Shoes_size = @Shoes_size WHERE Id=@userId";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@userId";
                param1.Value = userID;
                param1.SqlDbType = SqlDbType.NVarChar;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Growth";
                param1.Value = Convert.ToDouble(model.Growth);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);
                
                param1 = new SqlParameter();
                param1.ParameterName = "@Weight";
                param1.Value = Convert.ToDouble(model.Weight);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);
                
                param1 = new SqlParameter();
                param1.ParameterName = "@Bust";
                param1.Value = Convert.ToDouble(model.Bust);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Waist";
                param1.Value = Convert.ToDouble(model.Waist);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Hip";
                param1.Value = Convert.ToDouble(model.Hip);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);
                
                param1 = new SqlParameter();
                param1.ParameterName = "@Shoes_size";
                param1.Value = Convert.ToDouble(model.Shoes_size);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);
                
                /*/check parametres if null here
                param1 = new SqlParameter();
                param1.ParameterName = "@SkinColor";
                param1.Value = model.SkinColour;
                param1.SqlDbType = SqlDbType.NVarChar;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@HairColor";
                param1.Value = model.HairColour;
                param1.SqlDbType = SqlDbType.NVarChar;
                cmd1.Parameters.Add(param1);*/
                try
                {
                    ViewData["Message"] = "updated";

                    cmd1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    ModelState.AddModelError("", "Can't update. " + ex);
                    ViewData["Message"] = "Cant't update 1 " +ex;
                    return View();
                }
            }
            else
            {
                dr.Close();
                conn.Open();

                string query1 = "INSERT INTO Profile (Id,Growth, Weight, Bust, " +
                    "Waist, Hip, Shoes_size, " +
                    "SkinColor, HairColor) VALUES(@userId, @Growth, @Weight, @Bust, " +
                    "@Waist,@Hip,@Shoes_size," +
                    "@SkinColor, @HairColor)";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@userId";
                param1.Value = userID;
                param1.SqlDbType = SqlDbType.NVarChar;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Growth";
                param1.Value = Convert.ToDouble( model.Growth);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Weight";
                param1.Value = Convert.ToDouble(model.Weight);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Bust";
                param1.Value = Convert.ToDouble(model.Bust);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Waist";
                param1.Value = Convert.ToDouble(model.Waist);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Hip";
                param1.Value = Convert.ToDouble(model.Hip);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Shoes_size";
                param1.Value = Convert.ToDouble(model.Shoes_size);
                param1.SqlDbType = SqlDbType.Float;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@SkinColor";
                param1.Value = model.SkinColour;
                param1.SqlDbType = SqlDbType.NVarChar;
                cmd1.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@HairColor";
                param1.Value = model.HairColour;
                param1.SqlDbType = SqlDbType.NVarChar;
                cmd1.Parameters.Add(param1);
                try
                {
                    ViewData["Message"] = "Inserted";

                    cmd1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    ModelState.AddModelError("", "Can't update. " + ex);
                    ViewData["Message"] = "Cant't Insert " +ex ;
                    return View();
                }
            }


            return RedirectToAction("Index","Home");
        }
    }
}