<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EventiPatrimoniali.aspx.vb"
    Inherits="Condomini_EventiPatrimoniali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <title>Eventi Patrimoniali</title>
    <style type="text/css">
        .style1
        {
            color: #000000;
            font-family: Arial;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 95%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF; visibility: visible;">
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
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <script type="text/javascript">

        function PulisciCampi() {
            document.getElementById('txtMil_Pro').value = ""
            document.getElementById('txtPosBil').value = ""
            document.getElementById('txtAsc').value = ""
            document.getElementById('txtMil_Compro').value = ""
            document.getElementById('txtMil_Gest').value = ""
            document.getElementById('txt_Mil_Risc').value = ""
            document.getElementById('txtNote').value = ""
            document.getElementById('TextBox4').value = "0"
            document.getElementById('IdCondominio').value = "0"
            document.getElementById('IdUnita').value = "0"
            document.getElementById('id').value = "0"
        }
        function AutoDecimal(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(4)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        function AutoDecimal2(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        function cerca() {
            if (document.all) {
                finestra = showModelessDialog('Find.htm', window, 'dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                finestra.focus
                finestra.document.close()
            }
            else if (document.getElementById) {
                self.find()
            }
            else window.alert('Il tuo browser non supporta questo metodo')
        }
        function ApriComSloggio() {
            window.showModalDialog('ComunicazSloggio.aspx?IDCONT=' + document.getElementById('idContratto').value + '&IDAVVISO=' + document.getElementById('id').value + '&IDCOND=' + document.getElementById('IdCondominio').value, 'window', 'status:no;dialogWidth:500px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
        }
    </script>
    <form id="form1" runat="server">
    <asp:HiddenField ID="TextBox4" runat="server" />
    <div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Eventi Patrimoniali
            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Height="18px" Style="z-index: 800; left: 192px; position: absolute;
                top: 38px" Visible="False" Width="548px"></asp:Label>
            <br />
            <img id="imgCambiaAmm" alt="Ricerca Rapida" onclick="cerca();" src="Immagini/Search_16x16.png"
                style="left: 734px; cursor: pointer; top: 78px; right: 179px; position: absolute;" />
        </span></strong>
    </div>
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <div style="left: 11px; overflow: auto; width: 718px; position: absolute; top: 56px;
            height: 460px">
            <asp:DataGrid ID="DataGridBManager" runat="server" AutoGenerateColumns="False" BackColor="White"
                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="200" Style="z-index: 105; left: 193px; top: 54px" Width="97%" 
                AllowPaging="True">
                <PagerStyle Mode="NumericPages" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <ItemStyle ForeColor="Black" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONDOMINIO" HeaderText="ID_CONDOMINIO" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA" HeaderText="DATA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EVENTO" HeaderText="EVENTO" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="CONDOMINIO">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONDOMINIO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="EVENTO">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EVENTO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="COD UNITA IMMOBILIARE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="DATA" HeaderText="DATA" ReadOnly="True"></asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="#0000C0" />
            </asp:DataGrid>
        </div>
        <div id="divInquilini" style="border: thin none #3366ff; z-index: 201; left: 0px;
            background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); visibility: hidden;
            vertical-align: top; width: 802px; margin-right: 10px; position: absolute; top: 0px;
            height: 582px; background-color: #dedede; text-align: left">
            <table style="width: 98%;">
                <tr>
                    <td style="font-size: 6pt">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong><span style="font-size: 12pt; color: #801f1c; font-family: Arial">
                            <asp:Label ID="lblTitolo" runat="server" Text="Condominio: NameCond"></asp:Label>
                        </span></strong>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 8pt">
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 8pt; background-color: #FFFFFF;">
                        <table style="border-right: lightblue thin solid; border-top: lightblue thin solid;
                            z-index: 120; border-left: lightblue thin solid; width: 100%; border-bottom: lightblue thin solid;
                            border-color: #000080;">
                            <tr>
                                <td style="vertical-align: top; width: 140px; height: 16px; text-align: right">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="107px">UNITA IMMOBILIARE :</asp:Label>
                                </td>
                                <td style="width: 238px; height: 16px">
                                    <asp:TextBox ID="txtCodUI" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" ReadOnly="True" Style="z-index: 600;
                                        left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="181px"></asp:TextBox>
                                </td>
                                <td>
                                    <strong><span>
                                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="70px">TIPOLOGIA</asp:Label>
                                    </span></strong>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:TextBox ID="TxtTipologia" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" ReadOnly="True"
                                            Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="197px"></asp:TextBox>
                                    </span></strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 140px; height: 16px; text-align: right">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="84px">INDIRIZZO U.I.</asp:Label>
                                </td>
                                <td colspan="3" style="width: 248px; height: 16px">
                                    <asp:TextBox ID="txtIndirizzoUI" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" ReadOnly="True"
                                        Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="313px"></asp:TextBox>
                                </td>
                                <td style="vertical-align: top; width: 131px; height: 16px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: middle; width: 140px; height: 16px; text-align: right">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="27px">Interno</asp:Label>
                                </td>
                                <td style="vertical-align: top; width: 238px; height: 16px; text-align: left" colspan="4">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                                    <asp:TextBox ID="txtInterno" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        EnableTheming="True" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" Font-Strikeout="False"
                                                        Font-Underline="False" MaxLength="10" ReadOnly="True" Style="z-index: 600; left: 10px;
                                                        top: 72px; text-align: left" TabIndex="-1" Width="65px"></asp:TextBox>
                                                </span></strong>
                                            </td>
                                            <td>
                                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="27px">Scala</asp:Label>
                                                </span></strong>
                                            </td>
                                            <td>
                                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                                    <asp:TextBox ID="txtScala" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="z-index: 600;
                                                        left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="63px"></asp:TextBox>
                                                </span></strong>
                                            </td>
                                            <td>
                                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="33px">Piano</asp:Label>
                                                </span></strong>
                                            </td>
                                            <td>
                                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                                    <asp:TextBox ID="txtPiano" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                                                        Font-Italic="False" Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" ReadOnly="True"
                                                        Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="88px"></asp:TextBox>
                                                </span></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 8pt">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 140px; height: 16px">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="83px">INTESTATARIO</asp:Label>
                                </td>
                                <td style="width: 252px; height: 16px">
                                    <asp:DropDownList ID="cmbIntestatari" runat="server" BackColor="White" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="9pt" Style="right: 481px; left: 9px; top: 109px"
                                        TabIndex="1" Width="294px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 131px; height: 16px">
                                    &nbsp;<asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px"
                                        Width="106px">STATO RAPPORTO</asp:Label>
                                </td>
                                <td style="width: 131px; height: 16px">
                                    <asp:TextBox ID="txtStatoRapp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True"
                                        Style="z-index: 605; left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="156px"></asp:TextBox>
                                </td>
                            </tr>
    </span>
        <tr>
            <td colspan="4">
                <table>
                    <tr>
                        <td class="style1">
                            DATA DECORRENZA
                        </td>
                        <td>
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:TextBox ID="txtDataDecorrenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" Style="z-index: 600;
                                    left: 10px; top: 72px; text-align: left" TabIndex="4" Width="78px"></asp:TextBox>
                            </span></strong>
                        </td>
                        <td class="style1">
                            DATA SLOGGIO
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataSloggio" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" Style="z-index: 600;
                                left: 10px; top: 72px; text-align: left" TabIndex="4" Width="78px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnComunicazione" runat="server" OnClientClick="ApriComSloggio();return false;"
                                ImageUrl="~/Condomini/Immagini/ImgDatiChiusura.png" ToolTip="Dati per la chiusura del contratto" />
                        </td>
                    </tr>
                </table>
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            </td>
            <td style="width: 252px; height: 16px">
                &nbsp;
            </td>
            <td style="width: 131px; height: 16px">
                &nbsp;
            </td>
            <td style="width: 131px; height: 16px">
                &nbsp;
            </td>
        </tr>
        </table> </span></td> </tr>
        <tr>
            <td style="font-size: 8pt">
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: top; width: 1%; height: 24px; text-align: left">
                            &nbsp;<asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px">Recapito</asp:Label>
                        </td>
                        <td style="vertical-align: top; width: 11%; height: 24px; text-align: left">
                            <asp:DropDownList ID="cmbTipoCor" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" TabIndex="2" Width="90px">
                            </asp:DropDownList>
                        </td>
                        <td style="vertical-align: top; width: 11%; height: 24px; text-align: left">
                            <asp:TextBox ID="txtVia" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" Style="z-index: 600; left: 10px;
                                top: 72px; text-align: left" TabIndex="3" Width="198px"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="25px">Civ.</asp:Label>
                        </td>
                        <td style="vertical-align: top; width: 3px; text-align: left">
                            <asp:TextBox ID="txtCivico" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" Style="z-index: 600;
                                left: 10px; top: 72px; text-align: left" TabIndex="4" Width="78px"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top; width: 7px; text-align: left">
                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="27px">Cap</asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left">
                            <asp:TextBox ID="txtCap" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" Style="z-index: 600; left: 10px;
                                top: 72px; text-align: left" TabIndex="5" Width="78px"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top; width: 331%; height: 24px; text-align: left">
                            <asp:TextBox ID="txtLocalità" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" ReadOnly="True"
                                Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="6" Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1%; height: 24px; text-align: left">
                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="52px">Presso</asp:Label>
                        </td>
                        <td colspan="5" style="vertical-align: top; width: 11%; height: 24px; text-align: left">
                            <asp:TextBox ID="txtPresso" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" ReadOnly="True"
                                Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="7" Width="294px"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top; width: 11%; height: 24px; text-align: left">
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 8pt">
                <table style="width: 90%">
                    <tr>
                        <td style="width: 116px; height: 21px">
                        </td>
                        <td style="width: 207px; height: 21px">
                        </td>
                        <td style="width: 167px; height: 21px">
                        </td>
                        <td style="width: 918px; height: 21px">
                        </td>
                        <td style="width: 918px; height: 21px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 21px">
                            <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="149px">Pos. su Bilancio Condominiale</asp:Label>
                        </td>
                        <td style="width: 207px; height: 21px">
                            <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px">Millesimi Proprietà</asp:Label>
                        </td>
                        <td style="width: 167px; height: 21px">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="104px">Millesimi Ascensore</asp:Label>
                        </td>
                        <td style="width: 918px; height: 21px">
                            <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="104px">Millesimi Presenza</asp:Label>
                        </td>
                        <td style="width: 918px; height: 21px">
                            <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="104px">Num.Persone</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 116px; height: 20px; text-align: left">
                            <asp:TextBox ID="txtPosBil" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 600; left: 10px;
                                top: 72px; text-align: right" TabIndex="8" Width="75px"></asp:TextBox>
                        </td>
                        <td style="width: 207px; height: 20px">
                            <asp:TextBox ID="txtMil_Pro" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="9" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMil_Pro"
                                Display="Dynamic" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                SetFocusOnError="True" Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                                ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 167px; height: 20px; text-align: left">
                            <asp:TextBox ID="txtAsc" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                Font-Size="8pt" MaxLength="30" Style="z-index: 605; left: 10px; top: 72px; text-align: right"
                                TabIndex="10" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAsc"
                                Display="Dynamic" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                                ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 20px; text-align: left">
                            <asp:TextBox ID="txtMillPres" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="21" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtMillPres"
                                Display="Dynamic" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                                ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 20px; text-align: left">
                            <asp:TextBox ID="txtnumPers" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Enabled="False" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Black" MaxLength="30"
                                Style="z-index: 605; left: 10px; top: 72px; text-align: right" TabIndex="21"
                                Width="75px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 20px">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="124px">Millesimi Comproprietà</asp:Label>
                        </td>
                        <td style="width: 207px; height: 20px">
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="86px">Millesimi Gestione</asp:Label>
                        </td>
                        <td style="width: 167px; height: 20px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="116px">Millesimi Riscaldamento</asp:Label>
                        </td>
                        <td style="width: 918px; height: 20px">
                            <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="116px">Addebito Singolo</asp:Label>
                        </td>
                        <td style="width: 918px; height: 20px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 116px; height: 22px; text-align: left">
                            <asp:TextBox ID="txtMil_Compro" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="11" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMil_Compro"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 207px; height: 22px">
                            <asp:TextBox ID="txtMil_Gest" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="12" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMil_Gest"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 167px; height: 22px; text-align: left">
                            <asp:TextBox ID="txt_Mil_Risc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="13" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_Mil_Risc"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 22px; text-align: left">
                            <asp:TextBox ID="txtAddebito" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="14" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtAddebito"
                                ErrorMessage="N,00" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 22px; text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 22px">
                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="30px">Note</asp:Label>
                        </td>
                        <td style="width: 207px; height: 22px">
                        </td>
                        <td style="width: 167px; height: 22px">
                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="101px">Altri Condomini</asp:Label>
                        </td>
                        <td style="width: 918px; height: 22px">
                        </td>
                        <td style="width: 918px; height: 22px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtNote" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                Font-Size="8pt" Height="70px" MaxLength="100" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: left" TabIndex="16" TextMode="MultiLine" Width="271px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:ListBox ID="ListCondomini" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Height="80px" Style="overflow: auto; border-collapse: separate" TabIndex="-1"
                                Width="96%"></asp:ListBox>
                        </td>
                        <td style="vertical-align: top; width: 837px; height: 21px; text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 22px">
                        </td>
                        <td style="width: 207px; height: 22px">
                        </td>
                        <td style="width: 167px; height: 22px">
                        </td>
                        <td style="width: 918px; height: 22px">
                            <asp:ImageButton ID="btnSalvaInquilini" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="17" ToolTip="Salva i dati" />
                        </td>
                        <td style="width: 918px; height: 22px">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <img id="imgAnnulla" alt="Chiudi la finestra" onclick="document.getElementById('TextBox4').value!='1';myOpacity4.toggle();PulisciCampi();"
                                    src="../NuoveImm/Img_AnnullaVal.png" style="left: 185px; cursor: pointer; top: 23px" /></span></strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table> </div>
        <asp:ImageButton ID="btnVisualizza" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_VisualizzaPiccolo.png"
            Style="z-index: 102; left: 732px; position: absolute; top: 57px; height: 12px;"
            ToolTip="Visualizza dettaglio" />
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
            ReadOnly="True" Style="left: 13px; position: absolute; top: 517px" Width="572px">Nessuna Selezione</asp:TextBox>
    </strong>
    <script type="text/javascript">
        myOpacity4 = new fx.Opacity('divInquilini', { duration: 200 });
        //myOpacity.hide();
        if (document.getElementById('TextBox4').value != '2') {
            myOpacity4.hide(); ;
        }

    </script>
    <asp:HiddenField ID="idContratto" runat="server" Value="0" />
    <asp:HiddenField ID="DataEvento" runat="server" />
    <asp:HiddenField ID="id" runat="server" Value="0" />
    <asp:HiddenField ID="IdUnita" runat="server" Value="0" />
    <asp:HiddenField ID="IdCondominio" runat="server" Value="0" />
    <asp:HiddenField ID="descEvento" runat="server" />
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 652px; position: absolute; top: 538px" ToolTip="Home" />
    </span></strong>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('splash').style.visibility = 'hidden';
    </script>
</body>
</html>
