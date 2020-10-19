<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsistenzaUC.aspx.vb" Inherits="MANUTENZIONI_ConsistenzaUC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CONSISTENZA</title>
</head>
<body bgcolor="#ffffff" text="#ede0c0">
    <form id="form1" runat="server">
    <div>
        <table style="z-index: 99; left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
            width: 674px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; text-align: right">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Consistenza
                        U.C.&nbsp;</strong></span><br />
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
                    <br />
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 3; left: 279px; position: absolute; top: 315px;
                        text-align: left" Text="Label" Visible="False" Width="387px"></asp:Label>
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
                </td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
            Style="left: 24px; position: absolute; top: 29px; z-index: 100;" ToolTip="Indietro" />
        &nbsp;<a href="javascript:" onclick="history.go(document.getElementById('txtindietro').value); return false"></a> 
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="left: 91px; position: absolute; top: 29px; z-index: 101;" ToolTip="Salva" />
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 435px; position: absolute; top: 30px; z-index: 102;" ToolTip="Esci" />
        <asp:ImageButton ID="btnrilievo" runat="server" ImageUrl="~/NuoveImm/Img_SchedaRilievo.png"
            Style="left: 500px; position: absolute; top: 491px; z-index: 103;" Visible="False" ToolTip="Schede Rilievo" TabIndex="6" />
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 24px; position: absolute; top: 152px" ForeColor="Black">TIPOLOGIA</asp:Label>
        <asp:DropDownList ID="cmbTipologia" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 105; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 152px" TabIndex="1"
            Width="248px">
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="LblDescIndirizzo" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="9pt" Style="z-index: 106; left: 24px; position: absolute; top: 72px"
            Width="288px" ForeColor="Black">Indirizzo</asp:Label>
        &nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 107; left: 24px; position: absolute; top: 128px" ForeColor="Black">UBICAZIONE</asp:Label>
        <asp:DropDownList ID="cmbUbicazione" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 108; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 128px" Width="248px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 109; left: 24px; position: absolute; top: 176px" ForeColor="Black">STATO FISICO</asp:Label>
        <asp:DropDownList ID="cmbStatoFisico" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 110; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 176px" TabIndex="2"
            Width="248px">
        </asp:DropDownList>
        <asp:Label ID="LblCivico" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Style="z-index: 111; left: 320px; position: absolute; top: 72px" Height="16px" Width="32px">Civ</asp:Label>
        <asp:Label ID="LblCap" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Style="z-index: 112; left: 376px; position: absolute; top: 72px" Height="16px" Width="48px">Cap</asp:Label>
        <asp:Label ID="LblLocali" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Style="z-index: 113; left: 456px; position: absolute; top: 72px" Height="16px" Width="128px">Localita</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 114; left: 8px; position: absolute; top: 208px">CARATTERISTICHE E DOTAZIONI SINGOLA UNITA' COMUNE</asp:Label>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="3" Style="z-index: 115; left: 24px;
            position: absolute; top: 232px; text-align: left;" Width="288px" EnableTheming="True" Height="16px">
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
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ROWNUM" HeaderText="NUM" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_TIPOLOGIA" HeaderText="Dotazione" ReadOnly="True"
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_UNITA_COMUNE" HeaderText="Edificio" ReadOnly="True"
                    Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="###" Visible="False">
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="o" Font-Names="Wingdings" Height="16px" Width="16px"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DOTAZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPODOTAZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPODOTAZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="QUANTITA'">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANT") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANT") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <hr id="HR1" style="left: 384px; width: 264px; position: absolute; top: 216px; height: 1px; z-index: 116;" onclick="return HR1_onclick()" />
        &nbsp; &nbsp;
        <asp:Label ID="LBLID" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 515px; position: absolute; top: 266px" Visible="False"
            Width="23px">Label</asp:Label>
        <asp:Label ID="LBLDESCRIZIONE" runat="server" Font-Size="10pt" ForeColor="Black"
            Height="16px" Style="left: 603px; position: absolute; top: 266px"
            Visible="False" Width="16px">Label</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="left: 395px; position: absolute; top: 266px" Width="120px">Nessuna selezione</asp:Label>
        <asp:ImageButton ID="BtnAdd" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 320px; position: absolute; top: 240px; z-index: 120;" ToolTip="AGGIUNGI" TabIndex="-1" />
        &nbsp;&nbsp;
        <asp:ImageButton ID="BtnDelete" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 320px; position: absolute; top: 264px; z-index: 121;" ToolTip="ELIMINA" TabIndex="-1" />
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 122; left: 384px; position: absolute; top: 128px">DEST. USO</asp:Label>
        <asp:DropDownList ID="cmbDestUso" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            Style="border: 1px solid black; z-index: 123; left: 440px; position: absolute; top: 128px; width: 209px;" 
            TabIndex="3">
        </asp:DropDownList>
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 124; left: 320px; position: absolute; top: 56px"
            Width="8px">N°</asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 125; left: 376px; position: absolute; top: 56px"
            Width="8px">CAP</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 126; left: 456px; position: absolute; top: 56px"
            Width="8px">LOCALITA'</asp:Label>
        <asp:Label ID="lblAnno" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Height="16px" Style="z-index: 127; left: 320px; position: absolute;
            top: 104px" Width="48px" Visible="False">Anno</asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 128; left: 320px; position: absolute; top: 88px"
            Width="112px" Visible="False">ANNO COSTRUZIONE</asp:Label>
        <asp:Label ID="lblfoglio" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Height="16px" Style="z-index: 129; left: 456px; position: absolute;
            top: 104px" Width="40px" Visible="False">- - </asp:Label>
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 130; left: 456px; position: absolute; top: 88px"
            Width="8px" Visible="False">FOGLIO</asp:Label>
        <asp:Label ID="lblmap" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Height="16px" Style="z-index: 131; left: 536px; position: absolute;
            top: 104px" Width="32px" Visible="False">- -</asp:Label>
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 132; left: 536px; position: absolute; top: 88px"
            Width="8px" Visible="False">MAPPALE</asp:Label>
        <asp:TextBox ID="txtNote" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Height="48px" MaxLength="100" Style="z-index: 133; left: 440px;
            position: absolute; top: 152px" TextMode="MultiLine" Width="208px" TabIndex="4"></asp:TextBox>
        &nbsp; &nbsp;
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 134; left: 384px; position: absolute; top: 152px">NOTE</asp:Label>
        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 135; left: 8px; position: absolute; top: 336px">ANOMALIE CANTINE</asp:Label>
        <hr id="Hr2" style="left: 136px; width: 504px; position: absolute; top: 344px; height: 1px; z-index: 136;" onclick="return HR1_onclick()" />
        <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="3" Style="z-index: 137; left: 24px;
            position: absolute; top: 352px; text-align: left;" Width="288px" EnableTheming="True">
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
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_TIPOLOGIA" HeaderText="Dotazione" ReadOnly="True"
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_UNITA_COMUNE" HeaderText="Edificio" ReadOnly="True"
                    Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="###" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="o" Font-Names="Wingdings"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TPOLOGIA">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="VALORE %">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        &nbsp;&nbsp;
        <asp:Label ID="LblId2" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 515px; position: absolute; top: 242px" Visible="False"
            Width="27px" TabIndex="-1">Label</asp:Label>
        <asp:Label ID="Lbltipo" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 603px; position: absolute; top: 242px" Visible="False"
            Width="16px" TabIndex="-1">Label</asp:Label>
        <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="left: 395px; position: absolute; top: 242px" TabIndex="-1">Nessuna selezione</asp:Label>
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnDelAnomalie" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 320px; position: absolute; top: 392px; z-index: 141;" ToolTip="ELIMINA" TabIndex="-1" />
        &nbsp;
        <asp:ImageButton ID="BtnAddAnomalie" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 320px; position: absolute; top: 368px; z-index: 142;" ToolTip="AGGIUNGI" TabIndex="-1" />
        &nbsp;&nbsp;
        <asp:DropDownList ID="DrlSchede" runat="server" Enabled="False" Style="left: 16px;
            position: absolute; top: 488px; z-index: 143;" Width="472px" TabIndex="5">
        </asp:DropDownList>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 144; left: 8px; position: absolute; top: 456px">SCHEDE RILIEVO</asp:Label>
        <hr id="Hr3" onclick="return HR1_onclick()" style="left: 120px; width: 528px; position: absolute;
            top: 464px; height: 1px; z-index: 145;" />
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 24px; position: absolute;
            top: 312px; z-index: 146;" Width="232px" Font-Bold="True">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtmiaanom" runat="server" BackColor="White" BorderColor="White"
            BorderStyle="None" Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 24px;
            position: absolute; top: 432px; z-index: 147;" Width="232px" Font-Bold="True">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 512px; position: absolute; top: 161px;" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 445px; position: absolute; top: 159px;"
            Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtidanom" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 500px; position: absolute; top: 160px;"
            Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtdescanom" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 483px; position: absolute; top: 161px;"
            Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 465px; position: absolute; top: 158px;"
            Width="1px">0</asp:TextBox>
        &nbsp;
        <asp:Label ID="lblTipologia" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Style="z-index: 153; left: 24px; position: absolute; top: 88px"
            Width="288px" Height="40px">TIPOLOGIA</asp:Label>
        &nbsp;
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 154; left: 24px; position: absolute; top: 56px"
            Width="208px">RIEPILOGO UNITA' COMUNE</asp:Label>
    
    </div>
    </form>
</body>
</html>


