<%@ Page Language="VB" Debug="false" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    Dim dtmDate As DateTime
    Dim userName, status As String
    Dim compID As Integer
    
    Dim conSMSQuiz As OleDbConnection
    Dim strReader, strInsert, strUpdate As String
    Dim strConString As String = ConfigurationSettings.AppSettings("ConnectionString")
    Dim strIncomingConString As String = ConfigurationSettings.AppSettings("IncomingConnectionString")
    Dim cmdSelectCompetitions, cmdSelectCount, cmdInsert, cmdUpdate As OleDbCommand
    Dim dtrCompetitionGrid, dtrStats, dtrInterest As OleDbDataReader
    
    Sub Page_Load()
    
        If Not IsPostBack Then
    
            Dim userID As FormsIdentity
    
            'user status
            If User.Identity.IsAuthenticated Then
    
                userID = User.Identity
                userName = userID.Name
    
                lblWelcome.Text = "Logged in as " + userName
    
                BindDataGrid()
    
                btnWinner.Attributes.Add("onclick", "return confirm('Are you sure you wish to pick a winner and close this competition?')")
    
            End If
           
            'counts all entries into this competition based on keyword
            countAll()
            'calls function to count number of correct answers based on keyword, response and within date of competition close
            countCorrectAnswers()
            countA()
            countB()
            countC()
            countD()
            lblBreadcrumb.Text = Session("categorySel")
            lblBreadcrumb2.Text = Session("question")
    
        End If
    
        'write todays date
        dtmDate = DateTime.Now()
        lblDate.Text = dtmDate.ToString("u")
    
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
    
    
    Sub BindDataGrid()
    
        'assign connection string from web.config file
        conSMSQuiz = New OleDbConnection(strConString)
    
        'get creator id to load relevant competitions
        compID = Int32.Parse(Request.QueryString("id"))
        status = "open"
    
        strReader = "SELECT [competition].* FROM [competition] WHERE (([competition].[competitionID] = @co" & _
