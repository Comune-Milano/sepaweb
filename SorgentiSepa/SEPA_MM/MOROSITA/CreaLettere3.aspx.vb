Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing


Partial Class Morosita_CreaLettere3
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim pp As New MavOnline.MAVOnlineBeanService
    Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
    Dim Esito As New MavOnline.rispostaMAVOnlineWS
    Dim binaryData() As Byte
    Dim outFile As System.IO.FileStream
    Dim outputFileName As String = ""
    Dim myExcelFile As New CM.ExcelFile
    Dim sNomeFile As String


    Private Function RicavaFile(ByVal sFile As String) As String
        Dim N As Integer

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        Dim MIOCOLORE As String
        Dim MiaSHTML As String
        Dim ElencoFile() As String
        Dim i, j As Integer

        Dim IndiceBollette As String = "-1"
        Dim idMorisita As String = "-1"
        Dim idContratto As String = "-1"

        Dim Str As String = ""

        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader
        Dim FlagConnessione As Boolean = False
        Dim Trovato As Integer = 0

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                IndiceBollette = Request.QueryString("ID_BOLLETTE")

                idMorisita = Request.QueryString("ID_MOROSITA")
                idContratto = Request.QueryString("ID_CONTRATTO")


                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                ''MOROSITA'' MA'"
                par.cmd.Parameters.Clear()

                If IndiceBollette <> "-1" Then
                    par.cmd.CommandText = "select ID,NOTE from SISCOM_MI.BOL_BOLLETTE " _
                                       & " where ID in (" & IndiceBollette & ")" _
                                       & "  and NOTE='MOROSITA'' MA'"

                Else
                    par.cmd.CommandText = "select ID,NOTE from SISCOM_MI.BOL_BOLLETTE " _
                                       & " where ID_MOROSITA=" & idMorisita _
                                       & "   and ID_CONTRATTO=" & idContratto _
                                       & "   and ID_BOLLETTA_RIC is null " _
                                       & "   and NOTE='MOROSITA'' MA'" _
                                       & " order by DATA_EMISSIONE desc"


                End If

                i = 0

                myReaderA = par.cmd.ExecuteReader()
                While myReaderA.Read



                    'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("ELABORAZIONI") & "\MAV\", FileIO.SearchOption.SearchTopLevelOnly, Format(myReaderA(0), "0000000000") & ".pdf")
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\"), FileIO.SearchOption.SearchTopLevelOnly, Format(myReaderA("ID"), "0000000000") & ".pdf")

                        ReDim Preserve ElencoFile(i)
                        ElencoFile(i) = foundFile
                        i = i + 1
                    Next

                    Dim kk As Long
                    Dim jj As Long
                    Dim scambia As String

                    For kk = 0 To i - 2
                        For jj = kk + 1 To i - 1

                            'If CDbl(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14)) < CDbl(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14)) Then
                            If CLng(Mid(RicavaFile(ElencoFile(kk)), 1, Strings.Len(RicavaFile(ElencoFile(kk))) - 4)) < CLng(Mid(RicavaFile(ElencoFile(jj)), 1, Strings.Len(RicavaFile(ElencoFile(jj))) - 4)) Then

                                scambia = ElencoFile(kk)
                                ElencoFile(kk) = ElencoFile(jj)
                                ElencoFile(jj) = scambia
                            End If
                        Next
                    Next
                End While
                myReaderA.Close()
                '****************************



                If i > 0 Then
                    Trovato = Trovato + 1

                    MIOCOLORE = "#CCFFFF"
                    MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='200px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font face='Arial' size='2'>Descrizione del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                    For j = 0 To i - 1
                        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='200px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & "MOROSITA' MA" & "</font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

                        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                        If MIOCOLORE = "#CCFFFF" Then
                            MIOCOLORE = "#FFFFCC"
                        Else
                            MIOCOLORE = "#CCFFFF"
                        End If
                        If j = 10 Then Exit For
                    Next j

                    MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                    Response.Write(MiaSHTML)
                End If


                ''MOROSITA'' MG'"
                par.cmd.Parameters.Clear()

                If IndiceBollette <> "-1" Then
                    par.cmd.CommandText = "select ID,NOTE from SISCOM_MI.BOL_BOLLETTE " _
                                       & " where ID in (" & IndiceBollette & ")" _
                                       & "  and NOTE='MOROSITA'' MG'"

                Else
                    par.cmd.CommandText = "select ID,NOTE from SISCOM_MI.BOL_BOLLETTE " _
                                       & " where ID_MOROSITA=" & idMorisita _
                                       & "   and ID_CONTRATTO=" & idContratto _
                                       & "   and ID_BOLLETTA_RIC is null " _
                                       & "   and NOTE='MOROSITA'' MG'" _
                                       & " order by DATA_EMISSIONE desc"


                End If

                i = 0
                myReaderA = par.cmd.ExecuteReader()
                While myReaderA.Read


                    'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("ELABORAZIONI") & "\MAV\", FileIO.SearchOption.SearchTopLevelOnly, Format(myReaderA(0), "0000000000") & ".pdf")
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\"), FileIO.SearchOption.SearchTopLevelOnly, Format(myReaderA("ID"), "0000000000") & ".pdf")

                        ReDim Preserve ElencoFile(i)
                        ElencoFile(i) = foundFile
                        i = i + 1
                    Next

                    Dim kk As Long
                    Dim jj As Long
                    Dim scambia As String

                    For kk = 0 To i - 2
                        For jj = kk + 1 To i - 1
                            'If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14)) Then
                            If CLng(Mid(RicavaFile(ElencoFile(kk)), 1, Strings.Len(RicavaFile(ElencoFile(kk))) - 4)) < CLng(Mid(RicavaFile(ElencoFile(jj)), 1, Strings.Len(RicavaFile(ElencoFile(jj))) - 4)) Then

                                scambia = ElencoFile(kk)
                                ElencoFile(kk) = ElencoFile(jj)
                                ElencoFile(jj) = scambia
                            End If
                        Next
                    Next

                End While
                myReaderA.Close()
                '****************************



                If i > 0 Then
                    Trovato = Trovato + 1

                    MIOCOLORE = "#CCFFFF"
                    MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='200px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font face='Arial' size='2'>Descrizione del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                    For j = 0 To i - 1
                        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='200px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & "MOROSITA' MG" & "</font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

                        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                        If MIOCOLORE = "#CCFFFF" Then
                            MIOCOLORE = "#FFFFCC"
                        Else
                            MIOCOLORE = "#CCFFFF"
                        End If
                        If j = 10 Then Exit For
                    Next j
                    MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                    Response.Write(MiaSHTML)
                End If
               
                If Trovato = 0 Then
                    MIOCOLORE = "#CCFFFF"
                    MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='200px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font face='Arial' size='2'>Descrizione del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                    MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                    Response.Write(MiaSHTML)
                End If


                '************CHIUSURA CONNESSIONE**********
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If



            Catch ex As Exception

                '************CHIUSURA CONNESSIONE**********
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                Response.Write(ex.Message)

            End Try
        End If
    End Sub



End Class
