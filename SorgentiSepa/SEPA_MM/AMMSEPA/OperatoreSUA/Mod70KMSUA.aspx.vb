
Partial Class AMMSEPA_OperatoreSUA_Mod70KMSUA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
            Response.Cache.SetLastModified(DateTime.Now)
            Response.Cache.SetAllowResponseInBrowserHistory(False)
            If Not IsPostBack Then
                If Request.QueryString("id") = 1 Then
                    CaricaValori()
                End If
            End If
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub

    Private Sub CaricaValori()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT id,cod,nome,sigla,cap,entro_70km,distanza_km,popolazione FROM comuni_nazioni where id=" & Request.QueryString("txtid")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                TextBox1.Text = par.IfNull(lettore("NOME"), "")
                TextBox2.Text = par.IfNull(lettore("SIGLA"), "")
                TextBox3.Text = par.IfNull(lettore("CAP"), "")
                TextBox4.Text = par.IfNull(lettore("COD"), "")
                TextBox5.Text = par.IfNull(lettore("DISTANZA_KM"), "")
                DropDownList1.SelectedValue = par.IfNull(lettore("ENTRO_70KM"), "0")
                TxtPopolazione.Text = par.IfNull(lettore("POPOLAZIONE"), "0")
            End If
            lettore.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        If Val(TextBox5.Text) <> 0 Or TextBox5.Text = "0" Then
            Try

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & Request.QueryString("txtid") & " FOR UPDATE NOWAIT"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    par.cmd.CommandText = "UPDATE comuni_nazioni set entro_70km='" & par.PulisciStrSql(DropDownList1.SelectedValue) & "',DISTANZA_KM=" & Val(TextBox5.Text) & ",POPOLAZIONE=" & Val(TxtPopolazione.Text) & " where id=" & Request.QueryString("txtid")
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderA.Close()

                Try
                    Dim operatore As String = Session.Item("ID_OPERATORE")
                    Eventi_Gestione(operatore, "F02", "AGGIORNAMENTO CAMPO ENTRO_70KM DA MILANO COMUNE/DISTANZA KM " & TextBox1.Text)
                Catch ex As Exception
                    lblErrore.Text = ex.Message
                    lblErrore.Visible = True
                End Try
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>self.close();</script>")
            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    lblErrore.Text = EX1.Message
                    lblErrore.Visible = True
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    lblErrore.Text = EX1.Message
                    lblErrore.Visible = True
                End If
            Catch ex As Exception
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        Else
            lblErrore.Visible = True
            lblErrore.Text = "Inserire un numero senza decimali"
        End If
    End Sub

    Protected Sub ImgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEsci.Click
        Try
            Response.Write("<script>self.close();</script>")
        Catch ex As Exception

        End Try
    End Sub

    Function Eventi_Gestione(ByVal operatore As String, ByVal cod_evento As String, ByVal motivazione As String) As Integer
        Dim data As String = Format(Now, "yyyyMMddHHmmss")
        Try
            par.cmd.CommandText = "INSERT INTO EVENTI_GESTIONE (ID_OPERATORE, COD_EVENTO, DATA_ORA, MOTIVAZIONE) VALUES" _
                            & " (" & operatore & ",'" & cod_evento & "'," & data & ",'" & par.PulisciStrSql(motivazione) & "')"
            PAR.cmd.ExecuteNonQuery()
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
        Return 0
    End Function
End Class
