
Partial Class Condomini_RicPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then


            Me.txtAnnoDa.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            Me.txtAnnoA.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

            Me.txtNumAdpDA.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            Me.txtNumAdpA.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

            Me.txtImportoDA.Attributes.Add("onBlur", "javascript:AutoDecimal2(this);return false;")
            Me.txtImportoA.Attributes.Add("onBlur", "javascript:AutoDecimal2(this);return false;")

            Me.txtNumMandatoDA.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            Me.txtNumMandatoA.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")


            txtDataMandatoDA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataMandatoA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            CaricaCondomini()


        End If
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Try

            Dim CondSelezionati As String = ""
            For Each i As ListItem In chkCondomini.Items
                If i.Selected = True Then
                    If Not String.IsNullOrEmpty(CondSelezionati) Then
                        CondSelezionati += "," & i.Value
                    Else
                        CondSelezionati = i.Value
                    End If
                End If
            Next

            If Not String.IsNullOrEmpty(txtAnnoDa.Text) And Not String.IsNullOrEmpty(txtAnnoA.Text) Then
                If txtAnnoDa.Text > txtAnnoA.Text Then
                    Response.Write("<script>alert('L\' anno di ricerca A.D.P. finale deve essere maggiore di quella iniziale. Riprovare!');</script>")
                    Exit Sub
                End If
            End If

            If Not String.IsNullOrEmpty(txtDataMandatoDA.Text) And Not String.IsNullOrEmpty(txtDataMandatoA.Text) Then
                If par.AggiustaData(txtDataMandatoDA.Text) > par.AggiustaData(txtDataMandatoA.Text) Then
                    Response.Write("<script>alert('La data di ricerca mandato finale deve essere maggiore di quella iniziale. Riprovare!');</script>")
                    Exit Sub
                End If
            End If

            Response.Redirect("RisultatiPagamenti.aspx?" _
                              & "ANNODA=" & par.IfEmpty(Me.txtAnnoDa.Text, "") & "&ANNOA=" & par.IfEmpty(Me.txtAnnoA.Text, "") _
                              & "&NUMADPDA=" & par.IfEmpty(Me.txtNumAdpDA.Text, "") & "&NUMADPA=" & par.IfEmpty(Me.txtNumAdpA.Text, "") _
                              & "&IMPADPDA=" & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoDA.Text, "").ToString.Replace(".", "")) & "&IMPADPA=" & par.VirgoleInPunti(par.IfEmpty(Me.txtImportoA.Text, "").ToString.Replace(".", "")) _
                              & "&NUMMANDDA=" & par.IfEmpty(Me.txtNumMandatoDA.Text, "") & "&NUMMANDA=" & par.IfEmpty(Me.txtNumMandatoA.Text, "") _
                              & "&DATAMANDDA=" & par.AggiustaData(par.IfEmpty(Me.txtDataMandatoDA.Text, "")) & "&DATAMANDA=" & par.AggiustaData(par.IfEmpty(Me.txtDataMandatoA.Text, "")) & "&COND=" & CondSelezionati)

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub


    Private Sub CaricaCondomini()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            par.cmd.CommandText = "SELECT ID, denominazione FROM SISCOM_MI.CONDOMINI order by denominazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            Me.chkCondomini.DataSource = dt
            Me.chkCondomini.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza: CaricaCondomini " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