"mpetitionID) )"
    
        cmdSelectCompetitions = New OleDbCommand(strReader, conSMSQuiz)
    
        cmdSelectCompetitions.Parameters.Add("@competitionID", compID)
        conSMSQuiz.Open()
    
        'datareader that assigns all quiz data selected to the datagrid
        dtrCompetitionGrid = cmdSelectCompetitions.ExecuteReader()
    
        dgrdCompetition.DataSource = dtrCompetitionGrid
        dgrdCompetition.DataBind()
    
        dtrCompetitionGrid.Close()
    
        dtrStats = cmdSelectCompetitions.ExecuteReader()
    
        While dtrStats.Read()
            'get keyword
            Session("compID") = (dtrStats("competitionID"))
            Session("question") = (dtrStats("question"))
            Session("keyword") = (dtrStats("requiredKeyword"))
            Session("answer") = (dtrStats("correctAnswer"))
            Session("close") = (dtrStats("closingDate"))
    
        End While
    
        dtrStats.Close()
    
        conSMSQuiz.Close()
    
    
    
    End Sub
    
    Sub countAll()
    
        conSMSQuiz = New OleDbConnection(strIncomingConString)
    
        strReader = "Select Count(*) From contestants WHERE keyword=@Keyword AND timeDate<@close"
    
        cmdSelectCount = New OleDbCommand(strReader, conSMSQuiz)
        cmdSelectCount.Parameters.Add("@Keyword", Session("keyword"))
        cmdSelectCount.Parameters.Add("@close", Session("close"))
    
        conSMSQuiz.Open()
    
        lblAllCount.Text = cmdSelectCount.ExecuteScalar()
    
        conSMSQuiz.Close()
    
    End Sub
    
    Sub countCorrectAnswers()
    
    
        conSMSQuiz = New OleDbConnection(strIncomingConString)
    
        strReader = "Select Count(*) From contestants WHERE keyword=@reqKeyword AND answer=@correctAnswer AND timeDate<@close"
    
        cmdSelectCount = New OleDbCommand(strReader, conSMSQuiz)
        cmdSelectCount.Parameters.Add("@reqKeyword", Session("keyword"))
        cmdSelectCount.Parameters.Add("@correctAnswer", Session("answer"))
        cmdSelectCount.Parameters.Add("@close", Session("close"))
    
        conSMSQuiz.Open()
    
        lblcorrectCount.Text = cmdSelectCount.ExecuteScalar()
    
        conSMSQuiz.Close()
    
    End Sub
    
    Sub countA()
    
    
        Dim counterA As Integer
        Dim aPercent As Double
        conSMSQuiz = New OleDbConnection(strIncomingConString)
    
        strReader = "Select Count(*) From contestants WHERE keyword=@reqKeyword AND answer=@correctAnswer AND timeDate<@close"
    
        cmdSelectCount = New OleDbCommand(strReader, conSMSQuiz)
        cmdSelectCount.Parameters.Add("@reqKeyword", Session("keyword"))
        cmdSelectCount.Parameters.Add("@correctAnswer", "A")
        cmdSelectCount.Parameters.Add("@close", Session("close"))
    
        conSMSQuiz.Open()
    
        counterA = cmdSelectCount.ExecuteScalar()
        
        conSMSQuiz.Close()
    
        If counterA <> 0 Then
            aPercent = ((counterA) / (lblAllCount.Text) * 100)
        End If
        
        lblA.Text = CDec(aPercent).ToString("N2") & " %"
    
    
    End Sub
    
    Sub countB()
    
    
        Dim counterB As Integer
        Dim bPercent As Double
        conSMSQuiz = New OleDbConnection(strIncomingConString)
    
        strReader = "Select Count(*) From contestants WHERE keyword=@reqKeyword AND answer=@correctAnswer AND timeDate<@close"
    
        cmdSelectCount = New OleDbCommand(strReader, conSMSQuiz)
        cmdSelectCount.Parameters.Add("@reqKeyword", Session("keyword"))
        cmdSelectCount.Parameters.Add("@correctAnswer", "B")
        cmdSelectCount.Parameters.Add("@close", Session("close"))
    
        conSMSQuiz.Open()
    
        counterB = cmdSelectCount.ExecuteScalar()
        
        conSMSQuiz.Close()
    
        If counterB <> 0 Then
            bPercent = ((counterB) / (lblAllCount.Text) * 100)
        End If
        
        lblB.Text = CDec(bPercent).ToString("N2") & " %"
    
    
    End Sub
    
    Sub countC()
    
    
        Dim counterC As Integer
        Dim cPercent As Double
        conSMSQuiz = New OleDbConnection(strIncomingConString)
    
        strReader = "Select Count(*) From contestants WHERE keyword=@reqKeyword AND answer=@correctAnswer AND timeDate<@close"
    
        cmdSelectCount = New OleDbCommand(strReader, conSMSQuiz)
        cmdSelectCount.Parameters.Add("@reqKeyword", Session("keyword"))
        cmdSelectCount.Parameters.Add("@correctAnswer", "C")
        cmdSelectCount.Parameters.Add("@close", Session("close"))
    
        conSMSQuiz.Open()
    
        
        counterC = cmdSelectCount.ExecuteScalar()
        
        conSMSQuiz.Close()
    
        If counterC <> 0 Then
            cPercent = ((counterC) / (lblAllCount.Text) * 100)
        End If
        
        lblC.Text = CDec(cPercent).ToString("N2") & " %"
    
    End Sub
    
    Sub countD()
    
    
        Dim counterD As Integer
        Dim dPercent As Double
        conSMSQuiz = New OleDbConnection(strIncomingConString)
    
        strReader = "Select Count(*) From contestants WHERE keyword=@reqKeyword AND answer=@correctAnswer AND timeDate<@close"
    
        cmdSelectCount = New OleDbCommand(strReader, conSMSQuiz)
        cmdSelectCount.Parameters.Add("@reqKeyword", Session("keyword"))
        cmdSelectCount.Parameters.Add("@correctAnswer", "D")
        cmdSelectCount.Parameters.Add("@close", Session("close"))
    
        conSMSQuiz.Open()
    
        counterD = cmdSelectCount.ExecuteScalar()
    
        conSMSQuiz.Close()
    
        If counterD <> 0 Then
            dPercent = ((counterD) / (lblAllCount.Text) * 100)
        End If
        lblD.Text = CDec(dPercent).ToString("N2") & " %"
    
    
    End Sub
    
    
    Sub btnWinner_Click(sender As Object, e As EventArgs)
    
        Dim colArrayList As ArrayList
        Dim dtrWinner As OleDbDataReader
        Dim cmdGetWinner As OleDbCommand
        Dim objItem As Object
        Dim intLowerBound, intUpperBound As Integer
    
    
        If lblDate.Text < Session("close") Then
    
    
            lblStatus.Text = "<b>" & "You cannot close this quiz yet as the closing date has not expired!" & "</b>"
    
        Else
    
            If lblcorrectCount.Text = 0 Then
    
                'close the competition status
                conSMSQuiz = New OleDbConnection(strConString)
                strUpdate = "UPDATE [competition] SET [status]=@status, [response]=@response, [requiredKeyword]=@requiredKeyword, [winner]=@winner  WHERE ([competition].[competitionID] = " & _
      "@competitionID)"
                cmdUpdate = New OleDbCommand(strUpdate, conSMSQuiz)
                cmdUpdate.Parameters.Add("@status", "closed")
                cmdUpdate.Parameters.Add("@response", lblAllCount.Text)
                cmdUpdate.Parameters.Add("@requiredKeyword", "")
                cmdUpdate.Parameters.Add("@competitionID", Session("compID"))
                cmdUpdate.Parameters.Add("@winner", Session("compID"))
    
                conSMSQuiz.Open()
                cmdUpdate.ExecuteNonQuery()
                conSMSQuiz.Close()
    
                Response.Redirect("nowinner.aspx")
    
    
                lblStatus.Text = "<b>" & "There are no correct answers in this quiz" & "</b>"
    
            Else
    
                conSMSQuiz = New OleDbConnection(strIncomingConString)
    
                strReader = "Select mobile From contestants WHERE keyword=@reqKeyword AND answer=@correctAnswer AND timeDate<@close"
    
                cmdGetWinner = New OleDbCommand(strReader, conSMSQuiz)
    
                cmdGetWinner.Parameters.Add("@reqKeyword", Session("keyword"))
                cmdGetWinner.Parameters.Add("@correctAnswer", Session("answer"))
                cmdGetWinner.Parameters.Add("@close", Session("close"))
    
                conSMSQuiz.Open()
    
                dtrWinner = cmdGetWinner.ExecuteReader()
    
                'assigns size of array based on previous count of correct answers for quiz
                colArrayList = New ArrayList(Int32.Parse(lblcorrectCount.Text))
    
                While dtrWinner.Read()
    
                    colArrayList.Add(dtrWinner("mobile"))
    
                End While
    
                ' indicates number of item in the array for testing
                '
                dtrWinner.Close()
    
                conSMSQuiz.Close()
    
                ' Set the labels for output text to the values we just read in
                intLowerBound = 0
                intUpperBound = CInt(lblcorrectCount.Text)
    
                ' decrement this value as array begins from 1, not 1 like the correct number of answers count
                intUpperBound = (intUpperBound - 1)
    
    
                ' Get the random number and display it in lblRandomNumber
                Dim intRandom As Integer
                intRandom = GetRandomNumberInRange(intLowerBound, intUpperBound)
    
                ' selects mobile number from array at index position generated by random number function
                Session("theWinner") = colArrayList(intRandom)
    
                'close the competition status, removes keyword and adds winner
                conSMSQuiz = New OleDbConnection(strConString)
                strUpdate = "UPDATE [competition] SET [status]=@status, [response]=@response, [requiredKeyword]=@requiredKeyword, [winner]=@winner WHERE ([competition].[competitionID] = " & _
      "@competitionID)"
                cmdUpdate = New OleDbCommand(strUpdate, conSMSQuiz)
                cmdUpdate.Parameters.Add("@status", "closed")
                cmdUpdate.Parameters.Add("@response", lblAllCount.Text)
                cmdUpdate.Parameters.Add("@requiredKeyword", "closed")
                cmdUpdate.Parameters.Add("@winner", Session("theWinner"))
                cmdUpdate.Parameters.Add("@competitionID", Session("compID"))
    
                conSMSQuiz.Open()
                cmdUpdate.ExecuteNonQuery()
                conSMSQuiz.Close()
    
                Response.Redirect("winner.aspx")
    
    
            End If
        End If
    
    End Sub
    
    Function GetRandomNumberInRange(intLowerBound As Integer, intUpperBound As Integer)
        Dim RandomGenerator As Random
        Dim intRandomNumber As Integer
    
    
        ' Create and init the randon number generator
        RandomGenerator = New Random()
        'RandomGenerator = New Random(DateTime.Now.Millisecond)
    
        ' Get the next random number
        intRandomNumber = RandomGenerator.Next(intLowerBound, intUpperBound + 1)
    
        ' Return the random # as the function's return value
        GetRandomNumberInRange = intRandomNumber
    End Function

