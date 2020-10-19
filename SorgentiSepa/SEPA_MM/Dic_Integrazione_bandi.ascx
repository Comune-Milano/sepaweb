<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Integrazione_bandi.ascx.vb" Inherits="Dic_Integrazione" %>

<script language=javascript SRC=Funzioni.js></script>
<script language="javascript" type="text/javascript">
<!--

function MyDialogArguments3()
{
    this.Sender = null;
    this.StringValue = "";
}


function AggiungiInt() {

dialogArgs = new MyDialogArguments3();
dialogArgs.StringValue = '';
dialogArgs.Sender = window;

mioval='';
obj1=document.getElementById('Dic_Nucleo1_ListBox1');
for (i=0;i<=obj1.length-1;i++)
{   
str=obj1.options[i].text;
str1=obj1.options[i].value;
mioval = mioval + str.substring(0, 46) + ';' + str1 + '!';
if (i == 7) { i = obj1.length - 1; }
}

window.showModalDialog("com_integ_bandi.aspx?OP=0&COMPONENTE=" + mioval,window,'status:no;dialogWidth:450px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
}

function ModificaInt() {
obj1=document.getElementById('Dic_Integrazione1_ListBox1');
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
    mioval=mioval + str.substring(0,46)+ ';' + str1 + '!';
    }

    str=obj1.options[obj1.selectedIndex].text;

    importo = str.substring(71, 79);
    tipocont = str.substring(46, 70);

    nome=obj1.options[obj1.selectedIndex].value;
    window.showModalDialog("com_integ_bandi.aspx?OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&IM=" + importo + "&CONT=" + tipocont ,'','status:no;dialogWidth:450px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
}
}

function AggiungiDet() {

dialogArgs = new MyDialogArguments3();
dialogArgs.StringValue = '';
dialogArgs.Sender = window;

mioval='';
obj1=document.getElementById('Dic_Nucleo1_ListBox1');
for (i=0;i<=obj1.length-1;i++)
{   
str=obj1.options[i].text;
str1=obj1.options[i].value;
mioval = mioval + str.substring(0, 51) + ';' + str1 + '!';
if (i == 7) { i = obj1.length - 1; }
}

window.showModalDialog("com_detrazioni_bandi.aspx?OP=0&COMPONENTE=" + mioval,window,'status:no;dialogWidth:433px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
}

function ModificaDet() {
obj1=document.getElementById('Dic_Integrazione1_ListBox2');
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
        
    importo=str.substring(83,91);
    nome = obj1.options[obj1.selectedIndex].value;

    tipo = str.substring(47, 82);
    tipodett = str.substring(31, 46);

    window.showModalDialog("com_detrazioni_bandi.aspx?OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&IM=" + importo + "&TI=" + tipo + "&TIPODET=" + tipodett, '', 'status:no;dialogWidth:433px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
}
}

// -->
</script>

<div id="int" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 220; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 400px;
    background-color: #ffffff">
    &nbsp;
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 4px; position: absolute;
        top: 4px" Width="338px">INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ISEE</asp:Label>
    <asp:ListBox ID="ListBox1" runat="server" Font-Names="Courier New" 
        Font-Size="8pt" Style="z-index: 101; left: 6px; position: absolute; top: 59px; height: 130px;"
        Width="586px"></asp:ListBox>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 102; left: 358px;
        position: absolute; top: 4px" Width="218px">Anno di riferimento della sit. economica</asp:Label>
    <asp:Label ID="lbldataMob" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="times"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 103; left: 579px; position: absolute;
        top: 5px" Text="2005"></asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 104; left: 6px;
        position: absolute; top: 196px" Width="77px">DETRAZIONI</asp:Label>
    <asp:Label ID="lblSocio" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 104; left: 83px;
        position: absolute; top: 195px" Visible="False" Width="542px">Le spese di Degenza  in case di riposo sono valide solo per componenti oltre i 65 anni e fino a 2.582 Euro</asp:Label>
    &nbsp;&nbsp;
    <div id="ContenitoreDet" 
        
        
        style="position: absolute; width: 579px; height: 149px; top: 219px; left: 9px; overflow: auto;">
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 108; left: 10px;
        position: absolute; top: 0px" Width="72px">COMPONENTE</asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 110; left: 591px;
        position: absolute; top: 0px; width: 52px;">IMPORTO</asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 110; left: 342px;
        position: absolute; top: 0px; width: 108px;">TIPO DETRAZIONE</asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 109; left: 226px;
        position: absolute; top: 0px" Width="111px">TIPO</asp:Label>
    <asp:ListBox ID="ListBox2" runat="server" Font-Names="Courier New" 
        Font-Size="8pt" 
            Style="z-index: 105; left: 6px; position: absolute; top: 15px; height: 117px; width: 929px;"></asp:ListBox>
    </div>
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 106; left: 10px;
        position: absolute; top: 45px" Width="72px">COMPONENTE</asp:Label>
    &nbsp;&nbsp;
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 107; left: 507px;
        position: absolute; top: 45px" Width="55px">IMPORTO</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 107; left: 352px;
        position: absolute; top: 45px; height: 12px; width: 131px;">CONTRIBUTI/SUSSIDI</asp:Label>
    &nbsp; &nbsp;
    
    <asp:Button ID="btnAgg1" runat="server" Style="z-index: 111; left: 598px; position: absolute;
        top: 61px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnModifica" runat="server" Style="z-index: 112; left: 598px; position: absolute;
        top: 91px" Text="/" Width="38px" ToolTip="Modifica Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnElimina" runat="server" Style="z-index: 113; left: 598px; position: absolute;
        top: 121px" Text="-" Width="38px" ToolTip="Elimina Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnAggiungi" runat="server" Style="z-index: 114; left: 598px; position: absolute;
        top: 234px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnMod" runat="server" Style="z-index: 115; left: 598px; position: absolute;
        top: 264px" Text="/" Width="38px" ToolTip="Modifica Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnCanc1" runat="server" Style="z-index: 116; left: 598px; position: absolute;
        top: 294px" Text="-" Width="38px" ToolTip="Elimina Elemento" 
        UseSubmitBehavior="False" 
        onclientclick="document.getElementById('H1').value='0';" />
    &nbsp;&nbsp;
    <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 117; left: 7px; position: absolute;
        top: 377px" Width="64px">Milano, lì</asp:Label>
    <asp:TextBox ID="txtData1" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 118; left: 70px; position: absolute; top: 373px" TabIndex="1"
        Width="68px"></asp:TextBox>
    <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 119; left: 7px;
        position: absolute; top: 26px" Width="300px">ALTRI REDDITI</asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QI" Style="z-index: 121;
        left: 620px; position: absolute; top: 2px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
</div>
<asp:HiddenField ID="V1" runat="server" />
<asp:HiddenField ID="V2" runat="server" />

