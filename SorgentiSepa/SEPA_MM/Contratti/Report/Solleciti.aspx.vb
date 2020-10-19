
Partial Class Contratti_Report_Solleciti
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim StringaSQL As String = "SELECT bol_bollette.cod_affittuario as idana,BOL_BOLLETTE.ID,rapporti_utenza.id as ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO AS CONTRATTO,CASE WHEN BOL_BOLLETTE.N_RATA=99 THEN 'MA' WHEN BOL_BOLLETTE.N_RATA=999 THEN 'AU' WHEN BOL_BOLLETTE.N_RATA=99999 THEN 'CO' ELSE TO_CHAR(BOL_BOLLETTE.N_RATA) END||'/'||BOL_BOLLETTE.ANNO AS RATA,TO_CHAR(TO_DATE(BOL_BOLLETTE.RIFERIMENTO_DA,'yyyymmdd'),'dd/mm/yyyy') AS RIFERIMENTO_DA,TO_CHAR(TO_DATE(BOL_BOLLETTE.RIFERIMENTO_A,'yyyymmdd'),'dd/mm/yyyy') AS RIFERIMENTO_A,BOL_BOLLETTE.INTESTATARIO,BOL_BOLLETTE.INDIRIZZO,TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS EMISSIONE,TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA,TO_CHAR(TO_DATE(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') AS PAGATA,TO_CHAR(TO_DATE(BOL_BOLLETTE_SOLLECITI.DATA_INVIO,'yyyymmdd'),'dd/mm/yyyy') AS SOLLECITO,BOL_BOLLETTE_TOT.IMPORTO_TOTALE AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_TOT,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_SOLLECITI WHERE RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID=BOL_BOLLETTE_SOLLECITI.ID_BOLLETTA AND BOL_BOLLETTE.FL_ANNULLATA='0' AND BOL_BOLLETTE.FL_STAMPATO='1' AND BOL_BOLLETTE_TOT.ID_BOLLETTA=BOL_BOLLETTE.ID "

            If par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") <> "Null" And par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") <> "Null" Then
                If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
                    Response.Write("<script>alert('Intervallo date non valido (DATA EMISSIONE)!')</script>")
                    Exit Sub
                End If
            End If

            If PAR.IfEmpty(PAR.AggiustaData(Me.txtDataAl0.Text), "Null") <> "Null" And PAR.IfEmpty(PAR.AggiustaData(Me.txtDataDal0.Text), "Null") <> "Null" Then
                If PAR.AggiustaData(Me.txtDataDal0.Text) > PAR.AggiustaData(Me.txtDataAl0.Text) Then
                    Response.Write("<script>alert('Intervallo date non valido (DATA RIFERIMENTO)!')</script>")
                    Exit Sub
                End If
            End If

            If PAR.IfEmpty(PAR.AggiustaData(Me.txtDataAl1.Text), "Null") <> "Null" And PAR.IfEmpty(PAR.AggiustaData(Me.txtDataDal1.Text), "Null") <> "Null" Then
                If PAR.AggiustaData(Me.txtDataDal1.Text) > PAR.AggiustaData(Me.txtDataAl1.Text) Then
                    Response.Write("<script>alert('Intervallo date non valido (DATA SOLLECITO)!')</script>")
                    Exit Sub
                End If
            End If

            If PAR.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_EMISSIONE>= " & PAR.AggiustaData(Me.txtDataDal.Text)
            End If

            If PAR.IfEmpty(Me.txtDataAl.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_EMISSIONE<= " & PAR.AggiustaData(Me.txtDataAl.Text)
            End If

            If PAR.IfEmpty(Me.txtDataDal0.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND RIFERIMENTO_DA>= " & PAR.AggiustaData(Me.txtDataDal0.Text)
            End If

            If PAR.IfEmpty(Me.txtDataAl0.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND RIFERIMENTO_A<= " & PAR.AggiustaData(Me.txtDataAl0.Text)
            End If

            If PAR.IfEmpty(Me.txtDataDal1.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND BOL_BOLLETTE_SOLLECITI.DATA_INVIO>= " & PAR.AggiustaData(Me.txtDataDal1.Text)
            End If

            If PAR.IfEmpty(Me.txtDataAl1.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND BOL_BOLLETTE_SOLLECITI.DATA_INVIO<= " & PAR.AggiustaData(Me.txtDataAl1.Text)
            End If

            StringaSQL = StringaSQL & " ORDER BY INTESTATARIO ASC,BOL_BOLLETTE.ID ASC,BOL_BOLLETTE_SOLLECITI.DATA_INVIO desc "
            Session.Add("REPORT_S", StringaSQL)
            Response.Write("<script>window.open('StampaSolleciti.aspx?sDAL=" & PAR.AggiustaData(Me.txtDataDal1.Text) & "&sAL=" & PAR.AggiustaData(Me.txtDataAl1.Text) & "&eDAL=" & PAR.AggiustaData(Me.txtDataDal.Text) & "&eAL=" & PAR.AggiustaData(Me.txtDataAl.Text) & "&rDAL=" & PAR.AggiustaData(Me.txtDataDal0.Text) & "&rAL=" & PAR.AggiustaData(Me.txtDataAl0.Text) & "');</script>")



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If

        txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtDataDal0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtDataDal1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href='../../Contabilita/pagina_home.aspx';</script>")
    End Sub
End Class