</script>
<!doctype html public "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>Competition Details</title>
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
                            <a href="default.aspx">Homepage</a> &gt; <a href="competitions.aspx">My Competitions</a> &gt;&nbsp;&nbsp;<asp:Label id="lblBreadcrumb" runat="server"></asp:Label> &gt; <asp:Label id="lblBreadcrumb2" runat="server"></asp:Label>
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
                        <h2>Competition Details 
                        </h2>
                        <p class="error" align="center">
                            <asp:Label id="lblStatus" runat="server"></asp:Label>
                        </p>
                        <asp:DataGrid id="dgrdCompetition" datakeyfield="competitionID" Font-Names="Verdana" Font-Size="Smaller" Runat="Server" AutoGenerateColumns="False" CellPadding="5" BackColor="#EEFFEE">
                            <HeaderStyle font-size="XX-Small" font-bold="True" forecolor="White" backcolor="#7BCC30"></HeaderStyle>
                            <ItemStyle font-size="X-Small" font-names="Verdana"></ItemStyle>
                            <Columns>
                                <asp:BoundColumn DataField="competitionID" ReadOnly="True" HeaderText="CompetitionID"></asp:BoundColumn>
                                <asp:BoundColumn DataField="question" HeaderText="Question"></asp:BoundColumn>
                                <asp:BoundColumn DataField="answerA" HeaderText="Choice A"></asp:BoundColumn>
                                <asp:BoundColumn DataField="answerB" HeaderText="Choice B"></asp:BoundColumn>
                                <asp:BoundColumn DataField="answerC" HeaderText="Choice C"></asp:BoundColumn>
                                <asp:BoundColumn DataField="answerD" HeaderText="Choice D"></asp:BoundColumn>
                                <asp:BoundColumn DataField="correctAnswer" HeaderText="Correct Answer"></asp:BoundColumn>
                                <asp:BoundColumn DataField="prize" HeaderText="Prize"></asp:BoundColumn>
                                <asp:BoundColumn DataField="closingDate" HeaderText="Closing Date (yyyy-mm-dd HH:MM:SS)"></asp:BoundColumn>
                                <asp:BoundColumn DataField="requiredKeyword" HeaderText="Keyword Required"></asp:BoundColumn>
                                <asp:BoundColumn DataField="status" HeaderText="Quiz Status"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                        <p align="center">
                            <asp:Button id="btnWinner" onclick="btnWinner_Click" runat="server" Text="Pick a Winner"></asp:Button>
                        </p>
                        <table>
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <h2>Statistics 
                                        </h2>
                                        <p class="header">
                                            Number of Entries in this quiz: <asp:Label id="lblAllCount" runat="server"></asp:Label>
                                        </p>
                                        <p class="header">
                                            Number of Correct Answers in this quiz: <asp:Label id="lblcorrectCount" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                    <td width="50">
                                    </td>
                                    <td valign="top">
                                        <h2>Response Percentages 
                                        </h2>
                                        <p class="header">
                                            A: <asp:Label id="lblA" runat="server" forecolor="Magenta"></asp:Label>
                                        </p>
                                        <p class="header">
                                            B: <asp:Label id="lblB" runat="server" forecolor="Blue"></asp:Label>
                                        </p>
                                        <p class="header">
                                            C: <asp:Label id="lblC" runat="server" forecolor="Orange"></asp:Label>
                                        </p>
                                        <p class="header">
                                            D: <asp:Label id="lblD" runat="server" forecolor="Red"></asp:Label>
                                        </p>
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
                            <asp:LinkButton id="btnLogout" onclick="btnLogout_Click" runat="server">Log Out</asp:LinkButton>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>