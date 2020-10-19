<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProposteDec.aspx.vb" Inherits="ANAUT_ProposteDec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proposta Decadenza</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <table 
        style="left: 0px; background-image: url('../NuoveImm/SfondoMascheraRubrica.jpg');
            width: 498px; position: absolute; top: 0px; background-repeat: no-repeat;">
            <tr>
            <td>
        <asp:HiddenField ID="IDDIC" runat="server" />
    
    
                <asp:HiddenField ID="IDPROP" runat="server" />
    
    
                <br />
    
    
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="12pt" Text="Dichiarazione"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="9pt" 
                    Text="I requisiti non selezionati nella lista sottostante si intendono NON PIU' POSSEDUTI!"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="ChM1" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Cittadinanza  o Soggiorno" TabIndex="1" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="ChM2" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Assenza di assegnazione in proprietà" TabIndex="2" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM3" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Assenza di Decadenza per Attività Illecite" TabIndex="3" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM4" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Assenza di cessione alloggio ERP" TabIndex="4" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM5" runat="server" Font-Names="arial" Font-Size="9pt" 
                    
                    
                    Text="Assenza di possesso UI adeguata al nucleo e/o di valore (RR 1/2004 art. 18)" 
                    TabIndex="5" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM6" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Assenza di morosità da alloggio ERP ultimi 5 anni" TabIndex="6" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM7" runat="server" Font-Names="arial" Font-Size="9pt" 
                    
                    
                    Text="5/2008 (ex art.8 R.R. 1/2007 c.l. lett i) Occupazione abusiva ultimi 5 anni" 
                    TabIndex="7" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM8" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Inutilizzo dell'alloggio" TabIndex="8" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM9" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Cambio destinazione d'uso" TabIndex="9" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM10" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Gravi Danni" TabIndex="10" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM11" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Utilizzo per attività illecite" TabIndex="11" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM12" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Inadempimento a seguito di diffida" TabIndex="12" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM13" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Inadempimento art. 20-21 RR 1/2004" TabIndex="13" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:CheckBox ID="ChM14" runat="server" Font-Names="arial" Font-Size="9pt" 
                    Text="Valore Immobile superiore al limite" TabIndex="14" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
&nbsp;<table style="width:100%;">
                    <tr>
                        <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="9pt" Text="Data"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtData" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" 
                    TabIndex="16"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                    runat="server" ControlToValidate="txtData"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" 
                    Font-Names="arial" Font-Size="8pt"
                    TabIndex="300" 
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="9pt" Text="Note"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="311px" MaxLength="500" 
                    TabIndex="17" Height="60px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                <asp:ImageButton ID="imgSalva" runat="server" 
                    ImageUrl="~/NuoveImm/Img_SalvaGrande.png" TabIndex="18" />
&nbsp;&nbsp;&nbsp;&nbsp;
                <img id="imgChiudi" alt="Chiudi" src="../NuoveImm/Img_EsciCorto.png" onclick="self.close();" style="cursor:pointer"/></td>
                    </tr>
                </table>
                    </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" 
                    ForeColor="Red" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        </td>
        </tr>
    </table> 
    </form>
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
</body>
</html>
