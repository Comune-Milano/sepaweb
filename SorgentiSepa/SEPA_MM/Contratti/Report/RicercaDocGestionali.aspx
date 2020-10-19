<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDocGestionali.aspx.vb"
    Inherits="Contratti_Report_RicercaDocGestionali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Documenti Gestionali</title>
    <script type="text/javascript">
        function ScrollPosFiliali(obj) {
            document.getElementById('yPosFiliali').value = obj.scrollTop;
        }
        function ScrollPosQuartieri(obj) {
            document.getElementById('yPosQuartieri').value = obj.scrollTop;
        }
        function ScrollPosComplessi(obj) {
            document.getElementById('yPosComplessi').value = obj.scrollTop;
        }
        function ScrollPosEdifici(obj) {
            document.getElementById('yPosEdifici').value = obj.scrollTop;
        }
        function ScrollPosIndirizzi(obj) {
            document.getElementById('yPosIndirizzi').value = obj.scrollTop;
        }
        function ScrollPosContrattoSpecifico(obj) {
            document.getElementById('yPosContrattoSpecifico').value = obj.scrollTop;
        }
        function ScrollPosTipologiaUI(obj) {
            document.getElementById('yPosTipologiaUI').value = obj.scrollTop;
        }
        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    if (window.event) {
                        event.keyCode = 0;
                    } else {
                        e.preventDefault();
                    }
                }
            } else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                } else if (obj.value.length == 5) {
                    obj.value += "/";
                } else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            event.keyCode = 0;
                        } else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('../../MOROSITA/Immagini/sfondo2.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../../MOROSITA/Immagini/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td style="text-align: center; color: #801f1c; font-family: ARIAL, Helvetica, sans-serif;
                            font-size: 14pt; font-weight: bold;">
                            RICERCA DOCUMENTI GESTIONALI
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" Text="Informazioni Rapporto Utenza" runat="server" Font-Names="Arial"
                                Font-Size="11pt" Font-Bold="True" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <table style="background-color: #F2FBFF; width: 100%">
                        <tr>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="SEDE T." runat="server" ID="CheckBoxFiliali" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="QUARTIERI" runat="server" ID="CheckBoxQuartieri" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="COMPLESSI" runat="server" ID="CheckBoxComplessi" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="EDIFICI" runat="server" ID="CheckBoxEdifici" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="INDIRIZZO" runat="server" ID="CheckBoxIndirizzi" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelFiliali" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto" onscroll="ScrollPosFiliali(this);">
                                    <asp:CheckBoxList ID="CheckBoxListFiliali" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        Width="90%" AutoPostBack="True">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelQuartieri" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto" onscroll="ScrollPosQuartieri(this);">
                                    <asp:CheckBoxList ID="CheckBoxListQuartieri" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        Width="90%" AutoPostBack="True">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelComplessi" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto" onscroll="ScrollPosComplessi(this);">
                                    <asp:CheckBoxList ID="CheckBoxListComplessi" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        Width="90%" AutoPostBack="True">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelEdifici" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto" onscroll="ScrollPosEdifici(this);">
                                    <asp:CheckBoxList ID="CheckBoxListEdifici" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        Width="90%" AutoPostBack="True">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelIndirizzi" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto" onscroll="ScrollPosIndirizzi(this);">
                                    <asp:CheckBoxList ID="CheckBoxListIndirizzi" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        Width="90%">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="TIPOLOGIA RAPPORTO" runat="server" ID="CheckBoxTipologiaRapporto"
                                    BackColor="#CCFF99" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC"
                                    Width="100%" AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="CONTRATTO SPECIFICO" runat="server" ID="CheckBoxContrattoSpecifico"
                                    BackColor="#CCFF99" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC"
                                    Width="100%" AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="TIPOLOGIA UI" runat="server" ID="CheckBoxTipologiaUI" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="STATO CONTRATTO" runat="server" ID="CheckBoxStatoContratto" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="AREA CANONE" runat="server" ID="CheckBoxAreaCanone" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelTipologiaRapporti" runat="server" Style="border: 1px solid #C6EEFF;
                                    height: 100px; overflow: auto">
                                    <asp:CheckBoxList ID="CheckBoxListTipologiaRapporti" runat="server" Font-Names="Arial"
                                        Font-Size="7pt" Width="90%" AutoPostBack="True">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelContrattoSpecifico" runat="server" Style="border: 1px solid #C6EEFF;
                                    height: 100px; overflow: auto" onscroll="ScrollPosContrattoSpecifico(this);">
                                    <asp:CheckBoxList ID="CheckBoxListContrattoSpecifico" runat="server" Font-Names="Arial"
                                        Font-Size="7pt" Width="90%">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelTipologiaUI" runat="server" Style="border: 1px solid #C6EEFF;
                                    height: 100px; overflow: auto" onscroll="ScrollPosTipologiaUI(this);">
                                    <asp:CheckBoxList ID="CheckBoxListTipologiaUI" runat="server" Font-Names="Arial"
                                        Font-Size="7pt" Width="90%">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelStatoContratto" runat="server" Style="border: 1px solid #C6EEFF;
                                    height: 100px; overflow: auto">
                                    <asp:CheckBoxList ID="CheckBoxListStatoContratto" runat="server" Font-Names="Arial"
                                        Font-Size="7pt" Width="90%">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td style="width: 20%">
                                <asp:Panel ID="PanelAreaCanone" runat="server" Style="border: 1px solid #C6EEFF;
                                    height: 100px; overflow: auto">
                                    <asp:CheckBoxList ID="CheckBoxListAreaCanone" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        Width="90%">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                <asp:Label Text="STIPULA CONTRATTO" runat="server" ID="Label3" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%" />
                            </td>
                            <td>
                                &nbsp
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                <asp:Label Text="CODICE CONTRATTO" runat="server" ID="Label1" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label11" Text="DAL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="stipulaDal" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label12" Text="AL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="stipulaAl" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                &nbsp
                            </td>
                            <td>
                                <asp:Panel ID="Panel4" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%" style="vertical-align: top;">
                                        <tr>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox ID="txtCodContr" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Width="98%" Font-Bold="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" Text="Informazioni Doc. Gestionale" runat="server" Font-Names="Arial"
                                Font-Size="11pt" Font-Bold="True" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <table width="100%" style="background-color: #FFF2FF">
                        <tr>
                            <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                <asp:CheckBox Text="TIPO DOCUMENTO" runat="server" ID="CheckBoxTipoDoc" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%"
                                    AutoPostBack="True" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                <asp:Label Text="DETTAGLI DOCUMENTO" runat="server" ID="Label2" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                <asp:Label Text="DATA EMISSIONE" runat="server" ID="Label4" BackColor="#CCFF99" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                <asp:Label Text="DATA RIFERIMENTO" runat="server" ID="Label7" BackColor="#CCFF99"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%" />
                            </td>
                            <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                <asp:Label Text="IMPORTO €" runat="server" ID="Label10" BackColor="#CCFF99" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; vertical-align: top;" rowspan="2">
                                <asp:Panel ID="PanelTipoDoc" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto">
                                    <asp:CheckBoxList ID="CheckBoxListTipoDoc" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        Width="90%">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td>
                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel2" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                                overflow: auto">
                                                <asp:DropDownList ID="DropDownListCredDeb" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Height="20px" Width="100%" Font-Bold="True">
                                                    <asp:ListItem Value="-1">TUTTI</asp:ListItem>
                                                    <asp:ListItem Value="1">Solo Credito</asp:ListItem>
                                                    <asp:ListItem Value="0">Solo Debito</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:CheckBox ID="CheckBoxElab" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Height="20px" Font-Bold="True" TabIndex="11" Text="Solo da eleborare" TextAlign="Left" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td rowspan="2">
                                <asp:Panel ID="Panel5" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label5" Text="DAL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="emissioneDal" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label6" Text="AL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="emissioneAl" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td rowspan="2">
                                <asp:Panel ID="Panel6" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label8" Text="DAL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="riferimentoDal" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label9" Text="AL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="riferimentoAl" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td rowspan="2">
                                <asp:Panel ID="Panel7" runat="server" Style="border: 1px solid #C6EEFF; height: 100px;
                                    overflow: auto">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label14" Text="DA" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="importoDA" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label15" Text="A" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="importoA" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; vertical-align: middle">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" Text="Ordinamento dati per" runat="server" BackColor="#CCFF99"
                                            Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0033CC" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListOrdinamento" runat="server" Font-Names="arial"
                                Font-Size="10pt" Height="20px" Width="30%" Font-Bold="True">
                                <asp:ListItem Value="INTESTATARIO" Selected="True">INTESTATARIO</asp:ListItem>
                                <asp:ListItem Value="IMP_EMESSO DESC">IMPORTO DECRESCENTE</asp:ListItem>
                                <asp:ListItem Value="IMP_EMESSO ASC">IMPORTO CRESCENTE</asp:ListItem>
                                <asp:ListItem Value="LOCALITA,INDIRIZZO,CIVICO">COMUNE, INDIRIZZO, CIVICO</asp:ListItem>
                                <asp:ListItem Value="RAPPORTI_UTENZA.COD_CONTRATTO">CODICE CONTRATTO</asp:ListItem>
                                <asp:ListItem Value="UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE">CODICE UNITA</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                </table>
                <table style="text-align: right; width: 100%;">
                    <tr>
                        <td>
                            &nbsp
                        </td>
                        <td width="50%">
                            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                                TabIndex="1" />
                        </td>
                        <td width="5%">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../NuoveImm/Img_EsciCorto.png"
                                ToolTip="Esci" TabIndex="2" OnClientClick="self.close()" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="yPosFiliali" runat="server" Value="0" />
                <asp:HiddenField ID="yPosQuartieri" runat="server" Value="0" />
                <asp:HiddenField ID="yPosComplessi" runat="server" Value="0" />
                <asp:HiddenField ID="yPosEdifici" runat="server" Value="0" />
                <asp:HiddenField ID="yPosIndirizzi" runat="server" Value="0" />
                <asp:HiddenField ID="yPosContrattoSpecifico" runat="server" Value="0" />
                <asp:HiddenField ID="yPosTipologiaUI" runat="server" Value="0" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g
            //'notnumbers': /[^\d\,]/g
        }

        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            o.value = o.value.replace('.', ',');
        }

        function AutoDecimal2(obj) {

            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a != 'NaN') {
                    if (a.substring(a.length - 3, 0).length >= 4) {
                        var decimali = a.substring(a.length, a.length - 2);
                        var dascrivere = a.substring(a.length - 3, 0);
                        var risultato = '';
                        while (dascrivere.replace('-', '').length >= 4) {
                            risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                        }
                        risultato = dascrivere + risultato + ',' + decimali;
                        //document.getElementById(obj.id).value = a.replace('.', ',')
                        document.getElementById(obj.id).value = risultato;
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace('.', ',');
                    }

                }
                else
                    document.getElementById(obj.id).value = '';
            }
        }
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        }
    </script>
</body>
</html>
