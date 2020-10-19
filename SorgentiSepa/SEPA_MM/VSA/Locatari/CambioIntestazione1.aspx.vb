
Partial Class CambioIntestazione1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                Indice = Request.QueryString("ID")
                Label8.Text = Request.QueryString("PG")

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT comp_nucleo_vsa.*,domande_bando_vsa.id_stato,domande_bando_vsa.id_dichiarazione,t_motivo_domanda_vsa.descrizione as motivo FROM comp_nucleo_vsa,domande_bando_vsa,t_motivo_domanda_vsa where t_motivo_domanda_vsa.id= domande_bando_vsa.id_motivo_domanda and domande_bando_vsa.id=" & Indice & " and comp_nucleo_vsa.id_dichiarazione=domande_bando_vsa.id_dichiarazione and comp_nucleo_vsa.progr=domande_bando_vsa.progr_componente"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Label1.Text = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " - " & par.IfNull(myReader("cod_fiscale"), "")
                    sStato = par.IfNull(myReader("id_stato"), "")
                    IndiceDichiarazione = par.IfNull(myReader("id_dichiarazione"), -1)
                    IndiceComponente = par.IfNull(myReader("id"), -1)

                    'lblTipo.Text = par.IfNull(myReader("MOTIVO"), "")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT comp_nucleo_vsa.* FROM comp_nucleo_vsa,domande_bando_vsa where domande_bando_vsa.id=" & Indice & " and comp_nucleo_vsa.id_dichiarazione=domande_bando_vsa.id_dichiarazione and comp_nucleo_vsa.progr<>0 order by comp_nucleo_vsa.cognome asc"
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


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If conferma.Value = 1 Then
            Try
                Dim i As Integer

                If cmbNucleo.Items.Count = 0 Then
                    Label4.Visible = True
                    Exit Sub
                End If
                Label4.Visible = False

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "update comp_nucleo_vsa set progr=0 WHERE ID=" & cmbNucleo.SelectedItem.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update domande_bando_vsa set progr_componente=0,note='Precedente intestatario:" & par.PulisciStrSql(Label1.Text) & "' WHERE ID=" & Indice
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select * from comp_nucleo_vsa where id_dichiarazione=" & IndiceDichiarazione & " and id<>" & cmbNucleo.SelectedItem.Value

                i = 1
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    par.cmd.CommandText = "update COMP_NUCLEO_vsa set progr=" & i & " WHERE ID=" & myReader("id")
                    par.cmd.ExecuteNonQuery()
                    i = i + 1
                End While
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                      & "','F205','','')"
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
