Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class PED_ElencoUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim CodContratto As String
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
                If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            caricaDati()
            Response.Flush()
        End If

    End Sub

    Private Sub caricaDati()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim RIGA As System.Data.DataRow
            Dim RIGA2 As System.Data.DataRow

            par.cmd.CommandText = Session.Item("EUI")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Dim miocolore As String = "#E6E6E6"
            Dim CodContrattoAttuale As String = ""
            Dim strIntest As String = ""

            strIntest = "<p><font face='Arial'><b>" & Session.Item("TITOLO") & "</b></br></font></p>"
            strIntest &= "<p><font face='Arial' size='2'>Data Estrazione <b>" & Format(Now, "dd/MM/yyyy") & "</b></font></p>"

            dt.Columns.Add("COD_UI")
            dt.Columns.Add("FOGLIO")
            dt.Columns.Add("NUMERO")
            dt.Columns.Add("SUB")
            dt.Columns.Add("PIANO")
            dt.Columns.Add("SCALA")
            dt.Columns.Add("INTERNO")
            dt.Columns.Add("CIVICO")
            dt.Columns.Add("TIPOLOGIA")
            dt.Columns.Add("SUP_CONV")
            dt.Columns.Add("SUP_NETTA")
            dt.Columns.Add("DISP")
            dt.Columns.Add("OCCUPANTE")
            dt.Columns.Add("DATA_NASCITA")
            dt.Columns.Add("LUOGO_NASCITA")
            dt.Columns.Add("DESCR_OCC")
            dt.Columns.Add("RAPP_CONTR")
            dt.Columns.Add("TIPO_CONTR")
            dt.Columns.Add("DECORRENZA")
            dt.Columns.Add("DATA_SLOGGIO")

            CodContratto = "-1"
            Do While myReader.Read()

                CodContratto = par.IfNull(myReader("cod_unita_immobiliare"), "")

                'Riga DataTable
                RIGA = dt.NewRow()

                RIGA.Item("COD_UI") = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(myReader("Cod_unita_immobiliare"), "") & "','', 'height=580,width=780');" & Chr(34) & ">" & par.IfNull(myReader("Cod_unita_immobiliare"), "") & "</a>"
                RIGA.Item("SUB") = par.IfNull(myReader("sub"), "")
                RIGA.Item("FOGLIO") = par.IfNull(myReader("FOGLIO"), "")
                RIGA.Item("NUMERO") = par.IfNull(myReader("NUMERO"), "")
                RIGA.Item("PIANO") = par.IfNull(myReader("piano"), "")
                RIGA.Item("SCALA") = par.IfNull(myReader("scala"), "")
                RIGA.Item("INTERNO") = par.IfNull(myReader("INTERNO"), "")
                RIGA.Item("CIVICO") = par.IfNull(myReader("CIVICO"), "")
                RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")

                par.cmd.CommandText = "select DIMENSIONI.VALORE AS ""SUP_CONV"" from SISCOM_MI.DIMENSIONI where (DIMENSIONI.COD_TIPOLOGIA='SUP_CONV' OR DIMENSIONI.COD_TIPOLOGIA='SUP CONV' ) and DIMENSIONI.ID_UNITA_IMMOBILIARE =" & myReader("ID")
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("SUP_CONV") = par.IfNull(myReader2("SUP_CONV"), "")
                Else
                    RIGA.Item("SUP_CONV") = ""
                End If
                myReader2.Close()

                par.cmd.CommandText = "select DIMENSIONI.VALORE AS ""SUP_NETTA"" from SISCOM_MI.DIMENSIONI where (DIMENSIONI.COD_TIPOLOGIA='SUP_NETTA') and DIMENSIONI.ID_UNITA_IMMOBILIARE =" & myReader("ID")
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("SUP_NETTA") = par.IfNull(myReader2("SUP_NETTA"), "")
                Else
                    RIGA.Item("SUP_NETTA") = ""
                End If
                myReader2.Close()


                RIGA.Item("DISP") = par.IfNull(myReader("DISPONIBILITA"), "")

                par.cmd.CommandText = "select rapporti_utenza.cod_contratto,tipologia_rapp_contrattuale.descrizione as ""TipoRapporto"",rapporti_utenza.cod_tipologia_contr_loc as ""TipoContratto"",RAPPORTI_UTENZA.DATA_DECORRENZA,anagrafica.cognome,anagrafica.nome,anagrafica.ragione_sociale,anagrafica.data_nascita,comuni_nazioni.nome as COMUNE_NASCITA,tipologia_occupante.descrizione as ""TipoOccupante"",rapporti_utenza.data_riconsegna from COMUNI_NAZIONI,SISCOM_MI.tipologia_occupante,SISCOM_MI.tipologia_rapp_contrattuale,SISCOM_MI.anagrafica,SISCOM_MI.soggetti_contrattuali,SISCOM_MI.rapporti_utenza,SISCOM_MI.unita_contrattuale,SISCOM_MI.unita_immobiliari where anagrafica.cod_comune_nascita=COMUNI_NAZIONI.COD (+) AND soggetti_contrattuali.cod_tipologia_occupante=tipologia_occupante.cod (+) and rapporti_utenza.cod_tipologia_rapp_contr=tipologia_rapp_contrattuale.cod (+) and rapporti_utenza.id=soggetti_contrattuali.id_contratto (+) and soggetti_contrattuali.id_anagrafica=anagrafica.id (+) and unita_contrattuale.id_unita=unita_immobiliari.id (+) and rapporti_utenza.id=unita_contrattuale.id_contratto (+) and unita_immobiliari.id =" & myReader("ID")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                i = 0
                dt.Rows.Add(RIGA)
                Do While myReader1.Read()

                    If i = 0 Then

                        If par.IfNull(myReader1("cognome"), "") <> "" Then
                            RIGA.Item("OCCUPANTE") = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                            RIGA.Item("DATA_NASCITA") = par.FormattaData(par.IfNull(myReader1("DATA_NASCITA"), ""))
                            RIGA.Item("LUOGO_NASCITA") = par.IfNull(myReader1("COMUNE_NASCITA"), "")
                        Else
                            RIGA.Item("OCCUPANTE") = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                            RIGA.Item("DATA_NASCITA") = "---"
                            RIGA.Item("LUOGO_NASCITA") = "---"

                        End If
                        RIGA.Item("DESCR_OCC") = par.IfNull(myReader1("Tipooccupante"), "")
                        RIGA.Item("RAPP_CONTR") = par.IfNull(myReader1("TipoRapporto"), "")
                        RIGA.Item("TIPO_CONTR") = par.IfNull(myReader1("TipoContratto"), "")
                        RIGA.Item("DECORRENZA") = par.FormattaData(par.IfNull(myReader1("DATA_DECORRENZA"), ""))
                        RIGA.Item("DATA_SLOGGIO") = par.FormattaData(par.IfNull(myReader1("DATA_RICONSEGNA"), ""))

                        i = i + 1
                    Else
                        RIGA2 = dt.NewRow()

                        RIGA2.Item("COD_UI") = ""
                        RIGA2.Item("SUB") = ""
                        RIGA2.Item("PIANO") = ""
                        RIGA2.Item("SCALA") = ""
                        RIGA2.Item("INTERNO") = ""
                        RIGA2.Item("CIVICO") = ""
                        RIGA2.Item("TIPOLOGIA") = ""
                        RIGA2.Item("SUP_CONV") = ""
                        RIGA2.Item("SUP_NETTA") = ""
                        RIGA2.Item("DISP") = ""

                        If par.IfNull(myReader1("cognome"), "") <> "" Then
                            RIGA2.Item("OCCUPANTE") = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                            RIGA2.Item("DATA_NASCITA") = par.FormattaData(par.IfNull(myReader1("DATA_NASCITA"), ""))
                            RIGA2.Item("LUOGO_NASCITA") = par.IfNull(myReader1("COMUNE_NASCITA"), "")
                        Else

                            RIGA2.Item("OCCUPANTE") = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                            RIGA2.Item("DATA_NASCITA") = "---"
                            RIGA2.Item("LUOGO_NASCITA") = "---"
                        End If
                        RIGA2.Item("DESCR_OCC") = par.IfNull(myReader1("Tipooccupante"), "")
                        RIGA2.Item("RAPP_CONTR") = par.IfNull(myReader1("TipoRapporto"), "")
                        RIGA2.Item("TIPO_CONTR") = par.IfNull(myReader1("TipoContratto"), "")
                        RIGA2.Item("DECORRENZA") = par.FormattaData(par.IfNull(myReader1("DATA_DECORRENZA"), ""))
                        RIGA2.Item("DATA_SLOGGIO") = par.FormattaData(par.IfNull(myReader1("DATA_RICONSEGNA"), ""))

                        dt.Rows.Add(RIGA2)
                        i = i + 1

                    End If
                Loop
                myReader1.Close()
                j = j + 1
            Loop
            myReader.Close()

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Session.Add("MIADT", dt)
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblIntestazione.Text = "La ricerca non ha prodotto risultati!"
            End If

            For Each di As DataGridItem In DataGrid1.Items
                If di.Cells(0).Text.Contains("&nbsp;") Then
                    di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
                Else
                    If miocolore = "#ffffff" Then
                        miocolore = "#dcdcdc"
                    Else
                        miocolore = "#ffffff"
                    End If
                End If
                
                If Not di.Cells(0).Text.Contains("&nbsp;") Then
                    di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
                End If
            Next

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            strIntest &= "<p><font face='Arial' size='2'>Numero Unita: <b>" & j & "</b></font></p>"
            lblIntestazione.Text = strIntest
            'Response.Write("<p><font face='Arial' size='2'>Numero Unita: <b>" & j & "</b></font></p>")

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

            i = 0
            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
                .SetColumnWidth(1, 1, 25)
                .SetColumnWidth(2, 2, 10)
                .SetColumnWidth(3, 3, 15)
                .SetColumnWidth(4, 4, 20)
                .SetColumnWidth(5, 5, 15)
                .SetColumnWidth(6, 6, 15)
                .SetColumnWidth(7, 7, 20)
                .SetColumnWidth(8, 8, 20)
                .SetColumnWidth(9, 9, 15)
                .SetColumnWidth(10, 10, 30)
                .SetColumnWidth(11, 11, 45)
                .SetColumnWidth(12, 12, 30)
                .SetColumnWidth(13, 13, 30)
                .SetColumnWidth(14, 14, 30)
                .SetColumnWidth(15, 15, 30)
                .SetColumnWidth(16, 16, 30)
                .SetColumnWidth(17, 17, 20)
                .SetColumnWidth(18, 18, 20)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE U.I.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "FOGLIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "MAPPALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "SUB", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PIANO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "SCALA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "INTERNO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "NUM.CIVICO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "TIPOLOGIA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "SUP.CONV.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "SUP.NETTA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "DISPONIBILITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "OCCUPANTE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "DATA NASCITA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "LUOGO NASCITA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "DESCR. OCCUP.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "RAPPORTO CONTR.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "TIPO CONTR.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "DECORRENZA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "DATA SLOGGIO", 0)


                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(Left(Right(par.IfNull(dt.Rows(i).Item("COD_UI"), ""), 21), 17)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FOGLIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUMERO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUB"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCALA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUP_CONV"), "0")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUP_NETTA"), "0")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DISP"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(dt.Rows(i).Item("OCCUPANTE"), " "))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_NASCITA"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(dt.Rows(i).Item("LUOGO_NASCITA"), " "))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCR_OCC"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RAPP_CONTR"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_CONTR"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DECORRENZA"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_SLOGGIO"), " ")))

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            caricaDati()
        End If
    End Sub
End Class
