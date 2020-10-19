Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ApplicazioneGruppoAU0
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0
    Dim Tipo1 As Integer = 0
    Dim Tipo2 As Integer = 0
    Dim Tipo3 As Integer = 0
    Dim Tipo4 As Integer = 0
    Dim Tipo5 As Integer = 0
    Dim Tipo6 As Integer = 0
    Dim Tipo7 As Integer = 0
    Dim Tipo8 As Integer = 0
    Dim Tipo9 As Integer = 0
    Dim Tipo10 As Integer = 0
    Dim Tipo11 As Integer = 0

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
        Response.Flush()

        If Not IsPostBack Then
            Response.Flush()

            CaricaDatiAU()
            Carica()
            H1.Value = "Estrazione_" & Format(Now, "yyyyMMddHHmmss")

        End If
    End Sub

    Private Function CaricaDatiAU()
        Try
            Dim AnnoAU As String = ""

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If


            If Request.QueryString("T") = "1" Then
                Dim s As String = Session.Item("ElencoSimulazione")
                par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=(select distinct(id_bando_au) from UTENZA_GRUPPI_LAVORO where id in " & s & ")"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lIdAU = myReader("id")
                    AnnoAU = myReader("anno_au")
                    Label3.Text = " Anno " & myReader("anno_au") & " Redditi " & myReader("anno_isee")
                End If
                myReader.Close()
            Else
                par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE STATO=1 ORDER BY ID DESC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lIdAU = myReader("id")
                    AnnoAU = myReader("anno_au")
                    Label3.Text = " Anno " & myReader("anno_au") & " Redditi " & myReader("anno_isee")
                End If
                myReader.Close()
            End If

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

    Function Export()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try

            Dim dt As New System.Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("ElencoRisultati"), Data.DataTable)
            FileCSV = H1.Value '"Estrazione" & Format(Now, "yyyyMMddHHmmss")

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    i = 0
                    With myExcelFile

                        .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
                        .PrintGridLines = False
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                        .SetDefaultRowHeight(14)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                        .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)




                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "SPORTELLO/FILIALE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD.CONTRATTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "TIPOLOGIA CONTRATTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "PG DICHIARAZIONE AU")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "STATO AU")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DIFFIDA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DATA GENERAZIONE DIFFIDA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "DATA STIPULA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "NUM.COMPONENTI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "NUM. MINORI 15")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "NUM. MAGGIORI65")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "NUM.COMP.66-99%")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "NUM.COMP.100% IND.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "NUM.COMP.100% SENZA IND.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "PSE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "VSE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "REDDITI DIPENDENTI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "REDDITI ALTRI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "COEFF.NUCLEO.FAM.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "LIMITE PENSIONI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "REDD.PREV.DIP.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "REDD.COMPLESSIVO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "REDD.IMMOBILIARI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "REDD.MOBILIARI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 25, "ISE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 26, "DETRAZIONI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 27, "DETRAZIONI FRAGILITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 28, "ISEE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 29, "ISR")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 30, "ISP")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 31, "ISEE 27")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 32, "AREA ECONOMICA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 33, "SOTTO AREA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 34, "LIMITE ISEE SUPERATO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 35, "DECADENZA ALL. ADEGUATO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 36, "DECADENZA.VAL.ICI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 37, "PATRIMONIO SUP.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 38, "ANNO COSTRUZIONE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 39, "LOCALITA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 40, "NUMERO PIANO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 41, "PRESENTE ASCENSORE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 42, "PIANO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 43, "DEM")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 44, "ZONA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 45, "COSTOBASE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 46, "VETUSTA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 47, "CONSERVAZIONE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 48, "SUP.NETTA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 49, "SUP.CONVENZIONALE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 50, "ALTRE SUP.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 51, "SUP.ACCESSORI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 52, "VALORE LOCATIVO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 53, "PERC.VAL.LOC.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 54, "CANONE CLASSE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 55, "PERC.ISTAT APPLICATA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 56, "CANONE CLASSE ISTAT")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 57, "INC.MAX")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 58, "CANONE SOPPORTABILE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 59, "CANONE MINIMO AREA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 60, "CANONE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 61, "CANONE ATTUALE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 62, "ADEGUAMENTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 63, "ISTAT")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 64, "NOTE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 65, "ANNOTAZIONI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 66, "CANONE L.91")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 67, "TIPO CANONE APPLICATO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 68, "COMPETENZA 1 ANNO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 69, "COMPETENZA 2 ANNO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 70, "SCONTO COSTO BASE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 71, "CANONE 1243 1 ANNO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 72, "CANONE 1243 2 ANNO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 73, "DELTA 1243 1 ANNO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 74, "DELTA 1243 2 ANNO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 75, "ESCLUSIONE 1243")
                        K = 2
                        For Each row In dt.Rows

                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("SPORTELLO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("TIPOLOGIA_CONTRATTO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("PG_DICHIARAZIONE_AU"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("STATO_AU"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("DIFFIDA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("DATA_GENERAZIONE_DIFFIDA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("DATA_STIPULA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("NUM_COMP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("MINORI_15"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("MAGGIORI_65"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("NUM_COMP_66"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(dt.Rows(i).Item("NUM_COMP_100_CON"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(dt.Rows(i).Item("NUM_COMP_100"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(dt.Rows(i).Item("PSE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(dt.Rows(i).Item("VSE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.IfNull(dt.Rows(i).Item("REDDITI_DIPENDENTI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.IfNull(dt.Rows(i).Item("REDDITI_ALTRI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.IfNull(dt.Rows(i).Item("COEFF_NUCLEO_FAM"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.IfNull(dt.Rows(i).Item("LIMITE_PENSIONI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.IfNull(dt.Rows(i).Item("REDD_PREV_DIP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.IfNull(dt.Rows(i).Item("REDD_COMPLESSIVO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, par.IfNull(dt.Rows(i).Item("REDD_IMMOBILIARI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, par.IfNull(dt.Rows(i).Item("REDD_MOBILIARI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, par.IfNull(dt.Rows(i).Item("ISE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, par.IfNull(dt.Rows(i).Item("DETRAZIONI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, par.IfNull(dt.Rows(i).Item("DETRAZIONI_FRAGILITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, par.IfNull(dt.Rows(i).Item("ISEE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, par.IfNull(dt.Rows(i).Item("ISR"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, par.IfNull(dt.Rows(i).Item("ISP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 31, par.IfNull(dt.Rows(i).Item("ISEE_27"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 32, par.IfNull(dt.Rows(i).Item("ID_AREA_ECONOMICA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 33, par.IfNull(dt.Rows(i).Item("SOTTO_AREA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 34, par.IfNull(dt.Rows(i).Item("LIMITE_ISEE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 35, par.IfNull(dt.Rows(i).Item("DECADENZA_ALL_ADEGUATO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 36, par.IfNull(dt.Rows(i).Item("DECADENZA_VAL_ICI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 37, par.IfNull(dt.Rows(i).Item("PATRIMONIO_SUP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 38, par.IfNull(dt.Rows(i).Item("ANNO_COSTRUZIONE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 39, par.IfNull(dt.Rows(i).Item("LOCALITA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 40, par.IfNull(dt.Rows(i).Item("NUMERO_PIANO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 41, par.IfNull(dt.Rows(i).Item("PRESENTE_ASCENSORE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 42, par.IfNull(dt.Rows(i).Item("PIANO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 43, par.IfNull(dt.Rows(i).Item("DEM"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 44, par.IfNull(dt.Rows(i).Item("ZONA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 45, par.IfNull(dt.Rows(i).Item("COSTOBASE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 46, par.IfNull(dt.Rows(i).Item("VETUSTA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 47, par.IfNull(dt.Rows(i).Item("CONSERVAZIONE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 48, par.IfNull(dt.Rows(i).Item("SUP_NETTA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 49, par.IfNull(dt.Rows(i).Item("SUPCONVENZIONALE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 50, par.IfNull(dt.Rows(i).Item("ALTRE_SUP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 51, par.IfNull(dt.Rows(i).Item("SUP_ACCESSORI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 52, par.IfNull(dt.Rows(i).Item("VALORE_LOCATIVO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 53, par.IfNull(dt.Rows(i).Item("PERC_VAL_LOC"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 54, par.IfNull(dt.Rows(i).Item("CANONE_CLASSE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 55, par.IfNull(dt.Rows(i).Item("PERC_ISTAT_APPLICATA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 56, par.IfNull(dt.Rows(i).Item("CANONE_CLASSE_ISTAT"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 57, par.IfNull(dt.Rows(i).Item("INC_MAX"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 58, par.IfNull(dt.Rows(i).Item("CANONE_SOPPORTABILE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 59, par.IfNull(dt.Rows(i).Item("CANONE_MINIMO_AREA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 60, par.IfNull(dt.Rows(i).Item("CANONE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 61, par.IfNull(dt.Rows(i).Item("CANONE_ATTUALE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 62, par.IfNull(dt.Rows(i).Item("ADEGUAMENTO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 63, par.IfNull(dt.Rows(i).Item("ISTAT"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 64, par.IfNull(dt.Rows(i).Item("NOTE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 65, par.IfNull(dt.Rows(i).Item("ANNOTAZIONI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 66, par.IfNull(dt.Rows(i).Item("CANONE_91"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 67, par.IfNull(dt.Rows(i).Item("TIPO_CANONE_APPLICATO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 68, par.IfNull(dt.Rows(i).Item("COMPETENZA_1_ANNO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 69, par.IfNull(dt.Rows(i).Item("COMPETENZA_2_ANNO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 70, par.IfNull(dt.Rows(i).Item("SCONTO_COSTO_BASE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 71, par.IfNull(dt.Rows(i).Item("CANONE_1243_1_ANNO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 72, par.IfNull(dt.Rows(i).Item("CANONE_1243_2_ANNO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 73, par.IfNull(dt.Rows(i).Item("DELTA_1243_1_ANNO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 74, par.IfNull(dt.Rows(i).Item("DELTA_1243_2_ANNO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 75, par.IfNull(dt.Rows(i).Item("ESCLUSIONE_1243"), ""))
                            i = i + 1
                            K = K + 1
                        Next

                        .CloseFile()
                    End With

                End If

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String

                zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)

                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
                Dim strmFile As FileStream = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

                strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                Dim sFile As String = Path.GetFileName(strFile)
                Dim theEntry As ZipEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

                File.Delete(strFile)

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                If File.Exists(Server.MapPath("~\FileTemp\") & FileCSV & ".ZIP") Then
                    Response.Write("<script>window.open('DownLoad.aspx?C=1&F=" & par.Cripta(FileCSV) & "','Export','');</script>")

                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else

            End If

        Catch ex As Exception
            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub imgExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
        Export()
    End Sub

    Protected Sub imgExport0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport0.Click
        Export()
    End Sub

    Private Function Carica()
        Dim puntoErrore As String = ""
        Try
            Dim comunicazioni As String = ""
            Dim LimiteIsee As Integer = 0
            Dim DAFARE As Boolean
            Dim CANONE91 As String = ""
            Dim dt As New System.Data.DataTable
            Dim ROW As System.Data.DataRow
            Dim I As Integer = 0
            Dim NUMERORIGHE As Long = 0
            Dim Contatore As Long = 0
            Dim Anomalia As Boolean = False
            Dim AnnoAU As String = ""
            Dim AnnoRedditi As String = ""

            Dim ISTAT_1_PR As Double = 0
            Dim ISTAT_2_PR As Double = 0
            Dim ISTAT_1_AC As Double = 0
            Dim ISTAT_2_AC As Double = 0
            Dim ISTAT_1_PE As Double = 0
            Dim ISTAT_2_PE As Double = 0
            Dim ISTAT_1_DE As Double = 0
            Dim ISTAT_2_DE As Double = 0

            Dim ICI_1_2 As Double = 0
            Dim ICI_3_4 As Double = 0
            Dim ICI_5_6 As Double = 0
            Dim ICI_7 As Double = 0

            Dim LimiteA4 As Double = 0
            Dim LimiteA5 As Double = 0
            Dim InizioB1 As Double = 0
            Dim InizioC12 As Double = 0

            Dim CanoneMinimoA5 As Double = 0
            Dim Perc_Inc_ISE_A5 As Double = 0
            Dim Perc_Inc_Loc_A5 As Double = 0

            Dim CanoneMinimoB1 As Double = 0
            Dim Perc_Inc_ISE_B1 As Double = 0
            Dim Perc_Inc_Loc_B1 As Double = 0

            Dim CanoneMinimoC12 As Double = 0
            Dim Perc_Inc_ISE_C12 As Double = 0
            Dim Perc_Inc_Loc_C12 As Double = 0

            Dim CanoneMinimoD4 As Double = 0
            Dim Perc_Inc_ISE_D4 As Double = 0
            Dim Perc_Inc_Loc_D4 As Double = 0

            Dim LimitePensioneAU As Double = 0

            Dim InizioCanone As String = ""
            Dim FineCanone As String = ""

            Dim S As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            Dim ElencoGruppi As String = ""
            If Request.QueryString("T") = "1" Then
                ElencoGruppi = Session.Item("ElencoSimulazione")
            End If

            Dim S1 As String = ""
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim S2 As String = ""
            Dim ss As String = "("


            dt.Columns.Add("SPORTELLO")
            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("TIPOLOGIA_CONTRATTO")
            dt.Columns.Add("PG_DICHIARAZIONE_AU")
            dt.Columns.Add("DIFFIDA")
            dt.Columns.Add("DATA_GENERAZIONE_DIFFIDA")
            dt.Columns.Add("DATA_STIPULA")
            dt.Columns.Add("NUM_COMP")
            dt.Columns.Add("MINORI_15")
            dt.Columns.Add("MAGGIORI_65")
            dt.Columns.Add("NUM_COMP_66")
            dt.Columns.Add("NUM_COMP_100")
            dt.Columns.Add("NUM_COMP_100_CON")
            dt.Columns.Add("PSE")
            dt.Columns.Add("VSE")
            dt.Columns.Add("REDDITI_DIPENDENTI")
            dt.Columns.Add("REDDITI_ALTRI")
            dt.Columns.Add("COEFF_NUCLEO_FAM")
            dt.Columns.Add("LIMITE_PENSIONI")
            dt.Columns.Add("REDD_PREV_DIP")
            dt.Columns.Add("REDD_COMPLESSIVO")
            dt.Columns.Add("REDD_IMMOBILIARI")
            dt.Columns.Add("REDD_MOBILIARI")
            dt.Columns.Add("ISE")
            dt.Columns.Add("DETRAZIONI")
            dt.Columns.Add("DETRAZIONI_FRAGILITA")
            dt.Columns.Add("ISEE")
            dt.Columns.Add("ISR")
            dt.Columns.Add("ISP")
            dt.Columns.Add("ISEE_27")
            dt.Columns.Add("ID_AREA_ECONOMICA")
            dt.Columns.Add("SOTTO_AREA")
            dt.Columns.Add("LIMITE_ISEE")
            dt.Columns.Add("DECADENZA_ALL_ADEGUATO")
            dt.Columns.Add("DECADENZA_VAL_ICI")
            dt.Columns.Add("PATRIMONIO_SUP")
            dt.Columns.Add("ANNO_COSTRUZIONE")
            dt.Columns.Add("LOCALITA")
            dt.Columns.Add("NUMERO_PIANO")
            dt.Columns.Add("PRESENTE_ASCENSORE")
            dt.Columns.Add("PIANO")
            dt.Columns.Add("DEM")
            dt.Columns.Add("ZONA")
            dt.Columns.Add("COSTOBASE")
            dt.Columns.Add("VETUSTA")
            dt.Columns.Add("CONSERVAZIONE")
            dt.Columns.Add("SUP_NETTA")
            dt.Columns.Add("SUPCONVENZIONALE")
            dt.Columns.Add("ALTRE_SUP")
            dt.Columns.Add("SUP_ACCESSORI")
            dt.Columns.Add("VALORE_LOCATIVO")
            dt.Columns.Add("PERC_VAL_LOC")
            dt.Columns.Add("CANONE_CLASSE")
            dt.Columns.Add("PERC_ISTAT_APPLICATA")
            dt.Columns.Add("CANONE_CLASSE_ISTAT")
            dt.Columns.Add("INC_MAX")
            dt.Columns.Add("CANONE_SOPPORTABILE")
            dt.Columns.Add("CANONE_MINIMO_AREA")
            dt.Columns.Add("CANONE")
            dt.Columns.Add("CANONE_ATTUALE")
            dt.Columns.Add("ADEGUAMENTO")
            dt.Columns.Add("ISTAT")
            dt.Columns.Add("NOTE")
            dt.Columns.Add("ANNOTAZIONI")
            dt.Columns.Add("CANONE_91")
            dt.Columns.Add("ID_DICHIARAZIONE")
            dt.Columns.Add("ID_GRUPPO")
            dt.Columns.Add("TIPO_CANONE_APPLICATO")
            dt.Columns.Add("COMPETENZA_1_ANNO")
            dt.Columns.Add("COMPETENZA_2_ANNO")

            dt.Columns.Add("SCONTO_COSTO_BASE")
            dt.Columns.Add("CANONE_1243_1_ANNO")
            dt.Columns.Add("CANONE_1243_2_ANNO")
            dt.Columns.Add("DELTA_1243_1_ANNO")
            dt.Columns.Add("DELTA_1243_2_ANNO")
            dt.Columns.Add("ESCLUSIONE_1243")
            dt.Columns.Add("STATO_AU")

            Dim TipoProvenienza As String = ""
            'COMPETENZA_1_ANNO

            puntoErrore = "1"

            Dim IDAU As Long = 0
            Dim ss3 As String = "("
            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE ID=" & lIdAU
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IDAU = myReader("ID")
                AnnoAU = myReader("anno_au")
                AnnoRedditi = myReader("anno_isee")
                InizioCanone = myReader("inizio_canone")
                FineCanone = myReader("fine_canone")
                TipoProvenienza = par.IfNull(myReader("ID_TIPO_PROVENIENZA"), "NULL")

                If par.IfNull(myReader("erp_1"), "") = "1" Then
                    ss3 = ss3 & " ((rapporti_utenza.provenienza_ass = 1 OR rapporti_utenza.provenienza_ass = 2) AND unita_immobiliari.id_destinazione_uso <> 2) or "
                End If

                If par.IfNull(myReader("erp_2"), "") = "1" Then
                    ss3 = ss3 & " unita_immobiliari.id_destinazione_uso = 2 or "
                End If

                If par.IfNull(myReader("erp_FF_OO"), "") = "1" Then
                    'ss3 = ss3 & " unita_immobiliari.id_destinazione_uso = 10 or "
                    ss3 = ss3 & " rapporti_utenza.provenienza_ass = 10 or "
                End If

                If par.IfNull(myReader("erp_art_22"), "") = "1" Then
                    ss3 = ss3 & " rapporti_utenza.provenienza_ass = 8 or "
                End If

                If par.IfNull(myReader("erp_conv"), "") = "1" Then
                    ss3 = ss3 & " unita_immobiliari.id_destinazione_uso = 12 or "
                End If

                If par.IfNull(myReader("ERP_4"), "") = "1" Then

                End If

                If par.IfNull(myReader("ERP_3"), "") = "1" Then
                    ss3 = ss3 & " rapporti_utenza.dest_uso = 'D' OR "
                End If

                If par.IfNull(myReader("L39278"), "") = "1" Then

                End If

                If par.IfNull(myReader("L43198"), "") = "1" Then

                End If

                If par.IfNull(myReader("OA"), "") = "1" Then
                    ss3 = ss3 & " rapporti_utenza.provenienza_ass = 7 or "
                End If

                If ss = ss & "(" Then
                    ss3 = " (rapporti_utenza.dest_uso='XXX') and "
                Else
                    ss3 = Mid(ss3, 1, Len(ss3) - 4) & ")  and "
                End If

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI_PARAMETRI WHERE ID_AU=" & lIdAU
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                ISTAT_1_PR = myReader("ISTAT_1_PR")
                ISTAT_2_PR = myReader("ISTAT_2_PR")

                ISTAT_1_AC = myReader("ISTAT_1_AC")
                ISTAT_2_AC = myReader("ISTAT_2_AC")

                ISTAT_1_PE = myReader("ISTAT_1_PE")
                ISTAT_2_PE = myReader("ISTAT_2_PE")

                ISTAT_1_DE = myReader("ISTAT_1_DE")
                ISTAT_2_DE = myReader("ISTAT_2_DE")

                ICI_1_2 = myReader("ICI_1_2")
                ICI_3_4 = myReader("ICI_3_4")
                ICI_5_6 = myReader("ICI_5_6")
                ICI_7 = myReader("ICI_7")

                LimitePensioneAU = par.IfNull(myReader("limite_pensione"), 0)

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='A4'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                LimiteA4 = par.IfNull(myReader("ISEE_ERP"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='A5'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                LimiteA5 = par.IfNull(myReader("ISEE_ERP"), 0)
                InizioB1 = par.IfNull(myReader("ISEE_ERP"), 0) + 1
                CanoneMinimoA5 = par.IfNull(myReader("canone_minimo"), 0)
                Perc_Inc_ISE_A5 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                Perc_Inc_Loc_A5 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='C11'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                InizioC12 = par.IfNull(myReader("ISEE_ERP"), 0) + 1
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='B1'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CanoneMinimoB1 = par.IfNull(myReader("canone_minimo"), 0)
                Perc_Inc_ISE_B1 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                Perc_Inc_Loc_B1 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='C12'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CanoneMinimoC12 = par.IfNull(myReader("canone_minimo"), 0)
                Perc_Inc_ISE_C12 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                Perc_Inc_Loc_C12 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='D4'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CanoneMinimoD4 = par.IfNull(myReader("canone_minimo"), 0)
                Perc_Inc_ISE_D4 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                Perc_Inc_Loc_D4 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
            End If
            myReader.Close()


            Dim DataStampa1Anno As String = Format(Now, "yyyyMMddHHmmss")
            Dim DataStampa2Anno As String = Format(DateAdd(DateInterval.Second, 1, Now), "yyyyMMddHHmmss")

            Select Case Request.QueryString("T")
                Case "1"
                    'par.cmd.CommandText = "SELECT ROWNUM,UTENZA_GRUPPI_DICHIARAZIONI.ID_GRUPPO,(CASE WHEN UTENZA_DICHIARAZIONI.ID_STATO=0 THEN 'DA COMPLETARE' WHEN UTENZA_DICHIARAZIONI.ID_STATO=1 THEN 'COMPLETA' WHEN UTENZA_DICHIARAZIONI.ID_STATO=2 THEN 'DA CANCELLARE' END) AS STATO_AU,NVL(EDIFICI.SCONTO_COSTO_BASE,-1000) AS SCONTO_COSTO_BASE,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO,convocazioni_au.*,RAPPORTI_UTENZA.COD_CONTRATTO,UTENZA_DICHIARAZIONI.PATR_SUPERATO,UTENZA_DICHIARAZIONI.PG,UTENZA_DICHIARAZIONI.ISEE,UTENZA_DICHIARAZIONI.ID_STATO,UTENZA_DICHIARAZIONI.id as idAU,UTENZA_DICHIARAZIONI.FL_SOSP_1,UTENZA_DICHIARAZIONI.FL_SOSP_2,UTENZA_DICHIARAZIONI.FL_SOSP_3,UTENZA_DICHIARAZIONI.FL_SOSP_4,UTENZA_DICHIARAZIONI.FL_SOSP_5,UTENZA_DICHIARAZIONI.FL_SOSP_7,unita_immobiliari.ID AS idunita,(SELECT (CASE WHEN TIPO=0 THEN 'INCOMPLETA' ELSE 'NON RISPONDENTE' END) FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DIFFIDA,(SELECT TO_CHAR(TO_DATE(DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DATA_GENERAZIONE_DIFFIDA FROM UTENZA_GRUPPI_DICHIARAZIONI,siscom_mi.convocazioni_au,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,UTENZA_DICHIARAZIONI,UTENZA_SPORTELLI,SISCOM_MI.EDIFICI WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_GRUPPI_DICHIARAZIONI.ID_DICHIARAZIONE AND UTENZA_GRUPPI_DICHIARAZIONI.APPLICA_AU=0 AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND rapporti_utenza.cod_contratto=UTENZA_DICHIARAZIONI.rapporto  AND rapporti_utenza.ID=convocazioni_au.id_contratto AND unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto=convocazioni_au.id_contratto AND unita_immobiliari.ID=unita_contrattuale.id_unita AND convocazioni_au.id_contratto IS NOT NULL AND data_app IS NOT NULL AND UTENZA_DICHIARAZIONI.ID IN (SELECT id_dichiarazione FROM UTENZA_GRUPPI_DICHIARAZIONI WHERE id_gruppo IN " & ElencoGruppi & ") AND convocazioni_au.id_stato=2 ORDER BY ROWNUM DESC"
                    par.cmd.CommandText = "SELECT ROWNUM,UTENZA_GRUPPI_DICHIARAZIONI.ID_GRUPPO,(CASE WHEN UTENZA_DICHIARAZIONI.ID_STATO=0 THEN 'DA COMPLETARE' WHEN UTENZA_DICHIARAZIONI.ID_STATO=1 THEN 'COMPLETA' WHEN UTENZA_DICHIARAZIONI.ID_STATO=2 THEN 'DA CANCELLARE' END) AS STATO_AU,NVL(EDIFICI.SCONTO_COSTO_BASE,-1000) AS SCONTO_COSTO_BASE,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO,convocazioni_au.*,RAPPORTI_UTENZA.COD_CONTRATTO,UTENZA_DICHIARAZIONI.PATR_SUPERATO,UTENZA_DICHIARAZIONI.PG,UTENZA_DICHIARAZIONI.ISEE,UTENZA_DICHIARAZIONI.ID_STATO,UTENZA_DICHIARAZIONI.id as idAU,UTENZA_DICHIARAZIONI.DATA_PG,UTENZA_DICHIARAZIONI.FL_SOSP_1,UTENZA_DICHIARAZIONI.FL_SOSP_2,UTENZA_DICHIARAZIONI.FL_SOSP_3,UTENZA_DICHIARAZIONI.FL_SOSP_4,UTENZA_DICHIARAZIONI.FL_SOSP_5,UTENZA_DICHIARAZIONI.FL_SOSP_7,unita_immobiliari.ID AS idunita,(SELECT (CASE WHEN TIPO=0 THEN 'INCOMPLETA' ELSE 'NON RISPONDENTE' END) FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DIFFIDA,(SELECT TO_CHAR(TO_DATE(DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DATA_GENERAZIONE_DIFFIDA FROM UTENZA_GRUPPI_DICHIARAZIONI,siscom_mi.convocazioni_au,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,UTENZA_DICHIARAZIONI,UTENZA_SPORTELLI,SISCOM_MI.EDIFICI WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_GRUPPI_DICHIARAZIONI.ID_DICHIARAZIONE AND UTENZA_GRUPPI_DICHIARAZIONI.APPLICA_AU=0 AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND rapporti_utenza.cod_contratto=UTENZA_DICHIARAZIONI.rapporto  AND rapporti_utenza.ID=convocazioni_au.id_contratto AND unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto=convocazioni_au.id_contratto AND unita_immobiliari.ID=unita_contrattuale.id_unita AND convocazioni_au.id_contratto IS NOT NULL AND data_app IS NOT NULL AND UTENZA_DICHIARAZIONI.ID IN (SELECT id_dichiarazione FROM UTENZA_GRUPPI_DICHIARAZIONI WHERE id_gruppo IN " & ElencoGruppi & ") AND convocazioni_au.id_stato=2 AND convocazioni_au.id_gruppo in (select id from siscom_mi.CONVOCAZIONI_AU_GRUPPI where id_au=" & IDAU & ") ORDER BY ROWNUM DESC"

                Case "2"
                    Dim sospese As String = Request.QueryString("S")
                    Dim Tsospese As String = ""
                    If UCase(sospese) <> "TRUE" Then
                        Tsospese = " CONVOCAZIONI_AU.ID_STATO<>1 AND "
                    End If
                    par.cmd.CommandText = "SELECT ROWNUM,'--' AS STATO_AU,'0' as PATR_SUPERATO,'48001' AS ISEE,'--' AS PG,'-1' as IDAU,NVL(EDIFICI.SCONTO_COSTO_BASE,-1000) AS SCONTO_COSTO_BASE,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO,convocazioni_au.*,RAPPORTI_UTENZA.COD_CONTRATTO,unita_immobiliari.ID AS idunita,(SELECT (CASE WHEN TIPO=0 THEN 'INCOMPLETA' ELSE 'NON RISPONDENTE' END) FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DIFFIDA,(SELECT TO_CHAR(TO_DATE(DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & IDAU & ") AS DATA_GENERAZIONE_DIFFIDA FROM siscom_mi.convocazioni_au,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,UTENZA_SPORTELLI,SISCOM_MI.EDIFICI WHERE " & Tsospese & " " & ss3 & " EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND rapporti_utenza.cod_contratto NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND NVL(FL_GENERAZ_AUTO,0)=0 AND ID_BANDO=" & IDAU & ") AND rapporti_utenza.ID=convocazioni_au.id_contratto AND unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto=convocazioni_au.id_contratto AND unita_immobiliari.ID=unita_contrattuale.id_unita AND convocazioni_au.id_contratto IS NOT NULL AND data_app IS NOT NULL AND id_gruppo IN (SELECT ID FROM siscom_mi.convocazioni_au_gruppi WHERE id_au=" & IDAU & ") ORDER BY ROWNUM DESC"

                    Dim Diffidati As String = Request.QueryString("D")
                    If UCase(Diffidati) = "TRUE" Then
                        par.cmd.CommandText = "SELECT ROWNUM,'--' AS STATO_AU,'0' as PATR_SUPERATO,'48001' AS ISEE,'--' AS PG,'-1' as IDAU,NVL(EDIFICI.SCONTO_COSTO_BASE,-1000) AS SCONTO_COSTO_BASE,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO,convocazioni_au.*,RAPPORTI_UTENZA.COD_CONTRATTO,unita_immobiliari.ID AS idunita,DECODE(TIPO,0,'INCOMPLETA',1,'NON RISPONDENTE') AS DIFFIDA,TO_CHAR(TO_DATE (data_generazione, 'YYYYmmdd'),'DD/MM/YYYY') AS data_generazione_diffida FROM siscom_mi.convocazioni_au,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,UTENZA_SPORTELLI,SISCOM_MI.EDIFICI,SISCOM_MI.DIFFIDE_LETTERE WHERE " & Tsospese & " " & ss3 & " DIFFIDE_LETTERE.ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND DIFFIDE_LETTERE.ID_AU=" & IDAU & " AND DIFFIDE_LETTERE.TIPO=1 AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND rapporti_utenza.cod_contratto NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND NVL(FL_GENERAZ_AUTO,0)=0 AND ID_BANDO=" & IDAU & ") AND rapporti_utenza.ID=convocazioni_au.id_contratto AND unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto=convocazioni_au.id_contratto AND unita_immobiliari.ID=unita_contrattuale.id_unita AND convocazioni_au.id_contratto IS NOT NULL AND data_app IS NOT NULL AND id_gruppo IN (SELECT ID FROM siscom_mi.convocazioni_au_gruppi WHERE id_au=" & IDAU & ") ORDER BY ROWNUM DESC"
                    End If

                Case "3"
                    par.cmd.CommandText = "SELECT   ROWNUM, '' AS ID_GRUPPO,'' AS DIFFIDA,'' AS DATA_GENERAZIONE_DIFFIDA,'' AS SPORTELLO,'--' AS STATO_AU,'0' as PATR_SUPERATO,'48001' AS ISEE,'--' AS PG,'-1' as IDAU,NVL (EDIFICI.sconto_costo_base, -1000) AS sconto_costo_base,rapporti_utenza.cod_tipologia_contr_loc AS tipo_contratto,rapporti_utenza.ID AS id_contratto, rapporti_utenza.cod_contratto,unita_immobiliari.ID AS idunita FROM siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,siscom_mi.EDIFICI,UTENZA_GRUPPI_DICHIARAZIONI WHERE RAPPORTI_UTENZA.ID=UTENZA_GRUPPI_DICHIARAZIONI.ID_CONTRATTO AND UTENZA_GRUPPI_DICHIARAZIONI.ID_CONTRATTO IS NOT NULL AND UTENZA_GRUPPI_DICHIARAZIONI.ID_GRUPPO IN (SELECT ID FROM UTENZA_GRUPPI_LAVORO WHERE FL_ABUSIVI='1' AND ID_BANDO_AU=" & IDAU & ") AND EDIFICI.ID = unita_immobiliari.id_edificio AND unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto = rapporti_utenza.ID AND unita_immobiliari.ID = unita_contrattuale.id_unita AND UTENZA_GRUPPI_DICHIARAZIONI.APPLICA_AU=0 ORDER BY ROWNUM DESC"
            End Select

            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                If NUMERORIGHE = 0 Then
                    NUMERORIGHE = par.IfNull(myReader("ROWNUM"), 0)
                End If

                DAFARE = True

                If Request.QueryString("T") = "1" Then
                    If par.IfNull(myReader("ID_STATO"), "0") = "0" Then
                        If par.IfNull(myReader("fl_sosp_7"), "0") = "1" And par.IfNull(myReader("fl_sosp_1"), "0") = "0" And par.IfNull(myReader("fl_sosp_2"), "0") = "0" And par.IfNull(myReader("fl_sosp_3"), "0") = "0" And par.IfNull(myReader("fl_sosp_4"), "0") = "0" And par.IfNull(myReader("fl_sosp_5"), "0") = "0" Then
                            DAFARE = True
                        Else
                            If par.IfNull(myReader("fl_sosp_7"), "0") = "0" And par.IfNull(myReader("fl_sosp_1"), "0") = "0" And par.IfNull(myReader("fl_sosp_2"), "0") = "0" And par.IfNull(myReader("fl_sosp_3"), "0") = "0" And par.IfNull(myReader("fl_sosp_4"), "0") = "0" And par.IfNull(myReader("fl_sosp_5"), "0") = "0" Then
                                DAFARE = True
                            Else
                                DAFARE = False
                            End If
                        End If
                    End If
                End If

                If DAFARE = True Then
                    I = I + 1
                    puntoErrore = "2"
                    S = par.CalcolaCanone27_ANAGRAFE_UTENZA(par.IfNull(myReader("ID_CONTRATTO"), "0"), par.IfNull(myReader("PATR_SUPERATO"), "0"), myReader("SCONTO_COSTO_BASE"), ISTAT_1_PR, ISTAT_2_PR, ISTAT_1_AC, ISTAT_2_AC, ISTAT_1_PE, ISTAT_2_PE, ISTAT_1_DE, ISTAT_2_DE, LimiteA4, LimiteA5, InizioB1, InizioC12, CanoneMinimoA5, Perc_Inc_ISE_A5, Perc_Inc_Loc_A5, CanoneMinimoB1, Perc_Inc_ISE_B1, Perc_Inc_Loc_B1, CanoneMinimoC12, Perc_Inc_ISE_C12, Perc_Inc_Loc_C12, CanoneMinimoD4, Perc_Inc_ISE_D4, Perc_Inc_Loc_D4, LimitePensioneAU, ICI_7, ICI_5_6, ICI_3_4, ICI_1_2, AnnoAU, AnnoRedditi, InizioCanone, FineCanone, IDAU, myReader("idAU"), CDbl(myReader("idunita")), myReader("cod_contratto"), CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sISTAT2ANNO, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, sTIPOCANONEAPPLICATO, sCOMPETENZA1ANNO, sCOMPETENZA2ANNO, sESCLUSIONE1243, sDELTA12432, sDELTA12431, sCANONE12432, sCANONE12431)
                    puntoErrore = "3"
                    par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ISTAT"",(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ADEGUAMENTO"" FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & myReader("COD_CONTRATTO") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        CANONE91 = ""
                        par.cmd.CommandText = "SELECT IMP_ANN_CANONE_91_ATTUALIZZ FROM SISCOM_MI.RAPPORTI_UTENZA_EXTRA WHERE ID_CONTRATTO=" & myReader1("ID")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            CANONE91 = par.IfNull(myReader2(0), "")
                        End If
                        myReader2.Close()

                        If CDbl(par.IfNull(myReader("ISEE"), "0")) > 35000 Then
                            LimiteIsee = 1
                        Else
                            LimiteIsee = 0
                        End If

                        ROW = dt.NewRow()

                        If AreaEconomica <> -1 And AreaEconomica <> 0 Then
                            ROW.Item("SPORTELLO") = myReader("SPORTELLO")
                            ROW.Item("COD_CONTRATTO") = myReader1("COD_CONTRATTO")
                            ROW.Item("TIPOLOGIA_CONTRATTO") = myReader("TIPO_CONTRATTO")
                            ROW.Item("PG_DICHIARAZIONE_AU") = myReader("PG")
                            ROW.Item("DIFFIDA") = myReader("DIFFIDA")
                            puntoErrore = "4"
                            ROW.Item("DATA_GENERAZIONE_DIFFIDA") = myReader("DATA_GENERAZIONE_DIFFIDA")
                            ROW.Item("DATA_STIPULA") = par.FormattaData(myReader1("DATA_STIPULA"))
                            ROW.Item("NUM_COMP") = sNUMCOMP
                            ROW.Item("MINORI_15") = sMINORI15
                            ROW.Item("MAGGIORI_65") = sMAGGIORI65
                            puntoErrore = "5"
                            ROW.Item("NUM_COMP_66") = sNUMCOMP66
                            ROW.Item("NUM_COMP_100") = sNUMCOMP100
                            ROW.Item("NUM_COMP_100_CON") = sNUMCOMP100C
                            ROW.Item("PSE") = sPSE
                            puntoErrore = "6"
                            ROW.Item("VSE") = sVSE
                            ROW.Item("REDDITI_DIPENDENTI") = Format(CDbl(par.IfEmpty(sREDD_DIP, 0)), "##,##0.00")
                            ROW.Item("REDDITI_ALTRI") = Format(CDbl(par.IfEmpty(sREDD_ALT, 0)), "##,##0.00")
                            ROW.Item("COEFF_NUCLEO_FAM") = sCOEFFFAM
                            puntoErrore = "7"
                            ROW.Item("LIMITE_PENSIONI") = LimitePensioneAU
                            If sPREVDIP = "1" Then
                                ROW.Item("REDD_PREV_DIP") = "SI"
                            Else
                                ROW.Item("REDD_PREV_DIP") = "NO"
                            End If
                            ROW.Item("REDD_COMPLESSIVO") = Format(CDbl(par.IfEmpty(sCOMPLESSIVO, 0)), "##,##0.00")
                            ROW.Item("REDD_IMMOBILIARI") = Format(CDbl(par.IfEmpty(sIMMOBILIARI, 0)), "##,##0.00")
                            puntoErrore = "8"
                            ROW.Item("REDD_MOBILIARI") = Format(CDbl(par.IfEmpty(sMOBILIARI, 0)), "##,##0.00")
                            ROW.Item("ISE") = par.Tronca(par.IfEmpty(sISE, 0))
                            ROW.Item("DETRAZIONI") = Format(CDbl(par.IfEmpty(sDETRAZIONI, 0)), "##,##0.00")
                            puntoErrore = "9"
                            ROW.Item("DETRAZIONI_FRAGILITA") = Format(CDbl(par.IfEmpty(sDETRAZIONEF, 0)), "##,##0.00")
                            ROW.Item("ISEE") = par.Tronca(par.IfEmpty(sISEE, 0))
                            ROW.Item("ISR") = par.Tronca(par.IfEmpty(sISR, 0))
                            puntoErrore = "10"
                            ROW.Item("ISP") = sISP
                            ROW.Item("ISEE_27") = par.Tronca(par.IfEmpty(sISE_MIN, 0))
                            puntoErrore = "11"
                            Select Case AreaEconomica
                                Case 1
                                    ROW.Item("ID_AREA_ECONOMICA") = "PROTEZIONE"
                                Case 2
                                    ROW.Item("ID_AREA_ECONOMICA") = "ACCESSO"
                                Case 3
                                    ROW.Item("ID_AREA_ECONOMICA") = "PERMANENZA"
                                Case 4
                                    ROW.Item("ID_AREA_ECONOMICA") = "DECADENZA"
                            End Select

                            ROW.Item("SOTTO_AREA") = sSOTTOAREA
                            If LimiteIsee = 1 Then
                                ROW.Item("LIMITE_ISEE") = "SI"
                            Else
                                ROW.Item("LIMITE_ISEE") = "NO"
                            End If
                            If sALLOGGIOIDONEO = "1" Then
                                ROW.Item("DECADENZA_ALL_ADEGUATO") = "SI"
                            Else
                                ROW.Item("DECADENZA_ALL_ADEGUATO") = "NO"
                            End If
                            If sVALOCIICI = "1" Then
                                ROW.Item("DECADENZA_VAL_ICI") = "SI"
                            Else
                                ROW.Item("DECADENZA_VAL_ICI") = "NO"
                            End If
                            If par.IfNull(myReader("PATR_SUPERATO"), "0") = "1" Then
                                ROW.Item("PATRIMONIO_SUP") = "SI"
                            Else
                                ROW.Item("PATRIMONIO_SUP") = "NO"
                            End If
                            puntoErrore = "12"
                            ROW.Item("ANNO_COSTRUZIONE") = sANNOCOSTRUZIONE
                            ROW.Item("LOCALITA") = sLOCALITA
                            ROW.Item("NUMERO_PIANO") = sDESCRIZIONEPIANO
                            If sASCENSORE = "1" Then
                                ROW.Item("PRESENTE_ASCENSORE") = "SI"
                            Else
                                ROW.Item("PRESENTE_ASCENSORE") = "NO"
                            End If
                            puntoErrore = "13"
                            ROW.Item("PIANO") = sPIANO
                            ROW.Item("DEM") = sDEM
                            ROW.Item("ZONA") = sZONA
                            ROW.Item("COSTOBASE") = sCOSTOBASE
                            ROW.Item("VETUSTA") = sVETUSTA
                            ROW.Item("CONSERVAZIONE") = sCONSERVAZIONE
                            ROW.Item("SUP_NETTA") = sSUPNETTA
                            ROW.Item("SUPCONVENZIONALE") = sSUPCONVENZIONALE
                            puntoErrore = "14"
                            ROW.Item("ALTRE_SUP") = sALTRESUP
                            ROW.Item("SUP_ACCESSORI") = sSUPACCESSORI
                            ROW.Item("VALORE_LOCATIVO") = par.Tronca(par.IfEmpty(sVALORELOCATIVO, 0))
                            ROW.Item("PERC_VAL_LOC") = sPER_VAL_LOC
                            puntoErrore = "15"
                            ROW.Item("CANONE_CLASSE") = Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00")
                            ROW.Item("PERC_ISTAT_APPLICATA") = sISTAT
                            puntoErrore = "16"
                            ROW.Item("CANONE_CLASSE_ISTAT") = Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00")
                            ROW.Item("INC_MAX") = sPERC_INC_MAX_ISE_ERP 'sINCIDENZAISE
                            puntoErrore = "17"
                            ROW.Item("CANONE_SOPPORTABILE") = Format(CDbl(par.IfEmpty(sCANONESOPP, 0)), "##,##0.00")
                            ROW.Item("CANONE_MINIMO_AREA") = Format(CDbl(par.IfEmpty(sCANONE_MIN, 0)), "##,##0.00")
                            ROW.Item("CANONE") = Format(CDbl(par.IfEmpty(sCanone, 0)), "##,##0.00")
                            puntoErrore = "18"
                            ROW.Item("CANONE_ATTUALE") = Format(par.IfNull(myReader1("IMP_CANONE_INIZIALE"), 0), "##,##0.00")
                            ROW.Item("ADEGUAMENTO") = Format(par.IfNull(myReader1("ADEGUAMENTO"), 0), "##,##0.00")
                            ROW.Item("ISTAT") = par.IfNull(myReader1("ISTAT"), "0,00")
                            puntoErrore = "19"
                            ROW.Item("NOTE") = sNOTE
                            ROW.Item("ANNOTAZIONI") = sMOTIVODECADENZA
                            If CANONE91 <> "" Then
                                ROW.Item("CANONE_91") = Format(CDbl(par.IfEmpty(CANONE91, 0)), "##,##0.00")
                            Else
                                ROW.Item("CANONE_91") = ""
                            End If
                            puntoErrore = "20"
                            ROW.Item("ID_DICHIARAZIONE") = myReader("IDAU")
                            ROW.Item("ID_GRUPPO") = myReader("ID_GRUPPO")
                            ROW.Item("TIPO_CANONE_APPLICATO") = sTIPOCANONEAPPLICATO
                            puntoErrore = "21"
                            ROW.Item("COMPETENZA_1_ANNO") = sCOMPETENZA1ANNO
                            ROW.Item("COMPETENZA_2_ANNO") = sCOMPETENZA2ANNO
                            puntoErrore = "22"
                            ROW.Item("SCONTO_COSTO_BASE") = Replace(myReader("SCONTO_COSTO_BASE"), "1000", "")
                            ROW.Item("CANONE_1243_1_ANNO") = sCANONE12431
                            ROW.Item("CANONE_1243_2_ANNO") = sCANONE12432
                            puntoErrore = "23"
                            ROW.Item("DELTA_1243_1_ANNO") = sDELTA12431
                            ROW.Item("DELTA_1243_2_ANNO") = sDELTA12432
                            puntoErrore = "24"
                            ROW.Item("ESCLUSIONE_1243") = sESCLUSIONE1243
                            ROW.Item("STATO_AU") = myReader("STATO_AU")
                            puntoErrore = myReader1("PRESSO_COR")

                            'INIZIO OPERAZIONI SCRITTURA
                            Dim url As String = Replace(Replace(Replace(Replace(Replace(myReader1("cod_contratto") & "_" & myReader1("PRESSO_COR"), "\", "."), "/", "."), "*", "."), "?", "."), """", ".")

                            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & url & ".TXT", False, System.Text.Encoding.Default)
                            sr.WriteLine(S)
                            sr.Close()

                            If System.IO.File.Exists(Server.MapPath("..\FileTemp\") & url & ".TXT") = True Then

                                Dim objStream As Stream = File.Open(Server.MapPath("..\FileTemp\") & url & ".TXT", FileMode.Open)
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

                                Dim DataInizio1 As String = ""
                                Dim DataInizio2 As String = ""
                                Dim DataFine1 As String = ""
                                Dim DataFine2 As String = ""
                                Dim Applica2Anno As Boolean = True
                                puntoErrore = "26"
                                If CDec(par.IfEmpty(sCanone, 0)) = CDec(Replace(par.IfEmpty(sCOMPETENZA2ANNO, 0), ".", "")) Or ISTAT_2_PR = 0 Then
                                    Applica2Anno = False
                                    DataInizio1 = InizioCanone
                                    DataFine1 = FineCanone
                                Else
                                    Applica2Anno = True
                                    DataInizio1 = InizioCanone
                                    DataFine1 = Mid(InizioCanone, 1, 4) & "1231"
                                    DataInizio2 = Mid(FineCanone, 1, 4) & "0101"
                                    DataFine2 = Mid(FineCanone, 1, 4) & "1231"
                                End If
                                puntoErrore = "27"
                                DataStampa1Anno = Format(Now, "yyyyMMddHHmmss")
                                DataStampa2Anno = Format(DateAdd(DateInterval.Second, 1, Now), "yyyyMMddHHmmss")

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, " _
                                                       & "ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA,INCIDENZA_ISE," _
                                                       & "COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
                                                       & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
                                                       & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91," _
                                                       & "INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN,TIPO_PROVENIENZA,SCONTO_COSTO_BASE,CANONE_1243_12,DELTA_1243_12,ESCLUSIONE_1243_12,TIPO_CANONE_APP,COMPETENZA) " _
                                                       & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & par.IfEmpty(sMINORI15, 0) & "," & par.IfEmpty(sMAGGIORI65, 0) & ",'" & par.FormattaData(myReader1("DATA_STIPULA")) & "','" & myReader1("COD_CONTRATTO") & "'," & myReader1("id") & ",'" & DataStampa1Anno _
                                                       & "'," & AreaEconomica & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" _
                                                       & sREDD_ALT & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & sCanone & "',:TESTO,'" _
                                                       & par.PulisciStrSql(sNOTE) & "'," & par.IfEmpty(IDAU, "null") & ",'" & sDEM & "','" & sSUPCONVENZIONALE & "','" & sCOSTOBASE & "','" & sZONA & "','" & sPIANO & "','" _
                                                       & sCONSERVAZIONE & "','" & sVETUSTA & "','" & sINCIDENZAISE & "','" & sCOEFFFAM & "','" & sSOTTOAREA & "','" & sMOTIVODECADENZA & "'," _
                                                       & par.IfNull(myReader("PATR_SUPERATO"), 0) & ",0," & par.IfEmpty(LimiteIsee, 0) & "," & par.IfNull(myReader("IDAU"), 0) _
                                                       & ",'" & par.IfNull(myReader1("IMP_CANONE_INIZIALE"), "0,00") & "','" & par.IfNull(myReader1("ADEGUAMENTO"), "0,00") & "','" _
                                                       & par.IfNull(myReader1("ISTAT"), "0,00") & "'," & par.IfEmpty(sNUMCOMP, 0) & "," & par.IfEmpty(sNUMCOMP66, 0) & "," & par.IfEmpty(sNUMCOMP100, 0) & "," & par.IfEmpty(sNUMCOMP100C, 0) & "," & par.IfEmpty(sPREVDIP, 0) _
                                                       & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
                                                       & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','" & CANONE91 & "','" & DataInizio1 & "','" & DataFine1 _
                                                       & "','" & TipoProvenienza & "','" & Replace(myReader("SCONTO_COSTO_BASE"), "1000", "") & "','" & sCANONE12431 & "','" & sDELTA12431 & "','" & sESCLUSIONE1243 & "','" & sTIPOCANONEAPPLICATO & "'," & Mid(InizioCanone, 1, 4) & ") "
                                par.cmd.ExecuteNonQuery()
                                puntoErrore = "28"
                                If Applica2Anno = True Then
                                    '2 Anno
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, " _
                                                        & "ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA,INCIDENZA_ISE," _
                                                        & "COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
                                                        & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
                                                        & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91," _
                                                        & "INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN,TIPO_PROVENIENZA,SCONTO_COSTO_BASE,CANONE_1243_12,DELTA_1243_12,ESCLUSIONE_1243_12,TIPO_CANONE_APP,COMPETENZA) " _
                                                        & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & par.IfEmpty(sMINORI15, 0) & "," & par.IfEmpty(sMAGGIORI65, 0) & ",'" & par.FormattaData(myReader1("DATA_STIPULA")) & "','" & myReader1("COD_CONTRATTO") & "'," & myReader1("id") & ",'" & DataStampa2Anno _
                                                        & "'," & par.IfEmpty(AreaEconomica, 0) & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" _
                                                        & sREDD_ALT & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & Replace(sCOMPETENZA2ANNO, ".", "") & "',:TESTO,'" _
                                                        & par.PulisciStrSql(sNOTE) & "'," & par.IfEmpty(IDAU, "null") & ",'" & sDEM & "','" & sSUPCONVENZIONALE & "','" & sCOSTOBASE & "','" & sZONA & "','" & sPIANO & "','" _
                                                        & sCONSERVAZIONE & "','" & sVETUSTA & "','" & sINCIDENZAISE & "','" & sCOEFFFAM & "','" & sSOTTOAREA & "','" & sMOTIVODECADENZA & "'," _
                                                        & par.IfNull(myReader("PATR_SUPERATO"), 0) & ",0," & par.IfEmpty(LimiteIsee, 0) & "," & par.IfNull(myReader("IDAU"), 0) _
                                                        & ",'" & par.IfNull(myReader1("IMP_CANONE_INIZIALE"), "0,00") & "','" & par.IfNull(myReader1("ADEGUAMENTO"), "0,00") & "','" _
                                                        & par.IfNull(myReader1("ISTAT"), "0,00") & "'," & par.IfEmpty(sNUMCOMP, 0) & "," & par.IfEmpty(sNUMCOMP66, 0) & "," & par.IfEmpty(sNUMCOMP100, 0) & "," & par.IfEmpty(sNUMCOMP100C, 0) & "," & par.IfEmpty(sPREVDIP, 0) _
                                                        & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
                                                        & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT2ANNO & "','" & Replace(sCOMPETENZA2ANNO, ".", "") & "','" & CANONE91 & "','" & DataInizio2 & "','" & DataFine2 _
                                                        & "','" & TipoProvenienza & "','" & Replace(myReader("SCONTO_COSTO_BASE"), "1000", "") & "','" & sCANONE12432 & "','" & sDELTA12432 & "','" & sESCLUSIONE1243 & "','" & sTIPOCANONEAPPLICATO & "'," & Mid(FineCanone, 1, 4) & ") "
                                    par.cmd.ExecuteNonQuery()
                                End If
                                puntoErrore = "29"
                                System.IO.File.Delete(Server.MapPath("..\FileTemp\") & url & ".TXT")
                                par.cmd.Parameters.Remove(parmData)

                                buffer = Nothing
                                objStream = Nothing

                                If Request.QueryString("T") = "1" Then
                                    par.cmd.CommandText = "UPDATE UTENZA_GRUPPI_DICHIARAZIONI SET APPLICA_AU=1 WHERE ID_DICHIARAZIONE=" & myReader("IDAU") & " AND ID_GRUPPO=" & myReader("ID_GRUPPO")
                                    par.cmd.ExecuteNonQuery()
                                End If
                                If Request.QueryString("T") = "3" Then
                                    par.cmd.CommandText = "UPDATE UTENZA_GRUPPI_DICHIARAZIONI SET APPLICA_AU=1 WHERE ID_DICHIARAZIONE IS NULL AND ID_CONTRATTO=" & myReader1("id") & " AND ID_GRUPPO IN (SELECT ID FROM UTENZA_GRUPPI_LAVORO WHERE FL_ABUSIVI=1 AND ID_BANDO_AU=" & IDAU & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                                puntoErrore = "30"
                            End If


                        Else
                            ROW.Item("SPORTELLO") = myReader("SPORTELLO")
                            ROW.Item("COD_CONTRATTO") = myReader1("COD_CONTRATTO")
                            ROW.Item("TIPOLOGIA_CONTRATTO") = myReader("TIPO_CONTRATTO")
                            puntoErrore = "31"
                            ROW.Item("PG_DICHIARAZIONE_AU") = ""
                            ROW.Item("DIFFIDA") = ""
                            ROW.Item("DATA_GENERAZIONE_DIFFIDA") = ""
                            ROW.Item("DATA_STIPULA") = ""
                            ROW.Item("NUM_COMP") = ""
                            ROW.Item("MINORI_15") = ""
                            ROW.Item("MAGGIORI_65") = ""
                            ROW.Item("NUM_COMP_66") = ""
                            ROW.Item("NUM_COMP_100") = ""
                            ROW.Item("NUM_COMP_100_CON") = ""
                            ROW.Item("PSE") = ""
                            ROW.Item("VSE") = ""
                            ROW.Item("REDDITI_DIPENDENTI") = ""
                            ROW.Item("REDDITI_ALTRI") = ""
                            ROW.Item("COEFF_NUCLEO_FAM") = ""
                            ROW.Item("LIMITE_PENSIONI") = ""
                            ROW.Item("REDD_PREV_DIP") = ""
                            ROW.Item("REDD_COMPLESSIVO") = ""
                            ROW.Item("REDD_IMMOBILIARI") = ""
                            ROW.Item("REDD_MOBILIARI") = ""
                            ROW.Item("ISE") = ""
                            ROW.Item("DETRAZIONI") = ""
                            ROW.Item("DETRAZIONI_FRAGILITA") = ""
                            ROW.Item("ISEE") = ""
                            ROW.Item("ISR") = ""
                            ROW.Item("ISP") = ""
                            ROW.Item("ISEE_27") = ""
                            ROW.Item("ID_AREA_ECONOMICA") = ""
                            ROW.Item("SOTTO_AREA") = ""
                            ROW.Item("LIMITE_ISEE") = ""
                            ROW.Item("DECADENZA_ALL_ADEGUATO") = ""
                            ROW.Item("DECADENZA_VAL_ICI") = ""
                            ROW.Item("PATRIMONIO_SUP") = ""
                            ROW.Item("ANNO_COSTRUZIONE") = ""
                            ROW.Item("LOCALITA") = ""
                            ROW.Item("NUMERO_PIANO") = ""
                            ROW.Item("PRESENTE_ASCENSORE") = ""
                            ROW.Item("PIANO") = ""
                            ROW.Item("DEM") = ""
                            ROW.Item("ZONA") = ""
                            ROW.Item("COSTOBASE") = ""
                            ROW.Item("VETUSTA") = ""
                            ROW.Item("CONSERVAZIONE") = ""
                            ROW.Item("SUP_NETTA") = ""
                            ROW.Item("SUPCONVENZIONALE") = ""
                            ROW.Item("ALTRE_SUP") = ""
                            ROW.Item("SUP_ACCESSORI") = ""
                            ROW.Item("VALORE_LOCATIVO") = ""
                            ROW.Item("PERC_VAL_LOC") = ""
                            ROW.Item("CANONE_CLASSE") = ""
                            ROW.Item("PERC_ISTAT_APPLICATA") = ""
                            ROW.Item("CANONE_CLASSE_ISTAT") = ""
                            ROW.Item("INC_MAX") = ""
                            ROW.Item("CANONE_SOPPORTABILE") = ""
                            ROW.Item("CANONE_MINIMO_AREA") = ""
                            ROW.Item("CANONE") = ""
                            ROW.Item("CANONE_ATTUALE") = ""
                            ROW.Item("ADEGUAMENTO") = ""
                            ROW.Item("ISTAT") = ""
                            ROW.Item("NOTE") = sNOTE
                            ROW.Item("ANNOTAZIONI") = sMOTIVODECADENZA
                            puntoErrore = "32"
                            ROW.Item("CANONE_91") = ""
                            ROW.Item("ID_DICHIARAZIONE") = myReader("IDAU")
                            ROW.Item("ID_GRUPPO") = ""
                            ROW.Item("TIPO_CANONE_APPLICATO") = ""
                            ROW.Item("COMPETENZA_1_ANNO") = ""
                            ROW.Item("COMPETENZA_2_ANNO") = ""

                            ROW.Item("SCONTO_COSTO_BASE") = Replace(myReader("SCONTO_COSTO_BASE"), "1000", "")
                            puntoErrore = "33"
                            ROW.Item("CANONE_1243_1_ANNO") = ""
                            ROW.Item("CANONE_1243_2_ANNO") = ""
                            ROW.Item("DELTA_1243_1_ANNO") = ""
                            ROW.Item("DELTA_1243_2_ANNO") = ""
                            ROW.Item("ESCLUSIONE_1243") = ""
                            ROW.Item("STATO_AU") = par.IfNull(myReader("STATO_AU"), "")
                            Anomalia = True
                        End If
                        dt.Rows.Add(ROW)
                    End If
                    myReader1.Close()
                End If

                Contatore = Contatore + 1

                percentuale = (Contatore * 100) / NUMERORIGHE
                Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                Response.Flush()
            End While
            myReader.Close()
            puntoErrore = "40"
            If I > 0 Then
                'DataGrid1.DataSource = dt
                'DataGrid1.DataBind()
                HttpContext.Current.Session.Add("ElencoRisultati", dt)


                Label2.Text = " - " & dt.Rows.Count & " nella lista"
                BindGrid()
                If Anomalia = True Then
                    Response.Write("<script>alert('Attenzione...sono state rilevate delle anomalie. Non è stato possibile calcolare il canone per alcune unità per mancanza di dati!\nVerificare nella lista.');</script>")
                Else
                    If ElencoGruppi <> "" Then
                        par.cmd.CommandText = "UPDATE UTENZA_GRUPPI_LAVORO SET APPLICAZIONE=1 WHERE ID IN " & ElencoGruppi
                        par.cmd.ExecuteNonQuery()
                    End If
                End If

            Else
                    Label2.Text = "0 nella lista"
                    Response.Write("<script>alert('Nessuna riga selezionata');</script>")
            End If

            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label5.Visible = True
            Label5.Text = ex.Message & " - " & puntoErrore
        End Try
    End Function

    Private Sub BindGrid()

        Dim dt As New System.Data.DataTable
        dt = CType(HttpContext.Current.Session.Item("ElencoRisultati"), Data.DataTable)
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()



    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

   
    Protected Sub DataGrid1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
