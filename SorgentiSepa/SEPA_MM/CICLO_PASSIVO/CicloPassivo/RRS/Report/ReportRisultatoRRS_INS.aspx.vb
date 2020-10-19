'*** STAMPA RISULTATO RICERCA MANUTENZIONE PRE-INSERIMENTO

Partial Class ReportRisultatoRRS_INS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Dim sValoreIndirizzoR As String
    Dim sValoreVoceR As String
    Dim sValoreAppaltoR As String
    Dim sValoreComplessoR As String
    Dim sValoreEdificioR As String
    Dim sValoreTipoR As String
    Dim sValoreUbicazione As String

    Dim sOrdinamento As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sStringaSqlPatrimonio As String = ""
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO MANUTENZIONI GESTIONE NON PATRIMONIALE"

                'Passato = Request.QueryString("Pas")


                sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
                sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
                sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

                sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))

                sOrdinamento = Request.QueryString("ORD")



                Select Case sOrdinamento
                    Case "NUM_REPERTORIO"
                        sOrder = " order by NUM_REPERTORIO asc"
                    Case "INDIRIZZO"
                        sOrder = " order by INDIRIZZO asc"
                    Case "VOCE"
                        sOrder = " order by COD_VOCE,VOCE asc"
                    Case Else
                        sOrder = ""
                End Select


                DataGrid1.Visible = True

                If sValoreUbicazione = 0 Then
                    'COMPLESSO

                    sStringaSql = "select APPALTI_VOCI_PF.ID_APPALTO,APPALTI_VOCI_PF.ID_PF_VOCE,EDIFICI.ID as ""ID_EDIFICIO"",COMPLESSI_IMMOBILIARI.ID as ""ID_COMPLESSO""," _
                                      & " APPALTI.NUM_REPERTORIO," _
                                      & " PF_VOCI.CODICE AS ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""VOCE""," _
                                      & " SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO"", " _
                                      & " EDIFICI.DENOMINAZIONE as ""DESC_EDIFICIO"", " _
                                      & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP)  AS ""INDIRIZZO"", appalti.descrizione as descrizione_appalti" _
                             & " from  SISCOM_MI.APPALTI_VOCI_PF, " _
                                   & " SISCOM_MI.APPALTI, " _
                                   & " SISCOM_MI.PF_VOCI, " _
                                   & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                   & " SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI " _
                             & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                    & " where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')" _
                             & "   and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID=SISCOM_MI.EDIFICI.ID_COMPLESSO (+) " _
                             & "   and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO=SISCOM_MI.INDIRIZZI.ID (+) " _
                             & "   and APPALTI_VOCI_PF.ID_PF_VOCE=" & sValoreVoceR _
                             & "   and APPALTI_VOCI_PF.ID_APPALTO=" & sValoreAppaltoR _
                             & "   and APPALTI_VOCI_PF.ID_PF_VOCE=PF_VOCI.ID (+) " _
                             & "   and APPALTI_VOCI_PF.ID_APPALTO=APPALTI.ID (+) "

                Else

                    sStringaSql = "select APPALTI_VOCI_PF.ID_APPALTO,APPALTI_VOCI_PF.ID_PF_VOCE,EDIFICI.ID as ""ID_EDIFICIO"",COMPLESSI_IMMOBILIARI.ID as ""ID_COMPLESSO""," _
                                      & " APPALTI.NUM_REPERTORIO," _
                                      & " PF_VOCI.CODICE AS ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""VOCE""," _
                                      & " SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO"", " _
                                      & " EDIFICI.DENOMINAZIONE as DESC_EDIFICIO, " _
                                      & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP)  AS INDIRIZZO, appalti.descrizione as descrizione_appalti " _
                             & " from  SISCOM_MI.APPALTI_VOCI_PF, " _
                                   & " SISCOM_MI.APPALTI, " _
                                   & " SISCOM_MI.PF_VOCI, " _
                                   & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                   & " SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI " _
                             & " where SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                    & " where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')" _
                             & "   and SISCOM_MI.EDIFICI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                             & "   and SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE=SISCOM_MI.INDIRIZZI.ID (+) " _
                             & "   and APPALTI_VOCI_PF.ID_PF_VOCE=" & sValoreVoceR _
                             & "   and APPALTI_VOCI_PF.ID_APPALTO=" & sValoreAppaltoR _
                             & "   and APPALTI_VOCI_PF.ID_PF_VOCE=PF_VOCI.ID (+) " _
                             & "   and APPALTI_VOCI_PF.ID_APPALTO=APPALTI.ID (+) "

                End If

                sStringaSql = sStringaSql & sOrder



                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                lblTotale.Text = "0"
                Do While myReader.Read()
                    lblTotale.Text = CInt(lblTotale.Text) + 1
                Loop

                lblTotale.Text = "TOTALE MANUTENZIONI TROVATE: " & lblTotale.Text
                myReader.Close()

                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds)

                DataGrid1.DataSource = ds
                DataGrid1.DataBind()
                

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
