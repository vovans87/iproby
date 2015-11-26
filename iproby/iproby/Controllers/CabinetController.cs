using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;

namespace iproby.Controllers
{
    public class CabinetController : Controller
    {
        //
        // GET: /Cabinet/

        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();
        private static bool isSaved = false;
        private static bool incorrectFile = false;
        private static bool editAvatar = false;
        private static bool showPaymentDialog = false;

        public ActionResult Index()
        {
            if (Session["login"] != null)
            {
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a);
                int contact_id = 0; int customer_id = 0;
                foreach (var item in contact_id_arr)
                {
                    contact_id = item.contact_id;
                    customer_id = item.customer_id;
                }
                var contact_arr = (from a in db.contacts
                                   where a.contact_id == contact_id
                                   select a);
                var announ_id_arr = (from a in db.customer_announ
                                     where a.customer_id == customer_id
                                     select a.announ_id);
                int announ_id = 0;
                foreach (int item in announ_id_arr)
                {
                    announ_id = item;
                }
                var announ_arr = (from a in db.announs
                                  where a.id == announ_id
                                  select a);
                iproby.Models.announ_details details = new iproby.Models.announ_details();
                details.announ_id = announ_id;
                foreach (var item in announ_arr)
                {
                    details.price = item.price;
                    details.subjects = item.subjects;
                    details.header = item.header;
                    details.description = item.description;
                    details.about = item.about;
                }
                foreach (var item in contact_arr)
                {
                    details.address = item.address;
                    details.email = item.email;
                    details.skype = item.skype;
                    details.facebook = item.facebook;
                    details.site = item.site;
                    details.vkontakte = item.vkontakte;
                    details.icq = item.icq;
                    details.first_name = item.first_name;
                    details.avatar = item.avatar;
                    details.login = Session["login"].ToString();
                    details.mobile = item.mobile;
                    details.password = string.Empty;
                }
                if (isSaved)
                {
                    ViewData["isSaved"] = "isSaved";
                    isSaved = false;
                }
                if (incorrectFile)
                {
                    ViewData["incorrectFile"] = "incorrectFile";
                    incorrectFile = false;
                }
                if (editAvatar)
                {
                    ViewData["editAvatar"] = "editAvatar";
                    editAvatar = false;
                }
                ViewData["login"] = "isLogin";
                return View(details);
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditDetails()
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a.contact_id);
                int contact_id = 0;
                foreach (int item in contact_id_arr)
                {
                    contact_id = item;
                }
                var contact_arr = (from a in db.contacts
                                   where a.contact_id == contact_id
                                   select a);
                iproby.Models.announ_details regist = new iproby.Models.announ_details();
                foreach (var item in contact_arr)
                {
                    regist.address = item.address;
                    regist.email = item.email;
                    regist.skype = item.skype;
                    regist.facebook = item.facebook;
                    regist.site = item.site;
                    regist.vkontakte = item.vkontakte;
                    regist.icq = item.icq;
                    regist.first_name = item.first_name;
                    regist.login = Session["login"].ToString();
                    regist.mobile = item.mobile;
                    regist.password = string.Empty;
                }
                return View(regist);
            }
            else
            {
                ViewData["login"] = "notLogin";
                return View();
            }
        }

        public ActionResult Reviews(int announ_id)
        {

            var reviews_arr = (from a in db.reviews
                               where a.announ_id == announ_id
                               select a);
            List<iproby.Models.review> review_list = new List<iproby.Models.review>();
            foreach (var item in reviews_arr)
            {
                iproby.Models.review review = new iproby.Models.review();
                review.header = item.header;
                review.description = item.description;
                review.date_from = item.date_from;
                review.reviewer_id = item.reviewer_id;
                var contact_id_arr = (from a in db.customers
                                      where a.customer_id == item.reviewer_id
                                      select a.contact_id);
                int contact_id = 0;
                foreach (int item_customer in contact_id_arr)
                {
                    contact_id = item_customer;
                }
                var contact_arr = (from a in db.contacts
                                   where a.contact_id == contact_id
                                   select a);
                foreach (var item_inside in contact_arr)
                {
                    review.first_name = item_inside.first_name;
                    review.address = item_inside.address;
                    review.avatar = item_inside.avatar;
                }
                review_list.Add(review);
            }
            return View("~/Views/Cabinet/ReviewListCabinet.cshtml", review_list);
        }


        [HttpPost]
        public ActionResult SaveDetails(iproby.Models.announ_details model)
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a.contact_id);
                int contact_id = 0;
                foreach (int item in contact_id_arr)
                {
                    contact_id = item;
                }
                var contact = db.contacts.Find(contact_id);

                if (contact != null)
                {
                    contact.address = model.address;
                    contact.first_name = model.first_name;
                    contact.mobile = model.mobile;
                    contact.skype = model.skype;
                    contact.facebook = model.facebook;
                    contact.site = model.site;
                    contact.vkontakte = model.vkontakte;
                    contact.icq = model.icq;
                    db.SaveChanges();
                }
                isSaved = true;
                return RedirectToAction("Index");
            }
            else
            {
                isSaved = false;
                return RedirectToAction("Index");
            }

        }

        public ActionResult EditAnnoun()
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a);
                int contact_id = 0; int customer_id = 0;
                foreach (var item in contact_id_arr)
                {
                    contact_id = item.contact_id;
                    customer_id = item.customer_id;
                }
                var announ_id_arr = (from a in db.customer_announ
                                     where a.customer_id == customer_id
                                     select a.announ_id);
                int announ_id = 0;
                foreach (int item in announ_id_arr)
                {
                    announ_id = item;
                }
                var announ_arr = (from a in db.announs
                                  where a.id == announ_id
                                  select a);
                iproby.Models.announ_clients announ = new iproby.Models.announ_clients();

                foreach (var item in announ_arr)
                {
                    announ.price = item.price;
                    announ.subjects = item.subjects;
                    announ.header = item.header;
                    announ.description = item.description;
                    announ.about = item.about;
                }
                if (isSaved)
                {
                    ViewData["isSaved"] = "isSaved";
                    isSaved = false;
                }
                return View(announ);
            }
            else
            {
                ViewData["login"] = "notLogin";
                return View();
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveAnnoun(iproby.Models.announ_clients model)
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a);
                int contact_id = 0; int customer_id = 0;
                foreach (var item in contact_id_arr)
                {
                    contact_id = item.contact_id;
                    customer_id = item.customer_id;
                }
                var announ_id_arr = (from a in db.customer_announ
                                     where a.customer_id == customer_id
                                     select a.announ_id);
                int announ_id = 0;
                foreach (int item in announ_id_arr)
                {
                    announ_id = item;
                }
                var announ = db.announs.Find(announ_id);

                if (announ != null)
                {
                    announ.about = model.about;
                    announ.description = model.description;
                    announ.header = model.header;
                    db.SaveChanges();
                }
                isSaved = true;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["login"] = "notLogin";
                return View();
            }
        }

        public ActionResult MyMessages()
        {

            return View();
        }

        public ActionResult EmptySession()
        {
            ViewData["login"] = null;
            Session["login"] = null;
            Session["fio"] = null;
            ViewData["showPaymentDialog"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditOptions(iproby.Models.options model)
        {
            string login = Session["login"].ToString();
            var contact_id_arr = (from a in db.customers
                                  where a.login == login
                                  select a);
            int customer_id = 0;
            int confirmed_flag = 0;
            foreach (var item in contact_id_arr)
            {
                customer_id = item.customer_id;
                confirmed_flag = item.confirmed_flag.Value;
            }
            var options_arr = (from a in db.options
                                  where a.customer_id == customer_id
                                  select a);
            int flag_send_from_clients = 0;
            foreach (var item in options_arr)
            {
                flag_send_from_clients = item.send_email_from_clients_flag.Value;
            }
            iproby.Models.options options = new iproby.Models.options();
            options.customer_id = customer_id;
            options.send_email_from_clients = flag_send_from_clients;
            options.confirmed_flag = confirmed_flag;
            if (showPaymentDialog) {
                iproby.Models.payment payment = new iproby.Models.payment();
                payment.desc = "Оплата поднятия объявления";
                payment.invid = 1;
                payment.mrchlogin = "testipro";
                payment.outsum = 100;
                payment.password1 = "N9qxZ9di";
                var announ_arr = (from a in db.customer_announ
                                  where a.customer_id == customer_id
                                  select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
                iproby.Data_Model.payment payment_db = new iproby.Data_Model.payment();
                payment_db.customer_id = customer_id;
                payment_db.announ_id = announ_id;
                payment_db.description = payment.desc;
                payment_db.invid = payment.invid;
                payment_db.mrchlogin = payment.mrchlogin;
                payment_db.outsum = payment.outsum;
                payment_db.date_from = DateTime.Now;
                payment_db.status = "try";
                string source=payment.mrchlogin+":"+payment.outsum+":"+payment.invid+":"+payment.password1;
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, source);
                    payment.signaturevalue = hash;
                    payment_db.signaturevalue = hash;
                }
                db.payments.Add(payment_db);
                db.SaveChanges();
                payment.invid = payment_db.id;
                var payments = db.payments.Find(payment_db.id);
                if (payments != null)
                {
                    payments.invid = payment_db.id;
                    string source_new = payment.mrchlogin + ":" + payment.outsum + ":" + payment_db.id + ":" + payment.password1;
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = GetMd5Hash(md5Hash, source_new);
                        payment.signaturevalue = hash;
                        payment_db.signaturevalue = hash;
                    }
                    db.SaveChanges();
                }
                ViewData["showPaymentDialog"] = "showPaymentDialog";
                showPaymentDialog = false;
                options.payment = payment;
            }

            return View(options);
        }

        [HttpPost]
        public ActionResult SaveOptions(iproby.Models.options model)
        {
            string login = Session["login"].ToString();
            var contact_id_arr = (from a in db.customers
                                  where a.login == login
                                  select a);
            int customer_id = 0;
            foreach (var item in contact_id_arr)
            {
                customer_id = item.customer_id;
            }
            var option_id = (from a in db.options
                                   where a.customer_id == customer_id
                                  select a.id);
            int opt_id = 0;
            foreach (var item in option_id)
            {
                opt_id = item;
            }
            var options = db.options.Find(opt_id);
            if (options != null)
            {
                options.send_email_from_clients_flag = model.send_email_from_clients;
                db.SaveChanges();
            }
            if (model.status_vip_flag == 1)
            {
                showPaymentDialog = true;
            }
            else {
                showPaymentDialog = false;
            }
            return RedirectToAction("EditOptions", "Cabinet"); 
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {

                string path = AppDomain.CurrentDomain.BaseDirectory + "Content/img/user_img";
                string filename = Path.GetFileName(file.FileName);
                int length = file.ContentLength;
                string new_filename = filename.Substring(filename.IndexOf("."), filename.Length - filename.IndexOf("."));
                if (length < 100000 || new_filename.ToUpper()==".JPG"||new_filename.ToUpper()==".PNG"||new_filename.ToUpper()==".JPEG")
                {
                    new_filename = Session["login"] + "ava" + new_filename;
                    if (filename != null)
                    {
                        file.SaveAs(Path.Combine(path, new_filename));
                        string fileName = Path.Combine(path, new_filename);
                        Size size = new Size();
                        size.Width = 100;
                        size.Height = 100;
                        Image img = Image.FromFile(fileName);
                        Bitmap tmp = new Bitmap(img,size);
                        string WorkingDirectory = path;
                        Image imgPhotoVert = Image.FromFile(fileName);
                        Image imgPhotoHoriz = Image.FromFile(fileName);
                        Image imgPhoto = null;
                        imgPhoto = Crop(imgPhotoVert, 60, 60, AnchorPosition.Center);
                        string target_img = Session["login"] + "ava_cropped.jpg";
                        imgPhoto.Save(WorkingDirectory +"/"+
                               target_img, ImageFormat.Jpeg);
                        imgPhoto.Dispose();

                        string login = Session["login"].ToString();
                        var contact_id_arr = (from a in db.customers
                                              where a.login == login
                                              select a.contact_id);
                        int contact_id = 0;
                        foreach (int item in contact_id_arr)
                        {
                            contact_id = item;
                        }
                        var contact = db.contacts.Find(contact_id);

                        if (contact != null)
                        {
                            contact.avatar = new_filename;
                            contact.avatar_cropped = target_img;
                            db.SaveChanges();
                            editAvatar = false;
                        }
                    }
                }
                else {
                    incorrectFile = true;
                }
            }
            return RedirectToAction("Index","Cabinet");
        }

        enum AnchorPosition
        {
            Top,
            Center,
            Bottom,
            Left,
            Right
        }

        static Image Crop(Image imgPhoto, int Width, int Height, AnchorPosition Anchor)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (Anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int)
                            (Height - (sourceHeight * nPercent));
                        break;
                    default:
                        destY = (int)
                            ((Height - (sourceHeight * nPercent)) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (Anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int)
                          (Width - (sourceWidth * nPercent));
                        break;
                    default:
                        destX = (int)
                          ((Width - (sourceWidth * nPercent)) / 2);
                        break;
                }
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width,
                    Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                    imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode =InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
