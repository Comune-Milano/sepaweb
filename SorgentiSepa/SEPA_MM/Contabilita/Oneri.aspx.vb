
Partial Class Contratti_Report_Oneri
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim UltimoPagam As String = 0
            Dim INIZIO As String = ""
            Dim FINE As String = ""

            If par.AggiustaData(Me.txtDataDal.Text) < Format(Date.Now, "yyyyMMdd") Then

                INIZIO = Me.txtDataDal.Text
                FINE = Me.txtDataAl.Text
                'If par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") = "Null" Or par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") = "Null" Then
                '    Response.Write("<script>alert('E\' necessario specificare entrabe le date!')</script>")
                '    Exit Sub
                'End If
                '******APERTURA CONNESSIONE*****
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT to_char(to_date(MAX(DATA_PAGAMENTO),'yyyymmdd'),'dd/mm/yyyy') AS ULTIMA_BOLL FROM SISCOM_MI.BOL_BOLLETTE "
                Dim myReaderTEMP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReaderTEMP.Read Then
                    UltimoPagam = par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
                End If
                myReaderTEMP.Close()

                If Me.txtDataDal.Text > UltimoPagam Then

                    Me.txtDataDal.Text = UltimoPagam
                End If
                If Me.txtDataAl.Text > UltimoPagam Then
                    Me.txtDataAl.Text = UltimoPagam
                End If
                'If par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") <> "Null" AndAlso par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") <> "Null" Then
                If Not String.IsNullOrEmpty(Me.txtDataAl.Text) Then
                    If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
                        Response.Write("<script>alert('Intervallo non valido!')</script>")
                        Me.txtDataAl.Text = ""
                        Me.txtDataDal.Text = ""
                        Exit Sub

                    End If
                Else : Me.txtDataAl.Text = UltimoPagam
                End If



                'Else
                '    Response.Write("<script>alert('Definire l\'intervallo di tempo!')</script>")
                '    Exit Sub
                'End If
                If Request.QueryString("CHIAMA") = "O" Then
                    Response.Write("<script>window.open('StampaOneri.aspx?DAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataDal.Text)) & "&AL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataAl.Text)) & "&INIZIO=" & par.VaroleDaPassare(INIZIO) & "&FINE=" & par.VaroleDaPassare(FINE) & "');</script>")
                ElseIf Request.QueryString("CHIAMA") = "M" Then
                    Response.Write("<script>window.open('ValoreMedio.aspx?DAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataDal.Text)) & "&AL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataAl.Text)) & "&INIZIO=" & par.VaroleDaPassare(INIZIO) & "&FINE=" & par.VaroleDaPassare(FINE) & "');</script>")
                ElseIf Request.QueryString("CHIAMA") = "P" Then
                    Response.Write("<script>window.open('PropertyManagement.aspx?DAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataDal.Text)) & "&AL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataAl.Text)) & "&INIZIO=" & par.VaroleDaPassare(INIZIO) & "&FINE=" & par.VaroleDaPassare(FINE) & "');</script>")
                End If

                '*******CHIUSURA CONNESSIONE
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Me.txtDataDal.Text = INIZIO
                Me.txtDataAl.Text = FINE

            Else

                Response.Write("<script>alert('Intervallo non valido!')</script>")
                Me.txtDataAl.Text = ""
                Me.txtDataDal.Text = ""

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            'par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If

        txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")

    End Sub
End Class
