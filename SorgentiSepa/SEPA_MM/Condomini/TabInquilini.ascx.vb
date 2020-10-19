
Partial Class Condomini_TabInquilini
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtMil_Pro.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
                txtAsc.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
                txtMil_Compro.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
                txtMil_Gest.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
                txt_Mil_Risc.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
                txtMillPres.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")

                txtAddebito.Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                'txtPosBil.Attributes.Add("onblur", "javascript:valid(this,'notnumbers');")
                'txtPosBil.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                '*******NUOVA GESTIONE INDIRIZZO INQUILINO 15/04/2010
                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO"
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'Me.cmbTipoCor.Items.Clear()
                'Do While myReader1.Read
                '    Me.cmbTipoCor.Items.Add(New ListItem(myReader1("DESCRIZIONE"), myReader1("COD")))
                'Loop
                'myReader1.Close()
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbTipoCor", "select * from siscom_mi.tipologia_indirizzo order by descrizione asc", "DESCRIZIONE", "COD")
                Cerca()
                If DirectCast(Me.Page.FindControl("ImgVisibility"), HiddenField).Value <> 1 Then
                    Me.btnVisualizza.Visible = False
                End If

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabInquilini"
        End Try
    End Sub
    Public Sub Cerca()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            If CType(Me.Page, Object).vIdCondominio() <> "" Then

                Dim sStringaSQL As String = "SELECT  UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO, RAPPORTI_UTENZA.DATA_DECORRENZA,  UNITA_IMMOBILIARI.ID AS ID_UI, " _
                                        & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TIPO_DISPONIBILITA.DESCRIZIONE AS OCCUPAZIONE,'' AS STATO_RAPP," _
                                        & " '' AS STATOVISUAL , POSIZIONE_BILANCIO,(SELECT COUNT(*) FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO = RAPPORTI_UTENZA.ID) AS NUM_COMP_NUCLEO, (SELECT COUNT(*) FROM SISCOM_MI.OSPITI WHERE ID_CONTRATTO = RAPPORTI_UTENZA.ID) AS NUM_OSPITI, TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO," _
                                        & " TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC,TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST, TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC,TO_CHAR(MILL_PRES_ASS,'9G999G990D9999') AS MILL_PRES_ASS,COND_UI.NOTE,UNITA_CONTRATTUALE.ID_CONTRATTO,(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN '' " _
                                        & " ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,1) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select,(CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='CHIUSO' THEN '' ELSE siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1)END )AS INTESTATARIO," _
                                        & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO, nvl(cod_fiscale,partita_iva) as cf_iva,INDIRIZZI.DESCRIZIONE,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA" _
                                        & " FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID" _
                                        & " AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) " _
                                        & " AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND  COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio=" & CType(Me.Page, Object).vIdCondominio() & ") " _
                                        & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL "

                If CType(Me.Page, Object).vSelezionati() = 1 Then
                    'VELOCIZZAZIONE 10/08/2010
                    'If CType(Me.Page, Object).Selezionati = True Then

                    'sStringaSQL = sStringaSQL & " AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & ") AND UNITA_CONTRATTUALE.ID_CONTRATTO = (SELECT MAX (ID_CONTRATTO) FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_UNITA= UNITA_IMMOBILIARI.ID)"
                    sStringaSQL = sStringaSQL & " AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & ") "

                Else
                    Exit Sub
                End If

                sStringaSQL = sStringaSQL & "UNION SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID AS ID_UI, " _
                                          & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE , TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TIPO_DISPONIBILITA.DESCRIZIONE AS OCCUPAZIONE,'' AS STATO_RAPP," _
                                          & "'' AS STATOVISUAL , '' AS POSIZIONE_BILANCIO,(SELECT COUNT(*) FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO = RAPPORTI_UTENZA.ID) AS NUM_COMP_NUCLEO, (SELECT COUNT(*) FROM SISCOM_MI.OSPITI WHERE ID_CONTRATTO = RAPPORTI_UTENZA.ID) AS NUM_OSPITI, '' AS MIL_PRO,''AS MIL_ASC, '' AS MIL_COMPRO,''AS MIL_GEST, '' AS MIL_RISC, " _
                                          & "'' AS MILL_PRES_ASS, '' AS NOTE,UNITA_CONTRATTUALE.ID_CONTRATTO,(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN '' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,1) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select," _
                                          & "(CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='CHIUSO' THEN '' ELSE siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1)END )AS INTESTATARIO,'' AS NOMINATIVO,'' as cf_iva," _
                                          & "INDIRIZZI.DESCRIZIONE,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA " _
                                          & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA " _
                                          & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA  AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                          & "AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+)  AND COD_TIPO_DISPONIBILITA <> 'VEND' AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                                          & "EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio() & ") AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE id_condominio = " & CType(Me.Page, Object).vIdCondominio() & ") AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                          & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL ORDER BY DESCRIZIONE ASC,INTERNO ASC,SCALA ASC, ID_UI ASC, DATA_DECORRENZA DESC"
                par.cmd.CommandText = sStringaSQL

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)

                DataGridInquilini.DataSource = FiltraContrattiVeri(dt)
                DataGridInquilini.DataBind()
                da.Dispose()
                dt.Dispose()
                DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 1
                CType(Me.Page.FindControl("TabMillesimalil1"), Object).CalcolaSOMMAMillesimi()

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabInquilini"
        End Try
    End Sub
    Private Function FiltraContrattiVeri(ByVal Table As Data.DataTable) As Data.DataTable
        FiltraContrattiVeri = Table.Clone()
        Dim idUi As Integer = 0
        Try
            Dim rSelect As Data.DataRow()

            For i As Integer = 0 To Table.Rows.Count - 1
                If par.IfNull(Table.Rows(i).Item("COD_CONTRATTO"), "") <> "" Then
                    If Table.Rows(i).Item("ID_UI") <> idUi Then
                        rSelect = Table.Select("ID_UI = " & Table.Rows(i).Item("ID_UI") & " AND STATO_DT_SELECT LIKE '%IN CORSO%'")
                        If rSelect.Length > 0 Then
                            FiltraContrattiVeri.Rows.Add(rSelect(0).ItemArray)
                            idUi = rSelect(0).Item("ID_UI")
                        Else
                            FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                            idUi = Table.Rows(i).Item("ID_UI")
                        End If
                    End If
                Else
                    FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                End If
            Next

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " FiltraContrattiVeri"
        End Try
        Return FiltraContrattiVeri
    End Function
    'Private Function FiltraContrattiVeri(ByVal Table As Data.DataTable) As Data.DataTable
    '    FiltraContrattiVeri = Table.Clone()

    '    Try

    '        For i As Integer = 0 To Table.Rows.Count - 1
    '            If i < Table.Rows.Count - 1 Then
    '                If par.IfNull(Table.Rows(i).Item("COD_CONTRATTO"), "") <> "" Then
    '                    If Table.Rows(i).Item("ID_UI") = Table.Rows(i + 1).Item("ID_UI") Then
    '                        FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
    '                        i = i + 1
    '                    Else
    '                        FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
    '                    End If
    '                Else

    '                    FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
    '                End If
    '            End If
    '        Next

    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " FiltraContrattiVeri"

    '    End Try


    '    Return FiltraContrattiVeri
    'End Function


    Public Property vIndirizzoCor() As String
        Get
            If Not (ViewState("par_vIndirizzoCor") Is Nothing) Then
                Return CStr(ViewState("par_vIndirizzoCor"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIndirizzoCor") = value
        End Set

    End Property
    Public Property vIdIndUnita() As String
        Get
            If Not (ViewState("par_vIdIndUnita") Is Nothing) Then
                Return CStr(ViewState("par_vIdIndUnita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdIndUnita") = value
        End Set

    End Property
    Public Property IndirizzoUnita() As String
        Get
            If Not (ViewState("par_IndirizzoUnita") Is Nothing) Then
                Return CStr(ViewState("par_IndirizzoUnita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndirizzoUnita") = value
        End Set

    End Property
    Protected Sub DataGridInquilini_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridInquilini.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TabInquilini1_txtmia').value='Hai selezionato: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtidInquilini').value='" & e.Item.Cells(0).Text & "';document.getElementById('TabInquilini1_cod_ui').value='" & e.Item.Cells(1).Text & "';document.getElementById('TabInquilini1_STATO').value='" & e.Item.Cells(3).Text & "';document.getElementById('TabInquilini1_TIPOLOGIA').value='" & e.Item.Cells(2).Text & "';document.getElementById('TabInquilini1_idContratto').value='" & e.Item.Cells(4).Text & "';document.getElementById('TabInquilini1_Id_Edificio').value='" & e.Item.Cells(5).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TabInquilini1_txtmia').value='Hai selezionato: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtidInquilini').value='" & e.Item.Cells(0).Text & "';document.getElementById('TabInquilini1_cod_ui').value='" & e.Item.Cells(1).Text & "';document.getElementById('TabInquilini1_STATO').value='" & e.Item.Cells(3).Text & "';document.getElementById('TabInquilini1_TIPOLOGIA').value='" & e.Item.Cells(2).Text & "';document.getElementById('TabInquilini1_idContratto').value='" & e.Item.Cells(4).Text & "';document.getElementById('TabInquilini1_Id_Edificio').value='" & e.Item.Cells(5).Text & "'")

        End If
    End Sub

    Protected Sub btnSalvaInquilini_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaInquilini.Click
        Dim scriptblock As String = ""

        Try
            If idContratto.Value <> "&nbsp;" And Trim(Me.txtStatoRapp.Text.ToUpper) <> "IN CORSO ABUSIVO" Then

                If Me.cmbIntestatari.SelectedValue <> "" Then

                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_UI SET POSIZIONE_BILANCIO= '" & par.PulisciStrSql(Me.txtPosBil.Text) & "' , MIL_PRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Pro.Text), "Null") & " " _
                    & " , MIL_ASC = " & par.IfEmpty(par.VirgoleInPunti(Me.txtAsc.Text), "Null") & ", MIL_COMPRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Compro.Text), "Null") & "" _
                    & " , MIL_GEST = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Gest.Text), "Null") & ", MIL_RISC = " & par.IfEmpty(par.VirgoleInPunti(Me.txt_Mil_Risc.Text), "Null") & "" _
                    & " , NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', ADDEBITO_SINGOLO = " & par.VirgoleInPunti(par.IfEmpty(Me.txtAddebito.Text, "Null")) & ", ID_INTESTARIO = " & par.IfEmpty(Me.cmbIntestatari.SelectedValue.ToString, "Null") & ", MILL_PRES_ASS = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMillPres.Text), "Null") & " WHERE COND_UI.ID_UI = " & DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value & " AND COND_UI.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()

                    par.cmd.ExecuteNonQuery()
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','MILLESIMI INQUILINO')"
                    par.cmd.ExecuteNonQuery()


                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    '************* CONTROLLO BLOCCO RAPPORTO UTENZA PER AGGIORNAMENTO LUOGO COR ***********
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************

                    If vIndirizzoCor <> (Trim(Me.cmbTipoCor.SelectedItem.Text & par.PulisciStrSql(Me.txtVia.Text) & par.PulisciStrSql(Me.txtCivico.Text) & Me.txtCap.Text & par.PulisciStrSql(Me.txtLocalità.Text)).ToUpper) Then
                        Try
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & idContratto.Value & " FOR UPDATE NOWAIT"
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                            End If
                            myReader1.Close()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET TIPO_COR='" & Me.cmbTipoCor.SelectedItem.Text & "', LUOGO_COR= '" & par.PulisciStrSql(Me.txtLocalità.Text) & "',VIA_COR='" & par.PulisciStrSql(Me.txtVia.Text) & "', CIVICO_COR='" & par.PulisciStrSql(Me.txtCivico.Text) & "', CAP_COR='" & Me.txtCap.Text & "' WHERE ID = " & idContratto.Value
                            par.cmd.ExecuteNonQuery()

                            '****************EVENTO PER RAPPORTI UTENZA!!!!!****************
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F59','')"
                            par.cmd.ExecuteNonQuery()

                        Catch EX1 As Oracle.DataAccess.Client.OracleException
                            If EX1.Number = 54 Then
                                'par.OracleConn.Close()
                                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('Il contratto associato, è aperto da un altro utente!Attendere...');" _
                                & "</script>"
                                DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 0

                                'ApriFrmWithDBLock()
                                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                                End If
                            Else
                                par.OracleConn.Close()
                                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                            End If

                        Catch ex As Exception
                            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabInquilini"
                        End Try

                    End If
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    '********************* END CONTROLLO **********************
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************

                    '-------------------------------------------------------------------------------------------------------------------------

                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    '************* CONTROLLO BLOCCO UNITA IMMOBILIARI PER AGGIORNAMENTO INDIRIZZO ***********
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    If IndirizzoUnita <> (Trim(par.PulisciStrSql(Me.txtIndirizzoUI.Text) & par.PulisciStrSql(Me.txtCivicoUI.Text) & Me.txtCapUI.Text & par.PulisciStrSql(Me.txtLocalitaUI.Text)).ToUpper) Then
                        Try
                            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & vIdIndUnita & " FOR UPDATE NOWAIT"
                            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'If myReader1.Read Then
                            'End If
                            'myReader1.Close()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.INDIRIZZI SET DESCRIZIONE='" & par.PulisciStrSql(Me.txtIndirizzoUI.Text.ToUpper) & "', CIVICO= '" & par.PulisciStrSql(Me.txtCivicoUI.Text) & "',CAP='" & par.PulisciStrSql(Me.txtCapUI.Text) & "', LOCALITA='" & par.PulisciStrSql(Me.txtLocalitaUI.Text.ToUpper) & "'  WHERE ID = " & vIdIndUnita
                            par.cmd.ExecuteNonQuery()

                            ''****************EVENTO PER RAPPORTI UTENZA!!!!!****************
                            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F59','')"
                            'par.cmd.ExecuteNonQuery()

                            'Catch EX1 As Oracle.DataAccess.Client.OracleException
                            '    If EX1.Number = 54 Then
                            '        'par.OracleConn.Close()
                            '        scriptblock = "<script language='javascript' type='text/javascript'>" _
                            '        & "alert('Unita aperta da un altro utente!Attendere...');" _
                            '        & "</script>"
                            '        DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 0

                            '        'ApriFrmWithDBLock()
                            '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                            '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                            '        End If
                            '    Else
                            '        par.OracleConn.Close()
                            '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                            '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                            '    End If

                        Catch ex As Exception
                            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabInquilini"
                        End Try

                    End If
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    '********************* END CONTROLLO **********************
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************




                    Cerca()


                    'AGGIORNO I VALORI NELLE TABELLE SCALE E FABBRICATI DEL TAB MILLESIMALI
                    CType(Me.Page.FindControl("TabMillesimalil1"), Object).CercaScale()

                    DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 1
                    DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value = ""
                Else
                    DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 2

                    'MESSAGGIO OBBLIGATORIO INTESTATARIO
                    Response.Write("<script>alert('E\' obbligatorio scegliere un intestatario principale!');</script>")

                End If


            Else
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_UI SET POSIZIONE_BILANCIO= '" & par.PulisciStrSql(Me.txtPosBil.Text) & "' , MIL_PRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Pro.Text), "Null") & " " _
                & " , MIL_ASC = " & par.IfEmpty(par.VirgoleInPunti(Me.txtAsc.Text), "Null") & ", MIL_COMPRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Compro.Text), "Null") & "" _
                & " , MIL_GEST = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Gest.Text), "Null") & ", MIL_RISC = " & par.IfEmpty(par.VirgoleInPunti(Me.txt_Mil_Risc.Text), "Null") & "" _
                & " , NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', ADDEBITO_SINGOLO = " & par.VirgoleInPunti(par.IfEmpty(Me.txtAddebito.Text, "Null")) & ", ID_INTESTARIO = " & par.IfEmpty(Me.cmbIntestatari.SelectedValue.ToString, "Null") & ", MILL_PRES_ASS = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMillPres.Text), "Null") & " WHERE COND_UI.ID_UI = " & DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value & " AND COND_UI.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()

                par.cmd.ExecuteNonQuery()

                '**********************************************************
                '**********************************************************
                '**********************************************************
                '************* CONTROLLO BLOCCO UNITA IMMOBILIARI PER AGGIORNAMENTO INDIRIZZO ***********
                '**********************************************************
                '**********************************************************
                '**********************************************************
                'If vIndirizzoCor <> (Trim(Me.cmbTipoCor.SelectedItem.Text & par.PulisciStrSql(Me.txtVia.Text) & par.PulisciStrSql(Me.txtCivico.Text) & Me.txtCap.Text & par.PulisciStrSql(Me.txtLocalità.Text)).ToUpper) Then
                If vIndirizzoCor <> (Trim(Me.cmbTipoCor.SelectedItem.Text & par.PulisciStrSql(Me.txtVia.Text) & par.PulisciStrSql(Me.txtCivico.Text) & Me.txtCap.Text & par.PulisciStrSql(Me.txtLocalità.Text)).ToUpper) Then
                    Try
                        'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & vIdIndUnita & " FOR UPDATE NOWAIT"
                        'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        'If myReader1.Read Then
                        'End If
                        'myReader1.Close()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.INDIRIZZI SET DESCRIZIONE='" & par.PulisciStrSql(Me.txtIndirizzoUI.Text.ToUpper) & "', CIVICO= '" & par.PulisciStrSql(Me.txtCivicoUI.Text) & "',CAP='" & par.PulisciStrSql(Me.txtCapUI.Text) & "', LOCALITA='" & par.PulisciStrSql(Me.txtLocalitaUI.Text.ToUpper) & "'  WHERE ID = " & vIdIndUnita
                        par.cmd.ExecuteNonQuery()

                        ''****************EVENTO PER RAPPORTI UTENZA!!!!!****************
                        'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F59','')"
                        'par.cmd.ExecuteNonQuery()

                        'Catch EX1 As Oracle.DataAccess.Client.OracleException
                        '    If EX1.Number = 54 Then
                        '        'par.OracleConn.Close()
                        '        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        '        & "alert('Unita aperta da un altro utente!Attendere...');" _
                        '        & "</script>"
                        '        DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 0

                        '        'ApriFrmWithDBLock()
                        '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                        '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                        '        End If
                        '    Else
                        '        par.OracleConn.Close()
                        '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                        '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                        '    End If

                    Catch ex As Exception
                        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabInquilini"
                    End Try

                End If
                '**********************************************************
                '**********************************************************
                '**********************************************************
                '********************* END CONTROLLO **********************
                '**********************************************************
                '**********************************************************
                '**********************************************************


                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','MILLESIMI INQUILINO CONDOMINIO')"
                par.cmd.ExecuteNonQuery()

                Cerca()
                'AGGIORNO I VALORI NELLE TABELLE SCALE E FABBRICATI DEL TAB MILLESIMALI
                CType(Me.Page.FindControl("TabMillesimalil1"), Object).CercaScale()

                DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 1
                DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value = ""
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabInquilini"
        End Try


    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        Dim scriptblock As String = ""
        Try
            If DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value <> "0" And DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value <> "" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Me.txtPosBil.Text = ""
                Me.txtMil_Pro.Text = ""
                Me.txtAsc.Text = ""
                Me.txtMil_Compro.Text = ""
                Me.txtMil_Gest.Text = ""
                Me.txt_Mil_Risc.Text = ""
                Me.txtNote.Text = ""
                Me.txtStatoRapp.Text = ""
                Me.TxtTipologia.Text = ""
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                par.cmd.CommandText = "SELECT CONDOMINI.DENOMINAZIONE FROM SISCOM_MI.CONDOMINI WHERE ID = " & CType(Me.Page, Object).vIdCondominio()
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    Me.lblTitolo.Text = "Condominio: " & myReader1("DENOMINAZIONE")
                End If
                myReader1.Close()
                If idContratto.Value <> "&nbsp;" Then
                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & idContratto.Value & " FOR UPDATE NOWAIT"
                    'myReader1 = par.cmd.ExecuteReader()
                    'If myReader1.Read Then

                    'End If
                    'myReader1.Close()
                    'Me.cmbTipoCor.ClearSelection()
                    Me.cmbTipoCor.SelectedIndex = -1

                    '☺ query che preleva anche lo stato del contratto
                    par.cmd.CommandText = "SELECT PRESSO_COR,TIPO_COR,VIA_COR,CIVICO_COR,LUOGO_COR,CAP_COR,(CASE WHEN ID IS NULL THEN 'LIBERO'ELSE  SISCOM_MI.Getstatocontratto2(ID,0) END) AS STATO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID =" & idContratto.Value
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        vIndirizzoCor = (Trim(par.IfNull(myReader1("TIPO_COR"), "") & par.PulisciStrSql(par.IfNull(myReader1("VIA_COR").ToString, "")) & par.PulisciStrSql(par.IfNull(myReader1("CIVICO_COR"), "")) & myReader1("CAP_COR") & par.PulisciStrSql(par.IfNull(myReader1("LUOGO_COR"), ""))).ToUpper)
                        Me.cmbTipoCor.Items.FindByText(par.IfNull(myReader1("TIPO_COR"), "VIA")).Selected = True
                        Me.txtVia.Text = par.IfNull(myReader1("VIA_COR"), "")
                        Me.txtCivico.Text = par.IfNull(myReader1("CIVICO_COR"), "")
                        Me.txtCap.Text = par.IfNull(myReader1("CAP_COR"), "")
                        Me.txtLocalità.Text = par.IfNull(myReader1("LUOGO_COR"), "")
                        Me.txtPresso.Text = par.IfNull(myReader1("PRESSO_COR"), "")
                        Me.txtStatoRapp.Text = par.IfNull(myReader1("STATO"), "")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND  nvl(DATA_FINE,'29991231')>= to_char(to_date(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') AND ID_CONTRATTO=" & idContratto.Value
                    myReader1 = par.cmd.ExecuteReader()
                    Me.cmbIntestatari.Items.Clear()
                    cmbIntestatari.Items.Add(New ListItem(" ", ""))
                    Dim i As Integer = 0
                    Do While myReader1.Read
                        If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") <> "" Then
                            Me.cmbIntestatari.Items.Add(New ListItem(myReader1("RAGIONE_SOCIALE"), myReader1("ID")))
                        Else
                            Me.cmbIntestatari.Items.Add(New ListItem(myReader1("COGNOME") & " " & myReader1("NOME"), myReader1("ID")))
                        End If
                        i = i + 1
                    Loop
                    myReader1.Close()
                    Me.txtnumPers.Text = i
                    'Se singolo intestatario seleziono direttamente quello
                    If Me.cmbIntestatari.Items.Count = 2 Then
                        Me.cmbIntestatari.SelectedIndex = 1
                        'Else
                        '    Me.cmbIntestatari.SelectedValue = ""
                    End If
                Else
                    Me.cmbIntestatari.Items.Clear()
                    cmbIntestatari.Items.Add(New ListItem(" ", ""))

                    Me.txtVia.Text = ""
                    Me.txtCivico.Text = ""
                    Me.txtCap.Text = ""
                    Me.txtLocalità.Text = ""
                    Me.txtPresso.Text = ""

                End If
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.cond_ui WHERE ID_UI = " & DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then

                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT COND_UI.*, INDIRIZZI.ID AS ID_IND_UNITA,UNITA_IMMOBILIARI.INTERNO, SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,TIPO_DISPONIBILITA.DESCRIZIONE AS STATOOCC, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.INDIRIZZI , SISCOM_MI.TIPO_DISPONIBILITA WHERE ID_UI = " & DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value & " AND ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio() & " AND UNITA_IMMOBILIARI.ID = COND_UI.ID_UI AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA =TIPO_DISPONIBILITA.COD"
                Me.txtCodUI.Text = cod_ui.Value
                Me.TxtTipologia.Text = TIPOLOGIA.Value
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtPosBil.Text = myReader1("POSIZIONE_BILANCIO").ToString
                    Me.txtMil_Pro.Text = IsNumFormat(myReader1("MIL_PRO"), "", "0.0000")
                    Me.txtAsc.Text = IsNumFormat(myReader1("MIL_ASC"), "", "0.0000")
                    Me.txtMillPres.Text = IsNumFormat(myReader1("MILL_PRES_ASS"), "", "0.0000")

                    Me.txtMil_Compro.Text = IsNumFormat(myReader1("MIL_COMPRO"), "", "0.0000")
                    Me.txtMil_Gest.Text = IsNumFormat(myReader1("MIL_GEST"), "", "0.0000")
                    Me.txt_Mil_Risc.Text = IsNumFormat(myReader1("MIL_RISC"), "", "0.0000")
                    Me.txtAddebito.Text = IsNumFormat(myReader1("ADDEBITO_SINGOLO"), "", "0.00")
                    Me.txtNote.Text = myReader1("NOTE").ToString

                    'MODIFICA PER DIVIDERE L'INDIRIZZO DELL'UNITA E PERMETTERNE L'AGGIORNAMENTO
                    IndirizzoUnita = (Trim(par.IfNull(myReader1("INDIRIZZO"), "") & myReader1("CIVICO").ToString & myReader1("CAP") & myReader1("LOCALITA")).ToUpper)
                    Me.txtIndirizzoUI.Text = myReader1("INDIRIZZO").ToString
                    Me.txtCivicoUI.Text = myReader1("CIVICO").ToString
                    Me.txtCapUI.Text = myReader1("CAP").ToString
                    Me.txtLocalitaUI.Text = myReader1("LOCALITA").ToString

                    vIdIndUnita = myReader1("ID_IND_UNITA").ToString
                    Me.txtInterno.Text = myReader1("INTERNO").ToString
                    Me.txtScala.Text = myReader1("DESCRIZIONE").ToString
                    Me.txtPiano.Text = myReader1("PIANO").ToString
                    Me.txtStOccupazione.Text = myReader1("STATOOCC").ToString
                    Me.cmbIntestatari.ClearSelection()
                    If Me.cmbIntestatari.Items.Count = 2 And myReader1("ID_INTESTARIO").ToString = "" Then
                        Me.cmbIntestatari.SelectedIndex = 1
                    Else
                        Dim item = Me.cmbIntestatari.Items.FindByValue(myReader1("ID_INTESTARIO").ToString)
                        If Not IsNothing(item) Then
                            item.Selected = True
                        Else
                            Me.cmbIntestatari.SelectedIndex = 0
                        End If
                        'Me.cmbIntestatari.SelectedValue = myReader1("ID_INTESTARIO").ToString
                    End If
                    myReader1.Close()

                Else
                    myReader1.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI (ID_CONDOMINIO, ID_UI, ID_EDIFICIO) VALUES (" & CType(Me.Page, Object).vIdCondominio() & ", " & DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value & "," & Me.Id_Edificio.Value & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT COND_UI.*,INDIRIZZI.ID AS ID_IND_UNITA, UNITA_IMMOBILIARI.INTERNO, SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,TIPO_DISPONIBILITA.DESCRIZIONE AS STATOOCC, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.INDIRIZZI , SISCOM_MI.TIPO_DISPONIBILITA WHERE ID_UI = " & DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value & " AND ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio() & " AND UNITA_IMMOBILIARI.ID = COND_UI.ID_UI AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA =TIPO_DISPONIBILITA.COD"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        Me.txtPosBil.Text = myReader1("POSIZIONE_BILANCIO").ToString
                        Me.txtMil_Pro.Text = myReader1("MIL_PRO").ToString
                        Me.txtAsc.Text = myReader1("MIL_ASC").ToString
                        Me.txtMil_Compro.Text = myReader1("MIL_COMPRO").ToString
                        Me.txtMil_Gest.Text = myReader1("MIL_GEST").ToString
                        Me.txt_Mil_Risc.Text = myReader1("MIL_RISC").ToString
                        Me.txtNote.Text = myReader1("NOTE").ToString

                        'MODIFICA PER DIVIDERE L'INDIRIZZO DELL'UNITA E PERMETTERNE L'AGGIORNAMENTO
                        IndirizzoUnita = (Trim(par.IfNull(myReader1("INDIRIZZO"), "") & myReader1("CIVICO").ToString & myReader1("CAP") & myReader1("LOCALITA")).ToUpper)
                        Me.txtIndirizzoUI.Text = myReader1("INDIRIZZO").ToString
                        Me.txtCivicoUI.Text = myReader1("CIVICO").ToString
                        Me.txtCapUI.Text = myReader1("CAP").ToString
                        Me.txtLocalitaUI.Text = myReader1("LOCALITA").ToString
                        vIdIndUnita = myReader1("ID_IND_UNITA").ToString

                        Me.txtInterno.Text = myReader1("INTERNO").ToString
                        Me.txtScala.Text = myReader1("DESCRIZIONE").ToString
                        Me.txtPiano.Text = myReader1("PIANO").ToString
                        Me.txtStOccupazione.Text = myReader1("STATOOCC").ToString
                        'Me.cmbIntestatari.SelectedValue = myReader1("ID_INTESTARIO").ToString
                        Me.txtAddebito.Text = myReader1("ADDEBITO_SINGOLO").ToString

                    End If
                    myReader1.Close()

                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F33','MILLESIMI INQUILINO')"
                    par.cmd.ExecuteNonQuery()

                End If

                '*****lo stato del contratto viene rilevato prima, inutile query, e accorpata nella query precedente contrassegnata con ☺
                'par.cmd.CommandText = "SELECT UNITA_CONTRATTUALE.ID_UNITA,(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE = '" & cod_ui.Value & "'"
                'myReader1 = par.cmd.ExecuteReader
                'If myReader1.Read Then
                '    Me.txtStatoRapp.Text = myReader1("STATO")
                '    myReader1.Close()
                'Else
                '    Me.txtStatoRapp.Text = ""
                'End If


                par.cmd.CommandText = "SELECT CONDOMINI.ID, CONDOMINI. DENOMINAZIONE FROM SISCOM_MI.CONDOMINI ,SISCOM_MI.COND_EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_EDIFICIO = COND_EDIFICI.ID_EDIFICIO  AND (CONDOMINI.ID = COND_EDIFICI.ID_CONDOMINIO)AND UNITA_IMMOBILIARI.ID = " & DirectCast(Me.Page.FindControl("txtidInquilini"), HiddenField).Value & " AND CONDOMINI.ID<> " & CType(Me.Page, Object).vIdCondominio & " ORDER BY DENOMINAZIONE ASC "
                myReader1 = par.cmd.ExecuteReader
                Me.ListCondomini.Items.Clear()
                While myReader1.Read
                    Me.ListCondomini.Items.Add(New ListItem(myReader1("DENOMINAZIONE"), myReader1("ID")))
                End While
                DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 2
                myReader1.Close()

            Else
                Response.Write("<script>alert('Scegliere un elemento dalla lista!');</script>")

            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Il contratto associato, è aperto da un altro utente!Attendere...');" _
                & "</script>"
                DirectCast(Me.Page.FindControl("TextBox4"), HiddenField).Value = 0

                'ApriFrmWithDBLock()
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabInquilini"
        End Try
    End Sub

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    Protected Sub DataGridInquilini_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridInquilini.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGridInquilini.CurrentPageIndex = e.NewPageIndex
            Cerca()

        End If

    End Sub
End Class
