Imports System.IO

Partial Class CanoniPreReca
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Public Property dataFine() As String
        Get
            If Not (ViewState("par_dataFine") Is Nothing) Then
                Return CStr(ViewState("par_dataFine"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataFine") = value
        End Set

    End Property

    Protected Sub btnRistampa_Click(sender As Object, e As System.EventArgs) Handles btnRistampa.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim percorso As String = Server.MapPath("FileTemp")
            Dim FileName As String = ""
            If FileUpload1.HasFile = True Then
                FileName = percorso & "\" & FileUpload1.FileName

                FileUpload1.SaveAs(FileName)
                If System.IO.File.Exists(FileName) = True Then
                    Dim sr = New StreamReader(FileName)
                    Dim pgAU As String = ""
                    Do
                        pgAU = sr.ReadLine
                        If pgAU <> "" Then
                            par.cmd.CommandText = "SELECT ID AS ID_DOM,CONTRATTO_NUM,ID_DICHIARAZIONE AS ID_DICH,ID_D_IMPORT FROM DOMANDE_BANDO_VSA WHERE PG ='" & pgAU & "'"
                            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt1 As New Data.DataTable
                            da1.Fill(dt1)
                            da1.Dispose()
                            If dt1.Rows.Count > 0 Then
                                For Each rowDT In dt1.Rows
                                    RicavaSituazionePRE(rowDT.item("CONTRATTO_NUM"), rowDT.item("ID_DOM"), rowDT.item("ID_DICH"), rowDT.item("ID_D_IMPORT"))
                                Next
                            End If
                        End If
                    Loop Until pgAU Is Nothing
                    sr.Close()

                    par.myTrans.Commit()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                Else
                    Response.Write("<script>alert('File non trovato!')</script>")
                End If
                Response.Write("<script>alert('Operazione effettuata!')</script>")
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub RicavaSituazionePREold(ByVal codContratto As String, ByVal new_id_dom As Long, ByVal new_idDichia As Long, ByVal ID_D_IMPORT As Long)

        Dim IMPORTO As Double
        Dim NuovoTransit As Double
        Dim LOCATIVO As String = ""
        Dim comunicazioni As String = ""
        Dim AreaEconomica As Integer
        Dim sISEE As String = ""
        Dim sISE As String = ""
        Dim sISR As String = ""
        Dim sISP As String = ""
        Dim sVSE As String = ""
        Dim sREDD_DIP As String = ""
        Dim sREDD_ALT As String = ""
        Dim sLimitePensione As String = ""
        Dim sPER_VAL_LOC As String = ""
        Dim sPERC_INC_MAX_ISE_ERP As String = ""
        Dim sCANONE_MIN As String = ""
        Dim sISE_MIN As String = ""
        Dim sCanone As String = ""
        Dim sNOTE As String = ""
        Dim sDEM As String = ""
        Dim sSUPCONVENZIONALE As String = ""
        Dim sCOSTOBASE As String = ""
        Dim sZONA As String = ""
        Dim sPIANO As String = ""
        Dim sCONSERVAZIONE As String = ""
        Dim sVETUSTA As String = ""
        Dim sPSE As String = ""
        Dim sINCIDENZAISE As String = ""
        Dim sCOEFFFAM As String = ""
        Dim sSOTTOAREA As String = ""
        Dim sMOTIVODECADENZA As String = ""
        Dim sNUMCOMP As String = ""
        Dim sNUMCOMP66 As String = ""
        Dim sNUMCOMP100 As String = ""
        Dim sNUMCOMP100C As String = ""
        Dim sPREVDIP As String = ""
        Dim sDETRAZIONI As String = ""
        Dim sMOBILIARI As String = ""
        Dim sIMMOBILIARI As String = ""
        Dim sCOMPLESSIVO As String = ""
        Dim sDETRAZIONEF As String = ""
        Dim sANNOCOSTRUZIONE As String = ""
        Dim sLOCALITA As String = ""
        Dim sASCENSORE As String = ""
        Dim sDESCRIZIONEPIANO As String = ""
        Dim sSUPNETTA As String = ""
        Dim sALTRESUP As String = ""
        Dim sMINORI15 As String = ""
        Dim sMAGGIORI65 As String = ""
        Dim sSUPACCESSORI As String = ""
        Dim sVALORELOCATIVO As String = ""
        Dim sCANONESOPP As String = ""
        Dim sVALOCIICI As String = ""
        Dim sALLOGGIOIDONEO As String = ""
        Dim sISTAT As String = ""
        Dim sCANONECLASSE As String = ""
        Dim sCANONECLASSEISTAT As String = ""
        Dim sANNOINIZIOVAL As String = ""
        Dim sANNOFINEVAL As String = ""
        Dim parte1 As String = ""
        Dim parte2 As String = ""
        Dim parte3 As String = ""
        Dim parte4 As String = ""
        Dim IDdich As String = ""
        Dim dataInizioValidita As String = ""
        Dim I As Integer
        Dim Prov As Integer
        Dim IDUNITA As Long
        Dim ANNO_INIZIO As Integer = 0
        Dim PER_ANNI As Integer = 0
        Dim CanonePREreca As Decimal = 0
        Dim idDichCan_EC As Long = 0
        Dim idDOMCan_EC As Long = 0
        'Dim istat2009 As String = "2,025"
        Dim importoTrovato As Boolean = True
        Dim parte2new As String = ""
        Dim parte3new As String = ""
        Dim ID_AU As Long
        Dim annotazioni As String = ""
        Dim idContratto As Long = 0
        Dim canoneIniziale As Decimal = 0
        Dim tipoContrattoLoc As String = ""
        Dim id_dom As Long = 0

        Prov = 0

        'par.OracleConn.Open()
        'par.SettaCommand(par)

        par.cmd.CommandText = "DELETE FROM CANONI_PRE_RECA WHERE ID_DOMANDA=" & new_id_dom
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & ID_D_IMPORT
        Dim myReaderX0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderX0.Read Then
            id_dom = par.IfNull(myReaderX0("id"), 0)
        Else
            par.cmd.CommandText = "SELECT ID FROM UTENZA_DICHIARAZIONI WHERE ID=" & ID_D_IMPORT
            Dim myReaderX01 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX01.Read Then
                id_dom = par.IfNull(myReaderX01("id"), 0)
            End If
            myReaderX01.Close()
        End If
        myReaderX0.Close()

        par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID,ID_UNITA,RAPPORTI_UTENZA.ID_AU,RAPPORTI_UTENZA.IMP_CANONE_INIZIALE,COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.unita_immobiliari WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND unita_contrattuale.id_unita = unita_immobiliari.ID AND unita_immobiliari.id_unita_principale IS NULL AND COD_CONTRATTO='" & codContratto & "'"
        Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderX1.Read Then
            IDUNITA = myReaderX1("ID_UNITA")
            ID_AU = par.IfNull(myReaderX1("ID_AU"), 0)
            idContratto = par.IfNull(myReaderX1("ID"), -1)
            canoneIniziale = par.IfNull(myReaderX1("IMP_CANONE_INIZIALE"), 0)
            tipoContrattoLoc = par.IfNull(myReaderX1("COD_TIPOLOGIA_CONTR_LOC"), "")
        End If
        myReaderX1.Close()

        If IDUNITA <> 0 Then

            par.cmd.CommandText = "SELECT DATA_EVENTO,DATA_INIZIO_VAL,DATA_FINE_VAL,DICHIARAZIONI_VSA.ID_STATO,DICHIARAZIONI_VSA.ID,DOMANDE_BANDO_VSA.REDDITI_PRE_RECA FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID=" & new_id_dom
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                ANNO_INIZIO = CInt(Mid(par.IfNull(myReader("DATA_INIZIO_VAL"), Year(Now)), 1, 4))

                dataInizioValidita = par.IfNull(myReader("DATA_INIZIO_VAL"), "")
                IDdich = par.IfNull(myReader("ID"), "")

                If par.IfNull(myReader("DATA_FINE_VAL"), "") = "29991231" Then
                    dataFine = Year(Now) & "1231"
                Else
                    dataFine = par.IfNull(myReader("DATA_FINE_VAL"), "")
                End If
                PER_ANNI = DateDiff(DateInterval.Year, CDate(par.FormattaData(myReader("DATA_INIZIO_VAL"))), CDate(par.FormattaData(dataFine)))

                'ANNICONGUAGLIO = ANNO_INIZIO + PER_ANNI

                'If causale.Value = "28" Then
                '    ANNICONGUAGLIO = Year(Now)
                'End If
            End If
            myReader.Close()

            For I = ANNO_INIZIO To ANNO_INIZIO + PER_ANNI
                CanonePREreca = 0
                parte2 = ""
                parte3 = ""
                parte4 = ""

                If id_dom <> 0 And I = ANNO_INIZIO Then
                    par.CalcolaCanone27RECA(id_dom, Prov, IDUNITA, codContratto, IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                    parte3new = parte3
                    parte2new = parte2
                End If

                LOCATIVO = ""
                comunicazioni = ""

                sISEE = ""
                sISE = ""
                sISR = ""
                sISP = ""
                sVSE = ""
                sREDD_DIP = ""
                sREDD_ALT = ""
                sLimitePensione = ""
                sPER_VAL_LOC = ""
                sPERC_INC_MAX_ISE_ERP = ""
                sCANONE_MIN = ""
                sISE_MIN = ""
                sCanone = ""
                sNOTE = ""
                sDEM = ""
                sSUPCONVENZIONALE = ""
                sCOSTOBASE = ""
                sZONA = ""
                sMOTIVODECADENZA = ""

                If I = 2008 Or I = 2009 Then
                    par.cmd.CommandText = "SELECT * from SISCOM_MI.RAPPORTI_UTENZA_EXTRA where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "')"
                    Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRX.Read Then
                        parte4 = vbCrLf & vbCrLf & "CANONE ANNO " & I & vbCrLf

                        CanonePREreca = par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0)
                        'TOLTO COME DA ISTRUZIONI 11/06/2012
                        'If I = 2009 Then
                        '    If par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") >= 12 And par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") < 27 Then
                        '        CanonePREreca = CanonePREreca + ((CanonePREreca * CDbl(istat2009)) / 100)
                        '    End If
                        'End If
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(CanonePREreca), "##,##0.00")
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0) / 12), "##,##0.00")
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE TRANSITORIO:..........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_TRANSITORIO"), 0)), "##,##0.00")
                        If I = 2008 Then
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE GRADUATO " & I & ":........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_PRIMO_ANNO"), 0)), "##,##0.00")
                        Else
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE GRADUATO " & I & ":........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_SECONDO_ANNO"), 0)), "##,##0.00")
                        End If
                    Else
                        importoTrovato = False
                    End If
                    myReaderRX.Close()

                    If I <= 2012 Then
                        par.cmd.CommandText = "SELECT CANONE_COMPETENZA_" & I & " FROM SISCOM_MI.ELABORAZIONE_CONGUAGLI WHERE COD_CONTRATTO='" & codContratto & "'"
                        Dim myReaderCanone As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCanone.Read Then
                            If par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0) <> 0 Then
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0)), "##,##0.00")
                            End If
                        End If
                        myReaderCanone.Close()
                    End If

                End If


                If I = 2010 Or I = 2011 Then
                    'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 ORDER BY DATA_CALCOLO DESC"
                    par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "') and (TIPO_PROVENIENZA=1 OR TIPO_PROVENIENZA=2 OR TIPO_PROVENIENZA=5 OR TIPO_PROVENIENZA=6) AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' ORDER BY DATA_CALCOLO DESC"
                    Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRX.HasRows = True Then


                        If myReaderRX.Read Then
                            parte4 = ""
                            idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                            If idDichCan_EC <> 0 Then
                                If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 2 Then
                                    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, codContratto, IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                Else
                                    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderID.Read Then
                                        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                    End If
                                    myReaderID.Close()

                                    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, codContratto, IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                    & "525,10001,10002,30003,530," _
                                    & "30075,1,10072,10087,10125," _
                                    & "10135,20003,20019,20020," _
                                    & "20023,20096,20097,553," _
                                    & "10075,10128,20021,10127," _
                                    & "10126,512,10074,534,10073," _
                                    & "604,30071,603,30068,506," _
                                    & "647,653,599,648,30080,622," _
                                    & "30123,30124,508,10160,509," _
                                    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderComp.Read Then
                                        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                    End If
                                    myReaderComp.Close()

                                End If
                                CanonePREreca = IMPORTO
                            Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                par.cmd.CommandText = "SELECT CANONE_COMPETENZA_" & I & " FROM SISCOM_MI.ELABORAZIONE_CONGUAGLI WHERE COD_CONTRATTO='" & codContratto & "'"
                                Dim myReaderCanone As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderCanone.Read Then
                                    If par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0) <> 0 Then
                                        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0)), "##,##0.00")
                                    End If
                                End If
                                myReaderCanone.Close()
                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                            End If
                        Else
                            'parte4 = ""
                            'If ID_AU <> 0 Then
                            '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)
                            '    CanonePREreca = IMPORTO
                            'Else
                            '    importoTrovato = False
                            'End If
                        End If
                    Else
                        CanonePREreca = canoneIniziale

                        parte4 = ""
                        parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                        'If ID_AU <> 0 Then
                        par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                            & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                            & "525,10001,10002,30003,530," _
                            & "30075,1,10072,10087,10125," _
                            & "10135,20003,20019,20020," _
                            & "20023,20096,20097,553," _
                            & "10075,10128,20021,10127," _
                            & "10126,512,10074,534,10073," _
                            & "604,30071,603,30068,506," _
                            & "647,653,599,648,30080,622," _
                            & "30123,30124,508,10160,509," _
                            & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                            & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                            & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                        Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderComp.Read Then
                            parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                        End If
                        myReaderComp.Close()

                        'par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                        'Else
                        'importoTrovato = False
                        'End If

                    End If
                    myReaderRX.Close()
                End If



                If I = 2012 Or I = 2013 Then
                    'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 ORDER BY DATA_CALCOLO DESC"
                    par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "') and (TIPO_PROVENIENZA=1 OR TIPO_PROVENIENZA=2 OR TIPO_PROVENIENZA=5 OR TIPO_PROVENIENZA=6) AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' ORDER BY DATA_CALCOLO DESC"
                    Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRX.HasRows = True Then
                        If myReaderRX.Read Then
                            parte4 = ""
                            idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                            If idDichCan_EC <> 0 Then
                                If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 5 Then
                                    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, codContratto, IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                Else
                                    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderID.Read Then
                                        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                    End If
                                    myReaderID.Close()
                                    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, codContratto, IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                    & "525,10001,10002,30003,530," _
                                    & "30075,1,10072,10087,10125," _
                                    & "10135,20003,20019,20020," _
                                    & "20023,20096,20097,553," _
                                    & "10075,10128,20021,10127," _
                                    & "10126,512,10074,534,10073," _
                                    & "604,30071,603,30068,506," _
                                    & "647,653,599,648,30080,622," _
                                    & "30123,30124,508,10160,509," _
                                    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderComp.Read Then
                                        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                    End If
                                    myReaderComp.Close()
                                End If
                                CanonePREreca = IMPORTO
                                parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                            Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If I <= 2012 Then
                                    par.cmd.CommandText = "SELECT CANONE_COMPETENZA_" & I & " FROM SISCOM_MI.ELABORAZIONE_CONGUAGLI WHERE COD_CONTRATTO='" & codContratto & "'"
                                    Dim myReaderCanone As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderCanone.Read Then
                                        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0)), "##,##0.00")
                                    End If
                                    myReaderCanone.Close()
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                            End If
                        Else
                            'parte4 = ""
                            'If ID_AU <> 0 Then
                            '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                            '    CanonePREreca = IMPORTO
                            '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                            'Else
                            '    importoTrovato = False
                            'End If
                        End If
                    Else
                        CanonePREreca = canoneIniziale
                        parte4 = ""
                        parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                        'If ID_AU <> 0 Then
                        'par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                        par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                            & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                            & "525,10001,10002,30003,530," _
                            & "30075,1,10072,10087,10125," _
                            & "10135,20003,20019,20020," _
                            & "20023,20096,20097,553," _
                            & "10075,10128,20021,10127," _
                            & "10126,512,10074,534,10073," _
                            & "604,30071,603,30068,506," _
                            & "647,653,599,648,30080,622," _
                            & "30123,30124,508,10160,509," _
                            & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                            & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                            & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                        Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderComp.Read Then
                            parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                        End If
                        myReaderComp.Close()

                        'Else
                        'importoTrovato = False
                        'End If
                    End If
                    myReaderRX.Close()
                End If

                If I = 2014 Or I = 2015 Then
                    'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 ORDER BY DATA_CALCOLO DESC"
                    par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "') and (TIPO_PROVENIENZA=1 OR TIPO_PROVENIENZA=2 OR TIPO_PROVENIENZA=5 OR TIPO_PROVENIENZA=6 OR TIPO_PROVENIENZA=8) AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' ORDER BY DATA_CALCOLO DESC"
                    Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRX.HasRows = True Then
                        If myReaderRX.Read Then
                            parte4 = ""
                            idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                            If idDichCan_EC <> 0 Then
                                If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 8 Then
                                    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, codContratto, IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                Else
                                    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderID.Read Then
                                        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                    End If
                                    myReaderID.Close()
                                    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, codContratto, IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                    & "525,10001,10002,30003,530," _
                                    & "30075,1,10072,10087,10125," _
                                    & "10135,20003,20019,20020," _
                                    & "20023,20096,20097,553," _
                                    & "10075,10128,20021,10127," _
                                    & "10126,512,10074,534,10073," _
                                    & "604,30071,603,30068,506," _
                                    & "647,653,599,648,30080,622," _
                                    & "30123,30124,508,10160,509," _
                                    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderComp.Read Then
                                        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                    End If
                                    myReaderComp.Close()
                                End If
                                CanonePREreca = IMPORTO
                                parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                            Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If I <= 2012 Then
                                    par.cmd.CommandText = "SELECT CANONE_COMPETENZA_" & I & " FROM SISCOM_MI.ELABORAZIONE_CONGUAGLI WHERE COD_CONTRATTO='" & codContratto & "'"
                                    Dim myReaderCanone As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderCanone.Read Then
                                        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0)), "##,##0.00")
                                    End If
                                    myReaderCanone.Close()
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                            End If
                        Else
                            'parte4 = ""
                            'If ID_AU <> 0 Then
                            '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                            '    CanonePREreca = IMPORTO
                            '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                            'Else
                            '    importoTrovato = False
                            'End If
                        End If
                    Else
                        CanonePREreca = canoneIniziale
                        parte4 = ""
                        parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                        parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                        'If ID_AU <> 0 Then
                        '''''''''par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                        par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                            & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                            & "525,10001,10002,30003,530," _
                            & "30075,1,10072,10087,10125," _
                            & "10135,20003,20019,20020," _
                            & "20023,20096,20097,553," _
                            & "10075,10128,20021,10127," _
                            & "10126,512,10074,534,10073," _
                            & "604,30071,603,30068,506," _
                            & "647,653,599,648,30080,622," _
                            & "30123,30124,508,10160,509," _
                            & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                            & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                            & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                        Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderComp.Read Then
                            parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                        End If
                        myReaderComp.Close()

                        'Else
                        'importoTrovato = False
                        'End If
                    End If
                    myReaderRX.Close()
                End If

                Dim numparte As String = ""
                Dim testo As String = ""
                For j As Integer = 0 To 3
                    numparte = j + 1
                    Select Case j
                        Case 0
                            testo = parte1
                        Case 1
                            testo = parte2new
                        Case 2
                            If parte3new = "" Then
                                If idDichCan_EC = 0 Then
                                    If annotazioni <> "" Then
                                        parte3new = "<< Dati reddituali non importati per " & LCase(par.PulisciStrSql(annotazioni)) & " >>"
                                    Else
                                        parte3new = "<< Dati reddituali non importati da precedenti istanze >>"
                                    End If
                                Else
                                    parte3new = "<< Dati reddituali non importati da precedenti istanze >>"
                                End If
                            End If
                            testo = parte3new
                        Case 3
                            testo = parte4
                    End Select

                    If importoTrovato = True Then
                        par.cmd.CommandText = "INSERT INTO CANONI_PRE_RECA (ID_DOMANDA,ANNO_RIFERIMENTO,TESTO_CANONE,NUM_PARTE,IMPORTO) VALUES (" & new_id_dom & "," & I & ",'" & par.PulisciStrSql(testo) & "','" & numparte & "'," & par.VirgoleInPunti(Format(CanonePREreca, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            Next
        End If

        Dim strScript As String = ""
        If tipoContrattoLoc = "ERP" Then
            par.cmd.CommandText = "SELECT * FROM CANONI_PRE_RECA WHERE ID_DOMANDA = " & new_id_dom
            Dim myReaderDelete As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderDelete.Read = False Then
                par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA WHERE ID =" & new_id_dom
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "DELETE FROM DICHIARAZIONI_VSA WHERE ID =" & new_idDichia
                par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Impossibile procedere. Nessuna situazione pre-reca è stata memorizzata!')</script>")
            Else
                Response.Write(strScript)
            End If
            myReaderDelete.Close()
        Else
            Response.Write(strScript)
        End If

    End Sub


    Private Sub RicavaSituazionePRE(ByVal codContratto As String, ByVal new_id_dom As Long, ByVal new_idDichia As Long, ByVal ID_D_IMPORT As Long)

        Dim IMPORTO As Double
        Dim NuovoTransit As Double
        Dim LOCATIVO As String = ""
        Dim comunicazioni As String = ""
        Dim AreaEconomica As Integer
        Dim sISEE As String = ""
        Dim sISE As String = ""
        Dim sISR As String = ""
        Dim sISP As String = ""
        Dim sVSE As String = ""
        Dim sREDD_DIP As String = ""
        Dim sREDD_ALT As String = ""
        Dim sLimitePensione As String = ""
        Dim sPER_VAL_LOC As String = ""
        Dim sPERC_INC_MAX_ISE_ERP As String = ""
        Dim sCANONE_MIN As String = ""
        Dim sISE_MIN As String = ""
        Dim sCanone As String = ""
        Dim sNOTE As String = ""
        Dim sDEM As String = ""
        Dim sSUPCONVENZIONALE As String = ""
        Dim sCOSTOBASE As String = ""
        Dim sZONA As String = ""
        Dim sPIANO As String = ""
        Dim sCONSERVAZIONE As String = ""
        Dim sVETUSTA As String = ""
        Dim sPSE As String = ""
        Dim sINCIDENZAISE As String = ""
        Dim sCOEFFFAM As String = ""
        Dim sSOTTOAREA As String = ""
        Dim sMOTIVODECADENZA As String = ""
        Dim sNUMCOMP As String = ""
        Dim sNUMCOMP66 As String = ""
        Dim sNUMCOMP100 As String = ""
        Dim sNUMCOMP100C As String = ""
        Dim sPREVDIP As String = ""
        Dim sDETRAZIONI As String = ""
        Dim sMOBILIARI As String = ""
        Dim sIMMOBILIARI As String = ""
        Dim sCOMPLESSIVO As String = ""
        Dim sDETRAZIONEF As String = ""
        Dim sANNOCOSTRUZIONE As String = ""
        Dim sLOCALITA As String = ""
        Dim sASCENSORE As String = ""
        Dim sDESCRIZIONEPIANO As String = ""
        Dim sSUPNETTA As String = ""
        Dim sALTRESUP As String = ""
        Dim sMINORI15 As String = ""
        Dim sMAGGIORI65 As String = ""
        Dim sSUPACCESSORI As String = ""
        Dim sVALORELOCATIVO As String = ""
        Dim sCANONESOPP As String = ""
        Dim sVALOCIICI As String = ""
        Dim sALLOGGIOIDONEO As String = ""
        Dim sISTAT As String = ""
        Dim sCANONECLASSE As String = ""
        Dim sCANONECLASSEISTAT As String = ""
        Dim sANNOINIZIOVAL As String = ""
        Dim sANNOFINEVAL As String = ""
        Dim parte1 As String = ""
        Dim parte2 As String = ""
        Dim parte3 As String = ""
        Dim parte4 As String = ""
        Dim IDdich As String = ""
        Dim dataInizioValidita As String = ""
        Dim I As Integer
        Dim Prov As Integer
        Dim IDUNITA As Long
        Dim ANNO_INIZIO As Integer = 0
        Dim PER_ANNI As Integer = 0
        Dim CanonePREreca As Decimal = 0
        Dim idDichCan_EC As Long = 0
        Dim idDOMCan_EC As Long = 0
        'Dim istat2009 As String = "2,025"
        Dim importoTrovato As Boolean = True
        Dim parte2new As String = ""
        Dim parte3new As String = ""
        Dim ID_AU As Long
        Dim annotazioni As String = ""
        Dim idContratto As Long = 0
        Dim canoneIniziale As Decimal = 0
        Dim tipoContrattoLoc As String = ""
        Dim id_dom As Long = 0
        'Dim ANNICONGUAGLIO As Integer = 0
        Try

            Prov = 0

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            par.cmd.CommandText = "DELETE FROM CANONI_PRE_RECA WHERE ID_DOMANDA=" & new_id_dom
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & ID_D_IMPORT
            Dim myReaderX0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX0.Read Then
                id_dom = par.IfNull(myReaderX0("id"), 0)
            Else
                par.cmd.CommandText = "SELECT ID FROM UTENZA_DICHIARAZIONI WHERE ID=" & ID_D_IMPORT
                Dim myReaderX01 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX01.Read Then
                    id_dom = par.IfNull(myReaderX01("id"), 0)
                End If
                myReaderX01.Close()
            End If
            myReaderX0.Close()

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID,ID_UNITA,RAPPORTI_UTENZA.ID_AU,RAPPORTI_UTENZA.IMP_CANONE_INIZIALE,COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.unita_immobiliari WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND unita_contrattuale.id_unita = unita_immobiliari.ID AND unita_immobiliari.id_unita_principale IS NULL AND COD_CONTRATTO='" & codContratto & "'"
            Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX1.Read Then
                IDUNITA = myReaderX1("ID_UNITA")
                ID_AU = par.IfNull(myReaderX1("ID_AU"), 0)
                idContratto = par.IfNull(myReaderX1("ID"), -1)
                canoneIniziale = par.IfNull(myReaderX1("IMP_CANONE_INIZIALE"), 0)
                tipoContrattoLoc = par.IfNull(myReaderX1("COD_TIPOLOGIA_CONTR_LOC"), "")
            End If
            myReaderX1.Close()


            If IDUNITA <> 0 Then

                par.cmd.CommandText = "SELECT DATA_EVENTO,DATA_INIZIO_VAL,DATA_FINE_VAL,DICHIARAZIONI_VSA.ID_STATO,DICHIARAZIONI_VSA.ID,DOMANDE_BANDO_VSA.REDDITI_PRE_RECA FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID=" & new_id_dom
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    ANNO_INIZIO = CInt(Mid(par.IfNull(myReader("DATA_INIZIO_VAL"), Year(Now)), 1, 4))

                    dataInizioValidita = par.IfNull(myReader("DATA_INIZIO_VAL"), "")
                    IDdich = par.IfNull(myReader("ID"), "")

                    If par.IfNull(myReader("DATA_FINE_VAL"), "") = "29991231" Then
                        dataFine = Year(Now) & "1231"
                    Else
                        dataFine = par.IfNull(myReader("DATA_FINE_VAL"), "")
                    End If
                    PER_ANNI = DateDiff(DateInterval.Year, CDate(par.FormattaData(myReader("DATA_INIZIO_VAL"))), CDate(par.FormattaData(dataFine)))

                    'ANNICONGUAGLIO = ANNO_INIZIO + PER_ANNI

                    'If causale.Value = "28" Then
                    '    ANNICONGUAGLIO = Year(Now)
                    'End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & ANNO_INIZIO & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & ANNO_INIZIO & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                Dim myReaderCEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCEC.HasRows = True Then
                    If myReaderCEC.Read Then
                        If par.IfNull(myReaderCEC("tipo_provenienza"), 0) = 1 Then
                            Prov = 3
                        Else
                            Prov = 0
                        End If
                        id_dom = par.IfNull(myReaderCEC("id_dichiarazione"), 0)
                    End If
                End If
                myReaderCEC.Close()

                For I = ANNO_INIZIO To ANNO_INIZIO + PER_ANNI
                    CanonePREreca = 0
                    parte2 = ""
                    parte3 = ""
                    parte4 = ""

                    'If id_dom <> 0 And I = ANNO_INIZIO Then
                    '    par.CalcolaCanone27RECA(id_dom, Prov, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                    '    parte3new = parte3
                    '    parte2new = parte2
                    'End If

                    LOCATIVO = ""
                    comunicazioni = ""

                    sISEE = ""
                    sISE = ""
                    sISR = ""
                    sISP = ""
                    sVSE = ""
                    sREDD_DIP = ""
                    sREDD_ALT = ""
                    sLimitePensione = ""
                    sPER_VAL_LOC = ""
                    sPERC_INC_MAX_ISE_ERP = ""
                    sCANONE_MIN = ""
                    sISE_MIN = ""
                    sCanone = ""
                    sNOTE = ""
                    sDEM = ""
                    sSUPCONVENZIONALE = ""
                    sCOSTOBASE = ""
                    sZONA = ""
                    sMOTIVODECADENZA = ""

                    If I = 2008 Or I = 2009 Then
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.RAPPORTI_UTENZA_EXTRA where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "')"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.Read Then
                            parte4 = vbCrLf & vbCrLf & "CANONE ANNO " & I & vbCrLf

                            CanonePREreca = par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0)
                            'TOLTO COME DA ISTRUZIONI 11/06/2012
                            'If I = 2009 Then
                            '    If par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") >= 12 And par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") < 27 Then
                            '        CanonePREreca = CanonePREreca + ((CanonePREreca * CDbl(istat2009)) / 100)
                            '    End If
                            'End If
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(CanonePREreca), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0) / 12), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE TRANSITORIO:..........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_TRANSITORIO"), 0)), "##,##0.00")
                            If I = 2008 Then
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE GRADUATO " & I & ":........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_PRIMO_ANNO"), 0)), "##,##0.00")
                            Else
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE GRADUATO " & I & ":........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_SECONDO_ANNO"), 0)), "##,##0.00")
                            End If
                        Else
                            importoTrovato = False
                        End If
                        myReaderRX.Close()

                        If I <= 2012 Then
                            par.cmd.CommandText = "SELECT CANONE_COMPETENZA_" & I & " FROM SISCOM_MI.ELABORAZIONE_CONGUAGLI WHERE COD_CONTRATTO='" & codContratto & "'"
                            Dim myReaderCanone As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderCanone.Read Then
                                If par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0) <> 0 Then
                                    parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0)), "##,##0.00")
                                End If
                            End If
                            myReaderCanone.Close()
                        End If

                    End If


                    If I = 2010 Or I = 2011 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then


                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 2 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()

                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()

                                'End If
                                'CanonePREreca = IMPORTO
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If
                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)
                                '    CanonePREreca = IMPORTO
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale

                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")


                            'par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                            '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                            '    & "525,10001,10002,30003,530," _
                            '    & "30075,1,10072,10087,10125," _
                            '    & "10135,20003,20019,20020," _
                            '    & "20023,20096,20097,553," _
                            '    & "10075,10128,20021,10127," _
                            '    & "10126,512,10074,534,10073," _
                            '    & "604,30071,603,30068,506," _
                            '    & "647,653,599,648,30080,622," _
                            '    & "30123,30124,508,10160,509," _
                            '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                            '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                            '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            'Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'If myReaderComp.Read Then
                            '    parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            'End If
                            'myReaderComp.Close()

                        End If
                        myReaderRX.Close()
                    End If



                    If I = 2012 Or I = 2013 Then
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then
                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 5 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()
                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()
                                'End If
                                'CanonePREreca = IMPORTO
                                'parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                '    CanonePREreca = IMPORTO
                                '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale
                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                            'If ID_AU <> 0 Then
                            'par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                            par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                & "525,10001,10002,30003,530," _
                                & "30075,1,10072,10087,10125," _
                                & "10135,20003,20019,20020," _
                                & "20023,20096,20097,553," _
                                & "10075,10128,20021,10127," _
                                & "10126,512,10074,534,10073," _
                                & "604,30071,603,30068,506," _
                                & "647,653,599,648,30080,622," _
                                & "30123,30124,508,10160,509," _
                                & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderComp.Read Then
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            End If
                            myReaderComp.Close()

                            'Else
                            'importoTrovato = False
                            'End If
                        End If
                        myReaderRX.Close()
                    End If

                    If I = 2014 Or I = 2015 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then
                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 8 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()
                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()
                                'End If
                                'CanonePREreca = IMPORTO
                                'parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                '    CanonePREreca = IMPORTO
                                '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale
                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                            'If ID_AU <> 0 Then
                            '''''''''par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                            par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                & "525,10001,10002,30003,530," _
                                & "30075,1,10072,10087,10125," _
                                & "10135,20003,20019,20020," _
                                & "20023,20096,20097,553," _
                                & "10075,10128,20021,10127," _
                                & "10126,512,10074,534,10073," _
                                & "604,30071,603,30068,506," _
                                & "647,653,599,648,30080,622," _
                                & "30123,30124,508,10160,509," _
                                & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderComp.Read Then
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            End If
                            myReaderComp.Close()

                            'Else
                            'importoTrovato = False
                            'End If
                        End If
                        myReaderRX.Close()
                    End If

                    If I >= 2016 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then
                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 10 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()
                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()
                                'End If
                                'CanonePREreca = IMPORTO
                                'parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")


                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                '    CanonePREreca = IMPORTO
                                '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale
                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                            'If ID_AU <> 0 Then
                            '''''''''par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                            par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                & "525,10001,10002,30003,530," _
                                & "30075,1,10072,10087,10125," _
                                & "10135,20003,20019,20020," _
                                & "20023,20096,20097,553," _
                                & "10075,10128,20021,10127," _
                                & "10126,512,10074,534,10073," _
                                & "604,30071,603,30068,506," _
                                & "647,653,599,648,30080,622," _
                                & "30123,30124,508,10160,509," _
                                & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderComp.Read Then
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            End If
                            myReaderComp.Close()

                            'Else
                            'importoTrovato = False
                            'End If
                        End If
                        myReaderRX.Close()
                    End If

                    Dim numparte As String = ""
                    Dim testo As String = ""
                    For j As Integer = 0 To 3
                        numparte = j + 1
                        Select Case j
                            Case 0
                                testo = parte1
                            Case 1
                                testo = parte2new
                            Case 2
                                If parte3new = "" Then
                                    If idDichCan_EC = 0 Then
                                        If annotazioni <> "" Then
                                            parte3new = "<< Dati reddituali non importati per " & LCase(par.PulisciStrSql(annotazioni)) & " >>"
                                        Else
                                            parte3new = "<< Dati reddituali non importati da precedenti istanze >>"
                                        End If
                                    Else
                                        parte3new = "<< Dati reddituali non importati da precedenti istanze >>"
                                    End If
                                End If
                                testo = parte3new
                            Case 3
                                testo = parte4
                        End Select

                        If importoTrovato = True Then
                            par.cmd.CommandText = "INSERT INTO CANONI_PRE_RECA (ID_DOMANDA,ANNO_RIFERIMENTO,TESTO_CANONE,NUM_PARTE,IMPORTO) VALUES (" & new_id_dom & "," & I & ",'" & par.PulisciStrSql(testo) & "','" & numparte & "'," & par.VirgoleInPunti(Format(CanonePREreca, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next
                Next
            End If

            Dim strScript As String = ""
            If tipoContrattoLoc = "ERP" Then
                par.cmd.CommandText = "SELECT * FROM CANONI_PRE_RECA WHERE ID_DOMANDA = " & new_id_dom
                Dim myReaderDelete As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderDelete.Read = False Then
                    par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA WHERE ID =" & new_id_dom
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "DELETE FROM DICHIARAZIONI_VSA WHERE ID =" & new_idDichia
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Impossibile procedere. Nessuna situazione pre-reca è stata memorizzata!')</script>")
                Else
                    Response.Write(strScript)
                End If
                myReaderDelete.Close()
            Else
                Response.Write(strScript)
            End If


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
