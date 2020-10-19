<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagaManuale.aspx.vb" Inherits="Contratti_Pagamenti_PagaManuale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pagamento Manuale</title>
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
        function ApriDettBollMOR() {

            if (document.getElementById('idBollettaMor').value != 0) {
                if (document.getElementById('idTipoBoll').value == '4') {
                    if (parseFloat(document.getElementById('ResCredito').value.replace(/\./g, '').replace(',', '.')) > 0 || document.getElementById('txtPagResoconto').value == '') {
                        if (document.getElementById('txtDataPagamento').value == '' || document.getElementById('txtDataRegistrazione').value == '' || document.getElementById('cmbTipoPagamento').value == -1 || document.getElementById('txtImpPagamento').value == '') {
                            alert('Impossibile procedere. Valorizzare le date, la tipologia di documento e l\'importo!');
                        }
                        else {
                            window.showModalDialog('DettRiclassificate.aspx?IDBOL=' + document.getElementById('idBollettaMor').value + '&IDCONTR=' + document.getElementById('vIdContratto').value + '&CONN=' + document.getElementById('vIdConnessione').value + '&IMPORTO=' + document.getElementById('txtImpPagamento').value + '&TOTPAGATO=' + document.getElementById('SumSelected').value + '&DATAREG=' + document.getElementById('txtDataRegistrazione').value + '&DATAPAG=' + document.getElementById('txtDataPagamento').value + '&TIPOPAG=' + document.getElementById('cmbTipoPagamento').value + '&ASS=' + document.getElementById('txtNumAssegno').value + '&NOTE=' + document.getElementById('txtnote').value.replace("'", "\'"), 'windowRicl', 'status:no;dialogWidth:950px;dialogHeight:680px;dialogHide:true;help:no;scroll:no');
                        }

                    }
                    else {
                        alert('Impossibile procedere! Residuo a zero!');

                    };
                }
            }
            else {
                alert('Nessuna bolletta selezionata!')
                return false;
            }
        };
    </script>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</head>
