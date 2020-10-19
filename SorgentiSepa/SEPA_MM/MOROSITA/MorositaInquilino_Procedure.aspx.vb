'MOROSITA' INQUILINO - PROCEDURE

Imports System.Collections
Imports System.Data.OleDb
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Imports System.Drawing
Imports System.Math
'12/01/2015 PUCCIA Nuova connessione  tls ssl
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security



Partial Class MorositaInquilino_Procedure
    Inherits PageSetIdMode
    Dim par As New CM.Global

    '12/01/2015 PUCCIA Nuova connessione  tls ssl
    Private Shared Function CertificateHandler(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal SSLerror As SslPolicyErrors) As Boolean
        Return True
    End Function
    'Public listMG As New System.Collections.Generic.SortedList(Of String, String)
    'Public listMA As New System.Collections.Generic.SortedList(Of String, String)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        Try
            Response.Expires = 0


            'Dim Str As String

            'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            'Str = Str & "<" & "/div>"

            'Response.Write(Str)


            If Not IsPostBack Then

                'Response.Flush()

                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                    Exit Sub
                End If


                'CONNESSIONE
                If Request.QueryString("IDCON") <> "" Then
                    IdConnessione = Request.QueryString("IDCON")
                    Me.txtIdConnessione.Value = IdConnessione
                End If

                'MOROSITA
                vIdMorosita = 0
                If Request.QueryString("ID_MOROSITA") <> "" Then
                    vIdMorosita = Request.QueryString("ID_MOROSITA")
                End If

                'MOROSITA LETTERE
                vIdMorositaLettera = 0
                If Request.QueryString("ID_MOR_LETTERA") <> "" Then
                    vIdMorositaLettera = Request.QueryString("ID_MOR_LETTERA")
                End If


                'CONTRATTO
                vIdContratto = 0
                If Request.QueryString("ID_CONTRATTO") <> "" Then
                    vIdContratto = Request.QueryString("ID_CONTRATTO")
                End If


                'Me.Session.Add("MODIFYMODAL", 0)
                Me.txtModificato.Value = "0"

                SettaScelte(0)

                ' CONNESSIONE DB
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                End If
                ''''''''''''''''''''''''''

            End If

            Me.txtIdMA.Value = ""
            Me.txtIdMG.Value = ""

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



            If Request.QueryString("IDVISUAL") = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Public Property IdConnessione() As String
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
            If Not (ViewState("par_IdMorosita") Is Nothing) Then
                Return CLng(ViewState("par_IdMorosita"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdMorosita") = value
        End Set

    End Property

    Public Property vIdMorositaLettera() As Long
        Get
            If Not (ViewState("par_IdMorositaLettera") Is Nothing) Then
                Return CLng(ViewState("par_IdMorositaLettera"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdMorositaLettera") = value
        End Set

    End Property

    Public Property vIdContratto() As Long
        Get
            If Not (ViewState("par_idContratto") Is Nothing) Then
                Return CLng(ViewState("par_idContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idContratto") = value
        End Set

    End Property



    Sub SettaScelte(ByVal provenienza As Integer)
        Dim FlagConnessione As Boolean
        Dim sScelte As String = ""

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            'CARICAMENTO DELLE COMBO BOX UTILIZZATE NELLE VARIE SOTTO MASCHERE

            ' ELENCO ESITI (TIPOLOGIA_ESITI_POSTALER) utilizzato nell'inserimento MANUALE dell'esito PostAler
            Me.cmbElencoEsiti.Items.Clear()
            Me.cmbElencoEsiti.Items.Add(New ListItem(" ", -1))

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_ESITI_POSTALER order by DESCRIZIONE asc "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbElencoEsiti.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), ""), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '****************************************


            '1) Select  dell 'ultimo evento inserito MOROSITA_EVENTI
            '2) in base allo stato, faccio apparire le varie condizioni

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select SISCOM_MI.MOROSITA_EVENTI.*,SISCOM_MI.TAB_EVENTI_MOROSITA.FUNZIONE as ""FUNZIONE1"",SISCOM_MI.MOROSITA_LETTERE.COD_STATO as STATO_LETTERA " _
                                & " from SISCOM_MI.MOROSITA_EVENTI,SISCOM_MI.TAB_EVENTI_MOROSITA,SISCOM_MI.MOROSITA_LETTERE " _
                                & " where ID_MOROSITA_LETTERE=" & vIdMorositaLettera _
                                & "   and SISCOM_MI.MOROSITA_LETTERE.ID=" & vIdMorositaLettera _
                                & "   and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
                                & " order by MOROSITA_EVENTI.ID DESC"

            myReader1 = par.cmd.ExecuteReader()

            Me.RBList1.Items.Clear()

            Me.lbl_Opzioni.Visible = False
            Me.lbl_Opzioni_Contenuto.Visible = False
            Me.lbl_Opzioni_Contenuto.Text = ""

            If myReader1.Read Then
                Me.txtDescrizioneM.Text = par.IfNull(myReader1("MOTIVAZIONE"), "")

                If par.IfNull(myReader1("STATO_LETTERA"), "") = "" Or par.IfNull(myReader1("STATO_LETTERA"), "") = "M94" Then
                    Me.txtDescrizioneM.Text = "MAV ERRATO"
                    sScelte = 94
                Else

                    sScelte = par.IfNull(myReader1("FUNZIONE1"), "")

                    'SE nel frattempo qualcuno ha pagato un bollettino presente in uno dei 2 MAV
                    If Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG" Then
                        If par.IfNull(myReader1("FUNZIONE1"), "") = 0 Then
                            'Se il pagamento è avvenuto prima ancora che l'inquilino abbia ricevuto il mav o prima dell'arrivo del postaler
                            sScelte = 97
                        End If
                    End If
                End If

                Select Case sScelte
                    Case Epifani.TabEventiMorosita.MESSA_IN_MORA
                        'Me.ImgProcedi.Visible = False

                        'RBList1.Items.Add(New ListItem("In attesa della ricevuta PostAler", Epifani.TabEventiMorosita.MESSA_IN_MORA))
                        'RBList1.Visible = False

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " In attesa della ricevuta PostAler"

                        RBList1.Items.Add(New ListItem("Aggiornamento Manuale dell'esito PostAler", Convert.ToInt32(Epifani.TabEventiMorosita.AGG_POSTALER_MANUALENTE)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.MOROSITA_ANNULLATA
                        Me.ImgProcedi.Visible = False

                        Me.RBList1.Visible = False

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " Morosità Annullata"

                    Case Epifani.TabEventiMorosita.POSTALER_RITIRATA_DAL_DESTINATARIO
                        'DOPO che è stata RITIRATA dal DESTINATORIO si può:
                        '1) Richiedere una Dilazione 
                        '2) Richiedere una Sospensione
                        '3) Aggiornare la situazione (in caso l'iquilino dimostri di aver pagato tutto o in parte le bollette messe in mora

                        RBList1.Items.Add(New ListItem("Richiesta Dilazione", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE)))
                        RBList1.Items.Add(New ListItem("Richiesta Sospensione", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ALTRO)))
                        RBList1.Items.Add(New ListItem("Aggiornamento Situazione", Convert.ToInt32(Epifani.TabEventiMorosita.AGGIORNAMENTO_SITUAZIONE)))

                        'SE nel frattempo qualcuno ha pagato un bollettino presente in uno dei 2 MAV
                        If Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG" Then
                            RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))
                        End If


                    Case Epifani.TabEventiMorosita.DILAZIONE
                        'DOPO che è stata FATTA una DILAZIONE si può:
                        '1) Richiedere una altra Dilazione 
                        '2) Richiedere una Sospensione
                        '3) Aggiornare la situazione (in caso l'iquilino dimostri di aver pagato tutto o in parte le bollette messe in mora

                        RBList1.Items.Add(New ListItem("Richiesta Dilazione", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE)))
                        RBList1.Items.Add(New ListItem("Richiesta Sospensione", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ALTRO)))
                        RBList1.Items.Add(New ListItem("Aggiornamento Situazione", Convert.ToInt32(Epifani.TabEventiMorosita.AGGIORNAMENTO_SITUAZIONE)))

                        'SE nel frattempo qualcuno ha pagato un bollettino presente in uno dei 2 MAV
                        If Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG" Then
                            RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))
                        End If

                    Case Epifani.TabEventiMorosita.SOSPENSIONE_REVISIONE_CANONE To Epifani.TabEventiMorosita.SOSPENSIONE_ALTRO

                        'DOPO che è stata FATTA una SOSPENSIONE si può:
                        '1) Solo ANNULLARE la sospensione per continuare con gli altri eventi della morosità


                        RBList1.Items.Add(New ListItem("Annulla Sospensione ", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ANNULLATA)))
                        RBList1.Items(0).Selected = True


                    Case Epifani.TabEventiMorosita.SOSPENSIONE_ANNULLATA
                        'DOPO che è stata ANNULLATA una SOSPENSIONE si può:
                        '1) Richiedere una Dilazione 
                        '2) Richiedere una altra Sospensione
                        '3) Aggiornare la situazione (in caso l'iquilino dimostri di aver pagato tutto o in parte le bollette messe in mora

                        RBList1.Items.Add(New ListItem("Richiesta Dilazione", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE)))
                        RBList1.Items.Add(New ListItem("Richiesta Sospensione", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ALTRO)))
                        RBList1.Items.Add(New ListItem("Aggiornamento Situazione", Convert.ToInt32(Epifani.TabEventiMorosita.AGGIORNAMENTO_SITUAZIONE)))

                        'SE nel frattempo qualcuno ha pagato un bollettino presente in uno dei 2 MAV
                        If Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG" Then
                            RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))
                        End If

                    Case Epifani.TabEventiMorosita.STRAGIUDIZIALE_AFFIDAMENTO_LEGALE
                        'DOPO che è stata AFFIDATA la pratica ad UN LEGALE, si può (per il momento):

                        '1) Aggiornare la situazione (in caso l'iquilino dimostri di aver pagato tutto o in parte le bollette messe in mora

                        RBList1.Items.Add(New ListItem("Aggiornamento Situazione", Convert.ToInt32(Epifani.TabEventiMorosita.AGGIORNAMENTO_SITUAZIONE)))

                        'SE nel frattempo qualcuno ha pagato un bollettino presente in uno dei 2 MAV
                        If Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG" Then
                            RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))
                        End If

                    Case Epifani.TabEventiMorosita.MAV_AGGIORNATO
                        'DOPO che è stata AGGIORNATO il MAV, si può:
                        '1) Richiedere una Dilazione 
                        '2) Richiedere una altra Sospensione
                        '3) Aggiornare la situazione (in caso l'iquilino dimostri di aver pagato tutto o in parte le bollette messe in mora

                        RBList1.Items.Add(New ListItem("Richiesta Dilazione", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE)))
                        RBList1.Items.Add(New ListItem("Richiesta Sospensione", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ALTRO)))
                        RBList1.Items.Add(New ListItem("Aggiornamento Situazione", Convert.ToInt32(Epifani.TabEventiMorosita.AGGIORNAMENTO_SITUAZIONE)))

                        'SE nel frattempo qualcuno ha pagato un bollettino presente in uno dei 2 MAV
                        If Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG" Then
                            RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))
                        End If


                    Case Epifani.TabEventiMorosita.RATEIZZAZIONE
                        Me.ImgProcedi.Visible = False

                        Me.RBList1.Visible = False

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " Reteizzazione in corso"

                    Case Epifani.TabEventiMorosita.MOROSITA_CONCLUSA
                        Me.ImgProcedi.Visible = False

                        Me.RBList1.Visible = False

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " Morosità Conclusa"

                    Case 97 'DEBITO AGGIORNATO

                        RBList1.Items.Add(New ListItem("Aggiornamento Manuale dell'esito PostAler", Convert.ToInt32(Epifani.TabEventiMorosita.AGG_POSTALER_MANUALENTE)))
                        RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))


                        ' ESITI DA ANALIZZARE e FARE
                    Case Epifani.TabEventiMorosita.POSTALER_INDIRIZZO_INSUFFICIENTE
                        Me.ImgProcedi.Visible = True

                        Me.RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Indirizzo insufficiente "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True



                    Case Epifani.TabEventiMorosita.POSTALER_RECAPITATA_IN_SLA
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Recapitata in SLA "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_COMPIUTA_GIACENZA
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Compiuta giacenza "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_ASSENTE_INIZIO_GIACENZA
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Assente inizio giacenza "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_SCONOSCIUTO
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Sconosciuto "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_TRASFERITO
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Trasferito "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_CIVICO_INESISTENTE
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Civico inesistente "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_DECEDUTO
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Deceduto "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_RESPINTO
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Respinto "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_VIA_INESISTENTE
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Via inesistente"

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_RECAPITATA_IN_SLA
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Impossibile recapito "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                    Case Epifani.TabEventiMorosita.POSTALER_POSTALIZZATA
                        Me.ImgProcedi.Visible = True

                        RBList1.Visible = True

                        Me.lbl_Opzioni.Visible = True
                        Me.lbl_Opzioni_Contenuto.Visible = True
                        Me.lbl_Opzioni_Contenuto.Text = " RICEVUTA POSTALER: Postalizzata "

                        RBList1.Items.Add(New ListItem("Annulla MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_MAV_LETTERA)))
                        RBList1.Items(0).Selected = True

                        'DOPO ANNULLA e RIGENERA MAV
                    Case Epifani.TabEventiMorosita.MAV_ANNULLATO_RIGENERATO

                        Dim EffettuatoEsito As Boolean = False
                        'CONTROLLO SE EFFETTUATO un ESITO MANUALE o automatico di POSTALER precedentemente
                        par.cmd.CommandText = " select count(*) from SISCOM_MI.MOROSITA_EVENTI " _
                                            & " where ID_MOROSITA_LETTERE=" & vIdMorositaLettera _
                                            & "  and COD_EVENTO in (select COD from SISCOM_MI.TAB_EVENTI_MOROSITA where FUNZIONE>0 and FUNZIONE <=13)"

                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            If myReader2(0) > 1 Then
                                EffettuatoEsito = True
                            End If
                        End If
                        myReader2.Close()

                        If EffettuatoEsito = False Then
                            Me.lbl_Opzioni.Visible = True
                            Me.lbl_Opzioni_Contenuto.Visible = True
                            Me.lbl_Opzioni_Contenuto.Text = " In attesa della ricevuta PostAler"

                            If (Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG") And provenienza = 0 Then
                                RBList1.Items.Add(New ListItem("Aggiornamento Manuale dell'esito PostAler", Convert.ToInt32(Epifani.TabEventiMorosita.AGG_POSTALER_MANUALENTE)))
                                RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))

                            Else
                                RBList1.Items.Add(New ListItem("Aggiornamento Manuale dell'esito PostAler", Convert.ToInt32(Epifani.TabEventiMorosita.AGG_POSTALER_MANUALENTE)))
                                RBList1.Items(0).Selected = True
                            End If

                        Else
                            RBList1.Items.Add(New ListItem("Richiesta Dilazione", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE)))
                            RBList1.Items.Add(New ListItem("Richiesta Sospensione", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ALTRO)))
                            RBList1.Items.Add(New ListItem("Aggiornamento Situazione", Convert.ToInt32(Epifani.TabEventiMorosita.AGGIORNAMENTO_SITUAZIONE)))

                            If Request.QueryString("FUNZIONE_1") = "97_MA" Or Request.QueryString("FUNZIONE_1") = "97_MG" And provenienza = 0 Then
                                RBList1.Items.Add(New ListItem("Annulla e Rigenera MAV", Convert.ToInt32(Epifani.TabEventiMorosita.ANNULLA_RIGENERA_MAV)))
                            End If

                        End If


                        'DOPO ANNULLA e RIGENERA MAV
                    Case Epifani.TabEventiMorosita.MAV_ERRATO

                        If provenienza = 1 Then
                            Me.ImgProcedi.Visible = False

                            RBList1.Visible = False

                            Me.lbl_Opzioni.Visible = True
                            Me.lbl_Opzioni_Contenuto.Visible = True
                            Me.lbl_Opzioni_Contenuto.Text = " Uscire dalla finestra per aggiornare la situazione della morosità"

                        Else
                            RBList1.Items.Add(New ListItem("Creazione MAV", Convert.ToInt32(Epifani.TabEventiMorosita.MAV_ERRATO)))
                            RBList1.Items(0).Selected = True
                        End If

                End Select
            End If
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


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
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


    Private Sub FrmSolaLettura()

        'Me.btnAgg1.Visible = False
        'Me.btnAggRimborso.Visible = False

        'Me.btnElimina1.Visible = False
        'Me.btnApri1.Visible = False

        Dim CTRL As Control = Nothing
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next

        Me.txtIdMA.Value = ""
        Me.txtIdMG.Value = ""

    End Sub



    Protected Sub ImgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEsci.Click

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "1"
        Response.Write("<script>window.close();</script>")

    End Sub

   

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click


        'DILAZIONE *************************************
        If Me.txtAppareDilazione.Value = 1 Then

            If Request.QueryString("CONTA_MAV") = "3" Then

                AbilitaGruppo(1, False, True)
                AbilitaGruppo(2, True, False)

            ElseIf Request.QueryString("CONTA_MAV") = "2" Then

                AbilitaGruppo(1, True, False)
                AbilitaGruppo(2, False, True)
            Else

                AbilitaGruppo(1, False, False)
                AbilitaGruppo(2, False, True)

            End If

            RiempiCampiDilazione()

        End If
        '************************************************


        'SOSPENSIONE ************************************
        If Me.txtAppareSospensione.Value = 1 Then
            If Request.QueryString("CONTA_MAV") = "3" Then
                Me.chkSospensione.Visible = True
                Me.chkSospensione.Text = "Sospensione valida anche per il M.A.V. Gestore"

            ElseIf Request.QueryString("CONTA_MAV") = "2" Then
                Me.chkSospensione.Visible = True
                Me.chkSospensione.Text = "Sospensione valida anche per il M.A.V. Global Service"
            Else
                Me.chkSospensione.Visible = False
            End If
        End If
        '************************************************


        'ANNULLO SOSPENSIONE ****************************
        If Me.txtAnnullo.Value = 1 Then
            AnnullaSospensione()
        End If
        '************************************************


        'AGGIORNAMENTO SITUAZIONE ***********************
        If Me.txtApparePagamento.Value = 1 Then
            CaricaPagamenti()
        End If
        '************************************************


        'ANNULLA / RIGENERA MAV ************************
        If Me.txtRigeneraMAV.Value = 1 Then
            Dim ID_BOLLETTE_NUOVE As String = ""

            If Request.QueryString("FUNZIONE_1") = "97_MA" Then
                ID_BOLLETTE_NUOVE = CreaMAV(False, True, Convert.ToInt32(Epifani.TabEventiMorosita.MAV_ANNULLATO_RIGENERATO))
            Else
                ID_BOLLETTE_NUOVE = CreaMAV(True, False, Convert.ToInt32(Epifani.TabEventiMorosita.MAV_ANNULLATO_RIGENERATO))
            End If

            If ID_BOLLETTE_NUOVE <> "" Then
                Me.txtModificato.Value = "0"

                Session.Item("FUNZIONE_1") = ""

                Response.Write("<script>window.open('CreaLettere3.aspx?ID_MOROSITA=-1&ID_CONTRATTO=-1&ID_BOLLETTE=" & ID_BOLLETTE_NUOVE & "','MOROSITA','height=400,top=0,left=0,width=700');</script>")
                SettaScelte(1)
            Else
                'Response.Write("<script>alert('MAV non rigenerato!')</script>")
            End If
        End If
        '**********************************************

        'ANNULLA / RIGENERA MAV ************************
        If Me.txtCreaMAV.Value = 1 Then
            Dim EsitoCreazione As Boolean = False

            If Request.QueryString("FUNZIONE_1") = "94_MA" Or Request.QueryString("FUNZIONE_1") = "94_MG" Then
                If CREA_MAV() = False Then
                    Response.Write("<script>alert('MAV non CREATO, riprovare!')</script>")
                Else
                    Me.txtModificato.Value = "0"
                    Session.Item("FUNZIONE_1") = ""

                    SettaScelte(1)
                End If
            End If
        End If
        '**********************************************


        'ANNULLA / RIGENERA LETTERA e MAV ************************
        If Me.txtRigeneraLettera.Value = 1 Then
            Dim ID_INDICE_MOROSITA As String = ""

            ''ID_INDICE_MOROSITA = CreaLetteraMAV()
            'Session.Add("ID_MOR", 100)

            'Session.Item("ID_MOR") = 100

            'window.returnValue = '1';

            Response.Write("<script>alert('Operazione ese!'); window.returnValue=100</script>")


            'If ID_INDICE_MOROSITA > 0 Then
            '    Me.txtModificato.Value = "0"
            '    Request.QueryString("FUNZIONE_1") = ""

            '    Response.Write("<script>window.open('CreaLettereZIP.aspx?IDMOR_LETTERA_1=" & ID_INDICE_MOROSITA & "&IDMOR_LETTERA_2=" & ID_INDICE_MOROSITA & "','MOROSITA', 'height=400,top=0,left=0,width=700');</script>")

            '    SettaScelte()
            'Else
            '    Response.Write("<script>alert('Lettera e MAV non rigenerato!')</script>")
            'End If
        End If
        '**********************************************

        If Me.txtAnnullaMAV.Value = 1 Then

            Dim EsitoAnnullo As Boolean = False

            If AnnullaMAV() = False Then
                Response.Write("<script>alert('MAV non ANNULLATO, riprovare!')</script>")
            Else
                Me.txtModificato.Value = "0"
                Session.Item("FUNZIONE_1") = ""

                SettaScelte(1)
            End If

        End If


    End Sub


    Private Sub AbilitaGruppo(ByVal gruppo As Integer, ByVal CheckBox As Boolean, ByVal Abilitazione As Boolean)


        If gruppo = 1 Then
            Me.lbl_Lettera1_1.Enabled = Abilitazione
            Me.lbl_Lettera1_2.Enabled = Abilitazione

            Me.chk1.Visible = CheckBox

            Me.lbl_Importo1.Enabled = Abilitazione
            Me.lbl_euro1.Enabled = Abilitazione
            Me.lbl_Tipo1.Enabled = Abilitazione
            Me.lbl_Dal1.Enabled = Abilitazione
            Me.lbl_Al_1.Enabled = Abilitazione

            Me.lbl_Emissione_1.Enabled = Abilitazione
            Me.lbl_Scadenza_1.Enabled = Abilitazione
            Me.lbl_Giorni_Scadenza_1.Enabled = Abilitazione

            Me.lbl_Giorni_Dilazione_1.Enabled = Abilitazione
            Me.lbl_gg1.Enabled = Abilitazione
            Me.lbl_Scadenza_Dilazione_1.Enabled = Abilitazione

            Me.txtImporto1.Enabled = Abilitazione
            Me.txtTipo1.Enabled = Abilitazione
            Me.txtDataDal1.Enabled = Abilitazione
            Me.txtDataAL_1.Enabled = Abilitazione

            Me.txtEmissione_1.Enabled = Abilitazione
            Me.txtScadenza_1.Enabled = Abilitazione
            Me.txtGironiScadenza_1.Enabled = Abilitazione

            Me.txtGiorni_1.Enabled = Abilitazione
            Me.txtScadenzaDilazione_1.Enabled = Abilitazione
            '*******************************************************
        Else

            '2 DISABILITATO con CHECK BOX
            Me.lbl_Lettera2_1.Enabled = Abilitazione
            Me.lbl_Lettera2_2.Enabled = Abilitazione

            Me.chk2.Visible = CheckBox

            Me.lbl_Importo2.Enabled = Abilitazione
            Me.lbl_euro2.Enabled = Abilitazione
            Me.lbl_Tipo2.Enabled = Abilitazione
            Me.lbl_Dal2.Enabled = Abilitazione
            Me.lbl_Al_2.Enabled = Abilitazione

            Me.lbl_Emissione_2.Enabled = Abilitazione
            Me.lbl_Scadenza_2.Enabled = Abilitazione
            Me.lbl_Giorni_Scadenza_2.Enabled = Abilitazione

            Me.lbl_Giorni_Dilazione_2.Enabled = Abilitazione
            Me.lbl_gg2.Enabled = Abilitazione
            Me.lbl_Scadenza_Dilazione_2.Enabled = Abilitazione

            Me.txtImporto2.Enabled = Abilitazione
            Me.txtTipo2.Enabled = Abilitazione
            Me.txtDataDal2.Enabled = Abilitazione
            Me.txtDataAL_2.Enabled = Abilitazione

            Me.txtEmissione_2.Enabled = Abilitazione
            Me.txtScadenza_2.Enabled = Abilitazione
            Me.txtGironiScadenza_2.Enabled = Abilitazione

            Me.txtGiorni_2.Enabled = Abilitazione
            Me.txtScadenzaDilazione_2.Enabled = Abilitazione
            '*******************************************************
        End If

    End Sub


    Private Sub RiempiCampiDilazione()
        Dim FlagConnessione As Boolean
        Dim num_bolletta As String
        Dim importobolletta As Decimal

        Dim GiorniTrascorsi As Integer

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.txt_ID1.Text = -1
            Me.txt_ID2.Text = -1


            'DETTAGLI MAV MG e MAV MA
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select * from SISCOM_MI.MOROSITA_LETTERE  " _
                                & " where ID_MOROSITA=" & vIdMorosita _
                                & "   and ID_CONTRATTO=" & vIdContratto

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
            While myReaderT.Read
                par.cmd.CommandText = " select BOL_BOLLETTE.*,TIPO_BOLLETTE.ACRONIMO " _
                                    & " from SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.TIPO_BOLLETTE " _
                                    & " where ID_MOROSITA=" & par.IfNull(myReaderT("ID_MOROSITA"), 0) _
                                    & "   and ID_CONTRATTO=" & par.IfNull(myReaderT("ID_CONTRATTO"), 0) _
                                    & "   and rif_bollettino='" & par.IfNull(myReaderT("BOLLETTINO"), "") & "'" _
                                    & "   and ID_TIPO=4" _
                                    & "   and ID_BOLLETTA_RIC is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                    & "   and SISCOM_MI.BOL_BOLLETTE.ID_TIPO=SISCOM_MI.TIPO_BOLLETTE.ID (+)" _
                                    & " order by BOL_BOLLETTE.data_emissione desc," _
                                             & " BOL_BOLLETTE.ANNO DESC," _
                                             & " BOL_BOLLETTE.N_RATA DESC," _
                                             & " BOL_BOLLETTE.id desc"

                Dim myReaderS1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS1.Read Then
                    Select Case par.IfNull(myReaderS1("n_rata"), "")
                        Case "99" 'bolletta manuale
                            num_bolletta = "MA"
                        Case "999" 'bolletta automatica
                            num_bolletta = "AU"
                        Case "99999" 'bolletta di conguaglio
                            num_bolletta = "CO"
                        Case Else
                            num_bolletta = Format(par.IfNull(myReaderS1("n_rata"), "??"), "00")
                    End Select

                    par.cmd.CommandText = "select SUM(IMPORTO) from SISCOM_MI.BOL_BOLLETTE_VOCI where ID_BOLLETTA=" & myReaderS1("ID")
                    Dim myReaderS2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderS2.Read Then
                        importobolletta = par.IfNull(myReaderS2(0), "0,00")
                    End If
                    myReaderS2.Close()


                    Dim STATO As String = ""
                    If par.IfNull(myReaderS1("FL_ANNULLATA"), "0") <> "0" Then
                        STATO = "ANNULLATA"
                    Else
                        STATO = "VALIDA"
                    End If

                    'Tot_Bolletta = 0

                    If Request.QueryString("CONTA_MAV") <> "1" And par.IfNull(myReaderT("NUM_LETTERE"), 1) = 1 Then

                        Me.txt_ID1.Text = par.IfNull(myReaderT("ID"), 0)
                        Me.txtImporto1.Text = IsNumFormat(par.IfNull(importobolletta, 0), "", "##,##0.00")
                        Me.txtTipo1.Text = num_bolletta & "/" & STATO

                        Me.txtDataDal1.Text = par.FormattaData(par.IfNull(myReaderS1("RIFERIMENTO_DA"), ""))
                        Me.txtDataAL_1.Text = par.FormattaData(par.IfNull(myReaderS1("RIFERIMENTO_A"), ""))

                        Me.txtEmissione_1.Text = par.FormattaData(par.IfNull(myReaderS1("DATA_EMISSIONE"), ""))


                        If par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), "")) <> "" Then
                            Me.txtScadenza_1.Text = par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), ""))
                            GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(Now.ToString("dd/MM/yyyy")), CDate(par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), ""))))

                        Else
                            Me.txtScadenza_1.Text = par.FormattaData(par.IfNull(myReaderS1("DATA_SCADENZA"), ""))
                            GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(Now.ToString("dd/MM/yyyy")), CDate(par.FormattaData(par.IfNull(myReaderS1("DATA_SCADENZA"), ""))))

                        End If

                        If GiorniTrascorsi < 0 Then
                            Me.lbl_Giorni_Scadenza_1.Text = "Scaduto da "
                            Me.txtGironiScadenza_1.ForeColor = Drawing.Color.Red
                        Else
                            Me.lbl_Giorni_Scadenza_1.Text = "Scadenza tra "
                            Me.txtGironiScadenza_1.ForeColor = Drawing.Color.Black
                        End If

                        Me.txtGironiScadenza_1.Text = Math.Abs(GiorniTrascorsi)

                    Else
                        Me.txt_ID2.Text = par.IfNull(myReaderT("ID"), 0)

                        Me.txtImporto2.Text = IsNumFormat(par.IfNull(importobolletta, 0), "", "##,##0.00")
                        Me.txtTipo2.Text = num_bolletta & "/" & STATO

                        Me.txtDataDal2.Text = par.FormattaData(par.IfNull(myReaderS1("RIFERIMENTO_DA"), ""))
                        Me.txtDataAL_2.Text = par.FormattaData(par.IfNull(myReaderS1("RIFERIMENTO_A"), ""))

                        Me.txtEmissione_2.Text = par.FormattaData(par.IfNull(myReaderS1("DATA_EMISSIONE"), ""))

                        If par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), "")) <> "" Then
                            Me.txtScadenza_2.Text = par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), ""))
                            GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(Now.ToString("dd/MM/yyyy")), CDate(par.FormattaData(par.IfNull(myReaderT("DATA_DILAZIONE"), ""))))

                        Else
                            Me.txtScadenza_2.Text = par.FormattaData(par.IfNull(myReaderS1("DATA_SCADENZA"), ""))
                            GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(Now.ToString("dd/MM/yyyy")), CDate(par.FormattaData(par.IfNull(myReaderS1("DATA_SCADENZA"), ""))))

                        End If

                        If GiorniTrascorsi < 0 Then
                            Me.lbl_Giorni_Scadenza_2.Text = "Scaduto da "
                            Me.txtGironiScadenza_2.ForeColor = Drawing.Color.Red
                        Else
                            Me.lbl_Giorni_Scadenza_2.Text = "Scadenza tra "
                            Me.txtGironiScadenza_2.ForeColor = Drawing.Color.Black
                        End If

                        Me.txtGironiScadenza_2.Text = Math.Abs(GiorniTrascorsi)
                    End If

                    End If
                    myReaderS1.Close()
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

    End Sub


    Protected Sub txtGiorni_1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGiorni_1.TextChanged
        CalcolaAllarme(1)
    End Sub

    Protected Sub txtGiorni_2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGiorni_2.TextChanged
        CalcolaAllarme(2)
    End Sub


    Sub CalcolaAllarme(ByVal Tipo As Integer)
        Dim GiorniTrascorsi As Integer
        Dim Data1 As String


        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"


        If Tipo = 1 Then
            If Val(Strings.Trim(Me.txtGiorni_1.Text)) > 0 Then
                Data1 = DateAdd(DateInterval.Day, Int(Me.txtGiorni_1.Text), CDate(Me.txtScadenza_1.Text))
                Me.txtScadenzaDilazione_1.Text = Data1

                GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(Now.ToString("dd/MM/yyyy")), CDate(Me.txtScadenzaDilazione_1.Text))

                If GiorniTrascorsi < 0 Then
                    Me.lbl_Giorni_Scadenza_2.Text = "Scaduto da "
                    Me.txtGironiScadenza_2.ForeColor = Drawing.Color.Red
                Else
                    Me.lbl_Giorni_Scadenza_2.Text = "Scadenza tra "
                    Me.txtGironiScadenza_2.ForeColor = Drawing.Color.Black
                End If

                Me.txtGironiScadenza_2.Text = Math.Abs(GiorniTrascorsi)

            End If
        Else

            If Val(Strings.Trim(Me.txtGiorni_2.Text)) > 0 Then
                Data1 = DateAdd(DateInterval.Day, Int(Me.txtGiorni_2.Text), CDate(Me.txtScadenza_2.Text))
                Me.txtScadenzaDilazione_2.Text = Data1

                GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(Now.ToString("dd/MM/yyyy")), CDate(Me.txtScadenzaDilazione_2.Text))

                If GiorniTrascorsi < 0 Then
                    Me.lbl_Giorni_Scadenza_2.Text = "Scaduto da "
                    Me.txtGironiScadenza_2.ForeColor = Drawing.Color.Red
                Else
                    Me.lbl_Giorni_Scadenza_2.Text = "Scadenza tra "
                    Me.txtGironiScadenza_2.ForeColor = Drawing.Color.Black
                End If

                Me.txtGironiScadenza_2.Text = Math.Abs(GiorniTrascorsi)


            End If
        End If

    End Sub



    Protected Sub btn_InserisciDilazione_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciDilazione.Click
        Dim sStr1 As String

        Try

            If Request.QueryString("CONTA_MAV") = "3" Then
                'MAV MG Abilitato
                If Val(par.IfEmpty(Me.txtGiorni_1.Text, 0)) = 0 Then
                    Response.Write("<script>alert('Inserire il numero di giorni di dilazione!')</script>")
                    Exit Sub
                End If

            ElseIf Request.QueryString("CONTA_MAV") = "2" Then

                'MAV MA Abilitato
                If Val(par.IfEmpty(Me.txtGiorni_2.Text, 0)) = 0 Then
                    Response.Write("<script>alert('Inserire il numero di giorni di dilazione!')</script>")
                    Exit Sub
                End If
            Else
                'MAV MA Abilitato
                If Val(par.IfEmpty(Me.txtGiorni_2.Text, 0)) = 0 Then
                    Response.Write("<script>alert('Inserire il numero di giorni di dilazione!')</script>")
                    Exit Sub
                End If
            End If


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
            End If


            If Me.txt_ID1.Text <> -1 Then
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                    & " set DATA_DILAZIONE=:data,COD_STATO=:stato " _
                                    & " where ID=" & Me.txt_ID1.Text

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtScadenzaDilazione_1.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE))))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()

                ''****************MYEVENT*****************
                sStr1 = "Dilazione: giorni richiesti= " & Me.txtGiorni_1.Text & " prossima scadenza il: " & Me.txtScadenzaDilazione_1.Text

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", Convert.ToInt32(Me.txt_ID1.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '************************************************

            End If


            If Me.txt_ID2.Text <> -1 Then
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                    & " set DATA_DILAZIONE=:data,COD_STATO=:stato " _
                                    & " where ID=" & Me.txt_ID2.Text

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtScadenzaDilazione_2.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE))))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()

                '****************MYEVENT*****************
                sStr1 = "Dilazione: giorni richiesti= " & Me.txtGiorni_2.Text & " prossima scadenza il: " & Me.txtScadenzaDilazione_2.Text

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", Convert.ToInt32(Me.txt_ID2.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.DILAZIONE))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '************************************************

            End If


            ' COMMIT
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Commit()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            par.cmd.Dispose()
            par.cmd.CommandText = ""


            SettaScelte(0)


        Catch ex As Exception


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

        

    End Sub

    Protected Sub chk1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk1.CheckedChanged

        Me.lbl_Lettera1_1.Enabled = chk1.Checked
        Me.lbl_Lettera1_2.Enabled = chk1.Checked


        Me.lbl_Importo1.Enabled = chk1.Checked
        Me.lbl_euro1.Enabled = chk1.Checked
        Me.lbl_Tipo1.Enabled = chk1.Checked
        Me.lbl_Dal1.Enabled = chk1.Checked
        Me.lbl_Al_1.Enabled = chk1.Checked

        Me.lbl_Emissione_1.Enabled = chk1.Checked
        Me.lbl_Scadenza_1.Enabled = chk1.Checked
        Me.lbl_Giorni_Scadenza_1.Enabled = chk1.Checked

        Me.lbl_Giorni_Dilazione_1.Enabled = chk1.Checked
        Me.lbl_gg1.Enabled = chk1.Checked
        Me.lbl_Scadenza_Dilazione_1.Enabled = chk1.Checked

        Me.txtImporto1.Enabled = chk1.Checked
        Me.txtTipo1.Enabled = chk1.Checked
        Me.txtDataDal1.Enabled = chk1.Checked
        Me.txtDataAL_1.Enabled = chk1.Checked

        Me.txtEmissione_1.Enabled = chk1.Checked
        Me.txtScadenza_1.Enabled = chk1.Checked
        Me.txtGironiScadenza_1.Enabled = chk1.Checked

        Me.txtGiorni_1.Enabled = chk1.Checked
        Me.txtScadenzaDilazione_1.Enabled = chk1.Checked


        If Val(par.IfEmpty(Me.txtGiorni_1.Text, 0)) = 0 Then
            Me.txtGiorni_1.Text = Me.txtGiorni_2.Text
        End If

        If Val(par.IfEmpty(Me.txtGiorni_1.Text, 0)) > 0 Then
            CalcolaAllarme(1)
        End If


    End Sub

    Protected Sub chk2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk2.CheckedChanged

        Me.lbl_Lettera2_1.Enabled = chk2.Checked
        Me.lbl_Lettera2_2.Enabled = chk2.Checked


        Me.lbl_Importo2.Enabled = chk2.Checked
        Me.lbl_euro2.Enabled = chk2.Checked
        Me.lbl_Tipo2.Enabled = chk2.Checked
        Me.lbl_Dal2.Enabled = chk2.Checked
        Me.lbl_Al_2.Enabled = chk2.Checked

        Me.lbl_Emissione_2.Enabled = chk2.Checked
        Me.lbl_Scadenza_2.Enabled = chk2.Checked
        Me.lbl_Giorni_Scadenza_2.Enabled = chk2.Checked

        Me.lbl_Giorni_Dilazione_2.Enabled = chk2.Checked
        Me.lbl_gg2.Enabled = chk2.Checked
        Me.lbl_Scadenza_Dilazione_2.Enabled = chk2.Checked

        Me.txtImporto2.Enabled = chk2.Checked
        Me.txtTipo2.Enabled = chk2.Checked
        Me.txtDataDal2.Enabled = chk2.Checked
        Me.txtDataAL_2.Enabled = chk2.Checked

        Me.txtEmissione_2.Enabled = chk2.Checked
        Me.txtScadenza_2.Enabled = chk2.Checked
        Me.txtGironiScadenza_2.Enabled = chk2.Checked

        Me.txtGiorni_2.Enabled = chk2.Checked
        Me.txtScadenzaDilazione_2.Enabled = chk2.Checked

        If Val(par.IfEmpty(Me.txtGiorni_2.Text, 0)) = 0 Then
            Me.txtGiorni_2.Text = Me.txtGiorni_1.Text
        End If

        If Val(par.IfEmpty(Me.txtGiorni_2.Text, 0)) > 0 Then
            CalcolaAllarme(2)
        End If

    End Sub

    Protected Sub btn_InserisciSospensione_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciSospensione.Click
        Dim i As Integer
        Dim bSelezione As Integer = 0
        Dim Id2 As Long = 0


        For i = 0 To Me.RButtonSospensione.Items.Count - 1
            If Me.RButtonSospensione.Items(i).Selected = True Then
                bSelezione = Me.RButtonSospensione.Items(i).Value
                Exit For
            End If
        Next

        If bSelezione = False Then
            Response.Write("<script>alert('Selezionare il motivo della sospensione!')</script>")
            Exit Sub
        End If

        Try


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
            End If



            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(bSelezione)) & "'" _
                                & " where ID=" & vIdMorositaLettera

            par.cmd.ExecuteNonQuery()

            '****************MYEVENT*****************
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                          & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(bSelezione))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", Me.txtNoteSospensione.Text))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************************


            If Me.chkSospensione.Visible = True And Me.chkSospensione.Checked = True Then

                par.cmd.CommandText = " select ID from SISCOM_MI.MOROSITA_LETTERE  " _
                                    & " where ID_MOROSITA=" & vIdMorosita _
                                    & "  and  ID_CONTRATTO=" & vIdContratto _
                                    & "  and  ID<>" & vIdMorositaLettera

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    Id2 = par.IfNull(myReader1("ID"), -1)
                End If
                myReader1.Close()

                If Id2 <> 0 Then


                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                        & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(bSelezione)) & "'" _
                                        & " where ID=" & Id2

                    par.cmd.ExecuteNonQuery()


                    ''****************MYEVENT*****************
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                  & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", Id2))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(bSelezione))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", Me.txtNoteSospensione.Text))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '************************************************

                End If

            End If

            ' COMMIT
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Commit()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            par.cmd.Dispose()
            par.cmd.CommandText = ""

            Response.Write("<script>alert('Sospensione effettuata con successo!')</script>")

            Me.txtAppareSospensione.Value = 0

            SettaScelte(0)


        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Sub AnnullaSospensione()
        Dim sStr1 As String

        Try


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
            End If


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ANNULLATA)) & "'" _
                                & " where ID=" & vIdMorositaLettera


            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()

            '****************MYEVENT*****************
            sStr1 = "Annullamento Sospensione."

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                          & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.SOSPENSIONE_ANNULLATA))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************************


            ' COMMIT
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Commit()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            par.Dispose()

            par.cmd.CommandText = ""

            Me.txtAnnullo.Value = 0

            Response.Write("<script>alert('Annullamento effettuato con successo!')</script>")

            SettaScelte(0)


        Catch ex As Exception


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub



    'BOLLETTE GRID1
    Private Sub CaricaPagamenti()

        Dim vidMorosita As Long
        Dim vIdContratto As Long
        Dim vidAnagrafica As Long

        Dim nBollette As String = ""

        Dim IdBoletta1 As Decimal = 0
        Dim IdBoletta2 As Decimal = 0

        Dim num_bolletta As String = ""

       
        Try

            Me.DataGridBolletteMG.Visible = True
            Me.DataGridBolletteMA.Visible = True

            Me.btn_EsciPagamento.Visible = False
            Me.btn_ChiudiPagamento.Visible = True
            Me.btn_InserisciPagamento.Visible = True

            Me.btnApri1.Visible = True
            Me.btnApri2.Visible = True


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

                Me.txtID_SelectMG.Value = ""
                Me.txtID_SelectMA.Value = ""
            End If

            Me.txtModificato.Value = "0"

            vidMorosita = Request.QueryString("ID_MOROSITA")
            vIdContratto = Request.QueryString("ID_CONTRATTO")

            'DETTAGLI MAV MG e MAV MA
            par.cmd.CommandText = " select * from SISCOM_MI.MOROSITA_LETTERE  " _
                                              & " where ID_MOROSITA=" & vidMorosita _
                                              & "  and  ID_CONTRATTO=" & vIdContratto

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReaderT.Read

                vidAnagrafica = par.IfNull(myReaderT("ID_ANAGRAFICA"), -1)

                par.cmd.CommandText = " select BOL_BOLLETTE.*,TIPO_BOLLETTE.ACRONIMO " _
                                    & " from SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.TIPO_BOLLETTE " _
                                    & " where ID_MOROSITA=" & vidMorosita _
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

                    Dim STATO As String = ""
                    If par.IfNull(myReaderBOL("FL_ANNULLATA"), "0") <> "0" Then
                        STATO = "ANNULLATA"
                    Else
                        STATO = "VALIDA"
                    End If


                    If par.IfNull(myReaderT("NUM_LETTERE"), 1) = 1 And par.IfNull(myReaderT("FINE_PERIODO"), "") = "20090930" Then

                        'DETTAGLI MAV MG GLOBAL prima del 2009 o solo prima del 2009

                        IdBoletta1 = myReaderBOL("ID")                      'BOL_BOLLETTE.ID

                        Me.txtTipo_MG.Text = num_bolletta & "/" & STATO
                        Me.txtDataDal_MG.Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_da"), ""))
                        Me.txtDataAL_MG.Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_a"), ""))


                        Me.totPagabile.Value = 0
                        Me.totPagato.Value = 0
                        Me.totResiduo.Value = 0

                        Me.txtImportoP1.Text = IsNumFormat(par.IfNull(myReaderBOL("IMPORTO_TOTALE"), 0), "", "##,##0.00")

                        Dim bPrimoCaricamento As Boolean

                        '**************** 24/08/2011 ESCLUDO LE BOLLETTE RATEIZZATE*************************
                        ' " and (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND IMPORTO_TOTALE > 0 
                        '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                        'Estraggo solo le bollette con importo positivo, non annullate e non rateizzate
                        par.cmd.CommandText = "select REPLACE(REPLACE('<a href=£javascript:void(0)£ onclick=£window.open(''../Contabilita/DettaglioBolletta.aspx?IDCONT='||ID_CONTRATTO||'$IDBOLL='||ID||'$IDANA=" & vidAnagrafica & "'',''Dettagli'','''');£>'||NUM_BOLLETTA||'</a>','$','&'),'£','') as NUM_BOLLETTA, " _
                                            & " BOL_BOLLETTE.ID,BOL_BOLLETTE.N_RATA," _
                                            & "TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_EMISSIONE""," _
                                            & "TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy')  AS ""DATA_SCADENZA""," _
                                            & " TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_PAGATO,0),'9G999G999G990D99'))AS IMPORTO_PAGATO," _
                                            & " TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_TOTALE,0),'9G999G999G990D99'))AS IMPORTO_TOTALE, " _
                                            & "(TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' al '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS ""RIFERIMENTO""," _
                                            & " TRIM(TO_CHAR(NVL(NVL(IMPORTO_TOTALE,0)-NVL(IMPORTO_PAGATO,0),0),'9G999G999G990D99')) AS RESIDUO " _
                                            & " from SISCOM_MI.BOL_BOLLETTE " _
                                            & " where SISCOM_MI.BOL_BOLLETTE.ID_CONTRATTO=" & vIdContratto _
                                            & " and FL_ANNULLATA=0 " _
                                            & " and ID_RATEIZZAZIONE IS NULL AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                            & " and  NVL(IMPORTO_TOTALE,0) > 0 "


                        If Strings.Len(Strings.Trim(Me.txtID_SelectMG.Value)) > 0 Then
                            'Se mi mitrovo nell'aggiornamento della pagina dopo aver fatto un pagamento
                            bPrimoCaricamento = False
                            par.cmd.CommandText = par.cmd.CommandText & "   and SISCOM_MI.BOL_BOLLETTE.ID in (" & Me.txtID_SelectMG.Value & ")"
                        Else
                            bPrimoCaricamento = True
                            par.cmd.CommandText = par.cmd.CommandText & " and SISCOM_MI.BOL_BOLLETTE.ID_MOROSITA = " & vidMorosita _
                                                                      & " and SISCOM_MI.BOL_BOLLETTE.ID_BOLLETTA_RIC in (" & IdBoletta1 & ")" _
                                                                      & " and ID_BOLLETTA_RIC IS NOT NULL "

                        End If

                        par.cmd.CommandText = par.cmd.CommandText & " order by BOL_BOLLETTE.DATA_SCADENZA ASC"

                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dt As New Data.DataTable()
                        da.Fill(dt)

                        'elaborazione totali
                        Me.txtID_SelectMG.Value = ""

                        Dim row As Data.DataRow
                        For Each row In dt.Rows

                            If Me.txtID_SelectMG.Value = "" Then
                                Me.txtID_SelectMG.Value = par.IfNull(row.Item("ID"), 0)
                            Else
                                Me.txtID_SelectMG.Value = Me.txtID_SelectMG.Value & "," & par.IfNull(row.Item("ID"), 0)
                            End If

                            Me.totPagabile.Value = CDec(totPagabile.Value) + CDec((par.IfNull(row.Item("IMPORTO_TOTALE"), 0)))
                            Me.totPagato.Value = CDec(totPagato.Value) + CDec(par.IfNull(row.Item("IMPORTO_PAGATO"), 0))
                            Me.totResiduo.Value = CDec(totResiduo.Value) + CDec(par.IfNull(row.Item("RESIDUO"), 0))
                        Next

                        row = dt.NewRow()
                        row.Item("NUM_BOLLETTA") = "TOTALE"
                        row.Item("IMPORTO_TOTALE") = Format(CDec(totPagabile.Value), "##,##0.00")
                        row.Item("IMPORTO_PAGATO") = Format(CDec(totPagato.Value), "##,##0.00")
                        row.Item("RESIDUO") = Format(CDec(totResiduo.Value), "##,##0.00")

                        If CDec(totPagato.Value) > 0 And bPrimoCaricamento = False Then
                            Me.txtModificato.Value = 1
                        End If

                        dt.Rows.Add(row)

                        Me.DataGridBolletteMG.DataSource = dt
                        Me.DataGridBolletteMG.DataBind()
                        Dim di As DataGridItem
                        di = Me.DataGridBolletteMG.Items(Me.DataGridBolletteMG.Items.Count - 1)
                        di.Cells(0).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(1).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(2).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(3).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(4).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(5).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(6).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(7).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(8).BackColor = Drawing.Color.DodgerBlue

                        di.Cells(0).ForeColor = Drawing.Color.White
                        di.Cells(1).ForeColor = Drawing.Color.White
                        di.Cells(2).ForeColor = Drawing.Color.White
                        di.Cells(3).ForeColor = Drawing.Color.White
                        di.Cells(5).ForeColor = Drawing.Color.White
                        di.Cells(6).ForeColor = Drawing.Color.White
                        di.Cells(7).ForeColor = Drawing.Color.White
                        di.Cells(8).ForeColor = Drawing.Color.White

                    Else
                        'DETTAGLI MAV MA dopo il 2009

                        IdBoletta2 = myReaderBOL("ID")                      'BOL_BOLLETTE.ID

                        Me.txtTipo_MA.Text = num_bolletta & "/" & STATO
                        Me.txtDataDal_MA.Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_da"), ""))
                        Me.txtDataAL_MA.Text = par.FormattaData(par.IfNull(myReaderBOL("riferimento_a"), ""))


                        Me.totPagabile.Value = 0
                        Me.totPagato.Value = 0
                        Me.totResiduo.Value = 0

                        Me.txtImportoP2.Text = IsNumFormat(par.IfNull(myReaderBOL("IMPORTO_TOTALE"), 0), "", "##,##0.00")

                        Dim bPrimoCaricamento As Boolean

                        '**************** 24/08/2011 ESCLUDO LE BOLLETTE RATEIZZATE*************************
                        ' " and (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND IMPORTO_TOTALE > 0 
                        '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                        'Estraggo solo le bollette con importo positivo, non annullate e non rateizzate
                        par.cmd.CommandText = "select REPLACE(REPLACE('<a href=£javascript:void(0)£ onclick=£window.open(''../Contabilita/DettaglioBolletta.aspx?IDCONT='||ID_CONTRATTO||'$IDBOLL='||ID||'$IDANA=" & vidAnagrafica & "'',''Dettagli'','''');£>'||NUM_BOLLETTA||'</a>','$','&'),'£','') as NUM_BOLLETTA, " _
                                            & " BOL_BOLLETTE.ID,BOL_BOLLETTE.N_RATA," _
                                            & "TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_EMISSIONE""," _
                                            & "TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy')  AS ""DATA_SCADENZA""," _
                                            & " TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_PAGATO,0),'9G999G999G990D99'))AS IMPORTO_PAGATO," _
                                            & " TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_TOTALE,0),'9G999G999G990D99'))AS IMPORTO_TOTALE, " _
                                            & "(TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' al '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS ""RIFERIMENTO""," _
                                            & " TRIM(TO_CHAR(NVL(NVL(IMPORTO_TOTALE,0)-NVL(IMPORTO_PAGATO,0),0),'9G999G999G990D99')) AS RESIDUO " _
                                            & " from SISCOM_MI.BOL_BOLLETTE " _
                                            & " where SISCOM_MI.BOL_BOLLETTE.ID_CONTRATTO=" & vIdContratto _
                                            & " and FL_ANNULLATA=0 " _
                                            & " and ID_RATEIZZAZIONE IS NULL AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                            & " and  NVL(IMPORTO_TOTALE,0) > 0 "

                        If Strings.Len(Strings.Trim(Me.txtID_SelectMA.Value)) > 0 Then
                            'Se mi mitrovo nell'aggiornamento della pagina dopo aver fatto un pagamento
                            bPrimoCaricamento = False
                            par.cmd.CommandText = par.cmd.CommandText & "   and SISCOM_MI.BOL_BOLLETTE.ID in (" & Me.txtID_SelectMA.Value & ")"
                        Else
                            bPrimoCaricamento = True
                            par.cmd.CommandText = par.cmd.CommandText & " and SISCOM_MI.BOL_BOLLETTE.ID_MOROSITA = " & vidMorosita _
                                                                      & " and SISCOM_MI.BOL_BOLLETTE.ID_BOLLETTA_RIC in (" & IdBoletta2 & ")" _
                                                                      & " and ID_BOLLETTA_RIC IS NOT NULL "


                        End If

                        par.cmd.CommandText = par.cmd.CommandText & " order by BOL_BOLLETTE.DATA_SCADENZA ASC"

                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dt As New Data.DataTable()
                        da.Fill(dt)
                        'elaborazione totali
                        Me.txtID_SelectMA.Value = ""

                        Dim row As Data.DataRow
                        For Each row In dt.Rows

                            If Me.txtID_SelectMA.Value = "" Then
                                Me.txtID_SelectMA.Value = par.IfNull(row.Item("ID"), 0)
                            Else
                                Me.txtID_SelectMA.Value = Me.txtID_SelectMA.Value & "," & par.IfNull(row.Item("ID"), 0)
                            End If

                            Me.totPagabile.Value = CDec(totPagabile.Value) + CDec((par.IfNull(row.Item("IMPORTO_TOTALE"), 0)))
                            Me.totPagato.Value = CDec(totPagato.Value) + CDec(par.IfNull(row.Item("IMPORTO_PAGATO"), 0))
                            Me.totResiduo.Value = CDec(totResiduo.Value) + CDec(par.IfNull(row.Item("RESIDUO"), 0))
                        Next

                        row = dt.NewRow()
                        row.Item("NUM_BOLLETTA") = "TOTALE"
                        row.Item("IMPORTO_TOTALE") = Format(CDec(totPagabile.Value), "##,##0.00")
                        row.Item("IMPORTO_PAGATO") = Format(CDec(totPagato.Value), "##,##0.00")
                        row.Item("RESIDUO") = Format(CDec(totResiduo.Value), "##,##0.00")

                        If CDec(totPagato.Value) > 0 And bPrimoCaricamento = False Then
                            Me.txtModificato.Value = 1
                        End If

                        dt.Rows.Add(row)

                        Me.DataGridBolletteMA.DataSource = dt
                        Me.DataGridBolletteMA.DataBind()

                        Dim di As DataGridItem
                        di = Me.DataGridBolletteMA.Items(Me.DataGridBolletteMA.Items.Count - 1)
                        di.Cells(0).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(1).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(2).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(3).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(4).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(5).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(6).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(7).BackColor = Drawing.Color.DodgerBlue
                        di.Cells(8).BackColor = Drawing.Color.DodgerBlue

                        di.Cells(0).ForeColor = Drawing.Color.White
                        di.Cells(1).ForeColor = Drawing.Color.White
                        di.Cells(2).ForeColor = Drawing.Color.White
                        di.Cells(3).ForeColor = Drawing.Color.White
                        di.Cells(5).ForeColor = Drawing.Color.White
                        di.Cells(6).ForeColor = Drawing.Color.White
                        di.Cells(7).ForeColor = Drawing.Color.White
                        di.Cells(8).ForeColor = Drawing.Color.White

                    End If

                End If
                myReaderBOL.Close()

            End While
            myReaderT.Close()


        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub DataGridBollette_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridBolletteMG.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtSelMG').value='Hai selezionato la bolletta N: " & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';document.getElementById('txtIdMG').value='" & e.Item.Cells(0).Text & "';document.getElementById('numBollettaMG').value='" & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtSelMG').value='Hai selezionato la bolletta N: " & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';document.getElementById('txtIdMG').value='" & e.Item.Cells(0).Text & "';document.getElementById('numBollettaMG').value='" & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';")

        End If
    End Sub

    Protected Sub DataGridBolletteMA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridBolletteMA.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtSelMA').value='Hai selezionato la bolletta N: " & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';document.getElementById('txtIdMA').value='" & e.Item.Cells(0).Text & "';document.getElementById('numBollettaMA').value='" & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtSelMA').value='Hai selezionato la bolletta N: " & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';document.getElementById('txtIdMA').value='" & e.Item.Cells(0).Text & "';document.getElementById('numBollettaMA').value='" & e.Item.Cells(1).Text.Substring(e.Item.Cells(1).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "';")

        End If
    End Sub


    Protected Sub btn_InserisciPagamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciPagamento.Click
        Dim i As Integer
        Dim bTrovatoMG As Boolean = False
        Dim bTrovatoMA As Boolean = False
        Dim FlagConnessione As Boolean


        Dim ID_BOLLETTE_NUOVE As String = ""
        Dim ImportoMA As Decimal = 0
        Dim ImportoMG As Decimal = 0

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            par.cmd.Parameters.Clear()

            par.cmd.CommandText = " select SISCOM_MI.MOROSITA_LETTERE.*,SISCOM_MI.BOL_BOLLETTE.NOTE" _
                                & " from SISCOM_MI.MOROSITA_LETTERE, SISCOM_MI.BOL_BOLLETTE" _
                                & "  where MOROSITA_LETTERE.ID_MOROSITA=" & vIdMorosita _
                                & "    and MOROSITA_LETTERE.ID_CONTRATTO=" & vIdContratto _
                                & "    and SISCOM_MI.MOROSITA_LETTERE.BOLLETTINO=BOL_BOLLETTE.RIF_BOLLETTINO"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                If par.IfNull(myReader1("NOTE"), "") = "MOROSITA' MG" Then
                    ImportoMG = par.IfNull(myReader1("IMPORTO"), 0)
                End If
                If par.IfNull(myReader1("NOTE"), "") = "MOROSITA' MA" Then
                    ImportoMA = par.IfNull(myReader1("IMPORTO"), 0)
                End If
            End While
            myReader1.Close()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            '1) Verificare quale MAV è stato modificato il pagamento
            For i = 0 To Me.DataGridBolletteMG.Items.Count - 2
                If strToNumber(Me.DataGridBolletteMG.Items(i).Cells(7).Text.Replace(".", "")) > 0 Then
                    bTrovatoMG = True
                    Exit For
                End If
            Next

            For i = 0 To Me.DataGridBolletteMA.Items.Count - 2
                If strToNumber(Me.DataGridBolletteMA.Items(i).Cells(7).Text.Replace(".", "")) > 0 Then
                    bTrovatoMA = True
                    Exit For
                End If
            Next

            If bTrovatoMG = False And bTrovatoMA = False Then
                Response.Write("<script>alert('Nessun MAV creato perchè non è stato effettuato nessun pagamento!')</script>")
                Exit Sub
            Else
                If Me.DataGridBolletteMA.Items.Count > 0 And ImportoMA > 0 Then
                    If Round(strToNumber(Me.DataGridBolletteMA.Items(Me.DataGridBolletteMA.Items.Count - 1).Cells(8).Text.Replace(".", "")), 2) = ImportoMA Then
                        Response.Write("<script>alert('Nessun MAV creato perchè non è stato effettuato nessun pagamento!')</script>")
                        Exit Sub
                    End If
                End If


                If Me.DataGridBolletteMG.Items.Count > 0 And ImportoMG > 0 Then
                    If Round(strToNumber(Me.DataGridBolletteMG.Items(Me.DataGridBolletteMG.Items.Count - 1).Cells(8).Text.Replace(".", "")), 2) = ImportoMG Then
                        Response.Write("<script>alert('Nessun MAV creato perchè non è stato effettuato nessun pagamento!')</script>")
                        Exit Sub
                    End If
                End If
            End If
            '**********************************


                ID_BOLLETTE_NUOVE = CreaMAV(bTrovatoMG, bTrovatoMA, Convert.ToInt32(Epifani.TabEventiMorosita.MAV_AGGIORNATO))


                Me.txtModificato.Value = "0"

                Me.btn_EsciPagamento.Visible = True
                Me.btn_ChiudiPagamento.Visible = False
                Me.btn_InserisciPagamento.Visible = False

                Me.btnApri1.Visible = False
                Me.btnApri2.Visible = False

                Response.Write("<script>window.open('CreaLettere3.aspx?ID_MOROSITA=-1&ID_CONTRATTO=-1&ID_BOLLETTE=" & ID_BOLLETTE_NUOVE & "','MOROSITA','height=400,top=0,left=0,width=700');</script>")



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

    Protected Sub btnApri1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri1.Click
        CaricaPagamenti()
    End Sub

    Protected Sub btnApri2_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri2.Click
        CaricaPagamenti()
    End Sub

    Protected Sub btn_ChiudiPagamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiPagamento.Click

        Try

            If Me.txtModificato.Value = "111" Then

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                End If

                Me.txtApparePagamento.Value = 0

                Me.DataGridBolletteMG.Visible = False
                Me.DataGridBolletteMA.Visible = False


            ElseIf Me.txtModificato.Value = "0" Then
                Me.txtApparePagamento.Value = 0
                Me.DataGridBolletteMG.Visible = False
                Me.DataGridBolletteMA.Visible = False

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                End If

            Else
                Me.txtModificato.Value = "1"
            End If


        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub



    Private Function CreaCausale(ByVal Tipo As String, ByVal idb As Long) As String
        Try
            Dim sCausale As String = ""
            Dim sImporto As String = ""
            Dim iDifferenza As Integer = 0
            Dim sDescrizione As String = ""

            sCausale = ""
            CreaCausale = ""

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select T_VOCI_BOLLETTA.descrizione,BOL_BOLLETTE_VOCI.importo " _
                               & " from siscom_mi.BOL_BOLLETTE,siscom_mi.T_VOCI_BOLLETTA,siscom_mi.BOL_BOLLETTE_VOCI " _
                               & " where BOL_BOLLETTE_VOCI.id_bolletta=BOL_BOLLETTE.id " _
                               & "   and T_VOCI_BOLLETTA.id=BOL_BOLLETTE_VOCI.id_voce " _
                               & "   and BOL_BOLLETTE.id=" & idb _
                               & " order by t_voci_bolletta.descrizione asc"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                sImporto = Format(par.IfNull(myReader("importo"), "0"), "##,##0.00")

                'If sImporto < 1 And sImporto > 0 Then
                '    sImporto = Format(CDbl(sImporto), "0.00")
                'End If

                If sImporto < 1 And sImporto > 0 Then
                    sImporto = "0" & sImporto
                End If

                If sImporto > -1 And sImporto < 0 Then
                    sImporto = "-0" & Replace(sImporto, "-", "")
                End If

                iDifferenza = 55 - Len(sImporto)
                sDescrizione = par.IfNull(myReader("descrizione"), "")
                sCausale = sCausale & Mid(sDescrizione.PadRight(iDifferenza), 1, iDifferenza) & sImporto
            Loop
            CreaCausale = sCausale
            myReader.Close()
            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            CreaCausale = ""
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'lblErrore.Visible = True
            'lblErrore.Text = ex.Message
            'Button1.Visible = False
        End Try

    End Function



    Protected Sub btn_EsciPagamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_EsciPagamento.Click
        Me.DataGridBolletteMG.Visible = False
        Me.DataGridBolletteMA.Visible = False
    End Sub

    Public Property DisabilitaExpect100Continue() As String
        Get
            If Not (ViewState("par_DisabilitaExpect100Continue") Is Nothing) Then
                Return CStr(ViewState("par_DisabilitaExpect100Continue"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DisabilitaExpect100Continue") = value
        End Set
    End Property

    Function CreaMAV(ByVal bTrovatoMG As Boolean, ByVal bTrovatoMA As Boolean, ByVal iEsito As Integer) As String
        CreaMAV = ""


        Dim ID_BOLLETTA As Long = 0
        Dim ScadenzaBollettino As String = ""
        Dim NoteBollette As String = ""

        Dim APPLICABOLLO As Double = 0
        Dim SPESEmav As Double = 0
        Dim BOLLO As Double = 0
        Dim spese_notifica As Double = 0
        Dim Tot_Bolletta As Double = 0
        Dim causalepagamento As String = ""

        Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS


        Dim sStr1 As String = ""
        Dim ValSomma As Decimal = 0
        Dim sDataDA As String = ""
        Dim sDataA As String = ""

        Dim TipoIngiunzione As String = ""

        Dim CodiceContratto As String = ""
        Dim IdAnagrafica As String = "-1"

        Dim civico_cor As String = ""
        Dim luogo_cor As String = ""
        Dim cap_cor As String = ""
        Dim indirizzo_cor As String = ""
        Dim tipo_cor As String = ""
        Dim sigla_cor As String = ""

        Dim Nome As String = ""
        Dim Cognome As String = ""
        Dim CF As String = ""


        Dim Esito As New MavOnline.rispostaMAVOnlineWS
        Dim pp As New MavOnline.MAVOnlineBeanService
        Dim outputFileName As String = ""
        Dim binaryData() As Byte
        Dim outFile As System.IO.FileStream

        Dim num_bollettino As String = ""

        Dim lBOLLETTE_ID As Long

        Dim ID_BOLLETTE_NUOVE As String = ""

        Dim ValSommaImportoCanone As Decimal = 0
        Dim ValSommaImportoOneri As Decimal = 0

        Try



            Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
            pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
            pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

            Dim Licenza As String = Session.Item("LicenzaPdfMerge")
            If Licenza <> "" Then
                pdfMerge.LicenseKey = Licenza
            End If


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
            End If

            '1) PARAMETRI BOLLETTA
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.PARAMETRI_BOLLETTA "
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReaderA.Read
                Select Case par.IfNull(myReaderA("ID"), 0)
                    Case "25"
                        APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    Case "26"
                        SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    Case "0"
                        BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    Case "36" '"ex 34"
                        spese_notifica = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    Case "32"
                        causalepagamento = par.IfNull(myReaderA("VALORE"), "")
                End Select
            End While
            myReaderA.Close()
            '*********************************************
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='EXPECT100CONTINUE'"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                DisabilitaExpect100Continue = par.IfNull(myReaderA("valore"), "0")
            End If
            myReaderA.Close()

            'NOTA: prima del 02/Sett/2001 era di 40 gg ora è di 60 + 20 + controllo se capita di sabato o domenica
            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 80, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 80, Now.Date))
            ScadenzaBollettino = par.AggiustaData(DateAdd("d", 40, Now.Date))

            Dim d1 As Date
            d1 = New Date(Mid(ScadenzaBollettino, 1, 4), Mid(ScadenzaBollettino, 5, 2), Mid(ScadenzaBollettino, 7, 2))
            If d1.DayOfWeek = DayOfWeek.Saturday Then
                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 42, CDate(par.FormattaData(Format(Now, "yyyyMMdd")))))

            ElseIf d1.DayOfWeek = DayOfWeek.Sunday Then
                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 41, CDate(par.FormattaData(Format(Now, "yyyyMMdd")))))
            End If
            '************

            '**** MG INIZIO ***************************************************************************************
            If bTrovatoMG = True Then
                'Aggiornato MAV MG Global Prima del 2009

                NoteBollette = "MOROSITA' MG"
                TipoIngiunzione = "RECUPERO MOROSITA'"

                Tot_Bolletta = 0

                'Ricavo ID della lettera (MOROSITA_LETTERE)
                sStr1 = "select ID from SISCOM_MI.MOROSITA_LETTERE " _
                    & " where ID_MOROSITA= " & vIdMorosita _
                    & "   and ID_CONTRATTO=" & vIdContratto _
                    & "   and BOLLETTINO=(select RIF_BOLLETTINO from SISCOM_MI.BOL_BOLLETTE " _
                                      & " where NOTE='" & par.PulisciStrSql(NoteBollette) & "'" _
                                      & "   and ID_MOROSITA=" & vIdMorosita _
                                      & "   and ID_CONTRATTO=" & vIdContratto _
                                      & "   and FL_ANNULLATA=0 )"


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    vIdMorositaLettera = par.IfNull(myReaderB(0), 0)
                End If
                myReaderB.Close()


                'RICAVO ID del MAV Vecchio
                sStr1 = "select ID from SISCOM_MI.BOL_BOLLETTE " _
                     & " where  NOTE='" & par.PulisciStrSql(NoteBollette) & "'" _
                     & "   and ID_MOROSITA=" & vIdMorosita _
                     & "   and ID_CONTRATTO=" & vIdContratto _
                     & "   and ID_BOLLETTA_RIC is null " _
                     & "   and ID_TIPO=4 " _
                     & "   and FL_ANNULLATA=0 "


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    lBOLLETTE_ID = par.IfNull(myReaderB(0), 0)
                End If
                myReaderB.Close()


                'RIPRISTINO IMPORTO_RICLASSIFICATO di TUTTE le BOLLETTE VOCI interesate dal MAV vecchio
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                   & " set  IMPORTO_RICLASSIFICATO = Null " _
                                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                        & "  where ID_BOLLETTA_RIC=" & lBOLLETTE_ID _
                                                        & "    and ID_MOROSITA=" & vIdMorosita _
                                                        & "    and FL_ANNULLATA=0 " _
                                                        & "    and ID_CONTRATTO=" & vIdContratto _
                                                        & "    and ID_BOLLETTA_RIC IS NOT NULL " _
                                                        & "    and ID_RATEIZZAZIONE IS NULL ) " _
                                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()


                'RIPRISTINO TUTTE le BOLLETTE interesate dal MAV vecchio
                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                                   & " set ID_BOLLETTA_RIC=Null," _
                                   & "     ID_MOROSITA=Null " _
                                   & " where ID_MOROSITA=" & vIdMorosita _
                                   & "   and ID_BOLLETTa_RIC=" & lBOLLETTE_ID _
                                   & "   and ID_CONTRATTO=" & vIdContratto _
                                   & "   and FL_ANNULLATA=0 " _
                                   & "   and ID_BOLLETTA_RIC IS NOT NULL " _
                                   & "   and ID_RATEIZZAZIONE IS NULL "

                par.cmd.ExecuteNonQuery()

                'ANNULLO il MAV Vecchio
                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                                   & " set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'" _
                                   & " where ID=" & lBOLLETTE_ID
                par.cmd.ExecuteNonQuery()

                '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                ' SOLO PRIMA del 2009 (ricavo la somma da Pagare ancora)
                'sStr1 = "select SUM((NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0))) "
                sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
                     & " from SISCOM_MI.BOL_BOLLETTE " _
                     & " where ID_CONTRATTO=" & vIdContratto _
                     & "   and FL_ANNULLATA=0 " _
                     & "   and ID_MOROSITA is Null  " _
                     & "   and ID_BOLLETTA_RIC is null  " _
                     & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                     & "   and ID<0 " _
                     & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                     & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                     & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                     & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                '& "   and ID in (" & Me.txtID_SelectMG.Value & ") " 
                '& "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    ValSomma = par.IfNull(myReaderB(0), 0)
                End If
                myReaderB.Close()



                If ValSomma <= 0 Then

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                        & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA)) & "'" _
                                        & " where ID=" & vIdMorositaLettera


                    par.cmd.ExecuteNonQuery()

                    '****************MYEVENT*****************
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                  & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", "Effettuato il Pagamento"))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '************************************************


                    'COMMIT
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        par.myTrans.Commit()
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                    End If
                    par.cmd.Dispose()

                    Exit Function
                End If

                '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
                                    & " from SISCOM_MI.BOL_BOLLETTE " _
                                    & " where ID_CONTRATTO=" & vIdContratto _
                                    & "   and FL_ANNULLATA=0 " _
                                    & "   and ID_MOROSITA is Null  " _
                                    & "   and ID_BOLLETTA_RIC Is NULL " _
                                    & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                    & "   and ID<0 " _
                                    & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                    & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                    & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    sDataDA = par.IfNull(myReaderB(0), 0)
                End If
                myReaderB.Close()
                '******************************************


                ' '' Ricavo ID_BOLLETTA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL FROM dual "
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    ID_BOLLETTA = myReaderB(0)

                    If ID_BOLLETTE_NUOVE = "" Then
                        ID_BOLLETTE_NUOVE = ID_BOLLETTA
                    Else
                        ID_BOLLETTE_NUOVE = ID_BOLLETTE_NUOVE & "," & ID_BOLLETTA
                    End If
                End If
                myReaderB.Close()




                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select * from SISCOM_MI.BOL_BOLLETTE " _
                                    & " where ID=" & lBOLLETTE_ID

                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then

                    IdAnagrafica = par.IfNull(myReaderB("COD_AFFITTUARIO"), -1)

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
                                                & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, " _
                                                & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA,ID_TIPO) " _
                                        & "values (:id,:n_rata,:data_emissione,:data_scadenza," _
                                                & ":note,:id_contratto,:id_esercizio_f," _
                                                & ":id_unita,:fl_annullata,:pagabile_presso,:cod_affittuario,:intestatario," _
                                                & ":indirizzo,:cap_citta,:presso,:riferimento_da,:riferimento_a," _
                                                & ":fl_stampato,:id_complesso,:data_ins_pagamento,:importo_pagato,:note_pagamento," _
                                                & ":anno,:operatore_pag,:id_edificio,:data_annullo_pag,:operatore_annullo_pag,:rif_file,:id_morosita,4)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", ID_BOLLETTA))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_rata", 999))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_emissione", Format(Now, "yyyyMMdd")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", ScadenzaBollettino))

                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_1_sollecito", "NULL"))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_2_sollecito", "NULL"))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_pagamento", "NULL"))

                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", NoteBollette))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_CONTRATTO"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_esercizio_f", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_ESERCIZIO_F"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_UNITA"), -1))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_annullata", "0"))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pagabile_presso", ""))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_affittuario", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("COD_AFFITTUARIO"), -1))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("intestatario", par.IfNull(myReaderB("INTESTATARIO"), "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", par.IfNull(myReaderB("INDIRIZZO"), "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap_citta", par.IfNull(myReaderB("CAP_CITTA"), "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("presso", par.IfNull(myReaderB("PRESSO"), "")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_da", sDataDA))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_a", par.IfNull(myReaderB("RIFERIMENTO_A"), "")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_stampato", "0"))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_COMPLESSO"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ins_pagamento", ""))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_pagato", DBNull.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note_pagamento", ""))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Year(Now)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_pag", ""))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_EDIFICIO"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_annullo_pag", DBNull.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_annullo_pag", DBNull.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_file", "MOR"))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorosita))


                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()

                End If
                myReaderB.Close()

                Dim VOCE = "150" '"626"

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                    & " values " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                            & ID_BOLLETTA & "," _
                                            & VOCE & "," _
                                            & par.VirgoleInPunti(ValSomma) & ")"
                par.cmd.ExecuteNonQuery()
                Tot_Bolletta = Tot_Bolletta + ValSomma

                If NoteBollette = "MOROSITA' MA" Then
                    'SPESE DI NOTIFICA (628)
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                       & " values " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                            & ID_BOLLETTA _
                                            & ",628," _
                                            & par.VirgoleInPunti(spese_notifica) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + spese_notifica
                End If

                'SPESE MAV
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                   & " values " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                        & ID_BOLLETTA _
                                        & ",407," _
                                        & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                par.cmd.ExecuteNonQuery()
                Tot_Bolletta = Tot_Bolletta + SPESEmav

                'BOLLO
                If Tot_Bolletta >= APPLICABOLLO Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                       & " values " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                & ID_BOLLETTA _
                                                & ",95," _
                                                & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + BOLLO
                End If
                '******************************************************


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select RAPPORTI_UTENZA.* " _
                                   & " from SISCOM_MI.RAPPORTI_UTENZA " _
                                 & " where ID=" & vIdContratto


                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    'Da RAPPORTI_UTENZA

                    luogo_cor = par.IfNull(myReaderA("luogo_cor"), "")
                    civico_cor = par.IfNull(myReaderA("civico_cor"), "")
                    cap_cor = par.IfNull(myReaderA("cap_cor"), "")

                    indirizzo_cor = par.IfNull(myReaderA("VIA_cor"), "")
                    tipo_cor = par.IfNull(myReaderA("tipo_cor"), "")
                    sigla_cor = par.IfNull(myReaderA("sigla_cor"), "")

                End If
                myReaderA.Close()

                'ANAGRAFICA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA where ID=" & IdAnagrafica
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then

                    If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                        Cognome = par.IfNull(myReaderA("ragione_sociale"), "")
                        Nome = ""
                        CF = par.IfNull(myReaderA("partita_iva"), "")
                    Else
                        Cognome = par.IfNull(myReaderA("cognome"), "")
                        Nome = par.IfNull(myReaderA("nome"), "")
                        CF = par.IfNull(myReaderA("cod_fiscale"), "")
                    End If
                End If
                myReaderA.Close()



                'MAV

                'If Session.Item("AmbienteDiTest") = "1" Then
                '    causalepagamento = "COMMITEST01"
                '    'pp.Url = "https://web1.unimaticaspa.it/pagamenti20-test-ws/services/MAVOnline"
                '    pp.Url = "https://demoweb.infogroup.it/pagamenti20-test-ws/services/MAVOnline"

                'End If
                If Session.Item("AmbienteDiTest") = "1" Then
                    causalepagamento = "COMMITEST01"
                    'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                    pp.Url = Session.Item("indirizzoMavOnLine")
                Else
                    'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                    pp.Url = Session.Item("indirizzoMavOnLine")
                End If


                RichiestaEmissioneMAV.codiceEnte = "commi"
                RichiestaEmissioneMAV.tipoPagamento = causalepagamento
                RichiestaEmissioneMAV.idOperazione = Format(ID_BOLLETTA, "0000000000")
                RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(IdAnagrafica), "0000000000")

                RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA)

                RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
                RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

                RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                If Nome <> "" Then
                    RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
                End If

                If Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor) <= 23 Then
                    RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
                Else
                    RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
                    RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
                End If

                RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
                RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
                RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
                RichiestaEmissioneMAV.nazioneDebitore = "IT"

                Try
                    '12/01/2015 PUCCIA Nuova connessione  tls ssl
                    If DisabilitaExpect100Continue = "1" Then
                        System.Net.ServicePointManager.Expect100Continue = False
                    End If
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler

                    '/*/*/*/*/*tls v1
                    Dim v As String = ""
                    par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                    v = par.cmd.ExecuteScalar
                    System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                    '/*/*/*/*/*tls v1

                    Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                Catch ex As Exception

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        par.myTrans.Rollback()
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                    End If

                    'par.cmd.Dispose()
                    'par.OracleConn.Close()
                    'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Session.Item("LAVORAZIONE") = "0"

                    Response.Write("<p style='color: #FF0000; font-weight: bold'>" & ex.Message & " !</p>")
                    Exit Function
                End Try

                If Esito.codiceRisultato = "0" Then
                    'If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                    '    pdfMerge.AppendPDFFile(url & ".pdf")
                    '    IO.File.Delete(url & ".htm")
                    'End If

                    'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                    outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".pdf"

                    binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                    outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                    outFile.Write(binaryData, 0, binaryData.Length - 1)
                    outFile.Close()

                    pdfMerge.AppendPDFFile(outputFileName)


                    ' se la banca emette correttamente i MAV allora:
                    ' SETTO a BOL_BOLLETTE che è stato stampato e il numero di bollettino
                    num_bollettino = Esito.numeroMAV
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                       & " set   FL_STAMPATO='1'," _
                                       & "       rif_bollettino='" & num_bollettino & "'" _
                                       & " where ID=" & ID_BOLLETTA
                    par.cmd.ExecuteNonQuery()


                    
                    'UPDATE BOL_BOLLETTE VECCHIE raggruppate per la BOLLETTA MOROSITA' NUOVA
                    ' & "  IMPORTO_RICLASSIFICATO=NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) " _

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                        & " set   ID_BOLLETTA_RIC=" & ID_BOLLETTA & "," _
                                        & "       ID_MOROSITA=" & vIdMorosita _
                                        & " where ID_CONTRATTO=" & vIdContratto _
                                        & "   and FL_ANNULLATA=0 " _
                                        & "   and ID_MOROSITA is Null  " _
                                        & "   and ID_BOLLETTA_RIC Is NULL " _
                                        & "   and ID_RATEIZZAZIONE is null " _
                                        & "   and ID<0 " _
                                        & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                        & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                        & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                        & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()
                    '************************************

                    'ORIGINALE da PUCCIA
                    'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) WHERE ID_BOLLETTA IN (" & vIdBolletta & ") AND ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                       & " set  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) " _
                                       & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                            & "  where ID_BOLLETTA_RIC=" & ID_BOLLETTA _
                                                            & "    and ID_MOROSITA=" & vIdMorosita _
                                                            & "    and FL_ANNULLATA=0 " _
                                                            & "    and ID_CONTRATTO=" & vIdContratto & ") " _
                                      & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()


                    'IMPORTO_CANONE    
                    ValSommaImportoCanone = 0
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                      & "  from SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA " _
                                      & "  where BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID " _
                                      & "    and T_VOCI_BOLLETTA.GRUPPO=4 " _
                                      & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                             & " where ID_MOROSITA=" & vIdMorosita _
                                                             & "   and FL_ANNULLATA=0 " _
                                                             & "   and ID_CONTRATTO=" & vIdContratto _
                                                             & "   and ID>0 " _
                                                             & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        ValSommaImportoCanone = par.IfNull(myReaderA(0), 0)
                    End If
                    myReaderA.Close()
                    '***********************************

                    'IMPORTO_ONERI    
                    ValSommaImportoOneri = 0
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                      & "  from SISCOM_MI.BOL_BOLLETTE_VOCI" _
                                      & "  where ID_VOCE NOT IN (select ID from SISCOM_MI.T_VOCI_BOLLETTA where GRUPPO=4) " _
                                      & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                             & " where ID_MOROSITA=" & vIdMorosita _
                                                             & "   and FL_ANNULLATA=0 " _
                                                             & "   and ID_CONTRATTO=" & vIdContratto _
                                                             & "   and ID>0 " _
                                                             & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        ValSommaImportoOneri = par.IfNull(myReaderA(0), 0)
                    End If
                    myReaderA.Close()
                    '***********************************

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update  SISCOM_MI.MOROSITA_LETTERE " _
                                    & "     set BOLLETTINO='" & num_bollettino & "'," _
                                    & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                    & "         COD_STATO='M99'," _
                                    & "         EMISSIONE='" & Format(Now, "yyyyMMdd") & "'," _
                                    & "         INIZIO_PERIODO='" & sDataDA & "'," _
                                    & "         IMPORTO=" & par.VirgoleInPunti(ValSomma) & "," _
                                    & "         IMPORTO_INIZIALE=" & par.VirgoleInPunti(ValSomma) & "," _
                                    & "         IMPORTO_BOLLETTA=" & par.VirgoleInPunti(Tot_Bolletta) & "," _
                                    & "         IMPORTO_CANONE= " & par.VirgoleInPunti(ValSommaImportoCanone) & "," _
                                    & "         IMPORTO_ONERI= " & par.VirgoleInPunti(ValSommaImportoOneri) _
                                    & " where ID_MOROSITA = " & vIdMorosita _
                                    & "   and BOLLETTINO=(select RIF_BOLLETTINO from SISCOM_MI.BOL_BOLLETTE " _
                                                      & " where ID=" & lBOLLETTE_ID & ")"

                    par.cmd.ExecuteNonQuery()


                    '****************MYEVENT*****************
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                  & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", iEsito)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", "Effettuati dei pagamenti, aggiornato il MAV e consegnato al destinatario"))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '************************************************



                Else
                    'lblErrore.Visible = True

                    ' se la banca NON emette correttamente i MAV allora:

                    ' Con la RollBack annullo eventuali inserimenti o modifiche


                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans.Rollback()

                        par.myTrans = par.OracleConn.BeginTransaction()
                        ‘‘par.cmd.Transaction = par.myTrans
                        HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

                    End If

                    Dim FileDaCreare As String = Format(ID_BOLLETTA, "0000000000")
                    If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml")) = True Then
                        FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                    End If

                    'Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")
                    Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")

                    'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml"
                    outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & FileDaCreare & ".xml"

                    binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                    outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                    outFile.Write(binaryData, 0, binaryData.Length)
                    outFile.Close()
                    Exit Function

                End If
                '**********
            End If




            '**** MA INIZIO ***************************************************************************************
            If bTrovatoMA = True Then
                'Aggiornate MAV MA dopo il 2009

                NoteBollette = "MOROSITA' MA"
                TipoIngiunzione = "RECUPERO MOROSITA'"

                Tot_Bolletta = 0

                'Ricavo ID della lettera (MOROSITA_LETTERE)
                sStr1 = "select ID from SISCOM_MI.MOROSITA_LETTERE " _
                    & " where ID_MOROSITA = " & vIdMorosita _
                    & "   and ID_CONTRATTO=" & vIdContratto _
                    & "   and BOLLETTINO=(select RIF_BOLLETTINO from SISCOM_MI.BOL_BOLLETTE " _
                                      & " where NOTE='" & par.PulisciStrSql(NoteBollette) & "'" _
                                      & "   and ID_MOROSITA=" & vIdMorosita _
                                      & "   and ID_CONTRATTO=" & vIdContratto _
                                      & "   and FL_ANNULLATA=0 )"


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    vIdMorositaLettera = par.IfNull(myReaderB(0), 0)
                End If
                myReaderB.Close()


                'RICAVO ID del MAV Vecchio
                sStr1 = "select ID from SISCOM_MI.BOL_BOLLETTE " _
                     & " where  NOTE='" & par.PulisciStrSql(NoteBollette) & "'" _
                     & "   and ID_MOROSITA=" & vIdMorosita _
                     & "   and ID_CONTRATTO=" & vIdContratto _
                     & "   and ID_BOLLETTA_RIC is null " _
                     & "   and ID_TIPO=4 " _
                     & "   and FL_ANNULLATA=0 "

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    lBOLLETTE_ID = par.IfNull(myReaderB(0), 0)
                End If
                myReaderB.Close()


                'RIPRISTINO IMPORTO_RICLASSIFICATO di TUTTE le BOLLETTE VOCI interesate dal MAV vecchio
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                   & " set  IMPORTO_RICLASSIFICATO = Null " _
                                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                        & "  where ID_BOLLETTA_RIC=" & lBOLLETTE_ID _
                                                        & "    and ID_MOROSITA=" & vIdMorosita _
                                                        & "    and FL_ANNULLATA=0 " _
                                                        & "    and ID_CONTRATTO=" & vIdContratto _
                                                        & "    and ID_BOLLETTA_RIC IS NOT NULL " _
                                                        & "    and ID_RATEIZZAZIONE IS NULL ) " _
                                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()

                'RIPRISTINO TUTTE le BOLLETTE interesate dal MAV vecchio
                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                                   & " set ID_BOLLETTA_RIC=Null," _
                                   & "     ID_MOROSITA=Null " _
                                   & " where ID_MOROSITA=" & vIdMorosita _
                                   & "   and ID_BOLLETTa_RIC=" & lBOLLETTE_ID _
                                   & "   and ID_CONTRATTO=" & vIdContratto _
                                   & "   and FL_ANNULLATA=0 " _
                                   & "   and ID_BOLLETTA_RIC IS NOT NULL " _
                                   & "   and ID_RATEIZZAZIONE IS NULL "

                par.cmd.ExecuteNonQuery()

                'ANNULLO il MAV Vecchio
                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                                   & " set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'" _
                                   & " where ID=" & lBOLLETTE_ID
                par.cmd.ExecuteNonQuery()

                '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                ' SOLO DOPO il 2009 (ricavo la somma da Pagare ancora)
                sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
                       & " from SISCOM_MI.BOL_BOLLETTE " _
                       & " where ID_CONTRATTO=" & vIdContratto _
                       & "   and ID>0 " _
                       & "   and FL_ANNULLATA=0 " _
                       & "   and ID_MOROSITA is Null  " _
                       & "   and ID_BOLLETTA_RIC is null  " _
                       & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                       & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                       & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    ValSomma = par.IfNull(myReaderB(0), 0)
                End If
                myReaderB.Close()



                If ValSomma <= 0 Then

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                        & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA)) & "'" _
                                        & " where ID= " & vIdMorositaLettera

                    par.cmd.ExecuteNonQuery()


                    '****************MYEVENT*****************
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                  & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", "Effettuato il Pagamento"))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '************************************************


                    'COMMIT
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        par.myTrans.Commit()
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                    End If
                    par.cmd.Dispose()

                    Exit Function
                End If

                '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "   select MIN(RIFERIMENTO_DA)  " _
                                      & " from SISCOM_MI.BOL_BOLLETTE " _
                                      & " where ID_CONTRATTO=" & vIdContratto _
                                      & "   and ID>0 " _
                                      & "   and FL_ANNULLATA=0 " _
                                      & "   and ID_MOROSITA is Null  " _
                                      & "   and ID_BOLLETTA_RIC Is NULL " _
                                      & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                      & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                      & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    sDataDA = par.IfNull(myReaderB(0), "")
                End If
                myReaderB.Close()
                '******************************************

                '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "   select MAX(RIFERIMENTO_A)  " _
                                      & " from SISCOM_MI.BOL_BOLLETTE " _
                                      & " where ID_CONTRATTO=" & vIdContratto _
                                      & "   and ID>0 " _
                                      & "   and FL_ANNULLATA=0 " _
                                      & "   and ID_MOROSITA is Null  " _
                                      & "   and ID_BOLLETTA_RIC Is NULL " _
                                      & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                      & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                      & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    sDataA = par.IfNull(myReaderB(0), "")
                End If
                myReaderB.Close()
                '******************************************



                '' '' Ricavo ID_BOLLETTA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL FROM dual "
                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    ID_BOLLETTA = myReaderB(0)

                    If ID_BOLLETTE_NUOVE = "" Then
                        ID_BOLLETTE_NUOVE = ID_BOLLETTA
                    Else
                        ID_BOLLETTE_NUOVE = ID_BOLLETTE_NUOVE & "," & ID_BOLLETTA
                    End If
                End If
                myReaderB.Close()



                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select * from SISCOM_MI.BOL_BOLLETTE " _
                                    & " where ID=" & lBOLLETTE_ID

                myReaderB = par.cmd.ExecuteReader()
                If myReaderB.Read Then

                    IdAnagrafica = par.IfNull(myReaderB("COD_AFFITTUARIO"), -1)

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
                                                & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, " _
                                                & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA,ID_TIPO) " _
                                        & "values (:id,:n_rata,:data_emissione,:data_scadenza," _
                                                & ":note,:id_contratto,:id_esercizio_f," _
                                                & ":id_unita,:fl_annullata,:pagabile_presso,:cod_affittuario,:intestatario," _
                                                & ":indirizzo,:cap_citta,:presso,:riferimento_da,:riferimento_a," _
                                                & ":fl_stampato,:id_complesso,:data_ins_pagamento,:importo_pagato,:note_pagamento," _
                                                & ":anno,:operatore_pag,:id_edificio,:data_annullo_pag,:operatore_annullo_pag,:rif_file,:id_morosita,4)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", ID_BOLLETTA))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_rata", 999))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_emissione", Format(Now, "yyyyMMdd")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", ScadenzaBollettino))

                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_1_sollecito", "NULL"))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_2_sollecito", "NULL"))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_pagamento", "NULL"))

                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", NoteBollette))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_CONTRATTO"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_esercizio_f", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_ESERCIZIO_F"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_UNITA"), -1))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_annullata", "0"))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pagabile_presso", ""))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_affittuario", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("COD_AFFITTUARIO"), -1))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("intestatario", par.IfNull(myReaderB("INTESTATARIO"), "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", par.IfNull(myReaderB("INDIRIZZO"), "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap_citta", par.IfNull(myReaderB("CAP_CITTA"), "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("presso", par.IfNull(myReaderB("PRESSO"), "")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_da", sDataDA))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_a", sDataA))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_stampato", "0"))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_COMPLESSO"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ins_pagamento", ""))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_pagato", DBNull.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note_pagamento", ""))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Year(Now)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_pag", ""))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(par.IfNull(myReaderB("ID_EDIFICIO"), -1))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_annullo_pag", DBNull.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_annullo_pag", DBNull.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_file", "MOR"))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorosita))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()

                End If
                myReaderB.Close()


                Dim VOCE = "150" '"626"

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                    & " values " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                            & ID_BOLLETTA & "," _
                                            & VOCE & "," _
                                            & par.VirgoleInPunti(ValSomma) & ")"
                par.cmd.ExecuteNonQuery()
                Tot_Bolletta = Tot_Bolletta + ValSomma

                If NoteBollette = "MOROSITA' MA" Then
                    'SPESE DI NOTIFICA (628)
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                       & " values " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                            & ID_BOLLETTA _
                                            & ",628," _
                                            & par.VirgoleInPunti(spese_notifica) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + spese_notifica
                End If


                'SPESE MAV
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                   & " values " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                        & ID_BOLLETTA _
                                        & ",407," _
                                        & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                par.cmd.ExecuteNonQuery()
                Tot_Bolletta = Tot_Bolletta + SPESEmav

                'BOLLO
                If Tot_Bolletta >= APPLICABOLLO Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                       & " values " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                & ID_BOLLETTA _
                                                & ",95," _
                                                & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + BOLLO
                End If
                '******************************************************


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select RAPPORTI_UTENZA.* " _
                                   & " from SISCOM_MI.RAPPORTI_UTENZA" _
                                   & " where ID=" & vIdContratto


                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    'Da RAPPORTI_UTENZA

                    luogo_cor = par.IfNull(myReaderA("luogo_cor"), "")
                    civico_cor = par.IfNull(myReaderA("civico_cor"), "")
                    cap_cor = par.IfNull(myReaderA("cap_cor"), "")

                    indirizzo_cor = par.IfNull(myReaderA("VIA_cor"), "")
                    tipo_cor = par.IfNull(myReaderA("tipo_cor"), "")
                    sigla_cor = par.IfNull(myReaderA("sigla_cor"), "")

                End If
                myReaderA.Close()

                'ANAGRAFICA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA where ID=" & IdAnagrafica
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then

                    If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                        Cognome = par.IfNull(myReaderA("ragione_sociale"), "")
                        Nome = ""
                        CF = par.IfNull(myReaderA("partita_iva"), "")
                    Else
                        Cognome = par.IfNull(myReaderA("cognome"), "")
                        Nome = par.IfNull(myReaderA("nome"), "")
                        CF = par.IfNull(myReaderA("cod_fiscale"), "")
                    End If
                End If
                myReaderA.Close()


                'If Session.Item("AmbienteDiTest") = "1" Then
                '    causalepagamento = "COMMITEST01"
                '    'pp.Url = "https://web1.unimaticaspa.it/pagamenti20-test-ws/services/MAVOnline"
                '    pp.Url = "https://demoweb.infogroup.it/pagamenti20-test-ws/services/MAVOnline"

                'End If
                If Session.Item("AmbienteDiTest") = "1" Then
                    causalepagamento = "COMMITEST01"
                    'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                    pp.Url = Session.Item("indirizzoMavOnLine")
                Else
                    'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                    pp.Url = Session.Item("indirizzoMavOnLine")
                End If


                RichiestaEmissioneMAV.codiceEnte = "commi"
                RichiestaEmissioneMAV.tipoPagamento = causalepagamento
                RichiestaEmissioneMAV.idOperazione = Format(ID_BOLLETTA, "0000000000")
                RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(IdAnagrafica), "0000000000")

                RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA)

                RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
                RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

                RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                If Nome <> "" Then
                    RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
                End If


                If Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor) <= 23 Then
                    RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
                Else
                    RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
                    RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
                End If

                RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
                RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
                RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
                RichiestaEmissioneMAV.nazioneDebitore = "IT"

                Try
                    '12/01/2015 PUCCIA Nuova connessione  tls ssl
                    If DisabilitaExpect100Continue = "1" Then
                        System.Net.ServicePointManager.Expect100Continue = False
                    End If
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler

                    '/*/*/*/*/*tls v1
                    Dim v As String = ""
                    par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                    v = par.cmd.ExecuteScalar
                    System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                    '/*/*/*/*/*tls v1

                    Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                Catch ex As Exception

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        par.myTrans.Rollback()
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                    End If

                    CreaMAV = ""

                    'par.cmd.Parameters.Clear()
                    'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                    'par.cmd.ExecuteNonQuery()

                    'par.myTrans.Commit()
                    'par.cmd.Dispose()
                    'par.OracleConn.Close()
                    'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Session.Item("LAVORAZIONE") = "0"
                    Response.Write("<p style='color: #FF0000; font-weight: bold'>" & ex.Message & " !</p>")
                    Exit Function
                End Try

                If Esito.codiceRisultato = "0" Then
                    'If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                    '    pdfMerge.AppendPDFFile(url & ".pdf")
                    '    IO.File.Delete(url & ".htm")
                    'End If

                    'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                    outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".pdf"

                    binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                    outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                    outFile.Write(binaryData, 0, binaryData.Length - 1)
                    outFile.Close()

                    pdfMerge.AppendPDFFile(outputFileName)


                    ' se la banca emette correttamente i MAV allora:
                    ' SETTO a BOL_BOLLETTE che è stato stampato e il numero di bollettino
                    num_bollettino = Esito.numeroMAV
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                       & " set   FL_STAMPATO='1'," _
                                       & "       rif_bollettino='" & num_bollettino & "'" _
                                       & " where ID=" & ID_BOLLETTA
                    par.cmd.ExecuteNonQuery()



                    'UPDATE BOL_BOLLETTE VECCHIE raggruppate per la BOLLETTA MOROSITA' NUOVA
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                        & " set   ID_BOLLETTA_RIC=" & ID_BOLLETTA & "," _
                                        & "       ID_MOROSITA=" & vIdMorosita _
                                        & " where ID_CONTRATTO=" & vIdContratto _
                                        & "   and ID>0 " _
                                        & "   and FL_ANNULLATA=0 " _
                                        & "   and ID_MOROSITA is Null  " _
                                        & "   and ID_BOLLETTA_RIC is null  " _
                                        & "   and ID_RATEIZZAZIONE is null " _
                                        & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                        & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()
                    '************************************

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                       & " set  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) " _
                                       & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                            & "  where ID_BOLLETTA_RIC=" & ID_BOLLETTA _
                                                            & "    and ID_MOROSITA=" & vIdMorosita _
                                                            & "    and FL_ANNULLATA=0 " _
                                                            & "    and ID_CONTRATTO=" & vIdContratto & ") " _
                                      & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                    'IMPORTO_CANONE    
                    ValSommaImportoCanone = 0
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                      & "  from SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA " _
                                      & "  where BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID " _
                                      & "    and T_VOCI_BOLLETTA.GRUPPO=4 " _
                                      & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                             & " where ID_MOROSITA=" & vIdMorosita _
                                                             & "   and FL_ANNULLATA=0 " _
                                                             & "   and ID_CONTRATTO=" & vIdContratto _
                                                             & "   and ID>0 " _
                                                             & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        ValSommaImportoCanone = par.IfNull(myReaderA(0), 0)
                    End If
                    myReaderA.Close()
                    '***********************************

                    'IMPORTO_ONERI    
                    ValSommaImportoOneri = 0
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                      & "  from SISCOM_MI.BOL_BOLLETTE_VOCI" _
                                      & "  where ID_VOCE NOT IN (select ID from SISCOM_MI.T_VOCI_BOLLETTA where GRUPPO=4) " _
                                      & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                             & " where ID_MOROSITA=" & vIdMorosita _
                                                             & "   and FL_ANNULLATA=0 " _
                                                             & "   and ID_CONTRATTO=" & vIdContratto _
                                                             & "   and ID>0 " _
                                                             & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        ValSommaImportoOneri = par.IfNull(myReaderA(0), 0)
                    End If
                    myReaderA.Close()
                    '***********************************


                    'Aggiorno le informazioni di MOROSITA_LETTERE
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update  SISCOM_MI.MOROSITA_LETTERE " _
                                      & "     set BOLLETTINO='" & num_bollettino & "'," _
                                      & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                      & "         COD_STATO='M99'," _
                                      & "         EMISSIONE='" & Format(Now, "yyyyMMdd") & "'," _
                                      & "         INIZIO_PERIODO='" & sDataDA & "'," _
                                      & "         FINE_PERIODO='" & sDataA & "'," _
                                      & "         IMPORTO=" & par.VirgoleInPunti(ValSomma) & "," _
                                      & "         IMPORTO_INIZIALE=" & par.VirgoleInPunti(ValSomma) & "," _
                                      & "         IMPORTO_BOLLETTA=" & par.VirgoleInPunti(Tot_Bolletta) & "," _
                                      & "         IMPORTO_CANONE= " & par.VirgoleInPunti(ValSommaImportoCanone) & "," _
                                      & "         IMPORTO_ONERI= " & par.VirgoleInPunti(ValSommaImportoOneri) _
                                      & " where ID_MOROSITA = " & vIdMorosita _
                                      & "   and BOLLETTINO=(select RIF_BOLLETTINO from SISCOM_MI.BOL_BOLLETTE " _
                                                        & " where ID=" & lBOLLETTE_ID & ")"

                    par.cmd.ExecuteNonQuery()


                    '****************MYEVENT*****************
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                  & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", iEsito)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", "Effettuati dei pagamenti, aggiornato il MAV e consegnato al destinatario"))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '************************************************



                    ' Response.Redirect("ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf")

                Else
                    'lblErrore.Visible = True

                    ' se la banca NON emette correttamente i MAV allora:
                    ' Con la RollBack annullo eventuali inserimenti o modifiche

                    'If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    '    par.myTrans.Rollback()

                    '    par.myTrans = par.OracleConn.BeginTransaction()
                    '    ‘‘par.cmd.Transaction = par.myTrans
                    '    HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)

                    'End If

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        par.myTrans.Rollback()
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
                    End If

                    CreaMAV = ""


                    'par.cmd.Parameters.Clear()
                    'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                    'par.cmd.ExecuteNonQuery()

                    'Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")

                    Dim FileDaCreare As String = Format(ID_BOLLETTA, "0000000000")
                    If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml")) = True Then
                        FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                    End If

                    Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")

                    'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml"
                    outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & FileDaCreare & ".xml"

                    binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                    outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                    outFile.Write(binaryData, 0, binaryData.Length)
                    outFile.Close()

                    Exit Function
                End If
            End If


            'COMMIT
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Commit()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            par.cmd.Dispose()

            CreaMAV = ID_BOLLETTE_NUOVE



        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try



    End Function

    Protected Sub btn_InserisciEsitoManuale_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciEsitoManuale.Click


        If Me.cmbElencoEsiti.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare l\'esito del PostAler!');</script>")
            Exit Sub
        End If


        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
            End If

            Dim sStr1 As String = " Aggiornamento Esisto PostAler effettuato manualmente. " + ControlChars.CrLf

            '****************MYEVENT*****************
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                          & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Me.cmbElencoEsiti.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1 & Me.txtNoteEsito.Text))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************************

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Me.cmbElencoEsiti.SelectedValue)) & "'" _
                                & " where ID=" & vIdMorositaLettera

            par.cmd.ExecuteNonQuery()

            'Se c'è il secondo MAV
            par.cmd.CommandText = " select ID from SISCOM_MI.MOROSITA_LETTERE  " _
                    & " where ID_MOROSITA=" & vIdMorosita _
                    & "  and  ID_CONTRATTO=" & vIdContratto _
                    & "  and  ID<>" & vIdMorositaLettera

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Dim Id2 As Long = 0

            If myReaderT.Read Then
                Id2 = par.IfNull(myReaderT("ID"), -1)
            End If
            myReaderT.Close()

            If Id2 <> 0 Then

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
                                    & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Me.cmbElencoEsiti.SelectedValue)) & "'" _
                                    & " where ID=" & Id2

                par.cmd.ExecuteNonQuery()


                ''****************MYEVENT*****************
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", Id2))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Me.cmbElencoEsiti.SelectedValue))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1 & Me.txtNoteEsito.Text))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '************************************************

            End If



            ' COMMIT
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Commit()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            par.cmd.Dispose()
            par.cmd.CommandText = ""


            Response.Write("<script>alert('Aggiornamento esito PostAler effettuato con successo!')</script>")

            Me.txtAppareEsitoManuale.Value = 0

            SettaScelte(0)


        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Function CreaLetteraMAV() As Long

        Dim APPLICABOLLO As Double = 0
        Dim SPESEmav As Double = 0
        Dim BOLLO As Double = 0
        Dim spese_notifica As Double = 0
        Dim Tot_Bolletta As Double = 0
        Dim causalepagamento As String = ""

        Dim ScadenzaBollettino As String = ""

        Dim lBOLLETTE_ID As Long

        Dim TrovatoAB As Boolean = False    '"Ingiunzione_Aa.htm" e/o "Ingiunzione_Bb.htm"
        Dim TrovatoCD As Boolean = False    '"Ingiunzione_C.htm"  e/o "Ingiunzione_D.htm"
        Dim TrovatoEF As Boolean = False    '"Ingiunzione_E.htm"  e/o "Ingiunzione_F.htm"

        Dim sStr1 As String = ""

        Dim ValSomma As Decimal = 0
        Dim sDataDA As String = ""
        Dim sDataA As String = ""

        Dim Riassunto As String = ""

        Dim vIdMorositaLetteraFILE As Long = -1
        Dim sNomeFile As String
        Dim contenutoRiassunto As String = ""
        Dim myExcelFile As New CM.ExcelFile

        Dim idMorositaLettere As Long

        Dim TipoIngiunzione As String = ""
        Dim Importo As String = "0"
        Dim CodiceContratto As String = ""
        Dim VOCE As String = ""
        Dim IdAnagrafica As String = "-1"

        Dim presso_cor As String = ""
        Dim civico_cor As String = ""
        Dim luogo_cor As String = ""
        Dim cap_cor As String = ""
        Dim indirizzo_cor As String = ""
        Dim tipo_cor As String = ""
        Dim sigla_cor As String = ""

        Dim sSCALA As String = ""
        Dim sINTERNO As String = ""

        Dim TipoStampa As String = ""
        Dim NoteBollette As String = ""

        Dim sPosteAler As String = ""               'TUTTI i CAMPI
        Dim sPosteAlerNominativo As String = ""     '1)  Nominativo Postale (50)
        Dim sPosteAlerInd As String = ""            '3)  Indirizzo          (50)
        Dim sPosteAlerScala As String = ""          '6)  Scala              (2)
        Dim sPosteAlerInterno As String = ""        '7)  Interno            (3)
        Dim sPosteAlerCAP As String = ""            '8)  CAP                (5)
        Dim sPosteAlerLocalita As String = ""       '9)  Località           (50)
        Dim sPosteAlerProv As String = ""           '10) Provincia          (2)
        Dim sPosteAlerCodUtente As String = ""      '11) Codice Utente      (12)
        Dim sPosteAlerAcronimo As String = ""       '12) Acronimo           (4)
        Dim sPosteAlerIA As String = ""             '13) IA                 (16)
        Dim sPosteDefault As String = ""            ' per i campi 2-4-5 (Presso, casella postale, indirizzo casella postale)

        Dim sNomeFiliale As String = ""
        Dim sNumTel_Filiale As String = ""
        Dim sIndirizzo_Filiale As String = ""

        Dim PeriodoXLS_INIZIO As Long = 0
        Dim PeriodoXLS_FINE As Long = 0

        Dim periodo As String = ""

        Dim ValImporto1 As Decimal = 0
        Dim ValImporto2 As Decimal = 0

        Dim url As String

        Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
        Dim pp As New MavOnline.MAVOnlineBeanService
        Dim Esito As New MavOnline.rispostaMAVOnlineWS

        Dim outputFileName As String = ""
        Dim outFile As System.IO.FileStream
        Dim binaryData() As Byte

        Dim num_bollettino As String = ""

        CreaLetteraMAV = -1

        'Try

        '    ' RIPRENDO LA CONNESSIONE
        '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '    par.SettaCommand(par)

        '    'RIPRENDO LA TRANSAZIONE
        '    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
        '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '        ‘‘par.cmd.Transaction = par.myTrans
        '    Else
        '        'CREO LA TRANSAZIONE
        '        par.myTrans = par.OracleConn.BeginTransaction()
        '        ‘‘par.cmd.Transaction = par.myTrans
        '        HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
        '    End If


        '    '1) PARAMETRI BOLLETTA
        '    par.cmd.Parameters.Clear()
        '    par.cmd.CommandText = "select * from SISCOM_MI.PARAMETRI_BOLLETTA "
        '    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        '    While myReaderA.Read
        '        Select Case par.IfNull(myReaderA("ID"), 0)
        '            Case "25"
        '                APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
        '            Case "26"
        '                SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
        '            Case "0"
        '                BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
        '            Case "36" '"ex 34"
        '                spese_notifica = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
        '            Case "32"
        '                causalepagamento = par.IfNull(myReaderA("VALORE"), "")
        '        End Select
        '    End While
        '    myReaderA.Close()
        '    '*********************************************

        '    'NOTA: prima del 02/Sett/2001 era di 40 gg ora è di 60 + 20 + controllo se capita di sabato o domenica
        '    ScadenzaBollettino = par.AggiustaData(DateAdd("d", 80, Now.Date))

        '    Dim d1 As Date
        '    d1 = New Date(Mid(ScadenzaBollettino, 1, 4), Mid(ScadenzaBollettino, 5, 2), Mid(ScadenzaBollettino, 7, 2))
        '    If d1.DayOfWeek = DayOfWeek.Tuesday Then
        '        ScadenzaBollettino = par.AggiustaData(DateAdd("d", 82, Format(Now, "yyyyMMdd")))
        '    ElseIf d1.DayOfWeek = DayOfWeek.Wednesday Then
        '        ScadenzaBollettino = par.AggiustaData(DateAdd("d", 81, Format(Now, "yyyyMMdd")))
        '    End If
        '    '************

        '    'DETTAGLI MAV MG e MAV MA
        '    par.cmd.Parameters.Clear()
        '    par.cmd.CommandText = " select * from SISCOM_MI.MOROSITA_LETTERE  " _
        '                        & " where ID_MOROSITA=" & vIdMorosita _
        '                        & "   and ID_CONTRATTO=" & vIdContratto

        '    Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        '    While myReaderT.Read

        '        If par.IfNull(myReaderT("TIPO_LETTERA"), "") = "AB" Then
        '            TrovatoAB = True
        '        End If

        '        If par.IfNull(myReaderT("TIPO_LETTERA"), "") = "CD" Then
        '            TrovatoCD = True
        '        End If

        '        If par.IfNull(myReaderT("TIPO_LETTERA"), "") = "EF" Then
        '            TrovatoEF = True
        '        End If

        '        'RICAVO ID del MAV Vecchio
        '        par.cmd.Parameters.Clear()
        '        par.cmd.CommandText = " select ID from SISCOM_MI.BOL_BOLLETTE " _
        '                             & " where  RIF_BOLLETTINO='" & par.IfNull(myReaderT("BOLLETTINO"), "") & "'" _
        '                             & "   and ID_MOROSITA=" & vIdMorosita _
        '                             & "   and ID_CONTRATTO=" & vIdContratto _
        '                             & "   and ID_BOLLETTA_RIC is null " _
        '                             & "   and ID_TIPO=4 " _
        '                             & "   and FL_ANNULLATA=0 "


        '        myReaderA = par.cmd.ExecuteReader()
        '        If myReaderA.Read Then
        '            lBOLLETTE_ID = par.IfNull(myReaderA(0), 0)
        '        End If
        '        myReaderA.Close()


        '        'RIPRISTINO TUTTE le BOLLETTE interesate dal MAV vecchio
        '        par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
        '                           & " set ID_BOLLETTA_RIC=Null," _
        '                           & "     ID_MOROSITA=Null," _
        '                           & "     IMPORTO_RICLASSIFICATO=Null " _
        '                           & " where ID_MOROSITA=" & vIdMorosita _
        '                           & "   and ID_BOLLETTa_RIC=" & lBOLLETTE_ID _
        '                           & "   and ID_CONTRATTO=" & vIdContratto _
        '                           & "   and FL_ANNULLATA=0 " _
        '                           & "   and ID_BOLLETTA_RIC IS NOT NULL " _
        '                           & "   and ID_RATEIZZAZIONE IS NULL "

        '        par.cmd.ExecuteNonQuery()

        '        'ANNULLO il MAV Vecchio
        '        par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
        '                           & " set FL_ANNULLATA=1" _
        '                           & " where ID=" & lBOLLETTE_ID
        '        par.cmd.ExecuteNonQuery()


        '    End While
        '    myReaderT.Close()



        '    'DETTAGLI MAV MG e MAV MA
        '    par.cmd.Parameters.Clear()
        '    par.cmd.CommandText = " select * from SISCOM_MI.MOROSITA_LETTERE  " _
        '                        & " where ID_MOROSITA=" & vIdMorosita _
        '                        & "   and ID_CONTRATTO=" & vIdContratto _
        '                        & " order by NUM_LETTERE "

        '    myReaderT = par.cmd.ExecuteReader()

        '    While myReaderT.Read
        '        If (TrovatoAB = True And par.IfNull(myReaderT("NUM_LETTERE"), 1) = 1) Or (TrovatoCD = True And par.IfNull(myReaderT("NUM_LETTERE"), 1) = 1) Then

        '            'MAV MA
        '            ' SOLO DOPO il 2009 (ricavo la somma da Pagare ancora)
        '            sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
        '                   & " from SISCOM_MI.BOL_BOLLETTE " _
        '                   & " where ID_CONTRATTO=" & vIdContratto _
        '                   & "   and ID>0 " _
        '                   & "   and FL_ANNULLATA=0 " _
        '                   & "   and ID_BOLLETTA_RIC is null  and ID_RATEIZZAZIONE is null " _
        '                   & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
        '                   & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

        '            par.cmd.Parameters.Clear()
        '            par.cmd.CommandText = sStr1
        '            myReaderA = par.cmd.ExecuteReader()
        '            If myReaderA.Read Then
        '                ValSomma = par.IfNull(myReaderA(0), 0)
        '            End If
        '            myReaderA.Close()

        '            If ValSomma <= 0 Then

        '                par.cmd.Parameters.Clear()
        '                par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
        '                                    & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA)) & "'" _
        '                                    & " where ID=" & par.IfNull(myReaderT("ID"), -1)


        '                par.cmd.ExecuteNonQuery()

        '                '****************MYEVENT*****************
        '                par.cmd.Parameters.Clear()
        '                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
        '                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", par.IfNull(myReaderT("ID"), -1)))
        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA))))
        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", "Effettuato il Pagamento"))

        '                par.cmd.ExecuteNonQuery()
        '                par.cmd.CommandText = ""
        '                par.cmd.Parameters.Clear()
        '                '************************************************
        '            Else
        '                If vIdMorositaLetteraFILE = -1 Then
        '                    vIdMorositaLetteraFILE = par.IfNull(myReaderT("ID"), -1)
        '                End If

        '                par.cmd.Parameters.Clear()
        '                par.cmd.CommandText = "   select MIN(RIFERIMENTO_DA)  " _
        '                                      & " from SISCOM_MI.BOL_BOLLETTE " _
        '                                      & " where ID_CONTRATTO=" & vIdContratto _
        '                                      & "   and ID>0 " _
        '                                      & "   and FL_ANNULLATA=0 " _
        '                                      & "   and ID_BOLLETTA_RIC Is NULL and ID_RATEIZZAZIONE is null " _
        '                                      & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
        '                                      & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "
        '                myReaderA = par.cmd.ExecuteReader()
        '                If myReaderA.Read Then
        '                    sDataDA = par.IfNull(myReaderA(0), 0)
        '                End If
        '                myReaderA.Close()
        '                '******************************************

        '                par.cmd.CommandText = "update  SISCOM_MI.MOROSITA_LETTERE " _
        '                        & "     set DATA_SCADENZA='" & ScadenzaBollettino & "'," _
        '                        & "         COD_STATO='M99'," _
        '                        & "         EMISSIONE='" & Format(Now, "yyyyMMdd") & "'," _
        '                        & "         INIZIO_PERIODO='" & sDataDA & "'," _
        '                        & "         IMPORTO=" & par.VirgoleInPunti(ValSomma) & "," _
        '                        & "         IMPORTO_INIZIALE=" & par.VirgoleInPunti(ValSomma) _
        '                        & " where ID = " & par.IfNull(myReaderT("ID"), -1)

        '                par.cmd.ExecuteNonQuery()

        '            End If


        '        Else
        '            'MAV MG
        '            sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
        '                 & " from SISCOM_MI.BOL_BOLLETTE " _
        '                 & " where ID_CONTRATTO=" & vIdContratto _
        '                 & "   and FL_ANNULLATA=0 " _
        '                 & "   and ID_BOLLETTA_RIC is null  and ID_RATEIZZAZIONE is null " _
        '                 & "   and ID<0 " _
        '                 & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null)  " _
        '                 & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
        '                 & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

        '            par.cmd.Parameters.Clear()
        '            par.cmd.CommandText = sStr1
        '            myReaderA = par.cmd.ExecuteReader()
        '            If myReaderA.Read Then
        '                ValSomma = par.IfNull(myReaderA(0), 0)
        '            End If
        '            myReaderA.Close()

        '            If ValSomma <= 0 Then

        '                par.cmd.Parameters.Clear()
        '                par.cmd.CommandText = " update  SISCOM_MI.MOROSITA_LETTERE " _
        '                                    & " set COD_STATO='M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA)) & "'" _
        '                                    & " where ID=" & par.IfNull(myReaderT("ID"), -1)


        '                par.cmd.ExecuteNonQuery()

        '                '****************MYEVENT*****************
        '                par.cmd.Parameters.Clear()
        '                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
        '                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", par.IfNull(myReaderT("ID"), -1)))
        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & String.Format("{0:00}", Convert.ToInt32(Epifani.TabEventiMorosita.MOROSITA_CONCLUSA))))
        '                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", "Effettuato il Pagamento"))

        '                par.cmd.ExecuteNonQuery()
        '                par.cmd.CommandText = ""
        '                par.cmd.Parameters.Clear()
        '                '************************************************
        '            Else
        '                If vIdMorositaLetteraFILE = -1 Then
        '                    vIdMorositaLetteraFILE = par.IfNull(myReaderT("ID"), -1)
        '                End If

        '                par.cmd.Parameters.Clear()
        '                par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
        '                                    & " from SISCOM_MI.BOL_BOLLETTE " _
        '                                    & " where ID_CONTRATTO=" & vIdContratto _
        '                                    & "   and FL_ANNULLATA=0 " _
        '                                    & "   and ID_BOLLETTA_RIC Is NULL and ID_RATEIZZAZIONE is null " _
        '                                    & "   and ID<0 " _
        '                                    & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null)   " _
        '                                    & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
        '                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

        '                myReaderA = par.cmd.ExecuteReader()
        '                If myReaderA.Read Then
        '                    sDataDA = par.IfNull(myReaderA(0), 0)
        '                End If
        '                myReaderA.Close()
        '                '******************************************

        '                par.cmd.CommandText = "update  SISCOM_MI.MOROSITA_LETTERE " _
        '                        & "     set DATA_SCADENZA='" & ScadenzaBollettino & "'," _
        '                        & "         COD_STATO='M99'," _
        '                        & "         EMISSIONE='" & Format(Now, "yyyyMMdd") & "'," _
        '                        & "         INIZIO_PERIODO='" & sDataDA & "'," _
        '                        & "         IMPORTO=" & par.VirgoleInPunti(ValSomma) & "," _
        '                        & "         IMPORTO_INIZIALE=" & par.VirgoleInPunti(ValSomma) _
        '                        & " where ID = " & par.IfNull(myReaderT("ID"), -1)

        '                par.cmd.ExecuteNonQuery()

        '            End If

        '        End If

        '    End While
        '    myReaderT.Close()




        '    'INIZIO Aa Bb **********************************************************************************************************
        '    '             **********************************************************************************************************
        '    If TrovatoAB = True Then
        '        ' PRIMA VOLTA CHE VIENE ESEGUITA LA MOROSITA'

        '        sPosteAler = ""


        '        Riassunto = "<table style='width:100%;'>"
        '        Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COD.CONTRATTO</td>" _
        '                                                                                                 & "<td>INDIRIZZO</td>" _
        '                                                                                                 & "<td>COGN./RAG.SOCIALE</td>" _
        '                                                                                                 & "<td>NOME</td>" _
        '                                                                                                 & "<td>PERIODO DI RIF.</td>" _
        '                                                                                                 & "<td>EMISSIONE</td>" _
        '                                                                                                 & "<td>SCADENZA</td>" _
        '                                                                                                 & "<td>N.BOLLETTINO</td>" _
        '                                                                                                 & "<td>IMPORTO</td>" _
        '                                                                                                 & "<td>SPESE</td></tr>"
        '        Riassunto = Riassunto & "<tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>"


        '        Dim idedificio As String = "0"
        '        Dim idcomplesso As String = "0"
        '        Dim idunita As String = "0"

        '        Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
        '        pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
        '        pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
        '        Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

        '        Dim Licenza As String = Session.Item("LicenzaPdfMerge")
        '        If Licenza <> "" Then
        '            pdfMerge.LicenseKey = Licenza
        '        End If

        '        Dim pdfMergeF As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)    'x FILE ZIP singolo

        '        Dim fileNamePosteAler As String = "PosteAler_" & vIdMorosita & "-" & vIdMorositaLetteraFILE & "-" & Format(Now, "yyyyMMddHHmmss") & "-Aa_Bb"

        '        Dim xx As String = "MorositaLettera_" & vIdMorositaLetteraFILE & "-" & Format(Now, "yyyyMMddHHmmss") & "-Aa_Bb"
        '        sNomeFile = xx
        '        xx = xx & ".pdf"



        '        Dim sr2 As StreamReader = New StreamReader(Server.MapPath("Elenco.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        '        contenutoRiassunto = sr2.ReadToEnd()
        '        sr2.Close()


        '        Dim K As Integer = 2
        '        'inizio a scrivere il file xls
        '        With myExcelFile

        '            '.CreateFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & sNomeFile & ".xls")
        '            .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

        '            .PrintGridLines = False
        '            .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '            .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '            .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '            .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '            .SetDefaultRowHeight(14)
        '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '            .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)

        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "TITOLO", 0)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NOMINATIVO", 0)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INDIRIZZO", 0)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CAP-CITTA", 0)

        '            .SetColumnWidth(1, 4, 30)

        '            Dim Contenuto As String = ""
        '            'Dim url As String
        '            Dim pdfConverter1 As PdfConverter

        '            Dim FlagStampa As Boolean = False


        '            par.cmd.Parameters.Clear()
        '            par.cmd.CommandText = "select MOROSITA.TIPO_INVIO,MOROSITA.DATA_PROTOCOLLO,MOROSITA.PROTOCOLLO_ALER," _
        '                                      & " MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE,MOROSITA_LETTERE.COD_CONTRATTO,MOROSITA_LETTERE.Importo, MOROSITA_LETTERE.ID_ANAGRAFICA, MOROSITA_LETTERE.EMISSIONE, MOROSITA_LETTERE.INIZIO_PERIODO, MOROSITA_LETTERE.FINE_PERIODO, MOROSITA_LETTERE.NUM_LETTERE," _
        '                                      & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA " _
        '                               & " from  SISCOM_MI.MOROSITA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.MOROSITA_LETTERE " _
        '                               & " where MOROSITA.ID=" & vIdMorosita _
        '                               & "   and MOROSITA_LETTERE.ID_ANAGRAFICA=ANAGRAFICA.ID " _
        '                               & "   and MOROSITA.ID                   =MOROSITA_LETTERE.ID_MOROSITA " _
        '                               & "   and MOROSITA_LETTERE.TIPO_LETTERA='AB' " _
        '                               & "   and MOROSITA_LETTERE.COD_STATO='M99' " _
        '                               & "   and MOROSITA_LETTERE.ID_CONTRATTO=" & vIdContratto _
        '                               & " order by MOROSITA_LETTERE.ID_ANAGRAFICA,MOROSITA_LETTERE.ID_CONTRATTO,MOROSITA_LETTERE.NUM_LETTERE "


        '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '            Do While myReader.Read
        '                'LOOP di tutte le lettere di MOROSITA

        '                If Len(par.IfNull(myReader("PARTITA_IVA"), 0)) = 11 Or Len(par.IfNull(myReader("COD_FISCALE"), 0)) = 16 Then
        '                    idMorositaLettere = par.IfNull(myReader("ID_MOROSITA_LETTERE"), 0)

        '                    Tot_Bolletta = 0

        '                    CodiceContratto = par.IfNull(myReader("COD_CONTRATTO"), "")
        '                    TipoIngiunzione = "RECUPERO MOROSITA'"
        '                    VOCE = "150" '"626"
        '                    Importo = par.IfNull(myReader("Importo"), "0,00")
        '                    IdAnagrafica = par.IfNull(myReader("id_anagrafica"), "")


        '                    par.cmd.Parameters.Clear()
        '                    par.cmd.CommandText = "select COMPLESSI_IMMOBILIARI.ID as idcomplesso,EDIFICI.id as idedificio," _
        '                                              & " RAPPORTI_UTENZA.*,UNITA_CONTRATTUALE.ID_UNITA,UNITA_CONTRATTUALE.SCALA,UNITA_CONTRATTUALE.INTERNO," _
        '                                              & " TAB_FILIALI.NOME as ""NOME_FILIALE"",TAB_FILIALI.ACRONIMO,TAB_FILIALI.N_TELEFONO," _
        '                                              & " (INDIRIZZI.DESCRIZIONE||' N°'||INDIRIZZI.CIVICO)  AS ""INDIRIZZO_FILIALE"" " _
        '                                       & " from SISCOM_MI.EDIFICI," _
        '                                            & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
        '                                            & " SISCOM_MI.UNITA_IMMOBILIARI," _
        '                                            & " SISCOM_MI.UNITA_CONTRATTUALE," _
        '                                            & " SISCOM_MI.RAPPORTI_UTENZA," _
        '                                            & " SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI " _
        '                                     & " where COMPLESSI_IMMOBILIARI.ID     =EDIFICI.ID_COMPLESSO " _
        '                                     & "   and EDIFICI.ID                   =UNITA_IMMOBILIARI.ID_EDIFICIO " _
        '                                     & "   and UNITA_IMMOBILIARI.ID         =UNITA_CONTRATTUALE.ID_UNITA " _
        '                                     & "   and RAPPORTI_UTENZA.ID           =UNITA_CONTRATTUALE.ID_CONTRATTO " _
        '                                     & "   and COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) " _
        '                                     & "   and TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID (+) " _
        '                                     & "   and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE is null " _
        '                                     & "   and COD_CONTRATTO='" & CodiceContratto & "'"


        '                    myReaderA = par.cmd.ExecuteReader()
        '                    If myReaderA.Read Then
        '                        'Da RAPPORTI_UTENZA
        '                        'Me.idcontratto.Value = par.IfNull(myReaderA("ID"), "-1")

        '                        presso_cor = par.IfNull(myReaderA("presso_cor"), "")
        '                        luogo_cor = par.IfNull(myReaderA("luogo_cor"), "")
        '                        civico_cor = par.IfNull(myReaderA("civico_cor"), "")
        '                        cap_cor = par.IfNull(myReaderA("cap_cor"), "")
        '                        indirizzo_cor = par.IfNull(myReaderA("VIA_cor"), "")
        '                        tipo_cor = par.IfNull(myReaderA("tipo_cor"), "")
        '                        sigla_cor = par.IfNull(myReaderA("sigla_cor"), "")

        '                        'Da UNITA_CONTRATTUALE
        '                        idunita = par.IfNull(myReaderA("ID_UNITA"), "-1")
        '                        sSCALA = par.IfNull(myReaderA("SCALA"), "")
        '                        sINTERNO = par.IfNull(myReaderA("INTERNO"), "")

        '                        idedificio = par.IfNull(myReaderA("idedificio"), "0")
        '                        idcomplesso = par.IfNull(myReaderA("idcomplesso"), "0")


        '                        If par.IfNull(myReaderA("COD_TIPOLOGIA_RAPP_CONTR"), "") <> "ILLEG" Then
        '                            TipoStampa = "Ingiunzione_Aa.htm"
        '                        Else
        '                            TipoStampa = "Ingiunzione_Bb.htm"
        '                        End If
        '                        NoteBollette = "MOROSITA' MG" 'MG (M.AV. Global Service) per quello relativo alla morosità fino al 30/9/2009

        '                        Dim sr1 As StreamReader = New StreamReader(Server.MapPath(TipoStampa), System.Text.Encoding.GetEncoding("iso-8859-1"))
        '                        Dim contenutoOriginale As String = sr1.ReadToEnd()
        '                        sr1.Close()
        '                        Contenuto = contenutoOriginale

        '                        'INFORMAZIONI SULLA FILIALE
        '                        sNomeFiliale = par.IfNull(myReaderA("NOME_FILIALE"), "")
        '                        sPosteAlerAcronimo = "CORE" 'par.IfNull(myReaderA("ACRONIMO"), "")
        '                        sNumTel_Filiale = par.IfNull(myReaderA("N_TELEFONO"), "")
        '                        sIndirizzo_Filiale = par.IfNull(myReaderA("INDIRIZZO_FILIALE"), "")
        '                        '****************************

        '                    End If
        '                    myReaderA.Close()


        '                    'TIPO INVIO
        '                    Select Case par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")
        '                        Case "Racc. A.R"
        '                            Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
        '                        Case "Racc. mano"
        '                            Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A MANO")
        '                        Case "FAX"
        '                            Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "FAX.")
        '                        Case Else
        '                            Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
        '                    End Select
        '                    '************************************

        '                    Dim Titolo As String = ""
        '                    Dim Nome As String = ""
        '                    Dim Cognome As String = ""
        '                    Dim CF As String = ""

        '                    Dim ID_BOLLETTA As Long = 0

        '                    'ANAGRAFICA
        '                    par.cmd.Parameters.Clear()
        '                    par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA where ID=" & IdAnagrafica
        '                    myReaderA = par.cmd.ExecuteReader()
        '                    If myReaderA.Read Then

        '                        sPosteAlerCodUtente = Format(CDbl(par.IfNull(myReaderA("ID"), 0)), "000000000000")
        '                        If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
        '                            Titolo = "Spettabile Ditta"
        '                            Cognome = par.IfNull(myReaderA("ragione_sociale"), "")
        '                            Nome = ""
        '                            CF = par.IfNull(myReaderA("partita_iva"), "")
        '                        Else
        '                            If par.IfNull(myReaderA("sesso"), "") = "M" Then
        '                                Titolo = "Egregio Signore"
        '                            Else
        '                                Titolo = "Gentile Signora"
        '                            End If
        '                            Cognome = par.IfNull(myReaderA("cognome"), "")
        '                            Nome = par.IfNull(myReaderA("nome"), "")
        '                            CF = par.IfNull(myReaderA("cod_fiscale"), "")
        '                        End If
        '                    End If
        '                    myReaderA.Close()
        '                    '*********************************************

        '                    Contenuto = Replace(Contenuto, "$data$", par.FormattaData(par.IfNull(myReader("DATA_PROTOCOLLO"), "")))
        '                    Contenuto = Replace(Contenuto, "$protocollo$", "GL0000/" & sPosteAlerAcronimo & "/" & par.IfNull(myReader("PROTOCOLLO_ALER"), ""))

        '                    Contenuto = Replace(Contenuto, "$codcontratto$", CodiceContratto)
        '                    Contenuto = Replace(Contenuto, "$codUI$", Strings.Left(CodiceContratto, Strings.Len(CodiceContratto) - 2))

        '                    If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
        '                        Contenuto = Replace(Contenuto, "$nominativo$", Cognome & " " & Nome & "<br />presso " & presso_cor)
        '                    Else
        '                        Contenuto = Replace(Contenuto, "$nominativo$", presso_cor)
        '                    End If
        '                    sPosteAlerNominativo = Cognome & " " & Nome 'POSTE 

        '                    If sINTERNO <> "" Then
        '                        If sSCALA <> "" Then
        '                            Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO & " SCALA " & sSCALA)
        '                        Else
        '                            Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO)
        '                        End If
        '                    ElseIf sSCALA <> "" Then
        '                        Contenuto = Replace(Contenuto, "$indirizzo2$", "SCALA " & sSCALA)
        '                    End If

        '                    sPosteAlerInterno = sINTERNO    'POSTE 

        '                    For i = 1 To Strings.Len(sSCALA)
        '                        If Char.IsDigit(Strings.Mid(sSCALA, i, 1)) = False Then
        '                            sPosteAlerScala = Strings.Mid(sSCALA, i, Strings.Len(sSCALA))  'POSTE 
        '                            Exit For
        '                        End If
        '                    Next i

        '                    Contenuto = Replace(Contenuto, "$indirizzo0$", indirizzo_cor & ", " & civico_cor)
        '                    Contenuto = Replace(Contenuto, "$indirizzo1$", cap_cor & " " & luogo_cor & " " & sigla_cor)
        '                    'Contenuto = Replace(Contenuto, "$indirizzo$", indirizzo_cor & ", " & civico_cor & "</br>" & cap_cor & " " & luogo_cor & " " & sigla_cor)

        '                    Contenuto = Replace(Contenuto, "$titolo$", Titolo)

        '                    sPosteAlerInd = indirizzo_cor & " " & civico_cor 'POSTE 
        '                    sPosteAlerCAP = cap_cor                          'POSTE 
        '                    sPosteAlerLocalita = luogo_cor                   'POSTE 
        '                    sPosteAlerProv = sigla_cor                       'POSTE 


        '                    'INFORMAZIONI FILIALE
        '                    Contenuto = Replace(Contenuto, "$NomeFiliale$", sNomeFiliale)
        '                    Contenuto = Replace(Contenuto, "$IndirizzoFiliale$", sIndirizzo_Filiale)
        '                    Contenuto = Replace(Contenuto, "$TelFiliale$", sNumTel_Filiale)



        '                    periodo = par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))

        '                    If PeriodoXLS_INIZIO > par.IfNull(myReader("inizio_periodo"), 0) Or PeriodoXLS_INIZIO = 0 Then
        '                        PeriodoXLS_INIZIO = par.IfNull(myReader("inizio_periodo"), 0)
        '                    End If

        '                    If PeriodoXLS_FINE < par.IfNull(myReader("fine_periodo"), 0) Or PeriodoXLS_FINE = 0 Then
        '                        PeriodoXLS_FINE = par.IfNull(myReader("fine_periodo"), 0)
        '                    End If

        '                    Contenuto = Replace(Contenuto, "$spazi$", "")
        '                    Contenuto = Replace(Contenuto, "$spazi1$", "")


        '                    'Contenuto = Replace(Contenuto, "$tipo$", TipoIngiunzione)
        '                    If TipoStampa = "Ingiunzione_Aa.htm" Or TipoStampa = "Ingiunzione_Bb.htm" Then

        '                        ValImporto1 = par.IfNull(myReader("Importo"), 0)

        '                        If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
        '                            par.cmd.Parameters.Clear()
        '                            par.cmd.CommandText = "select IMPORTO" _
        '                                               & " from SISCOM_MI.MOROSITA_LETTERE " _
        '                                               & " where ID_MOROSITA=" & vIdMorosita _
        '                                               & "   and ID_CONTRATTO=" & vIdContratto _
        '                                               & "   and ID_ANAGRAFICA=" & IdAnagrafica _
        '                                               & "   and BOLLETTINO is NULL " _
        '                                               & "   and NUM_LETTERE=2"

        '                            myReaderA = par.cmd.ExecuteReader()
        '                            If myReaderA.Read Then
        '                                ValImporto2 = par.IfNull(myReaderA("Importo"), 0)
        '                            End If
        '                            myReaderA.Close()

        '                            Contenuto = Replace(Contenuto, "$importo1$", Format(ValImporto1, "##,##0.00"))
        '                            Contenuto = Replace(Contenuto, "$importo2$", Format(ValImporto2, "##,##0.00"))

        '                            ValImporto1 = ValImporto1 + ValImporto2
        '                            Contenuto = Replace(Contenuto, "$importoTOT$", Format(ValImporto1, "##,##0.00"))

        '                        End If

        '                    Else
        '                        Contenuto = Replace(Contenuto, "$importoTOT$", Format(CDbl(Importo), "##,##0.00"))
        '                    End If


        '                    If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then

        '                        If IO.File.Exists(Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto & ".htm") = True Then
        '                            IO.File.Delete(Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto & ".htm")
        '                        End If

        '                        'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
        '                        Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

        '                        sr.WriteLine(Contenuto)
        '                        sr.Close()

        '                        'Dim url As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto
        '                        url = Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto
        '                        pdfConverter1 = New PdfConverter

        '                        Licenza = Session.Item("LicenzaHtmlToPdf")
        '                        If Licenza <> "" Then
        '                            pdfConverter1.LicenseKey = Licenza
        '                        End If

        '                        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        '                        pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
        '                        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        '                        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        '                        pdfConverter1.PdfDocumentOptions.ShowFooter = False
        '                        pdfConverter1.PdfDocumentOptions.LeftMargin = 40
        '                        pdfConverter1.PdfDocumentOptions.RightMargin = 40
        '                        pdfConverter1.PdfDocumentOptions.TopMargin = 30
        '                        pdfConverter1.PdfDocumentOptions.BottomMargin = 30
        '                        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

        '                        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        '                        pdfConverter1.PdfFooterOptions.FooterText = ("")
        '                        pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
        '                        pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        '                        pdfConverter1.PdfFooterOptions.PageNumberText = ""
        '                        pdfConverter1.PdfFooterOptions.ShowPageNumber = False
        '                        pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")

        '                        'If sPosteAler <> "" Then
        '                        '    sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
        '                        'Else
        '                        '    sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
        '                        'End If

        '                        ''****************EVENTI CONTRATTI UNA PER CONTRATTO***************
        '                        sStr1 = "Inviato il MAV MG, il MAV MA e la lettera di messa in mora con " & par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")

        '                        par.cmd.Parameters.Clear()

        '                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_CONTRATTI " _
        '                                                  & " (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '                                            & "values (:id_contratto,:id_operatore,:data,:cod_evento,:motivo)"

        '                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", vIdContratto))
        '                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

        '                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
        '                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "F176"))
        '                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

        '                        par.cmd.ExecuteNonQuery()
        '                        par.cmd.CommandText = ""
        '                        par.cmd.Parameters.Clear()
        '                        '************************************************


        '                        ' '' Ricavo ID di POSTALER per PostAler.txt
        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
        '                        myReaderA = par.cmd.ExecuteReader()
        '                        If myReaderA.Read Then
        '                            sPosteAlerIA = myReaderA(0)
        '                        End If
        '                        myReaderA.Close()


        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = "insert into SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
        '                                          & " values (" & sPosteAlerIA & "," & idMorositaLettere & ",1)"
        '                        par.cmd.ExecuteNonQuery()
        '                        '******************************************************************

        '                        If sPosteAler <> "" Then
        '                            sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
        '                        Else
        '                            sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
        '                        End If


        '                    Else
        '                        NoteBollette = "MOROSITA' MA" 'MA” (M.AV.) per quello relativo alla morosità dall’1/10/2009


        '                        ' CREO UN SOLO RECORD DI POSTALER (anche se ci sono 2 MAV), ed al secondo MAV aggiorno ID_LETTERA_2
        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = "update SISCOM_MI.POSTALER " _
        '                                           & " set   ID_LETTERA_2=" & idMorositaLettere _
        '                                           & " where ID=" & sPosteAlerIA
        '                        par.cmd.ExecuteNonQuery()
        '                    End If


        '                    Dim Nome1 As String = ""
        '                    Dim Nome2 As String = ""

        '                    If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
        '                        Nome1 = Cognome & " " & Nome
        '                        Nome2 = presso_cor
        '                    Else
        '                        Nome1 = presso_cor
        '                    End If


        '                    ' '' Ricavo ID_BOLLETTA
        '                    par.cmd.Parameters.Clear()
        '                    par.cmd.CommandText = " select SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL FROM dual "
        '                    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '                    If myReaderB.Read Then
        '                        ID_BOLLETTA = myReaderB(0)
        '                    End If
        '                    myReaderB.Close()


        '                    par.cmd.Parameters.Clear()
        '                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
        '                                                & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, " _
        '                                                & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
        '                                                & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
        '                                                & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
        '                                                & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
        '                                                & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA,ID_TIPO) " _
        '                                        & "values (:id,:n_rata,:data_emissione,:data_scadenza," _
        '                                                & ":note,:id_contratto,:id_esercizio_f," _
        '                                                & ":id_unita,:fl_annullata,:pagabile_presso,:cod_affittuario,:intestatario," _
        '                                                & ":indirizzo,:cap_citta,:presso,:riferimento_da,:riferimento_a," _
        '                                                & ":fl_stampato,:id_complesso,:data_ins_pagamento,:importo_pagato,:note_pagamento," _
        '                                                & ":anno,:operatore_pag,:id_edificio,:data_annullo_pag,:operatore_annullo_pag,:rif_file,:id_morosita,4)"

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", ID_BOLLETTA))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_rata", 999))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_emissione", Format(Now, "yyyyMMdd")))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", ScadenzaBollettino))

        '                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_1_sollecito", "NULL"))
        '                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_2_sollecito", "NULL"))
        '                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_pagamento", "NULL"))

        '                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", NoteBollette))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", vIdContratto))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_esercizio_f", RitornaNullSeIntegerMenoUno(par.RicavaEsercizioCorrente)))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", idunita))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_annullata", "0"))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pagabile_presso", ""))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_affittuario", RitornaNullSeIntegerMenoUno(IdAnagrafica)))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("intestatario", Strings.Left(Nome1, 100)))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", Strings.Left(tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor), 100)))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap_citta", Strings.Left(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")", 100)))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("presso", Strings.Left(Nome2, 100)))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_da", par.IfNull(myReader("inizio_periodo"), "")))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_a", par.IfNull(myReader("fine_periodo"), "")))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_stampato", "0"))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(idcomplesso)))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ins_pagamento", ""))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_pagato", DBNull.Value))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note_pagamento", ""))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Year(Now)))

        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_pag", ""))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(idedificio)))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_annullo_pag", DBNull.Value))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_annullo_pag", DBNull.Value))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_file", "MOR"))
        '                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorosita))


        '                    par.cmd.ExecuteNonQuery()
        '                    par.cmd.CommandText = ""
        '                    par.cmd.Parameters.Clear()



        '                    ' INSERT tutte le sotto voci di BOL_BOLLETTE_VOCI
        '                    'RECUPERO MOROSITA' (150)
        '                    par.cmd.Parameters.Clear()
        '                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
        '                                        & " values " _
        '                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
        '                                                & ID_BOLLETTA & "," _
        '                                                & VOCE & "," _
        '                                                & par.VirgoleInPunti(Importo) & ")"
        '                    par.cmd.ExecuteNonQuery()
        '                    Tot_Bolletta = Tot_Bolletta + Importo

        '                    If NoteBollette = "MOROSITA' MA" Then
        '                        'SPESE DI NOTIFICA (628)
        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
        '                                           & " values " _
        '                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
        '                                                & ID_BOLLETTA _
        '                                                & ",628," _
        '                                                & par.VirgoleInPunti(spese_notifica) & ")"
        '                        par.cmd.ExecuteNonQuery()
        '                        Tot_Bolletta = Tot_Bolletta + spese_notifica
        '                    End If

        '                    'SPESE MAV
        '                    par.cmd.Parameters.Clear()
        '                    par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
        '                                       & " values " _
        '                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
        '                                            & ID_BOLLETTA _
        '                                            & ",407," _
        '                                            & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
        '                    par.cmd.ExecuteNonQuery()
        '                    Tot_Bolletta = Tot_Bolletta + SPESEmav

        '                    'BOLLO
        '                    If Tot_Bolletta >= APPLICABOLLO Then
        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
        '                                           & " values " _
        '                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
        '                                                    & ID_BOLLETTA _
        '                                                    & ",95," _
        '                                                    & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
        '                        par.cmd.ExecuteNonQuery()
        '                        Tot_Bolletta = Tot_Bolletta + BOLLO
        '                    End If
        '                    '******************************************************

        '                    If Session.Item("AmbienteDiTest") = "1" Then
        '                        causalepagamento = "COMMITEST01"
        '                        pp.Url = "https://web1.unimaticaspa.it/pagamenti20-test-ws/services/MAVOnline"
        '                        pp.Url = "https://demoweb.infogroup.it/pagamenti20-test-ws/services/MAVOnline"

        '                    End If


        '                    RichiestaEmissioneMAV.codiceEnte = "commi"
        '                    RichiestaEmissioneMAV.tipoPagamento = causalepagamento
        '                    RichiestaEmissioneMAV.idOperazione = Format(ID_BOLLETTA, "0000000000")
        '                    RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(IdAnagrafica), "0000000000")

        '                    RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA)

        '                    RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
        '                    RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
        '                    RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
        '                    RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

        '                    RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
        '                    If Nome <> "" Then
        '                        RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
        '                    End If


        '                    If Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor) <= 23 Then
        '                        RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
        '                    Else
        '                        RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
        '                        RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
        '                    End If

        '                    RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
        '                    RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
        '                    RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
        '                    RichiestaEmissioneMAV.nazioneDebitore = "IT"

        '                    Try

        '                        Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

        '                    Catch ex As Exception

        '                        'par.cmd.Parameters.Clear()
        '                        ''par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
        '                        'par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1  where ID=" & ID_BOLLETTA
        '                        'par.cmd.ExecuteNonQuery()

        '                        par.myTrans.Rollback()
        '                        'par.cmd.Dispose()
        '                        'par.OracleConn.Close()
        '                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '                        CreaLetteraMAV = -1

        '                        Session.Item("LAVORAZIONE") = "0"
        '                        Response.Write("<p style='color: #FF0000; font-weight: bold'>" & ex.Message & " !</p>")
        '                        Exit Function
        '                    End Try

        '                    If Esito.codiceRisultato = "0" Then
        '                        'If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
        '                        '    pdfMerge.AppendPDFFile(url & ".pdf")
        '                        '    IO.File.Delete(url & ".htm")
        '                        'End If

        '                        'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
        '                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".pdf"

        '                        binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
        '                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
        '                        outFile.Write(binaryData, 0, binaryData.Length - 1)
        '                        outFile.Close()

        '                        pdfMerge.AppendPDFFile(outputFileName)
        '                        pdfMergeF.AppendPDFFile(outputFileName)

        '                        ' se la banca emette correttamente i MAV allora:
        '                        ' SETTO a BOL_BOLLETTE che è stato stampato e il numero di bollettino
        '                        num_bollettino = Esito.numeroMAV
        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
        '                                           & " set   FL_STAMPATO='1'," _
        '                                           & "       rif_bollettino='" & num_bollettino & "'" _
        '                                           & " where ID=" & ID_BOLLETTA
        '                        par.cmd.ExecuteNonQuery()

        '                        Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 9pt;'><td style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & CodiceContratto & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & indirizzo_cor & ", " & civico_cor & " " & cap_cor & " " & luogo_cor & " " & sigla_cor & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Cognome & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Nome & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("emissione"), "")) & " </td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(ScadenzaBollettino) & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & num_bollettino & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Importo), "##,##0.00") & "</td>" _
        '                                                                                              & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Tot_Bolletta - Importo), "##,##0.00") & "</td></tr>"

        '                        If par.IfNull(myReader("NUM_LETTERE"), 1) = 2 Or TipoStampa = "Ingiunzione_C.htm" Or TipoStampa = "Ingiunzione_D.htm" Then

        '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(Titolo))
        '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(Cognome & " " & Nome))
        '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(indirizzo_cor & ", " & civico_cor))
        '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(cap_cor & " " & luogo_cor & " " & sigla_cor))

        '                            K = K + 1
        '                        End If

        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
        '                                          & "     set BOLLETTINO='" & num_bollettino & "'," _
        '                                          & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
        '                                          & "         ID_OPERATRE=" & Session.Item("ID_OPERATORE") & "," _
        '                                          & "         DATA_DILAZIONE=''," _
        '                                          & "         COD_STATO='M99'" _
        '                                          & " where ID=" & idMorositaLettere

        '                        par.cmd.ExecuteNonQuery()


        '                        'SOLO x TEST , per la produxione toglie il commento sopra e rimettrerlo sotto e togliere MOROSITA_EVENTI
        '                        'INIZIO TEST
        '                        'par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
        '                        '                  & "     set BOLLETTINO='" & num_bollettino & "'," _
        '                        '                  & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
        '                        '                  & "         COD_STATO='M04'" _
        '                        '                  & " where ID=" & idMorositaLettere

        '                        'par.cmd.ExecuteNonQuery()

        '                        'par.cmd.Parameters.Clear()
        '                        'par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
        '                        '                       & "  (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '                        '                       & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL," & idMorositaLettere & "," _
        '                        '                                & Session.Item("ID_OPERATORE") & "," & Format(Now, "yyyyMMddHHmmss") & "," _
        '                        '                                & "'M04','Ricevuta PostAler: RITIRATA DAL DESTINATARIO')"

        '                        'par.cmd.ExecuteNonQuery()
        '                        par.cmd.CommandText = ""
        '                        '************ FINE TEST



        '                        'UPDATE BOL_BOLLETTE VECCHIE raggruppate per la BOLLETTA MOROSITA' NUOVA
        '                        par.cmd.Parameters.Clear()
        '                        par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
        '                                            & " set   ID_BOLLETTA_RIC=" & ID_BOLLETTA & "," _
        '                                            & "       ID_MOROSITA=" & vIdMorosita & "," _
        '                                            & "       IMPORTO_RICLASSIFICATO=NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) " _
        '                                            & " where ID_CONTRATTO=" & vIdContratto _
        '                                            & "   and FL_ANNULLATA=0 " _
        '                                            & "   and ID_BOLLETTA_RIC Is NULL and ID_RATEIZZAZIONE is null " _
        '                                            & "   and ID<0 " _
        '                                            & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null)   " _
        '                                            & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
        '                                            & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

        '                        par.cmd.ExecuteNonQuery()
        '                        par.cmd.Parameters.Clear()
        '                        '************************************



        '                        ' Response.Redirect("ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf")

        '                    Else
        '                        'lblErrore.Visible = True

        '                        ' se la banca NON emette correttamente i MAV allora:
        '                        ' ELIMINO  BOL_BOLLETTE 

        '                        par.myTrans.Rollback()
        '                        'par.cmd.Dispose()
        '                        'par.OracleConn.Close()
        '                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '                        CreaLetteraMAV = -1

        '                        'par.cmd.Parameters.Clear()
        '                        ''par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
        '                        'par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1  where ID=" & ID_BOLLETTA
        '                        'par.cmd.ExecuteNonQuery()

        '                        'Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")
        '                        Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")

        '                        'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml"
        '                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".xml"

        '                        binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
        '                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
        '                        outFile.Write(binaryData, 0, binaryData.Length)
        '                        outFile.Close()
        '                        Exit Function
        '                    End If


        '                Else
        '                    If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
        '                        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("ragione_sociale"), "") & " non sono stati stampati perchè la partita iva non ha un formato corretto!</p>")
        '                    Else
        '                        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & " non sono stati stampati perchè il codice fiscale non ha un formato corretto!</p>")
        '                    End If
        '                End If

        '                'AGGIUNGO LA LETTERA
        '                If par.IfNull(myReader("NUM_LETTERE"), 1) = 2 Or TipoStampa = "Ingiunzione_C.htm" Or TipoStampa = "Ingiunzione_D.htm" Then
        '                    pdfMerge.AppendPDFFile(url & ".pdf")

        '                    IO.File.Delete(url & ".htm")

        '                End If


        '            Loop
        '            myReader.Close()
        '            .CloseFile()
        '        End With

        '        Riassunto = Riassunto & "</table>"
        '        contenutoRiassunto = Replace(contenutoRiassunto, "$riassunto$", Riassunto)
        '        contenutoRiassunto = Replace(contenutoRiassunto, "$periodo$", par.FormattaData(PeriodoXLS_INIZIO) & " - " & par.FormattaData(PeriodoXLS_FINE))

        '        PeriodoXLS_INIZIO = 0
        '        PeriodoXLS_FINE = 0

        '        'Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
        '        Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & vIdMorosita & "-" & vIdMorositaLetteraFILE & "-Aa_Bb" & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

        '        sr3.WriteLine(contenutoRiassunto)
        '        sr3.Close()


        '        'Dim url1 As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita
        '        Dim url1 As String = Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & vIdMorosita & "-" & vIdMorositaLetteraFILE & "-Aa_Bb"

        '        Dim pdfConverter As PdfConverter = New PdfConverter

        '        Licenza = Session.Item("LicenzaHtmlToPdf")
        '        If Licenza <> "" Then
        '            pdfConverter.LicenseKey = Licenza
        '        End If


        '        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        '        pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
        '        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        '        pdfConverter.PdfDocumentOptions.ShowHeader = False
        '        pdfConverter.PdfDocumentOptions.ShowFooter = False
        '        pdfConverter.PdfDocumentOptions.LeftMargin = 30
        '        pdfConverter.PdfDocumentOptions.RightMargin = 30
        '        pdfConverter.PdfDocumentOptions.TopMargin = 30
        '        pdfConverter.PdfDocumentOptions.BottomMargin = 30
        '        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

        '        pdfConverter.PdfDocumentOptions.ShowHeader = False
        '        pdfConverter.PdfFooterOptions.FooterText = ("")
        '        pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
        '        pdfConverter.PdfFooterOptions.DrawFooterLine = False
        '        pdfConverter.PdfFooterOptions.PageNumberText = ""
        '        pdfConverter.PdfFooterOptions.ShowPageNumber = False
        '        pdfConverter.SavePdfFromUrlToFile(url1 & ".htm", url1 & ".pdf")

        '        ''AGGIUNGO LA LETTERA
        '        'pdfMerge.AppendPDFFile(url & ".pdf")
        '        'IO.File.Delete(url & ".htm")

        '        'AGGIUNGO L'elenco
        '        pdfMerge.AppendPDFFile(url1 & ".pdf")
        '        IO.File.Delete(url1 & ".htm")


        '        'COMMIT
        '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '        If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
        '            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '            par.myTrans.Commit()
        '            HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
        '        End If
        '        par.cmd.Dispose()

        '        CreaLetteraMAV = idMorositaLettere

        '        'pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx)
        '        pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\FileTemp\") & xx)

        '        Dim objCrc32 As New Crc32()
        '        Dim strmZipOutputStream As ZipOutputStream
        '        Dim zipfic As String
        '        Dim strFile As String
        '        Dim strmFile As FileStream

        '        'zipfic = Server.MapPath("Varie\" & sNomeFile & ".zip")
        '        zipfic = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".zip")

        '        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '        strmZipOutputStream.SetLevel(6)

        '        'scrivo file XLS
        '        'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".xls")
        '        strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
        '        strmFile = File.OpenRead(strFile)
        '        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

        '        Dim sFile As String = Path.GetFileName(strFile)
        '        Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '        Dim fi As New FileInfo(strFile)

        '        theEntry.DateTime = fi.LastWriteTime
        '        theEntry.Size = strmFile.Length
        '        strmFile.Close()
        '        objCrc32.Reset()
        '        objCrc32.Update(abyBuffer)
        '        theEntry.Crc = objCrc32.Value
        '        strmZipOutputStream.PutNextEntry(theEntry)
        '        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '        File.Delete(strFile)

        '        'scrivo file PDF
        '        'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx
        '        strFile = Server.MapPath("..\FileTemp\") & xx
        '        strmFile = File.OpenRead(strFile)
        '        Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '        strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)

        '        Dim sFile1 As String = Path.GetFileName(strFile)
        '        theEntry = New ZipEntry(sFile1)
        '        fi = New FileInfo(strFile)

        '        theEntry.DateTime = fi.LastWriteTime
        '        theEntry.Size = strmFile.Length
        '        strmFile.Close()
        '        objCrc32.Reset()
        '        objCrc32.Update(abyBuffer1)
        '        theEntry.Crc = objCrc32.Value
        '        strmZipOutputStream.PutNextEntry(theEntry)
        '        strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
        '        File.Delete(strFile)

        '        'Scrivo FILE TXT POSTE *******************************
        '        Using sw As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt"))
        '            sw.Write(sPosteAler)
        '            sw.Close()
        '        End Using

        '        strFile = Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt")
        '        strmFile = File.OpenRead(strFile)
        '        Dim abyBuffer2(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '        strmFile.Read(abyBuffer2, 0, abyBuffer2.Length)

        '        Dim sFile2 As String = Path.GetFileName(strFile)
        '        theEntry = New ZipEntry(sFile2)
        '        fi = New FileInfo(strFile)

        '        theEntry.DateTime = fi.LastWriteTime
        '        theEntry.Size = strmFile.Length
        '        strmFile.Close()
        '        objCrc32.Reset()
        '        objCrc32.Update(abyBuffer2)
        '        theEntry.Crc = objCrc32.Value
        '        strmZipOutputStream.PutNextEntry(theEntry)
        '        strmZipOutputStream.Write(abyBuffer2, 0, abyBuffer2.Length)
        '        File.Delete(strFile)
        '        '******************************************

        '        strmZipOutputStream.Finish()
        '        strmZipOutputStream.Close()

        '    End If
        '    'FINE Aa Bb **********************************************************************************************************
        '    '           **********************************************************************************************************




        'Catch ex As Exception

        '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
        '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '        par.myTrans.Rollback()
        '        HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
        '    End If

        '    CreaLetteraMAV = -1

        '    Session.Item("LAVORAZIONE") = "0"
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        'End Try


    End Function



    Function CREA_MAV() As Boolean
        Dim sStr1 As String = ""
        Dim ValSomma As Decimal = 0
        Dim sDataDA As String = ""
        Dim Crearelettera1_AB As Boolean = False
        Dim Crearelettera2_AB As Boolean = False
        Dim Crearelettera1_CD As Boolean = False
        Dim Crearelettera1_EF As Boolean = False
        Dim Id_Lettera1 As Long = -1
        Dim Id_Lettera2 As Long = -1

        Try
            'NOTA: in caso di MAV ERRATO
            CREA_MAV = False

            'MOROSITA
            vIdMorosita = 0
            If Request.QueryString("ID_MOROSITA") <> "" Then
                vIdMorosita = Request.QueryString("ID_MOROSITA")
            End If

            'CONTRATTO
            vIdContratto = 0
            If Request.QueryString("ID_CONTRATTO") <> "" Then
                vIdContratto = Request.QueryString("ID_CONTRATTO")
            End If

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
            End If



            par.cmd.Parameters.Clear()
            sStr1 = "select * from SISCOM_MI.MOROSITA_LETTERE " _
                  & " where ID_MOROSITA= " & vIdMorosita _
                  & "   and ID_CONTRATTO=" & vIdContratto

            par.cmd.CommandText = sStr1
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReaderA.Read
                If par.IfNull(myReaderA("BOLLETTINO"), "") = "" Then
                    Select Case par.IfNull(myReaderA("TIPO_LETTERA"), "")
                        Case "AB"
                            If par.IfNull(myReaderA("NUM_LETTERE"), 1) = 1 Then
                                Crearelettera1_AB = True
                                Id_Lettera1 = par.IfNull(myReaderA("ID"), -1)
                            ElseIf par.IfNull(myReaderA("NUM_LETTERE"), 1) = 1 Then
                                Crearelettera2_AB = True
                                Id_Lettera2 = par.IfNull(myReaderA("ID"), -1)
                            End If

                        Case "CD"
                            Crearelettera1_CD = True
                            Id_Lettera1 = par.IfNull(myReaderA("ID"), -1)
                        Case "EF"
                            Crearelettera1_EF = True
                            Id_Lettera1 = par.IfNull(myReaderA("ID"), -1)

                    End Select
                End If
            End While
            myReaderA.Close()



            sStr1 = "select ANAGRAFICA.ID as ID_ANAGRAFICA, RAPPORTI_UTENZA.ID as ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR,SALDI.SALDO_1,SALDI.SALDO_2 " _
                 & " from   SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.SALDI " _
                 & " where  RAPPORTI_UTENZA.ID=" & vIdContratto _
                 & "   and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
                 & "   and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID  " _
                 & "   and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                 & "   and  RAPPORTI_UTENZA.ID=SALDI.ID_CONTRATTO " _
                 & " order by ID_CONTRATTO	"

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            myReaderA = par.cmd.ExecuteReader()

            While myReaderA.Read
                'Per ogni riga di RAPPORTO_UTENZA ID_CONTRATTO:
                '1) select BOL_BOLLETTE x ID_CONTRATTO + altre OPZIONI
                '2) somma1 di tutte le BOL_BOLLETTE_VOCI.IMPORTO  x ogni BOLLETTA
                '3) sommaTOT=somma1- somma2 di BOL_BOLLETTE.IMPORTO_PAGATO
                '4) insert MOROSITA_DETT
                '5) insert MOROSITA_LETTERE

                '(1)
                ValSomma = 0
                '1) SOMMA IMPORTO DA DARE (dare - avere)

                If (par.IfNull(myReaderA("SALDO_1"), 0) > 0 And par.IfNull(myReaderA("SALDO_2"), 0) > 0) Then
                    '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                    sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
                         & " from SISCOM_MI.BOL_BOLLETTE " _
                         & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                         & "   and FL_ANNULLATA=0 " _
                         & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                         & "   and ID_BOLLETTA_RIC Is NULL " _
                         & "   and ID_RATEIZZAZIONE is null " _
                         & "   and ID<0 " _
                         & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                         & "   and BOL_BOLLETTE.ID_TIPO is not null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                         & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                         & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                    '& "   and ((NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0))) >0"


                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = sStr1
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        ValSomma = par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()
                    '***********************************

                    If ValSomma > 0 Then
                        '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
                                            & " from SISCOM_MI.BOL_BOLLETTE" _
                                            & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                                            & "   and FL_ANNULLATA=0 " _
                                            & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                                            & "   and ID_BOLLETTA_RIC Is NULL " _
                                            & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                            & "   and ID<0 " _
                                            & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                            & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                            & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                            & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "


                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sDataDA = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()
                        '******************************************

                        If Crearelettera1_AB = True Then
                            'UPDATE SISCOM_MI.MOROSITA_LETTERE 
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " update SISCOM_MI.MOROSITA_LETTERE " _
                                               & " set ID_OPERATORE=:id_operatore,EMISSIONE=:emissione,IMPORTO=:importo,INIZIO_PERIODO=:inizio, " _
                                               & "     FINE_PERIODO=:fine,NUM_LETTERE=:num_lettere,TIPO_LETTERA=:tipo_lettera,IMPORTO_INIZIALE=:importo_iniziale,COD_STATO=:cod_stato " _
                                               & " where ID=" & Id_Lettera1 _
                                               & "   and ID_MOROSITA=" & vIdMorosita _
                                               & "   and ID_CONTRATTO=" & vIdContratto

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", sDataDA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", "20090930"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 1))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "AB"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()


                            'UPDATE BOL_BOLLETTE PRIMA DEL 2009 (DARE e AVERE) in data 09/02/2012 invece di ID_BOLLETTA_RIC uso ID_MOROSITA_LETTERA
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                & " set   ID_MOROSITA_LETTERA=" & Id_Lettera1 & "," _
                                                & "       ID_MOROSITA=" & vIdMorosita _
                                                & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                                                & "   and FL_ANNULLATA=0 " _
                                                & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                                                & "   and ID_BOLLETTA_RIC Is NULL " _
                                                & "   and ID_RATEIZZAZIONE is null " _
                                                & "   and ID<0 " _
                                                & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                                & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "


                            ''****************MYEVENT**PRIMA del 2009 LETTERA 1 di 2***************

                            sStr1 = "Inviato il MAV e la lettera di messa in mora con  per il recupero crediti MOROSITA dal: " & Format(sDataDA, "yyyyMMdd") & " al: 30/09/2009"

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                          & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************
                        End If



                        sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0) ) ) " _
                              & " from SISCOM_MI.BOL_BOLLETTE " _
                              & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                              & "   and ID>0 " _
                              & "   and FL_ANNULLATA=0 " _
                              & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                              & "   and ID_BOLLETTA_RIC is null  " _
                              & "   and ID_RATEIZZAZIONE is null " _
                              & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                              & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = sStr1
                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            ValSomma = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()

                        If ValSomma > 0 Then
                            'UPDATE MOROSITA LETTERE
                            If Crearelettera2_AB = True Then
                                'UPDATE SISCOM_MI.MOROSITA_LETTERE 
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = " update SISCOM_MI.MOROSITA_LETTERE " _
                                                   & " set ID_OPERATORE=:id_operatore,EMISSIONE=:emissione,IMPORTO=:importo,INIZIO_PERIODO=:inizio, " _
                                                   & "     FINE_PERIODO=:fine,NUM_LETTERE=:num_lettere,TIPO_LETTERA=:tipo_lettera,IMPORTO_INIZIALE=:importo_iniziale,COD_STATO=:cod_stato " _
                                                   & " where ID=" & Id_Lettera2 _
                                                   & "   and ID_MOROSITA=" & vIdMorosita _
                                                   & "   and ID_CONTRATTO=" & vIdContratto

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", "20091001"))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", Format(Now, "yyyyMMdd")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 2))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "AB"))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                par.cmd.Parameters.Clear()
                                '************************************************


                                'UPDATE BOL_BOLLETTE
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                                      & " set   ID_MOROSITA_LETTERA=" & Id_Lettera2 & "," _
                                                      & "       ID_MOROSITA=" & vIdMorosita _
                                                      & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                                                      & "   and ID>0 " _
                                                      & "   and FL_ANNULLATA=0 " _
                                                      & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                                                      & "   and ID_BOLLETTA_RIC is null  " _
                                                      & "   and ID_RATEIZZAZIONE is null " _
                                                      & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                                      & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "
                                par.cmd.ExecuteNonQuery()


                                ''****************MYEVENT**DOPO del 2009 LETTERA 2 di 2***************

                                sStr1 = "Inviato il MAV e la lettera di messa in mora con  per il recupero crediti MOROSITA dal: 30/09/2009 al: " & par.FormattaData(Format(Now, "yyyyMMdd"))

                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                par.cmd.Parameters.Clear()
                                '************************************************
                            End If
                        End If

                    End If

                ElseIf (par.IfNull(myReaderA("SALDO_1"), 0) > 0 And par.IfNull(myReaderA("SALDO_2"), 0) = 0) Then
                    'SOLO PRIMA DEL 2009
                    '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                    sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
                         & " from SISCOM_MI.BOL_BOLLETTE " _
                         & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                         & "   and FL_ANNULLATA=0 " _
                         & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                         & "   and ID_BOLLETTA_RIC Is NULL " _
                         & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                         & "   and ID<0 " _
                         & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                         & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                         & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                         & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = sStr1
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        ValSomma = par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()
                    '***********************************

                    If ValSomma > 0 Then
                        '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
                                            & " from SISCOM_MI.BOL_BOLLETTE" _
                                            & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                                            & "   and FL_ANNULLATA=0 " _
                                            & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                                            & "   and ID_BOLLETTA_RIC Is NULL " _
                                            & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                            & "   and ID<0 " _
                                            & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                            & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                            & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                            & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "


                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sDataDA = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()
                        '******************************************



                        'UPDATE
                        If Crearelettera1_EF = True Then
                            'UPDATE SISCOM_MI.MOROSITA_LETTERE 
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " update SISCOM_MI.MOROSITA_LETTERE " _
                                               & " set ID_OPERATORE=:id_operatore,EMISSIONE=:emissione,IMPORTO=:importo,INIZIO_PERIODO=:inizio, " _
                                               & "     FINE_PERIODO=:fine,NUM_LETTERE=:num_lettere,TIPO_LETTERA=:tipo_lettera,IMPORTO_INIZIALE=:importo_iniziale,COD_STATO=:cod_stato " _
                                               & " where ID=" & Id_Lettera1 _
                                               & "   and ID_MOROSITA=" & vIdMorosita _
                                               & "   and ID_CONTRATTO=" & vIdContratto

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", sDataDA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", "20090930"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 1))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "EF"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************

                            'UPDATE BOL_BOLLETTE PRIMA DEL 2009 (DARE e AVERE)
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                & " set   ID_MOROSITA_LETTERA=" & Id_Lettera1 & "," _
                                                & "       ID_MOROSITA=" & vIdMorosita _
                                                & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                                                & "   and FL_ANNULLATA=0 " _
                                                & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                                                & "   and ID_BOLLETTA_RIC Is NULL " _
                                                & "   and ID_RATEIZZAZIONE is null " _
                                                & "   and ID<0 " _
                                                & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                                & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "


                            par.cmd.ExecuteNonQuery()
                            par.cmd.Parameters.Clear()


                            ''****************MYEVENT**PRIMA del 2009 LETTERA 1 ***************

                            sStr1 = "Inviato il MAV e la lettera di messa in mora con  per il recupero crediti MOROSITA dal: " & Format(sDataDA, "yyyyMMdd") & " al: 30/09/2009"

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                          & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************
                        End If
                    End If

                Else
                    ' SOLO DOPO il 2009
                    '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                    sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0) ) ) " _
                            & " from SISCOM_MI.BOL_BOLLETTE " _
                            & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                            & "   and ID>0 " _
                            & "   and FL_ANNULLATA=0 " _
                            & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                            & "   and ID_BOLLETTA_RIC is null  " _
                            & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                            & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                            & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = sStr1
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        ValSomma = par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()
                    '***********************************

                    If ValSomma > 0 Then
                        '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "   select MIN(RIFERIMENTO_DA)  " _
                                                & " from SISCOM_MI.BOL_BOLLETTE " _
                                                & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                                                & "   and ID>0 " _
                                                & "   and FL_ANNULLATA=0 " _
                                                & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                                                & "   and ID_BOLLETTA_RIC is null  " _
                                                & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                                & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sDataDA = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()
                        '******************************************

                        If Crearelettera1_CD = True Then
                            'UPDATE SISCOM_MI.MOROSITA_LETTERE 

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " update SISCOM_MI.MOROSITA_LETTERE " _
                                               & " set ID_OPERATORE=:id_operatore,EMISSIONE=:emissione,IMPORTO=:importo,INIZIO_PERIODO=:inizio, " _
                                               & "     FINE_PERIODO=:fine,NUM_LETTERE=:num_lettere,TIPO_LETTERA=:tipo_lettera,IMPORTO_INIZIALE=:importo_iniziale,COD_STATO=:cod_stato " _
                                               & " where ID=" & Id_Lettera1 _
                                               & "   and ID_MOROSITA=" & vIdMorosita _
                                               & "   and ID_CONTRATTO=" & vIdContratto

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", sDataDA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 1))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "CD"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************


                            'UPDATE BOL_BOLLETTE
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                                  & " set   ID_MOROSITA_LETTERA=" & Id_Lettera1 & "," _
                                                  & "       ID_MOROSITA=" & vIdMorosita _
                                                  & " where ID_CONTRATTO=" & par.IfNull(myReaderA("ID_CONTRATTO"), "-1") _
                                                  & "   and ID>0 " _
                                                  & "   and FL_ANNULLATA=0 " _
                                                  & "   and (ID_MOROSITA=" & vIdMorosita & " or ID_MOROSITA is null) " _
                                                  & "   and ID_BOLLETTA_RIC is null  " _
                                                  & "   and ID_RATEIZZAZIONE is null " _
                                                  & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                                  & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

                            par.cmd.ExecuteNonQuery()



                            ''****************MYEVENT****DOPO il 2009 Lettera 1*************

                            sStr1 = "Inviato il MAV e la lettera di messa in mora con  per il recupero crediti MOROSITA dal: " & Format(sDataDA, "yyyyMMdd") & " al: " & Format(Now, "yyyyMMdd")

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                          & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************
                        End If
                    End If
                End If

            End While
            myReaderA.Close()

            'COMMIT
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Commit()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            par.cmd.Dispose()
            par.cmd.CommandText = ""


            Response.Write("<script>window.showModalDialog('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "',window, 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');</script>")


            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            CREA_MAV = True


        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            CREA_MAV = False

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Function



    Function AnnullaMAV() As Boolean
        Dim sStr1 As String = ""

        AnnullaMAV = False

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            Else
                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, par.myTrans)
            End If


            
            'RIPRISTINO IMPORTO_RICLASSIFICATO di TUTTE le BOLLETTE VOCI interesate di MAV vecchi
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                               & " set  IMPORTO_RICLASSIFICATO = Null " _
                               & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                    & "  where ID_BOLLETTA_RIC is not null " _
                                                    & "    and ID_MOROSITA=" & vIdMorosita _
                                                    & "    and FL_ANNULLATA=0 " _
                                                    & "    and ID_CONTRATTO=" & vIdContratto _
                                                    & "    and ID_RATEIZZAZIONE IS NULL ) "

            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()


            'RIPRISTINO TUTTE le BOLLETTE interesate dai MAV vecchi
            par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                               & " set ID_BOLLETTA_RIC=Null," _
                               & "     ID_MOROSITA=Null " _
                               & " where ID_MOROSITA=" & vIdMorosita _
                               & "   and ID_CONTRATTO=" & vIdContratto _
                               & "   and FL_ANNULLATA=0 " _
                               & "   and ID_BOLLETTA_RIC IS NOT NULL " _
                               & "   and ID_RATEIZZAZIONE IS NULL "

            par.cmd.ExecuteNonQuery()

            'ANNULLO il MAV Vecchio
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
                               & " set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'" _
                               & " where ID_MOROSITA = " & vIdMorosita _
                               & "   and ID_CONTRATTO=" & vIdContratto _
                               & "   and ID_TIPO=4"

            par.cmd.ExecuteNonQuery()

            '3) MODIFICO LO STATO DELLE MOROSITA LETTERE
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "update  SISCOM_MI.MOROSITA_LETTERE " _
                               & " set COD_STATO='M98' " _
                               & " where ID_MOROSITA=" & vIdMorosita _
                               & "   and ID_CONTRATTO=" & vIdContratto

            par.cmd.ExecuteNonQuery()


            'EVENTI
            par.cmd.CommandText = " select ID from SISCOM_MI.MOROSITA_LETTERE  " _
                    & " where ID_MOROSITA=" & vIdMorosita _
                    & "   and ID_CONTRATTO=" & vIdContratto
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReaderB.Read

                sStr1 = "Annullata la morosità"

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                              & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", par.IfNull(myReaderB("ID"), -1)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MOROSITA_ANNULLATA), "00")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '************************************************

            End While
            myReaderB.Close()





            'COMMIT
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Commit()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            par.cmd.Dispose()
            AnnullaMAV = True



        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & IdConnessione)
            End If
            AnnullaMAV = False

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Function

End Class
