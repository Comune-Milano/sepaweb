
Partial Class PED_DatiMisure
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreID As Long
    Dim sValoreUI As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        sValoreID = Request.QueryString("ID")
        sValoreUI = Request.QueryString("UI")

        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Response.Write("<p><font face='Arial'>Codice U.I.: <b>" & sValoreUI & " - Misurazioni</b></font></p>")
            Response.Write("<table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#111111' width='100%' id='AutoNumber1'>")
            Response.Write("<tr>")
            Response.Write("<td width='40%'><b><font face='Arial' size='1'>Descrizione</font></b></td>")
            Response.Write("<td width='60%'><b><font face='Arial' size='1'>Valore</font></b></td>")
            Response.Write("</tr>")


            par.cmd.CommandText = "select tipologia_dimensioni.descrizione as ""d1"",dimensioni.valore from  siscom_mi.tipologia_dimensioni,siscom_mi.dimensioni WHERE dimensioni.cod_tipologia=tipologia_dimensioni.cod (+) and dimensioni.ID_unita_immobiliare=" & sValoreID
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read()
                Response.Write("<tr>")
                Response.Write("<td width='40%'><font face='Arial' size='1'>" & par.IfNull(myReader1("d1"), "") & "</font></td>")
                Response.Write("<td width='60%'><font face='Arial' size='1'>" & par.IfNull(myReader1("valore"), "") & "</font></td>")
                Response.Write("</tr>")
            Loop

            myReader1.Close()
            Response.Write("</table>")
            par.OracleConn.Close()



        Catch ex As Exception
            Response.Write("ERRORE " & ex.Message)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
        End Try
    End Sub
End Class
