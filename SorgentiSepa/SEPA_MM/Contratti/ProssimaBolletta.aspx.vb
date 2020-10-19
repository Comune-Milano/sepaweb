
Partial Class Contratti_ProssimaBolletta
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
             Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            cmbAnno.Items.Add(Year(Now) - 1)
            cmbAnno.Items.Add(Year(Now))
            cmbAnno.Items.Add(Year(Now) + 1)
            
            Carica()
        End If
    End Sub

    Private Sub Carica()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(Request.QueryString("CN")), Oracle.DataAccess.Client.OracleConnection)
            par.cmd = par.OracleConn.CreateCommand()
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("CN")), Oracle.DataAccess.Client.OracleTransaction)


            par.cmd.CommandText = "select * FROM siscom_mi.RAPPORTI_UTENZA_PROSSIMA_BOL WHERE ID_CONTRATTO=" & Request.QueryString("ID")
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                cmbAnno.SelectedIndex = -1
                cmbAnno.Items.FindByText(par.IfNull(Mid(myReaderA("PROSSIMA_BOLLETTA"), 1, 4), Year(Now))).Selected = True
                cmbMese.SelectedIndex = -1
                cmbMese.Items.FindByValue(par.IfNull(Mid(myReaderA("PROSSIMA_BOLLETTA"), 5, 2), Format(Month(Now), "00"))).Selected = True
            End If
            myReaderA.Close()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(Request.QueryString("CN")), Oracle.DataAccess.Client.OracleConnection)
            par.cmd = par.OracleConn.CreateCommand()
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("CN")), Oracle.DataAccess.Client.OracleTransaction)


            par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza_prossima_bol where id_contratto=" & Request.QueryString("ID")
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.HasRows = True Then
                par.cmd.CommandText = "update siscom_mi.RAPPORTI_UTENZA_PROSSIMA_BOL set prossima_bolletta='" & cmbAnno.SelectedItem.Text & cmbMese.SelectedItem.Value & "' where id_contratto=" & Request.QueryString("ID")
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO siscom_mi.RAPPORTI_UTENZA_PROSSIMA_BOL (PROSSIMA_BOLLETTA,ID_CONTRATTO) VALUES ('" & cmbAnno.SelectedItem.Text & cmbMese.SelectedItem.Value & "'," & Request.QueryString("ID") & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderA.Close()

            par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & Request.QueryString("ID") & ",1, '" & Format(Now, "yyyyMMddHHmmss") & "', 'F02', ' CAMBIO DATA PROSSIMA BOLLETTA (" & cmbMese.SelectedItem.Value & "/" & cmbAnno.SelectedItem.Value & ")')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "update siscom_mi.rapporti_utenza set inizio_periodo='" & cmbAnno.SelectedItem.Text & cmbMese.SelectedItem.Value & "' where id=" & Request.QueryString("ID")
            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Operazione effettuata! Premere il pulsante SALVA del contratto!');self.close();</script>")
        Catch ex As Exception

        End Try
    End Sub
End Class
