<%@ Page Language="VB" Debug="True" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    Dim dtmDate As DateTime
    Dim userName As String
    
    
    Sub Page_Load()
    
        If Not IsPostBack Then
    
            'assign welcome label
            Dim userID As FormsIdentity
    
            'user status
            If User.Identity.IsAuthenticated Then
    
                userID = User.Identity
                userName = userID.Name
    
                lblWelcome.Text = "Logged in as " + userName
    
            End If
    
            Session("creator") = userName
    
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
    
    Sub btnCreate_Click(sender As Object, e As EventArgs)
        If IsValid Then
    
            If lblDate.Text > txtCloseDate.Text Then
    
                lblMessage.Text = "*The closing date you have selected has already passed, please choose an alternative date!"
    
            Else
    
                'database connection settings
                Dim conSMSQuiz As OleDbConnection
                Dim strInsert, strConString, strKeywordChecker, strReader, strReadKeys As String
                Dim cmdInsert, cmdKeyword, cmdShowKeys As OleDbCommand
    
                ' assign connection string from web.config file
                strConString = ConfigurationSettings.AppSettings("ConnectionString")
                conSMSQuiz = New OleDbConnection(strConString)
    
                strReader = "SELECT requiredKeyword FROM competition WHERE requiredKeyword=@requiredKeyword"
    
                cmdKeyword = New OleDbCommand(strReader, conSMSQuiz)
    
                cmdKeyword.Parameters.Add("@requiredKeyword", drpKeyword.SelectedItem.Text)
    
    
                conSMSQuiz.Open()
                lblMessage.Text = "Validating..."
                strKeywordChecker = cmdKeyword.ExecuteScalar()
    
                If strKeywordChecker <> drpKeyword.SelectedItem.Text Then
    
                    strInsert = "INSERT into competition ( question, answerA, answerB, answerC, answerD, correctAnswer, prize, closingDate, requiredKeyword, status, creatorID, category ) Values ( @question, @answerA, @answerB, @answerC, @answerD, @correctAnswer, @prize, @closingDate, @requiredKeyword, @status, @creatorID, @category  )"
    
                    cmdInsert = New OleDbCommand(strInsert, conSMSQuiz)
                    cmdInsert.Parameters.Add("@question", txtQuestion.Text)
                    cmdInsert.Parameters.Add("@answerA", txtAnswer1.Text)
                    cmdInsert.Parameters.Add("@answerB", txtAnswer2.Text)
                    cmdInsert.Parameters.Add("@answerC", txtAnswer3.Text)
                    cmdInsert.Parameters.Add("@answerD", txtAnswer4.Text)
                    cmdInsert.Parameters.Add("@correctAnswer", drpAnswer.SelectedItem.Text)
                    cmdInsert.Parameters.Add("@prize", txtPrize.Text)
                    cmdInsert.Parameters.Add("@closingDate", DateTime.Parse(txtCloseDate.Text))
                    cmdInsert.Parameters.Add("@requiredKeyword", drpKeyword.SelectedItem.Text)
                    cmdInsert.Parameters.Add("@status", "open")
                    cmdInsert.Parameters.Add("@creatorID", Session("creator"))
                    cmdInsert.Parameters.Add("@category", drpCategory.SelectedItem.Value)
    
                    cmdInsert.ExecuteNonQuery()
    
                    lblMessage.Text = "Quiz Added!"
    
                Else
    
                    'keyword already exists!!!
                    lblMessage.Text = "The Keyword you selected is already being used in another competition, please select an alternative!"
    
    
                End If
    
                'closes all possible connections
                conSMSQuiz.Close()
    
            End If
    
    
        End If
    
    End Sub
    
    Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs)
        Dim dtmSelDate As DateTime
    
        For Each dtmSelDate In Calendar1.SelectedDates
            txtCloseDate.Text = dtmSelDate.ToString("u")
        Next
    End Sub

</script>
<!doctype html public "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>Create a New Quiz</title>
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
                            <a href="default.aspx">Homepage</a> &gt; Create Quiz 
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
                        <h2>Create a New Quiz 
                        </h2>
                        <table border="0">
                            <tbody>
                                <tr>
                                    <td align="middle" colspan="6">
                                        <p class="error">
                                            <asp:Label id="lblMessage" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <p class="header">
                                            Question: 
                                        </p>
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox id="txtQuestion" runat="server" MaxLength="255" TextMode="MultiLine" Width="468px" Height="67px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuestion" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <p class="header">
                                            Answer A: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtAnswer1" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnswer1" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                    <td align="right">
                                        <p class="header">
                                            Category: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:DropDownList id="drpCategory" runat="server">
                                            <asp:ListItem Value="1">General</asp:ListItem>
                                            <asp:ListItem Value="2">Sports</asp:ListItem>
                                            <asp:ListItem Value="3">Television</asp:ListItem>
                                            <asp:ListItem Value="4">Film</asp:ListItem>
                                            <asp:ListItem Value="5">People</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <p class="header">
                                            Answer B: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtAnswer2" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtAnswer2" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                    <td align="right">
                                        <p class="header">
                                            SMS Keyword: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:DropDownList id="drpKeyword" runat="server" Width="92px">
                                            <asp:ListItem Value="UOG1">UOG1</asp:ListItem>
                                            <asp:ListItem Value="UOG2">UOG2</asp:ListItem>
                                            <asp:ListItem Value="UOG3">UOG3</asp:ListItem>
                                            <asp:ListItem Value="UOG4">UOG4</asp:ListItem>
                                            <asp:ListItem Value="UOG5">UOG5</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <p class="status">
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <p class="header">
                                            Answer C: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtAnswer3" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtAnswer3" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                    <td align="right">
                                        <p class="header">
                                        </p>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <p class="status">
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <p class="header">
                                            Answer D: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtAnswer4" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtAnswer4" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                    <td align="right">
                                        <p class="header">
                                            Prize: 
                                        </p>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtPrize" runat="server" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p class="status">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ControlToValidate="txtPrize" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p class="header">
                                            Correct Answer: 
                                        </p>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList id="drpAnswer" runat="server">
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="C">C</asp:ListItem>
                                            <asp:ListItem Value="D">D</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <p class="header">
                                            Closing Date:&nbsp; 
                                        </p>
                                    </td>
                                    <td align="middle">
                                        <asp:Calendar id="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                                    </td>
                                    <td>
                                        <p class="header">
                                            <asp:TextBox id="txtCloseDate" runat="server" Width="133px" ReadOnly="True"></asp:TextBox>
                                        </p>
                                        <p class="header">
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="middle" colspan="6">
                                        <asp:Button id="btnCreate" onclick="btnCreate_Click" runat="server" Text="Create Quiz"></asp:Button>
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
