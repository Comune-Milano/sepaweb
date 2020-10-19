<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaServizio.aspx.vb" Inherits="CicloPassivo_CicloPassivo_APPALTI_SceltaServizio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<!--<base target="_self"/>
<base target="<%=tipo%>"/> -->
    <title>Scelta Servizio</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Gestione
                        Voci Servizio</strong>
                    </span><br />
                    <br />

                    <br />
                    <asp:Label ID="lblServizio" runat="server" 
                        style="position:absolute; top: 91px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Scegli l&#39;esercizio</asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblServizio0" runat="server" 
                        style="position:absolute; top: 147px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Scegli un servizio</asp:Label>
                    <br />
                    <asp:DropDownList ID="cmbservizio" runat="server" 
                        style="position:absolute; top: 171px; left: 15px;" Font-Names="arial" 
                        Font-Size="10pt" Width="750px" TabIndex="1">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 483px; height: 20px;" 
                        TabIndex="4" />
                    &nbsp;<asp:DropDownList ID="cmbEsercizio" runat="server" 
                        style="position:absolute; top: 113px; left: 14px;" Font-Names="arial" 
                        Font-Size="10pt" Width="500px" TabIndex="1">
                    </asp:DropDownList>
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
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 671px; position: absolute; top: 483px" TabIndex="4"
                        ToolTip="Home" />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
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
