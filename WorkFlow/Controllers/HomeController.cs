using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlow.Controllers
{
    public class HomeController : Controller
    {
        //USER'S QUEUE TABLE
        public JsonResult UserQueue()
        {

            var factor = Session["firstname"].ToString();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=WORKFLOW;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            if (factor == "Gbenga")
            {
                con.Open();
                var query = "SELECT [DATE],[AMOUNT],[DESCRIPTION],[STATUS],[S1_Comment],[S2_Comment],[APPROVAL_STAGE] FROM [WORKFLOW].[dbo].[Cash_Adv] where [STATUS]='Approved by Supervisor1' order by [DATE] asc";
                com.CommandText = query;
                SqlDataAdapter da = new SqlDataAdapter(com);

                DataTable dt = new DataTable();

                da.Fill(dt);
                if (dt != null)
                {
                    var m = JsonConvert.SerializeObject(dt);
                    return Json(new { m, value = 1 });
                }
                return Json(new { m = ".Network Issues...", value = 0 });

            }
            else if (factor == "Olawale")
            {
                con.Open();
                var query = "SELECT [DATE],[AMOUNT],[DESCRIPTION],[STATUS],[S1_Comment],[S2_Comment],[APPROVAL_STAGE] FROM [WORKFLOW].[dbo].[Cash_Adv] where [STATUS]='Pending' order by [DATE] asc";
                com.CommandText = query;
                SqlDataAdapter da = new SqlDataAdapter(com);

                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt != null)
                {
                    var m = JsonConvert.SerializeObject(dt);
                    return Json(new { m, value = 1 });
                }
                return Json(new { m = ".Network Issues...", value = 0 });
            }
            else
            {
                con.Open();
                var query = "SELECT [DATE],[AMOUNT],[DESCRIPTION],[STATUS],[S1_Comment],[S2_Comment],[APPROVAL_STAGE] FROM [WORKFLOW].[dbo].[Cash_Adv] order by [DATE] asc";
                com.CommandText = query;
                SqlDataAdapter da = new SqlDataAdapter(com);

                DataTable dt = new DataTable();

                da.Fill(dt);
                if (dt != null)
                {
                    var m = JsonConvert.SerializeObject(dt);
                    return Json(new { m, value = 1 });
                }
                return Json(new { m = ".Network Issues...", value = 0 });
            }








        }

        //LOGIN 
        public JsonResult Login(string username, string password)
        {
           //API TO AUTHENTICATE AGAINST DB AND RETURN BOLEAN FOR AUTH AND DETAILS OF USERS

            
            // STORING RETURN PARAMETERS IN SESSIONS
            /*
            
            Session["userid"] = dt.Rows[0]["USERNAME"].ToString();
            Session["firstname"] = dt.Rows[0]["FIRSTNAME"].ToString();
            Session["lastname"] = dt.Rows[0]["LASTNAME"].ToString();
            Session["department"] = dt.Rows[0]["DEPARTMENT"].ToString();
            Session["title"] = dt.Rows[0]["TITLE"].ToString();
            Session["email"] = dt.Rows[0]["EMAIL"].ToString();
            Session["role"] = dt.Rows[0]["ROLE"].ToString();

            */

            return Json(new { m = "Test Login", value = 1 });
        }

        // CASH ADVANCE REQUEST
        public JsonResult FirstLevelRequest(string details)
        {

         //   API REQUEST To ENTER REQUEST
            
            /*
            var myFile = csh.SupportingDocument;
            var myFileName = Path.GetFileName(myFile.FileName);

            var mypath = Server.MapPath("~/DocFolder/");
            if (!Directory.Exists(mypath))
            {
                Directory.CreateDirectory(mypath);
            }

            var myFileSaveDirectory = mypath + myFileName;
            myFile.SaveAs(myFileSaveDirectory);


            var preparedby = Session["firstname"].ToString() + " " + Session["lastname"].ToString();



            Cash_Adv encsh = new Cash_Adv();

            Random num = new Random();
            var RanID = num.Next();
            encsh.AMOUNT = csh.amount;
            encsh.APPROVAL_STAGE = "Waiting with Supervisor1";
            encsh.DATE = csh.date;
            encsh.DEPARTMENT = csh.department;
            encsh.DESCRIPTION = csh.description;
            encsh.PREPAREDBY = preparedby;
            encsh.S1_Comment = "Await Supervisor1 Approval";
            encsh.S2_Comment = null;
            encsh.STAFFNUMBER = csh.staffno;
            encsh.STATION = csh.station;
            encsh.STATUS = "Pending";
            encsh.SUPERVISOR1 = "Olawale Kikelomo";
            encsh.SUPERVISOR2 = "Gbenga Akande";
            encsh.TRANSACTION_ID = RanID.ToString();
            encsh.DocDirectory = myFileSaveDirectory;

            db.Cash_Adv.Add(encsh);
            var k = db.SaveChanges();

            if (k > 0)
            {
                //uncomment when there is network

                MailParameters mp = new MailParameters();
                mp.message = "Dear Sir, " + "\n" + "A cash advance from " + encsh.PREPAREDBY + ", is awating your approval." + "\n" + "Kindly log in to the Portal to Approve or reject with Comments." + "\n" + "Log on to http://www.Cashadvance.com Fake-URL for Test." + "\n" + "\n" + "Thank You";
                mp.subject = "Cash Advance Approval";
                mp.mailTo = example@mail.com
                MailShooter ms = new MailShooter();

                var MailSend = ms.EmailSender(mp);
            */

                return Json(new { m = "Your request has been Successfully sent for Approval", value = 1 });
            }

        }

       
        // REJECTED REQUEST

        public JsonResult RejectReq(string TxtnID, string status, string comment)
        {
            con.Open();
            com.Connection = con;
            var role = Session["role"].ToString();

            if (role == "Supervisor1")
            {
                var query = "UPDATE [WORKFLOW].[dbo].[Cash_Adv] SET [APPROVAL_STAGE]='Rejected by Supervisor1',[STATUS] = '" + status + "', [S1_Comment]='" + comment + "' WHERE [TRANSACTION_ID]='" + TxtnID + "'";
                com.CommandText = query;

                var k = com.ExecuteNonQuery();
                if (k > 0)
                {
                    return Json("Request rejected and notification sent to requester");

                }
                else
                {
                    return Json("Network Issues...you may retry.... ");

                }


            }
            else
            {
                var query = "UPDATE [WORKFLOW].[dbo].[Cash_Adv] SET [APPROVAL_STAGE]='Rejected by Supervisor2',[STATUS] = '" + status + "', [S2_Comment]='" + comment + "'WHERE [TRANSACTION_ID]='" + TxtnID + "'";
                com.CommandText = query;

                var k = com.ExecuteNonQuery();

                if (k > 1)
                {
                    return Json("Requested and notification sent to requester");

                }
                else
                {
                    return Json("Network Issues...you may retry.... ");

                }


            }

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

         //MATERIAL REQUEST
        public JsonResult MatReq(MReq mreq)
        {
            var requestor = Session["firstname"] + " " + Session["lastname"];
            mreq.Requestor = requestor;
            Material_Requisition mr = new Material_Requisition();
            mr.AddressTo = mreq.AddressTo;
            mr.Availability = mreq.Availability;
            mr.Date = mreq.Date;
            mr.DeadLineDate = mreq.DeadLineDate;
            mr.Depaartment = mreq.Department;
            mr.MaterialCode = mreq.MaterialCode;
            mr.MaterialDescription = mreq.MaterialDescription;
            mr.QuantityOrdered = mreq.QuantityOrdered;
            mr.QuantiyIssue = mreq.QuantityIssue;
            mr.Requestor = mreq.Requestor;
            mr.Status = "Pending Approval";
            mr.StorePersonnel = mreq.StorePersonnel;
            mr.Supervisor = "Gbenga Akande";
            mr.Supervisor_Remark = "";
            db.Material_Requisition.Add(mr);
            var k=db.SaveChanges();
            if (k>0)
            {

                MailParameters mp = new MailParameters();
                mp.message = "Dear Sir, " + "\n" + "An Item Requisition from " + requestor + ", is awating your approval." + "\n" + "Kindly log in to the Portal to Approve or reject with Comments." + "\n" + "Item Requested : " + mreq.MaterialDescription + "\n" + "Log on to http://www.WorkFlow.com Fake-URL for Test." + "\n" + "\n" + "Thank You";
                mp.subject = "Material Requisition";
                
                var MailSend = ms.EmailSender(mp);


                return Json(new { m = "Your request has been Successfully sent for Approval", value = 1 });
                
            }


            return Json(new {m="Sorry your request fails/Try again" });
        }

    }
}