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
                BindGrid()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUtenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            connData.apri()

            Dim Str As String = ""
            Str = "select * from SISCOM_MI.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1) order by descrizione asc"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridParam0.DataSource = dt
            DataGridParam0.DataBind()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUtenti - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
        CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
    End Sub

    Protected Sub DataGridParam0_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridParam0.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_LBLID').value='" & e.Item.Cells(0).Text & "';")
            'e.Item.Attributes.Add("onDblclick", "document.getElementById('MasterPage_ContentPlaceHolder2_ButtonClickEsercizio').click();")
        End If
    End Sub

    Private Sub Update()

        If par.IfEmpty(Me.txtDenominazione.Text, "Null") <> "Null" Then
            connData.apri()

            par.cmd.CommandText = "select * FROM SISCOM_MI.RILIEVO_TAB_UTENTI WHERE id<>" & LBLID.Value & " and ID_RILIEVO=(SELECT ID FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1) AND DESCRIZIONE='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                par.modalDialogMessage("Info", "Valore già inserito!", Me.Page)
                'txtDenominazione.Text = ""
                '*****CHIUSURA DEL MYREADER
                myReader.Close()
                connData.chiudi()
                Exit Sub
            End If
            myReader.Close()
            par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_TAB_UTENTI SET DESCRIZIONE='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "' WHERE ID=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
            BindGrid()

            Me.TextBox1.Value = "0"
            txtDenominazione.Text = ""
            Me.LBLID.Value = ""
        End If

    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        If LBLID.Value <> "" Then
            connData.apri()
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.RILIEVO_TAB_UTENTI WHERE ID = " & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtDenominazione.Text = par.IfNull(myReader("DESCRIZIONE"), "")
            End If
            myReader.Close()
            connData.chiudi()
            Me.TextBox1.Value = "2"
        Else
            Me.TextBox1.Value = "0"
            par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
        End If
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        'If par.IfEmpty(Me.LBLID.Value, "") <> "" Then
        '    par.modalDialogConfirm("Attenzione", "Eliminare l\'elemento selezionato?", "SI", "document.getElementById('CPContenuto_btnEliminaElemento').click();", "NO", "return false;", Me.Page)
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
        'End If
    End Sub

    Public Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try
            connData.apri()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_TAB_UTENTI WHERE ID = " & Me.LBLID.Value
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            Me.TextBox1.Value = "0"
            txtDenominazione.Text = ""
            Me.LBLID.Value = ""
            BindGrid()

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
                    par.cmd.CommandText = "select * FROM SISCOM_MI.RILIEVO_TAB_UTENTI WHERE ID_RILIEVO=(SELECT ID FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1) AND DESCRIZIONE='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        par.modalDialogMessage("Attenzione", "Elemento già inserito!", Me.Page)
                        txtDenominazione.Text = ""
                        myReader.Close()
                        connData.chiudi()
                        Exit Sub
                    End If
                    myReader.Close()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_TAB_UTENTI (ID, DESCRIZIONE,ID_RILIEVO) VALUES (SISCOM_MI.SEQ_RILIEVO_TAB_UTENTI.NEXTVAL, '" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "',(SELECT ID FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1))"
                    par.cmd.ExecuteNonQuery()

                    Me.TextBox1.Value = "0"
                    lblErrore.Visible = False
                    Me.txtDenominazione.Text = ""
                    LBLID.Value = ""
                    connData.chiudi()
                    par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
                    BindGrid()
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

    Protected Sub DataGridParam0_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridParam0.SelectedIndexChanged

    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
End Class
