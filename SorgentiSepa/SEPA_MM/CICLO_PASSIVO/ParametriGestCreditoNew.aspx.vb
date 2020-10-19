
Imports Telerik.Web.UI

Partial Class Contratti_ParametriGestCredito
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Caricacombo()
            BindGrid()
            BindGrid1()
        End If
    End Sub

    Private Sub Caricacombo()
        Try
            Dim PianoF As Long = par.RicavaPianoUltimoApprovato
            par.caricaComboTelerik("select codice,codice||'-'||descrizione as DESCRIZIONE from siscom_mi.pf_voci where id_piano_finanziario=" & PianoF & " and connect_by_isleaf=1 connect by prior id=id_voce_madre start with id_voce_madre is null", cmbVoceBP0, "codice", "DESCRIZIONE", True)
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_BOLLETTE ORDER BY DESCRIZIONE ASC", cmbDocContabile, "ID", "DESCRIZIONE", True)
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_BOLLETTE_GEST ORDER BY DESCRIZIONE ASC", cmbDocGestionale, "ID", "DESCRIZIONE", True)
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.FORNITORI ORDER BY RAGIONE_SOCIALE ASC", cmbFornitore, "ID", "RAGIONE_SOCIALE", True)
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC", cmbstruttura, "ID", "NOME", True)

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGrid1()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            Dim Str As String = ""
            Str = "select tab_filiali.nome as struttura," _
                    & " tab_gest_rest_credito.id," _
                    & " fornitori.ragione_sociale as fornitore," _
                    & " tipo_bollette.descrizione as tipo_doc_cont," _
                    & " (select codice||'-'||descrizione from siscom_mi.pf_voci where codice=codice_pf_voce " _
                    & " and id_piano_finanziario=" & par.RicavaPianoUltimoApprovato & ") as voce_bp," _
                    & " (select descrizione from siscom_mi.tipo_bollette_gest where id=id_tipo_gest) as tipo_gest" _
                    & " from siscom_mi.tab_filiali," _
                    & " siscom_mi.fornitori," _
                    & " siscom_mi.tipo_bollette," _
                    & " siscom_mi.tab_gest_rest_credito" _
                    & " where tab_filiali.id = tab_gest_rest_credito.id_struttura" _
                    & " And fornitori.id = tab_gest_rest_credito.id_fornitore" _
                    & " And tipo_bollette.id = tab_gest_rest_credito.id_doc_rest_credito" _
                    & " order by tab_gest_rest_credito.id asc"

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


    'Protected Sub DataGridParam_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridParam.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "'")
    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "'")
    '    End If
    'End Sub

    Protected Sub DataGridParam_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridParam.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato: " & Replace(dataItem("DESCRIZIONE").Text, "'", "\'") & "';document.getElementById('LBLID').value='" & dataItem("ID").Text & "'")
            e.Item.Attributes.Add("onDblClick", "document.getElementById('btnModifica').click();")
        End If
    End Sub

    Protected Sub btn_inserisci_Click(sender As Object, e As System.EventArgs) Handles btn_inserisci.Click
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

            'scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
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

    Protected Sub btn_chiudi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_chiudi.Click
        txtDurata.Text = ""
        LBLID.Value = 0
        Me.txtmia.Text = "Nessuna Selezione"
        Modificato.Value = "0"
        'lbl_titMotiv.Text = "Nuova Motivazione"
    End Sub

    Protected Sub DataGridParam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridParam.SelectedIndexChanged

    End Sub

    Protected Sub DataGridParam0_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridParam0.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia0').value='Hai selezionato: " & Replace(dataItem("STRUTTURA").Text, "'", "\'") & "';document.getElementById('LBLID1').value='" & dataItem("ID").Text & "'")
            e.Item.Attributes.Add("onDblClick", "document.getElementById('btnModifica0').click();")
        End If
    End Sub



    Private Sub Update()

        'If par.IfEmpty(Me.txtInizio.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtFine.Text, "Null") <> "Null" Then
        ''*********************APERTURA CONNESSIONE**********************

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
        End If

        par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID_STRUTTURA=" & cmbstruttura.SelectedValue & " AND " _
            & " CODICE_PF_VOCE='" & cmbVoceBP0.SelectedValue & "' And id_tipo_gest=" & cmbDocGestionale.SelectedValue _
            & " And ID_DOC_REST_CREDITO=" & cmbDocContabile.SelectedValue & " And ID_FORNITORE=" & cmbFornitore.SelectedValue & " And id<>" & LBLID1.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Response.Write("<script>alert('Valori già inseriti!')</script>")
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Exit Sub
        End If

        par.cmd.CommandText = "UPDATE SISCOM_MI.TAB_GEST_REST_CREDITO SET ID_STRUTTURA=" & cmbstruttura.SelectedValue & ",CODICE_PF_VOCE='" & cmbVoceBP0.SelectedValue & "'," _
            & " ID_DOC_REST_CREDITO=" & cmbDocContabile.SelectedValue & ", ID_FORNITORE=" & cmbFornitore.SelectedValue & ",id_tipo_gest=" & cmbDocGestionale.SelectedValue _
            & " WHERE ID=" & LBLID1.Value
        par.cmd.ExecuteNonQuery()



        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        par.cmd.Dispose()
        'Response.Write("<script>alert('Operazione effettuata!')</script>")
        BindGrid1()

        Me.TextBox1.Value = "0"
        Me.LBLID1.Value = ""
        Me.txtmia0.Text = "Nessuna Selezione"
        'End If

    End Sub


    Protected Sub img_InserisciSchema_Click(sender As Object, e As System.EventArgs) Handles img_InserisciSchema.Click
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

                par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID_STRUTTURA=" & cmbstruttura.SelectedValue & " AND CODICE_PF_VOCE='" & cmbVoceBP0.SelectedValue & "'" _
                    & " And ID_DOC_REST_CREDITO=" & cmbDocContabile.SelectedValue & " And ID_FORNITORE=" & cmbFornitore.SelectedValue
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Valori già inseriti!')</script>")
                    myReader.Close()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.TAB_GEST_REST_CREDITO (ID, CODICE_PF_VOCE, ID_DOC_REST_CREDITO,ID_TIPO_GEST," _
                    & " ID_FORNITORE,ID_STRUTTURA) VALUES (SISCOM_MI.SEQ_TAB_GEST_REST_CREDITO.NEXTVAL, '" & cmbVoceBP0.SelectedValue & "'," _
                    & "" & cmbDocContabile.SelectedValue & "," & cmbDocGestionale.SelectedValue & "," & cmbFornitore.SelectedValue & "," _
                    & cmbstruttura.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()

                Me.TextBox1.Value = "0"
                lblErrore.Visible = False
                Me.txtmia0.Text = "Nessuna Selezione"
                LBLID1.Value = "0"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'Response.Write("<script>alert('Operazione effettuata!')</script>")
                BindGrid1()
                'Else
                '    par.OracleConn.Close()
                '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '    Response.Write("<script>alert('Campi obbligatori!')</script>")
                'End If
                ' Me.txtNome.Text = ""
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnModifica0_Click(sender As Object, e As System.EventArgs) Handles btnModifica0.Click

        ModificaGest()
    End Sub

    Private Sub ModificaGest()
        If LBLID1.Value <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID = " & LBLID1.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                cmbDocContabile.ClearSelection()
                If Not IsNothing(myReader("ID_DOC_REST_CREDITO")) Then
                    cmbDocContabile.SelectedValue = par.IfNull(myReader("ID_DOC_REST_CREDITO"), "-1")
                End If

                cmbDocGestionale.ClearSelection()
                If Not IsNothing(myReader("ID_TIPO_GEST")) Then
                    cmbDocGestionale.SelectedValue = par.IfNull(myReader("ID_TIPO_GEST"), "-1")
                End If

                cmbFornitore.ClearSelection()
                If Not IsNothing(myReader("ID_FORNITORE")) Then
                    cmbFornitore.SelectedValue = par.IfNull(myReader("ID_FORNITORE"), "-1")
                End If

                cmbVoceBP0.ClearSelection()

                If par.IfNull(myReader("CODICE_PF_VOCE"), "-1") <> "-1" Then
                    cmbVoceBP0.SelectedValue = par.IfNull(myReader("CODICE_PF_VOCE"), "-1")
                End If

                cmbstruttura.ClearSelection()
                    cmbstruttura.Items.FindItemByValue(par.IfNull(myReader("ID_STRUTTURA"), "")).Selected = True
                    Dim script As String = "function f(){$find(""" + RadWindowGestCrediti.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
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

    Protected Sub DataGridParam_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGridParam.ItemCommand
        Try
            Select Case e.CommandName
                Case "myEdit"
                    ModificaMesi()

            End Select
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ModificaMesi()
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
                Dim script As String = "function f(){$find(""" + RadWindowMesi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
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

    Protected Sub DataGridParam0_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGridParam0.ItemCommand
        Try
            Select Case e.CommandName
                Case "myEdit"
                    lblTitoloWindow.Text = "Modifica"
                    ModificaGest()
                Case "Delete"
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
            End Select
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    Private Sub btnModifica_Click(sender As Object, e As EventArgs) Handles btnModifica.Click
        ModificaMesi()
    End Sub

    Private Sub DataGridParam0_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridParam0.NeedDataSource
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            Dim Str As String = ""
            Str = "select tab_filiali.nome as struttura," _
                    & " tab_gest_rest_credito.id," _
                    & " fornitori.ragione_sociale as fornitore," _
                    & " tipo_bollette.descrizione as tipo_doc_cont," _
                    & " (select codice||'-'||descrizione from siscom_mi.pf_voci where codice=codice_pf_voce " _
                    & " and id_piano_finanziario=" & par.RicavaPianoUltimoApprovato & ") as voce_bp," _
                    & " (select descrizione from siscom_mi.tipo_bollette_gest where id=id_tipo_gest) as tipo_gest" _
                    & " from siscom_mi.tab_filiali," _
                    & " siscom_mi.fornitori," _
                    & " siscom_mi.tipo_bollette," _
                    & " siscom_mi.tab_gest_rest_credito" _
                    & " where tab_filiali.id = tab_gest_rest_credito.id_struttura" _
                    & " And fornitori.id = tab_gest_rest_credito.id_fornitore" _
                    & " And tipo_bollette.id = tab_gest_rest_credito.id_doc_rest_credito" _
                    & " order by tab_gest_rest_credito.id asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridParam0.DataSource = dt
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub DataGridParam_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridParam.NeedDataSource
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
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub img_ChiudiSchema_Click(sender As Object, e As EventArgs) Handles img_ChiudiSchema.Click
        cmbDocContabile.ClearSelection()
        cmbDocGestionale.ClearSelection()
        cmbFornitore.ClearSelection()
        cmbstruttura.ClearSelection()
        cmbVoceBP0.ClearSelection()
    End Sub

    Private Sub Contratti_ParametriGestCredito_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Me.TextBox1.Value = 2 Then
            lblTitoloWindow.Text = "Modifica"
        Else
            cmbDocContabile.ClearSelection()
            cmbDocGestionale.ClearSelection()
            cmbFornitore.ClearSelection()
            cmbstruttura.ClearSelection()
            cmbVoceBP0.ClearSelection()
            lblTitoloWindow.Text = "Inserisci"
        End If

    End Sub
End Class
