
Partial Class Contabilita_DettaglioBolletta2
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            ScriviTabella()
        End If
    End Sub

    Private Sub ScriviTabella()
        Try
            Dim COLORE As String = "#E6E6E6"
            Dim bTrovato As Boolean = False
            Dim TotDaPagare As Decimal = 0
            Dim TotFinalePagato As Decimal = 0
            Dim TotFinaleDaPagare As Decimal = 0
            Dim testoTabella As String = ""
            Dim testoTabellaVoci As String = ""
            Dim sStringaSql As String = ""
            Dim TotRicoperto As Decimal = 0
            Dim TotResiduo As Decimal = 0

            Dim Contatore As Integer = 0
            Dim codContratto As String = ""

            Dim elencoIDBoll As String = ""

            If Not IsNothing(Session.Item("IDBollCOP")) Then
                elencoIDBoll = Session.Item("IDBollCOP")
            End If

            If elencoIDBoll <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sStringaSql = sStringaSql & " BOL_BOLLETTE.ID in " & par.PulisciStrSql(elencoIDBoll) & " AND "
            End If

            testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong></strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUM.BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>TIPO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUM. RATA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                                & "<td style='height: 19px; text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>VOCI DELLA BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMP.BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOT.COPERTO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMP.PAGATO TOT.</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMP.RESIDUO</strong></span></td>" _
                                & "</tr>"
            testoTabellaVoci = "<table cellpadding='0' cellspacing='0' width='100%'>" _
                & "<tr>" _
                & "<td style='height: 15'>" _
                & "<span style='font-size: 6pt; font-family:Courier New'><strong>DESCRIZIONE</strong></span></td>" _
                & "<td style='height: 15px;text-align:right'>" _
                & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMPORTO TOT.</strong></span></td>" _
                & "<td style='height: 15px;text-align:right'>" _
                & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMP.COPERTO</strong></span></td>" _
                & "<td style='height: 15px;text-align:right'>" _
                & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMP.PAGATO TOT.</strong></span></td>" _
                & "<td style='height: 15px;text-align:right'>" _
                & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMP.RESIDUO</strong></span></td>" _
                & "</tr>"

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If Request.QueryString("IDANA") <> "" And Request.QueryString("IDCONT") <> "" Then
                Dim Nbolletta As String
                Dim coloreAnnullo As String

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & Request.QueryString("IDCONT")
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    codContratto = par.IfNull(myReader0("COD_CONTRATTO"), "")
                End If
                myReader0.Close()

                par.cmd.CommandText = "SELECT BOL_BOLLETTE.*,(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE " _
                    & "WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND " & sStringaSql & " SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") _
                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA =  " & Request.QueryString("IDANA") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' ORDER BY BOL_BOLLETTE.ID ASC"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtBoll As New Data.DataTable
                da.Fill(dtBoll)
                da.Dispose()

                If dtBoll.Rows.Count > 0 Then
                    For Each row As Data.DataRow In dtBoll.Rows

                        Nbolletta = ""
                        If par.IfNull(row.Item("N_RATA"), "") = "99" Then
                            Nbolletta = "MA/" & par.IfNull(row.Item("ANNO"), "")
                        ElseIf par.IfNull(row.Item("N_RATA"), "") = "999" Then
                            Nbolletta = "AU/" & par.IfNull(row.Item("ANNO"), "")
                        ElseIf par.IfNull(row.Item("N_RATA"), "") = "99999" Then
                            Nbolletta = "CO/" & par.IfNull(row.Item("ANNO"), "")
                        Else
                            Nbolletta = par.IfNull(row.Item("N_RATA"), "") & "/" & par.IfNull(row.Item("ANNO"), "")
                        End If

                        Contatore = Contatore + 1

                        If par.IfNull(row.Item("FL_ANNULLATA"), "0") <> 0 Then
                            coloreAnnullo = "bgcolor = 'red'"
                        Else
                            coloreAnnullo = ""
                        End If
                        testoTabella = testoTabella _
                        & "<tr " & coloreAnnullo & ">" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Contatore & ")</span></td>" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioImpPagati.aspx?IDBOLLETTA=" & par.IfNull(row.Item("ID"), "") & "','DettPagati" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(row.Item("NUM_BOLLETTA"), "- - -") & "</a></span></td>"


                        If par.IfNull(row.Item("TIPO"), "n.d.") = "MOR" Or par.IfNull(row.Item("TIPO"), "n.d.") = "FIN" Then
                            testoTabella = testoTabella _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DetMorosita.aspx?IDBOLL=" & par.IfNull(row.Item("ID"), "") & "','DettMorosita" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(row.Item("TIPO"), "n.d.") & "</a></span></td>"
                        Else
                            testoTabella = testoTabella _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(row.Item("TIPO"), "n.d.") & "</span></td>"
                        End If


                        testoTabella = testoTabella _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Nbolletta & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial;text-align:center'>" & par.IfNull(row.Item("RIFERIMENTO"), "") & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(row.Item("DATA_EMISSIONE"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(row.Item("DATA_SCADENZA"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Courier New''>" & testoTabellaVoci & "</span>"


                        '*** QUERY PER LE VOCI DI BOLLETTA ***
                        Dim impCoperto As Decimal = 0
                        Dim impResiduo As Decimal = 0
                        'par.cmd.CommandText = "SELECT bol_bollette_voci.id_bolletta,bol_bollette_voci.id, t_voci_bolletta.descrizione AS descrVoce,bol_bollette_voci.importo as IMP_TOT," _
                        '    & " eventi_pagamenti_parz_dett.importo as imp_coperto," _
                        '    & " bol_bollette_voci.imp_pagato," _
                        '    & " bol_bollette_voci.importo-bol_bollette_voci.imp_pagato as residuo" _
                        '    & " FROM siscom_mi.bol_bollette," _
                        '    & " siscom_mi.bol_bollette_voci," _
                        '    & " siscom_mi.t_voci_bolletta," _
                        '    & " siscom_mi.eventi_pagamenti_parz_dett," _
                        '    & "	siscom_mi.eventi_pagamenti_parziali" _
                        '    & " WHERE eventi_pagamenti_parz_dett.id_voce_bolletta = bol_bollette_voci.ID" _
                        '    & " AND bol_bollette_voci.id_voce = t_voci_bolletta.ID" _
                        '    & " AND bol_bollette.id =" & row.Item("ID") _
                        '    & " AND bol_bollette.ID = bol_bollette_voci.id_bolletta" _
                        '    & " AND bol_bollette.id_bolletta_ric IS NULL" _
                        '    & " AND bol_bollette.id_rateizzazione IS NULL" _
                        '    & " and eventi_pagamenti_parziali.id=eventi_pagamenti_parz_dett.id_evento_principale" _
                        '    & " ORDER BY bol_bollette_voci.id ASC"
                        par.cmd.CommandText = "SELECT bol_bollette_voci.id_bolletta, bol_bollette_voci.ID," _
                            & " t_voci_bolletta.descrizione AS descrvoce," _
                            & " nvl(bol_bollette_voci.importo,0) AS imp_tot," _
                            & " nvl(bol_bollette_voci.imp_pagato,0) as imp_pagato," _
                            & " (nvl(bol_bollette_voci.importo,0) - nvl(bol_bollette_voci.imp_pagato,0)) AS residuo," _
                            & " '0' as imp_coperto" _
                            & " FROM siscom_mi.bol_bollette," _
                            & " siscom_mi.bol_bollette_voci," _
                            & " siscom_mi.t_voci_bolletta" _
                            & " WHERE bol_bollette_voci.id_voce = t_voci_bolletta.ID" _
                            & " AND bol_bollette.ID = " & row.Item("ID") & "" _
                            & " AND bol_bollette.ID = bol_bollette_voci.id_bolletta" _
                            & " " _
                            & " " _
                            & " ORDER BY bol_bollette_voci.ID ASC"
                        Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtVoci As New Data.DataTable
                        da2.Fill(dtVoci)
                        da2.Dispose()
                        If dtVoci.Rows.Count > 0 Then
                            For Each rowVoci As Data.DataRow In dtVoci.Rows

                                par.cmd.CommandText = "SELECT bol_bollette_voci.id_bolletta,bol_bollette_voci.id, t_voci_bolletta.descrizione AS descrVoce,bol_bollette_voci.importo as IMP_TOT," _
                                        & " bol_bollette_voci_pagamenti.importo_pagato as imp_coperto," _
                                        & " bol_bollette_voci.imp_pagato," _
                                        & " bol_bollette_voci.importo-bol_bollette_voci.imp_pagato as residuo" _
                                        & " FROM siscom_mi.bol_bollette," _
                                        & " siscom_mi.bol_bollette_voci," _
                                        & " siscom_mi.t_voci_bolletta," _
                                        & " siscom_mi.bol_bollette_voci_pagamenti" _
                                        & " WHERE bol_bollette_voci_pagamenti.id_voce_bolletta = bol_bollette_voci.ID" _
                                        & " AND bol_bollette_voci.id_voce = t_voci_bolletta.ID" _
                                        & " AND bol_bollette_voci.id =" & rowVoci.Item("ID") _
                                        & " AND bol_bollette_voci_pagamenti.fl_no_report=0 and bol_bollette.ID = bol_bollette_voci.id_bolletta" _
                                        & " " _
                                        & " " _
                                        & " AND bol_bollette_voci_pagamenti.id_incasso_Extramav=" & Request.QueryString("IDE") & "" _
                                        & " ORDER BY bol_bollette_voci.id ASC"
                                Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                                Dim dtVociC As New Data.DataTable
                                da3.Fill(dtVociC)
                                da3.Dispose()

                                If dtVociC.Rows.Count > 0 Then
                                    For Each rowVocic As Data.DataRow In dtVociC.Rows
                                        testoTabella = testoTabella _
                                            & "<tr bgcolor = '" & COLORE & "'>" _
                                            & "<td style='height: 15px'>" _
                                            & "<span style='font-size: 6pt; font-family: Courier New'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioImpPagati.aspx?IDBOLLETTA=" & par.IfNull(row.Item("ID"), 0) & "&IDVOCE=" & par.IfNull(rowVoci.Item("ID"), "") & "','DettPagati" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(rowVoci.Item("descrVoce"), "") & "</a></span></td>" _
                                            & "<td style='height: 15px;text-align:right'>" _
                                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(rowVocic.Item("IMP_TOT"), 0)), "##,##0.00") & "</span></td>" _
                                            & "<td style='height: 15px;text-align:right'>" _
                                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(rowVocic.Item("IMP_COPERTO"), 0)), "##,##0.00") & "</span></td>" _
                                            & "<td style='height: 15px;text-align:right'>" _
                                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(rowVocic.Item("IMP_PAGATO"), 0)), "##,##0.00") & "</span></td>" _
                                            & "<td style='height: 15px;text-align:right'>" _
                                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(rowVocic.Item("RESIDUO"), 0)), "##,##0.00") & "</span></td>"
                                        impCoperto = impCoperto + par.IfNull(rowVocic.Item("IMP_COPERTO"), 0)
                                    Next
                                Else
                                    testoTabella = testoTabella _
                                        & "<tr bgcolor = '" & COLORE & "'>" _
                                        & "<td style='height: 15px'>" _
                                        & "<span style='font-size: 6pt; font-family: Courier New'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioImpPagati.aspx?IDBOLLETTA=" & par.IfNull(row.Item("ID"), 0) & "&IDVOCE=" & par.IfNull(rowVoci.Item("ID"), "") & "','DettPagati" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(rowVoci.Item("descrVoce"), "") & "</a></span></td>" _
                                        & "<td style='height: 15px;text-align:right'>" _
                                        & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(rowVoci.Item("IMP_TOT"), 0)), "##,##0.00") & "</span></td>" _
                                        & "<td style='height: 15px;text-align:right'>" _
                                        & "<span style='font-size: 6pt; font-family: Courier New'>€.0</span></td>" _
                                        & "<td style='height: 15px;text-align:right'>" _
                                        & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(rowVoci.Item("IMP_PAGATO"), 0)), "##,##0.00") & "</span></td>" _
                                        & "<td style='height: 15px;text-align:right'>" _
                                        & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(rowVoci.Item("RESIDUO"), 0)), "##,##0.00") & "</span></td>"
                                End If


                                If COLORE = "#FFFFFF" Then
                                    COLORE = "#E6E6E6"
                                Else
                                    COLORE = "#FFFFFF"
                                End If

                                impResiduo = impResiduo + par.IfNull(rowVoci.Item("RESIDUO"), 0)
                            Next
                        End If

                        TotRicoperto = TotRicoperto + impCoperto
                        TotResiduo = TotResiduo + impResiduo

                        TotDaPagare = par.IfNull(row.Item("IMPORTO_TOTALE"), 0)

                        If par.IfNull(row.Item("ID_TIPO"), 1) = 4 Then
                            testoTabella = testoTabella & "</table></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(row.Item("IMPORTO_TOTALE"), 0), "##,##0.00") & "<br/>MOROSITA' €." & Format(par.IfNull(row.Item("IMPORTO_RIC_B"), 0), "##,##0.00") & "</span></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(impCoperto, "##,##0.00") & "&nbsp;</span></td>" _
                                                        & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(row.Item("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>"
                        ElseIf par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                            testoTabella = testoTabella & "</table></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(row.Item("IMPORTO_TOTALE"), 0), "##,##0.00") & "<br/>RATEIZ.' €." & Format(par.IfNull(row.Item("IMPORTO_RIC_B"), 0), "##,##0.00") & "</span></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(impCoperto, "##,##0.00") & "&nbsp;</span></td>" _
                                                        & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(row.Item("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>"
                        Else
                            testoTabella = testoTabella & "</table></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(TotDaPagare, "##,##0.00") & "</span></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(impCoperto, "##,##0.00") & "&nbsp;</span></td>" _
                                                        & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(row.Item("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>"
                        End If

                        testoTabella = testoTabella & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format((par.IfNull(row.Item("IMPORTO_PAGATO"), 0)), "##,##0.00") & "</span></td>" _
                                                    & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(impResiduo, "##,##0.00") & "</span></td></tr>"

                        TotFinalePagato = TotFinalePagato + (par.IfNull(row.Item("IMPORTO_PAGATO"), 0))
                        TotFinaleDaPagare = TotFinaleDaPagare + TotDaPagare

                    Next
                End If

                testoTabella = testoTabella _
                    & "<tr>" _
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
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinaleDaPagare, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial;background-color:yellow;'><strong>€." & Format(TotRicoperto, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinalePagato, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotResiduo, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "</tr>"

            End If

            Me.TBL_DETTAGLIO_BOLLETTA.Text = testoTabella & "</table>"

            If Request.QueryString("IDANA") <> "" Then

                par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = " & Request.QueryString("IDANA")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.LblIntestazione.Text = myReader("INTESTATARIO") & " - Cod. Contratto " & codContratto
                End If
                myReader.Close()

            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
