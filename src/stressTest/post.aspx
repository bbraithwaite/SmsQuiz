<%@ Page Language="VB" Debug="false" %>
<%@ import Namespace="System.Data.OleDb" %>
<script runat="server">
    
    private Generator As System.Random = New System.Random()
    
    Sub Page_Load()
    
        Dim mobNo, tmeDate, txtkeyword, txtAnswer As String
        
        'get variables from SMS server
        For i As Integer = 0 To 10
            mobNo = mobNo & GetRandom(0, 9)
        Next
        
        tmeDate = "1-Oct-2012"
        txtAnswer = GetRandomAnswer()
        txtkeyword = "UOG2"
    
    
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
    
    Public Function GetRandomAnswer() As String
        
        Dim number As Integer = GetRandom(1, 4)
        
        Select Case number
            Case 1
                Return "A"
            Case 2
                Return "B"
            Case 3
                Return "C"
            Case 4
                Return "D"
        End Select
        
        Throw New Exception("Error")
        
    End Function
    
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Return Generator.Next(Min, Max)
    End Function

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