Imports System.Collections.Generic

Partial Class ScI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public DictMat As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAna As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictInerv As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAnomalie As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)
    'Aggiunta STATO DEGRADO 08/07/2009
    Public DictDegrado As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictElementi As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)

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


        '        ' ''If TypeOf CTRL Is CheckBox Then
        '        ' ''    stringa = "Me."
        '        ' ''    stringa = stringa & DirectCast(CTRL, CheckBox).ID.ToString()

        '        ' ''    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("I1") Then
        '        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I1" & Chr(34) & vbCrLf
        '        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        ' ''        If descrizione.Contains("0") Then
        '        ' ''            descrizione = descrizione.Replace("0", "%")
        '        ' ''        End If
        '        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("I2") Then
        '        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I2" & Chr(34) & vbCrLf
        '        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        ' ''        If descrizione.Contains("0") Then
        '        ' ''            descrizione = descrizione.Replace("0", "%")
        '        ' ''        End If
        '        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("I3") Then
        '        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I3" & Chr(34) & vbCrLf
        '        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        ' ''        If descrizione.Contains("0") Then
        '        ' ''            descrizione = descrizione.Replace("0", "%")
        '        ' ''        End If
        '        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("I4") Then
        '        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I4" & Chr(34) & vbCrLf
        '        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        '        ' ''        If descrizione.Contains("0") Then
        '        ' ''            descrizione = descrizione.Replace("0", "%")
        '        ' ''        End If


        '        ' ''    End If



        '        ' ''    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("MAT") Then
        '        ' ''        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "MATERIALI" & Chr(34) & vbCrLf
        '        ' ''        '******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE
        '        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '        ' ''        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_MATERIALI where DESCRIZIONE like '%" & descrizione & "%'"
        '        ' ''        par.cmd.CommandText = StrSql
        '        ' ''        myReader = par.cmd.ExecuteReader

        '        ' ''        If myReader.Read Then
        '        ' ''            idtipo = myReader(0)
        '        ' ''            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
        '        ' ''        End If
        '        ' ''        myReader.Close()
        '        ' ''        '*****FINE *****
        '        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("INT") Then
        '        ' ''        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "INTERVENTI" & Chr(34) & vbCrLf
        '        ' ''        '******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE

        '        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '        ' ''        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_INTERVENTI where DESCRIZIONE like '%" & descrizione & "%'"
        '        ' ''        par.cmd.CommandText = StrSql
        '        ' ''        myReader = par.cmd.ExecuteReader

        '        ' ''        If myReader.Read Then
        '        ' ''            idtipo = myReader(0)
        '        ' ''            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        '        ' ''        End If
        '        ' ''        myReader.Close()
        '        ' ''        '*****FINE *****

        '        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("ANA") Then

        '        ' ''        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANALISI_PRESTAZIONALE" & Chr(34) & vbCrLf
        '        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        '        ' ''        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANALISI where DESCRIZIONE like '%" & descrizione & "%'"
        '        ' ''        par.cmd.CommandText = StrSql
        '        ' ''        myReader = par.cmd.ExecuteReader

        '        ' ''        If myReader.Read Then
        '        ' ''            idtipo = myReader(0)
        '        ' ''            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        '        ' ''        End If
        '        ' ''        myReader.Close()
        '        ' ''        '*****FINE *****


        '        ' ''    End If
        '        ' ''    TxtNote_1.Text = TxtNote_1.Text & stringa
        '        ' ''    TxtNote_1.Text = TxtNote_1.Text & vbCrLf

        '        ' ''End If

        '        If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_1" AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_2" Then
        '            stringa = "Me."
        '            stringa = stringa & DirectCast(CTRL, TextBox).ID.ToString()

        '            If DirectCast(CTRL, TextBox).ID.ToString().Contains("I1") Then
        '                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I1" & Chr(34) & vbCrLf
        '                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        '                If descrizione.Contains("0") Then
        '                    descrizione = descrizione.Replace("0", "%")
        '                End If

        '            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("I2") Then
        '                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I2" & Chr(34) & vbCrLf
        '                descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                If descrizione.Contains("0") Then
        '                    descrizione = descrizione.Replace("0", "%")
        '                End If
        '            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("I3") Then
        '                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I3" & Chr(34) & vbCrLf
        '                descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                If descrizione.Contains("0") Then
        '                    descrizione = descrizione.Replace("0", "%")
        '                End If
        '            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("I4") Then
        '                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "I4" & Chr(34) & vbCrLf
        '                descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        '                If descrizione.Contains("0") Then
        '                    descrizione = descrizione.Replace("0", "%")
        '                End If


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
        '                Next


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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_COMPLESSO = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANALISI_PRESTAZIONALE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANOMALIE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_UNITA_COMUNE = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%I%' "
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
    '*******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE *********
    Private Sub SettaChkDegrado()
        Me.ChkSt1.Attributes("COD_TIPO_SCHEDA") = "I"
        Me.ChkSt1.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt1.Attributes("STATO") = "1"

        Me.ChkSt21.Attributes("COD_TIPO_SCHEDA") = "I"
        Me.ChkSt21.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt21.Attributes("STATO") = "2.1"

        Me.ChkSt22.Attributes("COD_TIPO_SCHEDA") = "I"
        Me.ChkSt22.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt22.Attributes("STATO") = "2.2"

        Me.ChkSt31.Attributes("COD_TIPO_SCHEDA") = "I"
        Me.ChkSt31.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt31.Attributes("STATO") = "3.1"

        Me.ChkSt32.Attributes("COD_TIPO_SCHEDA") = "I"
        Me.ChkSt32.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt32.Attributes("STATO") = "3.2"

        Me.ChkSt33.Attributes("COD_TIPO_SCHEDA") = "I"
        Me.ChkSt33.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt33.Attributes("STATO") = "3.3"


    End Sub
    'fine aggiunta stato degrado
    Private Sub SettaChkAnalisi()
        Me.C_ANA_I1_forte.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_ANA_I1_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_I1_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I1_lieve.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_ANA_I1_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_I1_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I1_nuovo.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_ANA_I1_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_I1_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I1_tot.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_ANA_I1_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_I1_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I2_forte.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_ANA_I2_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_I2_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I2_lieve.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_ANA_I2_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_I2_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I2_nuovo.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_ANA_I2_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_I2_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I2_tot.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_ANA_I2_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_I2_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I3_forte.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_ANA_I3_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_I3_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I3_lieve.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_ANA_I3_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_I3_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I3_nuovo.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_ANA_I3_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_I3_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I3_tot.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_ANA_I3_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_I3_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I4_forte.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_ANA_I4_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_I4_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I4_lieve.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_ANA_I4_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_I4_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I4_nuovo.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_ANA_I4_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_I4_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_I4_tot.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_ANA_I4_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_I4_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"


    End Sub
    Private Sub SettaChkInterventi()
        Me.C_INT_I1_bonifica0serbatoio.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_INT_I1_bonifica0serbatoio.Attributes("ID_TIPO") = "183"
        Me.C_INT_I1_bonifica0serbatoio.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I1_spostamento0contatore.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_INT_I1_spostamento0contatore.Attributes("ID_TIPO") = "182"
        Me.C_INT_I1_spostamento0contatore.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I1_tenuta0pressione0adduzione.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_INT_I1_tenuta0pressione0adduzione.Attributes("ID_TIPO") = "181"
        Me.C_INT_I1_tenuta0pressione0adduzione.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I1_tenuta0serbatoio.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_INT_I1_tenuta0serbatoio.Attributes("ID_TIPO") = "180"
        Me.C_INT_I1_tenuta0serbatoio.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I2_analisi0amianto.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_INT_I2_analisi0amianto.Attributes("ID_TIPO") = "184"
        Me.C_INT_I2_analisi0amianto.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I2_eliminazione0perdite.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_INT_I2_eliminazione0perdite.Attributes("ID_TIPO") = "186"
        Me.C_INT_I2_eliminazione0perdite.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I2_ripristino0coibentazione.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_INT_I2_ripristino0coibentazione.Attributes("ID_TIPO") = "185"
        Me.C_INT_I2_ripristino0coibentazione.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I3_analisi0amianto.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_INT_I3_analisi0amianto.Attributes("ID_TIPO") = "184"
        Me.C_INT_I3_analisi0amianto.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I3_distacco0scaldanti.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_INT_I3_distacco0scaldanti.Attributes("ID_TIPO") = "189"
        Me.C_INT_I3_distacco0scaldanti.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I3_eliminazione0perdite.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_INT_I3_eliminazione0perdite.Attributes("ID_TIPO") = "186"
        Me.C_INT_I3_eliminazione0perdite.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I4_analisi0amianto.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_INT_I4_analisi0amianto.Attributes("ID_TIPO") = "184"
        Me.C_INT_I4_analisi0amianto.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I4_intervento0risanamento.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_INT_I4_intervento0risanamento.Attributes("ID_TIPO") = "64"
        Me.C_INT_I4_intervento0risanamento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I4_sostituzione0elemento.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_INT_I4_sostituzione0elemento.Attributes("ID_TIPO") = "107"
        Me.C_INT_I4_sostituzione0elemento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_I4_strutture0rinforzo.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_INT_I4_strutture0rinforzo.Attributes("ID_TIPO") = "36"
        Me.C_INT_I4_strutture0rinforzo.Attributes("TABELLA_DB") = "INTERVENTI"


    End Sub
    Private Sub SettaChkMateriali()
        Me.C_MAT_I1_contatore.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_MAT_I1_contatore.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I1_contatore.Attributes("ID_TIPO") = "222"

        Me.C_MAT_I1_linee0adduzione.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_MAT_I1_linee0adduzione.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I1_linee0adduzione.Attributes("ID_TIPO") = "223"

        Me.C_MAT_I1_serbatoio.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_MAT_I1_serbatoio.Attributes("ID_TIPO") = "221"
        Me.C_MAT_I1_serbatoio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_I1_teleriscaldamento.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.C_MAT_I1_teleriscaldamento.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I1_teleriscaldamento.Attributes("ID_TIPO") = "224"

        Me.C_MAT_I2_a0vista.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_MAT_I2_a0vista.Attributes("ID_TIPO") = "49"
        Me.C_MAT_I2_a0vista.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_I2_immuratura.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.C_MAT_I2_immuratura.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I2_immuratura.Attributes("ID_TIPO") = "225"

        Me.C_MAT_I3_espansione0ct.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_MAT_I3_espansione0ct.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I3_espansione0ct.Attributes("ID_TIPO") = "226"

        Me.C_MAT_I3_espansione0sotto0tetto.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_MAT_I3_espansione0sotto0tetto.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I3_espansione0sotto0tetto.Attributes("ID_TIPO") = "227"

        Me.C_MAT_I3_scaldanti0comuni.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.C_MAT_I3_scaldanti0comuni.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I3_scaldanti0comuni.Attributes("ID_TIPO") = "228"

        Me.C_MAT_I4_fumaria0esterna.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_MAT_I4_fumaria0esterna.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I4_fumaria0esterna.Attributes("ID_TIPO") = "229"

        Me.C_MAT_I4_fumaria0interna.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.C_MAT_I4_fumaria0interna.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_I4_fumaria0interna.Attributes("ID_TIPO") = "230"

    End Sub
    Private Sub SettaTxtAnomalie()
        Me.T_ANO_I1_perdita0serbatoio.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.T_ANO_I1_perdita0serbatoio.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I1_perdita0serbatoio.Attributes("ID_TIPO") = "176"

        Me.T_ANO_I1_odore0gas.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.T_ANO_I1_odore0gas.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I1_odore0gas.Attributes("ID_TIPO") = "177"

        Me.T_ANO_I1_contatori0difficoltoso.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.T_ANO_I1_contatori0difficoltoso.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I1_contatori0difficoltoso.Attributes("ID_TIPO") = "178"

        Me.T_ANO_I1_serbatoio0disuso.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.T_ANO_I1_serbatoio0disuso.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I1_serbatoio0disuso.Attributes("ID_TIPO") = "179"

        Me.T_ANO_I2_perdite0rete.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.T_ANO_I2_perdite0rete.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I2_perdite0rete.Attributes("ID_TIPO") = "180"

        Me.T_ANO_I2_mancanza0coibentazione.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.T_ANO_I2_mancanza0coibentazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I2_mancanza0coibentazione.Attributes("ID_TIPO") = "181"

        Me.T_ANO_I2_ruggine.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.T_ANO_I2_ruggine.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I2_ruggine.Attributes("ID_TIPO") = "182"

        Me.T_ANO_I2_distacchi0coibentazione.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.T_ANO_I2_distacchi0coibentazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I2_distacchi0coibentazione.Attributes("ID_TIPO") = "183"

        Me.T_ANO_I2_sospetta0amianto.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.T_ANO_I2_sospetta0amianto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I2_sospetta0amianto.Attributes("ID_TIPO") = "184"

        Me.T_ANO_I3_scaldanti0caldi.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.T_ANO_I3_scaldanti0caldi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I3_scaldanti0caldi.Attributes("ID_TIPO") = "185"

        Me.T_ANO_I3_perdite0scaldanti.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.T_ANO_I3_perdite0scaldanti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I3_perdite0scaldanti.Attributes("ID_TIPO") = "186"

        Me.T_ANO_I3_ruggine.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.T_ANO_I3_ruggine.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I3_ruggine.Attributes("ID_TIPO") = "182"

        Me.T_ANO_I3_sospetta0amianto.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.T_ANO_I3_sospetta0amianto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I3_sospetta0amianto.Attributes("ID_TIPO") = "184"

        Me.T_ANO_I3_perdite0espansione.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.T_ANO_I3_perdite0espansione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I3_perdite0espansione.Attributes("ID_TIPO") = "189"

        Me.T_ANO_I4_presenza0amianto.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.T_ANO_I4_presenza0amianto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I4_presenza0amianto.Attributes("ID_TIPO") = "184"

        Me.T_ANO_I4_fessurazioni0strutturali.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.T_ANO_I4_fessurazioni0strutturali.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I4_fessurazioni0strutturali.Attributes("ID_TIPO") = "191"

        Me.T_ANO_I4_instab0strutturale.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.T_ANO_I4_instab0strutturale.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I4_instab0strutturale.Attributes("ID_TIPO") = "192"

        Me.T_ANO_I4_rottura0camino.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.T_ANO_I4_rottura0camino.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_I4_rottura0camino.Attributes("ID_TIPO") = "193"

    End Sub
    Private Sub settamq()
        Me.Text_mq_I1.Attributes("COD_TIPO_ELEMENTO") = "I1"
        Me.Text_mq_I1.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_I2.Attributes("COD_TIPO_ELEMENTO") = "I2"
        Me.Text_mq_I2.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_I3.Attributes("COD_TIPO_ELEMENTO") = "I3"
        Me.Text_mq_I3.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_I4.Attributes("COD_TIPO_ELEMENTO") = "I4"
        Me.Text_mq_I4.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta
                Case "COMP"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "UC"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA like '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%I%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA LIKE '%I%'"
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
        'Response.Write("<SCRIPT>alert('Operazione eseguita correttamente!');</SCRIPT>")
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
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_EDIFICIO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'I','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""



                    Case "COMP"

                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_COMPLESSO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'I','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""



                    Case "UC"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_UNITA_COMUNE,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'I','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
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
                            If DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO") = "I3" Then
                                Beep()
                            End If
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
                            If DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO") = "I3" Then
                                Beep()
                            End If
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
                            If DirectCast(CTRL, TextBox).Attributes("COD_TIPO_ELEMENTO") = "I3" Then
                                Beep()
                            End If
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

End Class
