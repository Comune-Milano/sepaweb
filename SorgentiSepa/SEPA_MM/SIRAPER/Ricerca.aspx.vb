
Partial Class SIRAPER_Ricerca
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        Page.Form.DefaultButton = Me.BtnCerca.UniqueID
        If Not IsPostBack Then
            Me.txtDataRiferimentoDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRiferimentoA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtPartitaIva.Attributes.Add("onkeyup", "javascript:valid(this,'codice');")
            par.caricaComboBox("SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_ENTE_SIRAPER", ddlTipoEnte, "COD", "DESCRIZIONE", True)
        End If
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
       Response.Redirect("Home.aspx", False)
    End Sub
    Protected Sub BtnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles BtnCerca.Click
        Try
            Dim condizioni As String = ""
            Dim Primo As Boolean = True
            If Not String.IsNullOrEmpty(txtSiglaEnte.Text) Then
                If Primo Then
                    condizioni += "SE=" & txtSiglaEnte.Text
                    Primo = False
                Else
                    condizioni += "&SE=" & txtSiglaEnte.Text
                End If
            End If
            If ddlTipoEnte.SelectedValue.ToString <> "-1" Then
                If Primo Then
                    condizioni += "TE=" & ddlTipoEnte.SelectedValue
                    Primo = False
                Else
                    condizioni += "&TE=" & ddlTipoEnte.SelectedValue
                End If
            End If
            If Not String.IsNullOrEmpty(txtDataRiferimentoDa.Text) Then
                If Primo Then
                    condizioni += "DRD=" & par.AggiustaData(txtDataRiferimentoDa.Text)
                    Primo = False
                Else
                    condizioni += "&DRD=" & par.AggiustaData(txtDataRiferimentoDa.Text)
                End If
            End If
            If Not String.IsNullOrEmpty(txtDataRiferimentoA.Text) Then
                If Primo Then
                    condizioni += "DRA=" & par.AggiustaData(txtDataRiferimentoA.Text)
                    Primo = False
                Else
                    condizioni += "&DRA=" & par.AggiustaData(txtDataRiferimentoA.Text)
                End If
            End If
            If Not String.IsNullOrEmpty(txtCodFiscale.Text) Then
                If Primo Then
                    condizioni += "CF=" & txtCodFiscale.Text
                    Primo = False
                Else
                    condizioni += "&CF=" & txtCodFiscale.Text
                End If
            End If
            If Not String.IsNullOrEmpty(txtPartitaIva.Text) Then
                If Primo Then
                    condizioni += "PI=" & txtPartitaIva.Text
                    Primo = False
                Else
                    condizioni += "&PI=" & txtPartitaIva.Text
                End If
            End If
            If Not String.IsNullOrEmpty(txtRagioneSociale.Text) Then
                If Primo Then
                    condizioni += "RS=" & txtRagioneSociale.Text
                    Primo = False
                Else
                    condizioni += "&RS=" & txtRagioneSociale.Text
                End If
            End If
            If String.IsNullOrEmpty(condizioni) Then
                Response.Redirect("RisultatiElaborazioni.aspx", False)
            Else
                Response.Redirect("RisultatiElaborazioni.aspx?" & condizioni, False)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Ricerca - BtnCerca_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
