
Partial Class Contratti_CONTRATTI_LIGHT_VisualizzazioneCompNucleo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE_AU_LIGHT") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CercaComponenti()
        End If
    End Sub

    Private Sub CercaComponenti()
        Try
            Dim dt0 As New Data.DataTable
            Dim dt2 As New Data.DataTable
            Dim compCancell As Boolean = False

            dt0.Columns.Add("COGNOME")
            dt0.Columns.Add("NOME")
            dt0.Columns.Add("COD_FISCALE")
            dt0.Columns.Add("PARENTELA")
            dt0.Columns.Add("DATA_INIZIO")
            dt0.Columns.Add("DATA_FINE")

            Dim StrQuery As String = ""
            Dim StrQuery2 As String = ""
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Select Case Request.QueryString("T")
                Case 1 'VSA
                    StrQuery2 = "SELECT comp_nucleo_cancell.* FROM comp_nucleo_cancell, dichiarazioni_vsa WHERE comp_nucleo_cancell.id_dichiarazione = dichiarazioni_vsa.ID AND dichiarazioni_vsa.ID =" & Request.QueryString("ID")
                    par.cmd.CommandText = StrQuery2
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If lettore.Read Then
                        compCancell = True
                    End If
                    lettore.Read()
                    StrQuery = "SELECT COMP_NUCLEO_VSA.*,T_TIPO_PARENTELA.DESCRIZIONE AS PARENTELA FROM COMP_NUCLEO_VSA,DICHIARAZIONI_VSA,T_TIPO_PARENTELA WHERE T_TIPO_PARENTELA.COD = COMP_NUCLEO_VSA.GRADO_PARENTELA AND DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID=" & Request.QueryString("ID") & " ORDER BY PROGR ASC"
                Case 2 'ANAGRAFE UTENZA
                    StrQuery = "SELECT UTENZA_COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE AS PARENTELA,UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL AS DATA_INIZIO,UTENZA_DICHIARAZIONI.DATA_FINE_VAL AS DATA_FINE FROM UTENZA_COMP_NUCLEO,UTENZA_DICHIARAZIONI,T_TIPO_PARENTELA WHERE T_TIPO_PARENTELA.COD = UTENZA_COMP_NUCLEO.GRADO_PARENTELA AND UTENZA_DICHIARAZIONI.ID = UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE AND UTENZA_DICHIARAZIONI.ID=" & Request.QueryString("ID") & " ORDER BY PROGR ASC"
                Case 3 'BANDO ERP
                    StrQuery = "SELECT COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE AS PARENTELA FROM COMP_NUCLEO,DICHIARAZIONI,T_TIPO_PARENTELA WHERE T_TIPO_PARENTELA.COD = COMP_NUCLEO.GRADO_PARENTELA AND DICHIARAZIONI.ID = COMP_NUCLEO.ID_DICHIARAZIONE AND DICHIARAZIONI.ID=" & Request.QueryString("ID") & " ORDER BY PROGR ASC"
                Case 4
                    StrQuery = "SELECT ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTELA,SOGGETTI_CONTRATTUALI_INIZIO.DATA_INIZIO,SOGGETTI_CONTRATTUALI_INIZIO.DATA_FINE FROM SISCOM_MI.SOGGETTI_CONTRATTUALI_INIZIO,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_PARENTELA WHERE TIPOLOGIA_PARENTELA.COD = SOGGETTI_CONTRATTUALI_INIZIO.COD_TIPOLOGIA_PARENTELA AND SOGGETTI_CONTRATTUALI_INIZIO.ID_ANAGRAFICA = ANAGRAFICA.ID AND ID_CONTRATTO = " & Request.QueryString("ID")

            End Select

            Dim RIGA As System.Data.DataRow
            Dim RIGA2 As System.Data.DataRow
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StrQuery, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If compCancell = True Then
                Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(StrQuery2, par.OracleConn)
                da2.Fill(dt2)
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                RIGA = dt0.NewRow()
                RIGA.Item("COGNOME") = par.IfNull(dt.Rows(i).Item("COGNOME"), "")
                RIGA.Item("NOME") = par.IfNull(dt.Rows(i).Item("NOME"), "")
                RIGA.Item("COD_FISCALE") = par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "")
                RIGA.Item("PARENTELA") = par.IfNull(dt.Rows(i).Item("PARENTELA"), "").ToString.ToUpper

                If Request.QueryString("T") = 1 Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND COD_FISCALE ='" & dt.Rows(i).Item("COD_FISCALE") & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        RIGA.Item("DATA_INIZIO") = par.FormattaData(par.IfNull(myReader2("DATA_INIZIO"), ""))
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND COD_FISCALE ='" & dt.Rows(i).Item("COD_FISCALE") & "'"
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader4.Read Then
                        RIGA.Item("DATA_FINE") = par.FormattaData(par.IfNull(myReader4("DATA_FINE"), ""))
                    End If
                    myReader4.Close()
                ElseIf Request.QueryString("T") = "2" Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND COD_FISCALE ='" & dt.Rows(i).Item("COD_FISCALE") & "'"
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader4.Read Then
                        If par.IfNull(myReader4("DATA_FINE"), "") < par.IfNull(dt.Rows(i).Item("DATA_FINE"), "") Then
                            RIGA.Item("DATA_FINE") = par.FormattaData(par.IfNull(myReader4("DATA_FINE"), ""))
                        Else
                            RIGA.Item("DATA_FINE") = par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_FINE"), ""))
                        End If
                    End If
                    myReader4.Close()

                    RIGA.Item("DATA_INIZIO") = par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_INIZIO"), ""))

                ElseIf Request.QueryString("T") = "3" Then
                    RIGA.Item("DATA_INIZIO") = ""
                    RIGA.Item("DATA_FINE") = ""

                ElseIf Request.QueryString("T") = "4" Then
                    RIGA.Item("DATA_INIZIO") = par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_INIZIO"), ""))
                    RIGA.Item("DATA_FINE") = par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_FINE"), ""))

                End If

                dt0.Rows.Add(RIGA)
            Next


            If compCancell = True Then
                For j As Integer = 0 To dt2.Rows.Count - 1
                    RIGA2 = dt0.NewRow()
                    RIGA2.Item("COGNOME") = par.IfNull(dt2.Rows(j).Item("COGNOME"), "")
                    RIGA2.Item("NOME") = par.IfNull(dt2.Rows(j).Item("NOME"), "")
                    RIGA2.Item("COD_FISCALE") = par.IfNull(dt2.Rows(j).Item("COD_FISCALE"), "")
                    RIGA2.Item("DATA_FINE") = par.FormattaData(par.IfNull(dt2.Rows(j).Item("DATA_USCITA"), ""))
                    par.cmd.CommandText = "SELECT TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTE,ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.ANAGRAFICA WHERE TIPOLOGIA_PARENTELA.COD = SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND COD_FISCALE ='" & dt2.Rows(j).Item("COD_FISCALE") & "'"
                    Dim lettParente As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If lettParente.Read Then
                        RIGA2.Item("PARENTELA") = par.IfNull(lettParente("PARENTE"), "").ToString.ToUpper
                    End If
                    lettParente.Close()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND COD_FISCALE ='" & dt2.Rows(j).Item("COD_FISCALE") & "'"
                    Dim lettDataInizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If lettDataInizio.Read Then
                        RIGA2.Item("DATA_INIZIO") = par.FormattaData(par.IfNull(lettDataInizio("DATA_INIZIO"), ""))
                    End If
                    lettDataInizio.Close()
                    dt0.Rows.Add(RIGA2)
                Next
            End If

            DataGrid1.DataSource = dt0
            DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

End Class
