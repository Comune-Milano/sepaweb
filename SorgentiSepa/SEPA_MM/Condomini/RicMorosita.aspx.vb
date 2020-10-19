
Partial Class Condomini_RicMorosita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        If Not IsPostBack Then
            CaricaComboCondomini()

            txtRifDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtRifAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

    Private Sub CaricaComboCondomini()
        Try


            par.caricaComboBox("SELECT ID, denominazione FROM siscom_mi.CONDOMINI ORDER BY denominazione ASC ", cmbCondominio, "ID", "DENOMINAZIONE", True)


        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If cmbCondominio.SelectedValue <> "-1" Then
            If Not String.IsNullOrEmpty(txtRifDa.Text) And Not String.IsNullOrEmpty(txtRifAl.Text) Then
                If par.AggiustaData(txtRifDa.Text) > par.AggiustaData(txtRifAl.Text) Then
                    Response.Write("<script>alert('La data di riferimento finale deve essere maggiore di quella di inizio. Riprovare!');</script>")
                    Exit Sub
                End If
            End If
            Response.Write("<script>window.open('RptMorCondomini.aspx?IDCOND=" & Me.cmbCondominio.SelectedValue.ToString & "&DAL=" & Me.txtRifDa.Text & "&AL=" & Me.txtRifAl.Text & "&INQ=" & Me.chkInquilino.Checked & "&CSOL=" & Me.chkContrSolid.Checked & "&MAV=" & Me.chkInfoMav.Checked & "&FSOL=" & Me.chkFonSocial.Checked & "');</script>")
        Else
            Response.Write("<script>alert('Inserire un condominio!');</script>")
        End If
        
    End Sub

End Class
