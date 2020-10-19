<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagaModificaRuolo.aspx.vb"
    Inherits="Contratti_Pagamenti_PagaModificaRuolo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MODIFICA Pagamento Ruoli</title>
    <link href="css/Site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="js/jsFunzioni.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../AUTOCOMPLETE/cmbstyle/chosen.css" />
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
       
    </script>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
        .style2
        {
            font-size: 14pt;
            color: #FF3300;
            background-color: #99CCFF;
        }
        .style3
        {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="BeforeSubmit();return true;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td style="font-size: 4pt">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="70%">
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td width="10%">
                                        <strong style="font-size: 10pt">INTESTATARIO:</strong>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:TextBox ID="txtintestatario" runat="server" BackColor="#FFFFCC" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="#000066" Style="text-align: center" Width="80%" ReadOnly="True"
                                            Font-Bold="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20%">
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnEsci" runat="server" CssClass="bottone3" Text="Esci" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 4pt">
            </td>
        </tr>
        <tr>
            <td align="center" class="style2">
                <span class="style3">MODIFICA DEL PAGAMENTO DI IMPORTI A RUOLO</span>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset style="border: 1px solid #663300">
                    <legend><span style="font-family: Arial; font-size: 8pt; font-weight: bold; color: #003399;
                        text-align: center;">TIPO INCASSO</span></legend>
                    <table style="width: 100%;">
                        <tr>
                            <td width="1%">
                                &nbsp;
                            </td>
                            <td width="98%">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>TIPOLOGIA*</strong>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdbTipoIncasso" runat="server" AutoPostBack="True" Font-Bold="True"
                                                Font-Italic="True" Font-Names="Arial" Font-Size="8pt" Font-Underline="True" ForeColor="#CC3300"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="-1">AUTOMATICA</asp:ListItem>
                                                <asp:ListItem Value="0">SEMI AUTOMATICA</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="font-size: 4pt">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <fieldset style="border: 1px solid #33CC33;">
                    <legend><span style="font-family: Arial; font-size: 8pt; font-weight: bold; color: #003399;
                        text-align: center;">REGISTRAZIONE INCASSO </span></legend>
                    <table style="width: 100%;">
                        <tr>
                            <td width="1%">
                                &nbsp;
                            </td>
                            <td width="96%">
                                <table>
                                    <tr>
                                        <td width="8%" nowrap="nowrap">
                                            <strong>DATA Pag.*</strong>
                                        </td>
                                        <td width="10%" nowrap="nowrap">
                                            <asp:TextBox ID="txtDataPagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="10" Width="65px"></asp:TextBox>
                                        </td>
                                        <td width="8%" nowrap="nowrap">
                                            <strong>DATA Val.*</strong>
                                        </td>
                                        <td width="10%">
                                            <asp:TextBox ID="txtDataRegistrazione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="10" Width="65px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right" width="10%">
                                            <b>&nbsp;IMPORTO* €</b>
                                        </td>
                                        <td width="10%">
                                            <b>
                                                <asp:TextBox ID="txtImpPagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    MaxLength="20" Width="80px" Style="text-align: right"></asp:TextBox>
                                            </b>
                                        </td>
                                        <td style="text-align: right" width="5%">
                                            <strong>MODALITA&#39;*</strong>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="cmbTipoPagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Font-Bold="False" Style="z-index: 5000" AutoPostBack="true" Width="98%" onchange="IsAssegno(this);">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumAssegno" runat="server" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="8pt" Text="Num."></asp:Label>
                                        </td>
                                        <td width="50%">
                                            <asp:TextBox ID="txtNumAssegno" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="15" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>NOTE</strong>
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtnote" runat="server" Width="99%" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="500"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp
                                        </td>
                                        <td>
                                            <strong>SGRAVIO</strong>
                                            <asp:CheckBox ID="chkSgravio" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td width="5%" class="style1">
                        </td>
                        <td width="30%" class="style1">
                            <asp:RadioButtonList ID="rdbTipoModifica" runat="server" AutoPostBack="True" Font-Bold="True"
                                Font-Italic="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False"
                                ForeColor="#CC3300" RepeatDirection="Horizontal" Style="color: #0000FF; font-size: 10pt">
                                <asp:ListItem Value="0">MOD. REGISTRAZIONE</asp:ListItem>
                                <asp:ListItem Value="1">MOD. PAGAMENTO</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="65%" align="right" class="style1">
                            <table id="tblAutomatica" runat="server" style="border: 1px solid #66FF33; background-color: #F2EBBF;">
                                <tr>
                                    <td>
                                        PAGAMENTO €
                                    </td>
                                    <td>
                                        <b>
                                            <asp:TextBox ID="txtPagResoconto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="20" Width="80px" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                        </b>
                                    </td>
                                    <td>
                                        SELEZIONE.€.
                                    </td>
                                    <td>
                                        <b>
                                            <asp:TextBox ID="txtSommaSel" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="20"
                                                Width="80px" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                        </b>
                                    </td>
                                    <td>
                                        CREDITO€.
                                    </td>
                                    <td>
                                        <b>
                                            <asp:TextBox ID="txtResResoconto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="20" Width="80px" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                        </b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="bollette" runat="server">
                <fieldset style="border: 1px solid #FF6600">
                    <legend><span style="font-family: Arial; font-size: 8pt; font-weight: bold; color: #003399;
                        text-align: center;">ELENCO BOLLETTE A RUOLO PAGABILI</span></legend>
                    <table style="width: 100%;">
                        <tr>
                            <td width="1%">
                                &nbsp;
                            </td>
                            <td width="98%">
                                <div style="width: 100%; overflow: auto;" id="divBo">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:DataGrid ID="dgvBollVoci" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;
                                            left: 193px; top: 54px" Width="97%" BorderColor="#E7E7FF" BorderWidth="1px" BorderStyle="None"
                                            CellPadding="3" GridLines="Horizontal">
                                            <PagerStyle Mode="NumericPages" BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            <AlternatingItemStyle BackColor="#F7F7F7" />
                                            <ItemStyle ForeColor="Black" BackColor="#EBE9ED" Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID_BOLLETTA" HeaderText="ID_BOLLETTA" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUMERO">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" Font-Names="Arial" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TIPOBOLL" HeaderText="TIPO">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="RIFERIMENTO" HeaderText="RIFERIMENTO">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DATA_PAGAMENTO" HeaderText="DATA PAG.">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="IMPORTO_RUOLO" HeaderText="TOTALE RUOLO €.">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="imp_ruolo_pagato" HeaderText="PAGATO RUOLO €">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO €">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="IMPORTO €">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtImpPag" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:TextBox>
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="SELEZIONE">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelAll" runat="server" Text="SELEZIONA" AutoPostBack="True"
                                                            OnCheckedChanged="chkSelAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <HeaderStyle BackColor="#CCFF99" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Blue" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        </asp:DataGrid>
                                    </span></strong>
                                </div>
                            </td>
                            <td width="2%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td style="text-align: center" width="60%">
                            &nbsp
                        </td>
                        <td style="text-align: right" width="30%">
                            <asp:Button ID="btnSalvaPag" runat="server" CssClass="bottone" Text="Salva Pagamento"
                                OnClientClick="ConfPagam();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="dialog" style="display: none;">
    </div>
    <div id="confirm" style="display: none;">
    </div>
    <div id="loading" style="display: none; text-align: center;">
    </div>
    <div id="divLoading" style="width: 0px; height: 0px; display: none;">
        <img src="image/load.gif" id="imageLoading" alt="" />
    </div>
    <div id="divOscura" style="display: none; text-align: center; width: 100%; height: 100%;
        position: absolute; top: 0px; left: 0px; background-color: #cccccc; z-index: 2;">
    </div>
    <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="noClose" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="noCloseRead" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="vIdConnessione" runat="server" />
    <asp:HiddenField ID="vIdContratto" runat="server" Value="0" />
    <asp:HiddenField ID="vIdUnita" runat="server" Value="0" />
    <asp:HiddenField ID="vIdAnagrafica" runat="server" Value="0" />
    <asp:HiddenField ID="totPagabile" runat="server" Value="0" />
    <asp:HiddenField ID="totPagato" runat="server" Value="0" />
    <asp:HiddenField ID="totResiduo" runat="server" Value="0" />
    <asp:HiddenField ID="tipoPagValue" runat="server" Value="" />
    <asp:HiddenField ID="SumSelected" runat="server" Value="0" />
    <asp:HiddenField ID="ResCredito" runat="server" Value="0" />
    <asp:HiddenField ID="contrLocked" runat="server" Value="0" />
    <asp:HiddenField ID="SoloLettura" runat="server" Value="0" />
    <asp:HiddenField ID="confPagamento" runat="server" Value="0" />
    <asp:HiddenField ID="idIncasso" runat="server" Value="0" />
    <asp:HiddenField ID="impWritePagamento" runat="server" Value="0" />
    <asp:HiddenField ID="idBollettaMor" runat="server" Value="0" />
    <asp:HiddenField ID="idTipoBoll" runat="server" Value="0" />
    <asp:HiddenField ID="HFExitForce" runat="server" Value="0" />
    <asp:HiddenField ID="BolloPagParz" runat="server" Value="0" />
    </form>
    <script src="js/jsExit.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        initialize();
        function initialize() {
            AfterSubmit();
            $("#txtDataPagamento").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
            $("#txtDataRegistrazione").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });

            IsAssegno(document.getElementById('cmbTipoPagamento'));

        }
