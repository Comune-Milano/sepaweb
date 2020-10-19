Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class InvioAllegato
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Private Function CaricaTipologieAllegati()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.RiempiDList(Me, par.OracleConn, "cmbTipoAllegato", "select * from SISCOM_MI.TAB_MODELLI_ALLEGATI WHERE TIPO=" & tipo.Value & " order by descrizione asc", "DESCRIZIONE", "COD")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

    Private Function CaricaTitolo()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            lblErrore.Visible = False

            Select Case tipo.Value
                Case "1"
                    lblTitolo.Text = "Allega documento a rapporto cod. " & identificativo.Value
                Case "2"
                    lblTitolo.Text = "Allega documento a condominio cod. " & Format(CDbl(identificativo.Value), "00000")
                Case "3"
                    par.cmd.CommandText = "select * from siscom_mi.appalti where id=" & identificativo.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        lblTitolo.Text = "Allega documento ad appalto num. repertorio " & par.IfNull(myReader("num_repertorio"), "")
                    End If
                    myReader.Close()

                Case "4"
                    par.cmd.CommandText = "select * from siscom_mi.fornitori where id=" & identificativo.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                            lblTitolo.Text = "Allega documento a fornitore " & par.IfNull(myReader("ragione_sociale"), "")
                        Else
                            lblTitolo.Text = "Allega documento a fornitore " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")
                        End If
                    End If
                    myReader.Close()
                Case "5"
                    par.cmd.CommandText = "select * from siscom_mi.AUTOGESTIONI where id=" & identificativo.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader("ID_COMPLESSO"), -1) <> -1 Then
                            par.cmd.CommandText = "select * from siscom_mi.COMPLESSI_IMMOBILIARI where id=" & par.IfNull(myReader("ID_COMPLESSO"), -1)
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                lblTitolo.Text = "Allega documento a Autogestione su Complesso " & par.IfNull(myReader1("denominazione"), "")
                            End If
                            myReader1.Close()

                        Else
                            par.cmd.CommandText = "select * from siscom_mi.edifici where id=" & par.IfNull(myReader("ID_edificio"), -1)
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                lblTitolo.Text = "Allega documento a Autogestione su Edificio " & par.IfNull(myReader1("denominazione"), "")
                            End If
                            myReader1.Close()
                        End If
                    End If
                    myReader.Close()

            End Select


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            tipo.Value = Request.QueryString("T")
            identificativo.Value = Request.QueryString("ID")
            CaricaTitolo()
            CaricaTipologieAllegati()
        End If
        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub ImgNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNuovo.Click
        CaricaTipologieAllegati()
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try

            Dim percorsoTemp As String = Server.MapPath("FileTemp\")
            lblErrore.Visible = False

            If cmbTipoAllegato.SelectedIndex <> -1 Then
                If txtDescrizione.Text <> "" And txtData.Text <> "" Then
                    Dim nFile As String = ""
                    Dim percorso As String = ""
                    Dim fileDescrizione As String = ""
                    Dim zipfic As String = ""
                    Dim ZipFicDefinitivo As String = ""

                    If FileUpload1.HasFile = True Then

                        Select Case tipo.Value
                            Case "1"
                                percorso = Server.MapPath("ALLEGATI\CONTRATTI\")
                            Case "2"
                                percorso = Server.MapPath("ALLEGATI\CONDOMINI\")
                            Case "3"
                                percorso = Server.MapPath("ALLEGATI\APPALTI\")
                            Case "4"
                                percorso = Server.MapPath("ALLEGATI\FORNITORI\")
                            Case "5"
                                percorso = Server.MapPath("ALLEGATI\AUTOGESTIONI\")
                            Case "6"
                                percorso = Server.MapPath("ALLEGATI\MANUTENZIONI\")
                            Case "7"
                                percorso = Server.MapPath("ALLEGATI\PAGAMENTI\")

                        End Select
                        fileDescrizione = percorsoTemp & cmbTipoAllegato.SelectedItem.Value & "_" & identificativo.Value & "_DESCRIZIONE.txt"
                        nFile = percorsoTemp & FileUpload1.FileName


                        'fatto per evitare che un documento con stessa data, tipo e codice venga sovrascritto 
                        Dim zipficTemp As String = par.AggiustaData(txtData.Text) & "_" & cmbTipoAllegato.SelectedItem.Value & "_" & Format(Now, "yyyyMMddhhmmss") & "_" & identificativo.Value & ".zip"
                        zipfic = percorsoTemp & zipficTemp
                        ZipFicDefinitivo = percorso & zipficTemp
                        'zipfic = percorso & par.AggiustaData(txtData.Text) & "_" & cmbTipoAllegato.SelectedItem.Value & "_" & identificativo.Value & ".zip"
                        '******
                        Dim sr As StreamWriter = New StreamWriter(fileDescrizione, False, System.Text.Encoding.Default)
                        sr.WriteLine("Data del documento:" & txtData.Text & vbCrLf & txtDescrizione.Text)
                        sr.Close()

                        If File.Exists(nFile) Then
                            File.Delete(nFile)
                        End If


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



                        strFile = percorsoTemp & cmbTipoAllegato.SelectedItem.Value & "_" & identificativo.Value & "_DESCRIZIONE.txt"
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

                        File.Move(zipfic, ZipFicDefinitivo)
                        File.Delete(zipfic)
                        File.Delete(fileDescrizione)
                        Select Case tipo.Value
                            Case "1"
                                ScriviEvento()
                        End Select

                        Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")
                    Else
                        lblErrore.Visible = True
                        lblErrore.Text = "Specificare il file per l'allegato!"
                    End If
                Else
                    lblErrore.Visible = True
                    lblErrore.Text = "Specificare una data e una descrizione per l'allegato!"
                End If

            Else
                lblErrore.Visible = True
                lblErrore.Text = "Specificare una tipologia di allegato!"
            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "Errore durante la scrittura file - " & ex.Message
        End Try
    End Sub

    Private Sub ScriviEvento()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
              & "VALUES ((SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & identificativo.Value & "')," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
              & "'F196','INSERIMENTO ALLEGATO DESCRIZIONE:" & par.PulisciStrSql(Mid(txtDescrizione.Text, 1, 50)) & " - TIPO:" & par.PulisciStrSql(cmbTipoAllegato.SelectedItem.Text) & "')"
            par.cmd.ExecuteNonQuery()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = "Errore durante la scrittura evento! " & ex.Message
        End Try



    End Sub

    Protected Sub ImgElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgElimina.Click
        If sicuro.Value = True Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                lblErrore.Visible = False

                Dim percorso As String = ""

                Select Case tipo.Value
                    Case "1"
                        percorso = Server.MapPath("ALLEGATI\CONTRATTI\")
                    Case "2"
                        percorso = Server.MapPath("ALLEGATI\CONDOMINI\")
                    Case "3"
                        percorso = Server.MapPath("ALLEGATI\APPALTI\")
                    Case "4"
                        percorso = Server.MapPath("ALLEGATI\FORNITORI\")
                    Case "5"
                        percorso = Server.MapPath("ALLEGATI\AUTOGESTIONI\")
                    Case "6"
                        percorso = Server.MapPath("ALLEGATI\MANUTENZIONI\")
                    Case "7"
                        percorso = Server.MapPath("ALLEGATI\PAGAMENTI\")

                End Select

                Dim i As Integer = 0
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(percorso, FileIO.SearchOption.SearchTopLevelOnly, cmbTipoAllegato.SelectedItem.Value & "_*.zip")
                    i = i + 1
                Next


                If i = 0 Then
                    par.cmd.CommandText = "delete from siscom_mi.tab_modelli_allegati where tipo=" & tipo.Value & " and cod='" & cmbTipoAllegato.SelectedItem.Value & "'"
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Operazione effettuata!');</script>")

                Else
                    Response.Write("<script>alert('Non è possibile eliminare...la tipologia del documento è in uso!');</script>")
                End If

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If i = 0 Then
                    CaricaTipologieAllegati()
                End If

            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try
        End If
    End Sub
End Class
