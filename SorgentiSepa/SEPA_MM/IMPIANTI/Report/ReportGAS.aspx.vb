' REPORT GAS DEGLI IMPIANTI
Partial Class ReportGAS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdImpianto As String

    Dim sValoreIDALL As String
    Dim SValoreG As String
    Dim s_Stringasql As String
    Dim SValoreOfferta As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String
        Dim ds As New Data.DataSet()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dlist As CheckBoxList

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
                    ' LEGGO IMPIANTO GAS
                    Label2.Text = "IMPIANTO GAS"

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


                        StringaSql = " select SISCOM_MI.I_GAS.*,SISCOM_MI.TIPOLOGIA_TUBI_GAS.DESCRIZIONE AS ""TIPO_TUBAZIONE"" " _
                                   & " from SISCOM_MI.I_GAS,SISCOM_MI.TIPOLOGIA_TUBI_GAS " _
                                   & " where SISCOM_MI.I_GAS.ID =" & vIdImpianto _
                                        & " and SISCOM_MI.I_GAS.ID_TIPOLOGIA_TUBI_GAS=SISCOM_MI.TIPOLOGIA_TUBI_GAS.ID (+) "

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

                            Me.lblTubazione.Text = par.IfNull(myReader2("TIPO_TUBAZIONE"), "")

                        End If
                        myReader2.Close()
                    End If
                    myReader1.Close()

                    '*** ELENCO SERVIZI GAS
                    dlist = chkTipoServizio

                    StringaSql = " select SISCOM_MI.TIPOLOGIA_SERVIZI_GAS.ID,SISCOM_MI.TIPOLOGIA_SERVIZI_GAS.DESCRIZIONE " _
                               & " from(SISCOM_MI.TIPOLOGIA_SERVIZI_GAS) " _
                               & " where SISCOM_MI.TIPOLOGIA_SERVIZI_GAS.ID in " _
                                    & " (select SISCOM_MI.I_GAS_SERVIZI.ID_TIPOLOGIA_SERVIZI_GAS from SISCOM_MI.I_GAS_SERVIZI where SISCOM_MI.I_GAS_SERVIZI.ID_GAS=" & vIdImpianto & ") " _
                               & " order by SISCOM_MI.TIPOLOGIA_SERVIZI_GAS.DESCRIZIONE "

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, par.OracleConn)
                    da.Fill(ds)

                    dlist.Items.Clear()
                    dlist.DataSource = ds
                    dlist.DataTextField = "DESCRIZIONE"
                    dlist.DataValueField = "ID"
                    dlist.DataBind()

                    Dim i As Integer
                    For i = 0 To chkTipoServizio.Items.Count - 1
                        chkTipoServizio.Items(i).Selected = True
                    Next
                    '**********************************************************

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
