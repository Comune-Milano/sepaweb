Partial Class MANUTENZIONI_NuovoLottoOLD
    Inherits PageSetIdMode
    Dim par As New CM.Global
    'EDIFICI Dim lstScale As System.Collections.Generic.List(Of CM.Scale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        'EDIFICI lstScale = CType(HttpContext.Current.Session.Item("LSTSCALE"), System.Collections.Generic.List(Of CM.Scale))

        Try
            If Not IsPostBack Then

                vId = 0
                vId = Session.Item("ID") 'ricavo id LOTTI aggiunto alla sessione nella pagina dei risultati

                'EDIFICI   lstScale.Clear()

                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                Me.txtConnessione.Text = CStr(lIdConnessione)
                Me.txtIdlotti.Text = "-1"

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    'HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

                SettaggioCampi()


                If vId <> 0 Then
                    lbltitolo.Text = "Modifica Lotti"
                    cmbfiliale.Enabled = False
                    cmbesercizio.Enabled = False
                    Apriricerca()
                End If




                '*** FORM GENERALE

                Dim CTRL1 As Control
                For Each CTRL1 In Me.form1.Controls
                    If TypeOf CTRL1 Is TextBox Then
                        DirectCast(CTRL1, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        DirectCast(CTRL1, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL1 Is DropDownList Then
                        DirectCast(CTRL1, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL1 Is CheckBoxList Then
                        DirectCast(CTRL1, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

            End If




        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Private Sub SettaggioCampi()

        'CARICO DRL
        Dim gest As Integer
        gest = 0

        Try
            'LISTA FILIALI
            If Session.Item("LIVELLO") = "1" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID order by nome asc"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, OPERATORI WHERE OPERATORI.ID=" & Session.Item("ID_OPERATORE") & " AND TAB_FILIALI.ID=OPERATORI.ID_UFFICIO AND SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID order by TAB_FILIALI.nome asc"
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()
            cmbfiliale.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbfiliale.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & "  -  " & par.IfNull(myReader1("DESCRIZIONE"), "") & " " & par.IfNull(myReader1("CIVICO"), "") & " " & par.IfNull(myReader1("LOCALITA"), ""), par.IfNull(myReader1("ID"), -1)))

            End While
            myReader1.Close()

            If cmbfiliale.SelectedValue <> "-1" Then 'nel caso in cui lo abbia già selezionato per un operatore
                par.cmd.CommandText = "SELECT  SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " ORDER BY SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read()
                    lstservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                End While
                myReader2.Close()
            End If

            '******PEPPE MODIFY 21102010 NON è POSSIBILE INSERIRE UN NUOVO LOTTO QUANDO L'ESERCIZIO FINANZIARIO è NUOVO
            'ESERCIZIO FINANZIARIO
            Dim Idcorrente As Long = par.RicavaEsercizioCorrente
            If vId = 0 Then
                par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE ,PF_MAIN.ID_STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PF_MAIN WHERE (T_ESERCIZIO_FINANZIARIO.ID=" & Idcorrente & " OR T_ESERCIZIO_FINANZIARIO.ID=" & Idcorrente + 1 & " OR T_ESERCIZIO_FINANZIARIO.ID=" & Idcorrente - 1 & ") AND T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO(+) "
                myReader1 = par.cmd.ExecuteReader()

                cmbesercizio.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    If par.IfNull(myReader1("id_stato"), -1) <> 5 Then
                        cmbesercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE"), " "), par.IfNull(myReader1("ID"), -1)))
                    End If
                End While
                cmbesercizio.SelectedValue = -1
                myReader1.Close()


            Else
                par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.ID_STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO , SISCOM_MI.PF_MAIN WHERE T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO(+)"
                myReader1 = par.cmd.ExecuteReader()

                cmbesercizio.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbesercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
                cmbesercizio.SelectedValue = -1
                myReader1.Close()

            End If

            'LISTA COMPLESSI IMMOBILIARI 
            ' '' '' ''par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI  where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 order by DENOMINAZIONE asc"
            ' '' '' ''myReader1 = par.cmd.ExecuteReader()
            ' '' '' ''cmbComplesso.Items.Add(New ListItem(" ", -1))
            ' '' '' ''While myReader1.Read
            ' '' '' ''    cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            ' '' '' ''End While
            ' '' '' ''cmbComplesso.SelectedValue = -1
            ' '' '' ''myReader1.Close()

            '***** NON SERVE ADESSO EDIFICI*********
            '' '' ''par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID order by DENOMINAZIONE asc"
            '' '' ''myReader1 = par.cmd.ExecuteReader()
            '' '' ''While myReader1.Read
            '' '' ''    lstedifici.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            '' '' ''End While
            '' '' ''myReader1.Close()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub
    Private Sub Apriricerca()

        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader


        Try
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            If vId <> 0 Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiLotti(myReader1) 'riempio campi di LOTTI

                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI_SERVIZI WHERE ID_LOTTO = " & vId & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiServizi(myReader1) 'riempio campi di LOTTI_SERVIZI
                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI_PATRIMONIO WHERE ID_LOTTO = " & vId & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiPatrimonio(myReader1) 'riempio campi di LOTTI_PATRIMONIO
                End While
                myReader1.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")
            End If



        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                ' par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Dettagli lotti visualizzati da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI WHERE ID = " & vId
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiLotti(myReader1)
                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI_SERVIZI WHERE ID_LOTTO = " & vId
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiServizi(myReader1)
                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI_PATRIMONIO WHERE ID_LOTTO = " & vId
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiPatrimonio(myReader1)
                End While
                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            Else
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub
    Private Sub RiempiLotti(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Try
            Me.txtdescrizione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
            Me.cmbfiliale.ClearSelection()
            Me.cmbfiliale.Items.FindByValue(par.IfNull(myReader1("ID_FILIALE"), 0)).Selected = True
            Me.cmbtipo.ClearSelection()
            Me.cmbtipo.Items.FindByValue(par.IfNull(myReader1("TIPO"), "")).Selected = True
            Me.cmbesercizio.ClearSelection()
            Me.cmbesercizio.Items.FindByValue(par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), "")).Selected = True

            'ricarico la lista dei servizi in base alla filiale
            If cmbfiliale.SelectedValue <> "-1" Then
                Me.lstservizi.Items.Clear()
                par.cmd.CommandText = "SELECT  TAB_FILIALI.ID_TIPO_ST,SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " ORDER BY SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read()
                    lstservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                    tipostruttura.Value = par.IfNull(myReader2("ID_TIPO_ST"), "0")
                End While
                myReader2.Close()
                If lstservizi.Items.Count = 0 Then
                    Me.lblnoservizi.Visible = True
                Else
                    Me.lblnoservizi.Visible = False
                End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub RiempiServizi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Try

            If par.IfNull(myReader1("ID_SERVIZIO"), 0) <> 0 Then 'seleziono i servizi che sono stati salvati nei lotti
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_SERVIZI WHERE SISCOM_MI.TAB_SERVIZI.ID=" & myReader1("ID_SERVIZIO")
                Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myreader2.Read Then
                    Me.lstservizi.Items.FindByText(par.IfNull(myreader2("DESCRIZIONE"), "")).Selected = True
                End If
                myreader2.Close()
            End If

            If cmbfiliale.SelectedValue <> "-1" And lstservizi.SelectedValue <> "-1" Then  'carico tutti i complessi per filiali e servizi non assegnati oltre a quelli assegnati per questo lotto
                Dim s As Integer
                Dim servizi As String
                servizi = ""
                For s = 0 To lstservizi.Items.Count - 1
                    If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then

                        If servizi = "" Then
                            servizi = lstservizi.Items(s).Value
                        Else
                            servizi = servizi & "," & lstservizi.Items(s).Value
                        End If
                    End If
                Next

                Dim TipoStruttura1 As String = ""



                Select Case tipostruttura.Value
                    Case "0"
                        TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
                    Case "1"
                        TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
                    Case "2"
                        TipoStruttura1 = ""
                End Select


                Me.lstcomplessi.Items.Clear()
                par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                             & "  from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                             & " where  SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " & TipoStruttura1 & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID not in " _
                                             & "     (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI_SERVIZI, SISCOM_MI.LOTTI" _
                                             & "            WHERE SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO AND " _
                                             & "      SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID AND " _
                                             & "     SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID AND " _
                                             & "     LOTTI_SERVIZI.ID_SERVIZIO IN (" & servizi & ") AND LOTTI.ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue & " ) " _
                                             & " OR SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in " _
                                             & " (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO from SISCOM_MI.LOTTI_PATRIMONIO WHERE SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=" & vId & ") " _
                                             & " ORDER BY DENOMINAZIONE ASC"



                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader3.Read
                    lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader3("DENOMINAZIONE"), " "), par.IfNull(myReader3("ID"), -1)))
                End While
                myReader3.Close()


                If lstcomplessi.Items.Count = 0 Then
                    Me.LblNoResult.Visible = True
                Else
                    Me.LblNoResult.Visible = False
                End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub RiempiPatrimonio(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Try
            'EDIFICI '' '' ''If par.IfNull(myReader1("ID_EDIFICIO"), 0) <> 0 Then
            ' '' '' ''    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EDIFICI WHERE SISCOM_MI.EDIFICI.ID=" & myReader1("ID_EDIFICIO")
            ' '' '' ''    Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            ' '' '' ''    While myreader2.Read
            ' '' '' ''        Me.lstedifici.Items.FindByValue(par.IfNull(myreader2("ID"), "")).Selected = True
            ' '' '' ''        Me.cmbComplesso.ClearSelection()
            ' '' '' ''        Me.cmbComplesso.Items.FindByValue(par.IfNull(myreader2("ID_COMPLESSO"), "")).Selected = True ' mostro a quale complesso appartengono gli edifici
            ' '' '' ''    End While
            ' '' '' ''    myreader2.Close()
            ' '' '' ''    AddSelectedEdifici()
            ' '' '' ''End If

            If par.IfNull(myReader1("ID_COMPLESSO"), 0) <> 0 Then

                Me.lstcomplessi.Items.FindByValue(myReader1("ID_COMPLESSO")).Selected = True

                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID=" & myReader1("ID_COMPLESSO")
                'Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myreader2.Read Then
                '    ' lstcomplessi.Items.Add(New ListItem(par.IfNull(myreader2("DENOMINAZIONE"), " "), par.IfNull(myreader2("ID"), -1)))
                '    Me.lstcomplessi.Items.FindByValue(par.IfNull(myreader2("ID"), "")).Selected = True
                'End If
                'myreader2.Close()
                AddSelectedComplessi()
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try
            Me.ImgProcedi.Visible = False

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next

            btnEliminaEdificio.Visible = False
            ' BtnConfermacomplesso.Enabled = False
            BtnConfermaedificio.Enabled = False

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click

        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vId = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If
    End Sub

    Public Function ControlloCampi() As Boolean

        ControlloCampi = True
        lblErrore.Visible = False

        If cmbfiliale.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare una filiale!');</script>")
            ControlloCampi = False
            cmbfiliale.Focus()
            Exit Function
        End If

        If serviziSelezionati() = False Then
            Response.Write("<script>alert('Selezionare almeno un servizio!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If txtdescrizione.Text = "" Then
            Response.Write("<script>alert('Inserire una descrizione!');</script>")
            ControlloCampi = False
            txtdescrizione.Focus()
            Exit Function
        End If

        If cmbesercizio.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare un esercizio finanziario !');</script>")
            ControlloCampi = False
            cmbfiliale.Focus()
            Exit Function
        End If

        If complessiSelezionati() = False Then
            Response.Write("<script>alert('Selezionare almeno un complesso!');</script>")
            ControlloCampi = False
            Exit Function
        End If

    End Function
    Private Sub Salva()

        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT SISCOM_MI.LOTTI.DESCRIZIONE FROM SISCOM_MI.LOTTI WHERE SISCOM_MI.LOTTI.DESCRIZIONE LIKE '" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 50)) & "' AND SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myRec.Read Then
                Response.Write("<script>alert('Attenzione...Descrizione già presente nei nostri archivi.');</script>")
                myRec.Close()
                Exit Sub
            End If
            myRec.Close()

            par.cmd.CommandText = ""



            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            'Ricavo vId lotti
            par.cmd.CommandText = " select SISCOM_MI.SEQ_LOTTI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vId = myReader1(0)
            End If

            myReader1.Close()
            par.cmd.CommandText = ""

            Me.txtIdlotti.Text = vId

            Dim c As Integer
            'controllo se il complesso è già stato assegnato allo stesso servizio con stesso esercizio in un altro lotto
            For c = 0 To lstservizi.Items.Count - 1
                If lstservizi.Items(c).Selected = True And Str(lstservizi.Items(c).Value) > -1 Then
                    If controlla_servizio(lstservizi.Items(c).Value) = False Then
                        lstservizi.Items(c).Selected = False
                        Exit Sub
                    End If
                End If
            Next

            'INSERIMENTO IN LOTTI
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI(ID, ID_FILIALE, TIPO, DESCRIZIONE, ID_ESERCIZIO_FINANZIARIO)" _
            & "VALUES(" & vId & "," & par.PulisciStrSql(Me.cmbfiliale.SelectedValue) & ",'" & cmbtipo.SelectedValue & "','" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 50)) & "'," & cmbesercizio.SelectedValue & ")"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            ' INSERIMENTO IN LOTTI_SERVIZI
            Dim s As Integer
            For s = 0 To lstservizi.Items.Count - 1
                If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_SERVIZI(ID_LOTTO, ID_SERVIZIO)" _
                    & "VALUES(" & vId & "," & Me.lstservizi.Items(s).Value & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If
            Next

            'INSERIMENTO LOTTI PATRIMONIO
            Dim i As Integer
            'Dim j As Integer
            For i = 0 To lstcomplessi.Items.Count - 1
                If lstcomplessi.Items(i).Selected = True And Str(lstcomplessi.Items(i).Value) > -1 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_COMPLESSO, ID_EDIFICIO)" _
                 & "VALUES(" & vId & "," & Me.lstcomplessi.Items(i).Value & ",NULL)"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If
            Next

            'For j = 0 To lstScale.Count - 1

            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_COMPLESSO, ID_EDIFICIO)" _
            '    & "VALUES(" & vId & ",NULL," & lstScale(j).ID & ")"
            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""
            '    par.cmd.Parameters.Clear()

            'Next NON SERVE ADESSO EDIFICI

            ' COMMIT
            par.myTrans.Commit()



            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN APRI RICERCA)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Lotti inseriti correttamente!');</SCRIPT>")


            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub Update()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            'LOTTI
            par.cmd.CommandText = "UPDATE SISCOM_MI.LOTTI SET " _
                   & "ID_FILIALE=" & par.PulisciStrSql(Me.cmbfiliale.SelectedValue) & "," _
                   & "TIPO='" & par.PulisciStrSql(Me.cmbtipo.SelectedValue) & "'," _
                   & "DESCRIZIONE='" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 50)) & "'," _
                   & "ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue & "" _
                   & "WHERE ID='" & vId & "'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()

            ' LOTTI_SERVIZI
            par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_SERVIZI where ID_LOTTO = " & vId
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            Dim s As Integer
            For s = 0 To lstservizi.Items.Count - 1
                If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_SERVIZI(ID_LOTTO, ID_SERVIZIO)" _
                    & "VALUES(" & vId & "," & Me.lstservizi.Items(s).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If
            Next

            'LOTTI_PATRIMONIO
            par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_PATRIMONIO where ID_LOTTO = " & vId
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            Dim i As Integer
            'Dim j As Integer
            For i = 0 To lstcomplessi.Items.Count - 1
                If lstcomplessi.Items(i).Selected = True And Str(lstcomplessi.Items(i).Value) > -1 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_COMPLESSO, ID_EDIFICIO)" _
                 & "VALUES(" & vId & "," & Me.lstcomplessi.Items(i).Value & ",NULL)"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If
            Next


            'For j = 0 To lstScale.Count - 1

            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_COMPLESSO, ID_EDIFICIO)" _
            '    & "VALUES(" & vId & ",NULL," & lstScale(j).ID & ")"
            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""
            '    par.cmd.Parameters.Clear()

            'Next  NON SERVE ADESSO EDIFICI

            par.myTrans.Commit() 'COMMIT


            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Lotti aggiornati correttamente!');</SCRIPT>")
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property
    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property

    Protected Sub BtnConfermacomplesso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConfermaComplesso.Click
        AddSelectedComplessi()
    End Sub
    Private Sub AddSelectedComplessi()
        Dim I As Integer
        Dim str1 As String

        Try

            str1 = ""

            txtseledifici.Visible = True
            tabcomplessi.Visible = True

            If complessiSelezionati() = True Then

                For I = 0 To Me.lstcomplessi.Items.Count() - 1
                    If Me.lstcomplessi.Items(I).Selected = True Then

                        If Strings.Len(str1) = 0 Then
                            str1 = lstcomplessi.Items(I).Value
                        Else
                            str1 = str1 & "," & lstcomplessi.Items(I).Value
                        End If
                    End If
                Next

            End If

            If Strings.Len(str1) = 0 Then
                str1 = "-1"
            End If

            Dim TipoStruttura1 As String = ""

            Select Case tipostruttura.Value
                Case "0"
                    TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
                Case "1"
                    TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
                Case "2"
                    TipoStruttura1 = ""
            End Select


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                & "from SISCOM_MI.COMPLESSI_IMMOBILIARI where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (" & str1 & ") " & TipoStruttura1 & " order by DENOMINAZIONE"



            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            myReader = par.cmd.ExecuteReader()


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "COMPLESSI_IMMOBILIARI")


            tabcomplessi.DataSource = ds
            tabcomplessi.DataBind()

            ds.Dispose()
            myReader.Close()

            If tabcomplessi.Items.Count <> 0 Then
                btnEliminaComplesso.Visible = True
            End If

            Me.txtcomplessi.Text = "1"
        Catch ex As Exception
            par.cmd.Dispose()

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub

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
    Public Function serviziSelezionati() As Boolean
        Try
            Dim s As Integer
            serviziSelezionati = False
            For s = 0 To Me.lstservizi.Items.Count() - 1
                If Me.lstservizi.Items(s).Selected = True Then
                    serviziSelezionati = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Protected Sub cmbfiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfiliale.SelectedIndexChanged
        txtcomplessi.Text = "" 'altrimenti si vede la lista
        txtedifici.Text = "" 'altrimenti si vede la lista
        'lblcomplesso.Text = "" 'altrimenti potrebbe visualizzarsi in tabella un complesso non corrispondente
        Try
            If cmbfiliale.SelectedValue <> "-1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'Svuoto gli oggetti contenenti i dati associati alla precedente filiale per poi ricaricarli 
                lstservizi.Items.Clear()
                lstcomplessi.Items.Clear()
                tabcomplessi.DataBind()
                par.cmd.CommandText = "SELECT  TAB_FILIALI.ID_TIPO_ST,SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " ORDER BY SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read()
                    lstservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                    tipostruttura.value = par.IfNull(myReader2("ID_TIPO_ST"), "0")
                End While
                myReader2.Close()
            End If

            Me.txtcomplessi.Text = ""
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        Me.lstcomplessi.Items.Clear()
    '        par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER, SISCOM_MI.TAB_SERVIZI " _
    '        & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " ORDER BY DENOMINAZIONE ASC"
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        While myReader1.Read
    '            lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
    '        End While
    '        If lstcomplessi.Items.Count = 0 Then
    '            Me.LblNoResult.Visible = True
    '        Else
    '            Me.LblNoResult.Visible = False
    '        End If
    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Me.txtcomplessi.Text = 2
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub lstservizi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstservizi.SelectedIndexChanged
    '    txtcomplessi.Text = "" 'altrimenti si vede la lista
    '    txtedifici.Text = "" 'altrimenti si vede la lista
    '    'lblcomplesso.Text = "" 'altrimenti potrebbe visualizzarsi in tabella un complesso non corrispondente
    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        If cmbfiliale.SelectedValue <> "-1" Then
    '            Me.lstcomplessi.Items.Clear()
    '            par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER, SISCOM_MI.TAB_SERVIZI " _
    '            & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " ORDER BY DENOMINAZIONE ASC"
    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReader1.Read
    '                lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
    '            End While
    '            myReader1.Close()
    '            If lstcomplessi.Items.Count = 0 Then
    '                Me.LblNoResult.Visible = True
    '            Else
    '                Me.LblNoResult.Visible = False
    '            End If
    '        End If

    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        'Me.txtcomplessi.Text = 2
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub BtnConfermaedificio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConfermaedificio.Click
        ''EDIFICI AddSelectedEdifici()
    End Sub
    '' EDIFICI   Private Sub AddSelectedEdifici()

    ''        Dim i, j As Integer
    ''        Try

    ''            tabedifici.Visible = True
    ''            txtseledifici.Visible = True

    ''            If par.OracleConn.State = Data.ConnectionState.Closed Then
    ''                par.OracleConn.Open()
    ''                par.SettaCommand(par)
    ''            End If

    ''            'Rimuovo il contenuto della lista corrente
    ''            For i = 0 To Me.lstedifici.Items.Count() - 1
    ''SCALE:
    ''                For j = 0 To lstScale.Count - 1
    ''                    If lstScale(j).ID = lstedifici.Items(i).Value Then
    ''                        lstScale.RemoveAt(j)
    ''                        GoTo SCALE
    ''                    End If
    ''                Next
    ''            Next i

    ''            For i = 0 To Me.lstedifici.Items.Count() - 1
    ''                If Me.lstedifici.Items(i).Selected = True Then

    ''                    Dim gen As CM.Scale

    ''                    'inserisco gli elementi selezionati nella lista da visualizzare in datagrid 
    ''                    gen = New CM.Scale(lstedifici.Items(i).Value, lstedifici.Items(i).Text, "")

    ''                    tabedifici.DataSource = Nothing
    ''                    lstScale.Add(gen)
    ''                    gen = Nothing

    ''                End If
    ''            Next
    ''            tabedifici.DataSource = lstScale
    ''            tabedifici.DataBind()

    ''            If tabedifici.Items.Count <> 0 Then
    ''                btnEliminaEdificio.Visible = True
    ''            End If

    ''            Me.txtedifici.Text = "1"
    ''        Catch ex As Exception
    ''            Me.lblErrore.Visible = True
    ''            lblErrore.Text = ex.Message

    ''        End Try
    ''    End Sub
    'EDIFICI ''Public Function edificiSelezionati() As Boolean
    ' ''    Try
    ' ''        Dim I As Integer
    ' ''        edificiSelezionati = False
    ' ''        For I = 0 To Me.lstedifici.Items.Count() - 1
    ' ''            If Me.lstedifici.Items(I).Selected = True Then
    ' ''                edificiSelezionati = True
    ' ''                Exit For
    ' ''            End If
    ' ''        Next
    ' ''    Catch ex As Exception
    ' ''        Me.lblErrore.Visible = True
    ' ''        lblErrore.Text = ex.Message
    ' ''    End Try
    ' ''End Function

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        '' '' '' '' '' ''txtedifici.Text = "2" 'altrimenti si chiude la lista  EDIFICI
        '' '' '' '' '' ''txtcomplessi.Text = "" 'altrimenti si vede la lista
        '' '' '' '' '' ''Try
        '' '' '' '' '' ''    If par.OracleConn.State = Data.ConnectionState.Closed Then
        '' '' '' '' '' ''        par.OracleConn.Open()
        '' '' '' '' '' ''        par.SettaCommand(par)
        '' '' '' '' '' ''    End If

        '' '' '' '' '' ''    Me.lstedifici.Items.Clear()
        '' '' '' '' '' ''    If Me.cmbComplesso.SelectedValue <> "-1" Then
        '' '' '' '' '' ''        par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID_COMPLESSO=" & cmbComplesso.SelectedValue.ToString & " order by DENOMINAZIONE asc"
        '' '' '' '' '' ''    Else
        '' '' '' '' '' ''        par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID order by DENOMINAZIONE asc"
        '' '' '' '' '' ''    End If
        '' '' '' '' '' ''    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '' '' '' '' '' ''    While myReader1.Read
        '' '' '' '' '' ''        lstedifici.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
        '' '' '' '' '' ''    End While
        '' '' '' '' '' ''    If lstedifici.Items.Count = 0 Then
        '' '' '' '' '' ''        Me.LblNoResulted.Visible = True
        '' '' '' '' '' ''    Else
        '' '' '' '' '' ''        Me.LblNoResulted.Visible = False
        '' '' '' '' '' ''    End If
        '' '' '' '' '' ''    myReader1.Close()

        '' '' '' '' '' ''    '*********************CHIUSURA CONNESSIONE**********************
        '' '' '' '' '' ''    par.cmd.Dispose()
        '' '' '' '' '' ''    par.OracleConn.Close()
        '' '' '' '' '' ''    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '' '' '' '' '' ''    'Me.txtedifici.Text = 2
        '' '' '' '' '' ''Catch ex As Exception
        '' '' '' '' '' ''    Me.lblErrore.Visible = True
        '' '' '' '' '' ''    lblErrore.Text = ex.Message
        '' '' '' '' '' ''End Try
    End Sub

    Protected Sub tabedifici_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles tabedifici.ItemDataBound
        ''EDIFICI ''If e.Item.ItemType = ListItemType.Item Then
        '' ''    '---------------------------------------------------         
        '' ''    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '' ''    '---------------------------------------------------       
        '' ''    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '' ''    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        '' ''    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtseledifici').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
        '' ''End If
        '' ''If e.Item.ItemType = ListItemType.AlternatingItem Then
        '' ''    '---------------------------------------------------         
        '' ''    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '' ''    '---------------------------------------------------         
        '' ''    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '' ''    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        '' ''    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtseledifici').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
        '' ''End If
    End Sub

    Protected Sub btnEliminaEdificio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaEdificio.Click
        '  EDIFICI      If txtIdComponente.Text <> "" Then
        '            Dim j As Integer

        '            'Pulisco elementi selezionati nella checkboxlist e la lista nella tabella
        '            For j = 0 To Me.lstedifici.Items.Count() - 1
        '                If lstedifici.Items(j).Value = txtIdComponente.Text Then
        '                    lstedifici.Items(j).Selected = False
        '                End If
        '            Next
        'SCALE:
        '            For j = 0 To lstScale.Count - 1
        ''                If lstScale(j).ID = txtIdComponente.Text Then
        '                    lstScale.RemoveAt(j)
        '                    GoTo SCALE
        '                End If
        '            Next

        '            tabedifici.DataSource = lstScale
        '            tabedifici.DataBind()

        '            txtIdComponente.Text = ""

        '            If tabedifici.Items.Count = 0 Then
        '                btnEliminaEdificio.Visible = False
        '                tabedifici.Visible = False
        '            End If

        '        End If
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

    Protected Sub btnEliminaComplesso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaComplesso.Click
        Dim str1 As String
        If txtIdComponente.Text <> "" Then
            Dim i As Integer

            'Pulisco elementi selezionati nella checkboxlist
            For i = 0 To Me.lstcomplessi.Items.Count() - 1
                If lstcomplessi.Items(i).Value = txtIdComponente.Text Then
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

            par.cmd.CommandText = "select SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                & "from SISCOM_MI.COMPLESSI_IMMOBILIARI where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (" & str1 & ") order by DENOMINAZIONE"



            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            myReader = par.cmd.ExecuteReader()


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "COMPLESSI_IMMOBILIARI")


            tabcomplessi.DataSource = ds
            tabcomplessi.DataBind()

            ds.Dispose()
            myReader.Close()

            If tabcomplessi.Items.Count = 0 Then
                btnEliminaComplesso.Visible = False
                tabcomplessi.Visible = False
            End If

        End If
    End Sub

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConfermaComplesso.Click
    '    AddSelectedComplessi()
    'End Sub

    Protected Sub lstservizi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstservizi.SelectedIndexChanged

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If


        Dim c As Integer
        'controllo se il complesso è già stato assegnato allo stesso servizio con stesso esercizio in un altro lotto
        For c = 0 To lstservizi.Items.Count - 1
            If lstservizi.Items(c).Selected = True And Str(lstservizi.Items(c).Value) > -1 Then
                If controlla_servizio(lstservizi.Items(c).Value) = False Then
                    lstservizi.Items(c).Selected = False
                    Exit Sub
                End If
            End If
        Next

        Dim servizi As String

        If cmbfiliale.SelectedValue <> "-1" And lstservizi.SelectedValue <> "-1" Then

            Me.lstcomplessi.Items.Clear()

            Dim s As Integer
            servizi = ""
            For s = 0 To lstservizi.Items.Count - 1
                If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then

                    If servizi = "" Then
                        servizi = lstservizi.Items(s).Value
                    Else
                        servizi = servizi & "," & lstservizi.Items(s).Value
                    End If
                End If
            Next

            If servizi = "" Then
                servizi = "-1"
            End If


            Dim TipoStruttura1 As String = ""



            Select Case tipostruttura.Value
                Case "0"
                    TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
                Case "1"
                    TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
                Case "2"
                    TipoStruttura1 = ""
            End Select


            par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                     & "  from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                      & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " & TipoStruttura1 & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID not in " _
                                      & "     (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI_SERVIZI,SISCOM_MI.LOTTI" _
                                      & "      WHERE SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO AND " _
                                      & "      SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID AND " _
                                       & "     SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID AND " _
                                      & "        LOTTI_SERVIZI.ID_SERVIZIO IN (" & servizi & ") and LOTTI_SERVIZI.ID_LOTTO<>" & vId & " AND LOTTI.ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue & " ) " _
                                      & " ORDER BY DENOMINAZIONE ASC"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            If lstcomplessi.Items.Count = 0 Then
                Me.LblNoResult.Visible = True
                'Response.Write("<script>alert('Servizio non selezionabile ');</script>")
                'servizi = "-1"
                'lstcomplessi.Items(e.ToString).Selected = False
            Else
                Me.LblNoResult.Visible = False
            End If

            If servizi <> "-1" Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI_PATRIMONIO WHERE ID_LOTTO = " & vId
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    If myReader1("ID_COMPLESSO") <> 0 Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID=" & myReader1("ID_COMPLESSO")
                        Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myreader2.Read Then
                            ' lstcomplessi.Items.Add(New ListItem(par.IfNull(myreader2("DENOMINAZIONE"), " "), par.IfNull(myreader2("ID"), -1)))
                            Me.lstcomplessi.Items.FindByValue(par.IfNull(myreader2("ID"), "")).Selected = True
                        End If
                        myreader2.Close()
                    End If
                End If

            End If

            ' AddSelectedComplessi() aggiorno la tabella

            'Dim i As Integer
            'For s = 0 To lstservizi.Items.Count - 1
            '    If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then
            '        For i = 0 To lstcomplessi.Items.Count - 1
            '            If lstcomplessi.Items(i).Selected = True And Str(lstcomplessi.Items(i).Value) > -1 Then
            '                par.cmd.CommandText = " select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO FROM SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI_SERVIZI,SISCOM_MI.LOTTI" _
            '               & " where SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO=" & lstcomplessi.Items(i).Value & " AND SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=4 AND SISCOM_MI.LOTTI.ID!=" & vId & " AND SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue & " AND SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID and SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID"
            '                myReader1 = par.cmd.ExecuteReader
            '                If myReader1.Read Then
            '                    Response.Write("<script>alert('Per questo servizio sono già stati selezionati dei complessi in altri lotti con lo stesso esercizio finanziario');</script>")
            '                    lstservizi.Items(i).Selected = False
            '                End If
            '            End If
            '        Next

            '    End If
            'Next
        End If
    End Sub
    Public Function controlla_servizio(ByVal servizio) As Boolean
        controlla_servizio = True
        Dim i As Integer
        For i = 0 To lstcomplessi.Items.Count - 1
            If lstcomplessi.Items(i).Selected = True And Str(lstcomplessi.Items(i).Value) > -1 Then
                par.cmd.CommandText = " select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO FROM SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI_SERVIZI,SISCOM_MI.LOTTI" _
               & " where SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO=" & lstcomplessi.Items(i).Value & " AND SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=" & servizio & " AND SISCOM_MI.LOTTI.ID!=" & vId & " AND SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue & " AND SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID and SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID"
                Dim myreader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myreader1.Read Then
                    Response.Write("<script>alert('Attenzione!\nPer questo servizio i complessi disponibili sono già stati selezionati in altri lotti con lo stesso esercizio finanziario');</script>")
                    controlla_servizio = False
                End If
            End If
        Next
    End Function

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    '    Dim servizi As String


    '    If cmbfiliale.SelectedValue <> "-1" And lstservizi.SelectedValue <> "-1" Then

    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        Me.lstcomplessi.Items.Clear()

    '        Dim s As Integer
    '        servizi = ""
    '        For s = 0 To lstservizi.Items.Count - 1
    '            If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then

    '                If servizi = "" Then
    '                    servizi = lstservizi.Items(s).Value
    '                Else
    '                    servizi = servizi & "," & lstservizi.Items(s).Value
    '                End If
    '            End If
    '        Next

    '        If servizi = "" Then
    '            servizi = "-1"
    '        End If

    '        par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
    '                                    & "  from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                     & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " and " _
    '                                    & "        SISCOM_MI.COMPLESSI_IMMOBILIARI.ID not in " _
    '                                     & "     (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI_SERVIZI " _
    '                                     & "            WHERE SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO AND " _
    '                                     & "                   LOTTI_SERVIZI.ID_SERVIZIO IN (" & servizi & ")) ORDER BY DENOMINAZIONE ASC"





    '        'par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
    '        '                & " from SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER, SISCOM_MI.TAB_SERVIZI " _
    '        '                 & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND " _
    '        '                 & "      SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE And " _
    '        '                 & "      SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE And " _
    '        '                 & "      SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " And " _
    '        '                 & "      SISCOM_MI.COMPLESSI_IMMOBILIARI.ID NOT IN " _
    '        '                 & "     (SELECT SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO FROM SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI_SERVIZI " _
    '        '                 & "            WHERE SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO AND " _
    '        '                 & "                   LOTTI_SERVIZI.ID_SERVIZIO IN (" & servizi & ")) ORDER BY DENOMINAZIONE ASC"


    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        While myReader1.Read
    '            lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
    '        End While
    '        myReader1.Close()
    '        If lstcomplessi.Items.Count = 0 Then
    '            Me.LblNoResult.Visible = True
    '        Else
    '            Me.LblNoResult.Visible = False
    '        End If

    '    End If
    'End Sub

    Protected Sub cmbesercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbesercizio.SelectedIndexChanged

        Try
            If cmbesercizio.SelectedValue <> "-1" Then
                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'Svuoto gli oggetti contenenti i dati associati alla precedente filiale per poi ricaricarli 
                lstservizi.Items.Clear()
                lstcomplessi.Items.Clear()
                tabcomplessi.DataBind()
                par.cmd.CommandText = "SELECT  SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " ORDER BY SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read()
                    lstservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                End While
                myReader2.Close()
            End If

            Me.txtcomplessi.Text = ""
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
