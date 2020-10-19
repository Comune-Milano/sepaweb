<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneAllegati.aspx.vb"
    Inherits="GestioneAllegati_GestioneAllegati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Allegati - Sep@Web</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/jquery/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../CICLO_PASSIVO/CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">


        function esci() {
            if (typeof (window.opener.aggiornaDopoAllegati) === 'function')
                window.opener.aggiornaDopoAllegati($('#HFIdOggetto').val());

            self.close();
            return false;
        }

        function EliminaAllegato() {
            var sicuro = window.confirm(Elimina);
            if (sicuro == true) {
                return true;
            } else {
                return false;
            }
        };
        function AddTipologia() {
            //caricamento(1, 1);
            var test = $find('RadWindow1');
            test.setUrl('GestioneTipologia.aspx?ID=-1&O=' + document.getElementById('HFOggetto').value);
            test.show();
            //CenterPageModal('GestioneTipologia.aspx?ID=-1&O=' + document.getElementById('HFOggetto').value, 'AddTipologia', 400, 200);
        };
        function ModTipologia() {
            var combo = $find('ddlTipologiaAllegati');
            if (combo._value != '-1') {
                var test = $find('RadWindow1');
                test.setUrl('GestioneTipologia.aspx?ID=' + combo._value + '&O=' + document.getElementById('HFOggetto').value);
                test.show();
            } else {
                radalert('Non hai selezionato alcuna riga!', '300', '150');
                return false;
            };
        };
        function DeleteTipologia() {
            var combo = $find('ddlTipologiaAllegati');
            if (combo._value != '-1') {
                var sicuro = window.confirm(Elimina2);
                if (sicuro == true) {
                    caricamento(1, 1);
                } else {
                    return false;
                }
            } else {
                radalert('Non hai selezionato alcuna riga!', '300', '150');
                return false;
            };
        };
        function ConfDefault() {
            var sicuro = window.confirm('Sei sicuro di voler procedere?');
            if (sicuro == true) {
                caricamento(1, 1);
            } else {
                return false;
            }
        };
        function ConfProtocollo() {
            if (document.getElementById('cbProtocolla') != null) {
                if (!document.getElementById('cbProtocolla').checked) {
                    var sicuro = window.confirm('Sei sicuro di voler protocollare l\'allegato?');
                    if (sicuro == true) {
                        document.getElementById('HFConferma').value = '1';
                    } else {
                        document.getElementById('HFConferma').value = '0';
                    }
                } else {
                    document.getElementById('HFConferma').value = '1';
                };
            } else {
                document.getElementById('HFConferma').value = '1';
            };
        };
        function ConfSpostaProtocollo() {
            var sicuro = window.confirm('Sei sicuro di voler spostare l\'allegato nel protocollo?');
            if (sicuro == true) {
                document.getElementById('HFConferma').value = '1';
            } else {
                document.getElementById('HFConferma').value = '0';
            }
        };
        function openProtocollo(tipo) {
            document.getElementById('divProtocolloA').style.visibility = 'visible';
            document.getElementById('divProtocolloB').style.visibility = 'visible';
            if (tipo == 0) {
                document.getElementById('txtNumProtocollo').innerHTML = '';
                document.getElementById('txtDescrizioneAllegatoProtocollo').innerHTML = '';
                document.getElementById('txtNumProtocollo').value = '';
                document.getElementById('txtDescrizioneAllegatoProtocollo').value = '';
                document.getElementById('ddlTipologiaAllegatoProtocollo').value = '-1';
            };
        };
        function closeProtocollo() {
            document.getElementById('divProtocolloA').style.visibility = 'hidden';
            document.getElementById('divProtocolloB').style.visibility = 'hidden';
        };
    </script>
    <style type="text/css">
