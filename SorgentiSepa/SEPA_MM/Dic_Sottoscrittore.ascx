<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Sottoscrittore.ascx.vb" Inherits="Dic_Sottoscrittore" %>



<div id="sot"  style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 210; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 315px;
    background-color: #ffffff; visibility: hidden;">
    
    <script type="text/javascript">



function Nascondi(){
//document.getElementById('img1').src = '..\IMG\noSotto.gif';
    //document.getElementById('img2').src = '..\IMG\Sotto.gif';
document.getElementById('img1').src = 'IMG/noSotto.gif';
document.getElementById('img2').src = 'IMG/Sotto.gif';
document.getElementById('Dic_Sottoscrittore1_txtS').value='0'
document.getElementById('Dic_Sottoscrittore1_txtCognome').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label1').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label2').disabled=true;
document.getElementById('Dic_Sottoscrittore1_txtNome').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label18').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label5').disabled=true;
document.getElementById('Dic_Sottoscrittore1_cmbNazioneNas').disabled=true;


if (document.getElementById('Dic_Sottoscrittore1_cmbNazioneNas').value==3670)
{
document.getElementById('Dic_Sottoscrittore1_Label6').disabled=true;
document.getElementById('Dic_Sottoscrittore1_cmbPrNas').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label7').disabled=true;
document.getElementById('Dic_Sottoscrittore1_cmbComuneNas').disabled=true;
}
document.getElementById('Dic_Sottoscrittore1_Label8').disabled=true;
document.getElementById('Dic_Sottoscrittore1_txtDataNascita').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label17').disabled=true;

document.getElementById('Dic_Sottoscrittore1_cmbNazioneRes').disabled=true;

if (document.getElementById('Dic_Sottoscrittore1_cmbNazioneRes').value==3670)
{
document.getElementById('Dic_Sottoscrittore1_Label10').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label11').disabled=true;
document.getElementById('Dic_Sottoscrittore1_cmbPrRes').disabled=true;
document.getElementById('Dic_Sottoscrittore1_cmbComuneRes').disabled=true;
}

document.getElementById('Dic_Sottoscrittore1_Label12').disabled=true;

document.getElementById('Dic_Sottoscrittore1_Label13').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label14').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label15').disabled=true;
document.getElementById('Dic_Sottoscrittore1_Label16').disabled=true;


document.getElementById('Dic_Sottoscrittore1_cmbTipoIRes').disabled=true;
document.getElementById('Dic_Sottoscrittore1_txtIndRes').disabled=true;
document.getElementById('Dic_Sottoscrittore1_txtCivicoRes').disabled=true;
document.getElementById('Dic_Sottoscrittore1_txtCAPRes').disabled=true;
document.getElementById('Dic_Sottoscrittore1_txtTelRes').disabled=true;

}

function Visualizza(){

//    document.getElementById('img1').src = '..\IMG\Sotto.gif';
    //    document.getElementById('img2').src = '..\IMG\noSotto.gif';
document.getElementById('img1').src = 'IMG/Sotto.gif';
document.getElementById('img2').src = 'IMG/noSotto.gif';
document.getElementById('Dic_Sottoscrittore1_txtS').value='1'
document.getElementById('Dic_Sottoscrittore1_txtCognome').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label1').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label2').disabled=false;
document.getElementById('Dic_Sottoscrittore1_txtNome').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label18').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label5').disabled=false;
document.getElementById('Dic_Sottoscrittore1_cmbNazioneNas').disabled=false;


if (document.getElementById('Dic_Sottoscrittore1_cmbNazioneNas').value==3670)
{
document.getElementById('Dic_Sottoscrittore1_Label6').disabled=false;
document.getElementById('Dic_Sottoscrittore1_cmbPrNas').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label7').disabled=false;
document.getElementById('Dic_Sottoscrittore1_cmbComuneNas').disabled=false;
}

document.getElementById('Dic_Sottoscrittore1_Label8').disabled=false;
document.getElementById('Dic_Sottoscrittore1_txtDataNascita').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label17').disabled=false;
document.getElementById('Dic_Sottoscrittore1_cmbNazioneRes').disabled=false;


if (document.getElementById('Dic_Sottoscrittore1_cmbNazioneRes').value==3670)
{
document.getElementById('Dic_Sottoscrittore1_Label10').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label11').disabled=false;
document.getElementById('Dic_Sottoscrittore1_cmbPrRes').disabled=false;
document.getElementById('Dic_Sottoscrittore1_cmbComuneRes').disabled=false;
}

