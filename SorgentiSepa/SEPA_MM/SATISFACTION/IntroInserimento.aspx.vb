
Partial Class SATISFACTION_IntroInserimento
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        If Not IsPostBack Then
            'txt_data.Text = Format(Now(), "dd/MM/yyyy")
            txt_data.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txt_codui.Attributes.Add("onkeypress", "javascript:this.value=this.value.toUpperCase();")
            txt_codui.Attributes.Add("onchange", "javascript:this.value=this.value.toUpperCase();")
        End If
    End Sub


    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNext.Click

        Dim id_unita As Long = -1
        Dim cod_ui As String = par.PulisciStrSql(Trim(txt_codui.Text))
        Dim data_compilaz As String = txt_data.Text
        Dim id_indir As String = ""
        Dim contr As Integer = 0
        Dim url As String = ""

        If Not cod_ui = "" And Not data_compilaz = "" Then
            If Not par.ControllaData(txt_data) Then
                Response.Write("<script>alert('Inserire una data valida!');</script>")
            Else
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE ='" & cod_ui & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader.Read Then
                        id_unita = myReader("ID")
                        id_indir = par.IfNull(myReader("ID_INDIRIZZO"), " ")
                    Else
                        id_unita = -1
                        Response.Write("<script>alert('Codice unità immobiliare non corretto!');</script>")
                    End If

                    myReader.Close()

                    'modifica 1 settembre 2011 - (controllo se è stata già inserita una scheda per una data UI)

                    par.cmd.CommandText = "SELECT SISCOM_MI.CUSTOMER_SATISFACTION.* FROM SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI WHERE CUSTOMER_SATISFACTION.ID_UNITA = SISCOM_MI.UNITA_IMMOBILIARI.ID AND COD_UNITA_IMMOBILIARE ='" & cod_ui & "'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        url = "SchedaQuestionario.aspx?chk=1&id=" & myReader("ID") & "&idU=" & myReader("ID_UNITA") & "&codUI=" & cod_ui & _
                            "&data=" & myReader("DATA_COMPILAZIONE") & "&idInd=" & id_indir & ""
                        lblMess.Text = "Scheda già presente per l'unità selezionata! Per visualizzarla fare click <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('" & url & "','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">qui</a>"
                        contr = 1
                    End If
                    myReader.Close()

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    If id_unita <> -1 And contr = 0 Then
                        Response.Redirect("SchedaQuestionario.aspx?id=-1&idU=" & id_unita & "&codUI=" & cod_ui & _
                                          "&data=" & par.AggiustaData(data_compilaz) & "&idInd=" & id_indir)
                    End If

                Catch ex As Exception

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write(ex.Message)

                End Try
            End If
        Else
            Response.Write("<script>alert('Inserire il codice dell\'unita\' immobiliare e la data!');</script>")
        End If
    End Sub

End Class
