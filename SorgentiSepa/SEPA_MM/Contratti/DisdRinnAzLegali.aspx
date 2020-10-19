<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DisdRinnAzLegali.aspx.vb"
    Inherits="Contratti_DisdRinnAzLegali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Disdetta-Rinnovo-Az.Legali</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
        <div>
            &nbsp;&nbsp;
        <table style="left: 0px; background-image: url('../NuoveImm/XBackGround.gif'); position: absolute; top: 0px;">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Disdetta
                        - Rinnovo - Az. Legali
                        <asp:ImageButton ID="ImgButSave" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                            Style="z-index: 102; cursor: pointer; text-align: right; left: 1067px; position: absolute; top: 570px; height: 20px; right: 168px;"
                            ToolTip="Salva" />
                        <asp:ImageButton ID="ImgButEsci" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                            Style="z-index: 102; cursor: pointer; text-align: right; left: 1174px; position: absolute; top: 570px;"
                            ToolTip="Esci" />
                    </strong></span>
                    <br />
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
                                    Width="70px">Data Emiss.</asp:Label>
                                <br />
                                <asp:TextBox ID="TxtDataReqGenerali" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtDataReqGenerali"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="120px">Data Decorr./Esec.</asp:Label>
                                <br />
                                <asp:TextBox ID="TxtDataDecorrReqGen" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox>
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
                                <asp:TextBox ID="TxtNumIdentReqGenerali" runat="server" Width="160px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; text-align: center; vertical-align: top;">
                                <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px">Note</asp:Label>
                                <br />
                                <asp:TextBox ID="TxtNoteReqGenerali" runat="server" Width="260px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">EMESSO DECRETO DECADENZA</asp:Label>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
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
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:TextBox ID="TxtDataMorosita" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtDataMorosita"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrMorosita" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TxtDataDecorrMorosita"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentMorosita" runat="server" Width="160px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteMorosita" runat="server" Width="260px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">REGOLARITA&#39; STATO ABITATIVO</asp:Label>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                                <asp:DropDownList ID="CmbStatoAbitativo" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">NON REGOLARE</asp:ListItem>
                                    <asp:ListItem Value="1">REGOLARE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="33px"></asp:Label>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:TextBox ID="TxtDataStatoAbitativo" runat="server" Width="70px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="10"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtDataStatoAbitativo"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrStatoAbit" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="TxtDataDecorrStatoAbit"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentStatoAbitativo" runat="server" Width="160px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:TextBox ID="TxtNoteStatoAbitativo" runat="server" Width="260px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200" Style="margin-left: 0px" TextMode="MultiLine" Font-Size="8pt"
                                    Height="38px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 52px; top: 121px">CONTROLLO ANAGRAFICO</asp:Label>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                                <asp:DropDownList ID="CmbContrAnagr" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="103px">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">NON ESEGUITO</asp:ListItem>
                                    <asp:ListItem Value="1">ESEGUITO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">&nbsp;<br />
                                <asp:DropDownList ID="CmbContAnagEsito" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid; top: 198px"
                                    Width="150px" Enabled="False"
                                    AutoPostBack="True">
                                    <asp:ListItem Value="NULL">- - -</asp:ListItem>
                                    <asp:ListItem Value="0">VUOTO</asp:ListItem>
                                    <asp:ListItem Value="1">TITOLARE MONONUCLEO DECEDUTO</asp:ListItem>
                                    <%-- <asp:ListItem Value="0">NEGATIVO</asp:ListItem>
                                    <asp:ListItem Value="1">POSITIVO</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top;">
                                <br />
                                <asp:TextBox ID="TxtDataContAnag" runat="server" Width="70px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="10"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TxtDataContAnag"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrContAnag" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="TxtDataDecorrContAnag"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentContAnag" runat="server" Width="160px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>

                            <td style="border-bottom: gray thin solid; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteContAnag" runat="server" Width="260px" ToolTip="dd/Mm/YYYY"
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
                                    Width="70px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="TxtDataDecorrMesseInMora"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentInMora" runat="server" Width="160px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteMesseInMora" runat="server" Width="260px" ToolTip="dd/Mm/YYYY"
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
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">&nbsp;<br />
                                <asp:DropDownList ID="CmbContGiudiziari" runat="server" AutoPostBack="True" BackColor="White"
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
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">
                                <br />
                                <asp:TextBox ID="TxtDataContGiudiziari" runat="server" Width="70px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="10"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TxtDataContGiudiziari"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtDataDecorrContGiudiz" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="TxtDataDecorrContGiudiz"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentContGiudiz" runat="server" Width="160px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtNoteContGiudiz" runat="server" Width="260px" ToolTip="dd/Mm/YYYY"
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
                            <td style="border-bottom: gray thin solid; vertical-align: top; height: 63px;">
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
                                    Width="70px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtRinnovoData"
                                    ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px; top: 158px"
                                    ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-top-style: none; border-bottom: gray thin solid; border-right-style: none; border-left-style: none; vertical-align: top; text-align: center;">
                                <br />
                                <asp:TextBox ID="TxtRinnovoDataDecorr" runat="server" ToolTip="dd/Mm/YYYY" MaxLength="10"
                                    Width="70px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="TxtRinnovoDataDecorr"
                                    ErrorMessage="!" Font-Bold="True" Style="left: 173px; top: 158px; z-index: 2;"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Inserire una data valida" Height="19px" Width="9px"></asp:RegularExpressionValidator>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">
                                <br />
                                <asp:TextBox ID="TxtNumIdentRinnovo" runat="server" Width="160px" ToolTip="dd/Mm/YYYY"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="border-bottom-width: thin; border-bottom: gray thin solid; vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none; height: 42px">&nbsp
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
            &nbsp;
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 8px; position: absolute; top: 581px; right: 1119px;"
            Text="Label" Visible="False" Width="386px"></asp:Label>
        </div>
    </form>
    <script type="text/jscript">
        window.focus();
        self.focus();
    </script>
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

    </script>
</body>
</html>
