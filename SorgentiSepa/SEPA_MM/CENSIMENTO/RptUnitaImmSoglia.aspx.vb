
Partial Class CENSIMENTO_RptUnitaImmSoglia
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim Str As String
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:250px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"
            Response.Write(Str)
            Response.Flush()
            If Not IsPostBack Then
                RiempiCampi()
                AggiungiJavascript()

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub RiempiCampi()
        Try
            par.caricaComboBox("SELECT ID, COD_COMPLESSO || ' - ' || DENOMINAZIONE AS DESCRIZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID <> 1 ORDER BY DENOMINAZIONE", ddlcomplesso, "ID", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DESCRIZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE", ddledificio, "ID", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME", ddlstruttura, "ID", "NOME", True)
            par.caricaComboBox("SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1) order by descrizione asc", ddlindirizzo, "DESCRIZIONE", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT ID, NOME FROM SISCOM_MI.TAB_QUARTIERI ORDER BY NOME", ddlquartieri, "ID", "NOME", True)
            caricaCheckBoxList("SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY DESCRIZIONE", cbltipologia, "COD", "DESCRIZIONE")
            For Each Items As ListItem In cbltipologia.Items
                If UCase(Items.Text) = "ALLOGGIO" = True Then
                    ' StringaCheck = StringaCheck & Items.Value & ","
                    Items.Selected = True
                End If
            Next

            caricaCheckBoxList("SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_DISPONIBILITA ORDER BY DESCRIZIONE", cbldisponibilita, "COD", "DESCRIZIONE")
            caricaCheckBoxList("SELECT ID, DESCRIZIONE FROM SISCOM_MI.DESTINAZIONI_USO_UI ORDER BY DESCRIZIONE", cbldestuso, "ID", "DESCRIZIONE")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub AggiungiJavascript()
        Try
            'txtdataaperti.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'txtdatabozza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'txtdatachiusi.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Public Sub caricaCheckBoxList(ByVal query As String, ByVal checkboxlist As CheckBoxList, ByVal value As String, ByVal descrizione As String)
        Try
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(query, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            checkboxlist.DataSource = dt
            checkboxlist.DataValueField = value
            checkboxlist.DataTextField = descrizione
            checkboxlist.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Public Sub SelezionaCheck(ByVal checkboxlist As CheckBoxList, ByVal hfselect As Integer)
        For Each item As ListItem In checkboxlist.Items
            If hfselect = 0 Then
                item.Selected = True
            Else
                item.Selected = False
            End If
        Next
    End Sub
    Protected Sub btntipologia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btntipologia.Click
        SelezionaCheck(cbltipologia, selecttipologia.Value)
        If selecttipologia.Value = 0 Then
            selecttipologia.Value = 1
        Else
            selecttipologia.Value = 0
        End If
    End Sub
    Protected Sub btndisponibilita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndisponibilita.Click
        SelezionaCheck(cbldisponibilita, selectdisponibilita.Value)
        If selectdisponibilita.Value = 0 Then
            selectdisponibilita.Value = 1
        Else
            selectdisponibilita.Value = 0
        End If
    End Sub
    Protected Sub btndestuso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndestuso.Click
        SelezionaCheck(cbldestuso, selectdestuso.Value)
        If selectdestuso.Value = 0 Then
            selectdestuso.Value = 1
        Else
            selectdestuso.Value = 0
        End If
    End Sub
    Protected Sub ddlindirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlindirizzo.SelectedIndexChanged
        IndirizzoIndexChange()
    End Sub
    Private Sub IndirizzoIndexChange()
        Try
            ddlcivico.Items.Clear()
            ddlscala.Items.Clear()
            ddlinterno.Items.Clear()
            Dim CondEdifici As String = ""
            If Me.ddledificio.SelectedValue <> "-1" Then
                CondEdifici = " AND ID_EDIFICIO =" & Me.ddledificio.SelectedValue
                par.caricaComboBox("SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where ID IN " & ddlindirizzo.SelectedValue & " order by civico asc", ddlcivico, "CIVICO", "CIVICO", True)
            ElseIf Me.ddlcomplesso.SelectedValue <> "-1" Then
                CondEdifici = ""
                par.caricaComboBox("SELECT DISTINCT CIVICO FROM SISCOM_MI.INDIRIZZI WHERE ID IN (SELECT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & ddlcomplesso.SelectedValue & ") ORDER BY CIVICO ASC", ddlcivico, "CIVICO", "CIVICO", True)
            Else
                par.caricaComboBox("SELECT DISTINCT CIVICO FROM SISCOM_MI.INDIRIZZI WHERE DESCRIZIONE IN '" & par.PulisciStrSql(ddlindirizzo.SelectedItem.ToString) & "' ORDER BY CIVICO ASC", ddlcivico, "CIVICO", "CIVICO", True)
            End If
            If ddlcivico.SelectedValue <> "-1" Then
                par.caricaComboBox("SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.ddlcivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') " & CondEdifici & ") order by descrizione asc", ddlscala, "ID", "DESCRIZIONE", True)
            Else
                par.caricaComboBox("SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') " & CondEdifici & ") order by descrizione asc", ddlscala, "ID", "DESCRIZIONE", True)
            End If
            If Me.ddlcivico.SelectedValue <> "-1" Then
                par.caricaComboBox("SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.ddlcivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') order by interno asc", ddlinterno, "INTERNO", "INTERNO", True)
            Else
                par.caricaComboBox("SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') order by interno asc", ddlinterno, "INTERNO", "INTERNO", True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Protected Sub ddlcivico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcivico.SelectedIndexChanged
        Try
            ddlscala.Items.Clear()
            ddlinterno.Items.Clear()
            Dim CondEdifici As String = ""
            If Me.ddledificio.SelectedValue <> -1 Then
                CondEdifici = " AND ID_EDIFICIO =" & Me.ddledificio.SelectedValue
            Else
                CondEdifici = ""
            End If
            If ddlcivico.SelectedValue <> "-1" Then
                par.caricaComboBox("SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.ddlcivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "')" & CondEdifici & ") order by descrizione asc", ddlscala, "ID", "DESCRIZIONE", True)
            Else
                par.caricaComboBox("SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "')" & CondEdifici & ") order by descrizione asc", ddlscala, "ID", "DESCRIZIONE", True)
            End If
            If Me.ddlcivico.SelectedValue <> "-1" Then
                par.caricaComboBox("SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & par.PulisciStrSql(Me.ddlcivico.SelectedValue.ToString) & "' and descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') order by interno asc", ddlinterno, "INTERNO", "INTERNO", True)
            Else
                par.caricaComboBox("SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') order by interno asc", ddlinterno, "INTERNO", "INTERNO", True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Protected Sub ddlscala_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlscala.SelectedIndexChanged
        Try
            ddlinterno.Items.Clear()
            Dim CondEdifici As String = ""
            If Me.ddledificio.SelectedValue <> -1 Then
                CondEdifici = " AND ID_EDIFICIO =" & Me.ddledificio.SelectedValue
            Else
                CondEdifici = ""
            End If
            If Me.ddlcivico.SelectedValue <> "-1" Then
                par.caricaComboBox("SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.ddlcivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') order by interno asc", ddlinterno, "INTERNO", "INTERNO", True)
            Else
                par.caricaComboBox("SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where descrizione='" & par.PulisciStrSql(ddlindirizzo.SelectedItem.Text) & "') order by interno asc", ddlinterno, "INTERNO", "INTERNO", True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Protected Sub ddlcomplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcomplesso.SelectedIndexChanged
        If Me.ddlcomplesso.SelectedValue <> "-1" Then
            ddlcivico.Items.Clear()
            ddlscala.Items.Clear()
            ddlinterno.Items.Clear()
            Me.ddledificio.Items.Clear()
            par.caricaComboBox("SELECT ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DESCRIZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 AND ID_COMPLESSO = " & ddlcomplesso.SelectedValue & " ORDER BY DENOMINAZIONE", ddledificio, "ID", "DESCRIZIONE", True)
            Me.ddlstruttura.Items.Clear()
            CaricaStruttureCompl()
            FiltraIndirizzo()
            IndirizzoIndexChange()
        Else
            Me.ddledificio.Items.Clear()
            ddlindirizzo.Items.Clear()
            par.caricaComboBox("SELECT ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DESCRIZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 ORDER BY DENOMINAZIONE", ddledificio, "ID", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1) order by descrizione asc", ddlindirizzo, "DESCRIZIONE", "DESCRIZIONE", True)
            ddlstruttura.Items.Clear()
            par.caricaComboBox("SELECT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME", ddlstruttura, "ID", "NOME", True)
            ddlcivico.Items.Clear()
            ddlscala.Items.Clear()
            ddlinterno.Items.Clear()
        End If
    End Sub
    Protected Sub ddledificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddledificio.SelectedIndexChanged
        If Me.ddledificio.Text <> "-1" Then
            ddlcivico.Items.Clear()
            ddlscala.Items.Clear()
            ddlinterno.Items.Clear()
            SelezionaComplesso()
            Dim EdificioSelezionato As Integer = ddledificio.SelectedValue
            Me.ddledificio.Items.Clear()
            par.caricaComboBox("SELECT ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DESCRIZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 AND ID_COMPLESSO = " & ddlcomplesso.SelectedValue & " ORDER BY DENOMINAZIONE", ddledificio, "ID", "DESCRIZIONE", False)
            ddledificio.SelectedValue = EdificioSelezionato
            FiltraIndirizzo()
            IndirizzoIndexChange()
            Me.ddlstruttura.Items.Clear()
            CaricaStruttureCompl()
        Else
            ddlcivico.Items.Clear()
            ddlscala.Items.Clear()
            ddlinterno.Items.Clear()
            Me.ddledificio.Items.Clear()
            par.caricaComboBox("SELECT ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DESCRIZIONE FROM SISCOM_MI.EDIFICI WHERE ID <> 1 AND ID_COMPLESSO = " & ddlcomplesso.SelectedValue & " ORDER BY DENOMINAZIONE", ddledificio, "ID", "DESCRIZIONE", True)
            Me.ddlstruttura.Items.Clear()
            CaricaStruttureCompl()
            FiltraIndirizzo()
            IndirizzoIndexChange()
        End If
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        ControlloContratti()
        AvviaRicerca()
    End Sub
    Private Sub ControlloContratti()
        Try
            'If Cbchiusi.Checked = True And Len(txtdatachiusi.Text) <> 10 Then
            '    Controllo.Value = 1
            '    Exit Sub
            'End If
            'If cbaperti.Checked = True And Len(txtdataaperti.Text) <> 10 Then
            '    Controllo.Value = 2
            '    Exit Sub
            'End If
            'If cbbozza.Checked = True And Len(txtdatabozza.Text) <> 10 Then
            '    Controllo.Value = 3
            '    Exit Sub
            'End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub AvviaRicerca()
        Try
            If Controllo.Value = 0 Then
                Dim SoloFiltro As String = 0
                Dim RequestQueryString As String = ""
                If Me.cbsolofiltrati.Checked = True Then
                    SoloFiltro = 1
                End If
                RequestQueryString = RequestQueryString & "C=" & ddlcomplesso.SelectedValue & "&E=" & ddledificio.SelectedValue
                RequestQueryString = RequestQueryString & "&I=" & par.VaroleDaPassare(ddlindirizzo.SelectedItem.ToString) & "&Ci=" & par.VaroleDaPassare(ddlcivico.SelectedValue) & "&S=" & ddlscala.SelectedValue & "&In=" & ddlinterno.SelectedValue
                RequestQueryString = RequestQueryString & "&A=" & Me.cmbAscensore.SelectedValue & "&Co=" & Me.cmbcondominio.SelectedValue & "&St=" & ddlstruttura.SelectedValue & "&Q=" & ddlquartieri.SelectedValue
                RequestQueryString = RequestQueryString & "&T=" & SceltaCheck(cbltipologia) & "&Di=" & SceltaCheck(cbldisponibilita) & "&De=" & SceltaCheck(cbldestuso)
                RequestQueryString = RequestQueryString & "&CC=&CA=&CB="
                RequestQueryString = RequestQueryString & "&SCC=&SCA=&ONFILT=" & SoloFiltro
                RequestQueryString = RequestQueryString & "&SOGLIA=" & cmbSottosoglia.SelectedValue
                Response.Write("<script>window.open('RisultatiRptUnitaImmSoglia.aspx?" & RequestQueryString & "','ReportUnitaImmSoglia','scrollbars=yes,resizable=yes');</script>")
            Else
                If Controllo.Value = 1 Then
                    Response.Write("<script>alert('Inserire o correggere la data per i contratti chiusi!!');</script>")
                ElseIf Controllo.Value = 2 Then
                    Response.Write("<script>alert('Inserire o correggere la data per i contratti aperti!!');</script>")
                ElseIf Controllo.Value = 3 Then
                    Response.Write("<script>alert('Inserire o correggere la data per i contratti in bozza!!');</script>")
                End If
                Controllo.Value = 0
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Function chkZeroUno(ByVal chk As CheckBox) As Integer
        chkZeroUno = 0
        If chk.Checked = True Then
            chkZeroUno = 1
        End If
        Return chkZeroUno
    End Function
    Private Function SceltaCheck(ByRef checkboxlist As CheckBoxList) As String
        Dim StringaCheck As String = ""
        For Each Items As ListItem In checkboxlist.Items
            If Items.Selected = True Then
                StringaCheck = StringaCheck & Items.Value & ","
            End If
        Next
        If StringaCheck <> "" Then
            StringaCheck = Left(StringaCheck, Len(StringaCheck) - 1)
        End If
        Return StringaCheck
    End Function
    Private Sub CaricaStruttureCompl()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            par.cmd.CommandText = "SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & ddlcomplesso.SelectedValue
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.caricaComboBox("SELECT ID, NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & par.IfNull(myReader("ID_FILIALE"), 0) & " ORDER BY NOME", ddlstruttura, "ID", "NOME", False)
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub FiltraIndirizzo()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            If Me.ddledificio.SelectedValue <> "-1" Then
                par.cmd.CommandText = "SELECT ID_INDIRIZZO_PRINCIPALE as id_indirizzo_riferimento FROM SISCOM_MI.edifici WHERE ID = " & ddledificio.SelectedValue
            ElseIf Me.ddlcomplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = "SELECT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & ddlcomplesso.SelectedValue
            End If
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                ddlindirizzo.Items.Clear()
                par.caricaComboBox("SELECT distinct id, descrizione FROM SISCOM_MI.indirizzi WHERE ID = " & par.IfNull(myReader("ID_INDIRIZZO_RIFERIMENTO"), 0) & " order by descrizione asc", ddlindirizzo, "ID", "DESCRIZIONE", False)
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub SelezionaComplesso()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            par.cmd.CommandText = "SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID = " & ddledificio.SelectedValue
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                ddlcomplesso.SelectedValue = par.IfNull(myReader("ID_COMPLESSO"), "-1")
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    'Protected Sub Cbchiusi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cbchiusi.CheckedChanged
    '    If Cbchiusi.Checked = True Then
    '        txtdatachiusi.Visible = True
    '        Me.txtdatachiusi.Text = Format(Now, "dd/MM/yyyy")
    '        Me.cblstatochiusi.Visible = True
    '    Else
    '        txtdatachiusi.Visible = False
    '        txtdatachiusi.Text = ""
    '        Me.cblstatochiusi.Visible = False
    '        For Each i As ListItem In cblstatochiusi.Items
    '            i.Selected = False
    '        Next
    '    End If
    'End Sub
    'Protected Sub cbaperti_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbaperti.CheckedChanged
    '    If cbaperti.Checked = True Then
    '        txtdataaperti.Visible = True
    '        Me.txtdataaperti.Text = Format(Now, "dd/MM/yyyy")
    '        Me.cblstatoaperti.Visible = True
    '    Else
    '        txtdataaperti.Visible = False
    '        txtdataaperti.Text = ""
    '        Me.cblstatoaperti.Visible = False
    '        For Each i As ListItem In cblstatoaperti.Items
    '            i.Selected = False
    '        Next
    '    End If
    'End Sub
    'Protected Sub cbbozza_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbbozza.CheckedChanged
    '    If cbbozza.Checked = True Then
    '        txtdatabozza.Visible = True
    '        Me.txtdatabozza.Text = Format(Now, "dd/MM/yyyy")

    '    Else
    '        txtdatabozza.Visible = False
    '        txtdatabozza.Text = ""
    '    End If
    'End Sub

End Class
