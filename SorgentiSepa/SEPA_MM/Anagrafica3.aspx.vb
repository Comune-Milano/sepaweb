Imports System.ServiceModel.Channels
Imports System.ServiceModel
Imports System.Net
Imports System.IO


Partial Class Anagrafica3
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TestoFamiglia As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim TestoErrore As String = ""
        Dim SIPO_APPLICAZIONE As String = ""
        Dim SIPO_OPERATORE As String = ""
        Dim SIPO_PWOPERATORE As String = ""
        Dim SIPO_TOKEN As String = ""
        Dim i As Integer = 1
        Dim NUMERO As String = ""
        Dim DATARILASCIO As String = ""
        Dim DATASCADENZA As String = ""
        Dim DATAPROROGA As String = ""
        Dim COMUNE As String = ""


        If Request.QueryString("ID") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim ValorePassato As String = par.DeCriptaMolto(Request.QueryString("ID"))
        If Mid(ValorePassato, 1, 10) <> Format(Now, "yyyyMMddHH") Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                lblData.Text = Format(Now, "dd/MM/yyyy")
                lblOperatore.Text = Session.Item("operatore")

                Dim sStringaSql As String

                Dim s As String = ""
                Dim Indice As String

                Indice = Mid(ValorePassato, InStr(ValorePassato, "#") + 1, 15)
                Dim SessioneLavoro As String = Trim(Mid(ValorePassato, InStr(ValorePassato, "@") + 1, 100))

                par.cmd.CommandText = "SELECT * FROM AU_LIGHT_CONCESSIONI WHERE CODICE='" & SessioneLavoro & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = False Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Redirect("~/AccessoNegato.htm", True)
                    Exit Sub
                Else
                    par.cmd.CommandText = "DELETE FROM AU_LIGHT_CONCESSIONI WHERE CODICE='" & SessioneLavoro & "'"
                    par.cmd.ExecuteNonQuery()
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=53"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    SIPO_APPLICAZIONE = par.IfNull(myReader1("VALORE"), "")
                End If
                myReader1.Close()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=54"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    SIPO_OPERATORE = par.IfNull(myReader1("VALORE"), "")
                End If
                myReader1.Close()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=55"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    SIPO_PWOPERATORE = par.IfNull(myReader1("VALORE"), "")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=56"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    SIPO_TOKEN = par.IfNull(myReader1("VALORE"), "")
                End If
                myReader1.Close()

                chiamante.Value = Request.QueryString("C")
                Dim CFdaCercare As String = par.DeCripta(UCase(Request.QueryString("CF")))
                lblCF.Text = "CODICE FISCALE:" & CFdaCercare
                HCfDaCercare.Value = CFdaCercare

                Dim obj As New Anagrafe11.XMLWSAnagrafe2009SoapClient
                Dim risultato As Anagrafe11.getRicercaIndividuiXMLResult
                Dim httpRequestProperty As New HttpRequestMessageProperty

                Dim risultatoF As Anagrafe11.getFamigliaByMatricolaXMLResult
                Dim RisultatoDoc As Anagrafe11.getCarteIdentitaXMLResult
                Dim RisultatoPU As Anagrafe11.getDatiPuntualiXMLResult
                Dim CF As String = ""

                httpRequestProperty.Method = "POST"
                httpRequestProperty.Headers.Add("Authorization", "Bearer " & SIPO_TOKEN)
                Using scope As New OperationContextScope(obj.InnerChannel)
                    OperationContext.Current.OutgoingMessageProperties(HttpRequestMessageProperty.Name) = httpRequestProperty

                    Try
                        risultato = obj.getRicercaIndividuiXML(SIPO_APPLICAZIONE, SIPO_OPERATORE, SIPO_PWOPERATORE, Session.Item("ID_OPERATORE"), "", "", "", "", "", "", 0, "", CFdaCercare, 0, "", "N", "T")
                    Catch ex As CommunicationException
                        lblErrore.Visible = True
                        lblErrore.Text = "Elemento non trovato"
                        ImageButton1.Visible = False
                    End Try

                    If lblErrore.Visible = False Then
                        lblI1.Text = risultato.Item(0).Matricola
                        lblI2.Text = risultato.Item(0).NumeroFamiglia
                        lblI3.Text = risultato.Item(0).Cognome
                        lblI4.Text = risultato.Item(0).Nome
                        lblI5.Text = risultato.Item(0).Sesso
                        lblI6.Text = par.FormattaData(risultato.Item(0).DataNascita)
                        lblI7.Text = risultato.Item(0).Indirizzo
                        lblI8.Text = risultato.Item(0).StatoCivile
                        lblI9.Text = risultato.Item(0).RapportoParentela
                        lblI11.Text = risultato.Item(0).CivicoIntero
                        lblI12.Text = risultato.Item(0).CivicoBarrato

                        Try
                            risultatoF = obj.getFamigliaByMatricolaXML(SIPO_APPLICAZIONE, SIPO_OPERATORE, SIPO_PWOPERATORE, Session.Item("ID_OPERATORE"), lblI1.Text)
                            For i = 0 To risultatoF.Count - 1
                                Try
                                    RisultatoDoc = obj.getCarteIdentitaXML(SIPO_APPLICAZIONE, SIPO_OPERATORE, SIPO_PWOPERATORE, Session.Item("ID_OPERATORE"), lblI1.Text)
                                    NUMERO = RisultatoDoc.Item(0).Numero
                                    DATARILASCIO = par.FormattaData(Trim(RisultatoDoc.Item(0).DataRilascio))
                                    DATASCADENZA = par.FormattaData(Trim(RisultatoDoc.Item(0).DataScadenza))
                                    DATAPROROGA = par.FormattaData(Trim(RisultatoDoc.Item(0).DataProroga))
                                    COMUNE = RisultatoDoc.Item(0).Comune
                                Catch ex As Exception
                                    NUMERO = "*"
                                    DATARILASCIO = "*"
                                    DATASCADENZA = "*"
                                    DATAPROROGA = "*"
                                    COMUNE = "*"
                                End Try

                                Try
                                    RisultatoPU = obj.getDatiPuntualiXML(SIPO_APPLICAZIONE, SIPO_OPERATORE, SIPO_PWOPERATORE, Session.Item("ID_OPERATORE"), lblI1.Text)
                                    CF = RisultatoPU.Item(0).CodiceFiscale
                                Catch ex As Exception
                                    CF = "*"
                                End Try

                                TestoFamiglia = TestoFamiglia & "<tr style='font-family: ARIAL, Helvetica, sans-serif; font-size: 9pt;'><td>" & risultatoF.Item(i).Matricola & "</td><td>" & risultatoF.Item(i).NumeroFamiglia & "</td><td>" & risultatoF.Item(i).Cognome & "</td><td>" & risultatoF.Item(i).Nome & "</td><td>" & CF & "</td><td>" & risultatoF.Item(i).Sesso & "</td><td>" & par.FormattaData(risultatoF.Item(i).DataNascita) & "</td><td>" & risultatoF.Item(i).StatoCivile & "</td><td>" & risultatoF.Item(i).RapportoParentela & "</td><td>" & risultatoF.Item(i).Dicitura & "</td><td>" & NUMERO & "</td><td>" & DATARILASCIO & "</td><td>" & DATASCADENZA & "</td><td>" & DATAPROROGA & "</td><td>" & COMUNE & "</td></tr>"
                            Next
                        Catch ex As Exception
                            TestoFamiglia = TestoFamiglia & "<tr style='font-family: ARIAL, Helvetica, sans-serif; font-size: 9pt;'><td>Nessun dato da visualizzare</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                        End Try

                    End If

                End Using


                Select Case chiamante.Value
                    Case "1" 'bando erp
                        sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F166',' " & par.DeCripta(Request.QueryString("CF")) & "','I')"
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    Case "2" 'au
                        sStringaSql = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F166',' " & par.DeCripta(Request.QueryString("CF")) & "','I')"
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    Case "3" 'cambi
                        sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI_CAMBI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F166',' " & par.DeCripta(Request.QueryString("CF")) & "','I')"
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    Case "4" 'FSA
                        sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI_FSA (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F166',' " & par.DeCripta(Request.QueryString("CF")) & "','I')"
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    Case "5" 'VSA
                        sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI_VSA (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F166',' " & par.DeCripta(Request.QueryString("CF")) & "','I')"
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    Case "6"
                        sStringaSql = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F166',' " & par.DeCripta(Request.QueryString("CF")) & "')"
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    Case "7"
                        If CLng(Indice) <> "0" Then
                            sStringaSql = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & Indice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                    & "'F166','" & par.DeCripta(Request.QueryString("CF")) & "')"
                            par.cmd.CommandText = sStringaSql
                            par.cmd.ExecuteNonQuery()
                        Else
                            Session.Item("sipo") = par.DeCripta(Request.QueryString("CF")) & ";" & Session.Item("sipo")
                            'Session.Add("sipo", par.DeCripta(Request.QueryString("CF")))
                        End If
                End Select
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                If InStr(ex.Message, "Il server ha restituito un errore SOAP non valido") > 0 Then
                    lblErrore.Text = ex.InnerException.ToString
                Else
                    lblErrore.Text = ex.Message
                End If

            End Try
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim Chiave As String = par.getPageId & "_" & Format(Now, "yyyyMMddHHmmss")

            par.cmd.CommandText = "INSERT INTO AU_LIGHT_CONCESSIONI VALUES ('" & Chiave & "')"
            par.cmd.ExecuteNonQuery()

            'lblIDatiCompleti.Attributes.Add("onclick", "javascript:window.open('Anagrafe4.aspx?ID=" & par.CriptaMolto(Format(Now, "yyyyMMddHH") & "#" & Format(CLng(lblI1.Text), "000000000000000") & "@" & Chiave) & "&CF=" & par.CriptaMolto(CFdaCercare) & "','Anagrafe4','top=0,left=0,width=1000,height=600');")
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "window.open('Anagrafe4.aspx?ID=" & par.CriptaMolto(Format(Now, "yyyyMMddHH") & "#" & Format(CLng(lblI1.Text), "000000000000000") & "@" & Chiave) & "&CF=" & par.CriptaMolto(HCfDaCercare.Value) & "','Anagrafe4','top=0,left=0,width=1000,height=600');", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
