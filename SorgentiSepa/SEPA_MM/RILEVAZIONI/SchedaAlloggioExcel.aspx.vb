Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml

Partial Class RILEVAZIONI_SchedaAlloggioExcel
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_PAR") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                BindGrid()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUtenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            connData.apri()

            Dim Str As String = ""
            Str = "select ID,'' AS FILE_EXCEL,(CASE WHEN IN_USO=0 THEN 'NO' ELSE 'SI' END) AS IN_USO, TO_CHAR(TO_DATE(substr(DATA_CARICAMENTO,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_CARICAMENTO from siscom_mi.rilievo_gestione_schede order by id desc"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                For Each rowIdContr As Data.DataRow In dt.Rows
                    rowIdContr.Item("FILE_EXCEL") = "<a href='javascript:AfterSubmit()' onclick=" & Chr(34) & "document.getElementById('CPContenuto_LBLID').value='" & rowIdContr.Item("ID") & "';ScaricaFileXLS();" & Chr(34) & "><img src='../Standard/Immagini/Form_24.png' alt='Scheda' border='0'/></a>"
                Next
            End If
            DataGridParam0.DataSource = dt
            DataGridParam0.DataBind()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUtenti - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGridParam0_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridParam0.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
    Public Function GetString(bytes As Byte()) As String
        Dim chars As Char() = New Char(bytes.Length \ 2 - 1) {}
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length)
        Return New String(chars)
    End Function

    Protected Sub btnScaricaXls_Click(sender As Object, e As System.EventArgs) Handles btnScaricaXls.Click
        Try
            connData.apri()
            Dim Xls As Byte()
            Dim NomeFileXls As String = ""
            Dim bw As BinaryWriter
            par.cmd.CommandText = "select * from siscom_mi.rilievo_gestione_schede where id=" & LBLID.Value & " order by id desc"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                Xls = par.IfNull(MyReader("file_Excel"), "")
                NomeFileXls = "Scheda_manutentiva_" & Format(Now, "yyyyMMddhhmmss") & ".zip"
                Dim fileName As String = Server.MapPath("..\FileTemp\") & NomeFileXls
                Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
                bw = New BinaryWriter(fs)
                bw.Write(Xls)
                bw.Flush()
                bw.Close()

                If File.Exists(Server.MapPath("~\FileTemp\") & NomeFileXls) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & NomeFileXls & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                    Exit Sub
                Else
                    par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
                End If
            End If
            MyReader.Close()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: SchedaAlloggioExcel - btnScaricaXls - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try

            If Me.FileUpload1.HasFile Then
                Dim fileOK As Boolean = False
                Dim fileExtension As String = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower()
                Dim allowedExtensions As String() = {".xlsx", ".xls"}
                For i As Integer = 0 To allowedExtensions.Length - 1
                    If fileExtension = allowedExtensions(i) Then
                        fileOK = True
                        Exit For
                    End If
                Next
                If fileOK = False Then
                    par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare un file .xlsx o .xls", Me.Page)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
                Else
                    CaricaModelloXLSX()
                    par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
                End If
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: SchedaAlloggioExcel - btnAllega_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaModelloXLSX()
        Dim estensioneFile As String = ""
        If Right(FileUpload1.FileName, 5) = ".xlsx" Then
            estensioneFile = "xlsx"
        ElseIf Right(FileUpload1.FileName, 4) = ".xls" Then
            estensioneFile = "xls"
        End If

        Dim nFile As String = Server.MapPath("..\FileTemp\") & "Scheda_manutentiva_" & Format(Now, "yyyyMMddHHmm") & "." & estensioneFile
        FileUpload1.SaveAs(nFile)

        connData.apri(True)

        par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_GESTIONE_SCHEDE SET IN_USO=0 WHERE IN_USO=1"
        par.cmd.ExecuteNonQuery()

       

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_GESTIONE_SCHEDE (ID, FILE_EXCEL, IN_USO, DATA_CARICAMENTO,ESTENSIONE_FILE) VALUES (SISCOM_MI.SEQ_RILIEVO_GESTIONE_SCHEDE.NEXTVAL,:XLSX, 1,'" & Format(Now, "yyyyMMddHHmm") & "'," & par.insDbValue(estensioneFile, True) & ")"

        Dim zipfic As String
        Dim NomeFilezip As String = "Scheda_manutentiva_" & Format(Now, "yyyyMMddHHmm") & ".zip"

        zipfic = Server.MapPath("..\FileTemp\" & NomeFilezip)

        Dim kkK As Integer = 0

        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)

        Dim strFile As String


        strFile = nFile
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

        Dim sFile As String = Path.GetFileName(strFile)
        Dim theEntry As ZipEntry = New ZipEntry(sFile)
        Dim fi As New FileInfo(strFile)
        theEntry.DateTime = fi.LastWriteTime
        theEntry.Size = strmFile.Length
        strmFile.Close()
        objCrc32.Reset()
        objCrc32.Update(abyBuffer)
        theEntry.Crc = objCrc32.Value
        strmZipOutputStream.PutNextEntry(theEntry)
        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        'File.Delete(strFile)

        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()


        Dim objStream As Stream = File.Open(zipfic, FileMode.Open)
        Dim buffer(objStream.Length) As Byte
        objStream.Read(buffer, 0, objStream.Length)
        objStream.Close()

        Dim parmData As New Oracle.DataAccess.Client.OracleParameter
        With parmData
            .Direction = Data.ParameterDirection.Input
            .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
            .ParameterName = "XLSX"
            .Value = buffer
        End With

        par.cmd.Parameters.Add(parmData)
        par.cmd.ExecuteNonQuery()
        System.IO.File.Delete(nFile)
        par.cmd.Parameters.Remove(parmData)
        buffer = Nothing
        objStream = Nothing

        connData.chiudi(True)

        BindGrid()

        File.Delete(nFile)

    End Sub

    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE EXCEL ##########
            If FileUpload1.HasFile = True Then
                UploadOnServer = Server.MapPath("..\FileTemp\") & FileUpload1.FileName
                FileUpload1.SaveAs(UploadOnServer)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:UploadOnServer " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return UploadOnServer
    End Function

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try
            connData.apri(True)
            Dim eliminata As Boolean = False
            par.cmd.CommandText = "select * from SISCOM_MI.RILIEVO_GESTIONE_SCHEDE order by data_Caricamento desc,id desc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                For Each rowDati As Data.DataRow In dt.Rows
                    If rowDati.Item("ID") = LBLID.Value Then
                        If rowDati.Item("IN_USO") = "1" And dt.Rows.Count = 1 Then
                            par.modalDialogMessage("Attenzione", "Impossibile eliminare l\'elemento selezionato!", Me.Page)
                            Exit For
                        ElseIf rowDati.Item("IN_USO") = "1" And dt.Rows.Count > 1 Then
                            eliminata = True
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_GESTIONE_SCHEDE WHERE ID=" & LBLID.Value
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_GESTIONE_SCHEDE WHERE ID=" & LBLID.Value
                            par.cmd.ExecuteNonQuery()
                        End If
                    Else
                        If eliminata = True Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_GESTIONE_SCHEDE set in_uso=1 where id=" & rowDati.Item("ID")
                            par.cmd.ExecuteNonQuery()
                            eliminata = False
                        End If
                    End If
                Next
            End If
            connData.chiudi(True)
            Me.TextBox1.Value = "0"
            Me.LBLID.Value = ""
            BindGrid()
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - btnEliminaElemento_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
