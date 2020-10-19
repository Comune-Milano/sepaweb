
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CENSIMENTO_InviaAllStatoManut
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()

            txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If
    End Sub

    Protected Sub ImgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try


            If Not String.IsNullOrEmpty(Me.txtData.Text) AndAlso Not String.IsNullOrEmpty(Me.txtDescrizione.Text) Then

                If FileUpload1.HasFile = True Then

                    par.OracleConn.Open()
                    par.cmd = par.OracleConn.CreateCommand
                    identificativo.Value = -1

                    par.cmd.CommandText = "SELECT ID_ALLEGATO FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE ID_UNITA = " & Request.QueryString("IDUNITA")

                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        identificativo.Value = par.IfNull(lettore("id_allegato"), "-1")
                    End If
                    lettore.Close()

                    If identificativo.Value < 0 Then
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ID_ALLEGATO_STATO_MANUT.NEXTVAL FROM DUAL"
                        lettore = par.cmd.ExecuteReader
                        If lettore.Read Then
                            identificativo.Value = lettore(0)
                        End If
                        lettore.Close()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_STATO_MANUTENTIVO SET ID_ALLEGATO = " & identificativo.Value & " WHERE ID_UNITA = " & Request.QueryString("IDUNITA")
                        par.cmd.ExecuteNonQuery()
                    End If


                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                    Dim percorso As String = Server.MapPath("..\ALLEGATI\STATO_MANUTENTIVO\")

                    Dim fileDescrizione As String = percorso & "Verbale_" & identificativo.Value & "_DESCRIZIONE.txt"
                    Dim nFile As String = percorso & FileUpload1.FileName

                    Dim zipfic As String = percorso & Request.QueryString("IDUNITA") & "_" & identificativo.Value & "_" & par.AggiustaData(txtData.Text) & ".zip"

                    Dim sr As StreamWriter = New StreamWriter(fileDescrizione, False, System.Text.Encoding.Default)
                    sr.WriteLine("Data del documento:" & txtData.Text & vbCrLf & txtDescrizione.Text)
                    sr.Close()


                    FileUpload1.SaveAs(nFile)
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
                    File.Delete(strFile)



                    strFile = percorso & "Verbale_" & identificativo.Value & "_DESCRIZIONE.txt"
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer12(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer12, 0, abyBuffer12.Length)
                    Dim sFile12 As String = Path.GetFileName(strFile)
                    theEntry = New ZipEntry(sFile12)
                    fi = New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer12)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer12, 0, abyBuffer12.Length)
                    File.Delete(strFile)


                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                    Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")



                    lblErrore.Visible = False

                Else
                    lblErrore.Visible = True
                    lblErrore.Text = "Specificare il file per l'allegato!"

                End If


            Else
                lblErrore.Visible = True

                lblErrore.Text = "Specificare una data e una descrizione per l'allegato!"
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub
End Class
