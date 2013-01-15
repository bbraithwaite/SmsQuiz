//using System.Linq;
//using System.Net.Http;
//using System.Web.Mvc;
//using BB.SmsQuiz.Infrastructure.Encryption;
//using BB.SmsQuiz.Model.Users;
//using BB.SmsQuiz.Infrastructure.Authentication;
//using BB.SmsQuiz.Web.Models;

//namespace BB.SmsQuiz.Web.Controllers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class AccountsController : Controller
//    {
//        private HttpClient _client;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="AccountsController" /> class.
//        /// </summary>
//        /// <param name="client">The client.</param>
//        public AccountsController(HttpClient client)
//        {
//            _client = client;
//        }

//        //
//        // GET: /Accounts/Logon
//        [HttpGet]
//        public ActionResult LogOn()
//        {
//            return View();
//        }

//        //
//        // POST: /Accounts/Logon
//        [HttpPost]
//        public ActionResult LogOn(string username, string password, bool remember)
//        {
//            User user = _userRepository.FindAll().SingleOrDefault(u => u.Username == username);

//            if (user != null)
//            {
//                if (user.Password.EncryptedValue.SequenceEqual(_encryptionService.Encrypt(password)))
//                {
//                    _formsAuthentication.SetAuthenticationToken(username);
//                    return RedirectToAction("Index", "Home");
//                }
//            }

//            return View();
//        }

//        //
//        // GET: /Accounts/Create
//        [HttpGet]
//        public ActionResult Create()
//        {
//            return View();
//        }

//        //
//        // POST: /Accounts/Create
//        [HttpPost]
//        public ActionResult Create(string username, string password)
//        {
//            var user = new User()
//                {
//                    Username = username,
//                    Password = EncryptedString.Create(password, _encryptionService)
//                };

//            if (user.IsValid)
//            {
//                _userRepository.Add(user);
//                return RedirectToAction("LogOn");
//            }

//            var model = new BaseViewModel()
//                {
//                    ValidationErrors = user.ValidationErrors.Items
//                };

//            return View(model);
//        }

//        //
//        // GET: /Accounts/LogOut
//        public ActionResult LogOut()
//        {
//            _formsAuthentication.SignOut();
//            return RedirectToAction("LogOn");
//        }
//    }
//}
