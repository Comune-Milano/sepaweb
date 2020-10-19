<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CanoneFO.aspx.vb" Inherits="Contratti_CanoneFO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Label ID="Label10" runat="server" Font-Bold="True"
                Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 104; left: 48px; position: absolute; top: 249px"
                Visible="False">Attenzione...eventuali ducumenti potranno essere allegati utilizzando l&#39;apposita funzione presente nella maschera del rapporto.</asp:Label>
        </p>
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
        <div>
        </div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Canone
                        per stipula contratto Forze dell&#39;Ordine</strong></span><br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />

                    <asp:LinkButton ID="LinkButton1"
                        Text="Clicca qui per visualizzare i dettagli"
                        Style="position: absolute; top: 117px; left: 246px;"
                        Font-Names="Arial"
                        Font-Size="8pt"
                        OnClick="LinkButton_Click"
                        runat="server" />

                    <br />
                    &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13"
                        runat="server" ControlToValidate="txtCanone431"
                        ErrorMessage="Non valido (0,00)" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt"
                        Style="left: 138px; position: absolute; top: 161px"
                        ValidationExpression="\b\d*,\d{2}\b"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCanone27"
                        ErrorMessage="Non valido (0,00)" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt"
                        Style="left: 138px; position: absolute; top: 117px"
                        ValidationExpression="\b\d*,\d{2}\b"></asp:RegularExpressionValidator>
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtData" ErrorMessage="Non Valido (gg/mm/aaaa)" Font-Bold="True"
                        Font-Names="arial" Font-Size="9pt" Height="15px" Style="z-index: 105; left: 138px; position: absolute; top: 214px"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="155px"></asp:RegularExpressionValidator>
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 48px; position: absolute; top: 369px"
                        Visible="False">Allegato - Modulo Accettazione</asp:Label>
                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 48px; position: absolute; top: 320px"
                        Visible="False">Allegato - Verbale sopralluogo</asp:Label>
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 48px; position: absolute; top: 268px"
                        Visible="False">Allegato - Convocazione per sopralluogo</asp:Label>
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <asp:FileUpload ID="FileUpload3" runat="server" Font-Names="arial" Font-Size="8pt"
                        Style="z-index: 101; left: 46px; position: absolute; top: 387px; right: 83px; width: 671px;"
                        TabIndex="6" Visible="False" />
                    <asp:FileUpload ID="FileUpload2" runat="server" Font-Names="arial" Font-Size="8pt"
                        Style="z-index: 101; left: 46px; position: absolute; top: 337px; right: 83px; width: 671px;"
                        TabIndex="5" Visible="False" />
                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
                        Style="z-index: 101; left: 46px; position: absolute; top: 287px; right: 83px; width: 671px;"
                        TabIndex="4" Visible="False" />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                        Style="left: 623px; position: absolute; top: 515px; right: 97px;"
                        TabIndex="7" />
                    <asp:ImageButton ID="ImgIndietro" runat="server" ImageUrl="~/NuoveImm/Img_Indietro2.png"
                        Style="left: 521px; position: absolute; top: 515px; right: 149px;"
                        TabIndex="7" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 712px; position: absolute; top: 515px; height: 20px;"
                        ToolTip="Home" TabIndex="8" />
                    &nbsp;
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 143px">Canone Annuo 431/98</asp:Label>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 9px; position: absolute; top: 70px">Viene applicato il più basso dei canoni specificati.</asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 98px">Canone Annuo L.27/2007</asp:Label>
                    <asp:TextBox ID="txtCanone431" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 45px; position: absolute; top: 158px"
                        TabIndex="2" Width="82px"></asp:TextBox>
                    <asp:TextBox ID="txtCanone27" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 46px; position: absolute; top: 114px"
                        TabIndex="1" Width="82px" Enabled="False"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Font-Bold="False"
                        Font-Names="Courier New" Font-Size="8pt"
                        Style="z-index: 104; left: 319px; position: absolute; top: 73px; width: 452px;"
                        Visible="False"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 194px">Data Assegnazione</asp:Label>
                    <asp:TextBox ID="txtData" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 105; left: 46px; position: absolute; top: 212px; right: 672px;" TabIndex="3"
                        Width="82px"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
