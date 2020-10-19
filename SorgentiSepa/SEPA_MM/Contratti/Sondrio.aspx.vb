Imports System.Security.Cryptography
Imports System.Convert
Imports System.IO


Partial Class Contratti_Sondrio
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Indirizzo As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)


                par.cmd.CommandText = "SELECT * from siscom_mi.bol_bollette WHERE fl_stampato='1' and FL_ANNULLATA='0' AND (rif_bollettino is NOT NULL) and  id=" & Request.QueryString("ID")
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX.HasRows = False Then

                    Dim sr1 As StreamReader = New StreamReader(Server.MapPath("AVVISO.xml"), System.Text.Encoding.UTF8)
                    Dim contenuto As String = sr1.ReadToEnd()
                    sr1.Close()

                    idtransazione.Value = Request.QueryString("ID")

                    Dim SIA As String = ""
                    Dim SERVIZIO As String = ""
                    Dim SOTTOSERVIZIO As String = ""
                    Dim LISTA As String = ""
                    Dim PROSEGUI As Boolean = True

                    par.cmd.CommandText = "SELECT * from parameter WHERE id=78"
                    Dim myReaderJ2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ2.Read Then
                        form1.Action = par.IfNull(myReaderJ2("valore"), "")
                        If par.IfNull(myReaderJ2("valore"), "") = "" Then
                            PROSEGUI = False
                        End If
                        'form1.Action = "SondrioEsito.aspx"
                    End If
                    myReaderJ2.Close()

                    par.cmd.CommandText = "SELECT * from parameter WHERE id=84"
                    myReaderJ2 = par.cmd.ExecuteReader()
                    If myReaderJ2.Read Then
                        url_ritorno.Value = par.IfNull(myReaderJ2("valore"), "") & "/Contratti/SondrioEsito.aspx"
                        If par.IfNull(myReaderJ2("valore"), "") = "" Then
                            PROSEGUI = False
                        End If
                    End If
                    myReaderJ2.Close()


                    par.cmd.CommandText = "SELECT * from parameter WHERE id=79"
                    myReaderJ2 = par.cmd.ExecuteReader()
                    If myReaderJ2.Read Then
                        SIA = par.IfNull(myReaderJ2("valore"), "")
                        If par.IfNull(myReaderJ2("valore"), "") = "" Then
                            PROSEGUI = False
                        End If
                    End If
                    myReaderJ2.Close()

                    par.cmd.CommandText = "SELECT * from parameter WHERE id=80"
                    myReaderJ2 = par.cmd.ExecuteReader()
                    If myReaderJ2.Read Then
                        SERVIZIO = par.IfNull(myReaderJ2("valore"), "")
                        If par.IfNull(myReaderJ2("valore"), "") = "" Then
                            PROSEGUI = False
                        End If
                    End If
                    myReaderJ2.Close()

                    par.cmd.CommandText = "SELECT * from parameter WHERE id=81"
                    myReaderJ2 = par.cmd.ExecuteReader()
                    If myReaderJ2.Read Then
                        SOTTOSERVIZIO = par.IfNull(myReaderJ2("valore"), "")
                        If par.IfNull(myReaderJ2("valore"), "") = "" Then
                            PROSEGUI = False
                        End If
                    End If
                    myReaderJ2.Close()

                    par.cmd.CommandText = "SELECT * from parameter WHERE id=82"
                    myReaderJ2 = par.cmd.ExecuteReader()
                    If myReaderJ2.Read Then
                        LISTA = par.IfNull(myReaderJ2("valore"), "")
                        If par.IfNull(myReaderJ2("valore"), "") = "" Then
                            PROSEGUI = False
                        End If
                    End If
                    myReaderJ2.Close()


                    par.cmd.CommandText = "SELECT * from parameter WHERE id=83"
                    myReaderJ2 = par.cmd.ExecuteReader()
                    If myReaderJ2.Read Then
                        cassa.Value = par.IfNull(myReaderJ2("valore"), "")
                        If par.IfNull(myReaderJ2("valore"), "") = "" Then
                            PROSEGUI = False
                        End If
                    End If
                    myReaderJ2.Close()

                    If PROSEGUI = True Then
                        par.cmd.CommandText = "SELECT ANAGRAFICA.PARTITA_IVA,ANAGRAFICA.COD_FISCALE,BOL_BOLLETTE.* from SISCOM_MI.ANAGRAFICA,siscom_mi.bol_bollette WHERE ANAGRAFICA.ID=BOL_BOLLETTE.COD_AFFITTUARIO AND BOL_BOLLETTE.ID=" & idtransazione.Value
                        Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then
                            If par.IfNull(myReaderJ("RIF_FILE"), "") <> "MAV" Then
                                Response.Write("<script>alert('Attenzione, questa bolletta non può essere pagata tramite Mav on Line Banca di Sondrio!');self.close();</script>")

                            Else
                                If par.IfNull(myReaderJ("FL_ANNULLATA"), "") = "1" Or par.IfNull(myReaderJ("DATA_INS_PAGAMENTO"), "") <> "" Then
                                    Response.Write("<script>alert('Attenzione, questa bolletta è stata Annullata o già pagata!');self.close();</script>")
                                Else
                                    contenuto = Replace(contenuto, "$sia$", SIA)
                                    contenuto = Replace(contenuto, "$servizio$", SERVIZIO)
                                    contenuto = Replace(contenuto, "$sottoservizio$", SOTTOSERVIZIO)
                                    contenuto = Replace(contenuto, "$lista$", LISTA)


                                    Label1.Text = par.IfNull(myReaderJ("COD_FISCALE"), "")
                                    If par.IfNull(myReaderJ("COD_FISCALE"), "") = "" Then
                                        Label1.Text = par.IfNull(myReaderJ("PARTITA_IVA"), "")
                                        contenuto = Replace(contenuto, "$cfdebitore$", par.IfNull(myReaderJ("PARTITA_IVA"), ""))
                                    Else
                                        contenuto = Replace(contenuto, "$cfdebitore$", par.IfNull(myReaderJ("COD_FISCALE"), ""))
                                    End If

                                    contenuto = Replace(contenuto, "$codicedebitore$", par.IfNull(myReaderJ("COD_AFFITTUARIO"), ""))
                                    contenuto = Replace(contenuto, "$debitore$", Replace(Mid(par.IfNull(myReaderJ("INTESTATARIO"), ""), 1, 30), "'", ""))
                                    Label2.Text = par.IfNull(myReaderJ("INTESTATARIO"), "")
                                    contenuto = Replace(contenuto, "$presso$", Replace(par.IfNull(myReaderJ("PRESSO"), " "), "'", ""))
                                    Label3.Text = par.IfNull(myReaderJ("PRESSO"), " ")
                                    contenuto = Replace(contenuto, "$indirizzo$", Mid(Replace(par.IfNull(myReaderJ("INDIRIZZO"), ""), "'", ""), 1, 30))
                                    Label4.Text = par.IfNull(myReaderJ("INDIRIZZO"), "")
                                    If Len(Label4.Text) > 30 Then
                                        Label4.Text = Mid(Label4.Text, 1, 30)
                                    End If
                                    contenuto = Replace(contenuto, "$cap$", Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), 1, 5))
                                    Label5.Text = Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), 1, 5)
                                    Dim AA As String = ""
                                    AA = Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), 7, InStr(par.IfNull(myReaderJ("CAP_CITTA"), ""), "(") - 7)
                                    contenuto = Replace(contenuto, "$citta$", Replace(Trim(AA), "'", ""))
                                    Label6.Text = Trim(AA)
                                    contenuto = Replace(contenuto, "$provincia$", Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), InStr(par.IfNull(myReaderJ("CAP_CITTA"), ""), "(") + 1, 2))
                                    Label7.Text = Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), InStr(par.IfNull(myReaderJ("CAP_CITTA"), ""), "(") + 1, 2)
                                    contenuto = Replace(contenuto, "$mail$", " ")
                                    Label8.Text = " "

                                    contenuto = Replace(contenuto, "$scadenza$", Format(CDate(par.FormattaData(par.IfNull(myReaderJ("DATA_SCADENZA"), ""))), "yyyy-MM-dd"))
                                    Label10.Text = Format(CDate(par.FormattaData(par.IfNull(myReaderJ("DATA_SCADENZA"), ""))), "yyyy-MM-dd")
                                    contenuto = Replace(contenuto, "$identificativo$", idtransazione.Value)
                                    contenuto = Replace(contenuto, "$causale$", Replace(par.IfNull(myReaderJ("NOTE"), " "), "'", ""))
                                    Label11.Text = par.IfNull(myReaderJ("NOTE"), "")

                                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS ""IMPORTO"" FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & idtransazione.Value
                                    Dim myReaderJ1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderJ1.Read Then
                                        contenuto = Replace(contenuto, "$importo$", par.VirgoleInPunti(Format(par.IfNull(myReaderJ1("IMPORTO"), "0.00"), "0.00")))
                                        Label9.Text = Format(par.IfNull(myReaderJ1("IMPORTO"), "0.00"), "0.00")
                                    End If
                                    myReaderJ1.Close()

                                    If Label9.Text <= 0 Then
                                        Response.Write("<script>alert('Attenzione, questa bolletta non può essere pagata perchè di importo negativo!');self.close();</script>")
                                    End If

                                    xml.Value = Encode(contenuto)
                                    mac.Value = HashMD5("MAVONLINE" & idtransazione.Value & xml.Value)
                                End If
                            End If
                        Else
                            Response.Write("<script>alert('Attenzione, probabile errore nel recupero dei dati della bolletta! Segnalare il problema all\'amministratore del sistema.');self.close();</script>")
                        End If
                        myReaderJ.Close()
                    Else

                        Response.Write("<script>alert('Attenzione, i parametri per il collegamento alla BPS non sono stati specificati. Non è possibile procedere!');self.close();</script>")


                    End If
                Else
                    If myReaderX.Read Then
                        Dim INDIRIZZO As String = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & myReaderX("RIF_BOLLETTINO") & ".pdf"
                        Dim fff As String = myReaderX("RIF_BOLLETTINO") & ".pdf"

                        myReaderX.Close()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        If IO.File.Exists(INDIRIZZO) = True Then
                            Response.Redirect("../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & fff)
                        Else
                            Response.Redirect("..\PageNotFound.htm")
                        End If
                    End If
                    End If
                    myReaderX.Close()


                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Response.Write(ex.Message)
            End Try
        End If




        'Response.Write(mac.Value)
        'Response.Write("----")
        'Response.Write("MAVONLINE" & idtransazione.Value & xml.Value)
    End Sub

    Function Encode(ByVal dec As String) As String

        Dim bt() As Byte
        ReDim bt(dec.Length)

        bt = System.Text.Encoding.ASCII.GetBytes(dec)


        Dim enc As String
        enc = System.Convert.ToBase64String(bt)

        Return enc
    End Function

    Function HashMD5(ByVal password As String) As String
        Dim hash As String = ""
        Dim bb As Byte() = System.Text.Encoding.Default.GetBytes(password)
        Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim enc As Byte() = md5.TransformFinalBlock(bb, 0, bb.Length)
        Dim b As Byte
        For Each b In md5.Hash
            hash += Convert.ToString(b, 16).PadLeft(2, "0")
            md5.Clear()
        Next
        HashMD5 = hash
    End Function

    


End Class
