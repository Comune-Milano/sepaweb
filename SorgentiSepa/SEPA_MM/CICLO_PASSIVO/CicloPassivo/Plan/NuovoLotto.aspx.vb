
Partial Class Contabilita_CicloPassivo_Plan_NuovoLotto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim lstScale As System.Collections.Generic.List(Of Epifani.Scale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        lstScale = CType(HttpContext.Current.Session.Item("LSTSCALE"), System.Collections.Generic.List(Of Epifani.Scale))

        If IsPostBack = False Then
            'lstScale.Clear()
            idPianoF.Value = Request.QueryString("IDP")
            idVoce.Value = Request.QueryString("IDV")
            idServizio.Value = Request.QueryString("IDS")
            idLotto.Value = Request.QueryString("ID")
            lettura.Value = Request.QueryString("L")

            CaricaStato()
            CaricaServizio()
            CaricaDati()

            If idLotto.Value <> "-1" Then
                lblLotto.Text = "Edita Lotto"
                CaricaLotto()
            Else
                lblLotto.Text = "Nuovo Lotto"
            End If

            If lettura.Value = "1" Then
                ImgProcedi.Visible = False
                lblLotto.Text = "Lotto"
                txtdescrizione.enabled = False
                btnEliminaComplesso.visible = False
                cmbTipoLotto.Enabled = False
                cmbTipoImpianto.Enabled = False
            End If

        End If

        Dim CTRL1 As Control
        For Each CTRL1 In Me.form1.Controls
            If TypeOf CTRL1 Is TextBox Then
                DirectCast(CTRL1, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL1 Is DropDownList Then
                DirectCast(CTRL1, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL1 Is CheckBoxList Then
                DirectCast(CTRL1, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            End If
        Next
    End Sub

    Function CaricaLotto()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select * from siscom_mi.lotti where id=" & idLotto.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtDescrizione.Text = par.IfNull(myReader("descrizione"), "")
                cmbTipoLotto.Items.FindByValue(par.IfNull(myReader("TIPO"), "E")).Selected = True
                cmbFiliale.Items.FindByValue(par.IfNull(myReader("id_filiale"), 0)).Selected = True
                cmbTipoImpianto.ClearSelection()
                cmbTipoImpianto.Items.FindByValue(par.IfNull(myReader("COD_TIPO_IMPIANTO"), "-1")).Selected = True
            End If
            myReader.Close()


            'par.cmd.CommandText = "select * from siscom_mi.lotti_servizi where id=" & idlotto.Value
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    txtdescrizione.Text = par.IfNull(myReader("descrizione"), "")
            'End If
            'myReader.Close()
            Dim I As Integer = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI_PATRIMONIO WHERE ID_LOTTO = " & idlotto.Value
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                RiempiPatrimonio(myReader)
                I = I + 1
            Loop
            myReader.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function


    Private Sub RiempiPatrimonio(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Try
            If par.IfNull(myReader1("ID_EDIFICIO"), 0) <> 0 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=" & myReader1("ID_EDIFICIO")
                Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myreader2.Read
                    Me.lstcomplessi.Items.FindByValue(par.IfNull(myreader2("ID"), "")).Selected = True
                Loop
                myreader2.Close()
                AddSelectedComplessi()
                'AddSelectedEdifici()
            End If

            'If par.IfNull(myReader1("ID_COMPLESSO"), 0) <> 0 Then
            '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID=" & myReader1("ID_COMPLESSO")
            '    Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    Do While myreader2.Read
            '        Me.lstcomplessi.Items.FindByValue(par.IfNull(myreader2("ID"), "")).Selected = True
            '    Loop
            '    myreader2.Close()
            '    AddSelectedComplessi()
            'End If


        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Function CaricaDati()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)



            If Session.Item("LIVELLO") = "1" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, OPERATORI WHERE OPERATORI.ID=" & Session.Item("ID_OPERATORE") & " AND TAB_FILIALI.ID=OPERATORI.ID_UFFICIO AND SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID "
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()
            cmbFiliale.Items.Add(New ListItem(" ", -1))
            tipostruttura.Value = ""
            While myReader1.Read
                cmbFiliale.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & "  -  " & par.IfNull(myReader1("DESCRIZIONE"), "") & " " & par.IfNull(myReader1("CIVICO"), "") & " " & par.IfNull(myReader1("LOCALITA"), ""), par.IfNull(myReader1("ID"), -1)))
                If Session.Item("LIVELLO") <> "1" Then
                    cmbFiliale.SelectedValue = par.IfNull(myReader1("ID"), -1)
                    cmbFiliale.Enabled = False

                    Select Case par.IfNull(myReader1("ID_TIPO_ST"), "2")
                        Case "0"
                            tipostruttura.Value = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & par.IfNull(myReader1("ID"), -1) & ") "
                        Case "1"
                            tipostruttura.Value = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & par.IfNull(myReader1("ID"), -1) & ")) "
                        Case "2"
                            tipostruttura.Value = ""
                    End Select

                End If
            End While


            Me.lstcomplessi.Items.Clear()

            par.cmd.CommandText = "select * from "

            If cmbTipoImpianto.SelectedItem.Value = "-1" Then
               
                'par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.ID," _
                '                      & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                '                      & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio  " _
                '                      & "FROM SISCOM_MI.EDIFICI WHERE " _
                '                      & "EDIFICI.ID<>1  " & tipostruttura.Value & " AND    " _
                '                      & "EDIFICI.ID NOT IN  (SELECT id_EDIFICIO FROM siscom_mi.LOTTI_PATRIMONIO,siscom_mi.LOTTI,siscom_mi.LOTTI_SERVIZI WHERE LOTTI.id_esercizio_finanziario=" & idEsercizioF.Value & " AND " _
                '                      & "LOTTI_PATRIMONIO.id_lotto=LOTTI.ID AND LOTTI_SERVIZI.id_lotto=LOTTI.ID  AND LOTTI_SERVIZI.id_servizio=" & idServizio.Value _
                '                      & "AND LOTTI.ID<>" & idLotto.Value & " AND ID_EDIFICIO IS NOT NULL) " _
                '                      & "ORDER BY DENOMINAZIONE ASC"
                par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.ID," _
                                     & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                                     & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio  " _
                                     & "FROM SISCOM_MI.EDIFICI WHERE " _
                                     & "EDIFICI.ID<>1  " & tipostruttura.Value & "     " _
                                     & "" _
                                     & "ORDER BY DENOMINAZIONE ASC"
            Else
                
                par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.ID," _
                                                    & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE," _
                                                    & "(case when condominio=1 then '- Edificio in Condominio' ELSE '' END) as condominio  " _
                                                    & " from SISCOM_MI.EDIFICI " _
                                                    & " WHERE EDIFICI.id<>1 " & tipostruttura.Value & "  " _
                                                    & "  " _
                                                    & " AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.IMPIANTI WHERE COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "') " _
                                                    & "ORDER BY DENOMINAZIONE ASC"

            End If
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader3.Read
                lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader3("DENOMINAZIONE"), " ") & par.IfNull(myReader3("CONDOMINIO"), " "), par.IfNull(myReader3("ID"), -1)))
            End While
            myReader3.Close()


            cmbComplesso.SelectedValue = -1




            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function


    Function CaricaDati1()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select * from siscom_mi.tab_filiali where id=" & cmbFiliale.SelectedItem.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()
            tipostruttura.Value = ""
            While myReader1.Read

                Select Case par.IfNull(myReader1("ID_TIPO_ST"), "2")
                    Case "0"
                        tipostruttura.Value = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & par.IfNull(myReader1("ID"), -1) & " "
                    Case "1"
                        tipostruttura.Value = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & par.IfNull(myReader1("ID"), -1) & ") "
                    Case "2"
                        tipostruttura.Value = ""
                End Select
            End While
            Me.lstcomplessi.Items.Clear()

            par.cmd.CommandText = "select * from "

            par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID," _
                                & "(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE  " _
                                & " from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                & " WHERE complessi_immobiliari.id<>1 " & tipostruttura.Value & " and " _
                                & " complessi_immobiliari.id not in  " _
                                & "(select id_complesso from siscom_mi.lotti_patrimonio,siscom_mi.lotti,siscom_mi.lotti_servizi where " _
                                & "lotti.id_esercizio_finanziario=" & idEsercizioF.Value & " and lotti_patrimonio.id_lotto=lotti.id and lotti_servizi.id_lotto=lotti.id " _
                                & " and lotti_servizi.id_servizio=" & idServizio.Value & " and lotti.id<>" & idLotto.Value & ") ORDER BY DENOMINAZIONE ASC"

            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader3.Read
                lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader3("DENOMINAZIONE"), " "), par.IfNull(myReader3("ID"), -1)))
            End While
            myReader3.Close()


            '        cmbComplesso.SelectedValue = -1




            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function



    Function CaricaServizio()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select * from siscom_mi.TAB_SERVIZI where id=" & idServizio.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = par.IfNull(myReader5("descrizione"), "")
            End If
            myReader5.Close()

            cmbTipoImpianto.Items.Add(New ListItem("TUTTI", "-1"))
            par.cmd.CommandText = "select * from siscom_mi.TIPOLOGIA_IMPIANTI ORDER BY DESCRIZIONE ASC"
            myReader5 = par.cmd.ExecuteReader()
            While myReader5.Read
                cmbTipoImpianto.Items.Add(New ListItem(par.IfNull(myReader5("DESCRIZIONE"), " "), par.IfNull(myReader5("COD"), -1)))
            End While
            myReader5.Close()

            cmbTipoImpianto.ClearSelection()
            cmbTipoImpianto.Items.FindByValue("-1").Selected = True

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select T_ESERCIZIO_FINANZIARIO.id,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
                idEsercizioF.Value = par.IfNull(myReader5("id"), "0")
            End If
            myReader5.Close()

            par.cmd.Dispose()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub BtnConfermacomplesso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConfermacomplesso.Click
        AddSelectedComplessi()
    End Sub



    Private Sub AddSelectedComplessi()
        Dim I As Integer
        Dim str1 As String
        Dim OLTRE1000 As Boolean = False

        Try

            str1 = ""
            Dim kk As Long = 0

            tabcomplessi.Visible = True

            If complessiSelezionati() = True Then

                For I = 0 To Me.lstcomplessi.Items.Count() - 1
                    If Me.lstcomplessi.Items(I).Selected = True Then
                        If Strings.Len(str1) = 0 Then
                            str1 = lstcomplessi.Items(I).Value
                        Else
                            If kk = 999 And Me.lstcomplessi.Items.Count() > 1000 Then
                                str1 = str1 & ") OR EDIFICI.ID IN (-1"
                            End If
                            kk = kk + 1
                            str1 = str1 & "," & lstcomplessi.Items(I).Value

                        End If
                    End If
                Next

            End If

            If Strings.Len(str1) = 0 Then
                str1 = "-1"
            End If

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE " _
                                & "from SISCOM_MI.EDIFICI where SISCOM_MI.EDIFICI.ID in (" & str1 & ")  order by EDIFICI.denominazione asc"


            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            myReader = par.cmd.ExecuteReader()


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "EDIFICI")


            tabcomplessi.DataSource = ds
            tabcomplessi.DataBind()

            ds.Dispose()
            myReader.Close()

            If tabcomplessi.Items.Count <> 0 Then
                btnEliminaComplesso.Visible = True
            End If

            Me.txtcomplessi.Value = "1"
        Catch ex As Exception
            par.cmd.Dispose()

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub tabcomplessi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles tabcomplessi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------       
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtseledifici').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtseledifici').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub

    'Protected Sub tabcomplessi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabcomplessi.SelectedIndexChanged
    '    txtedifici.Value = "2" 'altrimenti si chiude la lista
    '    txtcomplessi.Value = "" 'altrimenti si vede la lista
    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        Me.lstedifici.Items.Clear()
    '        If Me.cmbComplesso.SelectedValue <> "-1" Then
    '            par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID_COMPLESSO=" & cmbComplesso.SelectedValue.ToString & " order by DENOMINAZIONE asc"
    '        Else
    '            par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID order by DENOMINAZIONE asc"
    '        End If
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        While myReader1.Read
    '            lstedifici.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
    '        End While
    '        If lstedifici.Items.Count = 0 Then
    '            Me.LblNoResulted.Visible = True
    '        Else
    '            Me.LblNoResulted.Visible = False
    '        End If
    '        myReader1.Close()

    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        'Me.txtedifici.Text = 2
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Sub

   
    Protected Sub btnEliminaComplesso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaComplesso.Click
        If sicuro.Value = "1" Then


            Dim str1 As String
            If txtIdComponente.Value <> "" Then
                Dim i As Integer

                'Pulisco elementi selezionati nella checkboxlist
                For i = 0 To Me.lstcomplessi.Items.Count() - 1
                    If lstcomplessi.Items(i).Value = txtIdComponente.Value Then
                        lstcomplessi.Items(i).Selected = False
                    End If
                Next

                str1 = ""

                If complessiSelezionati() = True Then

                    For i = 0 To Me.lstcomplessi.Items.Count() - 1
                        If Me.lstcomplessi.Items(i).Selected = True Then

                            If Strings.Len(str1) = 0 Then
                                str1 = lstcomplessi.Items(i).Value
                            Else

                                If i = 1000 And Me.lstcomplessi.Items.Count() > 1000 Then
                                    str1 = str1 & ") OR EDIFICI.ID IN (-1"
                                End If
                                str1 = str1 & "," & lstcomplessi.Items(i).Value
                            End If
                        End If
                    Next

                End If

                If Strings.Len(str1) = 0 Then
                    str1 = "-1"
                End If

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                'par.cmd.CommandText = "select SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                '                    & "from SISCOM_MI.COMPLESSI_IMMOBILIARI where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (" & str1 & ")  order by complessi_immobiliari.denominazione asc"


                par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE " _
                               & "from SISCOM_MI.EDIFICI where SISCOM_MI.EDIFICI.ID in (" & str1 & ")  order by EDIFICI.denominazione asc"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                myReader = par.cmd.ExecuteReader()


                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim ds As New Data.DataSet()

                da.Fill(ds, "EDIFICI")


                tabcomplessi.DataSource = ds
                tabcomplessi.DataBind()

                ds.Dispose()
                myReader.Close()

                If tabcomplessi.Items.Count = 0 Then
                    btnEliminaComplesso.Visible = False
                    tabcomplessi.Visible = False
                End If

            End If
        End If
    End Sub

    'Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
    '    txtedifici.Value = "2" 'altrimenti si chiude la lista
    '    txtcomplessi.Value = "" 'altrimenti si vede la lista
    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        Me.lstedifici.Items.Clear()
    '        If Me.cmbComplesso.SelectedValue <> "-1" Then
    '            par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID_COMPLESSO=" & cmbComplesso.SelectedValue.ToString & " order by DENOMINAZIONE asc"
    '        Else
    '            par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID order by DENOMINAZIONE asc"
    '        End If
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        While myReader1.Read
    '            lstedifici.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
    '        End While
    '        If lstedifici.Items.Count = 0 Then
    '            Me.LblNoResulted.Visible = True
    '        Else
    '            Me.LblNoResulted.Visible = False
    '        End If
    '        myReader1.Close()

    '        'par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio where id_lotto=" & idLotto.Value & " and id_edificio in (select id from siscom_mi.edifici where id_complesso=" & cmbComplesso.SelectedItem.Value & ")"
    '        'myReader1 = par.cmd.ExecuteReader()
    '        'While myReader1.Read
    '        '    lstedifici.Items.FindByValue(par.IfNull(myReader1("ID_edificio"), -1)).Selected = True
    '        'End While
    '        'myReader1.Close()

    '        Dim j As Long = 0

    '        For j = 0 To lstScale.Count - 1
    '            If lstedifici.Items.Contains(New ListItem(lstScale(j).DENOMINAZIONE_EDIFICIO, lstScale(j).ID)) = True Then
    '                lstedifici.Items.FindByText(lstScale(j).DENOMINAZIONE_EDIFICIO).Selected = True
    '            End If
    '        Next


    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        'Me.txtedifici.Text = 2
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Sub

    'Public Function edificiSelezionati() As Boolean
    '    Try
    '        Dim I As Integer
    '        edificiSelezionati = False
    '        For I = 0 To Me.lstedifici.Items.Count() - 1
    '            If Me.lstedifici.Items(I).Selected = True Then
    '                edificiSelezionati = True
    '                Exit For
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Function

    

    Public Function complessiSelezionati() As Boolean
        Try
            Dim I As Integer
            complessiSelezionati = False
            For I = 0 To Me.lstcomplessi.Items.Count() - 1
                If Me.lstcomplessi.Items(I).Selected = True Then
                    complessiSelezionati = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Function Inserimento()
        Try
            Dim vId As Long = 0
            Dim Buono As Boolean = True
            Dim ind As String = "("
            Dim id_filiale As String = "-1"

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

           

            id_filiale = cmbFiliale.SelectedItem.Value

            Dim ID_ESERCIZIO_F As Long = 0

            par.cmd.CommandText = "select * FROM siscom_mi.PF_MAIN WHERE ID=" & idPianoF.Value
            Dim myReader22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader22.Read Then
                ID_ESERCIZIO_F = myReader22("ID_ESERCIZIO_FINANZIARIO")
            End If
            myReader22.Close()


            par.cmd.CommandText = "select * from siscom_mi.lotti where id_esercizio_finanziario=" & ID_ESERCIZIO_F & " and upper(descrizione)='" & par.PulisciStrSql(UCase(txtDescrizione.Text)) & "'"
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader11.HasRows = True Then
                Buono = False
            End If
            myReader11.Close()

            If tabcomplessi.Items.Count <= 0 Then
                Buono = False
            End If

            If Buono = True Then


                'par.cmd.CommandText = "insert into siscom_mi.lotti_servizi (id,id_servizio,id_filiale,descrizione,id_appalto,id_voce_pf) values (siscom_mi.seq_lotti_servizi.nextval," & idServizio.Value & "," & Session.Item("ID_CAF") & ",'" & par.PulisciStrSql(UCase(txtDescrizione.Text)) & "',NULL," & idVoce.Value & ")"

                par.cmd.CommandText = "insert into siscom_mi.lotti (id,ID_FILIALE,TIPO,DESCRIZIONE,ID_ESERCIZIO_FINANZIARIO,COD_TIPO_IMPIANTO) values (siscom_mi.seq_lotti.nextval," & id_filiale & ",'E','" & par.PulisciStrSql(UCase(txtDescrizione.Text)) & "'," & ID_ESERCIZIO_F & ",'" & cmbTipoImpianto.SelectedItem.Value & "')"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "select siscom_mi.seq_lotti.currval from dual"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    vId = myReader1(0)
                    idLotto.Value = vId
                End If
                myReader1.Close()


                par.cmd.CommandText = "insert into siscom_mi.lotti_servizi (id_LOTTO,id_servizio) values (" & idLotto.Value & "," & idServizio.Value & ")"
                par.cmd.ExecuteNonQuery()


                ''INSERIMENTO LOTTI PATRIMONIO
                'Dim i As Integer
                'Dim j As Integer
                'For i = 0 To lstcomplessi.Items.Count - 1
                '    If lstcomplessi.Items(i).Selected = True And Str(lstcomplessi.Items(i).Value) > -1 Then
                '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_EDIFICIO)" _
                '     & "VALUES(" & vId & "," & Me.lstcomplessi.Items(i).Value & ")"
                '        par.cmd.ExecuteNonQuery()
                '        par.cmd.CommandText = ""
                '        par.cmd.Parameters.Clear()
                '    End If
                'Next


                'INSERIMENTO LOTTI PATRIMONIO
                Dim i As Integer

                For i = 0 To tabcomplessi.Items.Count - 1

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_EDIFICIO)" _
                                        & "VALUES(" & vId & "," & Me.tabcomplessi.Items(i).Cells(0).Text & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()

                Next


                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                        & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                       & "'F83','" & par.PulisciStrSql(txtDescrizione.Text) & "'," & Session.Item("ID_STRUTTURA") & ")"
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")

            Else
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'Response.Write("<script>alert('Operazione NON Effettuata! Il problema potrebbe essere:\nI complessi devono appartenere ad una stessa filiale;\nEsiste gia un lotto con questa descrizione nell\'esercizio finanziario!\nNessun complesso inserito nella lista!;');</script>")
                Response.Write("<script>alert('Operazione NON Effettuata! Il problema potrebbe essere:\nEsiste gia un lotto con questa descrizione nell\'esercizio finanziario!\nNessun Edificio inserito nella lista!;');</script>")
            End If


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Function Aggiorna()
        Try

            If tabcomplessi.Items.Count > 0 Then
                Dim vId As Long = CLng(idLotto.Value)

                par.OracleConn.Open()
                par.SettaCommand(par)

                'cancello
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.LOTTI_PATRIMONIO WHERE ID_LOTTO=" & vId
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.LOTTI SET COD_TIPO_IMPIANTO='" & cmbTipoImpianto.SelectedItem.Value & "',tipo='E', DESCRIZIONE='" & par.PulisciStrSql(UCase(txtDescrizione.Text)) & "' WHERE ID=" & vId
                par.cmd.ExecuteNonQuery()

                'INSERIMENTO LOTTI PATRIMONIO
                Dim i As Integer

                For i = 0 To lstcomplessi.Items.Count - 1
                    If lstcomplessi.Items(i).Selected = True And Str(lstcomplessi.Items(i).Value) > -1 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_EDIFICIO)" _
                     & "VALUES(" & vId & "," & Me.lstcomplessi.Items(i).Value & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                        par.cmd.Parameters.Clear()
                    End If
                Next

                par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where (id_lotto,id_EDIFICIO) not in (select id_lotto,id_EDIFICIO from siscom_mi.lotti_patrimonio)"
                par.cmd.ExecuteNonQuery()

                ''INSERIMENTO EDIFICI
                'For j = 0 To lstScale.Count - 1

                '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_COMPLESSO, ID_EDIFICIO)" _
                '    & "VALUES(" & vId & ",NULL," & lstScale(j).ID & ")"
                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""
                '    par.cmd.Parameters.Clear()

                'Next

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F02','LOTTO " & par.PulisciStrSql(txtDescrizione.Text) & "'," & Session.Item("ID_STRUTTURA") & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")
            Else
                Response.Write("<script>alert('Nessun complesso inserito!');</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If txtDescrizione.Text <> "" Then
            If idLotto.Value = "-1" Then
                Inserimento()
            Else
                Aggiorna()
            End If
        Else
            Response.Write("<script>alert('La descrizione del lotto è obbligatoria!');</script>")
        End If

    End Sub

    Protected Sub tabcomplessi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabcomplessi.SelectedIndexChanged

    End Sub

    Protected Sub SelezionaTutto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SelezionaTutto.Click
        Dim I As Integer
        For I = 0 To lstcomplessi.Items.Count - 1
            lstcomplessi.Items(I).Selected = True
        Next
        ' txtcomplessi.Value = "2"
        AddSelectedComplessi()
    End Sub

    Protected Sub cmbFiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        tabcomplessi.DataBind()
        CaricaDati1()
    End Sub

    Protected Sub cmbTipoImpianto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoImpianto.SelectedIndexChanged
        tabcomplessi.DataBind()
        CaricaDati()
    End Sub

    Protected Sub ImgInseriscitutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgInseriscitutti.Click
        AggiungiTutto()
    End Sub

    Private Function AggiungiTutto()


        Dim OLTRE1000 As Boolean = False

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)




            'Select Case tipostruttura.Value
            '    Case "0"
            '        tipostruttura.Value = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & cmbFiliale.SelectedItem.Value & ") "
            '    Case "1"
            '        tipostruttura.Value = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbFiliale.SelectedItem.Value & ")) "
            '    Case "2"
            '        tipostruttura.Value = ""
            'End Select



            par.cmd.CommandText = "select * from "

            If cmbTipoImpianto.SelectedItem.Value = "-1" Then
               
                par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.ID," _
                                      & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                                      & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio  " _
                                      & "FROM SISCOM_MI.EDIFICI WHERE " _
                                      & "EDIFICI.ID<>1  " & tipostruttura.Value  _
                                      & "ORDER BY DENOMINAZIONE ASC"
            Else
              
                par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.ID," _
                                                    & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE," _
                                                    & "(case when condominio=1 then '- Edificio in Condominio' ELSE '' END) as condominio  " _
                                                    & " from SISCOM_MI.EDIFICI " _
                                                    & " WHERE EDIFICI.id<>1 " & tipostruttura.Value & " and " _
                                                    & " EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.IMPIANTI WHERE COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "') " _
                                                    & "ORDER BY DENOMINAZIONE ASC"

            End If



            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            myReader = par.cmd.ExecuteReader()


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "EDIFICI")

            tabcomplessi.Visible = True
            tabcomplessi.DataSource = ds
            tabcomplessi.DataBind()

            ds.Dispose()
            myReader.Close()

            If tabcomplessi.Items.Count <> 0 Then
                btnEliminaComplesso.Visible = True
            Else
                Response.Write("<script>alert('Nessun edificio selezionabile');</script>")
            End If

            Me.txtcomplessi.Value = "1"


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Function
End Class
