<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VisualizzaIstatERP.aspx.vb" Inherits="Contratti_VisualizzaIstatERP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="imgVisualizza" 
    defaultfocus="cmbAnno">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label7" runat="server" Text="Visualizza Aggiornamenti ISTAT"></asp:Label><br />
                    </strong></span>
                    <br />
                    <br />
                    &nbsp; &nbsp;<asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt"
                        Width="700px">Indicare quale aggiornamento istat visualizzare</asp:Label><br />
                    <br />
                    <table width="90%">
                        <tr>
                            <td style="width: 5px">
                            </td>
                            <td style="width: 54px">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                                    Style="z-index: 100; left: 50px; position: static; top: 188px" Width="51px">Periodo</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbAnno" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 116px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 185px" TabIndex="1" Width="95px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="cmbMese" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 116px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 185px" TabIndex="2" Width="95px">
                                    <asp:ListItem Value="01">Gennaio</asp:ListItem>
                                    <asp:ListItem Value="02">Febbraio</asp:ListItem>
                                    <asp:ListItem Value="03">Marzo</asp:ListItem>
                                    <asp:ListItem Value="04">Aprile</asp:ListItem>
                                    <asp:ListItem Value="05">Maggio</asp:ListItem>
                                    <asp:ListItem Value="06">Giugno</asp:ListItem>
                                    <asp:ListItem Value="07">Luglio</asp:ListItem>
                                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                                    <asp:ListItem Value="09">Settembre</asp:ListItem>
                                    <asp:ListItem Value="10">Ottobre</asp:ListItem>
                                    <asp:ListItem Value="11">Novembre</asp:ListItem>
                                    <asp:ListItem Value="12">Dicembre</asp:ListItem>
                                    <asp:ListItem Value="00">ANNO INTERO</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 5px; height: 20px">
                                &nbsp;</td>
                            <td style="width: 54px; height: 20px">
                            </td>
                            <td style="height: 20px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;</td>
                            <td style="width: 54px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;</td>
                            <td style="width: 54px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;</td>
                            <td style="width: 54px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px">
                                &nbsp;</td>
                            <td style="width: 54px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 667px; position: absolute; top: 493px" 
            ToolTip="Home" TabIndex="4" />
        <asp:ImageButton ID="imgVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            
            Style="z-index: 101; left: 535px; position: absolute; top: 493px; " 
            ToolTip="Visualizza" TabIndex="3" />
    
    </div>
    </form>
</body>
</html>

