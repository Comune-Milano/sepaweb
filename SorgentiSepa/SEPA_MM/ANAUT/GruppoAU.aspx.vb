Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_GruppoAU
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If conferma.Value = "1" Then
            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        End If
    End Sub


    Public Property IdGruppo() As Long
        Get
            If Not (ViewState("par_IdGruppo") Is Nothing) Then
                Return CStr(ViewState("par_IdGruppo"))
            Else
                Return -1
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdGruppo") = value
        End Set

    End Property


    Public Property Registro() As Integer
        Get
            If Not (ViewState("par_Registro") Is Nothing) Then
                Return CStr(ViewState("par_Registro"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Registro") = value
        End Set

    End Property

    Public Property IdRegistro() As Long
        Get
            If Not (ViewState("par_IDRegistro") Is Nothing) Then
                Return CLng(ViewState("par_IDRegistro"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IDRegistro") = value
        End Set

    End Property

    Public Property ApplicataAU() As Integer
        Get
            If Not (ViewState("par_ApplicataAU") Is Nothing) Then
                Return CStr(ViewState("par_ApplicataAU"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_ApplicataAU") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        txtNomeGruppo.Attributes.Add("onchange", "javascript:document.getElementById('modificato').value='1';")

        If Not IsPostBack Then



            par.OracleConn.Open()
            par.SettaCommand(par)

            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")

            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IDAU.Value = myReader("ID")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT ID FROM UTENZA_GRUPPI_LAVORO WHERE FL_REGISTRO=1 AND ID_BANDO_AU=" & IDAU.Value
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Registro = myReader("ID")
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Dim dt1 As New Data.DataTable
            dt1.Columns.Add("IDAU")
            HttpContext.Current.Session.Add("ElencoRegistroDT", dt1)


            If Request.QueryString("ID") = "-1" Then
                IdGruppo = -1
            Else
                IdGruppo = Request.QueryString("ID")
                CaricaContratti()
            End If

            If Registro = IdGruppo Then
                imgRegistro.Visible = False
                txtNomeGruppo.Enabled = False
            End If


            If ApplicataAU = 1 Then
                imgRegistro.Visible = False
                imgSalva.Visible = False
                imgElimina.Visible = False
                ImageButton1.Visible = False
                txtNomeGruppo.Enabled = False
            End If
        End If
    End Sub

    Private Function CaricaContratti()
        Try
            Dim dt As New System.Data.DataTable
            Dim ROW As System.Data.DataRow

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM UTENZA_GRUPPI_LAVORO WHERE ID=" & IdGruppo
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                txtNomeGruppo.Text = par.IfNull(myReader("nome_gruppo"), "")
                If par.IfNull(myReader("fl_registro"), "") = "2" Then
                    txtNomeGruppo.Enabled = False
                    imgElimina.Visible = False
                    ImageButton1.Visible = False
                End If
                If par.IfNull(myReader("applicazione"), "0") = "1" Then
                    ApplicataAU = 1
                Else
                    ApplicataAU = 0
                End If
            End If
            myReader.Close()

            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("PG_AU")
            dt.Columns.Add("COGNOME")
            dt.Columns.Add("NOME")
            dt.Columns.Add("TIPOLOGIA")
            dt.Columns.Add("DECORRENZA")
            dt.Columns.Add("SCADENZA")
            dt.Columns.Add("INDIRIZZO_UNITA")
            dt.Columns.Add("CIVICO_UNITA")
            dt.Columns.Add("COMUNE_UNITA")
            dt.Columns.Add("CAP_UNITA")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("PREVALENTE")
            dt.Columns.Add("PRESENZA_15")
            dt.Columns.Add("PRESENZA_65")
            dt.Columns.Add("N_INV_100_CON")
            dt.Columns.Add("N_INV_100_SENZA")
            dt.Columns.Add("N_INV_66_99")
            dt.Columns.Add("IDC")
            dt.Columns.Add("IDAU")


            'par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID AS IDAU,UTENZA_DICHIARAZIONI.pg AS PG_AU,UTENZA_DICHIARAZIONI.N_COMP_NUCLEO,UTENZA_DICHIARAZIONI.N_INV_100_CON,UTENZA_DICHIARAZIONI.N_INV_100_SENZA,UTENZA_DICHIARAZIONI.N_INV_100_66 AS n_inv_66_99," _
            '                    & "DECODE(PREVALENTE_DIP,0,'NO',1,'SI') AS PREVALENTE,DECODE(PRESENZA_MIN_15,0,'NO',1,'SI') AS PRESENZA_15,DECODE(PRESENZA_MAG_65,0,'NO',1,'SI') AS PRESENZA_65, RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA," _
            '                    & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
            '                    & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA,(TAB_FILIALI.NOME ) AS FILIALE  " _
            '                    & "FROM siscom_mi.TAB_FILIALI,siscom_mi.COMPLESSI_IMMOBILIARI, siscom_mi.EDIFICI, UTENZA_DICHIARAZIONI,siscom_mi.indirizzi,siscom_mi.rapporti_utenza,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica  WHERE " _
            '                    & "UTENZA_DICHIARAZIONI.ID IN (SELECT ID_DICHIARAZIONE FROM UTENZA_GRUPPI_DICHIARAZIONI WHERE ID_GRUPPO=" & IdGruppo & ") " _
            '                    & "AND UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND COMPLESSI_IMMOBILIARI.id_filiale=TAB_FILIALI.ID (+) AND " _
            '                    & "COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio  AND  ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
            '                    & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO AND " _
            '                    & "UNITA_IMMOBILIARI.ID = unita_contrattuale.id_unita AND unita_contrattuale.id_contratto = rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL AND  " _
            '                    & "COD_CONTRATTO IS NOT NULL ORDER BY descrizione ASC,indirizzi.civico ASC,anagrafica.cognome ASC,anagrafica.nome ASC"

            par.cmd.CommandText = "SELECT   UTENZA_DICHIARAZIONI.ID AS idau, UTENZA_DICHIARAZIONI.pg AS pg_au, " _
& "         UTENZA_DICHIARAZIONI.n_comp_nucleo, " _
& "         UTENZA_DICHIARAZIONI.n_inv_100_con, " _
         & "UTENZA_DICHIARAZIONI.n_inv_100_senza, " _
         & "UTENZA_DICHIARAZIONI.n_inv_100_66 AS n_inv_66_99, " _
         & "DECODE (prevalente_dip, 0, 'NO', 1, 'SI') AS prevalente, " _
         & "DECODE (presenza_min_15, 0, 'NO', 1, 'SI') AS presenza_15, " _
         & "DECODE (presenza_mag_65, 0, 'NO', 1, 'SI') AS presenza_65, " _
         & "rapporti_utenza.ID AS idc, cod_contratto, anagrafica.cognome, " _
         & "anagrafica.nome, cod_tipologia_contr_loc AS tipologia, " _
         & "TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'YYYYmmdd'), " _
                  & "'DD/MM/YYYY' " _
                 & ") AS decorrenza, " _
         & "TO_CHAR (TO_DATE (data_riconsegna, 'YYYYmmdd'), " _
                  & "'DD/MM/YYYY' " _
                 & ") AS scadenza, " _
         & "indirizzi.descrizione AS indirizzo_unita, " _
         & "indirizzi.civico AS civico_unita, indirizzi.cap AS cap_unita, " _
         & "indirizzi.localita AS comune_unita, UTENZA_SPORTELLI.DESCRIZIONE AS filiale " _
    & "FROM UTENZA_SPORTELLI,UTENZA_SPORTELLI_PATRIMONIO, " _
         & "UTENZA_DICHIARAZIONI, " _
         & "siscom_mi.indirizzi, " _
         & "siscom_mi.rapporti_utenza, " _
         & "siscom_mi.unita_contrattuale, " _
         & "siscom_mi.unita_immobiliari, " _
         & "siscom_mi.soggetti_contrattuali, " _
         & "siscom_mi.anagrafica " _
   & "WHERE UTENZA_DICHIARAZIONI.ID IN (SELECT id_dichiarazione " _
                                       & "FROM UTENZA_GRUPPI_DICHIARAZIONI " _
                                      & "WHERE id_gruppo = " & IdGruppo & ") " _
     & "AND UTENZA_DICHIARAZIONI.rapporto = rapporti_utenza.cod_contratto " _
     & "AND UTENZA_SPORTELLI.ID=UTENZA_SPORTELLI_PATRIMONIO.ID_SPORTELLO AND UTENZA_SPORTELLI_PATRIMONIO.ID_UNITA=UNITA_IMMOBILIARI.ID AND UTENZA_SPORTELLI_PATRIMONIO.ID_AU=" & IDAU.Value _
     & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
     & "AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
     & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
     & "AND indirizzi.ID = unita_immobiliari.id_indirizzo " _
     & "AND unita_immobiliari.ID = unita_contrattuale.id_unita " _
     & "AND unita_contrattuale.id_contratto = rapporti_utenza.ID " _
     & "AND unita_contrattuale.id_unita_principale IS NULL " _
     & "AND cod_contratto IS NOT NULL " _
& "ORDER BY INDIRIZZI.descrizione ASC, " _
         & "indirizzi.civico ASC, " _
         & "anagrafica.cognome ASC, " _
         & "anagrafica.nome ASC "


            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                ROW = dt.NewRow()
                ROW.Item("COD_CONTRATTO") = par.IfNull(myReader("COD_CONTRATTO"), "")
                ROW.Item("PG_AU") = par.IfNull(myReader("PG_AU"), "")
                ROW.Item("COGNOME") = par.IfNull(myReader("COGNOME"), "")
                ROW.Item("NOME") = par.IfNull(myReader("NOME"), "")
                ROW.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                ROW.Item("DECORRENZA") = par.IfNull(myReader("DECORRENZA"), "")
                ROW.Item("SCADENZA") = par.IfNull(myReader("SCADENZA"), "")
                ROW.Item("INDIRIZZO_UNITA") = par.IfNull(myReader("INDIRIZZO_UNITA"), "")
                ROW.Item("CIVICO_UNITA") = par.IfNull(myReader("CIVICO_UNITA"), "")
                ROW.Item("COMUNE_UNITA") = par.IfNull(myReader("COMUNE_UNITA"), "")
                ROW.Item("CAP_UNITA") = par.IfNull(myReader("CAP_UNITA"), "")
                ROW.Item("FILIALE") = par.IfNull(myReader("FILIALE"), "")
                ROW.Item("PREVALENTE") = par.IfNull(myReader("PREVALENTE"), "")
                ROW.Item("PRESENZA_15") = par.IfNull(myReader("PRESENZA_15"), "")
                ROW.Item("PRESENZA_65") = par.IfNull(myReader("PRESENZA_65"), "")
                ROW.Item("N_INV_100_CON") = par.IfNull(myReader("N_INV_100_CON"), "")
                ROW.Item("N_INV_100_SENZA") = par.IfNull(myReader("N_INV_100_SENZA"), "")
                ROW.Item("N_INV_66_99") = par.IfNull(myReader("N_INV_66_99"), "")
                ROW.Item("IDC") = par.IfNull(myReader("IDC"), "")
                ROW.Item("IDAU") = par.IfNull(myReader("IDAU"), "")
                dt.Rows.Add(ROW)
            Loop
            myReader.Close()

            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            Label1.Text = "Elenco Nominativi (" & DataGrid1.Items.Count & " nella lista)"
            HttpContext.Current.Session.Add("ElencoOriginaleDT", dt)

            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            'Dim ds As New Data.DataSet()

            'da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")

            'DataGrid1.DataSource = ds
            'DataGrid1.DataBind()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('IDC').value='" & e.Item.ItemIndex & "';document.getElementById('IDAU').value='" & e.Item.Cells(18).Text & "';")

        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

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

    'Private Sub BindGrid()

    '    par.OracleConn.Open()
    '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
    '    Dim ds As New Data.DataSet()
    '    da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")
    '    DataGrid1.DataSource = ds
    '    DataGrid1.DataBind()
    '    'Label1.Text = Label1.Text & " (" & ds.Tables(0).Rows.Count & ")"
    '    par.OracleConn.Close()
    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    'End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Dim dt1 As New System.Data.DataTable
            Dim dt As New System.Data.DataTable

            dt = CType(HttpContext.Current.Session.Item("ElencoDT"), Data.DataTable)

            If Not IsNothing(CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)) Then
                dt1 = CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)
            End If

            If Not IsNothing(dt) Then
                dt1.Merge(dt)
                modificato.Value = "1"
            End If

            DataGrid1.DataSource = dt1
            DataGrid1.DataBind()
            Label1.Text = "Elenco Nominativi (" & DataGrid1.Items.Count & " nella lista)"
            HttpContext.Current.Session.Add("ElencoOriginaleDT", dt1)
            HttpContext.Current.Session.Remove("ElencoDT")

        Catch ex As Exception
            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub imgElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgElimina.Click
        Try


            If conferma.Value = "1" Then

                Dim dt As New System.Data.DataTable
                dt = CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)
                dt.Rows(IDC.Value).Delete()

                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                Label1.Text = "Elenco Nominativi (" & DataGrid1.Items.Count & " nella lista)"
                HttpContext.Current.Session.Add("ElencoOriginaleDT", dt)
                modificato.Value = "1"
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub imgExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
        If conferma.Value = "1" Then
            ExportXLS()
        End If
    End Sub

    Private Function ExportXLS()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try
            lblErrore.Visible = False
            Dim dt As New System.Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)
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

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. CONTRATTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "PG A.U.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COGNOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "NOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "TIPOLOGIA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DECORRENZA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "SCADENZA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CIVICO UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "CAP UNITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "FILIALE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "PREVALENTE DIP.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "PRESENZA <15 ANNI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "PRESENZA >65 ANNI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "NUM. INVALIDI 100% CON IND.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "NUM. INVALIDI 100% SENZA IND.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "NUM. INVALIDI 66%-99%")


                        K = 2
                        For Each row In dt.Rows
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), ""))

                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("PG_AU"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("COGNOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("NOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("DECORRENZA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("SCADENZA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("INDIRIZZO_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("CIVICO_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("COMUNE_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("CAP_UNITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("FILIALE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(dt.Rows(i).Item("PREVALENTE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(dt.Rows(i).Item("PRESENZA_15"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(dt.Rows(i).Item("PRESENZA_65"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(dt.Rows(i).Item("N_INV_100_CON"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.IfNull(dt.Rows(i).Item("N_INV_100_SENZA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.IfNull(dt.Rows(i).Item("N_INV_66_99"), ""))

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


                Dim scriptblock As String = "<script language='javascript' type='text/javascript'>" _
                                        & "window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');" _
                                        & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                End If


                'Response.Redirect("..\FileTemp\" & FileCSV & ".zip")
            Else
                lblErrore.Visible = True
                lblErrore.Text = "Niente da esportare! Inserire il nome del gruppo e i rapporti"
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try



    End Function

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Try
            Dim row As System.Data.DataRow
            Dim dt As New System.Data.DataTable
            Dim i As Long = 0

            dt = CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            lblErrore.Visible = False

            If Not IsNothing(dt) Then

                If txtNomeGruppo.Text = "" Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Inserire il nome del gruppo. Salvataggi onon effettuato!"
                End If

                par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
                Dim myReaderAU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderAU.Read() Then
                    IDAU.Value = myReaderAU("ID")
                End If
                myReaderAU.Close()

                If IdGruppo = -1 Then
                    par.cmd.CommandText = "INSERT INTO UTENZA_GRUPPI_LAVORO (ID,NOME_GRUPPO,DATA_CREAZIONE,ID_OPERATORE,ID_BANDO_AU) VALUES (SEQ_UTENZA_GRUPPI.NEXTVAL,'" & par.PulisciStrSql(UCase(txtNomeGruppo.Text)) & "','" & Format(Now, "yyyyMMdd") & "'," & Session.Item("ID_OPERATORE") & "," & IDAU.Value & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_UTENZA_GRUPPI.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        IdGruppo = myReader(0)
                    End If
                    myReader.Close()
                Else
                    par.cmd.CommandText = "UPDATE UTENZA_GRUPPI_LAVORO SET NOME_GRUPPO='" & par.PulisciStrSql(UCase(txtNomeGruppo.Text)) & "' WHERE ID=" & IdGruppo
                    par.cmd.ExecuteNonQuery()
                End If

                par.cmd.CommandText = "delete from utenza_gruppi_dichiarazioni where id_gruppo=" & IdGruppo
                par.cmd.ExecuteNonQuery()


                For Each row In dt.Rows
                    par.cmd.CommandText = "INSERT INTO UTENZA_GRUPPI_DICHIARAZIONI (ID,ID_GRUPPO,ID_DICHIARAZIONE,APPLICA_AU) VALUES (SEQ_UTENZA_GR_DIC.NEXTVAL," & IdGruppo & "," & par.IfNull(dt.Rows(i).Item("IDAU"), "") & ",0)"
                    par.cmd.ExecuteNonQuery()
                    i = i + 1
                Next

                i = 0
                dt = CType(HttpContext.Current.Session.Item("ElencoRegistroDT"), Data.DataTable)

                For Each row In dt.Rows
                    par.cmd.CommandText = "INSERT INTO UTENZA_GRUPPI_DICHIARAZIONI (ID,ID_GRUPPO,ID_DICHIARAZIONE,APPLICA_AU) VALUES (SEQ_UTENZA_GR_DIC.NEXTVAL," & Registro & "," & par.IfNull(dt.Rows(i).Item("IDAU"), "") & ",0)"
                    par.cmd.ExecuteNonQuery()
                    i = i + 1
                Next
                modificato.Value = "0"
                Response.Write("<script>alert('Operazione Effettuata!');</script>")
            Else
                lblErrore.Visible = True
                lblErrore.Text = "Niente da salvare! Inserire il nome del gruppo e i rapporti"
            End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub imgRegistro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRegistro.Click
        If conferma.Value = "1" Then

            Try

                Dim Sposta As Boolean = True

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
                Dim myReaderAU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderAU.Read() Then
                    IDAU.Value = myReaderAU("ID")
                End If
                myReaderAU.Close()

                par.cmd.CommandText = "select * from utenza_gruppi_dichiarazioni where id_gruppo=" & IdGruppo & " and id_dichiarazione=" & IDAU.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If myReader("applica_au") = "1" Then
                        Sposta = False
                    End If
                End If
                myReader.Close()

                If Sposta = True Then
                    par.cmd.CommandText = "select * from utenza_gruppi_dichiarazioni where id_gruppo<>" & IdGruppo & " and applica_au=0 and id_dichiarazione=" & IDAU.Value
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.HasRows = True Then
                        Sposta = False
                    End If
                    myReader1.Close()
                End If



                If Sposta = True Then
                    Dim dt1 As New Data.DataTable
                    Dim row As System.Data.DataRow

                    dt1 = CType(HttpContext.Current.Session.Item("ElencoRegistroDT"), Data.DataTable)

                    row = dt1.NewRow()
                    row.Item("IDAU") = IDAU.Value
                    dt1.Rows.Add(row)


                    Dim dt As New System.Data.DataTable
                    dt = CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)
                    dt.Rows(IDC.Value).Delete()

                    DataGrid1.DataSource = dt
                    DataGrid1.DataBind()
                    'Label1.Text = "Elenco Nominativi (" & DataGrid1.Items.Count & " nella lista)"
                    HttpContext.Current.Session.Add("ElencoOriginaleDT", dt)
                    HttpContext.Current.Session.Add("ElencoRegistroDT", dt1)
                    modificato.Value = "1"
                Else
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione...Alla dichiarazione AU è stato già applicato il canone oppure è nel gruppo POST ELABORAZIONE"
                End If

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                HttpContext.Current.Session.Remove("ElencoOriginaleDT")
                HttpContext.Current.Session.Remove("ElencoDT")
                HttpContext.Current.Session.Remove("ElencoRegistroDT")
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try


        End If
    End Sub
End Class
