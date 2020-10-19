<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Integrazione.ascx.vb" Inherits="Dic_Integrazione" %>

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
mioval = mioval + str.substring(0, 51) + ';' + str1 + '!';
if (i == 9) { i = obj1.length - 1; }
}

window.showModalDialog("com_integ.aspx?OP=0&COMPONENTE=" + mioval,window,'status:no;dialogWidth:433px;dialogHeight:270px;dialogHide:true;help:no;scroll:no');
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
    mioval=mioval + str.substring(0,51)+ ';' + str1 + '!';
    }

    str=obj1.options[obj1.selectedIndex].text;
        
    importo=str.substring(51,59);
    nome=obj1.options[obj1.selectedIndex].value;
    window.showModalDialog("com_integ.aspx?OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&IM=" + importo,'','status:no;dialogWidth:433px;dialogHeight:270px;dialogHide:true;help:no;scroll:no');
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
if (i == 9) { i = obj1.length - 1; }
}

window.showModalDialog("com_detrazioni.aspx?OP=0&COMPONENTE=" + mioval,window,'status:no;dialogWidth:433px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
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
        
    importo=str.substring(67,75);
    nome=obj1.options[obj1.selectedIndex].value;
    tipo = str.substring(31, 66);


    window.showModalDialog("com_detrazioni.aspx?OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&IM=" + importo + "&TI=" + tipo,'','status:no;dialogWidth:433px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
}
}

// -->
</script>

<div id="int" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 220; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 315px;
    background-color: #ffffff">
    &nbsp;
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 4px; position: absolute;
        top: 4px" Width="338px">INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ISEE</asp:Label>
    <asp:ListBox ID="ListBox1" runat="server" Font-Names="Courier New" Font-Size="8pt"
        Height="94px" Style="z-index: 101; left: 6px; position: absolute; top: 61px"
        Width="586px"></asp:ListBox>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 102; left: 358px;
        position: absolute; top: 4px" Width="218px">Anno di riferimento della sit. economica</asp:Label>
    <asp:Label ID="lbldataMob" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="times"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 103; left: 579px; position: absolute;
        top: 5px" Text="2005"></asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 104; left: 6px;
        position: absolute; top: 162px" Width="77px">DETRAZIONI</asp:Label>
    <asp:Label ID="lblSocio" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 104; left: 83px;
        position: absolute; top: 161px" Visible="False" Width="542px">Le spese di Degenza  in case di riposo sono valide solo per componenti oltre i 65 anni e fino a 2.582 Euro</asp:Label>
    &nbsp;&nbsp;
    <asp:ListBox ID="ListBox2" runat="server" Font-Names="Courier New" Font-Size="8pt"
        Height="93px" Style="z-index: 105; left: 6px; position: absolute; top: 197px"
        Width="586px"></asp:ListBox>
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 106; left: 10px;
        position: absolute; top: 47px" Width="72px">COMPONENTE</asp:Label>
    &nbsp;&nbsp;
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 107; left: 390px;
        position: absolute; top: 47px" Width="55px">IMPORTO</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 108; left: 10px;
        position: absolute; top: 181px" Width="72px">COMPONENTE</asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 109; left: 225px;
        position: absolute; top: 181px" Width="111px">TIPO DETRAZIONE</asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 110; left: 507px;
        position: absolute; top: 181px" Width="32px">IMPORTO</asp:Label>
    <asp:Button ID="btnAgg1" runat="server" Style="z-index: 111; left: 598px; position: absolute;
        top: 61px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" />
    <asp:Button ID="btnModifica" runat="server" Style="z-index: 112; left: 598px; position: absolute;
        top: 90px" Text="/" Width="38px" ToolTip="Modifica Elemento" />
    <asp:Button ID="btnElimina" runat="server" Style="z-index: 113; left: 598px; position: absolute;
        top: 118px" Text="-" Width="38px" ToolTip="Elimina Elemento" />
    <asp:Button ID="btnAggiungi" runat="server" Style="z-index: 114; left: 598px; position: absolute;
        top: 198px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" />
    <asp:Button ID="btnMod" runat="server" Style="z-index: 115; left: 598px; position: absolute;
        top: 225px" Text="/" Width="38px" ToolTip="Modifica Elemento" />
    <asp:Button ID="btnCanc1" runat="server" Style="z-index: 116; left: 598px; position: absolute;
        top: 252px" Text="-" Width="38px" ToolTip="Elimina Elemento" UseSubmitBehavior="False" />
    &nbsp;&nbsp;
    <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 117; left: 7px; position: absolute;
        top: 293px" Width="64px">Milano, lì</asp:Label>
    <asp:TextBox ID="txtData1" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 118; left: 70px; position: absolute; top: 292px" TabIndex="1"
        Width="68px"></asp:TextBox>
    <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 119; left: 7px;
        position: absolute; top: 28px" Width="300px">ALTRI REDDITI</asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QI" Style="z-index: 121;
        left: 620px; position: absolute; top: 2px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
</div>
<asp:HiddenField ID="V1" runat="server" />
<asp:HiddenField ID="V2" runat="server" />

