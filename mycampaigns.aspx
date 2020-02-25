<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mycampaigns.aspx.cs" Inherits="MerchantDashboard.mycampaigns" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>My campaigns</title>
    <link rel="shortcut icon" href="assets/images/favicon.ico" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- Local CSS files -->
    <link rel="stylesheet" type="text/css" media="screen" href="assets/css/main.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="assets/css/mycampaigns.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="assets/css/gridview.css" />
    <!-- jQuery CDN -->
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"
        integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>

    <script language="javascript" type="text/javascript">
        /* Idle Timeout Handler (logs user out after 30 minutes of idle time) <CarlB 07-20-2018>*/
        var idleTime = 0;
        $(document).ready(function () {
            //Increment the idle time counter every minute.
            var idleInterval = setInterval(timerIncrement, 60000); // 1 minute

            //alert("idleInterval=" + idleInterval);

            //Zero the idle timer on mouse movement.
            $(this).mousemove(function (e) {
                idleTime = 0;
            });
            $(this).keypress(function (e) {
                idleTime = 0;
            });
        });
        function timerIncrement() {
            var l_lbl_MarketingCampaings_IdleTime = document.getElementById('lbl_MarketingCampaings_IdleTime');
            idleTime = idleTime + 1;

            //alert("timerIncrement: idleTime=" + idleTime);

            if (idleTime > 29) { // 30 minutes or more
                window.location.href = "LoginNew.aspx";
                return false;
            }
        }
    </script>

</head>

