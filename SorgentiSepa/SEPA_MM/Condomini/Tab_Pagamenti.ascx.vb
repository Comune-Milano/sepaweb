Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Partial Class Condomini_Tab_Pagamenti
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim sUnita(19) As String
    Dim sDecina(9) As String
    Dim dt As New Data.DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdCondominio.ToString) Then
                Cerca()
            End If
        End If
    End Sub

    Public Sub Cerca()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 1. Scad. '|| TO_CHAR(TO_DATE(RATA_1_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO, " _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 1 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_1_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 2. Scad. '|| TO_CHAR(TO_DATE(RATA_2_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO, " _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 2 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_2_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 3. Scad. '|| TO_CHAR(TO_DATE(RATA_3_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO," _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 3 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_3_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 4. Scad. '|| TO_CHAR(TO_DATE(RATA_4_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO," _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 4 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_4_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 5. Scad. '|| TO_CHAR(TO_DATE(RATA_5_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO," _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 5 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_5_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO, " _
                                & "('RATA 6. Scad. '|| TO_CHAR(TO_DATE(RATA_6_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO, " _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 6 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO, " _
                                & "COND_GESTIONE.RATA_6_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE, " _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_MOROSITA.ID,PRENOTAZIONI.ID_PAGAMENTO, ('MOROSITA''CONDOMINIALE') AS DESCRIZIONE, " _
                                & "TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, PAGAMENTI.PROGR," _
                                & "TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO,(CASE WHEN PAGAMENTI.ID IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM SISCOM_MI.COND_MOROSITA, SISCOM_MI.PAGAMENTI,siscom_mi.prenotazioni " _
                                & "WHERE COND_MOROSITA.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & "  " _
                                & "AND PRENOTAZIONI.ID = COND_MOROSITA.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID(+) "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            Dim Totale As Double = 0
            Dim r As Data.DataRow
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            For Each row As Data.DataRow In dt.Rows
                par.cmd.CommandText = "select num_mandato, TO_CHAR(TO_DATE(data_mandato,'yyyymmdd'),'dd/mm/yyyy') as data_mandato from siscom_mi.pagamenti_liquidati where id_pagamento = " & par.IfNull(row.Item("id_pagamento"), 0)
                lettore = par.cmd.ExecuteReader
                If lettore.HasRows Then
                    If lettore.Read Then
                        row.Item("num_data_mandato") = "Num." & par.IfNull(lettore("num_mandato"), "") & " del " & par.IfNull(lettore("data_mandato"), "")
                    End If
                Else
                    row.Item("num_data_mandato") = "- - -"
                End If
                lettore.Close()
                Totale = Totale + par.IfNull(row.Item("IMPORTO_CONSUNTIVATO"), 0)
            Next
            r = dt.NewRow
            r.Item("DESCRIZIONE") = "T O T A L E"
            r.Item("IMPORTO_CONSUNTIVATO") = Format(Totale, "##,##0.00")
            dt.Rows.Add(r)
            DataGridPagamenti.DataSource = dt
            DataGridPagamenti.DataBind()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabPagamenti"
        End Try
    End Sub
    Protected Sub DataGridPagamenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridPagamenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('Tab_Pagamenti1_txtmia').value='Hai selezionato :" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('Tab_Pagamenti1_txtidPagamento').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Pagamenti1_txtDescrizione').value='" & e.Item.Cells(1).Text.Replace("'", "\'") & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('Tab_Pagamenti1_txtmia').value='Hai selezionato :" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('Tab_Pagamenti1_txtidPagamento').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Pagamenti1_txtDescrizione').value='" & e.Item.Cells(1).Text.Replace("'", "\'") & "';")
        End If
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridPagamenti, "ExportPagamentiCondominio", , , , False)
            If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                Response.Redirect("..\/FileTemp/" & nomefile, False)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabPagamenti"
        End Try
    End Sub
    '******************************************************************************
    '                               NumeroToLettere
    '
    '                Converte il numero intero in lettere
    '
    ' Input : ImportoN                -->Importo Numerico
    '
    ' Ouput : NumeroToLettere         -->Il numero in lettere
    '******************************************************************************
    Function NumeroInLettere(ByVal Numero As String) As String

        '************************
        'Gestisce la virgola
        '************************
        Dim PosVirg As Integer
        Dim Lettere As String

        Numero$ = ChangeStr(Numero$, ".", "")
        PosVirg% = InStr(Numero$, ",")

        If PosVirg% Then
            Lettere$ = NumInLet(Mid(Numero$, 1, Len(Numero) + PosVirg% - 1))
            Lettere$ = Lettere$ & "\" & Format(CInt(Mid(Numero$, PosVirg% + 1, Len(Numero$))), "00")
        Else
            Lettere$ = NumInLet(CDbl(Numero$))
        End If

        NumeroInLettere = Lettere$

    End Function

    Private Function NumInLet(ByVal N As Double) As String

        '************************************************
        'inizializzo i due arry di numeri
        '************************************************
        SetNumeri()

        Dim ValT As Double     'Valore Temporaneo per la conversione
        Dim iCent As Integer    'Valore su cui calcolare le centinaia
        Dim L As String     'Importo in Lettere

        NumInLet = "zero"

        If N = 0 Then Exit Function

        ValT = N
        L = ""

        'miliardi
        iCent = Int(ValT / 1000000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = "unmiliardo"
            Else
                L = LCent(iCent) + "miliardi"
            End If
            ValT = ValT - CDbl(iCent) * 1000000000.0#
        End If

        'milioni
        iCent = Int(ValT / 1000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = L + "unmilione"
            Else
                L = L + LCent(iCent) + "milioni"
            End If
            ValT = ValT - CDbl(iCent) * 1000000.0#
        End If

        'miliaia
        iCent = Int(ValT / 1000)
        If iCent Then
            If iCent = 1 Then
                L = L + "mille"
            Else
                L = L + LCent(iCent) + "mila"
            End If
            ValT = ValT - CDbl(iCent) * 1000
        End If

        ''centinaia
        'If ValT Then
        '    L = L + LCent(CInt(ValT))
        'End If
        If ValT Then
            L = L + LCent(Fix(CDbl(ValT)))
        End If

        NumInLet = L

    End Function

    Function LCent(ByVal N As Integer) As String

        ' Ritorna xx% (1/999) convertito in lettere
        Dim Numero As String
        Dim Lettere As String
        Dim Centinaia As Integer
        Dim Decine As Integer
        Dim x As Integer
        Dim Unita As Integer
        Dim sDec As String

        Numero$ = Format(N, "000")

        Lettere$ = ""
        Centinaia% = Val(Left$(Numero$, 1))
        If Centinaia% Then
            If Centinaia% > 1 Then
                Lettere = sUnita(Centinaia%)
            End If
            Lettere = Lettere + "cento"
        End If

        Decine% = (N Mod 100)
        If Decine% Then
            Select Case Decine%
                Case Is >= 20                               'Decine
                    sDec = sDecina(Val(Mid$(Numero$, 2, 1)))
                    x% = Len(sDec)
                    Unita% = Val(Right$(Numero$, 1))          'Unita
                    If Unita% = 1 Or Unita% = 8 Then x% = x% - 1
                    Lettere$ = Lettere$ & Left(sDec, x%) & sUnita(Unita%)    'Tolgo l'ultima lettera della decina per i
                Case Else
                    Lettere$ = Lettere$ + sUnita(Decine)
            End Select
        End If

        LCent$ = Lettere$

    End Function


    Sub SetNumeri()

        '************************************************
        ' Stringhe per traslitterazione numeri
        '************************************************
        sUnita(1) = "uno"
        sUnita(2) = "due"
        sUnita(3) = "tre"
        sUnita(4) = "quattro"
        sUnita(5) = "cinque"
        sUnita(6) = "sei"
        sUnita(7) = "sette"
        sUnita(8) = "otto"
        sUnita(9) = "nove"
        sUnita(10) = "dieci"
        sUnita(11) = "undici"
        sUnita(12) = "dodici"
        sUnita(13) = "tredici"
        sUnita(14) = "quattordici"
        sUnita(15) = "quindici"
        sUnita(16) = "sedici"
        sUnita(17) = "diciassette"
        sUnita(18) = "diciotto"
        sUnita(19) = "diciannove"

        sDecina(1) = "dieci"
        sDecina(2) = "venti"
        sDecina(3) = "trenta"
        sDecina(4) = "quaranta"
        sDecina(5) = "cinquanta"
        sDecina(6) = "sessanta"
        sDecina(7) = "settanta"
        sDecina(8) = "ottanta"
        sDecina(9) = "novanta"

    End Sub

    '*********************************************************************
    '                ChangeStr - da usare con versioni minori del Vb6
    '
    'Input  = Stringa                           -->Da convertire
    '         Lettera da sostituire             -->Da convertire
    '         Nuova lettera da rimpiazzare      -->Da convertire
    '
    'Ouput  = Stringa rimpiazzata
    '
    '*********************************************************************
    Function ChangeStr(ByRef sBuffer As String, ByRef OldChar As String, _
                       ByRef NewChar As String) As String

        Dim TmpBuf As String
        Dim p As Integer

        On Error GoTo ErrChangeStr

        ChangeStr$ = ""   'Default Error

        TmpBuf$ = sBuffer$
        p% = InStr(TmpBuf$, OldChar$)
        Do While p > 0
            TmpBuf$ = Left$(TmpBuf$, p% - 1) + NewChar$ + Mid$(TmpBuf$, p% + Len(OldChar$))
            p% = InStr(p% + Len(NewChar$), TmpBuf$, OldChar$)
        Loop
        ChangeStr$ = TmpBuf$

        Exit Function

ErrChangeStr:
        ChangeStr$ = ""

    End Function





End Class
