<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IntroInserimento.aspx.vb"
    Inherits="SATISFACTION_IntroInserimento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Questionario di rilevazione</title>
    <style type="text/css">
        .stile_tabella
        {
            width: 100%;
            margin-top: 10%;
        }
        
        .pulsante
        {
            margin-left: 70%;
            margin-top: 5%;
        }
    </style>
    <script type="text/javascript">

        // Funzione javascript per l'inserimento in automatico degli slash nella data
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

        function apriRicerca() {

            var win = null;
            LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
            LeftPosition = LeftPosition - 20;
            TopPosition = TopPosition - 20;
            window.open('RicercaUI.aspx', 'RicercaUI', 'height=450,top=0,left=0,width=670,scrollbars=no');
        }

    </script>
</head>
<body background="../NuoveImm/SfondoMascheraContratti.jpg">
    <form id="form1" runat="server">
    <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:Label ID="Label33" runat="server" Text="Inserimento Scheda Questionario"></asp:Label>
    </span>
    <table class="stile_tabella">
        <tr>
            <td>
                <asp:Label ID="lbl_data" runat="server" Text="Inserire la data di compilazione" Font-Size="10pt"
                    Font-Bold="True" Font-Names="Arial"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txt_data" runat="server" Width="90px" TabIndex="1"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label34" runat="server" Font-Names="Arial" Font-Size="8pt" Text="(rilevabile dal questionario)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_UI" runat="server" Text="Inserire il codice dell'unità immobiliare"
                    Font-Size="10pt" Font-Bold="True" Font-Names="Arial"></asp:Label>
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_codui" runat="server" TabIndex="2"></asp:TextBox>
                &nbsp&nbsp
                <asp:Image ID="Image1" runat="server" onclick="apriRicerca()" TabIndex="3" ImageUrl="../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png"
                    Style="cursor: pointer" />
            </td>
        </tr>
    </table>
    <table>
       <tr>
       <td><br /><br /><br /><br /><br /><br /><br /><br /></td>
       </tr>
            <td>
                <asp:Label ID="lblMess" runat="server" Font-Bold="False" Font-Names="Arial" 
                    Font-Size="9pt" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <div class="pulsante">
        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../NuoveImm/Img_Procedi.png"
            Style="z-index: 101; left: 527px; top: 516px" TabIndex="4" ToolTip="Procedere con l'inserimento dei dati" />
    </div>
    </form>
</body>
</html>
