'MOROSITA' - Storia Dettaglio INQUILINO

Imports System.Collections
Imports System.Data.OleDb



Partial Class MOROSITA_MorositaInquilino
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""

    Public TabberHideMG As String = ""
    Public TabberHideMA As String = ""


    Public sValoreStato As String

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreIndirizzo As String
    Public sValoreCivico As String

    Public sValoreCodice As String
    Public sValoreCognome As String
    Public sValoreNome As String

    Public sValoreTI As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sProvenienza As String

    Public sValoreData_Dal_P As String
    Public sValoreData_Al_P As String
    Public sValoreProtocollo As String

    Public sTipoRicerca As String

    Public sOrdinamento As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            'Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
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


                vIdMorosita = Request.QueryString("ID_MOR")

                Me.txtIdMorosita.Value = vIdMorosita
                'Me.txtIdAnagrafica.Value = UCase(Request.QueryString("ANA"))
                Me.txtIdContratto.Value = UCase(Request.QueryString("CON"))

                'x Ricerca
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

                sTipoRicerca = Request.QueryString("MORA")

                sProvenienza = Request.QueryString("PROV")
                sOrdinamento = Request.QueryString("ORD")
                '*************************************************

                Me.EVENTO_MODIFICATO.Value = "0"
                Me.txtContaEventiMG.Value = 0
                Me.txtContaEventiMA.Value = 0

                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                Me.txtConnessione.Value = CStr(lIdConnessione)

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                End If

                CaricaStatiEventi()

                CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "0"

                Me.txtVisualizza.Value = 1

                Me.txtIdBollette1.Value = -1
                Me.txtIdBollette2.Value = -1

                Me.txtIdMorositaLett1.Value = -1
                Me.txtIdMorositaLett2.Value = -1

                Me.txtContaMAV.Value = 0    'Conta se ci sono 1 o 2 MAV

                If vIdMorosita <> 0 Then
                    Me.btnINDIETRO.Visible = True
                    'Visibile = "style=" & Chr(34) & "visibility:Visible" & Chr(34)

                    TabberHideMG = "tabbertabhide"
                    TabberHideMA = "tabbertabhide"
                    Tabber1 = "tabbertabdefault"

                    VisualizzaDati()
                    Me.txtindietro.Value = 0

                Else

                    Me.btnINDIETRO.Visible = False
                    Me.txtindietro.Value = 1

                    TabberHideMG = "tabbertabhide"
                    TabberHideMA = "tabbertabhide"
                    Tabber1 = "tabbertabdefault"
                End If

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


                ''*** FORM DETTAGLI
                For Each CTRL In Me.Tab_Morosita_Generale.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                CType(Tab_Morosita_STATO_MG.FindControl("btnProcedi"), ImageButton).Attributes.Add("onclick", "ApriProcedureMG();")
                CType(Tab_Morosita_STATO_MA.FindControl("btnProcedi"), ImageButton).Attributes.Add("onclick", "ApriProcedureMA();")


                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")

                If Session.Item("MOD_MOROSITA_SL") = "1" Or Request.QueryString("X") = "1" Then
                    Me.txtVisualizza.Value = 2 'SOLO LETTURA
                    If Request.QueryString("X") = "1" Then
                        Me.btnINDIETRO.Visible = False
                    End If
                    CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                    FrmSolaLettura()

                End If

            End If


            If vIdMorosita <> 0 Then
                Me.EVENTO_MODIFICATO.Value = "0"

                vIdMorosita = CType(Me.Page.FindControl("txtIdMorosita"), HiddenField).Value

                CaricaSTATO_MAV_MA()
                CaricaSTATO_MAV_MG()


                If Me.EVENTO_MODIFICATO.Value = "1" Then

                    VisualizzaDati()
                    Me.EVENTO_MODIFICATO.Value = "0"
                End If
            End If


        Catch ex As Exception

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""

        Select Case Me.txttab.value
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
            Case "3"
                Tabber3 = "tabbertabdefault"
            Case "4"
                Tabber4 = "tabbertabdefault"
            Case "5"
                Tabber5 = "tabbertabdefault"
        End Select

        If txtContaMAV.Value = 2 Then
            'MG + MA
            TabberHideMG = "tabbertab"
            TabberHideMA = "tabbertab"
        Elseif  par.IfEmpty(Me.txtIdMorositaLett2.Value, -1) <> -1 Then
                'STATO MA (solo)
            TabberHideMG = "tabbertabhide"
            TabberHideMA = "tabbertab"
        Else
            'STATO MG (solo)
            TabberHideMG = "tabbertab"
            TabberHideMA = "tabbertabhide"
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


    Public Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try
            sValoreStato = Request.QueryString("ST") '0=PRENOTATO (1=EMESSO e 5=LIQUIDATO da PAGAMENTI)


            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdMorosita <> 0 Then
                ' LEGGO MOROSITA
                '& "  and  ID_ANAGRAFICA=" & Me.txtIdAnagrafica.Value 
                par.cmd.CommandText = " select * from SISCOM_MI.MOROSITA_LETTERE  " _
                                    & " where ID_MOROSITA=" & vIdMorosita _
                                    & "  and  ID_CONTRATTO=" & Me.txtIdContratto.Value _
                                    & "  and  NUM_LETTERE=1" _
                                    & " FOR UPDATE NOWAIT"

                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                ''CREO LA TRANSAZIONE
                'par.myTrans = par.OracleConn.BeginTransaction()
                '‘‘par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


                Session.Add("LAVORAZIONE", "1")
            End If


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Pagamenti Morosità aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                ' & "  and  ID_ANAGRAFICA=" & Me.txtIdAnagrafica.Value 
                par.cmd.CommandText = " select * from SISCOM_MI.MOROSITA_LETTERE  " _
                                    & " where ID_MOROSITA=" & vIdMorosita _
                                    & "  and  ID_CONTRATTO=" & Me.txtIdContratto.Value _
                                    & "  and  NUM_LETTERE=1"

                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                Me.txtVisualizza.Value = 2 'SOLO LETTURA
                FrmSolaLettura()

            Else

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Item("LAVORAZIONE") = "0"


                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If

        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


        'CONDUTTORE
        par.cmd.CommandText = " select * from SISCOM_MI.ANAGRAFICA where ID=" & par.IfNull(myReader1("ID_ANAGRAFICA"), 0)
        myReaderT = par.cmd.ExecuteReader()
        If myReaderT.Read Then

            If par.IfNull(myReaderT("RAGIONE_SOCIALE"), "") <> "" Then
                'Conduttore1 = Conduttore1 & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('anagrafica/Inserimento.aspx?DAC=1&ID=" & par.IfNull(myReader1("id"), "-1") & "','Anagrafe','height=500,top=0,left=0,width=500,scroll=no,status=no');" & Chr(34) & ">" & par.IfNull(myReader1("INTESTATARIO"), "") & "</a>, "

                Me.HL_Anagrafica.Attributes.Add("onClick", "javascript:window.open('../Contratti/Anagrafica/Inserimento.aspx?LT=1&DAC=1&ID=" & par.IfNull(myReaderT("ID"), "") & "','Anagrafe','height=500,top=0,left=0,width=500,scroll=no,status=no');")

                Me.lblCognome.Text = "Ragione Saciale"
                Me.txtNome.Text = ""

                Me.txtCognome.Text = UCase(par.IfNull(myReaderT("RAGIONE_SOCIALE"), ""))
            Else

                Me.lblCognome.Text = "Cognome"
                Me.txtNome.Text = "Nome"

                Me.HL_Anagrafica.Attributes.Add("onClick", "javascript:window.open('../Contratti/Anagrafica/Inserimento.aspx?LT=1&DAC=1&ID=" & par.IfNull(myReaderT("ID"), "") & "','Anagrafe','height=500,top=0,left=0,width=500,scroll=no,status=no');")
                Me.txtCognome.Text = UCase(par.IfNull(myReaderT("COGNOME"), ""))
                Me.txtNome.Text = UCase(par.IfNull(myReaderT("NOME"), ""))
            End If
        End If
        myReaderT.Close()


        'IMMOBILE
        par.cmd.CommandText = " select  COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME AS ""COMUNE_DI""," _
                                    & " UNITA_CONTRATTUALE.*,TIPOLOGIA_UNITA_IMMOBILIARI.descrizione as ""TIPO"" " _
                            & " from SEPA.COMUNI_NAZIONI," _
                                 & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI," _
                                 & " SISCOM_MI.UNITA_CONTRATTUALE " _
                            & " where COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE " _
                            & "   and TIPOLOGIA_UNITA_IMMOBILIARI.cod=UNITA_CONTRATTUALE.tipologia " _
                            & "   and ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), 0)

        myReaderT = par.cmd.ExecuteReader()
        If myReaderT.Read Then
            Me.txtCodUnita.Text = par.IfNull(myReaderT("cod_unita_immobiliare"), "")
            Me.txtIndirizzo.Text = par.IfNull(myReaderT("COMUNE_DI"), "") & " (" & par.IfNull(myReaderT("SIGLA"), "") & ") " & "CAP " & par.IfNull(myReaderT("cap"), "") & " " & par.IfNull(myReaderT("indirizzo"), "") & ", " & par.IfNull(myReaderT("civico"), "") '
            Me.txtTipologia.Text = par.IfNull(myReaderT("Tipo"), "")

            Me.HL_Complesso.Attributes.Add("onClick", "javascript:window.open('../Contratti/DatiComplessoEdificio.aspx?COD=" & par.IfNull(myReaderT("cod_unita_immobiliare"), "") & "','Dati','');")
        End If
        myReaderT.Close()


        'CONTRATTO
        par.cmd.CommandText = " select  RAPPORTI_UTENZA.*" _
                    & " from SISCOM_MI.RAPPORTI_UTENZA" _
                    & " where ID=" & par.IfNull(myReader1("ID_CONTRATTO"), 0)

        myReaderT = par.cmd.ExecuteReader()
        If myReaderT.Read Then
            Me.txtCodContratto.Text = par.IfNull(myReaderT("COD_CONTRATTO"), "")
            Me.txtCodGIMI.Text = par.IfNull(myReaderT("COD_CONTRATTO_GIMI"), "")
            Me.txtTipologiaContratto.Text = par.IfNull(myReaderT("COD_TIPOLOGIA_CONTR_LOC"), "")

            Me.HL_Contratto.Attributes.Add("onClick", "javascript:window.open('../Contratti/Contratto.aspx?LT=1&ID=" & par.IfNull(myReaderT("ID"), "") & "','Contratto','height=780,top=0,left=0,width=1160');")
        End If
        myReaderT.Close()


        CaricaDettaglioMAV()
        CaricaBollette()
        CaricaBolletteTutte()

        CaricaSTATO_MAV_MG()
        CaricaSTATO_MAV_MA()


    End Sub





    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If Me.txtModificato.Value <> "111" Then

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            'If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
            '    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '    par.myTrans.Rollback()
            '    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            'End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()


            If Request.QueryString("X") = "1" Then
                Response.Write("<script language='javascript'> { self.close(); }</script>")
            Else

                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If

        Else
            Me.txtModificato.Value = "1"
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        End If

    End Sub

    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click
        If txtModificato.value <> "111" Then
            Session.Add("LAVORAZIONE", "0")


            vIdMorosita = Request.QueryString("ID_MOR")


            'x Ricerca
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

            sTipoRicerca = Request.QueryString("MORA")

            sProvenienza = Request.QueryString("PROV")
            sOrdinamento = Request.QueryString("ORD")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            'If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
            '    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '    par.myTrans.Rollback()
            '    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            'End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()


            If Me.txtindietro.Value = 1 Then
                Response.Write("<script language='javascript'> { self.close(); }</script>")
            Else

                If sProvenienza = "RICERCA_MOROSITA" Then
                    Response.Write("<script>location.replace('Morosita.aspx?ID=" & vIdMorosita _
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
                Else
                    Response.Write("<script>location.replace('RisultatiInquilini.aspx?ID=" & vIdMorosita _
                                                                        & "&TI=" & sValoreTI _
                                                                        & "&CG=" & par.VaroleDaPassare(sValoreCognome) _
                                                                        & "&NM=" & par.VaroleDaPassare(sValoreNome) _
                                                                        & "&ORD=" & sOrdinamento _
                                                            & "');</script>")
                End If

            End If

        Else
            Me.txtModificato.Value = "1"
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        End If

    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click

        Response.Write("<script>window.open('CreaLettereZIP.aspx?IDMOR_LETTERA_1=" & Me.txtIdMorositaLett1.Value & "&IDMOR_LETTERA_2=" & Me.txtIdMorositaLett2.Value & "','MOROSITA', 'height=400,top=0,left=0,width=700');</script>")

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



    Private Function RicavaFile(ByVal sFile As String) As String
        Dim N As Integer

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

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


    'CARICO COMBO TAB_EVENTI_MOROSITA
    Private Sub CaricaStatiEventi()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbStatoMG.Items.Clear()
            Me.cmbStatoMG.Items.Add(New ListItem(" ", -1))

            Me.cmbStatoMA.Items.Clear()
            Me.cmbStatoMA.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select * from SISCOM_MI.TAB_EVENTI_MOROSITA "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbStatoMG.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), ""), par.IfNull(myReader1("FUNZIONE"), -1)))
                Me.cmbStatoMA.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), ""), par.IfNull(myReader1("FUNZIONE"), -1)))
            End While
            myReader1.Close()


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

    End Sub


    'CARICO TAB. MOROSITA INQUILINO GENERALE
    Private Sub CaricaDettaglioMAV()
        Dim FlagConnessione As Boolean
        Dim num_bolletta As String = ""
        Dim importobolletta As Double = 0
        Dim Tot_Bolletta As Double = 0

        Dim DiffImporto As Double = 0
        Dim FlagImportoAggiornatoMG As Boolean = False
        Dim FlagImportoAggiornatoMA As Boolean = False
        Dim TrovataBollettaMAV As Boolean = False

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.txtFUNZIONE_MA.Value = ""
            Me.txtFUNZIONE_MG.Value = ""

            Me.btnStampa.Visible = True
            Me.btnStampa0.Visible = True

            CType(Tab_Morosita_Generale.FindControl("txtImporto_MG"), TextBox).BackColor = Nothing
            CType(Tab_Morosita_Generale.FindControl("txtImporto_MA"), TextBox).BackColor = Nothing

            'DETTAGLI MAV MG e MAV MA
            par.cmd.CommandText = " select * from SISCOM_MI.MOROSITA_LETTERE  " _
                                              & " where ID_MOROSITA=" & vIdMorosita _
                                              & "  and  ID_CONTRATTO=" & Me.txtIdContratto.Value _
                                              & " order by NUM_LETTERE "

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReaderT.Read
                TrovataBollettaMAV = False
                par.cmd.CommandText = " select BOL_BOLLETTE.*,TIPO_BOLLETTE.ACRONIMO " _
                                    & " from SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.TIPO_BOLLETTE " _
                                    & " where ID_MOROSITA=" & vIdMorosita _
                                    & "   and ID_CONTRATTO=" & par.IfNull(myReaderT("ID_CONTRATTO"), 0) _
                                    & "   and rif_bollettino='" & par.IfNull(myReaderT("BOLLETTINO"), "") & "'" _
                                    & "   and ID_TIPO=4" _
                                    & "   and ID_BOLLETTA_RIC is null " _
                                    & "   and SISCOM_MI.BOL_BOLLETTE.ID_TIPO=SISCOM_MI.TIPO_BOLLETTE.ID (+)" _
                                    & " order by BOL_BOLLETTE.data_emissione desc," _
                                             & " BOL_BOLLETTE.ANNO DESC," _
                                             & " BOL_BOLLETTE.N_RATA DESC," _
                                             & " BOL_BOLLETTE.id desc"

                Dim myReaderBOL As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderBOL.Read Then
                    Select Case par.IfNull(myReaderBOL("n_rata"), "")
                        Case "99" 'bolletta manuale
                            num_bolletta = "MA"
                        Case "999" 'bolletta automatica
                            num_bolletta = "AU"
                        Case "99999" 'bolletta di conguaglio
                            num_bolletta = "CO"
                        Case Else
                            num_bolletta = Format(par.IfNull(myReaderBOL("n_rata"), "??"), "00")
                    End Select

                    TrovataBollettaMAV = True
                    importobolletta = 0
                    '2011/11/16 Leggo il DEBITO iniziale e corrente (senza bollo) da MOROSITA LETTERE
                    'par.cmd.CommandText = "select SUM(NVL(IMPORTO,0) - NVL(IMP_PAGATO,0)) from SISCOM_MI.BOL_BOLLETTE_VOCI where ID_BOLLETTA=" & myReaderBOL("ID")
                    'Dim myReaderBOL_V As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderBOL_V.Read Then
                    '    importobolletta = par.IfNull(myReaderBOL_V(0), "0,00")
                    'End If
                    'myReaderBOL_V.Close()


                    Dim STATO As String = ""
                    If par.IfNull(myReaderBOL("FL_ANNULLATA"), "0") <> "0" Then
                        STATO = "ANNULLATA"
                    Else
                        STATO = "VALIDA"
                    End If

                    Tot_Bolletta = 0

                    If par.IfNull(myReaderBOL("NOTE"), "") = "MOROSITA' MA" And par.IfNull(myReaderT("NUM_LETTERE"), 1) = 1 Then
                        'SOLO MAV MA (nel primo riquadro) Tipo MAV (MA di CD)
                        Me.txtContaMAV.Value = 1

                        Me.txtIdBollette2.Value = myReaderBOL("ID")                      'BOL_BOLLETTE.ID
                        Me.txtIdMorositaLett2.Value = par.IfNull(myReaderT("ID"), -1)    'MOROSITA_LETTERE.ID
                        idMorLeft.Value = par.IfNull(myReaderT("ID"), -1)

                        CType(Tab_Morosita_Generale.FindControl("label_Lettera1"), Label).Text = "Dettaglio M.AV. Gestore:"
                        CType(Tab_Morosita_Generale.FindControl("label_Lettera2"), Label).Text = "Dettaglio M.AV. Global Service:"
                        CType(Tab_Morosita_Generale.FindControl("txtTipo_MG"), TextBox).Text = num_bolletta & "/" & STATO
                        CType(Tab_Morosita_Generale.FindControl("txtDataDAL_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_da"), ""))
                        CType(Tab_Morosita_Generale.FindControl("txtDataAL_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_a"), ""))

                        CType(Tab_Morosita_Generale.FindControl("txtDataEmissione_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("data_emissione"), ""))

                        If par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), "")) <> "" Then
                            CType(Tab_Morosita_Generale.FindControl("txtDataScadenza_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), ""))
                        Else
                            CType(Tab_Morosita_Generale.FindControl("txtDataScadenza_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("data_scadenza"), ""))
                        End If

                        CType(Tab_Morosita_Generale.FindControl("txtNote_MG"), TextBox).Text = par.IfNull(myReaderBOL("note"), "")

                        CType(Tab_Morosita_Generale.FindControl("txtDataPagamento_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("DATA_INS_PAGAMENTO"), ""))


                        'lstBollette.Items.Add(New ListItem(par.MiaFormat(num_bolletta, 2) & " " & par.MiaFormat(STATO, 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_da"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_a"), "")), 12) & " " & par.MiaFormat(Format(importobolletta, "0.00"), 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_emissione"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_scadenza"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_pagamento"), "")), 12) & " " & par.MiaFormat(par.IfNull(myReader2("note"), ""), 35) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_i_sollecito"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_ii_sollecito"), "")), 12), myReader2("ID")))

                        par.cmd.CommandText = " select BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE as ""VOCE"" " _
                                        & " from SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA " _
                                        & " where ID_BOLLETTA=" & myReaderBOL("ID") _
                                        & "   and BOL_BOLLETTE_VOCI.ID_VOCE=T_VOCI_BOLLETTA.ID (+)"

                        Dim myReaderBOL_V As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderBOL_V.Read

                            Select Case par.IfNull(myReaderBOL_V("ID_VOCE"), 0)
                                Case "95"
                                    CType(Tab_Morosita_Generale.FindControl("txtBollo_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")
                                Case "407"
                                    CType(Tab_Morosita_Generale.FindControl("txtMAV_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                                Case "628"
                                    CType(Tab_Morosita_Generale.FindControl("txtNotifica_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                                Case "626" '"RIMBORSO SPESE CONDOMINIALI"
                                    CType(Tab_Morosita_Generale.FindControl("txtRimborso_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                            End Select
                            'Tot_Bolletta = Tot_Bolletta + par.IfNull(myReaderBOL_V("IMPORTO"), 0)

                        End While
                        myReaderBOL_V.Close()
                        CType(Tab_Morosita_Generale.FindControl("txtImporto_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderT("IMPORTO"), 0), "", "##,##0.00") 'IsNumFormat(par.IfNull(importobolletta, 0), "", "##,##0.00")
                        CType(Tab_Morosita_Generale.FindControl("txtImportoINIZIALE_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderT("IMPORTO_INIZIALE"), 0), "", "##,##0.00")
                       
                        If par.IfNull(myReaderT("IMPORTO"), 0) > 0 Then
                            DiffImporto = par.IfNull(myReaderT("IMPORTO_INIZIALE"), 0) - par.IfNull(myReaderT("IMPORTO"), 0)
                            If DiffImporto > 0 Then
                                FlagImportoAggiornatoMA = True
                                CType(Tab_Morosita_Generale.FindControl("txtImporto_MG"), TextBox).BackColor = Drawing.Color.Yellow
                            End If
                        End If

                    ElseIf par.IfNull(myReaderBOL("NOTE"), "") = "MOROSITA' MA" And par.IfNull(myReaderT("NUM_LETTERE"), 1) = 2 Then
                        'MAV MG e MA (MA nel secondo riquadro) Tipo MAV (MA di AB )
                        Me.txtContaMAV.Value = 2

                        Me.txtIdBollette2.Value = myReaderBOL("ID")                      'BOL_BOLLETTE.ID
                        Me.txtIdMorositaLett2.Value = par.IfNull(myReaderT("ID"), -1)   'MOROSITA_LETTERE.ID

                        idMorRight.Value = par.IfNull(myReaderT("ID"), -1)

                        CType(Tab_Morosita_Generale.FindControl("label_Lettera1"), Label).Text = "Dettaglio M.AV. Global Service:"
                        CType(Tab_Morosita_Generale.FindControl("label_Lettera2"), Label).Text = "Dettaglio M.AV. Gestore:"

                        CType(Tab_Morosita_Generale.FindControl("txtTipo_MA"), TextBox).Text = num_bolletta & "/" & STATO
                        CType(Tab_Morosita_Generale.FindControl("txtDataDAL_MA"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_da"), ""))
                        CType(Tab_Morosita_Generale.FindControl("txtDataAL_MA"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_a"), ""))

                        CType(Tab_Morosita_Generale.FindControl("txtDataEmissione_MA"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("data_emissione"), ""))

                        If par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), "")) <> "" Then
                            CType(Tab_Morosita_Generale.FindControl("txtDataScadenza_MA"), TextBox).Text = par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), ""))
                        Else
                            CType(Tab_Morosita_Generale.FindControl("txtDataScadenza_MA"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("data_scadenza"), ""))
                        End If


                        CType(Tab_Morosita_Generale.FindControl("txtNote_MA"), TextBox).Text = par.IfNull(myReaderBOL("note"), "")

                        CType(Tab_Morosita_Generale.FindControl("txtDataPagamento_MA"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("DATA_INS_PAGAMENTO"), ""))


                        'lstBollette.Items.Add(New ListItem(par.MiaFormat(num_bolletta, 2) & " " & par.MiaFormat(STATO, 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_da"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_a"), "")), 12) & " " & par.MiaFormat(Format(importobolletta, "0.00"), 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_emissione"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_scadenza"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_pagamento"), "")), 12) & " " & par.MiaFormat(par.IfNull(myReader2("note"), ""), 35) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_i_sollecito"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_ii_sollecito"), "")), 12), myReader2("ID")))

                        par.cmd.CommandText = " select BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE as ""VOCE"" " _
                                        & " from SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA " _
                                        & " where ID_BOLLETTA=" & myReaderBOL("ID") _
                                        & "   and BOL_BOLLETTE_VOCI.ID_VOCE=T_VOCI_BOLLETTA.ID (+)"
                        Dim myReaderBOL_V As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                        While myReaderBOL_V.Read

                            Select Case par.IfNull(myReaderBOL_V("ID_VOCE"), 0)
                                Case "95"
                                    CType(Tab_Morosita_Generale.FindControl("txtBollo_MA"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")
                                Case "407"
                                    CType(Tab_Morosita_Generale.FindControl("txtMAV_MA"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                                Case "628"
                                    CType(Tab_Morosita_Generale.FindControl("txtNotifica_MA"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                                Case "626" '"RIMBORSO SPESE CONDOMINIALI"
                                    CType(Tab_Morosita_Generale.FindControl("txtRimborso_MA"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                            End Select
                            'Tot_Bolletta = Tot_Bolletta + par.IfNull(myReaderBOL_V("IMPORTO"), 0) 

                        End While
                        myReaderBOL_V.Close()
                        CType(Tab_Morosita_Generale.FindControl("txtImporto_MA"), TextBox).Text = IsNumFormat(par.IfNull(myReaderT("IMPORTO"), 0), "", "##,##0.00") 'IsNumFormat(par.IfNull(importobolletta, 0), "", "##,##0.00")
                        CType(Tab_Morosita_Generale.FindControl("txtImportoINIZIALE_MA"), TextBox).Text = IsNumFormat(par.IfNull(myReaderT("IMPORTO_INIZIALE"), 0), "", "##,##0.00")

                        If par.IfNull(myReaderT("IMPORTO"), 0) > 0 Then
                            DiffImporto = par.IfNull(myReaderT("IMPORTO_INIZIALE"), 0) - par.IfNull(myReaderT("IMPORTO"), 0)
                            If DiffImporto > 0 Then
                                FlagImportoAggiornatoMA = True
                                CType(Tab_Morosita_Generale.FindControl("txtImporto_MA"), TextBox).BackColor = Drawing.Color.Yellow
                            End If
                        End If

                    Else
                        'SOLO MAV MG (nel primo riquadro) Tipo MAV (MG di AB e solo MG di EF)
                        Me.txtContaMAV.Value = 1

                        Me.txtIdBollette1.Value = myReaderBOL("ID")                      'BOL_BOLLETTE.ID
                        Me.txtIdMorositaLett1.Value = par.IfNull(myReaderT("ID"), -1)    'MOROSITA_LETTERE.ID
                        idMorLeft.Value = par.IfNull(myReaderT("ID"), -1)
                        CType(Tab_Morosita_Generale.FindControl("label_Lettera1"), Label).Text = "Dettaglio M.AV. Global Service:"
                        CType(Tab_Morosita_Generale.FindControl("label_Lettera2"), Label).Text = "Dettaglio M.AV. Gestore:"

                        CType(Tab_Morosita_Generale.FindControl("txtTipo_MG"), TextBox).Text = num_bolletta & "/" & STATO
                        CType(Tab_Morosita_Generale.FindControl("txtDataDAL_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_da"), ""))
                        CType(Tab_Morosita_Generale.FindControl("txtDataAL_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_a"), ""))

                        CType(Tab_Morosita_Generale.FindControl("txtDataEmissione_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("data_emissione"), ""))

                        If par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), "")) <> "" Then
                            CType(Tab_Morosita_Generale.FindControl("txtDataScadenza_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), ""))
                        Else
                            CType(Tab_Morosita_Generale.FindControl("txtDataScadenza_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("data_scadenza"), ""))
                        End If

                        CType(Tab_Morosita_Generale.FindControl("txtNote_MG"), TextBox).Text = par.IfNull(myReaderBOL("note"), "")

                        CType(Tab_Morosita_Generale.FindControl("txtDataPagamento_MG"), TextBox).Text = par.FormattaData(par.IfNull(myReaderBOL("DATA_INS_PAGAMENTO"), ""))


                        'lstBollette.Items.Add(New ListItem(par.MiaFormat(num_bolletta, 2) & " " & par.MiaFormat(STATO, 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_da"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_a"), "")), 12) & " " & par.MiaFormat(Format(importobolletta, "0.00"), 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_emissione"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_scadenza"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_pagamento"), "")), 12) & " " & par.MiaFormat(par.IfNull(myReader2("note"), ""), 35) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_i_sollecito"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_ii_sollecito"), "")), 12), myReader2("ID")))

                        par.cmd.CommandText = " select BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE as ""VOCE"" " _
                                        & " from SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA " _
                                        & " where ID_BOLLETTA=" & myReaderBOL("ID") _
                                        & "   and BOL_BOLLETTE_VOCI.ID_VOCE=T_VOCI_BOLLETTA.ID (+)"

                        Dim myReaderBOL_V As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderBOL_V.Read

                            Select Case par.IfNull(myReaderBOL_V("ID_VOCE"), 0)
                                Case "95"
                                    CType(Tab_Morosita_Generale.FindControl("txtBollo_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")
                                Case "407"
                                    CType(Tab_Morosita_Generale.FindControl("txtMAV_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                                Case "628"
                                    CType(Tab_Morosita_Generale.FindControl("txtNotifica_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                                Case "626" '"RIMBORSO SPESE CONDOMINIALI"
                                    CType(Tab_Morosita_Generale.FindControl("txtRimborso_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderBOL_V("IMPORTO"), 0), "", "##,##0.00")

                            End Select
                            'Tot_Bolletta = Tot_Bolletta + par.IfNull(myReaderBOL_V("IMPORTO"), 0) 

                        End While
                        myReaderBOL_V.Close()
                        CType(Tab_Morosita_Generale.FindControl("txtImporto_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderT("IMPORTO"), 0), "", "##,##0.00") 'IsNumFormat(par.IfNull(importobolletta, 0), "", "##,##0.00")
                        CType(Tab_Morosita_Generale.FindControl("txtImportoINIZIALE_MG"), TextBox).Text = IsNumFormat(par.IfNull(myReaderT("IMPORTO_INIZIALE"), 0), "", "##,##0.00")

                        If par.IfNull(myReaderT("IMPORTO"), 0) > 0 Then
                            DiffImporto = par.IfNull(myReaderT("IMPORTO_INIZIALE"), 0) - par.IfNull(myReaderT("IMPORTO"), 0)
                            If DiffImporto > 0 Then
                                FlagImportoAggiornatoMG = True
                                CType(Tab_Morosita_Generale.FindControl("txtImporto_MG"), TextBox).BackColor = Drawing.Color.Yellow
                            End If
                        End If

                    End If

                End If

                If TrovataBollettaMAV = False Then

                    If par.IfNull(myReaderT("TIPO_LETTERA"), "") = "CD" And par.IfNull(myReaderT("NUM_LETTERE"), 1) = 1 Then
                        'SOLO MAV MA (nel primo riquadro) Tipo MAV (MA di CD)
                        Me.txtContaMAV.Value = 1

                        Me.txtIdMorositaLett2.Value = par.IfNull(myReaderT("ID"), -1)    'MOROSITA_LETTERE.ID
                        idMorLeft.Value = par.IfNull(myReaderT("ID"), -1)

                        CType(Tab_Morosita_Generale.FindControl("label_Lettera1"), Label).Text = "Dettaglio M.AV. Gestore:"
                        CType(Tab_Morosita_Generale.FindControl("label_Lettera2"), Label).Text = "Dettaglio M.AV. Global Service:"

                        CType(Tab_Morosita_Generale.FindControl("txtNote_MG"), TextBox).Text = "MAV ERRATO (non creato)"

                        Me.cmbStatoMA.SelectedValue = 94
                        Me.cmbStatoMA.ToolTip = "MAV ERRATO"

                        Me.txtFUNZIONE_MA.Value = "94_MA"

                    ElseIf par.IfNull(myReaderT("TIPO_LETTERA"), "") = "AB" And par.IfNull(myReaderT("NUM_LETTERE"), 1) = 2 Then
                        'MAV MG e MA (MA nel secondo riquadro) Tipo MAV (MA di AB )
                        Me.txtContaMAV.Value = 2

                        Me.txtIdMorositaLett2.Value = par.IfNull(myReaderT("ID"), -1)   'MOROSITA_LETTERE.ID
                        idMorRight.Value = par.IfNull(myReaderT("ID"), -1)

                        CType(Tab_Morosita_Generale.FindControl("label_Lettera1"), Label).Text = "Dettaglio M.AV. Global Service:"
                        CType(Tab_Morosita_Generale.FindControl("label_Lettera2"), Label).Text = "Dettaglio M.AV. Gestore:"


                        CType(Tab_Morosita_Generale.FindControl("txtNote_MA"), TextBox).Text = "MAV ERRATO (non creato)"

                        Me.cmbStatoMA.SelectedValue = 94
                        Me.cmbStatoMA.ToolTip = "MAV ERRATO"

                        Me.txtFUNZIONE_MA.Value = "94_MA"
                    Else

                        'SOLO MAV MG (nel primo riquadro) Tipo MAV (MG di AB e solo MG di EF)
                        Me.txtContaMAV.Value = 1

                        Me.txtIdMorositaLett1.Value = par.IfNull(myReaderT("ID"), -1)    'MOROSITA_LETTERE.ID
                        idMorLeft.Value = par.IfNull(myReaderT("ID"), -1)

                        CType(Tab_Morosita_Generale.FindControl("label_Lettera1"), Label).Text = "Dettaglio M.AV. Global Service:"
                        CType(Tab_Morosita_Generale.FindControl("label_Lettera2"), Label).Text = "Dettaglio M.AV. Gestore:"


                        CType(Tab_Morosita_Generale.FindControl("txtNote_MG"), TextBox).Text = "MAV ERRATO (non creato)"

                        Me.cmbStatoMG.SelectedValue = 94
                        Me.cmbStatoMG.ToolTip = "MAV ERRATO"

                        Me.txtFUNZIONE_MG.Value = "94_MG"
                    End If
                End If

                myReaderBOL.Close()

            End While
            myReaderT.Close()



            If Me.txtContaMAV.Value = 1 Then
                If par.IfEmpty(Me.txtIdMorositaLett2.Value, -1) <> -1 Then
                    'STATO MA (solo)
                    par.cmd.CommandText = " select SISCOM_MI.MOROSITA_EVENTI.*,SISCOM_MI.TAB_EVENTI_MOROSITA.FUNZIONE as ""FUNZIONE1"",SISCOM_MI.TAB_EVENTI_MOROSITA.DESCRIZIONE as ""DESC_STATO"" " _
                                       & " from SISCOM_MI.MOROSITA_EVENTI,SISCOM_MI.TAB_EVENTI_MOROSITA " _
                                       & " where ID_MOROSITA_LETTERE=" & Me.txtIdMorositaLett2.Value _
                                       & "   and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
                                       & " order by ID DESC"

                    myReaderT = par.cmd.ExecuteReader()
                    If myReaderT.Read Then

                        If Me.txtFUNZIONE_MA.Value <> "94_MA" Then
                            Me.txtFUNZIONE_MA.Value = par.IfNull(myReaderT("FUNZIONE1"), "-1")

                            Me.cmbStatoMA.SelectedValue = par.IfNull(myReaderT("FUNZIONE1"), "-1")
                            Me.cmbStatoMA.ToolTip = par.IfNull(myReaderT("DESC_STATO"), "")
                        End If

                        If par.IfNull(myReaderT("FUNZIONE1"), "-1") = 98 Or par.IfNull(myReaderT("FUNZIONE1"), "-1") = 94 Then
                            btnStampa.Visible = False
                            btnStampa0.Visible = False
                        End If

                        If Me.txtFUNZIONE_MA.Value = "94_MA" Then
                            btnStampa.Visible = False
                            btnStampa0.Visible = False
                        End If

                        If FlagImportoAggiornatoMA = True And par.IfNull(myReaderT("FUNZIONE1"), "-1") <> 96 Then
                            ' SE 
                            ' l'inquilino o il comune paga parte o tutto di uno o più bollettini che compongono il MAV
                            ' (perchè qualcuno del comune ha trovato dei bollettini o nel frattempo l'inquilino ha pagato una bolletta alla banca)
                            ' allora setto lo stato a DEBITO VARIATO .
                            ' L'evento DEBITO VARIATO (97) non è settato da nessuno, altrimenti apparirerebbero tanti di questi eventi per quanti sono le bollette del MAV 
                            Me.cmbStatoMA.SelectedValue = 97
                            Me.cmbStatoMA.ToolTip = "DEBITO VARIATO"

                            Me.txtFUNZIONE_MA.Value = "97_MA"
                        End If

                    End If
                    myReaderT.Close()

                ElseIf par.IfEmpty(Me.txtIdMorositaLett1.Value, -1) <> -1 Then
                    'STATO MG (solo)
                    par.cmd.CommandText = " select SISCOM_MI.MOROSITA_EVENTI.*,SISCOM_MI.TAB_EVENTI_MOROSITA.FUNZIONE as ""FUNZIONE1"",SISCOM_MI.TAB_EVENTI_MOROSITA.DESCRIZIONE as ""DESC_STATO"" " _
                                       & " from SISCOM_MI.MOROSITA_EVENTI,SISCOM_MI.TAB_EVENTI_MOROSITA " _
                                       & " where ID_MOROSITA_LETTERE=" & Me.txtIdMorositaLett1.Value _
                                       & "   and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
                                       & " order by ID DESC"

                    myReaderT = par.cmd.ExecuteReader()
                    If myReaderT.Read Then

                        If Me.txtFUNZIONE_MG.Value <> "94_MG" Then
                            Me.txtFUNZIONE_MG.Value = par.IfNull(myReaderT("FUNZIONE1"), "-1")

                            Me.cmbStatoMG.SelectedValue = par.IfNull(myReaderT("FUNZIONE1"), "-1")
                            Me.cmbStatoMG.ToolTip = par.IfNull(myReaderT("DESC_STATO"), "")

                        End If

                        If par.IfNull(myReaderT("FUNZIONE1"), "-1") = 98 Or par.IfNull(myReaderT("FUNZIONE1"), "-1") = 94 Then
                            'Se MOROSITA ANNULLATA (98)
                            btnStampa.Visible = False
                            btnStampa0.Visible = False
                        End If

                        If Me.txtFUNZIONE_MG.Value = "94_MG" Then
                            btnStampa.Visible = False
                            btnStampa0.Visible = False
                        End If

                        If FlagImportoAggiornatoMG = True And par.IfNull(myReaderT("FUNZIONE1"), "-1") <> 96 Then
                            ' SE 
                            ' l'inquilino o il comune paga parte o tutto di uno o più bollettini che compongono il MAV
                            ' (perchè qualcuno del comune ha trovato dei bollettini o nel frattempo l'inquilino ha pagato una bolletta alla banca)
                            ' allora setto lo stato a DEBITO VARIATO .
                            ' L'evento DEBITO VARIATO (97) non è settato da nessuno, altrimenti apparirerebbero tanti di questi eventi per quanti sono le bollette del MAV 
                            Me.cmbStatoMG.SelectedValue = 97
                            Me.cmbStatoMG.ToolTip = "DEBITO VARIATO"

                            Me.txtFUNZIONE_MG.Value = "97_MG"
                        End If

                    End If
                    myReaderT.Close()
                    End If

            ElseIf Me.txtContaMAV.Value = 2 Then

                If par.IfEmpty(Me.txtIdMorositaLett1.Value, -1) <> -1 Then
                    'STATO MG
                    par.cmd.CommandText = " select SISCOM_MI.MOROSITA_EVENTI.*,SISCOM_MI.TAB_EVENTI_MOROSITA.FUNZIONE as ""FUNZIONE1"",SISCOM_MI.TAB_EVENTI_MOROSITA.DESCRIZIONE as ""DESC_STATO"" " _
                                       & " from SISCOM_MI.MOROSITA_EVENTI,SISCOM_MI.TAB_EVENTI_MOROSITA " _
                                       & " where ID_MOROSITA_LETTERE=" & Me.txtIdMorositaLett1.Value _
                                       & "   and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
                                       & " order by ID DESC"

                    myReaderT = par.cmd.ExecuteReader()
                    If myReaderT.Read Then

                        If Me.txtFUNZIONE_MG.Value <> "94_MG" Then
                            Me.txtFUNZIONE_MG.Value = par.IfNull(myReaderT("FUNZIONE1"), "-1")

                            Me.cmbStatoMG.SelectedValue = par.IfNull(myReaderT("FUNZIONE1"), "-1")
                            Me.cmbStatoMG.ToolTip = par.IfNull(myReaderT("DESC_STATO"), "")

                        End If

                        If par.IfNull(myReaderT("FUNZIONE1"), "-1") = 98 Or par.IfNull(myReaderT("FUNZIONE1"), "-1") = 94 Then
                            'Se MOROSITA ANNULLATA (98)
                            btnStampa.Visible = False
                            btnStampa0.Visible = False
                        End If

                        If Me.txtFUNZIONE_MG.Value = "94_MG" Then
                            btnStampa.Visible = False
                            btnStampa0.Visible = False
                        End If

                        If FlagImportoAggiornatoMG = True And par.IfNull(myReaderT("FUNZIONE1"), "-1") <> 96 Then
                            ' SE 
                            ' l'inquilino o il comune paga parte o tutto di uno o più bollettini che compongono il MAV
                            ' (perchè qualcuno del comune ha trovato dei bollettini o nel frattempo l'inquilino ha pagato una bolletta alla banca)
                            ' allora setto lo stato a DEBITO VARIATO .
                            ' L'evento DEBITO VARIATO (97) non è settato da nessuno, altrimenti apparirerebbero tanti di questi eventi per quanti sono le bollette del MAV 
                            Me.cmbStatoMG.SelectedValue = 97
                            Me.cmbStatoMG.ToolTip = "DEBITO VARIATO"

                            Me.txtFUNZIONE_MG.Value = "97_MG"
                        End If
                    End If
                    myReaderT.Close()
                End If

                'STATO MA
                If par.IfEmpty(Me.txtIdMorositaLett2.Value, -1) <> -1 Then
                    par.cmd.CommandText = " select SISCOM_MI.MOROSITA_EVENTI.*,SISCOM_MI.TAB_EVENTI_MOROSITA.FUNZIONE as ""FUNZIONE1"",SISCOM_MI.TAB_EVENTI_MOROSITA.DESCRIZIONE as ""DESC_STATO"" " _
                                       & " from SISCOM_MI.MOROSITA_EVENTI,SISCOM_MI.TAB_EVENTI_MOROSITA " _
                                       & " where ID_MOROSITA_LETTERE=" & Me.txtIdMorositaLett2.Value _
                                       & "   and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
                                       & " order by ID DESC"

                    myReaderT = par.cmd.ExecuteReader()
                    If myReaderT.Read Then

                        If Me.txtFUNZIONE_MA.Value <> "94_MA" Then
                            Me.txtFUNZIONE_MA.Value = par.IfNull(myReaderT("FUNZIONE1"), "-1")

                            Me.cmbStatoMA.SelectedValue = par.IfNull(myReaderT("FUNZIONE1"), "-1")
                            Me.cmbStatoMA.ToolTip = par.IfNull(myReaderT("DESC_STATO"), "")

                        End If

                        If Me.txtFUNZIONE_MA.Value = "94_MA" Then
                            btnStampa.Visible = False
                            btnStampa0.Visible = False
                        End If

                        If FlagImportoAggiornatoMA = True And par.IfNull(myReaderT("FUNZIONE1"), "-1") <> 96 Then
                            ' SE PRIMA di ricevere il MAV o dopo che è stato ricevuto ma non ancora intrapesa alcuna procedura, 
                            ' l'inquilino o il comune paga parte o tutto di uno o più bollettini che compongono il MAV
                            ' (perchè qualcuno del comune ha trovato dei bollettini o nel frattempo l'inquilino ha pagato una bolletta alla banca)
                            ' allora setto lo stato a DEBITO VARIATO .
                            ' L'evento DEBITO VARIATO (97) non è settato da nessuno, altrimenti apparirerebbero tanti di questi eventi per quanti sono le bollette del MAV 
                            Me.cmbStatoMA.SelectedValue = 97
                            Me.cmbStatoMA.ToolTip = "DEBITO VARIATO"

                            Me.txtFUNZIONE_MA.Value = "97_MA"
                        End If

                    End If
                    myReaderT.Close()
                End If

                End If

                'If Me.txtContaMAV.Value = 0 Then
                '    par.cmd.Parameters.Clear()
                '    par.cmd.CommandText = " select SISCOM_MI.TAB_EVENTI_MOROSITA.FUNZIONE as ""FUNZIONE1"",SISCOM_MI.TAB_EVENTI_MOROSITA.DESCRIZIONE as ""DESC_STATO"" " _
                '                           & " from SISCOM_MI.TAB_EVENTI_MOROSITA " _
                '                           & " where FUNZIONE=94"

                '    myReaderT = par.cmd.ExecuteReader()
                '    If myReaderT.Read Then
                '        Me.txtFUNZIONE_MA.Value = par.IfNull(myReaderT("FUNZIONE1"), "-1")
                '        Me.txtFUNZIONE_MG.Value = par.IfNull(myReaderT("FUNZIONE1"), "-1")

                '        Me.cmbStatoMA.SelectedValue = par.IfNull(myReaderT("FUNZIONE1"), "-1")
                '        Me.cmbStatoMA.ToolTip = par.IfNull(myReaderT("DESC_STATO"), "")

                '        Me.cmbStatoMG.SelectedValue = par.IfNull(myReaderT("FUNZIONE1"), "-1")
                '        Me.cmbStatoMG.ToolTip = par.IfNull(myReaderT("DESC_STATO"), "")
                '    End If
                '    myReaderT.Close()
                'End If


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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO TAB. MOROSITA BOLLETTE 
    Private Sub CaricaBollette()
        Dim StringaSql As String
        Dim FlagConnessione As Boolean

        Dim vidMorosita As Long
        Dim vIdContratto As Long

        Dim nBollette As String = ""

        Try



            vidMorosita = CType(Me.Page.FindControl("txtIdMorosita"), HiddenField).Value
            'vIdAnagrafica = CType(Me.Page.FindControl("txtIdAnagrafica"), HiddenField).Value
            vIdContratto = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            If par.IfEmpty(CType(Me.Page.FindControl("txtIdBollette1"), HiddenField).Value, 0) <> 0 Then
                nBollette = CType(Me.Page.FindControl("txtIdBollette1"), HiddenField).Value
            End If

            If par.IfEmpty(CType(Me.Page.FindControl("txtIdBollette2"), HiddenField).Value, 0) <> 0 Then
                If nBollette <> "" Then
                    nBollette = nBollette & "," & CType(Me.Page.FindControl("txtIdBollette2"), HiddenField).Value
                Else
                    nBollette = CType(Me.Page.FindControl("txtIdBollette2"), HiddenField).Value
                End If
            End If



            StringaSql = " select BOL_BOLLETTE.ID," _
                                & " CASE WHEN N_RATA = '99'     THEN 'MA'" _
                                    & "  WHEN N_RATA = '999'    THEN 'AU'" _
                                    & "  WHEN N_RATA = '99999'  THEN 'CO'" _
                                    & "  WHEN N_RATA = NULL     THEN '??'" _
                                    & " ELSE LPAD(N_RATA,2,'0') END ||'/'|| DECODE(BOL_BOLLETTE.FL_ANNULLATA,0,'VALIDA',1,'ANNULLATA') as NUMERO_RATA, " _
                                & " to_char(to_date(substr(BOL_BOLLETTE.RIFERIMENTO_DA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as RIFERIMENTO_DA," _
                                & " to_char(to_date(substr(BOL_BOLLETTE.RIFERIMENTO_A,1,8),'YYYYmmdd'),'DD/MM/YYYY') as RIFERIMENTO_A," _
                                & " TRIM(TO_CHAR( (select SUM(IMPORTO) from SISCOM_MI.BOL_BOLLETTE_VOCI where ID_BOLLETTA=SISCOM_MI.BOL_BOLLETTE.ID),'9G999G999G999G999G990D99')) as IMPORTO," _
                                & " to_char(to_date(substr(BOL_BOLLETTE.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE," _
                                & " to_char(to_date(substr(BOL_BOLLETTE.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_SCADENZA," _
                                & " TRIM(TO_CHAR(BOL_BOLLETTE.IMPORTO_PAGATO,'9G999G999G999G999G990D99')) AS PAGAMENTO," _
                                & " to_char(to_date(substr(BOL_BOLLETTE.DATA_INS_PAGAMENTO,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INS_PAGAMENTO,BOL_BOLLETTE.NOTE " _
                      & " from  SISCOM_MI.BOL_BOLLETTE   " _
                      & " where SISCOM_MI.BOL_BOLLETTE.ID_MOROSITA =" & vidMorosita _
                      & "   and SISCOM_MI.BOL_BOLLETTE.ID_CONTRATTO=" & vIdContratto _
                      & "   and SISCOM_MI.BOL_BOLLETTE.ID_BOLLETTA_RIC in (" & nBollette & ")" _
                      & " order by BOL_BOLLETTE.ID desc " 'DATA_EMISSIONE DESC,SISCOM_MI.BOL_BOLLETTE.N_RATA DESC"

            par.cmd.CommandText = StringaSql

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable

            da.Fill(ds)

            CType(Tab_Morosita_Bollette.FindControl("DataGrid1"), DataGrid).DataSource = ds
            CType(Tab_Morosita_Bollette.FindControl("DataGrid1"), DataGrid).DataBind()


            da.Dispose()
            ds.Dispose()

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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO TAB. MOROSITA BOLLETTE TUTTE
    Private Sub CaricaBolletteTutte()
        Dim StringaSql As String
        Dim FlagConnessione As Boolean

        Dim vIdContratto As Long

        Try

            vIdContratto = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            StringaSql = " select BOL_BOLLETTE.ID," _
                                  & " CASE WHEN N_RATA = '99'     THEN 'MA'" _
                                      & "  WHEN N_RATA = '999'    THEN 'AU'" _
                                      & "  WHEN N_RATA = '99999'  THEN 'CO'" _
                                      & "  WHEN N_RATA = NULL     THEN '??'" _
                                      & " ELSE LPAD(N_RATA,2,'0') END ||'/'|| DECODE(BOL_BOLLETTE.FL_ANNULLATA,0,'VALIDA',1,'ANNULLATA') as NUMERO_RATA, " _
                                  & " DECODE(BOL_BOLLETTE.RIFERIMENTO_DA,0, '',to_char(to_date(substr(BOL_BOLLETTE.RIFERIMENTO_DA,1,8),'YYYYmmdd'),'DD/MM/YYYY')) as RIFERIMENTO_DA," _
                                  & " to_char(to_date(substr(BOL_BOLLETTE.RIFERIMENTO_A,1,8),'YYYYmmdd'),'DD/MM/YYYY') as RIFERIMENTO_A," _
                                  & " TRIM(TO_CHAR( (select SUM(IMPORTO) from SISCOM_MI.BOL_BOLLETTE_VOCI where ID_BOLLETTA=SISCOM_MI.BOL_BOLLETTE.ID),'9G999G999G999G999G990D99')) as IMPORTO," _
                                  & " to_char(to_date(substr(BOL_BOLLETTE.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE," _
                                  & " to_char(to_date(substr(BOL_BOLLETTE.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_SCADENZA," _
                                & " TRIM(TO_CHAR(BOL_BOLLETTE.IMPORTO_PAGATO,'9G999G999G999G999G990D99')) AS PAGAMENTO," _
                                & " to_char(to_date(substr(BOL_BOLLETTE.DATA_INS_PAGAMENTO,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INS_PAGAMENTO,BOL_BOLLETTE.NOTE " _
                        & " from  SISCOM_MI.BOL_BOLLETTE   " _
                        & " where " _
                        & "       SISCOM_MI.BOL_BOLLETTE.ID_CONTRATTO=" & vIdContratto _
                        & " order by BOL_BOLLETTE.ID desc " 'DATA_EMISSIONE DESC,SISCOM_MI.BOL_BOLLETTE.N_RATA DESC"

            par.cmd.CommandText = StringaSql

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable

            da.Fill(ds)

            CType(Tab_Morosita_BolletteTUTTE.FindControl("DataGrid1"), DataGrid).DataSource = ds
            CType(Tab_Morosita_BolletteTUTTE.FindControl("DataGrid1"), DataGrid).DataBind()


            da.Dispose()
            ds.Dispose()

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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO TAB. MOROSITA STATO MAV MA
    Private Sub CaricaSTATO_MAV_MA()
        Dim StringaSql As String
        Dim FlagConnessione As Boolean

        Dim vIdContratto As Long

        Try

            vIdMorosita = CType(Me.Page.FindControl("txtIdMorosita"), HiddenField).Value
            vIdContratto = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value


            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            StringaSql = "select MOROSITA_EVENTI.ID," _
                              & " TO_DATE(MOROSITA_EVENTI.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
                              & " trim(TAB_EVENTI_MOROSITA.DESCRIZIONE) as ""STATO"",trim(MOROSITA_EVENTI.MOTIVAZIONE) as MOTIVAZIONE," _
                              & " trim(SEPA.OPERATORI.OPERATORE) as OPERATORE " _
                       & " from  SISCOM_MI.MOROSITA_EVENTI, SISCOM_MI.TAB_EVENTI_MOROSITA,SEPA.OPERATORI " _
                       & " where SISCOM_MI.MOROSITA_EVENTI.ID_MOROSITA_LETTERE in (" _
                                    & " select ID from SISCOM_MI.MOROSITA_LETTERE " _
                                    & " where  ID_MOROSITA=" & vIdMorosita _
                                    & "   and  ID_CONTRATTO=" & vIdContratto _
                                    & "   and  NUM_LETTERE=" & CType(Me.Page.FindControl("txtContaMAV"), HiddenField).Value & ")" _
                         & " and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
                         & " and SISCOM_MI.MOROSITA_EVENTI.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
                      & " order by MOROSITA_EVENTI.ID desc"

            par.cmd.CommandText = StringaSql

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable

            da.Fill(ds)

            CType(Tab_Morosita_STATO_MA.FindControl("DataGrid1"), DataGrid).DataSource = ds
            CType(Tab_Morosita_STATO_MA.FindControl("DataGrid1"), DataGrid).DataBind()


            da.Dispose()
            ds.Dispose()

            If Me.txtContaEventiMA.Value = 0 Then
                Me.txtContaEventiMA.Value = CType(Tab_Morosita_STATO_MA.FindControl("DataGrid1"), DataGrid).Items.Count
            ElseIf Me.txtContaEventiMA.Value <> CType(Tab_Morosita_STATO_MA.FindControl("DataGrid1"), DataGrid).Items.Count Then
                Me.txtContaEventiMA.Value = CType(Tab_Morosita_STATO_MA.FindControl("DataGrid1"), DataGrid).Items.Count
                Me.EVENTO_MODIFICATO.Value = "1"
            End If

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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO TAB. MOROSITA STATO MAV MG
    Private Sub CaricaSTATO_MAV_MG()
        Dim StringaSql As String
        Dim FlagConnessione As Boolean

        Dim vIdContratto As Long

        Try

            vIdMorosita = CType(Me.Page.FindControl("txtIdMorosita"), HiddenField).Value
            vIdContratto = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value


            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            StringaSql = "select MOROSITA_EVENTI.ID," _
                              & " TO_DATE(MOROSITA_EVENTI.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
                              & " trim(TAB_EVENTI_MOROSITA.DESCRIZIONE) as ""STATO"",trim(MOROSITA_EVENTI.MOTIVAZIONE) as MOTIVAZIONE," _
                              & " trim(SEPA.OPERATORI.OPERATORE) as OPERATORE " _
                       & " from  SISCOM_MI.MOROSITA_EVENTI, SISCOM_MI.TAB_EVENTI_MOROSITA,SEPA.OPERATORI " _
                       & " where SISCOM_MI.MOROSITA_EVENTI.ID_MOROSITA_LETTERE in (" _
                                    & " select ID from SISCOM_MI.MOROSITA_LETTERE " _
                                    & " where  ID_MOROSITA=" & vIdMorosita _
                                    & "   and  ID_CONTRATTO=" & vIdContratto _
                                    & "   and  NUM_LETTERE=1)" _
                         & " and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
                         & " and SISCOM_MI.MOROSITA_EVENTI.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
                      & " order by MOROSITA_EVENTI.ID desc"

            par.cmd.CommandText = StringaSql

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable

            da.Fill(ds)

            CType(Tab_Morosita_STATO_MG.FindControl("DataGrid1"), DataGrid).DataSource = ds
            CType(Tab_Morosita_STATO_MG.FindControl("DataGrid1"), DataGrid).DataBind()


            da.Dispose()
            ds.Dispose()

            If Me.txtContaEventiMG.Value = 0 Then
                Me.txtContaEventiMG.Value = CType(Tab_Morosita_STATO_MG.FindControl("DataGrid1"), DataGrid).Items.Count
            ElseIf Me.txtContaEventiMG.Value <> CType(Tab_Morosita_STATO_MG.FindControl("DataGrid1"), DataGrid).Items.Count Then
                Me.txtContaEventiMA.Value = CType(Tab_Morosita_STATO_MG.FindControl("DataGrid1"), DataGrid).Items.Count
                Me.EVENTO_MODIFICATO.Value = "1"
            End If

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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnStampa0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampa0.Click

        ' Response.Write("<script>window.open('CreaLettere3.aspx?ID_BOLLETTE=" & Me.txtIdBollette1.Value & "," & Me.txtIdBollette2.Value & "','MOROSITA', 'height=400,top=0,left=0,width=700');</script>")
        Response.Write("<script>window.open('CreaLettere3.aspx?ID_BOLLETTE=-1&ID_MOROSITA=" & vIdMorosita & "&ID_CONTRATTO=" & Me.txtIdContratto.Value & "','MOROSITA', 'height=400,top=0,left=0,width=700');</script>")

    End Sub


   


End Class

