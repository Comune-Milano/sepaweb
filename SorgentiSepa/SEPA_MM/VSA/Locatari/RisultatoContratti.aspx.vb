Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_RisultatoContratti
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

    Dim sValoreSDAL As String
    Dim sValoreSAL As String
    Dim sValoreDDAL As String
    Dim sValoreDAL As String

    Dim sValoreFDAL As String
    Dim sValoreFAL As String
    Dim sValorePIVA As String
    Dim sValoreGIMI As String

    Dim sValoreRECDA As String
    Dim sValoreRECA As String
    Dim sValoreSLODA As String
    Dim sValoreSLOA As String
    Dim sValoreINSDA As String
    Dim sValoreINSA As String

    Dim sValoreSLOV As String

    Dim sStringaSql As String
    Dim scriptblock As String




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()

            ModRichiesta.Value = Request.QueryString("ModR")

            sValoreCG = UCase(Request.QueryString("CG"))
            sValoreNM = UCase(Request.QueryString("NM"))
            sValoreCF = UCase(Request.QueryString("CF"))
            sValoreRS = UCase(Request.QueryString("RS"))
            sValoreCO = UCase(Request.QueryString("CO"))
            sValoreTI = UCase(Request.QueryString("TI"))
            sValoreTC = UCase(Request.QueryString("TC"))
            sValoreUN = UCase(Request.QueryString("UN"))
            sValoreST = UCase(Request.QueryString("ST"))

            sValoreSDAL = Request.QueryString("SDAL")
            sValoreSAL = Request.QueryString("SAL")
            sValoreDDAL = Request.QueryString("DDAL")
            sValoreDAL = Request.QueryString("DAL")

            sValoreFDAL = Request.QueryString("FDAL")
            sValoreFAL = Request.QueryString("FAL")
            sValorePIVA = Request.QueryString("PIVA")
            sValoreGIMI = Request.QueryString("GIMI")

            sValoreRECDA = Request.QueryString("RECDA")
            sValoreRECA = Request.QueryString("RECA")
            sValoreSLODA = Request.QueryString("SLODA")
            sValoreSLOA = Request.QueryString("SLOA")

            sValoreINSDA = Request.QueryString("INSDA")
            sValoreINSA = Request.QueryString("INSA")

            sValoreSLOV = Request.QueryString("SLOV")

            'btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
            If Session.Item("LIVELLO") = "1" Then
                'btnExport.Visible = True
            End If
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

        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message

        End Try
    End Sub


    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePIVA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePIVA
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.PARTITA_IVA " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreUN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreUN
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreST <> "TUTTI" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreRS <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRS
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.RAGIONE_SOCIALE" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
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

        If sValoreDDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DECORRENZA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DECORRENZA<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreFDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreFAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA<='" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCO <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreCO
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreTC <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreTC
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC ='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreTI <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreTI
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_CONTRATTUALE.TIPOLOGIA ='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreGIMI <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreGIMI

            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO_GIMI ='" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreRECDA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRECDA
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreRECA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRECA
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO<='" & par.PulisciStrSql(sValore) & "' "
        End If


        'INSERIMENTO

        If sValoreINSDA <> "" Or sValoreINSA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sStringaSql = "  RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.EVENTI_CONTRATTI WHERE EVENTI_CONTRATTI.COD_EVENTO='F01' "
            bTrovato = True

            If sValoreINSDA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreINSDA
                bTrovato = True
                sStringaSql = sStringaSql & " SUBSTR(EVENTI_CONTRATTI.DATA_ORA,1,8)>='" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreINSA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreINSA
                bTrovato = True
                sStringaSql = sStringaSql & " SUBSTR(EVENTI_CONTRATTI.DATA_ORA,1,8)<='" & par.PulisciStrSql(sValore) & "' "
            End If
            sStringaSql = sStringaSql & ")"
        End If


        If sValoreSLOV <> "1" Then
            If sValoreSLODA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreSLODA
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA>='" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreSLOA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreSLOA
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA<='" & par.PulisciStrSql(sValore) & "' "
            End If
        Else
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            bTrovato = True
            sStringaSql = sStringaSql & " (RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL OR RAPPORTI_UTENZA.DATA_RICONSEGNA='') "

        End If

        'window.open('ElencoAllegati.aspx?COD=<%=CodContratto1 %>', 'Allegati', '');

        sStringaSQL1 = "SELECT TAB_FILIALI.NOME AS FILIALE_ALER,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,UNITA_IMMOBILIARI.COD_TIPOLOGIA,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA," _
                     & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE " _
                     & ",replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''ElencoAllegati.aspx?COD='||rapporti_utenza.cod_contratto||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') as ALLEGATI_CONTRATTO " _
                     & ",RAPPORTI_UTENZA.*,siscom_mi.getstatocontratto(rapporti_utenza.id) as STATO_DEL_CONTRATTO, " _
                     & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"" ," _
                     & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""COD FISCALE/PIVA"" ,TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_NASCITA,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA," _
                     & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) AS ""POSIZIONE_CONTRATTO"" FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA," _
                     & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
                     & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                     & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) <> 'CHIUSO' AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) <> 'BOZZA' AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        If Session.Item("INDIRIZZI") <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND (" & Session.Item("INDIRIZZI") & ") "
            'Session.Item("INDIRIZZI") = ""
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY ""INTESTATARIO"" ASC"


        sStringaSQL2 = "SELECT COUNT(distinct rapporti_utenza.cod_contratto) FROM SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA," _
             & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
             & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
             & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "

        If sStringaSql <> "" Then
            sStringaSQL2 = sStringaSQL2 & " AND " & sStringaSql
        End If

        If Session.Item("INDIRIZZI") <> "" Then
            sStringaSQL2 = sStringaSQL2 & " AND (" & Session.Item("INDIRIZZI") & ") "
            Session.Item("INDIRIZZI") = ""
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

    Protected Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '--------------------------------------------------- 

            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "';document.getElementById('LBLintest').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('LBLcodF').value='" & e.Item.Cells(3).Text & "';")


            'e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            'e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=''")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "';document.getElementById('LBLintest').value='" & e.Item.Cells(2).Text & "'")

        End If
    End Sub

    Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        Response.Redirect("RicercaContratti.aspx")
    End Sub
End Class
