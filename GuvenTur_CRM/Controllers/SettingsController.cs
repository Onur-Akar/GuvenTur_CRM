using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuvenTur_CRM.Models;

namespace GuvenTur_CRM.Controllers
{
    public class SettingsController : Controller
    {
        GuvenTurDBModel db = new GuvenTurDBModel();
        // GET: Settings
        public ActionResult Users()
        {
            List<Users> users = db.Users.OrderBy(o => o.Id).ToList();
            return View(users);
        }

        public ActionResult User_Profile(int id)
        {
            Users user = db.Users.Single(o => o.Id == id);
            Level_Settings levelSettings = db.Level_Settings.Single(o => o.Level_Id == user.Level_Id);
            User_Special_Settings userSettings = db.User_Special_Settings.SingleOrDefault(o => o.User_Id == id);

            ViewBag.User = user;

            if (userSettings != null)
            {
                ViewBag.UserSettings = userSettings;
            }
            else
            {
                ViewBag.UserSettings = levelSettings;
            }

            return View(ViewBag);
        }

        public ActionResult Add_New_User(int? id)
        {
            if (id != null && id > 0)
            {
                Users user = db.Users.Single(o => o.Id == id);

                ViewBag.User = user;
            }

            List<Levels> levelList = db.Levels.OrderBy(o => o.Level_Name).ToList();

            ViewBag.Levels = levelList;
            return View(ViewBag);
        }

        public ActionResult User_Levels()
        {
            List<int> userCountOfLevel = new List<int>();

            List<Levels> levels = db.Levels.OrderBy(o => o.Level_Name).ToList();

            foreach (var item in levels)
            {
                int levelUsercount = db.Users.Count(O => O.Level_Id == item.Id);
                userCountOfLevel.Add(levelUsercount);
            }
            ViewBag.Count = userCountOfLevel;
            ViewBag.Levels = levels;

            return View(ViewBag);
        }

        public ActionResult Add_Or_Edit_User(int id, int levelId, string userTitle, string userFirstName, string userLastName, string password, int phoneLine, string gsm, string email, string address, string userTc)
        {
            try
            {
                string resultMessage;

                if (id <= 0)
                {
                    Users user = new Users();

                    user.User_Title = userTitle;
                    user.User_First_Name = userFirstName;
                    user.User_Last_Name = userLastName;
                    user.Password = password;
                    user.Phone_Line = phoneLine;
                    user.Gsm = gsm;
                    user.Email = email;
                    user.Address = address;
                    user.User_TC = userTc;
                    user.Level_Id = levelId;

                    db.Users.Add(user);

                    resultMessage = "Yeni Üye Başarıyla Kaydedildi.";
                }
                else
                {
                    Users user = db.Users.Single(o => o.Id == id);

                    user.User_Title = userTitle;
                    user.User_First_Name = userFirstName;
                    user.User_Last_Name = userLastName;
                    user.Phone_Line = phoneLine;
                    user.Gsm = gsm;
                    user.Email = email;
                    user.Address = address;
                    user.User_TC = userTc;
                    user.Level_Id = levelId;

                    resultMessage = "Üye Bilgileri Başarıyla Güncellebdi";
                }

                db.SaveChanges();

                return Json(resultMessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Kayıt Esnasında Bir Hata Oluştu! " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete_User(int id)
        {
            if (id <= 0)
            {
                return Json("Geçersiz Bir Kullanıcı Seçtiniz!", JsonRequestBehavior.AllowGet);
            }
            try
            {
                Users user = db.Users.Single(o => o.Id == id);

                db.Users.Remove(user);
                db.SaveChanges();

                return Json("Kullanıcı Başarıyla Silindi.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Silme İşlemi Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Get_LevelSettings_By_Id(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;

                Level_Settings levelSettings = db.Level_Settings.Single(o => o.Level_Id == id);

                return Json(levelSettings, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Seviye Ayarları Hazırlanırken Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Add_New_Level(string levelName)
        {
            try
            {
                Levels level = new Levels();

                Level_Settings levelSetting = new Level_Settings();

                level.Level_Name = levelName;

                db.Levels.Add(level);

                int levelId = level.Id;

                levelSetting.Level_Id = levelId;
                levelSetting.Add_Company = false;
                levelSetting.Edit_Company = false;
                levelSetting.Delete_Company = false;
                levelSetting.See_Comp_Accounts = false;
                levelSetting.Add_Vehicle = false;
                levelSetting.Edit_Vehicle = false;
                levelSetting.Delete_Vehicle = false;
                levelSetting.See_Vehicle_Accounts = false;
                levelSetting.Add_Member = false;
                levelSetting.Edit_Member = false;
                levelSetting.Delete_Member = false;
                levelSetting.See_Member_Accounts = false;

                db.Level_Settings.Add(levelSetting);

                db.SaveChanges();

                return Json(level, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Yeni Yetki Kaydı Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit_LevelSettings(int choose, int levelId, bool addLevel, bool editLevel, bool deleteLevel, bool seeAccounts)
        {
            Level_Settings levelSetting = db.Level_Settings.Single(o => o.Level_Id == levelId);

            string resultMessage = "";
            try
            {
                switch (choose)
                {
                    case 1:
                        levelSetting.Add_Company = addLevel;
                        levelSetting.Edit_Company = editLevel;
                        levelSetting.Delete_Company = deleteLevel;
                        levelSetting.See_Comp_Accounts = seeAccounts;

                        db.SaveChanges();

                        resultMessage = "Firma Yetki Seviyeleri Başarıyla Kaydedili";
                        break;
                    case 2:
                        levelSetting.Add_Vehicle = addLevel;
                        levelSetting.Edit_Vehicle = editLevel;
                        levelSetting.Delete_Vehicle = deleteLevel;
                        levelSetting.See_Vehicle_Accounts = seeAccounts;

                        db.SaveChanges();

                        resultMessage = "Araç Yetki Seviyeleri Başarıyla Kaydedili";
                        break;
                    case 3:
                        levelSetting.Add_Member = addLevel;
                        levelSetting.Edit_Member = editLevel;
                        levelSetting.Delete_Member = deleteLevel;
                        levelSetting.See_Member_Accounts = seeAccounts;

                        db.SaveChanges();

                        resultMessage = "Üye Yetki Seviyeleri Başarıyla Kaydedili";
                        break;
                    default:
                        break;
                }

                return Json(resultMessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Yetki Kaydı Esnasında Bir Hata Oluştu! " + ex, JsonRequestBehavior.AllowGet);
            }
        }
    }
}