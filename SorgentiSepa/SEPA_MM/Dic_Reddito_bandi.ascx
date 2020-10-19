<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Reddito_bandi.ascx.vb" Inherits="Dic_Reddito" %>

<script type="text/javascript" src="funzioni.js"></script>
<script language="javascript" type="text/javascript">
<!--

function MyDialogArguments1()
{
    this.Sender = null;
    this.StringValue = "";
}


function AggiungiReddito() {

dialogArgs = new MyDialogArguments1();
dialogArgs.StringValue = '';
dialogArgs.Sender = window;

mioval = '';
mioval1 = '';

obj1=document.getElementById('Dic_Nucleo1_ListBox1');
for (i=0;i<=obj1.length-1;i++)
{
    str = obj1.options[i].text;
    str1=obj1.options[i].value;
    mioval = mioval + str.substring(0, 51) + ';' + str1 + '!';
    if (i == 7) { i = obj1.length - 1; }
}


window.showModalDialog('com_reddito_bandi.aspx?OP=0&COMPONENTE=' + mioval + '&COMPONENTE1=' + mioval1, window, 'status:no;dialogWidth:433px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');

}

function ModificaReddito() {
obj1=document.getElementById('Dic_Reddito1_ListBox1');
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

    tipoirpef = str.substring(36, 61);
    
    irpef = str.substring(62, 70);
    
    tipoagrari = str.substring(74, 99);
    
    agrari = str.substring(100, 108);
    
    nome=obj1.options[obj1.selectedIndex].value;
    window.showModalDialog("com_reddito_bandi.aspx?OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&IR=" + irpef + "&AG=" + agrari + "&TIPOR=" + tipoirpef + "&TIPOA=" + tipoagrari ,'','status:no;dialogWidth:433px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
}
}
// -->
</script>
<div id="red" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 201; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 400px;
    background-color: #ffffff">
    &nbsp;
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 4px; position: absolute;
        top: 4px" Width="556px">QUADRO D: REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</asp:Label>
        <div id="ContenitoreRedditi" 
        
        
        style="overflow: auto; position: absolute; width: 584px; height: 325px; top: 56px; left: 8px;">
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 104; left: 10px;
        position: absolute; top: 0px" Width="72px">COMPONENTE</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 105; left: 430px;
        position: absolute; top: 0px; height: 18px; width: 94px;">REDDITO IRPEF</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 105; left: 264px;
        position: absolute; top: 0px; width: 136px;">TIPO REDDITI IRPEF</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 105; left: 529px;
        position: absolute; top: 0px; width: 170px;">TIPO PROVENTI AGRARI</asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 106; left: 712px;
        position: absolute; top: 0px" Width="106px">PROVENTI AGRARI</asp:Label>
    <asp:ListBox ID="ListBox1" runat="server" Font-Names="Courier New" Font-Size="8pt" 
                
                
                
                Style="z-index: 101; left: 6px; position: absolute; top: 15px; width: 903px; height: 289px;"></asp:ListBox>
        </div>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 102; left: 6px;
        position: absolute; top: 29px" Width="253px">Anno di riferimento della situazione economica</asp:Label>
    <asp:Label ID="lblAnnoR" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="times"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 103; left: 264px; position: absolute;
        top: 29px" Text="2005"></asp:Label>
    &nbsp; &nbsp;
    
    &nbsp;
    
    
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnAggiungi" runat="server" Style="z-index: 107; left: 598px; position: absolute;
        top: 71px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnModifica" runat="server" Style="z-index: 108; left: 598px; position: absolute;
        top: 100px" Text="/" Width="38px" ToolTip="Modifica Elemento" 
        UseSubmitBehavior="False" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnElimina" runat="server" Style="z-index: 109; left: 598px; position: absolute;
        top: 129px" Text="-" Width="38px" ToolTip="Elimina Elemento" 
        UseSubmitBehavior="False" 
        onclientclick="document.getElementById('H1').value='0';" />
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QD" Style="z-index: 111;
        left: 620px; position: absolute; top: 2px" Target="_blank" Width="17px">Aiuto</asp:HyperLink>
</div>
&nbsp;&nbsp;
<asp:HiddenField ID="V1" runat="server" />

