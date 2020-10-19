<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabMillesimalil.ascx.vb"
    Inherits="Condomini_TabMillesimalil" %>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
    <style type="text/css">
        .styleMilles
        {
            font-family: Arial;
            font-size: 8pt;
            vertical-align:top;
            text-align :left;
        }
    </style>
<table>
    <tr style="vertical-align: top; text-align: left;">
        <td style="vertical-align: top; text-align: left;" >
            <table cellpadding="0" cellspacing="0" style="width: 97%; ">
                <tr>
                    <td  class="styleMilles" style = "width:15%">
                        Mill.Prop.Comune</td>
                    <td class="styleMilles" style="width:19%">
                        <asp:TextBox ID="txtMilPropComune" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="11" Width="86px" ReadOnly="True"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMilPropComune"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles" style="width:16%">
                        Mill. TOT. Condominio</td>
                    <td style= "width:19%">
                        <asp:TextBox ID="txtMilProp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; text-align: right;" TabIndex="11" Width="75px" Font-Strikeout="False"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMilProp"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,10}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles" style="width:9%">
                        Percentuale</td>
                    <td style="width: 19%">
                        <asp:TextBox ID="txtMilPropComunePerc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="16" Style="z-index: 107; right: 628px;
                            left: 351px; top: 72px; text-align: right;" TabIndex="12" Width="75px" ReadOnly="True"></asp:TextBox><asp:Label
                                ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 5px; top: 25px">%</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="styleMilles">
                        Mill. Compr. Comune</td>
                    <td class="styleMilles">
                        <asp:TextBox ID="txtMillCompComune" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="13" Width="86px" ReadOnly="True"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMillCompComune"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Mill. TOT. Condominio</td>
                    <td>
                        <asp:TextBox ID="txtMillComp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; text-align: right;" TabIndex="14" Width="75px"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtMillComp"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Percentuale</td>
                    <td>
                        <asp:TextBox ID="txtMillCompComunePerc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="16" Style="z-index: 107; right: 628px;
                            left: 351px; top: 72px; text-align: right;" TabIndex="15" Width="75px" ReadOnly="True"></asp:TextBox><asp:Label
                                ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 5px; top: 25px">%</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="styleMilles">
                        Mill./Sup Comune</td>
                    <td>
                        <asp:TextBox ID="TxtMillSupComune" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="16" Width="86px" ReadOnly="True"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtMillSupComune"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Mill/Sup TOT Cond.</td>
                    <td>
                        <asp:TextBox ID="TxtMillSup" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="17" Width="75px"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator8" runat="server" ControlToValidate="TxtMillSup"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Percentuale</td>
                    <td>
                        <asp:TextBox ID="TxtMillSupComunePerc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; text-align: right;" TabIndex="18" Width="75px" ReadOnly="True"></asp:TextBox><asp:Label
                                ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 5px; top: 25px">%</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="styleMilles">
                        Mill. Gest. Comune</td>
                    <td>
                        <asp:TextBox ID="txtMillGestComune" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="19" Width="86px" ReadOnly="True"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtMillGestComune"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Mill. TOT Condominio</td>
                    <td>
                        <asp:TextBox ID="txtMillGest" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="20" Width="75px"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtMillGest"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Percentuale</td>
                    <td>
                        <asp:TextBox ID="txtMillGestComunePerc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; text-align: right;" TabIndex="21" Width="75px" ReadOnly="True"></asp:TextBox><asp:Label
                                ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 5px; top: 25px">%</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="styleMilles">
                        Mill. Pres. Assemblea</td>
                    <td>
                        <asp:TextBox ID="txtMillPresComune" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="22" Width="86px" ReadOnly="True"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                            ControlToValidate="txtMillGestComune" ErrorMessage="N,0000" Font-Bold="True"
                            Font-Names="Arial" Font-Size="8pt" Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                            ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Mill.TOT Pres.Assemb.</td>
                    <td>
                        <asp:TextBox ID="txtMillPres" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="23" Width="75px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                            ControlToValidate="txtMillGest" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                            ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="styleMilles">
                        Percentuale</td>
                    <td>
                        <asp:TextBox ID="txtMillPresComunePerc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; text-align: right;" TabIndex="24" Width="75px" ReadOnly="True"></asp:TextBox>
                        <asp:Label ID="Label38" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 5px; top: 25px">%</asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="vertical-align: top; text-align: left;">
        <td style="vertical-align: top; text-align: left;" colspan="0" rowspan="0" >
            <div id="TabMillSE" class="tabber " 
                style="padding: 0px; margin: 0px; text-align: left; visibility: visible;">
                <div class="tabbertab" title="Scale">
                    <table>
                        <tr>
                            <td style="vertical-align: top; text-align: left">
                                <div style="border: 2px solid #ccccff; vertical-align: top; text-align: left; height: 100px;
                                    overflow: auto; width: 698px;">
                                    <asp:DataGrid Style="z-index: 105; left: 8px; top: 32px" ID="DataGridScaleMill" Font-Size="8pt"
                                        Font-Names="Arial" Font-Bold="False" runat="server" BorderWidth="1px" CellSpacing="1"
                                        BorderColor="Black" PageSize="1" GridLines="Vertical" Font-Underline="False"
                                        Font-Strikeout="False" Font-Overline="False" Font-Italic="False" BackColor="White"
                                        AutoGenerateColumns="False" Width="743px">
                                        <PagerStyle Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID_SCALA" HeaderText="ID_SCALA" Visible="False">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="SCALA" Visible="False">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="COD. EDIFCIO">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label32" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_EDIFICIO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="EDIFICIO">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label33" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EDIFICIO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="SCALA">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="MIL_ASC_TOT" HeaderText="ASC. COM.">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIL_ASC_COND" HeaderText="ASC. COND">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PERCENTASC" HeaderText="%">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIL_PRO_TOT" HeaderText="PROP. COM.">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIL_PROP_COND" HeaderText="PROP. COND.">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PERCENTPROP" HeaderText="%">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                            ForeColor="#0000C0" Wrap="False" />
                                    </asp:DataGrid></div>
                                <asp:TextBox Style="left: 13px; top: 197px" ID="txtmia" Font-Size="8pt" Font-Names="Arial"
                                    Font-Bold="True" runat="server" ReadOnly="True" Width="572px" MaxLength="100"
                                    BorderStyle="None" BorderColor="White" BackColor="White" ForeColor="Black">Nessuna Selezione</asp:TextBox>
                            </td>
                            <td style="vertical-align: top; text-align: left">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton Style="z-index: 102; left: 392px; top: 387px" ID="btnVisualizza"
                                                runat="server" ToolTip="Modifica " CausesValidation="False" ImageUrl="Immagini/pencil-icon.png"
                                                OnClientClick="document.getElementById('splash').style.visibility = 'visible';">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" ImageUrl="Immagini/Delete-icon.png"
                                                Style="z-index: 102; left: 392px; top: 387px" OnClientClick="ConfermaElimina();"
                                                ToolTip="Rimuovi Scala" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab " title="Edifici">
                    <table style="width: 741px;">
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 693px; text-align: left">
                                <div style="border: 2px solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                                    width: 700px; top: 0px; text-align: left; height: 100px;">
                                    <asp:DataGrid Style="z-index: 105; left: 8px; top: 32px" ID="DataGridFabbMill" Font-Size="8pt"
                                        Font-Names="Arial" Font-Bold="False" runat="server" Width="699px" BorderWidth="1px"
                                        CellSpacing="1" BorderColor="Black" PageSize="1" GridLines="Vertical" Font-Underline="False"
                                        Font-Strikeout="False" Font-Overline="False" Font-Italic="False" BackColor="White"
                                        AutoGenerateColumns="False">
                                        <PagerStyle Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FABBRICATO" HeaderText="FABBRICATO" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="COD. EDIFICIO">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_EDIFICIO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="FABBRICATO">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FABBRICATO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PROP. COMUNE">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PROP_COM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="MILL. PROP. COND.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_PROP_COND") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label31" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PERCENTPROP") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                            ForeColor="#0000C0" Wrap="False" />
                                    </asp:DataGrid></div>
                                <asp:TextBox Style="left: 13px; top: 197px" ID="txtmiaFabb" Font-Size="8pt" Font-Names="Arial"
                                    Font-Bold="True" runat="server" ReadOnly="True" Width="572px" MaxLength="100"
                                    BorderStyle="None" BorderColor="White" BackColor="White" ForeColor="Black">Nessuna Selezione</asp:TextBox>
                            </td>
                            <td style="vertical-align: top; text-align: left">
                                <asp:ImageButton Style="z-index: 102; left: 392px; top: 387px" ID="BtnVisualEdif"
                                    runat="server" ToolTip="Modifica" CausesValidation="False" ImageUrl="Immagini/pencil-icon.png"
                                    OnClientClick="document.getElementById('splash').style.visibility = 'visible';">
                                </asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </td>
    </tr>
    <tr style="vertical-align: top; text-align: left;">
        <td style="vertical-align: top; text-align: left;" >
            <table cellpadding="0" cellspacing="0">
                <tr style="vertical-align: top; text-align: left;">
                    <td style="vertical-align: top; text-align: left; width: 6%;" 
                        class="styleMilles" >
                        Tot Scale</td>
                    <td style="vertical-align: top; width: 71px; text-align: left;">
                        <asp:TextBox ID="TxtTotScale" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="25" Width="50px"></asp:TextBox>
                    </td>
                    <td style="vertical-align: top; text-align: left; width: 8%;" 
                        class="styleMilles" >
                        Tot Alloggi</td>
                    <td style="vertical-align: top; text-align: left; width:12%" >
                        <asp:TextBox ID="TxtTotAlloggi" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="26" Width="50px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                            ControlToValidate="TxtTotAlloggi" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                            ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                    <td style="vertical-align: top; text-align: left; width: 6%;" 
                        class="styleMilles" >
                        Tot Box</td>
                    <td style="vertical-align: top; text-align: left;width:12%" >
                        <asp:TextBox ID="txtTotBox" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; text-align: right;" TabIndex="27" Width="50px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                            ControlToValidate="txtTotBox" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                            ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                    <td style="vertical-align: top; text-align: left; width: 8%;" 
                        class="styleMilles" >
                        Tot Negozi</td>
                    <td style="vertical-align: top; text-align: left; width: 12%;">
                        <asp:TextBox ID="TxtTotNegozi" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; text-align: right;" TabIndex="28" Width="50px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                            ControlToValidate="TxtTotNegozi" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                            ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                    <td style="vertical-align: top; text-align: left; width: 8%;" 
                        class="styleMilles" >
                        Tot Diversi</td>
                    <td style="vertical-align: top; width: 283px; text-align: left">
                        &nbsp;<asp:TextBox ID="txtTotDiversi" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; text-align: right;" TabIndex="29" Width="50px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                            ControlToValidate="txtTotDiversi" ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                            ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div id="MillScale" style="border: thin none #3366ff; z-index: 201; left: 0px; width: 802px;
    position: absolute; top: 0px; height: 582px; background-color: #dedede; vertical-align: top;
    text-align: left; visibility: hidden; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    margin-right: 10px;">
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="111px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 100; left: 48px; position: absolute; top: 163px" Width="699px" />
    <br />
    <br />
    <br />
    <br />
    &nbsp;
    <table style="width: 80%; z-index: 200; left: 73px; position: absolute; top: 171px;">
        <tr>
            <td style="width: 92px; height: 16px;">
                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 104; left: 10px; top: 104px">Scala</asp:Label>
            </td>
            <td style="width: 166px; height: 16px;">
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 104; left: 10px; top: 104px" Width="146px">Millesimi Propietà Condominio</asp:Label>
            </td>
            <td style="width: 144px; height: 16px">
                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 104; left: 10px; top: 104px" Width="165px">Millesimi Ascensore Condominio</asp:Label>
            </td>
            <td style="height: 16px">
            </td>
        </tr>
        <tr>
            <td style="width: 92px; height: 24px;">
                <asp:Label ID="lblScala" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                    Style="z-index: 104; left: 5px; top: 25px"></asp:Label>
            </td>
            <td style="width: 166px; height: 24px;">
                <asp:TextBox ID="txtPropCondomominio" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 105; left: 10px;
                    top: 72px; text-align: right" TabIndex="23" Width="75px"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtPropCondomominio"
                        ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                        top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 144px; height: 24px;">
                <asp:TextBox ID="txtMilScaleCondominio" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 105; left: 10px;
                    top: 72px; text-align: right" TabIndex="24" Width="75px"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtMilScaleCondominio"
                        ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                        top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
            </td>
            <td style="height: 24px">
            </td>
        </tr>
        <tr>
            <td style="width: 92px; height: 22px;">
            </td>
            <td style="width: 166px; height: 22px;">
            </td>
            <td style="width: 144px; height: 22px">
            </td>
            <td style="height: 22px">
                <asp:ImageButton ID="btnSalvaCambioAmm" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                    TabIndex="25" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                <img id="imgCambiaAmm0" alt="Aiuto Ricerca per Denominazione Edificio" onclick="document.getElementById('TextBox3').value = '1';myOpacity3.toggle();"
                    src="../NuoveImm/Img_AnnullaVal.png" style="left: 185px; cursor: pointer; top: 23px;
                    z-index: 300;" />
            </td>
        </tr>
    </table>
