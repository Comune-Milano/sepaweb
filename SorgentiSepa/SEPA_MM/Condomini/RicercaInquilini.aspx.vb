
Partial Class Condomini_RicercaInquilini
    Inherits PageSetIdMode
    Dim sStringaSql As String
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCf.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtRagSociale.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPiva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            'Response.Redirect("RisUtenza.aspx?COGNOME=" & par.VaroleDaPassare(par.PulisciStrSql(Me.txtCognome.Text)) & "&NOME=" & par.VaroleDaPassare(par.PulisciStrSql(Me.txtNome.Text)) & "&CF=" & par.VaroleDaPassare(par.PulisciStrSql(Me.txtCf.Text)) & "&CODCONTRATTO=" & par.VaroleDaPassare(par.PulisciStrSql(Me.txtCodContratto.Text)) & "&CODUI=" & par.VaroleDaPassare(par.PulisciStrSql(Me.txtCodUi.Text)) & "&FOGLIO=" & Me.txtFoglio.Text & "&PARTICELLA=" & Me.txtParticella.Text & "&SUB=" & Me.txtSub.Text&"&FOGLIO=" & Me.txtFoglio.Text)
            Dim sValore As String
            Dim sCompara As String

            sStringaSql = ""

            'If IdAnagrafica = "0" Then

            If par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" Then
                sValore = txtCognome.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.COGNOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" Then
                sValore = txtNome.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.NOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            'Else
            'sStringaSql = sStringaSql & " AND ANAGRAFICA.id = " & IdAnagrafica

            'End If

            If par.IfEmpty(Me.txtCf.Text, "Null") <> "Null" Then
                sValore = txtCf.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.COD_FISCALE) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            '*********RAGIONE SOCIALE E PARTITA IVA***

            If par.IfEmpty(Me.txtRagSociale.Text, "Null") <> "Null" Then
                sValore = txtRagSociale.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.RAGIONE_SOCIALE) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If


            If par.IfEmpty(Me.txtPiva.Text, "Null") <> "Null" Then
                sValore = txtPiva.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.PARTITA_IVA) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If par.IfEmpty(Me.txtCodContratto.Text, "Null") <> "Null" Then
                sValore = txtCodContratto.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(RAPPORTI_UTENZA.COD_CONTRATTO) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            sStringaSql = "SELECT CONDOMINI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, COD_CONTRATTO, (ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME||' '||ANAGRAFICA.RAGIONE_SOCIALE)AS INQUILINO, (ANAGRAFICA.COD_FISCALE||''||ANAGRAFICA.PARTITA_IVA) AS CF_IVA, CONDOMINI.DENOMINAZIONE AS CONDOMINIO FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.COND_EDIFICI, SISCOM_MI.CONDOMINI, SISCOM_MI.RAPPORTI_UTENZA WHERE ANAGRAFICA.ID= SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = UNITA_CONTRATTUALE.ID_CONTRATTO AND  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_EDIFICIO = COND_EDIFICI.ID_EDIFICIO AND CONDOMINI.ID = COND_EDIFICI.ID_CONDOMINIO  AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>= TO_CHAR(TO_DATE(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') " & sStringaSql & " ORDER BY INQUILINO ASC"
            Session.Add("RICINQUILINI", sStringaSql)
            Response.Redirect("RisultatiInquilini.aspx")

            '*****************FINE********************
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
End Class
