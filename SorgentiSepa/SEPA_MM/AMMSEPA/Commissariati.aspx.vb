
Partial Class AMMSEPA_Commissariati
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim stringa As String

    Function Eventi_Gestione(ByVal operatore As String, ByVal cod_evento As String, ByVal motivazione As String) As Integer
        Dim data As String = Format(Now, "yyyyMMddHHmmss")
        Try
            PAR.cmd.CommandText = "INSERT INTO EVENTI_GESTIONE (ID_OPERATORE, COD_EVENTO, DATA_ORA, MOTIVAZIONE) VALUES" _
                            & " (" & operatore & ",'" & cod_evento & "'," & data & ",'" & motivazione & "')"
            PAR.cmd.ExecuteNonQuery()
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
        Return 0
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim str As String = ""
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        str = str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        str = str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()
        Try
            BindGrid()

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

            Dim Str As String = "SELECT * FROM SISCOM_MI.TAB_commissariati ORDER BY descrizione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.TAB_COMMISSARIATI")

            DataGridIntLegali.DataSource = ds
            DataGridIntLegali.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
                If par.IfEmpty(Me.txtCommissariato.Text, "") <> "" Then

                    par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_COMMISSARIATI WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(Me.txtCommissariato.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Valore già inserito!')</script>")
                        Me.txtCommissariato.Text = ""
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub

                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.TAB_COMMISSARIATI (ID,DESCRIZIONE) VALUES (SISCOM_MI.SEQ_TAB_COMMISSARIATI.NEXTVAL,'" & par.PulisciStrSql(Me.txtCommissariato.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()
                    Try
                        Dim operatore As String = Session.Item("ID_OPERATORE")
                        Eventi_Gestione(operatore, "F55", "INSERIMENTO COMMISSARIATO " & txtCommissariato.Text)
                    Catch ex As Exception
                        lblErrore.Text = ex.Message
                        lblErrore.Visible = True
                    End Try
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid()
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Campo obbligatorio!')</script>")
                End If
                Me.txtCommissariato.Text = ""
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        txtCommissariato.Text = ""
        Me.txtid.Value = ""
        Me.txtmia.Text = "Nessuna Selezione"
        TextBox1.Value = "0"
    End Sub

    Protected Sub ImgBtnAggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.TAB_COMMISSARIATI WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()

                stringa = txtmia.Text
                NomeCommissariato()
                Try
                    Dim operatore As String = Session.Item("ID_OPERATORE")
                    Eventi_Gestione(operatore, "F56", "CANCELLAZIONE COMMISSARIATO" & stringa)
                Catch ex As Exception
                    lblErrore.Text = ex.Message
                    lblErrore.Visible = True
                End Try
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
                lblErrore.Text = "Commissariato in uso. Non è possibile eliminare!"
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

    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value.ToString <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT  DESCRIZIONE FROM SISCOM_MI.TAB_COMMISSARIATI WHERE ID = " & Me.txtid.Value

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtCommissariato.Text = myReader("DESCRIZIONE").ToString
            End If
            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"

        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")

        End If
    End Sub
    Private Sub Update()
        Try

            If par.IfEmpty(Me.txtCommissariato.Text, "Null") <> "Null" Then
                ''*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_COMMISSARIATI WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(Me.txtCommissariato.Text.ToUpper) & "' AND ID <>" & txtid.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Valore già inserito!')</script>")
                    Me.txtCommissariato.Text = ""
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.TAB_COMMISSARIATI SET DESCRIZIONE = '" & par.PulisciStrSql(Me.txtCommissariato.Text.ToUpper) & "' WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()

                stringa = txtmia.Text
                NomeCommissariato()
                Try
                    Dim operatore As String = Session.Item("ID_OPERATORE")
                    Eventi_Gestione(operatore, "F02", "MODIFICA COMMISSARIATO" & stringa)
                Catch ex As Exception
                    lblErrore.Text = ex.Message
                    lblErrore.Visible = True
                End Try

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                par.cmd.Dispose()
                BindGrid()
                Me.TextBox1.Value = "0"
                Me.txtid.Value = ""
                Me.txtmia.Text = "Nessuna Selezione"
                Me.txtCommissariato.Text = ""

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
End Class
