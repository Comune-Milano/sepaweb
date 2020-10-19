<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRicevuteImposte.aspx.vb" Inherits="Contratti_RicercaRicevuteImposte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Ricerca 
                    Ricevute A.E.<br />
                    </span></strong>
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 320px">
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 652px; position: absolute; top: 295px" ToolTip="Home" 
                            TabIndex="10" />
                            <img onclick="ApriRicevute();" alt="" 
                            src="../NuoveImm/Img_AvviaRicerca.png" style="position: absolute;
                    top: 295px; left: 508px; cursor: pointer;" id="Visualizza" />
                        &nbsp; &nbsp;
                        &nbsp;
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
                    <asp:HiddenField ID="DivVisible" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 366px; height: 13px; width: 442px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            
            Style="z-index: 102; left: 50px; position: absolute; top: 102px; width: 112px;">Numero Registrazione</asp:Label>
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            
            
            Style="z-index: 102; left: 50px; position: absolute; top: 136px; width: 112px;">Codice Uff. Registro</asp:Label>
        <asp:TextBox ID="TxtNumReg" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 170px; width: 93px; position: absolute;
            top: 100px" TabIndex="1" ToolTip="Codice Utente"></asp:TextBox>
            <asp:TextBox ID="txtCodUffReg" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="3" Style="z-index: 103; left: 170px; width: 34px; position: absolute;
            top: 133px" TabIndex="1" ToolTip="Codice Utente"></asp:TextBox>
    </div>
    </form>
        <script type ="text/javascript" >

            function ApriRicevute() {
                if (document.getElementById('TxtNumReg').value != '-1' && document.getElementById('TxtNumReg').value != '' && document.getElementById('txtCodUffReg').value != '') {
                    popupWindow = window.open('ElencoPagamenti.aspx?ID=-1&NR=' + document.getElementById('TxtNumReg').value + '&UF=' + document.getElementById('txtCodUffReg').value, '', '');
                    popupWindow.focus();
                }
                else {
                    alert('Inserire il numero di registrazione e il codice ufficio registro!');
                }

            }

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

