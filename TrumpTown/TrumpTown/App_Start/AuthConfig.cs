using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using TrumpTown.Models;

namespace TrumpTown
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //Live
            OAuthWebSecurity.RegisterFacebookClient(
                appId: "220737724717525",
                appSecret: "a96b9b42bb9b560bd785b218fd020318");

            //Dev
            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "100440283488071",
            //    appSecret: "157351e3f0f78b42180c265fd4cd9b1f");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
