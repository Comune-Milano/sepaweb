
Partial Class Contratti_RptVociBollSchema
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src=""../NuoveImm/load.gif"" alt='Caricamento in corso' ><br>Caricamento in corso...</br>"
        Str = Str & "</div>"

        Response.Write(Str)
        Response.Flush()

        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Try
            connData.apri()

            par.cmd.CommandText = "select '' as cod_contratto,'' as data_Ora,0 as anno,'' as nomevoce,0 as importo_vecchio,0 da_rata_old,0 per_rate_old,0 as importo_nuovo,0 da_rata_new,0 per_rate_new from dual"
            Dim daB1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            daB1.Fill(dt1)
            daB1.Dispose()

            Dim dtBolSchema As New Data.DataTable
            dtBolSchema = dt1.Clone

            par.cmd.CommandText = "select TO_CHAR(TO_DATE(DATA_ORA,'yyyyMMddHH24MISS'),'dd/mm/yyyy - HH24:mi') as dataOra,cod_contratto,id_contratto,anno,t_voci_bolletta.descrizione as nomevoce,bol_schema_storico.id_voce,to_char(importo_singola_rata,'999G999G990D99') as imp_sing_rata,to_char(importo_nuovo,'999G999G990D99') as imp_nuovo," _
                & " da_rata_old,per_rate_old,da_rata_new,per_rate_new from siscom_mi.bol_schema_storico,siscom_mi.t_voci_bolletta,siscom_mi.rapporti_utenza where t_voci_bolletta.id=bol_schema_storico.id_voce " _
                & " and bol_schema_storico.id_contratto=rapporti_utenza.id and anno=" & txtAnnoRiferim.Text & " order by id_contratto asc"
            Dim daB2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable
            daB2.Fill(dt2)
            daB2.Dispose()

            Dim row As Data.DataRow
            If dt2.Rows.Count > 0 Then
                For Each rowDati As Data.DataRow In dt2.Rows
                    row = dtBolSchema.NewRow()

                    row.Item("cod_contratto") = par.IfNull(rowDati.Item("cod_contratto"), "")
                    row.Item("anno") = par.IfNull(rowDati.Item("anno"), Year(Now))
                    row.Item("nomevoce") = par.IfNull(rowDati.Item("nomevoce"), "")
                    row.Item("importo_vecchio") = par.IfNull(rowDati.Item("imp_sing_rata"), 0)
                    row.Item("importo_nuovo") = par.IfNull(rowDati.Item("imp_nuovo"), 0)
                    row.Item("da_rata_new") = par.IfNull(rowDati.Item("da_rata_new"), 0)
                    row.Item("per_rate_new") = par.IfNull(rowDati.Item("per_rate_new"), 0)
                    row.Item("da_rata_old") = par.IfNull(rowDati.Item("da_rata_old"), 0)
                    row.Item("per_rate_old") = par.IfNull(rowDati.Item("per_rate_old"), 0)
                    row.Item("data_ora") = par.IfNull(rowDati.Item("dataOra"), 0)

                    'par.cmd.CommandText = "select * from siscom_mi.bol_schema where id_contratto=" & rowDati.Item("id_contratto") & " and id_voce=" & rowDati.Item("id_voce") & " and anno=" & txtAnnoRiferim.Text
                    'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    'If lettore.Read Then
                    '    row.Item("importo_nuovo") = par.IfNull(rowDati.Item("importo_singola_rata"), 0)
                    '    row.Item("da_rata") = par.IfNull(rowDati.Item("da_rata"), 0)
                    '    row.Item("per_rate") = par.IfNull(rowDati.Item("per_rate"), 0)
                    'End If
                    'lettore.Close()

                    dtBolSchema.Rows.Add(row)
                Next
            End If

            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ElVociSch", "ExportVociSchema", dtBolSchema)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If

            connData.chiudi()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Report Voci Schema - btnRicerca_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
