
Partial Class RILEVAZIONI_GestReferenti
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
            Str = "select rilievo_referenti.* from SISCOM_MI.rilievo_referenti order by cognome asc"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridRefer.DataSource = dt
            DataGridRefer.DataBind()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUtenti - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGridRefer_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridRefer.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
        CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
    End Sub

    Protected Sub DataGridRefer_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridRefer.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridRefer.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Private Sub Update()

        If par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" Or par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" Then
            connData.apri()

            par.cmd.CommandText = "select * FROM SISCOM_MI.RILIEVO_REFERENTI WHERE id<>" & LBLID.Value & " and cognome='" & par.PulisciStrSql(txtCognome.Text.ToUpper) & "' and nome='" & par.PulisciStrSql(txtNome.Text.ToUpper) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                par.modalDialogMessage("Info", "Valore già inserito!", Me.Page)
                myReader.Close()
                connData.chiudi()
                Exit Sub
            End If
            myReader.Close()

            par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_REFERENTI SET cognome='" & par.PulisciStrSql(txtCognome.Text.ToUpper) & "',nome='" & par.PulisciStrSql(txtNome.Text.ToUpper) & "',telefono='" & par.IfEmpty(txtTel.Text, "") & "',email='" & par.IfEmpty(txtEmail.Text, "") & "' WHERE ID=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
            BindGrid()

            Me.TextBox1.Value = "0"
            Me.txtCognome.Text = ""
            Me.txtNome.Text = ""
            Me.txtEmail.Text = ""
            Me.txtTel.Text = ""
            Me.LBLID.Value = ""
        End If

    End Sub

    Protected Sub btnSalvaDen_Click(sender As Object, e As System.EventArgs) Handles btnSalvaDen.Click
        Try
            Dim CodComune As String = ""

            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then
                connData.apri()

                If par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" Or par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" Then
                    par.cmd.CommandText = "select * FROM SISCOM_MI.RILIEVO_REFERENTI WHERE COGNOME='" & par.PulisciStrSql(txtCognome.Text.ToUpper) & "' and NOME='" & par.PulisciStrSql(txtNome.Text.ToUpper) & "' and EMAIL='" & par.IfEmpty(txtEmail.Text, "") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        par.modalDialogMessage("Attenzione", "Elemento già inserito!", Me.Page)
                        Me.txtCognome.Text = ""
                        Me.txtNome.Text = ""
                        Me.txtEmail.Text = ""
                        Me.txtTel.Text = ""
                        myReader.Close()
                        connData.chiudi()
                        Exit Sub
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_REFERENTI (ID, COGNOME,NOME,EMAIL,TELEFONO) VALUES (SISCOM_MI.SEQ_RILIEVO_REFERENTI.NEXTVAL, '" & par.PulisciStrSql(txtCognome.Text.ToUpper) & "','" & par.PulisciStrSql(txtNome.Text.ToUpper) & "','" & par.IfEmpty(txtEmail.Text, "") & "','" & par.IfEmpty(txtTel.Text, "") & "')"
                    par.cmd.ExecuteNonQuery()

                    Me.TextBox1.Value = "0"
                    lblErrore.Visible = False
                    Me.txtCognome.Text = ""
                    Me.txtNome.Text = ""
                    Me.txtEmail.Text = ""
                    Me.txtTel.Text = ""
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

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try
            connData.apri()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_REFERENTI WHERE ID = " & Me.LBLID.Value
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            Me.TextBox1.Value = "0"
            txtCognome.Text = ""
            txtNome.Text = ""
            txtEmail.Text = ""
            txtTel.Text = ""
            Me.LBLID.Value = ""
            BindGrid()

        Catch EX1 As Data.OracleClient.OracleException
            connData.chiudi()
            Me.lblErrore.Visible = True
            If EX1.Code = 2292 Then
                lblErrore.Text = "Referente in uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        If LBLID.Value <> "" Then
            connData.apri()
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.RILIEVO_REFERENTI WHERE ID = " & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtCognome.Text = par.IfNull(myReader("COGNOME"), "")
                txtNome.Text = par.IfNull(myReader("NOME"), "")
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

    Protected Sub btnChiudi_Click(sender As Object, e As System.EventArgs) Handles btnChiudi.Click
        txtCognome.Text = ""
        txtNome.Text = ""
        txtEmail.Text = ""
        txtTel.Text = ""
    End Sub
End Class
