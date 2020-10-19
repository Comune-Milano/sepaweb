
Partial Class ASS_RicercaUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim S As String = ""
        Dim B As Boolean = False

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaIndirizzi()

            If Session.Item("ABB_ERP") = "1" Or Session.Item("ABB_392") = "1" Or Session.Item("ABB_431") = "1" Then
                S = " COD='AL' OR COD='B' OR COD='H' OR COD='I' "
                B = True
            End If

            If Session.Item("ABB_UD") = "1" Then
                If B = True Then S = S & " OR "
                S = S & " COD='F' OR COD='ST' OR COD='SEAS' OR COD='AU' OR COD='B' OR COD='S' OR COD='D' OR COD='E' OR COD='O' OR COD='L' OR COD='M' OR COD='N' OR COD='H' OR COD='I' OR COD='RIST' OR COD='SC' OR COD='R' OR COD='U' "
            End If
            If S <> "" Then S = " WHERE " & S

            par.RiempiDListConVuoto(Me, par.OracleConn, "cmbTipo", "select * from siscom_mi.tipologia_unita_immobiliari " & S & " ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            cmbTipo.Text = " "
        End If

    End Sub

    Private Function CaricaIndirizzi()
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Exit Function
        Else
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

        par.cmd.Dispose()
        par.OracleConn.Close()
    End Function

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

                par.cmd.Dispose()
                par.OracleConn.Close()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
        'If cmbIndirizzo.Text <> "" Then
        '    If par.OracleConn.State = Data.ConnectionState.Open Then
        '        Exit Sub
        '    Else
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)
        '    End If

        '    cmbCivico.Items.Clear()

        '    par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
        '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    While myReader1.Read
        '        cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("id"), "-1")))
        '    End While
        '    myReader1.Close()

        '    cmbInterno.Items.Clear()
        '    cmbInterno.Items.Add(New ListItem(" ", "-1"))
        '    If cmbCivico.Text <> "" Then
        '        par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
        '        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '        While myReader3.Read
        '            cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
        '        End While
        '        myReader3.Close()
        '    End If

        '    par.cmd.Dispose()
        '    par.OracleConn.Close()
        'End If
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
                par.cmd.Dispose()
                par.OracleConn.Close()


            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
        'If cmbCivico.Text <> "" Then
        '    If par.OracleConn.State = Data.ConnectionState.Open Then
        '        Exit Sub
        '    Else
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)
        '    End If

        '    cmbInterno.Items.Clear()
        '    cmbInterno.Items.Add(New ListItem(" ", "-1"))
        '    If cmbCivico.Text <> "" Then
        '        par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
        '        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '        While myReader2.Read
        '            cmbInterno.Items.Add((New ListItem(par.IfNull(myReader2("interno"), " "), par.IfNull(myReader2("interno"), "-1"))))
        '        End While
        '        myReader2.Close()
        '    End If
        '    par.cmd.Dispose()
        '    par.OracleConn.Close()


        'End If
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String
        Dim S As String = ""
        Dim B As Boolean = False

        bTrovato = False
        sStringaSql = ""

        If txtUI.Text <> "" Or txtFoglio.Text <> "" Or txtParticella.Text <> "" Or txtSub.Text <> "" Or cmbIndirizzo.Text <> " " Then

            If cmbTipo.Text = "-1" Or cmbTipo.Text = "" Then
                If Session.Item("ABB_ERP") = "1" Or Session.Item("ABB_392") = "1" Or Session.Item("ABB_431") = "1" Then
                    S = " COD_TIPOLOGIA='AL' OR COD_TIPOLOGIA='B' OR COD_TIPOLOGIA='H' OR COD_TIPOLOGIA='I' "
                    B = True
                End If

                If Session.Item("ABB_UD") = "1" Then
                    If B = True Then S = S & " OR "
                    S = S & " COD_TIPOLOGIA='F' OR COD_TIPOLOGIA='ST' OR COD_TIPOLOGIA='SEAS' OR COD_TIPOLOGIA='AU' OR COD_TIPOLOGIA='B' OR COD_TIPOLOGIA='S' OR COD_TIPOLOGIA='D' OR COD_TIPOLOGIA='E' OR COD_TIPOLOGIA='O' OR COD_TIPOLOGIA='L' OR COD_TIPOLOGIA='M' OR COD_TIPOLOGIA='N' OR COD_TIPOLOGIA='H' OR COD_TIPOLOGIA='I' OR COD_TIPOLOGIA='RIST' OR COD_TIPOLOGIA='SC' OR COD_TIPOLOGIA='R' OR COD_TIPOLOGIA='U' "
                End If
                If S <> "" Then S = "(" & S & ") AND "
            End If


            If txtUI.Text <> "" Then
                sValore = txtUI.Text
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
                'sStringaSql = sStringaSql & " EDIFICI.ID_INDIRIZZO_PRINCIPALE" & sCompara & "" & par.PulisciStrSql(sValore) & " "
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

            If cmbTipo.Text <> "-1" And cmbTipo.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbTipo.SelectedItem.Value

                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.COD_TIPOLOGIA='" & par.PulisciStrSql(sValore) & "' "
            End If


            If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

            sStringaSql = "select (SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA," _
                        & "(SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONVENZIONALE," _
                        & "PIANI.DESCRIZIONE AS PIANO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,EDIFICI.DENOMINAZIONE AS ""NOME_EDIFICIO""," _
                        & "unita_immobiliari.*,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB," _
                        & "INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from " _
                        & "siscom_mi.unita_immobiliari,siscom_mi.identificativi_catastali,siscom_mi.edifici,siscom_mi.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI," _
                        & "SISCOM_MI.PIANI where " & S _
                        & "  UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID (+) AND unita_immobiliari.id_piano=piani.id (+) AND " _
                        & "unita_immobiliari.cod_tipo_disponibilita<>'VEND' AND cod_tipo_disponibilita<>'NAGI' and UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL and " _
                        & "EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND " _
                        & "UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID (+) " & sStringaSql & " ORDER BY UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ASC"


            Session.Add("PED_MI", sStringaSql)
            Response.Redirect("RisultatiUI.aspx?T=0")
        Else
            Response.Write("<script>alert('Specificare almeno un valore tra Codice, Foglio, Mappale, Sub o Indirizzo!');</script>")
        End If

    End Sub


End Class
