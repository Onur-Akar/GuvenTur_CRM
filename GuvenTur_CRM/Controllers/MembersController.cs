using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GuvenTur_CRM.Models;

namespace GuvenTur_CRM.Controllers
{
    public class MembersController : Controller
    {
        GuvenTurDBModel db = new GuvenTurDBModel();
        // GET: Members
        public ActionResult Members()
        {
            List<int> borrowList = new List<int>();
            List<int> paymentList = new List<int>();

            List<Members> members = db.Members.OrderBy(o => o.Id).ToList();

            foreach (var item in members)
            {
                var payments = db.Member_Payments.Where(o => o.Member_Id == item.Id).ToList();

                borrowList.Add(item.Borrow);
                paymentList.Add(payments.Sum(o => o.Payment));
            }

            int totalBorrow = borrowList.Sum();
            int totalPayment = paymentList.Sum();


            ViewBag.Members = members;
            ViewBag.Borrows = borrowList;
            ViewBag.Payments = paymentList;
            ViewBag.TotalBorrows = totalBorrow;
            ViewBag.TotalPayments = totalPayment;

            return View(members);
        }

        public ActionResult Member_Detail(int id)
        {
            List<string> payDates = new List<string>();
            List<string> payTimes = new List<string>();

            Members member = db.Members.Single(o => o.Id == id);
            Companies serviceInfo = db.Companies.Single(o => o.Id == member.Service_Id);
            Vehicles vehicleInfo = db.Vehicles.Single(o => o.Id == member.Vehicle_Id);
            Members_Pay_Info memPayInfo = db.Members_Pay_Info.Single(o => o.Member_Id == id);
            List<Member_Payments> memberPayments = db.Member_Payments.Where(o => o.Member_Id == id).ToList();

            foreach (var item in memberPayments)
            {
                var payHistory = db.Member_Payments.Single(o => o.Id == item.Id);

                string payDate = Convert.ToDateTime(payHistory.Payment_Date).ToShortDateString();
                string payTime = Convert.ToDateTime(payHistory.Payment_Date).ToShortTimeString();

                payDates.Add(payDate);
                payTimes.Add(payTime);
            }

            ViewBag.Member = member;
            ViewBag.ServiceName = serviceInfo.Service_Name;
            ViewBag.VehicleNo = vehicleInfo.Vehicle_No;
            ViewBag.MemberPayInfo = memPayInfo;
            ViewBag.MemberPayments = memberPayments;
            ViewBag.PayDate = payDates;
            ViewBag.PayTime = payTimes;
            ViewBag.TotalPayments = memberPayments.Sum(o => o.Payment);

            return View(ViewBag);
        }

        public ActionResult Add_New_Member(int? id)
        {
            if (id != null && id > 0)
            {
                Members member = db.Members.Single(o => o.Id == id);
                List<Vehicles> vehicles = db.Vehicles.Where(o => o.Service_Id == member.Service_Id).OrderBy(o => o.Vehicle_No).ToList();
                List<Town> towns =
                    db.Town.Where(o => o.County_Id == member.County_Id).OrderBy(o => o.Town_Name).ToList();
                Members_Pay_Info memPayInfo = db.Members_Pay_Info.Single(o => o.Member_Id == id);

                ViewBag.Member = member;
                ViewBag.Member_Pay_Info = memPayInfo;
                ViewBag.Vehicles = vehicles;
                ViewBag.Towns = towns;
            }
            List<Member_Groups> memGroupList = db.Member_Groups.OrderBy(o => o.Member_Group_Name).ToList();
            List<Companies> companyList = db.Companies.OrderBy(o => o.Service_Name).ToList();
            List<Countries> tr = db.Countries.ToList();
            List<Counties> countiesList = db.Counties.OrderBy(o => o.County_Name).ToList();

            ViewBag.Member_Groups = memGroupList;
            ViewBag.Services = companyList;
            ViewBag.Country = tr;
            ViewBag.Counties = countiesList;

            return View(ViewBag);
        }

