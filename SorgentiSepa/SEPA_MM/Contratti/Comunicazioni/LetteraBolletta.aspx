<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LetteraBolletta.aspx.vb" Inherits="Contratti_Comunicazioni_LetteraBolletta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lettera Bolletta</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Lettera
                        Bolletta</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                        Style="left: 11px; position: absolute; top: 61px" Text="Inserisci il testo che dovrà comparire nella lettera inviata all'utente insieme alla fattura e al modello MAV"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 17px;
                        position: absolute; top: 143px" Text="Rif. XXXXXX" Width="121px"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 17px;
                        position: absolute; top: 161px" Text="Cod. contratto: XXXXX" Width="121px"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 17px;
                        position: absolute; top: 191px" Text="OGGETTO: Bollettazione del $periodo$" Width="219px"></asp:Label>
                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 17px;
                        position: absolute; top: 228px" Text="Egr. Sig./Gent Signora XXXXX" Width="168px"></asp:Label>
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 681px;
                        position: absolute; top: 364px" Text="Logo Direttore" Width="90px"></asp:Label>
                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                        Style="left: 355px; position: absolute; top: 102px" Text="Logo Azienda" Width="90px"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 601px;
                        position: absolute; top: 143px" Text="Indirizzo XXXXXXX" Width="121px"></asp:Label>
                    <br />
                    <br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;
                    <asp:TextBox ID="txtTesto" runat="server" Font-Names="Arial" Font-Size="10pt" Height="103px"
                        Style="left: 18px; position: absolute; top: 246px" TextMode="MultiLine" Width="751px"></asp:TextBox>
                    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="10pt" Height="64px"
                        Style="left: 18px; position: absolute; top: 391px" TextMode="MultiLine" Width="751px"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                        Style="left: 620px; position: absolute; top: 496px" />
                    <asp:ImageButton ID="btnAnteprima" runat="server" ImageUrl="~/NuoveImm/Img_AnteprimaModelli.png"
                        Style="left: 19px; position: absolute; top: 496px" />
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/NuoveImm/img_HomeModelli.png"
                        Style="left: 706px; position: absolute; top: 496px" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
