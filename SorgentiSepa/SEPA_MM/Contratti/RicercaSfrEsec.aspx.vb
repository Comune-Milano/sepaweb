
Partial Class Contratti_RicercaSfrEsec
    Inherits PageSetIdMode
    Dim sStringaSQL As String = ""
    Dim par As New CM.Global


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

                '******DATA CONVALIDA SFRATTO ******
                If par.IfEmpty(Me.TxtConvSfDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtConvSfDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_CONVALIDA_SFRATTO >= " & sValore
                End If
                If par.IfEmpty(Me.TxtConvSfAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtConvSfAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_CONVALIDA_SFRATTO <= " & sValore
                End If

                '******DATA SFRATTO ESECUTIVO ******
                If par.IfEmpty(Me.TxtSfEsecDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtSfEsecDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_ESECUZIONE_SFRATTO >= " & sValore
                End If
                If par.IfEmpty(Me.TxtSfEsecAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtSfEsecAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_ESECUZIONE_SFRATTO <= " & sValore
                End If
                '**********DATA CONFERMA F. P.
                If par.IfEmpty(Me.TxtDataConfFPDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtDataConfFPDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_CONFERMA_FP >= " & sValore
                End If
                If par.IfEmpty(Me.TxtDataConfFPAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtDataConfFPAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_CONFERMA_FP <= " & sValore
                End If
                '*********DATA RINVIO SFRATTO
                If par.IfEmpty(Me.TxtDataRinvDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtDataRinvDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_RINVIO_SFRATTO >= " & sValore
                End If
                If par.IfEmpty(Me.TxtDataRinvAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtDataRinvAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_RINVIO_SFRATTO <= " & sValore
                End If
            Else
                sStringaSQL = sStringaSQL & " AND (DATA_CONVALIDA_SFRATTO IS NOT NULL OR DATA_ESECUZIONE_SFRATTO IS NOT NULL OR DATA_RINVIO_SFRATTO IS NOT NULL OR DATA_CONFERMA_FP IS NOT NULL)"
            End If
            sStringaSQL = "SELECT RAPPORTI_UTENZA.ID,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" ,CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS  ""CFIVA"", RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_CONVALIDA_SFRATTO,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_CONVALIDA_SFRATTO"", TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_ESECUZIONE_SFRATTO,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_ESECUZIONE_SFRATTO"",TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RINVIO_SFRATTO,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_RINVIO_SFRATTO"",TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_CONFERMA_FP,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_CONFERMA_FP"", (select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA, UNITA_IMMOBILIARI.INTERNO, (RAPPORTI_UTENZA.TIPO_COR||' '|| RAPPORTI_UTENZA.VIA_COR||' '|| RAPPORTI_UTENZA.CIVICO_COR) AS INDIRIZZO,RAPPORTI_UTENZA.CAP_COR AS CAP, (RAPPORTI_UTENZA.LUOGO_COR||'('||RAPPORTI_UTENZA.SIGLA_COR||')') AS PROV_COMUNE, TIPO_LIVELLO_PIANO.DESCRIZIONE,  UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.UNITA_CONTRATTUALE WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA= ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND  UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " & sStringaSQL
            Session.Add("RICSFRATTO", sStringaSQL)
            Response.Redirect("RisultSfEsec.aspx")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Me.TxtConvSfDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtConvSfAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.TxtSfEsecDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtSfEsecAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.TxtDataRinvDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtDataRinvAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.TxtDataConfFPDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtDataConfFPAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If


    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
