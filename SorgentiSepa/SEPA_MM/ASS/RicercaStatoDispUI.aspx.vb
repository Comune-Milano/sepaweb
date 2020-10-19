
Partial Class ASS_RicercaStatoDispUI
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
                '******DATA DISDETTA ******
                If par.IfEmpty(Me.TxtDisdettaDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtDisdettaDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_DISDETTA_LOCATARIO >= " & sValore
                End If
                If par.IfEmpty(Me.TxtDisdettaAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtDisdettaAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_DISDETTA_LOCATARIO <= " & sValore
                End If

                '******DATA SLOGGIO ******
                If par.IfEmpty(Me.TxtSlDal.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtSlDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_RICONSEGNA >= " & sValore
                End If
                If par.IfEmpty(Me.TxtSlAl.Text, "Null") <> "Null" Then
                    Dim sValore As String
                    sValore = par.AggiustaData(Me.TxtSlAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_RICONSEGNA <= " & sValore
                End If



            Else
                sStringaSQL = sStringaSQL & " AND (DATA_DISDETTA_LOCATARIO IS NOT NULL OR DATA_RICONSEGNA IS NOT NULL) "
            End If
            sStringaSQL = "SELECT RAPPORTI_UTENZA.ID, RTRIM(LTRIM(COGNOME ||' ' ||NOME||' ' || RAGIONE_SOCIALE)) AS ""INTESTATARIO"" , RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_DISDETTA_LOCATARIO"", TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_RICONSEGNA"", (select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA, UNITA_IMMOBILIARI.INTERNO, (RAPPORTI_UTENZA.TIPO_COR||' '|| RAPPORTI_UTENZA.VIA_COR||' '|| RAPPORTI_UTENZA.CIVICO_COR) AS ""INDIRIZZO"",RAPPORTI_UTENZA.CAP_COR AS ""CAP"", (RAPPORTI_UTENZA.LUOGO_COR||'('||RAPPORTI_UTENZA.SIGLA_COR||')') AS ""PROV_COMUNE"", TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""LIV_PIANO"",  UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.UNITA_CONTRATTUALE WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA= ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND  UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'" & sStringaSQL

            Session.Add("STATODISP", sStringaSQL)
            Response.Redirect("RisultatiStatoDispUI.aspx")

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

            Me.TxtDisdettaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtDisdettaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.TxtSlDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.TxtSlAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
