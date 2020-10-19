'*** LISTA RISULTATO DEBITORI MOROSITA' da Affidare al LEGALE
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class MOROSITA_RisultatiMorositaLegale
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Public sValoreStrutturaAler As String
    Public sValoreAreaCanone As String

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreIndirizzo As String
    Public sValoreCivico As String

    Public sValoreCognome As String
    Public sValoreNome As String

    Public sValoreCodice As String
    Public sValoreTI As String

    Public sValoreImporto1 As String
    Public sValoreImporto2 As String

    Public sValoreTribunale As String
    Public sValorePraticheDA As String
    Public sValorePraticheA As String

    Public sOrdinamento As String

    Public ID_Rapporti As New System.Collections.ArrayList()

    Dim lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
        End If


        Dim Str As String

        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))


        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()

            lstListaRapporti.Clear()


            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)
            sValoreAreaCanone = Request.QueryString("AREAC")    'AREA CANONE (CANONI_EC.ID_AREA_ECONOMICA)


            sValoreComplesso = Request.QueryString("CO")    'COMPLESSO
            sValoreEdificio = Request.QueryString("ED")     'EDIFICIO
            sValoreIndirizzo = Request.QueryString("IN")    'INDIRIZZO
            sValoreCivico = Request.QueryString("CI")       'CIVICO


            sValoreCognome = Request.QueryString("CG")      'COGNOME
            sValoreNome = Request.QueryString("NM")         'NOME

            sValoreCodice = Request.QueryString("CD")       'CODICE RAPPORTO
            sValoreTI = UCase(Request.QueryString("TI"))    'TIPOLOGIA U.I.

            sValoreImporto1 = Request.QueryString("IMP1")   'IMPORTO DA
            sValoreImporto2 = Request.QueryString("IMP2")   'IMPORTO A

            sValoreTribunale = Request.QueryString("TR")

            sValorePraticheDA = Request.QueryString("PRA_DA")   'NUM. PRATICHE DA
            sValorePraticheA = Request.QueryString("PRA_A")    'NUM. PRATICHE A

            sOrdinamento = Request.QueryString("ORD")       'ORDINAMENTO


            Cerca()
            BindGrid()

            CaricaLegale()

            If Session.Item("LIVELLO") = "1" Then
                btnProcedi.Visible = True
            End If

        End If

    End Sub


    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property


    Private Sub Cerca()
        Dim sValore As String
        Dim sCompara As String

        Dim sStringaSql As String = ""
        Dim sStringa_FROM_WHERE As String = ""


        Dim sStringa_SELECT_1 As String = ""
        Dim sStringa_SELECT_2 As String = ""

        Dim gen As Epifani.ListaGenerale
        Dim FlagConnessione As Boolean



        'DOVE LA DATA SCADENZA O DILAZIONE è SCADUTA DA 60 giorni (NOTA: PRIMA DELLA CONSEGNA sostituire sCondizione da < a >)
        Dim GiorniScandeza As Integer = 60
        Dim sCondizione As String = ">"

        Try
            'PAGAMENTO SCADUTO

            sStringaSql = "  and ((  ( TO_DATE('" & par.AggiustaData(Now.Date) & "','YYYYMMDD') - TO_DATE(NVL(MOROSITA_LETTERE.DATA_DILAZIONE,0),'YYYYMMDD') " _
                             & "  )" & sCondizione & GiorniScandeza _
                             & " and NVL(MOROSITA_LETTERE.DATA_DILAZIONE,0)>0 " _
                             & "  ) " _
                             & " or (   ( TO_DATE('" & par.AggiustaData(Now.Date) & "','YYYYMMDD') - TO_DATE(NVL(MOROSITA_LETTERE.DATA_SCADENZA,0),'YYYYMMDD') " _
                             & "    )" & sCondizione & GiorniScandeza _
                             & " and NVL(MOROSITA_LETTERE.DATA_SCADENZA,0)>0 " _
                             & " ))"

            'Da escludere:
            ' - tutte quelle in SOSPENSIONE 15,16,17,18
            ' - quelle dove è già stata assegnata ad un legale 20 
            ' - quelle annullate M98 e quelle concluse M100
            ' - Rateizzazione in corso M96

            sStringaSql = sStringaSql & " and  MOROSITA_LETTERE.COD_STATO not in ('M15','M16','M17','M18','M20','M98','M100','M96') "


            'COMPLESSO-EDIFICIO-INDIRIZZO-CIVICO
            If par.IfEmpty(sValoreIndirizzo, "-1") <> "-1" Then

                sStringaSql = sStringaSql & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                            & " where DESCRIZIONE='" & par.PulisciStrSql(sValoreIndirizzo) & "'"

                If par.IfEmpty(sValoreCivico, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and CIVICO='" & par.PulisciStrSql(sValoreCivico) & "')"
                Else
                    sStringaSql = sStringaSql & " )"
                End If

            ElseIf par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then

                sStringaSql = sStringaSql & " and  SISCOM_MI.EDIFICI.ID =" & sValoreEdificio

            ElseIf par.IfEmpty(sValoreComplesso, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID =" & sValoreComplesso
            ElseIf par.IfEmpty(sValoreStrutturaAler, "-1") <> "-1" Then
                'FILIALE (STRUTTURA)
                sStringaSql = sStringaSql & " and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE in (" & sValoreStrutturaAler & ")"
            End If
            '*********************************************************

            'COGNOME e NOME
            If sValoreCognome <> "" Then
                sValore = Strings.UCase(sValoreCognome)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " and ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreNome <> "" Then
                sValore = Strings.UCase(sValoreNome)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " and ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            '********************************************************


            'CODICE RAPPORTO
            If sValoreCodice <> "" Then
                sValore = Strings.UCase(sValoreCodice)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            '***********************************************************

            'TIPOLOGIA U.I.
            If sValoreTI <> "" Then
                sStringaSql = sStringaSql & " and UNITA_CONTRATTUALE.TIPOLOGIA ='" & par.PulisciStrSql(sValoreTI) & "' "
            End If
            '*********************************************************


            'AREA CANONE
            If par.IfEmpty(sValoreAreaCanone, "-1") <> "-1" Then
                'AREA CANONE
                sStringaSql = sStringaSql & " and  RAPPORTI_UTENZA.ID in (select distinct(ID_CONTRATTO) " _
                                                                      & " from SISCOM_MI.CANONI_EC " _
                                                                      & " where ID_AREA_ECONOMICA in (" & sValoreAreaCanone & ")) "
            End If


            If sValoreImporto1 <> "" Then
                If sValoreImporto2 <> "" Then
                    'IMPORTO 1 + IMPORTO2
                    sStringaSql = " and ( NVL(SISCOM_MI.MOROSITA_LETTERE.IMPORTO,0)>=" & sValoreImporto1 _
                                  & " and NVL(SISCOM_MI.MOROSITA_LETTERE.IMPORTO,0)<=" & sValoreImporto2 & " ) "

                Else
                    'IMPORTO1
                    sStringaSql = " and NVL(SISCOM_MI.MOROSITA_LETTERE.IMPORTO,0)>=" & sValoreImporto1
                End If
            ElseIf sValoreImporto2 <> "" Then
                'IMPORTO2
                sStringaSql = " and NVL(SISCOM_MI.MOROSITA_LETTERE.IMPORTO,0)<=" & sValoreImporto2
            End If


            '********************************************

            sStringa_SELECT_1 = "select  distinct MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE," _
                                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'',''top=0,left=0'');£>'||" _
                                        & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                                                    & "     then  RAGIONE_SOCIALE " _
                                                                    & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                                    & " SISCOM_MI.MOROSITA_LETTERE.IMPORTO as DEBITO2 ," _
                                    & " (case when MOROSITA_LETTERE.NUM_LETTERE=1 then 'MAV MG' " _
                                        & " else 'MAV MA' end ) as TIPO_MAV, " _
                                    & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC, " _
                                    & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                                    & " UNITA_IMMOBILIARI.COD_TIPOLOGIA, " _
                                    & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                                    & " INDIRIZZI.CIVICO,(select NOME from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                                    & " SISCOM_MI.MOROSITA_LETTERE.IMPORTO as DEBITO,  " _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & "     then  trim(RAGIONE_SOCIALE) " _
                                    & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 "

            sStringa_SELECT_2 = "select  MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE," _
                               & " RAPPORTI_UTENZA.COD_CONTRATTO as  COD_CONTRATTO ," _
                                   & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                                               & "     then  RAGIONE_SOCIALE " _
                                                               & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                               & "  as  INTESTATARIO ," _
                               & " SISCOM_MI.MOROSITA_LETTERE.IMPORTO as DEBITO2 ," _
                               & " (case when MOROSITA_LETTERE.NUM_LETTERE=1 then 'MAV MG' " _
                                   & " else 'MAV MA' end ) as TIPO_MAV, " _
                               & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC, " _
                               & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                               & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE as  COD_UNITA_IMMOBILIARE ," _
                               & " UNITA_IMMOBILIARI.COD_TIPOLOGIA, " _
                               & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                               & " INDIRIZZI.CIVICO,(select NOME from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                               & " SISCOM_MI.MOROSITA_LETTERE.IMPORTO as DEBITO,  " _
                               & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                               & "     then  trim(RAGIONE_SOCIALE) " _
                               & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 "

            sStringa_FROM_WHERE = " from  " _
                                    & " SISCOM_MI.MOROSITA_LETTERE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA," _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE," _
                                    & " SISCOM_MI.INDIRIZZI," _
                                    & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                    & " SISCOM_MI.EDIFICI," _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                        & " where  " _
                        & "       MOROSITA_LETTERE.ID_CONTRATTO             =RAPPORTI_UTENZA.ID" _
                        & "  and  MOROSITA_LETTERE.ID_ANAGRAFICA            =ANAGRAFICA.ID " _
                        & "  and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                        & "  and  UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                        & "  and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID " _
                        & "  and  UNITA_CONTRATTUALE.ID_CONTRATTO           =RAPPORTI_UTENZA.ID " _
                        & "  and  EDIFICI.ID_COMPLESSO                      =COMPLESSI_IMMOBILIARI.ID " _
                        & "  and  UNITA_IMMOBILIARI.ID_EDIFICIO             =EDIFICI.ID" _
                        & "  and  MOROSITA_LETTERE.ID_ANAGRAFICA            =SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                        & "  and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
                        & "  and  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null  "



            sStringaSQL1 = sStringa_SELECT_1 & sStringa_FROM_WHERE & sStringaSql & " order by INTESTATARIO2"

            'Select CONTA RAPPORTI
            sStringaSQL2 = "select distinct RAPPORTI_UTENZA.COD_CONTRATTO " & sStringa_FROM_WHERE & sStringaSql


            ''Select RAPPORTI.ID (per checkBox)
            sStringaSQL3 = "select MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE " & sStringa_FROM_WHERE & sStringaSql


            ''Select EXCEL
            Session.Add("MIADT", sStringa_SELECT_2 & sStringa_FROM_WHERE & sStringaSql & " order by INTESTATARIO2")


            ' RIEMPI la LISTA CON TUTTI i RAPPORTI.ID (tutte le righe con check)
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            par.cmd.CommandText = sStringaSQL1
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read()

                gen = New Epifani.ListaGenerale(lstListaRapporti.Count, par.IfNull(myReader(0), -1))
                lstListaRapporti.Add(gen)
                gen = Nothing
            Loop
            myReader.Close()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            '****************************************


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub BindGrid()

        Dim gen As Epifani.ListaGenerale
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim FlagConnessione As Boolean


        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.RAPPORTI_UTENZA")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()


            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            'TOTALE RAPPORTI
            par.cmd.CommandText = sStringaSQL2
            Dim i As Integer = 0
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read()
                i = i + 1
            Loop
            'Label4.Text = "(" & DataGrid1.Items.Count & " nella pagina - Totale :" & ds.Tables(0).Rows.Count & ") in " & i & " Rapporti"
            myReader.Close()
            '****************************



            For Each oDataGridItem In Me.DataGrid1.Items

                chkExport = oDataGridItem.FindControl("CheckBox1")

                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(0).Text Then
                        chkExport.Checked = True
                        Exit For
                    End If
                Next
            Next
            gen = Nothing


            da.Dispose()
            ds.Dispose()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click


        'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        'par.OracleConn.Close()

        'HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
        'HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'Page.Dispose()

        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub




    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim i As Integer
        Dim Trovato As Boolean

        Dim gen As Epifani.ListaGenerale

        If e.NewPageIndex >= 0 Then

            ' X la pagina Max 200 record 
            For Each oDataGridItem In Me.DataGrid1.Items

                chkExport = oDataGridItem.FindControl("CheckBox1")
                ' AddHandler chkExport.CheckedChanged, AddressOf cazzo

                If chkExport.Checked Then

                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next

                    If Trovato = False Then
                        gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(0).Text)
                        lstListaRapporti.Add(gen)
                        Me.Label3.Value = Val(Label3.Value) + 1
                        gen = Nothing
                    End If
                Else

                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next

                    If Trovato = True Then
                        i = 0
                        For Each gen In lstListaRapporti
                            If gen.STR = oDataGridItem.Cells(0).Text Then

                                lstListaRapporti.RemoveAt(i)
                                'Me.Label3.Value = Val(Label3.Value) - 1
                                Exit For
                            End If
                            i = i + 1
                        Next
                        gen = Nothing


                        Dim indice As Integer = 0
                        For Each gen In lstListaRapporti
                            gen.ID = indice
                            indice += 1
                        Next
                    End If
                End If
            Next

            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub




    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaMorositaLegale.aspx""</script>")
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

    Public Property sStringaSQL3() As String
        Get
            If Not (ViewState("par_sStringaSQL3") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL3"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL3") = value
        End Set

    End Property


    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim gen As Epifani.ListaGenerale


        ' X la pagina Max 200 record 
        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")

            chkExport.Checked = False

            For Each gen In lstListaRapporti
                If gen.STR = oDataGridItem.Cells(0).Text Then
                    chkExport.Checked = True
                    Exit For
                End If
            Next

        Next
        gen = Nothing


    End Sub



    Protected Sub btnSelTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelTutti.Click
        Dim gen As Epifani.ListaGenerale
        Dim FlagConnessione As Boolean


        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            lstListaRapporti.Clear()

            ' RIEMPI la LISTA CON TUTTI i RAPPORTI.ID (tutte le righe con check)
            par.cmd.CommandText = sStringaSQL3
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read()

                gen = New Epifani.ListaGenerale(lstListaRapporti.Count, par.IfNull(myReader(0), -1))
                lstListaRapporti.Add(gen)
                gen = Nothing
            Loop
            myReader.Close()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Protected Sub btnDeselTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselTutti.Click

        lstListaRapporti.Clear()

    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim Trovato As Boolean
        Dim i As Integer

        Dim gen As Epifani.ListaGenerale


        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")

            If chkExport.Checked Then

                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(0).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = False Then
                    gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(0).Text)
                    lstListaRapporti.Add(gen)
                    'Me.Label3.Value = Val(Label3.Value) + 1
                    gen = Nothing
                End If
            Else

                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(0).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = True Then
                    i = 0
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(0).Text Then

                            lstListaRapporti.RemoveAt(i)
                            'Me.Label3.Value = Val(Label3.Value) - 1
                            Exit For
                        End If
                        i = i + 1
                    Next
                    gen = Nothing

                    Dim indice As Integer = 0
                    For Each gen In lstListaRapporti
                        gen.ID = indice
                        indice += 1
                    Next

                End If
            End If
        Next

        If Me.cmbLegale.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare il legale!');</script>")
            Exit Sub
        End If

        If lstListaRapporti.Count > 0 Then


            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)
            sValoreAreaCanone = Request.QueryString("AREAC")    'AREA CANONE (CANONI_EC.ID_AREA_ECONOMICA)


            sValoreComplesso = Request.QueryString("CO")    'COMPLESSO
            sValoreEdificio = Request.QueryString("ED")     'EDIFICIO
            sValoreIndirizzo = Request.QueryString("IN")    'INDIRIZZO
            sValoreCivico = Request.QueryString("CI")       'CIVICO


            sValoreCognome = Request.QueryString("CG")      'COGNOME
            sValoreNome = Request.QueryString("NM")         'NOME

            sValoreCodice = Request.QueryString("CD")       'CODICE RAPPORTO
            sValoreTI = UCase(Request.QueryString("TI"))    'TIPOLOGIA U.I.

            sValoreImporto1 = Request.QueryString("IMP1")   'IMPORTO DA
            sValoreImporto2 = Request.QueryString("IMP2")   'IMPORTO A

            sValoreTribunale = Request.QueryString("TR")
            sValorePraticheDA = Request.QueryString("PRA_DA")   'NUM. PRATICHE DA
            sValorePraticheA = Request.QueryString("PRA_A")    'NUM. PRATICHE A

            sOrdinamento = Request.QueryString("ORD")       'ORDINAMENTO


            Response.Write("<script>location.replace('MorositaLegale.aspx?FI=" & sValoreStrutturaAler _
                                                                    & "&AREAC=" & sValoreAreaCanone _
                                                                    & "&CO=" & sValoreComplesso _
                                                                    & "&ED=" & sValoreEdificio _
                                                                    & "&IN=" & sValoreIndirizzo _
                                                                    & "&CI=" & par.VaroleDaPassare(sValoreCivico) _
                                                                    & "&CG=" & par.VaroleDaPassare(sValoreCognome) _
                                                                    & "&NM=" & par.VaroleDaPassare(sValoreNome) _
                                                                    & "&CD=" & par.VaroleDaPassare(sValoreCodice) _
                                                                    & "&TI=" & sValoreTI _
                                                                    & "&IMP1=" & sValoreImporto1 _
                                                                    & "&IMP2=" & sValoreImporto2 _
                                                                    & "&PRA_DA=" & sValorePraticheDA _
                                                                    & "&PRA_A=" & sValorePraticheA _
                                                                    & "&TR=" & sValoreTribunale _
                                                                    & "&LE=" & par.VaroleDaPassare(Me.cmbLegale.SelectedValue) _
                                                                    & "&ORD=" & sOrdinamento _
                                                & "');</script>")

        Else
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If


    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim FlagConnessione As Boolean

        Try

            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            'Dim row As System.Data.DataRow

            Dim dt As New Data.DataTable
            Dim ds As New Data.DataSet()

            'Dim oDataGridItem As DataGridItem


            sStringaSQL1 = CType(HttpContext.Current.Session.Item("MIADT"), String)
            'ds = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataSet)
            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")


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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DEBITO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COD. UNITA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO UNITA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE", 12)

                K = 2


                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    FlagConnessione = True
                End If


                par.cmd.CommandText = sStringaSQL1

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read()

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(myReader("COD_CONTRATTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(myReader("INTESTATARIO2"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(myReader("DEBITO2"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(myReader("POSIZIONE_CONTRATTO"), "")))

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(myReader("COD_TIPOLOGIA"), "")))

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(myReader("INDIRIZZO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(myReader("CIVICO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(myReader("COMUNE_UNITA"), "")))

                    i = i + 1
                    K = K + 1
                Loop
                myReader.Close()


                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                'For i = 0 To Int(ds.Tables(0).Rows)

                '    'For Each oDataGridItem In Me.DataGrid1.Items
                '    'For Each row In dt.Rows
                '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(ds.Tables(0).Rows(i).Item(0).ToString, "")))
                '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(ds.Tables(0).Rows(i).Item(1).ToString, "")))
                '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(ds.Tables(0).Rows(i).Item(2).ToString, "")))
                '    ''''   .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ID_COMPLESSO"), 0)))
                '    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA INIZIO"), 0)))
                '    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA FINE"), 0)))
                '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(ds.Tables(0).Rows(i).Item(3).ToString, "")))
                '    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ID_SERVIZIO"), 0)))
                '    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ID_APPALTO"), 0)))

                '    i = i + 1
                '    K = K + 1
                'Next i

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

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO COMBO ESERCIZIO FINANZIARIO
    Private Sub CaricaLegale()
        Dim FlagConnessione As Boolean
        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.cmbLegale.Items.Clear()
            Me.cmbLegale.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select MOROSITA_LEGALI.ID as ID,trim(COGNOME) as COGNOME,trim(MOROSITA_LEGALI.NOME) as NOME, " _
                                   & " RTRIM(LTRIM(TIPO_INDIRIZZO||' '||INDIRIZZO|| ' N. '||CIVICO)) as INDIRIZZO," _
                                   & " trim(SEPA.COMUNI_NAZIONI.NOME) as COMUNE, " _
                                   & " MOROSITA_LEGALI.CAP as CAP" _
                             & " from  SISCOM_MI.MOROSITA_LEGALI,SEPA.COMUNI_NAZIONI " _
                             & " where  SISCOM_MI.MOROSITA_LEGALI.COD_COMUNE=SEPA.COMUNI_NAZIONI.COD (+) "


            If par.IfEmpty(sValoreTribunale, "-1") <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_TRIBUNALI_COMPETENTI=" & sValoreTribunale
            End If


            If sValorePraticheDA <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ( select count(*) " _
                                                                & " from SISCOM_MI.MOROSITA_LEGALI_PRATICHE " _
                                                                & " where MOROSITA_LEGALI_PRATICHE.ID_LEGALE = MOROSITA_LEGALI.ID " _
                                                                & "   and DATA_CHIUSURA is  null)>=" & sValorePraticheDA
            End If

            If sValorePraticheA <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ( select count(*) " _
                                                                & " from SISCOM_MI.MOROSITA_LEGALI_PRATICHE " _
                                                                & " where MOROSITA_LEGALI_PRATICHE.ID_LEGALE = MOROSITA_LEGALI.ID " _
                                                                & "   and DATA_CHIUSURA is  null)<=" & sValorePraticheA
            End If


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                Me.cmbLegale.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME") & " " & myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            Select Case i
                Case 0
                    Me.cmbLegale.Items(0).Selected = True
                    Me.cmbLegale.Enabled = False
                Case 1
                    Me.cmbLegale.Items(1).Selected = True
                    Me.cmbLegale.Enabled = False
            End Select


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

End Class
