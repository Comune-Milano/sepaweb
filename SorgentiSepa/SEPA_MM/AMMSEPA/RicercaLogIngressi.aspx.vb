
Partial Class AMMSEPA_RicercaLogIngressi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtOperatore.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            par.RiempiDList(Me, par.OracleConn, "cmbEnte", "SELECT * FROM CAF_WEB ORDER BY COD_CAF ASC", "COD_CAF", "ID")
            cmbEnte.Items.Add("TUTTI")
            cmbEnte.Items.FindByText("TUTTI").Selected = True
        End If

        txtDataDal0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim btrovato As Boolean = False
        Dim sStringaSql As String = ""
        Dim sCompara As String = ""

        If txtCognome.Text <> "" Then
            If InStr(txtCognome.Text, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(txtCognome.Text)
            Else
                sCompara = " = "
            End If
            btrovato = True
            sStringaSql = sStringaSql & " OPERATORI.COGNOME " & sCompara & " '" & par.PulisciStrSql(txtCognome.Text) & "' "
        End If

        If txtNome.Text <> "" Then
            If btrovato = True Then sStringaSql = sStringaSql & " AND "
            If InStr(txtNome.Text, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(txtNome.Text)
            Else
                sCompara = " = "
            End If
            btrovato = True
            sStringaSql = sStringaSql & " OPERATORI.NOME " & sCompara & " '" & par.PulisciStrSql(txtNome.Text) & "' "
        End If

        If txtCF.Text <> "" Then
            If btrovato = True Then sStringaSql = sStringaSql & " AND "
            If InStr(txtCF.Text, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(txtCF.Text)
            Else
                sCompara = " = "
            End If
            btrovato = True
            sStringaSql = sStringaSql & " OPERATORI.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(txtCF.Text) & "' "
        End If

        If txtOperatore.Text <> "" Then
            If btrovato = True Then sStringaSql = sStringaSql & " AND "
            If InStr(txtOperatore.Text, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(txtOperatore.Text)
            Else
                sCompara = " = "
            End If
            btrovato = True
            sStringaSql = sStringaSql & " OPERATORI.operatore" & sCompara & "'" & par.PulisciStrSql(txtOperatore.Text) & "' "
        End If


        If txtDataDal0.Text <> "" Then
            If btrovato = True Then sStringaSql = sStringaSql & " AND "

            btrovato = True
            sStringaSql = sStringaSql & " OPERATORI_weB_log.data_ora_in>='" & par.PulisciStrSql(par.AggiustaData(txtDataDal0.Text)) & "' "
        End If

        If txtDataAl0.Text <> "" Then
            If btrovato = True Then sStringaSql = sStringaSql & " AND "

            btrovato = True
            sStringaSql = sStringaSql & " OPERATORI_weB_log.data_ora_out<='" & par.PulisciStrSql(par.AggiustaData(txtDataAl0.Text)) & "' "
        End If


        If cmbEnte.SelectedItem.Value <> " " And cmbEnte.SelectedItem.Text <> "TUTTI" Then
            If btrovato = True Then sStringaSql = sStringaSql & " AND "

            sCompara = " = "

            btrovato = True
            sStringaSql = sStringaSql & " OPERATORI.ID_caf " & sCompara & " " & par.PulisciStrSql(cmbEnte.SelectedItem.Value) & " "
        End If


        Session.Add("LOGINGRESSI", sStringaSql)

        Response.Write("<script>location.replace('LogIngressi.aspx?');</script>")

    End Sub
End Class
