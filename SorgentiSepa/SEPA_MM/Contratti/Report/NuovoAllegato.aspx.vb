Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Report_NuovoAllegato
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


    'Private Function CaricaTitolo()
    '    Try
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '        lblErrore.Visible = False

    '        Select Case tipo.Value
    '            Case "1"
    '                lblTitolo.Text = "Allega documento a rapporto cod. " & identificativo.Value
    '            Case "2"
    '                lblTitolo.Text = "Allega documento a condominio cod. " & Format(CDbl(identificativo.Value), "00000")
    '            Case "3"
    '                par.cmd.CommandText = "select * from siscom_mi.appalti where id=" & identificativo.Value
    '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReader.Read Then
    '                    lblTitolo.Text = "Allega documento ad appalto num. repertorio " & par.IfNull(myReader("num_repertorio"), "")
    '                End If
    '                myReader.Close()

    '            Case "4"
    '                par.cmd.CommandText = "select * from siscom_mi.fornitori where id=" & identificativo.Value
    '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReader.Read Then
    '                    If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
    '                        lblTitolo.Text = "Allega documento a fornitore " & par.IfNull(myReader("ragione_sociale"), "")
    '                    Else
    '                        lblTitolo.Text = "Allega documento a fornitore " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")
    '                    End If
    '                End If
    '                myReader.Close()
    '            Case "5"
    '                par.cmd.CommandText = "select * from siscom_mi.AUTOGESTIONI where id=" & identificativo.Value
    '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReader.Read Then
    '                    If par.IfNull(myReader("ID_COMPLESSO"), -1) <> -1 Then
    '                        par.cmd.CommandText = "select * from siscom_mi.COMPLESSI_IMMOBILIARI where id=" & par.IfNull(myReader("ID_COMPLESSO"), -1)
    '                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                        If myReader1.Read Then
    '                            lblTitolo.Text = "Allega documento a Autogestione su Complesso " & par.IfNull(myReader1("denominazione"), "")
    '                        End If
    '                        myReader1.Close()

    '                    Else
    '                        par.cmd.CommandText = "select * from siscom_mi.edifici where id=" & par.IfNull(myReader("ID_edificio"), -1)
    '                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                        If myReader1.Read Then
    '                            lblTitolo.Text = "Allega documento a Autogestione su Edificio " & par.IfNull(myReader1("denominazione"), "")
    '                        End If
    '                        myReader1.Close()
    '                    End If
    '                End If
    '                myReader.Close()

    '        End Select


    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    '    Catch ex As Exception
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        lblErrore.Text = ex.Message
    '        lblErrore.Visible = True
    '    End Try
    'End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            tipo.Value = Request.QueryString("T")
            'CaricaTitolo()
            CaricaTipologieAllegati()
        End If
        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtRiferimentoDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtRiferimentoA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub ImgElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgElimina.Click
        If sicuro.Value = True Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                lblErrore.Visible = False

                Dim percorso As String = ""

                percorso = Server.MapPath("..\..\ALLEGATI\ACCERTATO\")

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

            Catch ex1 As Oracle.DataAccess.Client.OracleException
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                If EX1.Number <> 2292 Then
                    lblErrore.Text = ex1.Message
                    lblErrore.Visible = True
                Else
                    lblErrore.Text = "Tipologia in uso! Non è possibile cancellare"
                    lblErrore.Visible = True
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

    Protected Sub ImgNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNuovo.Click
        CaricaTipologieAllegati()
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            lblErrore.Visible = False

            If cmbTipoAllegato.SelectedIndex <> -1 Then
                If txtRiferimentoA.Text <> "" And txtRiferimentoDa.Text <> "" And txtData.Text <> "" Then
                    Dim nFile As String = ""
                    Dim percorso As String = ""
                    Dim fileDescrizione As String = ""
                    Dim zipfic As String = ""
                    Dim nomefile As String = cmbTipoAllegato.SelectedItem.Value & "_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                    If FileUpload1.HasFile = True Then


                        percorso = Server.MapPath("..\..\ALLEGATI\ACCERTATO\")

                        nFile = percorso & FileUpload1.FileName
                        zipfic = percorso & nomefile
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
                        strmZipOutputStream.Finish()
                        strmZipOutputStream.Close()

                        par.cmd.CommandText = "insert into siscom_mi.ALLEGATI_ACCERTATO (id,id_tipo_allegato,data_emissione,periodo_da,periodo_a,file_allegato,id_operatore,data_inserimento) values (siscom_mi.seq_allegati_accertato.nextval, " & CInt(cmbTipoAllegato.SelectedItem.Value) & ",'" & par.AggiustaData(txtData.Text) & "','" & par.AggiustaData(txtRiferimentoDa.Text) & "','" & par.AggiustaData(txtRiferimentoA.Text) & "','" & par.PulisciStrSql(nomefile) & "'," & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "')"
                        par.cmd.ExecuteNonQuery()

                        Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")
                    Else
                        lblErrore.Visible = True
                        lblErrore.Text = "Specificare il file per l'allegato!"
                    End If
                Else
                    lblErrore.Visible = True
                    lblErrore.Text = "Specificare tutte le date!"
                End If

            Else
                lblErrore.Visible = True
                lblErrore.Text = "Specificare una tipologia di allegato!"
            End If
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
