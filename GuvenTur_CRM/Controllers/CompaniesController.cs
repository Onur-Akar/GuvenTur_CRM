using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuvenTur_CRM.Models;
using System.Web.Services;
using System.Web.Script;
using System.Configuration;

namespace GuvenTur_CRM.Controllers
{
    public class CompaniesController : Controller
    {
        GuvenTurDBModel db = new GuvenTurDBModel();
        // GET: Companies
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("/Authentication/Login");

                return null;
            }
            else
            {
                List<int> borrowList = new List<int>();
                List<int> paymentList = new List<int>();

                int levelId = 1 /*Convert.ToInt32(Session["UserLevel"].ToString())*/;
                int userId = 1/*Convert.ToInt32(Session["UserId"].ToString())*/;

                List<Companies> companies = db.Companies.OrderBy(o => o.Service_Name).ToList();
                List<Member_Payments> memberPayments = db.Member_Payments.ToList();
                List<Members> members = db.Members.ToList();

                Level_Settings levelSettings =
                    db.Level_Settings.Single(o => o.Level_Id == levelId);

                User_Special_Settings specialSettings =
                    db.User_Special_Settings.SingleOrDefault(o => o.User_Id == userId);

                if (specialSettings != null)
                {
                    ViewBag.Add_Company = specialSettings.Add_Company ?? levelSettings.Add_Company;

                    ViewBag.Delete_Company = specialSettings.Delete_Company ?? levelSettings.Delete_Company;

                    ViewBag.See_Comp_Accounts = specialSettings.See_Comp_Accounts ?? levelSettings.See_Comp_Accounts;
                }
                else
                {
                    ViewBag.Add_Company = levelSettings.Add_Company;
                    ViewBag.Delete_Company = levelSettings.Delete_Company;
                    ViewBag.See_Comp_Accounts = levelSettings.See_Comp_Accounts;
                }

                foreach (Companies item in companies)
                {
                    var payments = from mp in memberPayments
                                   join m in members on mp.Member_Id equals m.Id
                                   join c in companies on m.Service_Id equals c.Id
                                   where c.Id == item.Id
                                   select new { mp.Payment };

                    var borrow = members.Where(o => o.Service_Id == item.Id);


                    borrowList.Add(borrow.Sum(o => o.Borrow));
                    paymentList.Add(payments.Sum(o => o.Payment));
                }

                int totalTakens = members.Sum(o => o.Borrow);
                int totalPayments = paymentList.Sum();


                ViewBag.Taken = borrowList;
                ViewBag.Payment = paymentList;
                ViewBag.Companies = companies;

                ViewBag.TotalTakens = totalTakens;
                ViewBag.TotalPayments = totalPayments;
            }