<body style="background-image: url('image/Sfondo.png'); background-repeat: repeat-x;">
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
                                        <asp:Button ID="btnEventi" runat="server" CssClass="bottone3" Text="Eventi" OnClientClick="ApriEventiIncassi();return false;" />
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
            <td>
                <fieldset style="border: 1px solid #0099FF">
                    <legend><span style="font-family: Arial; font-size: 8pt; font-weight: bold; color: #003399;
                        text-align: center;">INFO CONTRATTUALI</span></legend>
                    <table style="width: 100%;">
                        <tr>
                            <td width="2%">
                                &nbsp;
                            </td>
                            <td width="98%">
                                <div id="DatiContratto" style="width: 100%; height: 45px; overflow: auto;">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:DataGrid ID="DataGridContratto" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;
                                            left: 193px; top: 54px" Width="97%" BorderColor="#E7E7FF" BorderWidth="1px" BorderStyle="None"
                                            CellPadding="3" GridLines="Horizontal">
                                            <PagerStyle Mode="NumericPages" BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            <AlternatingItemStyle BackColor="#F7F7F7" />
                                            <ItemStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="STATO" HeaderText="STATO RAPPORTO"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="UNITA IMMOBILIARE">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SCALA" HeaderText="SCALA" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="LIV_PIANO" HeaderText="PIANO" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DATA_DECORRENZA" HeaderText="DATA DECORRENZA"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="COD_CONT" HeaderText="COD_CONT" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="#F7F7F7" />
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
                <fieldset style="border: 1px solid #663300">
                    <legend><span style="font-family: Arial; font-size: 8pt; font-weight: bold; color: #003399;
                        text-align: center;">TIPO INCASSO E FILTRO BOLLETTE&nbsp; </span></legend>
                    <table style="width: 100%;">
                        <tr>
                            <td width="5%">
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
                                                RepeatDirection="Horizontal" onclick="FiltroBollette();">
                                                <asp:ListItem Value="-1">AUTOMATICA</asp:ListItem>
                                                <asp:ListItem Value="0">SEMI AUTOMATICA</asp:ListItem>
                                                <asp:ListItem Value="1">MANUALE PER BOLLETTA</asp:ListItem>
                                                <asp:ListItem Value="2">MANUALE PER VOCE</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <table id="filtDate" runat="server">
                                                <tr>
                                                    <td>
                                                        <b>Riferimento dal</b>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:TextBox ID="txtDataDal" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="10"
                                                                Width="65px"></asp:TextBox>
                                                        </b>
                                                    </td>
                                                    <td>
                                                        <b>al</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDataAl" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="10"
                                                            Width="65px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Contratti/Pagamenti/image/refresh-icon.png"
                                                            ToolTip="Aggiorna le bollette in base al filtro" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Emesso dal</b>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:TextBox ID="txtEmesDal" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="10"
                                                                Width="65px"></asp:TextBox>
                                                        </b>
                                                    </td>
                                                    <td>
                                                        <strong>al</strong>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmesAl" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="10"
                                                            Width="65px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
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
                            <td width="2%">
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
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataPagamento"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                                width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </td>
                                        <td width="8%" nowrap="nowrap">
                                            <strong>DATA Val.*</strong>
                                        </td>
                                        <td width="10%">
                                            <asp:TextBox ID="txtDataRegistrazione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="10" Width="65px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataRegistrazione"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                                width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
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
                                            <asp:DropDownList ID="cmbTipoPagamento" runat="server" class="chzn-select" Font-Names="Arial"
                                                Font-Size="8pt" data-placeholder="Scegli un tipo atto..." Font-Bold="False" Style="z-index: 5000"
                                                AutoPostBack="True" Width="98%" onchange="IsAssegno(this);">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" width="50%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNumAssegno" runat="server" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="8pt" Text="Num." Style="visibility: hidden"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNumAssegno" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            MaxLength="15" Width="100px" Style="visibility: hidden"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDataAssegno" runat="server" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="8pt" Text="Data Assegno" Style="visibility: hidden" Width="100px"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox ID="txtDataAssegno" runat="server" Font-Names="Arial" 
                                                            Font-Size="8pt" MaxLength="10"
                                                            Width="65px" Style="visibility: hidden"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>NOTE</strong>
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtnote" runat="server" Width="90%" Font-Names="Arial" Font-Size="8pt"
                                                MaxLength="500"></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            <asp:Button ID="btnIncNonAttrib" runat="server" CssClass="bottone3" Text="Inc. Non Attribuiti"
                                                OnClientClick="ApriIncassiNonAttribuiti();" Width="90px" />
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
                            <asp:Label ID="lblResiduo" runat="server" Style="font-weight: 700; color: #FF0000;
                                text-align: center; font-size: 10pt; text-decoration: underline" Width="100%"></asp:Label>
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
            <td style="text-align: center">
                <span id="testo" runat="server" visible="false"><strong>PAGAMENTO DERIVATO DA INCASSI
                    NON ATTRIBUITI! ! !</strong></span>
            </td>
        </tr>
        <tr>
            <td id="bollette" runat="server">
                <fieldset style="border: 1px solid #FF6600">
                    <legend><span style="font-family: Arial; font-size: 8pt; font-weight: bold; color: #003399;
                        text-align: center;">ELENCO BOLLETTE PAGABILI</span></legend>
                    <table style="width: 100%;">
                        <tr>
                            <td width="2%">
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
                                                <asp:BoundColumn DataField="id_tipo_voce" HeaderText="id_tipo_voce" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ID_VOCE_BOL" HeaderText="ID_VOCE_BOL" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ID_BOLLETTA" HeaderText="ID_BOLLETTA" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="id_voce_aliquota" HeaderText="id_voce_aliquota" Visible="False">
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
                                                <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="TOTALE €.">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="PAGATO €">
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
                                                        <asp:ImageButton ID="imgSelezionaMor" runat="server" CommandName="Edit" ImageUrl="../../NuoveImm/Abbina_Seleziona.png"
                                                            Visible="false" ToolTip="Seleziona Bolletta" />
                                                        <asp:TextBox ID="txtImpPag" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="SELEZIONE">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelAll" runat="server" Text="SELEZIONA" AutoPostBack="True"
                                                            OnCheckedChanged="chkSelAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server"></asp:TextBox>
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
            <td style="font-family: Arial; font-size: 6pt">
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
                            <span id="txtqs" runat="server" clientidmode="Static" visible="false"><b><span style="color: #ec483e;
                                font-family: Arial; font-size: 12pt;">QUOTE SINDACALI ESCLUSE</span></b></span>
                        </td>
                        <td style="text-align: right" width="30%">
                            <asp:Button ID="btnSalvaPag" runat="server" CssClass="bottone" Text="Salva Pagamento"
                                OnClientClick="ConfPagam();" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnApriDettMor" runat="server" OnClientClick="ApriDettBollMOR();"
                    Style="visibility: hidden" />
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
    <asp:HiddenField ID="SumSelected" runat="server" Value="0" />
    <asp:HiddenField ID="ResCredito" runat="server" Value="0" />
    <asp:HiddenField ID="contrLocked" runat="server" Value="0" />
    <asp:HiddenField ID="SoloLettura" runat="server" Value="0" />
    <asp:HiddenField ID="confPagamento" runat="server" Value="0" />
    <asp:HiddenField ID="idIncasso" runat="server" Value="0" />
    <asp:HiddenField ID="impWritePagamento" runat="server" Value="0" />
    <asp:HiddenField ID="tipoPagValue" runat="server" Value="" />
    <asp:HiddenField ID="bloccoData" runat="server" Value="" />
    <asp:HiddenField ID="bloccoRangeData" runat="server" Value="" />
    <asp:HiddenField ID="flReload" runat="server" Value="0" />
    <asp:HiddenField ID="NonAttrib" runat="server" Value="0" />
    <asp:HiddenField ID="containsRat" runat="server" Value="0" />
    <asp:HiddenField ID="idBollettaMor" runat="server" Value="0" />
    <asp:HiddenField ID="idTipoBoll" runat="server" Value="0" />
    <asp:HiddenField ID="PagInMorosita" runat="server" Value="0" />
    <asp:HiddenField ID="HFExitForce" runat="server" Value="0" />
    <asp:HiddenField ID="ObbFiltro" runat="server" Value="0" />
    <asp:HiddenField ID="OldSelTipoPag" runat="server" Value="-1" />
    <asp:HiddenField ID="FiltNumBol" runat="server" Value="" />
    <asp:HiddenField ID="noCtrlDate" runat="server" Value="0" />
    <asp:HiddenField ID="BolloPagParz" runat="server" Value="0" />
    </form>
