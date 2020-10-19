<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_patrimonioI_Bandi.aspx.vb" Inherits="com_patrimonioI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self"> </base>
    <title>Patrimonio Immobiliare Componenti</title>
    <style type="text/css">
    .CssMaiuscolo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; COLOR:blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
	.CssComuniNazioni { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 166px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssPresenta { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 450px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssFamiAbit { FONT-SIZE: 8pt;  WIDTH: 600px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssProv { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 48px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssIndirizzo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 66px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssLabel { FONT-SIZE: 8pt; COLOR: black; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: times; HEIGHT: 20px;FONT-VARIANT: normal }
	.CssLblValori { FONT-SIZE: 8pt; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
	.CssEtichetta { ALIGNMENT: center }
	.CssNuovoMaiuscolo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; COLOR:blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
    </style>
</head>
<script type="text/javascript" src="Funzioni.js"></script>
<body bgcolor="lightsteelblue">
    <form id="form1" runat="server">
    <div>
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 122;
            left: 377px; position: absolute; top: 274px" type="button" 
            value="Chiudi" />
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 101; left: 225px; position: absolute;
            top: 274px" TabIndex="7" Text="SALVA e Chiudi" />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 11px; position: absolute;
            top: 17px; width: 77px;">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" Style="z-index: 105;
            left: 114px; position: absolute; top: 14px" TabIndex="1" Width="316px" 
            Font-Bold="False" Font-Names="arial" Font-Size="8pt">
        </asp:DropDownList>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 12px; position: absolute;
            top: 46px" Width="79px">Tipologia F.</asp:Label>
        <asp:DropDownList onchange="Controlla();" ID="cmbTipo" runat="server" Style="z-index: 107;
            left: 114px; position: absolute; top: 43px" TabIndex="2" Width="316px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="3">--</asp:ListItem>
            <asp:ListItem Value="0">FABBRICATI</asp:ListItem>
            <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
            <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList onchange="ControllaImporto();MostraSalva();" ID="cmbTipoDoc" runat="server" Style="z-index: 107;
            left: 114px; position: absolute; top: 101px" TabIndex="4" Width="316px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">AUTOCERTIFICATO</asp:ListItem>
            <asp:ListItem Value="1">730</asp:ListItem>
            <asp:ListItem Value="2">UNICO</asp:ListItem>
            <asp:ListItem Value="3">ATTO DI ACQUISIZIONE</asp:ListItem>
            <asp:ListItem Value="4">DICHIARAZIONE ICI</asp:ListItem>
            <asp:ListItem Value="5">VISURA CATASTO</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList onchange="ControllaMutuo();MostraSalva();" ID="cmbTipoDocMutuo" runat="server" Style="z-index: 107;
            left: 114px; position: absolute; top: 160px" TabIndex="6" Width="316px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">AUTOCERTIFICATO</asp:ListItem>
            <asp:ListItem Value="1">DOC. ISTITUTO DI CREDITO</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 13px; position: absolute;
            top: 73px" Width="69px">% Possesso</asp:Label>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 108; left: 13px; position: absolute;
            top: 103px; width: 99px; height: 29px;">Doc. Importo</asp:Label>
            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 108; left: 13px; position: absolute;
            top: 163px; width: 99px; height: 16px;">Doc. Mutuo</asp:Label>
        <asp:TextBox ID="txtPerc" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="5" Style="z-index: 109;
            left: 115px; position: absolute; top: 71px" TabIndex="3" Width="34px">100</asp:TextBox>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 110; left: 161px; position: absolute; top: 74px"
            Text="(valorizzare)" Visible="False" Width="245px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 208px; position: absolute; top: 132px"
            Text="(valorizzare)" Visible="False" Width="210px"></asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 186px; position: absolute;
            top: 133px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtValore" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="8" Style="z-index: 113;
            left: 114px; position: absolute; top: 130px; width: 65px;" TabIndex="5"></asp:TextBox>
        <asp:Label ID="lblValore" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 14px; position: absolute;
            top: 132px" Width="71px">Valore</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 115; left: 187px; position: absolute;
            top: 193px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="TxtMutuo" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="8" Style="z-index: 116;
            left: 114px; position: absolute; top: 189px; width: 65px;" TabIndex="7"></asp:TextBox>
        <asp:Label ID="lblMutuo" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 117; left: 14px; position: absolute;
            top: 191px" Width="71px">Mutuo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 118; left: 209px; position: absolute; top: 192px; width: 218px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
        <asp:Label ID="lblResidenza" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 15px; position: absolute;
            top: 221px" Width="50px">Residenza</asp:Label>
        <asp:DropDownList ID="cmbResidenza" runat="server" Style="z-index: 120;
            left: 114px; position: absolute; top: 219px" TabIndex="8" Width="45px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Selected="True" Value="0">NO</asp:ListItem>
            <asp:ListItem Value="1">SI</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 175px; position: absolute; top: 222px"
            Text="(valorizzare)" Visible="False" Width="245px"></asp:Label>
    
    </div>
    <asp:HiddenField ID="txtOperazione" runat="server" />
    <asp:HiddenField ID="txtRiga" runat="server" />
    </form>
</body>
<script type="text/javascript">
document.getElementById('txtRiga').style.visibility='hidden';
document.getElementById('txtOperazione').style.visibility='hidden';




ControllaImporto();
ControllaMutuo();
Controlla();

function ControllaImporto() {

    if (document.getElementById('cmbTipoDoc').value == '-1') {
        document.getElementById('lblValore').style.visibility = 'hidden';
        document.getElementById('txtValore').style.visibility = 'hidden';
        document.getElementById('Label5').style.visibility = 'hidden';
    }
    else {
        document.getElementById('lblValore').style.visibility = 'visible';
        document.getElementById('txtValore').style.visibility = 'visible';
        document.getElementById('Label5').style.visibility = 'visible';
    }
}

function ControllaMutuo() {

    if (document.getElementById('cmbTipoDocMutuo').value == '-1') {
        document.getElementById('lblMutuo').style.visibility = 'hidden';
        document.getElementById('TxtMutuo').style.visibility = 'hidden';
        document.getElementById('Label2').style.visibility = 'hidden';
    }
    else {
        document.getElementById('lblMutuo').style.visibility = 'visible';
        document.getElementById('TxtMutuo').style.visibility = 'visible';
        document.getElementById('Label2').style.visibility = 'visible';
    }
}

function Controlla() {
    
    switch (document.getElementById('cmbTipo').value) {
        case '0':
            Mostra();
            document.getElementById('lblResidenza').style.visibility = 'visible';
            document.getElementById('cmbResidenza').style.visibility = 'visible';
            ControllaImporto();
            ControllaMutuo();
            break;
        case '1':
            Mostra();
            document.getElementById('lblResidenza').style.visibility = 'hidden';
            document.getElementById('cmbResidenza').style.visibility = 'hidden';
            ControllaImporto();
            ControllaMutuo();
            break;
        case '2':
            Mostra();
            document.getElementById('lblResidenza').style.visibility = 'hidden';
            document.getElementById('cmbResidenza').style.visibility = 'hidden';
            ControllaImporto();
            ControllaMutuo();
            break;
        case '3':
            Nascondi();
            break;
    }

}

function Nascondi() {
    document.getElementById('btnSalva').style.visibility = 'hidden';

    if (document.getElementById('L3')) {
        document.getElementById('L3').style.visibility = 'hidden';
    }
    if (document.getElementById('L4')) {
        document.getElementById('L4').style.visibility = 'hidden';
    }
    if (document.getElementById('L1')) {
        document.getElementById('L1').style.visibility = 'hidden';
    }
    if (document.getElementById('L2')) {
        document.getElementById('L2').style.visibility = 'hidden';
    }

    if (document.getElementById('Label3')) {
        document.getElementById('Label3').style.visibility = 'hidden';
    }
    if (document.getElementById('Label2')) {
        document.getElementById('Label2').style.visibility = 'hidden';
    }
    if (document.getElementById('Label3')) {
        document.getElementById('Label3').style.visibility = 'hidden';
    }
    if (document.getElementById('Label5')) {
        document.getElementById('Label5').style.visibility = 'hidden';
    }
  
    if (document.getElementById('Label9')) {
        document.getElementById('Label9').style.visibility = 'hidden';
    }
    if (document.getElementById('Label10')) {
        document.getElementById('Label10').style.visibility = 'hidden';
    }


    if (document.getElementById('lblResidenza')) {
        document.getElementById('lblResidenza').style.visibility = 'hidden';
    }

    if (document.getElementById('lblMutuo')) {
        document.getElementById('lblMutuo').style.visibility = 'hidden';
    }

    if (document.getElementById('lblValore')) {
        document.getElementById('lblValore').style.visibility = 'hidden';
    }


    if (document.getElementById('cmbTipoDoc')) {
        document.getElementById('cmbTipoDoc').style.visibility = 'hidden';
    }

    if (document.getElementById('txtValore')) {
        document.getElementById('txtValore').style.visibility = 'hidden';
    }

    if (document.getElementById('cmbTipoDocMutuo')) {
        document.getElementById('cmbTipoDocMutuo').style.visibility = 'hidden';
    }

    if (document.getElementById('TxtMutuo')) {
        document.getElementById('TxtMutuo').style.visibility = 'hidden';
    }

    if (document.getElementById('cmbResidenza')) {
        document.getElementById('cmbResidenza').style.visibility = 'hidden';
    }

    if (document.getElementById('txtPerc')) {
        document.getElementById('txtPerc').style.visibility = 'hidden';
    }
    
}

function Mostra() {


    document.getElementById('Label3').style.visibility = 'visible';

    
//            if (document.getElementById('L3')) {
//                document.getElementById('L3').style.visibility = 'visible';
//            }
            //if (document.getElementById('Label9')) {
                document.getElementById('Label9').style.visibility = 'visible';
            //}
            //if (document.getElementById('Label10')) {
                document.getElementById('Label10').style.visibility = 'visible';
            //}

            //if (document.getElementById('lblResidenza')) {
                document.getElementById('lblResidenza').style.visibility = 'visible';
            //}

            //if (document.getElementById('lblMutuo')) {
                document.getElementById('lblMutuo').style.visibility = 'visible';
            //}

            //if (document.getElementById('lblValore')) {
                document.getElementById('lblValore').style.visibility = 'visible';
            //}


            //if (document.getElementById('cmbTipoDoc')) {
                document.getElementById('cmbTipoDoc').style.visibility = 'visible';
            //}

            //if (document.getElementById('txtValore')) {
                document.getElementById('txtValore').style.visibility = 'visible';
            //}

            //if (document.getElementById('cmbTipoDocMutuo')) {
                document.getElementById('cmbTipoDocMutuo').style.visibility = 'visible';
            //}

            //if (document.getElementById('TxtMutuo')) {
                document.getElementById('TxtMutuo').style.visibility = 'visible';
            //}

            //if (document.getElementById('cmbResidenza')) {
                document.getElementById('cmbResidenza').style.visibility = 'visible';
            //}
            //if (document.getElementById('txtPerc')) {
                document.getElementById('txtPerc').style.visibility = 'visible';
            //}
}


function MostraSalva() {
    if (document.getElementById('cmbTipoDoc').value != '-1' || document.getElementById('cmbTipoDocMutuo').value != '-1') {
        document.getElementById('btnSalva').style.visibility = 'visible';
        if (document.getElementById('L3')) {
            document.getElementById('L3').style.visibility = 'hidden';
        }
        if (document.getElementById('L2')) {
            document.getElementById('L2').style.visibility = 'hidden';
        }
    }
    else {
        document.getElementById('btnSalva').style.visibility = 'hidden';
        if (document.getElementById('L3')) {
            document.getElementById('L3').style.visibility = 'hidden';
        }
        if (document.getElementById('L2')) {
            document.getElementById('L2').style.visibility = 'hidden';
        }
    }

}

function Chiudi()
{
window.close();
}

</script>
</html>
