' VISUALIZZAZIONE E RI-STAMPA MOROSITA + LISTA INQUILINI (con possibilità di entrare nel dettaglio della morosità dell'inquilino)

Imports System.Collections
Imports System.IO

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class MOROSITA_Morosita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreStrutturaAler As String
    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreIndirizzo As String
    Public sValoreCivico As String

    Public sValoreTI As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreData_Dal_P As String
    Public sValoreData_Al_P As String
    Public sValoreProtocollo As String

    Public sValoreCodice As String
    Public sValoreCognome As String
    Public sValoreNome As String

    Public sOrdinamento As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Response.Expires = 0

        Try

            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)


            If Not IsPostBack Then
                Response.Flush()

                sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreIndirizzo = Request.QueryString("IN")
                sValoreCivico = Request.QueryString("CI")

                sValoreTI = UCase(Request.QueryString("TI"))

                sValoreData_Dal = Request.QueryString("DAL")
                sValoreData_Al = Request.QueryString("AL")

                sValoreData_Dal_P = Request.QueryString("DAL_P")
                sValoreData_Al_P = Request.QueryString("AL_P")
                sValoreProtocollo = Request.QueryString("PRO")


                sValoreCodice = Request.QueryString("CD")
                sValoreCognome = Request.QueryString("CG")
                sValoreNome = Request.QueryString("NM")

                sOrdinamento = Request.QueryString("ORD")


                vIdMorosita = Request.QueryString("ID") 'Session.Item("ID")


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")
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
                '*******************************************

                SettaggioCampi()

                VisualizzaDati()

                Me.txtindietro.Text = 0

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

                'cmbStato.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")

                If par.IfEmpty(Request.QueryString("CHIAMANTE"), "") = "REPORT" Then
                    'CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    Me.btnINDIETRO.Visible = False
                    FrmSolaLettura()
                End If

                If Session.Item("MOD_MOROSITA_ANN") <> "1" Then
                    Me.btnAnnulla.Visible = False
                End If

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

    Public Property vIdMorosita() As Long
        Get
            If Not (ViewState("par_vIdMorosita") Is Nothing) Then
                Return CLng(ViewState("par_vIdMorosita"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdMorosita") = value
        End Set

    End Property




    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If Me.txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")


            Page.Dispose()

            If par.IfEmpty(Request.QueryString("CHIAMANTE"), "") = "REPORT" Then
                Response.Write("<script>window.close();</script>")
            Else

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Else
            txtModificato.Text = "1"

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub

    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreIndirizzo = Request.QueryString("IN")
            sValoreCivico = Request.QueryString("CI")

            sValoreTI = UCase(Request.QueryString("TI"))

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreData_Dal_P = Request.QueryString("DAL_P")
            sValoreData_Al_P = Request.QueryString("AL_P")
            sValoreProtocollo = Request.QueryString("PRO")

            sValoreCodice = Request.QueryString("CD")
            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")

            sOrdinamento = Request.QueryString("ORD")


            Page.Dispose()


            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")

            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else
                Response.Write("<script>location.replace('RisultatiMorosita.aspx?FI=" & sValoreStrutturaAler _
                                                                                 & "&CO=" & sValoreComplesso _
                                                                                 & "&ED=" & sValoreEdificio _
                                                                                 & "&TI=" & sValoreTI _
                                                                                 & "&IN=" & par.VaroleDaPassare(sValoreIndirizzo) _
                                                                                 & "&CI=" & par.VaroleDaPassare(sValoreCivico) _
                                                                                & "&DAL=" & sValoreData_Dal _
                                                                                 & "&AL=" & sValoreData_Al _
                                                                                 & "&CD=" & par.VaroleDaPassare(sValoreCodice) _
                                                                                 & "&CG=" & par.VaroleDaPassare(sValoreCognome) _
                                                                                 & "&NM=" & par.VaroleDaPassare(sValoreNome) _
                                                                                & "&DAL_P=" & sValoreData_Dal_P _
                                                                                & "&AL_P=" & sValoreData_Al_P _
                                                                                & "&PRO=" & par.VaroleDaPassare(sValoreProtocollo) _
                                                                                & "&ORD=" & sOrdinamento _
                                                                                 & "');</script>")


            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub



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



    Private Sub SettaggioCampi()


        Me.cmbTipoInvio.Items.Add(New ListItem("Racc. A.R.", "Racc. A.R."))
        Me.cmbTipoInvio.Items.Add(New ListItem("Racc. a mano", "Racc. mano"))
        Me.cmbTipoInvio.Items.Add(New ListItem("FAX", "FAX"))

    End Sub


    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try

            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdMorosita <> 0 Then
                ' LEGGO ODL
                par.cmd.CommandText = "select * from SISCOM_MI.MOROSITA  where ID=" & vIdMorosita '& " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()



                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                'HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                'par.myTrans = par.OracleConn.BeginTransaction()
                '‘‘par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "0") ' Session.Add("LAVORAZIONE", "1")


                Me.txtVisualizza.Value = 0 'SOLO LETTURA
                FrmSolaLettura()
            End If


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Pagamenti aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA

                par.cmd.CommandText = "select * from SISCOM_MI.MOROSITA where ID=" & vIdMorosita
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                Me.txtVisualizza.Value = 1 'SOLO LETTURA
                FrmSolaLettura()

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Dim sStr1 As String
        Dim Somma1 As Decimal = 0
        Dim Somma2 As Decimal = 0
        Dim contaNoAnnullati As Integer = 0

        'Me.txtVisualizza.Value = 1  'SEMPRE


        sValoreComplesso = Request.QueryString("CO")
        sValoreEdificio = Request.QueryString("ED")
        sValoreIndirizzo = Request.QueryString("IN")
        sValoreCivico = Request.QueryString("CI")

        sValoreTI = UCase(Request.QueryString("TI"))

        sValoreData_Dal = Request.QueryString("DAL")
        sValoreData_Al = Request.QueryString("AL")


        sValoreCodice = Request.QueryString("CD")
        sValoreCognome = Request.QueryString("CG")
        sValoreNome = Request.QueryString("NM")

        sOrdinamento = Request.QueryString("ORD")



        Me.cmbTipoInvio.Items.FindByValue(par.IfNull(myReader1("TIPO_INVIO"), "Racc. A.R.")).Selected = True
        Me.txtNote.Text = par.IfNull(myReader1("NOTE"), "")

        Me.txtProtocollo.Text = par.IfNull(myReader1("PROTOCOLLO_ALER"), "")
        Me.txtDataProtocolloAler.Text = par.FormattaData(par.IfNull(myReader1("DATA_PROTOCOLLO"), ""))


        Me.lbl_RIF_DA.Text = par.FormattaData(par.IfNull(myReader1("RIF_DA"), ""))
        Me.lbl_RIF_A.Text = par.FormattaData(par.IfNull(myReader1("RIF_A"), ""))


        'Me.cmbTitoloD.Items.FindByText(par.IfNull(myReader1("TITOLO_DIRIGENTE"), "Dott.")).Selected = True
        'Me.cmbTitoloR.Items.FindByText(par.IfNull(myReader1("TITOLO_RESPONSABILE"), "Dott.")).Selected = True
        'Me.cmbTitoloT.Items.FindByText(par.IfNull(myReader1("TITOLO_TRATTATA"), "Dott.")).Selected = True

        'Me.txtCognomeD.Text = par.IfNull(myReader1("COGNOME_DIRIGENTE"), "")
        'Me.txtCognomeR.Text = par.IfNull(myReader1("COGNOME_RESPONSABILE"), "")
        'Me.txtCognomeT.Text = par.IfNull(myReader1("COGNOME_TRATTATA"), "")

        'Me.txtNomeD.Text = par.IfNull(myReader1("NOME_DIRIGENTE"), "")
        'Me.txtNomeR.Text = par.IfNull(myReader1("NOME_RESPONSABILE"), "")
        'Me.txtNomeT.Text = par.IfNull(myReader1("NOME_TRATTATA"), "")

        'Me.txtTelefonoD.Text = par.IfNull(myReader1("TELEFONO_DIRIGENTE"), "")
        'Me.txtTelefonoR.Text = par.IfNull(myReader1("TELEFONO_RESPONSABILE"), "")
        'Me.txtTelefonoT.Text = par.IfNull(myReader1("TELEFONO_TRATTATA"), "")


        sStr1 = "select  UNITA_IMMOBILIARI.COD_TIPOLOGIA," _
                    & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                    & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ,"

        If par.IfEmpty(Request.QueryString("CHIAMANTE"), "") = "REPORT" Then
            sStr1 = sStr1 & " '' as  MOROSITA ,"
        Else
            sStr1 = sStr1 & " replace(replace('<a href=£javascript:void(0)£ onclick=£location.replace(''MorositaInquilino.aspx?ID_MOR=" & vIdMorosita & "$X=0" & "$CO=" & sValoreComplesso & "$ED=" & sValoreEdificio & "$IN=" & par.VaroleDaPassare(sValoreIndirizzo) & "$CI=" & par.VaroleDaPassare(sValoreCivico) & "$TI=" & sValoreTI & "$DAL=" & sValoreData_Dal & "$AL=" & sValoreData_Al & "$CD=" & par.VaroleDaPassare(sValoreCodice) & "$CG=" & par.VaroleDaPassare(sValoreCognome) & "$PROV=RICERCA_MOROSITA$NM=" & par.VaroleDaPassare(sValoreNome) & "$DAL_P=" & sValoreData_Dal_P & "$AL_P=" & sValoreData_Al_P & "$PRO=" & par.VaroleDaPassare(sValoreProtocollo) & "$ORD=" & sOrdinamento & "$CON='||RAPPORTI_UTENZA.ID||'" & "$ANA='||ANAGRAFICA.ID||''');£>Dettagli</a>','$','&'),'£','" & Chr(34) & "') as  MOROSITA ,"
        End If

        sStr1 = sStr1 & "RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'',''top=0,left=0'');£>'||" _
                        & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                                    & "     then  RAGIONE_SOCIALE " _
                                                    & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                    & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                    & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                    & "     then  trim(RAGIONE_SOCIALE) " _
                    & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 ," _
                    & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_INIZIALE,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & " and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_MOROSITA, " _
                    & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_BOLLETTA,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & "and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_BOLLETTATO,  " _
                    & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_CANONE,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & " and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_CANONI, " _
                    & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_ONERI,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & " and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_ONERI_SERVIZI  " _
          & " from  " _
                & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE," _
                & " SISCOM_MI.RAPPORTI_UTENZA, " _
                & " SISCOM_MI.ANAGRAFICA," _
                & " SISCOM_MI.INDIRIZZI," _
                & " SISCOM_MI.EDIFICI," _
                & " SISCOM_MI.UNITA_CONTRATTUALE," _
                & " SISCOM_MI.UNITA_IMMOBILIARI," _
                & " SISCOM_MI.SOGGETTI_CONTRATTUALI " _
        & " where  " _
        & "       EDIFICI.ID_COMPLESSO                      =COMPLESSI_IMMOBILIARI.ID " _
        & "  and  UNITA_IMMOBILIARI.ID_EDIFICIO             =EDIFICI.ID" _
        & "  and  UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
        & "  and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID " _
        & "  and  UNITA_CONTRATTUALE.ID_CONTRATTO           =RAPPORTI_UTENZA.ID " _
        & "  and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
        & "  and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
        & "  and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID" _
        & "  and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
        & "  and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null  " _
        & "  and RAPPORTI_UTENZA.ID in ( select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & ") " _
        & "  order by INTESTATARIO2"



        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStr1, par.OracleConn)

        Dim ds As New Data.DataTable()

        da.Fill(ds) ', "SISCOM_MI.RAPPORTI_UTENZA")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Dim ContaIntestatari As Integer

        For Each oDataGridItem In Me.DataGrid1.Items
            ContaIntestatari = ContaIntestatari + 1
        Next

        Me.lblNum1.Text = ContaIntestatari

        da.Dispose()
        ds.Dispose()

        Me.btnStampa.Visible = True
        Me.btnAnnulla.Visible = True
        Me.btnRigenera.Visible = False 'NON USO più il Rigenera ma si deve fare per il momento inquilini x inquilino nella propria scheda

        'CONTROLLO SE LA MOROSITA si può ancora annullare, cioè se lo stato è M00 oppure nullo in caso di errore MAV (non dovrebbe accadere) o MAV ERRATO M94
        par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LETTERE " _
                            & " where ID_MOROSITA=" & vIdMorosita _
                            & "   and COD_STATO<>'M00' " _
                            & "   and COD_STATO Is Not null " _
                            & "   and COD_STATO<>'M94'"
        myReaderT = par.cmd.ExecuteReader()
        If myReaderT.Read Then
            If par.IfNull(myReaderT(0), 0) > 0 Then
                Me.btnAnnulla.Visible = False
            End If
        End If
        myReaderT.Close()



        'CONTROLLO SE LA MOROSITA è stata tutta ANNULLATA (disabilito Annulla e Stampa)
        par.cmd.CommandText = "select distinct(COD_STATO) from SISCOM_MI.MOROSITA_LETTERE " _
                            & " where ID_MOROSITA=" & vIdMorosita

        myReaderT = par.cmd.ExecuteReader()
        While myReaderT.Read
            If par.IfNull(myReaderT(0), "") <> "M98" Then
                contaNoAnnullati = contaNoAnnullati + 1
            End If
        End While
        myReaderT.Close()

        If contaNoAnnullati = 0 Then
            Me.btnStampa.Visible = False
            Me.btnAnnulla.Visible = False
        End If

        If Session.Item("MOD_MOROSITA_ANN") <> "1" Then
            Me.btnAnnulla.Visible = False
        End If


        'CONTROLLO SE LA MOROSITA è STATA ANNULLATA
        'Me.btnRigenera.Visible = False
        'par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LETTERE where BOLLETTINO IS NULL and ID_MOROSITA=" & vIdMorosita

        'myReaderT = par.cmd.ExecuteReader()
        'If myReaderT.Read Then
        '    If par.IfNull(myReaderT(0), 0) > 0 Then
        '        If Me.btnStampa.Visible = True Then
        '            'Se il bottone stampa è false vuol dire che è stata annullata l'intera morosità
        '            Me.btnRigenera.Visible = True
        '        End If
        '    End If
        'End If
        'myReaderT.Close()


        sStr1 = "select SUM(NVL(IMPORTO_INIZIALE,0)) as IMPORTO_INIZIALE ,SUM(NVL(IMPORTO_BOLLETTA,0)) as IMPORTO_BOLLETTA " _
                & " from   SISCOM_MI.MOROSITA_LETTERE " _
                & " where  ID_MOROSITA=" & vIdMorosita & " and cod_stato <> 'M98'"

        par.cmd.Parameters.Clear()
        par.cmd.CommandText = sStr1
        myReaderT = par.cmd.ExecuteReader()

        If myReaderT.Read Then
            Somma1 = par.IfNull(myReaderT("IMPORTO_INIZIALE"), 0)
            Somma2 = par.IfNull(myReaderT("IMPORTO_BOLLETTA"), 0)
        End If
        myReaderT.Close()

        Me.lblTotMor.Text = IsNumFormat(Somma1, "", "##,##0.00") & " €"
        Me.lblTotBol.Text = IsNumFormat(Somma2, "", "##,##0.00") & " €"



    End Sub



    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click


        'If Me.txtAnnullo.Value = "1" Then

        '    Me.txtAnnullo.Value = "0"

        'Response.Write("<script>window.open('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "','MOROSITA','');</script>")
        Response.Write("<script>window.showModalDialog('VisualizzaFileZIP.aspx?IDMOR=" & vIdMorosita & "',window, 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');</script>")

        'End If


        'MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
        'MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
        'MiaSHTML = MiaSHTML & "<td width='450px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
        'MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
        'MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

        'i = 0
        'MIOCOLORE = "#CCFFFF"

        'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("Varie\"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & vIdMorosita & "-*.zip")
        '    ReDim Preserve ElencoFile(i)
        '    ElencoFile(i) = foundFile
        '    i = i + 1
        'Next

        'If i > 0 Then
        '    For j = 0 To i - 1
        '        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
        '        MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='Varie/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
        '        MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

        '        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
        '        If MIOCOLORE = "#CCFFFF" Then
        '            MIOCOLORE = "#FFFFCC"
        '        Else
        '            MIOCOLORE = "#CCFFFF"
        '        End If
        '        If j = 10 Then Exit For
        '    Next j
        'End If
        'MiaSHTML = MiaSHTML & "</table>" & vbCrLf
        'Response.Write(MiaSHTML)
        'Exit Sub


        'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("Varie/"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & vIdMorosita & "*.zip")
        '    '            Response.Write("<script>window.open('" & foundFile & "','Stampa','');</script>")
        '    'Response.Write("<script>window.open('Allegati_1.aspx?NOME=" & foundFile & "&EXT=ZIP','Export','');location.replace('CreaXML.aspx');</script>")

        '    Response.ContentType = "application/force-download"
        '    Response.AddHeader("Content-Disposition", "attachment;filename=" & foundFile)
        '    Response.BufferOutput = True
        '    Response.BinaryWrite(FileIO.FileSystem.ReadAllBytes(Server.MapPath(foundFile)))
        '    Response.End()

        '    Exit For
        'Next


    End Sub


    Private Function RicavaFile(ByVal sFile As String) As String
        Dim N As Integer

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Dim FlagConnessione As Boolean

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim sStr1 As String

        Try

            FlagConnessione = False

            If Me.txtAnnullo.Value = "1" Then

                Me.txtAnnullo.Value = "0"


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")
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
                '*******************************************


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)



                FlagConnessione = True

                '1) ANNULLO LE BOLLETTE DEI MAV della MOROSITA (vIdMorosita)
                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                                   & " set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "' " _
                                   & " where ID_MOROSITA=" & vIdMorosita _
                                   & "   and ID_BOLLETTA_RIC is null and id_tipo = 4"

                par.cmd.ExecuteNonQuery()


                '2) RIPRISTINO TUTTE le BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                   & " set  IMPORTO_RICLASSIFICATO=Null " _
                                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                        & " where ID_MOROSITA=" & vIdMorosita _
                                                        & "   and FL_ANNULLATA=0 " _
                                                        & "   and ID_RATEIZZAZIONE IS NULL ) " _
                                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"
                ' & "   and ID_BOLLETTA_RIC is not null " _
                par.cmd.ExecuteNonQuery()
                '**************************************

                '2) RIPRISTINO TUTTE le BOLLETTE interessate della MOROSITA (vIdMorosita)
                ' 13/09/2013 AGGIUNTO ID_MOROSITA_LETTERA = NULL
                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                                   & " set ID_BOLLETTA_RIC=Null," _
                                   & "     ID_MOROSITA=Null, " _
                                   & " ID_MOROSITA_LETTERA = NULL " _
                                   & " where ID_MOROSITA=" & vIdMorosita _
                                   & "   and FL_ANNULLATA=0 " _
                                   & "   and ID_RATEIZZAZIONE IS NULL"
                '                                   & "   and ID_BOLLETTA_RIC is not null " _

                par.cmd.ExecuteNonQuery()


                '3) MODIFICO LO STATO DELLE MOROSITA LETTERE
                par.cmd.CommandText = "update  SISCOM_MI.MOROSITA_LETTERE " _
                                   & " set COD_STATO='M98' " _
                                   & " where ID_MOROSITA=" & vIdMorosita

                par.cmd.ExecuteNonQuery()


                sStr1 = "select ID " _
                    & " from   SISCOM_MI.MOROSITA_LETTERE " _
                    & " where ID_MOROSITA=" & vIdMorosita

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReader1 = par.cmd.ExecuteReader()

                While myReader1.Read

                    ''****************MYEVENT**PRIMA del 2009 LETTERA 1 di 2***************

                    sStr1 = "Annullata la morosità"

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                  & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", par.IfNull(myReader1("ID"), 0)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MOROSITA_ANNULLATA), "00")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '************************************************

                End While
                myReader1.Close()

                par.cmd.CommandText = "update siscom_mi.morosita set fl_annullata = 1 where id = " & vIdMorosita
                par.cmd.ExecuteNonQuery()

                ' COMMIT
                par.myTrans.Commit()
                par.cmd.CommandText = ""
                FlagConnessione = False


                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")


                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                Me.txtModificato.Text = "0"


                Session.Add("LAVORAZIONE", "0")

                Me.btnAnnulla.Visible = False
                Me.btnStampa.Visible = False
                Me.btnRigenera.Visible = False

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

    Protected Sub btnRigenera_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnRigenera.Click

        If Me.txtAnnullo.Value = "1" Then

            Me.txtAnnullo.Value = "0"
            Response.Write("<script>window.showModalDialog('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "',window, 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');</script>")

        End If

    End Sub


    Sub ExportElenco()

        Dim ElencoID_Rapporti As String = ""
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String

        Dim FlagConnessione As Boolean

        Try

            sNomeFile = "Export_Inquilini_Mor_" & vIdMorosita & "_" & Format(Now, "yyyyMMddHHmmss")
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CODICE FISCALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "POSIZIONE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COD. UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CIVICO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE", 0)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "Tot. MOROSITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "Tot. BOLLETTATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "Tot. CANONI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "Tot. ONERI e SERVIZI", 0)


                .SetColumnWidth(1, 1, 25)   'CODICE 
                .SetColumnWidth(2, 2, 35)   'INTESTATARIO 
                .SetColumnWidth(3, 3, 25)   'CF
                .SetColumnWidth(4, 4, 10)   'TIPO
                .SetColumnWidth(5, 5, 20)   'POSIZIONE
                .SetColumnWidth(6, 6, 20)   'COD UNITA
                .SetColumnWidth(7, 7, 15)   'TIPO UNITA
                .SetColumnWidth(8, 8, 35)   'INDIRIZZO
                .SetColumnWidth(9, 10, 10)  'CIVICO e COMUNE

                .SetColumnWidth(11, 14, 20)  'Importi

                K = 2


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
                            & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.PARTITA_IVA," _
                            & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                            & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_INIZIALE,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & " and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_MOROSITA, " _
                            & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_BOLLETTA,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & "and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_BOLLETTATO,  " _
                            & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_CANONE,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & " and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_CANONI, " _
                            & " TRIM (TO_CHAR(( select SUM(NVL(IMPORTO_ONERI,0)) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & " and ID_CONTRATTO=RAPPORTI_UTENZA.ID),'9G999G999G999G999G990D99'))    as TOT_ONERI_SERVIZI  " _
                  & " from  " _
                        & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE," _
                        & " SISCOM_MI.RAPPORTI_UTENZA, " _
                        & " SISCOM_MI.ANAGRAFICA," _
                        & " SISCOM_MI.INDIRIZZI," _
                        & " SISCOM_MI.EDIFICI," _
                        & " SISCOM_MI.UNITA_CONTRATTUALE," _
                        & " SISCOM_MI.UNITA_IMMOBILIARI," _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                & " where  " _
                & "       EDIFICI.ID_COMPLESSO                      =COMPLESSI_IMMOBILIARI.ID " _
                & "  and  UNITA_IMMOBILIARI.ID_EDIFICIO             =EDIFICI.ID" _
                & "  and  UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                & "  and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID " _
                & "  and  UNITA_CONTRATTUALE.ID_CONTRATTO           =RAPPORTI_UTENZA.ID " _
                & "  and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
                & "  and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                & "  and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID" _
                & "  and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
                & "  and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null  " _
                & "  and RAPPORTI_UTENZA.ID in ( select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & vIdMorosita & ") " _
                & "  order by INTESTATARIO"

                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderT = par.cmd.ExecuteReader

                While myReaderT.Read

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(myReaderT("COD_CONTRATTO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(myReaderT("INTESTATARIO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(myReaderT("COD_FISCALE"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA_CONTR_LOC"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(myReaderT("POSIZIONE_CONTRATTO"), "")), 0)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(myReaderT("COD_UNITA_IMMOBILIARE"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA"), "")), 0)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(myReaderT("INDIRIZZO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(myReaderT("CIVICO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(myReaderT("COMUNE_UNITA"), "")), 0)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(myReaderT("TOT_MOROSITA"), 0))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(myReaderT("TOT_BOLLETTATO"), 0))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(myReaderT("TOT_CANONI"), 0))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(myReaderT("TOT_ONERI_SERVIZI"), 0))


                    i = i + 1
                    K = K + 1
                End While
                myReaderT.Close()

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


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Protected Sub btnXLS_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnXLS.Click
        ExportElenco()
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound



        If e.Item.ItemType = ListItemType.Item Then
            par.cmd.CommandText = "SELECT cod_stato FROM siscom_mi.morosita_lettere WHERE ID_contratto = " & e.Item.Cells(0).Text & " AND id_morosita = " & vIdMorosita
            Dim stato As String = par.cmd.ExecuteScalar

            If stato <> "M00" Then
                e.Item.BackColor = Drawing.Color.Coral
            End If


        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            par.cmd.CommandText = "SELECT cod_stato FROM siscom_mi.morosita_lettere WHERE ID_contratto = " & e.Item.Cells(0).Text & " AND id_morosita = " & vIdMorosita
            Dim stato As String = par.cmd.ExecuteScalar

            If stato <> "M00" Then
                e.Item.BackColor = Drawing.Color.Coral
            End If

        End If

    End Sub
End Class
