' TAB RIEPILOGO GENERALE DELLA SCHEDA SAL

Partial Class Tab_SAL_Riepilogo
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then


            ' CONNESSIONE DB
            'IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text
            'Me.txtIdConnessione.Text = IdConnessione

            'If PAR.OracleConn.State = Data.ConnectionState.Open Then
            '    Response.Write("IMPOSSIBILE VISUALIZZARE")
            '    Exit Sub
            'Else
            '    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '    par.SettaCommand(par)
            'End If
            ''''''''''''''''''''''''''


            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If
    End Sub


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
                    DirectCast(CTRL, TextBox).ReadOnly = True
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




    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function



End Class
