<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoAssestamento.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_NuovoAssestamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Conferma() {
            if (document.getElementById('caric').value == '1') {
                var sicuro = window.confirm('Confermi di voler creare un nuovo Assestamento per l\'esercizio finanziario selezionato?');
                if (sicuro == true) {
                    document.getElementById('prosegui').value = '1';
                }
                else {
                    document.getElementById('prosegui').value = '0';
                }
            }
        }
    </script>
</head>
<body>
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
    <asp:HiddenField runat="server" Value="0" ID="idnuovoAssestamento" />
    <asp:HiddenField runat="server" Value="0" ID="prosegui" />
    <asp:HiddenField runat="server" Value="0" ID="caric" />
    <div>
        <table width="100%">
            <tr>
                <td style="width: 100%; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Assestamento
                        Esercizio Finanziario - Nuovo<br />
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 5px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label3" runat="server" Text="Esercizio Finanziario" Font-Names="Arial"
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlanno" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Width="590px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 380px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblerrore" runat="server" Text="" Font-Names="Arial" Font-Size="9pt"
                        ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 100%;">
                    <table width="100%">
                        <tr>
                            <td style="width: 80%">
                                &nbsp;
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:ImageButton ID="btnNuovo" runat="server" alt="Nuovo Assestamento" ImageUrl="../../../NuoveImm/Img_Nuovo.png"
                                    ToolTip="Nuovo Assestamento" OnClientClick="document.getElementById('splash').style.visibility='visible';Conferma();" />
                            </td>
                            <td style="width: 10%; text-align: left;">
                                <asp:ImageButton ID="ImageButton1" runat="server" alt="Esci" ImageUrl="../../../NuoveImm/Img_EsciCorto.png"
                                    ToolTip="Esci" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
