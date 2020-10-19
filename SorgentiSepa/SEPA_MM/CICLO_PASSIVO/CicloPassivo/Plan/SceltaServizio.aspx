<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaServizio.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_SceltaServizio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self"/>
    <title>Scelta Servizio</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Piano Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" style="font-weight: 700"></asp:Label>
                    </span><br />
                    <br />

                    <br />
                    <asp:Label ID="lblServizio" runat="server" 
                        style="position:absolute; top: 114px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Servizio</asp:Label>
                    <asp:Label ID="Label2" runat="server" 
                        style="position:absolute; top: 63px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Voce DGR</asp:Label>                        
                    <asp:Label ID="lblVoce" runat="server" 
                        style="position:absolute; top: 84px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:DropDownList ID="cmbServizio" runat="server" 
                        style="position:absolute; top: 138px; left: 15px;" Font-Names="arial" 
                        Font-Size="10pt" Width="750px" TabIndex="1">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 546px; height: 20px;" 
                        TabIndex="4" />
    
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
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 546px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="idPianoF" runat="server" />
                    <asp:HiddenField ID="idVoce" runat="server" />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 472px; left: 20px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    
        <script type="text/javascript">
        function ConfermaEsci() {

         
                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    self.close();
                }
            
        }
    </script>
</body>
</html>
