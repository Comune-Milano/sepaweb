Imports System.Collections

Partial Class Tab_Termico_Edifici
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Dim lstEdificiCT_Extra As System.Collections.Generic.List(Of Epifani.EdificiCT)
    Dim lstUI_Extra As System.Collections.Generic.List(Of Epifani.ListaUI)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        lstEdificiCT_Extra = CType(HttpContext.Current.Session.Item("LST_EDIFICI_CT_EXTRA"), System.Collections.Generic.List(Of Epifani.EdificiCT))
        lstUI_Extra = CType(HttpContext.Current.Session.Item("LST_UI_EXTRA"), System.Collections.Generic.List(Of Epifani.ListaUI))


        Try
            If Not IsPostBack Then

                vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Property vIdImpianto() As Long
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

    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property



    Protected Sub DataGridEdificiExtra_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridEdificiExtra.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Edifici_txtSel').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Termico_Edifici_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Edifici_txtSel').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Termico_Edifici_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampi() As Boolean
       
        ControlloCampi = True

        If Me.cmbComplesso.SelectedValue = "-1" Then
            Response.Write("<script>alert('Selezionare il complesso!');</script>")
            ControlloCampi = False
            Exit Function
        End If

    End Function

    Protected Sub btn_Inserisci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci.Click
        If ControlloCampi() = False Then
            txtAppareE.Text = "1"
            Exit Sub
        End If

        Me.SalvaEdificio()

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSel.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_Chiudi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSel.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaEdificio()
        Dim StringaSql As String
        Dim FlagConnessione As Boolean

        Dim SommaUI As Integer = 0
        Dim SommaMQ As Decimal = 0
        Dim SommaEdifici As Integer = 0

        Dim oDataGridItem As DataGridItem
        'Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim Trovato As Boolean
        Dim i As Integer


        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            StringaSql = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE," _
                                            & "(select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and (COD_TIPOLOGIA <> 'K' AND COD_TIPOLOGIA <> 'B' AND COD_TIPOLOGIA <> 'C' AND COD_TIPOLOGIA <> 'H' AND COD_TIPOLOGIA <> 'I') ) as TOTALE_UI," _
                                            & "(select SUM (( " _
                                                    & " select sum(VALORE) from SISCOM_MI.DIMENSIONI " _
                                                    & " where ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID and COD_TIPOLOGIA='SUP_NETTA')) " _
                                             & " from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID and COD_TIPOLOGIA='AL') as TOTALE_MQ, " _
                                             & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.showModalDialog(''Tab_Termico_Unita.aspx?TIPO=EXTRA&IDVISUAL=" & CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text & "&ED='||SISCOM_MI.EDIFICI.ID||''',''Dettagli'',''dialogWidth:800px;dialogHeight:550px;'');window.form1.submit();£>Seleziona Unità</a>','$','&'),'£','" & Chr(34) & "') as  UNITA  " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString

            If Me.cmbEdificio.SelectedValue <> "-1" Then
                StringaSql = StringaSql & " and ID=" & Me.cmbEdificio.SelectedValue.ToString
            End If

            StringaSql = StringaSql & " order by DENOMINAZIONE asc"

            PAR.cmd.CommandText = StringaSql

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

            While myReader1.Read

                Trovato = False
                Dim gen As Epifani.EdificiCT

                For Each gen In lstEdificiCT_Extra
                    If gen.ID = PAR.IfNull(myReader1("ID"), -1) Then
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = False Then
                    gen = New Epifani.EdificiCT(PAR.IfNull(myReader1("ID"), -1), PAR.IfNull(myReader1("DENOMINAZIONE"), " "), 0, PAR.IfNull(myReader1("TOTALE_UI"), 0), 0, PAR.IfNull(myReader1("TOTALE_MQ"), 0), PAR.IfNull(myReader1("UNITA"), ""), True)
                    lstEdificiCT_Extra.Add(gen)


                    '********INIZIO

                    StringaSql = "select UNITA_IMMOBILIARI.ID, " _
                                    & " ( " _
                                        & " select sum(VALORE) from SISCOM_MI.DIMENSIONI " _
                                        & " where ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID " _
                                        & "   and COD_TIPOLOGIA='SUP_NETTA') as MQ " _
                              & " from SISCOM_MI.UNITA_IMMOBILIARI " _
                              & " where UNITA_IMMOBILIARI.ID_EDIFICIO=" & PAR.IfNull(myReader1("ID"), -1) _
                              & "   and (UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'K' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'B' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'C' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'H' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'I') "

                    PAR.cmd.CommandText = StringaSql
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                    While myReader2.Read

                        ' CONTROLLO SE GIA INSERITO nella LISTA
                        Dim genUI As Epifani.ListaUI
                        Trovato = False
                        For Each genUI In lstUI_Extra
                            If genUI.ID_EDIFICIO = PAR.IfNull(myReader1("ID"), -1) And genUI.ID_UNITA = PAR.IfNull(myReader2("ID"), -1) Then
                                Trovato = True
                                Exit For
                            End If
                        Next

                        If Trovato = False Then
                            genUI = New Epifani.ListaUI(PAR.IfNull(myReader1("ID"), -1), PAR.IfNull(myReader2("ID"), -1))
                            lstUI_Extra.Add(genUI)
                            genUI = Nothing
                        End If


                        For i = 0 To lstEdificiCT_Extra.Count - 1
                            If lstEdificiCT_Extra.Item(i).ID = PAR.IfNull(myReader1("ID"), -1) Then

                                lstEdificiCT_Extra.Item(i).TOTALE_UI_AL = lstEdificiCT_Extra.Item(i).TOTALE_UI_AL + 1
                                lstEdificiCT_Extra.Item(i).TOTALE_MQ_AL = lstEdificiCT_Extra.Item(i).TOTALE_MQ_AL + PAR.IfNull(myReader2("MQ"), 0)

                                lstEdificiCT_Extra.Item(i).CHK = True
                                Exit For
                            End If
                        Next
                    End While
                    myReader2.Close()
                    'FINE

                End If

                gen = Nothing

            End While
            myReader1.Close()

            DataGridEdificiExtra.DataSource = Nothing
            DataGridEdificiExtra.DataSource = lstEdificiCT_Extra
            DataGridEdificiExtra.DataBind()


            For Each oDataGridItem In DataGridEdifici.Items
                'chkExport = oDataGridItem.FindControl("CheckBox1")

                'If chkExport.Checked = True Then
                If PAR.IfEmpty(oDataGridItem.Cells(2).Text, 0) > 0 Then
                    SommaEdifici = SommaEdifici + 1
                End If
            Next

            For Each oDataGridItem In DataGridEdificiExtra.Items

                SommaUI = SommaUI + oDataGridItem.Cells(2).Text
                SommaMQ = SommaMQ + oDataGridItem.Cells(4).Text
                SommaEdifici = SommaEdifici + 1
            Next


            CType(Me.Page.FindControl("txtSommaEdifici"), HiddenField).Value = SommaEdifici
            CType(Me.Page.FindControl("TabGenerale").FindControl("txtTotEdifici"), TextBox).Text = SommaEdifici

            Me.txtTotUI_Extra.Text = SommaUI
            Me.txtTotMq_Extra.Text = IsNumFormat(SommaMQ, "", "##,##0.00")


            SommaMQ = SommaMQ + Me.txtTotMq.Text
            SommaUI = SommaUI + Me.txtTotUI.Text

            CType(Me.Page.FindControl("TabGenerale").FindControl("txtTotUI"), TextBox).Text = SommaUI
            CType(Me.Page.FindControl("TabGenerale").FindControl("txtTotMq"), TextBox).Text = IsNumFormat(SommaMQ, "", "##,##0.00")


            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"


        Catch ex As Exception
            If FlagConnessione = True Then PAR.OracleConn.Close()

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Dim i As Integer
        Dim SommaUI As Integer = 0
        Dim SommaMQ As Decimal = 0
        Dim SommaEdifici As Integer = 0

        Dim oDataGridItem As DataGridItem


        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareE.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    Dim gen As Epifani.EdificiCT

                    For Each gen In lstEdificiCT_Extra
                        If gen.ID = Me.txtIdComponente.Text Then

                            lstEdificiCT_Extra.RemoveAt(i)
                            Exit For
                        End If
                        i = i + 1
                    Next

                    'Dim indice As Integer = 0
                    'For Each griglia As Epifani.EdificiCT In lstEdificiCT_Extra
                    '    griglia.ID = indice
                    '    indice += 1
                    'Next


                    DataGridEdificiExtra.DataSource = Nothing
                    DataGridEdificiExtra.DataSource = lstEdificiCT_Extra
                    DataGridEdificiExtra.DataBind()

                    SommaEdifici = Val(CType(Me.Page.FindControl("txtSommaEdifici"), HiddenField).Value) - 1
                    CType(Me.Page.FindControl("txtSommaEdifici"), HiddenField).Value = SommaEdifici
                    CType(Me.Page.FindControl("TabGenerale").FindControl("txtTotEdifici"), TextBox).Text = SommaEdifici


                    For Each oDataGridItem In DataGridEdificiExtra.Items

                        SommaUI = SommaUI + oDataGridItem.Cells(2).Text
                        SommaMQ = SommaMQ + oDataGridItem.Cells(4).Text
                    Next


                    Me.txtTotUI_Extra.Text = SommaUI
                    Me.txtTotMq_Extra.Text = IsNumFormat(SommaMQ, "", "##,##0.00")

                    SommaMQ = SommaMQ + Me.txtTotMq.Text
                    SommaUI = SommaUI + Me.txtTotUI.Text

                    CType(Me.Page.FindControl("TabGenerale").FindControl("txtTotUI"), TextBox).Text = SommaUI
                    CType(Me.Page.FindControl("TabGenerale").FindControl("txtTotMq"), TextBox).Text = IsNumFormat(SommaMQ, "", "##,##0.00")


                    txtSel.Text = ""
                    txtIdComponente.Text = ""

                End If
            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnAgg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgg.Click
        Try

            cmbComplesso.SelectedValue = "-1"

            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))


        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub




    Private Sub FrmSolaLettura()
        Try

            Me.btnAgg.Visible = False
            Me.btnElimina.Visible = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
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


    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        Dim FlagConnessione As Boolean

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        If Me.cmbComplesso.SelectedValue <> "-1" Then

            Try
                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Me.cmbEdificio.Items.Clear()
                Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

                PAR.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                       & " from SISCOM_MI.EDIFICI " _
                                       & " where ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString _
                                       & " order by DENOMINAZIONE asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                While myReader1.Read

                    Me.cmbEdificio.Items.Add(New ListItem(PAR.IfNull(myReader1("DENOMINAZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()
                Me.cmbEdificio.SelectedValue = "-1"
                '********************************

            Catch ex As Exception
                If FlagConnessione = True Then PAR.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try

        Else
            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))
        End If


    End Sub


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

End Class
