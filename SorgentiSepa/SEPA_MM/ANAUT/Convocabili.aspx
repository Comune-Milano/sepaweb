<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Convocabili.aspx.vb" Inherits="ANAUT_Convocabili" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    var Selezionato;
</script>

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

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
		
	    <style type="text/css">
            #contenitore
            {
                top: 61px;
            }
            .style1
            {
                width: 93px;
            }
        </style>
        <title>Convocabili</title>
	</head>
	<body bgcolor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Convocabili</strong></span><br />
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
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                        
                        <asp:HiddenField ID="H1" runat="server" Value="" />
                    </td>
                </tr>
            </table>
            &nbsp;<img alt="Torna alla pagina principale" 
                        src="../NuoveImm/Img_Home.png" id="imgEliminafiliale" 
                        
                style="cursor:pointer;left: 598px; position: absolute; top: 484px; height: 20px;" 
                onclick="PaginaHome();"/>&nbsp;
            <asp:ImageButton ID="btnProcedi" runat="server" 
                style="position:absolute; top: 484px; left: 463px; right: 945px;" 
                ImageUrl="~/NuoveImm/Img_Visualizza.png" TabIndex="20" />
                <div id="contenitore" 
                
                
                
                
                style="position: absolute; width: 642px; height: 396px; left: 14px; overflow: auto;">
                    <table style="width:100%;" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="style1" bgcolor="#FFFFCE">
                    <asp:Label ID="Label10" runat="server" Text="Anagrafe Utenza" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            </td>
                            <td style="border: 1px solid #000080" bgcolor="#FFFFCE">
                                <asp:DropDownList ID="cmbAU" runat="server" AutoPostBack="True" 
                                    CausesValidation="True" Font-Names="arial" Font-Size="8pt" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="border: 2px solid #000080">
                            <td valign="top" class="style1" bgcolor="#EBEBEB">
                    <asp:Label ID="Label11" runat="server" Text="Struttura/e" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            </td>
                            <td valign="top" style="border: 1px solid #000080" bgcolor="#EBEBEB">
                                <asp:CheckBoxList ID="chListStrutture" runat="server" AutoPostBack="True" 
                                    CausesValidation="True" Font-Names="arial" Font-Size="8pt" 
                                    RepeatColumns="3" TabIndex="2">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr style="border: 2px solid #0000FF">
                            <td valign="top" class="style1" bgcolor="#FFDFD5">
                    <asp:Label ID="Label12" runat="server" Text="Sportello/i" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            </td>
                            <td valign="top" style="border: 1px solid #000080" bgcolor="#FFDFD5">
                                <asp:CheckBoxList ID="chListSportelli" runat="server" Font-Names="arial" 
                                    Font-Size="8pt" RepeatColumns="3" TabIndex="3">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td>
                                &nbsp;&nbsp;</td>
                        </tr>
                    </table>
                        <table style="width:100%;">
                            <tr>
                                <td>
                    <asp:Label ID="Label13" runat="server" Text="Num.Comp.Nucleo" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td valign="middle">
                    <asp:Label ID="Label23" runat="server" Text="da" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            &nbsp;<asp:DropDownList ID="cmbCompDa" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="4">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem Selected="True">--</asp:ListItem>
                                    </asp:DropDownList>
&nbsp;<asp:Label ID="Label24" runat="server" Text="a" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            &nbsp;<asp:DropDownList ID="cmbCompA" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        TabIndex="5">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem Selected="True">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                    <asp:Label ID="Label14" runat="server" Text="Comp. &lt;15 anni " Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmb15" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        TabIndex="6">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                    <asp:Label ID="Label15" runat="server" Text="Comp.&gt;65 anni" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmb65" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        TabIndex="7">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                    <asp:Label ID="Label16" runat="server" Text="Disabili  tra 66 e 99%" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmb6699" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="8">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                    <asp:Label ID="Label17" runat="server" Text="Disab. 100% NO Acc." Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmb100Non" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="9">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                    <asp:Label ID="Label19" runat="server" Text="Disab.  100% Acc." Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmb100Acc" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="10">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                    <asp:Label ID="Label20" runat="server" Text="Reddito Prev.Dip." Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbDip" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        TabIndex="11">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                    <asp:Label ID="Label21" runat="server" Text="Patrimonio Immobiliare" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbImmobiliare" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="12">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                    <asp:Label ID="Label25" runat="server" Text="Tipo Contratto" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipoContratto" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="13">
                                    </asp:DropDownList>
                                </td>
                                <td>
                    <asp:Label ID="Label26" runat="server" Text="Sindacato Riferimento" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbSindacato" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="14">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                    <asp:Label ID="Label27" runat="server" Text="Data Stipula" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                    <asp:Label ID="Label28" runat="server" Text="da" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                                                       &nbsp;<asp:TextBox ID="txtStipulaDal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113;"
                            TabIndex="15" ToolTip="gg/mm/aaaa" Width="62px" Font-Names="arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                runat="server" ControlToValidate="txtStipulaDal"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            TabIndex="-1" 
                            
                            
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            
                            &nbsp;&nbsp;<asp:Label ID="Label29" runat="server" Text="a" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            &nbsp;<asp:TextBox ID="txtStipulaAl" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; "
                            TabIndex="16" ToolTip="gg/mm/aaaa" Width="62px" Font-Names="arial" Font-Size="8pt"></asp:TextBox>  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                             TabIndex="-1"                             
                            
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                    <asp:Label ID="Label30" runat="server" Text="Data Sloggio" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                    <asp:Label ID="Label31" runat="server" Text="da" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>&nbsp;<asp:TextBox ID="txtSloggioDal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113;"
                            TabIndex="17" ToolTip="gg/mm/aaaa" Width="62px" Font-Names="arial" Font-Size="8pt"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                runat="server" ControlToValidate="txtSloggioDal"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            TabIndex="-1" 
                            
                            
                
                
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            
                                    &nbsp;&nbsp;<asp:Label ID="Label32" runat="server" Text="a" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            &nbsp;<asp:TextBox ID="txtSloggioAl" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; "
                            TabIndex="18" ToolTip="gg/mm/aaaa" Width="62px" Font-Names="arial" Font-Size="8pt"></asp:TextBox>  &nbsp;<asp:RegularExpressionValidator 
                                        ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtSloggioAl"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                             TabIndex="-1"                             
                            
                
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                    <asp:Label ID="Label33" runat="server" Text="Tutore Straordinario" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTutore" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" TabIndex="19">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                    </table>
                        </div>
            
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
        </asp:UpdatePanel>

                        <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
    <script type="text/javascript" language="javascript">
        function PaginaHome() {
            document.location.href = 'pagina_home.aspx';
        }

        function Verifica() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                window.open('ElencoAssegnatari.aspx?ID=' + document.getElementById('LBLID').value, '', '');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare una Anagrafe Utenza dalla lista!');

            }
        }

        function Messaggio(TestoMessaggio) {

            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }
    </script>    
    </form>    

	</body>
     <script language="javascript" type="text/javascript">
         document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
</html>
