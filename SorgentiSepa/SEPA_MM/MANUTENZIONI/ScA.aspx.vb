Imports System.Collections.Generic
Partial Class ScA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public DictMat As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAna As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictInerv As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAnomalie As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)
    Public DictElementi As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)
    'Aggiunta STATO DEGRADO 08/07/2009
    Public DictDegrado As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)


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

            '

            SettaCheckBoxMateriali()
            SettaChecAnalisi()
            SettaChecInterventi()
            'Aggiunta rilievo Stato Degrado 08/07/2009
            SettaChkDegrado()
            SettaTxtAnomalie()
            settaMq()
            SettaDictChech()
            settadictTextBox()
            ControlloEsistenza()
            ImageButton1.Attributes.Add("onClick", "javascript:window.close();")
        End If
    End Sub
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
            Dim e As New Exception(DirectCast(CTRL, CheckBox).ID)
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_COMPLESSO = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANALISI_PRESTAZIONALE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANOMALIE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_UNITA_COMUNE = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%A%' "
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
    Private Sub SettaCheckBoxMateriali()

        '*****SEZIONE MATERIALI*******
        Me.ChkA1_MAT_Continue.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.ChkA1_MAT_Continue.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA1_MAT_Continue.Attributes("ID_TIPO") = "1"

        Me.ChkA1_MATDiscont.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.ChkA1_MATDiscont.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA1_MATDiscont.Attributes("ID_TIPO") = "2"

        Me.ChkA2_MAT_MurPort.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_MAT_MurPort.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA2_MAT_MurPort.Attributes("ID_TIPO") = "3"

        Me.ChkA2_MAT_ClsArmato.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_MAT_ClsArmato.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA2_MAT_ClsArmato.Attributes("ID_TIPO") = "4"

        Me.ChkA2_MAT_Prefabb.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_MAT_Prefabb.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA2_MAT_Prefabb.Attributes("ID_TIPO") = "5"

        Me.ChkA3_MAT_legno.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_MAT_legno.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA3_MAT_legno.Attributes("ID_TIPO") = "6"

        Me.ChkA3_MAT_Acciaio.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_MAT_Acciaio.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA3_MAT_Acciaio.Attributes("ID_TIPO") = "7"

        Me.ChKA3_MAT_ClsArmato.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChKA3_MAT_ClsArmato.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChKA3_MAT_ClsArmato.Attributes("ID_TIPO") = "4"

        Me.ChkA3_MAT_miste.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_MAT_miste.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA3_MAT_miste.Attributes("ID_TIPO") = "9"

        Me.ChkA3_MAT_Mattoni.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_MAT_Mattoni.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA3_MAT_Mattoni.Attributes("ID_TIPO") = "10"

        Me.ChkA3_MAT_Laterocemento.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_MAT_Laterocemento.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA3_MAT_Laterocemento.Attributes("ID_TIPO") = "11"

        Me.ChkA3_MAT_Prefab.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_MAT_Prefab.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA3_MAT_Prefab.Attributes("ID_TIPO") = "5"

        Me.ChkA4_MAT_clsArmato.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_clsArmato.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_clsArmato.Attributes("ID_TIPO") = "4"
        'ATTENZIONE DUE VOLTE LEGNO NELLO STESSO GRUPPO 
        Me.ChkA4_MAT_Legno.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Legno.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Legno.Attributes("ID_TIPO") = "6"

        'Me.ChkA4_MAT_Legno2.Attributes("COD_TIPO_ELEMENTO") = "A4"
        'Me.ChkA4_MAT_Legno2.Attributes("TABELLA_DB") = "MATERIALI"
        'Me.ChkA4_MAT_Legno2.Attributes("ID_TIPO") = "6"
        '***FINE

        Me.ChkA4_MAT_Acciaio.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Acciaio.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Acciaio.Attributes("ID_TIPO") = "7"

        Me.ChkA4_MAT_Miste.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Miste.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Miste.Attributes("ID_TIPO") = "9"

        Me.ChkA4_MAT_Mattoni.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Mattoni.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Mattoni.Attributes("ID_TIPO") = "10"

        Me.ChkA4_MAT_Prefab.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Prefab.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Prefab.Attributes("ID_TIPO") = "5"

        Me.ChkA4_MAT_Parapetto.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Parapetto.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Parapetto.Attributes("ID_TIPO") = "19"

        Me.ChkA4_MAT_Ferro.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Ferro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Ferro.Attributes("ID_TIPO") = "20"



        Me.ChkA4_MAT_Pvc.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Pvc.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Pvc.Attributes("ID_TIPO") = "22"

        Me.ChkA4_MAT_MuratCls.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_MuratCls.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_MuratCls.Attributes("ID_TIPO") = "23"

        Me.ChkA4_MAT_Vetro.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_Vetro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_Vetro.Attributes("ID_TIPO") = "24"

        Me.ChkA4_MAT_CorrimFerro.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_MAT_CorrimFerro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA4_MAT_CorrimFerro.Attributes("ID_TIPO") = "402"

        Me.ChkA5_MAT_Solai.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_Solai.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_Solai.Attributes("ID_TIPO") = "25"

        Me.ChkA5_MAT_Prefab.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_Prefab.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_Prefab.Attributes("ID_TIPO") = "26"

        Me.ChkA5_MAT_Legno.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_Legno.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_Legno.Attributes("ID_TIPO") = "27"

        Me.ChkA5_MAT_ClsArmato.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_ClsArmato.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_ClsArmato.Attributes("ID_TIPO") = "28"

        Me.ChkA5_MAT_Acciaio.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_Acciaio.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_Acciaio.Attributes("ID_TIPO") = "29"

        Me.ChkA5_MAT_Paina.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_Paina.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_Paina.Attributes("ID_TIPO") = "30"

        Me.ChkA5_MAT_Falde.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_Falde.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_Falde.Attributes("ID_TIPO") = "31"

        Me.ChkA5_MAT_GrondeAggetto.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_MAT_GrondeAggetto.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA5_MAT_GrondeAggetto.Attributes("ID_TIPO") = "32"

        Me.ChkA6_MAT_LaterCemento.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_LaterCemento.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_LaterCemento.Attributes("ID_TIPO") = "11"

        Me.ChkA6_MAT_ClsArmato.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_ClsArmato.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_ClsArmato.Attributes("ID_TIPO") = "34"

        Me.ChkA6_MAT_Pietra.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_Pietra.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_Pietra.Attributes("ID_TIPO") = "35"

        Me.ChkA6_MAT_Acciaio.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_Acciaio.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_Acciaio.Attributes("ID_TIPO") = "7"

        Me.ChkA6_MAT_ParapFerro.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_ParapFerro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_ParapFerro.Attributes("ID_TIPO") = "37"

        Me.ChkA6_MAT_ParapLegno.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_ParapLegno.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_ParapLegno.Attributes("ID_TIPO") = "38"

        Me.ChkA6_MAT_ParapMuratura.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_ParapMuratura.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_ParapMuratura.Attributes("ID_TIPO") = "39"

        Me.ChkA6_MAT_Vetro.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_Vetro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_Vetro.Attributes("ID_TIPO") = "24"

        Me.ChkA6_MAT_ParapetPVC.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_MAT_ParapetPVC.Attributes("TABELLA_DB") = "MATERIALI"
        Me.ChkA6_MAT_ParapetPVC.Attributes("ID_TIPO") = "403"

        '******FINE SEZIONE MATERIALI*******

    End Sub
    Private Sub SettaChecAnalisi()
        '*****SEZIONE ANALISI*******
        Me.ChkA1_ANA_Nuovo.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.ChkA1_ANA_Nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA1_ANA_Nuovo.Attributes("ID_TIPO") = "1"


        Me.ChkA1_ANA_LieveObsolescenza.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.ChkA1_ANA_LieveObsolescenza.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA1_ANA_LieveObsolescenza.Attributes("ID_TIPO") = "2"

        Me.ChkA1_ANA_ForteObsolescenza.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.ChkA1_ANA_ForteObsolescenza.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA1_ANA_ForteObsolescenza.Attributes("ID_TIPO") = "3"

        Me.ChkA1_ANA_TotObs.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.ChkA1_ANA_TotObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA1_ANA_TotObs.Attributes("ID_TIPO") = "4"

        Me.ChkA2_ANA_Nuovo.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_ANA_Nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA2_ANA_Nuovo.Attributes("ID_TIPO") = "1"

        Me.ChkA2_ANA_LieveObs.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_ANA_LieveObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA2_ANA_LieveObs.Attributes("ID_TIPO") = "2"

        Me.ChkA2_ANA_ForteObs.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_ANA_ForteObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA2_ANA_ForteObs.Attributes("ID_TIPO") = "3"

        Me.ChkA2_ANA_TotObs.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_ANA_TotObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA2_ANA_TotObs.Attributes("ID_TIPO") = "4"

        Me.ChkA3_ANA_Nuovo.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_ANA_Nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA3_ANA_Nuovo.Attributes("ID_TIPO") = "1"

        Me.ChkA3_ANA_LieveObs.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_ANA_LieveObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA3_ANA_LieveObs.Attributes("ID_TIPO") = "2"

        Me.ChkA3_ANA_ForteObs.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_ANA_ForteObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA3_ANA_ForteObs.Attributes("ID_TIPO") = "3"

        Me.ChkA3_ANA_TotObs.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_ANA_TotObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA3_ANA_TotObs.Attributes("ID_TIPO") = "4"

        Me.ChkA4_ANA_Nuovo.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_ANA_Nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA4_ANA_Nuovo.Attributes("ID_TIPO") = "1"

        Me.ChkA4_ANA_LieveObs.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_ANA_LieveObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA4_ANA_LieveObs.Attributes("ID_TIPO") = "2"

        Me.ChkA4_ANA_ForteObs.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_ANA_ForteObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA4_ANA_ForteObs.Attributes("ID_TIPO") = "3"

        Me.ChkA4_ANA_TotObs.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_ANA_TotObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA4_ANA_TotObs.Attributes("ID_TIPO") = "4"

        Me.ChkA5_ANA_Nuovo.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_ANA_Nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA5_ANA_Nuovo.Attributes("ID_TIPO") = "1"

        Me.ChkA5_ANA_LieveObs.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_ANA_LieveObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA5_ANA_LieveObs.Attributes("ID_TIPO") = "2"

        Me.ChkA5_ANA_ForteObs.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_ANA_ForteObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA5_ANA_ForteObs.Attributes("ID_TIPO") = "3"

        Me.ChkA5_ANA_TotObs.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_ANA_TotObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA5_ANA_TotObs.Attributes("ID_TIPO") = "4"

        Me.ChkA6_ANA_Nuovo.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_ANA_Nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA6_ANA_Nuovo.Attributes("ID_TIPO") = "1"

        Me.ChkA6_ANA_LieveObs.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_ANA_LieveObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA6_ANA_LieveObs.Attributes("ID_TIPO") = "2"

        Me.ChkA6_ANA_ForteObs.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_ANA_ForteObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA6_ANA_ForteObs.Attributes("ID_TIPO") = "3"

        Me.ChkA6_ANA_TotObs.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_ANA_TotObs.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"
        Me.ChkA6_ANA_TotObs.Attributes("ID_TIPO") = "4"
        '******FINE SEZIONE ANALISI*******
    End Sub
    Private Sub SettaChecInterventi()
        '*****SEZIONE INTERVENTI*******
        Me.ChkA1_INT_monitoraggio.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.ChkA1_INT_monitoraggio.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA1_INT_monitoraggio.Attributes("ID_TIPO") = "1"

        Me.ChkA2_INT_demolizione.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_INT_demolizione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA2_INT_demolizione.Attributes("ID_TIPO") = "2"

        Me.ChkA2_INT_Rimoz_Cls.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_INT_Rimoz_Cls.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA2_INT_Rimoz_Cls.Attributes("ID_TIPO") = "3"

        Me.ChkA2_INT_Ristrutturazione.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_INT_Ristrutturazione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA2_INT_Ristrutturazione.Attributes("ID_TIPO") = "4"

        Me.ChkA2_INT_Consolidamento_Strut.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_INT_Consolidamento_Strut.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA2_INT_Consolidamento_Strut.Attributes("ID_TIPO") = "5"

        Me.ChkA2_INT_trattamento.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.ChkA2_INT_trattamento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA2_INT_trattamento.Attributes("ID_TIPO") = "6"

        Me.ChkA3_INT_demolizione.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_INT_demolizione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA3_INT_demolizione.Attributes("ID_TIPO") = "2"

        Me.ChkA3_INT_ristrutturazione.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_INT_ristrutturazione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA3_INT_ristrutturazione.Attributes("ID_TIPO") = "4"

        Me.ChkA3_INT_Consolidamento.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_INT_Consolidamento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA3_INT_Consolidamento.Attributes("ID_TIPO") = "5"

        Me.ChkA3_INT_Trattamento.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.ChkA3_INT_Trattamento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA3_INT_Trattamento.Attributes("ID_TIPO") = "6"

        Me.ChkA4_INT_Demolizione.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Demolizione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Demolizione.Attributes("ID_TIPO") = "2"

        Me.ChkA4_INT_Ristrutturazione.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Ristrutturazione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Ristrutturazione.Attributes("ID_TIPO") = "4"

        Me.ChkA4_INT_Consolidamento.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Consolidamento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Consolidamento.Attributes("ID_TIPO") = "5"

        Me.ChkA4_INT_Ricostruzione.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Ricostruzione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Ricostruzione.Attributes("ID_TIPO") = "14"

        Me.ChkA4_INT_Rifacimento.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Rifacimento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Rifacimento.Attributes("ID_TIPO") = "15"

        Me.ChkA4_INT_Adeguamento.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Adeguamento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Adeguamento.Attributes("ID_TIPO") = "16"

        Me.ChkA4_INT_Pulizia.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Pulizia.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Pulizia.Attributes("ID_TIPO") = "17"

        Me.ChkA4_INT_Sostituzione.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_Sostituzione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_Sostituzione.Attributes("ID_TIPO") = "18"

        Me.ChkA4_INT_SostLastVetr.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_SostLastVetr.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_SostLastVetr.Attributes("ID_TIPO") = "19"


        Me.ChkA4_INT_TratProtFerri.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_TratProtFerri.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_TratProtFerri.Attributes("ID_TIPO") = "20"

        Me.ChkA4_INT_FissaggParapetto.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_FissaggParapetto.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_FissaggParapetto.Attributes("ID_TIPO") = "21"

        Me.ChkA4_INT_ElimInfiltraz.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.ChkA4_INT_ElimInfiltraz.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA4_INT_ElimInfiltraz.Attributes("ID_TIPO") = "22"

        Me.ChkA5_INT_demolizione.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_demolizione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_demolizione.Attributes("ID_TIPO") = "2"

        Me.ChkA5_INT_ristrutturazione.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_ristrutturazione.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_ristrutturazione.Attributes("ID_TIPO") = "4"

        Me.ChkA5_INT_consolidStrutt.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_consolidStrutt.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_consolidStrutt.Attributes("ID_TIPO") = "5"

        Me.ChkA5_INT_sistTerreno.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_sistTerreno.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_sistTerreno.Attributes("ID_TIPO") = "26"

        Me.ChkA5_INT_ricostportante.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_ricostportante.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_ricostportante.Attributes("ID_TIPO") = "14"

        Me.ChkA5_INT_rifacimento.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_rifacimento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_rifacimento.Attributes("ID_TIPO") = "15"

        Me.ChkA5_INT_impermeabiliz.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_impermeabiliz.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_impermeabiliz.Attributes("ID_TIPO") = "29"

        Me.ChkA5_INT_ricostGronde.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_ricostGronde.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_ricostGronde.Attributes("ID_TIPO") = "30"

        Me.ChkA5_INT_pulizGronde.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_pulizGronde.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_pulizGronde.Attributes("ID_TIPO") = "31"

        Me.ChkA5_INT_puliziaSottet.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_puliziaSottet.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_puliziaSottet.Attributes("ID_TIPO") = "32"

        Me.ChkA5_INT_ricorstett.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_ricorstett.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_ricorstett.Attributes("ID_TIPO") = "33"

        Me.ChkA5_INT_mantoCopert.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.ChkA5_INT_mantoCopert.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA5_INT_mantoCopert.Attributes("ID_TIPO") = "34"

        Me.ChkA6_INT_ricostPortante.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_ricostPortante.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_ricostPortante.Attributes("ID_TIPO") = "14"

        Me.ChkA6_INT_rinforzPortante.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_rinforzPortante.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_rinforzPortante.Attributes("ID_TIPO") = "36"

        Me.ChkA6_INT_protettPareti.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_protettPareti.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_protettPareti.Attributes("ID_TIPO") = "37"

        Me.ChkA6_INT_RifaciMantoSup.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_RifaciMantoSup.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_RifaciMantoSup.Attributes("ID_TIPO") = "38"

        Me.ChkA6_INT_consolidamento.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_consolidamento.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_consolidamento.Attributes("ID_TIPO") = "5"

        Me.ChkA6_INT_sostElem.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_sostElem.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_sostElem.Attributes("ID_TIPO") = "40"

        Me.ChkA6_INT_sostLastra.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_sostLastra.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_sostLastra.Attributes("ID_TIPO") = "41"

        Me.ChkA6_INT_adegAltezza.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_adegAltezza.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_adegAltezza.Attributes("ID_TIPO") = "16"

        Me.ChkA6_INT_fissParapetto.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_fissParapetto.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_fissParapetto.Attributes("ID_TIPO") = "21"

        Me.ChkA6_INT_sostParapetto.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.ChkA6_INT_sostParapetto.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.ChkA6_INT_sostParapetto.Attributes("ID_TIPO") = "44"
        '******FINE SEZIONE INTERVENTI*******


    End Sub
    '*******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE *********
    Private Sub SettaChkDegrado()
        Me.ChkSt1.Attributes("COD_TIPO_SCHEDA") = "A"
        Me.ChkSt1.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt1.Attributes("STATO") = "1"

        Me.ChkSt21.Attributes("COD_TIPO_SCHEDA") = "A"
        Me.ChkSt21.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt21.Attributes("STATO") = "2.1"

        Me.ChkSt22.Attributes("COD_TIPO_SCHEDA") = "A"
        Me.ChkSt22.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt22.Attributes("STATO") = "2.2"

        Me.ChkSt31.Attributes("COD_TIPO_SCHEDA") = "A"
        Me.ChkSt31.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt31.Attributes("STATO") = "3.1"

        Me.ChkSt32.Attributes("COD_TIPO_SCHEDA") = "A"
        Me.ChkSt32.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt32.Attributes("STATO") = "3.2"

        Me.ChkSt33.Attributes("COD_TIPO_SCHEDA") = "A"
        Me.ChkSt33.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt33.Attributes("STATO") = "3.3"


    End Sub
    'fine aggiunta stato degrado
    Private Sub SettaTxtAnomalie()
        '*****SEZIONE INTERVENTI*******
        Me.Txt_A1_Cedim_Strutt.Attributes("COD_TIPO_ELEMENTO") = "A1"
        Me.Txt_A1_Cedim_Strutt.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A1_Cedim_Strutt.Attributes("ID_TIPO") = "1"

        Me.Txt_A2_Cedim_Strutt.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.Txt_A2_Cedim_Strutt.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A2_Cedim_Strutt.Attributes("ID_TIPO") = "1"

        Me.Txt_A2_Distacchi_rotture.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.Txt_A2_Distacchi_rotture.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A2_Distacchi_rotture.Attributes("ID_TIPO") = "2"

        Me.Txt_A2_Corrosione_Armat.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.Txt_A2_Corrosione_Armat.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A2_Corrosione_Armat.Attributes("ID_TIPO") = "3"

        Me.Txt_A3_Cedim_Strutt.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.Txt_A3_Cedim_Strutt.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A3_Cedim_Strutt.Attributes("ID_TIPO") = "1"

        Me.Txt_A3_InstabSingElem.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.Txt_A3_InstabSingElem.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A3_InstabSingElem.Attributes("ID_TIPO") = "4"

        Me.Txt_A3_DeformSingElem.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.Txt_A3_DeformSingElem.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A3_DeformSingElem.Attributes("ID_TIPO") = "5"

        Me.Txt_A3_CorrosioneArm.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.Txt_A3_CorrosioneArm.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A3_CorrosioneArm.Attributes("ID_TIPO") = "3"

        Me.Txt_A4_Cedim_Strutt.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_Cedim_Strutt.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_Cedim_Strutt.Attributes("ID_TIPO") = "1"

        Me.Txt_A4_DeformPartStru.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_DeformPartStru.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_DeformPartStru.Attributes("ID_TIPO") = "6"

        Me.Txt_A4_RotturePortante.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_RotturePortante.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_RotturePortante.Attributes("ID_TIPO") = "7"

        Me.Txt_A4_Graffiti.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_Graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_Graffiti.Attributes("ID_TIPO") = "8"

        Me.Txt_A4_Pavimento.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_Pavimento.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_Pavimento.Attributes("ID_TIPO") = "9"

        Me.Txt_A4_Instab_Parapett.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_Instab_Parapett.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_Instab_Parapett.Attributes("ID_TIPO") = "10"

        Me.Txt_A4_Altezza_inferiore.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_Altezza_inferiore.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_Altezza_inferiore.Attributes("ID_TIPO") = "11"

        Me.Txt_A4_Infiltraz.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_Infiltraz.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_Infiltraz.Attributes("ID_TIPO") = "12"

        Me.Txt_A4_CorrosioneArmat.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_CorrosioneArmat.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_CorrosioneArmat.Attributes("ID_TIPO") = "3"

        Me.Txt_A4_DistacFinitura.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Txt_A4_DistacFinitura.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A4_DistacFinitura.Attributes("ID_TIPO") = "14"

        Me.Txt_A5_Cedim_Strutt.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_Cedim_Strutt.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_Cedim_Strutt.Attributes("ID_TIPO") = "1"

        Me.Txt_A5_DeformStrutt.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_DeformStrutt.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_DeformStrutt.Attributes("ID_TIPO") = "15"


        Me.Txt_A5_RuotePartiPort.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_RuotePartiPort.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_RuotePartiPort.Attributes("ID_TIPO") = "16"

        Me.Txt_A5_Manto_Copert.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_Manto_Copert.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_Manto_Copert.Attributes("ID_TIPO") = "17"

        Me.Txt_A5_Degrad_imperm.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_Degrad_imperm.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_Degrad_imperm.Attributes("ID_TIPO") = "18"

        Me.Txt_A5_Distacchi_Grande.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_Distacchi_Grande.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_Distacchi_Grande.Attributes("ID_TIPO") = "19"

        Me.Txt_A5_Otturaz_pluviali.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_Otturaz_pluviali.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_Otturaz_pluviali.Attributes("ID_TIPO") = "20"

        Me.Txt_A5_MacerieSottotetto.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_MacerieSottotetto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_MacerieSottotetto.Attributes("ID_TIPO") = "21"

        Me.Txt_A5_InfiltrGronda.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Txt_A5_InfiltrGronda.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A5_InfiltrGronda.Attributes("ID_TIPO") = "22"

        Me.Txt_A6_CedimStrutturali.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_CedimStrutturali.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_CedimStrutturali.Attributes("ID_TIPO") = "1"

        Me.Txt_A6_Distacchi_Finiture.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_Distacchi_Finiture.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_Distacchi_Finiture.Attributes("ID_TIPO") = "14"

        Me.Txt_A6_Instabil_deform.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_Instabil_deform.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_Instabil_deform.Attributes("ID_TIPO") = "24"

        Me.Txt_A6_Corros_arm.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_Corros_arm.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_Corros_arm.Attributes("ID_TIPO") = "3"

        Me.Txt_A6_Distacchi_portante.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_Distacchi_portante.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_Distacchi_portante.Attributes("ID_TIPO") = "7"

        Me.Txt_A6_Rottura_Lastra.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_Rottura_Lastra.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_Rottura_Lastra.Attributes("ID_TIPO") = "27"

        Me.Txt_A6_Instab_Parapetto.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_Instab_Parapetto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_Instab_Parapetto.Attributes("ID_TIPO") = "10"

        Me.Txt_A6_Parap_inferior.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Txt_A6_Parap_inferior.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.Txt_A6_Parap_inferior.Attributes("ID_TIPO") = "11"
        '******FINE SEZIONE INTERVENTI*******
    End Sub
    'Public Property QUERY() As String
    '    Get
    '        If Not (ViewState("par_QUERY") Is Nothing) Then
    '            Return CStr(ViewState("par_QUERY"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_QUERY") = value
    '    End Set

    'End Property
    'Public Property DictMat() As Dictionary(Of Integer, String)
    '    Get
    '        Return

    '    End Get
    '    Set(ByVal value As Dictionary(Of Integer, String))

    '    End Set
    'End Property

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    'Dim CTRL As Control=nothing
    '    ''Dim StrSql As String
    '    ''Dim idtipo As String
    '    'Dim stringa As String = ""
    '    'For Each CTRL In Me.form1.Controls
    '    '    If TypeOf CTRL Is TextBox Then

    '    '        stringa = "Me."
    '    '        stringa = stringa & DirectCast(CTRL, TextBox).ID.ToString()

    '    '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & DirectCast(CTRL, TextBox).ID.ToString().Substring(4, 2) & Chr(34) & vbCrLf
    '    '        stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANOMALIE" & Chr(34) & vbCrLf



    '    '        Me.TextBox2.Text = Me.TextBox2.Text & stringa

    '    '        'par.OracleConn.Open()
    '    '        'par.SettaCommand(par)
    '    '        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader
    '    '        'StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_INTERVENTI where DESCRIZIONE like '%" & DirectCast(CTRL, CheckBox).ID.ToString().ToLower.Substring(10) & "%'"
    '    '        'par.cmd.CommandText = StrSql
    '    '        'myReader = par.cmd.ExecuteReader
    '    '        'If myReader.Read Then
    '    '        '    idtipo = myReader(0)

    '    '        '    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

    '    '        'End If
    '    '        'myReader.Close()
    '    '        'par.OracleConn.Close()

    '    '        'stringa = stringa & vbCrLf & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

    '    '        'If DirectCast(CTRL, CheckBox).ID.ToString().ToUpper.Contains("NUOVO") Then
    '    '        '    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & "1" & Chr(34) & vbCrLf
    '    '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().ToUpper.Contains("LIEVE") Then
    '    '        '    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & "2" & Chr(34) & vbCrLf
    '    '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().ToUpper.Contains("FORTE") Then
    '    '        '    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & "3" & Chr(34) & vbCrLf
    '    '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().ToUpper.Contains("TOT") Then
    '    '        '    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & "4" & Chr(34) & vbCrLf
    '    '        'End If
    '    '        'par.OracleConn.Open()
    '    '        'par.SettaCommand(par)
    '    '        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader

    '    '        'par.cmd.CommandText = query
    '    '        'myReader = par.cmd.ExecuteReader
    '    '        'If myReader.Read Then
    '    '        '    idtipo = myReader(0)
    '    '        'End If
    '    '        'myReader.Close()
    '    '        'par.OracleConn.Close()

    '    '        'stringa = stringa & vbCrLf & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

    '    '    End If

    '    'Next
    '    Dim CTRL As Control=nothing
    '    For Each ctrl In Me.form1.Controls
    '        If TypeOf ctrl Is CheckBox Then
    '            DirectCast(ctrl, CheckBox).Checked = True
    '        ElseIf TypeOf ctrl Is TextBox Then
    '            DirectCast(ctrl, TextBox).Text = "55"
    '        End If
    '    Next


    '    'End Sub



    'End Sub

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
    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta
                Case "COMP"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "UC"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA like '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_ELEMENTO LIkE '%A%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA LIKE '%A%'"
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
    Private Sub settaMq()

        Me.Text_mq_A2.Attributes("COD_TIPO_ELEMENTO") = "A2"
        Me.Text_mq_A2.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_A3.Attributes("COD_TIPO_ELEMENTO") = "A3"
        Me.Text_mq_A3.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_A4.Attributes("COD_TIPO_ELEMENTO") = "A4"
        Me.Text_mq_A4.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_A5.Attributes("COD_TIPO_ELEMENTO") = "A5"
        Me.Text_mq_A5.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_A6.Attributes("COD_TIPO_ELEMENTO") = "A6"
        Me.Text_mq_A6.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"



    End Sub

    'Protected Sub Crea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Crea.Click
    '    Dim CTRL As Control=nothing
    '    For Each ctrl In Me.form1.Controls
    '        If TypeOf ctrl Is CheckBox Then
    '            DirectCast(ctrl, CheckBox).Checked = True
    '        ElseIf TypeOf ctrl Is TextBox Then
    '            DirectCast(ctrl, TextBox).Text = "55"
    '        End If
    '    Next
    '    'Dim CTRL As Control=nothing
    '    'For Each ctrl In Me.form1.Controls
    '    '    If TypeOf ctrl Is TextBox Then
    '    '        DirectCast(ctrl, TextBox).MaxLength = 4
    '    '    End If
    '    'Next
    '    'Dim CTRL As Control=nothing
    '    'Dim name As String
    '    'name = 
    'End Sub
    Private Sub SalvaTextNote()

        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try
            If Me.TxtNote_1.Text <> "" Or Me.TxtNote_2.Text <> "" Then
                Select Case vTipo
                    Case "EDIF"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_EDIFICIO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'A','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""


                    Case "COMP"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_COMPLESSO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'A','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""



                    Case "UC"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_UNITA_COMUNE,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'A','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
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

    Private Sub FrmSolaLettura()
        Try
            Me.ImageButton1.Visible = False
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
End Class
