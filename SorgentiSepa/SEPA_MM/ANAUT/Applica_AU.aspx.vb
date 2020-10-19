Imports System.IO
Imports ExpertPdf.HtmlToPdf

Partial Class ANAUT_Applica_AU
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Beep()
    End Sub

    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property

    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set
    End Property

    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property

    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property

    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sPIANO() As String
        Get
            If Not (ViewState("par_sPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPIANO") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property


    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property


    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property


    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property


    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        par.OracleConn.Open()
        par.SettaCommand(par)
        Dim s As String = ""

        Dim comunicazioni As String = ""
        Dim LimiteIsee As Integer = 0
        Dim DAFARE As Boolean
        Dim CANONE91 As String = ""

        'RISPONDENTI, TRANNE DICHIARAZIONI SU CONTRATTI VIRTUALI, SU DICHIARAZIONI SOSPESE PER VERIFICHE DOCUMENTI MANCANTI
        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UNITA_IMMOBILIARI.ID AS IDUNITA FROM UTENZA_DICHIARAZIONI, SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
        & "pg in ('0000059819','0000064734','0000074837','0000060740','0000061300','0000057526','0000060973','0000059993','0000073867','0000072238','0000074078','0000063542','0000062028','0000060870','0000062403','0000067984','0000074663" _
        & "','0000074416','0000064674','0000065957','0000064974','0000062287','0000058273','0000063060','0000059105','0000069432','0000064938','0000069013','0000064337','0000063314','0000067643','0000061392','0000059445" _
        & "','0000061773','0000080511','0000070124','0000072414','0000067285','0000065605','0000062594','0000061781','0000058475','0000058793','0000062668','0000067078" _
        & "','0000059849','0000063940','0000064234','0000070412','0000063993','0000063561" _
        & "','0000063733','0000066538','0000065558" _
        & "','0000076259','0000071202','0000067839','0000066291','0000064400','0000060466','0000070755','0000073531','0000064643','0000069323','0000057820','0000076674" _
        & "','0000080211','0000066282','0000065977','0000079718','0000077297','0000067404','0000064135','0000076609','0000074855','0000066802','0000068064','0000062756','0000062549','0000057726') " _
        & " and UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND  SUBSTR(POSIZIONE,1,6) <>'000000' and UTENZA_DICHIARAZIONI.ID_BANDO=2 AND (UTENZA_DICHIARAZIONI.ID_STATO=1 or UTENZA_DICHIARAZIONI.ID_STATO=0 or UTENZA_DICHIARAZIONI.ID_STATO=2) AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE=UTENZA_DICHIARAZIONI.POSIZIONE AND UTENZA_DICHIARAZIONI.RAPPORTO IN (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA) AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND NVL(FL_GENERAZ_AUTO,0)=0  ORDER BY PG ASC"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read
            DAFARE = True
            If par.IfNull(myReader("ID_STATO"), "0") = "0" Then
                If par.IfNull(myReader("fl_sosp_7"), "0") = "1" And par.IfNull(myReader("fl_sosp_1"), "0") = "0" And par.IfNull(myReader("fl_sosp_2"), "0") = "0" And par.IfNull(myReader("fl_sosp_3"), "0") = "0" And par.IfNull(myReader("fl_sosp_4"), "0") = "0" And par.IfNull(myReader("fl_sosp_5"), "0") = "0" And par.IfNull(myReader("fl_sosp_6"), "0") = "0" Then
                    DAFARE = True
                Else
                    If par.IfNull(myReader("fl_sosp_7"), "0") = "0" And par.IfNull(myReader("fl_sosp_1"), "0") = "0" And par.IfNull(myReader("fl_sosp_2"), "0") = "0" And par.IfNull(myReader("fl_sosp_3"), "0") = "0" And par.IfNull(myReader("fl_sosp_4"), "0") = "0" And par.IfNull(myReader("fl_sosp_5"), "0") = "0" And par.IfNull(myReader("fl_sosp_6"), "0") = "0" Then
                        DAFARE = True
                    Else
                        DAFARE = False
                    End If

                End If
            End If

            If DAFARE = True Then
                s = par.CalcolaCanone27_AU_2009(myReader("id"), myReader("idunita"), myReader("rapporto"), CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)

                par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ISTAT"",(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ADEGUAMENTO"" FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & myReader("RAPPORTO") & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    CANONE91 = ""
                    par.cmd.CommandText = "SELECT IMP_ANN_CANONE_91_ATTUALIZZ FROM SISCOM_MI.RAPPORTI_UTENZA_EXTRA WHERE ID_CONTRATTO=" & myReader1("ID")
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        CANONE91 = par.IfNull(myReader2(0), "")
                    End If
                    myReader2.Close()

                    'par.cmd.CommandText = "DELETE FROM SISCOM_MI.CANONI_EC_ELABORAZIONI WHERE ID_BANDO=2 AND COD_CONTRATTO= '" & myReader("rapporto") & "'"
                    'par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC_ELABORAZIONI (ID_CONTRATTO,) VALUES ('" & myReader("rapporto") & "','" & par.FormattaData(myReader1("DATA_DECORRENZA")) & "','" & par.FormattaData(par.IfNull(myReader1("DATA_RICONSEGNA"), "")) & "','" & par.PulisciStrSql(myReader1("PRESSO_COR")) & "','" & par.PulisciStrSql(myReader1("VIA_COR")) & "','" & par.PulisciStrSql(myReader1("LUOGO_COR")) & "','" & par.PulisciStrSql(myReader1("CIVICO_COR")) & "','" & par.PulisciStrSql(myReader1("SIGLA_COR")) & "','" & par.PulisciStrSql(myReader1("CAP_COR")) & "'," & par.VirgoleInPunti(CanoneCorrente / 12) & "," & par.VirgoleInPunti(CanoneCorrente) & "," & par.VirgoleInPunti(par.IfNull(myReader1("IMP_CANONE_INIZIALE"), 0)) & ")"
                    'par.cmd.ExecuteNonQuery()

                    ' par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_CANONE_INIZIALE=" & par.VirgoleInPunti(CanoneCorrente) & " WHERE COD_CONTRATTO='" & myReader("RAPPORTO") & "'"
                    ' par.cmd.ExecuteNonQuery()



                    's = Replace(s, vbCrLf, "<br />")

                    's = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body><table><tr><td style='font-family: Courier New; font-size: 14pt'>" & s & "</td></tr></table></body></html>"


                    Dim url As String = Replace(Replace(Replace(myReader("rapporto") & "_" & myReader1("PRESSO_COR"), "\", "."), "/", "."), "*", ".")

                    Dim sr As StreamWriter = New StreamWriter("C:\MILANO_AU_2009\" & url & ".TXT", False, System.Text.Encoding.Default)
                    sr.WriteLine(s)
                    sr.Close()


                    If System.IO.File.Exists("C:\MILANO_AU_2009\" & url & ".TXT") = True Then

                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.CANONI_EC_ELABORAZIONI WHERE ID_BANDO_AU=2 AND ID_CONTRATTO=" & myReader1("id")
                        par.cmd.ExecuteNonQuery()

                        If CDbl(par.IfNull(myReader("ISEE"), "0")) > 35000 Then
                            LimiteIsee = 1
                        Else
                            LimiteIsee = 0
                        End If



                        If sNOTE = "" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC_ELABORAZIONI (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, " _
                                                & "ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA,INCIDENZA_ISE," _
                                                & "COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
                                                & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
                                                & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91) " _
                                                & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'" & par.FormattaData(myReader1("DATA_STIPULA")) & "','" & myReader1("COD_CONTRATTO") & "'," & myReader1("id") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                                & "'," & AreaEconomica & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" _
                                                & sREDD_ALT & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & sCanone & "',:TESTO,'" _
                                                & par.PulisciStrSql(sNOTE) & "',2,'" & sDEM & "','" & sSUPCONVENZIONALE & "','" & sCOSTOBASE & "','" & sZONA & "','" & sPIANO & "','" _
                                                & sCONSERVAZIONE & "','" & sVETUSTA & "','" & sINCIDENZAISE & "','" & sCOEFFFAM & "','" & sSOTTOAREA & "','" & sMOTIVODECADENZA & "'," _
                                                & par.IfNull(myReader("PATR_SUPERATO"), 0) & ",0," & LimiteIsee & "," & par.IfNull(myReader("ID"), 0) _
                                                & ",'" & par.IfNull(myReader1("IMP_CANONE_INIZIALE"), "0,00") & "','" & par.IfNull(myReader1("ADEGUAMENTO"), "0,00") & "','" _
                                                & par.IfNull(myReader1("ISTAT"), "0,00") & "'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
                                                & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
                                                & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','" & CANONE91 & "') "
                        Else
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC_ELABORAZIONI (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI," _
                                                & "LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE," _
                                                & "CANONE_ATTUALE,ADEGUAMENTO,ISTAT,NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI," _
                                                & "REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE,LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91) " _
                                                & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'" & par.FormattaData(myReader1("DATA_STIPULA")) & "','" & myReader1("COD_CONTRATTO") & "'," & myReader1("id") & ",'" & Format(Now, "yyyyMMddHHmmss") & "',NULL,'','','','','','','','','','','','','',:TESTO,'" & _
                                                par.PulisciStrSql(sNOTE) & "',2,'" & sMOTIVODECADENZA & "'," & par.IfNull(myReader("PATR_SUPERATO"), 0) & ",0," & LimiteIsee & "," _
                                                & par.IfNull(myReader("ID"), 0) & ",'" & par.IfNull(myReader1("IMP_CANONE_INIZIALE"), "0,00") & "','" & par.IfNull(myReader1("ADEGUAMENTO"), "0,00") _
                                                & "','" & par.IfNull(myReader1("ISTAT"), "0,00") & "'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
                                                & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" _
                                                & par.PulisciStrSql(sLOCALITA) & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','" & CANONE91 & "') "

                        End If
                        Dim objStream As Stream = File.Open("C:\MILANO_AU_2009\" & url & ".TXT", FileMode.Open)
                        Dim buffer(objStream.Length) As Byte
                        objStream.Read(buffer, 0, objStream.Length)
                        objStream.Close()

                        Dim parmData As New Oracle.DataAccess.Client.OracleParameter
                        With parmData
                            .Direction = Data.ParameterDirection.Input
                            .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
                            .ParameterName = "TESTO"
                            .Value = buffer
                        End With

                        par.cmd.Parameters.Add(parmData)
                        par.cmd.ExecuteNonQuery()
                        System.IO.File.Delete("C:\MILANO_AU_2009\" & url & ".TXT")
                        par.cmd.Parameters.Remove(parmData)

                        buffer = Nothing
                        objStream = Nothing
                    End If



                    'Dim pdfConverter As PdfConverter = New PdfConverter
                    'pdfConverter.LicenseKey = "GzAqOyM7Ii47LTUrOygqNSopNSIiIiI="
                    'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                    'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfDocumentOptions.ShowFooter = False
                    'pdfConverter.PdfDocumentOptions.LeftMargin = 10
                    'pdfConverter.PdfDocumentOptions.RightMargin = 10
                    'pdfConverter.PdfDocumentOptions.TopMargin = 21
                    'pdfConverter.PdfDocumentOptions.BottomMargin = 21
                    'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfFooterOptions.FooterText = ("")

                    'pdfConverter.PdfFooterOptions.DrawFooterLine = False
                    'pdfConverter.PdfFooterOptions.PageNumberText = ""
                    'pdfConverter.PdfFooterOptions.ShowPageNumber = False

                    'pdfConverter.SavePdfFromHtmlStringToFile(s, "C:\OSIO_AU_2011\" & url & ".pdf")
                End If
                myReader1.Close()
            End If
        End While
        myReader.Close()


        ''NON RISPONDENTI, TRANNE CONTRATTI VIRTUALI
        'comunicazioni = ""
        'LimiteIsee = 0

        'par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS IDUNITA, RAPPORTI_UTENZA.IMP_CANONE_INIZIALE,RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.PRESSO_COR,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.DATA_STIPULA,(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT,(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_IMMOBILIARI.COD_tipologia='AL' and UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SUBSTR(COD_CONTRATTO,1,6)<>'000000' AND SUBSTR(COD_CONTRATTO,1,2)<>'41' AND SUBSTR(COD_CONTRATTO,1,2)<>'42' AND SUBSTR(COD_CONTRATTO,1,2)<>'43' AND RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.DIFFIDE_LETTERE) AND COD_CONTRATTO NOT IN (SELECT NVL(RAPPORTO,'0') FROM UTENZA_DICHIARAZIONI WHERE (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND NVL(FL_GENERAZ_AUTO,0)<>1 AND ID_BANDO=2)"
        'myReader = par.cmd.ExecuteReader()
        'While myReader.Read
        '    s = par.CalcolaCanone27_AU_2009(-1, myReader("idunita"), myReader("COD_CONTRATTO"), CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT)

        '    CANONE91 = ""
        '    par.cmd.CommandText = "SELECT IMP_ANN_CANONE_91_ATTUALIZZ FROM SISCOM_MI.RAPPORTI_UTENZA_EXTRA WHERE ID_CONTRATTO=" & myReader("ID")
        '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReader2.Read Then
        '        CANONE91 = par.IfNull(myReader2(0), "")
        '    End If
        '    myReader2.Close()

        '    'par.cmd.CommandText = "DELETE FROM SISCOM_MI.CANONI_EC_ELABORAZIONI WHERE ID_BANDO=2 AND COD_CONTRATTO= '" & myReader("rapporto") & "'"
        '    'par.cmd.ExecuteNonQuery()

        '    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC_ELABORAZIONI (ID_CONTRATTO,) VALUES ('" & myReader("rapporto") & "','" & par.FormattaData(myReader1("DATA_DECORRENZA")) & "','" & par.FormattaData(par.IfNull(myReader1("DATA_RICONSEGNA"), "")) & "','" & par.PulisciStrSql(myReader1("PRESSO_COR")) & "','" & par.PulisciStrSql(myReader1("VIA_COR")) & "','" & par.PulisciStrSql(myReader1("LUOGO_COR")) & "','" & par.PulisciStrSql(myReader1("CIVICO_COR")) & "','" & par.PulisciStrSql(myReader1("SIGLA_COR")) & "','" & par.PulisciStrSql(myReader1("CAP_COR")) & "'," & par.VirgoleInPunti(CanoneCorrente / 12) & "," & par.VirgoleInPunti(CanoneCorrente) & "," & par.VirgoleInPunti(par.IfNull(myReader1("IMP_CANONE_INIZIALE"), 0)) & ")"
        '    'par.cmd.ExecuteNonQuery()

        '    ' par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_CANONE_INIZIALE=" & par.VirgoleInPunti(CanoneCorrente) & " WHERE COD_CONTRATTO='" & myReader("RAPPORTO") & "'"
        '    ' par.cmd.ExecuteNonQuery()



        '    's = Replace(s, vbCrLf, "<br />")

        '    's = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body><table><tr><td style='font-family: Courier New; font-size: 14pt'>" & s & "</td></tr></table></body></html>"


        '    Dim url As String = Replace(Replace(Replace(myReader("COD_CONTRATTO") & "_" & myReader("PRESSO_COR"), "\", "."), "/", "."), "*", ".")

        '    Dim sr As StreamWriter = New StreamWriter("C:\MILANO_AU_2009\" & url & ".TXT", False, System.Text.Encoding.Default)
        '    sr.WriteLine(s)
        '    sr.Close()


        '    If System.IO.File.Exists("C:\MILANO_AU_2009\" & url & ".TXT") = True Then

        '        par.cmd.CommandText = "DELETE FROM SISCOM_MI.CANONI_EC_ELABORAZIONI WHERE ID_BANDO_AU=2 AND ID_CONTRATTO=" & myReader("id")
        '        par.cmd.ExecuteNonQuery()

        '        LimiteIsee = 0




        '    If sNOTE = "" Then
        '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC_ELABORAZIONI (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, " _
        '                                    & "LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA," _
        '                                    & "INCIDENZA_ISE,COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT" _
        '                                    & ",NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
        '                                    & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91) " _
        '                                    & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'" & par.FormattaData(myReader("DATA_STIPULA")) & "','" & myReader("COD_CONTRATTO") & "'," & myReader("id") & ",'" & Format(Now, "yyyyMMddHHmmss") _
        '                                    & "'," & AreaEconomica & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" & sREDD_ALT _
        '                                    & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & sCanone & "',:TESTO,'" _
        '                                    & par.PulisciStrSql(sNOTE) & "',2,'" & sDEM & "','" & sSUPCONVENZIONALE & "','" & sCOSTOBASE & "','" & sZONA & "','" & sPIANO & "','" & sCONSERVAZIONE & "','" _
        '                                    & sVETUSTA & "','" & sINCIDENZAISE & "','" & sCOEFFFAM & "','" & sSOTTOAREA & "','" & sMOTIVODECADENZA & "',0,0," & LimiteIsee & ",null,'" _
        '                                    & par.IfNull(myReader("IMP_CANONE_INIZIALE"), "0,00") & "','" & par.IfNull(myReader("ADEGUAMENTO"), "0,00") & "','" & par.IfNull(myReader("ISTAT"), "0,00") & "'," _
        '                                    & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
        '                                    & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
        '                                    & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','" & CANONE91 & "') "
        '    Else
        '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC_ELABORAZIONI (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, " _
        '                                    & "LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE," _
        '                                    & "ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
        '                                    & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA," _
        '                                    & "ANNO_COSTRUZIONE," _
        '                                    & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91) " _
        '                                    & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'" & par.FormattaData(myReader("DATA_STIPULA")) & "','" & myReader("COD_CONTRATTO") & "'," & myReader("id") & ",'" & Format(Now, "yyyyMMddHHmmss") _
        '                                    & "',NULL,'','','','','','','','','','','','','',:TESTO,'" & par.PulisciStrSql(sNOTE) & "',2,'" & sMOTIVODECADENZA & "',0,0," & LimiteIsee & ",null,'" _
        '                                    & par.IfNull(myReader("IMP_CANONE_INIZIALE"), "0,00") & "','" & par.IfNull(myReader("ADEGUAMENTO"), "0,00") & "','" _
        '                                    & par.IfNull(myReader("ISTAT"), "0,00") & "'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
        '                                    & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
        '                                    & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','" & CANONE91 & "') "

        '    End If
        '    Dim objStream As Stream = File.Open("C:\MILANO_AU_2009\" & url & ".TXT", FileMode.Open)
        '    Dim buffer(objStream.Length) As Byte
        '    objStream.Read(buffer, 0, objStream.Length)
        '    objStream.Close()

        '    Dim parmData As New Oracle.DataAccess.Client.OracleParameter
        '    With parmData
        '        .Direction = Data.ParameterDirection.Input
        '        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
        '        .ParameterName = "TESTO"
        '        .Value = buffer
        '    End With

        '    par.cmd.Parameters.Add(parmData)
        '    par.cmd.ExecuteNonQuery()
        '    System.IO.File.Delete("C:\MILANO_AU_2009\" & url & ".TXT")
        '    par.cmd.Parameters.Remove(parmData)

        '    buffer = Nothing
        '    objStream = Nothing
        '    End If



        '    'Dim pdfConverter As PdfConverter = New PdfConverter
        '    'pdfConverter.LicenseKey = "GzAqOyM7Ii47LTUrOygqNSopNSIiIiI="
        '    'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        '    'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        '    'pdfConverter.PdfDocumentOptions.ShowHeader = False
        '    'pdfConverter.PdfDocumentOptions.ShowFooter = False
        '    'pdfConverter.PdfDocumentOptions.LeftMargin = 10
        '    'pdfConverter.PdfDocumentOptions.RightMargin = 10
        '    'pdfConverter.PdfDocumentOptions.TopMargin = 21
        '    'pdfConverter.PdfDocumentOptions.BottomMargin = 21
        '    'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

        '    'pdfConverter.PdfDocumentOptions.ShowHeader = False
        '    'pdfConverter.PdfFooterOptions.FooterText = ("")

        '    'pdfConverter.PdfFooterOptions.DrawFooterLine = False
        '    'pdfConverter.PdfFooterOptions.PageNumberText = ""
        '    'pdfConverter.PdfFooterOptions.ShowPageNumber = False

        '    'pdfConverter.SavePdfFromHtmlStringToFile(s, "C:\OSIO_AU_2011\" & url & ".pdf")


        'End While
        'myReader.Close()



        par.cmd.Dispose()
        par.OracleConn.Close()


    End Sub

    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property



    Public Property CanoneCorrente() As Double
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDbl(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property
End Class
