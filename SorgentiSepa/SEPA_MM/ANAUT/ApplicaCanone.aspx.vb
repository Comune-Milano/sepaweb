
Partial Class ANAUT_ApplicaCanone
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'Dim Str As String = ""

        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...</br>"
        'Str = Str & "</div> <br />"

        'Response.Write(Str)
        'Response.Flush()

        If Not IsPostBack Then
            Label10.Text = "Stai per applicare il canone calcolato sulla base dell'Anagrafe utenza. Il canone sarà emesso dalla prossima bollettazione. Indicare se inserire il canone di competenza del 1 anno o del 2 anno di validità dell'AU."
            Carica()
        End If
    End Sub

    Public Property lIdAU() As Long
        Get
            If Not (ViewState("par_lIdAU") Is Nothing) Then
                Return CLng(ViewState("par_lIdAU"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdAU") = value
        End Set
    End Property

    Public Property Provenienza() As Long
        Get
            If Not (ViewState("par_Provenienza") Is Nothing) Then
                Return CLng(ViewState("par_Provenienza"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_Provenienza") = value
        End Set
    End Property

    Public Property AnnoAu() As Long
        Get
            If Not (ViewState("par_AnnoAu") Is Nothing) Then
                Return CLng(ViewState("par_AnnoAu"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_AnnoAu") = value
        End Set
    End Property

    Public Property IdTipoGest() As Long
        Get
            If Not (ViewState("par_IdTipoGest") Is Nothing) Then
                Return CLng(ViewState("par_IdTipoGest"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdTipoGest") = value
        End Set
    End Property

    Public Property IdVoceGest() As Long
        Get
            If Not (ViewState("par_IdVoceGest") Is Nothing) Then
                Return CLng(ViewState("par_IdVoceGest"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdVoceGest") = value
        End Set
    End Property

    Public Property idBollGest() As Long
        Get
            If Not (ViewState("par_idBollGest") Is Nothing) Then
                Return CLng(ViewState("par_idBollGest"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idBollGest") = value
        End Set
    End Property

    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI ORDER BY ID DESC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lIdAU = myReader("id")
                Provenienza = myReader("id_tipo_provenienza")
                cmbAnno.Items.Add(New ListItem(Mid(myReader("inizio_canone"), 1, 4), "1"))
                cmbAnno.Items.Add(New ListItem(Mid(myReader("fine_canone"), 1, 4), "2"))
                'cmbAnnoApplicazione.Items.Add(New ListItem(Mid(myReader("inizio_canone"), 1, 4), "1"))
                'cmbAnnoApplicazione.Items.Add(New ListItem(Mid(myReader("fine_canone"), 1, 4), "2"))
                AnnoAu = myReader("ANNO_AU")
                Label11.Text = "Anno " & AnnoAu & " Redditi " & myReader("ANNO_isee")
            End If
            myReader.Close()

            ''CALCOLO CONGUAGLIO AU - DISABILITATO
            'par.cmd.CommandText = "SELECT TAB_VOCI_CONG_AU.*,TIPO_BOLLETTE_GEST.descrizione FROM TAB_VOCI_CONG_AU,SISCOM_MI.TIPO_BOLLETTE_GEST where TIPO_BOLLETTE_GEST.id=TAB_VOCI_CONG_AU.id_tipo_gest and id_au=" & lIdAU
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    IdTipoGest = myReader("ID_TIPO_GEST")
            '    IdVoceGest = myReader("ID_VOCE")
            '    chkConguaglioAU.Text = "Calcola Conguaglio e inserisci in gestionale con bolletta di tipo: <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & par.IfNull(myReader("DESCRIZIONE"), "")
            'Else
            '    chkConguaglioAU.Checked = False
            '    chkConguaglioAU.Enabled = False
            '    Dim Str As String = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">alert('Non sarà possibile calcolare ed applicare i conguagli in quanto non è stata definita la tipologia di gestionale da utilizzare!');</script>"
            '    Response.Write(Str)
            'End If
            'myReader.Close()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label10.Text = ex.Message
            Label10.Visible = True
        End Try
    End Function

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Dim Tariffa As Double = 0
            Dim Str As String = ""
            Dim contatore As Long = 0
            Dim contatore1 As Long = 0
            Dim contatoreNonFatti As Long = 0
            Dim ElencoNonFatti As String = ""
            Dim IndiceApplicazioneAU As Long = -1

            Dim VecchioContributoRisc As Double = 0
            Dim VecchioCanone As Double = 0
            Dim VecchioContributoCanone As Double = 0

            Dim IdVoceContributoCanone As Long = 0
            Dim IdVoceContributoCalore As Long = 0
            Dim VociContributo As Boolean = par.RicavaIdVociContributo(IdVoceContributoCanone, IdVoceContributoCalore)
            If VociContributo = False Then
                'errore
            End If
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Ricerca ed elaborazione in corso degli RU da aggiornare...Attendere il messaggio di fine operazione</br>RU Analizzati:<input id=" & Chr(34) & "Text1" & Chr(34) & " readonly=" & Chr(34) & "readonly" & Chr(34) & " type=" & Chr(34) & "text" & Chr(34) & " style=" & Chr(34) & "border: 1px solid #FFFFFF; font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; text-align: center; width: 30px;" & Chr(34) & "/> Aggiornati:<input id=" & Chr(34) & "Text2" & Chr(34) & " readonly=" & Chr(34) & "readonly" & Chr(34) & " type=" & Chr(34) & "text" & Chr(34) & " style=" & Chr(34) & "border: 1px solid #FFFFFF; font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; text-align: center; width: 30px;" & Chr(34) & "/></div><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo;var tempo1; tempo=''; tempo1=''; function Mostra() {document.getElementById('Text1').value = tempo;document.getElementById('Text2').value = tempo1;}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

            Response.Write(Str)
            Response.Flush()

            Dim DataStampa1 As String = Format(Now, "yyyyMMddHHmmss")
            Dim DataStampa2 As String = Format(DateAdd(DateInterval.Second, 1, Now), "yyyyMMddHHmmss")
            Dim DataStampa3 As String = Format(DateAdd(DateInterval.Second, 4, Now), "yyyyMMddHHmmss")
            Dim DataStampa4 As String = Format(DateAdd(DateInterval.Second, 8, Now), "yyyyMMddHHmmss")
            Dim DataStampa5 As String = Format(DateAdd(DateInterval.Second, 12, Now), "yyyyMMddHHmmss")

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "Insert into SISCOM_MI.CANONI_EC_APPLICAZIONI_AU_FILE (ID, DATA_APPLICAZIONE, ID_AU, TIPO) Values (siscom_mi.SEQ_CANONI_EC_APP_AU_FILE.nextval, '" & Format(Now, "yyyyMMddHHmmss") & "', " & lIdAU & ", 0)"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select siscom_mi.SEQ_CANONI_EC_APP_AU_FILE.currval from dual"
            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX.Read Then
                IndiceApplicazioneAU = par.IfNull(myReaderX(0), -1)
            End If
            myReaderX.Close()

            Dim ss As String = ""
            If TextBox1.Text <> "" Then
                ss = " cod_contratto in ('" & Replace(TextBox1.Text, ",", "','") & "') and "
            End If

            'par.cmd.CommandText = "select distinct id_contratto from siscom_mi.canoni_ec where tipo_provenienza=" & Provenienza & " and id_contratto not in (select id_contratto from SISCOM_MI.EVENTI_CONTRATTI where MOTIVAZIONE LIKE 'APPLICAZIONE CANONE AU " & AnnoAu & "%')"
            par.cmd.CommandText = "select distinct id_contratto,data_calcolo from siscom_mi.canoni_ec where " & ss & " tipo_provenienza=" & Provenienza & " order by data_calcolo desc"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                If cmbAnno.SelectedItem.Value = "1" Then
                    par.cmd.CommandText = "select (SELECT nvl(VALORE,0) FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_CONTRATTUALE.ID_UNITA) AS SUP_NETTA,rapporti_utenza.nro_rate,canoni_ec.*,rapporti_utenza.imp_canone_iniziale,unita_contrattuale.id_unita,utenza_dichiarazioni.data_pg,rapporti_utenza_prossima_bol.prossima_bolletta from SISCOM_MI.rapporti_utenza_prossima_bol,utenza_dichiarazioni,siscom_mi.rapporti_utenza,siscom_mi.unita_contrattuale, siscom_mi.canoni_ec where rapporti_utenza_prossima_bol.id_contratto (+)=rapporti_utenza.id and utenza_dichiarazioni.id (+)=CANONI_EC.id_dichiarazione and rapporti_utenza.id=unita_contrattuale.id_contratto and unita_contrattuale.id_contratto=canoni_ec.id_contratto and unita_contrattuale.id_unita_principale is null and tipo_provenienza=" & Provenienza & " and canoni_ec.id_contratto=" & myReader("id_contratto") & " order by data_calcolo asc"
                Else
                    par.cmd.CommandText = "select (SELECT nvl(VALORE,0) FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_CONTRATTUALE.ID_UNITA) AS SUP_NETTA,rapporti_utenza.nro_rate,canoni_ec.*,rapporti_utenza.imp_canone_iniziale,unita_contrattuale.id_unita,utenza_dichiarazioni.data_pg,rapporti_utenza_prossima_bol.prossima_bolletta from SISCOM_MI.rapporti_utenza_prossima_bol,utenza_dichiarazioni,siscom_mi.rapporti_utenza,siscom_mi.unita_contrattuale, siscom_mi.canoni_ec where rapporti_utenza_prossima_bol.id_contratto (+)=rapporti_utenza.id and utenza_dichiarazioni.id (+)=CANONI_EC.id_dichiarazione and rapporti_utenza.id=unita_contrattuale.id_contratto and unita_contrattuale.id_contratto=canoni_ec.id_contratto and unita_contrattuale.id_unita_principale is null and tipo_provenienza=" & Provenienza & " and canoni_ec.id_contratto=" & myReader("id_contratto") & " order by data_calcolo desc"
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then

                    VecchioContributoRisc = 0
                    VecchioCanone = 0
                    VecchioContributoCanone = 0

                    VecchioCanone = par.IfNull(myReader1("imp_canone_iniziale"), 0)

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_CONTRATTI WHERE ID_CONTRATTO=" & myReader("ID_CONTRATTO") & " AND MOTIVAZIONE LIKE 'APPLICAZIONE CANONE AU " & AnnoAu & "%'"
                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.HasRows = False Then

                        'VERIFICO SE ESISTONO APPLICAZIONI CANONE DERIVANTI DA GESTIONE LOCATARI CON LO STESSO PERIODO
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE TIPO_PROVENIENZA=1 AND ID_CONTRATTO=" & myReader("ID_CONTRATTO") & " AND INIZIO_VALIDITA_CAN>='" & par.IfNull(myReader1("INIZIO_VALIDITA_CAN"), "29990101") & "'"
                        Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader6.HasRows = False Then

                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_CANONE_INIZIALE=" & par.VirgoleInPunti(myReader1("canone")) & " WHERE ID=" & myReader("ID_CONTRATTO")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & myReader("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & DataStampa1 & "','F02','" & par.PulisciStrSql("APPLICAZIONE CANONE AU " & AnnoAu & "  (EURO " & myReader1("canone") & "). CANONE PRECEDENTE (" & par.IfNull(myReader1("CANONE_ATTUALE"), "") & ")") & "')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "select * from siscom_mi.bol_schema where anno=" & cmbAnno.SelectedItem.Text & " and id_voce=" & IdVoceContributoCanone & " and id_contratto=" & myReader("ID_CONTRATTO") & " order by id desc"
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.HasRows = True Then
                                If myReader2.Read Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & myReader("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & DataStampa2 & "','F184','ELIMINATA VOCE Contributo Canone (euro " & par.VirgoleInPunti(Format((myReader2("IMPORTO_SINGOLA_RATA")), "0.00")) & ") MENSILI DA RATA " & myReader2("DA_RATA") & " PER " & myReader2("PER_RATE") & " RATE')"
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = "DELETE from siscom_mi.bol_schema where anno=" & cmbAnno.SelectedItem.Text & " and id_voce=" & IdVoceContributoCanone & " and id_contratto=" & myReader("ID_CONTRATTO")
                                    par.cmd.ExecuteNonQuery()

                                    VecchioContributoCanone = par.IfNull(myReader2("IMPORTO_SINGOLA_RATA"), 0)

                                End If
                            End If
                            myReader2.Close()

                            'max 11/01/2018 il contributo non viene più inserito dal 01/01/2018
                            'If par.IfNull(myReader1("delta_1243_12"), "-") <> "-" And par.IfNull(myReader1("delta_1243_12"), "0") <> "0" Then
                            '    If par.VirgoleInPunti(Format((myReader1("delta_1243_12") * -1), "0.00")) <> 0 Then
                            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE,IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & myReader("ID_CONTRATTO") & ", " & myReader1("ID_UNITA") & ", " & par.RicavaEsercizio(cmbAnno.SelectedItem.Text) & " , " & IdVoceContributoCanone & "," & par.VirgoleInPunti(Format((myReader1("delta_1243_12") * -1), "0.00")) & ", 1, " & myReader1("nro_rate") & ", " & par.VirgoleInPunti(Format(((myReader1("delta_1243_12") * -1) / myReader1("nro_rate")), "0.00")) & ", " & cmbAnno.SelectedItem.Text & ")"
                            '        par.cmd.ExecuteNonQuery()

                            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & myReader("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & DataStampa3 & "','F184','AGGIUNTA VOCE Contributo CANONE (euro -" & par.IfNull(myReader1("delta_1243_12"), "") & ") ANNUALI')"
                            '        par.cmd.ExecuteNonQuery()
                            '    Else
                            '        'Beep()
                            '    End If
                            'End If

                            par.cmd.CommandText = "select * from siscom_mi.bol_schema where anno=" & cmbAnno.SelectedItem.Text & " and id_voce=" & IdVoceContributoCalore & " and id_contratto=" & myReader("ID_CONTRATTO") & " order by id desc"
                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.HasRows = True Then
                                If myReader2.Read Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & myReader("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & DataStampa4 & "','F184','ELIMINATA VOCE CONTRIBUTO RISC/SERV (euro " & par.VirgoleInPunti(Format((myReader2("IMPORTO_SINGOLA_RATA")), "0.00")) & ") MENSILI PER APPLICAZIONE AU')"
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = "DELETE from siscom_mi.bol_schema where anno=" & cmbAnno.SelectedItem.Text & " and id_voce=" & IdVoceContributoCalore & " and id_contratto=" & myReader("ID_CONTRATTO")
                                    par.cmd.ExecuteNonQuery()

                                    VecchioContributoRisc = par.IfNull(myReader2("IMPORTO_SINGOLA_RATA"), 0)

                                End If
                            End If
                            myReader2.Close()

                            'max 11/01/2018 il contributo non viene più inserito dal 01/01/2018
                            'If myReader1("ID_AREA_ECONOMICA") = "1" Then
                            '    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO IN (SELECT UNITA_IMMOBILIARI.ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & myReader("ID_CONTRATTO") & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL)"
                            '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_EDIFICI, siscom_mi.condomini WHERE ID_EDIFICIO IN (SELECT UNITA_IMMOBILIARI.ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE ID = UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO = " & myReader("ID_CONTRATTO") & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL) and SISCOM_MI.COND_EDIFICI.id_condominio= siscom_mi.condomini.id and tipo_gestione='D'"
                            '    myReader2 = par.cmd.ExecuteReader()
                            '    If myReader2.HasRows = False Then
                            '        'If myReader2.Read Then
                            '        Tariffa = Format(5.1600000000000001 * CDbl(par.IfNull(myReader1("sup_netta"), 0)), "0.00")
                            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE,IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & myReader("ID_CONTRATTO") & ", " & myReader1("ID_UNITA") & ", " & par.RicavaEsercizio(cmbAnno.SelectedItem.Text) & " , " & IdVoceContributoCalore & "," & par.VirgoleInPunti(Format((Tariffa * -1), "0.00")) & ", 1, " & myReader1("nro_rate") & ", " & par.VirgoleInPunti(Format(((Tariffa * -1) / myReader1("nro_rate")), "0.00")) & ", " & cmbAnno.SelectedItem.Text & ")"
                            '        par.cmd.ExecuteNonQuery()

                            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & myReader("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & DataStampa5 & "','F184','AGGIUNTA VOCE CONTRIBUTO RISC/SERV (euro " & par.VirgoleInPunti(Format((Tariffa), "0.00")) & ") ANNUALI')"
                            '        par.cmd.ExecuteNonQuery()
                            '        'End If
                            '    End If
                            '    myReader2.Close()
                            'End If

                            par.cmd.CommandText = "Insert into SISCOM_MI.CANONI_EC_APPLICAZIONI_AU (ID_CANONI_EC, ID_CANONI_EC_APP_FILE,CANONE,CONTR_CANONE,CONTR_RISC) Values (" & par.IfNull(myReader1("id"), -1) & ", " & IndiceApplicazioneAU & "," & par.VirgoleInPunti(VecchioCanone) & "," & par.VirgoleInPunti(VecchioContributoCanone) & "," & par.VirgoleInPunti(VecchioContributoRisc) & ")"
                            par.cmd.ExecuteNonQuery()


                            '' 12/04/2018 CALCOLO CONGUAGLIO AU - DISABILITATO
                            'Dim DataInizio As String = ""
                            'Dim DataFine As String = ""
                            'Dim Emesso As Decimal = 0
                            'Dim Dovuto As Decimal = 0
                            'Dim Conguaglio As Decimal = 0
                            'If chkConguaglioAU.Checked = True Then
                            '    If par.IfNull(myReader("DATA_PG"), "") <> "" Then

                            '        If par.IfNull(myReader("INIZIO_VALIDITA"), "29990101") > par.IfNull(myReader("DATA_PG"), "") Then
                            '            DataInizio = par.IfNull(myReader("INIZIO_VALIDITA"), "29990101")
                            '        Else
                            '            'Si parte dal mese successivo a quello della data Pg
                            '            DataInizio = Format(DateAdd("M", 1, CDate(par.FormattaData(par.IfNull(myReader("DATA_PG"), "29990101")))), "yyyyMMdd")
                            '        End If

                            '        'If par.IfNull(myReader("prossima_bolletta"), "29990101") & "01" > Mid(Format(DateSerial(Now.Year, Now.Month + 1, 0), "yyyyMMdd"), 1, 6) & "01" Then
                            '        '    DataFine = par.IfNull(myReader("prossima_bolletta"), "29990101") & "31"
                            '        'Else
                            '        '    'Si parte dal mese successivo a quello della data Pg
                            '        '    DataFine = Format(DateSerial(Now.Year, Now.Month + 1, 0), "yyyyMMdd")
                            '        'End If

                            '        DataFine = Format(DateAdd("d", -1, CDate(par.FormattaData(par.IfNull(myReader("prossima_bolletta"), "299901") & "01"))), "yyyyMMdd")

                            '        If DataFine >= DataInizio Then
                            '            'calcolo emesso dalla data di inizio AU fino ad Oggi
                            '            Emesso = par.CalcolaEmesso(myReader("ID_CONTRATTO"), Mid(DataInizio, 1, 6) & "01", DataFine, 4)
                            '            'Calcolo del dovuto con riferimento DataInizio e ultimo giorno del mese del calcolo
                            '            Dovuto = par.CalcolaDovuto(myReader("CANONE_DA_APPLICARE"), Mid(DataInizio, 1, 6) & "01", DataFine)
                            '            Conguaglio = Dovuto - Emesso
                            '            If Conguaglio <> 0 Then
                            '                par.cmd.CommandText = "Insert into BOL_BOLLETTE_GEST  (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA, " _
                            '                            & "RIFERIMENTO_DA, RIFERIMENTO_A, IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, " _
                            '                            & "ID_TIPO, TIPO_APPLICAZIONE, DATA_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE,  NOTE, " _
                            '                            & "ID_EVENTO_PAGAMENTO) " _
                            '                            & "Values " _
                            '                            & "(SEQ_BOL_BOLLETTE_GEST.NEXTVAL, " & myReader("ID_CONTRATTO") & ", " & par.RicavaEsercizioCorrente _
                            '                            & " , (SELECT ID_UNITA FROM UNITA_CONTRATTUALE WHERE ID_UNITA_PRINCIPALE IS NULL AND ID_CONTRATTO=" & myReader("ID_CONTRATTO") _
                            '                            & "), (SELECT ID_ANAGRAFICA FROM SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & myReader("ID_CONTRATTO") & "), " _
                            '                            & "'" & Mid(DataInizio, 1, 6) & "01" & "', '" & Format(DateSerial(Now.Year, Now.Month + 1, 0), "yyyyMMdd") & "', " & par.VirgoleInPunti(Format(Conguaglio, "0.00")) _
                            '                            & ", '" & Format(Now, "yyyyMMdd") & "', '', " _
                            '                           & "'', " & IdTipoGest & ", 'N', NULL, NULL, " _
                            '                           & "'CONGUAGLIO CANONE AU " & AnnoAu & "', NULL)"
                            '                par.cmd.ExecuteNonQuery()
                            '                par.cmd.CommandText = "SELECT SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                            '                Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            '                If myReaderC.Read() Then
                            '                    idBollGest = myReaderC(0)
                            '                End If
                            '                myReaderC.Close()

                            '                par.cmd.CommandText = "INSERT INTO BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            '                    & "VALUES (SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & "," & IdVoceGest & "," & par.VirgoleInPunti(Format(Conguaglio, "0.00")) & ")"
                            '                par.cmd.ExecuteNonQuery()

                            '                par.cmd.CommandText = "INSERT INTO EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            '            & "VALUES (" & myReader("ID_CONTRATTO") & "," & Session("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            '            & "'F08','BOLLETTA GESTIONALE CONGUAGLIO CANONE AU " & AnnoAu & " PARI A EURO " & Format(Conguaglio, "##,##0.00") & "')"
                            '                par.cmd.ExecuteNonQuery()
                            '            End If
                            '        End If
                            '    End If
                            'End If

                            contatore1 = contatore1 + 1

                        Else
                            contatoreNonFatti = contatoreNonFatti + 1
                            ElencoNonFatti = ElencoNonFatti & "," & par.IfNull(myReader("id_contratto"), "")
                        End If
                        myReader6.Close()
                    End If
                    myReader5.Close()
                    contatore = contatore + 1
                Else
                    contatoreNonFatti = contatoreNonFatti + 1
                    ElencoNonFatti = ElencoNonFatti & "," & par.IfNull(myReader("id_contratto"), "")
                End If

                myReader1.Close()

                Response.Write("<script>tempo=" & contatore & ";tempo1=" & contatore1 & ";</script>")
                Response.Flush()
            Loop
            myReader.Close()

            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If contatoreNonFatti > 0 Then
                txtNonFatti.Visible = True
                txtNonFatti.Text = "ATTENZIONE...ai seguenti contratti non è stato applicato il canone, contattare l'amministratore: " & ElencoNonFatti
                Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';alert('Operazione effettuata!');</script>"
            Else
                txtNonFatti.Visible = False
                Str = "<script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">document.getElementById('dvvvPre').style.visibility = 'hidden';alert('Operazione effettuata!');document.location.href = 'pagina_home.aspx';</script>"
            End If


            Response.Write(Str)
            Response.Flush()
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label10.Text = ex.Message
        End Try
    End Sub
End Class
