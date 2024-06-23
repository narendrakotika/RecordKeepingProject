using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RecordKeepingProject.Models;

namespace RecordKeeper.Controllers
{
    public class AccountController : Controller
    {
        // Hardcoded user for simplicity; in a real application, use a user store
        private readonly User _user = new User { Username = "admin", Password = "password" };

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid && model.Username == _user.Username && model.Password == _user.Password)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
