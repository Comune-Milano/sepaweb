
Partial Class Contratti_SfrattoEsecutivo
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDataConfFP.TextChanged

    End Sub
    Public Property vIdContratto() As String
        Get
            If Not (ViewState("par_vIdContratto") Is Nothing) Then
                Return CStr(ViewState("par_vIdContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdContratto") = value
        End Set

    End Property
    Private Sub CaricaDati()
        Try

            'APERTURA TRANSAZIONE DA FRM MASSIMILIANO CONTRATTO PRENDENDO L'ID CONNESSIONE DALLA PAGINA DEL CONTRATTO

            par.OracleConn = CType(HttpContext.Current.Session.Item(Request.QueryString("ID_CNS")), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("ID_CNS")), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.cmd.CommandText = "SELECT DATA_CONVALIDA_SFRATTO, DATA_ESECUZIONE_SFRATTO, DATA_RINVIO_SFRATTO, DATA_CONFERMA_FP FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & vIdContratto

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Me.txtDataConvSfratto.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_CONVALIDA_SFRATTO").ToString, ""))
                Me.txtDataRinvioSfratto.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_RINVIO_SFRATTO").ToString, ""))
                Me.TxtDataConfFP.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_CONFERMA_FP").ToString, ""))
                Me.txtDataEsecuzioneSfratto.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_ESECUZIONE_SFRATTO").ToString, ""))
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub Update()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item(Request.QueryString("ID_CNS")), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("ID_CNS")), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET DATA_CONVALIDA_SFRATTO = '" & par.AggiustaData(Me.txtDataConvSfratto.Text) & "', DATA_ESECUZIONE_SFRATTO= '" & par.AggiustaData(Me.txtDataEsecuzioneSfratto.Text) & "', DATA_RINVIO_SFRATTO = '" & par.AggiustaData(Me.txtDataRinvioSfratto.Text) & "', DATA_CONFERMA_FP = '" & par.AggiustaData(Me.TxtDataConfFP.Text) & "' WHERE RAPPORTI_UTENZA.ID =" & vIdContratto
            par.cmd.ExecuteNonQuery()
            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F22','')"
            par.cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub ImgButEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgButEsci.Click
        Response.Write("<script language='javascript'> { self.close() }</script>")
    End Sub

    Protected Sub ImgButSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgButSave.Click
        Update()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            vIdContratto = Request.QueryString("ID_CONTRATTO")
            CaricaDati()
            Me.TxtDataConfFP.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataConvSfratto.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataEsecuzioneSfratto.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRinvioSfratto.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If
    End Sub
End Class
