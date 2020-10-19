
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_DivisioneManuale1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If IsPostBack = False Then
            AggiornaDati()
        End If
    End Sub

    Protected Sub imgAggiorna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAggiorna.Click
        AggiornaDati()
    End Sub

    Private Function AggiornaDati()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim Vlordo As String = "0,00"
            Dim IDP As String = "0"

            par.cmd.CommandText = "select codice,descrizione,ID_PIANO_FINANZIARIO,valore_lordo_aler from siscom_mi.pf_voci,siscom_mi.pf_voci_STRUTTURA where PF_VOCI_STRUTTURA.ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") & " AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND id=" & Request.QueryString("IDV")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Response.Write("<p style='font-family: arial; font-size: 12pt; font-weight: bold'>" & par.IfNull(myReader("codice"), "") & " - " & par.IfNull(myReader("descrizione"), "") & "</p>")
                IDP = par.IfNull(myReader("ID_PIANO_FINANZIARIO"), "0")
                Vlordo = par.IfNull(myReader("valore_lordo_aler"), "0,00")
            End If
            myReader.Close()

            Response.Write("<table style='width:100%;'>")
            Response.Write("<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>FILIALE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>LOTTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SERVIZIO</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO TOTALE LORDO</td></tr>")

            Dim MioColore As String = "#F5F5F5"
            Dim NomeServizio As String = ""
            Dim NomeLotto As String = ""
            Dim Totale As String = "0,00"
            Dim NomeFiliale As String = ""
            Dim totaleTotale As Double = 0

            'If Session.Item("LIVELLO") = "1" Then
            'par.cmd.CommandText = "select distinct id_lotto,id_servizio from siscom_mi.pf_voci_importo where id_voce=" & Request.QueryString("IDV")
            'Else
            'par.cmd.CommandText = "select distinct id_lotto,id_servizio from siscom_mi.pf_voci_importo where id_lotto in (select lotti.id from siscom_mi.lotti,siscom_mi.tab_filiali,operatori where tab_filiali.id=lotti.id_filiale and operatori.id_caf=tab_filiali.id and operatori.id=" & Session.Item("ID_OPERATORE") & ") and id_voce=" & Request.QueryString("IDV")
            par.cmd.CommandText = "select distinct id_lotto,id_servizio from siscom_mi.pf_voci_importo where id_lotto in (select lotti.id from siscom_mi.lotti WHERE ID_FILIALE=" & Session.Item("ID_STRUTTURA") & ") and id_voce=" & Request.QueryString("IDV")
            'End If
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                par.cmd.CommandText = "select * from siscom_mi.tab_servizi where id=" & par.IfNull(myReader("id_servizio"), "0")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    NomeServizio = par.IfNull(myReader1("descrizione"), "")
                End If
                myReader1.Close()

                par.cmd.CommandText = "select * from siscom_mi.lotti where id=" & par.IfNull(myReader("id_lotto"), "0")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    NomeLotto = par.IfNull(myReader1("descrizione"), "")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT SUM((valore_canone+(valore_canone*iva_canone)/100)+(valore_consumo+(valore_consumo*iva_consumo)/100)) as TotaleLordo FROM siscom_mi.pf_voci_importo WHERE id_servizio=" & par.IfNull(myReader("id_servizio"), "0") & " and id_lotto=" & par.IfNull(myReader("id_lotto"), "0") & " and id_voce=" & Request.QueryString("IDV")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Totale = par.IfNull(myReader1("TotaleLordo"), "0,00")
                    totaleTotale = totaleTotale + CDbl(par.IfNull(myReader1("TotaleLordo"), "0"))
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT tab_filiali.nome from siscom_mi.tab_filiali,siscom_mi.lotti where lotti.id_filiale=tab_filiali.id and lotti.id=" & par.IfNull(myReader("id_lotto"), "0")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    NomeFiliale = par.IfNull(myReader1("nome"), "")
                End If
                myReader1.Close()




                ' Response.Write("<tr style='background-color: " & MioColore & ";font-family: ARIAL; font-size: 9pt;'><td style=''>" & NomeFiliale & "</td><td style=''>" & NomeLotto & "</td><td style=''>" & NomeServizio & "</td><td align='right'><a href=" & Chr(34) & "javascript:void(0)"  & Chr(34) & " onclick=" & Chr(34) & "window.showModalDialog('VociServizio.aspx?APP=" & par.Cripta(Vlordo) & "&PR=1&IDP=" & IDP & "&IDL=" & par.IfNull(myReader("id_lotto"), "0") & "&IDV=" & Request.QueryString("IDV") & "&IDS=" & par.IfNull(myReader("id_servizio"), "0") & "&SE=" & NomeServizio & "',window,'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');document.getElementById('imgAggiorna').style.visibility='visible';" & Chr(34) & ">" & Format(CDbl(Totale), "##,##0.00") & "</a></td></tr>")
                Response.Write("<tr style='background-color: " & MioColore & ";font-family: ARIAL; font-size: 9pt;'><td style=''>" & NomeFiliale & "</td><td style=''>" & NomeLotto & "</td><td style=''>" & NomeServizio & "</td><td align='right'>" & Format(CDbl(Totale), "##,##0.00") & "</td><td><img alt='Visualizza e modifica'  style='cursor: pointer' src='Immagini/Edit.png' onclick=" & Chr(34) & "window.showModalDialog('VociServizio.aspx?APP=" & par.Cripta(Vlordo) & "&PR=1&IDP=" & IDP & "&IDL=" & par.IfNull(myReader("id_lotto"), "0") & "&IDV=" & Request.QueryString("IDV") & "&IDS=" & par.IfNull(myReader("id_servizio"), "0") & "&SE=" & NomeServizio & "',window,'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');document.getElementById('Contenitore').style.visibility='visible';" & Chr(34) & "/></td></tr>")


                If MioColore = "#F5F5F5" Then
                    MioColore = "FFFFFF"
                Else
                    MioColore = "#F5F5F5"
                End If

            End While
            myReader.Close()

            Response.Write("<tr style='background-color: " & MioColore & ";font-family: ARIAL; font-size: 9pt;'><td style=''></td><td style=''></td><td style=''></td><td align='right'><b>" & Format(CDbl(totaleTotale), "##,##0.00") & "</b></td></tr>")


            Response.Write("</table>")


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<p style='font-family: arial; font-size: 12pt; font-weight: bold; color: #FF0000'>ERRORE: " & ex.Message & "</p>")
        End Try
    End Function
End Class
