Partial Class IMPIANTI_RicercaImpianti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                Me.RiempiCampi()
            End If
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCf.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCodContratto.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCodUi.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtRagSociale.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPiva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub RiempiCampi()
        CaricaIndirizzi()
    End Sub
    Private Sub CaricaIndirizzi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbIndirizzo.Items.Add(" ")

            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
            End While
            myReader1.Close()

            cmbIndirizzo.Text = " "

            cmbCivico.Items.Clear()

            If cmbIndirizzo.Text <> " " Then


                par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
                End While
                myReader2.Close()
            End If

            cmbInterno.Items.Clear()
            If cmbCivico.Text <> "" Then
                cmbInterno.Items.Add((New ListItem(" ", "-1")))

                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader3.Read
                    cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
                End While
                myReader3.Close()
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                cmbCivico.Items.Clear()

                par.cmd.CommandText = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " ")))
                End While
                myReader1.Close()

                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If cmbCivico.Text <> "" Then
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale IN (SELECT ID FROM siscom_mi.INDIRIZZI WHERE INDIRIZZI.descrizione = '" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "' AND INDIRIZZI.CIVICO = '" & par.PulisciStrSql(Me.cmbCivico.SelectedItem.Text) & "' ) and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
                    End While
                    myReader3.Close()
                End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub cmbCivico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCivico.SelectedIndexChanged
        Try
            If cmbCivico.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If cmbCivico.Text <> "" Then
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale IN (SELECT ID FROM siscom_mi.INDIRIZZI WHERE INDIRIZZI.descrizione = '" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "' AND INDIRIZZI.CIVICO = '" & par.PulisciStrSql(Me.cmbCivico.SelectedItem.Text) & "') and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader2("interno"), " "), par.IfNull(myReader2("interno"), "-1"))))
                    End While
                    myReader2.Close()
                End If
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        'Try
        '    Dim StrAppoggio As String = ""
        '    Dim dt As New Data.DataTable
        '    'Me.RdbListUtente.Items.Clear()
        '    Dim sValore As String
        '    Dim sCompara As String

        '    If par.IfEmpty(Me.txtCf.Text, "Null") = "Null" And (par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" Or par.IfEmpty(Me.txtNome.Text, "Null") <> "Null") Then
        '        If par.OracleConn.State = Data.ConnectionState.Closed Then
        '            par.OracleConn.Open()
        '            par.SettaCommand(par)
        '        End If
        '        If par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" Then
        '            sValore = Trim(txtCognome.Text)
        '            If InStr(sValore, "*") Then
        '                sCompara = " LIKE "
        '                Call par.ConvertiJolly(sValore)
        '            Else
        '                sCompara = " = "
        '            End If
        '            StrAppoggio = StrAppoggio & "AND ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        '        End If
        '        If par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" Then
        '            sValore = Trim(txtNome.Text)
        '            If InStr(sValore, "*") Then
        '                sCompara = " LIKE "
        '                Call par.ConvertiJolly(sValore)
        '            Else
        '                sCompara = " = "
        '            End If
        '            StrAppoggio = StrAppoggio & "AND ANAGRAFICA.NOME" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        '        End If

        '        par.cmd.CommandText = "SELECT ANAGRAFICA.ID,COGNOME, NOME, COD_FISCALE, DATA_NASCITA FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA  AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " & StrAppoggio
        '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '        da.Fill(dt)
        '        If dt.Rows.Count > 1 Then
        '            'Int16 caso in cui il cognome non è univoco.
        '            Me.DivVisible.Value = 2
        '            Dim i As Integer = 0
        '            While i < dt.Rows.Count
        '                Me.RdbListUtente.Items.Add(New ListItem(par.IfNull(dt.Rows(i).Item("COGNOME"), "") & " " & par.IfNull(dt.Rows(i).Item("NOME"), "") & " nato il " & par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_NASCITA"), "")) & " c.f " & par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "################"), dt.Rows(i).Item("ID")))
        '                i = i + 1
        '            End While
        '            Me.RdbListUtente.Items(0).Selected = True
        '        Else
        '            Dim IdFittizzio As String = "0"
        '            CompletaRicerca()
        '            ''In caso il cognome dell'utente è unicvoco e permette di individuare direttamente informazioni ad esso associate
        '        End If

        '        '*********************CHIUSURA CONNESSIONE**********************
        '        par.cmd.Dispose()
        '        par.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    Else
        '        CompletaRicerca()
        '    End If


        'Catch ex As Exception
        '    Me.lblErrore.Visible = True
        '    lblErrore.Text = ex.Message
        'End Try
        CompletaRicerca()
    End Sub
    Private Sub CompletaRicerca(Optional ByVal IdAnagrafica As String = "0")
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

            '*****************FINE********************





            If par.IfEmpty(Me.txtCodContratto.Text, "Null") <> "Null" Then
                sValore = txtCodContratto.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(RAPPORTI_UTENZA.COD_CONTRATTO)" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If par.IfEmpty(Me.txtCodUi.Text, "Null") <> "Null" Then
                sValore = txtCodUi.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE)" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            '*************FOGLIO PARTICELLA E SUB**************
            If par.IfEmpty(Me.txtFoglio.Text, "Null") <> "Null" Then
                sValore = txtFoglio.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND IDENTIFICATIVI_CATASTALI.FOGLIO" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If par.IfEmpty(txtParticella.Text, "Null") <> "Null" Then

                sValore = txtParticella.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND IDENTIFICATIVI_CATASTALI.NUMERO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If par.IfEmpty(txtSub.Text, "Null") <> "Null" Then

                sValore = txtSub.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(IDENTIFICATIVI_CATASTALI.SUB)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If
            '*****FINE FOGLIO, PARTICELLA E SUB**************



            '*****SEZIONE RISERVATA ALL'INDIRIZZO************

            If Me.cmbCivico.Text <> "" Then
                sCompara = " = " '
                sStringaSql = sStringaSql & " AND UPPER(INDIRIZZI.DESCRIZIONE)" & sCompara & " '" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.ToString.ToUpper) & "' AND UPPER(INDIRIZZI.CIVICO) = '" & par.PulisciStrSql(Me.cmbCivico.Text.ToUpper) & "' "
            End If

            If Me.cmbInterno.Text <> "-1" And Me.cmbInterno.Text <> "" Then
                sCompara = " = "
                sStringaSql = sStringaSql & " AND UNITA_IMMOBILIARI.INTERNO" & sCompara & " '" & par.PulisciStrSql(Me.cmbInterno.SelectedItem.ToString) & "' "
            End If
            '*****SEZIONE RISERVATA ALL'INDIRIZZO************

            'sStringaSql = "SELECT ANAGRAFICA.ID AS ID_ANAGRAFICA, RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME||' '||RAGIONE_SOCIALE))AS ""INTESTATARIO"",RTRIM(LTRIM(COD_FISCALE ||' ' ||PARTITA_IVA)) AS ""CFIVA"",ANAGRAFICA.DATA_NASCITA, ANAGRAFICA.SESSO, ANAGRAFICA.TELEFONO, RTRIM(LTRIM(ANAGRAFICA.INDIRIZZO_RESIDENZA||','|| ANAGRAFICA.CIVICO_RESIDENZA)) AS RESIDENZA,ANAGRAFICA.COMUNE_RESIDENZA, ANAGRAFICA.PROVINCIA_RESIDENZA " _
            '& "RAPPORTI_UTENZA.COD_CONTRATTO, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
            '& "INDIRIZZI.DESCRIZIONE AS INDIRIZZO,INDIRIZZI.CIVICO, UNITA_IMMOBILIARI.INTERNO,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,INDIRIZZI.CAP, TIPO_LIVELLO_PIANO.DESCRIZIONE AS LIV_PIANO, " _
            '& "TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPO_CONTRATTO, TIPOLOGIA_CONTRATTO_LOCAZIONE.RIF_LEGISLATIVO " _
            '& "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE, " _
            '& "SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI, " _
            '& "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.IDENTIFICATIVI_CATASTALI  WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
            '& "RAPPORTI_UTENZA.ID= SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = TIPOLOGIA_CONTRATTO_LOCAZIONE.COD " _
            '& "AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " & sStringaSql


            'sStringaSql = "SELECT DISTINCT ANAGRAFICA.ID AS ID_ANAGRAFICA,RAPPORTI_UTENZA.COD_CONTRATTO, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""CFIVA"",TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA , ANAGRAFICA.SESSO, ANAGRAFICA.TELEFONO, RTRIM(LTRIM(ANAGRAFICA.INDIRIZZO_RESIDENZA||','|| ANAGRAFICA.CIVICO_RESIDENZA)) AS RESIDENZA,ANAGRAFICA.COMUNE_RESIDENZA, ANAGRAFICA.PROVINCIA_RESIDENZA " _
            '            & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE, " _
            '            & "SISCOM_MI.INTESTATARI_RAPPORTO, SISCOM_MI.ANAGRAFICA, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI, " _
            '            & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.IDENTIFICATIVI_CATASTALI  WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
            '            & "RAPPORTI_UTENZA.ID= INTESTATARI_RAPPORTO.ID_CONTRATTO AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA = ANAGRAFICA.ID AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = TIPOLOGIA_CONTRATTO_LOCAZIONE.COD " _
            '            & "AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND data_fine>= to_char(to_date(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') " & sStringaSql
            sStringaSql = "SELECT DISTINCT ANAGRAFICA.ID AS ID_ANAGRAFICA,SOGGETTI_CONTRATTUALI.ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"", " _
                        & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""CFIVA"",TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA , ANAGRAFICA.SESSO, ANAGRAFICA.TELEFONO," _
                        & "RTRIM(LTRIM(ANAGRAFICA.INDIRIZZO_RESIDENZA||','|| ANAGRAFICA.CIVICO_RESIDENZA)) AS RESIDENZA,ANAGRAFICA.COMUNE_RESIDENZA, ANAGRAFICA.PROVINCIA_RESIDENZA " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE, " _
                        & "SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI, " _
                        & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.IDENTIFICATIVI_CATASTALI  WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
                        & "RAPPORTI_UTENZA.ID= SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = TIPOLOGIA_CONTRATTO_LOCAZIONE.COD " _
                        & "AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) " _
                        & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL  AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' " & sStringaSql
            sStringaSql = sStringaSql & " ORDER BY INTESTATARIO ASC"

            Session.Add("RICUTENTE", sStringaSql)
            Response.Redirect("RisUtenza.aspx")
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


    'Protected Sub btnConfirm_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click
    '    CompletaRicerca(Me.RdbListUtente.SelectedValue.ToString)

    'End Sub
End Class
