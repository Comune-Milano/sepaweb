
Partial Class CENSIMENTO_EdificiRifLegislativi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim stringa As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                txtSoglia.Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);")
                txtSoglia.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                caricaAbitabilita()
                BindGrid()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGrid()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim Str As String = "SELECT COD,DESCRIZIONE,DECODE(L_SPECIALE,0,'NO',1,'SI') AS L_SPECIALE FROM SISCOM_MI.TAB_RIFERIMENTI_LEG ORDER BY COD asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.TAB_RIFERIMENTI_LEG")

            DataGridIntLegali.DataSource = ds
            DataGridIntLegali.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        txtDescrizione.Text = ""
        Me.txtid.Value = ""
        Me.txtmia.Text = "Nessuna Selezione"
        TextBox1.Value = "0"
        cmbSpeciale.SelectedIndex = -1
        cmbSpeciale.Items.FindByValue("0").Selected = True
        MultiView1.ActiveViewIndex = 0
    End Sub


    Protected Sub ImgBtnAggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.TAB_RIFERIMENTI_LEG WHERE COD = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()
                stringa = txtmia.Text
                NomeCommissariato()
                
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                BindGrid()
                '******pulizia variabili uso per modifica e cancellazione!!!++++++
                Me.txtmia.Text = "Nessuna Selezione"
                Me.txtid.Value = ""

            Else
                Response.Write("<script>alert('Nessuna voce selezionata!')</script>")
                par.OracleConn.Close()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            If EX1.Number = 2292 Then
                lblErrore.Text = "Riferimento in uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGridIntLegali_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntLegali.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub

    Protected Sub DataGridIntLegali_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIntLegali.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridIntLegali.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridIntLegali_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridIntLegali.SelectedIndexChanged

    End Sub

    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value.ToString <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_RIFERIMENTI_LEG WHERE COD = " & Me.txtid.Value

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtDescrizione.Text = myReader("DESCRIZIONE").ToString
                cmbSpeciale.SelectedIndex = -1
                cmbSpeciale.Items.FindByValue(myReader("L_SPECIALE").ToString).Selected = True
            End If
            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"
            MultiView1.ActiveViewIndex = 1
        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")
            MultiView1.ActiveViewIndex = 0
        End If
    End Sub

    Private Sub Update()
        Try

            If par.IfEmpty(Me.txtDescrizione.Text, "Null") <> "Null" Then
                ''*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_RIFERIMENTI_LEG WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(Me.txtDescrizione.Text.ToUpper) & "' AND COD <>" & txtid.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Riferimento già inserito!')</script>")
                    Me.txtDescrizione.Text = ""
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.TAB_RIFERIMENTI_LEG SET DESCRIZIONE = '" & par.PulisciStrSql(Me.txtDescrizione.Text.ToUpper) & "',L_SPECIALE=" & cmbSpeciale.SelectedItem.Value & " WHERE COD = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()

                stringa = txtmia.Text
                NomeCommissariato()
                

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                par.cmd.Dispose()
                BindGrid()
                MultiView1.ActiveViewIndex = 0
                Me.TextBox1.Value = "0"
                Me.txtid.Value = ""
                Me.txtmia.Text = "Nessuna Selezione"
                Me.txtDescrizione.Text = ""
                cmbSpeciale.SelectedIndex = -1
                cmbSpeciale.Items.FindByValue("0").Selected = True
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub NomeCommissariato()
        Try
            Dim posiniziale As Long
            Dim posfinale As Long
            Dim rimozione As String
            Do
                posiniziale = InStr(1, stringa, "Hai selezionato")
                If posiniziale > 0 Then
                    posfinale = InStr(1, stringa, ": ")
                    If posfinale > 0 Then
                        rimozione = Mid$(stringa, posiniziale, posfinale - posiniziale + 1)
                        stringa = Replace(stringa, rimozione, vbNullString)
                    Else
                        Exit Do
                    End If
                End If
            Loop Until posiniziale = 0
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If par.IfEmpty(Me.txtDescrizione.Text, "") <> "" Then

                    par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_RIFERIMENTI_LEG WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(Me.txtDescrizione.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Riferimento già inserito!')</script>")
                        Me.txtDescrizione.Text = ""
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub

                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.TAB_RIFERIMENTI_LEG (COD,DESCRIZIONE,L_SPECIALE) VALUES (SISCOM_MI.SEQ_TAB_RIFERIMENTI_LEGI.NEXTVAL,'" & par.PulisciStrSql(UCase(Me.txtDescrizione.Text)) & "'," & cmbSpeciale.SelectedItem.Value & ")"
                    par.cmd.ExecuteNonQuery()

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid()
                    MultiView1.ActiveViewIndex = 0
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Campo DESCRIZIONE obbligatorio!')</script>")
                End If
                Me.txtDescrizione.Text = ""
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub ImgAggiungi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgAggiungi.Click
        MultiView1.ActiveViewIndex = 1
    End Sub

    Private Sub caricaAbitabilita()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='SOGLIA ABITABILITA'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim soglia As Decimal = 0
            If lettore.Read Then
                soglia = par.IfNull(CDec(lettore(0)), 0)
            End If
            lettore.Close()
            txtSoglia.Text = Format(soglia, "#,##0.00")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore nel caricamento della soglia!');</script>")
        End Try
    End Sub

    Protected Sub btnModificaSoglia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModificaSoglia.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI SET VALORE='" & txtSoglia.Text & "' WHERE PARAMETRO='SOGLIA ABITABILITA'"
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Soglia modificata con successo!');</script>")
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore nella modifica della soglia!');</script>")
        End Try
    End Sub
End Class
