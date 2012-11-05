<%@ Page Language="VB" Debug="True" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    Dim dtmDate As DateTime
    
    Sub Page_Load()
    
        If Not IsPostBack Then
    
            'assign welcome label
            Dim userID As FormsIdentity
    
            'user status
            If User.Identity.IsAuthenticated Then
    
                userID = User.Identity
    
                'display user ID for active log in
                lblWelcome.Text = "Logged in as " + userID.Name
    
            End If
    
            BindDataGrid()
    
    
    
        End If
    
        'write todays date
        dtmDate = DateTime.Now()
        lblDate.Text = dtmDate.ToString("u")
    
    End Sub
    
    Sub BindDataGrid()
    
        'database connection settings
        Dim conSMSQuiz As OleDbConnection
        Dim strConString, strReader As String
        Dim cmdSelectCompetitions As OleDbCommand
        Dim dtrCompetition, dtroldCompetiton As OleDbDataReader
    
        ' assign connection string from web.config file
        strConString = ConfigurationSettings.AppSettings("ConnectionString")
        conSMSQuiz = New OleDbConnection(strConString)
    
        conSMSQuiz.Open()
    
    
        'display competition category groups
        strReader = "SELECT categoryID, category FROM category"
    
        cmdSelectCompetitions = New OleDbCommand(strReader, conSMSQuiz)
    
        'allows competitions to be selected in relation to their category
        dtrCompetition = cmdSelectCompetitions.ExecuteReader()
    
        dgrdCategory.DataSource = dtrCompetition
        dgrdCategory.DataBind()
    
        dtrCompetition.Close()
    
        'allows competitions to be selected in relation to their category
        dtroldCompetiton = cmdSelectCompetitions.ExecuteReader()
    
        dgrdOldCategory.DataSource = dtroldCompetiton
        dgrdOldCategory.DataBind()
        dtroldCompetiton.Close()
    
    
        conSMSQuiz.Close()
    
    End Sub
    
    Sub LogoutPage()
    
        FormsAuthentication.SignOut()
        Session.Abandon()
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
    <title>Competitions</title>
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
                            <asp:LinkButton id="btnSignout" onclick="btnSignout_Click" runat="server" ForeColor="White" Font-Bold="True" CausesValidation="False">Log Out</asp:LinkButton>
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
                            <a href="default.aspx">Homepage</a> &gt; My Competitions 
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
                    <td valign="top" width="20%">
                        <p class="header">
                        </p>
                    </td>
                    <td valign="top" width="80%">
                        <p align="right">
                            <asp:Label id="lblDate" runat="server"></asp:Label>
                        </p>
                        <h2>My Competitions 
                        </h2>
                        <table cellspacing="10" cellpadding="5" width="60%" border="0">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <h3>Open Competitions 
                                        </h3>
                                        <p>
                                            <a href="mynew.aspx?id=0">View All</a> 
                                        </p>
                                        <p>
                                            <u>In&nbsp;Categories:</u> 
                                        </p>
                                        <asp:DataList id="dgrdCategory" runat="server" cellpadding="0">
                                            <ItemTemplate>
                                                <li>
                                                    <a href='<%# string.format( "mynew.aspx?id={0}", Container.DataItem( "categoryID" ) ) %>'><%# container.DataItem( "category" ) %></a> 
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                    <td valign="top">
                                        <h3>Closed Competitions 
                                        </h3>
                                        <p>
                                            <a href="myold.aspx?id=0">View All</a> 
                                        </p>
                                        <p>
                                            <u>In&nbsp;Categories:</u> 
                                        </p>
                                        <asp:DataList id="dgrdOldCategory" runat="server" cellpadding="0">
                                            <ItemTemplate>
                                                <li>
                                                    <a href='<%# string.format( "myold.aspx?id={0}", Container.DataItem( "categoryID" ) ) %>'><%# container.DataItem( "category" ) %></a> 
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="middle" colspan="2">
                        <p>
                            <a href="default.aspx">Home</a> | <a href="competitions.aspx">Competitions</a> | <a href="create.aspx">Create
                            Quiz</a> | <a href="message.aspx">Create SMS</a>&nbsp;| 
                            <asp:LinkButton id="btnLogout" onclick="btnLogout_Click" runat="server" CausesValidation="False">Log Out</asp:LinkButton>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
