Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class ARCHIVIO_RisultatoScatole
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValoreRS As String
    Dim sValoreCO As String
    Dim sValoreTC As String
    Dim sValoreTI As String
    Dim sValoreUN As String
    Dim sValoreST As String

    Dim sValorePIVA As String
    Dim sValoreGIMI As String



    Dim sValoreVIRT As String
    Dim sValoreTIPO As String
    Dim sValorePROVEN As String
    Dim sValoreANNI As String
    Dim sValoreRINN As String


    Dim sValoreINTEST As String

    Dim sValoreEUS As String
    Dim sValoreFAL As String
    Dim sValoreUT As String
    Dim sValoreSCA As String
    Dim sValoreSCADA As String
    Dim sValoreSCAA As String
    Dim sValoreINT As String

    Dim sStringaSql As String
    Dim scriptblock As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()
            sValoreEUS = Request.QueryString("EUS")
            sValoreFAL = Request.QueryString("FAL")
            sValoreUT = Request.QueryString("UT")
            sValoreSCA = par.DeCripta(Request.QueryString("SCA"))
            sValoreSCADA = Request.QueryString("SCADA")
            sValoreSCAA = Request.QueryString("SCAA")
            sValoreINT = Request.QueryString("INT")
            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim bTrovato1 As Boolean
        Dim sValore As String
        Dim sCompara As String
        Dim sSqlAppoggio As String = ""

        bTrovato = False
        bTrovato1 = False
        sStringaSql = ""
        sSqlAppoggio = ""


        If sValoreUT <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreUT
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.ID " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        'valori archivio

        If sValoreEUS <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreEUS
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA_ARCHIVIO.COD_EUSTORGIO " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
        End If


        If sValoreFAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFAL
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA_ARCHIVIO.FALDONE " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
        End If

        Dim POS As Integer = 0

        If sValoreSCA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreSCA
            If InStr(sValore, ",") > 0 Then
                Dim Valori() As String
                Dim sss As String = ""
                Dim seps() As Char = {","}

                Valori = sValore.Split(seps)
                For i = 0 To Valori.Length - 1
                    sss = sss & par.PulisciStrSql(Valori(i)) & "','"
                Next
                bTrovato = True
                If Mid(sss, Len(sss) - 1, 2) = ",'" Then
                    sss = Mid(sss, 1, Len(sss) - 2)
                End If
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA_ARCHIVIO.SCATOLA IN ('" & sss & ") "
            Else
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                'sStringaSql = sStringaSql & " RAPPORTI_UTENZA_ARCHIVIO.SCATOLA " & sCompara & " '" & par.PulisciStrSql(Format(CLng(sValore), "000000000")) & "'"
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA_ARCHIVIO.SCATOLA " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
            End If
        End If

        'If sValoreSCADA <> "" Then
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
        '    sValore = sValoreSCADA
        '    sStringaSql = sStringaSql & " TO_NUMBER(RAPPORTI_UTENZA_ARCHIVIO.SCATOLA)>=" & CLng(sValore) & " "
        '    bTrovato = True
        'End If

        'If sValoreSCAA <> "" Then
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
        '    sValore = sValoreSCAA
        '    sStringaSql = sStringaSql & " TO_NUMBER(RAPPORTI_UTENZA_ARCHIVIO.SCATOLA)<=" & CLng(sValore) & " "
        'End If

        If sValoreINT <> "" And sValoreINT <> "0" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreSCAA
            sStringaSql = sStringaSql & " SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' "
        End If

        sStringaSQL1 = "SELECT DISTINCT(RAPPORTI_UTENZA.COD_CONTRATTO) AS CODCONTR,RAPPORTI_UTENZA_ARCHIVIO.SCATOLA,RAPPORTI_UTENZA_ARCHIVIO.FALDONE,RAPPORTI_UTENZA_ARCHIVIO.COD_EUSTORGIO,ANAGRAFICA.ID AS COD_UTENTE,(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 then 'ERP Sociale' WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004' " _
                     & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='V' THEN '431/98 ART.15 C.2 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' " _
                     & "WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) AS TIPO_SPECIFICO, (CASE WHEN RAPPORTI_UTENZA.DURATA_ANNI IS NULL AND RAPPORTI_UTENZA.DURATA_RINNOVO IS NULL THEN RAPPORTI_UTENZA.DURATA_ANNI||''||RAPPORTI_UTENZA.DURATA_RINNOVO ELSE " _
                     & "RAPPORTI_UTENZA.DURATA_ANNI||'+'||RAPPORTI_UTENZA.DURATA_RINNOVO END) AS DURATA,TAB_FILIALI.NOME AS FILIALE_ALER,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,UNITA_IMMOBILIARI.COD_TIPOLOGIA,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE as CUI," _
                     & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE as  COD_UNITA_IMMOBILIARE, " _
                     & "'' as ALLEGATI_CONTRATTO, " _
                     & "RAPPORTI_UTENZA.*,nvl(ANAGRAFICA.CITTADINANZA,'') as cittadinanza,siscom_mi.getstatocontratto(rapporti_utenza.id) as STATO_DEL_CONTRATTO, SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID) AS NOME_INTEST," _
                     & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"" ," _
                     & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""COD FISCALE/PIVA"" ,TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_NASCITA,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA," _
                     & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) AS ""POSIZIONE_CONTRATTO"",TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCCUPANTE,unita_immobiliari.interno,scale_edifici.descrizione AS scala,tipo_livello_piano.descrizione AS piano " _
                     & " FROM " _
                     & "siscom_mi.scale_edifici,siscom_mi.tipo_livello_piano,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_OCCUPANTE," _
                     & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO " _
                     & "WHERE " _
                     & " RAPPORTI_UTENZA_ARCHIVIO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND scale_edifici.ID (+)=unita_immobiliari.id_scala AND tipo_livello_piano.cod(+)=unita_immobiliari.cod_tipo_livello_piano AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+) AND TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
                     & " AND EDIFICI.ID (+) =UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO (+) =RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID (+) =UNITA_CONTRATTUALE.ID_UNITA  AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                     & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND TIPOLOGIA_OCCUPANTE.COD = SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "
        If sStringaSql <> "" Then
            If Left(sStringaSql, 4) = " AND" Then
                sStringaSql = Replace(sStringaSql, "AND", " ")
            End If
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY scatola ASC"
        sStringaSQL2 = "SELECT COUNT(distinct rapporti_utenza.cod_contratto) FROM SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA," _
             & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO WHERE RAPPORTI_UTENZA_ARCHIVIO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
             & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
             & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "
        If sStringaSql <> "" Then
            sStringaSQL2 = sStringaSQL2 & " AND " & sStringaSql
        End If
        BindGrid()
    End Function

    Public Property sStringaSQL2() As String
        Get
            If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL2") = value
        End Set

    End Property

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Protected Sub Datagrid2_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "'")
            e.Item.Attributes.Add("onDblclick", "ApriScheda();")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub

    Protected Sub Datagrid2_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.RAPPORTI_UTENZA")
            Label4.Text = Datagrid2.Items.Count
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()

            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = sStringaSQL2
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Label4.Text = "(" & Datagrid2.Items.Count & " nella pagina - Totale :" & ds.Tables(0).Rows.Count & ") in " & myReader(0) & " Rapporti"
            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key", "<script>MakeStaticHeader('" + Datagrid2.ClientID + "', 350, 776 , 25 ,true); </script>", False)
        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message

        End Try
    End Sub

    Private Function ExportXLS_Chiama100()

        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim dt As New Data.DataTable
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try
            par.OracleConn.Open()
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                i = 0
                With myExcelFile

                    .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
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
                    .SetColumnWidth(1, 1, 30)
                    .SetColumnWidth(2, 2, 20)
                    .SetColumnWidth(3, 3, 30)
                    .SetColumnWidth(4, 4, 15)
                    .SetColumnWidth(5, 5, 45)
                    .SetColumnWidth(6, 6, 20)
                    .SetColumnWidth(7, 7, 45)
                    .SetColumnWidth(8, 8, 20)
                    .SetColumnWidth(9, 9, 25)
                    .SetColumnWidth(10, 10, 20)
                    .SetColumnWidth(11, 11, 25)
                    .SetColumnWidth(12, 12, 20)
                    .SetColumnWidth(13, 13, 20)
                    .SetColumnWidth(14, 14, 20)
                    .SetColumnWidth(15, 15, 55)
                    .SetColumnWidth(16, 16, 60)
                    .SetColumnWidth(17, 17, 30)
                    .SetColumnWidth(18, 18, 20)
                    .SetColumnWidth(19, 19, 35)
                    .SetColumnWidth(20, 20, 20)
                    .SetColumnWidth(21, 21, 25)
                    .SetColumnWidth(22, 22, 20)
                    .SetColumnWidth(23, 23, 20)
                    .SetColumnWidth(24, 24, 20)
                    .SetColumnWidth(25, 25, 20)
                    .SetColumnWidth(26, 26, 20)
                    .SetColumnWidth(27, 27, 20)
                    .SetColumnWidth(28, 28, 20)



                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD.UTENTE", 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "SCATOLA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "FALDONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD.EUSTORGIO", 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NOMINATIVO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INTESTATARIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "DATA NASCITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COD.FISCALE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "PARTITA IVA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "POSIZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "CITTADINANZA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "COD. UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "TIPO UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "INDIRIZZO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "CIVICO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "INTERNO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "PIANO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "SCALA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "COMUNE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "FILIALE", 12)



                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_UTENTE"), "")))

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCATOLA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FALDONE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_EUSTORGIO"), "")))

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_OCCUPANTE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME_INTEST"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_NASCITA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PARTITA_IVA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_CONTRATTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CITTADINANZA"), "")))

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CUI"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_TIPOLOGIA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCALA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("LUOGO_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FILIALE_ALER"), "")))



                        i = i + 1
                        K = K + 1
                    Next

                    .CloseFile()
                End With

            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

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

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            ' Response.Write("<script>window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');</script>")
            Response.Redirect("..\FileTemp\" & FileCSV & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message
        End Try

    End Function

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        ExportXLS_Chiama100()
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        ExportXLS()
    End Sub

    Private Function ExportXLS()

        Dim myExcelFile As New CM.ExcelFile
        Dim sNomeFile As String = ""
        Dim dt As New Data.DataTable
        Dim par As New CM.Global
        Dim NuovaStringa As String = sStringaSQL1
        Dim NuovaStringa1 As String = ""
        Dim FilePDF As String = ""
        Dim stringahtml As String = ""

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            FilePDF = "Report_" & Format(Now, "yyyyMMddHHmmss")

            'Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modello.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            'ContenutoIniziale = sr1.ReadToEnd()
            'sr1.Close()

            Dim fileDaCreare As System.IO.StreamWriter
            fileDaCreare = My.Computer.FileSystem.OpenTextFileWriter(Server.MapPath("../FileTemp/" & FilePDF & ".htm"), True)
            fileDaCreare.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><style type='text/css'>.style1{font-family: Arial;font-weight: bold;text-align: center;font-size: large;}.style3{font-family: Arial;font-size: small;}</style></head><body>")
            '            file.Close()

            NuovaStringa = Mid(NuovaStringa, InStr(1, UCase(NuovaStringa), "WHERE  RAPPORTI_UTENZA_ARCHIVIO"), Len(NuovaStringa))
            NuovaStringa = Replace(NuovaStringa, "ORDER BY to_number(scatola) ASC", " AND COD_TIPOLOGIA_OCCUPANTE='INTE' ORDER BY to_number(scatola) ASC")
            NuovaStringa = "SELECT DISTINCT rapporti_utenza_archivio.scatola FROM siscom_mi.scale_edifici,siscom_mi.tipo_livello_piano,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.tipologia_rapp_contrattuale,siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.tipologia_occupante,siscom_mi.indirizzi,siscom_mi.edifici,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza_archivio " & NuovaStringa

            par.cmd.CommandText = NuovaStringa
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read()
                NuovaStringa1 = Replace(sStringaSQL1, "ORDER BY to_number(scatola) ASC", " and SCATOLA='" & par.IfNull(myReader("scatola"), "") & "' AND COD_TIPOLOGIA_OCCUPANTE='INTE' ORDER BY TO_NUMBER (scatola) ASC,INTESTATARIO ASC")
                par.cmd.CommandText = NuovaStringa1
                'TestoDaScrivere = TestoDaScrivere & "<table style='width:100%;'><tr><td class='style1'>COD. EUSTORGIO: " & par.IfNull(myReader("scatola"), "") & "</td></tr><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr><tr><td><table style='width:100%;'><tr bgcolor='Silver'><td class='style3'>COD. CONTRATTO</td><td class='style3'>COD.UTENTE</td><td class='style3'>NOMINATIVO</td><td class='style3'>INDIRIZZO</td><td class='style3'>CIVICO</td><td class='style3'>INTERNO</td><td class='style3'>SCALA</td></tr>"
                fileDaCreare.WriteLine("<table style='width:100%;'><tr><td class='style1'>COD. EUSTORGIO: " & par.IfNull(myReader("scatola"), "") & "</td></tr><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr><tr><td><table style='width:100%;'><tr bgcolor='Silver'><td class='style3'>COD. CONTRATTO</td><td class='style3'>COD.UTENTE</td><td class='style3'>NOMINATIVO</td><td class='style3'>INDIRIZZO</td><td class='style3'>CIVICO</td><td class='style3'>INTERNO</td><td class='style3'>SCALA</td></tr>")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    'TestoDaScrivere = TestoDaScrivere & "<tr><td class='style3'>" & par.IfNull(myReader1("codcontr"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("cod_utente"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("intestatario"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("indirizzo"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("civico"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("interno"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("SCALA"), "") & "</td></tr>"
                    fileDaCreare.WriteLine("<tr><td class='style3'>" & par.IfNull(myReader1("codcontr"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("cod_utente"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("intestatario"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("indirizzo"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("civico"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("interno"), "") & "</td><td class='style3'>" & par.IfNull(myReader1("SCALA"), "") & "</td></tr>")
                Loop
                myReader1.Close()
                'TestoDaScrivere = TestoDaScrivere & "</table></td></tr></table><p style='page-break-before: always'>&nbsp;</p>"
                fileDaCreare.WriteLine("</table></td></tr></table><p style='page-break-before: always'>&nbsp;</p>")

            Loop
            myReader.Close()

            'ContenutoIniziale = Replace(ContenutoIniziale, "$tabella$", TestoDaScrivere)
            fileDaCreare.WriteLine("</body></html>")
            fileDaCreare.Close()

            Dim url As String = Server.MapPath("..\FileTemp\") & FilePDF
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = ""
            Licenza = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 40
            pdfConverter1.PdfDocumentOptions.RightMargin = 40
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 30
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'pdfConverter1.SavePdfFromHtmlStringToFile(ContenutoIniziale, url & ".pdf")
            pdfConverter1.SavePdfFromHtmlFileToFile(url & ".htm", url & ".pdf")



            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & FilePDF & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & FilePDF & ".PDF")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

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

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



            Response.Redirect("..\FileTemp\" & FilePDF & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message
        End Try




    End Function

End Class
