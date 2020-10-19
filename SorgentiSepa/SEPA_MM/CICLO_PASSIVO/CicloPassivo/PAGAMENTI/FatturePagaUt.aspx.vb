Imports ExpertPdf.HtmlToPdf
Imports System.IO

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FatturePagaUt
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim sUnita(19) As String
    Dim sDecina(9) As String

    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then

            'CaricaEsercizio()
            CaricaParam()
            If idPagamento.Value > 0 Then
                Me.btnSalva.Visible = False
                Me.btnSalStampa.Visible = True
                CaricaDatiPagam()
            End If
            txtEmissione.Text = Format(Now, "dd/MM/yyyy")
            txtEmissione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
        'If Request.QueryString("TIPO") = "C" Then
        '    Me.lblTitolo.Text = "Pagamento Custodi"

        'End If

    End Sub
    Private Sub CaricaDatiPagam()
        connData.apri(True)
        par.cmd.CommandText = "select getdata(data_scadenza) as data_scadenza, descrizione_breve from siscom_mi.pagamenti where id = " & idPagamento.Value

        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If reader.Read Then
            Me.txtScadenza.Text = par.IfNull(reader("data_scadenza"), "")
            Me.txtdescrizione.Text = par.IfNull(reader("descrizione_breve"), "")
        End If
        reader.Close()
        connData.chiudi(True)

    End Sub
    Private Function CtrlResiduo() As Decimal
        CtrlResiduo = -1

        If Request.QueryString("TIPO") = "U" Then
            par.cmd.CommandText = "select distinct id_voce_pf,id_struttura from siscom_mi.prenotazioni where id in (select id_prenotazione from siscom_mi.fatture_utenze where " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", True) & ")"
        ElseIf Request.QueryString("TIPO") = "M" Then
            par.cmd.CommandText = "select distinct id_voce_pf,id_struttura from siscom_mi.prenotazioni where id in (select id_prenotazione from siscom_mi.MULTE where id in (" & Session.Item("idSel").ToString & "))"
        ElseIf Request.QueryString("TIPO") = "COSAP" Then
            par.cmd.CommandText = "select distinct id_voce_pf,id_struttura from siscom_mi.prenotazioni where id in (select id_prenotazione from siscom_mi.COSAP where id in (" & Session.Item("idSel").ToString & "))"
        Else
            par.cmd.CommandText = "select distinct id_voce_pf,id_struttura from siscom_mi.prenotazioni where id in (select id_prenotazione from siscom_mi.pagamenti_custodi where id in (" & Session.Item("idSel").ToString & "))"
        End If

        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        Dim TotBilancio As Decimal = 0
        Dim TotPrenotato As Decimal = 0
        Dim TotApprovato As Decimal = 0

        While reader.Read
            TotBilancio = 0
            TotPrenotato = 0
            TotApprovato = 0
            par.cmd.CommandText = "select sum(nvl(valore_lordo,0) + nvl(assestamento_valore_lordo,0) + nvl(variazioni,0)) from siscom_mi.pf_voci_struttura where id_Voce = " & par.IfNull(reader("id_voce_pf"), 0) & " and id_struttura = " & par.IfNull(reader("id_struttura"), 0)
            TotBilancio = par.cmd.ExecuteScalar

            par.cmd.CommandText = "select round(sum(nvl(importo_prenotato,0)-(nvl(importo_prenotato,0)*nvl(perc_iva,0))/100),2)  as tot  from siscom_mi.prenotazioni where  id_voce_pf = " & par.IfNull(reader("id_voce_pf"), 0) & " and id_struttura = " & par.IfNull(reader("id_struttura"), 0) & " and id_stato = 0"
            TotPrenotato = par.IfNull(par.cmd.ExecuteScalar, 0)

            par.cmd.CommandText = "select round(sum(nvl(importo_approvato,0)-(nvl(importo_approvato,0)*nvl(perc_iva,0))/100),2)  as tot  from siscom_mi.prenotazioni where  id_voce_pf = " & par.IfNull(reader("id_voce_pf"), 0) & " and id_struttura = " & par.IfNull(reader("id_struttura"), 0) & " and id_stato > 0 "
            TotApprovato = CDec(par.IfNull(par.cmd.ExecuteScalar, 0))

            CtrlResiduo = TotBilancio - TotPrenotato - TotApprovato
            If CtrlResiduo < 0 Then
                Exit While
            End If
        End While
        reader.Close()


    End Function

    'Private Sub CaricaEsercizio()
    '    Try

    '        Dim strQuery As String = "SELECT PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO as idpf,PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY')||' - '||TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS esercizio " _
    '                                & "FROM siscom_mi.T_ESERCIZIO_FINANZIARIO,siscom_mi.PF_MAIN,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
    '                                & "WHERE T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.id_esercizio_finanziario AND PF_MAIN.ID = PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO " _
    '                                & "AND id_tipo_utenza = 1"
    '        par.caricaComboBox(strQuery, cmbEsercizio, "idpf", "esercizio", False)
    '    Catch ex As Exception
    '        If connData.Connessione.State = Data.ConnectionState.Open Then
    '            connData.chiudi()
    '        End If
    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", Page.Title & " CaricaEsercizio - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")


    '    End Try

    'End Sub
    Private Sub CaricaParam()
        Try
            connData.apri()
            connData.RiempiPar(par)
            If Not String.IsNullOrEmpty(Request.QueryString("idpag")) Then
                idPagamento.Value = Request.QueryString("idpag")
            End If

            If idPagamento.Value = 0 Then
                If Request.QueryString("TIPO") = "U" Then
                    par.cmd.CommandText = "select sum(base_imponibile + iva + totale_oneri_diversi) from siscom_mi.fatture_utenze where " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", True)

                    Me.txtImporto.Text = Format(Math.Round(CDec(par.IfNull(par.cmd.ExecuteScalar, 0)), 2), "##,##0.00")

                    par.cmd.CommandText = "select distinct id_fornitore from siscom_mi.fatture_utenze,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = pagamenti_utenze_voci.id_voce_pf " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", False)



                ElseIf Request.QueryString("TIPO") = "M" Then
                    par.cmd.CommandText = "select sum(nvl(IMPORTO,0)) from siscom_mi.multe where  id  in (" & Session.Item("idSel") & ")"

                    Me.txtImporto.Text = Format(Math.Round(CDec(par.IfNull(par.cmd.ExecuteScalar, 0)), 2), "##,##0.00")

                    par.cmd.CommandText = "select distinct id_fornitore from siscom_mi.MULTE,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = pagamenti_utenze_voci.id_voce_pf and multe.id in (" & Session.Item("idSel") & ")"

                ElseIf Request.QueryString("TIPO") = "COSAP" Then
                    par.cmd.CommandText = "select sum(nvl(IMPORTO,0)) from siscom_mi.COSAP where  id  in (" & Session.Item("idSel") & ")"

                    Me.txtImporto.Text = Format(Math.Round(CDec(par.IfNull(par.cmd.ExecuteScalar, 0)), 2), "##,##0.00")

                    par.cmd.CommandText = "select distinct id_fornitore from siscom_mi.COSAP,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = pagamenti_utenze_voci.id_voce_pf and COSAP.id in (" & Session.Item("idSel") & ")"

                Else
                    par.cmd.CommandText = "select sum(importo) from siscom_mi.pagamenti_custodi where id in (" & Session.Item("idSel").ToString & ")"
                    Me.txtImporto.Text = Format(Math.Round(CDec(par.IfNull(par.cmd.ExecuteScalar, 0)), 2), "##,##0.00")

                    par.cmd.CommandText = "select distinct id_fornitore from siscom_mi.PAGAMENTI_CUSTODI,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = pagamenti_utenze_voci.id_voce_pf and PAGAMENTI_CUSTODI.id in (" & Session.Item("idSel") & ")"

                End If
            Else
                'par.cmd.CommandText = "select distinct id_fornitore from siscom_mi.fatture_utenze,siscom_mi.pf_voci where " _
                '                    & "pf_voci.id = id_voce_pf and fatture_utenze.id_prenotazione in (select id from prenotazioni where id_pagamento = " & idPagamento.Value & ")"


                par.cmd.CommandText = "select nvl(IMPORTO_CONSUNTIVATO,0) from siscom_mi.pagamenti where  id =" & idPagamento.Value
                Me.txtImporto.Text = Format(Math.Round(CDec(par.IfNull(par.cmd.ExecuteScalar, 0)), 2), "##,##0.00")


                par.cmd.CommandText = "select id_fornitore from siscom_mi.prenotazioni where id_pagamento = " & idPagamento.Value

            End If
            Me.idFornitore.Value = par.cmd.ExecuteScalar

            par.cmd.CommandText = "SELECT TIPO_MODALITA_PAG.ID AS idTipoModPag,TIPO_MODALITA_PAG.descrizione AS descrmodalita ," _
                                & "TIPO_PAGAMENTO.ID AS idtipopagamento,TIPO_PAGAMENTO.descrizione AS descrtipo " _
                                & "FROM siscom_Mi.TIPO_MODALITA_PAG,siscom_mi.FORNITORI, siscom_mi.TIPO_PAGAMENTO " _
                                & "WHERE TIPO_MODALITA_PAG.ID = FORNITORI.ID_TIPO_MODALITA_PAG " _
                                & "AND TIPO_PAGAMENTO.ID = FORNITORI.id_tipo_pagamento " _
                                & "AND FORNITORI.ID = " & idFornitore.Value
            Dim reader As Oracle.DataAccess.Client.OracleDataReader
            reader = par.cmd.ExecuteReader
            If reader.Read Then
                idModalita.Value = par.IfNull(reader("idTipoModPag"), "NULL")
                Me.txtModalita.Text = par.IfNull(reader("descrmodalita"), "")

                idCondizione.Value = par.IfNull(reader("idtipopagamento"), "NULL")
                Me.txtcondizione.Text = par.IfNull(reader("descrtipo"), "")
            End If
            reader.Close()
            par.cmd.CommandText = "select id from siscom_mi.fornitori_iban where id_fornitore = " & idFornitore.Value & " and fl_attivo = 1 order by id desc"
            reader = par.cmd.ExecuteReader
            If reader.Read Then
                idIban.Value = par.IfNull(reader(0), "null")
            End If
            reader.Close()
            par.cmd.CommandText = "select getdata(data_scadenza) from siscom_mi.pagamenti where id = " & idPagamento.Value

            Me.txtScadenza.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            If idPagamento.Value = 0 Then
                If Request.QueryString("TIPO") = "U" Then
                    par.cmd.CommandText = "select distinct pf_voci.id_piano_finanziario from siscom_mi.fatture_utenze,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = id_voce_pf " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", False)
                ElseIf Request.QueryString("TIPO") = "M" Then
                    par.cmd.CommandText = "select distinct pf_voci.id_piano_finanziario from siscom_mi.MULTE,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = id_voce_pf and MULTE.id in (" & Session.Item("idSel") & ")"

                ElseIf Request.QueryString("TIPO") = "COSAP" Then
                    par.cmd.CommandText = "select distinct pf_voci.id_piano_finanziario from siscom_mi.COSAP,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = id_voce_pf and COSAP.id in (" & Session.Item("idSel") & ")"

                Else
                    par.cmd.CommandText = "select distinct pf_voci.id_piano_finanziario from siscom_mi.PAGAMENTI_CUSTODI,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = id_voce_pf and PAGAMENTI_CUSTODI.id in (" & Session.Item("idSel") & ")"

                End If
            Else
                If Request.QueryString("TIPO") = "U" Then
                    par.cmd.CommandText = "select distinct pf_voci.id_piano_finanziario from siscom_mi.fatture_utenze,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = id_voce_pf and fatture_utenze.id_prenotazione in (select id from siscom_mi.prenotazioni where id_pagamento = " & idPagamento.Value & ")"
                Else
                    par.cmd.CommandText = "select distinct pf_voci.id_piano_finanziario from siscom_mi.PAGAMENTI_CUSTODI,siscom_mi.pagamenti_utenze_voci,siscom_mi.pf_voci where " _
                                        & "ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pf_voci.id = id_voce_pf and PAGAMENTI_CUSTODI.id_prenotazione in (select id from siscom_mi.prenotazioni where id_pagamento = " & idPagamento.Value & ")"

                End If

            End If
            Me.idEsercizio.Value = par.IfNull(par.cmd.ExecuteScalar, 0)
            connData.chiudi()
        Catch ex As Exception
            Session.Remove("idSel")

            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaEsercizio - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")


        End Try

    End Sub
    Private Function CalcolaDataScadenza(ByVal TipoModalita As String, ByVal tipoPagamento As String, ByVal DataScadPagamento As String) As String
        CalcolaDataScadenza = ""
        If String.IsNullOrEmpty(DataScadPagamento) Then

            If Not String.IsNullOrEmpty(TipoModalita) Then
                Dim openNow As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connData.RiempiPar(par)
                    openNow = True
                End If

                Dim Table As String = ""
                Dim Column As String = ""
                Dim FlSomma As Integer = 0
                Dim DaySum As Integer = 0
                par.cmd.CommandText = "SELECT tab_rif,fld_rif,fl_somma_giorni FROM siscom_mi.TAB_DATE_MODALITA_PAG WHERE ID = (SELECT id_data_riferimento FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = " & par.IfEmpty(idModalita.Value, 0) & ")"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    Table = par.IfNull(lettore("tab_rif"), "")
                    Column = par.IfNull(lettore("fld_rif"), "")
                    FlSomma = par.IfNull(lettore("fl_somma_giorni"), "")
                End If
                lettore.Close()

                If Not String.IsNullOrEmpty(Table) And Not String.IsNullOrEmpty(Column) Then
                    par.cmd.CommandText = "select " & Column & " from siscom_Mi." & Table & " where id = " & par.IfEmpty(Me.idPagamento.Value, 0)
                    CalcolaDataScadenza = par.IfNull(par.cmd.ExecuteScalar, "")
                End If

                If Not String.IsNullOrEmpty(CalcolaDataScadenza) Then
                    If FlSomma = 1 Then
                        par.cmd.CommandText = "select nvl(num_giorni,0) from siscom_mi.tipo_pagamento where id = " & par.IfEmpty(tipoPagamento, 0)
                        DaySum = par.IfNull(par.cmd.ExecuteScalar, 0)

                        If DaySum > 0 Then
                            CalcolaDataScadenza = Date.Parse(par.FormattaData(CalcolaDataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddDays(DaySum).ToString("dd/MM/yyyy")
                            CalcolaDataScadenza = par.AggiustaData(CalcolaDataScadenza)
                        End If
                    End If
                End If
                If openNow = True Then
                    connData.chiudi(False)
                End If
            End If
        End If

        If String.IsNullOrEmpty(CalcolaDataScadenza) Then
            CalcolaDataScadenza = DataScadPagamento
        End If

    End Function
    Private Sub SalvaDatiCustodi()
        Try
            'SALVA LE PRENOTAZIONI
            connData.apri(True)
            connData.RiempiPar(par)
            Dim idPrenot As Integer = 0

            Dim idVocePf As Integer = 0
            Dim idVocePfImporto As String = ""
            Dim idStruttura As Integer = 0

            Dim importoPrenotazione As Decimal = 0
            Dim readImporti As Oracle.DataAccess.Client.OracleDataReader

            Dim lstFatture() = Session.Item("idSel").ToString.Split(",")
            par.cmd.CommandText = "select sum(importo) from siscom_mi.pagamenti_custodi where id in (" & Session.Item("idSel").ToString & ")"
            Dim totCdp As Decimal
            totCdp = par.IfNull(par.cmd.ExecuteScalar, 0)
            If totCdp > 0 Then

                For Each s As String In lstFatture
                    idVocePf = 0
                    idVocePfImporto = ""

                    par.cmd.CommandText = "select id_voce_pf from siscom_mi.pagamenti_custodi,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pagamenti_custodi.id = " & s
                    idVocePf = par.IfNull(par.cmd.ExecuteScalar, 0)

                    par.cmd.CommandText = "select id_voce_pf_importo from siscom_mi.pagamenti_custodi,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and pagamenti_custodi.id = " & s
                    idVocePfImporto = par.IfNull(par.cmd.ExecuteScalar, "null")

                    par.cmd.CommandText = "SELECT ID_STRUTTURA FROM SISCOM_MI.PAGAMENTI_UTENZE_VOCI, SISCOM_MI.pagamenti_custodi " _
                                        & "WHERE pagamenti_custodi.ID = " & s & " AND ID_PARAM_UTENZA = PAGAMENTI_UTENZE_VOCI.ID " '_
                    '& "FATTURE_UTENZE.ID_TIPO_UTENZA = PAGAMENTI_UTENZE_VOCI.ID_TIPO_UTENZA AND " _
                    '& "FATTURE_UTENZE.ID_FORNITORE = PAGAMENTI_UTENZE_VOCI.ID_FORNITORE AND " _
                    '& "FATTURE_UTENZE.ID_VOCE_PF = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF AND " _
                    '& "FATTURE_UTENZE.ID_VOCE_PF_IMPORTO = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF_IMPORTO AND " _
                    '& " ID_PIANO_FINANZIARIO = " & Me.idEsercizio.Value
                    idStruttura = par.IfNull(par.cmd.ExecuteScalar, 0)

                    importoPrenotazione = 0
                    idPrenot = 0
                    par.cmd.CommandText = "select sum(importo) as totale from siscom_mi.pagamenti_custodi where id = " & s
                    readImporti = par.cmd.ExecuteReader
                    If readImporti.Read Then
                        importoPrenotazione = par.IfNull(readImporti("totale"), 0)
                    End If
                    readImporti.Close()
                    If idVocePf > 0 And idVocePfImporto <> "" Then 'And importoPrenotazione > 0 <--possono inserire prenotazioni negative! MAIL CARIATI 27/07/2015
                        par.cmd.CommandText = "select siscom_mi.seq_prenotazioni.nextval from dual"
                        idPrenot = par.cmd.ExecuteScalar

                        par.cmd.CommandText = "insert into siscom_mi.prenotazioni (ID, ID_FORNITORE, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO, DESCRIZIONE, TIPO_PAGAMENTO,DATA_PRENOTAZIONE, IMPORTO_PRENOTATO,ID_STRUTTURA) values " _
                                            & "(" & idPrenot & "," & idFornitore.Value & "," & idVocePf & "," & idVocePfImporto & ",1," & par.insDbValue("PAGAMENTO CUSTODI", True) & ",13,'" & Format(Now, "yyyyMMdd") & "'," & par.insDbValue(importoPrenotazione, False) & "," & idStruttura & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "update siscom_mi.pagamenti_custodi set id_prenotazione = " & idPrenot & " where id = " & s
                        par.cmd.ExecuteNonQuery()

                    End If

                Next
                If CtrlResiduo() > 0 Then
                    'SALVA IL PAGAMENTO
                    par.cmd.CommandText = "select siscom_mi.seq_pagamenti.nextval from dual"
                    idPagamento.Value = par.cmd.ExecuteScalar
                    Dim ImpConsuntivato As Decimal = 0
                    par.cmd.CommandText = "select round(sum(importo),2) from siscom_mi.pagamenti_custodi where id in (" & Session.Item("idSel") & ")"
                    ImpConsuntivato = par.cmd.ExecuteScalar
                    par.cmd.CommandText = "INSERT INTO siscom_mi.PAGAMENTI (ID, DATA_EMISSIONE, DESCRIZIONE, IMPORTO_CONSUNTIVATO, ID_FORNITORE,ID_IBAN_FORNITORE, ID_STATO, TIPO_PAGAMENTO,ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO, DATA_SCADENZA, DESCRIZIONE_BREVE) VALUES " _
                                        & "(" & idPagamento.Value & "," & par.insDbValue(Me.txtEmissione.Text, True, True) & ",'PAGAMENTO CUSTODI'," & par.VirgoleInPunti(ImpConsuntivato) & "," & idFornitore.Value & "," & idIban.Value & ",1,13" _
                                        & "," & idModalita.Value & "," & idCondizione.Value & "," & par.insDbValue(txtScadenza.Text, True, True) & "," & par.insDbValue(txtdescrizione.Text, True) & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update siscom_mi.prenotazioni set ID_STATO = 2,id_pagamento = " & idPagamento.Value & ",importo_approvato = importo_prenotato where id in (select id_prenotazione from siscom_mi.pagamenti_custodi where id in (" & Session.Item("idSel") & "))"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    RadWindowManager1.RadAlert("CDP memorizzato correttamente!", 300, 150, "Attenzione", "", "null")
                    'SE TUTTO OK
                    Me.btnSalStampa.Visible = True
                Else
                    RadWindowManager1.RadAlert("Residuo BP insufficiente per generare il CDP!", 300, 150, "Attenzione", "", "null")
                    connData.chiudi(False)
                End If
                Me.txtScadenza.Text = CalcolaDataScadenza(par.IfEmpty(idModalita.Value, 0), par.IfEmpty(idCondizione.Value, 0), Me.txtScadenza.Text)
            Else
                RadWindowManager1.RadAlert("Non è possibile emettere un CDP con totale uguale o inferiore di 0!", 300, 150, "Attenzione", "", "null")
            End If

        Catch ex As Exception
            Session.Remove("idSel")

            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaDati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

        End Try
    End Sub
    Private Function splitIN(ByVal ins As String, ByVal preIn As String, ByVal noAnd As Boolean) As String
        Dim condIdSel As String = ""
        Dim s As Generic.List(Of String)
        Dim link As String = "AND"
        If noAnd = True Then
            link = ""
        End If
        s = par.QueryINSplit(ins, " " & preIn & " (#SOST#) ", "#SOST#")
        condIdSel = ""
        Select Case s.Count
            Case 1
                condIdSel &= " " & link & " " & s(0) & " "
            Case Else
                condIdSel &= " " & link & " ( "
                For i As Integer = 0 To s.Count - 1
                    If i = 0 Then
                        condIdSel &= s(i) & " "
                    Else
                        condIdSel &= " OR " & s(i) & " "
                    End If
                Next
                condIdSel &= " ) "
        End Select
        Return condIdSel
    End Function
    Private Sub SalvaDati()
        Try
            'SALVA LE PRENOTAZIONI
            connData.apri(True)
            connData.RiempiPar(par)
            Dim idPrenot As Integer = 0

            Dim idVocePf As Integer = 0
            Dim idVocePfImporto As String = ""
            Dim idStruttura As Integer = 0

            Dim importoPrenotazione As Decimal = 0
            Dim percIva As Decimal = 0
            Dim oneri As Decimal = 0
            Dim readImporti As Oracle.DataAccess.Client.OracleDataReader

            Dim lstFatture() = Session.Item("idSel").ToString.Split(",")
            'par.cmd.CommandText = "select sum(base_imponibile + iva + totale_oneri_diversi) from siscom_mi.fatture_utenze where id in (" & Session.Item("idSel").ToString & ")"
            par.cmd.CommandText = "select sum(base_imponibile + iva + totale_oneri_diversi) from siscom_mi.fatture_utenze where " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", True)

            Dim totCdp As Decimal
            totCdp = par.IfNull(par.cmd.ExecuteScalar, 0)
            If totCdp > 0 Then

                For Each s As String In lstFatture
                    idVocePf = 0
                    idVocePfImporto = ""

                    par.cmd.CommandText = "select id_voce_pf from siscom_mi.fatture_utenze,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and fatture_utenze.id = " & s
                    idVocePf = par.IfNull(par.cmd.ExecuteScalar, 0)

                    par.cmd.CommandText = "select id_voce_pf_importo from siscom_mi.fatture_utenze,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and fatture_utenze.id = " & s
                    idVocePfImporto = par.IfNull(par.cmd.ExecuteScalar, "null")

                    par.cmd.CommandText = "SELECT ID_STRUTTURA FROM SISCOM_MI.PAGAMENTI_UTENZE_VOCI, SISCOM_MI.FATTURE_UTENZE " _
                                        & "WHERE FATTURE_UTENZE.ID = " & s & " AND ID_PARAM_UTENZA = PAGAMENTI_UTENZE_VOCI.ID " '_
                    '& "FATTURE_UTENZE.ID_TIPO_UTENZA = PAGAMENTI_UTENZE_VOCI.ID_TIPO_UTENZA AND " _
                    '& "FATTURE_UTENZE.ID_FORNITORE = PAGAMENTI_UTENZE_VOCI.ID_FORNITORE AND " _
                    '& "FATTURE_UTENZE.ID_VOCE_PF = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF AND " _
                    '& "FATTURE_UTENZE.ID_VOCE_PF_IMPORTO = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF_IMPORTO AND " _
                    '& " ID_PIANO_FINANZIARIO = " & Me.idEsercizio.Value
                    idStruttura = par.IfNull(par.cmd.ExecuteScalar, 0)

                    importoPrenotazione = 0
                    percIva = 0
                    oneri = 0
                    idPrenot = 0
                    par.cmd.CommandText = "select sum(base_imponibile + iva + totale_oneri_diversi)as totale,sum(iva)as iva,round(sum((iva*100)/(case when nvl(base_imponibile,0) = 0 then 1 else nvl(base_imponibile,1) end)),0)as perc_iva,sum(totale_oneri_diversi) as oneri from siscom_mi.fatture_utenze where id = " & s
                    readImporti = par.cmd.ExecuteReader
                    If readImporti.Read Then
                        importoPrenotazione = par.IfNull(readImporti("totale"), 0)
                        percIva = par.IfNull(readImporti("perc_iva"), 0)
                        oneri = par.IfNull(readImporti("oneri"), 0)
                    End If
                    readImporti.Close()
                    If idVocePf > 0 And idVocePfImporto <> "" Then 'And importoPrenotazione > 0 <--possono inserire prenotazioni negative! MAIL CARIATI 27/07/2015
                        par.cmd.CommandText = "select siscom_mi.seq_prenotazioni.nextval from dual"
                        idPrenot = par.cmd.ExecuteScalar

                        par.cmd.CommandText = "insert into siscom_mi.prenotazioni (ID, ID_FORNITORE, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO, DESCRIZIONE, TIPO_PAGAMENTO,DATA_PRENOTAZIONE, IMPORTO_PRENOTATO,PERC_IVA,ID_STRUTTURA,fuori_campo_iva) values " _
                                            & "(" & idPrenot & "," & idFornitore.Value & "," & idVocePf & "," & idVocePfImporto & ",1," & par.insDbValue("PAGAMENTO UTENZE", True) & ",12,'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(importoPrenotazione) & "," & percIva & "," & idStruttura & "," & par.VirgoleInPunti(oneri) & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "update siscom_mi.fatture_utenze set id_prenotazione = " & idPrenot & " where id = " & s
                        par.cmd.ExecuteNonQuery()

                    End If

                Next
                If CtrlResiduo() > 0 Then
                    'SALVA IL PAGAMENTO
                    par.cmd.CommandText = "select siscom_mi.seq_pagamenti.nextval from dual"
                    idPagamento.Value = par.cmd.ExecuteScalar
                    Dim ImpConsuntivato As Decimal = 0
                    par.cmd.CommandText = "select round(sum(totale_bolletta),2) from siscom_mi.fatture_utenze where " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", True)
                    ImpConsuntivato = par.cmd.ExecuteScalar
                    par.cmd.CommandText = "INSERT INTO siscom_mi.PAGAMENTI (ID, DATA_EMISSIONE, DESCRIZIONE, IMPORTO_CONSUNTIVATO, ID_FORNITORE,ID_IBAN_FORNITORE, ID_STATO, TIPO_PAGAMENTO,ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO, DATA_SCADENZA, DESCRIZIONE_BREVE) VALUES " _
                                        & "(" & idPagamento.Value & "," & par.insDbValue(Me.txtEmissione.Text, True, True) & ",'PAGAMENTO UTENZE'," & par.VirgoleInPunti(ImpConsuntivato) & "," & idFornitore.Value & "," & idIban.Value & ",1,12" _
                                        & "," & par.IfEmpty(idModalita.Value, "null") & "," & par.IfEmpty(idCondizione.Value, "null") & "," & par.insDbValue(txtScadenza.Text, True, True) & "," & par.insDbValue(txtdescrizione.Text, True) & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update siscom_mi.prenotazioni set ID_STATO = 2,id_pagamento = " & idPagamento.Value & ",importo_approvato = importo_prenotato where id in (select id_prenotazione from siscom_mi.fatture_utenze where " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", True) & ")"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    RadNotificationNote.Text = "CDP memorizzato correttamente!"
                    RadNotificationNote.Show()

                    'SE TUTTO OK
                    Me.btnSalStampa.Visible = True

                Else
                    RadWindowManager1.RadAlert("Residuo BP insufficiente per generare il CDP!", 300, 150, "Attenzione", "", "null")
                    connData.chiudi(False)

                End If

                Me.txtScadenza.Text = CalcolaDataScadenza(par.IfEmpty(idModalita.Value, 0), par.IfEmpty(idCondizione.Value, 0), Me.txtScadenza.Text)
            Else
                RadWindowManager1.RadAlert("Non è possibile emettere un CDP con totale uguale o inferiore di 0!", 300, 150, "Attenzione", "", "null")
            End If

        Catch ex As Exception
            Session.Remove("idSel")

            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaDati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

        End Try
    End Sub

    Private Sub SalvaDatiCosap()
        Try
            'SALVA LE PRENOTAZIONI
            connData.apri(True)
            connData.RiempiPar(par)
            Dim idPrenot As Integer = 0

            Dim idVocePf As Integer = 0
            Dim idVocePfImporto As String = ""
            Dim idStruttura As Integer = 0

            Dim importoPrenotazione As Decimal = 0
            Dim percIva As Decimal = 0
            Dim oneri As Decimal = 0
            Dim readImporti As Oracle.DataAccess.Client.OracleDataReader

            Dim lstFatture() = Session.Item("idSel").ToString.Split(",")
            par.cmd.CommandText = "select sum(IMPORTO) from siscom_mi.COSAP where id in (" & Session.Item("idSel").ToString & ")"
            Dim totCdp As Decimal
            totCdp = par.IfNull(par.cmd.ExecuteScalar, 0)
            If totCdp > 0 Then

                For Each s As String In lstFatture
                    idVocePf = 0
                    idVocePfImporto = ""

                    par.cmd.CommandText = "select id_voce_pf from siscom_mi.COSAP,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and COSAP.id = " & s
                    idVocePf = par.IfNull(par.cmd.ExecuteScalar, 0)

                    par.cmd.CommandText = "select id_voce_pf_importo from siscom_mi.COSAP,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and COSAP.id = " & s
                    idVocePfImporto = par.IfNull(par.cmd.ExecuteScalar, "null")

                    par.cmd.CommandText = "SELECT ID_STRUTTURA FROM SISCOM_MI.PAGAMENTI_UTENZE_VOCI, SISCOM_MI.COSAP " _
                                        & "WHERE COSAP.ID = " & s & " AND ID_PARAM_UTENZA = PAGAMENTI_UTENZE_VOCI.ID " '_
                    idStruttura = par.IfNull(par.cmd.ExecuteScalar, 0)

                    importoPrenotazione = 0
                    percIva = 0
                    oneri = 0
                    idPrenot = 0
                    par.cmd.CommandText = "select IMPORTO as totale from siscom_mi.COSAP where id = " & s
                    readImporti = par.cmd.ExecuteReader
                    If readImporti.Read Then
                        importoPrenotazione = par.IfNull(readImporti("totale"), 0)
                        'percIva = par.IfNull(readImporti("perc_iva"), 0)
                        'oneri = par.IfNull(readImporti("oneri"), 0)
                    End If
                    readImporti.Close()
                    If idVocePf > 0 And idVocePfImporto <> "" Then 'And importoPrenotazione > 0 <--possono inserire prenotazioni negative! MAIL CARIATI 27/07/2015
                        par.cmd.CommandText = "select siscom_mi.seq_prenotazioni.nextval from dual"
                        idPrenot = par.cmd.ExecuteScalar

                        par.cmd.CommandText = "insert into siscom_mi.prenotazioni (ID, ID_FORNITORE, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO, DESCRIZIONE, TIPO_PAGAMENTO,DATA_PRENOTAZIONE, IMPORTO_PRENOTATO,PERC_IVA,ID_STRUTTURA,fuori_campo_iva) values " _
                                            & "(" & idPrenot & "," & idFornitore.Value & "," & idVocePf & "," & idVocePfImporto & ",1," & par.insDbValue("PAGAMENTO COSAP", True) & ",16,'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(importoPrenotazione) & "," & percIva & "," & idStruttura & "," & par.VirgoleInPunti(oneri) & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "update siscom_mi.COSAP set id_prenotazione = " & idPrenot & " where id = " & s
                        par.cmd.ExecuteNonQuery()

                    End If

                Next
                If CtrlResiduo() > 0 Then
                    'SALVA IL PAGAMENTO
                    par.cmd.CommandText = "select siscom_mi.seq_pagamenti.nextval from dual"
                    idPagamento.Value = par.cmd.ExecuteScalar
                    Dim ImpConsuntivato As Decimal = 0
                    par.cmd.CommandText = "select round(sum(IMPORTO),2) from siscom_mi.COSAP where id in (" & Session.Item("idSel") & ")"
                    ImpConsuntivato = par.cmd.ExecuteScalar
                    par.cmd.CommandText = "INSERT INTO siscom_mi.PAGAMENTI (ID, DATA_EMISSIONE, DESCRIZIONE, IMPORTO_CONSUNTIVATO, ID_FORNITORE,ID_IBAN_FORNITORE, ID_STATO, TIPO_PAGAMENTO,ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO, DATA_SCADENZA, DESCRIZIONE_BREVE) VALUES " _
                                        & "(" & idPagamento.Value & "," & par.insDbValue(Me.txtEmissione.Text, True, True) & ",'PAGAMENTO COSAP'," & par.VirgoleInPunti(ImpConsuntivato) & "," & idFornitore.Value & "," & idIban.Value & ",1,16" _
                                        & "," & idModalita.Value & "," & idCondizione.Value & "," & par.insDbValue(txtScadenza.Text, True, True) & "," & par.insDbValue(txtdescrizione.Text, True) & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update siscom_mi.prenotazioni set ID_STATO = 2,id_pagamento = " & idPagamento.Value & ",importo_approvato = importo_prenotato where id in (select id_prenotazione from siscom_mi.COSAP where id in (" & Session.Item("idSel") & "))"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    RadNotificationNote.Text = "CDP memorizzato correttamente!"
                    RadNotificationNote.Show()
                    'SE TUTTO OK
                    Me.btnSalStampa.Visible = True
                Else
                    RadWindowManager1.RadAlert("Residuo BP insufficiente per generare il CDP!", 300, 150, "Attenzione", "", "null")
                    connData.chiudi(False)
                End If
                Me.txtScadenza.Text = CalcolaDataScadenza(idModalita.Value, idCondizione.Value, Me.txtScadenza.Text)
            Else
                RadWindowManager1.RadAlert("Non è possibile emettere un CDP con totale uguale o inferiore di 0!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            Session.Remove("idSel")
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaDati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try
    End Sub


    Private Sub SalvaDatiMulte()
        Try
            'SALVA LE PRENOTAZIONI
            connData.apri(True)
            connData.RiempiPar(par)
            Dim idPrenot As Integer = 0

            Dim idVocePf As Integer = 0
            Dim idVocePfImporto As String = ""
            Dim idStruttura As Integer = 0

            Dim importoPrenotazione As Decimal = 0
            Dim percIva As Decimal = 0
            Dim oneri As Decimal = 0
            Dim readImporti As Oracle.DataAccess.Client.OracleDataReader

            Dim lstFatture() = Session.Item("idSel").ToString.Split(",")
            par.cmd.CommandText = "select sum(IMPORTO) from siscom_mi.multe where id in (" & Session.Item("idSel").ToString & ")"
            Dim totCdp As Decimal
            totCdp = par.IfNull(par.cmd.ExecuteScalar, 0)
            If totCdp > 0 Then

                For Each s As String In lstFatture
                    idVocePf = 0
                    idVocePfImporto = ""

                    par.cmd.CommandText = "select id_voce_pf from siscom_mi.multe,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and multe.id = " & s
                    idVocePf = par.IfNull(par.cmd.ExecuteScalar, 0)

                    par.cmd.CommandText = "select id_voce_pf_importo from siscom_mi.multe,siscom_mi.pagamenti_utenze_voci where ID_PARAM_UTENZA = pagamenti_utenze_voci.id and multe.id = " & s
                    idVocePfImporto = par.IfNull(par.cmd.ExecuteScalar, "null")

                    par.cmd.CommandText = "SELECT ID_STRUTTURA FROM SISCOM_MI.PAGAMENTI_UTENZE_VOCI, SISCOM_MI.MULTE " _
                                        & "WHERE MULTE.ID = " & s & " AND ID_PARAM_UTENZA = PAGAMENTI_UTENZE_VOCI.ID " '_
                    idStruttura = par.IfNull(par.cmd.ExecuteScalar, 0)

                    importoPrenotazione = 0
                    percIva = 0
                    oneri = 0
                    idPrenot = 0
                    par.cmd.CommandText = "select IMPORTO as totale from siscom_mi.MULTE where id = " & s
                    readImporti = par.cmd.ExecuteReader
                    If readImporti.Read Then
                        importoPrenotazione = par.IfNull(readImporti("totale"), 0)
                        'percIva = par.IfNull(readImporti("perc_iva"), 0)
                        'oneri = par.IfNull(readImporti("oneri"), 0)
                    End If
                    readImporti.Close()
                    If idVocePf > 0 And idVocePfImporto <> "" Then 'And importoPrenotazione > 0 <--possono inserire prenotazioni negative! MAIL CARIATI 27/07/2015
                        par.cmd.CommandText = "select siscom_mi.seq_prenotazioni.nextval from dual"
                        idPrenot = par.cmd.ExecuteScalar

                        par.cmd.CommandText = "insert into siscom_mi.prenotazioni (ID, ID_FORNITORE, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO, DESCRIZIONE, TIPO_PAGAMENTO,DATA_PRENOTAZIONE, IMPORTO_PRENOTATO,PERC_IVA,ID_STRUTTURA,fuori_campo_iva) values " _
                                            & "(" & idPrenot & "," & idFornitore.Value & "," & idVocePf & "," & idVocePfImporto & ",1," & par.insDbValue("PAGAMENTO MULTE", True) & ",14,'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(importoPrenotazione) & "," & percIva & "," & idStruttura & "," & par.VirgoleInPunti(oneri) & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "update siscom_mi.MULTE set id_prenotazione = " & idPrenot & " where id = " & s
                        par.cmd.ExecuteNonQuery()

                    End If

                Next
                If CtrlResiduo() > 0 Then
                    'SALVA IL PAGAMENTO
                    par.cmd.CommandText = "select siscom_mi.seq_pagamenti.nextval from dual"
                    idPagamento.Value = par.cmd.ExecuteScalar
                    Dim ImpConsuntivato As Decimal = 0
                    par.cmd.CommandText = "select round(sum(IMPORTO),2) from siscom_mi.MULTE where id in (" & Session.Item("idSel") & ")"
                    ImpConsuntivato = par.cmd.ExecuteScalar
                    par.cmd.CommandText = "INSERT INTO siscom_mi.PAGAMENTI (ID, DATA_EMISSIONE, DESCRIZIONE, IMPORTO_CONSUNTIVATO, ID_FORNITORE,ID_IBAN_FORNITORE, ID_STATO, TIPO_PAGAMENTO,ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO, DATA_SCADENZA, DESCRIZIONE_BREVE) VALUES " _
                                        & "(" & idPagamento.Value & "," & par.insDbValue(Me.txtEmissione.Text, True, True) & ",'PAGAMENTO MULTE'," & par.VirgoleInPunti(ImpConsuntivato) & "," & idFornitore.Value & "," & idIban.Value & ",1,14" _
                                        & "," & idModalita.Value & "," & idCondizione.Value & "," & par.insDbValue(txtScadenza.Text, True, True) & "," & par.insDbValue(txtdescrizione.Text, True) & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update siscom_mi.prenotazioni set ID_STATO = 2,id_pagamento = " & idPagamento.Value & ",importo_approvato = importo_prenotato where id in (select id_prenotazione from siscom_mi.MULTE where id in (" & Session.Item("idSel") & "))"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    RadNotificationNote.Text = "CDP memorizzato correttamente!"
                    RadNotificationNote.Show()
                    'SE TUTTO OK
                    Me.btnSalStampa.Visible = True
                Else
                    RadWindowManager1.RadAlert("Residuo BP insufficiente per generare il CDP!", 300, 150, "Attenzione", "", "null")
                    connData.chiudi(False)
                End If
                Me.txtScadenza.Text = CalcolaDataScadenza(idModalita.Value, idCondizione.Value, Me.txtScadenza.Text)
            Else
                RadWindowManager1.RadAlert("Non è possibile emettere un CDP con totale uguale o inferiore di 0!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            Session.Remove("idSel")

            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaDati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

        End Try

        

    End Sub

    Private Sub Update(Optional Dstampa As String = "")
        Try

            'Aggiorna il pagamento
            connData.apri(True)
            connData.RiempiPar(par)
            If Not String.IsNullOrEmpty(Dstampa) Then
                par.cmd.CommandText = "update siscom_mi.pagamenti set " & Dstampa & " where id = " & idPagamento.Value & " and data_stampa is null"
                par.cmd.ExecuteNonQuery()
            End If
            Me.txtScadenza.Text = par.FormattaData(CalcolaDataScadenza(par.IfEmpty(idModalita.Value, 0), par.IfEmpty(idCondizione.Value, 0), par.AggiustaData(Me.txtScadenza.Text)))

            par.cmd.CommandText = "update siscom_mi.pagamenti set " _
                & " ID_TIPO_PAGAMENTO = " & par.IfEmpty(idCondizione.Value, "null") _
                & ",ID_TIPO_MODALITA_PAG = " & par.IfEmpty(idModalita.Value, "null") _
                & ",DATA_SCADENZA = " & par.insDbValue(Me.txtScadenza.Text, True, True) _
                & ",DESCRIZIONE_BREVE = " & par.insDbValue(Me.txtdescrizione.Text, True) _
                & " where id = " & idPagamento.Value
            par.cmd.ExecuteNonQuery()


            connData.chiudi(True)

        Catch ex As Exception
            Session.Remove("idSel")

            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaDati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

        End Try

    End Sub
   Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If idPagamento.Value = 0 Then
            If Request.QueryString("TIPO") = "U" Then
                SalvaDati()
            ElseIf Request.QueryString("TIPO") = "M" Then
                SalvaDatiMulte()
            ElseIf Request.QueryString("TIPO") = "COSAP" Then
                SalvaDatiCosap()
            Else
                SalvaDatiCustodi()
            End If
        Else
            Update()
            RadNotificationNote.Text = "CDP aggiornato correttamente!"
            RadNotificationNote.Show()


        End If
        Session.Remove("idSel")
    End Sub
 Protected Sub btnSalStampa_Click(sender As Object, e As System.EventArgs) Handles btnSalStampa.Click
        If idPagamento.Value > 0 Then
            Update(" DATA_STAMPA = " & Format(Now, "yyyyMMdd"))
            PdfPagamento(idPagamento.Value)
        Else
            RadWindowManager1.RadAlert("Impossibile effettuare la stampa! Salvare prima il CdP", 300, 150, "Attenzione", "", "null")
        End If
    End Sub
    
    Private Sub PdfPagamento(ByVal ID As String)
        Try
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim tb1 As String = "<table style='width:100%;'>"
            Dim tb2 As String = "<table style='width:100%;'>"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim lettDettagli As Oracle.DataAccess.Client.OracleDataReader
            Dim Matricola As String = ""
            Dim ibanFornitore As String = "- - -"
            par.cmd.CommandText = "select iban from siscom_mi.fornitori_iban where ID  = (select id_IBAN_FORNITORE from siscom_mi.pagamenti where id = " & ID & ")"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                ibanFornitore = par.IfNull(myReader1("iban"), "- - -")
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT FORNITORI.* FROM SISCOM_MI.FORNITORI WHERE id = " & idFornitore.Value
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$chiamante$", "")
                '*****************SCRITTURA TABELLA DETTAGLI dettagli
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "</td>"
                tb1 = tb1 & "</tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> IBAN: " & ibanFornitore & "</td></tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> cod. fiscale: " & par.IfNull(myReader1("COD_FISCALE"), "") & "</td></table>"
                '*****************FINE SCRITTURA DETTAGLI

            End If
            myReader1.Close()

            ''par.cmd.CommandText = "SELECT * FROM SISCOM_MI.cond_gestione WHERE ID =" & idPrenotazione.Value
            ''myReader1 = par.cmd.ExecuteReader
            ''If myReader1.Read Then
            'InizioES = "01/01/2010"
            'FineEs = ""

            'InizioES = par.FormattaData(myReader1("DATA_INIZIO"))
            'FineEs = par.FormattaData(myReader1("DATA_FINE"))
            ''End If
            ''myReader1.Close()
            'contenuto = Replace(contenuto, "$dettagli_chiamante$", "12000X01")
            contenuto = Replace(contenuto, "$copia$", "")
            contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(ID))
            contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$grigliaM$", "")
            par.cmd.CommandText = ""
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & ID & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','Stampa Mandato di Pagamento')"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "SELECT PAGAMENTI.*,PRENOTAZIONI.*,SISCOM_MI.GETDATA(PAGAMENTI.DATA_SCADENZA) AS D_SCAD, T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO " _
                                & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PAGAMENTI " _
                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO = " & ID & " AND PAGAMENTI.ID = PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.ID = PRENOTAZIONI.ID_VOCE_PF " _
                                & "AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            myReader1 = par.cmd.ExecuteReader
            '*****Peppe Modify 27/04/2011 secondo nuove direttive stampa modelli pagamento
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$dettagli_chiamante$", "")
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00"))


                ''*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE_BREVE"), "N.D.") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                ''*****************
                'par.cmd.CommandText = "SELECT COND_VOCI_SPESA.DESCRIZIONE, PRENOTAZIONI.* FROM SISCOM_MI.COND_VOCI_SPESA_PF,SISCOM_MI.COND_VOCI_SPESA, SISCOM_MI.PRENOTAZIONI " _
                '                    & "WHERE PRENOTAZIONI.ID_PAGAMENTO =" & ID & " AND COND_VOCI_SPESA.ID = COND_VOCI_SPESA_PF.ID_VOCE_COND " _
                '                    & "AND (PRENOTAZIONI.ID_VOCE_PF = COND_VOCI_SPESA_PF.ID_VOCE_PF OR PRENOTAZIONI.ID_VOCE_PF_IMPORTO = COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO)"
                'lettDettagli = par.cmd.ExecuteReader
                'While lettDettagli.Read
                '    tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.IfNull(lettDettagli("DESCRIZIONE"), "n.d.") & " €.</td>"
                '    tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(lettDettagli("IMPORTO_APPROVATO"), 0), "##,##0.00") & "</td>"
                '    tb2 = tb2 & "</tr>"

                '    If String.IsNullOrEmpty(idvocePf) Then
                '        idvocePf = lettDettagli("ID_VOCE_PF")
                '    Else
                '        idvocePf = idvocePf & "," & lettDettagli("ID_VOCE_PF")
                '    End If

                'End While
                'lettDettagli.Close()
                If myReader1("TIPO_PAGAMENTO") = 12 Then
                    Dim sommaImponibile As Decimal = 0
                    Dim sommaIva As Decimal = 0
                    par.cmd.CommandText = "select sum(nvl(imponibile,0)) from siscom_mi.prenotazioni where id_pagamento = " & ID
                    sommaImponibile = par.cmd.ExecuteScalar
                    par.cmd.CommandText = "select sum(nvl(iva,0)) from siscom_mi.prenotazioni where id_pagamento = " & ID
                    sommaIva = par.cmd.ExecuteScalar

                    tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IMPONIBILE €.</td>"
                    tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(sommaImponibile, "##,##0.00") & "</td>"
                    tb2 = tb2 & "</tr>"
                    tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €.</td>"
                    tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(sommaIva, "##,##0.00") & "</td>"
                    tb2 = tb2 & "</tr>"

                End If
                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>TOTALE €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr>"

                tb2 = tb2 & "</table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    contenuto = Replace(contenuto, "$imp_letterale$", "EURO -" & NumeroInLettere(Format(Math.Abs(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)), "##,##0.00")))
                Else
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")))
                End If                
                Dim modalita As String
                Dim condizione As String

                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_modalita_pag where id =" & par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), -1)
                modalita = par.IfNull(par.cmd.ExecuteScalar, "")
                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_pagamento where id =" & par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), -1)
                condizione = par.IfNull(par.cmd.ExecuteScalar, "")
                contenuto = Replace(contenuto, "$modalita$", par.IfEmpty(modalita, "n.d."))
                contenuto = Replace(contenuto, "$condizione$", par.IfEmpty(condizione, "n.d."))
                contenuto = Replace(contenuto, "$dscadenza$", par.IfNull(myReader1("D_SCAD"), "---"))

            End If
            myReader1.Close()

            tb1 = "<table style='width:100%;'>"
            tb2 = "<table style='width:100%;'>"
            Dim tb3 As String = "<table style='width:100%;'>"
            par.cmd.CommandText = "SELECT  pf_voci.codice,pf_voci.descrizione,sum(importo_prenotato) as importo_prenotato FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PRENOTAZIONI WHERE " _
                & " PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PRENOTAZIONI.ID_PAGAMENTO = " & ID & " group by pf_voci.codice,pf_voci.descrizione"
            myReader1 = par.cmd.ExecuteReader

            While myReader1.Read
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("CODICE"), "") & "</td>"
                tb1 = tb1 & "</tr>"

                tb2 = tb2 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                tb2 = tb2 & "</tr>"

                tb3 = tb3 & "<tr><td style='text-align: right; font-size:12pt;font-family :Arial ;'> €." & Format(par.IfNull(myReader1("IMPORTO_PRENOTATO"), 0), "##,##0.00") & "</td>"
                tb3 = tb3 & "</tr>"

            End While

            tb1 = tb1 & "</table>"
            tb2 = tb2 & "</table>"
            tb3 = tb3 & "</table>"

            contenuto = Replace(contenuto, "$cod_capitolo$", tb1)
            contenuto = Replace(contenuto, "$voce_pf$", tb2)
            contenuto = Replace(contenuto, "$TOTSING$", tb3)

            myReader1.Close()

            par.cmd.CommandText = "SELECT COD_ANA FROM OPERATORI WHERE ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.EVENTI_PAGAMENTI WHERE ID_PAGAMENTO = " & ID & " AND COD_EVENTO = 'F98' ) "
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then
                Matricola = par.IfNull(myReader1("COD_ANA"), "n.d.")
            End If

            myReader1.Close()

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 20
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "PagamentoUtenze" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            nomefile = par.NomeFileManut("CDP", ID) & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../FileTemp/" & nomefile & "','cdp','');", True)
        Catch ex As Exception
            Session.Remove("idSel")

            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Remove("idSel")
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " PdfPagamento - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

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
