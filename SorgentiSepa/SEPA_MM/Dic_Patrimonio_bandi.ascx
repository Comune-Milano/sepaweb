<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Patrimonio_bandi.ascx.vb" Inherits="Dic_Patrimonio" %>

<script type="text/javascript" src="Funzioni.js"></script>
<script type="text/javascript">
<!--

function MyDialogArguments2()
{
    this.Sender = null;
    this.StringValue = "";
}


function AggiungiPatrimonio() {

dialogArgs = new MyDialogArguments2();
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

window.showModalDialog("com_patrimonio_bandi.aspx?OP=0&COMPONENTE=" + mioval,window,'status:no;dialogWidth:480px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');
}

function ModificaPatrimonio() {
obj1=document.getElementById('Dic_Patrimonio1_ListBox1');
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
    
    
    tipologia=str.substring(26,53);
    sottotipo=str.substring(54,79);
    imp=str.substring(80,90);
    iban = str.substring(94, 121);
    intermediario = str.substring(122, 172);

    nome=obj1.options[obj1.selectedIndex].value;
    window.showModalDialog("com_patrimonio_bandi.aspx?INTERMEDIARIO=" + intermediario + "&OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&TIPO=" + tipologia + "&SOTTOTIPO=" + sottotipo  + "&IMP=" + imp  + "&IBAN=" + iban  ,'','status:no;dialogWidth:480px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');
}
}

