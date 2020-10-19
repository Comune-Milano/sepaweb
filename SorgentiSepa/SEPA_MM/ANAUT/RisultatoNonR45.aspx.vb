
Partial Class ANAUT_RisultatoNonR
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sValoreFI As String
    Dim sValoreSDAL As String
    Dim sValoreSAL As String
    Dim sStringaSql As String

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

       
        If sValoreFI <> "58" Then
            If sValoreFI <> "" Then
                sValore = sValoreFI
                sCompara = " = "
                bTrovato = True
                sStringaSql = sStringaSql & " tab_filiali.id " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            Else
                bTrovato = True
                sStringaSql = sStringaSql & " COMPLESSI_IMMOBILIARI.id_filiale is null "
            End If
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


        If sValoreFI <> "58" Then
            sStringaSQL1 = "SELECT RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA,TO_CHAR(TO_DATE(DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                       & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA," _
                       & "(TAB_FILIALI.NOME ) AS FILIALE  FROM siscom_mi.filiali_virtuali,siscom_mi.unita_contrattuale,SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.TAB_FILIALI " _
                       & " WHERE filiali_virtuali.id_filiale=tab_filiali.ID AND filiali_virtuali.id_contratto=rapporti_utenza.ID AND (SUBSTR(COD_CONTRATTO,1,1)='4' or SUBSTR(COD_CONTRATTO,1,6)='000000') and " _
                       & "  " _
                       & " (anagrafica.ragione_sociale is null or anagrafica.ragione_sociale='') and " _
                       & " ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
                       & " AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND  " _
                       & " UNITA_IMMOBILIARI.ID=unita_contrattuale.id_unita AND unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL " _
                       & " AND (COD_TIPOLOGIA_CONTR_LOC='ERP' OR COD_TIPOLOGIA_CONTR_LOC='EQC392') AND (DATA_RICONSEGNA IS NULL)" _
                       & " AND COD_CONTRATTO IS NOT NULL AND  " _
                       & " COD_CONTRATTO NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE RAPPORTO IS NOT NULL AND ID_BANDO=" & sValoreBA & " AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) " _
                       & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & sValoreBA & ")" _
                       & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.CONVOCAZIONI_AU WHERE CARICO_AUSI=1 AND ID_GRUPPO IN " _
                       & "(SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & sValoreBA & ")) "

        Else
            sStringaSQL1 = "SELECT RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA,TO_CHAR(TO_DATE(DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                                   & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA," _
                                   & "(TAB_FILIALI.NOME ) AS FILIALE  FROM siscom_mi.filiali_virtuali,siscom_mi.unita_contrattuale,SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.TAB_FILIALI " _
                                   & " WHERE filiali_virtuali.id_filiale=tab_filiali.ID AND filiali_virtuali.id_contratto=rapporti_utenza.ID AND (SUBSTR(COD_CONTRATTO,1,1)='4' or SUBSTR(COD_CONTRATTO,1,6)='000000') and " _
                                   & "  " _
                                   & " (anagrafica.ragione_sociale is null or anagrafica.ragione_sociale='') and " _
                                   & " ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
                                   & " AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND  " _
                                   & " UNITA_IMMOBILIARI.ID=unita_contrattuale.id_unita AND unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL " _
                                   & " AND (COD_TIPOLOGIA_CONTR_LOC='ERP' OR COD_TIPOLOGIA_CONTR_LOC='EQC392') AND (DATA_RICONSEGNA IS NULL)" _
                                   & " AND COD_CONTRATTO IS NOT NULL AND  " _
                                   & " COD_CONTRATTO NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE RAPPORTO IS NOT NULL AND ID_BANDO=" & sValoreBA & " AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) " _
                                   & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & sValoreBA & ")" _
                                   & " AND RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.CONVOCAZIONI_AU WHERE CARICO_AUSI=1 AND ID_GRUPPO IN " _
                                   & "(SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & sValoreBA & ")) "
        End If
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
        Response.Write("<script>document.location.href=""RicercaNonR45.aspx""</script>")
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

            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("IDC")
            dt.Columns.Add("BANDO")

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    ROW = dt.NewRow()
                    ROW.Item("COD_CONTRATTO") = oDataGridItem.Cells(1).Text
                    If Request.QueryString("FI") <> "58" Then
                        ROW.Item("FILIALE") = oDataGridItem.Cells(2).Text
                    Else
                        ROW.Item("FILIALE") = "GRUPPO DI LAVORO ANAGRAFE COMUNE DI MILANO"
                    End If

                    ROW.Item("IDC") = oDataGridItem.Cells(3).Text
                    ROW.Item("BANDO") = sValoreBA
                    dt.Rows.Add(ROW)
                    I = I + 1
                End If
            Next

            If I > 0 Then
                HttpContext.Current.Session.Add("ElencoDT", dt)
                Response.Redirect("StampaNonR45.aspx?PG=" & npg.Value & "&FI=" & Request.QueryString("FI"))
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

End Class
