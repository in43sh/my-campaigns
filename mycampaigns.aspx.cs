using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMP;
using CBQ;
using GlobalTransactionNetwork;
using StoreManagement;
using MySql.Data.MySqlClient;
using Campaigns;

namespace MerchantDashboard
{
    public partial class mycampaigns : System.Web.UI.Page
    {
        uint g_StoreID;
        public StoreInformationServer c_Parent = null;
        public StoreInformationServer c_CurrentStore = null;
        ArrayList c_Children = new ArrayList();
        List<Campaign> g_CampaignsList = new List<Campaign>();

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
                g_StoreID = Convert.ToUInt32(c_CurrentStore.StoreID);

                if (!IsPostBack)
                {
                    lbl_Merch_Name.Text = c_CurrentStore.StoreName;
                    Session["CampaignID"] = -1;                    
                }
            }
            if (Session["CloverToken"] != null)
            {
                hl_signout.Visible = false;
                hl_RegisterCustomer.Visible = true;
                hl_redeem.Visible = false;
            }
            GetCampaigns();
        }

        public void GetCampaigns()
        {
            lbl_debug.Text = "";
            lbl_debug.Visible = false;
            string l_strError = "";
            ArrayList l_CampaignList = new ArrayList();
            ErrorCodes l_Error = ErrorCodes.NoError;
            int l_iState = -1; //-1 Denotes Campaigns in ANY state
            l_Error = CBQManager.CampaignHelper.GetCampaign(g_StoreID, 0, true, 1, l_iState, l_CampaignList, out l_strError);

            if (l_Error == ErrorCodes.NoError)
            {
                dgv_CampaignsGrid.DataSource = MakeDataGrid(l_CampaignList, "Campaign");
                dgv_CampaignsGrid.DataBind();
                foreach(Campaign campaign in l_CampaignList)
                { g_CampaignsList.Add(campaign); }                
            }
            else
            { 
                lbl_debug.Text = "Error"; lbl_debug.Visible = true;                
            }           
        }

        public DataTable MakeDataGrid(ArrayList p_items, string p_TableType)
        {
            DataTable dt_DatatableReturn = new DataTable(); //Create DataTable and all of it required fields
            int i = 0;
            ArrayList l_Offers = GetOffers();
            if (p_TableType == "Campaign")
            {
                dt_DatatableReturn.Columns.Add("CampaignID", typeof(int));
                dt_DatatableReturn.Columns.Add("Name");
                dt_DatatableReturn.Columns.Add("Message");
                dt_DatatableReturn.Columns.Add("Start Date");
                dt_DatatableReturn.Columns.Add("Type");
                dt_DatatableReturn.Columns.Add("AnalyticsLink");
                dt_DatatableReturn.Columns.Add("Offer");
                dt_DatatableReturn.Columns.Add("Status");

                if (p_items.Count > 0)
                {
                    foreach (Campaign campaigns in p_items)
                    {
                        DataRow dr_CampaignRow = dt_DatatableReturn.NewRow();
                        dr_CampaignRow["CampaignID"] = campaigns.CampaignID;
                        dr_CampaignRow["Name"] = campaigns.FriendlyName;
                        
                        string l_strUnsubString = campaigns.Message;
                        l_strUnsubString = RemoveLinesContainingToken(l_strUnsubString, "STOP");

                        dr_CampaignRow["Message"] = campaigns.Message; //l_strUnsubString;
                        
                        DateTime l_start = (DateTime)campaigns.CampaignDate.ToLocalTime();
                        dr_CampaignRow["Start Date"] = l_start.ToString("MM/dd/yyyy hh:mm tt");
                        dr_CampaignRow["AnalyticsLink"] = "Link";//campaigns.;
                        if (campaigns.OfferID == 1)
                        { dr_CampaignRow["Offer"] = "No Offer"; }
                        else
                        {
                            foreach (OfferInformation myoffer in l_Offers)
                            {
                                if (campaigns.OfferID == myoffer.OfferID)
                                { dr_CampaignRow["Offer"] = myoffer.Description; }

                            }
                        }
                        string l_strStatus;
                        if (campaigns.State == 1)
                            l_strStatus = "Pending";
                        else if (campaigns.State == 2)
                            l_strStatus = "Completed";
                        else
                            l_strStatus = "Disabled";

                        //make cell or text green when status completed
                        //Get Offer Name and remove variables from message.

                        dr_CampaignRow["Status"] = l_strStatus;

                        if (campaigns.CampaignType == 1)
                        { dr_CampaignRow["Type"] = "Text"; }
                        else { dr_CampaignRow["Type"] = "Email"; }
                        

                        dt_DatatableReturn.Rows.Add(dr_CampaignRow);
                    }
                }
                else if (p_TableType == "Transaction")
                { 
                }
                else
                {
                    DataRow dr_ErrorRow = dt_DatatableReturn.NewRow();
                }
            }
            dt_DatatableReturn = MyGridView_Sorting(dt_DatatableReturn);
            return dt_DatatableReturn;
        }

        private string RemoveLinesContainingToken(string tb, String p_Token)
        {
            String newText = String.Empty;
            List<String> lines = tb.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines.RemoveAll(str => str.Contains(p_Token));
            lines.ForEach(str => newText += str + Environment.NewLine);
            return newText;
        }
        
        private ArrayList GetOffers()
        {
            ArrayList l_OfferResults = new ArrayList();
            l_OfferResults = CBQManager.CBQHelper.GetStoreServerOffers(c_CurrentStore, true);
            
            return l_OfferResults;
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
                //This used to just be the below, but now we have this whole construct to handle what if the parent is selected
                //minus 2 because we put one more item in this list, the parent.
                l_Store = (StoreInformationServer)c_Children[Convert.ToInt16(Session["dropDownIndex"].ToString()) - 2];
            }
            //select the correct picked index from the drop down list
            theList.SelectedIndex = Convert.ToInt16(Session["dropDownIndex"]);
            c_CurrentStore = l_Store;
            lbl_Merch_Name.Text = c_CurrentStore.StoreName;
            GetCampaigns();

        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            dgv_CampaignsGrid.PageIndex = e.NewPageIndex;
            GetCampaigns();
        }

        protected DataTable MyGridView_Sorting(DataTable dataTable)
        {
            dataTable.DefaultView.Sort = "CampaignID DESC";              
            DataTable dt_return = dataTable.DefaultView.ToTable();
            return dt_return;
        }

        protected void dgv_CampaignsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           dgv_CampaignsGrid.EditIndex = -1;
            GetCampaigns();
        }
               
        protected void dgv_CampaignsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["CampaignID"] = dgv_CampaignsGrid.SelectedDataKey.Value;
            lbl_debug.Text = "Selected Campaign ID is: " + Session["CampaignID"];
            lbl_debug.Visible = true;
        }
        
        protected void dgv_CampaignsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}