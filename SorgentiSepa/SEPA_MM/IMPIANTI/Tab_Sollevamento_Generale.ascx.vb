
Partial Class IMPIANTI_Tab_Sollevamento_Generale
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then


            ' CONNESSIONE DB
            IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text


            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text


            If PAR.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If
            ''''''''''''''''''''''''''

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If

    End Sub

    Private Property vIdImpianto() As Long
        Get
            If Not (ViewState("par_idImpianto") Is Nothing) Then
                Return CLng(ViewState("par_idImpianto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idImpianto") = value
        End Set

    End Property

    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmbTeleallarme_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTeleallarme.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Protected Sub cmbTeleallarme_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTeleallarme.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Protected Sub txtNumFermate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumFermate.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        CalcolaCanone()

    End Sub

    Public Sub CalcolaCanone()
        Dim FlagConnessione As Boolean
        Dim TipoSollevamento As String
        Dim NumFermate As Integer

        Try
            FlagConnessione = False

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            txtNumero.Text = ""

            If PAR.IfEmpty(txtNumFermate.Text, "Null") = "Null" Then
                txtNumFermate.Text = ""
                Exit Sub
            End If


            TipoSollevamento = CType(Me.Page.FindControl("cmbTipoUso"), DropDownList).SelectedItem.ToString
            NumFermate = txtNumFermate.Text
            If NumFermate = 1 Then NumFermate = 2
            If NumFermate > 20 Then NumFermate = 20


            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdImpianto > 0 Then
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If

            par.cmd.CommandText = "  select SISCOM_MI.TAB_CANONE_SOLLEVAMENTO.NUMERO " _
                                & " from  SISCOM_MI.TAB_CANONE_SOLLEVAMENTO " _
                                & " where SISCOM_MI.TAB_CANONE_SOLLEVAMENTO.FERMATE = " & NumFermate _
                                  & " and SISCOM_MI.TAB_CANONE_SOLLEVAMENTO.TIPO='" & TipoSollevamento & "'"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                txtNumero.Text = PAR.IfNull(myReader1("NUMERO"), "")
            End If
            myReader1.Close()

            If FlagConnessione = True Then
                par.OracleConn.Close()
            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub



End Class