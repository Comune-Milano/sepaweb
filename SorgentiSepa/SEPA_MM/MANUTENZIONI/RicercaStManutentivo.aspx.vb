
Partial Class MANUTENZIONI_RicercaStManutentivo
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSQL As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Me.TxtConsChDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtConsChAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.TxtRipChDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtRipChAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim TuttoVuoto As Boolean = False
            For Each ctrl As Control In Me.form1.Controls
                If TypeOf (ctrl) Is TextBox Then
                    If String.IsNullOrEmpty(CType(ctrl, TextBox).Text) Then
                        TuttoVuoto = True
                    Else
                        TuttoVuoto = False
                        Exit For
                    End If
                End If
            Next
            If TuttoVuoto = False Then

                '******DATA CONSEGNA CHIAVI ******
                If par.IfEmpty(Me.TxtConsChDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtConsChDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_CONSEGNA_CHIAVI >= " & sValore
                End If
                If par.IfEmpty(Me.TxtConsChAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtConsChAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_CONSEGNA_CHIAVI <= " & sValore
                End If

                '******DATA RICONSEGNA CHIAVI ******
                If par.IfEmpty(Me.TxtRipChDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtRipChDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_RIPRESA_CHIAVI >= " & sValore
                End If
                If par.IfEmpty(Me.TxtRipChAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtRipChAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_RIPRESA_CHIAVI <= " & sValore
                End If

            Else

                sStringaSQL = sStringaSQL & " and (data_consegna_chiavi is not null or data_ripresa_chiavi is not null) "

            End If

            sStringaSQL = "select ID_UNITA, cod_unita_immobiliare, to_char(to_date(data_pre_sloggio,'yyyymmdd'),'dd/mm/yyyy') AS data_pre_sloggio, to_char(to_date(data_s,'yyyymmdd'),'dd/mm/yyyy') AS data_s, to_char(to_date(data_consegna_chiavi,'yyyymmdd'),'dd/mm/yyyy') AS data_consegna_chiavi, to_char(to_date(data_ripresa_chiavi,'yyyymmdd'),'dd/mm/yyyy') AS data_ripresa_chiavi,consegnate_a as ditta from siscom_mi.unita_stato_manutentivo, siscom_mi.unita_immobiliari where unita_immobiliari.id = unita_stato_manutentivo.id_unita" & sStringaSQL
            Session.Add("RICSTMANUT", sStringaSQL)
            Response.Redirect("RisultatiStManutentivo.aspx")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
