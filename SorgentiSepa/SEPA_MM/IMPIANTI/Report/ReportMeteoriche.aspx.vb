' REPORT IMPIANTI SOLLEVAMENTO ACQUE METEORICHE

Partial Class ReportMeteoriche
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdImpianto As String

    Dim sValoreIDALL As String
    Dim SValoreG As String
    Dim s_Stringasql As String
    Dim SValoreOfferta As String

    Dim lstScale As System.Collections.Generic.List(Of Epifani.Scale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String
        Dim i As Integer
        Dim dlist As CheckBoxList

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
                    ' LEGGO IMPIANTO SOLLEVAMENTO
                    Label2.Text = "IMPIANTO SOLLEVAMENTO ACQUE METEORICHE"

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


                        '*** CARICAMENTO SCALE e SETTAGGIO SCALE
                        lstScale.Clear()
                        par.cmd.CommandText = " select  ID, DESCRIZIONE AS SCALE " _
                                            & " from SISCOM_MI.SCALE_EDIFICI   " _
                                            & " where SISCOM_MI.SCALE_EDIFICI.ID in " _
                                                & "   (select ID_SCALA from SISCOM_MI.I_MET_SCALE " _
                                                & "    where  SISCOM_MI.I_MET_SCALE.ID_IMPIANTO=" & vIdImpianto & ") " _
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
                        par.cmd.CommandText = "select ID_SCALA from SISCOM_MI.I_MET_SCALE where  SISCOM_MI.I_MET_SCALE.ID_IMPIANTO = " & vIdImpianto

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


                        StringaSql = " select SISCOM_MI.I_METEORICHE.* " _
                                   & " from SISCOM_MI.I_METEORICHE " _
                                   & " where SISCOM_MI.I_METEORICHE.ID =" & vIdImpianto

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")


                            Me.chkQuadro_SI.Checked = False
                            Me.chkQuadro_NO.Checked = False
                            Me.chkVasca_SI.Checked = False
                            Me.chkVasca_NO.Checked = False
                            Me.chkContinuita_SI.Checked = False
                            Me.chkContinuita_NO.Checked = False
                            Me.chkDisoleatore_SI.Checked = False
                            Me.chkDisoleatore_NO.Checked = False


                            If par.IfNull(myReader2("QUADRO_ELETTRICO"), "") = "S" Then
                                Me.chkQuadro_SI.Checked = True
                                Me.chkQuadro_NO.Checked = False
                            Else
                                Me.chkQuadro_SI.Checked = False
                                Me.chkQuadro_NO.Checked = True
                            End If
                            Me.lblUbicazioneQuadro.Text = par.IfNull(myReader2("UBICAZIONE"), "")


                            If par.IfNull(myReader2("VASCA_RACCOLTA"), "") = "S" Then
                                Me.chkVasca_SI.Checked = True
                                Me.chkVasca_NO.Checked = False
                            Else
                                Me.chkVasca_SI.Checked = False
                                Me.chkVasca_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("DISOLEATORE"), "") = "S" Then
                                Me.chkDisoleatore_SI.Checked = True
                                Me.chkDisoleatore_NO.Checked = False
                            Else
                                Me.chkDisoleatore_SI.Checked = False
                                Me.chkDisoleatore_NO.Checked = True
                            End If

                            If par.IfNull(myReader2("IMP_CONTINUITA"), "") = "S" Then
                                Me.chkContinuita_SI.Checked = True
                                Me.chkContinuita_NO.Checked = False
                            Else
                                Me.chkContinuita_SI.Checked = False
                                Me.chkContinuita_NO.Checked = True
                            End If
                            Me.lblDurata.Text = par.IfNull(myReader2("DURATA"), "")

                        End If
                        myReader2.Close()
                    End If
                    myReader1.Close()



                    '*** ELENCO POMPE DI SOLLEVAMENTO
                    StringaSql = "  select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,TIPO,POTENZA,PORTATA,PREVALENZA " _
                                & " from SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO " _
                                & " where SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO.ID_IMPIANTO = " & vIdImpianto _
                                & " order by SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO.MODELLO "


                    par.cmd.CommandText = StringaSql

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds As New Data.DataSet()

                    da.Fill(ds, "I_MET_POMPE_SOLLEVAMENTO")

                    DataGrid3.DataSource = ds
                    DataGrid3.DataBind()

                    ds.Dispose()
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
