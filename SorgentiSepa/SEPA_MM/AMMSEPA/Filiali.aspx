<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Filiali.aspx.vb" Inherits="AMMSEPA_Filiali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Filiali</title>
    <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
    <script type="text/javascript">
        var Selezionato;
        function svuota() {
            document.getElementById('txtNome').value = '';
            document.getElementById('txtIndirizzo').value = '';
            document.getElementById('txtCivico').value = '';
            document.getElementById('txtCap').value = '';
            document.getElementById('txtid').value = '';
            document.getElementById('txtmia').value = 'Nessuna Selezione';
            //document.getElementById('cmbTecnica').value = '-1';
            document.getElementById('TextBox1').value = '0';
            document.getElementById('Dvisibile').value = '0';
            document.getElementById('txtTelefono').value = '';
            document.getElementById('txtTelefonoVerde').value = '';
            document.getElementById('txtFax').value = '';
            document.getElementById('txtReferente').value = '';
            document.getElementById('txtResponsabile').value = '';
            document.getElementById('txtCentro').value = '';
            document.getElementById('txtAcronimo').value = '';
            myOpacity.toggle();
        }

        function ConfermaElimina() {
            var sicuro = window.confirm('Sei sicuro di voler eliminare la struttura selezionata?\nSi ricorda che se usata, la struttura non sarà eliminata comunque!');
            if (sicuro == true) {
                document.getElementById('sicuro').value = '1';
                document.getElementById('txtNome').value = 'x';
                document.getElementById('txtIndirizzo').value = 'x';
                document.getElementById('txtCivico').value = 'x';
                document.getElementById('txtCap').value = '12345';
                document.getElementById('txtTelefono').value = 'x';
            }
            else {
                document.getElementById('sicuro').value = '0';
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
    </style>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Strutture" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="Immagini/SfondoHome.jpg" height="75px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            <div style="width: 100%; overflow: auto; height: 480px;">
                                                <asp:DataGrid ID="DataGridIntLegali" runat="server" AutoGenerateColumns="False" BackColor="#F2F5F1"
                                                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                                                    PageSize="100" Width="100%" BorderColor="Navy" BorderStyle="Solid">
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        ForeColor="#0000C0" Wrap="False" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NOME" HeaderText="NOME" ReadOnly="True" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="SEDE TERRITORIALE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="TIPO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOFILIALE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="INDIRIZZO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="CIVICO">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="CAP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="COMUNE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="TELEFONO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.N_TELEFONO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="FAX">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.N_FAX") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="REF.AMMINISTRATIVO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REF_AMMINISTRATIVO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="RESPONSABILE">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" 
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.RESPONSABILE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" 
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.RESPONSABILE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="C. DI COSTO">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" 
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.CENTRO_DI_COSTO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" 
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.CENTRO_DI_COSTO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="ACRONIMO" HeaderText="ACRONIMO"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                                                    meta:resourcekey="LinkButton1Resource1" Text="Seleziona"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" meta:resourcekey="LinkButton3Resource1"
                                                                    Text="Aggiorna"></asp:LinkButton><asp:LinkButton ID="LinkButton2" runat="server"
                                                                        CausesValidation="False" CommandName="Cancel" meta:resourcekey="LinkButton2Resource1"
                                                                        Text="Annulla"></asp:LinkButton>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td style="width: 5%; vertical-align: top;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <img alt="Aggiungi Interessi Legali" onclick="document.getElementById('TextBox1').value='1';myOpacity.toggle();"
                                                            src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer;" id="IMG1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgModifica" OnClientClick="document.getElementById('TextBox1').value='2'"
                                                            runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png" ToolTip="Modifica Sede Territoriale"
                                                            EnableTheming="True" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                                                            ToolTip="Elimina la voce selezionata" OnClientClick ="ConfermaElimina();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="InserimentoLegali" style="z-index: 199; position:absolute;top: 86px;
                                                left: 478px; width: 632px;
                                                height: 453px; background-repeat: no-repeat; background-color: #C0C0C0;">

                                                <table width="100%" bgcolor="Silver">
                                                <tr align="center" valign="middle">
                                                <td>&nbsp;</td>
                                                </tr>
                                                <tr align="center" valign="middle">
                                                <td>
                                                <table border="0" cellpadding="1" cellspacing="1" style="border: 2px solid #0000FF;">
                                                    <tr>
                                                        <td style="text-align: left" class="style1">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: center" align="center">
                                                            <strong><span style="font-family: Arial; text-align: center;">STRUTTURA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></strong>
                                                        &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 10pt; font-family: Arial; text-align: left;">
                                                            Tipo*
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left">
                                                            <asp:DropDownList ID="cmbTipo" runat="server" Width="420px" AutoPostBack="True" 
                                                                TabIndex="1">
                                                                <asp:ListItem Value="0">SEDE AMMINISTRATIVA</asp:ListItem>
                                                                <asp:ListItem Value="1">SEDE TECNICA</asp:ListItem>
                                                                <asp:ListItem Value="2">SEDE CENTRALE</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 10pt; font-family: Arial; text-align: left;">
                                                            <asp:Label ID="lblTecnica" runat="server" Text="St.Tecnica*"></asp:Label>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left">
                                                            <asp:DropDownList ID="cmbTecnica" runat="server" Width="420px" TabIndex="2">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                        <td style="text-align: left" class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">Nome*</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left">
                                                            <asp:TextBox ID="txtNome" runat="server" Font-Names="Arial" Font-Size="9pt" Width="420px"
                                                                MaxLength="50" TabIndex="3" Height="33px" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNome"
                                                                ErrorMessage="Err.!!" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                        <td style="text-align: left" class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">Indirizzo*</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left">
                                                            <asp:TextBox ID="txtIndirizzo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                Width="420px" MaxLength="50" TabIndex="4"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIndirizzo"
                                                                ErrorMessage="Err.!" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                        <td style="text-align: left" class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">Civico*</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left">
                                                            <asp:TextBox ID="txtCivico" runat="server" Font-Names="Arial" Font-Size="9pt" Width="86px"
                                                                MaxLength="13" TabIndex="5"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCivico"
                                                                ErrorMessage="Obbligatorio" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                        <td style="text-align: left" class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">CAP*</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left">
                                                            <asp:TextBox ID="txtCap" runat="server" Font-Names="Arial" Font-Size="9pt" Width="86px"
                                                                MaxLength="5" TabIndex="6"></asp:TextBox>
                                                            &nbsp;<asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$"
                                                                ControlToValidate="txtCap" ErrorMessage="5 Numeri" Font-Names="arial" Font-Size="8pt"
                                                                TabIndex="303" ID="RegularExpressionValidator2"></asp:RegularExpressionValidator>
                                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCap"
                                                                ErrorMessage="Obbligatorio" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">Comune*</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtComune" runat="server" Font-Names="Arial" Font-Size="9pt" Width="162px"
                                                                MaxLength="35" TabIndex="7">MILANO</asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtComune"
                                                                ErrorMessage="Obbligatorio" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">Telefono*</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtTelefono" runat="server" Font-Names="Arial" Font-Size="9pt" Width="162px"
                                                                MaxLength="50" TabIndex="8"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTelefono"
                                                                ErrorMessage="Obbligatorio" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" style="font-size: 10pt; font-family: Arial">
                                                            N.Verde
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtTelefonoVerde" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                Width="162px" MaxLength="50" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">Fax</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtFax" runat="server" Font-Names="Arial" Font-Size="9pt" Width="162px"
                                                                MaxLength="50" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <span style="font-size: 10pt; font-family: Arial">Ref.Amministrativo</span>
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtReferente" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                Width="420px" MaxLength="50" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" style="font-size: 10pt; font-family: Arial">
                                                            Responsabile
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtResponsabile" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                Width="420px" MaxLength="50" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" style="font-size: 10pt; font-family: Arial">
                                                            C. di costo
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtCentro" runat="server" Font-Names="Arial" Font-Size="9pt" Width="420px"
                                                                MaxLength="50" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" style="font-size: 10pt; font-family: Arial">
                                                            Acronimo
                                                        </td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtAcronimo" runat="server" Font-Names="Arial" Font-Size="9pt" Width="420px"
                                                                MaxLength="50" TabIndex="13"></asp:TextBox>
                                                            &nbsp; &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" style="font-size: 10pt; font-family: Arial">
                                                            Procura</td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            <asp:TextBox ID="txtprocura" runat="server" Font-Names="Arial" Font-Size="9pt" Width="420px"
                                                                MaxLength="4000" TabIndex="14" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" style="font-size: 10pt; font-family: Arial">
                                                            &nbsp;</td>
                                                        <td style="width: 469px; height: 19px; text-align: left;">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            *Obbligatorio
                                                        </td>
                                                        <td align="right" style="width: 469px; height: 19px; text-align: right">
                                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 110%; text-align: left;">
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="../NuoveImm/Img_SalvaVal.png"
                                                                            TabIndex="15" />&nbsp;<img id="btnAnnulla" alt="" src="../NuoveImm/Img_AnnullaVal.png"
                                                                                style="cursor: pointer" onclick="svuota();" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td>&nbsp; &nbsp;</td>
                                                </tr>
                                                </table>
                                                
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;
                                <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                                    meta:resourcekey="TextBox3Resource1" Text="Nessuna Selezione" Width="482px" BackColor="#F2F5F1"
                                    BorderWidth="0px"></asp:TextBox>
                            </td>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%">
                                            &nbsp;
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Red" Height="16px" Visible="False" Width="525px"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
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
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    <asp:HiddenField ID="txtid" runat="server" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="Dvisibile" runat="server" Value='0' />
    <asp:HiddenField ID="sicuro" runat="server" Value='0' />
    <script type="text/javascript">

        myOpacity = new fx.Opacity('InserimentoLegali', { duration: 200 });
        //myOpacity.hide();

        if (document.getElementById('Dvisibile').value == '0') {
            myOpacity.hide();
        }
    </script>
    </form>
</body>
</html>
