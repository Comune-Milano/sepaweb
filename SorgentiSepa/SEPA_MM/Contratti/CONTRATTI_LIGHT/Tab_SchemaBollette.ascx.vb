
Partial Class Contratti_Tab_SchemaBollette
    Inherits UserControlSetIdMode


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub





    'Private Sub CaricaSchema()
    '    Try
    '        Dim LL As Integer = 0
    '        'CARICAMENTO schema bollette

    '        'MODIFICATO ORDINE DI VISUALIZZAZIONE VOCI (PER IMPORTO ASC)
    '        PAR.cmd.CommandText = "select bol_schema.*,t_voci_bolletta.descrizione from SISCOM_MI.t_voci_bolletta, SISCOM_MI.bol_schema where t_voci_bolletta.id=bol_schema.id_voce and bol_schema.id_contratto=" & txtIdContratto.Value & " and anno=" & CType(Me.Page, Object).sAnnoSchema & " order by t_voci_bolletta.descrizione,(-importo) ASC"
    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
    '        Dim ds As New Data.DataSet()
    '        da.Fill(ds, "BOL_SCHEMA")
    '        DataGridSchema.DataSource = ds
    '        DataGridSchema.DataBind()


    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '        Session.Item("LAVORAZIONE") = "0"
    '        PAR.myTrans.Rollback()
    '        PAR.OracleConn.Close()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub


End Class
