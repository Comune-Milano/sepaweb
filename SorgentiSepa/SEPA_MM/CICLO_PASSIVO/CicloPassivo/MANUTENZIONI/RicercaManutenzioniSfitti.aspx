<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaManutenzioniSfitti.aspx.vb" Inherits="MANUTENZIONI_RicercaManutenzioniSfitti" %>

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
    <title>RICERCA</title>
</head>

<body>
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        &nbsp;
        <table style="left: 0px;
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td >
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        &nbsp; 
                        <br />
                        &nbsp; Ricerca Manutenzioni</span></strong><br />
                    <br />
                    <br />
                    <div style="left: 8px; overflow: auto; position: absolute; top: 64px; width: 728px; height: 304px;">
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <table>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 32px" Width="130px">Struttura</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="cmbStruttura" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 32px" TabIndex="1" Width="550px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 32px" Width="130px">Esercizio Finanziario</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="cmbEsercizio" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 32px" TabIndex="4" Width="550px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 48px; top: 64px" Width="130px">Edificio</asp:Label></td>
                                <td style="height: 21px">
        <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 64px" TabIndex="5"
            Width="550px" AutoPostBack="True">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
        <asp:Label ID="lblTipoServizio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
            Width="130px">Unità Immobiliare</asp:Label></td>
                                <td style="height: 21px">
        <asp:DropDownList ID="cmbUnita" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 96px" TabIndex="6"
            Width="550px" AutoPostBack="True">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp; &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblFornitore" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" Width="130px">Fornitore</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbFornitore" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 32px" TabIndex="7" Width="550px" AppendDataBoundItems="True">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LblAppalto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="130px">Num. Repertorio</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbAppalto" runat="server" BackColor="White"
                                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 64px" TabIndex="8" Width="550px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                        <asp:Label ID="lblStato" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 80px; top: 288px"
                            Width="130px">Stato ODL</asp:Label></td>
                                <td>
                        <asp:DropDownList ID="cmbStato" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 416px; border-left: black 1px solid; border-bottom: black 1px solid; top: 280px" TabIndex="9" Width="300px">
                        </asp:DropDownList></td>
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
                                <td style="vertical-align: top; text-align: left">
                    </td>
                                <td>
                    </td>
                            </tr>
                        </table>
                    </div>
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
                    &nbsp;<br />
                    &nbsp;<br />
                    <br />
                    &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 656px; position: absolute; top: 448px" ToolTip="Home" TabIndex="6" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 512px; position: absolute; top: 448px" 
                        ToolTip="Avvia Ricerca" OnClick="btnCerca_Click" />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    
    </form>
    
</body>


</html>
