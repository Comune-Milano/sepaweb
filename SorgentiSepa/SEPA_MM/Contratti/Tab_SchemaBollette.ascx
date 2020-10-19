<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SchemaBollette.ascx.vb" Inherits="Contratti_Tab_SchemaBollette" %>
<div style="left: 8px; width: 1130px; position: absolute; top: 168px; height: 520px">
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="SCHEMA VOCI BOLLETTA" Width="649px" Height="16px"></asp:Label><br />
                <table style="border-color: #C0C0C0; border-top-width: 3px; border-left-width: 3px;
                    border-bottom-width: 3px; width: 100%; border-right-width: 3px;">
                    <tr>
                        <td style="width: 86%">
                            &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            &nbsp;&nbsp;
                            </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 86%">
                            <div id="DivRoot3" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow3">
    </div>
    <div style="width: 1000px;overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent3" >
        <asp:DataGrid ID="DataGridSchema" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="30" Style="z-index: 101; width: 100%;" TabIndex="1" 
            BorderWidth="0px" CellPadding="2">
            <AlternatingItemStyle BackColor="#CCCCCC" BorderStyle="None" />
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID_SCHEMA" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE" ReadOnly="True">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Names="Courier New" 
                        Font-Size="8pt" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Names="Courier New" 
                        Font-Size="8pt" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTO_SINGOLA_RATA" HeaderText="IMPORTO">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DA_RATA" HeaderText="DA RATA">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PER_RATE" HeaderText="PER RATE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Italic="False" Font-Names="Courier New"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="White" Wrap="False" Height="25px" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
    </div>

    <div id="DivFooterRow3" style="overflow:hidden">
    </div>
