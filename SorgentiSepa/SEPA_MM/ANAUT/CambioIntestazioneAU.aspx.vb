
Partial Class ANAUT_CambioIntestazioneAU
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                Indice = Request.QueryString("ID")
                Label8.Text = Request.QueryString("PG")

                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()

                par.cmd.CommandText = "SELECT utenza_comp_nucleo.*,utenza_dichiarazioni.id_stato,utenza_dichiarazioni.id FROM utenza_dichiarazioni,utenza_comp_nucleo where utenza_dichiarazioni.id=" & Indice & " and utenza_comp_nucleo.id_dichiarazione=utenza_dichiarazioni.id and utenza_comp_nucleo.progr=0"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Label1.Text = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " - " & par.IfNull(myReader("cod_fiscale"), "")
                    sStato = par.IfNull(myReader("id_stato"), "")
                    IndiceDichiarazione = par.IfNull(myReader("id_dichiarazione"), -1)
                    IndiceComponente = par.IfNull(myReader("id"), -1)

                    'lblTipo.Text = par.IfNull(myReader("MOTIVO"), "")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT utenza_comp_nucleo.* FROM utenza_comp_nucleo,utenza_dichiarazioni where utenza_dichiarazioni.id=" & Indice & " and utenza_comp_nucleo.id_dichiarazione=utenza_dichiarazioni.id and utenza_comp_nucleo.progr<>0 order by utenza_comp_nucleo.cognome asc"
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    If par.RicavaEta(myReader("DATA_NASCITA")) >= 18 Then
                        cmbNucleo.Items.Add(New ListItem(par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""), myReader("id")))
                    End If
                End While
                myReader.Close()

                If cmbNucleo.Items.Count = 0 Then
                    cmbNucleo.Visible = False
                    Label2.Visible = False
                    Label4.Visible = True
                    Label4.Text = "Impossibile procedere...Non sono presenti altri componenti oltre l'attuale intestatario della domanda!"
                    btnSalva.Enabled = False
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Public Property IndiceComponente() As Long
        Get
            If Not (ViewState("par_IndiceComponente") Is Nothing) Then
                Return CLng(ViewState("par_IndiceComponente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IndiceComponente") = value
        End Set

    End Property

    Public Property IndiceDichiarazione() As Long
        Get
            If Not (ViewState("par_IndiceDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_IndiceDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IndiceDichiarazione") = value
        End Set

    End Property

    Public Property Indice() As Long
        Get
            If Not (ViewState("par_indice") Is Nothing) Then
                Return CLng(ViewState("par_indice"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_indice") = value
        End Set

    End Property

    Public Property sStato() As String
        Get
            If Not (ViewState("par_sStato") Is Nothing) Then
                Return CStr(ViewState("par_sStato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStato") = value
        End Set

    End Property

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If conferma.Value = 1 Then
            Try
                Dim i As Integer

                If cmbNucleo.Items.Count = 0 Then
                    Label4.Visible = True
                    Exit Sub
                End If
                Label4.Visible = False

                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()

                par.cmd.CommandText = "update utenza_comp_nucleo set progr=0 WHERE ID=" & cmbNucleo.SelectedItem.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update utenza_dichiarazioni set progr_dnte=0,note='Precedente intestatario:" & par.PulisciStrSql(Label1.Text) & "' WHERE ID=" & Indice
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select * from utenza_comp_nucleo where id_dichiarazione=" & IndiceDichiarazione & " and id<>" & cmbNucleo.SelectedItem.Value

                i = 1
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    par.cmd.CommandText = "update utenza_comp_nucleo set progr=" & i & " WHERE ID=" & myReader("id")
                    par.cmd.ExecuteNonQuery()
                    i = i + 1
                End While
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                      & "'F205','','')"
                par.cmd.ExecuteNonQuery()

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                btnSalva.Enabled = False

                Response.Write("<script>alert('Operazione effettuata con successo!')</script>")

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End Try
        End If
    End Sub
End Class
