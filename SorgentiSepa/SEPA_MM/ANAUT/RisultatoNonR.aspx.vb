Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_RisultatoNonR
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sValoreFI As String
    Dim sValoreSDAL As String
    Dim sValoreSAL As String
    Dim sValoreADAL As String
    Dim sValoreAAL As String
    Dim sStringaSql As String
    Dim sStringaSqlD As String
    Dim sStringaSqlD1 As String
    Dim sStringaSqlD2 As String = ""
    Dim sValoreES As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sValoreBA = Request.QueryString("BA")
            sValoreFI = Request.QueryString("FI")
            sValoreSDAL = Request.QueryString("SDAL")
            sValoreSAL = Request.QueryString("SAL")
            sValoreADAL = Request.QueryString("ADAL")
            sValoreAAL = Request.QueryString("AAL")
            sValoreES = Request.QueryString("ES")
            If sValoreFI = "-1" Then sValoreFI = ""
            If sValoreBA = -1 Then sValoreBA = ""
            'btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
        End If

    End Sub


    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""
        sStringaSqlD = ""
        sStringaSqlD2 = ""


        If sValoreADAL <> "" Then
            sStringaSqlD = "data_app>='" & sValoreADAL & "' "
            If sValoreAAL <> "" Then
                sStringaSqlD = sStringaSqlD & " AND DATA_APP<='" & sValoreAAL & "'"
                sStringaSqlD1 = " DATA_APP>'" & sValoreAAL & "'  "
                sStringaSqlD2 = " DATA_APP<'" & sValoreAAL & "'  "
            End If
        Else
            If sValoreAAL <> "" Then
                sStringaSqlD = sStringaSqlD & " DATA_APP<='" & sValoreAAL & "'"
                sStringaSqlD1 = " DATA_APP>'" & sValoreAAL & "'  "
                sStringaSqlD2 = " DATA_APP<'" & sValoreAAL & "'  "
            End If
        End If

        'If sValoreFI <> "58" Then
        If sValoreFI <> "" Then

            sValore = sValoreFI

            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " tab_filiali.id " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        Else
            bTrovato = True
            sStringaSql = sStringaSql & " COMPLESSI_IMMOBILIARI.id_filiale is null "

        End If
        'End If

        If sValoreSDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreSAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreSDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreSAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If UCase(sValoreES) = "TRUE" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL "
        End If



        ''sStringaSQL1 = "SELECT RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA,TO_CHAR(TO_DATE(DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
        ''            & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA," _
        ''            & "(TAB_FILIALI.NOME ) AS FILIALE  FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.TAB_FILIALI, siscom_mi.COMPLESSI_IMMOBILIARI, siscom_mi.EDIFICI WHERE COMPLESSI_IMMOBILIARI.id_filiale=TAB_FILIALI.ID (+) AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio  AND  " _
        ''            & " (anagrafica.ragione_sociale is null or anagrafica.ragione_sociale='') and " _
        ''            & " ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND SUBSTR(COD_CONTRATTO,1,6)<>'000000' AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE=SUBSTR(RAPPORTI_UTENZA.COD_CONTRATTO,1,17)  " _
        ''            & " AND (COD_TIPOLOGIA_CONTR_LOC='ERP' OR COD_TIPOLOGIA_CONTR_LOC='EQC392') AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA>='20100101')" _
        ''            & " AND COD_CONTRATTO IS NOT NULL AND SUBSTR(COD_CONTRATTO,1,1)<>'4' AND " _
        ''            & " COD_CONTRATTO NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE COD_CONVOCAZIONE IS NULL AND RAPPORTO IS NOT NULL AND ID_BANDO=" & sValoreBA & " AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) " _
        ''            & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & sValoreBA & ")"


        ''SUBSTR(COD_CONTRATTO,1,1)<>'4' and SUBSTR(COD_CONTRATTO,1,6)<>'000000' and

        ''If sValoreFI <> "58" Then
        'sStringaSQL1 = "SELECT RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA,TO_CHAR(TO_DATE(DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
        '           & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA," _
        '           & "(TAB_FILIALI.NOME ) AS FILIALE  FROM siscom_mi.unita_contrattuale,SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.TAB_FILIALI, siscom_mi.COMPLESSI_IMMOBILIARI, siscom_mi.EDIFICI " _
        '           & " WHERE  SUBSTR(COD_CONTRATTO,1,1)<>'4' and SUBSTR(COD_CONTRATTO,1,6)<>'000000' and COMPLESSI_IMMOBILIARI.id_filiale=TAB_FILIALI.ID (+) AND " _
        '           & " COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio  AND  " _
        '           & " (anagrafica.ragione_sociale is null or anagrafica.ragione_sociale='') and " _
        '           & " ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
        '           & " AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND  " _
        '           & " UNITA_IMMOBILIARI.ID=unita_contrattuale.id_unita AND unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL " _
        '           & " AND (COD_TIPOLOGIA_CONTR_LOC='ERP' OR COD_TIPOLOGIA_CONTR_LOC='EQC392') AND (DATA_RICONSEGNA IS NULL)" _
        '           & " AND COD_CONTRATTO IS NOT NULL AND  " _
        '           & " COD_CONTRATTO NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE FL_AUSI=0 AND RAPPORTO IS NOT NULL AND ID_BANDO=" & sValoreBA & " AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) " _
        '           & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & sValoreBA & ") " _
        '           & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.CONVOCAZIONI_AU WHERE CARICO_AUSI=1 AND ID_GRUPPO IN " _
        '           & "(SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & sValoreBA & ")) "


        sStringaSQL1 = "SELECT rapporti_utenza.ID AS idc, cod_contratto, anagrafica.cognome," _
   & "anagrafica.nome, cod_tipologia_contr_loc AS tipologia," _
   & "TO_CHAR (TO_DATE (data_decorrenza, 'YYYYmmdd')," _
  & "'DD/MM/YYYY'" _
   & ") AS decorrenza," _
   & "TO_CHAR (TO_DATE (data_riconsegna, 'YYYYmmdd')," _
  & "'DD/MM/YYYY'" _
   & ") AS SLOGGIO," _
   & "indirizzi.descrizione AS indirizzo_unita," _
