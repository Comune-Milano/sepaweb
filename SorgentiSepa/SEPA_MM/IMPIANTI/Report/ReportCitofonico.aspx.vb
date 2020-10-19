' REPORT IMPIANTI CITOFONICO

Partial Class ReportCitofonico
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
                    ' LEGGO IMPIANTO CITOFONICO
                    Label2.Text = "IMPIANTO CITOFONICO"

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

                        '*** I_CITOFONI
                        par.cmd.CommandText = "select * from SISCOM_MI.I_CITOFONI where SISCOM_MI.I_CITOFONI.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            '*** TAB GENERALE
                            Me.lblDittaInstallatrice.Text = par.IfNull(myReader2("DITTA_INSTALLAZIONE"), "")
                            Me.lblTelInstalla.Text = par.IfNull(myReader2("TELEFONO_DITTA_INST"), "")

                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTelGestione.Text = par.IfNull(myReader2("TELEFONO_DITTA_GEST"), "")

                        End If
                    End If
                    myReader1.Close()


                    '*** ELENCO DETTAGLI CITOFONO
                    StringaSql = "select SISCOM_MI.I_CIT_DETTAGLI.ID,SISCOM_MI.I_CIT_DETTAGLI.TIPOLOGIA,SISCOM_MI.I_CIT_DETTAGLI.UBICAZIONE," _
                                      & "SISCOM_MI.I_CIT_DETTAGLI.TASTIERA,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                                      & "SISCOM_MI.I_CIT_DETTAGLI.ID_TIPO_DISTRIBUZIONE, SISCOM_MI.I_CIT_DETTAGLI.QUANTITA," _
                                   & " (select count(*) from SISCOM_MI.I_CIT_DETTAGLI_SCALE where ID_I_CIT_DETTAGLI=SISCOM_MI.I_CIT_DETTAGLI.ID) AS ""SCALE_SERVITE"" " _
                              & " from SISCOM_MI.I_CIT_DETTAGLI,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
                              & " where SISCOM_MI.I_CIT_DETTAGLI.ID_IMPIANTO = " & vIdImpianto _
                              & " and   SISCOM_MI.I_CIT_DETTAGLI.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
                              & " order by SISCOM_MI.I_CIT_DETTAGLI.ID "

                    par.cmd.CommandText = StringaSql

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds As New Data.DataSet()

                    da.Fill(ds, "I_CIT_DETTAGLI")

                    DataGridCI.DataSource = ds
                    DataGridCI.DataBind()

                    ds.Dispose()
                    '****************************


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
