<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriGestCredito.aspx.vb"
    Inherits="Contratti_ParametriGestCredito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        var Selezionato;
        var Selezionato1;
    </script>
    <style type="text/css">
        .CSSmaiuscolo
        {
            text-transform: uppercase;
        }
        .style1
        {
            width: 400px;
        }
        .style2
        {
            height: 24px;
        }
    </style>
</head>
<body style="width: 800px; left: 0px; top: -15px; background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    height: 540px;">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td style="height: 10px" valign="bottom">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Tabella Parametri
                    - Gestione Credito </strong></span>
            </td>
        </tr>
    </table>
    <div id="InseriMotivazione" style="margin: 0px; width: 100%; background-image: url('../NuoveImm/sfondo_grigio.png');
        height: 100%; position: fixed; top: 5px; left: 5px; z-index: 200; visibility: hidden;">
        <div style="position: fixed; top: 50%; left: 50%; width: 492px; height: 320px; margin-left: -246px;
            margin-top: -160px; background-image: url('../ImmDiv/SfondoDim1.jpg'); z-index: 300;"
            align="center">
            <table width="460px" style="height: 250px; text-align: center; margin-left: 10px;
                z-index: 400;" align="center">
                <tr>
                    <td style="height: 19px; text-align: left" align="center" valign="middle" colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px; text-align: left" align="center" valign="top" colspan="2">
                        <asp:Label ID="lbl_titMotiv" runat="server" Text="Modifica mensilità" Font-Bold="True"
                            Font-Names="Arial" Font-Size="12pt" Width="386px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="font-size: 10pt; font-family: Arial">Num.mesi</span>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtDurata" runat="server" Font-Names="Arial" Font-Size="10pt" Width="150px"
                            MaxLength="400" TabIndex="1" Font-Bold="True" CssClass="CSSmaiuscolo"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                            ControlToValidate="txtDurata" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                            Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+" ForeColor="Red"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" height="50px">
                        &nbsp;
                    </td>
                    <td style="text-align: right">
                        <asp:ImageButton ID="btn_inserisci" runat="server" TabIndex="2" Style="height: 16px"
                            ImageUrl="../NuoveImm/Img_InserisciVal.png" />
                        <asp:ImageButton ID="btn_chiudi" runat="server" ImageUrl="../NuoveImm/Img_AnnullaVal.png"
                            OnClientClick="document.getElementById('InseriMotivazione').style.visibility='hidden';" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <table style="width: 96%; margin-left: 15px;">
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 80%">
                    <div style="height: 119px; z-index: 100;">
                        <asp:DataGrid ID="DataGridParam" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                            Font-Size="8pt" Width="97%" PageSize="15" BackColor="White" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            GridLines="None" ShowFooter="True" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Navy" Wrap="False"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_MESI" HeaderText="NUM.MESI">
                                    <HeaderStyle Width="70%" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="30%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" />
                            <PagerStyle Mode="NumericPages" />
                        </asp:DataGrid>
                    </div>
                </td>
                <td style="width: 20%" valign="top">
                    <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="../NuoveImm/Img_Modifica.png"
                        ToolTip="Modifica Motivazione" OnClientClick="   if (document.getElementById('LBLID').value != 0) { document.getElementById('InseriMotivazione').style.visibility = 'visible';  document.getElementById('Modificato').value='2';} else{  alert('Nessuna riga selezionata!!'); document.getElementById('Modificato').value='0';}" />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="11pt"
                        Width="624px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="width: 80%;">
                <div id="contenitore" 
                        
                        
                        style="position: relative; visibility: visible; overflow: auto; top: 0px; left: 0px; width: 689px;">
                        <asp:DataGrid ID="DataGridParam0" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                            Font-Size="8pt" Width="150%" PageSize="15" BackColor="White" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            GridLines="None" ShowFooter="True" BorderColor="Navy" 
                        BorderStyle="Solid" BorderWidth="1px">
                            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Navy" Wrap="False"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VOCE_BP" HeaderText="VOCE B.P. DEP.CAUZIONALE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VOCE_BP_INTERESSI" HeaderText="VOCE B.P. INTERESSI DEP.CAUZ.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VOCE_BP_CREDITI" HeaderText="VOCE B.P. CREDITO GEN.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_INIZIO_VALIDITA" HeaderText="INIZIO VAL.">
                                    <HeaderStyle Width="70%" Font-Bold="True" />
                                    <HeaderStyle Font-Bold="true" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="10%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_FINE_VALIDITA" HeaderText="FINE VAL.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_DOC_CONT" HeaderText="TIPO DOC. CONTABILE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Width="30%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Width="30%"/>
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" />
                            <PagerStyle Mode="NumericPages" />
                        </asp:DataGrid>
                        </div>
                    </td>
                <td style="width: 20%" valign="top">
                    <img alt="Aggiungi" 
                                            src="../NuoveImm/Img_Aggiungi.png" 
                        style="cursor: pointer;" 
                        id="IMG1" onclick="document.getElementById('TextBox1').value='1';document.getElementById('InserimentoP').style.visibility = 'visible';" /><br />
                    <br />
                    <asp:ImageButton ID="btnModifica0" runat="server" ImageUrl="../NuoveImm/Img_Modifica.png"
                        ToolTip="Modifica Motivazione" 
                        OnClientClick="   if (document.getElementById('LBLID1').value != 0) { document.getElementById('InserimentoP').style.visibility = 'visible';  document.getElementById('TextBox1').value='2';} else{  alert('Nessuna riga selezionata!!'); document.getElementById('TextBox1').value='0';}" />
                    <br />
                    <br />

   

                    <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                        ToolTip="Elimina la voce selezionata" onclientclick="Sicuro();" />

   

                </td>
            </tr>
            <tr>
                <td style="width: 80%;">
                    <asp:TextBox ID="txtmia0" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="11pt"
                        Width="624px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                    </td>
                <td style="width: 20%" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 80%;">
                    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                </td>
                <td style="width: 20%" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 80%; height: 60px;" valign="bottom" align="right">
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                        ToolTip="Home" />
                </td>
                <td style="width: 20%" valign="top">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
        <div id="InserimentoP" 
            
        
        
        
        
        
        style="left: 0px; width: 100%; position: absolute;
            top: 0px; height: 100%; text-align: left; background-repeat: no-repeat; visibility: visible; background-image: url('../ImmDiv/SfondoDiv.png'); z-index: 200;">
            <span style="font-family: Arial"></span>
            <br />
            <br />
            <table border="0" cellpadding="1" cellspacing="1" style="left: 126px; width: 532px;
                position: absolute; top: 128px; background-color: #FFFFFF; z-index: 200;">
                <tr>
                    <td style="text-align: left" class="style1">
                        <strong><span style="font-family: Arial">Gestione</span></strong></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <strong><span style="font-family: Arial">Crediti</span></strong></td>
                </tr>
                <tr>
                    <td style="text-align: left" class="style1">
                        &nbsp;</td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left" class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Struttura Competente</span></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <asp:DropDownList ID="cmbstruttura" runat="server" Width="380px" TabIndex="100" Enabled="false">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="text-align: left" class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Voce B.P. Dep. Cauz.</span></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <asp:DropDownList ID="cmbVoceBP" runat="server" Width="380px" TabIndex="101">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="text-align: left" class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Voce B.P. Res.Crediti</span> </td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <asp:DropDownList ID="cmbVoceBP0" runat="server" Width="380px" TabIndex="101">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="text-align: left" class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Voce B.P. Int. D.C.</span> </td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <asp:DropDownList ID="cmbVoceBP1" runat="server" Width="380px" TabIndex="101">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="text-align: left" class="style1" valign="top">
                        <span style="font-size: 10pt; font-family: Arial">Doc.Contabile</span></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <asp:DropDownList ID="cmbDocContabile" runat="server" Width="380px" 
                            TabIndex="102" Enabled="false">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Fornitore</span></td>
                    <td style="width: 469px; height: 19px">
                        <asp:DropDownList ID="cmbFornitore" runat="server" Width="380px" TabIndex="103">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Inizio Validità</span></td>
                    <td style="width: 469px; height: 19px">
                        <asp:TextBox ID="txtInizio" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" MaxLength="10" 
                            Style="z-index: 200;" ToolTip="gg/mm/aaaa" Width="70px" TabIndex="104"></asp:TextBox>
                    &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtInizio"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    TabIndex="-1" 
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Fine Validità</span></td>
                    <td style="width: 469px; height: 19px">
                        <asp:TextBox ID="txtFine" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" MaxLength="10" 
                            Style="z-index: 200;" ToolTip="gg/mm/aaaa" Width="70px" TabIndex="105"></asp:TextBox>
                    &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtFine"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    TabIndex="-1" 
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td align="right" style="width: 469px; height: 19px; text-align: right">
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 110%; text-align: left;">
                            <tr>
                                <td style="text-align: center">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="img_InserisciSchema" runat="server" 
                                        ImageUrl="~/NuoveImm/Img_SalvaVal.png" TabIndex="106" />&nbsp;<asp:ImageButton 
                                        ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                            OnClientClick="document.getElementById('TextBox1').value='0';"
                            TabIndex="107" ToolTip="Annulla operazione" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    
            
        </div>

    <asp:HiddenField ID="LBLID" runat="server" Value="0" />
    <asp:HiddenField ID="LBLID1" runat="server" Value="0" />
    <asp:HiddenField ID="Modificato" runat="server" />
    <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
    <asp:HiddenField ID="eliminato" runat="server" Value="0" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <script language="javascript" type="text/javascript">
        //        myOpacity = new fx.Opacity('InseriMotivazione', { duration: 200 });

        if ((document.getElementById('Modificato').value != '2') && (document.getElementById('Modificato').value != '1')) {
            document.getElementById('InseriMotivazione').style.visibility = 'hidden';

        } else {

            document.getElementById('InseriMotivazione').style.visibility = 'visible';

        }

        if ((document.getElementById('TextBox1').value != '2') && (document.getElementById('TextBox1').value != '1')) {
            document.getElementById('InserimentoP').style.visibility = 'hidden';

        } else {

            document.getElementById('InserimentoP').style.visibility = 'visible';

        }
        function Sicuro() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione, Eliminare la voce selezionata?");
            if (chiediConferma == true) {
                document.getElementById('eliminato').value = '1';
            }
            else {
                document.getElementById('eliminato').value = '0';
            }
        }
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

    </form>
</body>
</html>
