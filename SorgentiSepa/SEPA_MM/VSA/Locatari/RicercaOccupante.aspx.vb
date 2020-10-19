
Partial Class PED_RicercaOccupante
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaIndirizzi()
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

            If cmbCivico.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbCivico.Text
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " EDIFICI.ID_INDIRIZZO_PRINCIPALE in ( SELECT ID FROM siscom_mi.INDIRIZZI WHERE INDIRIZZI.descrizione = '" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "' AND INDIRIZZI.CIVICO = '" & par.PulisciStrSql(Me.cmbCivico.SelectedItem.Text) & "'  ) "
            End If

            If cmbInterno.Text <> "-1" And cmbInterno.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbInterno.Text
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.INTERNO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtCodContr.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtCodContr.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

            sStringaSql = "select CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"",unita_immobiliari.*,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.id as IDCONTR from " _
                        & "SISCOM_MI.ANAGRAFICA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.unita_immobiliari,SISCOM_MI.identificativi_catastali,SISCOM_MI.edifici,SISCOM_MI.INDIRIZZI,SISCOM_MI.RAPPORTI_UTENZA where COD_TIPOLOGIA_CONTR_LOC = 'ERP' AND " _
                        & "unita_immobiliari.cod_tipo_disponibilita<>'VEND' and unita_immobiliari.id_indirizzo=INDIRIZZI.ID (+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND " _
                        & "UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID (+) AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " & sStringaSql & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' ORDER BY INTESTATARIO ASC"


            Session.Add("PEDOCC", sStringaSql)
            Response.Redirect("RisultatiOccupante.aspx?T=0")
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub CaricaIndirizzi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbIndirizzo.Items.Add(" ")

            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI where id<>1) order by descrizione asc"
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
        End Try
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmbCivico_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbCivico.SelectedIndexChanged
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
End Class
