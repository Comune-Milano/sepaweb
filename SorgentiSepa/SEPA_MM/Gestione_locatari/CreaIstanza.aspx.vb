Imports Telerik.Web.UI
Imports System.ServiceModel
Imports System.ServiceModel.Channels

Partial Class Gestione_locatari_CreaIstanza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            idMotivoIstanza.Value = par.IfEmpty(Request.QueryString("IDM"), 0)
            idcont.Value = par.IfEmpty(Request.QueryString("IDC"), 0)
            par.caricaComboTelerik("select * from t_motivo_presentaz_vsa where fl_nuova_normativa=1 order by id asc", RadComboModPres, "id", "descrizione", True)
            PrendiUltima()
        End If
    End Sub

    Private Sub PrendiUltima()
        Try
            connData.apri(False)

            Dim id_dichia As Long = 0
            Dim id_dichia1 As Long = 0
            Dim data_Fine As String = ""
            Dim data_Fine1 As String = ""
            Dim numComponImport As Integer = 0
            Dim numComponRU As Integer = 0

            Dim MESSAGGIO As String = "ATTENZIONE: nessuna situazione anagrafico-reddituale aggiornata è stata trovata. Verranno importati i dati anagrafici presentati in fase di stipula contrattuale.<br/>" _
                & "Prima di aprire l'istanza sarà necessario procedere alla verifica dei dati anagrafici del nucleo tramite servizio SIPO - Anagrafe Centrale del Comune di Milano."
            Dim MESSAGGIO1 As String = ""
            Dim MESSAGGIO2 As String = ""
            Dim tipoProvenienza As String = ""

            par.cmd.CommandText = "SELECT count(ID_anagrafica) AS NUMC FROM siscom_mi.soggetti_contrattuali WHERE nvl(data_fine,'29991231')>='" & Format(Now, "yyyyMMdd") & "'" _
                & " and id_contratto=" & idcont.Value
            Dim myReaderNC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderNC.Read Then
                numComponRU = par.IfNull(myReaderNC("NUMC"), 0)
            End If
            myReaderNC.Close()


            par.cmd.CommandText = "Select MAX(ID) As ID_BANDO FROM BANDI_VSA WHERE STATO = 1"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderB.Read Then
                id_bando.Value = myReaderB("ID_BANDO")
            End If
            myReaderB.Close()

            par.cmd.CommandText = "Select UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE As NOME_BANDO,UTENZA_BANDI.ANNO_ISEE,id_tipo_provenienza FROM siscom_mi.rapporti_utenza,UTENZA_DICHIARAZIONI,UTENZA_BANDI " _
               & " WHERE rapporti_utenza.id=" & idcont.Value & " And NVL(FL_GENERAZ_AUTO,0)=0 And (UTENZA_DICHIARAZIONI.NOTE_WEB Is NULL Or UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
           & "AND RAPPORTO=rapporti_utenza.cod_contratto ORDER BY UTENZA_DICHIARAZIONI.DATA_FINE_VAL DESC"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                tipoProvenienza = myReader2("id_tipo_provenienza")
                id_dichia1 = myReader2("ID")
                data_Fine1 = par.IfNull(myReader2("DATA_FINE_VAL"), Format(Now, "yyyyMMdd"))
                MESSAGGIO1 = "ATTENZIONE: verranno importati tutti i dati utili ai fini della presente Tipologia di Istanza, a partire da: <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br />" _
                    & "Prima di aprire l'istanza sarà necessario procedere alla verifica dei dati anagrafici del nucleo tramite servizio SIPO - Anagrafe Centrale del Comune di Milano."

            End If
            myReader2.Close()


            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=" & idcont.Value & "" _
                & " AND DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM=RAPPORTI_UTENZA.COD_CONTRATTO " _
                & " AND (NVL(fl_autorizzazione,0)=1 OR NVL(id_stato_istanza,0)=5)  ORDER BY replace(DICHIARAZIONI_VSA.DATA_FINE_VAL,'29991231','20200114') DESC,DICHIARAZIONI_VSA.ID desc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("fl_autorizzazione"), 0) = 1 Or par.IfNull(myReader1("id_stato_istanza"), 0) = 5 Then
                    id_dichia = myReader1("ID_DICHIARAZIONE")
                    data_Fine = par.IfNull(myReader1("DATA_FINE_VAL"), myReader1("data_evento"))
                    If data_Fine = "29991231" Then data_Fine = Format(Now, "yyyyMMdd")
                    MESSAGGIO = "ATTENZIONE: verranno importati tutti i dati utili ai fini della presente Tipologia di Istanza, a partire dalla domanda di <b>""" & myReader1("DESCRIZIONE") & """</b> presentata in data " & par.FormattaData(myReader1("DATA_presentazione")) & ".<br/>" _
                        & "Prima di aprire l'istanza sarà necessario procedere alla verifica dei dati anagrafici del nucleo tramite servizio SIPO - Anagrafe Centrale del Comune di Milano."
                End If
            End If
            myReader1.Close()


            If data_Fine = "" And data_Fine1 = "" Then
                tipoDomImportata.Value = "4"
                lblMsgImport.Text = MESSAGGIO
                numComponImport = Intestatari(idcont.Value)
            Else
                If data_Fine >= data_Fine1 Then
                    tipoDomImportata.Value = "1"
                    lblMsgImport.Text = MESSAGGIO
                    lIdDichiarazione.Value = id_dichia
                    numComponImport = Intestatari1()

                Else
                    tipoDomImportata.Value = tipoProvenienza
                    lIdDichiarazione.Value = id_dichia1
                    lblMsgImport.Text = MESSAGGIO1
                    numComponImport = Intestatari2()
                End If
            End If

            If idMotivoIstanza.Value = 1 Then
                If ListaInt.Items.Count = 1 Then
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Nucleo composto da un solo componente. Impossibile procedere col subentro!", 450, 150, "Attenzione", Nothing, Nothing)
                    btnAnagrafeSipo.Visible = False
                End If
            End If

            If numComponRU <> numComponImport Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Il nucleo familiare del contratto non è allineato all\'ultima istanza inserita a sistema.", 450, 150, "Attenzione", Nothing, Nothing)
            End If


            connData.chiudi(False)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Private Function Intestatari(ByVal idc As String) As Integer
        par.cmd.CommandText = "SELECT COGNOME||' '||NOME AS NOMINATIVO,ID,data_nascita,cod_fiscale,cognome,nome FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
            & " WHERE (SOGGETTI_CONTRATTUALI.DATA_FINE IS NULL OR SOGGETTI_CONTRATTUALI.DATA_FINE>='" & Format(Now, "yyyyMMdd") & "') AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idc & " " _
            & " AND  ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY ANAGRAFICA.COGNOME ASC,ANAGRAFICA.NOME ASC"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read
            If par.IfNull(myReader("data_nascita"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " NON HA LA DATA DI NASCITA!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If
            If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = False Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA UN CODICE FISCALE ERRATO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.IfNull(myReader("cognome"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO COGNOME VUOTO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.IfNull(myReader("NOME"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO NOME VUOTO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.RicavaEta(par.IfNull(myReader("data_nascita"), Format(Now, "yyyyMMdd"))) >= 18 Then
                Dim lsiFrutto As New ListItem(myReader("nominativo") & " - " & myReader("cod_fiscale"), myReader("cod_fiscale"))
                ListaInt.Items.Add(lsiFrutto)
                lsiFrutto = Nothing
            Else
                Dim lsiFrutto As New ListItem(myReader("nominativo") & " - " & myReader("cod_fiscale"), myReader("cod_fiscale"))
                ListaInt.Items.Add(lsiFrutto)
                lsiFrutto.Enabled = False
            End If
        End While

        myReader.Close()

        Dim Trovato As Boolean = False
        If idMotivoIstanza.Value = "1" Then
            par.cmd.CommandText = "SELECT ANAGRAFICA.cod_Fiscale,cod_tipologia_occupante FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE /*soggetti_contrattuali.cod_tipologia_occupante<>'INTE'*/ " _
            & " nvl(DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idc
        Else
            par.cmd.CommandText = "SELECT ANAGRAFICA.cod_Fiscale,cod_tipologia_occupante FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE soggetti_contrattuali.cod_tipologia_occupante='INTE' " _
            & " AND nvl(DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' and ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idc
        End If
        myReader = par.cmd.ExecuteReader()
        If myReader.HasRows Then
            Do While myReader.Read
                For Each R As ListItem In ListaInt.Items
                    If R.Enabled = True Then
                        If idMotivoIstanza.Value = "1" Then
                            If par.IfNull(myReader("cod_tipologia_occupante"), 0) = "INTE" Then
                                If R.Value = par.IfNull(myReader("cod_Fiscale"), 0) Then
                                    R.Enabled = False
                                End If
                            Else
                                R.Selected = True
                                Trovato = True
                            End If
                        Else
                            If R.Value = par.IfNull(myReader("cod_Fiscale"), 0) Then
                                R.Selected = True
                                Trovato = True
                                Exit For
                            End If
                        End If
                    End If
                Next
            Loop
        Else
            ListaInt.Enabled = True
        End If
        myReader.Close()
        If Trovato = False Then
            ListaInt.Enabled = True
        End If

        Return ListaInt.Items.Count
    End Function

    Private Function Intestatari1() As Integer

        par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.* FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read

            If par.IfNull(myReader("data_nascita"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " NON HA LA DATA DI NASCITA!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If
            'If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = False Then
            '    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA UN CODICE FISCALE ERRATO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            'End If

            If par.IfNull(myReader("cognome"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO COGNOME VUOTO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.IfNull(myReader("NOME"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO NOME VUOTO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.RicavaEta(par.IfNull(myReader("data_nascita"), Format(Now, "yyyyMMdd"))) >= 18 Then
                Dim lsiFrutto As New ListItem(Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " - " & myReader("cod_fiscale"), myReader("cod_fiscale"))
                ListaInt.Items.Add(lsiFrutto)
                lsiFrutto = Nothing
            Else
                Dim lsiFrutto As New ListItem(Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " - " & myReader("cod_fiscale"), myReader("cod_fiscale"))
                ListaInt.Items.Add(lsiFrutto)
                lsiFrutto.Enabled = False
            End If
            'End If
        End While

        myReader.Close()

        Dim codFiscaleIntest As String = ""

        codFiscaleIntest = RicavaCFIntestatario(idcont.Value)

        Dim Trovato As Boolean = False
        If idMotivoIstanza.Value = "1" Then
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE id_dichiarazione=" & lIdDichiarazione.Value
        Else
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE cod_fiscale = '" & codFiscaleIntest.ToUpper & "' and id_dichiarazione=" & lIdDichiarazione.Value
        End If
        myReader = par.cmd.ExecuteReader()
        If myReader.HasRows Then
            Do While myReader.Read
                For Each R As ListItem In ListaInt.Items
                    If R.Enabled = True Then
                        If idMotivoIstanza.Value = "1" Then
                            If R.Value = codFiscaleIntest.ToUpper Then
                                R.Enabled = False
                            Else
                                R.Selected = True
                                Trovato = True
                            End If
                        Else
                            If R.Value = par.IfNull(myReader("cod_Fiscale"), "") Then
                                R.Selected = True
                                Trovato = True
                                Exit For
                            End If
                        End If
                    End If
                Next
            Loop
        Else
            ListaInt.Enabled = True
        End If
        myReader.Close()
        If Trovato = False Then
            ListaInt.Enabled = True
        End If
        Return ListaInt.Items.Count
    End Function

    Private Function Intestatari2() As Integer

        par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.* FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read
            If par.IfNull(myReader("data_nascita"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " NON HA LA DATA DI NASCITA!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If
            If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = False Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA UN CODICE FISCALE ERRATO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.IfNull(myReader("cognome"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO COGNOME VUOTO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.IfNull(myReader("NOME"), "") = "" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "" & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO NOME VUOTO!! La situazione dovrà essere corretta nella nuova istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            If par.RicavaEta(par.IfNull(myReader("data_nascita"), Format(Now, "yyyyMMdd"))) >= 18 Then
                Dim lsiFrutto As New ListItem(Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " - " & myReader("cod_fiscale"), myReader("cod_fiscale"))
                ListaInt.Items.Add(lsiFrutto)
                lsiFrutto = Nothing
            Else
                Dim lsiFrutto As New ListItem(Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " - " & myReader("cod_fiscale"), myReader("cod_fiscale"))
                ListaInt.Items.Add(lsiFrutto)
                lsiFrutto.Enabled = False
            End If
        End While

        myReader.Close()

        Dim codFiscaleIntest As String = ""

        codFiscaleIntest = RicavaCFIntestatario(idcont.Value)
        Dim Trovato As Boolean = False

        If idMotivoIstanza.Value = "1" Then
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE id_dichiarazione=" & lIdDichiarazione.Value
        Else
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE cod_fiscale = '" & codFiscaleIntest.ToUpper & "' and id_dichiarazione=" & lIdDichiarazione.Value
        End If
        myReader = par.cmd.ExecuteReader()
        If myReader.HasRows Then
            Do While myReader.Read
                For Each R As ListItem In ListaInt.Items
                    If R.Enabled = True Then
                        If idMotivoIstanza.Value = "1" Then
                            If R.Value = codFiscaleIntest.ToUpper Then
                                R.Enabled = False
                            Else
                                R.Selected = True
                                Trovato = True
                            End If
                        Else
                            If R.Value = par.IfNull(myReader("cod_Fiscale"), "") Then
                                R.Selected = True
                                Trovato = True
                                Exit For
                            End If
                        End If
                    End If
                Next
            Loop
        Else
            ListaInt.Enabled = True
        End If
        myReader.Close()
        If Trovato = False Then
            ListaInt.Enabled = True
        End If

        Return ListaInt.Items.Count
    End Function

    Private Function RicavaCFIntestatario(ByVal idContratto As Long) As String

        Dim codFiscale As String = ""
        par.cmd.CommandText = "SELECT ANAGRAFICA.cod_fiscale FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND COD_tipologia_occupante='INTE' and id_contratto=" & idContratto
        Dim myReaderIdA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If myReaderIdA.Read Then
            codFiscale = par.IfNull(myReaderIdA("cod_fiscale"), "")
        End If
        myReaderIdA.Close()

        Return codFiscale
    End Function

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcedi.Click
        Try
            Select Case tipoDomImportata.Value
                Case "1"
                    ImportDaVSA()
                Case "4"
                    ImportDaRU()
                Case Else
                    ImportDaAU()
            End Select

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub ImportDaRU()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim num_comp_nucleo As Integer
        Dim prog As Integer
        Dim prog_int As Integer
        Dim prog_comp As Integer
        'Dim anno As Integer
        Dim strScript As String
        Dim idLUOGOnasc As Integer

        connData.apri(True)
        par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            lValoreCorrente = par.IfNull(myReader(0), 0) + 1
        End If
        myReader.Close()
        par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
        par.cmd.ExecuteNonQuery()
        valorePG = Format(lValoreCorrente, "0000000000")


        par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
        Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderNew.Read() Then
            new_id_dom.Value = myReaderNew(0)
        End If
        myReaderNew.Close()

        par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.NEXTVAL FROM DUAL"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            new_idDichia.Value = myReader1(0)
        End If
        myReader1.Close()

        Dim annoReddito As Integer = Year(Now)
        codFisc.Value = ListaInt.SelectedValue

        par.cmd.CommandText = "SELECT COUNT(ID_ANAGRAFICA) AS NUM_COMP_NUCLEO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO = " & idcont.Value & " AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            num_comp_nucleo = myReader("NUM_COMP_NUCLEO")
        End If
        myReader.Close()


        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & idcont.Value & " AND " _
        & "(ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
        Dim myReaderAnagr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderAnagr.Read Then
            id_intest.Value = myReaderAnagr("ID")

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD ='" & par.IfNull(myReaderAnagr("COD_COMUNE_NASCITA"), "") & "'" _
                & " OR NOME ='" & par.IfNull(myReaderAnagr("COD_COMUNE_NASCITA"), "") & "'"
            Dim myReaderIDluogoNAS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIDluogoNAS.Read Then
                idLUOGOnasc = par.IfNull(myReaderIDluogoNAS("ID"), 0)
            End If
            myReaderIDluogoNAS.Close()


            par.cmd.CommandText = "INSERT INTO Dichiarazioni_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
            & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,CAP_RES_DNTE,MOD_PRESENTAZIONE,N_COMP_NUCLEO,PROGR_DNTE,ANNO_SIT_ECONOMICA,DATA_FINE_VAL,DATA_INIZIO_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO,PG_COLLEGATO)" _
            & " VALUES (" & new_idDichia.Value & "," & Session.Item("ID_CAF") & "," & id_bando.Value & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
            & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLUOGOnasc & ",'" & par.IfNull(myReaderAnagr("TELEFONO"), "") & "'," _
            & "'','','" & par.PulisciStrSql(par.IfNull(myReaderAnagr("INDIRIZZO_RESIDENZA"), "")) & "','" & par.IfNull(myReaderAnagr("CIVICO_RESIDENZA"), "") & "'," _
            & "'" & par.IfNull(myReaderAnagr("CAP_RESIDENZA"), "") & "'," & RadComboModPres.SelectedValue & "," & num_comp_nucleo & ",0," & annoReddito & ",'" & Mid(par.AggiustaData(RadDateEvento.SelectedDate), 1, 4) & "1231" & "','" & par.AggiustaData(RadDateEvento.SelectedDate) & "','','','')"

            par.cmd.ExecuteNonQuery()
        End If
        myReaderAnagr.Close()

        Dim id_compon As Long = 0
        Dim new_id_compon As Long = 0

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & idcont.Value & " AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
        Dim myReaderComNucleo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReaderComNucleo.Read
            id_compon = myReaderComNucleo("ID")
            prog = 1
            If id_intest.Value = id_compon Then
                prog = 0
            Else
                prog_int = prog_int + 1
            End If
            par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                new_id_compon = myReader1(0)
            End If
            myReader1.Close()

            If Not prog = 0 Then
                prog_comp = prog_int
            Else
                prog_comp = prog
            End If

            par.cmd.CommandText = "SELECT TIPOLOGIA_PARENTELA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD(+)" _
                & " AND SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=" & id_compon
            Dim myReaderParente As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderParente.Read Then
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PROGR,PERC_INVAL,INDENNITA_ACC) " _
                & "VALUES (" & new_id_compon & "," & new_idDichia.Value & ",'" & par.IfNull(myReaderComNucleo("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("COGNOME"), "")) & "'," _
                & "'" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("NOME"), "")) & "','" & par.IfNull(myReaderComNucleo("SESSO"), "") & "','" & par.IfNull(myReaderComNucleo("DATA_NASCITA"), "") & "'," _
                & par.IfNull(myReaderParente("ID_SEPA"), "") & "," & prog_comp & ",0,'0')"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PROGR,PERC_INVAL,INDENNITA_ACC) " _
                    & "VALUES (" & new_id_compon & "," & new_idDichia.Value & ",'" & par.IfNull(myReaderComNucleo("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("COGNOME"), "")) & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("NOME"), "")) & "','" & par.IfNull(myReaderComNucleo("SESSO"), "") & "','" & par.IfNull(myReaderComNucleo("DATA_NASCITA"), "") & "'," _
                    & "NULL," & prog_comp & ",0,'0')"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderParente.Close()

        End While
        myReaderComNucleo.Close()


        ImportDaRU_2()

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Apri", "CancelEdit();CenterPage2('Istanza.aspx?ONE=1&IDD=" & new_idDichia.Value & "&IDM=" & idMotivoIstanza.Value & "', 'Istanza' + " & Format(Now, "yyyyMMddmmhhss") & ", 1300, 750);", True)

        connData.chiudi(True)


    End Sub

    Protected Sub ImportDaRU_2()

        Dim iDluogo As Long
        Dim codIndir As Integer
        Dim sup_netta As Decimal
        Dim ascens As Integer
        Dim lValoreCorrenteDom As Long
        Dim valorePGdom As String
        Dim tipoBando As Long
        Dim anno As Integer


        par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            lValoreCorrenteDom = par.IfNull(myReader(0), 0) + 1
        End If
        myReader.Close()
        par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrenteDom & ")"
        par.cmd.ExecuteNonQuery()
        valorePGdom = Format(lValoreCorrenteDom, "0000000000")

        par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID=" & id_bando.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            tipoBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
            anno = par.IfNull(myReader("ANNO_ISEE"), "-1")
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
        & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.id =" & idcont.Value & " AND (ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & myReader("LUOGO_COR") & "'"
            Dim myReaderIDluogo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIDluogo.Read Then
                iDluogo = myReaderIDluogo("ID")
            End If
            myReaderIDluogo.Close()

            par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE DESCRIZIONE='" & myReader("TIPO_COR") & "'"
            Dim myReaderTipoIndir As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderTipoIndir.Read Then
                codIndir = myReaderTipoIndir("COD")
            End If
            myReaderTipoIndir.Close()

            Dim dataPr As String = par.AggiustaData(RadDatePresentaz.SelectedDate) 'Format(Now, "yyyyMMdd")
            Dim dataEv As String = par.AggiustaData(RadDateEvento.SelectedDate)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & " RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_IMMOBILIARI.id_unita_principale is null and UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND ID_CONTRATTO =" & idcont.Value
            Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderUI.Read Then
                CodContratto.Value = myReaderUI("cod_contratto")
                par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                    & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                    & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,CAP_REC_DNTE,ID_MOTIVO_DOMANDA," _
                    & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                    & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                    & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                    & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                    & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                    & "FL_ASS_ESTERNA,FL_FATTA_AU,FL_FATTA_ERP,FL_FAI_ERP,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                    & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT," _
                    & " COD_CONTRATTO_SCAMBIO,FL_NUOVA_NORMATIVA,ID_CONTRATTO,ID_STATO_ISTANZA) " _
                    & "VALUES (" & new_id_dom.Value & "," & id_bando.Value & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia.Value & ",0," _
                    & "'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReader("PRESSO_COR"), "")) & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReader("VIA_COR"), "")) & "','" & iDluogo & "','" & par.IfNull(myReader("TELEFONO"), "") & "','" & codIndir & "'," _
                    & "'" & par.IfNull(myReader("CIVICO_COR"), "") & "','" & par.IfNull(myReader("CAP_COR"), "") & "'," & idMotivoIstanza.Value & "," _
                    & "'" & dataPr & "','0','1','0','" & anno & "',0,'" & anno & "',0,-1,-1,-1,-1,-1,-1,-1,-1," _
                    & "-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                    & par.IfNull(myReader("DATA_STIPULA"), "") & "','" & myReaderUI("COD_UNITA_IMMOBILIARE") & "','0',-1,'" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "'," _
                    & "'1','1','1','0','1','1','1','1','1','1','1','0','1','1','1','0','0','0','','" & dataEv & "'," & tipoDomImportata.Value & "," & idcont.Value & ",'',1," & idcont.Value & ",1)"

                par.cmd.ExecuteNonQuery()

                'Campi non inseriti: REDDITO_ISEE,ISR_ERP,ISP_ERP,ISE_ERP,MINORI_CARICO,PSE,VSE,CARTA_I,CARTA_I_DATA,PERMESSO_SOGG_N,
                'PERMESSO_SOGG_DATA, PERMESSO_SOGG_SCADE, PERMESSO_SOGG_RINNOVO, CARTA_SOGG_N, CARTA_SOGG_DATA, CARTA_I_RILASCIATA

                par.cmd.CommandText = "INSERT INTO COMP_NAS_RES_VSA (ID_COMPONENTE,ID_LUOGO_NAS_DNTE,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE," _
                & "CIVICO_RES_DNTE,TELEFONO_DNTE,CAP_RES) VALUES (" & id_intest.Value & ",'',null," _
                & "'" & par.PulisciStrSql(par.IfNull(myReader("INDIRIZZO_RESIDENZA"), "")) & "','" & par.IfNull(myReader("CIVICO_RESIDENZA"), "") & "'," _
                & "'" & par.IfNull(myReader("TELEFONO"), "") & "','" & par.IfNull(myReader("CAP_RESIDENZA"), "") & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & par.IfNull(myReaderUI("ID"), "") & " AND COD_TIPOLOGIA='SUP_NETTA'"
                Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderMQ.Read Then
                    sup_netta = par.IfNull(myReaderMQ("VALORE"), "")
                End If
                myReaderMQ.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & par.IfNull(myReaderUI("ID_INDIRIZZO"), "")
                Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderInd.Read Then

                    'query per verificare la presenza o meno dell'ascensore
                    par.cmd.CommandText = "SELECT IMPIANTI.* FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.SCALE_EDIFICI WHERE IMPIANTI.ID = IMPIANTI_SCALE.ID_IMPIANTO " _
                    & "AND SCALE_EDIFICI.ID = IMPIANTI_SCALE.ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = '" & myReaderUI("ID_EDIFICIO") & "' AND SCALE_EDIFICI.DESCRIZIONE = '" & par.IfNull(myReaderUI("SCALA"), "") & "' AND IMPIANTI.COD_TIPOLOGIA = 'SO'"
                    Dim myReaderAsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAsc.Read Then
                        ascens = 1
                    Else
                        ascens = 0
                    End If
                    myReaderAsc.Close()

                    par.cmd.CommandText = "INSERT INTO DOMANDE_VSA_ALLOGGIO (ID_DOMANDA,COMUNE,CAP,INDIRIZZO,CIVICO,ID_TIPO_GESTORE," _
                    & "SCALA,PIANO,INTERNO,NUM_CONTRATTO,DEC_CONTRATTO,ASS_TEMPORANEA,ID_TIPO_CONTRATTO,COD_UNITA_IMMOBILIARE,SUP_NETTA,ASCENSORE)" _
                    & "VALUES (" & new_id_dom.Value & ",'" & par.IfNull(myReaderInd("LOCALITA"), "") & "','" & par.IfNull(myReaderInd("CAP"), "") & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "','" & par.IfNull(myReaderInd("CIVICO"), "") & "','9','" & par.IfNull(myReaderUI("SCALA"), "") & "'," _
                    & "'" & par.IfNull(myReaderUI("COD_TIPO_LIVELLO_PIANO"), "") & "','" & par.IfNull(myReaderUI("INTERNO"), "") & "','" & par.IfNull(myReader("COD_CONTRATTO"), "") & "'," _
                    & "'" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "','0','0','" & par.IfNull(myReaderUI("COD_UNITA_IMMOBILIARE"), "") & "','" & sup_netta & "','" & ascens & "')"
                    par.cmd.ExecuteNonQuery()

                End If
                myReaderInd.Close()

                par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                    & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & idcont.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIntest.Read Then
                    par.cmd.CommandText = "select ID from COMUNI_NAZIONI where COD='" & par.IfNull(myReaderIntest("COD_COMUNE_NASCITA"), "") & "'"
                    Dim IDluogoNascita = par.IfNull(par.cmd.ExecuteScalar, "0")

                    par.cmd.CommandText = "INSERT INTO INTESTATARI_CONTRATTI_VSA (ID_DOMANDA,COGNOME,NOME,SESSO,COD_FISCALE,ID_LUOGO_NAS_DNTE,DATA_NASCITA_DNTE) VALUES " _
                    & "(" & new_id_dom.Value & ",'" & par.PulisciStrSql(par.IfNull(myReaderIntest("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderIntest("NOME"), "")) & "'," _
                    & "'" & par.IfNull(myReaderIntest("SESSO"), "") & "','" & par.IfNull(myReaderIntest("COD_FISCALE"), "") & "'," _
                    & " " & IDluogoNascita & ",'" & par.IfNull(myReaderIntest("DATA_NASCITA"), "") & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderIntest.Close()

            End If
            myReaderUI.Close()

        End If
        myReader.Close()

        'Aggiungo eventuali ospiti

        par.cmd.CommandText = "SELECT * FROM siscom_mi.OSPITI WHERE nvl(DATA_FINE_OSPITE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' and ID_CONTRATTO=" & idcont.Value & ""
        Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader12.Read
            par.cmd.CommandText = "SELECT COMP_NUCLEO_OSPITI_VSA.* FROM COMP_NUCLEO_OSPITI_VSA,comp_nucleo_vsa,dichiarazioni_vsa,domande_bando_vsa " _
                & " WHERE " _
                & " comp_nucleo_vsa.id_dichiarazione=dichiarazioni_vsa.id " _
                & " and domande_bando_vsa.id_dichiarazione=dichiarazioni_vsa.id " _
                & " and domande_bando_vsa.id=COMP_NUCLEO_OSPITI_VSA.id_domanda " _
                & " and COMP_NUCLEO_OSPITI_VSA.COD_FISCALE='" & par.IfNull(myReader12("COD_FISCALE"), "") & "' order by dichiarazioni_vsa.id desc "
            Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderR.Read Then
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_OSPITI_VSA (ID, ID_DOMANDA, DATA_AGG, DATA_INIZIO_OSPITE, DATA_FINE_OSPITE, COGNOME, NOME, COD_FISCALE, DATA_NASC, ID_TIPO_IND_RES_DNTE, IND_RES_DNTE, CIVICO_RES_DNTE, COMUNE_RES_DNTE, CAP_RES_DNTE, CARTA_I, CARTA_I_DATA, CARTA_I_RILASCIATA, PERMESSO_SOGG_N, PERMESSO_SOGG_DATA, FL_REFERENTE, GRADO_PARENTELA, ID_TIPO_RUOLO) " _
                                       & "VALUES " _
                                       & "(SEQ_COMP_NUCLEO_OSPITI_VSA.NEXTVAL, " & new_id_dom.Value & ", " & par.insDbValue(par.IfNull(myReaderR("DATA_AGG"), ""), True) & ", " & par.insDbValue(myReaderR("DATA_INIZIO_OSPITE"), True) & ", " _
                                       & "" & par.insDbValue(par.IfNull(myReaderR("DATA_FINE_OSPITE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COGNOME"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("NOME"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COD_FISCALE"), ""), True) _
                                       & ", " & par.insDbValue(par.IfNull(myReaderR("DATA_NASC"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("ID_TIPO_IND_RES_DNTE"), ""), False) & ", " & par.insDbValue(par.IfNull(myReaderR("IND_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CIVICO_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COMUNE_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CAP_RES_DNTE"), ""), True) & ", " _
                                       & "" & par.insDbValue(par.IfNull(myReaderR("CARTA_I"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CARTA_I_DATA"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CARTA_I_RILASCIATA"), ""), True) & "," & par.insDbValue(par.IfNull(myReaderR("PERMESSO_SOGG_N"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("PERMESSO_SOGG_DATA"), ""), True) & ", " _
                                       & "NULL," & par.IfNull(myReaderR("GRADO_PARENTELA"), "") & "," & par.IfNull(myReaderR("ID_TIPO_RUOLO"), "1") & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderR.Close()
        End While
        myReader12.Close()

        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
        & "VALUES (" & new_id_dom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
        & "','F190','','I')"
        par.cmd.ExecuteNonQuery()


    End Sub

    Protected Sub ImportDaVSA()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim i As Integer = 1
        Dim strScript As String

        connData.apri(True)
        par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            lValoreCorrente = par.IfNull(myReader(0), 0) + 1
        End If
        myReader.Close()
        par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
        par.cmd.ExecuteNonQuery()
        valorePG = Format(lValoreCorrente, "0000000000")

        codFisc.Value = ListaInt.SelectedValue

        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(ListaInt.SelectedItem.Text) & "%')"
        myReader = par.cmd.ExecuteReader()
        If myReader.HasRows Then
            While myReader.Read
                If par.IfNull(myReader("COD_FISCALE"), "") = codFisc.Value Then
                    id_intest.Value = myReader("ID") 'ottengo l'id dell'intestatario che intende presentare la domanda
                End If
            End While
            myReader.Close()
        Else
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " AND PROGR=0"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                id_intest.Value = myReader0("ID")
            End If
            myReader0.Close()
        End If

        par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
        Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderNew.Read() Then
            new_id_dom.Value = myReaderNew(0)
        End If
        myReaderNew.Close()

        par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.NEXTVAL FROM DUAL"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            new_idDichia.Value = myReader1(0)
        End If
        myReader1.Close()

        Dim idLuogoNascita As Integer = 0
        par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DICHIARAZIONI_VSA.ID = " & lIdDichiarazione.Value & " AND COMP_NUCLEO_VSA.COD_FISCALE='" & codFisc.Value & "' AND COMP_NUCLEO_VSA.PROGR =0"
        Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderCodF.Read = False Then
            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & codFisc.Value.Substring(11, 4) & "'"
            Dim myReaderCom As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderCom.Read Then
                idLuogoNascita = par.IfNull(myReaderCom("ID"), "")
            End If
            myReaderCom.Close()
        End If
        myReaderCodF.Close()

        par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID = " & lIdDichiarazione.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            If idLuogoNascita = 0 Then
                idLuogoNascita = par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
            End If

            Dim annoReddito As Integer = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

            par.cmd.CommandText = "INSERT INTO DICHIARAZIONI_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
                & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,N_COMP_NUCLEO,N_INV_100_CON,N_INV_100_SENZA,N_INV_100_66,ID_TIPO_CAT_AB,ANNO_SIT_ECONOMICA,LUOGO_INT_ERP," _
                & " DATA_INT_ERP,LUOGO_S,DATA_S,PROGR_DNTE,FL_GIA_TITOLARE,CAP_RES_DNTE,FL_UBICAZIONE,POSSESSO_UI,FL_APPLICA_36,MINORI_CARICO,ISEE,ISE_ERP,ISR_ERP,ISP_ERP,PSE,VSE,MOD_PRESENTAZIONE,DATA_INIZIO_VAL,DATA_FINE_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO) " _
                & " VALUES (" & new_idDichia.Value & "," & Session.Item("ID_CAF") & "," & id_bando.Value & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
                & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLuogoNascita & ",'','" _
                & "',null,'','" _
                & "'," & par.IfNull(myReader("N_COMP_NUCLEO"), "''") & "," & par.IfNull(myReader("N_INV_100_CON"), "''") & "," _
                & par.IfNull(myReader("N_INV_100_SENZA"), "''") & "," & par.IfNull(myReader("N_INV_100_66"), "''") & "," & par.IfNull(myReader("ID_TIPO_CAT_AB"), "''") & "," _
                & annoReddito & ",'" & par.IfNull(myReader("LUOGO_INT_ERP"), "") & "','" & par.IfNull(myReader("DATA_INT_ERP"), "") & "','" _
                & par.IfNull(myReader("LUOGO_S"), "") & "','" & par.IfNull(myReader("DATA_S"), "") & "'," & par.IfNull(myReader("PROGR_DNTE"), "''") & ",'" _
                & par.IfNull(myReader("FL_GIA_TITOLARE"), "") & "','','" & par.IfNull(myReader("FL_UBICAZIONE"), "") & "'," _
                & par.IfNull(myReader("POSSESSO_UI"), "''") & ",'" & par.IfNull(myReader("FL_APPLICA_36"), "") & "','" & par.IfNull(myReader("MINORI_CARICO"), "") & "','" _
                & par.IfNull(myReader("ISEE"), "") & "',0,'','" _
                & "','" & par.IfNull(myReader("PSE"), "") & "','" & par.IfNull(myReader("VSE"), "") & "'," & RadComboModPres.SelectedValue & ",'" & par.AggiustaData(RadDateEvento.SelectedDate) & "','" & Mid(par.AggiustaData(RadDateEvento.SelectedDate), 1, 4) & "1231" & "','','')"
            par.cmd.ExecuteNonQuery()

        End If
        myReader.Close()

        Dim id_compon As Long = 0
        Dim new_id_compon As Long = 0



        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value
        myReader = par.cmd.ExecuteReader()
        While myReader.Read '-------------- CICLO SUI COMPONENTI DEL NUCLEO FAMILIARE --------------'
            id_compon = myReader("ID")

            par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
            Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderN.Read Then
                new_id_compon = myReaderN(0)
            End If
            myReaderN.Close()

            par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                & "VALUES (" & new_id_compon & "," & new_idDichia.Value & "," & par.IfNull(myReader("PROGR"), "") & ",'" & par.IfNull(myReader("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader("COGNOME"), "")) & "'," _
                & "'" & par.PulisciStrSql(par.IfNull(myReader("NOME"), "")) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "','" _
                & par.IfNull(myReader("PERC_INVAL"), "") & "','" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
            par.cmd.ExecuteNonQuery()

            If id_compon = id_intest.Value Then
                id_intest.Value = new_id_compon
            End If

            Dim distanzaKm As Decimal = 0
            Dim idRedditoTot As Long = 0

            par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE = " & id_compon
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader2.Read
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "'"
                Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCC.Read() Then
                    distanzaKm = par.IfNull(myReaderCC("DISTANZA_KM"), 0)
                End If
                myReaderCC.Close()

                par.cmd.CommandText = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA,DISTANZA_KM) " _
                    & "VALUES (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                    & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "'," _
                    & "'" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "','" & par.IfNull(myReader2("N_VANI"), "") & "'," _
                    & "'" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), 0) & "," _
                    & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), 0) & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & "," & distanzaKm & ")"
                par.cmd.ExecuteNonQuery()
            End While
            myReader2.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE = " & id_compon
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader3.Read
                par.cmd.CommandText = "INSERT INTO COMP_PATR_MOB_VSA (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO) " _
                    & "VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & ")"
                par.cmd.ExecuteNonQuery()
            End While
            myReader3.Close()

            Dim idReddComp As Long = 0
            Dim reddAltro As Decimal = 0
            par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE = " & id_compon
            Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader4.Read
                par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                    & "VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & new_id_compon & "," & par.VirgoleInPunti(par.IfNull(myReader4("REDDITO_IRPEF"), 0)) & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SEQ_COMP_REDDITO_VSA.currval from dual"
                Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCV.Read Then
                    idReddComp = myReaderCV(0)
                End If
                myReaderCV.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader5.Read
                    reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                    par.cmd.ExecuteNonQuery()
                End While
                myReader5.Close()
            End While
            myReader4.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE = " & id_compon
            Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader6.Read
                par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                & "VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                par.cmd.ExecuteNonQuery()
            End While
            myReader6.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_VSA WHERE ID_COMPONENTE = " & id_compon
            Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader7.Read
                par.cmd.CommandText = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                & "VALUES (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                par.cmd.ExecuteNonQuery()
            End While
            myReader7.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
            Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader8.Read
                par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID,ID_DOMANDA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & new_idDichia.Value & "," & new_id_compon & ",'" _
                & par.IfNull(myReader8("CONDIZIONE"), "0") & "','" & par.IfNull(myReader8("PROFESSIONE"), "0") & "','" _
                & par.IfNull(myReader8("DIPENDENTE"), "0") & "','" & par.IfNull(myReader8("PENSIONE"), "0") & "','" _
                & par.IfNull(myReader8("AUTONOMO"), "0") & "','" & par.IfNull(myReader8("NON_IMPONIBILI"), "0") & "','" _
                & par.IfNull(myReader8("DOM_AG_FAB"), "0") & "','" & par.IfNull(myReader8("OCCASIONALI"), "0") & "','" & par.IfNull(myReader8("ONERI"), "0") & "','" & par.IfNull(myReader8("PENS_ESENTE"), "0") & "','" & par.IfNull(myReader8("NO_ISEE"), "0") & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SEQ_UTENZA_REDDITI.currval from dual"
                Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCV.Read Then
                    idRedditoTot = myReaderCV(0)
                End If
                myReaderCV.Close()

                par.cmd.CommandText = "SELECT * FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddA.Read

                    par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (SELECT seq_VSA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID =" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddA.Close()

                par.cmd.CommandText = "SELECT * FROM VSA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddD.Read

                    par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (SELECT seq_VSA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_DIPEND_IMPORTI WHERE ID =" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddD.Close()

                par.cmd.CommandText = "SELECT * FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddN.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (SELECT seq_VSA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID =" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddN.Close()

                par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddP.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (SELECT seq_VSA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID =" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddP.Close()

                par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddP2.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (SELECT seq_VSA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_IMPORTI WHERE ID =" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddP2.Close()
            End While
            myReader8.Close()

        End While
        myReader.Close()

        par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = 0 WHERE ID =" & id_intest.Value
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID <> " & id_intest.Value & " AND ID_DICHIARAZIONE = " & new_idDichia.Value & " ORDER BY PROGR ASC FOR UPDATE NOWAIT"
        myReader = par.cmd.ExecuteReader
        While myReader.Read
            par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = " & i & " WHERE ID =" & myReader("ID")
            par.cmd.ExecuteNonQuery()
            i = i + 1
        End While
        myReader.Close()

        ImportDaVSA_2()

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Apri", "CancelEdit();CenterPage2('Istanza.aspx?ONE=1&IDD=" & new_idDichia.Value & "&IDM=" & idMotivoIstanza.Value & "', 'Istanza' + " & Format(Now, "yyyyMMddmmhhss") & ", 1300, 750);", True)

        connData.chiudi(True)
    End Sub

    Protected Sub ImportDaVSA_2()
        Dim iDluogo As Long
        Dim codIndir As Integer
        Dim sup_netta As Decimal
        Dim ascens As Integer
        Dim lValoreCorrenteDom As Long
        Dim valorePGdom As String
        Dim tipoBando As Long
        Dim cod_UI As String = ""


        par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            lValoreCorrenteDom = par.IfNull(myReader(0), 0) + 1
        End If
        myReader.Close()
        par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrenteDom & ")"
        par.cmd.ExecuteNonQuery()
        valorePGdom = Format(lValoreCorrenteDom, "0000000000")

        par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID=" & id_bando.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            tipoBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT DISTINCT(UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.id_unita_principale is null " _
            & " and  UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND ID_CONTRATTO=" & idcont.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            cod_UI = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "0")
        End If
        myReader.Close()

        '**************** RICAVO INFO DOCUMENTO DI RICONOSCIMENTO QUANDO IL RICHIEDENTE E' DIVERSO DA QUELLO VECCHIO ************
        Dim infoDocumento As Boolean = False
        Dim numDoc As String = ""
        Dim dataDoc As String = ""
        Dim rilascioDoc As String = ""
        Dim docSoggiorno As String = ""

        par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DICHIARAZIONI_VSA.ID = " & lIdDichiarazione.Value & " AND COMP_NUCLEO_VSA.COD_FISCALE='" & codFisc.Value & "' AND COMP_NUCLEO_VSA.PROGR =0"
        Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderCodF.Read = False Then
            par.cmd.CommandText = "SELECT SISCOM_MI.ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
            & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.id =" & idcont.Value & " AND (ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
            Dim myReaderCI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCI.Read Then
                infoDocumento = True
                numDoc = par.IfNull(myReaderCI("NUM_DOC"), "")
                dataDoc = par.IfNull(myReaderCI("DATA_DOC"), "")
                rilascioDoc = par.IfNull(myReaderCI("RILASCIO_DOC"), "")
                docSoggiorno = par.IfNull(myReaderCI("DOC_SOGGIORNO"), "")
            End If
            myReaderCI.Close()
        End If
        myReaderCodF.Close()


        par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then

            If infoDocumento = False Then
                numDoc = par.IfNull(myReader("CARTA_I"), "")
                dataDoc = par.IfNull(myReader("CARTA_I_DATA"), "")
                rilascioDoc = par.IfNull(myReader("CARTA_I_RILASCIATA"), "")
                docSoggiorno = par.IfNull(myReader("PERMESSO_SOGG_N"), "")
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE id =" & idcont.Value
            Dim myReaderContr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderContr.Read Then
                CodContratto.Value = myReaderContr("cod_contratto")

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE DESCRIZIONE='" & myReaderContr("TIPO_COR") & "'"
                Dim myReaderTipoIndir As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderTipoIndir.Read Then
                    codIndir = myReaderTipoIndir("COD")
                End If
                myReaderTipoIndir.Close()

                Dim dataPr As String = par.AggiustaData(RadDatePresentaz.SelectedDate) 'Format(Now, "yyyyMMdd")
                Dim dataEv As String = par.AggiustaData(RadDateEvento.SelectedDate)

                Dim idLUOGOnasc As Long = 0

                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione.Value
                Dim myReaderBandoVSA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderBandoVSA.Read Then

                    par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                        & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                        & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,REDDITO_ISEE,ISR_ERP,ISP_ERP,ISE_ERP,CAP_REC_DNTE,MINORI_CARICO,PSE,VSE,ID_MOTIVO_DOMANDA," _
                        & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                        & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                        & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                        & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                        & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                        & "FL_ASS_ESTERNA,CARTA_I,CARTA_I_DATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,PERMESSO_SOGG_SCADE,PERMESSO_SOGG_RINNOVO,FL_FATTA_AU,FL_FATTA_ERP," _
                        & "FL_FAI_ERP,CARTA_SOGG_N,CARTA_SOGG_DATA,CARTA_I_RILASCIATA,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                        & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT," _
                        & "TIPO_DOCUMENTO,COD_CONTRATTO_SCAMBIO,ID_INTEST_NEW_RU,FL_NUOVA_NORMATIVA,ID_CONTRATTO,ID_STATO_ISTANZA) " _
                            & "VALUES (" & new_id_dom.Value & "," & id_bando.Value & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia.Value & "," _
                            & myReaderBandoVSA("PROGR_COMPONENTE") & ",'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReaderBandoVSA("PRESSO_REC_DNTE"), "")) & "'," _
                            & "'','','',''," _
                            & "''," & CDec(par.VirgoleInPunti(par.IfNull(myReaderBandoVSA("REDDITO_ISEE"), "0"))) & "," & par.IfNull(par.VirgoleInPunti(myReaderBandoVSA("ISR_ERP")), "''") & "," _
                            & "" & par.IfNull(par.VirgoleInPunti(myReaderBandoVSA("ISP_ERP")), "''") & ", " & par.IfNull(par.VirgoleInPunti(myReaderBandoVSA("ISE_ERP")), "''") & ",'','" _
                            & " " & par.IfNull(myReaderBandoVSA("MINORI_CARICO"), "") & "','" & par.IfNull(myReaderBandoVSA("PSE"), "") & "','" & par.IfNull(myReaderBandoVSA("VSE"), "") & "'," & idMotivoIstanza.Value & "," _
                            & "'" & dataPr & "','0','1','0','" & par.IfNull(myReaderBandoVSA("ANNO_RIF_CANONE"), "") & "',0,'" & par.IfNull(myReaderBandoVSA("ANNO_RIF_SPESE_ACC"), "") & "'," _
                            & "0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                            & par.IfNull(myReaderBandoVSA("CONTRATTO_DATA"), "") & "','" & cod_UI & "','0',-1,'" & par.IfNull(myReaderBandoVSA("CONTRATTO_DATA_DEC"), "") & "'," _
                            & "'1','" & numDoc & "','" & dataDoc & "','" & docSoggiorno & "','" _
                            & par.IfNull(myReaderBandoVSA("PERMESSO_SOGG_DATA"), "") & "','" & par.IfNull(myReaderBandoVSA("PERMESSO_SOGG_SCADE"), "") & "','" & par.IfNull(myReaderBandoVSA("PERMESSO_SOGG_RINNOVO"), "") & "'," _
                            & "'1','1','0','" & par.IfNull(myReaderBandoVSA("CARTA_SOGG_N"), "") & "','" & par.IfNull(myReaderBandoVSA("CARTA_SOGG_DATA"), "") & "','" & par.PulisciStrSql(rilascioDoc) & "'," _
                            & "'1','1','1','1','1','1','1','0','1','1','1','0','0','0','','" & dataEv & "'," & tipoDomImportata.Value & "," & lIdDichiarazione.Value & "," & par.IfNull(myReaderBandoVSA("TIPO_DOCUMENTO"), 0) & ",'',null,1," & idcont.Value & ",1)"
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SCALE_EDIFICI WHERE COD_UNITA_IMMOBILIARE='" & cod_UI & "' AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA"
                    Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderUI.Read() Then

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & par.IfNull(myReaderUI("ID"), "") & " AND COD_TIPOLOGIA='SUP_NETTA'"
                        Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderMQ.Read Then
                            sup_netta = par.IfNull(myReaderMQ("VALORE"), "")
                        End If
                        myReaderMQ.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & par.IfNull(myReaderUI("ID_INDIRIZZO"), "")
                        Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderInd.Read Then

                            'query per verificare la presenza o meno dell'ascensore
                            par.cmd.CommandText = "SELECT IMPIANTI.* FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.SCALE_EDIFICI WHERE IMPIANTI.ID = IMPIANTI_SCALE.ID_IMPIANTO " _
                                & "AND SCALE_EDIFICI.ID = IMPIANTI_SCALE.ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = '" & myReaderUI("ID_EDIFICIO") & "' AND SCALE_EDIFICI.DESCRIZIONE = '" & myReaderUI("DESCRIZIONE") & "' AND IMPIANTI.COD_TIPOLOGIA = 'SO'"
                            Dim myReaderAsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAsc.Read Then
                                ascens = 1
                            Else
                                ascens = 0
                            End If
                            myReaderAsc.Close()

                            par.cmd.CommandText = "INSERT INTO DOMANDE_VSA_ALLOGGIO (ID_DOMANDA,COMUNE,CAP,INDIRIZZO,CIVICO,ID_TIPO_GESTORE," _
                                & "SCALA,PIANO,INTERNO,NUM_CONTRATTO,DEC_CONTRATTO,ASS_TEMPORANEA,ID_TIPO_CONTRATTO," _
                                & "COD_UNITA_IMMOBILIARE,SUP_NETTA,ASCENSORE) VALUES (" & new_id_dom.Value & ",'" & par.IfNull(myReaderInd("LOCALITA"), "") & "','" & par.IfNull(myReaderInd("CAP"), "") & "'," _
                                & "'" & par.PulisciStrSql(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "','" & par.IfNull(myReaderInd("CIVICO"), "") & "','9','" & par.IfNull(myReaderUI("descrizione"), "") & "'," _
                                & "'" & par.IfNull(myReaderUI("COD_TIPO_LIVELLO_PIANO"), "") & "','" & par.IfNull(myReaderUI("INTERNO"), "") & "','" & CodContratto.Value & "'," _
                                & "'" & par.IfNull(myReaderBandoVSA("CONTRATTO_DATA"), "") & "','0','0','" & cod_UI & "','" & sup_netta & "','" & ascens & "')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                                & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & idcont.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                            Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderIntest.Read Then

                                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD ='" & myReaderIntest("COD_COMUNE_NASCITA") & "'"
                                Dim myReaderLuogoNasc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderLuogoNasc.Read Then
                                    idLUOGOnasc = myReaderLuogoNasc("ID")
                                End If
                                myReaderLuogoNasc.Close()

                                par.cmd.CommandText = "INSERT INTO INTESTATARI_CONTRATTI_VSA (ID_DOMANDA,COGNOME,NOME,SESSO,COD_FISCALE,ID_LUOGO_NAS_DNTE,DATA_NASCITA_DNTE) VALUES " _
                                    & "(" & new_id_dom.Value & ",'" & par.PulisciStrSql(par.IfNull(myReaderIntest("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderIntest("NOME"), "")) & "'," _
                                    & "'" & par.IfNull(myReaderIntest("SESSO"), "") & "','" & par.IfNull(myReaderIntest("COD_FISCALE"), "") & "','" & idLUOGOnasc & "','" & par.IfNull(myReaderIntest("DATA_NASCITA"), "") & "')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReaderIntest.Close()

                        End If
                        myReaderInd.Close()

                    End If
                    myReaderUI.Close()

                End If
                myReaderBandoVSA.Close()
            End If
            myReaderContr.Close()
        End If
        myReader.Close()

        'Aggiungo eventuali ospiti

        par.cmd.CommandText = "SELECT * FROM siscom_mi.OSPITI WHERE nvl(DATA_FINE_OSPITE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' and ID_CONTRATTO=" & idcont.Value & ""
        Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader12.Read
            par.cmd.CommandText = "SELECT COMP_NUCLEO_OSPITI_VSA.* FROM COMP_NUCLEO_OSPITI_VSA,comp_nucleo_vsa,dichiarazioni_vsa,domande_bando_vsa " _
                & " WHERE " _
                & " comp_nucleo_vsa.id_dichiarazione=dichiarazioni_vsa.id " _
                & " and domande_bando_vsa.id_dichiarazione=dichiarazioni_vsa.id " _
                & " and domande_bando_vsa.id=COMP_NUCLEO_OSPITI_VSA.id_domanda " _
                & " and COMP_NUCLEO_OSPITI_VSA.COD_FISCALE='" & par.IfNull(myReader12("COD_FISCALE"), "") & "' order by dichiarazioni_vsa.id desc "
            Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderR.Read Then
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_OSPITI_VSA (ID, ID_DOMANDA, DATA_AGG, DATA_INIZIO_OSPITE, DATA_FINE_OSPITE, COGNOME, NOME, COD_FISCALE, DATA_NASC, ID_TIPO_IND_RES_DNTE, IND_RES_DNTE, CIVICO_RES_DNTE, COMUNE_RES_DNTE, CAP_RES_DNTE, CARTA_I, CARTA_I_DATA, CARTA_I_RILASCIATA, PERMESSO_SOGG_N, PERMESSO_SOGG_DATA, FL_REFERENTE, GRADO_PARENTELA, ID_TIPO_RUOLO) " _
                                       & "VALUES " _
                                       & "(SEQ_COMP_NUCLEO_OSPITI_VSA.NEXTVAL, " & new_id_dom.Value & ", " & par.insDbValue(par.IfNull(myReaderR("DATA_AGG"), ""), True) & ", " & par.insDbValue(myReaderR("DATA_INIZIO_OSPITE"), True) & ", " _
                                       & "" & par.insDbValue(par.IfNull(myReaderR("DATA_FINE_OSPITE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COGNOME"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("NOME"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COD_FISCALE"), ""), True) _
                                       & ", " & par.insDbValue(par.IfNull(myReaderR("DATA_NASC"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("ID_TIPO_IND_RES_DNTE"), ""), False) & ", " & par.insDbValue(par.IfNull(myReaderR("IND_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CIVICO_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COMUNE_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CAP_RES_DNTE"), ""), True) & ", " _
                                       & "" & par.insDbValue(par.IfNull(myReaderR("CARTA_I"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CARTA_I_DATA"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CARTA_I_RILASCIATA"), ""), True) & "," & par.insDbValue(par.IfNull(myReaderR("PERMESSO_SOGG_N"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("PERMESSO_SOGG_DATA"), ""), True) & ", " _
                                       & "NULL," & par.IfNull(myReaderR("GRADO_PARENTELA"), "") & "," & par.IfNull(myReaderR("ID_TIPO_RUOLO"), "1") & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderR.Close()
        End While
        myReader12.Close()



        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
        & "VALUES (" & new_id_dom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
        & "','F190','','I')"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(intestatario.Value) & "%')"
        Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderI.Read Then

            par.cmd.CommandText = "SELECT * FROM COMP_NAS_RES_VSA WHERE ID_COMPONENTE=" & myReaderI("ID")
            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderComp.Read Then

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE PROGR= 0 AND ID_DICHIARAZIONE = " & new_idDichia.Value
                Dim myReaderIDComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIDComp.Read Then
                    par.cmd.CommandText = "INSERT INTO COMP_NAS_RES_VSA (ID_COMPONENTE,ID_LUOGO_NAS_DNTE,ID_LUOGO_RES_DNTE,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE," _
                        & "CIVICO_RES_DNTE,TELEFONO_DNTE,CAP_RES) VALUES (" & myReaderIDComp("ID") & ",'" & par.IfNull(myReaderComp("ID_LUOGO_NAS_DNTE"), "") & "'," _
                        & "'" & par.IfNull(myReaderComp("ID_LUOGO_RES_DNTE"), "") & "','" & par.IfNull(myReaderComp("ID_TIPO_IND_RES_DNTE"), "") & "','" _
                        & par.PulisciStrSql(par.IfNull(myReaderComp("IND_RES_DNTE"), "")) & "','" & par.IfNull(myReaderComp("CIVICO_RES_DNTE"), "") & "'," _
                        & "'" & par.IfNull(myReaderComp("TELEFONO_DNTE"), "") & "','" & par.IfNull(myReaderComp("CAP_RES"), "") & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderIDComp.Close()

            End If
            myReaderComp.Close()

        End If
        myReaderI.Close()


    End Sub


    Protected Sub ImportDaAU()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim i As Integer = 1
        Dim strScript As String = ""

        connData.apri(True)
        par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            lValoreCorrente = par.IfNull(myReader(0), 0) + 1
        End If
        myReader.Close()
        par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
        par.cmd.ExecuteNonQuery()
        valorePG = Format(lValoreCorrente, "0000000000")

        codFisc.Value = ListaInt.SelectedValue

        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(ListaInt.SelectedItem.Text) & "%')"
        myReader = par.cmd.ExecuteReader()
        If myReader.HasRows Then
            While myReader.Read
                If par.IfNull(myReader("COD_FISCALE"), "") = codFisc.Value Then
                    id_intest.Value = myReader("ID") 'ottengo l'id dell'intestatario che intende presentare la domanda
                End If
            End While
            myReader.Close()
        Else
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " AND PROGR=0"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                id_intest.Value = myReader0("ID")
            End If
            myReader0.Close()
        End If


        par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
        Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderNew.Read() Then
            new_id_dom.Value = myReaderNew(0)
        End If
        myReaderNew.Close()

        Dim idLuogoNascita As Integer = 0
        par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE = UTENZA_DICHIARAZIONI.ID AND UTENZA_DICHIARAZIONI.ID = " & lIdDichiarazione.Value & " AND UTENZA_COMP_NUCLEO.COD_FISCALE='" & codFisc.Value & "' AND UTENZA_COMP_NUCLEO.PROGR =0"
        Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderCodF.Read = False Then
            If codFisc.Value <> "" Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & codFisc.Value.Substring(11, 4) & "'"
                Dim myReaderCom As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderCom.Read Then
                    idLuogoNascita = par.IfNull(myReaderCom("ID"), "")
                End If
                myReaderCom.Close()
            End If
        End If
        myReaderCodF.Close()



        par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID = " & lIdDichiarazione.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            If idLuogoNascita = 0 Then
                idLuogoNascita = par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
            End If

            par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.NEXTVAL FROM DUAL"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                new_idDichia.Value = myReader1(0)
            End If
            myReader1.Close()

            Dim annoReddito As Integer = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

            par.cmd.CommandText = "INSERT INTO Dichiarazioni_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
                & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,N_COMP_NUCLEO,N_INV_100_CON,N_INV_100_SENZA,N_INV_100_66,ID_TIPO_CAT_AB,ANNO_SIT_ECONOMICA,LUOGO_INT_ERP," _
                & " DATA_INT_ERP,LUOGO_S,DATA_S,PROGR_DNTE,FL_GIA_TITOLARE,CAP_RES_DNTE,FL_UBICAZIONE,POSSESSO_UI,FL_APPLICA_36,MINORI_CARICO,ISEE,ISE_ERP,ISR_ERP,ISP_ERP,PSE,VSE,MOD_PRESENTAZIONE,DATA_INIZIO_VAL,DATA_FINE_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO) " _
                & " VALUES (" & new_idDichia.Value & "," & Session.Item("ID_CAF") & "," & id_bando.Value & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
                & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLuogoNascita & ",'','" _
                & "',null,'','" _
                & "'," & par.IfNull(myReader("N_COMP_NUCLEO"), "''") & "," & par.IfNull(myReader("N_INV_100_CON"), "''") & "," _
                & par.IfNull(myReader("N_INV_100_SENZA"), "''") & "," & par.IfNull(myReader("N_INV_100_66"), "''") & "," & par.IfNull(myReader("ID_TIPO_CAT_AB"), "''") & "," _
                & annoReddito & ",'" & par.IfNull(myReader("LUOGO_INT_ERP"), "") & "','" & par.IfNull(myReader("DATA_INT_ERP"), "") & "','" _
                & par.IfNull(myReader("LUOGO_S"), "") & "','" & par.IfNull(myReader("DATA_S"), "") & "'," & par.IfNull(myReader("PROGR_DNTE"), "''") & ",'" _
                & par.IfNull(myReader("FL_GIA_TITOLARE"), "") & "','','" & par.IfNull(myReader("FL_UBICAZIONE"), "") & "'," _
                & par.IfNull(myReader("POSSESSO_UI"), "''") & ",'" & par.IfNull(myReader("FL_APPLICA_36"), "") & "','" & par.IfNull(myReader("MINORI_CARICO"), "") & "','" _
                & par.IfNull(myReader("ISEE"), "") & "'," & par.VirgoleInPunti(par.IfNull(myReader("ISE_ERP"), 0)) & "," & par.VirgoleInPunti(par.IfNull(myReader("ISR_ERP"), 0)) & "," _
                & par.VirgoleInPunti(par.IfNull(myReader("ISP_ERP"), 0)) & ",'" & par.IfNull(myReader("PSE"), "") & "','" & par.IfNull(myReader("VSE"), "") & "'," & RadComboModPres.SelectedValue & ",'" & par.AggiustaData(RadDateEvento.SelectedDate) & "','" & Mid(par.AggiustaData(RadDateEvento.SelectedDate), 1, 4) & "1231" & "','','')"
            par.cmd.ExecuteNonQuery()
        End If
        myReader.Close()

        Dim id_compon As Long = 0
        Dim new_id_compon As Long = 0

        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & lIdDichiarazione.Value
        myReader = par.cmd.ExecuteReader()
        While myReader.Read '-------------- CICLO SUI COMPONENTI DEL NUCLEO FAMILIARE --------------'
            id_compon = myReader("ID")

            par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                new_id_compon = myReader1(0)
            End If
            myReader1.Close()

            par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                & "VALUES (" & new_id_compon & "," & new_idDichia.Value & "," & par.IfNull(myReader("PROGR"), "") & ",'" & par.IfNull(myReader("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader("COGNOME"), "")) & "'," _
                & "'" & par.PulisciStrSql(par.IfNull(myReader("NOME"), "")) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "'," _
                & par.IfNull(myReader("PERC_INVAL"), "") & ",'" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
            par.cmd.ExecuteNonQuery()

            If id_compon = id_intest.Value Then
                id_intest.Value = new_id_compon
            End If

            Dim distanzaKm As Decimal = 0
            Dim idRedditoTot As Long = 0

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE = " & id_compon
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "'"
                Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCC.Read() Then
                    distanzaKm = par.IfNull(myReaderCC("DISTANZA_KM"), 0)
                End If
                myReaderCC.Close()

                par.cmd.CommandText = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA,DISTANZA_KM) " _
                    & "VALUES (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                    & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "'," _
                    & "'" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "','" & par.IfNull(myReader2("N_VANI"), "") & "'," _
                    & "'" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), 0) & "," _
                    & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), 0) & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & "," & distanzaKm & ")"
                par.cmd.ExecuteNonQuery()
            End While
            myReader2.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE = " & id_compon
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader3.Read
                par.cmd.CommandText = "INSERT INTO COMP_PATR_MOB_VSA (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO) " _
                    & "VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & ")"
                par.cmd.ExecuteNonQuery()
            End While
            myReader3.Close()

            Dim idReddComp As Long = 0
            Dim reddAltro As Decimal = 0
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE = " & id_compon
            Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader4.Read
                par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                    & "VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & new_id_compon & "," & par.VirgoleInPunti(par.IfNull(myReader4("REDDITO_IRPEF"), 0)) & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SEQ_COMP_REDDITO_VSA.currval from dual"
                Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCV.Read Then
                    idReddComp = myReaderCV(0)
                End If
                myReaderCV.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader5.Read
                    reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                    par.cmd.ExecuteNonQuery()
                End While
                myReader5.Close()
            End While
            myReader4.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE = " & id_compon
            Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader6.Read
                par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                & "VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                par.cmd.ExecuteNonQuery()
            End While
            myReader6.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE = " & id_compon
            Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader7.Read
                par.cmd.CommandText = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                & "VALUES (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                par.cmd.ExecuteNonQuery()
            End While
            myReader7.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID_COMPONENTE = " & id_compon
            Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader8.Read
                par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID,ID_DOMANDA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & new_idDichia.Value & "," & new_id_compon & ",'" _
                & par.IfNull(myReader8("CONDIZIONE"), "0") & "','" & par.IfNull(myReader8("PROFESSIONE"), "0") & "','" _
                & par.IfNull(myReader8("DIPENDENTE"), "0") & "','" & par.IfNull(myReader8("PENSIONE"), "0") & "','" _
                & par.IfNull(myReader8("AUTONOMO"), "0") & "','" & par.IfNull(myReader8("NON_IMPONIBILI"), "0") & "','" _
                & par.IfNull(myReader8("DOM_AG_FAB"), "0") & "','" & par.IfNull(myReader8("OCCASIONALI"), "0") & "','" & par.IfNull(myReader8("ONERI"), "0") & "','" & par.IfNull(myReader8("PENS_ESENTE"), "0") & "','" & par.IfNull(myReader8("NO_ISEE"), "0") & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SEQ_UTENZA_REDDITI.currval from dual"
                Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCV.Read Then
                    idRedditoTot = myReaderCV(0)
                End If
                myReaderCV.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_AUTONOMO_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddA.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (SELECT seq_VSA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_AUTONOMO_IMPORTI WHERE ID=" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddA.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddD.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (SELECT seq_VSA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID=" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddD.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddN.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (SELECT seq_VSA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID=" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddN.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddP.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (SELECT seq_VSA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddP.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderReddP2.Read
                    par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (SELECT seq_VSA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderReddP2.Close()

            End While
            myReader8.Close()

        End While
        myReader.Close()

        par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = 0 WHERE ID =" & id_intest.Value
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID <> " & id_intest.Value & " AND ID_DICHIARAZIONE = " & new_idDichia.Value & " ORDER BY PROGR ASC FOR UPDATE NOWAIT"
        myReader = par.cmd.ExecuteReader
        While myReader.Read
            par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = " & i & " WHERE ID =" & myReader("ID")
            par.cmd.ExecuteNonQuery()
            i = i + 1
        End While
        myReader.Close()

        ImportDaAU_2()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Apri", "CancelEdit();CenterPage2('Istanza.aspx?ONE=1&IDD=" & new_idDichia.Value & "&IDM=" & idMotivoIstanza.Value & "', 'Istanza' + " & Format(Now, "yyyyMMddmmhhss") & ", 1300, 750);", True)

        connData.chiudi(True)
    End Sub

    Protected Sub ImportDaAU_2()

        Dim iDluogo As Long
        Dim codIndir As Integer
        Dim sup_netta As Decimal
        Dim ascens As Integer
        Dim lValoreCorrenteDom As Long
        Dim valorePGdom As String
        Dim tipoBando As Long
        Dim cod_UI As String = ""

        par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            lValoreCorrenteDom = par.IfNull(myReader(0), 0) + 1
        End If
        myReader.Close()
        par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrenteDom & ")"
        par.cmd.ExecuteNonQuery()
        valorePGdom = Format(lValoreCorrenteDom, "0000000000")

        par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID=" & id_bando.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            tipoBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT DISTINCT(UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.id_unita_principale is null " _
            & " and  UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND ID_CONTRATTO=" & idcont.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            cod_UI = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "0")
        End If
        myReader.Close()

        Dim infoDocumento As Boolean = False
        Dim numDoc As String = ""
        Dim dataDoc As String = ""
        Dim rilascioDoc As String = ""
        Dim docSoggiorno As String = ""

        par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE = UTENZA_DICHIARAZIONI.ID AND UTENZA_DICHIARAZIONI.ID = " & lIdDichiarazione.Value & " " _
            & " AND UTENZA_COMP_NUCLEO.COD_FISCALE='" & codFisc.Value & "' AND UTENZA_COMP_NUCLEO.PROGR =0"
        Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderCodF.Read = False Then
            par.cmd.CommandText = "SELECT SISCOM_MI.ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
            & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.id =" & idcont.Value & " AND (ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
            Dim myReaderCI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCI.Read Then
                infoDocumento = True
                numDoc = par.IfNull(myReaderCI("NUM_DOC"), "")
                dataDoc = par.IfNull(myReaderCI("DATA_DOC"), "")
                rilascioDoc = par.IfNull(myReaderCI("RILASCIO_DOC"), "")
                docSoggiorno = par.IfNull(myReaderCI("DOC_SOGGIORNO"), "")
            End If
            myReaderCI.Close()
        End If
        myReaderCodF.Close()


        par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID = " & lIdDichiarazione.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then

            If infoDocumento = False Then
                numDoc = par.IfNull(myReader("CARTA_I"), "")
                dataDoc = par.IfNull(myReader("CARTA_I_DATA"), "")
                rilascioDoc = par.IfNull(myReader("CARTA_I_RILASCIATA"), "")
                docSoggiorno = par.IfNull(myReader("PERMESSO_SOGG_N"), "")
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE id =" & idcont.Value
            Dim myReaderContr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderContr.Read Then
                CodContratto.Value = myReaderContr("cod_contratto")
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & myReader("LUOGO").ToString.ToUpper & "'"
                Dim myReaderIDluogo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIDluogo.Read Then
                    iDluogo = myReaderIDluogo("ID")
                End If
                myReaderIDluogo.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE DESCRIZIONE='" & myReaderContr("TIPO_COR") & "'"
                Dim myReaderTipoIndir As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderTipoIndir.Read Then
                    codIndir = myReaderTipoIndir("COD")
                End If
                myReaderTipoIndir.Close()

                Dim dataPr As String = par.AggiustaData(RadDatePresentaz.SelectedDate) 'Format(Now, "yyyyMMdd")
                Dim dataEv As String = par.AggiustaData(RadDateEvento.SelectedDate)

                Dim idLUOGOnasc As Long = 0
                par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                    & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                    & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,REDDITO_ISEE,ISR_ERP,ISP_ERP,ISE_ERP,CAP_REC_DNTE,MINORI_CARICO,PSE,VSE,ID_MOTIVO_DOMANDA," _
                    & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                    & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                    & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                    & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                    & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                    & "FL_ASS_ESTERNA,CARTA_I,CARTA_I_DATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,PERMESSO_SOGG_SCADE,PERMESSO_SOGG_RINNOVO,FL_FATTA_AU,FL_FATTA_ERP," _
                    & "FL_FAI_ERP,CARTA_SOGG_N,CARTA_SOGG_DATA,CARTA_I_RILASCIATA,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                    & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT," _
                    & " TIPO_DOCUMENTO,COD_CONTRATTO_SCAMBIO,ID_INTEST_NEW_RU,FL_NUOVA_NORMATIVA,ID_CONTRATTO,ID_STATO_ISTANZA) " _
                        & "VALUES (" & new_id_dom.Value & "," & id_bando.Value & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia.Value & "," _
                        & myReader("PROGR_DNTE") & ",'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReaderContr("PRESSO_COR"), "")) & "'," _
                        & "'','','',''," _
                        & "''," & CDec(par.VirgoleInPunti(par.IfNull(myReader("ISEE"), "0"))) & "," & par.IfNull(par.VirgoleInPunti(myReader("ISR_ERP")), "''") & "," _
                        & "" & par.IfNull(par.VirgoleInPunti(myReader("ISP_ERP")), "''") & ", " & par.IfNull(par.VirgoleInPunti(myReader("ISE_ERP")), "''") & ",'','" _
                        & " " & par.IfNull(myReader("MINORI_CARICO"), "") & "','" & par.IfNull(myReader("PSE"), "") & "','" & par.IfNull(myReader("VSE"), "") & "'," & idMotivoIstanza.Value & "," _
                        & "'" & dataPr & "','0','1','0','" & par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "") & "',0,'" & par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "") & "'," _
                        & "0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                        & par.IfNull(myReaderContr("DATA_STIPULA"), "") & "','" & cod_UI & "','0',-1,'" & par.IfNull(myReaderContr("DATA_DECORRENZA"), "") & "'," _
                        & "'1','" & numDoc & "','" & dataDoc & "','" & docSoggiorno & "','" _
                        & par.IfNull(myReader("PERMESSO_SOGG_DATA"), "") & "','" & par.IfNull(myReader("PERMESSO_SOGG_SCADE"), "") & "','" & par.IfNull(myReader("PERMESSO_SOGG_RINNOVO"), "") & "'," _
                        & "'1','1','0','" & par.IfNull(myReader("CARTA_SOGG_N"), "") & "'," & "'" & par.IfNull(myReader("CARTA_SOGG_DATA"), "") & "','" & par.PulisciStrSql(rilascioDoc) & "'," _
                        & "'1','1','1','1','1','1','1','0','1','1','1','0','0','0','','" & dataEv & "'," & tipoDomImportata.Value & "," & lIdDichiarazione.Value & "," & par.IfNull(myReader("TIPO_DOCUMENTO"), 0) & ",'',null,1," & idcont.Value & ",1)"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO COMP_NAS_RES_VSA (ID_COMPONENTE,ID_LUOGO_NAS_DNTE,ID_LUOGO_RES_DNTE,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE," _
                    & "CIVICO_RES_DNTE,TELEFONO_DNTE,CAP_RES) VALUES (" & id_intest.Value & ",'" & par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "") & "'," _
                    & "'" & par.IfNull(myReader("ID_LUOGO_RES_DNTE"), "") & "','" & par.IfNull(myReader("ID_TIPO_IND_RES_DNTE"), "") & "','" _
                    & par.PulisciStrSql(par.IfNull(myReader("IND_RES_DNTE"), "")) & "','" & par.IfNull(myReader("CIVICO_RES_DNTE"), "") & "'," _
                    & "'" & par.IfNull(myReader("TELEFONO_DNTE"), "") & "','" & par.IfNull(myReader("CAP_RES_DNTE"), "") & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SCALE_EDIFICI WHERE COD_UNITA_IMMOBILIARE='" & cod_UI & "' AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA"
                Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderUI.Read() Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & par.IfNull(myReaderUI("ID"), "") & " AND COD_TIPOLOGIA='SUP_NETTA'"
                    Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderMQ.Read Then
                        sup_netta = par.IfNull(myReaderMQ("VALORE"), "")
                    End If
                    myReaderMQ.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & par.IfNull(myReaderUI("ID_INDIRIZZO"), "")
                    Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderInd.Read Then

                        'query per verificare la presenza o meno dell'ascensore
                        par.cmd.CommandText = "SELECT IMPIANTI.* FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.SCALE_EDIFICI WHERE IMPIANTI.ID = IMPIANTI_SCALE.ID_IMPIANTO " _
                            & "AND SCALE_EDIFICI.ID = IMPIANTI_SCALE.ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = '" & myReaderUI("ID_EDIFICIO") & "' AND SCALE_EDIFICI.DESCRIZIONE = '" & myReaderUI("DESCRIZIONE") & "' AND IMPIANTI.COD_TIPOLOGIA = 'SO'"
                        Dim myReaderAsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderAsc.Read Then
                            ascens = 1
                        Else
                            ascens = 0
                        End If
                        myReaderAsc.Close()

                        par.cmd.CommandText = "INSERT INTO DOMANDE_VSA_ALLOGGIO (ID_DOMANDA,COMUNE,CAP,INDIRIZZO,CIVICO,ID_TIPO_GESTORE," _
                            & "SCALA,PIANO,INTERNO,NUM_CONTRATTO,DEC_CONTRATTO,ASS_TEMPORANEA,ID_TIPO_CONTRATTO," _
                            & "COD_UNITA_IMMOBILIARE,SUP_NETTA,ASCENSORE) VALUES (" & new_id_dom.Value & ",'" & par.IfNull(myReaderInd("LOCALITA"), "") & "','" & par.IfNull(myReaderInd("CAP"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "','" & par.IfNull(myReaderInd("CIVICO"), "") & "','9','" & par.IfNull(myReader("SCALA"), "") & "'," _
                            & "'" & par.IfNull(myReaderUI("COD_TIPO_LIVELLO_PIANO"), "") & "','" & par.IfNull(myReaderUI("INTERNO"), "") & "','" & par.IfNull(myReader("RAPPORTO"), "") & "'," _
                            & "'" & par.IfNull(myReaderContr("DATA_DECORRENZA"), "") & "','0','0','" & cod_UI & "','" & sup_netta & "','" & ascens & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                            & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & myReaderContr("ID") & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                        Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderIntest.Read Then

                            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD ='" & myReaderIntest("COD_COMUNE_NASCITA") & "'"
                            Dim myReaderLuogoNasc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderLuogoNasc.Read Then
                                idLUOGOnasc = myReaderLuogoNasc("ID")
                            End If
                            myReaderLuogoNasc.Close()

                            par.cmd.CommandText = "INSERT INTO INTESTATARI_CONTRATTI_VSA (ID_DOMANDA,COGNOME,NOME,SESSO,COD_FISCALE,ID_LUOGO_NAS_DNTE,DATA_NASCITA_DNTE) VALUES " _
                                & "(" & new_id_dom.Value & ",'" & par.PulisciStrSql(par.IfNull(myReaderIntest("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderIntest("NOME"), "")) & "'," _
                                & "'" & par.IfNull(myReaderIntest("SESSO"), "") & "','" & par.IfNull(myReaderIntest("COD_FISCALE"), "") & "','" & idLUOGOnasc & "','" & par.IfNull(myReaderIntest("DATA_NASCITA"), "") & "')"
                            par.cmd.ExecuteNonQuery()

                        End If
                        myReaderIntest.Close()

                    End If
                    myReaderInd.Close()

                End If
                myReaderUI.Close()

            End If
            myReaderContr.Close()

        End If
        myReader.Close()

        'Aggiungo eventuali ospiti

        par.cmd.CommandText = "SELECT * FROM siscom_mi.OSPITI WHERE nvl(DATA_FINE_OSPITE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' and ID_CONTRATTO=" & idcont.Value & ""
        Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader12.Read
            par.cmd.CommandText = "SELECT COMP_NUCLEO_OSPITI_VSA.* FROM COMP_NUCLEO_OSPITI_VSA,comp_nucleo_vsa,dichiarazioni_vsa,domande_bando_vsa " _
                & " WHERE " _
                & " comp_nucleo_vsa.id_dichiarazione=dichiarazioni_vsa.id " _
                & " and domande_bando_vsa.id_dichiarazione=dichiarazioni_vsa.id " _
                & " and domande_bando_vsa.id=COMP_NUCLEO_OSPITI_VSA.id_domanda " _
                & " and COMP_NUCLEO_OSPITI_VSA.COD_FISCALE='" & par.IfNull(myReader12("COD_FISCALE"), "") & "' order by dichiarazioni_vsa.id desc "
            Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderR.Read Then
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_OSPITI_VSA (ID, ID_DOMANDA, DATA_AGG, DATA_INIZIO_OSPITE, DATA_FINE_OSPITE, COGNOME, NOME, COD_FISCALE, DATA_NASC, ID_TIPO_IND_RES_DNTE, IND_RES_DNTE, CIVICO_RES_DNTE, COMUNE_RES_DNTE, CAP_RES_DNTE, CARTA_I, CARTA_I_DATA, CARTA_I_RILASCIATA, PERMESSO_SOGG_N, PERMESSO_SOGG_DATA, FL_REFERENTE, GRADO_PARENTELA, ID_TIPO_RUOLO) " _
                                       & "VALUES " _
                                       & "(SEQ_COMP_NUCLEO_OSPITI_VSA.NEXTVAL, " & new_id_dom.Value & ", " & par.insDbValue(par.IfNull(myReaderR("DATA_AGG"), ""), True) & ", " & par.insDbValue(myReaderR("DATA_INIZIO_OSPITE"), True) & ", " _
                                       & "" & par.insDbValue(par.IfNull(myReaderR("DATA_FINE_OSPITE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COGNOME"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("NOME"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COD_FISCALE"), ""), True) _
                                       & ", " & par.insDbValue(par.IfNull(myReaderR("DATA_NASC"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("ID_TIPO_IND_RES_DNTE"), ""), False) & ", " & par.insDbValue(par.IfNull(myReaderR("IND_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CIVICO_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("COMUNE_RES_DNTE"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CAP_RES_DNTE"), ""), True) & ", " _
                                       & "" & par.insDbValue(par.IfNull(myReaderR("CARTA_I"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CARTA_I_DATA"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("CARTA_I_RILASCIATA"), ""), True) & "," & par.insDbValue(par.IfNull(myReaderR("PERMESSO_SOGG_N"), ""), True) & ", " & par.insDbValue(par.IfNull(myReaderR("PERMESSO_SOGG_DATA"), ""), True) & ", " _
                                       & "NULL," & par.IfNull(myReaderR("GRADO_PARENTELA"), "") & "," & par.IfNull(myReaderR("ID_TIPO_RUOLO"), "1") & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderR.Close()
        End While
        myReader12.Close()

        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
        & "VALUES (" & new_id_dom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
        & "','F190','','I')"
        par.cmd.ExecuteNonQuery()

    End Sub

    Protected Sub btnAnagrafeSipo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnagrafeSipo.Click
        Try
            Dim strErrore As String = "<strong><font size='2' face='Arial' color='Red'> (Errore)</font></strong>"
            Dim strok As String = "<strong><font size='2' face='Arial' color='Black'> (OK)</font></strong>"
            Dim strNT As String = "<strong><font size='2' face='Arial' color='Black'> (Non trovato)</font></strong>"
            Dim errore As Boolean = False
            Dim lblSIPO As String = ""

            Dim CodFiscSIPO As String = ""
            For Each lstComponente As ListItem In ListaInt.Items
                CodFiscSIPO = ConnessioneServizioSIPO(lstComponente.Value)
                If lstComponente.Value <> CodFiscSIPO Then
                    If CodFiscSIPO = "" Then
                        'errore = True
                        lstComponente.Text = lstComponente.Text & strNT
                    Else
                        If lista.Value = "" Then
                            lista.Value = lstComponente.Value
                        Else
                            lista.Value &= "," & lstComponente.Value
                        End If
                        btnAnagrafeSipo.Visible = False
                        btnAggiornaSipo.Visible = True
                        btnAggiornaSipo.Enabled = True

                        lstComponente.Text = lstComponente.Text & strErrore
                        errore = True
                    End If

                Else
                    btnAggiornaSipo.Visible = True
                    btnAggiornaSipo.Enabled = False
                    lstComponente.Text = lstComponente.Text & strok
                End If
            Next
            If errore = True Then
                btnProcedi.Visible = False
            Else
                btnAnagrafeSipo.Enabled = False
                btnProcedi.Visible = True
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Function ConnessioneServizioSIPO(ByVal CFdaCercare As String) As String

        connData.apri(False)

        Dim sipo_applicazione As String = ""
        Dim sipo_operatore As String = ""
        Dim sipo_pwoperatore As String = ""
        Dim sipo_token As String = ""

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=53"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            sipo_applicazione = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=54"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            sipo_operatore = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=55"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            sipo_pwoperatore = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=56"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            sipo_token = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()

        Dim obj As New Anagrafe11.XMLWSAnagrafe2009SoapClient
        Dim risultato As Anagrafe11.getRicercaIndividuiXMLResult
        Dim httpRequestProperty As New HttpRequestMessageProperty

        Dim risultatoF As Anagrafe11.getFamigliaByMatricolaXMLResult
        Dim RisultatoDoc As Anagrafe11.getCarteIdentitaXMLResult
        Dim RisultatoPU As Anagrafe11.getDatiPuntualiXMLResult
        Dim errore As Boolean = False
        Dim matricola As String = ""

        httpRequestProperty.Method = "POST"
        httpRequestProperty.Headers.Add("Authorization", "Bearer " & sipo_token)
        Using scope As New OperationContextScope(obj.InnerChannel)
            OperationContext.Current.OutgoingMessageProperties(HttpRequestMessageProperty.Name) = httpRequestProperty

            Try
                risultato = obj.getRicercaIndividuiXML(sipo_applicazione, sipo_operatore, sipo_pwoperatore, Session.Item("ID_OPERATORE"), "", "", "", "", "", "", 0, "", CFdaCercare, 0, "", "N", "T")
            Catch ex As CommunicationException
                errore = True
                hfCF.Value = ""
            End Try

            If errore = False Then
                matricola = risultato.Item(0).Matricola

                Try
                    risultatoF = obj.getFamigliaByMatricolaXML(sipo_applicazione, sipo_operatore, sipo_pwoperatore, Session.Item("ID_OPERATORE"), matricola)
                    For i = 0 To risultatoF.Count - 1
                        Try
                            RisultatoDoc = obj.getCarteIdentitaXML(sipo_applicazione, sipo_operatore, sipo_pwoperatore, Session.Item("ID_OPERATORE"), matricola)
                            hfNumDocId.Value = RisultatoDoc.Item(0).Numero
                            hfDataDocId.Value = par.FormattaData(Trim(RisultatoDoc.Item(0).DataRilascio))
                            hfComuneRilascio.Value = RisultatoDoc.Item(0).Comune
                        Catch ex As Exception
                            hfNumDocId.Value = "*"
                            hfDataDocId.Value = "*"
                            hfComuneRilascio.Value = "*"
                        End Try
                        Try
                            RisultatoPU = obj.getDatiPuntualiXML(sipo_applicazione, sipo_operatore, sipo_pwoperatore, Session.Item("ID_OPERATORE"), matricola)
                            hfCF.Value = RisultatoPU.Item(0).CodiceFiscale

                            hfCognome.Value = RisultatoPU.Item(0).Cognome
                            hfNome.Value = RisultatoPU.Item(0).Nome
                            hfSesso.Value = RisultatoPU.Item(0).Sesso
                            hfDataNascita.Value = par.FormattaData(RisultatoPU.Item(0).DataNascita)
                            hfComuneNascita.Value = RisultatoPU.Item(0).ComuneNascita
                            hfProvinciaNascita.Value = RisultatoPU.Item(0).ProvinciaNascita
                            hfNazioneNascita.Value = RisultatoPU.Item(0).NazioneNascita
                            hfIndirizzo.Value = RisultatoPU.Item(0).Indirizzo
                            hfCittadinanza.Value = RisultatoPU.Item(0).Cittadinanza

                        Catch ex As Exception
                            errore = True
                            If par.OracleConn.State = Data.ConnectionState.Open Then
                                connData.chiudi(False)
                            End If
                            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore nell\'interrogazione al servizio!", 450, 150, "Attenzione", Nothing, Nothing)
                            Exit For
                        End Try
                    Next
                Catch ex As Exception
                    'errore = True
                    'If par.OracleConn.State = Data.ConnectionState.Open Then
                    '    connData.chiudi(False)
                    'End If
                    'par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore nell\'interrogazione al servizio!", 450, 150, "Attenzione", Nothing, Nothing)
                    'Exit Try
                End Try
            Else
                hfCF.Value = ""
            End If
        End Using

        connData.chiudi(False)

        Return hfCF.Value

    End Function

    Private Function idLUOGOnasc() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub btnAggiornaSipo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiornaSipo.Click
        Try
            connData.apri(True)
            Dim arraylista As String() = lista.Value.Split(",")
            If lista.Value <> "" Then
                'End If
                'If listCFerrati.Count > 0 Then
                For Each CFerrato As String In arraylista
                    par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA" _
                           & " SET   " _
                           & " COGNOME               = " & par.insDbValue(hfCognome.Value, True) & "," _
                           & " NOME                  = " & par.insDbValue(hfNome.Value, True) & "," _
                           & " COD_FISCALE           = " & par.insDbValue(hfCF.Value, True) & "," _
                           & " CITTADINANZA          = " & par.insDbValue(hfCittadinanza.Value, True) & "," _
                           & " DATA_NASCITA          = " & par.insDbValue(hfDataNascita.Value, True, True) & "," _
                           & " COD_COMUNE_NASCITA    = (select cod from comuni_nazioni where nome=" & par.insDbValue(Trim(hfComuneNascita.Value), True) & ")," _
                           & " SESSO                 = " & par.insDbValue(hfSesso.Value, True) & "," _
                           & " INDIRIZZO_RESIDENZA   = " & par.insDbValue(Trim(hfIndirizzo.Value), True) & "," _
                           & " NUM_DOC               = " & par.insDbValue(hfNumDocId.Value, True) & "," _
                           & " DATA_DOC              = " & par.insDbValue(hfDataDocId.Value, True, True) & "," _
                           & " RILASCIO_DOC          = " & par.insDbValue(Trim(hfComuneRilascio.Value), True) & "" _
                    & " WHERE  COD_FISCALE           = '" & CFerrato & "'"
                    par.cmd.ExecuteNonQuery()
                Next
                par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)
                btnAggiornaSipo.Visible = False
                btnAnagrafeSipo.Visible = True
                connData.chiudi(True)
                ListaInt.Items.Clear()
                PrendiUltima()
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
