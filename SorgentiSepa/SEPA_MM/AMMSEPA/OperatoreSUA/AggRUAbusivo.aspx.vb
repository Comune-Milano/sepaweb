
Partial Class AMMSEPA_OperatoreSUA_AggRUAbusivo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub ImgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub
    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        If cerca.Value = 1 Then
            Try
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO = '" & par.PulisciStrSql(txtcontratto.Text.ToString.ToUpper) & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE ID_CONTRATTO = " & par.IfNull(myReader("ID"), 0)
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        Response.Write("<script>alert('Il contratto è gia\' presente nella lista!!');</script>")
                    Else
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI (ID_CONTRATTO) VALUES (" & par.IfNull(myReader("ID"), 0) & ")"
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione Completata!!');self.close();</script>")
                    End If
                    myReader2.Close()
                Else
                    Response.Write("<script>alert('Il contratto inserito non e\' valido')</script>")
                End If
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                cerca.Value = 0
                lblintestatario.Text = ""
                lblintestatario.Visible = False
            Catch ex1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    Response.Write("<script>alert('Non è possibile inserire il dato in questo momento. Riprovare più tardi!')</script>")
                Else
                    Response.Write("<script>alert('Il contratto inserito non e\' valido')</script>")
                End If
                If Data.ConnectionState.Open = True Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
                Response.Redirect("../../Errore.aspx", False)
            End Try
        Else
            Response.Write("<script>alert('Devi ricercare l\'intestatario prima di poter salvare')</script>")
            cerca.Value = 0
            lblintestatario.Text = ""
            lblintestatario.Visible = False
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            SettaControlModifiche(Me)
        End If
    End Sub
    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('cerca').value='0';")
            End If
        Next
    End Sub
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO = '" & par.PulisciStrSql(txtcontratto.Text.ToString.ToUpper) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT ID_ANAGRAFICA FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND ID_CONTRATTO = " & par.IfNull(myReader("ID"), 0)
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    par.cmd.CommandText = "SELECT (CASE WHEN siscom_mi.anagrafica.cognome IS NULL AND siscom_mi.anagrafica.NOME IS NULL THEN siscom_mi.anagrafica.RAGIONE_SOCIALE ELSE siscom_mi.anagrafica.cognome || ' ' ||siscom_mi.anagrafica.NOME END) AS INTESTATARIO " _
                                        & "FROM SISCOM_MI.ANAGRAFICA " _
                                        & "WHERE ID = " & par.IfNull(myReader2("ID_ANAGRAFICA"), 0)
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        lblintestatario.Text = "Intestatario: " & par.IfNull(myReader3("INTESTATARIO"), "")
                        lblintestatario.Visible = True
                        cerca.Value = 1
                    End If
                    myReader3.Close()
                End If
                myReader2.Close()
            Else
                Response.Write("<script>alert('Il contratto inserito non e\' valido')</script>")
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                Response.Write("<script>alert('Non è possibile effettuare operazioni in questo momento. Riprovare più tardi!')</script>")
            Else
                Response.Write("<script>alert('Il contratto inserito non e\' valido')</script>")
            End If
            If Data.ConnectionState.Open = True Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
End Class
