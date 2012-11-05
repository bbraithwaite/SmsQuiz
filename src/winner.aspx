<%@ Page Language="VB" Debug="True" AspCompat="true" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">

    Dim dtmDate as DateTime
    Dim userName, status as string
    
    Dim conSMSQuiz As OleDbConnection
    Dim strReader  As String
    Dim cmdSelectCompetitions As OleDbCommand
    Dim dtrCompetitionGrid As OleDbDataReader
    Dim strConString As String =  ConfigurationSettings.appSettings("ConnectionString")
    
    Sub Page_Load
    
        If Not IsPostBack Then
    
            Dim userID As FormsIdentity
    
            'user status
            If User.Identity.IsAuthenticated Then
    
                userID = User.Identity
                userName = userID.Name
    
                lblWelcome.Text = "Logged in as " + userName
    
                lblWinner.Text = (Session("theWinner"))
    
                BindDataGrid()
    
                Session.Abandon()
    
    
            End If
    
        End If
    
    'write todays date
    dtmDate = DateTime.Now()
    lblDate.Text = dtmDate.ToString( "u" )
    
    End Sub
    
    
    sub BindDataGrid
    
                  'assign connection string from web.config file
                  conSMSQuiz = new OleDbConnection( strConString )
    
                  'get creator id to load relevant competitions
    
    
                  strReader =  "SELECT [competition].* FROM [competition] WHERE ([competition].[competitionID] = @competitionID )"
    
                  cmdSelectCompetitions = New OleDbCommand ( strReader, conSMSQuiz )
    
                  cmdSelectCompetitions.Parameters.Add( "@competitionID", session("compID") )
                  conSMSQuiz.Open()
    
                   'datareader that assigns all quiz data selected to the datagrid
                  dtrCompetitionGrid = cmdSelectCompetitions.ExecuteReader()
    
                  dgrdCompetition.DataSource =  dtrCompetitionGrid
                  dgrdCompetition.DataBind()
    
                  dtrCompetitionGrid.close()
    
                  conSMSQuiz.close()
    
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
    finalMessage = "Congratulations, you are the winner of the ACME quiz competitions. We will contact you shortly to arrange collection of your prize."
    mobNo = lblWinner.Text
    call sendsms(mobNo,0,finalMessage,"","")
    
    End Sub
    
    'main sms sending function
    
    Function sendsms(to_number, flash, message, f_username, f_password)
    
        Dim method, secured, error_on_length, username, password, AQresponse As String
    
        secured = 0               ' Set to either 1 for SSL connection or 0 for normal connection.
        error_on_length = 0       ' Whether to give and error on messages over 160 chracters. 1 for true, 0 for false.
        username = "XXXXXXX"     ' sets the usernamem for SMS provider.
        password = "XXXXXX"      ' password for sms provider.
    
        Dim objXMLHTTP, xml
        message = Replace(message, " ", "+")
        xml = Server.CreateObject("Microsoft.XMLHTTP")
    
        xml.Open("POST", "http://www.example.com/sms/", False)
        xml.setRequestHeader("Content-Type", "application/x-www-form-urlencoded")
        xml.Send("username=" & username & "&password=" & password & "&to_num=" & to_number & "&message=" & message & "&flash=" & flash)
    
    
        AQresponse = xml.responseText
    
        xml = Nothing
    
    End Function

</script>
<!doctype html public "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>Winner</title>
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
                            <asp:LinkButton id="btnSignout" onclick="btnSignout_Click" runat="server" Font-Bold="True" ForeColor="White">Log Out</asp:LinkButton>
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
                        <h2>Competition Winner! 
                        </h2>
                        <p class="header">
                            This competition has been won by: &nbsp;<asp:Label id="lblWinner" runat="server"></asp:Label> 
                        </p>
                        <p class="header">
                            <asp:DataGrid id="dgrdCompetition" BackColor="#EEFFEE" CellPadding="5" AutoGenerateColumns="False" Runat="Server" Font-Size="Smaller" Font-Names="Verdana" datakeyfield="competitionID">
                                <HeaderStyle font-size="XX-Small" font-bold="True" forecolor="White" backcolor="#7BCC30"></HeaderStyle>
                                <ItemStyle font-size="X-Small" font-names="Verdana"></ItemStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="question" HeaderText="Question"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="answerA" HeaderText="Choice A"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="answerB" HeaderText="Choice B"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="answerC" HeaderText="Choice C"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="answerD" HeaderText="Choice D"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="correctAnswer" HeaderText="Correct Answer"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="prize" HeaderText="Prize"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="closingDate" HeaderText="Closing Date (yyyy-mm-dd HH:MM:SS)"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="status" HeaderText="Quiz Status"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </p>
                        <p align="center">
                            <asp:Button id="btnSMS" onclick="btnSMS_Click" runat="server" Text="Send SMS to Winner"></asp:Button>
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
