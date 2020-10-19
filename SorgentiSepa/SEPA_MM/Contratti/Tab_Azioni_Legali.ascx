<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Azioni_Legali.ascx.vb"
    Inherits="Contratti_Tab_Azioni_Legali" %>
<table style="width: 100%; height: 459px;" cellpadding="2" cellspacing="0">
                        <tr>
                            <td style="border-bottom-width: thin; border-bottom-color: gray; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px"></td>
                            <td style="border-bottom-width: thin; border-bottom-color: gray; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px"></td>
                            <td style="border-bottom-width: thin; border-bottom-color: gray; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px"></td>
                            <td style="border-bottom-width: thin; border-bottom-color: gray; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px"></td>
                            <td style="border-bottom-width: thin; border-bottom: gray; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px"></td>
                            <td style="border-bottom-width: thin; border-bottom-color: gray; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px"></td>
                        </tr>
                        <tr>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">PROPOSTA DECADENZA</asp:Label>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px">Stato</asp:Label>
                                <br />
                                <asp:DropDownList ID="cmbReqGenerali" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Value="2">PREAVVISO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px">Esito</asp:Label>
                                <br />
                                <asp:DropDownList ID="cmbReqGenEsito" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="150px" Enabled="False">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">IN CORSO</asp:ListItem>
                                    <asp:ListItem Value="1">ARCHIVIATO</asp:ListItem>
                                    <asp:ListItem Value="2">EMESSO DECRETO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="80px">Data</asp:Label>
                                <br />
                                <asp:TextBox ID="TxtDataReqGenerali" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtDataReqGenerali"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="120px">Data Esecuzione</asp:Label>
                                <br />
                                <asp:TextBox ID="TxtDataDecorrReqGen" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TxtDataDecorrReqGen"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; text-align: center; border-right-style: none; border-left-style: none; height: 42px">
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px">N.ident.</asp:Label>
                                <br />
                                <asp:TextBox ID="TxtNumIdentReqGenerali" runat="server" Width="120px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; text-align: center; vertical-align: top;">
                                <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px">Note</asp:Label>
                                <br />

                                <asp:DropDownList ID="txtNoteReqGenerali" runat="server" AutoPostBack="False" BackColor="White"
                                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="140px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="1">Abbandono/cessione</asp:ListItem>
                                    <asp:ListItem Value="2">Non rispondenza</asp:ListItem>
                                    <asp:ListItem Value="3">Perdita requisiti</asp:ListItem>
                                    <asp:ListItem Value="4">Altro</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">EMESSO DECRETO DECADENZA</asp:Label>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                                <asp:DropDownList ID="CmbMorosita" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">NON ESEGUITO</asp:ListItem>
                                    <asp:ListItem Value="1">ESEGUITO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px"></asp:Label>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:TextBox ID="TxtDataMorosita" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtDataMorosita"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrMorosita" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TxtDataDecorrMorosita"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center;border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentMorosita" runat="server" Width="120px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteMorosita" runat="server" Width="140px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">PROVVEDIMENTO</asp:Label>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                                <asp:DropDownList ID="CmbStatoAbitativo" runat="server" AutoPostBack="False" BackColor="White"
                                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">DIFFIDA</asp:ListItem>
                                    <asp:ListItem Value="1">DECRETO DI RILASCIO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px"></asp:Label>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:TextBox ID="TxtDataStatoAbitativo" runat="server" Width="80px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="10"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtDataStatoAbitativo"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrStatoAbit" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="TxtDataDecorrStatoAbit"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center;border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentStatoAbitativo" runat="server" Width="120px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteStatoAbitativo" runat="server" Width="140px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">PROVVEDIMENTO</asp:Label>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                                <asp:DropDownList ID="CmbContrAnagr" runat="server" AutoPostBack="False" BackColor="White"
                                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">QUERELA</asp:ListItem>
                                    <asp:ListItem Value="1">PROCEDIMENTO PENALE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                                <asp:DropDownList ID="CmbContAnagEsito" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="150px" Enabled="False"
                                    AutoPostBack="False">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">VUOTO</asp:ListItem>
                                    <asp:ListItem Value="1">TITOLARE MONONUCLEO DECEDUTO</asp:ListItem>
                                    <%-- <asp:ListItem Value="0">NEGATIVO</asp:ListItem>
                                    <asp:ListItem Value="1">POSITIVO</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:TextBox ID="TxtDataContAnag" runat="server" Width="80px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="10"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TxtDataContAnag"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrContAnag" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="TxtDataDecorrContAnag"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center;border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentContAnag" runat="server" Width="120px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>

                            <td style="border-bottom: gray thin solid; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteContAnag" runat="server" Width="140px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">
                                <br />
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px" Width="362px">PRESENZA DI CONTENZIOSI GIUDIZIARI PER INADEMPIMENTI CONTRATTUALI/PROCEDURE ESECUTIVE</asp:Label>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top; height: 63px;">&nbsp;<br />
                                <asp:DropDownList ID="CmbContGiudiziari" runat="server" AutoPostBack="False" BackColor="White"
                                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">
                                <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px"></asp:Label>
                            </td>
                            <td style="text-align: center;text-align: center;border-bottom: gray thin solid; vertical-align: top; height: 63px;">
                                <br />
                                <asp:TextBox ID="TxtDataContGiudiziari" runat="server" Width="80px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="10"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TxtDataContGiudiziari"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrContGiudiz" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="TxtDataDecorrContGiudiz"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center;border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentContGiudiz" runat="server" Width="120px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteContGiudiz" runat="server" Width="140px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px; width: 30px">ACCESSO UFFICIALE GIUDIZIARIO</asp:Label>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                               &nbsp
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                &nbsp
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                               &nbsp
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrMesseInMora" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="TxtDataDecorrMesseInMora"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center;border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentInMora" runat="server" Width="120px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteMesseInMora" runat="server" Width="140px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">
                                <br />
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">RINNOVO AMMISSIBILE</asp:Label>
                            </td>
                            <td style="text-align: center;border-bottom: gray thin solid; vertical-align: top; height: 63px;">
                                <br />
                                <asp:DropDownList ID="CmbRinnovoAmmissibile" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">&nbsp;
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">
                                <br />
                                <asp:TextBox ID="TxtRinnovoData" runat="server" MaxLength="10" ToolTip="dd/Mm/YYYY"
                                    Width="80px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtRinnovoData"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center;border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtRinnovoDataDecorr" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="80px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="TxtRinnovoDataDecorr"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center;border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentRinnovo" runat="server" Width="120px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">&nbsp
                            </td>
                        </tr>
                    </table>
<asp:HiddenField ID="modify" runat="server" Value="0" />
