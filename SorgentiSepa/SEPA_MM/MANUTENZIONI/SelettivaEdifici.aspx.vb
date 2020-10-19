
Partial Class MANUTENZIONI_SelettivaEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            cmbGestore.Items.Add(New ListItem(" ", 0))
            cmbGestore.Items.Add(New ListItem("GEFI", 1))
            cmbGestore.Items.Add(New ListItem("PIRELLI", 2))
            cmbGestore.Items.Add(New ListItem("ROMEO", 3))
            cmbGestore.Text = " "

            cmbProvenienza.Items.Add(New ListItem(" ", "-1"))
            cmbProvenienza.Items.Add(New ListItem("ALER", "ALER"))
            cmbProvenienza.Items.Add(New ListItem("DEMANIO", "DEMA"))
            cmbProvenienza.Text = " "
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbComplesso.Items.Add(New ListItem(" ", -1))
            If Session("PED2_ESTERNA") = "1" Then
                'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                'cmbComplesso.Items.Add(New ListItem(par.IfNull("cod." & myReader1("cod_complesso"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()

            '*********************COMBO TIPOLOGIA UTENZA**********************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UTENZA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbTipoUtenze.Items.Add(New ListItem(" ", -1))

            While myReader.Read
                cmbTipoUtenze.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("COD"), -1)))
            End While
            myReader.Close()

            '*********************COMBO FORNITORE**********************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA_FORNITORI"
            myReader = par.cmd.ExecuteReader()
            cmbFornitore.Items.Add(New ListItem(" ", -1))

            While myReader.Read
                cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
            End While
            myReader.Close()



            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaEdifici()
            If Request.QueryString("CHIAMA") <> "UTENZE" Then
                Me.LBLFORN.Visible = False
                Me.LBLCONT.Visible = False
                Me.LBLCONTR.Visible = False
                Me.LBLTIPOL.Visible = False

                Me.cmbFornitore.Visible = False
                Me.cmbTipoUtenze.Visible = False
                Me.txtContratto.Visible = False
                Me.txtContatore.Visible = False
            End If
        End If
        TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub
    Private Sub CaricaEdifici()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim gest As Integer = 0
            Me.cmbEdificio.Items.Clear()

            cmbEdificio.Items.Add(New ListItem(" ", -1))

            If Session("PED2_ESTERNA") = "1" Then
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID order by denominazione asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                'cmbEdificio.Items.Add(New ListItem("cod." & par.IfNull(myReader1("COD_EDIFICIO"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

            End While

            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        If Me.cmbComplesso.SelectedValue <> "-1" Then
            FiltraEdifici()
        Else
            CaricaEdifici()
        End If



        'If par.OracleConn.State = Data.ConnectionState.Open Then
        '    Exit Sub
        'Else
        '    par.OracleConn.Open()
        '    par.SettaCommand(par)
        'End If


        'cmbEdificio.Items.Clear()

        'cmbEdificio.Items.Add(New ListItem(" ", -1))

        'par.cmd.CommandText = "SELECT distinct(id),denominazione FROM SISCOM_MI.edifici where id_complesso=" & cmbComplesso.SelectedValue & " order by denominazione asc"
        'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'While myReader1.Read
        '    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
        'End While
        'myReader1.Close()
        'par.OracleConn.Close()
        'cmbEdificio.Text = "-1"

    End Sub
    Private Sub FiltraEdifici()
        Try
            If Me.cmbComplesso.SelectedValue <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))

                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    'cmbEdificio.Items.Add(New ListItem("cod." & par.IfNull(myReader1("COD_EDIFICIO"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

                End While

                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""

        'If IfEmpty(cmbGestore.Text, "") <> "" And cmbGestore.Text <> "0" Then
        '    sValore = cmbGestore.SelectedValue
        '    If InStr(sValore, "*") Then
        '        sCompara = " LIKE "
        '        Call par.ConvertiJolly(sValore)
        '    Else
        '        sCompara = " = "
        '    End If
        '    bTrovato = True
        '    sStringaSql = sStringaSql & " substr(Complessi_immobiliari.id,1,1) " & sCompara & " " & par.PulisciStrSql(sValore) & " and EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID "
        'End If


        'If cmbProvenienza.Text <> " " And cmbProvenienza.Text <> "-1" Then
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

        '    sValore = cmbProvenienza.SelectedValue
        '    If InStr(sValore, "*") Then
        '        sCompara = " = "
        '        Call par.ConvertiJolly(sValore)
        '    Else
        '        sCompara = " = "
        '    End If
        '    bTrovato = True
        '    sStringaSql = sStringaSql & " complessi_immobiliari.cod_tipologia_provenienza" & sCompara & "'" & par.PulisciStrSql(sValore) & "' and EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID"
        'End If

        'If cmbComplesso.Text <> "" Then
        If Request.QueryString("CHIAMA") = "UTENZE" Then
            '********************CHIAMATO DA MENU' UTENZE**************************
            If par.IfEmpty(cmbComplesso.SelectedValue, "-1") <> "-1" Then

                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbProvenienza.SelectedValue
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "edifici.id_complesso = " & Me.cmbComplesso.SelectedValue.ToString

            End If

            If par.IfEmpty(cmbEdificio.SelectedValue, "-1") <> "-1" Then

                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbProvenienza.SelectedValue
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "edifici.id = " & Me.cmbEdificio.SelectedValue.ToString

            End If

            If Me.cmbFornitore.SelectedValue <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                bTrovato = True
                sStringaSql = sStringaSql & " UTENZE.ID_FORNITORE = " & Me.cmbFornitore.SelectedValue
            End If

            If Me.cmbTipoUtenze.SelectedValue <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " UTENZE.COD_TIPOLOGIA = '" & Me.cmbFornitore.SelectedValue & "'"
            End If
            If par.IfEmpty(Me.txtContratto.Text, "NULL") <> "NULL" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " UTENZE.CONTRATTO = '" & Me.cmbFornitore.SelectedValue & "'"

            End If
            If par.IfEmpty(Me.txtContatore.Text, "NULL") <> "NULL" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " UTENZE.CONTATORE = '" & Me.cmbFornitore.SelectedValue & "'"

            End If
            If bTrovato = True Then
                sStringaSql = "SELECT DISTINCT siscom_mi.UTENZE.ID,SISCOM_MI.EDIFICI.ID AS ID_IMMOBILE,SISCOM_MI.TIPOLOGIA_UTENZA.DESCRIZIONE AS TIPOLOGIA, SISCOM_MI.ANAGRAFICA_FORNITORI.DESCRIZIONE AS DESC_FORNITORE, siscom_mi.UTENZE.CONTATORE,siscom_mi.UTENZE.CONTRATTO,siscom_mi.UTENZE.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UTENZA, siscom_mi.TABELLE_MILLESIMALI,SISCOM_MI.ANAGRAFICA_FORNITORI, siscom_mi.UTENZE_TABELLE_MILLESIMALI,siscom_mi.UTENZE, SISCOM_MI.EDIFICI WHERE siscom_mi.UTENZE_TABELLE_MILLESIMALI.id_tabella_millesimale=siscom_mi.TABELLE_MILLESIMALI.ID AND siscom_mi.UTENZE_TABELLE_MILLESIMALI.ID_UTENZA = siscom_mi.UTENZE.ID AND EDIFICI.ID = TABELLE_MILLESIMALI.ID_EDIFICIO AND ANAGRAFICA_FORNITORI.ID=UTENZE.ID_FORNITORE AND TIPOLOGIA_UTENZA.COD = UTENZE.COD_TIPOLOGIA AND " & sStringaSql & " "
            Else
                sStringaSql = "SELECT DISTINCT siscom_mi.UTENZE.ID,SISCOM_MI.EDIFICI.ID AS ID_IMMOBILE,SISCOM_MI.TIPOLOGIA_UTENZA.DESCRIZIONE AS TIPOLOGIA, SISCOM_MI.ANAGRAFICA_FORNITORI.DESCRIZIONE AS DESC_FORNITORE, siscom_mi.UTENZE.CONTATORE,siscom_mi.UTENZE.CONTRATTO,siscom_mi.UTENZE.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UTENZA, siscom_mi.TABELLE_MILLESIMALI,SISCOM_MI.ANAGRAFICA_FORNITORI, siscom_mi.UTENZE_TABELLE_MILLESIMALI,siscom_mi.UTENZE, SISCOM_MI.EDIFICI WHERE siscom_mi.UTENZE_TABELLE_MILLESIMALI.id_tabella_millesimale=siscom_mi.TABELLE_MILLESIMALI.ID AND siscom_mi.UTENZE_TABELLE_MILLESIMALI.ID_UTENZA = siscom_mi.UTENZE.ID AND EDIFICI.ID = TABELLE_MILLESIMALI.ID_EDIFICIO AND ANAGRAFICA_FORNITORI.ID=UTENZE.ID_FORNITORE AND TIPOLOGIA_UTENZA.COD = UTENZE.COD_TIPOLOGIA "
            End If

            Session.Add("PED", sStringaSql)
            Response.Redirect("RisultaiUtenzeEdifici.aspx?CHIAMA=EDIF")

        Else

            '********************CHIAMATO DA ALTRI MENU**************************
            If par.IfEmpty(cmbComplesso.SelectedValue, "-1") <> "-1" Then

                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbProvenienza.SelectedValue
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "edifici.id_complesso = " & Me.cmbComplesso.SelectedValue.ToString

            End If

            If par.IfEmpty(cmbEdificio.SelectedValue, "-1") <> "-1" Then

                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbProvenienza.SelectedValue
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "edifici.id = " & Me.cmbEdificio.SelectedValue.ToString

            End If

            'If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql
            If bTrovato = True Then
                sStringaSql = "select EDIFICI.id,EDIFICI.COD_EDIFICIO,edifici.COD_EDIFICIO_GIMI, EDIFICI.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as COMPLESSO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari, SISCOM_MI.EDIFICI where  " & sStringaSql & " AND EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and EDIFICI.ID_INDIRIZZO_PRINCIPALE=indirizzi.id ORDER BY EDIFICI.COD_EDIFICIO ASC"
            Else
                sStringaSql = "select EDIFICI.id,EDIFICI.COD_EDIFICIO,edifici.COD_EDIFICIO_GIMI, EDIFICI.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as COMPLESSO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari, SISCOM_MI.EDIFICI where EDIFICI.ID_INDIRIZZO_PRINCIPALE=indirizzi.id ORDER BY EDIFICI.COD_EDIFICIO ASC"

            End If


            Session.Add("PED", sStringaSql)
            Response.Redirect("RisultatiEdifici.aspx?T=1")
        End If

    End Sub
    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function

    Protected Sub cmbProvenienza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvenienza.SelectedIndexChanged
        If par.OracleConn.State = Data.ConnectionState.Closed Then

            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        'Select Case cmbGestore.SelectedValue
        '    Case 1
        '        Gestore()
        '    Case 2

        '    Case 3
        'End Select

        cmbComplesso.Items.Clear()

        cmbComplesso.Items.Add(New ListItem(" ", -1))
        If cmbGestore.SelectedValue <> 0 Then
            par.cmd.CommandText = "SELECT id,denominazione FROM SISCOM_MI.complessi_immobiliari where substr(id,1,1)=" & cmbGestore.SelectedValue & " and  cod_tipologia_provenienza='" & cmbProvenienza.SelectedValue & "' order by denominazione asc"
        Else
            par.cmd.CommandText = "SELECT id,denominazione FROM SISCOM_MI.complessi_immobiliari where cod_tipologia_provenienza='" & cmbProvenienza.SelectedValue & "' order by denominazione asc"

        End If
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader1.Read
            cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
        End While
        myReader1.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        cmbComplesso.Text = "-1"

    End Sub

    Protected Sub cmbGestore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGestore.SelectedIndexChanged
        cmbComplesso.Items.Clear()
        cmbEdificio.Items.Clear()
        cmbProvenienza.SelectedIndex = 0
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
                Me.ListEdifci.Items.Clear()

                If Session("PED2_ESTERNA") = "1" Then
                    If Me.cmbComplesso.SelectedValue = "-1" Then
                        par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 order by denominazione asc"
                    Else
                        par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"

                    End If
                Else
                    If Me.cmbComplesso.SelectedValue = "-1" Then
                        par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'order by denominazione asc"
                    Else
                        par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & "order by denominazione asc"

                    End If

                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
            End If
            If ListEdifci.Items.Count = 0 Then
                Me.LblNoResult.Visible = True
            Else
                Me.LblNoResult.Visible = False
            End If
            par.OracleConn.Close()
            Me.TextBox1.Text = 2
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        Try
            If Me.ListEdifci.SelectedValue.ToString <> "" Then
                Me.cmbEdificio.SelectedValue = Me.ListEdifci.SelectedValue.ToString
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.TextBox1.Text = 1
                Me.LblNoResult.Visible = False
            Else
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.LblNoResult.Visible = False
                Me.TextBox1.Text = 1
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
End Class
