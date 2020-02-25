using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMP;
using System.Collections;
using CBQ;
using MySql.Data.MySqlClient;

namespace MerchantDashboard
{

    public partial class LoginResponsive : System.Web.UI.Page
    {
        private String strUser;
        private String strEmail;
        String c_strException = "";

        //WebCommandProcessor c_WebCommandProcessor = null;
        ManageStore c_ManageStore = new ManageStore(CyberGlobals.MANAGESTORE_LOGFILES);
        ArrayList c_Parents = new ArrayList();
        /*protected void Page_LoadComplete(object sender, EventArgs e)
        {
            Response.Write(hashValue.Value);
            Response.End();
        }*/

        protected void Page_Load(object sender, EventArgs e)
        {
            global.WriteLog("start loginnew");
            //Clean up if they are trying to put wwww.
            bool redirect = false;
            string str_url = Request.Url.ToString();
            str_url = str_url.ToLower();

            // Start the Load Time timer to evalualte latency in loading the page <CarlB 04-19-2018>
#if DEBUG 
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            watch.Start();
#endif

            // Cashier Login Store ID, redirect to Cashier Login <CarlB 08-16-2017>
            if (Request.QueryString["cslogin"] != null)
            {
                loginPage.Style.Value = "display:none";
                cashierlogin.Style.Value = "display:block";
                ClientScript.RegisterStartupScript(typeof(Page), "CashierLogin", "LoadCashierLoginPopup('" + Request.QueryString["cslogin"].ToString() + "');", true);

                return;
            }

            // Grab the Clover account vars so we can auto-login the Clover user
            if (Request.QueryString["username"] != null)
            {
                strUser = Request.QueryString["username"];
                strUser.Replace(' ', '_');
                strUser.Replace('\'', '_');
                Session["CloverUser"] = strUser;
            }
            else
                strUser = "";
            if (Request.QueryString["email"] != null)
            {
                strEmail = Request.QueryString["email"];
                strEmail = strEmail.Replace("\"", "");
                Session["CloverEmail"] = strEmail;
            }
            else
                strEmail = "";

            /*
            if (str_url.Contains("www."))
            {
                //Response.Redirect(str_url.Replace("www.", ""));
                str_url = str_url.Replace("www.", "");
                redirect = true;
            }
             */

            /*
            if (str_url.Contains("http:") && !str_url.Contains(":53935") && !str_url.Contains(":53936") && !str_url.Contains("salessignup") && !str_url.Contains("test."))
            {
                //Response.Redirect(str_url.Replace("www.", ""));
                str_url = str_url.Replace("http:", "https:");
                redirect = true;
            }
            */

            if (!Request.IsLocal && !Request.IsSecureConnection && !str_url.Contains(":53935") && !str_url.Contains(":53936") && !str_url.Contains("test."))
            {
                string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
                Response.Redirect(redirectUrl, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

#if DEBUG
            System.Diagnostics.Debug.Print("LoginNew::Page_Load: Load Time 1: " + watch.Elapsed.Seconds.ToString() + " seconds");
#endif

            if (redirect)
            {
                Response.Redirect(str_url);
            }

            Session["parent"] = null;
            Session["ddl_locationCount"] = null;
            //keeps messages from showing up.
            DMP.Secure.HideMessages = true;

            bool l_bCloverRedirect = IsCloverRedirect();
            bool l_bPOSRedirect = IsPOSRedirect();

            if (Session["CloverToken"] != null)
            {
                Session["CloverToken"] = null;
            }

#if DEBUG
            System.Diagnostics.Debug.Print("LoginNew::Page_Load: Load Time 2: " + watch.Elapsed.Seconds.ToString() + " seconds");
#endif
            // Clover Token Code
            if ((l_bCloverRedirect) || (l_bPOSRedirect))
            {


                //grab that token because we will use it to fill...
                /*
                https://www.clover.com/v3/merchants/TSCDQGP40WF9T/?access_token=720932d27594f4b56a4ffff2c8f821ec

                name
                phoneNumber
                createdTime - ticks

                https://www.clover.com/v3/merchants/TSCDQGP40WF9T/address?access_token=720932d27594f4b56a4ffff2c8f821ec

                address1
                address2
                address3
                city
                country
                phoneNumber
                state
                zip
                */
                //we use token to get the merchantID!!
                //if (Request.QueryString["token"] != null)
                //    Session["CloverToken"] = Request.QueryString["token"].ToString();
                //else
                //    Session["CloverToken"] = Request.QueryString["access_token"].ToString();

                //Validate merchant and token will work
                //if (ValidateCloverAccount2(Session["CloverMerchantID"].ToString(), Session["CloverToken"].ToString()))
                //{
                //    Session["allow"] = "true";
                //    if (Request.QueryString["storeSet"] != null)
                //    {
                //        //store not set send them to locations.aspx, well if there are locations
                //        Session["storeSet"] = Request.QueryString["storeSet"].ToString();
                //    }

                //    // Check for Clover Merchant
                //    if (Session["CloverToken"].ToString().Trim().Length > 0)
                //    {
                //        try
                //        {
                //            //    // ?token=a6cada43-60ca-4bfe-ea17-fcab00f1b9c7&username=Shaun SVenson&email=ssvenson@opt-inmedia.com"
                //            //    //http://localhost:53935/loginnew.aspx?token=8c784c4c-900b-825c-8b70-8dc96c7704a6&username=Shaun_Svenson&email=ssvenson@opt-inmedia.com                                


                //            // Load the Clover Session variables and redirect to createCloverLogin.aspx

                //            if (Session["CloverMerchantID"] == null)
                //                Session["CloverMerchantID"] = "";

                //            //if (Request.QueryString["merchant_id"] != null)
                //            //    Session["CloverMerchantID"] = Request.QueryString["merchant_id"].ToString();
                //            //else if (Request.QueryString["mid"] != null)
                //            //    Session["CloverMerchantID"] = Request.QueryString["mid"].ToString();
                //            //else
                //            //    GetCloverMerchantIDFromDB();

                //            if (Request.QueryString["email"] != null)
                //                Session["CloverEmail"] = Request.QueryString["email"].ToString();
                //            else
                //                GetCloverEmailVar();
                //            if (Request.QueryString["storeid"] != null)
                //                Session["CloverStoreID"] = Request.QueryString["storeid"].ToString();

                //            // Merchant Demographics
                //            if (Request.QueryString["storename"] != null)
                //                Session["CloverStoreName"] = Request.QueryString["storename"].ToString();
                //            else
                //                GetCloverStoreNameVar();

                //            if (Request.QueryString["storeaddr1"] != null)
                //            {
                //                Session["CloverStoreAddr1"] = Request.QueryString["storeaddr1"].ToString();
                //                if (Request.QueryString["storeaddr2"] != null)
                //                    Session["CloverStoreAddr2"] = Request.QueryString["storeaddr2"].ToString();
                //                if (Request.QueryString["storeaddr3"] != null)
                //                    Session["CloverStoreAddr3"] = Request.QueryString["storeaddr3"].ToString();
                //                if (Request.QueryString["storecity"] != null)
                //                    Session["CloverStoreCity"] = Request.QueryString["storecity"].ToString();
                //                if (Request.QueryString["storestate"] != null)
                //                    Session["CloverStoreState"] = Request.QueryString["storestate"].ToString();
                //                if (Request.QueryString["storecountry"] != null)
                //                    Session["CloverSDtoreCountry"] = Request.QueryString["storecountry"].ToString();
                //                if (Request.QueryString["storezip"] != null)
                //                    Session["CloverStoreZip"] = Request.QueryString["storezip"].ToString();
                //                if (Request.QueryString["storephone"] != null)
                //                    Session["CloverStorePhone"] = Request.QueryString["storephone"].ToString();
                //            }
                //            else
                //            {
                //                // Get Merchant demographic info from Clover's site                            
                //                GetCloverVars();
                //            }

                //            //// If we were unable to get the Merchant ID, retrieve it with the Clover redirect URL
                //            //if (Session["CloverMerchantID"].ToString().Trim().Length < 1)
                //            //Response.Redirect("https://www.clover.com/oauth/authorize?client_id=T6N4V03WDT6F2&response_type=token&redirect_uri=https://cybercoupons.com");
                //            //else
                //            Response.Redirect("createCloverLogin.aspx", false);
                //            return;
                //        }
                //        catch (Exception ex)
                //        {
                //            showError("Error reading your Clover settings! <font size='-3'>(" + ex.Message + ")</font>");
                //        }
                //    }
                //}//if valid clover account provided
                //else
                //{
                //    //lblcreateCloverLogin.Text = "<font color='black' size='3'>You have selected a Merchant that is not associated with this Clover Terminal. Please log in to Clover and select the Merchant that is associated with this Clover Terminal.2";
                //    //btnCloverCompleteSetup.Text = "Log In To Clover";
                //    //btnCloverLogin.Visible = false;
                //    //bError = true;
                //    Response.Redirect("https://www.clover.com/oauth/authorize?client_id=T6N4V03WDT6F2&response_type=token&redirect_uri=https://cybercoupons.com/merchant", true);
                //}

            }
            else
            {
                // Clover Terminal has sent just the Access Token that doesn't exist in the cloverMerchants table, 
                // redirect to CLover login for a complete send of Merchant ID, Employee ID, and Access Token
                //if(Request.QueryString["token"] != null)
                //    Response.Redirect("https://www.clover.com/oauth/authorize?client_id=T6N4V03WDT6F2&response_type=token&redirect_uri=https://cybercoupons.com/merchant", true);

            }

#if DEBUG
            System.Diagnostics.Debug.Print("LoginNew::Page_Load: Load Time 3: " + watch.Elapsed.Seconds.ToString() + " seconds");
#endif

            if (IsPostBack)
            {
                //If we did fill the hash value and merchantID we need to store that in the database...
                if (hashValue.Value.Length > 0 || merchantID.Value.Length > 0)
                {
                    //Session["CloverMerhcantID"] = merchantID.Value;
                    //Session["CloverAccessCode"] = hashValue.Value;

                    Response.Redirect("/merchant/Login/CloverCallback.aspx?" + Request.QueryString.ToString(), true);
                    return;

                    //insert that store data into the legacy db
                    MySql.Data.MySqlClient.MySqlConnection r_ReturnVal2 = null;
                    string l_strConnection2 = "server=localhost;user id=root; password=CyBEr553CouPOnS!; database=cbqmaster; pooling=false";
                    try
                    {
                        r_ReturnVal2 = new MySqlConnection(l_strConnection2);
                        MySqlCommand command2 = r_ReturnVal2.CreateCommand();
                        //brand new so we are always going to be inserting here.
                        r_ReturnVal2.Open();
                        command2.CommandText = "Replace INTO cloverMerchants (cloverMerchantID, cloverAccessToken,cloverUser,cloverEmail) VALUES ('" + merchantID.Value + "', '" + hashValue.Value + "','" + strUser + "','" + Session["CloverEmail"].ToString().Trim() + "')";
                        command2.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        c_strException = ex.Message;
                    }
                    if (r_ReturnVal2 != null)
                    {
                        r_ReturnVal2.Dispose();
                        r_ReturnVal2 = null;
                    }
                    Session["allow"] = "true";
                }
                else if (Request.QueryString["token"] != null)
                {
                    Session["CloverAccessCode"] = Request.QueryString["token"].ToString();
                    Response.Redirect("/merchant/Login/CloverCallback.aspx?" + Request.QueryString.ToString(), true);
                    return;
                }
                //button handles this
            }
            else
            {
                handleBrand();
                //not a postback, but a page load, load  their encrypted cookie data and put in UI.
                retrieveCookieLogin();
                Page.Focus();
            }

#if DEBUG
            System.Diagnostics.Debug.Print("LoginNew::Page_Load: Load Time 4: " + watch.Elapsed.Seconds.ToString() + " seconds");
#endif

            if (Request.QueryString["mtdsuser"] != null)
            {
                usernamefield.Text = Request.QueryString["mtdsuser"];
            }
        }

        public bool ValidateCloverAccount2(string p_strMerchantID, string p_strToken)
        {
            bool r_bReturnVal = false;
            try
            {
                /*
                if (Session["CloverToken"] == null)
                    return false;
                if (Session["CloverMerchantID"] == null)
                    return false;

                if (Session["CloverToken"].ToString().Trim().Length < 1)
                    return false;
                if (Session["CloverMerchantID"].ToString().Trim().Length < 1)
                    return false;

                string url1 = "https://www.clover.com/v3/merchants/" + Session["CloverMerchantID"].ToString().Trim() + "/?access_token=" + Session["CloverToken"].ToString().Trim();
                 * */
                string url1 = "https://www.clover.com/v3/merchants/" + p_strMerchantID;

                //Debug.Print("ValidateCloverAccount: url1=" + url1);

                ////get with JSON parser
                ////name
                ////phoneNumber
                ////createdTime - ticks
                var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url1);
                httpWebRequest.ProtocolVersion = System.Net.HttpVersion.Version10;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Headers.Add("Authorization:Bearer " + Session["CloverToken"].ToString());

                var response = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                //var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                //Newtonsoft.Json.Linq.JObject objResp = Newtonsoft.Json.Linq.JObject.Parse(responseString);

                r_bReturnVal = true;
            }
            catch (Exception ex)
            {
                c_strException = ex.Message;
                r_bReturnVal = false;
            }

            return r_bReturnVal;

        }

        string c_strCloverToken = "";

        bool IsPOSRedirect()
        {
            bool r_bReturnVal = false;
            // This code checks to see if we're being called by the Clover POS.
            // Grab the hashed Clover Token
            if (Request.QueryString["token"] != null)
            {
                Session["CloverToken"] = Request.QueryString["token"].ToString();
                //Query our DB to get the MerchantID associated with this token
                string l_strMerchantID = GetMerchantIDFromToken(Session["CloverToken"].ToString());
                if (l_strMerchantID.Length > 1)
                {
                    Session["CloverMerchantID"] = l_strMerchantID;
                    r_bReturnVal = true;
                }
            }
            return r_bReturnVal;
        }

        bool IsCloverRedirect()
        {
            bool r_bReturnVal = false;
            // This code checks to see if we're being called by the Clover login webpage.
            // Grab the hashed Clover Token
            if (Session["CloverToken"] == null)
            {
                if (hashValue.Value.Trim().Length > 1)
                {
                    Session["CloverToken"] = hashValue.Value;
                    c_strCloverToken = hashValue.Value;
                }
                else
                {
                    if (Request.QueryString["token"] != null)
                    {
                        Session["CloverToken"] = Request.QueryString["token"].ToString();
                        c_strCloverToken = Request.QueryString["token"].ToString();
                    }
                }
            }
            if (c_strCloverToken.Length > 1)
            {
                // Is this a Clover.com redirect?
                if (Request.QueryString["client_id"] != null)
                {
                    if (Request.QueryString["client_id"].ToString().Trim() == "T6N4V03WDT6F2")
                    {
                        if (Request.QueryString["merchant_id"] != null)
                        {
                            r_bReturnVal = true;
                            Session["CloverMerchantID"] = Request.QueryString["merchant_id"].ToString();
                        }
                    }
                }
            }
            return r_bReturnVal;
        }

        protected void handleBrand()
        {
            if (Session["partnerID"] != null || Request.Cookies["partnerID"] != null || Request.QueryString["partnerID"] != null)
            {
                if (Request.QueryString["partnerID"] != null)
                {
                    img_Logo.ImageUrl = global.GetPartnerImage(Convert.ToInt32(Request.QueryString["partnerID"])) + "?t=" + DateTime.Now;
                    Response.Cookies["partnerID"].Value = Request.QueryString["partnerID"].ToString();
                    Response.Cookies["partnerID"].Expires = DateTime.Now.AddYears(1);
                    Session["partnerID"] = Request.Cookies["partnerID"].Value;
                }
                else if (Session["partnerID"] != null)
                {
                    img_Logo.ImageUrl = global.GetPartnerImage(Convert.ToInt32(Session["partnerID"])) + "?t=" + DateTime.Now;
                    Response.Cookies["partnerID"].Value = Session["partnerID"].ToString();
                    Response.Cookies["partnerID"].Expires = DateTime.Now.AddYears(1);
                }
                else if (Request.Cookies["partnerID"] != null)
                {
                    img_Logo.ImageUrl = global.GetPartnerImage(Convert.ToInt32(Request.Cookies["partnerID"].Value)) + "?t=" + DateTime.Now;
                    Session["partnerID"] = Request.Cookies["partnerID"].Value;
                }
            }
            else
            {
                Session["partnerID"] = 0;
            }
        }

        protected void submitbtn_Click(object sender, EventArgs e)
        {
            //if (Session["CreateCloverLoginPage"] != null)
            //{
            //    // This should mean that the Clover User chose 'Assign Clover Terminal to an existing Account'. Pass control back to createCloverLogin.aspx.
            //    if (Session["CreateCloverLoginPage"].ToString() == "2")
            //    {
            //        Session["CreateCloverLoginPage"] = "3"; // Advance to the Parent / Store selection page
            //        Session["UserNameField"] = usernamefield.Text;
            //        Session["PasswordField"] = passwordfield.Text;
            //        Response.Redirect("createCloverLogin.aspx");
            //        return;
            //    }
            //}

            handleLogin();
        }

        protected void btn_LoginNew_SalesLogin_Click(object sender, EventArgs e)
        {
        }

        protected void submitbtn_Click9(object sender, EventArgs e)
        {
            // Clover Login, do not send to invite.aspx
            //if (Session["CreateCloverLoginPage"] != null)
            //    return;

            //handleLogin();
            if (usernamefield.Text.Length == 0 && passwordfield.Text.Length == 0)
            {
                Response.Redirect("invite.aspx");
            }
            else
            {
                //errorLabel.Text = "Press Login to continue";
                handleLogin();
            }
        }

        void getStoreInfo()
        {
            //server side field validation totr ensure not too long or \
            //pw not over 25, greater than 5 characters, alphanumberic only letter and number plus  
            string l_strEmail = usernamefield.Text;
            string l_strPassword = passwordfield.Text;
            string l_strError = "";
            errorLabel.Text = "";
            bool l_bValid = false;

            if (CyberpassReg.VerifyEmail(l_strEmail, out l_strError))
            {
                if (CyberpassReg.VerifyPassword(l_strPassword, out l_strError))
                {
                    l_bValid = true;
                }
            }
            if (l_bValid)
            {
                //login in the member. If they don't have a parent store, create one. Then save the ManageStore object to the session
                if (c_ManageStore.Login(usernamefield.Text, passwordfield.Text) == true)
                {
                    Session["UserNameField"] = usernamefield.Text;
                    Session["PasswordField"] = passwordfield.Text;

                    // Initialize the BrandBuilder object for use with the Subscription Plan code <CarlB 08-01-2018>
                    BackOffice l_BackOffice = new BackOffice("");
                    if (l_BackOffice.Login(Session["UserNameField"].ToString(), Session["PasswordField"].ToString()) == true)
                    {
                        Session["BackOffice"] = l_BackOffice.BrandBuilderInfo.Serialize();
                    }
                    else
                    {
                        Session["BackOffice"] = null;
                    }

                    c_Parents = c_ManageStore.StoreInfo.StoreInfoList;

                    if (c_Parents.Count > 0)
                    {
                        //It worked
                        if (c_Parents.Count > 1)
                        {
                            //they need to pick what parent they would like to manage
                            global.SaveManageStore(c_ManageStore.StoreInfo);
                        }
                        else
                        {

                            StoreInformationServer l_Parent = (CBQ.StoreInformationServer)c_Parents[0];

                            Session["parent"] = l_Parent.Serialize();

                            //might be a better place for this. This should be the only place this happens, but might want to put this out at global
                            //this really needs to pull from the database so we will do that instead, using parameterized queries of course
                            if (l_Parent.SalesAgentID == "palmetto01")
                            {
                                Session["partnerID"] = "1";
                                handleBrand();
                            }
                            else if (l_Parent.SalesAgentID == "ignite01")
                            {
                                Session["partnerID"] = "2";
                                handleBrand();
                            }
                            else if (l_Parent.SalesAgentID == "cbq")
                            {
                                Session["partnerID"] = "0";
                                handleBrand();
                            }
                            else if (l_Parent.SalesAgentID == "blackstone01")
                            {
                                Session["partnerID"] = "3";
                                handleBrand();
                            }
                            else if (l_Parent.SalesAgentID == "vantiv01")
                            {
                                Session["partnerID"] = "4";
                                handleBrand();
                            }
                            else if (l_Parent.SalesAgentID == "globalpayments01")
                            {
                                Session["partnerID"] = "5";
                                handleBrand();
                            }
                            else if (l_Parent.SalesAgentID == "usacard01")
                            {
                                Session["partnerID"] = "6";
                                handleBrand();
                            }

                            global.SaveManageStore(c_ManageStore.StoreInfo);
                        }
                    }
                    else
                    {
                        l_strError = "Your Login Is Correct But There Was An Error Generating Your Store. Please Contact Us.";
                        showError(l_strError);
                    }
                }
                else
                {
                    l_strError = "Your Login Information is Incorrect. Please Try Again.";
                    showError(l_strError);
                }
            }
            else
            {
                //display an error. message is in l_strError
                showError(l_strError);
            }
        }

        void handleLogin()
        {
            getStoreInfo();

            if (errorLabel.Text.Length == 0)
            {
                //handle saving the username and password to cookie
                if (this.Chk_SaveLogin.Checked)
                {
                    string l_strClear = passwordfield.Text + ";" + usernamefield.Text;
                    string l_strUrlEncrypted = DMP.Security.URLEncrypt(l_strClear);
                    Response.Cookies["LoginInfo"].Value = l_strUrlEncrypted;
                    Response.Cookies["LoginInfo"].Expires = DateTime.Now.AddYears(1);
                }
                else
                {
                    //wipe out cookies
                    /*
                    var c = new HttpCookie("LoginInfo");
                    c.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c);
                     */

                    // Use more Secure Cookies, this may fix the "Validation of viewstate MAC failed" error <CarlB 07-07-2017>
                    var c = new HttpCookie("LoginInfo");
                    c.Expires = DateTime.Now.AddDays(-1);
                    c.HttpOnly = true;
                    if (Request.IsSecureConnection == true)
                    {
                        c.Secure = true;
                    }
                    Response.Cookies.Add(c);
                }

                bool l_boolSignupState = false;

                //How many child stores are there, if there are none that means they haven't completed their sign up process and should go back to that path,
                //otherwise they should go to dashboardhome.aspx
                MySql.Data.MySqlClient.MySqlConnection r_ReturnVal = null;
                string l_strConnection = "server=localhost;user id=root; password=CyBEr553CouPOnS!; database=cbqmaster; pooling=false";
                try
                {
                    r_ReturnVal = new MySqlConnection(l_strConnection);
                    MySqlCommand command = r_ReturnVal.CreateCommand();
                    command.CommandText = "SELECT signupcomplete,salesagentString " +
                                            "FROM storemanagers " +
                                            "LEFT JOIN cbqmaster.salesagents ON salesagents.agentemail = storemanagers.loginname " +
                                            "WHERE loginname = '" + usernamefield.Text + "'";
                    r_ReturnVal.Open();
                    MySqlDataReader Reader;
                    Reader = command.ExecuteReader();
                    Session["MOM_Access"] = null;
                    while (Reader.Read())
                    {
                        l_boolSignupState = Convert.ToBoolean(Reader["signupComplete"].ToString());
                        Session["MOM_Access"] = Reader["salesagentString"];
                    }

                    if (Session["MOM_Access"].ToString() == "")
                    {
                        Session["MOM_Access"] = null;
                    }

                    // Set whether User has access to the Marketing Tab or not <CarlB 11-08-2017>
                    Session["MarketingTab_Access"] = false;
                    if (usernamefield.Text.ToLower().Contains("cbeeson@opt-inmedia.com") == true ||
                        usernamefield.Text.ToLower().Contains("mike1chang@yahoo.com") == true ||
                        usernamefield.Text.ToLower().Contains("bigkevstest@cybercoupons.com") == true ||
                        usernamefield.Text.ToUpper().Contains("FILLANUCHIES@CYBERCOUPONS.COM") == true ||
                        usernamefield.Text.ToLower().Contains("optinhotdogs@cybercoupons.com") == true ||
                        usernamefield.Text.ToLower().Contains("ikevanskike@yahoo.com") == true ||
                        usernamefield.Text.ToLower().Contains("sales@atlastechsolutions.com") == true ||
                        usernamefield.Text.ToLower().Contains("mario@cybercoupons.com") == true ||
                        usernamefield.Text.ToLower().Contains("dpal71@hotmail.com") == true ||
                        usernamefield.Text.ToLower().Contains("jayjung@microsysinc.net") == true ||
                        usernamefield.Text.ToLower().Contains("lobster@lobster.com") == true ||
                        usernamefield.Text.ToLower().Contains("kevskafe@cybercoupoins.com") == true ||
                        usernamefield.Text.ToLower().Contains("lobster@lobstershop.com") == true ||
                        usernamefield.Text.ToLower().Contains("familythriftcenter@cybercoupons.com") == true)
                    {
                        Session["MarketingTab_Access"] = true;
                    }

                    r_ReturnVal.Close();
                }
                catch (Exception ex)
                {
                    string theresponse;
                    theresponse = ("MySQL.ConnectToDatabase() Exception connecting to cqmain.Exception = " + ex.Message);
                    if (r_ReturnVal != null)
                    {
                        r_ReturnVal.Dispose();
                        r_ReturnVal = null;
                    }
                }

                if (Session["parent"] == null)
                {
                    Response.Redirect("selectMerchant.aspx");
                }

                if (l_boolSignupState == false)
                {
                    Response.Redirect("~/signup1.aspx");
                }
                else
                {
                    DateTime startDate;
                    DateTime endDate;
                    startDate = Convert.ToDateTime(System.DateTime.Today.AddDays(-7));
                    endDate = Convert.ToDateTime(System.DateTime.Today);
                    Session["calendar1"] = startDate;
                    Session["calendar2"] = endDate;

                    Response.Redirect("dashboardHome.aspx");
                    //Response.Redirect("processPayment.aspx");
                }
            }
        }

        void showError(string theError)
        {
            //A login error
            errorLabel.Text = theError;
            passwordfield.Text = "";
        }

        void retrieveCookieLogin()
        {
            try
            {
                if (Request.Cookies["LoginInfo"] != null)
                {
                    string l_strLoginInfo = Request.Cookies["LoginInfo"].Value;
                    if ((l_strLoginInfo != null) && (l_strLoginInfo.Length > 0))
                    {
                        string l_strUrlDecoded = System.Web.HttpUtility.UrlDecode(l_strLoginInfo);
                        l_strLoginInfo = DMP.Security.Decrypt(l_strUrlDecoded);
                        string[] l_strSplit = l_strLoginInfo.Split(";".ToCharArray());
                        if (l_strSplit.Length > 1)
                        {
                            string l_strPassword = l_strSplit[0];
                            string l_strEmail = l_strSplit[1];
                            if ((l_strEmail.Length > 0) && (l_strPassword.Length > 0))
                            {
                                //fill the form with their emailaddress and password.
                                this.usernamefield.Text = l_strEmail;
                                passwordfield.Attributes.Add("value", l_strPassword);
                                this.Chk_SaveLogin.Checked = true;
                            }
                            else
                            {
                                //not valid clear the cookie
                                Response.Cookies["LoginInfo"].Value = "";
                            }
                        }
                        else
                        {
                            //not valid
                            Response.Cookies["LoginInfo"].Value = "";
                        }
                    }
                }
            }
            catch
            {
                //clear the cookie since an error occured.
                Response.Cookies["LoginInfo"].Value = "";
            }
            return;
        }

        void GetCloverStoreNameVar()
        {
            try
            {
                var httpWebRequest2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://www.clover.com/v3/merchants/" + Session["CloverMerchantID"].ToString());
                httpWebRequest2.ProtocolVersion = System.Net.HttpVersion.Version10;
                httpWebRequest2.KeepAlive = false;
                httpWebRequest2.Accept = "application/json";
                httpWebRequest2.Headers.Add("Authorization:Bearer " + Session["CloverToken"].ToString());

                var response = (System.Net.HttpWebResponse)httpWebRequest2.GetResponse();
                var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                Newtonsoft.Json.Linq.JObject objResp = Newtonsoft.Json.Linq.JObject.Parse(responseString);

                Session["CloverStoreName"] = (string)objResp["name"];
            }
            catch (Exception ex)
            {
                c_strException = ex.Message;
            }
        }

        void GetCloverEmailVar()
        {
            if (Session["CloverMerchantID"] == null)
                return;
            if (Session["CloverMerchantID"].ToString().Trim().Trim().Length < 1)
                return;

            try
            {
                var httpWebRequest2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://www.clover.com/v3/merchants/" + Session["CloverMerchantID"].ToString() + "/employees/");
                httpWebRequest2.ProtocolVersion = System.Net.HttpVersion.Version10;
                httpWebRequest2.KeepAlive = false;
                httpWebRequest2.Accept = "application/json";
                httpWebRequest2.Headers.Add("Authorization:Bearer " + Session["CloverToken"].ToString());

                var response = (System.Net.HttpWebResponse)httpWebRequest2.GetResponse();
                var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                Newtonsoft.Json.Linq.JObject objResp = Newtonsoft.Json.Linq.JObject.Parse(responseString);

                for (int i = 0; i < objResp.Count; i++)
                {
                    if (Session["CloverEmail"] == null)
                    {
                        if (objResp["elements"][i] != null)
                        {
                            if (objResp["elements"][i] != null)
                            {
                                if (objResp["elements"][i]["email"] != null)
                                    Session["CloverEmail"] = objResp["elements"][i]["email"].ToString().Replace("\"", "");
                                if (objResp["elements"][i]["name"] != null)
                                    Session["CloverUser"] = objResp["elements"][i]["name"].ToString().Replace("\"", "");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                c_strException = ex.Message;
            }
        }

        String GetMerchantIDFromToken(String p_strAccessToken)
        {
            String strConnection = "server=localhost;user id=root; password=CyBEr553CouPOnS!; database=cbqmaster; pooling=false";
            String strSQL = "";
            MySqlConnection conTmp;
            MySqlCommand cmdTmp;
            MySqlDataReader Reader;
            String strRes = "";

            try
            {
                conTmp = new MySqlConnection(strConnection);
                cmdTmp = conTmp.CreateCommand();
                strSQL = "select * from cloverMerchants where cloverAccessToken='" + Session["CloverToken"].ToString() + "' ";
                cmdTmp.CommandText = strSQL;
                conTmp.Open();
                Reader = cmdTmp.ExecuteReader();
                if (Reader.Read() == true)
                    if (Reader["cloverMerchantID"] != null)
                        strRes = Reader["cloverMerchantID"].ToString().Trim();
                conTmp.Close();
                conTmp.Dispose();
                conTmp = null;
            }
            catch (Exception ex)
            {
                c_strException = ex.Message;
                strRes = "";
            }
            return strRes;
        }

    }
}
