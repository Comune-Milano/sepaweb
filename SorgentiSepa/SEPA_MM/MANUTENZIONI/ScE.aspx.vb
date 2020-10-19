Imports System.Collections.Generic

Partial Class ScC
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public DictMat As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAna As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictInerv As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAnomalie As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)
    Public DictElementi As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)
    'Aggiunta STATO DEGRADO 08/07/2009
    Public DictDegrado As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../LoginManutenzioni.aspx""</script>")
            Exit Sub
        End If

        If Not IsPostBack Then
            If String.IsNullOrEmpty(Session.Item("ID")) = True Then
                '*per la nuova modifica dall'anagrafe del patrimonio viene passato tutto in html string
                vId = Request.QueryString("ID")
                vTipo = Request.QueryString("TIPO")
            Else
                vId = Session.Item("ID")
                vTipo = Session.Item("TIPO")
            End If

            SettaChkMateriali()
            settamq()
            SettaChkAnalisi()
            SettaChkInterventi()
            'Aggiunta rilievo Stato Degrado 08/07/2009
            SettaChkDegrado()

            SettaTxtAnomalie()
            SettaDictChech()
            settadictTextBox()
            ControlloEsistenza()
            BtnChiudi.Attributes.Add("onClick", "javascript:window.close();")
        End If
    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.BtnChiudi.Visible = False
            Me.ImageButton4.Visible = False

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False

                End If

            Next

        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
        End Try
    End Sub

    Private Property EsistonoMateriali() As Boolean
        Get
            If Not (ViewState("Par_esistonoMateriali") Is Nothing) Then
                Return (ViewState("Par_esistonoMateriali"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("Par_esistonoMateriali") = value
        End Set

    End Property
    Private Property EsistonoInterventi() As Boolean
        Get
            If Not (ViewState("Par_EsistonoInterventi") Is Nothing) Then
                Return (ViewState("Par_EsistonoInterventi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("Par_EsistonoInterventi") = value
        End Set

    End Property
    'Aggiunta rilevamento STATO DEGRADO 08/07/2009
    Private Property EsisteDegrado() As Boolean
        Get
            If Not (ViewState("Par_EsisteDegrado") Is Nothing) Then
                Return (ViewState("Par_EsisteDegrado"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("Par_EsisteDegrado") = value
        End Set

    End Property
    Private Property EsistonoAnalisi() As Boolean
        Get
            If Not (ViewState("Par_EsistonoAnalisi") Is Nothing) Then
                Return (ViewState("Par_EsistonoAnalisi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("Par_EsistonoAnalisi") = value
        End Set

    End Property
    Private Property EsistonoAnomalie() As Boolean
        Get
            If Not (ViewState("Par_EsistonoAnomalie") Is Nothing) Then
                Return (ViewState("Par_EsistonoAnomalie"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("Par_EsistonoAnomalie") = value
        End Set

    End Property

    Private Property EsistonoDimensioni() As Boolean
        Get
            If Not (ViewState("Par_EsistonoDimension") Is Nothing) Then
                Return (ViewState("Par_EsistonoDimension"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("Par_EsistonoDimension") = value
        End Set

    End Property

    Private Property EsistonoNote() As Boolean
        Get
            If Not (ViewState("Par_EsistonoNote") Is Nothing) Then
                Return (ViewState("Par_EsistonoNote"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("Par_EsistonoNote") = value
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
    Private Property vTipo() As String
        Get
            If Not (ViewState("par_Tipo") Is Nothing) Then
                Return CStr(ViewState("par_Tipo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tipo") = value
        End Set
    End Property
    Private Sub SettaDictChech()
        Dim CTRL As Control = Nothing
        Try
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is CheckBox Then
                    If DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB") = "MATERIALI" Then
                        DictMat.Add(DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_ELEMENTO") & "_" & DirectCast(CTRL, CheckBox).Attributes("ID_TIPO"), DirectCast(CTRL, CheckBox))
                    ElseIf DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE" Then
                        DictAna.Add(DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_ELEMENTO") & "_" & DirectCast(CTRL, CheckBox).Attributes("ID_TIPO"), DirectCast(CTRL, CheckBox))
                    ElseIf DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB") = "INTERVENTI" Then
                        DictInerv.Add(DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_ELEMENTO") & "_" & DirectCast(CTRL, CheckBox).Attributes("ID_TIPO"), DirectCast(CTRL, CheckBox))
                        'Aggiunta Rilievo STATO DEGRADO
                    ElseIf DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB") = "STATO_DEGRADO" Then
                        DictDegrado.Add(DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_SCHEDA") & "_" & DirectCast(CTRL, CheckBox).Attributes("STATO"), DirectCast(CTRL, CheckBox))
                    End If

                End If
            Next
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
        End Try
    End Sub

    Private Sub settadictTextBox()
        Dim CTRL As Control = Nothing
        Try
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    If DirectCast(CTRL, TextBox).Attributes("TABELLA_DB") = "ANOMALIE" Then
                        DictAnomalie.Add(DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO") & "_" & DirectCast(CTRL, TextBox).Attributes("ID_TIPO"), DirectCast(CTRL, TextBox))
                    ElseIf DirectCast(CTRL, TextBox).Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI" Then
                        DictElementi.Add(DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO"), DirectCast(CTRL, TextBox))

                    End If

                End If
            Next
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
        End Try
    End Sub

    Private Sub ControlloEsistenza()
        Try


            ' Dim row As Data.DataRow
            Dim Oggetto As System.Web.UI.WebControls.CheckBox = Nothing
            Dim TextObj As System.Web.UI.WebControls.TextBox = Nothing
            Dim PassData As String = par.IfNull(Request.QueryString("DATA"), "")
            Dim StringControl As String = ""
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Materiali where id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    Dim dt As New Data.DataTable


                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoMateriali = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictMat.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                        Oggetto.Dispose()
                    End If

                    Oggetto = Nothing

                    dt = New Data.DataTable
                    da.Dispose()
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Interventi where id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)


                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoInterventi = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictInerv.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                    End If
                    dt.Dispose()
                    dt = New Data.DataTable
                    da.Dispose()
                    Oggetto = Nothing

                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoAnalisi = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictAna.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                    End If
                    da.Dispose()

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoAnomalie = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictAnomalie.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), TextObj) Then
                                TextObj.Text = row.Item("VALORE").ToString
                            End If
                        Next
                    End If
                    TextObj = Nothing

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoDimensioni = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictElementi.TryGetValue(row.Item("COD_TIPO_ELEMENTO"), TextObj) Then
                                TextObj.Text = row.Item("VALORE").ToString
                            End If
                        Next
                    End If

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.NOTE where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoNote = True
                        Me.TxtNote_1.Text = dt.Rows(0).Item("NOTE_1").ToString
                        Me.TxtNote_2.Text = dt.Rows(0).Item("NOTE_2").ToString

                    End If

                    '******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE
                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    End If
                    dt = New Data.DataTable
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsisteDegrado = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictDegrado.TryGetValue(row.Item("COD_TIPO_SCHEDA") & "_" & row.Item("STATO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next

                    End If
                    'fine aggiunta 
                    '**********************************************************************************************************************************
                Case "COMP"

                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Materiali where id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    Dim dt As New Data.DataTable


                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoMateriali = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictMat.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                        Oggetto.Dispose()
                    End If

                    Oggetto = Nothing
                    dt = New Data.DataTable

                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Interventi where id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    da.Dispose()
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)


                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoInterventi = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictInerv.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                    End If
                    dt.Dispose()
                    dt = New Data.DataTable
                    da.Dispose()

                    Oggetto = Nothing
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoAnalisi = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictAna.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                    End If
                    da.Dispose()

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoAnomalie = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictAnomalie.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), TextObj) Then
                                TextObj.Text = row.Item("VALORE").ToString
                            End If
                        Next
                    End If
                    TextObj = Nothing

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoDimensioni = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictElementi.TryGetValue(row.Item("COD_TIPO_ELEMENTO"), TextObj) Then
                                TextObj.Text = row.Item("VALORE").ToString
                            End If
                        Next
                    End If

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.NOTE where id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoNote = True
                        Me.TxtNote_1.Text = dt.Rows(0).Item("NOTE_1").ToString
                        Me.TxtNote_2.Text = dt.Rows(0).Item("NOTE_2").ToString

                    End If
                    '******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE
                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_COMPLESSO = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsisteDegrado = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictDegrado.TryGetValue(row.Item("COD_TIPO_SCHEDA") & "_" & row.Item("STATO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next

                    End If
                    'fine aggiunta 
                    '**************************************************************************************************************************************

                Case "UC"
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Materiali where id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    Dim dt As New Data.DataTable


                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoMateriali = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictMat.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                        Oggetto.Dispose()
                    End If

                    Oggetto = Nothing

                    dt = New Data.DataTable
                    da.Dispose()
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Interventi where id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)


                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoInterventi = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictInerv.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                    End If
                    dt.Dispose()
                    dt = New Data.DataTable
                    da.Dispose()
                    Oggetto = Nothing
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANALISI_PRESTAZIONALE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoAnalisi = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictAna.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next
                    End If
                    da.Dispose()

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANOMALIE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoAnomalie = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictAnomalie.TryGetValue(row.Item("COD_TIPO_ELEMENTO") & "_" & row.Item("ID_TIPO"), TextObj) Then
                                TextObj.Text = row.Item("VALORE").ToString
                            End If
                        Next
                    End If
                    TextObj = Nothing

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%E%' "
                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        EsistonoDimensioni = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictElementi.TryGetValue(row.Item("COD_TIPO_ELEMENTO"), TextObj) Then
                                TextObj.Text = row.Item("VALORE").ToString
                            End If
                        Next
                    End If

                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.NOTE where id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsistonoNote = True
                        Me.TxtNote_1.Text = dt.Rows(0).Item("NOTE_1").ToString
                        Me.TxtNote_2.Text = dt.Rows(0).Item("NOTE_2").ToString

                    End If
                    '******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE
                    dt = New Data.DataTable
                    If String.IsNullOrEmpty(PassData) = True Then
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_UNITA_COMUNE = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%E%' "
                    End If

                    da = New Oracle.DataAccess.Client.OracleDataAdapter(StringControl, par.OracleConn)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        EsisteDegrado = True
                        For Each row As Data.DataRow In dt.Rows
                            If DictDegrado.TryGetValue(row.Item("COD_TIPO_SCHEDA") & "_" & row.Item("STATO"), Oggetto) Then
                                Oggetto.Checked = True
                            End If
                        Next

                    End If
                    'fine aggiunta 
                    '**************************************************************************************************************************************


            End Select


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If String.IsNullOrEmpty(PassData) = False Or Request.QueryString("SL") > 0 Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Private Sub SalvaTextNote()

        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try
            If Me.TxtNote_1.Text <> "" Or Me.TxtNote_2.Text <> "" Then

                Select Case vTipo
                    Case "EDIF"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_EDIFICIO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'E','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""



                    Case "COMP"


                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (id_complesso,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'E','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""


                    Case "UC"

                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (id_unita_comune,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'E','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""


                End Select

            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "COMP"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta


                Case "UC"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA like '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If


                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA LIKE '%E%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

            End Select

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If ControlloNumerico() = True Then

                SalvaCheckBox()
                SalvaTextBox()
                SalvaTextBoxMq()
                SalvaTextNote()
                'Aggiunta Rilievo Stato Degrado 08/07/2009
                SalvaCheckDegrado()

                Response.Write("<SCRIPT>alert('Operazione eseguita correttamente!');</SCRIPT>")
            Else
                Response.Write("<SCRIPT>alert('Impossibile procedere!Verificare i campi di testo, e che il loro contenuto sia numerico');</SCRIPT>")

            End If

        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Private Function ControlloNumerico() As Boolean
        ControlloNumerico = True
        Dim CTRL As Control = Nothing

        For Each CTRL In Me.form1.Controls

            If TypeOf CTRL Is TextBox Then
                If DirectCast(CTRL, TextBox).Text.ToString <> "" AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then
                    ControlloNumerico = IsNumeric(DirectCast(CTRL, TextBox).Text.ToString)
                    If ControlloNumerico = False Then
                        Return ControlloNumerico
                        Exit Function

                    End If

                End If


            End If
        Next
        Return ControlloNumerico
    End Function
    Private Sub SettaChkMateriali()
        Me.C_MAT_E1_assente.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_MAT_E1_assente.Attributes("ID_TIPO") = "143"
        Me.C_MAT_E1_assente.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E1_blocchi0laterizio.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_MAT_E1_blocchi0laterizio.Attributes("ID_TIPO") = "41"
        Me.C_MAT_E1_blocchi0laterizio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E1_blocchi0precompressi.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_MAT_E1_blocchi0precompressi.Attributes("ID_TIPO") = "144"
        Me.C_MAT_E1_blocchi0precompressi.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E1_elementi0prefabbricati.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_MAT_E1_elementi0prefabbricati.Attributes("ID_TIPO") = "142"
        Me.C_MAT_E1_elementi0prefabbricati.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E1_profilati0ferro.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_MAT_E1_profilati0ferro.Attributes("ID_TIPO") = "141"
        Me.C_MAT_E1_profilati0ferro.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E1_rete0metallica.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_MAT_E1_rete0metallica.Attributes("ID_TIPO") = "124"
        Me.C_MAT_E1_rete0metallica.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E2_apertura0motorizzato.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_MAT_E2_apertura0motorizzato.Attributes("ID_TIPO") = "147"
        Me.C_MAT_E2_apertura0motorizzato.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E2_legno.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_MAT_E2_legno.Attributes("ID_TIPO") = "6"
        Me.C_MAT_E2_legno.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E2_profilati0ferro.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_MAT_E2_profilati0ferro.Attributes("ID_TIPO") = "141"
        Me.C_MAT_E2_profilati0ferro.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E2_rete0metallica.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_MAT_E2_rete0metallica.Attributes("ID_TIPO") = "124"
        Me.C_MAT_E2_rete0metallica.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E2_serratura0elettrica.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_MAT_E2_serratura0elettrica.Attributes("ID_TIPO") = "145"
        Me.C_MAT_E2_serratura0elettrica.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E2_serratura0manuale.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_MAT_E2_serratura0manuale.Attributes("ID_TIPO") = "146"
        Me.C_MAT_E2_serratura0manuale.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_E2_telecomando.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_MAT_E2_telecomando.Attributes("ID_TIPO") = "148"
        Me.C_MAT_E2_telecomando.Attributes("TABELLA_DB") = "MATERIALI"

    End Sub
    Private Sub SettaChkAnalisi()
        Me.C_ANA_E1_forte0obsole.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_ANA_E1_forte0obsole.Attributes("ID_TIPO") = "3"
        Me.C_ANA_E1_forte0obsole.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_E1_lieve0obsole.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_ANA_E1_lieve0obsole.Attributes("ID_TIPO") = "2"
        Me.C_ANA_E1_lieve0obsole.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_E1_nuovo.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_ANA_E1_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_E1_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_E1_tot.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_ANA_E1_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_E1_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_E2_forte0obsole.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_ANA_E2_forte0obsole.Attributes("ID_TIPO") = "3"
        Me.C_ANA_E2_forte0obsole.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_E2_lieve0obsole.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_ANA_E2_lieve0obsole.Attributes("ID_TIPO") = "2"
        Me.C_ANA_E2_lieve0obsole.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_E2_nuovo.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_ANA_E2_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_E2_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_E2_tot.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_ANA_E2_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_E2_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

    End Sub
    Private Sub SettaChkInterventi()
        Me.C_INT_E1_applicazione0trattamenti.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_applicazione0trattamenti.Attributes("ID_TIPO") = "103"
        Me.C_INT_E1_applicazione0trattamenti.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_consolidamento.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_consolidamento.Attributes("ID_TIPO") = "39"
        Me.C_INT_E1_consolidamento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_fissaggio0elementi.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_fissaggio0elementi.Attributes("ID_TIPO") = "91"
        Me.C_INT_E1_fissaggio0elementi.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_protettivi.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_protettivi.Attributes("ID_TIPO") = "104"
        Me.C_INT_E1_protettivi.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_pulizia.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_E1_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_recinzione.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_recinzione.Attributes("ID_TIPO") = "111"
        Me.C_INT_E1_recinzione.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_ricostruzione0portante.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_ricostruzione0portante.Attributes("ID_TIPO") = "14"
        Me.C_INT_E1_ricostruzione0portante.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_ripristino0finitura.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_ripristino0finitura.Attributes("ID_TIPO") = "109"
        Me.C_INT_E1_ripristino0finitura.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E1_sostituzione0elemento.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.C_INT_E1_sostituzione0elemento.Attributes("ID_TIPO") = "107"
        Me.C_INT_E1_sostituzione0elemento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_adeguamento0protezioni.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_adeguamento0protezioni.Attributes("ID_TIPO") = "119"
        Me.C_INT_E2_adeguamento0protezioni.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_adeguamento0segnaletica.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_adeguamento0segnaletica.Attributes("ID_TIPO") = "120"
        Me.C_INT_E2_adeguamento0segnaletica.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_guasto0elettrico.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_guasto0elettrico.Attributes("ID_TIPO") = "116"
        Me.C_INT_E2_guasto0elettrico.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_pulizia.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_E2_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_revisione0aperture.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_revisione0aperture.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_E2_revisione0aperture.Attributes("ID_TIPO") = "117"

        Me.C_INT_E2_revisione0integrale0cancello.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_revisione0integrale0cancello.Attributes("ID_TIPO") = "114"
        Me.C_INT_E2_revisione0integrale0cancello.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_revisione0semplice0cancello.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_revisione0semplice0cancello.Attributes("ID_TIPO") = "115"
        Me.C_INT_E2_revisione0semplice0cancello.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_sostituzione0aperture.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_sostituzione0aperture.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_E2_sostituzione0aperture.Attributes("ID_TIPO") = "282"

        Me.C_INT_E2_sostituzione0cancelli.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_sostituzione0cancelli.Attributes("ID_TIPO") = "112"
        Me.C_INT_E2_sostituzione0cancelli.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_telecomando.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_telecomando.Attributes("ID_TIPO") = "122"
        Me.C_INT_E2_telecomando.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_E2_verifica0funzionamento.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.C_INT_E2_verifica0funzionamento.Attributes("ID_TIPO") = "121"
        Me.C_INT_E2_verifica0funzionamento.Attributes("TABELLA_DB") = "INTERVENTI"

    End Sub

    '*******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE *********
    Private Sub SettaChkDegrado()
        Me.ChkSt1.Attributes("COD_TIPO_SCHEDA") = "E"
        Me.ChkSt1.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt1.Attributes("STATO") = "1"

        Me.ChkSt21.Attributes("COD_TIPO_SCHEDA") = "E"
        Me.ChkSt21.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt21.Attributes("STATO") = "2.1"

        Me.ChkSt22.Attributes("COD_TIPO_SCHEDA") = "E"
        Me.ChkSt22.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt22.Attributes("STATO") = "2.2"

        Me.ChkSt31.Attributes("COD_TIPO_SCHEDA") = "E"
        Me.ChkSt31.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt31.Attributes("STATO") = "3.1"

        Me.ChkSt32.Attributes("COD_TIPO_SCHEDA") = "E"
        Me.ChkSt32.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt32.Attributes("STATO") = "3.2"

        Me.ChkSt33.Attributes("COD_TIPO_SCHEDA") = "E"
        Me.ChkSt33.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt33.Attributes("STATO") = "3.3"


    End Sub
    'fine aggiunta stato degrado
    Private Sub SettaTxtAnomalie()
        Me.T_ANO_E1_corrosione.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.T_ANO_E1_corrosione.Attributes("ID_TIPO") = "3"
        Me.T_ANO_E1_corrosione.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E1_deformzione.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.T_ANO_E1_deformzione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_E1_deformzione.Attributes("ID_TIPO") = "98"

        Me.T_ANO_E1_distacchi0finitura.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.T_ANO_E1_distacchi0finitura.Attributes("ID_TIPO") = "14"
        Me.T_ANO_E1_distacchi0finitura.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E1_distacco0portante.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.T_ANO_E1_distacco0portante.Attributes("ID_TIPO") = "66"
        Me.T_ANO_E1_distacco0portante.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E1_graffiti.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.T_ANO_E1_graffiti.Attributes("ID_TIPO") = "8"
        Me.T_ANO_E1_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E1_instabilita0recinzione.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.T_ANO_E1_instabilita0recinzione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_E1_instabilita0recinzione.Attributes("ID_TIPO") = "99"

        Me.T_ANO_E2_anomalo0funzionamento.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_anomalo0funzionamento.Attributes("ID_TIPO") = "111"
        Me.T_ANO_E2_anomalo0funzionamento.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_apertura0chiusura.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_apertura0chiusura.Attributes("ID_TIPO") = "81"
        Me.T_ANO_E2_apertura0chiusura.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_corrosione.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_corrosione.Attributes("ID_TIPO") = "3"
        Me.T_ANO_E2_corrosione.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_deformazione.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_deformazione.Attributes("ID_TIPO") = "5"
        Me.T_ANO_E2_deformazione.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_distacchi0finitura.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_distacchi0finitura.Attributes("ID_TIPO") = "14"
        Me.T_ANO_E2_distacchi0finitura.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_guasto0elettrico0serratura.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_guasto0elettrico0serratura.Attributes("ID_TIPO") = "104"
        Me.T_ANO_E2_guasto0elettrico0serratura.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_instabilita.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_instabilita.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_E2_instabilita.Attributes("ID_TIPO") = "103"

        Me.T_ANO_E2_malfunzionamento0organi.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_malfunzionamento0organi.Attributes("ID_TIPO") = "48"
        Me.T_ANO_E2_malfunzionamento0organi.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_mancanza0protezioni.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_mancanza0protezioni.Attributes("ID_TIPO") = "110"
        Me.T_ANO_E2_mancanza0protezioni.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_mancanza0segnalazioni.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_mancanza0segnalazioni.Attributes("ID_TIPO") = "113"
        Me.T_ANO_E2_mancanza0segnalazioni.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_non0funzionante.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_non0funzionante.Attributes("ID_TIPO") = "109"
        Me.T_ANO_E2_non0funzionante.Attributes("TABELLA_DB") = "ANOMALIE"

        Me.T_ANO_E2_rumorosita.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_rumorosita.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_E2_rumorosita.Attributes("ID_TIPO") = "108"

        Me.T_ANO_E2_telecomando.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.T_ANO_E2_telecomando.Attributes("ID_TIPO") = "112"
        Me.T_ANO_E2_telecomando.Attributes("TABELLA_DB") = "ANOMALIE"
    End Sub
    Private Sub SalvaTextBox()
        Dim CTRL As Control = Nothing
        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try
            Select Case vTipo
                Case "EDIF"
                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is TextBox Then

                            If DirectCast(CTRL, TextBox).Text.ToString <> "" AndAlso Not DirectCast(CTRL, TextBox).ID.Contains("mq") AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, TextBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_EDIFICIO,ID_TIPO,COD_TIPO_ELEMENTO,VALORE) VALUES " _
                                & " (" & vId & "," & DirectCast(CTRL, TextBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & " )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""

                            End If

                        End If
                    Next

                Case "COMP"

                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is TextBox Then

                            If DirectCast(CTRL, TextBox).Text.ToString <> "" AndAlso Not DirectCast(CTRL, TextBox).ID.Contains("mq") AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, TextBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_COMPLESSO,ID_TIPO,COD_TIPO_ELEMENTO,VALORE) VALUES " _
                                & " (" & vId & "," & DirectCast(CTRL, TextBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & " )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""

                            End If

                        End If
                    Next


                Case "UC"
                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is TextBox Then

                            If DirectCast(CTRL, TextBox).Text.ToString <> "" AndAlso Not DirectCast(CTRL, TextBox).ID.Contains("mq") AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, TextBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_UNITA_COMUNE,ID_TIPO,COD_TIPO_ELEMENTO,VALORE) VALUES " _
                                & " (" & vId & "," & DirectCast(CTRL, TextBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & " )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""

                            End If

                        End If
                    Next


            End Select

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Private Sub SalvaTextBoxMq()
        Dim CTRL As Control = Nothing
        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try
            Select Case vTipo
                Case "EDIF"

                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.Contains("mq") AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then

                            If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, TextBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_EDIFICIO,COD_TIPO_ELEMENTO,VALORE) VALUES " _
                                & " ( " & vId & ",'" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & " )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If
                        End If
                    Next

                Case "COMP"

                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.Contains("mq") AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then

                            If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, TextBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_COMPLESSO,COD_TIPO_ELEMENTO,VALORE) VALUES " _
                                & " ( " & vId & ",'" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & " )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If
                        End If
                    Next


                Case "UC"

                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.Contains("mq") AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then

                            If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, TextBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_UNITA_COMUNE,COD_TIPO_ELEMENTO,VALORE) VALUES " _
                                & " ( " & vId & ",'" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & " )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If
                        End If
                    Next

            End Select

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    '***************AGGIUNTA STATO DEGRADO 07/08/2009 PEPPE
    Private Sub SalvaCheckDegrado()
        Dim CTRL As Control = Nothing
        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try

            Select Case vTipo
                Case "EDIF"

                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is CheckBox AndAlso DirectCast(CTRL, CheckBox).ID.Contains("ChkSt") Then

                            If DirectCast(CTRL, CheckBox).Checked = True Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_EDIFICIO,STATO,COD_TIPO_SCHEDA) VALUES " _
                                & " (" & vId & ",'" & (DirectCast(CTRL, CheckBox).Attributes("STATO").ToString) & "', '" & DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_SCHEDA").ToString & "' )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If

                        End If
                    Next
                Case "COMP"
                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is CheckBox AndAlso DirectCast(CTRL, CheckBox).ID.Contains("ChkSt") Then

                            If DirectCast(CTRL, CheckBox).Checked = True Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_COMPLESSO,STATO,COD_TIPO_SCHEDA) VALUES " _
                                & " (" & vId & ",'" & DirectCast(CTRL, CheckBox).Attributes("STATO").ToString & "', '" & DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_SCHEDA").ToString & "' )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If

                        End If
                    Next


                Case "UC"
                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is CheckBox AndAlso DirectCast(CTRL, CheckBox).ID.Contains("ChkSt") Then

                            If DirectCast(CTRL, CheckBox).Checked = True Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_UNITA_COMUNE,STATO,COD_TIPO_SCHEDA) VALUES " _
                                & " (" & vId & ",'" & DirectCast(CTRL, CheckBox).Attributes("STATO").ToString & "', '" & DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_SCHEDA").ToString & "' )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If

                        End If
                    Next
            End Select
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    'FINE AGGIUNTA PEPPE
    Private Sub SalvaCheckBox()
        Dim CTRL As Control = Nothing
        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try

            Select Case vTipo
                Case "EDIF"

                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is CheckBox Then

                            If DirectCast(CTRL, CheckBox).Checked = True AndAlso DirectCast(CTRL, CheckBox).ID.Contains("ChkSt") = False Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_EDIFICIO,ID_TIPO,COD_TIPO_ELEMENTO) VALUES " _
                                & " (" & vId & "," & DirectCast(CTRL, CheckBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_ELEMENTO").ToString & "' )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""

                            End If

                        End If
                    Next
                Case "COMP"
                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is CheckBox Then

                            If DirectCast(CTRL, CheckBox).Checked = True AndAlso DirectCast(CTRL, CheckBox).ID.Contains("ChkSt") = False Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_COMPLESSO,ID_TIPO,COD_TIPO_ELEMENTO) VALUES " _
                                & " (" & vId & "," & DirectCast(CTRL, CheckBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_ELEMENTO").ToString & "' )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""

                            End If

                        End If
                    Next

                Case "UC"
                    For Each CTRL In Me.form1.Controls

                        If TypeOf CTRL Is CheckBox Then

                            If DirectCast(CTRL, CheckBox).Checked = True AndAlso DirectCast(CTRL, CheckBox).ID.Contains("ChkSt") = False Then
                                STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, CheckBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_UNITA_COMUNE,ID_TIPO,COD_TIPO_ELEMENTO) VALUES " _
                                & " (" & vId & "," & DirectCast(CTRL, CheckBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, CheckBox).Attributes("COD_TIPO_ELEMENTO").ToString & "' )"
                                par.cmd.CommandText = STRINSERT
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If

                        End If
                    Next

            End Select

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Private Sub settamq()
        Me.Text_mq_E1.Attributes("COD_TIPO_ELEMENTO") = "E1"
        Me.Text_mq_E1.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_E2.Attributes("COD_TIPO_ELEMENTO") = "E2"
        Me.Text_mq_E2.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        '    Dim CTRL As Control=nothing
        '    Dim StrSql As String
        '    Dim descrizione As String
        '    Dim idtipo As String
        '    Dim stringa As String = ""
        '    If par.OracleConn.State = Data.ConnectionState.Open Then
        '        par.OracleConn.Close()
        '        Exit Sub
        '    Else
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)
        '    End If
        '    Try


        '        For Each CTRL In Me.form1.Controls


        '            'If TypeOf CTRL Is CheckBox Then
        '            '    stringa = "Me."
        '            '    stringa = stringa & DirectCast(CTRL, CheckBox).ID.ToString()

        '            '    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("E1") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "E1" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("E2") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "E2" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(10)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C2") Then
        '            '        '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
        '            '        '    descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        '    If descrizione.Contains("0") Then
        '            '        '        descrizione = descrizione.Replace("0", "%")
        '            '        '    End If

        '            '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C3") Then
        '            '        '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.2" & Chr(34) & vbCrLf
        '            '        '    descrizione = DirectCast(CTRL, CheckBox).ID.Substring(10)
        '            '        '    If descrizione.Contains("0") Then
        '            '        '        descrizione = descrizione.Replace("0", "%")
        '            '        '    End If

        '            '    End If



        '            '    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("MAT") Then
        '            '        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "MATERIALI" & Chr(34) & vbCrLf
        '            '        '******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE
        '            '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '            '        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_MATERIALI where DESCRIZIONE like '%" & descrizione & "%'"
        '            '        par.cmd.CommandText = StrSql
        '            '        myReader = par.cmd.ExecuteReader

        '            '        If myReader.Read Then
        '            '            idtipo = myReader(0)
        '            '            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
        '            '        End If
        '            '        myReader.Close()
        '            '        '*****FINE *****
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("INT") Then
        '            '        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "INTERVENTI" & Chr(34) & vbCrLf
        '            '        '******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE

        '            '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '            '        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_INTERVENTI where DESCRIZIONE like '%" & descrizione & "%'"
        '            '        par.cmd.CommandText = StrSql
        '            '        myReader = par.cmd.ExecuteReader

        '            '        If myReader.Read Then
        '            '            idtipo = myReader(0)
        '            '            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        '            '        End If
        '            '        myReader.Close()
        '            '        '*****FINE *****

        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("ANA") Then

        '            '        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANALISI_PRESTAZIONALE" & Chr(34) & vbCrLf
        '            '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '            '        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANALISI where DESCRIZIONE like '%" & descrizione & "%'"
        '            '        par.cmd.CommandText = StrSql
        '            '        myReader = par.cmd.ExecuteReader

        '            '        If myReader.Read Then
        '            '            idtipo = myReader(0)
        '            '            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        '            '        End If
        '            '        myReader.Close()
        '            '        '*****FINE *****


        '            '    End If
        '            '    TxtNote_1.Text = TxtNote_1.Text & stringa
        '            '    TxtNote_1.Text = TxtNote_1.Text & vbCrLf

        '            'End If

        '            If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_1" AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_2" Then
        '                stringa = "Me."
        '                stringa = stringa & DirectCast(CTRL, TextBox).ID.ToString()

        '                If DirectCast(CTRL, TextBox).ID.ToString().Contains("E1") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "D1" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If

        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("E2") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.1" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                End If

        '                'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        '                '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
        '                '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                '    If descrizione.Contains("0") Then
        '                '        descrizione = descrizione.Replace("0", "%")
        '                '    End If
        '                'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        '                '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "D1.1" & Chr(34) & vbCrLf
        '                '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                '    If descrizione.Contains("0") Then
        '                '        descrizione = descrizione.Replace("0", "%")
        '                '    End If
        '                'End If
        '                stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANOMALIE" & Chr(34) & vbCrLf

        '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '                StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANOMALIE where DESCRIZIONE like '%" & descrizione & "%'"
        '                par.cmd.CommandText = StrSql
        '                myReader = par.cmd.ExecuteReader

        '                If myReader.Read Then
        '                    idtipo = myReader(0)
        '                    stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
        '                End If
        '                myReader.Close()

        '                stringa = stringa & vbCrLf
        '                TxtNote_1.Text = TxtNote_1.Text & stringa
        '                TxtNote_1.Text = TxtNote_1.Text & vbCrLf

        '            End If
        '        Next


        '    Catch ex As Exception
        '        Dim s As New Exception(DirectCast(CTRL, CheckBox).ID)
        '        par.OracleConn.Close()
        '        par.SettaCommand(par)
        '    End Try
        '    par.OracleConn.Close()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Checked = True
            ElseIf TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Text = "55"
            End If
        Next
    End Sub


End Class
