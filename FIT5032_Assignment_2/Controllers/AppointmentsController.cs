using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_2.Models;

namespace FIT5032_Assignment_2.Controllers
{
    public class AppointmentsController : Controller
    {
        private AppointmentsModel db = new AppointmentsModel();

        // GET: Appointments
        public ActionResult Index()
        {
            return View(db.Appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointments appointments = db.Appointments.Find(id);
            if (appointments == null)
            {
                return HttpNotFound();
            }
            return View(appointments);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,PatientID,DoctorID,AppointmentDate,AggregateRating")] Appointments appointments)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointments);
                Console.WriteLine("AppointmentDate value: " + appointments.AppointmentDate);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointments);
        }
        */

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointments appointments = db.Appointments.Find(id);
            if (appointments == null)
            {
                return HttpNotFound();
            }
            return View(appointments);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,PatientID,DoctorID,AppointmentDate,AggregateRating")] Appointments appointments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointments);
        }

        [HttpPost]
        public String CreateAppointment()
        {

            var appointment = new Appointments
            {
                PatientID = int.Parse(Request.Form["PatientID"]),
                DoctorID = int.Parse(Request.Form["DoctorID"]),
                //AppointmentDate = Request.Form["AppointmentDate"]
                AppointmentDate = DateTime.Parse(Request.Form["AppointmentDate"])

            };

            var vx = Request.Files["Attachment"].ContentLength;

            // Store the attachment in local storage.
            var Str1 = Request.Files[0].FileName.Split('.');
            var FileType = Str1[Str1.Length - 1];
            var FilePath =
                Server.MapPath("~/Uploads/") +
                string.Format(@"{0}", Guid.NewGuid()) +
                "." + FileType;
            Request.Files[0].SaveAs(FilePath);

            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction()) 
                {
                    try
                    {
                        // Add the appointment into the database.
                        db.Appointments.Add(appointment);

                        db.SaveChanges();
                        transaction.Commit(); // 提交事务
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // 回滚事务
                        return "Database Error: " + ex.Message;
                    }
                }
                // Send confirmation email.
                var mail = new MailMessage();
                mail.To.Add(new MailAddress(Request.Form["Email"]));
                mail.From = new MailAddress("Yao_Li_2000@outlook.com");

                mail.Subject = "Appointment Conformation";
                mail.Body =
                    "You made an appointment:\n" +
                    "Patient ID: " + Request.Form["PatientID"] + "\n" +
                    "Doctor ID: " + Request.Form["DoctorID"] + "\n" +
                    "Date: " + Request.Form["AppointmentDate"];
                mail.IsBodyHtml = false;

                var attachment = new System.Net.Mail.Attachment(FilePath);
                mail.Attachments.Add(attachment);

                var smtp = new SmtpClient();
                smtp.Host = "smtp.office365.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential
                    ("Yao_Li_2000@outlook.com", "Myoutlook123");

                smtp.Send(mail);



                return "Success";
            }

            return "Database Unavailable.";
        }


        public ActionResult Rating()
        {
            // Perform any necessary logic
            return View();
        }

        [HttpPost]
        public ActionResult CreateRating(int patientID, int doctorID, int aggregateRating)
        {
            try
            {
                // 查询满足特定 PatientID 和 DoctorID 的 Appointments 行
                var appointment = db.Appointments
                    .FirstOrDefault(a => a.PatientID == patientID && a.DoctorID == doctorID);

                if (appointment != null)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            // 更新找到的 Appointments 行的 AggregateRating 值
                            appointment.AggregateRating = aggregateRating;

                            db.SaveChanges();
                            transaction.Commit(); // 提交事务
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // 回滚事务
                            return Content("Database Error: " + ex.Message);
                        }
                    }

                    return Content("Rating updated successfully!");
                }
                else
                {
                    return Content("No matching appointment found.");
                }
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }




        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointments appointments = db.Appointments.Find(id);
            if (appointments == null)
            {
                return HttpNotFound();
            }
            return View(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointments appointments = db.Appointments.Find(id);
            db.Appointments.Remove(appointments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
