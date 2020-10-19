Imports System
Imports System.Data
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_CaricaRUAU
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0
    Dim NomeF As String = ""

    Private Sub ZippaFiles(ByVal nomefile As String)
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\FileTemp\" & nomefile & ".zip")

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        strFile = Server.MapPath("..\FileTemp\" & nomefile & ".txt")
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '
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

    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If (Session.Item("FL_RUAU") = "0" And Session.Item("FL_RUSALDI") = "0") Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Request.QueryString("T") = "1" Then
            Label26.Text = "Funzione per la Generazione del Report RU-AU"
            ORIGINE.Value = "1"
        Else
            Label26.Text = "Funzione per la Generazione del Report RU-Saldi"
            ORIGINE.Value = "2"
        End If
    End Sub

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


    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim FileName As String = UCase(UploadOnServer())
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".TXT") Then
                    If ORIGINE.Value = "1" Then
                        ReadFileTXT(FileName)
                    Else
                        ReadFileTXTRUSALDI(FileName)
                    End If
                Else
                    Response.Write("<script>alert('Tipo file non valido. Selezionare un file txt');</script>")
                End If
                End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReadFileTXTRUSALDI(ByVal FileTXT As String)
        Try
            Dim sContenutoRiga As String = ""
            Dim dt As New Data.DataTable
            Dim ContaRighe As Integer = 0
            Dim ContrattiErrati As String = ""
            Dim ContrattiCorretti As String = ""
            Dim ElencoRisultati As String = ""

            Dim AREA_PRE_2011 As String = ""
            Dim CLASSE_PRE_2011 As String = ""
            Dim PROVENIENZA_PRE_2011 As String = ""

            Dim AREA_2011 As String = ""
            Dim CLASSE_2011 As String = ""

            Dim AREA_2013 As String = ""
            Dim CLASSE_2013 As String = ""
            Dim NUM_COMP As String = ""
            Dim ISEE As String = ""
            Dim SUP_NETTA As String = ""

            Dim RigaDaInserire As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sr1 As StreamReader = New StreamReader(FileTXT, System.Text.Encoding.GetEncoding("iso-8859-1"))
            Do While sr1.Peek() >= 0
                sContenutoRiga = sr1.ReadLine()
                If sContenutoRiga <> "" Then
                    ContrattiCorretti = ContrattiCorretti & sContenutoRiga
                    ContaRighe += 1
                    If ContaRighe >= 1000 Then
                        Exit Do
                    End If
                Else
                    ContrattiErrati = ContrattiErrati & "<br/>"
                End If
            Loop
            sr1.Close()
            Dim FileCreato As String = ""
            Dim AREA As String = ""
            Dim CLASSE As String = ""

            If ContaRighe > 0 Then
                FileCreato = Replace(FileTXT, ".TXT", "_1.TXT")
                Dim sr As StreamWriter = New StreamWriter(FileCreato, False, System.Text.Encoding.Default)

                sr.WriteLine("COD_CONTRATTO#INDIRIZZO_UNITA#CIVICO_UNITA#INTERNO_UNITA#SCALA_UNITA#PIANO_UNITA#DATA_DECORRENZA#DATA_STIPULA#DATA_DISDETTA#DATA_SLOGGIO#INTESTATARIO#NOTE_CONTRATTO#STATO_CONTRATTO#TIPO_CONTRATTO#TIPO_UNITA#SALDO#C.F.#IMPORTO CANONE ATTUALE#AREA#CLASSE#")


                ContrattiCorretti = Replace(ContrattiCorretti, ";", "','")
                ContrattiCorretti = Mid(ContrattiCorretti, 1, Len(ContrattiCorretti) - 3)


                par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID AS IDC,UNITA_IMMOBILIARI.INTERNO, TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO_UI,SCALE_EDIFICI.DESCRIZIONE AS SCALA_UI,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPO_UI,INDIRIZZI.DESCRIZIONE AS INDIRIZZO,INDIRIZZI.CIVICO,INDIRIZZI.CAP,SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS STATO_CONTRATTO,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_RICONSEGNA,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_DISDETTA_LOCATARIO,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_STIPULA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_STIPULA,rapporti_utenza.id,NOTE,COD_CONTRATTO,cod_tipologia_contr_loc,siscom_mi.getintestatari(rapporti_utenza.ID) AS intestatario,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_DECORRENZA,(SELECT (SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0))-(SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)))) FROM SISCOM_MI.BOL_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND ID_CONTRATTO = RAPPORTI_UTENZA.ID) AS SALDO_TOTALE,RAPPORTI_UTENZA.NOTE,ANAGRAFICA.COD_FISCALE,RAPPORTI_UTENZA.IMP_CANONE_INIZIALE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND TIPO_LIVELLO_PIANO.COD(+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.ID_SCALA AND TIPOLOGIA_UNITA_IMMOBILIARI.COD (+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND INDIRIZZI.ID (+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID (+)=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_CONTRATTO IN ('" & ContrattiCorretti & "')"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read
                    AREA = ""
                    CLASSE = ""
                    par.cmd.CommandText = "SELECT CANONI_EC.SOTTO_AREA,AREA_ECONOMICA.DESCRIZIONE FROM SISCOM_MI.CANONI_EC,SISCOM_MI.AREA_ECONOMICA WHERE AREA_ECONOMICA.ID=CANONI_EC.ID_AREA_ECONOMICA AND CANONI_EC.ID_CONTRATTO=" & par.IfNull(myReader("IDC"), "-1") & " AND CANONI_EC.DATA_CALCOLO=(SELECT MAX(DATA_CALCOLO) FROM SISCOM_MI.CANONI_EC EC WHERE EC.ID_CONTRATTO=CANONI_EC.ID_CONTRATTO)"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        AREA = par.IfNull(myReader1("DESCRIZIONE"), "")
                        CLASSE = par.IfNull(myReader1("SOTTO_AREA"), "")
                    End If
                    myReader1.Close()
                    sr.WriteLine(par.IfNull(myReader("COD_CONTRATTO"), "") & "#" & par.IfNull(myReader("INDIRIZZO"), "") & "#" & par.IfNull(myReader("CIVICO"), "") & "#" & par.IfNull(myReader("INTERNO"), "") & "#" & par.IfNull(myReader("SCALA_UI"), "") & "#" & par.IfNull(myReader("PIANO_UI"), "") & "#" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "#" & par.IfNull(myReader("DATA_STIPULA"), "") & "#" & par.IfNull(myReader("DATA_DISDETTA_LOCATARIO"), "") & "#" & par.IfNull(myReader("DATA_RICONSEGNA"), "") & "#" & par.IfNull(myReader("INTESTATARIO"), "") & "#" & Replace(par.IfNull(myReader("NOTE"), ""), vbCrLf, " ") & "#" & par.IfNull(myReader("STATO_CONTRATTO"), "") & "#" & par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") & "#" & par.IfNull(myReader("TIPO_UI"), "") & "#" & par.IfNull(myReader("SALDO_TOTALE"), "") & "#" & par.IfNull(myReader("COD_FISCALE"), "") & "#" & par.IfNull(myReader("IMP_CANONE_INIZIALE"), "") & "#" & AREA & "#" & CLASSE & "#")
                Loop
                myReader.Close()
                RigaDaInserire = ""
                sr.Close()

            Else
                Response.Write("<script>alert('Nessun contratto presente nel file');</script>")
            End If

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If ContaRighe > 0 Then
                Dim filename As String = Replace(Replace(UCase(FileCreato), UCase(Server.MapPath("..\FileTemp\")), ""), ".TXT", "")
                ZippaFiles(filename)
                Response.Write("<script>window.open('../FileTemp/" & filename & ".zip','','');</script>")
                'Response.Redirect("pagina_home.aspx")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:ReadFileTXT " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub ReadFileTXT(ByVal FileTXT As String)
        Try
            Dim sContenutoRiga As String = ""
            Dim dt As New Data.DataTable
            Dim ContaRighe As Integer = 0
            Dim ContrattiErrati As String = ""
            Dim ContrattiCorretti As String = ""
            Dim ElencoRisultati As String = ""

            Dim AREA_PRE_2011 As String = ""
            Dim CLASSE_PRE_2011 As String = ""
            Dim PROVENIENZA_PRE_2011 As String = ""

            Dim AREA_2011 As String = ""
            Dim CLASSE_2011 As String = ""

            Dim AREA_2013 As String = ""
            Dim CLASSE_2013 As String = ""
            Dim NUM_COMP As String = ""
            Dim ISEE As String = ""
            Dim SUP_NETTA As String = ""

            Dim RigaDaInserire As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sr1 As StreamReader = New StreamReader(FileTXT, System.Text.Encoding.GetEncoding("iso-8859-1"))
            Do While sr1.Peek() >= 0
                sContenutoRiga = sr1.ReadLine()
                If sContenutoRiga <> "" Then
                    ContrattiCorretti = ContrattiCorretti & sContenutoRiga
                    ContaRighe += 1
                    If ContaRighe >= 1000 Then
                        Exit Do
                    End If
                Else
                    ContrattiErrati = ContrattiErrati & "<br/>"
                End If
            Loop
            sr1.Close()
            Dim FileCreato As String = ""
            If ContaRighe > 0 Then
                FileCreato = Replace(FileTXT, ".TXT", "_1.TXT")
                Dim sr As StreamWriter = New StreamWriter(FileCreato, False, System.Text.Encoding.Default)
                sr.WriteLine("COD_CONTRATTO#TIPO_CONTRATTO#INTESTATARIO#DATA_DECORRENZA#SALDO_TOTALE#AREA_PRE_AU_2011#CLASSE_PRE_AU_2011#PROVENIENZA_PRE_AU_2011#AREA_2011#CLASSE_2011#AREA_2013#CLASSE_2013#NUM_COMPONENTI_BASE_AU_2013#SUP_NETTA_BASE_AU_2013#ISEE_BASE_AU_2013#NOTE#")

                ContrattiCorretti = Replace(ContrattiCorretti, ";", "','")
                ContrattiCorretti = Mid(ContrattiCorretti, 1, Len(ContrattiCorretti) - 3)


                par.cmd.CommandText = "SELECT id,NOTE,COD_CONTRATTO,cod_tipologia_contr_loc,siscom_mi.getintestatari(rapporti_utenza.ID) AS intestatario,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_DECORRENZA,(SELECT (SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0))-(SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)))) FROM SISCOM_MI.BOL_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND ID_CONTRATTO = RAPPORTI_UTENZA.ID) AS SALDO_TOTALE,RAPPORTI_UTENZA.NOTE FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO IN ('" & ContrattiCorretti & "')"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read
                    RigaDaInserire = ""
                    AREA_PRE_2011 = ""
                    CLASSE_PRE_2011 = ""
                    AREA_2011 = ""
                    CLASSE_2011 = ""
                    AREA_2013 = ""
                    CLASSE_2013 = ""
                    PROVENIENZA_PRE_2011 = ""
                    NUM_COMP = ""
                    ISEE = ""
                    SUP_NETTA = ""

                    par.cmd.CommandText = "SELECT DECODE(ID_AREA_ECONOMICA,1,'PROTEZIONE',2,'ACCESSO',3,'PERMANENZA',4,'DECADENZA') AS AREA,SOTTO_AREA AS CLASSE,DECODE(TIPO_PROVENIENZA,1,'GEST.LOCATARI',2,'AU 2009') AS PROVENIENZA FROM SISCOM_MI.CANONI_EC WHERE TIPO_PROVENIENZA IN (1,2) AND INIZIO_VALIDITA_CAN<='20120101' AND ID_CONTRATTO=" & par.IfNull(myReader("ID"), "-1") & " ORDER BY DATA_CALCOLO DESC"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        AREA_PRE_2011 = par.IfNull(myReader1("AREA"), "")
                        CLASSE_PRE_2011 = par.IfNull(myReader1("CLASSE"), "")
                        PROVENIENZA_PRE_2011 = par.IfNull(myReader1("PROVENIENZA"), "")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT DECODE(ID_AREA_ECONOMICA,1,'PROTEZIONE',2,'ACCESSO',3,'PERMANENZA',4,'DECADENZA') AS AREA,SOTTO_AREA AS CLASSE,'AU 2011' AS PROVENIENZA FROM SISCOM_MI.CANONI_EC WHERE TIPO_PROVENIENZA IN (5) AND ID_CONTRATTO=" & par.IfNull(myReader("ID"), "-1") & " ORDER BY DATA_CALCOLO DESC"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        AREA_2011 = par.IfNull(myReader1("AREA"), "")
                        CLASSE_2011 = par.IfNull(myReader1("CLASSE"), "")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT NUM_COMP,ISEE,SUP_NETTA,DECODE(ID_AREA_ECONOMICA,1,'PROTEZIONE',2,'ACCESSO',3,'PERMANENZA',4,'DECADENZA') AS AREA,SOTTO_AREA AS CLASSE,'AU 2013' AS PROVENIENZA FROM SISCOM_MI.CANONI_EC WHERE TIPO_PROVENIENZA IN (8) AND ID_CONTRATTO=" & par.IfNull(myReader("ID"), "-1") & " ORDER BY DATA_CALCOLO DESC"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        AREA_2013 = par.IfNull(myReader1("AREA"), "")
                        CLASSE_2013 = par.IfNull(myReader1("CLASSE"), "")
                        NUM_COMP = par.IfNull(myReader1("NUM_COMP"), "")
                        ISEE = par.IfNull(myReader1("ISEE"), "")
                        SUP_NETTA = par.IfNull(myReader1("SUP_NETTA"), "")
                    End If
                    myReader1.Close()

                    sr.WriteLine(par.IfNull(myReader("COD_CONTRATTO"), "") & "#" & par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") & "#" & par.IfNull(myReader("INTESTATARIO"), "") & "#" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "#" & par.IfNull(myReader("SALDO_TOTALE"), "") & "#" & AREA_PRE_2011 & "#" & CLASSE_PRE_2011 & "#" & PROVENIENZA_PRE_2011 & "#" & AREA_2011 & "#" & CLASSE_2011 & "#" & AREA_2013 & "#" & CLASSE_2013 & "#" & NUM_COMP & "#" & SUP_NETTA & "#" & ISEE & "#" & Replace(par.IfNull(myReader("NOTE"), ""), vbCrLf, " ") & "#")
                Loop
                myReader.Close()
                RigaDaInserire = ""
                sr.Close()

            Else
                Response.Write("<script>alert('Nessun contratto presente nel file');</script>")
            End If

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If ContaRighe > 0 Then
                Dim filename As String = Replace(Replace(UCase(FileCreato), UCase(Server.MapPath("..\FileTemp\")), ""), ".TXT", "")
                ZippaFiles(filename)
                Response.Write("<script>window.open('../FileTemp/" & filename & ".zip','','');</script>")
                'Response.Redirect("pagina_home.aspx")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:ReadFileTXT " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE TXT ##########
            If FileUpload.HasFile = True Then
                UploadOnServer = Replace(UCase(FileUpload.FileName), ".TXT", "_" & Format(Now, "yyyyMMddHHmmss")) & ".TXT"
                UploadOnServer = Server.MapPath("..\FileTemp\") & UploadOnServer
                FileUpload.SaveAs(UploadOnServer)
            End If
        Catch ex As Exception
            UploadOnServer = ""
            Session.Add("ERRORE", "Provenienza:UploadOnServer " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return UploadOnServer
    End Function

End Class
