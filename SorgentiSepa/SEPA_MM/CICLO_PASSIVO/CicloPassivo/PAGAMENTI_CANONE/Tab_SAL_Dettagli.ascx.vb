Imports System.Collections
Imports Telerik.Web.UI

Partial Class Tab_SAL_Dettagli
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim gen As Epifani.ListaGenerale

        Try

            lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))

            If Not IsPostBack Then


                For Each gen In lstListaRapporti
                    If vIdPrenotazioni <> "" Then
                        vIdPrenotazioni = vIdPrenotazioni & "," & gen.STR
                    Else
                        vIdPrenotazioni = gen.STR
                    End If
                Next


                vIdAppalto = CType(Me.Page.FindControl("txtID_APPALTO"), HiddenField).Value
                'vIdFornitore = CType(Me.Page.FindControl("txtID_FORNITORE"), HiddenField).Value
                'Me.txtData.Value = CType(Me.Page.FindControl("txtDataScadenza"), TextBox).Text


                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text

                If IdConnessione = "-1" Then

                    ' CONNESSIONE DB
                    IdConnessione = Format(Now, "yyyyMMddHHmmss")
                    Me.txtIdConnessione.Text = CStr(IdConnessione)

                    If PAR.OracleConn.State = Data.ConnectionState.Open Then
                        CType(Me.Page.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Impossibile visualizzare!", 300, 150, "Attenzione", "", "null")

                        Exit Sub
                    Else
                        PAR.OracleConn.Open()
                        PAR.SettaCommand(PAR)
                        HttpContext.Current.Session.Add("CONNESSIONE" & IdConnessione, PAR.OracleConn)
                        ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                    End If

                    BindGrid_Prenotazioni()

                    'CHIUSURA DB
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)

                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                    End If

                    PAR.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)

                    Session.Item("LAVORAZIONE") = "0"


                Else

                    Me.txtIdConnessione.Text = IdConnessione

                    If PAR.OracleConn.State = Data.ConnectionState.Open Then

                        CType(Me.Page.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Impossibile visualizzare!", 300, 150, "Attenzione", "", "null")
                        Exit Sub
                    Else
                        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        PAR.SettaCommand(PAR)
                        'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '''par.cmd.Transaction = par.myTrans
                    End If
                    ''''''''''''''''''''''''''

                    BindGrid_Prenotazioni()

                End If

            End If
            'If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Or CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value > 0 Then
            '    FrmSolaLettura()
            'End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Public Property vIdPrenotazioni() As String
        Get
            If Not (ViewState("par_idPrenotazioni") Is Nothing) Then
                Return CStr(ViewState("par_idPrenotazioni"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idPrenotazioni") = value
        End Set

    End Property

    'Private Property vIdFornitore() As Long
    '    Get
    '        If Not (ViewState("par_idFornitore") Is Nothing) Then
    '            Return CLng(ViewState("par_idFornitore"))
    '        Else
    '            Return 0
    '        End If
    '    End Get

    '    Set(ByVal value As Long)
    '        ViewState("par_idFornitore") = value
    '    End Set

    'End Property

    Private Property vIdAppalto() As Long
        Get
            If Not (ViewState("par_idAppalto") Is Nothing) Then
                Return CLng(ViewState("par_idAppalto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idAppalto") = value
        End Set

    End Property



    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property


    'PRENOTAZIONI GRID1
    Private Sub BindGrid_Prenotazioni()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        PAR.SettaCommand(PAR)

        If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
        End If

        '& "  and  SISCOM_MI.PRENOTAZIONI.ID_APPALTO=" & vIdAppalto _
        '& "  and  SISCOM_MI.PRENOTAZIONI.ID_FORNITORE=" & vIdFornitore _
        '& "  and  SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA=" & PAR.AggiustaData(Me.txtData.Value) _
        '& "  and  SISCOM_MI.PRENOTAZIONI.TIPO_PAGAMENTO=6 " 

        Dim stato As String = CType(Page.FindControl("txtStatoPagamento"), HiddenField).Value

        If stato = "0" Then
            StringaSql = " select SISCOM_MI.PRENOTAZIONI.ID,(SISCOM_MI.PRENOTAZIONI.PROGR_FORNITORE||'/'||SISCOM_MI.PRENOTAZIONI.ANNO) as ""PROG_ANNO""," _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE""," _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_SCADENZA""," _
                         & " (SISCOM_MI.PF_VOCI.CODICE|| ' - ' ||SISCOM_MI.PF_VOCI.DESCRIZIONE) AS ""VOCE_SERVIZIO""," _
                         & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO,'9G999G999G999G999G990D99')) AS ""PREN_LORDO"", " _
                         & " TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN PRENOTAZIONI.IMPORTO_PRENOTATO ELSE PRENOTAZIONI.IMPORTO_APPROVATO END),'9G999G999G999G999G990D99')) AS ""CONS_LORDO""," _
                         & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_PENALI.IMPORTO,'9G999G999G999G999G990D99')) AS ""PENALE""," _
                         & " SISCOM_MI.PRENOTAZIONI.DESCRIZIONE," _
                         & " SISCOM_MI.APPALTI_PENALI.ID as ""ID_PENALE""" _
                 & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI_PENALI " _
                 & " where SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF=SISCOM_MI.PF_VOCI.ID (+) " _
                 & "  and  SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) " _
                 & "  and  SISCOM_MI.PRENOTAZIONI.ID in (" & vIdPrenotazioni & ")" _
                 & " order by  PROG_ANNO"
        Else
            StringaSql = " select SISCOM_MI.PRENOTAZIONI.ID,(SISCOM_MI.PRENOTAZIONI.PROGR_FORNITORE||'/'||SISCOM_MI.PRENOTAZIONI.ANNO) as ""PROG_ANNO""," _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE""," _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_SCADENZA""," _
                         & " (SISCOM_MI.PF_VOCI.CODICE|| ' - ' ||SISCOM_MI.PF_VOCI.DESCRIZIONE) AS ""VOCE_SERVIZIO""," _
                         & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO,'9G999G999G999G999G990D99')) AS ""PREN_LORDO"", " _
                         & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_APPROVATO,'9G999G999G999G999G990D99')) AS ""CONS_LORDO""," _
                         & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_PENALI.IMPORTO,'9G999G999G999G999G990D99')) AS ""PENALE""," _
                         & " SISCOM_MI.PRENOTAZIONI.DESCRIZIONE," _
                         & " SISCOM_MI.APPALTI_PENALI.ID as ""ID_PENALE""" _
                 & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI_PENALI " _
                 & " where SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF=SISCOM_MI.PF_VOCI.ID (+) " _
                 & "  and  SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) " _
                 & "  and  SISCOM_MI.PRENOTAZIONI.ID in (" & vIdPrenotazioni & ")" _
                 & " order by  PROG_ANNO"
        End If


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataTable()

        da.Fill(ds) ', "PRENOTAZIONI")


        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        da.Dispose()
        ds.Dispose()


    End Sub

    'Protected Sub DataGrid1_ItemDataBound1(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_SAL_Dettagli_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('Tab_SAL_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_SAL_Dettagli_txtIdPenale').value='" & e.Item.Cells(1).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_SAL_Dettagli_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('Tab_SAL_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_SAL_Dettagli_txtIdPenale').value='" & e.Item.Cells(1).Text & "'")

    '    End If
    'End Sub

    Protected Sub DataGrid13_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('Tab_SAL_Dettagli_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('Tab_SAL_Dettagli_txtIdComponente').value='" & dataItem("ID").Text & "';document.getElementById('Tab_SAL_Dettagli_txtIdPenale').value='" & dataItem("ID_PENALE").Text & "'")
        End If
    End Sub

    Function ControlloCampiInterventi() As Boolean
        Dim Somma1 As Decimal = 0

        ControlloCampiInterventi = True


        Somma1 = Decimal.Parse(PAR.IfEmpty(Me.txtResiduoControllo.Value.Replace(".", ""), "0"))

        If Decimal.Parse(PAR.IfEmpty(Me.txtNettoOneriIVA2.Text.Replace(".", ""), "0")) > Somma1 Then
            'Response.Write("<script>alert('Attenzione: l\'importo da approvare è superiore all\'importo residuo disponibile!');</script>")
            CType(Me.Page.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Attenzione: l\'importo da approvare è superiore all\'importo residuo disponibile!", 300, 150, "Attenzione", "", "null")

            ControlloCampiInterventi = False
            Exit Function
        End If


        'If PAR.IfEmpty(Me.cmbTipologia.Text, "Null") = "Null" Or Me.cmbTipologia.Text = "-1" Then
        '    Response.Write("<script>alert('Inserire la tipologia oggetto!');</script>")
        '    ControlloCampiInterventi = False
        '    cmbTipologia.Focus()
        '    Exit Function
        'End If


        'If PAR.IfEmpty(Me.cmbDettaglio.Text, "Null") = "Null" Or Me.cmbDettaglio.Text = "-1" Then
        '    Response.Write("<script>alert('Inserire il dettaglio tipologia oggetto!');</script>")
        '    ControlloCampiInterventi = False
        '    cmbDettaglio.Focus()
        '    Exit Function
        'End If

        'If PAR.IfEmpty(Me.txtImporto.Text, "Null") = "Null" Then
        '    Response.Write("<script>alert('Inserire l\'importo presunto!');</script>")
        '    ControlloCampiInterventi = False
        '    txtImporto.Focus()
        '    Exit Function
        'End If


    End Function

    Protected Sub btn_Inserisci1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Inserisci1.Click

        Dim Somma1 As Decimal = 0

        Somma1 = Decimal.Parse(PAR.IfEmpty(Me.txtResiduoControllo.Value.Replace(".", ""), "0"))

        If CDec(PAR.PuntiInVirgole(Me.txtNettoOneriIVA2.Text)) > Somma1 Then
            CType(Me.Page.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Attenzione: l\'importo da approvare è superiore all\'importo residuo disponibile!", 300, 150, "Attenzione", "", "null")

            txtAppare1.Text = "1"
            Exit Sub
        Else
            Me.UpdatePrenotazioni()
            CType(Me.Page, Object).SettaValoriInApprovazione()
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtSel1.Text = ""
            txtIdComponente.Text = ""
        End If



    End Sub

    Protected Sub btn_Chiudi1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Chiudi1.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        txtSel1.Text = ""
        txtIdComponente.Text = ""
    End Sub



    Private Sub UpdatePrenotazioni()
        Dim fl_ritenuta As Integer = 0
        Dim perc_scontoas As Decimal = 0
        Dim perc_iva As Decimal = 0
        Dim importo, ritenuta, ritenuta_IVATA, risultato3 As Decimal

        Try


            ' RIPRENDO LA CONNESSIONE
            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            PAR.SettaCommand(PAR)


            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            End If


            PAR.cmd.Parameters.Clear()

            '****************************************
            If PAR.IfEmpty(Me.txtNettoOneriIVA2.Text, 0) >= 0 Then

                'CALCOLARE RIT_LEGGE_IVATA: (Modifica del 15/07/2011) se veniva cambiato l'importo approvato, no ricalcolavo la ritenuta di legge

                '***** RICAVO IVA CANONE da APPALTI_LOTTI_SERVIZI
                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "select APPALTI_LOTTI_SERVIZI.*,SISCOM_MI.PRENOTAZIONI.PERC_IVA " _
                                   & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI, SISCOM_MI.PRENOTAZIONI " _
                                   & " where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO = PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                                   & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=PRENOTAZIONI.ID_APPALTO " _
                                   & "   and PRENOTAZIONI.ID=" & Me.txtIdComponente.Text

                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReaderT.Read Then
                    If PAR.IfNull(myReaderT("PERC_IVA"), -1) = -1 Then
                        perc_iva = PAR.IfNull(myReaderT("IVA_CANONE"), 0)
                    Else
                        perc_iva = PAR.IfNull(myReaderT("PERC_IVA"), 0)
                    End If
                End If
                myReaderT.Close()
                '*************************


                fl_ritenuta = PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0)
                importo = strToNumber(Me.txtNettoOneriIVA2.Text)

                risultato3 = ((importo * 100) / (100 + perc_iva))

                'ALIQUOTA 0,5% 
                If PAR.IfNull(fl_ritenuta, 0) = 1 Then

                    ritenuta = (risultato3 * 0.5) / 100
                    ritenuta_IVATA = Math.Round(CDec(ritenuta + ((ritenuta * perc_iva) / 100)), 2)

                    'AGGIORNO SOLO IMPORTO_APPROVATO,RIT_LEGGE_IVATA ,PERC_IVA
                    PAR.cmd.Parameters.Clear()
                    PAR.cmd.CommandText = "update  SISCOM_MI.PRENOTAZIONI  " _
                                       & " set IMPORTO_APPROVATO=:importo" _
                                       & ", RIT_LEGGE_IVATA=:rit_legge_ivata,PERC_IVA=:iva " _
                                       & " where ID=" & Me.txtIdComponente.Text

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", Math.Round(CDec(strToNumber(Me.txtNettoOneriIVA2.Text)), 2)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge_ivata", Math.Round(CDec(strToNumber(ritenuta_IVATA)), 2)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva", Math.Round(CDec(strToNumber(perc_iva)), 2)))

                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = ""

                Else
                    ritenuta = 0
                    ritenuta_IVATA = 0

                    'AGGIORNO SOLO IMPORTO_APPROVATO
                    PAR.cmd.Parameters.Clear()
                    PAR.cmd.CommandText = "update  SISCOM_MI.PRENOTAZIONI  set " _
                            & "IMPORTO_APPROVATO=:importo" _
                    & " where ID=" & Me.txtIdComponente.Text


                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtNettoOneriIVA2.Text)))
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = ""

                End If
                '*************************************************************************


                ''****Scrittura evento PRENOTAZIONE
                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PRENOTAZIONI (ID_PRENOTAZIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                   & " values ( " & Me.txtIdComponente.Text & "," _
                                                  & Session.Item("ID_OPERATORE") & ",'" _
                                                  & Format(Now, "yyyyMMddHHmmss") & "','F02'," _
                                                  & "'Modificato IMPORTO PRENOTATO: " & strToNumber(Me.txtNettoOneriIVA2.Text) & "')"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""
                PAR.cmd.Parameters.Clear()
                '****************************************************

            End If

            '*****************************************
            If Me.txtIdPenale.Value = "&nbsp;" Then Me.txtIdPenale.Value = -1
            If Me.txtIdPenale.Value = "" Then Me.txtIdPenale.Value = -1
            If Me.txtIdPenale.Value < 0 And PAR.IfEmpty(Me.txtPenale.Text, 0) > 0 Then

                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "select SISCOM_MI.SEQ_APPALTI_PENALI.NEXTVAL FROM DUAL"
                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReaderT.Read Then
                    Me.txtIdPenale.Value = myReaderT(0)
                End If
                myReaderT.Close()


                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "insert into SISCOM_MI.APPALTI_PENALI " _
                                            & " (ID,ID_APPALTO,ID_PRENOTAZIONE,IMPORTO) " _
                                    & " values (:id,:id_appalto,:id_prenotazione,:importo)"

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", Me.txtIdPenale.Value))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", vIdAppalto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione", Me.txtIdComponente.Text))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtPenale.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""
                '****************************************************


                ''****Scrittura evento PRENOTAZIONE
                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PRENOTAZIONI (ID_PRENOTAZIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                   & " values ( " & Me.txtIdComponente.Text & "," _
                                                  & Session.Item("ID_OPERATORE") & ",'" _
                                                  & Format(Now, "yyyyMMddHHmmss") & "','F02'," _
                                                  & "'Modificato IMPORTO PENALE: " & strToNumber(Me.txtPenale.Text) & "')"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""
                PAR.cmd.Parameters.Clear()
                '****************************************************

            Else

                If Me.txtPenale.Text = "&nbsp;" Then Me.txtPenale.Text = 0
                If Me.txtPenale.Text = "" Then Me.txtPenale.Text = 0

                If PAR.IfEmpty(Me.txtPenale.Text, 0) = 0 Then
                    PAR.cmd.Parameters.Clear()
                    PAR.cmd.CommandText = "delete from SISCOM_MI.APPALTI_PENALI where ID=" & Me.txtIdPenale.Value
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = ""
                    Me.txtIdPenale.Value = -1

                    ''****Scrittura evento PRENOTAZIONE
                    PAR.cmd.Parameters.Clear()
                    PAR.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PRENOTAZIONI (ID_PRENOTAZIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.txtIdComponente.Text & "," _
                                                      & Session.Item("ID_OPERATORE") & ",'" _
                                                      & Format(Now, "yyyyMMddHHmmss") & "','F02'," _
                                                      & "'Modificato IMPORTO PENALE a 0')"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = ""
                    PAR.cmd.Parameters.Clear()
                    '****************************************************

                Else

                    PAR.cmd.Parameters.Clear()
                    PAR.cmd.CommandText = " update SISCOM_MI.APPALTI_PENALI " _
                                        & " set IMPORTO=:importo " _
                                        & " where ID=:id"

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtPenale.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", Me.txtIdPenale.Value))

                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = ""

                    ''****Scrittura evento PRENOTAZIONE
                    PAR.cmd.Parameters.Clear()
                    PAR.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PRENOTAZIONI (ID_PRENOTAZIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.txtIdComponente.Text & "," _
                                                      & Session.Item("ID_OPERATORE") & ",'" _
                                                      & Format(Now, "yyyyMMddHHmmss") & "','F02'," _
                                                      & "'Modificato IMPORTO PENALE: " & strToNumber(Me.txtPenale.Text) & "')"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = ""
                    PAR.cmd.Parameters.Clear()
                    '****************************************************

                End If
            End If

            'LETTURA PENALE
            Dim penale As Decimal = 0
            PAR.cmd.CommandText = " select * from SISCOM_MI.APPALTI_PENALI " _
                            & " where ID_PRENOTAZIONE in (" & vIdPrenotazioni & ")"
            'select ID from SISCOM_MI.PRENOTAZIONI " _
            '& " where TIPO_PAGAMENTO=6 " _
            ' & "  and ID_APPALTO=" & vIdAppalto _
            ' & "  and ID_FORNITORE=" & vIdFornitore _
            ' & "  and DATA_SCADENZA=" & PAR.AggiustaData(Me.txtData.Value) & ")"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            While myReader2.Read
                penale = penale + PAR.IfNull(myReader2("IMPORTO"), 0)
            End While
            myReader2.Close()

            CType(Me.Page.FindControl("Tab_SAL_Riepilogo").FindControl("txtPenale"), TextBox).Text = IsNumFormat(PAR.IfNull(penale, 0), "", "##,##0.00")
            '****************************


            BindGrid_Prenotazioni()

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdPrenotazioni <> "" Then
                PAR.myTrans.Rollback()

                PAR.myTrans = PAR.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnApri1_Click(sender As Object, e As System.EventArgs) Handles btnApri1.Click

        Try

            If txtIdComponente.Text = "" Then
                CType(Me.Page.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                Exit Sub
            Else

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    CType(Me.Page.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Impossibile visualizzare!", 300, 150, "Attenzione", "", "null")

                    Exit Sub
                Else
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    PAR.SettaCommand(PAR)
                    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
                End If


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                Dim Somma1 As Decimal = 0
                Dim sRisultato As String = ""

                Dim stato As String = CType(Page.FindControl("txtSTATO"), HiddenField).Value

                If stato = "0" Then
                    PAR.cmd.CommandText = " select to_char(PRENOTAZIONI.IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO," _
                                           & " to_char((case when prenotazioni.importo_approvato is null then prenotazioni.importo_prenotato else prenotazioni.importo_approvato end)) as IMPORTO_APPROVATO," _
                                        & " (PF_VOCI.CODICE|| ' - ' ||PF_VOCI.DESCRIZIONE) as ""VOCE_SERVIZIO"",PRENOTAZIONI.ID_VOCE_PF," _
                                        & " PF_VOCI_IMPORTO.DESCRIZIONE as ""DETTAGLIO_VOCE""," _
                                        & " to_char(APPALTI_PENALI.IMPORTO) as ""PENALE"",SISCOM_MI.PRENOTAZIONI.ID_STRUTTURA" _
                                    & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI_PENALI " _
                                    & " where PRENOTAZIONI.ID=" & Me.txtIdComponente.Text _
                                    & "   and PRENOTAZIONI.ID_VOCE_PF=SISCOM_MI.PF_VOCI.ID (+) " _
                                    & "   and PRENOTAZIONI.ID_VOCE_PF_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                                    & "   and PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) "

                Else
                    PAR.cmd.CommandText = " select to_char(PRENOTAZIONI.IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO," _
                                           & " to_char(PRENOTAZIONI.IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                        & " (PF_VOCI.CODICE|| ' - ' ||PF_VOCI.DESCRIZIONE) as ""VOCE_SERVIZIO"",PRENOTAZIONI.ID_VOCE_PF," _
                                        & " PF_VOCI_IMPORTO.DESCRIZIONE as ""DETTAGLIO_VOCE""," _
                                        & " to_char(APPALTI_PENALI.IMPORTO) as ""PENALE"",SISCOM_MI.PRENOTAZIONI.ID_STRUTTURA" _
                                    & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI_PENALI " _
                                    & " where PRENOTAZIONI.ID=" & Me.txtIdComponente.Text _
                                    & "   and PRENOTAZIONI.ID_VOCE_PF=SISCOM_MI.PF_VOCI.ID (+) " _
                                    & "   and PRENOTAZIONI.ID_VOCE_PF_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                                    & "   and PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) "

                End If


                myReader1 = PAR.cmd.ExecuteReader

                If myReader1.Read Then


                    SettaggioCampi(PAR.IfNull(myReader1("ID_VOCE_PF"), -1), PAR.IfNull(myReader1("ID_STRUTTURA"), -1))


                    Me.txtVoce.Text = PAR.IfNull(myReader1("VOCE_SERVIZIO"), "")
                    Me.txtVoceDettaglio.Text = PAR.IfNull(myReader1("DETTAGLIO_VOCE"), "")

                    sRisultato = PAR.IfNull(myReader1("IMPORTO_PRENOTATO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)
                    Me.txtNettoOneriIVA.Text = IsNumFormat(Somma1, "", "##,##0.00")


                    sRisultato = CDec(PAR.IfNull(myReader1("IMPORTO_APPROVATO"), 0))
                    Somma1 = Decimal.Parse(sRisultato)

                    Me.txtNettoOneriIVA2.Text = sRisultato.Replace(",", ".")


                    sRisultato = PAR.IfNull(myReader1("PENALE"), "0")
                    Somma1 = Decimal.Parse(sRisultato)
                    Me.txtPenale.Text = sRisultato.Replace(",", ".")

                End If
                myReader1.Close()
                Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                'If Me.txt_FL_BLOCCATO.Value = 1 Then
                '    Response.Write("<SCRIPT>alert('Attenzione...Non è possibile modificare la voce perchè proveniente da un ordine emesso integrativo!');</SCRIPT>")
                '    Me.btn_Inserisci1.Visible = False
                'Else
                '    Me.btn_Inserisci1.Visible = True
                'End If


            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdPrenotazioni <> "" Then
                PAR.myTrans.Rollback()

                PAR.myTrans = PAR.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub


    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


    Private Sub FrmSolaLettura()
        Try

            'Me.btn_Inserisci1.Visible = False

            'Dim CTRL As Control = Nothing
            'For Each CTRL In Me.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).ReadOnly = False
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Enabled = False
            '    End If
            'Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub



    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function




    Private Sub AbilitaDisabilita()


        Dim CTRL As Control = Nothing
        For Each CTRL In Me.Page.FindControl("form1").Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = True
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is RadioButtonList Then
                DirectCast(CTRL, RadioButtonList).Enabled = False
            End If
        Next


    End Sub


    Private Sub SettaggioCampi(ByVal ID_VOCE_PF As Integer, ByVal ID_STRUTTURA As Integer)
        'CARICO COMBO TAB SECODNDARI
        Dim FlagConnessione As Boolean

        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0

        Dim SommaValoreLordo As Decimal = 0
        Dim SommaValoreAssestatoLordo As Decimal = 0

        Dim SommaResiduo As Decimal = 0
        Dim SommaAssesato As Decimal = 0

        Dim SommaPrenotatoControllo As Decimal = 0
        Dim SommaConsuntivatoControllo As Decimal = 0
        Dim SommaLiquidatoControllo As Decimal = 0

        Dim SommaValoreLordoControllo As Decimal = 0
        Dim SommaValoreAssestatoLordoControllo As Decimal = 0
        Dim SommaValoreVariazioni As Decimal = 0

        Dim SommaResiduoControllo As Decimal = 0
        Dim SommaAssesatoControllo As Decimal = 0

        Dim sRisultato As String = ""
        Dim Flag As Boolean = False
        Dim sSelect1 As String = ""

        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)

                FlagConnessione = True
            End If


            Dim sFiliale As String = ""
            'If Session.Item("ID_STRUTTURA") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            '    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            'End If

            If ID_STRUTTURA <> "-1" Then
                sFiliale = " and ID_STRUTTURA=" & ID_STRUTTURA
            End If

            'VOCE
            PAR.cmd.CommandText = " select  PF_VOCI.* " _
                                & " from    SISCOM_MI.PF_VOCI " _
                                & " where   PF_VOCI.ID=" & ID_VOCE_PF

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            myReader1 = PAR.cmd.ExecuteReader()

            If myReader1.Read Then

                If Strings.Left(PAR.IfNull(myReader1("CODICE"), ""), 2) = "1." Or Strings.Left(PAR.IfNull(myReader1("CODICE"), ""), 2) = "01" Then
                    Flag = True

                    sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & ID_VOCE_PF & "))"


                    'SOMMA_LORDO
                    PAR.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                    myReader2 = PAR.cmd.ExecuteReader()

                    If myReader2.Read Then
                        sRisultato = PAR.IfNull(myReader2(0), "0")
                        SommaValoreLordoControllo = Decimal.Parse(sRisultato)
                    End If
                    myReader2.Close()


                    'SOMMA_LORDO_ASSESSTATO
                    PAR.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                    myReader2 = PAR.cmd.ExecuteReader()

                    If myReader2.Read Then
                        sRisultato = PAR.IfNull(myReader2(0), "0")
                        SommaValoreAssestatoLordoControllo = Decimal.Parse(sRisultato)
                    End If
                    myReader2.Close()

                    'VARIAZIONI
                    PAR.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                    myReader2 = PAR.cmd.ExecuteReader()

                    If myReader2.Read Then
                        sRisultato = PAR.IfNull(myReader2(0), "0")
                        SommaValoreVariazioni = Decimal.Parse(sRisultato)
                    End If
                    myReader2.Close()

                    SommaAssesatoControllo = SommaValoreLordoControllo + SommaValoreAssestatoLordoControllo + SommaValoreVariazioni

                End If

                sSelect1 = "=" & ID_VOCE_PF

                'SOMMA_LORDO
                PAR.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                myReader2 = PAR.cmd.ExecuteReader()

                If myReader2.Read Then
                    sRisultato = PAR.IfNull(myReader2(0), "0")
                    SommaValoreLordo = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()


                'SOMMA_LORDO_ASSESSTATO
                PAR.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                myReader2 = PAR.cmd.ExecuteReader()

                If myReader2.Read Then
                    sRisultato = PAR.IfNull(myReader2(0), "0")
                    SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()

                'VARIAZIONI
                PAR.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                myReader2 = PAR.cmd.ExecuteReader()

                If myReader2.Read Then
                    sRisultato = PAR.IfNull(myReader2(0), "0")
                    SommaValoreVariazioni = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()

                SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni
                '*******************************

                'SommaResiduo = par.IfNull(myReader1("VALORE_LORDO"), 0)
                'SommaAssesato = SommaResiduo + par.IfNull(myReader1("ASSESTAMENTO_VALORE_LORDO"), 0)
                'Me.txtIVA.Text = IsNumFormat(par.IfNull(myReader1("IVA"), 0), "", "##,##0.00")
            End If
            myReader1.Close()



            sSelect1 = "=" & ID_VOCE_PF & " and ID not in (" & PAR.IfEmpty(Me.txtIdComponente.Text, 0) & ") "

            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
            PAR.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO)) " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=0 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReader1 = PAR.cmd.ExecuteReader()

            If myReader1.Read Then
                sRisultato = PAR.IfNull(myReader1(0), "0")
                SommaPrenotato = Decimal.Parse(sRisultato)
            End If
            myReader1.Close()
            '******************************************


            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            PAR.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO)) " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=1 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReader1 = PAR.cmd.ExecuteReader()

            If myReader1.Read Then
                sRisultato = PAR.IfNull(myReader1(0), "0")
                SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
            End If
            myReader1.Close()
            '********************************************


            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            PAR.cmd.CommandText = "select to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                & " where  ID_VOCE_PF" & sSelect1 _
                                & "   and  ID_STATO>1 " & sFiliale

            myReader1 = PAR.cmd.ExecuteReader
            If myReader1.Read Then
                sRisultato = PAR.IfNull(myReader1(0), "0")
                SommaConsuntivato = Decimal.Parse(sRisultato)
            End If
            myReader1.Close()


            'IMPORTO CONSUNTIVATO e LIQUIDATO
            PAR.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
                               & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                               & "  where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                        & " from   SISCOM_MI.PRENOTAZIONI " _
                                                        & " where  ID_VOCE_PF" & sSelect1 _
                                                        & "   and  ID_STATO>1 " & sFiliale & ")"

            myReader1 = PAR.cmd.ExecuteReader
            If myReader1.Read Then
                sRisultato = PAR.IfNull(myReader1(0), "0")
                SommaLiquidato = Decimal.Parse(sRisultato)
            End If
            myReader1.Close()

            SommaConsuntivato = SommaConsuntivato - SommaLiquidato

            SommaResiduo = Fix(SommaAssesato * 100) / 100.0 - (Fix(SommaPrenotato * 100) / 100.0 + Fix(SommaConsuntivato * 100) / 100.0 + Fix(SommaLiquidato * 100) / 100.0)

            Me.txtImporto.Text = IsNumFormat(Fix(SommaValoreLordo * 100) / 100.0, "", "##,##0.00")     'Budget o consistenza inizale
            Me.txtImporto1.Text = IsNumFormat(Fix(SommaAssesato * 100) / 100.0, "", "##,##0.00")       'Budget assestato o consistenza assestante

            Me.txtImporto2.Text = IsNumFormat(Fix(SommaPrenotato * 100) / 100.0, "", "##,##0.00")
            Me.txtImporto3.Text = IsNumFormat(Fix(SommaConsuntivato * 100) / 100.0, "", "##,##0.00")
            Me.txtImporto4.Text = IsNumFormat(Fix(SommaLiquidato * 100) / 100.0, "", "##,##0.00")

            Me.txtImporto5.Text = IsNumFormat(SommaResiduo, "", "##,##0.00")        'Disponibilità residua


            If Flag = True Then

                sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & ID_VOCE_PF & ")) " & " and ID not in (" & PAR.IfEmpty(Me.txtIdComponente.Text, 0) & ") "

                'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                PAR.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                    & " from    SISCOM_MI.PRENOTAZIONI " _
                                    & " where   ID_STATO=0 " _
                                    & "   and   ID_PAGAMENTO is null " _
                                    & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

                myReader1 = PAR.cmd.ExecuteReader()

                If myReader1.Read Then
                    sRisultato = PAR.IfNull(myReader1(0), "0")
                    SommaPrenotatoControllo = Decimal.Parse(sRisultato)
                End If
                myReader1.Close()
                '******************************************


                'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                PAR.cmd.CommandText = " select   to_char(SUM(IMPORTO_APPROVATO) )" _
                                    & " from    SISCOM_MI.PRENOTAZIONI " _
                                    & " where   ID_STATO=1 " _
                                    & "   and   ID_PAGAMENTO is null " _
                                    & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

                myReader1 = PAR.cmd.ExecuteReader()

                If myReader1.Read Then
                    sRisultato = PAR.IfNull(myReader1(0), "0")
                    SommaPrenotatoControllo = SommaPrenotatoControllo + Decimal.Parse(sRisultato)
                End If
                myReader1.Close()
                '********************************************


                'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si può prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                PAR.cmd.CommandText = "select to_char(SUM(IMPORTO_APPROVATO) )" _
                                    & " from   SISCOM_MI.PRENOTAZIONI " _
                                    & " where  ID_VOCE_PF" & sSelect1 _
                                    & "   and  ID_STATO>1 " & sFiliale


                myReader1 = PAR.cmd.ExecuteReader
                If myReader1.Read Then
                    sRisultato = PAR.IfNull(myReader1(0), "0")
                    SommaConsuntivatoControllo = Decimal.Parse(sRisultato)
                End If
                myReader1.Close()

                PAR.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
                                   & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                   & "  where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                                                            & " where  ID_VOCE_PF" & sSelect1 _
                                                            & "   and  ID_STATO>1 " & sFiliale & ")"

                myReader1 = PAR.cmd.ExecuteReader
                If myReader1.Read Then
                    sRisultato = PAR.IfNull(myReader1(0), "0")
                    SommaLiquidatoControllo = Decimal.Parse(sRisultato)
                End If
                myReader1.Close()

                SommaConsuntivatoControllo = SommaConsuntivatoControllo - SommaLiquidatoControllo

                SommaResiduoControllo = SommaAssesatoControllo - (SommaPrenotatoControllo + SommaConsuntivatoControllo + SommaLiquidatoControllo)

                Me.txtResiduoControllo.Value = IsNumFormat(SommaResiduoControllo, "", "##,##0.00")
            Else
                Me.txtResiduoControllo.Value = Me.txtImporto5.Text
            End If



            If FlagConnessione = True Then
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            If vIdPrenotazioni <> "" Then
                PAR.myTrans.Rollback()

                PAR.myTrans = PAR.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)
            End If

            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub
End Class
