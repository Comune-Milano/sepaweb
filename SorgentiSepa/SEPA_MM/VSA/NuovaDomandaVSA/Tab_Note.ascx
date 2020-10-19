<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Note.ascx.vb" Inherits="VSA_NuovaDomandaVSA_Tab_Note" %>
<script type="text/javascript">
<!--

    function MyDialogArguments2() {
        this.Sender = null;
        this.StringValue = "";
    }


    function AggiungiDocumento() {
        dialogArgs = new MyDialogArguments2();
        dialogArgs.StringValue = '';
        dialogArgs.Sender = window;

        mioval = '';
        if (document.getElementById('Dic_Nucleo1_ListBox1') != null) {
            obj1 = document.getElementById('Dic_Nucleo1_ListBox1');
            for (i = 0; i <= obj1.length - 1; i++) {
                str = obj1.options[i].text;
                str1 = obj1.options[i].value;
                mioval = mioval + str.substring(0, 51) + ';' + str1 + '!';
                if (i == 7) { i = obj1.length - 1; }
            }
            window.showModalDialog("com_documenti.aspx?OP=0&COMPONENTE=" + mioval + "&TDOM=" + document.getElementById('tipoRichiesta').value, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');

        }
        else {
            window.showModalDialog("com_documenti.aspx?OP=0&COMPONENTE=" + mioval + "&DIC=" + document.getElementById('Dom_Note1_idDic').value + "&TDOM=" + document.getElementById('tipoRichiesta').value, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');
        }
    }


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

    
// -->
</script>
<asp:HiddenField ID="V2" runat="server" />
<asp:HiddenField ID="idDic" runat="server" Value="0" />
<asp:HiddenField ID="documMancante" runat="server" Value="0" />
<table style="width: 97%;">
    <tr>
        <td colspan="3" style="padding-left: 15px; text-align: center;">
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Height="18px" Width="119px">NOTE GENERALI</asp:Label>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;" colspan="3">
            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="4000" Style="height: 80px; width: 97%;" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>
<table width="97%">
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Height="18px" Style="width: 190px; float: left;">DOCUMENTAZIONE MANCANTE</asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chkDocManc" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black" Style="width: 177px;" Text="Doc. mancante presentata" Font-Bold="False" Enabled="False" />
        </td>
        <td style="padding-left: 15px; width: 120px; text-align: center">
            <asp:Label ID="lblDataManc" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Height="18px" Style="width: 50px;" Text="DATA"></asp:Label>
        </td>
        <td style="width: 250px;">
            <asp:TextBox ID="txtDataDocManc" runat="server" Style="width: 90px;" MaxLength="10" Enabled="False"></asp:TextBox>
        </td>
    </tr>
</table>
<table width="97%" style="padding-left: 15px;">
    <tr>
        <td>
            <table style="margin-left: 10px; width: 100%; height: 20px;">
                <tr>
                    <td style="border: 1px solid #0066FF; vertical-align: top; width: 97%;">
                        <div style="overflow-x: hidden; overflow-y: auto; width: 100%; height: 70px;">
                            <asp:ListBox ID="ListBox1" runat="server" Style="width: 97%; height: 77px;" Font-Names="Courier New" Font-Size="9pt"></asp:ListBox>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Elemento" Style="width: 16px; cursor: pointer;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="Button2" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png" ToolTip="Elimina Elemento" Style="width: 16px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table style="width: 97%;">
    <tr>
        <td colspan="3" style="padding-left: 15px; text-align: center;">
            <asp:CheckBox ID="chkSosp" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black" Style="width: 126px;" Text="Fine sospensione" Font-Bold="False" Enabled="False" BorderColor="#9999FF" BorderStyle="Solid" BorderWidth="2px" Visible="False" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table width="97%">
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Height="18px" Style="width: 220px;">NOTE SOPRALLUOGO</asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chkSoprall" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black" Style="width: 233px;" Text="Richiesta sopralluogo" Font-Bold="False" />
        </td>
        <td style="padding-left: 15px; width: 120px; text-align: center">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Height="18px" Style="width: 50px;" Text="DATA"></asp:Label>
        </td>
        <td style="width: 250px;">
            <asp:TextBox ID="txtDataSoprall" runat="server" Style="width: 90px;" MaxLength="10" ToolTip="Inserire la data del sopralluogo"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;" colspan="4">
            <asp:TextBox ID="txtNoteSoprall" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="4000" Style="height: 80px; width: 97%;" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
</table>
