
Partial Class Contratti_CreaAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'Dim Str As String

        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:190px; height:100px; top:50px; left:25px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='inserimento in corso' ><br>inserimento in corso..."
        'Str = Str & "<" & "/div>"

        'Response.Write(Str)
        'Response.Flush()
        Response.Expires = 0
        If IsPostBack = False Then


            idc.Value = Request.QueryString("IDC")
            t.Value = Request.QueryString("T")
            If Verifica(idc.Value) = True Then
                procedi.value = "1"
                Intestatari(idc.Value, t.Value)
                If procedi.value = "1" Then
                    If ListaInt.Items.Count = 1 Then
                        ListaInt.Items(0).Selected = True
                        CreaAU(idc.Value, t.Value)
                    End If
                Else
                    ImageButton1.Visible = False
                End If
            Else
                Response.Redirect("CreaAU1.aspx")
            End If
        End If

    End Sub

    Private Function Verifica(ByVal idc As String) As Boolean
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Verifica = True
            par.cmd.CommandText = "select utenza_dichiarazioni.* from utenza_dichiarazioni,siscom_mi.rapporti_utenza where rapporti_utenza.id=" & idc & " and utenza_dichiarazioni.rapporto=rapporti_utenza.cod_contratto and utenza_dichiarazioni.anno_sit_economica=2008"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Verifica = False
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
            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "SELECT COGNOME||' '||NOME AS NOMINATIVO,ID,data_nascita FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idc & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY ANAGRAFICA.COGNOME ASC,ANAGRAFICA.NOME ASC"
            par.cmd.CommandText = "SELECT COGNOME||' '||NOME AS NOMINATIVO,ID,data_nascita,cod_fiscale,cognome,nome FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE (SOGGETTI_CONTRATTUALI.DATA_FINE IS NULL OR SOGGETTI_CONTRATTUALI.DATA_FINE>='" & Format(Now, "yyyyMMdd") & "') AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idc & " AND  ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY ANAGRAFICA.COGNOME ASC,ANAGRAFICA.NOME ASC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                If par.IfNull(myReader("data_nascita"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " NON HA LA DATA DI NASCITA!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    procedi.value = "0"
                    ImageButton1.Visible = False
                End If
                If par.ControllaCF(par.IfNull(myReader("cod_fiscale"), "")) = False Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " HA UN CODICE FISCALE ERRATO!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    procedi.value = "0"
                    ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("cognome"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " HA IL CAMPO COGNOME VUOTO!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    procedi.value = "0"
                    ImageButton1.Visible = False
                End If

                If par.IfNull(myReader("NOME"), "") = "" Then
                    Response.Write("<script>alert('Attenzione..." & Replace(myReader("nominativo"), "'", "") & " HA IL CAMPO NOME VUOTO!! APRIRE LA SCHEDA ANAGRAFICA TRAMITE IL MENU A SINISTRA E INSERIRE!');</script>")
                    procedi.value = "0"
                    ImageButton1.Visible = False
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


            'Dim ds As New Data.DataSet()
            'Dim dlist As RadioButtonList
            'Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            'dlist = ListaInt

            'da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT COGNOME||' '||NOME AS NOMINATIVO,ID FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idc & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY ANAGRAFICA.COGNOME ASC,ANAGRAFICA.NOME ASC", par.OracleConn)
            'da.Fill(ds)

            'dlist.Items.Clear()
            'dlist.DataSource = ds
            'dlist.DataTextField = "NOMINATIVO"
            'dlist.DataValueField = "ID"
            'dlist.DataBind()

            'da.Dispose()
            'da = Nothing

            'dlist.DataSource = Nothing
            'dlist = Nothing

            'ds.Clear()
            'ds.Dispose()
            'ds = Nothing

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write(ex.Message)
        End Try

    End Function


    Private Function CreaAU(ByVal idc As String, ByVal t As String)
        Try

            fase.value = "0"


            'creo l'anagrafe utenza
            Dim sAnnoIsee As String = ""
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

            fase.value = "1"
            par.OracleConn = CType(HttpContext.Current.Session.Item(t), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & t), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            fase.value = "2"
            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                sAnnoIsee = "2008" 'myReader(0)
                lIndice_Bando = 2 'myReader(1)
            End If
            myReader.Close()

            fase.value = "3"
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

                fase.value = "4"


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

                par.cmd.CommandText = "Insert into UTENZA_DICHIARAZIONI  (ID, PG, DATA_PG, LUOGO, DATA, NOTE, ID_STATO,ANNO_SIT_ECONOMICA,ID_BANDO) values (seq_utenza_dichiarazioni.nextval, '" & Format(lValoreCorrente, "0000000000") & "','" & Format(Now, "yyyyMMdd") & "','Milano','" & Format(Now, "yyyyMMdd") & "','', 0," & sAnnoIsee & "," & lIndice_Bando & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select seq_utenza_dichiarazioni.currval from dual"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    lIdDichiarazioneAU = myReader1(0)
                End If
                myReader1.Close()

                fase.value = "5"

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

                fase.value = "6"

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

                fase.value = "7"


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


                fase.value = "8"

                Dim progr As Boolean = False
                Dim indice As Integer = 1

                par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.*,TIPOLOGIA_PARENTELA.id_sepa FROM siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.TIPOLOGIA_PARENTELA where TIPOLOGIA_PARENTELA.cod=cod_tipologia_parentela and ID_CONTRATTO=" & idc
                myReader1 = par.cmd.ExecuteReader()

                fase.value = "81"
                Do While myReader1.Read
                    If par.IfNull(myReader1("data_fine"), "29991231") >= Format(Now, "yyyyMMdd") Then
                        par.cmd.CommandText = "select * from siscom_mi.anagrafica where id=" & myReader1("id_anagrafica")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read() Then
                            fase.value = "82"
                            If progr = False And ListaInt.SelectedItem.Value = myReader1("id_anagrafica") Then
                                par.cmd.CommandText = "Insert into UTENZA_COMP_NUCLEO (ID, ID_DICHIARAZIONE, PROGR, COD_FISCALE, COGNOME, NOME, SESSO, DATA_NASCITA, USL, GRADO_PARENTELA, PERC_INVAL, INDENNITA_ACC) " _
                                                    & "Values " _
                                                    & "(seq_utenza_comp_nucleo.nextval, " & lIdDichiarazioneAU & ", 0, '" _
                                                    & par.PulisciStrSql(UCase(par.IfNull(myReader2("cod_fiscale"), ""))) & "', '" _
                                                    & par.PulisciStrSql(UCase(par.IfNull(myReader2("cognome"), ""))) _
                                                    & "', '" & par.PulisciStrSql(UCase(par.IfNull(myReader2("nome"), ""))) & "', '" _
                                                    & par.RicavaSesso(UCase(par.IfNull(myReader2("cod_fiscale"), ""))) & "', '" _
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
                                                    & par.RicavaSesso(UCase(par.IfNull(myReader2("cod_fiscale"), ""))) & "', '" _
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

            fase.value = "87"
            par.cmd.CommandText = "update siscom_mi.rapporti_utenza set id_au=" & lIdDichiarazioneAU & " where id=" & idc
            par.cmd.ExecuteNonQuery()
            fase.value = "88"

            fase.value = "9"

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & t, par.myTrans)

            fase.value = "10"

            Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")

            'par.cmd.Dispose()
            'par.myTrans.Commit()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Label2.Visible = True
            Label2.Text = fase.value & " " & ex.Message
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

            CreaAU(idc.Value, t.Value)
            'Response.Write("<script>window.open('CreaAU1.aspx?IDC=" & idc.Value & "&t=" & t.Value & "&I=" & ListaInt.SelectedItem.Value & "','Crea','');self.close();</script>")
        Else
            Label2.Visible = True
            Label2.Text = "Selezionare un intestatario!"
        End If
    End Sub
End Class
