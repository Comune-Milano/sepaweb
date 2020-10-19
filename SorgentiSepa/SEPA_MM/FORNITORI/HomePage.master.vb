Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing

Partial Class FORNITORI_HomePage
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global
    Private Sub gestioneFornitoreEsterno()
        par.cmd.CommandText = "SELECT MOD_FO_ID_FO FROM SEPA.OPERATORI WHERE OPERATORI.ID=" & Session.Item("ID_OPERATORE")
        Dim idOperatore As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        idFornitore.Value = idOperatore
        idDirettoreLavori.Value = 0
    End Sub

    Private Sub gestioneDirettoreLavori()
        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        idDirettoreLavori.Value = idOperatore
        idFornitore.Value = 0
    End Sub

    Protected Sub OnCallbackUpdate(sender As Object, e As RadNotificationEventArgs)
        'If newMsgs = 5 OrElse newMsgs = 7 OrElse newMsgs = 8 OrElse newMsgs = 9 Then
        '    newMsgs = 0
        'End If
        'newMsgs = 100
        'lbl.Text = [String].Concat(New Object() {"You have ", newMsgs, " new messages!"})
        'RadNotification1.Value = newMsgs.ToString()
        'RicavaNotifiche()
    End Sub

    Public Property lOperatoreDitta() As Long
        Get
            If Not (ViewState("par_llOperatoreDitta") Is Nothing) Then
                Return CLng(ViewState("par_llOperatoreDitta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_llOperatoreDitta") = value
        End Set

    End Property

    Public Property lOperatoreTIPO() As Long
        Get
            If Not (ViewState("par_lOperatoreTIPO") Is Nothing) Then
                Return CLng(ViewState("par_lOperatoreTIPO"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lOperatoreTIPO") = value
        End Set

    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("MOD_FORNITORI") <> "1" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.ID = "MasterPage"

            If Not IsPostBack Then
               
                FormCompleto()
                Try
                    If par.OracleConn.State = Data.ConnectionState.Open Then
                        Exit Sub
                    Else
                        par.OracleConn.Open()
                        par.cmd = par.OracleConn.CreateCommand()
                    End If
                    par.cmd.CommandText = "select ragione_sociale from siscom_mi.fornitori,operatori where fornitori.id=operatori.mod_fo_id_fo And operatori.id=" & Session.Item("id_operatore")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        If myReader.Read Then
                            lblDitta.Text = par.IfNull(myReader("ragione_sociale"), "")
                        End If
                    Else
                        lblDitta.Text = "---"
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader("MOD_FORNITORI_RDO"), "") = "1" Then
                            Session.Add("MOD_FORNITORI_RDO", "1")
                        Else
                            Session.Add("MOD_FORNITORI_RDO", "0")
                        End If
                        If par.IfNull(myReader("MOD_FORNITORI_ODL"), "") = "1" Then
                            Session.Add("MOD_FORNITORI_ODL", "1")
                        Else
                            Session.Add("MOD_FORNITORI_ODL", "0")
                        End If
                        If par.IfNull(myReader("MOD_FORNITORI_RPT"), "") = "1" Then
                            Session.Add("MOD_FORNITORI_RPT", "1")
                        Else
                            Session.Add("MOD_FORNITORI_RPT", "0")
                        End If
                        If par.IfNull(myReader("MOD_FORNITORI_PARAM"), "") = "1" Then
                            Session.Add("MOD_FORNITORI_PARAM", "1")
                        Else
                            Session.Add("MOD_FORNITORI_PARAM", "0")
                        End If
                        If par.IfNull(myReader("MOD_FORNITORI_LOG"), "") = "1" Then
                            Session.Add("MOD_FORNITORI_LOG", "1")
                        Else
                            Session.Add("MOD_FORNITORI_LOG", "0")
                        End If
                        If Session.Item("MOD_FORNITORI_LOG") = "1" Then
                            NavigationMenu.FindItemByValue("RptLogEventi").Visible = True
                            NavigationMenu.FindItemByValue("RptLogAccessi").Visible = True
                        End If

                        lOperatoreDitta = par.IfNull(myReader("MOD_FO_ID_FO"), -1)
                        lOperatoreTIPO = 0
                        If par.IfNull(myReader("LIVELLO_WEB"), "0") = "1" Then
                            lOperatoreTIPO = 1 'ADMIN
                            NavigationMenu.FindItemByValue("RptConsuntivi").Visible = True
                        Else
                            If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
                                lOperatoreTIPO = 2 'STANDARD
                                NavigationMenu.FindItemByValue("Manuale").Visible = True
                                NavigationMenu.FindItemByValue("Manuale1").Visible = False
                                NavigationMenu.FindItemByValue("RptConsuntivi").Visible = True
                                
                            Else
                                NavigationMenu.FindItemByValue("Manuale").Visible = False
                                NavigationMenu.FindItemByValue("Manuale1").Visible = True
                                par.cmd.CommandText = "SELECT DISTINCT APPALTI.ID " _
                                                    & "FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                                                    & "WHERE Appalti.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO And APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO = EDIFICI.ID " _
                                                    & "And EDIFICI.ID_BM = BUILDING_MANAGER_OPERATORI.ID_BM And BUILDING_MANAGER_OPERATORI.ID_OPERATORE = " & Session.Item("id_operatore")
                                Dim myReaderBM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderBM.HasRows = True Then
                                    lOperatoreTIPO = 3 'BUILDING MANAGER
                                End If
                                myReaderBM.Close()
                                If lOperatoreTIPO = 0 Then
                                    par.cmd.CommandText = "SELECT DISTINCT APPALTI.ID FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_DL WHERE APPALTI.ID=APPALTI_DL.ID_GRUPPO And APPALTI_DL.ID_OPERATORE=" & Session.Item("id_operatore")
                                    Dim myReaderDL As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderDL.HasRows = True Then
                                        lOperatoreTIPO = 4 'DIRRETORE LAVORI
                                    End If
                                    myReaderDL.Close()
                                End If
                            End If
                        End If
                        If Session.Item("MOD_FORNITORI_SLE") = "1" Then

                        End If
                    End If
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
                Select Case lOperatoreTIPO
                    Case 1
                        lblOperatore.Text = Session.Item("OPERATORE") & " (ADMIN)"
                    Case 2
                        lblOperatore.Text = Session.Item("OPERATORE") & " (STANDARD)"
                    Case 3
                        lblOperatore.Text = Session.Item("OPERATORE") & " (BUILDING MANAGER)"
                    Case 4
                        lblOperatore.Text = Session.Item("OPERATORE") & " (DIRETTORE LAVORI)"
                    Case Else
                        lblOperatore.Text = Session.Item("OPERATORE")
                End Select


                If Session.Item("MLoading") = "" Then
                    Session.Add("MLoading", "1")
                End If



            End If
            CaricaAbilitazini()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Master - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function CaricaAbilitazini()
        If Session.Item("MOD_FORNITORI_RDO") = "0" Then
            NavigationMenu.FindItemByValue("Segnalazioni").Visible = False
            'NavigationMenu.FindItemByValue("mSeparator1").Visible = False
        End If
        If Session.Item("MOD_FORNITORI_ODL") = "0" Then
            NavigationMenu.FindItemByValue("Ordini di lavoro MM").Visible = False
            'NavigationMenu.FindItemByValue("Programma attivita' MM").Visible = False
            'NavigationMenu.FindItemByValue("mSeparator2").Visible = False

            NavigationMenu.FindItemByValue("Ordini di lavoro DI").Visible = False
            NavigationMenu.FindItemByValue("mSeparator4").Visible = False
        Else
            If Session.Item("LIVELLO_WEB") <> "1" Then
                If lOperatoreDitta = 0 Then
                    NavigationMenu.FindItemByValue("Ordini di lavoro DI").Visible = False
                    NavigationMenu.FindItemByValue("mSeparator4").Visible = False
                Else
                    NavigationMenu.FindItemByValue("Ordini di lavoro MM").Visible = False
                    ' NavigationMenu.FindItemByValue("mSeparator2").Visible = False
                End If
            End If
        End If
        If Session.Item("MOD_FORNITORI_RPT") = "0" Then
            NavigationMenu.FindItemByValue("Reportistica").Visible = False
            NavigationMenu.FindItemByValue("mSeparator3").Visible = False
        End If
        If Session.Item("MOD_FORNITORI_PARAM") = "0" Then
            NavigationMenu.FindItemByValue("Parametri").Visible = False
            NavigationMenu.FindItemByValue("mSeparator5").Visible = False
        End If

        If Session.Item("MOD_FORNITORI_SLE") = "1" Then
            NavigationMenu.FindItemByValue("Parametri").Visible = False
            NavigationMenu.FindItemByValue("mSeparator5").Visible = False
        End If

    End Function

    Private Sub FormCompleto()
        Dim contenuto As ContentPlaceHolder = Me.Page.Controls.Item(0).FindControl("Form1").FindControl("CPContenuto")
        For Each controllo In contenuto.Controls
            If controllo.controls.count > 0 Then
                Dim collezioneControlli As Object = controllo.controls
                For Each controllo2 In collezioneControlli
                    If controllo2.controls.count > 0 Then
                        Dim collezioneControlli2 As Object = controllo2.controls
                        For Each controllo3 In collezioneControlli2
                            If TypeOf controllo3 Is TextBox Then
                                DirectCast(controllo3, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is DropDownList Then
                                DirectCast(controllo3, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is CheckBoxList Then
                                DirectCast(controllo3, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is CheckBox Then
                                DirectCast(controllo3, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is RadComboBox Then
                                DirectCast(controllo3, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is RadDatePicker Then
                                DirectCast(controllo3, RadDatePicker).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

                            End If
                        Next
                    End If
                    If TypeOf controllo2 Is TextBox Then
                        DirectCast(controllo2, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is DropDownList Then
                        DirectCast(controllo2, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is CheckBoxList Then
                        DirectCast(controllo2, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is RadComboBox Then
                        DirectCast(controllo2, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is CheckBox Then
                        DirectCast(controllo2, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is RadDatePicker Then
                        DirectCast(controllo2, RadDatePicker).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

                    End If
                Next
            End If
            If TypeOf controllo Is TextBox Then
                DirectCast(controllo, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is DropDownList Then
                DirectCast(controllo, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is CheckBoxList Then
                DirectCast(controllo, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is RadComboBox Then
                DirectCast(controllo, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is CheckBox Then
                DirectCast(controllo, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is RadDatePicker Then
                DirectCast(controllo, RadDatePicker).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
    End Sub
End Class

