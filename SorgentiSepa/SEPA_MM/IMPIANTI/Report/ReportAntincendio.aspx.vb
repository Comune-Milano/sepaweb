' REPORT IMPIANTI ANTINCENDIO

Partial Class ReportAntincendio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdImpianto As String

    Dim sValoreIDALL As String
    Dim SValoreG As String
    Dim s_Stringasql As String
    Dim SValoreOfferta As String

    Dim dt As New Data.DataTable

    Dim lstScale As System.Collections.Generic.List(Of Epifani.Scale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String
        Dim i As Integer
        Dim dlist As CheckBoxList
        Dim SommaUI As Integer

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        lstScale = CType(HttpContext.Current.Session.Item("LSTSCALE"), System.Collections.Generic.List(Of Epifani.Scale))


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
                    ' LEGGO IMPIANTO ANTINCENDIO
                    Label2.Text = "IMPIANTO ANTINCENDIO"

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

                        '*** CARICAMENTO SCALE
                        lstScale.Clear()
                        par.cmd.CommandText = " select  ID, DESCRIZIONE AS SCALE " _
                                            & " from SISCOM_MI.SCALE_EDIFICI   " _
                                            & " where SISCOM_MI.SCALE_EDIFICI.ID in " _
                                                & "   (select ID_SCALA from SISCOM_MI.I_ANT_SCALE " _
                                                & "    where  SISCOM_MI.I_ANT_SCALE.ID_IMPIANTO=" & vIdImpianto & ") " _
                                            & " order by SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE asc "


                        myReader2 = par.cmd.ExecuteReader()
                        While myReader2.Read
                            Dim gen As Epifani.Scale
                            gen = New Epifani.Scale(par.IfNull(myReader2("ID"), -1), par.IfNull(myReader1("EDIFICIO"), ""), par.IfNull(myReader2("SCALE"), " "))
                            lstScale.Add(gen)
                            gen = Nothing
                        End While
                        myReader2.Close()

                        'SETTAGGIO BOX e CT
                        par.cmd.CommandText = "select ID_SCALA from SISCOM_MI.I_ANT_SCALE where  SISCOM_MI.I_ANT_SCALE.ID_IMPIANTO = " & vIdImpianto

                        myReader2 = par.cmd.ExecuteReader()
                        While myReader2.Read
                            If par.IfNull(myReader2("ID_SCALA"), -1) = -1 Then
                                Dim gen As Epifani.Scale
                                gen = New Epifani.Scale(-1, " ", "C.T.")
                                lstScale.Add(gen)
                                gen = Nothing
                            End If

                            If par.IfNull(myReader2("ID_SCALA"), -2) = -2 Then
                                Dim gen As Epifani.Scale
                                gen = New Epifani.Scale(-1, " ", "BOX")
                                lstScale.Add(gen)
                                gen = Nothing
                            End If
                        End While
                        myReader2.Close()


                        dlist = CheckBoxScale
                        dlist.DataSource = lstScale

                        dlist.DataTextField = "SCALE_NO_TITLE"
                        dlist.DataValueField = "ID"
                        dlist.DataBind()

                        For i = 0 To CheckBoxScale.Items.Count - 1
                            CheckBoxScale.Items(i).Selected = True
                        Next
                        '*********************************


                        lblComplesso.Text = par.IfNull(myReader1("COMPLESSO"), "")
                        lblEdificio.Text = par.IfNull(myReader1("EDIFICIO"), "")

                        lblDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

                        lblDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
                        lblData.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))

                        Me.lblNote.Text = par.IfNull(myReader1("ANNOTAZIONI"), "")

                        'SISCOM_MI.TIPOLOGIA_USO_ANTINCENDIO.DESCRIZIONE AS ""TIPOLOGIA_USO""
                        StringaSql = " select SISCOM_MI.I_ANTINCENDIO.* " _
                                   & " from SISCOM_MI.I_ANTINCENDIO " _
                                   & " where SISCOM_MI.I_ANTINCENDIO.ID =" & vIdImpianto

                        '&(" and   SISCOM_MI.I_ANTINCENDIO.ID_TIPOLOGIA_USO=SISCOM_MI.TIPOLOGIA_USO_ANTINCENDIO.ID (+) ")

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

                            'Me.lblTipo.Text = par.IfNull(myReader2("TIPOLOGIA_USO"), "")

                            Me.lblGruppo.Text = par.IfNull(myReader2("GRUPPO_ELETTROGENO"), "")
                            Me.lblPresenzaBox.Text = par.IfNull(myReader2("PRESENZA_BOX"), "")
                            'Me.lblEstintori.Text = par.IfNull(myReader2("NUM_ESTINTORI"), "")

                        End If
                        myReader2.Close()

                        '*** EDIFICI ALIMENTATI
                        Dim row As System.Data.DataRow
                        Dim daED As Oracle.DataAccess.Client.OracleDataAdapter

                        StringaSql = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as EDIFICIO, " _
                                         & "(select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID ) as UI  " _
                                 & " from SISCOM_MI.EDIFICI " _
                                 & " where ID in " _
                              & " (select SISCOM_MI.IMPIANTI_EDIFICI.ID_EDIFICIO from SISCOM_MI.IMPIANTI_EDIFICI where  SISCOM_MI.IMPIANTI_EDIFICI.ID_IMPIANTO =" & vIdImpianto & ") " _
                                 & " order by EDIFICIO asc"

                        par.cmd.CommandText = StringaSql


                        daED = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                        daED.Fill(dt)

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


                    ' SETTAGGIO SCALE
                    par.cmd.CommandText = "select ID_SCALA from SISCOM_MI.I_ANT_SCALE where  SISCOM_MI.I_ANT_SCALE.ID_IMPIANTO = " & vIdImpianto
                    myReader2 = par.cmd.ExecuteReader()

                    While myReader2.Read

                        For i = 0 To CheckBoxScale.Items.Count - 1
                            If CheckBoxScale.Items(i).Value = par.IfNull(myReader2("ID_SCALA"), "-1") Then
                                CheckBoxScale.Items(i).Selected = True
                            End If
                        Next
                    End While
                    myReader2.Close()



                    '*** ELENCO MOTOPOMPE UNI70
                    StringaSql = "select SISCOM_MI.I_ANT_MOTOPOMPE.ID,SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_SCALA," _
                                        & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                                        & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA""," _
                                        & " (select count(*) from SISCOM_MI.I_ANT_MOTOPOMPE_SCALE where  SISCOM_MI.I_ANT_MOTOPOMPE_SCALE.ID_I_ANT_MOTOPOMPE=SISCOM_MI.I_ANT_MOTOPOMPE.ID) AS ""SCALE_SERVITE"" " _
                          & " from  SISCOM_MI.I_ANT_MOTOPOMPE,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
                          & " where SISCOM_MI.I_ANT_MOTOPOMPE.ID_IMPIANTO = " & vIdImpianto _
                          & " and   SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
                          & " and   SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
                          & " order by SISCOM_MI.I_ANT_MOTOPOMPE.ID "


                    par.cmd.CommandText = StringaSql

                    Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds1 As New Data.DataSet()

                    da1.Fill(ds1, "I_ANT_MOTOPOMPE")

                    DataGridMotopompa.DataSource = ds1
                    DataGridMotopompa.DataBind()

                    ds1.Dispose()
                    '*************************

                    '*** ELENCO SERBATOI ACCUMULO
                    StringaSql = "select SISCOM_MI.I_ANT_SERBATOI.ID,SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_SCALA," _
                                            & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                                            & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA"",SISCOM_MI.I_ANT_SERBATOI.CAPACITA" _
                              & " from  SISCOM_MI.I_ANT_SERBATOI,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
                              & " where SISCOM_MI.I_ANT_SERBATOI.ID_IMPIANTO = " & vIdImpianto _
                              & " and   SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
                              & " and   SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
                              & " order by SISCOM_MI.I_ANT_SERBATOI.ID "


                    par.cmd.CommandText = StringaSql


                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds2 As New Data.DataSet()

                    da2.Fill(ds2, "I_ANT_SERBATOI")

                    DataGridSerbatoio.DataSource = ds2
                    DataGridSerbatoio.DataBind()

                    ds2.Dispose()


                    '*** ELENCO SPRINKLER
                    StringaSql = "select SISCOM_MI.I_ANT_SPRINKLER.ID,SISCOM_MI.I_ANT_SPRINKLER.ALLACCIAMENTO," _
                                        & " SISCOM_MI.TIPOLOGIA_SPRINKLER.DESCRIZIONE AS ""SPRINKLER"",SISCOM_MI.I_ANT_SPRINKLER.CERTIFICAZIONI,SISCOM_MI.I_ANT_SPRINKLER.ID_TIPOLOGIA_SPRINKLER " _
                              & " from SISCOM_MI.I_ANT_SPRINKLER,SISCOM_MI.TIPOLOGIA_SPRINKLER " _
                              & " where SISCOM_MI.I_ANT_SPRINKLER.ID_IMPIANTO = " & vIdImpianto _
                              & " and SISCOM_MI.I_ANT_SPRINKLER.ID_TIPOLOGIA_SPRINKLER=SISCOM_MI.TIPOLOGIA_SPRINKLER.ID (+) " _
                              & " order by SISCOM_MI.I_ANT_SPRINKLER.ALLACCIAMENTO "

                    par.cmd.CommandText = StringaSql

                    Dim daS As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsS As New Data.DataSet()

                    daS.Fill(dsS, "I_ANT_SPRINKLER")

                    DataGridS.DataSource = dsS
                    DataGridS.DataBind()

                    dsS.Dispose()


                    '*** ELENCO RILEVATORI FUMI
                    StringaSql = "select SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID," _
                                        & " SISCOM_MI.TIPOLOGIA_RILEVAZIONE_FUMI.DESCRIZIONE AS ""TIPOLOGIA_FUMI"",SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.UBICAZIONE_CENTRALINA,SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID_TIPOLOGIA_RILEVAZIONE " _
                              & " from SISCOM_MI.I_ANT_RILEVAZIONE_FUMI,SISCOM_MI.TIPOLOGIA_RILEVAZIONE_FUMI " _
                              & " where SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID_IMPIANTO = " & vIdImpianto _
                              & " and SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID_TIPOLOGIA_RILEVAZIONE=SISCOM_MI.TIPOLOGIA_RILEVAZIONE_FUMI.ID (+) " _
                              & " order by TIPOLOGIA_FUMI "

                    par.cmd.CommandText = StringaSql

                    Dim daF As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsF As New Data.DataSet()

                    daF.Fill(dsF, "I_ANT_RILEVAZIONE_FUMI")

                    DataGridF.DataSource = dsF
                    DataGridF.DataBind()

                    dsF.Dispose()


                    '*** ELENCO IDRANTI
                    StringaSql = "  select SISCOM_MI.I_ANT_IDRANTI.ID,(select count(*) from SISCOM_MI.I_ANT_IDRANTI_PIANI where  SISCOM_MI.I_ANT_IDRANTI_PIANI.ID_ANT_IDRANTI=SISCOM_MI.I_ANT_IDRANTI.ID) AS ""PIANI""," _
                                    & " SISCOM_MI.I_ANT_IDRANTI.DIAMETRO, SISCOM_MI.I_ANT_IDRANTI.NUM_IDRANTI, SISCOM_MI.I_ANT_IDRANTI.LOCALIZZAZIONE, " _
                                    & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                                    & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.I_ANT_IDRANTI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.I_ANT_IDRANTI.ID_IMPIANTO=" & vIdImpianto _
                                    & " and SISCOM_MI.I_ANT_IDRANTI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ID' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) " _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "

                    par.cmd.CommandText = StringaSql

                    Dim daI As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsI As New Data.DataSet()

                    daI.Fill(dsI, "I_ANT_IDRANTI")

                    DataGridI.DataSource = dsI
                    DataGridI.DataBind()

                    dsI.Dispose()


                    '*** ELENCO ESTINTORI
                    StringaSql = "  select SISCOM_MI.I_ANT_ESTINTORI.ID,SISCOM_MI.I_ANT_ESTINTORI.ESTINTORI, " _
                                    & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                                    & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.I_ANT_ESTINTORI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.I_ANT_ESTINTORI.ID_IMPIANTO=" & vIdImpianto _
                                    & " and SISCOM_MI.I_ANT_ESTINTORI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ES' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) " _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "

                    par.cmd.CommandText = StringaSql

                    Dim daE As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsE As New Data.DataSet()

                    daE.Fill(dsE, "I_ANT_ESTINTORI")

                    DataGridE.DataSource = dsE
                    DataGridE.DataBind()

                    dsE.Dispose()

                    '*** ELENCO ATTACCHI AUTOPOMPE
                    StringaSql = "  select SISCOM_MI.I_ANT_AUTOPOMPA.ID," _
                                    & " (select count(*) from SISCOM_MI.I_ANT_AUTOPOMPA_PIANI where  SISCOM_MI.I_ANT_AUTOPOMPA_PIANI.ID_ANT_AUTOPOMPA=SISCOM_MI.I_ANT_AUTOPOMPA.ID) AS ""PIANI""," _
                                    & " SISCOM_MI.I_ANT_AUTOPOMPA.BOCCA_COLLEGAMENTO " _
                                & " from SISCOM_MI.I_ANT_AUTOPOMPA " _
                                & " where SISCOM_MI.I_ANT_AUTOPOMPA.ID_IMPIANTO=" & vIdImpianto _
                                & " order by PIANI "

                    par.cmd.CommandText = StringaSql

                    Dim daA As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsA As New Data.DataSet()

                    daA.Fill(dsA, "I_ANT_AUTOPOMPA")

                    DataGridAUTOP.DataSource = dsA
                    DataGridAUTOP.DataBind()

                    dsA.Dispose()


                    ' VERIFICHE
                    'NOTA TIPO=PR (PRESSIONE RESIDUA)
                    StringaSql = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                            & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                            & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                            & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PR'" _
                                & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


                    par.cmd.CommandText = StringaSql

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "IMPIANTI_VERIFICHE")

                    DataGrid1.DataSource = ds
                    DataGrid1.DataBind()

                    ds.Dispose()
                    '******************************

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
