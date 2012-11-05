<%@ Page Language="VB" Debug="True" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    Dim dtmDate as DateTime
    Dim userName, status as string
    Dim categoryID as Integer
    
    Sub Page_Load
    
    If not isPostBack Then
    
      Dim userID As FormsIdentity
    
           'user status
           If User.Identity.IsAuthenticated Then
    
                userID = User.Identity
                userName = UserID.Name
    
           lblWelcome.Text = "Logged in as " + userName
    
           'display category label of chosen quiz
           getCategory
           lblCategory.Text = "Open Competitions in the " & session("categorySel") & " Category:"
           lblHeader.Text = session("categorySel")
           end if
    
    
           IF ( Request.QueryString( "id" ) ) = 0 Then
           ' is user has selected ALL competitions
           BindAll
           lblHeader.Text = "All"
           lblCategory.Text = "ALL Open Competitions"
           ' is user has selected competitions from a specific category
           else
           categoryID = Int32.Parse( Request.QueryString( "id" ) )
           BindDataGrid
    
           End if
    
    End if
    
    'write todays date
    dtmDate = DateTime.Now()
    lblDate.Text = dtmDate.ToString( "u" )
    
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
    
    sub BindDataGrid
    
                  Dim conSMSQuiz As OleDbConnection
                  Dim strConString, strReader As String
                  Dim cmdSelectCompetitions As OleDbCommand
                  Dim dtrCompetitionGrid As OleDbDataReader
    
                  'assign connection string from web.config file
                  strConString = ConfigurationSettings.appSettings("ConnectionString")
                  conSMSQuiz = new OleDbConnection( strConString )
    
    
                 'get creator id to load relevant competitions
                  status = "open"
    
                  strReader =  "SELECT [competition].[competitionID], [competition].[question], [competition].[closingDate] FROM [competition] WHERE (([competition].[creatorID] = @cr"& _
    "eatorID) AND ([competition].[status] = @status) AND ([competition].[category] = @category))"
    
                  cmdSelectCompetitions = New OleDbCommand ( strReader, conSMSQuiz )
    
                    cmdSelectCompetitions.Parameters.Add( "@creatorID", userName )
                    cmdSelectCompetitions.Parameters.Add( "@status", status )
                    cmdSelectCompetitions.Parameters.Add( "@category", categoryID )
                    conSMSQuiz.Open()
    
                 ' datareader that assigns all quiz data selected to the datagrid
                  dtrCompetitionGrid = cmdSelectCompetitions.ExecuteReader()
    
                    dgrdCompetition.DataSource =  dtrCompetitionGrid
                    dgrdCompetition.DataBind()
    
                  dtrCompetitionGrid.close()
    
                  conSMSQuiz.close()
    
     End Sub
    
    
    sub BindAll
    
                  Dim conSMSQuiz As OleDbConnection
                  Dim strConString, strReader As String
                  Dim cmdSelectCompetitions As OleDbCommand
                  Dim dtrCompetitionGrid As OleDbDataReader
    
                  'assign connection string from web.config file
                  strConString = ConfigurationSettings.appSettings("ConnectionString")
                  conSMSQuiz = new OleDbConnection( strConString )
    
    
                 'get creator id to load relevant competitions
                  status = "open"
    
                  strReader =  "SELECT [competition].[competitionID], [competition].[question], [competition].[closingDate] FROM [competition] WHERE (([competition].[creatorID] = @cr"& _
    "eatorID) AND ([competition].[status] = @status))"
    
                  cmdSelectCompetitions = New OleDbCommand ( strReader, conSMSQuiz )
    
                  cmdSelectCompetitions.Parameters.Add( "@creatorID", userName )
                  cmdSelectCompetitions.Parameters.Add( "@status", status )
                  conSMSQuiz.Open()
    
                 ' datareader that assigns all quiz data selected to the datagrid
                  dtrCompetitionGrid = cmdSelectCompetitions.ExecuteReader()
    
                    dgrdCompetition.DataSource =  dtrCompetitionGrid
                    dgrdCompetition.DataBind()
    
                  dtrCompetitionGrid.close()
    
                  conSMSQuiz.close()
    
     End Sub
    
    
    sub getCategory
    
                  Dim conSMSQuiz As OleDbConnection
                  Dim strConString, strReader As String
                  Dim cmdSelectCompetitions As OleDbCommand
                  Dim dtrCompetitionGrid As OleDbDataReader
    
                  'assign connection string from web.config file
                  strConString = ConfigurationSettings.appSettings("ConnectionString")
                  conSMSQuiz = new OleDbConnection( strConString )
    
    
                 'get creator id to load relevant competitions
                  categoryID = Int32.Parse( Request.QueryString( "id" ) )
    
    
                  strReader =  "SELECT [category].* FROM [category] WHERE ([category].[categoryID] = @categoryID)"& _
    ""
                  cmdSelectCompetitions = New OleDbCommand ( strReader, conSMSQuiz )
    
                    cmdSelectCompetitions.Parameters.Add( "@categoryID", categoryID )
                    conSMSQuiz.Open()
    
                 ' datareader that assigns all quiz data selected to the datagrid
                  dtrCompetitionGrid = cmdSelectCompetitions.ExecuteReader()
    
                  while dtrCompetitionGrid.Read()
                    session("categorySel") = dtrCompetitionGrid("category")
                  End While
    
                  dtrCompetitionGrid.close()
    
                  conSMSQuiz.close()
    
     End Sub

</script>
<!doctype html public "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>My Current Competitions</title>
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
                            <a href="default.aspx">Homepage</a> &gt; <a href="competitions.aspx">My Competitions</a> &gt;&nbsp; <asp:Label id="lblHeader" runat="server"></asp:Label>
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
                        <h2><asp:Label id="lblCategory" runat="server"></asp:Label>
                        </h2>
                        <asp:DataList id="dgrdCompetition" runat="server" cellpadding="3">
                            <ItemTemplate>
                                <li>
                                    <a href='<%# string.format( "competitionDetails.aspx?id={0}", Container.DataItem( "competitionID" ) ) %>'><%# container.DataItem( "question" ) %></a> <b>Competition
                                    Closing Date:</b> [ <%# container.DataItem( "closingDate" ) %> ]
                            </ItemTemplate>
                        </asp:DataList>
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
