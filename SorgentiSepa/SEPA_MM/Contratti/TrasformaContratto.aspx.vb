Imports ExpertPdf.HtmlToPdf
Imports System.Collections.Generic

Partial Class Contratti_TrasformaContratto
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public percentuale As Long = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)


        Dim Loading As String = " <div id='divLoading' style='position: absolute; margin: 0px; width: 100%; height: 100%;" _
                & " top: 0px; left: 0px; background-color: #ffffff; z-index: 1000;'>" _
                & " <div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px;" _
                & " margin-left: -117px; margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
                & " <table style='width: 100%; height: 100%;'>" _
                & " <tr>" _
                & " <td valign='middle' align='center'>" _
                & " <img src='../NuoveImm/load.gif' alt='Elaborazione in corso' /><p style='font-family: Arial;font-size: 8pt;'>" _
                & " Elaborazione in corso...</p>" _
                & " <div align='left' id='AA' style='background-color: #FFFFFF; border: none;width:100px;'>" _
                & " <img alt='' src='barra.gif' id='barra' height='10' width='100' /></div>" _
                & " </td>" _
                & " </tr>" _
                & " <tr>" _
                & " <td style='text-align: center;border: none;'>" _
                & " <input id='txtpercent' value='' type='text' style='width: 35px; font-family: Arial, Helvetica, sans-serif;" _
                & "  font-size: 8pt;border: none; ' />" _
                & " </td>" _
                & " </tr>" _
                & " </table>" _
                & " </div>" _
                & " </div>" _
                & " <script  language='javascript' type='text/javascript'>var tempo; tempo=0; function Mostra()" _
                & " {document.getElementById('barra').style.width = tempo + 'px';document.getElementById('txtpercent').value = tempo + '%'};setInterval('Mostra();', 100);</script>"
        If conferma.Value = "1" Then
            Response.Write(Loading)
            Response.Flush()
        End If
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("IDDICH")) Then
                idDich.Value = Request.QueryString("IDDICH")
                idContratto.Value = Request.QueryString("IDCONT")
                RicavaAreaEconomica()
                CaricaDati()
                If ControllaRiclassificate() = True Then
                    Response.Write("<script>alert('Presenza di bollette riclassificate! Impossibile procedere');self.close();</script>")
                End If

            End If
        End If

    End Sub

    Private Function RicavaAreaEconomica() As Integer
        Try
            connData.apri()
            par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_calcolo desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                idAreaEconomica.Value = par.IfNull(myReader("id_area_economica"), "")
            End If
            myReader.Close()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " RicavaAreaEconomica - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

        Return idAreaEconomica.Value
    End Function

    Private Function CercaBolletteDaStornare() As Data.DataTable
        Dim dt As New Data.DataTable

        Dim AreaEconomica As Integer = 0
        Dim condizQuery As String = ""
        par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_Calcolo desc"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            AreaEconomica = par.IfNull(myReader("id_area_economica"), "")
        End If
        myReader.Close()

        'If dataDisdetta.Value = Format(Now, "yyyyMMdd") Then
        '    condizQuery = "and substr(riferimento_a,1,6)<='" & Mid(dataDisdetta.Value, 1, 6) & "'"
        'End If
        If dataDisdetta.Value = "" Then
            dataDisdetta.Value = Format(Now, "yyyyMMdd")
        End If
        If AreaEconomica = 4 Then
            dataDisdetta.Value = Format(Now, "yyyyMMdd")
        End If

        If dataDisdetta.Value <> "" Then
            par.cmd.CommandText = "SELECT * from siscom_mi.bol_bollette where id_bolletta_storno is null and id_tipo not in (4,5,22) " _
                & " and NVL (id_rateizzazione, 0) = 0 AND NVL (id_bolletta_ric, 0) = 0 AND nvl(IMPORTO_RUOLO,0) = 0" _
                & " and substr(riferimento_da,1,6)>='" & Mid(dataDisdetta.Value, 1, 6) & "' and id_contratto=" & idContratto.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            da.Dispose()
        End If

        Return dt

    End Function

    Private Function ControllaRiclassificate() As Boolean
        ControllaRiclassificate = False
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * from siscom_mi.bol_bollette where (id_tipo=5 or id_tipo=4) " _
                & " and nvl(importo_pagato,0)=0 and substr(riferimento_da,1,6)>='" & Mid(dataDisdetta.Value, 1, 6) & "' and id_bolletta_storno is null and id_contratto=" & idContratto.Value
            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            da.Dispose()

            If dt.Rows.Count > 0 Then
                ControllaRiclassificate = True
            End If

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " ControllaRiclassificate - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
        Return ControllaRiclassificate
    End Function

    Private Sub CaricaDati()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * FROM utenza_dichiarazioni WHERE  ID = " & idDich.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each r As Data.DataRow In dt.Rows
                    txtDataDisdetta.Text = par.FormattaData(par.IfNull(r.Item("data_disdetta_392").ToString, ""))
                    dataDisdetta.Value = par.IfNull(r.Item("data_disdetta_392").ToString, Format(Now, "yyyyMMdd"))
                    'If txtDataDisdetta.Text = "" Then
                    '    txtDataDisdetta.Text = Format(Now, "dd/MM/yyyy")
                    '    dataDisdetta.Value = Format(Now, "yyyyMMdd")
                    '    If idAreaEconomica.Value <> "4" Then
                    '    lblMsgData.Style.Value = "display:block;"
                    '    lblMsgData.Text = "La data disdetta è l'odierna perchè l'Utente non ha prodotto documenti per il diritto pregresso"
                    'End If
                    'End If
                    lblPg.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../ANAUT/DichAUnuova.aspx?PR=TC&TORNA=0&CHIUDI=1&ID=" & idDich.Value & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">" & par.IfNull(r.Item("PG"), "") & "</a>"
                Next
            End If

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " CaricaDati - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub MostraCanone()

        Try
            Dim sStringasql As String = ""
            Dim ANNO_SIT_ECONOMICA As Integer = 0
            Dim cognome As String = ""
            Dim nome As String = ""
            Dim codRU As String = ""
            Dim codUI As String = ""
            Dim indirizzoUI As String = ""
            Dim TestoTutti As String = ""
            Dim TestoDecadenti As String = ""
            Dim sDEM As String = ""
            Dim sSUPCONVENZIONALE As String = ""
            Dim sANNOCOSTRUZIONE As String = ""
            Dim sCOSTOBASE As String = ""
            Dim sZONA As String = ""
            Dim sDESCRIZIONEPIANO As String = ""
            Dim sPIANO As String = ""
            Dim sCONSERVAZIONE As String = ""
            Dim sVETUSTA As String = ""
            Dim sVALORELOCATIVO As String = ""
            Dim sNUMCOMP As Integer = 0
            Dim sMINORI15 As Integer = 0
            Dim sMAGGIORI65 As Integer = 0
            Dim sNUMCOMP66 As Integer = 0
            Dim sNUMCOMP100 As Integer = 0
            Dim sNUMCOMP100C As Integer = 0
            Dim sDETRAZIONI As Integer = 0
            Dim sDETRAZIONEF As Integer = 0
            Dim sMOBILIARI As String = ""
            Dim sIMMOBILIARI As String = ""
            Dim sCOMPLESSIVO As String = ""
            Dim sISEE As String = ""
            Dim sISE As String = ""
            Dim sISR As String = ""
            Dim sISP As String = ""
            Dim sVSE As String = ""
            Dim sPSE As String = ""
            Dim sREDD_DIP As String = ""
            Dim sREDD_ALT As String = ""
            Dim sLIMITEPENSIONE As String = ""
            Dim sPREVDIP As String = ""
            Dim sSOTTOAREA As String = ""
            Dim sNOTE As String = ""
            Dim sISE_MIN As String = ""
            Dim sPER_VAL_LOC As Integer = 0
            Dim sPERC_INC_MAX_ISE_ERP As Integer = 0
            Dim sCANONESOPP As String = ""
            Dim sCOEFFFAM As String = ""
            Dim sCANONE_MIN As String = ""
            Dim sCANONECLASSE As String = ""
            Dim sISTAT As String = ""
            Dim sCANONECLASSEISTAT As String = ""
            Dim sCANONE As String = ""
            Dim numero As String = ""

            connData.apri()
            par.cmd.CommandText = "select * from utenza_dichiarazioni,utenza_comp_nucleo where utenza_dichiarazioni.id=utenza_comp_nucleo.id_dichiarazione and progr=0 and utenza_dichiarazioni.id=" & idDich.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                ANNO_SIT_ECONOMICA = par.IfNull(myReader0("ANNO_SIT_ECONOMICA"), 0)
                codRU = par.IfNull(myReader0("RAPPORTO"), "")
                codUI = par.IfNull(myReader0("POSIZIONE"), "")
                cognome = par.IfNull(myReader0("COGNOME"), "")
                nome = par.IfNull(myReader0("NOME"), "")
                numero = par.IfNull(myReader0("PG"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA,RAPPORTI_UTENZA.ID,DATA_DISDETTA_LOCATARIO,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,RAPPORTI_UTENZA.PROVENIENZA_ASS FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.COD_CONTRATTO='" & codRU & "'"
            Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderID.Read Then
                indirizzoUI = par.Cripta(par.IfNull(myReaderID("DESCRIZIONE"), "") & " " & par.IfNull(myReaderID("CIVICO"), "") & " " & par.IfNull(myReaderID("CAP"), "") & " " & par.IfNull(myReaderID("LOCALITA"), ""))
                idContratto.Value = par.IfNull(myReaderID("ID"), 0)
            End If
            myReaderID.Close()

            par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta WHERE ID=58"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                TestoTutti = par.IfNull(myReaderA("VALORE"), "")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta WHERE ID=59"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderB.Read Then
                TestoDecadenti = par.IfNull(myReaderB("VALORE"), "")
            End If
            myReaderB.Close()

            Dim CODICEANAGRAFICO As String = ""
            par.cmd.CommandText = "SELECT operatori.*,caf_web.cod_caf as ENTE from operatori,caf_web where operatori.id_caf=caf_web.id and operatori.ID=" & Session.Item("ID_OPERATORE")
            Dim myReaderENTE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderENTE.Read() Then
                CODICEANAGRAFICO = par.IfNull(myReaderENTE("ENTE"), "") & " - " & par.IfNull(myReaderENTE("COD_ANA"), "")
            End If
            myReaderENTE.Close()

            par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_calcolo desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                idAreaEconomica.Value = par.IfNull(myReader("id_area_economica"), "")
                sDEM = par.IfNull(myReader("dem"), "")
                sSUPCONVENZIONALE = par.IfNull(myReader("SUPCONVENZIONALE"), "")
                sANNOCOSTRUZIONE = par.IfNull(myReader("ANNO_COSTRUZIONE"), "")
                sCOSTOBASE = par.IfNull(myReader("ANNO_COSTRUZIONE"), "")
                sZONA = par.IfNull(myReader("ZONA"), "")
                sDESCRIZIONEPIANO = par.IfNull(myReader("NUMERO_PIANO"), "")
                sPIANO = par.IfNull(myReader("PIANO"), "")
                sCONSERVAZIONE = par.IfNull(myReader("CONSERVAZIONE"), "")
                sVETUSTA = par.IfNull(myReader("VETUSTA"), "")
                sVALORELOCATIVO = par.IfNull(myReader("VALORE_LOCATIVO"), "")
                sNUMCOMP = par.IfNull(myReader("NUM_COMP"), 0)
                sMINORI15 = par.IfNull(myReader("MINORI_15"), 0)
                sMAGGIORI65 = par.IfNull(myReader("MAGGIORI_65"), 0)
                sNUMCOMP66 = par.IfNull(myReader("NUM_COMP_66"), 0)
                sNUMCOMP100 = par.IfNull(myReader("NUM_COMP_100"), 0)
                sNUMCOMP100C = par.IfNull(myReader("NUM_COMP_100_CON"), 0)
                sDETRAZIONI = par.IfNull(myReader("DETRAZIONI"), 0)
                sDETRAZIONEF = par.IfNull(myReader("DETRAZIONI_FRAGILITA"), 0)
                sMOBILIARI = par.IfNull(myReader("REDD_MOBILIARI"), "")
                sIMMOBILIARI = par.IfNull(myReader("REDD_IMMOBILIARI"), "")
                sCOMPLESSIVO = par.IfNull(myReader("REDD_COMPLESSIVO"), "")
                sISEE = par.IfNull(myReader("ISEE"), "")
                sISE = par.IfNull(myReader("ISE"), "")
                sISR = par.IfNull(myReader("ISR"), "")
                sISP = par.IfNull(myReader("ISP"), "")
                sVSE = par.IfNull(myReader("VSE"), "")
                sPSE = par.IfNull(myReader("PSE"), "")
                sREDD_DIP = par.IfNull(myReader("REDDITI_DIP"), "")
                sREDD_ALT = par.IfNull(myReader("REDDITI_ATRI"), "")
                sLIMITEPENSIONE = par.IfNull(myReader("LIMITE_PENSIONI"), "")
                sPREVDIP = par.IfNull(myReader("REDD_PREV_DIP"), "")
                sSOTTOAREA = par.IfNull(myReader("SOTTO_AREA"), "")
                sNOTE = par.IfNull(myReader("NOTE"), "")
                sISE_MIN = par.IfNull(myReader("ISEE_27"), "")
                sPER_VAL_LOC = par.IfNull(myReader("PERC_VAL_LOC"), 0)
                sPERC_INC_MAX_ISE_ERP = par.IfNull(myReader("INC_MAX"), 0)
                sCANONESOPP = par.IfNull(myReader("CANONE_SOPPORTABILE"), "")
                sCOEFFFAM = par.IfNull(myReader("COEFF_NUCLEO_FAM"), "")
                sCANONE_MIN = par.IfNull(myReader("CANONE_MINIMO_AREA"), "")
                sCANONECLASSE = par.IfNull(myReader("CANONE_CLASSE"), "")
                sISTAT = par.IfNull(myReader("PERC_ISTAT_APPLICATA"), "")
                sCANONECLASSEISTAT = par.IfNull(myReader("CANONE_CLASSE_ISTAT"), "")
                sCANONE = par.IfNull(myReader("CANONE"), "")

                sStringasql = sStringasql & ""
                sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style2' width='50%'>"
                sStringasql = sStringasql & "COMUNE DI MILANO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td align='right' class='style2' width='50%'>"
                sStringasql = sStringasql & "ANAGRAFE UTENZA " & ANNO_SIT_ECONOMICA + 1 & " (Redditi " & ANNO_SIT_ECONOMICA & ") - " & Format(Now, "dd/MM/yyyy")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "<p>"
                sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                sStringasql = sStringasql & "INTESTATARIO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td class='style3' width='70%'>"
                sStringasql = sStringasql & cognome & " " & nome
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                sStringasql = sStringasql & "CONTRATTO COD."
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td class='style3' width='70%'>"
                sStringasql = sStringasql & codRU
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                sStringasql = sStringasql & "ALLOGGIO COD."
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td class='style3' width='70%'>"
                sStringasql = sStringasql & codUI
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                sStringasql = sStringasql & "INDIRIZZO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td class='style3' width='70%'>"
                sStringasql = sStringasql & par.DeCripta(indirizzoUI)
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "</p>"
                sStringasql = sStringasql & "<br/>"

                If idAreaEconomica.Value <> 4 Then
                    sStringasql = sStringasql & "<br/>"
                    sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>" & TestoTutti & "</p>"
                    sStringasql = sStringasql & "<br/>"
                End If

                sStringasql = sStringasql & "<p>"
                sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style4'>"
                sStringasql = sStringasql & "DETERMINAZIONE DEL VALORE LOCATIVO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "<table style='width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "COEFF. DEMOGRAFIA"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sDEM
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "SUP.CONVENZIONALE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sSUPCONVENZIONALE
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "COSTO BASE MQ"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & "Anno " & sANNOCOSTRUZIONE & " - " & sCOSTOBASE
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "INDICE UBICAZIONE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sZONA
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "INDICE PIANO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sDESCRIZIONEPIANO & " - " & sPIANO
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "INDICE CONSERVAZIONE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sCONSERVAZIONE
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "INDICE VETUSTA"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & "Anno " & sANNOCOSTRUZIONE & " - " & sVETUSTA
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "VALORE CONVENZIONALE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format((par.Tronca(sVALORELOCATIVO) * 100) / 5, "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                sStringasql = sStringasql & "<td width='60%' class='style5'>"
                sStringasql = sStringasql & "VALORE LOCATIVO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style5'>"
                sStringasql = sStringasql & Format(CDbl(par.Tronca(sVALORELOCATIVO)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "</p>"
                sStringasql = sStringasql & "<p>"
                sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style4'>"
                sStringasql = sStringasql & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "<table style='width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "NUMERO COMP."
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sNUMCOMP
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "NUMERO COMP. MINORI 15 ANNI"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sMINORI15
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "NUMERO COMP. MAGGIORI 65 ANNI"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sMAGGIORI65
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "NUMERO COMP. INVALIDI 66%-99%"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sNUMCOMP66
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "NUMERO COMP. INVALIDI 100%"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sNUMCOMP100
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "NUMERO COMP. INVALIDI 100%CON IND.ACC."
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sNUMCOMP100C
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "DETRAZIONI"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sDETRAZIONI, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "DETRAZIONI PER FRAGILITA&#39;"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sDETRAZIONEF, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "VALORE MOBILIARE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sMOBILIARI, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "VALORE IMMOBILIARE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sIMMOBILIARI, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "REDDITO COMPLESSIVO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCOMPLESSIVO, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style5'>"
                sStringasql = sStringasql & "ISEE ERP EFFETTIVO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & par.Tronca(sISEE)
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style5'>"
                sStringasql = sStringasql & "ISE ERP EFFETTIVO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & par.Tronca(sISE)
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "ISR"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & par.Tronca(sISR)
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "ISP"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sISP
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "VSE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sVSE
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "REDDITI DIPENDENTI O ASSIMILATI"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sREDD_DIP, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "ALTRI TIPI DI REDDITO IMPONIBILI"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sREDD_ALT, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "LIMITE 2 PENSIONI INPS, MINIMA+SOCIALE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(sLIMITEPENSIONE), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "PREVALENTEMENTE DIPENDENTE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sPREVDIP
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "</p>"
                sStringasql = sStringasql & "<p>"
                sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td class='style4'>"
                sStringasql = sStringasql & "DETERMINAZIONE DEL CANONE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "<table style='width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr>"

                Select Case idAreaEconomica.Value
                    Case 1
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & "AREA"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & "PROTEZIONE"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & "FASCIA"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & sSOTTOAREA & "&nbsp; <span class='style7'>" & sNOTE & "</span>"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                    Case 2
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & "AREA"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & "ACCESSO"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & "FASCIA"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & sSOTTOAREA & "&nbsp; <span class='style7'>" & sNOTE & "</span>"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                    Case 3
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & "AREA"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & "PERMANENZA"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & "FASCIA"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & sSOTTOAREA & "&nbsp; <span class='style7'>" & sNOTE & "</span>"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                    Case 4
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & ""
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & "MANCANZA DEI REQUISITI DI ACCESSO ALL'ERP"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style3'>"
                        sStringasql = sStringasql & ""
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & "&nbsp; <span class='style7'></span>"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                End Select


                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "ISEE-ERP L.R. 27"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & par.Tronca(sISE_MIN / sPSE)
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "ISE-ERP L.R. 27"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & par.Tronca(sISE_MIN)
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "PERCENTUALE VALORE LOCATIVO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sPER_VAL_LOC
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "INCIDENZA PERC. VALORE LOCATIVO"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sPERC_INC_MAX_ISE_ERP
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "VALORE INCIDENZA SU ISE-ERP"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONESOPP, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "COEFF. NUCLEI FAMILIARI"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & sCOEFFFAM
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style3'>"
                sStringasql = sStringasql & "CANONE MINIMO MENSILE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style3'>"
                sStringasql = sStringasql & Format(CDbl(sCANONE_MIN), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style6'>"
                sStringasql = sStringasql & "CANONE CLASSE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style6'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style6'>"
                sStringasql = sStringasql & "% ISTAT APPLICATA CANONE CLASSE"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style6'>"
                sStringasql = sStringasql & sISTAT
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='60%' class='style6'>"
                sStringasql = sStringasql & "CANONE CLASSE CON ISTAT"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='40%' class='style6'>"
                sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00")
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"

                If idAreaEconomica.Value <> 4 Then

                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style6'>"
                    sStringasql = sStringasql & "CANONE ERP ANNUALE REGIME"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style6'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONE, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style5'>"
                    sStringasql = sStringasql & "CANONE ERP MENSILE CALCOLATO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style5'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONE, 0) / 12), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                Else
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style6'>"
                    sStringasql = sStringasql & ""
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style6'>"
                    sStringasql = sStringasql & ""
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style5'>"
                    sStringasql = sStringasql & ""
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style5'>"
                    sStringasql = sStringasql & ""
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                End If
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "</p>"

                If idAreaEconomica.Value = 4 Then
                    sStringasql = sStringasql & "<br/>"
                    sStringasql = sStringasql & "<br/>"
                    sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>" & TestoDecadenti & "</p>"
                    sStringasql = sStringasql & "<br/>"
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey2A", "alert('Attenzione! Dichiarazione non stampata!')", True)
            End If
            myReader.Close()

            connData.chiudi()

            sStringasql = sStringasql & "</font></BODY></HTML>"
            Dim url As String = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\")

            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("Dichiarazione N. " & numero & " / " & CODICEANAGRAFICO)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = ""

            nomefile = "00_" & codRU & "_" & idDich.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sStringasql, url & nomefile, Server.MapPath("..\IMG\"))
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "window.open('../ALLEGATI/ANAGRAFE_UTENZA/" & nomefile & "','Dichiarazione','');", True)

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " MostraCanone - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub CreaStornoEnuovaBoll(ByVal idbolletta As Long, ByVal dtTot As Data.DataTable)

        Dim pagata As Boolean = False
        Dim dataPagamento As String = ""
        Dim dataValuta As String = ""
        Dim idAnagrafica As Long = 0
        Dim dataEmiss As String = ""
        Dim dataCompetDal As String = ""
        Dim dataCompetAl As String = ""
        Dim dataDecorr As String = ""
        Dim dataScadenza As String = ""
        Dim importoContrCalore As Decimal = 0
        Dim delta As Decimal = 0
        Dim dtV As New Data.DataTable
        Dim idBollGest As Long = 0

        par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto.Value & " AND ID=" & idbolletta
        Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt0 As New Data.DataTable
        da0.Fill(dt0)
        da0.Dispose()
        If dt0.Rows.Count > 0 Then
            If par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0 Or (par.IfNull(dt0.Rows(0).Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0) Then
                pagata = True
                dataPagamento = par.IfNull(dt0.Rows(0).Item("DATA_PAGAMENTO"), "")
                dataValuta = par.IfNull(dt0.Rows(0).Item("DATA_VALUTA"), "")
                dataEmiss = par.IfNull(dt0.Rows(0).Item("DATA_EMISSIONE"), "")
                dataCompetDal = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_DA"), "")
                dataCompetAl = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_A"), "")
            Else
                pagata = False
                dataPagamento = Format(Now, "yyyyMMdd")
                dataValuta = Format(Now, "yyyyMMdd")
                dataEmiss = par.IfNull(dt0.Rows(0).Item("DATA_EMISSIONE"), "")
                dataCompetDal = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_DA"), "")
                dataCompetAl = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_A"), "")
            End If
        End If

        Dim freqCanone As String = ""
        par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza where id='" & idContratto.Value & "'"
        Dim lettoreOA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreOA.Read Then
            dataDecorr = par.IfNull(lettoreOA("DATA_DECORRENZA"), "")
            dataScadenza = par.IfNull(lettoreOA("DATA_SCADENZA"), "")
            freqCanone = par.IfNull(lettoreOA("NRO_RATE"), "")
        End If
        lettoreOA.Close()

        Dim proxBoll As String = ""
        par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza_prossima_bol where id_contratto='" & idContratto.Value & "'"
        lettoreOA = par.cmd.ExecuteReader
        If lettoreOA.Read Then
            proxBoll = par.IfNull(lettoreOA("prossima_bolletta"), "")
        End If
        lettoreOA.Close()

        Dim idAnagr As Long = 0
        par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE " _
            & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND RAPPORTI_UTENZA.ID=" & idContratto.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
        Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreDati.Read Then
            idAnagr = par.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
        End If
        lettoreDati.Close()

        'Dim dataAttuale As String = ""
        'Dim dataInizioCompet As String = ""
        'Dim dataFineCompet As String = ""
        'dataAttuale = Format(Now, "dd/MM/yyyy")
        'If dataAttuale <> "" Then
        '    dataInizioCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & "01"
        '    dataFineCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & DateTime.DaysInMonth(Right(dataAttuale, 4), dataAttuale.Substring(3, 2))
        'End If

        'STORNA BOLLETTA SELEZIONATA
        Dim note As String = ""
        Dim pagataParz As Boolean = False
        If pagata = True Then
            Dim importoTot As Decimal = 0

            importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0)

            If par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0) > par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) Then
                importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0)
                pagataParz = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                        & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & idContratto.Value & "," & par.RicavaEsercizioCorrente & "," & dt0.Rows(0).Item("ID_UNITA") & "," & idAnagr & ",'" & dataCompetDal & "','" & dataCompetAl & "'," & par.VirgoleInPunti(importoTot * -1) & "," _
                        & "'" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_VALUTA") & "',6,'N',NULL,'ECCEDENZA PER PAGAMENTO BOLLETTA STORNATA " & dt0.Rows(0).Item("NUM_BOLLETTA") & "')"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                idBollGest = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",712," & par.VirgoleInPunti(importoTot * -1) & ")"
            par.cmd.ExecuteNonQuery()
        End If

        If pagataParz = True Then
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & idbolletta
            Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            daV.Fill(dtV)
            daV.Dispose()
        Else
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & idbolletta
            Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            daV.Fill(dtV)
            daV.Dispose()
        End If

        par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto.Value & "AND ID=" & idbolletta
        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt1 As New Data.DataTable
        da1.Fill(dt1)
        da1.Dispose()
        For Each row As Data.DataRow In dt1.Rows

            note = "STORNO PER TRASFORM. CONTRATTO - NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA")

            par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                    & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                    & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                    & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                    & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                    & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, NOTE_PAGAMENTO, " _
                    & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                    & "Values " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                    & "', '" & Format(Now, "yyyyMMdd") & "', NULL,NULL,NULL,'" & par.PulisciStrSql(note) & "'," _
                    & "" & par.IfNull(row.Item("ID_CONTRATTO"), 0) _
                    & " ," & par.RicavaEsercizioCorrente & ", " _
                    & par.IfNull(row.Item("ID_UNITA"), 0) _
                    & ", '0', '" & par.PulisciStrSql(par.IfNull(row.Item("PAGABILE_PRESSO"), "")) & "', " & par.IfNull(row.Item("COD_AFFITTUARIO"), 0) & "" _
                    & ", '" & par.PulisciStrSql(par.IfNull(row.Item("INTESTATARIO"), "")) & "', " _
                    & "'" & par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")) _
                    & "', '" & par.PulisciStrSql(par.IfNull(row.Item("CAP_CITTA"), "")) _
                    & "', '" & par.PulisciStrSql(par.IfNull(row.Item("PRESSO"), "")) & "', '" & par.IfNull(row.Item("RIFERIMENTO_DA"), "") _
                    & "', '" & par.IfNull(row.Item("RIFERIMENTO_A"), "") & "', " _
                    & "'1', " & par.IfNull(row.Item("ID_COMPLESSO"), 0) & ", '', '', " _
                    & Year(Now) & ", '', " & par.IfNull(row.Item("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD',22)"
            par.cmd.ExecuteNonQuery()
        Next

        Dim ID_BOLLETTA_STORNO As Long = 0
        par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
        Dim myReaderST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderST.Read Then
            ID_BOLLETTA_STORNO = myReaderST(0)
        End If
        myReaderST.Close()

        Dim ID_VOCE_STORNO As Long = 0
        Dim SumImportoVOCI As Decimal = 0
        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID= " & idbolletta
        Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtBVoci As New Data.DataTable
        daBVoci.Fill(dtBVoci)
        daBVoci.Dispose()
        For Each row As Data.DataRow In dtBVoci.Rows
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI ( ID, ID_BOLLETTA, ID_VOCE, IMPORTO, NOTE, IMP_PAGATO_OLD," _
                & "IMP_PAGATO_BAK, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO, COMPETENZA_INIZIO," _
                & "COMPETENZA_FINE, FL_ACCERTATO ) VALUES ( SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL, " & ID_BOLLETTA_STORNO & ", " & row.Item("ID_VOCE") & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & ",'STORNO'," _
                & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_OLD"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_BAK"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), 0)) & "," _
                & "'" & par.IfNull(row.Item("COMPETENZA_INIZIO"), "") & "', '" & par.IfNull(row.Item("COMPETENZA_FINE"), "") & "'," & par.IfNull(row.Item("FL_ACCERTATO"), 0) & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.CURRVAL FROM DUAL"
            Dim myReaderIDV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIDV.Read Then
                ID_VOCE_STORNO = myReaderIDV(0)
            End If
            myReaderIDV.Close()

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & " WHERE ID=" & ID_VOCE_STORNO
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) & " WHERE ID=" & row.Item("ID")
            par.cmd.ExecuteNonQuery()

            SumImportoVOCI = SumImportoVOCI + par.IfNull(row.Item("IMPORTO"), 0)
        Next

        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_STORNO & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & idbolletta
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & ID_BOLLETTA_STORNO
        par.cmd.ExecuteNonQuery()

        Dim strPagata As String = ""
        If pagata = True Then
            strPagata = "(precedentam. pagata) "
        Else
            strPagata = "(non precedentem. pagata) "
        End If
        If pagataParz = True Then
            pagata = False
            strPagata = "(parzialm. pagata) "
        End If
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F203','NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA") & " " & strPagata & " STORNATA PER TRASFORM. CONTRATTO')"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & idContratto.Value
        Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader0.Read Then
            idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), 0)
        End If
        myReader0.Close()

        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader

        'Inserimento nuova bolletta

        If Mid(dt0.Rows(0).Item("RIFERIMENTO_DA"), 1, 6) < Format(Now, "yyyyMM") Then


            par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & idContratto.Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
            myReaderS = par.cmd.ExecuteReader()
            If myReaderS.Read Then
                par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                    & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                    & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                    & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                    & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                    & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                    & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                    & "Values " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 99, '" & dt0.Rows(0).Item("DATA_EMISSIONE") _
                    & "', '" & dt0.Rows(0).Item("DATA_SCADENZA") & "', NULL,NULL,NULL,'IMPORTO RICALCOLATO IN SEGUITO A RETTIFICA PARTITA CONTAB.'," _
                    & "" & idContratto.Value _
                    & " ," & par.RicavaEsercizioCorrente & ", " _
                    & dt0.Rows(0).Item("ID_UNITA") _
                    & ", '0', '', " & idAnagrafica _
                    & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                    & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                    & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                    & "', '', '" & dataCompetDal _
                    & "', '" & dataCompetAl & "', " _
                    & "'0', " & par.IfNull(myReaderS("ID_COMPLESSO"), 0) & ", '', NULL, '', " _
                    & Year(Now) & ", '', " & par.IfNull(myReaderS("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD'," & dt0.Rows(0).Item("ID_TIPO") & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderS.Close()

            Dim ID_BOLLETTA_NEW As Long = 0
            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                ID_BOLLETTA_NEW = myReaderA(0)
            End If
            myReaderA.Close()

            'Ricavo Canone Annuo
            Dim canoneAnnuo As Decimal = 0
            par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_Calcolo desc"
            Dim myReaderEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderEC.Read Then
                canoneAnnuo = Math.Round(CDec(par.IfNull(myReaderEC("CANONE"), "0").ToString.Replace(".", "")), 2)
            End If
            myReaderEC.Close()

            For Each rowV As Data.DataRow In dtV.Rows
                If rowV.Item("ID_VOCE") <> 1 And rowV.Item("ID_VOCE") <> 36 And rowV.Item("ID_VOCE") <> 404 And rowV.Item("ID_VOCE") <> 562 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & rowV.Item("ID_VOCE") & "," & par.VirgoleInPunti(par.IfNull(rowV.Item("IMPORTO"), 0)) & ",'" & par.PulisciStrSql(par.IfNull(rowV.Item("NOTE"), "")) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            'VOCE CANONE ID = 1
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & ",1," & par.VirgoleInPunti(canoneAnnuo / freqCanone) & ")"
            par.cmd.ExecuteNonQuery()

            importoContrCalore = ContributoCalore()
            delta = CalcolaScontoCostoBase()

            importoContrCalore = Math.Round(importoContrCalore, 2)
            delta = Math.Round(delta, 2)

            Dim voceDaUsareRidCanone As Integer = 0
            Dim voceDaUsareContrCalore As Integer = 0

            'Dim contaBollGenerale As Integer = 0
            'contaBollGenerale = dtTot.Select("RIFERIMENTO_DA> = " & Mid(dtTot.Rows(0).Item("RIFERIMENTO_DA"), 1, 4) & "0101 and RIFERIMENTO_A< = " & Mid(dtTot.Rows(0).Item("RIFERIMENTO_DA"), 1, 4) & "1231").Length

            Select Case Mid(dt0.Rows(0).Item("RIFERIMENTO_DA"), 1, 4)
                Case "2012", "2013", "2014"
                    If importoContrCalore <> 0 Then
                        contaBollette = contaBollette + 1
                        voceDaUsareContrCalore = 693
                    End If
                    If delta <> 0 Then
                        voceDaUsareRidCanone = 694
                        contaBolletteB = contaBolletteB + 1
                    End If
                Case "2015"
                    contaBollette = 0
                    contaBolletteB = 0
                    If importoContrCalore <> 0 Then
                        contaBollette2 = contaBollette2 + 1
                        voceDaUsareContrCalore = 803
                    End If
                    If delta <> 0 Then
                        voceDaUsareRidCanone = 802
                        contaBollette2B = contaBollette2B + 1
                    End If
                Case "2016", "2017"
                    contaBollette2 = 0
                    contaBollette2B = 0
                    If importoContrCalore <> 0 Then
                        contaBollette3 = contaBollette3 + 1
                        voceDaUsareContrCalore = 132
                    End If
                    If delta <> 0 Then
                        voceDaUsareRidCanone = 131
                        contaBollette3B = contaBollette3B + 1
                    End If
            End Select
            If importoContrCalore <> 0 And voceDaUsareContrCalore <> 0 Then
                'If contaBollette > 0 Then
                '    importoContrCalore = Math.Round(importoContrCalore / contaBollette, 2)
                'End If
                'If contaBollette2 > 0 Then
                '    importoContrCalore = Math.Round(importoContrCalore / contaBollette2, 2)
                'End If
                'If contaBollette3 > 0 Then
                '    importoContrCalore = Math.Round(importoContrCalore / contaBollette3, 2)
                'End If


                importoContrCalore = Math.Round(importoContrCalore / freqCanone, 2)


                par.cmd.CommandText = "select BOL_BOLLETTE_VOCI.* from SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.BOL_BOLLETTE where BOL_BOLLETTE_VOCI.id_bolletta=SISCOM_MI.BOL_BOLLETTE.id and " _
                    & " BOL_BOLLETTE_VOCI.id_voce=" & voceDaUsareContrCalore & " and id_Contratto=" & idContratto.Value
                Dim daSconti As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtSconti As New Data.DataTable
                daSconti = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                daSconti.Fill(dtSconti)
                daSconti.Dispose()
                If dtSconti.Rows.Count > 0 Then
                    For Each rowSconti As Data.DataRow In dtSconti.Rows
                        If par.IfNull(rowSconti.Item("IMP_PAGATO"), 0) = par.IfNull(rowSconti.Item("IMPORTO"), 0) Then
                            par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI set importo=" & par.VirgoleInPunti(importoContrCalore * (-1)) & ",IMP_PAGATO=" & par.VirgoleInPunti(importoContrCalore * (-1)) & " where id=" & rowSconti.Item("ID")
                            par.cmd.ExecuteNonQuery()
                        ElseIf par.IfNull(rowSconti.Item("IMP_PAGATO"), 0) = 0 Then
                            par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI set importo=" & par.VirgoleInPunti(importoContrCalore * (-1)) & " where id=" & rowSconti.Item("ID")
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & voceDaUsareContrCalore & "," & par.VirgoleInPunti(importoContrCalore * (-1)) & ")"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                   & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & voceDaUsareContrCalore & "," & par.VirgoleInPunti(importoContrCalore * (-1)) & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            If delta <> 0 And voceDaUsareRidCanone <> 0 Then

                delta = Math.Round(delta / freqCanone, 2)

                par.cmd.CommandText = "select BOL_BOLLETTE_VOCI.* from SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.BOL_BOLLETTE where BOL_BOLLETTE_VOCI.id_bolletta=SISCOM_MI.BOL_BOLLETTE.id and " _
                    & " BOL_BOLLETTE_VOCI.id_voce=" & voceDaUsareRidCanone & " and id_Contratto=" & idContratto.Value
                Dim daSconti2 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtSconti2 As New Data.DataTable
                daSconti2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                daSconti2.Fill(dtSconti2)
                daSconti2.Dispose()
                If dtSconti2.Rows.Count > 0 Then
                    For Each rowSconti2 As Data.DataRow In dtSconti2.Rows
                        If par.IfNull(rowSconti2.Item("IMP_PAGATO"), 0) = par.IfNull(rowSconti2.Item("IMPORTO"), 0) Then
                            par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI set importo=" & par.VirgoleInPunti(delta * (-1)) & ",IMP_PAGATO=" & par.VirgoleInPunti(delta * (-1)) & " where id=" & rowSconti2.Item("ID")
                            par.cmd.ExecuteNonQuery()
                        ElseIf par.IfNull(rowSconti2.Item("IMP_PAGATO"), 0) = 0 Then
                            par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI set importo=" & par.VirgoleInPunti(delta * (-1)) & " where id=" & rowSconti2.Item("ID")
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & voceDaUsareRidCanone & "," & par.VirgoleInPunti(delta * (-1)) & ")"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & voceDaUsareRidCanone & "," & par.VirgoleInPunti(delta * (-1)) & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F08','')"
            par.cmd.ExecuteNonQuery()

            Dim importo As Decimal = 0
            If pagata = True Then
                par.cmd.CommandText = "select sum(importo) from siscom_mi.bol_bollette_voci where id_bolletta=" & ID_BOLLETTA_NEW
                Dim myReaderbb As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                importo = par.IfNull(par.cmd.ExecuteScalar, 0)

                PagaBollettaStorno(idBollGest, ID_BOLLETTA_NEW, idAnagr, dataPagamento, dataValuta)
            End If

        End If
        'FINE Inserimento nuova bolletta


    End Sub

    Private Function CalcolaScontoCostoBase() As Decimal

        Dim canoneMIN As Decimal = 0
        Dim nuovoCostoBase As Decimal = 0
        Dim valoreConvenz As Decimal = 0
        Dim valoreLocativo As Decimal = 0
        Dim canoneSopp As Decimal = 0
        Dim canoneApp As Decimal = 0
        Dim esclusione As String = ""
        Dim idEdificio As Long = 0
        Dim idUnita As Long = 0
        Dim scontoCostoBase As Integer = 0
        Dim scontoOk As Boolean = False
        Dim canoneClasse As Decimal = 0
        Dim CanoneErpRegime As Decimal = 0
        Dim tipoCanone As String = ""
        Dim numRate As Integer = 0
        Dim calcoloUltimoAnno As Boolean = False
        Dim codContratto As String = ""

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContratto.Value & " and id_unita_principale is null"
        Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader0.Read Then
            idEdificio = par.IfNull(myReader0("ID_EDIFICIO"), "")
            idUnita = par.IfNull(myReader0("ID_UNITA"), "")
        End If
        myReader0.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContratto.Value
        myReader0 = par.cmd.ExecuteReader()
        If myReader0.Read Then
            numRate = par.IfNull(myReader0("NRO_RATE"), 0)
            codContratto = par.IfNull(myReader0("COD_CONTRATTO"), 0)
        End If
        myReader0.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EDIFICI WHERE ID=" & idEdificio
        myReader0 = par.cmd.ExecuteReader()
        If myReader0.Read Then
            scontoCostoBase = par.IfNull(myReader0("SCONTO_COSTO_BASE"), 0)
        End If
        myReader0.Close()

        Dim canoneIniz As Decimal = 0
        par.cmd.CommandText = "SELECT * FROM UTENZA_DICH_CANONI_EC WHERE ID_CONTRATTO=" & idContratto.Value & " AND ID_DICHIARAZIONE=" & idDich.Value & " order by data_calcolo desc"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            calcoloUltimoAnno = True
            canoneIniz = par.IfNull(myReader("CANONE"), 0)
            tipoCanone = par.IfNull(myReader("TIPO_CANONE_APP"), 0)
            If par.IfNull(myReader("ID_AREA_ECONOMICA"), 0) = 4 Then
                esclusione = "a)"
            End If
            If par.IfNull(myReader("CANONE"), 0) = par.IfNull(myReader("CANONE_MINIMO_AREA"), 0) Then
                esclusione = "b)"
            End If
            If par.IfNull(myReader("CANONE"), 0) < par.IfNull(myReader("CANONE_91"), 0) Then
                esclusione = "c)"
            End If

            If scontoCostoBase <> 0 And esclusione = "" Then
                scontoOk = True
            End If

            canoneMIN = par.IfNull(myReader("CANONE_MINIMO_AREA"), "0") * 12
            nuovoCostoBase = par.IfNull(myReader("COSTOBASE"), "0") + ((scontoCostoBase / 100) * par.IfNull(myReader("COSTOBASE"), "0"))
            valoreConvenz = nuovoCostoBase * par.IfNull(myReader("SUPCONVENZIONALE"), "0") * par.IfNull(myReader("DEM"), "0") * par.IfNull(myReader("ZONA"), "0") * par.IfNull(myReader("PIANO"), "0") * par.IfNull(myReader("CONSERVAZIONE"), "0") * par.IfNull(myReader("VETUSTA"), "0")
            valoreLocativo = (valoreConvenz * 5) / 100
            canoneSopp = par.IfNull(myReader("CANONE_SOPPORTABILE"), "0")
            canoneApp = Format((par.IfNull(myReader("PERC_VAL_LOC"), "0") * valoreLocativo) / 100, "0.00")

            canoneClasse = (canoneApp + ((canoneApp * CDec(par.IfNull(myReader("PERC_ISTAT_APPLICATA"), "0"))) / 100)) * par.IfNull(myReader("COEFF_NUCLEO_FAM"), "0")
            If CDec(canoneSopp) < CDec(canoneClasse) Then
                If CDec(canoneSopp) > CDec(canoneMIN) Then
                    CanoneErpRegime = canoneSopp
                    'tipoCanone = "SOPPORTABILE"
                Else
                    CanoneErpRegime = canoneMIN
                    'tipoCanone = "MINIMO AREA"
                End If
            Else
                If CDec(canoneClasse) > CDec(canoneMIN) Then
                    CanoneErpRegime = canoneClasse
                    'tipoCanone = "CLASSE"
                Else
                    CanoneErpRegime = canoneMIN
                    'tipoCanone = "MINIMO AREA"
                End If
            End If

        End If
        myReader.Close()

        Dim canoneScontato As Decimal = 0
        Dim delta As Decimal = 0
        Dim importo694 As Decimal = 0

        If calcoloUltimoAnno = True Then

            If tipoCanone = "CLASSE" And scontoOk = True Then
                canoneScontato = CanoneErpRegime '- ((CanoneErpRegime * (scontoCostoBase * -1)) / 100)

                delta = canoneIniz - canoneScontato

                If delta > 0 Then
                    par.cmd.CommandText = "UPDATE UTENZA_DICH_CANONI_EC SET SCONTO_COSTO_BASE='" & scontoCostoBase & "',DELTA_1243_12='" & Format(delta * -1, "0.00") & "',CANONE_1243_12='" & Format(canoneScontato, "0.00") & "',TIPO_CANONE_APP='" & tipoCanone & "' WHERE ID_CONTRATTO=" & idContratto.Value & " AND ID_DICHIARAZIONE=" & idDich.Value & " AND ID_BANDO_AU IS NOT NULL"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            If esclusione <> "" Then
                par.cmd.CommandText = "UPDATE UTENZA_DICH_CANONI_EC SET ESCLUSIONE_1243_12='" & esclusione & "' WHERE ID_CONTRATTO=" & idContratto.Value & " AND ID_DICHIARAZIONE=" & idDich.Value & " AND ID_BANDO_AU IS NOT NULL"
                par.cmd.ExecuteNonQuery()
            End If
        End If

        Return delta

    End Function

    Private Function ContributoCalore() As Decimal
        Dim IdVoceContributoCanone As Long = 0
        Dim IdVoceContributoCalore As Long = 0

        Dim VociContributo As Boolean = par.RicavaIdVociContributo(IdVoceContributoCanone, IdVoceContributoCalore)
        If VociContributo = False Then
            'errore
        End If

        Dim idAreaEconomica As Integer = 0
        Dim importoContrCalore As Decimal = 0
        Dim VoceDaCancell As Boolean = False
        Dim impContrCaloreDaCanc As Decimal = True

        '**** CALCOLA 
        par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_calcolo desc"
        Dim myReaderEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderEC.Read Then
            idAreaEconomica = par.IfNull(myReaderEC("ID_AREA_ECONOMICA"), "")
        End If
        myReaderEC.Close()

        importoContrCalore = par.CalcolaContribCalore(idContratto.Value, idAreaEconomica, VoceDaCancell, impContrCaloreDaCanc, IdVoceContributoCalore)

        Return importoContrCalore
    End Function

    Private Sub PagaBollettaStorno(ByVal idBollGest As Long, ByVal idBollNew As Long, ByVal idAnagr As Long, ByVal dataPagamento As String, ByVal datavaluta As String)

        Dim importoMorosita As Decimal = 0
        Dim impNuovoPAGATO As Decimal = 0
        Dim diffCreditoMoros As Decimal = 0
        Dim idTipoBoll As Long = 0
        Dim impCredito As Decimal = 0
        Dim impCredIniziale As Decimal = 0

        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.* FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE BOL_BOLLETTE_GEST.ID=SISCOM_MI.BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND BOL_BOLLETTE_GEST.ID=" & idBollGest & " ORDER BY BOL_BOLLETTE_VOCI_GEST.ID ASC"
        Dim daGest As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dtGest As New Data.DataTable
        daGest = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        daGest.Fill(dtGest)
        daGest.Dispose()
        If dtGest.Rows.Count > 0 Then
            For Each rowGest As Data.DataRow In dtGest.Rows
                impCredIniziale = Math.Abs(par.IfNull(rowGest.Item("IMPORTO"), 0))
                impCredito = par.IfNull(rowGest.Item("IMPORTO"), 0)

                '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
                par.cmd.CommandText = "select distinct(bol_bollette.id),bol_bollette.*,bol_bollette_voci.*,BOL_BOLLETTE_VOCI.ID AS ID_VOCE1 from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id=" & idBollNew & " ORDER BY IMPORTO ASC"
                Dim daMoros1 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtMoros1 As New Data.DataTable
                daMoros1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                daMoros1.Fill(dtMoros1)
                daMoros1.Dispose()
                If dtMoros1.Rows.Count > 0 Then
                    '(0)*** AGGIORNO LA VOCE degli INCASSI EXTRAMAV ***
                    par.cmd.CommandText = "INSERT INTO siscom_mi.incassi_extramav (ID, id_tipo_pag, motivo_pagamento, id_contratto,data_pagamento, riferimento_da, riferimento_a, fl_annullata,importo, id_operatore,id_bolletta_gest)" _
                        & "VALUES (siscom_mi.seq_incassi_extramav.NEXTVAL,12,'RIPARTIZ. CREDITO DA STORNO'," & idContratto.Value & ",'" & Format(Now, "yyyyMMdd") & "','','',0," & par.VirgoleInPunti(impCredito) & "," & Session.Item("ID_OPERATORE") & "," & idBollGest & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.currval from dual"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        idIncasso = par.IfNull(lettore(0), "")
                    End If
                    lettore.Close()

                    'idEventoPrincipale = WriteEvent(True, "null", "F205", impCredito, Format(Now, "dd/MM/yyyy"), "null", idIncasso, idContratto.Value, "COPERTURA VOCI BOLLETTA CON CREDITO DA STORNO")

                    For Each row1 As Data.DataRow In dtMoros1.Rows
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & row1.Item("ID_BOLLETTA") & " AND DATA_PAGAMENTO IS NOT NULL"
                        lettore = par.cmd.ExecuteReader
                        If Not lettore.Read Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & datavaluta _
                                        & "',DATA_PAGAMENTO='" & dataPagamento & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                        & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()
                        End If
                        lettore.Close()

                        If dtMoros1.Rows(0).Item("ID_TIPO") <> "4" Then
                            importoMorosita = par.IfNull(row1.Item("IMPORTO"), 0) - par.IfNull(row1.Item("IMP_PAGATO"), 0)
                            If impCredito <> 0 Then
                                diffCreditoMoros = importoMorosita - Math.Abs(impCredito)

                                If diffCreditoMoros >= 0 Then
                                    '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO in BOL_BOLLETTE_VOCI_PAGAM ***
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                    & "(" & row1.Item("ID_VOCE1") & ",'" & row1.Item("DATA_PAGAMENTO") & "'," & par.VirgoleInPunti(Math.Round(impCredito, 2)) & ",4," & idIncasso & ")"
                                    par.cmd.ExecuteNonQuery()

                                    'WriteEvent(False, row1.Item("ID_VOCE1"), "F205", impCredito, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto.Value, "")

                                    impCredito = impCredito + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                    'ATTRAVERSO IL CREDITO, INIZIO AD INCASSARE LE BOLLETTE (scaduta e sollecitata) PARTENDO DALLE PIù VECCHIE

                                    '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                    par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impCredito) & " where id=" & row1.Item("ID_VOCE1")
                                    par.cmd.ExecuteNonQuery()

                                    impCredito = 0
                                Else
                                    impNuovoPAGATO = importoMorosita + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                    '(1)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                    par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impNuovoPAGATO) & " where id=" & row1.Item("ID_VOCE1")
                                    par.cmd.ExecuteNonQuery()

                                    '(2)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                    If importoMorosita <> 0 Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                        & "(" & row1.Item("ID_VOCE1") & ",'" & row1.Item("DATA_PAGAMENTO") & "'," & par.VirgoleInPunti(Math.Round(importoMorosita, 2)) & ",4," & idIncasso & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If

                                    'WriteEvent(False, row1.Item("ID_VOCE1"), "F205", importoMorosita, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto.Value, "")

                                    impCredito = Math.Abs(impCredito) - importoMorosita
                                End If
                            Else
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
        End If
        Dim importoRipartito As Decimal = 0
        If impCredito > 0 Then
            importoRipartito = impCredIniziale - impCredito
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST set IMPORTO_TOTALE=" & par.VirgoleInPunti(par.IfNull(impCredito * (-1), 0)) & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F204','IMPORTO STORNATO DI EURO " & Format(impCredito, "##,##0.00") & " PARI A EURO " & Format(impCredIniziale, "##,##0.00") & " DI CUI EURO " & Format(importoRipartito, "##,##0.00") & " UTILIZZATI PER RICOPRIRE LA NUOVA BOLLETTA EMESSA')"
            par.cmd.ExecuteNonQuery()

        Else
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()
        End If
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F08','IMPORTO PARI A EURO " & Format(importoRipartito, "##,##0.00") & " PAGATO CON CREDITO DA STORNO')"
        par.cmd.ExecuteNonQuery()
        'Next
        'End If
    End Sub

    Public Property idEventoPrincipale() As Long
        Get
            If Not (ViewState("par_idEventoPrincipale") Is Nothing) Then
                Return CLng(ViewState("par_idEventoPrincipale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEventoPrincipale") = value
        End Set

    End Property


    Public Property idIncasso() As Long
        Get
            If Not (ViewState("par_idIncasso") Is Nothing) Then
                Return CLng(ViewState("par_idIncasso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idIncasso") = value
        End Set

    End Property

    Public Property contaBollette2() As Integer
        Get
            If Not (ViewState("par_contaBollette2") Is Nothing) Then
                Return CInt(ViewState("par_contaBollette2"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_contaBollette2") = value
        End Set
    End Property

    Public Property contaBollette3() As Integer
        Get
            If Not (ViewState("par_contaBollette3") Is Nothing) Then
                Return CInt(ViewState("par_contaBollette3"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_contaBollette3") = value
        End Set
    End Property

    Public Property contaBollette() As Integer
        Get
            If Not (ViewState("par_contaBollette") Is Nothing) Then
                Return CInt(ViewState("par_contaBollette"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_contaBollette") = value
        End Set
    End Property


    Public Property contaBolletteB() As Integer
        Get
            If Not (ViewState("par_contaBolletteB") Is Nothing) Then
                Return CInt(ViewState("par_contaBolletteB"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_contaBolletteB") = value
        End Set
    End Property

    Public Property contaBollette3B() As Integer
        Get
            If Not (ViewState("par_contaBollette3B") Is Nothing) Then
                Return CInt(ViewState("par_contaBollette3B"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_contaBollette3B") = value
        End Set
    End Property

    Public Property contaBollette2B() As Integer
        Get
            If Not (ViewState("par_contaBollette2B") Is Nothing) Then
                Return CInt(ViewState("par_contaBollette2B"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_contaBollette2B") = value
        End Set
    End Property

    Protected Sub btnStampaCanone_Click(sender As Object, e As System.EventArgs) Handles btnStampaCanone.Click
        MostraCanone()
        If txtDataDisdetta.Text = "" Or txtDataDisdetta.Text = Format(Now, "dd/MM/yyyy") Then
            If idAreaEconomica.Value <> "4" Then
                'lblMsgData.Style.Value = "display:block;"
                ' lblMsgData.Text = "La data disdetta è l'odierna perchè l'Utente non ha prodotto documenti per il diritto pregresso"
            Else
                lblMsgData.Text = ""
                lblMsgData.Style.Value = "display:none;"
            End If
        Else
            lblMsgData.Text = ""
            lblMsgData.Style.Value = "display:none;"
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.EventArgs) Handles btnProcedi.Click
        Try
            If ControllaRiclassificate() = True Then
                Response.Write("<script>alert('Impossibile procedere.Presenza di bollette riclassificate!')</script>")
            Else
                Dim dtBoll As New Data.DataTable
                If conferma.Value = "1" Then
                    btnProcedi.Visible = False
                    connData.apri(True)
                    dtBoll = CercaBolletteDaStornare()
                    Dim NUMERORIGHE As Long = 0
                    Dim Contatore As Long = 0
                    If dtBoll.Rows.Count > 0 Then
                        NUMERORIGHE = dtBoll.Rows.Count
                        For Each row As Data.DataRow In dtBoll.Rows
                            Contatore = Contatore + 1
                            percentuale = (Contatore * 100) / NUMERORIGHE

                            CreaStornoEnuovaBoll(row.Item("ID"), dtBoll)

                            percentuale = (Contatore * 100) / NUMERORIGHE
                            Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                            Response.Flush()
                        Next
                        connData.chiudi(True)
                        'Response.Write("<script>alert('Operazione effettuata!')</script>")
                        btnStampaCanone.Visible = False


                        lblTitoloPg.Visible = False
                        lblPg.Visible = False
                        lblDataDisdetta.Visible = False
                        txtDataDisdetta.Visible = False

                        imgRight.Visible = True
                        imgVisto.Visible = True
                        lblMsgData.Text = ""
                        lblMsgData.Style.Value = "display:none;"
                        lblRicalcolo.Visible = True
                        lblCrea.Visible = True

                        btnChiudiContr.Visible = True
                        conferma.Value = "0"

                        If dataDisdetta.Value = Format(Now, "yyyyMMdd") Then
                            btnStampaCanone.Visible = False
                            btnProcedi.Visible = False

                            lblTitoloPg.Visible = False
                            lblPg.Visible = False
                            lblDataDisdetta.Visible = False
                            txtDataDisdetta.Visible = False

                            imgRight.Visible = True
                            imgVisto.Visible = False
                            lblRicalcolo.Visible = False
                            lblCrea.Visible = True

                            btnChiudiContr.Visible = True
                            conferma.Value = "0"
                        End If
                    Else
                        btnStampaCanone.Visible = False
                        btnProcedi.Visible = False
                        lblMsgData.Text = ""
                        lblMsgData.Style.Value = "display:none;"
                        lblTitoloPg.Visible = False
                        lblPg.Visible = False
                        lblDataDisdetta.Visible = False
                        txtDataDisdetta.Visible = False

                        imgRight.Visible = True
                        imgVisto.Visible = False
                        lblRicalcolo.Visible = False
                        lblCrea.Visible = True

                        btnChiudiContr.Visible = True
                        conferma.Value = "0"
                    End If
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function CaricaCapoluoghi() As String
        CaricaCapoluoghi = "AGRIGENTO (SICILIA) " _
        & "ALESSANDRIA(PIEMONTE) " _
        & "ANCONA(MARCHE) " _
& "AOSTA (VALLE D'AOSTA) " _
        & "AREZZO(TOSCANA) " _
        & "ASCOLI PICENO(MARCHE)) " _
        & "ASTI(PIEMONTE) " _
        & "AVELLINO(CAMPANIA) " _
        & "BARI(PUGLIA)  " _
        & "TRANI() " _
        & "ANDRIA() " _
        & "BARLETTA() " _
        & "BELLUNO(VENETO) " _
        & "BENEVENTO(CAMPANIA) " _
        & "BERGAMO(LOMBARDIA) " _
        & "BIELLA(PIEMONTE) " _
        & "BOLOGNA(EMILIA - ROMAGNA) " _
& "BOLZANO (TRENTINO-ALTO ADIGE) " _
        & "BRESCIA(LOMBARDIA) " _
        & "BRINDISI(PUGLIA) " _
        & "CAGLIARI(SARDEGNA) " _
        & "CALTANISSETTA(SICILIA) " _
        & "CAMPOBASSO(MOLISE) " _
        & "CARBONIA-IGLESIAS(SARDEGNA)) " _
        & "CASERTA(CAMPANIA) " _
        & "CATANIA(SICILIA) " _
        & "CATANZARO(CALABRIA) " _
        & "CHIETI(ABRUZZO) " _
        & "COMO(LOMBARDIA) " _
        & "COSENZA(CALABRIA) " _
        & "CREMONA(LOMBARDIA) " _
        & "CROTONE(CALABRIA) " _
        & "CUNEO(PIEMONTE) " _
        & "ENNA(SICILIA) " _
        & "FERMO(MARCHE) " _
        & "FERRARA(EMILIA - ROMAGNA) " _
        & "FIRENZE(TOSCANA) " _
        & "FOGGIA(PUGLIA) " _
        & "FORLÌ-CESENA(EMILIA - ROMAGNA) " _
        & "FROSINONE(LAZIO) " _
        & "GENOVA(LIGURIA) " _
& "GORIZIA (FRIULI-VENEZIA GIULIA) " _
        & "GROSSETO(TOSCANA) " _
        & "IMPERIA(LIGURIA) " _
        & "ISERNIA(MOLISE) " _
        & "LA SPEZIA(LIGURIA)) " _
        & "L'AQUILA (ABRUZZO) " _
        & "LATINA(LAZIO) " _
        & "LECCE(PUGLIA) " _
        & "LECCO(LOMBARDIA) " _
        & "LIVORNO(TOSCANA) " _
        & "LODI(LOMBARDIA) " _
        & "LUCCA(TOSCANA) " _
        & "MACERATA(MARCHE) " _
        & "MANTOVA(LOMBARDIA) " _
        & "MASSA-CARRARA(TOSCANA)) " _
        & "MATERA(BASILICATA) " _
        & "MESSINA(SICILIA) " _
        & " " _
        & "MODENA(EMILIA - ROMAGNA) " _
        & "MONZA() " _
        & "NAPOLI(CAMPANIA) " _
        & "NOVARA(PIEMONTE) " _
        & "NUORO(SARDEGNA) " _
        & "OLBIA(-TEMPIO(SARDEGNA)) " _
        & "ORISTANO(SARDEGNA) " _
        & "PADOVA(VENETO)" _
        & "PALERMO(SICILIA) " _
        & "PARMA(EMILIA - ROMAGNA) " _
        & "PAVIA(LOMBARDIA) " _
        & "PERUGIA(UMBRIA) " _
& "PESARO E URBINO (MARCHE) " _
   & "PESCARA(ABRUZZO) " _
      & "  PIACENZA(EMILIA - ROMAGNA) " _
        & "PISA(TOSCANA) " _
        & "PISTOIA(TOSCANA) " _
& "PORDENONE (FRIULI-VENEZIA GIULIA) " _
        & "POTENZA(BASILICATA) " _
        & "PRATO(TOSCANA) " _
        & "RAGUSA(SICILIA) " _
        & "RAVENNA(EMILIA - ROMAGNA) " _
        & "REGGIO CALABRIA(CALABRIA)) " _
        & "REGGIO EMILIA(EMILIA - ROMAGNA)) " _
        & "RIETI(LAZIO)  " _
        & "RIMINI(EMILIA - ROMAGNA) " _
        & "ROMA(LAZIO) " _
        & "ROVIGO(VENETO) " _
        & "SALERNO(CAMPANIA) " _
        & "MEDIO CAMPIDANO(SARDEGNA))  " _
        & "SASSARI(SARDEGNA)  " _
        & "SAVONA(LIGURIA) " _
        & "SIENA(TOSCANA) " _
        & "SIRACUSA(SICILIA) " _
        & "SONDRIO(LOMBARDIA) " _
        & "TARANTO(PUGLIA) " _
        & "TERAMO(ABRUZZO) " _
        & "TERNI(UMBRIA) " _
        & "TORINO(PIEMONTE) " _
        & "OGLIASTRA(SARDEGNA) " _
        & "TRAPANI(SICILIA) " _
& "TRENTO (TRENTINO-ALTO ADIGE) " _
        & "TREVISO(VENETO) " _
& "TRIESTE (FRIULI-VENEZIA GIULIA) " _
& "UDINE (FRIULI-VENEZIA GIULIA) " _
   & "     OSSOLA() " _
      & "  VARESE(LOMBARDIA) " _
        & "CUSIO() " _
        & "VENEZIA(VENETO) " _
        & "VERBANIA() " _
        & "VERBANO() " _
        & "VERCELLI(PIEMONTE) " _
        & "VERONA(VENETO) " _
        & "VIBO VALENTIA(CALABRIA)) " _
        & "VICENZA(VENETO) " _
        & "VITERBO(LAZIO)"
    End Function

    Private Sub ChiusuraContratto()

        Dim Risoluzione As Boolean = False
        Dim importoRisoluzione As Double = 0
        Dim IMPORTOINTERESSI As Double = 0
        Dim sAggiunta As String = ""
        Dim DataCalcolo As String = ""
        Dim DataInizio As String = ""

        Dim tasso As Double = 0
        Dim baseCalcolo As Double = 0

        Dim Giorni As Integer = 0
        Dim GiorniAnno As Integer = 0
        Dim dataPartenza As String = ""

        Dim Totale As Double = 0
        Dim TotalePeriodo As Double = 0
        Dim indice As Long = 0
        Dim DataFine As String = ""
        Dim num_bolletta As String = ""
        Dim importobolletta As Double = 0
        Dim I As Integer = 0

        Dim importodanni As Double = 0
        Dim importotrasporto As Double = 0

        Dim Interessi As New SortedDictionary(Of Integer, Double)

        Dim RIASSUNTO_BOLLETTA As String = ""
        Dim SPESEmav As Double = 0
        Dim INDICEANAGRAFICA As Long = 0

        Dim UnitaContratto As Long = 0

        connData.apri(True)

        dataRicons.Value = Format(Now, "dd/MM/yyyy")
        dataRicons.Value = DateAdd("d", -1, dataRicons.Value)
        Dim dataDecorrenza As String = ""
        Dim dataDisdLocatario As String = ""
        Dim impCauzione As Decimal = 0
        Dim interessiCauz As Boolean = False
        Dim luogoCor As String = ""

        If dataRicons.Value <> "" Then


            par.cmd.CommandText = "select * from siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza where unita_contrattuale.id_contratto=rapporti_utenza.id and id_contratto=" & idContratto.Value & " and id_unita_principale is null"
            Dim myReaderAa1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderAa1.Read Then
                UnitaContratto = par.IfNull(myReaderAa1("id_unita"), 0)
                luogoCor = par.IfNull(myReaderAa1("luogo_cor"), "")
                impCauzione = par.IfNull(myReaderAa1("imp_deposito_cauz"), 0)
                dataDecorrenza = par.IfNull(myReaderAa1("data_decorrenza"), "")
                If par.IfNull(myReaderAa1("interessi_cauzione"), False) = 0 Then
                    interessiCauz = False
                Else
                    interessiCauz = True
                End If
                dataDisdLocatario = par.IfNull(myReaderAa1("DATA_DISDETTA_LOCATARIO"), "")

            End If
            myReaderAa1.Close()


            Risoluzione = True
            par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=1"
            Dim myReaderAa As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderAa.Read Then
                importoRisoluzione = CDbl(par.PuntiInVirgole(myReaderAa("valore")))
            End If
            myReaderAa.Close()


            par.cmd.CommandText = "select * from siscom_mi.tab_interessi_legaLI order by anno desc"
            Interessi.Clear()
            Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReaderC.Read
                Interessi.Add(myReaderC("anno"), myReaderC("tasso"))
            Loop
            myReaderC.Close()

            DataCalcolo = Format(Now, "yyyyMMdd") 'par.AggiustaData(dataRicons.Value)

            baseCalcolo = par.IfEmpty(impCauzione, 0)
            If baseCalcolo > 0 And interessiCauz = True Then

                par.cmd.CommandText = "select * from siscom_mi.adeguamento_interessi where id_contratto=" & idContratto.Value & " order by id desc"
                Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderZ.HasRows = False Then
                    DataInizio = dataDecorrenza
                End If
                If myReaderZ.Read Then
                    DataInizio = Format(DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(myReaderZ("data")))), "yyyyMMdd")
                End If
                myReaderZ.Close()

                If DataInizio < "20091001" Then
                    DataInizio = "20091001"
                End If

                Giorni = 0
                GiorniAnno = 0
                dataPartenza = DataInizio
                Totale = 0
                TotalePeriodo = 0

                par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi (id,id_contratto,data,fl_applicato) values (siscom_mi.seq_adeguamento_interessi.nextval," & idContratto.Value & ",'" & DataCalcolo & "',1)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select siscom_mi.seq_adeguamento_interessi.currval from dual"
                myReaderZ = par.cmd.ExecuteReader()
                indice = 0
                If myReaderZ.Read Then
                    indice = myReaderZ(0)
                End If

                For I = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                    If I = CInt(Mid(DataCalcolo, 1, 4)) Then
                        DataFine = par.FormattaData(DataCalcolo)
                    Else
                        DataFine = "31/12/" & I

                    End If

                    GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & I), CDate("31/12/" & I)) + 1

                    Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                    If I < 1990 Then
                        tasso = 5
                    Else
                        If Interessi.ContainsKey(I) = True Then
                            tasso = Interessi(I)
                        End If
                    End If

                    TotalePeriodo = Format((((baseCalcolo * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                    Totale = Totale + TotalePeriodo



                    par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi_voci (id_adeguamento,dal,al,giorni,tasso,importo) values (" & indice & ",'" & dataPartenza & "','" & Format(CDate(DataFine), "yyyyMMdd") & "'," & Giorni & "," & par.VirgoleInPunti(tasso) & "," & par.VirgoleInPunti(TotalePeriodo) & ")"
                    par.cmd.ExecuteNonQuery()

                    dataPartenza = I + 1 & "0101"

                Next
                par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set importo=" & par.VirgoleInPunti(Format(Totale, "0.00")) & " where id=" & indice
                par.cmd.ExecuteNonQuery()
            End If

            'Ricavo Canone Annuo
            Dim canoneAnnuo As Decimal = 0
            Dim areaEconomica As Integer = 0
            par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_calcolo desc"
            Dim myReaderEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderEC.Read Then
                canoneAnnuo = par.IfNull(myReaderEC("CANONE"), "0")
                areaEconomica = par.IfNull(myReaderEC("ID_AREA_ECONOMICA"), "0")
            End If
            myReaderEC.Close()

            If areaEconomica = 4 Or txtDataDisdetta.Text = Format(Now, "dd/MM/yyyy") Or txtDataDisdetta.Text = "" Then
                par.cmd.CommandText = "select imp_canone_iniziale from siscom_mi.rapporti_utenza where id=" & idContratto.Value
                Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCC.Read Then
                    canoneAnnuo = par.IfNull(myReaderCC("imp_canone_iniziale"), 0)
                End If
                myReaderCC.Close()
            End If

            par.cmd.CommandText = "select sum(importo) as TotInteressi from siscom_mi.adeguamento_interessi where id_contratto=" & idContratto.Value & " and fl_applicato=0 order by id desc"
            Dim myReaderZ2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderZ2.Read Then
                Totale = Totale + par.IfNull(myReaderZ2("TotInteressi"), 0)
            End If
            myReaderZ2.Close()

            Dim DataScadenza As String = DateAdd("d", 10, Now)
            Dim TOTALE_BOLLETTA As Double = 0

            par.cmd.CommandText = "select anagrafica.id as ida,anagrafica.cognome,anagrafica.nome,ANAGRAFICA.RAGIONE_SOCIALE,RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & idContratto.Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderS.Read Then


                Dim PRESSO As String = UCase(Trim(par.IfNull(myReaderS("PRESSO_COR"), "")))
                Dim Cognome As String = ""
                Dim Nome As String = ""

                If par.IfNull(myReaderS("ragione_sociale"), "") <> "" Then
                    Cognome = par.IfNull(myReaderS("ragione_sociale"), "")
                    Nome = ""
                Else
                    Cognome = par.IfNull(myReaderS("cognome"), "")
                    Nome = par.IfNull(myReaderS("nome"), "")
                End If

                If UCase(Trim(Cognome & " " & Nome)) = PRESSO Then
                    PRESSO = ""
                End If
                INDICEANAGRAFICA = par.IfNull(myReaderS("IDA"), 0)
                par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                                                & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                                & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                                                & "Values " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & par.AggiustaData(dataRicons.Value) _
                                                & "', '" & par.AggiustaData(dataRicons.Value) & "', NULL,NULL,NULL,'BOLLETTA FINE CONTRATTO'," _
                                                & "" & idContratto.Value _
                                                & " ," & par.RicavaEsercizioCorrente & ", " _
                                                & UnitaContratto _
                                                & ", '0', '', " & par.IfNull(myReaderS("IDA"), 0) _
                                                & ", '" & par.PulisciStrSql(Trim(Cognome & " " & Nome)) & "', " _
                                                & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                                                & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                                                & "', '" & par.PulisciStrSql(PRESSO) & "', '" & par.AggiustaData(dataRicons.Value) _
                                                & "', '" & par.AggiustaData(dataRicons.Value) & "', " _
                                                & "'0', " & myReaderS("ID_COMPLESSO") & ", '', NULL, '', " _
                                                & Year(Now) & ", '', " & myReaderS("ID_EDIFICIO") & ", NULL, NULL,'FIN',3)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    Dim ID_BOLLETTA As Long = myReaderA(0)

                    If importoRisoluzione > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                            & ",35" _
                                            & "," & par.VirgoleInPunti(importoRisoluzione) & ")"
                        RIASSUNTO_BOLLETTA = "Risoluzione: " & par.VirgoleInPunti(importoRisoluzione) & " Euro;\n"
                        par.cmd.ExecuteNonQuery()

                        TOTALE_BOLLETTA = importoRisoluzione
                    End If

                    If Totale > 0 And interessiCauz = True Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,note) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                            & ",15" _
                                            & "," & par.VirgoleInPunti(Format(Totale * -1, "0.00")) & ",'Dal " & par.FormattaData(DataInizio) & "')"
                        par.cmd.ExecuteNonQuery()
                        RIASSUNTO_BOLLETTA = RIASSUNTO_BOLLETTA & "Rimborso Interessi Cauzione: " & par.VirgoleInPunti(Format(Totale * -1, "0.00")) & " Euro;\n"
                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + (Totale * -1)
                    End If

                    If importodanni > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                            & ",100" _
                                            & "," & par.VirgoleInPunti(importodanni) & ")"
                        RIASSUNTO_BOLLETTA = "Risoluzione: " & par.VirgoleInPunti(importodanni) & " Euro;\n"
                        par.cmd.ExecuteNonQuery()

                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + importodanni
                    End If

                    If importotrasporto > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                            & ",101" _
                                            & "," & par.VirgoleInPunti(importotrasporto) & ")"
                        RIASSUNTO_BOLLETTA = "Risoluzione: " & par.VirgoleInPunti(importotrasporto) & " Euro;\n"
                        par.cmd.ExecuteNonQuery()

                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + importotrasporto
                    End If



                    Dim Mesi1 As Integer = 0
                    Dim Giorni2 As Integer = 0

                    Dim BOLLO As Double = 0
                    Dim APPLICABOLLO As Double = 0

                    Dim PARTENZA As String = ""
                    Dim FINE As String = ""


                    par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    End If
                    myReaderA.Close()

                    Dim SPESEPOSTALI As Double = 0
                    Dim SPESEPOSTALI_CAPOLUOGHI As Double = 0
                    Dim SPESEPOSTALI_ALTRI As Double = 0
                    Dim SPESEPOSTALI_DA_APPLICARE As Double = 0
                    Dim giorniCanone As Integer = 0

                    par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=29"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        SPESEPOSTALI_CAPOLUOGHI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=30"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        SPESEPOSTALI_ALTRI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                    End If
                    myReaderA.Close()

                    If UCase(luogoCor) = "MILANO" Then
                        SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI
                    Else
                        If InStr(CaricaCapoluoghi, UCase(luogoCor)) > 0 Then
                            SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI_CAPOLUOGHI
                        Else
                            SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI_ALTRI
                        End If
                    End If


                    par.cmd.CommandText = "select * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_BOLLETTA_STORNO IS NULL AND FL_ANNULLATA='0' AND N_RATA<>999 AND N_RATA<>99999 AND ID_CONTRATTO=" & idContratto.Value & " ORDER BY RIFERIMENTO_A DESC"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        PARTENZA = myReaderA("RIFERIMENTO_A")
                    End If
                    myReaderA.Close()

                    'Cerco ultima bolletta per prendere la voce canone
                    Dim idVoceCanone As Integer = 1
                    If areaEconomica = 4 Or txtDataDisdetta.Text = Format(Now, "dd/MM/yyyy") Or txtDataDisdetta.Text = "" Then
                    par.cmd.CommandText = " select id_voce from siscom_mi.bol_bollette_voci where id_bolletta in ( " _
                            & " SELECT max(bol_bollette.id) " _
                            & " FROM siscom_mi.bol_bollette_voci, siscom_mi.bol_bollette " _
                            & " WHERE bol_bollette_voci.id_bolletta = bol_bollette.id " _
                            & " AND id_tipo = 1 " _
                            & " AND n_rata BETWEEN 1 AND 10 " _
                            & " AND riferimento_da = " _
                            & " (SELECT MAX (riferimento_da) " _
                            & " FROM siscom_mi.bol_bollette bb " _
                            & " WHERE  bb.id = bol_bollette.id " _
                            & " AND id_tipo = 1 " _
                            & " AND n_rata BETWEEN 1 AND 10 " _
                            & " AND bb.id_contratto = bol_bollette.id_contratto) " _
                            & " AND id_contratto =" & idContratto.Value & ")"
                    Dim daBV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtBV As New Data.DataTable
                    daBV.Fill(dtBV)
                    daBV.Dispose()
                    If dtBV.Rows.Count > 0 Then
                        For Each rowBV As Data.DataRow In dtBV.Rows
                            If par.IfNull(rowBV.Item("id_voce"), "0") = 1 Or par.IfNull(rowBV.Item("id_voce"), "0") = 36 Then
                                idVoceCanone = par.IfNull(rowBV.Item("id_voce"), "0")
                                Exit For
                            End If
                        Next
                        End If
                    End If

                    FINE = par.AggiustaData(dataRicons.Value)

                    Dim GiorniUltimoMese As Integer = 0
                    If PARTENZA <> "" And FINE <> "" Then

                        Mesi1 = DateDiff("m", CDate(par.FormattaData(PARTENZA)), CDate(par.FormattaData(FINE)))


                        If Mesi1 > 0 Then
                            'calcolo l'importo dell'affitto in base al numero di rate annue

                            Dim affitto As Double = 0
                            Select Case Mid(par.FormattaData(PARTENZA), 4, 2)
                                Case "01", "03", "05", "07", "08", "10", "12"
                                    GiorniUltimoMese = DateDiff("d", par.FormattaData(PARTENZA), "31" & Mid(par.FormattaData(PARTENZA), 3, 8))
                                Case "02"
                                    GiorniUltimoMese = DateDiff("d", par.FormattaData(PARTENZA), "28" & Mid(par.FormattaData(PARTENZA), 3, 8)) + 3
                                Case Else
                                    GiorniUltimoMese = DateDiff("d", par.FormattaData(PARTENZA), "30" & Mid(par.FormattaData(PARTENZA), 3, 8)) + 1
                            End Select

                            giorniCanone = GiorniUltimoMese + 30 * DateDiff("m", DateAdd(DateInterval.Month, 1, CDate(par.FormattaData(PARTENZA))), CDate(par.FormattaData(FINE)))
                            giorniCanone = giorniCanone + DateDiff("d", CDate("01/" & Mid(FINE, 5, 2) & "/" & Mid(FINE, 1, 4)), CDate(par.FormattaData(FINE)))

                            affitto = Format((canoneAnnuo / 360) * giorniCanone, "0.00")



                            If par.IfNull(canoneAnnuo, 0) > 0 Then
                                'affitto = Format((par.IfNull(canoneAnnuo, 1) / 365) * (Mesi1), "0.00")
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + (par.IfNull(canoneAnnuo, 1) / 360) * (giorniCanone)

                                If par.IfNull(myReaderS("PROVENIENZA_ASS"), "") <> "7" Then
                                    'If FINE = par.IfNull(myReaderS("data_scadenza_rinnovo"), "") Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & idVoceCanone _
                                                        & "," & par.VirgoleInPunti(affitto) & ")"
                                    par.cmd.ExecuteNonQuery()
                                    'Else
                                    '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                    '                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",1" _
                                    '                        & "," & par.VirgoleInPunti(affitto) & ")"
                                    '    par.cmd.ExecuteNonQuery()
                                    'End If
                                Else
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",560" _
                                        & "," & par.VirgoleInPunti(affitto) & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If


                            End If
                        End If
                    End If

                    If Mesi1 > 0 Then
                        Dim aggiornamento_istat As Double = 0
                        Dim AltriAdeguamenti As Double = 0

                        par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo=2 and ID_CONTRATTO=" & idContratto.Value
                        myReaderA = par.cmd.ExecuteReader()
                        If myReaderA.Read Then
                            aggiornamento_istat = par.IfNull(myReaderA(0), 0)
                        End If
                        myReaderA.Close()

                        par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo<>2 and ID_CONTRATTO=" & idContratto.Value
                        myReaderA = par.cmd.ExecuteReader()
                        If myReaderA.Read Then
                            AltriAdeguamenti = par.IfNull(myReaderA(0), 0)
                        End If
                        myReaderA.Close()

                        If aggiornamento_istat > 0 Then

                            TOTALE_BOLLETTA = TOTALE_BOLLETTA + Format((aggiornamento_istat / 360) * giorniCanone, "0.00")
                            aggiornamento_istat = (aggiornamento_istat / 360) * giorniCanone

                            If FINE = par.IfNull(myReaderS("data_scadenza_rinnovo"), "") Or par.IfNull(myReaderS("PROVENIENZA_ASS"), "") = "7" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",405" _
                                                    & "," & par.VirgoleInPunti(Format(aggiornamento_istat, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",404" _
                                                    & "," & par.VirgoleInPunti(Format(aggiornamento_istat, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If

                        If AltriAdeguamenti > 0 Then

                            TOTALE_BOLLETTA = TOTALE_BOLLETTA + Format((AltriAdeguamenti / 360) * giorniCanone, "0.00")
                            AltriAdeguamenti = (AltriAdeguamenti / 360) * giorniCanone


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",652" _
                                                & "," & par.VirgoleInPunti(Format(AltriAdeguamenti, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()

                        End If

                        par.cmd.CommandText = "select bol_schema.*,t_voci_bolletta.descrizione from siscom_mi.bol_schema,siscom_mi.t_voci_bolletta where anno=" & Year(Now) & " and da_rata=1 And per_rate=12 AND t_voci_bolletta.id=bol_schema.id_voce and  bol_schema.id_contratto=" & idContratto.Value
                        myReaderA = par.cmd.ExecuteReader()
                        Do While myReaderA.Read

                            TOTALE_BOLLETTA = TOTALE_BOLLETTA + CDbl((myReaderA("importo") / 360) * giorniCanone)

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & myReaderA("ID_VOCE") _
                                                & "," & par.VirgoleInPunti(Format((myReaderA("importo") / 360) * giorniCanone, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()


                        Loop
                        myReaderA.Close()


                        Dim TotMorosita As Double = 0
                        Dim TotMorositaSB As Double = 0

                        If TotMorosita > 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                           & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",151" _
                                           & "," & par.VirgoleInPunti(Format(TotMorosita, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                            TOTALE_BOLLETTA = TOTALE_BOLLETTA + TotMorosita
                        End If


                    End If

                    If SPESEmav > 0 And TOTALE_BOLLETTA > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                        & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()

                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + SPESEmav
                    End If

                    If TOTALE_BOLLETTA > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",403" _
                                            & "," & par.VirgoleInPunti(Format(SPESEPOSTALI_DA_APPLICARE, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + SPESEPOSTALI_DA_APPLICARE
                    End If

                    If TOTALE_BOLLETTA > APPLICABOLLO Then
                        If BOLLO > 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    If TOTALE_BOLLETTA <= SPESEmav And TOTALE_BOLLETTA > 0 Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & ID_BOLLETTA
                        par.cmd.ExecuteNonQuery()
                        RIASSUNTO_BOLLETTA = ""
                    End If

                End If

            End If
            myReaderS.Close()

            'MAX 15/12/2015 SCRITTURA GEST. DEPOSITO CAUZIONALE
            If par.IfEmpty(impCauzione, 0) > 0 Then
                Dim ID_BOLLETTA_GEST As Long = 0
                par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA, RIFERIMENTO_DA, RIFERIMENTO_A, IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO, TIPO_APPLICAZIONE, DATA_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE, ID_EVENTO_PAGAMENTO) " _
                                    & " Values " _
                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL, " & idContratto.Value & ", " & par.RicavaEsercizioCorrente & ", " & UnitaContratto & "," _
                                    & INDICEANAGRAFICA & ", '" & DataInizio & "', '" & Format(Now, "yyyyMMdd") _
                                    & "', " & par.VirgoleInPunti(-1 * par.IfEmpty(impCauzione, 0)) & ", '" & Format(Now, "yyyyMMdd") & "',NULL, NULL, 55, 'N', NULL, " _
                                    & "'', '" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE") & "', NULL)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    ID_BOLLETTA_GEST = myReaderX(0)
                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) Values  (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & ID_BOLLETTA_GEST & ",7, " & par.VirgoleInPunti(par.IfEmpty(impCauzione, 0) * -1) & ",  NULL, NULL)"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO OPERAZIONI_PART_GEST (DATA_ORA,ID_CONTRATTO,OPERAZIONE,NOTE,ID_BOLLETTA,ID_BOLLETTA_GEST) VALUES ('" & Format(Now, "yyyyMMddHHmmss") & "'," & idContratto.Value & ",'RIMBORSO DEPOSITO CAUZIONALE','CREATA IN P.GEST. BOLLETTA NUM." & par.PulisciStrSql(ID_BOLLETTA_GEST) & " PER RIMBORSO DEP. CAUZIONALE',NULL," & ID_BOLLETTA_GEST & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F02', 'CREATA IN P.GEST. BOLLETTA RIMBORSO DEPOSITO CAUZIONE NUM." & ID_BOLLETTA_GEST & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderX.Close()
            End If


            'INSERIMENTO AVVISO PER CONDOMINI
            par.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & UnitaContratto & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (1," & UnitaContratto & ",'" & Format(Now, "yyyyMMdd") & "',0," & myReader("ID_CONDOMINIO") & "," & idContratto.Value & ")"
                par.cmd.ExecuteNonQuery()
            Loop
            myReader.Close()

            'If Not IsNothing(Session.Item("lIdConnessione")) Then
            '    Dim par1 As New CM.Global
            '    par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
            '    par1.cmd = par1.OracleConn.CreateCommand()
            '    par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)

            '    par1.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET" _
            '   & " data_riconsegna='" & Format(Now, "yyyyMMdd") & "'" _
            '   & " WHERE ID=" & idContratto.Value
            '    par1.cmd.ExecuteNonQuery()

            '    par1.myTrans.Commit()
            '    par1.myTrans = par1.OracleConn.BeginTransaction()
            '    HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
            '    par1.Dispose()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End If

            connData.chiudi(True)
        Else
            Response.Write("<script>alert('Il contratto si può considerare chiuso solo se è presente una data di sloggio!');</script>")
        End If



    End Sub

    Protected Sub btnChiudiContr_Click(sender As Object, e As System.EventArgs) Handles btnChiudiContr.Click
        Try
            btnChiudiContr.Visible = False
            ChiusuraContratto()
            imgVisto2.Visible = True
            lblMsgData.Text = ""
            lblMsgData.Style.Value = "display:none;"
            imgRight.Visible = False
            'If lblRicalcolo.Visible = True Then
            '    lblMsgInfo.Visible = True
            '    imgAlert.Visible = True
            'End If
            lblCrea.Font.Bold = False
            btnChiudiContr.Visible = False
            btnChiudiMaschera.Visible = True
            lblMsgAttesa.Visible = True
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
