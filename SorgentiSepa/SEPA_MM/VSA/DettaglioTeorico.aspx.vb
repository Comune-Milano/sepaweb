
Partial Class VSA_DettaglioTeorico
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim TotImporto As Double = 0
    Dim TotContabile As Double = 0
    Dim TotPagato As Double = 0
    Dim TotMorosita As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaDettaglioSaldo()
        End If
    End Sub

    Private Sub CaricaDettaglioSaldo()
        Try
            '******APERTURA CONNESSIONE*****

            If Request.QueryString("ID") <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim testoTabella As String = ""
                Dim COLORE As String = "#E6E6E6"
                Dim EmessoContabile As Double = 0
                Dim ImportoBolletta As Double = 0
                Dim Contatore As Integer = 0
                Dim Nbolletta As String
                Dim GiorniRitardo As String = 0
                Dim IDANA As String = ""

                par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",anagrafica.id as id_ana,COD_CONTRATTO FROM DOMANDE_BANDO_VSA,SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.INTESTATARI_RAPPORTO WHERE RAPPORTI_UTENZA.COD_CONTRATTO=DOMANDE_BANDO_VSA.CONTRATTO_NUM AND DOMANDE_BANDO_VSA.ID = " & Request.QueryString("ID") & " AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID = INTESTATARI_RAPPORTO.ID_ANAGRAFICA"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.LblIntestazione.Text = myReader("INTESTATARIO") & " COD. CONTRATTO " & myReader("COD_CONTRATTO") '& " - " & UltimoPagam
                    IDANA = myReader("id_ana")
                End If

                myReader.Close()
                testoTabella = "<table cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>ANNO COMPETENZA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>NUMERO BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>NUMERO RATA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO   </strong></span></td>" _
                                & "<td style='height: 19px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO</strong></span></td>" _
                                & "</tr>"


               

                par.cmd.CommandText = "select * from VSA_BOL_RID_CANONE where id_domanda=" & Request.QueryString("ID")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read

                    par.cmd.CommandText = "SELECT BOL_BOLLETTE.*,substr(riferimento_da,1,4) AS COMPETENZA, " _
                                   & "(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, " _
                                   & "(CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO " _
                                   & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) and bol_bollette.id =" & myReader1("id_bolletta") & " AND " _
                                   & "TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO " _
                                   & " ORDER BY COMPETENZA ASC"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Nbolletta = ""
                        ImportoBolletta = par.IfNull(myReader1("importo"), 0)

                        If par.IfNull(myReader("N_RATA"), "") = "99" Then
                            Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                        ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                            Nbolletta = "AUT/" & par.IfNull(myReader("ANNO"), "")
                        ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                            Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                        Else
                            Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                        End If

                        If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
                            COLORE = "#FF1800"
                        End If


                        Contatore = Contatore + 1

                        testoTabella = testoTabella _
                        & "<tr bgcolor = '" & COLORE & "'>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COMPETENZA"), "") & "</span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contabilita/DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & IDANA & "&IDCONT=" & myReader("id_contratto") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>"

                        If par.IfNull(myReader("TIPO"), "n.d.") = "MOR" Or par.IfNull(myReader("TIPO"), "n.d.") = "FIN" Then
                            testoTabella = testoTabella _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contabilita/DetMorosita.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "','DettMorosita" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("TIPO"), "n.d.") & "</a></span></td>"
                        Else
                            testoTabella = testoTabella _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>"
                        End If

                        testoTabella = testoTabella _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), " ") & "</span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>"

                        '**************colonna importo TOTALE****************************************************************************************************************************
                        testoTabella = testoTabella _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(myReader1("IMPORTO"), 0)), "##,##0.00") & "</span></td>"
                        TotImporto = TotImporto + par.IfNull(myReader1("IMPORTO"), 0)


                        testoTabella = testoTabella & "</td>"


                        If COLORE = "#FFFFFF" Then
                            COLORE = "#E6E6E6"
                        Else
                            COLORE = "#FFFFFF"
                        End If
                    End If
                    myReader.Close()
                Loop

                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                testoTabella = testoTabella _
                & "<tr style = 'background-color: #D8D8D8;'>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px; text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotImporto, "##,##0.00") & "</strong></span></td>" _
                & "</tr>"



                Me.TBL_DETTAGLIO_SALDO.Text = testoTabella & "</table>"


            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
