<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_patrimonio_bandi.aspx.vb" Inherits="com_patrimonio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <base target="_self"> </base>
    <title>Patrimonio Mobiliare Componenti</title>
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
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:HiddenField ID="txtOperazione" runat="server" />&nbsp;<asp:Label 
            ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 100; left: 12px; position: absolute;
            top: 30px; width: 77px;">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" Style="z-index: 101;
            left: 123px; position: absolute; top: 28px" TabIndex="1" Width="316px" 
            Font-Bold="True" Font-Names="arial" Font-Size="8pt">
        </asp:DropDownList>
        <asp:DropDownList onchange="Verifica()" ID="cmbScegliTipo" runat="server" Style="z-index: 101;
            left: 123px; position: absolute; top: 56px" TabIndex="2" Width="316px" 
            Font-Bold="True" Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="3">--</asp:ListItem>
            <asp:ListItem Value="0">SALDO AL 31/12</asp:ListItem>
            <asp:ListItem Value="1">TITOLI AL 31/12-ASS. VITA</asp:ListItem>
            <asp:ListItem Value="2">PARTECIPAZIONI AL 31/12</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList  onchange="Verifica1();" ID="cmbBAN" runat="server" Style="z-index: 101;
            left: 123px; position: absolute; top: 85px" TabIndex="3" Width="316px" 
            Font-Bold="True" Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="2">--</asp:ListItem>
            <asp:ListItem Value="0">AUTOCERTIFICATO</asp:ListItem>
            <asp:ListItem Value="1">SALDO CONTO</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList onchange="Verifica2()" ID="cmbTitoli" runat="server" Style="z-index: 101;
            left: 123px; position: absolute; top: 114px" TabIndex="4" Width="316px" 
            Font-Bold="True" Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="3">--</asp:ListItem>
            <asp:ListItem Value="0">AUTOCERTIFICATO</asp:ListItem>
            <asp:ListItem Value="1">SALDO TITOLI</asp:ListItem>
            <asp:ListItem Value="2">SALDO PREMI VERSATI</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList onchange="Verifica3()" ID="cmbPartecipazioni" runat="server" Style="z-index: 101;
            left: 123px; position: absolute; top: 142px" TabIndex="5" Width="316px" 
            Font-Bold="True" Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="5">--</asp:ListItem>
            <asp:ListItem Value="0">AUTOCERTIFICATO</asp:ListItem>
            <asp:ListItem Value="1">DOCUMENTAZIONE CCIAA</asp:ListItem>
            <asp:ListItem Value="2">730</asp:ListItem>
            <asp:ListItem Value="3">UNICO</asp:ListItem>
            <asp:ListItem Value="4">BILANCIO</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 13px; position: absolute;
            top: 59px" Width="69px">Scegli Tipo</asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 13px; position: absolute;
            top: 88px; width: 87px;">CC BAN O POST</asp:Label>
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 13px; position: absolute;
            top: 116px; width: 87px;">Fonte del Dato</asp:Label>
            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 13px; position: absolute;
            top: 144px; width: 87px;">Partecipazioni</asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 108; left: 374px; position: absolute; top: 201px; width: 112px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 109; left: 14px; position: absolute;
            top: 201px" Width="71px">Iban</asp:Label>
            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 109; left: 14px; position: absolute;
            top: 226px; width: 102px; height: 31px;">Val.Nominale/ Tot.Premi Versati</asp:Label>
            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 109; left: 14px; position: absolute;
            top: 265px; width: 109px; height: 31px;">Importo Partec.</asp:Label>
            <asp:Label ID="lblIntermediario" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 109; left: 14px; position: absolute;
            top: 290px; width: 101px; height: 31px;">Intermediario</asp:Label>
        <asp:TextBox ID="txtIban" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="27"
            Style="z-index: 110; left: 121px; position: absolute; top: 199px" TabIndex="7"
            Width="240px"></asp:TextBox>
            <asp:TextBox ID="txtIntermediario" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="100"
            
            Style="z-index: 110; left: 121px; position: absolute; top: 287px; width: 318px;" 
            TabIndex="10"></asp:TextBox>
            <asp:TextBox ID="txtValTitoli" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="9"
            
            Style="z-index: 110; left: 121px; position: absolute; top: 229px; width: 80px;" 
            TabIndex="8"></asp:TextBox>
            <asp:TextBox ID="txtValPartecipazioni" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="9"
            
            Style="z-index: 110; left: 121px; position: absolute; top: 259px; width: 80px;" 
            TabIndex="9"></asp:TextBox>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 261px; position: absolute; top: 233px"
            Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 261px; position: absolute; top: 261px"
            Text="(valorizzare)" Visible="False"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtSaldo" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" 
            MaxLength="9" Style="z-index: 115;
            left: 122px; position: absolute; top: 170px; width: 80px;" TabIndex="6"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 116; left: 13px; position: absolute;
            top: 174px; width: 93px;" Font-Overline="False">Importo Saldo</asp:Label>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 116; left: 212px; position: absolute;
            top: 263px; width: 27px;" Font-Overline="False">,00</asp:Label>
            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 116; left: 213px; position: absolute;
            top: 234px; width: 27px;" Font-Overline="False">,00</asp:Label>
            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 116; left: 213px; position: absolute;
            top: 174px; width: 27px;" Font-Overline="False">,00</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 117; left: 243px; position: absolute; top: 173px"
            Text="(valorizzare)" Visible="False" Width="203px"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 118; left: 230px; position: absolute;
            top: 360px" TabIndex="11" Text="SALVA e Chiudi" />
    
    </div>
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 121;
            left: 394px; position: absolute; top: 360px" type="button" 
        value="Chiudi" /><asp:HiddenField ID="txtRiga" runat="server" />
        <asp:HiddenField ID="tipologia" runat="server" />
        <asp:HiddenField ID="sottotipologia" runat="server" />
        <asp:HiddenField ID="nuovo" runat="server" />
    </form>
