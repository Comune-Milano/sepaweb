
Partial Class CAMBI_BandoCambi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim gsVettTitoli(15) As String
    Dim gsVettcommenti(15) As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaBando()
        End If
    End Sub

    Private Function CaricaBando()

        Dim TIPOLOGIA_BANDO As String = ""
        Dim DESCRIZIONE As String = ""
        Dim DATA_INIZIO As String = ""
        Dim DATA_FINE As String = ""
        Dim TIPO_BANDO As String = ""
        Dim ANNO_ISEE As String = ""
        Dim PROVVEDIMENTO As String = ""
        Dim ALLOGGI_S As String = ""
        Dim ALLOGGI_M As String = ""
        Dim Q_ANZIANI As String = ""
        Dim Q_FAMIGLIE As String = ""
        Dim Q_SOLI As String = ""
        Dim Q_PROFUGHI As String = ""
        Dim Q_DISABILI As String = ""
        Dim TASSO As String = ""
        Dim ANNO_SPESE_CANONE As String = ""
        Dim Q_VARI As String = ""
        Dim TITOLO As String = ""
        Dim I As Integer

        Dim INDICE_BANDO As Long

        Try



            If par.OracleConn.State = Data.ConnectionState.Open Then
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘‘par.cmd.Transaction = par.myTrans
                Response.Write("IMPOSSIBILE VISUALIZZARE I PARAMETRI DI BANDO")
                Exit Function
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from bandi_cambio where stato=1"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.Read() Then

                INDICE_BANDO = par.IfNull(myReader("ID"), -2)

                Select Case Val(par.IfNull(myReader("tipo_bando"), "-1"))
                    Case 0
                        TIPOLOGIA_BANDO = "I° SEMESTRE"
                    Case 1
                        TIPOLOGIA_BANDO = "II° SEMESTRE"
                    Case 2
                        TIPOLOGIA_BANDO = "ANNUALE"
                    Case Else
                        TIPOLOGIA_BANDO = ""
                End Select

                DESCRIZIONE = par.IfNull(myReader("descrizione"), "")
                DATA_INIZIO = par.FormattaData(par.IfNull(myReader("data_inizio"), ""))
                DATA_FINE = par.FormattaData(par.IfNull(myReader("data_FINE"), ""))
                TIPO_BANDO = TIPOLOGIA_BANDO
                ANNO_ISEE = par.IfNull(myReader("anno_isee"), "")
                PROVVEDIMENTO = par.IfNull(myReader("provvedimento"), "")
                ALLOGGI_S = par.IfNull(myReader("all_sociale"), "0")
                ALLOGGI_M = par.IfNull(myReader("all_moderato"), "0")
                Q_ANZIANI = par.IfNull(myReader("quota_anziani"), "0")
                Q_FAMIGLIE = par.IfNull(myReader("quota_famiglie"), "0")
                Q_SOLI = par.IfNull(myReader("quota_soli"), "0")
                Q_PROFUGHI = par.IfNull(myReader("quota_profughi"), "0")
                Q_DISABILI = par.IfNull(myReader("quota_DISABILI"), "0")
                TASSO = par.IfNull(myReader("TASSO_RENDIMENTO"), "0")
                ANNO_SPESE_CANONE = par.IfNull(myReader("ANNO_CANONE"), "0")
                Q_VARI = par.IfNull(myReader("QUOTA_VARI"), "0")
            End If
            myReader.Close()

            TITOLO = ""

            gsVettTitoli(0) = "01) ANZIANI"
            gsVettTitoli(1) = "02) DISABILI"
            gsVettTitoli(2) = "03) FAMIGLIA DI NUOVA FORMAZIONE"
            gsVettTitoli(3) = "04) PERSONE SOLE, CON EVENTUALI MINORI A CARICO"
            gsVettTitoli(4) = "05) STATO DI DISOCCUPAZIONE"
            gsVettTitoli(5) = "06) RICONGIUNZIONE"
            gsVettTitoli(6) = "07) CASI PARTICOLARI"
            gsVettTitoli(7) = "08) RILASCIO ALLOGGIO"
            gsVettTitoli(8) = "09) CONDIZIONE ABITATIVA IMPROPRIA"
            gsVettTitoli(9) = "10) COABITAZIONE"
            gsVettTitoli(10) = "11) SOVRAFFOLAMENTO"
            gsVettTitoli(11) = "12) CONDIZIONE DELL'ALLOGGIO"
            gsVettTitoli(12) = "13) BARRIERE ARCHITETTONICHE"
            gsVettTitoli(13) = "14) CONDIZIONE DI ACCESSIBILITA'"
            gsVettTitoli(14) = "15) LONTANANZA DALLA SEDE DI LAVORO"
            gsVettTitoli(15) = "16) AFFITTO ONEROSO"

            gsVettcommenti(0) = "Nuclei familiari di non più di due componenti o persone singole"
            gsVettcommenti(1) = "Nuclei familiari con componenti, anche anagraficamente non conviventi, ma presenti nella domanda, siano affetti da minorazioni o malattie invalidanti che comportino un handicap grave"
            gsVettcommenti(2) = "Nuclei familiari da costituirsi prima della consegna dell'alloggio, ovvero costituitisi entro i due anni precedenti alla data della domanda"
            gsVettcommenti(3) = "Nuclei di un componente, con un eventuale minore o più a carico"
            gsVettcommenti(4) = "Stato di disoccupazione determinato da una caduta del reddito complessivo del nucleo familiare superiore al 50%"
            gsVettcommenti(5) = "Nucleo familiare che necessiti di alloggio idoneo per accogliervi parente disabile"
            gsVettcommenti(6) = ""
            gsVettcommenti(7) = "Concorrenti che debbano rilasciare l'alloggio a seguito di ordinanza"
            gsVettcommenti(8) = ""
            gsVettcommenti(9) = "Richiedenti che abitino da almeno tre anni con il proprio nucleo familiare in uno stesso alloggio con altro o più nuclei familiari"
            gsVettcommenti(10) = "Richiedenti che abitino da almeno tre anni con il proprio nucleo familiare"
            gsVettcommenti(11) = "Richiedenti che abitino da almeno tre anni con il proprio nucleo familiare"
            gsVettcommenti(12) = "Richiedenti, di cui al precedente punto 2) che abitino con il proprio nucleo familiare in alloggio che non consenta una normale condizione abitativa"
            gsVettcommenti(13) = "Richiedenti, di cui ai precedenti punti 1) e 2), che abitino con il proprio nucleo familiare in alloggio che non è servito da ascensore ed è situato superiormente al primo piano"
            gsVettcommenti(14) = ""
            gsVettcommenti(15) = ""

            par.cmd.CommandText = "select * from bandi_cambio where stato=1"
            myReader = par.cmd.ExecuteReader()
            myReader.Close()

            For I = 0 To 15
                TITOLO = TITOLO & "<tr>" _
                       & "<td width=70% colspan=3><BR><b>" & gsVettTitoli(I) & "</b>" _
                       & "<BR>" & gsVettcommenti(I) & "</td></tr><tr><td width=70% align=center bgcolor=#cccccc><b>Parametro</b></td>" _
                       & "<td width=15% align=center bgcolor=#cccccc><b>Regionale</b></td>" _
                       & " <td width=15% align=center bgcolor=#cccccc><b>Comunale</b></td></tr>"

                par.cmd.CommandText = "SELECT * FROM PARAMETRI_BANDO_cambi WHERE TIPO_PARAMETRO=" & I & " AND ID_BANDO=" & INDICE_BANDO & " ORDER BY ID ASC"
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    TITOLO = TITOLO & "<tr><td width=70%><font face=Arial size=2>" & par.IfNull(myReader("DESCRIZIONE"), "") & "</td>" _
                                    & "<td width=15% align=center><font face=Arial size=2>" & par.IfNull(myReader("REGIONALE"), "0") & "</td>" _
                                    & "<td width=15% align=center><font face=Arial size=2>" & par.IfNull(myReader("COMUNALE"), "0") _
                                    & "</td></tr>"

                    '        myrec1.MoveNext()
                End While
                myReader.Close()


            Next I



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            'POS = InStr(1, TESTO_STAMPA, "***DESCRIZIONE_PARAMETRO***")
            'TESTO_STAMPA = Mid(TESTO_STAMPA, 1, POS - 1) & TITOLO & Mid(TESTO_STAMPA, POS + 27, Len(TESTO_STAMPA))

            Response.Write("<html xmlns='http://www.w3.org/1999/xhtml'>")
            Response.Write("<head>")
            Response.Write("")
            Response.Write("<title>STAMPA BANDO CAMBI LOMBARDIA</title>")
            Response.Write("")
            Response.Write("")
            Response.Write("</head>")
            Response.Write("<BODY >")
            Response.Write("")
            Response.Write("")
            Response.Write("")
            Response.Write("")
            Response.Write("")
            Response.Write("")
            Response.Write("")
            Response.Write("<script type ='text/javascript'>")
            Response.Write("//-------------- disabilitazione tasto destro del mouse")
            Response.Write("var message='Funzione non attiva';")
            Response.Write("function click(e) {")
            Response.Write("if (document.all) {")
            Response.Write("if (event.button==2||event.button==3) {")
            Response.Write("alert(message);")
            Response.Write("return false;")
            Response.Write("}")
            Response.Write("}")
            Response.Write("if (document.layers) {")
            Response.Write("if (e.which == 3) {")
            Response.Write("alert(message);")
            Response.Write("return false;")
            Response.Write("}")
            Response.Write("}")
            Response.Write("}")
            Response.Write("")
            Response.Write("if (document.layers) {")
            Response.Write("document.captureEvents(Event.MOUSEDOWN);")
            Response.Write("}")
            Response.Write("")
            Response.Write("document.onmousedown=click;")
            Response.Write("")
            Response.Write("//-------------- funzione che serve per stampare")
            Response.Write("var da = (document.all) ? 1 : 0;")
            Response.Write("var pr = (window.print) ? 1 : 0;")
            Response.Write("var mac = (navigator.userAgent.indexOf('Mac') != -1);")
            Response.Write("function printDialog() {")
            Response.Write("if (pr) // NS4")
            Response.Write("IE5")
            Response.Write("//dancy parent.frames[2].print()")
            Response.Write("print()")
            Response.Write("else if (da && !mac) // IE4 (Windows)")
            Response.Write("vbPrintPage()")
            Response.Write("else // other browsers")
            Response.Write("alert('Sorry")
            Response.Write("your browser doesn't support this feature.');")
            Response.Write("return false;")
            Response.Write("}")
            Response.Write("</script>")
            Response.Write("<table width='100%' cellspacing='0' cellpadding='2' border='1' bordercolor='black' bgcolor='#cccccc'>")
            Response.Write("<tr>")
            Response.Write("<td width='50%' colspan='2' class='titolo'>")
            Response.Write("<p align='center'><b><font color='#FF0000' size='4'>COMUNE DI MILANO<BR>DATI RIASSUNTIVI ")
            Response.Write(DESCRIZIONE & "</font></b></p>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("</table>")
            Response.Write("<BR>")
            Response.Write("")
            Response.Write("<table width='100%' cellspacing='0' cellpadding='2' border='1' bordercolor='black'>")
            Response.Write("<tr>")
            Response.Write("<td width='50%' colspan='2'  bgcolor='#cccccc'>")
            Response.Write("<p align='center'><b>DATI BANDO CAMBI</b></p>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Data inizio</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & DATA_INIZIO & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Data fine</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & DATA_FINE & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Tipo bando</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & TIPO_BANDO & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Anno di riferimento dell'Isee</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & ANNO_ISEE & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Anno di riferimento del canone e")
            Response.Write("delle spese</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & ANNO_SPESE_CANONE & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Tasso di rendimento medio annuo</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & TASSO & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Provvedimento</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & PROVVEDIMENTO & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Alloggi previsti (canone sociale)</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & ALLOGGI_S & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Alloggi previsti (canone moderato)</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & ALLOGGI_M & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Quota anziani</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & Q_ANZIANI & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Quota nuove famiglie</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & Q_FAMIGLIE & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Quota soli</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & Q_SOLI & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Quota disabili</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & Q_DISABILI & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Quota Vari</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & Q_VARI & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("<tr>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>Quota profughi</font>")
            Response.Write("</td>")
            Response.Write("<td width='50%'><font face='Arial' size='2'>" & Q_PROFUGHI & "</font>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("</table>")
            Response.Write("")
            Response.Write("<BR>")
            Response.Write("<table width='100%' cellspacing='0' cellpadding='2' border='1' bordercolor='black'>")
            Response.Write("<tr>")
            Response.Write("<td width='50%' colspan='3'  bgcolor='#cccccc'>")
            Response.Write("<p align='center'><b>PARAMETRI BANDO</b></p>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("")
            Response.Write("")
            Response.Write(TITOLO)
            Response.Write("")
            Response.Write("</table>")
            Response.Write("")
            Response.Write("")
            Response.Write("</BODY>")
            Response.Write("</html>")

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function
End Class
