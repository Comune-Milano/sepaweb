
Partial Class MANUTENZIONI_SelettivaEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Response.Redirect("RisultaiUtenzeEdifici.aspx?CHIAMA=COMP&ID=" & Me.cmbComplesso.SelectedValue.ToString & "&TIPOLOGIA=" & Me.cmbTipoUtenze.SelectedValue & "&FORNITORE=" & Me.cmbFornitore.SelectedValue & "&CONT=" & Me.txtContatore.Text.ToUpper & "&CONTR=" & Me.txtContratto.Text.ToUpper)

            'Dim bTrovato As Boolean
            'Dim sValore As String
            'Dim sCompara As String

            'bTrovato = False
            'sStringaSql = ""
            'If Request.QueryString("CHIAMA") = "UTENZE" Then

            'Else


            '    If IfEmpty(cmbGestore.Text, "") <> "" And cmbGestore.Text <> "0" Then
            '        sValore = cmbGestore.SelectedValue
            '        If InStr(sValore, "*") Then
            '            sCompara = " LIKE "
            '            Call par.ConvertiJolly(sValore)
            '        Else
            '            sCompara = " = "
            '        End If
            '        bTrovato = True
            '        sStringaSql = sStringaSql & " substr(Complessi_immobiliari.id,1,1) " & sCompara & " " & par.PulisciStrSql(sValore) & " "
            '    End If


            '    If cmbProvenienza.Text <> " " And cmbProvenienza.Text <> "-1" Then
            '        If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            '        sValore = cmbProvenienza.SelectedValue
            '        If InStr(sValore, "*") Then
            '            sCompara = " = "
            '            Call par.ConvertiJolly(sValore)
            '        Else
            '            sCompara = " = "
            '        End If
            '        bTrovato = True
            '        sStringaSql = sStringaSql & " complessi_immobiliari.cod_tipologia_provenienza" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            '    End If

            '    'If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql
            '    If bTrovato = True Then
            '        sStringaSql = "select complessi_immobiliari.id, COMPLESSI_IMMOBILIARI.COD_COMPLESSO_GIMI, complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari where  " & sStringaSql & " and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id ORDER BY complessi_immobiliari.COD_complesso ASC"
            '    Else
            '        sStringaSql = "select complessi_immobiliari.id, COMPLESSI_IMMOBILIARI.COD_COMPLESSO_GIMI, complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari where complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id ORDER BY complessi_immobiliari.COD_complesso ASC"

            '    End If


            '    Session.Add("PED", sStringaSql)
            '    Response.Redirect("RisultatiSelettivaComplessi.aspx?T=1")
            'End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If


        If Not IsPostBack Then
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            cmbGestore.Items.Add(New ListItem(" ", 0))
            cmbGestore.Items.Add(New ListItem("GEFI", 1))
            cmbGestore.Items.Add(New ListItem("PIRELLI", 2))
            cmbGestore.Items.Add(New ListItem("ROMEO", 3))
            cmbGestore.Text = " "

            cmbProvenienza.Items.Add(New ListItem(" ", "-1"))
            cmbProvenienza.Items.Add(New ListItem("ALER", "ALER"))
            cmbProvenienza.Items.Add(New ListItem("DEMANIO", "DEMA"))
            cmbProvenienza.Text = " "
            If Session("PED2_ESTERNA") = "1" Then
                'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If Request.QueryString("CHIAMA") = "UTENZE" Then
                cmbComplesso.Items.Add(New ListItem(" ", -1))
            End If

            While myReader1.Read
                'cmbComplesso.Items.Add(New ListItem("cod." & par.IfNull(myReader1("cod_complesso"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            If Request.QueryString("CHIAMA") = "UTENZE" Then
                cmbComplesso.SelectedValue = "-1"
            End If


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
            cmbComplesso.Text = "-1"

            If Request.QueryString("CHIAMA") <> "UTENZE" Then
                Me.LBLFORN.Visible = False
                Me.LBLCONT.Visible = False
                Me.LBLCONTR.Visible = False
                Me.LBLTIPOL.Visible = False

                Me.cmbFornitore.Visible = False
                Me.cmbTipoUtenze.Visible = False
                Me.txtContratto.Visible = False
                Me.txtContatore.Visible = False
            Else
                '*******BOTTONE CERCA PERCHE' QUESTO TIPO PORTA A DEI RISULTATI E NON DIRETTAMENTE AI DATI ASSOCIATI!
                Me.btnCerca.Visible = True
                Me.BtnVisualizza.Visible = False
                Me.btnCerca.Enabled = True

            End If

        End If
        TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub

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

   
    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function

 
    Protected Sub BtnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnVisualizza.Click
        If Request.QueryString("CHIAMA") = "UTENZE" Then
        Else
            Response.Redirect("ConsistenzaEdifici.aspx?ID=" & Me.cmbComplesso.SelectedValue.ToString & "&TIPO=COMP")
            'Response.Redirect("InserimentoComplessi.aspx?C=RicercaComplessi&ID=" & Me.cmbComplesso.SelectedValue.ToString)
        End If
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
                    'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 order by denominazione asc"

                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'order by denominazione asc"

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

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        Try
            If Me.ListEdifci.SelectedValue.ToString <> "" Then
                Me.cmbComplesso.SelectedValue = Me.ListEdifci.SelectedValue.ToString
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
