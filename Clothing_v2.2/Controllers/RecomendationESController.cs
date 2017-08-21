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
    public class RecomendationESController : Controller
    {
        // GET: RecomendationES
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuestionForES()
        {
            var model = new QuestionsModels();
            return View(model);
        }

        //public ActionResult TheBad()
        //{
        //    var model = Que.GetMovie();
        //    ViewData["AllGenres"] = from genre in Data.GetGenres()
        //                            select new SelectListItem { Text = genre.Name, Value = genre.Id.ToString() };
        //    return View(model);
        //}
        [HttpPost]
        public ActionResult QuestionForES(QuestionsModels model)
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
                return View(model);
            }
            string query = "SELECT * FROM Profile WHERE Id=@userId";
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
                ViewData["Message"] = "Cant't update 3";
                return View();
            }
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProfileModels profile = new ProfileModels();
            if (dr.Read())
            {
                profile.Growth = float.Parse(dr.GetValue(1).ToString());
                profile.Weight = float.Parse(dr.GetValue(2).ToString());
                profile.Bust = float.Parse(dr.GetValue(3).ToString());
                profile.Waist = float.Parse(dr.GetValue(4).ToString());
                profile.Hip = float.Parse(dr.GetValue(5).ToString());
                profile.Shoes_size = float.Parse(dr.GetValue(6).ToString());
            }
            dr.Close();
            conn.Open();

            bool HIGH_ = false;
            bool FAT_ = false;
            RecomendationESModels recom = new RecomendationESModels();

            recom.Action = model.Actions.Where(x=>x.Value == model.Select_action).Select(v=>v.Text).First();
            // string name_recom = "Select ActionName FROM ActionNames WHERE Id = recom.Model_suit";

            if (profile.Bust < 97)
                recom.Size_up = "44-46";
            else if (profile.Bust >= 97 && profile.Bust < 105)
                recom.Size_up = "46-48";
            else if (profile.Bust >= 105 && profile.Bust < 113)
                recom.Size_up = "48-50";
            else if (profile.Bust >= 113 && profile.Bust < 121)
                recom.Size_up = "50-52";
            else if (profile.Bust >= 121)
                recom.Size_up = "52-54";

            if (profile.Bust < 97)
                recom.Size_down = "44-46";
            else if (profile.Hip >= 97 && profile.Bust < 105)
                recom.Size_down = "46-48";
            else if (profile.Hip >= 105 && profile.Bust < 113)
                recom.Size_down = "48-50";
            else if (profile.Hip >= 113 && profile.Bust < 121)
                recom.Size_down = "50-52";
            else if (profile.Hip >= 121)
                recom.Size_down = "52-54";


            if (profile.Weight > 100)
                FAT_ = true;
            if (profile.Growth >= 175)
                HIGH_ = true;
            if (model.Select_action == "1" || model.Select_action == "2" || model.Select_action == "4" || model.Select_action == "7")
            {
                if (HIGH_ == true && FAT_ == false)
                    recom.Model_suit = "Костюм тройка или Двуборный костюм";
                else recom.Model_suit = "Костюм тройка";
                recom.ColorSuit = "Черный костюм";
                recom.ColourTie = "В цвет костюма";
                recom.ColourShirt = "Белая или в светлых тонах рубашка";
            }

            if (model.Select_action == "3" || model.Select_action == "5")
            {
                if (HIGH_ == true)
                    recom.Model_suit = "Одноборный костюм";
                else recom.Model_suit = "Костюм тройка";
                recom.ColorSuit = "Синий (любые оттенки), Фиолетовый, костюмы в крупную клетку";
                recom.ColourTie = "В контраст с костюмом или без галстука";
                recom.ColourShirt = "Белая или в светлых тонах рубашка";
            }

            if (model.Select_action == "6")
            {
                recom.Model_suit = "Одноборный костюм или Костюм тройка";
                recom.ColorSuit = "Синий (любые оттенки), так же можно в тонкую полоску";
                recom.ColourTie = "В цвет костюма";
                recom.ColourShirt = "Белая или в светлых тонах рубашка";
            }

            string query1 = "INSERT INTO RecomendationES (Action,Size_Up, Size_Down, Model, " +
                     "CSuit, CTie, CShirt, " +
                     "Id) VALUES(@action, @size_up, @size_down, @model, " +
                     "@csuit,@ctie,@cshirt, @id)";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@Id";
            param1.Value = userID;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@CShirt";
            param1.Value = recom.ColourShirt;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@CTie";
            param1.Value = recom.ColourTie;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@CSuit";
            param1.Value = recom.ColorSuit;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@model";
            param1.Value = recom.Model_suit;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@size_down";
            param1.Value = recom.Size_down;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@size_up";
            param1.Value = recom.Size_up;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@action";
            param1.Value = model.Select_action;
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
                ViewData["Message"] = "Cant't Insert " + ex;
                return View();
            }
            ViewData["action"] = recom.Action;
            ViewData["Size_UP"] = recom.Size_up;
            ViewData["Size_DOWN"] = recom.Size_down;
            ViewData["Model"] = recom.Model_suit;
            ViewData["CSuit"] = recom.ColorSuit;
            ViewData["CTie"] = recom.ColourTie;
            ViewData["CShirt"] = recom.ColourShirt;
            
            //ViewBag.Actions = new SelectList { }

            dr.Close();
            string suitQuery = "SELECT DISTINCT c.Id, c.colorName AS SuitColor " +
                "                   FROM Color c " +
                "                   INNER JOIN AcceptableColors ac ON c.Id = ac.SuitColor" +
                "                   WHERE Action=@action";
            SqlCommand suitCmd = new SqlCommand(suitQuery, conn);
            SqlParameter suitParam = new SqlParameter();
            suitParam.ParameterName = "@action";
            suitParam.Value = model.Select_action;
            suitParam.SqlDbType = SqlDbType.NVarChar;
            suitCmd.Parameters.Add(suitParam);
            try
            {
                suitCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Can't update. " + ex);
                ViewData["Message"] = "Cant't update 4" +ex;
                return View();
            }
            SqlDataReader suitDr = suitCmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<SelectListItem> suitColors = new List<SelectListItem>();
            while (suitDr.Read())
            {
                string val = suitDr.GetValue(1).ToString();
                string id = suitDr.GetValue(0).ToString();
                suitColors.Add(new SelectListItem { Text = val, Value = id });
                //ViewData["Message"] += val + ";";
            }
            //IEnumerable<SelectListItem> blah = suitColors;
            ViewData["SuitColors"] = suitColors;
            List<SelectListItem> shirtColors = new List<SelectListItem>();
            shirtColors.Add(new SelectListItem { Text = "Сначала выберите цвет костюма", Disabled = true });
            ViewData["ShirtColors"] = shirtColors;
            List<SelectListItem> tieColors = new List<SelectListItem>();
            tieColors.Add(new SelectListItem { Text = "Сначала выберите цвет костюма и рубашки", Disabled = true });
            ViewData["TieColors"] = tieColors;

            suitDr.Close();
            dr.Close();
            conn.Close();
            return View(model);
        }

        public JsonResult GetShirt(string someMoreAction, string suitColor)
        {
            List<SelectListItem> result = new List<SelectListItem>();
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
                RedirectToAction("Index", "Home");
            }
            string query = "SELECT DISTINCT c.Id,c.colorName AS ShirtColor " +
                "                   FROM Color c " +
                "                   INNER JOIN AcceptableColors ac ON c.Id = ac.ShirtColor" +
                "                   WHERE Action=@action AND SuitColor = @suitColor";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@action";
            param.Value = someMoreAction;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@suitColor";
            param.Value = Int32.Parse(suitColor);
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Can't select. " + ex);
                ViewData["Message"] = "Cant't select 2" +ex;
                RedirectToAction("Index", "Home");
            }
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProfileModels profile = new ProfileModels();
            while (dr.Read())
            {
                string val = dr.GetValue(1).ToString();
                string id = dr.GetValue(0).ToString();
                result.Add(new SelectListItem { Text = val, Value = id });
            }
            dr.Close();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTie(string someMoreAction, string suitColor, string shirtColor)
        {
            List<SelectListItem> result = new List<SelectListItem>();
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
                RedirectToAction("Index", "Home");
            }
            string query = "SELECT DISTINCT c.Id,c.colorName AS TieColor " +
                "                   FROM Color c " +
                "                   INNER JOIN AcceptableColors ac ON c.Id = ac.TieColor" +
                "                   WHERE Action=@action AND SuitColor = @suitColor AND ShirtColor = @shirtColor";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@action";
            param.Value = someMoreAction;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@suitColor";
            param.Value = Int32.Parse(suitColor);
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@shirtColor";
            param.Value = Int32.Parse(shirtColor);
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Can't select. " + ex);
                ViewData["Message"] = "Cant't select 8" + ex;
                RedirectToAction("Index", "Home");
            }
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProfileModels profile = new ProfileModels();
            while (dr.Read())
            {
                string val = dr.GetValue(1).ToString();
                string id = dr.GetValue(0).ToString();
                result.Add(new SelectListItem { Text = val, Value = id });
            }
            dr.Close();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string SaveRecomendation(string CSuit,string CShirt,string CTie)
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
                return "1";
            }
            string query = "SELECT MAX(Id_recomendation) FROM RecomendationES WHERE Id=@userId";
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
                ViewData["Message"] = "Cant't select 9" + ex;
                return "3";
            }
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            string recomId = "tot";
            while (dr.Read())
            {
                recomId = dr.GetValue(0).ToString();
            }
            dr.Close();
            conn.Open();

            string query1 = "INSERT INTO Rec_ES (Id_user, Id_recomendation, CSuit_ES, CTie_ES, CShirt_ES) VALUES(@Id, @reco_id, @CSuit,@CTie,@CShirt)";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@Id";
            param1.Value = userID;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@reco_id";
            param1.Value = recomId;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@CShirt";
            param1.Value = CShirt==null?"":CShirt;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@CTie";
            param1.Value = CTie == null ? "" : CTie;
            param1.SqlDbType = SqlDbType.NVarChar;
            cmd1.Parameters.Add(param1);

            param1 = new SqlParameter();
            param1.ParameterName = "@CSuit";
            param1.Value = CSuit == null ? "" : CSuit;
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
                return "2 "+ex;
            }
            return "0";
        }


    }
}
