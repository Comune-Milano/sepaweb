
Partial Class ASS_DecidiOfferta
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim s As String = ""
    Dim CANONE As Double = 0


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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If IsPostBack = False Then
            lIdDomanda = CLng(Request.QueryString("ID"))
            lIdOfferta = CLng(Request.QueryString("OF"))
            lblScad.Text = Request.QueryString("SC")

            HiddenField1.Value = Request.QueryString("T")
            Dim comunicazioni As String = ""
            Select Case Request.QueryString("T")
                Case "1"
                    VisualizzaDomanda()
                    If TipoAssegnazione = "0" Then
                        Dim VAL_LOCATIVO_UNITA As String = ""


                        If lIdUnita <> 0 Then
                            s = par.CalcolaCanone27(lIdDomanda, 1, lIdUnita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)

                            If comunicazioni <> "" Then
                                Response.Write("<script>alert('" & comunicazioni & "');</script>")
                            End If
                        End If
                    Else
                        HyperLink3.Visible = False
                    End If
                Case "2"
                    VisualizzaDomandaCambi()
                    Dim VAL_LOCATIVO_UNITA As String = ""
                    If lIdUnita <> 0 Then
                        s = par.CalcolaCanone27(lIdDomanda, 2, lIdUnita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
                        If comunicazioni <> "" Then
                            Response.Write("<script>alert('" & comunicazioni & "');</script>")
                        End If
                    End If
                Case "3"
                    VisualizzaDomandaEmergenza()
                    Dim VAL_LOCATIVO_UNITA As String = ""
                    If lIdUnita <> 0 Then
                        s = par.CalcolaCanone27(lIdDomanda, 3, lIdUnita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
                        If comunicazioni <> "" Then
                            Response.Write("<script>alert('" & comunicazioni & "');</script>")
                        End If
                    End If

            End Select

            If lIdDomanda < 500000 Then

            Else

            End If

            If lIdUnita <> 0 And TipoAssegnazione = "0" Then
                Session.Add("canone", s)
                HyperLink3.NavigateUrl = "Canone.aspx"
                HyperLink3.Target = "_blank"
            Else
                HyperLink3.Visible = False

            End If
                'Label23.Attributes.Add("OnClick", "window.open('../CONS/domanda.aspx?ID=" & lIdDomanda & "&ID1=-1&PROGR=-1&LE=1&APP=0','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
            End If
    End Sub


    Public Property Data_Prenotazione() As String
        Get
            If Not (ViewState("par_Data_Prenotazione") Is Nothing) Then
                Return CLng(ViewState("par_Data_Prenotazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Data_Prenotazione") = value
        End Set

    End Property

    Public Property lIdOfferta() As Long
        Get
            If Not (ViewState("par_lIdOfferta") Is Nothing) Then
                Return CLng(ViewState("par_lIdOfferta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdOfferta") = value
        End Set

    End Property

    Public Property lIdDomanda() As Long
        Get
            If Not (ViewState("par_lIdDomanda") Is Nothing) Then
                Return CLng(ViewState("par_lIdDomanda"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDomanda") = value
        End Set

    End Property

    Public Property TipoAssegnazione() As Long
        Get
            If Not (ViewState("par_TipoAssegnazione") Is Nothing) Then
                Return CLng(ViewState("par_TipoAssegnazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_TipoAssegnazione") = value
        End Set

    End Property

    Public Property lIdUnita() As Long
        Get
            If Not (ViewState("par_lIdUnita") Is Nothing) Then
                Return CLng(ViewState("par_lIdUnita"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdUnita") = value
        End Set

    End Property

    Private Function VisualizzaDomandaCambi()
        Dim scriptblock As String
        Dim CF As String
        Dim ID_Dichiarazione As Long

        Try
            DropDownList1.Visible = False


            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE1111", par.OracleConn)

            lblOfferta.Text = "Offerta N° " & lIdOfferta
            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_cambi WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                myReader.Close()
                par.cmd.CommandText = "SELECT DICHIARAZIONI_CAMBI.PG AS ""PGDIC"",trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_cambi.DATA_PG,DOMANDE_BANDO_cambi.id_bando,DOMANDE_BANDO_cambi.PG,DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,COMP_NUCLEO_CAMBI.COD_FISCALE,DOMANDE_BANDO_cambi.tipo_pratica,domande_bando_cambi.FL_ASS_ESTERNA,DOMANDE_BANDO_cambi.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF_cambi.POSIZIONE,trunc(BANDI_GRADUATORIA_DEF_cambi.ISBARC_R,4) AS ""GRAD"" FROM BANDI_GRADUATORIA_DEF_cambi,DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi,dichiarazioni_cambi WHERE dichiarazioni_cambi.id=domande_bando_cambi.id_dichiarazione and DOMANDE_BANDO_cambi.ID=BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA (+) AND DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=COMP_NUCLEO_cambi.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_cambi.PROGR=DOMANDE_BANDO_cambi.PROGR_COMPONENTE AND DOMANDE_BANDO_cambi.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda non è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                        btnAccetta.Enabled = False
                        btnRifiuta.Enabled = False
                    End If
                    ID_Dichiarazione = par.IfNull(myReader1("ID_DICHIARAZIONE"), 0)
                    CF = par.IfNull(myReader1("COD_FISCALE"), "")
                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDic.Text = par.IfNull(myReader1("PGDIC"), "")


                    If Len(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")) > 25 Then
                        lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 23) & "..."
                        lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    Else
                        lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    End If
                    lblIsbarcr.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                    Label20.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    Label18.Text = par.IfNull(myReader1("GRAD"), "0")

                    Label23.Text = par.IfNull(myReader1("COGNOME"), "")
                    Label24.Text = par.IfNull(myReader1("NOME"), "")
                    Label25.Text = CF
                    Label26.Text = par.IfNull(myReader1("ID_DICHIARAZIONE"), "-1")




                    lblPGDic.Attributes.Add("onclick", "javascript:window.open('../CAMBI/max.aspx?ID=" & par.IfNull(myReader1("id_dichiarazione"), "0") & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    lblPG.Attributes.Add("onclick", "javascript:window.open('../CAMBI/domanda.aspx?ID=" & lIdDomanda & "&LE=1&US=1','','top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    lblVisDoc.Attributes.Add("onclick", "javascript:window.open('../EstremiDocumento.aspx?T=2&ID=" & lIdDomanda & "&I=" & par.Cripta(lblNominativo.Text) & "','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")


                    par.cmd.CommandText = "SELECT  * FROM EVENTI_BANDI_cambi WHERE ID_DOMANDA=" & lIdDomanda & " AND COD_EVENTO='F136'"
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader11.Read() Then
                        If par.IfNull(myReader11("cod_evento"), "") <> "" Then
                            'MyExecuteSql("update bandi_graduatoria set tipo=1 where id_domanda=" & Id_Pratica)
                            DropDownList1.Visible = True

                            Label16.Text = "E' stata rilevata una deroga accolta in data " & par.FormattaData(Mid(myReader11("data_ora"), 1, 8)) & ". Procedere con.."
                            'Else
                            '   MyExecuteSql("update bandi_graduatoria set tipo=0 where id_domanda=" & Id_Pratica)
                            '  MsgBox("Si procede con Assegnazione REGOLARE", vbInformation)
                            ' ScriviEventoBANDI(Id_Pratica, 162, 9, 0)
                        End If
                    Else
                        DropDownList1.Visible = False

                        Label16.Text = ""
                    End If
                    myReader11.Close()



                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_cambi.ID) FROM COMP_NUCLEO_cambi,dichiarazioni_cambi,domande_bando_cambi WHERE DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND DOMANDE_BANDO_cambi.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT id,cod_alloggio,DATA_DISPONIBILITA,fl_mod from alloggi where id_pratica=" & lIdDomanda & " and prenotato='1'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read() Then

                    TipoAssegnazione = par.IfNull(myReader2("fl_mod"), "0")
                    If TipoAssegnazione = "0" Then
                        Label22.Text = "A"
                    Else
                        Label22.Text = "B"
                    End If

                    lblIdAll.Text = par.IfNull(myReader2("id"), "-1")
                    lblData.Text = par.FormattaData(par.IfNull(myReader2("data_disponibilita"), ""))
                    par.cmd.CommandText = "SELECT id from siscom_MI.unita_immobiliari where cod_unita_immobiliare='" & par.IfNull(myReader2("cod_alloggio"), "-1") & "'"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read() Then
                        lIdUnita = par.IfNull(myReader3(0), -1)

                        par.cmd.CommandText = "select *  from SISCOM_MI.unita_stato_manutentivo where id_unita=" & lIdUnita
                        Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader123.HasRows = False Then
                            Response.Write("<script>alert('Attenzione, non è possibile procedere perchè manca verifica dello stato manutentivo!');window.close();</script>")
                            btnAccetta.Enabled = False
                            btnRifiuta.Enabled = False
                            DropDownList1.Enabled = False
                            cmbStato.Enabled = False
                        Else
                            If myReader123.Read = True Then
                                If par.IfNull(myReader123("riassegnabile"), "0") = "0" Then
                                    Response.Write("<script>alert('Attenzione, non è possibile procedere perchè questa unità è INAGIBILE secondo quanto descritto nell\'ultima verifica dello stato manutentivo!');window.close();</script>")
                                    btnAccetta.Enabled = False
                                    btnRifiuta.Enabled = False
                                    DropDownList1.Enabled = False
                                    cmbStato.Enabled = False
                                End If

                                If par.IfNull(myReader123("tIPO_riassegnabile"), "0") = "2" Then
                                    Response.Write("<script>alert('Attenzione, si ricorda che questa unità è AGIBILE ma non RIASSEGNABILE (si può inserire il rapporto ma non lo si può attivare) secondo quanto descritto nell\'ultima verifica dello stato manutentivo!');</script>")
                                End If
                            End If
                        End If
                        myReader123.Close()

                        If lIdUnita <> -1 Then
                            LBLMANUTENTIVO.Text = "<a href='../CENSIMENTO/VerificaSManutentivo.aspx?ID=" & lIdUnita & "&L=2' target='_blank'>Stato Manutentivo'</a>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod_GESTORE=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & par.IfNull(myReader2("id"), "-1")
                    myReader3 = par.cmd.ExecuteReader()

                    If myReader3.Read() Then
                        lblCodice.Text = par.IfNull(myReader3("COD_ALLOGGIO"), "")
                        lblZona.Text = par.IfNull(myReader3("zona"), "")
                        lblProprieta.Text = par.IfNull(myReader3("proprieta"), "")
                        lblStato.Text = par.IfNull(myReader3("stato"), "")
                        Data_Prenotazione = par.IfNull(myReader3("DATA_PRENOTATO"), "")
                        If par.IfNull(myReader3("TIPO"), "") = "0" Then
                            lblGestore.Text = Mid(par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - ERP", 1, 25)
                        Else
                            lblGestore.Text = Mid(par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - EQ", 1, 25)
                        End If
                    End If
                    myReader3.Close()
                Else
                    cmbStato.Enabled = False
                    txtNote.Enabled = False
                    btnAccetta.Enabled = False
                    btnRifiuta.Enabled = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio non più disponibile per questa assegnazione!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If

                myReader2.Close()

                par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & lIdDomanda & " and ultimo='1'"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    lblRelazione.Text = myReader2("id")
                Else
                    lblRelazione.Text = "1"
                End If
                myReader2.Close()

                myReader.Close()

                par.cmd.CommandText = "SELECT DICHIARAZIONI.ID as ""dichiarazione"",domande_bando.id as ""domanda"" FROM domande_bando,DICHIARAZIONI,COMP_NUCLEO WHERE domande_bando.id_dichiarazione=dichiarazioni.id and DICHIARAZIONI.ID=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO.COD_FISCALE='" & CF & "' OR COMP_NUCLEO.COD_FISCALE='" & CF & "')"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Image1.Visible = True

                    HyperLink1.Visible = True
                    HyperLink1.Text = "DOMANDA ERP"
                    HyperLink1.NavigateUrl = "../CAMBI/CORRELAZIONI.ASPX?CF=" & CF & "&ID=0"

                    HyperLink2.Visible = True
                    'HyperLink2.NavigateUrl = "../CAMBI/StatoDomanda.ASPX?ID=" & myReader("domanda")
                    HyperLink2.Attributes.Add("onClick", "javascript:window.open('../CAMBI/StatoDomanda.aspx?ID=" & myReader("domanda") & "','Stato','height=370,top=0,left=0,width=480,scrollbars=no');")

                End If
                myReader.Close()

                'imgPreferenze.Attributes.Add("OnClick", "javascript:window.open('Preferenze.aspx?" & "ID=" & lIdDomanda & "&PG=" & lblPG.Text & "','Preferenze','top=0,left=0,width=600,height=400');")
                'btnStampa.Attributes.Add("OnClick", "javascript:window.open('ListaAlloggi.aspx?" & "ID=" & lIdDomanda & "','Alloggi');")

                par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_TIPO_RIFIUTO_ALL ORDER BY COD ASC", "DESCRIZIONE", "COD")
                cmbStato.Items.Add(" ")
                cmbStato.Items.FindByText(" ").Selected = True

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            Else
                myReader.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.Message & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try
    End Function


    Private Function VisualizzaDomandaEmergenza()
        Dim scriptblock As String
        Dim CF As String
        Dim ID_Dichiarazione As Long

        Try
            DropDownList1.Visible = False


            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE1111", par.OracleConn)

            lblOfferta.Text = "Offerta N° " & lIdOfferta
            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_vsa WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                myReader.Close()
                par.cmd.CommandText = "SELECT DICHIARAZIONI_vsa.PG AS ""PGDIC"",trunc(DOMANDE_BANDO_vsa.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_vsa.DATA_PG,DOMANDE_BANDO_vsa.id_bando,DOMANDE_BANDO_vsa.PG,DOMANDE_BANDO_vsa.ID_DICHIARAZIONE,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME,COMP_NUCLEO_vsa.COD_FISCALE,DOMANDE_BANDO_vsa.tipo_pratica,domande_bando_vsa.FL_ASS_ESTERNA,DOMANDE_BANDO_vsa.TIPO_ALLOGGIO FROM DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa,dichiarazioni_vsa WHERE dichiarazioni_vsa.id=domande_bando_vsa.id_dichiarazione and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_vsa.PROGR=DOMANDE_BANDO_vsa.PROGR_COMPONENTE AND DOMANDE_BANDO_vsa.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda non è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                        btnAccetta.Enabled = False
                        btnRifiuta.Enabled = False
                    End If
                    ID_Dichiarazione = par.IfNull(myReader1("ID_DICHIARAZIONE"), 0)
                    CF = par.IfNull(myReader1("COD_FISCALE"), "")
                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDic.Text = par.IfNull(myReader1("PGDIC"), "")


                    If Len(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")) > 25 Then
                        lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 23) & "..."
                        lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    Else
                        lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    End If
                    lblIsbarcr.Text = "//" 'par.IfNull(myReader1("isbarc_r"), "0")
                    lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                    Label20.Text = "" 'par.IfNull(myReader1("POSIZIONE"), "0")
                    Label18.Text = "" 'par.IfNull(myReader1("GRAD"), "0")

                    Label23.Text = par.IfNull(myReader1("COGNOME"), "")
                    Label24.Text = par.IfNull(myReader1("NOME"), "")
                    Label25.Text = CF
                    Label26.Text = par.IfNull(myReader1("ID_DICHIARAZIONE"), "-1")




                    'lblPGDic.Attributes.Add("onclick", "javascript:window.open('../CAMBI/max.aspx?ID=" & par.IfNull(myReader1("id_dichiarazione"), "0") & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    'lblPG.Attributes.Add("onclick", "javascript:window.open('../CAMBI/domanda.aspx?ID=" & lIdDomanda & "&LE=1&US=1','','top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    lblVisDoc.Attributes.Add("onclick", "javascript:window.open('../EstremiDocumento.aspx?T=3&ID=" & lIdDomanda & "&I=" & par.Cripta(lblNominativo.Text) & "','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")


                    par.cmd.CommandText = "SELECT  * FROM EVENTI_BANDI_vsa WHERE ID_DOMANDA=" & lIdDomanda & " AND COD_EVENTO='F136'"
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader11.Read() Then
                        If par.IfNull(myReader11("cod_evento"), "") <> "" Then
                            'MyExecuteSql("update bandi_graduatoria set tipo=1 where id_domanda=" & Id_Pratica)
                            DropDownList1.Visible = True

                            Label16.Text = "E' stata rilevata una deroga accolta in data " & par.FormattaData(Mid(myReader11("data_ora"), 1, 8)) & ". Procedere con.."
                            'Else
                            '   MyExecuteSql("update bandi_graduatoria set tipo=0 where id_domanda=" & Id_Pratica)
                            '  MsgBox("Si procede con Assegnazione REGOLARE", vbInformation)
                            ' ScriviEventoBANDI(Id_Pratica, 162, 9, 0)
                        End If
                    Else
                        DropDownList1.Visible = False

                        Label16.Text = ""
                    End If
                    myReader11.Close()



                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & lIdDomanda
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    lblComp.Text = par.IfNull(myReader1(0), "0")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT id,cod_alloggio,DATA_DISPONIBILITA,fl_mod from alloggi where id_pratica=" & lIdDomanda & " and prenotato='1'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read() Then

                    TipoAssegnazione = "0" 'par.IfNull(myReader2("fl_mod"), "0")
                    Label22.Text = ""
                    'If TipoAssegnazione = "0" Then
                    '    Label22.Text = "A"
                    'Else
                    '    Label22.Text = "B"
                    'End If

                    lblIdAll.Text = par.IfNull(myReader2("id"), "-1")
                    lblData.Text = par.FormattaData(par.IfNull(myReader2("data_disponibilita"), ""))
                    par.cmd.CommandText = "SELECT id from siscom_MI.unita_immobiliari where cod_unita_immobiliare='" & par.IfNull(myReader2("cod_alloggio"), "-1") & "'"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read() Then
                        lIdUnita = par.IfNull(myReader3(0), -1)

                        par.cmd.CommandText = "select *  from SISCOM_MI.unita_stato_manutentivo where id_unita=" & lIdUnita
                        Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader123.HasRows = False Then
                            Response.Write("<script>alert('Attenzione, non è possibile procedere perchè manca verifica dello stato manutentivo!');window.close();</script>")
                            btnAccetta.Enabled = False
                            btnRifiuta.Enabled = False
                            DropDownList1.Enabled = False
                            cmbStato.Enabled = False
                        Else
                            If myReader123.Read = True Then
                                If par.IfNull(myReader123("riassegnabile"), "0") = "0" Then
                                    Response.Write("<script>alert('Attenzione, non è possibile procedere perchè questa unità è INAGIBILE secondo quanto descritto nell\'ultima verifica dello stato manutentivo!');window.close();</script>")
                                    btnAccetta.Enabled = False
                                    btnRifiuta.Enabled = False
                                    DropDownList1.Enabled = False
                                    cmbStato.Enabled = False
                                End If

                                If par.IfNull(myReader123("tIPO_riassegnabile"), "0") = "2" Then
                                    Response.Write("<script>alert('Attenzione, si ricorda che questa unità è AGIBILE ma non RIASSEGNABILE (si può inserire il rapporto ma non lo si può attivare) secondo quanto descritto nell\'ultima verifica dello stato manutentivo!');</script>")
                                End If
                            End If
                        End If
                        myReader123.Close()


                        If lIdUnita <> -1 Then
                            LBLMANUTENTIVO.Text = "<a href='../CENSIMENTO/VerificaSManutentivo.aspx?ID=" & lIdUnita & "&L=2' target='_blank'>Stato Manutentivo'</a>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod_GESTORE=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & par.IfNull(myReader2("id"), "-1")
                    myReader3 = par.cmd.ExecuteReader()

                    If myReader3.Read() Then
                        lblCodice.Text = par.IfNull(myReader3("COD_ALLOGGIO"), "")
                        lblZona.Text = par.IfNull(myReader3("zona"), "")
                        lblProprieta.Text = par.IfNull(myReader3("proprieta"), "")
                        lblStato.Text = par.IfNull(myReader3("stato"), "")
                        Data_Prenotazione = par.IfNull(myReader3("DATA_PRENOTATO"), "")
                        If par.IfNull(myReader3("TIPO"), "") = "0" Then
                            lblGestore.Text = Mid(par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - ERP", 1, 25)
                        Else
                            lblGestore.Text = Mid(par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - EQ", 1, 25)
                        End If
                    End If
                    myReader3.Close()
                Else
                    cmbStato.Enabled = False
                    txtNote.Enabled = False
                    btnAccetta.Enabled = False
                    btnRifiuta.Enabled = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Nessun Alloggio prenotato per questo assegnatario!');history.go(-1);" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If

                myReader2.Close()

                par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & lIdDomanda & " and ultimo='1'"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read() Then
                    lblRelazione.Text = myReader2("id")
                Else
                    lblRelazione.Text = "1"
                End If
                myReader2.Close()

                myReader.Close()


                HyperLink2.Visible = False

                'par.cmd.CommandText = "SELECT DICHIARAZIONI.ID as ""dichiarazione"",domande_bando.id as ""domanda"" FROM domande_bando,DICHIARAZIONI,COMP_NUCLEO WHERE domande_bando.id_dichiarazione=dichiarazioni.id and DICHIARAZIONI.ID=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO.COD_FISCALE='" & CF & "' OR COMP_NUCLEO.COD_FISCALE='" & CF & "')"
                'myReader = par.cmd.ExecuteReader()
                'If myReader.Read() Then
                '    Image1.Visible = True

                '    HyperLink1.Visible = True
                '    HyperLink1.Text = "DOMANDA ERP"
                '    HyperLink1.NavigateUrl = "../CAMBI/CORRELAZIONI.ASPX?CF=" & CF & "&ID=0"

                '    HyperLink2.Visible = True
                '    'HyperLink2.NavigateUrl = "../CAMBI/StatoDomanda.ASPX?ID=" & myReader("domanda")
                '    HyperLink2.Attributes.Add("onClick", "javascript:window.open('../CAMBI/StatoDomanda.aspx?ID=" & myReader("domanda") & "','Stato','height=370,top=0,left=0,width=480,scrollbars=no');")

                'End If
                'myReader.Close()

                'imgPreferenze.Attributes.Add("OnClick", "javascript:window.open('Preferenze.aspx?" & "ID=" & lIdDomanda & "&PG=" & lblPG.Text & "','Preferenze','top=0,left=0,width=600,height=400');")
                'btnStampa.Attributes.Add("OnClick", "javascript:window.open('ListaAlloggi.aspx?" & "ID=" & lIdDomanda & "','Alloggi');")

                par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_TIPO_RIFIUTO_ALL ORDER BY COD ASC", "DESCRIZIONE", "COD")
                cmbStato.Items.Add(" ")
                cmbStato.Items.FindByText(" ").Selected = True

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            Else
                myReader.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.Message & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try
    End Function


    Private Function VisualizzaDomanda()
        Dim scriptblock As String
        Dim ID_Dichiarazione As Long
        Dim CF As String

        Try
            DropDownList1.Visible = False


            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE1111", par.OracleConn)

            lblOfferta.Text = "Offerta N° " & lIdOfferta
            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                myReader.Close()
                par.cmd.CommandText = "SELECT DICHIARAZIONI.PG AS ""PGDIC"", trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO.DATA_PG,DOMANDE_BANDO.id_bando,DOMANDE_BANDO.PG,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.COD_FISCALE,DOMANDE_BANDO.tipo_pratica,domande_bando.FL_ASS_ESTERNA,DOMANDE_BANDO.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF.POSIZIONE,trunc(BANDI_GRADUATORIA_DEF.ISBARC_R,4) AS ""GRAD"",DOMANDE_BANDO.ID_DICHIARAZIONE FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI WHERE DICHIARAZIONI.ID=DOMANDE_BANDO.ID_DICHIARAZIONE AND DOMANDE_BANDO.ID=BANDI_GRADUATORIA_DEF.ID_DOMANDA (+) AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND DOMANDE_BANDO.ID=" & lIdDomanda
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    If par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "0" Or par.IfNull(myReader1("FL_ASS_ESTERNA"), "") = "2" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE... Questa domanda non è stata data in gestione ad ente esterno per assegnazione e non può essere modificata!!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                        End If
                        btnAccetta.Enabled = False
                        btnRifiuta.Enabled = False
                    End If

                    CF = par.IfNull(myReader1("COD_FISCALE"), "")
                    ID_Dichiarazione = par.IfNull(myReader1("ID_DICHIARAZIONE"), 0)
                    lblPG.Text = par.IfNull(myReader1("PG"), "")
                    lblPGDic.Text = par.IfNull(myReader1("PGDIC"), "")

                    If Len(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")) > 25 Then
                        lblNominativo.Text = Mid(par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), ""), 1, 23) & "..."
                        lblNominativo.ToolTip = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    Else
                        lblNominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    End If


                    lblIsbarcr.Text = par.IfNull(myReader1("isbarc_r"), "0")
                    lblTipoPratica.Text = par.IfNull(myReader1("TIPO_PRATICA"), "0")
                    Label20.Text = par.IfNull(myReader1("POSIZIONE"), "0")
                    Label18.Text = par.IfNull(myReader1("GRAD"), "0")

                    Label23.Text = par.IfNull(myReader1("COGNOME"), "")
                    Label24.Text = par.IfNull(myReader1("NOME"), "")
                    Label25.Text = CF
                    Label26.Text = par.IfNull(myReader1("ID_DICHIARAZIONE"), "-1")

                    'If par.IfNull(myReader1("TIPO_ALLOGGIO"), "0") = "0" Then
                    '    Label22.Text = "A"
                    'Else
                    '    Label22.Text = "B"
                    'End If

                    lblPGDic.Attributes.Add("onclick", "javascript:window.open('../max.aspx?ID=" & ID_Dichiarazione & "&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    lblPG.Attributes.Add("onclick", "javascript:window.open('../domanda.aspx?ID=" & lIdDomanda & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")
                    lblVisDoc.Attributes.Add("onclick", "javascript:window.open('../EstremiDocumento.aspx?T=1&ID=" & lIdDomanda & "&I=" & par.Cripta(lblNominativo.Text) & "','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');")


                    par.cmd.CommandText = "SELECT  * FROM EVENTI_BANDI WHERE ID_DOMANDA=" & lIdDomanda & " AND COD_EVENTO='F136'"
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader11.Read() Then
                        If par.IfNull(myReader11("cod_evento"), "") <> "" Then
                            'MyExecuteSql("update bandi_graduatoria set tipo=1 where id_domanda=" & Id_Pratica)
                            DropDownList1.Visible = True

                            Label16.Text = "E' stata rilevata una deroga accolta in data " & par.FormattaData(Mid(myReader11("data_ora"), 1, 8)) & ". Procedere con.."
                            'Else
                            '   MyExecuteSql("update bandi_graduatoria set tipo=0 where id_domanda=" & Id_Pratica)
                            '  MsgBox("Si procede con Assegnazione REGOLARE", vbInformation)
                            ' ScriviEventoBANDI(Id_Pratica, 162, 9, 0)
                        End If
                    Else
                        DropDownList1.Visible = False

                        Label16.Text = ""
                    End If
                    myReader11.Close()



                End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & lIdDomanda
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lblComp.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()

                par.cmd.CommandText = "SELECT id,cod_alloggio,data_disponibilita,fl_mod from alloggi where id_pratica=" & lIdDomanda & " and prenotato='1' ORDER BY ID DESC"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read() Then
                        lblIdAll.Text = par.IfNull(myReader2("id"), "-1")
                        lblData.Text = par.FormattaData(par.IfNull(myReader2("data_disponibilita"), ""))
                    TipoAssegnazione = par.IfNull(myReader2("fl_mod"), "0")
                    If TipoAssegnazione = "0" Then
                        Label22.Text = "A"
                    Else
                        Label22.Text = "B"
                    End If
                    par.cmd.CommandText = "SELECT id from siscom_MI.unita_immobiliari where cod_unita_immobiliare='" & Trim(par.IfNull(myReader2("cod_alloggio"), "-1")) & "'"
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader3.Read() Then
                            lIdUnita = par.IfNull(myReader3(0), -1)

                            par.cmd.CommandText = "select *  from SISCOM_MI.unita_stato_manutentivo where id_unita=" & lIdUnita
                            Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader123.HasRows = False Then
                            Response.Write("<script>alert('Attenzione, non è possibile procedere perchè manca verifica dello stato manutentivo!');window.close();</script>")
                            btnAccetta.Enabled = False
                            btnRifiuta.Enabled = False
                            DropDownList1.Enabled = False
                            cmbStato.Enabled = False
                            Else
                                If myReader123.Read = True Then
                                If par.IfNull(myReader123("riassegnabile"), "0") = "0" Then
                                    Response.Write("<script>alert('Attenzione, non è possibile procedere perchè questa unità è INAGIBILE secondo quanto descritto nell\'ultima verifica dello stato manutentivo!');window.close();</script>")
                                    btnAccetta.Enabled = False
                                    btnRifiuta.Enabled = False
                                    DropDownList1.Enabled = False
                                    cmbStato.Enabled = False
                                End If

                                If par.IfNull(myReader123("tIPO_riassegnabile"), "0") = "2" Then
                                    Response.Write("<script>alert('Attenzione, si ricorda che questa unità è AGIBILE ma non RIASSEGNABILE (si può inserire il rapporto ma non lo si può attivare) secondo quanto descritto nell\'ultima verifica dello stato manutentivo!');</script>")
                                End If

                                End If
                            End If
                            myReader123.Close()


                            If lIdUnita <> -1 Then
                                LBLMANUTENTIVO.Text = "<a href='../CENSIMENTO/VerificaSManutentivo.aspx?ID=" & lIdUnita & "&L=2' target='_blank'>Stato Manutentivo'</a>"
                            End If
                        End If
                        myReader3.Close()




                    par.cmd.CommandText = "SELECT GESTIONE_ALLOGGI.TIPO,GESTIONE_ALLOGGI.COD,t_TIPO_GESTORE.DESCRIZIONE,GESTIONE_ALLOGGI.SEDE,GESTIONE_ALLOGGI.TELEFONO,T_COND_ALLOGGIO.descrizione as ""stato"",t_tipo_indirizzo.descrizione as ""tipoindirizzo"",T_TIPO_ALL_ERP.descrizione as ""tipoalloggio"",alloggi.*,T_TIPO_PROPRIETA.descrizione as ""proprieta"" FROM GESTIONE_ALLOGGI,T_TIPO_GESTORE,T_COND_ALLOGGIO,t_tipo_indirizzo,T_TIPO_ALL_ERP,alloggi,T_TIPO_PROPRIETA WHERE t_TIPO_GESTORE.COD=GESTIONE_ALLOGGI.COD_GESTORE AND GESTIONE_ALLOGGI.COD_PROPRIETA=alloggi.proprieta and gestione_alloggi.cod_GESTORE=alloggi.gestione (+) and  alloggi.stato=T_COND_ALLOGGIO.cod (+) and alloggi.tipo_indirizzo=t_tipo_indirizzo.cod (+) and alloggi.tipo_alloggio=T_TIPO_ALL_ERP.cod (+) and alloggi.proprieta=T_TIPO_PROPRIETA.cod (+) and alloggi.ID=" & par.IfNull(myReader2("id"), "-1")
                        myReader3 = par.cmd.ExecuteReader()

                        If myReader3.Read() Then
                            lblCodice.Text = par.IfNull(myReader3("COD_ALLOGGIO"), "")
                            lblZona.Text = par.IfNull(myReader3("zona"), "")
                            lblProprieta.Text = par.IfNull(myReader3("proprieta"), "")
                            lblStato.Text = par.IfNull(myReader3("stato"), "")
                            Data_Prenotazione = par.IfNull(myReader3("DATA_PRENOTATO"), "")
                            If par.IfNull(myReader3("TIPO"), "") = "0" Then
                            lblGestore.Text = Mid(par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - ERP", 1, 25)
                            Else
                            lblGestore.Text = Mid(par.IfNull(myReader3("DESCRIZIONE"), "") & " - " & par.IfNull(myReader3("SEDE"), "") & " - " & par.IfNull(myReader3("TELEFONO"), "") & " - EQ", 1, 25)
                            End If
                        End If
                        myReader3.Close()
                    Else
                        cmbStato.Enabled = False
                        txtNote.Enabled = False
                        btnAccetta.Enabled = False
                    btnRifiuta.Enabled = False
                    Label22.Text = ""

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio non più disponibile per questa assegnazione!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    End If

                    myReader2.Close()

                    par.cmd.CommandText = "SELECT id from rel_prat_all_ccaa_erp where id_pratica=" & lIdDomanda & " and ultimo='1'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read() Then
                        lblRelazione.Text = myReader2("id")
                    Else
                        lblRelazione.Text = "1"
                    End If
                    myReader2.Close()

                    myReader.Close()

                    'imgPreferenze.Attributes.Add("OnClick", "javascript:window.open('Preferenze.aspx?" & "ID=" & lIdDomanda & "&PG=" & lblPG.Text & "','Preferenze','top=0,left=0,width=600,height=400');")
                    'btnStampa.Attributes.Add("OnClick", "javascript:window.open('ListaAlloggi.aspx?" & "ID=" & lIdDomanda & "','Alloggi');")

                    par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_TIPO_RIFIUTO_ALL ORDER BY COD ASC", "DESCRIZIONE", "COD")
                    cmbStato.Items.Add(" ")
                    cmbStato.Items.FindByText(" ").Selected = True


                    par.cmd.CommandText = "SELECT DICHIARAZIONI_CAMBI.ID as ""dichiarazione"",domande_bando_cambi.id as ""domanda"" FROM domande_bando_cambi,DICHIARAZIONI_CAMBI,COMP_NUCLEO_CAMBI WHERE domande_bando_cambi.id_dichiarazione=dichiarazioni_cambi.id and DICHIARAZIONI_CAMBI.ID=COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_CAMBI.COD_FISCALE='" & CF & "' OR COMP_NUCLEO_CAMBI.COD_FISCALE='" & CF & "')"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Image1.Visible = True

                        HyperLink1.Visible = True
                        HyperLink1.Text = "DOMANDA CAMBI"
                        HyperLink1.NavigateUrl = "../CAMBI/CORRELAZIONI1.ASPX?CF=" & CF & "&ID=0"

                        HyperLink2.Visible = True
                        'HyperLink2.NavigateUrl = "../CAMBI/StatoDomandaCambi.ASPX?ID=" & myReader("domanda")
                        HyperLink2.Attributes.Add("onClick", "javascript:window.open('../CAMBI/StatoDomandaCambi.aspx?ID=" & myReader("domanda") & "','Stato','height=370,top=0,left=0,width=480,scrollbars=no');")

                    End If
                    myReader.Close()




                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)

                    Session.Add("LAVORAZIONE", "1")

                Else
                    myReader.Close()
                End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile procedere in questo momento!');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & EX1.ToString & "');history.go(-1);" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('" & ex.ToString & "');history.go(-1);" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If
        End Try
    End Function

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsci.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                'par.OracleConn.Close()
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE1111"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE1111"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.myTrans.Rollback()

                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE1111")
                HttpContext.Current.Session.Remove("CONNESSIONE1111")
                Session.Item("LAVORAZIONE") = "0"
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Catch EX As Exception
            Session.Item("LAVORAZIONE") = "0"
            Page.Dispose()
            Response.Write("<script>document.location.href=""../ErrorPage.aspx""</script>")
        Finally

        End Try
    End Sub


    Protected Sub btnRifiuta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRifiuta.Click

        Try
            If btnRifiuta.Text = "RIFIUTA ALLOGGIO" Then

                If cmbStato.SelectedItem.Value = " " And txtNote.Text = "" Then
                    Response.Write("<script>alert('Motivo dell\'esclusione non specificato!');</script>")
                    Exit Sub
                End If

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE1111"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE1111"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'If par.OracleConn.State = Data.ConnectionState.Closed Then
                '    par.OracleConn.Open()
                'End If




                par.cmd.CommandText = "UPDATE DOMANDE_OFFERTE_SCAD SET FL_VALIDA='0' WHERE ID=" & lIdOfferta
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0',DATA_PRENOTATO='' WHERE ID=" & lblIdAll.Text
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='0',MOTIVAZIONE='" & par.PulisciStrSql(cmbStato.SelectedItem.Text) & " - Note: " & par.PulisciStrSql(txtNote.Text) & "' WHERE ID=" & lblRelazione.Text
                par.cmd.ExecuteNonQuery()




                par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                       & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                       & "0,5," _
                                       & lblIdAll.Text & "," _
                                       & lIdDomanda & ",'" & par.PulisciStrSql(cmbStato.SelectedItem.Text) & "')"
                par.cmd.ExecuteNonQuery()



                'SCRIVI_MOVIMENTO1:

                par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read = False Then
                    par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                    par.cmd.ExecuteNonQuery()
                End If
                myReader.Close()
                par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI-1,DISPONIBILI=DISPONIBILI+1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                par.cmd.ExecuteNonQuery()

                Select Case HiddenField1.Value
                    Case "1"
                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                        & "','F16','','I')"
                        par.cmd.ExecuteNonQuery()

                        If par.PulisciStrSql(cmbStato.SelectedItem.Text) <> "" Then
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_INVITO='0',FL_PROPOSTA='0' WHERE ID=" & lIdDomanda
                            par.cmd.ExecuteNonQuery()
                        End If
                    Case "2"
                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                        & "','F16','','I')"
                        par.cmd.ExecuteNonQuery()

                        If par.PulisciStrSql(cmbStato.SelectedItem.Text) <> "" Then
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_INVITO='0',FL_PROPOSTA='0' WHERE ID=" & lIdDomanda
                            par.cmd.ExecuteNonQuery()
                        End If
                    Case "3"
                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                        & "','F16','','I')"
                        par.cmd.ExecuteNonQuery()

                        If par.PulisciStrSql(cmbStato.SelectedItem.Text) <> "" Then
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_INVITO='0',FL_PROPOSTA='0' WHERE ID=" & lIdDomanda
                            par.cmd.ExecuteNonQuery()
                        End If
                End Select

                If lIdDomanda < 500000 Then

                Else

                End If



                par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                    & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & lblPG.Text & "','" & LblDataPG.Text & "',10," & lblTipoPratica.Text & ",10)"
                par.cmd.ExecuteNonQuery()


                Dim scriptblock As String

                Session.Add("NOTEABBINAMENTO", txtNote.Text)
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "window.open('ReportAbbinamento_1.aspx?T=" & HiddenField1.Value & "&ABB=" & lIdOfferta & "&IDALL=" & lblIdAll.Text & "&DATAS=" & lblScad.Text & "&RISP=0&MOT=" & par.VaroleDaPassare(cmbStato.SelectedItem.Text) & "&PG=" & lblPG.Text & "&NOM=" & par.VaroleDaPassare(lblNominativo.Text) & "&DATAP=" & Data_Prenotazione & "','Report');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                End If
                btnRifiuta.Text = "Visualizza Rifiuto"
                cmbStato.Enabled = False
                txtNote.Enabled = False
                btnAccetta.Enabled = False

                par.myTrans.Commit()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)
            Else
                Dim scriptblock As String

                Session.Add("NOTEABBINAMENTO", txtNote.Text)
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "window.open('ReportAbbinamento_1.aspx?ABB=" & lIdOfferta & "&IDALL=" & lblIdAll.Text & "&DATAS=" & lblScad.Text & "&RISP=0&MOT=" & par.VaroleDaPassare(cmbStato.SelectedItem.Text) & "&PG=" & lblPG.Text & "&NOM=" & par.VaroleDaPassare(lblNominativo.Text) & "&DATAP=" & Data_Prenotazione & "','Report');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                End If
            End If
        Catch ex As Exception
            Label6.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnAccetta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccetta.Click
        Dim scriptblock As String
        Dim CODICE As String = ""
        Dim PROPRIETA As String = ""

        Try
            If btnAccetta.Text <> "Visualizza Accettaz." Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE1111"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE1111"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                End If

                par.cmd.CommandText = "SELECT * FROM ALLOGGI WHERE ID=" & lblIdAll.Text
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    CODICE = par.IfNull(myReader("COD_ALLOGGIO"), "")
                    PROPRIETA = par.IfNull(myReader("PROPRIETA"), "")

                    If par.IfNull(myReader("STATO"), "") = "7" And par.IfNull(myReader("ID_PRATICA"), -1) = lIdDomanda Then

                        par.cmd.CommandText = "UPDATE DOMANDE_OFFERTE_SCAD SET FL_VALIDA='0' WHERE ID=" & lIdOfferta
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=8,PRENOTATO='1',ID_PRATICA=" & lIdDomanda & ",ASSEGNATO='1',DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "',DATA_RESO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & lblIdAll.Text
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='1' WHERE Id = " & lblRelazione.Text
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                   & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                   & "1,8," _
                                   & lblIdAll.Text & "," _
                                   & lIdDomanda & ",'')"

                        par.cmd.ExecuteNonQuery()

                        Select Case HiddenField1.Value
                            Case "1"
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET ID_STATO='9',NUM_ALLOGGIO='" & lblCodice.Text & "' WHERE ID=" & lIdDomanda
                            Case "2"
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET ID_STATO='9',NUM_ALLOGGIO='" & lblCodice.Text & "' WHERE ID=" & lIdDomanda
                            Case "3"
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET ID_STATO='9',NUM_ALLOGGIO='" & lblCodice.Text & "' WHERE ID=" & lIdDomanda
                        End Select


                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read = False Then
                            par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI-1,ASSEGNATI=ASSEGNATI+1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                            & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,20,'" & lblPG.Text & "','',10," & lblTipoPratica.Text & ",10)"
                        par.cmd.ExecuteNonQuery()

                        If DropDownList1.Visible = True Then
                            If DropDownList1.SelectedItem.Value = "0" Then
                                Select Case HiddenField1.Value
                                    Case "1"
                                        par.cmd.CommandText = "update bandi_graduatoria set tipo=1 where id_domanda=" & lIdDomanda
                                        par.cmd.ExecuteNonQuery()
                                    Case "2"
                                        par.cmd.CommandText = "update bandi_graduatoria_DEF set tipo=1 where id_domanda=" & lIdDomanda
                                        par.cmd.ExecuteNonQuery()

                                End Select

                            Else
                                Select Case HiddenField1.Value
                                    Case "1"
                                        par.cmd.CommandText = "update bandi_graduatoria set tipo=0 where id_domanda=" & lIdDomanda
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "update bandi_graduatoria_def set tipo=0 where id_domanda=" & lIdDomanda
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                        & "','F162','','I')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F13','','I')"
                                        par.cmd.ExecuteNonQuery()
                                    Case "2"
                                        par.cmd.CommandText = "update bandi_graduatoria_CAMBI set tipo=0 where id_domanda=" & lIdDomanda
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F162','','I')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F13','','I')"
                                        par.cmd.ExecuteNonQuery()
                                    Case "3"

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F162','','I')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                                            & "','F13','','I')"
                                        par.cmd.ExecuteNonQuery()
                                End Select

                                If lIdDomanda < 500000 Then

                                Else

                                End If

                            End If
                        End If

                        If PROPRIETA = "0" Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & CODICE & "'"
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read = True Then
                                CODICE = par.IfNull(myReader1("ID"), "-1")
                            Else
                                Response.Write("<script>alert('Errore durante il caricamento. Questa unità non è presente nel patrimonio!');</script>")
                                CODICE = ""
                                par.myTrans.Rollback()
                                par.myTrans = par.OracleConn.BeginTransaction()
                                HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)

                            End If
                            myReader1.Close()

                            If CODICE <> "" Then
                                Dim TIPO As String = ""
                                Select Case HiddenField1.Value
                                    Case "1"
                                        TIPO = "E"
                                    Case "2"
                                        TIPO = "C"
                                    Case "3"
                                        TIPO = "Y"
                                End Select


                                par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_ASSEGNATE " _
                                                    & "(ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, GENERATO_CONTRATTO, ID_DICHIARAZIONE, " _
                                                    & "COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,DATA_DISPONIBILITA) " _
                                                    & "Values " _
                                                    & "(" & lIdDomanda & ", " & CODICE & ", '" & Format(Now, "yyyyMMdd") & "',0, " & Label26.Text & " , " _
                                                    & "'" & par.PulisciStrSql(Label23.Text) & "', '" _
                                                    & par.PulisciStrSql(Label24.Text) & "', '" _
                                                    & par.PulisciStrSql(Label25.Text) & "', '" & TIPO & "', " & lIdOfferta & ",'" & par.AggiustaData(lblData.Text) & "')"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Else
                            par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_ASSEGNATE " _
                                            & "(ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, GENERATO_CONTRATTO, ID_DICHIARAZIONE, " _
                                            & "COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,DATA_DISPONIBILITA) " _
                                            & "Values " _
                                            & "(" & lIdDomanda & ", " & lblIdAll.Text & ", '" & Format(Now, "yyyyMMdd") & "',0, " & Label26.Text & " , " _
                                            & "'" & par.PulisciStrSql(Label23.Text) & "', '" _
                                            & par.PulisciStrSql(Label24.Text) & "', '" _
                                            & par.PulisciStrSql(Label25.Text) & "', 'G', " & lIdOfferta & ",'" & par.AggiustaData(lblData.Text) & "')"
                            par.cmd.ExecuteNonQuery()
                        End If

                        If CODICE <> "" Then
                            btnAccetta.Text = "Visualizza Accettaz."
                            cmbStato.Enabled = False
                            txtNote.Enabled = False
                            btnRifiuta.Enabled = False

                            Session.Add("NOTEABBINAMENTO", txtNote.Text)
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "window.open('ReportAbbinamento_1.aspx?T=" & HiddenField1.Value & "&ABB=" & lIdOfferta & "&IDALL=" & lblIdAll.Text & "&DATAS=" & lblScad.Text & "&RISP=1&MOT=&PG=" & lblPG.Text & "&NOM=" & par.VaroleDaPassare(lblNominativo.Text) & "&DATAP=" & Data_Prenotazione & "','Report');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                            End If
                            CalcolaStampa()
                        End If
                    Else
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Alloggio non più disponibile o prenotato-assegnato ad altro assegnatario');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                        End If
                    End If
                End If
                myReader.Close()
                'par.myTrans.Rollback()
                par.myTrans.Commit()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)
            Else
                Session.Add("NOTEABBINAMENTO", txtNote.Text)
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "window.open('ReportAbbinamento_1.aspx?T=" & HiddenField1.Value & "&ABB=" & lIdOfferta & "&IDALL=" & lblIdAll.Text & "&DATAS=" & lblScad.Text & "&RISP=1&MOT=&PG=" & lblPG.Text & "&NOM=" & par.VaroleDaPassare(lblNominativo.Text) & "&DATAP=" & Data_Prenotazione & "','Report');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                End If

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE1111"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE1111"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                End If
                CalcolaStampa()
            End If
        Catch ex As Exception
            Label6.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)
        End Try
    End Sub

    Function CalcolaStampa()

        Dim Tr As String

        Dim DATI_ANAGRAFICI As String
        Dim DATI_NUCLEO As String
        Dim SPESE_SOSTENUTE As String
        Dim PATRIMONIO_MOB As String
        Dim PATRIMONIO_IMMOB As String
        Dim REDDITO_NUCLEO As String
        Dim dichiarante As String
        Dim DATI_DICHIARANTE As String
        Dim REDDITO_IRPEF As String
        Dim REDDITO_DETRAZIONI As String
        Dim ANNO_SIT_ECONOMICA As String = ""
        Dim CAT_CATASTALE As String
        Dim IMMAGINE_A As String
        Dim IMMAGINE_B As String
        Dim IMMAGINE_C As String
        Dim IMMAGINE_C1 As String
        Dim IMMAGINE_D As String
        Dim LUOGO As String
        Dim SDATA As String
        Dim LUOGO_REDDITO As String
        Dim DATA_REDDITO As String
        Dim numero As String
        Dim sStringasql As String


        Dim i As Integer

        Try



            par.cmd.CommandText = "SELECT * FROM domande_bando WHERE domande_bando.id=" & lIdDomanda
            Dim myReader9 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader9.Read Then


                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘‘par.cmd.Transaction = par.myTrans


                DATI_ANAGRAFICI = "<BR>   Domanda Protocollo:   <I>" & lblPG.Text & "</I>   " _
                                    & ", Nominativo:   <I>" & lblNominativo.Text & "</I><BR>" _
                                    & "<I>" & lblOfferta.Text & "</I>   , " _
                                    & "<BR>"



                DATI_NUCLEO = ""

                i = 1
                par.cmd.CommandText = "SELECT comp_nucleo.*,t_tipo_parentela.descrizione as ""parente"" FROM comp_nucleo,domande_bando,t_tipo_parentela,dichiarazioni WHERE comp_nucleo.id_dichiarazione=dichiarazioni.id and comp_nucleo.grado_parentela=t_tipo_parentela.cod (+) and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda & " order by comp_nucleo.progr asc"
                Dim myReader10 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader10.Read
                    DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                        & "<td width=5%><small><small>    <center>" & i & "</center></small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & par.IfNull(myReader10("cod_fiscale"), "") & "</I>   </small></small></td>" _
                        & "<td width=20%><small><small>   <I>" & par.IfNull(myReader10("cognome"), "") & "</I>   </small></small></td>" _
                        & "<td width=20%><small><small>   <I>" & par.IfNull(myReader10("nome"), "") & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & par.FormattaData(par.IfNull(myReader10("data_nascita"), "")) & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & par.IfNull(myReader10("parente"), "") & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & par.IfNull(myReader10("perc_inval"), "") & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & par.IfNull(myReader10("indennita_acc"), "") & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & par.IfNull(myReader10("usl"), "") & "</I>   </small></small></td>" _
                        & "</tr>"
                    i = i + 1
                End While
                myReader10.Close()



                SPESE_SOSTENUTE = ""

                i = 1
                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,domande_bando,dichiarazioni WHERE comp_nucleo.id_dichiarazione=dichiarazioni.id and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda & " order by comp_nucleo.progr asc"
                myReader10 = par.cmd.ExecuteReader()
                While myReader10.Read
                    par.cmd.CommandText = "select importo from comp_elenco_spese where id_componente=" & myReader10("id")
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader11.Read
                        SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                                        & "<td width=50%><small><small><CENTER>" & par.IfNull(myReader10("cognome"), "") & " " & par.IfNull(myReader10("nome"), "") & "</CENTER></small></small></td>" _
                                        & "<td align=right width=50%><small><small>   <I>" & par.IfNull(myReader11("importo"), "0") & ",00" & "</I></small></small></td>" _
                                        & "</tr>"
                    End While
                    myReader11.Close()
                End While
                myReader10.Close()




                PATRIMONIO_MOB = ""

                i = 1
                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,domande_bando,dichiarazioni WHERE comp_nucleo.id_dichiarazione=dichiarazioni.id and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda & " order by comp_nucleo.progr asc"
                myReader10 = par.cmd.ExecuteReader()
                While myReader10.Read
                    par.cmd.CommandText = "select * from comp_patr_mob where id_componente=" & myReader10("id")
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader11.Read
                        PATRIMONIO_MOB = PATRIMONIO_MOB _
                                       & "<tr>" _
                                       & "<td width=25%><small><small><center>   <I>" & par.IfNull(myReader10("cognome"), "") & " " & par.IfNull(myReader10("nome"), "") & "</I>   </center></small></small></td>" _
                                       & "<TD  width=25%><small><small>   <I>" & par.IfNull(myReader11("cod_intermediario"), "") & "</I>   </small></small></td>" _
                                       & "<td width=50%><small><small>   <I>" & par.IfNull(myReader11("intermediario"), "") & "</I>   </small></small></td>" _
                                       & "<TD  align=right  width=50%><small><small>   <I>" & par.IfNull(myReader11("importo"), "") & ",00</I></small></small></td>" _
                                       & "</tr>"
                    End While
                    myReader11.Close()
                End While
                myReader10.Close()


                PATRIMONIO_IMMOB = ""


                i = 1
                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,domande_bando,dichiarazioni WHERE comp_nucleo.id_dichiarazione=dichiarazioni.id and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda & " order by comp_nucleo.progr asc"
                myReader10 = par.cmd.ExecuteReader()
                While myReader10.Read
                    par.cmd.CommandText = "select * from comp_patr_immob where id_componente=" & myReader10("id")
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader11.Read
                        Tr = ""
                        Select Case par.IfNull(myReader11("id_tipo"), "")
                            Case "0"
                                Tr = "Fabbricati"
                            Case "1"
                                Tr = "Terreni Agr."
                            Case "2"
                                Tr = "Terreni Edif."
                        End Select


                        PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                            & "<tr>" _
                            & "<td><small><small><center>   <I>" & par.IfNull(myReader10("cognome"), "") & " " & par.IfNull(myReader10("nome"), "") & "</I>   </center></small></small></td>" _
                            & "<td><small><small>   <I>" & Tr & "</I>   </small></small></td>" _
                            & "<td><small><small><p align=right>   <I>" & par.IfNull(myReader11("perc_patr_immobiliare"), "") & "</I>   %   </p></small></small></td>" _
                            & "<td><small><small><p align=right>   <I>" & par.IfNull(myReader11("valore"), "") & ",00</I>   </p></small></small></td>" _
                            & "<td><small><small><p align=right>   <I>" & par.IfNull(myReader11("mutuo"), "") & ",00</I>   </p></small></small></td>" _
                            & "<td><small><small>   <I></I><center>" & par.IfNull(myReader11("f_residenza"), "") & "</center><I></I>   </small></small></td>" _
                            & "</tr>"
                    End While
                    myReader11.Close()
                End While
                myReader10.Close()

                par.cmd.CommandText = "SELECT t_tipo_categorie_immobile.descrizione FROM domande_bando,dichiarazioni,t_tipo_categorie_immobile WHERE dichiarazioni.ID_TIPO_CAT_AB = t_tipo_categorie_immobile.cod (+) and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda
                myReader10 = par.cmd.ExecuteReader()
                If myReader10.Read Then
                    CAT_CATASTALE = par.IfNull(myReader10("descrizione"), "")
                Else
                    CAT_CATASTALE = ""
                End If
                myReader10.Close()


                REDDITO_NUCLEO = ""

                i = 1
                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,domande_bando,dichiarazioni WHERE comp_nucleo.id_dichiarazione=dichiarazioni.id and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda & " order by comp_nucleo.progr asc"
                myReader10 = par.cmd.ExecuteReader()
                While myReader10.Read
                    par.cmd.CommandText = "select * from comp_reddito where id_componente=" & myReader10("id")
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader11.Read
                        REDDITO_NUCLEO = REDDITO_NUCLEO _
                        & "<tr>" _
                        & "<td><small><small><center>   <I>" & par.IfNull(myReader10("cognome"), "") & " " & par.IfNull(myReader10("nome"), "") & "</I>   </center></small></small></td>" _
                        & "<td><small><small><p align=right>   <I>" & par.IfNull(myReader11("reddito_irpef"), "") & ",00</I>   </small></small></p></td>" _
                        & "<td><small><small><p align=right>   <I>" & par.IfNull(myReader11("prov_agrari"), "") & ",00</I>   </small></small></p></td>" _
                        & "</tr>"
                    End While
                    myReader11.Close()
                End While
                myReader10.Close()



                IMMAGINE_A = ""
                IMMAGINE_B = ""

                If PATRIMONIO_MOB <> "" Then
                    IMMAGINE_C = ""
                Else
                    IMMAGINE_C = ""
                End If

                If PATRIMONIO_IMMOB <> "" Then
                    IMMAGINE_C1 = ""
                Else
                    IMMAGINE_C1 = ""
                End If

                If REDDITO_NUCLEO <> "" Then
                    IMMAGINE_D = ""
                Else
                    IMMAGINE_D = ""
                End If

                LUOGO = ""
                SDATA = ""
                dichiarante = " "
                DATI_DICHIARANTE = ""


                REDDITO_IRPEF = ""
                i = 1
                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,domande_bando,dichiarazioni WHERE comp_nucleo.id_dichiarazione=dichiarazioni.id and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda & " order by comp_nucleo.progr asc"
                myReader10 = par.cmd.ExecuteReader()
                While myReader10.Read
                    par.cmd.CommandText = "select * from comp_altri_redditi where id_componente=" & myReader10("id")
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader11.Read
                        REDDITO_IRPEF = REDDITO_IRPEF _
                        & "<tr>" _
                        & "<td width=40%><small><small><center>   <I>" & par.IfNull(myReader10("cognome"), "") & " " & par.IfNull(myReader10("nome"), "") & "</I>   </center></small></small></td>" _
                        & "<TD  width=505%><small><small><p align=right>   <I>" & par.IfNull(myReader11("importo"), "") & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                    End While
                    myReader11.Close()
                End While
                myReader10.Close()



                REDDITO_DETRAZIONI = ""
                i = 1
                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,domande_bando,dichiarazioni WHERE comp_nucleo.id_dichiarazione=dichiarazioni.id and domande_bando.id_dichiarazione=dichiarazioni.id and domande_bando.id=" & lIdDomanda & " order by comp_nucleo.progr asc"
                myReader10 = par.cmd.ExecuteReader()
                While myReader10.Read
                    par.cmd.CommandText = "select * from comp_detrazioni where id_componente=" & myReader10("id")
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader11.Read
                        Tr = ""
                        Select Case myReader11("id_tipo")
                            Case 0
                                Tr = "IRPEF"
                            Case 1
                                Tr = "Spese Sanitarie"
                            Case 2
                                Tr = "Spese per ricorvero in strutture sociosanitarie"
                        End Select
                        REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & par.IfNull(myReader10("cognome"), "") & " " & par.IfNull(myReader10("nome"), "") & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & Tr & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & par.IfNull(myReader11("importo"), "0") & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                    End While
                    myReader11.Close()
                End While
                myReader10.Close()

                LUOGO_REDDITO = ""

                par.cmd.CommandText = "select * from dichiarazioni where id=" & myReader9("id_dichiarazione")
                Dim myReader13 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader13.Read Then
                    DATA_REDDITO = par.IfNull(myReader13("LUOGO_INT_ERP"), "")
                    ANNO_SIT_ECONOMICA = par.IfNull(myReader13("anno_sit_economica"), "")

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & par.IfNull(myReader13("ID_LUOGO_RES_DNTE"), -1)
                    Dim myReader14 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader14.Read Then
                        If par.IfNull(myReader14("SIGLA"), "") = "E" Or par.IfNull(myReader14("SIGLA"), "") = "C" Then
                            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & par.IfNull(myReader14("NOME"), "") & "</I>   , " _
                            & "STATO ESTERO:   <I>" & par.IfNull(myReader14("NOME"), "") & "</I><BR>"
                        Else
                            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & par.IfNull(myReader14("NOME"), "") & "</I>   , " _
                            & "PROVINCIA:   <I>" & par.IfNull(myReader14("SIGLA"), "") & "</I><BR>"
                        End If
                    End If
                    myReader14.Close()
                    par.cmd.CommandText = "select descrizione from t_tipo_INDIRIZZO where cod=" & par.IfNull(myReader13("ID_TIPO_IND_RES_DNTE"), -1)
                    myReader14 = par.cmd.ExecuteReader()
                    If myReader14.Read Then
                        DATI_ANAGRAFICI = DATI_ANAGRAFICI & "INDIRIZZO:   <I>" & par.IfNull(myReader14("descrizione"), "") & " " & par.IfNull(myReader13("IND_RES_DNTE"), "") & "</I>   ," _
                            & "N. CIVICO:   <I>" & par.IfNull(myReader13("CIVICO_RES_DNTE"), "") & "</I>   , CAP:   <I>" & par.IfNull(myReader13("CAP_RES_DNTE"), "") & "</I>"
                    End If
                    myReader14.Close()
                    myReader13.Close()
                End If

                numero = ""


                sStringasql = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>Finestra di Stampa</title></head><BODY><UL><UL>   <NOBR></NOBR><basefont SIZE=2></UL></UL>"
                sStringasql = sStringasql & "<p align='center'><b><font size='4'></font></b><P>"
                sStringasql = sStringasql & ""
                sStringasql = sStringasql & "<NOBR></NOBR>"
                sStringasql = sStringasql & " "
                sStringasql = sStringasql & " <BR>"
                sStringasql = sStringasql & "<table width='100%'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "<img src='../IMG/logo.gif' style='z-index: 100; left: 0px; position: static; top: 0px' /></td>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "<span style='font-size: 10pt'><strong>Settore Assegnazione Alloggi di Erp</strong></span></td>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "<span style='font-size: 10pt'>Tel. 02/884.64435-36 Fax 02/884.66991</span></td>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "<BR><BR><center><table border=1 cellspacing=0 width=95%><tr><td><small>   <B>DATI ANAGRAFICI DEL RICHIEDENTE</B><BR>"
                sStringasql = sStringasql & DATI_ANAGRAFICI & "<br><br></small></td></tr></table></center>"
                sStringasql = sStringasql & "<BR><UL>   </UL><NOBR></NOBR><center>"
                sStringasql = sStringasql & "<table border=1 cellspacing=0 width=95%><tr><td><br><small>   SOGGETTI COMPONENTI IL NUCLEO FAMILIARE: richiedente"
                sStringasql = sStringasql & " componenti la famiglia anagrafica e altri soggetti considerati a carico ai fini IRPEF"
                sStringasql = sStringasql & "<BR>"
                sStringasql = sStringasql & ""
                sStringasql = sStringasql & ""
                sStringasql = sStringasql & "<center>"
                sStringasql = sStringasql & "</small>"
                sStringasql = sStringasql & "<table border=1 cellspacing=0 width=90%><tr><td>"
                sStringasql = sStringasql & "<p align='center'><small>A</small></p>"
                sStringasql = sStringasql & "</td><td>"
                sStringasql = sStringasql & "<p align='center'><small>B</small></p>"
                sStringasql = sStringasql & "</td><td colspan=2>"
                sStringasql = sStringasql & "<p align='center'><small>C</small></p>"
                sStringasql = sStringasql & "</td><td>"
                sStringasql = sStringasql & "<p align='center'><small>D</small></p>"
                sStringasql = sStringasql & "</td><td>"
                sStringasql = sStringasql & "<p align='center'><small>E</small></td><td>"
                sStringasql = sStringasql & "<p align='center'><small>F</small></td><td>"
                sStringasql = sStringasql & "<p align='center'><small>G</small></td><td>"
                sStringasql = sStringasql & "<p align='center'><small>H</small></p>"
                sStringasql = sStringasql & "</td></tr>   <small>   <tr><td bgcolor=#C0C0C0><center><small><small>N.Progr.</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE FISCALE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>COGNOME</small></small></center></td><td   bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>DATA DI NASCITA</small></small></center></td>"
                sStringasql = sStringasql & "</small>"
                sStringasql = sStringasql & "<td bgcolor=#C0C0C0>"
                sStringasql = sStringasql & "<p align='center'><small><small><small>GR. PARENTELA</small></small></small></td><td bgcolor=#C0C0C0>"
                sStringasql = sStringasql & "<p align='center'><small><small><small>&nbsp;% INVALIDITA'</small></small></small></td><td bgcolor=#C0C0C0>"
                sStringasql = sStringasql & "<p align='center'><small><small><small>INDENNITA' ACC.</small></small></small></td>   <td bgcolor=#C0C0C0><small><small><small>ASL&nbsp;</small></small></small></td></tr><UL><UL>   <NOBR></NOBR>"
                sStringasql = sStringasql & DATI_NUCLEO
                sStringasql = sStringasql & "</ul>"
                sStringasql = sStringasql & "</ul>"
                sStringasql = sStringasql & "</table></center>"
                sStringasql = sStringasql & "<BR><UL>   <NOBR></NOBR><small>   <B></B><BR></small>"
                sStringasql = sStringasql & "<p><small><b></b>"
                sStringasql = sStringasql & " <BR>"
                sStringasql = sStringasql & "</p>"
                sStringasql = sStringasql & "<table cellspacing=0 border=0 width=90%><tr><td height=18 width=35% ><small></small></td><td width=5%><table  bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right><small>   <I></I>   </p></td></tr></table></td><td width=50% ><small></small></td></tr><tr><td><small><CENTER>Spese effettivamente sostenute distinte per componente</small><table border=1 cellpadding=0 cellspacing=0 width=50%>   <tr><td width=50%><small><CENTER><b>A</b></CENTER></small></td><td align=right width=50%><small><CENTER><b>B</b></CENTER></small></td></tr>   <tr><td bgcolor=#C0C0C0 width=50%><CENTER><small><small>Nome</small></small></small></CENTER></td><small>   <td bgcolor=#C0C0C0 align=right width=50%><small><small><CENTER>SPESA</CENTER></small></small></td></tr><UL><UL>   <NOBR></NOBR>" & SPESE_SOSTENUTE & "</UL></UL>   <NOBR></NOBR></table></CENTER></td><td>&nbsp;</td><td>&nbsp;<BR>"
                sStringasql = sStringasql & "</small></td></tr><tr><td height=18 width=30% ><small></small></td><td width=5%><table  bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><small>   <small></small></small></I>   </p></td></tr></table></td><td width=55% ><small><BR>"
                sStringasql = sStringasql & "</small></td></tr><tr><td height=18 width=30% ><small></small></td><td width=5%><table  bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><small>   <small></small></small></I>   </p></td></tr></table></td><td width=55% ><small><BR>"
                sStringasql = sStringasql & "</small></td></tr></table>"
                sStringasql = sStringasql & "</ul>"
                sStringasql = sStringasql & "</td></tr></table><p style='page-break-before: always'>&nbsp;</p>"
                sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=95%><tr><td><small><br>   <B>ANNO DI RIFERIMENTO DELLA SITUAZIONE ECONOMICA</B>   :   <I>" & ANNO_SIT_ECONOMICA & "</I>   </small><br><br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><small><BR>"
                sStringasql = sStringasql & "<B>SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</B>   <B><UL><BR>CONSISTENZA DEL PATRIMONIO MOBILIARE</B>   <BR>posseduto alla data del 31 dicembre " & ANNO_SIT_ECONOMICA & "&nbsp;<BR>   <BR>   DATI SUI SOGGETTI CHE GESTISCONO IL PATRIMONIO MOBILIARE</UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td></tr>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE INTERMEDIARIO O GESTORE</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>INTERMEDIARIO O GESTORE (indicare se &egrave;  Banca"
                sStringasql = sStringasql & " Posta"
                sStringasql = sStringasql & " SIM"
                sStringasql = sStringasql & " SGR"
                sStringasql = sStringasql & " Impresa di investimento comunitaria o extracomunitaria"
                sStringasql = sStringasql & " Agente di cambio"
                sStringasql = sStringasql & " ecc.)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>VALORE INVESTIMENTO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_MOB & "<BR>"
                sStringasql = sStringasql & "</table></center>   <B><BR><UL>CONSISTENZA DEL PATRIMONIO IMMOBILIARE</B>   <BR>posseduto alla data del 31"
                sStringasql = sStringasql & " Dicembre " & ANNO_SIT_ECONOMICA
                sStringasql = sStringasql & " <br></UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TIPO DI PATRIMONIO  IMMOBILIARE</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>QUOTA POSSEDUTA (percentuale)</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>VALORE AI FINI ICI (valore della quota posseduta dell'immobile"
                sStringasql = sStringasql & " come definita ai fini ICI)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>QUOTA CAPITALE RESIDUA DEL MUTUO (valore della quota posseduta)   </small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>AD USO ABITATIVO DEL NUCLEO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"
                sStringasql = sStringasql & "</table></center><br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=90%><tr><td width=80%><p align=right><small>   Categoria catastale dell'immobile ad uso abitativo del nucleo   </small></p></td><td width=10% style='border: thin solid rgb(0"
                sStringasql = sStringasql & " 0"
                sStringasql = sStringasql & " 0)'><small><p align=center>   <I>" & CAT_CATASTALE & "</I>   </p></small></td></tr></table></center>   <br><br></small></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
                sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>REDDITO COMPLESSIVO DICHIARATO AI FINI IRPEF (1)</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>PROVENTI AGRARI DA DICHIARAZIONE IRAP (per i soli impreditori agricolil)</small></small><center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_NUCLEO & "<BR>"
                sStringasql = sStringasql & "</table></center><br>(1) al netto dei redditi agrari dell'imprenditore agricolo; compresi i redditi da lavoro prestato nelle zone di frontiera"
                sStringasql = sStringasql & "<BR><UL>"
                sStringasql = sStringasql & "<BR></UL>"
                sStringasql = sStringasql & "</I>"
                sStringasql = sStringasql & "<UL><UL>"
                sStringasql = sStringasql & "</B>"
                sStringasql = sStringasql & ""
                sStringasql = sStringasql & "</ul>"
                sStringasql = sStringasql & "</ul>"
                sStringasql = sStringasql & "</small></td></tr></table></center><BR>"
                sStringasql = sStringasql & "</center>"
                sStringasql = sStringasql & "<p>"
                sStringasql = sStringasql & "<BR><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ERP</B><BR>"
                sStringasql = sStringasql & "<B><BR><UL>ALTRI REDDITI</ul></B>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>IMPORTO REDDITO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_IRPEF & "</UL></UL><BR>"
                sStringasql = sStringasql & "</table></center>   <B><BR><UL>DETRAZIONI</ul></B>   <NOBR></NOBR><center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>TIPO DETRAZIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>IMPORTO DETRAZIONE</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_DETRAZIONI & "</UL></UL><BR>"
                sStringasql = sStringasql & "</table></center><BR>"
                sStringasql = sStringasql & "<BR><BR><CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><small><center>   <I></I>   </center></small></td><td width=33%><small><center>"
                sStringasql = sStringasql & "<I></I>   </center></small></td><td width=34%><center></center></td></tr><tr><td width=33% height=15><small><center></center></small></td><td width=33%><small><center></center></small></td><td width=34%><small><center></center></small></td></tr></table></CENTER><BR>"
                sStringasql = sStringasql & "</small>"
                sStringasql = sStringasql & "</table>"
                sStringasql = sStringasql & "<p align='left'>"
                sStringasql = sStringasql & " &nbsp;"
                sStringasql = sStringasql & "<p align='left'>"
                sStringasql = sStringasql & " "
                sStringasql = sStringasql & "<BR><BR>"
                sStringasql = sStringasql & "</center>"
                sStringasql = sStringasql & "<p align='center'><font face='Arial'></font></p>"
                sStringasql = sStringasql & "</BODY></html>"

                HttpContext.Current.Session.Add("DICHIARAZIONE", sStringasql)
                Response.Write("<script>window.open('../StampaDichiarazione.aspx','Dichiarazione','');</script>")
            End If
            myReader9.Close()
        Catch ex As Exception
            'Label4.Text = "err " & ex.Message
            'par.myTrans.Rollback()
            'par.myTrans = par.OracleConn.BeginTransaction()
            'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        Finally

        End Try
    End Function


End Class
