<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Ospiti.ascx.vb" Inherits="Dom_Ospiti" %>
<script type="text/javascript" src="Funzioni.js"></script>
<script language="javascript" type="text/javascript">
<!--


    function MyDialogArguments() {
        this.Sender = null;
        this.StringValue = "";
    }


    function AggiungiNucleo() {
        a = document.getElementById('Dom_Ospiti1_txtprogr').value;
        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = a;
        dialogArgs.Sender = window;
        window.showModalDialog("com_ospiti.aspx?OP=0&IDDOM=" + document.getElementById('Dom_Ospiti1_iddom').value + "&PR=" + a, window, 'status:no;dialogWidth:433px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
    }

    function ModificaNucleo() {
        a = document.getElementById('Dom_Ospiti1_txtprogr').value;
        obj1 = document.getElementById('Dom_Ospiti1_ListBox1');
        if (obj1.selectedIndex == -1) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            str = obj1.options[obj1.selectedIndex].text;

            cognome = str.substring(0, 25);
            nome = str.substring(26, 51);
            data = str.substring(52, 62);
            cf = str.substring(63, 79);
            dataingr = str.substring(85, 95);
            datafine = str.substring(102, 112);

            window.showModalDialog("com_ospiti.aspx?OP=1&IDDOM=" + document.getElementById('Dom_Ospiti1_iddom').value + "&ID=" + obj1.value + "&RI=" + obj1.selectedIndex + "&COGNOME=" + cognome + "&NOME=" + nome + "&DATA=" + data + "&CF=" + cf + "&TESTO=" + obj1.options[obj1.selectedIndex].text + "&PR=" + a + "&DATAINGR=" + dataingr + "&DATAFINE=" + datafine, '', 'status:no;dialogWidth:433px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
        }
    }

    
    
// -->
</script>
<asp:HiddenField ID="V1" runat="server" />
<asp:HiddenField ID="iddom" runat="server" />
<asp:HiddenField ID="txtidTipoVIA" runat="server" />
<asp:HiddenField ID="txtVIA" runat="server" />
<asp:HiddenField ID="txtCIVICO" runat="server" />
<asp:HiddenField ID="txtCOMUNE" runat="server" />
<asp:HiddenField ID="txtCAP" runat="server" />
<asp:HiddenField ID="txtDOCIDENT" runat="server" />
<asp:HiddenField ID="txtDATADOC" runat="server" />
<asp:HiddenField ID="txtRILASCIO" runat="server" />
<asp:HiddenField ID="txtSOGGIORNO" runat="server" />
<asp:HiddenField ID="txtDATASogg" runat="server" />
<asp:HiddenField ID="txtREFERENTE" runat="server" />
<asp:HiddenField ID="txtIDospite" runat="server" />
<div id="osp" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 200; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 390px;
    background-color: #ffffff; background-image: none;">
    &nbsp;&nbsp;
    <p>
        <asp:HyperLink ID="HyperLink1111" runat="server" Font-Names="arial" Font-Size="8pt"
            ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QB" Style="z-index: 115;
            left: 620px; position: absolute; top: 2px" Target="_blank" Width="17px">Aiuto</asp:HyperLink>
        &nbsp;</p>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <p>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 4px; position: absolute;
            top: 4px" Width="575px">Elenco OSPITI: persone non appartenenti al nucleo familiare originario</asp:Label>
        <asp:Button ID="Button2" runat="server" Style="left: 591px; position: absolute; top: 85px;
            z-index: 104;" Text="-" Width="38px" TabIndex="22" ToolTip="Elimina Elemento" />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    </p>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <p style="z-index: 114; left: 0px; position: absolute; top: 0px">
        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
    </p>
    <div style="border-right: 1px solid; border-top: 1px solid; left: 7px; overflow: auto;
        border-left: 1px solid; width: 573px; border-bottom: 1px solid; position: absolute;
        top: 32px; height: 149px; z-index: 113;">
        <asp:ListBox ID="ListBox1" runat="server" Font-Names="Courier New" Font-Size="8pt"
            Height="107px" Style="left: 0px; position: absolute; top: 18px; border-top-style: none;
            border-right-style: none; border-left-style: none; border-bottom-style: none;
            z-index: 124;" Width="800px" Rows="7"></asp:ListBox>
        <asp:Label ID="Label1" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 101; left: 5px; position: absolute; top: 0px" Text="COGNOME"
            Width="51px"></asp:Label>
        &nbsp;
        <asp:Label ID="Label4" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 103; left: 187px; position: absolute; top: 0px" Text="NOME" Width="51px"></asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 104; left: 369px; position: absolute; top: 0px" Text="DATA NAS."
            Width="63px"></asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 105; left: 446px; position: absolute; top: 0px" Text="COD. FISCALE"
            Width="89px"></asp:Label>
        <asp:Label ID="Label15" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 109; left: 600px; position: absolute; top: 0px" Text="DATA INIZIO"
            Width="150px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 109; left: 720px; position: absolute; top: 0px" Text="DATA FINE"
            Width="150px"></asp:Label>
        &nbsp;
    </div>
    <asp:TextBox ID="txtprogr" runat="server" Style="left: 518px; position: absolute;
        top: 108px; z-index: 105;" Width="23px" Height="8px"></asp:TextBox>
    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    &nbsp;&nbsp; &nbsp; &nbsp;
    <asp:Button ID="Button4" runat="server" Style="z-index: 111; left: 591px; position: absolute;
        top: 33px" Text="+" Width="38px" ToolTip="Inserimento componente" UseSubmitBehavior="False" />
    &nbsp;
    <asp:Button ID="Button6" runat="server" Style="z-index: 112; left: 591px; position: absolute;
        top: 59px" Text="/" Width="38px" ToolTip="Modifica Componente" UseSubmitBehavior="False" />
</div>
<script type="text/javascript">
    document.getElementById('Dom_Ospiti1_txtprogr').style.visibility = 'hidden';
    //document.getElementById('Dic_Nucleo1_txtprova').style.visibility='hidden';
    //document.getElementById('comp').style.visibility='hidden';


</script>
