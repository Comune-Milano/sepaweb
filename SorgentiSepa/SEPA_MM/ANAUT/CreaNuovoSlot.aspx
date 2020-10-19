<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaNuovoSlot.aspx.vb" Inherits="ANAUT_CreaNuovoSlot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
    <title>Nuovo Slot</title>
    <style type="text/css">
        .style1
        {
            font-size: large;
            font-weight: bold;
            text-align: center;
            color: #FFFFFF;
        }
    </style>
</head>
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
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td bgcolor="Maroon" class="style1">
                    CREA NUOVO/I SLOT LIBERI</td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Sportello in cui saranno inseriti gli slot"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSede" runat="server" ReadOnly="True" Width="248px" 
                                    BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Durata Appuntamento in minuti"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDurata" runat="server" ReadOnly="True" Width="37px" 
                                    BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Orario"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbInizioM" runat="server" TabIndex="9">
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem Selected="True">08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                            &nbsp;: <asp:DropDownList ID="cmbInizioM1" runat="server" TabIndex="10">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                        </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Giorno"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGiorno" runat="server" Width="90px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Numero di slot da inserire"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbOperatori" runat="server">
                                    <asp:ListItem Selected="True">1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblerrore" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Visible="False" ForeColor="#CC0000"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:ImageButton ID="imgSalva" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaGrande.png" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img id="IMGeSCI" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" onclick="self.close();" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