& "   indirizzi.civico AS civico_unita, indirizzi.cap AS cap_unita," _
   & "indirizzi.localita AS comune_unita, (tab_filiali.nome) AS filiale " _
  & "FROM siscom_mi.unita_contrattuale," _
   & "siscom_mi.anagrafica," _
& "   siscom_mi.soggetti_contrattuali," _
   & "siscom_mi.rapporti_utenza," _
& "   siscom_mi.indirizzi," _
   & "siscom_mi.unita_immobiliari," _
& "   siscom_mi.tab_filiali," _
   & "siscom_mi.complessi_immobiliari," _
& "   siscom_mi.edifici " _
 & "WHERE SUBSTR (cod_contratto, 1, 1) <> '4' " _
 & "AND SUBSTR (cod_contratto, 1, 6) <> '000000' " _
& " AND complessi_immobiliari.id_filiale = tab_filiali.ID(+) " _
 & "AND complessi_immobiliari.ID = edifici.id_complesso " _
& " AND edifici.ID = unita_immobiliari.id_edificio " _
 & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
& " AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
 & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
& " AND indirizzi.ID = unita_immobiliari.id_indirizzo " _
 & "AND unita_immobiliari.ID = unita_contrattuale.id_unita " _
& " AND unita_contrattuale.id_contratto = rapporti_utenza.ID " _
 & "AND unita_contrattuale.id_unita_principale IS NULL " _
 & "AND cod_contratto IS NOT NULL " _
 & "AND rapporti_utenza.ID NOT IN (SELECT id_contratto FROM siscom_mi.diffide_lettere WHERE id_contratto = rapporti_utenza.ID AND id_au = " & sValoreBA & ") " _
& "	 AND rapporti_utenza.ID IN (SELECT DISTINCT id_contratto FROM siscom_mi.convocazioni_au WHERE " & sStringaSqlD & " AND (NVL(carico_ausi,0) <> 1 " _
  & "AND NVL(id_stato,0)<>2 AND NVL(id_motivo_annullo,0)<>1)  " _
