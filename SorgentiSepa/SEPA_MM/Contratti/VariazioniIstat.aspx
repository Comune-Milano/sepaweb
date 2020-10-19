<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VariazioniIstat.aspx.vb" Inherits="Contratti_VariazioniIstat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>

</head>
<body>

    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px; height: 537px;">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Variazioni
                        Istat</strong></span><br />
                    <br />
                    <br />
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;&nbsp;<br />
                    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<br />
                    <br />
                    &nbsp;<br />
                    &nbsp; &nbsp;<br />
                    &nbsp; &nbsp;
                                        <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    &nbsp;&nbsp;
                    <div style="left: 14px; overflow: auto; width: 778px; position: absolute; top: 56px;
                        height: 324px">
                        <br />
                        <asp:DataGrid ID="DataGridVarIstat" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Height="168px"
                            meta:resourcekey="DataGrid1Resource1" PageSize="100" Style="z-index: 101; left: 483px;
                            top: 68px" Width="755px">
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
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_VALIDITA" HeaderText="DATA" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="DATA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_VALIDITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDICE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDICE_NAZIONALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="BASE ">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BASE_INDICE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VAR.ANNUALE%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VAR_100_ANNUALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VAR.BIENNALE%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VAR_100_BIENNALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VAR.ANNUALE 75%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VAR_75_ANNUALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NUM. GAZZETTA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NRO_GAZZETTA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA GAZZETTA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_GAZZETTA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
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
                        </asp:DataGrid></div>
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Height="16px" Style="z-index: 104; left: 11px; position: absolute;
                        top: 511px" Visible="False" Width="525px"></asp:Label>
                    <br /><img id="Img2" alt="Aggiungi Variazione ISTAT" onclick="document.getElementById('TextBox1').value='1';document.getElementById('InsVariazione').style.visibility='visible';"
                        src="../NuoveImm/Img_Aggiungi.png" style="left: 587px; cursor: pointer; position: absolute;
                        top: 395px" /><asp:ImageButton ID="ImgModifica"  runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                        Style="left: 649px; position: absolute; top: 395px" ToolTip="Modifica Variazione ISTAT" EnableTheming="True" CausesValidation="False" OnClientClick="document.getElementById('TextBox1').value='2';document.getElementById('InsVariazione').style.visibility='visible';"/>
                    <br />
                    <br />
                    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                        meta:resourcekey="TextBox3Resource1" Style="border-right: white 1px solid; border-top: white 1px solid; left: 12px; border-left: white 1px solid; border-bottom: white 1px solid;
                        position: absolute; top: 393px" Text="Nessuna Selezione" Width="482px"></asp:TextBox>
                    <asp:ImageButton ID="ImgBtnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                        Style="left: 709px; position: absolute; top: 395px" ToolTip="Elimina Variazione ISTAT Selezionata" />
                    &nbsp;&nbsp;
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <div id="InsVariazione" style="display: block; left: 0px; width: 800px; position: absolute;
            top: 0px; height: 573px; background-color: #ffffff; text-align: left; z-index: 1;">
            <span style="font-family: Arial"></span>
            <br />
            <br />
            <table border="0" cellpadding="1" cellspacing="1" style="left: 101px; width: 76%;
                position: absolute; top: 109px; background-color: gainsboro; height: 343px; border-right: blue 2px solid; border-top: blue 2px solid; border-left: blue 2px solid; border-bottom: blue 2px solid;">
                <tr>
                    <td style="width: 176px; height: 19px; text-align: left">
                        <strong><span style="font-family: Arial">Variazioni </span></strong></td>
                    <td style="width: 363px; height: 19px; text-align: left">
                        <strong><span style="font-family: Arial">ISTAT</span></strong></td>
                    <td style="width: 170px; height: 19px; text-align: left">
                    </td>
                    <td style="width: 274px; height: 19px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 176px; height: 19px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Data*</span></td>
                    <td style="width: 363px; height: 19px; text-align: left">
                        <asp:TextBox ID="TxtData" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                            ToolTip="Data inerente alla Variazione ISTAT (gg/MM/yyyy)" Width="76px" 
                            TabIndex="1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtData"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                    <td style="width: 170px; height: 19px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Indice*</span></td>
                    <td style="width: 274px; height: 19px; text-align: left">
                        <asp:TextBox ID="TxtIndice" runat="server" Font-Names="Arial" Font-Size="9pt" 
                            MaxLength="10" Width="49px" ToolTip="Indice della Variazione ISTAT" 
                            TabIndex="2"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TxtIndice"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                            ValidationExpression="-?\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"></asp:RegularExpressionValidator></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="width: 176px; height: 19px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Base Ind.*</span></td>
                    <td style="width: 363px; height: 19px; text-align: left">
                        <asp:TextBox ID="txtBaseIndice" runat="server" Font-Names="Arial" 
                            Font-Size="9pt" Width="48px" MaxLength="4" ToolTip="Anno base indice (aaaa)" 
                            TabIndex="3"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBaseIndice"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ValidationExpression="\d{4}"></asp:RegularExpressionValidator></td>
                    <td style="width: 170px; height: 19px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Var. Annuale 100%*</span></td>
                    <td style="width: 274px; height: 19px; text-align: left">
                        <asp:TextBox ID="TxtVarAnn100" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="10" Width="48px" ToolTip="Variazione Annuale su 100%" 
                            TabIndex="4"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtVarAnn100"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                            ValidationExpression="-?\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"></asp:RegularExpressionValidator></td>
                </tr>
                <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                    <td style="width: 176px; height: 19px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Var. Biennale 100%</span></td>
                    <td style="width: 363px; height: 19px; text-align: left">
                        <asp:TextBox ID="TxtVarBienn" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Width="48px" MaxLength="10" ToolTip="Variazione biennale al 100%" 
                            TabIndex="5"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtVarBienn"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                            ValidationExpression="-?\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"></asp:RegularExpressionValidator></td>
                    <td style="width: 170px; height: 19px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Var. Annuale 75%</span></td>
                    <td style="width: 274px; height: 19px; text-align: left">
                        <asp:TextBox ID="TxtVarAnn75" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Width="48px" MaxLength="10" ToolTip="Variazione Annuale al 75%" 
                            TabIndex="6"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtVarAnn75"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                            ValidationExpression="-?\d+(\.\d\d\d)?(,\d\d\d)?(\.\d)?(,\d)?"></asp:RegularExpressionValidator></td>
                </tr>
                <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                    <td style="width: 176px; height: 18px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">N.ro Gazzetta</span></td>
                    <td style="width: 363px; height: 18px; text-align: left">
                        <asp:TextBox ID="txtNumGazzetta" runat="server" Font-Names="Arial" 
                            Font-Size="9pt" Width="48px" MaxLength="10" ToolTip="Numero Gazzetta" 
                            TabIndex="7"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtNumGazzetta"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
                    <td style="width: 170px; height: 18px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Data Gazzetta</span></td>
                    <td style="width: 274px; height: 18px; text-align: left">
                        <asp:TextBox ID="txtDataGazz" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Data Gazzetta (dd/MM/yyyy)"
                            Width="76px" MaxLength="10" TabIndex="8"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataGazz"
                            ErrorMessage="Errore" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="width: 176px; height: 19px">
                        &nbsp;</td>
                    <td style="width: 363px; height: 19px">
                        &nbsp;</td>
                    <td style="width: 170px; height: 19px">
                                    <asp:ImageButton ID="img_InserisciSchema" runat="server" 
                                        ImageUrl="~/NuoveImm/Img_SalvaVal.png" ToolTip="Salva i dati " TabIndex="9" /></td>
                    <td style="width: 274px; height: 19px">
                        <asp:ImageButton
                                            ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                            
                            OnClientClick="document.getElementById('TextBox1').value='0';document.getElementById('InsVariazione').style.visibility='hidden';" 
                            ToolTip="Annulla operazione" TabIndex="10" /></td>
                </tr>
            </table>
            <asp:HiddenField ID="txtid" runat="server" />
        </div>
    
    </div>
        <asp:HiddenField ID="TextBox1" runat="server" />
        <script type="text/javascript">

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
            
        if (document.getElementById('TextBox1').value!='2') {
        document.getElementById('InsVariazione').style.visibility='hidden';
        }
        </script>
                                   <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
    </form>

</body>

</html>
