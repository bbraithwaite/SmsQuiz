<%@ Page Language="VB" Debug="False" %>
<script runat="server">
    
    Dim dtmDate As DateTime
    
    Sub Page_Load()
    
        If Not IsPostBack Then
    
            Dim userID As FormsIdentity
    
            'user status
            If User.Identity.IsAuthenticated Then
    
                userID = User.Identity
    
                'display user ID for active log in
                lblWelcome.Text = "Logged in as " + userID.Name
    
            End If
    
    
        End If
    
        'write todays date
        dtmDate = DateTime.Now()
        lblDate.Text = dtmDate.ToString("u")
    
    End Sub
    
    Sub LogoutPage()
    
        'end forms authentication session
        FormsAuthentication.SignOut()
        Response.Redirect("default.aspx")
    
    End Sub
    
    Sub btnSignout_Click(sender As Object, e As EventArgs)
    
        LogoutPage()
    
    End Sub
    
    Sub btnLogout_Click(sender As Object, e As EventArgs)
    
        LogoutPage()
    
    End Sub

</script>
<!doctype html public "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>Home</title>
    <link href="style.css" type="text/css" rel="stylesheet" />
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
                            Homepage 
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
                    </td>
                    <td valign="top" width="80%">
                        <p align="right">
                            <asp:Label id="lblDate" runat="server"></asp:Label>
                        </p>
                        <h2>Create a New Competition 
                        </h2>
                        <p>
                            <a href="competitions.aspx">Create a new competition</a> 
                        </p>
                        <h2>View Competitions 
                        </h2>
                        <p>
                            <a href="create.aspx">View open competitions and past competitions</a> 
                        </p>
                        <h2>Create an SMS 
                        </h2>
                        <p>
                            <a href="message.aspx">Create and send a&nbsp;SMS message</a> 
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