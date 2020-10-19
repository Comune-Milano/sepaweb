<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaInquilino.aspx.vb" Inherits="MOROSITA_RicercaInquilino" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<script language="javascript" type="text/javascript">

function CompletaData(e,obj) {
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

<head id="Head1" runat="server">
    <title>RICERCA MOROSITA</title>
</head>

<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        <table style="left: 0px; top: 0px">
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        Ricerca Intestatari</span></strong> &nbsp;&nbsp;<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <table>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    </td>
                                <td style="height: 21px">
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    </td>
                                <td style="height: 21px"></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
        <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
            Width="150px">Cognome o Ragione Sociale</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtCognome" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px; text-transform: uppercase;" TabIndex="7" Width="530px" 
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del cognome o ragione sociale"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp; &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblNome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px" Width="130px">Nome</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNome" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px; text-transform: uppercase;" TabIndex="8" Width="530px" 
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del cognome "></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    </td>
                                <td>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipologia" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                        Width="130px">Stato Avanzamento Morosità</asp:Label></td>
                                <td>
        <asp:DropDownList ID="cmbEventi" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 96px" TabIndex="10"
            Width="530px">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    </td>
                                <td>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                    </td>
                                <td>
                    </td>
                            </tr>
                            <tr>
                                <td style="height: 15px">
                                </td>
                                <td style="vertical-align: top; height: 15px; text-align: left">
                                </td>
                                <td style="height: 15px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                    </td>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 512px; top: 448px" 
                        ToolTip="Avvia Ricerca" OnClick="btnCerca_Click" />
                                    &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 656px; top: 448px" ToolTip="Home" TabIndex="1" /></td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
    
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
        
</body>
</html>
