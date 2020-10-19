<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsistenzaEdifici.aspx.vb" Inherits="MANUTENZIONI_ConsistenzaEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CONSISTENZA</title>

</head>
<body bgcolor="#ffffff" text="#ede0c0">
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
            Style="left: 24px; position: absolute; top: 29px; z-index: 100;" ToolTip="Indietro" /><asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="left: 80px; position: absolute; top: 29px; z-index: 101;" ToolTip="Salva" />
        &nbsp;<a href="javascript:" onclick="history.go(document.getElementById('txtindietro').value);return false;"></a> 
        <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 473px; position: absolute; top: 29px; z-index: 102;" ToolTip="Esci" />
        <asp:ImageButton ID="btnrilievo" runat="server" ImageUrl="~/NuoveImm/Img_SchedaRilievo.png"
            Style="left: 509px; position: absolute; top: 515px; z-index: 103;" Visible="False" ToolTip="Schede Rilievo" TabIndex="8" />
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 328px; position: absolute; top: 112px" ForeColor="Black">TIPOLOGIA STRUTTURA</asp:Label>
        <asp:DropDownList ID="cmbTipolStrutt" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 105; left: 448px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 112px" TabIndex="1"
            Width="200px">
        </asp:DropDownList>
        <asp:Label ID="LblCodEdifi" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            Style="z-index: 106; left: 8px; position: absolute; top: 64px" ForeColor="Black" Width="72px">CODIFICA</asp:Label>
        <asp:Label ID="LblDescIndirizzo" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="10pt" Style="z-index: 107; left: 96px; position: absolute; top: 64px"
            Width="272px" ForeColor="Black">Indirizzo</asp:Label>
        &nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 8px; position: absolute; top: 112px" ForeColor="Black">DEST. PRINCIPALE</asp:Label>
        <asp:DropDownList ID="cmbDestPrincipale" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 109; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 112px" Width="192px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 110; left: 376px; position: absolute; top: 160px" ForeColor="Black">TIPO EDILIZIA 1</asp:Label>
        <asp:DropDownList ID="cmbTipolEdil1" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 480px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 160px" TabIndex="2"
            Width="168px">
        </asp:DropDownList>
        <asp:Label ID="LblCivico" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Style="z-index: 112; left: 400px; position: absolute; top: 64px" Height="16px" Width="32px">Civ</asp:Label>
        <asp:Label ID="LblCap" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Style="z-index: 113; left: 480px; position: absolute; top: 64px" Height="16px" Width="48px">Cap</asp:Label>
        <asp:Label ID="LblLocali" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Style="z-index: 114; left: 584px; position: absolute; top: 64px" Height="16px" Width="64px">Localita</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 115; left: 8px; position: absolute; top: 136px">CONSISTENZA</asp:Label>
        <asp:DataGrid ID="DatGridConsistenza" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="3" Style="z-index: 116; left: 16px;
            position: absolute; top: 152px" Width="296px" EnableTheming="True">
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
                <asp:BoundColumn DataField="ID_EDICOMP" HeaderText="Edificio" ReadOnly="True"
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
                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOCONS") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOCONS") %>'></asp:TextBox>
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
        <hr id="HR1" style="left: 104px; width: 232px; position: absolute; top: 144px; height: 1px; z-index: 117;" onclick="return HR1_onclick()" />
        &nbsp;
        <asp:Label ID="LBLID" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 172px; position: absolute; top: 429px" Visible="False"
            Width="1px">Label</asp:Label>
        <asp:Label ID="LBLDESCRIZIONE" runat="server" Font-Size="10pt" ForeColor="Black"
            Height="16px" Style="left: 252px; position: absolute; top: 429px"
            Visible="False" Width="2px">Label</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="left: 52px; position: absolute; top: 429px"></asp:Label>
        <asp:ImageButton ID="BtnAdd" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 312px; position: absolute; top: 168px; z-index: 121;" ToolTip="AGGIUNGI" TabIndex="-1" />
        &nbsp;&nbsp;
        <asp:ImageButton ID="BtnDeleteConsistenza" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 312px; position: absolute; top: 192px; z-index: 122;" ToolTip="ELIMINA" TabIndex="-1" />
        &nbsp;&nbsp;
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 123; left: 384px; position: absolute; top: 64px"
            Width="8px">N°</asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 124; left: 456px; position: absolute; top: 64px"
            Width="8px">CAP</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 125; left: 544px; position: absolute; top: 64px"
            Width="8px">Località</asp:Label>
        <asp:Label ID="lblAnno" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Height="16px" Style="z-index: 126; left: 120px; position: absolute;
            top: 88px" Width="40px">Anno</asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 127; left: 8px; position: absolute; top: 88px"
            Width="112px">ANNO COSTRUZIONE</asp:Label>
        <asp:Label ID="lblfoglio" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Height="16px" Style="z-index: 128; left: 296px; position: absolute;
            top: 88px" Width="32px" Visible="False">- - </asp:Label>
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 129; left: 256px; position: absolute; top: 88px"
            Width="8px" Visible="False">FOGLIO</asp:Label>
        <asp:Label ID="lblmap" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" Height="16px" Style="z-index: 130; left: 440px; position: absolute;
            top: 88px" Width="32px" Visible="False">- -</asp:Label>
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 131; left: 384px; position: absolute; top: 88px"
            Width="8px" Visible="False">MAPPALE</asp:Label>
        <asp:TextBox ID="txtNote" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Height="88px" MaxLength="100" Style="z-index: 132; left: 8px;
            position: absolute; top: 392px" TextMode="MultiLine" Width="304px" TabIndex="6"></asp:TextBox>
        &nbsp; &nbsp;&nbsp; &nbsp;
        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 133; left: 8px; position: absolute; top: 256px">SERVIZI</asp:Label>
        <hr id="Hr2" style="left: 64px; width: 264px; position: absolute; top: 256px; height: 1px; z-index: 134;" onclick="return HR1_onclick()" />
        <asp:DataGrid ID="DataGridServComuni" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="3" Style="z-index: 135; left: 16px;
            position: absolute; top: 272px" Width="296px" EnableTheming="True">
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
                <asp:BoundColumn DataField="ID_TIPOLOGIA" HeaderText="ID_TIPOLOGIA" Visible="False">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID_EDICOMP" HeaderText="ID_EDICOMP" Visible="False"></asp:BoundColumn>
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
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOSERV") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOSERV") %>'></asp:TextBox>
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
        &nbsp;&nbsp;
        <asp:Label ID="LblIdserv" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 172px; position: absolute; top: 453px" Visible="False"
            Width="1px">Label</asp:Label>
        <asp:Label ID="LblDescServ" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 260px; position: absolute; top: 453px" Visible="False"
            Width="4px">Label</asp:Label>
        <asp:Label ID="LblselServ" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="left: 52px; position: absolute; top: 453px"></asp:Label>
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 312px; position: absolute; top: 280px; z-index: 139;" ToolTip="AGGIUNGI" TabIndex="-1" />
        <asp:ImageButton ID="BtnDeleteServizi" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 312px; position: absolute; top: 304px; z-index: 140;" ToolTip="ELIMINA" TabIndex="-1" />
        &nbsp; v v &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <asp:DataGrid ID="DatGridImpComuni" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="3" Style="z-index: 141; left: 344px;
            position: absolute; top: 272px" Width="296px" EnableTheming="True">
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
                <asp:BoundColumn DataField="ID_TIPOLOGIA" HeaderText="Dotazione" ReadOnly="True"
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ROWNUM" HeaderText="Rownum" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_EDICOMP" HeaderText="IDEDICOMP" Visible="False">
                </asp:BoundColumn>
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
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOIMP") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOIMP") %>'></asp:TextBox>
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
        <asp:Label ID="LblIDImpComuni" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 171px; position: absolute; top: 403px" Visible="False"
            Width="25px">Label</asp:Label>
        <asp:Label ID="LblDescImCom" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="left: 259px; position: absolute; top: 403px" Visible="False"
            Width="7px">Label</asp:Label>
        <asp:Label ID="LblselImpCom" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="left: 51px; position: absolute; top: 403px" Width="118px"></asp:Label>
        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 640px; position: absolute; top: 288px; z-index: 145;" ToolTip="AGGIUNGI" TabIndex="-1" />
        <asp:ImageButton ID="btnDeleteImpComuni" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 640px; position: absolute; top: 312px; z-index: 146;" ToolTip="ELIMINA" TabIndex="-1" />
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 147; left: 376px; position: absolute; top: 184px"
            Width="80px">TIPO EDILIZIA 2</asp:Label>
        <asp:DropDownList ID="cmbTipoEdilizia2" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 148; left: 480px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 184px" TabIndex="3"
            Width="168px">
        </asp:DropDownList>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 149; left: 376px; position: absolute; top: 208px"
            Width="80px">TIPO EDILIZIA 3</asp:Label>
        <asp:DropDownList ID="cmbTipoEdilizia3" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 150; left: 480px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 208px" TabIndex="4"
            Width="168px">
        </asp:DropDownList>
        <asp:DropDownList ID="DrlSchede" runat="server" Style="left: 24px; position: absolute;
            top: 512px; z-index: 151;" Width="472px" TabIndex="7">
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 152; left: 8px; position: absolute; top: 488px">SCHEDE RILIEVO</asp:Label>
        <hr id="Hr3" style="left: 120px; width: 528px; position: absolute; top: 496px; height: 1px; z-index: 153;" onclick="return HR1_onclick()" />
        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 154; left: 8px; position: absolute; top: 368px">NOTE</asp:Label>
        <hr id="Hr4" style="left: 48px; width: 272px; position: absolute; top: 376px; height: 1px; z-index: 155;" onclick="return HR1_onclick()" />
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 156; left: 336px; position: absolute; top: 256px">IMPIANTI COMUNI</asp:Label>
        <hr id="Hr5" style="left: 456px; width: 192px; position: absolute; top: 256px; height: 1px; z-index: 157;" onclick="return HR1_onclick()" />
        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 158; left: 336px; position: absolute; top: 136px">TIPOLOGIA EDILE</asp:Label>
        <hr id="Hr6" style="left: 456px; width: 192px; position: absolute; top: 144px; height: 1px; z-index: 159;" onclick="return HR1_onclick()" />
        <asp:TextBox ID="txtmia" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 16px; position: absolute;
            top: 232px; z-index: 160;" Width="232px" Font-Bold="True">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 21px; position: absolute; top: 437px; background-color: transparent;" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 21px; position: absolute; top: 413px; background-color: transparent;"
            Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtmiaserv" runat="server" BackColor="White" BorderColor="White"
            BorderStyle="None" Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 16px;
            position: absolute; top: 352px; z-index: 163;" Width="232px" Font-Bold="True">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtidserv" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 115px; position: absolute; top: 451px; background-color: transparent;"
            Width="5px"></asp:TextBox>
        <asp:TextBox ID="txtdescserv" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 115px; position: absolute; top: 427px; background-color: transparent;"
            Width="5px"></asp:TextBox>
        <asp:TextBox ID="txtidimp" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 74px; position: absolute; top: 427px; background-color: transparent;"
            Width="14px"></asp:TextBox>
        <asp:TextBox ID="txtdescimp" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 74px; position: absolute; top: 403px; background-color: transparent;"
            Width="12px"></asp:TextBox>
        <asp:TextBox ID="txtidlocale" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 115px; position: absolute; top: 403px; background-color: transparent;"
            Width="5px"></asp:TextBox>
        <asp:TextBox ID="txtmiaimp" runat="server" BackColor="White" BorderColor="White"
            BorderStyle="None" Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 344px;
            position: absolute; top: 352px; z-index: 169;" Width="232px" Font-Bold="True">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 205px; position: absolute; top: 414px; background-color: transparent;"
            Width="12px">0</asp:TextBox>
        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 171; left: 344px; position: absolute; top: 386px">STATO FISICO BENE</asp:Label>
        <asp:DropDownList ID="cmbStatoFisico" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 172; left: 448px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 386px" TabIndex="5" Width="184px">
        </asp:DropDownList>
        <asp:DataGrid ID="DataGridLocaliIrrilevati" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="3" Style="z-index: 173; left: 344px;
            position: absolute; top: 408px" Width="296px" EnableTheming="True">
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
                <asp:BoundColumn DataField="ID_LOCALE" HeaderText="ID_LOCALE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID" HeaderText="id" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ROWNUM" HeaderText="Rownum" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="LOCALE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <asp:ImageButton ID="addriliev" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 640px; position: absolute; top: 424px; z-index: 174;" ToolTip="AGGIUNGI" TabIndex="-1" />
        <asp:ImageButton ID="delriliev" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 640px; position: absolute; top: 448px; z-index: 175;" ToolTip="ELIMINA" TabIndex="-1" />
        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 176; left: 336px; position: absolute; top: 368px">MANCATO RILIEVO</asp:Label>
        <hr id="Hr7" style="left: 464px; width: 184px; position: absolute; top: 376px; height: 1px; z-index: 177;" onclick="return HR1_onclick()" />
        <table style="z-index: 99; left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
            width: 674px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; text-align: right">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Consistenza&nbsp;</strong></span><br />
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
                        ForeColor="Red" Style="z-index: 3; left: 262px; position: absolute; top: 239px;
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
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>


