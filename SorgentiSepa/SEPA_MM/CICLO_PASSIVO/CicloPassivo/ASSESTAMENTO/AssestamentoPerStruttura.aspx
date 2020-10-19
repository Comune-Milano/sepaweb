<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssestamentoPerStruttura.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_AssestamentoPerStruttura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASSESTAMENTO ESERCIZIO FINANZIARIO</title>
    <style type="text/css">
        #form1
        {
            width: 783px;
            height: 517px;
        }
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 12pt;
            color: #801f1c;
            text-align: center;
        }
        .style2
        {
            font-size: 12pt;
        }
    </style>
    <script type="text/javascript">
        function ConvalidaAler() {
            var chiediConferma2
            if (document.getElementById('CONThidden').value == '1') {
                chiediConferma2 = window.confirm("Attenzione...Confermi di voler approvare questo assestamento?\nGli importi APPROVATI vuoti verranno convalidati per l\'importo richiesto!");
            } else {
                chiediConferma2 = window.confirm("Attenzione...Confermi di voler approvare questo assestamento?");
            }
            if (chiediConferma2 == true) {
                document.getElementById('ConfAlerCompleto').value = '1';
            }
            else {
                document.getElementById('ConfAlerCompleto').value = '0';
            }
        }
    </script>
</head>
<body >
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 777px; height: 525px; visibility: hidden;
        vertical-align: top; line-height: normal; top: 15px; left: 12px; background-color: #FFFFFF;">
        <table style="height: 100%; width: 100%">
            <tr>
                <td style="width: 100%; height: 100%; vertical-align: middle; text-align: center">
                    <img src='../../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblCaricamento" Font-Names="Arial" Font-Size="10pt">caricamento in corso...</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <form id="form1" runat="server">
    <asp:HiddenField ID="idstato" runat="server" Value="0" />
    <asp:HiddenField ID="idassestamento" runat="server" Value="0" />
    <asp:HiddenField ID="ConfAlerCompleto" runat="server" Value="0" />
    <asp:HiddenField ID="CONThidden" runat="server" Value="0" />
    <table style="width: 99%; position: absolute; top: 15px; left: 9px;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 7%">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../../../NuoveImm/Img_Indietro.png"
                                ToolTip="Indietro" alt="Indietro" />
                        </td>
                        <td style="width: 7%">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png"
                                ToolTip="Esci" />
                        </td>
                        <td class="style1" style="width: 86%">
                            <span class="style2">ASSESTAMENTO ESERCIZIO FINANZIARIO
                                <asp:Label ID="esercizio" runat="server" Text=""></asp:Label></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 20px;">
            </td>
        </tr>
        <tr>
            <td class="style3" style="text-align: center; font-size: 1pt;">
                <table width="100%">
                    <tr>
                        <td style="width: 20%; text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text="Seleziona la struttura" Font-Names="Arial"
                                Font-Size="10pt"></asp:Label>
                        </td>
                        <td style="width: 60%">
                            <asp:DropDownList ID="ddlStrutture" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: left;">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../../../CICLO_PASSIVO/CicloPassivo/Plan/Immagini/img_modifica.png"
                                OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                ToolTip="Modifica Assestamento" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 410px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="Label" Visible="False" Width="580px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td align="right" style="width: 100%">
                            <asp:ImageButton ID="completa" runat="server" ImageUrl="../../../NuoveImm/ImgCompleta.png"
                                OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                alt="Completa Assestamento" ToolTip="Completa Assestamento" />
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="convalida" runat="server" ImageUrl="../../../NuoveImm/Img_ConvAler.png"
                                OnClientClick="ConvalidaAler();" alt="Convalida Assestamento" ToolTip="Convalida Assestamento" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript" language="javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
