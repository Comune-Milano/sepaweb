
Partial Class PED_RicercaOccupante
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String

            bTrovato = False
            sStringaSql = ""

            If txtCognome.Text <> "" Then
                sValore = UCase(txtCognome.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.COGNOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtNome.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtNome.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.NOME)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtCF.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtCF.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.COD_FISCALE)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtRS.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtRS.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.RAGIONE_SOCIALE)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtIva.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtIva.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.PARTITA_IVA)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

            sStringaSql = "select CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"",unita_immobiliari.*,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from " _
                        & "SISCOM_MI.ANAGRAFICA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.unita_immobiliari,SISCOM_MI.identificativi_catastali,SISCOM_MI.edifici,SISCOM_MI.INDIRIZZI where " _
                        & "unita_immobiliari.cod_tipo_disponibilita<>'VEND' and unita_immobiliari.id_indirizzo=INDIRIZZI.ID (+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND " _
                        & "UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID (+) AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " & sStringaSql & " ORDER BY INTESTATARIO ASC"


            Session.Add("PEDOCC", sStringaSql)
            Response.Redirect("RisultatiOccupante.aspx?T=0")
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
End Class
