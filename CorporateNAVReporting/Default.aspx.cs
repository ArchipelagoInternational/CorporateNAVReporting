using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Arch.Service.AuthSdk;

namespace CorporateNAVReporting
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //HttpCookie aCookie = Request.Cookies["arch_token"];
                //if (aCookie != null)
                //{
                //    object userSettings = aCookie.Value;
                //    var data = AuthClient.GetSessionFromService($"Bearer " + userSettings);

                //}
                //else
                //{
                //    HttpCookie cookie = new HttpCookie("arch_token");
                //    cookie.Expires = DateTime.Now.AddYears(1);
                //    Response.Cookies.Add(cookie);
                //    //Cookie not set.
                //    object userSettings = aCookie.Value;
                //    var data = AuthClient.GetSessionFromService($"Bearer " + userSettings);
                //}
            }


        }
    }
}