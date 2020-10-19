
Partial Class FSA_Eventi_Tab
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String

    Dim vId As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO EVENTI"

                vId = Request.QueryString("ID")

                sStringaSql = "select TO_DATE(SISCOM_MI.LOG.DATA_OPERAZIONE,'yyyyMMdd') as ""DATA_ORA""," _
                                  & " SISCOM_MI.LOG.TIPO_OPERAZIONE,SISCOM_MI.LOG.NOTE as OPERAZIONE," _
                                  & " SEPA.OPERATORI.OPERATORE,SISCOM_MI.LOG.ID_OPERATORE,SEPA.CAF_WEB.COD_CAF " _
                           & " from  SEPA.CAF_WEB,SISCOM_MI.LOG, SEPA.OPERATORI " _
                           & " where SISCOM_MI.LOG.TIPO_OGGETTO='" & vId & "'" _
                             & " and SISCOM_MI.LOG.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
                             & " and SEPA.CAF_WEB.ID=SEPA.OPERATORI.ID_CAF " _
                          & " order by DATA_ORA desc,TIPO_OPERAZIONE "


                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                lblTotale.Text = "0"
                Do While myReader.Read()
                    lblTotale.Text = CInt(lblTotale.Text) + 1
                Loop

                lblTotale.Text = "TOTALE EVENTI TROVATI: " & lblTotale.Text
                myReader.Close()

                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds)
                DataGrid1.DataSource = ds
                DataGrid1.DataBind()

                da.Dispose()
                ds.Dispose()
                '*******************************


                par.cmd.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End If

    End Sub



End Class
