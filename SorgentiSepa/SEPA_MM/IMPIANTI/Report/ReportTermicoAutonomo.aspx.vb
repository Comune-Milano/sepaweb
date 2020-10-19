
Partial Class ASS_ReportTermicoAutonomo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdImpianto As String

    Dim sValoreIDALL As String
    Dim SValoreG As String
    Dim s_Stringasql As String
    Dim SValoreOfferta As String
    Dim dt As New Data.DataTable
    

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
                    Label2.Text = "IMPIANTO TERMICO AUTONOMO (fino a 35 KW)"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    StringaSql = " select COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO""," _
                                      & " EDIFICI.DENOMINAZIONE AS ""EDIFICIO""," _
                                      & " 'COD.'||SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||' - SCALA '||(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala)||' - INTERNO '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO as ""DESCRIZIONE_UNITA""," _
                                      & " IMPIANTI.* " _
                               & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI " _
                               & " where IMPIANTI.ID =" & vIdImpianto _
                               & " and   IMPIANTI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+)" _
                               & " and   IMPIANTI.ID_EDIFICIO=EDIFICI.ID (+) " _
                               & " and   IMPIANTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID  (+) "

                    par.cmd.CommandText = StringaSql '"select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then


                        lblCodice.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

                        lblComplesso.Text = par.IfNull(myReader1("COMPLESSO"), "")
                        lblEdificio.Text = par.IfNull(myReader1("EDIFICIO"), "")
                        lblUnita.Text = par.IfNull(myReader1("DESCRIZIONE_UNITA"), "")

                        lblDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                        lblDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
                        lblData.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


                        Me.lblNote.Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


                        StringaSql = " select TIPOLOGIA_USO_TERMICI.DESCRIZIONE AS ""TIPOLOGIA_USO"",I_TERMICI_AUTONOMI.* " _
                                   & " from SISCOM_MI.I_TERMICI_AUTONOMI, SISCOM_MI.TIPOLOGIA_USO_TERMICI " _
                                   & " where I_TERMICI_AUTONOMI.ID =" & vIdImpianto _
                                   & " and   I_TERMICI_AUTONOMI.ID_TIPOLOGIA_USO=TIPOLOGIA_USO_TERMICI.ID (+) "

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            '*** TAB GENERALE
                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

                            Me.lblTipologia.Text = par.IfNull(myReader2("TIPOLOGIA_USO"), "")

                            Select Case par.IfNull(myReader2("TIPO_APPARECCHIO"), "")
                                Case 1
                                    Me.lblApparecchio.Text = "B (Camera Aperta)"
                                Case 2
                                    Me.lblApparecchio.Text = "C (Camera Stagna)"
                                Case Else
                                    Me.lblApparecchio.Text = ""
                            End Select


                            Select Case par.IfNull(myReader2("TIPO_UBICAZIONE"), "")
                                Case "1"
                                    Me.lblUbicazione.Text = "INTERNO"
                                Case "2"
                                    Me.lblUbicazione.Text = "ESTERNO"
                                Case Else
                                    Me.lblUbicazione.Text = ""
                            End Select

                            Select Case par.IfNull(myReader2("TIPO_POSIZIONAMENTO"), "")
                                Case "1"
                                    Me.lblPosizionamento.Text = "BASAMENTO"
                                Case "2"
                                    Me.lblPosizionamento.Text = "MURALE"
                                Case Else
                                    Me.lblPosizionamento.Text = ""
                            End Select

                            Select Case par.IfNull(myReader2("TIPO_SCARICO_FUMI"), "")
                                Case "1"
                                    Me.lblScaricoFumi.Text = "IN CANNA FUMARIA SINGOLA"
                                Case "2"
                                    Me.lblScaricoFumi.Text = "IN CANNA FUMARIA COLLETTIVA"
                                Case "3"
                                    Me.lblScaricoFumi.Text = "IN FACCIATA"
                                Case Else
                                    Me.lblScaricoFumi.Text = ""
                            End Select

                            Select Case par.IfNull(myReader2("TIPO_CAPPA_PIANO_COTTURA"), "")
                                Case "1"
                                    Me.lblCappa.Text = "ELETTRICO"
                                Case "2"
                                    Me.lblCappa.Text = "NATURALE"
                                Case Else
                                    Me.lblCappa.Text = ""
                            End Select

                            Me.lblPotenza.Text = par.IfNull(myReader2("POTENZA"), "")
                            Me.lblCosumo.Text = par.IfNull(myReader2("CONSUMO_MEDIO"), "")

                            Me.lblDimensioneVentilazione.Text = par.IfNull(myReader2("DIMENSIONE_FORO_VENTILAZIONE"), "")
                            Me.lblDimensioneAreazionezione.Text = par.IfNull(myReader2("DIMENSIONE_FORO_AREAZIONE"), "")


                            Me.chkVENTILAZIONE_SI.Checked = False
                            Me.chkVENTILAZIONE_NO.Checked = False

                            Me.chkAREAZIONE_SI.Checked = False
                            Me.chkAREAZIONE_NO.Checked = False

                            Me.chkISPESL_SI.Checked = False
                            Me.chkISPESL_NO.Checked = False
                            Me.chkCT_SI.Checked = False
                            Me.chkCT_NO.Checked = False
                            Me.chkContabilizzatore_SI.Checked = False
                            Me.chkContabilizzatore_NO.Checked = False
                            'Me.chkTrattamento_SI.Checked = False
                            'Me.chkTrattamento_NO.Checked = False
                            'Me.chkUTF_SI.Checked = False
                            'Me.chkUTF_NO.Checked = False
                            Me.chkConformita_SI.Checked = False
                            Me.chkConformita_NO.Checked = False
                            'Me.chkDecreto_SI.Checked = False
                            'Me.chkDecreto_NO.Checked = False
                            'Me.chkVVF_SI.Checked = False
                            'Me.chkVVF_SI.Checked = False

                            If par.IfNull(myReader2("FORO_VENTILAZIONE"), "") = "S" Then
                                Me.chkVENTILAZIONE_SI.Checked = True
                                Me.chkVENTILAZIONE_NO.Checked = False
                            Else
                                Me.chkVENTILAZIONE_SI.Checked = False
                                Me.chkVENTILAZIONE_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("FORO_AREAZIONE"), "") = "S" Then
                                Me.chkAREAZIONE_SI.Checked = True
                                Me.chkAREAZIONE_NO.Checked = False
                            Else
                                Me.chkAREAZIONE_SI.Checked = False
                                Me.chkAREAZIONE_NO.Checked = True
                            End If


                            '*** TAB. CERTIFICAZIONI
                            If par.IfNull(myReader2("PRATICA_ISPESL"), "") = "S" Then
                                Me.chkISPESL_SI.Checked = True
                                Me.chkISPESL_NO.Checked = False
                            Else
                                Me.chkISPESL_SI.Checked = False
                                Me.chkISPESL_NO.Checked = True
                            End If
                            Me.lblDataISPESL.Text = par.FormattaData(par.IfNull(myReader2("DATA_PRATICA_ISPESL"), ""))

                            If par.IfNull(myReader2("LIBRETTO"), "") = "S" Then
                                Me.chkCT_SI.Checked = True
                                Me.chkCT_NO.Checked = False
                            Else
                                Me.chkCT_SI.Checked = False
                                Me.chkCT_NO.Checked = True
                            End If
                            'Me.lblDataCT.Text = par.FormattaData(par.IfNull(myReader2("DATA_DISMISSIONE_CT"), ""))

                            If par.IfNull(myReader2("CONT_ENERGIA"), "") = "S" Then
                                Me.chkContabilizzatore_SI.Checked = True
                                Me.chkContabilizzatore_NO.Checked = False
                            Else
                                Me.chkContabilizzatore_SI.Checked = False
                                Me.chkContabilizzatore_NO.Checked = True
                            End If

                            'If par.IfNull(myReader2("TRATTAMENTO_ACQUA"), "") = "S" Then
                            '    Me.chkTrattamento_SI.Checked = True
                            '    Me.chkTrattamento_NO.Checked = False
                            'Else
                            '    Me.chkTrattamento_SI.Checked = False
                            '    Me.chkTrattamento_NO.Checked = True
                            'End If

                            'If par.IfNull(myReader2("LICENZA_UTF"), "") = "S" Then
                            '    Me.chkUTF_SI.Checked = True
                            '    Me.chkUTF_NO.Checked = False
                            'Else
                            '    Me.chkUTF_SI.Checked = False
                            '    Me.chkUTF_NO.Checked = True
                            'End If

                            If par.IfNull(myReader2("DICH_CONF_LG_46_90"), "") = "S" Then
                                Me.chkConformita_SI.Checked = True
                                Me.chkConformita_NO.Checked = False
                            Else
                                Me.chkConformita_SI.Checked = False
                                Me.chkConformita_NO.Checked = True
                            End If

                            'If par.IfNull(myReader2("DECR_PREFETTIZIO_SERBATOI"), "") = "S" Then
                            '    Me.chkDecreto_SI.Checked = True
                            '    Me.chkDecreto_NO.Checked = False
                            'Else
                            '    Me.chkDecreto_SI.Checked = False
                            '    Me.chkDecreto_NO.Checked = True
                            'End If

                            'If par.IfNull(myReader2("PRATICA_VVF"), "") = "S" Then
                            '    Me.chkVVF_SI.Checked = True
                            '    Me.chkVVF_NO.Checked = False
                            'Else
                            '    Me.chkVVF_SI.Checked = False
                            '    Me.chkVVF_NO.Checked = True
                            'End If
                            'Me.lblDataRilascioVVF.Text = par.FormattaData(par.IfNull(myReader2("DATA_RILASCIO_VVF"), ""))
                            'Me.lblDataScadenzaVVF.Text = par.FormattaData(par.IfNull(myReader2("DATA_SCADENZA_VVF"), ""))
                            '*******************


                        End If
                            myReader2.Close()

                    End If
                        myReader1.Close()


                        '*** SCAMBIATORE DI CALORE
                        StringaSql = "select SISCOM_MI.GENERATORI_TERMICI.ID,SISCOM_MI.GENERATORI_TERMICI.MODELLO," _
                                            & "SISCOM_MI.GENERATORI_TERMICI.MATRICOLA,SISCOM_MI.GENERATORI_TERMICI.NOTE," _
                                               & "SISCOM_MI.GENERATORI_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.GENERATORI_TERMICI.POTENZA,SISCOM_MI.GENERATORI_TERMICI.MARC_EFF_ENERGETICA,SISCOM_MI.GENERATORI_TERMICI.FLUIDO_TERMOVETTORE " _
                                      & " from SISCOM_MI.GENERATORI_TERMICI " _
                                      & " where SISCOM_MI.GENERATORI_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                                      & " order by SISCOM_MI.GENERATORI_TERMICI.MODELLO "


                        par.cmd.CommandText = StringaSql

                        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim ds1 As New Data.DataSet()

                        da1.Fill(ds1, "GENERATORI_TERMICI")

                        DataGrid1.DataSource = ds1
                        DataGrid1.DataBind()

                        ds1.Dispose()
                        '*************************

                        '*** BRUCIATORI
                    StringaSql = "select SISCOM_MI.BRUCIATORI.ID,SISCOM_MI.BRUCIATORI.MODELLO," _
                                    & "SISCOM_MI.BRUCIATORI.MATRICOLA,SISCOM_MI.BRUCIATORI.NOTE," _
                                       & "SISCOM_MI.BRUCIATORI.ANNO_COSTRUZIONE,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO_MAX " _
                              & " from SISCOM_MI.BRUCIATORI " _
                              & " where SISCOM_MI.BRUCIATORI.ID_IMPIANTO = " & vIdImpianto _
                              & " order by SISCOM_MI.BRUCIATORI.MODELLO "


                        par.cmd.CommandText = StringaSql

                        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
                        Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim ds2 As New Data.DataSet()

                        da2.Fill(ds2, "BRUCIATORI")

                        DataGrid2.DataSource = ds2
                        DataGrid2.DataBind()

                        ds2.Dispose()
                        '******************************************+

                        '*** POMPE DI CIRCOLAZIONE
                    'StringaSql = "select SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO," _
                    '                & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MATRICOLA,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.NOTE," _
                    '                & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.POTENZA " _
                    '            & " from SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI " _
                    '            & " where SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                    '            & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO "

                    'par.cmd.CommandText = StringaSql

                    'Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    'Dim ds3 As New Data.DataSet()

                    'da3.Fill(ds3, "POMPE_CIRCOLAZIONE_TERMICI")


                    'DataGrid3.DataSource = ds3
                    'DataGrid3.DataBind()

                    'ds3.Dispose()
                        '**********************************

                        '*** RENDIMENTO DI COMBUSTIONE
                    'StringaSql = "select ID,to_char(to_date(DATA_ESAME,'yyyymmdd'),'dd/mm/yyyy') as ""DATA_ESAME""," _
                    '            & "ESECUTORE,TEMP_FUMI," _
                    '               & "TEMP_AMB,O2," _
                    '               & "CO2,BACHARACH," _
                    '               & "CO,RENDIMENTO," _
                    '               & "TIRAGGIO " _
                    '      & " from SISCOM_MI.RENDIMENTO_TERMICI " _
                    '      & " where SISCOM_MI.RENDIMENTO_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                    '      & " order by SISCOM_MI.RENDIMENTO_TERMICI.DATA_ESAME "


                    'par.cmd.CommandText = StringaSql

                    ''Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    ''Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
                    'Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    'Dim ds4 As New Data.DataSet()
                    'da4.Fill(ds4, "RENDIMENTO_TERMICI")

                    'DataGrid4.DataSource = ds4
                    'DataGrid4.DataBind()

                    'ds4.Dispose()

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
