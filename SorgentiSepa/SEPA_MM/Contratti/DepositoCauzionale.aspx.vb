
Partial Class Contratti_DepositoCauzionale
    Inherits PageSetIdMode
    Dim par As New CM.Global()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            CaricaDatiRimborso()
            CaricaInfoInteressi()
            CaricaStoricoDep()
        End If
    End Sub

    Public Property idAnagrafe() As Long
        Get
            If Not (ViewState("par_idAnagrafe") Is Nothing) Then
                Return CLng(ViewState("par_idAnagrafe"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idAnagrafe") = value
        End Set

    End Property

    Private Function CaricaDatiRimborso()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "select  * from SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ WHERE ID_CONTRATTO=" & Request.QueryString("IDC")
            'Dim lettoreA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If lettoreA.Read Then
            '    txtImportoDep.Text = par.IfNull(lettoreA("CREDITO"), "")
            '    If par.IfNull(lettoreA("DATA_MANDATO"), "") <> "" Then
            '        lblInFase.Text = ""
            '    Else
            '        lblInFase.Text = "In fase di rest."
            '    End If
            '    txtDataCert.Text = par.FormattaData(par.IfNull(lettoreA("DATA_CERT_PAG"), ""))
            '    txtNumCDP.Text = par.IfNull(lettoreA("NUM_CDP"), "")
            '    txtAnnoCDP.Text = par.IfNull(lettoreA("ANNO_CDP"), "")

            '    txtDataMandato.Text = par.FormattaData(par.IfNull(lettoreA("DATA_MANDATO"), ""))
            '    txtNumMandato.Text = par.IfNull(lettoreA("NUM_MANDATO"), "")
            '    txtAnnoMandato.Text = par.IfNull(lettoreA("ANNO_MANDATO"), "")


            'End If
            'lettoreA.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & Request.QueryString("IDC") & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
            Dim myReaderInt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderInt.Read Then
                idAnagrafe = par.IfNull(myReaderInt("ID_ANAGRAFICA"), 0)
            End If
            myReaderInt.Close()

            par.cmd.CommandText = "select  RAPPORTI_UTENZA_DEP_CAUZ.*,nvl(bol_bollette.id_bolletta_storno,0) AS id_bolletta_storno from SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ,SISCOM_MI.BOL_BOLLETTE WHERE RAPPORTI_UTENZA_DEP_CAUZ.ID_CONTRATTO=" & Request.QueryString("IDC") & " AND ID_BOLLETTA=BOL_BOLLETTE.ID /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/"
            Dim lettoreA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettoreA.Read Then
                If par.IfNull(lettoreA("ID_BOLLETTA_STORNO"), "0") = "0" Then
                    txtImportoDep.Text = par.IfNull(lettoreA("CREDITO"), "")
                    If par.IfNull(lettoreA("DATA_MANDATO"), "") <> "" Then
                        lblInFase.Text = ""
                    Else
                        lblInFase.Text = "In fase di rest."
                    End If
                    txtDataCert.Text = par.FormattaData(par.IfNull(lettoreA("DATA_CERT_PAG"), ""))
                    txtNumCDP.Text = par.IfNull(lettoreA("NUM_CDP"), "")
                    txtAnnoCDP.Text = par.IfNull(lettoreA("ANNO_CDP"), "")

                    txtDataMandato.Text = par.FormattaData(par.IfNull(lettoreA("DATA_MANDATO"), ""))
                    txtNumMandato.Text = par.IfNull(lettoreA("NUM_MANDATO"), "")
                    txtAnnoMandato.Text = par.IfNull(lettoreA("ANNO_MANDATO"), "")
                Else
                    lblInFase.Text = ""
                    txtDataCert.Text = ""
                    txtNumCDP.Text = ""
                    txtAnnoCDP.Text = ""
                    txtDataMandato.Text = ""
                    txtNumMandato.Text = ""
                    txtAnnoMandato.Text = ""
                End If
            End If
            lettoreA.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Private Sub CaricaInfoInteressi()
        'Try
        '    Dim RIGA As System.Data.DataRow
        '    Dim RIGA1 As System.Data.DataRow
        '    Dim BollStornata As Long = 0

        '    par.OracleConn.Open()
        '    par.cmd = par.OracleConn.CreateCommand()

        '    par.cmd.CommandText = "SELECT id_adeguamento as id,TO_CHAR(TO_DATE(DAL,'yyyymmdd'),'dd/mm/yyyy') AS DAL,TO_CHAR(TO_DATE(AL,'yyyymmdd'),'dd/mm/yyyy') AS AL,GIORNI,TASSO,adeguamento_interessi_voci.importo,fl_applicato FROM siscom_mi.rapporti_utenza,siscom_mi.adeguamento_interessi,siscom_mi.adeguamento_interessi_voci WHERE rapporti_utenza.ID = adeguamento_interessi.id_contratto " _
        '        & "AND adeguamento_interessi_voci.id_adeguamento = adeguamento_interessi.ID AND id_contratto=" & Request.QueryString("IDC") & " order by adeguamento_interessi_voci.dal asc"
        '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '    Dim dt As New Data.DataTable()

        '    Dim dtRisultato As New Data.DataTable()
        '    dtRisultato.Columns.Add("ID")
        '    dtRisultato.Columns.Add("DAL")
        '    dtRisultato.Columns.Add("AL")
        '    dtRisultato.Columns.Add("GIORNI")
        '    dtRisultato.Columns.Add("TASSO")
        '    dtRisultato.Columns.Add("IMPORTO")
        '    dtRisultato.Columns.Add("RESTITUITI")

        '    Dim dt2 As New Data.DataTable()
        '    dt2.Columns.Add("ID", Type.GetType("System.String"))
        '    dt2.Columns.Add("DAL", Type.GetType("System.String"))
        '    dt2.Columns.Add("AL", Type.GetType("System.String"))
        '    dt2.Columns.Add("GIORNI", Type.GetType("System.String"))
        '    dt2.Columns.Add("TASSO", Type.GetType("System.String"))
        '    dt2.Columns.Add("IMPORTO", Type.GetType("System.String"))
        '    dt2.Columns.Add("RESTITUITI", Type.GetType("System.String"))

        '    da.Fill(dt)
        '    da.Dispose()

        '    Dim importoInteresse As Decimal = 0
        '    Dim idBolletta As Long = 0
        '    Dim numbolletta As String = ""




        '    If dt.Rows.Count = 0 Then
        '        RIGA = dt2.NewRow()
        '        RIGA.Item("ID") = -1
        '        RIGA.Item("DAL") = "&nbsp"
        '        RIGA.Item("AL") = "&nbsp"
        '        RIGA.Item("GIORNI") = "&nbsp"
        '        RIGA.Item("TASSO") = "&nbsp"
        '        RIGA.Item("IMPORTO") = "&nbsp"
        '        RIGA.Item("RESTITUITI") = "&nbsp"
        '        dt2.Rows.Add(RIGA)
        '        DataGridDepCauz.DataSource = dt2
        '        lblNoInteressi.Text = "<img id='Img1' src='../IMG/Alert.gif' style='height: 17px;' alt='alert' /> (Interessi non calcolati)"
        '    Else
        '        For Each row As Data.DataRow In dt.Rows

        '            'CERCO L'INTERESSE SE APPLICATO
        '            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ADEGUAMENTO_INTERESSI WHERE ID=" & row.Item("ID")
        '            Dim lettoreA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '            If lettoreA.Read Then
        '                If par.IfNull(lettoreA("FL_APPLICATO"), "") = "1" Then
        '                    importoInteresse = par.IfNull(lettoreA("IMPORTO"), 0)
        '                End If
        '            End If
        '            lettoreA.Close()
        '            BollStornata = 0
        '            numbolletta = ""
        '            If importoInteresse <> 0 Then
        '                par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") & " and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette_voci.id_voce=15 and abs(bol_bollette_voci.importo)=" & par.VirgoleInPunti(importoInteresse)
        '                lettoreA = par.cmd.ExecuteReader()
        '                If lettoreA.Read Then
        '                    idBolletta = par.IfNull(lettoreA("ID_BOLLETTA"), "")
        '                    numbolletta = par.IfNull(lettoreA("NUM_BOLLETTA"), "")
        '                    BollStornata = par.IfNull(lettoreA("id_bolletta_storno"), 0)
        '                Else
        '                    par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") & " and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=3 and bol_bollette_voci.id_voce in (350,351,352,353,354,355,356) and abs(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("importo"))
        '                    Dim lettoreB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '                    If lettoreB.Read Then
        '                        idBolletta = par.IfNull(lettoreB("ID_BOLLETTA"), "")
        '                        numbolletta = par.IfNull(lettoreB("NUM_BOLLETTA"), "")
        '                        BollStornata = par.IfNull(lettoreB("id_bolletta_storno"), 0)
        '                    End If
        '                    lettoreB.Close()

        '                End If
        '                lettoreA.Close()
        '            End If

        '            RIGA1 = dtRisultato.NewRow()
        '            RIGA1.Item("ID") = row.Item("ID")
        '            RIGA1.Item("DAL") = par.IfNull(row.Item("DAL"), "")
        '            RIGA1.Item("AL") = par.IfNull(row.Item("AL"), "")
        '            RIGA1.Item("GIORNI") = par.IfNull(row.Item("GIORNI"), "")
        '            RIGA1.Item("TASSO") = par.IfNull(row.Item("TASSO"), "")
        '            RIGA1.Item("IMPORTO") = par.IfNull(row.Item("IMPORTO"), "")
        '            If RIGA1.Item("IMPORTO") > 0 Then
        '                If row.Item("FL_APPLICATO") = "1" And numbolletta <> "" Then
        '                    If BollStornata = 0 Then
        '                        RIGA1.Item("RESTITUITI") = "SI - <i>N.ro bolletta</i>: <a href=""javascript:window.open('../Contabilita/DettaglioBolletta.aspx?IDCONT=" & Request.QueryString("IDC") & "&IDBollCOP=" & idBolletta & "&IDANA=" & idAnagrafe & "','DettBollettaDepos','');void(0);"">" & numbolletta & "</a>"
        '                    Else
        '                        RIGA1.Item("RESTITUITI") = "NO - <i>bolletta STORNATA</i>: <a href=""javascript:window.open('../Contabilita/DettaglioBolletta.aspx?IDCONT=" & Request.QueryString("IDC") & "&IDBollCOP=" & idBolletta & "&IDANA=" & idAnagrafe & "','DettBollettaDepos','');void(0);"">" & numbolletta & "</a>"
        '                    End If
        '                Else
        '                    RIGA1.Item("RESTITUITI") = "NO"
        '                End If
        '            Else
        '                RIGA1.Item("RESTITUITI") = "NO-Importo=0"
        '            End If
        '            dtRisultato.Rows.Add(RIGA1)
        '        Next
        '        DataGridDepCauz.DataSource = dtRisultato
        '    End If
        '    DataGridDepCauz.DataBind()

        '    par.cmd.CommandText = "SELECT sum(adeguamento_interessi_voci.importo) as totimporto FROM siscom_mi.rapporti_utenza,siscom_mi.adeguamento_interessi,siscom_mi.adeguamento_interessi_voci WHERE rapporti_utenza.ID = adeguamento_interessi.id_contratto " _
        '        & "AND adeguamento_interessi_voci.id_adeguamento = adeguamento_interessi.ID AND id_contratto=" & Request.QueryString("IDC")
        '    Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If lettore0.Read Then
        '        lblTotImp.Text = Format(par.IfNull(lettore0("totimporto"), 0), "##,##0.00") & " €."
        '    End If
        '    lettore0.Close()

        '    par.cmd.CommandText = "SELECT * FROM siscom_mi.adeguamento_interessi where id_contratto=" & Request.QueryString("IDC") & " order by data desc"
        '    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If lettore.Read Then
        '        lblDataUltima.Text = par.FormattaData(par.IfNull(lettore("DATA"), ""))
        '        'lblTotImp.Text = Format(par.IfNull(lettore("IMPORTO"), 0), "##,##0.00") & " €."
        '    Else
        '        lblDataUltima.Text = "---"
        '        lblTotImp.Text = "---"
        '    End If
        '    lettore.Close()

        '    par.cmd.CommandText = "SELECT * FROM siscom_mi.bol_bollette where id_contratto=" & Request.QueryString("IDC") & " AND ID_TIPO=9 and nvl(data_scadenza,'29991231')<>'29991231' AND ID_BOLLETTA_STORNO IS NULL AND (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) order BY ID DESC"
        '    lettore = par.cmd.ExecuteReader()
        '    If lettore.Read Then
        '        txtDataMAV.Text = par.FormattaData(par.IfNull(lettore("DATA_PAGAMENTO"), ""))
        '        txtDataValuta.Text = par.FormattaData(par.IfNull(lettore("DATA_VALUTA"), ""))
        '        txtImportoTot.Text = Format(par.IfNull(lettore("IMPORTO_TOTALE"), 0), "##,##0.00")
        '        txtImpPagato.Text = Format(par.IfNull(lettore("IMPORTO_PAGATO"), 0), "##,##0.00")
        '        txtDataEmiss.Text = par.FormattaData(par.IfNull(lettore("DATA_EMISSIONE"), ""))
        '        txtDataScad.Text = par.FormattaData(par.IfNull(lettore("DATA_SCADENZA"), ""))
        '        txtDataCostituzione.Text = par.FormattaData(par.IfNull(lettore("DATA_PAGAMENTO"), ""))
        '        'Else
        '        '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.STORICO_DEP_CAUZIONALE WHERE FL_ORIGINALE=1 AND ID_CONTRATTO=" & Request.QueryString("IDC")
        '        '    Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '        '    If lettore2.Read Then
        '        '        txtImportoTot.Text = Format(par.IfNull(lettore2("IMPORTO"), 0), "##,##0.00")
        '        '        txtDataCostituzione.Text = par.FormattaData(par.IfNull(lettore2("DATA_COSTITUZIONE"), ""))
        '        '        txtDataEmiss.Text = par.FormattaData(par.IfNull(lettore2("DATA"), ""))
        '        '        txtImpPagato.Text = Format(par.IfNull(lettore2("IMPORTO"), 0), "##,##0.00")
        '        '    End If
        '        '    lettore2.Close()
        '        '    If txtImportoTot.Text = "" Then
        '        '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & Request.QueryString("IDC")
        '        '        lettore2 = par.cmd.ExecuteReader()
        '        '        If lettore2.Read Then
        '        '            txtImportoTot.Text = Format(par.IfNull(lettore2("IMP_DEPOSITO_CAUZ"), 0), "##,##0.00")
        '        '        End If
        '        '        lettore2.Close()
        '        '    End If
        '    End If
        '    lettore.Close()

        '    par.cmd.CommandText = "SELECT BOL_BOLLETTE.id_bolletta_storno,STORICO_dEP_CAUZIONALE.* FROM SISCOM_MI.STORICO_dEP_CAUZIONALE,SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID=STORICO_dEP_CAUZIONALE.ID_BOLLETTA AND STORICO_dEP_CAUZIONALE.ID_CONTRATTO=" & Request.QueryString("IDC") & " AND STORICO_dEP_CAUZIONALE.ID_ANAGRAFICA=" & idAnagrafe & " ORDER BY STORICO_dEP_CAUZIONALE.ID DESC"
        '    lettore = par.cmd.ExecuteReader()
        '    If lettore.Read Then
        '        If par.IfNull(lettore("ID_BOLLETTA_STORNO"), 0) <> 0 Then
        '            lblTitolo.Text = lblTitolo.Text & " - Importo Dep. Cauzionale €: " & "---"
        '        Else
        '            lblTitolo.Text = lblTitolo.Text & " - Importo Dep. Cauzionale €: " & Format(par.IfNull(lettore("IMPORTO"), 0), "##,##0.00")
        '        End If
        '    End If
        '    lettore.Close()

        '    par.OracleConn.Close()

        'Catch ex As Exception
        '    Me.lblErrore.Visible = True
        '    lblErrore.Text = ex.Message
        '    par.OracleConn.Close()
        '    par.OracleConn.Dispose()
        'End Try
        Try
            Dim RIGA As System.Data.DataRow
            Dim RIGA1 As System.Data.DataRow
            Dim BollStornata As Long = 0

            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            'par.cmd.CommandText = "SELECT id_adeguamento as id,TO_CHAR(TO_DATE(DAL,'yyyymmdd'),'dd/mm/yyyy') AS DAL,TO_CHAR(TO_DATE(AL,'yyyymmdd'),'dd/mm/yyyy') AS AL,GIORNI,TASSO,adeguamento_interessi_voci.importo,(case when (select distinct tipo_applicazione from siscom_mi.bol_bollette_gest where id_adeguamento=adeguamento_interessi.id)='T' then 1 else 0 end) as fl_applicato FROM siscom_mi.rapporti_utenza,siscom_mi.adeguamento_interessi,siscom_mi.adeguamento_interessi_voci WHERE rapporti_utenza.ID = adeguamento_interessi.id_contratto " _
            '    & "AND adeguamento_interessi_voci.id_adeguamento = adeguamento_interessi.ID AND id_contratto=" & Request.QueryString("IDC") & " AND id_ANAGRAFICA=" & idanagrafe & " order by adeguamento_interessi_voci.dal asc"

            par.cmd.CommandText = "SELECT id_adeguamento as id,TO_CHAR(TO_DATE(DAL,'yyyymmdd'),'dd/mm/yyyy') AS DAL,TO_CHAR(TO_DATE(AL,'yyyymmdd'),'dd/mm/yyyy') AS AL,GIORNI,TASSO,adeguamento_interessi_voci.importo,'1' as fl_applicato FROM siscom_mi.rapporti_utenza,siscom_mi.adeguamento_interessi,siscom_mi.adeguamento_interessi_voci WHERE rapporti_utenza.ID = adeguamento_interessi.id_contratto " _
               & "AND adeguamento_interessi_voci.id_adeguamento = adeguamento_interessi.ID AND id_contratto=" & Request.QueryString("IDC") & " /*AND id_ANAGRAFICA=" & idAnagrafe & "*/ order by adeguamento_interessi_voci.dal asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()

            Dim dtRisultato As New Data.DataTable()
            dtRisultato.Columns.Add("ID")
            dtRisultato.Columns.Add("DAL")
            dtRisultato.Columns.Add("AL")
            dtRisultato.Columns.Add("GIORNI")
            dtRisultato.Columns.Add("TASSO")
            dtRisultato.Columns.Add("IMPORTO")
            dtRisultato.Columns.Add("RESTITUITI")

            Dim dt2 As New Data.DataTable()
            dt2.Columns.Add("ID", Type.GetType("System.String"))
            dt2.Columns.Add("DAL", Type.GetType("System.String"))
            dt2.Columns.Add("AL", Type.GetType("System.String"))
            dt2.Columns.Add("GIORNI", Type.GetType("System.String"))
            dt2.Columns.Add("TASSO", Type.GetType("System.String"))
            dt2.Columns.Add("IMPORTO", Type.GetType("System.String"))
            dt2.Columns.Add("RESTITUITI", Type.GetType("System.String"))

            da.Fill(dt)
            da.Dispose()

            Dim importoInteresse As Decimal = 0
            Dim idBolletta As Long = 0
            Dim numbolletta As String = ""

            If dt.Rows.Count = 0 Then
                RIGA = dt2.NewRow()
                RIGA.Item("ID") = -1
                RIGA.Item("DAL") = "&nbsp"
                RIGA.Item("AL") = "&nbsp"
                RIGA.Item("GIORNI") = "&nbsp"
                RIGA.Item("TASSO") = "&nbsp"
                RIGA.Item("IMPORTO") = "&nbsp"
                RIGA.Item("RESTITUITI") = "&nbsp"
                dt2.Rows.Add(RIGA)
                DataGridDepCauz.DataSource = dt2
                lblNoInteressi.Text = "<img id='Img1' src='../IMG/Alert.gif' style='height: 17px;' alt='alert' /> (Interessi non calcolati)"
            Else
                For Each row As Data.DataRow In dt.Rows
                    idBolletta = 0
                    numbolletta = ""
                    BollStornata = 0
                    'CERCO L'INTERESSE SE APPLICATO
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ADEGUAMENTO_INTERESSI WHERE ID=" & row.Item("ID")
                    Dim lettoreA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If lettoreA.Read Then
                        If par.IfNull(lettoreA("FL_APPLICATO"), "") = "1" Then
                            importoInteresse = par.IfNull(lettoreA("IMPORTO"), 0)
                        End If
                    End If
                    lettoreA.Close()
                    BollStornata = 0
                    If importoInteresse <> 0 Then
                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO " _
                            & " from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") _
                            & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette_voci.id_voce=15 " _
                            & " AND BOL_BOLLETTE.ID in (SELECT ID_BOLLETTA FROM siscom_mi.BOL_BOLLETTE_GEST WHERE ID_ADEGUAMENTO=" & row.Item("ID") & ") " _
                            & "/*and abs(bol_bollette_voci.importo)=" & par.VirgoleInPunti(importoInteresse) & "*/"

                        lettoreA = par.cmd.ExecuteReader()
                        If lettoreA.Read Then
                            idBolletta = par.IfNull(lettoreA("ID_BOLLETTA"), "")
                            numbolletta = par.IfNull(lettoreA("NUM_BOLLETTA"), "")
                            BollStornata = par.IfNull(lettoreA("id_bolletta_storno"), 0)
                        Else
                            par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=3 and bol_bollette_voci.id_voce in (15) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                            Dim lettoreB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If lettoreB.Read Then
                                idBolletta = par.IfNull(lettoreB("ID_BOLLETTA"), "")
                                numbolletta = par.IfNull(lettoreB("NUM_BOLLETTA"), "")
                                BollStornata = par.IfNull(lettoreB("id_bolletta_storno"), 0)
                            Else
                                Select Case Mid(row.Item("DAL"), 7, 4)
                                    Case "2010"
                                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_bolletta_storno is null and id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=1 and bol_bollette_voci.id_voce in (350) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                                    Case "2011"
                                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_bolletta_storno is null and id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=1 and bol_bollette_voci.id_voce in (351) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                                    Case "2012"
                                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_bolletta_storno is null and id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=1 and bol_bollette_voci.id_voce in (352) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                                    Case "2013"
                                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_bolletta_storno is null and id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=1 and bol_bollette_voci.id_voce in (353) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                                    Case "2014"
                                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_bolletta_storno is null and id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=1 and bol_bollette_voci.id_voce in (354) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                                    Case "2015"
                                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_bolletta_storno is null and id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=1 and bol_bollette_voci.id_voce in (355) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                                    Case "2016"
                                        par.cmd.CommandText = "select bol_bollette_voci.*,bol_bollette.NUM_BOLLETTA,BOL_BOLLETTE.ID_BOLLETTA_STORNO from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_bolletta_storno is null and id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id_tipo=1 and bol_bollette_voci.id_voce in (356) and ABS(bol_bollette_voci.importo)=" & par.VirgoleInPunti(row.Item("IMPORTO"))
                                End Select
                                Dim lettoreC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If lettoreC.Read Then
                                    idBolletta = par.IfNull(lettoreC("ID_BOLLETTA"), "")
                                    numbolletta = par.IfNull(lettoreC("NUM_BOLLETTA"), "")
                                    BollStornata = par.IfNull(lettoreC("id_bolletta_storno"), 0)
                                End If
                                lettoreC.Close()
                            End If
                            lettoreB.Close()
                        End If
                        lettoreA.Close()
                    End If

                    RIGA1 = dtRisultato.NewRow()
                    RIGA1.Item("ID") = row.Item("ID")
                    RIGA1.Item("DAL") = par.IfNull(row.Item("DAL"), "")
                    RIGA1.Item("AL") = par.IfNull(row.Item("AL"), "")
                    RIGA1.Item("GIORNI") = par.IfNull(row.Item("GIORNI"), "")
                    RIGA1.Item("TASSO") = par.IfNull(row.Item("TASSO"), "")
                    RIGA1.Item("IMPORTO") = par.IfNull(row.Item("IMPORTO"), "")
                    If RIGA1.Item("IMPORTO") > 0 Then
                        If row.Item("FL_APPLICATO") = "1" And idBolletta <> 0 Then
                            If BollStornata = 0 Then
                                RIGA1.Item("RESTITUITI") = "SI - <i>N.ro bolletta</i>: <a href=""javascript:window.open('../Contabilita/DettaglioBolletta.aspx?IDCONT=" & Request.QueryString("IDC") & "&IDBollCOP=" & idBolletta & "&IDANA=" & idAnagrafe & "','DettBollettaDepos','');void(0);"">" & numbolletta & "</a>"
                            Else
                                RIGA1.Item("RESTITUITI") = "NO - <i>bolletta STORNATA</i>: <a href=""javascript:window.open('../Contabilita/DettaglioBolletta.aspx?IDCONT=" & Request.QueryString("IDC") & "&IDBollCOP=" & idBolletta & "&IDANA=" & idAnagrafe & "','DettBollettaDepos','');void(0);"">" & numbolletta & "</a>"
                            End If
                        Else
                            RIGA1.Item("RESTITUITI") = "NO"
                        End If
                    Else
                        RIGA1.Item("RESTITUITI") = "NO-Importo=0"
                    End If
                    dtRisultato.Rows.Add(RIGA1)
                Next
                DataGridDepCauz.DataSource = dtRisultato
            End If
            DataGridDepCauz.DataBind()

            par.cmd.CommandText = "SELECT sum(adeguamento_interessi_voci.importo) as totimporto FROM siscom_mi.rapporti_utenza,siscom_mi.adeguamento_interessi,siscom_mi.adeguamento_interessi_voci WHERE rapporti_utenza.ID = adeguamento_interessi.id_contratto " _
                & "AND adeguamento_interessi_voci.id_adeguamento = adeguamento_interessi.ID AND id_contratto=" & Request.QueryString("IDC") & " /*AND id_ANAGRAFICA=" & idAnagrafe & "*/"
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore0.Read Then
                lblTotImp.Text = Format(par.IfNull(lettore0("totimporto"), 0), "##,##0.00") & " €."
            End If
            lettore0.Close()

            par.cmd.CommandText = "SELECT * FROM siscom_mi.adeguamento_interessi where id_contratto=" & Request.QueryString("IDC") & " /*AND id_ANAGRAFICA=" & idAnagrafe & "*/ order by data desc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                lblDataUltima.Text = par.FormattaData(par.IfNull(lettore("DATA"), ""))
            Else
                lblDataUltima.Text = "---"
                lblTotImp.Text = "---"
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT * FROM siscom_mi.bol_bollette where id_contratto=" & Request.QueryString("IDC") & " /*AND COD_AFFITTUARIO=" & idAnagrafe & "*/ AND ID_TIPO=9 and nvl(data_scadenza,'29991231')<>'29991231' AND ID_BOLLETTA_STORNO IS NULL AND (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) order BY ID DESC"
            lettore = par.cmd.ExecuteReader()
            If lettore.Read Then
                txtDataMAV.Text = par.FormattaData(par.IfNull(lettore("DATA_PAGAMENTO"), ""))
                txtDataValuta.Text = par.FormattaData(par.IfNull(lettore("DATA_VALUTA"), ""))
                txtImportoTot.Text = Format(par.IfNull(lettore("IMPORTO_TOTALE"), 0), "##,##0.00")
                txtImpPagato.Text = Format(par.IfNull(lettore("IMPORTO_PAGATO"), 0), "##,##0.00")
                txtDataEmiss.Text = par.FormattaData(par.IfNull(lettore("DATA_EMISSIONE"), ""))
                txtDataScad.Text = par.FormattaData(par.IfNull(lettore("DATA_SCADENZA"), ""))
                txtDataCostituzione.Text = par.FormattaData(par.IfNull(lettore("DATA_PAGAMENTO"), ""))
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.id_bolletta_storno,STORICO_dEP_CAUZIONALE.* FROM SISCOM_MI.STORICO_dEP_CAUZIONALE,SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID (+)=STORICO_dEP_CAUZIONALE.ID_BOLLETTA AND STORICO_dEP_CAUZIONALE.ID_CONTRATTO=" & Request.QueryString("IDC") & " /*AND STORICO_dEP_CAUZIONALE.ID_ANAGRAFICA=" & idAnagrafe & "*/ ORDER BY STORICO_dEP_CAUZIONALE.ID DESC"
            lettore = par.cmd.ExecuteReader()
            If lettore.Read Then
                If par.IfNull(lettore("ID_BOLLETTA_STORNO"), 0) <> 0 Then
                    lblTitolo.Text = lblTitolo.Text & " - Importo Dep. Cauzionale €: " & "---"
                Else
                    lblTitolo.Text = lblTitolo.Text & " - Importo Dep. Cauzionale €: " & Format(par.IfNull(lettore("IMPORTO"), 0), "##,##0.00")
                End If

            End If
            lettore.Close()

            par.OracleConn.Close()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Sub

    Private Sub CaricaStoricoDep()
        'Try
        '    par.OracleConn.Open()
        '    par.cmd = par.OracleConn.CreateCommand()

        '    Dim RIGA As System.Data.DataRow

        '    Dim dtStorico As New Data.DataTable()
        '    dtStorico.Columns.Add("ID")
        '    dtStorico.Columns.Add("DATA_EMISSIONE")
        '    dtStorico.Columns.Add("IMPORTO_TOTALE")
        '    dtStorico.Columns.Add("NUM_BOLLETTA")
        '    dtStorico.Columns.Add("NOTE")
        '    dtStorico.Columns.Add("DATA_PAGAMENTO")
        '    dtStorico.Columns.Add("PROVENIENZA")
        '    dtStorico.Columns.Add("LIBRO")
        '    dtStorico.Columns.Add("BOLLA")
        '    dtStorico.Columns.Add("DATA_COSTITUZIONE")
        '    dtStorico.Columns.Add("DATA_RESTITUZIONE")
        '    dtStorico.Columns.Add("IMPORTO_RESTITUZIONE")


        '    par.cmd.CommandText = "SELECT STORICO_DEP_CAUZIONALE.*,RAPPORTI_UTENZA_DEP_PROV.LIBRO,RAPPORTI_UTENZA_DEP_PROV.BOLLA,siscom_mi.TAB_PROVENIENZA_DEP.DESCRIZIONE AS PROVENIENZA FROM siscom_mi.TAB_PROVENIENZA_DEP,siscom_mi.STORICO_DEP_CAUZIONALE,SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV WHERE siscom_mi.TAB_PROVENIENZA_DEP.ID (+)=RAPPORTI_UTENZA_DEP_PROV.PROVENIENZA AND RAPPORTI_UTENZA_DEP_PROV.ID_CONTRATTO=STORICO_DEP_CAUZIONALE.ID_CONTRATTO AND RAPPORTI_UTENZA_DEP_PROV.ID_STORICO_DEP=STORICO_DEP_CAUZIONALE.ID AND STORICO_DEP_CAUZIONALE.ID_CONTRATTO=" & Request.QueryString("IDC") & " ORDER BY DATA ASC"
        '    Dim da0 As Oracle.DataAccess.Client.OracleDataAdapter
        '    Dim dt0 As New Data.DataTable
        '    da0 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '    da0.Fill(dt0)
        '    da0.Dispose()
        '    If dt0.Rows.Count > 0 Then
        '        For Each row As Data.DataRow In dt0.Rows
        '            RIGA = dtStorico.NewRow()
        '            RIGA.Item("DATA_EMISSIONE") = par.FormattaData(par.IfNull(row.Item("DATA"), ""))
        '            RIGA.Item("IMPORTO_TOTALE") = Format(par.IfNull(row.Item("IMPORTO"), 0), "##,##0.00")
        '            RIGA.Item("NOTE") = par.IfNull(row.Item("NOTE"), 0)

        '            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,NUM_BOLLETTA,to_char(to_date(DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_EMISSIONE,to_char(to_date(DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA,to_char(to_date(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') as DATA_PAGAMENTO,to_char(to_date(BOL_BOLLETTE.DATA_VALUTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_VALUTA,TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMPORTO,'9G999G990D99')) as IMPORTO_TOTALE, TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMP_PAGATO,'9G999G990D99')) AS IMPORTO_PAGATO FROM siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") & "AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID=" & par.IfNull(row.Item("ID_BOLLETTA"), 0) & " order BY ID DESC"
        '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '            If myReader.Read Then
        '                RIGA.Item("ID") = par.IfNull(myReader("ID"), 0)
        '                RIGA.Item("NUM_BOLLETTA") = par.IfNull(myReader("NUM_BOLLETTA"), "")
        '                RIGA.Item("DATA_PAGAMENTO") = par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), ""))
        '            Else
        '                RIGA.Item("ID") = ""
        '                RIGA.Item("NUM_BOLLETTA") = ""
        '                If par.IfNull(row.Item("DATA_COSTITUZIONE"), "") <> "" Then
        '                    RIGA.Item("DATA_PAGAMENTO") = par.FormattaData(par.IfNull(row.Item("DATA_COSTITUZIONE"), ""))
        '                Else
        '                    RIGA.Item("DATA_PAGAMENTO") = par.FormattaData(par.IfNull(row.Item("DATA"), ""))
        '                End If
        '            End If
        '            myReader.Close()
        '            RIGA.Item("PROVENIENZA") = par.IfNull(row.Item("PROVENIENZA"), "")
        '            RIGA.Item("LIBRO") = par.IfNull(row.Item("LIBRO"), "")
        '            RIGA.Item("BOLLA") = par.IfNull(row.Item("BOLLA"), "")

        '            RIGA.Item("IMPORTO_RESTITUZIONE") = Format(par.IfNull(row.Item("IMPORTO_RESTITUZIONE"), 0), "##,##0.00")
        '            RIGA.Item("DATA_RESTITUZIONE") = par.FormattaData(par.IfNull(row.Item("DATA_RESTITUZIONE"), ""))

        '            dtStorico.Rows.Add(RIGA)
        '        Next
        '        DataGridStorico.DataSource = dtStorico
        '        DataGridStorico.DataBind()
        '    Else
        '        Label6.Text = Label6.Text & " (DATO NON ANCORA DISPONIBILE)"
        '        'par.cmd.CommandText = "SELECT '' AS DATA_RESTITUZIONE,''AS IMPORTO_RESTITUZIONE,'' as note,RAPPORTI_UTENZA_DEP_PROV.LIBRO,RAPPORTI_UTENZA_DEP_PROV.BOLLA,siscom_mi.TAB_PROVENIENZA_DEP.DESCRIZIONE AS PROVENIENZA,BOL_BOLLETTE.NUM_BOLLETTA,BOL_BOLLETTE.ID,to_char(to_date(DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_EMISSIONE,to_char(to_date(DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA,to_char(to_date(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') as DATA_PAGAMENTO,to_char(to_date(BOL_BOLLETTE.DATA_VALUTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_VALUTA,TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMPORTO,'9G999G990D99')) as IMPORTO_TOTALE, TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMP_PAGATO,'9G999G990D99')) AS IMPORTO_PAGATO FROM SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV, siscom_mi.TAB_PROVENIENZA_DEP,siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where siscom_mi.TAB_PROVENIENZA_DEP.ID (+)=RAPPORTI_UTENZA_DEP_PROV.PROVENIENZA AND RAPPORTI_UTENZA_DEP_PROV.ID_CONTRATTO=BOL_BOLLETTE.ID_CONTRATTO AND bol_bollette.id_contratto=" & Request.QueryString("IDC") & " AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_VOCE=7 AND BOL_BOLLETTE.ID_TIPO=9 AND BOL_BOLLETTE.ID_BOLLETTA_STORNO IS NULL order BY bol_bollette.ID DESC"
        '        'Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        '        'Dim dt As New Data.DataTable
        '        'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '        'da.Fill(dt)
        '        'da.Dispose()

        '        'If dt.Rows.Count > 0 Then
        '        '    DataGridStorico.DataSource = dt
        '        '    DataGridStorico.DataBind()
        '        'Else
        '        '    par.cmd.CommandText = "SELECT * FROM siscom_mi.RAPPORTI_UTENZA WHERE ID=" & Request.QueryString("IDC")
        '        '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '        '    If myReader.Read Then
        '        '        RIGA = dtStorico.NewRow()
        '        '        RIGA.Item("ID") = ""
        '        '        RIGA.Item("DATA_EMISSIONE") = par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), ""))
        '        '        RIGA.Item("IMPORTO_TOTALE") = par.IfNull(myReader("IMP_DEPOSITO_CAUZ"), 0)
        '        '        RIGA.Item("NOTE") = "Importo originario deposito cauzionale"
        '        '        RIGA.Item("NUM_BOLLETTA") = ""
        '        '        RIGA.Item("DATA_PAGAMENTO") = ""
        '        '        RIGA.Item("PROVENIENZA") = ""
        '        '        RIGA.Item("LIBRO") = ""
        '        '        RIGA.Item("BOLLA") = ""
        '        '        RIGA.Item("IMPORTO_RESTITUZIONE") = ""
        '        '        RIGA.Item("DATA_RESTITUZIONE") = ""

        '        '        dtStorico.Rows.Add(RIGA)

        '        '        DataGridStorico.DataSource = dtStorico
        '        '        DataGridStorico.DataBind()

        '        '    End If
        '        '    myReader.Close()
        '        'End If
        '    End If

        '    par.OracleConn.Close()
        '    par.OracleConn.Dispose()


        'Catch ex As Exception
        '    Me.lblErrore.Visible = True
        '    lblErrore.Text = ex.Message
        '    par.OracleConn.Close()
        '    par.OracleConn.Dispose()
        'End Try
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim RIGA As System.Data.DataRow

            Dim dtStorico As New Data.DataTable()
            dtStorico.Columns.Add("ID")
            dtStorico.Columns.Add("DATA_EMISSIONE")
            dtStorico.Columns.Add("IMPORTO_TOTALE")
            dtStorico.Columns.Add("NUM_BOLLETTA")
            dtStorico.Columns.Add("NOTE")
            dtStorico.Columns.Add("DATA_PAGAMENTO")
            dtStorico.Columns.Add("PROVENIENZA")
            dtStorico.Columns.Add("LIBRO")
            dtStorico.Columns.Add("BOLLA")
            dtStorico.Columns.Add("DATA_COSTITUZIONE")
            dtStorico.Columns.Add("DATA_RESTITUZIONE")
            dtStorico.Columns.Add("IMPORTO_RESTITUZIONE")
            dtStorico.Columns.Add("RESTITUIBILE")


            par.cmd.CommandText = "SELECT STORICO_DEP_CAUZIONALE.*,RAPPORTI_UTENZA_DEP_PROV.LIBRO,RAPPORTI_UTENZA_DEP_PROV.BOLLA,siscom_mi.TAB_PROVENIENZA_DEP.DESCRIZIONE AS PROVENIENZA FROM siscom_mi.TAB_PROVENIENZA_DEP,siscom_mi.STORICO_DEP_CAUZIONALE,SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV WHERE siscom_mi.TAB_PROVENIENZA_DEP.ID(+)=RAPPORTI_UTENZA_DEP_PROV.PROVENIENZA AND RAPPORTI_UTENZA_DEP_PROV.ID_STORICO_DEP(+)=STORICO_DEP_CAUZIONALE.ID AND STORICO_DEP_CAUZIONALE.ID_CONTRATTO=" & Request.QueryString("IDC") & " /*AND STORICO_DEP_CAUZIONALE.ID_ANAGRAFICA=" & idAnagrafe & "*/ ORDER BY DATA DESC,ID DESC"
            Dim da0 As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt0 As New Data.DataTable
            da0 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da0.Fill(dt0)
            da0.Dispose()
            If dt0.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt0.Rows
                    RIGA = dtStorico.NewRow()
                    RIGA.Item("DATA_EMISSIONE") = par.FormattaData(par.IfNull(row.Item("DATA"), ""))
                    RIGA.Item("IMPORTO_TOTALE") = Format(par.IfNull(row.Item("IMPORTO"), 0), "##,##0.00")
                    RIGA.Item("NOTE") = par.IfNull(row.Item("NOTE"), "")
                    If par.IfNull(row.Item("RESTITUIBILE"), "0") = "1" Then
                        RIGA.Item("RESTITUIBILE") = "SI"
                    Else
                        RIGA.Item("RESTITUIBILE") = "NO"
                    End If

                    par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,NUM_BOLLETTA,to_char(to_date(DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_EMISSIONE,to_char(to_date(DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA,to_char(to_date(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') as DATA_PAGAMENTO,to_char(to_date(BOL_BOLLETTE.DATA_VALUTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_VALUTA,TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMPORTO,'9G999G990D99')) as IMPORTO_TOTALE, TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMP_PAGATO,'9G999G990D99')) AS IMPORTO_PAGATO,bol_bollette.id_bolletta_storno FROM siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") & "AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID=" & par.IfNull(row.Item("ID_BOLLETTA"), 0) & " order BY ID DESC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        RIGA.Item("ID") = par.IfNull(myReader("ID"), 0)
                        RIGA.Item("NUM_BOLLETTA") = par.IfNull(myReader("NUM_BOLLETTA"), "")
                        If par.IfNull(myReader("ID_BOLLETTA_STORNO"), 0) = 0 Then
                            RIGA.Item("DATA_PAGAMENTO") = par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), ""))
                        Else
                            RIGA.Item("DATA_PAGAMENTO") = ""
                        End If
                    Else
                        RIGA.Item("ID") = ""
                        RIGA.Item("NUM_BOLLETTA") = ""
                        If par.IfNull(row.Item("DATA_COSTITUZIONE"), "") <> "" Then
                            RIGA.Item("DATA_PAGAMENTO") = par.FormattaData(par.IfNull(row.Item("DATA_COSTITUZIONE"), ""))
                        Else
                            RIGA.Item("DATA_PAGAMENTO") = par.FormattaData(par.IfNull(row.Item("DATA"), ""))
                        End If
                    End If
                    myReader.Close()
                    RIGA.Item("PROVENIENZA") = par.IfNull(row.Item("PROVENIENZA"), "")
                    RIGA.Item("LIBRO") = par.IfNull(row.Item("LIBRO"), "")
                    RIGA.Item("BOLLA") = par.IfNull(row.Item("BOLLA"), "")

                    RIGA.Item("IMPORTO_RESTITUZIONE") = Format(par.IfNull(row.Item("IMPORTO_RESTITUZIONE"), 0), "##,##0.00")
                    RIGA.Item("DATA_RESTITUZIONE") = par.FormattaData(par.IfNull(row.Item("DATA_RESTITUZIONE"), ""))

                    dtStorico.Rows.Add(RIGA)
                Next
                DataGridStorico.DataSource = dtStorico
                DataGridStorico.DataBind()
            Else
                Label6.Text = Label6.Text & " (DATO NON ANCORA DISPONIBILE)"
            End If

            par.OracleConn.Close()
            par.OracleConn.Dispose()


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Sub

    'Private Sub CaricaStoricoDep()
    '    Try
    '        par.OracleConn.Open()
    '        par.cmd = par.OracleConn.CreateCommand()

    '        Dim RIGA As System.Data.DataRow
    '        Dim RIGA2 As System.Data.DataRow

    '        Dim dtStorico As New Data.DataTable()
    '        dtStorico.Columns.Add("ID")
    '        dtStorico.Columns.Add("DATA_EMISSIONE")
    '        dtStorico.Columns.Add("DATA_SCADENZA")
    '        dtStorico.Columns.Add("IMPORTO_TOTALE")
    '        dtStorico.Columns.Add("IMPORTO_PAGATO")
    '        dtStorico.Columns.Add("DATA_PAGAMENTO")
    '        dtStorico.Columns.Add("DATA_VALUTA")

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.STORICO_DEP_CAUZIONALE WHERE ID_CONTRATTO=" & Request.QueryString("IDC")
    '        Dim da0 As Oracle.DataAccess.Client.OracleDataAdapter
    '        Dim dt0 As New Data.DataTable
    '        da0 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        da0.Fill(dt0)
    '        da0.Dispose()
    '        If dt0.Rows.Count > 0 Then
    '            For Each row As Data.DataRow In dt0.Rows
    '                par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,to_char(to_date(DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_EMISSIONE,to_char(to_date(DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA,to_char(to_date(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') as DATA_PAGAMENTO,to_char(to_date(BOL_BOLLETTE.DATA_VALUTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_VALUTA,TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMPORTO,'9G999G990D99')) as IMPORTO_TOTALE, TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMP_PAGATO,'9G999G990D99')) AS IMPORTO_PAGATO FROM siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") & "AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_VOCE=7 AND BOL_BOLLETTE.ID=" & par.IfNull(row.Item("ID_BOLLETTA"), 0) & " AND BOL_BOLLETTE.ID_TIPO=9 order BY ID DESC"
    '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReader.Read Then
    '                    RIGA = dtStorico.NewRow()

    '                    RIGA.Item("ID") = par.IfNull(myReader("ID"), 0)
    '                    RIGA.Item("DATA_EMISSIONE") = par.IfNull(myReader("DATA_EMISSIONE"), "")
    '                    RIGA.Item("DATA_SCADENZA") = par.IfNull(myReader("DATA_SCADENZA"), "")
    '                    RIGA.Item("IMPORTO_TOTALE") = par.IfNull(myReader("IMPORTO_TOTALE"), 0)
    '                    RIGA.Item("IMPORTO_PAGATO") = par.IfNull(myReader("IMPORTO_PAGATO"), "")
    '                    RIGA.Item("DATA_PAGAMENTO") = par.IfNull(myReader("DATA_PAGAMENTO"), "")
    '                    RIGA.Item("DATA_VALUTA") = par.IfNull(myReader("DATA_VALUTA"), "")

    '                    dtStorico.Rows.Add(RIGA)
    '                Else
    '                    RIGA2 = dtStorico.NewRow()
    '                    RIGA2.Item("ID") = par.IfNull(row.Item("ID_BOLLETTA"), 0)
    '                    RIGA2.Item("DATA_EMISSIONE") = par.FormattaData(par.IfNull(row.Item("DATA"), ""))
    '                    RIGA2.Item("IMPORTO_TOTALE") = par.IfNull(row.Item("IMPORTO"), 0)

    '                    dtStorico.Rows.Add(RIGA2)
    '                End If
    '                myReader.Close()
    '            Next
    '            DataGridStorico.DataSource = dtStorico
    '            DataGridStorico.DataBind()
    '        Else
    '            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,to_char(to_date(DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_EMISSIONE,to_char(to_date(DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA,to_char(to_date(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') as DATA_PAGAMENTO,to_char(to_date(BOL_BOLLETTE.DATA_VALUTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_VALUTA,TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMPORTO,'9G999G990D99')) as IMPORTO_TOTALE, TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMP_PAGATO,'9G999G990D99')) AS IMPORTO_PAGATO FROM siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where id_contratto=" & Request.QueryString("IDC") & " AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_VOCE=7 AND BOL_BOLLETTE.ID_TIPO=9 order BY ID DESC"
    '            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
    '            Dim dt As New Data.DataTable
    '            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '            da.Fill(dt)
    '            da.Dispose()

    '            If dt.Rows.Count > 0 Then
    '                DataGridStorico.DataSource = dt
    '                DataGridStorico.DataBind()
    '            Else
    '                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & Request.QueryString("IDC")
    '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReader.Read Then
    '                    RIGA2 = dtStorico.NewRow()
    '                    RIGA2.Item("ID") = ""
    '                    RIGA2.Item("DATA_EMISSIONE") = par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), ""))
    '                    RIGA2.Item("IMPORTO_TOTALE") = par.IfNull(myReader("IMP_DEPOSITO_CAUZ"), 0)

    '                    dtStorico.Rows.Add(RIGA2)

    '                    DataGridStorico.DataSource = dtStorico
    '                    DataGridStorico.DataBind()

    '                End If
    '                myReader.Close()
    '            End If
    '        End If

    '        'par.cmd.CommandText = "SELECT ID,to_char(to_date(DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_EMISSIONE,to_char(to_date(DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA,to_char(to_date(DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') as DATA_PAGAMENTO,to_char(to_date(DATA_VALUTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_VALUTA,TRIM(TO_CHAR(IMPORTO_TOTALE,'9G999G990D99')) as IMPORTO_TOTALE, TRIM(TO_CHAR(IMPORTO_PAGATO,'9G999G990D99')) AS IMPORTO_PAGATO FROM siscom_mi.bol_bollette where id_contratto=" & Request.QueryString("IDC") & " AND ID_TIPO=9 order BY ID DESC"
    '        'Dim da As Data.OracleClient.OracleDataAdapter
    '        'Dim dt As New Data.DataTable
    '        'da = New Data.OracleClient.OracleDataAdapter(par.cmd)
    '        'da.Fill(dt)
    '        'da.Dispose()

    '        'If dt.Rows.Count > 0 Then
    '        '    For Each row As Data.DataRow In dt.Rows
    '        '        RIGA = dtStorico.NewRow()

    '        '        RIGA.Item("ID") = par.IfNull(row.Item("ID"), 0)
    '        '        RIGA.Item("DATA_EMISSIONE") = par.IfNull(row.Item("DATA_EMISSIONE"), "")
    '        '        RIGA.Item("DATA_SCADENZA") = par.IfNull(row.Item("DATA_SCADENZA"), "")
    '        '        RIGA.Item("IMPORTO_TOTALE") = par.IfNull(row.Item("IMPORTO_TOTALE"), 0)
    '        '        RIGA.Item("IMPORTO_TOTALE") = par.IfNull(row.Item("IMPORTO_PAGATO"), 0)
    '        '        RIGA.Item("DATA_PAGAMENTO") = par.IfNull(row.Item("DATA_PAGAMENTO"), "")
    '        '        RIGA.Item("DATA_VALUTA") = par.IfNull(row.Item("DATA_VALUTA"), "")

    '        '        dtStorico.Rows.Add(RIGA)
    '        '    Next
    '        'End If


    '        par.OracleConn.Close()
    '        par.OracleConn.Dispose()

    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '        par.OracleConn.Close()
    '        par.OracleConn.Dispose()
    '    End Try
    'End Sub
End Class
