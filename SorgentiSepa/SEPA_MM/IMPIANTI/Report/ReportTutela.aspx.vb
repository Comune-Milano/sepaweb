' REPORT TUTELA DEGLI IMPIANTI
Partial Class ReportTutela
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdImpianto As String

    Dim sValoreIDALL As String
    Dim SValoreG As String
    Dim s_Stringasql As String
    Dim SValoreOfferta As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            'sValoreIDALL = Request.QueryString("IDALL")
            'SValoreG = Request.QueryString("DATAS")
            'SValoreOfferta = Request.QueryString("ABB")

            vIdImpianto = Request.QueryString("ID_IMPIANTO")

            If IsNumeric(vIdImpianto) Then

                Try
                    ' LEGGO IMPIANTO SOLLEVAMENTO
                    Label2.Text = "TUTELA DEGLI IMMOBILI"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    StringaSql = " select COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO""," _
                                      & " EDIFICI.DENOMINAZIONE AS ""EDIFICIO"",IMPIANTI.* " _
                               & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI " _
                               & " where IMPIANTI.ID =" & vIdImpianto _
                               & " and   IMPIANTI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+) " _
                               & " and   IMPIANTI.ID_EDIFICIO=EDIFICI.ID (+) "

                    par.cmd.CommandText = StringaSql
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then


                        lblCodice.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

                        lblComplesso.Text = par.IfNull(myReader1("COMPLESSO"), "")
                        lblEdificio.Text = par.IfNull(myReader1("EDIFICIO"), "")

                        lblDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

                        Me.lblNote.Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


                        StringaSql = " select SISCOM_MI.I_TUTELA.*,SISCOM_MI.TIPOLOGIA_RECINZIONE.DESCRIZIONE AS ""TIPO_RECINZIONE"" " _
                                   & " from SISCOM_MI.I_TUTELA,SISCOM_MI.TIPOLOGIA_RECINZIONE " _
                                   & " where SISCOM_MI.I_TUTELA.ID =" & vIdImpianto _
                                        & " and SISCOM_MI.I_TUTELA.ID_RECINZIONE=SISCOM_MI.TIPOLOGIA_RECINZIONE.ID (+) "

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

                            Me.lblRecinzione.Text = par.IfNull(myReader2("RECINZIONE"), "")
                            Me.lblResponsabile.Text = par.IfNull(myReader2("RECINZIONE"), "")

                            'Me.lblPassiCarrai.Text = par.IfNull(myReader2("PASSI_CARRAI"), "")
                            'Me.lblResponsabile.Text = par.IfNull(myReader2("NUM_PASSI_CARRAI"), "")

                            Me.lblVideo.Text = par.IfNull(myReader2("VIDEO_SORVEGLIANZA"), "")

                            Me.lblCancelloCarrabile.Text = par.IfNull(myReader2("CANCELLO_CARRABILE"), "")
                            Me.lblNumCancelliCarrabili.Text = par.IfNull(myReader2("NUM_CANCELLO_CARRABILE"), "")

                            Me.lblCancelloAuto.Text = par.IfNull(myReader2("CANCELLO_AUTO"), "")
                            Me.lblNumCancelliAuto.Text = par.IfNull(myReader2("NUM_CANCELLO_AUTO"), "")

                            Me.lblTipoRecinzione.Text = par.IfNull(myReader2("TIPO_RECINZIONE"), "")
                            Me.lblResponsabile.Text = par.IfNull(myReader2("RESPONSABILE_TRATTAMENTO"), "")


                        End If
                        myReader2.Close()
                    End If
                    myReader1.Close()



                    '*** ELENCO CANCELLI
                    StringaSql = "select SISCOM_MI.I_TUT_CANCELLI.ID,SISCOM_MI.I_TUT_CANCELLI.CARRABILE,SISCOM_MI.I_TUT_CANCELLI.AUTOMATIZZATO," _
                                               & "SISCOM_MI.I_TUT_CANCELLI.MARCA,SISCOM_MI.I_TUT_CANCELLI.MODELLO,SISCOM_MI.I_TUT_CANCELLI.DITTA_MANUTENZIONE " _
                                       & " from SISCOM_MI.I_TUT_CANCELLI " _
                                       & " where SISCOM_MI.I_TUT_CANCELLI.ID_IMPIANTO = " & vIdImpianto _
                                       & " order by SISCOM_MI.I_TUT_CANCELLI.MODELLO "

                    par.cmd.CommandText = StringaSql

                    Dim daCA As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsCA As New Data.DataSet()

                    daCA.Fill(dsCA, "I_TUT_CANCELLI")

                    DataGridCA.DataSource = dsCA
                    DataGridCA.DataBind()

                    dsCA.Dispose()
                    '*************************

                    '*** ELENCO PASSI CARRABILI
                    StringaSql = "select SISCOM_MI.I_TUT_CARRABILE.ID,SISCOM_MI.I_TUT_CARRABILE.NUM_LICENZA," _
                                        & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_CARRABILE.DATA_RILASCIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_RILASCIO"" " _
                              & " from SISCOM_MI.I_TUT_CARRABILE " _
                              & " where SISCOM_MI.I_TUT_CARRABILE.ID_IMPIANTO = " & vIdImpianto _
                              & " order by SISCOM_MI.I_TUT_CARRABILE.NUM_LICENZA "

                    par.cmd.CommandText = StringaSql

                    Dim daP As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsP As New Data.DataSet()

                    daP.Fill(dsP, "I_TUT_CARRABILE")

                    DataGridPasso.DataSource = dsP
                    DataGridPasso.DataBind()

                    dsP.Dispose()
                    '*************************

                    '*** ELENCO ALLOGGI
                    StringaSql = "select SISCOM_MI.I_TUT_ALLOGGI.ID,SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO as EDIFICIO, " _
                                                & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE AS  PIANO, " _
                                                & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO,SISCOM_MI.IDENTIFICATIVI_CATASTALI.SUB as NOME_SUB, " _
                                                & "SISCOM_MI.I_TUT_ALLOGGI.ANTINTRUSIONE,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_ANTINTR,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_ANTINTR," _
                                                & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_ANTINTR,'YYYYmmdd'),'DD/MM/YYYY') as DATA_RIMOZIONE_ANTINTR, " _
                                                & "SISCOM_MI.I_TUT_ALLOGGI.BLINDATA,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_BLINDATA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_BLINDATA, " _
                                                & "SISCOM_MI.I_TUT_ALLOGGI.LASTRATURA,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_LASTRATURA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_LASTRATURA, " _
                                                & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_LASTRATURA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_RIMOZIONE_LASTRATURA, " _
                                                & "SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI " _
                                            & "from SISCOM_MI.I_TUT_ALLOGGI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                            & "where SISCOM_MI.I_TUT_ALLOGGI.ID_IMPIANTO = " & vIdImpianto & " and " _
                                             & "      SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI=SISCOM_MI.UNITA_IMMOBILIARI.ID and " _
                                             & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID and " _
                                             & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and " _
                                             & "      SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD and " _
                                             & "      SISCOM_MI.IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE " _
                                            & "order by SISCOM_MI.I_TUT_ALLOGGI.ID"

                    par.cmd.CommandText = StringaSql

                    Dim daA As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsA As New Data.DataSet()

                    daA.Fill(dsA, "SISCOM_MI.I_TUT_ALLOGGI")

                    DataGridA.DataSource = dsA
                    DataGridA.DataBind()

                    dsA.Dispose()
                    '*************************

                    ' VERIFICHE
                    'NOTA TIPO=TP (TUTELA VERIFICHE PERIODICHE)
                    StringaSql = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                            & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                            & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                            & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='TP'" _
                                & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


                    par.cmd.CommandText = StringaSql

                    'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
                    Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    Dim ds1 As New Data.DataSet()
                    da1.Fill(ds1, "IMPIANTI_VERIFICHE")

                    DataGrid1.DataSource = ds1
                    DataGrid1.DataBind()

                    ds1.Dispose()
                    '**************************


                    'NOTA TIPO=TS (TUTELA VERIFICHE SCALE)
                    StringaSql = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                            & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                            & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                            & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='TS'" _
                                & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


                    par.cmd.CommandText = StringaSql

                    'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    Dim ds2 As New Data.DataSet()
                    da2.Fill(ds2, "IMPIANTI_VERIFICHE")

                    DataGrid2.DataSource = ds2
                    DataGrid2.DataBind()

                    ds2.Dispose()
                    '***********************+

                    '*** EVENTI_IMPIANTI
                    par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.STAMPA_IMPIANTO, "")

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                Catch ex As Exception
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                End Try
            End If
        End If

    End Sub



End Class
