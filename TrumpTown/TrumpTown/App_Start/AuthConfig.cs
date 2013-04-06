using System;
using System.Collections.Generic;
using System.Configuration;
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

            //LinkedIn is currently broken
            //OAuthWebSecurity.RegisterLinkedInClient(
            //consumerKey: ConfigurationManager.AppSettings["LinkedInClient.ConsumerKey"],
            //consumerSecret: ConfigurationManager.AppSettings["LinkedInClient.ConsumerSecret"]);

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: ConfigurationManager.AppSettings["MicrosoftClient.ClientId"],
            //    clientSecret: ConfigurationManager.AppSettings["MicrosoftClient.ClientSecret"]);

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: ConfigurationManager.AppSettings["TwitterClient.ConsumerKey"],
                consumerSecret: ConfigurationManager.AppSettings["TwitterClient.ConsumerSecret"]);

            OAuthWebSecurity.RegisterFacebookClient(
                appId: ConfigurationManager.AppSettings["FacebookClient.AppId"],
                appSecret: ConfigurationManager.AppSettings["FacebookClient.AppSecret"]);

            OAuthWebSecurity.RegisterGoogleClient();
            OAuthWebSecurity.RegisterYahooClient();
        }
    }
}
