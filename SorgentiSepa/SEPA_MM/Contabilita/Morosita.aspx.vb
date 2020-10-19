Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contabilita_Morosita
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim StringaSQL As String = ""
    Dim DT As New Data.DataTable

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Try
                Dim condition As String = ""

                If Not String.IsNullOrEmpty(Request.QueryString("DAL")) Then
                    condition = " and bol_bollette.data_emissione>='" & Request.QueryString("DAL") & "'"
                End If


                If Not String.IsNullOrEmpty(Request.QueryString("AL")) Then
                    condition = condition & " and bol_bollette.data_emissione<='" & Request.QueryString("AL") & "'"
                End If



                If Not String.IsNullOrEmpty(Request.QueryString("DALRIF")) Then
                    condition = condition & " and bol_bollette.riferimento_da>='" & Request.QueryString("DALRIF") & "'"
                End If



                If Not String.IsNullOrEmpty(Request.QueryString("ALRIF")) Then
                    condition = condition & " and bol_bollette.riferimento_a<='" & Request.QueryString("ALRIF") & "'"
                End If

                DT.Columns.Add("CONDUTTORE")
                DT.Columns.Add("EMESSO")
                DT.Columns.Add("PAGATO")
                DT.Columns.Add("DA_PAGARE")

                Dim RIGA As System.Data.DataRow

                Dim Str As String

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()

                Dim TOTEMESSO As Double = 0
                Dim TOTPAGATO As Double = 0
                Dim TOTDAPAGARE As Double = 0
                Dim TOTMOROSITA As Double = 0

                Dim MioTotEmesso As Double = 0
                Dim MioTotPagato As Double = 0
                Dim MioTotDaPagare As Double = 0


                '*******************APERURA CONNESSIONE*********************
                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 18px; font-weight: bold; text-align: center'><td></td><td>RIEPILOGO DEI PAGAMENTI</td></tr></table>")
                Response.Write("<br/>")

                'PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME||' '||ANAGRAFICA.RAGIONE_SOCIALE) AS CONDUTTORE,SUM((SELECT SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=BOL_BOLLETTE.ID)) AS TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc='ERP'GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE"
                PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS  CONDUTTORE,SUM(bol_bollette.importo_totale) as TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM  SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' " & condition & " AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc='ERP'GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE order by conduttore asc"


                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold; text-align: left'><td></td><td>RELATIVO AI CONTRATTI ERP</td></tr></table>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 10px; font-weight: bold'><td></td><td>CONDUTTORE</td style='text-align: right'><td style='text-align: right'>EMESSO</td><td style='text-align: right'>PAGATO</td><td style='text-align: right'>DA PAGARE</td></tr>")

                Dim myReaderAa As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = ""
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = "CONTRATTI ERP"
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                Do While myReaderAa.Read
                    If myReaderAa("TOTALE") > 0 Then
                        Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00") & "</td></tr>")
                        TOTEMESSO = TOTEMESSO + PAR.IfNull(myReaderAa("TOTALE"), 0)

                        MioTotEmesso = MioTotEmesso + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        MioTotPagato = MioTotPagato + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        MioTotDaPagare = MioTotDaPagare + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        TOTPAGATO = TOTPAGATO + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        TOTDAPAGARE = TOTDAPAGARE + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        RIGA = DT.NewRow()


                        RIGA.Item("CONDUTTORE") = PAR.IfNull(myReaderAa("CONDUTTORE"), "")
                        RIGA.Item("EMESSO") = PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0)
                        RIGA.Item("PAGATO") = PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)
                        RIGA.Item("DA_PAGARE") = Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00")

                        DT.Rows.Add(RIGA)
                    End If
                Loop
                myReaderAa.Close()
                Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold'><td></td><td>TOTALE</td><td style = 'text-align: right'>" & Format(TOTEMESSO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTPAGATO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTDAPAGARE, "##,##0.00") & "</td></tr>")

                Response.Write("</table>")


                Response.Write("<br/>")
                Response.Write("<br/>")
                Response.Flush()
                TOTMOROSITA = TOTMOROSITA + TOTDAPAGARE
                TOTEMESSO = 0
                TOTPAGATO = 0
                TOTDAPAGARE = 0
                '***********************USDX***********************

                'PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS CONDUTTORE,SUM((SELECT SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=BOL_BOLLETTE.ID)) AS TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND SUBSTR(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,1,3)= 'USD'  GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE"
                PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS CONDUTTORE,SUM(bol_bollette.importo_totale) as TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM  SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND SUBSTR(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,1,3)= 'USD'  GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE order by conduttore asc"

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = ""
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = "CONTRATTI USI DIVERSI"
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold;text-align: left'><td></td><td>RELATIVO AI CONTRATTI USI DIVERSI</td></tr></table>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 10px; font-weight: bold'><td></td><td>CONDUTTORE</td style='text-align: right'><td style='text-align: right'>EMESSO</td><td style='text-align: right'>PAGATO</td><td style='text-align: right'>DA PAGARE</td></tr>")

                myReaderAa = PAR.cmd.ExecuteReader()

                Do While myReaderAa.Read
                    If myReaderAa("TOTALE") > 0 Then
                        ' Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)) & "</td></tr>")
                        Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00") & "</td></tr>")


                        TOTEMESSO = TOTEMESSO + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        TOTPAGATO = TOTPAGATO + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        TOTDAPAGARE = TOTDAPAGARE + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        MioTotEmesso = MioTotEmesso + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        MioTotPagato = MioTotPagato + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        MioTotDaPagare = MioTotDaPagare + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        RIGA = DT.NewRow()


                        RIGA.Item("CONDUTTORE") = PAR.IfNull(myReaderAa("CONDUTTORE"), "")
                        RIGA.Item("EMESSO") = PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0)
                        RIGA.Item("PAGATO") = PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)
                        RIGA.Item("DA_PAGARE") = Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00")

                        DT.Rows.Add(RIGA)

                    End If
                Loop
                myReaderAa.Close()
                Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold'><td></td><td>TOTALE</td><td style = 'text-align: right'>" & Format(TOTEMESSO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTPAGATO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTDAPAGARE, "##,##0.00") & "</td></tr>")

                Response.Write("</table>")


                Response.Write("<br/>")
                Response.Write("<br/>")
                TOTMOROSITA = TOTMOROSITA + TOTDAPAGARE
                TOTEMESSO = 0
                TOTPAGATO = 0
                TOTDAPAGARE = 0

                RIGA = DT.NewRow()



                RIGA.Item("CONDUTTORE") = ""
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()

                RIGA.Item("CONDUTTORE") = "CONTRATTI 431/98"
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                '**********************L43198***********************
                'PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS   CONDUTTORE,SUM((SELECT SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=BOL_BOLLETTE.ID)) AS TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc= 'L43198'  GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE"
                PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS CONDUTTORE,SUM(bol_bollette.importo_totale) as TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' " & condition & " AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc= 'L43198' " & condition & " GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE order by conduttore asc"

                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold; text-align: left'><td></td><td>RELATIVO AI CONTRATTI L.431/98</td></tr></table>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 10px; font-weight: bold'><td></td><td>CONDUTTORE</td style='text-align: right'><td style='text-align: right'>EMESSO</td><td style='text-align: right'>PAGATO</td><td style='text-align: right'>DA PAGARE</td></tr>")

                myReaderAa = PAR.cmd.ExecuteReader()

                Do While myReaderAa.Read
                    If myReaderAa("TOTALE") > 0 Then

                        'Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)) & "</td></tr>")
                        Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00") & "</td></tr>")


                        TOTEMESSO = TOTEMESSO + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        TOTPAGATO = TOTPAGATO + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        TOTDAPAGARE = TOTDAPAGARE + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        MioTotEmesso = MioTotEmesso + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        MioTotPagato = MioTotPagato + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        MioTotDaPagare = MioTotDaPagare + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        RIGA = DT.NewRow()


                        RIGA.Item("CONDUTTORE") = PAR.IfNull(myReaderAa("CONDUTTORE"), "")
                        RIGA.Item("EMESSO") = PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0)
                        RIGA.Item("PAGATO") = PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)
                        RIGA.Item("DA_PAGARE") = Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00")

                        DT.Rows.Add(RIGA)

                    End If
                Loop
                myReaderAa.Close()
                Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold'><td></td><td>TOTALE</td><td style = 'text-align: right'>" & Format(TOTEMESSO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTPAGATO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTDAPAGARE, "##,##0.00") & "</td></tr>")

                Response.Write("</table>")


                Response.Write("<br/>")
                Response.Write("<br/>")
                TOTMOROSITA = TOTMOROSITA + TOTDAPAGARE
                TOTEMESSO = 0
                TOTPAGATO = 0
                TOTDAPAGARE = 0

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = ""
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = "CONTRATTI 392/78"
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)
                '***********************EQC392***********************
                'PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS   CONDUTTORE,SUM((SELECT SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=BOL_BOLLETTE.ID)) AS TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc= 'EQC392'  GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE"
                PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS CONDUTTORE,SUM(bol_bollette.importo_totale) as TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' " & condition & " AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc= 'EQC392'  GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE order by conduttore asc"
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold; text-align: left'><td></td><td>RELATIVO AI CONTRATTI EQUOCANONE</td></tr></table>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 10px; font-weight: bold'><td></td><td>CONDUTTORE</td style='text-align: right'><td style='text-align: right'>EMESSO</td><td style='text-align: right'>PAGATO</td><td style='text-align: right'>DA PAGARE</td></tr>")

                myReaderAa = PAR.cmd.ExecuteReader()

                Do While myReaderAa.Read
                    If myReaderAa("TOTALE") > 0 Then

                        '                        Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)) & "</td></tr>")
                        Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00") & "</td></tr>")

                        TOTEMESSO = TOTEMESSO + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        TOTPAGATO = TOTPAGATO + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        TOTDAPAGARE = TOTDAPAGARE + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        MioTotEmesso = MioTotEmesso + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        MioTotPagato = MioTotPagato + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        MioTotDaPagare = MioTotDaPagare + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        RIGA = DT.NewRow()


                        RIGA.Item("CONDUTTORE") = PAR.IfNull(myReaderAa("CONDUTTORE"), "")
                        RIGA.Item("EMESSO") = PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0)
                        RIGA.Item("PAGATO") = PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)
                        RIGA.Item("DA_PAGARE") = Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00")

                        DT.Rows.Add(RIGA)

                    End If
                Loop
                myReaderAa.Close()
                Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold'><td></td><td>TOTALE</td><td style = 'text-align: right'>" & Format(TOTEMESSO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTPAGATO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTDAPAGARE, "##,##0.00") & "</td></tr>")

                Response.Write("</table>")


                Response.Write("<br/>")
                Response.Write("<br/>")
                TOTMOROSITA = TOTMOROSITA + TOTDAPAGARE
                TOTEMESSO = 0
                TOTPAGATO = 0
                TOTDAPAGARE = 0

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = ""
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = "RAPPORTI CON ABUSIVI"
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)
                '***********************NONE***********************
                'PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS   CONDUTTORE,SUM((SELECT SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=BOL_BOLLETTE.ID)) AS TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc= 'NONE'  GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE"
                PAR.cmd.CommandText = "SELECT ANAGRAFICA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS CONDUTTORE,SUM(bol_bollette.importo_totale) as TOTALE ,SUM(BOL_BOLLETTE.IMPORTO_PAGATO) AS PAGATO, '' AS DA_PAGARE FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.INTESTATARI_RAPPORTO WHERE BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') AND BOL_BOLLETTE.FL_ANNULLATA='0' " & condition & " AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND rapporti_utenza.cod_tipologia_contr_loc= 'NONE'  GROUP BY ANAGRAFICA.ID,COGNOME,NOME,RAGIONE_SOCIALE order by conduttore asc"

                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold; text-align: left'><td></td><td>RELATIVO AI RAPPORTI DI UTENZA DI CONTRATTI ABUSIVI</td></tr></table>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 10px; font-weight: bold'><td></td><td>CONDUTTORE</td style='text-align: right'><td style='text-align: right'>EMESSO</td><td style='text-align: right'>PAGATO</td><td style='text-align: right'>DA PAGARE</td></tr>")

                myReaderAa = PAR.cmd.ExecuteReader()

                Do While myReaderAa.Read
                    If myReaderAa("TOTALE") > 0 Then

                        'Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)) & "</td></tr>")
                        Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td></td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "DatiUtenza.aspx?C=RisUtenza&IDANA=" & PAR.IfNull(myReaderAa("ID"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & myReaderAa("CONDUTTORE") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin; text-align: right'>" & Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00") & "</td></tr>")


                        TOTEMESSO = TOTEMESSO + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        TOTPAGATO = TOTPAGATO + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        TOTDAPAGARE = TOTDAPAGARE + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        MioTotEmesso = MioTotEmesso + PAR.IfNull(myReaderAa("TOTALE"), 0)
                        MioTotPagato = MioTotPagato + PAR.IfNull(myReaderAa("PAGATO"), 0)
                        MioTotDaPagare = MioTotDaPagare + (PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0))

                        RIGA = DT.NewRow()


                        RIGA.Item("CONDUTTORE") = PAR.IfNull(myReaderAa("CONDUTTORE"), "")
                        RIGA.Item("EMESSO") = PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0)
                        RIGA.Item("PAGATO") = PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)
                        RIGA.Item("DA_PAGARE") = Format((PAR.IfEmpty(myReaderAa("TOTALE").ToString, 0) - PAR.IfEmpty(myReaderAa("PAGATO").ToString, 0)), "0.00")

                        DT.Rows.Add(RIGA)

                    End If
                Loop
                myReaderAa.Close()
                Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold'><td></td><td>TOTALE</td><td style = 'text-align: right'>" & Format(TOTEMESSO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTPAGATO, "##,##0.00") & "</td><td style = 'text-align: right'>" & Format(TOTDAPAGARE, "##,##0.00") & "</td></tr>")

                Response.Write("</table>")



                Response.Write("<br/>")
                Response.Write("<br/>")
                TOTMOROSITA = TOTMOROSITA + TOTDAPAGARE
                TOTEMESSO = 0
                TOTPAGATO = 0
                TOTDAPAGARE = 0
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold; text-align: left'><td></td><td>TOTALE DELLE SOMME PAGATE € " & Format(MioTotPagato, "##,##0.00") & "</td></tr></table>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold; text-align: left'><td></td><td>TOTALE DELLE SOMME NON PAGATE € " & Format(MioTotDaPagare, "##,##0.00") & "</td></tr></table>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 12px; font-weight: bold; text-align: left'><td></td><td>TOTALE GENERALE € " & Format(MioTotEmesso, "##,##0.00") & "</td></tr></table>")


                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = ""
                RIGA.Item("EMESSO") = ""
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = "TOTALE DELLE SOMME PAGATE"
                RIGA.Item("EMESSO") = Format(MioTotPagato, "##,##0.00")
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = "TOTALE DELLE SOMME NON PAGATE"
                RIGA.Item("EMESSO") = Format(MioTotDaPagare, "##,##0.00")
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()


                RIGA.Item("CONDUTTORE") = "TOTALE GENERALE"
                RIGA.Item("EMESSO") = Format(MioTotEmesso, "##,##0.00")
                RIGA.Item("PAGATO") = ""
                RIGA.Item("DA_PAGARE") = ""

                DT.Rows.Add(RIGA)

                Session.Add("MIADT", DT)

                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String
        Dim row As System.Data.DataRow

        DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
        sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

        i = 0

        With myExcelFile

            .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
            .PrintGridLines = False
            .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
            .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
            .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
            .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
            .SetDefaultRowHeight(14)
            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
            .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)



            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CONDUTTORE", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "EMESSO", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "PAGATO", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DA PAGARE", 12)



            K = 2
            For Each row In DT.Rows
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CONDUTTORE"), 0)))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("EMESSO"), 0)))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PAGATO"), 0)))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DA_PAGARE"), 0)))
                i = i + 1
                K = K + 1
            Next

            .CloseFile()
        End With

        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '
        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

        Dim sFile As String = Path.GetFileName(strFile)
        Dim theEntry As ZipEntry = New ZipEntry(sFile)
        Dim fi As New FileInfo(strFile)
        theEntry.DateTime = fi.LastWriteTime
        theEntry.Size = strmFile.Length
        strmFile.Close()
        objCrc32.Reset()
        objCrc32.Update(abyBuffer)
        theEntry.Crc = objCrc32.Value
        strmZipOutputStream.PutNextEntry(theEntry)
        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()

        File.Delete(strFile)
        Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

        'Response.Write("<script>window.open('../Varie/" & sNomeFile & ".zip','','');</script>")
    End Sub
End Class
