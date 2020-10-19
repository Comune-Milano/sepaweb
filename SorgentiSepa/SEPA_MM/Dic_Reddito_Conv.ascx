<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Reddito_Conv.ascx.vb" Inherits="Dich_Reddito_Conv" %>

<script language=javascript SRC=Funzioni.js></script>
<script language="javascript" type="text/javascript">
<!--

function MyDialogArguments1()
{
    this.Sender = null;
    this.StringValue = "";
}


function AggiungiRedditoConv() {

dialogArgs = new MyDialogArguments1();
dialogArgs.StringValue = '';
dialogArgs.Sender = window;

mioval='';
obj1=document.getElementById('Dic_Nucleo1_ListBox1');
for (i=0;i<=obj1.length-1;i++)
{   
str=obj1.options[i].text;
str1=obj1.options[i].value;
mioval = mioval + str.substring(0, 51) + ';' + str1 + '!';
if (i == 9) { i = obj1.length - 1; }
}

window.showModalDialog("com_convenzionale.aspx?OP=0&COMPONENTE=" + mioval,window,'status:no;dialogWidth:433px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
}

function ModificaRedditoConv() {
obj1=document.getElementById('Dic_Reddito_Conv1_ListBox1');
if (obj1.selectedIndex==-1)
    {  
     alert('Selezionare una riga dalla lista!');
    }
else
{
    mioval='';
    obj2=document.getElementById('Dic_Nucleo1_ListBox1');
    for (i=0;i<=obj2.length-1;i++)
    {   
    str=obj2.options[i].text;
    str1=obj2.options[i].value;
    mioval=mioval + str.substring(0,51)+ ';' + str1 + '!';
    }

    str=obj1.options[obj1.selectedIndex].text;
        
    v1=str.substring(36,41);
    
    v2=str.substring(43,48);
    
    v3=str.substring(50,57);
    
    v4=str.substring(61,68);
    
    v5=str.substring(72,79);
    
    v6=str.substring(83,90);
    
    v7=str.substring(94,101);
    
    v8=str.substring(105,112);
    
    v9=str.substring(116,123);

    nome=obj1.options[obj1.selectedIndex].value;
    window.showModalDialog("com_convenzionale.aspx?OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&1=" + v1 + "&2=" + v2 + "&3=" + v3 + "&4=" + v4 + "&5=" + v5 + "&6=" + v6 + "&7=" + v7 + "&8=" + v8 + "&9=" + v9,'','status:no;dialogWidth:433px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
}
}
// -->
</script>

<div id="redC" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 220; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 315px;
    background-color: #ffffff">
    &nbsp;
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 4px; position: absolute;
        top: 4px" Width="377px">REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA 2006</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnAgg1" runat="server" Style="z-index: 101; left: 598px; position: absolute;
        top: 73px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" />
    <asp:Button ID="btnModifica" runat="server" Style="z-index: 102; left: 598px; position: absolute;
        top: 102px" Text="/" Width="38px" ToolTip="Modifica Elemento" />
    <asp:Button ID="btnElimina" runat="server" Style="z-index: 103; left: 598px; position: absolute;
        top: 130px" Text="-" Width="38px" ToolTip="Elimina Elemento" />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QC" Style="z-index: 126;
        left: 620px; position: absolute; top: 2px" Target="_blank" Width="18px">Aiuto</asp:HyperLink>
    <div 
        style="border-style: solid; border-color: inherit; border-width: 1px; z-index: 107; left: 6px;
        overflow: auto; width: 581px; position: absolute; top: 74px; height: 149px">
        <asp:ListBox ID="ListBox1" runat="server" Font-Names="Courier New" Font-Size="8pt"
            Height="107px" Rows="7" Style="z-index: 100; left: 1px; border-top-style: none;
            border-right-style: none; border-left-style: none; position: absolute; top: 18px;
            border-bottom-style: none" Width="899px"></asp:ListBox>
            
        &nbsp; &nbsp;
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 1px;
        position: absolute; top: 1px" Width="72px">COMPONENTE</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 102; left: 523px;
        position: absolute; top: 1px" Width="55px">AUTONOMO</asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 103; left: 357px;
        position: absolute; top: 1px" Width="55px">DIPENDENTE</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 104; left: 744px;
        position: absolute; top: 1px" Width="55px">DOM/AG/FAB</asp:Label>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Courier New"
            Font-Size="8pt" ForeColor="#0000C0" Height="7px" Style="z-index: 106; left: 240px;
            position: absolute; top: 1px" Width="46px">C.PROF.</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Courier New"
            Font-Size="8pt" ForeColor="#0000C0" Height="7px" Style="z-index: 107; left: 315px;
            position: absolute; top: 1px" Width="39px">PROF.</asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Courier New"
            Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 108; left: 446px;
            position: absolute; top: 1px" Width="55px">PENSIONE</asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Courier New"
            Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 109; left: 587px;
            position: absolute; top: 1px" Width="71px">NON IMPON.</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Courier New"
            Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 111; left: 662px;
            position: absolute; top: 1px" Width="55px">OCCASIONALI</asp:Label>
    </div>
    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="7px" Style="z-index: 104; left: 7px;
        position: absolute; top: 43px" Width="519px">Numero di FIGLI/MINORI fiscalmente a carico presenti nel nucleo familiare</asp:Label>
    <asp:TextBox ID="txtMinori" runat="server" Font-Names="times" Font-Size="8pt" Height="18px"
        Style="z-index: 105; left: 532px; position: absolute; top: 42px" Width="38px">0</asp:TextBox>
    &nbsp; &nbsp;
    <input id="btnCalcola" Onclick="CalcolaReddito()" style="z-index: 108; left: 385px; width: 208px; position: absolute;
        top: 234px; visibility: hidden;" type="button" value="Stampa Report" />
</div>
<asp:HiddenField ID="V1" runat="server" />

