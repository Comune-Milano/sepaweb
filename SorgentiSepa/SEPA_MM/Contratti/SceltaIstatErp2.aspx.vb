
Partial Class Contratti_SceltaIstatErp2
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                Dim Str As String
                'Dim totaleRiga As Double
                'Dim TotAffitto As Double
                'Dim TotSpese As Double
                'Dim TotRegCont As Double
                'Dim TotSuTotRiga As Double
                Dim I = 0

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()
                NomeFile = "ApplicazioneISTAT"

                '********CONNESSIONE*********
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader

                par.cmd.CommandText = CType(HttpContext.Current.Session.Item("BB"), String)


                'Dim row As System.Data.DataRow
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt2 As New Data.DataTable

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    DataGridRateEmesse.DataSource = dt
                    DataGridRateEmesse.DataBind()
                    par.OracleConn.Close()

                    HttpContext.Current.Session.Add("AA", dt)
                    imgExcel.Attributes.Add("onclick", "javascript:window.open('Report/DownLoad.aspx?CHIAMA=100','Distinta','');")

                Else
                    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")
                    Response.Write("<script language='javascript'> { self.close() }</script>")
                End If





            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Private Property NomeFile() As String
        Get
            If Not (ViewState("par_NomeFile") Is Nothing) Then
                Return CStr(ViewState("par_NomeFile"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_NomeFile") = value
        End Set

    End Property
End Class
