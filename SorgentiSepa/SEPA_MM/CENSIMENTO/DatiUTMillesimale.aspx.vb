
Partial Class CENSIMENTO_DatiUTMillesimale
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                vId = Request.QueryString("ID")
                Passato = Request.QueryString("Pas")
                vIdUtenza = Request.QueryString("IDUTENZA")

              
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                If vId <> 0 Then
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    Select Case Passato
                        Case "CO"


                            par.cmd.CommandText = " SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO  from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_COMPLESSO = " & vId
                            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                            myReader1 = par.cmd.ExecuteReader()
                            cmbTabMillesimale.Items.Add(New ListItem(" ", -1))
                            While myReader1.Read
                                cmbTabMillesimale.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIO"), " "), par.IfNull(myReader1("ID"), -1)))
                            End While
                            myReader1.Close()


                        Case "ED"
                            par.cmd.CommandText = " SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO  from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO = " & vId
                            myReader1 = par.cmd.ExecuteReader()
                            cmbTabMillesimale.Items.Add(New ListItem(" ", -1))
                            While myReader1.Read
                                cmbTabMillesimale.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIO"), " "), par.IfNull(myReader1("ID"), -1)))
                            End While
                            myReader1.Close()
                    End Select
                    par.cmd.CommandText = "Select * from SISCOM_MI.TIPOLOGIA_COSTO"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbTipolCatasto.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbTipolCatasto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
                    End While
                    myReader1.Close()

                End If

                'par.OracleConn.Close()

            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message

            End Try
        End If
    End Sub
    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property
    Private Property vIdUtenza() As Long
        Get
            If Not (ViewState("par_vIdUtenza") Is Nothing) Then
                Return CLng(ViewState("par_vIdUtenza"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdUtenza") = value
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

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Me.salva()
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub
    Private Sub salva()
        If par.IfEmpty(Me.txtPercRipart.Text, "Null") <> "Null" AndAlso Me.cmbTabMillesimale.SelectedValue <> "-1" AndAlso Me.cmbTipolCatasto.SelectedValue <> "-1" Then
            Try
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                'par.OracleConn.Open()
                'par.SettaCommand(par)


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                par.cmd.CommandText = "SELECT COD_TIPOLOGIA_COSTO FROM SISCOM_MI.UTENZE_TABELLE_MILLESIMALI WHERE ID_TABELLA_MILLESIMALE =" & cmbTabMillesimale.SelectedValue & " AND COD_TIPOLOGIA_COSTO = '" & Me.cmbTipolCatasto.SelectedValue & "'"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Response.Write("<SCRIPT>alert('Il costo è già esistente!');</SCRIPT>")
                    Exit Sub
                Else
                    myReader1.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.UTENZE_TABELLE_MILLESIMALI (id_utenza, id_tabella_millesimale, cod_tipologia_costo, perc_ripartizione_costi) values (" & vIdUtenza & ", " & cmbTabMillesimale.SelectedValue.ToString & ", '" & Me.cmbTipolCatasto.SelectedValue.ToString & "', " & par.VirgoleInPunti(Me.txtPercRipart.Text) & ")"
                    par.cmd.ExecuteNonQuery()
                    'par.myTrans.Commit()
                End If
                'par.OracleConn.Close()
                'par.myTrans.Commit()

            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            End Try
        Else
            Response.Write("<SCRIPT>alert('Scegliere una tabella millesimale!');</SCRIPT>")

        End If
    End Sub

    Protected Sub ImButEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImButEsci.Click
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub
End Class
