Imports System.ServiceModel.Channels
Imports System.ServiceModel
Imports System.Net
Imports System.IO

Partial Class Anagrafe4
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TestoFamiglia As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim SIPO_APPLICAZIONE As String = ""
        Dim SIPO_OPERATORE As String = ""
        Dim SIPO_PWOPERATORE As String = ""
        Dim SIPO_TOKEN As String = ""
        Dim i As Integer = 1

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

                Dim CFdaCercare As String = par.DeCriptaMolto(UCase(Request.QueryString("CF")))
                lblCF.Text = "CODICE FISCALE:" & CFdaCercare

                lblData.Text = Format(Now, "dd/MM/yyyy")
                lblOperatore.Text = Session.Item("operatore")

                Dim obj As New Anagrafe11.XMLWSAnagrafe2009SoapClient
                Dim risultato As Anagrafe11.getDatiPuntualiXMLResult
                Dim httpRequestProperty As New HttpRequestMessageProperty

                httpRequestProperty.Method = "POST"
                httpRequestProperty.Headers.Add("Authorization", "Bearer " & SIPO_TOKEN)
                Using scope As New OperationContextScope(obj.InnerChannel)
                    OperationContext.Current.OutgoingMessageProperties(HttpRequestMessageProperty.Name) = httpRequestProperty

                    risultato = obj.getDatiPuntualiXML(SIPO_APPLICAZIONE, SIPO_OPERATORE, SIPO_PWOPERATORE, Session.Item("ID_OPERATORE"), Indice)

                    lblDati1.Text = risultato.Item(0).Matricola
                    lblDati2.Text = risultato.Item(0).NumeroFamiglia
                    lblDati3.Text = risultato.Item(0).Cognome
                    lblDati4.Text = risultato.Item(0).Nome
                    lblDati5.Text = risultato.Item(0).Sesso
                    lblDati6.Text = par.FormattaData(risultato.Item(0).DataNascita)
                    lblDati7.Text = risultato.Item(0).Paternita
                    lblDati8.Text = risultato.Item(0).Maternita

                    lblDati9.Text = risultato.Item(0).ComuneNascita
                    lblDati10.Text = risultato.Item(0).ProvinciaNascita
                    lblDati11.Text = risultato.Item(0).NazioneNascita
                    lblDati12.Text = risultato.Item(0).CodiceFiscale

                    Select Case risultato.Item(0).FlagValiditaCFIS
                        Case "V"
                            lblDati13.Text = "Validato"
                        Case "C"
                            lblDati13.Text = "Calcolato"
                        Case Else
                            lblDati13.Text = ""
                    End Select


                    lblDati14.Text = risultato.Item(0).Indirizzo
                    lblDati15.Text = risultato.Item(0).StatoCivile
                    lblDati16.Text = risultato.Item(0).RapportoParentela
                    lblDati17.Text = risultato.Item(0).Cittadinanza
                    Select Case risultato.Item(0).DataCittadinanza
                        Case "00000000", "0       "
                            lblDati39.Text = "Mai cambiata"
                        Case Else
                            lblDati39.Text = par.FormattaData(risultato.Item(0).DataCittadinanza)
                    End Select

                    lblDati18.Text = par.FormattaData(Replace(risultato.Item(0).DataMatrimonio, "*", ""))
                    lblDati19.Text = par.FormattaData(Replace(risultato.Item(0).DataCessazioneMatrimonio, "*", ""))
                    lblDati20.Text = risultato.Item(0).ComuneMatrimonio
                    lblDati21.Text = risultato.Item(0).ProvinciaMatrimonio
                    lblDati22.Text = risultato.Item(0).Coniuge
                    Select Case risultato.Item(0).DataEmigrazione
                        Case "00000000", "0       "
                            lblDati23.Text = ""
                        Case Else
                            lblDati23.Text = par.FormattaData(risultato.Item(0).DataEmigrazione)
                    End Select

                    lblDati24.Text = risultato.Item(0).ComuneEmigrazione
                    lblDati25.Text = risultato.Item(0).ProvinciaEmigrazione
                    Select Case Trim(risultato.Item(0).DataDecesso)
                        Case "0"
                            lblDati26.Text = ""
                        Case Else
                            lblDati26.Text = par.FormattaData(risultato.Item(0).DataDecesso)
                    End Select

                    lblDati27.Text = risultato.Item(0).ComuneDecesso
                    lblDati28.Text = risultato.Item(0).ProvinciaDecesso
                    Select Case risultato.Item(0).Ecopass
                        Case "N"
                            lblDati29.Text = "non appartiene alla cerchia Ecopass"
                        Case "S"
                            lblDati29.Text = "appartiene alla cerchia Ecopass"
                        Case Else
                            lblDati29.Text = ""
                    End Select
                    Select Case risultato.Item(0).INASAIA
                        Case "N"
                            lblDati40.Text = "non trasmesso INA/SAIA"
                        Case "S"
                            lblDati40.Text = "trasmesso INA/SAIA"
                        Case Else
                            lblDati40.Text = ""
                    End Select


                    lblDati30.Text = par.FormattaData(Replace(Trim(risultato.Item(0).DataIngressoFamiglia), "*", ""))
                    lblDati31.Text = risultato.Item(0).Cap
                    lblDati32.Text = risultato.Item(0).StatusAnagrafico
                    'lblDati33.Text = risultato.Item(0).FlagStatus1
                    'lblDati34.Text = risultato.Item(0).FlagStatus2
                    lblDati35.Text = risultato.Item(0).CodiceVia
                    lblDati36.Text = risultato.Item(0).CivicoIntero
                    lblDati37.Text = risultato.Item(0).CivicoBarrato
                    Select Case risultato.Item(0).CodiceStatusAnagrafico
                        Case "A"
                            lblDati38.Text = "RESIDENTE"
                        Case "E"
                            lblDati38.Text = "EMIGRATO"
                        Case "D"
                            lblDati38.Text = "DECEDUTO"
                        Case "I"
                            lblDati38.Text = "IRREPERIBILE"
                        Case "R"
                            lblDati38.Text = "ISCRITTO A.I.R.E."
                    End Select
                End Using
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = False
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If

    End Sub
End Class
