
Partial Class CENSIMENTO_NumMillesimi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vIdEdif = Request.QueryString("IDED")
            Passato = Request.QueryString("Pas")
            vId = Request.QueryString("IDUNI")

            riempicombo()
        End If

    End Sub
    Public Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property

    Public Property vIdEdif() As Long
        Get
            If Not (ViewState("par_lIdEdificio") Is Nothing) Then
                Return CLng(ViewState("par_lIdEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdEdificio") = value
        End Set

    End Property

    Public Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property
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
    Private Sub salva()
        Try


            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Select Case Passato
                Case "UNICOM"

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.VALORI_MILLESIMALI (ID_TABELLA, VALORE_MILLESIMO, ID_UNITA_COMUNE) VALUES (" & Me.DrlTabMillesim.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.TxtValore.Text), "null") & ", " & vId & ")"
                    par.cmd.ExecuteNonQuery()
                Case "UNCOMED"
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.VALORI_MILLESIMALI (ID_TABELLA, VALORE_MILLESIMO, ID_UNITA_COMUNE) VALUES (" & Me.DrlTabMillesim.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.TxtValore.Text), "null") & ", " & vId & ")"
                    par.cmd.ExecuteNonQuery()

                Case "UI"
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.VALORI_MILLESIMALI (ID_TABELLA, VALORE_MILLESIMO, ID_UNITA_IMMOBILIARE) VALUES (" & Me.DrlTabMillesim.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.TxtValore.Text), "Null") & ", " & vId & ")"
                    par.cmd.ExecuteNonQuery()
            End Select
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()

        End Try
    End Sub

    Private Sub riempicombo()
        Dim ds As New Data.DataSet
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter

        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Try
            Select Case Passato
                Case "UNICOM"
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(" SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO  from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_COMPLESSO = " & vIdEdif, par.OracleConn)
                    da.Fill(ds)
                    'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    DrlTabMillesim.DataSource = ds
                    DrlTabMillesim.DataTextField = "DESCRIZIO"
                    DrlTabMillesim.DataValueField = "ID"
                    DrlTabMillesim.DataBind()
                Case "UNCOMED"
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(" SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO  from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO = " & vIdEdif, par.OracleConn)
                    da.Fill(ds)
                    'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    DrlTabMillesim.DataSource = ds
                    DrlTabMillesim.DataTextField = "DESCRIZIO"
                    DrlTabMillesim.DataValueField = "ID"
                    DrlTabMillesim.DataBind()

                Case "UI"
                    da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO = " & vIdEdif, par.OracleConn)
                    da.Fill(ds)
                    'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                    DrlTabMillesim.DataSource = ds
                    DrlTabMillesim.DataTextField = "DESCRIZIO"
                    DrlTabMillesim.DataValueField = "ID"
                    DrlTabMillesim.DataBind()
            End Select
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
        par.OracleConn.Close()
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script language='javascript'> { self.close() }</script>")
    End Sub

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        If par.IfEmpty(Me.TxtValore.Text, "null") <> "null" AndAlso Me.DrlTabMillesim.SelectedValue <> "-1" AndAlso Me.DrlTabMillesim.SelectedValue <> "" Then
            Me.salva()
        Else
            Response.Write("<SCRIPT>alert('Inserire tutti i dati per procedere con il salvataggio!');</SCRIPT>")

        End If

        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub
End Class
