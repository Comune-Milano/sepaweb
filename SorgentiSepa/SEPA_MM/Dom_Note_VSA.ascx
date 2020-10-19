<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Note_VSA.ascx.vb"
    Inherits="Dom_Note_VSA" %>
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
            window.showModalDialog("com_documenti.aspx?OP=0&COMPONENTE=" + mioval + "&TDOM=" + document.getElementById('tipoRichiesta').value, window, 'status:no;dialogWidth:580px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');

        }
        else {
            window.showModalDialog("com_documenti.aspx?OP=0&COMPONENTE=" + mioval + "&DIC=" + document.getElementById('Dom_Note1_idDic').value + "&TDOM=" + document.getElementById('tipoRichiesta').value, window, 'status:no;dialogWidth:580px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
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
<div id="not" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 200; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 390px;
    background-color: #ffffff">
    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="4000"
        Style="z-index: 100; left: 10px; position: absolute; top: 26px; height: 45px;
        width: 621px;" TextMode="MultiLine"></asp:TextBox>
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 12px; position: absolute;
        top: 8px" Width="519px">NOTE GENERALI</asp:Label>
    <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 12px; position: absolute;
        top: 109px; width: 220px;">DOCUMENTAZIONE MANCANTE</asp:Label>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 12px; position: absolute;
        top: 281px; width: 220px;">NOTE SOPRALLUOGO</asp:Label>
    <asp:CheckBox ID="chkDocManc" runat="server" Font-Names="arial" Font-Size="10pt"
        ForeColor="Black" Style="z-index: 100; left: 216px; position: absolute; top: 106px;
        width: 177px;" Text="Doc. mancante presentata" Font-Bold="False" 
        Enabled="False" />
    <asp:CheckBox ID="chkSosp" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black"
        Style="z-index: 100; left: 218px; position: absolute; top: 231px; width: 126px;"
        Text="Fine sospensione" Font-Bold="False" Enabled="False" 
        BorderColor="#9999FF" BorderStyle="Solid" BorderWidth="2px" Visible="False" />
    <asp:CheckBox ID="chkSoprall" runat="server" Font-Names="arial" Font-Size="10pt"
        ForeColor="Black" Style="z-index: 100; left: 220px; position: absolute; top: 280px;
        width: 233px;" Text="Richiesta sopralluogo" Font-Bold="False" />
    <asp:Label ID="lblDataManc" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 466px; position: absolute;
        top: 109px; width: 50px;" Text="DATA"></asp:Label>
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 468px; position: absolute;
        top: 282px; width: 50px;" Text="DATA"></asp:Label>
    <asp:TextBox ID="txtDataDocManc" runat="server" Style="z-index: 100; left: 508px;
        position: absolute; top: 105px; width: 70px;" MaxLength="10" Enabled="False"></asp:TextBox>
    <asp:TextBox ID="txtDataSoprall" runat="server" Style="z-index: 100; left: 511px;
        position: absolute; top: 278px; width: 70px;" MaxLength="10" ToolTip="Inserire la data del sopralluogo"></asp:TextBox>
    <asp:TextBox ID="txtNoteSoprall" runat="server" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 9px; position: absolute; top: 306px; height: 45px;
        width: 621px;" TextMode="MultiLine"></asp:TextBox>
    <div style="overflow: auto; position: absolute; width: 580px; height: 116px; top: 111px;
        left: 6px;" id="docPresentata">
        <asp:ListBox ID="ListBox1" runat="server" Style="z-index: 106; left: 4px; position: absolute;
            top: 21px; width: 750px; height: 77px;" Font-Names="Courier New" 
            Font-Size="8pt">
        </asp:ListBox>
    </div>
    <asp:Button ID="Button1" runat="server" Style="z-index: 117; left: 592px; position: absolute;
        top: 133px; bottom: 231px; height: 26px;" Text="+" Width="38px" ToolTip="Aggiungi Elemento" />
    <asp:Button ID="Button2" runat="server" Style="z-index: 119; left: 592px; position: absolute;
        top: 164px" Text="-" Width="38px" ToolTip="Elimina Elemento" />
</div>
