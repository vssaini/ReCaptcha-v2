using System.Configuration;

namespace ReCaptchaV2.Models
{
    public class HomeModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// The public key for ReCaptcha
        /// </summary>
        public string ReCaptchaPublicKey => ConfigurationManager.AppSettings["recaptchaPublicKey"];

        /// <summary>
        /// Url for recaptcha challenge
        /// </summary>
        public string ReCaptchaChallengeUrl
        {
            get
            {
                var challengeUrl = string.Format("https://www.google.com/recaptcha/api/challenge?k={0}",ReCaptchaPublicKey);
                return challengeUrl;
            }
        }

        /// <summary>
        /// Url for recaptcha noscript
        /// </summary>
        public string ReCaptchaNoScriptUrl
        {
            get
            {
                var noScriptUrl = string.Format("https://www.google.com/recaptcha/api/noscript?k={0}", ReCaptchaPublicKey);
                return noScriptUrl;
            }
        }
    }
}