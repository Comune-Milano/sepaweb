Imports System.IO
Imports SubSystems.RP
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf

Partial Class ANAUT_RisultatoNonRispondenti
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Dim sValoreSDAL As String
    Dim sValoreSAL As String
    Dim sValoreADAL As String
    Dim sValoreAAL As String
    Dim sStringaSql As String
    Dim sStringaSqlD As String
    Dim sStringaSqlD1 As String
    Dim sStringaSqlD2 As String = ""
    Dim sValoreES As String
    Dim sValoreDV As String
    Dim sValoreGD As String
    Dim sValoreSP As String

    Dim sValoreM1 As String
    Dim sValoreM2 As String
    Dim sValoreM3 As String
    Dim sValoreM4 As String
    Dim sValoreM5 As String
    Dim sValoreM6 As String
    Dim sValoreM7 As String
    Dim sValoreM8 As String


    Public Property sCriteri() As String
        Get
            If Not (ViewState("par_sCriteri") Is Nothing) Then
                Return CStr(ViewState("par_sCriteri"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCriteri") = value
        End Set

    End Property

    Public Property sValoreFI() As String
        Get
            If Not (ViewState("par_sValoreFI") Is Nothing) Then
                Return CStr(ViewState("par_sValoreFI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sValoreFI") = value
        End Set

    End Property

    Public Property sNomeBandoAU() As String
        Get
            If Not (ViewState("par_sNomeBandoAU") Is Nothing) Then
                Return CStr(ViewState("par_sNomeBandoAU"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNomeBandoAU") = value
        End Set

    End Property

    Public Property sAnnoAU() As String
        Get
            If Not (ViewState("par_sAnnoAU") Is Nothing) Then
                Return CStr(ViewState("par_sAnnoAU"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sAnnoAU") = value
        End Set

    End Property

    Public Property sAnnoIsee() As String
        Get
            If Not (ViewState("par_sAnnoIsee") Is Nothing) Then
                Return CStr(ViewState("par_sAnnoIsee"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sAnnoIsee") = value
        End Set

    End Property

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
            sValoreBA = Request.QueryString("BA")
            sValoreFI = Request.QueryString("FI")
            sValoreSDAL = Request.QueryString("SDAL")
            sValoreSAL = Request.QueryString("SAL")
            sValoreADAL = Request.QueryString("ADAL")
            sValoreAAL = Request.QueryString("AAL")
            sValoreES = Request.QueryString("ES")

            sValoreGD = Request.QueryString("GD")
            sValoreDV = Request.QueryString("DV")
            sValoreSP = par.DeCripta(Request.QueryString("NSP"))

            If sValoreFI = "-1" Then sValoreFI = ""
            If sValoreBA = -1 Then sValoreBA = ""

            sValoreM1 = Request.QueryString("M1")
            sValoreM2 = Request.QueryString("M2")
            sValoreM3 = Request.QueryString("M3")
            sValoreM4 = Request.QueryString("M4")
            sValoreM5 = Request.QueryString("M5")
            sValoreM6 = Request.QueryString("M6")
            sValoreM7 = Request.QueryString("M7")
            sValoreM8 = Request.QueryString("M8")

            If sValoreM1 = "1" Then
                sValoreM2 = "1"
                sValoreM3 = "1"
                sValoreM4 = "1"
                sValoreM5 = "1"
                sValoreM6 = "1"
                sValoreM7 = "1"
                sValoreM8 = "1"
            End If

            LBLID.Value = "-1"
            Cerca()
            CercaModelli()
            DatiBando()

            sCriteri = "AU: " & sNomeBandoAU & " - Sportello:" & sValoreSP
            Dim ss As String = "Indifferente"
            Dim s1 As String = ""

            If sValoreSDAL <> "" Then
                s1 = "Dal " & sValoreSDAL
            End If
            If sValoreSAL <> "" Then
                s1 = s1 & " Fino Al " & sValoreSAL
            End If
            If s1 <> "" Then ss = s1

            sCriteri = sCriteri & " - Contr. Stipulati: " & ss

            ss = "Indifferente"
            s1 = ""

            If sValoreADAL <> "" Then
                s1 = "Dal " & sValoreADAL
            End If
            If sValoreAAL <> "" Then
                s1 = s1 & " Fino Al " & sValoreAAL
            End If
            If s1 <> "" Then ss = s1

            sCriteri = sCriteri & " - Appuntamenti: " & ss

            If UCase(sValoreES) = "TRUE" Then
                sCriteri = sCriteri & " - Escludi contratti chiusi dopo l'invio della notifica dell'appuntamento: SI"
            Else
                sCriteri = sCriteri & " - Escludi contratti chiusi dopo l'invio della notifica dell'appuntamento: NO"
            End If

            If UCase(sValoreGD) = "TRUE" Then
                sCriteri = sCriteri & " - Escludi i contratti già Diffidati: SI"
            Else
                sCriteri = sCriteri & " - Escludi i contratti già Diffidati: NO"
            End If

            ss = "Nessuno motivo"
            sCriteri = sCriteri & " - Escludi i contratti con appuntamenti sospesi per:"

            If UCase(sValoreM2) = "1" Then
                ss = ss & " -Sospese per altro motivo "
            End If

            If UCase(sValoreM3) = "1" Then
                ss = ss & " -Sospesa per Fissato Nuovo Appuntamento "
            End If

            If UCase(sValoreM4) = "1" Then
                ss = ss & " -Sospesa per In Altro Procedimento "
            End If

            If UCase(sValoreM5) = "1" Then
                ss = ss & " -Sospesa Invio per Posta "
            End If

            If UCase(sValoreM6) = "1" Then
                ss = ss & " -Sospesa per Invio Tramite Sindacati "
            End If

            If UCase(sValoreM7) = "1" Then
                ss = ss & " -Sospesa per sloggio "
            End If

            If UCase(sValoreM8) = "1" Then
                ss = ss & " -Sospesa per Visita Domiciliare "
            End If
            sCriteri = sCriteri & ss


        End If
        txtDataStampa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function CercaModelli()
        sStringaSQLModelli = "select utenza_bandi.descrizione as DESCR_BANDO,UTENZA_TIPO_DOC.*,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''VisModello.aspx?ID='||UTENZA_TIPO_DOC.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||'Visualizza'||'</a>','$','&'),'£','" & Chr(34) & "') as MODELLO1,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''TestModello.aspx?ID='||UTENZA_TIPO_DOC.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||'TEST'||'</a>','$','&'),'£','" & Chr(34) & "') as TEST FROM UTENZA_TIPO_DOC,UTENZA_BANDI WHERE UTENZA_BANDI.ID=UTENZA_TIPO_DOC.ID_BANDO and UTENZA_TIPO_DOC.ID_BANDO=" & sValoreBA & " order by UTENZA_TIPO_DOC.DESCRIZIONE ASC,ID_BANDO desc"
        BindGridModelli()
    End Function

    Private Sub BindGridModelli()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQLModelli, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_BANDI")
            DataGrid2.DataSource = ds
            DataGrid2.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim bTrovato1 As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        bTrovato1 = False
        sStringaSql = ""
        sStringaSqlD = ""
        sStringaSqlD2 = ""


        If sValoreADAL <> "" Then
            sStringaSqlD = "  SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8)>='" & sValoreADAL & "' "
            If sValoreAAL <> "" Then
                sStringaSqlD = sStringaSqlD & " AND SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8)<='" & sValoreAAL & "'"
            End If
        Else
            If sValoreAAL <> "" Then
                sStringaSqlD = sStringaSqlD & " AND SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8)<='" & sValoreAAL & "'"
            End If
        End If
        If sStringaSqlD <> "" Then
            'sStringaSqlD = sStringaSqlD & " AND NVL(id_stato,0)<>2 "
            bTrovato1 = True
        End If

        If sValoreFI <> "" Then
            sValore = sValoreFI
            If bTrovato1 = True Then
                bTrovato1 = True
                sStringaSqlD = sStringaSqlD & " AND NVL(AGENDA_APPUNTAMENTI.ID_SPORTELLO,-1)=" & par.PulisciStrSql(sValore) & " "
            Else
                sStringaSqlD = sStringaSqlD & " NVL(AGENDA_APPUNTAMENTI.ID_SPORTELLO,-1)=" & par.PulisciStrSql(sValore) & " "
            End If
        End If

        If sStringaSqlD <> "" Then
            sStringaSqlD = sStringaSqlD & " AND  "
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
            bTrovato = True
        End If

        If UCase(sValoreGD) = "TRUE" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sStringaSql = sStringaSql & " rapporti_utenza.ID NOT IN (SELECT id_contratto FROM siscom_mi.diffide_lettere WHERE id_contratto = rapporti_utenza.ID AND id_au = " & sValoreBA & ") "
            bTrovato = True
        End If

        If UCase(sValoreDV) = "TRUE" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sStringaSql = sStringaSql & " nvl(UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE,0)<>1 "
            bTrovato = True
        End If

        Dim CC As String = "("

        If UCase(sValoreM2) = "TRUE" Then
            CC = CC & " ID_MOTIVO_ANNULLO=3 OR "
        End If

        If UCase(sValoreM3) = "TRUE" Then
            CC = CC & " ID_MOTIVO_ANNULLO=6 OR "
        End If
        If UCase(sValoreM4) = "TRUE" Then
            CC = CC & " ID_MOTIVO_ANNULLO=4 OR "
        End If
        If UCase(sValoreM5) = "TRUE" Then
            CC = CC & " ID_MOTIVO_ANNULLO=1 OR "
        End If
        If UCase(sValoreM6) = "TRUE" Then
            CC = CC & " ID_MOTIVO_ANNULLO=2 OR "
        End If
        If UCase(sValoreM7) = "TRUE" Then
            CC = CC & " ID_MOTIVO_ANNULLO=5 OR "
        End If
        If UCase(sValoreM8) = "TRUE" Then
            CC = CC & " ID_MOTIVO_ANNULLO=0 OR "
        End If

        If CC <> "(" Then
            CC = "SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_STATO=1 AND " & Mid(CC, 1, Len(CC) - 3) & ") "
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sStringaSql = sStringaSql & " CONVOCAZIONI_AU.ID NOT IN (" & CC & ")"
        End If


        'sStringaSQL1 = "SELECT rapporti_utenza.ID AS idc, cod_contratto, anagrafica.cognome,anagrafica.nome, cod_tipologia_contr_loc AS tipologia," _
        '            & "TO_CHAR (TO_DATE (data_decorrenza, 'YYYYmmdd'),'DD/MM/YYYY') AS decorrenza," _
        '            & "TO_CHAR (TO_DATE (data_riconsegna, 'YYYYmmdd'),'DD/MM/YYYY') AS SLOGGIO," _
        '            & "indirizzi.descrizione AS indirizzo_unita,indirizzi.civico AS civico_unita, indirizzi.cap AS cap_unita," _
        '            & "indirizzi.localita AS comune_unita, UTENZA_SPORTELLI.DESCRIZIONE AS filiale " _
        '            & "FROM UTENZA_SPORTELLI,UTENZA_DICHIARAZIONI,siscom_mi.unita_contrattuale,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza,siscom_mi.indirizzi,siscom_mi.unita_immobiliari" _
        '            & " " _
        '            & "WHERE " _
        '            & "SUBSTR (cod_contratto, 1, 1) <> '4' " _
        '            & "AND SUBSTR (cod_contratto, 1, 6) <> '000000' " _
        '            & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
        '            & " AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
        '            & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
        '            & " AND indirizzi.ID = unita_immobiliari.id_indirizzo " _
        '            & "AND unita_immobiliari.ID = unita_contrattuale.id_unita " _
        '            & " AND unita_contrattuale.id_contratto = rapporti_utenza.ID " _
        '            & "AND unita_contrattuale.id_unita_principale IS NULL " _
        '            & "AND cod_contratto IS NOT NULL " _
        '            & "AND rapporti_utenza.ID NOT IN (SELECT id_contratto FROM siscom_mi.diffide_lettere WHERE id_contratto = rapporti_utenza.ID AND id_au = " & sValoreBA & ") " _
        '            & "	 AND rapporti_utenza.ID IN (SELECT DISTINCT id_contratto FROM siscom_mi.convocazioni_au WHERE " & sStringaSqlD & " AND (NVL(carico_ausi,0) <> 1 " _
        '            & "AND NVL(id_stato,0)<>2 AND NVL(id_motivo_annullo,0)<>1)  " _
        '            & " and id_gruppo IN (SELECT ID FROM siscom_mi.convocazioni_au_gruppi WHERE id_au = " & sValoreBA _
        '            & ")) AND rapporti_utenza.id NOT IN (SELECT DISTINCT id_contratto  FROM siscom_mi.convocazioni_au  WHERE " & sStringaSqlD2 & " and NVL(id_stato, 0) = 2) " _
        '            & "	and rapporti_utenza.cod_contratto in (select nvl(rapporto,'') from utenza_dichiarazioni where id_bando=" & sValoreBA _
        '            & ") and rapporti_utenza.id NOT IN (SELECT id_contratto FROM  siscom_mi.convocazioni_au WHERE " & sStringaSqlD1 & ") "

        sStringaSQL1 = "SELECT (TO_CHAR (TO_DATE (SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8), 'YYYYmmdd'),'DD/MM/YYYY')||' '||SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,9,2)||':'||SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,11,2)) AS APPUNTAMENTO,(SELECT DESCRIZIONE FROM UTENZA_SPORTELLI WHERE ID=AGENDA_APPUNTAMENTI.ID_SPORTELLO) AS FILIALE,RAPPORTI_UTENZA.ID AS IDC," _
                     & "RAPPORTI_UTENZA.COD_CONTRATTO,ANAGRAFICA.cognome,ANAGRAFICA.nome, RAPPORTI_UTENZA.cod_tipologia_contr_loc AS TIPOLOGIA," _
                     & "TO_CHAR (TO_DATE (RAPPORTI_UTENZA.data_decorrenza, 'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA," _
                     & "TO_CHAR (TO_DATE (RAPPORTI_UTENZA.data_riconsegna, 'YYYYmmdd'),'DD/MM/YYYY') AS SLOGGIO," _
                     & "indirizzi.descrizione AS indirizzo_unita,indirizzi.civico AS civico_unita, indirizzi.cap AS cap_unita,indirizzi.localita as comune_unita " _
                     & "FROM " _
                     & "SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.AGENDA_APPUNTAMENTI,SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, siscom_mi.unita_contrattuale, " _
                     & "siscom_mi.indirizzi, siscom_mi.unita_immobiliari " _
                     & "WHERE " & sStringaSqlD & " CONVOCAZIONI_AU.ID=AGENDA_APPUNTAMENTI.ID_CONVOCAZIONE AND AGENDA_APPUNTAMENTI.COD_CONTRATTO IS NOT NULL AND AGENDA_APPUNTAMENTI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                     & "AGENDA_APPUNTAMENTI.ID_AU = " & sValoreBA & " " _
                     & " and rapporti_utenza.cod_contratto not in (select rapporto from utenza_dichiarazioni where id_bando=" & sValoreBA & ") " _
                     & "AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                     & "AND indirizzi.ID = unita_immobiliari.id_indirizzo AND unita_immobiliari.ID = unita_contrattuale.id_unita AND unita_contrattuale.id_contratto = rapporti_utenza.ID " _
                     & "AND unita_contrattuale.id_unita_principale IS NULL"

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

    Public Property sStringaSQLModelli() As String
        Get
            If Not (ViewState("par_sStringaSQLModelli") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQLModelli"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQLModelli") = value
        End Set

    End Property

    Protected Sub DataGrid2_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('LBLIDmodello').value='" & e.Item.Cells(0).Text & "';")


        End If
    End Sub

    Protected Sub DataGrid2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid2.SelectedIndexChanged

    End Sub

    Protected Sub btnSelezionaTutti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = True
        Next
    End Sub

    Protected Sub btnDeselezionaTutti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDeselezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = False
        Next
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
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "SPORTELLO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "APPUNTAMENTO DEL")
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
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("APPUNTAMENTO"), ""))

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

    Private Function DatiBando()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & sValoreBA
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                sNomeBandoAU = par.IfNull(myReader("DESCRIZIONE"), "")
                sAnnoAU = par.IfNull(myReader("ANNO_AU"), "")
                sAnnoIsee = par.IfNull(myReader("ANNO_ISEE"), "")
            End If
            myReader.Read()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaNonRispondenti.aspx""</script>")
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click

        Dim j As Integer = 0


        Dim zipfic As String = ""
        Dim ElencoFile() As String
        Dim ElencoContratti As String = ""
        Dim NumeroLettere As Long = 1000000
        Dim ContatorePagine As Long = 0

        Dim Contatore As Long = 0
        Dim NUMERORIGHE As Long = 0

        Dim Str As String = ""
        Dim ZIPFile As String = ""
        Dim ZIPFileSimulazione As String = ""

        Dim basefileS As String = ""

        Dim Simulazione As Boolean = True

        If cmbSimulazione.SelectedItem.Value = "NO" Then
            Simulazione = False
        End If

        'postaler
        Dim sPosteAler As String = ""
        Dim sPosteAlerNominativo As String = ""
        Dim sPosteAlerInd As String = ""
        Dim sPosteAlerScala As String = ""
        Dim sPosteAlerInterno As String = ""
        Dim sPosteAlerCAP As String = ""
        Dim sPosteAlerLocalita As String = ""
        Dim sPosteAlerProv As String = ""
        Dim sPosteAlerCodUtente As String = ""
        Dim sPosteAlerAcronimo As String = ""
        Dim sPosteDefault As String = ""
        Dim sPosteAlerIA As String = ""
        Dim NomeFilePosteAler As String = ""
        Dim sPosteIndirizzoPostale As String = ""
        Dim TestoPostAler As String = ""
        Dim bloccoStampa As Integer = 0
        Dim Identificativo As String = ""
        Dim documentazionemancante As String = ""
        Dim NumeroFileDiffida As String = ""
        Dim IndiceDichiarazione As String = ""




        If txtDataStampa.Text <> "" And txtNumPG.Text <> "" And LBLIDmodello.Value <> "" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...Attendere il messaggio di fine operazione</br></div></script>"

                Response.Write(Str)
                Response.Flush()


                Dim MIADATA As String = Format(Now, "yyyyMMddHHmmss")
                Dim BaseFile As String = Format(CLng(sValoreBA), "000000") & "_" & Format(CLng(sValoreFI), "000000") & "_" & MIADATA
                basefileS = BaseFile
                NomeFilePosteAler = Server.MapPath("..\FileTemp\") & BaseFile & ".txt"
                zipfic = Server.MapPath("..\FileTemp\") & BaseFile & ".pdf"
                ZIPFile = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\DIFFIDE\") & BaseFile & ".zip"
                ZIPFileSimulazione = Server.MapPath("..\FileTemp\") & BaseFile & ".zip"

                Dim file1 As String = BaseFile & ".RTF"
                Dim fileName As String = Server.MapPath("..\FileTemp\") & file1
                Dim contenuto As String = ""
                Dim rp As New Rpn
                Dim i As Long = 0
                Dim K As Integer = 0

                'Dim result As Integer = Rpn.RpsSetLicenseInfo("G927S-F6R7A-7VH31", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                Dim result As Int64 = Rpn.RpsSetLicenseInfo("8RWQS-6Y9UC-HA2L1-91017", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                rp.InWebServer = True
                rp.EmbedFonts = True
                rp.ExactTextPlacement = True


                Dim trovato As Boolean = False


                Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
                pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait


                Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)
                If Simulazione = False Then
                    Dim Licenza As String = Session.Item("LicenzaPdfMerge")
                    If Licenza <> "" Then
                        pdfMerge.LicenseKey = Licenza
                    End If
                End If

                par.cmd.CommandText = "SELECT * FROM UTENZA_TIPO_DOC WHERE id=" & LBLIDmodello.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Dim bw As BinaryWriter
                    If par.IfNull(myReader("MODELLO"), "").LENGTH > 0 Then
                        Dim fs As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                        bw = New BinaryWriter(fs)
                        bw.Write(myReader("MODELLO"))
                        bw.Flush()
                        bw.Close()
                        trovato = True
                    End If
                End If
                myReader.Close()

                Dim Tempistica As String = ""

                Dim contenutoOriginale As String = ""
                If trovato = True Then
                    Dim sr1 As StreamReader = New StreamReader(fileName, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenutoOriginale = sr1.ReadToEnd()
                    sr1.Close()
                End If

                Dim chkExport As System.Web.UI.WebControls.CheckBox
                ElencoContratti = "( RAPPORTI_UTENZA.ID IN ("
                NUMERORIGHE = 0
                K = 0
                i = 0
                For Each oDataGridItem In Me.DataGrid1.Items
                    chkExport = oDataGridItem.FindControl("ChSelezionato")
                    If chkExport.Checked Then
                        i = i + 1
                        NUMERORIGHE = i
                        K = K + 1
                        If K > 990 Then
                            ElencoContratti = ElencoContratti & oDataGridItem.Cells(3).Text & ","
                            ElencoContratti = Mid(ElencoContratti, 1, Len(ElencoContratti) - 1) & ") OR RAPPORTI_UTENZA.ID IN ( "
                            K = 0
                        Else
                            ElencoContratti = ElencoContratti & oDataGridItem.Cells(3).Text & ","
                        End If
                    End If
                Next

                K = 0
                If trovato = True And i > 0 Then
                    Dim sr2 As StreamWriter = New StreamWriter(NomeFilePosteAler, False, System.Text.Encoding.Default)
                    par.cmd.CommandText = "select valore from parameter where id=119"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Tempistica = par.IfNull(myReader("valore"), "0")
                    End If
                    myReader.Close()

                    If Simulazione = False Then
                        par.cmd.CommandText = "INSERT INTO UTENZA_FILE_DIFFIDE (ID,DATA_CREAZIONE,CRITERI,ID_SPORTELLO,DESCR_SPORTELLO,NOME_FILE,TIPO,ID_AU,PROTOCOLLO,DATA_ANNULLO,NOTE) VALUES (SEQ_FILE_DIFFIDE.NEXTVAL,'" & MIADATA & "','" & par.PulisciStrSql(sCriteri) & "'," & sValoreFI & ",'','" & BaseFile & ".zip',1," & sValoreBA & ",'" & par.PulisciStrSql(txtNumPG.Text) & "','','')"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "select SEQ_FILE_DIFFIDE.CURRVAL FROM dual"
                        myReader = par.cmd.ExecuteReader()
                        If myReader.Read Then
                            NumeroFileDiffida = par.IfNull(myReader(0), "-1")
                        End If
                        myReader.Close()
                    End If


                    ElencoContratti = Mid(ElencoContratti, 1, Len(ElencoContratti) - 1) & "))"

                    par.cmd.CommandText = "SELECT " _
                                        & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.ID AS IDA,(SELECT REPLACE(UPPER(DESCRIZIONE),'FUORI TERRA','') FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO) AS PIANO, " _
                                        & " (SELECT DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID=UNITA_IMMOBILIARI.ID_SCALA) AS SCALA,  " _
                                        & " UNITA_IMMOBILIARI.INTERNO,INDIRIZZI.DESCRIZIONE AS UN_INDIRIZZO,INDIRIZZI.CIVICO AS UN_CIVICO,INDIRIZZI.CAP AS UN_CAP,INDIRIZZI.LOCALITA AS UN_LOCALITA, " _
                                        & " UTENZA_SPORTELLI.DESCRIZIONE AS DESCR_SPORTELLO,UTENZA_SPORTELLI.INDIRIZZO AS INDIRIZZO_SPORTELLO,UTENZA_SPORTELLI.CIVICO AS CIVICO_SPORTELLO,UTENZA_SPORTELLI.CAP AS CAP_SPORTELLO, " _
                                        & " (SELECT NOME FROM COMUNI_NAZIONI WHERE ID=UTENZA_SPORTELLI.ID_COMUNE) AS CITTA_SPORTELLO,UTENZA_SPORTELLI.N_VERDE,UTENZA_SPORTELLI.N_TELEFONO,UTENZA_SPORTELLI.N_FAX, " _
                                        & " TAB_FILIALI.RESPONSABILE,TAB_FILIALI.REF_AMMINISTRATIVO,tab_filiali.acronimo, " _
                                        & " RAPPORTI_UTENZA.ID AS IDC,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.PRESSO_COR,RAPPORTI_UTENZA.VIA_COR,RAPPORTI_UTENZA.CIVICO_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.SIGLA_COR,rapporti_utenza.tipo_cor " _
                                        & " FROM  " _
                                        & " UTENZA_FILIALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.TAB_FILIALI,SISCOM_MI.RAPPORTI_UTENZA,UTENZA_SPORTELLI,SISCOM_MI.INDIRIZZI " _
                                        & " WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                                        & " AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND TAB_FILIALI.ID = UTENZA_FILIALI.ID_STRUTTURA  " _
                                        & " AND UTENZA_FILIALI.ID=UTENZA_SPORTELLI.id_filiale AND UTENZA_SPORTELLI.ID=" & sValoreFI & " AND  " & ElencoContratti
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader1.Read
                        'postaler
                        sPosteAler = ""
                        sPosteAlerNominativo = ""
                        sPosteAlerInd = ""
                        sPosteAlerScala = ""
                        sPosteAlerInterno = ""
                        sPosteAlerCAP = ""
                        sPosteAlerLocalita = ""
                        sPosteAlerProv = ""
                        sPosteAlerCodUtente = ""
                        sPosteAlerAcronimo = ""
                        sPosteDefault = ""
                        sPosteAlerIA = ""
                        sPosteIndirizzoPostale = ""

                        If Simulazione = False Then
                            par.cmd.CommandText = "select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual"
                            Dim myReaderAler As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAler.Read Then
                                sPosteAlerIA = CStr(par.IfNull(myReaderAler(0), "-1")).PadRight(16)
                            End If
                            myReaderAler.Close()
                        Else
                            sPosteAlerIA = "0"
                        End If

                        sPosteIndirizzoPostale = Trim(Mid(Replace(UCase(par.IfNull(myReader1("PRESSO_COR"), "")), "C/O", ""), 1, 50)).PadRight(50)
                        sPosteDefault = "                                                  "
                        sPosteAlerInd = CStr(par.IfNull(myReader1("TIPO_COR"), "") & " " & par.IfNull(myReader1("VIA_COR"), "") & " " & par.IfNull(myReader1("CIVICO_COR"), "")).PadRight(50).Substring(0, 50)
                        sPosteAlerCodUtente = Format(par.IfNull(myReader1("ida"), ""), "000000000000").PadRight(12)
                        sPosteAlerInterno = CStr(Mid(par.IfNull(myReader1("INTERNO"), ""), 1, 3)).PadRight(3)
                        sPosteAlerScala = Replace(Mid(par.IfNull(myReader1("SCALA"), ""), 1, 2), "00", "", 1, 2).PadRight(2)
                        sPosteAlerAcronimo = CStr(par.IfNull(myReader1("ACRONIMO"), "")).PadRight(4)
                        sPosteAlerCAP = CStr(par.IfNull(myReader1("CAP_COR"), "")).PadRight(5)
                        sPosteAlerLocalita = CStr(Mid(par.IfNull(myReader1("LUOGO_COR"), ""), 1, 50)).PadRight(50)
                        sPosteAlerProv = CStr(par.IfNull(myReader1("SIGLA_COR"), "")).PadRight(2)

                        sPosteAler = sPosteIndirizzoPostale & ";" _
                                   & sPosteDefault & ";" _
                                   & sPosteAlerInd & ";" _
                                   & sPosteDefault & ";" _
                                   & sPosteDefault & ";" _
                                   & sPosteAlerScala & ";" _
                                   & sPosteAlerInterno & ";" _
                                   & sPosteAlerCAP & ";" _
                                   & sPosteAlerLocalita & ";" _
                                   & sPosteAlerProv & ";" _
                                   & sPosteAlerCodUtente & ";" _
                                   & sPosteAlerAcronimo & ";" _
                                   & sPosteAlerIA & ";"

                        If Simulazione = False Then
                            par.cmd.CommandText = "Insert into SISCOM_MI.DIFFIDE_LETTERE (ID, ID_CONTRATTO, ID_AU, DATA_GENERAZIONE, TIPO) Values (SISCOM_MI.SEQ_DIFFIDE_LETTERE.NEXTVAL, " & par.IfNull(myReader1("IDC"), "") & ", " & sValoreBA & " , '" & Mid(MIADATA, 1, 8) & "',  1)"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "select SISCOM_MI.SEQ_DIFFIDE_LETTERE.CURRVAL FROM dual"
                            Dim myReaderAler As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAler.Read Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) VALUES (" & sPosteAlerIA & "," & myReaderAler(0) & ",2)"
                                par.cmd.ExecuteNonQuery()
                            End If
                            myReaderAler.Close()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & par.IfNull(myReader1("IDC"), "") & "," & Session.Item("ID_OPERATORE") & ", '" & MIADATA & "', 'F227', '" & par.PulisciStrSql(sNomeBandoAU) & "')"
                            par.cmd.ExecuteNonQuery()
                        End If

                        contenuto = contenutoOriginale

                        IndiceDichiarazione = "null"
                        documentazionemancante = "\par "
                        par.cmd.CommandText = "select * from UTENZA_DOC_MANCANTE where id_dichiarazione=(select id from utenza_dichiarazioni where id_bando=" & sValoreBA & " and rapporto='" & par.IfNull(myReader1("COD_CONTRATTO"), "") & "')"
                        Dim myReaderDoc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReaderDoc.Read
                            documentazionemancante = documentazionemancante & "-" & par.IfNull(myReaderDoc("descrizione"), "") & "\par "
                            IndiceDichiarazione = par.IfNull(myReaderDoc("id_dichiarazione"), "")
                        Loop
                        myReaderDoc.Close()

                        If Simulazione = False Then
                            par.cmd.CommandText = "INSERT INTO UTENZA_FILE_DIFFIDE_DETT (ID_FILE_DIFFIDE,ID_CONTRATTO,ID_DICHIARAZIONE) VALUES (" & NumeroFileDiffida & "," & par.IfNull(myReader1("IDC"), "") & "," & IndiceDichiarazione & ")"
                            par.cmd.ExecuteNonQuery()
                        End If

                        contenuto = Replace(contenuto, "$documentimancanti$", documentazionemancante)
                        contenuto = Replace(contenuto, "$tempisticanonrispondenti$", Tempistica)

                        contenuto = Replace(contenuto, "$testoresponsabile$", "Il Responsabile")
                        contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
                        contenuto = Replace(contenuto, "$datastampa$", txtDataStampa.Text)
                        contenuto = Replace(contenuto, "$dataappuntamento$", "")
                        contenuto = Replace(contenuto, "$oreappuntamento$", "")
                        contenuto = Replace(contenuto, "$annoau$", sAnnoAU)
                        contenuto = Replace(contenuto, "$annoredditi$", sAnnoIsee)
                        contenuto = Replace(contenuto, "$dichiarante$", "")
                        contenuto = Replace(contenuto, "$datanascitadichiarante$", "")
                        contenuto = Replace(contenuto, "$luogonascitadichiarante$", "")
                        contenuto = Replace(contenuto, "$provincianascitadichiarante$", "")

                        contenuto = Replace(contenuto, "$codcontratto$", par.IfNull(myReader1("COD_CONTRATTO"), ""))
                        contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""))
                        contenuto = Replace(contenuto, "$nominativocorr$", Mid(par.IfNull(myReader1("PRESSO_COR"), ""), 1, 40))
                        contenuto = Replace(contenuto, "$indirizzocorr$", Mid(par.IfNull(myReader1("TIPO_COR"), "") & " " & par.IfNull(myReader1("VIA_COR"), "") & " " & par.IfNull(myReader1("CIVICO_COR"), ""), 1, 40))
                        contenuto = Replace(contenuto, "$localitacorr$", par.IfNull(myReader1("LUOGO_COR"), ""))

                        contenuto = Replace(contenuto, "$internoscalapiano$", Replace("INT. " & par.IfNull(myReader1("interno"), "") & " Scala:" & par.IfNull(myReader1("scala"), "") & " Piano:" & par.IfNull(myReader1("piano"), ""), "MEZZANINO", "MEZ."))
                        contenuto = Replace(contenuto, "$internoscalapianocorr$", Replace("INT. " & par.IfNull(myReader1("interno"), "") & " Scala:" & par.IfNull(myReader1("scala"), "") & " Piano:" & par.IfNull(myReader1("piano"), ""), "MEZZANINO", "MEZ."))
                        contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader1("interno"), ""))
                        contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader1("scala"), ""))
                        contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader1("piano"), ""))
                        contenuto = Replace(contenuto, "$indirizzounita$", Mid(par.IfNull(myReader1("UN_INDIRIZZO"), ""), 1, 23))
                        contenuto = Replace(contenuto, "$localitaunita$", par.IfNull(myReader1("UN_LOCALITA"), ""))
                        contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader1("DESCR_SPORTELLO"), ""))
                        contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader1("INDIRIZZO_SPORTELLO"), "") & ", " & par.IfNull(myReader1("CIVICO_SPORTELLO"), ""))
                        contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader1("CAP_SPORTELLO"), ""))
                        contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader1("CITTA_SPORTELLO"), ""))
                        contenuto = Replace(contenuto, "$telfax$", "Tel:" & par.IfNull(myReader1("N_TELEFONO"), "") & " - Fax:" & par.IfNull(myReader1("N_FAX"), ""))
                        contenuto = Replace(contenuto, "$referente$", par.IfNull(myReader1("REF_AMMINISTRATIVO"), ""))
                        contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                        contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader1("RESPONSABILE"), ""))
                        contenuto = Replace(contenuto, "$numverdefiliale$", par.IfNull(myReader1("N_VERDE"), ""))
                        'contenuto = Replace(contenuto, "$filialeappartenenza$", par.IfNull(myReader2("DATI_FILIALE"), ""))
                        contenuto = Replace(contenuto, "$acronimo$", "")
                        contenuto = Replace(contenuto, "$protocollo$", txtNumPG.Text)
                        contenuto = Replace(contenuto, "$cds$", "GL0000/" & par.PulisciStrSql(par.IfNull(myReader1("ACRONIMO"), "")) & "/" & par.PulisciStrSql(txtNumPG.Text))

                        If Simulazione = False Then
                            sr2.WriteLine(Replace(sPosteAler, "''", "'") & vbCrLf)
                        End If
                        'TestoPostAler = TestoPostAler & Replace(sPosteAler, "''", "'") & vbCrLf

                        BaseFile = sValoreBA & "_" & par.IfNull(myReader1("IDC"), "") & "_" & MIADATA
                        file1 = BaseFile & ".RTF"
                        fileName = Server.MapPath("..\FileTemp\") & file1
                        Dim basefilePDF As String = BaseFile & ".pdf"
                        Dim fileNamePDF As String = Server.MapPath("..\FileTemp\") & basefilePDF

                        Dim sr As StreamWriter = New StreamWriter(fileName, False, System.Text.Encoding.Default)
                        sr.WriteLine(contenuto)
                        sr.Close()

                        i = rp.RpsConvertFile(fileName, fileNamePDF)

                        ReDim Preserve ElencoFile(K)
                        ElencoFile(K) = fileNamePDF
                        K = K + 1

                        Contatore = Contatore + 1
                        Response.Flush()
                    Loop
                    myReader1.Close()

                    If Simulazione = False Then
                        'chiusura file postaler
                        sr2.Close()
                    End If

                    'raggruppo tutti i pdf in un solo file pdf
                    For j = 0 To K - 1
                        pdfMerge.AppendPDFFile(ElencoFile(j))
                    Next
                    pdfMerge.SaveMergedPDFToFile(zipfic)

                    'creo il file zip
                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream

                    If Simulazione = False Then
                        strmZipOutputStream = New ZipOutputStream(File.Create(ZIPFile))
                    Else
                        strmZipOutputStream = New ZipOutputStream(File.Create(ZIPFileSimulazione))
                    End If
                    strmZipOutputStream.SetLevel(6)
                    Dim strFile As String

                    strFile = zipfic
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

                    If Simulazione = False Then
                        strFile = NomeFilePosteAler
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                        sFile = Path.GetFileName(strFile)
                        theEntry = New ZipEntry(sFile)
                        fi = New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer1)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
                    End If
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                    'File.Delete(zipfic)
                    'File.Delete(NomeFilePosteAler)


                Else
                    Response.Write("<script>alert('Attenzione...Selezionare almeno un contratto da diffidare!');</script>")
                End If


                If Simulazione = False Then
                    Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';document.location.href = 'ElencoDiffideIncomplete.aspx?T=1';</script>"
                Else
                    Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';document.location.href = 'SimulazioneDiffida.aspx?ID=" & par.Cripta(basefileS) & "';</script>"
                End If

                Response.Write(Str)
                Response.Flush()


                'par.myTrans.Rollback()
                par.myTrans.Commit()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:RisultatoNonRispondenti.aspx - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)

            End Try
        Else
            Response.Write("<script>alert('Attenzione...Specificare il modello da utilizzare, il numero di protocollo, la data da apporre sulle lettere di diffida!');</script>")
        End If
    End Sub

    Protected Sub imgExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
        Export()
    End Sub
End Class