function AggiungiPatrimonioI() {

dialogArgs = new MyDialogArguments2();
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

window.showModalDialog("com_patrimonioI_Bandi.aspx?OP=0&COMPONENTE=" + mioval,window,'status:no;dialogWidth:450px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
}

function ModificaPatrimonioI() {
obj1=document.getElementById('Dic_Patrimonio1_ListBox2');
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


    tipo = str.substring(26, 45);
    
    perc = str.substring(48, 53);
    
    tipodoc = str.substring(54, 78);
   
    valore = str.substring(80, 88);
    
    tipodocmutuo = str.substring(92, 116);
   
    mutuo = str.substring(118, 126);
    
    res = str.substring(130, 135);
    
    nome=obj1.options[obj1.selectedIndex].value;
    window.showModalDialog("com_patrimonioI_Bandi.aspx?OP=1&RI=" + obj1.selectedIndex + "&COMPONENTE=" + mioval + "&COMP=" + nome + "&TIPO=" + tipo + "&PER=" + perc + "&TIPOVAL=" + tipodoc +  "&VAL=" + valore + "&TIPOMU=" + tipodocmutuo + "&MU=" + mutuo + "&RES=" + res,'','status:no;dialogWidth:450px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
}
}
// -->
</script>

<div id="pat" 
    style="border: 1px solid lightsteelblue; z-index: 200; left: 10px; width: 641px;
    position: absolute; top: 106px; height: 400px; background-color: #ffffff;" >
    &nbsp;
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 4px; position: absolute;
        top: 4px" Width="519px">QUADRO C: SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</asp:Label>
        <div id="contenitoreMobiliare" 
        
        
        
        style="z-index: 200;position: absolute; width: 583px; top: 44px; left: 6px; height: 129px; overflow: auto">
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 107; left: 10px;
        position: absolute; top: 0px" Width="72px">COMPONENTE</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 108; left: 194px;
        position: absolute; top: 0px; width: 114px;">TIPOLOGIA</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 109; left: 389px;
        position: absolute; top: 0px; width: 115px;">RILEVATO DA</asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 110; left: 576px;
        position: absolute; top: 0px" Width="55px">IMPORTO</asp:Label>
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 110; left: 670px;
        position: absolute; top: 0px" Width="55px">IBAN</asp:Label>
        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 110; left: 861px;
        position: absolute; top: 0px" Width="55px">INTERMEDIARIO</asp:Label>
    <asp:ListBox ID="ListBox1" runat="server" Style="z-index: 101; left: 6px;
        position: absolute; top: 15px; width: 1263px; height: 93px;" Font-Names="Courier New" 
                Font-Size="8pt"></asp:ListBox>

        </div>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 102; left: 6px;
        position: absolute; top: 24px" Width="284px">PATRIMONIO MOBILIARE alla data del 31 Dicembre</asp:Label>
    <asp:Label ID="lbldataMob" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="times"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 103; left: 291px; position: absolute;
        top: 24px" Text="2005"></asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 104; left: 6px;
        position: absolute; top: 180px" Width="300px">PATRIMONIO IMMOBILIARE alla data del 31 Dicembre</asp:Label>
    <asp:Label ID="lblDataImm" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="times"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 105; left: 310px; position: absolute;
        top: 180px" Text="2005"></asp:Label>
        <div id="ContenitoreImmobili" 
        
        
        
        style="z-index: 200;position: absolute; width: 583px; top: 202px; left: 6px; height: 144px; overflow: auto">
        
    <asp:ListBox ID="ListBox2" runat="server" Style="z-index: 106; left: 6px;
        position: absolute; top: 15px; height: 107px; width: 986px;" 
        Font-Names="Courier New" Font-Size="8pt"></asp:ListBox>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 111; left: 10px;
        position: absolute; top: 0px" Width="72px">COMPONENTE</asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 112; left: 194px;
        position: absolute; top: 0px" Width="49px">TIPO</asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 113; left: 346px;
        position: absolute; top: 0px" Width="37px">% Pr.</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 114; left: 388px;
        position: absolute; top: 0px; width: 84px;">DOC.IMPORTO</asp:Label>
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 115; left: 571px;
        position: absolute; top: 0px" Width="55px">IMPORTO</asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 116; left: 655px;
        position: absolute; top: 0px; width: 76px;">DOC. MUTUO</asp:Label>
        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 116; left: 838px;
        position: absolute; top: 0px; width: 76px;">IMPORTO</asp:Label>
        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Courier New"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 116; left: 921px;
        position: absolute; top: 0px; width: 76px;">RES.</asp:Label>
        </div>
    
    
    
    
    
    
    <asp:Button ID="Button1" runat="server" Style="z-index: 117; left: 598px; position: absolute;
        top: 61px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnModifica" runat="server" Style="z-index: 118; left: 598px; position: absolute;
        top: 90px" Text="/" Width="38px" ToolTip="Modifica Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="Button2" runat="server" Style="z-index: 119; left: 598px; position: absolute;
        top: 119px" Text="-" Width="38px" ToolTip="Elimina Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnAggiungi" runat="server" Style="z-index: 120; left: 598px; position: absolute;
        top: 220px" Text="+" Width="38px" ToolTip="Aggiungi Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="btnMod" runat="server" Style="z-index: 121; left: 598px; position: absolute;
        top: 249px" Text="/" Width="38px" ToolTip="Modifica Elemento" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:Button ID="Button6" runat="server" Style="z-index: 122; left: 598px; position: absolute;
        top: 278px" Text="-" Width="38px" ToolTip="Elimina Elemento" 
        UseSubmitBehavior="False" 
        onclientclick="document.getElementById('H1').value='0';" />
    <asp:DropDownList ID="cmbTipoImm" runat="server" Enabled="False" Style="z-index: 123;
        left: 332px; position: absolute; top: 351px" Width="64px" 
        Font-Names="arial" Font-Size="8pt">
    </asp:DropDownList>
    <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 124; left: 7px;
        position: absolute; top: 354px" Width="339px">Categoria catastale dell'immobile ad uso abitativo del nucleo</asp:Label>
    &nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QC" Style="z-index: 126;
        left: 620px; position: absolute; top: 2px" Target="_blank" Width="18px">Aiuto</asp:HyperLink>
    <asp:DropDownList ID="cmbUI" runat="server" Enabled="False" Style="z-index: 123;
        left: 401px; position: absolute; top: 374px; right: 195px;" Width="45px" 
        Font-Names="arial" Font-Size="8pt" Visible="False">
            <asp:ListItem Selected="True" Value="-1">----</asp:ListItem>
            <asp:ListItem Value="1">SI</asp:ListItem>
            <asp:ListItem Value="0">NO</asp:ListItem>
        </asp:DropDownList>
    <asp:Label ID="Label15" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Height="18px" Style="z-index: 124; left: 7px;
        position: absolute; top: 376px" Visible="False" Width="393px">posesso di U.I. adeguata al nucleo e/o di valore (RR 1/2004 Art.18 lett. f e g)</asp:Label>
    <asp:CheckBox ID="chUbicazione" runat="server" Enabled="False" Font-Bold="True" Font-Names="arial"
        Font-Size="8pt" ForeColor="#0000C0" Style="left: 446px; position: absolute; top: 373px"
        Text="Ub. a MILANO o entro 70 Km" Visible="False" Width="176px" 
        ToolTip="Ubicazione dell'immobile a Milano o entro 70 Km" />
</div>
&nbsp; &nbsp;&nbsp;
<asp:HiddenField ID="V1" runat="server" />
<asp:HiddenField ID="V2" runat="server" />