</body>
<script src="js/jsExit.js" type="text/javascript"></script>
<script src="../../AUTOCOMPLETE/cmbscript/chosen.jquery.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    var c1 = '#E0E4E3';
    var c2 = '#F62F0B';
    function colore1() {
        codice = '<font style = "font-family: Arial, Helvetica, sans-serif; font-size: 10pt; background-color :' + c2 + '" color=' + c1 + '><b>PAGAMENTO DERIVATO DA INCASSI NON ATTRIBUITI</b></font>';
        if (document.all) {
            document.all('testo').innerHTML = codice;
        }
        else if (document.getElementById) {
            document.getElementById("testo").innerHTML = codice;
        };
        attesa = window.setTimeout("colore2()", 500);
    };
    function colore2() {
        codice = '<font style = "font-family: Arial, Helvetica, sans-serif; font-size: 10pt;background-color :' + c1 + '" color=' + c2 + '><b>PAGAMENTO DERIVATO DA INCASSI NON ATTRIBUITI</b></font>';
        if (document.all) {
            document.all('testo').innerHTML = codice;
        }
        else if (document.getElementById) {
            document.getElementById("testo").innerHTML = codice;
        };
        attesa = window.setTimeout("colore1()", 500);
    };
    function avvia() {
        attesa = window.setTimeout("colore1()", 500);
    };
    initialize();
    function initialize() {
        AfterSubmit();
        $("#txtDataPagamento").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtDataRegistrazione").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtDataDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtDataAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtEmesDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtEmesAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtDataAssegno").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        if (document.getElementById('NonAttrib').value == 1) {
            avvia();
        };

        IsAssegno(document.getElementById('cmbTipoPagamento'));


    }
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
</html>
