<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_FornitoreF_Generale.ascx.vb" Inherits="Tab_FornitoreF_Generale" %>
<style type="text/css">
    .style1
    {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 10pt;
    }
</style>
<script type ="text/javascript" >
    function AutoDecimalPercentage(obj) {
        if (obj.value.replace(',', '.') != '') {
            if ((obj.value.replace(',', '.') >= 0) && (obj.value.replace(',', '.') <= 100)) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(4)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
            else {
                document.getElementById(obj.id).value = ''
                alert('La percentuale non può essere superiore a 100!')
            }
        }

    }

</script>
<table style="width: 77%;">
    <tr>
        <td>
                    <table width="98%">
                    <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" 
                            Width="72px">Tipo indirizzo*</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="DrLTipoInd" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 30px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 19px" TabIndex="5" Width="92px">
                        </asp:DropDownList></td>
                    <td style="width: 3px;">
                        &nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Indirizzo*</asp:Label></td>
                    <td style="width: 203px;">
                        <asp:TextBox ID="txtIndirizzoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" Style="z-index: 107; left: 109px;
                            top: 67px" TabIndex="6" Width="200px"></asp:TextBox></td>
                    <td style="width: 4px;">
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 288px; top: 53px">Civico*</asp:Label></td>
                    <td style="width: 542px;">
                        <asp:TextBox ID="txtCivicoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="6" Style="z-index: 107; right: 305px;
                            left: 287px; width: 47px; top: 67px" TabIndex="7"></asp:TextBox></td>
                </tr>
                    </table>
                    </td>
    </tr>
    <tr>
        <td>
                        <table width="98%">
                                        <tr>
                    <td>
                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Width="72px">CAP*</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCAP" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="5"  Width="64px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black"  Width="60px">Comune*</asp:Label></td>
                    <td style="width: 203px;">
                        <asp:TextBox ID="txtComuneResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="25" Style="z-index: 107; left: 416px; top: 69px" TabIndex="9" Width="200px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 37px; top: -286px">Pr.*</asp:Label></td>
                    <td style="width: 542px;">
                        <asp:TextBox ID="txtProvinciaResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="2" Style="z-index: 107; left: 594px;
                            top: 69px" TabIndex="10" Width="50px"></asp:TextBox></td>
                </tr>

                        </table>
                        
                        </td>
    </tr>
    <tr>
        <td>
                       <table width="98%">
                                        <tr>
                    <td>
                        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" 
                            Width="72px">Telefono</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtTel" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="11" Width="162px"></asp:TextBox></td>
                    <td style="width: 3px;">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" 
                        Style="z-index: 106; left: 154px; top: 134px" ForeColor="Black">IBAN*</asp:Label></td>
                    <td style="width: 203px;">
                    <asp:TextBox ID="txtIban" 
                        runat="server" MaxLength="27" 
                        Style="font-size: 10pt; font-family: Arial; top: 148px; left: 153px; width: 189px;" 
                        TabIndex="11" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Names="Arial" Font-Size="10pt" Width="200px"></asp:TextBox></td>
                    <td style="width: 4px;">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td style="width: 542px;">
                        <asp:RegularExpressionValidator ID="validaiban" runat="server" 
                            ControlToValidate="txtIban" ErrorMessage="Codice Errato!" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" SetFocusOnError="True" Style="font-size: 8pt; left: 425px;
                            font-family: Arial; top: 127px;" 
                            ValidationExpression="IT\d{2}[ ][a-zA-Z]\d{3}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{3}|IT\d{2}[a-zA-Z]\d{22}" 
                            Height="14px" Width="263px"></asp:RegularExpressionValidator></td>
                </tr>
   
                       </table>
                    </td>
    </tr>
    <tr>
        <td>
                        <table>
                            <tr>
                                <td>
                        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="72px">E-Mail</asp:Label>
                                </td>
                                <td>
                        <asp:TextBox ID="txtemail" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="11" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="98px">E-Mail Sede Amm.</asp:Label>
                                </td>
                                <td>
                        <asp:TextBox ID="txtEmailSedeAmm" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="11" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
    </tr>
    <tr>
        <td>
                        <table>
                            <tr>
                                <td>
                        <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="72px">IVA</asp:Label>
                                </td>
                                <td>
                        <asp:DropDownList ID="cmbIVA" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="8pt" Width="81px">
                            <asp:ListItem Value="4">4%</asp:ListItem>
                            <asp:ListItem Value="10">10%</asp:ListItem>
                            <asp:ListItem Value="20">20%</asp:ListItem>
                        </asp:DropDownList></td>
                                <td>
                        <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="100px">Percentuale Cassa</asp:Label>
                                </td>
                                <td>
                        <asp:TextBox ID="txtPercCassa" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="5" Width="80px" style="text-align: right"></asp:TextBox>
                                </td>
                                <td>
                        <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="100px">Rit. d&#39;Acconto</asp:Label>
                                </td>
                                <td>
                        <asp:TextBox ID="txtRitAcconto" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="5"  Width="80px" style="text-align: right"></asp:TextBox>
                                </td>
                                <td class="style1">
                                    %</td>
                            </tr>
                        </table>
                    </td>
    </tr>
</table>

