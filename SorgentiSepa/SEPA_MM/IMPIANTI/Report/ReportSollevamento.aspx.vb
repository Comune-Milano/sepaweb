
Partial Class ASS_ReportSollevamento
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
                    Label2.Text = "IMPIANTO DI SOLLEVAMENTO"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    StringaSql = " select COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO""," _
                                      & " EDIFICI.DENOMINAZIONE AS ""EDIFICIO"",IMPIANTI.* " _
                               & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI " _
                               & " where IMPIANTI.ID =" & vIdImpianto _
                               & " and   IMPIANTI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+) " _
                               & " and   IMPIANTI.ID_EDIFICIO=EDIFICI.ID (+) "

                    par.cmd.CommandText = StringaSql '"select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then

                        lblComplesso.Text = par.IfNull(myReader1("COMPLESSO"), "")
                        lblEdificio.Text = par.IfNull(myReader1("EDIFICIO"), "")

                        lblCodice.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

                        lblDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                        lblDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
                        lblData.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))

                        Me.lblNote.Text = par.IfNull(myReader1("ANNOTAZIONI"), "")

                        StringaSql = " select SCALE_EDIFICI.DESCRIZIONE AS ""SCALA"",I_SOLLEVAMENTO.* " _
                                   & " from   SISCOM_MI.I_SOLLEVAMENTO, SISCOM_MI.SCALE_EDIFICI " _
                                   & " where  I_SOLLEVAMENTO.ID =" & vIdImpianto _
                                   & " and    I_SOLLEVAMENTO.ID_SCALA=SCALE_EDIFICI.ID (+) "


                        par.cmd.CommandText = StringaSql '"select * from SISCOM_MI.I_SOLLEVAMENTO where SISCOM_MI.I_SOLLEVAMENTO.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblScala.Text = par.IfNull(myReader2("SCALA"), "")

                            Me.lblMarca.Text = par.IfNull(myReader2("MARCA_MODELLO"), "")

                            Select Case par.IfNull(myReader2("MODELLO"), "")
                                Case "1"
                                    Me.lblModello.Text = "MONOSPACE"
                                Case "2"
                                    Me.lblModello.Text = "TRADIZIONALE"
                                Case Else
                                    Me.lblModello.Text = ""
                            End Select


                            Me.lblNumImpianto.Text = par.IfNull(myReader2("NUM_IMPIANTO"), "")
                            Me.lblMatricola.Text = par.IfNull(myReader2("MATRICOLA"), "")
                            Me.lblLotto.Text = par.IfNull(myReader2("NUM_LOTTO"), "")
                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

                            Me.lblUbicazione.Text = par.IfNull(myReader2("UBICAZIONE"), "")

                            Select Case par.IfNull(myReader2("TIPO_AZIONAMENTO"), "")
                                Case "1"
                                    Me.lblAzionamento.Text = "ELETTRICO A FUNE"
                                Case "2"
                                    Me.lblAzionamento.Text = "IDRAULICO"
                                Case Else
                                    Me.lblAzionamento.Text = ""
                            End Select

                            Select Case par.IfNull(myReader2("TIPOLOGIA"), "")
                                Case "1"
                                    Me.lblTipologia.Text = "ASCENSORE"
                                Case "2"
                                    Me.lblTipologia.Text = "MONTACARICHI"
                                Case "3"
                                    Me.lblTipologia.Text = "MONTASCALE"
                                Case "4"
                                    Me.lblTipologia.Text = "PIATTAFORMA ELEVATRICE"
                                Case Else
                                    Me.lblTipologia.Text = ""
                            End Select

                            Select Case par.IfNull(myReader2("TIPO_MANOVRA"), "")
                                Case "1"
                                    Me.lblManovra.Text = "NORMALE"
                                Case "2"
                                    Me.lblManovra.Text = "A PRENOTAZIONE"
                                Case "3"
                                    Me.lblManovra.Text = "DUPLEX"
                                Case "4"
                                    Me.lblManovra.Text = "TRIPLEX"
                                Case Else
                                    Me.lblManovra.Text = ""
                            End Select


                            Me.lblVelocita.Text = par.IfNull(myReader2("VELOCITA"), "")
                            Me.lblPortata.Text = par.IfNull(myReader2("PORTATA_KG"), "")
                            Me.lblCorsa.Text = par.IfNull(myReader2("CORSA_MT"), "")
                            Me.lblMaxPersone.Text = par.IfNull(myReader2("PORTATA_PERSONE"), "")

                            Me.lblFermate.Text = par.IfNull(myReader2("NUM_FERMATE"), "")
                            Me.lblNumero.Text = par.IfNull(myReader2("NUMERICO"), "")



                            Me.chkSchema_SI.Checked = False
                            Me.chkSchema_NO.Checked = False
                            Me.chkDM37_SI.Checked = False
                            Me.chkDM37_NO.Checked = False
                            Me.chkLibretto_SI.Checked = False
                            Me.chkLibretto_NO.Checked = False
                            Me.chkCE_SI.Checked = False
                            Me.chkCE_NO.Checked = False
                            Me.chkAllarme_SI.Checked = False
                            Me.chkAllarme_NO.Checked = False
                            Me.chkMatricola_SI.Checked = False
                            Me.chkMatricola_NO.Checked = False

                            If par.IfNull(myReader2("TELEALLARME"), "") = "S" Then
                                Me.chkAllarme_SI.Checked = True
                                Me.chkAllarme_NO.Checked = False
                            Else
                                Me.chkAllarme_SI.Checked = False
                                Me.chkAllarme_NO.Checked = True
                            End If
                            Me.lblTelAllarme.Text = par.IfNull(myReader2("TELEFONO_TELEALLARME"), "")


                            ''*** TAB. CERTIFICAZIONI
                            If par.IfNull(myReader2("SCHEMA_IMPIANTO"), "") = "S" Then
                                Me.chkSchema_SI.Checked = True
                                Me.chkSchema_NO.Checked = False
                            Else
                                Me.chkSchema_SI.Checked = False
                                Me.chkSchema_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("DICH_CONF_LG_37_08"), "") = "S" Then
                                Me.chkDM37_SI.Checked = True
                                Me.chkDM37_NO.Checked = False
                            Else
                                Me.chkDM37_SI.Checked = False
                                Me.chkDM37_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("LIBRETTO"), "") = "S" Then
                                Me.chkLibretto_SI.Checked = True
                                Me.chkLibretto_NO.Checked = False
                            Else
                                Me.chkLibretto_SI.Checked = False
                                Me.chkLibretto_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("CONF_CE"), "") = "S" Then
                                Me.chkCE_SI.Checked = True
                                Me.chkCE_NO.Checked = False
                            Else
                                Me.chkCE_SI.Checked = False
                                Me.chkCE_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("NUM_MATRICOLA"), "") = "S" Then
                                Me.chkMatricola_SI.Checked = True
                                Me.chkMatricola_NO.Checked = False
                            Else
                                Me.chkMatricola_SI.Checked = False
                                Me.chkMatricola_NO.Checked = True
                            End If
                            ''*******************

                        End If
                        myReader2.Close()
                    End If
                    myReader1.Close()


                    '*** ELENCO INQUILINI
                    If Me.lblTipologia.Text = "MONTASCALE" Then
                        Me.lblTitoloInquilini.Visible = True
                        Me.DataGridI.Visible = True

                        StringaSql = "select ID_CONTRATTO AS ID,INTESTATARIO,SCALA,PIANO,INTERNO " _
                              & " from SISCOM_MI.INTESTATARI_UI " _
                              & " where SISCOM_MI.INTESTATARI_UI.ID_CONTRATTO in " _
                              & " (select ID_CONTRATTO from SISCOM_MI.IMPIANTI_INTESTATARI where ID_IMPIANTO=" & vIdImpianto & ")" _
                              & " order by INTESTATARIO"

                        par.cmd.CommandText = StringaSql

                        Dim daIQ As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dsIQ As New Data.DataSet()

                        daIQ.Fill(dsIQ, "INTESTATARI_UI")

                        DataGridI.DataSource = dsIQ
                        DataGridI.DataBind()

                        dsIQ.Dispose()
                    Else
                        Me.lblTitoloInquilini.Visible = False
                        Me.DataGridI.Visible = False
                    End If
                    '*************************


                    '*** VERIFICHE PERIODICE BIENNALI
                    StringaSql = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                                    & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                                    & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                                    & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                                        & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                        & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                        & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PB'" _
                                        & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                        & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "

                    par.cmd.CommandText = StringaSql

                    Dim daPB As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsPB As New Data.DataSet()

                    daPB.Fill(dsPB, "IMPIANTI_VERIFICHE")

                    DataGrid1.DataSource = dsPB
                    DataGrid1.DataBind()

                    dsPB.Dispose()
                    '*************************


                    '*** VERIFICHE STRAORDINARIA
                    'NOTA TIPO=ST (VERIFICHE SOLLEVAMENTO STRAORDINARI)
                    StringaSql = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                            & "ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                            & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                            & "ESITO,MESI_PREALLARME,TIPO,NOTE " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where   SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ST'" _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


                    par.cmd.CommandText = StringaSql

                    Dim daST As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsST As New Data.DataSet()

                    daST.Fill(dsST, "IMPIANTI_VERIFICHE")

                    DataGrid2.DataSource = dsST
                    DataGrid2.DataBind()

                    dsST.Dispose()
                    '*************************

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
