<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Nucleo_VSA.ascx.vb"
    Inherits="Dic_Nucleo_VSA" %>
<script type="text/javascript" src="Funzioni.js"></script>
<script language="javascript" type="text/javascript">
<!--


    function MyDialogArguments() {
        this.Sender = null;
        this.StringValue = "";
    }

    function AggiungiNucleo() {
        a = document.getElementById('Dic_Nucleo1_txtprogr').value;
        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = a;
        dialogArgs.Sender = window;
        window.showModalDialog("com_nucleo.aspx?OP=0&IDDICH=" + document.getElementById('Dic_Nucleo1_iddich').value + "&PR=" + a, window, 'status:no;dialogWidth:433px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
    }

    function ModificaNucleo() {
        a = document.getElementById('Dic_Nucleo1_txtprogr').value;
        obj1 = document.getElementById('Dic_Nucleo1_ListBox1');
        if (obj1.selectedIndex == -1) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            str = obj1.options[obj1.selectedIndex].text;

            cognome = str.substring(0, 25);
            nome = str.substring(26, 51);
            data = str.substring(52, 62);
            cf = str.substring(63, 79);
            parenti = str.substring(80, 105);
            inv = str.substring(106, 112);
            asl = str.substring(113, 118);
            acc = str.substring(119, 121);

            //variabili da passare per NUOVO COMPONENTE
            ncomp = str.substring(130, 132);
            dataingr = str.substring(143, 153);

            window.showModalDialog("com_nucleo.aspx?OP=1&IDDICH=" + document.getElementById('Dic_Nucleo1_iddich').value + "&RI=" + obj1.selectedIndex + "&COGNOME=" + cognome + "&NOME=" + nome + "&DATA=" + data + "&CF=" + cf + "&PARENTI=" + parenti + "&INV=" + inv + "&ASL=" + asl + "&ACC=" + acc + "&TESTO=" + obj1.options[obj1.selectedIndex].text + "&PR=" + a + "&NCOMP=" + ncomp + "&DATAINGR=" + dataingr, '', 'status:no;dialogWidth:433px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
        }
    }

    function ModificaSpese() {

        obj1 = document.getElementById('Dic_Nucleo1_ListBox2');
        if (obj1.selectedIndex == -1) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            str = obj1.options[obj1.selectedIndex].text;
            componente = str.substring(0, 51);
            importo = str.substring(53, 59);
            descrizione = str.substring(63, 80);
            window.showModalDialog("com_spese.aspx?RI=" + obj1.selectedIndex + "&CM=" + componente + "&IM=" + importo + "&DS=" + descrizione, '', 'status:no;dialogWidth:433px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
        }
    }

    function EliminaSoggetto() {
        obj1 = document.getElementById('Dic_Nucleo1_ListBox1');
        if (obj1.selectedIndex == -1) {
            alert('Selezionare una riga dalla lista!');
        }
        else {

            str = obj1.options[obj1.selectedIndex].text;

            cognome = str.substring(0, 25);
            nome = str.substring(26, 51);
            data = str.substring(52, 62);
            cf = str.substring(63, 79);

            window.showModalDialog("com_uscita.aspx?OP=1&RI=" + obj1.selectedIndex + "&COGNOME=" + cognome + "&NOME=" + nome + "&DATA=" + data + "&CF=" + cf, '', 'status:no;dialogWidth:470px;dialogHeight:340px;dialogHide:true;help:no;scroll:no');
        }

    }

// -->
</script>
<div id="nuc" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 190; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 315px;
    background-color: #ffffff; background-image: none;">
    &nbsp;&nbsp;
    <p>
        <asp:HyperLink ID="HyperLink1111" runat="server" Font-Names="arial" Font-Size="8pt"
            ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QB" Style="z-index: 115;
            left: 620px; position: absolute; top: 2px" Target="_blank" Width="17px">Aiuto</asp:HyperLink>
        &nbsp;</p>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="Button1" runat="server" Style="left: 591px; position: absolute; top: 141px;
        z-index: 101;" Text="+" Width="38px" TabIndex="19" ToolTip="Aggiungi Elemento"
        Visible="False" />
    <p>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 4px; position: absolute;
            top: 4px" Width="575px">QUADRO B:COMPONENTI DEL NUCLEO-Richiedente, componenti e altri soggetti considerati a carico IRPEF</asp:Label>
        <asp:Button ID="Button3" runat="server" Style="left: 593px; position: absolute; top: 120px;
            z-index: 103;" Text="/" Width="38px" TabIndex="20" ToolTip="Modifica Elemento"
            Visible="False" />
        <asp:Button ID="Button2" runat="server" Style="left: 591px; position: absolute; top: 85px;
            z-index: 104;" Text="-" Width="38px" TabIndex="22" ToolTip="Elimina Elemento" />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    </p>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <p style="z-index: 114; left: 0px; position: absolute; top: 0px">
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="left: 4px; position: absolute; top: 182px"
            Width="633px">SPESE DOCUMENTATE SUPERIORI A 10.000 Euro (solo componenti con indennità accompagnamento)</asp:Label>
        &nbsp; &nbsp;
    </p>
    <div style="border-style: solid; border-color: inherit; border-width: 1px; left: 7px;
        overflow: auto; width: 573px; position: absolute; top: 32px; height: 120px; z-index: 113;">
        <asp:ListBox ID="ListBox1" runat="server" Font-Names="Courier New" Font-Size="8pt"
            Style="border-style: none; left: 0px; position: absolute; top: 18px; z-index: 124;
            height: 85px;" Width="1120px" Rows="7"></asp:ListBox>
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
        <asp:Label ID="Label7" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 106; left: 564px; position: absolute; top: 0px" Text="GR. PARENTELA"
            Width="96px"></asp:Label>
        <asp:Label ID="Label8" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 107; left: 745px; position: absolute; top: 0px" Text="% INV."
            Width="51px"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 108; left: 795px; position: absolute; top: 0px" Text="ASL" Width="31px"></asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 109; left: 836px; position: absolute; top: 0px" Text="IND. ACC."
            Width="80px"></asp:Label>
        <asp:Label ID="Label14" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 109; left: 910px; position: absolute; top: 0px" Text="NUOVO COMP."
            Width="100px"></asp:Label>
        <asp:Label ID="Label15" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
            Style="z-index: 109; left: 1000px; position: absolute; top: 0px" Text="DATA INGR.NUCLEO"
            Width="150px"></asp:Label>
        &nbsp;
    </div>
    <asp:TextBox ID="txtprogr" runat="server" Style="left: 518px; position: absolute;
        top: 108px; z-index: 105;" Width="23px" Height="8px"></asp:TextBox>
    &nbsp;
    <asp:ListBox ID="ListBox2" runat="server" Height="75px" Style="left: 6px; position: absolute;
        top: 233px; z-index: 106;" Width="572px" Font-Names="Courier New" Font-Size="8pt">
    </asp:ListBox>
    <asp:Button ID="Button5" runat="server" Style="left: 590px; position: absolute; top: 232px;
        z-index: 107;" Text="/" Width="38px" ToolTip="Modifica Elemento" />
    <asp:Label ID="Label11" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
        Style="z-index: 108; left: 12px; position: absolute; top: 218px" Text="COMPONENTE"
        Width="51px"></asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
        Style="z-index: 109; left: 380px; position: absolute; top: 218px" Text="IMPORTO"
        Width="51px"></asp:Label>
    <asp:Label ID="Label13" runat="server" Font-Names="Courier New" Font-Size="8pt" ForeColor="#0000C0"
        Style="z-index: 110; left: 455px; position: absolute; top: 218px" Text="DESCRIZIONE"
        Width="51px"></asp:Label>
    <asp:Label ID="lblEliminati" runat="server" CssClass="CssLabel" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" Height="18px" Style="z-index: 119;
        left: 9px; position: absolute; top: 163px"></asp:Label>
    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    &nbsp;&nbsp; &nbsp; &nbsp;
    <asp:Button ID="Button4" runat="server" Style="z-index: 111; left: 591px; position: absolute;
        top: 33px" Text="+" Width="38px" ToolTip="Inserimento componente" UseSubmitBehavior="False" />
    &nbsp;
    <asp:Button ID="Button6" runat="server" Style="z-index: 112; left: 591px; position: absolute;
        top: 59px" Text="/" Width="38px" ToolTip="Modifica Componente" UseSubmitBehavior="False" />
    <asp:HiddenField ID="NuovoCompon" runat="server" Value="0" />
    <asp:HiddenField ID="MotivoUscita" runat="server" />
    <asp:HiddenField ID="DataUscita" runat="server" />
    <asp:HiddenField ID="txtID" runat="server" />
    <asp:HiddenField ID="iddich" runat="server" />
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
</div>
<script type="text/javascript">
    document.getElementById('Dic_Nucleo1_txtprogr').style.visibility = 'hidden';
    //document.getElementById('Dic_Nucleo1_txtprova').style.visibility='hidden';
    //document.getElementById('comp').style.visibility='hidden';

    
</script>