</body>
<script type="text/javascript">
document.getElementById('txtRiga').style.visibility='hidden';
document.getElementById('txtOperazione').style.visibility = 'hidden';


function azzera() {
    document.getElementById('cmbBAN').style.visibility = 'hidden';
    document.getElementById('Label2').style.visibility = 'hidden';
    document.getElementById('Label8').style.visibility = 'hidden';
    document.getElementById('txtSaldo').style.visibility = 'hidden';
    document.getElementById('Label11').style.visibility = 'hidden';
    document.getElementById('Label6').style.visibility = 'hidden';
    document.getElementById('cmbTitoli').style.visibility = 'hidden';
    document.getElementById('Label7').style.visibility = 'hidden';
    document.getElementById('cmbPartecipazioni').style.visibility = 'hidden';
    document.getElementById('Label4').style.visibility = 'hidden';
    document.getElementById('txtIban').style.visibility = 'hidden';
    document.getElementById('Label10').style.visibility = 'hidden';
    document.getElementById('txtValTitoli').style.visibility = 'hidden';
    document.getElementById('Label13').style.visibility = 'hidden';
    document.getElementById('Label12').style.visibility = 'hidden';
    document.getElementById('txtValPartecipazioni').style.visibility = 'hidden';
    document.getElementById('Label9').style.visibility = 'hidden';

    document.getElementById('txtIntermediario').style.visibility = 'hidden';
    document.getElementById('lblIntermediario').style.visibility = 'hidden';
}

if (document.getElementById('nuovo').value == '0') {
    Verifica()
    document.getElementById('nuovo').value='1';
}
else {
    azzera();
    AggiornaCampi
    switch(document.getElementById('sottotipologia').value)
    {
    case '0':
        document.getElementById('cmbBAN').value ='0';
        document.getElementById('cmbTitoli').value = '0';
        document.getElementById('cmbPartecipazioni').value = '0';
        break;
    case '1':
        document.getElementById('cmbBAN').value = '1';
        document.getElementById('cmbTitoli').value = '0';
        document.getElementById('cmbPartecipazioni').value = '0';
        break;
    case '2':
        document.getElementById('cmbBAN').value = '0';
        document.getElementById('cmbTitoli').value = '1';
        document.getElementById('cmbPartecipazioni').value = '0';
        break;
    case '3':
        document.getElementById('cmbBAN').value = '0';
        document.getElementById('cmbTitoli').value = '2';
        document.getElementById('cmbPartecipazioni').value = '0';
        break;
    case '4':
        document.getElementById('cmbBAN').value = '0';
        document.getElementById('cmbTitoli').value = '0';
        document.getElementById('cmbPartecipazioni').value = '1';
        break;
    case '5':
        document.getElementById('cmbBAN').value = '0';
        document.getElementById('cmbTitoli').value = '0';
        document.getElementById('cmbPartecipazioni').value = '2';
        break;
    case '6':
        document.getElementById('cmbBAN').value = '0';
        document.getElementById('cmbTitoli').value = '0';
        document.getElementById('cmbPartecipazioni').value = '3';
        break;
    case '7':
        document.getElementById('cmbBAN').value = '0';
        document.getElementById('cmbTitoli').value = '0';
        document.getElementById('cmbPartecipazioni').value = '4';
        break;
    }

    Verifica1();
    Verifica2();
    Verifica3();
}