document.getElementById('Dic_Sottoscrittore1_Label12').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label13').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label14').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label15').disabled=false;
document.getElementById('Dic_Sottoscrittore1_Label16').disabled=false;
document.getElementById('Dic_Sottoscrittore1_cmbTipoIRes').disabled=false;
document.getElementById('Dic_Sottoscrittore1_txtIndRes').disabled=false;
document.getElementById('Dic_Sottoscrittore1_txtCivicoRes').disabled=false;
document.getElementById('Dic_Sottoscrittore1_txtCAPRes').disabled=false;
document.getElementById('Dic_Sottoscrittore1_txtTelRes').disabled=false;
}


</script>

    &nbsp;&nbsp;
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
            ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QS" Style="z-index: 112;
            left: 620px; position: absolute; top: 2px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
        &nbsp;</p>
    <asp:Label ID="Label9" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 10px" Width="64px">Milano, lì</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:TextBox ID="txtData1" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 102; left: 76px; position: absolute; top: 9px" TabIndex="1" Width="76px"></asp:TextBox>
    &nbsp; &nbsp;&nbsp;
    <img alt="Sottoscrittore" id="img1" src="IMG/noSotto.gif" style="z-index: 109; left: 13px; position: absolute;
        top: 42px; width: 10px; height: 10px;" language="javascript" onclick="Visualizza()" />
    <p>
        &nbsp;
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 27px; position: absolute;
            top: 59px" Width="113px">NO Sottoscrittore</asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 27px; position: absolute;
            top: 40px" Width="64px">Sottoscrittore</asp:Label>
        <img alt="No Sottoscrittore" id="img2" src="IMG/Sotto.gif" style="z-index: 110; left: 13px; position: absolute;
        top: 61px; width: 10px; height: 10px;" language="javascript" 
            onclick="Nascondi()" />
        <asp:Label ID="Label19" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="X-Small" ForeColor="Red" Height="16px" Style="z-index: 105; left: 159px;
            position: absolute; top: 9px" Visible="False" Width="405px"></asp:Label>
        &nbsp;
        <asp:TextBox ID="txtId" runat="server" Style="left: 462px; position: absolute;
            top: 105px; z-index: 106;" Visible="False" Width="13px" Height="14px"></asp:TextBox>
        <asp:TextBox ID="txtbinserito" runat="server" Style="left: 72px; position: absolute;
            top: 255px" Width="14px" Height="13px"></asp:TextBox>
        <asp:TextBox ID="txtS" runat="server" Style="left: 561px; position: absolute;
            top: 108px; z-index: 108;" Width="11px" Height="13px">0</asp:TextBox>
        &nbsp; &nbsp; &nbsp; &nbsp;
    </p>
    <div id="S" style="z-index: 111; left: 5px; width: 629px; position: absolute; top: 94px;
        height: 188px">

    <asp:TextBox ID="txtCognome" runat="server" Columns="53" CssClass="CssMaiuscolo"
        Font-Bold="False" Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="50"
        Style="z-index: 100; left: 66px; position: absolute; top: 10px" TabIndex="2"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 10px" Width="50px">Cognome</asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 102; left: 340px; position: absolute;
        top: 11px" Width="31px">Nome</asp:Label>
        <asp:TextBox ID="txtNome" runat="server" Columns="53" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="50"
            Style="z-index: 103; left: 379px; position: absolute; top: 10px" TabIndex="3"></asp:TextBox>
        <asp:Label ID="Label18" runat="server" BackColor="LemonChiffon" BorderStyle="Solid"
            BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0"
            Height="10px" Style="z-index: 104; left: 5px; position: absolute; top: 38px"
            Width="616px">................................................................................................ NATO A.............................................................................................</asp:Label>
    <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 105; left: 10px; position: absolute;
        top: 62px" Width="50px">Nato in</asp:Label>
        <asp:DropDownList ID="cmbNazioneNas" runat="server" Style="left: 66px;
            position: absolute; top: 60px; z-index: 106;" CssClass="CssComuniNazioni" Font-Names="TIMES" Font-Size="8pt" Width="166px" AutoPostBack="True">
        </asp:DropDownList>
    <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 107; left: 244px; position: absolute;
        top: 61px" Width="21px">Pr.</asp:Label>
        <asp:DropDownList ID="cmbPrNas" runat="server" Style="left: 264px;
            position: absolute; top: 59px; z-index: 108;" CssClass="CssProv" Font-Names="TIMES" Font-Size="8pt" Width="47px" AutoPostBack="True">
        </asp:DropDownList>
    <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 109; left: 328px; position: absolute;
        top: 61px" Width="45px">Comune</asp:Label>
        <asp:DropDownList ID="cmbComuneNas" runat="server" Style="left: 379px;
            position: absolute; top: 60px; z-index: 110;" CssClass="CssComuniNazioni" Font-Names="TIMES" Font-Size="8pt" Width="156px">
        </asp:DropDownList>
    <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 111; left: 543px; position: absolute;
        top: 62px" Width="13px">Il</asp:Label>
    <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        MaxLength="10" Style="z-index: 112; left: 555px; position: absolute; top: 61px"
        TabIndex="9"></asp:TextBox>
        <asp:Label ID="lblErrData" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="X-Small" ForeColor="Red" Height="16px" Style="z-index: 113; left: 456px;
            position: absolute; top: 83px" Visible="False" Width="164px"></asp:Label>
    <asp:Label ID="Label17" runat="server" BackColor="LemonChiffon" BorderStyle="Solid"
        BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0"
        Height="10px" Style="z-index: 114; left: 5px; position: absolute; top: 106px"
        Width="616px">........................................................................................... RESIDENTE IN........................................................................................</asp:Label>
    <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 115; left: 10px; position: absolute;
        top: 131px" Width="55px">Residente</asp:Label>
        <asp:DropDownList ID="cmbNazioneRes" runat="server" Style="left: 66px;
            position: absolute; top: 131px; z-index: 116;" CssClass="CssComuniNazioni" Font-Names="TIMES" Font-Size="8pt" Width="166px" AutoPostBack="True">
        </asp:DropDownList>
    <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 117; left: 244px; position: absolute;
        top: 132px" Width="18px">Pr.</asp:Label>
        <asp:DropDownList ID="cmbPrRes" runat="server" Style="left: 264px;
            position: absolute; top: 131px; z-index: 118;" CssClass="CssProv" Font-Names="TIMES" Font-Size="8pt" Width="48px" AutoPostBack="True">
        </asp:DropDownList>
    <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 119; left: 329px; position: absolute;
        top: 132px" Width="44px">Comune</asp:Label>
        <asp:DropDownList ID="cmbComuneRes" runat="server" Style="left: 379px;
            position: absolute; top: 131px; z-index: 120;" CssClass="CssComuniNazioni" Font-Names="TIMES" Font-Size="8pt" Width="157px" AutoPostBack="True">
        </asp:DropDownList>
    <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 121; left: 10px; position: absolute;
        top: 160px" Width="51px">Indirizzo</asp:Label>
    <asp:TextBox ID="txtIndRes" runat="server" Columns="36" CssClass="CssMaiuscolo" Font-Bold="False"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="40"
        Style="z-index: 122; left: 137px; position: absolute; top: 159px" TabIndex="14"></asp:TextBox>
        <asp:DropDownList ID="cmbTipoIRes" runat="server" Style="left: 65px;
            position: absolute; top: 159px; z-index: 123;" CssClass="CssIndirizzo" Font-Names="TIMES" Font-Size="8pt" Width="67px">
        </asp:DropDownList>
    <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 124; left: 319px; position: absolute;
        top: 160px" Width="37px">Civico</asp:Label>
    <asp:TextBox ID="txtCivicoRes" runat="server" Columns="4" CssClass="CssMaiuscolo"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        MaxLength="5" Style="z-index: 125; left: 355px; position: absolute; top: 159px"
        TabIndex="15"></asp:TextBox>
    <asp:Label ID="Label15" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 126; left: 408px; position: absolute;
        top: 160px" Width="29px">CAP</asp:Label>
    <asp:TextBox ID="txtCAPRes" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5"
        Style="z-index: 127; left: 439px; position: absolute; top: 159px" TabIndex="16"></asp:TextBox>
    <asp:Label ID="Label16" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 128; left: 507px; position: absolute;
        top: 160px" Width="46px">Tel.</asp:Label>
    <asp:TextBox ID="txtTelRes" runat="server" Columns="13" CssClass="CssMaiuscolo" Font-Bold="False"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="15"
        Style="z-index: 130; left: 533px; position: absolute; top: 159px" TabIndex="17"></asp:TextBox>
    </div>
    <script type="text/javascript">
    if (document.getElementById('Dic_Sottoscrittore1_txtS').value=='1') {
        
        Visualizza();
        }
        else
        {
        
        Nascondi();
        }
    </script>
</div>


