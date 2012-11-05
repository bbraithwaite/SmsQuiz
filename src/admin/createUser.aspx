<%@ Page Language="VB" Debug="True" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    Sub cmdCreate_Click(sender As Object, e As EventArgs)
    
        If IsValid Then
            Dim conSMSQuiz As OleDbConnection
            Dim strConString, strInsert As String
            Dim cmdInsert As OleDbCommand
    
            ' assign connection string from web.config file
            strConString = ConfigurationSettings.AppSettings("ConnectionString")
            conSMSQuiz = New OleDbConnection(strConString)
             
            strInsert = "INSERT into admin ( [username], [password] ) Values ( @username, @password )"
            conSMSQuiz.Open()
            cmdInsert = New OleDbCommand(strInsert, conSMSQuiz)
            cmdInsert.Parameters.Add("@userName", txtUsername.Text)
            cmdInsert.Parameters.Add("@password", FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "MD5"))
    
            cmdInsert.ExecuteNonQuery()
            conSMSQuiz.Close()
        End If
    
    End Sub

</script>
<!doctype html public "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <title>Login</title>
    <link href="../style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td width="10" bgcolor="#7bcc30">
                        <img height="64" alt="" src="../images/stripLeft.gif" width="10" /> 
                    </td>
                    <td align="left" width="135" bgcolor="#7bcc30" height="64">
                        <img height="44" alt="SMS Quiz Logo" src="../images/logo.gif" width="135" /> 
                    </td>
                    <td valign="bottom" align="right" bgcolor="#7bcc30">
                        <p class="top">
                        </p>
                    </td>
                    <td width="10" bgcolor="#7bcc30">
                        <img height="64" alt="" src="../images/stripRight.gif" width="10" /> 
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#7bcc30" colspan="4">
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
                            Login 
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
                        <p class="header" align="left">
                            <table height="250" width="400" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <p class="header">
                                                Username: 
                                            </p>
                                        </td>
                                        <td>
                                            <p align="left">
                                                <asp:TextBox id="txtUsername" runat="server"></asp:TextBox>
                                            </p>
                                        </td>
                                        <td>
                                            <p class="status">
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <p class="header">
                                                Password: 
                                            </p>
                                        </td>
                                        <td valign="top">
                                            <p align="left">
                                                <asp:TextBox id="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                            </p>
                                        </td>
                                        <td>
                                            <p class="status">
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td valign="top">
                                            <asp:Button id="cmdCreate" onclick="cmdCreate_Click" runat="server" Text="Create"></asp:Button>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td valign="top">
                                            <p class="status" align="left">
                                                <asp:Label id="lblStatus" runat="server"></asp:Label>
                                            </p>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>