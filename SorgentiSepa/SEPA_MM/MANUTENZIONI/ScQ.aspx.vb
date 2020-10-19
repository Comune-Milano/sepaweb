Imports System.Collections.Generic

Partial Class ScQ
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public DictMat As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAna As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictInerv As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)
    Public DictAnomalie As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)
    Public DictElementi As New Dictionary(Of String, System.Web.UI.WebControls.TextBox)
    'Aggiunta STATO DEGRADO 08/07/2009
    Public DictDegrado As New Dictionary(Of String, System.Web.UI.WebControls.CheckBox)

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


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ''Dim CTRL As Control=nothing
        ''Dim StrSql As String
        ''Dim descrizione As String
        ''Dim idtipo As String
        ''Dim stringa As String = ""
        ''If par.OracleConn.State = Data.ConnectionState.Open Then
        ''    par.OracleConn.Close()
        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        ''    Exit Sub
        ''Else
        ''    par.OracleConn.Open()
        ''    par.SettaCommand(par)
        ''End If
        ''Try


        ''    For Each ctrl In Me.form1.Controls


        ''        ' ''If TypeOf CTRL Is CheckBox Then
        ''        ' ''    stringa = "Me."
        ''        ' ''    stringa = stringa & DirectCast(CTRL, CheckBox).ID.ToString()

        ''        ' ''    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q1") Then
        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q1" & Chr(34) & vbCrLf
        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        ''        ' ''        If descrizione.Contains("0") Then
        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        ''        ' ''        End If
        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q2") Then
        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q2" & Chr(34) & vbCrLf
        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        ''        ' ''        If descrizione.Contains("0") Then
        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        ''        ' ''        End If
        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q3") Then
        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q3" & Chr(34) & vbCrLf
        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        ''        ' ''        If descrizione.Contains("0") Then
        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        ''        ' ''        End If

        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q4") Then
        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q4" & Chr(34) & vbCrLf
        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        ''        ' ''        If descrizione.Contains("0") Then
        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        ''        ' ''        End If
        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("Q5") Then
        ''        ' ''        stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q5" & Chr(34) & vbCrLf
        ''        ' ''        descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        ''        ' ''        If descrizione.Contains("0") Then
        ''        ' ''            descrizione = descrizione.Replace("0", "%")
        ''        ' ''        End If


        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C2") Then
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    descrizione = DirectCast(CTRL, CheckBox).ID.Substring(9)
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    If descrizione.Contains("0") Then
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '        descrizione = descrizione.Replace("0", "%")
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    End If

        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        'ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("C3") Then
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2.2" & Chr(34) & vbCrLf
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    descrizione = DirectCast(CTRL, CheckBox).ID.Substring(10)
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    If descrizione.Contains("0") Then
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '        descrizione = descrizione.Replace("0", "%")
        ''        ' ''        '        ' ''        ''        ''        ''        '        ''        ''        ''        '        '            '        '    End If

        ''        ' ''    End If



        ''        ' ''    If DirectCast(CTRL, CheckBox).ID.ToString().Contains("MAT") Then
        ''        ' ''        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "MATERIALI" & Chr(34) & vbCrLf
        ''        ' ''        ''''' ******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE
        ''        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        ''        ' ''        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_MATERIALI where DESCRIZIONE like '%" & descrizione & "%'"
        ''        ' ''        par.cmd.CommandText = StrSql
        ''        ' ''        myReader = par.cmd.ExecuteReader

        ''        ' ''        If myReader.Read Then
        ''        ' ''            idtipo = myReader(0)
        ''        ' ''            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
        ''        ' ''        End If
        ''        ' ''        myReader.Close()
        ''        ' ''        ''''''*****FINE *****
        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("INT") Then
        ''        ' ''        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "INTERVENTI" & Chr(34) & vbCrLf
        ''        ' ''        '''''******SOSTITUISCO L'OCCORENZA DI 0 con % per select in DB e Prendo ID Corrispondente a LIKE DESCRIZIONE

        ''        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        ''        ' ''        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_INTERVENTI where DESCRIZIONE like '%" & descrizione & "%'"
        ''        ' ''        par.cmd.CommandText = StrSql
        ''        ' ''        myReader = par.cmd.ExecuteReader

        ''        ' ''        If myReader.Read Then
        ''        ' ''            idtipo = myReader(0)
        ''        ' ''            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        ''        ' ''        End If
        ''        ' ''        myReader.Close()
        ''        ' ''        ''''*****FINE *****

        ''        ' ''    ElseIf DirectCast(CTRL, CheckBox).ID.ToString().Contains("ANA") Then

        ''        ' ''        stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANALISI_PRESTAZIONALE" & Chr(34) & vbCrLf
        ''        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        ''        ' ''        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANALISI where DESCRIZIONE like '%" & descrizione & "%'"
        ''        ' ''        par.cmd.CommandText = StrSql
        ''        ' ''        myReader = par.cmd.ExecuteReader

        ''        ' ''        If myReader.Read Then
        ''        ' ''            idtipo = myReader(0)
        ''        ' ''            stringa = stringa & "Me." & DirectCast(CTRL, CheckBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf

        ''        ' ''        End If
        ''        ' ''        myReader.Close()
        ''        ' ''        '''*****FINE *****


        ''        ' ''    End If
        ''        ' ''    TxtNote_1.Text = TxtNote_1.Text & stringa
        ''        ' ''    TxtNote_1.Text = TxtNote_1.Text & vbCrLf

        ''        ' ''End If

        ''        If TypeOf CTRL Is TextBox AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_1" AndAlso DirectCast(CTRL, TextBox).ID.ToString() <> "TxtNote_2" Then
        ''            stringa = "Me."
        ''            stringa = stringa & DirectCast(CTRL, TextBox).ID.ToString()

        ''            If DirectCast(CTRL, TextBox).ID.ToString().Contains("Q1") Then
        ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q1" & Chr(34) & vbCrLf
        ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        ''                If descrizione.Contains("0") Then
        ''                    descrizione = descrizione.Replace("0", "%")
        ''                End If

        ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("Q2") Then
        ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q2" & Chr(34) & vbCrLf
        ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        ''                If descrizione.Contains("0") Then
        ''                    descrizione = descrizione.Replace("0", "%")
        ''                End If
        ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("Q3") Then
        ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q3" & Chr(34) & vbCrLf
        ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        ''                If descrizione.Contains("0") Then
        ''                    descrizione = descrizione.Replace("0", "%")
        ''                End If

        ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("Q4") Then
        ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q4" & Chr(34) & vbCrLf
        ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        ''                If descrizione.Contains("0") Then
        ''                    descrizione = descrizione.Replace("0", "%")
        ''                End If
        ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("Q5") Then
        ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "Q5" & Chr(34) & vbCrLf
        ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        ''                If descrizione.Contains("0") Then
        ''                    descrizione = descrizione.Replace("0", "%")
        ''                End If



        ''                ''                ''                '                ''            ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("L4") Then
        ''                ''                ''                '                ''                stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "L4" & Chr(34) & vbCrLf
        ''                ''                ''                '                ''                descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        ''                ''                ''                '                ''                If descrizione.Contains("0") Then
        ''                ''                ''                '                ''                    descrizione = descrizione.Replace("0", "%")
        ''                ''                ''                '                ''                End If


        ''                ''                ''                '                ''                ''                'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        ''                ''                ''                '                ''                ''                '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "C2" & Chr(34) & vbCrLf
        ''                ''                ''                '                ''                ''                '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(10)
        ''                ''                ''                '                ''                ''                '    If descrizione.Contains("0") Then
        ''                ''                ''                '                ''                ''                '        descrizione = descrizione.Replace("0", "%")
        ''                ''                ''                '                ''                ''                '    End If
        ''                ''                ''                '                ''                ''                'ElseIf DirectCast(CTRL, TextBox).ID.ToString().Contains("D2") Then
        ''                ''                ''                '                ''                ''                '    stringa = stringa & ".Attributes(" & Chr(34) & "COD_TIPO_ELEMENTO" & Chr(34) & ")= " & Chr(34) & "D1.1" & Chr(34) & vbCrLf
        ''                ''                ''                '                ''                ''                '    descrizione = DirectCast(CTRL, TextBox).ID.Substring(9)
        ''                ''                ''                '                ''                ''                '    If descrizione.Contains("0") Then
        ''                ''                ''                '                ''                ''                '        descrizione = descrizione.Replace("0", "%")
        ''                'End If
        ''            End If
        ''        stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "TABELLA_DB" & Chr(34) & ")= " & Chr(34) & "ANOMALIE" & Chr(34) & vbCrLf

        ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        ''        StrSql = "SELECT ID FROM SISCOM_MI.TIPOLOGIA_ANOMALIE where DESCRIZIONE like '%" & descrizione & "%'"
        ''        par.cmd.CommandText = StrSql
        ''        myReader = par.cmd.ExecuteReader

        ''        If myReader.Read Then
        ''            idtipo = myReader(0)
        ''            stringa = stringa & "Me." & DirectCast(CTRL, TextBox).ID.ToString() & ".Attributes(" & Chr(34) & "ID_TIPO" & Chr(34) & ")= " & Chr(34) & idtipo & Chr(34) & vbCrLf
        ''        End If
        ''        myReader.Close()

        ''        stringa = stringa & vbCrLf
        ''        TxtNote_1.Text = TxtNote_1.Text & stringa
        ''        TxtNote_1.Text = TxtNote_1.Text & vbCrLf

        ''                End If
        ''    Next


        ''Catch ex As Exception
        ''    Dim s As New Exception(DirectCast(CTRL, CheckBox).ID)
        ''    par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        ''    par.SettaCommand(par)
        ''End Try
        ''par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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

        Me.T_ANO_Q1_inadeguatezza0contatore0elettrico.Attributes("COD_TIPO_ELEMENTO") = "Q1"
        Me.T_ANO_Q1_inadeguatezza0contatore0elettrico.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q1_inadeguatezza0contatore0elettrico.Attributes("ID_TIPO") = "231"

        Me.T_ANO_Q2_non0protetto.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.T_ANO_Q2_non0protetto.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q2_non0protetto.Attributes("ID_TIPO") = "232"

        Me.T_ANO_Q2_inadeguatezza0posizione.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.T_ANO_Q2_inadeguatezza0posizione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q2_inadeguatezza0posizione.Attributes("ID_TIPO") = "231"

        Me.T_ANO_Q2_mancanza0schemi.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.T_ANO_Q2_mancanza0schemi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q2_mancanza0schemi.Attributes("ID_TIPO") = "234"

        Me.T_ANO_Q3_cavi0vista.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.T_ANO_Q3_cavi0vista.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q3_cavi0vista.Attributes("ID_TIPO") = "235"

        Me.T_ANO_Q3_allacciamenti0abusivi.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.T_ANO_Q3_allacciamenti0abusivi.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q3_allacciamenti0abusivi.Attributes("ID_TIPO") = "236"

        Me.T_ANO_Q4_mancata0posizionamento0dispoersori.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.T_ANO_Q4_mancata0posizionamento0dispoersori.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q4_mancata0posizionamento0dispoersori.Attributes("ID_TIPO") = "237"

        Me.T_ANO_Q4_manomissione0faraday.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.T_ANO_Q4_manomissione0faraday.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q4_manomissione0faraday.Attributes("ID_TIPO") = "238"

        Me.T_ANO_Q5_non0luci0scale.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.T_ANO_Q5_non0luci0scale.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q5_non0luci0scale.Attributes("ID_TIPO") = "239"

        Me.T_ANO_Q5_non0luci0esterne.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.T_ANO_Q5_non0luci0esterne.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q5_non0luci0esterne.Attributes("ID_TIPO") = "240"

        Me.T_ANO_Q5_non0crepuscolare.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.T_ANO_Q5_non0crepuscolare.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q5_non0crepuscolare.Attributes("ID_TIPO") = "241"

        Me.T_ANO_Q5_rottura0illuminazione.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.T_ANO_Q5_rottura0illuminazione.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q5_rottura0illuminazione.Attributes("ID_TIPO") = "242"

        Me.T_ANO_Q5_non0interruttori.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.T_ANO_Q5_non0interruttori.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q5_non0interruttori.Attributes("ID_TIPO") = "243"

        Me.T_ANO_Q5_mancanza0interruttori.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.T_ANO_Q5_mancanza0interruttori.Attributes("TABELLA_DB") = "ANOMALIE"
        Me.T_ANO_Q5_mancanza0interruttori.Attributes("ID_TIPO") = "244"

    End Sub
    Private Sub SettaChkAnalisi()
        Me.C_ANA_Q1_forte.Attributes("COD_TIPO_ELEMENTO") = "Q1"
        Me.C_ANA_Q1_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_Q1_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q1_lieve.Attributes("COD_TIPO_ELEMENTO") = "Q1"
        Me.C_ANA_Q1_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_Q1_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q1_nuovo.Attributes("COD_TIPO_ELEMENTO") = "Q1"
        Me.C_ANA_Q1_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_Q1_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q1_tot.Attributes("COD_TIPO_ELEMENTO") = "Q1"
        Me.C_ANA_Q1_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_Q1_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q2_forte.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_ANA_Q2_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_Q2_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q2_lieve.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_ANA_Q2_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_Q2_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q2_nuovo.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_ANA_Q2_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_Q2_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q2_tot.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_ANA_Q2_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_Q2_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q3_forte.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_ANA_Q3_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_Q3_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q3_lieve.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_ANA_Q3_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_Q3_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q3_nuovo.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_ANA_Q3_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_Q3_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q3_tot.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_ANA_Q3_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_Q3_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q4_forte.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_ANA_Q4_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_Q4_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q4_lieve.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_ANA_Q4_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_Q4_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q4_nuovo.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_ANA_Q4_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_Q4_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q4_tot.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_ANA_Q4_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_Q4_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q5_forte.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_ANA_Q5_forte.Attributes("ID_TIPO") = "3"
        Me.C_ANA_Q5_forte.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q5_lieve.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_ANA_Q5_lieve.Attributes("ID_TIPO") = "2"
        Me.C_ANA_Q5_lieve.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q5_nuovo.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_ANA_Q5_nuovo.Attributes("ID_TIPO") = "1"
        Me.C_ANA_Q5_nuovo.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

        Me.C_ANA_Q5_tot.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_ANA_Q5_tot.Attributes("ID_TIPO") = "4"
        Me.C_ANA_Q5_tot.Attributes("TABELLA_DB") = "ANALISI_PRESTAZIONALE"

    End Sub
    Private Sub SettaChkInterventi()
        Me.C_INT_Q1_spostamento0contatore.Attributes("COD_TIPO_ELEMENTO") = "Q1"
        Me.C_INT_Q1_spostamento0contatore.Attributes("ID_TIPO") = "182"
        Me.C_INT_Q1_spostamento0contatore.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_Q2_aggiornamento0documentale.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_INT_Q2_aggiornamento0documentale.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_Q2_aggiornamento0documentale.Attributes("ID_TIPO") = "301"

        Me.C_INT_Q2_protezione0que.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_INT_Q2_protezione0que.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_Q2_protezione0que.Attributes("ID_TIPO") = "232"

        Me.C_INT_Q2_spostamento0qe.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_INT_Q2_spostamento0qe.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_Q2_spostamento0qe.Attributes("ID_TIPO") = "231"

        Me.C_INT_Q3_chiusura0scatolette.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_INT_Q3_chiusura0scatolette.Attributes("ID_TIPO") = "234"
        Me.C_INT_Q3_chiusura0scatolette.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_Q3_ripristino0canalizzazioni.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_INT_Q3_ripristino0canalizzazioni.Attributes("ID_TIPO") = "236"
        Me.C_INT_Q3_ripristino0canalizzazioni.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_Q3_ripristino0regolarità.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_INT_Q3_ripristino0regolarità.Attributes("ID_TIPO") = "235"
        Me.C_INT_Q3_ripristino0regolarità.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_Q5_revisione0impianto.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_INT_Q5_revisione0impianto.Attributes("ID_TIPO") = "237"
        Me.C_INT_Q5_revisione0impianto.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_Q5_sostituz0frutti.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_INT_Q5_sostituz0frutti.Attributes("ID_TIPO") = "240"
        Me.C_INT_Q5_sostituz0frutti.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_Q5_sostituz0illuminaz.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_INT_Q5_sostituz0illuminaz.Attributes("ID_TIPO") = "239"
        Me.C_INT_Q5_sostituz0illuminaz.Attributes("TABELLA_DB") = "INTERVENTI"

        Me.C_INT_Q5_sostituz0lampadine.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_INT_Q5_sostituz0lampadine.Attributes("TABELLA_DB") = "INTERVENTI"
        Me.C_INT_Q5_sostituz0lampadine.Attributes("ID_TIPO") = "238"


    End Sub
    Private Sub SettaChkMateriali()
        Me.C_MAT_Q1_contatore.Attributes("COD_TIPO_ELEMENTO") = "Q1"
        Me.C_MAT_Q1_contatore.Attributes("ID_TIPO") = "222"
        Me.C_MAT_Q1_contatore.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_Q2_ascensori.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_MAT_Q2_ascensori.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q2_ascensori.Attributes("ID_TIPO") = "301"

        Me.C_MAT_Q2_autoclave.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_MAT_Q2_autoclave.Attributes("ID_TIPO") = "231"
        Me.C_MAT_Q2_autoclave.Attributes("TABELLA_DB") = "MATERIALI"

        Me.C_MAT_Q2_CT.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_MAT_Q2_CT.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q2_CT.Attributes("ID_TIPO") = "302"

        Me.C_MAT_Q2_generale.Attributes("COD_TIPO_ELEMENTO") = "Q2"
        Me.C_MAT_Q2_generale.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q2_generale.Attributes("ID_TIPO") = "303"

        Me.C_MAT_Q3_canalizzazioni0tubazioni.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_MAT_Q3_canalizzazioni0tubazioni.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q3_canalizzazioni0tubazioni.Attributes("ID_TIPO") = "304"

        Me.C_MAT_Q3_cavi0elettrici.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_MAT_Q3_cavi0elettrici.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q3_cavi0elettrici.Attributes("ID_TIPO") = "305"

        Me.C_MAT_Q3_scatolette0derivazione.Attributes("COD_TIPO_ELEMENTO") = "Q3"
        Me.C_MAT_Q3_scatolette0derivazione.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q3_scatolette0derivazione.Attributes("ID_TIPO") = "306"


        Me.C_MAT_Q4_dispersori.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_MAT_Q4_dispersori.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q4_dispersori.Attributes("ID_TIPO") = "308"

        Me.C_MAT_Q4_rete0faraday.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_MAT_Q4_rete0faraday.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q4_rete0faraday.Attributes("ID_TIPO") = "311"

        '-----DOVREBBERO ESSERE Q5------
        Me.C_MAT_Q4_illuminazione0prese.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_MAT_Q4_illuminazione0prese.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q4_illuminazione0prese.Attributes("ID_TIPO") = "309"

        Me.C_MAT_Q4_interrutori.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_MAT_Q4_interrutori.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q4_interrutori.Attributes("ID_TIPO") = "310"

        Me.C_MAT_Q4_crepuscolari.Attributes("COD_TIPO_ELEMENTO") = "Q4"
        Me.C_MAT_Q4_crepuscolari.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q4_crepuscolari.Attributes("ID_TIPO") = "307"
        '------FINE DELLA SVISTA-------
        Me.C_MAT_Q5_luci.Attributes("COD_TIPO_ELEMENTO") = "Q5"
        Me.C_MAT_Q5_luci.Attributes("TABELLA_DB") = "MATERIALI"
        Me.C_MAT_Q5_luci.Attributes("ID_TIPO") = "404"


    End Sub
    Private Sub settamq()
        Me.Text_mq_Q1.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q1.Attributes("COD_TIPO_ELEMENTO") = "Q1"

        Me.Text_mq_Q21.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q21.Attributes("COD_TIPO_ELEMENTO") = "Q2"

        Me.Text_mq_Q22.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q22.Attributes("COD_TIPO_ELEMENTO") = "Q22"

        Me.Text_mq_Q23.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q23.Attributes("COD_TIPO_ELEMENTO") = "Q23"

        Me.Text_mq_Q24.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q24.Attributes("COD_TIPO_ELEMENTO") = "Q24"

        Me.Text_mq_Q3.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q3.Attributes("COD_TIPO_ELEMENTO") = "Q3"

        Me.Text_mq_Q4.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q4.Attributes("COD_TIPO_ELEMENTO") = "Q4"

        Me.Text_mq_Q51.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q51.Attributes("COD_TIPO_ELEMENTO") = "Q5"

        Me.Text_mq_Q52.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q52.Attributes("COD_TIPO_ELEMENTO") = "Q52"

        Me.Text_mq_Q53.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q53.Attributes("COD_TIPO_ELEMENTO") = "Q53"

        Me.Text_mq_Q54.Attributes("TABELLA_DB") = "DIMENSIONI_ELEMENTI"
        Me.Text_mq_Q54.Attributes("COD_TIPO_ELEMENTO") = "Q54"

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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_edificio = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Anomalie_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_COMPLESSO = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_complesso = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Materiali where id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Materiali_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Interventi where id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.Interventi_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Analisi_prestazionale where id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANALISI_PRESTAZIONALE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.Anomalie where id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.ANOMALIE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI where id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.DIMENSIONI_ELEMENTI_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and cod_tipo_elemento like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.NOTE where id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.NOTE_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
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
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO where ID_UNITA_COMUNE = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
                    Else
                        StringControl = "Select * From SISCOM_MI.STATO_DEGRADO_ST where DATA_CENSIMENTO = " & PassData & " AND id_unita_comune = " & vId & " and COD_TIPO_SCHEDA like '%Q%' "
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
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_EDIFICIO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'Q','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""


                    Case "COMP"


                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_COMPLESSO,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'Q','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
                        par.cmd.CommandText = STRINSERT
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                    Case "UC"

                        STRINSERT = "INSERT INTO SISCOM_MI.NOTE (ID_UNITA_COMUNE,COD_TIPO_SCHEDA, NOTE_1,NOTE_2) values(" & vId & ",'Q','" & par.PulisciStrSql(Me.TxtNote_1.Text) & "','" & par.PulisciStrSql(Me.TxtNote_2.Text) & "')"
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

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Select Case vTipo
                Case "EDIF"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_EDIFICIO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_EDIFICIO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "COMP"

                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_COMPLESSO = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_COMPLESSO = " & vId & " AND COD_TIPO_SCHEDA LIKE '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'fine aggiunta

                Case "UC"
                    If EsistonoMateriali = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.MATERIALI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnalisi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANALISI_PRESTAZIONALE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoAnomalie Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.ANOMALIE WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()

                    End If
                    If EsistonoInterventi = True Then
                        par.cmd.CommandText = "DELETE FROM  SISCOM_MI.INTERVENTI WHERE ID_UNITA_COMUNE = " & vId & " and cod_Tipo_elemento like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If EsistonoNote = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.NOTE WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA like '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    If EsistonoDimensioni = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIMENSIONI_ELEMENTI WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_ELEMENTO LIKE '%Q%'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    'Aggiunta STATO DEGRADO 07/08/2009 PEPPE
                    If EsisteDegrado = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.STATO_DEGRADO WHERE ID_UNITA_COMUNE = " & vId & " AND COD_TIPO_SCHEDA LIKE '%Q%'"
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
            Response.Write("<SCRIPT>alert('Si sono verificati degli errori!');</SCRIPT>")
            Label1.Visible = True
            Label1.Text = ex.Message
        End Try

    End Sub
    Private Sub SettaChkDegrado()
        Me.ChkSt1.Attributes("COD_TIPO_SCHEDA") = "Q"
        Me.ChkSt1.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt1.Attributes("STATO") = "1"

        Me.ChkSt21.Attributes("COD_TIPO_SCHEDA") = "Q"
        Me.ChkSt21.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt21.Attributes("STATO") = "2.1"

        Me.ChkSt22.Attributes("COD_TIPO_SCHEDA") = "Q"
        Me.ChkSt22.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt22.Attributes("STATO") = "2.2"

        Me.ChkSt31.Attributes("COD_TIPO_SCHEDA") = "Q"
        Me.ChkSt31.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt31.Attributes("STATO") = "3.1"

        Me.ChkSt32.Attributes("COD_TIPO_SCHEDA") = "Q"
        Me.ChkSt32.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt32.Attributes("STATO") = "3.2"

        Me.ChkSt33.Attributes("COD_TIPO_SCHEDA") = "Q"
        Me.ChkSt33.Attributes("TABELLA_DB") = "STATO_DEGRADO"
        Me.ChkSt33.Attributes("STATO") = "3.3"


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



End Class
