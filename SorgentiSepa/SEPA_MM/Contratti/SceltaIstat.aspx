﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaIstat.aspx.vb" Inherits="Contratti_SceltaIstat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Adeguamento ISTAT</title>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="cmbMese" 
    defaultbutton="imgProcedi">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label7" runat="server" Text="Adeguamento ISTAT"></asp:Label><br />
                    </strong></span>
                    <br />
                    <br />
                    &nbsp;
                    
                    <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Width="700px"></asp:Label>&nbsp;<br />
                    <br />
                    &nbsp;
                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Width="700px">Aggiorna i Contratti attivi con decorrenza</asp:Label>
                    <table width="90%">
                        <tr>
                            <td style="width: 5px">
                            </td>
                            <td style="width: 54px">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                                    Style="z-index: 100; left: 50px; position: static; top: 188px" Width="51px">Periodo</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbMese" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 116px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 185px" TabIndex="1" Width="125px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 20px; width: 5px;">
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
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Visible="False"
                        Width="700px"></asp:Label>
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
            Style="z-index: 101; left: 665px; position: absolute; top: 499px" 
            ToolTip="Home" TabIndex="3" />
        <asp:ImageButton ID="imgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
            Style="z-index: 101; left: 567px; position: absolute; top: 499px; right: 483px;" 
            ToolTip="Procedi" TabIndex="2" />
    
    </div>
    </form>
</body>
</html>