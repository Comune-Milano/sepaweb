Imports System.Collections


Partial Class IMPIANTI_Imp_Teleriscaldamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""

    Public TabberHide As String = ""

    Public Visibile As String = ""

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String
    Public sOrdinamento As String
    Public sVerifiche As String

    Public sProvenienza As String

    'Dim lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)

    Dim lstEdificiCT As System.Collections.Generic.List(Of Epifani.EdificiCT)
    Dim lstUnita As System.Collections.Generic.List(Of Epifani.ListaUI)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        'lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))

        lstEdificiCT = CType(HttpContext.Current.Session.Item("LST_EDIFICI_CT"), System.Collections.Generic.List(Of Epifani.EdificiCT))
        lstUnita = CType(HttpContext.Current.Session.Item("LST_UI"), System.Collections.Generic.List(Of Epifani.ListaUI))


        If Not IsPostBack Then

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            sProvenienza = Request.QueryString("SL")

            vIdImpianto = 0

            If Not IsNothing(Request.QueryString("ID")) Then
                vIdImpianto = Request.QueryString("ID")
                lstEdificiCT = New System.Collections.Generic.List(Of Epifani.EdificiCT)
                lstUnita = New System.Collections.Generic.List(Of Epifani.ListaUI)
                
            Else
                vIdImpianto = Session.Item("ID")
            End If

            'lstEdifici.Clear()

            lstEdificiCT.Clear()
            lstUnita.Clear()

            ' CONNESSIONE DB
            lIdConnessione = Format(Now, "yyyyMMddHHmmss")

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
            CType(Me.Page.FindControl("txtTIPO_IMPIANTO"), TextBox).Text = "TR"


            SettaggioCampi()
            AbilitazioneOggetti(True)


            If vIdImpianto <> 0 Then
                'Visibile = "style=" & Chr(34) & "visibility:Visible" & Chr(34)
                TabberHide = "tabbertab"
                VisualizzaDati()
                txtindietro.Text = 0
                Tabber1 = "tabbertabdefault"
            Else
                '*** DEVO DISABILITARE IL TAB COMPONENTI
                'NuovoImpianto()
                TabberHide = "tabbertabhide"
                Me.txtIdImpianto.Text = -1
                Me.btnElimina.Visible = False

                'Visibile = "style=" & Chr(34) & "visibility:hidden" & Chr(34)

                Tabber1 = "tabbertabdefault"
                txtindietro.Text = 1
                'Me.LblRiepilogo.Visible = False
            End If

            Dim CTRL As Control

            '*** FORM PRINCIPALE
            cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            DrLEdificio.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            cmbTipoUso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")

            txtDenominazione.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtDitta.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

            txtAnnoRealizzazione.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtAnnoRealizzazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            '*** FORM GENERALE
            For Each CTRL In Me.TabGeneraleT.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '' Controllo DATA
            CType(TabGeneraleT.FindControl("txtDataAccensione"), TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            CType(TabGeneraleT.FindControl("txtDataAccensione"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(TabGeneraleT.FindControl("txtDataRiposo"), TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            CType(TabGeneraleT.FindControl("txtDataRiposo"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(TabGeneraleT.FindControl("cmbEstintori"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            '***************************


            '*** TAB DETTAGLI TELE
            For Each CTRL In Me.TabDettagliTELE.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            ' TAB_DETTAGLI1
            For Each CTRL In Me.TabDettagli1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            ' TAB_POMPE
            For Each CTRL In Me.Tab_Termico_Pompe.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** TAB CERTIFICAZIONI
            For Each CTRL In Me.Tab_Termico_Certificazioni.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** Controllo DATA
            CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            '*******************

            If par.IfNull(sProvenienza, 0) = 1 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                btnINDIETRO.Visible = False
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Teleriscaldamento.png';</script>")
            End If


            If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 5, 1) = 0 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            End If

        End If


            CType(TabGeneraleT.FindControl("cmbTipoSerbatoio"), DropDownList).Enabled = True
            CType(TabGeneraleT.FindControl("lblSerbatoio"), Label).Enabled = True

            CType(TabGeneraleT.FindControl("txtCapacita"), TextBox).Enabled = True
            CType(TabGeneraleT.FindControl("lblCapacita"), Label).Text = "Capacità (mc)"
            CType(TabGeneraleT.FindControl("lblCapacita"), Label).Enabled = True

            CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).Visible = False


            If CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).Text = "1" Then 'METANO
                CType(TabGeneraleT.FindControl("cmbTipoSerbatoio"), DropDownList).Text = ""
                CType(TabGeneraleT.FindControl("cmbTipoSerbatoio"), DropDownList).Enabled = False
                CType(TabGeneraleT.FindControl("lblSerbatoio"), Label).Enabled = False

                CType(TabGeneraleT.FindControl("lblCapacita"), Label).Text = "Contatore PDR"
                CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).Visible = True

                CType(TabGeneraleT.FindControl("txtCapacita"), TextBox).Visible = False
            End If

            If CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).Text = "3" Then  'GASOLIO
                CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Enabled = True
                CType(Tab_Termico_Certificazioni.FindControl("lblDecreto"), Label).Enabled = True
            Else
                CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Text = ""
                CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Enabled = False
                CType(Tab_Termico_Certificazioni.FindControl("lblDecreto"), Label).Enabled = False
            End If

            ' CAMPI NON PIU' UTILIZZATI
            CType(TabGeneraleT.FindControl("lblEstintori"), Label).Visible = False
            CType(TabGeneraleT.FindControl("cmbEstintori"), DropDownList).Visible = False

            CType(TabGeneraleT.FindControl("lblNumEstintori"), Label).Visible = False
            CType(TabGeneraleT.FindControl("txtNumEstintori"), TextBox).Visible = False



        If Session.Item("TERMICO_UNITA") = 1 Then

            Dim i As Integer
            Dim SommaUI As Integer = 0
            Dim SommaMQ As Decimal = 0
            Dim SommaEdifici As Integer = 0

            Dim oDataGridItem As DataGridItem


            CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).DataSource = Nothing
            CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).DataSource = lstEdificiCT
            CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).DataBind()


            'Loop della griglia per settare il flag a tutti gli edifici salvati in IMPIANTI_EDIFICI
            '     e la somma delle UI e MQ totali
            For Each oDataGridItem In CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).Items

                For i = 0 To lstEdificiCT.Count - 1

                    If lstEdificiCT.Item(i).ID = oDataGridItem.Cells(0).Text Then
                        If par.IfEmpty(lstEdificiCT.Item(i).TOTALE_UI_AL, 0) > 0 Then
                            SommaUI = SommaUI + oDataGridItem.Cells(2).Text
                            SommaMQ = SommaMQ + oDataGridItem.Cells(4).Text
                            SommaEdifici = SommaEdifici + 1
                        End If
                        Exit For
                    End If
                Next i
            Next


            CType(TabGeneraleT.FindControl("txtTotUI"), TextBox).Text = SommaUI
            CType(TabGeneraleT.FindControl("txtTotMq"), TextBox).Text = IsNumFormat(SommaMQ, "", "##,##0.00")

            Me.txtModificato.Text = "1"

            Session.Remove("TERMICO_UNITA")

        End If



    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
            Case "3"
                Tabber3 = "tabbertabdefault"
            Case "4"
                Tabber4 = "tabbertabdefault"
            Case "5"
                Tabber5 = "tabbertabdefault"
            Case "6"
                Tabber6 = "tabbertabdefault"
        End Select

        If vIdImpianto <> 0 Then
            TabberHide = "tabbertab"
        Else
            TabberHide = "tabbertabhide"
        End If


    End Sub

    Private Sub SettaggioCampi()

        'CARICO COMBO COMPLESSI
        Dim gest As Integer
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


            '*** TIPOLOGIA_USO_TERMICI
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_USO_TERMICI order by ID"
            myReader1 = par.cmd.ExecuteReader
            'cmbTipoUso.Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                If par.IfNull(myReader1("DESCRIZIONE"), "") <> "COMBINATA" Then
                    cmbTipoUso.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End If
            End While
            cmbTipoUso.Text = cmbTipoUso.Items(0).Text
            myReader1.Close()
            '*********************+++


            '***  TIPOLOGIA_COMBUSTIBILI (Tab Generale)
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_COMBUSTIBILI order by ID"
            myReader1 = par.cmd.ExecuteReader
            CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).Text = "-1"
            myReader1.Close()

            'par.OracleConn.Close()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
        CaricaEdifici()



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

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.cmbComplesso.SelectedValue <> "-1" Then

            FiltraEdifici()
        Else
            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))
        End If

    End Sub

    Protected Sub cmbComplesso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Private Sub FiltraEdifici()
        Dim StringaSql As String

        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean

        Dim SommaUI As Integer
        Dim SommaMQ As Decimal
        Dim SommaEdifici As Integer

        Dim i As Integer
        Dim oDataGridItem As DataGridItem

        Try

            SommaMQ = 0
            SommaUI = 0
            SommaEdifici = 0

            FlagConnessione = False
            If Me.cmbComplesso.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim gest As Integer = 0

                Me.DrLEdificio.Items.Clear()
                Me.DrLEdificio.Items.Add(New ListItem(" ", -1))

                'lstEdifici.Clear()

                lstEdificiCT.Clear()
                lstUnita.Clear()



                If gest <> 0 Then
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE," _
                                                    & " (select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and (COD_TIPOLOGIA <> 'K' AND COD_TIPOLOGIA <> 'B' AND COD_TIPOLOGIA <> 'C' AND COD_TIPOLOGIA <> 'H' AND COD_TIPOLOGIA <> 'I'))," _
                                                    & " (SELECT SUM (( " _
                                                    & " select sum(VALORE) from SISCOM_MI.DIMENSIONI where ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID and COD_TIPOLOGIA='SUP_NETTA')) " _
                                                    & " from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and (COD_TIPOLOGIA <> 'K' AND COD_TIPOLOGIA <> 'B' AND COD_TIPOLOGIA <> 'C' AND COD_TIPOLOGIA <> 'H' AND COD_TIPOLOGIA <> 'I')) " _
                                                    & " from SISCOM_MI.EDIFICI " _
                                                    & " where substr(ID,1,1)= " & gest & " order by DENOMINAZIONE asc"

                Else
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE," _
                                                    & " (select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and (COD_TIPOLOGIA <> 'K' AND COD_TIPOLOGIA <> 'B' AND COD_TIPOLOGIA <> 'C' AND COD_TIPOLOGIA <> 'H' AND COD_TIPOLOGIA <> 'I'))  as TOTALE_UI," _
                                                    & " (SELECT SUM (( " _
                                                    & " select sum(VALORE) from SISCOM_MI.DIMENSIONI where ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID and COD_TIPOLOGIA='SUP_NETTA')) " _
                                                    & " from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and (COD_TIPOLOGIA <> 'K' AND COD_TIPOLOGIA <> 'B' AND COD_TIPOLOGIA <> 'C' AND COD_TIPOLOGIA <> 'H' AND COD_TIPOLOGIA <> 'I'))  as TOTALE_MQ, " _
                                                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.showModalDialog(''Tab_Termico_Unita.aspx?TIPO=NORM&IDVISUAL=" & CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text & "&ED='||SISCOM_MI.EDIFICI.ID||''',''Dettagli'',''dialogWidth:800px;dialogHeight:550px;'');window.form1.submit();£>Seleziona Unità</a>','$','&'),'£','" & Chr(34) & "') as  UNITA  " _
                                        & " from SISCOM_MI.EDIFICI " _
                                        & " where ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString _
                                        & " order by DENOMINAZIONE asc"

                End If
                StringaSql = par.cmd.CommandText

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader1.Read
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                    'Dim gen As Epifani.Edifici
                    'gen = New Epifani.Edifici(par.IfNull(myReader1("ID"), -1), par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1(2), 0), par.IfNull(myReader1(3), 0))

                    'lstEdifici.Add(gen)
                    'gen = Nothing


                    Dim gen As Epifani.EdificiCT
                    If vIdImpianto <> 0 Then
                        gen = New Epifani.EdificiCT(par.IfNull(myReader1("ID"), -1), par.IfNull(myReader1("DENOMINAZIONE"), " "), 0, par.IfNull(myReader1("TOTALE_UI"), 0), 0, par.IfNull(myReader1("TOTALE_MQ"), 0), par.IfNull(myReader1("UNITA"), ""), False)
                    Else
                        gen = New Epifani.EdificiCT(par.IfNull(myReader1("ID"), -1), par.IfNull(myReader1("DENOMINAZIONE"), " "), 0, par.IfNull(myReader1("TOTALE_UI"), 0), 0, par.IfNull(myReader1("TOTALE_MQ"), 0), par.IfNull(myReader1("UNITA"), ""), True)
                    End If
                    lstEdificiCT.Add(gen)
                    gen = Nothing


                End While
                myReader1.Close()



                If vIdImpianto <> 0 Then

                    SommaUI = 0
                    SommaMQ = 0

                    'Riempio EVENTUALE lista delle Unità 
                    par.cmd.CommandText = "select *  from SISCOM_MI.IMPIANTI_UI " _
                                        & " where ID_IMPIANTO= " & vIdImpianto _
                                        & "   and ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                              & " where ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString & ")" _
                                        & "  order by ID_EDIFICIO"

                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    While myReader2.Read

                        Dim gen1 As Epifani.ListaUI
                        gen1 = New Epifani.ListaUI(par.IfNull(myReader2("ID_EDIFICIO"), -1), par.IfNull(myReader2("ID_UNITA"), -1))
                        lstUnita.Add(gen1)
                        gen1 = Nothing

                    End While
                    myReader2.Close()
                    '*****************************

                    'x ogni EDIFICIO
                    par.cmd.CommandText = "select *  from SISCOM_MI.IMPIANTI_EDIFICI " _
                                        & " where ID_IMPIANTO= " & vIdImpianto _
                                        & "   and  ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                               & " where ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString & ")"

                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read

                        For i = 0 To lstEdificiCT.Count - 1

                            If lstEdificiCT.Item(i).ID = par.IfNull(myReader2("ID_EDIFICIO"), -1) Then
                                ' Ricercato l'edificio nella lista:

                                '1) select delle UNITA per ricavare il Totale delle UNITA' alimentate
                                par.cmd.CommandText = " select count(*) from SISCOM_MI.IMPIANTI_UI " _
                                                                   & "  where ID_IMPIANTO= " & vIdImpianto _
                                                                  & "    and  ID_EDIFICIO=" & par.IfNull(myReader2("ID_EDIFICIO"), -1)

                                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                                If myReader3.Read Then
                                    If par.IfNull(myReader3(0), 0) > 0 Then lstEdificiCT.Item(i).TOTALE_UI_AL = par.IfNull(myReader3(0), 0)
                                End If
                                myReader3.Close()


                                '2) select delle UNITA per ricavare il Totale MQ delle UNITA' alimentate
                                par.cmd.CommandText = "select SUM(VALORE) from SISCOM_MI.DIMENSIONI  " _
                                                   & " where ID_UNITA_IMMOBILIARE in  (select ID_UNITA " _
                                                                                   & "  from SISCOM_MI.IMPIANTI_UI " _
                                                                                   & " where ID_IMPIANTO= " & vIdImpianto _
                                                                                   & "   and ID_EDIFICIO=" & par.IfNull(myReader2("ID_EDIFICIO"), -1) & ")" _
                                                & "  and COD_TIPOLOGIA='SUP_NETTA'"

                                myReader3 = par.cmd.ExecuteReader()

                                If myReader3.Read Then
                                    If par.IfNull(myReader3(0), 0) > 0 Then lstEdificiCT.Item(i).TOTALE_MQ_AL = par.IfNull(myReader3(0), 0)
                                End If
                                myReader3.Close()


                                '3) setto il check=TRUE
                                lstEdificiCT.Item(i).CHK = True
                                Exit For
                            End If
                        Next i
                    End While
                    myReader2.Close()
                End If


                'dlist = CType(TabGeneraleT.FindControl("CheckBoxEdifici"), CheckBoxList)
                ''da = New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, par.OracleConn)
                ''da.Fill(ds)

                ''dlist.Items.Clear()
                'dlist.DataSource = lstEdifici

                'dlist.DataTextField = "descrizione"
                'dlist.DataValueField = "ID"
                'dlist.DataBind()

                If FlagConnessione = True Then
                    par.OracleConn.Close()
                End If
            Else

                Me.DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))

                lstEdificiCT.Clear()
                lstUnita.Clear()
            End If


            CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).DataSource = Nothing
            CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).DataSource = lstEdificiCT
            CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).DataBind()


            For Each oDataGridItem In CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).Items

                For i = 0 To lstEdificiCT.Count - 1

                    If lstEdificiCT.Item(i).ID = oDataGridItem.Cells(0).Text Then
                        If par.IfEmpty(lstEdificiCT(i).TOTALE_UI_AL, 0) > 0 Then
                            SommaUI = SommaUI + oDataGridItem.Cells(2).Text
                            SommaMQ = SommaMQ + oDataGridItem.Cells(4).Text
                            SommaEdifici = SommaEdifici + 1
                        End If
                        Exit For
                    End If
                Next i
            Next


            CType(TabGeneraleT.FindControl("txtTotUI"), TextBox).Text = SommaUI
            CType(TabGeneraleT.FindControl("txtTotMq"), TextBox).Text = IsNumFormat(SommaMQ, "", "##,##0.00")


        Catch ex As Exception
            If FlagConnessione = True Then par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader




        Me.txtIdImpianto.Text = par.IfNull(myReader1("ID"), "-1")

        Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
        FiltraEdifici()

        Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")

        Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

        Me.txtDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
        Me.txtAnnoRealizzazione.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


        par.cmd.CommandText = "select * from SISCOM_MI.I_TERMICI where SISCOM_MI.I_TERMICI.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            '*** FORM PRINCIPALE
            Me.cmbTipoUso.SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), "")

            '*** TAB GENERALE
            CType(TabGeneraleT.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(TabGeneraleT.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

            CType(TabGeneraleT.FindControl("txtDittaFornitrice"), TextBox).Text = par.IfNull(myReader2("DITTA_FORNITRICE"), "")
            CType(TabGeneraleT.FindControl("txtNumTelefonico2"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA_FORNITRICE"), "")


            CType(TabGeneraleT.FindControl("txtDataAccensione"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRIMA_ACCENSIONE"), ""))
            CType(TabGeneraleT.FindControl("txtDataRiposo"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_MESSA_RIPOSO"), ""))
            CType(TabGeneraleT.FindControl("txtOreEsercizio"), TextBox).Text = par.IfNull(myReader2("NUM_ORE_ESERCIZIO"), "")

            'CType(TabGenerale.FindControl("RBList_Tipologia"), RadioButtonList).SelectedValue = par.IfNull(myReader2("COD_TIPOLOGIA"), "")
            CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).SelectedValue = par.IfNull(myReader2("ID_COMBUSTIBILE"), -1)
            CType(TabGeneraleT.FindControl("cmbTipoSerbatoio"), DropDownList).SelectedValue = par.IfNull(myReader2("SERBATOIO"), "")

            CType(TabGeneraleT.FindControl("txtCapacita"), TextBox).Text = par.IfNull(myReader2("CAPACITA"), "")
            CType(TabGeneraleT.FindControl("txtPotenza"), TextBox).Text = par.IfNull(myReader2("POTENZA"), "")
            CType(TabGeneraleT.FindControl("txtConsumo"), TextBox).Text = par.IfNull(myReader2("CONSUMO_MEDIO"), "")

            CType(TabGeneraleT.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")

            ' NOTA: prima non esistema CONTATORE_PDR, per il metano veniva usato CAPACITA, quindi se hanno inserito qualcosa prima, devo comunque visualizzarlo
            If par.IfNull(myReader2("CONTATORE_PDR"), "") = "" Then
                CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).Text = par.IfNull(myReader2("CAPACITA"), "")
                CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).ToolTip = par.IfNull(myReader2("CAPACITA"), "")

            Else
                CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).Text = par.IfNull(myReader2("CONTATORE_PDR"), "")
                CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).ToolTip = par.IfNull(myReader2("CONTATORE_PDR"), "")
            End If


            '*** TAB CERTIFICAZIONI
            CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LIBRETTO"), "")).Selected = True
            'CType(TabGenerale.FindControl("cmbExUNI"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CERT_EX_UNI_8364"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF_LG_46_90"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DECR_PREFETTIZIO_SERBATOI"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LICENZA_UTF"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_ISPESL"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).Items.FindByValue(par.IfNull(myReader2("TRATTAMENTO_ACQUA"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CONT_ENERGIA"), "")).Selected = True
            'CType(TabGenerale.FindControl("cmbDenuncia"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DENUNCIA_IMPIANTO"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRATICA_ISPESL"), ""))
            CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_DISMISSIONE_CT"), ""))

            CType(Tab_Termico_Certificazioni.FindControl("cmbCPI"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_VVF"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_RILASCIO_VVF"), ""))
            CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_SCADENZA_VVF"), ""))

            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If

        End If
        myReader2.Close()


        '*** TAB GENERALE (EDIFICI)
        'Dim lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)
        'lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))

        'SommaMQ = 0
        'SommaUI = 0

        'par.cmd.CommandText = "select SISCOM_MI.IMPIANTI_EDIFICI.ID_EDIFICIO from SISCOM_MI.IMPIANTI_EDIFICI where  SISCOM_MI.IMPIANTI_EDIFICI.ID_IMPIANTO = " & vIdImpianto

        'myReader2 = par.cmd.ExecuteReader()

        ''                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'While myReader2.Read
        '    For i = 0 To CType(TabGeneraleT.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
        '        If CType(TabGeneraleT.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
        '            CType(TabGeneraleT.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True

        '            '************+
        '            For j = 0 To lstEdifici.Count - 1

        '                If lstEdifici(j).ID = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
        '                    SommaMQ = SommaMQ + lstEdifici(j).DIMENSIONE
        '                    SommaUI = SommaUI + lstEdifici(j).TOT_UNITA
        '                End If

        '            Next j
        '            '********************************
        '        End If
        '    Next
        'End While
        'myReader2.Close()

        'CType(TabGeneraleT.FindControl("txtTotUI"), TextBox).Text = SommaUI
        'CType(TabGeneraleT.FindControl("txtTotMq"), TextBox).Text = SommaMQ



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
                ' LEGGO IMP_TERMICI

                par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then

                    '*** FORM PRINCIPALE
                    RiempiCampi(myReader1)


                    '    Me.txtIdImpianto.Text = par.IfNull(myReader1("ID"), "-1")

                    '    Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
                    '    FiltraEdifici()

                    '    Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")

                    '    Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                    '    Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

                    '    Me.txtDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
                    '    Me.txtAnnoRealizzazione.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


                    '    par.cmd.CommandText = "select * from SISCOM_MI.I_TERMICI where SISCOM_MI.I_TERMICI.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
                    '    myReader2 = par.cmd.ExecuteReader()

                    '    If myReader2.Read Then

                    '        '*** FORM PRINCIPALE
                    '        Me.cmbTipoUso.SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), "")

                    '        '*** TAB GENERALE
                    '        CType(TabGenerale.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                    '        CType(TabGenerale.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")
                    '        CType(TabGenerale.FindControl("txtDataAccensione"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRIMA_ACCENSIONE"), ""))
                    '        CType(TabGenerale.FindControl("txtDataRiposo"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_MESSA_RIPOSO"), ""))
                    '        CType(TabGenerale.FindControl("txtOreEsercizio"), TextBox).Text = par.IfNull(myReader2("NUM_ORE_ESERCIZIO"), "")

                    '        'CType(TabGenerale.FindControl("RBList_Tipologia"), RadioButtonList).SelectedValue = par.IfNull(myReader2("COD_TIPOLOGIA"), "")
                    '        CType(TabGenerale.FindControl("cmbCombustibile"), DropDownList).SelectedValue = par.IfNull(myReader2("ID_COMBUSTIBILE"), -1)
                    '        CType(TabGenerale.FindControl("cmbTipoSerbatoio"), DropDownList).SelectedValue = par.IfNull(myReader2("SERBATOIO"), "")

                    '        CType(TabGenerale.FindControl("txtCapacita"), TextBox).Text = par.IfNull(myReader2("CAPACITA"), "")
                    '        CType(TabGenerale.FindControl("txtPotenza"), TextBox).Text = par.IfNull(myReader2("POTENZA"), "")
                    '        CType(TabGenerale.FindControl("txtConsumo"), TextBox).Text = par.IfNull(myReader2("CONSUMO_MEDIO"), "")

                    '        CType(TabGenerale.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


                    '        '*** TAB CERTIFICAZIONI
                    '        CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LIBRETTO"), "")).Selected = True
                    '        'CType(TabGenerale.FindControl("cmbExUNI"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CERT_EX_UNI_8364"), "")).Selected = True
                    '        CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF_LG_46_90"), "")).Selected = True
                    '        CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DECR_PREFETTIZIO_SERBATOI"), "")).Selected = True
                    '        CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LICENZA_UTF"), "")).Selected = True
                    '        CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_ISPESL"), "")).Selected = True
                    '        CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).Items.FindByValue(par.IfNull(myReader2("TRATTAMENTO_ACQUA"), "")).Selected = True
                    '        CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CONT_ENERGIA"), "")).Selected = True
                    '        'CType(TabGenerale.FindControl("cmbDenuncia"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DENUNCIA_IMPIANTO"), "")).Selected = True
                    '        CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRATICA_ISPESL"), ""))
                    '        CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_DISMISSIONE_CT"), ""))



                    '    End If
                    '    myReader2.Close()


                    '    '*** TAB GENERALE (EDIFICI)
                    '    Dim lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)
                    '    lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))

                    '    SommaMQ = 0
                    '    SommaUI = 0

                    '    par.cmd.CommandText = "select SISCOM_MI.IMPIANTI_EDIFICI.ID_EDIFICIO from SISCOM_MI.IMPIANTI_EDIFICI where  SISCOM_MI.IMPIANTI_EDIFICI.ID_IMPIANTO = " & vIdImpianto

                    '    myReader2 = par.cmd.ExecuteReader()

                    '    '                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '    While myReader2.Read
                    '        For i = 0 To CType(TabGenerale.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
                    '            If CType(TabGenerale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
                    '                CType(TabGenerale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True

                    '                '************+
                    '                For j = 0 To lstEdifici.Count - 1

                    '                    If lstEdifici(j).ID = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
                    '                        SommaMQ = SommaMQ + lstEdifici(j).DIMENSIONE
                    '                        SommaUI = SommaUI + lstEdifici(j).TOT_UNITA
                    '                    End If

                    '                Next j
                    '                '********************************
                    '            End If
                    '        Next
                    '    End While
                    '    myReader2.Close()
                End If

                myReader1.Close()

                'CType(TabGenerale.FindControl("txtTotUI"), TextBox).Text = SommaUI
                'CType(TabGenerale.FindControl("txtTotMq"), TextBox).Text = SommaMQ



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

                ' LEGGO IL RECORD
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

        If par.IfEmpty(Me.cmbTipoUso.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Selezionare la tipologia d\'uso dell\'impianto!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If Me.txtAnnoRealizzazione.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazione.Text = ""

        End If

        If CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = "dd/mm/YYYY" Then
            CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = ""
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

            par.cmd.Parameters.Clear()

            par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI (ID, ID_COMPLESSO, ID_EDIFICIO,COD_TIPOLOGIA,DESCRIZIONE,ANNO_COSTRUZIONE,DITTA_COSTRUTTRICE,ANNOTAZIONI) " _
                                & "values (:id,:id_complesso,:id_edificio,:cod_tipologia,:descrizione,:anno,:ditta,:annotazioni)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "TR"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(TabGeneraleT.FindControl("txtNote"), TextBox).Text, 4000)))


            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")


            par.cmd.CommandText = "insert into SISCOM_MI.I_TERMICI (ID,ID_TIPOLOGIA_USO, ID_COMBUSTIBILE,SERBATOIO,CAPACITA," _
                                                                & "POTENZA,CONSUMO_MEDIO,LIBRETTO,DICH_CONF_LG_46_90,DECR_PREFETTIZIO_SERBATOI," _
                                                                & "LICENZA_UTF,PRATICA_ISPESL,TRATTAMENTO_ACQUA,CONT_ENERGIA,DATA_PRATICA_ISPESL," _
                                                                & "DATA_MESSA_RIPOSO,DITTA_GESTIONE,NUM_ORE_ESERCIZIO,DATA_PRIMA_ACCENSIONE,TELEFONO_DITTA,DATA_DISMISSIONE_CT," _
                                                                & "PRATICA_VVF,DATA_RILASCIO_VVF,DATA_SCADENZA_VVF,DITTA_FORNITRICE,TELEFONO_DITTA_FORNITRICE,CONTATORE_PDR) " _
                                 & "values (" & vIdImpianto & "," _
                                 & RitornaNullSeMenoUno(Me.cmbTipoUso.SelectedValue.ToString) & "," & RitornaNullSeMenoUno((CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).SelectedValue.ToString)) & ",'" _
                                 & par.PulisciStrSql(CType(TabGeneraleT.FindControl("cmbTipoSerbatoio"), DropDownList).SelectedItem.Text) & "'," _
                                 & par.VirgoleInPunti(par.IfEmpty(CType(TabGeneraleT.FindControl("txtCapacita"), TextBox).Text, "Null")) & "," _
                                 & par.VirgoleInPunti(par.IfEmpty(CType(TabGeneraleT.FindControl("txtPotenza"), TextBox).Text, "Null")) & "," _
                                 & par.VirgoleInPunti(par.IfEmpty(CType(TabGeneraleT.FindControl("txtConsumo"), TextBox).Text, "Null")) & ",'" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString & "','" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).SelectedValue.ToString & "','" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).SelectedValue.ToString & "','" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).SelectedValue.ToString & "','" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).SelectedValue.ToString & "','" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).SelectedValue.ToString & "','" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).SelectedValue.ToString & "','" _
                                 & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text) & "','" _
                                 & par.AggiustaData(CType(TabGeneraleT.FindControl("txtDataRiposo"), TextBox).Text) & "','" _
                                 & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtDittaGestione"), TextBox).Text) & "'," _
                                 & par.IfEmpty(CType(TabGeneraleT.FindControl("txtOreEsercizio"), TextBox).Text, "Null") & ",'" _
                                 & par.AggiustaData(CType(TabGeneraleT.FindControl("txtDataAccensione"), TextBox).Text) & "','" _
                                 & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtNumTelefonico"), TextBox).Text) & "','" _
                                 & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Text) & "','" _
                                 & CType(Tab_Termico_Certificazioni.FindControl("cmbCPI"), DropDownList).SelectedValue.ToString & "','" _
                                 & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Text) & "','" _
                                 & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Text) & "','" _
                                 & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtDittaFornitrice"), TextBox).Text) & "','" _
                                 & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtNumTelefonico2"), TextBox).Text) & "','" _
                                 & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).Text) & "')"

            par.cmd.ExecuteNonQuery()

            AggiornaImpiantiEdifici()


            '*** INSERIMENTO SCAMBIATORI DI CALORE
            Dim lstScambiatori As System.Collections.Generic.List(Of Epifani.Generatori)

            lstScambiatori = CType(HttpContext.Current.Session.Item("LSTSCAMBIATORI"), System.Collections.Generic.List(Of Epifani.Generatori))

            For Each gen As Epifani.Generatori In lstScambiatori

                par.cmd.CommandText = "insert into SISCOM_MI.GENERATORI_TERMICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE,FLUIDO_TERMOVETTORE,MARC_EFF_ENERGETICA) " _
                                    & "values (SISCOM_MI.SEQ_GENERATORI_TERMICI.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.POTENZA, "Null")) & ",'" & Strings.Left(par.PulisciStrSql(gen.NOTE), 300) & "','" _
                                        & par.PulisciStrSql(gen.FLUIDO_TERMOVETTORE) & "','" & gen.MARC_EFF_ENERGETICA & "')"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scambiatore di calore")

            Next
            '********************************


            '*** INSERIMENTO GENERATORI
            Dim lstGeneratori As System.Collections.Generic.List(Of Epifani.Generatori)

            lstGeneratori = CType(HttpContext.Current.Session.Item("LSTGENERATORI"), System.Collections.Generic.List(Of Epifani.Generatori))

            For Each gen As Epifani.Generatori In lstGeneratori

                par.cmd.CommandText = "insert into SISCOM_MI.I_TER_GENERATORI_TELE (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE,FLUIDO_TERMOVETTORE,MARC_EFF_ENERGETICA) " _
                                    & "values (SISCOM_MI.SEQ_I_TER_GENERATORI_TELE.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.POTENZA, "Null")) & ",'" & Strings.Left(par.PulisciStrSql(gen.NOTE), 300) & "','" _
                                        & par.PulisciStrSql(gen.FLUIDO_TERMOVETTORE) & "','" & gen.MARC_EFF_ENERGETICA & "')"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Generatori")

            Next
            '********************************

            '*** INSERIMENTO BRUCIATORI
            Dim lstBruciatori As System.Collections.Generic.List(Of Epifani.Bruciatori)

            lstBruciatori = CType(HttpContext.Current.Session.Item("LSTBRUCIATORI"), System.Collections.Generic.List(Of Epifani.Bruciatori))

            For Each gen As Epifani.Bruciatori In lstBruciatori

                par.cmd.CommandText = "insert into SISCOM_MI.BRUCIATORI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,CAMPO_FUNZIONAMENTO,CAMPO_FUNZIONAMENTO_MAX,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_BRUCIATORI.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.CAMPO_FUNZIONAMENTO, "Null")) & "," & par.VirgoleInPunti(par.IfEmpty(gen.CAMPO_FUNZIONAMENTO_MAX, "Null")) & ",'" & Strings.Left(par.PulisciStrSql(gen.NOTE), 300) & "')"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Bruciatori")

            Next
            '********************************

            '*** INSERIMENTO POMPE CIRCOLAZIONE
            Dim lstPompe As System.Collections.Generic.List(Of Epifani.Pompe)

            lstPompe = CType(HttpContext.Current.Session.Item("LSTPOMPE"), System.Collections.Generic.List(Of Epifani.Pompe))

            For Each gen As Epifani.Pompe In lstPompe

                par.cmd.CommandText = "insert into SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_POMPE_CIRCOLAZIONE_TERMICI.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.POTENZA, "Null")) & ",'" & Strings.Left(par.PulisciStrSql(gen.NOTE), 300) & "')"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Circolazione")

            Next
            '********************************

            '*** INSERIMENTO POMPE SOLLEVAMENTO
            Dim lstPompeS As System.Collections.Generic.List(Of Epifani.PompeS)

            lstPompeS = CType(HttpContext.Current.Session.Item("LSTPOMPES"), System.Collections.Generic.List(Of Epifani.PompeS))

            For Each gen As Epifani.PompeS In lstPompeS

                par.cmd.CommandText = "insert into SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,PORTATA,PREVALENZA) " _
                                    & "values (SISCOM_MI.SEQ_I_TER_POMPE_SOLLEVAMENTO.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.POTENZA, "Null")) & "," & par.VirgoleInPunti(par.IfEmpty(gen.PORTATA, "Null")) & "," & par.VirgoleInPunti(par.IfEmpty(gen.PREVALENZA, "Null")) & ")"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento Acque Meteoriche")

            Next
            '********************************


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


            '**** AGGIORNO I COMPONENTI

            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato

            '*** SCAMBIATORI x IL TELERISCALDAMENTO
            par.cmd.CommandText = "select SISCOM_MI.GENERATORI_TERMICI.ID,SISCOM_MI.GENERATORI_TERMICI.MODELLO," _
                        & "SISCOM_MI.GENERATORI_TERMICI.MATRICOLA,SISCOM_MI.GENERATORI_TERMICI.NOTE," _
                           & "SISCOM_MI.GENERATORI_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.GENERATORI_TERMICI.POTENZA,SISCOM_MI.GENERATORI_TERMICI.MARC_EFF_ENERGETICA,SISCOM_MI.GENERATORI_TERMICI.FLUIDO_TERMOVETTORE " _
                  & " from SISCOM_MI.GENERATORI_TERMICI " _
                  & " where SISCOM_MI.GENERATORI_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                  & " order by SISCOM_MI.GENERATORI_TERMICI.MODELLO "

            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds2 As New Data.DataSet()

            da2.Fill(ds2, "GENERATORI_TERMICI")

            CType(TabDettagliTELE.FindControl("DataGrid1"), DataGrid).DataSource = ds2
            CType(TabDettagliTELE.FindControl("DataGrid1"), DataGrid).DataBind()
            ds2.Dispose()
            '*******************************

            '*** BRUCIATORI
            par.cmd.CommandText = " select SISCOM_MI.BRUCIATORI.ID,SISCOM_MI.BRUCIATORI.MODELLO," _
                                        & "SISCOM_MI.BRUCIATORI.MATRICOLA,SISCOM_MI.BRUCIATORI.NOTE," _
                                           & "SISCOM_MI.BRUCIATORI.ANNO_COSTRUZIONE,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO_MAX " _
                                  & " from SISCOM_MI.BRUCIATORI " _
                                  & " where SISCOM_MI.BRUCIATORI.ID_IMPIANTO = " & vIdImpianto _
                                  & " order by SISCOM_MI.BRUCIATORI.MODELLO "

            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds1 As New Data.DataSet()

            da1.Fill(ds1, "BRUCIATORI")

            CType(TabDettagli1.FindControl("DataGrid1"), DataGrid).DataSource = ds1
            CType(TabDettagli1.FindControl("DataGrid1"), DataGrid).DataBind()
            ds1.Dispose()


            '*** GENERATORI
            par.cmd.CommandText = "select SISCOM_MI.I_TER_GENERATORI_TELE.ID,SISCOM_MI.I_TER_GENERATORI_TELE.MODELLO," _
                        & "SISCOM_MI.I_TER_GENERATORI_TELE.MATRICOLA,SISCOM_MI.I_TER_GENERATORI_TELE.NOTE," _
                           & "SISCOM_MI.I_TER_GENERATORI_TELE.ANNO_COSTRUZIONE,SISCOM_MI.I_TER_GENERATORI_TELE.POTENZA," _
                           & "SISCOM_MI.I_TER_GENERATORI_TELE.MARC_EFF_ENERGETICA,SISCOM_MI.I_TER_GENERATORI_TELE.FLUIDO_TERMOVETTORE " _
                  & " from SISCOM_MI.I_TER_GENERATORI_TELE " _
                  & " where SISCOM_MI.I_TER_GENERATORI_TELE.ID_IMPIANTO = " & vIdImpianto _
                  & " order by SISCOM_MI.I_TER_GENERATORI_TELE.MODELLO "

            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds3 As New Data.DataSet()

            da3.Fill(ds3, "GENERATORI_TERMICI")

            CType(TabDettagli1.FindControl("DataGrid2"), DataGrid).DataSource = ds3
            CType(TabDettagli1.FindControl("DataGrid2"), DataGrid).DataBind()
            ds3.Dispose()


            '*** POMPE CIRCOLAZIONE
            par.cmd.CommandText = "select SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO," _
                        & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MATRICOLA,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.NOTE," _
                           & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.POTENZA " _
                  & " from SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI " _
                  & " where SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                  & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO "

            Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds4 As New Data.DataSet()

            da4.Fill(ds4, "POMPE_CIRCOLAZIONE_TERMICI")

            CType(Tab_Termico_Pompe.FindControl("DataGrid3"), DataGrid).DataSource = ds4
            CType(Tab_Termico_Pompe.FindControl("DataGrid3"), DataGrid).DataBind()
            ds4.Dispose()
            '*******************************


            '*** POMPE SOLLEVALEMNTO
            par.cmd.CommandText = "select SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ID,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MODELLO," _
                        & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MATRICOLA,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ANNO_COSTRUZIONE," _
                           & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.POTENZA,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.PORTATA,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.PREVALENZA " _
                  & " from SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO " _
                  & " where SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ID_IMPIANTO = " & vIdImpianto _
                  & " order by SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MODELLO "

            Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds5 As New Data.DataSet()

            da5.Fill(ds5, "I_TER_POMPE_SOLLEVAMENTO")

            CType(Tab_Termico_Pompe.FindControl("DataGrid4"), DataGrid).DataSource = ds5
            CType(Tab_Termico_Pompe.FindControl("DataGrid4"), DataGrid).DataBind()
            ds5.Dispose()
            '*******************************

            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If

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


            par.cmd.CommandText = "update SISCOM_MI.IMPIANTI  set " _
                                         & " ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & ", ID_EDIFICIO=" & RitornaNullSeMenoUno(Me.DrLEdificio.SelectedValue.ToString) & "," _
                                         & " DESCRIZIONE='" & par.PulisciStrSql(Me.txtDenominazione.Text) & "'," _
                                         & " COD_TIPOLOGIA='TR'," _
                                         & " DITTA_COSTRUTTRICE='" & par.PulisciStrSql(Me.txtDitta.Text) & "'," _
                                         & " ANNO_COSTRUZIONE='" & par.AggiustaData(Me.txtAnnoRealizzazione.Text) & "'," _
                                         & " ANNOTAZIONI='" & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtNote"), TextBox).Text) & "'" _
                                         & " where ID = " & vIdImpianto

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "update SISCOM_MI.I_TERMICI  set " _
                                         & " ID_TIPOLOGIA_USO = " & RitornaNullSeMenoUno(Me.cmbTipoUso.SelectedValue.ToString) & ", ID_COMBUSTIBILE=" & RitornaNullSeMenoUno((CType(TabGeneraleT.FindControl("cmbCombustibile"), DropDownList).SelectedValue.ToString)) & "," _
                                         & " SERBATOIO='" & par.PulisciStrSql(CType(TabGeneraleT.FindControl("cmbTipoSerbatoio"), DropDownList).SelectedItem.Text) & "'," _
                                         & " CAPACITA=" & par.VirgoleInPunti(par.IfEmpty(CType(TabGeneraleT.FindControl("txtCapacita"), TextBox).Text, "Null")) & "," _
                                         & " POTENZA=" & par.VirgoleInPunti(par.IfEmpty(CType(TabGeneraleT.FindControl("txtPotenza"), TextBox).Text, "Null")) & "," _
                                         & " CONSUMO_MEDIO=" & par.VirgoleInPunti(par.IfEmpty(CType(TabGeneraleT.FindControl("txtConsumo"), TextBox).Text, "Null")) & "," _
                                         & " LIBRETTO='" & CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString & "'," _
                                         & " DICH_CONF_LG_46_90='" & CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).SelectedValue.ToString & "'," _
                                         & " DECR_PREFETTIZIO_SERBATOI='" & CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).SelectedValue.ToString & "'," _
                                         & " LICENZA_UTF='" & CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).SelectedValue.ToString & "'," _
                                         & " PRATICA_ISPESL='" & CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).SelectedValue.ToString & "'," _
                                         & " TRATTAMENTO_ACQUA='" & CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).SelectedValue.ToString & "'," _
                                         & " CONT_ENERGIA='" & CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).SelectedValue.ToString & "'," _
                                         & " DATA_PRATICA_ISPESL='" & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text) & "'," _
                                         & " DATA_MESSA_RIPOSO='" & par.AggiustaData(CType(TabGeneraleT.FindControl("txtDataRiposo"), TextBox).Text) & "'," _
                                         & " DITTA_GESTIONE='" & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtDittaGestione"), TextBox).Text) & "'," _
                                         & " NUM_ORE_ESERCIZIO=" & par.IfEmpty(CType(TabGeneraleT.FindControl("txtOreEsercizio"), TextBox).Text, "Null") & "," _
                                         & " DATA_PRIMA_ACCENSIONE='" & par.AggiustaData(CType(TabGeneraleT.FindControl("txtDataAccensione"), TextBox).Text) & "'," _
                                         & " TELEFONO_DITTA='" & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtNumTelefonico"), TextBox).Text) & "'," _
                                         & " DATA_DISMISSIONE_CT='" & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Text) & "'," _
                                         & " PRATICA_VVF='" & CType(Tab_Termico_Certificazioni.FindControl("cmbCPI"), DropDownList).SelectedValue.ToString & "'," _
                                         & " DATA_RILASCIO_VVF='" & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Text) & "'," _
                                         & " DATA_SCADENZA_VVF='" & par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Text) & "'," _
                                         & " DITTA_FORNITRICE='" & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtDittaFornitrice"), TextBox).Text) & "'," _
                                         & " TELEFONO_DITTA_FORNITRICE='" & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtNumTelefonico2"), TextBox).Text) & "'," _
                                         & " CONTATORE_PDR='" & par.PulisciStrSql(CType(TabGeneraleT.FindControl("txtContatoreDPR"), TextBox).Text) & "' " _
                                         & " where ID = " & vIdImpianto


            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")


            AggiornaImpiantiEdifici()

            par.myTrans.Commit() '

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

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans
            'par.myTrans.Rollback()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            'HttpContext.Current.Session.Remove("LSTGENERATORI")
            'HttpContext.Current.Session.Remove("LSTBRUCIATORI")


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

            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans
            'par.myTrans.Rollback()


            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            'HttpContext.Current.Session.Remove("LSTGENERATORI")
            'HttpContext.Current.Session.Remove("LSTBRUCIATORI")


            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            ElseIf Strings.Len(sVerifiche) > 0 Then
                Response.Write("<script>location.replace('RisultatiVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
            Else
                Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "');</script>")
                'Response.Write("<script>document.location.href=""RisultatiImpianti.aspx""</script>")
                'Response.Write("<script>history.go(" & txtindietro.Text & ");</script>")
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

    Private Sub AbilitazioneOggetti(ByVal ValorePass As Boolean)
        Try

            cmbComplesso.Enabled = ValorePass
            DrLEdificio.Enabled = ValorePass

            txtDenominazione.ReadOnly = Not (ValorePass)
            txtCodImpianto.ReadOnly = True

            cmbTipoUso.Enabled = ValorePass

        Catch ex As Exception

        End Try

    End Sub

    Private Sub AggiornaImpiantiEdifici()
        Dim i As Integer
        Dim oDataGridItem As DataGridItem
        Dim bTrovato As Boolean = False

        Try


            par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_EDIFICI where ID_IMPIANTO = " & vIdImpianto
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_UI where ID_IMPIANTO = " & vIdImpianto
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            'For i = 0 To CType(TabGeneraleT.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
            '    If CType(TabGeneraleT.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True Then
            '        par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_EDIFICI (ID_IMPIANTO,ID_EDIFICIO) values " _
            '                   & "(" & vIdImpianto & "," & CType(TabGeneraleT.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value & ")"

            '        par.cmd.ExecuteNonQuery()
            '        par.cmd.CommandText = ""
            '    End If
            'Next

            'Loop EDIFICI
            For Each oDataGridItem In CType(TabGeneraleT.FindControl("DataGridEdifici"), DataGrid).Items

                If par.IfEmpty(oDataGridItem.Cells(2).Text, 0) > 0 Then
                    par.cmd.CommandText = " insert into SISCOM_MI.IMPIANTI_EDIFICI (ID_IMPIANTO,ID_EDIFICIO) " _
                                        & " values (" & vIdImpianto & "," _
                                                      & oDataGridItem.Cells(0).Text & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""


                    bTrovato = False
                    If lstUnita.Count > 0 Then

                        For i = 0 To lstUnita.Count - 1

                            If lstUnita(i).ID_EDIFICIO = oDataGridItem.Cells(0).Text Then

                                par.cmd.CommandText = " insert into SISCOM_MI.IMPIANTI_UI (ID_IMPIANTO,ID_EDIFICIO,ID_UNITA) " _
                                                    & " values (" & vIdImpianto & "," _
                                                                  & oDataGridItem.Cells(0).Text & "," _
                                                                  & lstUnita(i).ID_UNITA & ")"

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                bTrovato = True
                            End If
                        Next i
                    End If
                End If
            Next

        Catch ex As Exception
        End Try


    End Sub

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
                End If
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
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

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO TELERISCALDAMENTO"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Impianto Teleriscaldamento del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
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

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

End Class



