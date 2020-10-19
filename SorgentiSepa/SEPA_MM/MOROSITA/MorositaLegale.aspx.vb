Imports System.Collections

Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class MOROSITA_MorositaLegale
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
    Public sValoreID_MOROSITA_LEGALI As String

    Public sValorePraticheDA As String
    Public sValorePraticheA As String

    Public sOrdinamento As String

    Public lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Response.Expires = 0

        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))

        Try


            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)

            If Not IsPostBack Then

                Response.Flush()

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

                sValoreID_MOROSITA_LEGALI = Request.QueryString("LE")

                sOrdinamento = Request.QueryString("ORD")       'ORDINAMENTO


                Me.txtDataProtocollo.Text = par.FormattaData(Format(Now, "yyyyMMdd"))

                vIdMorositaPratica = -1


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)


                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

                BindGrid()

                SettaLegale()

                txtindietro.Text = 0


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next


                Me.txtDataProtocollo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")

                Me.btnSalva.Visible = True

                'cmbStato.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

                'If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 7, 1) = 0 Then
                '    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                '    FrmSolaLettura()
                'End If

            End If

        Catch ex As Exception

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


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

    Public Property vIdMorositaPratica() As Long
        Get
            If Not (ViewState("par_vIdMorositaPratica") Is Nothing) Then
                Return CLng(ViewState("par_vIdMorositaPratica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdMorositaPratica") = value
        End Set

    End Property



    Private Sub BindGrid()
        Dim ElencoID_Rapporti As String = ""
        Dim sStringaSQL1 As String

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim ContaIntestatari As Integer = 0

        Dim sStringa_SELECT_1 As String
        Dim sStringa_FROM_WHERE As String
        Dim sStringaSql As String

        Try

            sOrdinamento = Request.QueryString("ORD")       'ORDINAMENTO

            Dim gen As Epifani.ListaGenerale

            For Each gen In lstListaRapporti
                If ElencoID_Rapporti <> "" Then
                    ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                Else
                    ElencoID_Rapporti = gen.STR
                End If
            Next


            If ElencoID_Rapporti <> "" Then
                sStringaSql = "  and MOROSITA_LETTERE.ID in (" & ElencoID_Rapporti & ")"
            Else
                sStringaSql = "  and UNITA_IMMOBILIARI.ID in (select ID_UNITA from SISCOM_MI.BOL_BOLLETTE " _
                                                                          & " where  (NVL(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) " _
                                                                          & "   and   FL_ANNULLATA=0) "

            End If

            ' GRIGLIA ELENCO INQUILINI

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
                                    & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 ," _
                                    & " MOROSITA_LETTERE.ID_MOROSITA as ID_MOROSITA "

            'sStringa_SELECT_2 = "select  MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE," _
            '                   & " RAPPORTI_UTENZA.COD_CONTRATTO as  COD_CONTRATTO ," _
            '                       & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
            '                                                   & "     then  RAGIONE_SOCIALE " _
            '                                                   & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
            '                   & "  as  INTESTATARIO ," _
            '                   & " SISCOM_MI.MOROSITA_LETTERE.IMPORTO as DEBITO2 ," _
            '                   & " (case when MOROSITA_LETTERE.NUM_LETTERE=1 then 'MAV MG' " _
            '                       & " else 'MAV MA' end ) as TIPO_MAV, " _
            '                   & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC, " _
            '                   & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
            '                   & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE as  COD_UNITA_IMMOBILIARE ," _
            '                   & " UNITA_IMMOBILIARI.COD_TIPOLOGIA, " _
            '                   & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
            '                   & " INDIRIZZI.CIVICO,(select NOME from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
            '                   & " SISCOM_MI.MOROSITA_LETTERE.IMPORTO as DEBITO,  " _
            '                   & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
            '                   & "     then  trim(RAGIONE_SOCIALE) " _
            '                   & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 "

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

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.RAPPORTI_UTENZA")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("CheckBox1")
                chkExport.Checked = True
                ContaIntestatari = ContaIntestatari + 1
            Next

            Me.lblNumIntestatari.Text = ContaIntestatari

            Dim Trovati As Integer = 0
            Trovati = InquiliniNoCF("DA_BINDGRID")
            If Trovati = 1 Then
                Me.imgExcel.Visible = True
                Response.Write("<SCRIPT>alert('Attenzione: Riscontrato un inquilino con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            ElseIf Trovati > 1 Then
                Me.imgExcel.Visible = True
                Response.Write("<SCRIPT>alert('Attenzione: Riscontrati " & Trovati & " inquilini con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            Else
                Me.imgExcel.Visible = False
            End If
            '*********************************************************

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub



    Private Sub SettaLegale()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            par.cmd.CommandText = "select * " _
                               & " from  SISCOM_MI.MOROSITA_LEGALI " _
                               & " where  ID=" & par.IfEmpty(Request.QueryString("LE"), -1)

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtLegale.Text = par.IfNull(myReader1("COGNOME") & " " & myReader1("NOME"), " ")
            End If
            myReader1.Close()


            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
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

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        Else
            txtModificato.Text = "1"

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub

    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

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



            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()


            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")

            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else

                Response.Write("<script>location.replace('RisultatiMorositaLegale.aspx?FI=" & sValoreStrutturaAler _
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
                                                                                & "&ORD=" & sOrdinamento _
                                                    & "');</script>")

            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

        Dim FlagConnessione As Boolean

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim sListRapporti As String = ""

        Dim vIdMorosita As Long = -1


        Try

            FlagConnessione = False

            If ControlloCampi() = False Then
                Exit Sub
            End If


            If Me.txtAnnullo.Value = "1" Then

                Me.txtAnnullo.Value = "0"


                sValoreID_MOROSITA_LEGALI = Request.QueryString("LE")

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                FlagConnessione = True

                '1) per ogni pratica checkata, inserisco un record di MOROSITA_LEGALI_PRATICHE
                For Each oDataGridItem In Me.DataGrid1.Items
                    chkExport = oDataGridItem.FindControl("CheckBox1")
                    If chkExport.Checked = True Then

                        If vIdMorosita = -1 Or (vIdMorosita <> oDataGridItem.Cells(13).Text) Then
                            '2) INSERT MOROSITA_LEGALI_PRATICHE

                            vIdMorosita = oDataGridItem.Cells(13).Text
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MOROSITA_LEGALI_PRATICHE.NEXTVAL FROM DUAL"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                vIdMorositaPratica = myReader1(0)
                            End If
                            myReader1.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " insert into SISCOM_MI.MOROSITA_LEGALI_PRATICHE " _
                                                         & " (ID,ID_LEGALE,ID_MOROSITA_LETTERA1,PROTOCOLLO,DATA_APERTURA,NOTE)" _
                                                & " values (:id,:id_legale,:id_morosita,:protocollo,:data,:note)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdMorositaPratica))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_legale", sValoreID_MOROSITA_LEGALI))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", oDataGridItem.Cells(0).Text))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("protocollo", Me.txtProtocollo.Text))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.IfEmpty(par.AggiustaData(Me.txtDataProtocollo.Text), "")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Me.txtNote.Text))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '*****************************************


                            '****************MYEVENT*****************
                            Dim sStr1 As String = ""

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                          & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", oDataGridItem.Cells(0).Text))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.STRAGIUDIZIALE_AFFIDAMENTO_LEGALE))))

                            sStr1 = "Fase stragiudiziale (legali esterni) - Avv. " & Me.txtLegale.Text & " - Protocollo: " & Me.txtProtocollo.Text & " - Data Affidamento: " & Me.txtDataProtocollo.Text
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                                & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.STRAGIUDIZIALE_AFFIDAMENTO_LEGALE)) & "'" _
                                                & " where ID=" & oDataGridItem.Cells(0).Text

                            par.cmd.ExecuteNonQuery()
                            par.cmd.Parameters.Clear()


                        Else

                            If vIdMorositaPratica <> -1 Then
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.MOROSITA_LEGALI_PRATICHE set ID_MOROSITA_LETTERA2=" & oDataGridItem.Cells(0).Text _
                                                    & " where id=" & vIdMorositaPratica

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""


                                '****************MYEVENT*****************
                                Dim sStr1 As String = ""

                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", oDataGridItem.Cells(0).Text))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.STRAGIUDIZIALE_AFFIDAMENTO_LEGALE))))

                                sStr1 = "Fase stragiudiziale (legali esterni) - Avv. " & Me.txtLegale.Text & " - Protocollo: " & Me.txtProtocollo.Text & " - Data Affidamento: " & Me.txtDataProtocollo.Text
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                par.cmd.Parameters.Clear()
                                '************************************************

                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                                    & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.STRAGIUDIZIALE_AFFIDAMENTO_LEGALE)) & "'" _
                                                    & " where ID=" & oDataGridItem.Cells(0).Text

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()


                                vIdMorositaPratica = -1
                            End If
                        End If
                    End If
                Next



                ' COMMIT
                par.myTrans.Commit()
                par.cmd.CommandText = ""
                FlagConnessione = False


                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtModificato.Text = "0"

                ' Response.Redirect("CreaLettere2.aspx?IDMOR=" & vIdMorosita, False)

                'Response.Write("<script>window.open('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "','Crea Lettera " & Format(Now, "hhss") & "','height=300,top=0,left=0,width=500');</script>")
                'Response.Write("<script>window.showModalDialog('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "',window, 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');</script>")

                btnSalva.Visible = False

            Else
                Me.txtAnnullo.Value = "0"
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            End If

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            Page.Dispose()


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Public Function ControlloCampi() As Boolean
        Dim ElencoID_Rapporti As String = ""

        Dim trovato As Boolean
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim Trovati As Integer = 0

        ControlloCampi = True
        trovato = False

        If Strings.Len(Strings.Trim(Me.txtProtocollo.Text)) = 0 Then
            Response.Write("<script>alert('Attenzione: Inserire il protocollo!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If Strings.Len(Strings.Trim(Me.txtDataProtocollo.Text)) = 0 Then
            Response.Write("<script>alert('Attenzione: Inserire la data di affidamento!');</script>")
            ControlloCampi = False
            Exit Function
        End If


        trovato = False
        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("CheckBox1")
            If chkExport.Checked = True Then
                trovato = True
                Exit For
            End If
        Next

        If trovato = False Then
            Response.Write("<SCRIPT>alert('Attenzione: Selezionare almeno un inquilino dalla lista!');</SCRIPT>")
            ControlloCampi = False
            Exit Function
        End If


        Trovati = InquiliniNoCF("DA_SALVA")
        If Trovati = 1 Then
            ControlloCampi = False
            Me.imgExcel.Visible = True

            'Response.Write("<SCRIPT>alert('Attenzione: premendo il bottone \'Elenco CF Errati\' verrà visualizzato l\'elenco di inquilini con codice fiscale errato.');</SCRIPT>")
            Response.Write("<SCRIPT>alert('Attenzione: Riscontrato un inquilino con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            Exit Function
        ElseIf Trovati > 1 Then
            ControlloCampi = False
            Me.imgExcel.Visible = True
            Response.Write("<SCRIPT>alert('Attenzione: Riscontrati " & Trovati & " inquilini con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            Exit Function
        End If



    End Function


    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                End If
            Next


        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

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

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function




    Public Sub ControllaCF()
        Dim ElencoID_Rapporti As String = ""
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String

        Dim FlagConnessione As Boolean

        Try

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
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CODICE FISCALE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COD. UNITA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO UNITA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE", 12)

                K = 2


                Dim gen As Epifani.ListaGenerale

                For Each gen In lstListaRapporti
                    If ElencoID_Rapporti <> "" Then
                        ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                    Else
                        ElencoID_Rapporti = gen.STR
                    End If
                Next

                ' APRO CONNESSIONE
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim sStr1 As String

                sStr1 = "select  UNITA_IMMOBILIARI.COD_TIPOLOGIA," _
                        & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                        & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                        & " (UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) as  COD_UNITA_IMMOBILIARE ," _
                        & " (RAPPORTI_UTENZA.COD_CONTRATTO) as  COD_CONTRATTO ," _
                        & "RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                            & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                            & "     then  RAGIONE_SOCIALE " _
                            & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end  as  INTESTATARIO ," _
                        & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.RAGIONE_SOCIALE," _
                        & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO"" " _
              & " from  " _
                 & " SISCOM_MI.RAPPORTI_UTENZA,  " _
                 & " SISCOM_MI.INDIRIZZI, " _
                 & " SISCOM_MI.UNITA_IMMOBILIARI,  " _
                 & " SISCOM_MI.UNITA_CONTRATTUALE,  " _
                 & " SISCOM_MI.ANAGRAFICA, " _
                 & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                 & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE " _
            & " where  " _
                  & "      UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                  & " and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID   " _
                  & " and  RAPPORTI_UTENZA.ID 					     =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                  & " and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID " _
                  & " and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                  & " and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID  " _
                  & " and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                  & " and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null   "

                If ElencoID_Rapporti <> "" Then
                    sStr1 = sStr1 & "  and RAPPORTI_UTENZA.ID in (" & ElencoID_Rapporti & ")"
                Else
                    sStr1 = sStr1 & "  and UNITA_IMMOBILIARI.ID in (select ID_UNITA from SISCOM_MI.BOL_BOLLETTE " _
                                                                              & " where  (NVL(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) " _
                                                                              & "   and   FL_ANNULLATA=0) "

                End If


                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderT = par.cmd.ExecuteReader

                Dim CF_Errato As Boolean

                While myReaderT.Read
                    If par.IfNull(myReaderT("RAGIONE_SOCIALE"), "") = "" Then

                        CF_Errato = False

                        If par.ControllaCF(UCase(par.IfNull(myReaderT("COD_FISCALE"), ""))) = False Then
                            CF_Errato = True
                        ElseIf par.ControllaCFNomeCognome(UCase(par.IfNull(myReaderT("COD_FISCALE"), "")), par.IfNull(myReaderT("COGNOME"), ""), par.IfNull(myReaderT("NOME"), "")) = False Then
                            CF_Errato = True
                        End If


                        If CF_Errato = True Then


                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(myReaderT("COD_CONTRATTO"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(myReaderT("INTESTATARIO"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(myReaderT("COD_FISCALE"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA_CONTR_LOC"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(myReaderT("POSIZIONE_CONTRATTO"), "")))

                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(myReaderT("COD_UNITA_IMMOBILIARE"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA"), "")))

                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(myReaderT("INDIRIZZO"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(myReaderT("CIVICO"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(myReaderT("COMUNE_UNITA"), "")))

                            i = i + 1
                            K = K + 1
                        End If
                    End If
                End While
                myReaderT.Close()

                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                .CloseFile()
            End With

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

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


            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip", False)



            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")


        End Try


    End Sub


    Protected Sub imgExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExcel.Click
        ControllaCF()
    End Sub


    Public Function InquiliniNoCF(ByVal TipoControllo As String) As Integer
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim sStr1 As String
        Dim ElencoID_Rapporti As String = ""
        Dim gen As Epifani.ListaGenerale

        Dim FlagConnessione As Boolean

        Try
            InquiliniNoCF = 0

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            If TipoControllo = "DA_SALVA" Then
                For Each oDataGridItem In Me.DataGrid1.Items
                    chkExport = oDataGridItem.FindControl("CheckBox1")
                    If chkExport.Checked = True Then

                        If ElencoID_Rapporti <> "" Then
                            ElencoID_Rapporti = ElencoID_Rapporti & "," & oDataGridItem.Cells(0).Text
                        Else
                            ElencoID_Rapporti = oDataGridItem.Cells(0).Text
                        End If

                    End If
                Next
            Else

                For Each gen In lstListaRapporti
                    If ElencoID_Rapporti <> "" Then
                        ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                    Else
                        ElencoID_Rapporti = gen.STR
                    End If
                Next
            End If

            sStr1 = "select ANAGRAFICA.ID as ID_ANAGRAFICA,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.COD_FISCALE, ANAGRAFICA.RAGIONE_SOCIALE,RAPPORTI_UTENZA.ID as ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR" _
             & " from   SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
             & " where  RAPPORTI_UTENZA.ID in (" & ElencoID_Rapporti & ")" _
             & "   and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
             & "   and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID  " _
             & "   and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
             & " order by ID_CONTRATTO	"

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            myReaderT = par.cmd.ExecuteReader

            Dim CF_Errato As Boolean

            While myReaderT.Read
                CF_Errato = False

                If par.IfNull(myReaderT("RAGIONE_SOCIALE"), "") = "" Then
                    If par.ControllaCF(UCase(par.IfNull(myReaderT("COD_FISCALE"), ""))) = False Then
                        CF_Errato = True
                    ElseIf par.ControllaCFNomeCognome(UCase(par.IfNull(myReaderT("COD_FISCALE"), "")), par.IfNull(myReaderT("COGNOME"), ""), par.IfNull(myReaderT("NOME"), "")) = False Then
                        CF_Errato = True
                    End If

                    If CF_Errato = True Then
                        For Each oDataGridItem In Me.DataGrid1.Items

                            If TipoControllo = "DA_SALVA" Then
                                InquiliniNoCF = InquiliniNoCF + 1
                                Exit For
                            Else
                                If par.IfNull(myReaderT("ID_CONTRATTO"), "") = oDataGridItem.Cells(0).Text Then

                                    chkExport = oDataGridItem.FindControl("CheckBox1")
                                    chkExport.Checked = False
                                    InquiliniNoCF = InquiliniNoCF + 1
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                End If

            End While
            myReaderT.Close()


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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Function

End Class
