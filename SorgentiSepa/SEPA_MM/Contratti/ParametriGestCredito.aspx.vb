
Partial Class Contratti_ParametriGestCredito
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        txtInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        If Not IsPostBack Then
            CaricaCombo()
            BindGrid()
            BindGrid1()
        End If
    End Sub

    Private Function Caricacombo()
        Try
            Dim PianoF As Long = par.RicavaPianoUltimoApprovato
            par.caricaComboBox("select id,codice||'-'||descrizione as DESCRIZIONE from siscom_mi.pf_voci where id_piano_finanziario=" & PianoF & " and connect_by_isleaf=1 connect by prior id=id_voce_madre start with id_voce_madre is null", cmbVoceBP0, "ID", "DESCRIZIONE", False)
            par.caricaComboBox("select id,codice||'-'||descrizione as DESCRIZIONE from siscom_mi.pf_voci where id_piano_finanziario=" & PianoF & " and connect_by_isleaf=1 connect by prior id=id_voce_madre start with id_voce_madre is null", cmbVoceBP1, "ID", "DESCRIZIONE", True, "-1")
            par.caricaComboBox("SELECT id,codice||'-'||descrizione as DESCRIZIONE FROM siscom_mi.PF_VOCI WHERE ID_TIPO_UTILIZZO=2 AND ID_PIANO_FINANZIARIO = " & PianoF, cmbVoceBP, "ID", "DESCRIZIONE", False)
            par.caricaComboBox("SELECT  * FROM SISCOM_MI.TIPO_BOLLETTE WHERE ID=25 ORDER BY DESCRIZIONE ASC", cmbDocContabile, "ID", "DESCRIZIONE", False)
            par.caricaComboBox("SELECT  * FROM SISCOM_MI.FORNITORI ORDER BY RAGIONE_SOCIALE ASC", cmbFornitore, "ID", "RAGIONE_SOCIALE", False)
            par.caricaComboBox("SELECT  * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC", cmbstruttura, "ID", "NOME", False)

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Private Sub BindGrid1()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            Dim Str As String = ""
            'Str = "SELECT '-1' AS VOCE_BP_INTERESSI,TAB_FILIALI.nome AS STRUTTURA,TAB_GEST_REST_CREDITO.id,FORNITORI.RAGIONE_SOCIALE AS FORNITORE,TIPO_BOLLETTE.DESCRIZIONE AS TIPO_DOC_CONT,PF_VOCI.descrizione AS VOCE_BP,TO_CHAR(TO_DATE(TAB_GEST_REST_CREDITO.DATA_INIZIO_VALIDITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO_VALIDITA,TO_CHAR(TO_DATE(TAB_GEST_REST_CREDITO.DATA_FINE_VALIDITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE_VALIDITA,PF_VOCI1.DESCRIZIONE AS VOCE_BP_CREDITI FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI,SISCOM_MI.TIPO_BOLLETTE,SISCOM_MI.PF_VOCI,siscom_mi.TAB_GEST_REST_CREDITO,SISCOM_MI.PF_VOCI PF_VOCI1 WHERE TAB_FILIALI.ID=TAB_GEST_REST_CREDITO.ID_STRUTTURA AND FORNITORI.ID=TAB_GEST_REST_CREDITO.ID_FORNITORE AND TIPO_BOLLETTE.ID=TAB_GEST_REST_CREDITO.ID_DOC_REST_CREDITO AND PF_VOCI.ID=TAB_GEST_REST_CREDITO.ID_VOCE_BP_RIMBORSO AND PF_VOCI1.ID=TAB_GEST_REST_CREDITO.ID_VOCE_BP_CREDITO ORDER BY TAB_GEST_REST_CREDITO.ID DESC"
            Str = "SELECT PF_VOCI2.DESCRIZIONE AS VOCE_BP_INTERESSI,TAB_FILIALI.nome AS STRUTTURA,TAB_GEST_REST_CREDITO.id,FORNITORI.RAGIONE_SOCIALE AS FORNITORE,TIPO_BOLLETTE.DESCRIZIONE AS TIPO_DOC_CONT,PF_VOCI.descrizione AS VOCE_BP,TO_CHAR(TO_DATE(TAB_GEST_REST_CREDITO.DATA_INIZIO_VALIDITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO_VALIDITA,TO_CHAR(TO_DATE(TAB_GEST_REST_CREDITO.DATA_FINE_VALIDITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE_VALIDITA,PF_VOCI1.DESCRIZIONE AS VOCE_BP_CREDITI FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI,SISCOM_MI.TIPO_BOLLETTE,SISCOM_MI.PF_VOCI,siscom_mi.TAB_GEST_REST_CREDITO,SISCOM_MI.PF_VOCI PF_VOCI1,SISCOM_MI.PF_VOCI PF_VOCI2 WHERE TAB_FILIALI.ID=TAB_GEST_REST_CREDITO.ID_STRUTTURA AND FORNITORI.ID=TAB_GEST_REST_CREDITO.ID_FORNITORE AND TIPO_BOLLETTE.ID=TAB_GEST_REST_CREDITO.ID_DOC_REST_CREDITO AND PF_VOCI.ID=TAB_GEST_REST_CREDITO.ID_VOCE_BP_RIMBORSO AND PF_VOCI1.ID=TAB_GEST_REST_CREDITO.ID_VOCE_BP_CREDITO AND PF_VOCI2.ID(+)=TAB_GEST_REST_CREDITO.ID_VOCE_BP_INTERESSI ORDER BY TAB_GEST_REST_CREDITO.ID DESC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridParam0.DataSource = dt
            DataGridParam0.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub BindGrid()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            Dim Str As String = ""
            Str = "select * from siscom_mi.TAB_GEST_CREDITO order by id asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridParam.DataSource = dt
            DataGridParam.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub DataGridParam_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridParam.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub


    Protected Sub btn_inserisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_inserisci.Click
        Try
            Dim scriptblock As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            If txtDurata.Text = "" Then
                scriptblock = "<script language='javascript' type='text/javascript'> alert('Inserire il num. di mesi!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If


                Exit Try
            End If

            par.cmd.CommandText = "UPDATE siscom_mi.TAB_GEST_CREDITO SET N_MESI = '" & par.PulisciStrSql(Me.txtDurata.Text.ToUpper) & "' WHERE ID = " & Me.LBLID.Value
            par.cmd.ExecuteNonQuery()

            scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Modificato.Value = "0"

            LBLID.Value = 0
            txtmia.Text = "Nessuna Selezione"
            txtDurata.Text = ""
            BindGrid()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            If LBLID.Value <> "0" Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_GEST_CREDITO WHERE ID = " & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    txtDurata.Text = par.IfNull(myReader("N_MESI"), "")
                End If
                myReader.Close()

                Me.Modificato.Value = 2
            Else
                '  Me.Modificato.Value = 0
                ' Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btn_chiudi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_chiudi.Click
        txtDurata.Text = ""
        LBLID.Value = 0
        Me.txtmia.Text = "Nessuna Selezione"
        Modificato.Value = "0"
        lbl_titMotiv.Text = "Nuova Motivazione"
    End Sub

    Protected Sub DataGridParam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridParam.SelectedIndexChanged

    End Sub

    Protected Sub DataGridParam0_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridParam0.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato1) {Selezionato1.style.backgroundColor='';}Selezionato1=this;this.style.backgroundColor='red';document.getElementById('txtmia0').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID1').value='" & e.Item.Cells(0).Text & "'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato1) {Selezionato1.style.backgroundColor='';}Selezionato1=this;this.style.backgroundColor='red';document.getElementById('txtmia0').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID1').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub


    Protected Sub ImgBtnAggiungi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        If eliminato.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.cmd = par.OracleConn.CreateCommand()
                End If
                If par.IfEmpty(Me.LBLID1.Value, "") <> "" Then

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID = " & Me.LBLID1.Value
                    par.cmd.ExecuteNonQuery()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid1()
                Else
                    Response.Write("<script>alert('Nessuna voce selezionata!')</script>")
                    par.OracleConn.Close()

                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Catch EX1 As Data.OracleClient.OracleException
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.lblErrore.Visible = True
                If EX1.Code = 2292 Then
                    lblErrore.Text = "Voce in uso. Non è possibile eliminare!"
                Else
                    lblErrore.Text = EX1.Message
                End If
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
           
        End If
    End Sub

    Private Sub Update()

        'If par.IfEmpty(Me.txtInizio.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtFine.Text, "Null") <> "Null" Then
        ''*********************APERTURA CONNESSIONE**********************

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
        End If

        par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID_STRUTTURA=" & cmbstruttura.SelectedItem.Value & " AND ID_VOCE_BP_RIMBORSO=" & cmbVoceBP.SelectedItem.Value & " AND DATA_INIZIO_VALIDITA='" & par.AggiustaData(txtInizio.Text) & "' AND DATA_FINE_VALIDITA='" & par.AggiustaData(txtFine.Text) & "' AND ID_DOC_REST_CREDITO=" & cmbDocContabile.SelectedItem.Value & " AND ID_FORNITORE=" & cmbFornitore.SelectedItem.Value & " and id<>" & LBLID1.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Response.Write("<script>alert('Valori già inseriti!')</script>")
            txtInizio.Text = ""
            txtFine.Text = ""
            '*****CHIUSURA DEL MYREADER
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Exit Sub
        End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.TAB_GEST_REST_CREDITO SET ID_STRUTTURA=" & cmbstruttura.SelectedItem.Value & ",ID_VOCE_BP_RIMBORSO=" & cmbVoceBP.SelectedItem.Value & ", DATA_INIZIO_VALIDITA='" & par.AggiustaData(txtInizio.Text) & "', DATA_FINE_VALIDITA='" & par.AggiustaData(txtFine.Text) & "', ID_DOC_REST_CREDITO=" & cmbDocContabile.SelectedItem.Value & ", ID_FORNITORE=" & cmbFornitore.SelectedItem.Value & ",ID_VOCE_BP_CREDITO=" & cmbVoceBP0.SelectedItem.Value & ",ID_VOCE_BP_INTERESSI=" & cmbVoceBP1.SelectedItem.Value & " WHERE ID=" & LBLID1.Value
        par.cmd.ExecuteNonQuery()



        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        par.cmd.Dispose()
        Response.Write("<script>alert('Operazione effettuata!')</script>")
        BindGrid1()

        Me.TextBox1.Value = "0"
        txtInizio.Text = ""
        txtFine.Text = ""
        Me.LBLID1.Value = ""
        Me.txtmia0.Text = "Nessuna Selezione"
        'End If

    End Sub


    Protected Sub img_InserisciSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            Dim CodComune As String = ""

            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.cmd = par.OracleConn.CreateCommand()
                End If

                'If par.IfEmpty(Me.txtInizio.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtFine.Text, "Null") <> "Null" Then

                par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID_STRUTTURA=" & cmbstruttura.SelectedItem.Value & " AND ID_VOCE_BP_RIMBORSO=" & cmbVoceBP.SelectedItem.Value & " AND DATA_INIZIO_VALIDITA='" & par.AggiustaData(txtInizio.Text) & "' AND DATA_FINE_VALIDITA='" & par.AggiustaData(txtFine.Text) & "' AND ID_DOC_REST_CREDITO=" & cmbDocContabile.SelectedItem.Value & " AND ID_FORNITORE=" & cmbFornitore.SelectedItem.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Valori già inseriti!')</script>")
                    txtInizio.Text = ""
                    txtFine.Text = ""
                    '*****CHIUSURA DEL MYREADER
                    myReader.Close()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.TAB_GEST_REST_CREDITO (ID, ID_VOCE_BP_RIMBORSO, DATA_INIZIO_VALIDITA, DATA_FINE_VALIDITA, ID_DOC_REST_CREDITO, ID_FORNITORE,ID_STRUTTURA,ID_VOCE_BP_CREDITO,ID_VOCE_BP_INTERESSI) VALUES (SISCOM_MI.SEQ_TAB_GEST_REST_CREDITO.NEXTVAL, " & cmbVoceBP.SelectedItem.Value & ", '" & par.AggiustaData(txtInizio.Text) & "', '" & par.AggiustaData(txtFine.Text) & "', " & cmbDocContabile.SelectedItem.Value & "," & cmbFornitore.SelectedItem.Value & "," & cmbstruttura.SelectedItem.Value & "," & cmbVoceBP0.SelectedItem.Value & "," & cmbVoceBP1.SelectedItem.Value & ")"
                par.cmd.ExecuteNonQuery()

                Me.TextBox1.Value = "0"
                lblErrore.Visible = False
                Me.txtInizio.Text = ""
                Me.txtFine.Text = ""
                Me.txtmia0.Text = "Nessuna Selezione"
                LBLID1.Value = "0"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!')</script>")
                BindGrid1()
                'Else
                '    par.OracleConn.Close()
                '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '    Response.Write("<script>alert('Campi obbligatori!')</script>")
                'End If
                ' Me.txtNome.Text = ""
            End If
            svuotaCampi()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnModifica0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModifica0.Click
        If LBLID1.Value <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID = " & LBLID1.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtInizio.Text = par.FormattaData(par.IfNull(myReader("DATA_INIZIO_VALIDITA"), ""))
                txtFine.Text = par.FormattaData(par.IfNull(myReader("DATA_FINE_VALIDITA"), ""))

                cmbDocContabile.SelectedIndex = -1
                cmbDocContabile.Items.FindByValue(par.IfNull(myReader("ID_DOC_REST_CREDITO"), "")).Selected = True

                cmbFornitore.SelectedIndex = -1
                cmbFornitore.Items.FindByValue(par.IfNull(myReader("ID_FORNITORE"), "")).Selected = True

                cmbVoceBP.SelectedIndex = -1
                cmbVoceBP.Items.FindByValue(par.IfNull(myReader("ID_VOCE_BP_RIMBORSO"), "")).Selected = True

                cmbVoceBP0.SelectedIndex = -1
                If par.IfNull(myReader("ID_VOCE_BP_CREDITO"), "-1") <> "-1" Then
                    cmbVoceBP0.Items.FindByValue(par.IfNull(myReader("ID_VOCE_BP_CREDITO"), "-1")).Selected = True
                End If

                cmbVoceBP1.SelectedIndex = -1
                If par.IfNull(myReader("ID_VOCE_BP_INTERESSI"), "-1") <> "-1" Then
                    cmbVoceBP1.Items.FindByValue(par.IfNull(myReader("ID_VOCE_BP_INTERESSI"), "-1")).Selected = True
                End If

                cmbstruttura.SelectedIndex = -1
                cmbstruttura.Items.FindByValue(par.IfNull(myReader("ID_STRUTTURA"), "")).Selected = True

            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"

        Else
            Me.TextBox1.Value = "0"
            Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")

        End If
    End Sub

    Protected Sub img_ChiudiSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        svuotaCampi()
    End Sub

    Private Sub svuotaCampi()
        txtInizio.Text = ""
        txtFine.Text = ""
        cmbVoceBP.ClearSelection()
        cmbVoceBP.SelectedIndex = 0
        cmbVoceBP0.ClearSelection()
        cmbVoceBP0.SelectedIndex = 0
        cmbVoceBP1.ClearSelection()
        cmbVoceBP1.SelectedIndex = 0
        cmbDocContabile.ClearSelection()
        cmbDocContabile.SelectedIndex = 0
        cmbFornitore.ClearSelection()
        cmbFornitore.SelectedIndex = 0
    End Sub
End Class
