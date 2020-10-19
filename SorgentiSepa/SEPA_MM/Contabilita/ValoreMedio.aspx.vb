
Partial Class Contratti_Report_ValoreMedio
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then


            Try
                Dim COLORE As String = "#E6E6E6"
                Dim sStringaSQL As String = ""
                Dim DataDal As String = ""
                Dim DataAl As String = ""
                Dim testoTabella As String = ""
                Dim testoTabellaVoci As String = ""
                Dim bTrovato As Boolean = False
                Dim CondDate As String = ""
                Dim Nunita As Double = 0
                Dim TotMq As Double = 0
                Dim totServRimb As Double = 0
                Dim totPerNumUni As Double = 0
                Dim totPerMq As Double = 0

                Dim Str As String = ""
                Dim NUMERORIGHE As Long = 0
                Dim Contatore As Long = 0

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
                Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

                Response.Write(Str)
                Response.Flush()



                'If Request.QueryString("DAL") <> "" Then
                '******APERTURA CONNESSIONE*****
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                testoTabellaVoci = "<table cellpadding='0' cellspacing='0' width='100%'>" _
                & "<tr>" _
                & "<td style='height: 19;text-align:right'width='50%'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>MEDIA PER N. UNITA'</strong></span></td>" _
                & "<td style='height: 19px;text-align:right'width='50%'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>MEDIA PER MQ</strong></span></td>" _
                & "</tr></table>"

                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                & "<tr>" _
                & "<td style='height: 19px;text-align:left'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>DENOMINAZIONE COMPLESSO</strong></span></td>" _
                & "<td style='height: 19px;text-align:left'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>INDIRIZZO</strong></span></td>" _
                & "<td style='height: 19px;text-align:center'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>VALORE MEDIO</strong></span>" & testoTabellaVoci & "</td>" _
                & "</tr>"

                DataDal = Request.QueryString("DAL")
                DataAl = Request.QueryString("AL")
                '*****AGGIUNTA DI 30 GIORNI DALLA DATA INIZIALE SE LA DATA AL è VUOTA, ALTRIMENTI PRENDE DATA AL****

                'DataAl = par.IfEmpty(Request.QueryString("AL"), par.AggiustaData(DateAdd("d", 30, par.FormattaData(Request.QueryString("DAL")))))

                If DataDal <> "" Then
                    bTrovato = True
                    CondDate = CondDate & " BOL_BOLLETTE.DATA_EMISSIONE>='" & par.PulisciStrSql(DataDal) & "' "
                End If

                If DataAl <> "" Then
                    If bTrovato = True Then CondDate = CondDate & " AND "
                    CondDate = CondDate & " BOL_BOLLETTE.DATA_EMISSIONE<='" & par.PulisciStrSql(DataAl) & "' "
                End If

                If CondDate <> "" Then
                    CondDate = CondDate & " AND "
                End If
                Me.Label6.Text = "VALORE MEDIO DEI SERVIZI A RIMBORSO DAL " & par.IfEmpty(Request.QueryString("INIZIO"), "--") & " AL " & par.IfEmpty(Request.QueryString("FINE"), "--")

                sStringaSQL = "SELECT COUNT(COMPLESSI_IMMOBILIARI.ID) AS NUM_RIGHE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.TAB_COMUNI  WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID AND  INDIRIZZI.COD_COMUNE= TAB_COMUNI.COD_COM "
                par.cmd.CommandText = sStringaSQL
                Dim myReaderTEMP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReaderTEMP.Read Then
                    NUMERORIGHE = par.IfNull(myReaderTEMP("NUM_RIGHE"), 0)
                End If
                myReaderTEMP.Close()
                sStringaSQL = ""




                sStringaSQL = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS COMPLESSO, (INDIRIZZI.DESCRIZIONE||', '||INDIRIZZI.CIVICO||' '||TAB_COMUNI.COMUNE) AS INDIRIZZO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.TAB_COMUNI  WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID AND  INDIRIZZI.COD_COMUNE= TAB_COMUNI.COD_COM ORDER BY COMPLESSO ASC"
                par.cmd.CommandText = sStringaSQL
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read

                    'If myReader("COMPLESSO") = "* BOLLETTAZIONE *" Then
                    '    Beep()
                    'End If
                    testoTabella = testoTabella _
                    & "<tr bgcolor = '" & COLORE & "'>" _
                    & "<td style='height: 19px;text-align:left'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COMPLESSO"), "") & "</span></td>" _
                    & "<td style='height: 19px; text-align:left'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("INDIRIZZO"), "") & "</span></td>" _
                    & "<td style='height: 19px; text-align:right'>"  'APERTURA COLONNA VALORE MEDIO


                    '*****************************************
                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS VALORE FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND  BOL_BOLLETTE_VOCI.ID_VOCE IN (533,300,301,302,303) AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO =" & myReader("ID") & ""
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        totServRimb = par.IfNull(myReader2("VALORE"), 0)
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT COUNT(BOL_BOLLETTE.ID_UNITA) AS NUM_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO =" & myReader("ID") & " AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA AND ID_UNITA IN (SELECT DISTINCT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA )"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        Nunita = par.IfNull(myReader2("NUM_UNITA"), 0)
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT  SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND UNITA_IMMOBILIARI.ID IN (SELECT DISTINCT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO =" & myReader("ID") & " AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA)"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TotMq = par.IfNull(myReader2("MQ"), 0)
                    End If
                    myReader2.Close()



                    testoTabella = testoTabella _
                    & "<table cellpadding='0' cellspacing='0' width='100%'>" _
                        & "<tr>" _
                            & "<td style='height: 19px;text-align:right'width='50%'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & FormattaDivisione(totServRimb, Nunita, "##,##0.00") & "</span></td>" _
                            & "<td style='height: 19px;text-align:right'width='50%'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & FormattaDivisione(totServRimb, TotMq, "##,##0.00") & "</span></td>" _
                        & "</tr> " _
                    & "</table>"

                    If FormattaDivisione(totServRimb, Nunita, "##,##0.00") <> "N.D." Then
                        totPerNumUni = totPerNumUni + FormattaDivisione(totServRimb, Nunita, "##,##0.00")
                    End If
                    If FormattaDivisione(totServRimb, TotMq, "##,##0.00") <> "N.D." Then
                        totPerMq = totPerNumUni + FormattaDivisione(totServRimb, TotMq, "##,##0.00")
                    End If


                    Nunita = 0
                    TotMq = 0
                    totServRimb = 0

                    testoTabella = testoTabella _
                    & "<tr>" 'CHIUSURA RIGA
                    If COLORE = "#FFFFFF" Then
                        COLORE = "#E6E6E6"
                    Else
                        COLORE = "#FFFFFF"
                    End If

                    Contatore = Contatore + 1
                    percentuale = (Contatore * 100) / NUMERORIGHE

                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()


                Loop

                testoTabella = testoTabella _
                & "<tr>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>" _
                        & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                        & "<tr>" _
                        & "<td style='height: 19px;text-align:right'width='60%'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(totPerNumUni, 0), "##,##0.00") & "</span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(totPerMq, 0), "##,##0.00") & "</span></td></tr></table>" _
                & "</strong></span></td>"



                testoTabella = testoTabella _
                & "<tr>"


                Me.TBL_ESTRATTO_VALORI_MEDI.Text = testoTabella & "</table>"


                '****CHIUSURA CONNESSIONE
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()




                'End If



            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End Try

        End If

    End Sub
    Private Function FormattaDivisione(ByVal quantita As Double, ByVal numero As Double, ByVal formato As String) As String

        If numero > 0 Then
            Return Format((quantita / numero), formato)
        Else
            Return "N.D."
        End If

    End Function

    Protected Sub ImgBtnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnExport.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("Content-Disposition", "attachment;filename=ValoreMedio.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Response.Write(GenHtmlTable())
        Response.End()
    End Sub
    Function GenHtmlTable()
        Dim sRet As String
        'sRet = "<table border=0 width='100%'><tr>"
        'sRet = sRet & Me.Label6.Text
        'sRet = sRet & "</tr>"
        'sRet = sRet & "<tr>" & LblSaldoInizio.Text & "</tr>"
        'sRet = sRet & "<tr>" & LblSaldoFine.Text & "</tr></table>"
        sRet = TBL_ESTRATTO_VALORI_MEDI.Text

        GenHtmlTable = sRet

    End Function
End Class
