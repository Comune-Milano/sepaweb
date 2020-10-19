
Partial Class Contratti_RicercaScadenza
    Inherits PageSetIdMode
    Dim sStringaSQL As String = ""
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Me.txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Me.TextBox1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Me.TextBox2.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        If Not IsPostBack Then
            RiempiTipologie()
        End If

    End Sub


    Private Function RiempiTipologie()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter


        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Function
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If


        dlist = CheckTipologie

        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT cod,descrizione FROM siscom_mi.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY cod ASC", par.OracleConn)
        da.Fill(ds)

        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = "DESCRIZIONE"
        dlist.DataValueField = "COD"
        dlist.DataBind()

        da.Dispose()
        da = Nothing

        dlist.DataSource = Nothing
        dlist = Nothing

        ds.Clear()
        ds.Dispose()
        ds = Nothing

        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Function


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try

            If par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null" Or par.IfEmpty(Me.txtDataAl.Text, "Null") <> "Null" Or par.IfEmpty(Me.TextBox1.Text, "Null") <> "Null" Or par.IfEmpty(Me.TextBox2.Text, "Null") <> "Null" Then

                Dim sValore As String
                If (par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null") Then
                    sValore = par.AggiustaData(Me.txtDataDal.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_SCADENZA_RINNOVO >= " & sValore
                End If

                If (par.IfEmpty(Me.txtDataAl.Text, "Null") <> "Null") Then
                    sValore = par.AggiustaData(Me.txtDataAl.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_SCADENZA_RINNOVO <= " & sValore
                End If

                If (par.IfEmpty(Me.TextBox1.Text, "Null") <> "Null") Then
                    sValore = par.AggiustaData(Me.TextBox1.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_SCADENZA >= " & sValore
                End If

                If (par.IfEmpty(Me.TextBox2.Text, "Null") <> "Null") Then
                    sValore = par.AggiustaData(Me.TextBox2.Text)
                    sStringaSQL = sStringaSQL & " AND DATA_SCADENZA <= " & sValore
                End If


                If Me.CmbRinnovoAmmissibile.SelectedValue <> "NON DEFINITO" Then
                    sValore = Me.CmbRinnovoAmmissibile.SelectedValue.ToString
                    sStringaSQL = sStringaSQL & " AND RAPPORTI_UTENZA_CONTROLLO.RINNOVO_OK= " & sValore ' VALORE DEFINITO DALLA COMBO 0-1
                Else
                    sStringaSQL = sStringaSQL & " AND RAPPORTI_UTENZA_CONTROLLO.RINNOVO_OK IS NULL " ' COMBO LASCIATA IN BIANCO QUINDI VALORE PRESUPPOSTO 0/NULL
                End If

                If Me.cmbNotificato.SelectedValue <> "" Then
                    If Me.cmbNotificato.SelectedValue = 1 Then
                        sStringaSQL = sStringaSQL & " AND RAPPORTI_UTENZA.DATA_NOTIFICA_DISDETTA IS NOT NULL" 'il campo data_notifica_disdetta è pieno è stato notificato all'utente la scadenza
                    Else
                        sStringaSQL = sStringaSQL & " AND RAPPORTI_UTENZA.DATA_NOTIFICA_DISDETTA IS NULL" ' il campo data_notifica_disdetta è vuoto non è stato notificato all'utente la scadenza
                    End If

                End If


                Dim Tipologie As String = ""
                Dim i As Integer

                For i = 0 To CheckTipologie.Items.Count - 1
                    If CheckTipologie.Items(i).Selected Then
                        Tipologie = Tipologie & "'" & CheckTipologie.Items(i).Value & "',"
                    End If
                Next
                If Tipologie = "" Then
                    Response.Write("<script>alert('Selezionare almeno una Tipologia di contratto');</script>")
                    Exit Sub
                Else
                    Tipologie = Mid(Tipologie, 1, Len(Tipologie) - 1)
                    sStringaSQL = sStringaSQL & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC IN (" & Tipologie & ") "
                End If



                sStringaSQL = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" ,CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""CFIVA"",RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,RAPPORTI_UTENZA.ID, RAPPORTI_UTENZA.COD_CONTRATTO,to_char(to_date(RAPPORTI_UTENZA.DATA_SCADENZA_RINNOVO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_SCADENZA_RINNOVO  ,to_char(to_date(RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_DISDETTA_LOCATARIO  ,to_char(to_date(RAPPORTI_UTENZA.DATA_NOTIFICA_DISDETTA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NOTIFICA_DISDETTA ,  (select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA, UNITA_IMMOBILIARI.INTERNO, (RAPPORTI_UTENZA.TIPO_COR||' '|| RAPPORTI_UTENZA.VIA_COR||' '|| RAPPORTI_UTENZA.CIVICO_COR) AS INDIRIZZO,RAPPORTI_UTENZA.CAP_COR AS CAP, (RAPPORTI_UTENZA.LUOGO_COR||'('||RAPPORTI_UTENZA.SIGLA_COR||')') AS PROV_COMUNE, TIPO_LIVELLO_PIANO.DESCRIZIONE, DECODE(RAPPORTI_UTENZA_CONTROLLO.RINNOVO_OK,1,'SI',0,'NO') AS RINNOVABILE, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA= ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND RAPPORTI_UTENZA.ID = RAPPORTI_UTENZA_CONTROLLO.ID_CONTRATTO(+) AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' and substr(rapporti_utenza.cod_contratto,1,6)<>'000000' " & sStringaSQL
                sStringaSQL = sStringaSQL & " ORDER BY DATA_SCADENZA_RINNOVO ASC"
                Session.Add("RICSCADENZA", sStringaSQL)
                Response.Redirect("RisultatiScadenza.aspx")
            Else
                Response.Write("<script>alert('Definire almeno l\'inizio dell\'intervallo di tempo relativo alla scadenza di rinnovo o prima scadenza dei contratti!');</script>")

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Redirect("pagina_home.aspx")

    End Sub

End Class
