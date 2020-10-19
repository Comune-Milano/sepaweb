<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicevuteImposteAnteAler.aspx.vb" Inherits="Contratti_REG_PREGRESSE_RicevuteImposteAnteAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ricevuta Entratel</title>
    <style type="text/css">
        #contenitore
        {
            top: 228px;
            left: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Ricevute ENTRATEL 
                    per Adempimenti Ann.Successive-Modello RLI - ANTE GESTORE</span></strong><br />
                    <br />
                    <br />
                    <br />
        <asp:Button ID="Button1" runat="server" Font-Names="arial"
            Font-Size="9pt" Style="z-index: 100; left: 575px;
            position: absolute; top: 161px; height: 24px; width: 141px;" 
                        Text="Invia ed Elabora file" OnClick="Button1_Click" />
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
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 674px; position: absolute; top: 489px" ToolTip="Home" 
                            TabIndex="10" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
            Style="z-index: 106; left: 22px; position: absolute; top: 489px" ToolTip="Home" 
                            TabIndex="10" Visible="False" />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 456px; height: 13px; width: 758px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            
            
            Style="z-index: 102; left: 18px; position: absolute; top: 87px; width: 337px;">Seleziona il file REL delle ricevute da elaborare</asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            
            
            
            Style="z-index: 102; left: 18px; position: absolute; top: 121px; width: 337px;">Seleziona il file PDF associato alla ricevuta</asp:Label>
        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
            
            Style="z-index: 101; left: 351px; position: absolute; top: 86px; width: 307px; right: 882px;" 
            TabIndex="1" />
            <asp:FileUpload ID="FileUpload2" runat="server" Font-Names="arial" Font-Size="8pt"
            
            Style="z-index: 101; left: 351px; position: absolute; top: 120px; width: 307px; right: 882px;" 
            TabIndex="1" />
            <div id="contenitore" 
            
            style="position: absolute; width: 759px; height: 221px; overflow: auto;left=0px">
            <asp:Label ID="lblDescrizione" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; width: 100%;"></asp:Label>
                <br />
            <asp:DataGrid ID="dgRisultati" runat="server" AutoGenerateColumns="False"
                            BackColor="White" Font-Bold="False" Font-Italic="False" 
                    Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="20" Style="z-index: 105" Width="150%">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="NOME_FILE" HeaderText="FILE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ESITO" HeaderText="ESITO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid>
          </div>
    </div>
    </form>
        <script type ="text/javascript" >

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

            document.getElementById('dvvvPre').style.visibility = 'hidden';
</script>
</body>
</html>
