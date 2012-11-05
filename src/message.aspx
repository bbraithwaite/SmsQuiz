<%@ Page Language="VB" Debug="True" AspCompat="true" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    Dim dtmDate as DateTime
    Dim userName as string
    
    Sub Page_Load
    
    If not isPostBack Then
    
      Dim userID As FormsIdentity
    
           'user status
           If User.Identity.IsAuthenticated Then
    
            userID = User.Identity
            userName = UserID.Name
    
            lblWelcome.Text = "Logged in as " + userName
    
    
           end if
    
    End if
    
    'write todays date
    dtmDate = DateTime.Now()
    lblDate.Text = dtmDate.ToString( "u" )
    txtMessage.Attributes("name") = "txtMessage"
    txtMessage.Attributes("onKeyUp") = "textCounter(txtMessage,remLen1,160);"
    txtMessage.Attributes("onKeyDown") = "textCounter(txtMessage,remLen1,160);"
    txtMessage.Attributes("onKeyPress") = "textCounter(txtMessage,remLen1,160);"
    
    End Sub
    
    
    
    Sub LogoutPage
    
    FormsAuthentication.SignOut()
    Session.Abandon()
    Response.Redirect("default.aspx")
    
    End Sub
    
    Sub btnSignout_Click(sender As Object, e As EventArgs)
    
    LogoutPage
    
    End Sub
    
    Sub btnLogout_Click(sender As Object, e As EventArgs)
    
    LogoutPage
    
    End Sub
    
    Sub btnSMS_Click(sender As Object, e As EventArgs)
    
    
    
    Dim mobNo, finalMessage As String
    finalMessage = txtMessage.Text
    mobNo = txtMobile.Text
    call sendsms(mobNo,0,finalMessage,"","")
    txtMobile.Text = ""
    txtMessage.Text = ""
    lblSent.Text = "Message Sent!"
    
    
    
    End Sub
    
    'main sms sending function
    
    Function sendsms(to_number, flash, message, f_username, f_password)
    
         Dim method, secured, error_on_length, username, password, AQresponse as string
    
         ' User Editable Variables
    
         secured = 0               ' Set to either 1 for SSL connection or 0 for normal connection.
         error_on_length = 0       ' Whether to give and error on messages over 160 chracters. 1 for true, 0 for false.
        username = "XXXXXX"     ' sets the usernamem for SMS provider.
        password = "XXXXXX"      ' password for sms provider.
    
               Dim objXMLHTTP, xml
         message = replace(message," ","+")
         xml = Server.CreateObject("Microsoft.XMLHTTP")
    
           xml.Open ("POST", "http://www.example.com/sms/", False)
           xml.setRequestHeader ("Content-Type", "application/x-www-form-urlencoded")
           xml.Send ("username=" & username & "&password=" & password & "&to_num=" & to_number & "&message=" & message & "&flash=" & flash)
    
    
         AQresponse = xml.responseText
    
         xml = nothing
    
    End Function

</script>
<!doctype html public "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>Winner</title>
    <link href="style.css" type="text/css" rel="stylesheet" />
    <script language="JavaScript">
function textCounter(field,cntfield,maxlimit) {
if (field.value.length > maxlimit) {
field.value = field.value.substring(0, maxlimit);
}
else {
cntfield.value = maxlimit -
field.value.length;
}
}
</script>
</head>
<body>
    <form runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td width="10" bgcolor="#7bcc30">
                        <img height="64" alt="" src="images/stripLeft.gif" width="10" /> 
                    </td>
                    <td align="left" width="135" bgcolor="#7bcc30" height="64">
                        <img height="44" alt="SMS Quiz Logo" src="images/logo.gif" width="135" /> 
                    </td>
                    <td valign="bottom" align="right" bgcolor="#7bcc30">
                        <p class="top">
                            <a id="top" href="default.aspx">Home</a> | <a id="top" href="competitions.aspx">Competitions</a> | <a id="top" href="create.aspx">Create
                            Quiz</a> |&nbsp;<a id="top" href="message.aspx">Create SMS</a> | 
                            <asp:LinkButton id="btnSignout" onclick="btnSignout_Click" runat="server" ForeColor="White" Font-Bold="True">Log Out</asp:LinkButton>
                        </p>
                    </td>
                    <td width="10" bgcolor="#7bcc30">
                        <img height="64" alt="" src="images/stripRight.gif" width="10" /> 
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#7bcc30" colspan="4" height="5">
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#005500" colspan="4" height="1">
                    </td>
                </tr>
            </tbody>
        </table>
        <table cellspacing="0" cellpadding="5" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="left" bgcolor="#eeffee">
                        <p class="wayfinder">
                            <a href="default.aspx">Homepage</a> &gt; <a href="competitions.aspx">Competitions</a> &gt;
                            My Competitions 
                        </p>
                    </td>
                    <td bgcolor="#eeffee">
                        <p class="name" align="right">
                            <asp:Label id="lblWelcome" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td bgcolor="#005500" colspan="2" height="1">
                    </td>
                </tr>
            </tbody>
        </table>
        <table cellspacing="0" cellpadding="10" width="100%" border="0">
            <tbody>
                <tr>
                    <td width="20%">
                        <p class="header">
                        </p>
                    </td>
                    <td valign="top" width="80%">
                        <p align="right">
                            <asp:Label id="lblDate" runat="server"></asp:Label>
                        </p>
                        <h2>Send an SMS 
                        </h2>
                        <p>
                            <asp:Label id="lblSent" runat="server"></asp:Label>
                        </p>
                        <table width="300">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <p class="header">
                                            To: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtMobile" runat="server" Width="184px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <p class="header">
                                            Message: 
                                        </p>
                                    </td>
                                    <td>
                                        <p>
                                            <asp:TextBox id="txtMessage" runat="server" Width="185px" TextMode="MultiLine" Height="89px" MaxLength="160"></asp:TextBox>
                                        </p>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*Required" ControlToValidate="txtMessage"></asp:RequiredFieldValidator>
                                        </p>
                                        <p class="status">
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <p>
                                            Number of characters left:<input id="remLen1" type="text" maxlength="3" size="3" value="160" name="remLen1" />
                                        </p>
                                        <p align="center">
                                            <asp:Button id="btnSMS" onclick="btnSMS_Click" runat="server" Text="Send SMS"></asp:Button>
                                        </p>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <h2>
                        </h2>
                        <p class="header">
                        </p>
                        <p class="header">
                        </p>
                        <p align="center">
                        </p>
                    </td>
                </tr>
                <tr>
                    <td align="middle" colspan="2">
                        <p>
                            <a href="default.aspx">Home</a> | <a href="competitions.aspx">Competitions</a> | <a href="create.aspx">Create
                            Quiz</a> | <a href="message.aspx">Create SMS</a>&nbsp;| 
                            <asp:LinkButton id="btnLogout" onclick="btnLogout_Click" runat="server">Log Out</asp:LinkButton>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
