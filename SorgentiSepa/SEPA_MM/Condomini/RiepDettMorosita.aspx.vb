
Partial Class Condomini_RiepDettMorosita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        If Not IsPostBack Then
            CaricaRiepilogo()
        End If
    End Sub

    Private Sub CaricaRiepilogo()

        Try
            Dim idConn As String = Request.QueryString("IDCON")


            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & idConn), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & idConn), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT id_morosita, to_char(to_date(DATA,'yyyymmdd'),'dd/mm/yyyy') AS DATA,id_intestatario,trim(TO_CHAR(IMPORTO,'999G999G999G990D99')) as IMPORTO,NOTE,id_ui, " _
                                & "(CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (cognome || ' ' || nome))END) AS inquilino " _
                                & "FROM siscom_mi.COND_MOROSITA_INQUILINI_DET,siscom_mi.ANAGRAFICA " _
                                & "WHERE id_morosita = " & Request.QueryString("IDMOR") & " AND id_intestatario = " & Me.Request.QueryString("IDINQ") & " and ID_UI = " & Request.QueryString("IDUI") _
                                & "AND ANAGRAFICA.ID = id_intestatario"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()

            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Me.lblTitolo.Text = "Dettaglio di " & dt.Rows(0).Item("INQUILINO")
            End If
            DgvDettInqMorosita.DataSource = dt

            DgvDettInqMorosita.DataBind()




        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Sub
End Class
