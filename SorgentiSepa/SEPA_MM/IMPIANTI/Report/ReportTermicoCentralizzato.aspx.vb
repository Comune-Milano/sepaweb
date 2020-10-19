' REPORT IMPIANTO TERMICO CENTRALIZZATO

Partial Class ASS_ReportTermicoCentralizzato
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdImpianto As String

    Dim sValoreIDALL As String
    Dim SValoreG As String
    Dim s_Stringasql As String
    Dim SValoreOfferta As String
    Dim dt As New Data.DataTable
    
    Dim lstEdificiCT As System.Collections.Generic.List(Of Epifani.EdificiCT)
    Dim lstUnita As System.Collections.Generic.List(Of Epifani.ListaUI)

    Dim lstEdificiCT_Extra As System.Collections.Generic.List(Of Epifani.EdificiCT)
    Dim lstUI_Extra As System.Collections.Generic.List(Of Epifani.ListaUI)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String
        Dim i As Integer

        Dim CTRL As Control

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        lstEdificiCT = CType(HttpContext.Current.Session.Item("LST_EDIFICI_CT"), System.Collections.Generic.List(Of Epifani.EdificiCT))
        lstUnita = CType(HttpContext.Current.Session.Item("LST_UI"), System.Collections.Generic.List(Of Epifani.ListaUI))

        lstEdificiCT_Extra = CType(HttpContext.Current.Session.Item("LST_EDIFICI_CT_EXTRA"), System.Collections.Generic.List(Of Epifani.EdificiCT))
        lstUI_Extra = CType(HttpContext.Current.Session.Item("LST_UI_EXTRA"), System.Collections.Generic.List(Of Epifani.ListaUI))


        If Not IsPostBack Then

            'sValoreIDALL = Request.QueryString("IDALL")
            'SValoreG = Request.QueryString("DATAS")
            'SValoreOfferta = Request.QueryString("ABB")

            vIdImpianto = Request.QueryString("ID_IMPIANTO")

            If IsNumeric(vIdImpianto) Then

                Try
                    ' LEGGO IMPIANTO TERMICO
                    Label2.Text = "IMPIANTO CENTRALE TERMICA" '"IMPIANTO TERMICO CENTRALIZZATO"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    StringaSql = " select COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO""," _
                                      & " EDIFICI.DENOMINAZIONE AS ""EDIFICIO"",IMPIANTI.* " _
                               & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI " _
                               & " where IMPIANTI.ID =" & vIdImpianto _
                               & " and   IMPIANTI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+)" _
                               & " and   IMPIANTI.ID_EDIFICIO=EDIFICI.ID (+) "

                    par.cmd.CommandText = StringaSql '"select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then


                        lblCodice.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

                        lblComplesso.Text = par.IfNull(myReader1("COMPLESSO"), "")
                        lblEdificio.Text = par.IfNull(myReader1("EDIFICIO"), "")


                        lblDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                        lblDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
                        lblData.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))

                        Me.lblNote.Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


                        StringaSql = " select TIPOLOGIA_USO_TERMICI.DESCRIZIONE AS ""TIPOLOGIA_USO"",TIPOLOGIA_COMBUSTIBILI.DESCRIZIONE AS ""COMBUSTIBILE"",I_TERMICI.* " _
                                   & " from SISCOM_MI.I_TERMICI, SISCOM_MI.TIPOLOGIA_COMBUSTIBILI,SISCOM_MI.TIPOLOGIA_USO_TERMICI " _
                                   & " where I_TERMICI.ID =" & vIdImpianto _
                                   & " and   I_TERMICI.ID_COMBUSTIBILE=TIPOLOGIA_COMBUSTIBILI.ID (+) " _
                                   & " and   I_TERMICI.ID_TIPOLOGIA_USO=TIPOLOGIA_USO_TERMICI.ID (+) "

                        par.cmd.CommandText = StringaSql '"select * from SISCOM_MI.I_SOLLEVAMENTO where SISCOM_MI.I_SOLLEVAMENTO.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblCapacita.Text = par.IfNull(myReader2("CAPACITA"), "")
                            Me.lblPotenza.Text = par.IfNull(myReader2("POTENZA"), "")
                            Me.lblCosumo.Text = par.IfNull(myReader2("CONSUMO_MEDIO"), "")

                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")


                            Me.lblDataAccensione.Text = par.FormattaData(par.IfNull(myReader2("DATA_PRIMA_ACCENSIONE"), ""))
                            Me.lblDataRiposo.Text = par.FormattaData(par.IfNull(myReader2("DATA_MESSA_RIPOSO"), ""))
                            Me.lblNumOre.Text = par.IfNull(myReader2("NUM_ORE_ESERCIZIO"), "")

                            Me.lblTipologia.Text = par.IfNull(myReader2("TIPOLOGIA_USO"), "")
                            Me.lblCombustibile.Text = par.IfNull(myReader2("COMBUSTIBILE"), "")

                            If Me.lblCombustibile.Text = "METANO" Then
                                Me.lblTitolo1.Text = "CONTATORE PDR:"
                                Me.lblCapacita.Text = par.IfNull(myReader2("CONTATORE_PDR"), "")

                                If par.IfNull(myReader2("CONTATORE_PDR"), "") = "" Then
                                    Me.lblCapacita.Text = par.IfNull(myReader2("CAPACITA"), "")
                                End If
                            End If

                            Me.lblSerbatoio.Text = par.IfNull(myReader2("SERBATOIO"), "")

                            Me.lblNumEstintori.Text = par.IfNull(myReader2("NUM_ESTINTORI"), "")


                            For Each CTRL In Me.Controls
                                If TypeOf CTRL Is CheckBox Then
                                    DirectCast(CTRL, CheckBox).Checked = False
                                End If
                            Next


                            If par.IfNull(myReader2("ESTINTORI"), "") = "S" Then
                                Me.chkEstintori_SI.Checked = True
                                Me.chkEstintori_NO.Checked = False
                            Else
                                Me.chkEstintori_SI.Checked = False
                                Me.chkEstintori_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("TRATTAMENTO_ACQUE"), "") = "S" Then
                                Me.chkSistemaTrattamento_SI.Checked = True
                                Me.chkSistemaTrattamento_NO.Checked = False
                            Else
                                Me.chkSistemaTrattamento_SI.Checked = False
                                Me.chkSistemaTrattamento_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("DEFANGATORI"), "") = "S" Then
                                Me.chkDefangatori_SI.Checked = True
                                Me.chkDefangatori_NO.Checked = False
                            Else
                                Me.chkDefangatori_SI.Checked = False
                                Me.chkDefangatori_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("CANALI_FUMO"), "") = "S" Then
                                Me.chkCanali_SI.Checked = True
                                Me.chkCanali_NO.Checked = False
                            Else
                                Me.chkCanali_SI.Checked = False
                                Me.chkCanali_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("VASO_ESPANSIONE"), "") = "A" Then
                                Me.chkVaso_APERTO.Checked = True
                                Me.chkVaso_CHIUSO.Checked = False
                            Else
                                Me.chkVaso_APERTO.Checked = False
                                Me.chkVaso_CHIUSO.Checked = True
                            End If

                            'Me.chkEstintori_SI.Checked = False

                            'Me.chkISPESL_SI.Checked = False
                            'Me.chkISPESL_NO.Checked = False
                            'Me.chkCT_SI.Checked = False
                            'Me.chkCT_NO.Checked = False
                            'Me.chkContabilizzatore_SI.Checked = False
                            'Me.chkContabilizzatore_NO.Checked = False
                            'Me.chkTrattamento_SI.Checked = False
                            'Me.chkTrattamento_NO.Checked = False
                            'Me.chkUTF_SI.Checked = False
                            'Me.chkUTF_NO.Checked = False
                            'Me.chkConformita_SI.Checked = False
                            'Me.chkConformita_NO.Checked = False
                            'Me.chkDecreto_SI.Checked = False
                            'Me.chkDecreto_NO.Checked = False



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
                            Me.lblDataCT.Text = par.FormattaData(par.IfNull(myReader2("DATA_DISMISSIONE_CT"), ""))

                            If par.IfNull(myReader2("CONT_ENERGIA"), "") = "S" Then
                                Me.chkContabilizzatore_SI.Checked = True
                                Me.chkContabilizzatore_NO.Checked = False
                            Else
                                Me.chkContabilizzatore_SI.Checked = False
                                Me.chkContabilizzatore_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("TRATTAMENTO_ACQUA"), "") = "S" Then
                                Me.chkTrattamento_SI.Checked = True
                                Me.chkTrattamento_NO.Checked = False
                            Else
                                Me.chkTrattamento_SI.Checked = True
                                Me.chkTrattamento_NO.Checked = False
                            End If

                            If par.IfNull(myReader2("LICENZA_UTF"), "") = "S" Then
                                Me.chkUTF_SI.Checked = True
                                Me.chkUTF_NO.Checked = False
                            Else
                                Me.chkUTF_SI.Checked = True
                                Me.chkUTF_NO.Checked = False
                            End If

                            If par.IfNull(myReader2("DICH_CONF_LG_46_90"), "") = "S" Then
                                Me.chkConformita_SI.Checked = True
                                Me.chkConformita_NO.Checked = False
                            Else
                                Me.chkConformita_SI.Checked = True
                                Me.chkConformita_NO.Checked = False
                            End If

                            If par.IfNull(myReader2("DECR_PREFETTIZIO_SERBATOI"), "") = "S" Then
                                Me.chkDecreto_SI.Checked = True
                                Me.chkDecreto_NO.Checked = False
                            Else
                                Me.chkDecreto_SI.Checked = True
                                Me.chkDecreto_NO.Checked = False
                            End If

                            Me.lblPraticaVVF.Text = par.IfNull(myReader2("PRATICA_VVF"), "")
                            Me.lblDataVVF.Text = par.FormattaData(par.IfNull(myReader2("DATA_RILASCIO_VVF"), ""))
                            Me.lblDataVVFScadenza.Text = par.FormattaData(par.IfNull(myReader2("DATA_SCADENZA_VVF"), ""))
                            '*******************

                        End If
                        myReader2.Close()


                        '*** EDIFICI ALIMENTATI
                        'Dim row As System.Data.DataRow
                        'Dim daE As Oracle.DataAccess.Client.OracleDataAdapter

                        'StringaSql = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as EDIFICIO, " _
                        '                 & "(select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and COD_TIPOLOGIA='AL') as UI,  " _
                        '                 & "(select sum ((  select sum(VALORE) from SISCOM_MI.DIMENSIONI where ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID and COD_TIPOLOGIA='SUP_NETTA'))  from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and COD_TIPOLOGIA='AL') as MQ  " _
                        '         & " from SISCOM_MI.EDIFICI " _
                        '         & " where ID in " _
                        '      & " (select SISCOM_MI.IMPIANTI_EDIFICI.ID_EDIFICIO from SISCOM_MI.IMPIANTI_EDIFICI where  SISCOM_MI.IMPIANTI_EDIFICI.ID_IMPIANTO =" & vIdImpianto & ") " _
                        '         & " order by EDIFICIO asc"


                        'par.cmd.CommandText = StringaSql


                        'daE = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                        'daE.Fill(dt)

                        'SommaMQ = 0
                        'SommaUI = 0

                        'i = 0
                        'For Each row In dt.Rows

                        '    SommaMQ = SommaMQ + par.IfNull(dt.Rows(i).Item("MQ"), 0)
                        '    SommaUI = SommaUI + par.IfNull(dt.Rows(i).Item("UI"), 0)

                        '    'dt.Rows(i).Item("CONTATORE") = i + 1
                        '    i = i + 1
                        'Next

                        'row = dt.NewRow()
                        'row.Item("EDIFICIO") = "TOTALI:"
                        'row.Item("MQ") = Format(SommaMQ, "##,##0.00")
                        'row.Item("UI") = SommaUI

                        'dt.Rows.Add(row)

                        DataGridEdifici.DataSource = lstEdificiCT 'dt
                        DataGridEdifici.DataBind()

                        For Each oDataGridItem In DataGridEdifici.Items

                            chkExport = oDataGridItem.FindControl("CheckBox1")

                            For i = 0 To lstEdificiCT.Count - 1

                                If lstEdificiCT.Item(i).ID = oDataGridItem.Cells(0).Text Then
                                    If par.IfEmpty(lstEdificiCT.Item(i).TOTALE_UI_AL, 0) > 0 Then
                                        chkExport.Checked = True
                                    Else
                                        chkExport.Checked = False
                                    End If
                                    Exit For
                                End If
                            Next i
                        Next

                        DataGridEdificiExtra.DataSource = lstEdificiCT_Extra
                        DataGridEdificiExtra.DataBind()


                    End If
                    myReader1.Close()


                    '*** GENERATORI DI CALORE
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
                    '******************************************

                    '*** POMPE DI CIRCOLAZIONE
                    StringaSql = "select SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO," _
                                    & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MATRICOLA,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.NOTE," _
                                    & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.POTENZA " _
                                & " from SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI " _
                                & " where SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                                & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO "

                    par.cmd.CommandText = StringaSql

                    Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds3 As New Data.DataSet()

                    da3.Fill(ds3, "POMPE_CIRCOLAZIONE_TERMICI")


                    DataGrid3.DataSource = ds3
                    DataGrid3.DataBind()

                    ds3.Dispose()
                    '**********************************

                    '*** POMPE DI SOLLEVAMENTO
                    StringaSql = "select SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ID,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MODELLO," _
                                      & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MATRICOLA,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ANNO_COSTRUZIONE," _
                                      & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.POTENZA,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.PORTATA," _
                                      & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.PREVALENZA" _
                          & " from SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO " _
                          & " where SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ID_IMPIANTO = " & vIdImpianto _
                          & " order by SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MODELLO "

                    par.cmd.CommandText = StringaSql

                    Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds5 As New Data.DataSet()

                    da5.Fill(ds5, "POMPE_CIRCOLAZIONE_TERMICI")


                    DataGrid5.DataSource = ds5
                    DataGrid5.DataBind()

                    ds5.Dispose()
                    '**********************************

                    '*** RENDIMENTO DI COMBUSTIONE
                    StringaSql = "select ID,to_char(to_date(DATA_ESAME,'yyyymmdd'),'dd/mm/yyyy') as ""DATA_ESAME""," _
                                & "ESECUTORE,TEMP_FUMI," _
                                   & "TEMP_AMB,O2," _
                                   & "CO2,BACHARACH," _
                                   & "CO,RENDIMENTO," _
                                   & "TIRAGGIO " _
                          & " from SISCOM_MI.RENDIMENTO_TERMICI " _
                          & " where SISCOM_MI.RENDIMENTO_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                          & " order by SISCOM_MI.RENDIMENTO_TERMICI.DATA_ESAME "


                    par.cmd.CommandText = StringaSql

                    'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
                    Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    Dim ds4 As New Data.DataSet()
                    da4.Fill(ds4, "RENDIMENTO_TERMICI")

                    DataGrid4.DataSource = ds4
                    DataGrid4.DataBind()

                    ds4.Dispose()

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
