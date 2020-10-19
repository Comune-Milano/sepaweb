
Partial Class MANUTENZIONI_RicercaLotti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            'Me.txtdatainizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'Me.txtdatafine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            '*** FORM GENERALE

            Dim CTRL As Control
            For Each CTRL In Me.Form.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                End If
            Next

            SettaggioCampi()
        End If
    End Sub

    Private Sub SettaggioCampi()

        'CARICO DRL

        Try
            'LISTA FILIALI
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim TipoStruttura As String = ""
            Dim TipoStruttura1 As String = ""

            'ESERCIZIO FINANZIARIO
            'Dim Idcorrente As Long = par.RicavaEsercizioCorrente

            Me.cmbservizi.Items.Clear()
            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, " _
                                      & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' ||" _
                                      & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS DESCRIZIONE " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                               & " where ID in (select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.LOTTI) " _
                               & " order by ID desc"

            '& " where id<=" & Idcorrente + 1 & " order by ID desc"
            par.caricaComboTelerik(par.cmd.CommandText, cmbesercizio, "ID", "DESCRIZIONE", True)

            '******************************************************


            'FILIALI IN BASE AD OPERATORE COLLEGATO
            If Session.Item("LIVELLO") = "1" Then
                par.cmd.CommandText = "SELECT TAB_FILIALI.ID, " _
                                    & "TAB_FILIALI.NOME || ' - ' || INDIRIZZI.DESCRIZIONE || ' ' || CIVICO || ' ' || LOCALITA AS DESCRIZIONE " _
                                   & " where SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID " _
                                   & "   and SISCOM_MI.TAB_FILIALI.ID in (select ID_FILIALE  from SISCOM_MI.LOTTI)" _
                                   & " order by NOME asc"
            Else
                par.cmd.CommandText = "SELECT TAB_FILIALI.ID, " _
                                    & "TAB_FILIALI.NOME || ' - ' || INDIRIZZI.DESCRIZIONE || ' ' || CIVICO || ' ' || LOCALITA AS DESCRIZIONE " _
                                    & "FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, OPERATORI " _
                                    & "WHERE     OPERATORI.ID =" & Session.Item("ID_OPERATORE") _
                                    & "AND TAB_FILIALI.ID = OPERATORI.ID_UFFICIO " _
                                    & "AND SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID " _
                                    & "AND SISCOM_MI.TAB_FILIALI.ID IN (SELECT ID_FILIALE " _
                                    & "FROM SISCOM_MI.LOTTI) " _
                                    & "ORDER BY TAB_FILIALI.NOME ASC "
            End If
            'par.caricaComboTelerik(par.cmd.CommandText, cmbfiliale, "ID", "DESCRIZIONE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            'myReader1 = par.cmd.ExecuteReader()
            'If Session.Item("LIVELLO") = "1" Then
            '    Me.cmbfiliale.Items.Add(New ListItem(" ", -1))
            'End If

            'While myReader1.Read
            '    Me.cmbfiliale.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & "  -  " & par.IfNull(myReader1("DESCRIZIONE"), "") & " " & par.IfNull(myReader1("CIVICO"), "") & " " & par.IfNull(myReader1("LOCALITA"), ""), par.IfNull(myReader1("ID"), -1)))
            '    'TipoStruttura = par.IfNull(myReader1("ID_TIPO_ST"), "")
            'End While

            If Session.Item("LIVELLO") = "1" Then
                par.caricaComboTelerik(par.cmd.CommandText, cmbfiliale, "ID", "DESCRIZIONE", True)
                Me.cmbfiliale.SelectedValue = -1
            Else
                par.caricaComboTelerik(par.cmd.CommandText, cmbfiliale, "ID", "DESCRIZIONE", False)
                '   Me.cmbfiliale.Enabled = False
            End If
            'myReader1.Close()


            'COMPLESSI IN BASE A FILIALE
            'Select Case TipoStruttura
            '    Case "0"
            '        TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
            '    Case "1"
            '        TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
            '    Case "2"
            '        TipoStruttura1 = ""
            '    Case Else
            '        TipoStruttura1 = ""
            'End Select

            Me.cmbcomplesso.Items.Clear()
            'Me.cmbcomplesso.Items.Add(New ListItem(" ", -1))


            'If Session.Item("LIVELLO") = "1" Then
            If RBL1.SelectedIndex = 0 Then
                par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where EDIFICI.ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.LOTTI_PATRIMONIO) " _
                                   & " order by DENOMINAZIONE ASC"

            Else
                par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID,(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
                                   & " from SISCOM_MI.IMPIANTI " _
                                   & " where IMPIANTI.ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.LOTTI_PATRIMONIO) " _
                                   & " order by DENOMINAZIONE ASC"
            End If

            'par.cmd.CommandText = "select DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID," _
            '                         & " (SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
            '                    & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                    & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
            '                    & "  and  SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select ID_COMPLESSO from SISCOM_MI.LOTTI_PATRIMONIO ) " _
            '                    & " order by DENOMINAZIONE ASC"
            'Else
            'par.cmd.CommandText = "select DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID," _
            '                         & " (SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
            '                   & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                   & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " & TipoStruttura1 _
            '                   & "  and  SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select ID_COMPLESSO from SISCOM_MI.LOTTI_PATRIMONIO ) " _
            '                   & " order by DENOMINAZIONE ASC"

            ' End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()
            ' If Session.Item("LIVELLO") = "1" Then

            'End If

            par.caricaComboTelerik(par.cmd.CommandText, cmbcomplesso, "ID", "DENOMINAZIONE", True)

            If Session.Item("LIVELLO") = "1" Then
                Me.cmbcomplesso.SelectedValue = -1
            End If
            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub
   

    Protected Sub cmbfiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfiliale.SelectedIndexChanged
        Try
            If cmbfiliale.SelectedValue <> "-1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim TipoStruttura1 As String = ""
                Dim TipoStruttura As String = ""

                cmbservizi.Items.Clear()
                par.cmd.CommandText = "select  SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                                    & " where SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO " _
                                    & "   AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE " _
                                    & "   AND SISCOM_MI.TAB_FILIALI.ID = " & Me.cmbfiliale.SelectedValue.ToString _
                                    & " ORDER BY SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"
                par.caricaComboTelerik(par.cmd.CommandText, cmbservizi, "ID", "DESCRIZIONE", True)

                'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                'myReader2 = par.cmd.ExecuteReader()
                'Me.cmbservizi.Items.Add(New ListItem(" ", -1))
                'While myReader2.Read()
                '    Me.cmbservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                '    'TipoStruttura = par.IfNull(myReader2("ID_TIPO_ST"), "0")
                'End While
                'Me.cmbservizi.SelectedValue = -1
                'myReader2.Close()


                'Select Case TipoStruttura
                '    Case "0"    'FILIALE AMMINISTRATIVA
                '        'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
                '        TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
                '    Case "1"    'FILIALE TECNICA
                '        'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
                '        TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "

                '    Case "2"    'UFFICIO CENTRALE
                '        TipoStruttura1 = ""
                'End Select



                'Me.cmbcomplesso.Items.Clear()
                'Me.cmbcomplesso.Items.Add(New ListItem(" ", -1))

                If RBL1.SelectedValue = "E" Then
                    par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE " _
                                       & " from SISCOM_MI.EDIFICI " _
                                       & " where EDIFICI.ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.LOTTI_PATRIMONIO " _
                                                            & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ")) " _
                                       & " order by DENOMINAZIONE ASC"

                Else
                    par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID,(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
                                       & " from SISCOM_MI.IMPIANTI " _
                                       & " where IMPIANTI.ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.LOTTI_PATRIMONIO " _
                                                            & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ")) " _
                                       & " order by DENOMINAZIONE ASC"
                End If
                par.caricaComboTelerik(par.cmd.CommandText, cmbcomplesso, "ID", "DENOMINAZIONE", True)
                'myReader2 = par.cmd.ExecuteReader()

                'While myReader2.Read
                '    Me.cmbcomplesso.Items.Add(New ListItem(par.IfNull(myReader2("DENOMINAZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                'End While
                'Me.cmbcomplesso.SelectedValue = -1


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub RBL1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBL1.SelectedIndexChanged

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            'Me.cmbcomplesso.Items.Clear()
            'Me.cmbcomplesso.Items.Add(New ListItem(" ", -1))

            If cmbfiliale.SelectedValue <> "-1" Then
                If RBL1.SelectedValue = "E" Then
                    par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE " _
                                       & " from SISCOM_MI.EDIFICI " _
                                       & " where EDIFICI.ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.LOTTI_PATRIMONIO " _
                                                            & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ")) " _
                                       & " order by DENOMINAZIONE ASC"

                Else
                    par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID,(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE " _
                                       & " from SISCOM_MI.IMPIANTI " _
                                       & " where IMPIANTI.ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.LOTTI_PATRIMONIO " _
                                                            & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ")) " _
                                       & " order by DENOMINAZIONE ASC"
                End If
            Else

                If RBL1.SelectedValue = "E" Then
                    par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE " _
                                       & " from SISCOM_MI.EDIFICI " _
                                       & " where EDIFICI.ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.LOTTI_PATRIMONIO )" _
                                       & " order by DENOMINAZIONE ASC"

                Else
                    par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID,(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE " _
                                       & " from SISCOM_MI.IMPIANTI " _
                                       & " where IMPIANTI.ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.LOTTI_PATRIMONIO )" _
                                       & " order by DENOMINAZIONE ASC"
                End If
            End If

            par.caricaComboTelerik(par.cmd.CommandText, cmbcomplesso, "ID", "DENOMINAZIONE", True)
            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

            'myReader2 = par.cmd.ExecuteReader()

            'While myReader2.Read
            '    Me.cmbcomplesso.Items.Add(New ListItem(par.IfNull(myReader2("DENOMINAZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
            'End While
            'Me.cmbcomplesso.SelectedValue = -1

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        ' Dim sStringaSql As String
        Try

            If Me.cmbfiliale.SelectedIndex <> "-1" Or Me.cmbfiliale.Enabled = True Then

                lblErrore.Visible = False

                Response.Write("<script>location.replace('RisultatiLotti.aspx?FI=" & par.PulisciStrSql(cmbfiliale.SelectedValue) _
                                                                          & "&EF=" & cmbesercizio.SelectedValue _
                                                                          & "&CO=" & cmbcomplesso.SelectedValue _
                                                                          & "&SE=" & par.PulisciStrSql(cmbservizi.SelectedValue) _
                                                                          & "&T=" & RBL1.SelectedValue _
                                                                          & "&TIPO=RICERCA_LOTTI" _
                                                                          & "');</script>")
            Else
                Response.Write("<script>alert('Nessun lotto caricato per la struttura competente!');</script>")
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub


End Class
