using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using GuvenTur_CRM.Models;

namespace GuvenTur_CRM.Controllers
{
    public class VehiclesController : Controller
    {
        GuvenTurDBModel db = new GuvenTurDBModel();
        // GET: Vehicles
        public ActionResult Vehicles()
        {
            List<int> salaryList = new List<int>();
            List<int> paymentList = new List<int>();
            List<string> serviceNames = new List<string>();
            List<int> memberCount = new List<int>();

            int totalVehicles = 0;
            int totalSalaries = 0;
            int totalPayments = 0;
            int total = 0;


            List<Vehicles> vehicles = db.Vehicles.OrderBy(o => o.Id).ToList();

            foreach (var item in vehicles)
            {
                List<Vehicle_Payments> vehiclePayments = db.Vehicle_Payments.Where(o => o.Vehicle_Id == item.Id).ToList();

                salaryList.Add(item.Salary);
                paymentList.Add(vehiclePayments.Sum(o => o.Payment));

                List<Members> members = db.Members.Where(o => o.Vehicle_Id == item.Id).ToList();
                Companies serviceNamesList = db.Companies.Single(o => o.Id == item.Service_Id);

                memberCount.Add(members.Count);

                serviceNames.Add(serviceNamesList.Service_Name);

                totalVehicles++;

            }

            totalSalaries = salaryList.Sum();
            totalPayments = paymentList.Sum();

            ViewBag.Vehicles = vehicles;
            ViewBag.Salaries = salaryList;
            ViewBag.Payments = paymentList;
            ViewBag.TotalVehicles = totalVehicles;
            ViewBag.TotalSalaries = totalSalaries;
            ViewBag.TotalPayments = totalPayments;
            ViewBag.ServiceNames = serviceNames;
            ViewBag.MemberCounts = memberCount;

            return View(ViewBag);
        }

        public ActionResult Vehicle_Detail(int id)
        {
            List<int> borrowList = new List<int>();
            List<int> paymentList = new List<int>();
            List<string> payDates = new List<string>();
            List<string> payTimes = new List<string>();

            int totalBorrows = 0;
            int totalPayments = 0;

            Vehicles vehicles = db.Vehicles.Single(o => o.Id == id);

            List<Vehicle_Payments> vehiclePayments = db.Vehicle_Payments.Where(o => o.Vehicle_Id == id).ToList();

            List<Members> members = db.Members.Where(o => o.Vehicle_Id == id).ToList();

            Companies serviceName = db.Companies.Single(o => o.Id == vehicles.Service_Id);

            List<Vehicle_Maintenances> maintenances = db.Vehicle_Maintenances.Where(o => o.Vehicle_Id == id).ToList();

            Vehicle_Files fileInfo = db.Vehicle_Files.Single(o => o.Vehicle_Id == id);


            foreach (var item in members)
            {
                var payment = db.Member_Payments.Where(o => o.Member_Id == item.Id).ToList();
                int payments = payment.Sum(o => o.Payment);

                borrowList.Add(item.Borrow);
                paymentList.Add(payments);
            }

            foreach (var item in vehiclePayments)
            {
                var payHistory = db.Vehicle_Payments.Single(o => o.Id == item.Id);

                string payDate = Convert.ToDateTime(payHistory.Payment_Date).ToShortDateString();
                string payTime = Convert.ToDateTime(payHistory.Payment_Date).ToShortTimeString();

                payDates.Add(payDate);
                payTimes.Add(payTime);
            }

            List<string> mainDates = maintenances.Select(item => db.Vehicle_Maintenances.Single(o => o.Id == item.Id)).Select(mainHistory => Convert.ToDateTime(mainHistory.Maintenance_Date).ToShortDateString()).ToList();

            totalBorrows = borrowList.Sum();
            totalPayments = paymentList.Sum();

            ViewBag.Vehicles = vehicles;
            ViewBag.ServiceName = serviceName.Service_Name;
            ViewBag.Members = members;
            ViewBag.Maintenances = maintenances;
            ViewBag.PaymentInfo = vehiclePayments;
            ViewBag.FileInfo = fileInfo;
            ViewBag.TrafficInsurance = Convert.ToDateTime(fileInfo.Traffic_Insurance_Date).ToShortDateString();
            ViewBag.ExaminDate = Convert.ToDateTime(fileInfo.Examination_Date).ToShortDateString();
            ViewBag.Borrows = borrowList;
            ViewBag.Payments = paymentList;
            ViewBag.TotalBorrows = totalBorrows;
            ViewBag.TotalPayments = totalPayments;
            ViewBag.PayDate = payDates;
            ViewBag.PayTime = payTimes;
            ViewBag.MainDate = mainDates;
            ViewBag.TotalVehcPayments = vehiclePayments.Sum(o => o.Payment);
            ViewBag.TotalMainPayment = maintenances.Sum(o => o.Maintenance_Payment);

            return View(ViewBag);
        }

