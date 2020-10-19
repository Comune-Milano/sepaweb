
Partial Class CALL_CENTER_VerificaCondomino
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            lblNome.Text = UCase(Request.QueryString("C") & " " & Request.QueryString("N"))
            NomeRichiedente = Request.QueryString("N")
            CognomeRichiedente = Request.QueryString("C")
            tipo.Value = Request.QueryString("T")
            identificativo.Value = Request.QueryString("ID")
            Verifica()

        End If
    End Sub

    Public Property NomeRichiedente() As String
        Get
            If Not (ViewState("par_NomeRichiedente") Is Nothing) Then
                Return CStr(ViewState("par_NomeRichiedente"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_NomeRichiedente") = value
        End Set

    End Property

    Public Property CognomeRichiedente() As String
        Get
            If Not (ViewState("par_CognomeRichiedente") Is Nothing) Then
                Return CStr(ViewState("par_CognomeRichiedente"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CognomeRichiedente") = value
        End Set

    End Property

    Private Function Verifica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Select Case tipo.Value
                Case "C"
                    Dim complesso As Boolean
                    par.cmd.CommandText = "SELECT DISTINCT RAPPORTI_UTENZA.COD_CONTRATTO,rapporti_utenza.id " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI " _
                        & "WHERE UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO AND " _
                        & "EDIFICI.ID_COMPLESSO=" & identificativo.Value & " AND RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_ANAGRAFICA IN " _
                        & "(select id from SISCOM_MI.anagrafica where (cognome='" & UCase(par.PulisciStrSql(CognomeRichiedente)) _
                        & "' OR RAGIONE_SOCIALE='" & UCase(par.PulisciStrSql(CognomeRichiedente)) & "') AND NOME='" & UCase(par.PulisciStrSql(NomeRichiedente)) & "'))"

                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.HasRows = False Then
                        lblDettagli.Text = "Non risulta in alcun rapporto nel complesso selezionato."
                    Else
                        lblDettagli.Text = "Presente nel/i rapporti del complesso selezionato:<br/>"
                        complesso = True
                    End If
                    Do While myReader5.Read
                        lblDettagli.Text = lblDettagli.Text & "Cod. " & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader5("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & myReader5("COD_CONTRATTO") & "</a><br/>"
                    Loop
                    myReader5.Close()

                    If complesso = False Then
                        par.cmd.CommandText = "SELECT DISTINCT RAPPORTI_UTENZA.COD_CONTRATTO,rapporti_utenza.id " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI " _
                        & "WHERE UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO AND " _
                        & "RAPPORTI_UTENZA.ID IN " _
                        & "(SELECT ID_CONTRATTO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_ANAGRAFICA IN " _
                        & "(select id from SISCOM_MI.anagrafica where (cognome='" & UCase(par.PulisciStrSql(CognomeRichiedente)) _
                        & "' OR RAGIONE_SOCIALE='" & UCase(par.PulisciStrSql(CognomeRichiedente)) & "') " _
                        & "AND NOME='" & UCase(par.PulisciStrSql(NomeRichiedente)) & "'))"

                        myReader5 = par.cmd.ExecuteReader()
                        If myReader5.HasRows = False Then
                            lblDettagli.Text = "Non risulta in alcun rapporto in complessi diversi dal selezionato"
                        Else
                            lblDettagli.Text = "Presente nel/i rapporti in complessi diversi dal selezionato:<br/>"
                        End If
                        Do While myReader5.Read
                            lblDettagli.Text = lblDettagli.Text & "Cod. " & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader5("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & myReader5("COD_CONTRATTO") & "</a><br/>"
                        Loop
                        myReader5.Close()
                    End If

                Case "E"
                    Dim edificio As Boolean
                    par.cmd.CommandText = "SELECT DISTINCT RAPPORTI_UTENZA.COD_CONTRATTO,rapporti_utenza.id " _
                    & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI " _
                    & "WHERE UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO AND " _
                    & "EDIFICI.ID=" & identificativo.Value & " AND RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_ANAGRAFICA IN " _
                    & "(select id from SISCOM_MI.anagrafica where (cognome='" & UCase(par.PulisciStrSql(CognomeRichiedente)) _
                    & "' OR RAGIONE_SOCIALE='" & UCase(par.PulisciStrSql(CognomeRichiedente)) & "') AND NOME='" & UCase(par.PulisciStrSql(NomeRichiedente)) & "'))"

                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.HasRows = False Then
                        lblDettagli.Text = "Non risulta in alcun rapporto nell'Edificio selezionato."
                    Else
                        lblDettagli.Text = "Presente nel/i rapporti dell'Edificio selezionato:<br/>"
                        edificio = True
                    End If
                    Do While myReader5.Read
                        lblDettagli.Text = lblDettagli.Text & "Cod. " & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader5("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & myReader5("COD_CONTRATTO") & "</a><br/>"
                    Loop
                    myReader5.Close()


                    If edificio = False Then
                        par.cmd.CommandText = "SELECT DISTINCT RAPPORTI_UTENZA.COD_CONTRATTO,rapporti_utenza.id " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI " _
                        & "WHERE UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO AND " _
                        & "RAPPORTI_UTENZA.ID IN " _
                        & "(SELECT ID_CONTRATTO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_ANAGRAFICA IN " _
                        & "(select id from SISCOM_MI.anagrafica where (cognome='" & UCase(par.PulisciStrSql(CognomeRichiedente)) _
                        & "' OR RAGIONE_SOCIALE='" & UCase(par.PulisciStrSql(CognomeRichiedente)) & "') " _
                        & "AND NOME='" & UCase(par.PulisciStrSql(NomeRichiedente)) & "'))"

                        myReader5 = par.cmd.ExecuteReader()
                        If myReader5.HasRows = False Then
                            lblDettagli.Text = "Non risulta in alcun rapporto in Edifici diversi dal selezionato"
                        Else
                            lblDettagli.Text = "Presente nel/i rapporti in Edifici diversi dal selezionato:<br/>"
                        End If
                        Do While myReader5.Read
                            lblDettagli.Text = lblDettagli.Text & "Cod. " & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader5("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & myReader5("COD_CONTRATTO") & "</a><br/>"
                        Loop
                        myReader5.Close()
                    End If

                Case "U"
                    Dim unita As Boolean
                    par.cmd.CommandText = "SELECT DISTINCT RAPPORTI_UTENZA.COD_CONTRATTO,rapporti_utenza.id " _
                    & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE " _
                    & "WHERE UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND " _
                    & "unita_CONTRATTUALE.ID_UNITA=" & identificativo.Value & " AND RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_ANAGRAFICA IN " _
                    & "(select id from SISCOM_MI.anagrafica where (cognome='" & UCase(par.PulisciStrSql(CognomeRichiedente)) _
                    & "' OR RAGIONE_SOCIALE='" & UCase(par.PulisciStrSql(CognomeRichiedente)) & "') AND NOME='" & UCase(par.PulisciStrSql(NomeRichiedente)) & "'))"

                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.HasRows = False Then
                        lblDettagli.Text = "Non risulta in alcun rapporto nell'Unità selezionata."
                    Else
                        lblDettagli.Text = "Presente nel/i rapporti dell'Unità selezionata:<br/>"
                        unita = True
                    End If
                    Do While myReader5.Read
                        lblDettagli.Text = lblDettagli.Text & "Cod. " & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader5("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & myReader5("COD_CONTRATTO") & "</a><br/>"
                    Loop
                    myReader5.Close()


                    If unita = False Then
                        par.cmd.CommandText = "SELECT DISTINCT RAPPORTI_UTENZA.COD_CONTRATTO,rapporti_utenza.id " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE " _
                        & "WHERE UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND " _
                        & "RAPPORTI_UTENZA.ID IN " _
                        & "(SELECT ID_CONTRATTO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_ANAGRAFICA IN " _
                        & "(select id from SISCOM_MI.anagrafica where (cognome='" & UCase(par.PulisciStrSql(CognomeRichiedente)) _
                        & "' OR RAGIONE_SOCIALE='" & UCase(par.PulisciStrSql(CognomeRichiedente)) & "') " _
                        & "AND NOME='" & UCase(par.PulisciStrSql(NomeRichiedente)) & "'))"

                        myReader5 = par.cmd.ExecuteReader()
                        If myReader5.HasRows = False Then
                            lblDettagli.Text = "Non risulta in alcun rapporto in Unità diverse dalla selezionata"
                        Else
                            lblDettagli.Text = "Presente nel/i rapporti in Unità diverse dalla selezionata:<br/>"
                        End If
                        Do While myReader5.Read
                            lblDettagli.Text = lblDettagli.Text & "Cod. " & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader5("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & myReader5("COD_CONTRATTO") & "</a><br/>"
                        Loop
                        myReader5.Close()
                    End If

            End Select

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblDettagli.Text = ex.Message
        End Try
    End Function
End Class
