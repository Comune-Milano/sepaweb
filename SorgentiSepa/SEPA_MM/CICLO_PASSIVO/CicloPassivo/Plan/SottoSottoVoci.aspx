<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SottoSottoVoci.aspx.vb"
    Inherits="Contabilita_CicloPassivo_Plan_SottoVoci" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Sotto Voci</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg');
            width: 798px; position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Piano
                        Finanziario-</strong>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                        -
                        <asp:Label ID="lblStato" runat="server" Style="font-weight: 700"></asp:Label>
                    </span>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblImporto10" runat="server" Style="text-align: right; position: absolute;
                        top: 416px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto9" runat="server" Style="text-align: right; position: absolute;
                        top: 386px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto8" runat="server" Style="text-align: right; position: absolute;
                        top: 356px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto7" runat="server" Style="text-align: right; position: absolute;
                        top: 326px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto6" runat="server" Style="text-align: right; position: absolute;
                        top: 296px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto5" runat="server" Style="text-align: right; position: absolute;
                        top: 266px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto4" runat="server" Style="text-align: right; position: absolute;
                        top: 236px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto3" runat="server" Style="text-align: right; position: absolute;
                        top: 206px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto1" runat="server" Style="text-align: right; position: absolute;
                        top: 146px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblImporto2" runat="server" Style="text-align: right; position: absolute;
                        top: 176px; left: 639px;" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="17px"
                        Width="75px">0,00</asp:Label>
                    <asp:Label ID="lblVoce3" runat="server" Style="position: absolute; top: 127px; left: 646px;
                        width: 74px;" Font-Bold="False" Font-Names="arial" Font-Size="8pt">Importo (lordo)</asp:Label>
                    <asp:Label ID="lblIva" runat="server" Style="position: absolute; top: 127px; left: 594px;
                        width: 74px;" Font-Bold="False" Font-Names="arial" Font-Size="8pt">Iva</asp:Label>
                    <asp:Label ID="lblVoce2" runat="server" Style="position: absolute; top: 127px; left: 508px;"
                        Font-Bold="False" Font-Names="arial" Font-Size="8pt">Servizi</asp:Label>
                    <asp:Label ID="lblVoce1" runat="server" Style="position: absolute; top: 126px; left: 99px;"
                        Font-Bold="False" Font-Names="arial" Font-Size="8pt">Descrizione Voce</asp:Label>
                    <asp:Label ID="lblVoce0" runat="server" Style="position: absolute; top: 127px; left: 31px;"
                        Font-Bold="False" Font-Names="arial" Font-Size="8pt">Codice</asp:Label>
                    <asp:Label ID="lblVoce4" runat="server" Style="position: absolute; top: 61px; left: 14px;"
                        Font-Bold="True" Font-Names="arial" Font-Size="10pt">Voce DGR</asp:Label>
                    <asp:Label ID="lblVoce" runat="server" Style="position: absolute; top: 79px; left: 14px;"
                        Font-Bold="True" Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txtVoce10" runat="server" Style="position: absolute; top: 416px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="47" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce9" runat="server" Style="position: absolute; top: 386px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="42" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce8" runat="server" Style="position: absolute; top: 356px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="37" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce7" runat="server" Style="position: absolute; top: 326px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="32" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce6" runat="server" Style="position: absolute; top: 296px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="27" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce5" runat="server" Style="position: absolute; top: 266px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="22" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce4" runat="server" Style="position: absolute; top: 236px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="17" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce3" runat="server" Style="position: absolute; top: 206px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="12" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce2" runat="server" Style="position: absolute; top: 176px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="7" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtVoce1" runat="server" Style="position: absolute; top: 146px;
                        left: 98px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="300"
                        TabIndex="2" ToolTip="Descrizione Voce" Width="380px"></asp:TextBox>
                    <asp:TextBox ID="txtCod10" runat="server" Style="position: absolute; top: 416px;
                        left: 30px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10"
                        TabIndex="46" ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod9" runat="server" Style="position: absolute; top: 386px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="41"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod8" runat="server" Style="position: absolute; top: 356px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="36"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod7" runat="server" Style="position: absolute; top: 326px; left: 30px;
                        bottom: 299px;" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10"
                        TabIndex="31" ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod6" runat="server" Style="position: absolute; top: 296px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="26"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod5" runat="server" Style="position: absolute; top: 266px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="21"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod4" runat="server" Style="position: absolute; top: 236px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="16"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod3" runat="server" Style="position: absolute; top: 206px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="11"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod2" runat="server" Style="position: absolute; top: 176px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="6"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="txtCod1" runat="server" Style="position: absolute; top: 146px; left: 30px;"
                        Font-Names="arial" Font-Size="9pt" Font-Strikeout="False" MaxLength="10" TabIndex="1"
                        ToolTip="Codice" Width="60px"></asp:TextBox>
                    <asp:DropDownList ID="cmbIva10" runat="server" Style="position: absolute; top: 416px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="49">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva9" runat="server" Style="position: absolute; top: 386px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="44">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva8" runat="server" Style="position: absolute; top: 356px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="39">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva7" runat="server" Style="position: absolute; top: 326px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="34">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva6" runat="server" Style="position: absolute; top: 296px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="29">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva5" runat="server" Style="position: absolute; top: 266px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="24">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva4" runat="server" Style="position: absolute; top: 236px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="19">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva2" runat="server" Style="position: absolute; top: 176px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="9">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva1" runat="server" Style="position: absolute; top: 146px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="4">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbIva3" runat="server" Style="position: absolute; top: 206px;
                        left: 594px;" Font-Names="arial" Font-Size="9pt" Visible="False" Width="40px"
                        TabIndex="14">
                    </asp:DropDownList>
                    <br />
                    <asp:Image ID="ImgAvviso10" runat="server" Style="position: absolute; top: 417px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso9" runat="server" Style="position: absolute; top: 387px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso8" runat="server" Style="position: absolute; top: 357px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso7" runat="server" Style="position: absolute; top: 327px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso6" runat="server" Style="position: absolute; top: 298px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso5" runat="server" Style="position: absolute; top: 268px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso4" runat="server" Style="position: absolute; top: 237px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso3" runat="server" Style="position: absolute; top: 207px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso2" runat="server" Style="position: absolute; top: 177px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <asp:Image ID="ImgAvviso1" runat="server" Style="position: absolute; top: 148px;
                        left: 7px;" ImageUrl="~/IMG/Alert.gif" Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                        Style="left: 612px; position: absolute; top: 521px; height: 20px; right: 126px;"
                        TabIndex="66" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                        ControlToValidate="txtImporto6" Display="Dynamic" ErrorMessage="Errore! 0,00"
                        Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 507px; top: 316px;
                        position: absolute; width: 51px; height: 11px; margin-bottom: 0px;" ToolTip="Inserire un valore con decimale a precisione doppia"
                        ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Image ID="imgEsci" runat="server" Style="position: absolute; top: 521px; left: 710px;
                        cursor: pointer" ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclick="ConfermaEsci();"
                        TabIndex="67" />
                    <br />
                    <br />
                    <asp:HiddenField ID="idPianoF" runat="server" />
                    <asp:HiddenField ID="idVoce" runat="server" />
                    <asp:HiddenField ID="modificato" runat="server" />
                    <asp:HiddenField ID="capitolo" runat="server" />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" Font-Bold="True" Font-Names="arial"
                        Font-Size="8pt" ForeColor="Red" Style="position: absolute; top: 504px; left: 8px;"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtImporto10" runat="server" Font-Names="Arial" Font-Size="9pt"
            Style="text-align: right; position: absolute; top: 416px; left: 506px;" Text='0,00'
            Visible="False" Width="80px" TabIndex="48"></asp:TextBox>
        <asp:TextBox ID="txtImporto9" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 386px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="43"></asp:TextBox>
        <asp:TextBox ID="txtImporto8" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 356px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="38"></asp:TextBox>
        <asp:TextBox ID="txtImporto7" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 326px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="33"></asp:TextBox>
        <asp:TextBox ID="txtImporto6" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 296px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="28"></asp:TextBox>
        <asp:TextBox ID="txtImporto5" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 266px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="23"></asp:TextBox>
        <asp:TextBox ID="txtImporto4" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 236px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="18"></asp:TextBox>
        <asp:TextBox ID="txtImporto3" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 206px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="13"></asp:TextBox>
        <asp:TextBox ID="txtImporto2" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 176px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="8"></asp:TextBox>
        <asp:TextBox ID="txtImporto1" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: right;
            position: absolute; top: 146px; left: 506px;" Text='0,00' Visible="False" Width="80px"
            TabIndex="3"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
            ControlToValidate="txtImporto10" Display="Dynamic" ErrorMessage="Errore! 0,00"
            Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 507px; top: 434px;
            position: absolute" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
            ControlToValidate="txtImporto9" Display="Dynamic" ErrorMessage="Errore! 0,00"
            Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 507px; top: 405px;
            position: absolute" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
            ControlToValidate="txtImporto8" Display="Dynamic" ErrorMessage="Errore! 0,00"
            Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 507px; top: 375px;
            position: absolute" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
            ControlToValidate="txtImporto7" Display="Dynamic" ErrorMessage="Errore! 0,00"
            Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 507px; top: 345px;
            position: absolute" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
            ControlToValidate="txtImporto5" Display="Dynamic" ErrorMessage="Errore! 0,00"
            Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 507px; top: 285px;
            position: absolute" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
            ControlToValidate="txtImporto4" Display="Dynamic" ErrorMessage="Errore! 0,00"
            Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 512px; top: 255px;
            position: absolute;" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
            ControlToValidate="txtImporto3" Display="Dynamic" ErrorMessage="Errore! 0,00"
            Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Style="left: 507px; top: 225px;
            position: absolute" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtImporto2"
            Display="Dynamic" ErrorMessage="Errore! 0,00" Font-Bold="False" Font-Names="Arial"
            Font-Size="7pt" Style="left: 507px; top: 195px; position: absolute" ToolTip="Inserire un valore con decimale a precisione doppia"
            ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtImporto1"
            Display="Dynamic" ErrorMessage="Errore! 0,00" Font-Bold="False" Font-Names="Arial"
            Font-Size="7pt" Style="left: 507px; top: 165px; position: absolute; height: 12px;"
            ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^-{0,1}\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
    </div>
    <script type="text/javascript">
        function ConfermaEsci() {

            if (document.getElementById('modificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente e perdere le modifiche effettuate?");
                if (chiediConferma == true) {
                    self.close();
                }
            }
            else {
                self.close();
            }
        }




        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\.\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //document.getElementById('txtmodificato').value = '1';
        }

        function AutoDecimal2(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }

        }
    </script>
    </form>
</body>
</html>
