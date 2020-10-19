Imports System.Collections

Partial Class Tab_Termico_Unita
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Dim lstEdificiCT As System.Collections.Generic.List(Of Epifani.EdificiCT)
    Dim lstUnita As System.Collections.Generic.List(Of Epifani.ListaUI)

    Dim lstEdificiCT_Extra As System.Collections.Generic.List(Of Epifani.EdificiCT)
    Dim lstUI_Extra As System.Collections.Generic.List(Of Epifani.ListaUI)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Expires = 0

            lstEdificiCT = CType(HttpContext.Current.Session.Item("LST_EDIFICI_CT"), System.Collections.Generic.List(Of Epifani.EdificiCT))
            lstUnita = CType(HttpContext.Current.Session.Item("LST_UI"), System.Collections.Generic.List(Of Epifani.ListaUI))

            lstEdificiCT_Extra = CType(HttpContext.Current.Session.Item("LST_EDIFICI_CT_EXTRA"), System.Collections.Generic.List(Of Epifani.EdificiCT))
            lstUI_Extra = CType(HttpContext.Current.Session.Item("LST_UI_EXTRA"), System.Collections.Generic.List(Of Epifani.ListaUI))


            If Not IsPostBack Then




                If Request.QueryString("ED") <> "" Then
                    IdEdificio = Request.QueryString("ED")
                End If

                If Request.QueryString("TIPO") <> "" Then
                    Me.txtTipo.Value = Request.QueryString("TIPO")
                End If

                Dim sID As String
                sID = Request.QueryString("ID")

                BindGrid_UI()
                SettaUnita()


                'If Request.QueryString("IDCONN") <> "" Then
                '    IdConnessione = Request.QueryString("IDCONN")
                'End If



                'Me.Session.Add("MODIFYMODAL", 0)


                'vIdManutenzione = 0
                'If Request.QueryString("ID_MANUTENZIONE") <> "" Then
                '    vIdManutenzione = Request.QueryString("ID_MANUTENZIONE")
                'End If


                ' CONNESSIONE DB
                'IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text
                'Me.txtIdConnessione.Text = IdConnessione

                'If par.OracleConn.State = Data.ConnectionState.Open Then
                '    Response.Write("IMPOSSIBILE VISUALIZZARE")
                '    Exit Sub
                'Else
                '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                '    par.SettaCommand(par)
                '    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '    '‘‘par.cmd.Transaction = par.myTrans
                'End If

                'UNITA DI MISURA
                'SettaMisure()

                'BindGrid_Consuntivo()

            End If


            Dim CTRL As Control

            '*** FORM PRINCIPALE
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            If Request.QueryString("IDVISUAL") = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Public Property IdConnessione() As String
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

    Public Property IdEdificio() As Long
        Get
            If Not (ViewState("par_IdEdificio") Is Nothing) Then
                Return CLng(ViewState("par_IdEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdEdificio") = value
        End Set

    End Property





    'INTERVENTI GRID1
    Private Sub BindGrid_UI()
        Dim StringaSql As String
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            'PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            'par.SettaCommand(par)
            'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans

            'EDIFICI.DENOMINAZIONE
            'SISCOM_MI.EDIFICI.COD_EDIFICIO

            StringaSql = "select UNITA_IMMOBILIARI.ID," _
                            & "SCALE_EDIFICI.DESCRIZIONE as SCALA," _
                            & "TIPO_LIVELLO_PIANO.DESCRIZIONE as PIANO," _
                            & "UNITA_IMMOBILIARI.INTERNO as INTERNO, " _
                            & " ( " _
                                        & " select sum(VALORE) from SISCOM_MI.DIMENSIONI " _
                                        & " where ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID " _
                                        & "   and COD_TIPOLOGIA='SUP_NETTA') as MQ, " _
                            & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE as TIPOLOGIA, " _
                            & "INTESTATARI_UI.INTESTATARIO as INTESTATARIO  " _
                      & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI" _
                & " where UNITA_IMMOBILIARI.ID_EDIFICIO=" & IdEdificio _
                & "   and UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL'" _
                & "   and UNITA_IMMOBILIARI.ID_SCALA                =SCALE_EDIFICI.ID (+) " _
                & "   and UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO  =TIPO_LIVELLO_PIANO.COD (+) " _
                & "   and UNITA_IMMOBILIARI.ID                      =INTESTATARI_UI.ID_UI (+) " _
                & "   and UNITA_IMMOBILIARI.COD_TIPOLOGIA           =TIPOLOGIA_UNITA_IMMOBILIARI.COD (+) " _
                & " order by SCALA,PIANO asc"


            par.cmd.CommandText = StringaSql

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UNITA_IMMOBILIARI")


            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            ds.Dispose()

            par.OracleConn.Close()

            'par.cmd.CommandText = "select SUM(PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI  where ID_MANUTENZIONI_INTERVENTI =" & vIdManutenzione
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            'myReader1 = par.cmd.ExecuteReader

            'If myReader1.Read Then
            '    lbl_Tot_Cons.Text = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
            'End If
            'myReader1.Close()

            'par.cmd.CommandText = "select SUM(PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI  where COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE' and ID_MANUTENZIONI_INTERVENTI =" & vIdManutenzione
            'myReader1 = par.cmd.ExecuteReader

            'If myReader1.Read Then
            '    lbl_Tot_Rimborsi.Text = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
            'End If
            'myReader1.Close()

        Catch ex As Exception

            If FlagConnessione = True Then par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Private Sub SettaUnita()
        Dim j As Integer
        Dim Trovato As Boolean

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Trovato = False

        For Each oDataGridItem In DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")


            If Me.txtTipo.Value = "EXTRA" Then
                For j = 0 To lstUI_Extra.Count - 1
                    If lstUI_Extra(j).ID_EDIFICIO = IdEdificio And lstUI_Extra(j).ID_UNITA = oDataGridItem.Cells(0).Text Then
                        chkExport.Checked = True
                        Trovato = True
                        Exit For
                    End If
                Next j
            Else
                For j = 0 To lstUnita.Count - 1
                    If lstUnita(j).ID_EDIFICIO = IdEdificio And lstUnita(j).ID_UNITA = oDataGridItem.Cells(0).Text Then
                        chkExport.Checked = True
                        Trovato = True
                        Exit For
                    End If
                Next j
            End If

        Next

        If Trovato = False Then
            For Each oDataGridItem In DataGrid1.Items
                chkExport = oDataGridItem.FindControl("CheckBox1")
                chkExport.Checked = True
            Next
        End If

    End Sub



    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "1"
        Response.Write("<script>window.close();</script>")

    End Sub





    Private Sub FrmSolaLettura()
        Try

            Me.btnSelTutti.Visible = False
            Me.btnDeselTutti.Visible = False
            Me.imgProcedi.Visible = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub




    Protected Sub btnSelTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")
            chkExport.Checked = True
        Next

    End Sub

    Protected Sub btnDeselTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim Trovato As Boolean
        Dim i As Integer

        Dim gen As Epifani.ListaUI

        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")
            chkExport.Checked = False

            Trovato = False

            If Me.txtTipo.Value = "EXTRA" Then
                For Each gen In lstUI_Extra
                    If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = True Then
                    i = 0
                    For Each gen In lstUI_Extra
                        If gen.ID_UNITA = oDataGridItem.Cells(0).Text Then

                            lstUI_Extra.RemoveAt(i)
                            Exit For
                        End If
                        i = i + 1
                    Next
                    gen = Nothing

                End If
            Else

                For Each gen In lstUnita
                    If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = True Then
                    i = 0
                    For Each gen In lstUnita
                        If gen.ID_UNITA = oDataGridItem.Cells(0).Text Then

                            lstUnita.RemoveAt(i)
                            Exit For
                        End If
                        i = i + 1
                    Next
                    gen = Nothing

                End If
            End If
        Next


    End Sub

    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim Trovato As Boolean
        Dim i As Integer
        Dim gen As Epifani.ListaUI


        Trovato = False
        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")

            If chkExport.Checked Then
                Trovato = True
                Exit For
            End If
        Next

        If Trovato = False Then
            Response.Write("<script>alert('Selezionare almeno una Unità Immobiliare!');</script>")
            Exit Sub
        End If

        Trovato = False

        If Me.txtTipo.Value = "EXTRA" Then
            For i = 0 To lstEdificiCT_Extra.Count - 1
                If lstEdificiCT_Extra.Item(i).ID = IdEdificio Then
                    lstEdificiCT_Extra.Item(i).TOTALE_UI_AL = 0
                    lstEdificiCT_Extra.Item(i).TOTALE_MQ_AL = 0
                    Exit For
                End If
            Next

            For Each oDataGridItem In Me.DataGrid1.Items

                chkExport = oDataGridItem.FindControl("CheckBox1")

                If chkExport.Checked Then
                    'TRUE ************************************************************

                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstUI_Extra
                        If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next

                    If Trovato = False Then
                        gen = New Epifani.ListaUI(IdEdificio, oDataGridItem.Cells(0).Text)
                        lstUI_Extra.Add(gen)
                        gen = Nothing
                    End If


                    For i = 0 To lstEdificiCT_Extra.Count - 1
                        If lstEdificiCT_Extra.Item(i).ID = IdEdificio Then
                            lstEdificiCT_Extra.Item(i).TOTALE_UI_AL = lstEdificiCT_Extra.Item(i).TOTALE_UI_AL + 1
                            lstEdificiCT_Extra.Item(i).TOTALE_MQ_AL = lstEdificiCT_Extra.Item(i).TOTALE_MQ_AL + oDataGridItem.Cells(4).Text
                            lstEdificiCT_Extra.Item(i).CHK = True
                            Exit For
                        End If
                    Next
                Else
                    'FALSE *****************************************************
                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstUI_Extra
                        If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next

                    If Trovato = True Then
                        i = 0
                        For Each gen In lstUI_Extra
                            If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then

                                lstUI_Extra.RemoveAt(i)
                                Exit For
                            End If
                            i = i + 1
                        Next
                        gen = Nothing

                        'Dim indice As Integer = 0
                        'For Each gen In lstListaRapporti
                        '    gen.ID = indice
                        '    indice += 1
                        'Next

                    End If
                End If
            Next
        Else

            For i = 0 To lstEdificiCT.Count - 1
                If lstEdificiCT.Item(i).ID = IdEdificio Then
                    lstEdificiCT.Item(i).TOTALE_UI_AL = 0
                    lstEdificiCT.Item(i).TOTALE_MQ_AL = 0
                    Exit For
                End If
            Next

            For Each oDataGridItem In Me.DataGrid1.Items

                chkExport = oDataGridItem.FindControl("CheckBox1")

                If chkExport.Checked Then
                    'TRUE ************************************************************

                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstUnita
                        If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next

                    If Trovato = False Then
                        gen = New Epifani.ListaUI(IdEdificio, oDataGridItem.Cells(0).Text)
                        lstUnita.Add(gen)
                        gen = Nothing
                    End If

                    For i = 0 To lstEdificiCT.Count - 1
                        If lstEdificiCT.Item(i).ID = IdEdificio Then
                            lstEdificiCT.Item(i).TOTALE_UI_AL = lstEdificiCT.Item(i).TOTALE_UI_AL + 1
                            lstEdificiCT.Item(i).TOTALE_MQ_AL = lstEdificiCT.Item(i).TOTALE_MQ_AL + oDataGridItem.Cells(4).Text
                            lstEdificiCT.Item(i).CHK = True
                            Exit For
                        End If
                    Next
                Else
                    'FALSE *****************************************************
                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstUnita
                        If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next

                    If Trovato = True Then
                        i = 0
                        For Each gen In lstUnita
                            If gen.ID_EDIFICIO = IdEdificio And gen.ID_UNITA = oDataGridItem.Cells(0).Text Then

                                lstUnita.RemoveAt(i)
                                Exit For
                            End If
                            i = i + 1
                        Next
                        gen = Nothing

                        'Dim indice As Integer = 0
                        'For Each gen In lstListaRapporti
                        '    gen.ID = indice
                        '    indice += 1
                        'Next

                    End If
                End If
            Next

        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "1"

        Session.Add("TERMICO_UNITA", "1")

        'Response.Write("<script>opener.document.getElementById('form1').txtModUI.value='1';opener.document.getElementById('form1').submit();window.close();</script>")

        ' Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
        ' Response.Write("<script>window.close();</script>")

        Response.Write("<script>window.close();</script>")

    End Sub


End Class
