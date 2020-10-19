
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VariazioneImportiNP
    Inherits System.Web.UI.UserControl
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            '   Me.txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")


            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.APPALTI_TIPI_VARIAZIONE", ddlTipoVariazione, "ID", "DESCRIZIONE", True)
            'par.caricaComboBox("SELECT ID, DESCRIZIONE FROM SISCOM_MI.pf_voci WHERE ID IN (SELECT id_pf_voce FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & ") ORDER BY DESCRIZIONE", ddlVoceServizio, "ID", "DESCRIZIONE", True)
            'CaricaImpServizi()
            CaricaVarServizi()
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                frmsololettura()
            End If
        End If

    End Sub
    Private Sub frmsololettura()
        'DataGridVCan.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
        'DataGridVCan.MasterTableView.Columns.FindByUniqueName("modificaServizio").Visible = False
        DataGridVCan.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
        DataGridVCan.Rebind()
        '  imgAddVarImporti.Visible = False
        'btnDelAutoCan.Visible = False
    End Sub
    Private Sub AddJavascriptFunction(ByVal Data As DataGrid, ByVal txtname As String)
        Dim i As Integer = 0
        Dim di As DataGridItem
        For i = 0 To Data.Items.Count - 1
            di = Data.Items(i)
            DirectCast(di.Cells(4).FindControl(txtname), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariazAuto(this);")
        Next
    End Sub
    Public Sub CaricaVarServizi()
        Try
            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            Dim tipologia As Integer = 6

            par.cmd.CommandText = "SELECT DATA_ORA_OP, TRIM (TO_CHAR (IMPORTO_CONSUMO, '9G999G999G999G999G990D99')) AS IMPORTO_CONSUMO, " _
                                & " TRIM (TO_CHAR (ONERI_SICUREZZA_CONSUMO, '9G999G999G999G999G990D99')) AS ONERI_SICUREZZA_CONSUMO, " _
                                & " TRIM (TO_CHAR (IMPORTO_CANONE, '9G999G999G999G999G990D99')) AS IMPORTO_CANONE, " _
                                & " TRIM (TO_CHAR (ONERI_SICUREZZA_CANONE, '9G999G999G999G999G990D99')) AS ONERI_SICUREZZA_CANONE " _
                                & " FROM SISCOM_MI.APPALTI_VOCI_PF_S " _
                                & " WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " ORDER BY DATA_ORA_OP DESC"


            'par.cmd.CommandText = "SELECT ID, TO_CHAR (TO_DATE (DATA_VARIAZIONE, 'yyyymmdd'), 'dd/mm/yyyy') AS DATA_VARIAZIONE, " _
            '                    & "(SELECT pf_voci.descrizione FROM siscom_mi.pf_voci WHERE ID = id_pf_voce) AS DESC_VOCE, " _
            '                    & "(SELECT descrizione FROM SISCOM_MI.tipologia_variazioni WHERE id = SISCOM_MI.appalti_variazioni.id_tipologia) || ' '  " _
            '                    & "|| TRIM (TO_CHAR (IMPORTO, '9G999G999G999G999G990D99')) || ' € - ' || " _
            '                    & "(SELECT descrizione FROM SISCOM_MI.appalti_tipi_variazione WHERE id = SISCOM_MI.appalti_variazioni.id_tipo_variazione) AS tipo_variazione " _
            '                    & "FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
            '                    & "WHERE     APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE " _
            '                    & "AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            Me.DataGridVCan.DataSource = dt
            DataGridVCan.DataBind()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_VaImporti"
        End Try
    End Sub
    Protected Sub btn_AddVariazAutoCan_Click(sender As Object, e As System.EventArgs) Handles btn_AddVariazAutoCan.Click
        SalvaAutoCan()
    End Sub
    'Private Sub SalvaAutoCan()
    '    Try

    '        If Not IsNothing(txtData.SelectedDate) Or Not String.IsNullOrEmpty(txtImportoVar.Text.ToString) Or ddlTipoVariazione.SelectedValue = "-1" Or ddlVoceServizio.SelectedValue = "-1" Then
    '            Dim annoinizio As String = DirectCast(Me.Page.FindControl("txtannoinizio"), RadDatePicker).SelectedDate
    '            Dim annofine As String = DirectCast(Me.Page.FindControl("txtannofine"), RadDatePicker).SelectedDate
    '            If par.AggiustaData(txtData.SelectedDate) < par.AggiustaData(annoinizio) Or par.AggiustaData(txtData.SelectedDate) > par.AggiustaData(annofine) Then
    '                Response.Write("<SCRIPT>alert('La data della variazione deve essere compresa nel periodo di durata dell\'appalto!');</SCRIPT>")
    '                hfRestaVisible.Value = 1
    '                Exit Sub
    '            End If

    '            Dim i As Integer = 0

    '            '*******************RICHIAMO LA CONNESSIONE*********************
    '            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '            par.SettaCommand(par)
    '            '*******************RICHIAMO LA TRANSAZIONE*********************
    '            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '            '‘par.cmd.Transaction = par.myTrans
    '            'CONTROLLO SUI DATI INSERITI 
    '            Dim tipologia As Integer = 0
    '            If radioBtnCanone.Checked Then
    '                tipologia = 5
    '            ElseIf radioBtnConsumo.Checked Then
    '                tipologia = 6
    '            End If
    '            Dim residuo As Integer = 0
    '            If tipologia = 5 Then
    '                residuo = CInt(DirectCast(Me.Page.FindControl("HiddenResiduoCanone"), HiddenField).Value)
    '            ElseIf tipologia = 6 Then
    '                residuo = CInt(DirectCast(Me.Page.FindControl("HiddenResiduoConsumo"), HiddenField).Value)
    '            End If
    '            If (residuo + CInt(txtImportoVar.Text)) < 0 Then
    '                Response.Write("<script>alert('Il residuo contrattuale non può essere negativo!\nModificare l\' importo della variazione!');</script>")
    '                hfRestaVisible.Value = 1
    '            Else
    '                '*********CONTROLLO NON ESISTA GIà UNA VARIAZIONE CON LA STESSA DATA SUL CONTRATTO
    '                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND DATA_VARIAZIONE = " & par.AggiustaData(Me.txtData.Text) & " AND ID_TIPOLOGIA = " & tipologia
    '                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '                'If myReader.Read Then
    '                '    Response.Write("<script>alert('Attenzione...Variazione esistente su questo appalto e con la stessa data!');</script>")
    '                '    hfRestaVisible.Value = 1
    '                '    myReader.Close()
    '                '    Exit Sub
    '                'End If

    '                If tipologia = 5 Then
    '                    par.cmd.CommandText = "SELECT SUM ((Importo_canone - (Importo_canone*(sconto_canone/100))))  AS ImpCont FROM  siscom_mi.APPALTI_LOTTI_SERVIZI WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
    '                ElseIf tipologia = 6 Then
    '                    par.cmd.CommandText = "SELECT SUM ((Importo_CONSUMO - (Importo_CONSUMO*(sconto_consumo/100))))  AS ImpCont FROM  siscom_mi.APPALTI_LOTTI_SERVIZI WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
    '                End If
    '                Dim ImpContSenzaIva As Decimal = 0
    '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '                If myReader.Read Then
    '                    ImpContSenzaIva = myReader("ImpCont")
    '                End If
    '                myReader.Close()
    '                If ImpContSenzaIva > 0 Then
    '                    Dim idTipoVariazione As String = "null"
    '                    Dim idVoceServizio As String = ""
    '                    If ddlTipoVariazione.SelectedValue <> "-1" Then
    '                        idTipoVariazione = ddlTipoVariazione.SelectedValue
    '                    End If
    '                    Dim IdVoceServ As String = "null"
    '                    If ddlVoceServizio.SelectedValue <> "-1" Then
    '                        IdVoceServ = ddlVoceServizio.SelectedValue
    '                    End If
    '                    If String.IsNullOrEmpty(idSelected.Value) Then
    '                        'INSERT
    '                        Dim primoGruppo As Boolean = True
    '                        Dim IdAppVariazione As Integer = 0
    '                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
    '                        myReader = par.cmd.ExecuteReader
    '                        If myReader.Read Then
    '                            IdAppVariazione = myReader(0)
    '                        End If
    '                        myReader.Close()
    '                        If primoGruppo = True Then
    '                            HFidGruppoVariazione.Value = IdAppVariazione
    '                            primoGruppo = False
    '                        End If

    '                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO, ID_TIPO_VARIAZIONE)" _
    '                            & " VALUES(" & IdAppVariazione & ", " & CType(Me.Page, Object).vIdAppalti & ",'" & par.AggiustaData(Me.txtData.SelectedDate) & "'," & tipologia & ",'" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "', " & idTipoVariazione & ")"
    '                        par.cmd.ExecuteNonQuery()
    '                        '****************INIZIO A SALVARE LA VARIAZIONE O LE VARIAZIONI


    '                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,id_pf_voce,IMPORTO,PERCENTUALE, ID_GRUPPO_VARIAZIONE) VALUES (" & IdAppVariazione _
    '                                            & ", " & IdVoceServ & "," & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoVar.Text, 0)) & ",'', " & HFidGruppoVariazione.Value & ")"
    '                        par.cmd.ExecuteNonQuery()
    '                        SalvaPluriennali(IdAppVariazione, tipologia)
    '                        CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaImpContrattuale()
    '                        CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaResiduo()
    '                    Else
    '                        'UPDATE
    '                        par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_VARIAZIONI " _
    '                                            & "SET    DATA_VARIAZIONE = " & par.AggiustaData(Me.txtData.SelectedDate) & ", " _
    '                                            & "       ID_TIPOLOGIA    = " & tipologia & ", " _
    '                                            & "       PROVVEDIMENTO   = '" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "' " _
    '                                            & "WHERE  ID              = " & idSelected.Value
    '                        par.cmd.ExecuteNonQuery()
    '                        'par.cmd.CommandText = "SELECT nvl(ID,'-1') FROM siscom_mi.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & idSelected.Value
    '                        'Dim idApp As String = par.cmd.ExecuteScalar.ToString
    '                        par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
    '                                            & "SET    id_pf_voce 		= " & IdVoceServ & ", " _
    '                                            & "       IMPORTO            		= " & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoVar.Text, 0)) & ", " _
    '                                            & "       PERCENTUALE        		= '' " _
    '                                            & "WHERE  ID_VARIAZIONE           	= " & idSelected.Value
    '                        par.cmd.ExecuteNonQuery()
    '                        SalvaPluriennali(idSelected.Value, tipologia)
    '                        CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaImpContrattuale()
    '                        CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaResiduo()
    '                    End If
    '                Else
    '                    If tipologia = 5 Then
    '                        Response.Write("<script>alert('Nessun importo a Canone per questo appalto!');</script>")
    '                    Else
    '                        Response.Write("<script>alert('Nessun importo a Consumo per questo appalto!');</script>")
    '                    End If
    '                    Me.txtData.Clear()
    '                    Me.txtImportoVar.Text = ""
    '                    Me.txtNote.Text = ""
    '                    hfRestaVisible.Value = 1
    '                    ddlTipoVariazione.SelectedValue = "-1"
    '                    ddlVoceServizio.SelectedValue = "-1"
    '                End If
    '                Me.txtData.Clear()
    '                ddlTipoVariazione.SelectedValue = "-1"
    '                ddlVoceServizio.SelectedValue = "-1"
    '                Me.txtImportoVar.Text = ""
    '                Me.txtNote.Text = ""
    '                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
    '                CaricaVarServizi()
    '                'End If
    '                hfRestaVisible.Value = 0
    '            End If
    '        Else
    '            Response.Write("<script>alert('Tutti i campi ad eccezione del PROVVEDIMENTO sono obbligatori!');</script>")
    '            hfRestaVisible.Value = 1
    '        End If
    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_VaImporti"
    '        par.myTrans.Rollback()

    '    End Try
    'End Sub

    Private Sub SalvaAutoCan()
        Try

            If Not IsNothing(txtData.SelectedDate) And Not String.IsNullOrEmpty(txtImportoVar.Text.ToString) Then
                Dim annoinizio As String = DirectCast(Me.Page.FindControl("txtannoinizio"), RadDatePicker).SelectedDate
                Dim annofine As String = DirectCast(Me.Page.FindControl("txtannofine"), RadDatePicker).SelectedDate
                If par.AggiustaData(txtData.SelectedDate) < par.AggiustaData(annoinizio) Or par.AggiustaData(txtData.SelectedDate) > par.AggiustaData(annofine) Then
                    RadWindowManager1.RadAlert("La data della variazione deve essere compresa nel periodo di durata dell\'appalto!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If
                Dim stringaOneri As String = ""

                Dim i As Integer = 0
                Dim messaggioOneri As Boolean = True

                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                'CONTROLLO SUI DATI INSERITI 
                Dim tipologia As Integer = 6

                Dim residuo As Integer = 0
                residuo = CInt(DirectCast(Me.Page.FindControl("HiddenResiduoConsumo"), HiddenField).Value)
                If (residuo + CInt(txtImportoVar.Text)) < 0 Then
                    RadWindowManager1.RadAlert("Il residuo contrattuale non può essere negativo!\nModificare l\' importo della variazione!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                    'Dim script As String = "function f(){$find(""" + RadWindowVarImporti.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    'RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                Else
                    '*********CONTROLLO NON ESISTA GIà UNA VARIAZIONE CON LA STESSA DATA SUL CONTRATTO
                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND DATA_VARIAZIONE = " & par.AggiustaData(Me.txtData.Text) & " AND ID_TIPOLOGIA = " & tipologia
                    'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    'If myReader.Read Then
                    '    Response.Write("<script>alert('Attenzione...Variazione esistente su questo appalto e con la stessa data!');</script>")
                    '    hfRestaVisible.Value = 1
                    '    myReader.Close()
                    '    Exit Sub
                    'End If

                    If tipologia = 5 Then
                        par.cmd.CommandText = "SELECT SUM ((Importo_canone - (Importo_canone*(sconto_canone/100))))  AS ImpCont, sum(ONERI_SICUREZZA_CANONE) as sconto FROM  siscom_mi.APPALTI_VOCI_PF WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
                    ElseIf tipologia = 6 Then
                        par.cmd.CommandText = "SELECT SUM ((Importo_CONSUMO - (Importo_CONSUMO*(sconto_consumo/100))))  AS ImpCont, sum(ONERI_SICUREZZA_CONSUMO) as sconto FROM  siscom_mi.APPALTI_VOCI_PF WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
                    End If
                    Dim ImpContSenzaIva As Decimal = 0
                    Dim sconto As Decimal = 0
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        ImpContSenzaIva = myReader("ImpCont")
                        sconto = myReader("sconto")
                    End If
                    myReader.Close()
                    If ImpContSenzaIva > 0 Then
                        Dim idTipoVariazione As String = "null"
                        Dim idVoceServizio As String = ""
                        If ddlTipoVariazione.SelectedValue <> "-1" Then
                            idTipoVariazione = ddlTipoVariazione.SelectedValue
                        End If
                        Dim IdVoceServ As String = "null"
                        'If ddlVoceServizio.SelectedValue <> "-1" Then
                        '    IdVoceServ = ddlVoceServizio.SelectedValue
                        'End If
                        Dim tipoImporto As String = ""
                        Dim tipoOneri As String = ""
                        Dim sommaImporti As Decimal = 0
                        Dim sommaOneri As Decimal = 0
                        If tipologia = 5 Then
                            par.cmd.CommandText = "SELECT SUM(IMPORTO_CANONE), SUM(ONERI_SICUREZZA_CANONE) FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                            tipoImporto = "IMPORTO_CANONE"
                            tipoOneri = "ONERI_SICUREZZA_CANONE"
                        Else
                            par.cmd.CommandText = "SELECT SUM(IMPORTO_CONSUMO), SUM(ONERI_SICUREZZA_CONSUMO) FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                            tipoImporto = "IMPORTO_CONSUMO"
                            tipoOneri = "ONERI_SICUREZZA_CONSUMO"
                        End If
                        myReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            sommaImporti = CDec(par.IfEmpty(myReader(0).ToString, "0"))
                            sommaOneri = CDec(par.IfEmpty(myReader(1).ToString, "0"))
                        End If
                        myReader.Close()
                        par.cmd.CommandText = "SELECT id_pf_voce FROM SISCOM_mI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                        Dim dtPfVociImporto As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                        For Each rigaImporto As Data.DataRow In dtPfVociImporto.Rows
                            par.cmd.CommandText = "select id_appalto, id_pf_voce from SISCOM_MI.APPALTI_VOCI_PF where id_appalto in " _
                                    & "(select id from SISCOM_MI.appalti where id_gruppo=(select id_Gruppo from SISCOM_MI.appalti where id=" & CType(Me.Page, Object).vIdAppalti & ")) " _
                                    & "and id_pf_voce in ( " _
                                    & "select id from SISCOM_MI.pf_voci connect by prior id=id_old " _
                                    & "start with id= " & rigaImporto.Item("id_pf_voce").ToString _
                                    & "union " _
                                    & "select id from SISCOM_MI.pf_voci connect by prior id_old=id " _
                                    & "start with id=" & rigaImporto.Item("id_pf_voce").ToString & ") "
                            Dim dtVoci As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                            For Each riga1 As Data.DataRow In dtVoci.Rows
                                If sconto > 0 Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_VOCI_PF SET " _
                                                   & tipoOneri & "  = " & tipoOneri & " + round((" & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoOneri.Text, 0)) & " / " & par.VirgoleInPunti(sommaOneri) & " * " & tipoOneri & "),2), " _
                                                    & tipoImporto & "  = " & tipoImporto & " + round((" & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoVar.Text, 0)) & " / " & par.VirgoleInPunti(sommaImporti) & " * " & tipoImporto & "),2) " _
                                                    & " WHERE ID_APPALTO = " & riga1.Item("ID_APPALTO") _
                                                    & " AND id_pf_voce = " & riga1.Item("id_pf_voce")
                                    par.cmd.ExecuteNonQuery()

                                    stringaOneri = "DI ONERI €" & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoOneri.Text, 0)) & " E "
                                    messaggioOneri = False
                                Else
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_VOCI_PF SET " _
                                                    & tipoImporto & "  = " & tipoImporto & " + round((" & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoVar.Text, 0)) & " / " & par.VirgoleInPunti(sommaImporti) & " * " & tipoImporto & "),2) " _
                                                   & " WHERE ID_APPALTO = " & riga1.Item("ID_APPALTO") _
                                                   & " AND id_pf_voce = " & riga1.Item("id_pf_voce")
                                    par.cmd.ExecuteNonQuery()
                                End If
                            Next
                        Next
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI (ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & CType(Me.Page, Object).vIdAppalti & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F223','Modifica appalti - INSERIMENTO VARIAZIONE CONTRATTUALE " & stringaOneri & "DI IMPORTO €" & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoVar.Text, 0)) & " CON PROVVEDIMENTO: " & par.PulisciStrSql(txtNote.Text) & "')"
                        par.cmd.ExecuteNonQuery()
                        Dim t = CType(Me.Page.FindControl("Tab_VociNPl1"), Object)

                        CType(t.FindControl("DataGrid3"), RadGrid).Rebind()
                        CType(Me.Page.FindControl("Tab_VociNPl1"), Object).BindGrid_Servizi()
                        CType(Me.Page.FindControl("Tab_VociNPl1"), Object).somma()
                        CType(Me.Page.FindControl("Tab_VociNPl1"), Object).CalcolaImpContrattuale()
                        CType(Me.Page.FindControl("Tab_VociNPl1"), Object).CalcolaResiduo()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI SET " _
                                            & " TOT_CONSUMO = " & par.VirgoleInPunti(par.IfEmpty(DirectCast(CType(Me.Page.FindControl("Tab_ImportiNP1"), Object).FindControl("txtImpTotPlusOneriCon"), TextBox).Text.Replace(".", ""), 0)) _
                                            & " WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & CType(Me.Page, Object).vIdAppalti & ")"
                        par.cmd.ExecuteNonQuery()
                    Else
                        If tipologia = 5 Then

                            RadWindowManager1.RadAlert("Nessun importo a Canone per questo appalto!", 300, 150, "Attenzione", "", "null")
                        Else

                            RadWindowManager1.RadAlert("Nessun importo a Consumo per questo appalto!", 300, 150, "Attenzione", "", "null")
                        End If
                        Me.txtData.Clear()
                        Me.txtImportoVar.Text = ""
                        Me.txtImportoOneri.Text = ""
                        Me.txtNote.Text = ""
                        Dim script As String = "function f(){$find(""" + RadWindowVarImporti.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                        ddlTipoVariazione.SelectedValue = "-1"
                        ' ddlVoceServizio.SelectedValue = "-1"
                    End If
                    If messaggioOneri = True And txtImportoOneri.Text.Length > 0 Then
                        If tipologia = 5 Then

                            RadWindowManager1.RadAlert("Oneri a canone non presenti per questo appalto!", 300, 150, "Attenzione", "", "null")
                        Else
                            RadWindowManager1.RadAlert("Oneri a consumo non presenti per questo appalto!", 300, 150, "Attenzione", "", "null")

                        End If
                    End If
                    Me.txtData.Clear()
                    ddlTipoVariazione.SelectedValue = "-1"
                    'ddlVoceServizio.SelectedValue = "-1"
                    Me.txtImportoVar.Text = ""
                    Me.txtImportoOneri.Text = ""
                    Me.txtNote.Text = ""
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                    CaricaVarServizi()
                    'End If
                    hfRestaVisible.Value = 0
                End If
            Else
                RadWindowManager1.RadAlert("Tutti i campi ad eccezione del PROVVEDIMENTO sono obbligatori!", 300, 150, "Attenzione", "", "null")
                'Dim script As String = "function f(){$find(""" + RadWindowVarImporti.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                'RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_VaImporti"
            par.myTrans.Rollback()
        End Try
    End Sub

    Protected Sub btn_ChiudiAppalti_Click(sender As Object, e As System.EventArgs) Handles btn_ChiudiAppalti.Click
        txtImportoVar.Text = ""
        txtImportoOneri.Text = ""
        txtData.Clear()
        txtNote.Text = ""
        hfRestaVisible.Value = "0"
        ddlTipoVariazione.SelectedValue = "-1"
        'ddlVoceServizio.SelectedValue = "-1"
    End Sub
    'Protected Sub btn_ChiudiConsumo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiConsumo.Click
    '    Me.txtPercVarCons.Text = ""
    '    Me.txtDataConsumo.Text = ""
    '    Me.txtNoteConsumo.Text = ""
    '    hfRestaVisibleCon.Value = "0"
    '    Dim i As Integer = 0
    '    Dim di As DataGridItem

    '    For i = 0 To DgvServAutoCons.Items.Count - 1
    '        di = DgvServAutoCons.Items(i)
    '        DirectCast(di.Cells(4).FindControl("txtPercVarCons"), TextBox).Text = ""
    '    Next

    'End Sub
    Private Sub Elimina()

        If Me.idSelected.Value > 0 Then
            Try
                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE ID_GRUPPO_VARIAZIONE = " & idSelected.Value
                Dim importo As Integer = CInt(par.IfEmpty(par.cmd.ExecuteScalar, "0"))
                Dim residuo As Integer = 0
                par.cmd.CommandText = "select id_tipologia from SISCOM_MI.APPALTI_VARIAZIONI WHERE ID = " & idSelected.Value
                Dim tipologia As String = par.IfNull(par.cmd.ExecuteScalar, "0")
                If tipologia = 5 Then
                    residuo = CInt(DirectCast(Me.Page.FindControl("HiddenResiduoCanone"), HiddenField).Value)
                ElseIf tipologia = 6 Then
                    residuo = CInt(DirectCast(Me.Page.FindControl("HiddenResiduoConsumo"), HiddenField).Value)
                End If
                If (residuo - importo) < 0 Then
                    RadWindowManager1.RadAlert("Il residuo contrattuale non può essere negativo!\nImpossibile eliminare!", 300, 150, "Attenzione", "", "null")
                Else
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE ID_GRUPPO_VARIAZIONE = " & idSelected.Value
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID = " & idSelected.Value
                    par.cmd.ExecuteNonQuery()
                    Me.idSelected.Value = 0
                    Me.hfElimina.Value = 0
                    CaricaVarServizi()
                    CaricaImporti()
                    CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaImpContrattuale()
                    CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaResiduo()
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                End If
            Catch ex As Exception
                CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_VaImporti"
                par.myTrans.Rollback()
            End Try
        End If

    End Sub
    Protected Sub DataGridVCan_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGridVCan.ItemCommand
        Try
            Select Case e.CommandName
                Case "myEdit"
                    CaricaImporti()
                Case "Delete"
                    Elimina()
                    CaricaVarServizi()
            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_VaImporti"
            par.myTrans.Rollback()
        End Try
    End Sub
    Protected Sub DataGridVCan_ItemDataBound1(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridVCan.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('Tab_VariazioneImporti1_txtmia').value='Hai selezionato la variazione a CANONE del " & dataItem("DATA_ORA_OP").Text.Replace("'", "\'") & " ';document.getElementById('Tab_VariazioneImporti1_idSelected').value='" & dataItem("ID").Text & "'")
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                'dataItem("DeleteColumn").Enabled = False
                'dataItem("modificaServizio").Enabled = False
                DataGridVCan.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            End If
        End If
    End Sub
    'Protected Sub DataGridVCan_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVCan.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('Tab_VariazioneImporti1_txtmia').value='Hai selezionato la variazione a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VariazioneImporti1_idSelected').value='" & e.Item.Cells(0).Text & "'")
    '        e.Item.Attributes.Add("onDblClick", "document.getElementById('Tab_VariazioneImporti1_imgModVarImporti').click();")
    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('Tab_VariazioneImporti1_txtmia').value='Hai selezionato la variazione a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VariazioneImporti1_idSelected').value='" & e.Item.Cells(0).Text & "'")
    '        e.Item.Attributes.Add("onDblClick", "document.getElementById('Tab_VariazioneImporti1_imgModVarImporti').click();")
    '    End If
    'End Sub

    'Protected Sub DataGridVCons_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVCons.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VarAutomatica1_txtmia').value='Hai selezionato la variazione a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VarAutomatica1_idSelected').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VarAutomatica1_txtmia').value='Hai selezionato la variazione a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VarAutomatica1_idSelected').value='" & e.Item.Cells(0).Text & "'")
    '    End If

    'End Sub

    Private Sub SalvaPluriennali(ByVal idVariazione As Integer, ByVal idTipo As Integer)


        'Verifico se l'appalto è pluriennale
        par.cmd.CommandText = "select id as id_appalto,'' as id_pf_voce from siscom_mi.appalti where id_gruppo = " _
                    & "(select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " ) order by id_appalto asc"
        Dim daAppalti As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtAppalti As New Data.DataTable()
        daAppalti.Fill(dtAppalti)
        daAppalti.Dispose()
        If dtAppalti.Rows.Count > 1 Then
            Dim rIApp As Integer = 0
            Dim reader As Oracle.DataAccess.Client.OracleDataReader
            '***********************  creo la datatable sulla quale ciclare per creare le variazioni sugli appalti pluriennali 
            par.cmd.CommandText = "select '' as id_appalto,'' as id_pf_voce,0 as importo,0 as PERCENTUALE from dual"
            Dim daInsert As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtInsert As New Data.DataTable()
            daInsert.Fill(dtInsert)


            ''tengo in memoria le pfVociImporto variate per l'appalto in uso
            'par.cmd.CommandText = "select id_pf_voce,IMPORTO,PERCENTUALE from siscom_mi.appalti_variazioni_importi where id_variazione = " & idVariazione
            'Dim daPfVociImporti As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dtPfVoci As New Data.DataTable()
            'daPfVociImporti.Fill(dtPfVoci)
            'daPfVociImporti.Dispose()
            ''TROVO L'INDICE DELL'APPALTO IN CUI STO ESEGUENDO LA VARIAZIONE
            'For Each r As Data.DataRow In dtAppalti.Rows
            '    If r.Item("ID_APPALTO") = CType(Me.Page, Object).vIdAppalti Then
            '        Exit For
            '    End If
            '    rIApp += 1
            'Next
            dtInsert.Rows.RemoveAt(0)
            dtInsert.AcceptChanges()
            For Each rVoci As Data.DataRow In dtAppalti.Rows
                par.cmd.CommandText = "select id_appalto, id_pf_voce from SISCOM_MI.APPALTI_VOCI_PF where id_appalto in " _
                                    & "(select id from SISCOM_MI.appalti where id_gruppo=(select id_Gruppo from SISCOM_MI.appalti where id=" & CType(Me.Page, Object).vIdAppalti & ")) " _
                                    & "and id_pf_voce in ( " _
                                    & "select id from SISCOM_MI.pf_voci connect by prior id=id_old " _
                                    & "start with id= " & rVoci.Item("id_pf_voce") _
                                    & "union " _
                                    & "select id from SISCOM_MI.pf_voci connect by prior id_old=id " _
                                    & "start with id=" & rVoci.Item("id_pf_voce") & ") "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtInsert1 As New Data.DataTable()
                da.Fill(dtInsert1)
                da.Dispose()
                Dim idappaltoIns As Integer = 0
                If dtInsert1.Rows.Count > 0 Then
                    Dim IdAppVariazione As Integer = 0
                    For Each rInsert As Data.DataRow In dtInsert1.Rows
                        If rInsert.Item("id_pf_voce") <> rVoci.Item("id_pf_voce") Then
                            If idappaltoIns <> par.IfEmpty(rInsert.Item("id_appalto").ToString, 0) Then
                                idappaltoIns = rInsert.Item("id_appalto")

                                'par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
                                'reader = par.cmd.ExecuteReader
                                'If reader.Read Then
                                '    IdAppVariazione = reader(0)
                                'End If

                                'reader.Close()
                                'If String.IsNullOrEmpty(idSelected.Value) Then
                                '    'INSERT
                                '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO, id_tipo_variazione)" _
                                '                        & " VALUES(" & IdAppVariazione & ", " & idappaltoIns & ",'" & par.AggiustaData(Me.txtData.SelectedDate) & "'," & idTipo & ",'" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "', " & ddlTipoVariazione.SelectedValue & ")"
                                '    par.cmd.ExecuteNonQuery()
                                '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,id_pf_voce,IMPORTO,PERCENTUALE, id_gruppo_variazione) VALUES (" & IdAppVariazione _
                                '                & ", " & rInsert.Item("id_pf_voce") & "," & par.VirgoleInPunti(rVoci.Item("importo")) & "," & par.IfEmpty(par.VirgoleInPunti(rVoci.Item("percentuale")), "null") & "," & HFidGruppoVariazione.Value & ")"
                                '    par.cmd.ExecuteNonQuery()
                                'Else
                                '    'UPDATE
                                '    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_VARIAZIONI " _
                                '           & "SET    DATA_VARIAZIONE = " & par.AggiustaData(Me.txtData.SelectedDate) & ", " _
                                '           & "       ID_TIPOLOGIA    = " & idTipo & ", " _
                                '           & "       PROVVEDIMENTO   = '" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "' " _
                                '           & "WHERE  ID_APPALTO              = " & idappaltoIns
                                '    par.cmd.ExecuteNonQuery()
                                '    'par.cmd.CommandText = "SELECT nvl(ID,'-1') FROM siscom_mi.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & 
                                '    par.cmd.CommandText = "SELECT APPALTI_VARIAZIONI.ID " _
                                '                       & "FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
                                '                       & "WHERE ID_APPALTO = " & idappaltoIns & " " _
                                '                       & "AND APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE " _
                                '                       & "AND APPALTI_VARIAZIONI_IMPORTI.ID_GRUPPO_VARIAZIONE = " & idSelected.Value
                                '    Dim idApp As String = par.cmd.ExecuteScalar.ToString
                                '    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
                                '                        & "SET    id_pf_voce 		        = " & rInsert.Item("id_pf_voce") & ", " _
                                '                        & "       IMPORTO            		        = " & par.VirgoleInPunti(rVoci.Item("importo")) & ", " _
                                '                        & "       PERCENTUALE        		        = " & par.IfEmpty(par.VirgoleInPunti(rVoci.Item("percentuale")), "null") & " " _
                                '                        & "WHERE  ID_VARIAZIONE           	= " & par.IfEmpty(idApp, "-1")
                                '    par.cmd.ExecuteNonQuery()
                                'End If
                            End If

                        End If
                    Next
                End If
            Next
        End If
    End Sub


    Private Sub CaricaImporti()
        Try
            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            par.cmd.CommandText = "SELECT ID, getdata(DATA_VARIAZIONE) AS DATA_VARIAZIONE,PROVVEDIMENTO, " _
                                & "id_pf_voce, ID_TIPOLOGIA, ID_TIPO_VARIAZIONE," _
                                & "TRIM (TO_CHAR (PERCENTUALE, '9G999G999G999G999G990D99')) AS PERCENTUALE, " _
                                & "TRIM (TO_CHAR (IMPORTO, '9G999G999G999G999G990D99')) AS IMPORTO " _
                                & "FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
                                & "WHERE APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE " _
                                & "AND APPALTI_VARIAZIONI.ID = " & idSelected.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                Me.txtImportoVar.Text = par.IfEmpty(myReader("IMPORTO").ToString, "0")
                Me.txtData.SelectedDate = par.FormattaData(par.IfNull(myReader("DATA_VARIAZIONE"), ""))
                Me.txtNote.Text = par.IfEmpty(myReader("PROVVEDIMENTO").ToString, "")
                hfRestaVisible.Value = "1"
                ddlTipoVariazione.SelectedValue = par.IfEmpty(myReader("ID_TIPO_VARIAZIONE").ToString, "-1")
                'ddlVoceServizio.SelectedValue = par.IfEmpty(myReader("id_pf_voce").ToString, "")
                'Select Case myReader("ID_TIPOLOGIA")
                '    Case 5
                '        radioBtnCanone.Checked = True
                '        radioBtnConsumo.Checked = False
                '    Case 6
                '        radioBtnCanone.Checked = False
                '        radioBtnConsumo.Checked = True
                'End Select
            End If
            myReader.Close()
            Dim script As String = "function f(){$find(""" + RadWindowVarImporti.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_VaImporti"
            par.myTrans.Rollback()
        End Try
    End Sub
    Protected Sub DataGridVCan_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGridVCan.NeedDataSource
        Try
            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            Dim tipologia As Integer = 6

            par.cmd.CommandText = "SELECT ID, TO_CHAR (TO_DATE (DATA_VARIAZIONE, 'yyyymmdd'), 'dd/mm/yyyy') AS DATA_VARIAZIONE, " _
                                & "(SELECT pf_voci.descrizione FROM siscom_mi.pf_voci WHERE ID = id_pf_voce) AS DESC_VOCE, " _
                                & "(SELECT descrizione FROM SISCOM_MI.tipologia_variazioni WHERE id = SISCOM_MI.appalti_variazioni.id_tipologia) || ' '  " _
                                & "|| TRIM (TO_CHAR (IMPORTO, '9G999G999G999G999G990D99')) || ' € - ' || " _
                                & "(SELECT descrizione FROM SISCOM_MI.appalti_tipi_variazione WHERE id = SISCOM_MI.appalti_variazioni.id_tipo_variazione) AS tipo_variazione " _
                                & "FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
                                & "WHERE     APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE " _
                                & "AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_VaImporti"
        End Try
    End Sub
End Class

