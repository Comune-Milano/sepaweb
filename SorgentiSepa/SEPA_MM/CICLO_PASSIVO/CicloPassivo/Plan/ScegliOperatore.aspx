<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScegliOperatore.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_Plan_ScegliOperatore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Scelta Operatore</title>
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
                        Font-Names="arial" Font-Size="10pt">Operatore</asp:Label>
                    <asp:Label ID="lblVoce" runat="server" 
                        style="position:absolute; top: 72px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Scegli l&#39;operatore di cui vuoi gestire le voci abilitate.</asp:Label>
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
                        Style="left: 574px; position: absolute; top: 511px; height: 20px;" 
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
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 510px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="document.location.href='../../pagina_home.aspx';"/>
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
    
        
</body>
</html>
