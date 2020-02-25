using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using DMP;
using CBQ;
using GlobalTransactionNetwork;

namespace MerchantDashboard
{
    public partial class CampaignAnalytics : System.Web.UI.Page
    {
        public StoreInformationServer c_Parent = null;
        public StoreInformationServer c_CurrentStore = null;
        ArrayList c_Children = new ArrayList();
        int g_istoreid { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CBQ.Globals.StoreInfo.StoreID = 1234;//98704079;// 1234;// 95465330;//1234;
            CBQ.Globals.StoreInfo.StorePassword = "5678";//"z253d7xgt9";//"5678";// "965833X5U3";//"5678";
            DMP.Secure.HideMessages = true;
                                  
            if (Session["parent"] == null)
            {
                //Lost session state, go back to login
                Response.Redirect("loginnew.aspx");
            }
            else
            {
                //load session data into class objects
                c_Parent = global.GetParent();
                c_Children = global.GetChildren();
            }

            if (c_CurrentStore == null)
            {
                handleMultiAccountLogin();
            }
            if (c_CurrentStore != null)
            {
                lbl_Merch_Name.Text = c_CurrentStore.StoreName;
                g_istoreid = c_CurrentStore.StoreID;
            }

            if (!IsPostBack)
            {
                                         
            }
        }

        protected void handleMultiAccountLogin()
        {
            // This code is handled centrally in the global.cs class.
            c_CurrentStore = global.StoreDropDownList_Populate(theList, c_Parent, c_Children);

        }

        protected void loadNewSelection(object sender, EventArgs e)
        {
            if (theList.SelectedItem.Value.Length > 1)
            {
                Session["dropDownIndex"] = theList.SelectedIndex;
            }
            else
            {
                //set it back to whatever it was because we are not allowed to pick 0 on the drop down list, that is reserved for Select Store To Manage
                theList.SelectedIndex = Convert.ToInt16(Session["dropDownIndex"]);
            }
            StoreInformationServer l_Store = null;
            if (Convert.ToInt16(Session["dropDownIndex"].ToString()) == 1)
            {
                if (c_Children.Count >= 1)
                {
                    l_Store = c_Parent;
                }
                else
                {
                    l_Store = (StoreInformationServer)c_Children[Convert.ToInt16(Session["dropDownIndex"].ToString()) - 1];
                }
            }
            else
            {
                l_Store = (StoreInformationServer)c_Children[Convert.ToInt16(Session["dropDownIndex"].ToString()) - 2];
            }
            //select the correct picked index from the drop down list
            theList.SelectedIndex = Convert.ToInt16(Session["dropDownIndex"]);
            c_CurrentStore = l_Store;
            g_istoreid = c_CurrentStore.StoreID;
        }
    }
}
