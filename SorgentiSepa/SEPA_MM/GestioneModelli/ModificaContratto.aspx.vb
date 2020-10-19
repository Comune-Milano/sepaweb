Imports System.IO
Imports System.Xml
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Collections.Generic
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Math


Partial Class GestioneModelli_ModificaContratto
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim conduttore As String = ""
    Dim Locatari As String = ""
    Dim protocollo As String = ""
    Dim Delibera As String = ""
    Dim DataDelibera As String = ""
    Dim DataStipula As String = ""
    Dim Stringa_Sup As String = ""
    Dim direttore As String = ""
    Dim sedelocatore As String = ""
    Dim indirizzoalloggio As String = ""
    Dim locatore As String = ""
    Dim cflocatore As String = ""
    Dim scala As String = ""
    Dim giorniscadenza As String = ""
    Dim interno As String = ""
    Dim DatiCatastali As String = ""
    Dim sezione As String = ""
    Dim foglio As String = ""
    Dim particella As String = ""
    Dim subalterno As String = ""
    Dim categoria As String = ""
    Dim classe As String = ""
    Dim zona As String = ""
    Dim microzona As String = ""
    Dim superficie As String = ""
    Dim datialloggio As String = ""
    Dim datilocatore As String = ""
    Dim rendita As String = ""
    Dim piano As String = ""
    Dim numlocali As String = ""
    Dim numservizi As String = ""
    Dim comunelocatore As String = ""
    Dim provincialocatore As String = ""
    Dim indirizzolocatore As String = ""
    Dim civicolocatore As String = ""
    Dim nominativoconduttore As String = ""
    Dim daticonduttore As String = ""
    Dim decorrenza As String = ""
    Dim decorrenzacontratto As String = ""
    Dim scadenza As String = ""
    Dim canoneiniziale As String = ""
    Dim percistat As String = ""
    Dim durata As String = ""
    Dim annirinnovo As String = ""
    Dim scadenzarinnovo As String = ""
    Dim mesidisdetta As String = ""
    Dim numerorate As String = ""
    Dim anticipo As String = ""
    Dim pertinenze As String = ""
    Dim bollosucontratto As String = ""
    Dim destinazioneuso As String = ""
    Dim destinazione As String = ""
    Dim ragionesociale As String = ""
    Dim intestatariocontratto As String = ""
    Dim intestatariocontratto1 As String = ""
    Dim partitaiva As String = ""
    Dim residenzaconduttore As String = ""
    Dim EstremiConduttore As String = ""

    Dim testorappresentato As String = ""
    Dim testorappresentato1 As String = ""

    Dim denominazioneedificio As String = ""
    Dim derogaart15 As String = ""

    '*****Variabili PEPPE 29/10/2009
    Dim codcontratto As String = ""
    Dim comuneConduttore As String = ""
    Dim provinciaconduttore As String = ""
    Dim datanascitaconduttore As String = ""
    Dim cfconduttore As String = ""
    Dim comunealloggio As String = ""
    Dim viaalloggio As String = ""
    Dim civicoalloggio As String = ""
    Dim conduttoresesso As String = ""
    Dim canoneannuo As String = ""

    Dim capalloggio As String = ""
    Dim codicealloggio As String = ""

    Dim telefonoconduttore As String = ""
    Dim numerocomponenti As String = ""

    Dim TipologiaUnita As String = ""



    'Dim quotapreventiva As Long = 0
    'Dim riscaldamento As Long = 0
    'Dim ascensore As Long = 0
    'Dim servizicomuni As Long = 0
    'Dim spgenerali As Long = 0
    Dim quotapreventiva As String = "0,00"
    Dim riscaldamento As String = "0,00"
    Dim ascensore As String = "0,00"
    Dim servizicomuni As String = "0,00"
    Dim spgenerali As String = "0,00"
    Dim notecontratto As String = ""


    Dim supcommerciale As String = ""
    Dim suplorda As String = ""
    Dim balconiterrazzi As String = ""
    Dim supcantina As String = ""
    Dim commercialeparticomuni As String = ""



    Dim annuoteorico As String = "0,00"
    Dim ColSx As String = ""
    Dim ColDx As String = ""
    Dim CodiceUtente As String = ""
    Dim SuperficiAlloggio As String = ""
    Dim superficie_netta As String = ""
    Dim superficie_catastale As String = ""

    '****FINE VARIABILI PEPPE
    Dim importoRegistrazione As String = "0,00"

    Dim ELENCOCOMPONENTI As String = ""

    Dim RespAmministrativo As String = ""
    Dim LuogoRespAmministrativo As String = ""
    Dim DataRespAmministrativo As String = ""

    Dim Superfici1 As String = "--"
    Dim Superfici2 As String = "--"
    Dim Superfici3 As String = "--"
    Dim Superfici4 As String = "--"
    Dim Superfici5 As String = "--"

    Dim speseunitacondominio As String = ""

    Public Property DDD() As String
        Get
            If Not (ViewState("par_DDD") Is Nothing) Then
                Return CStr(ViewState("par_DDD"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DDD") = value
        End Set

    End Property

    Public Property TESTO_DDD() As String
        Get
            If Not (ViewState("par_TESTO_DDD") Is Nothing) Then
                Return CStr(ViewState("par_TESTO_DDD"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TESTO_DDD") = value
        End Set

    End Property


    Public Property Licenza() As String
        Get
            If Not (ViewState("par_Licenza") Is Nothing) Then
                Return CStr(ViewState("par_Licenza"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Licenza") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If Not IsPostBack Then
            vTipo = Request.QueryString("T")
            vIdContratto = CLng(Request.QueryString("ID"))
            LBLcONTRATTO.Text = Request.QueryString("C")
            lIdConnessione = Request.QueryString("V")
            DDD = Request.QueryString("D")

            Licenza = Session.Item("LicenzaHtmlToPdf")

            ImportoRegistro = PAR.DeCripta(Request.QueryString("U"))
            ImportoRegistro = Format(CDbl(Format(CDbl(ImportoRegistro), "0")), "0.00")
            If ImportoRegistro <> "67,00" Then
                'Beep()
            End If

            Select Case vTipo
                Case "6", "3", "61", "BOX", "NEGOZI", "ERPB", "62", "392ASS", "63", "CONCESSIONE", "COMODATO"
                    S4P31.Visible = False
                    S4P32.Visible = False
                    S4P33.Visible = False
                    S4P34.Visible = False
                    S4P35.Visible = False
                    S4P36.Visible = False
                    S4P37.Visible = False
                    S4P38.Visible = False
                    S4P39.Visible = False
                    S4P40.Visible = False
                    S4P41.Visible = False
                    S4P42.Visible = False
                    S4P43.Visible = False
                    S4P44.Visible = False
                    S4P45.Visible = False
                    S4P46.Visible = False
                    S4P47.Visible = False
                    S4P48.Visible = False
                    S4P49.Visible = False
                    S4P50.Visible = False
                    S4P51.Visible = False
                    S4P52.Visible = False
                    S4P53.Visible = False
                    S4P54.Visible = False
                    S4P55.Visible = False
                    S4P56.Visible = False
                    S4P57.Visible = False
                    S4P58.Visible = False
                    S4P59.Visible = False
                    S4P60.Visible = False

                Case "64"
                    S4P31.Visible = True
                    S4P32.Visible = True
                    S4P33.Visible = True
                    S4P34.Visible = True
                    S4P35.Visible = True
                    S4P36.Visible = True
                    S4P37.Visible = True
                    S4P38.Visible = True
                    S4P39.Visible = False
                    S4P40.Visible = False
                    S4P41.Visible = False
                    S4P42.Visible = False
                    S4P43.Visible = False
                    S4P44.Visible = False
                    S4P45.Visible = False
                    S4P46.Visible = False
                    S4P47.Visible = False
                    S4P48.Visible = False
                    S4P49.Visible = False
                    S4P50.Visible = False
                    S4P51.Visible = False
                    S4P52.Visible = False
                    S4P53.Visible = False
                    S4P54.Visible = False
                    S4P55.Visible = False
                    S4P56.Visible = False
                    S4P57.Visible = False
                    S4P58.Visible = False
                    S4P59.Visible = False
                    S4P60.Visible = False

                Case "65"
                    S4P31.Visible = True
                    S4P32.Visible = True
                    S4P33.Visible = True
                    S4P34.Visible = True
                    S4P35.Visible = True
                    S4P36.Visible = True
                    S4P37.Visible = True
                    S4P38.Visible = True
                    S4P39.Visible = True
                    S4P40.Visible = True
                    S4P41.Visible = True
                    S4P42.Visible = True
                    S4P43.Visible = True
                    S4P44.Visible = False
                    S4P45.Visible = False
                    S4P46.Visible = False
                    S4P47.Visible = False
                    S4P48.Visible = False
                    S4P49.Visible = False
                    S4P50.Visible = False
                    S4P51.Visible = False
                    S4P52.Visible = False
                    S4P53.Visible = False
                    S4P54.Visible = False
                    S4P55.Visible = False
                    S4P56.Visible = False
                    S4P57.Visible = False
                    S4P58.Visible = False
                    S4P59.Visible = False
                    S4P60.Visible = False

            End Select



            FL_BOLLO_ESENTE = "0"
            If Verifica() = False Then
                Select Case vTipo
                    Case "6"
                        CaricaStandard431()
                    Case "61"
                        CaricaStandard431COOP()
                    Case "62"
                        CaricaStandard431POR()
                    Case "63"
                        Carica431Speciali()
                    Case "64"
                        CaricaStandard431ART_15()
                    Case "65"
                        CaricaStand431ART_15C2bis()
                    Case "1", "8", "2", "10"
                        CaricaStandardERP()
                    Case "12"
                        CaricaConvenzionato()
                    Case "ERPB"
                        CaricaStandardERPModerato()
                    Case "3"
                        CaricaStandardUSD()
                    Case "BOX"
                        CaricaStandardBOX()
                    Case "NEGOZI"
                        CaricaStandardNEGOZI()
                    Case "392ASS"
                        CaricaStandard392ASS()
                    Case "CONCESSIONE"
                        CaricaStandardCONCESSIONE()
                    Case "COMODATO"
                        CaricaStandardCOMODATO()
                End Select
            End If

            txtTitoloContratto.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtTitoloSezione1.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtTitoloSezione2.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtTitoloSezione3.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtTitoloSezione4.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtTitoloSezione5.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

            S1P1.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S1P2.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S1P3.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S1P4.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S1P5.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

            S2P1.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S2P2.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S2P3.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S2P4.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S2P5.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

            S3P1.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P2.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P3.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P4.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P5.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P6.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P7.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P8.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P9.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P10.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P11.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P12.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P13.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P14.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P15.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P16.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P17.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P18.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P19.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P20.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P21.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P22.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P23.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P24.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P25.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P26.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P27.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P28.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P29.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S3P30.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

            S4P1.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P2.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P3.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P4.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P5.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P6.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P7.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P8.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P9.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P10.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P11.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P12.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P13.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P14.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P15.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P16.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P17.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P18.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P19.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P20.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P21.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P22.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P23.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P24.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P25.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P26.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P27.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P28.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P29.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S4P30.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            If vTipo = "6" Then
                S4P31.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P32.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P33.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P34.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P35.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P36.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P37.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P38.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P39.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P40.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P41.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P42.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P43.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P44.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P45.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P46.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P47.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P48.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P49.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P50.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P51.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P52.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P53.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P54.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P55.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P56.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P57.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P58.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P59.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                S4P60.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
            S5P1.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S5P2.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S5P3.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S5P4.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            S5P5.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
        End If

        If imgStampa.Visible = False Then
            Me.imgConduttore.Visible = True
            imgAllegatoContratto.Visible = True

            imgAnnullaStampa.Visible = True

            ImgInfo.Visible = True
            txtpreventiva.Enabled = False
            txtriscaldamento.Enabled = False
            txtAscensore.Enabled = False
            txtComuni.Enabled = False
            txtGenerali.Enabled = False

            txtNoteContratto.Enabled = False
        End If
    End Sub

    Private Function Verifica() As Boolean
        Try

            PAR.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            bolloesente = "" '"Imposta Bollo assolta in modo virtuale, Autorizzazione Intendenza Finanza di Milano n. 3/11914/85 Rep. del 17/12/85"

            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.XML_CONTRATTI WHERE ID_CONTRATTO=" & vIdContratto
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                If myReader.Read Then
                    Verifica = True
                    CaricaContratto()
                End If
            Else
                Verifica = False
            End If
            myReader.Close()




            PAR.cmd.CommandText = "SELECT FL_STAMPATO,FL_BOLLO_ASSOLTO,DATA_DECORRENZA_AE,DATA_DECORRENZA FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & vIdContratto
            myReader = PAR.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                If myReader.Read Then
                    If PAR.IfNull(myReader("FL_STAMPATO"), "0") <> "0" Then
                        PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza_registrazione WHERE ID_CONTRATTO=" & vIdContratto
                        Dim myReaderREG As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReaderREG.HasRows = True Then
                            imgSalva.Visible = False
                            imgAnteprima.Visible = False
                            imgStampa.Visible = False
                        End If
                        myReaderREG.Close()
                    End If
                    FL_BOLLO_ESENTE = PAR.IfNull(myReader("FL_BOLLO_ASSOLTO"), "0")
                    If FL_BOLLO_ESENTE = "1" Then
                        bolloesente = "<table style='border: 1px solid #000000; width: 120px;' cellpadding='0' cellspacing='0'><tr><td style='font-family: Arial; font-size: 5pt; text-align: justify;'>Imposta di bollo esente ai sensi dell'art. 17 D.Lgs. n. 460 del 4-12-97;</td></tr></table>"
                    End If
                End If
            Else
                imgSalva.Enabled = False
                imgAnteprima.Enabled = False
                imgStampa.Enabled = False
            End If
            myReader.Close()

            Dim totale As Double = 0

            If Verifica = False Then
                'PAR.cmd.CommandText = "SELECT importo FROM SISCOM_MI.bol_schema where anno=(select min(anno) from siscom_mi.bol_schema bs where bs.id_contratto=bol_schema.id_contratto) and id_voce=300 and ID_contratto=" & vIdContratto
                'Rif. segnalazione 1628/2018
                PAR.cmd.CommandText = "SELECT sum(importo) as importo FROM SISCOM_MI.bol_schema where anno=(select min(anno) from siscom_mi.bol_schema bs where bs.id_contratto=bol_schema.id_contratto) " _
                    & " and id_voce in (select id_t_voce from siscom_mi.t_voci_spesa_bozza where id_voce_padre=300) and ID_contratto=" & vIdContratto
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read Then
                    txtAscensore.Text = Format(PAR.IfNull(myReader1("importo"), "0,00"), "0.00")
                    totale = totale + PAR.IfNull(myReader1("importo"), 0)
                    txtAscensore.Enabled = False
                End If
                myReader1.Close()

                'PAR.cmd.CommandText = "SELECT importo FROM SISCOM_MI.bol_schema where anno=(select min(anno) from siscom_mi.bol_schema bs where bs.id_contratto=bol_schema.id_contratto) and  id_voce=302 and ID_contratto=" & vIdContratto
                PAR.cmd.CommandText = "SELECT sum(importo) as importo FROM SISCOM_MI.bol_schema where anno=(select min(anno) from siscom_mi.bol_schema bs where bs.id_contratto=bol_schema.id_contratto) " _
                    & " and id_voce in (select id_t_voce from siscom_mi.t_voci_spesa_bozza where id_voce_padre=302) and ID_contratto=" & vIdContratto
                Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader12.Read Then
                    txtriscaldamento.Text = Format(PAR.IfNull(myReader12("importo"), "0,00"), "0.00")
                    totale = totale + PAR.IfNull(myReader12("importo"), 0)
                    txtriscaldamento.Enabled = False
                End If
                myReader12.Close()

                'PAR.cmd.CommandText = "SELECT sum(importo) as importo FROM SISCOM_MI.bol_schema where anno=(select min(anno) from siscom_mi.bol_schema bs where bs.id_contratto=bol_schema.id_contratto) and (id_voce=301 or id_voce=303) and ID_contratto=" & vIdContratto
                PAR.cmd.CommandText = "SELECT sum(importo) as importo FROM SISCOM_MI.bol_schema where anno=(select min(anno) from siscom_mi.bol_schema bs where bs.id_contratto=bol_schema.id_contratto) " _
                    & " and id_voce in (select id_t_voce from siscom_mi.t_voci_spesa_bozza where id_voce_padre=301 or id_voce_padre=303) and ID_contratto=" & vIdContratto
                myReader12 = PAR.cmd.ExecuteReader()

                If myReader12.Read Then
                    'txtComuni.Text = Format(PAR.IfNull(myReader12("importo"), "0,00"), "0.00")
                    txtGenerali.Text = Format(PAR.IfNull(myReader12("importo"), "0,00"), "0.00")
                    totale = totale + PAR.IfNull(myReader12("importo"), 0)
                    If PAR.IfNull(myReader12("importo"), "0") <> "0" Then
                        txtGenerali.Enabled = False
                    End If
                End If
                myReader12.Close()

                txtpreventiva.Text = Format(totale, "0.00")

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function


    Private Function CaricaDati()
        Try


            PAR.OracleConn.Open()
            par.SettaCommand(par)

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=18"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                bollosucontratto = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            'PAR.cmd.CommandText = "SELECT * from parameter where id=121"
            'myReader = PAR.cmd.ExecuteReader()
            'If myReader.Read Then
            '    testorappresentato = PAR.IfNull(myReader("valore"), "")
            'End If
            'myReader.Close()

            'PAR.cmd.CommandText = "SELECT * from parameter where id=122"
            'myReader = PAR.cmd.ExecuteReader()
            'If myReader.Read Then
            '    testorappresentato1 = PAR.IfNull(myReader("valore"), "")
            'End If
            'myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=8"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                locatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=10"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                cflocatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=9"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                direttore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=11"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                sedelocatore = PAR.IfNull(myReader("valore"), "")
                comunelocatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=19"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                sedelocatore = sedelocatore & " (" & PAR.IfNull(myReader("valore"), "") & ") "
                provincialocatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=20"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                sedelocatore = sedelocatore & PAR.IfNull(myReader("valore"), "") & " "
                indirizzolocatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=21"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                sedelocatore = sedelocatore & PAR.IfNull(myReader("valore"), "") & " "
                civicolocatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=40"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                RespAmministrativo = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=41"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                LuogoRespAmministrativo = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=42"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                DataRespAmministrativo = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            Dim SUP_UTILE_NETTA As Double = 0
            Dim SUP_BALCONI As Double = 0
            Dim SUP_ESCLUSIVA As Double = 0
            Dim VAL_MILLESIMO As Double = 0
            Dim NUM_VANI As Double = 0
            Dim SUP_VERDE As Double = 0
            Dim STRINGA_SUP As String = ""



            PAR.cmd.CommandText = "SELECT edifici.id_complesso as complesso,edifici.denominazione as denominazioneedificio,rapporti_utenza.id_domanda,rapporti_utenza.cod_contratto,rapporti_utenza.descr_dest_uso,RAPPORTI_UTENZA.DEST_USO,rapporti_utenza.data_stipula,rapporti_utenza.cod_ufficio_reg,RAPPORTI_UTENZA.MESI_DISDETTA,rapporti_utenza.durata_anni,rapporti_utenza.data_decorrenza,rapporti_utenza.data_scadenza,rapporti_utenza.imp_canone_iniziale,rapporti_utenza.imp_deposito_cauz,rapporti_utenza.durata_rinnovo,rapporti_utenza.data_scadenza_rinnovo,rapporti_utenza.data_delibera,rapporti_utenza.delibera,rapporti_utenza.nro_rate,rapporti_utenza.perc_istat,unita_contrattuale.*,comuni_nazioni.cod as ""cod_catastale"",comuni_nazioni.nome as ""comune"",comuni_nazioni.sigla from siscom_MI.rapporti_utenza,comuni_nazioni,siscom_mi.UNITA_CONTRATTuale,SISCOM_MI.edifici where edifici.id=unita_contrattuale.id_edificio and rapporti_utenza.id=unita_contrattuale.id_contratto and comuni_nazioni.cod=unita_contrattuale.cod_comune and unita_contrattuale.id_unita_principale is null and id_CONTRATTO=" & vIdContratto
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                denominazioneedificio = ""
                PAR.cmd.CommandText = "select max(rownum) from SISCOM_MI.edifici where id_complesso=" & PAR.IfNull(myReader("complesso"), -1)
                Dim myReaderXX As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderXX.Read Then
                    If myReaderXX(0) > 1 Then
                        denominazioneedificio = PAR.IfNull(myReader("denominazioneedificio"), "")
                    Else
                        denominazioneedificio = ""
                    End If
                End If
                myReaderXX.Close()


                derogaart15 = ""
                PAR.cmd.CommandText = "select tipo from bandi_graduatoria_DEF where id_domanda=" & PAR.IfNull(myReader("id_domanda"), "-1")
                myReaderXX = PAR.cmd.ExecuteReader()
                If myReaderXX.Read Then
                    If myReaderXX(0) = "2" Then
                        PAR.cmd.CommandText = "select id_tipo from deroghe_art_15 where id_domanda=" & PAR.IfNull(myReader("id_domanda"), "-1")
                        Dim myReaderXX1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReaderXX1.Read Then
                            If myReaderXX1("id_tipo") = "0" Then
                                derogaart15 = "Visto il Provvedimento del Comune di Milano, il contratto avra' la durata di due anni, ai sensi dell'art. 15-c.1-lett.a) R.R. 1/2004."
                            End If
                            If myReaderXX1("id_tipo") = "1" Then
                                derogaart15 = "Il contratto per effetto del Provvedimento del Comune di Milano, emesso ai sensi dell'art.15 -c.1 -lett.b) R.R. 1/2004, avrà la durata di due anni e potrà essere rinnovato solo su disposizione del medesimo Comune."
                            End If
                        End If
                        myReaderXX1.Close()
                    Else
                        derogaart15 = ""
                    End If
                End If
                myReaderXX.Close()

                'bandi_graduatoria_DEF PAR.IfNull(myReader("cod_ufficio_reg"), "")


                If DDD = "1" Then
                    TESTO_DDD = "Trattandosi di assegnazione disposta ai sensi dell’art.23 RR 1/2004, il canone di locazione soggettivo applicato, così come previsto dall’art. 3 c.7 LR 27/07, è stato quantificato secondo quanto previsto dalla LR 431/98."
                Else
                    TESTO_DDD = ""
                End If



                Select Case PAR.IfNull(myReader("dest_uso"), "")
                    Case "R"
                        destinazioneuso = "ABITAZIONE"
                    Case "B"
                        destinazioneuso = "POSTO AUTO"
                    Case "L"
                        destinazioneuso = "LABORATORIO"
                    Case "N"
                        destinazioneuso = "NEGOZIO COMMERCIALE"
                    Case "C"
                        destinazioneuso = "COOPERATIVE"
                    Case "A"
                        destinazioneuso = "ASSOCIAZIONI"
                    Case "Y"
                        destinazioneuso = "COMODATO D'USO GRATUITO"
                End Select
                codcontratto = PAR.IfNull(myReader("cod_contratto"), "--")
                destinazione = PAR.IfNull(myReader("descr_dest_uso"), "--")
                If destinazione <> "" Then destinazione = destinazione

                'DateAdd(DateInterval.Year, PAR.IfNull(myReader("durata_anni"), ""), CDate(CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text)), par.FormattaData(dataProssimoPeriodo))

                UfficioRegistro = PAR.IfNull(myReader("cod_ufficio_reg"), "")
                DataDelibera = PAR.FormattaData(PAR.IfNull(myReader("data_delibera"), ""))
                DataStipula = "<DataStipula>" & PAR.FormattaData(PAR.IfNull(myReader("data_stipula"), "")) & "</DataStipula>"
                Delibera = PAR.IfNull(myReader("delibera"), "")
                decorrenza = "<DataInizioContratto>" & PAR.FormattaData(PAR.IfNull(myReader("data_decorrenza"), "")) & "</DataInizioContratto>"
                decorrenzacontratto = PAR.FormattaData(PAR.IfNull(myReader("data_decorrenza"), ""))
                scadenza = "<DataFineContratto>" & DateAdd(DateInterval.Year, PAR.IfNull(myReader("durata_anni"), 1), CDate(decorrenzacontratto)) & "</DataFineContratto>"
                canoneiniziale = "<Canone><CanoneFisso><ImportoCanoneFisso TipoCanone=" & Chr(34) & "A" & Chr(34) & " ValutaCanoneFisso=" & Chr(34) & "E" & Chr(34) & ">" & PAR.IfNull(myReader("imp_canone_iniziale"), "") & "</ImportoCanoneFisso></CanoneFisso></Canone>"
                percistat = PAR.IfNull(myReader("perC_istat"), "") & "%"
                durata = PAR.IfNull(myReader("durata_anni"), "") & " " & AnniALettere(PAR.IfNull(myReader("durata_anni"), ""))
                annirinnovo = PAR.IfNull(myReader("durata_rinnovo"), "") & " " & AnniALettere(PAR.IfNull(myReader("durata_RINNOVO"), ""))
                scadenzarinnovo = DateAdd(DateInterval.Year, PAR.IfNull(myReader("durata_anni"), 1) + PAR.IfNull(myReader("durata_rinnovo"), 1), CDate(decorrenzacontratto)) 'PAR.FormattaData(PAR.IfNull(myReader("DATA_SCADENZA_RINNOVO"), ""))
                mesidisdetta = PAR.IfNull(myReader("MESI_DISDETTA"), "")
                giorniscadenza = Val(mesidisdetta) * 3
                numerorate = InLettere(PAR.IfNull(myReader("NRO_RATE"), ""))
                anticipo = PAR.IfNull(myReader("imp_deposito_cauz"), "")

                'PEPPE MODIFY
                comunealloggio = PAR.IfNull(myReader("COMUNE"), "")
                viaalloggio = PAR.IfNull(myReader("indirizzo"), "")
                civicoalloggio = PAR.IfNull(myReader("civico"), "")
                capalloggio = PAR.IfNull(myReader("cap"), "-----")
                codicealloggio = PAR.IfNull(myReader("cod_unita_immobiliare"), "")

                PAR.cmd.CommandText = "select TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPO_UNITA from siscom_mi.unita_immobiliari,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI where TIPOLOGIA_UNITA_IMMOBILIARI.cod=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE='" & codicealloggio & "'"
                myReaderXX = PAR.cmd.ExecuteReader()
                If myReaderXX.Read Then
                    TipologiaUnita = PAR.IfNull(myReaderXX("TIPO_UNITA"), "")
                End If
                myReaderXX.Close()

                If denominazioneedificio <> "" Then


                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(CALLE)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(CORSO)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(LARGO)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(PIAZZA)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(PIAZZALE)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(STRADA)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(VIA)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(VIALE)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(VICO)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(VICOLO)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(ALTRO)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(ALZAIA)", "")
                    denominazioneedificio = Replace(UCase(denominazioneedificio), "(RIPA)", "")

                    denominazioneedificio = Replace(UCase(denominazioneedificio), UCase(civicoalloggio), "")

                    Dim AA As String = viaalloggio

                    AA = Replace(UCase(AA), "CALLE", "")
                    AA = Replace(UCase(AA), "CORSO", "")
                    AA = Replace(UCase(AA), "LARGO", "")
                    AA = Replace(UCase(AA), "PIAZZA", "")
                    AA = Replace(UCase(AA), "PIAZZALE", "")
                    AA = Replace(UCase(AA), "STRADA", "")
                    AA = Replace(UCase(AA), "VIA", "")
                    AA = Replace(UCase(AA), "VIALE", "")
                    AA = Replace(UCase(AA), "VICO", "")
                    AA = Replace(UCase(AA), "VICOLO", "")
                    AA = Replace(UCase(AA), "ALTRO", "")
                    AA = Replace(UCase(AA), "ALZAIA", "")
                    AA = Replace(UCase(AA), "RIPA", "")

                    denominazioneedificio = Trim(Replace(UCase(denominazioneedificio), Trim(UCase(AA)), ""))

                End If

                canoneannuo = PAR.IfNull(myReader("imp_canone_iniziale"), "0")

                scala = PAR.IfNull(myReader("scala"), "---")
                If scala = "000" Then scala = "---"

                interno = PAR.IfNull(myReader("interno"), "---")
                sezione = PAR.IfNull(myReader("sezione"), "---")
                foglio = PAR.IfNull(myReader("foglio"), "---")
                particella = PAR.IfNull(myReader("NUMERO"), "---")
                subalterno = PAR.IfNull(myReader("sub"), "---")
                categoria = PAR.IfNull(myReader("cod_categoria_catastale"), "")
                classe = PAR.IfNull(myReader("cod_classe_catastale"), "")
                zona = PAR.IfNull(myReader("zona_censuaria"), "---")
                microzona = PAR.IfNull(myReader("microzona_censuaria"), "")
                superficie = PAR.IfNull(myReader("SUPERFICIE_MQ"), "---")
                rendita = PAR.IfNull(myReader("rendita"), "")

                DatiCatastali = "<Catasto Accatastamento=" & Chr(34) & "N" & Chr(34) _
                & " CodiceComuneCatastale=" & Chr(34) & PAR.IfNull(myReader("cod_catastale"), "") & Chr(34) _
                & " ParteImmobile=" & Chr(34) & "I" & Chr(34) & " TipoCatasto=" & Chr(34) & "U" & Chr(34) & ">" _
                & "<SezioneUrbana>" & Mid(PAR.IfNull(myReader("sezione"), "   "), 1, 3) & "</SezioneUrbana> Foglio <Foglio>" & Mid(PAR.IfNull(myReader("foglio"), "    "), 1, 4) & "</Foglio>, " _
                & "Particella <ParticellaDenominatore>" & Mid(PAR.IfNull(myReader("NUMERO"), "     "), 1, 5) & " </ParticellaDenominatore>Subalterno <Subalterno>" & Mid(PAR.IfNull(myReader("sub"), "    "), 1, 4) _
                & "</Subalterno></Catasto>"

                piano = ""
                PAR.cmd.CommandText = "SELECT descrizione from siscom_mi.tipo_livello_piano where cod='" & PAR.IfNull(myReader("cod_tipo_livello_piano"), "") & "'"
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader123.Read Then
                    piano = PAR.IfNull(myReader123("descrizione"), "")
                End If
                myReader123.Close()

                annuoteorico = PAR.IfNull(myReader("val_locativo_unita"), "0,00")

                If annuoteorico = "0,00" And destinazioneuso = "ABITAZIONE" Then
                    annuoteorico = PAR.CalcolaValoreLocativo(PAR.IfNull(myReader("id_unita"), "-1"))
                    PAR.cmd.CommandText = "update siscom_mi.unita_contrattuale set val_locativo_unita='" & annuoteorico & "' where id_contratto=" & PAR.IfNull(myReader("id_contratto"), "-1") & " and id_unita=" & PAR.IfNull(myReader("id_unita"), "-1")
                    PAR.cmd.ExecuteNonQuery()
                End If

                indirizzoalloggio = PAR.IfNull(myReader("comune"), "") & "(" & PAR.IfNull(myReader("sigla"), "") & ") " & PAR.IfNull(myReader("indirizzo"), "") & " " & PAR.IfNull(myReader("civico"), "")
                datialloggio = "<Immobile><IndirizzoImmobile>" & PAR.IfNull(myReader("indirizzo"), "") & "</IndirizzoImmobile> <CivicoImmobile>" & PAR.IfNull(myReader("civico"), "") & "</CivicoImmobile> Scala " & scala & " Piano " & piano & " Interno " & interno & " cap " & PAR.IfNull(myReader("cap"), "") & " <ComuneImmobile>" & PAR.IfNull(myReader("comune"), "") & "</ComuneImmobile> (<ProvinciaImmobile>" & PAR.IfNull(myReader("sigla"), "") & "</ProvinciaImmobile>) " & DatiCatastali & "</Immobile>"
                DatiCatastali = ""

                datilocatore = "<Locatore SessoLocatore=" & Chr(34) & "S" & Chr(34) & "><DenominazioneLocatore>" & locatore & "</DenominazioneLocatore>, codice fiscale  n. <CodiceFiscaleLocatore>" & cflocatore & "</CodiceFiscaleLocatore>, con sede in <ComuneLocatore>" & comunelocatore & "</ComuneLocatore> (<ProvinciaLocatore>" & provincialocatore & "</ProvinciaLocatore>), <IndirizzoLocatore>" & indirizzolocatore & "</IndirizzoLocatore><CivicoLocatore>" & civicolocatore & "</CivicoLocatore></Locatore>"

                pertinenze = "" '"dotata di cantina e posto auto quali elementi accessori"
                PAR.cmd.CommandText = "select tipologia_unita_immobiliari.descrizione from siscom_mi.unita_immobiliari,siscom_mi.tipologia_unita_immobiliari where tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and unita_immobiliari.id_unita_principale=" & PAR.IfNull(myReader("id_unita"), "-1")
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                Do While myReaderX.Read
                    pertinenze = pertinenze & PAR.IfNull(myReaderX("descrizione"), "") & " , "
                Loop
                myReaderX.Close()
                If pertinenze <> "" Then
                    pertinenze = "dotata di " & Replace(Mid(pertinenze, 1, Len(pertinenze) - 3), ",", "e") & " quali elementi accessori"
                End If

                numlocali = "--"
                numservizi = "--"
                PAR.cmd.CommandText = "SELECT * from alloggi where cod_alloggio='" & PAR.IfNull(myReader("cod_unita_immobiliare"), "") & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.HasRows = True Then
                    If myReader1.Read Then
                        numlocali = PAR.IfNull(myReader1("num_locali"), "--")
                        numservizi = PAR.IfNull(myReader1("num_servizi"), "--")
                    End If
                Else
                    PAR.cmd.CommandText = "SELECT * from siscom_mi.ui_usi_diversi where cod_alloggio='" & PAR.IfNull(myReader("cod_unita_immobiliare"), "") & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReader2.Read Then
                        numlocali = PAR.IfNull(myReader2("num_locali"), "--")
                        numservizi = PAR.IfNull(myReader2("num_servizi"), "--")
                    End If
                    myReader2.Close()
                End If
                myReader1.Close()

                Dim sessoconduttore As String = ""
                Dim cognomeconduttore As String = ""
                Dim nomeconduttore As String = ""
                Dim DenominazioneConduttore As String = ""
                Dim comunenascita As String = ""
                Dim provincianascita As String = ""

                Dim jj As Integer = 0

                Dim testotabella As String = ""
                Dim mio_codice_comune_nascita As String = ""
                Dim rag_s As Boolean = False



                PAR.cmd.CommandText = "SELECT ANAGRAFICA.* from SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI where ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & vIdContratto & " order by soggetti_contrattuali.cod_tipologia_occupante desc"
                myReader123 = PAR.cmd.ExecuteReader()
                Do While myReader123.Read
                    jj = jj + 1
                    If PAR.IfNull(myReader123("RAGIONE_SOCIALE"), "") <> "" Then
                        rag_s = True
                        ELENCOCOMPONENTI = ELENCOCOMPONENTI & "<tr><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">" & PAR.IfNull(myReader123("RAGIONE_SOCIALE"), "") & "</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & "></td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & "></td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">" & PAR.IfNull(myReader123("PARTITA_IVA"), "") & "</td></tr>"
                    Else
                        ELENCOCOMPONENTI = ELENCOCOMPONENTI & "<tr><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">" & PAR.IfNull(myReader123("cognome"), "") & "</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">" & PAR.IfNull(myReader123("nome"), "") & "</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">" & PAR.FormattaData(PAR.IfNull(myReader123("data_nascita"), "")) & "</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">" & PAR.IfNull(myReader123("cod_fiscale"), "") & "</td></tr>"
                    End If


                Loop

                myReader123.Close()
                numerocomponenti = CStr(jj)

                If rag_s = False Then
                    testotabella = "<table cellpadding='0' cellspacing='0' style=" & Chr(34) & "border: 1px solid #000000;width:100%;font-family: Arial; font-size: 10pt;" & Chr(34) & "><tr><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">COGNOME/RAGIONE SOCIALE</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">NOME</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">DATA DI NASCITA</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">COD.FISCALE/P.IVA</td></tr>"
                Else
                    testotabella = "<table cellpadding='0' cellspacing='0' style=" & Chr(34) & "border: 1px solid #000000;width:100%;font-family: Arial; font-size: 10pt;" & Chr(34) & "><tr><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">RAGIONE SOCIALE</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">NOME</td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & "></td><td style=" & Chr(34) & "border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" & Chr(34) & ">P.IVA</td></tr>"
                End If

                ELENCOCOMPONENTI = testotabella & ELENCOCOMPONENTI & "</table>"


                PAR.cmd.CommandText = "SELECT ANAGRAFICA.* from SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI where SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & vIdContratto
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    CodiceUtente = Format(PAR.IfNull(myReader1("id"), ""), "0000000000")
                    'If PAR.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    ragionesociale = ragionesociale & PAR.IfNull(myReader1("ragione_sociale"), "") & " "
                    partitaiva = partitaiva & PAR.IfNull(myReader1("partita_iva"), "") & " "

                    nominativoconduttore = nominativoconduttore & PAR.IfNull(myReader1("COGNOME"), "") & " " & PAR.IfNull(myReader1("NOME"), "") & ","
                    'PEPPE MODIFY
                    cfconduttore = PAR.IfNull(myReader1("COD_FISCALE"), "")
                    telefonoconduttore = PAR.IfNull(myReader1("TELEFONO"), "")
                    datanascitaconduttore = PAR.FormattaData(PAR.IfNull(myReader1("data_nascita"), ""))

                    mio_codice_comune_nascita = PAR.IfNull(myReader1("COD_COMUNE_NASCITA"), "")

                    If mio_codice_comune_nascita <> "" Then
                        If mio_codice_comune_nascita = "-1" Then
                            If Len(cfconduttore) = 16 Then
                                mio_codice_comune_nascita = Mid(cfconduttore, 12, 4)
                            End If
                        End If
                        PAR.cmd.CommandText = "SELECT * from COMUNI_NAZIONI where cod='" & mio_codice_comune_nascita & "'"
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReader3.Read Then
                            comunenascita = PAR.IfNull(myReader3("nome"), "")
                            provincianascita = PAR.IfNull(myReader3("sigla"), "")
                            If provincianascita = "E" Or provincianascita = "C" Then
                                provincianascita = "EE"
                            End If
                            'PEPPE MODIFY
                            comuneConduttore = comunenascita
                            provinciaconduttore = provincianascita
                            conduttoresesso = PAR.IfNull(myReader1("sesso"), "")
                        End If
                        myReader3.Close()
                    End If

                    If PAR.IfEmpty(ragionesociale, "") <> "" Then
                        sessoconduttore = "S"
                        conduttoresesso = "S"
                    Else
                        sessoconduttore = PAR.IfNull(myReader1("sesso"), "")
                    End If

                    If sessoconduttore = "S" Then
                       
                        Select Case vTipo
                            Case "CONCESSIONE"
                                daticonduttore = daticonduttore & "<Conduttore SessoConduttore=" & Chr(34) & sessoconduttore & Chr(34) _
                                       & "><DenominazioneConduttore>" & PAR.IfNull(myReader1("ragione_sociale"), "") _
                                       & "</DenominazioneConduttore>" _
                                       & " con sede in <ComuneConduttore>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") _
                                       & "</ComuneConduttore> (<ProvinciaConduttore>" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") _
                                       & "</ProvinciaConduttore>) " _
                                       & "<IndirizzoConduttore>" & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") _
                                       & "</IndirizzoConduttore> n. " _
                                       & "<CivicoConduttore>" & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</CivicoConduttore>, c.f. " _
                                       & "<CodiceFiscaleConduttore>" & PAR.IfNull(myReader1("partita_iva"), "") & "</CodiceFiscaleConduttore> " _
                                       & " di seguito denominato Conduttore, nella persona di <CognomeRap>" & PAR.IfNull(myReader1("cognome"), "") _
                                       & "</CognomeRap> <NomeRap>" & PAR.IfNull(myReader1("nome"), "") _
                                       & "</NomeRap> nato a <ComuneNascitaRap>" & comunenascita & "</ComuneNascitaRap> (<ProvinciaNacitaRap>" _
                                       & provincianascita & "</ProvinciaNacitaRap>) il <DataNascitaRap>" _
                                       & PAR.FormattaData(PAR.IfNull(myReader1("data_nascita"), "")) & "</DataNascitaRap> Codice Fiscale " _
                                       & "<CodiceFiscaleRap>" & PAR.IfNull(myReader1("cod_fiscale"), "") & "</CodiceFiscaleRap>" _
                                       & ", e domiciliato per la carica in <IndirizzoRap>" & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") & "</IndirizzoRap>" _
                                       & " <CivicoRap>" & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</CivicoRap>" _
                                       & " <ComuneRap>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") & "</ComuneRap>" _
                                       & " (<ProvinciaRap>" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") & "</ProvinciaRap>)</Conduttore> "
                                EstremiConduttore = EstremiConduttore & "<b>" & PAR.IfNull(myReader1("ragione_sociale"), "") _
                                               & "</b> di seguito denominato 'CONCESSIONARIO' con sede in <b>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") _
                                               & " (" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") & ") " _
                                               & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") & " n. " _
                                               & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</b> e C.F. " & PAR.IfNull(myReader1("cod_fiscale"), "--") & " e P.I <b>" & PAR.IfNull(myReader1("partita_iva"), "") _
                                               & "</b> rappresentata dal/la Sig./Sig.ra "

                                EstremiConduttore = EstremiConduttore & " <b>" _
                                                    & PAR.IfNull(myReader1("cognome"), "") & " " & PAR.IfNull(myReader1("nome"), "") _
                                                    & "</b> nato/a a " & comuneConduttore & " Prov. (" & provinciaconduttore & ")  il " & PAR.FormattaData(PAR.IfNull(myReader1("data_nascita"), "")) & ", in qualità di "

                                If PAR.IfNull(myReader1("tipo_r"), "0") = "0" Then
                                    EstremiConduttore = EstremiConduttore & "rappresentante legale "
                                Else
                                    EstremiConduttore = EstremiConduttore & "procuratore, numero della procura <b>" & PAR.IfNull(myReader1("num_procura"), "") & "</b> rilasciata in data <b>" & PAR.FormattaData(PAR.IfNull(myReader1("data_procura"), "")) & "</b>, "
                                End If
                            Case "COMODATO"
                                daticonduttore = daticonduttore & "<Conduttore SessoConduttore=" & Chr(34) & sessoconduttore & Chr(34) _
                                       & "><DenominazioneConduttore>" & PAR.IfNull(myReader1("ragione_sociale"), "") _
                                       & "</DenominazioneConduttore>" _
                                       & " con sede legale in <ComuneConduttore>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") _
                                       & "</ComuneConduttore> (<ProvinciaConduttore>" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") _
                                       & "</ProvinciaConduttore>) " _
                                       & "<IndirizzoConduttore>" & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") _
                                       & "</IndirizzoConduttore> n. " _
                                       & "<CivicoConduttore>" & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</CivicoConduttore>, c.f. " _
                                       & "<CodiceFiscaleConduttore>" & PAR.IfNull(myReader1("partita_iva"), "") & "</CodiceFiscaleConduttore> " _
                                       & " di seguito denominato Richiedente, rappresentata, in qualita' di rappresentante legale, dal Sig./Sig.ra <CognomeRap>" & PAR.IfNull(myReader1("cognome"), "") _
                                       & "</CognomeRap> <NomeRap>" & PAR.IfNull(myReader1("nome"), "") _
                                       & "</NomeRap> Codice Fiscale " _
                                       & "<CodiceFiscaleRap>" & PAR.IfNull(myReader1("cod_fiscale"), "") & "</CodiceFiscaleRap>" _
                                       & "</Conduttore> "

                                EstremiConduttore = EstremiConduttore & "<b>" & PAR.IfNull(myReader1("ragione_sociale"), "") _
                                                & "</b> P.I./C.F. <b>" & PAR.IfNull(myReader1("partita_iva"), "") _
                                                & "</b> con sede legale in <b>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") _
                                                & " (" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") & ") " _
                                                & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") & " n. " _
                                                & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</b> di seguito denominato " & Chr(34) & "Richiedente" & Chr(34) _
                                                & " rappresentata, in qualità di "
                                If PAR.IfNull(myReader1("tipo_r"), "0") = "0" Then
                                    EstremiConduttore = EstremiConduttore & "rappresentante legale, "
                                Else
                                    EstremiConduttore = EstremiConduttore & "procuratore, numero della procura <b>" & PAR.IfNull(myReader1("num_procura"), "") & "</b> rilasciata in data <b>" & PAR.FormattaData(PAR.IfNull(myReader1("data_procura"), "")) & "</b>, "
                                End If

                                EstremiConduttore = EstremiConduttore & "dal Sig./Sig.ra <b>" _
                                                    & PAR.IfNull(myReader1("cognome"), "") & " " & PAR.IfNull(myReader1("nome"), "") _
                                                    & "</b> codice fiscale <b>" & PAR.IfNull(myReader1("cod_fiscale"), "") & "</b>"
                            Case Else
                                daticonduttore = daticonduttore & "<Conduttore SessoConduttore=" & Chr(34) & sessoconduttore & Chr(34) _
                                       & "><DenominazioneConduttore>" & PAR.IfNull(myReader1("ragione_sociale"), "") _
                                       & "</DenominazioneConduttore>" _
                                       & " con sede in <ComuneConduttore>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") _
                                       & "</ComuneConduttore> (<ProvinciaConduttore>" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") _
                                       & "</ProvinciaConduttore>) " _
                                       & "<IndirizzoConduttore>" & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") _
                                       & "</IndirizzoConduttore> n. " _
                                       & "<CivicoConduttore>" & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</CivicoConduttore>, c.f. " _
                                       & "<CodiceFiscaleConduttore>" & PAR.IfNull(myReader1("partita_iva"), "") & "</CodiceFiscaleConduttore> " _
                                       & " di seguito denominato Conduttore, nella persona di <CognomeRap>" & PAR.IfNull(myReader1("cognome"), "") _
                                       & "</CognomeRap> <NomeRap>" & PAR.IfNull(myReader1("nome"), "") _
                                       & "</NomeRap> nato a <ComuneNascitaRap>" & comunenascita & "</ComuneNascitaRap> (<ProvinciaNacitaRap>" _
                                       & provincianascita & "</ProvinciaNacitaRap>) il <DataNascitaRap>" _
                                       & PAR.FormattaData(PAR.IfNull(myReader1("data_nascita"), "")) & "</DataNascitaRap> Codice Fiscale " _
                                       & "<CodiceFiscaleRap>" & PAR.IfNull(myReader1("cod_fiscale"), "") & "</CodiceFiscaleRap>" _
                                       & ", e domiciliato per la carica in <IndirizzoRap>" & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") & "</IndirizzoRap>" _
                                       & " <CivicoRap>" & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</CivicoRap>" _
                                       & " <ComuneRap>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") & "</ComuneRap>" _
                                       & " (<ProvinciaRap>" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") & "</ProvinciaRap>)</Conduttore> "
                                EstremiConduttore = EstremiConduttore & "<b>" & PAR.IfNull(myReader1("ragione_sociale"), "") _
                                                        & "</b> P.I./C.F. <b>" & PAR.IfNull(myReader1("partita_iva"), "") _
                                                        & "</b> con sede legale in <b>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") _
                                                        & " (" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") & ") " _
                                                        & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") & " n. " _
                                                        & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</b> di seguito denominato " & Chr(34) & "Conduttore" & Chr(34) _
                                                        & " rappresentata, in qualità di "
                                If PAR.IfNull(myReader1("tipo_r"), "0") = "0" Then
                                    EstremiConduttore = EstremiConduttore & "rappresentante legale, "
                                Else
                                    EstremiConduttore = EstremiConduttore & "procuratore, numero della procura <b>" & PAR.IfNull(myReader1("num_procura"), "") & "</b> rilasciata in data <b>" & PAR.FormattaData(PAR.IfNull(myReader1("data_procura"), "")) & "</b>, "
                                End If

                                EstremiConduttore = EstremiConduttore & "dal Sig./Sig.ra <b>" _
                                                    & PAR.IfNull(myReader1("cognome"), "") & " " & PAR.IfNull(myReader1("nome"), "") _
                                                    & "</b> codice fiscale <b>" & PAR.IfNull(myReader1("cod_fiscale"), "") & "</b>"
                        End Select
                        
                    Else
                        Dim SSS As String = ""
                        If sessoconduttore = "M" Then
                            SSS = "Il Sig. "
                        Else
                            SSS = "La Sig.ra "
                        End If

                        EstremiConduttore = EstremiConduttore & SSS
                        daticonduttore = daticonduttore & SSS & "<Conduttore SessoConduttore=" & Chr(34) & sessoconduttore & Chr(34) _
                                         & "><CognomeConduttore>" & PAR.IfNull(myReader1("COGNOME"), "") _
                                         & "</CognomeConduttore> <NomeConduttore>" & PAR.IfNull(myReader1("NOME"), "") _
                                         & "</NomeConduttore> nato/a a <ComuneNascitaConduttore>" & comunenascita _
                                         & "</ComuneNascitaConduttore> (<ProvinciaNascitaConduttore>" & provincianascita _
                                         & "</ProvinciaNascitaConduttore>) il <DataNascitaConduttore>" _
                                         & PAR.FormattaData(PAR.IfNull(myReader1("data_nascita"), "")) _
                                         & "</DataNascitaConduttore> e residente in <ComuneConduttore>" _
                                         & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") _
                                         & "</ComuneConduttore> (<ProvinciaConduttore>" _
                                         & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") & "</ProvinciaConduttore>), " _
                                         & "<IndirizzoConduttore>" & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") _
                                         & "</IndirizzoConduttore> n. " _
                                         & "<CivicoConduttore>" & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</CivicoConduttore>, c.f. " _
                                         & "<CodiceFiscaleConduttore>" & PAR.IfNull(myReader1("cod_fiscale"), "") & "</CodiceFiscaleConduttore> " _
                                         & "</Conduttore> di seguito denominato Conduttore"

                        EstremiConduttore = EstremiConduttore & "<b>" & PAR.IfNull(myReader1("COGNOME"), "") & " " & PAR.IfNull(myReader1("NOME"), "") & "</b> nato/a a <b>" & comunenascita & "</b> Prov <b>" & provincianascita & "</b> il <b>" & PAR.FormattaData(PAR.IfNull(myReader1("data_nascita"), "")) & "</b> e residente in <b>" & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "") & "</b> (<b>" & PAR.IfNull(myReader1("PROVINCIA_RESIDENZA"), "") & "</b>) <b>" & PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") & " n. " & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & "</b> codice fiscale <b>" & PAR.IfNull(myReader1("cod_fiscale"), "") & "</b> di seguito denominato " & Chr(34) & "Conduttore" & Chr(34)
                    End If



                    residenzaconduttore = PAR.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "") & ", " & PAR.IfNull(myReader1("CIVICO_RESIDENZA"), "") & " in " & PAR.IfNull(myReader1("COMUNE_RESIDENZA"), "")

                Loop
                myReader1.Close()

                If PAR.IfEmpty(ragionesociale, "") <> "" Then
                    intestatariocontratto = ragionesociale
                    intestatariocontratto1 = "a " & ragionesociale
                Else
                    intestatariocontratto = "il Sig./Sig.ra " & nominativoconduttore
                    intestatariocontratto1 = "al Sig./Sig.ra " & nominativoconduttore
                End If





                PAR.cmd.CommandText = "select identificativi_catastali.* from siscom_mi.identificativi_catastali,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari where identificativi_catastali.id=unita_immobiliari.id_catastale and unita_immobiliari.id=unita_contrattuale.id_unita and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & vIdContratto
                myReader1 = PAR.cmd.ExecuteReader()
                If myReader1.HasRows = True Then
                    If myReader1.Read Then
                        superficie_catastale = PAR.IfNull(myReader1("superficie_catastale"), "0")
                    End If
                End If
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_CONTRATTO=" & vIdContratto & " AND ID_UNITA=" & PAR.IfNull(myReader("id_unita"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    SUP_UTILE_NETTA = SUP_UTILE_NETTA + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE WHERE (COD_TIPOLOGIA='SUSCO' OR COD_TIPOLOGIA='VESCL') AND ID_CONTRATTO=" & vIdContratto & " AND ID_UNITA=" & PAR.IfNull(myReader("id_unita"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    SUP_ESCLUSIVA = SUP_ESCLUSIVA + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE WHERE COD_TIPOLOGIA='BALCONI' AND ID_CONTRATTO=" & vIdContratto & " AND ID_UNITA=" & PAR.IfNull(myReader("id_unita"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    SUP_BALCONI = SUP_BALCONI + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT DIMENSIONI_UNITA_CONTRATTUALE.* FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE,siscom_mi.unita_contrattuale WHERE DIMENSIONI_UNITA_CONTRATTUALE.COD_TIPOLOGIA='SUP_NETTA' AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & vIdContratto & " AND UNITA_CONTRATTUALE.ID_UNITA_principale=" & PAR.IfNull(myReader("id_unita"), -1) & " and DIMENSIONI_UNITA_CONTRATTUALE.id_contratto=UNITA_CONTRATTUALE.id_contratto and DIMENSIONI_UNITA_CONTRATTUALE.id_unita=UNITA_CONTRATTUALE.id_unita"
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    SUP_BALCONI = SUP_BALCONI + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT NUM_VANI FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & vIdContratto & " AND ID_UNITA=" & PAR.IfNull(myReader("id_unita"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    NUM_VANI = NUM_VANI + CDbl(PAR.IfNull(myReader1("NUM_VANI"), 0))
                Loop
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT DIMENSIONI_UNITA_CONTRATTUALE.* FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE,siscom_mi.unita_contrattuale WHERE DIMENSIONI_UNITA_CONTRATTUALE.COD_TIPOLOGIA='SUVEC' AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & vIdContratto & " AND UNITA_CONTRATTUALE.ID_UNITA_principale=" & PAR.IfNull(myReader("id_unita"), -1) & " and DIMENSIONI_UNITA_CONTRATTUALE.id_contratto=UNITA_CONTRATTUALE.id_contratto and DIMENSIONI_UNITA_CONTRATTUALE.id_unita=UNITA_CONTRATTUALE.id_unita"
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    SUP_VERDE = SUP_VERDE + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()

                If SUP_UTILE_NETTA > 0 Then
                    STRINGA_SUP = SUP_UTILE_NETTA & " (Sup. Utile Netta), "
                    superficie_netta = SUP_UTILE_NETTA
                End If

                If SUP_BALCONI > 0 Then
                    STRINGA_SUP = STRINGA_SUP & "mq " & SUP_BALCONI & " (Balconi,Terrezze,Cantine e Simili), "
                End If

                If SUP_ESCLUSIVA > 0 Then
                    STRINGA_SUP = STRINGA_SUP & " mq " & SUP_ESCLUSIVA & " (Sup.scoperta uso escl.), "
                End If

                If SUP_VERDE > 0 Then
                    STRINGA_SUP = STRINGA_SUP & " mq " & SUP_VERDE & " (Sup.Condominiale a verde), "
                End If
                If NUM_VANI > 0 Then
                    STRINGA_SUP = STRINGA_SUP & "num. vani conv. " & NUM_VANI & " "
                End If

                SuperficiAlloggio = SUP_UTILE_NETTA & "/" & SUP_BALCONI & "/" & SUP_ESCLUSIVA & "/" & SUP_VERDE

                Superfici1 = SUP_UTILE_NETTA
                Superfici2 = SUP_BALCONI
                Superfici3 = SUP_ESCLUSIVA
                Superfici4 = SUP_VERDE
                Superfici5 = NUM_VANI

                If Superfici1 = "0" Then Superfici1 = "____"
                If Superfici2 = "0" Then Superfici2 = "____"
                If Superfici3 = "0" Then Superfici3 = "____"
                If Superfici4 = "0" Then Superfici4 = "____"
                If Superfici5 = "0" Then Superfici5 = "____"


                quotapreventiva = txtpreventiva.Text
                riscaldamento = txtriscaldamento.Text
                ascensore = txtAscensore.Text
                servizicomuni = txtComuni.Text
                spgenerali = txtGenerali.Text

                notecontratto = txtNoteContratto.Text

                Dim ss As Double = 0

                PAR.cmd.CommandText = "SELECT DIMENSIONI_UNITA_CONTRATTUALE.* FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE,siscom_mi.unita_contrattuale WHERE DIMENSIONI_UNITA_CONTRATTUALE.COD_TIPOLOGIA='SUP_LORDA' AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & vIdContratto & " AND UNITA_CONTRATTUALE.ID_UNITA=" & PAR.IfNull(myReader("id_unita"), -1) & " and DIMENSIONI_UNITA_CONTRATTUALE.id_contratto=UNITA_CONTRATTUALE.id_contratto and DIMENSIONI_UNITA_CONTRATTUALE.id_unita=UNITA_CONTRATTUALE.id_unita"
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    ss = ss + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()
                If ss > 0 Then
                    suplorda = ss
                End If
                ss = 0

                PAR.cmd.CommandText = "SELECT DIMENSIONI_UNITA_CONTRATTUALE.* FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE,siscom_mi.unita_contrattuale WHERE DIMENSIONI_UNITA_CONTRATTUALE.COD_TIPOLOGIA='SUP_COMM' AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & vIdContratto & " AND UNITA_CONTRATTUALE.ID_UNITA=" & PAR.IfNull(myReader("id_unita"), -1) & " and DIMENSIONI_UNITA_CONTRATTUALE.id_contratto=UNITA_CONTRATTUALE.id_contratto and DIMENSIONI_UNITA_CONTRATTUALE.id_unita=UNITA_CONTRATTUALE.id_unita"
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    ss = ss + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()
                If ss > 0 Then
                    supcommerciale = ss
                End If
                ss = 0

                PAR.cmd.CommandText = "SELECT DIMENSIONI_UNITA_CONTRATTUALE.* FROM SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE,siscom_mi.unita_contrattuale WHERE DIMENSIONI_UNITA_CONTRATTUALE.COD_TIPOLOGIA='BALCONI' AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & vIdContratto & " AND UNITA_CONTRATTUALE.ID_UNITA=" & PAR.IfNull(myReader("id_unita"), -1) & " and DIMENSIONI_UNITA_CONTRATTUALE.id_contratto=UNITA_CONTRATTUALE.id_contratto and DIMENSIONI_UNITA_CONTRATTUALE.id_unita=UNITA_CONTRATTUALE.id_unita"
                myReader1 = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    ss = ss + CDbl(PAR.IfNull(myReader1("VALORE"), 0))
                Loop
                myReader1.Close()
                If ss > 0 Then
                    balconiterrazzi = ss
                End If
                ss = 0

                supcantina = ""
                commercialeparticomuni = ""

                speseunitacondominio = ""
                PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO IN (SELECT UNITA_IMMOBILIARI.ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & vIdContratto & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL)"
                myReader1 = PAR.cmd.ExecuteReader()
                If myReader1.HasRows = True Then
                    speseunitacondominio = "SI COMUNICA CHE GLI ONERI ACCESSORI VERRANNO CORRISPOSTI ALL'AMMINISTRATORE, TRATTANDOSI DI UNO STABILE IN CONDOMINIO."
                End If
                myReader1.Close()

            End If
            myReader.Close()
            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Copia Conduttore - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Function

    Private Function AnniALettere(ByVal anni As String) As String
        AnniALettere = ""

        Select Case anni
            Case "1"
                AnniALettere = "(uno)"
            Case "2"
                AnniALettere = "(due)"
            Case "3"
                AnniALettere = "(tre)"
            Case "4"
                AnniALettere = "(quattro)"
            Case "5"
                AnniALettere = "(cinque)"
            Case "6"
                AnniALettere = "(sei)"
            Case "7"
                AnniALettere = "(sette)"
            Case "8"
                AnniALettere = "(otto)"
            Case "9"
                AnniALettere = "(nove)"
            Case "10"
                AnniALettere = "(dieci)"
            Case "11"
                AnniALettere = "(undici)"
            Case "12"
                AnniALettere = "(dodici)"
            Case "13"
                AnniALettere = "(tredici)"
            Case "14"
                AnniALettere = "(quattordici)"
            Case "15"
                AnniALettere = "(quindici)"
        End Select
    End Function

    Private Function InLettere(ByVal T As String) As String
        InLettere = ""

        Select Case T
            Case "12"
                InLettere = "dodici rate mensili"
            Case "6"
                InLettere = "sei rate bimestrali"
            Case "4"
                InLettere = "quattro rate trimestrali"
            Case "2"
                InLettere = "due rate semestrali"
            Case "1"
                InLettere = "una rata annuale"
        End Select
    End Function

    'Private Function Carica431()
    '    Try
    '        PAR.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)
    '        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans



    '        PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.XML_CONTRATTI WHERE ID_CONTRATTO=" & vIdContratto
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
    '        If myReader.HasRows = True Then
    '            If myReader.Read Then
    '                'importobolletta = PAR.IfNull(myReaderS(0), "0,00")
    '            End If
    '        Else
    '            CaricaStandard431()
    '        End If
    '        myReader.Close()





    '    Catch ex As Exception

    '    End Try
    'End Function


    Private Function Sostituisci(ByVal Testo As String) As String
        Dim TestoDaModificare As String = Testo


        TestoDaModificare = Replace(TestoDaModificare, "$daticatastali$", DatiCatastali)

        TestoDaModificare = Replace(TestoDaModificare, "$tipocanoneapplicato$", TESTO_DDD)

        TestoDaModificare = Replace(TestoDaModificare, "$direttore$", direttore)
        TestoDaModificare = Replace(TestoDaModificare, "$locatore$", locatore)
        TestoDaModificare = Replace(TestoDaModificare, "$cflocatore$", cflocatore)
        TestoDaModificare = Replace(TestoDaModificare, "$sedelocatore$", sedelocatore)

        TestoDaModificare = Replace(TestoDaModificare, "$locatari$", Locatari)
        TestoDaModificare = Replace(TestoDaModificare, "$protocollo$", protocollo)
        TestoDaModificare = Replace(TestoDaModificare, "$delibera$", Delibera)
        TestoDaModificare = Replace(TestoDaModificare, "$datadelibera$", DataDelibera)
        TestoDaModificare = Replace(TestoDaModificare, "$indirizzoalloggio$", indirizzoalloggio)
        TestoDaModificare = Replace(TestoDaModificare, "$scala$", scala)
        TestoDaModificare = Replace(TestoDaModificare, "$interno$", interno)
        TestoDaModificare = Replace(TestoDaModificare, "$sezione$", sezione)
        TestoDaModificare = Replace(TestoDaModificare, "$foglio$", foglio)
        TestoDaModificare = Replace(TestoDaModificare, "$particella$", particella)
        TestoDaModificare = Replace(TestoDaModificare, "$subalterno$", subalterno)
        TestoDaModificare = Replace(TestoDaModificare, "$categoria$", categoria)
        TestoDaModificare = Replace(TestoDaModificare, "$classe$", classe)
        TestoDaModificare = Replace(TestoDaModificare, "$zonafascia$", "Zona urbana omogenea " & zona & " Sub fascia " & microzona)
        TestoDaModificare = Replace(TestoDaModificare, "$suputile$", superficie_netta)
        TestoDaModificare = Replace(TestoDaModificare, "$datialloggio$", datialloggio)
        TestoDaModificare = Replace(TestoDaModificare, "$rendita$", rendita)
        TestoDaModificare = Replace(TestoDaModificare, "$piano$", piano)
        'TestoDaModificare = Replace(TestoDaModificare, "$numerolocali$", numlocali)
        'TestoDaModificare = Replace(TestoDaModificare, "$numeroservizi$", numservizi)
        If numlocali = "0" And numservizi = "0" Then
            TestoDaModificare = Replace(TestoDaModificare, "$numerolocaliservizi$", "")
        Else
            If (numlocali <> "" And numlocali <> "--") Or (numservizi <> "" And numservizi <> "--") Then
                TestoDaModificare = Replace(TestoDaModificare, "$numerolocaliservizi$", "costituita da numero " & numlocali & " locali più " & numservizi & " servizi")
            Else
                TestoDaModificare = Replace(TestoDaModificare, "$numerolocaliservizi$", "")
            End If
        End If
        
        TestoDaModificare = Replace(TestoDaModificare, "$datilocatore$", datilocatore)

        TestoDaModificare = Replace(TestoDaModificare, "$nominativoconduttore$", nominativoconduttore)
        TestoDaModificare = Replace(TestoDaModificare, "$daticonduttore$", daticonduttore)
        TestoDaModificare = Replace(TestoDaModificare, "$datadelibera$", DataDelibera)
        TestoDaModificare = Replace(TestoDaModificare, "$delibera$", Delibera)
        TestoDaModificare = Replace(TestoDaModificare, "$decorrenza$", decorrenza)
        TestoDaModificare = Replace(TestoDaModificare, "$scadenza$", scadenza)
        TestoDaModificare = Replace(TestoDaModificare, "$canoneiniziale$", canoneiniziale)
        TestoDaModificare = Replace(TestoDaModificare, "$percistat$", percistat)
        TestoDaModificare = Replace(TestoDaModificare, "$durata$", durata)
        TestoDaModificare = Replace(TestoDaModificare, "$annirinnovo$", annirinnovo)
        TestoDaModificare = Replace(TestoDaModificare, "$scadenzarinnovo$", scadenzarinnovo)
        TestoDaModificare = Replace(TestoDaModificare, "$mesidisdetta$", mesidisdetta & " mese/i")
        TestoDaModificare = Replace(TestoDaModificare, "$numerorate$", numerorate)
        TestoDaModificare = Replace(TestoDaModificare, "$anticipo$", anticipo)
        TestoDaModificare = Replace(TestoDaModificare, "$depositocauzionale$", anticipo)
        TestoDaModificare = Replace(TestoDaModificare, "$pertinenze$", pertinenze)
        TestoDaModificare = Replace(TestoDaModificare, "$datastipula$", DataStipula)
        TestoDaModificare = Replace(TestoDaModificare, "$dettaglisup$", Stringa_Sup)
        TestoDaModificare = Replace(TestoDaModificare, "$giornidisdetta$", giorniscadenza & " (giorno/i)")
        TestoDaModificare = Replace(TestoDaModificare, "$indirizzolocatore$", indirizzolocatore)
        TestoDaModificare = Replace(TestoDaModificare, "$destinazioneuso$", destinazioneuso)
        TestoDaModificare = Replace(TestoDaModificare, "$zona$", zona)
        'TestoDaModificare = Replace(TestoDaModificare, "$annuoteorico$", Format(annuoteorico, "0.00"))

        TestoDaModificare = Replace(TestoDaModificare, "$riscaldamento$", riscaldamento)
        TestoDaModificare = Replace(TestoDaModificare, "$ascensore$", ascensore)
        TestoDaModificare = Replace(TestoDaModificare, "$servizicomuni$", servizicomuni)
        TestoDaModificare = Replace(TestoDaModificare, "$spgenerali$", spgenerali)
        TestoDaModificare = Replace(TestoDaModificare, "$quotapreventiva$", quotapreventiva)
        TestoDaModificare = Replace(TestoDaModificare, "$totanticipo$", quotapreventiva)

        TestoDaModificare = Replace(TestoDaModificare, "$notecontratto$", notecontratto)

        TestoDaModificare = Replace(TestoDaModificare, "$destinazione$", destinazione)

        TestoDaModificare = Replace(TestoDaModificare, "$numdetermina$", "45/2010")
        TestoDaModificare = Replace(TestoDaModificare, "$numdeterminabando$", "n. 7 del 09/02/2010")




        If DDD = "1" And annuoteorico = "0,00" Then
            annuoteorico = "---"
        End If

        TestoDaModificare = Replace(TestoDaModificare, "$annuoteorico$", annuoteorico)
        TestoDaModificare = Replace(TestoDaModificare, "$canoneannuo$", canoneannuo)



        TestoDaModificare = Replace(TestoDaModificare, "$ragionesociale$", ragionesociale)

        TestoDaModificare = Replace(TestoDaModificare, "$intestatariocontratto$", intestatariocontratto)
        TestoDaModificare = Replace(TestoDaModificare, "$intestatario1$", intestatariocontratto1)


        TestoDaModificare = Replace(TestoDaModificare, "$partitaiva$", partitaiva)
        TestoDaModificare = Replace(TestoDaModificare, "$codutente$", CodiceUtente)
        TestoDaModificare = Replace(TestoDaModificare, "$codcontratto$", codcontratto)
        TestoDaModificare = Replace(TestoDaModificare, "$codfiscale$", cfconduttore)
        TestoDaModificare = Replace(TestoDaModificare, "$residenzaconduttore$", residenzaconduttore)
        TestoDaModificare = Replace(TestoDaModificare, "$estremiconduttore$", EstremiConduttore)


        TestoDaModificare = Replace(TestoDaModificare, "$capalloggio$", capalloggio)
        TestoDaModificare = Replace(TestoDaModificare, "$codicealloggio$", codicealloggio)

        TestoDaModificare = Replace(TestoDaModificare, "$tipologiaunita$", TipologiaUnita)

        TestoDaModificare = Replace(TestoDaModificare, "$comunealloggio$", comunealloggio)
        TestoDaModificare = Replace(TestoDaModificare, "$viaalloggio$", viaalloggio)
        TestoDaModificare = Replace(TestoDaModificare, "$civicoalloggio$", civicoalloggio)
        TestoDaModificare = Replace(TestoDaModificare, "$capalloggio$", capalloggio)

        TestoDaModificare = Replace(TestoDaModificare, "$telefonoconduttore$", telefonoconduttore)

        TestoDaModificare = Replace(TestoDaModificare, "$numerocomponenti$", numerocomponenti)

        TestoDaModificare = Replace(TestoDaModificare, "$elencocomponenti$", ELENCOCOMPONENTI)

        TestoDaModificare = Replace(TestoDaModificare, "$superficie_netta$", superficie_netta)
        TestoDaModificare = Replace(TestoDaModificare, "$superficie_cat$", superficie_catastale)

        TestoDaModificare = Replace(TestoDaModificare, "$supcommerciale$", PAR.IfEmpty(supcommerciale, "0"))
        TestoDaModificare = Replace(TestoDaModificare, "$suplorda$", PAR.IfEmpty(suplorda, "0"))
        TestoDaModificare = Replace(TestoDaModificare, "$balconiterrazzi$", PAR.IfEmpty(balconiterrazzi, "0"))
        TestoDaModificare = Replace(TestoDaModificare, "$supcantina$", PAR.IfEmpty(supcantina, "0"))
        TestoDaModificare = Replace(TestoDaModificare, "$commercialeparticomuni$", PAR.IfEmpty(commercialeparticomuni, "0"))
        TestoDaModificare = Replace(TestoDaModificare, "$denominazioneedificio$", PAR.IfEmpty(denominazioneedificio, "0"))
        TestoDaModificare = Replace(TestoDaModificare, "$destinazione$", destinazione)
        TestoDaModificare = Replace(TestoDaModificare, "$bolloesente$", bolloesente)


        TestoDaModificare = Replace(TestoDaModificare, "$superfici$", SuperficiAlloggio)
        TestoDaModificare = Replace(TestoDaModificare, "$superfici1$", Superfici1)
        TestoDaModificare = Replace(TestoDaModificare, "$superfici2$", Superfici2)
        TestoDaModificare = Replace(TestoDaModificare, "$superfici3$", Superfici3)
        TestoDaModificare = Replace(TestoDaModificare, "$superfici4$", Superfici4)
        TestoDaModificare = Replace(TestoDaModificare, "$superfici5$", Superfici5)

        TestoDaModificare = Replace(TestoDaModificare, "$derogaart15$", derogaart15)

        TestoDaModificare = Replace(TestoDaModificare, "$provinciacomunecollegato$", provincialocatore)
        TestoDaModificare = Replace(TestoDaModificare, "$comunecollegato$", UCase(comunelocatore))
        TestoDaModificare = Replace(TestoDaModificare, "$direttore$", UCase(RespAmministrativo))
        TestoDaModificare = Replace(TestoDaModificare, "$luogodirettore$", UCase(LuogoRespAmministrativo))
        TestoDaModificare = Replace(TestoDaModificare, "$datadirettore$", UCase(DataRespAmministrativo))

        TestoDaModificare = Replace(TestoDaModificare, "$speseunitacondominio$", speseunitacondominio)






        '        $supcommerciale$
        '$rendita$
        '$suplorda$
        '$balconiterrazzi$
        '$supcantina$
        '$commercialeparticomuni$


        Sostituisci = TestoDaModificare

    End Function


    Private Function CaricaStandardBOX()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=5"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=5 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()

            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()


    End Function

    Private Function CaricaStandardCONCESSIONE()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=15"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=15 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()

            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()


    End Function

    Private Function CaricaStandardCOMODATO()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=16"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=16 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()

            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()


    End Function


    Private Function CaricaStandardNEGOZI()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=6"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=6 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()

            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()


    End Function



    Private Function CaricaStandardERP()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=0"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=0 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P31.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P32.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P33.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P34.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P35.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P36.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P37.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P38.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P39.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P40.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P41.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P42.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P43.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P44.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P45.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P46.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P47.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P48.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P49.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P50.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P51.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P52.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P53.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P54.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P55.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P56.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P57.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P58.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P59.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P60.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()


    End Function



    Private Function CaricaConvenzionato()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=12"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=12 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P31.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P32.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P33.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P34.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P35.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P36.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P37.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P38.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P39.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P40.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P41.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P42.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P43.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P44.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P45.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P46.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P47.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P48.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P49.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P50.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P51.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P52.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P53.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P54.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P55.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P56.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P57.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P58.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P59.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P60.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()


    End Function


    Private Function CaricaStandardERPModerato()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=2"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=2 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")

            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()


    End Function

    Private Function CaricaStandard431COOP()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=4"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=4 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()





            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()
    End Function


    Private Function CaricaStandard392ASS()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=8"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=8 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()





            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()
    End Function



    Private Function CaricaStandard431POR()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=7"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=7 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()





            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()
    End Function


    Private Function CaricaStandard431()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=1"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=1 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()





            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()
    End Function


    Private Sub CaricaStandard431ART_15()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=13"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=13 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P31.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P32.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P33.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P34.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P35.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P36.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P37.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P38.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P39.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P40.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P41.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P42.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P43.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P44.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P45.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P46.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P47.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P48.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P49.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P50.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P51.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P52.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P53.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                'S4P54.Text = PAR.IfNull(myReaderB("TESTO"), "")
                'myReaderB.Read()
                'S4P55.Text = PAR.IfNull(myReaderB("TESTO"), "")
                'myReaderB.Read()
                'S4P56.Text = PAR.IfNull(myReaderB("TESTO"), "")
                'myReaderB.Read()
                'S4P57.Text = PAR.IfNull(myReaderB("TESTO"), "")
                'myReaderB.Read()
                'S4P58.Text = PAR.IfNull(myReaderB("TESTO"), "")
                'myReaderB.Read()
                'S4P59.Text = PAR.IfNull(myReaderB("TESTO"), "")
                'myReaderB.Read()
                'S4P60.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()



        End If
        myReaderA.Close()
    End Sub

    Private Sub CaricaStand431ART_15C2bis()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=14"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=14 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()

            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P31.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P32.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P33.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P34.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P35.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P36.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P37.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P38.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P39.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P40.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P41.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P42.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P43.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P44.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P45.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P46.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P47.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P48.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P49.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P50.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P51.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P52.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P53.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P54.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P55.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P56.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P57.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P58.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P59.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P60.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()
    End Sub

    Private Function Carica431Speciali()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=11"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=11 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()





            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()
    End Function


    Private Function CaricaStandardUSD()
        PAR.cmd.CommandText = "select schema_contratti.titolo from siscom_mi.schema_contratti where schema_contratti.id=3"
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select schema_contratti_SEZIONI.ID,schema_contratti_SEZIONI.titolo from siscom_mi.schema_contratti_SEZIONI where schema_contratti_SEZIONI.id_SCHEMA=3 ORDER BY ID ASC"
        myReaderA = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderA("TITOLO"), "")

            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S1P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S1P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S2P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S2P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S3P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S3P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()





            myReaderA.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S4P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P6.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P7.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P8.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P9.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P10.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P11.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P12.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P13.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P14.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P15.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P16.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P17.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P18.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P19.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P20.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P21.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P22.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P23.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P24.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P25.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P26.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P27.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P28.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P29.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S4P30.Text = PAR.IfNull(myReaderB("TESTO"), "")
            End If
            myReaderB.Close()


            myReaderA.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            PAR.cmd.CommandText = "select schema_contratti_PARAGRAFI.ID,schema_contratti_PARAGRAFI.testo from siscom_mi.schema_contratti_paragrafi where schema_contratti_paragrafi.id_sezione=" & PAR.IfNull(myReaderA("id"), "-1") & " order by id asc"
            myReaderB = PAR.cmd.ExecuteReader()
            If myReaderB.Read Then
                S5P1.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P2.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P3.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P4.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
                S5P5.Text = PAR.IfNull(myReaderB("TESTO"), "")
                myReaderB.Read()
            End If
            myReaderB.Close()
        End If
        myReaderA.Close()
    End Function


    Public Property UfficioRegistro() As String
        Get
            If Not (ViewState("par_UfficioRegistro") Is Nothing) Then
                Return CStr(ViewState("par_UfficioRegistro"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_UfficioRegistro") = value
        End Set

    End Property

    Public Property ImportoRegistro() As String
        Get
            If Not (ViewState("par_ImportoRegistro") Is Nothing) Then
                Return CStr(ViewState("par_ImportoRegistro"))
            Else
                Return "0,00"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_ImportoRegistro") = value
        End Set

    End Property


    'Dim bolloesente As String = ""

    Public Property bolloesente() As String
        Get
            If Not (ViewState("par_bolloesente") Is Nothing) Then
                Return CStr(ViewState("par_bolloesente"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_bolloesente") = value
        End Set

    End Property

    Public Property FL_BOLLO_ESENTE() As String
        Get
            If Not (ViewState("par_FL_BOLLO_ESENTE") Is Nothing) Then
                Return CStr(ViewState("par_FL_BOLLO_ESENTE"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_FL_BOLLO_ESENTE") = value
        End Set

    End Property


    Public Property vTipo() As String
        Get
            If Not (ViewState("par_vTipologia") Is Nothing) Then
                Return CStr(ViewState("par_vTipologia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vTipologia") = value
        End Set

    End Property

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

    Public Property vIdContratto() As Long
        Get
            If Not (ViewState("par_vIdContratto") Is Nothing) Then
                Return CLng(ViewState("par_vIdContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdContratto") = value
        End Set

    End Property


    Protected Sub imgAnteprima_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAnteprima.Click
        Try

            CaricaDati()



            Dim percorso As String = ""
            Dim NomeFile As String = "TEST_" & Format(Now, "yyyyMMddHHmmss")
            Dim TestoContratto As String = ""


            TestoContratto = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & " ?>" & vbCrLf _
                          & "<!DOCTYPE FileContratti SYSTEM " & Chr(34) & "FileContratti.dtd" & Chr(34) & ">" & vbCrLf _
                          & "<?xml-stylesheet href=" & Chr(34) & "FileContratti XLS.xsl" & Chr(34) & " type=" & Chr(34) & "text/xsl" & Chr(34) & "?>" & vbCrLf _
                          & "<FileContratti CodiceFiscaleFornitore=" & Chr(34) & "XXXXXXXXXXXXXXXX" & Chr(34) & " CodiceUfficio=" & Chr(34) & "XXX" & Chr(34) & " CodiceFiscaleConto=" & Chr(34) & "XXXXXXXXXXXXXXXX" & Chr(34) & " CodiceAzienda=" & Chr(34) & "00000" & Chr(34) & " CodiceCABSportello=" & Chr(34) & "000000" & Chr(34) & " ProvinciaBanca=" & Chr(34) & "XX" & Chr(34) & " ValutaPrelievo=" & Chr(34) & "E" & Chr(34) & ">" & vbCrLf _
                          & "<Contratto TipoContratto=" & Chr(34) & "S" & Chr(34) & " IdContratto=" & Chr(34) & "0" & Chr(34) & " SoggettoIVA=" & Chr(34) & "N" & Chr(34) & " RegistrazioneEsente=" & Chr(34) & "N" & Chr(34) & " ContrattoAgevolato=" & Chr(34) & "S" & Chr(34) & " OggettoLocazione=" & Chr(34) & "00" & Chr(34) & " TipoPagamento=" & Chr(34) & "P" & Chr(34) & " ImportoBollo=" & Chr(34) & "10,00" & Chr(34) & " ImportoRegistrazione=" & Chr(34) & "10" & Chr(34) & " ImportoSanzioniRegistrazione=" & Chr(34) & Chr(34) & " ImportoInteressi=" & Chr(34) & Chr(34) & " NumeroPagine=" & Chr(34) & "1" & Chr(34) & ">" & vbCrLf


            TestoContratto = TestoContratto & "<TitoloContratto>" & Sostituisci(txtTitoloContratto.Text) & "</TitoloContratto>" & vbCrLf
            'If txtTitoloSezione1.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione1.Text) & "</TitoloSezione>" & vbCrLf
            If S1P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P1.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S1P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P2.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S1P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P3.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S1P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P4.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S1P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P5.Text) & "</Paragrafo>" & vbCrLf
            End If
            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            'If txtTitoloSezione2.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione2.Text) & "</TitoloSezione>" & vbCrLf
            If S2P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P1.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S2P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P2.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S2P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P3.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S2P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P4.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S2P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P5.Text) & "</Paragrafo>" & vbCrLf
            End If

            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            'If txtTitoloSezione3.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione3.Text) & "</TitoloSezione>" & vbCrLf

            If S3P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P1.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P2.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P3.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P4.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P5.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P6.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P6.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P7.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P7.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P8.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P8.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P9.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P9.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P10.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P10.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P11.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P11.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P12.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P12.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P13.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P13.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P14.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P14.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P15.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P15.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P16.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P16.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P17.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P17.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P18.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P18.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P19.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P19.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P20.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P20.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P21.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P21.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P22.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P22.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P23.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P23.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P24.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P24.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P25.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P25.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P26.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P26.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P27.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P27.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P28.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P28.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P29.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P29.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S3P30.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P30.Text) & "</Paragrafo>" & vbCrLf
            End If

            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            'If txtTitoloSezione4.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione4.Text) & "</TitoloSezione>" & vbCrLf

            If S4P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P1.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P2.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P3.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P4.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P5.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P6.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P6.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P7.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P7.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P8.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P8.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P9.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P9.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P10.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P10.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P11.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P11.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P12.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P12.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P13.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P13.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P14.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P14.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P15.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P15.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P16.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P16.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P17.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P17.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P18.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P18.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P19.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P19.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P20.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P20.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P21.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P21.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P22.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P22.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P23.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P23.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P24.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P24.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P25.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P25.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P26.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P26.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P27.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P27.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P28.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P28.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P29.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P29.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S4P30.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P30.Text) & "</Paragrafo>" & vbCrLf
            End If

            If vTipo = "1" Or vTipo = "8" Or vTipo = "2" Or vTipo = "10" Or vTipo = "64" Or vTipo = "65" Then
                If S4P31.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P31.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P32.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P32.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P33.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P33.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P34.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P34.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P35.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P35.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P36.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P36.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P37.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P37.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P38.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P38.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P39.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P39.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P40.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P40.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P41.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P41.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P42.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P42.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P43.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P43.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P44.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P44.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P45.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P45.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P46.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P46.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P47.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P47.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P48.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P48.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P49.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P49.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P50.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P50.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P51.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P51.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P52.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P52.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P53.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P53.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P54.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P54.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P55.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P55.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P56.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P56.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P57.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P57.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P58.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P58.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P59.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P59.Text) & "</Paragrafo>" & vbCrLf
                End If
                If S4P60.Text <> "" Then
                    TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P60.Text) & "</Paragrafo>" & vbCrLf
                End If
            End If

            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            'End If

            ' If txtTitoloSezione5.Text <> "" Then
            TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
            TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione5.Text) & "</TitoloSezione>" & vbCrLf

            If S5P1.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P1.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S5P2.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P2.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S5P3.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P3.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S5P4.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P4.Text) & "</Paragrafo>" & vbCrLf
            End If
            If S5P5.Text <> "" Then
                TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P5.Text) & "</Paragrafo>" & vbCrLf
            End If

            TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
            ' End If

            TestoContratto = TestoContratto & "</Contratto></FileContratti>"

            TestoContratto = caricaRespFiliale(vIdContratto, TestoContratto, 1)
            TestoContratto = Replace(TestoContratto, "IN DATA", "in data")
            TestoContratto = Replace(TestoContratto, " IN ", " in ")
            TestoContratto = Replace(TestoContratto, "AUTO", "auto")

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\") & NomeFile & ".xml", False, System.Text.Encoding.GetEncoding("ISO-8859-1"))
            sr.WriteLine(TestoContratto)
            sr.Close()

            Response.Write("<script>var f;f=window.open('../ALLEGATI/CONTRATTI/StampeContratti/" & NomeFile & ".xml" & "','Contratto','top=0,left=0,scrollbars=yes,width=800,height=600,resizable=yes');f.focus();</script>")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Try
            'Dim SCHEMA As Integer
            'Dim BUONO As Boolean = True

            'PAR.OracleConn.Open()
            'par.SettaCommand(par)


            'PAR.cmd.CommandText = "select * from siscom_mi.RAPPORTI_UTENZA where id=" & vIdContratto
            'Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            'If myReaderZ.Read Then
            '    If PAR.IfNull(myReaderZ("DATA_STIPULA"), "") = "" Then
            '        BUONO = False
            '    End If
            '    If PAR.IfNull(myReaderZ("DATA_SCADENZA"), "") = "" Then
            '        BUONO = False
            '    End If
            '    If PAR.IfNull(myReaderZ("DATA_DECORRENZA"), "") = "" Then
            '        BUONO = False
            '    End If
            '    If PAR.IfNull(myReaderZ("COD_UFFICIO_REG"), "") = "" Then
            '        BUONO = False
            '    End If
            '    If PAR.IfNull(myReaderZ("MESI_DISDETTA"), "") = "" Then
            '        BUONO = False
            '    End If
            'End If
            'myReaderZ.Close()
            'PAR.OracleConn.Close()
            'If BUONO = False Then
            '    Response.Write("<script>alert('Attenzione, non è possibile salvare. Alcuni dati fondamentali non sono stati inseriti nel contratto!');</script>")
            'End If
            'If BUONO = True Then
            PAR.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.XML_CONTRATTI WHERE ID_CONTRATTO=" & vIdContratto
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.HasRows = False Then

                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI (ID_CONTRATTO,TITOLO,S1,S2,S3,S4,S5,NOTE) VALUES (" & vIdContratto & ",'','" & PAR.PulisciStrSql(txtpreventiva.Text) & "','" & PAR.PulisciStrSql(txtriscaldamento.Text) & "','" & PAR.PulisciStrSql(txtAscensore.Text) & "','" & PAR.PulisciStrSql(txtComuni.Text) & "','" & PAR.PulisciStrSql(txtGenerali.Text) & "','" & PAR.PulisciStrSql(txtNoteContratto.Text) & "')"
                PAR.cmd.ExecuteNonQuery()

                'SEZIONI
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_SEZIONI (ID_CONTRATTO,PROGR,TITOLO) VALUES (" & vIdContratto & ",0,'')"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_SEZIONI (ID_CONTRATTO,PROGR,TITOLO) VALUES (" & vIdContratto & ",1,'')"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_SEZIONI (ID_CONTRATTO,PROGR,TITOLO) VALUES (" & vIdContratto & ",2,'')"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_SEZIONI (ID_CONTRATTO,PROGR,TITOLO) VALUES (" & vIdContratto & ",3,'')"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_SEZIONI (ID_CONTRATTO,PROGR,TITOLO) VALUES (" & vIdContratto & ",4,'')"
                PAR.cmd.ExecuteNonQuery()

                '5 PARAGRAFI SEZIONE 1
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (0,0,'" & PAR.PulisciStrSql(S1P1.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (0,2,'" & PAR.PulisciStrSql(S1P2.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (0,4,'" & PAR.PulisciStrSql(S1P3.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (0,6,'" & PAR.PulisciStrSql(S1P4.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (0,8,'" & PAR.PulisciStrSql(S1P5.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()

                '5 PARAGRAFI SEZIONE 2
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (1,10,'" & PAR.PulisciStrSql(S2P1.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (1,12,'" & PAR.PulisciStrSql(S2P2.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (1,14,'" & PAR.PulisciStrSql(S2P3.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (1,16,'" & PAR.PulisciStrSql(S2P4.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (1,18,'" & PAR.PulisciStrSql(S2P5.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()

                '30 PARAGRAFI SEZIONE 3
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,20,'" & PAR.PulisciStrSql(S3P1.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,22,'" & PAR.PulisciStrSql(S3P2.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,24,'" & PAR.PulisciStrSql(S3P3.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,26,'" & PAR.PulisciStrSql(S3P4.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,28,'" & PAR.PulisciStrSql(S3P5.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,30,'" & PAR.PulisciStrSql(S3P6.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,32,'" & PAR.PulisciStrSql(S3P7.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,34,'" & PAR.PulisciStrSql(S3P8.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,36,'" & PAR.PulisciStrSql(S3P9.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,38,'" & PAR.PulisciStrSql(S3P10.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,40,'" & PAR.PulisciStrSql(S3P11.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,42,'" & PAR.PulisciStrSql(S3P12.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,44,'" & PAR.PulisciStrSql(S3P13.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,46,'" & PAR.PulisciStrSql(S3P14.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,48,'" & PAR.PulisciStrSql(S3P15.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,50,'" & PAR.PulisciStrSql(S3P16.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,52,'" & PAR.PulisciStrSql(S3P17.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,54,'" & PAR.PulisciStrSql(S3P18.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,56,'" & PAR.PulisciStrSql(S3P19.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,58,'" & PAR.PulisciStrSql(S3P20.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,60,'" & PAR.PulisciStrSql(S3P21.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,62,'" & PAR.PulisciStrSql(S3P22.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,64,'" & PAR.PulisciStrSql(S3P23.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,66,'" & PAR.PulisciStrSql(S3P24.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,68,'" & PAR.PulisciStrSql(S3P25.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,70,'" & PAR.PulisciStrSql(S3P26.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,72,'" & PAR.PulisciStrSql(S3P27.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,74,'" & PAR.PulisciStrSql(S3P28.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,76,'" & PAR.PulisciStrSql(S3P29.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (2,78,'" & PAR.PulisciStrSql(S3P30.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()


                '60 PARAGRAFI SEZIONE 4
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,80,'" & PAR.PulisciStrSql(S4P1.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,82,'" & PAR.PulisciStrSql(S4P2.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,84,'" & PAR.PulisciStrSql(S4P3.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,86,'" & PAR.PulisciStrSql(S4P4.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,88,'" & PAR.PulisciStrSql(S4P5.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,90,'" & PAR.PulisciStrSql(S4P6.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,92,'" & PAR.PulisciStrSql(S4P7.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,94,'" & PAR.PulisciStrSql(S4P8.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,96,'" & PAR.PulisciStrSql(S4P9.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,98,'" & PAR.PulisciStrSql(S4P10.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,100,'" & PAR.PulisciStrSql(S4P11.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,102,'" & PAR.PulisciStrSql(S4P12.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,104,'" & PAR.PulisciStrSql(S4P13.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,106,'" & PAR.PulisciStrSql(S4P14.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,108,'" & PAR.PulisciStrSql(S4P15.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,110,'" & PAR.PulisciStrSql(S4P16.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,112,'" & PAR.PulisciStrSql(S4P17.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,114,'" & PAR.PulisciStrSql(S4P18.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,116,'" & PAR.PulisciStrSql(S4P19.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,118,'" & PAR.PulisciStrSql(S4P20.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,120,'" & PAR.PulisciStrSql(S4P21.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,122,'" & PAR.PulisciStrSql(S4P22.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,124,'" & PAR.PulisciStrSql(S4P23.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,126,'" & PAR.PulisciStrSql(S4P24.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,128,'" & PAR.PulisciStrSql(S4P25.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,130,'" & PAR.PulisciStrSql(S4P26.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,132,'" & PAR.PulisciStrSql(S4P27.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,134,'" & PAR.PulisciStrSql(S4P28.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,136,'" & PAR.PulisciStrSql(S4P29.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,138,'" & PAR.PulisciStrSql(S4P30.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()

                If vTipo = "1" Or vTipo = "8" Or vTipo = "2" Or vTipo = "10" Or vTipo = "64" Or vTipo = "65" Then
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,140,'" & PAR.PulisciStrSql(S4P31.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,142,'" & PAR.PulisciStrSql(S4P32.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,144,'" & PAR.PulisciStrSql(S4P33.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,146,'" & PAR.PulisciStrSql(S4P34.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,148,'" & PAR.PulisciStrSql(S4P35.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,150,'" & PAR.PulisciStrSql(S4P36.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,152,'" & PAR.PulisciStrSql(S4P37.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,154,'" & PAR.PulisciStrSql(S4P38.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,156,'" & PAR.PulisciStrSql(S4P39.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,158,'" & PAR.PulisciStrSql(S4P40.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,160,'" & PAR.PulisciStrSql(S4P41.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,162,'" & PAR.PulisciStrSql(S4P42.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,164,'" & PAR.PulisciStrSql(S4P43.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,166,'" & PAR.PulisciStrSql(S4P44.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,168,'" & PAR.PulisciStrSql(S4P45.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,170,'" & PAR.PulisciStrSql(S4P46.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,172,'" & PAR.PulisciStrSql(S4P47.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,174,'" & PAR.PulisciStrSql(S4P48.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,176,'" & PAR.PulisciStrSql(S4P49.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,178,'" & PAR.PulisciStrSql(S4P50.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,180,'" & PAR.PulisciStrSql(S4P51.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,182,'" & PAR.PulisciStrSql(S4P52.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,184,'" & PAR.PulisciStrSql(S4P53.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,186,'" & PAR.PulisciStrSql(S4P54.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,188,'" & PAR.PulisciStrSql(S4P55.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,190,'" & PAR.PulisciStrSql(S4P56.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,192,'" & PAR.PulisciStrSql(S4P57.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,194,'" & PAR.PulisciStrSql(S4P58.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,196,'" & PAR.PulisciStrSql(S4P59.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (3,198,'" & PAR.PulisciStrSql(S4P60.Text) & "'," & vIdContratto & ")"
                    PAR.cmd.ExecuteNonQuery()
                End If

                '5 PARAGRAFI SEZIONE 5
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (4,200,'" & PAR.PulisciStrSql(S5P1.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (4,202,'" & PAR.PulisciStrSql(S5P2.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (4,204,'" & PAR.PulisciStrSql(S5P3.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (4,206,'" & PAR.PulisciStrSql(S5P4.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.XML_CONTRATTI_PARAGRAFI (ID_SEZIONE,PROGR,TESTO,ID_CONTRATTO) VALUES (4,208,'" & PAR.PulisciStrSql(S5P5.Text) & "'," & vIdContratto & ")"
                PAR.cmd.ExecuteNonQuery()


            End If
            myReader.Close()


            PAR.cmd.CommandText = "UPDATE SISCOM_MI.xml_CONTRATTI SET TITOLO='" & PAR.PulisciStrSql(txtTitoloContratto.Text) _
                                & "',S1='" & PAR.PulisciStrSql(txtpreventiva.Text) _
                                & "',S2='" & PAR.PulisciStrSql(txtriscaldamento.Text) _
                                & "',S3='" & PAR.PulisciStrSql(txtAscensore.Text) _
                                & "',S4='" & PAR.PulisciStrSql(txtComuni.Text) _
                                & "',S5='" & PAR.PulisciStrSql(txtGenerali.Text) & "',NOTE='" & PAR.PulisciStrSql(txtNoteContratto.Text) & "' WHERE ID_contratto=" & vIdContratto
            PAR.cmd.ExecuteNonQuery()



            'sezione 1
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.xml_CONTRATTI_SEZIONI SET TITOLO='" & PAR.PulisciStrSql(txtTitoloSezione1.Text) & "' WHERE PROGR=0 AND ID_CONTRATTO=" & vIdContratto
            PAR.cmd.ExecuteNonQuery()

            'paragrafi sezione 1
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.xml_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S1P1.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=0 AND ID_SEZIONE=0"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S1P2.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=2 AND ID_SEZIONE=0"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S1P3.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=4 AND ID_SEZIONE=0"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S1P4.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=6 AND ID_SEZIONE=0"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S1P5.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=8 AND ID_SEZIONE=0"
            PAR.cmd.ExecuteNonQuery()


            'sezione 2
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_SEZIONI SET TITOLO='" & PAR.PulisciStrSql(txtTitoloSezione2.Text) & "' WHERE PROGR=1 AND ID_CONTRATTO=" & vIdContratto
            PAR.cmd.ExecuteNonQuery()

            'paragrafi sezione 2
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S2P1.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=10 AND ID_SEZIONE=1"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S2P2.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=12 AND ID_SEZIONE=1"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S2P3.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=14 AND ID_SEZIONE=1"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S2P4.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=16 AND ID_SEZIONE=1"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S2P5.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=18 AND ID_SEZIONE=1"
            PAR.cmd.ExecuteNonQuery()


            'sezione 3
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_SEZIONI SET TITOLO='" & PAR.PulisciStrSql(txtTitoloSezione3.Text) & "' WHERE PROGR=2 AND ID_CONTRATTO=" & vIdContratto
            PAR.cmd.ExecuteNonQuery()

            'paragrafi sezione 3
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P1.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=20 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P2.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=22 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P3.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=24 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P4.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=26 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P5.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=28 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P6.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=30 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P7.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=32 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P8.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=34 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P9.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=36 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P10.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=38 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P11.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=40 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P12.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=42 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P13.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=44 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P14.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=46 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P15.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=48 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P16.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=50 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P17.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=52 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P18.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=54 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P19.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=56 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P20.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=58 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P21.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=60 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P22.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=62 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P23.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=64 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P24.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=66 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P25.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=68 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P26.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=70 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P27.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=72 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P28.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=74 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P29.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=76 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S3P30.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=78 AND ID_SEZIONE=2"
            PAR.cmd.ExecuteNonQuery()


            'sezione 4
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_SEZIONI SET TITOLO='" & PAR.PulisciStrSql(txtTitoloSezione4.Text) & "' WHERE PROGR=3 AND ID_CONTRATTO=" & vIdContratto
            PAR.cmd.ExecuteNonQuery()

            'PARAGRAFI sezione 4
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P1.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=80 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P2.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=82 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P3.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=84 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P4.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=86 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P5.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=88 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P6.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=90 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P7.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=92 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P8.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=94 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P9.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=96 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P10.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=98 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P11.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=100 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P12.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=102 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P13.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=104 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P14.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=106 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P15.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=108 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P16.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=110 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P17.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=112 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P18.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=114 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P19.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=116 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P20.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=118 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P21.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=120 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P22.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=122 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P23.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=124 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P24.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=126 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P25.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=128 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P26.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=130 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P27.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=132 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P28.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=134 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P29.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=136 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P30.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=138 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P31.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=140 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P32.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=142 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P33.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=144 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P34.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=146 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P35.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=148 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P36.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=150 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P37.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=152 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P38.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=154 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P39.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=156 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P40.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=158 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P41.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=160 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P42.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=162 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P43.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=164 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P44.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=166 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P45.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=168 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P46.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=170 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P47.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=172 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P48.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=174 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P49.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=176 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P50.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=178 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P51.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=180 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P52.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=182 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P53.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=184 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P54.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=186 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P55.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=188 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P56.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=190 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P57.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=192 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P58.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=194 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P59.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=196 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S4P60.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=198 AND ID_SEZIONE=3"
            PAR.cmd.ExecuteNonQuery()



            'sezione 5
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_SEZIONI SET TITOLO='" & PAR.PulisciStrSql(txtTitoloSezione5.Text) & "' WHERE PROGR=4 AND ID_CONTRATTO=" & vIdContratto
            PAR.cmd.ExecuteNonQuery()

            'PARAGRAFI sezione 5
            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S5P1.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=200 AND ID_SEZIONE=4"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S5P2.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=202 AND ID_SEZIONE=4"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S5P3.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=204 AND ID_SEZIONE=4"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S5P4.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=206 AND ID_SEZIONE=4"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.XML_CONTRATTI_PARAGRAFI SET TESTO='" & PAR.PulisciStrSql(S5P5.Text) & "' WHERE ID_CONTRATTO=" & vIdContratto & " AND PROGR=208 AND ID_SEZIONE=4"
            PAR.cmd.ExecuteNonQuery()


            'End Select

            'PAR.OracleConn.Close()

            txtAttiva.Value = "0"
            txtModificato.Value = "0"
            Response.Write("<script>alert('Operazione Effettuata!');</script>")
            'End If



        Catch ex As Exception
            'PAR.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function CaricaContratto()


        PAR.cmd.CommandText = "select xml_Contratti.* from siscom_mi.xml_contratti where xml_contratti.id_contratto=" & vIdContratto
        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderA.Read Then
            txtTitoloContratto.Text = PAR.IfNull(myReaderA("TITOLO"), "")
            txtpreventiva.Text = PAR.IfNull(myReaderA("S1"), "0,00")
            txtriscaldamento.Text = PAR.IfNull(myReaderA("S2"), "0,00")
            txtAscensore.Text = PAR.IfNull(myReaderA("S3"), "0,00")
            txtComuni.Text = PAR.IfNull(myReaderA("S4"), "0,00")
            txtGenerali.Text = PAR.IfNull(myReaderA("S5"), "0,00")
            txtNoteContratto.Text = PAR.IfNull(myReaderA("note"), "")
        End If
        myReaderA.Close()

        PAR.cmd.CommandText = "select xml_contratti_sezioni.titolo from siscom_mi.xml_contratti_sezioni where xml_contratti_sezioni.id_contratto=" & vIdContratto & " order by progr asc"
        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReaderB.Read Then
            txtTitoloSezione1.Text = PAR.IfNull(myReaderB("TITOLO"), "")
            PAR.cmd.CommandText = "select * from siscom_mi.xml_contratti_PARAGRAFI where id_contratto=" & vIdContratto & " and ID_SEZIONE=0 order by progr asc"
            Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderC.Read Then
                S1P1.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S1P2.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S1P3.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S1P4.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S1P5.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
            End If
            myReaderC.Close()

            myReaderB.Read()
            txtTitoloSezione2.Text = PAR.IfNull(myReaderB("TITOLO"), "")
            PAR.cmd.CommandText = "select * from siscom_mi.xml_contratti_PARAGRAFI where id_contratto=" & vIdContratto & " and ID_SEZIONE=1 order by progr asc"
            myReaderC = PAR.cmd.ExecuteReader()
            If myReaderC.Read Then
                S2P1.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S2P2.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S2P3.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S2P4.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S2P5.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
            End If
            myReaderC.Close()

            myReaderB.Read()
            txtTitoloSezione3.Text = PAR.IfNull(myReaderB("TITOLO"), "")
            PAR.cmd.CommandText = "select * from siscom_mi.xml_contratti_PARAGRAFI where id_contratto=" & vIdContratto & " and ID_SEZIONE=2 order by progr asc"
            myReaderC = PAR.cmd.ExecuteReader()
            If myReaderC.Read Then
                S3P1.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P2.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P3.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P4.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P5.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P6.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P7.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P8.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P9.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P10.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P11.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P12.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P13.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P14.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P15.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P16.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P17.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P18.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P19.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P20.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P21.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P22.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P23.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P24.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P25.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P26.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P27.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P28.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P29.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S3P30.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
            End If
            myReaderC.Close()

            myReaderB.Read()
            txtTitoloSezione4.Text = PAR.IfNull(myReaderB("TITOLO"), "")
            PAR.cmd.CommandText = "select * from siscom_mi.xml_contratti_PARAGRAFI where id_contratto=" & vIdContratto & " and ID_SEZIONE=3 order by progr asc"
            myReaderC = PAR.cmd.ExecuteReader()
            If myReaderC.Read Then
                S4P1.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P2.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P3.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P4.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P5.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P6.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P7.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P8.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P9.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P10.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P11.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P12.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P13.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P14.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P15.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P16.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P17.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P18.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P19.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P20.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P21.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P22.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P23.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P24.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P25.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P26.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P27.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P28.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P29.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S4P30.Text = PAR.IfNull(myReaderC("TESTO"), "")
                'myReaderC.Read()
                If vTipo = "1" Or vTipo = "8" Or vTipo = "2" Or vTipo = "10" Or vTipo = "64" Or vTipo = "65" Then
                    myReaderC.Read()
                    S4P31.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P32.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P33.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P34.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P35.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P36.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P37.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P38.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P39.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P40.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P41.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P42.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P43.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P44.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P45.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P46.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P47.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P48.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P49.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P50.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P51.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P52.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P53.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P54.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P55.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P56.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P57.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P58.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P59.Text = PAR.IfNull(myReaderC("TESTO"), "")
                    myReaderC.Read()
                    S4P60.Text = PAR.IfNull(myReaderC("TESTO"), "")
                End If
            End If
            myReaderC.Close()


            myReaderB.Read()
            txtTitoloSezione5.Text = PAR.IfNull(myReaderB("TITOLO"), "")
            PAR.cmd.CommandText = "select * from siscom_mi.xml_contratti_PARAGRAFI where id_contratto=" & vIdContratto & " and ID_SEZIONE=4 order by progr asc"
            myReaderC = PAR.cmd.ExecuteReader()
            If myReaderC.Read Then
                S5P1.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S5P2.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S5P3.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S5P4.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()
                S5P5.Text = PAR.IfNull(myReaderC("TESTO"), "")
                myReaderC.Read()


            End If
            myReaderC.Close()
        End If
        myReaderB.Close()
    End Function

    Protected Sub ImgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEsci.Click
        If txtAttiva.Value = "0" Then
            Response.Write("<script>window.close();</script>")
        End If
    End Sub

    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        Try
            Dim buono As Boolean = True

            Dim importoBolloParam As String = ""

            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim TipoPagamento As String = "P"


            PAR.cmd.CommandText = "select valore from SISCOM_MI.PARAMETRI_BOLLETTA where ID = 18"
            Dim myReaderZ1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderZ1.Read Then
                importoBolloParam = PAR.IfNull(myReaderZ1("VALORE"), "0")
            End If
            myReaderZ1.Close()

            Dim NumCopie As Integer = 1
            PAR.cmd.CommandText = "select valore from SISCOM_MI.PARAMETRI_BOLLETTA where ID = 45"
            myReaderZ1 = PAR.cmd.ExecuteReader()
            If myReaderZ1.Read Then
                NumCopie = PAR.IfNull(myReaderZ1("VALORE"), "0")
            End If
            myReaderZ1.Close()


            PAR.cmd.CommandText = "select rapporti_utenza.* from siscom_mi.RAPPORTI_UTENZA where id=" & vIdContratto
            Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderZ.Read Then
                'If PAR.IfNull(myReaderZ("DATA_STIPULA"), "") = "" Then
                '    BUONO = False
                'End If
                If PAR.IfNull(myReaderZ("DATA_SCADENZA"), "") = "" Then
                    buono = False
                End If
                If PAR.IfNull(myReaderZ("DATA_DECORRENZA_AE"), "") = "" Then
                    buono = False
                End If
                'If PAR.IfNull(myReaderZ("COD_UFFICIO_REG"), "") = "" Then
                '    BUONO = False
                'End If
                If PAR.IfNull(myReaderZ("MESI_DISDETTA"), 0) = 0 Then
                    buono = False
                End If
                If PAR.IfNull(myReaderZ("data_stipula"), "") = "" Then
                    buono = False
                End If
                If PAR.IfNull(myReaderZ("data_consegna"), "") = "" Then
                    buono = False
                End If

                If PAR.IfNull(myReaderZ("data_scadenza"), "") = "" Then
                    buono = False
                End If

                If PAR.IfNull(myReaderZ("data_scadenza_rinnovo"), "") = "" Then
                    buono = False
                End If

                If PAR.IfNull(myReaderZ("durata_anni"), "-1") = "-1" Then
                    buono = False
                End If

                If PAR.IfNull(myReaderZ("durata_rinnovo"), "-1") = "-1" Then
                    buono = False
                End If

                If PAR.IfNull(myReaderZ("versamento_tr"), "A") = "U" Then
                    TipoPagamento = "T"
                End If

            End If
            myReaderZ.Close()

            Dim Agevolato As String = "N"

            If vTipo = "6" Or vTipo = "62" Or vTipo = "64" Or vTipo = "65" Then
                Agevolato = "S"
            End If

            If vTipo = "3" Or vTipo = "NEGOZI" Then
                PAR.cmd.CommandText = "select identificativi_catastali.* from siscom_mi.identificativi_catastali,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari where identificativi_catastali.id=unita_immobiliari.id_catastale and unita_immobiliari.id=unita_contrattuale.id_unita and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & vIdContratto
                myReaderZ = PAR.cmd.ExecuteReader()
                If myReaderZ.HasRows = True Then
                    If myReaderZ.Read Then
                        If PAR.IfNull(myReaderZ("superficie_catastale"), "0") = "0" Then
                            buono = False
                        End If
                    End If
                Else
                    buono = False
                End If
                myReaderZ.Close()
            End If

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If buono = False Then
                Response.Write("<script>alert('Attenzione, non è possibile stampare. Alcuni dati fondamentali, DATA STIPULA, DATA DECORRENZA, DATA CONSEGNA, DATA SCADENZA, DATA SEC. SCADENZA, DURATA, MESI DISDETTA non sono stati inseriti nel contratto, oppure, se USI Diversi, potrebbe mancare la sup. catastale!');</script>")
            End If

            If buono = True Then
                If txtAttiva.Value = "0" Then
                    CaricaDati()


                    Dim percorso As String = ""
                    Dim NomeFile As String = Mid(Request.QueryString("C"), 16, 50) & "_" & Format(Now, "yyyyMMddHHmmss")
                    Dim TestoContratto As String = ""


                    'TestoContratto = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & " ?>" & vbCrLf _
                    '              & "<!DOCTYPE FileContratti SYSTEM " & Chr(34) & "FileContratti.dtd" & Chr(34) & ">" & vbCrLf _
                    '              & "<?xml-stylesheet href=" & Chr(34) & "FileContratti XLS.xsl" & Chr(34) & " type=" & Chr(34) & "text/xsl" & Chr(34) & "?>" & vbCrLf _
                    '              & "<FileContratti CodiceFiscaleFornitore=" & Chr(34) & cflocatore & Chr(34) _
                    '              & " CodiceUfficio=" & Chr(34) & UfficioRegistro & Chr(34) & " CodiceFiscaleConto=" & Chr(34) _
                    '              & cflocatore & Chr(34) & " ValutaPrelievo=" & Chr(34) & "E" & Chr(34) & ">" & vbCrLf _
                    '              & "<Contratto TipoContratto=" & Chr(34) & "S" & Chr(34) & " IdContratto=" & Chr(34) _
                    '              & vIdContratto & Chr(34) & " SoggettoIVA=" & Chr(34) & "N" & Chr(34) _
                    '              & " RegistrazioneEsente=" & Chr(34) & "N" & Chr(34) _
                    '              & " ContrattoAgevolato=" & Chr(34) & "N" & Chr(34) _
                    '              & " OggettoLocazione=" & Chr(34) & "02" & Chr(34) _
                    '              & " TipoPagamento=" & Chr(34) & "P" & Chr(34) _
                    '              & " ImportoBollo=" & Chr(34) & "$BOLLO$" & Chr(34) _
                    '              & " ImportoRegistrazione=" & Chr(34) & ImportoRegistro & Chr(34) _
                    '              & " ImportoSanzioniRegistrazione=" & Chr(34) & Chr(34) _
                    '              & " ImportoInteressi=" & Chr(34) & Chr(34) _
                    '              & " NumeroPagine=" & Chr(34) & "$PAGINE$" & Chr(34) & ">" & vbCrLf

                    TestoContratto = "<Contratto TipoContratto=" & Chr(34) & "S" & Chr(34) & " IdContratto=" & Chr(34) _
                                  & vIdContratto & Chr(34) & " SoggettoIVA=" & Chr(34) & "N" & Chr(34) _
                                  & " RegistrazioneEsente=" & Chr(34) & "N" & Chr(34) _
                                  & " ContrattoAgevolato=" & Chr(34) & Agevolato & Chr(34) _
                                  & " OggettoLocazione=" & Chr(34) & "02" & Chr(34) _
                                  & " TipoPagamento=" & Chr(34) & TipoPagamento & Chr(34) _
                                  & " ImportoBollo=" & Chr(34) & "$BOLLO$" & Chr(34) _
                                  & " ImportoRegistrazione=" & Chr(34) & PAR.PuntiInVirgole(ImportoRegistro) & Chr(34) _
                                  & " ImportoSanzioniRegistrazione=" & Chr(34) & Chr(34) _
                                  & " ImportoInteressi=" & Chr(34) & Chr(34) _
                                  & " NumeroPagine=" & Chr(34) & "$PAGINE$" & Chr(34) & ">" & vbCrLf

                    TestoContratto = TestoContratto & "<TitoloContratto>" & Sostituisci(txtTitoloContratto.Text) & "</TitoloContratto>" & vbCrLf
                    'If txtTitoloSezione1.Text <> "" Then
                    TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
                    TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione1.Text) & "</TitoloSezione>" & vbCrLf
                    If S1P1.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P1.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S1P2.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P2.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S1P3.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P3.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S1P4.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P4.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S1P5.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S1P5.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
                    'End If

                    'If txtTitoloSezione2.Text <> "" Then
                    TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
                    TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione2.Text) & "</TitoloSezione>" & vbCrLf
                    If S2P1.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P1.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S2P2.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P2.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S2P3.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P3.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S2P4.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P4.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S2P5.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S2P5.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    TestoContratto = TestoContratto & "<Paragrafo></Paragrafo>" & vbCrLf

                    TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
                    'End If

                    'If txtTitoloSezione3.Text <> "" Then
                    TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
                    TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione3.Text) & "</TitoloSezione>" & vbCrLf

                    If S3P1.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P1.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P2.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P2.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P3.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P3.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P4.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P4.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P5.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P5.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P6.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P6.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P7.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P7.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P8.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P8.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P9.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P9.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P10.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P10.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P11.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P11.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P12.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P12.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P13.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P13.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P14.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P14.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P15.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P15.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P16.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P16.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P17.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P17.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P18.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P18.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P19.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P19.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P20.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P20.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P21.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P21.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P22.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P22.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P23.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P23.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P24.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P24.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P25.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P25.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P26.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P26.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P27.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P27.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P28.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P28.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P29.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P29.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S3P30.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S3P30.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    TestoContratto = TestoContratto & "<Paragrafo></Paragrafo>" & vbCrLf
                    TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
                    'End If

                    'If txtTitoloSezione4.Text <> "" Then
                    TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
                    TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione4.Text) & "</TitoloSezione>" & vbCrLf

                    If S4P1.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P1.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P2.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P2.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P3.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P3.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P4.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P4.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P5.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P5.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P6.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P6.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P7.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P7.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P8.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P8.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P9.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P9.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P10.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P10.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P11.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P11.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P12.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P12.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P13.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P13.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P14.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P14.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P15.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P15.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P16.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P16.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P17.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P17.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P18.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P18.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P19.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P19.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P20.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P20.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P21.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P21.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P22.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P22.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P23.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P23.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P24.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P24.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P25.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P25.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P26.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P26.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P27.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P27.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P28.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P28.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P29.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P29.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P30.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P30.Text) & "</Paragrafo>" & vbCrLf
                    End If



                    If S4P31.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P31.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P32.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P32.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P33.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P33.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P34.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P34.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P35.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P35.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P36.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P36.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P37.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P37.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P38.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P38.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P39.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P39.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P40.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P40.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P41.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P41.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P42.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P42.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P43.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P43.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P44.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P44.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P45.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P45.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P46.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P46.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P47.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P47.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P48.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P48.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P49.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P49.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P50.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P50.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P51.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P51.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P52.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P52.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P53.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P53.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P54.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P54.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P55.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P55.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P56.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P56.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P57.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P57.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P58.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P58.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P59.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P59.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    If S4P60.Text <> "" Then
                        TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S4P60.Text) & "</Paragrafo>" & vbCrLf
                    End If
                    TestoContratto = TestoContratto & "<Paragrafo></Paragrafo>" & vbCrLf
                    TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
                    'End If

                    If txtTitoloSezione5.Text <> "" Then
                        TestoContratto = TestoContratto & "<Sezione>" & vbCrLf
                        TestoContratto = TestoContratto & "<TitoloSezione>" & Sostituisci(txtTitoloSezione5.Text) & "</TitoloSezione>" & vbCrLf

                        If S5P1.Text <> "" Then
                            TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P1.Text) & "</Paragrafo>" & vbCrLf
                        End If
                        If S5P2.Text <> "" Then
                            TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P2.Text) & "</Paragrafo>" & vbCrLf
                        End If
                        If S5P3.Text <> "" Then
                            TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P3.Text) & "</Paragrafo>" & vbCrLf
                        End If
                        If S5P4.Text <> "" Then
                            TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P4.Text) & "</Paragrafo>" & vbCrLf
                        End If
                        If S5P5.Text <> "" Then
                            TestoContratto = TestoContratto & "<Paragrafo>" & Sostituisci(S5P5.Text) & "</Paragrafo>" & vbCrLf
                        End If

                        TestoContratto = TestoContratto & "<Paragrafo></Paragrafo>" & vbCrLf
                        TestoContratto = TestoContratto & "</Sezione>" & vbCrLf
                    End If

                    'TestoContratto = TestoContratto & "</Contratto></FileContratti>"
                    TestoContratto = TestoContratto & "</Contratto>"
                    TestoContratto = caricaRespFiliale(vIdContratto, TestoContratto, 1)
                    TestoContratto = Replace(TestoContratto, "IN DATA", "in data")
                    TestoContratto = Replace(TestoContratto, " IN ", " in ")
                    TestoContratto = Replace(TestoContratto, "AUTO", "auto")

                    Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\ZZZ") & NomeFile & ".xml", False, System.Text.Encoding.UTF8)
                    sr.WriteLine(TestoContratto)
                    sr.Close()



                    Dim url As String = Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\ZZZ") & NomeFile & ".xml"

                    'Dim xml As New XmlTextReader(url)
                    'Dim MIOTESTO As String = ""

                    'Do While xml.Read
                    '    MIOTESTO = Trim(xml.ReadString)
                    '    If MIOTESTO <> "" Or MIOTESTO <> " " Then
                    '        TextBox1.Text = TextBox1.Text & MIOTESTO & " "
                    '    End If
                    'Loop
                    'xml.Close()

                    'TextBox1.Text = Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 1)

                    Dim Righe As Integer = 0
                    Dim pagine As Integer = 0
                    Dim importobollo As String = ""
                    Dim r1 As Double = 0


                    'Righe = CalcolaRighe()

                    'r1 = Int(CInt(Righe) / 100)
                    'If (CInt(Righe) Mod 100) > 0 Then r1 = r1 + 1



                    'pagine = (r1 * 2) * 3

                    'importobollo = Format((r1 * 3) * PAR.PuntiInVirgole(bollosucontratto), "0.00")
                    importobollo = Replace(importoBolloParam, ".", ",") * 7
                    If vTipo = "6" Or vTipo = "61" Or vTipo = "62" Or vTipo = "62" Or vTipo = "12" Or vTipo = "1" Or vTipo = "ERPB" Or vTipo = "8" Or vTipo = "2" Or vTipo = "10" Or vTipo = "64" Or vTipo = "65" Then
                        pagine = 5
                        importobollo = Replace(importoBolloParam, ".", ",") * 5
                    End If

                    If vTipo = "392ASS" Then
                        pagine = 6
                        importobollo = Replace(importoBolloParam, ".", ",") * 6
                    End If

                    If vTipo = "BOX" Or vTipo = "NEGOZI" Or vTipo = "COMODATO" Then
                        pagine = 6
                        importobollo = Replace(importoBolloParam, ".", ",") * 6
                    End If

                    If FL_BOLLO_ESENTE = "1" Then
                        pagine = 2
                        importobollo = "0,00"
                    End If

                    importobollo = Ceiling((CDec(pagine) / 4)) * CDec(PAR.PuntiInVirgole(importoBolloParam)) * numcopie

                    'TestoContratto = Replace(TestoContratto, "$BOLLO$", importobollo)
                    'TestoContratto = Replace(TestoContratto, "$PAGINE$", pagine)

                    TestoContratto = Replace(TestoContratto, "$BOLLO$", "0,00")
                    TestoContratto = Replace(TestoContratto, "$PAGINE$", "2")


                    TestoContratto = Replace(TestoContratto, "é", "e'")
                    TestoContratto = Replace(TestoContratto, "ì", "i'")
                    TestoContratto = Replace(TestoContratto, "à", "a'")
                    TestoContratto = Replace(TestoContratto, "è", "e'")
                    TestoContratto = Replace(TestoContratto, "ò", "o'")


                    sr = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\") & NomeFile & ".xml", False, System.Text.Encoding.GetEncoding("ISO-8859-1"))
                    sr.WriteLine(TestoContratto)
                    sr.Close()

                    System.IO.File.Delete(url)

                    PAR.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                    PAR.cmd.CommandText = "update siscom_mi.rapporti_utenza set fl_stampato=1,bollo='" & importobollo & "',REG_TELEMATICA='' where id=" & vIdContratto
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F03','')"
                    PAR.cmd.ExecuteNonQuery()



                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_REGISTRAZIONE (ID_CONTRATTO,TESTO_XML) " _
                    & "VALUES (" & vIdContratto & ",:TESTO) "


                    Dim objStream As Stream = File.Open(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\") & NomeFile & ".xml", FileMode.Open)
                    Dim buffer(objStream.Length) As Byte
                    objStream.Read(buffer, 0, objStream.Length)
                    objStream.Close()

                    Dim parmData As New Oracle.DataAccess.Client.OracleParameter
                    With parmData
                        .Direction = Data.ParameterDirection.Input
                        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
                        .ParameterName = "TESTO"
                        .Value = buffer
                    End With

                    PAR.cmd.Parameters.Add(parmData)
                    PAR.cmd.ExecuteNonQuery()

                    System.IO.File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\") & NomeFile & ".xml")


                    PAR.myTrans.Commit()
                    PAR.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, PAR.myTrans)


                    imgSalva.Visible = False
                    imgAnteprima.Visible = False
                    imgStampa.Visible = False

                    imgConduttore.Visible = True
                    imgAllegatoContratto.Visible = True

                    imgAnnullaStampa.Visible = True

                    ImgInfo.Visible = True

                    txtpreventiva.Enabled = False
                    txtriscaldamento.Enabled = False
                    txtAscensore.Enabled = False
                    txtComuni.Enabled = False
                    txtGenerali.Enabled = False
                    txtNoteContratto.Enabled = False


                    'Response.Write("<script>alert('Operazione Effettuata! Premere il pulsante SALVA del rapporto per rendere effettive le modifiche e aggiornare i dati!');window.opener.document.form1.txtModificato.value = '1';</script>")
                    Response.Write("<script>alert('Operazione Effettuata! Premere il pulsante SALVA del rapporto per rendere effettive le modifiche e aggiornare i dati!');window.opener.document.forms['form1'].elements['txtModificato'].value = '1';</script>")



                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Function CalcolaRighe() As Integer
        Dim POS As Integer
        Dim NUMERORIGHE As Integer
        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim INIZIO As Integer

        CalcolaRighe = 0

        INIZIO = 1
        NUMERORIGHE = 0
        POS = 1

        For I = 1 To Len(TextBox1.Text)
            If Mid(TextBox1.Text, I, 2) = vbNewLine Then
                I = I + 1
                NUMERORIGHE = NUMERORIGHE + 1
                POS = 1
                INIZIO = I
            Else
                If POS > 65 Then
                    K = 0
                    If Mid(TextBox1.Text, I, 1) <> " " Then
                        For J = I To 1 Step -1
                            K = K + 1
                            If Mid(TextBox1.Text, J, 1) = " " Then
                                I = I - K
                                Exit For
                            End If
                        Next
                    End If
                    If K = 0 Then K = 1
                    INIZIO = I + 1
                    POS = 1
                    NUMERORIGHE = NUMERORIGHE + 1
                End If
            End If
            POS = POS + 1
        Next
        CalcolaRighe = NUMERORIGHE + 1
    End Function


    Private Function SostSegnaposto(ByVal TestoContratto As String) As String
        Dim Contratto As String = TestoContratto

        Contratto = Sostituisci(Contratto)

        Contratto = Replace(Contratto, "$daticatastali$", DatiCatastali)
        Contratto = Replace(Contratto, "$bolloesente$", bolloesente)
        Contratto = Replace(Contratto, "$tipocanoneapplicato$", TESTO_DDD)
        Contratto = Replace(Contratto, "$locatore$", locatore)
        Contratto = Replace(Contratto, "$cflocatore$", cflocatore)
        Contratto = Replace(Contratto, "$sedelocatore$", sedelocatore)

        Contratto = Replace(Contratto, "$superfici$", SuperficiAlloggio)
        Contratto = Replace(Contratto, "$codiceutente$", CodiceUtente)
        Contratto = Replace(Contratto, "$codcontratto$", codcontratto)
        Contratto = Replace(Contratto, "$nominativo$", nominativoconduttore)
        Contratto = Replace(Contratto, "$luogonascita$", Contratto)

        Contratto = Replace(Contratto, "$datanascitaconduttore$", datanascitaconduttore)
        Contratto = Replace(Contratto, "$comuneconduttore$", comuneConduttore)
        Contratto = Replace(Contratto, "$provinciaconduttore$", provinciaconduttore)
        Contratto = Replace(Contratto, "$datanascitaconduttore$", datanascitaconduttore)
        Contratto = Replace(Contratto, "$codfiscale$", cfconduttore)

        Contratto = Replace(Contratto, "$comunealloggio$", comunealloggio)
        Contratto = Replace(Contratto, "$via$", viaalloggio)
        Contratto = Replace(Contratto, "$civico$", civicoalloggio)
        Contratto = Replace(Contratto, "$piano$", piano)
        Contratto = Replace(Contratto, "$scala$", scala)
        Contratto = Replace(Contratto, "$interno$", interno)
        Contratto = Replace(Contratto, "$vani$", numlocali)

        Contratto = Replace(Contratto, "$foglio$", foglio)
        Contratto = Replace(Contratto, "$particella$", particella)
        Contratto = Replace(Contratto, "$sub$", subalterno)
        Contratto = Replace(Contratto, "$zona$", zona)
        Contratto = Replace(Contratto, "$categ$", categoria)
        Contratto = Replace(Contratto, "$cl$", classe)
        Contratto = Replace(Contratto, "$rendita$", rendita)


        Contratto = Replace(Contratto, "$numdetermina$", "45/2010")
        Contratto = Replace(Contratto, "$numdeterminabando$", "n. 7 del 09/02/2010")

        If canoneannuo = "" Then canoneannuo = "0"
        Contratto = Replace(Contratto, "$canoneiniziale1$", Format(CDbl(canoneannuo), "##,##0.00"))
        If DDD = "1" And annuoteorico = "0,00" Then
            annuoteorico = "---"
        End If
        If IsNumeric(annuoteorico) Then
            Contratto = Replace(Contratto, "$annuoteorico1$", Format(CDbl(annuoteorico), "##,##0.00"))
        Else
            Contratto = Replace(Contratto, "$annuoteorico1$", "---")
        End If


        Contratto = Replace(Contratto, "$depositocauzionale1$", Format(CDbl(anticipo), "##,##0.00"))


        Contratto = Replace(Contratto, "$decorrenza$", decorrenzacontratto)
        Contratto = Replace(Contratto, "$DataStipula$", "")
        Contratto = Replace(Contratto, "$scadenza$", scadenza)
        Contratto = Replace(Contratto, "$superficie_netta$", superficie_netta)

        'Contratto = Replace(Contratto, "$numerolocali$", numlocali)
        'Contratto = Replace(Contratto, "$numeroservizi$", numservizi)
        If numlocali = "0" And numservizi = "0" Then
            Contratto = Replace(Contratto, "$numerolocaliservizi$", "")
        Else
            If (numlocali <> "" And numlocali <> "--") Or (numservizi <> "" And numservizi <> "--") Then
                Contratto = Replace(Contratto, "$numerolocaliservizi$", "costituita da numero " & numlocali & " locali più " & numservizi & " servizi")
            Else
                Contratto = Replace(Contratto, "$numerolocaliservizi$", "")
            End If
        End If
        

        Contratto = Replace(Contratto, "$denominazioneedificio$", denominazioneedificio)

        Contratto = Replace(Contratto, "$destinazione$", destinazione)

        Contratto = Replace(Contratto, "$notecontratto$", notecontratto)


        If conduttoresesso <> "" Then
            If conduttoresesso = "M" Then
                Contratto = Replace(Contratto, "$sig$", "Sig.")
            Else
                Contratto = Replace(Contratto, "$sig$", "Sig.ra")
            End If
        Else
            Contratto = Replace(Contratto, "$sig$", "Sig/Sig.ra")
        End If

        If riscaldamento <> "0,00" Then
            Contratto = Replace(Contratto, "$riscaldamento1$", Format(CDbl(riscaldamento), "##,##0.00"))
        Else
            Contratto = Replace(Contratto, "$riscaldamento1$", "0,00")
        End If
        If ascensore <> "0,00" Then
            Contratto = Replace(Contratto, "$ascensore1$", Format(CDbl(ascensore), "##,##0.00"))
        Else
            Contratto = Replace(Contratto, "$ascensore1$", "0,00")
        End If
        If servizicomuni <> "0,00" Then
            Contratto = Replace(Contratto, "$servizicomuni1$", Format(CDbl(servizicomuni), "##,##0.00"))
        Else
            Contratto = Replace(Contratto, "$servizicomuni1$", "0,00")
        End If
        If spgenerali <> "0,00" Then
            Contratto = Replace(Contratto, "$spgenerali1$", Format(CDbl(spgenerali), "##,##0.00"))
        Else
            Contratto = Replace(Contratto, "$spgenerali1$", "0,00")
        End If
        If quotapreventiva <> "0,00" Then
            Contratto = Replace(Contratto, "$totanticipo1$", Format(CDbl(quotapreventiva), "##,##0.00"))
            Contratto = Replace(Contratto, "$quotapreventiva1$", Format(CDbl(quotapreventiva), "##,##0.00"))
        Else
            Contratto = Replace(Contratto, "$totanticipo1$", "0,00")
            Contratto = Replace(Contratto, "$quotapreventiva1$", "0,00")
        End If



        Contratto = Replace(Contratto, "$DataStipula$", "")


        Contratto = Replace(Contratto, "$ragionesociale$", ragionesociale)
        Contratto = Replace(Contratto, "$partitaiva$", partitaiva)
        Contratto = Replace(Contratto, "$codutente$", CodiceUtente)
        Contratto = Replace(Contratto, "$codcontratto$", codcontratto)
        Contratto = Replace(Contratto, "$codfiscale$", cfconduttore)
        Contratto = Replace(Contratto, "$residenzaconduttore$", residenzaconduttore)
        Contratto = Replace(Contratto, "$estremiconduttore$", EstremiConduttore)

        Contratto = Replace(Contratto, "$durata$", durata)
        Contratto = Replace(Contratto, "$zonafascia$", zona)

        Contratto = Replace(Contratto, "$supcommerciale$", PAR.IfEmpty(supcommerciale, "0"))
        Contratto = Replace(Contratto, "$suplorda$", PAR.IfEmpty(suplorda, "0"))
        Contratto = Replace(Contratto, "$balconiterrazzi$", PAR.IfEmpty(balconiterrazzi, "0"))
        Contratto = Replace(Contratto, "$supcantina$", PAR.IfEmpty(supcantina, "0"))
        Contratto = Replace(Contratto, "$commercialeparticomuni$", PAR.IfEmpty(commercialeparticomuni, "0"))
        Contratto = Replace(Contratto, "$bolloesente$", bolloesente)
        Contratto = Replace(Contratto, "$derogaart15$", derogaart15)
        '
        Contratto = Replace(Contratto, "$superficie_cat$", superficie_catastale)


        Select Case vTipo
            Case "61"
                ColSx = Sostituisci(S3P3.Text) & "</br></br>" & Sostituisci(S3P4.Text) & "</br></br>" & Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text) & "</br></br>" & Sostituisci(S3P11.Text)
                ColDx = Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text) & "</br></br>" & Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br>" & Sostituisci(S3P16.Text) & "</br></br>" & Sostituisci(S3P17.Text) & "</br></br>" & Sostituisci(S3P18.Text) & "</br></br>" & Sostituisci(S3P19.Text) & "</br></br>" & Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) '& "</br></br>" & Sostituisci(S3P23.Text) & "</br>" & Sostituisci(S3P24.Text)
            Case "62"
                ColSx = Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text) & "</br></br>" & Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text) & "</br></br>" & Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text)
                ColDx = Sostituisci(S3P16.Text) & "</br></br>" & Sostituisci(S3P17.Text) & "</br></br>" & Sostituisci(S3P18.Text) & "</br></br>" & Sostituisci(S3P19.Text) & "</br></br>" & Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) & "</br></br>" & Sostituisci(S3P23.Text) & "</br></br>" & Sostituisci(S3P24.Text) & "</br></br>" & Sostituisci(S3P25.Text) & "</br></br>" & Sostituisci(S3P26.Text) & "</br>" & Sostituisci(S3P27.Text) & "</br>" & Sostituisci(S3P28.Text) & "</br>" & Sostituisci(S3P29.Text)
            Case "63"
                ColSx = Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text) & "</br></br>" & Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text)
                ColDx = Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br>" & Sostituisci(S3P16.Text) & "</br></br>" & Sostituisci(S3P17.Text) & "</br></br>" & Sostituisci(S3P18.Text) & "</br></br>" & Sostituisci(S3P19.Text) & "</br></br>" & Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) & "</br></br>" & Sostituisci(S3P23.Text) & "</br></br>" & Sostituisci(S3P24.Text) & "</br></br>" & Sostituisci(S3P25.Text) '& "</br></br>" & Sostituisci(S3P26.Text) & "</br>" & Sostituisci(S3P27.Text) & "</br>" & Sostituisci(S3P28.Text) & "</br>" & Sostituisci(S3P29.Text)

            Case "64" 'Nuovo Contratto art.15
                ColSx = Sostituisci(S4P4.Text) & "</br>" & Sostituisci(S4P5.Text) & "</br>" & Sostituisci(S4P6.Text) & "</br>" & Sostituisci(S4P7.Text) & "</br>" & Sostituisci(S4P8.Text) & "</br>" & Sostituisci(S4P9.Text) & "</br>" & Sostituisci(S4P10.Text) & "</br>" & Sostituisci(S4P11.Text) & "</br>" & Sostituisci(S4P12.Text) & "</br>" & Sostituisci(S4P13.Text) & "</br>" & Sostituisci(S4P14.Text) & "</br>" & Sostituisci(S4P15.Text) & "</br>" & Sostituisci(S4P16.Text) & "</br>" & Sostituisci(S4P17.Text) & "</br>" & Sostituisci(S4P18.Text) & "</br>" & Sostituisci(S4P19.Text)
                ColDx = Sostituisci(S4P20.Text) & "</br>" & Sostituisci(S4P21.Text) & "</br>" & Sostituisci(S4P22.Text) & "</br>" & Sostituisci(S4P23.Text) & "</br>" & Sostituisci(S4P24.Text) & "</br>" & Sostituisci(S4P25.Text) & "</br>" & Sostituisci(S4P26.Text) & "</br>" & Sostituisci(S4P27.Text) & "</br>" & Sostituisci(S4P28.Text) & "</br>" & Sostituisci(S4P29.Text) & "</br>" & Sostituisci(S4P30.Text) & "</br>" & Sostituisci(S4P31.Text) & "</br>" & Sostituisci(S4P32.Text) & "</br>" & Sostituisci(S4P33.Text) & "</br>" & Sostituisci(S4P34.Text) & "</br>" & Sostituisci(S4P35.Text) & "</br>" & Sostituisci(S4P36.Text) & "</br>" & Sostituisci(S4P37.Text) & "</br>" & Sostituisci(S4P38.Text) & "</br>" & Sostituisci(S4P39.Text) & "</br>" & Sostituisci(S4P40.Text) & "</br>" & Sostituisci(S4P41.Text) & "</br>" & Sostituisci(S4P42.Text) & "</br>" & Sostituisci(S4P43.Text) & "</br>" & Sostituisci(S4P44.Text) & "</br>" & Sostituisci(S4P45.Text) & "</br>" & Sostituisci(S4P46.Text) & "</br>" & Sostituisci(S4P47.Text) & "</br>" & Sostituisci(S4P48.Text) & "</br>" & Sostituisci(S4P49.Text) & "</br>" & Sostituisci(S4P50.Text) & "</br>" & Sostituisci(S4P51.Text) & "</br>" & Sostituisci(S4P52.Text)
            Case "65" 'Nuovo Contratto art.15 C.2
                ColSx = Sostituisci(S4P4.Text) & "</br>" & Sostituisci(S4P5.Text) & "</br>" & Sostituisci(S4P6.Text) & "</br>" & Sostituisci(S4P7.Text) & "</br>" & Sostituisci(S4P8.Text) & "</br>" & Sostituisci(S4P9.Text) & "</br>" & Sostituisci(S4P10.Text) & "</br>" & Sostituisci(S4P11.Text) & "</br>" & Sostituisci(S4P12.Text) & "</br>" & Sostituisci(S4P13.Text) & "</br>" & Sostituisci(S4P14.Text) & "</br>" & Sostituisci(S4P15.Text) & "</br>" & Sostituisci(S4P16.Text) & "</br>" & Sostituisci(S4P17.Text) & "</br>" & Sostituisci(S4P18.Text) & "</br>" & Sostituisci(S4P19.Text) & "</br>" & Sostituisci(S4P20.Text) & "</br>" & Sostituisci(S4P21.Text) & "</br>" & Sostituisci(S4P22.Text) & "</br>" & Sostituisci(S4P23.Text) & "</br>" & Sostituisci(S4P24.Text) & "</br>" & Sostituisci(S4P25.Text) & "</br>"
                ColDx = Sostituisci(S4P26.Text) & "</br>" & Sostituisci(S4P27.Text) & "</br>" & Sostituisci(S4P28.Text) & "</br>" & Sostituisci(S4P29.Text) & "</br>" & Sostituisci(S4P30.Text) & "</br>" & Sostituisci(S4P31.Text) & "</br>" & Sostituisci(S4P32.Text) & "</br>" & Sostituisci(S4P33.Text) & "</br>" & Sostituisci(S4P34.Text) & "</br>" & Sostituisci(S4P35.Text) & "</br>" & Sostituisci(S4P36.Text) & "</br>" & Sostituisci(S4P37.Text) & "</br>" & Sostituisci(S4P38.Text) & "</br>" & Sostituisci(S4P39.Text) & "</br>" & Sostituisci(S4P40.Text) & "</br>" & Sostituisci(S4P41.Text) & "</br>" & Sostituisci(S4P42.Text) & "</br>" & Sostituisci(S4P43.Text) & "</br>" & Sostituisci(S4P44.Text) & "</br>" & Sostituisci(S4P45.Text) & "</br>" & Sostituisci(S4P46.Text) & "</br>" & Sostituisci(S4P47.Text) & "</br>" & Sostituisci(S4P48.Text) & "</br>" & Sostituisci(S4P49.Text) & "</br>" & Sostituisci(S4P50.Text) & "</br>" & Sostituisci(S4P51.Text) & "</br>" & Sostituisci(S4P52.Text)
            Case "392ASS"
                ColSx = Sostituisci(S3P3.Text) & "</br></br>" & Sostituisci(S3P4.Text) & "</br></br>" & Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text)
                ColDx = Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text) & "</br></br>" & Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br>" & Sostituisci(S3P16.Text) & "</br></br>" & Sostituisci(S3P17.Text) '& "</br></br>" & Sostituisci(S3P22.Text) & "</br></br>" & Sostituisci(S3P23.Text) & "</br></br>" & Sostituisci(S3P24.Text) & "</br></br>" & Sostituisci(S3P25.Text) & "</br></br>" & Sostituisci(S3P26.Text) & "</br>" & Sostituisci(S3P27.Text) & "</br>" & Sostituisci(S3P28.Text) & "</br>" & Sostituisci(S3P29.Text)

            Case "6"
                ColSx = Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text) & "</br></br>" & Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text)
                ColDx = Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br>" & Sostituisci(S3P16.Text) & "</br></br>" & Sostituisci(S3P17.Text) & "</br></br>" & Sostituisci(S3P18.Text) & "</br></br>" & Sostituisci(S3P19.Text) & "</br></br>" & Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) '& "</br></br>" & Sostituisci(S3P23.Text) & "</br>" & Sostituisci(S3P24.Text)
            Case "BOX"
                ColSx = Sostituisci(S3P3.Text) & "</br></br>" & Sostituisci(S3P4.Text) & "</br></br>" & Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text)
                ColDx = Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text) & "</br></br>" & Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br><p style='font-family: Arial; font-size: 10pt'></br></br></p>" ' & Sostituisci(S3P18.Text) & "</br></br>" & Sostituisci(S3P19.Text) & "</br></br>" & Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) '& "</br></br>" & Sostituisci(S3P23.Text) & "</br>" & Sostituisci(S3P24.Text)
            Case "NEGOZI"
                ColSx = Sostituisci(S3P3.Text) & "</br></br>" & Sostituisci(S3P4.Text) & "</br></br>" & Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text)
                ColDx = Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text) & "</br></br>" & Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br>" & Sostituisci(S3P16.Text) & "</br></br><p style='font-family: Arial; font-size: 10pt'></br></br></p>" '& Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) '& "</br></br>" & Sostituisci(S3P23.Text) & "</br>" & Sostituisci(S3P24.Text)
            Case "CONCESSIONE"
                ColSx = Sostituisci(S3P3.Text) & "</br></br>" & Sostituisci(S3P4.Text) & "</br></br>" & Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text)
                ColDx = Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text) & "</br></br>" & Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br>" & Sostituisci(S3P16.Text) & "</br></br><p style='font-family: Arial; font-size: 10pt'></br></br></p>" '& Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) '& "</br></br>" & Sostituisci(S3P23.Text) & "</br>" & Sostituisci(S3P24.Text)
            Case "ERPB"
                ColSx = Sostituisci(S3P3.Text) & "</br>" & Sostituisci(S3P4.Text) & "</br>" & Sostituisci(S3P5.Text) & "</br>" & Sostituisci(S3P6.Text) & "</br>" & Sostituisci(S3P7.Text)
                ColDx = Sostituisci(S3P8.Text) & "</br>" & Sostituisci(S3P9.Text) & "</br>" & Sostituisci(S3P10.Text) & "</br>" & Sostituisci(S3P11.Text) & "</br>" & Sostituisci(S3P12.Text) & "</br>" & Sostituisci(S3P13.Text) & "</br>" & Sostituisci(S3P14.Text) & "</br>" & Sostituisci(S3P15.Text) & "</br>" & Sostituisci(S3P16.Text) & "</br><p style='font-family: Arial; font-size: 8pt'></p></br>" '& Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) '& "</br></br>" & Sostituisci(S3P23.Text) & "</br>" & Sostituisci(S3P24.Text)
            Case "COMODATO"
                ColSx = Sostituisci(S3P3.Text) & "</br></br>" & Sostituisci(S3P4.Text) & "</br></br>" & Sostituisci(S3P5.Text) & "</br></br>" & Sostituisci(S3P6.Text) & "</br></br>" & Sostituisci(S3P7.Text) & "</br></br>" & Sostituisci(S3P8.Text) & "</br></br>" & Sostituisci(S3P9.Text) & "</br></br>" & Sostituisci(S3P10.Text)
                ColDx = Sostituisci(S3P11.Text) & "</br></br>" & Sostituisci(S3P12.Text) & "</br></br>" & Sostituisci(S3P13.Text) & "</br></br>" & Sostituisci(S3P14.Text) & "</br></br>" & Sostituisci(S3P15.Text) & "</br></br>" & Sostituisci(S3P16.Text) & "</br></br><p style='font-family: Arial; font-size: 10pt'></br></br></p>" '& Sostituisci(S3P20.Text) & "</br></br>" & Sostituisci(S3P21.Text) & "</br></br>" & Sostituisci(S3P22.Text) '& "</br></br>" & Sostituisci(S3P23.Text) & "</br>" & Sostituisci(S3P24.Text)

            Case Else
                ColSx = Sostituisci(S4P4.Text) & "</br>" & Sostituisci(S4P5.Text) & "</br>" & Sostituisci(S4P6.Text) & "</br>" & Sostituisci(S4P7.Text) & "</br>" & Sostituisci(S4P8.Text) & "</br>" & Sostituisci(S4P9.Text) & "</br>" & Sostituisci(S4P10.Text) & "</br>" & Sostituisci(S4P11.Text) & "</br>" & Sostituisci(S4P12.Text) & "</br>" & Sostituisci(S4P13.Text) & "</br>" & Sostituisci(S4P14.Text) & "</br>" & Sostituisci(S4P15.Text) & "</br>" & Sostituisci(S4P16.Text) & "</br>" & Sostituisci(S4P17.Text) & "</br>" & Sostituisci(S4P18.Text) & "</br>" & Sostituisci(S4P19.Text) & "</br>" & Sostituisci(S4P20.Text) & "</br>" & Sostituisci(S4P21.Text) & "</br>" & Sostituisci(S4P22.Text) & "</br>" & Sostituisci(S4P23.Text) & "</br>" & Sostituisci(S4P24.Text) & "</br>" & Sostituisci(S4P25.Text)
                ColDx = Sostituisci(S4P26.Text) & "</br>" & Sostituisci(S4P27.Text) & "</br>" & Sostituisci(S4P28.Text) & "</br>" & Sostituisci(S4P29.Text) & "</br>" & Sostituisci(S4P30.Text) & "</br>" & Sostituisci(S4P31.Text) & "</br>" & Sostituisci(S4P32.Text) & "</br>" & Sostituisci(S4P33.Text) & "</br>" & Sostituisci(S4P34.Text) & "</br>" & Sostituisci(S4P35.Text) & "</br>" & Sostituisci(S4P36.Text) & "</br>" & Sostituisci(S4P37.Text) & "</br>" & Sostituisci(S4P38.Text) & "</br>" & Sostituisci(S4P39.Text) & "</br>" & Sostituisci(S4P40.Text) & "</br>" & Sostituisci(S4P41.Text) & "</br>" & Sostituisci(S4P42.Text) & "</br>" & Sostituisci(S4P43.Text) & "</br>" & Sostituisci(S4P44.Text) & "</br>" & Sostituisci(S4P45.Text) & "</br>" & Sostituisci(S4P46.Text) & "</br>" & Sostituisci(S4P47.Text) & "</br>" & Sostituisci(S4P48.Text) & "</br>" & Sostituisci(S4P49.Text) & "</br>" & Sostituisci(S4P50.Text) & "</br>" & Sostituisci(S4P51.Text) & "</br>" & Sostituisci(S4P52.Text)
        End Select

        Contratto = Replace(Contratto, "$ColSX$", Replace(ColSx, vbCrLf, "</br>"))
        Contratto = Replace(Contratto, "$ColDX$", Replace(ColDx, vbCrLf, "</br>"))
        Contratto = Replace(Contratto, "$speseunitacondominio$", speseunitacondominio)

        'Contratto = Replace(Contratto, "$testorappresentato$", testorappresentato)
        'Contratto = Replace(Contratto, "$testorappresentato1$", testorappresentato1)
        Contratto = caricaRespFiliale(vIdContratto, Contratto)
        Return Contratto


    End Function

    Protected Sub imgConduttore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgConduttore.Click
        Try


            Dim contenuto As String = ""
            'Carica Modello HTML
            Select Case vTipo
                Case "61"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ContrattoCooperative.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "62"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\431POR.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "63"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\431SPEC.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "64"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\L43198Art15.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "65"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\L43198Art15_c2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "6"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\L43198S.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "392ASS"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\392ASS.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "BOX"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\PostoAuto.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "NEGOZI"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\NEGOZI.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "ERPB"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ERPModerato.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "12"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ContrattoConvenzionato.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "CONCESSIONE"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\CONCESSIONE_SPAZI.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case "COMODATO"
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ComodatoGratuito.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
                Case Else
                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\Contratto.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    contenuto = sr1.ReadToEnd()
                    sr1.Close()
            End Select


            CaricaDati()
            'MIO MEtodo SOSTITUZIONE VALORI
            Dim TestoContratto As String = SostSegnaposto(contenuto)

            '************ 11/09/2012 CODICE A BARRE ************
            'Dim PercorsoBarCode As String = PAR.RicavaBarCode(1, vIdContratto)
            'TestoContratto = Replace(TestoContratto, "$barcode$", "..\..\..\FileTemp\" & PercorsoBarCode)
            TestoContratto = Replace(TestoContratto, "IN DATA", "in data")
            TestoContratto = Replace(TestoContratto, " IN ", " in ")
            TestoContratto = Replace(TestoContratto, "AUTO ", "auto ")
            TestoContratto = Replace(TestoContratto, "AUTO,", "auto,")

            '************ FINE 11/09/2012 CODICE A BARRE ************
            'Scrivere il file in Contratti/stampeContratti

            'Visualizzare il file in una nuova finestra
            'Dim NomeFile As String = Mid(Request.QueryString("C"), 16, 50)
            Dim NomeFile As String = Mid(Request.QueryString("C"), 15, 50) & "_" & Format(Now, "yyyyMMddHHmmss")

            'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\cod_") & NomeFile & ".html", False, System.Text.Encoding.GetEncoding("UTF-8"))
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\cod_") & NomeFile & ".html", False, System.Text.Encoding.GetEncoding("UTF-8"))
            sr.WriteLine(TestoContratto)
            sr.Close()

            'Response.Write("<script>var f;f=window.open('../ALLEGATI/CONTRATTI/StampeContratti/cod_" & NomeFile & ".html" & "','Stampa','top=0,left=0,scrollbars=yes,width=800,height=600,resizable=yes,menubar=yes');f.focus();</script>")

            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If


            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 15
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False
            pdfConverter1.PdfDocumentOptions.EmbedFonts = True
            pdfConverter1.PdfDocumentOptions.FitWidth = False
            pdfConverter1.PageWidth = 750


            'pdfConverter1.SavePdfFromUrlToFile(Server.MapPath("..\FileTemp\cod_") & NomeFile & ".html", Server.MapPath("..\FileTemp\cod_") & NomeFile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(TestoContratto, Server.MapPath("..\FileTemp\cod_") & NomeFile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            System.IO.File.Copy(Server.MapPath("..\FileTemp\cod_") & NomeFile & ".pdf", Server.MapPath("..\ALLEGATI\CONTRATTI\StampeContratti\cod_") & NomeFile & ".pdf")
            System.IO.File.Delete(Server.MapPath("..\FileTemp\cod_") & NomeFile & ".html")
            Response.Write("<script>var f;f=window.open('../ALLEGATI/CONTRATTI/StampeContratti/cod_" & NomeFile & ".pdf" & "','Stampa','top=0,left=0,scrollbars=yes,width=800,height=600,resizable=yes,menubar=yes');f.focus();</script>")



        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:Copia Conduttore - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function PrimaGrande(ByVal testo As String) As String
        PrimaGrande = StrConv(testo, vbProperCase)
    End Function

    Private Function caricaRespFiliale(ByVal idContra As String, ByVal conten As String, Optional Tipo As Integer = 0) As String
        Try
            Dim Responsabile As String = ""
            Dim Acronimo As String = ""
            Dim dataPresent As String = ""
            Dim DataNascitaResp As String = ""
            Dim LuogoNascitaResp As String = ""

            Dim Percorso As String = "../" & Session.Item("Firme_Responsabili")

            Select Case Tipo
                Case 1
                    Percorso = "../" & Session.Item("Firme_Responsabili")
            End Select

            PAR.OracleConn.Open()
            PAR.SettaCommand(PAR)

            PAR.cmd.CommandText = "select cod_tipologia_contr_loc from siscom_mi.rapporti_utenza where id=" & idContra
            Dim tipoRU As String = PAR.IfNull(PAR.cmd.ExecuteScalar, "")

            If tipoRU = "USD" Then
                PAR.cmd.CommandText = "SELECT PROCURA,PROCURA1,RESPONSABILE,luogo_nascita_resp,data_nascita_resp,firma FROM siscom_mi.tab_filiali WHERE upper(responsabile)='DAVIDE FULGINI' and data_nascita_resp is not null"
            Else
                PAR.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr, indirizzi.civico,indirizzi.cap, indirizzi.localita FROM siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.FILIALI_UI WHERE unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto =" & idContra & " AND UNITA_IMMOBILIARI.ID = FILIALI_UI.ID_UI AND FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID AND indirizzi.ID = tab_filiali.id_indirizzo AND unita_immobiliari.ID = unita_contrattuale.id_unita AND INIZIO_VALIDITA <='" & Format(Now, "yyyyMMdd") & "' AND FINE_VALIDITA >= '" & Format(Now, "yyyyMMdd") & "' AND TAB_FILIALI.ID<>105"
            End If

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader
            If myReader.Read Then
                conten = Replace(conten, "$testorappresentato$", PAR.IfNull(myReader("PROCURA"), ""))
                conten = Replace(conten, "$testorappresentato1$", PAR.IfNull(myReader("PROCURA1"), ""))

                Responsabile = PAR.IfNull(myReader("RESPONSABILE"), "")
                conten = Replace(conten, "$responsabile$", PAR.IfNull(myReader("responsabile"), ""))
                conten = Replace(conten, "$luogonascitaresp$", PrimaGrande(PAR.IfNull(myReader("luogo_nascita_resp"), "")))
                conten = Replace(conten, "$datanascitaresp$", Format(CDate(PAR.IfNull(myReader("data_nascita_resp"), "")), "dd MMMM yyyy"))
                If PAR.IfNull(myReader("firma"), "") <> "" Then
                    conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & PAR.IfNull(myReader("firma"), "") & "' />")
                Else
                    conten = Replace(conten, "$firmaresponsabile$", "")
                End If
            Else
                PAR.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_contrattuale,siscom_mi.filiali_virtuali where filiali_virtuali.id_contratto=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & idContra & " and indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id=filiali_virtuali.id_filiale"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader
                If myReader2.Read Then
                    conten = Replace(conten, "$testorappresentato$", PAR.IfNull(myReader("PROCURA"), ""))
                    conten = Replace(conten, "$testorappresentato1$", PAR.IfNull(myReader("PROCURA1"), ""))
                    Responsabile = PAR.IfNull(myReader2("RESPONSABILE"), "")
                    conten = Replace(conten, "$responsabile$", PAR.IfNull(myReader2("responsabile"), ""))
                    conten = Replace(conten, "$luogonascitaresp$", PrimaGrande(PAR.IfNull(myReader("luogo_nascita_resp"), "")))
                    conten = Replace(conten, "$datanascitaresp$", Format(CDate(PAR.IfNull(myReader("data_nascita_resp"), "")), "dd MMMM yyyy"))
                    If PAR.IfNull(myReader2("firma"), "") <> "" Then
                        conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & PAR.IfNull(myReader("firma"), "") & "' />")
                    Else
                        conten = Replace(conten, "$firmaresponsabile$", "")
                    End If
                Else
                    Responsabile = ""
                    conten = Replace(conten, "$responsabile$", Responsabile)
                    conten = Replace(conten, "$firmaresponsabile$", "")
                    conten = Replace(conten, "$luogonascitaresp$", "")
                    conten = Replace(conten, "$datanascitaresp$", "")
                End If
                myReader2.Close()
            End If
            myReader.Close()

            conten = Replace(conten, "$responsabile$", "")
            conten = Replace(conten, "$firmaresponsabile$", "")
            conten = Replace(conten, "$luogonascitaresp$", "")
            conten = Replace(conten, "$datanascitaresp$", "")

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            conten = Replace(conten, "$responsabile$", "")
            conten = Replace(conten, "$firmaresponsabile$", "")
            conten = Replace(conten, "$luogonascitaresp$", "")
            conten = Replace(conten, "$datanascitaresp$", "")
            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return conten

    End Function



    Protected Sub imgAllegatoContratto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAllegatoContratto.Click
        Dim contenuto As String = ""

        'codcontratto

        CaricaDati()

        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\AllegatoRapporto.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        contenuto = sr1.ReadToEnd()
        sr1.Close()

        'MIO MEtodo SOSTITUZIONE VALORI
        Dim TestoContratto As String = Sostituisci(contenuto)
        TestoContratto = caricaRespFiliale(vIdContratto, TestoContratto, 1)

        Dim TIPOLOGIA As String = ""
        Dim ID_UNITA As String = ""
        Dim MODERATO As String = ""

        Try
            PAR.OracleConn.Open()
            par.SettaCommand(par)

            PAR.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_CONTRATTO='" & codcontratto & "'"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderA.Read Then
                TIPOLOGIA = PAR.IfNull(myReaderA("COD_TIPOLOGIA_CONTR_LOC"), "")
                ID_UNITA = PAR.IfNull(myReaderA("ID_UNITA"), "-1")
                If PAR.IfNull(myReaderA("provenienza_Ass"), "") = 6 And PAR.IfNull(myReaderA("dest_uso"), "") = "D" Then
                    TIPOLOGIA = "ART15"
                End If
            End If
            myReaderA.Close()

            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_UI WHERE ID=" & ID_UNITA & " ORDER BY DATA DESC"
            myReaderA = PAR.cmd.ExecuteReader()
            If myReaderA.Read Then
                MODERATO = "1"
            End If
            myReaderA.Close()

            If Mid(TIPOLOGIA, 1, 3) = "ERP" And MODERATO = "1" Then
                TIPOLOGIA = "L431/98"
            End If

            Dim firma As String = ""


            'If vTipo = "3" Or vTipo = "BOX" Or vTipo = "NEGOZI" Then
            '    firma = "<br /><span style='font-family: ARIAL; font-size: 12px; font-weight: bold'>Il responsabile dell&#39;Ufficio<br />&nbsp;&nbsp;&nbsp;&nbsp;CRISTINA GIUNTOLI</span>"
            'Else
            '    firma = "<img alt='' src='FirmaGiordano1.png' style='height: 71px; width: 233px' />"
            'End If

            Select Case Mid(TIPOLOGIA, 1, 3)
                Case "L43", "392"

                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=86"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=87"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = firma & " " & PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    firma = "<br /><span style='font-family: ARIAL; font-size: 12px; font-weight: bold'>Il responsabile dell&#39;Ufficio<br />&nbsp;&nbsp;&nbsp;&nbsp;" & firma & "</span>"


                Case "EQC"


                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=106"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=107"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = firma & " " & PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    firma = "<br /><span style='font-family: ARIAL; font-size: 12px; font-weight: bold'>Il responsabile dell&#39;Ufficio<br />&nbsp;&nbsp;&nbsp;&nbsp;" & firma & "</span>"


                Case "USD"
                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=96"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=97"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = firma & " " & PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    firma = "<br /><span style='font-family: ARIAL; font-size: 12px; font-weight: bold'>Il responsabile dell&#39;Ufficio<br />&nbsp;&nbsp;&nbsp;&nbsp;" & firma & "</span>"



                Case Else
                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=68"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    PAR.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=69"
                    myReaderA = PAR.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        firma = firma & " " & PAR.IfNull(myReaderA("VALORE"), "")
                    End If
                    myReaderA.Close()

                    'firma = "<img alt='' src='../IMG/FirmaGiordano.gif' style='height: 71px; width: 233px' />"

                    '04/07/2012 Nuova FIRMA
                    firma = "Il responsabile della sede territoriale"

            End Select

            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            TestoContratto = Replace(TestoContratto, "$firma$", firma)
            TestoContratto = Replace(TestoContratto, "$decorrenzacontratto$", decorrenzacontratto)
            TestoContratto = Replace(TestoContratto, "$intestatariocontratto$", intestatariocontratto)
            TestoContratto = Replace(TestoContratto, "$intestatario1$", intestatariocontratto1)


            TestoContratto = Replace(TestoContratto, "$dataoggi$", Format(Now, "dd/MM/yyyy"))

            Dim PercorsoBarCode As String = PAR.RicavaBarCode(20, vIdContratto)
            TestoContratto = Replace(TestoContratto, "$barcode$", Server.MapPath("..\FileTemp\") & PercorsoBarCode)


            'Dim NomeFile As String = "Allegato_Contratto_" & codcontratto & "_" & Format(Now, "yyyyMMddhhmmss")
            Dim NomeFile As String = Format(Now, "yyyyMMdd") & "_004_" & codcontratto

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\" & NomeFile & ".html"), False, System.Text.Encoding.GetEncoding("iso-8859-1"))
            sr.WriteLine(TestoContratto)
            sr.Close()




            Dim pdfConverter1 As PdfConverter = New PdfConverter


            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False


            'pdfConverter1.SavePdfFromHtmlStringToFile(TestoContratto, Server.MapPath("..\FileTemp\") & NomeFile & ".pdf", Server.MapPath("..\IMG\"))
            'pdfConverter1.SavePdfFromHtmlFileToFile(Server.MapPath("..\FileTemp\") & NomeFile & ".html", Server.MapPath("..\FileTemp\") & NomeFile & ".pdf")

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(TestoContratto, Server.MapPath("..\FileTemp\") & NomeFile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String


            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\") & NomeFile & ".zip"

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\") & NomeFile & ".pdf"
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
            Response.Write("<script>var f;f=window.open('../FileTemp/" & NomeFile & ".pdf','Allegato','');f.focus();</script>")


        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try



    End Sub

    Protected Sub imgAnnullaStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAnnullaStampa.Click


        If txtAttiva.Value = "0" Then
            Try
                PAR.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


                PAR.cmd.CommandText = "select bozza from siscom_mi.rapporti_utenza where id=" & vIdContratto
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderA.Read Then
                    If PAR.IfNull(myReaderA("bozza"), "") = "0" Then
                        myReaderA.Close()
                        Response.Write("<script>alert('Attenzione, questa operazione può essere effettuata solo se il contratto è in BOZZA!');</script>")
                        Exit Sub
                    Else

                    End If
                End If
                myReaderA.Close()

                PAR.cmd.CommandText = "DELETE FROM SISCOM_MI.XML_CONTRATTI WHERE ID_CONTRATTO=" & vIdContratto
                PAR.cmd.ExecuteNonQuery()

                PAR.cmd.CommandText = "DELETE FROM SISCOM_MI.RAPPORTI_UTENZA_REGISTRAZIONE WHERE ID_CONTRATTO=" & vIdContratto
                PAR.cmd.ExecuteNonQuery()

                'max 08/01/2015 storno automatico bollette attivazione
                PAR.cmd.CommandText = "select * FROM SISCOM_MI.BOL_BOLLETTE WHERE (id_tipo=10 or id_tipo=9 or id_tipo=1) and id_bolletta_storno is null and fl_annullata='0' AND ID_CONTRATTO=" & vIdContratto
                Dim myReaderAn As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myReaderAn.Read
                    If StornaBolletta(myReaderAn("id"), "STORNO BOLLETTINO PER ANNULLO STAMPA CONTRATTO") = False Then
                        PAR.myTrans.Rollback()
                        PAR.myTrans = PAR.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, PAR.myTrans)
                        LBLcONTRATTO.Text = "Anomalia in fase di storno bollettino n. " & myReaderAn("id") & " - OPERAZIONE ANNULLATA!"
                        myReaderAn.Close()
                        Exit Sub
                    End If
                Loop
                myReaderAn.Close()

                '------

                'PAR.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA='1',DATA_ANNULLO='" & Format(Now, "yyyyMMdd") & "' WHERE (RIF_FILE='MOD' OR RIF_FILE='MAV') AND ID_CONTRATTO=" & vIdContratto
                'PAR.cmd.ExecuteNonQuery()

                'PAR.cmd.CommandText = "select * FROM SISCOM_MI.BOL_BOLLETTE WHERE (RIF_FILE='MOD' OR RIF_FILE='MAV') AND ID_CONTRATTO=" & vIdContratto
                'Dim myReaderAn As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                'Do While myReaderAn.Read
                '    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                '           & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                '           & "'F07','BOLL. NUMERO " & myReaderAn("ID") & " A SEGUITO DI ANNULLO STAMPA CONTRATTO')"
                '    PAR.cmd.ExecuteNonQuery()
                'Loop
                'myReaderAn.Close()


                PAR.cmd.CommandText = "DELETE FROM SISCOM_MI.INTESTATARI_RAPPORTO WHERE ID_CONTRATTO=" & vIdContratto
                PAR.cmd.ExecuteNonQuery()

                PAR.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET FL_STAMPATO='0' WHERE ID=" & vIdContratto
                PAR.cmd.ExecuteNonQuery()

                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F30','')"
                PAR.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione Effettuata! Premere il pulsante SALVA del rapporto per rendere effettive le modifiche e aggiornare i dati!');window.opener.document.forms['form1'].elements['txtModificato'].value = '1';self.close();</script>")


            Catch ex As Exception

            End Try
        End If
    End Sub

    Function StornaBolletta(ByVal idBolletta As String, ByVal motivoStorno As String, Optional parziale As Boolean = False) As Boolean
        StornaBolletta = True
        Try
            Dim pagata As Boolean = False
            Dim dataPagamento As String = ""
            Dim dataValuta As String = ""
            PAR.cmd.CommandText = "SELECT * from SISCOM_MI.BOL_BOLLETTE where ID=" & idBolletta
            Dim daBolStorno As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
            Dim dtStorno As New Data.DataTable
            daBolStorno.Fill(dtStorno)
            daBolStorno.Dispose()
            If dtStorno.Rows.Count > 0 Then
                If PAR.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0 Or (PAR.IfNull(dtStorno.Rows(0).Item("FL_ANNULLATA"), 0) <> 0 And PAR.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0) Then
                    pagata = True
                    dataPagamento = PAR.IfNull(dtStorno.Rows(0).Item("DATA_PAGAMENTO"), "")
                    dataValuta = PAR.IfNull(dtStorno.Rows(0).Item("DATA_VALUTA"), "")
                Else
                    pagata = False
                    parziale = False ' SE NON è PAGATA, è INUTILE CERCARE DI FARE LO STORNO PARZIALE
                    dataPagamento = Format(Now, "yyyyMMdd")
                    dataValuta = Format(Now, "yyyyMMdd")
                End If
            Else
                'id bolletta passato non valido
                StornaBolletta = False
                Exit Function
            End If
            'ricava id Anagrafica
            Dim idAnagr As Long = 0
            Dim intestatario As String = ""
            PAR.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS INTEST  FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE " _
                            & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=" & PAR.IfEmpty(dtStorno.Rows(0).Item("ID_CONTRATTO").ToString, "0") & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
            Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader
            If lettoreDati.Read Then
                idAnagr = PAR.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
                intestatario = PAR.IfNull(lettoreDati("INTEST"), "")
            End If
            lettoreDati.Close()

            Dim dataAttuale As String = ""
            Dim dataInizioCompet As String = ""
            Dim dataFineCompet As String = ""
            dataAttuale = Format(Now, "dd/MM/yyyy")
            If dataAttuale <> "" Then
                dataInizioCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & "01"
                dataFineCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & DateTime.DaysInMonth(Right(dataAttuale, 4), dataAttuale.Substring(3, 2))
            End If


            Dim NumBOlletta As String = dtStorno.Rows(0).Item("num_bolletta").ToString
            Dim noteBolletta As String = motivoStorno & " NUM. BOLLETTA STORNATA:" & NumBOlletta

            Dim pagataParz As Boolean = False
            Dim ID_BOLLETTA_STORNO As Integer = 0

            'STORNA BOLLETTA SELEZIONATA
            If pagata = True Then
                If parziale = False Then
                    Dim importoTot As Decimal = 0
                    importoTot = PAR.IfNull(dtStorno.Rows(0).Item("IMPORTO_TOTALE"), 0)
                    'se pagata parzialmente viene creata l'eccedenza per l'importo pagato
                    If PAR.IfNull(dtStorno.Rows(0).Item("IMPORTO_TOTALE"), 0) > PAR.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0) Then
                        importoTot = PAR.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0)
                        pagataParz = True
                    End If

                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                                & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                                & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & PAR.IfEmpty(dtStorno.Rows(0).Item("ID_CONTRATTO").ToString, "0") & "," & PAR.RicavaEsercizioCorrente() & "," & dtStorno.Rows(0).Item("ID_UNITA") & "," & idAnagr & ",'" & dataInizioCompet & "','" & dataFineCompet & "'," & PAR.VirgoleInPunti(importoTot * -1) & "," _
                                & "'" & Format(Now, "yyyyMMdd") & "','" & dtStorno.Rows(0).Item("DATA_PAGAMENTO") & "','" & dtStorno.Rows(0).Item("DATA_VALUTA") & "',4,'N',NULL,'ECCEDENZA PER PAGAMENTO BOLLETTA STORNATA " & dtStorno.Rows(0).Item("NUM_BOLLETTA") & "')"
                    PAR.cmd.ExecuteNonQuery()

                    Dim idBollGest As Long = 0
                    PAR.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idBollGest = myReader(0)
                    End If
                    myReader.Close()

                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                                & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",712," & PAR.VirgoleInPunti(importoTot * -1) & ")"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & PAR.IfEmpty(dtStorno.Rows(0).Item("ID_CONTRATTO").ToString, "0") & "," & Session("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F204','IMPORTO PARI A EURO " & Format(importoTot, "##,##0.00") & "')"
                    PAR.cmd.ExecuteNonQuery()
                Else
                    pagataParz = True
                End If

            End If


            PAR.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL FROM DUAL"
            Dim myReaderST As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderST.Read Then
                ID_BOLLETTA_STORNO = myReaderST(0)
            End If
            myReaderST.Close()


            For Each row As Data.DataRow In dtStorno.Rows


                PAR.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(" & ID_BOLLETTA_STORNO & ", 999, '" & Format(Now, "yyyyMMdd") _
                        & "', '" & Format(Now, "yyyyMMdd") & "', NULL,NULL,NULL,'" & PAR.PulisciStrSql(noteBolletta) & "'," _
                        & "" & PAR.IfNull(row.Item("ID_CONTRATTO"), 0) _
                        & " ," & PAR.RicavaEsercizioCorrente() & ", " _
                        & PAR.IfNull(row.Item("ID_UNITA"), 0) _
                        & ", '0', '" & PAR.PulisciStrSql(PAR.IfNull(row.Item("PAGABILE_PRESSO"), "")) & "', " & PAR.IfNull(row.Item("COD_AFFITTUARIO"), 0) & "" _
                        & ", '" & PAR.PulisciStrSql(intestatario).ToUpper & "', " _
                        & "'" & PAR.PulisciStrSql(PAR.IfNull(row.Item("INDIRIZZO"), "")) _
                        & "', '" & PAR.PulisciStrSql(PAR.IfNull(row.Item("CAP_CITTA"), "")) _
                        & "', '" & PAR.PulisciStrSql(PAR.IfNull(row.Item("PRESSO"), "")) & "', '" & PAR.IfNull(row.Item("RIFERIMENTO_DA"), "") _
                        & "', '" & PAR.IfNull(row.Item("RIFERIMENTO_A"), "") & "', " _
                        & "'1', " & PAR.IfNull(row.Item("ID_COMPLESSO"), 0) & ", '', '', " _
                        & Year(Now) & ", '', " & PAR.IfNull(row.Item("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD',22)"
                PAR.cmd.ExecuteNonQuery()
            Next



            Dim ID_VOCE_STORNO As Long = 0
            'Dim SumImportoVOCI As Decimal = 0
            PAR.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA = " & idBolletta


            Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
            Dim dtBVoci As New Data.DataTable
            daBVoci.Fill(dtBVoci)
            daBVoci.Dispose()
            Dim ImpVoceBolStorno As Decimal = 0
            For Each row As Data.DataRow In dtBVoci.Rows
                ImpVoceBolStorno = 0
                PAR.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.nextval FROM DUAL"
                Dim myReaderIDV As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderIDV.Read Then
                    ID_VOCE_STORNO = myReaderIDV(0)
                End If
                myReaderIDV.Close()
                If parziale = False Then
                    ImpVoceBolStorno = PAR.IfNull(row.Item("IMPORTO"), 0)

                ElseIf parziale = True Then
                    ImpVoceBolStorno = PAR.IfNull(row.Item("IMPORTO"), 0) - PAR.IfNull(row.Item("imp_pagato"), 0)

                End If
                PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI ( ID, ID_BOLLETTA, ID_VOCE, IMPORTO, NOTE, IMP_PAGATO_OLD," _
                                & "IMP_PAGATO_BAK, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO" _
                                & ") " _
                                & "VALUES ( " & ID_VOCE_STORNO & ", " & ID_BOLLETTA_STORNO & ", " & row.Item("ID_VOCE") & ", " & PAR.VirgoleInPunti(ImpVoceBolStorno * -1) & ",'STORNO'," _
                                & PAR.VirgoleInPunti(PAR.IfNull(row.Item("IMP_PAGATO_OLD"), 0)) & ", " & PAR.VirgoleInPunti(PAR.IfNull(row.Item("IMP_PAGATO_BAK"), 0)) & ", " & PAR.VirgoleInPunti(PAR.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), 0)) & ", " & PAR.VirgoleInPunti(PAR.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), 0)) & "" _
                                & ")"
                PAR.cmd.ExecuteNonQuery()


                PAR.cmd.CommandText = "UPDATE SISCOM_MI.bol_bollette_voci set IMP_PAGATO=" & PAR.VirgoleInPunti(ImpVoceBolStorno * -1) & " WHERE ID=" & ID_VOCE_STORNO
                PAR.cmd.ExecuteNonQuery()

                'If parziale = False Then
                PAR.cmd.CommandText = "UPDATE SISCOM_MI.bol_bollette_voci set IMP_PAGATO=" & PAR.VirgoleInPunti(PAR.IfNull(row.Item("IMPORTO"), 0)) & " WHERE ID=" & row.Item("ID")
                PAR.cmd.ExecuteNonQuery()
                'End If

                'SumImportoVOCI = SumImportoVOCI + IfNull(row.Item("IMPORTO"), 0)
            Next



            PAR.cmd.CommandText = "UPDATE SISCOM_MI.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_STORNO & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & idBolletta
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE SISCOM_MI.bol_bollette set DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & ID_BOLLETTA_STORNO
            PAR.cmd.ExecuteNonQuery()

            Dim noteEvento As String = ""
            noteEvento = noteBolletta
            If pagata = True Then
                If pagataParz = True Then
                    noteEvento &= "(parzialm. pagata)"
                Else
                    noteEvento &= "(precedentam. pagata)"
                End If
            Else
                noteEvento &= "(non precedentem. pagata)"
            End If

            PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & dtStorno.Rows(0).Item("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F203','" & noteBolletta & "')"
            PAR.cmd.ExecuteNonQuery()


        Catch ex As Exception
            StornaBolletta = False
        End Try
    End Function

End Class
