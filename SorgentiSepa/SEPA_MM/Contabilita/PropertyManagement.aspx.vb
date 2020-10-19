
Partial Class Contratti_Report_PropertyManagement
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try

            If Not IsPostBack Then


                Dim COLORE As String = "#E6E6E6"
                Dim sStringaSQL As String = ""
                Dim DataDal As String = ""
                Dim DataAl As String = ""
                Dim testoTabella As String = ""
                Dim testoTabellaVoci As String = ""
                Dim bTrovato As Boolean = False
                Dim CondDate As String = ""

                Dim Str As String = ""
                Dim NUMERORIGHE As Long = 0
                Dim Contatore As Long = 0





                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
                Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

                Response.Write(Str)
                Response.Flush()

                If Not IsPostBack Then



                    'If Request.QueryString("DAL") <> "" Then
                    '******APERTURA CONNESSIONE*****
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If


                    testoTabellaVoci = "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                    & "<tr>" _
                                    & "<td style='height: 19;text-align:right'width='50%'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>N.</strong></span></td>" _
                                    & "<td style='height: 19px;text-align:right'width='50%'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>MQ</strong></span></td>" _
                                    & "</tr></table>"

                    testoTabella = "<table cellpadding='1' cellspacing='2' width='100%'>" & vbCrLf _
                                    & "<tr>" _
                                    & "<td style='height: 19px;text-align:left'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px;text-align:left'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>DENOMINAZIONE COMPLESSO</strong></span></td>" _
                                    & "<td style='height: 19px;text-align:left'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>INDIRIZZO</strong></span></td>" _
                                    & "<td style='height: 19px;text-align:center'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>ALLOGGI</strong></span>" & testoTabellaVoci & "</td>" _
                                    & "<td style='height: 19px;text-align:center'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>UNITA DIVERSE<br/>ESCLUSI BOX E POSTI AUTO</strong></span>" & testoTabellaVoci & "</td>" _
                                    & "<td style='height: 19px;text-align:center'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>BOX</strong></span>" & testoTabellaVoci & "</td>" _
                                    & "<td style='height: 19px;text-align:center'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>POSTI AUTO COPERTI</strong></span>" & testoTabellaVoci & "</td>" _
                                    & "<td style='height: 19px;text-align:center'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>POSTI AUTO SCOPERTI</strong></span>" & testoTabellaVoci & "</td>" _
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

                    Me.Label6.Text = "Compenso per il Property Management dal " & par.IfEmpty(Request.QueryString("INIZIO"), "--") & " al " & par.IfEmpty(Request.QueryString("FINE"), "--")

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
                        '*********************************************************OCCUPATI***************************************************************
                        testoTabella = testoTabella _
                        & "<tr bgcolor = '" & COLORE & "'>" _
                        & "<td style='height: 19px;text-align:left'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>Occupati</span></td>" _
                        & "<td style='height: 19px;text-align:left'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COMPLESSO"), "") & "</span></td>" _
                        & "<td style='height: 19px; text-align:left'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("INDIRIZZO"), "") & "</span></td>" _
                        & "<td style='height: 19px; text-align:right'>"  'Alloggioa open
                        '*****************************************ALLOGGI
                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV' AND UNITA_IMMOBILIARI.ID  IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID  )"

                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                    & "<tr>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                    & "</tr> " _
                                & "</table>"

                        End If
                        testoTabella = testoTabella & "</td>" 'Alloggioa closed

                        'totAlloggi = totAlloggi + par.IfNull(myReader2("N"), 0)
                        'totMqAlloggi = totMqAlloggi + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()

                        '****************************************UNITA' DIVERSE ESCLUSI BOX E POSTI AUTO
                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'UNITA' DIVERSE ESCLUSI BOX E POSTI AUTO Open


                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID  AND (UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'B' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'H' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'I') AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV' AND  UNITA_IMMOBILIARI.ID  IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND (UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'B' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'H' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'I') AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                     & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                         & "<tr>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                         & "</tr> " _
                                     & "</table>"
                        End If


                        testoTabella = testoTabella & "</td>" ''UNITA' DIVERSE ESCLUSI BOX E POSTI AUTO Closed

                        'totDiverse = totDiverse + par.IfNull(myReader2("N"), 0)
                        'totMqDiverse = totMqDiverse + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()


                        '****************************************UNITA' BOX 
                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'UNITA'  BOX  Open


                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID  AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'B'  AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV' AND  UNITA_IMMOBILIARI.ID  IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'B' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                     & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                         & "<tr>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                         & "</tr> " _
                                     & "</table>"
                        End If


                        testoTabella = testoTabella & "</td>" ''UNITA' BOX Closed

                        'totBox = totBox + par.IfNull(myReader2("N"), 0)
                        'totMqBox = totMqBox + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()



                        '***************************************POSTI AUTO COPERTI


                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'BOX E POSTI AUTO COPERTI Open


                        '
                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID  AND  UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'H' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND  DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'H' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                    & "<tr>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                    & "</tr> " _
                                & "</table>"
                        End If

                        testoTabella = testoTabella & "</td>" 'BOX E POSTI AUTO COPERTI Closed

                        'totPostiCop = totPostiCop + par.IfNull(myReader2("N"), 0)
                        'totMqPostiCop = totMqPostiCop + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()

                        '***************************************POSTI AUTO SCOPERTI

                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'POSTI AUTO SCOPERTI Open


                        'BOX E POSTI AUTO SCOPERTI

                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'I' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND  DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'I' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                    & "<tr>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                    & "</tr> " _
                                & "</table>"
                        End If

                        testoTabella = testoTabella & "</td>" 'POSTI AUTO SCOPERTI Closed

                        testoTabella = testoTabella _
                        & "<tr>"
                        '*********************************************************OCCUPATI END***************************************************************


                        '*_/_/_/_/_/_/_/__////_/_/_//_/_/_/_/_/_/_/_//_/_/_///_/_/_/_//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_//_//_/_/_/_/_/_/_/_/_
                        '*_/_/_/_/_/_/_/__////_/_/_//_/_/_/_/_/_/_/_//_/_/_///_/_/_/_//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_//_//_/_/_/_/_/_/_/_/_
                        '*_/_/_/_/_/_/_/__////_/_/_//_/_/_/_/_/_/_/_//_/_/_///_/_/_/_//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_//_//_/_/_/_/_/_/_/_/_


                        '*********************************************************SFITTI***************************************************************

                        testoTabella = testoTabella _
                        & "<tr bgcolor = '" & COLORE & "'>" _
                        & "<td style='height: 19px;text-align:left'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>Sfitti</span></td>" _
                        & "<td style='height: 19px;text-align:left'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COMPLESSO"), "") & "</span></td>" _
                        & "<td style='height: 19px; text-align:left'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("INDIRIZZO"), "") & "</span></td>" _
                        & "<td style='height: 19px; text-align:right'>"  'Alloggioa open
                        '*****************************************ALLOGGI
                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV' AND UNITA_IMMOBILIARI.ID NOT IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID  )"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                    & "<tr>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                    & "</tr> " _
                                & "</table>"

                        End If
                        testoTabella = testoTabella & "</td>" 'Alloggioa closed

                        'totAlloggi = totAlloggi + par.IfNull(myReader2("N"), 0)
                        'totMqAlloggi = totMqAlloggi + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()

                        '****************************************UNITA' DIVERSE ESCLUSI BOX E POSTI AUTO
                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'UNITA' DIVERSE ESCLUSI BOX E POSTI AUTO Open


                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID  AND (UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'B' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'H' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'I') AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV' AND  UNITA_IMMOBILIARI.ID NOT IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND (UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'B' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'H' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' OR UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'I') AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                     & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                         & "<tr>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                         & "</tr> " _
                                     & "</table>"
                        End If


                        testoTabella = testoTabella & "</td>" ''UNITA' DIVERSE ESCLUSI BOX E POSTI AUTO Closed

                        'totDiverse = totDiverse + par.IfNull(myReader2("N"), 0)
                        'totMqDiverse = totMqDiverse + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()


                        '****************************************UNITA' BOX 
                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'UNITA'  BOX  Open


                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID  AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'B'  AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV' AND  UNITA_IMMOBILIARI.ID NOT IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'B' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                     & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                         & "<tr>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                             & "<td style='height: 19px;text-align:right'width='50%'>" _
                                             & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                         & "</tr> " _
                                     & "</table>"
                        End If


                        testoTabella = testoTabella & "</td>" ''UNITA' BOX Closed

                        'totBox = totBox + par.IfNull(myReader2("N"), 0)
                        'totMqBox = totMqBox + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()



                        '***************************************POSTI AUTO COPERTI


                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'BOX E POSTI AUTO COPERTI Open


                        '
                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID  AND  UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'H' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND  DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND UNITA_IMMOBILIARI.ID NOT IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'H' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                    & "<tr>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                    & "</tr> " _
                                & "</table>"
                        End If

                        testoTabella = testoTabella & "</td>" 'BOX E POSTI AUTO COPERTI Closed

                        'totPostiCop = totPostiCop + par.IfNull(myReader2("N"), 0)
                        'totMqPostiCop = totMqPostiCop + par.IfNull(myReader2("MQ"), 0)

                        myReader2.Close()

                        '***************************************POSTI AUTO SCOPERTI

                        testoTabella = testoTabella & "<td style='height: 19px; text-align:right'>" 'POSTI AUTO SCOPERTI Open


                        'BOX E POSTI AUTO SCOPERTI

                        par.cmd.CommandText = "SELECT COUNT(*) AS N, SUM(VALORE) AS MQ FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'I' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND  DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND UNITA_IMMOBILIARI.ID NOT IN (SELECT ID_UNITA FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE " & CondDate & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'I' AND EDIFICI.ID_COMPLESSO = " & myReader("ID") & " AND BOL_BOLLETTE.ID_UNITA = UNITA_IMMOBILIARI.ID)"

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            testoTabella = testoTabella _
                                & "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                    & "<tr>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("N"), 0) & "</span></td>" _
                                        & "<td style='height: 19px;text-align:right'width='50%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(myReader2("MQ"), 0), "##,##0.00") & "</span></td>" _
                                    & "</tr> " _
                                & "</table>"
                        End If

                        testoTabella = testoTabella & "</td>" 'POSTI AUTO SCOPERTI Closed

                        testoTabella = testoTabella _
                        & "<tr>"
                        '*********************************************************SFITTI END***************************************************************


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
                    '*******CHIUSURA CONNESSIONE
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Me.TBL_PROP_MANAGE.Text = testoTabella & "</table>"



                End If

                'End If




            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub ImgBtnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnExport.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("Content-Disposition", "attachment;filename=ProperyManagement.xls")
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
        sRet = TBL_PROP_MANAGE.Text

        GenHtmlTable = sRet

    End Function
End Class
