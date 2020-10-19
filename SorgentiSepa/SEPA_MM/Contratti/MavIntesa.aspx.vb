Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Partial Class Contratti_MavIntesa
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim Indice As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            idbolletta.Value = Request.QueryString("X")
            CaricaDati()

            Try
                Dim indirizzo As String = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & CInt(lblNumeroMav.Text) & ".pdf"
                If IO.File.Exists(indirizzo) = True Then
                    Response.Write("<script>window.location.href='../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & CInt(lblNumeroMav.Text) & ".pdf';</script>")
                Else
                    indirizzo = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(CInt(lblNumeroMav.Text), "0000000000") & ".pdf"
                    If IO.File.Exists(indirizzo) = True Then
                        Response.Write("<script>window.location.href='../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & Format(CInt(lblNumeroMav.Text), "0000000000") & ".pdf';</script>")
                    End If
                End If
            Catch ex As Exception
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
                Button1.Visible = False
            End Try


        End If


    End Sub

    Private Sub CaricaDati()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=31"
            Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader123.Read Then
                causalepagamento.Value = par.IfNull(myReader123("valore"), "")
            End If
            myReader123.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='EXPECT100CONTINUE'"
            myReader123 = par.cmd.ExecuteReader()
            If myReader123.Read Then
                DisabilitaExpect100Continue = par.IfNull(myReader123("valore"), "0")
            End If
            myReader123.Close()

            par.cmd.CommandText = "select bol_bollette.*,anagrafica.cognome,anagrafica.nome,anagrafica.cod_fiscale,anagrafica.partita_iva,anagrafica.ragione_sociale from siscom_mi.bol_bollette,siscom_mi.anagrafica where anagrafica.id=bol_bollette.cod_affittuario and bol_bollette.id=" & idbolletta.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("id_tipo"), "") <> 12 _
                    And (par.IfNull(myReader("rif_file"), "") = "FIN" Or _
                         par.IfNull(myReader("rif_file"), "") = "MOD" Or _
                         par.IfNull(myReader("RIF_FILE"), "") = "REC" Or _
                         par.IfNull(myReader("rif_file"), "") = "MOR" Or _
                         par.IfNull(myReader("rif_file"), "") = "MAV") Then

                    tipoBolletta.Value = par.IfNull(myReader("id_tipo"), "")
                    lblNumeroMav.Text = Format(par.IfNull(myReader("id"), "0000000000"), "0000000000")
                    If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                        lblCognome.Text = Mid(par.IfNull(myReader("intestatario"), ""), 1, 30) 'Mid(par.IfNull(myReader("ragione_sociale"), ""), 1, 30)
                        lblNome.Text = ""
                        lblCodFiscale.Text = par.IfNull(myReader("partita_iva"), "11111111111")
                        lblCodDebitore.Text = Format(par.IfNull(myReader("cod_affittuario"), ""), "0000000000")
                    Else
                        lblCognome.Text = Mid(par.IfNull(myReader("intestatario"), ""), 1, 30) 'Mid(par.IfNull(myReader("cognome"), ""), 1, 30)
                        lblNome.Text = "" 'Mid(par.IfNull(myReader("nome"), ""), 1, 30)
                        lblCodFiscale.Text = par.IfNull(myReader("cod_fiscale"), "XXXXXXXXXXXXXXXX")
                        lblCodDebitore.Text = Format(par.IfNull(myReader("cod_affittuario"), ""), "0000000000")
                    End If


                    lblScadenza.Text = Mid(par.IfNull(myReader("data_scadenza"), "00000000"), 1, 4) & "-" & Mid(par.IfNull(myReader("data_scadenza"), "00000000"), 5, 2) & "-" & Mid(par.IfNull(myReader("data_scadenza"), "00000000"), 7, 2)

                    ' lblScadenza.Text = "220410"

                    lblCausale.Text = par.IfNull(myReader("note"), "")
                    lblindirizzo.Text = par.IfNull(myReader("indirizzo"), "")

                    lblLocalita.Text = Trim(Mid(Mid(par.IfNull(myReader("cap_citta"), ""), 7, 30), 1, Len(par.IfNull(myReader("cap_citta"), "0000000")) - 10))

                    lblProvincia.Text = Mid(par.IfNull(myReader("cap_citta"), ""), Len(par.IfNull(myReader("cap_citta"), "0000000")) - 2, 2)
                    lblCap.Text = Mid(par.IfNull(myReader("cap_citta"), ""), 1, 5)

                    par.cmd.CommandText = "select sum(importo) from siscom_mi.bol_bollette_voci where id_bolletta=" & idbolletta.Value
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        lblimporto.Text = par.IfNull(myReader1(0), "0")
                        If lblimporto.Text <= 0 Then
                            'Response.Write("<script>alert('Attenzione, questa bolletta non può essere pagata perchè di importo negativo!');</script>")
                            lblErrore.Text = "Attenzione, questa bolletta non può essere pagata perchè di importo negativo!"
                            lblErrore.Visible = True
                            Button1.Visible = False
                        End If
                    End If
                    myReader1.Close()
                Else
                    lblNumeroMav.Text = Format(par.IfNull(myReader("id"), ""), "")
                    'lblNumeroMav.Text = Format(par.IfNull(myReader("id"), "0000000000"), "0000000000")
                    'Response.Write("<script>alert('Attenzione, questa bolletta non può essere pagata tramite Mav on Line Banca Intesa!');self.close();</script>")
                    lblErrore.Text = "Attenzione, questa bolletta non può essere pagata tramite Mav on Line Banca Intesa!"
                    lblErrore.Visible = True

                    Button1.Visible = False
                End If
            Else
                'Response.Write("<script>alert('Attenzione, probabile errore nel recupero dei dati della bolletta! Segnalare il problema all\'amministratore del sistema.');self.close();</script>")
                lblErrore.Text = "Attenzione, probabile errore nel recupero dei dati della bolletta! Segnalare il problema all\'amministratore del sistema."
                lblErrore.Visible = True
                Button1.Visible = False
            End If
            myReader.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
            Button1.Visible = False
        End Try

    End Sub

    Private Function CreaCausale() As String
        Try
            Dim sCausale As String = ""
            Dim sImporto As String = ""
            Dim iDifferenza As Integer = 0
            Dim sDescrizione As String = ""

            If lblCausale.Text <> "" Then
                sCausale = lblCausale.Text.PadRight(55)
            End If
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select t_voci_bolletta.descrizione,bol_bollette_voci.importo from siscom_mi.bol_bollette,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where bol_bollette_voci.id_bolletta=bol_bollette.id and t_voci_bolletta.id=bol_bollette_voci.id_voce and bol_bollette.id=" & idbolletta.Value & " order by t_voci_bolletta.descrizione asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                sImporto = Format(par.IfNull(myReader("importo"), "0"), "##,##0.00")
                If sImporto < 1 And sImporto > 0 Then
                    If Mid(sImporto, 1, 1) <> "0" Then sImporto = "0" & sImporto
                End If

                If sImporto > -1 And sImporto < 0 Then
                    If Mid(sImporto, 2, 1) <> "0" Then sImporto = "-0" & Replace(sImporto, "-", "")
                End If

                iDifferenza = 55 - Len(sImporto)
                sDescrizione = par.IfNull(myReader("descrizione"), "")
                sCausale = sCausale & Mid(sDescrizione.PadRight(iDifferenza), 1, iDifferenza) & sImporto
            Loop
            If sCausale <> "" Then
                sCausale = sCausale & "Imposta di bollo assolta in modo virtuale              " & "Provvedimento n. 19870/73 del 15/11/1973               "
            End If
            CreaCausale = sCausale
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
            Button1.Visible = False
        End Try

    End Function
    Private Sub CreaDepCauz()

        Dim pp As New MavOnline.MAVOnlineBeanService
        Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
        Dim Esito As New MavOnline.rispostaMAVOnlineWS
        Dim binaryData() As Byte
        Dim outFile As System.IO.FileStream
        Dim outputFileName As String = ""
        Dim strCausale As String = ""
        Try
            Dim certificate As X509Certificate2 = basetester.Program.LoadCertificateFile(Server.MapPath("~/cert.pem"))
            pp.ClientCertificates.Add(certificate)

            If Session.Item("AmbienteDiTest") = "1" Then
                causalepagamento.Value = "commiquatest01"
                'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                pp.Url = Session.Item("indirizzoMavOnLine")
                RichiestaEmissioneMAV.codiceEnte = "commiqua"
            Else
                causalepagamento.Value = "commiquammre14ntsmolon"
                'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                pp.Url = Session.Item("indirizzoMavOnLine")
                RichiestaEmissioneMAV.codiceEnte = "commiqua"
            End If

            strCausale = CreaCausale()
            If Len(strCausale) > 1100 Then
                lblErrore.Visible = True
                lblErrore.Text = "Impossibile procedere! Il numero voci della bolletta supera il limite previsto per la stampa."
                Exit Try
            End If
            RichiestaEmissioneMAV.tipoPagamento = causalepagamento.Value
            RichiestaEmissioneMAV.idOperazione = lblNumeroMav.Text
            RichiestaEmissioneMAV.codiceDebitore = lblCodDebitore.Text

            RichiestaEmissioneMAV.causalePagamento = strCausale

            RichiestaEmissioneMAV.scadenzaPagamento = lblScadenza.Text
            RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(lblimporto.Text * 100)
            RichiestaEmissioneMAV.userName = lblCodDebitore.Text
            RichiestaEmissioneMAV.codiceFiscaleDebitore = lblCodFiscale.Text
            RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = lblCognome.Text
            If lblNome.Text <> "" Then
                RichiestaEmissioneMAV.nomeDebitore = lblNome.Text
            End If
            If Len(lblindirizzo.Text) <= 23 Then
                RichiestaEmissioneMAV.indirizzoDebitore = lblindirizzo.Text
            Else
                RichiestaEmissioneMAV.indirizzoDebitore = Mid(lblindirizzo.Text, 1, 23)
                RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(lblindirizzo.Text, 24, Len(lblindirizzo.Text)), 1, 28)
            End If

            RichiestaEmissioneMAV.capDebitore = lblCap.Text
            RichiestaEmissioneMAV.localitaDebitore = lblLocalita.Text
            RichiestaEmissioneMAV.provinciaDebitore = lblProvincia.Text
            RichiestaEmissioneMAV.nazioneDebitore = "IT"
            If DisabilitaExpect100Continue = "1" Then
                System.Net.ServicePointManager.Expect100Continue = False
            End If
            'APERTURA CONNESSIONE
            par.OracleConn.Open()
            par.SettaCommand(par)

            '/*/*/*/*/*tls v1
            Dim v As String = ""
            par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
            v = par.cmd.ExecuteScalar
            System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
            '/*/*/*/*/*tls v1



            System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler
            Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)
            If Esito.codiceRisultato = "0" Then
                lblErrore.Visible = False

                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & lblNumeroMav.Text & ".pdf"
                binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                outFile.Write(binaryData, 0, binaryData.Length - 1)
                outFile.Close()

                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.cmd.CommandText = "update siscom_mi.bol_bollette set FL_STAMPATO='1',rif_bollettino='" & Esito.numeroMAV & "' where id=" & CDbl(lblNumeroMav.Text)
                    par.cmd.ExecuteNonQuery()
                    

                    Response.Redirect("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & lblNumeroMav.Text & ".pdf")
                Catch ex As Exception
                    lblErrore.Visible = True
                    lblErrore.Text = ex.Message

                End Try
            Else
                FileDaCreare = lblNumeroMav.Text
                If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml") = True Then
                    FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                End If
                lblErrore.Visible = True
                lblErrore.Text = "Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!"


                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml"
                binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                outFile.Write(binaryData, 0, binaryData.Length)
                outFile.Close()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Shared Function CertificateHandler(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal SSLerror As SslPolicyErrors) As Boolean
        Return True
    End Function
    Private Sub Crea()

        Dim pp As New MavOnline.MAVOnlineBeanService
        Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
        Dim Esito As New MavOnline.rispostaMAVOnlineWS
        Dim binaryData() As Byte
        Dim outFile As System.IO.FileStream
        Dim outputFileName As String = ""
        Dim strCausale As String = ""
        Try

            'If Session.Item("AmbienteDiTest") = "1" Then
            '    causalepagamento.Value = "COMMITEST01"
            '    'pp.Url = "https://web1.unimaticaspa.it/pagamenti20-test-ws/services/MAVOnline"
            '    pp.Url = "https://demoweb.infogroup.it/pagamenti20-test-ws/services/MAVOnline"

            'End If
            If Session.Item("AmbienteDiTest") = "1" Then
                causalepagamento.Value = "COMMITEST01"
                'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                pp.Url = Session.Item("indirizzoMavOnLine")
            Else
                'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                pp.Url = Session.Item("indirizzoMavOnLine")
            End If
            strCausale = CreaCausale()
            If Len(strCausale) > 1100 Then
                lblErrore.Visible = True
                lblErrore.Text = "Impossibile procedere! Il numero voci della bolletta supera il limite previsto per la stampa."
                Exit Try
            End If
            RichiestaEmissioneMAV.codiceEnte = "commi"
            RichiestaEmissioneMAV.tipoPagamento = causalepagamento.Value
            RichiestaEmissioneMAV.idOperazione = lblNumeroMav.Text
            RichiestaEmissioneMAV.codiceDebitore = lblCodDebitore.Text

            RichiestaEmissioneMAV.causalePagamento = strCausale

            RichiestaEmissioneMAV.scadenzaPagamento = lblScadenza.Text
            RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(lblimporto.Text * 100)
            RichiestaEmissioneMAV.userName = lblCodDebitore.Text
            RichiestaEmissioneMAV.codiceFiscaleDebitore = lblCodFiscale.Text
            RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = lblCognome.Text
            If lblNome.Text <> "" Then
                RichiestaEmissioneMAV.nomeDebitore = lblNome.Text
            End If
            If Len(lblindirizzo.Text) <= 23 Then
                RichiestaEmissioneMAV.indirizzoDebitore = lblindirizzo.Text
            Else
                RichiestaEmissioneMAV.indirizzoDebitore = Mid(lblindirizzo.Text, 1, 23)
                RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(lblindirizzo.Text, 24, Len(lblindirizzo.Text)), 1, 28)
            End If

            RichiestaEmissioneMAV.capDebitore = lblCap.Text
            RichiestaEmissioneMAV.localitaDebitore = lblLocalita.Text
            RichiestaEmissioneMAV.provinciaDebitore = lblProvincia.Text
            RichiestaEmissioneMAV.nazioneDebitore = "IT"

            If DisabilitaExpect100Continue = "1" Then
                System.Net.ServicePointManager.Expect100Continue = False
            End If
            'apertura connessione
            par.OracleConn.Open()
            par.SettaCommand(par)

            '/*/*/*/*/*tls v1
            Dim v As String = ""
            par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
            v = par.cmd.ExecuteScalar
            System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
            '/*/*/*/*/*tls v1

            System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler
            Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)
            If Esito.codiceRisultato = "0" Then
                lblErrore.Visible = False

                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & lblNumeroMav.Text & ".pdf"
                binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                outFile.Write(binaryData, 0, binaryData.Length - 1)
                outFile.Close()

                Try
                    par.cmd.CommandText = "update siscom_mi.bol_bollette set FL_STAMPATO='1',rif_bollettino='" & Esito.numeroMAV & "' where id=" & CDbl(lblNumeroMav.Text)
                    par.cmd.ExecuteNonQuery()

                    Response.Redirect("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & lblNumeroMav.Text & ".pdf")
                Catch ex As Exception
                    lblErrore.Visible = True
                    lblErrore.Text = ex.Message

                End Try
            Else
                FileDaCreare = lblNumeroMav.Text
                If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml") = True Then
                    FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                End If
                lblErrore.Visible = True
                lblErrore.Text = "Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!"

                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml"
                binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                outFile.Write(binaryData, 0, binaryData.Length)
                outFile.Close()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Public Property DisabilitaExpect100Continue() As String
        Get
            If Not (ViewState("par_DisabilitaExpect100Continue") Is Nothing) Then
                Return CStr(ViewState("par_DisabilitaExpect100Continue"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DisabilitaExpect100Continue") = value
        End Set
    End Property


    Public Property FileDaCreare() As String
        Get
            If Not (ViewState("par_FileDaCreare") Is Nothing) Then
                Return CStr(ViewState("par_FileDaCreare"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_FileDaCreare") = value
        End Set
    End Property

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If tipoBolletta.Value = 9 Then
            CreaDepCauz()
        Else
            Crea()
        End If
    End Sub
End Class