& " and id_gruppo IN (SELECT ID FROM siscom_mi.convocazioni_au_gruppi WHERE id_au = " & sValoreBA & ")) AND rapporti_utenza.id NOT IN (SELECT DISTINCT id_contratto  FROM siscom_mi.convocazioni_au  WHERE " & sStringaSqlD2 & " and NVL(id_stato, 0) = 2) " _
& "	and rapporti_utenza.cod_contratto not in (select nvl(rapporto,'') from utenza_dichiarazioni where id_bando=" & sValoreBA & ") and rapporti_utenza.id NOT IN (SELECT id_contratto FROM  siscom_mi.convocazioni_au WHERE " & sStringaSqlD1 & ") "


        ''Else
        'sStringaSQL1 = "SELECT RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA,TO_CHAR(TO_DATE(DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
        '                       & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA," _
        '                       & "(TAB_FILIALI.NOME ) AS FILIALE  FROM siscom_mi.unita_contrattuale,SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.TAB_FILIALI, siscom_mi.COMPLESSI_IMMOBILIARI, siscom_mi.EDIFICI " _
        '                       & " WHERE  SUBSTR(COD_CONTRATTO,1,1)<>'4' and SUBSTR(COD_CONTRATTO,1,6)<>'000000' and COMPLESSI_IMMOBILIARI.id_filiale=TAB_FILIALI.ID (+) AND " _
        '                       & " COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio  AND  " _
        '                       & " (anagrafica.ragione_sociale is null or anagrafica.ragione_sociale='') and " _
        '                       & " ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
        '                       & " AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND  " _
        '                       & " UNITA_IMMOBILIARI.ID=unita_contrattuale.id_unita AND unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL " _
        '                       & " AND (COD_TIPOLOGIA_CONTR_LOC='ERP' OR COD_TIPOLOGIA_CONTR_LOC='EQC392') AND (DATA_RICONSEGNA IS NULL)" _
        '                       & " AND COD_CONTRATTO IS NOT NULL AND  " _
        '                       & " COD_CONTRATTO NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE FL_AUSI=0 AND RAPPORTO IS NOT NULL AND ID_BANDO=" & sValoreBA & " AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) " _
        '                       & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & sValoreBA & ") " _
        '                       & " AND RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.CONVOCAZIONI_AU WHERE CARICO_AUSI=1 AND ID_GRUPPO IN " _
        '                       & "(SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & sValoreBA & ")) "
        'End If
        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY descrizione asc,indirizzi.civico asc,anagrafica.cognome ASC,anagrafica.nome asc"


        BindGrid()
    End Function


    Private Sub BindGrid()

        par.OracleConn.Open()
      
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Dim dt As New Data.DataTable
        da.Fill(dt)
        HttpContext.Current.Session.Add("ElencoDiffide", dt)

        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count


        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub


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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaNonR.aspx""</script>")
    End Sub

    Protected Sub btnSelezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = True 
        Next
    End Sub

    Protected Sub btnDeselezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = False
        Next
    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If LBLID.Value = "1" Then
            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim dt As New System.Data.DataTable
            Dim ROW As System.Data.DataRow
            Dim I As Long = 0
            Dim filiale As String = "0"

            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("IDC")
            dt.Columns.Add("BANDO")

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    ROW = dt.NewRow()
                    ROW.Item("COD_CONTRATTO") = oDataGridItem.Cells(1).Text
                    'If Request.QueryString("FI") <> "58" Then
                    ROW.Item("FILIALE") = oDataGridItem.Cells(2).Text
                    'Else
                    '   ROW.Item("FILIALE") = "GRUPPO DI LAVORO ANAGRAFE COMUNE DI MILANO"
                    'End If
                    ROW.Item("IDC") = oDataGridItem.Cells(3).Text
                    ROW.Item("BANDO") = sValoreBA
                    dt.Rows.Add(ROW)
                    I = I + 1
                End If
            Next

            If I > 0 Then
                HttpContext.Current.Session.Add("ElencoDT", dt)
                Response.Redirect("StampaNonR.aspx?PG=" & npg.Value & "&FI=" & Request.QueryString("FI") & "&BA=" & sValoreBA & "&RIF=" & Request.QueryString("AAL"))
            Else
                Response.Write("<script>alert('Nessuna riga selezionata');</script>")
            End If
        End If
    End Sub


    Public Property sValoreBA() As String
        Get
            If Not (ViewState("par_sValoreBA") Is Nothing) Then
                Return CStr(ViewState("par_sValoreBA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sValoreBA") = value
        End Set

    End Property

    Protected Sub imgExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
        Export()
    End Sub

    Function Export()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try

            Dim dt As New System.Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("ElencoDiffide"), Data.DataTable)
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            If Not IsNothing(dt) Then
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

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD CONTRATTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COGNOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "NOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPOLOGIA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "DECORRENZA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "SLOGGIO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "INDIRIZZO UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "CIVICO UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CAP UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "FILIALE")
                        K = 2
                        For Each row In dt.Rows
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("COGNOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("NOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("DECORRENZA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("SLOGGIO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("INDIRIZZO_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("CIVICO_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("CAP_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("COMUNE_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("FILIALE"), ""))

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


                'Dim scriptblock As String = "<script language='javascript' type='text/javascript'>" _
                '                        & "window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');" _
                '                        & "</script>"
                'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                'End If

                If File.Exists(Server.MapPath("~\FileTemp\") & FileCSV & ".ZIP") Then
                    Response.Redirect("../FileTemp/" & FileCSV & ".ZIP", False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If


                'Response.Redirect("..\FileTemp\" & FileCSV & ".zip")
            Else

            End If

        Catch ex As Exception
            HttpContext.Current.Session.Remove("ElencoDiffide")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function
End Class
