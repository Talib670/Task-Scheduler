using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Money_Finder.Models;
using TaskSchedular.Models;

namespace wayPointLatest.Controllers
{
    public class UserController : Controller
    {
        AppDBContext d = new AppDBContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult submitnewuser(string userid, List<string> access ,string roll, string skype, string contact, string password, string gmail, string fname, string lname, List<IFormFile> files)
        {
            string AccessCampaign = null;
            string AccessSource = null;
            string AccessProvider = null;
            if (roll== "Admin")
            {
               AccessCampaign = "Campaign";
                AccessSource = "DataSource";
                 AccessProvider = "Provider";
            }
            if (roll == "Visitor")
            {
                AccessCampaign = "Campaign";
                AccessSource = "DataSource";
                AccessProvider = "Provider";
            }
            else
            { 

            foreach (var item in access)
            {
                Console.WriteLine(item);
                if (item == "Campaign")
                {
                    AccessCampaign = item;
                }
                if (item == "DataSource")
                {
                    AccessSource = item;

                }
                if (item == "Provider")
                {
                    AccessProvider = item;

                }
               
            }
            }
            if (files != null && files.Count != 0)
            {


                //    for (int i = 0; i <= files.Count; i++)
                //{ 
                foreach (var file in files)
                {
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(file.FileName);

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);

                        // Combines two strings into a path.
                        var filepath =
           new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedImages")).Root + $@"\{newFileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();

                        }
                        if (userid == "" || userid == string.Empty || userid == null)
                        {
                            AdminLogin s = new AdminLogin();
                            s.Image = newFileName;
                            s.UserName = fname + " " + lname;
                            s.SystemRoll = roll;
                            s.Contact = contact;
                            s.FirstName = fname;
                            s.LastName = lname;
                            s.Gmail = gmail;
                            s.SkypeAddress = skype;
                            s.Status = "Approved";
                            s.Password = password;
                            s.AccessCampaign = AccessCampaign;
                            s.AccessSource = AccessSource;
                            s.AccessProvider = AccessProvider;
                            d.AdminLogin.Add(s);
                            d.SaveChanges();
                        }
                        else
                        {
                            var data = d.AdminLogin.Find(Convert.ToInt32(userid));
                            data.Image = newFileName;
                            data.SkypeAddress = skype;
                            data.UserName = fname + " " + lname;
                            data.SystemRoll = roll;
                            data.FirstName = fname;
                            data.LastName = lname;
                            data.Gmail = gmail;
                            data.Password = password;
                            data.AccessCampaign = AccessCampaign;
                            data.AccessSource = AccessSource;
                            data.AccessProvider = AccessProvider;
                            d.Entry(data).State = EntityState.Modified;
                            d.SaveChanges();
                        }



                    }
                }
            }



            else
            {


                //    for (int i = 0; i <= files.Count; i++)
                //{ 

                if (userid == "" || userid == string.Empty || userid == null)
                {
                    AdminLogin s = new AdminLogin();

                    s.UserName = fname + " " + lname;
                    s.FirstName = fname;
                    s.LastName = lname;
                    s.SystemRoll = roll;
                    s.Contact = contact;
                    s.Gmail = gmail;
                    s.SkypeAddress = skype;
                    s.Status = "Approved";
                    s.Password = password;
                    s.AccessCampaign = AccessCampaign;
                    s.AccessSource = AccessSource;
                    s.AccessProvider = AccessProvider;
                    d.AdminLogin.Add(s);
                    d.SaveChanges();
                }
                else
                {
                    var data = d.AdminLogin.Find(Convert.ToInt32(userid));

                    data.SkypeAddress = skype;
                    data.UserName = fname + " " + lname;
                    data.FirstName = fname;
                    data.LastName = lname;
                    data.SystemRoll = roll;
                    data.Gmail = gmail;
                    data.Password = password;
                    data.AccessCampaign = AccessCampaign;
                    data.AccessSource = AccessSource;
                    data.AccessProvider = AccessProvider;
                    d.Entry(data).State = EntityState.Modified;
                    d.SaveChanges();
                }

            }
            return RedirectToAction("SystemUser", "Home");
        }


    }
}