</div>
                        </td>
                        <td style="width: 100%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td height="20">
                                        <img id="nuovoschema" alt="Aggiungi Voce nello Schema" onclick="NuovoSchema();" src="../NuoveImm/img_Aggiungi.png"
                                            style="cursor: pointer"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20">
                                        <asp:ImageButton ID="img_ModificaSchema" runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                                            OnClientClick="document.getElementById('USCITA').value='1';" ToolTip="Modifica Voce dallo Schema"
                                            TabIndex="69" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20">
                                        <asp:ImageButton ID="img_EliminaOspite" runat="server" ImageUrl="~/NuoveImm/img_Elimina.png"
                                            OnClientClick="document.getElementById('USCITA').value='1';" ToolTip="Elimina Voce dallo Schema"
                                            TabIndex="69" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblVociAutomatiche" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="top">
                            <asp:Label ID="lblProssimaEmissione" runat="server" Font-Names="arial" Font-Size="8pt"
                                Style="font-weight: 700">Prossima Emissione:</asp:Label>
                        
                            <asp:ImageButton ID="imgModificaPS" runat="server" ImageUrl="~/Images/icon_edit.jpg"
                                OnClientClick="document.getElementById('USCITA').value = '1';window.showModalDialog('ProssimaBolletta.aspx?ID=' + document.getElementById('Tab_SchemaBollette1_txtIdContratto').value + '&amp;CN=' + document.getElementById('Tab_SchemaBollette1_txtConnessione').value , window, 'status:no;dialogWidth:400px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');"
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVociOneri" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
<td valign="middle">
        
            <table style="width:100%;">
                <tr>
                    <td>
        <asp:Label ID="lblSindaCato" runat="server" Font-Names="arial" 
                Font-Size="8pt">Sindacato di Riferimento:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="lstSindacati" runat="server" Font-Names="arial" 
                            Font-Size="8pt">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        
        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div id="InserimentoSchema" style="left: 0px; width: 1160px; position: absolute; top: 0px;
    height: 780px; text-align: left; background-repeat: no-repeat; background-color: #c3c3bb;
    visibility: hidden; z-index: 600;">
    <span style="font-family: Arial"></span>
    <br />
    <br />
    <table cellpadding="1" cellspacing="1" style="border-style: inherit; width: 435px;
        left: 243px; position: absolute; top: 214px; background-color: #FFFFFF; z-index: 700;"
        border="0">
        <tr>
            <td style="width: 52px; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">Voce</span></strong>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">Schema</span></strong>
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px; text-align: left">
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px; text-align: left">
                <span style="font-size: 10pt; font-family: Arial">Anno</span>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <asp:DropDownList ID="cmbAnnoSchema" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="341px" TabIndex="700">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px; text-align: left">
                <span style="font-size: 10pt; font-family: Arial">Voce</span>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <asp:DropDownList ID="cmbVoceSchema" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="341px" TabIndex="701">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px; text-align: left">
                <span style="font-size: 10pt; font-family: Arial">Importo</span>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <asp:TextBox ID="txtImportoVoce" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="74px" TabIndex="702"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtImportoVoce"
                    ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td style="width: 52px; height: 19px; text-align: left">
                <span style="font-size: 10pt; font-family: Arial">Da Rata</span>
            </td>
            <td style="width: 300px; height: 19px; text-align: left">
                <asp:TextBox ID="txtDaRata" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Da Rata"
                    Width="48px" TabIndex="703"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDaRata"
                    ErrorMessage="Errore" Font-Bold="True" Font-Names="arial" Font-Size="9pt" TabIndex="303"
                    ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px">
                <span style="font-size: 10pt; font-family: Arial">Per Rate</span>
            </td>
            <td style="width: 300px; height: 19px">
                <asp:TextBox ID="txtPerRate" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Da Rata"
                    Width="48px" TabIndex="704"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPerRate"
                    ErrorMessage="Errore" Font-Bold="True" Font-Names="arial" Font-Size="9pt" TabIndex="303"
                    ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px">
                &nbsp;
            </td>
            <td style="width: 300px; height: 19px; font-family: arial; font-size: 8pt;">
                Se il numero delle rate è superiore a 12, usare l'anno successivo.
            </td>
        </tr>
        <tr>
            <td style="width: 52px; height: 19px">
            </td>
            <td style="width: 300px; height: 19px; text-align: right;" align="right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="img_SalvaSchema" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';" ToolTip="Salva la voce nello schema"
                                TabIndex="705" Visible="False" />
                            <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';" ToolTip="Inserisci la nuova voce nello schema"
                                TabIndex="705" />&nbsp;<img id="ImgChiudiSchema" alt="" src="../NuoveImm/Img_AnnullaVal.png"
                                    onclick="document.getElementById('USCITA').value='0';document.getElementById('Tab_SchemaBollette1_txtImportoVoce').value='';document.getElementById('Tab_SchemaBollette1_txtPerRate').value='';document.getElementById('Tab_SchemaBollette1_txtDaRata').value='';myOpacity2.toggle();"
                                    style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" ImageUrl="~/ImmDiv/SfondoDim1.jpg"
        Style="z-index: 199; position: absolute; top: 154px; left: 213px;" BackColor="White" />
    <asp:HiddenField ID="txtIdContratto" runat="server" />
    <asp:HiddenField ID="txtIdUnita" runat="server" />
    <asp:HiddenField ID="V1" runat="server" />
    <asp:HiddenField ID="txtConnessione" runat="server" />
    <asp:HiddenField ID="txtAppare" runat="server" />
    <asp:HiddenField ID="IdBolSchema" runat="server" />
    <asp:HiddenField ID="VDescr" runat="server" />
    <asp:HiddenField ID="inserimento" runat="server" />
</div>
<script type="text/javascript">
    document.getElementById('InserimentoSchema').style.visibility = 'hidden';

    function NuovoSchema() {
        if (document.getElementById('VisNuovoContratto').value == '0') {
            if (document.getElementById('HStatoContratto').value != 'CHIUSO') {
                document.getElementById('USCITA').value = '1';
                document.getElementById('Tab_SchemaBollette1_inserimento').value = '1';
                myOpacity2.toggle();
            }
            else {
                alert('Operazione non possibile al momento!');
            }
        }
        else {
            alert('Operazione non possibile al momento! Salvare il contratto prima di aggiungere nuovi voci.');
        }
    }

    
</script>
