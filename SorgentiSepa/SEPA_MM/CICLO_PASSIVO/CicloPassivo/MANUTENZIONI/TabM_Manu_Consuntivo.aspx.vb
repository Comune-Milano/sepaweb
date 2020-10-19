Imports System.Collections
Imports System.Math
Partial Class TabM_Manu_Consuntivo
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
        Try
            Response.Expires = 0

            If Not IsPostBack Then


                If Request.QueryString("IDCON") <> "" Then
                    IdConnessione = Request.QueryString("IDCON")
                End If

                If Request.QueryString("IDPADRE") <> "" Then
                    vIdManutenzionePadre = Request.QueryString("IDPADRE")
                End If

                txtResiduoConsumo.Value = 0
                If Request.QueryString("RESIDUO") <> "" Then
                    txtResiduoConsumo.Value = Request.QueryString("RESIDUO")
                End If


                'Me.Session.Add("MODIFYMODAL", 0)


                vIdManutenzione = 0
                If Request.QueryString("ID_MANUTENZIONE") <> "" Then
                    vIdManutenzione = Request.QueryString("ID_MANUTENZIONE")
                End If


                ' CONNESSIONE DB
                'IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text
                'Me.txtIdConnessione.Text = IdConnessione

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘‘par.cmd.Transaction = par.myTrans
                End If

                'UNITA DI MISURA
                SettaMisure()

                BindGrid_Consuntivo()

            End If


            Dim CTRL As Control

            '*** FORM PRINCIPALE
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            Me.txtPrezzo.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtPrezzo.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")
            Me.txtPrezzo.Attributes.Add("onChange", "javascript:CalcolaPrezzoTotale(this,document.getElementById('txtQuantita').value,document.getElementById('txtPrezzo').value);")

            Me.txtTotale.Attributes.Add("onChange", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

            Me.txtQuantita.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtQuantita.Attributes.Add("onChange", "javascript:CalcolaPrezzoTotale(this,document.getElementById('txtQuantita').value,document.getElementById('txtPrezzo').value);")


            If Request.QueryString("IDVISUAL") = "1" Then
                FrmSolaLettura()
            End If


            'CType(Me.Page, Object).ChiudiTutto()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Public Property vIdManutenzione() As Long
        Get
            If Not (ViewState("par_idManutenzione") Is Nothing) Then
                Return CLng(ViewState("par_idManutenzione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idManutenzione") = value
        End Set

    End Property

    Public Property vIdManutenzionePadre() As Long
        Get
            If Not (ViewState("par_idManutenzionePadre") Is Nothing) Then
                Return CLng(ViewState("par_idManutenzionePadre"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idManutenzionePadre") = value
        End Set

    End Property


    Public Property vIdAppalto() As Long
        Get
            If Not (ViewState("par_IdAppalto") Is Nothing) Then
                Return CLng(ViewState("par_IdAppalto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdAppalto") = value
        End Set

    End Property


    'INTERVENTI GRID1
    Private Sub BindGrid_Consuntivo()
        Dim StringaSql As String



        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = " select SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID,SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO," _
                          & " SISCOM_MI.MANUTENZIONI_CONSUNTIVI.DESCRIZIONE, SISCOM_MI.TIPOLOGIA_UNITA_MISURA.DESCRIZIONE as ""UM"", " _
                          & " TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.QUANTITA,'9G999G990D99')) as ""QUANTITA""," _
                          & " TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_UNITARIO,'9G999G999G999G999G990D99')) as ""PREZZO_UNITARIO""," _
                          & " TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE,'9G999G999G999G999G990D99')) as ""PREZZO_TOTALE"",SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_UM" _
                   & " from  SISCOM_MI.MANUTENZIONI_CONSUNTIVI, SISCOM_MI.TIPOLOGIA_UNITA_MISURA" _
                   & " where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI =" & vIdManutenzione _
                   & " and  SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_UM=SISCOM_MI.TIPOLOGIA_UNITA_MISURA.ID (+) " _
                   & " order by SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO"

        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataTable()

        da.Fill(ds) ', "MANUTENZIONI_CONSUNTIVI")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        da.Dispose()
        ds.Dispose()

        par.cmd.CommandText = "select SUM(PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI  where ID_MANUTENZIONI_INTERVENTI =" & vIdManutenzione
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        myReader1 = par.cmd.ExecuteReader

        If myReader1.Read Then
            lbl_Tot_Cons.Text = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
        End If
        myReader1.Close()

        par.cmd.CommandText = "select SUM(PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI  where COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE' and ID_MANUTENZIONI_INTERVENTI =" & vIdManutenzione
        myReader1 = par.cmd.ExecuteReader

        If myReader1.Read Then
            lbl_Tot_Rimborsi.Text = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
        End If
        myReader1.Close()

    End Sub


    Private Sub BindGrid2_Consuntivo(ByVal Rimborsi As Boolean)
        Dim StringaSql As String



        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = " select SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID,SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO," _
                          & " SISCOM_MI.MANUTENZIONI_CONSUNTIVI.DESCRIZIONE, SISCOM_MI.TIPOLOGIA_UNITA_MISURA.DESCRIZIONE as ""UM"", " _
                          & " TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.QUANTITA,'9G999G990D99')) as ""QUANTITA""," _
                          & " TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_UNITARIO,'9G999G999G999G999G990D99')) as ""PREZZO_UNITARIO""," _
                          & " TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE,'9G999G999G999G999G990D99')) as ""PREZZO_TOTALE"",SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_UM" _
                   & " from  SISCOM_MI.MANUTENZIONI_CONSUNTIVI, SISCOM_MI.TIPOLOGIA_UNITA_MISURA" _
                   & " where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI =" & vIdManutenzione _
                   & " and  SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_UM=SISCOM_MI.TIPOLOGIA_UNITA_MISURA.ID (+) "

        If Rimborsi = True Then
            StringaSql = StringaSql & " and  SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE' "
        Else
            StringaSql = StringaSql & " and  SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE' "
        End If

        StringaSql = StringaSql & " order by SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO"


        par.cmd.Parameters.Clear()
        par.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim ds As New Data.DataTable()

        da.Fill(ds) ', "MANUTENZIONI_CONSUNTIVI")


        DataGrid2.DataSource = ds
        DataGrid2.DataBind()

        da.Dispose()
        ds.Dispose()

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub


    Protected Sub btn_Inserisci1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci1.Click
        If ControlloCampiConsuntivo() = False Then
            txtAppare2.Value = "1"
            Exit Sub
        End If

        If Me.txtID1.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaConsuntivo()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateConsuntivo()
        End If

        'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        'txtSel1.Text = ""
        'txtIdComponente.Text = ""

        If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
            Me.txtID1.Text = -1

            Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE"
            Me.txtCodArticolo.ReadOnly = True

            Me.lblQuantita.Visible = False
            Me.txtQuantita.Text = ""
            Me.txtQuantita.Visible = False

            Me.lblDettaglio.Visible = False
            Me.cmbUnitaMisura.SelectedValue = -1
            Me.cmbUnitaMisura.Visible = False

            Me.lblPrezzoUNI.Visible = False
            Me.txtPrezzo.Visible = False
            Me.lblEU1.Visible = False

            Me.lblTotale.Text = "Prezzo * (IVA inclusa)"
            Me.txtTotale.Text = ""
            Me.txtTotale.ReadOnly = False

            Me.txtTotale2.Value = ""
            Me.txtImportoODL.Value = 0

            Me.DataGrid2.Visible = True
            BindGrid2_Consuntivo(True)

            Me.txtDescrizione.Focus()
            Me.txtDescrizione.Text = ""
        Else

            Me.txtID1.Text = -1

            Me.txtCodArticolo.ReadOnly = False

            Me.txtDescrizione.Text = ""

            Me.lblQuantita.Visible = True
            Me.txtQuantita.Visible = True
            Me.txtQuantita.Text = ""

            Me.lblDettaglio.Visible = True
            Me.cmbUnitaMisura.Visible = True
            Me.cmbUnitaMisura.SelectedValue = -1

            Me.lblPrezzoUNI.Visible = True
            Me.txtPrezzo.Visible = True
            Me.txtPrezzo.Text = ""
            Me.lblEU1.Visible = True

            Me.lblTotale.Text = "Prezzo Totale *"
            Me.txtTotale.Text = ""
            Me.txtTotale.ReadOnly = True

            Me.txtTotale2.Value = ""
            Me.txtImportoODL.Value = 0

            Me.DataGrid2.Visible = True
            BindGrid2_Consuntivo(False)

            Me.txtCodArticolo.Focus()
            Me.txtCodArticolo.Text = ""
        End If


    End Sub


    Protected Sub btn_Modifica1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Modifica1.Click
        If ControlloCampiConsuntivo() = False Then
            txtAppare2.Value = "1"
            Exit Sub
        End If

        If Me.txtID1.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaConsuntivo()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateConsuntivo()
        End If

        BindGrid_Consuntivo()

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSel1.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Function ControlloCampiConsuntivo() As Boolean

        ControlloCampiConsuntivo = True


        If PAR.IfEmpty(Me.txtCodArticolo.Text, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire il codice articolo!", 300, 150, "Attenzione", "", "null")
            ControlloCampiConsuntivo = False
            txtCodArticolo.Focus()
            Exit Function
        End If


        If par.IfEmpty(Me.txtDescrizione.Text, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire la descrizione articolo!", 300, 150, "Attenzione", "", "null")
            ControlloCampiConsuntivo = False
            txtCodArticolo.Focus()
            Exit Function
        End If

        'If PAR.IfEmpty(Me.cmbUnitaMisura.Text, "Null") = "Null" Or Me.cmbUnitaMisura.Text = "-1" Then
        '    Response.Write("<script>alert('Inserire l\'unità di misura!');</script>")
        '    ControlloCampiConsuntivo = False
        '    cmbUnitaMisura.Focus()
        '    Exit Function
        'End If

        If PAR.IfEmpty(Me.txtQuantita.Text, "Null") = "Null" Then
            Me.txtQuantita.Text = 0
        End If

        If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
            If par.IfEmpty(Me.txtTotale.Text, "Null") = "Null" Then
                RadWindowManager1.RadAlert("Inserire il prezzo imponibile con IVA inclusa!", 300, 150, "Attenzione", "", "null")
                ControlloCampiConsuntivo = False
                Me.txtTotale.Focus()
                Exit Function
            End If
        Else
            If par.IfEmpty(Me.txtPrezzo.Text, "Null") = "Null" Then
                RadWindowManager1.RadAlert("Inserire il prezzo unitario!", 300, 150, "Attenzione", "", "null")
                ControlloCampiConsuntivo = False
                txtPrezzo.Focus()
                Exit Function
            End If
        End If

        'Controllo che la somma degli importi non superi quello RESIDUO TOTALE (APPALTI_LOTTI_SERVIZI.RESIDUO_CONSUMO)

        Dim SommaTot As Decimal = 0
        Dim SommaTotRimborsi As Decimal = 0
        Dim SommaTot1 As Decimal = 0

        SommaTot = TotalePrezzoConsuntivo()

        'For i = 0 To Me.DataGrid1.Items.Count - 1
        '    If Me.DataGrid1.Items(i).Cells(6).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(6).Text = ""
        '    SommaTot = SommaTot + par.IfEmpty(Me.DataGrid1.Items(i).Cells(6).Text, 0)
        'Next i

        If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
            SommaTotRimborsi = SommaTotRimborsi - Decimal.Parse(par.IfEmpty(Me.txtImportoODL.Value.Replace(".", ""), "0")) + Decimal.Parse(par.IfEmpty(Me.txtTotale.Text.Replace(".", ""), "0"))
        Else
            SommaTot = SommaTot - Decimal.Parse(par.IfEmpty(Me.txtImportoODL.Value.Replace(".", ""), "0")) + Decimal.Parse(par.IfEmpty(Me.txtTotale2.Value.Replace(".", ""), "0"))
        End If

        SommaTot1 = CalcolaImportiControllo(SommaTot, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtOneriC_MANO"), HiddenField).Value, 0))

        'In caso di rimborsi, non viene applicato lo sconto ecc
        SommaTot1 = SommaTot1 + SommaTotRimborsi

        If Math.Round(SommaTot1, 2) > Math.Round(Ricalcola_ImportoResiduo(), 2) Then
            'If Math.Round(SommaTot1, 5) > Math.Round((Decimal.Parse(par.IfEmpty(Me.txtResiduoConsumo.Value, "0")) + TotalePrezzoPreventivato()), 5) Then
            RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
            ControlloCampiConsuntivo = False
            txtQuantita.Focus()
            Exit Function
        End If

    End Function


    Function CalcolaImportiControllo(ByVal importo As Decimal, ByVal perc_oneri As Decimal, ByVal perc_sconto As Decimal, ByVal perc_iva As Decimal, ByVal fl_rit_legge As Decimal, ByVal oneri As Decimal) As Decimal

        Dim asta, iva, ritenuta, risultato1, risultato2, risultato3, ritenutaIVATA As Decimal

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva


        If oneri = 0 Then
            'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
            oneri = importo - ((importo * 100) / (100 + perc_oneri))
        End If
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta

        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
            'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If

        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri


        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        If perc_iva < 0 Then
            iva = 0
        Else
            iva = Math.Round((risultato3 * perc_iva) / 100, 2)
        End If

        'I) NETTO+ONERI+IVA
        CalcolaImportiControllo = risultato3 + iva + ritenutaIVATA


    End Function


    Private Sub SalvaConsuntivo()

        Try

            If vIdManutenzione <> -1 Then
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI_CONSUNTIVI  " _
                                            & " (ID, ID_MANUTENZIONI_INTERVENTI,ID_UM,COD_ARTICOLO,DESCRIZIONE,QUANTITA,PREZZO_UNITARIO,PREZZO_TOTALE) " _
                                    & "values (SISCOM_MI.SEQ_MANUTENZIONI_CONSUNTIVI.NEXTVAL,:id_manu,:id_um,:cod_articolo,:descrizione,:quantita,:prezzo_uni,:prezzo_tot) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_manu", vIdManutenzione))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_um", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbUnitaMisura.SelectedValue))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_articolo", par.PulisciStrSql(Me.txtCodArticolo.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStringaInvio(Me.txtDescrizione.Text, 300)))


                If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", 1))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_uni", strToNumber(Me.txtTotale.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_tot", strToNumber(Me.txtTotale.Text.Replace(".", ""))))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", strToNumber(Me.txtQuantita.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_uni", strToNumber(Me.txtPrezzo.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_tot", strToNumber(Me.txtTotale2.Value.Replace(".", ""))))
                End If


                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()




                '*** EVENTI_MANUTENZIONE
                par.InserisciEventoManu(par.cmd, vIdManutenzionePadre, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLI_MANUTENZIONE, "Consuntivo")


            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
            Me.txtModificato.Text = "0"


            '*** SOMMA IMPORTO
            'Dim SommaTot As Double = 0
            'Dim i As Integer

            'For i = 0 To Me.DataGrid1.Items.Count - 1
            '    If Me.DataGrid1.Items(i).Cells(6).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(6).Text = ""
            '    SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(6).Text, 0)
            'Next i


            'Me.txtImportoTotale.Text = Format(SommaTot, "##,##0.00")
            '**********************

            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdManutenzionePadre <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub UpdateConsuntivo()

        Try

            If vIdManutenzione <> -1 Then

                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA MANUTENZIONI

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update  SISCOM_MI.MANUTENZIONI_CONSUNTIVI  set " _
                                            & "ID_UM=:id_um,COD_ARTICOLO=:cod_articolo," _
                                            & "DESCRIZIONE=:descrizione,QUANTITA=:quantita," _
                                            & "PREZZO_UNITARIO=:prezzo_uni,PREZZO_TOTALE=:prezzo_tot " _
                                    & " where ID=" & Me.txtID1.Text

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_um", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbUnitaMisura.SelectedValue))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_articolo", par.PulisciStrSql(Me.txtCodArticolo.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStringaInvio(Me.txtDescrizione.Text, 300)))

                If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", 1))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_uni", strToNumber(Me.txtTotale.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_tot", strToNumber(Me.txtTotale.Text.Replace(".", ""))))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", strToNumber(Me.txtQuantita.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_uni", strToNumber(Me.txtPrezzo.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_tot", strToNumber(Me.txtTotale2.Value.Replace(".", ""))))
                End If


                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()

                '*** EVENTI_MANUTENZIONE
                par.InserisciEventoManu(par.cmd, vIdManutenzionePadre, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLI_MANUTENZIONE, "Consuntivo")


            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
            Me.txtModificato.Text = "0"

            '*** SOMMA IMPORTO
            'Dim SommaTot As Double = 0
            'Dim i As Integer

            'For i = 0 To Me.DataGrid1.Items.Count - 1
            '    If Me.DataGrid1.Items(i).Cells(6).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(6).Text = ""
            '    SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(6).Text, 0)
            'Next i


            'Me.txtImportoTotale.Text = Format(SommaTot, "##,##0.00")
            '**********************

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdManutenzionePadre <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnAgg1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgg1.Click
        Try


            Me.btn_Inserisci1.Visible = True
            Me.btn_Modifica1.Visible = False

            Me.lblTitolo1.Text = "Inserimento Consuntivi"
            Me.lblTitolo2.Text = "Elenco Consuntivi Inseriti"

            Me.txtID1.Text = -1

            Me.txtCodArticolo.ReadOnly = False

            Me.txtDescrizione.Text = ""

            Me.lblQuantita.Visible = True
            Me.txtQuantita.Visible = True
            Me.txtQuantita.Text = ""

            Me.lblDettaglio.Visible = True
            Me.cmbUnitaMisura.Visible = True
            Me.cmbUnitaMisura.SelectedValue = -1

            Me.lblPrezzoUNI.Visible = True
            Me.txtPrezzo.Visible = True
            Me.txtPrezzo.Text = ""
            Me.lblEU1.Visible = True

            Me.lblTotale.Text = "Prezzo Totale *"
            Me.txtTotale.Text = ""
            Me.txtTotale.ReadOnly = True

            Me.txtTotale2.Value = ""
            Me.txtImportoODL.Value = 0

            Me.DataGrid2.Visible = True
            BindGrid2_Consuntivo(False)

            Me.txtCodArticolo.Focus()
            Me.txtCodArticolo.Text = ""


        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnAggRimborso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggRimborso.Click
        Try

            Me.btn_Inserisci1.Visible = True
            Me.btn_Modifica1.Visible = False

            Me.lblTitolo1.Text = "Inserimento Rimborsi"
            Me.lblTitolo2.Text = "Elenco Rimborsi Inseriti"


            Me.txtID1.Text = -1

            Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE"
            Me.txtCodArticolo.ReadOnly = True

            Me.lblQuantita.Visible = False
            Me.txtQuantita.Text = ""
            Me.txtQuantita.Visible = False

            Me.lblDettaglio.Visible = False
            Me.cmbUnitaMisura.SelectedValue = -1
            Me.cmbUnitaMisura.Visible = False

            Me.lblPrezzoUNI.Visible = False
            Me.txtPrezzo.Visible = False
            Me.lblEU1.Visible = False

            Me.lblTotale.Text = "Prezzo * (IVA inclusa)"
            Me.txtTotale.Text = ""
            Me.txtTotale.ReadOnly = False

            Me.txtTotale2.Value = ""
            Me.txtImportoODL.Value = 0

            Me.DataGrid2.Visible = True
            BindGrid2_Consuntivo(True)

            Me.txtDescrizione.Focus()
            Me.txtDescrizione.Text = ""

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btn_Chiudi1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi1.Click
        BindGrid_Consuntivo()

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Me.txtModificato.Text = "0"

        Me.txtSel1.Text = ""
        Me.txtIdComponente.Text = ""

    End Sub

    Protected Sub btnApri1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri1.Click
        Try

            If txtIdComponente.Text = "" Then

                radWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare2.Value = "0"
            Else
                If vIdManutenzione <> -1 Then

                    Me.btn_Inserisci1.Visible = False
                    Me.btn_Modifica1.Visible = True

                    Me.lblTitolo2.Text = ""

                    If par.OracleConn.State = Data.ConnectionState.Open Then
                        Response.Write("IMPOSSIBILE VISUALIZZARE")
                        Exit Sub
                    Else
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                    End If

                   
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where ID=" & Me.txtIdComponente.Text

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                    myReader1 = par.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtID1.Text = par.IfNull(myReader1("ID"), -1)

                        Me.txtCodArticolo.Text = par.IfNull(myReader1("COD_ARTICOLO"), "")
                        Me.txtDescrizione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

                        Me.txtQuantita.Text = par.IfNull(myReader1("QUANTITA"), "")
                        Me.cmbUnitaMisura.SelectedValue = par.IfNull(myReader1("ID_UM"), "-1")

                        If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
                            Me.txtPrezzo.Text = IsNumFormat(par.IfNull(myReader1("PREZZO_TOTALE"), 0), "", "##,##0.00")
                            Me.txtTotale.Text = IsNumFormat(par.IfNull(myReader1("PREZZO_TOTALE"), 0), "", "##,##0.00")
                            Me.txtTotale2.Value = IsNumFormat(par.IfNull(myReader1("PREZZO_TOTALE"), 0), "", "##,##0.00")
                            Me.txtImportoODL.Value = IsNumFormat(par.IfNull(myReader1("PREZZO_TOTALE"), 0), "", "##,##0.00")
                        Else
                            Me.txtPrezzo.Text = IsNumFormat(par.IfNull(myReader1("PREZZO_UNITARIO"), 0), "", "##,##0.00")
                            Me.txtTotale.Text = IsNumFormat(par.IfNull(myReader1("PREZZO_TOTALE"), 0), "", "##,##0.00")
                            Me.txtTotale2.Value = IsNumFormat(par.IfNull(myReader1("PREZZO_TOTALE"), 0), "", "##,##0.00")
                            Me.txtImportoODL.Value = IsNumFormat(par.IfNull(myReader1("PREZZO_TOTALE"), 0), "", "##,##0.00")
                        End If

                        If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
                            Me.txtCodArticolo.ReadOnly = True

                            Me.lblQuantita.Visible = False
                            Me.txtQuantita.Visible = False

                            Me.lblDettaglio.Visible = False
                            Me.cmbUnitaMisura.Visible = False

                            Me.lblPrezzoUNI.Visible = False
                            Me.txtPrezzo.Visible = False
                            Me.lblEU1.Visible = False

                            Me.lblTotale.Text = "Prezzo * (IVA inclusa)"
                            Me.txtTotale.ReadOnly = False

                            Me.lblTitolo1.Text = "Modifica Rimborso"
                        Else
                            Me.txtCodArticolo.ReadOnly = False

                            Me.lblQuantita.Visible = True
                            Me.txtQuantita.Visible = True

                            Me.lblDettaglio.Visible = True
                            Me.cmbUnitaMisura.Visible = True

                            Me.lblPrezzoUNI.Visible = True
                            Me.txtPrezzo.Visible = True
                            Me.lblEU1.Visible = True

                            Me.lblTotale.Text = "Prezzo Totale *"
                            Me.txtTotale.ReadOnly = True

                            Me.lblTitolo1.Text = "Modifica Consuntivo"
                        End If

                    End If
                        myReader1.Close()

                    End If
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdManutenzionePadre <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnElimina1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina1.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    radWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")

                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppare2.Value = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else
                    If vIdManutenzione <> -1 Then
                        If par.OracleConn.State = Data.ConnectionState.Open Then
                            Response.Write("IMPOSSIBILE VISUALIZZARE")
                            Exit Sub
                        Else
                            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)
                            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                            '‘par.cmd.Transaction = par.myTrans


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "delete from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where ID = " & txtIdComponente.Text
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()

                            BindGrid_Consuntivo()

                            '*** EVENTI_MANUTENZIONE
                            par.InserisciEventoManu(par.cmd, vIdManutenzionePadre, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLI_MANUTENZIONE, "Consuntivo")


                        End If
                    End If
                    txtSel1.Text = ""
                    txtIdComponente.Text = ""

                    '*** SOMMA IMPORTO
                    'Dim SommaTot As Double = 0
                    'Dim i As Integer

                    'For i = 0 To Me.DataGrid1.Items.Count - 1
                    '    If Me.DataGrid1.Items(i).Cells(6).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(6).Text = ""
                    '    SommaTot = SommaTot + par.IfEmpty(Me.DataGrid1.Items(i).Cells(6).Text, 0)
                    'Next i


                    'Me.txtImportoTotale.Text = Format(SommaTot, "##,##0.00")
                    '**********************

                End If
                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
                Me.txtModificato.Text = "0"

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdManutenzionePadre <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "1"
            Response.Write("<script>window.close();</script>")
        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub


    Sub SettaMisure()
        Dim myReaderTMP1 As Oracle.DataAccess.Client.OracleDataReader

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            Me.cmbUnitaMisura.Items.Clear()
            Me.cmbUnitaMisura.Items.Add(New ListItem(" ", -1))


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_UNITA_MISURA order by COD"
            myReaderTMP1 = par.cmd.ExecuteReader()

            While myReaderTMP1.Read
                Me.cmbUnitaMisura.Items.Add(New ListItem(par.IfNull(myReaderTMP1("COD"), "") & "  (" & par.IfNull(myReaderTMP1("DESCRIZIONE"), " ") & ")", par.IfNull(myReaderTMP1("ID"), -1)))
            End While

            myReaderTMP1.Close()
            par.cmd.CommandText = ""

            Me.cmbUnitaMisura.SelectedValue = "-1"


            'CAMPI per il controllo del prezzo, non deve superare quello residuo
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.IVA_CONSUMO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CON,APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,APPALTI.FL_RIT_LEGGE,MANUTENZIONI.ID_APPALTO " _
                               & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                               & " where MANUTENZIONI.ID=" & vIdManutenzionePadre _
                               & "   and APPALTI.ID=MANUTENZIONI.ID_APPALTO" _
                               & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO = MANUTENZIONI.ID_APPALTO  " _
                               & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=MANUTENZIONI.ID_PF_VOCE_IMPORTO "

            myReaderTMP1 = par.cmd.ExecuteReader()

            If myReaderTMP1.Read Then

                Me.txtPercIVA.Value = par.IfNull(myReaderTMP1("IVA_CONSUMO"), -1)
                Me.txtOneriC_MANO.value = par.IfNull(myReaderTMP1("IMPORTO_ONERI_CONS"), 0)

                Me.txtPercOneri.Value = par.IfNull(myReaderTMP1("PERC_ONERI_SIC_CON"), 0)
                Me.txtScontoConsumo.Value = par.IfNull(myReaderTMP1("SCONTO_CONSUMO"), 0)


                Me.txtFL_RIT_LEGGE.Value = par.IfNull(myReaderTMP1("FL_RIT_LEGGE"), 0)

                vIdAppalto = par.IfNull(myReaderTMP1("ID_APPALTO"), -1)
            End If
            myReaderTMP1.Close()
            '*************************************


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdManutenzionePadre <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Function TotalePrezzoConsuntivo() As Decimal
        Dim myReaderTMP1 As Oracle.DataAccess.Client.OracleDataReader

        Try

            TotalePrezzoConsuntivo = 0

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select SUM(PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where ID_MANUTENZIONI_INTERVENTI in (" _
                                    & " select ID from SISCOM_MI.MANUTENZIONI_INTERVENTI where ID_MANUTENZIONE=" & vIdManutenzionePadre & ")"
            myReaderTMP1 = par.cmd.ExecuteReader()

            If myReaderTMP1.Read Then
                TotalePrezzoConsuntivo = par.IfNull(myReaderTMP1(0), 0)
            End If

            myReaderTMP1.Close()
            par.cmd.CommandText = ""


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdManutenzionePadre <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Function


    Function TotalePrezzoPreventivato() As Decimal
        Dim myReaderTMP1 As Oracle.DataAccess.Client.OracleDataReader

        Try

            TotalePrezzoPreventivato = 0

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.Parameters.Clear()
            ' par.cmd.CommandText = "select SUM(IMPORTO_PRESUNTO) from SISCOM_MI.MANUTENZIONI_INTERVENTI where ID_MANUTENZIONE=" & vIdManutenzionePadre
            par.cmd.CommandText = "select SUM(IMPORTO_TOT) from SISCOM_MI.MANUTENZIONI where ID=" & vIdManutenzionePadre

            myReaderTMP1 = par.cmd.ExecuteReader()

            If myReaderTMP1.Read Then
                TotalePrezzoPreventivato = par.IfNull(myReaderTMP1(0), 0)
            End If

            myReaderTMP1.Close()
            par.cmd.CommandText = ""


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdManutenzionePadre <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Function


    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


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


    Private Sub FrmSolaLettura()
        Try

            Me.btnAgg1.Visible = False
            Me.btnAggRimborso.Visible = False

            Me.btnElimina1.Visible = False
            Me.btnApri1.Visible = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function Ricalcola_ImportoResiduo() As Decimal
        Dim FlagConnessione As Boolean
        Dim sStr1 As String

        Dim ris1, ris2 As Decimal

        Try

            FlagConnessione = False
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Ricalcola_ImportoResiduo = 0

            'CALCOLO l'IMPORTO RESIDUO dato da:

            '1) la somma di eventuali variazioni all'importo residuo di APPALTI_VARIAZIONI_IMPORTI

            sStr1 = "select SUM(IMPORTO) " _
                 & " from   SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
                 & " where  ID_VARIAZIONE in (select ID from SISCOM_MI.APPALTI_VARIAZIONI " _
                                         & " where ID_APPALTO=" & vIdAppalto & ")"

            'in data 2/11/2011 non è più filtrato per Voce Business Plan
            ' and ID_PF_VOCE_IMPORTO = " & Me.cmbServizioVoce.SelectedValue 


            PAR.cmd.Parameters.Clear()
            PAR.cmd.CommandText = sStr1
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

            If myReader1.Read Then
                Ricalcola_ImportoResiduo = PAR.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()


            '2)la somma dell'importo calcolato (IMPORTO-CONSUMO+IVA) 

            sStr1 = "select * from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                 & " where ID_APPALTO=" & vIdAppalto

            'in data 2/11/2011 non è più filtrato per Voce Business Plan
            '& "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
            '                              & " where ID_LOTTO=" & Me.txtIdLotto.Value _
            '                              & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue & ")"


            PAR.cmd.Parameters.Clear()
            PAR.cmd.CommandText = sStr1
            myReader1 = PAR.cmd.ExecuteReader()

            While myReader1.Read
                'IMPORTO a CONSUMO senza IVA=IMPORTO_CONSUMO-(IMPORTO_COSUMO*SCONTO_CONSUMO/100)
                'ris1 = par.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - par.IfNull(myReader1("SCONTO_CONSUMO"), 0)
                ris1 = PAR.IfNull(myReader1("IMPORTO_CONSUMO"), 0) * PAR.IfNull(myReader1("SCONTO_CONSUMO"), 0) / 100
                ris1 = PAR.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - ris1

                ris1 = ris1 + PAR.IfNull(myReader1("ONERI_SICUREZZA_CONSUMO"), 0) ' par.IfEmpty(Me.txtOneriSicurezza.Value, 0)

                If PAR.IfNull(myReader1("IVA_CONSUMO"), 0) > 0 Then
                    ris2 = ris1 * PAR.IfNull(myReader1("IVA_CONSUMO"), 0) / 100
                Else
                    ris2 = 0
                End If
                Ricalcola_ImportoResiduo = Ricalcola_ImportoResiduo + ris1 + ris2
            End While
            myReader1.Close()


            '3)la SommaResiduo va sottratto alla somma dell'IMPORTO PRENOTATO o CONSUNTIVATO o EMESSO PAGAMENTO (SAL) da MANUTENZIONI 
            sStr1 = "select SUM(IMPORTO_TOT) from SISCOM_MI.MANUTENZIONI " _
                 & " where ID_APPALTO=" & vIdAppalto _
                 & "   and ID_PF_VOCE_IMPORTO is not null " _
                 & "   and STATO in (1,2,4)"

            'TUTTO CIO' SPESO tranne quelle speso dalla manutenzioni in modifica
            If vIdManutenzionePadre > 0 Then
                sStr1 = sStr1 & " and ID<>" & vIdManutenzionePadre
            End If

            'in data 2/11/2011 non è più filtrato per Voce Business Plan
            '& "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue 

            PAR.cmd.Parameters.Clear()
            PAR.cmd.CommandText = sStr1
            myReader1 = PAR.cmd.ExecuteReader()

            If myReader1.Read Then
                Ricalcola_ImportoResiduo = Ricalcola_ImportoResiduo - PAR.IfNull(myReader1(0), 0) '+ par.IfEmpty(Me.txtOneriSicurezza.Value, 0)
            End If
            myReader1.Close()


            If FlagConnessione = True Then
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Ricalcola_ImportoResiduo = 0

            If FlagConnessione = True Then
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Function

End Class
