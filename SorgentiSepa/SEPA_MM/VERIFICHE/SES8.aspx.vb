Imports System.IO
Partial Class AMMSEPA_Controllo_Select
    Inherits PageSetIdMode
    Dim OracleConn As Oracle.DataAccess.Client.OracleConnection
    Dim cmd As New Oracle.DataAccess.Client.OracleCommand()
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
                ButtonEstrai.Visible = True
                lblNumResult.Visible = True
                Me.TextBox2.Focus()
            End If
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            OracleConn = New Oracle.DataAccess.Client.OracleConnection(PAR.StringaSiscom)
            Dim sComando As String = TextBox2.Text

            OracleConn.Open()
            cmd = OracleConn.CreateCommand()

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable

            da = New Oracle.DataAccess.Client.OracleDataAdapter(sComando, OracleConn)
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
        Dim nomeFile As String = PAR.EsportaExcelAutomaticoDaGridViewAutogenerato(GridView1, "SelectExport", False, False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub

    Protected Sub btnExport0_Click(sender As Object, e As System.EventArgs) Handles btnExport0.Click
        Dim nomeFile As String = PAR.EsportaExcelAutomaticoDaGridViewAutogenerato(GridView1, "SelectExport", , False)
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

    Protected Sub ButtonEstrai_Click(sender As Object, e As System.EventArgs) Handles ButtonEstrai.Click
        Try
            Dim sComando As String = TextBox2.Text
            PAR.OracleConn.Open()
            PAR.cmd = PAR.OracleConn.CreateCommand()
            PAR.cmd.CommandText = "SELECT SISCOM_MI.SEQ_REPORT.NEXTVAL FROM DUAL"
            Dim idReport As Integer = PAR.cmd.ExecuteScalar

            If Len(sComando) < 4000 Then
                PAR.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                & " INIZIO, FINE, ESITO,  " _
                & " Q1,  " _
                & " PARAMETRI, PARZIALE, TOTALE,  " _
                & " ERRORE, NOMEFILE,Q4)  " _
                & " VALUES (" & idReport & ", " _
                & " 0, " _
                & " 2, " _
                & Format(Now, "yyyyMMddHHmmss") & " , " _
                & "NULL, " _
                & "0, " _
                & "'" & TextBox2.Text.ToString.Replace("'", "''") & "', " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL,NULL)"
            Else
                PAR.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                & " INIZIO, FINE, ESITO,  " _
                & " Q1,  " _
                & " PARAMETRI, PARZIALE, TOTALE,  " _
                & " ERRORE, NOMEFILE,Q4)  " _
                & " VALUES (" & idReport & ", " _
                & " 0, " _
                & " 2, " _
                & Format(Now, "yyyyMMddHHmmss") & " , " _
                & "NULL, " _
                & "0, " _
                & "'', " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL,:TEXT_DATA)"
                Dim paramData As New Oracle.DataAccess.Client.OracleParameter
                With paramData
                    .Direction = Data.ParameterDirection.Input
                    .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
                    .ParameterName = "TEXT_DATA"
                    .Value = sComando
                End With

                PAR.cmd.Parameters.Add(paramData)
                paramData = Nothing
            End If

            PAR.cmd.ExecuteNonQuery()
            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Dim p As New System.Diagnostics.Process
            Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
            Dim dicParaConnection As New Generic.Dictionary(Of String, String)
            Dim sParametri As String = ""
            For i As Integer = 0 To elParameter.Length - 1
                Dim s As String() = elParameter(i).Split("=")
                If s.Length > 1 Then
                    dicParaConnection.Add(s(0), s(1))
                End If
            Next
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idReport
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Report.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()
        Catch ex As Exception
            lblNumResult.Visible = True
            lblNumResult.Text = ex.Message
        End Try
    End Sub



End Class
