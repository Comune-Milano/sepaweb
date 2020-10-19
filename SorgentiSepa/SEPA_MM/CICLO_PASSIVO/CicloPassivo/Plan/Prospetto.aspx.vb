
Partial Class Contabilita_CicloPassivo_Plan_Prospetto
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Carica()

        End If
    End Sub
    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)



            par.cmd.CommandText = "select * from SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE SUBSTR(FINE,1,4)>='2011' and SUBSTR(FINE,1,4)<=" & Year(Now) + 1 & " AND ID NOT IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN) ORDER BY ID DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                cmbEsercizio.Items.Add(New ListItem(par.FormattaData(myReader1("INIZIO")) & " - " & par.FormattaData(myReader1("FINE")), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            'par.cmd.CommandText = "select * from siscom_mi.pf_main where id_esercizio_finanziario=" & par.RicavaEsercizioCorrente()
            'myReader1 = par.cmd.ExecuteReader()
            'If myReader1.Read Then
            '    If myReader1("id_stato") <> "5" Then
            '        cmbEsercizio.Enabled = False
            '        ImgProcedi.Visible = False
            '        Response.Write("<script>alert('Non è possibile creare un nuovo piano finanziario. Il piano finanziario attuale non è stato approvato.');</script>")
            '    End If
            'End If
            'myReader1.Close()

            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("BP_FORMALIZZAZIONE_L"), "1") = "1" Then
                    cmbEsercizio.Enabled = False
                    ImgProcedi.Visible = False
                    CheckBox1.Enabled = False
                End If
            End If
            myReader1.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImgProcedi.Click
        If prosegui.Value = "1" Then
            If CheckBox1.Checked = True Then
                Response.Redirect("Prospetto1.aspx?P=-1&S=1&PERIODO=" & par.Cripta(cmbEsercizio.SelectedItem.Text) & "&ID=" & par.Cripta(cmbEsercizio.SelectedItem.Value))
            Else
                Response.Redirect("Prospetto1.aspx?P=-1&S=0&PERIODO=" & par.Cripta(cmbEsercizio.SelectedItem.Text) & "&ID=" & par.Cripta(cmbEsercizio.SelectedItem.Value))
            End If
        End If
    End Sub

    Private Sub imgEsci_Click(sender As Object, e As EventArgs) Handles imgEsci.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
End Class
