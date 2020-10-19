' REPORT IMPIANTI TV

Partial Class ReportTV
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
                    ' LEGGO IMPIANTO TV
                    Label2.Text = "IMPIANTO TV"

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

                        '*** I_TV
                        par.cmd.CommandText = "select * from SISCOM_MI.I_TV where SISCOM_MI.I_TV.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            '*** TAB GENERALE
                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA_GEST"), "")
                            Me.lblPrese.Text = par.IfNull(myReader2("NUM_PRESE"), "")

                        End If
                    End If
                    myReader1.Close()


                    '*** ELENCO DETTAGLI TV
                    StringaSql = "select SISCOM_MI.I_TV_DETTAGLI.ID,SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_SCALA," _
                                        & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                                        & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA"",SISCOM_MI.I_TV_DETTAGLI.DITTA_INSTALLAZIONE," _
                                        & " TO_CHAR(TO_DATE(SISCOM_MI.I_TV_DETTAGLI.DATA_INSTALLAZIONE,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_INSTALLAZIONE""," _
                                        & "SISCOM_MI.I_TV_DETTAGLI.CENTRALINO_TV,SISCOM_MI.I_TV_DETTAGLI.IMPIANTO,SISCOM_MI.I_TV_DETTAGLI.TIPO_IMPIANTO," _
                                        & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE"",SISCOM_MI.I_TV_DETTAGLI.ID_TIPO_DISTRIBUZIONE," _
                                        & " (select count(*) from SISCOM_MI.I_TV_DETTAGLI_EDIFICI where  SISCOM_MI.I_TV_DETTAGLI_EDIFICI.ID_TV_DETTAGLI=SISCOM_MI.I_TV_DETTAGLI.ID) AS ""FABB_SERVITI"", " _
                                        & "SISCOM_MI.I_TV_DETTAGLI.NOTE " _
                          & " from  SISCOM_MI.I_TV_DETTAGLI,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
                          & " where SISCOM_MI.I_TV_DETTAGLI.ID_IMPIANTO = " & vIdImpianto _
                          & " and   SISCOM_MI.I_TV_DETTAGLI.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
                          & " and   SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
                          & " and   SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
                          & " order by SISCOM_MI.I_TV_DETTAGLI.ID "


                    par.cmd.CommandText = StringaSql

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds As New Data.DataSet()

                    da.Fill(ds, "I_TV_DETTAGLI")

                    DataGridTV.DataSource = ds
                    DataGridTV.DataBind()

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
