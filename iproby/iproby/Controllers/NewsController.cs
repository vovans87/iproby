using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Odbc;

namespace iproby.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            string connetionString = "Driver={MySQL ODBC 3.51 Driver};Server = mssql2.win.agava.net; UID = iprob_wordpres_2; PWD = Se9sa2^9; Database = iproby94_wordpress_8; ";
            OdbcConnection cnn;
            List<iproby.Models.news> newsList = new List<Models.news>();
            cnn = new OdbcConnection(connetionString);
            try
            {
                cnn.Open();
                OdbcCommand DbCommand = cnn.CreateCommand();
                DbCommand.CommandText = "SELECT `post_title`,`post_content`,`post_date`,`guid` FROM `wp_posts` WHERE `post_status`='publish'";
                OdbcDataReader DbReader = DbCommand.ExecuteReader();
                int fCount = DbReader.FieldCount;

                while (DbReader.Read())
                {
                    iproby.Models.news news = new iproby.Models.news();
                    news.header = DbReader.GetString(0);
                    string description = DbReader.GetString(1);
                    if (description.Length > 320)
                    {
                        news.description = DbReader.GetString(1).Substring(0, 320);
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
               
            }
            

            return View(newsList);
        }

        public ActionResult SeoFooter(int type_id)
        {
            return View();
        }

    }
}
