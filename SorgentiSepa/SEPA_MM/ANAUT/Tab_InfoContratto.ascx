<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_InfoContratto.ascx.vb"
    Inherits="ANAUT_Tab_InfoContratto" %>
<table width="97%">
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 99%">
                <tr>
                    <td style="border: 1px solid #0066FF; vertical-align: top;">
                        <div style="overflow: auto;" id="elencoComp">
                            <table width="100%" style="font-family: Arial; font-size: 8pt;" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="5" align="center" style="background-color: #FFFFB3; font-family: Arial;
                                        font-size: 9pt;">
                                        <b>DATI CONTRATTO</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cod.Contratto
                                    </td>
                                    <td>
                                        Data Decorrenza
                                    </td>
                                    <td>
                                        Data Cessazione
                                    </td>
                                    <td>
                                        Tipo Contratto
                                    </td>
                                    <td>
                                        Occupante Abusivo
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TxtRapporto" runat="server" Width="150px" Font-Names="arial" Font-Size="8pt"
                                            ReadOnly="True"></asp:TextBox><asp:Image ID="imgRU" runat="server" Style="cursor: pointer; width: 18px;
                                                    height: 18px; vertical-align: top;" 
                                            ImageUrl="../NuoveImm/Img_Info.png" ToolTip="Visualizza i dati Contratto" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataDec" runat="server" Width="150px" Font-Names="arial" Font-Size="8pt"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataCessazione" runat="server" Width="150px" Font-Names="arial"
                                            Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbTipoRU" runat="server" Width="170px" Font-Names="arial"
                                            Font-Size="8pt" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbAbusivo" runat="server" Width="170px" Font-Names="arial"
                                            Font-Size="8pt">
                                            <asp:ListItem Value="1">SI</asp:ListItem>
                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                        <table width="100%" style="font-family: Arial; font-size: 8pt;" cellpadding="1" cellspacing="1">
                            <tr>
                                <td colspan="3" align="center" style="border-bottom-style: dotted; border-bottom-width: thin;
                                    border-bottom-color: #0066FF; border-right-style: dotted; border-right-width: thin;
                                    border-right-color: #0066FF; font-family: Arial; font-size: 9pt;">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/ANAUT/img/ImgSospesa.png" Width="18px" />&nbsp<b>MOTIVI
                                        DI SOSPENSIONE</b>
                                </td>
                                <td colspan="6" align="center" style="border-bottom-style: dotted; border-bottom-width: thin;
                                    border-bottom-color: #0066FF; font-family: Arial; font-size: 9pt;">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/ANAUT/img/ImgVerifica.png" />&nbsp<b>VERIFICHE</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                                <td>
                                    &nbsp
                                </td>
                                <td style="border-right-style: dotted; border-right-width: thin; border-right-color: #0066FF;">
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chVAIN" runat="server" Text="Variaz. intestazione" Width="140px" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chCAIN" runat="server" Text="Cambio intestazione" />
                                </td>
                                <td style="border-right-style: dotted; border-right-width: thin; border-right-color: #0066FF;">
                                    <asp:CheckBox ID="chAMPL" runat="server" Text="Ampliamento" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chRedditi" runat="server" Text="Redditi" Width="140px" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chNucleo" runat="server" Text="Nucleo" Width="120px" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chImmob" runat="server" Text="Patrimonio Immob." />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chDocManc" runat="server" Text="Docum. mancante" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chVerTitol" runat="server" Text="Verifica titolarità contratto" />
                                </td>
                                <td style="border-right-style: dotted; border-right-width: thin; border-right-color: #0066FF;">
                                    <asp:CheckBox ID="chRilascio" runat="server" Text="Decreto di rilascio" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
        <td colspan="3" style="text-align: center; background-color: #FFFFB3; font-family: Arial;">
            <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">NOTE</asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="4000"
                Style="height: 80px;" TextMode="MultiLine" TabIndex="100" BorderColor="#0066FF"
                BorderStyle="Solid" BorderWidth="1px" Width="100%"></asp:TextBox>
        </td>
    </tr>
            </table>
        </td>
    </tr>
</table>
<script type="text/javascript">

    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }

    }

</script>
