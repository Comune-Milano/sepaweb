
Partial Class RILEVAZIONI_Default
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_GEST") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                par.caricaComboBox("select * from SISCOM_MI.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1) order by descrizione asc", cmbUtenti, "ID", "DESCRIZIONE", False)
                BindGridOperatori()
            End If
            cmbOperatore.Enabled = True
            Me.txtEmail.Text = ""
            Me.txtTel.Text = ""
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUtenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGridOperatori()
        Try
            connData.apri()

            Dim Str As String = ""
            Str = "select OPERATORI.ID,operatori.operatore,operatori.cognome,operatori.nome,RILIEVO_UTENTI_OPERATORI.email,RILIEVO_UTENTI_OPERATORI.telefono from operatori,SISCOM_MI.RILIEVO_UTENTI_OPERATORI where operatori.id=RILIEVO_UTENTI_OPERATORI.id_operatore and RILIEVO_UTENTI_OPERATORI.id_utente=" & cmbUtenti.SelectedItem.Value & " order by operatore"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridOperatori.DataSource = dt
            DataGridOperatori.DataBind()

            par.caricaComboBox("select OPERATORI.ID,OPERATORI.OPERATORE from OPERATORI WHERE FL_RILIEVO_CAR=1 AND ID_CAF=2 AND ID NOT IN (SELECT ID_OPERATORE FROM SISCOM_MI.RILIEVO_UTENTI_OPERATORI WHERE ID_UTENTE IN (select ID from SISCOM_MI.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1))) ORDER BY OPERATORE ASC", cmbOperatore, "ID", "OPERATORE", False)
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbUtenti_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbUtenti.SelectedIndexChanged
        BindGridOperatori()
    End Sub

    Protected Sub DataGridOperatori_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridOperatori.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_LBLID').value='" & e.Item.Cells(0).Text & "';")
            'e.Item.Attributes.Add("onDblclick", "document.getElementById('MasterPage_ContentPlaceHolder2_ButtonClickEsercizio').click();")
        End If
    End Sub


    Protected Sub btnSalvaDen_Click(sender As Object, e As System.EventArgs) Handles btnSalvaDen.Click
        If Me.TextBox1.Value = 2 Then
            Update()
        ElseIf Me.TextBox1.Value = 1 Then
            If IsNothing(cmbOperatore.SelectedItem) = False Then
                Try
                    connData.apri()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_UTENTI_OPERATORI (ID_UTENTE, ID_OPERATORE, EMAIL, TELEFONO) VALUES (" & cmbUtenti.SelectedItem.Value & "," & cmbOperatore.SelectedItem.Value & "," & par.insDbValue(txtEmail.Text, True) & "," & par.insDbValue(txtTel.Text, True) & ")"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi()
                    par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
                    BindGridOperatori()
                    TextBox1.Value = ""
                    txtEmail.Text = ""
                    txtTel.Text = ""

                Catch ex As Exception
                    connData.chiudi()
                    Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUtenti-Operatori - " & ex.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End Try
            Else
                par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
            End If
        End If
    End Sub

    Private Sub Update()

        connData.apri()

        'par.cmd.CommandText = "select * FROM SISCOM_MI.RILIEVO_UTENTI_OPERATORI WHERE id_operatore<>" & LBLID.Value & " and id_utente=" & cmbUtenti.SelectedValue & ""
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'If myReader.Read Then
        '    par.modalDialogMessage("Info", "Valore già inserito!", Me.Page)
        '    myReader.Close()
        '    connData.chiudi()
        '    Exit Sub
        'End If
        'myReader.Close()

        par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_UTENTI_OPERATORI SET id_utente=" & cmbUtenti.SelectedValue & ",telefono='" & par.IfEmpty(txtTel.Text, "") & "',email='" & par.IfEmpty(txtEmail.Text, "") & "' WHERE ID_operatore=" & LBLID.Value
        par.cmd.ExecuteNonQuery()

        Me.TextBox1.Value = "0"
        Me.txtEmail.Text = ""
        Me.txtTel.Text = ""
        Me.LBLID.Value = ""

        connData.chiudi()
        par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
        BindGridOperatori()

       
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
        CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
        If TextBox1.Value <> "2" Then
            par.caricaComboBox("select OPERATORI.ID,OPERATORI.OPERATORE from OPERATORI WHERE FL_RILIEVO_CAR=1 AND ID_CAF=2 AND ID NOT IN (SELECT ID_OPERATORE FROM SISCOM_MI.RILIEVO_UTENTI_OPERATORI WHERE ID_UTENTE IN (select ID from SISCOM_MI.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1))) ORDER BY OPERATORE ASC", cmbOperatore, "ID", "OPERATORE", False)
            Me.txtEmail.Text = ""
            Me.txtTel.Text = ""
        End If
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        'If par.IfEmpty(Me.LBLID.Value, "") <> "" Then
        '    par.modalDialogConfirm("Attenzione", "Eliminare l\'elemento selezionato?", "SI", "document.getElementById('CPContenuto_btnEliminaElemento').click();", "NO", "return false;", Me.Page)
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
        'End If
    End Sub

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try
            connData.apri()

            If par.IfEmpty(Me.LBLID.Value, "") <> "" Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_UTENTI_OPERATORI WHERE ID_UTENTE=" & cmbUtenti.SelectedItem.Value & " AND ID_operatore = " & Me.LBLID.Value
                par.cmd.ExecuteNonQuery()
                connData.chiudi()
                BindGridOperatori()
                TextBox1.Value = ""
                par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
            Else
                par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
                connData.chiudi()
            End If
        Catch EX1 As Data.OracleClient.OracleException
            connData.chiudi()
            Me.lblErrore.Visible = True
            If EX1.Code = 2292 Then
                lblErrore.Text = "Utente in uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGridOperatori_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridOperatori.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridOperatori.CurrentPageIndex = e.NewPageIndex
            BindGridOperatori()
        End If
    End Sub


    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        If LBLID.Value <> "" Then
            cmbOperatore.Enabled = False
            connData.apri()
            par.caricaComboBox("select OPERATORI.ID,OPERATORI.OPERATORE from OPERATORI WHERE ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.RILIEVO_UTENTI_OPERATORI WHERE ID_UTENTE IN (select ID from SISCOM_MI.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1))) ORDER BY OPERATORE ASC", cmbOperatore, "ID", "OPERATORE", False)

            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.RILIEVO_UTENTI_OPERATORI WHERE ID_operatore = " & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                cmbOperatore.SelectedValue = LBLID.Value
                cmbUtenti.SelectedValue = par.IfNull(myReader("ID_UTENTE"), "")
                txtEmail.Text = par.IfNull(myReader("EMAIL"), "")
                txtTel.Text = par.IfNull(myReader("TELEFONO"), "")
            End If
            myReader.Close()
            connData.chiudi()
            Me.TextBox1.Value = "2"
        Else
            Me.TextBox1.Value = "0"
            par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
        End If
    End Sub
End Class
