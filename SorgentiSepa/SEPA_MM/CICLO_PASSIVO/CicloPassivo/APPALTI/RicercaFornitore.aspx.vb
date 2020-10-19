
Partial Class APPALTI_RicercaFornitore
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsNothing(Session.Item("LAVORAZIONE")) Then
            Session.Remove("LAVORAZIONE")
        End If
        If Not IsPostBack Then
            txtCodice.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCodiceFiscale.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPIva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If
    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Try
            Dim stringaParametri As String = ""
            If Trim(txtCodice.Text) = "" Then
                stringaParametri = stringaParametri & "CO=---&"
            Else
                stringaParametri = stringaParametri & "CO=" & txtCodice.Text & "&"
            End If
            If Trim(txtRagione.Text) = "" Then
                stringaParametri = stringaParametri & "RA=---&"
            Else
                stringaParametri = stringaParametri & "RA=" & txtRagione.Text & "&"
            End If
            If Trim(txtCodiceFiscale.Text) = "" Then
                stringaParametri = stringaParametri & "CF=---&"
            Else
                stringaParametri = stringaParametri & "CF=" & txtCodiceFiscale.Text & "&"
            End If

            If Trim(txtPIva.Text) = "" Then
                stringaParametri = stringaParametri & "PI=---"
            Else
                stringaParametri = stringaParametri & "PI=" & txtPIva.Text
            End If
            Response.Redirect("RisultatiFornitori.aspx?" & stringaParametri, False)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub RadButtonPulisci_Click(sender As Object, e As System.EventArgs) Handles RadButtonPulisci.Click
        Response.Redirect("RicercaFornitore.aspx", False)
    End Sub
End Class
