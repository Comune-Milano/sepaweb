Imports System.IO
Partial Class AMMSEPA_Controllo1_Select
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GridView1.Visible = False
        If Not IsPostBack Then
            Me.TextBox1.Focus()
        Else
            Me.TextBox2.Focus()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If PAR.VerificaPW(TextBox1.Text) = True Then
                Button2.Visible = True
                TextBox2.Visible = True
                btnExport.Visible = True
                btnExport0.Visible = True
                lblNumResult.Visible = True
                Me.TextBox2.Focus()
            End If
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim sComando As String = TextBox2.Text

            PAR.cmd.CommandText = sComando

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable

            da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                GridView1.Visible = True
                GridView1.DataSource = dt
                GridView1.DataBind()
                Me.lblNumResult.Text = " Numero righe: " & GridView1.Rows.Count
            Else
                Me.lblNumResult.Text = " Numero righe: 0"
            End If

            da.Dispose()
            da = Nothing
            dt.Clear()
            dt.Dispose()
            dt = Nothing


            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            Me.lblNumResult.Text = " Numero righe: 0"

            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = PAR.EsportaExcelAutomaticoDaGridViewAutogenerato(GridView1, "SelectExport", False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub

    Protected Sub btnExport0_Click(sender As Object, e As System.EventArgs) Handles btnExport0.Click
        Dim nomeFile As String = PAR.EsportaExcelAutomaticoDaGridViewAutogenerato(GridView1, "SelectExport")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub GridView1_PreRender(sender As Object, e As System.EventArgs) Handles GridView1.PreRender
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
        GridView1.FooterRow.TableSection = TableRowSection.TableFooter
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                e.Row.Attributes.Add("class", "odd gradeA")
            Case Else
        End Select
    End Sub

End Class
