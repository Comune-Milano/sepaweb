
Partial Class PED_RicercaUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            par.OracleConn.Open()
            par.SettaCommand(par)
            cmbTipologia.Items.Add(New ListItem(" ", "-1"))
            par.cmd.CommandText = "SELECT cod, descrizione FROM SISCOM_MI.tipologia_unita_immobiliari order by descrizione asc"

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader2.Read
                cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), "-1")))
            End While
            myReader2.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaIndirizzi()
        End If
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String

            bTrovato = False
            sStringaSql = ""

            If txtUI.Text <> "" Then
                sValore = txtUI.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtFoglio.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtFoglio.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " IDENTIFICATIVI_CATASTALI.FOGLIO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtParticella.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtParticella.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " IDENTIFICATIVI_CATASTALI.NUMERO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtSub.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtSub.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " IDENTIFICATIVI_CATASTALI.SUB" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
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

            If cmbTipologia.Text <> "-1" And cmbTipologia.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbTipologia.SelectedItem.Value

                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.COD_TIPOLOGIA='" & par.PulisciStrSql(sValore) & "' "
            End If



            If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

            sStringaSql = "SELECT DISTINCT ROWNUM,unita_immobiliari.id as id_UI,(select zona from sepa.zona_aler where cod = id_zona) as zona,  EDIFICI.DENOMINAZIONE, SISCOM_MI.UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE ,UNITA_IMMOBILIARI.INTERNO,  (SCALE_EDIFICI.DESCRIZIONE) AS SCALA ,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,UNITA_IMMOBILIARI.S_NETTA,NVL(TO_CHAR(identificativi_catastali.rendita),'NO') AS RENDITA,(INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO) AS INDIRIZZO,INDIRIZZI.LOCALITA AS COMUNE,(select TAB_FILIALI.NOME from siscom_mi.tab_filiali,siscom_mi.filiali_ui where filiali_ui.id_filiale = tab_filiali.id and unita_immobiliari.id = filiali_ui.id_ui and FILIALI_UI.fine_validita >to_char(sysdate,'yyyymmdd') ) as FILIALE,TIPO_DISPONIBILITA.DESCRIZIONE AS TIPO_DISP,PROGRAMMAZIONE_INTERVENTI.DESCRIZIONE AS PRG_INTERVENTI, " _
                        & "(select descrizione from siscom_mi.DESTINAZIONI_USO_UI where DESTINAZIONI_USO_UI.ID = UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO) as dest_uso,INDIRIZZI.CAP,(select descrizione from siscom_mi.tab_zona_osmi where id = edifici.id_osmi) as ZOSMI FROM SISCOM_MI.TIPO_LIVELLO_PIANO, " _
                        & "SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.unita_immobiliari,SISCOM_MI.identificativi_catastali,SISCOM_MI.edifici,SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_DISPONIBILITA,siscom_mi.PROGRAMMAZIONE_INTERVENTI where PROGRAMMAZIONE_INTERVENTI.ID (+)=UNITA_IMMOBILIARI.ID_PRG_EVENTI AND TIPO_DISPONIBILITA.COD (+)=UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA AND TIPOLOGIA_UNITA_IMMOBILIARI.COD= UNITA_IMMOBILIARI.COD_TIPOLOGIA AND " _
                        & "EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND " _
                        & "UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID (+) AND EDIFICI.ID <> 1 " & sStringaSql & " AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and SISCOM_MI.TIPO_LIVELLO_PIANO.COD(+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO " _
                        & "ORDER BY DENOMINAZIONE ASC, SCALE_EDIFICI.DESCRIZIONE ASC, INTERNO ASC"

            '*****PEPPE MODIFY 26/10/2010
            'and unita_immobiliari.cod_tipo_disponibilita<>'VEND'

            Session.Add("PED", sStringaSql)
            Response.Redirect("RisultatiUI.aspx?T=0")
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
        'par.OracleConn.Open()
        'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        'Label3.Text = "0"
        'Do While myReader.Read()
        '    Label3.Text = CInt(Label3.Text) + 1
        'Loop
        'Label3.Text = Label3.Text
        'cmd.Dispose()
        'myReader.Close()
        'par.OracleConn.Close()
        'BindGrid()
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub
End Class
