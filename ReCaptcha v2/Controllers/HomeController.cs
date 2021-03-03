using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using ReCaptchaV2.Models;

namespace ReCaptchaV2.Controllers
{
    public class HomeController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)] // For accepting get request. Or can be used for post by HttpVerbs.Post
        public ActionResult Index()
        {
            var model = new HomeModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("ValidateCaptcha")] // For using same name for get and post requests
        public ActionResult Index(HomeModel model)
        {
            try
            {
                var secretKey = ConfigurationManager.AppSettings["reCaptchaPrivateKey"];

                // Get the user submitted captcha response
                var response = Request["g-recaptcha-response"];

                if (!string.IsNullOrEmpty(response))
                {
                    TempData["Message"] = IsCaptchaResponseValid(response, secretKey)
                        ? "Captcha response is valid"
                        : "Error - Invalid captcha";
                }
                else
                {
                    TempData["Message"] = "Error - The captcha field is required";
                }
            }
            catch (Exception exc)
            {
                TempData["Message"] = string.Format("Error - {0}", exc.Message);
            }

            return PartialView("_Authenticate", model);
        }

        /// <summary>
        /// Check if user submitted captcha response is valid.
        /// </summary>
        /// <param name="response">The response object containing user submitted response.</param>
        /// <param name="secretKey">The respective secret key of pair for ReCaptcha.</param>
        /// <returns>Return true if valid captcha else false.</returns>
        private static bool IsCaptchaResponseValid(string response, string secretKey)
        {
            // Request to Google Server
            var url = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response);
            var req = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                // Google recaptcha Response
                bool valid;
                using (var wResponse = req.GetResponse())
                {
                    var stream = wResponse.GetResponseStream();
                    if (stream == null) return false;

                    using (var readStream = new StreamReader(stream))
                    {
                        var jsonResponse = readStream.ReadToEnd();

                        var js = new JavaScriptSerializer();
                        var data = js.Deserialize<JsonResponse>(jsonResponse); // Deserialize Json

                        valid = Convert.ToBoolean(data.Success);
                    }
                }

                return valid;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
    }

    /// <summary>
    /// Represent JsonResponse object.
    /// </summary>
    public class JsonResponse
    {
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> Errorcodes { get; set; }
    }
}