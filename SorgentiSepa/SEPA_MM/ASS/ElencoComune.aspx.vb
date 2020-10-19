Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class ASS_ElencoComune
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:300px; left:750px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        'Response.Flush() se devo soltanto visualizzare risultati di query meglio metterlo qui


        If Not IsPostBack Then
            BindGrid()
            Response.Flush() 'se devo fare reindirizzamento per file xls meglio metterlo qui
        End If
    End Sub

    Private Sub BindGrid()
        Dim sStringa As String = ""
        Dim sStringa1 As String = ""
        Dim I As Integer = 0
        Dim SS As String = ""

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            'Label8.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

            dt.Columns.Add("TIPO")
            dt.Columns.Add("TIPO UNITA")
            dt.Columns.Add("CODICE")
            dt.Columns.Add("NUM.")
            dt.Columns.Add("INDIRIZZO")
            dt.Columns.Add("STATO")
            dt.Columns.Add("NOMINATIVO")
            ' dt.Columns.Add("RISERVATA")
            dt.Columns.Add("QUARTIERE")
            dt.Columns.Add("PIANO VENDITA")
            dt.Columns.Add("FORZE DELL'ORDINE")

            Dim RIGA As System.Data.DataRow

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ""INDICE"",TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOUNITA"",ALLOGGI.*,T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"",T_COND_ALLOGGIO.DESCRIZIONE AS ""STATOA"",TAB_QUARTIERI.NOME AS ""QUARTIERE"",(CASE (EDIFICI.FL_PIANO_VENDITA) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""PIANO VENDITA"", (CASE (UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO) WHEN 6 THEN 'SI' ELSE 'NO' END) AS ""FORZE DELL'ORDINE"" FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI,SEPA.ALLOGGI,T_TIPO_INDIRIZZO,T_COND_ALLOGGIO,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TAB_QUARTIERI WHERE TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA (+) AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE=ALLOGGI.COD_ALLOGGIO AND T_COND_ALLOGGIO.COD=ALLOGGI.STATO (+) AND VECCHIO_CODICE='D' AND (ALLOGGI.STATO=5 OR ALLOGGI.STATO=10) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE=TAB_QUARTIERI.ID (+)"


            sStringa1 = ""
            Dim TIPO As String = ""
            Dim NOME As String = ""
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read()
                NOME = ""
                TIPO = ""
                If par.IfNull(myReader1("FL_POR"), "0") = "1" And par.IfNull(myReader1("FL_OA"), "0") = "0" Then
                    TIPO = "431"
                End If
                If par.IfNull(myReader1("FL_POR"), "0") = "0" And par.IfNull(myReader1("FL_OA"), "0") = "0" Then
                    TIPO = "ERP"
                End If

                If par.IfNull(myReader1("FL_OA"), "0") = "1" Then
                    TIPO = "O.A."
                End If

                If par.IfNull(myReader1("ID_PRATICA"), 0) <> 0 Then
                    If myReader1("ID_PRATICA") < 500000 Then
                        par.cmd.CommandText = "SELECT COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME FROM COMP_NUCLEO,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID=" & myReader1("ID_PRATICA") & " AND COMP_NUCLEO.PROGR=0 AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            NOME = par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader2("NOME"), "")
                        End If
                        myReader2.Close()
                    Else
                        par.cmd.CommandText = "SELECT COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME FROM COMP_NUCLEO_CAMBI,DOMANDE_BANDO_CAMBI WHERE DOMANDE_BANDO_CAMBI.ID=" & myReader1("ID_PRATICA") & " AND COMP_NUCLEO_CAMBI.PROGR=0 AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            NOME = par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader2("NOME"), "")
                        End If
                        myReader2.Close()
                    End If




                Else
                    If myReader1("STATOA") = "8" Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_ASSEGNATE WHERE ID_UNITA=" & myReader1("INDICE") & " ORDER BY DATA_ASSEGNAZIONE DESC"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            NOME = par.IfNull(myReader2("COGNOME_RS"), "") & " " & par.IfNull(myReader2("NOME"), "")
                        End If
                        myReader2.Close()
                    End If
                End If

                'sStringa1 = sStringa1 & vbCrLf _
                '            & "<tr>" & vbCrLf _
                '            & "<td width='10%'><font face='Arial' size='1'>" & TIPO & "</font></td>" & vbCrLf _
                '            & "<td width='10%'><font face='Arial' size='1'>" & myReader1("TIPOUNITA") & "</font></td>" & vbCrLf _
                '            & "<td width='10%'><font face='Arial' size='1'>" & myReader1("COD_ALLOGGIO") & "</font></td>" & vbCrLf _
                '            & "<td width='5%'><font face='Arial' size='1'>" & myReader1("NUM_ALLOGGIO") & "</font></td>" & vbCrLf _
                '            & "<td width='25%'><font face='Arial' size='1'>" & myReader1("TIPO_VIA") & " " & myReader1("INDIRIZZO") & ", " & myReader1("NUM_CIVICO") & "</font></td>" & vbCrLf _
                '            & "<td width='10%'><font face='Arial' size='1'>" & myReader1("STATOA") & "</font></td>" & vbCrLf _
                '            & "<td width='30%'><font face='Arial' size='1'>" & NOME & "</font></td>" & vbCrLf _
                '            & "</tr>"
                I = I + 1


                RIGA = dt.NewRow()
                RIGA.Item("TIPO") = TIPO
                RIGA.Item("TIPO UNITA") = par.IfNull(myReader1("TIPOUNITA"), " ")
                RIGA.Item("CODICE") = par.IfNull(myReader1("COD_ALLOGGIO"), " ")
                RIGA.Item("NUM.") = par.IfNull(myReader1("NUM_ALLOGGIO"), " ")
                RIGA.Item("INDIRIZZO") = par.IfNull(myReader1("TIPO_VIA"), " ") & " " & par.IfNull(myReader1("INDIRIZZO"), " ") & ", " & par.IfNull(myReader1("NUM_CIVICO"), " ")
                RIGA.Item("STATO") = par.IfNull(myReader1("STATOA"), " ")
                RIGA.Item("NOMINATIVO") = NOME
                'RIGA.Item("RISERVATA") = par.IfNull(myReader1("RISERVATA"), " ")
                RIGA.Item("QUARTIERE") = par.IfNull(myReader1("QUARTIERE"), " ")
                RIGA.Item("PIANO VENDITA") = par.IfNull(myReader1("PIANO VENDITA"), " ")
                RIGA.Item("FORZE DELL'ORDINE") = par.IfNull(myReader1("FORZE DELL'ORDINE"), " ")

                dt.Rows.Add(RIGA)

            End While
            myReader1.Close()




            'par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ""INDICE"",TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOUNITA"",UI_USI_DIVERSI.*,T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"",T_COND_ALLOGGIO.DESCRIZIONE AS ""STATOA"",TAB_QUARTIERI.NOME AS ""QUARTIERE"", (CASE (EDIFICI.FL_PIANO_VENDITA) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""PIANO VENDITA"", (CASE (UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO) WHEN 6 THEN 'SI' ELSE 'NO' END) AS ""FORZE DELL'ORDINE"" FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UI_USI_DIVERSI,T_TIPO_INDIRIZZO,T_COND_ALLOGGIO,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TAB_QUARTIERI WHERE TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA (+) AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE=UI_USI_DIVERSI.COD_ALLOGGIO AND T_COND_ALLOGGIO.COD=UI_USI_DIVERSI.STATO (+) AND VECCHIO_CODICE='D' AND (UI_USI_DIVERSI.STATO=5 OR UI_USI_DIVERSI.STATO=10) AND UI_USI_DIVERSI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE=TAB_QUARTIERI.ID (+)"


            'TIPO = ""
            'NOME = ""
            'myReader1 = par.cmd.ExecuteReader()
            'While myReader1.Read()
            '    NOME = ""
            '    TIPO = ""
            '    If par.IfNull(myReader1("FL_POR"), "0") = "1" And par.IfNull(myReader1("FL_OA"), "0") = "0" Then
            '        TIPO = "431"
            '    End If
            '    If par.IfNull(myReader1("FL_POR"), "0") = "0" And par.IfNull(myReader1("FL_OA"), "0") = "0" Then
            '        TIPO = "ERP"
            '    End If

            '    If par.IfNull(myReader1("ID_PRATICA"), 0) <> 0 Then
            '        If myReader1("ID_PRATICA") < 500000 Then
            '            par.cmd.CommandText = "SELECT COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME FROM COMP_NUCLEO,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID=" & myReader1("ID_PRATICA") & " AND COMP_NUCLEO.PROGR=0 AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE"
            '            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '            If myReader2.Read Then
            '                NOME = par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader2("NOME"), "")
            '            End If
            '            myReader2.Close()
            '        Else
            '            par.cmd.CommandText = "SELECT COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME FROM COMP_NUCLEO_CAMBI,DOMANDE_BANDO_CAMBI WHERE DOMANDE_BANDO_CAMBI.ID=" & myReader1("ID_PRATICA") & " AND COMP_NUCLEO_CAMBI.PROGR=0 AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE"
            '            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '            If myReader2.Read Then
            '                NOME = par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader2("NOME"), "")
            '            End If
            '            myReader2.Close()
            '        End If

            '    Else
            '        If myReader1("STATOA") = "8" Then
            '            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_ASSEGNATE WHERE ID_UNITA=" & myReader1("INDICE") & " ORDER BY DATA_ASSEGNAZIONE DESC"
            '            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '            If myReader2.Read Then
            '                NOME = par.IfNull(myReader2("COGNOME_RS"), "") & " " & par.IfNull(myReader2("NOME"), "")
            '            End If
            '            myReader2.Close()
            '        End If
            '    End If

            '    'sStringa1 = sStringa1 & vbCrLf _
            '    '            & "<tr>" & vbCrLf _
            '    '            & "<td width='10%'><font face='Arial' size='1'>" & TIPO & "</font></td>" & vbCrLf _
            '    '            & "<td width='10%'><font face='Arial' size='1'>" & myReader1("TIPOUNITA") & "</font></td>" & vbCrLf _
            '    '            & "<td width='10%'><font face='Arial' size='1'>" & myReader1("COD_ALLOGGIO") & "</font></td>" & vbCrLf _
            '    '            & "<td width='5%'><font face='Arial' size='1'>" & myReader1("NUM_ALLOGGIO") & "</font></td>" & vbCrLf _
            '    '            & "<td width='25%'><font face='Arial' size='1'>" & myReader1("TIPO_VIA") & " " & myReader1("INDIRIZZO") & ", " & myReader1("NUM_CIVICO") & "</font></td>" & vbCrLf _
            '    '            & "<td width='10%'><font face='Arial' size='1'>" & myReader1("STATOA") & "</font></td>" & vbCrLf _
            '    '            & "<td width='30%'><font face='Arial' size='1'>" & NOME & "</font></td>" & vbCrLf _
            '    '            & "</tr>"


            '    I = I + 1

            '    RIGA = dt.NewRow()
            '    RIGA.Item("TIPO") = TIPO
            '    RIGA.Item("TIPO UNITA") = par.IfNull(myReader1("TIPOUNITA"), " ")
            '    RIGA.Item("CODICE") = par.IfNull(myReader1("COD_ALLOGGIO"), " ")
            '    RIGA.Item("NUM.") = par.IfNull(myReader1("NUM_ALLOGGIO"), " ")
            '    RIGA.Item("INDIRIZZO") = par.IfNull(myReader1("TIPO_VIA"), " ") & " " & par.IfNull(myReader1("INDIRIZZO"), " ") & ", " & par.IfNull(myReader1("NUM_CIVICO"), " ")
            '    RIGA.Item("STATO") = par.IfNull(myReader1("STATOA"), " ")
            '    RIGA.Item("NOMINATIVO") = NOME
            '    'RIGA.Item("RISERVATA") = par.IfNull(myReader1("RISERVATA"), " ")
            '    RIGA.Item("QUARTIERE") = par.IfNull(myReader1("QUARTIERE"), " ")
            '    RIGA.Item("PIANO VENDITA") = par.IfNull(myReader1("PIANO VENDITA"), " ")
            '    RIGA.Item("FORZE DELL'ORDINE") = par.IfNull(myReader1("FORZE DELL'ORDINE"), " ")

            '    dt.Rows.Add(RIGA)

            'End While
            'myReader1.Close()

            Session.Add("MIADT", dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            sStringa = "<p><b>Lista Alloggi disponibili</b></p>" '& vbCrLf _
            '   & "<div widht=100%><table border='0' cellpadding='0' cellspacing='0' width:100%>" & vbCrLf _
            '                     & "<tr>" & vbCrLf _
            '                     & "<td width='10%' ><span style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" & vbCrLf _
            '                     & "<td width='10%'><span style='font-size: 8pt; font-family: Arial'><strong>TIPO UNITA</strong></span></td><td></td>" & vbCrLf _
            '                     & "<td width='10%'><span style='font-size: 8pt; font-family: Arial'><strong>CODICE</strong></span></td>" & vbCrLf _
            '                     & "<td width='5%'><span style='font-size: 8pt; font-family: Arial'><strong>NUM.</strong></span></td>" & vbCrLf _
            '                     & "<td width='25%'><span style='font-size: 8pt; font-family: Arial'><strong>INDIRIZZO</strong></span></td>" & vbCrLf _
            '                     & "<td width='10%'><span style='font-size: 8pt; font-family: Arial'><strong>STATO</strong></span></td>" & vbCrLf _
            '                     & "<td width='30%'><span style='font-size: 8pt; font-family: Arial'><strong>NOMINATIVO</strong></span></td>" & vbCrLf _
            '                     & "</tr></div></table>" & vbCrLf _
            '& "<div style=" & Chr(34) & "overflow: auto;height: 660px;width:100%" & Chr(34) & ">" & vbCrLf _
            '                     & "<table border='1' cellpadding='0' cellspacing='0' width:100% >" & sStringa1 & "</table></div>"

            'SS = "<table border='0' cellpadding='0' cellspacing='0' width:100%>" & vbCrLf _
            '         & "<tr>" & vbCrLf _
            '         & "<td width='10%'><span style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" & vbCrLf _
            '         & "<td width='10%'><span style='font-size: 8pt; font-family: Arial'><strong>TIPO UNITA</strong></span></td>" & vbCrLf _
            '         & "<td width='10%'><span style='font-size: 8pt; font-family: Arial'><strong>CODICE</strong></span></td>" & vbCrLf _
            '         & "<td width='5%'><span style='font-size: 8pt; font-family: Arial'><strong>NUM.</strong></span></td>" & vbCrLf _
            '         & "<td width='25%'><span style='font-size: 8pt; font-family: Arial'><strong>INDIRIZZO</strong></span></td>" & vbCrLf _
            '         & "<td width='10%'><span style='font-size: 8pt; font-family: Arial'><strong>STATO</strong></span></td>" & vbCrLf _
            '         & "<td width='30%'><span style='font-size: 8pt; font-family: Arial'><strong>NOMINATIVO</strong></span></td>" & vbCrLf _
            '         & "</tr>" & vbCrLf & sStringa1 & "</table>"




            'Label9.Text = sStringa
            Label8.Text = " - " & I
            'Label10.Text = SS


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub


    'Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
    '    Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    'End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
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

                .CreateFile(Server.MapPath("PROVVEDIMENTI\" & sNomeFile & ".xls"))
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "TIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "TIPO UNITA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "NUM.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "STATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NOMINATIVO", 0)
                ' .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "RISERVATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "QUARTIERE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "PIANO VENDITA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "FORZE DELL'ORDINE", 0)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO UNITA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CODICE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM."), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STATO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOMINATIVO"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RISERVATA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("QUARTIERE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO VENDITA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FORZE DELL'ORDINE"), 0)))

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("PROVVEDIMENTI\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("PROVVEDIMENTI\" & sNomeFile & ".xls")
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
            Response.Redirect("PROVVEDIMENTI\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('PROVVEDIMENTI/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    'Private Function Export()
    '    Session.Add("FILEXLS", par.Cripta(Label10.Text))
    '    Response.Write("<script>window.open('ExportElencoComune.aspx','Disponibilita','');</script>")



    'End Function
    'Private Function Export()
    '    Dim dt As New Data.DataTable
    '    Dim par As New CM.Global

    '    Dim FileCSV As String = ""
    '    Dim row As System.Data.DataRow
    '    Dim i As Long = 0

    '    Try
    '        par.OracleConn.Open()
    '        FileCSV = "Disponibilita_" & Format(Now, "yyyyMMddHHmmss")

    '        Dim da As Oracle.DataAccess.Client.OracleDataAdapter

    '        da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
    '        da.Fill(dt)

    '        If dt.Rows.Count > 0 Then
    '            Dim sSql As String
    '            sSql = "CREATE TABLE ESTRAZIONE ([COD. UNITA] TEXT(50),[COD. CONTRATTO] TEXT(50),[TIPO UNITA] TEXT(50),[INDIRIZZO] TEXT(50), [CIVICO] TEXT(50), [DATA DISDETTA] TEXT(50))"

    '            Dim cnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
    '                   "Data Source=" & Server.MapPath("Varie\" & FileCSV & ".xls;") & _
    '                   "Extended Properties=""Excel 8.0;HDR=YES"""

    '            Dim cn As New OleDbConnection(cnString)
    '            cn.Open()

    '            Dim cmd As New OleDbCommand(sSql, cn)
    '            cmd.ExecuteNonQuery()

    '            Dim cmd1 As New OleDbCommand
    '            cmd1.Connection = cn
    '            For Each row In dt.Rows
    '                'sr.WriteLine(par.IfNull(dt.Rows(i).Item("RATA"), 0) & ";" & par.IfNull(dt.Rows(i).Item("INTESTATARIO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("PERIODO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("AFFITTO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("SPESE"), 0) & ";" & par.IfNull(dt.Rows(i).Item("REGISTRAZIONE"), 0) & ";" & par.IfNull(dt.Rows(i).Item("TOT"), 0) & ";")
    '                sSql = "INSERT INTO ESTRAZIONE  VALUES ('" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), 0)) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), 0)) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_TIPOLOGIA"), 0)) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), 0)) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO"), 0)) & "','" & par.PulisciStrSql(par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_DISDETTA_LOCATARIO"), 0))) & "')"
    '                cmd1.CommandText = sSql
    '                cmd1.ExecuteNonQuery()
    '                i = i + 1
    '            Next
    '            cn.Close()
    '        End If

    '        Dim objCrc32 As New Crc32()
    '        Dim strmZipOutputStream As ZipOutputStream
    '        Dim zipfic As String

    '        zipfic = Server.MapPath("Varie\" & FileCSV & ".zip")

    '        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '        strmZipOutputStream.SetLevel(6)
    '        '
    '        Dim strFile As String
    '        strFile = Server.MapPath("Varie\" & FileCSV & ".xls")
    '        Dim strmFile As FileStream = File.OpenRead(strFile)
    '        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '        '
    '        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '        Dim sFile As String = Path.GetFileName(strFile)
    '        Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '        Dim fi As New FileInfo(strFile)
    '        theEntry.DateTime = fi.LastWriteTime
    '        theEntry.Size = strmFile.Length
    '        strmFile.Close()
    '        objCrc32.Reset()
    '        objCrc32.Update(abyBuffer)
    '        theEntry.Crc = objCrc32.Value
    '        strmZipOutputStream.PutNextEntry(theEntry)
    '        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '        strmZipOutputStream.Finish()
    '        strmZipOutputStream.Close()

    '        File.Delete(strFile)
    '        Response.Write("<script>window.open('varie/" & FileCSV & ".zip','Disponibilita','');</script>")
    '        'Response.Redirect("Varie\" & FileCSV & ".zip")


    '        par.OracleConn.Close()

    '    Catch ex As Exception
    '        par.OracleConn.Close()
    '    End Try
    'End Function


    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            'DA MODIFICARE SE OCCORRE SELEZIONARE e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato :PG " & e.Item.Cells(1).Text & "';")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex 'PER RICARICARE LA TABELLA NELLA PAGINAZIONE
            BindGrid()
        End If
    End Sub

End Class
