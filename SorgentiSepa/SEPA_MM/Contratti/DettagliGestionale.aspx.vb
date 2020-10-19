
Partial Class Contratti_DettagliGestionale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            idBoll = Request.QueryString("IDBOLL")
            CaricaDettagli()
        End If
    End Sub

    Private Sub CaricaDettagli()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim dtPerDatagr As New Data.DataTable

            Dim RIGA As System.Data.DataRow

            dtPerDatagr.Columns.Add("ID")
            dtPerDatagr.Columns.Add("TIPO_DOC")
            dtPerDatagr.Columns.Add("IMPORTO")
            dtPerDatagr.Columns.Add("TIPO_APPL")
            dtPerDatagr.Columns.Add("N_RATE")
            dtPerDatagr.Columns.Add("DA_RATA")
            dtPerDatagr.Columns.Add("IMP_RATA")
            dtPerDatagr.Columns.Add("DATA_APPL")
            dtPerDatagr.Columns.Add("OPERATORE")

            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.id as ID_VOCE_GEST,BOL_BOLLETTE_GEST.*,TIPO_BOLLETTE_GEST.descrizione FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST" _
                & " WHERE BOL_BOLLETTE_GEST.ID=BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND FL_VISUALIZZABILE=1 AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND BOL_BOLLETTE_GEST.ID=" & idBoll
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtDocGest As New Data.DataTable
            da.Fill(dtDocGest)
            da.Dispose()

            For i As Integer = 0 To dtDocGest.Rows.Count - 1
                RIGA = dtPerDatagr.NewRow()
                RIGA.Item("ID") = idBoll
                RIGA.Item("TIPO_DOC") = par.IfNull(dtDocGest.Rows(i).Item("DESCRIZIONE"), "")
                RIGA.Item("IMPORTO") = Format(CDec(par.IfNull(dtDocGest.Rows(i).Item("IMPORTO_TOTALE"), 0)), "##,##0.00")
                RIGA.Item("DATA_APPL") = par.FormattaData(par.IfNull(dtDocGest.Rows(i).Item("DATA_APPLICAZIONE"), ""))

                par.cmd.CommandText = "SELECT * FROM OPERATORI WHERE ID=" & par.IfNull(dtDocGest.Rows(i).Item("ID_OPERATORE_APPLICAZIONE"), 0)
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("OPERATORE") = par.IfNull(myReader2("OPERATORE"), "")
                End If
                myReader2.Close()

                Select Case par.IfNull(dtDocGest.Rows(i).Item("TIPO_APPLICAZIONE"), "")
                    Case "N"
                        RIGA.Item("TIPO_APPL") = "Nessuna"
                        RIGA.Item("N_RATE") = ""
                        RIGA.Item("IMP_RATA") = ""
                        RIGA.Item("DA_RATA") = ""
                    Case "P"
                        RIGA.Item("TIPO_APPL") = "Parziale - Scrittura Voci Schema Bollette"
                    Case "T"
                        RIGA.Item("TIPO_APPL") = "Totale - Nuova Emissione"
                End Select

                If par.IfNull(dtDocGest.Rows(i).Item("TIPO_APPLICAZIONE"), "") = "P" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_SCHEMA WHERE ID_VOCE_BOLLETTA_GEST=" & par.IfNull(dtDocGest.Rows(i).Item("ID_VOCE_GEST"), 0)
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        RIGA.Item("N_RATE") = par.IfNull(myReader1("PER_RATE"), "")
                        RIGA.Item("IMP_RATA") = Format(CDec(par.IfNull(myReader1("IMPORTO_SINGOLA_RATA"), "")), "##,##0.00")
                        RIGA.Item("DA_RATA") = MesiProssimaBollett(par.IfNull(myReader1("DA_RATA"), 1))
                    End If
                    myReader1.Close()
                End If
                dtPerDatagr.Rows.Add(RIGA)
            Next

            DataGrDettaglio.DataSource = dtPerDatagr
            DataGrDettaglio.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function MesiProssimaBollett(ByVal NumRata As Integer) As String
        Dim Mese As String = ""

        Select Case NumRata
            Case "1"
                Mese = "Gennaio"
            Case "2"
                Mese = "Febbraio"
            Case "3"
                Mese = "Marzo"
            Case "4"
                Mese = "Aprile"
            Case "5"
                Mese = "Maggio"
            Case "6"
                Mese = "Giugno"
            Case "7"
                Mese = "Luglio"
            Case "8"
                Mese = "Agosto"
            Case "9"
                Mese = "Settembre"
            Case "10"
                Mese = "Ottobre"
            Case "11"
                Mese = "Novembre"
            Case "12"
                Mese = "Dicembre"
        End Select

        Return Mese

    End Function

    Public Property idBoll() As Long
        Get
            If Not (ViewState("par_idBoll") Is Nothing) Then
                Return CLng(ViewState("par_idBoll"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idBoll") = value
        End Set
    End Property

End Class
