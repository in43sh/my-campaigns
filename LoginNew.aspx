<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginNew.aspx.cs" Inherits="MerchantDashboard.LoginResponsive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="cache-control" content="max-age=0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT" />
    <meta http-equiv="pragma" content="no-cache" />
    <title>Login Page</title>
    <link rel="shortcut icon" href="assets/images/favicon.ico" type="image/x-icon" />
    <!-- Local CSS files -->
    <link rel="stylesheet" type="text/css" media="screen" href="assets/css/main.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="assets/css/login.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <script language="javascript" src="/pathtojsfiles/jquery.min.js"></script>
    <script language="javascript" type="text/javascript">
        if (getCookie('txt_saleprice') != null)
            setCookie('txt_saleprice', null);
        if (getCookie('txt_redempt') != null)
            setCookie('txt_redempt', null);
        if (getCookie('txt_prodserv') != null)
            setCookie('txt_prodserv', null);
        if (getCookie('datepicker') != null)
            setCookie('datepicker', null);
        if (getCookie('ddl_SaleDuration') != null)
            setCookie('ddl_SaleDuration', null);
        if (getCookie('grp1') != null)
            setCookie('grp1', null);
        if (getCookie('grp2') != null)
            setCookie('grp2', null);
        if (getCookie('txt_reqrules') != null)
            setCookie("txt_reqrules", null);
        if (getCookie('txt_saleprice') != null)
            setCookie("txt_saleprice", null);
        if (getCookie('txt_redempt') != null)
            setCookie("txt_redempt", null);
        if (getCookie('txt_prodserv') != null)
            setCookie("txt_prodserv", null);
        if (getCookie('txt_customrules') != null)
            setCookie("txt_customrules", null);
        if (getCookie('txt_setquantity') != null)
            setCookie("txt_setquantity", null);
        if (getCookie('txt_LocationName') != null)
            setCookie("txt_LocationName", null);
        if (getCookie('txt_PhoneNumber') != null)
            setCookie("txt_PhoneNumber", null);
        if (getCookie('txt_WebSite') != null)
            setCookie("txt_WebSite", null);
        if (getCookie('txt_StreetAddress1') != null)
            setCookie("txt_StreetAddress1", null);
        if (getCookie('txt_StreetAddress2') != null)
            setCookie("txt_StreetAddress2", null);
        if (getCookie('txt_ZipCode') != null)
            setCookie("txt_ZipCode", null);
        if (getCookie('txt_Description') != null)
            setCookie("txt_Description", null);
        if (getCookie('hoursTo1') != null)
            setCookie("hoursTo1", null);
        if (getCookie('hoursFrom1') != null)
            setCookie("hoursFrom1", null);
        if (getCookie('hoursTo2') != null)
            setCookie("hoursTo2", null);
        if (getCookie('hoursFrom2') != null)
            setCookie("hoursFrom2", null);
        if (getCookie('hoursTo3') != null)
            setCookie("hoursTo3", null);
        if (getCookie('hoursFrom3') != null)
            setCookie("hoursFrom3", null);
        if (getCookie('hoursTo4') != null)
            setCookie("hoursTo4", null);
        if (getCookie('hoursFrom4') != null)
            setCookie("hoursFrom4", null);
        if (getCookie('hoursTo5') != null)
            setCookie("hoursTo5", null);
        if (getCookie('hoursFrom5') != null)
            setCookie("hoursFrom5", null);
        if (getCookie('hoursTo6') != null)
            setCookie("hoursTo6", null);
        if (getCookie('hoursFrom6') != null)
            setCookie("hoursFrom6", null);
        if (getCookie('hoursTo7') != null)
            setCookie("hoursTo7", null);
        if (getCookie('hoursFrom7') != null)
            setCookie("hoursFrom7", null);
        if (getCookie('ddl_City') != null)
            setCookie("ddl_City", null);
        if (getCookie('ddl_State') != null)
            setCookie("ddl_State", null);
        if (getCookie('txt_offerTitle') != null)
            setCookie("txt_offerTitle", null);
        if (getCookie('txt_title') != null)
            setCookie("txt_title", null);
        if (getCookie('txt_description') != null)
            setCookie("txt_description", null);
        if (getCookie('txt_discountamount') != null)
            setCookie("txt_discountamount", null);

        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "; expires=Yes" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }

        function getCookie(c_name) {
            var c_value = document.cookie;
            var c_start = c_value.indexOf(" " + c_name + "=");
            if (c_start == -1) {
                c_start = c_value.indexOf(c_name + "=");
            }
            if (c_start == -1) {
                c_value = null;
            }
            else {
                c_start = c_value.indexOf("=", c_start) + 1;
                var c_end = c_value.indexOf(";", c_start);
                if (c_end == -1) {
                    c_end = c_value.length;
                }
                c_value = unescape(c_value.substring(c_start, c_end));
            }
            return c_value;
        }

        function getQuerystringNameValue(name) {
            // For example... passing a name parameter of "name1" will return a value of "100", etc.
            // page.htm?name1=100&name2=101&name3=102

            var winURL = window.location.href;
            var queryStringArray = winURL.split("?");
            var queryStringParamArray = queryStringArray[1].split("&");
            var nameValue = null;

            for (var i = 0; i < queryStringParamArray.length; i++) {
                queryStringNameValueArray = queryStringParamArray[i].split("=");

                if (name == queryStringNameValueArray[0]) {
                    nameValue = queryStringNameValueArray[1];
                }
            }

            return nameValue;
        }

        function storeHash() {
            //Clover querystring value contains "#" and Request.QueryString() cannot read any characters past # so we cannot parse this in ASPX. To parse it, we have to put the
            //code here in ASP.
            //hash is not server side accessible...
            //alert("enterfunction");
            //alert(document.getElementById("hashValue").value);
            if (window.location.hash.indexOf("#access_token") > -1) {
                document.getElementById("hashValue").value = window.location.hash.replace("#access_token=", "");
                document.getElementById("merchantID").value = getQuerystringNameValue("merchant_id");
                //    alert(document.getElementById("hashValue").value);
                form1.submit();
            }
            //if (document.getElementById("hashValue").value.length > 0) {
            //    form1.submit();
            //}
        }

        function LoadCashierLoginPopup(l_strStoreID) {
            var closeup = window.open('popCashierLogin.aspx?storeid=' + l_strStoreID, 'closeup', 'location=no,titlebar=no,toolbar=no,scrollbars=no,resizable=no,top=200,left=' + ((screen.width / 2) - (400 / 2) + window.screenX) + ',width=400,height=300');
            closeup.focus();
            return false;
        }

    </script>

    <noscript>
        For full functionality of this site it is necessary to enable JavaScript.
 Here are the <a href="https://www.enable-javascript.com/" target="_blank">instructions how to enable JavaScript in your web browser</a>.
    </noscript>

