<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioIntUSDFase1.aspx.vb" Inherits="Contratti_CambioIntUSDFase1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cambio Intestazione USD</title>
</head>
<body>
    <script type="text/javascript">
        
    </script>
    <form id="form1" runat="server" defaultbutton="btnProcedi" defaultfocus="txtDataCessione">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Estremi rimborso Deposito Cauzionale per Cambio Intestazione USI DIVERSI</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <table style="width:99%;">
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Intestatario/i:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblIntestatario" runat="server" Font-Names="arial" 
                                Font-Size="8pt" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Importo Credito"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCredito" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="80px" TabIndex="1" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblCREDITO0" runat="server" Font-Bold="False" 
                                Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; " Visible="False" Width="238px">Nessun credito da restituire!!</asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Importo Interessi" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInteressi" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="80px" TabIndex="2" BorderStyle="Solid" BorderWidth="1px" Enabled="False" 
                                Visible="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Data Operazione"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataOp" runat="server" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" TabIndex="3" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Tipo Mod.Restituzione"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipo" runat="server" TabIndex="4" AutoPostBack="True" 
                                CausesValidation="True" Width="450px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="lblEstremi" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Estremi" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstremi" runat="server" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" TabIndex="5" Width="450px" 
                                Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="lblAvviso" runat="server" Font-Names="arial" Font-Size="8pt" 
                                ForeColor="#000099" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Dati utili per il pagamento"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" Height="37px" TabIndex="6" 
                                TextMode="MultiLine" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp; &nbsp;</td>
                    </tr>
                </table>
                    <br />
                    <asp:HiddenField ID="INTERESSI" runat="server" />
                    <asp:HiddenField ID="DECORRENZA" runat="server" />
                    <asp:HiddenField ID="IDT" runat="server" />
                    <asp:HiddenField ID="IDC" runat="server" />
                    <asp:HiddenField ID="DATARIC" runat="server" />
                    <asp:HiddenField ID="IMPC" runat="server" />
                    <asp:HiddenField ID="RESTDEP" runat="server" />
                    <asp:HiddenField ID="NEWDEP" runat="server" />
                    <asp:HiddenField ID="IDA" runat="server" />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 9px; position: absolute; top: 423px;
                        height: 30px; width: 741px;" Visible="False"></asp:Label>
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 516px; height: 20px;" TabIndex="31"/>
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 666px; position: absolute; top: 515px" ToolTip="Home"
                        TabIndex="32" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
