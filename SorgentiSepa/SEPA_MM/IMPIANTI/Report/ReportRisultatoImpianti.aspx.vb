'*** STAMPA RISULTATO RICERCA IMPIANTI

Partial Class ASS_ReportRisultato
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String
    Public sValoreMatricola As String
    Public sValoreLotto As String

    Dim sOrdinamento As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO IMPIANTI"

                'Passato = Request.QueryString("Pas")


                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreImpianto = Request.QueryString("IM")

                sValoreMatricola = Request.QueryString("MA")
                sValoreLotto = Request.QueryString("LO")

                sOrdinamento = Request.QueryString("ORD")

                Select Case sOrdinamento
                    Case "COMPLESSO"
                        sOrder = " order by DENOMINAZIONE_COMPLESSO"
                    Case "EDIFICIO"
                        sOrder = " order by DENOMINAZIONE_EDIFICIO"
                    Case "TIPO IMPIANTO"
                        sOrder = " order by TIPO_IMPIANTO"
                    Case "LOCALITA"
                        sOrder = " order by LOCALITA"
                    Case "LOTTO"
                        sOrder = " order by SISCOM_MI.I_SOLLEVAMENTO.NUM_LOTTO "
                    Case Else
                        sOrder = ""
                End Select

                sStringaSql = "select  SISCOM_MI.IMPIANTI.ID AS ""ID_IMPIANTO""," _
                                    & "SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO," _
                                    & "SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DENOMINAZIONE_COMPLESSO""," _
                                    & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""DENOMINAZIONE_EDIFICIO""," _
                                    & "SISCOM_MI.INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                                    & "SISCOM_MI.INDIRIZZI.CIVICO AS ""CIVICO""," _
                                    & "SISCOM_MI.INDIRIZZI.LOCALITA AS ""LOCALITA""," _
                                    & "SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS ""TIPO_IMPIANTO""," _
                                    & "SISCOM_MI.IMPIANTI.COD_IMPIANTO," _
                                    & "SISCOM_MI.IMPIANTI.DESCRIZIONE AS ""DENOMINAZIONE_IMPIANTO"""

                If sValoreImpianto = "SO" Then

                    Me.DataGrid1.Visible = False
                    Me.DataGrid2.Visible = True

                    sStringaSql = sStringaSql & ",SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO,SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA,SISCOM_MI.I_SOLLEVAMENTO.NUM_LOTTO " _
                        & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.INDIRIZZI,SISCOM_MI.I_SOLLEVAMENTO " _
                        & " where   SISCOM_MI.IMPIANTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) and " _
                            & "     SISCOM_MI.IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) and " _
                            & "     SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=SISCOM_MI.TIPOLOGIA_IMPIANTI.COD (+) and " _
                            & "     SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO=SISCOM_MI.INDIRIZZI.ID (+) and " _
                            & "     SISCOM_MI.IMPIANTI.ID=I_SOLLEVAMENTO.ID (+)  "

                    If sValoreMatricola <> "" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA LIKE '%" & par.PulisciStrSql(sValoreMatricola) & "%' "
                    End If

                    If sValoreLotto <> "" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.I_SOLLEVAMENTO.NUM_LOTTO LIKE '%" & par.PulisciStrSql(sValoreLotto) & "%' "
                    End If
                Else
                    Me.DataGrid1.Visible = True
                    Me.DataGrid2.Visible = False

                    sStringaSql = sStringaSql & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.INDIRIZZI " _
                        & " where   SISCOM_MI.IMPIANTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) and " _
                            & "     SISCOM_MI.IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) and " _
                            & "     SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=SISCOM_MI.TIPOLOGIA_IMPIANTI.COD (+) and " _
                            & "     SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO=SISCOM_MI.INDIRIZZI.ID (+) "
                End If

                If sValoreComplesso <> "-1" And sValoreComplesso <> "" Then
                    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & sValoreComplesso
                End If

                If sValoreEdificio <> "-1" And sValoreEdificio <> "" Then
                    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & sValoreEdificio
                End If

                If sValoreImpianto <> "-1" And sValoreImpianto <> "" Then
                    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.COD_TIPOLOGIA='" & sValoreImpianto & "' "
                End If

                sStringaSql = sStringaSql & sWhere & sOrder


                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                lblTotale.Text = "0"
                Do While myReader.Read()
                    lblTotale.Text = CInt(lblTotale.Text) + 1
                Loop

                lblTotale.Text = "TOTALE IMPIANTI TROVATI: " & lblTotale.Text
                myReader.Close()

                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds)
                If sValoreImpianto = "SO" Then
                    DataGrid2.DataSource = ds
                    DataGrid2.DataBind()
                Else
                    DataGrid1.DataSource = ds
                    DataGrid1.DataBind()
                    '*******************************
                End If


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
