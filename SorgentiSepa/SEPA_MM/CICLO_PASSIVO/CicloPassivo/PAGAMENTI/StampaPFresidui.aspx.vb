
Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_StampaPFresidui
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Dim totale2 As Decimal = 0
    Dim totale3 As Decimal = 0
    Dim totale4 As Decimal = 0
    Dim totale5 As Decimal = 0
    Dim totale6 As Decimal = 0
    Dim condizioneStruttura As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_RESIDUI") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            CaricaTabella()
        End If
    End Sub
    Protected Sub CaricaTabella()
        Dim Esercizio As String = ""
        Try
            Dim ANNO As String = ""
            Dim ANNO2 As String = ""
            Dim ANNOP As String = ""
            ApriConnessione()
            Dim DATAAL As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID='" & Request.QueryString("EF_R") & "' "
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                Esercizio = "Esercizio Finanziario " & par.FormattaData(par.IfNull(LETTORE("INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(LETTORE("FINE"), ""))
                DATAAL = par.IfNull(LETTORE("FINE"), "")
                ANNO = Left(DATAAL, 4)
                ANNO2 = CStr(CInt(ANNO) + 1)
                ANNOP = CStr(CInt(ANNO) - 1)
                Titolo.Text = "Situazione Uscite in Conto Capitale - " & Esercizio
            End If
            LETTORE.Close()
            If Not IsNothing(Request.QueryString("S")) Then
                If Request.QueryString("S") = "-1" Then
                    condizioneStruttura = ""
                Else
                    condizioneStruttura = " ID_STRUTTURA=" & Request.QueryString("S") & " AND "
                End If
            End If
            par.cmd.CommandText = "SELECT CODICE,DESCRIZIONE," _
                & "'' AS RESIDUI_AP, " _
                & "TRIM(TO_CHAR((" _
                & "(SELECT SUM(NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0)+NVL(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_STRUTTURA  WHERE " & condizioneStruttura & " ID_VOCE=PF_VOCI.ID) " _
                & "+NVL((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE " & condizioneStruttura & " ID_VOCE_DA IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND SUBSTR(DATA_ORA,1,8)>'" & DATAAL & "'),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE " & condizioneStruttura & " ID_VOCE_A IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND SUBSTR(DATA_ORA,1,8)>'" & DATAAL & "'),0) " _
                & "-NVL((SELECT SUM(NVL(IMPORTO_APPROVATO,0)) FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO WHERE " & condizioneStruttura & " ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND PF_ASSESTAMENTO.ID=PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO AND ID_PF_MAIN=" & Request.QueryString("EF_R") & " AND ID_STATO>=5 AND DATA_APP_COMUNE>'" & DATAAL & "'),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_rit_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO_rit_legge IN (SELECT DISTINCT ID_PAGAMENTO_rit_legge FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "),'999G999G990D99'))" _
                & "AS DISPONIBILITA_RESIDUA," _
                & "'' AS DISPONIBILITA_TOTALE," _
                & "'<a href=""javascript:window.open(''Impegnato.aspx?ID_STRUTTURA=" & Request.QueryString("S") & "&ID_VOCE='||PF_VOCI.ID||'&ID_PF='||pf_voci.ID_PIANO_FINANZIARIO|| ''',''_blank'',''resizable=yes,height=800,width=1000,top=0,left=100,scrollbars=yes'');void(0);"">' ||" _
                & "TRIM(TO_CHAR((" _
                & "NVL((SELECT SUM(NVL(IMPORTO_PRENOTATO,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " (ID_STATO=0 OR (ID_STATO = 1 AND DATA_CONSUNTIVAZIONE >'" & DATAAL & "') OR (ID_STATO = 2 AND DATA_CERTIFICAZIONE >'" & DATAAL & "' AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "')) AND ANNO>='" & ANNO & "' AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "+NVL((SELECT SUM(NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ((ID_STATO=1) OR (ID_STATO=2 AND DATA_CERTIFICAZIONE >'" & DATAAL & "'))AND DATA_CONSUNTIVAZIONE <='" & DATAAL & "' AND ANNO>='" & ANNO & "' AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "+NVL((SELECT SUM(NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " (ID_STATO=2 AND DATA_CERTIFICAZIONE<='" & DATAAL & "') AND ANNO>='" & ANNO & "' AND ID_VOCE_PF=PF_VOCI.ID),0)" _
                & "-NVL((SELECT SUM(NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " (ID_STATO=0 OR (ID_STATO = 1 AND DATA_CONSUNTIVAZIONE >'" & DATAAL & "') OR (ID_STATO = 2 AND DATA_CERTIFICAZIONE >'" & DATAAL & "' AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "')) AND ANNO>='" & ANNO & "' AND ID_VOCE_PF=PF_VOCI.ID AND ID_PAGAMENTO IN (" _
                & " SELECT ID_PAGAMENTO AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID " _
                & " )),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO_PRENOTATO,0)-NVL(IMPORTO_APPROVATO,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " (ID_STATO=0 OR (ID_STATO = 1 AND DATA_CONSUNTIVAZIONE >'" & DATAAL & "') OR (ID_STATO = 2 AND DATA_CERTIFICAZIONE >'" & DATAAL & "' AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "')) AND ANNO>='" & ANNO & "' AND ID_VOCE_PF=PF_VOCI.ID AND ID_PAGAMENTO IN (" _
                & " SELECT ID_PAGAMENTO AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID " _
                & " )),0)" _
                & "/*+NVL((SELECT SUM(NVL(RIT_LEGGE_IVATA,0))FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & "  ((ID_STATO=1) OR (ID_STATO=2 AND DATA_CERTIFICAZIONE >'" & DATAAL & "'))AND DATA_CONSUNTIVAZIONE <='" & DATAAL & "' AND ANNO>='" & ANNO & "' AND ID_VOCE_PF =PF_VOCI.ID),0)*/" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & "  DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "),'999G999G990D99'))" _
                & "|| '</a>' " _
                & "AS IMPEGNATO, " _
                & "'<a href=""javascript:window.open(''PagamentiContoCapitale.aspx?ID_STRUTTURA=" & Request.QueryString("S") & "&ID_VOCE='||PF_VOCI.ID||'&ID_PF='||pf_voci.ID_PIANO_FINANZIARIO|| ''',''_blank'',''resizable=yes,height=800,width=1000,top=0,left=150,scrollbars=yes'');void(0);"">' ||" _
                & "TRIM(TO_CHAR((" _
                & "NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "+NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "),'999G999G990D99'))" _
                & "|| '</a>' " _
                & "AS PAGAMENTI," _
                & "TRIM(TO_CHAR((" _
                & "(SELECT SUM(NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0)+NVL(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_STRUTTURA  WHERE " & condizioneStruttura & " ID_VOCE =PF_VOCI.ID)" _
                & "+NVL((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE " & condizioneStruttura & " ID_VOCE_DA IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND SUBSTR(DATA_ORA,1,8)>'" & DATAAL & "'),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE " & condizioneStruttura & " ID_VOCE_A IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND SUBSTR(DATA_ORA,1,8)>'" & DATAAL & "'),0) " _
                & "-NVL((SELECT SUM(NVL(IMPORTO_APPROVATO,0)) FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO WHERE " & condizioneStruttura & " ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND PF_ASSESTAMENTO.ID=PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO AND ID_PF_MAIN=" & Request.QueryString("EF_R") & " AND ID_STATO>=5 AND DATA_APP_COMUNE>'" & DATAAL & "'),0) " _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                & "),'999G999G990D99')) AS RESIDUI_FINALI " _
                & "FROM SISCOM_MI.PF_VOCI WHERE FL_CC=1 " _
                & "AND ID_PIANO_FINANZIARIO='" & Request.QueryString("EF_R") & "' order by 1"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            par.cmd.CommandText = ""
            If dt.Rows.Count > 0 Then
                DataGrid.DataSource = dt
                DataGrid.DataBind()
                Dim idpfmainprec As Integer = 0
                Dim dataalprec As String = ""
                par.cmd.CommandText = "SELECT pf_main.id,FINE FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND SUBSTR(FINE,1,4)='" & ANNOP & "'"
                Dim lettoreAnno As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreAnno.Read Then
                    idpfmainprec = par.IfNull(lettoreAnno(0), 0)
                    dataalprec = par.IfNull(lettoreAnno(1), 0)
                End If
                lettoreAnno.Close()

                For Each RIGA As DataGridItem In DataGrid.Items

                    If par.EliminaLink(RIGA.Cells(2).Text) = "&nbsp;" Then
                        RIGA.Cells(2).Text = "0.00"
                    End If
                    If par.EliminaLink(RIGA.Cells(3).Text) = "&nbsp;" Then
                        RIGA.Cells(3).Text = "0.00"
                    End If
                    If par.EliminaLink(RIGA.Cells(4).Text) = "&nbsp;" Then
                        RIGA.Cells(4).Text = "0.00"
                    End If
                    If par.EliminaLink(RIGA.Cells(5).Text) = "&nbsp;" Then
                        RIGA.Cells(5).Text = "0.00"
                    End If
                    If par.EliminaLink(RIGA.Cells(6).Text) = "&nbsp;" Then
                        RIGA.Cells(6).Text = "0.00"
                    End If
                    If par.EliminaLink(RIGA.Cells(7).Text) = "&nbsp;" Then
                        RIGA.Cells(7).Text = "0.00"
                    End If


                    Dim codicevoce As String = RIGA.Cells(0).Text
                    par.cmd.CommandText = "SELECT (SELECT SUM(NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0)+NVL(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_STRUTTURA  WHERE " & condizioneStruttura & " ID_VOCE =PF_VOCI.ID)" _
                        & "+NVL((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE " & condizioneStruttura & " ID_VOCE_DA IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND SUBSTR(DATA_ORA,1,8)>'" & dataalprec & "'),0)" _
                        & "-NVL((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE " & condizioneStruttura & " ID_VOCE_A IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND SUBSTR(DATA_ORA,1,8)>'" & dataalprec & "'),0) " _
                        & "-NVL((SELECT SUM(NVL(IMPORTO_APPROVATO,0)) FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO WHERE " & condizioneStruttura & " ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID=PF_VOCI.ID) AND PF_ASSESTAMENTO.ID=PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO AND ID_PF_MAIN=" & idpfmainprec & " AND ID_STATO>=5 AND DATA_APP_COMUNE>'" & dataalprec & "'),0) " _
                        & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & dataalprec & "' AND ANNO_MANDATO='" & ANNOP & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                        & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & dataalprec & "' AND ANNO_MANDATO='" & ANNOP & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                        & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & dataalprec & "' AND ANNO_MANDATO>='" & ANNO & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                        & "-NVL((SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & dataalprec & "' AND ANNO_MANDATO>='" & ANNO & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF =PF_VOCI.ID),0)" _
                        & " AS RESIDUI_FINALI " _
                        & "FROM SISCOM_MI.PF_VOCI WHERE FL_CC=1 " _
                        & "AND ID_PIANO_FINANZIARIO='" & idpfmainprec & "' " _
                        & "AND CODICE='" & codicevoce & "'"
                    Dim LettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If LettoreVoce.Read Then
                        RIGA.Cells(2).Text = par.IfNull(LettoreVoce(0), 0)
                    Else
                        RIGA.Cells(2).Text = 0
                    End If
                    LettoreVoce.Close()

                    RIGA.Cells(2).Text = Format(CDec(RIGA.Cells(2).Text), "##,##0.00")
                    RIGA.Cells(4).Text = Format(CDec(RIGA.Cells(2).Text) + CDec(RIGA.Cells(3).Text), "##,##0.00")
                    'RIGA.Cells(7).Text = Format(CDec(RIGA.Cells(7).Text) + CDec(RIGA.Cells(2).Text), "##,##0.00")
                    RIGA.Cells(7).Text = Format(CDec(RIGA.Cells(7).Text), "##,##0.00")

                    totale += CType(par.EliminaLink(RIGA.Cells(2).Text), Decimal)
                    totale2 += CType(par.EliminaLink(RIGA.Cells(3).Text), Decimal)
                    totale3 += CType(par.EliminaLink(RIGA.Cells(4).Text), Decimal)
                    totale4 += CType(par.EliminaLink(RIGA.Cells(5).Text), Decimal)
                    totale5 += CType(par.EliminaLink(RIGA.Cells(6).Text), Decimal)
                    totale6 += CType(par.EliminaLink(RIGA.Cells(7).Text), Decimal)
                Next

                CType(DataGrid.Controls(0).Controls(DataGrid.Controls(0).Controls.Count - 1), DataGridItem).Cells(2).Text = Format(totale, "#,##0.00")
                CType(DataGrid.Controls(0).Controls(DataGrid.Controls(0).Controls.Count - 1), DataGridItem).Cells(3).Text = Format(totale2, "#,##0.00")
                CType(DataGrid.Controls(0).Controls(DataGrid.Controls(0).Controls.Count - 1), DataGridItem).Cells(4).Text = Format(totale3, "#,##0.00")
                CType(DataGrid.Controls(0).Controls(DataGrid.Controls(0).Controls.Count - 1), DataGridItem).Cells(5).Text = Format(totale4, "#,##0.00")
                CType(DataGrid.Controls(0).Controls(DataGrid.Controls(0).Controls.Count - 1), DataGridItem).Cells(6).Text = Format(totale5, "#,##0.00")
                CType(DataGrid.Controls(0).Controls(DataGrid.Controls(0).Controls.Count - 1), DataGridItem).Cells(7).Text = Format(totale6, "#,##0.00")

            Else
                Errore.Text = "Nessun dato disponibile per l'esercizio finanziario selezionato!"
            End If
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            btnStampa.Visible = False
            btnExport.Visible = False
            Errore.Text = "Si è verificato un errore durante il caricamento dei dati!"
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        'CHIUSURA CONNESSIONE
        '************************
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '************************
    End Sub
    Protected Sub ApriConnessione()
        'APERTURA CONNESSIONE E TRANSAZIONE
        '************************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        '************************
    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGrid, "ExportCC")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim nomeFile As String = par.StampaDataGridPDF(DataGrid, "StampaCC", Titolo.Text)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Write("<script>window.open('../../../FileTemp/" & nomeFile & "');</script>")
            FIN.Value = "1"
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
        End If
    End Sub
End Class