//        if (document.getElementById('cmbTipoPagamento').value == 5) {
//            document.getElementById('lblNumAssegno').style.visibility = 'visible';
//            document.getElementById('txtNumAssegno').style.visibility = 'visible';

//        }
//        else {

//            document.getElementById('lblNumAssegno').style.visibility = 'hidden';
//            document.getElementById('txtNumAssegno').style.visibility = 'hidden';
//            document.getElementById('txtNumAssegno').value = '';
//        };
        var c1 = '#0c2238';
        var c2 = '#ec483e';
        function colore1() {
            codice = '<B><SPAN style="FONT-SIZE: 12pt; COLOR: ' + c1 + '; FONT-FAMILY: Arial">QUOTE SINDACALI ESCLUSE</SPAN></B>';
            if (document.getElementById("txtqs")) {
                document.getElementById("txtqs").innerHTML = codice;
            };
            attesa = window.setTimeout("colore2()", 500);
        };
        function colore2() {
            codice = '<B><SPAN style="FONT-SIZE: 12pt; COLOR: ' + c2 + '; FONT-FAMILY: Arial">QUOTE SINDACALI ESCLUSE</SPAN></B>';
            if (document.getElementById("txtqs")) {
                document.getElementById("txtqs").innerHTML = codice;
            };
            attesa = window.setTimeout("colore1()", 500);
        };
        function avvia() {
            attesa = window.setTimeout("colore1()", 500);
        };
        avvia();
    </script>
</body>
</html>
