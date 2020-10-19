Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_CanoneFO
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Indice = Request.QueryString("IDC")
            Unita = Request.QueryString("IDU")


            Canone27()
        End If
        txtDataProvvedimento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub


    Private Function Canone27()
        Dim IMPORTO As Double = 0
        Dim LOCATIVO As String = ""


        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT COD_CONTRATTO_SCAMBIO,ID_DICHIARAZIONE FROM DOMANDE_BANDO_VSA WHERE ID_dichiarazione=" & Indice
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CodUnita = Mid(par.IfNull(myReader("COD_CONTRATTO_SCAMBIO"), ""), 1, 17)
                Indice = par.IfNull(myReader("ID_DICHIARAZIONE"), "0")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & CodUnita & "'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Unita = par.IfNull(myReader("ID"), 0)
            End If
            myReader.Close()

            'Dim StatoAlloggio As String = ""

            'par.cmd.CommandText = "select stato from ALLOGGI where COD_ALLOGGIO='" & CodUnita & "'"
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    StatoAlloggio = par.IfNull(myReader("stato"), "")

            'End If
            'myReader.Close()
            'If StatoAlloggio <> "5" Then
            '    Label5.Text = "Attenzione...non è possibile procedere con il cambio alloggio. I rapporti oggetto del cambio devono essere chiusi e deve essere stata effettuata la verifica dello stato manutentivo."
            '    Label5.Visible = True
            '    ImgProcedi.Visible = False
            'End If


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
           
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label5.Text = "ERRORE..." & ex.Message
        End Try

    End Function


    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property


    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set

    End Property


    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property

    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property

    Public Property CodUnita() As String
        Get
            If Not (ViewState("par_CodUnita") Is Nothing) Then
                Return CStr(ViewState("par_CodUnita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CodUnita") = value
        End Set

    End Property


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

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Dim COGNOME As String = ""
        Dim NOME As String = ""
        Dim CF As String = ""
        Dim CANONE As String = "0"
        Dim APPLICATO As String = ""
        Dim canone_alt As String = "0"


        Try
            'If txtData.Text <> "" And par.AggiustaData(txtData.Text) <= Format(Now, "yyyyMMdd") And txtNumProvvedimento.Text <> "" And txtDataProvvedimento.Text <> "" And par.AggiustaData(txtDataProvvedimento.Text) <= Format(Now, "yyyyMMdd") Then
            If txtNumProvvedimento.Text <> "" And txtDataProvvedimento.Text <> "" And par.AggiustaData(txtDataProvvedimento.Text) <= Format(Now, "yyyyMMdd") Then
                
                par.OracleConn.Open()
                par.SettaCommand(par)

                'Dim progressivo As Integer
                'par.cmd.CommandText = "select MAX(TO_NUMBER(TRANSLATE(SUBSTR(cod_contratto,18,2),'A','0'))) from SISCOM_MI.rapporti_utenza where SUBSTR(cod_contratto,1,17)='" & CodUnita & "'"
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    progressivo = par.IfNull(myReader1(0), 0) + 1
                'Else
                '    progressivo = 1
                'End If
                'myReader1.Close()

                'CodiceContratto = CodUnita & Format((progressivo), "00")
                Dim iddom As String = ""

                par.cmd.CommandText = "select * from COMP_NUCLEO_VSA where PROGR=0 AND id_DICHIARAZIONE=" & Indice
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read = True Then

                    par.cmd.CommandText = "select id from domande_bando_VSA where id_DICHIARAZIONE=" & Indice
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123.Read Then
                        iddom = par.IfNull(myReader123("id"), "-1")
                    End If
                    myReader123.Close()

                    If par.IfNull(myReader("cognome"), "") <> "" Then
                        COGNOME = par.IfNull(myReader("cognome"), "")
                        NOME = par.IfNull(myReader("nome"), "")
                    Else
                        COGNOME = ""
                        NOME = ""
                    End If
                    If par.IfNull(myReader("cod_fiscale"), "") <> "" Then
                        CF = par.IfNull(myReader("cod_fiscale"), "")
                    Else
                        'cf = par.IfNull(myReader("partita_iva"), "")
                    End If

                    par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
                                        & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA,canone_alt,TIPO_APPLICATO,PROVVEDIMENTO,DATA_PROVVEDIMENTO) " _
                                        & " Values " _
                                        & "(" & iddom & ", " & Unita & ", '" & par.AggiustaData(txtDataProvvedimento.Text) & "', 0, " & Indice & ", " _
                                        & "'" & par.PulisciStrSql(COGNOME) & "', '" & par.PulisciStrSql(NOME) _
                                        & "', '" & par.PulisciStrSql(CF) & "', 'P', 0,null,-1,'','','" & par.PulisciStrSql(txtNumProvvedimento.Text) & "','" & par.AggiustaData(txtDataProvvedimento.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update ALLOGGI set stato='8',assegnato='1',DATA_PRENOTATO='" & par.AggiustaData(txtDataProvvedimento.Text) & "' where COD_ALLOGGIO='" & CodUnita & "'"
                    par.cmd.ExecuteNonQuery()

                    CaricaAllegati()

                    'Response.Write("<script>alert('Operazione Effettuata. Ora è possibile inserire il contratto!');</script>")
                    'Response.Write("<script>window.close();</script>")

                    Dim SriptJSContratto As String = "var chiediConferma;" _
                                              & "chiediConferma = window.confirm('Operazione Effettuata!\nSi vuole inserire immediatamente il nuovo contratto?');" _
                                              & "if (chiediConferma == true) { " _
                                              & "a = window.open('Contratto.aspx?ID=-1&IdDichiarazione=" & Indice & "&IdDomanda=" & iddom & "&IdUnita=" & Unita & "&TIPO=11&Lett=P','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" _
                                              & "window.close();}" _
                                              & "else {" _
                                              & "window.close();" _
                                              & "}"
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "conf", SriptJSContratto, True)
                End If
                myReader.Close()
                par.OracleConn.Close()
            Else
                Response.Write("<script>alert('Dati mancanti o errati! Si ricorda che la data di assegnazione e provvedimento devono essere precedenti alla data odierna!');</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Label5.Text = ex.Message
            Label5.Visible = True
        End Try
    End Sub

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
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Private Sub ImgIndietro_Click(sender As Object, e As ImageClickEventArgs) Handles ImgIndietro.Click
        Response.Write("<script>top.location.href='DichiarazioneCS.aspx';</script>")
    End Sub
End Class
