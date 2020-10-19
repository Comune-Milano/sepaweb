<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VociServizio.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_VociServizio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Voci Servizi</title>
    <script type="text/javascript">
        function Aggiorna() {
            if (document.getElementById('PROVENIENZAda').value == '1') {
                if (document.getElementById('DEL').value != '1') {
                    var chiediConferma;
                    chiediConferma = window.confirm('Per tutti gli importi da dividere modificati, l\'eventuale divisione effettuata precedentemente verrà persa! Proseguire lo stesso?');
                    if (chiediConferma == true) {
                        document.getElementById('Conferma').value = '1';
                    } else {
                        document.getElementById('Conferma').value = '0';
                    }
                } else {
                    document.getElementById('Conferma').value = '1';
                }
            }
        }
        function ConfermaEsci() {

            if (document.getElementById('txtmodificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, sono state fatte delle modifiche. Sei sicuro di voler uscire senza salvare?");
                if (chiediConferma == true) {
                    if (window.opener != null) {
                        if (!window.opener.closed) {
                            if (window.opener.location.pathname.substr(window.opener.location.pathname.lastIndexOf("/") + 1, window.opener.location.pathname.length - window.opener.location.pathname.lastIndexOf("/")) == 'DettagliSituazioneGenerale.aspx') {
                                window.opener.location.replace('../../../SPESE_REVERSIBILI/DettagliSituazioneGenerale.aspx?IDV=' + document.getElementById('idVoce').value);
                            }
                        }
                    }
                    self.close();
                }
            }
            else {

                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    if (window.opener != null) {
                        if (!window.opener.closed) {
                            if (window.opener.location.pathname.substr(window.opener.location.pathname.lastIndexOf("/") + 1, window.opener.location.pathname.length - window.opener.location.pathname.lastIndexOf("/")) == 'DettagliSituazioneGenerale.aspx') {
                                window.opener.location.replace('../../../SPESE_REVERSIBILI/DettagliSituazioneGenerale.aspx?IDV=' + document.getElementById('idVoce').value);
                            }
                        }
                    }
                    self.close();
                }
            }
        }

        function ConfermaIndietro() {

            if (document.getElementById('txtmodificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, sono state fatte delle modifiche. Sei sicuro di voler tornare indietro senza salvare?");
                if (chiediConferma == false) {
                    document.getElementById('indietro').value = '1';
                }
                else {
                    document.getElementById('indietro').value = '0';
                }
            }
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\.\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            document.getElementById('txtmodificato').value = '1';
        }

        function AutoDecimal2(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }

        }


        function Dividi(Indice) {
            if (document.getElementById('PROVENIENZAda').value == '1') {
                var stringaM = document.getElementById('Modifiche').value;
                var stringaD = document.getElementById('Divisione').value;
                if ((stringaM.indexOf(Indice) != -1) && (stringaD.indexOf(Indice) != -1)) {
                    //l'importo della voce è stato modificato ed era già diviso
                    //in questo particolare caso è necessario che la divisione precedente venga cancellata,
                    //quindi chiedere conferma
                    var chiediConferma;
                    chiediConferma = window.confirm('L\importo da dividere è stato modificato, pertanto la divisione effettuata precedentemente verrà persa! Proseguire lo stesso?');
                    if (chiediConferma == true) {
                        document.getElementById('VOCEp').value = Indice;
                        document.getElementById('DEL').value = '1';
                        document.getElementById('ImgProcedi').click();
                    }
                } else {
                    if (document.getElementById('txtmodificato').value == '1') {
                        alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di continuare!');
                    }
                    else {
                        if (Indice == '-1') {
                            alert('Attenzione, inserire un importo diverso da 0,00, salvare e quindi dividere!');
                        }
                        else {
                            if (document.getElementById('tipolotto').value == 'E') {
                                window.showModalDialog('DividiImporto.aspx?PROV=PREV&IDL=' + document.getElementById('idLotto').value + '&IDVS=' + Indice + '&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                            }
                            else {
                                window.showModalDialog('DividiImportoImpianto.aspx?PROV=PREV&IDL=' + document.getElementById('idLotto').value + '&IDVS=' + Indice + '&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                            }
                            document.getElementById('imgRefresh').click();
                        }
                    }
                }
            } else {
                if (document.getElementById('txtmodificato').value == '1') {
                    alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di continuare!');
                }
                else {
                    if (Indice == '-1') {
                        alert('Attenzione, inserire un importo diverso da 0,00, salvare e quindi dividere!');
                    }
                    else {
                        if (document.getElementById('tipolotto').value == 'E') {
                            window.showModalDialog('DividiImporto.aspx?IDL=' + document.getElementById('idLotto').value + '&IDVS=' + Indice + '&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                        }
                        else {
                            window.showModalDialog('DividiImportoImpianto.aspx?IDL=' + document.getElementById('idLotto').value + '&IDVS=' + Indice + '&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                        }
                        document.getElementById('imgRefresh').click();
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="left: 0px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg');
        width: 798px; position: absolute; top: 0px; height: 596px;">
        <tr>
            <td style="width: 706px">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Piano
                    Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" Style="font-weight: 700"></asp:Label>
                    <br />
                </span>
                <br />
                <br />
                <br />
                <asp:Label ID="lblServizio12" runat="server" Style="position: absolute; top: 146px;
                    left: 14px; font-style: italic;" Font-Bold="True" Font-Names="arial" Font-Size="10pt">Lotto</asp:Label>
                <asp:Label ID="lblServizio0" runat="server" Style="position: absolute; top: 98px;
                    left: 14px; font-style: italic;" Font-Bold="True" Font-Names="arial" Font-Size="10pt">Servizio DGR</asp:Label>
                <asp:Label ID="lblServizio11" runat="server" Style="position: absolute; top: 53px;
                    left: 14px; font-style: italic;" Font-Bold="True" Font-Names="arial" Font-Size="10pt">Voce Business Plan</asp:Label>
                <asp:Label ID="lblLotto" runat="server" Style="position: absolute; top: 162px; left: 14px;"
                    Font-Bold="True" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False"></asp:Label>
                <asp:Label ID="lblTotaleLordo" runat="server" Style="position: absolute; top: 194px;
                    left: 14px;" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False">Totale Lotto:</asp:Label>
                <asp:Label ID="Label2" runat="server" Style="position: absolute; top: 194px; left: 191px;"
                    Font-Bold="True" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False">Totale Voce BP:</asp:Label>
                <asp:Label ID="lblTotaleVoce" runat="server" Style="position: absolute; top: 194px;
                    left: 284px; width: 121px;" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Font-Strikeout="False">0,00</asp:Label>
                <asp:Label ID="lblApprovato" runat="server" Style="position: absolute; top: 194px;
                    left: 440px;" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Font-Strikeout="False"
                    Visible="False">Approvato per la Voce selezionata:</asp:Label>
                <asp:Label ID="TotaleLordo" runat="server" Style="position: absolute; top: 194px;
                    left: 90px; width: 94px;" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Font-Strikeout="False">0,00</asp:Label>
                <asp:Label ID="TotaleLordoRichiesto" runat="server" Style="position: absolute; top: 194px;
                    left: 645px; width: 136px;" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Font-Strikeout="False" ForeColor="Red" Visible="False"></asp:Label>
                <asp:Label ID="lblServizio" runat="server" Style="position: absolute; top: 116px;
                    left: 14px; bottom: 572px;" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Font-Strikeout="False"></asp:Label>
                <asp:Label ID="lblVoce" runat="server" Style="position: absolute; top: 72px; left: 14px;"
                    Font-Bold="True" Font-Names="arial" Font-Size="9pt"></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:ImageButton ID="ImgProcedi0" runat="server" ImageUrl="~/NuoveImm/Img_Precedente.png"
                    Style="left: 448px; position: absolute; top: 546px; height: 20px;" TabIndex="4"
                    OnClientClick="ConfermaIndietro();" />
                <asp:ImageButton ID="imgRefresh" runat="server" ImageUrl="~/NuoveImm/Img_Refresh.png"
                    Style="left: 15px; position: absolute; top: 546px; height: 20px; right: 683px;"
                    TabIndex="4" ToolTip="Aggiorna i valori della tabella" />
                <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                    Style="left: 575px; position: absolute; top: 546px;" TabIndex="4" OnClientClick="Aggiorna();" />
                <div id="ContenitoreVoci" style="overflow: auto; width: 770px; height: 282px; position: absolute;
                    top: 219px; left: 14px;">
                    <asp:DataGrid Style="z-index: 105; top: 0px; left: 0px; width: 96%;" AutoGenerateColumns="False"
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                        ID="DataGridVoci" PageSize="50" runat="server">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="id" Visible="False" DataField="ID" ReadOnly="True">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="SOTTO VOCE DGR">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IVA" HeaderText="IVA" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTOCONSUMO" HeaderText="IMPORTOCONSUMO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IVACONSUMO" HeaderText="IVACONSUMO" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PERC_REVERSIBILITA" HeaderText="REVERSIBILITA" ReadOnly="True"
                                Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="IMPORTO">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right;"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>' Width="70px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtImporto"
                                        Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                                        ValidationExpression="^\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="IVA">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbIva" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        TabIndex="7" Width="60px">
                                    </asp:DropDownList>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="IMPORTO CONSUMO" Visible="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtImportoConsumo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTOCONSUMO") %>'
                                        Width="70px"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="IVA CONSUMO" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbIvaConsumo" runat="server" Font-Names="arial" Font-Size="8pt"
                                        TabIndex="7"
                                        Width="60px">
                                    </asp:DropDownList>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REVERSIBILITA'">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRev" runat="server" Font-Names="arial" Font-Size="8pt" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.PERC_REVERSIBILITA") %>'
                                        TabIndex="7" Width="50px" Enabled="False">
                                        <asp:ListItem>0</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>60</asp:ListItem>
                                        <asp:ListItem>70</asp:ListItem>
                                        <asp:ListItem>80</asp:ListItem>
                                        <asp:ListItem>90</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DIVIDI">
                                <ItemTemplate>
                                    <img id="imgdivisione" style="cursor: pointer" alt="Dividi l'importo di questa voce tra i vari complessi e/o edifici del lotto"
                                        onclick="Dividi(<%# DataBinder.Eval(Container, "DataItem.id") %>);" src="Immagini/40px-Crystal_Clear_action_edit_add.png" /><asp:Label
                                            runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="IDVOCESERVIZIO" HeaderText="IDVOCESERVIZIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIdVoceServizio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Text='<%# DataBinder.Eval(Container, "DataItem.IDVOCESERVIZIO") %>'
                                        Visible="False" Width="0px"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <img id="imgdivisioneEdifici" alt="Indica che l'importo è stato diviso tra Edifici e/o Scale"
                                        onclick="Dividi(<%# DataBinder.Eval(Container, "DataItem.id") %>);" src="Immagini/Visualizza.png" />
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                    </asp:DataGrid>
                </div>
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
                <asp:Image ID="imgEsci" runat="server" Style="position: absolute; top: 546px; left: 669px;
                    cursor: pointer" ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclick="ConfermaEsci();" />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="lblErrore" runat="server" Visible="False" Font-Bold="True" Font-Names="arial"
                    Font-Size="8pt" ForeColor="Red" Style="position: absolute; top: 516px; left: 14px;"></asp:Label>
                <br />
            </td>
        </tr>
        <asp:HiddenField ID="idVoce" runat="server" />
        <asp:HiddenField ID="idLotto" runat="server" />
        <asp:HiddenField ID="txtmodificato" runat="server" />
        <asp:HiddenField ID="idServizio" runat="server" />
        <asp:HiddenField ID="idPianoF" runat="server" />
        <asp:HiddenField ID="indietro" runat="server" />
        <asp:HiddenField ID="reversibilita" runat="server" />
        <asp:HiddenField ID="TIPOIMPIANTO" runat="server" />
        <asp:HiddenField ID="tipolotto" runat="server" />
        <asp:HiddenField ID="PROVENIENZAda" runat="server" Value="0" />
        <asp:HiddenField ID="Modifiche" runat="server" Value="" />
        <asp:HiddenField ID="Divisione" runat="server" Value="" />
        <asp:HiddenField ID="DEL" runat="server" Value="0" />
        <asp:HiddenField ID="VOCEp" runat="server" Value="0" />
        <asp:HiddenField ID="Dividip" runat="server" Value="0" />
        <asp:HiddenField ID="Conferma" runat="server" Value="0" />
        <asp:HiddenField ID="AggiornaOP" runat="server" Value="1" />
    </table>
    </form>
    <script type="text/javascript">
        if ((document.getElementById('Dividip').value == '1') && (document.getElementById('DEL').value == '1')) {
            var appoggio = document.getElementById('VOCEp').value;
            document.getElementById('VOCEp').value = '0';
            document.getElementById('Dividip').value = '0';
            document.getElementById('DEL').value = '0';
            Dividi(appoggio);
        }
        if (document.getElementById('AggiornaOP').value == '1') {
            document.getElementById('AggiornaOP').value = '0';
            if (typeof opener != 'undefined') {
                if (opener.document.getElementById('btnAggiornaOPENER')) {
                    opener.document.getElementById('btnAggiornaOPENER').click();
                }
            }
        }
        
    </script>
</body>
</html>
