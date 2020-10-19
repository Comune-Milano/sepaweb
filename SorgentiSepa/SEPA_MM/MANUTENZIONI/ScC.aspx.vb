﻿Imports System.Collections.Generic

Partial Class ScE
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


    'Protected Sub Crea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Crea.Click
    ''Dim CTRL As Control=nothing
    ''Dim StrSql As String
    ''Dim descrizione As String
    ''Dim idtipo As String
    ''Dim stringa As String = ""
    ''If par.OracleConn.State = Data.ConnectionState.Open Then
    ''    par.OracleConn.Close()
    ''    Exit Sub
    ''Else
    ''    par.OracleConn.Open()
    ''    par.SettaCommand(par)
    ''End If
    ''Try


    ''    For Each CTRL In Me.form1.Controls


    ''        If TypeOf CTRL Is CheckBox Then
    ''            stringa = "Me."
    ''            stringa = stringa & DirectCast(CTRL, CheckBox).ID.ToString()

    ''            If DirectCast(CTRL, CheckBox).ID.ToString().Contains("C1") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C1" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If
    ''            ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C21") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.1" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, CheckBox).ID.Substring(10)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If
    ''            ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C2") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If

    ''            ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C3") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.2" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, CheckBox).ID.Substring(10)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If

    ''            End If


    ''            If DirectCast(CTRL, CheckBox).ID.ToString().Contains("MAT") Then
    ''                stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "MATERIALI" & Chr(34) & vbCrLf
    ''                '******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE
    ''                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
    ''                StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_MATERIALI where DESCRIZIONE like '%" & descrizione & "%'"
    ''                par.cmd.CommandText = StrSql
    ''                myReader = par.cmd.ExecuteReader

    ''                If myReader.Read Then
    ''                    idtipo = myReader(0)
    ''                    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
    ''                End If
    ''                myReader.Close()
    ''                '*****FINE *****
    ''            ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("INT") Then
    ''                stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "INTERVENTI" & Chr(34) & vbCrLf
    ''                '******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE

    ''                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
    ''                StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_INTERVENTI where DESCRIZIONE like '%" & descrizione & "%'"
    ''                par.cmd.CommandText = StrSql
    ''                myReader = par.cmd.ExecuteReader

    ''                If myReader.Read Then
    ''                    idtipo = myReader(0)
    ''                    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

    ''                End If
    ''                myReader.Close()
    ''                '*****FINE *****

    ''            ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("ANA") Then

    ''                stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANALISI_PRESTAZIONALE" & Chr(34) & vbCrLf
    ''                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
    ''                StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANALISI where DESCRIZIONE like '%" & descrizione & "%'"
    ''                par.cmd.CommandText = StrSql
    ''                myReader = par.cmd.ExecuteReader

    ''                If myReader.Read Then
    ''                    idtipo = myReader(0)
    ''                    stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

    ''                End If
    ''                myReader.Close()
    ''                '*****FINE *****


    ''            End If
    ''            TxtNote_1.Text = TxtNote_1.Text & stringa
    ''            TxtNote_1.Text = TxtNote_1.Text & vbCrLf

    ''        End If

    ''        If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_1" AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_2" Then
    ''            stringa = "Me."
    ''            stringa = stringa & DirectCast(CTRL, TextBox).ID.ToString()

    ''            If DirectCast(CTRL, TextBox).ID.ToString().Contains("C1") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C1" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If

    ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("C21") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.1" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If

    ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("C2") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If
    ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("C3") Then
    ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.2" & Chr(34) & vbCrLf
    ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
    ''                If descrizione.Contains("0") Then
    ''                    descrizione = descrizione.Replace("0", "%")
    ''                End If
    ''            End If
    ''            stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANOMALIE" & Chr(34) & vbCrLf

    ''            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
    ''            StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANOMALIE where DESCRIZIONE like '%" & descrizione & "%'"
    ''            par.cmd.CommandText = StrSql
    ''            myReader = par.cmd.ExecuteReader

    ''            If myReader.Read Then
    ''                idtipo = myReader(0)
    ''                stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
    ''            End If
    ''            myReader.Close()

    ''            stringa = stringa & vbCrLf
    ''            TxtNote_1.Text = TxtNote_1.Text & stringa
    ''            TxtNote_1.Text = TxtNote_1.Text & vbCrLf

    ''        End If
    ''    Next


    ''Catch ex As Exception
    ''    Dim s As New Exception(DirectCast(CTRL, CheckBox).ID)
    ''    par.OracleConn.Close()
    ''    par.SettaCommand(par)
    ''End Try
    ''par.OracleConn.Close()
    ' Dim CTRL As Control=nothing
    'For Each ctrl In Me.form1.Controls
    '  If TypeOf ctrl Is CheckBox Then
    '   DirectCast(ctrl, CheckBox).Checked = True
    '  ElseIf TypeOf ctrl Is TextBox Then
    '  DirectCast(ctrl, TextBox).Text = "55"
    '  ' End If
    ' Next

    'End Sub
    Private Sub SettaChkAnalisi()
        Me.C_ANA_C1_forte.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_ANA_C1_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_C1_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C1_lieve.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_ANA_C1_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_C1_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C1_nuovo.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_ANA_C1_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_C1_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C1_tot.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_ANA_C1_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_C1_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C2_forte.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_ANA_C2_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_C2_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C2_lieve.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_ANA_C2_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_C2_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C2_nuovo.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_ANA_C2_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_C2_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C2_tot.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_ANA_C2_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_C2_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C3_forte.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_ANA_C3_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_C3_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C3_lieve.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_ANA_C3_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_C3_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C3_nuovo.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_ANA_C3_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_C3_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_C3_totale.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_ANA_C3_totale.Attributes("ID_TIPO") = "4"
        Me.C_ANA_C3_totale.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

    End Sub
    Private Sub SettaChkMateriali()
        Me.C_MAT_C1_blocchi0laterizio.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_MAT_C1_blocchi0laterizio.Attributes("ID_TIPO") = "41"
        Me.C_MAT_C1_blocchi0laterizio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C1_cartongesso.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_MAT_C1_cartongesso.Attributes("ID_TIPO") = "121"
        Me.C_MAT_C1_cartongesso.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C1_pannelli0prefab.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_MAT_C1_pannelli0prefab.Attributes("ID_TIPO") = "26"
        Me.C_MAT_C1_pannelli0prefab.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C1_vetro0cemento.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_MAT_C1_vetro0cemento.Attributes("ID_TIPO") = "44"
        Me.C_MAT_C1_vetro0cemento.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C2_telaio0alluminio.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_MAT_C2_telaio0alluminio.Attributes("ID_TIPO") = "55"
        Me.C_MAT_C2_telaio0alluminio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C2_telaio0ferro.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_MAT_C2_telaio0ferro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_C2_telaio0ferro.Attributes("ID_TIPO") = "56"

        Me.C_MAT_C2_telaio0legno.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_MAT_C2_telaio0legno.Attributes("ID_TIPO") = "54"
        Me.C_MAT_C2_telaio0legno.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C2_telaio0misto.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_MAT_C2_telaio0misto.Attributes("ID_TIPO") = "58"
        Me.C_MAT_C2_telaio0misto.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C2_telaio0pvc.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_MAT_C2_telaio0pvc.Attributes("ID_TIPO") = "57"
        Me.C_MAT_C2_telaio0pvc.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C21_doppio.Attributes("COD_TIPO_ELEMENTO") = "C2.1"
        Me.C_MAT_C21_doppio.Attributes("ID_TIPO") = "53"
        Me.C_MAT_C21_doppio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C21_rete.Attributes("COD_TIPO_ELEMENTO") = "C2.1"
        Me.C_MAT_C21_rete.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_C21_rete.Attributes("ID_TIPO") = "126"

        Me.C_MAT_C21_retinato.Attributes("COD_TIPO_ELEMENTO") = "C2.1"
        Me.C_MAT_C21_retinato.Attributes("ID_TIPO") = "61"
        Me.C_MAT_C21_retinato.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C21_singolo.Attributes("COD_TIPO_ELEMENTO") = "C2.1"
        Me.C_MAT_C21_singolo.Attributes("ID_TIPO") = "52"
        Me.C_MAT_C21_singolo.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C21_vetro.Attributes("COD_TIPO_ELEMENTO") = "C2.1"
        Me.C_MAT_C21_vetro.Attributes("ID_TIPO") = "24"
        Me.C_MAT_C21_vetro.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_battente.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_battente.Attributes("ID_TIPO") = "70"
        Me.C_MAT_C3_battente.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_doppio.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_doppio.Attributes("ID_TIPO") = "53"
        Me.C_MAT_C3_doppio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_modalita0apertur.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_modalita0apertur.Attributes("ID_TIPO") = "122"
        Me.C_MAT_C3_modalita0apertur.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_REI.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_REI.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_C3_REI.Attributes("ID_TIPO") = "123"

        Me.C_MAT_C3_rete0metallica.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_rete0metallica.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_C3_rete0metallica.Attributes("ID_TIPO") = "124"

        Me.C_MAT_C3_scorrevole.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_scorrevole.Attributes("ID_TIPO") = "72"
        Me.C_MAT_C3_scorrevole.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_singolo.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_singolo.Attributes("ID_TIPO") = "52"
        Me.C_MAT_C3_singolo.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_telaio0alluminio.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_telaio0alluminio.Attributes("ID_TIPO") = "55"
        Me.C_MAT_C3_telaio0alluminio.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_telaio0ferro.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_telaio0ferro.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_C3_telaio0ferro.Attributes("ID_TIPO") = "56"

        Me.C_MAT_C3_telaio0legno.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_telaio0legno.Attributes("ID_TIPO") = "54"
        Me.C_MAT_C3_telaio0legno.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_telaio0misto.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_telaio0misto.Attributes("ID_TIPO") = "58"
        Me.C_MAT_C3_telaio0misto.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_telaio0pvc.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_telaio0pvc.Attributes("ID_TIPO") = "57"
        Me.C_MAT_C3_telaio0pvc.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_C3_vesistas.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_vesistas.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_C3_vesistas.Attributes("ID_TIPO") = "125"

        Me.C_MAT_C3_vetro.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_MAT_C3_vetro.Attributes("ID_TIPO") = "24"
        Me.C_MAT_C3_vetro.Attributes("TABELLA_DB") = "MATERIALI"

    End Sub

    Private Sub SettaChkInterventi()
        Me.C_INT_C1_fissaggio0finitura.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_INT_C1_fissaggio0finitura.Attributes("ID_TIPO") = "68"
        Me.C_INT_C1_fissaggio0finitura.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C1_pulizia.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_INT_C1_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_C1_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C1_ricostruzione.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_INT_C1_ricostruzione.Attributes("ID_TIPO") = "14"
        Me.C_INT_C1_ricostruzione.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C1_rifacimento0finitura.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_INT_C1_rifacimento0finitura.Attributes("ID_TIPO") = "46"
        Me.C_INT_C1_rifacimento0finitura.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C1_risanamento0conservativo.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_INT_C1_risanamento0conservativo.Attributes("ID_TIPO") = "45"
        Me.C_INT_C1_risanamento0conservativo.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C1_sostituzione0serramenti.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.C_INT_C1_sostituzione0serramenti.Attributes("ID_TIPO") = "50"
        Me.C_INT_C1_sostituzione0serramenti.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C2_integrale0serramento.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_INT_C2_integrale0serramento.Attributes("ID_TIPO") = "53"
        Me.C_INT_C2_integrale0serramento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C2_pulizia.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_INT_C2_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_C2_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C2_semplice0serramento.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_INT_C2_semplice0serramento.Attributes("ID_TIPO") = "54"
        Me.C_INT_C2_semplice0serramento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C2_sostituzione0lastre.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.C_INT_C2_sostituzione0lastre.Attributes("ID_TIPO") = "19"
        Me.C_INT_C2_sostituzione0lastre.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_anticorrosivo0metallica.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_anticorrosivo0metallica.Attributes("ID_TIPO") = "88"
        Me.C_INT_C3_anticorrosivo0metallica.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_integrale0oscuramento.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_integrale0oscuramento.Attributes("ID_TIPO") = "86"
        Me.C_INT_C3_integrale0oscuramento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_integrale0serramento.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_integrale0serramento.Attributes("ID_TIPO") = "53"
        Me.C_INT_C3_integrale0serramento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_pulizia.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_pulizia.Attributes("ID_TIPO") = "17"
        Me.C_INT_C3_pulizia.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_semplice0oscuramento.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_semplice0oscuramento.Attributes("ID_TIPO") = "62"
        Me.C_INT_C3_semplice0oscuramento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_semplice0serramento.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_semplice0serramento.Attributes("ID_TIPO") = "54"
        Me.C_INT_C3_semplice0serramento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_sostituzione0lastre.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_sostituzione0lastre.Attributes("ID_TIPO") = "19"
        Me.C_INT_C3_sostituzione0lastre.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_sostituzione0oscuramento.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_sostituzione0oscuramento.Attributes("ID_TIPO") = "60"
        Me.C_INT_C3_sostituzione0oscuramento.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_C3_sostituzione0serramenti.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.C_INT_C3_sostituzione0serramenti.Attributes("ID_TIPO") = "50"
        Me.C_INT_C3_sostituzione0serramenti.Attributes("TABELLA_DB") = "INTERVENTI"

    End Sub


    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "COMP"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE id_complesso = " & vId & " AND COD_TIPO_SCHEDA LIKE '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "UC"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA like '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%C%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA LIKE '%C%'"
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
    Private Sub SalvaTextNote()

        Dim STRINSERT As String
        par.OracleConn.Open()
        par.SettaCommand(par)
        Try
            If Me.TxtNote_1.Text <> "" Or Me.TxtNote_2.Text <> "" Then

                Select Case vTipo
                    Case "EDIF"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_EDIFICIO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'C','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                    Case "COMP"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_COMPLESSO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'C','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                    Case "UC"
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_UNITA_COMUNE,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'C','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_COMPLESSO = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANALISI_PRESTAZIONALE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANOMALIE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_UNITA_COMUNE = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%C%' "
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
    '*******AGGIUNTA STATO DEGRADO 08/07/2009 PEPPE *********
    Private Sub SettaChkDegrado()
        Me.ChkSt1.Attributes("COD_TIPO_SCHEDA") = "C"
        Me.ChkSt1.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt1.Attributes("STATO") = "1"

        Me.ChkSt21.Attributes("COD_TIPO_SCHEDA") = "C"
        Me.ChkSt21.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt21.Attributes("STATO") = "2.1"

        Me.ChkSt22.Attributes("COD_TIPO_SCHEDA") = "C"
        Me.ChkSt22.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt22.Attributes("STATO") = "2.2"

        Me.ChkSt31.Attributes("COD_TIPO_SCHEDA") = "C"
        Me.ChkSt31.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt31.Attributes("STATO") = "3.1"

        Me.ChkSt32.Attributes("COD_TIPO_SCHEDA") = "C"
        Me.ChkSt32.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt32.Attributes("STATO") = "3.2"

        Me.ChkSt33.Attributes("COD_TIPO_SCHEDA") = "C"
        Me.ChkSt33.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt33.Attributes("STATO") = "3.3"


    End Sub
    'fine aggiunta stato degrado
    Private Sub SettaTxtAnomalie()
        Me.T_ANO_C1_graffiti.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.T_ANO_C1_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C1_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_C1_fessurazioni.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.T_ANO_C1_fessurazioni.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C1_fessurazioni.Attributes("ID_TIPO") = "32"

        Me.T_ANO_C1_distacchi0finitura.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.T_ANO_C1_distacchi0finitura.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C1_distacchi0finitura.Attributes("ID_TIPO") = "14"

        Me.T_ANO_C1_distacco0portante.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.T_ANO_C1_distacco0portante.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C1_distacco0portante.Attributes("ID_TIPO") = "66"

        Me.T_ANO_C1_ritenzione0umidita.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.T_ANO_C1_ritenzione0umidita.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C1_ritenzione0umidita.Attributes("ID_TIPO") = "67"

        Me.T_ANO_C2_corrosione0profili.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.T_ANO_C2_corrosione0profili.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C2_corrosione0profili.Attributes("ID_TIPO") = "36"

        Me.T_ANO_C2_rottura0lastra.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.T_ANO_C2_rottura0lastra.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C2_rottura0lastra.Attributes("ID_TIPO") = "9"

        Me.T_ANO_C2_graffiti.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.T_ANO_C2_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C2_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_C2_degrado0guarnizioni.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.T_ANO_C2_degrado0guarnizioni.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C2_degrado0guarnizioni.Attributes("ID_TIPO") = "39"

        Me.T_ANO_C2_sporco.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.T_ANO_C2_sporco.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C2_sporco.Attributes("ID_TIPO") = "40"

        Me.T_ANO_C21_distacchi0muratura.Attributes("COD_TIPO_ELEMENTO") = "C2.1"
        Me.T_ANO_C21_distacchi0muratura.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C21_distacchi0muratura.Attributes("ID_TIPO") = "41"

        Me.T_ANO_C3_corrosione0profili.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_corrosione0profili.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_corrosione0profili.Attributes("ID_TIPO") = "36"

        Me.T_ANO_C3_rottura0lastra.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_rottura0lastra.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_rottura0lastra.Attributes("ID_TIPO") = "9"

        Me.T_ANO_C3_graffiti.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_graffiti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_graffiti.Attributes("ID_TIPO") = "8"

        Me.T_ANO_C3_degrado0guarnizioni.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_degrado0guarnizioni.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_degrado0guarnizioni.Attributes("ID_TIPO") = "39"

        Me.T_ANO_C3_sporco.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_sporco.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_sporco.Attributes("ID_TIPO") = "40"

        Me.T_ANO_C3_distacchi0muratura.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_distacchi0muratura.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_distacchi0muratura.Attributes("ID_TIPO") = "41"

        Me.T_ANO_C3_malfunzionamento0organi.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_malfunzionamento0organi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_malfunzionamento0organi.Attributes("ID_TIPO") = "48"

        Me.T_ANO_C3_movimenti0serramenti.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.T_ANO_C3_movimenti0serramenti.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_C3_movimenti0serramenti.Attributes("ID_TIPO") = "81"

    End Sub

    Private Sub settamq()
        Me.Text_mq_C1.Attributes("COD_TIPO_ELEMENTO") = "C1"
        Me.Text_mq_C1.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_C2.Attributes("COD_TIPO_ELEMENTO") = "C2"
        Me.Text_mq_C2.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"

        Me.Text_mq_C22.Attributes("COD_TIPO_ELEMENTO") = "C2.2"
        Me.Text_mq_C22.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"


    End Sub
    Protected Sub Crea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Crea.Click
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
