Imports System.IO
Imports SubSystems.RP
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class LETTERE_INTERRUTTIVE_Lettere
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        '<asp:DropDownList ID="cmbTipo" runat="server" Font-Names="arial" 
        '                            Font-Size="10pt" Width="350px">
        '                            <asp:ListItem Value="ABUSIVI_L_123_B1.rtf">ABUSIVI LOTTI 123 - BLOCCO 1</asp:ListItem>
        '	<asp:ListItem Value="ABUSIVI_L_123_B2.rtf">ABUSIVI LOTTI 123 - BLOCCO 2</asp:ListItem>
        '	<asp:ListItem Value="ABUSIVI_L_123_B3.rtf">ABUSIVI LOTTI 123 - BLOCCO 3</asp:ListItem>
        '                            <asp:ListItem Value="ABUSIVI_L_45_B1.rtf">ABUSIVI LOTTI 45 - BLOCCO 1</asp:ListItem>
        '                            <asp:ListItem Value="FERP_L_12345_B1.rtf">FUORI ERP LOTTI 12345 - BLOCCO 1</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B2.rtf">FUORI ERP LOTTI 12345 - BLOCCO 2</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B3.rtf">FUORI ERP LOTTI 12345 - BLOCCO 3</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B4.rtf">FUORI ERP LOTTI 12345 - BLOCCO 4</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B5.rtf">FUORI ERP LOTTI 12345 - BLOCCO 5</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B6.rtf">FUORI ERP LOTTI 12345 - BLOCCO 6</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B7.rtf">FUORI ERP LOTTI 12345 - BLOCCO 7</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B8.rtf">FUORI ERP LOTTI 12345 - BLOCCO 8</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B9.rtf">FUORI ERP LOTTI 12345 - BLOCCO 9</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B10.rtf">FUORI ERP LOTTI 12345 - BLOCCO 10</asp:ListItem>
        '	<asp:ListItem Value="FERP_L_12345_B11.rtf">FUORI ERP LOTTI 12345 - BLOCCO 11</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_CC_B1.rtf">ERP LOTTI 123 CON CONTR. CALORE - BLOCCO 1</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_CC_B2.rtf">ERP LOTTI 123 CON CONTR. CALORE - BLOCCO 2</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_CC_B3.rtf">ERP LOTTI 123 CON CONTR. CALORE - BLOCCO 3</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_CC_B4.rtf">ERP LOTTI 123 CON CONTR. CALORE - BLOCCO 4</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_CC_B5.rtf">ERP LOTTI 123 CON CONTR. CALORE - BLOCCO 5</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_45_CC_B1.rtf">ERP LOTTI 45 CON CONTR. CALORE - BLOCCO 1</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_45_NOCC_B1.rtf">ERP LOTTI 45 NO CONTR. - BLOCCO 1</asp:ListItem>
        '	<asp:ListItem Value="ERP_L_45_NOCC_B2.rtf">ERP LOTTI 45 NO CONTR. - BLOCCO 2</asp:ListItem>
        '	<asp:ListItem Value="ERP_L_45_NOCC_B3.rtf">ERP LOTTI 45 NO CONTR. - BLOCCO 3</asp:ListItem>
        '	<asp:ListItem Value="ERP_L_45_NOCC_B4.rtf">ERP LOTTI 45 NO CONTR. - BLOCCO 4</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B1.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 1</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B2.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 2</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B3.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 3</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B4.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 4</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B5.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 5</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B6.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 6</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B7.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 7</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B8.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 8</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B9.rtf">ERP LOTTI 123 NO CONTR. - BLOCCO 9</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B10.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 10</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B11.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 11</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B12.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 12</asp:ListItem>
        '	<asp:ListItem Value="ERP_L_123_NOC_B13.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 13</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B14.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 14</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B15.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 15</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B16.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 16</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B17.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 17</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B18.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 18</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B19.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 19</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B20.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 20</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B21.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 21</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B22.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 22</asp:ListItem>
        '                            <asp:ListItem Value="ERP_L_123_NOC_B23.rtf">ERP LOTTI 123 NO CONTR.- BLOCCO 23</asp:ListItem>
        '                        </asp:DropDownList>



        If Session.Item("OPERATORE_LETTERE") = "" Then
            Response.Write("<script>top.location.href=""Accesso.aspx""</script>")
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Generazione in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Generazione...</span>" _
           & "<input id=" & Chr(34) & "Text1" & Chr(34) & " readonly=" & Chr(34) & "readonly" & Chr(34) & " type=" & Chr(34) & "text" & Chr(34) & " style=" & Chr(34) & "border: 1px solid #FFFFFF; font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; text-align: center; width: 70px;" & Chr(34) & "/>" _
           & "</td></tr>" _
           & "</table></div></div><br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo='';function Mostra() {document.getElementById('Text1').value = tempo;}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"


        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str() & "font:verdana; font-size:10px;'><br><img src='../Contratti/Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        'Str = Str() & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Loading)
        Response.Flush()
        If Not IsPostBack Then
            EstraiFile()
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If txtDataStampa.Text = "" Or txtPG.Text = "" Then
            lblErrore.Text = "Inserire tutti i dati"
            lblErrore.Visible = True
            Exit Sub
        End If
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If
            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim sPosteAler As String = ""               'TUTTI i CAMPI
            Dim sPosteAlerNominativo As String = ""     '1)  Nominativo Postale (50)
            Dim sPosteAlerInd As String = ""            '3)  Indirizzo          (50)
            Dim sPosteAlerScala As String = ""          '6)  Scala              (2)
            Dim sPosteAlerInterno As String = ""        '7)  Interno            (3)
            Dim sPosteAlerCAP As String = ""            '8)  CAP                (5)
            Dim sPosteAlerLocalita As String = ""       '9)  Località           (50)
            Dim sPosteAlerProv As String = ""           '10) Provincia          (2)
            Dim sPosteAlerCodUtente As String = ""      '11) Codice Utente      (12)
            Dim sPosteAlerAcronimo As String = ""       '12) Acronimo           (4)
            Dim sPosteAlerIA As String = ""             '13) IA                 (16)
            Dim sPosteDefault As String = ""            ' per i campi 2-4-5 (Presso, casella postale, indirizzo casella postale)

            Dim fileNamePosteAler As String = ""

            Dim contenuto As String = ""
            Dim BaseFile As String = "INT_" & Format(Now, "yyyyMMddHHmmss") & "_" & cmbTipo.SelectedItem.Text
            Dim NomeFileRTF As String = BaseFile & ".RTF"
            Dim NomeFileZIP As String = BaseFile & ".ZIP"

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            Response.Write("<script>tempo='Fase 1 di 3';</script>")
            Response.Flush()
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\INTERRUTTIVE\") & NomeFileRTF, False, System.Text.Encoding.GetEncoding("iso-8859-1"))

            Dim strmZipInputStream As New ZipInputStream(File.OpenRead(Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\INTERRUTTIVE\" & Replace(cmbTipo.SelectedItem.Value, ".rtf", ".zip"))))
            Dim entry As ZipEntry = strmZipInputStream.GetNextEntry()
            ' open output file
            Dim bytesRead As Integer
            Dim out As FileStream = File.Create(Path.Combine(Server.MapPath("..\FileTemp\"), entry.Name))
            ' read input ZIP archive
            Dim mBuffer(Convert.ToInt32(strmZipInputStream.Length - 1)) As Byte
            bytesRead = strmZipInputStream.Read(mBuffer, 0, mBuffer.Length)
            If bytesRead = 0 Then Exit Sub
            out.Write(mBuffer, 0, bytesRead)
            ' close file
            out.Close()

            Response.Write("<script>tempo='Fase 2 di 3';</script>")
            Response.Flush()

            Dim kkk As Long = 0
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\FileTemp\") & cmbTipo.SelectedItem.Value, System.Text.Encoding.GetEncoding("iso-8859-1"))
            contenuto = sr1.ReadLine
            contenuto = Replace(contenuto, "$data$", txtDataStampa.Text)
            contenuto = Replace(contenuto, "$cds$", txtPG.Text)
            sr.WriteLine(contenuto)
            Do Until sr1.EndOfStream
                contenuto = sr1.ReadLine
                If Len(contenuto) > 0 Then
                    contenuto = Replace(contenuto, "$data$", txtDataStampa.Text)
                    contenuto = Replace(contenuto, "$CDS$", txtPG.Text)
                    sr.WriteLine(contenuto)
                End If
                'Response.Write("<script>tempo='Fase 2 di 3-" & kkk & "';</script>")
                'Response.Flush()
                'kkk = kkk + 1
            Loop
            sr1.Close()
            sr.Close()

            Response.Write("<script>tempo='Fase 3 di 3';</script>")
            Response.Flush()



            Dim tIPO As Integer = 0
            Select Case cmbTipo.SelectedItem.Value
                Case "ABUSIVI_L_45_B1_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=0 ORDER BY NOMINATIVO ASC"
                    tIPO = 0
                Case "ABUSIVI_L_123_B1_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=1 ORDER BY NOMINATIVO ASC"
                    tIPO = 1
                Case "ABUSIVI_L_123_B2_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=2 ORDER BY NOMINATIVO ASC"
                    tIPO = 2
                Case "FERP_L_12345_B1_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=3 ORDER BY NOMINATIVO ASC"
                    tIPO = 3
                Case "FERP_L_12345_B2_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=4 ORDER BY NOMINATIVO ASC"
                    tIPO = 4
                Case "ERP_L_45_CC_B1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=5 ORDER BY NOMINATIVO ASC"
                    tIPO = 5
                Case "ERP_L_45_NOCC_B1_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=6 ORDER BY NOMINATIVO ASC"
                    tIPO = 6
                Case "ERP_L_123_CC_B1_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=7 ORDER BY NOMINATIVO ASC"
                    tIPO = 7
                Case "ERP_L_123_NOCC_B1_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=8 ORDER BY NOMINATIVO ASC"
                    tIPO = 8
                Case "ERP_L_123_NOCC_B2_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=9 ORDER BY NOMINATIVO ASC"
                    tIPO = 9
                Case "ERP_L_123_NOCC_B3_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=10 ORDER BY NOMINATIVO ASC"
                    tIPO = 10
                Case "ERP_L_123_NOCC_B4_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=11 ORDER BY NOMINATIVO ASC"
                    tIPO = 11
                Case "ERP_L_123_NOCC_B5_1.rtf"
                    PAR.cmd.CommandText = "SELECT POSTALER_CREA_INT.*,RAPPORTI_UTENZA.CAP_COR,RAPPORTI_UTENZA.LUOGO_COR,RAPPORTI_UTENZA.SIGLA_COR FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.POSTALER_CREA_INT WHERE RAPPORTI_UTENZA.ID=TO_NUMBER(CODICE) AND  TIPO=12 ORDER BY NOMINATIVO ASC"
                    tIPO = 12
            End Select

            sPosteAler = ""
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReaderA.Read
                sPosteAlerNominativo = PAR.IfNull(myReaderA("NOMINATIVO"), "----")
                sPosteAlerInd = PAR.IfNull(myReaderA("INDIRIZZO"), "")
                sPosteAlerScala = ""
                sPosteAlerInterno = ""
                sPosteAlerCAP = PAR.IfNull(myReaderA("CAP_COR"), "")
                sPosteAlerLocalita = PAR.IfNull(myReaderA("LUOGO_COR"), "")
                sPosteAlerProv = PAR.IfNull(myReaderA("SIGLA_COR"), "")
                sPosteAlerCodUtente = Format(CDbl(PAR.IfNull(myReaderA("CODICE"), 0)), "000000000000")
                sPosteAlerAcronimo = txtPG.Text

                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
                Dim myReaderA1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderA1.Read Then
                    sPosteAlerIA = myReaderA1(0)
                End If
                myReaderA1.Close()
                PAR.cmd.CommandText = "insert into SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
                                  & " values (" & sPosteAlerIA & "," & tIPO & ",5)"
                PAR.cmd.ExecuteNonQuery()

                PAR.cmd.CommandText = "update SISCOM_MI.POSTALER_CREA_INT set id=" & sPosteAlerIA & " where tipo=" & tIPO & " and codice='" & PAR.IfNull(myReaderA("CODICE"), 0) & "'"
                PAR.cmd.ExecuteNonQuery()

                sPosteDefault = ""

                If sPosteAler <> "" Then
                    sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                Else
                    sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                End If

            Loop
            myReaderA.Close()

            'Scrivo FILE TXT POSTE *******************************
            Using sw As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\" & BaseFile & ".txt"))
                sw.Write(sPosteAler)
                sw.Close()
            End Using


            zipfic = Server.MapPath("..\ALLEGATI\INTERRUTTIVE\") & NomeFileZIP

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String

            strFile = Server.MapPath("..\ALLEGATI\INTERRUTTIVE\") & NomeFileRTF
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
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

            File.Delete(strFile)


            strFile = Server.MapPath("..\FileTemp\" & BaseFile & ".txt")
            strmFile = File.OpenRead(strFile)
            Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
            sFile = Path.GetFileName(strFile)
            theEntry = New ZipEntry(sFile)
            fi = New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer1)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
            File.Delete(strFile)

            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            EstraiFile()

            PAR.myTrans.Commit()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub EstraiFile()
        Try
            Dim MiaSHTML As String = ""
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile()
            Dim j As Integer


            lblTabella.Text = "<table border='0' cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf
            '& "<tr>" & vbCrLf _
            ' & "<td width='57%'><font face='Arial' size='2'>File</font></td>" & vbCrLf _
            ' & "<td width='33%'><font face='Arial' size='2'>Dettagli File</font></td>" & vbCrLf _
            ' & "<td width='10%'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf _
            ' & "</tr>" & vbCrLf

            'lblTabella.Text = "<table border='0' cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf _
            '  & "<tr>" & vbCrLf _
            '   & "<td width='90%'><font face='Arial' size='2'>File</font></td>" & vbCrLf _
            '                             & "<td width='10%'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf _
            '               & "</tr>" & vbCrLf

            i = 0

            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='57%' bgcolor='#6699FF'><font face='Arial' size='1'>Nome del File</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='33%' bgcolor='#6699FF'><font face='Arial' size='1'>Tipo File</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='10%' bgcolor='#6699FF'><font size='1' face='Arial'>Data Creazione</font></td>" & vbCrLf

            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/INTERRUTTIVE/"), FileIO.SearchOption.SearchTopLevelOnly, "INT_*.ZIP")
                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = foundFile
                i = i + 1
            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To i - 2
                For jj = kk + 1 To i - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "_") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "_") + 1, 14)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            If i > 0 Then
                For j = 0 To i - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='57%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/INTERRUTTIVE/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='33%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & Mid(RicavaFile(ElencoFile(j)), 20, InStr(RicavaFile(ElencoFile(j)), ".") - 20) & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='10%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & PAR.FormattaData(Mid(RicavaFile(ElencoFile(j)), InStr(RicavaFile(ElencoFile(j)), "_") + 1, 8)) & "</font></td>" & vbCrLf

                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If

                Next j
            End If
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            lblTabella.Text = lblTabella.Text & MiaSHTML
            'lblTabella.Text = MiaSHTML

        Catch ex As Exception
            lblTabella.Text = ex.Message
        End Try

    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function
End Class
