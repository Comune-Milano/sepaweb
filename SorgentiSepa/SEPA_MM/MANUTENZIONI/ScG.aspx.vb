Imports System.Collections.Generic
Partial Class ScG
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
            Me.BtnSave.Visible = False

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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_COMPLESSO = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANALISI_PRESTAZIONALE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANOMALIE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_UNITA_COMUNE = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%G%' "
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

        '            '    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("G1") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G1" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G2") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G2" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G3") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G3" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If

        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G4") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G4" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G5") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G5" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G6") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G6" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G7") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G7" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G8") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G8" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If
        '            '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("G9") Then
        '            '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G9" & Chr(34) & vbCrLf
        '            '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '            '        If descrizione.Contains("0") Then
        '            '            descrizione = descrizione.Replace("0", "%")
        '            '        End If



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

        '                If DirectCast(CTRL, TextBox).ID.ToString().Contains("G1") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G1" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If

        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G2") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G2" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G3") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G3" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G4") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G4" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G5") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G5" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G6") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G6" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G7") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G7" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G8") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G8" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If
        '                ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("G9") Then
        '                    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "G9" & Chr(34) & vbCrLf
        '                    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    If descrizione.Contains("0") Then
        '                        descrizione = descrizione.Replace("0", "%")
        '                    End If

        '                    'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        '                    '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
        '                    '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                    '    If descrizione.Contains("0") Then
        '                    '        descrizione = descrizione.Replace("0", "%")
        '                    '    End If
        '                    'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        '                    '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "D1.1" & Chr(34) & vbCrLf
        '                    '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                    '    If descrizione.Contains("0") Then
        '                    '        descrizione = descrizione.Replace("0", "%")
        '                    '    End If
        '                End If
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
        '                    Next


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
    Private Sub SettaChkAnalisi()
        Me.C_ANA_G1_forte.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_ANA_G1_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G1_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G1_lieve.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_ANA_G1_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G1_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G1_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_ANA_G1_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G1_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G1_tot.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_ANA_G1_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G1_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G2_forte.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_ANA_G2_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G2_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G2_lieve.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_ANA_G2_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G2_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G2_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_ANA_G2_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G2_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G2_tot.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_ANA_G2_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G2_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G3_forte.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_ANA_G3_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G3_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G3_lieve.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_ANA_G3_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G3_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G3_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_ANA_G3_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G3_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G3_tot.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_ANA_G3_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G3_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G4_forte.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_ANA_G4_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G4_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G4_lieve.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_ANA_G4_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G4_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G4_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_ANA_G4_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G4_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G4_tot.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_ANA_G4_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G4_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G5_forte.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_ANA_G5_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G5_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G5_lieve.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_ANA_G5_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G5_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G5_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_ANA_G5_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G5_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G5_tot.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_ANA_G5_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G5_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G6_forte.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_ANA_G6_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G6_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G6_lieve.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_ANA_G6_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G6_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G6_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_ANA_G6_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G6_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G6_tot.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_ANA_G6_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G6_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G7_forte.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_ANA_G7_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G7_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G7_lieve.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_ANA_G7_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G7_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G7_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_ANA_G7_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G7_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G7_tot.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_ANA_G7_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G7_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G8_forte.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_ANA_G8_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G8_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G8_lieve.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_ANA_G8_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G8_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G8_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_ANA_G8_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G8_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G8_tot.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_ANA_G8_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G8_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G9_forte.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_ANA_G9_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_G9_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G9_lieve.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_ANA_G9_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_G9_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G9_nuovo.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_ANA_G9_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_G9_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_G9_tot.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_ANA_G9_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_G9_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"


    End Sub
    '*******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE *********
    Private Sub SettaChkDegrado()
        Me.ChkSt1.Attributes("COD_TIPO_SCHEDA") = "G"
        Me.ChkSt1.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt1.Attributes("STATO") = "1"

        Me.ChkSt21.Attributes("COD_TIPO_SCHEDA") = "G"
        Me.ChkSt21.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt21.Attributes("STATO") = "2.1"

        Me.ChkSt22.Attributes("COD_TIPO_SCHEDA") = "G"
        Me.ChkSt22.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt22.Attributes("STATO") = "2.2"

        Me.ChkSt31.Attributes("COD_TIPO_SCHEDA") = "G"
        Me.ChkSt31.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt31.Attributes("STATO") = "3.1"

        Me.ChkSt32.Attributes("COD_TIPO_SCHEDA") = "G"
        Me.ChkSt32.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt32.Attributes("STATO") = "3.2"

        Me.ChkSt33.Attributes("COD_TIPO_SCHEDA") = "G"
        Me.ChkSt33.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt33.Attributes("STATO") = "3.3"


    End Sub
    'fine aggiunta stato degrado
    Private Sub SettaChkInterventi()
        Me.C_INT_G1_applicazione0protettivo.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_INT_G1_applicazione0protettivo.Attributes("ID_TIPO") = "133"
        Me.C_INT_G1_applicazione0protettivo.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G1_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_INT_G1_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G1_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G1_ricostruzione0cls.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_INT_G1_ricostruzione0cls.Attributes("ID_TIPO") = "132"
        Me.C_INT_G1_ricostruzione0cls.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G1_rimozione0cls.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_INT_G1_rimozione0cls.Attributes("ID_TIPO") = "3"
        Me.C_INT_G1_rimozione0cls.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G1_ripristino0finitura.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_INT_G1_ripristino0finitura.Attributes("ID_TIPO") = "109"
        Me.C_INT_G1_ripristino0finitura.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G1_risanamento0armature.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_INT_G1_risanamento0armature.Attributes("ID_TIPO") = "129"
        Me.C_INT_G1_risanamento0armature.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G2_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_INT_G2_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G2_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G2_ricostruzione0cls.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_INT_G2_ricostruzione0cls.Attributes("ID_TIPO") = "132"
        Me.C_INT_G2_ricostruzione0cls.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G2_ripristino0finitura.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_INT_G2_ripristino0finitura.Attributes("ID_TIPO") = "109"
        Me.C_INT_G2_ripristino0finitura.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G2_risanamento0armature.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_INT_G2_risanamento0armature.Attributes("ID_TIPO") = "129"
        Me.C_INT_G2_risanamento0armature.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G2_sostituzione0elemento.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_INT_G2_sostituzione0elemento.Attributes("ID_TIPO") = "107"
        Me.C_INT_G2_sostituzione0elemento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G2_trattamento0protettivo.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_INT_G2_trattamento0protettivo.Attributes("ID_TIPO") = "6"
        Me.C_INT_G2_trattamento0protettivo.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G3_ricostruzione0cls.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_INT_G3_ricostruzione0cls.Attributes("ID_TIPO") = "132"
        Me.C_INT_G3_ricostruzione0cls.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G3_rimozione0cls.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_INT_G3_rimozione0cls.Attributes("ID_TIPO") = "3"
        Me.C_INT_G3_rimozione0cls.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G3_risanamento0armature.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_INT_G3_risanamento0armature.Attributes("ID_TIPO") = "129"
        Me.C_INT_G3_risanamento0armature.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G3_sostituzione0elemento.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_INT_G3_sostituzione0elemento.Attributes("ID_TIPO") = "107"
        Me.C_INT_G3_sostituzione0elemento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G3_trattamento0protettivo.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_INT_G3_trattamento0protettivo.Attributes("ID_TIPO") = "6"
        Me.C_INT_G3_trattamento0protettivo.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G4_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_INT_G4_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G4_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G4_riparazione.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_INT_G4_riparazione.Attributes("ID_TIPO") = "116"
        Me.C_INT_G4_riparazione.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G4_sostituzione0elemento.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_INT_G4_sostituzione0elemento.Attributes("ID_TIPO") = "107"
        Me.C_INT_G4_sostituzione0elemento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G4_sostituzione0serrature.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_INT_G4_sostituzione0serrature.Attributes("ID_TIPO") = "148"
        Me.C_INT_G4_sostituzione0serrature.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G5_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_INT_G5_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G5_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G5_rimozione0arredi0disuso.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_INT_G5_rimozione0arredi0disuso.Attributes("ID_TIPO") = "151"
        Me.C_INT_G5_rimozione0arredi0disuso.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G5_sistemazione0terreno.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_INT_G5_sistemazione0terreno.Attributes("ID_TIPO") = "26"
        Me.C_INT_G5_sistemazione0terreno.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G5_sostituzione0arredo.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_INT_G5_sostituzione0arredo.Attributes("ID_TIPO") = "149"
        Me.C_INT_G5_sostituzione0arredo.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G6_manto0superiore.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_INT_G6_manto0superiore.Attributes("ID_TIPO") = "38"
        Me.C_INT_G6_manto0superiore.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G6_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_INT_G6_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G6_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G6_rifacimento0integrale.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_INT_G6_rifacimento0integrale.Attributes("ID_TIPO") = "157"
        Me.C_INT_G6_rifacimento0integrale.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G6_riparazion0locali.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_INT_G6_riparazion0locali.Attributes("ID_TIPO") = "154"
        Me.C_INT_G6_riparazion0locali.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G6_riparazioni0estese.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_INT_G6_riparazioni0estese.Attributes("ID_TIPO") = "155"
        Me.C_INT_G6_riparazioni0estese.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G6_ripristino0elementi.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_INT_G6_ripristino0elementi.Attributes("ID_TIPO") = "158"
        Me.C_INT_G6_ripristino0elementi.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G7_manto0superiore.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_INT_G7_manto0superiore.Attributes("ID_TIPO") = "38"
        Me.C_INT_G7_manto0superiore.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G7_percorsi0carrabili.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_INT_G7_percorsi0carrabili.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_G7_percorsi0carrabili.Attributes("ID_TIPO") = "301"

        Me.C_INT_G7_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_INT_G7_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G7_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G7_rifacimento0integrale.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_INT_G7_rifacimento0integrale.Attributes("ID_TIPO") = "157"
        Me.C_INT_G7_rifacimento0integrale.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G7_riparazioni0estese.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_INT_G7_riparazioni0estese.Attributes("ID_TIPO") = "155"
        Me.C_INT_G7_riparazioni0estese.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G7_ripristino0elementi.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_INT_G7_ripristino0elementi.Attributes("ID_TIPO") = "158"
        Me.C_INT_G7_ripristino0elementi.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G8_manutenzione0verde.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_INT_G8_manutenzione0verde.Attributes("ID_TIPO") = "167"
        Me.C_INT_G8_manutenzione0verde.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G8_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_INT_G8_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G8_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G8_reinterro0buche.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_INT_G8_reinterro0buche.Attributes("ID_TIPO") = "169"
        Me.C_INT_G8_reinterro0buche.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G8_ripristino0cordolo.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_INT_G8_ripristino0cordolo.Attributes("ID_TIPO") = "170"
        Me.C_INT_G8_ripristino0cordolo.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G8_ripristino0irrigazione.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_INT_G8_ripristino0irrigazione.Attributes("ID_TIPO") = "168"
        Me.C_INT_G8_ripristino0irrigazione.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G8_semina.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_INT_G8_semina.Attributes("ID_TIPO") = "166"
        Me.C_INT_G8_semina.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G9_pulizia.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_INT_G9_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_G9_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_G9_sosituzione0serramento.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_INT_G9_sosituzione0serramento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_G9_sosituzione0serramento.Attributes("ID_TIPO") = "55"


    End Sub
    Private Sub SettaChkMateriali()
        Me.C_MAT_G1_blocchi0laterizio.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_MAT_G1_blocchi0laterizio.Attributes("ID_TIPO") = "41"
        Me.C_MAT_G1_blocchi0laterizio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G1_blocchi0precompressi.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_MAT_G1_blocchi0precompressi.Attributes("ID_TIPO") = "144"
        Me.C_MAT_G1_blocchi0precompressi.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G1_cls0armato.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_MAT_G1_cls0armato.Attributes("ID_TIPO") = "4"
        Me.C_MAT_G1_cls0armato.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G1_miste.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.C_MAT_G1_miste.Attributes("ID_TIPO") = "9"
        Me.C_MAT_G1_miste.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G2_cls0opera.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_MAT_G2_cls0opera.Attributes("ID_TIPO") = "28"
        Me.C_MAT_G2_cls0opera.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G2_pietra.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_MAT_G2_pietra.Attributes("ID_TIPO") = "10"
        Me.C_MAT_G2_pietra.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G2_prefabbricati.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.C_MAT_G2_prefabbricati.Attributes("ID_TIPO") = "26"
        Me.C_MAT_G2_prefabbricati.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G3_a0perdere.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_MAT_G3_a0perdere.Attributes("ID_TIPO") = "181"
        Me.C_MAT_G3_a0perdere.Attributes("TABELLA_DB") = "MATERIALI"


        Me.C_MAT_G3_cls0armato.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_MAT_G3_cls0armato.Attributes("ID_TIPO") = "4"
        Me.C_MAT_G3_cls0armato.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G3_collegati0rete.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_MAT_G3_collegati0rete.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G3_collegati0rete.Attributes("ID_TIPO") = "182"

        Me.C_MAT_G3_ghisa.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_MAT_G3_ghisa.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G3_ghisa.Attributes("ID_TIPO") = "183"

        Me.C_MAT_G3_pietra.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_MAT_G3_pietra.Attributes("ID_TIPO") = "10"
        Me.C_MAT_G3_pietra.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G3_prefabbricati0cls.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.C_MAT_G3_prefabbricati0cls.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G3_prefabbricati0cls.Attributes("ID_TIPO") = "184"

        Me.C_MAT_G4_legno.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_MAT_G4_legno.Attributes("ID_TIPO") = "6"
        Me.C_MAT_G4_legno.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G4_metallo.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_MAT_G4_metallo.Attributes("ID_TIPO") = "82"
        Me.C_MAT_G4_metallo.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G4_miste.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.C_MAT_G4_miste.Attributes("ID_TIPO") = "9"
        Me.C_MAT_G4_miste.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G5_altro.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_MAT_G5_altro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G5_altro.Attributes("ID_TIPO") = "185"


        Me.C_MAT_G5_aree0gioco.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_MAT_G5_aree0gioco.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G5_aree0gioco.Attributes("ID_TIPO") = "186"


        Me.C_MAT_G5_contenitori0rifiuti.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_MAT_G5_contenitori0rifiuti.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G5_contenitori0rifiuti.Attributes("ID_TIPO") = "187"

        Me.C_MAT_G5_fioriere.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_MAT_G5_fioriere.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G5_fioriere.Attributes("ID_TIPO") = "189"


        Me.C_MAT_G5_fontane.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_MAT_G5_fontane.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G5_fontane.Attributes("ID_TIPO") = "188"

        Me.C_MAT_G5_panchine.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_MAT_G5_panchine.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G5_panchine.Attributes("ID_TIPO") = "190"

        Me.C_MAT_G5_porta0biciclette.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.C_MAT_G5_porta0biciclette.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G5_porta0biciclette.Attributes("ID_TIPO") = "191"

        Me.C_MAT_G6_asfalto.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_MAT_G6_asfalto.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G6_asfalto.Attributes("ID_TIPO") = "192"

        Me.C_MAT_G6_autobloccanti.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_MAT_G6_autobloccanti.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G6_autobloccanti.Attributes("ID_TIPO") = "193"

        Me.C_MAT_G6_battuto0cemento.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_MAT_G6_battuto0cemento.Attributes("ID_TIPO") = "84"
        Me.C_MAT_G6_battuto0cemento.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G6_ceramica.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_MAT_G6_ceramica.Attributes("ID_TIPO") = "127"
        Me.C_MAT_G6_ceramica.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G6_galelggianti.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_MAT_G6_galelggianti.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G6_galelggianti.Attributes("ID_TIPO") = "194"

        Me.C_MAT_G6_pietra.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.C_MAT_G6_pietra.Attributes("ID_TIPO") = "10"
        Me.C_MAT_G6_pietra.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G7_asfalto.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_MAT_G7_asfalto.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G7_asfalto.Attributes("ID_TIPO") = "192"

        Me.C_MAT_G7_autobloccanti.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_MAT_G7_autobloccanti.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G7_autobloccanti.Attributes("ID_TIPO") = "193"

        Me.C_MAT_G7_battuto0cemento.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_MAT_G7_battuto0cemento.Attributes("ID_TIPO") = "84"
        Me.C_MAT_G7_battuto0cemento.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G7_pietra.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_MAT_G7_pietra.Attributes("ID_TIPO") = "10"
        Me.C_MAT_G7_pietra.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G7_presenza0dossi.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.C_MAT_G7_presenza0dossi.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G7_presenza0dossi.Attributes("ID_TIPO") = "195"

        Me.C_MAT_G8_aree0erbose.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_MAT_G8_aree0erbose.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G8_aree0erbose.Attributes("ID_TIPO") = "196"

        Me.C_MAT_G8_irrigazione0automatico.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_MAT_G8_irrigazione0automatico.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G8_irrigazione0automatico.Attributes("ID_TIPO") = "197"

        Me.C_MAT_G8_piantumazioni0arboree.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_MAT_G8_piantumazioni0arboree.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G8_piantumazioni0arboree.Attributes("ID_TIPO") = "198"


        Me.C_MAT_G8_piantumazioni0arbustive.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.C_MAT_G8_piantumazioni0arbustive.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G8_piantumazioni0arbustive.Attributes("ID_TIPO") = "199"

        Me.C_MAT_G9_metallo.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_MAT_G9_metallo.Attributes("ID_TIPO") = "82"
        Me.C_MAT_G9_metallo.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_G9_plastica.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.C_MAT_G9_plastica.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_G9_plastica.Attributes("ID_TIPO") = "200"

    End Sub
    Private Sub SettaTxtAnomalie()
        Me.T_ANO_G1_alterazioni0cromatiche.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.T_ANO_G1_alterazioni0cromatiche.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G1_alterazioni0cromatiche.Attributes("ID_TIPO") = "31"

        Me.T_ANO_G1_cavillature0superficiali.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.T_ANO_G1_cavillature0superficiali.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G1_cavillature0superficiali.Attributes("ID_TIPO") = "121"

        Me.T_ANO_G1_carbonatazione.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.T_ANO_G1_carbonatazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G1_carbonatazione.Attributes("ID_TIPO") = "122"

        Me.T_ANO_G1_distacchi.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.T_ANO_G1_distacchi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G1_distacchi.Attributes("ID_TIPO") = "2"

        Me.T_ANO_G1_rigonfiamenti.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.T_ANO_G1_rigonfiamenti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G1_rigonfiamenti.Attributes("ID_TIPO") = "59"

        Me.T_ANO_G1_graffiti.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.T_ANO_G1_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G1_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_G1_efflorescenze.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.T_ANO_G1_efflorescenze.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G1_efflorescenze.Attributes("ID_TIPO") = "126"

        Me.T_ANO_G2_alterazioni0cromatiche.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.T_ANO_G2_alterazioni0cromatiche.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G2_alterazioni0cromatiche.Attributes("ID_TIPO") = "31"

        Me.T_ANO_G2_distacchi.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.T_ANO_G2_distacchi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G2_distacchi.Attributes("ID_TIPO") = "2"

        Me.T_ANO_G2_carbonatazione.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.T_ANO_G2_carbonatazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G2_carbonatazione.Attributes("ID_TIPO") = "122"

        Me.T_ANO_G2_graffiti.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.T_ANO_G2_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G2_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_G2_divelto.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.T_ANO_G2_divelto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G2_divelto.Attributes("ID_TIPO") = "131"

        Me.T_ANO_G2_mancanza0cordolo.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.T_ANO_G2_mancanza0cordolo.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G2_mancanza0cordolo.Attributes("ID_TIPO") = "132"

        Me.T_ANO_G3_rotture.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.T_ANO_G3_rotture.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G3_rotture.Attributes("ID_TIPO") = "2"

        Me.T_ANO_G3_mancanza0coperchio.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.T_ANO_G3_mancanza0coperchio.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G3_mancanza0coperchio.Attributes("ID_TIPO") = "134"

        Me.T_ANO_G3_carbonatazione.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.T_ANO_G3_carbonatazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G3_carbonatazione.Attributes("ID_TIPO") = "122"

        Me.T_ANO_G3_divelto.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.T_ANO_G3_divelto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G3_divelto.Attributes("ID_TIPO") = "131"

        Me.T_ANO_G4_divelto.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.T_ANO_G4_divelto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G4_divelto.Attributes("ID_TIPO") = "131"

        Me.T_ANO_G4_parziali0caselle.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.T_ANO_G4_parziali0caselle.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G4_parziali0caselle.Attributes("ID_TIPO") = "138"

        Me.T_ANO_G4_graffiti.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.T_ANO_G4_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G4_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_G4_danneggiamenti0generalizzati.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.T_ANO_G4_danneggiamenti0generalizzati.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G4_danneggiamenti0generalizzati.Attributes("ID_TIPO") = "140"

        Me.T_ANO_G5_rotture0arredo.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.T_ANO_G5_rotture0arredo.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G5_rotture0arredo.Attributes("ID_TIPO") = "141"

        Me.T_ANO_G5_graffiti.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.T_ANO_G5_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G5_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_G5_disuso.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.T_ANO_G5_disuso.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G5_disuso.Attributes("ID_TIPO") = "143"

        Me.T_ANO_G5_presenza0vegetazione.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.T_ANO_G5_presenza0vegetazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G5_presenza0vegetazione.Attributes("ID_TIPO") = "144"

        Me.T_ANO_G5_buche.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.T_ANO_G5_buche.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G5_buche.Attributes("ID_TIPO") = "145"

        Me.T_ANO_G6_buche.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.T_ANO_G6_buche.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G6_buche.Attributes("ID_TIPO") = "145"

        Me.T_ANO_G6_cedimenti0sottofondo.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.T_ANO_G6_cedimenti0sottofondo.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G6_cedimenti0sottofondo.Attributes("ID_TIPO") = "147"

        Me.T_ANO_G6_distacchi0elementi.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.T_ANO_G6_distacchi0elementi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G6_distacchi0elementi.Attributes("ID_TIPO") = "148"

        Me.T_ANO_G6_ristagno0acqua.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.T_ANO_G6_ristagno0acqua.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G6_ristagno0acqua.Attributes("ID_TIPO") = "149"

        Me.T_ANO_G6_presenza0vegetazione.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.T_ANO_G6_presenza0vegetazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G6_presenza0vegetazione.Attributes("ID_TIPO") = "144"

        Me.T_ANO_G6_sporcizia.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.T_ANO_G6_sporcizia.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G6_sporcizia.Attributes("ID_TIPO") = "151"

        Me.T_ANO_G7_buche.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.T_ANO_G7_buche.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G7_buche.Attributes("ID_TIPO") = "145"

        Me.T_ANO_G7_cedimenti0sottofondo.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.T_ANO_G7_cedimenti0sottofondo.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G7_cedimenti0sottofondo.Attributes("ID_TIPO") = "147"

        Me.T_ANO_G7_distacchi0elementi.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.T_ANO_G7_distacchi0elementi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G7_distacchi0elementi.Attributes("ID_TIPO") = "148"

        Me.T_ANO_G7_ristagno0acqua.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.T_ANO_G7_ristagno0acqua.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G7_ristagno0acqua.Attributes("ID_TIPO") = "149"

        Me.T_ANO_G7_presenza0vegetazione.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.T_ANO_G7_presenza0vegetazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G7_presenza0vegetazione.Attributes("ID_TIPO") = "144"

        Me.T_ANO_G8_mancanza0cordoli.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.T_ANO_G8_mancanza0cordoli.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G8_mancanza0cordoli.Attributes("ID_TIPO") = "157"

        Me.T_ANO_G8_arbusti0pericolanti.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.T_ANO_G8_arbusti0pericolanti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G8_arbusti0pericolanti.Attributes("ID_TIPO") = "158"

        Me.T_ANO_G8_sporco.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.T_ANO_G8_sporco.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G8_sporco.Attributes("ID_TIPO") = "40"

        Me.T_ANO_G8_scarsa0manutenzione.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.T_ANO_G8_scarsa0manutenzione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G8_scarsa0manutenzione.Attributes("ID_TIPO") = "160"

        Me.T_ANO_G8_buche.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.T_ANO_G8_buche.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G8_buche.Attributes("ID_TIPO") = "145"

        Me.T_ANO_G8_guasti0irrigazione.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.T_ANO_G8_guasti0irrigazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G8_guasti0irrigazione.Attributes("ID_TIPO") = "162"

        Me.T_ANO_G9_graffiti.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.T_ANO_G9_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G9_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_G9_divelto.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.T_ANO_G9_divelto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G9_divelto.Attributes("ID_TIPO") = "131"

        Me.T_ANO_G9_mancante.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.T_ANO_G9_mancante.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_G9_mancante.Attributes("ID_TIPO") = "165"

    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "COMP"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "UC"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA like '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%G%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA LIKE '%G%'"
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

    Private Sub SalvaTextNote()

        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try
            If Me.TxtNote_1.Text <> "" Or Me.TxtNote_2.Text <> "" Then
                Select Case vTipo
                    Case "EDIF"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_EDIFICIO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'G','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""



                    Case "COMP"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_COMPLESSO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'G','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""




                    Case "UC"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_UNITA_COMUNE,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'G','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
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
    'Private Sub SalvaTextBoxMq()
    '    Dim CTRL As Control=nothing
    '    Dim STRINSERT As String
    '    par.OracleConn.Open()
    '    par.SettaCommand(par)
    '    Try
    '        For Each CTRL In Me.form1.Controls

    '            If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.Contains("mq") AndAlso Not (DirectCast(CTRL, TextBox).ID.Contains("Note")) Then

    '                If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
    '                    STRINSERT = "INSERT INTO SISCOM_MI." & DirectCast(CTRL, TextBox).Attributes("TABELLA_DB").ToUpper.ToString & " (ID_EDIFICIO,COD_TIPO_ELEMENTO,VALORE) VALUES " _
    '                    & " ( " & vId & ",'" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & " )"
    '                    par.cmd.CommandText = STRINSERT
    '                    par.cmd.ExecuteNonQuery()
    '                    par.cmd.CommandText = ""
    '                End If
    '            End If
    '        Next
    '        par.OracleConn.Close()
    '    Catch ex As Exception
    '        Dim e As New Exception(DirectCast(CTRL, TextBox).ID)
    '        par.OracleConn.Close()
    '    End Try
    'End Sub
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
        Me.Text_mq_G1.Attributes("COD_TIPO_ELEMENTO") = "G1"
        Me.Text_mq_G1.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G2.Attributes("COD_TIPO_ELEMENTO") = "G2"
        Me.Text_mq_G2.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G3.Attributes("COD_TIPO_ELEMENTO") = "G3"
        Me.Text_mq_G3.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G4.Attributes("COD_TIPO_ELEMENTO") = "G4"
        Me.Text_mq_G4.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G51.Attributes("COD_TIPO_ELEMENTO") = "G5"
        Me.Text_mq_G51.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G52.Attributes("COD_TIPO_ELEMENTO") = "G52"
        Me.Text_mq_G52.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G53.Attributes("COD_TIPO_ELEMENTO") = "G53"
        Me.Text_mq_G53.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G54.Attributes("COD_TIPO_ELEMENTO") = "G54"
        Me.Text_mq_G54.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G55.Attributes("COD_TIPO_ELEMENTO") = "G55"
        Me.Text_mq_G55.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G56.Attributes("COD_TIPO_ELEMENTO") = "G56"
        Me.Text_mq_G56.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G57.Attributes("COD_TIPO_ELEMENTO") = "G57"
        Me.Text_mq_G57.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G6.Attributes("COD_TIPO_ELEMENTO") = "G6"
        Me.Text_mq_G6.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G7.Attributes("COD_TIPO_ELEMENTO") = "G7"
        Me.Text_mq_G7.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G8.Attributes("COD_TIPO_ELEMENTO") = "G8"
        Me.Text_mq_G8.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G81.Attributes("COD_TIPO_ELEMENTO") = "G8.1"
        Me.Text_mq_G81.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_G81.Attributes("MIO") = "2"

        Me.Text_mq_G82.Attributes("COD_TIPO_ELEMENTO") = "G8.2"
        Me.Text_mq_G82.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_G9.Attributes("COD_TIPO_ELEMENTO") = "G9"
        Me.Text_mq_G9.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

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

End Class