        public ActionResult Member_Payments()
        {
            List<Member_Groups> memberGroupList = db.Member_Groups.OrderBy(o => o.Member_Group_Name).ToList();
            List<Payment_Types> paymentTypeList = db.Payment_Types.OrderBy(o => o.Payment_Type).ToList();

            ViewBag.Member_Groups = memberGroupList;
            ViewBag.Payment_Types = paymentTypeList;
            return View(ViewBag);
        }

        public ActionResult Add_Or_Edit_Member(int id, int memberGroupId, string memberName, int serviceId, int vehicleId, int countryId, int countyId, int townId, string address, string phone, string gsm, string email, int borrow, decimal km, string memberTitle, string memberTc, string bankName, string cardOwner, string cardNumber, string lastExpDate, string securtyNo)
        {
            try
            {
                string resultMessage;

                if (id <= 0)
                {
                    Members member = new Members();

                    member.Member_Group_Id = memberGroupId;
                    member.Member_Name = memberName;
                    member.Service_Id = serviceId;
                    member.Vehicle_Id = vehicleId;
                    member.Country_Id = countryId;
                    member.County_Id = countyId;
                    member.Town_Id = townId;
                    member.Address = address;
                    member.Phone = phone;
                    member.Gsm = gsm;
                    member.Email = email;
                    member.Borrow = borrow;
                    member.Km = km;
                    member.Member_Title = memberTitle;
                    member.Member_TC = memberTc;

                    db.Members.Add(member);

                    int memberId = member.Id;

                    Members_Pay_Info memPayInfo = new Members_Pay_Info();

                    memPayInfo.Member_Id = memberId;
                    memPayInfo.Bank_Name = bankName;
                    memPayInfo.Card_Owner = cardOwner;
                    memPayInfo.Card_Number = cardNumber;
                    memPayInfo.Last_Exp_Date = lastExpDate;
                    memPayInfo.Securty_No = securtyNo;

                    db.Members_Pay_Info.Add(memPayInfo);

                    resultMessage = "Üye Başarıyla Kaydedildi.";
                }
                else
                {
                    Members member = db.Members.Single(o => o.Id == id);

                    member.Member_Group_Id = memberGroupId;
                    member.Member_Name = memberName;
                    member.Service_Id = serviceId;
                    member.Vehicle_Id = vehicleId;
                    member.Country_Id = countryId;
                    member.County_Id = countyId;
                    member.Town_Id = townId;
                    member.Address = address;
                    member.Phone = phone;
                    member.Gsm = gsm;
                    member.Email = email;
                    member.Borrow = borrow;
                    member.Km = km;
                    member.Member_Title = memberTitle;
                    member.Member_TC = memberTc;

                    Members_Pay_Info memPayInfo = db.Members_Pay_Info.Single(o => o.Member_Id == id);

                    memPayInfo.Bank_Name = bankName;
                    memPayInfo.Card_Owner = cardOwner;
                    memPayInfo.Card_Number = cardNumber;
                    memPayInfo.Last_Exp_Date = lastExpDate;
                    memPayInfo.Securty_No = securtyNo;

                    resultMessage = "Üye Bilgileri Başarıyla Güncellendi.";
                }

                db.SaveChanges();

                return Json(resultMessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Kayıt Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete_Member(int id)
        {
            if (id <= 0)
            {
                return Json("Geçersiz Bir Üye Seçtiniz!", JsonRequestBehavior.AllowGet);
            }

            try
            {
                Members member = db.Members.Single(o => o.Id == id);

                db.Members.Remove(member);
                db.SaveChanges();

                return Json("Üye Başarıyla Sİlindi.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Silme İşlemi Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Add_Member_Payment(int memberId, int payment, int paymentTypeId, DateTime paymentDate)
        {
            try
            {
                Member_Payments memPayment = new Member_Payments();

                memPayment.Member_Id = memberId;
                memPayment.Payment = payment;
                memPayment.Payment_Type_Id = paymentTypeId;
                memPayment.Payment_Date = paymentDate;

                db.Member_Payments.Add(memPayment);
                db.SaveChanges();

                return Json("Ödeme Başarıyla Kaydedildi.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Kayıt Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }
    }
}