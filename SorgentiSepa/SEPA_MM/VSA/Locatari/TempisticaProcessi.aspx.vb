
Partial Class VSA_Locatari_TempisticaProcessi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                BindGrid()

                If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
                    sololettura.Value = "1"
                    ImgModifica.Visible = False
                Else
                    sololettura.Value = "0"
                    ImgModifica.Visible = True
                End If

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Private Sub BindGrid()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim Str As String = ""
            Str = "select * from tempi_processi_vsa,t_motivo_domanda_vsa where tempi_processi_vsa.id_motivo_domanda = t_motivo_domanda_vsa.id order by descrizione asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridIntLegali.DataSource = dt
            DataGridIntLegali.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub img_InserisciSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            If Me.TextBox1.Value = 2 Then
                If par.IfEmpty(Me.txtDurata.Text, "Null") <> "Null" Then
                    ''*********************APERTURA CONNESSIONE**********************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    par.cmd.CommandText = "UPDATE tempi_processi_vsa SET TEMPO_GG = '" & par.PulisciStrSql(Me.txtDurata.Text.ToUpper) & "' WHERE ID_MOTIVO_DOMANDA = " & Me.txtid.Value
                    par.cmd.ExecuteNonQuery()

                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    par.cmd.Dispose()
                    BindGrid()
                    Me.TextBox1.Value = "0"
                    Me.txtid.Value = ""
                    Me.txtmia.Text = "Nessuna selezione"
                    Me.txtDurata.Text = ""
                    Response.Write("<script>alert('Operazione effettuata!');</script>")
                End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGridIntLegali_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntLegali.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub

    Protected Sub ImgModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value.ToString <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM TEMPI_PROCESSI_VSA,T_MOTIVO_DOMANDA_VSA WHERE ID_MOTIVO_DOMANDA = " & Me.txtid.Value & " AND T_MOTIVO_DOMANDA_VSA.ID=TEMPI_PROCESSI_VSA.ID_MOTIVO_DOMANDA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtDurata.Text = myReader("TEMPO_GG").ToString
                Me.lblDiv.Text = """" & myReader("DESCRIZIONE").ToString.ToLower & """"
            End If
            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"
        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If
    End Sub
End Class