.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}</style>
</head>
<body>
    <%--onsubmit="BeforeSubmit();return true;"--%>
    <form id="form1" runat="server" class="sfondo">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" AutoSize="false" Behavior="Pin, Move, Resize" Skin="Web20"
        Height="250" Width="500">
    </telerik:RadWindow>
    <table style="width: 100%;">
        <tr>
            <td style="width: 5px;">
                &nbsp;
            </td>
            <td>
                <div id="divTitolo" style="width: 100%; height: 35px;">
                    <br />
                    <table style="width: 97%; position: relative; top: -3px;">
                        <tr>
                            <td colspan="7" class="TitoloModulo">
                                <asp:Label ID="lblTitoloModulo" runat="server" Text="PROTOCOLLO"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitolo" runat="server" Text="" class="TitoloModulo"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                <asp:Image ID="imgAlertProtocollo" runat="server" ImageUrl="../../../Images/checkerboard.gif"
                                    Width="16px" Height="16px" ToolTip="L'utente non è associato al Protocollo e potrà generare solo lettere in BOZZA." />
                            </td>
                            <td style="width: 5px;">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                <img id="imgLogoProtocollo" runat="server" alt="Protocollo" src="../GestioneAllegati/Immagini/logo_protocollo.png" />
                            </td>
                            <td style="width: 5px;">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                <asp:Label ID="lblProtocollo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                         <asp:Button ID="btnEsci" runat="server" Text="Esci" OnClientClick="return esci();" Style="cursor:pointer" 
                                    ToolTip="Esci" />
                        </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div id="divAllega" runat="server" style="width: 100%; height: 300px;">
                    <fieldset style="height: 97%;">
                        <legend>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Image ID="imgAllega" runat="server" ImageUrl="Immagini/allega.png" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <span style="font-size: 1.3em;">
                                            <asp:Label ID="lblTitoloFieldset1" runat="server" Text=""></asp:Label></span>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </legend>
                        <div id="divGestioneInserimento" runat="server" style="width: 100%;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTipologia" runat="server" Text=""></asp:Label></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 97%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 85%;">
                                                    <telerik:RadComboBox ID="ddlTipologiaAllegati" AppendDataBoundItems="true" Filter="Contains"
                                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                        LoadingMessage="Caricamento..." Width="97%">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td style="width: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 20px;">
                                                    <asp:ImageButton ID="btnAddTipologia" runat="server" ImageUrl="Immagini/add_ico.png"
                                                        ToolTip="Aggiungi Tipologia Allegato" OnClientClick="AddTipologia();return false;" />
                                                </td>
                                                <td style="width: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 20px;">
                                                    <asp:ImageButton ID="btnModTipologia" runat="server" ImageUrl="Immagini/mod_ico.png"
                                                        ToolTip="Modifica Tipologia Allegato" OnClientClick="ModTipologia();return false;" />
                                                </td>
                                                <td style="width: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 20px;">
                                                    <asp:ImageButton ID="btnDeleteTipologia" runat="server" ImageUrl="Immagini/delete_ico.png"
                                                        ToolTip="Elimina Tipologia Allegato" OnClientClick="return DeleteTipologia();" />
                                                </td>
                                                <td style="width: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 20px;">
                                                    <asp:ImageButton ID="btnDefaultTipologia" runat="server" ImageUrl="Immagini/dafault.png"
                                                        ToolTip="Crea Tipologie di Default se assenti" OnClientClick="return ConfDefault();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 97%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 85%;">
                                                    <strong>Descrizione Allegato</strong>&nbsp;&nbsp;<em>(massimo 500 caratteri)</em>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbProtocolla" runat="server" Text="Protocolla Allegato" Checked="false"
                                                        Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDescrizioneAllegato" runat="server" Height="75px" TextMode="MultiLine"
                                            Width="97%"></asp:TextBox>
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
                                                <td style="width: 60px;">
                                                    <strong>Allegato*</strong>
                                                </td>
                                                <td style="width: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="FileUpload" runat="server" Width="97%" Size="100%" />
                                                </td>
                                                <td style="width: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 100px;">
                                                    <asp:Button ID="btnAllega" runat="server" Text="Allega" ToolTip="Allega File" OnClientClick="ConfProtocollo();" Style="cursor:pointer"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divGestioneRicerca" runat="server" style="width: 100%;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTipologiaWS" runat="server" Text="Tipologia Protocollo"></asp:Label></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlTipologiaWS" runat="server" Width="97%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Protocollo</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtProtocollo" runat="server" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Anno Archiviazione</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtAnnoArchiviazione" runat="server" Width="50px" MaxLength="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Button ID="btnRicercaProtocollo" runat="server" Text="Ricerca" ToolTip="Ricerca nel Protocollo"
                                            Visible="false" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
                <br />
                <div id="divAllegati" runat="server" style="width: 100%; height: 350px;">
                    <fieldset style="height: 97%;">
                        <legend>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Image ID="imgAllegati" runat="server" ImageUrl="Immagini/allegati.png" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <span style="font-size: 1.3em;">
                                            <asp:Label ID="lblTitoloFieldset2" runat="server" Text=""></asp:Label></span>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnAllegaProtocollo" runat="server" ImageUrl="Immagini/logo_protocollo.png"
                                            ToolTip="Allega un Protocollo agli Allegati" OnClientClick="openProtocollo(0);return false;"
                                            Visible="false" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnAggiorna" runat="server" ImageUrl="Immagini/aggiorna.png"
                                            ToolTip="Aggiorna Lista Allegati" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnScaricaTutto" runat="server" ImageUrl="../NuoveImm/giu.png"
                                            ToolTip="Scarica tutti gli allegati" Width="16" Height="16" />
                                    </td>
                                </tr>
                            </table>
                        </legend>
                        <div id="divGridAllegati" runat="server" style="width: 99%; height: 275px; overflow: auto;">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:DataGrid ID="dgvAllegati" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                                    Width="97%" ForeColor="#333333">
                                    <ItemStyle BackColor="#EFF3FB" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                        Position="TopAndBottom" Visible="False" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_ALLEGATO" HeaderText="ID_ALLEGATO" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NR" HeaderText="#">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOME" HeaderText="NOME">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA E ORA">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="DOWNLOAD">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="Immagini/download.png"
                                                    OnClick="btnDownload_Click" ToolTip="Download Allegato" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="ELIMINA">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="Immagini/delete.png" ToolTip="Elimina Allegato"
                                                    OnClick="btnElimina_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="PROTOCOLLO">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnProtocollo" runat="server" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="STATO" HeaderText="STATO" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FL_PROTOCOLLO" HeaderText="FL_PROTOCOLLO" Visible="False">
                                        </asp:BoundColumn>
                                         <asp:BoundColumn DataField="ID_TIPOLOGIA_OGGETTO" HeaderText="ID_TIPOLOGIA_OGGETTO" Visible="False">
                                        </asp:BoundColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="White" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </span></strong>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                                </td>
                                <td style="width: 15px;">
                                    &nbsp;
                                </td>
                                <td>
                                    <em>Legenda: I file barrati in elenco sono stati cancellati.</em>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
            <td style="width: 5px;">
            </td>
        </tr>
    </table>
    <div class="dialA" id="divProtocolloA" style="visibility: hidden">
    </div>
    <div class="dialB" id="divProtocolloB" style="visibility: hidden">
        <div class="dialC">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100px;">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center>
                            <asp:Label ID="lblTitoloContratti" runat="server" Font-Bold="True" Font-Size="12pt"
                                Font-Underline="True" ForeColor="#801F1C" Width="97%">Aggiungi Protocollo agli Allegati</asp:Label></center>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Protocollo*:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumProtocollo" runat="server" Width="97%" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        Tipologia*:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlTipologiaAllegatoProtocollo" AppendDataBoundItems="true"
                            Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                            HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Width="97%">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        Descrizione:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescrizioneAllegatoProtocollo" runat="server" Width="97%" Height="300px"
                            ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align: right;">
                        <em>Massimo 500 caratteri.</em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btnAggiungiProtocollo" runat="server" Text="Aggiungi Protocollo agli Allegati"
                            ToolTip="Aggiungi Protocollo" CausesValidation="False" ClientIDMode="Static"
                            Visible="false" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnEsciProtocollo" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
                            ClientIDMode="Static" OnClientClick="closeProtocollo();return false;" />&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dialog" style="display: none;">
    </div>
    <div id="confirm" style="display: none;">
    </div>
    <div id="loading" style="display: none; text-align: center;">
    </div>
    <div id="divLoading" style="width: 0px; height: 0px; display: none;">
        <img src="Immagini/load.gif" id="imageLoading" alt="" />
    </div>
    <div id="divOscura" style="display: none; text-align: center; width: 100%; height: 100%;
        position: absolute; top: 0px; left: 0px; background-color: #cccccc; z-index: 2;">
    </div>
    <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="HFOggetto" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFIdOggetto" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idSelected" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="FlProtocollo" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="HFConferma" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFTipoGestione" runat="server" Value="1" ClientIDMode="Static" />
    <asp:HiddenField ID="HFNomeFile" runat="server" Value="" ClientIDMode="Static" />
    </form>
</body>
</html>
