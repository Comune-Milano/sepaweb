Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class VSA_Graduatoria
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()
            CaricaGraduatoria()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox7').value='Hai selezionato il PG: " & e.Item.Cells(3).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox7').value='Hai selezionato il PG: " & e.Item.Cells(3).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            CaricaGraduatoria()
        End If
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If LBLID.Value <> "" And LBLID.Value <> "-1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "update domande_bando_vsa set id_stato='9' where id=" & LBLID.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione Effettuata. Pronto per essere invitato!');</script>")
                'BindGrid()

            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label8.Visible = True
                Label8.Text = ex.Message
            End Try
        Else
            Response.Write("<script>alert('Nessun nominativo selezionato!');</script>")
        End If
    End Sub

    Private Sub CaricaGraduatoria()
        Try
            Dim strArt22 As String = ""
            Dim dtRisult As New Data.DataTable

            par.OracleConn.Open()
            par.SettaCommand(par)

            dtRisult.Columns.Add("ID_DOMANDA")
            dtRisult.Columns.Add("POS")
            dtRisult.Columns.Add("PUNTEGGIO")
            dtRisult.Columns.Add("PG")
            dtRisult.Columns.Add("DATA_PRES")
            dtRisult.Columns.Add("COGNOME")
            dtRisult.Columns.Add("NOME")
            dtRisult.Columns.Add("PIANO")
            dtRisult.Columns.Add("AI")
            dtRisult.Columns.Add("EA")
            dtRisult.Columns.Add("AA")
            dtRisult.Columns.Add("IV")
            dtRisult.Columns.Add("H")
            dtRisult.Columns.Add("AN")
            dtRisult.Columns.Add("CD")
            dtRisult.Columns.Add("FS")
            dtRisult.Columns.Add("PV")
            dtRisult.Columns.Add("BA")
            dtRisult.Columns.Add("ACC")
            dtRisult.Columns.Add("ANN")
            dtRisult.Columns.Add("HH")
            dtRisult.Columns.Add("NE")

            strArt22 = "SELECT domande_bando_vsa.ID as id_domanda,DOMANDE_BANDO_VSA.PG, COGNOME,NOME,SUM (punteggio) AS punteggio,DATA_PRESENTAZIONE AS DATA_PRES,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO" _
                & " FROM tab_punti_emergenze, domande_bando_vsa_punti_em, domande_bando_vsa,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA,SISCOM_MI.TIPO_LIVELLO_PIANO,DOMANDE_VSA_ALLOGGIO " _
                & " WHERE domande_bando_vsa_punti_em.id_punteggio = tab_punti_emergenze.ID" _
                & " AND domande_bando_vsa_punti_em.id_domanda = domande_bando_vsa.ID" _
                & " AND comp_nucleo_vsa.id_dichiarazione = dichiarazioni_vsa.ID " _
                & " AND domande_bando_vsa.id_dichiarazione = dichiarazioni_vsa.ID AND COMP_NUCLEO_VSA.PROGR=0" _
                & " AND (domande_bando_vsa.id_stato = '8' OR domande_bando_vsa.id_stato = '9')" _
                & " AND domande_bando_vsa.id_motivo_domanda = 4 AND TIPO_LIVELLO_PIANO.COD=DOMANDE_VSA_ALLOGGIO.PIANO AND DOMANDE_VSA_ALLOGGIO.ID_DOMANDA=DOMANDE_BANDO_VSA.ID " _
                & " GROUP BY domande_bando_vsa.ID,domande_bando_vsa.pg,DATA_PRESENTAZIONE,NOME,COGNOME,TIPO_LIVELLO_PIANO.DESCRIZIONE" _
                & " ORDER BY punteggio DESC,DATA_PRESENTAZIONE ASC"

            Dim daE As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtE As New Data.DataTable
            daE = New Oracle.DataAccess.Client.OracleDataAdapter(strArt22, par.OracleConn)

            daE.Fill(dtE)
            daE.Dispose()

            Dim ROWnew As System.Data.DataRow
            Dim posizione As Integer = 1
            Dim strCategorie As String = ""
            For Each row1 As Data.DataRow In dtE.Rows
                ROWnew = dtRisult.NewRow()

                ROWnew.Item("ID_DOMANDA") = row1.Item("ID_DOMANDA")
                ROWnew.Item("POS") = posizione
                ROWnew.Item("PUNTEGGIO") = row1.Item("PUNTEGGIO")
                ROWnew.Item("PG") = row1.Item("PG")
                ROWnew.Item("DATA_PRES") = par.FormattaData(row1.Item("DATA_PRES"))
                ROWnew.Item("COGNOME") = row1.Item("COGNOME")
                ROWnew.Item("NOME") = row1.Item("NOME")
                ROWnew.Item("PIANO") = row1.Item("PIANO")

                strCategorie = "SELECT DISTINCT COD FROM tab_punti_emergenze,domande_bando_vsa_punti_em WHERE domande_bando_vsa_punti_em.id_punteggio = tab_punti_emergenze.ID AND ID_DOMANDA=" & row1.Item("ID_DOMANDA")
                Dim daC As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtC As New Data.DataTable
                daC = New Oracle.DataAccess.Client.OracleDataAdapter(strCategorie, par.OracleConn)

                daC.Fill(dtC)
                daC.Dispose()
                Dim codCategoria As String = ""
                For Each rowC As Data.DataRow In dtC.Rows
                    codCategoria = rowC.Item("COD")
                    If codCategoria = "AN+" Then
                        codCategoria = "ANN"
                    End If
                    If codCategoria = "H+" Then
                        codCategoria = "HH"
                    End If
                    ROWnew.Item(codCategoria) = "SI"
                Next
                If par.IfNull(ROWnew.Item("AI"), "") = "" Then
                    ROWnew.Item("AI") = "NO"
                End If

                If par.IfNull(ROWnew.Item("EA"), "") = "" Then
                    ROWnew.Item("EA") = "NO"
                End If

                If par.IfNull(ROWnew.Item("AA"), "") = "" Then
                    ROWnew.Item("AA") = "NO"
                End If

                If par.IfNull(ROWnew.Item("IV"), "") = "" Then
                    ROWnew.Item("IV") = "NO"
                End If

                If par.IfNull(ROWnew.Item("H"), "") = "" Then
                    ROWnew.Item("H") = "NO"
                End If

                If par.IfNull(ROWnew.Item("AN"), "") = "" Then
                    ROWnew.Item("AN") = "NO"
                End If

                If par.IfNull(ROWnew.Item("CD"), "") = "" Then
                    ROWnew.Item("CD") = "NO"
                End If

                If par.IfNull(ROWnew.Item("FS"), "") = "" Then
                    ROWnew.Item("FS") = "NO"
                End If

                If par.IfNull(ROWnew.Item("PV"), "") = "" Then
                    ROWnew.Item("PV") = "NO"
                End If

                If par.IfNull(ROWnew.Item("BA"), "") = "" Then
                    ROWnew.Item("BA") = "NO"
                End If

                If par.IfNull(ROWnew.Item("ACC"), "") = "" Then
                    ROWnew.Item("ACC") = "NO"
                End If

                If par.IfNull(ROWnew.Item("ANN"), "") = "" Then
                    ROWnew.Item("ANN") = "NO"
                End If

                If par.IfNull(ROWnew.Item("HH"), "") = "" Then
                    ROWnew.Item("HH") = "NO"
                End If

                If par.IfNull(ROWnew.Item("NE"), "") = "" Then
                    ROWnew.Item("NE") = "NO"
                End If

                dtRisult.Rows.Add(ROWnew)

                posizione = posizione + 1
            Next

            DataGrid1.DataSource = dtRisult
            DataGrid1.DataBind()
            Label7.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dtRisult.Rows.Count
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("MIADT", dtRisult)

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label8.Visible = True
            Label8.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            If DT.Rows.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(DT, DataGrid1, "ExportGraduatoria", , False, , False)

                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Redirect("../FileTemp/" & nomefile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception

        End Try
        'Try
        '    Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.DataGrid1, "ExportGraduatArt22", , , , False)
        '    If System.IO.File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
        '        Response.Redirect("../FileTemp/" & nomefile, False)
        '    Else
        '        Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        '    End If

        'Catch ex As Exception
        '    Label8.Visible = True
        '    Label8.Text = ex.Message
        'End Try
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
