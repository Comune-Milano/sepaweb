'*** STAMPA RISULTATO RICERCA VERIFICHE IMPIANTI

Partial Class ReportRisultatoVerifiche
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String

    Dim sValoreComplesso As String
    Dim sValoreEdificio As String
    Dim sValoreImpianto As String
    Dim sVerifiche As String

    Dim lstVerificheRicerche As System.Collections.Generic.List(Of Epifani.VerificheRicerche)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Data1 As String
        Dim GiorniTrascorsi As Integer
        Dim GiorniPreAllarme As Integer


        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        lstVerificheRicerche = CType(HttpContext.Current.Session.Item("LSTVERIFICHE_RICERCHE"), System.Collections.Generic.List(Of Epifani.VerificheRicerche))

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO VERIFICHE IMPIANTI"

                'Passato = Request.QueryString("Pas")

                lstVerificheRicerche.Clear()

                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreImpianto = Request.QueryString("IM")
                sVerifiche = Request.QueryString("VER")


                sStringaSql = "select SISCOM_MI.IMPIANTI.ID AS ""ID_IMPIANTO"", SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICHE""," _
                                               & "SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO," _
                                               & "SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DENOMINAZIONE_COMPLESSO""," _
                                               & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""DENOMINAZIONE_EDIFICIO""," _
                                               & "SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS ""TIPO_IMPIANTO""," _
                                               & "SISCOM_MI.IMPIANTI.DESCRIZIONE AS ""DENOMINAZIONE_IMPIANTO""," _
                                               & "DECODE(SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,'PR','PRESSIONE RESIDUA','TP','PERIODICHE','TS','SCALE','PB','BIENNALI','ST','STRAORDINARIE') AS ""TIPO_VERIFICA"", " _
                                               & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_VERIFICA"", " _
                                               & "SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA AS ""VALIDITA"", " _
                                               & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA""," _
                                               & "SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.TIPOLOGIA_IMPIANTI.COD AS ""CODICE_IMPIANTO"" " _
                                       & " from SISCOM_MI.IMPIANTI_VERIFICHE,SISCOM_MI.IMPIANTI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI ,SISCOM_MI.TIPOLOGIA_IMPIANTI " _
                                       & " where     SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) and " _
                                       & "     SISCOM_MI.IMPIANTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) and " _
                                       & "     SISCOM_MI.IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) and " _
                                       & "     SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=SISCOM_MI.TIPOLOGIA_IMPIANTI.COD (+) and " _
                                       & "     SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' "


                If sValoreComplesso <> "-1" And sValoreComplesso <> "" Then
                    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & sValoreComplesso
                End If

                If sValoreEdificio <> "-1" And sValoreEdificio <> "" Then
                    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & sValoreEdificio
                End If

                If sValoreImpianto <> "-1" And sValoreImpianto <> "" Then
                    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.COD_TIPOLOGIA='" & sValoreImpianto & "' "
                End If

                sStringaSql = sStringaSql & sWhere



                ' FILTRO SCADENZE
                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                lblTotale.Text = "0"

                While myReader1.Read

                    Data1 = par.IfNull(myReader1("SCADENZA"), "")

                    If Strings.Len(Data1) > 0 Then
                        GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(par.FormattaData(Data1)), CDate(Now.ToString("dd/MM/yyyy")))
                        GiorniPreAllarme = par.IfNull(myReader1("MESI_PREALLARME"), 1) * 30

                        Select Case sVerifiche
                            Case "SCADUTE"

                                If GiorniTrascorsi > 0 Then
                                    lblTotale.Text = CInt(lblTotale.Text) + 1

                                    Dim gen As Epifani.VerificheRicerche
                                    gen = New Epifani.VerificheRicerche(par.IfNull(myReader1("ID_IMPIANTO"), -1), par.IfNull(myReader1("ID_VERIFICHE"), -1), par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_EDIFICIO"), " "), par.IfNull(myReader1("TIPO_IMPIANTO"), " "), par.IfNull(myReader1("TIPO_VERIFICA"), " "), par.IfNull(myReader1("DATA_VERIFICA"), " "), par.IfNull(myReader1("VALIDITA"), 12), par.IfNull(myReader1("SCADENZA"), " "), par.IfNull(myReader1("CODICE_IMPIANTO"), " "))
                                    lstVerificheRicerche.Add(gen)
                                    gen = Nothing
                                End If

                            Case "IN SCADENZA"
                                If GiorniTrascorsi >= -GiorniPreAllarme And GiorniTrascorsi <= 0 Then
                                    lblTotale.Text = CInt(lblTotale.Text) + 1

                                    Dim gen As Epifani.VerificheRicerche
                                    gen = New Epifani.VerificheRicerche(par.IfNull(myReader1("ID_IMPIANTO"), -1), par.IfNull(myReader1("ID_VERIFICHE"), -1), par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_EDIFICIO"), " "), par.IfNull(myReader1("TIPO_IMPIANTO"), " "), par.IfNull(myReader1("TIPO_VERIFICA"), " "), par.IfNull(myReader1("DATA_VERIFICA"), " "), par.IfNull(myReader1("VALIDITA"), 12), par.IfNull(myReader1("SCADENZA"), " "), par.IfNull(myReader1("CODICE_IMPIANTO"), " "))
                                    lstVerificheRicerche.Add(gen)
                                    gen = Nothing
                                End If

                            Case "NON SCADUTE"
                                If GiorniTrascorsi < -GiorniPreAllarme And GiorniTrascorsi <= 0 Then
                                    lblTotale.Text = CInt(lblTotale.Text) + 1

                                    Dim gen As Epifani.VerificheRicerche
                                    gen = New Epifani.VerificheRicerche(par.IfNull(myReader1("ID_IMPIANTO"), -1), par.IfNull(myReader1("ID_VERIFICHE"), -1), par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_EDIFICIO"), " "), par.IfNull(myReader1("TIPO_IMPIANTO"), " "), par.IfNull(myReader1("TIPO_VERIFICA"), " "), par.IfNull(myReader1("DATA_VERIFICA"), " "), par.IfNull(myReader1("VALIDITA"), 12), par.IfNull(myReader1("SCADENZA"), " "), par.IfNull(myReader1("CODICE_IMPIANTO"), " "))
                                    lstVerificheRicerche.Add(gen)
                                    gen = Nothing
                                End If

                        End Select
                    End If
                End While
                myReader1.Close()

                DataGrid1.DataSource = Nothing
                DataGrid1.DataSource = lstVerificheRicerche
                DataGrid1.DataBind()

                lblTotale.Text = "TOTALE IMPIANTI TROVATI: " & lblTotale.Text

                cmd.Dispose()
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



    End Sub

    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property

End Class
