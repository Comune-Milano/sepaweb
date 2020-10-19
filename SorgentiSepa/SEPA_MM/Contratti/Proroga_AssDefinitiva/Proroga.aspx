<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Proroga.aspx.vb" Inherits="Contratti_Proroga_AssDefinitiva_Proroga" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proroga Contratto</title>
    <script type="text/javascript" language="javascript">
        window.name = "modal";

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
        function confronta_data(data1, data2) {	// controllo validità formato data    

            //trasformo le date nel formato aaaammgg (es. 20081103)        
            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
            //controllo se la seconda data è successiva alla prima
            if (data2str - data1str < 0) {
                alert("La data di scadenza deve essere successiva alla data di scadenza attuale!");
                document.getElementById('txtDataScadNuova').value = '';
            } else {
                //alert("ok");
            }
        }

        function ConfermaProroga() {

            var Conferma;
            if (document.getElementById('txtDataScadNuova').value != '') {
                Conferma = window.confirm('Attenzione...Confermi di voler prorogare la data di decorrenza del contratto?');
                if (Conferma == false) {
                    document.getElementById('confermaProroga').value = '0';
                }
                else {
                    document.getElementById('confermaProroga').value = '1';
                }
            }
            else {
                alert('Inserire la data nuova di scadenza!');
            }
        }

        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <div>
        <table style="padding: 20px; width: 100%;">
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial; font-weight: bold;">
                        Proroga </span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt">Compilare i seguenti campi:</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="False"
                                    Width="150px">Data Scadenza Attuale</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataScadAtt" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="False"
                                    Width="150px">Data Scadenza Nuova</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataScadNuova" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Visible="False" Font-Bold="True" Font-Names="Arial"
                        Font-Size="10pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table align="right" width="350px">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../../NuoveImm/Img_Procedi.png"
                                    ToolTip="Proroga Contratto" OnClientClick="ConfermaProroga();" />
                                <img id="exit" alt="Esci" src="../../NuoveImm/Img_EsciCorto.png" title="Esci" style="cursor: pointer"
                                    onclick="CloseModal(document.getElementById('dataProrogata').value)" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="confermaProroga" runat="server" Value="0" />
    <asp:HiddenField ID="dataProrogata" runat="server" Value="0" />
    </form>
</body>
</html>
