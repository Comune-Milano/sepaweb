<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_nucleo_bandi.aspx.vb" Inherits="prova"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self"> </base>
    
    <title>Componenti Nucleo</title>
<script language="javascript" type="text/javascript">
<!--


function Button2_onclick() {
window.close();
}



// -->
</script>
   
</head>
<script type="text/javascript" src="Funzioni.js"></script>
<body bgcolor="lightsteelblue">
    <form id="form1" runat="server">
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <div id="Div1" style="border-right: lightsteelblue 2px solid; border-top: lightsteelblue 2px solid;
            z-index: 191; background-attachment: fixed; left: 2px; border-left: lightsteelblue 2px solid;
            width: 433px; border-bottom: lightsteelblue 2px solid; position: absolute; top: 2px;
            height: 291px; background-color: lightsteelblue">
            <asp:TextBox ID="txtCognome" runat="server"
                Font-Bold="False" Font-Names="arial" Font-Size="8pt" MaxLength="25"
                
                Style="z-index: 100; left: 103px; position: absolute; top: 24px; width: 170px;" 
                TabIndex="1"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
                Font-Size="8pt" Height="18px" Style="z-index: 101; left: 21px; position: absolute;
                top: 25px; width: 68px;">Cognome</asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
                Font-Size="8pt" Height="18px" Style="z-index: 102; left: 21px; position: absolute;
                top: 52px; width: 57px;">Nome</asp:Label>
            <p>
                <asp:TextBox ID="txtNome" runat="server" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" MaxLength="25"
                    Style="z-index: 103; left: 103px; position: absolute; top: 50px" 
                    TabIndex="2" Width="170px"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 104; left: 21px; position: absolute;
                    top: 78px" Width="69px">Data Nascita</asp:Label>
                    <asp:Label ID="lblDataCert" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 104; left: 21px; position: absolute;
                    top: 186px; width: 75px;">Data Certific.</asp:Label>
                <asp:TextBox ID="txtData" runat="server" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" MaxLength="10"
                    Style="z-index: 105; left: 103px; position: absolute; top: 77px; width: 90px;" 
                    TabIndex="3"></asp:TextBox>
                    <asp:TextBox ID="txtDataCertificato" runat="server" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" MaxLength="10"
                    Style="z-index: 105; left: 103px; position: absolute; top: 183px; width: 90px;" 
                    TabIndex="7"></asp:TextBox>
            </p>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <p>
                &nbsp;&nbsp;
                <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 107; left: 281px; position: absolute; top: 25px"
                    Text="(valorizzare)" Visible="False" Width="138px"></asp:Label>
            </p>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <p>
                <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 108; left: 281px; position: absolute; top: 52px"
                    Text="(valorizzare)" Visible="False" Width="136px"></asp:Label>
                <input id="Button2" style="z-index: 129; left: 358px; position: absolute; top: 328px"
                    type="button" value="Chiudi" language="javascript" 
                    onclick="return Button2_onclick()" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Style="z-index: 110; left: 211px; position: absolute;
                    top: 328px" TabIndex="11" Text="SALVA e Chiudi" />
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 111; left: 21px; position: absolute;
                    top: 106px" Width="71px">Cod. Fiscale</asp:Label>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 112; left: 21px; position: absolute;
                    top: 134px" Width="71px">Gr. Parentela</asp:Label>
                <asp:TextBox ID="txtCF" runat="server" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" MaxLength="16"
                    
                    Style="z-index: 113; left: 103px; position: absolute; top: 105px; width: 170px;" 
                    TabIndex="4"></asp:TextBox>
                <asp:DropDownList ID="cmbParenti" runat="server" Style="z-index: 114;
                    left: 103px; position: absolute; top: 131px" TabIndex="5" Width="316px" 
                    Font-Names="arial" Font-Size="8pt">
                    <asp:ListItem Value="1">CAPOFAMIGLIA</asp:ListItem>
                    <asp:ListItem Value="2">CONIUGE</asp:ListItem>
                    <asp:ListItem Value="3">FIGLIO/A</asp:ListItem>
                    <asp:ListItem Value="4">GENITORE</asp:ListItem>
                    <asp:ListItem Value="5">FRATELLO/SORELLA</asp:ListItem>
                    <asp:ListItem Value="6">NIPOTE</asp:ListItem>
                    <asp:ListItem Value="7">NIPOTE COLLATERALE</asp:ListItem>
                    <asp:ListItem Value="8">NIPOTE AFFINE</asp:ListItem>
                    <asp:ListItem Value="9">ZIO/A</asp:ListItem>
                    <asp:ListItem Value="10">CUGINO/A</asp:ListItem>
                    <asp:ListItem Value="11">NUORA/GENERO</asp:ListItem>
                    <asp:ListItem Value="12">SUOCERO/A</asp:ListItem>
                    <asp:ListItem Value="13">COGNATO/A</asp:ListItem>
                    <asp:ListItem Value="14">BISCUGINO/A</asp:ListItem>
                    <asp:ListItem Value="15">ALTRO AFFINE</asp:ListItem>
                    <asp:ListItem Value="16">CONVIVENTE</asp:ListItem>
                    <asp:ListItem Value="17">NUBENDO/A</asp:ListItem>
                    <asp:ListItem Value="18">ALTRO PARENTE</asp:ListItem>
                    <asp:ListItem Value="20">NONNO/A</asp:ListItem>
                    <asp:ListItem Value="22">BISNONNO/A</asp:ListItem>
                    <asp:ListItem Value="24">FIGLIASTRO/A</asp:ListItem>
                    <asp:ListItem Value="26">PATRIGNO/MATRIGNA</asp:ListItem>
                    <asp:ListItem Value="28">FRATELLASTRO/SORELLASTRA</asp:ListItem>
                    <asp:ListItem Value="30">ZIO/A AFFINE</asp:ListItem>
                    <asp:ListItem Value="32">PRONIPOTE</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 115; left: 21px; position: absolute;
                    top: 214px" Width="69px">% Invalidità</asp:Label>
                <asp:TextBox ID="txtInv" runat="server" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" MaxLength="6" Style="z-index: 116;
                    left: 103px; position: absolute; top: 211px; width: 50px;" TabIndex="8"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 117; left: 21px; position: absolute;
                    top: 242px" Width="69px">ASL</asp:Label>
                <asp:TextBox ID="txtASL" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" 
                    MaxLength="5" Style="z-index: 118;
                    left: 103px; position: absolute; top: 239px; width: 50px;" TabIndex="9"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 119; left: 21px; position: absolute;
                    top: 270px" Width="71px">Ind. Accomp.</asp:Label>
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" Height="18px" Style="z-index: 119; left: 21px; position: absolute;
                    top: 159px" Width="71px">Invalidità</asp:Label>
                <asp:DropDownList ID="cmbAcc" runat="server" Style="z-index: 120;
                    left: 103px; position: absolute; top: 268px" TabIndex="10" Width="50px" 
                    Font-Names="arial" Font-Size="8pt">
                    <asp:ListItem Value="1">SI</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList onchange="Verifica()" ID="cmbInvalidita" 
                    runat="server" Style="z-index: 120;
                    left: 103px; position: absolute; top: 157px" TabIndex="6" Width="50px" 
                    Font-Names="arial" Font-Size="8pt">
                    <asp:ListItem Value="1">SI</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 121; left: 217px; position: absolute; top: 80px; height: 15px; width: 112px;"
                    Text="(valorizzare)" Visible="False"></asp:Label>
                <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 122; left: 280px; position: absolute; top: 107px; width: 87px;"
                    Text="(valorizzare)" Visible="False"></asp:Label>
                <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 123; left: 160px; position: absolute; top: 215px"
                    Text="(valorizzare)" Visible="False" Width="243px"></asp:Label>
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 123; left: 219px; position: absolute; top: 186px; height: 19px; width: 137px;"
                    Text="(valorizzare)" Visible="False"></asp:Label>
                <asp:Label ID="L6" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 124; left: 160px; position: absolute; top: 243px"
                    Text="(valorizzare)" Visible="False" Width="250px"></asp:Label>
                <asp:Label ID="L7" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 125; left: 160px; position: absolute; top: 270px"
                    Text="(valorizzare)" Visible="False" Width="248px"></asp:Label>
            </p>
        </div>
        <asp:HiddenField ID="txtOperazione" runat="server" />
        <asp:HiddenField ID="txtRiga" runat="server" />
        <asp:HiddenField ID="txtProgr" runat="server" />
        <asp:HiddenField ID="invalidita" runat="server" />
    <p>
        
    </p>
    
        <script type="text/javascript">
            document.getElementById('txtRiga').style.visibility = 'hidden';
            document.getElementById('txtProgr').style.visibility = 'hidden';
            document.getElementById('txtOperazione').style.visibility = 'hidden';

            if (document.getElementById('invalidita').value == '1') {
                document.getElementById('txtDataCertificato').style.visibility = 'visible';
                document.getElementById('txtInv').style.visibility = 'visible';
                document.getElementById('txtASL').style.visibility = 'visible';
                document.getElementById('cmbAcc').style.visibility = 'visible';
                document.getElementById('lblDataCert').style.visibility = 'visible';
                document.getElementById('Label6').style.visibility = 'visible';
                document.getElementById('Label7').style.visibility = 'visible';
                document.getElementById('Label8').style.visibility = 'visible';
            }
            else {

                document.getElementById('txtDataCertificato').style.visibility = 'hidden';
                document.getElementById('txtInv').style.visibility = 'hidden';
                document.getElementById('txtASL').style.visibility = 'hidden';
                document.getElementById('cmbAcc').style.visibility = 'hidden';
                document.getElementById('lblDataCert').style.visibility = 'hidden';
                document.getElementById('Label6').style.visibility = 'hidden';
                document.getElementById('Label7').style.visibility = 'hidden';
                document.getElementById('Label8').style.visibility = 'hidden';

            }

            function Verifica() {
                if (document.getElementById('cmbInvalidita').value == '1') {
                    document.getElementById('invalidita').value = '1';
                    document.getElementById('txtDataCertificato').style.visibility = 'visible';
                    document.getElementById('txtInv').style.visibility = 'visible';
                    document.getElementById('txtASL').style.visibility = 'visible';
                    document.getElementById('cmbAcc').style.visibility = 'visible';
                    document.getElementById('lblDataCert').style.visibility = 'visible';
                    document.getElementById('Label6').style.visibility = 'visible';
                    document.getElementById('Label7').style.visibility = 'visible';
                    document.getElementById('Label8').style.visibility = 'visible';
                }
                else {
                    document.getElementById('invalidita').value = '0';
                    document.getElementById('txtDataCertificato').style.visibility = 'hidden';
                    document.getElementById('txtInv').style.visibility = 'hidden';
                    document.getElementById('txtASL').style.visibility = 'hidden';
                    document.getElementById('cmbAcc').style.visibility = 'hidden';
                    document.getElementById('lblDataCert').style.visibility = 'hidden';
                    document.getElementById('Label6').style.visibility = 'hidden';
                    document.getElementById('Label7').style.visibility = 'hidden';
                    document.getElementById('Label8').style.visibility = 'hidden';

                }
            }

</script>
    </form>
    
    </body>

</html>
