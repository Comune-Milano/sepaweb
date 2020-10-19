
Partial Class ASS_ProvvedimentoAssegnazione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            'Response.Write("<script>window.open('ProvvedimentoAssegnazione.aspx?ID=" & LBLID.Text & "&OF=" & LBLPROGR.Text & "&SC=" & lblScad.Text & "','Provvedimento','');</script>")
            Label5.Text = "Assegnazione N. "
            OFFERTA.Value = Request.QueryString("OF")
            CFPIVA.Value = Request.QueryString("CF")
            If OFFERTA.Value <> "0" Then
                Label6.Text = OFFERTA.Value
            Else
                Label6.Text = "Offerta diretta"
            End If
            'lblAss.Text = "<a href=" & Chr(34) & "ProvvAssERP.aspx?ID=" & Request.QueryString("ID") & "&OF=" & Request.QueryString("OF") & "&SC=" & Request.QueryString("SC") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Clicca qui per stampare il provvedimento di Assegnazione</a>"
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                'par.cmd.CommandText = "SELECT REL_PRAT_ALL_CCAA_ERP.* FROM REL_PRAT_ALL_CCAA_ERP,ALLOGGI WHERE ALLOGGI.COD_ALLOGGIO='" & Request.QueryString("SC") & "' AND REL_PRAT_ALL_CCAA_ERP.ID_ALLOGGIO=ALLOGGI.ID AND REL_PRAT_ALL_CCAA_ERP.ID_PRATICA=" & Request.QueryString("ID") & " AND ULTIMO=1"
                '                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                lblAss.Text = "<a href=" & Chr(34) & "ProvvAssERP.aspx?ID=" & Request.QueryString("ID") & "&OF=" & OFFERTA.Value & "&SC=" & Request.QueryString("SC") & "&CF=" & CFPIVA.Value & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Clicca qui per stampare il provvedimento di Assegnazione</a>"
                'End If
                'myReaderA.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_ASSEGNATE WHERE N_OFFERTA=" & OFFERTA.Value & " OR CF_PIVA='" & CFPIVA.Value & "'"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    'If par.IfNull(myReaderA("PROVVEDIMENTO"), "") <> "" Then
                    txtData.Text = par.FormattaData(myReaderA("DATA_PROVVEDIMENTO"))
                    txtProvvedimento.Text = myReaderA("PROVVEDIMENTO")
                    'End If
                End If
                myReaderA.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Response.Write(ex.Message)
            End Try
        End If
            txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If H1.Value = "1" Then
            Try
                If par.AggiustaData(txtData.Text) <= Format(Now, "yyyyMMdd") Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    'If Len(CFPIVA.Value) <> 11 And Len(CFPIVA.Value) <> 16 Then
                    'par.cmd.CommandText = "update siscom_MI.unita_assegnate set provvedimento='" & par.PulisciStrSql(txtProvvedimento.Text) & "',data_provvedimento='" & par.AggiustaData(txtData.Text) & "' where n_offerta=" & OFFERTA.Value & " AND (CF_PIVA='' or cf_piva is null)"
                    'Else
                    par.cmd.CommandText = "update siscom_MI.unita_assegnate set DATA_INS_PROVV='" & Format(Now, "yyyyMMdd") & "',provvedimento='" & par.PulisciStrSql(txtProvvedimento.Text) & "',data_provvedimento='" & par.AggiustaData(txtData.Text) & "' where n_offerta=" & OFFERTA.Value
                    'End If

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Operazione Effettuata!');document.location.href='pagina_home.aspx';</script>")
                Else
                    Response.Write("<script>alert('Attenzione, la data del provvedimento deve essere precedente o uguale alla data odierna. Operazione NON Effettuata!');</script>")
                End If
            Catch ex As Exception
                par.OracleConn.Close()
                Label4.Text = ex.Message
            End Try
        End If
    End Sub
End Class
