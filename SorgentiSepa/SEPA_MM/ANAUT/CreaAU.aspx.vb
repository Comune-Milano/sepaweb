
Partial Class ANAUT_CreaAU
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim strScript As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        Response.Expires = 0
        If IsPostBack = False Then
            anomaliaReddAU = False
            anomaliaReddVSA = False
            IDA.Value = Request.QueryString("IDA")
            idc.Value = Request.QueryString("IDC")
            IDCONVOCAZIONE.Value = Request.QueryString("IC")
            AUS.Value = Request.QueryString("AUS")
            t.Value = Request.QueryString("T")
            If Verifica(idc.Value) = True Then
                procedi.Value = "1"
                PrendiUltima()

                If procedi.Value = "1" Then
                    'If ListaInt.Items.Count = 1 Then
                    '    ListaInt.Items(0).Selected = True
                    '    CreaAU(idc.Value, t.Value)
                    'End If
                Else
                    ImageButton1.Visible = False
                End If
            Else
                Response.Redirect("CreaAU1.aspx?ID=" & scheda.Value)
            End If
        End If
    End Sub

    Private Function PrendiUltima()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim id_dichia As Long = 0
            Dim id_dichia1 As Long = 0
            Dim data_Fine As String = ""
            Dim data_Fine1 As String = ""

            Dim MESSAGGIO As String = ""
            Dim MESSAGGIO1 As String = ""

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=" & idc.Value & " AND DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND (DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 or DOMANDE_BANDO_VSA.ID_STATO_ISTANZA=5)  AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM=RAPPORTI_UTENZA.COD_CONTRATTO ORDER BY DICHIARAZIONI_VSA.DATA_FINE_VAL DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                id_dichia = myReader1("ID_DICHIARAZIONE")
                data_Fine = par.IfNull(myReader1("DATA_FINE_VAL"), Format(Now, "yyyyMMdd"))
                MESSAGGIO = "<br/>La situazione anagrafica e reddituale più recente corrisponde alla domanda di <b>""" & myReader1("DESCRIZIONE") & """</b> presentata in data " & par.FormattaData(myReader1("DATA_presentazione")) & ".<br/>Premendo il pulsante 'PROCEDI' sarà importata la situazione REDDITUALE E ANAGRAFICA nella nuova scheda AU"
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM siscom_mi.rapporti_utenza,UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE rapporti_utenza.id=" & idc.Value & " and NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
            & "AND RAPPORTO=rapporti_utenza.cod_contratto ORDER BY UTENZA_DICHIARAZIONI.DATA_FINE_VAL DESC"

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                id_dichia1 = myReader2("ID")
                data_Fine1 = par.IfNull(myReader2("DATA_FINE_VAL"), Format(Now, "yyyyMMdd"))
                MESSAGGIO1 = "<br/>La situazione anagrafica e reddituale più recente corrisponde a: <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br/>Premendo il pulsante 'PROCEDI' sarà importata la situazione REDDITUALE E ANAGRAFICA nella nuova scheda AU"
            End If
            myReader2.Close()

            If data_Fine = "" And data_Fine1 = "" Then
                Intestatari(idc.Value, t.Value)
                tipo.Value = 0
            Else
                If data_Fine >= data_Fine1 Then
                    Label4.Text = MESSAGGIO
                    iddich.Value = id_dichia
                    Intestatari1(id_dichia)
                    tipo.Value = 1
                Else
                    iddich.Value = id_dichia1
                    Label4.Text = MESSAGGIO1
                    Intestatari2(id_dichia1)
                    tipo.Value = 2
                End If
            End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PrendiUltima = 0
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)

        End Try



    End Function

    Private Sub VerificaAnomalia(ByVal Iddich As Long)
        Try
            anomaliaReddAU = False
            anomaliaReddVSA = False

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT distinct(id_componente) FROM utenza_comp_reddito " _
            & " WHERE id_componente IN (SELECT ID FROM utenza_comp_nucleo WHERE id_dichiarazione IN (SELECT ID FROM utenza_dichiarazioni WHERE (note_web IS NULL OR note_web <> 'GENERATA_AUTOMATICAMENTE') AND NVL (fl_generaz_auto, 0) = 0 and id_bando<>1 and id=" & Iddich & ")) " _
            & " AND NVL(reddito_irpef, 0) > 0" _
            & " AND id_componente NOT IN (SELECT id_componente FROM utenza_redditi WHERE id_utenza IN (SELECT ID FROM utenza_dichiarazioni WHERE (note_web IS NULL OR note_web <> 'GENERATA_AUTOMATICAMENTE') AND NVL (fl_generaz_auto, 0) = 0 and id_bando<>1 and id=" & Iddich & "))"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                anomaliaReddAU = True
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DISTINCT id_componente FROM comp_reddito_vsa WHERE id_componente IN (" _
            & " SELECT ID FROM comp_nucleo_vsa WHERE id_dichiarazione IN ( SELECT ID" _
            & " FROM dichiarazioni_vsa WHERE dichiarazioni_vsa.id_stato <> 2" _
            & " AND data_pg >= '20120101' and id=" & Iddich & ")) AND NVL (reddito_irpef, 0) > 0" _
            & " AND id_componente NOT IN (SELECT id_componente" _
            & " FROM domande_redditi_vsa WHERE id_domanda IN (" _
            & " SELECT ID FROM dichiarazioni_vsa WHERE dichiarazioni_vsa.id_stato <> 2" _
            & " AND data_pg >= '20120101' and id=" & Iddich & "))"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                anomaliaReddVSA = True
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT id_COMPONENTE, COUNT (*) FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE IN (SELECT ID FROM utenza_dichiarazioni WHERE (note_web IS NULL OR note_web <> 'GENERATA_AUTOMATICAMENTE') AND NVL (fl_generaz_auto, 0) = 0 AND id_bando <> 1 and id=" & Iddich & ")) GROUP BY (id_COMPONENTE) HAVING COUNT (*) > 1"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                anomaliaReddAU = True
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT id_COMPONENTE, COUNT (*) FROM comp_reddito_vsa WHERE ID_COMPONENTE IN (SELECT ID FROM comp_nucleo_vsa WHERE ID_DICHIARAZIONE IN (SELECT ID FROM dichiarazioni_vsa WHERE dichiarazioni_vsa.id_stato <> 2 and data_pg >= '20120101' and id=" & Iddich & ")) GROUP BY (id_COMPONENTE) HAVING COUNT (*) > 1"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                anomaliaReddVSA = True
            End If
            myReader.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub VerificaAnomalia() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function Intestatari1(ByVal idc As Long)
        Try


            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.* FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & idc
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                If par.IfNull(myReader("data_nascita"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " NON HA LA DATA DI NASCITA!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If
                If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = False Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA UN CODICE FISCALE ERRATO!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("cognome"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO COGNOME VUOTO!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("NOME"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO NOME VUOTO!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = True Then
                    If par.RicavaEta(par.IfNull(myReader("data_nascita"), Format(Now, "yyyyMMdd"))) >= 18 Then
                        Dim lsiFrutto As New ListItem(Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", ""), myReader("ID"))
                        ListaInt.Items.Add(lsiFrutto)
                        lsiFrutto = Nothing
                    End If
                End If
            End While

            myReader.Close()



            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write(ex.Message)
        End Try

    End Function


    Private Function Intestatari2(ByVal idc As Long)
        Try


            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.* FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & idc
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                If par.IfNull(myReader("data_nascita"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " NON HA LA DATA DI NASCITA!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If
                If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = False Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA UN CODICE FISCALE ERRATO!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("cognome"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO COGNOME VUOTO!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("NOME"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", "") & " HA IL CAMPO NOME VUOTO!! La situazione dovrà essere corretta nella nuova scheda AU!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = True Then
                    If par.RicavaEta(par.IfNull(myReader("data_nascita"), Format(Now, "yyyyMMdd"))) >= 18 Then
                        Dim lsiFrutto As New ListItem(Replace(myReader("COGNOME"), "'", "") & " " & Replace(myReader("NOME"), "'", ""), myReader("ID"))
                        ListaInt.Items.Add(lsiFrutto)
                        lsiFrutto = Nothing
                    End If
                End If
            End While

            myReader.Close()



            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write(ex.Message)
        End Try

    End Function

    Private Function Verifica(ByVal idc As String) As Boolean
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim ID_AU As String = ""

            par.cmd.CommandText = "select MAX(ID) FROM UTENZA_BANDI"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read() Then
                ID_AU = myReader1(0)
            End If
            myReader1.Close()

            Verifica = True
            par.cmd.CommandText = "select utenza_dichiarazioni.* from utenza_dichiarazioni,siscom_mi.rapporti_utenza where rapporti_utenza.id=" & idc & " and utenza_dichiarazioni.rapporto=rapporti_utenza.cod_contratto and utenza_dichiarazioni.ID_BANDO=" & ID_AU
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Verifica = False
                scheda.Value = myReader("id")
            End If
            myReader.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Verifica = False
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function RicavaVia1(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim via As String


        pos = InStr(1, indirizzo, " ")
        If pos > 0 Then
            via = Mid(indirizzo, 1, pos - 1)
            Select Case via
                Case "C.SO"
                    RicavaVia1 = "CORSO"
                Case "PIAZZA", "PZ.", "P.ZZA", "PIAZZETTA"
                    RicavaVia1 = "PIAZZA"
                Case "PIAZZALE", "P.LE"
                    RicavaVia1 = "PIAZZALE"
                Case "P.T"
                    RicavaVia1 = "PORTA"
                Case "S.T.R.", "STRADA"
                    RicavaVia1 = "STRADA"
                Case "V.", "VIA"
                    RicavaVia1 = "VIA"
                Case "VIALE", "V.LE"
                    RicavaVia1 = "VIALE"
                Case "ALZAIA"
                    RicavaVia1 = "ALZAIA"
                Case Else
                    RicavaVia1 = "VIA"
            End Select

        Else
            RicavaVia1 = ""
        End If

    End Function


    Private Function Intestatari(ByVal idc As String, ByVal t As String)
        Try
            'par.OracleConn.Open()
            'par.SettaCommand(par)


            par.cmd.CommandText = "SELECT COGNOME||' '||NOME AS NOMINATIVO,ID,data_nascita,cod_fiscale,cognome,nome FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE (SOGGETTI_CONTRATTUALI.DATA_FINE IS NULL OR SOGGETTI_CONTRATTUALI.DATA_FINE>='" & Format(Now, "yyyyMMdd") & "') AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idc & " AND  ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY ANAGRAFICA.COGNOME ASC,ANAGRAFICA.NOME ASC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                If par.IfNull(myReader("data_nascita"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " NON HA LA DATA DI NASCITA!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If
                If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = False Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " HA UN CODICE FISCALE ERRATO!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("cognome"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " HA IL CAMPO COGNOME VUOTO!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("NOME"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " HA IL CAMPO NOME VUOTO!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    'procedi.Value = "0"
                    'ImageButton1.Visible = False
                End If

                If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = True Then
                    If par.RicavaEta(par.IfNull(myReader("data_nascita"), Format(Now, "yyyyMMdd"))) >= 18 Then
                        Dim lsiFrutto As New ListItem(myReader("nominativo"), myReader("ID"))
                        ListaInt.Items.Add(lsiFrutto)
                        lsiFrutto = Nothing
                    End If
                End If
            End While

            myReader.Close()



            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write(ex.Message)
        End Try

    End Function


    Private Function CreaAU(ByVal idc As String, ByVal t As String)
        Try

            fase.Value = "0"


            'creo l'anagrafe utenza
            Dim sAnnoIsee As String
            Dim lIndice_Bando As Long
            Dim lValoreCorrente As Long
            Dim lIdDichiarazioneAU As Long

            Dim SCALA As String = ""
            Dim PIANO As String = ""
            Dim COD_UNITA As String = ""
            Dim ALLOGGIO As String = ""
            Dim PIANOA As String = ""
            Dim FOGLIO As String = ""
            Dim MAPPALE As String = ""
            Dim SUBALTERNO As String = ""
            Dim COD_ALLOGGIO As String = ""


            Dim TIPO_ASS As String = "1"
            Dim FL_ATTIVITA_LAV As String = "1"
            Dim COD_CONTRATTO As String = ""
            Dim DATA_DECORRENZA As String = ""
            Dim ID_LUOGO_NAS_DNTE As Long = 0
            Dim TELEFONO_DNTE As String = ""
            Dim ID_LUOGO_RES_DNTE As Long = 0
            Dim IND_RES_DNTE As String = ""
            Dim CIVICO_RES_DNTE As String = ""
            Dim CAP_RES_DNTE As String = ""
            Dim N_COMP_NUCLEO As Integer = 0
            Dim N_INV_100_CON As Integer = 0
            Dim N_INV_100_SENZA As Integer = 0
            Dim N_INV_100_66 As Integer = 0
            Dim MINORI_CARICO As Integer = 0
            Dim CARTA_I As String = ""
            Dim CARTA_I_DATA As String = ""
            Dim CARTA_I_RILASCIATA As String = ""
            Dim tipo_documento As String = ""
            Dim ID_TIPO_IND_RES_DNTE As Integer = 6


            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM UTENZA_NUM_PROTOCOLLI"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = myReader(0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO UTENZA_NUM_PROTOCOLLI VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            fase.Value = "1"

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            fase.Value = "2"
            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                sAnnoIsee = myReader(0)
                lIndice_Bando = myReader(1)
            End If
            myReader.Close()

            fase.Value = "3"
            par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where id=" & idc
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                If UCase(par.IfNull(myReader("LUOGO_COR"), "MILANO")) = "MILANO" Then
                    ID_LUOGO_RES_DNTE = 4415
                Else
                    par.cmd.CommandText = "select ID from comuni_nazioni where UPPER(nome)='" & UCase(par.IfNull(myReader("LUOGO_COR"), "MILANO")) & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read() Then
                        ID_LUOGO_RES_DNTE = par.IfNull(myReader2(0), 4415)
                    End If
                    myReader2.Close()
                End If

                par.cmd.CommandText = "select * from T_TIPO_INDIRIZZO where UPPER(descrizione)='" & UCase(par.IfNull(myReader("TIPO_COR"), "VIA")) & "'"
                Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader12.Read Then
                    ID_TIPO_IND_RES_DNTE = par.IfNull(myReader12("cod"), 6)
                End If
                myReader12.Close()

                fase.Value = "4"
                If par.IfNull(myReader("cod_tipologia_contr_loc"), "ERP") = "ERP" Then
                    TIPO_ASS = "1"
                Else
                    TIPO_ASS = "0"
                End If
                COD_CONTRATTO = par.IfNull(myReader("cod_contratto"), "")
                DATA_DECORRENZA = par.IfNull(myReader("data_decorrenza"), "")
                IND_RES_DNTE = par.IfNull(myReader("VIA_COR"), "")
                CIVICO_RES_DNTE = par.IfNull(myReader("CIVICO_COR"), "")
                CAP_RES_DNTE = par.IfNull(myReader("CAP_COR"), "")

                par.cmd.CommandText = "Insert into UTENZA_DICHIARAZIONI  (ID, PG, DATA_PG, LUOGO, DATA, NOTE, ID_STATO,ANNO_SIT_ECONOMICA,ID_BANDO,DATA_INIZIO_VAL,DATA_FINE_VAL,FL_AUSI) values (seq_utenza_dichiarazioni.nextval, '" & Format(lValoreCorrente, "0000000000") & "','" & Format(Now, "yyyyMMdd") & "','Milano','" & Format(Now, "yyyyMMdd") & "','', 0," & sAnnoIsee & "," & lIndice_Bando & ",'" & sAnnoIsee + 2 & "0101','" & CInt(sAnnoIsee) + 3 & "1231'," & AUS.Value & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select seq_utenza_dichiarazioni.currval from dual"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    lIdDichiarazioneAU = myReader1(0)
                End If
                myReader1.Close()

                fase.Value = "5"

                N_COMP_NUCLEO = 0
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE (SOGGETTI_CONTRATTUALI.DATA_FINE IS NULL OR SOGGETTI_CONTRATTUALI.DATA_FINE>='" & Format(Now, "yyyyMMdd") & "') AND  ID_CONTRATTO=" & idc
                myReader1 = par.cmd.ExecuteReader()
                CARTA_I = ""
                CARTA_I_RILASCIATA = ""
                CARTA_I_DATA = ""
                tipo_documento = "0"

                Do While myReader1.Read
                    If par.IfNull(myReader1("data_fine"), "29991231") >= Format(Now, "yyyyMMdd") Then
                        N_COMP_NUCLEO = N_COMP_NUCLEO + 1

                        par.cmd.CommandText = "select * from siscom_mi.anagrafica where id=" & myReader1("id_anagrafica")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read() Then
                            If par.RicavaEta(par.IfNull(myReader2("data_nascita"), "")) < 18 Then
                                MINORI_CARICO = MINORI_CARICO + 1
                            End If

                            If ListaInt.SelectedItem.Value = myReader1("id_anagrafica") Then
                                If par.IfNull(myReader2("num_doc"), "") <> "" Then
                                    CARTA_I = myReader2("num_doc")
                                    CARTA_I_DATA = par.IfNull(myReader2("data_doc"), "")
                                    CARTA_I_RILASCIATA = par.IfNull(myReader2("rilascio_doc"), "")
                                    tipo_documento = par.IfNull(myReader2("tipo_doc"), "0")
                                    '0200006010300A00801
                                End If

                                par.cmd.CommandText = "select * from comuni_nazioni where upper(cod)='" & Mid(par.IfNull(myReader2("cod_fiscale"), ""), 12, 4) & "'"
                                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderX.Read Then
                                    ID_LUOGO_NAS_DNTE = par.IfNull(myReaderX("id"), 0)
                                End If
                                myReaderX.Close()
                            End If


                        End If
                        myReader2.Close()

                    End If
                Loop
                myReader1.Close()

                fase.Value = "6"

                SCALA = ""
                PIANO = ""
                COD_UNITA = ""
                ALLOGGIO = ""
                PIANOA = ""
                FOGLIO = ""
                MAPPALE = ""
                SUBALTERNO = ""
                COD_ALLOGGIO = ""

                par.cmd.CommandText = "select id_unita,COD_UNITA_IMMOBILIARE from siscom_mi.unita_contrattuale where id_unita_principale is null and id_contratto=" & idc
                Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderD.Read Then
                    COD_ALLOGGIO = par.IfNull(myReaderD("COD_UNITA_IMMOBILIARE"), "")
                    par.cmd.CommandText = "SELECT SCALE_EDIFICI.DESCRIZIONE AS SCALA,UNITA_IMMOBILIARI.*,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.SEZIONE," _
                                    & "IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB,IDENTIFICATIVI_CATASTALI.COD_TIPOLOGIA_CATASTO," _
                                    & "IDENTIFICATIVI_CATASTALI.RENDITA,IDENTIFICATIVI_CATASTALI.COD_CATEGORIA_CATASTALE " _
                                    & ",IDENTIFICATIVI_CATASTALI.COD_CLASSE_CATASTALE,IDENTIFICATIVI_CATASTALI.COD_QUALITA_CATASTALE, " _
                                    & "IDENTIFICATIVI_CATASTALI.SUPERFICIE_MQ,IDENTIFICATIVI_CATASTALI.CUBATURA,IDENTIFICATIVI_CATASTALI.NUM_VANI" _
                                    & ",IDENTIFICATIVI_CATASTALI.SUPERFICIE_CATASTALE,IDENTIFICATIVI_CATASTALI.RENDITA_STORICA," _
                                    & "IDENTIFICATIVI_CATASTALI.IMMOBILE_STORICO,IDENTIFICATIVI_CATASTALI.VALORE_IMPONIBILE" _
                                    & ",IDENTIFICATIVI_CATASTALI.DATA_ACQUISIZIONE,IDENTIFICATIVI_CATASTALI.DATA_FINE_VALIDITA" _
                                    & ",IDENTIFICATIVI_CATASTALI.DITTA,IDENTIFICATIVI_CATASTALI.NUM_PARTITA" _
                                    & ",IDENTIFICATIVI_CATASTALI.ESENTE_ICI,IDENTIFICATIVI_CATASTALI.PERC_POSSESSO" _
                                    & ",IDENTIFICATIVI_CATASTALI.INAGIBILE,IDENTIFICATIVI_CATASTALI.MICROZONA_CENSUARIA" _
                                    & ",IDENTIFICATIVI_CATASTALI.ZONA_CENSUARIA,IDENTIFICATIVI_CATASTALI.COD_STATO_CATASTALE" _
                                    & ",INDIRIZZI.DESCRIZIONE AS ""IND"",INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA,INDIRIZZI.COD_COMUNE" _
                                    & " FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                    & "WHERE UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID=unita_immobiliari.id_indirizzo AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND UNITA_IMMOBILIARI.ID=" & par.IfNull(myReaderD("id_unita"), "-1")
                    Dim myReaderE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderE.Read Then
                        SCALA = par.IfNull(myReaderE("SCALA"), "")
                        ALLOGGIO = par.IfNull(myReaderE("INTERNO"), "")
                        PIANOA = par.IfNull(myReaderE("COD_TIPO_LIVELLO_PIANO"), "01")
                        FOGLIO = par.IfNull(myReaderE("FOGLIO"), "")
                        MAPPALE = par.IfNull(myReaderE("NUMERO"), "")
                        SUBALTERNO = par.IfNull(myReaderE("SUB"), "")
                    End If
                    myReaderE.Close()

                    If PIANOA <> "" Then
                        par.cmd.CommandText = "select descrizione from SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD='" & PIANOA & "'"
                        Dim myReaderxx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderxx.Read Then
                            PIANOA = par.IfNull(myReaderxx("DESCRIZIONE"), "")
                        End If
                        myReaderxx.Close()
                    End If
                End If
                myReaderD.Close()

                fase.Value = "7"


                par.cmd.CommandText = "update utenza_dichiarazioni set " _
                                  & " ID_LUOGO_NAS_DNTE=" & ID_LUOGO_NAS_DNTE _
                                  & ",TELEFONO_DNTE='" & par.PulisciStrSql(TELEFONO_DNTE) _
                                  & "',ID_LUOGO_RES_DNTE=" & ID_LUOGO_RES_DNTE _
                                  & ",ID_TIPO_IND_RES_DNTE=" & ID_TIPO_IND_RES_DNTE _
                                  & ",IND_RES_DNTE='" & par.PulisciStrSql(IND_RES_DNTE) _
                                  & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CIVICO_RES_DNTE) _
                                  & "',N_COMP_NUCLEO=" & N_COMP_NUCLEO _
                                  & ",N_INV_100_CON='" & N_INV_100_CON _
                                  & "',N_INV_100_SENZA='" & N_INV_100_SENZA _
                                  & "',N_INV_100_66='" & N_INV_100_66 _
                                  & "',ID_TIPO_CAT_AB=null" _
                                  & ",LUOGO_INT_ERP='Milano" _
                                  & "',DATA_INT_ERP='" & Format(Now, "yyyyMMdd") _
                                  & "',LUOGO_S='Milano" _
                                  & "',DATA_S=null" _
                                  & ",PROGR_DNTE=0" _
                                  & ",FL_GIA_TITOLARE='0" _
                                  & "',CAP_RES_DNTE='" & CAP_RES_DNTE _
                                  & "',MINORI_CARICO='" & MINORI_CARICO _
                                  & "',SCALA='" & par.PulisciStrSql(SCALA) _
                                  & "',PIANO='" & par.PulisciStrSql(PIANOA) _
                                  & "',ALLOGGIO='" & par.PulisciStrSql(ALLOGGIO) _
                                  & "',INT_C='0',INT_V='0',INT_A='0" _
                                  & "',FOGLIO='" & par.PulisciStrSql(FOGLIO) _
                                  & "',MAPPALE='" & par.PulisciStrSql(MAPPALE) _
                                  & "',SUB='" & par.PulisciStrSql(SUBALTERNO) _
                                  & "',COD_ALLOGGIO='" & COD_ALLOGGIO _
                                  & "',INT_M='0',ISEE=null" _
                                  & ",POSIZIONE='" & COD_ALLOGGIO _
                                  & "',TIPO_ASS='" & TIPO_ASS _
                                  & "',RAPPORTO='" & COD_CONTRATTO _
                                  & "',CHIAVE_ENTE_ESTERNO=-1" _
                                  & ",ID_CAF=" & Session.Item("ID_CAF") _
                                  & ",N_DISTINTA=0,ISE_ERP=NULL,ISR_ERP=NULL,ISP_ERP=NULL,PSE=NULL,VSE=NULL,NOTE_WEB='',DATA_CESSAZIONE='',PATR_SUPERATO='0',FL_UBICAZIONE='0',POSSESSO_UI='0'" _
                                  & ",FL_APPLICA_36='1',DATA_DECORRENZA='" & DATA_DECORRENZA _
                                  & "',FL_TUTORE='0',RAPPORTO_REALE='',FL_DA_VERIFICARE='0',FL_SOSPENSIONE='0',CARTA_I='" & par.PulisciStrSql(CARTA_I) _
                                  & "',CARTA_I_DATA='" & CARTA_I_DATA _
                                  & "',CARTA_SOGG_DATA='" & "" _
                                  & "',PERMESSO_SOGG_SCADE='" & "" _
                                  & "',PERMESSO_SOGG_RINNOVO='" & "" _
                                  & "',PERMESSO_SOGG_DATA='" & "" _
                                  & "',CARTA_I_RILASCIATA='" & par.PulisciStrSql(CARTA_I_RILASCIATA) _
                                  & "',PERMESSO_SOGG_N='" & "" _
                                  & "',CARTA_SOGG_N='" & "" _
                                  & "',FL_ATTIVITA_LAV='" & FL_ATTIVITA_LAV _
                                  & "',TIPO_DOCUMENTO=" & tipo_documento & " WHERE ID=" & lIdDichiarazioneAU
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & lIdDichiarazioneAU & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F130','','I')"
                par.cmd.ExecuteNonQuery()


                fase.Value = "8"

                Dim progr As Boolean = False
                Dim indice As Integer = 1

                par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.*,TIPOLOGIA_PARENTELA.id_sepa FROM siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.TIPOLOGIA_PARENTELA where TIPOLOGIA_PARENTELA.cod=cod_tipologia_parentela and ID_CONTRATTO=" & idc
                myReader1 = par.cmd.ExecuteReader()

                fase.Value = "81"
                Do While myReader1.Read
                    If par.IfNull(myReader1("data_fine"), "29991231") >= Format(Now, "yyyyMMdd") Then
                        par.cmd.CommandText = "select * from siscom_mi.anagrafica where id=" & myReader1("id_anagrafica")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read() Then
                            fase.Value = "82"
                            If progr = False And ListaInt.SelectedItem.Value = myReader1("id_anagrafica") Then
                                par.cmd.CommandText = "Insert into UTENZA_COMP_NUCLEO (ID, ID_DICHIARAZIONE, PROGR, COD_FISCALE, COGNOME, NOME, SESSO, DATA_NASCITA, USL, GRADO_PARENTELA, PERC_INVAL, INDENNITA_ACC) " _
                                                    & "Values " _
                                                    & "(seq_utenza_comp_nucleo.nextval, " & lIdDichiarazioneAU & ", 0, '" _
                                                    & par.PulisciStrSql(UCase(par.IfNull(myReader2("cod_fiscale"), ""))) & "', '" _
                                                    & par.PulisciStrSql(UCase(par.IfNull(myReader2("cognome"), ""))) _
                                                    & "', '" & par.PulisciStrSql(UCase(par.IfNull(myReader2("nome"), ""))) & "', '" _
                                                    & par.RicavaSesso(par.IfNull(myReader2("cod_fiscale"), "")) & "', '" _
                                                    & par.PulisciStrSql(par.IfNull(myReader2("data_nascita"), "")) & "', NULL, " & par.IfNull(myReader1("id_sepa"), "1") & ", 0, '0')"
                                par.cmd.ExecuteNonQuery()
                                fase.Value = "83"
                                progr = True
                            Else
                                fase.Value = "85"
                                par.cmd.CommandText = "Insert into UTENZA_COMP_NUCLEO (ID, ID_DICHIARAZIONE, PROGR, COD_FISCALE, COGNOME, NOME, SESSO, DATA_NASCITA, USL, GRADO_PARENTELA, PERC_INVAL, INDENNITA_ACC) " _
                                                    & "Values " _
                                                    & "(seq_utenza_comp_nucleo.nextval, " & lIdDichiarazioneAU & ", " & indice & ", '" _
                                                    & par.PulisciStrSql(UCase(par.IfNull(myReader2("cod_fiscale"), ""))) & "', '" _
                                                    & par.PulisciStrSql(UCase(par.IfNull(myReader2("cognome"), ""))) _
                                                    & "', '" & par.PulisciStrSql(UCase(par.IfNull(myReader2("nome"), ""))) & "', '" _
                                                    & par.RicavaSesso(par.IfNull(myReader2("cod_fiscale"), "")) & "', '" _
                                                    & par.PulisciStrSql(par.IfNull(myReader2("data_nascita"), "")) & "', NULL, " & par.IfNull(myReader1("id_sepa"), "1") & ", 0, '0')"
                                par.cmd.ExecuteNonQuery()
                                fase.Value = "84"
                            End If

                            indice = indice + 1

                        End If
                        myReader2.Close()

                    End If
                Loop
                myReader1.Close()

            End If
            myReader.Close()

            fase.Value = "87"
            'par.cmd.CommandText = "update siscom_mi.rapporti_utenza set id_au=" & lIdDichiarazioneAU & " where id=" & idc
            'par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "insert into siscom_mi.AU_CONTRATTI (id_au,ID_CONTRATTO) values (" & lIdDichiarazioneAU & "," & idc & ")"
            par.cmd.ExecuteNonQuery()

            fase.Value = "88"

            fase.Value = "9"

            If IDA.Value <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & IDA.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APP. AVVENUTO, INSERITA SCHEDA AU')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=2 WHERE ID=" & IDA.Value
                par.cmd.ExecuteNonQuery()
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & IDCONVOCAZIONE.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'INSERITA SCHEDA AU')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET ID_STATO=2 WHERE ID=" & IDCONVOCAZIONE.Value
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');if (conf){window.open('DichAUnuova.aspx?ID=" & lIdDichiarazioneAU & "&CHIUDI=1&CH=1&ANNI=" & anni & "','Dettagli','');self.close();}" _
            & "else{window.close();}</script>"
            Response.Write(strScript)


        Catch ex As Exception
            Label2.Visible = True
            Label2.Text = fase.Value & " " & ex.Message
            par.myTrans.Rollback()
            par.OracleConn = CType(HttpContext.Current.Session.Item(t), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove(t)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If ListaInt.SelectedIndex <> -1 Then

            VerificaAnomalia(iddich.Value)

            If anomaliaReddAU = True Or anomaliaReddVSA = True Then
                Response.Write("<script>alert('Attenzione...Sono state riscontrate delle anomalie nei dati reddituali della dichiarazione che si sta importando.\nLa nuova scheda di Anagrafe Utenza riporterà i soli dati anagrafici!')</script>")
            End If

            Select Case tipo.Value
                Case "0" 'da contratto
                    CreaAU(idc.Value, t.Value)
                Case "1" ' da VSA
                    CreaAUVSA()
                Case "2" ' da AU
                    CreaAUAU()
            End Select
        Else
            Label2.Visible = True
            Label2.Text = "Selezionare un intestatario!"
        End If
    End Sub

    Private Function CreaAUAU()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim i As Integer = 1

        Dim id_intest As Long

        fase.Value = "0"


        'creo l'anagrafe utenza
        Dim sAnnoIsee As String
        Dim lIndice_Bando As Long

        Dim lIdDichiarazioneAU As Long

        Dim SCALA As String = ""
        Dim PIANO As String = ""
        Dim COD_UNITA As String = ""
        Dim ALLOGGIO As String = ""
        Dim PIANOA As String = ""
        Dim FOGLIO As String = ""
        Dim MAPPALE As String = ""
        Dim SUBALTERNO As String = ""
        Dim COD_ALLOGGIO As String = ""


        Dim TIPO_ASS As String = "1"
        Dim FL_ATTIVITA_LAV As String = "1"
        Dim COD_CONTRATTO As String = ""
        Dim DATA_CESS As String = ""
        Dim DATA_DECORRENZA As String = ""
        Dim ID_LUOGO_NAS_DNTE As Long = 0
        Dim TELEFONO_DNTE As String = ""
        Dim ID_LUOGO_RES_DNTE As Long = 0
        Dim IND_RES_DNTE As String = ""
        Dim CIVICO_RES_DNTE As String = ""
        Dim CAP_RES_DNTE As String = ""
        Dim N_COMP_NUCLEO As Integer = 0
        Dim N_INV_100_CON As Integer = 0
        Dim N_INV_100_SENZA As Integer = 0
        Dim N_INV_100_66 As Integer = 0
        Dim MINORI_CARICO As Integer = 0
        Dim CARTA_I As String = ""
        Dim CARTA_I_DATA As String = ""
        Dim CARTA_I_RILASCIATA As String = ""
        Dim tipo_documento As String = ""
        Dim ID_TIPO_IND_RES_DNTE As Integer = 6
        Dim idRedditoTot As Long = 0

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM UTENZA_NUM_PROTOCOLLI"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = myReader(0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO UTENZA_NUM_PROTOCOLLI VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            'CREATO INDICE SU STATO
            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                sAnnoIsee = myReader(0)
                lIndice_Bando = myReader(1)
            End If
            myReader.Close()

            par.cmd.CommandText = "Insert into UTENZA_DICHIARAZIONI  (ID, PG, DATA_PG, LUOGO, DATA, NOTE, ID_STATO,ANNO_SIT_ECONOMICA,ID_BANDO,DATA_INIZIO_VAL,DATA_FINE_VAL,FL_AUSI) values (seq_utenza_dichiarazioni.nextval, '" & Format(lValoreCorrente, "0000000000") & "','" & Format(Now, "yyyyMMdd") & "','Milano','" & Format(Now, "yyyyMMdd") & "','', 0," & sAnnoIsee & "," & lIndice_Bando & ",'" & sAnnoIsee + 2 & "0101','" & CInt(sAnnoIsee) + 3 & "1231'," & AUS.Value & ")"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "select seq_utenza_dichiarazioni.currval from dual"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lIdDichiarazioneAU = myReader1(0)
            End If
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID = " & ListaInt.SelectedItem.Value
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                id_intest = myReader("ID") 'ottengo l'id dell'intestatario che intende presentare la domanda
            End If
            myReader.Close()


            Dim idLUOGOres As String = ""
            Dim idTIPOres As String = ""
            Dim indirizzoRes As String = ""
            Dim civicoRes As String = ""
            Dim capRes As String = ""
            Dim telefono As String = ""
            Dim annoReddito As String = ""
            Dim IDD As String = "0"

            Dim idLuogoNascita As Integer = 0
            par.cmd.CommandText = "SELECT * FROM utenza_COMP_NUCLEO WHERE ID=" & id_intest
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read Then
                IDD = myReaderCodF("ID_DICHIARAZIONE")
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & Mid(par.IfNull(myReaderCodF("COD_FISCALE"), "-------------------"), 12, 4) & "'"
                Dim myReaderCom As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderCom.Read Then
                    idLuogoNascita = par.IfNull(myReaderCom("ID"), "")
                End If
                myReaderCom.Close()
            End If
            myReaderCodF.Close()

            If idLuogoNascita = 0 Then
                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID = " & IDD
                Dim myReaderCom1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderCom1.Read Then
                    idLuogoNascita = par.IfNull(myReaderCom1("ID_LUOGO_NAS_DNTE"), "")
                End If
                myReaderCom1.Close()
            End If

            par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET ID_DICH_IMPORT = " & IDD & " WHERE ID=" & lIdDichiarazioneAU
            par.cmd.ExecuteNonQuery()

            '************* 13/03/2012 CERCO L'ULTIMA DICHIARAZIONE DA CUI IMPORTARE GLI INDIRIZZI AGGIORNATI

            'piurecente = CercaUltimeDich(idLUOGOres, idTIPOres, indirizzoRes, civicoRes, capRes, telefono)


            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID = " & iddich.Value
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If idLuogoNascita = 0 Then
                    idLuogoNascita = par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
                End If
                annoReddito = sAnnoIsee

                SCALA = ""
                PIANO = ""
                COD_UNITA = ""
                ALLOGGIO = ""
                PIANOA = ""
                FOGLIO = ""
                MAPPALE = ""
                SUBALTERNO = ""
                COD_ALLOGGIO = ""

                par.cmd.CommandText = "select id_unita,COD_UNITA_IMMOBILIARE from siscom_mi.unita_contrattuale where id_unita_principale is null and id_contratto=" & idc.Value
                Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderD.Read Then
                    COD_ALLOGGIO = par.IfNull(myReaderD("COD_UNITA_IMMOBILIARE"), "")
                    par.cmd.CommandText = "SELECT SCALE_EDIFICI.DESCRIZIONE AS SCALA,UNITA_IMMOBILIARI.*,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.SEZIONE," _
                                    & "IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB,IDENTIFICATIVI_CATASTALI.COD_TIPOLOGIA_CATASTO," _
                                    & "IDENTIFICATIVI_CATASTALI.RENDITA,IDENTIFICATIVI_CATASTALI.COD_CATEGORIA_CATASTALE " _
                                    & ",IDENTIFICATIVI_CATASTALI.COD_CLASSE_CATASTALE,IDENTIFICATIVI_CATASTALI.COD_QUALITA_CATASTALE, " _
                                    & "IDENTIFICATIVI_CATASTALI.SUPERFICIE_MQ,IDENTIFICATIVI_CATASTALI.CUBATURA,IDENTIFICATIVI_CATASTALI.NUM_VANI" _
                                    & ",IDENTIFICATIVI_CATASTALI.SUPERFICIE_CATASTALE,IDENTIFICATIVI_CATASTALI.RENDITA_STORICA," _
                                    & "IDENTIFICATIVI_CATASTALI.IMMOBILE_STORICO,IDENTIFICATIVI_CATASTALI.VALORE_IMPONIBILE" _
                                    & ",IDENTIFICATIVI_CATASTALI.DATA_ACQUISIZIONE,IDENTIFICATIVI_CATASTALI.DATA_FINE_VALIDITA" _
                                    & ",IDENTIFICATIVI_CATASTALI.DITTA,IDENTIFICATIVI_CATASTALI.NUM_PARTITA" _
                                    & ",IDENTIFICATIVI_CATASTALI.ESENTE_ICI,IDENTIFICATIVI_CATASTALI.PERC_POSSESSO" _
                                    & ",IDENTIFICATIVI_CATASTALI.INAGIBILE,IDENTIFICATIVI_CATASTALI.MICROZONA_CENSUARIA" _
                                    & ",IDENTIFICATIVI_CATASTALI.ZONA_CENSUARIA,IDENTIFICATIVI_CATASTALI.COD_STATO_CATASTALE" _
                                    & ",INDIRIZZI.DESCRIZIONE AS ""IND"",INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA,INDIRIZZI.COD_COMUNE" _
                                    & " FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                    & "WHERE UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID=unita_immobiliari.id_indirizzo AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND UNITA_IMMOBILIARI.ID=" & par.IfNull(myReaderD("id_unita"), "-1")
                    Dim myReaderE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderE.Read Then
                        SCALA = par.IfNull(myReaderE("SCALA"), "")
                        ALLOGGIO = par.IfNull(myReaderE("INTERNO"), "")
                        PIANOA = par.IfNull(myReaderE("COD_TIPO_LIVELLO_PIANO"), "01")
                        FOGLIO = par.IfNull(myReaderE("FOGLIO"), "")
                        MAPPALE = par.IfNull(myReaderE("NUMERO"), "")
                        SUBALTERNO = par.IfNull(myReaderE("SUB"), "")
                    End If
                    myReaderE.Close()

                    If PIANOA <> "" Then
                        par.cmd.CommandText = "select descrizione from SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD='" & PIANOA & "'"
                        Dim myReaderxx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderxx.Read Then
                            PIANOA = par.IfNull(myReaderxx("DESCRIZIONE"), "")
                        End If
                        myReaderxx.Close()
                    End If
                End If
                myReaderD.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idc.Value
                myReaderD = par.cmd.ExecuteReader()
                If myReaderD.Read Then
                    COD_CONTRATTO = par.IfNull(myReaderD("cod_contratto"), "")
                    DATA_DECORRENZA = par.IfNull(myReaderD("data_decorrenza"), "")
                    DATA_CESS = par.IfNull(myReaderD("data_RICONSEGNA"), "")
                End If
                myReaderD.Close()

                par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET ID_LUOGO_NAS_DNTE=" & idLuogoNascita _
                                    & ",TELEFONO_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("TELEFONO_DNTE"), "")) _
                                    & "',ID_LUOGO_RES_DNTE=" & par.IfNull(myReader("ID_LUOGO_RES_DNTE"), 0) _
                                    & ",ID_TIPO_IND_RES_DNTE=" & par.IfNull(myReader("ID_TIPO_IND_RES_DNTE"), 0) _
                                    & ",IND_RES_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("IND_RES_DNTE"), "")) _
                                    & "', CIVICO_RES_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("CIVICO_RES_DNTE"), "")) _
                                    & "', N_COMP_NUCLEO=" & par.IfNull(myReader("N_COMP_NUCLEO"), "0") _
                                    & ", N_INV_100_CON=" & par.IfNull(myReader("N_INV_100_CON"), "0") _
                                    & ", N_INV_100_SENZA=" & par.IfNull(myReader("N_INV_100_SENZA"), "0") _
                                    & ", N_INV_100_66=" & par.IfNull(myReader("N_INV_100_66"), "0") _
                                    & ", ID_TIPO_CAT_AB=" & par.IfNull(myReader("ID_TIPO_CAT_AB"), "NULL") & ", " _
                                    & "ANNO_SIT_ECONOMICA=" & annoReddito _
                                    & ", LUOGO_INT_ERP='" & par.PulisciStrSql(par.IfNull(myReader("LUOGO_INT_ERP"), "")) _
                                    & "', DATA_INT_ERP='" & Format(Now, "yyyyMMdd") _
                                    & "', LUOGO_S='" & par.IfNull(myReader("LUOGO_S"), "") _
                                    & "', DATA_S='" & Format(Now, "yyyyMMdd") _
                                    & "', PROGR_DNTE=0, " _
                                    & "FL_GIA_TITOLARE='" & par.IfNull(myReader("FL_GIA_TITOLARE"), "") _
                                    & "', CAP_RES_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("CAP_RES_DNTE"), "")) _
                                    & "', MINORI_CARICO='" & par.IfNull(myReader("MINORI_CARICO"), "") _
                                    & "', TIPO_ASS=1,  CHIAVE_ENTE_ESTERNO='-1', ID_CAF=" & Session.Item("ID_CAF") & ", N_DISTINTA=0, " _
                                    & "FL_SCARICO=NULL, FL_CONFERMA_SCARICO=NULL, FL_UBICAZIONE='0', POSSESSO_UI='0', " _
                                    & "FL_APPLICA_36='1', FL_DELEGATO='0',FL_TUTORE='0',RAPPORTO_REALE='', FL_DA_VERIFICARE='0', FL_SOSPENSIONE='0', FL_SOSP_1='0', FL_SOSP_2='0', FL_SOSP_3='0', FL_SOSP_4='0', " _
                                    & "FL_SOSP_5='0', FL_SOSP_6='0', FL_SOSP_7='0', CARTA_I='', CARTA_I_DATA='', CARTA_SOGG_DATA='', PERMESSO_SOGG_SCADE='', PERMESSO_SOGG_RINNOVO='', PERMESSO_SOGG_DATA='', CARTA_I_RILASCIATA='', " _
                                    & "PERMESSO_SOGG_N='', CARTA_SOGG_N='', FL_ATTIVITA_LAV='0', TIPO_DOCUMENTO='-1', FL_NATO_ESTERO='0', FL_CITTADINO_IT='0', FL_RIC_POSTA='0', FL_4_5_LOTTO='0', COD_CONVOCAZIONE='', " _
                                    & "FL_GENERAZ_AUTO=0," _
                                    & "SCALA='" & par.PulisciStrSql(SCALA) _
                                    & "',PIANO='" & par.PulisciStrSql(PIANOA) _
                                    & "',ALLOGGIO='" & par.PulisciStrSql(ALLOGGIO) _
                                    & "',INT_C='0',INT_V='0',INT_A='0" _
                                    & "',FOGLIO='" & par.PulisciStrSql(FOGLIO) _
                                    & "',MAPPALE='" & par.PulisciStrSql(MAPPALE) _
                                    & "',SUB='" & par.PulisciStrSql(SUBALTERNO) _
                                    & "',COD_ALLOGGIO='" & COD_ALLOGGIO _
                                    & "',INT_M='0',ISEE=null" _
                                    & ",POSIZIONE='" & COD_ALLOGGIO _
                                    & "',RAPPORTO='" & COD_CONTRATTO _
                                    & "',DATA_CESSAZIONE='" & DATA_CESS _
                                    & "',DATA_DECORRENZA='" & DATA_DECORRENZA _
                                    & "' WHERE ID = " & lIdDichiarazioneAU


                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                   & "VALUES (" & lIdDichiarazioneAU & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                   & "'F130','','I')"
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "update siscom_mi.rapporti_utenza set id_au=" & lIdDichiarazioneAU & " where id=" & idc.Value
                'par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "insert into siscom_mi.AU_CONTRATTI (id_au,ID_CONTRATTO) values (" & lIdDichiarazioneAU & "," & idc.Value & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReader.Close()

            Dim id_compon As Long
            Dim new_id_compon As Long
            Dim IND_PROGR As Integer = 1

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & iddich.Value & " AND ID=" & ListaInt.SelectedItem.Value
            myReader = par.cmd.ExecuteReader()
            While myReader.Read '-------------- INTESTATARIO --------------'
                id_compon = myReader("ID")

                par.cmd.CommandText = "SELECT SEQ_UTENZA_COMP_NUCLEO.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    new_id_compon = myReaderN(0)
                End If
                myReaderN.Close()

                par.cmd.CommandText = "INSERT INTO UTENZA_COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                    & "VALUES (" & new_id_compon & "," & lIdDichiarazioneAU & ",0,'" & UCase(par.IfNull(myReader("COD_FISCALE"), "")) & "','" & par.PulisciStrSql(UCase(par.IfNull(myReader("COGNOME"), ""))) & "'," _
                    & "'" & par.PulisciStrSql(UCase(par.IfNull(myReader("NOME"), ""))) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "'," _
                    & par.IfNull(myReader("PERC_INVAL"), 0) & ",'" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
                par.cmd.ExecuteNonQuery()


                If anomaliaReddAU = False Then


                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_IMMOB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,INDIRIZZO,CIVICO,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                            & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "','" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) _
                            & "','" & par.IfNull(myReader2("N_VANI"), "") & "','" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), "") & ",'" & par.PulisciStrSql(par.IfNull(myReader2("INDIRIZZO"), "")) _
                            & "','" & par.IfNull(myReader2("CIVICO"), "") & "'," & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), "NULL") & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_MOB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_MOB.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader3.Close()

                    Dim idReddComp As Long = 0
                    Dim reddAltro As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader4.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_REDDITO (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                            & "VALUES (SEQ_UTENZA_COMP_REDDITO.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader4("REDDITO_IRPEF"), "") & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_UTENZA_COMP_REDDITO.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idReddComp = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader5.Read
                            reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                            par.cmd.CommandText = "UPDATE UTENZA_COMP_REDDITO SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReader5.Close()
                    End While
                    myReader4.Close()

                    'par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'While myReader5.Read
                    '    par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ALTRI_REDDITI (ID,ID_COMPONENTE,IMPORTO) " _
                    '    & "VALUES (SEQ_UTENZA_COMP_ALTRI_REDDITI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader5("IMPORTO"), "") & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End While
                    'myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader6.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ELENCO_SPESE (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                        & "VALUES (SEQ_UTENZA_COMP_ELENCO_SPESE.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader6.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader7.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_DETRAZIONI (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                        & "VALUES (SEQ_UTENZA_COMP_DETRAZIONI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader7.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader8.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_REDDITI (ID,ID_UTENZA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazioneAU & "," & new_id_compon & ",'" _
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
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_AUTONOMO_IMPORTI (SELECT seq_UTENZA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_AUTONOMO_IMPORTI WHERE ID=" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddA.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddD.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_DIPEND_IMPORTI (SELECT seq_UTENZA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID=" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddD.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddN.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_NO_ISEE_IMPORTI (SELECT seq_UTENZA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID=" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddN.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_ES_IMPORTI (SELECT seq_UTENZA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP2.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_IMPORTI (SELECT seq_UTENZA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP2.Close()
                    End While
                    myReader8.Close()

                End If


            End While
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & iddich.Value & " AND ID<>" & ListaInt.SelectedItem.Value & " ORDER BY PROGR ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read '-------------- CICLO SUI COMPONENTI DEL NUCLEO FAMILIARE --------------'
                id_compon = myReader("ID")

                par.cmd.CommandText = "SELECT SEQ_UTENZA_COMP_NUCLEO.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    new_id_compon = myReaderN(0)
                End If
                myReaderN.Close()

                par.cmd.CommandText = "INSERT INTO UTENZA_COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                    & "VALUES (" & new_id_compon & "," & lIdDichiarazioneAU & "," & IND_PROGR & ",'" & UCase(par.IfNull(myReader("COD_FISCALE"), "")) & "','" & par.PulisciStrSql(UCase(par.IfNull(myReader("COGNOME"), ""))) & "'," _
                    & "'" & par.PulisciStrSql(UCase(par.IfNull(myReader("NOME"), ""))) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "'," _
                    & par.IfNull(myReader("PERC_INVAL"), 0) & ",'" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
                par.cmd.ExecuteNonQuery()

                IND_PROGR = IND_PROGR + 1

                If anomaliaReddAU = False Then
                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_IMMOB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,INDIRIZZO,CIVICO,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                            & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "','" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) _
                            & "','" & par.IfNull(myReader2("N_VANI"), "") & "','" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), "") & ",'" & par.PulisciStrSql(par.IfNull(myReader2("INDIRIZZO"), "")) _
                            & "','" & par.IfNull(myReader2("CIVICO"), "") & "'," & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), "NULL") & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader2.Close()

                    Dim idReddComp As Long = 0
                    Dim reddAltro As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_MOB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_MOB.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader4.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_REDDITO (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                            & "VALUES (SEQ_UTENZA_COMP_REDDITO.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader4("REDDITO_IRPEF"), "") & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_UTENZA_COMP_REDDITO.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idReddComp = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader5.Read
                            reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                            par.cmd.CommandText = "UPDATE UTENZA_COMP_REDDITO SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReader5.Close()

                    End While
                    myReader4.Close()

                    'par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'While myReader5.Read
                    '    par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ALTRI_REDDITI (ID,ID_COMPONENTE,IMPORTO) " _
                    '    & "VALUES (SEQ_UTENZA_COMP_ALTRI_REDDITI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader5("IMPORTO"), "") & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End While
                    'myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader6.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ELENCO_SPESE (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                        & "VALUES (SEQ_UTENZA_COMP_ELENCO_SPESE.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader6.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader7.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_DETRAZIONI (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                        & "VALUES (SEQ_UTENZA_COMP_DETRAZIONI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader7.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader8.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_REDDITI (ID,ID_UTENZA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazioneAU & "," & new_id_compon & ",'" _
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
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_AUTONOMO_IMPORTI (SELECT seq_UTENZA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_AUTONOMO_IMPORTI WHERE ID=" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddA.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddD.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_DIPEND_IMPORTI (SELECT seq_UTENZA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID=" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddD.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddN.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_NO_ISEE_IMPORTI (SELECT seq_UTENZA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID=" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddN.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_ES_IMPORTI (SELECT seq_UTENZA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP2.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_IMPORTI (SELECT seq_UTENZA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP2.Close()
                    End While
                    myReader8.Close()

                End If
            End While
            myReader.Close()

            If IDA.Value <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & IDA.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APP. AVVENUTO, INSERITA SCHEDA AU')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=2 WHERE ID=" & IDA.Value
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & IDCONVOCAZIONE.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'INSERITA SCHEDA AU')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET ID_STATO=2 WHERE ID=" & IDCONVOCAZIONE.Value
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'CaricaDomVSA()

            strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');if (conf){window.open('DichAUnuova.aspx?ID=" & lIdDichiarazioneAU & "&CHIUDI=1&CH=1&ANNI=" & anni & "','Dettagli','');self.close();}" _
             & "else{window.close();}</script>"
            Response.Write(strScript)

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Function


    Public Property anni() As String
        Get
            If Not (ViewState("par_anni") Is Nothing) Then
                Return CStr(ViewState("par_anni"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_anni") = value
        End Set

    End Property

    Public Property anomaliaReddAU() As Boolean
        Get
            If Not (ViewState("par_anomaliaReddAU") Is Nothing) Then
                Return CBool(ViewState("par_anomaliaReddAU"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_anomaliaReddAU") = value
        End Set

    End Property

    Public Property anomaliaReddVSA() As Boolean
        Get
            If Not (ViewState("par_anomaliaReddVSA") Is Nothing) Then
                Return CBool(ViewState("par_anomaliaReddVSA"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_anomaliaReddVSA") = value
        End Set

    End Property

    Private Function CreaAUVSA()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim i As Integer = 1
        Dim strScript As String
        Dim id_intest As Long

        fase.Value = "0"


        'creo l'anagrafe utenza
        Dim sAnnoIsee As String
        Dim lIndice_Bando As Long

        Dim lIdDichiarazioneAU As Long

        Dim SCALA As String = ""
        Dim PIANO As String = ""
        Dim COD_UNITA As String = ""
        Dim ALLOGGIO As String = ""
        Dim PIANOA As String = ""
        Dim FOGLIO As String = ""
        Dim MAPPALE As String = ""
        Dim SUBALTERNO As String = ""
        Dim COD_ALLOGGIO As String = ""


        Dim TIPO_ASS As String = "1"
        Dim FL_ATTIVITA_LAV As String = "1"
        Dim COD_CONTRATTO As String = ""
        Dim DATA_CESS As String = ""
        Dim DATA_DECORRENZA As String = ""
        Dim ID_LUOGO_NAS_DNTE As Long = 0
        Dim TELEFONO_DNTE As String = ""
        Dim ID_LUOGO_RES_DNTE As Long = 0
        Dim IND_RES_DNTE As String = ""
        Dim CIVICO_RES_DNTE As String = ""
        Dim CAP_RES_DNTE As String = ""
        Dim N_COMP_NUCLEO As Integer = 0
        Dim N_INV_100_CON As Integer = 0
        Dim N_INV_100_SENZA As Integer = 0
        Dim N_INV_100_66 As Integer = 0
        Dim MINORI_CARICO As Integer = 0
        Dim CARTA_I As String = ""
        Dim CARTA_I_DATA As String = ""
        Dim CARTA_I_RILASCIATA As String = ""
        Dim tipo_documento As String = ""
        Dim ID_TIPO_IND_RES_DNTE As Integer = 6
        Dim idRedditoTot As Long = 0

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM UTENZA_NUM_PROTOCOLLI"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = myReader(0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO UTENZA_NUM_PROTOCOLLI VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                sAnnoIsee = myReader(0)
                lIndice_Bando = myReader(1)
            End If
            myReader.Close()

            par.cmd.CommandText = "Insert into UTENZA_DICHIARAZIONI  (ID, PG, DATA_PG, LUOGO, DATA, NOTE, ID_STATO,ANNO_SIT_ECONOMICA,ID_BANDO,DATA_INIZIO_VAL,DATA_FINE_VAL,FL_AUSI) values (seq_utenza_dichiarazioni.nextval, '" & Format(lValoreCorrente, "0000000000") & "','" & Format(Now, "yyyyMMdd") & "','Milano','" & Format(Now, "yyyyMMdd") & "','', 0," & sAnnoIsee & "," & lIndice_Bando & ",'" & sAnnoIsee + 2 & "0101','" & CInt(sAnnoIsee) + 3 & "1231'," & AUS.Value & ")"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "select seq_utenza_dichiarazioni.currval from dual"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lIdDichiarazioneAU = myReader1(0)
            End If
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID = " & ListaInt.SelectedItem.Value
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                id_intest = myReader("ID") 'ottengo l'id dell'intestatario che intende presentare la domanda
            End If
            myReader.Close()


            Dim idLUOGOres As String = ""
            Dim idTIPOres As String = ""
            Dim indirizzoRes As String = ""
            Dim civicoRes As String = ""
            Dim capRes As String = ""
            Dim telefono As String = ""
            Dim annoReddito As String = ""
            Dim IDD As String = "0"

            Dim idLuogoNascita As Integer = 0
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID=" & id_intest
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read Then
                IDD = myReaderCodF("ID_DICHIARAZIONE")
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & Mid(par.IfNull(myReaderCodF("COD_FISCALE"), "-------------------"), 12, 4) & "'"
                Dim myReaderCom As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderCom.Read Then
                    idLuogoNascita = par.IfNull(myReaderCom("ID"), "")
                End If
                myReaderCom.Close()
            End If
            myReaderCodF.Close()

            If idLuogoNascita = 0 Then
                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID = " & IDD
                Dim myReaderCom1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderCom1.Read Then
                    idLuogoNascita = par.IfNull(myReaderCom1("ID_LUOGO_NAS_DNTE"), "")
                End If
                myReaderCom1.Close()
            End If


            par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET ID_DICH_IMPORT = " & IDD & " WHERE ID=" & lIdDichiarazioneAU
            par.cmd.ExecuteNonQuery()

            '************* 13/03/2012 CERCO L'ULTIMA DICHIARAZIONE DA CUI IMPORTARE GLI INDIRIZZI AGGIORNATI

            'piurecente = CercaUltimeDich(idLUOGOres, idTIPOres, indirizzoRes, civicoRes, capRes, telefono)


            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID = " & iddich.Value
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If idLuogoNascita = 0 Then
                    idLuogoNascita = par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
                End If
                annoReddito = sAnnoIsee

                SCALA = ""
                PIANO = ""
                COD_UNITA = ""
                ALLOGGIO = ""
                PIANOA = ""
                FOGLIO = ""
                MAPPALE = ""
                SUBALTERNO = ""
                COD_ALLOGGIO = ""

                par.cmd.CommandText = "select id_unita,COD_UNITA_IMMOBILIARE from siscom_mi.unita_contrattuale where id_unita_principale is null and id_contratto=" & idc.Value
                Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderD.Read Then
                    COD_ALLOGGIO = par.IfNull(myReaderD("COD_UNITA_IMMOBILIARE"), "")
                    par.cmd.CommandText = "SELECT SCALE_EDIFICI.DESCRIZIONE AS SCALA,UNITA_IMMOBILIARI.*,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.SEZIONE," _
                                    & "IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB,IDENTIFICATIVI_CATASTALI.COD_TIPOLOGIA_CATASTO," _
                                    & "IDENTIFICATIVI_CATASTALI.RENDITA,IDENTIFICATIVI_CATASTALI.COD_CATEGORIA_CATASTALE " _
                                    & ",IDENTIFICATIVI_CATASTALI.COD_CLASSE_CATASTALE,IDENTIFICATIVI_CATASTALI.COD_QUALITA_CATASTALE, " _
                                    & "IDENTIFICATIVI_CATASTALI.SUPERFICIE_MQ,IDENTIFICATIVI_CATASTALI.CUBATURA,IDENTIFICATIVI_CATASTALI.NUM_VANI" _
                                    & ",IDENTIFICATIVI_CATASTALI.SUPERFICIE_CATASTALE,IDENTIFICATIVI_CATASTALI.RENDITA_STORICA," _
                                    & "IDENTIFICATIVI_CATASTALI.IMMOBILE_STORICO,IDENTIFICATIVI_CATASTALI.VALORE_IMPONIBILE" _
                                    & ",IDENTIFICATIVI_CATASTALI.DATA_ACQUISIZIONE,IDENTIFICATIVI_CATASTALI.DATA_FINE_VALIDITA" _
                                    & ",IDENTIFICATIVI_CATASTALI.DITTA,IDENTIFICATIVI_CATASTALI.NUM_PARTITA" _
                                    & ",IDENTIFICATIVI_CATASTALI.ESENTE_ICI,IDENTIFICATIVI_CATASTALI.PERC_POSSESSO" _
                                    & ",IDENTIFICATIVI_CATASTALI.INAGIBILE,IDENTIFICATIVI_CATASTALI.MICROZONA_CENSUARIA" _
                                    & ",IDENTIFICATIVI_CATASTALI.ZONA_CENSUARIA,IDENTIFICATIVI_CATASTALI.COD_STATO_CATASTALE" _
                                    & ",INDIRIZZI.DESCRIZIONE AS ""IND"",INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA,INDIRIZZI.COD_COMUNE" _
                                    & " FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                    & "WHERE UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID=unita_immobiliari.id_indirizzo AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND UNITA_IMMOBILIARI.ID=" & par.IfNull(myReaderD("id_unita"), "-1")
                    Dim myReaderE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderE.Read Then
                        SCALA = par.IfNull(myReaderE("SCALA"), "")
                        ALLOGGIO = par.IfNull(myReaderE("INTERNO"), "")
                        PIANOA = par.IfNull(myReaderE("COD_TIPO_LIVELLO_PIANO"), "01")
                        FOGLIO = par.IfNull(myReaderE("FOGLIO"), "")
                        MAPPALE = par.IfNull(myReaderE("NUMERO"), "")
                        SUBALTERNO = par.IfNull(myReaderE("SUB"), "")
                    End If
                    myReaderE.Close()

                    If PIANOA <> "" Then
                        par.cmd.CommandText = "select descrizione from SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD='" & PIANOA & "'"
                        Dim myReaderxx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderxx.Read Then
                            PIANOA = par.IfNull(myReaderxx("DESCRIZIONE"), "")
                        End If
                        myReaderxx.Close()
                    End If
                End If
                myReaderD.Close()

                par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idc.Value
                myReaderD = par.cmd.ExecuteReader()
                If myReaderD.Read Then
                    COD_CONTRATTO = par.IfNull(myReaderD("cod_contratto"), "")
                    DATA_DECORRENZA = par.IfNull(myReaderD("data_decorrenza"), "")
                    DATA_CESS = par.IfNull(myReaderD("data_RICONSEGNA"), "")
                End If
                myReaderD.Close()

                par.cmd.CommandText = "UPDATE SEPA.UTENZA_DICHIARAZIONI SET ID_LUOGO_NAS_DNTE=" & idLuogoNascita _
                                    & ",TELEFONO_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("TELEFONO_DNTE"), "")) _
                                    & "',ID_LUOGO_RES_DNTE=" & par.IfNull(myReader("ID_LUOGO_RES_DNTE"), 0) _
                                    & ",ID_TIPO_IND_RES_DNTE=" & par.IfNull(myReader("ID_TIPO_IND_RES_DNTE"), 0) _
                                    & ",IND_RES_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("IND_RES_DNTE"), "")) _
                                    & "', CIVICO_RES_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("CIVICO_RES_DNTE"), "")) _
                                    & "', N_COMP_NUCLEO=" & par.IfNull(myReader("N_COMP_NUCLEO"), "0") _
                                    & ", N_INV_100_CON=" & par.IfNull(myReader("N_INV_100_CON"), "0") _
                                    & ", N_INV_100_SENZA=" & par.IfNull(myReader("N_INV_100_SENZA"), "0") _
                                    & ", N_INV_100_66=" & par.IfNull(myReader("N_INV_100_66"), "0") _
                                    & ", ID_TIPO_CAT_AB=" & par.IfNull(myReader("ID_TIPO_CAT_AB"), "NULL") & ", " _
                                    & "ANNO_SIT_ECONOMICA=" & annoReddito _
                                    & ", LUOGO_INT_ERP='" & par.PulisciStrSql(par.IfNull(myReader("LUOGO_INT_ERP"), "")) _
                                    & "', DATA_INT_ERP='" & Format(Now, "yyyyMMdd") _
                                    & "', LUOGO_S='" & par.IfNull(myReader("LUOGO_S"), "") _
                                    & "', DATA_S='" & Format(Now, "yyyyMMdd") _
                                    & "', PROGR_DNTE=0, " _
                                    & "FL_GIA_TITOLARE='" & par.IfNull(myReader("FL_GIA_TITOLARE"), "") _
                                    & "', CAP_RES_DNTE='" & par.PulisciStrSql(par.IfNull(myReader("CAP_RES_DNTE"), "")) _
                                    & "', MINORI_CARICO='" & par.IfNull(myReader("MINORI_CARICO"), "") _
                                    & "', TIPO_ASS=1,  CHIAVE_ENTE_ESTERNO='-1', ID_CAF=" & Session.Item("ID_CAF") & ", N_DISTINTA=0, " _
                                    & "FL_SCARICO=NULL, FL_CONFERMA_SCARICO=NULL, FL_UBICAZIONE='0', POSSESSO_UI='0', " _
                                    & "FL_APPLICA_36='1', FL_DELEGATO='0',FL_TUTORE='0',RAPPORTO_REALE='', FL_DA_VERIFICARE='0', FL_SOSPENSIONE='0', FL_SOSP_1='0', FL_SOSP_2='0', FL_SOSP_3='0', FL_SOSP_4='0', " _
                                    & "FL_SOSP_5='0', FL_SOSP_6='0', FL_SOSP_7='0', CARTA_I='', CARTA_I_DATA='', CARTA_SOGG_DATA='', PERMESSO_SOGG_SCADE='', PERMESSO_SOGG_RINNOVO='', PERMESSO_SOGG_DATA='', CARTA_I_RILASCIATA='', " _
                                    & "PERMESSO_SOGG_N='', CARTA_SOGG_N='', FL_ATTIVITA_LAV='0', TIPO_DOCUMENTO='-1', FL_NATO_ESTERO='0', FL_CITTADINO_IT='0', FL_RIC_POSTA='0', FL_4_5_LOTTO='0', COD_CONVOCAZIONE='', " _
                                    & "FL_GENERAZ_AUTO=0" _
                                    & ",SCALA='" & par.PulisciStrSql(SCALA) _
                                    & "',PIANO='" & par.PulisciStrSql(PIANOA) _
                                    & "',ALLOGGIO='" & par.PulisciStrSql(ALLOGGIO) _
                                    & "',INT_C='0',INT_V='0',INT_A='0" _
                                    & "',FOGLIO='" & par.PulisciStrSql(FOGLIO) _
                                    & "',MAPPALE='" & par.PulisciStrSql(MAPPALE) _
                                    & "',SUB='" & par.PulisciStrSql(SUBALTERNO) _
                                    & "',COD_ALLOGGIO='" & COD_ALLOGGIO _
                                    & "',INT_M='0',ISEE=null" _
                                    & ",POSIZIONE='" & COD_ALLOGGIO _
                                    & "',RAPPORTO='" & COD_CONTRATTO _
                                    & "',DATA_CESSAZIONE='" & DATA_CESS _
                                    & "',DATA_DECORRENZA='" & DATA_DECORRENZA _
                                    & "' WHERE ID = " & lIdDichiarazioneAU


                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                   & "VALUES (" & lIdDichiarazioneAU & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                   & "'F130','','I')"
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "update siscom_mi.rapporti_utenza set id_au=" & lIdDichiarazioneAU & " where id=" & idc.Value
                'par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "insert into siscom_mi.AU_CONTRATTI (id_au,ID_CONTRATTO) values (" & lIdDichiarazioneAU & "," & idc.Value & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReader.Close()

            Dim id_compon As Long
            Dim new_id_compon As Long
            Dim IND_PROGR As Integer = 1

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & iddich.Value & " AND ID=" & ListaInt.SelectedItem.Value
            myReader = par.cmd.ExecuteReader()
            While myReader.Read '-------------- INTESTATARIO --------------'
                id_compon = myReader("ID")

                par.cmd.CommandText = "SELECT SEQ_UTENZA_COMP_NUCLEO.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    new_id_compon = myReaderN(0)
                End If
                myReaderN.Close()

                par.cmd.CommandText = "INSERT INTO UTENZA_COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                    & "VALUES (" & new_id_compon & "," & lIdDichiarazioneAU & ",0,'" & UCase(par.IfNull(myReader("COD_FISCALE"), "")) & "','" & par.PulisciStrSql(UCase(par.IfNull(myReader("COGNOME"), ""))) & "'," _
                    & "'" & par.PulisciStrSql(UCase(par.IfNull(myReader("NOME"), ""))) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "'," _
                    & par.IfNull(myReader("PERC_INVAL"), 0) & ",'" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
                par.cmd.ExecuteNonQuery()



                If anomaliaReddVSA = False Then


                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_IMMOB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                            & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "','" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "','" & par.IfNull(myReader2("N_VANI"), "") & "','" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" _
                            & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), "NULL") & "," & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), "NULL") & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_MOB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_MOB.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader3.Close()

                    Dim idReddComp As Long = 0
                    Dim reddAltro As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader4.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_REDDITO (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                            & "VALUES (SEQ_UTENZA_COMP_REDDITO.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader4("REDDITO_IRPEF"), "") & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_UTENZA_COMP_REDDITO.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idReddComp = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader5.Read
                            reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                            par.cmd.CommandText = "UPDATE UTENZA_COMP_REDDITO SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReader5.Close()
                    End While
                    myReader4.Close()

                    'par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'While myReader5.Read
                    '    par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ALTRI_REDDITI (ID,ID_COMPONENTE,IMPORTO) " _
                    '    & "VALUES (SEQ_UTENZA_COMP_ALTRI_REDDITI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader5("IMPORTO"), "") & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End While
                    'myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader6.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ELENCO_SPESE (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                        & "VALUES (SEQ_UTENZA_COMP_ELENCO_SPESE.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader6.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader7.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_DETRAZIONI (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                        & "VALUES (SEQ_UTENZA_COMP_DETRAZIONI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader7.Close()

                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader8.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_REDDITI (ID,ID_UTENZA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazioneAU & "," & new_id_compon & ",'" _
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
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_AUTONOMO_IMPORTI (SELECT seq_UTENZA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID=" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddA.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddD.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_DIPEND_IMPORTI (SELECT seq_UTENZA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_DIPEND_IMPORTI WHERE ID=" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddD.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddN.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_NO_ISEE_IMPORTI (SELECT seq_UTENZA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID=" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddN.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_ES_IMPORTI (SELECT seq_UTENZA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP2.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_IMPORTI (SELECT seq_UTENZA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP2.Close()
                    End While
                    myReader8.Close()

                End If

            End While
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & iddich.Value & " AND ID<>" & ListaInt.SelectedItem.Value & " ORDER BY PROGR ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read '-------------- CICLO SUI COMPONENTI DEL NUCLEO FAMILIARE --------------'
                id_compon = myReader("ID")

                par.cmd.CommandText = "SELECT SEQ_UTENZA_COMP_NUCLEO.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    new_id_compon = myReaderN(0)
                End If
                myReaderN.Close()

                par.cmd.CommandText = "INSERT INTO UTENZA_COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                    & "VALUES (" & new_id_compon & "," & lIdDichiarazioneAU & "," & IND_PROGR & ",'" & UCase(par.IfNull(myReader("COD_FISCALE"), "")) & "','" & par.PulisciStrSql(UCase(par.IfNull(myReader("COGNOME"), ""))) & "'," _
                    & "'" & par.PulisciStrSql(UCase(par.IfNull(myReader("NOME"), ""))) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "'," _
                    & par.IfNull(myReader("PERC_INVAL"), 0) & ",'" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
                par.cmd.ExecuteNonQuery()

                IND_PROGR = IND_PROGR + 1

                If anomaliaReddVSA = False Then


                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_IMMOB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                            & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "','" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "','" & par.IfNull(myReader2("N_VANI"), "") & "','" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" _
                            & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), "NULL") & "," & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), "NULL") & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_PATR_MOB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO) " _
                            & "VALUES (SEQ_UTENZA_COMP_PATR_MOB.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader3.Close()

                    Dim idReddComp As Long = 0
                    Dim reddAltro As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader4.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_REDDITO (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                            & "VALUES (SEQ_UTENZA_COMP_REDDITO.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader4("REDDITO_IRPEF"), "") & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_UTENZA_COMP_REDDITO.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idReddComp = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader5.Read
                            reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                            par.cmd.CommandText = "UPDATE UTENZA_COMP_REDDITO SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReader5.Close()
                    End While
                    myReader4.Close()

                    'par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'While myReader5.Read
                    '    par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ALTRI_REDDITI (ID,ID_COMPONENTE,IMPORTO) " _
                    '    & "VALUES (SEQ_UTENZA_COMP_ALTRI_REDDITI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader5("IMPORTO"), "") & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End While
                    'myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader6.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ELENCO_SPESE (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                        & "VALUES (SEQ_UTENZA_COMP_ELENCO_SPESE.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader6.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader7.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_COMP_DETRAZIONI (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                        & "VALUES (SEQ_UTENZA_COMP_DETRAZIONI.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader7.Close()

                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader8.Read
                        par.cmd.CommandText = "INSERT INTO UTENZA_REDDITI (ID,ID_UTENZA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazioneAU & "," & new_id_compon & ",'" _
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
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_AUTONOMO_IMPORTI (SELECT seq_UTENZA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID=" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddA.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddD.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_DIPEND_IMPORTI (SELECT seq_UTENZA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_DIPEND_IMPORTI WHERE ID=" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddD.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddN.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_NO_ISEE_IMPORTI (SELECT seq_UTENZA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID=" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddN.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_ES_IMPORTI (SELECT seq_UTENZA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP2.Read
                            par.cmd.CommandText = "INSERT INTO UTENZA_REDD_PENS_IMPORTI (SELECT seq_UTENZA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP2.Close()
                    End While
                    myReader8.Close()

                End If

            End While
            myReader.Close()

            If IDA.Value <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & IDA.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APP. AVVENUTO, INSERITA SCHEDA AU')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=2 WHERE ID=" & IDA.Value
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & IDCONVOCAZIONE.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'INSERITA SCHEDA AU')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET ID_STATO=2 WHERE ID=" & IDCONVOCAZIONE.Value
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'CaricaDomVSA()

            strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');if (conf){window.open('DichAUnuova.aspx?ID=" & lIdDichiarazioneAU & "&CHIUDI=1&CH=1&ANNI=" & anni & "','Dettagli','');self.close();}" _
             & "else{window.close();}</script>"
            Response.Write(strScript)

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Function
End Class
