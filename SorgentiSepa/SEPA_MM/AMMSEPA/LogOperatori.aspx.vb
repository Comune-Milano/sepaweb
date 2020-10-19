
Partial Class AMMSEPA_LogOperatori
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            Response.Write("<table style='width:100%;'>")
            Response.Write("<tr><td>OPERATORE</td><td>DATA E ORA</td><td>EVENTO</td><td>TESTO</td><td>OPERATORE MODIFICATO</td></tr>")


            par.cmd.CommandText = "select eventi_operatori.*,OPERATORI.OPERATORE from OPERATORI,eventi_operatori WHERE OPERATORI.ID=EVENTI_OPERATORI.ID_OPERATORE order by data_ora desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                Response.Write("<tr><td>" & par.IfNull(myReader("OPERATORE"), "") & "</td><td>" & par.FormattaData(Mid(par.IfNull(myReader("DATA_ORA"), ""), 1, 8)) & "</td><td>" & par.IfNull(myReader("FUNZIONE"), "") & "</td><td><a href='TestoEvento.aspx?t=" & Cripta(Mid(par.IfNull(myReader("TESTO"), ""), 21, 4000)) & "' target='_blank'>Visualizza</a></td><td>" & par.IfNull(myReader("OPERATORE_MOD"), "") & "</td></tr>")
            Loop
            Response.Write("</table>")
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub

    Function Cripta(ByVal testo As String) As String
        Dim EncStr As String
        Dim EncKey As String
        Dim TempEncKey As String
        Dim EncLen As Integer
        Dim EncPos As Integer
        Dim EncKeyPos As Integer
        Dim tempChar As String
        Dim TA As Integer, TB As Integer, TC As Integer
        Dim x As Integer
        Dim JustChanged As Boolean

        JustChanged = True
        Cripta = testo
        TempEncKey = "SX"

        EncKey = ""
        EncStr = ""
        EncPos = 1
        EncKeyPos = 1

        For x = 1 To Len(TempEncKey)
            EncKey = EncKey & Asc(Mid$(TempEncKey, x, 1))
        Next

        EncLen = Len(EncKey)

        For x = 1 To Len(testo)
            TB = Asc(Mid$(EncKey, EncKeyPos, 1))
            EncKeyPos = EncKeyPos + 1
            If EncKeyPos > EncLen Then EncKeyPos = 1
            TA = Asc(Mid$(testo, x, 1))
            TC = TB Xor TA
            tempChar = Hex$(TC)
            If Len(tempChar) < 2 Then tempChar = "0" & tempChar
            EncStr = EncStr & tempChar
        Next
        Cripta = EncStr
    End Function


    
End Class