</div>
<div id="MillFab" style="border: thin none #3366ff; z-index: 201; left: 0px; width: 802px;
    position: absolute; top: 0px; height: 582px; background-color: #dedede; vertical-align: top;
    text-align: left; visibility: hidden; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    margin-right: 10px;">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    &nbsp;
    <table style="width: 81%; z-index: 200; left: 80px; position: absolute; top: 181px;">
        <tr>
            <td style="width: 270px; height: 16px;">
                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 104; left: 10px; top: 104px">Fabbricato</asp:Label>
            </td>
            <td style="width: 166px; height: 16px;">
                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 104; left: 10px; top: 104px" Width="146px">Millesimi Propietà Condominio</asp:Label>
            </td>
            <td style="height: 16px">
            </td>
        </tr>
        <tr>
            <td style="width: 270px; height: 24px;">
                <asp:Label ID="LblFabbricato" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="9pt" Style="z-index: 104; left: 5px; top: 25px" Width="100%"></asp:Label>
            </td>
            <td style="width: 166px; height: 24px;">
                <asp:TextBox ID="txtPropCondFabb" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 105; left: 10px;
                    top: 72px; text-align: right" TabIndex="23" Width="75px"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPropCondomominio"
                        ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                        top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
            </td>
            <td style="height: 24px">
            </td>
        </tr>
        <tr>
            <td style="width: 270px; height: 22px;">
            </td>
            <td style="width: 166px; height: 22px;">
            </td>
            <td style="height: 22px">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                    TabIndex="25" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                <img id="Img1" alt="Annulla" onclick="document.getElementById('TextBox5').value ='1';myOpacityFabb.toggle();"
                    src="../NuoveImm/Img_AnnullaVal.png" style="left: 185px; cursor: pointer; top: 23px" />
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="111px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 100; left: 48px; position: absolute; top: 163px" Width="699px" />
</div>
<script type="text/javascript">
    myOpacity3 = new fx.Opacity('MillScale', { duration: 200 });
    //myOpacity.hide();
    if (document.getElementById('TextBox3').value != '2') {
        myOpacity3.hide(); ;
    }
    //                    function PulisciCampiMillesimo() {
    //                        document.getElementById('TabMillesimalil1_txtPropCondomominio').value = ""
    //                        document.getElementById('TabMillesimalil1_txtMilScaleCondominio').value = ""
    //                    }

    myOpacityFabb = new fx.Opacity('MillFab', { duration: 200 });
    //myOpacity.hide();
    if (document.getElementById('TextBox5').value != '2') {
        myOpacityFabb.hide(); ;
    }

</script>
<asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