</head>
<body class="body login-body" onload="storeHash()">
    <form autocomplete="off" id="form1" runat="server" defaultbutton="Button1">
        <div id="loginPage" class="login-content" runat="server">
            <div class="colorbar"></div>
            <div class="login-content__form-wrapper">
                <asp:Image CssClass="login-content__logo" runat="server" ID="img_Logo" ImageUrl="Assets/images/gtn-network.svg" />
                <%--<asp:Button CssClass="cancel-btn login-content__signup-button" ID="signup" runat="server" Text="Sign Up" OnClientClick="location.href='invite.aspx';" OnClick="submitbtn_Click9" />--%>
<%--                <asp:Label ID="lblloginNewCaption" runat="server"></asp:Label>
                <asp:Label ID="lblloginNewForgot" runat="server"><b></b></asp:Label>--%>
                <%--<span class="login-content__infomation">Have an account? Log in below.</span>--%>
            </div>
            <div class="login-content__form-wrapper">
                <div id="username" class="form-group">
                    <label class="label login-content__email-label" for="usernamefield">Email Address:</label>
                    <asp:TextBox CssClass="input" ID="usernamefield" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator9" runat="server"
                        ErrorMessage="This field Required."
                        ControlToValidate="usernamefield"
                        Style="font-size: small">
                    </asp:RequiredFieldValidator>
                </div>
                <div id="password" class="form-group">
                    <label class="label" for="passwordfield">Password:</label>
                    <asp:TextBox CssClass="input" ID="passwordfield" TextMode="password" runat="server" type="password" name="password"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="This field Required."
                        ControlToValidate="passwordfield"
                        Style="font-size: small">
                    </asp:RequiredFieldValidator>
                </div>
                <asp:Label CssClass="login-content__error-label" runat="server" ID="errorLabel" />
                <div class="login-content__actions-container">
                    <asp:CheckBox CssClass="login-content__keep-logged-text" ID="Chk_SaveLogin" runat="server" Font-Size="Small" Text="Remember me" Checked="True" />
                    <p><a class="bold login-content__forgot-password-link" href="lostPassword.aspx">Forgot password?</a></p>
                </div>
                <asp:Button CssClass="submit-btn login-content__login-button" ID="Button1" runat="server" Text="Login" OnClick="submitbtn_Click" />
                <div class="login-content__signup-container">
                    <span class="bold">Don't have an account?</span>                    
                    <a class="bold login-content__signup-link" href="invite.aspx">Sign up!</a>
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hashValue" runat="server" />
                <asp:HiddenField ID="merchantID" runat="server" />
            </div>
            <div id="cashierlogin" runat="server">
            </div>
        </div>
    </form>
</body>
</html>