function AggiornaCampi() {
    switch (document.getElementById('tipologia').value) {
        case '0':
            document.getElementById('cmbBAN').style.visibility = 'visible';
            document.getElementById('Label2').style.visibility = 'visible';
            document.getElementById('Label8').style.visibility = 'visible';
            document.getElementById('txtSaldo').style.visibility = 'visible';
            document.getElementById('Label11').style.visibility = 'visible';
            document.getElementById('txtIntermediario').style.visibility = 'visible';
            document.getElementById('lblIntermediario').style.visibility = 'visible';

            break;
        case '1':
            document.getElementById('Label6').style.visibility = 'visible';
            document.getElementById('cmbTitoli').style.visibility = 'visible';
            document.getElementById('Label10').style.visibility = 'visible';
            document.getElementById('txtValTitoli').style.visibility = 'visible';
            document.getElementById('Label13').style.visibility = 'visible';
            document.getElementById('txtIntermediario').style.visibility = 'visible';
            document.getElementById('lblIntermediario').style.visibility = 'visible';
            break;
        case '2':
            document.getElementById('Label7').style.visibility = 'visible';
            document.getElementById('cmbPartecipazioni').style.visibility = 'visible';
            document.getElementById('Label12').style.visibility = 'visible';
            document.getElementById('txtValPartecipazioni').style.visibility = 'visible';
            document.getElementById('Label9').style.visibility = 'visible';
            document.getElementById('txtIntermediario').style.visibility = 'visible';
            document.getElementById('lblIntermediario').style.visibility = 'visible';
            break;
    }
    
}


function Verifica() {
    switch (document.getElementById('cmbScegliTipo').value) {
        case '0':
            document.getElementById('tipologia').value = '0';
            azzera();
            AggiornaCampi();
            document.getElementById('cmbBAN').value = 2;
            document.getElementById('sottotipologia').value = 0;
            document.getElementById('Label8').style.visibility = 'hidden';
            document.getElementById('txtSaldo').style.visibility = 'hidden';
            document.getElementById('Label11').style.visibility = 'hidden';
            document.getElementById('txtIntermediario').style.visibility = 'hidden';
            document.getElementById('lblIntermediario').style.visibility = 'hidden';

            if (document.getElementById('Label14')) {
                document.getElementById('Label14').style.visibility = 'hidden';
            }
            if (document.getElementById('L1')) {
                document.getElementById('L1').style.visibility = 'hidden';
            }
            if (document.getElementById('L2')) {
                document.getElementById('L2').style.visibility = 'hidden';
            }
            if (document.getElementById('L3')) {
                document.getElementById('L3').style.visibility = 'hidden';
            }

            break;
        case '1':
            document.getElementById('tipologia').value = '1';
            azzera();
            AggiornaCampi();
            document.getElementById('cmbTitoli').value = 3;
            document.getElementById('sottotipologia').value = 0;
            document.getElementById('Label10').style.visibility = 'hidden';
            document.getElementById('txtValTitoli').style.visibility = 'hidden';
            document.getElementById('Label13').style.visibility = 'hidden';
            document.getElementById('Label4').style.visibility = 'hidden';
            document.getElementById('txtIban').style.visibility = 'hidden';

            document.getElementById('txtIntermediario').style.visibility = 'hidden';
            document.getElementById('lblIntermediario').style.visibility = 'hidden';

            if (document.getElementById('Label14')) {
                document.getElementById('Label14').style.visibility = 'hidden';
            }
            if (document.getElementById('L1')) {
                document.getElementById('L1').style.visibility = 'hidden';
            }
            if (document.getElementById('L2')) {
                document.getElementById('L2').style.visibility = 'hidden';
            }
            if (document.getElementById('L3')) {
                document.getElementById('L3').style.visibility = 'hidden';
            }

            break;
        case '2':
            document.getElementById('tipologia').value = '2';
            azzera();
            AggiornaCampi();
            document.getElementById('sottotipologia').value = 0;
            document.getElementById('cmbPartecipazioni').value = 5;

            document.getElementById('Label12').style.visibility = 'hidden';
            document.getElementById('txtValPartecipazioni').style.visibility = 'hidden';
            document.getElementById('Label9').style.visibility = 'hidden';

            document.getElementById('txtIntermediario').style.visibility = 'hidden';
            document.getElementById('lblIntermediario').style.visibility = 'hidden';

            if (document.getElementById('Label14')) {
                document.getElementById('Label14').style.visibility = 'hidden';
            }
            if (document.getElementById('L1')) {
                document.getElementById('L1').style.visibility = 'hidden';
            }
            if (document.getElementById('L2')) {
                document.getElementById('L2').style.visibility = 'hidden';
            }
            if (document.getElementById('L3')) {
                document.getElementById('L3').style.visibility = 'hidden';
            }

            break;
        case '3':
            azzera();
            document.getElementById('btnSalva').style.visibility = 'hidden';
            if (document.getElementById('Label14')) {
                document.getElementById('Label14').style.visibility = 'hidden';
            }
            if (document.getElementById('L1')) {
                document.getElementById('L1').style.visibility = 'hidden';
            }
            if (document.getElementById('L2')) {
                document.getElementById('L2').style.visibility = 'hidden';
            }
            if (document.getElementById('L3')) {
                document.getElementById('L3').style.visibility = 'hidden';
            }
            break;

    }
   
    
}


