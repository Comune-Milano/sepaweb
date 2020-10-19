Imports System.Collections

Partial Class IMPIANTI_Imp_Gas
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""

    Public TabberHide As String = ""

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String
    Public sOrdinamento As String
    Public sVerifiche As String

    Public sProvenienza As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0


        If Not IsPostBack Then

            Try
                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreImpianto = Request.QueryString("IM")

                sOrdinamento = Request.QueryString("ORD")
                sVerifiche = Request.QueryString("VER")

                sProvenienza = Request.QueryString("SL")

                vIdImpianto = 0
                vIdImpianto = Session.Item("ID")


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)
                Me.txtIdImpianto.Text = "-1"

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    HttpContext.Current.Session.Add("SESSION_IMPIANTI", par.OracleConn)
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

                SettaggioCampi()

                If vIdImpianto <> 0 Then
                    TabberHide = "tabbertab"
                    VisualizzaDati()
                    txtindietro.Text = 0

                    'Visibile = "Visible =" & Chr(34) & "true" & Chr(34)
                    'Visibile = "style=" & Chr(34) & "visibility:hidden" & Chr(34)
                    Tabber1 = "tabbertabdefault"
                Else
                    TabberHide = "tabbertabhide"
                    Me.txtIdImpianto.Text = -1

                    Me.btnElimina.Visible = False

                    'Visibile = "style=" & Chr(34) & "visibility:false" & Chr(34) NOTA: non si riesce a rendere non visibile un tabber tab
                    Tabber1 = "tabbertabdefault"
                    txtindietro.Text = 1
                End If

                'Controllo modifica campi nel form
                Dim CTRL As Control

                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                For Each CTRL In Me.Tab_Gas_Generale.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                '*****************

                cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                DrLEdificio.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

                If par.IfNull(sProvenienza, 0) = 1 Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                    btnINDIETRO.Visible = False
                Else
                    Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_GAS.png';</script>")
                End If


                If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 11, 1) = 0 Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                End If

            Catch ex As Exception

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try

        End If


    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
            Case "3"
                Tabber3 = "tabbertabdefault"
        End Select

        If vIdImpianto <> 0 Then
            TabberHide = "tabbertab"
        Else
            TabberHide = "tabbertabhide"
        End If

    End Sub

    Private Sub SettaggioCampi()
        Dim ds As New Data.DataSet()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dlist As CheckBoxList
        Dim gest As Integer

        'CARICO COMBO COMPLESSI
        gest = 0

        Try

            If gest > 0 Then
                par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where substr(ID,1,1)= " & gest & " and ID<>1 order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID<>1 order by DENOMINAZIONE asc"
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"



            '*** TIPOLOGIA_TUBI_GAS
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_TUBI_GAS order by DESCRIZIONE"
            myReader1 = par.cmd.ExecuteReader

            CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).SelectedValue = "-1"
            'CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).Text = CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).Items(0).Text
            myReader1.Close()


            ' TIPOLOGIA_SERVIZIO_GAS
            dlist = CType(Tab_Gas_Generale.FindControl("chkTipoServizio"), CheckBoxList)

            da = New Oracle.DataAccess.Client.OracleDataAdapter("select ID,DESCRIZIONE from SISCOM_MI.TIPOLOGIA_SERVIZI_GAS order by DESCRIZIONE asc", par.OracleConn)

            da.Fill(ds, "SISCOM_MI.TIPOLOGIA_SERVIZI_GAS")

            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "DESCRIZIONE"
            dlist.DataValueField = "ID"
            dlist.DataBind()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

        'CaricaEdifici()
    End Sub

    Private Sub CaricaEdifici()
        Dim gest As Integer = 0

        Try

            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))

            If gest <> 0 Then
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(ID,1,1)= " & gest & " order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI order by DENOMINAZIONE asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While

            DrLEdificio.SelectedValue = "-1"

            myReader1.Close()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.cmbComplesso.SelectedValue <> "-1" Then
            FiltraEdifici()
        Else
            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))
        End If
    End Sub



    Private Sub FiltraEdifici()
        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If Me.cmbComplesso.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Me.DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))


                '*** EDIFICI maschera principale
                par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE " _
                                    & " from SISCOM_MI.EDIFICI " _
                                    & " where SISCOM_MI.EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString _
                                    & " order by DENOMINAZIONE asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader1.Read
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()


                If FlagConnessione = True Then
                    par.OracleConn.Close()
                End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
        Dim i As Integer


        '*** FORM PRINCIPALE
        Me.txtIdImpianto.Text = par.IfNull(myReader1("ID"), "-1")

        Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
        FiltraEdifici()

        Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")

        Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")


        '*** I_GAS
        par.cmd.CommandText = "select * from SISCOM_MI.I_GAS where SISCOM_MI.I_GAS.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            '*** TAB GENERALE
            CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_TUBI_GAS"), "-1")
            'CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).Items.FindByValue(par.IfNull(myReader2("ID_TIPOLOGIA_TUBI_GAS"), "-1")).Selected = True
            CType(Tab_Gas_Generale.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(Tab_Gas_Generale.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

            CType(Tab_Gas_Generale.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")

        End If
        myReader2.Close()


        'SERVIZI GAS
        par.cmd.CommandText = "select SISCOM_MI.I_GAS_SERVIZI.ID_TIPOLOGIA_SERVIZI_GAS from SISCOM_MI.I_GAS_SERVIZI where  SISCOM_MI.I_GAS_SERVIZI.ID_GAS = " & vIdImpianto
        myReader2 = par.cmd.ExecuteReader()

        While myReader2.Read
            For i = 0 To CType(Tab_Gas_Generale.FindControl("chkTipoServizio"), CheckBoxList).Items.Count - 1
                If CType(Tab_Gas_Generale.FindControl("chkTipoServizio"), CheckBoxList).Items(i).Value = par.IfNull(myReader2("ID_TIPOLOGIA_SERVIZI_GAS"), "-1") Then
                    CType(Tab_Gas_Generale.FindControl("chkTipoServizio"), CheckBoxList).Items(i).Selected = True
                End If
            Next
        End While
        myReader2.Close()

        'DISABILITO COMBO COMPLESSO ED EDIFICIO (NO per questo impianto)
        Me.cmbComplesso.Enabled = False

    End Sub

    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try
            'Apro la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdImpianto <> 0 Then
                ' LEGGO IMPIANTI (Elettrico)

                par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Impianto aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If

                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

            Else
                'par.myTrans.Rollback()
                par.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vIdImpianto = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If

    End Sub

    Public Function ControlloCampi() As Boolean

        ControlloCampi = True

        If Me.cmbComplesso.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare un Complesso Immobiliare!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If par.IfEmpty(Me.txtDenominazione.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Denominazione Impianto!');</script>")
            ControlloCampi = False
            txtDenominazione.Focus()
            Exit Function
        End If

    End Function


    Private Sub Salva()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            ' '' Ricavo vIdImpianto
            par.cmd.CommandText = " select SISCOM_MI.SEQ_IMPIANTI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vIdImpianto = myReader1(0)
            End If

            myReader1.Close()
            par.cmd.CommandText = ""

            Me.txtIdImpianto.Text = vIdImpianto

            ' IMPIANTI
            par.cmd.Parameters.Clear()

            par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI (ID, ID_COMPLESSO, ID_EDIFICIO,COD_TIPOLOGIA,DESCRIZIONE,ANNOTAZIONI) " _
                                & "values (:id,:id_complesso,:id_edificio,:cod_tipologia,:descrizione,:annotazioni)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "GA"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Gas_Generale.FindControl("txtNote"), TextBox).Text, 4000)))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")



            ' I_GAS
            par.cmd.CommandText = "insert into SISCOM_MI.I_GAS (ID,DITTA_GESTIONE,TELEFONO_DITTA,ID_TIPOLOGIA_TUBI_GAS) " _
                                & "values (:id,:ditta_gestione,:num_tel,:tipo_tubi)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Gas_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Gas_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_tubi", RitornaNullSeIntegerMenoUno(CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).SelectedValue.ToString)))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*******************************

            AggiornaServiziGAS()

            ' COMMIT
            par.myTrans.Commit()


            '*** AGGIORNO il COD. IMPIANTO
            par.cmd.CommandText = "select COD_IMPIANTO from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                Me.txtCodImpianto.Text = myReader2(0)
            End If

            myReader2.Close()
            par.cmd.CommandText = ""

            'DISABILITO COMBO COMPLESSO ED EDIFICIO (NO per questo impianto)
            Me.cmbComplesso.Enabled = False

            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            'VisualizzaDati()

            TabberHide = "tabbertab"

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"



        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

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


            '* IMPIANTI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "update SISCOM_MI.IMPIANTI set ID_COMPLESSO=:id_complesso, ID_EDIFICIO=:id_edificio,COD_TIPOLOGIA=:cod_tipologia,DESCRIZIONE=:descrizione,ANNOTAZIONI=:annotazioni where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "GA"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Gas_Generale.FindControl("txtNote"), TextBox).Text, 4000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")



            '*** I_GAS
            par.cmd.CommandText = " update SISCOM_MI.I_GAS " _
                                & " set DITTA_GESTIONE=:ditta_gestione,TELEFONO_DITTA=:num_tel,ID_TIPOLOGIA_TUBI_GAS=:tipo_tubi " _
                                & " where ID=:id_impianto"


            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Gas_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Gas_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_tubi", RitornaNullSeIntegerMenoUno(CType(Tab_Gas_Generale.FindControl("cmbTubazione"), DropDownList).SelectedValue.ToString)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            AggiornaServiziGAS()

            par.myTrans.Commit() 'COMMIT

            '*** AGGIORNO il COD. IMPIANTO
            par.cmd.CommandText = "select COD_IMPIANTO from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                Me.txtCodImpianto.Text = myReader2(0)
            End If

            myReader2.Close()
            par.cmd.CommandText = ""


            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Dim sNote As String

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtElimina.Text = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI where ID = " & vIdImpianto
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""


                'LOG CANCELLAZIONE
                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "CANCELLAZIONE IMPIANTO"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO GAS"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Impianto GAS del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", sNote))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************

                ' COMMIT
                par.myTrans.Commit()

                Response.Write("<SCRIPT>alert('Eliminazione completata correttamente!');</SCRIPT>")

                Session.Add("LAVORAZIONE", "0")

                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreImpianto = Request.QueryString("IM")

                sOrdinamento = Request.QueryString("ORD")
                sVerifiche = par.IfEmpty(par.IfNull(Request.QueryString("VER"), ""), "")


                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()

                Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
                If txtindietro.Text = 1 Then
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                ElseIf Strings.Len(sVerifiche) > 0 Then
                    Response.Write("<script>location.replace('RisultatiVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
                Else
                    Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "');</script>")
                End If

            Else
                CType(Me.Page.FindControl("txtElimina"), TextBox).Text = "0"
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            sProvenienza = Request.QueryString("SL")
            If par.IfNull(sProvenienza, 0) = 1 Then
                Response.Write("<script>self.close();</script>")
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub

    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = par.IfEmpty(par.IfNull(Request.QueryString("VER"), ""), "")


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            ElseIf Strings.Len(sVerifiche) > 0 Then
                Response.Write("<script>location.replace('RisultatiVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
            Else
                Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "');</script>")
            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

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

    Public Property vIdImpianto() As Long
        Get
            If Not (ViewState("par_idImpianto") Is Nothing) Then
                Return CLng(ViewState("par_idImpianto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idImpianto") = value
        End Set

    End Property

    Private Function RitornaNumDaSiNo(ByVal valoredapassare As String) As String
        Dim a As String = "Null"

        Try
            If valoredapassare = "SI" Then
                a = 1
            ElseIf valoredapassare = "NO" Then
                a = 0
            Else
                a = "Null"
            End If

        Catch ex As Exception
        End Try

        Return a
    End Function

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try
            Me.btnSalva.Visible = False
            Me.btnElimina.Visible = False

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

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub




    Private Sub AggiornaServiziGAS()
        Dim i As Integer

        Try

            par.cmd.CommandText = "delete from SISCOM_MI.I_GAS_SERVIZI where ID_GAS = " & vIdImpianto
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            For i = 0 To CType(Tab_Gas_Generale.FindControl("chkTipoServizio"), CheckBoxList).Items.Count - 1
                If CType(Tab_Gas_Generale.FindControl("chkTipoServizio"), CheckBoxList).Items(i).Selected = True Then
                    par.cmd.CommandText = "insert into SISCOM_MI.I_GAS_SERVIZI (ID_GAS,ID_TIPOLOGIA_SERVIZI_GAS) values " _
                               & "(" & vIdImpianto & "," & CType(Tab_Gas_Generale.FindControl("chkTipoServizio"), CheckBoxList).Items(i).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                End If
            Next


        Catch ex As Exception
        End Try


    End Sub

End Class
