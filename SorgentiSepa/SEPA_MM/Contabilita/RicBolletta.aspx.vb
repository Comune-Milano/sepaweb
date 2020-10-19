
Partial Class Contabilita_RicBolletta
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim sValore As String
        Dim sCompara As String

        sStringaSql = ""
        Try
            If (Not String.IsNullOrEmpty(Me.txtNumBolletta.Text) And IsNumeric(txtNumBolletta.Text)) Or Not String.IsNullOrEmpty(Me.txtNumMav.Text) Then

                If Not String.IsNullOrEmpty(Me.txtNumBolletta.Text) Then
                    sValore = txtNumBolletta.Text.ToUpper
                    sCompara = " = "
                    sStringaSql = sStringaSql & " AND BOL_BOLLETTE.NUM_BOLLETTA " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                End If

                If Not String.IsNullOrEmpty(Me.txtNumMav.Text) Then
                    sValore = txtNumMav.Text.ToUpper
                    sCompara = " = "
                    sStringaSql = sStringaSql & " AND BOL_BOLLETTE.RIF_BOLLETTINO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                End If


                sStringaSql = "SELECT BOL_BOLLETTE.ID AS ID_BOLLETTA,BOL_BOLLETTE.ID_CONTRATTO, (ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME||' '||ANAGRAFICA.RAGIONE_SOCIALE) AS INTESTATARIO, ANAGRAFICA.ID AS ID_ANA, TO_CHAR(TO_DATE(RIFERIMENTO_DA,'yyyymmdd'),'dd/mm/yyyy') AS RIFERIMENTO_DA, TO_CHAR(TO_DATE(RIFERIMENTO_A,'yyyymmdd'),'dd/mm/yyyy') AS RIFERIMENTO_A,CASE WHEN BOL_BOLLETTE.FL_ANNULLATA = 0 THEN 'VALIDA' else 'ANNULLATA' END AS STATO ,'' AS IMPORTO, IMPORTO_PAGATO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA WHERE BOL_BOLLETTE.ID_CONTRATTO = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA .ID (+) AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'   " & sStringaSql & " ORDER BY ID_BOLLETTA ASC"

                Session.Add("RICBOLLETTA", sStringaSql)
                Response.Redirect("RisBolletta.aspx")

            Else
                Response.Write("<script>alert('Definire il Numero di Bolletta inserendo solo numeri o il numero mav!');</script>")
            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

    End Sub
End Class