function Verifica1() {
   
    if (document.getElementById('tipologia').value == '0') {
        document.getElementById('cmbBAN').style.visibility = 'visible';
        document.getElementById('Label2').style.visibility = 'visible';

        switch (document.getElementById('cmbBAN').value) {
            case '0':
                document.getElementById('Label8').style.visibility = 'visible';
                document.getElementById('txtSaldo').style.visibility = 'visible';
                document.getElementById('Label11').style.visibility = 'visible';
                document.getElementById('Label4').style.visibility = 'visible';
                document.getElementById('txtIban').style.visibility = 'visible';
                document.getElementById('sottotipologia').value = '0';
                document.getElementById('btnSalva').style.visibility = 'visible';
                document.getElementById('txtIntermediario').style.visibility = 'visible';
                document.getElementById('lblIntermediario').style.visibility = 'visible';
                break;
            case '1':
                document.getElementById('Label8').style.visibility = 'visible';
                document.getElementById('txtSaldo').style.visibility = 'visible';
                document.getElementById('Label11').style.visibility = 'visible';
                document.getElementById('Label4').style.visibility = 'visible';
                document.getElementById('txtIban').style.visibility = 'visible';
                document.getElementById('sottotipologia').value = '1';
                document.getElementById('btnSalva').style.visibility = 'visible';
                document.getElementById('txtIntermediario').style.visibility = 'visible';
                document.getElementById('lblIntermediario').style.visibility = 'visible';
                break;
            case '2':
                document.getElementById('Label8').style.visibility = 'hidden';
                document.getElementById('txtSaldo').style.visibility = 'hidden';
                document.getElementById('Label11').style.visibility = 'hidden';
                document.getElementById('Label4').style.visibility = 'hidden';
                document.getElementById('txtIban').style.visibility = 'hidden';
                document.getElementById('sottotipologia').value = '1';
                document.getElementById('btnSalva').style.visibility = 'hidden';

                document.getElementById('txtIntermediario').style.visibility = 'hidden';
                document.getElementById('lblIntermediario').style.visibility = 'hidden';
                if (document.getElementById('Label14')) {
                    document.getElementById('Label14').style.visibility = 'hidden';
                }
                if (document.getElementById('L1')) {
                    document.getElementById('L1').style.visibility = 'hidden';
                }
                if (document.getElementById('L2')) {
                    document.getElementById('L2').style.visibility = 'hidden';
                }
                if (document.getElementById('L3')) {
                    document.getElementById('L3').style.visibility = 'hidden';
                }
                break;
        }
    }
}