        public ActionResult Add_New_Vehicle(int? id)
        {
            if (id != null && id > 0)
            {
                Vehicles vehicle = db.Vehicles.Single(o => o.Id == id);
                Vehicle_Files vhcFiles = db.Vehicle_Files.Single(o => o.Vehicle_Id == id);
                List<Town> towns =
                    db.Town.Where(o => o.County_Id == vehicle.County_Id).OrderBy(o => o.Town_Name).ToList();

                ViewBag.Vehicle = vehicle;
                ViewBag.VehicleFiles = vhcFiles;
                ViewBag.Towns = towns;
            }

            List<Countries> countries = db.Countries.ToList();
            List<Counties> counties = db.Counties.ToList();

            List<Companies> companies = db.Companies.ToList();


            ViewBag.Countries = countries;
            ViewBag.Counties = counties;
            ViewBag.Companies = companies;

            return View(ViewBag);
        }

        public ActionResult Add_Maintenance()
        {
            List<Companies> companyList = db.Companies.OrderBy(o => o.Service_Name).ToList();
            List<Maintenance_Types> MaintenanceTypeList =
                db.Maintenance_Types.OrderBy(o => o.Maintenance_Type).ToList();

            ViewBag.Services = companyList;
            ViewBag.Maintenance_Types = MaintenanceTypeList;
            return View(ViewBag);
        }

        public ActionResult Vehicle_Payments()
        {
            List<Companies> companyList = db.Companies.OrderBy(o => o.Service_Name).ToList();
            List<Payment_Types> paymentTypeList = db.Payment_Types.OrderBy(o => o.Payment_Type).ToList();

            ViewBag.Services = companyList;
            ViewBag.Payment_Types = paymentTypeList;
            return View();
        }      

