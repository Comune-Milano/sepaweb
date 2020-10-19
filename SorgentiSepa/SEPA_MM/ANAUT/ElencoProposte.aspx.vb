
Partial Class ANAUT_ElencoProposte
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                PAR.OracleConn.Open()
                par.SettaCommand(par)
                Dim COLORE As String = "#E5E5E5"


                lblElenco.Text = "<table>"
                lblElenco.Text = lblElenco.Text & "<tr style='font-family: Arial; font-size: 10pt; font-weight: bold'><td>Protocollo</td><td>Nominativo</td><td>Data Proposta</td><td>Esito</td><td>Data Esito</td><td>Lettera</td></tr>"


                PAR.cmd.CommandText = "select utenza_comp_nucleo.cognome,utenza_comp_nucleo.nome,utenza_dichiarazioni.*,utenza_prop_decadenza.DATA_PROPOSTA,DECODE(utenza_prop_decadenza.ESITO,0,'NON DEFINITO',1,'CONFERMATA',2,'RESPINTA') AS VALUTAZIONE,utenza_prop_decadenza.DATA_ESITO from utenza_dichiarazioni,utenza_comp_nucleo,utenza_prop_decadenza where utenza_comp_nucleo.progr=0 and utenza_comp_nucleo.id_dichiarazione=utenza_dichiarazioni.id and utenza_dichiarazioni.id=utenza_prop_decadenza.id_dichiarazione order by utenza_prop_decadenza.id desc"
                Dim myRec As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myRec.Read
                    lblElenco.Text = lblElenco.Text & "<tr bgcolor='" & COLORE & "' style='font-family: Arial; font-size: 9pt'><td>" & PAR.IfNull(myRec("PG"), "") & "</td><td>" & PAR.IfNull(myRec("COGNOME"), "") & " " & PAR.IfNull(myRec("NOME"), "") & "</td><td>" & PAR.FormattaData(PAR.IfNull(myRec("DATA_PROPOSTA"), "")) & "</td><td>" & myRec("VALUTAZIONE") & "</td><td>" & PAR.FormattaData(PAR.IfNull(myRec("DATA_ESITO"), "")) & "</td><td><a href='LetteraPropDec.aspx?ID=" & PAR.IfNull(myRec("id"), "-1") & "' target='_blank'>Visualizza</a></tr>"
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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


End Class