function Verifica2() {
    //azzera();
    if (document.getElementById('tipologia').value == '1') {
        document.getElementById('cmbTitoli').style.visibility = 'visible';
        document.getElementById('Label6').style.visibility = 'visible';

        switch (document.getElementById('cmbTitoli').value) {
            case '0':
                document.getElementById('Label10').style.visibility = 'visible';
                document.getElementById('txtValTitoli').style.visibility = 'visible';
                document.getElementById('Label13').style.visibility = 'visible';
                document.getElementById('Label4').style.visibility = 'visible';
                document.getElementById('txtIban').style.visibility = 'visible';
                document.getElementById('sottotipologia').value = '0';
                document.getElementById('btnSalva').style.visibility = 'visible';
                document.getElementById('txtIntermediario').style.visibility = 'visible';
                document.getElementById('lblIntermediario').style.visibility = 'visible';
                break;
            case '1':
                document.getElementById('Label10').style.visibility = 'visible';
                document.getElementById('txtValTitoli').style.visibility = 'visible';
                document.getElementById('Label13').style.visibility = 'visible';
                document.getElementById('Label4').style.visibility = 'hidden';
                document.getElementById('txtIban').style.visibility = 'hidden';
                document.getElementById('Label4').style.visibility = 'visible';
                document.getElementById('txtIban').style.visibility = 'visible';
                document.getElementById('sottotipologia').value = '2';
                document.getElementById('btnSalva').style.visibility = 'visible';
                document.getElementById('txtIntermediario').style.visibility = 'visible';
                document.getElementById('lblIntermediario').style.visibility = 'visible';
                break;
            case '2':
                document.getElementById('Label10').style.visibility = 'visible';
                document.getElementById('txtValTitoli').style.visibility = 'visible';
                document.getElementById('Label13').style.visibility = 'visible';
                document.getElementById('Label4').style.visibility = 'hidden';
                document.getElementById('txtIban').style.visibility = 'hidden';
                document.getElementById('sottotipologia').value = '3';
                document.getElementById('btnSalva').style.visibility = 'visible';
                document.getElementById('txtIntermediario').style.visibility = 'visible';
                document.getElementById('lblIntermediario').style.visibility = 'visible';
                break;
            case '3':
                document.getElementById('Label10').style.visibility = 'hidden';
                document.getElementById('txtValTitoli').style.visibility = 'hidden';
                document.getElementById('Label13').style.visibility = 'hidden';
                document.getElementById('Label4').style.visibility = 'hidden';
                document.getElementById('txtIban').style.visibility = 'hidden';
                document.getElementById('btnSalva').style.visibility = 'hidden';
                document.getElementById('txtIntermediario').style.visibility = 'hidden';
                document.getElementById('lblIntermediario').style.visibility = 'hidden';
                if (document.getElementById('Label14')) {
                    document.getElementById('Label14').style.visibility = 'hidden';
                }
                if (document.getElementById('L1')) {
                    document.getElementById('L1').style.visibility = 'hidden';
                }
                if (document.getElementById('L2')) {
                    document.getElementById('L2').style.visibility = 'hidden';
                }
                if (document.getElementById('L3')) {
                    document.getElementById('L3').style.visibility = 'hidden';
                }
        }
        
    }
}

function Verifica3() {
    if (document.getElementById('tipologia').value == '2') {
        document.getElementById('cmbPartecipazioni').style.visibility = 'visible';
        document.getElementById('Label7').style.visibility = 'visible';
        document.getElementById('Label12').style.visibility = 'visible';
        document.getElementById('txtValPartecipazioni').style.visibility = 'visible';
        document.getElementById('Label9').style.visibility = 'visible';
        document.getElementById('txtIntermediario').style.visibility = 'visible';
        document.getElementById('lblIntermediario').style.visibility = 'visible';

        switch (document.getElementById('cmbPartecipazioni').value) {
            case '0':
                document.getElementById('sottotipologia').value = '0';
                document.getElementById('btnSalva').style.visibility = 'visible';
                break;
            case '1':
                document.getElementById('sottotipologia').value = '4';
                document.getElementById('btnSalva').style.visibility = 'visible';
                break;
            case '2':
                document.getElementById('sottotipologia').value = '5';
                document.getElementById('btnSalva').style.visibility = 'visible';
                break;
            case '3':
                document.getElementById('sottotipologia').value = '6';
                document.getElementById('btnSalva').style.visibility = 'visible';
                break;
            case '4':
                document.getElementById('sottotipologia').value = '7';
                document.getElementById('btnSalva').style.visibility = 'visible';
                break;
            case '5':
                document.getElementById('Label12').style.visibility = 'hidden';
                document.getElementById('txtValPartecipazioni').style.visibility = 'hidden';
                document.getElementById('Label9').style.visibility = 'hidden';
                document.getElementById('btnSalva').style.visibility = 'hidden';
                document.getElementById('txtIntermediario').style.visibility = 'hidden';
                document.getElementById('lblIntermediario').style.visibility = 'hidden';
                if (document.getElementById('Label14')) {
                    document.getElementById('Label14').style.visibility = 'hidden';
                }
                if (document.getElementById('L1')) {
                    document.getElementById('L1').style.visibility = 'hidden';
                }
                if (document.getElementById('L2')) {
                    document.getElementById('L2').style.visibility = 'hidden';
                }
                if (document.getElementById('L3')) {
                    document.getElementById('L3').style.visibility = 'hidden';
                }
                break;
        }
    }
}


function Chiudi()
{
window.close();
}



</script>
</html>
