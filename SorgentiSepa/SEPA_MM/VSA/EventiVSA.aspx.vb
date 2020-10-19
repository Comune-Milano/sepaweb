
Partial Class VSA_EventiVSA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:300px; left:750px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            CaricaEventi()
            Response.Flush()
        End If
    End Sub

    Private Sub CaricaEventi()
        Try
            Dim idDomanda As String = ""
            Dim dt As New Data.DataTable

            par.OracleConn.Open()
            par.SettaCommand(par)

            idDomanda = Request.QueryString("IDDOM")
            par.cmd.CommandText = "select TO_DATE(EVENTI_BANDI_VSA.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
                  & " tab_eventi.cod ||' - '|| tab_eventi.descrizione as descrizione,EVENTI_BANDI_VSA.COD_EVENTO,EVENTI_BANDI_VSA.MOTIVAZIONE," _
                  & " OPERATORI.OPERATORE,EVENTI_BANDI_VSA.ID_OPERATORE,CAF_WEB.COD_CAF " _
                  & " from CAF_WEB, EVENTI_BANDI_VSA,TAB_EVENTI,OPERATORI,TAB_STATI " _
                  & " where EVENTI_BANDI_VSA.ID_DOMANDA= " & idDomanda _
                  & " and EVENTI_BANDI_VSA.COD_EVENTO=TAB_EVENTI.COD (+) " _
                  & " and EVENTI_BANDI_VSA.ID_OPERATORE=OPERATORI.ID (+) " _
                  & " AND EVENTI_BANDI_VSA.STATO_PRATICA=TAB_STATI.COD (+)" _
                  & " and CAF_WEB.ID=OPERATORI.ID_CAF " _
                  & " order by EVENTI_BANDI_VSA.DATA_ORA desc,EVENTI_BANDI_VSA.COD_EVENTO desc"

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            'par.cmd.CommandText = "SELECT * FROM T_TIPO_DECISIONI_VSA,VSA_DECISIONI_REV_C WHERE COD=COD_DECISIONE AND ID_DOMANDA=" & idDomanda
            'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                lblTotale.Text = "TOTALE EVENTI TROVATI: " & dt.Rows.Count
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
