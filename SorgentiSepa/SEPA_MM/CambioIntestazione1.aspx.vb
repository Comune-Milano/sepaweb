
Partial Class CambioIntestazione1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                Indice = Request.QueryString("ID")
                Label8.Text = Request.QueryString("PG")
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT comp_nucleo.*,domande_bando.id_stato,domande_bando.id_dichiarazione FROM comp_nucleo,domande_bando where  domande_bando.id=" & Indice & " and comp_nucleo.id_dichiarazione=domande_bando.id_dichiarazione and comp_nucleo.progr=domande_bando.progr_componente"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Label1.Text = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " - " & par.IfNull(myReader("cod_fiscale"), "")
                    sStato = par.IfNull(myReader("id_stato"), "")
                    IndiceDichiarazione = par.IfNull(myReader("id_dichiarazione"), -1)
                    IndiceComponente = par.IfNull(myReader("id"), -1)
                End If
                myReader.Close()


                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,domande_bando where domande_bando.id=" & Indice & " and comp_nucleo.id_dichiarazione=domande_bando.id_dichiarazione and comp_nucleo.progr<>domande_bando.progr_componente order by comp_nucleo.progr asc"
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    If par.RicavaEta(myReader("DATA_NASCITA")) >= 18 Then
                        cmbNucleo.Items.Add(New ListItem(par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""), myReader("id")))
                    End If
                End While

                If cmbNucleo.Items.Count = 0 Then
                    btnSalva.Visible = False
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()




            Catch ex As Exception
                par.OracleConn.Close()
                Response.Write("<br>")
                Response.Write("<br>")
                Response.Write("<br>")
                Response.Write("<br>")
                Response.Write("<br>")
                Response.Write("<br>")
                Response.Write("<br>")
                Response.Write("<br><br><br><br><br>")
                Response.Write(ex.Message)
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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            Dim i As Integer



            If cmbNucleo.Items.Count = 0 Then
                Label4.Visible = True
                Exit Sub
            End If
            Label4.Visible = False
            par.OracleConn.Open()
            par.SettaCommand(par)
            If cmbElimina.SelectedItem.Text = "NO" Then
                par.cmd.CommandText = "DELETE FROM COMP_NUCLEO WHERE ID=" & IndiceComponente
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update COMP_NUCLEO set progr=0 WHERE ID=" & cmbNucleo.SelectedItem.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update domande_bando set progr_componente=0,note='Precedente intestatario:" & par.PulisciStrSql(Label1.Text) & "' WHERE ID=" & Indice
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select * from comp_nucleo where id_dichiarazione=" & IndiceDichiarazione & " and id<>" & cmbNucleo.SelectedItem.Value

                i = 1
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    par.cmd.CommandText = "update COMP_NUCLEO set progr=" & i & " WHERE ID=" & myReader("id")
                    par.cmd.ExecuteNonQuery()
                    i = i + 1
                End While

            Else
                par.cmd.CommandText = "update COMP_NUCLEO set progr=0 WHERE ID=" & cmbNucleo.SelectedItem.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update domande_bando set progr_componente=0,note='Precedente intestatario:" & par.PulisciStrSql(Label1.Text) & "' WHERE ID=" & Indice
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select * from comp_nucleo where id_dichiarazione=" & IndiceDichiarazione & " and id<>" & cmbNucleo.SelectedItem.Value

                i = 1
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    par.cmd.CommandText = "update COMP_NUCLEO set progr=" & i & " WHERE ID=" & myReader("id")
                    par.cmd.ExecuteNonQuery()
                    i = i + 1
                End While


            End If
            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                        & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                  & "','F165','','I')"

            par.cmd.ExecuteNonQuery()
            Image2.Visible = True
            Label6.Visible = True
            btnSalva.Visible = False
            Label1.Visible = False
            Label2.Visible = False
            Label3.Visible = False
            Label5.Visible = False
            cmbNucleo.Visible = False
            cmbElimina.Visible = False
            Label9.Visible = True

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write("<br>")
            Response.Write("<br>")
            Response.Write("<br>")
            Response.Write("<br>")
            Response.Write("<br>")
            Response.Write("<br>")
            Response.Write("<br>")
            Response.Write("<br><br><br><br><br>")
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