            return View(ViewBag);
        }

        public ActionResult Company_Detail(int id)
        {
            List<int> borrowList = new List<int>();
            List<int> paymentList = new List<int>();
            List<int> salaryList = new List<int>();
            List<int> vehcPaymentList = new List<int>();

            int levelId = Convert.ToInt32(Session["UserLevel"].ToString());
            int userId = Convert.ToInt32(Session["UserId"].ToString());

            Level_Settings levelSettings =
                    db.Level_Settings.Single(o => o.Level_Id == levelId);

            User_Special_Settings specialSettings =
                db.User_Special_Settings.SingleOrDefault(o => o.User_Id == userId);

            if (specialSettings != null)
            {
                ViewBag.Edit_Company = specialSettings.Edit_Company ?? levelSettings.Edit_Company;
                ViewBag.See_Comp_Accounts = specialSettings.See_Comp_Accounts ?? levelSettings.See_Comp_Accounts;
            }

            Companies company = db.Companies.Single(o => o.Id == id);
            List<Members> members = db.Members.Where(o => o.Service_Id == id).ToList();
            List<Vehicles> vehicles = db.Vehicles.Where(o => o.Service_Id == id).ToList();

            foreach (var item in members)
            {
                List<Member_Payments> memPays = db.Member_Payments.Where(o => o.Member_Id == item.Id).ToList();

                borrowList.Add(item.Borrow);
                paymentList.Add(memPays.Sum(o => o.Payment));
            }

            foreach (var item in vehicles)
            {
                List<Vehicle_Payments> vehcPays = db.Vehicle_Payments.Where(o => o.Vehicle_Id == item.Id).ToList();

                salaryList.Add(item.Salary);
                vehcPaymentList.Add(vehcPays.Sum(o => o.Payment));
            }


            ViewBag.TotalBorrows = borrowList.Sum();
            ViewBag.TotalPayments = paymentList.Sum();
            ViewBag.Borrows = borrowList;
            ViewBag.Payments = paymentList;

            ViewBag.TotalSalaries = salaryList.Sum();
            ViewBag.TotalVehcPayments = vehcPaymentList.Sum();
            ViewBag.Salaries = salaryList;
            ViewBag.VehcPayments = vehcPaymentList;


            ViewBag.Company = company;
            ViewBag.Members = members;
            ViewBag.Vehicles = vehicles;

            return View(ViewBag);
        }

        public ActionResult Add_New_Company(int? id)
        {
            ViewBag.Company = null;

            if (id != null && id > 0)
            {
                Companies comp = db.Companies.Single(o => o.Id == id);

                List<Town> towns = db.Town.Where(o => o.County_Id == comp.County_Id).OrderBy(o => o.Town_Name).ToList();
                ViewBag.Towns = towns;

                ViewBag.Company = comp;
            }
            List<Countries> countries = db.Countries.ToList();
            List<Counties> counties = db.Counties.ToList();


            ViewBag.Countries = countries;
            ViewBag.Counties = counties;


            return View(ViewBag);
        }

        public ActionResult Add_Or_Edit_Company(int id, string serviceName, int countryId, int countyId, int townId,
            string address,
            string phone, string fax, string email, string authName, string authPhone, string authEmail,
            string companyTitle, string taxOffice, string taxNumber)
        {
            try
            {
                string resultMessage;

                if (id <= 0)
                {
                    Companies comp = new Companies();

                    comp.Service_Name = serviceName;
                    comp.Country_Id = countryId;
                    comp.County_Id = countyId;
                    comp.Town_Id = townId;
                    comp.Address = address;
                    comp.Phone = phone;
                    comp.Fax = fax;
                    comp.Email = email;
                    comp.Authorized_Name = authName;
                    comp.Authorized_Phone = authPhone;
                    comp.Authorized_Email = authEmail;
                    comp.Company_Title = companyTitle;
                    comp.Tax_Office = taxOffice;
                    comp.Tax_Number = taxNumber;

                    db.Companies.Add(comp);
                    resultMessage = "Yeni Servis Barıyla Kaydedildi.";
                }
                else
                {
                    Companies comp = db.Companies.Single(o => o.Id == id);

                    comp.Service_Name = serviceName;
                    comp.Country_Id = countryId;
                    comp.County_Id = countyId;
                    comp.Town_Id = townId;
                    comp.Address = address;
                    comp.Phone = phone;
                    comp.Fax = fax;
                    comp.Email = email;
                    comp.Authorized_Name = authName;
                    comp.Authorized_Phone = authPhone;
                    comp.Authorized_Email = authEmail;
                    comp.Company_Title = companyTitle;
                    comp.Tax_Office = taxOffice;
                    comp.Tax_Number = taxNumber;

                    resultMessage = "Servis Bilgileri Başarıyla Güncellendi.";
                }

                db.SaveChanges();

                return Json(resultMessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Kayıt Esnasında Bir Hata Oluştu! " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete_Company(int id)
        {
            if (id <= 0)
            {
                return Json("Geçersiz Bir Servis Seçtiniz!", JsonRequestBehavior.AllowGet);
            }

            try
            {
                Companies company = db.Companies.Single(o => o.Id == id);

                db.Companies.Remove(company);
                db.SaveChanges();

                return Json("Servis Başarıyla Silindi.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Silme İşlemi Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }
    }
}