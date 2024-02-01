/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20220508 003
       Author .  .  .  .  .  .  .  .  Delaney

                          .___,
                       ___('v')___
                       `"-\._./-"'
                           ^ ^

 *********************************************************/
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SilkFlo.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        /// <summary>
        /// Usage: User.GetClaimValue("someClaimType")
        /// </summary>
        /// <param name="currentPrincipal"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetClaimValue(
            this IPrincipal currentPrincipal, 
            string key)
        {
            if (currentPrincipal.Identity is not ClaimsIdentity identity)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }


        /// <summary>
        /// Usage: User.HasClaim("someClaimType")
        /// </summary>
        /// <param name="currentPrincipal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static bool HasClaim(
            this IPrincipal currentPrincipal,
            string claimType)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            var claim = identity?.Claims.FirstOrDefault(x => x.Type == claimType);

            return claim != null;
        }


        /// <summary>
        /// User.SaveClaim("key1", "value1");
        /// </summary>
        /// <param name="currentPrincipal"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        public static void SaveClaim(
            this IPrincipal currentPrincipal,
            string key,
            string value,
            string valueType = "")
        {
            if (currentPrincipal.Identity is not ClaimsIdentity identity)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(string.IsNullOrWhiteSpace(valueType)
                ? new Claim(key, value)
                : new Claim(key, value, valueType));
        }
    }
}