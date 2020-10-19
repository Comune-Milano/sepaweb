<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DispAler.aspx.vb" Inherits="ASS_DispAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Disponibilità Gestore</title>
</head>
<script type="text/javascript">
	    //document.onkeydown = $onkeydown;


	    function CompletaData(e, obj) {
	        // Check if the key is a number
	        var sKeyPressed;

	        sKeyPressed = (window.event) ? event.keyCode : e.which;

	        if (sKeyPressed < 48 || sKeyPressed > 57) {
	            if (sKeyPressed != 8 && sKeyPressed != 0) {
	                // don't insert last non-numeric character
	                if (window.event) {
	                    event.keyCode = 0;
	                }
	                else {
	                    e.preventDefault();
	                }
	            }
	        }
	        else {
	            if (obj.value.length == 2) {
	                obj.value += "/";
	            }
	            else if (obj.value.length == 5) {
	                obj.value += "/";
	            }
	            else if (obj.value.length > 9) {
	                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
	                if (selText.length == 0) {
	                    // make sure the field doesn't exceed the maximum length
	                    if (window.event) {
	                        event.keyCode = 0;
	                    }
	                    else {
	                        e.preventDefault();
	                    }
	                }
	            }
	        }
	    }


</script>
<body>
    <form id="form1" runat="server" defaultbutton="imgSalva" defaultfocus="cmbZona">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; text-align: left;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Disponibilità
                        U.I.
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 108; left: 539px; position: absolute; top: 432px" 
                        ToolTip="Home" TabIndex="22" /><asp:ImageButton ID="imgSalva" 
                        runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                            Style="z-index: 108; left: 456px; position: absolute; top: 432px" 
                        ToolTip="Salva" TabIndex="21" />
                        &nbsp;</strong></span><br />
                    <br />
                    <br />
                    <table width="95%">
                        <tr>
                            <td style="width: 114px; text-align: left;">
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 23px; position: static; top: 397px" Text="Codice" Width="37px"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCodice" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="20"
                                    TabIndex="1" Width="159px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 114px; text-align: left;">
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 94px" Text="Zona Decentramento"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbZona" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="2" Width="80px">
                                    <asp:ListItem Selected="True">01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem Value="--">--</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 114px; text-align: left;">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 116px" Text="Disponibile Dal"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtDisponibile" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="10" TabIndex="3" Width="73px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDisponibile"
                                    Display="Dynamic" ErrorMessage="gg/mm/aaaa" Font-Names="arial" Font-Size="8pt"
                                    TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 114px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 98%">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 160px" Text="Tipo"></asp:Label></td>
                            <td style="width: 244px; text-align: left;">
                                <asp:DropDownList ID="cmbTipo" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="4" Width="237px">
                                </asp:DropDownList></td>
                            <td style="width: 3px">
                                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 164px; position: static; top: 160px" Text="N.Locali"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:TextBox ID="txtLocali" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="5" Width="40px"></asp:TextBox></td>
                            <td style="width: 62px; text-align: left;">
                                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 249px; position: static; top: 160px" Text="Sup.mq"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtSuperficie" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="8" TabIndex="6" Width="44px" ToolTip="Indicare la superficie comprensiva di , come separatore e due cifre decimali"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSuperficie"
                                    ErrorMessage="Formato 0,00" Font-Bold="False" Font-Names="ARIAL"
                                    Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b" Display="Dynamic"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 182px" Text="Indirizzo"></asp:Label></td>
                            <td style="width: 244px; text-align: left;">
                                <asp:DropDownList ID="cmbTipoVia" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="7" Width="77px">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtIndirizzo" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="20" TabIndex="8" Width="150px"></asp:TextBox></td>
                            <td style="width: 3px">
                                <asp:Label ID="Label10" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 96px; position: static; top: 182px" Text="Civico"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:TextBox ID="txtCivico" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="9" Width="40px"></asp:TextBox></td>
                            <td style="width: 62px; text-align: left;">
                                <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 182px; position: static; top: 182px" Text="Comune"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TXTCOMUNE" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="20"
                                    TabIndex="10" Width="99px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label11" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Interno"></asp:Label></td>
                            <td style="width: 244px; text-align: left;">
                                <asp:TextBox ID="txtInterno" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="11" Width="40px"></asp:TextBox></td>
                            <td style="width: 3px">
                                <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 96px; position: static; top: 204px" Text="Piano"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:DropDownList ID="cmbPiano" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="12" Width="125px">
                                </asp:DropDownList></td>
                            <td style="width: 62px; text-align: left;">
                                <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 177px; position: static; top: 204px" Text="Scala"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtscala" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="13" Width="40px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Foglio"></asp:Label></td>
                            <td style="width: 244px; text-align: left;">
                                <asp:TextBox ID="txtfoglio" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="14" Width="40px"></asp:TextBox></td>
                            <td style="width: 3px">
                                <asp:Label ID="Label15" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Mappale"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:TextBox ID="txtmappale" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="15" Width="40px"></asp:TextBox></td>
                            <td style="width: 62px; text-align: left;">
                                <asp:Label ID="Label16" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Sub"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtsub" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="16" Width="40px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
                    <table width="95%">
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chAscensore" runat="server" Font-Names="arial" Font-Size="8pt"
                                    Style="position: static" TabIndex="17" Text="Con Ascensore" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chBar" runat="server" Font-Names="arial" Font-Size="8pt" Style="position: static"
                                    TabIndex="18" Text="Alloggio con Barriere Architettoniche" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chHandicap" runat="server" Font-Names="arial" Font-Size="8pt" Style="position: static"
                                    TabIndex="19" Text="Destinato a portatori di handicap motorio" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="#C00000"
                                    Visible="False" Width="618px"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <asp:Image ID="imgElenco" runat="server" ImageUrl="~/NuoveImm/Img_ElencoContratti.png"
                        Style="left: 5px; position: absolute; top: 434px" 
                        ToolTip="Elenco Alloggi Gestore" TabIndex="20" />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
