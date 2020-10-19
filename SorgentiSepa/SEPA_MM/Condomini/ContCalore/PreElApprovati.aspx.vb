
Partial Class Condomini_ContCalore_PreElApprovati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then

            If Request.QueryString("TIPO") = "NUOVO" Then
                Me.lblTitolo.Text = "ELENCO INQUILINI CON PREVENTIVO CONTRIBUTO CALORE"
                par.caricaComboBox("SELECT CONT_CALORE_ANNO.ID, anno FROM siscom_mi.CONT_CALORE_ANNO,SISCOM_MI.TIPO_CALCOLO_CONT_CALORE WHERE TIPO_CALCOLO_CONT_CALORE.id = ID_STATO and id_stato >= 2", cmbContCalore, "ID", "anno", True)
            ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                Me.lblTitolo.Text = "ELENCO INQUILINI CON CONSUNTIVO CONTRIBUTO CALORE"
                par.caricaComboBox("SELECT CONT_CALORE_ANNO.ID, anno FROM siscom_mi.CONT_CALORE_ANNO,SISCOM_MI.TIPO_CALCOLO_CONT_CALORE WHERE TIPO_CALCOLO_CONT_CALORE.id = ID_STATO and id_stato = 4", cmbContCalore, "ID", "anno", True)
            End If
            par.caricaComboBox("SELECT ID, DENOMINAZIONE FROM SISCOM_MI.CONDOMINI WHERE TIPO_GESTIONE = 'D' ORDER BY DENOMINAZIONE", cmbcondomini, "ID", "DENOMINAZIONE", True)

        End If

    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If rblTipoStampe.SelectedValue <> "" Then
            If cmbContCalore.SelectedValue <> "-1" Then
                If Request.QueryString("TIPO") = "NUOVO" Then
                    Response.Write("<script>window.open('ElApprovati.aspx?ANNO=" & Me.cmbContCalore.SelectedItem.Text & "&TIPO=NUOVO&IDCONTRIBUTO=" & Me.cmbContCalore.SelectedValue & "&COND=" & cmbcondomini.SelectedValue & "&STATO=" & rblTipoStampe.SelectedValue & "','','');</script>")
                ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                    Response.Write("<script>window.open('ElApprovati.aspx??ANNO=" & Me.cmbContCalore.SelectedItem.Text & "&TIPO=CONGUAGLIO&IDCONTRIBUTO=" & Me.cmbContCalore.SelectedValue & "&COND=" & cmbcondomini.SelectedValue & "&STATO=" & rblTipoStampe.SelectedValue & "','','');</script>")
                End If
            Else
                Response.Write("<script>alert('Selezionare un\' anno prima di procedere!');</script>")
            End If
        Else
            Response.Write("<script>alert('Selezionare il tipo di preventivo!');</script>")
        End If
    End Sub
End Class
