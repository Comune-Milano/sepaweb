
Partial Class MANUTENZIONI_SelettivaUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
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
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            End If

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader2.Read
                'DrLComplesso.Items.Add(New ListItem(par.IfNull("cod." & myReader2("cod_complesso"), " ") & "- -" & par.IfNull(myReader2("denominazione"), " "), par.IfNull(myReader2("id"), -1)))
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader2("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader2("cod_complesso"), " "), par.IfNull(myReader2("id"), -1)))

            End While
            myReader2.Close()

            cmbTipologia.Items.Add(New ListItem(" ", "-1"))
            par.cmd.CommandText = "SELECT cod, descrizione FROM SISCOM_MI.tipo_unita_comune order by descrizione asc"

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read
                cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), "-1")))
            End While
            myReader2.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            cmbComplesso.Text = "-1"
            CaricaEdifici()

        End If
        TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        TextBoxDescIndEd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

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
        If Me.cmbComplesso.Text <> "-1" Then
            Me.cmbEdificio.Items.Clear()
            Me.CaricaEdificiComp()

        Else
            Me.cmbEdificio.Items.Clear()
            CaricaEdifici()
        End If

    End Sub
    Private Sub CaricaEdificiComp()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim gest As Integer = 3
            If Me.cmbComplesso.Text <> "-1" Then

                Me.cmbEdificio.Items.Clear()
                '****CARICA LISTA EDIFICI
                cmbEdificio.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.EDIFICI  where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"
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

                cmbEdificio.Text = "-1"
                cmbEdificio.Items.Add(New ListItem(" ", -1))
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim bTrovato As Boolean

        Dim Tipologia As String
        bTrovato = False
        sStringaSql = ""

        If Me.cmbComplesso.Items.Count > 0 And Me.cmbEdificio.Items.Count > 0 Then


            If Me.cmbComplesso.SelectedValue <> "-1" Or Me.cmbEdificio.SelectedValue <> "-1" Then
                If Me.cmbTipologia.SelectedValue <> "-1" Then
                    Tipologia = "AND UNITA_COMUNI.COD_TIPOLOGIA = '" & Me.cmbTipologia.SelectedValue & "'"
                End If

                If Me.cmbEdificio.Text <> "-1" Then
                    If Tipologia <> "" Then
                        sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.edifici where unita_comuni.id_edificio =  " & cmbEdificio.SelectedValue & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD " & Tipologia & "  and UNITA_COMUNI.ID_EDIFICIO = EDIFICI.id order by unita_comuni.id asc"
                    Else
                        sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.edifici where unita_comuni.id_edificio =  " & cmbEdificio.SelectedValue & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD and UNITA_COMUNI.ID_EDIFICIO = EDIFICI.id order by unita_comuni.id asc"
                    End If

                ElseIf Me.cmbComplesso.Text <> "-1" Then
                    If Tipologia <> "" Then
                        sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl, TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where unita_comuni.ID_COMPLESSO =  " & cmbComplesso.SelectedValue & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD " & Tipologia & "  and UNITA_COMUNI.id_complesso = complessi_immobiliari.id order by unita_comuni.id asc"
                    Else
                        sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl, TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where unita_comuni.ID_COMPLESSO =  " & cmbComplesso.SelectedValue & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD and UNITA_COMUNI.id_complesso = complessi_immobiliari.id order by unita_comuni.id asc"

                    End If


                End If
            Else
                Response.Write("<SCRIPT>alert('Bisogna selezionare almeno un Edificio o un Complesso');</SCRIPT>")
                Exit Sub

            End If

        Else
            Response.Write("<SCRIPT>alert('Non e\' stato trovato alcun Complesso o Edificio!');</SCRIPT>")
            Exit Sub
        End If


        Session.Add("PED", sStringaSql)
        Response.Redirect("RisultatiUC.aspx?T=1")



        'If cmbProvenienza.SelectedValue = "-1" Then
        '    If cmbComplesso.SelectedValue = "" And cmbEdificio.SelectedValue = "" Then
        '        Response.Write("<script>alert('Selezionare almeno un edificio o un complesso!')</script>")
        '        Exit Sub
        '    End If

        'Else
        '    If cmbComplesso.SelectedValue = "-1" And cmbEdificio.SelectedValue = "" Then
        '        Response.Write("<script>alert('Selezionare almeno un edificio o un complesso!')</script>")
        '        Exit Sub
        '    End If

        'End If

        'If cmbEdificio.SelectedValue <> -1 Then
        '    sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.edifici where unita_comuni.id_edificio =  " & cmbEdificio.SelectedValue & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD and UNITA_COMUNI.ID_EDIFICIO = EDIFICI.id order by unita_comuni.id asc"
        'ElseIf cmbComplesso.SelectedValue <> -1 Then
        '    sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl, TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where unita_comuni.ID_COMPLESSO =  " & cmbComplesso.SelectedValue & " and UNITA_COMUNI.id_complesso = complessi_immobiliari.id order by unita_comuni.id asc"
        'End If
        ''If IfEmpty(cmbGestore.Text, "") <> "" And cmbGestore.Text <> "0" Then
        ''    sValore = cmbGestore.SelectedValue
        ''    If InStr(sValore, "*") Then
        ''        sCompara = " LIKE "
        ''        Call par.ConvertiJolly(sValore)
        ''    Else
        ''        sCompara = " = "
        ''    End If
        ''    bTrovato = True
        ''    If cmbEdificio.SelectedValue <> -1 Then
        ''        sStringaSql = sStringaSql & " substr(Complessi_immobiliari.id,1,1) " & sCompara & " " & par.PulisciStrSql(sValore) & " and EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID "
        ''    Else
        ''        sStringaSql = sStringaSql & " substr(Complessi_immobiliari.id,1,1) " & sCompara & " " & par.PulisciStrSql(sValore) & ""

        ''    End If
        ''End If

        ''If cmbProvenienza.Text <> " " And cmbProvenienza.Text <> "-1" Then
        ''    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

        ''    sValore = cmbProvenienza.SelectedValue
        ''    If InStr(sValore, "*") Then
        ''        sCompara = " = "
        ''        Call par.ConvertiJolly(sValore)
        ''    Else
        ''        sCompara = " = "
        ''    End If
        ''    bTrovato = True

        ''    If cmbEdificio.SelectedValue <> -1 Then
        ''        sStringaSql = sStringaSql & " complessi_immobiliari.cod_tipologia_provenienza" & sCompara & "'" & par.PulisciStrSql(sValore) & "'and unita_comuni.id_EDIFICI = " & cmbEdificio.SelectedValue & " and UNITA_COMUNI.ID_EDIFICIO = EDIFICI.ID"
        ''    Else
        ''        sStringaSql = sStringaSql & " complessi_immobiliari.cod_tipologia_provenienza" & sCompara & "'" & par.PulisciStrSql(sValore) & "' and unita_comuni.id_COMPLESSO = " & cmbComplesso.SelectedValue & " UNITA_COMUNI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID"

        ''    End If
        ''    'sStringaSql = sStringaSql & " complessi_immobiliari.cod_tipologia_provenienza" & sCompara & "'" & par.PulisciStrSql(sValore) & "'muni.id_EDIFICI = " & cmbEdificio.SelectedValue & " and UNITA_COMUNI.ID_EDIFICIO = EDIFICI.ID"

        ''End If
        ' ''If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

        ''If cmbEdificio.SelectedValue = -1 Then
        ''    sStringaSql = "select UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as COMPLESSO from SISCOM_MI.complessi_immobiliari, SISCOM_MI.unita_comuni where  " & sStringaSql & " ORDER BY UNITA_COMUNI.COD_UNITA_COMUNE ASC"
        ''Else
        ''    sStringaSql = "select UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as COMPLESSO from SISCOM_MI.EDIFICI, SISCOM_MI.unita_comuni, SISCOM_MI.Complessi_immobiliari where  " & sStringaSql & " ORDER BY UNITA_COMUNI.COD_UNITA_COMUNE ASC"

        ''End If



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
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        myReader1 = par.cmd.ExecuteReader()
        While myReader1.Read
            cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
        End While
        myReader1.Close()


        'cmbEdificio.Items.Add(New ListItem(" ", -1))
        'If cmbGestore.SelectedValue <> 0 Then
        '    par.cmd.CommandText = "SELECT edifici.id,edifici.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.complessi_immobiliari where substr(edifici.id,1,1)=" & cmbGestore.SelectedValue & " and  complessi_immobiliari.cod_tipologia_provenienza='" & cmbProvenienza.SelectedValue & "'and edifici.id_complesso = complessi_immobiliari.id order by denominazione asc"
        'Else
        '    par.cmd.CommandText = "SELECT edifici.id,edifici.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.complessi_immobiliari where complessi_immobiliari.cod_tipologia_provenienza='" & cmbProvenienza.SelectedValue & "'and edifici.ID_COMPLESSO = complessi_immobiliari.id order by denominazione asc"

        'End If
        'myReader1 = par.cmd.ExecuteReader

        'While myReader1.Read
        '    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
        'End While
        'myReader1.Close()

        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'cmbComplesso.Text = "-1"


    End Sub

    Protected Sub cmbGestore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGestore.SelectedIndexChanged
        cmbComplesso.Items.Clear()
        cmbEdificio.Items.Clear()
        cmbProvenienza.SelectedIndex = 0

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
            Me.ListEdifci.Items.Clear()
            If Session("PED2_ESTERNA") = "1" Then
                'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text & "%'and lotto > 3 order by denominazione asc"

            Else
                par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text & "%'order by denominazione asc"

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
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Me.TextBox1.Text = 2
    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        If Me.ListEdifci.SelectedValue.ToString <> "" Then
            Me.cmbComplesso.SelectedValue = Me.ListEdifci.SelectedValue.ToString
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.TextBox1.Text = 1
            Me.LblNoResult.Visible = False
            Me.CaricaEdificiComp()

        Else
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.LblNoResult.Visible = False
            Me.TextBox1.Text = 1
        End If
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        If par.IfEmpty(Me.TextBoxDescIndEd.Text, "Null") <> "Null" Then
            Me.ListEdifici2.Items.Clear()

            If Session("PED2_ESTERNA") = "1" Then
                If Me.cmbComplesso.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%'and lotto > 3 order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%'and lotto > 3 AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"

                End If
            Else
                If Me.cmbComplesso.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%'order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%' AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & "order by denominazione asc"

                End If

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                ListEdifici2.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
        End If
        If ListEdifici2.Items.Count = 0 Then
            Me.LblNoresult2.Visible = True
        Else
            Me.LblNoresult2.Visible = False
        End If
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Me.TextBox2.Text = 2
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        If Me.ListEdifici2.SelectedValue.ToString <> "" Then
            Me.cmbEdificio.SelectedValue = Me.ListEdifici2.SelectedValue.ToString
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.TextBox2.Text = 1
            Me.LblNoresult2.Visible = False
        Else
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.LblNoresult2.Visible = False
            Me.TextBox2.Text = 1
        End If
    End Sub
End Class
