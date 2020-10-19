Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CENSIMENTO_RisultatiUI2
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String = ""
    Dim vEdificio As String
    Dim vIndirizzo As String
    Dim vCivico As String
    Dim vTipo As String
    Dim vComplesso As String
    Dim vInterno As String
    Dim vAsc As String
    Dim vDisp As String
    Dim vScala As String
    Dim vCiv As String
    Dim vDest As String
    Dim vHand As String
    Dim vSNDA As String
    Dim vSNA As String
    Dim vPRGeventi As String = ""
    Dim vREN As String
    Dim vZona As String = ""
    Dim vZosmi As String = ""


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

            Response.Flush()

            vPRGeventi = Request.QueryString("PRG")
            vEdificio = Request.QueryString("E")
            vIndirizzo = Request.QueryString("I")
            vCivico = Request.QueryString("CIV")
            vTipo = Request.QueryString("TIPOL")
            vComplesso = Request.QueryString("COMP")
            vInterno = Request.QueryString("INT")
            vAsc = Request.QueryString("ASC")
            vDisp = Request.QueryString("DISP")
            vScala = Request.QueryString("SCAL")
            vCiv = Request.QueryString("IDIND")
            vDest = Request.QueryString("DEST")
            vHand = Request.QueryString("HAN")
            vSNDA = par.DeCripta(Request.QueryString("SNDA"))
            vSNA = par.DeCripta(Request.QueryString("SNA"))
            VREN = Request.QueryString("REN")
            vZona = Request.QueryString("ZONA")
            vZosmi = Request.QueryString("ZOSMI")
            'LBLID.Text = "-1"
            LblEdificio.Text = vEdificio
            If vEdificio <> "-1" Then
                If Request.QueryString("T") = "0" Then
                    CercaSelettiva()

                Else
                    isSelettiva.Value = 1
                    cerca()

                End If

            End If
            ' If Session.Item("LIVELLO") = 1 Then
            Me.btnExport.Visible = True
            'End If


        End If

    End Sub
    Private Sub cerca()

        Dim bTrovato As Boolean
        Dim sValore As String
        Dim condizione As String = ""
        Try
            bTrovato = False
            sStringaSql = ""
            sStringaSql = "SELECT DISTINCT ROWNUM,unita_immobiliari.id as id_UI,(select zona from sepa.zona_aler where cod = id_zona) as zona,  EDIFICI.DENOMINAZIONE, SISCOM_MI.UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE ,UNITA_IMMOBILIARI.INTERNO,  (SCALE_EDIFICI.DESCRIZIONE) AS SCALA ,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,UNITA_IMMOBILIARI.S_NETTA,NVL(TO_CHAR(identificativi_catastali.rendita),'NO') AS RENDITA,(INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO) AS INDIRIZZO,INDIRIZZI.LOCALITA AS COMUNE,INDIRIZZI.CAP,(select TAB_FILIALI.NOME from siscom_mi.tab_filiali,siscom_mi.filiali_ui where filiali_ui.id_filiale = tab_filiali.id and unita_immobiliari.id = filiali_ui.id_ui and FILIALI_UI.fine_validita >to_char(sysdate,'yyyymmdd') ) as FILIALE,TIPO_DISPONIBILITA.DESCRIZIONE AS TIPO_DISP,PROGRAMMAZIONE_INTERVENTI.DESCRIZIONE AS PRG_INTERVENTI, " _
                        & "(select descrizione from siscom_mi.DESTINAZIONI_USO_UI where DESTINAZIONI_USO_UI.ID = UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO) as dest_uso,(select descrizione from siscom_mi.tab_zona_osmi where id = edifici.id_osmi) as ZOSMI FROM SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.identificativi_catastali,siscom_mi.tipo_disponibilita,siscom_mi.PROGRAMMAZIONE_INTERVENTI " _
                        & " where PROGRAMMAZIONE_INTERVENTI.ID (+)=UNITA_IMMOBILIARI.ID_PRG_EVENTI AND TIPO_DISPONIBILITA.COD (+)=UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA AND SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) = UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND UNITA_IMMOBILIARI.ID_SCALA= SCALE_EDIFICI.ID(+) AND TIPOLOGIA_UNITA_IMMOBILIARI.COD= UNITA_IMMOBILIARI.COD_TIPOLOGIA and SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO and SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID (+) and SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+)AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND EDIFICI.ID <> 1  "
            sStringaSql = sStringaSql & " "

            If vEdificio <> "" And vEdificio <> "-1" Then
                sValore = vEdificio
                'sValore = Mid(sValore, 1, InStr(sValore, "-") - 1)

                bTrovato = True
                condizione = "AND UNITA_IMMOBILIARI.ID_EDIFICIO = '" & par.PulisciStrSql(sValore) & "'"
            End If

            If Not IsNothing(Request.QueryString("COND")) Then
                If Request.QueryString("COND") <> "-1" Then
                    condizione = "AND EDIFICI.CONDOMINIO = '" & Request.QueryString("COND") & "'"
                End If
            End If
            If vComplesso <> "" And vComplesso <> "-1" Then
                sValore = vComplesso
                bTrovato = True
                condizione = condizione & "AND EDIFICI.ID_COMPLESSO =" & sValore
            End If
            If vZona <> "-1" And IsNothing(vZona) = False Then
                condizione = condizione & " and edifici.id_zona = " & vZona
            End If
            If vZosmi <> "-1" And IsNothing(vZosmi) = False Then
                condizione = condizione & " and edifici.id_osmi = " & vZosmi

            End If

            If par.IfEmpty(vIndirizzo, "Null") <> "Null" Then
                sValore = vIndirizzo
                condizione = condizione & "AND EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & (sValore) & "' "
                If par.IfEmpty(vCivico, "Null") <> "Null" Then
                    sValore = vCivico
                    condizione = condizione & "AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
                End If
                condizione = condizione & ")"
            End If
            '*******MODIFICA PER LA RICERCA ELIMINANDO I DOPPIONI DEI CIVICI
            If par.IfEmpty(Request.QueryString("IND"), "Null") <> "Null" Then
                sValore = Request.QueryString("IND")
                condizione = condizione & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & (sValore) & "' "
                If par.IfEmpty(vCiv, "Null") <> "Null" Then
                    sValore = vCiv
                    condizione = condizione & "AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
                End If
                condizione = condizione & ")"
            End If



            If par.IfEmpty(vCivico, "Null") <> "Null" Then
                sValore = vCiv
                condizione = condizione & " AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = " & vCiv
            End If
            If par.IfEmpty(vScala, "Null") <> "Null" Then
                sValore = vScala
                condizione = condizione & " AND UNITA_IMMOBILIARI.ID_SCALA = " & vScala
            End If


            'PARAMETRO DI RICERCA ASCENSORE

            If vAsc <> "-1" AndAlso vAsc <> "" Then
                If vAsc = 0 Then
                    condizione = condizione & " AND UNITA_IMMOBILIARI.id_scala NOT IN (SELECT DISTINCT id_scala FROM siscom_mi.IMPIANTI_SCALE,siscom_mi.IMPIANTI WHERE IMPIANTI_SCALE.id_impianto=IMPIANTI.ID AND IMPIANTI.cod_tipologia='SO') "
                ElseIf vAsc = 1 Then
                    condizione = condizione & " AND UNITA_IMMOBILIARI.id_scala IN (SELECT DISTINCT id_scala FROM siscom_mi.IMPIANTI_SCALE,siscom_mi.IMPIANTI WHERE IMPIANTI_SCALE.id_impianto=IMPIANTI.ID AND IMPIANTI.cod_tipologia='SO') "
                End If
            End If


            'PARAMETRO DI RICERCA HANDICAP

            If vHand <> "-1" AndAlso vHand <> "" Then
                If vHand = 0 Then
                    condizione = condizione & " AND 'NO'=(CASE WHEN (SELECT DISTINCT UNITA_STATO_MANUTENTIVO.ID_UNITA FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND UNITA_STATO_MANUTENTIVO.ID_UNITA = UI.ID  AND UNITA_STATO_MANUTENTIVO.HANDICAP=0)>0 THEN 'NO' END) "
                ElseIf vHand = 1 Then
                    condizione = condizione & " AND 'SI'=(CASE WHEN (SELECT DISTINCT UNITA_STATO_MANUTENTIVO.ID_UNITA FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND UNITA_STATO_MANUTENTIVO.ID_UNITA = UI.ID AND UNITA_STATO_MANUTENTIVO.HANDICAP=1)>0 THEN 'SI' END) "
                End If
            End If


            'PARAMETRO DI RICERCA STATO DISPONIBILITA

            If vDisp <> "-1" AndAlso vDisp <> "" Then
                condizione = condizione & " AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = '" & vDisp & "' "
            End If

            If vPRGeventi <> "-1" AndAlso vPRGeventi <> "" AndAlso UCase(vPRGeventi) <> "NULL" Then
                condizione = condizione & " AND UNITA_IMMOBILIARI.ID_PRG_EVENTI = " & vPRGeventi & " "
            End If

            If Not IsNothing(vDest) Then
                If vDest <> "-1" AndAlso vDisp <> "" Then
                    condizione = condizione & " AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = '" & vDest & "' "
                End If

            End If

            Dim SSTRAPPOGGIO As String = ""
            If vSNDA <> "" Then
                SSTRAPPOGGIO = " UNITA_IMMOBILIARI.S_NETTA>=" & par.VirgoleInPunti(vSNDA)
            End If
            If vSNA <> "" Then
                If SSTRAPPOGGIO <> "" Then
                    SSTRAPPOGGIO = SSTRAPPOGGIO & " AND UNITA_IMMOBILIARI.S_NETTA<=" & par.VirgoleInPunti(vSNA)
                Else
                    SSTRAPPOGGIO = "UNITA_IMMOBILIARI.S_NETTA<=" & par.VirgoleInPunti(vSNA)
                End If
            End If

            If SSTRAPPOGGIO <> "" Then
                condizione = condizione & " AND " & SSTRAPPOGGIO & " "
            End If


            'If vTipo <> "" And vTipo <> "-1" Then
            '    sValore = vTipo
            '    condizione = condizione & "AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='" & sValore & "'"
            'End If
            If Session.Item("TIPOLOGIA") <> "" Then
                condizione = condizione & " AND ( " & Session.Item("TIPOLOGIA") & ") "
            End If



            If par.IfEmpty(vInterno, "Null") <> "Null" Then
                sValore = vInterno
                condizione = condizione & " AND UNITA_IMMOBILIARI.INTERNO ='" & par.PulisciStrSql(sValore) & "' "

            End If

            If vREN = "1" Then
                condizione = condizione & " AND NVL(IDENTIFICATIVI_CATASTALI.RENDITA,0)<>0 "
            End If

            If vREN = "0" Then
                condizione = condizione & " AND NVL(IDENTIFICATIVI_CATASTALI.RENDITA,0)=0 "
            End If
            If Not IsNothing(Request.QueryString("SEDE")) Then
                If Request.QueryString("SEDE") <> "-1" Then
                    condizione = condizione & " AND EXISTS (SELECT * FROM SISCOM_MI.FILIALI_UI,siscom_mi.tab_filiali WHERE UNITA_IMMOBILIARI.ID = FILIALI_UI.ID_UI AND TAB_FILIALI.ID = FILIALI_UI.ID_FILIALE AND TAB_FILIALI.ID = " & Request.QueryString("SEDE") & " and FILIALI_UI.fine_validita >to_char(sysdate,'yyyymmdd'))"
                End If
            End If

            If condizione <> "" Then
                sStringaSql = sStringaSql & condizione

            End If
            sStringaSql = sStringaSql & " ORDER BY DENOMINAZIONE ASC, SCALA ASC, INTERNO ASC"
            QUERY = sStringaSql
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub CercaSelettiva()

        Try
            QUERY = Session.Item("PED")
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Public Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property
    Private Sub BindGrid()

        par.OracleConn.Open()
        If isSelettiva.Value = "0" Then
            Me.DataGrid1.Columns.RemoveAt(17)
        End If

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "UNITA_COMUNI, INDIRIZZI,COMUNI_NAZIONI")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        Session.Add("dtExport", ds.Tables(0))
        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        If Session.Item("PED") <> Nothing Then
            Response.Write("<script>document.location.href=""DirettaUI.aspx""</script>")
            Session.Remove("PED")

        Else
            Response.Write("<script>document.location.href=""RicercaUI.aspx""</script>")
        End If

    End Sub

    'Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    '    LBLID.Text = e.Item.Cells(0).Text
    '    Label2.Text = "Hai selezionato la riga n°: " & e.Item.Cells(1).Text

    'End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità COD. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità COD. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna Unita selezionata!')</script>")
        Else
            Response.Redirect("InserimentoUniImmob.aspx?C=RisultatiUI&COMPLESSO=" & Request.QueryString("COMP") & "&EDIFICIO=" & Request.QueryString("E") & "&IND=" & Request.QueryString("IND") & "&IDIND=" & Request.QueryString("IDIND") & "&INT=" & Request.QueryString("INT") & "&CIVICO=" & Request.QueryString("CIV") & "&SCAL=" & Request.QueryString("SCAL") & "&ID=" & txtid.Text & "&TIPOL=" & Request.QueryString("TIPOL") & "&T=" & Request.QueryString("T") & "&DISP=" & Request.QueryString("DISP"))
        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        'Response.Write("<script> window.open('CENSIMENTO/InserimentoComplessi.aspx?SLE=1&ID=100000001', 'Dettagliss', 'height=580,top=0,left=0,width=780');</script>")


    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim dt As New Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("dtExport"), Data.DataTable)
            sNomeFile = "ExpElencoUI_" & Format(Now, "yyyyMMddHHmmss")

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


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "ID U.I.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "CODICE U.I.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "EDIFICIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "ZONA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "FOGLIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "PARTICELLA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "SUB.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "DESCRIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "PIANO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "COMUNE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "SUP.NETTA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "RENDITA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "DISPONIBILITA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "PROGR.INTERVENTI", 12)
                'If isSelettiva.Value = "1" Then
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "SEDE TERR.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "DEST. USO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "Z. OSMI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "CAP", 12)

                'End If

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(row.Item("ID_UI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(row.Item("COD_UNITA_IMMOBILIARE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(row.Item("DENOMINAZIONE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(row.Item("ZONA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(row.Item("FOGLIO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(row.Item("NUMERO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(row.Item("SUB"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(row.Item("DESCRIZIONE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(row.Item("INDIRIZZO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(row.Item("INTERNO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(row.Item("SCALA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(row.Item("PIANO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(row.Item("COMUNE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(row.Item("S_NETTA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(row.Item("RENDITA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(row.Item("TIPO_DISP"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.IfNull(row.Item("PRG_INTERVENTI"), ""))
                    'If isSelettiva.Value = "1" Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.IfNull(row.Item("FILIALE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.IfNull(row.Item("DEST_USO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.IfNull(row.Item("ZOSMI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.IfNull(row.Item("CAP"), ""))

                    'End If
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
            'Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")
            Response.Write("<script>window.open('../FileTemp/" & sNomeFile & ".zip','','');</script>")
            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
End Class
