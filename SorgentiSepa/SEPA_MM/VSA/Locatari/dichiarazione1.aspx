<%@ Page Language="VB" AutoEventWireup="false" CodeFile="dichiarazione1.aspx.vb"
    Inherits="VSA_Locatari_dichiarazione1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import dei dati</title>
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; height: 522px; top: 0px; left: 0px;">
    <div>
        <table style="position: absolute; left: 8px; width: 800px; top: 11px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Gestione
                        Locatari - Dichiarazione</strong></span><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;">
                    <asp:Label ID="lblIntest" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;" width="100%">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;">
                    <asp:Image ID="icona" runat="server" ImageUrl="../../Images/icon_edit.jpg" Visible="False" />
                    <asp:Label ID="lblLink" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        ForeColor="Black"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;">
                    <asp:Label ID="lblErr" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#801F1C"
                        Visible="False" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;">
                    <asp:Label ID="lblMsgReca" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#801F1C"
                        Visible="False" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;">
                    <asp:Image ID="imgAlert" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" />
                    <asp:Label ID="lblAmpl" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        ForeColor="Black" Text="Inserire nella dichiarazione i nuovi componenti del nucleo!"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr align="right">
                <td>
                    <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../../NuoveImm/Img_Procedi.png"
                        ToolTip="Procedi" Visible="False" onclientclick="Nascondi();" />
                    <asp:ImageButton ID="btnEsci2" runat="server" ImageUrl="../../NuoveImm/Img_EsciCorto.png"
                        Visible="False" OnClientClick="javascript:window.close();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr align="right">
                <td>
                    <asp:ImageButton ID="btnSi" runat="server" ImageUrl="../../NuoveImm/Img_SI.png" 
                        OnClientClick="Nascondi();document.getElementById('LBLrisp').value=1;" />&nbsp&nbsp&nbsp
                    <asp:ImageButton ID="btnNo" runat="server" ImageUrl="../../NuoveImm/Img_NO.png" 
                        OnClientClick="Nascondi();document.getElementById('LBLrisp').value=0;" />&nbsp&nbsp&nbsp
                    <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../NuoveImm/Img_EsciCorto.png"
                        OnClientClick="javascript:window.close();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp&nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="SoloLettura" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="LBLrisp" runat="server" Value="0" />
                    <asp:HiddenField ID="CodContratto" runat="server" Value="0" />
                    <asp:HiddenField ID="intestatario" runat="server" Value="0" />
                    <asp:HiddenField ID="intestNewRU" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="causale" runat="server" />
                    <asp:HiddenField ID="tipoRich" runat="server" />
                    <asp:HiddenField ID="ModRichiesta" runat="server" />
                    <asp:HiddenField ID="codFisc" runat="server" />
                    <asp:HiddenField ID="id_contr" runat="server" />
                    <asp:HiddenField ID="id_intestatario" runat="server" />
                    <asp:HiddenField ID="importRedd" runat="server" Value="0" />
                    <asp:HiddenField ID="stampaDeb" runat="server" Value="" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script language="javascript" type="text/javascript">

        function TornaIndietro() {
            location.replace('RicercaContratti.aspx');
        }
        function Nascondi() {
            if (document.getElementById('btnSi')) {
                document.getElementById('btnSi').style.visibility = 'hidden';
            }
            if (document.getElementById('btnProcedi')) {
                document.getElementById('btnProcedi').style.visibility = 'hidden';
            }
            if (document.getElementById('btnNo')) {
                document.getElementById('btnNo').style.visibility = 'hidden';
            }
        }
    </script>
</body>
</html>
