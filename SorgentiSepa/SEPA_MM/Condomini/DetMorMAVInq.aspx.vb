
Partial Class Condomini_DetMorMAVInq
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property vIdConnModale() As String

        Get
            If Not (ViewState("par_vIdConnModale") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnModale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnModale") = value
        End Set

    End Property

    Public Property vIdMorosita() As String
        Get
            If Not (ViewState("par_vIdMorosita") Is Nothing) Then
                Return CStr(ViewState("par_vIdMorosita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdMorosita") = value
        End Set

    End Property

    Public Property vIdAnagrafica() As String

        Get
            If Not (ViewState("par_vIdAnagrafica") Is Nothing) Then
                Return CStr(ViewState("par_vIdAnagrafica"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdAnagrafica") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack Then

            Try

                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                End If

                If Request.QueryString("IDCON") <> "" Then
                    vIdConnModale = Request.QueryString("IDCON")
                End If

                If Request.QueryString("IDINQ") <> "" And Request.QueryString("IDMOR") <> "" Then
                    vIdAnagrafica = Request.QueryString("IDINQ")
                    vIdMorosita = Request.QueryString("IDMOR")
                    CaricaBollettini()
                End If


            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try

        End If



    End Sub


    Private Sub CaricaBollettini()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.cmd.CommandText = "SELECT COND_MOROSITA_LETTERE.ID, COND_MOROSITA_LETTERE.BOLLETTINO, TO_CHAR(TO_DATE(COND_MOROSITA_LETTERE.DATA_NOTIFICA_COMUNE,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_NOTIFICA_COMUNE, trim(TO_CHAR(COND_MOROSITA_LETTERE.IMPORTO,'9G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE ID_ANAGRAFICA = " & vIdAnagrafica & " AND ID_MOROSITA = " & vIdMorosita & ""
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                DataGridDettMorMav.DataSource = dt
                DataGridDettMorMav.DataBind()

                i = 0
                di = Nothing
                For i = 0 To Me.DataGridDettMorMav.Items.Count - 1
                    di = Me.DataGridDettMorMav.Items(i)
                    If Session.Item("MOD_CONDOMINIO_MOR") = 1 Then
                        DirectCast(di.Cells(1).FindControl("txtDataNotifica"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                        btnSalvaDataNotifica.Visible = True
                    Else
                        DirectCast(di.Cells(1).FindControl("txtDataNotifica"), TextBox).Enabled = False
                    End If
                Next
            Else
                Response.Write("<script>alert('Nessun Bollettino MAV generato per questo inquilino!');self.close();</script>")
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnSalvaDataNotifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaDataNotifica.Click
        Try

            aggiornaDataNotifica()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


    Private Sub aggiornaDataNotifica()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT COND_MOROSITA_LETTERE.ID, COND_MOROSITA_LETTERE.BOLLETTINO, COND_MOROSITA_LETTERE.DATA_NOTIFICA_COMUNE, trim(TO_CHAR(COND_MOROSITA_LETTERE.IMPORTO,'9G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE ID_ANAGRAFICA = " & vIdAnagrafica & " AND ID_MOROSITA = " & vIdMorosita & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader.Read
                For i = 0 To Me.DataGridDettMorMav.Items.Count - 1
                    di = Me.DataGridDettMorMav.Items(i)
                    If myReader("ID") = Me.DataGridDettMorMav.Items(i).Cells(0).Text Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA_LETTERE SET COND_MOROSITA_LETTERE.DATA_NOTIFICA_COMUNE= '" & par.FormatoDataDB(DirectCast(di.Cells(1).FindControl("txtDataNotifica"), TextBox).Text) & "' WHERE ID= " & myReader("ID")
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            End While


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Try

            Response.Write("<script>window.close();</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

End Class
