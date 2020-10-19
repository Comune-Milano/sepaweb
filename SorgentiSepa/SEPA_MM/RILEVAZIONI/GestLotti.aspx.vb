
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
                BindGridLotti()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Gestione Lotti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGridLotti()
        Try
            connData.apri()

            Dim Str As String = ""
            Str = "select RILIEVO_TAB_UTENTI.DESCRIZIONE AS UTENTE,RILIEVO_REFERENTI.COGNOME ||' '|| RILIEVO_REFERENTI.NOME AS REFERENTE,RILIEVO_LOTTI.*,replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£AssegnaUnita('||rilievo_lotti.id||');£><img src=£../Standard/Immagini/Gestione_24.png£ alt=£Gestione£ border=£0£/></a>','$','&'),'£','" & Chr(34) & "') as UNITA from SISCOM_MI.RILIEVO_TAB_UTENTI,SISCOM_MI.RILIEVO_LOTTI,SISCOM_MI.RILIEVO_REFERENTI where RILIEVO_TAB_UTENTI.ID=RILIEVO_LOTTI.ID_UTENTE AND RILIEVO_REFERENTI.ID(+)=RILIEVO_LOTTI.ID_REFERENTE AND RILIEVO_TAB_UTENTI.id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1) order by RILIEVO_LOTTI.descrizione asc"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridLotti.DataSource = dt
            DataGridLotti.DataBind()

            par.caricaComboBox("SELECT * FROM SISCOM_MI.RILIEVO_TAB_UTENTI WHERE ID_RILIEVO=(SELECT ID FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1) ORDER BY DESCRIZIONE ASC", cmbUtenti, "ID", "DESCRIZIONE", False)
            par.caricaComboBox("SELECT ID,COGNOME ||' '|| NOME AS REFERENTE FROM SISCOM_MI.RILIEVO_REFERENTI ORDER BY COGNOME ASC", cmbReferenti, "ID", "REFERENTE", True)

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Gestione Lotti - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CType(Me.Master.FindControl("noClose"), HiddenField).Value = 1
        CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
    End Sub

    Protected Sub DataGridParam0_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridLotti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_LBLID').value='" & e.Item.Cells(0).Text & "';")
            'e.Item.Attributes.Add("onDblclick", "document.getElementById('MasterPage_ContentPlaceHolder2_ButtonClickEsercizio').click();")
        End If
    End Sub

    Protected Sub DataGridParam0_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridLotti.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridLotti.CurrentPageIndex = e.NewPageIndex
            BindGridLotti()
        End If
    End Sub

    Private Sub Update()

        If par.IfEmpty(Me.txtDenominazione.Text, "Null") <> "Null" Then
            connData.apri()
            Dim referente As String = ""
            referente = cmbReferenti.SelectedValue
            If referente = "-1" Then
                referente = "NULL"
            End If
            par.cmd.CommandText = "select * FROM SISCOM_MI.RILIEVO_LOTTI WHERE id<>" & LBLID.Value & " and ID_UTENTE=" & cmbUtenti.SelectedItem.Value & " AND DESCRIZIONE='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                par.modalDialogMessage("Info", "Elemento già presente!", Me.Page)
                myReader.Close()
                connData.chiudi()
                Exit Sub
            End If
            myReader.Close()
            par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_LOTTI SET DESCRIZIONE='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "',ID_UTENTE=" & cmbUtenti.SelectedItem.Value & ",ID_REFERENTE=" & referente & " WHERE ID=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
            BindGridLotti()

            Me.TextBox1.Value = "0"
            txtDenominazione.Text = ""
            Me.LBLID.Value = ""
        Else
            par.modalDialogMessage("Attenzione", "Campi Obbligatori!", Me.Page)
        End If

    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        If LBLID.Value <> "" Then
            connData.apri()
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.RILIEVO_LOTTI WHERE ID = " & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtDenominazione.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                cmbUtenti.SelectedIndex = -1
                cmbUtenti.Items.FindByValue(par.IfNull(myReader("ID_UTENTE"), "1")).Selected = True
                cmbReferenti.SelectedIndex = -1
                cmbReferenti.Items.FindByValue(par.IfNull(myReader("ID_REFERENTE"), -1)).Selected = True
            End If
            myReader.Close()
            connData.chiudi()
            Me.TextBox1.Value = "2"
        Else
            Me.TextBox1.Value = "0"
            par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
        End If
    End Sub

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try
            connData.apri()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_LOTTI WHERE ID = " & Me.LBLID.Value
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            Me.TextBox1.Value = "0"
            txtDenominazione.Text = ""
            Me.LBLID.Value = ""
            BindGridLotti()

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

    Protected Sub btnSalvaDen_Click(sender As Object, e As System.EventArgs) Handles btnSalvaDen.Click

        Try
            Dim CodComune As String = ""

            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then
                connData.apri()

                If par.IfEmpty(Me.txtDenominazione.Text, "Null") <> "Null" Then
                    par.cmd.CommandText = "select * FROM SISCOM_MI.RILIEVO_LOTTI WHERE ID_UTENTE=" & cmbUtenti.SelectedItem.Value & " AND DESCRIZIONE='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        par.modalDialogMessage("Attenzione", "Elemento già inserita!", Me.Page)
                        txtDenominazione.Text = ""
                        myReader.Close()
                        connData.chiudi()
                        Exit Sub
                    End If
                    myReader.Close()
                    Dim referente As String = ""
                    referente = cmbReferenti.SelectedValue
                    If referente = "-1" Then
                        referente = "NULL"
                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_LOTTI (ID, DESCRIZIONE,ID_UTENTE, ID_REFERENTE) VALUES (SISCOM_MI.SEQ_RILIEVO_LOTTI.NEXTVAL, '" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "'," & cmbUtenti.SelectedItem.Value & "," & referente & ")"
                    par.cmd.ExecuteNonQuery()

                    Me.TextBox1.Value = "0"
                    lblErrore.Visible = False
                    Me.txtDenominazione.Text = ""
                    LBLID.Value = ""
                    connData.chiudi()
                    par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
                    BindGridLotti()
                Else
                    connData.chiudi()
                    par.modalDialogMessage("Attenzione", "Campi Obbligatori!", Me.Page)
                End If
            End If

        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
End Class
