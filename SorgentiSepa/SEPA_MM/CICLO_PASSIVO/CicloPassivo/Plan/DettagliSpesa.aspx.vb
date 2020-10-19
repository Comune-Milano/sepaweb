
Partial Class Contabilita_CicloPassivo_Plan_DettagliSpesa
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If IsPostBack = False Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "select codice,descrizione from siscom_mi.pf_voci where id=" & Request.QueryString("IDV")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<p style='font-family: arial; font-size: 12pt; font-weight: bold'>" & par.IfNull(myReader("codice"), "") & " - " & par.IfNull(myReader("descrizione"), "") & "</p>")
                End If
                myReader.Close()
                Response.Write("<p style='font-family: arial; font-size: 10pt; font-weight: bold'>Composizione Preventivo di bilancio " & Request.QueryString("P") & "</p>")

                Response.Write("<table style='width:100%;'>")
                Response.Write("<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>FILIALE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>LOTTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SERVIZIO</td><td style='border-bottom-style: dashed;border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO (Lordo)</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO (netto)</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>% IVA</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'></td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>% REVERSIBILITA&#39;</td></tr>")

                Dim MioColore As String = "#F5F5F5"
                'par.cmd.CommandText = "select lotti_servizi.id_servizio,pf_voci.id_piano_finanziario,lotti_servizi.descrizione as lotto,pf_voci_importo.* from siscom_mi.pf_voci,siscom_mi.lotti_servizi,siscom_mi.pf_voci_importo where pf_voci.id=pf_voci_importo.id_voce and lotti_servizi.id=pf_voci_importo.id_lotto and id_voce=" & Request.QueryString("IDV") & " order by pf_voci_importo.descrizione asc"

                par.cmd.CommandText = "select tab_filiali.nome as FILIALE,tab_servizi.descrizione as servizio,lotti_servizi.id_servizio,pf_voci.id_piano_finanziario,lotti.descrizione as lotto,LOTTI.TIPO AS TIPOLOTTO,pf_voci_importo.* " _
                & " from SISCOM_MI.TAB_FILIALI,siscom_mi.tab_servizi,siscom_mi.pf_voci, siscom_mi.lotti_servizi, siscom_mi.pf_voci_importo, siscom_mi.lotti " _
                & " where LOTTI.ID_FILIALE=" & Session.Item("ID_STRUTTURA") & " AND LOTTI.ID_FILIALE=TAB_FILIALI.ID (+) AND tab_servizi.id=lotti_servizi.id_servizio and pf_voci.id = pf_voci_importo.id_voce And lotti_servizi.id_lotto = pf_voci_importo.id_lotto And lotti.id = lotti_servizi.id_lotto " _
                & " And id_voce = " & Request.QueryString("IDV") & "  and lotti_servizi.id_servizio=pf_voci_importo.id_servizio order by  LOTTO ASC,servizio asc,pf_voci_importo.descrizione asc"

                Dim trovato As Boolean = False

                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    If CDbl(par.IfNull(myReader("valore_canone"), "0")) > 0 Or CDbl(par.IfNull(myReader("valore_consumo"), "0")) > 0 Then

                        If par.IfNull(myReader("TIPOLOTTO"), "E") = "E" Then
                            Response.Write("<tr style='background-color: " & MioColore & ";font-family: ARIAL; font-size: 9pt;'><td style=''>" & par.IfNull(myReader("FILIALE"), "---") & "</td><td style=''><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('NuovoLotto.aspx?L=1&IDP=" & par.IfNull(myReader("id_piano_finanziario"), "-1") & "&IDV=" & par.IfNull(myReader("id_voce"), "-1") & "&IDS=" & par.IfNull(myReader("id_servizio"), "-1") & "&ID=" & par.IfNull(myReader("id_lotto"), "-1") & "','Lotto','height=600,top=0,left=0,width=800');" & Chr(34) & ">" & par.IfNull(myReader("lotto"), "") & "</a></td><td style=''>" & par.IfNull(myReader("SERVIZIO"), "") & "</td><td style=''>" & par.IfNull(myReader("descrizione"), "") & "</td><td align='right'>" & Format((CDbl(par.IfNull(myReader("valore_CANONE"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CANONE"), "0")) / 100))) + (CDbl(par.IfNull(myReader("valore_CONSUMO"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CONSUMO"), "0")) / 100))), "##,##0.00") & "</td><td align='right'>" & Format(CDbl(par.IfNull(myReader("valore_CANONE"), "0")), "##,##0.00") & "</td><td align='right'>" & par.IfNull(myReader("iva_CANONE"), "0") & "</td><td align='right'></td><td align='right'></td><td align='center'>" & par.IfNull(myReader("perc_reversibilita"), "0") & "</td></tr>")
                        Else
                            Response.Write("<tr style='background-color: " & MioColore & ";font-family: ARIAL; font-size: 9pt;'><td style=''>" & par.IfNull(myReader("FILIALE"), "---") & "</td><td style=''><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('NuovoLottoImpianto.aspx?L=1&IDP=" & par.IfNull(myReader("id_piano_finanziario"), "-1") & "&IDV=" & par.IfNull(myReader("id_voce"), "-1") & "&IDS=" & par.IfNull(myReader("id_servizio"), "-1") & "&ID=" & par.IfNull(myReader("id_lotto"), "-1") & "','Lotto','height=600,top=0,left=0,width=800');" & Chr(34) & ">" & par.IfNull(myReader("lotto"), "") & "</a></td><td style=''>" & par.IfNull(myReader("SERVIZIO"), "") & "</td><td style=''>" & par.IfNull(myReader("descrizione"), "") & "</td><td align='right'>" & Format((CDbl(par.IfNull(myReader("valore_CANONE"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CANONE"), "0")) / 100))) + (CDbl(par.IfNull(myReader("valore_CONSUMO"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CONSUMO"), "0")) / 100))), "##,##0.00") & "</td><td align='right'>" & Format(CDbl(par.IfNull(myReader("valore_CANONE"), "0")), "##,##0.00") & "</td><td align='right'>" & par.IfNull(myReader("iva_CANONE"), "0") & "</td><td align='right'></td><td align='right'></td><td align='center'>" & par.IfNull(myReader("perc_reversibilita"), "0") & "</td></tr>")
                        End If

                        If MioColore = "#F5F5F5" Then
                            MioColore = "FFFFFF"
                        Else
                            MioColore = "#F5F5F5"
                        End If
                        trovato = True
                    End If
                End While
                myReader.Close()
                Response.Write("</table>")

                If trovato = False Then
                    Response.Write("<p style='font-family: arial; font-size: 10pt; font-weight: bold;'>Nessun dettaglio per questa voce.<br/>La voce potrebbe non essere associata al patrimonio e/o servizi.</p>")
                End If





                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<p style='font-family: arial; font-size: 12pt; font-weight: bold; color: #FF0000'>ERRORE: " & ex.Message & "</p>")
            End Try

            'Visualizza(Request.QueryString("id"), Request.QueryString("P"))
        End If
    End Sub
End Class
