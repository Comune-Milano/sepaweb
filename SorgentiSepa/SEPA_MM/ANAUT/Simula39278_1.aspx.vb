Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_Simula39278_1
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0
    Dim dt As New System.Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()
            CaricaDatiAU(Request.QueryString("ID"))
            Carica392()
            H1.Value = "Estrazione_" & Format(Now, "yyyyMMddHHmmss")

        End If
    End Sub

    Private Function CaricaDatiAU(ByVal Indice As String)
        Try
            Dim AnnoAU As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & Indice
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lIdAU = myReader("id")
                AnnoAU = myReader("anno_au")
                Label3.Text = " Anno " & myReader("anno_au") & " Redditi " & myReader("anno_isee")
            End If
            myReader.Close()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label5.Text = ex.Message
            Label5.Visible = True
        End Try
    End Function

    Public Property lIdAU() As Long
        Get
            If Not (ViewState("par_lIdAU") Is Nothing) Then
                Return CLng(ViewState("par_lIdAU"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdAU") = value
        End Set
    End Property

    Public Property sCANONE12431() As String
        Get
            If Not (ViewState("par_sCANONE12431") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE12431"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE12431") = value
        End Set
    End Property

    Public Property sCANONE12432() As String
        Get
            If Not (ViewState("par_sCANONE12432") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE12432"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE12432") = value
        End Set
    End Property

    Public Property sDELTA12431() As String
        Get
            If Not (ViewState("par_sDELTA12431") Is Nothing) Then
                Return CStr(ViewState("par_sDELTA12431"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDELTA12431") = value
        End Set
    End Property

    Public Property sDELTA12432() As String
        Get
            If Not (ViewState("par_sDELTA12432") Is Nothing) Then
                Return CStr(ViewState("par_sDELTA12432"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDELTA12432") = value
        End Set
    End Property

    Public Property sESCLUSIONE1243() As String
        Get
            If Not (ViewState("par_sESCLUSIONE1243") Is Nothing) Then
                Return CStr(ViewState("par_sESCLUSIONE1243"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sESCLUSIONE1243") = value
        End Set
    End Property

    Public Property sCOMPETENZA2ANNO() As String
        Get
            If Not (ViewState("par_sCOMPETENZA2ANNO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPETENZA2ANNO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPETENZA2ANNO") = value
        End Set
    End Property

    Public Property sCOMPETENZA1ANNO() As String
        Get
            If Not (ViewState("par_sCOMPETENZA1ANNO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPETENZA1ANNO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPETENZA1ANNO") = value
        End Set
    End Property

    Public Property sTIPOCANONEAPPLICATO() As String
        Get
            If Not (ViewState("par_sTIPOCANONEAPPLICATO") Is Nothing) Then
                Return CStr(ViewState("par_sTIPOCANONEAPPLICATO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sTIPOCANONEAPPLICATO") = value
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

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
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

    Public Property sISTAT2ANNO() As String
        Get
            If Not (ViewState("par_sISTAT2ANNO") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT2ANNO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT2ANNO") = value
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

    Private Sub Carica392()
        Try
            'par.OracleConn.Open()
            'par.SettaCommand(par)



            sStringaSQL1 = "SELECT RAPPORTO,TO_CHAR (TO_DATE (DATA_DISDETTA_392, 'yyyymmdd'), 'dd/mm/yyyy') AS DATA_RIC_INV_DISDETTA,UTENZA_DICHIARAZIONI.PG, " _
            & "         DECODE (UTENZA_DICHIARAZIONI.ID_STATO, " _
            & "                 0, 'DA COMPLETARE', " _
            & "1, 'COMPLETA', " _
            & "2, 'DA CANCELLARE') " _
            & "AS STATO_AU, " _
            & "(SELECT DECODE (ID_STATO,  0, 'APERTA',  1, 'SOSPESA',  2, 'CHIUSA') " _
            & "AS  " _
            & "FROM SISCOM_MI.CONVOCAZIONI_AU " _
            & "WHERE     ID_GRUPPO IN (SELECT ID " _
            & "FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI " _
            & "WHERE ID_AU = " & lIdAU & ") " _
            & "AND ID_CONTRATTO = RAPPORTI_UTENZA.ID) " _
            & "AS STATO_CONVOCAZIONE, " _
            & " " _
            & "             " _
         & " " _
         & "UTENZA_COMP_NUCLEO.COGNOME, " _
         & "UTENZA_COMP_NUCLEO.NOME, " _
         & " " _
         & " " _
         & " " _
         & " " _
          & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO, " _
         & "INDIRIZZI.CIVICO, " _
          & "SCALE_EDIFICI.DESCRIZIONE AS SCALA, " _
         & " " _
         & "TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,UNITA_IMMOBILIARI.INTERNO, " _
         & "INDIRIZZI.CAP, " _
         & "INDIRIZZI.LOCALITA, " _
         & "(CASE " _
         & "WHEN NVL (UTENZA_DICH_CANONI_EC.DATA_CALCOLO, '') = '' " _
         & "THEN " _
         & "                '' " _
         & "ELSE " _
         & "TO_CHAR ( " _
         & "                      TO_DATE ( " _
         & "SUBSTR (UTENZA_DICH_CANONI_EC.DATA_CALCOLO, 1, 8), " _
         & "'yyyymmdd'), " _
         & "'dd/mm/yyyy') " _
         & "|| ' ' " _
         & "|| SUBSTR (UTENZA_DICH_CANONI_EC.DATA_CALCOLO, 9, 2) " _
         & "|| ':' " _
         & "|| SUBSTR (UTENZA_DICH_CANONI_EC.DATA_CALCOLO, 11, 2) " _
         & "END) " _
         & "AS DATA_CALCOLO, " _
         & "RAPPORTI_UTENZA.IMP_CANONE_INIZIALE AS CANONE_392, " _
         & "UTENZA_DICH_CANONI_EC.ise,UTENZA_DICH_CANONI_EC.isr,UTENZA_DICH_CANONI_EC.isp,UTENZA_DICH_CANONI_EC.PSE,UTENZA_DICH_CANONI_EC.VSE,UTENZA_DICH_CANONI_EC.CANONE_CLASSE,UTENZA_DICH_CANONI_EC.CANONE_SOPPORTABILE,UTENZA_DICH_CANONI_EC.PERC_ISTAT_APPLICATA,UTENZA_DICH_CANONI_EC.CANONE_CLASSE_ISTAT, " _
         & "UTENZA_DICH_CANONI_EC.ISEE AS ISEE_ERP, " _
         & "DECODE (NVL (UTENZA_DICH_CANONI_EC.ID_AREA_ECONOMICA, 0), " _
         & "1, 'PROTEZIONE', " _
         & "2, 'ACCESSO', " _
         & "3, 'PERMANENZA', " _
         & "4, 'DECADENZA') " _
         & "AS AREA_ECONOMICA, " _
         & "UTENZA_DICH_CANONI_EC.SOTTO_AREA AS CLASSE, " _
         & "UTENZA_DICH_CANONI_EC.CANONE AS CANONE_ERP " _
         & "FROM UTENZA_COMP_NUCLEO, " _
         & "UTENZA_DICHIARAZIONI, " _
         & "SISCOM_MI.INDIRIZZI, " _
         & "SISCOM_MI.SCALE_EDIFICI, " _
         & "SISCOM_MI.TIPO_LIVELLO_PIANO, " _
         & "SISCOM_MI.RAPPORTI_UTENZA, " _
         & "UTENZA_DICH_CANONI_EC, " _
         & "SISCOM_MI.UNITA_CONTRATTUALE, " _
         & "SISCOM_MI.UNITA_IMMOBILIARI, " _
         & "SISCOM_MI.EDIFICI, " _
         & "SISCOM_MI.COMPLESSI_IMMOBILIARI " _
         & "WHERE     UTENZA_COMP_NUCLEO.PROGR = 0 " _
         & "AND UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE = UTENZA_DICHIARAZIONI.ID " _
         & "AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA " _
         & "AND TIPO_LIVELLO_PIANO.COD(+) = " _
         & "UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO " _
         & "AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
         & "AND EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO " _
         & "AND COMPLESSI_IMMOBILIARI.ID = EDIFICI.ID_COMPLESSO " _
         & "AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
         & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
         & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
         & "AND RAPPORTI_UTENZA.COD_CONTRATTO = UTENZA_DICHIARAZIONI.RAPPORTO " _
         & "AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = 'EQC392' " _
         & "AND UTENZA_DICHIARAZIONI.ID_BANDO = " & lIdAU _
         & "AND UTENZA_DICH_CANONI_EC.ID_CONTRATTO(+) = RAPPORTI_UTENZA.ID AND UTENZA_DICH_CANONI_EC.DATA_CALCOLO=(SELECT MAX (DATA_CALCOLO) FROM UTENZA_DICH_CANONI_EC EC WHERE EC.ID_CONTRATTO=UTENZA_DICH_CANONI_EC.ID_CONTRATTO AND EC.ID_BANDO_AU=" & lIdAU & ") " _
         & "ORDER BY cognome,nome, DATA_CALCOLO DESC "

            BindGrid()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Label5.Visible = True
            Label5.Text = ex.Message
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    'Private Function Carica()
    '    Try
    '        Dim comunicazioni As String = ""
    '        Dim LimiteIsee As Integer = 0
    '        Dim DAFARE As Boolean
    '        Dim CANONE91 As String = ""
    '        Dim dt As New System.Data.DataTable
    '        Dim ROW As System.Data.DataRow
    '        Dim I As Integer = 0
    '        Dim NUMERORIGHE As Long = 0
    '        Dim Contatore As Long = 0
    '        Dim Anomalia As Boolean = False
    '        Dim AnnoAU As String = ""
    '        Dim AnnoRedditi As String = ""

    '        Dim ISTAT_1_PR As Double = 0
    '        Dim ISTAT_2_PR As Double = 0
    '        Dim ISTAT_1_AC As Double = 0
    '        Dim ISTAT_2_AC As Double = 0
    '        Dim ISTAT_1_PE As Double = 0
    '        Dim ISTAT_2_PE As Double = 0
    '        Dim ISTAT_1_DE As Double = 0
    '        Dim ISTAT_2_DE As Double = 0

    '        Dim ICI_1_2 As Double = 0
    '        Dim ICI_3_4 As Double = 0
    '        Dim ICI_5_6 As Double = 0
    '        Dim ICI_7 As Double = 0

    '        Dim LimiteA4 As Double = 0
    '        Dim LimiteA5 As Double = 0
    '        Dim InizioB1 As Double = 0
    '        Dim InizioC12 As Double = 0

    '        Dim CanoneMinimoA5 As Double = 0
    '        Dim Perc_Inc_ISE_A5 As Double = 0
    '        Dim Perc_Inc_Loc_A5 As Double = 0

    '        Dim CanoneMinimoB1 As Double = 0
    '        Dim Perc_Inc_ISE_B1 As Double = 0
    '        Dim Perc_Inc_Loc_B1 As Double = 0

    '        Dim CanoneMinimoC12 As Double = 0
    '        Dim Perc_Inc_ISE_C12 As Double = 0
    '        Dim Perc_Inc_Loc_C12 As Double = 0

    '        Dim CanoneMinimoD4 As Double = 0
    '        Dim Perc_Inc_ISE_D4 As Double = 0
    '        Dim Perc_Inc_Loc_D4 As Double = 0

    '        Dim LimitePensioneAU As Double = 0

    '        Dim InizioCanone As String = ""
    '        Dim FineCanone As String = ""

    '        Dim S As String = ""

    '        par.OracleConn.Open()
    '        par.SettaCommand(par)

    '        Dim S1 As String = ""
    '        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
    '        Dim S2 As String = ""
    '        Dim ss As String = "("


    '        dt.Columns.Add("SPORTELLO")
    '        dt.Columns.Add("COD_CONTRATTO")
    '        dt.Columns.Add("TIPOLOGIA_CONTRATTO")
    '        dt.Columns.Add("PG_DICHIARAZIONE_AU")
    '        dt.Columns.Add("DIFFIDA")
    '        dt.Columns.Add("DATA_GENERAZIONE_DIFFIDA")
    '        dt.Columns.Add("DATA_STIPULA")
    '        dt.Columns.Add("NUM_COMP")
    '        dt.Columns.Add("MINORI_15")
    '        dt.Columns.Add("MAGGIORI_65")
    '        dt.Columns.Add("NUM_COMP_66")
    '        dt.Columns.Add("NUM_COMP_100")
    '        dt.Columns.Add("NUM_COMP_100_CON")
    '        dt.Columns.Add("PSE")
    '        dt.Columns.Add("VSE")
    '        dt.Columns.Add("REDDITI_DIPENDENTI")
    '        dt.Columns.Add("REDDITI_ALTRI")
    '        dt.Columns.Add("COEFF_NUCLEO_FAM")
    '        dt.Columns.Add("LIMITE_PENSIONI")
    '        dt.Columns.Add("REDD_PREV_DIP")
    '        dt.Columns.Add("REDD_COMPLESSIVO")
    '        dt.Columns.Add("REDD_IMMOBILIARI")
    '        dt.Columns.Add("REDD_MOBILIARI")
    '        dt.Columns.Add("ISE")
    '        dt.Columns.Add("DETRAZIONI")
    '        dt.Columns.Add("DETRAZIONI_FRAGILITA")
    '        dt.Columns.Add("ISEE")
    '        dt.Columns.Add("ISR")
    '        dt.Columns.Add("ISP")
    '        dt.Columns.Add("ISEE_27")
    '        dt.Columns.Add("ID_AREA_ECONOMICA")
    '        dt.Columns.Add("SOTTO_AREA")
    '        dt.Columns.Add("LIMITE_ISEE")
    '        dt.Columns.Add("DECADENZA_ALL_ADEGUATO")
    '        dt.Columns.Add("DECADENZA_VAL_ICI")
    '        dt.Columns.Add("PATRIMONIO_SUP")
    '        dt.Columns.Add("ANNO_COSTRUZIONE")
    '        dt.Columns.Add("LOCALITA")
    '        dt.Columns.Add("NUMERO_PIANO")
    '        dt.Columns.Add("PRESENTE_ASCENSORE")
    '        dt.Columns.Add("PIANO")
    '        dt.Columns.Add("DEM")
    '        dt.Columns.Add("ZONA")
    '        dt.Columns.Add("COSTOBASE")
    '        dt.Columns.Add("VETUSTA")
    '        dt.Columns.Add("CONSERVAZIONE")
    '        dt.Columns.Add("SUP_NETTA")
    '        dt.Columns.Add("SUPCONVENZIONALE")
    '        dt.Columns.Add("ALTRE_SUP")
    '        dt.Columns.Add("SUP_ACCESSORI")
    '        dt.Columns.Add("VALORE_LOCATIVO")
    '        dt.Columns.Add("PERC_VAL_LOC")
    '        dt.Columns.Add("CANONE_CLASSE")
    '        dt.Columns.Add("PERC_ISTAT_APPLICATA")
    '        dt.Columns.Add("CANONE_CLASSE_ISTAT")
    '        dt.Columns.Add("INC_MAX")
    '        dt.Columns.Add("CANONE_SOPPORTABILE")
    '        dt.Columns.Add("CANONE_MINIMO_AREA")
    '        dt.Columns.Add("CANONE")
    '        dt.Columns.Add("CANONE_ATTUALE")
    '        dt.Columns.Add("ADEGUAMENTO")
    '        dt.Columns.Add("ISTAT")
    '        dt.Columns.Add("NOTE")
    '        dt.Columns.Add("ANNOTAZIONI")
    '        dt.Columns.Add("CANONE_91")
    '        dt.Columns.Add("ID_DICHIARAZIONE")
    '        dt.Columns.Add("ID_GRUPPO")
    '        dt.Columns.Add("TIPO_CANONE_APPLICATO")
    '        dt.Columns.Add("COMPETENZA_1_ANNO")
    '        dt.Columns.Add("COMPETENZA_2_ANNO")

    '        dt.Columns.Add("SCONTO_COSTO_BASE")
    '        dt.Columns.Add("CANONE_1243_1_ANNO")
    '        dt.Columns.Add("CANONE_1243_2_ANNO")
    '        dt.Columns.Add("DELTA_1243_1_ANNO")
    '        dt.Columns.Add("DELTA_1243_2_ANNO")
    '        dt.Columns.Add("ESCLUSIONE_1243")
    '        dt.Columns.Add("STATO_AU")
    '        '
    '        'COMPETENZA_1_ANNO


    '        Dim IDAU As Long = 0
    '        par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE ID=" & lIdAU
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            IDAU = myReader("ID")
    '            AnnoAU = myReader("anno_au")
    '            AnnoRedditi = myReader("anno_isee")
    '            InizioCanone = myReader("inizio_canone")
    '            FineCanone = myReader("fine_canone")
    '        End If
    '        myReader.Close()

    '        par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI_PARAMETRI WHERE ID_AU=" & lIdAU
    '        myReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            ISTAT_1_PR = myReader("ISTAT_1_PR")
    '            ISTAT_2_PR = myReader("ISTAT_2_PR")

    '            ISTAT_1_AC = myReader("ISTAT_1_AC")
    '            ISTAT_2_AC = myReader("ISTAT_2_AC")

    '            ISTAT_1_PE = myReader("ISTAT_1_PE")
    '            ISTAT_2_PE = myReader("ISTAT_2_PE")

    '            ISTAT_1_DE = myReader("ISTAT_1_DE")
    '            ISTAT_2_DE = myReader("ISTAT_2_DE")

    '            ICI_1_2 = myReader("ICI_1_2")
    '            ICI_3_4 = myReader("ICI_3_4")
    '            ICI_5_6 = myReader("ICI_5_6")
    '            ICI_7 = myReader("ICI_7")

    '            LimitePensioneAU = par.IfNull(myReader("limite_pensione"), 0)

    '        End If
    '        myReader.Close()

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='A4'"
    '        myReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            LimiteA4 = par.IfNull(myReader("ISEE_ERP"), 0)
    '        End If
    '        myReader.Close()

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='A5'"
    '        myReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            LimiteA5 = par.IfNull(myReader("ISEE_ERP"), 0)
    '            InizioB1 = par.IfNull(myReader("ISEE_ERP"), 0) + 1
    '            CanoneMinimoA5 = par.IfNull(myReader("canone_minimo"), 0)
    '            Perc_Inc_ISE_A5 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
    '            Perc_Inc_Loc_A5 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
    '        End If
    '        myReader.Close()

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='C11'"
    '        myReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            InizioC12 = par.IfNull(myReader("ISEE_ERP"), 0) + 1
    '        End If
    '        myReader.Close()

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='B1'"
    '        myReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            CanoneMinimoB1 = par.IfNull(myReader("canone_minimo"), 0)
    '            Perc_Inc_ISE_B1 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
    '            Perc_Inc_Loc_B1 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
    '        End If
    '        myReader.Close()

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='C12'"
    '        myReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            CanoneMinimoC12 = par.IfNull(myReader("canone_minimo"), 0)
    '            Perc_Inc_ISE_C12 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
    '            Perc_Inc_Loc_C12 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
    '        End If
    '        myReader.Close()

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='D4'"
    '        myReader = par.cmd.ExecuteReader()
    '        If myReader.Read() Then
    '            CanoneMinimoD4 = par.IfNull(myReader("canone_minimo"), 0)
    '            Perc_Inc_ISE_D4 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
    '            Perc_Inc_Loc_D4 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
    '        End If
    '        myReader.Close()


    '        par.cmd.CommandText = "SELECT ROWNUM,UTENZA_GRUPPI_DICHIARAZIONI.ID_GRUPPO,(CASE WHEN UTENZA_DICHIARAZIONI.ID_STATO=0 THEN 'DA COMPLETARE' WHEN UTENZA_DICHIARAZIONI.ID_STATO=1 THEN 'COMPLETA' WHEN UTENZA_DICHIARAZIONI.ID_STATO=2 THEN 'DA CANCELLARE' END) AS STATO_AU,NVL(EDIFICI.SCONTO_COSTO_BASE,-1000) AS SCONTO_COSTO_BASE,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO,convocazioni_au.*,RAPPORTI_UTENZA.COD_CONTRATTO,UTENZA_DICHIARAZIONI.PATR_SUPERATO,UTENZA_DICHIARAZIONI.PG,UTENZA_DICHIARAZIONI.ISEE,UTENZA_DICHIARAZIONI.ID_STATO,UTENZA_DICHIARAZIONI.id as idAU,UTENZA_DICHIARAZIONI.FL_SOSP_1,UTENZA_DICHIARAZIONI.FL_SOSP_2,UTENZA_DICHIARAZIONI.FL_SOSP_3,UTENZA_DICHIARAZIONI.FL_SOSP_4,UTENZA_DICHIARAZIONI.FL_SOSP_5,UTENZA_DICHIARAZIONI.FL_SOSP_7,unita_immobiliari.ID AS idunita,(SELECT (CASE WHEN TIPO=0 THEN 'INCOMPLETA' ELSE 'NON RISPONDENTE' END) FROM SISCOM_MI.DIFFIDE_LETTERE WHERE CONVOCAZIONI_AU.id_stato=2 and ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DIFFIDA,(SELECT TO_CHAR(TO_DATE(DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DATA_GENERAZIONE_DIFFIDA FROM UTENZA_GRUPPI_DICHIARAZIONI,siscom_mi.convocazioni_au,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,UTENZA_DICHIARAZIONI,UTENZA_SPORTELLI,SISCOM_MI.EDIFICI WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_GRUPPI_DICHIARAZIONI.ID_DICHIARAZIONE AND UTENZA_GRUPPI_DICHIARAZIONI.APPLICA_AU=0 AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND rapporti_utenza.cod_contratto=UTENZA_DICHIARAZIONI.rapporto  AND rapporti_utenza.ID=convocazioni_au.id_contratto AND unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto=convocazioni_au.id_contratto AND unita_immobiliari.ID=unita_contrattuale.id_unita AND convocazioni_au.id_contratto IS NOT NULL AND data_app IS NOT NULL AND UTENZA_DICHIARAZIONI.ID IN (SELECT id_dichiarazione FROM UTENZA_GRUPPI_DICHIARAZIONI WHERE id_gruppo IN " & ElencoGruppi & ") AND convocazioni_au.id_stato=2 ORDER BY ROWNUM DESC"
    '        myReader = par.cmd.ExecuteReader()
    '        While myReader.Read
    '            If NUMERORIGHE = 0 Then
    '                NUMERORIGHE = par.IfNull(myReader("ROWNUM"), 0)
    '            End If

    '            DAFARE = True
    '            If par.IfNull(myReader("ID_STATO"), "0") = "0" Then
    '                If par.IfNull(myReader("fl_sosp_7"), "0") = "1" And par.IfNull(myReader("fl_sosp_1"), "0") = "0" And par.IfNull(myReader("fl_sosp_2"), "0") = "0" And par.IfNull(myReader("fl_sosp_3"), "0") = "0" And par.IfNull(myReader("fl_sosp_4"), "0") = "0" And par.IfNull(myReader("fl_sosp_5"), "0") = "0" Then
    '                    DAFARE = True
    '                Else
    '                    If par.IfNull(myReader("fl_sosp_7"), "0") = "0" And par.IfNull(myReader("fl_sosp_1"), "0") = "0" And par.IfNull(myReader("fl_sosp_2"), "0") = "0" And par.IfNull(myReader("fl_sosp_3"), "0") = "0" And par.IfNull(myReader("fl_sosp_4"), "0") = "0" And par.IfNull(myReader("fl_sosp_5"), "0") = "0" Then
    '                        DAFARE = True
    '                    Else
    '                        DAFARE = False
    '                    End If

    '                End If
    '            End If

    '            If DAFARE = True Then
    '                I = I + 1
    '                S = par.CalcolaCanone27_ANAGRAFE_UTENZA(par.IfNull(myReader("ID_CONTRATTO"), "0"), par.IfNull(myReader("PATR_SUPERATO"), "0"), myReader("SCONTO_COSTO_BASE"), ISTAT_1_PR, ISTAT_2_PR, ISTAT_1_AC, ISTAT_2_AC, ISTAT_1_PE, ISTAT_2_PE, ISTAT_1_DE, ISTAT_2_DE, LimiteA4, LimiteA5, InizioB1, InizioC12, CanoneMinimoA5, Perc_Inc_ISE_A5, Perc_Inc_Loc_A5, CanoneMinimoB1, Perc_Inc_ISE_B1, Perc_Inc_Loc_B1, CanoneMinimoC12, Perc_Inc_ISE_C12, Perc_Inc_Loc_C12, CanoneMinimoD4, Perc_Inc_ISE_D4, Perc_Inc_Loc_D4, LimitePensioneAU, ICI_7, ICI_5_6, ICI_3_4, ICI_1_2, AnnoAU, AnnoRedditi, InizioCanone, FineCanone, IDAU, myReader("idAU"), CDbl(myReader("idunita")), myReader("cod_contratto"), CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sISTAT2ANNO, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, sTIPOCANONEAPPLICATO, sCOMPETENZA1ANNO, sCOMPETENZA2ANNO, sESCLUSIONE1243, sDELTA12432, sDELTA12431, sCANONE12432, sCANONE12431)

    '                par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ISTAT"",(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ADEGUAMENTO"" FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & myReader("COD_CONTRATTO") & "'"
    '                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReader1.Read Then
    '                    CANONE91 = ""
    '                    par.cmd.CommandText = "SELECT IMP_ANN_CANONE_91_ATTUALIZZ FROM SISCOM_MI.RAPPORTI_UTENZA_EXTRA WHERE ID_CONTRATTO=" & myReader1("ID")
    '                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                    If myReader2.Read Then
    '                        CANONE91 = par.IfNull(myReader2(0), "")
    '                    End If
    '                    myReader2.Close()

    '                    If CDbl(par.IfNull(myReader("ISEE"), "0")) > 35000 Then
    '                        LimiteIsee = 1
    '                    Else
    '                        LimiteIsee = 0
    '                    End If

    '                    ROW = dt.NewRow()

    '                    If AreaEconomica <> -1 Then
    '                        ROW.Item("SPORTELLO") = myReader("SPORTELLO")
    '                        ROW.Item("COD_CONTRATTO") = myReader1("COD_CONTRATTO")
    '                        ROW.Item("TIPOLOGIA_CONTRATTO") = myReader("TIPO_CONTRATTO")
    '                        ROW.Item("PG_DICHIARAZIONE_AU") = myReader("PG")
    '                        ROW.Item("DIFFIDA") = myReader("DIFFIDA")
    '                        ROW.Item("DATA_GENERAZIONE_DIFFIDA") = myReader("DATA_GENERAZIONE_DIFFIDA")
    '                        ROW.Item("DATA_STIPULA") = par.FormattaData(myReader1("DATA_STIPULA"))
    '                        ROW.Item("NUM_COMP") = sNUMCOMP
    '                        ROW.Item("MINORI_15") = sMINORI15
    '                        ROW.Item("MAGGIORI_65") = sMAGGIORI65
    '                        ROW.Item("NUM_COMP_66") = sNUMCOMP66
    '                        ROW.Item("NUM_COMP_100") = sNUMCOMP100
    '                        ROW.Item("NUM_COMP_100_CON") = sNUMCOMP100C
    '                        ROW.Item("PSE") = sPSE
    '                        ROW.Item("VSE") = sVSE
    '                        ROW.Item("REDDITI_DIPENDENTI") = Format(CDbl(par.IfEmpty(sREDD_DIP, 0)), "##,##0.00")
    '                        ROW.Item("REDDITI_ALTRI") = Format(CDbl(par.IfEmpty(sREDD_ALT, 0)), "##,##0.00")
    '                        ROW.Item("COEFF_NUCLEO_FAM") = sCOEFFFAM
    '                        ROW.Item("LIMITE_PENSIONI") = LimitePensioneAU
    '                        If sPREVDIP = "1" Then
    '                            ROW.Item("REDD_PREV_DIP") = "SI"
    '                        Else
    '                            ROW.Item("REDD_PREV_DIP") = "NO"
    '                        End If
    '                        ROW.Item("REDD_COMPLESSIVO") = Format(CDbl(par.IfEmpty(sCOMPLESSIVO, 0)), "##,##0.00")
    '                        ROW.Item("REDD_IMMOBILIARI") = Format(CDbl(par.IfEmpty(sIMMOBILIARI, 0)), "##,##0.00")
    '                        ROW.Item("REDD_MOBILIARI") = Format(CDbl(par.IfEmpty(sMOBILIARI, 0)), "##,##0.00")
    '                        ROW.Item("ISE") = par.Tronca(sISE)
    '                        ROW.Item("DETRAZIONI") = Format(CDbl(sDETRAZIONI), "##,##0.00")
    '                        ROW.Item("DETRAZIONI_FRAGILITA") = Format(CDbl(sDETRAZIONEF), "##,##0.00")
    '                        ROW.Item("ISEE") = par.Tronca(sISEE)
    '                        ROW.Item("ISR") = par.Tronca(sISR)
    '                        ROW.Item("ISP") = sISP
    '                        ROW.Item("ISEE_27") = par.Tronca(sISE_MIN)
    '                        Select Case AreaEconomica
    '                            Case 1
    '                                ROW.Item("ID_AREA_ECONOMICA") = "PROTEZIONE"
    '                            Case 2
    '                                ROW.Item("ID_AREA_ECONOMICA") = "ACCESSO"
    '                            Case 3
    '                                ROW.Item("ID_AREA_ECONOMICA") = "PERMANENZA"
    '                            Case 4
    '                                ROW.Item("ID_AREA_ECONOMICA") = "DECADENZA"
    '                        End Select

    '                        ROW.Item("SOTTO_AREA") = sSOTTOAREA
    '                        If LimiteIsee = 1 Then
    '                            ROW.Item("LIMITE_ISEE") = "SI"
    '                        Else
    '                            ROW.Item("LIMITE_ISEE") = "NO"
    '                        End If
    '                        If sALLOGGIOIDONEO = "1" Then
    '                            ROW.Item("DECADENZA_ALL_ADEGUATO") = "SI"
    '                        Else
    '                            ROW.Item("DECADENZA_ALL_ADEGUATO") = "NO"
    '                        End If
    '                        If sVALOCIICI = "1" Then
    '                            ROW.Item("DECADENZA_VAL_ICI") = "SI"
    '                        Else
    '                            ROW.Item("DECADENZA_VAL_ICI") = "NO"
    '                        End If
    '                        If par.IfNull(myReader("PATR_SUPERATO"), "0") = "1" Then
    '                            ROW.Item("PATRIMONIO_SUP") = "SI"
    '                        Else
    '                            ROW.Item("PATRIMONIO_SUP") = "NO"
    '                        End If

    '                        ROW.Item("ANNO_COSTRUZIONE") = sANNOCOSTRUZIONE
    '                        ROW.Item("LOCALITA") = sLOCALITA
    '                        ROW.Item("NUMERO_PIANO") = sDESCRIZIONEPIANO
    '                        If sASCENSORE = "1" Then
    '                            ROW.Item("PRESENTE_ASCENSORE") = "SI"
    '                        Else
    '                            ROW.Item("PRESENTE_ASCENSORE") = "NO"
    '                        End If
    '                        ROW.Item("PIANO") = sPIANO
    '                        ROW.Item("DEM") = sDEM
    '                        ROW.Item("ZONA") = sZONA
    '                        ROW.Item("COSTOBASE") = sCOSTOBASE
    '                        ROW.Item("VETUSTA") = sVETUSTA
    '                        ROW.Item("CONSERVAZIONE") = sCONSERVAZIONE
    '                        ROW.Item("SUP_NETTA") = sSUPNETTA
    '                        ROW.Item("SUPCONVENZIONALE") = sSUPCONVENZIONALE
    '                        ROW.Item("ALTRE_SUP") = sALTRESUP
    '                        ROW.Item("SUP_ACCESSORI") = sSUPACCESSORI
    '                        ROW.Item("VALORE_LOCATIVO") = par.Tronca(sVALORELOCATIVO)
    '                        ROW.Item("PERC_VAL_LOC") = sPER_VAL_LOC
    '                        ROW.Item("CANONE_CLASSE") = Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00")
    '                        ROW.Item("PERC_ISTAT_APPLICATA") = sISTAT
    '                        ROW.Item("CANONE_CLASSE_ISTAT") = Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00")
    '                        ROW.Item("INC_MAX") = sPERC_INC_MAX_ISE_ERP 'sINCIDENZAISE
    '                        ROW.Item("CANONE_SOPPORTABILE") = Format(CDbl(par.IfEmpty(sCANONESOPP, 0)), "##,##0.00")
    '                        ROW.Item("CANONE_MINIMO_AREA") = Format(CDbl(par.IfEmpty(sCANONE_MIN, 0)), "##,##0.00")
    '                        ROW.Item("CANONE") = Format(CDbl(par.IfEmpty(sCanone, 0)), "##,##0.00")
    '                        ROW.Item("CANONE_ATTUALE") = Format(par.IfNull(myReader1("IMP_CANONE_INIZIALE"), 0), "##,##0.00")
    '                        ROW.Item("ADEGUAMENTO") = Format(par.IfNull(myReader1("ADEGUAMENTO"), 0), "##,##0.00")
    '                        ROW.Item("ISTAT") = par.IfNull(myReader1("ISTAT"), "0,00")
    '                        ROW.Item("NOTE") = sNOTE
    '                        ROW.Item("ANNOTAZIONI") = sMOTIVODECADENZA
    '                        If CANONE91 <> "" Then
    '                            ROW.Item("CANONE_91") = Format(CDbl(CANONE91), "##,##0.00")
    '                        Else
    '                            ROW.Item("CANONE_91") = ""
    '                        End If
    '                        ROW.Item("ID_DICHIARAZIONE") = myReader("IDAU")
    '                        ROW.Item("ID_GRUPPO") = myReader("ID_GRUPPO")
    '                        ROW.Item("TIPO_CANONE_APPLICATO") = sTIPOCANONEAPPLICATO
    '                        ROW.Item("COMPETENZA_1_ANNO") = sCOMPETENZA1ANNO
    '                        ROW.Item("COMPETENZA_2_ANNO") = sCOMPETENZA2ANNO

    '                        ROW.Item("SCONTO_COSTO_BASE") = Replace(myReader("SCONTO_COSTO_BASE"), "1000", "")
    '                        ROW.Item("CANONE_1243_1_ANNO") = sCANONE12431
    '                        ROW.Item("CANONE_1243_2_ANNO") = sCANONE12432
    '                        ROW.Item("DELTA_1243_1_ANNO") = sDELTA12431
    '                        ROW.Item("DELTA_1243_2_ANNO") = sDELTA12432
    '                        ROW.Item("ESCLUSIONE_1243") = sESCLUSIONE1243
    '                        ROW.Item("STATO_AU") = myReader("STATO_AU")
    '                    Else
    '                        ROW.Item("SPORTELLO") = myReader("SPORTELLO")
    '                        ROW.Item("COD_CONTRATTO") = myReader1("COD_CONTRATTO")
    '                        ROW.Item("TIPOLOGIA_CONTRATTO") = myReader("TIPO_CONTRATTO")
    '                        ROW.Item("PG_DICHIARAZIONE_AU") = ""
    '                        ROW.Item("DIFFIDA") = ""
    '                        ROW.Item("DATA_GENERAZIONE_DIFFIDA") = ""
    '                        ROW.Item("DATA_STIPULA") = ""
    '                        ROW.Item("NUM_COMP") = ""
    '                        ROW.Item("MINORI_15") = ""
    '                        ROW.Item("MAGGIORI_65") = ""
    '                        ROW.Item("NUM_COMP_66") = ""
    '                        ROW.Item("NUM_COMP_100") = ""
    '                        ROW.Item("NUM_COMP_100_CON") = ""
    '                        ROW.Item("PSE") = ""
    '                        ROW.Item("VSE") = ""
    '                        ROW.Item("REDDITI_DIPENDENTI") = ""
    '                        ROW.Item("REDDITI_ALTRI") = ""
    '                        ROW.Item("COEFF_NUCLEO_FAM") = ""
    '                        ROW.Item("LIMITE_PENSIONI") = ""
    '                        ROW.Item("REDD_PREV_DIP") = ""
    '                        ROW.Item("REDD_COMPLESSIVO") = ""
    '                        ROW.Item("REDD_IMMOBILIARI") = ""
    '                        ROW.Item("REDD_MOBILIARI") = ""
    '                        ROW.Item("ISE") = ""
    '                        ROW.Item("DETRAZIONI") = ""
    '                        ROW.Item("DETRAZIONI_FRAGILITA") = ""
    '                        ROW.Item("ISEE") = ""
    '                        ROW.Item("ISR") = ""
    '                        ROW.Item("ISP") = ""
    '                        ROW.Item("ISEE_27") = ""
    '                        ROW.Item("ID_AREA_ECONOMICA") = ""
    '                        ROW.Item("SOTTO_AREA") = ""
    '                        ROW.Item("LIMITE_ISEE") = ""
    '                        ROW.Item("DECADENZA_ALL_ADEGUATO") = ""
    '                        ROW.Item("DECADENZA_VAL_ICI") = ""
    '                        ROW.Item("PATRIMONIO_SUP") = ""
    '                        ROW.Item("ANNO_COSTRUZIONE") = ""
    '                        ROW.Item("LOCALITA") = ""
    '                        ROW.Item("NUMERO_PIANO") = ""
    '                        ROW.Item("PRESENTE_ASCENSORE") = ""
    '                        ROW.Item("PIANO") = ""
    '                        ROW.Item("DEM") = ""
    '                        ROW.Item("ZONA") = ""
    '                        ROW.Item("COSTOBASE") = ""
    '                        ROW.Item("VETUSTA") = ""
    '                        ROW.Item("CONSERVAZIONE") = ""
    '                        ROW.Item("SUP_NETTA") = ""
    '                        ROW.Item("SUPCONVENZIONALE") = ""
    '                        ROW.Item("ALTRE_SUP") = ""
    '                        ROW.Item("SUP_ACCESSORI") = ""
    '                        ROW.Item("VALORE_LOCATIVO") = ""
    '                        ROW.Item("PERC_VAL_LOC") = ""
    '                        ROW.Item("CANONE_CLASSE") = ""
    '                        ROW.Item("PERC_ISTAT_APPLICATA") = ""
    '                        ROW.Item("CANONE_CLASSE_ISTAT") = ""
    '                        ROW.Item("INC_MAX") = ""
    '                        ROW.Item("CANONE_SOPPORTABILE") = ""
    '                        ROW.Item("CANONE_MINIMO_AREA") = ""
    '                        ROW.Item("CANONE") = ""
    '                        ROW.Item("CANONE_ATTUALE") = ""
    '                        ROW.Item("ADEGUAMENTO") = ""
    '                        ROW.Item("ISTAT") = ""
    '                        ROW.Item("NOTE") = sNOTE
    '                        ROW.Item("ANNOTAZIONI") = sMOTIVODECADENZA
    '                        ROW.Item("CANONE_91") = ""
    '                        ROW.Item("ID_DICHIARAZIONE") = myReader("IDAU")
    '                        ROW.Item("ID_GRUPPO") = ""
    '                        ROW.Item("TIPO_CANONE_APPLICATO") = ""
    '                        ROW.Item("COMPETENZA_1_ANNO") = ""
    '                        ROW.Item("COMPETENZA_2_ANNO") = ""

    '                        ROW.Item("SCONTO_COSTO_BASE") = Replace(myReader("SCONTO_COSTO_BASE"), "1000", "")
    '                        ROW.Item("CANONE_1243_1_ANNO") = ""
    '                        ROW.Item("CANONE_1243_2_ANNO") = ""
    '                        ROW.Item("DELTA_1243_1_ANNO") = ""
    '                        ROW.Item("DELTA_1243_2_ANNO") = ""
    '                        ROW.Item("ESCLUSIONE_1243") = ""
    '                        ROW.Item("STATO_AU") = par.IfNull(myReader("STATO_AU"), "")
    '                        Anomalia = True
    '                    End If
    '                    dt.Rows.Add(ROW)
    '                End If
    '                myReader1.Close()
    '            End If

    '            Contatore = Contatore + 1

    '            percentuale = (Contatore * 100) / NUMERORIGHE
    '            Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
    '            Response.Flush()
    '        End While
    '        myReader.Close()

    '        If I > 0 Then

    '            HttpContext.Current.Session.Add("ElencoSimulazione", dt)
    '            Label2.Text = " - " & dt.Rows.Count & " nella lista"

    '            If Anomalia = True Then
    '                Response.Write("<script>alert('Attenzione...sono state rilevate delle anomalie. Non è stato possibile calcolare il canone per alcune unità per mancanza di dati!\nVerificare nella lista.');</script>")
    '            End If
    '            BindGrid()

    '        Else
    '            Response.Write("<script>alert('Nessuna riga selezionata');</script>")
    '        End If

    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    '    Catch ex As Exception
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    End Try
    'End Function

    Private Sub BindGrid()
        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")
        da.Fill(dt)
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        HttpContext.Current.Session.Add("ELENCOAU", dt)

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub DataGrid1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub imgExport0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport0.Click
        Export()
    End Sub

    Private Sub Export()
        Try
            dt = CType(HttpContext.Current.Session.Item("ELENCOAU"), Data.DataTable)

            If dt.Rows.Count > 0 Then
                
                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(dt, DataGrid1, "Export392", , False, , False)

                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Redirect("../FileTemp/" & nomefile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
        Export()
    End Sub
End Class
