' REPORT CANNA FUMARIA DEGLI IMPIANTI
Partial Class ReportCannaFumaria
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
                    Label2.Text = "IMPIANTO DI CANNA FUMARIA"

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


                        StringaSql = " select SISCOM_MI.I_CANNA_FUMARIA.* " _
                                   & " from SISCOM_MI.I_CANNA_FUMARIA " _
                                   & " where SISCOM_MI.I_CANNA_FUMARIA.ID =" & vIdImpianto

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblTipologia.Text = par.IfNull(myReader2("TIPOLOGIA"), "")
                            Me.lblServitu.Text = par.IfNull(myReader2("TIPO_SERVITU"), "")

                            Me.lblNumUtenze.Text = par.IfNull(myReader2("NUM_UTENZE"), "")
                            Me.lblConformita.Text = par.IfNull(myReader2("DICH_CONF"), "")

                            Me.lblPotenzaTOT.Text = par.IfNull(myReader2("POTENZA_TOTALE"), "")
                            Me.lblPotenza.Text = par.IfNull(myReader2("POTENZA_UTENZA"), "")

                        End If
                        myReader2.Close()
                    End If
                    myReader1.Close()

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
