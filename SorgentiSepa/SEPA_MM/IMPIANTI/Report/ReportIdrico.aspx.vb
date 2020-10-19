' REPORT IMPIANTO IDRICO

Partial Class ReportIdrico
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
        Dim i As Integer
        Dim SommaUI As Integer
        Dim ID_TIPOLOGIA As Integer
        Dim dlist As CheckBoxList
        Dim ds As New Data.DataSet()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter

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
                    ' LEGGO IMPIANTO IDRICO
                    Label2.Text = "IMPIANTO CENTRALE IDRICA"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    StringaSql = " select COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO""," _
                                      & " EDIFICI.DENOMINAZIONE AS ""EDIFICIO"",IMPIANTI.* " _
                               & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI " _
                               & " where IMPIANTI.ID =" & vIdImpianto _
                               & " and   IMPIANTI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+)" _
                               & " and   IMPIANTI.ID_EDIFICIO=EDIFICI.ID (+) "

                    par.cmd.CommandText = StringaSql
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then


                        lblCodice.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

                        lblComplesso.Text = par.IfNull(myReader1("COMPLESSO"), "")
                        lblEdificio.Text = par.IfNull(myReader1("EDIFICIO"), "")


                        lblDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                        lblDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
                        lblData.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


                        Me.lblNote.Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


                        StringaSql = " select TIPOLOGIA_USO_IDRICI.DESCRIZIONE AS ""TIPOLOGIA_USO"",I_IDRICI.* " _
                                   & " from SISCOM_MI.I_IDRICI,SISCOM_MI.TIPOLOGIA_USO_IDRICI " _
                                   & " where I_IDRICI.ID =" & vIdImpianto _
                                   & " and   I_IDRICI.ID_TIPOLOGIA_USO=TIPOLOGIA_USO_IDRICI.ID (+) "

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then


                            ID_TIPOLOGIA = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), 1)

                            ' riempimento TIPOLOGIA D'USO
                            dlist = CheckBoxTipologia
                            da = New Oracle.DataAccess.Client.OracleDataAdapter("select * from SISCOM_MI.TIPOLOGIA_USO_IDRICI where ID<3 order by ID", par.OracleConn)
                            da.Fill(ds)

                            dlist.Items.Clear()
                            dlist.DataSource = ds
                            dlist.DataTextField = "DESCRIZIONE"
                            dlist.DataValueField = "ID"
                            dlist.DataBind()

                            da.Dispose()
                            da = Nothing

                            dlist.DataSource = Nothing
                            dlist = Nothing

                            ds.Clear()
                            ds.Dispose()
                            ds = Nothing

                            For i = 0 To CheckBoxTipologia.Items.Count - 1
                                If CheckBoxTipologia.Items(i).Text = "IDRICO-SANITARIO" Then
                                    CheckBoxTipologia.Items(i).Selected = True
                                    Exit For
                                End If
                            Next
                            '*********************+++

                            ' settaggiO TIPOLOGIA D'USO
                            For i = 0 To CheckBoxTipologia.Items.Count - 1
                                If CheckBoxTipologia.Items(i).Value = ID_TIPOLOGIA Or ID_TIPOLOGIA = 3 Then
                                    CheckBoxTipologia.Items(i).Selected = True
                                Else
                                    CheckBoxTipologia.Items(i).Selected = False
                                End If
                            Next
                            '********************


                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")
                            'Me.lblDataAccensione.Text = par.FormattaData(par.IfNull(myReader2("DATA_PRIMA_ATTIVAZIONE"), ""))

                            Me.chkCompressore_SI.Checked = False
                            Me.chkCompressore_NO.Checked = False
                            Me.chkISPESL_SI.Checked = False
                            Me.chkISPESL_NO.Checked = False
                            Me.chkCT_SI.Checked = False
                            Me.chkCT_NO.Checked = False
                            Me.chkConformita_SI.Checked = False
                            Me.chkConformita_NO.Checked = False


                            If par.IfNull(myReader2("COMPRESSORE"), "") = "S" Then
                                Me.chkCompressore_SI.Checked = True
                                Me.chkCompressore_NO.Checked = False
                            Else
                                Me.chkCompressore_SI.Checked = True
                                Me.chkCompressore_NO.Checked = False
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

                            If par.IfNull(myReader2("DICH_CONF"), "") = "S" Then
                                Me.chkConformita_SI.Checked = True
                                Me.chkConformita_NO.Checked = False
                            Else
                                Me.chkConformita_SI.Checked = True
                                Me.chkConformita_NO.Checked = False
                            End If
                            '*******************

                        End If
                        myReader2.Close()


                        '*** EDIFICI ALIMENTATI
                        Dim row As System.Data.DataRow
                        Dim daE As Oracle.DataAccess.Client.OracleDataAdapter

                        StringaSql = "select distinct SISCOM_MI.SCALE_EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as EDIFICIO, " _
                                                   & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as SCALA, " _
                                         & "(select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_SCALA=SCALE_EDIFICI.ID ) as UI  " _
                                 & " from SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI  " _
                                 & " where SISCOM_MI.EDIFICI.ID=SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO (+) and " _
                                        & " SISCOM_MI.SCALE_EDIFICI.ID in (select SISCOM_MI.IMPIANTI_SCALE.ID_SCALA from SISCOM_MI.IMPIANTI_SCALE where SISCOM_MI.IMPIANTI_SCALE.ID_IMPIANTO =" & vIdImpianto & ") " _
                                 & " order by EDIFICIO asc"


                        par.cmd.CommandText = StringaSql


                        daE = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                        daE.Fill(dt)

                        SommaUI = 0

                        i = 0
                        For Each row In dt.Rows
                            SommaUI = SommaUI + par.IfNull(dt.Rows(i).Item("UI"), 0)
                            i = i + 1
                        Next

                        row = dt.NewRow()
                        row.Item("EDIFICIO") = "TOTALI:"
                        'row.Item("MQ") = Format(SommaMQ, "##,##0.00")
                        row.Item("UI") = SommaUI

                        dt.Rows.Add(row)

                        DataGridEdifici.DataSource = dt
                        DataGridEdifici.DataBind()

                        dt.Dispose()

                    End If
                    myReader1.Close()


                    'SERBATOI  PRE-AUTOCLAVE
                    StringaSql = "select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE " _
                          & " from SISCOM_MI.SERBATOI_IDRICI " _
                          & " where SISCOM_MI.SERBATOI_IDRICI.TIPO_SERBATOIO='PRE-AUTOCLAVE' and " _
                                & " SISCOM_MI.SERBATOI_IDRICI.ID_IMPIANTO = " & vIdImpianto _
                          & " order by SISCOM_MI.SERBATOI_IDRICI.MODELLO "


                    par.cmd.CommandText = StringaSql

                    'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds1 As New Data.DataSet()

                    da1.Fill(ds1, "SERBATOI_IDRICI")

                    DataGrid2.DataSource = ds1
                    DataGrid2.DataBind()

                    ds1.Dispose()
                    '**********************+


                    '*** SERBATOI AUTOCLAVE
                    StringaSql = "select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE " _
                              & " from SISCOM_MI.SERBATOI_IDRICI " _
                              & " where SISCOM_MI.SERBATOI_IDRICI.TIPO_SERBATOIO='AUTOCLAVE' and " _
                                    & " SISCOM_MI.SERBATOI_IDRICI.ID_IMPIANTO = " & vIdImpianto _
                              & " order by SISCOM_MI.SERBATOI_IDRICI.MODELLO "


                    par.cmd.CommandText = StringaSql

                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds2 As New Data.DataSet()

                    da2.Fill(ds2, "SERBATOI_IDRICI")

                    DataGrid1.DataSource = ds2
                    DataGrid1.DataBind()

                    ds2.Dispose()
                    '*************************


                    '*** POMPE DI SOLLEVAMENTO
                    StringaSql = "  select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,PORTATA,PREVALENZA,DISCONNETTORE" _
                                & " from SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI " _
                                & " where SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI.ID_IMPIANTO = " & vIdImpianto _
                                & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI.MODELLO "

                    par.cmd.CommandText = StringaSql

                    Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds3 As New Data.DataSet()

                    da3.Fill(ds3, "POMPE_CIRCOLAZIONE_IDRICI")


                    DataGrid3.DataSource = ds3
                    DataGrid3.DataBind()

                    ds3.Dispose()
                    '**********************************

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
