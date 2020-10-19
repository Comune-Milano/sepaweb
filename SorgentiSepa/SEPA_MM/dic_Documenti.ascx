<%@ Control Language="VB" AutoEventWireup="false" CodeFile="dic_Documenti.ascx.vb" Inherits="dic_Documenti" %>
<script type="text/javascript" src="Funzioni.js"></script>
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
        obj1 = document.getElementById('Dic_Nucleo1_ListBox1');
        for (i = 0; i <= obj1.length - 1; i++) {
            str = obj1.options[i].text;
            str1 = obj1.options[i].value;
            mioval = mioval + str.substring(0, 51) + ';' + str1 + '!';
            if (i == 7) { i = obj1.length - 1; }
        }

        window.showModalDialog("com_documenti.aspx?OP=0&COMPONENTE=" + mioval, window, 'status:no;dialogWidth:580px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
    }

    

    
// -->
</script>

<div id="doc" 
    style="border: 1px solid lightsteelblue; z-index: 200; left: 10px; width: 641px;
    position: absolute; top: 106px; height: 400px; background-color: #ffffff;" >
    &nbsp;
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 4px; position: absolute;
        top: 4px" Width="519px">DOCUMENTAZIONE MANCANTE</asp:Label>
        <div id="cont" 
        
        style="position: absolute; width: 578px; height: 351px; top: 38px; left: 8px; overflow: auto;">
    <asp:ListBox ID="ListBox1" runat="server" Style="z-index: 101; left: 0px;
        position: absolute; top: 0px; height: 339px; width: 984px;" 
        Font-Names="Courier New" Font-Size="8pt"></asp:ListBox></div>
    <asp:Button ID="Button1" runat="server" Style="z-index: 117; left: 598px; position: absolute;
        top: 38px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" />
    <asp:Button ID="Button2" runat="server" Style="z-index: 119; left: 598px; position: absolute;
        top: 74px" Text="-" Width="38px" ToolTip="Elimina Elemento" />
    &nbsp;
    </div>
&nbsp; &nbsp;&nbsp;
<asp:HiddenField ID="V2" runat="server" />
