<%@ Page Language="VB" Debug="true" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    Sub Page_Load()
    
        Dim mobNo, tmeDate, txtkeyword, txtAnswer As String
    
        'get variables from SMS server
        mobNo = Request.Form("sender")
        tmeDate = Request.Form("time")
        txtAnswer = Request.Form("message")
        txtkeyword = Request.Form("keyword")
    
    
        'database connection settings
        Dim conSMS As OleDbConnection
        Dim strInsert, strConString As String
        Dim cmdInsert As OleDbCommand
    
    
        ' assign connection string from web.config file
        strConString = ConfigurationSettings.AppSettings("IncomingConnectionString")
        conSMS = New OleDbConnection(strConString)
             
        strInsert = "INSERT into contestants ( answer, mobile, timeDate, keyword ) Values ( @answer, @mobile, @timeDate, @keyword )"
        conSMS.Open()
        cmdInsert = New OleDbCommand(strInsert, conSMS)
        cmdInsert.Parameters.Add("@answer", txtAnswer)
        cmdInsert.Parameters.Add("@mobile", mobNo)
        cmdInsert.Parameters.Add("@timeDate", tmeDate)
        cmdInsert.Parameters.Add("@keyword", txtkeyword)
    
    
        cmdInsert.ExecuteNonQuery()
        conSMS.Close()
    
    End Sub

</script>
<html>
<head>
</head>
<body>
    <form runat="server">
        <!-- Insert content here -->
    </form>
</body>
</html>