        public ActionResult Add_Or_Edit_Vehicle(int id, string driverTc, string driverName, int countryId, int countyId, int townId, string address, string phone, string gsm, string email, int salary, int vehicleNo, string vehiclePlate, int serviceId, decimal firstKmMaintnc, decimal nextKmMaintnc, DateTime trrafficInsrncDate, DateTime examinationDate, string licenseSample, string trafficInsrncSample, string examination, string driverLicense, string registryRecord, string gbt, string healthReport)
        {
            try
            {
                //HttpPostedFile[] sampleFiles = new HttpPostedFile[7] {licenseSample, trafficInsrncSample, examination, driverLicense, registryRecord, gbt, healthReport };

                //string[] fileFolder = new string[7] { "driver_license", "driver_license", "driver_license", "driver_license", "driver_license", "driver_license", "driver_license" };

                //FileUpload file = new FileUpload();

                //for (int i = 0; i < sampleFiles.Length; i++)
                //{
                //    var fileName = Path.GetFileName(sampleFiles[i].FileName);

                //    file.SaveAs(Server.MapPath("/vehicle_files/" + fileFolder[i] + "/" + fileName));
                //}

                string resultMessage;

                if (id <= 0)
                {
                    Vehicles vehicle = new Vehicles();

                    vehicle.Driver_TC = driverTc;
                    vehicle.Driver_Name = driverName;
                    vehicle.Country_Id = countryId;
                    vehicle.County_Id = countyId;
                    vehicle.Town_Id = townId;
                    vehicle.Address = address;
                    vehicle.Phone = phone;
                    vehicle.Gsm = gsm;
                    vehicle.Email = email;
                    vehicle.Salary = salary;
                    vehicle.Vehicle_No = vehicleNo;
                    vehicle.Vehicle_Plate = vehiclePlate;
                    vehicle.Service_Id = serviceId;

                    db.Vehicles.Add(vehicle);

                    int vehicleId = vehicle.Id;

                    Vehicle_Files files = new Vehicle_Files();

                    files.Vehicle_Id = vehicleId;
                    files.First_Km_Maintenance = firstKmMaintnc;
                    files.Next_Km_Maintenance = nextKmMaintnc;
                    files.Traffic_Insurance_Date = trrafficInsrncDate;
                    files.Examination_Date = examinationDate;
                    files.License_Sample = licenseSample.ToString();
                    files.Traffic_Insurance_Sample = trafficInsrncSample.ToString();
                    files.Examination = examination.ToString();
                    files.Driver_License = driverLicense.ToString();
                    files.Registry_Record = registryRecord.ToString();
                    files.Gbt = gbt.ToString();
                    files.Health_Report = healthReport.ToString();

                    db.Vehicle_Files.Add(files);

                    resultMessage = "Yeni Araç Başarıyla Kaydedildi.";
                }
                else
                {
                    Vehicles vehicle = db.Vehicles.Single(o => o.Id == id);

                    vehicle.Driver_TC = driverTc;
                    vehicle.Driver_Name = driverName;
                    vehicle.Country_Id = countryId;
                    vehicle.County_Id = countyId;
                    vehicle.Town_Id = townId;
                    vehicle.Address = address;
                    vehicle.Phone = phone;
                    vehicle.Gsm = gsm;
                    vehicle.Email = email;
                    vehicle.Salary = salary;
                    vehicle.Vehicle_No = vehicleNo;
                    vehicle.Vehicle_Plate = vehiclePlate;
                    vehicle.Service_Id = serviceId;

                    Vehicle_Files files = db.Vehicle_Files.Single(o => o.Vehicle_Id == id);

                    files.First_Km_Maintenance = firstKmMaintnc;
                    files.Next_Km_Maintenance = nextKmMaintnc;
                    files.Traffic_Insurance_Date = trrafficInsrncDate;
                    files.Examination_Date = examinationDate;
                    files.License_Sample = licenseSample.ToString();
                    files.Traffic_Insurance_Sample = trafficInsrncSample.ToString();
                    files.Examination = examination.ToString();
                    files.Driver_License = driverLicense.ToString();
                    files.Registry_Record = registryRecord.ToString();
                    files.Gbt = gbt.ToString();
                    files.Health_Report = healthReport.ToString();

                    resultMessage = "Araç Bilgileri Başarıyla Güncellebdi";
                }

                db.SaveChanges();

                return Json(resultMessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Kayıt Esnasında Bir Hata Oluştu! " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete_Vehicle(int id)
        {
            if (id <= 0)
            {
                return Json("Geçersiz Bir Araç Seçtiniz!", JsonRequestBehavior.AllowGet);
            }

            try
            {
                Vehicles vehicle = db.Vehicles.Single(o => o.Id == id);

                db.Vehicles.Remove(vehicle);
                db.SaveChanges();

                return Json("Araç Başarıyla Silindi.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Silme İşlemi Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Add_Vehilce_Maintenance(int vehicleId, int serviceId, int vehicleKm, int maintenanceTypeId, DateTime maintenanceDate, int maintenancePayment)
        {
            try
            {
                Vehicle_Maintenances vhcMaintnc = new Vehicle_Maintenances();

                vhcMaintnc.Vehicle_Id = vehicleId;
                vhcMaintnc.Service_Id = serviceId;
                vhcMaintnc.Vehicle_Km = vehicleKm;
                vhcMaintnc.Maintenance_Type_Id = maintenanceTypeId;
                vhcMaintnc.Maintenance_Date = maintenanceDate;
                vhcMaintnc.Maintenance_Payment = maintenancePayment;

                db.Vehicle_Maintenances.Add(vhcMaintnc);
                db.SaveChanges();

                return Json("Bakım Başarıyla Kaydedildi", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Kayıt Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Do_Vehicle_Payment(int vehicleId, int payment, int paymentTypeId, DateTime paymentDate)
        {
            try
            {
                Vehicle_Payments vhcPayment = new Vehicle_Payments();

                vhcPayment.Vehicle_Id = vehicleId;
                vhcPayment.Payment = payment;
                vhcPayment.Payment_Type_Id = paymentTypeId;
                vhcPayment.Payment_Date = paymentDate;

                db.Vehicle_Payments.Add(vhcPayment);
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