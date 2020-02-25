<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CampaignAnalytics.aspx.cs" Inherits="MerchantDashboard.CampaignAnalytics" %>

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
    <link rel="stylesheet" type="text/css" media="screen" href="assets/css/campaignAnalytics.css" />
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
                            <a class="menu__link-item" href="#">
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
                        <h5 class="page-header__page-title"><asp:label ID="lbl_Merch_Name" runat="server" ></asp:label> - Campaign Analytics (Beta)</h5>
                        <asp:DropDownList ID="theList" CssClass="page-header__select" runat="server" OnSelectedIndexChanged="loadNewSelection" AutoPostBack="true">                     
                        </asp:DropDownList>
                    </div>
                    <div class="main-container__content-container">
                        <%-- MAIN CONTENT --%> 
                            <div class="campaign-analytics__back-container">
                                <a href="mycampaigns.aspx" class="campaign-analytics__back-link">
                                    <img src="assets/images/arrow_left.svg" alt="arrow to the left" class="campaign-analytics__arrow-left"/>
                                    <span class="campaign-analytics__back-text">Back</span>
                                </a>
                            </div>
                            <div class="campaign-analytics__analytics-container">
                                <div class="campaign-analytics__graph-container">
                                    <div class="campaign-analytics__graph-header">
                                        <span class="statistics-card__header-text">Graph</span>
                                        <div class="campaign-analytics__tooltip">
                                            <img class="campaign-analytics__tooltip-img" src="assets/images/tooltip.svg" />
                                            <span class="campaign-analytics__tooltip-text">Lorem ipsum dolor sit amet, consectetur adipiscing elit.</span>
                                        </div>
                                    </div>
                                    <div class="campaign-analytics__graph">

                                    </div>
                                </div>

                                <div class="campaign-analytics__card campaign-analytics__summary-container">
                                    <div class="statistics-card__header">
                                        <span class="statistics-card__header-text">Summary</span>
                                        <div class="campaign-analytics__tooltip">
                                            <img class="campaign-analytics__tooltip-img" src="assets/images/tooltip.svg" />
                                            <span class="campaign-analytics__tooltip-text">Shows how many messages were sent and how many failed to send.</span>
                                        </div>
                                    </div>
                                    <div class="mt-md">
                                        <span class="bold">Name:</span>
                                        <asp:Label ID="Label9" runat="server" Text="Name of the Campaign"></asp:Label>
                                    </div>
                                    <div>
                                        <span class="bold">Number of uses:</span>
                                        <asp:Label ID="Label10" runat="server" Text="1234"></asp:Label>
                                    </div>
                                    <div>
                                        <span class="bold">Status:</span>
                                        <asp:Label ID="Label11" runat="server" Text="Sent out"></asp:Label>
                                    </div>
                                    <div>
                                        <span class="bold">Total revenue:</span>
                                        <asp:Label ID="Label12" runat="server" Text="$123"></asp:Label>
                                    </div>
                                    <div>
                                        <span class="bold">Message:</span>
                                        <asp:Label ID="Label13" runat="server" Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."></asp:Label>
                                    </div>
                                </div>
                                <div class="campaign-analytics__card campaign-analytics__statistics-card">
                                    <div class="statistics-card__header">
                                        <span class="statistics-card__header-text">Reach</span>
                                        <div class="campaign-analytics__tooltip">
                                            <img class="campaign-analytics__tooltip-img" src="assets/images/tooltip.svg" />
                                            <span class="campaign-analytics__tooltip-text">Shows how many messages were sent and how many failed to send.</span>
                                        </div>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">Messages Sent</span>
                                        <asp:Label ID="Label1" CssClass="statistics-card__section-data" runat="server" Text="5,653"></asp:Label>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">Failed Sends</span>
                                        <asp:Label ID="Label2" CssClass="statistics-card__section-data" runat="server" Text="16"></asp:Label>
                                    </div>
                                </div>

                                <div class="campaign-analytics__card campaign-analytics__statistics-card">
                                    <div class="statistics-card__header">
                                        <span class="statistics-card__header-text">Response</span>
                                        <div class="campaign-analytics__tooltip">
                                            <img class="campaign-analytics__tooltip-img" src="assets/images/tooltip.svg" />
                                            <span class="campaign-analytics__tooltip-text">Lorem ipsum dolor sit amet, consectetur adipiscing elit.</span>
                                        </div>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">Opt-Outs</span>
                                        <asp:Label ID="Label3" CssClass="statistics-card__section-data" runat="server" Text="13"></asp:Label>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">Offers Used</span>
                                        <asp:Label ID="Label4" CssClass="statistics-card__section-data" runat="server" Text="114"></asp:Label>
                                    </div>
                                </div>

                                <div class="campaign-analytics__card campaign-analytics__statistics-card">
                                    <div class="statistics-card__header">
                                        <span class="statistics-card__header-text">Return on Investment</span>
                                        <div class="campaign-analytics__tooltip">
                                            <img class="campaign-analytics__tooltip-img" src="assets/images/tooltip.svg" />
                                            <span class="campaign-analytics__tooltip-text">Shows how many messages were sent and how many failed to send.</span>
                                        </div>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">Campaign Cost</span>
                                        <asp:Label ID="Label5" CssClass="statistics-card__section-data" runat="server" Text="-$19.59"></asp:Label>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">Gross Revenue</span>
                                        <asp:Label ID="Label6" CssClass="statistics-card__section-data" runat="server" Text="$1,492"></asp:Label>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">Discounts</span>
                                        <asp:Label ID="Label7" CssClass="statistics-card__section-data" runat="server" Text="-$242"></asp:Label>
                                    </div>
                                    <div class="statistics-card__statistics-section">
                                        <span class="statistics-card__section-header">New Revenue</span>
                                        <asp:Label ID="Label8" CssClass="statistics-card__section-data" runat="server" Text="$1,230.41"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="campaign-analytics__recipients-container">
                                <p class="campaign-analytics__show-recipients mt-lg mb-md" onclick="toggleRecipientsList()">Show recipients</p>
                                <img id="campaign-analytics__recipients-list-item" style="width:100%;" src="https://i.ibb.co/khYsvYs/1.png" alt="just some random picture" border="0">
                            </div>
                        <%-- MAIN CONTENT END --%>
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

    function toggleRecipientsList() {
        let recipients_list = document.getElementById("campaign-analytics__recipients-list-item");
        if (recipients_list.style.display === "block") {
            recipients_list.style.display = "none";
        } else {
            recipients_list.style.display = "block";
        }
    }
</script>
</html>