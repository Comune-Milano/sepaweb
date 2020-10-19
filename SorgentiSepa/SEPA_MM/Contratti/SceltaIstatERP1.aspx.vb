Imports System.IO
Imports Telerik.Web.UI

Partial Class Contratti_SceltaIstatERP1
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public TabCalcolo As New System.Data.DataTable

    Private Property dtCalcolo As Object

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then
            Try

                par.OracleConn.Open()
                par.SettaCommand(par)


                Dim Str As String = ""

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()

                Dim TIPOCONTRATTO As String = Request.QueryString("C")
                If TIPOCONTRATTO = "0" Then
                    TIPOCONTRATTO = ""
                    par.cmd.CommandText = "select COD,DESCRIZIONE from SISCOM_MI.tipologia_contratto_locazione where cod in (select COD_TIPOLOGIA_CONTR_LOC from SISCOM_MI.rapporti_utenza where id in (select  id_contratto from SISCOM_MI.CANONI_EC where tipo_provenienza=(select max(id_tipo_provenienza) from utenza_bandi where stato=1))) order by descrizione Asc"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader1.Read
                        TIPOCONTRATTO = TIPOCONTRATTO & par.IfNull(myReader1("cod"), "") & ","
                    Loop
                    myReader1.Close()
                    If TIPOCONTRATTO <> "" Then
                        TIPOCONTRATTO = Mid(TIPOCONTRATTO, 1, Len(TIPOCONTRATTO) - 1)
                        TIPOCONTRATTO = Replace(TIPOCONTRATTO, ",", "','")
                    End If
                End If
                If Request.QueryString("P") = "0" Then
                    sPeriodo = Request.QueryString("A")
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ADEGUAMENTO_ISTAT WHERE substr(ANNO_MESE,1,4)='" & sPeriodo & "' and id_contratto in (select id from SISCOM_MI.rapporti_utenza where data_riconsegna is null and COD_TIPOLOGIA_CONTR_LOC IN ('" & TIPOCONTRATTO & "')) "
                Else
                    sPeriodo = Request.QueryString("A") & Format(CInt(Request.QueryString("P")), "00")
                    par.cmd.CommandText = "SELECT * FROM SISOCM_MI.ADEGUAMENTO_ISTAT WHERE ANNO_MESE='" & sPeriodo & "'  and id_contratto in (select id from SISCOM_MI.rapporti_utenza where data_riconsegna is null and COD_TIPOLOGIA_CONTR_LOC IN ('" & TIPOCONTRATTO & "')) "
                End If

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = True Then
                    Response.Write("<script>alert('Attenzione, Aggiornamento ISTAT già effettuato per questo periodo. Verrà effettuato un nuovo calcolo');</script>")
                End If
                myReader.Close()

                If Request.QueryString("P") = "0" Then
                    Str = "SELECT rapporti_utenza.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_DECORRENZA,CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (cognome || ' ' || ANAGRAFICA.nome)) END AS INTESTATARIO,UNITA_CONTRATTUALE.ID_UNITA,RAPPORTI_UTENZA.IMP_CANONE_INIZIALE AS CANONE_ATTUALE  FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.RAPPORTI_UTENZA,siscom_mi.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.data_riconsegna is null and substr(data_decorrenza,1,4)<'" & Mid(sPeriodo, 1, 4) & "' and rapporti_utenza.cod_tipologia_contr_loc IN ('" & TIPOCONTRATTO & "') AND RAPPORTI_UTENZA.COD_CONTRATTO='0300030010400A01102'"
                Else
                    Str = "SELECT rapporti_utenza.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_DECORRENZA,CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (cognome || ' ' || ANAGRAFICA.nome)) END AS INTESTATARIO,UNITA_CONTRATTUALE.ID_UNITA,RAPPORTI_UTENZA.IMP_CANONE_INIZIALE AS CANONE_ATTUALE  FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.RAPPORTI_UTENZA,siscom_mi.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.data_riconsegna is null and substr(data_decorrenza,1,4)<'" & Mid(sPeriodo, 1, 4) & "' and substr(data_decorrenza,5,2)='" & Mid(sPeriodo, 5, 2) & "' and rapporti_utenza.cod_tipologia_contr_loc IN ('" & TIPOCONTRATTO & "') AND RAPPORTI_UTENZA.COD_CONTRATTO='0300030010400A01102' "

                End If


                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

                Dim ds As New Data.DataTable

                da.Fill(ds)


                If ds.Rows.Count <= 0 Then
                    par.OracleConn.Close()
                    Response.Write("<script>alert('Nessun contratto disponibile per l\'adeguamento!');location.href('SceltaIstat.aspx');</script>")
                    Response.Redirect("SceltaIstat.aspx")
                    Exit Sub
                End If
                DataGridVarIstat.DataSource = ds
                DataGridVarIstat.DataBind()

                If Request.QueryString("P") <> "0" Then
                    Label1.Text = "Elenco contratti da analizzare a " & par.ConvertiMese(Mid(sPeriodo, 5, 2)) & " " & Mid(sPeriodo, 1, 4) & " (Num. " & DataGridVarIstat.Items.Count & ")"
                Else
                    Label1.Text = "Elenco contratti da analizzare a " & Mid(sPeriodo, 1, 4) & " Tutti i mesi (Num. " & DataGridVarIstat.Items.Count & ")"
                End If
                par.OracleConn.Close()

                HttpContext.Current.Session.Add("AA", ds)
                imgExport.Attributes.Add("onclick", "javascript:window.open('Report/DownLoad.aspx?CHIAMA=4','Istat','');")

            Catch ex As Exception
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try

        End If
        RadProgressArea1.Localization.Uploaded = "Avanzamento Totale"
        RadProgressArea1.Localization.UploadedFiles = "Avanzamento"
        RadProgressArea1.Localization.CurrentFileName = "Elaborazione in corso: "
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Public Property sStringaSql() As String
        Get
            If Not (ViewState("par_sStringaSql") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSql") = value
        End Set

    End Property

    Public Property sNomeFile() As String
        Get
            If Not (ViewState("par_sNomeFile") Is Nothing) Then
                Return CStr(ViewState("par_sNomeFile"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNomeFile") = value
        End Set

    End Property

    Public Property sPeriodo() As String
        Get
            If Not (ViewState("par_sPeriodo") Is Nothing) Then
                Return CStr(ViewState("par_sPeriodo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPeriodo") = value
        End Set

    End Property

    Protected Sub ImgIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgIndietro.Click
        Response.Redirect("SceltaIstatERP.aspx")
    End Sub

    Private Sub CalcolaIstat()
        Try
            Dim Str As String = ""
            Dim INDICE_ATTUALE As Double = 0
            Dim INDICE_PRECEDENTE As Double = 0
            Dim VAR_75 As Double = 0
            Dim VAR_100 As Double = 0
            Dim INDICE As Double = 0
            Dim AUMENTO As Double = 0
            Dim I As Integer = 0
            Dim CANONE_CORRENTE As Double = 0
            Dim dt As New Data.DataTable
            Dim sCalcolo As String = ""
            Dim CanoneCorrente As Double = 0
            Dim fileName As String = ""

            Dim ISTAT_1_PR As Double = 0
            Dim ISTAT_2_PR As Double = 0
            Dim ISTAT_1_AC As Double = 0
            Dim ISTAT_2_AC As Double = 0
            Dim ISTAT_1_PE As Double = 0
            Dim ISTAT_2_PE As Double = 0
            Dim ISTAT_1_DE As Double = 0
            Dim ISTAT_2_DE As Double = 0
            Dim ICI_1_2 As Double = 0
            Dim ICI_3_4 As Double = 0
            Dim ICI_5_6 As Double = 0
            Dim ICI_7 As Double = 0
            Dim LimitePensioneAU As Double = 0

            Dim ISTAT_APPLICATA As Double = 0

            Dim CanoneMIN As Double = 0
            Dim CANONE_SOPP As Double = 0
            Dim CANONE_CLASSE As Double = 0
            Dim CANONE_CLASSE_ISTAT As Double = 0
            Dim TIPOCANONE As String = ""
            Dim CanoneErpRegime As Double = 0
            Dim NewiD As Long = 0
            Dim Identificativo As String = Format(Now, "yyyyMMddHHmmss")

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()

            

            Dim IDAU As Long = 0
            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE STATO<>2 ORDER BY ID DESC"
            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX.Read() Then
                IDAU = myReaderX("ID")
            End If
            myReaderX.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI_PARAMETRI WHERE ID_AU=" & IDAU
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                ISTAT_2_PR = par.IfNull(myReader("ISTAT_2_PR"), 0)
                ISTAT_2_AC = par.IfNull(myReader("ISTAT_2_AC"), 0)
                ISTAT_2_PE = par.IfNull(myReader("ISTAT_2_PE"), 0)
                ISTAT_2_DE = par.IfNull(myReader("ISTAT_2_DE"), 0)


                LimitePensioneAU = par.IfNull(myReader("limite_pensione"), 0)

            End If
            myReader.Close()

            If ISTAT_2_PR = 0 Or ISTAT_2_PE = 0 Or ISTAT_2_DE = 0 Or ISTAT_2_AC = 0 Then
                Response.Write("<script>alert('Attenzione, verificare che siano state inserite le percentuali istat relative al secondo anno AU');</script>")

            Else


                dt = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)
                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"


                

                For Each row In dt.Rows
                    par.cmd.CommandText = "SELECT CANONI_EC.* FROM SISCOM_MI.CANONI_EC,SISCOM_MI.rapporti_utenza WHERE RAPPORTI_UTENZA.IMP_CANONE_INIZIALE=CANONI_EC.CANONE AND RAPPORTI_UTENZA.ID=CANONI_EC.ID_CONTRATTO AND TIPO_PROVENIENZA<>7 AND ID_DICHIARAZIONE IS NOT NULL AND ID_CONTRATTO=" & row.Item("ID") & " ORDER BY DATA_CALCOLO DESC"
                    Dim myReaderRU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRU.Read Then
                        If par.IfNull(myReaderRU("TIPO_CANONE_APP"), "") = "CLASSE" Then

                            CanoneMIN = par.IfNull(myReaderRU("CANONE_MINIMO_AREA"), "0") * 12
                            CANONE_SOPP = par.IfNull(myReaderRU("CANONE_SOPPORTABILE"), 0)
                            CANONE_CLASSE = par.IfNull(myReaderRU("CANONE_CLASSE"), 0)

                            Select Case par.IfNull(myReaderRU("ID_AREA_ECONOMICA"), 0)
                                Case 1
                                    CANONE_CLASSE_ISTAT = (CANONE_CLASSE + (CANONE_CLASSE * CDbl(ISTAT_2_PR) / 100)) '* par.IfNull(myReaderRU("COEFF_NUCLEO_FAM"), 0)
                                    ISTAT_APPLICATA = ISTAT_2_PR
                                Case 2
                                    CANONE_CLASSE_ISTAT = (CANONE_CLASSE + (CANONE_CLASSE * CDbl(ISTAT_2_AC) / 100)) '* par.IfNull(myReaderRU("COEFF_NUCLEO_FAM"), 0)
                                    ISTAT_APPLICATA = ISTAT_2_AC
                                Case 3
                                    CANONE_CLASSE_ISTAT = (CANONE_CLASSE + (CANONE_CLASSE * CDbl(ISTAT_2_PE) / 100)) '* par.IfNull(myReaderRU("COEFF_NUCLEO_FAM"), 0)
                                    ISTAT_APPLICATA = ISTAT_2_PE
                                Case 4
                                    CANONE_CLASSE_ISTAT = (CANONE_CLASSE + (CANONE_CLASSE * CDbl(ISTAT_2_DE) / 100)) '* par.IfNull(myReaderRU("COEFF_NUCLEO_FAM"), 0)
                                    ISTAT_APPLICATA = ISTAT_2_DE
                            End Select

                            TIPOCANONE = "SOPPORTABILE"

                            If CDbl(CANONE_SOPP) < CDbl(CANONE_CLASSE_ISTAT) Then
                                If CDbl(CANONE_SOPP) > CDbl(CanoneMIN) Then
                                    CanoneErpRegime = CANONE_SOPP
                                    TIPOCANONE = "SOPPORTABILE"
                                Else
                                    CanoneErpRegime = CanoneMIN
                                    TIPOCANONE = "MINIMO AREA"
                                End If
                            Else
                                If CDbl(CANONE_CLASSE_ISTAT) > CDbl(CanoneMIN) Then
                                    CanoneErpRegime = CANONE_CLASSE_ISTAT
                                    TIPOCANONE = "CLASSE"
                                Else
                                    CanoneErpRegime = CanoneMIN
                                    TIPOCANONE = "MINIMO AREA"
                                End If
                            End If

                            If CanoneErpRegime - row.Item("canone_attuale") > 0 Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC SELECT * FROM SISCOM_MI.CANONI_EC WHERE ID=" & par.IfNull(myReaderRU("ID"), 0)
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO=" & row.Item("ID")
                                Dim myReaderEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderEC.Read Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.CANONI_EC SET DATA_CALCOLO='" & Format(Now, "yyyyMMddHHmmss") & "',TIPO_PROVENIENZA=7,TIPO_CANONE_APP='" & TIPOCANONE & "',CANONE=" & par.VirgoleInPunti(Format(CanoneErpRegime, "0.00")) & ",CANONE_CLASSE_ISTAT=" & par.VirgoleInPunti(Format(CANONE_CLASSE_ISTAT, "0.00")) & ",PERC_ISTAT_APPLICATA=" & par.VirgoleInPunti(ISTAT_APPLICATA) & ",INIZIO_VALIDITA_CAN='" & Mid(sPeriodo, 1, 4) & "0101',FINE_VALIDITA_CAN='" & Mid(sPeriodo, 1, 4) & "1231' WHERE ID=" & myReaderEC(0)
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_CANONE_INIZIALE=" & par.VirgoleInPunti(Format(CanoneErpRegime, "0.00")) & " WHERE ID=" & row.Item("ID")
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & row.Item(0) & "," & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F09', 'APPLICAZIONE ISTAT " & Mid(sPeriodo, 1, 4) & ", CANONE ATTUALE EURO " & CanoneErpRegime & " CANONE PRECEDENTE EURO " & row.Item("canone_attuale") & "')"
                                    par.cmd.ExecuteNonQuery()


                                    If Len(sPeriodo) = 4 Then sPeriodo = sPeriodo & "00"
                                    par.cmd.CommandText = "Insert into SISCOM_MI.ADEGUAMENTO_ISTAT (ID, ID_CONTRATTO, DATA_AGG_INIZIO, DATA_AGG_FINE, " _
                                                   & "IMPORTO_CANONE_AGG, COD_TR, IMPORTO_TR_AGG, NRO_PROT_LETTERA, DATA_LETTERA, " _
                                                            & "IMPORTO_CANONE_INIZIALE, INDICE_INIZIALE, INDICE_FINALE, VAR_ISTAT, BASE_INIZIALE, BASE_FINALE, " _
                                                            & "COEF_RACCORDO, ANNO_MESE,IDENTIFICATIVO) Values " _
                                                            & "(SISCOM_MI.SEQ_ADEGUAMENTO_ISTAT.NEXTVAL, " & row.Item("ID") & ", '" & Mid(sPeriodo, 1, 4) & "0101' , '" _
                                                            & Mid(sPeriodo, 1, 4) & "1231', " & par.VirgoleInPunti(Format(CanoneErpRegime, "0.00")) & " , " _
                                                            & "NULL, " & par.VirgoleInPunti(Format(CanoneErpRegime - row.Item("canone_attuale"), "0.00")) & ", '', '', " _
                                                            & par.VirgoleInPunti(row.Item("canone_attuale")) & " , " _
                                                            & "NULL" & ", " & "0" _
                                                            & ", " & par.VirgoleInPunti(Format(ISTAT_APPLICATA, "0.0000")) & ", NULL, " & par.VirgoleInPunti(0) & " , " _
                                                            & "NULL, '" & sPeriodo & "','" & Identificativo & "')"
                                    par.cmd.ExecuteNonQuery()

                                End If
                                myReaderEC.Close()
                            End If
                            
                        End If
                    End If
                    myReaderRU.Close()

                    I = I + 1

                    progress.PrimaryTotal = Total
                    progress.PrimaryValue = I
                    progress.PrimaryPercent = Int((I * 100) / (Total))
                    progress.CurrentOperationText = " " & I.ToString() & " di " & Total
                Next
            End If
            



            If I > 0 Then
                Str = "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as INIZIO,to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as FINE,RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as DECORRENZA,PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS IMP_CANONE_INIZIALE,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM SISCOM_MI.ADEGUAMENTO_ISTAT,SISCOM_MI.RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ADEGUAMENTO_ISTAT.identificativo='" & Identificativo & "' "
                HttpContext.Current.Session.Add("BB", Str)
                Response.Write("<script>window.open('SceltaIstatErp2.aspx');</script>")
                Response.Write("<script>location.href='pagina_home.aspx';</script>")
            End If


            par.myTrans.Commit()
            par.OracleConn.Close()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Applicazione ISTAT - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub imgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        CalcolaIstat()
        'Try
        '    Dim Str As String = ""
        '    Dim INDICE_ATTUALE As Double = 0
        '    Dim INDICE_PRECEDENTE As Double = 0
        '    Dim VAR_75 As Double = 0
        '    Dim VAR_100 As Double = 0
        '    Dim INDICE As Double = 0
        '    Dim AUMENTO As Double = 0
        '    Dim I As Integer = 0
        '    Dim CANONE_CORRENTE As Double = 0
        '    Dim dt As New Data.DataTable
        '    Dim sCalcolo As String = ""
        '    Dim CanoneCorrente As Double = 0
        '    Dim fileName As String = ""





        '    par.OracleConn.Open()
        '    par.SettaCommand(par)
        '    par.myTrans = par.OracleConn.BeginTransaction()

        '    Dim base_finale As String = "0"
        '    Dim indice_finale As String = "0"

        '    par.cmd.CommandText = "select * from variazioni_istat where substr(data_validita,5,2)='12' order by id desc"
        '    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReaderX.Read Then
        '        base_finale = par.IfNull(myReaderX("base_indice"), "")
        '        indice_finale = par.IfNull(myReaderX("indice_nazionale"), "")
        '    End If
        '    myReaderX.Close()

        '    Dim row As System.Data.DataRow
        '    Dim Identificativo As String = Format(Now, "yyyyMMddHHmmss")

        '    GeneraTabellaCalcolo()
        '    dt = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)

        '    Dim Total As Integer = dt.Rows.Count
        '    Dim progress As RadProgressContext = RadProgressContext.Current
        '    progress.Speed = "N/A"

        '    For Each row In dt.Rows
        '        CANONE_CORRENTE = 0
        '        fileName = row.Item("ID") & "_" & Format(Now, "yyyyMMddHHmmss") & ".TXT"
        '        'CONSIDERANDO CHE L'ULTIMA RIGA INSERITA NEI DETTAGLI E' SEMPRE UGUALE AL CANONE ATTUALE, ESCLUDENDO COMUNQUE AGGIORNAMENTO ISTAT, RICAVO L'ORIGINE DATI PER IL CALCOLO DEL NUOVO CANONE
        '        'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC,SISCOM_MI.rapporti_utenza WHERE RAPPORTI_UTENZA.IMP_CANONE_INIZIALE=CANONI_EC.CANONE AND RAPPORTI_UTENZA.ID=CANONI_EC.ID_CONTRATTO AND TIPO_PROVENIENZA<>5 AND ID_DICHIARAZIONE IS NOT NULL AND ID_CONTRATTO=" & row.Item("ID") & " ORDER BY DATA_CALCOLO DESC"
        '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC,SISCOM_MI.rapporti_utenza WHERE RAPPORTI_UTENZA.IMP_CANONE_INIZIALE=CANONI_EC.CANONE AND RAPPORTI_UTENZA.ID=CANONI_EC.ID_CONTRATTO AND TIPO_PROVENIENZA<>5 AND ID_DICHIARAZIONE IS NOT NULL AND ID_CONTRATTO=" & row.Item("ID") & " ORDER BY DATA_CALCOLO DESC"
        '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '        If myReader.Read Then
        '            'PRENDO I DATI IN BASE ALL'ORIGINE
        '            Select Case par.IfNull(myReader("TIPO_PROVENIENZA"), "")
        '                Case "1" 'GESTIONE LOCATARI
        '                    sCalcolo = par.CalcolaCanone27(par.IfNull(myReader("id_dichiarazione"), -1), 4, row.Item("ID_UNITA"), myReader("COD_CONTRATTO"), myReader("IMP_CANONE_INIZIALE"), VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, annoSitEconom)
        '                Case "3" 'BANDO ERP 

        '                Case "4" 'CONTRATTI
        '                    sCalcolo = par.CalcolaCanone27(par.IfNull(myReader("id_dichiarazione"), -1), 20, row.Item("ID_UNITA"), myReader("ID_CONTRATTO"), False, 0, TabCalcolo)
        '                Case Else 'SARANNO AU, IL CUI ID AUMENTARA' NEL TEMPO
        '                    sCalcolo = par.CalcolaCanone27(par.IfNull(myReader("id_dichiarazione"), -1), 1, row.Item("ID_UNITA"), myReader("ID_CONTRATTO"), False, 0, TabCalcolo)
        '            End Select


        '            RiempiProspetto(TabCalcolo, row.Item("INTESTATARIO"), fileName)
        '            HttpContext.Current.Session.Add("TabCalcolo", TabCalcolo)
        '            CanoneCorrente = CDbl(TabCalcolo.Rows(0).Item("CANONEDAAPPLICARE"))

        '            Dim DataValidita As String = Mid(sPeriodo, 1, 4) & "0101"
        '            If DataValidita = "" Then
        '                DataValidita = Format(Now, "yyyyMMdd")
        '            End If

        '            If CanoneCorrente - par.IfNull(myReader("imp_canone_iniziale"), 0) > 0 Then
        '                par.cmd.CommandText = "INSERT INTO RAPPORTI_UTENZA_DETT_CANONI (ID_CONTRATTO,DATA_CALCOLO,TESTO,ID_AREA_ECONOMICA,SOTTO_AREA,DEM,DESCR_DEM,ZONA,DESCR_ZONA,SUP_CONVENZIONALE,COSTO_BASE,DESCR_COSTOBASE,PIANO,DESCR_PIANO,PRESENTE_ASCENSORE,CONSERVAZIONE,DESCR_CONSERVAZIONE,VETUSTA,DESCR_VETUSTA,CATASTALE, " _
        '                                    & "DESCR_CATASTALE,COSTO_UNITARIO,VALORE_LOCATIVO,COEFF_MAX,CANONE_OGGETTIVO,PERC_ISTAT_APPLICATA,EQUO_CANONE,ANNO_REDDITI,FIGLI_CARICO,NUM_COMP,DETRAZIONI,REDDITI_DIP,REDDITI_AUT,REDDITI_PEN,REDDITI_OCC,REDDITI_DED,REDDITI_RAD, " _
        '                                    & "LIMITE_PENSIONI,REDDITO_FISCALE,REDDITO_CONVENZIONALE,PREVALENTE_DIPENDENTE,PERCENTUALE_APPLICATA,LIMITE_INF,LIMITE_SUP,DESCRIZIONE_LIMITE_INF,DESCRIZIONE_LIMITE_SUP,CANONE_ANNUO,IMP_NUOVA_ASSEGNAZIONE,CANONE_DA_APPLICARE, " _
        '                                    & "LIMITE_DEC_SUPERATO,NOTE,NON_RISPONDENTE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT,INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN,TIPO_PROVENIENZA,ANNO_RIF_REDDITO,DETTAGLI_SUP_CONV,MAGGIORI_65,MINORI_15,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_IMMOBILIARI,FAPES,PERC_FAPES,ART15,COEFF_FASCIA,CANONE_FASCIA,INC_FASCIA,CANONE_MAX, DESCR_CANONE_MINIMO,CANONE_MINIMO,DESCR_RIDUZIONE,PERC_RIDUZIONE,VAL_RIDUZIONE,ID) " _
        '                                    & "VALUES " _
        '                                    & "( " _
        '                                    & myReader("ID_CONTRATTO") & "," _
        '                                    & "'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        '                                    & "EMPTY_BLOB()," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("AREA"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("SOTTO_AREA") & "'," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("INDICEDEMOGRAFIA"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONEDEMOGRAFIA") & "'," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("INDICEZONA"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONEZONA") & "'," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("SUPCONVENZIONALE"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("INDICECOSTOBASE"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONECOSTOBASE") & "'," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("INDICEPIANO"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONEPIANO") & "'," _
        '                                    & par.IfEmpty(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PRESENTE_ASCENSORE"), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("INDICECONSERVAZIONE"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONECONSERVAZIONE") & "'," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("INDICEVETUSTA"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONEVETUSTA") & "'," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("INDICECATASTALE"))), "NULL") & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONECATASTALE") & "'," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("COSTOUNITARIO"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("VALORELOCATIVO"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("COEFFMAX"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("CANONEOGGETTIVO"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PERCENTUALEISTAT"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("EQUOCANONE"))), "NULL") & "," _
        '                                    & par.IfEmpty(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("ANNOREDDITUALE"), "NULL") & "," _
        '                                    & par.IfEmpty(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("FIGLICARICO"), "NULL") & "," _
        '                                    & par.IfEmpty(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("COMPONENTI"), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DETRAZIONI"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDDITO_DP"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDDITO_RA"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDDITO_RP"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDDITO_RO"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("ONERI_OD"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDDITO_RAD"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PENSIONE"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDDITOFISCALE"))), "NULL") & "," _
        '                                    & par.IfEmpty(par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDDITOCONVENZIONALE"))), "NULL") & "," _
        '                                    & Replace(UCase(Replace(UCase(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PREVALENTEDIPENDENTE")), "NO", "0")), "SI", "1") & "," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PERCENTUALE"))) & "," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("LIMITE_INF"))) & "," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("LIMITE_SUP"))) & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONE_LIMITE_INF") & "'," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONE_LIMITE_SUP") & "'," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("CANONEANNUO"))) & "," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("NUOVAASSEGNAZIONE"))) & "," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("CANONEDAAPPLICARE"))) & "," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("LIMITE_DEC_SUPERATO"))) & "," _
        '                                    & "'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("NOTE") & "'," _
        '                                    & "0,NULL," _
        '                                    & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("CANONECORRENTE"))) & ",NULL,NULL," _
        '                                    & "'" & DataValidita & "'," _
        '                                    & "'" & Mid(DataValidita, 1, 4) + 1 & "1231" & "',5," & par.IfEmpty(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("ANNOREDDITUALE"), "NULL") & ",'" & par.PulisciStrSql(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DETTAGLI_CONV")) _
        '                                    & "'," & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("MAGGIORI_65") & "," & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("MINORI_15") & "," & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("NUM_COMP_66") & "," _
        '                                    & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("NUM_COMP_100") & "," & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("NUM_COMP_100_CON") & ",'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("REDD_IMMOBILIARI") _
        '                                    & "','" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("FAPES") & "'," & par.IfEmpty(par.VirgoleInPunti(Replace(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PERCFAPES"), "%", "")), "NULL") & ",'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("ART5") _
        '                                    & "','" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PERCFASCIA") & "'," & par.IfEmpty(par.VirgoleInPunti(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("CANONEFASCIA")), "NULL") _
        '                                    & ",'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PERCINCIDENZA") & "'," & par.IfEmpty(par.VirgoleInPunti(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("MAXCANONEANNUO")), "NULL") _
        '                                    & ",'" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCRIZIONE_LIMITE_INF") & "','" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("LIMITE_INF") & "','" & TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("DESCR_RIDUZIONE") _
        '                                    & "'," & par.IfEmpty(par.VirgoleInPunti(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("PERC_RIDUZIONE")), "NULL") & "," & par.IfEmpty(par.VirgoleInPunti(TabCalcolo.Rows(TabCalcolo.Rows.Count - 1).Item("VAL_RIDUZIONE")), "NULL") & ",SEQ_RAPPORTI_UTENZA_DETT_C.NEXTVAL)"
        '                par.cmd.ExecuteNonQuery()

        '                If Len(sPeriodo) = 4 Then sPeriodo = sPeriodo & "00"
        '                par.cmd.CommandText = "Insert into ADEGUAMENTO_ISTAT (ID, ID_CONTRATTO, DATA_AGG_INIZIO, DATA_AGG_FINE, " _
        '                               & "IMPORTO_CANONE_AGG, COD_TR, IMPORTO_TR_AGG, NRO_PROT_LETTERA, DATA_LETTERA, " _
        '                                        & "IMPORTO_CANONE_INIZIALE, INDICE_INIZIALE, INDICE_FINALE, VAR_ISTAT, BASE_INIZIALE, BASE_FINALE, " _
        '                                        & "COEF_RACCORDO, ANNO_MESE,IDENTIFICATIVO) Values " _
        '                                        & "(SEQ_ADEGUAMENTO_ISTAT.NEXTVAL, " & row.Item("ID") & ", '" & Mid(sPeriodo, 1, 4) & "0101' , '" _
        '                                        & Mid(sPeriodo, 1, 4) & "1231', " & par.VirgoleInPunti(CanoneCorrente) & " , " _
        '                                        & "NULL, " & par.VirgoleInPunti(CanoneCorrente - par.IfNull(row.Item("CANONE_ATTUALE"), 0)) & ", '', '', " & par.VirgoleInPunti(par.IfNull(row.Item("CANONE_ATTUALE"), 0)) & " , " _
        '                                        & "NULL" & ", " & par.VirgoleInPunti(indice_finale) _
        '                                        & ", " & par.VirgoleInPunti(CDbl(TabCalcolo.Rows(0).Item("PERCENTUALEISTAT"))) & ", NULL, " & par.VirgoleInPunti(base_finale) & " , " _
        '                                        & "NULL, '" & sPeriodo & "','" & Identificativo & "')"
        '                par.cmd.ExecuteNonQuery()

        '                par.cmd.CommandText = "INSERT INTO EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '                    & "VALUES (" & row.Item("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        '                    & "'F09','Aggiornato ISTAT " & Mid(sPeriodo, 1, 4) & "')"
        '                par.cmd.ExecuteNonQuery()

        '                par.cmd.CommandText = "UPDATE RAPPORTI_UTENZA SET IMP_CANONE_ATTUALE=" & par.VirgoleInPunti(Format(CanoneCorrente, "0.00")) & ",IMP_CANONE_INIZIALE=" & par.VirgoleInPunti(Format(CanoneCorrente, "0.00")) & " WHERE ID=" & row.Item("ID")
        '                par.cmd.ExecuteNonQuery()
        '            End If


        '            TabCalcolo.Rows.Clear()
        '            I = I + 1

        '            progress.PrimaryTotal = Total
        '            progress.PrimaryValue = I
        '            progress.PrimaryPercent = Int((I * 100) / (Total))
        '            progress.CurrentOperationText = " " & I.ToString() & " di " & Total

        '        End If
        '        myReader.Close()

        '    Next



        '    If I > 0 Then
        '        'Str = "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as ""INIZIO"",to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as ""FINE"",RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS IMP_CANONE_INIZIALE,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM ADEGUAMENTO_ISTAT,RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ADEGUAMENTO_ISTAT.identificativo='" & Identificativo & "' "
        '        Str = "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as INIZIO,to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as FINE,RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as DECORRENZA,PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS IMP_CANONE_INIZIALE,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM ADEGUAMENTO_ISTAT,RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ADEGUAMENTO_ISTAT.identificativo='" & Identificativo & "' "
        '        HttpContext.Current.Session.Add("BB", Str)
        '        Response.Write("<script>window.open('SceltaIstatErp2.aspx');</script>")
        '        Response.Write("<script>location.href='pagina_home.aspx';</script>")
        '    End If


        '    par.myTrans.Commit()
        '    par.OracleConn.Close()

        'Catch ex As Exception
        '    par.myTrans.Rollback()
        '    par.OracleConn.Close()
        '    Session.Add("ERRORE", "Provenienza:Applicazione ISTAT - " & ex.Message)
        '    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        'End Try
    End Sub



End Class
