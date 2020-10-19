Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Contratti_Bollettazione_GestIncassi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim conndata As CM.datiConnessione = Nothing
    Public percentuale As Long = 0

    Public Property dtCompleta() As Data.DataTable
        Get
            If Not (ViewState("dtCompleta") Is Nothing) Then
                Return ViewState("dtCompleta")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtCompleta") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.conndata = New CM.datiConnessione(par, False, False)
        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Str)

    End Sub
    Protected Sub btnElabora_Click(sender As Object, e As System.EventArgs) Handles btnElabora.Click
        Try
            Dim FileName As String = UploadOnServer()
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And (FileName.Contains(".xls") Or FileName.Contains(".xlsx")) Then

                    ReadFile(FileName)

                End If
            End If
        Catch ex As Exception

        End Try



    End Sub
    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE EXCEL ##########
            If FileUpload.HasFile = True Then
                nomeFile.Value = FileUpload.FileName
                UploadOnServer = Server.MapPath("..\..\FileTemp\") & FileUpload.FileName
                FileUpload.SaveAs(UploadOnServer)

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:UploadOnServer " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

        Return UploadOnServer
    End Function
    Private Sub ReadFile(ByVal file As String)

        Dim strConn As String = "Provider=Microsoft.ACE.OLEDB.12.0;" _
                  & "data source=" & file & ";" _
                  & "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
        Dim objConn As New OleDbConnection(strConn)
        Dim strSql As String = "Select * from[pagati$] "
        Dim dt As New Data.DataTable
        Dim ContaRighe As Integer = 0
        Try
            objConn.Open()
            Dim da As New OleDb.OleDbDataAdapter(strSql, objConn)
            da.Fill(dt)
            objConn.Dispose()

            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows

                    If RigoVuoto(row, dt.Columns.Count) = False And Not (row.Item(0).ToString.ToUpper.Contains("COGNOME")) And ContaRighe > 0 Then
                        If RigoVuoto(dt.Rows(ContaRighe - 1), dt.Columns.Count) = True Then
                            CaricaDt(dt, ContaRighe)

                            Exit For
                        End If
                    End If
                    ContaRighe += 1
                Next
            End If

            If dtCompleta.Rows.Count > 0 Then
                dgvImport.DataSource = dtCompleta
                dgvImport.DataBind()
                lblImport.Visible = True
                btnElabora.Visible = True
                btnAnnulla.Visible = True
                btnConferma.Visible = True
            Else
                dgvImport.DataSource = Nothing
                lblImport.Visible = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:ReadFile " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            'objConn.Dispose()

        End Try
    End Sub

    Private Function RigoVuoto(ByVal row As Data.DataRow, ByVal dtcol As Integer) As Boolean

        RigoVuoto = False
        Try
            For n As Integer = 0 To dtcol - 1
                If IsDBNull(row.Item(n)) Then
                    RigoVuoto = True
                Else
                    RigoVuoto = False
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
        Return RigoVuoto

    End Function
    Private Sub CaricaDt(ByVal dtSporca As Data.DataTable, ByVal StartRow As Integer)
        CreaDT()

        Dim r As Integer
        Dim numRighe As Integer = dtSporca.Rows.Count
        Dim Nuova As Data.DataRow
        For r = StartRow To numRighe - 1
            If Not IsDBNull(dtSporca.Rows(r).Item(10)) Then
                Nuova = dtCompleta.NewRow
                Nuova.Item("COGNOME_NOME") = dtSporca.Rows(r).Item(0)
                Nuova.Item("INDIRIZZO") = dtSporca.Rows(r).Item(1)
                Nuova.Item("RESIDENZA") = dtSporca.Rows(r).Item(2)
                Nuova.Item("SCADENZA") = dtSporca.Rows(r).Item(3)
                Nuova.Item("DA_PAGARE") = dtSporca.Rows(r).Item(4)
                Nuova.Item("PAGATO") = dtSporca.Rows(r).Item(5)
                Nuova.Item("DATA_PAGAMENTO") = dtSporca.Rows(r).Item(7).ToString.Substring(0, 10)
                Nuova.Item("RITARDO") = dtSporca.Rows(r).Item(8)
                Nuova.Item("NOTE") = dtSporca.Rows(r).Item(9)
                Nuova.Item("ID_BOLLETTA") = dtSporca.Rows(r).Item(10)
                dtCompleta.Rows.Add(Nuova)
            End If


        Next

    End Sub
    Protected Sub CreaDT()
        dtCompleta = New Data.DataTable
        '######### SVUOTA E CREA COLONNE DATATABLE #########
        dtCompleta.Clear()
        dtCompleta.Columns.Clear()
        dtCompleta.Rows.Clear()
        dtCompleta.Columns.Add("COGNOME_NOME")
        dtCompleta.Columns.Add("INDIRIZZO")
        dtCompleta.Columns.Add("RESIDENZA")
        dtCompleta.Columns.Add("SCADENZA")
        dtCompleta.Columns.Add("DA_PAGARE")
        dtCompleta.Columns.Add("PAGATO")
        dtCompleta.Columns.Add("DATA_PAGAMENTO")
        dtCompleta.Columns.Add("RITARDO")
        dtCompleta.Columns.Add("NOTE")
        dtCompleta.Columns.Add("ID_BOLLETTA")

    End Sub


    Protected Sub btnConferma_Click(sender As Object, e As System.EventArgs) Handles btnConferma.Click
        If conferma.Value = 1 Then
            If dtCompleta.Rows.Count > 0 Then
                ImportaDati()
            End If
            conferma.Value = 0
        End If

    End Sub
    Private Sub ImportaDati()
        Try
            Dim Contatore As Long = 0
            Dim NUMERORIGHE As Integer = dtCompleta.Rows.Count
            par.cmd = conndata.apri(True)

            For Each row As Data.DataRow In dtCompleta.Rows
                Contatore = Contatore + 1
                percentuale = (Contatore * 100) / NUMERORIGHE
                Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                Response.Flush()

                par.cmd.CommandText = "UPDATE BOL_BOLLETTE SET " _
                                    & " DATA_PAGAMENTO='" & par.FormatoDataDB(row.Item("data_pagamento")) & "'" _
                                    & ",DATA_VALUTA='" & par.FormatoDataDB(row.Item("data_pagamento")) & "'" _
                                    & ",DATA_VALUTA_CREDITORE='" & par.FormatoDataDB(row.Item("data_pagamento")) & "'" _
                                    & ",DATA_INS_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & ",RIF_FILE_RENDICONTO='" & nomeFile.Value & "'" _
                                    & ",FL_PAG_MAV=1 where id = " & row.Item("ID_BOLLETTA")
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "UPDATE BOL_BOLLETTE_VOCI SET " _
                                    & "IMP_PAGATO=IMPORTO " _
                                    & "WHERE ID_BOLLETTA=" & row.Item("ID_BOLLETTA")
                par.cmd.ExecuteNonQuery()



            Next
            conndata.chiudi(True)
            Response.Write("<script>alert('Operazione eseguita correttamente');self.close();</script>")

        Catch ex As Exception
            conndata.chiudi(False)

            Session.Add("ERRORE", "Provenienza:ImportaDatiBollette " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub
End Class
