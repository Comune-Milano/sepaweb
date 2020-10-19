
Imports System.Collections.Generic

Partial Class ScR
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
            SettaChkAnalisi()
            SettaChkInterventi()
            'Aggiunta rilievo Stato Degrado 08/07/2009
            SettaChkDegrado()

            SettaTxtAnomalie()
            settamq()
            SettaDictChech()
            settadictTextBox()
            ControlloEsistenza()
            BtnChiudi.Attributes.Add("onClick", "javascript:window.close();")
        End If

    End Sub
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

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "COMP"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE id_complesso = " & vId & " AND COD_TIPO_SCHEDA LIKE '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "UC"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA like '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA LIKE '%R%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

            End Select

            par.OracleConn.Close()
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
            par.cmd.Dispose()
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
                    If DirectCast(CTRL, TextBox).ID = "Text_mq_N1" Then
                        Beep()
                    End If
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_COMPLESSO = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANALISI_PRESTAZIONALE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANOMALIE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_UNITA_COMUNE = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%R%' "
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
            par.cmd.Dispose()
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



            par.cmd.Dispose()
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
                                & " ( " & vId & "," & DirectCast(CTRL, TextBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text.ToString) & " )"
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
                                & " ( " & vId & "," & DirectCast(CTRL, TextBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text.ToString) & " )"
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
                                & " ( " & vId & "," & DirectCast(CTRL, TextBox).Attributes("ID_TIPO").ToString & ", '" & DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO").ToString & "', " & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text.ToString) & " )"
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
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
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

                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_EDIFICIO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'R','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                    Case "COMP"

                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_COMPLESSO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'R','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""


                    Case "UC"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_UNITA_COMUNE,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'R','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""


                End Select
            End If


            par.OracleConn.Close()
        Catch ex As Exception
            Label1.Visible = True
            Label1.Text = ex.Message
            par.OracleConn.Close()
        End Try

    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim CTRL As Control=nothing
        'Dim StrSql As String
        'Dim descrizione As String
        'Dim idtipo As String
        'Dim stringa As String = ""
        'If par.OracleConn.State = Data.ConnectionState.Open Then
        '    par.OracleConn.Close()
        '    Exit Sub
        'Else
        '    par.OracleConn.Open()
        '    par.SettaCommand(par)
        'End If
        'Try


        '    For Each CTRL In Me.form1.Controls


        '        'If TypeOf CTRL Is CheckBox Then
        '        '    stringa = "Me."
        '        '    stringa = stringa & DirectCast(CTRL, CheckBox).ID.ToString()

        '        '    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("R1") Then
        '        '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "R1" & Chr(34) & vbCrLf
        '        '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        '        If descrizione.Contains("0") Then
        '        '            descrizione = descrizione.Replace("0", "%")
        '        '        End If
        '        '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("R2") Then
        '        '        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "R2" & Chr(34) & vbCrLf
        '        '        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        '        If descrizione.Contains("0") Then
        '        '            descrizione = descrizione.Replace("0", "%")
        '        '        End If
        '        '        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q3") Then
        '        '        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q3" & Chr(34) & vbCrLf
        '        '        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        '        ''        ' ''        If descrizione.Contains("0") Then
        '        '        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        '        '        ''        ' ''        End If

        '        '        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q4") Then
        '        '        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q4" & Chr(34) & vbCrLf
        '        '        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        '        ''        ' ''        If descrizione.Contains("0") Then
        '        '        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        '        '        ''        ' ''        End If
        '        '        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q5") Then
        '        '        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q5" & Chr(34) & vbCrLf
        '        '        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        '        ''        ' ''        If descrizione.Contains("0") Then
        '        '        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        '        '        ''        ' ''        End If


        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C2") Then
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    If descrizione.Contains("0") Then
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '        descrizione = descrizione.Replace("0", "%")
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    End If

        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C3") Then
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.2" & Chr(34) & vbCrLf
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    descrizione = DirectCast(CTRL, CheckBox).ID.Substring(10)
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    If descrizione.Contains("0") Then
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '        descrizione = descrizione.Replace("0", "%")
        '        '        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    End If

        '        '    End If



        '        '    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("MAT") Then
        '        '        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "MATERIALI" & Chr(34) & vbCrLf
        '        '        ''''' ******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE
        '        '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '        '        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_MATERIALI where DESCRIZIONE like '%" & descrizione & "%'"
        '        '        par.cmd.CommandText = StrSql
        '        '        myReader = par.cmd.ExecuteReader

        '        '        If myReader.Read Then
        '        '            idtipo = myReader(0)
        '        '            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
        '        '        End If
        '        '        myReader.Close()
        '        '        ''''''*****FINE *****
        '        '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("INT") Then
        '        '        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "INTERVENTI" & Chr(34) & vbCrLf
        '        '        '''''******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE

        '        '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '        '        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_INTERVENTI where DESCRIZIONE like '%" & descrizione & "%'"
        '        '        par.cmd.CommandText = StrSql
        '        '        myReader = par.cmd.ExecuteReader

        '        '        If myReader.Read Then
        '        '            idtipo = myReader(0)
        '        '            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        '        '        End If
        '        '        myReader.Close()
        '        '        ''''*****FINE *****

        '        '    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("ANA") Then

        '        '        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANALISI_PRESTAZIONALE" & Chr(34) & vbCrLf
        '        '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '        '        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANALISI where DESCRIZIONE like '%" & descrizione & "%'"
        '        '        par.cmd.CommandText = StrSql
        '        '        myReader = par.cmd.ExecuteReader

        '        '        If myReader.Read Then
        '        '            idtipo = myReader(0)
        '        '            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        '        '        End If
        '        '        myReader.Close()
        '        '        '''*****FINE *****


        '        '    End If
        '        '    TxtNote_1.Text = TxtNote_1.Text & stringa
        '        '    TxtNote_1.Text = TxtNote_1.Text & vbCrLf



        '        If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_1" AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_2" Then
        '            stringa = "Me."
        '            stringa = stringa & DirectCast(CTRL, TextBox).ID.ToString()

        '            If DirectCast(CTRL, TextBox).ID.ToString().Contains("R1") Then
        '                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "R1" & Chr(34) & vbCrLf
        '                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                If descrizione.Contains("0") Then
        '                    descrizione = descrizione.Replace("0", "%")
        '                End If

        '            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("R2") Then
        '                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "R2" & Chr(34) & vbCrLf
        '                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                If descrizione.Contains("0") Then
        '                    descrizione = descrizione.Replace("0", "%")
        '                End If
        '                ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("Q3") Then
        '                ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q3" & Chr(34) & vbCrLf
        '                ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                ''                If descrizione.Contains("0") Then
        '                ''                    descrizione = descrizione.Replace("0", "%")
        '                ''                End If

        '                ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("Q4") Then
        '                ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q4" & Chr(34) & vbCrLf
        '                ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                ''                If descrizione.Contains("0") Then
        '                ''                    descrizione = descrizione.Replace("0", "%")
        '                ''                End If
        '                ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("Q5") Then
        '                ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q5" & Chr(34) & vbCrLf
        '                ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                ''                If descrizione.Contains("0") Then
        '                ''                    descrizione = descrizione.Replace("0", "%")
        '                ''                End If



        '                ''                ''                ''                '                ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("L4") Then
        '                ''                ''                ''                '                ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "L4" & Chr(34) & vbCrLf
        '                ''                ''                ''                '                ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                ''                ''                ''                '                ''                If descrizione.Contains("0") Then
        '                ''                ''                ''                '                ''                    descrizione = descrizione.Replace("0", "%")
        '                ''                ''                ''                '                ''                End If


        '                ''                ''                ''                '                ''                ''                'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        '                ''                ''                ''                '                ''                ''                '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
        '                ''                ''                ''                '                ''                ''                '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                ''                ''                ''                '                ''                ''                '    If descrizione.Contains("0") Then
        '                ''                ''                ''                '                ''                ''                '        descrizione = descrizione.Replace("0", "%")
        '                ''                ''                ''                '                ''                ''                '    End If
        '                ''                ''                ''                '                ''                ''                'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        '                ''                ''                ''                '                ''                ''                '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "D1.1" & Chr(34) & vbCrLf
        '                ''                ''                ''                '                ''                ''                '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                ''                ''                ''                '                ''                ''                '    If descrizione.Contains("0") Then
        '                ''                ''                ''                '                ''                ''                '        descrizione = descrizione.Replace("0", "%")
        '                ''                'End If
        '            End If
        '            stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANOMALIE" & Chr(34) & vbCrLf

        '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '            StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANOMALIE where DESCRIZIONE like '%" & descrizione & "%'"
        '            par.cmd.CommandText = StrSql
        '            myReader = par.cmd.ExecuteReader

        '            If myReader.Read Then
        '                idtipo = myReader(0)
        '                stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
        '            End If
        '            myReader.Close()

        '            stringa = stringa & vbCrLf
        '            TxtNote_1.Text = TxtNote_1.Text & stringa
        '            TxtNote_1.Text = TxtNote_1.Text & vbCrLf

        '        End If
        '    Next


        'Catch ex As Exception
        '    Dim s As New Exception(DirectCast(CTRL, CheckBox).ID)
        '    par.OracleConn.Close()
        '    par.SettaCommand(par)
        'End Try
        'par.OracleConn.Close()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Checked = True
            ElseIf TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Text = "55"
            End If
        Next
    End Sub
    Private Sub SettaTxtAnomalie()

        Me.T_ANO_R1_instabilità.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.T_ANO_R1_instabilità.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_R1_instabilità.Attributes("ID_TIPO") = "10"


        Me.T_ANO_R1_disuso.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.T_ANO_R1_disuso.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_R1_disuso.Attributes("ID_TIPO") = "143"


        Me.T_ANO_R2_centralino0difficoltoso.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.T_ANO_R2_centralino0difficoltoso.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_R2_centralino0difficoltoso.Attributes("ID_TIPO") = "247"


        Me.T_ANO_R2_mancata0protezione0centralino.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.T_ANO_R2_mancata0protezione0centralino.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_R2_mancata0protezione0centralino.Attributes("ID_TIPO") = "248"


        Me.T_ANO_R2_anomalie0ricezione.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.T_ANO_R2_anomalie0ricezione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_R2_anomalie0ricezione.Attributes("ID_TIPO") = "249"


    End Sub
    Private Sub SettaChkAnalisi()
        Me.C_ANA_R1_forte.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_ANA_R1_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_R1_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_R1_lieve0obsolescenza.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_ANA_R1_lieve0obsolescenza.Attributes("ID_TIPO") = "2"
        Me.C_ANA_R1_lieve0obsolescenza.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_R1_nuovo.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_ANA_R1_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_R1_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_R1_tot.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_ANA_R1_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_R1_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_R2_forte.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_ANA_R2_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_R2_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_R2_lieve.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_ANA_R2_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_R2_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_R2_nuovo.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_ANA_R2_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_R2_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_R2_tot.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_ANA_R2_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_R2_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

    End Sub
    Private Sub SettaChkInterventi()
        Me.C_INT_R1_rimozione0disuso.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_INT_R1_rimozione0disuso.Attributes("ID_TIPO") = "151"
        Me.C_INT_R1_rimozione0disuso.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_R1_sostituzione0ancoraggio.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_INT_R1_sostituzione0ancoraggio.Attributes("ID_TIPO") = "242"
        Me.C_INT_R1_sostituzione0ancoraggio.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_R1_sostituzione0antenna.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_INT_R1_sostituzione0antenna.Attributes("ID_TIPO") = "241"
        Me.C_INT_R1_sostituzione0antenna.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_R2_sostituzione0centralino.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_INT_R2_sostituzione0centralino.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_R2_sostituzione0centralino.Attributes("ID_TIPO") = "341"

        Me.C_INT_R2_spostamento0centralino.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_INT_R2_spostamento0centralino.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_R2_spostamento0centralino.Attributes("ID_TIPO") = "245"

        Me.C_INT_R2_verifica0impianto.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_INT_R2_verifica0impianto.Attributes("ID_TIPO") = "246"
        Me.C_INT_R2_verifica0impianto.Attributes("TABELLA_DB") = "INTERVENTI"

    End Sub
    Private Sub SettaChkMateriali()
        Me.C_MAT_R1_centralizzata.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_MAT_R1_centralizzata.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_R1_centralizzata.Attributes("ID_TIPO") = "324"

        Me.C_MAT_R1_satellitare.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_MAT_R1_satellitare.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_R1_satellitare.Attributes("ID_TIPO") = "321"

        Me.C_MAT_R1_satellitare0centralizzata.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_MAT_R1_satellitare0centralizzata.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_R1_satellitare0centralizzata.Attributes("ID_TIPO") = "322"

        Me.C_MAT_R1_singola.Attributes("COD_TIPO_ELEMENTO") = "R1"
        Me.C_MAT_R1_singola.Attributes("ID_TIPO") = "282"
        Me.C_MAT_R1_singola.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_R2_centralino0tv.Attributes("COD_TIPO_ELEMENTO") = "R2"
        Me.C_MAT_R2_centralino0tv.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_R2_centralino0tv.Attributes("ID_TIPO") = "323"


    End Sub
    Private Sub settamq()
        Me.Text_mq_R1.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_R1.Attributes("COD_TIPO_ELEMENTO") = "R1"

        Me.Text_mq_R2.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_R2.Attributes("COD_TIPO_ELEMENTO") = "R2"

    End Sub
    '*******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE *********
    Private Sub SettaChkDegrado()
        Me.ChkSt1.Attributes("COD_TIPO_SCHEDA") = "R"
        Me.ChkSt1.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt1.Attributes("STATO") = "1"

        Me.ChkSt21.Attributes("COD_TIPO_SCHEDA") = "R"
        Me.ChkSt21.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt21.Attributes("STATO") = "2.1"

        Me.ChkSt22.Attributes("COD_TIPO_SCHEDA") = "R"
        Me.ChkSt22.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt22.Attributes("STATO") = "2.2"

        Me.ChkSt31.Attributes("COD_TIPO_SCHEDA") = "R"
        Me.ChkSt31.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt31.Attributes("STATO") = "3.1"

        Me.ChkSt32.Attributes("COD_TIPO_SCHEDA") = "R"
        Me.ChkSt32.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt32.Attributes("STATO") = "3.2"

        Me.ChkSt33.Attributes("COD_TIPO_SCHEDA") = "R"
        Me.ChkSt33.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt33.Attributes("STATO") = "3.3"
    End Sub
    'fine aggiunta stato degrado
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

End Class
