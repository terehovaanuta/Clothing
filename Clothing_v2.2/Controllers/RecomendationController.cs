using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

namespace Clothing_v2._2.Controllers
{
    public class RecomendationController : Controller
    {
        // GET: Recomendation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Recomendation()
        {
            //<tr>
            //    <td><p>Мероприятие</p></td>
            //    <td><p>Размер рубашки и костюма</p></td>
            //    <td><p>Размер брюк</p></td>
            //    <td><p>Модель костюма</p></td>
            //    <td><p>Цвет костюма</p></td>
            //    <td><p>Цвет рубашки</p></td>
            //    <td><p>Цвет гастука</p></td>
            //</tr>
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
            string query = "SELECT an.ActionName, Size_Up,Size_Down,Model,CSuit,CShirt,CTie,Id_recomendation " +
                "FROM RecomendationES r INNER JOIN ActionNames an ON(r.Action=an.Id) WHERE r.Id=@userId";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@userId";
            param.Value = userID;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Can't update. " + ex);
                ViewData["Message"] = "Cant't update 13"+ex;
                return View();
            }
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            string table = "";

            int line = 0;
            List <string[]> arr_recomendation = new List<string[]>();

            while (dr.Read())
            {
                string[] arr_help = new string[8];
                arr_help[0] = dr.GetValue(0).ToString();
                arr_help[1] = dr.GetValue(1).ToString();
                arr_help[2] = dr.GetValue(2).ToString();
                arr_help[3] = dr.GetValue(3).ToString();
                arr_help[4] = dr.GetValue(4).ToString();
                arr_help[5] = dr.GetValue(5).ToString();
                arr_help[6] = dr.GetValue(6).ToString();
                arr_help[7] = dr.GetValue(7).ToString();

                arr_recomendation.Add(arr_help);
                line++;
            }
            dr.Close();

            
            line--;

            conn.Open();
            while (line >= 0)
            {
                table += "<tr>" +
               "<td><p>" + arr_recomendation[line][0] + "</p></td>" +
               "<td><p>" + arr_recomendation[line][1] + "</p></td>" +
               "<td><p>" + arr_recomendation[line][2] + "</p></td>" +
               "<td><p>" + arr_recomendation[line][3] + "</p></td>" +
               "<td><p>" + arr_recomendation[line][4] + "</p></td>" +
               "<td><p>" + arr_recomendation[line][5] + "</p></td>" +
               "<td><p>" + arr_recomendation[line][6] + "</p></td>" +
            "</tr>";
                
                string query_ES = "SELECT cs.colorName, csh.colorName, ct.colorName " +
              "FROM Rec_ES INNER JOIN Color cs " +
              "ON CSuit_ES = cs.Id " +
              "INNER JOIN Color csh " +
              "ON CShirt_ES = csh.Id " +
              "INNER JOIN Color ct " +
              "ON CTie_ES = ct.Id " +
              "WHERE Id_user=@Id_user AND Id_recomendation = @Id_recomendation";
                SqlCommand cmd_ES = new SqlCommand(query_ES, conn);
                SqlParameter param_ES = new SqlParameter();
                param_ES.ParameterName = "@Id_user";
                param_ES.Value = userID;
                param_ES.SqlDbType = SqlDbType.NVarChar;
                cmd_ES.Parameters.Add(param_ES);

                param_ES = new SqlParameter();
                param_ES.ParameterName = "@Id_recomendation";
                param_ES.Value = arr_recomendation[line][7];
                param_ES.SqlDbType =  SqlDbType.NVarChar;
                cmd_ES.Parameters.Add(param_ES);
                try
                {
                    cmd_ES.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Can't update. " + ex);
                    ViewData["Message"] = "Cant't update 3" + ex;
                    return View();
                }
                SqlDataReader dr_ES = cmd_ES.ExecuteReader();
                while (dr_ES.Read())
                {
                    table += "<tr>" +
                   "<td></td>" +
                   "<td></td>" +
                   "<td></td>" +
                   "<td></td> " +
                   "<td><p>" + dr_ES.GetValue(0) + "</p></td>" +
                   "<td><p>" + dr_ES.GetValue(1) + "</p></td>" +
                   "<td><p>" + dr_ES.GetValue(2) + "</p></td>" +
                "</tr>";
                }
                dr_ES.Close();

                line--;
            }
            ViewData["TBody"] = table;

            /*
            string query_ES = "SELECT cs.colorName, csh.colorName, ct.colorName " +
              "FROM Rec_ES INNER JOIN Color cs " +
              "ON CSuit_ES = cs.Id " +
              "INNER JOIN Color csh " +
              "ON CShirt_ES = csh.Id " +
              "INNER JOIN Color ct " +
              "ON CTie_ES = ct.Id " +
              "WHERE Id_user=@Id_user AND Id_recomendation = @Id_recomendation";
            SqlCommand cmd_ES = new SqlCommand(query_ES, conn);
            SqlParameter param_ES = new SqlParameter();
            param_ES.ParameterName = "@Id_user";
            param_ES.Value = userID;
            param_ES.SqlDbType = SqlDbType.NVarChar;
            cmd_ES.Parameters.Add(param_ES);

            param_ES = new SqlParameter();
            param_ES.ParameterName = "@Id_recomendation";
            param_ES.Value = dr.GetValue(7).ToString();
            param_ES.SqlDbType = SqlDbType.NVarChar;
            cmd_ES.Parameters.Add(param_ES);
            try
            {
                cmd_ES.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Can't update. " + ex);
                ViewData["Message"] = "Cant't update 3" + ex;
                return View();
            }
            SqlDataReader dr_ES = cmd_ES.ExecuteReader();
            string table_ES = "";
            while (dr_ES.Read())
            {
                table += "<tr>" +
               "<td><p>" + dr_ES.GetValue(0) + "</p></td>" +
               "<td><p>" + dr_ES.GetValue(1) + "</p></td>" +
               "<td><p>" + dr_ES.GetValue(2) + "</p></td>" +
            "</tr>";
            }
            dr_ES.Close();*/
            //ViewData["Body_ES"] = table_ES;


            return View();
        }
    }
}