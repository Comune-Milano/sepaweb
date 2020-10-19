<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_FornitoreG_rappresentante.ascx.vb" Inherits="Tab_FornitoreG_rappresentante" %>
            <table style="width: 600px; height: 378px;">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; right: 571px; left: 5px; top: 54px;"
                            Width="100px" Height="14px">Cognome</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCognome" runat="server" MaxLength="50" Width="200px" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="21"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="70px">Nome</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" MaxLength="50" Width="200px" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="22"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Height="14px" Style="z-index: 106; right: 571px; left: 5px;
                            top: 54px" Width="100px">Codice Fiscale</asp:Label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtCFR" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="16" Style="z-index: 109; left: 326px; top: 389px" TabIndex="23" 
                            Width="150px"></asp:TextBox>
                        <asp:Label ID="lblErroreCF" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                            ForeColor="Red" Style="z-index: 106; left: 269px; top: 73px"
                            Visible="False" Width="4px">!</asp:Label></td>
                    <td>
                     <a href="../../../cf/codice.htm" target="_blank">
                    <img border="0" 
                        alt="Calcolo Codice Fiscale" src="../../../NuoveImm/codice_fiscale.gif" 
                        style="cursor:pointer; top: 383px; left: 465px; "/></a></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="70px">Tipo</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="DrLTipoR" runat="server" AutoPostBack="True" BackColor="White"
                            Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid;
                            border-top: black 1px solid; z-index: 111; left: 510px; border-left: black 1px solid; border-bottom: black 1px solid; top: 390px;" TabIndex="24" Width="200px">
                            <asp:ListItem Value="L">LEGALE RAPP.</asp:ListItem>
                            <asp:ListItem Value="P">PROCURATORE LEG.</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table id="TABLE1">
                            <tr>
                                <td style="width: 2px">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="100px">Tipo indirizzo</asp:Label></td>
                                <td>
                    <asp:DropDownList ID="DrLTipoIndR" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" 
                        Style="border: 1px solid black; z-index: 111; left: 30px; top: 19px; " 
                        TabIndex="25" Width="90px" >
                </asp:DropDownList></td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="50px">Indirizzo</asp:Label></td>
                                <td>
                    <asp:TextBox ID="txtIndirizzoResidenzaR" runat="server" BorderStyle="None" MaxLength="50" 
                        Style="z-index: 107; left: 109px; top: 67px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
                        TabIndex="26" Width="210px"></asp:TextBox></td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 288px; top: 53px" Width="50px">Civico</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCivicoResidenzaR" runat="server" MaxLength="6" Width="60px" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="27"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 2px">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="100px">CAP</asp:Label></td>
                                <td>
                    <asp:TextBox ID="txtCAPR" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="5" 
                        Style="z-index: 107; left: 347px; top: 68px; bottom: 225px; right: 245px;" 
                        TabIndex="28" Width="50px"></asp:TextBox></td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; right: 183px; left: 417px; top: 55px;
                                        height: 12px" Width="50px">Comune</asp:Label></td>
                                <td>
                    <asp:TextBox ID="txtComuneResidenzaR" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" MaxLength="25" 
                        Style="z-index: 107; left: 416px; top: 69px;" 
                        TabIndex="29" Width="210px"></asp:TextBox></td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 37px; top: -286px" Width="50px">Pr.</asp:Label></td>
                                <td>
	
                    <asp:TextBox ID="txtProvinciaResidenzaR" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" MaxLength="2" 
                        Style="z-index: 107; left: 594px; top: 69px;" 
                        TabIndex="30" Width="33px"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" ForeColor="Black" 
                        Style="z-index: 106; left: -254px; top: -281px" Width="100px">Telefono</asp:Label></td>
                    <td>
                    <asp:TextBox ID="txtTelR" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                        MaxLength="50" 
                        Style="z-index: 107; left: 11px; top: 146px;" 
                        TabIndex="31" Width="200px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblprocura" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 152px; top: 459px; color: black;" Visible="False" Width="100px">Numero Procura</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtnumprocura" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 151px; top: 473px" TabIndex="32"
                            Visible="False" Width="200px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbldataprocura" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 106; left: 276px; top: 457px; color: black;" Visible="False"
                            Width="70px">Data Procura</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtdataprocura" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 107; right: 455px; left: 275px; bottom: 79px;
                            top: 473px" TabIndex="33" ToolTip="GG/MM/YYYY" Visible="False" Width="100px"></asp:TextBox><asp:RegularExpressionValidator ID="controllodata" runat="server" ControlToValidate="txtdataprocura"
                            Display="Dynamic" Enabled="False" ErrorMessage="data non valida!" Font-Bold="True"
                            Font-Names="arial" Font-Size="8pt" Height="19px" Style="z-index: 150; left: 519px; top: 195px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" Width="100px"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td colspan="5">
                        </td>
                </tr>
                <tr>
                    <td>
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
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
