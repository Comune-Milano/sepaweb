Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Canone_CON
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim cognome As String = ""
    Dim nome As String = ""
    Dim cf As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Indice = Request.QueryString("C")
            Unita = Request.QueryString("U")
            CODICEUnita = Request.QueryString("CODICE")

        End If
        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Public Property Unita() As String
        Get
            If Not (ViewState("par_Unita") Is Nothing) Then
                Return CStr(ViewState("par_Unita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Unita") = value
        End Set

    End Property

    Public Property Indice() As String
        Get
            If Not (ViewState("par_Indice") Is Nothing) Then
                Return CStr(ViewState("par_Indice"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Indice") = value
        End Set

    End Property

    Public Property CODICEUnita() As String
        Get
            If Not (ViewState("par_CODICEUnita") Is Nothing) Then
                Return CStr(ViewState("par_CODICEUnita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CODICEUnita") = value
        End Set

    End Property

    Public Property CodiceContratto() As String
        Get
            If Not (ViewState("par_CodiceContratto") Is Nothing) Then
                Return CStr(ViewState("par_CodiceContratto"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CodiceContratto") = value
        End Set

    End Property


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Private Function CaricaAllegati()
        Dim nFile As String = ""
        'convocazione per sopralluogo
        If FileUpload1.HasFile = True Then
            nFile = Server.MapPath("..\ALLEGATI\CONTRATTI\" & FileUpload1.FileName)
            FileUpload1.SaveAs(nFile)
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\001_" & CodiceContratto & ".zip")
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
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
        End If

        'verbalizzazione sopralluogo
        If FileUpload2.HasFile = True Then
            nFile = Server.MapPath("..\ALLEGATI\CONTRATTI\" & FileUpload1.FileName)
            FileUpload2.SaveAs(nFile)
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\002_" & CodiceContratto & ".zip")
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
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
        End If

        'Accettazione
        If FileUpload3.HasFile = True Then
            nFile = Server.MapPath("..\ALLEGATI\CONTRATTI\" & FileUpload1.FileName)
            FileUpload3.SaveAs(nFile)
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\003_" & CodiceContratto & ".zip")
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
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
        End If
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            If txtpg.Text <> "" And txtCanoneCorrente.Text <> "" And txtData.Text <> "" And par.AggiustaData(txtData.Text) <= Format(Now, "yyyyMMdd") Then

                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim progressivo As Integer
                par.cmd.CommandText = "select MAX(TO_NUMBER(TRANSLATE(SUBSTR(cod_contratto,18,2),'A','0'))) from SISCOM_MI.rapporti_utenza where SUBSTR(cod_contratto,1,17)='" & CODICEUnita & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    progressivo = par.IfNull(myReader1(0), 0) + 1
                Else
                    progressivo = 1
                End If
                myReader1.Close()

                CodiceContratto = CodiceContratto & Format((progressivo), "00")

                par.cmd.CommandText = "select * from siscom_mi.anagrafica where id=" & Indice
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read = True Then
                    If par.IfNull(myReader("ragione_sociale"), "") = "" Then
                        cognome = par.IfNull(myReader("cognome"), "")
                        nome = par.IfNull(myReader("nome"), "")
                    Else
                        cognome = par.IfNull(myReader("ragione_sociale"), "")
                        nome = ""
                    End If
                    If par.IfNull(myReader("partita_iva"), "") = "" Then
                        cf = par.IfNull(myReader("cod_fiscale"), "")
                    Else
                        cf = par.IfNull(myReader("partita_iva"), "")
                    End If

                    par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
                                        & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA,PROVVEDIMENTO,DATA_PROVVEDIMENTO) " _
                                        & " Values " _
                                        & "(-1, " & Unita & ", '" & par.AggiustaData(txtData.Text) & "', 0, -1, " _
                                        & "'" & par.PulisciStrSql(cognome) & "', '" & par.PulisciStrSql(nome) _
                                        & "', '" & par.PulisciStrSql(cf) & "', 'A', 0," & par.VirgoleInPunti(txtCanoneCorrente.Text) & "," & Indice & ",'" & par.PulisciStrSql(txtpg.Text) & "','" & par.AggiustaData(txtData.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update alloggi set stato='8',assegnato='1',DATA_PRENOTATO='" & par.AggiustaData(txtData.Text) & "' where COD_ALLOGGIO='" & CODICEUnita & "'"
                    par.cmd.ExecuteNonQuery()


                    CaricaAllegati()

                    'Response.Write("<script>alert('Operazione Effettuata. Ora è possibile inserire il contratto!');</script>")
                    'Response.Write("<script>window.close();</script>")
                    Dim SriptJSContratto As String = "var chiediConferma;" _
                                              & "chiediConferma = window.confirm('Operazione Effettuata!\nSi vuole inserire immediatamente il nuovo contratto?');" _
                                              & "if (chiediConferma == true) { " _
                                              & "a = window.open('Contratto.aspx?ID=-1&IdDichiarazione=" & Indice & "&IdDomanda=-1&IdUnita=" & Unita & "&TIPO=12&Lett=A','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" _
                                              & "window.close();}" _
                                              & "else {" _
                                              & "window.close();" _
                                              & "}"
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "conf", SriptJSContratto, True)
                End If
                myReader.Close()
                par.OracleConn.Close()
            Else
                Response.Write("<script>alert('Dati mancanti o errati! Si ricorda che la data di assegnazione deve essere inferiore la data odierna!');</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub
End Class
