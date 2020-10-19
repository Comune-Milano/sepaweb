
Partial Class ANAUT_GestCongAU
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then
            Carica()
        End If
    End Sub

    Private Sub Carica()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            Dim INDICEBANDO As Long = 0
            idBando.Value = INDICEBANDO

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE stato=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblDescrBando.Text = "Stai Operando su: " & par.IfNull(myReader("descrizione"), "")
                INDICEBANDO = par.IfNull(myReader("ID"), 0)
                idBando.Value = INDICEBANDO
            Else
                lblDescrBando.Text = "Nessuna Anagrafe Utenza in corso..."
            End If
            myReader.Close()

            par.caricaComboBox("select * from SISCOM_MI.TIPO_BOLLETTE_GEST ORDER BY DESCRIZIONE ASC", cmbTipo, "id", "descrizione")
            par.caricaComboBox("select * from SISCOM_MI.t_voci_bolletta ORDER BY DESCRIZIONE ASC", cmbVoce, "id", "descrizione")

            If INDICEBANDO <> 0 Then
                par.cmd.CommandText = "select * from TAB_VOCI_CONG_AU where id_au=" & INDICEBANDO
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    cmbTipo.Items.FindByValue(myReader("ID_TIPO_GEST")).Selected = True
                    cmbVoce.Items.FindByValue(myReader("ID_VOCE")).Selected = True
                End If
                myReader.Close()
            End If

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

           

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            par.cmd.CommandText = "select * from TAB_VOCI_CONG_AU where id_au=" & idBando.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                par.cmd.CommandText = "update TAB_VOCI_CONG_AU set ID_TIPO_GEST=" & cmbTipo.SelectedItem.Value & ",ID_VOCE=" & cmbVoce.SelectedItem.Value & " where id_au=" & idBando.Value
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO TAB_VOCI_CONG_AU (ID_AU,ID_TIPO_GEST,ID_VOCE) VALUES (" & idBando.Value & "," & cmbTipo.SelectedItem.Value & "," & cmbVoce.SelectedItem.Value & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReader.Close()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Dim Str As String = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">alert('Operazione effettuata!');</script>"
            Response.Write(Str)

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub
End Class
