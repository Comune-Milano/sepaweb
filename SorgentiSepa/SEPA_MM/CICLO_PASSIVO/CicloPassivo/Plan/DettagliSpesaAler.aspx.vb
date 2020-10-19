
Partial Class Contabilita_CicloPassivo_Plan_DettagliSpesa
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim tott As Double = 0

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

                '

                Dim trovato As Boolean = False

                Dim MioColore As String = "#F5F5F5"
                par.cmd.CommandText = "select * from siscom_mi.pf_voci_importo where id_voce=" & Request.QueryString("IDV")
                myReader = par.cmd.ExecuteReader
                If myReader.HasRows = False Then

                    Response.Write("<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>FILIALE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO LORDO</td><td align='right' style='border-bottom-style: dashed;border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO NETTO</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IVA</td></tr>")
                    If Request.QueryString("IDS") = "" Then
                        par.cmd.CommandText = "SELECT PF_VOCI.DESCRIZIONE,TAB_FILIALI.nome AS FILIALE,PF_VOCI.id_piano_finanziario,PF_VOCI_STRUTTURA.* " _
                                          & "FROM SISCOM_MI.TAB_FILIALI, siscom_mi.PF_VOCI, siscom_mi.PF_VOCI_STRUTTURA " _
                                          & "WHERE PF_VOCI.ID = PF_VOCI_STRUTTURA.id_voce And id_voce = " & Request.QueryString("IDV") _
                                          & " And TAB_FILIALI.ID = PF_VOCI_STRUTTURA.id_struttura " _
                                          & "ORDER BY  TAB_FILIALI.nome ASC"
                    Else
                        par.cmd.CommandText = "SELECT PF_VOCI.DESCRIZIONE,TAB_FILIALI.nome AS FILIALE,PF_VOCI.id_piano_finanziario,PF_VOCI_STRUTTURA.* " _
                  & "FROM SISCOM_MI.TAB_FILIALI, siscom_mi.PF_VOCI, siscom_mi.PF_VOCI_STRUTTURA " _
                  & "WHERE PF_VOCI.ID = PF_VOCI_STRUTTURA.id_voce And id_voce = " & Request.QueryString("IDV") _
                  & " And TAB_FILIALI.ID = PF_VOCI_STRUTTURA.id_struttura AND PF_VOCI_STRUTTURA.ID_STRUTTURA= " & Request.QueryString("IDS") _
                  & " ORDER BY  TAB_FILIALI.nome ASC"
                    End If
                    myReader = par.cmd.ExecuteReader()


                    While myReader.Read

                        Response.Write("<tr style='background-color: " & MioColore & ";font-family: ARIAL; font-size: 9pt;'><td style=''>" & par.IfNull(myReader("FILIALE"), "---") & "</td><td align='right'>" & Format((CDbl(par.IfNull(myReader("valore_LORDO"), "0"))), "##,##0.00") & "</td><td align='right'>" & Format(CDbl(par.IfNull(myReader("valore_NETTO"), "0")), "##,##0.00") & "</td><td align='right'>" & par.IfNull(myReader("iva"), "0") & "</td></tr>")

                        If MioColore = "#F5F5F5" Then
                            MioColore = "FFFFFF"
                        Else
                            MioColore = "#F5F5F5"
                        End If
                        trovato = True

                    End While

                Else

                    Response.Write("<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>FILIALE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>LOTTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SERVIZIO</td><td style='border-bottom-style: dashed;border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO (Lordo)</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO (netto)</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>% IVA</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'></td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>% REVERSIBILITA&#39;</td></tr>")


                    If Request.QueryString("IDS") = "" Then
                        par.cmd.CommandText = "SELECT TAB_FILIALI.nome AS FILIALE,TAB_SERVIZI.descrizione AS servizio,LOTTI_SERVIZI.id_servizio,PF_VOCI.id_piano_finanziario,LOTTI.descrizione AS lotto,PF_VOCI_IMPORTO.*,LOTTI.ID AS IDL  " _
                                            & "FROM SISCOM_MI.TAB_FILIALI, siscom_mi.TAB_SERVIZI, siscom_mi.PF_VOCI, siscom_mi.LOTTI_SERVIZI, siscom_mi.PF_VOCI_IMPORTO, siscom_mi.LOTTI " _
                                            & "WHERE TAB_FILIALI.ID=LOTTI.ID_FILIALE AND TAB_SERVIZI.ID=LOTTI_SERVIZI.id_servizio AND " _
                                            & "PF_VOCI.ID = PF_VOCI_IMPORTO.id_voce AND LOTTI_SERVIZI.id_lotto = PF_VOCI_IMPORTO.id_lotto AND LOTTI.ID = LOTTI_SERVIZI.id_lotto  AND " _
                                            & "PF_VOCI_IMPORTO.id_voce = " & Request.QueryString("IDV") & "  AND LOTTI_SERVIZI.id_servizio=PF_VOCI_IMPORTO.id_servizio " _
                                            & "ORDER BY  TAB_FILIALI.NOME ASC,LOTTO ASC,servizio ASC,PF_VOCI_IMPORTO.descrizione ASC"
                    Else
                        par.cmd.CommandText = "SELECT TAB_FILIALI.nome AS FILIALE,TAB_SERVIZI.descrizione AS servizio,LOTTI_SERVIZI.id_servizio,PF_VOCI.id_piano_finanziario,LOTTI.descrizione AS lotto,PF_VOCI_IMPORTO.*,LOTTI.ID AS IDL  " _
                                           & "FROM SISCOM_MI.TAB_FILIALI, siscom_mi.TAB_SERVIZI, siscom_mi.PF_VOCI, siscom_mi.LOTTI_SERVIZI, siscom_mi.PF_VOCI_IMPORTO, siscom_mi.LOTTI " _
                                           & "WHERE tab_filiali.id=" & Request.QueryString("IDS") & " and TAB_FILIALI.ID=LOTTI.ID_FILIALE AND TAB_SERVIZI.ID=LOTTI_SERVIZI.id_servizio AND " _
                                           & "PF_VOCI.ID = PF_VOCI_IMPORTO.id_voce AND LOTTI_SERVIZI.id_lotto = PF_VOCI_IMPORTO.id_lotto AND LOTTI.ID = LOTTI_SERVIZI.id_lotto  AND " _
                                           & "PF_VOCI_IMPORTO.id_voce = " & Request.QueryString("IDV") & "  AND LOTTI_SERVIZI.id_servizio=PF_VOCI_IMPORTO.id_servizio " _
                                           & "ORDER BY  TAB_FILIALI.NOME ASC,LOTTO ASC,servizio ASC,PF_VOCI_IMPORTO.descrizione ASC"
                    End If


                    myReader = par.cmd.ExecuteReader()
                    While myReader.Read
                        If CDbl(par.IfNull(myReader("valore_canone"), "0")) > 0 Or CDbl(par.IfNull(myReader("valore_consumo"), "0")) > 0 Then
                            Response.Write("<tr style='background-color: " & MioColore & ";font-family: ARIAL; font-size: 9pt;'><td style=''>" & par.IfNull(myReader("FILIALE"), "---") & "</td><td style=''>" & par.IfNull(myReader("lotto"), "") & "</td><td style=''>" & par.IfNull(myReader("SERVIZIO"), "") & "</td><td style=''>" & par.IfNull(myReader("descrizione"), "") & "</td><td align='right'><a href=""javascript:window.open('LinkDettaglioSpesa.aspx?idl=" & par.IfNull(myReader("idl"), 0) & "&idvs=" & par.IfNull(myReader("id"), 0) & "&idv=" & Request.QueryString("idv") & "&idp=" & par.IfNull(myReader("id_piano_finanziario"), 0) & "&ids=" & par.IfNull(myReader("id_SERVIZIO"), 0) & "&p=" & Request.QueryString("P") & "','_blank','resizable=yes,height=400,width=800,top=0,left=100,scrollbars=yes,location=no,toolbar=no,directories=no,menubar=no,titlebar=no,status=no');void(0);"">" & Format((CDbl(par.IfNull(myReader("valore_CANONE"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CANONE"), "0")) / 100))) + (CDbl(par.IfNull(myReader("valore_CONSUMO"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CONSUMO"), "0")) / 100))), "##,##0.00") & "</a></td><td align='right'>" & Format(CDbl(par.IfNull(myReader("valore_CANONE"), "0")), "##,##0.00") & "</td><td align='right'>" & par.IfNull(myReader("iva_CANONE"), "0") & "</td><td align='right'></td><td align='right'></td><td align='center'>" & par.IfNull(myReader("perc_reversibilita"), "0") & "</td></tr>")

                            tott = tott + (CDbl(par.IfNull(myReader("valore_CANONE"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CANONE"), "0")) / 100))) + (CDbl(par.IfNull(myReader("valore_CONSUMO"), "0")) * (1 + (CDbl(par.IfNull(myReader("iva_CONSUMO"), "0")) / 100)))
                            If MioColore = "#F5F5F5" Then
                                MioColore = "FFFFFF"
                            Else
                                MioColore = "#F5F5F5"
                            End If
                            trovato = True
                        End If
                    End While

                End If

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