<body class="body">
    <div class="colorbar"></div>
    <form class="main-form" runat="server">
        <div class="body-internal-container">
            <!-- MENU -->
            <nav class="main-nav__menu">
                <ul class="menu">
                    <div class="menu__left">
                        <li class="menu__link-container menu__logo-item">
                            <a class="menu__link-item" href="/dashboardhome.aspx">
                                <asp:Image AlternateText="Company logo" CssClass="img-fluid" ImageUrl="assets/images/optin-rewards-powered-by-gtn.svg" runat="server" />
                            </a>
                        </li>
                    </div>
                    <div class="menu__right">
                        <li class="menu__link-container">
                            <asp:HyperLink id="hl_RegisterCustomer" CssClass="menu_link-item" NavigateUrl="addMember.aspx" Text="Register Customer" runat="server" Visible="false" /> 
                            <asp:HyperLink id="hl_redeem" CssClass="menu__link-item" NavigateUrl="redemptionhistory.aspx" Text="" runat="server" Visible="true" > 
                               <img class="img-fluid" src="images/redeem-icon.png" alt="Redeem coupon icon"/>
                            </asp:HyperLink>
                        </li>
                        <li class="menu__link-container">
                            <a class="menu__link-item" href="createstore.aspx">My Account</a>
                        </li>
                    </div>
                </ul>
            </nav>
            <main class="main">
                <!-- SIDEBAR -->
                <nav>
                    <ul class="sidebar">
                        <div onclick="toggleMenu()" class="sidebar__toggler-item">
                            <span>Menu</span>
                            <div class="sidebar__toggler-div">
                                <!-- <i class="fas fa-bars sidebar__menu-icon"></i> -->
                                <img class="sidebar__menu-img" src="assets/images/hamburger.svg" alt="Menu toggle button"/>
                            </div>
                        </div>
                        <div class="sidebar-wrapper" id="sidebar-phone">
                            <div class="sidebar__wrapper-inside">
                                <a class="sidebar__link" href="dashboardhome.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Dashboard</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="Transactions.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Transactions</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="revenue.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Revenue</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="revenue.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Revenue</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="Review.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Reviews</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="mapsNew.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Maps</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="members.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Visitor Loyalty</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="roi.aspx">
                                    <li class="sidebar__link-item">
                                        <span>ROI</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="addMember.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Customers</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="AddMerchantGroup.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Groups</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="newofferlist.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Offers</span>
                                    </li>
                                </a>
                                <a class="sidebar__link" href="mycampaigns.aspx">
                                    <li class="sidebar__link-item">
                                        <span>Marketing</span>
                                    </li>
                                </a>
                            </div>
                        </div>
                    </ul>
                </nav>
                <!-- MAIN -->
                <div class="main-container">
                    <div class="main-container__page-header">
                        <h5 class="page-header__page-title"><asp:label ID="lbl_Merch_Name" runat="server" ></asp:label> - My Campaigns <sup style="font-size:small">(BETA)</sup></h5>
                        <asp:DropDownList ID="theList" CssClass="page-header__select" runat="server" OnSelectedIndexChanged="loadNewSelection" AutoPostBack="true">                     
                        </asp:DropDownList>
                    </div>
                    <div class="main-container__content-container">
                        <div class="main-container__table-container">
                            <div class="my-campaigns__instructions-wrapper">
                                <div>
                                    <span class="label">To Create a Marketing Campaign:</span>
                                    <ol class="sub-text">                                        
                                        <li>Click “Create Campaign” to start the scheduler.</li>
                                        <li>Create an offer for your campaign (or you can select an Informational Campaign with No Offer)</li>
                                        <li>Select a Group (a targeted list of people you are sending the campaign)</li>
                                        <li>Select a Date to Launch your campaign</li>
                                    </ol>
                                </div>
                                <div class="my-campaigns__button-grp">
                                    <asp:Button ID="btn_Scheduler" CssClass="submit-btn-lg ml-md" PostBackUrl="MarketingCampaignScheduler.aspx" runat="server" text="Create Campaign" />
                                    <asp:Button ID="btn_CreateGroup" CssClass="submit-btn-lg" PostBackUrl="addMerchantGroup.aspx" runat="server" text="Create Group" Visible="false" />
                                </div>
                            </div>
                            <asp:Panel ID="pnl_Campaigns" Visible="true" runat="server" Style="overflow-y:scroll; max-height:500px;" >
                                <asp:GridView ID="dgv_CampaignsGrid" CssClass="table-container__table" GridLines="horizontal" runat="server" 
                                     DataKeyNames="CampaignID" AutoGenerateColumns="false" AllowPaging="true" PagerStyle-CssClass="pager" OnPageIndexChanging="OnPaging" PageSize="300" 
                                            OnRowCancelingEdit="dgv_CampaignsGrid_RowCancelingEdit" OnRowEditing="dgv_CampaignsGrid_RowEditing" OnRowUpdating="dgv_CampaignsGrid_RowUpdating">
                                    <RowStyle CssClass="table__table-row" HorizontalAlign="Center" />
                                    <pagersettings mode="Numeric" position="Bottom" />
                                    <AlternatingRowStyle CssClass="table__table-row-alt" HorizontalAlign="Center" />
                                    <Columns>
                                        <%--  %><asp:BoundField DataField="CampaignID" HeaderText="ID" /> --%>
                                        <asp:BoundField DataField="Start Date" HeaderText="Launch Date" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />                                
                                        <asp:BoundField DataField="Message" HeaderText="Message" />                            
                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                        <asp:BoundField DataField="AnalyticsLink" HeaderText="Analytics" />
                                        <asp:BoundField DataField="Offer" HeaderText="Offer" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <%--
                                            //This will be used as the disable button Although one we get the New Card interface enabled This will be used differently                                        <asp:TemplateField>
                                             <ItemTemplate>
                                                 <asp:Button ID="btn" runat="server" CommandName="Delete" CommandArgument='' Text="Disable" />
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                            --%>
                                        <asp:CommandField ShowEditButton="false" />                                                             
                                    </Columns>                            
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnl_Recipients" runat="server" Visible="false" Style="overflow-y:scroll;" >
                                <asp:ListBox ID="lb_Recipients" runat="server" />
                            </asp:Panel>                  
                        </div>
                        <asp:Label ID="lbl_debug" runat="server" visible="false"/>
                    </div>
                </div>                   
            </main>
        </div>
        <footer class="footer">
            <asp:HyperLink id="hl_signout" CssClass="footer-signout-link" NavigateUrl="loginnew.aspx" Text="Sign out" runat="server" Visible="true" />        
    </footer>
    </form>
    
</body>
<script>
    function toggleMenu() {
        let sidebar_menu = document.getElementById("sidebar-phone");
        if (sidebar_menu.style.display === "block") {
            sidebar_menu.style.display = "none";
        } else {
            sidebar_menu.style.display = "block";
        }
    }
</script>
</html>