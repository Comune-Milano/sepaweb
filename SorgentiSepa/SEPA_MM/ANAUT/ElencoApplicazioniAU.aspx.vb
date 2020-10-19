
Partial Class ANAUT_ElencoApplicazioniAU
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)
                Dim COLORE As String = "#E5E5E5"


                lblElenco.Text = "<table width='90%'>"
                lblElenco.Text = lblElenco.Text & "<tr style='font-family: Arial; font-size: 10pt; font-weight: bold'><td>Descrizione</td><td>A.U.</td><td>File</td></tr>"


                PAR.cmd.CommandText = "SELECT utenza_applicazioni.*,utenza_bandi.descrizione as au FROM UTENZA_BANDI,UTENZA_APPLICAZIONI WHERE UTENZA_BANDI.ID=UTENZA_APPLICAZIONI.ID_AU ORDER BY UTENZA_APPLICAZIONI.DESCRIZIONE DESC"
                Dim myRec As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myRec.Read
                    lblElenco.Text = lblElenco.Text & "<tr bgcolor='" & COLORE & "' style='font-family: Arial; font-size: 9pt'><td>" & PAR.IfNull(myRec("DESCRIZIONE"), "") & " del " & PAR.FormattaData(Mid(PAR.IfNull(myRec("DATA_ORA"), ""), 1, 8)) & " " & Mid(PAR.IfNull(myRec("DATA_ORA"), ""), 9, 2) & ":" & Mid(PAR.IfNull(myRec("DATA_ORA"), ""), 11, 2) & ":" & Mid(PAR.IfNull(myRec("DATA_ORA"), ""), 13, 2) & "</td><td>" & PAR.IfNull(myRec("AU"), "") & "</td><td><a href='../ALLEGATI/ANAGRAFE_UTENZA/APPLICAZIONI/" & PAR.IfNull(myRec("NOME_FILE"), "") & "' target='_blank'>download</a></tr>"
                    If COLORE = "#E5E5E5" Then
                        COLORE = "#FFFFFF"
                    Else
                        COLORE = "#E5E5E5"
                    End If
                Loop
                myRec.Close()
                lblElenco.Text = lblElenco.Text & "</table>"

                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
