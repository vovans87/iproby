using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Odbc;
using iproby.Data_Model;
using iproby.Models;

namespace iproby.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult Index()
        {
            string connetionString = "Driver={MySQL ODBC 3.51 Driver};Server = MSSQL2; UID = iprob_wordpres_2; PWD = aE5fv0*1; Database = iproby94_wordpress_8; ";
            OdbcConnection cnn;
            List<iproby.Models.news> newsList = new List<Models.news>();
            cnn = new OdbcConnection(connetionString);
            try
            {
                cnn.Open();
                OdbcCommand DbCommand = cnn.CreateCommand();
                DbCommand.CommandText = "SELECT `post_title`,`post_content`,`post_date`,`guid` FROM `wp_posts` WHERE `post_status`='publish' LIMIT 3";
                OdbcDataReader DbReader = DbCommand.ExecuteReader();
                int fCount = DbReader.FieldCount;

                while (DbReader.Read())
                {
                    iproby.Models.news news = new iproby.Models.news();
                    news.header = DbReader.GetString(0);
                    string description = DbReader.GetString(1);
                    if (description.Length > 320)
                    {
                        news.description = TruncateAtWord(DbReader.GetString(1),320);
                    }
                    else
                    {
                        news.description = DbReader.GetString(1);
                    }
                    news.date_from = DbReader.GetDateTime(2);
                    news.link = DbReader.GetString(3);
                    newsList.Add(news);
                }


                DbReader.Close();
                DbCommand.Dispose();
                cnn.Close();
             
            }
            catch (Exception ex)
            {
                List<iproby.Models.news> newsList1 = new List<Models.news>();
                iproby.Models.news news = new iproby.Models.news();
                news.description = ex.Message.ToString();
                newsList.Add(news);
            }
            

            return View(newsList);
        }

        private static string TruncateAtWord(string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

        public ActionResult SeoFooter(int type_id)
        {
            var type_arr = (from a in db.announ_type
                            where a.id == type_id
                            select a.seo_text);
            string seo_text = string.Empty;
            foreach (string item in type_arr)
            {
                seo_text = item;
            }
            seo_model seo = new seo_model();
            seo.seo_text = seo_text;

            return View("~/Views/News/SeoFooterContent.cshtml", seo);
        }

    }
}
