<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SchedaQuestionario.aspx.vb" Inherits="SATISFACTION_Pippo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Questionario di rilevazione</title>
        
    <style type="text/css">
        
            .stile_tabella
            {
                width: 750px;
            }
            
            .font_caption
            {
                font-size:13pt;
                font-weight: bold;
                color:#721C1F;
            }
            
            .colonna_domanda
            {
                width: 650px;
            }
            
            .colonna_cbx1
            {
                width: 50px;
            }
            
            .colonna_cbx2
            {
                width: 30px;
            }
            
        .style2
        {
            width: 384px;
        }
            
     </style>

     <script type="text/javascript">

        function disabilitaMinore(e) {
        var key;
        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 226)
            return false;
        else
            return true;
       }

     </script>

</head>

<body background="../NuoveImm/SfondoMascheraContratti.jpg">
    <form runat="server">
    <div>
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="0px" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" Width="750px" 
            Style="position:absolute; top: 174px; left: 15px; bottom: 34px;" Height="200px" 
            DisplaySideBar="False" FinishCompleteButtonText="Fine" 
            FinishPreviousButtonText="Precedente" StartNextButtonText="Avanti" 
            StepNextButtonText="Avanti" StepPreviousButtonText="Precedente">
            <CancelButtonStyle ForeColor="White" />
            <HeaderStyle BackColor="#666666" BorderColor="#E6E2D8" BorderStyle="Solid" 
                BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" 
                HorizontalAlign="Center" />
            <NavigationButtonStyle BackColor="White" BorderColor="#C5BBAF" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="10pt" 
                ForeColor="Black"/>
            <SideBarButtonStyle ForeColor="White" />
            <SideBarStyle BackColor="#339966" Font-Size="1em" VerticalAlign="Top" Height="5px"
                Font-Bold="False" Font-Names="Arial" Width="0px" />
            <SideBarTemplate>
                <asp:DataList ID="SideBarList" runat="server" Width="100%">
                    <ItemTemplate>
                        <asp:LinkButton ID="SideBarButton" runat="server" ForeColor="White"></asp:LinkButton>
                    </ItemTemplate>
                    <SelectedItemStyle Font-Bold="True"/>
                </asp:DataList>
            </SideBarTemplate>
            <StepStyle BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderStyle="Solid" 
                BorderWidth="2px" Font-Bold="False" Font-Names="Arial" />
            <WizardSteps>
                <asp:WizardStep runat="server" title="Servizi di pulizia" ID="pulizia" 
                     >
                <table class="stile_tabella">
                    <caption class="font_caption">SERVIZI DI PULIZIA &nbsp;
                    <asp:CheckBox ID="ChPulizia" 
                            runat="server" Text="applicabile" Checked="True" Font-Bold="True" TabIndex="1"
                            Font-Size="9pt" ForeColor="Black" AutoPostBack="True" />
                    </caption>
                    
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Font-Size="X-Small" 
                                Text="Valore di importanza" Width="60px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" 
                            Text="Ritiene che il servizio sia svolto con REGOLARITA'?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_pulizia1" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="2">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VApulizia1" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="3">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il servizio sia svolto con QUALITA'?"></asp:Label>
                        </td>
                        
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_pulizia2" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="4">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VApulizia2" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="5">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il personale sia CORTESE?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_pulizia3" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="6">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VApulizia3" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="7">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                Text="Ritiene che la condizione dei punti di raccolta rifiuti sia IGIENICAMENTE soddisfaciente?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_pulizia4" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="8">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2"> 
                            <asp:DropDownList ID="cbx_VApulizia4" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="9">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che la PULIZIA dei PIAZZALI E DELLE PARTI COMUNI sia adeguata?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_pulizia5" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="10">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VApulizia5" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="11">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene tempestiva la rimozione di masserizie e RIFIUTI INGOMBRANTI?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_pulizia6" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="12">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VApulizia6" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="13">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                        <tr>
                            <td>
                            <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="10pt" 
                            Text="Ha qualche suggerimento per migliorare il servizio?"></asp:Label>
                            </td>   
                        </tr>

                        <tr>
                        <td colspan="3">
                        <asp:TextBox ID="txt_pulizia" runat="server" Width="750px" Height="80px" 
                                MaxLength="1000" TextMode="MultiLine" TabIndex="14" onkeydown="return disabilitaMinore(event)"></asp:TextBox>
                            </td>
                        </tr>

                </table>
                    
                </asp:WizardStep>
                <asp:WizardStep runat="server" title="Servizi di portierato" ID="portierato" 
                     >
                <table class="stile_tabella">
                    <caption class="font_caption">SERVIZI DI PORTIERATO &nbsp;
                    <asp:CheckBox ID="ChPortierato" 
                            runat="server" Text="applicabile" Checked="True" Font-Bold="True" 
                            Font-Size="9pt" ForeColor="Black" AutoPostBack="True" TabIndex="15"/></caption>
                    <tr><td></td><td></td><td>
                        <asp:Label ID="Label3" runat="server" Text="Valore di importanza" Width="60px" 
                            Font-Size="X-Small"></asp:Label>
                        </td>
                        </tr>
                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="10pt" 
                            Text="Ritiene che il servizio sia svolto con REGOLARITA'?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_portierato1" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="16">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAportierato1" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="17">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il servizio sia svolto con QUALITA'?"></asp:Label>
                        </td>
                        
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_portierato2" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="18">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAportierato2" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="19">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il personale sia CORTESE?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_portierato3" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="20">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAportierato3" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="21">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        
                                Text="Ritiene che il personale offra INFORMAZIONI COMPLETE?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_portierato4" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="22">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAportierato4" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="23">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che la GESTIONE DELLA POSTA sia soddisfacente?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_portierato5" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="24">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAportierato5" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="25">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr><td>
                                &nbsp;</td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ha qualche suggerimento per migliorare il servizio?"></asp:Label>
                        </td>   
                    </tr>

                    <tr>
                        <td colspan="3">
                        <asp:TextBox ID="txt_portierato" runat="server" Width="750px" Height="80px" 
                                MaxLength="1000" TextMode="MultiLine" TabIndex="26" onkeydown="return disabilitaMinore(event)"></asp:TextBox>
                        </td>
                    </tr>

                </table>
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Servizi di riscaldamento" ID="riscaldamento"> 
                     
                <table class="stile_tabella">
                    <caption class="font_caption">SERVIZI DI RISCALDAMENTO &nbsp;
                    <asp:CheckBox ID="ChRiscaldamento" 
                            runat="server" Text="applicabile" Checked="True" Font-Bold="True" 
                            Font-Size="9pt" ForeColor="Black" AutoPostBack="True" TabIndex="27"/></caption>
                    <tr><td></td><td></td><td>
                        <asp:Label ID="Label17" runat="server" Text="Valore di importanza" Width="60px" 
                            Font-Size="X-Small"></asp:Label>
                        </td>
                        </tr>
                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="10pt" 
                            Text="Ritiene che il servizio sia svolto con REGOLARITA'?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_riscaldamento1" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="28">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAriscaldamento1" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="29">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il servizio sia svolto con QUALITA'?"></asp:Label>
                        </td>
                        
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_riscaldamento2" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="30">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAriscaldamento2" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="31">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il personale sia CORTESE?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_riscaldamento3" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="32">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAriscaldamento3" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="33">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        
                                Text="Ritiene che la TEMPERATURA sia adeguata nei mesi invernali?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_riscaldamento4" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="34">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAriscaldamento4" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="35">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che sia facile contattare il pronto intervento in caso di GUASTI?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_riscaldamento5" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="36">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAriscaldamento5" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="37">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che i GUASTI siano risolti con tempestività?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_riscaldamento6" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="38">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAriscaldamento6" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="39">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label24" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ha qualche suggerimento per migliorare il servizio?"></asp:Label>
                        </td>   
                    </tr>

                    <tr>
                        <td colspan="3">
                        <asp:TextBox ID="txt_riscaldam" runat="server" Width="750px" Height="80px" TabIndex="40"
                                MaxLength="1000" TextMode="MultiLine" onkeydown="return disabilitaMinore(event)"></asp:TextBox>
                        </td>
                    </tr>

                </table>
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Servizi di manutenzione del verde" ID="verde" 
                     >
                <table class="stile_tabella">
                    <caption class="font_caption">SERVIZI DI MANUTENZIONE DEL VERDE &nbsp;
                    <asp:CheckBox ID="ChVerde" 
                            runat="server" Text="applicabile" Checked="True" Font-Bold="True" TabIndex="41"
                            Font-Size="9pt" ForeColor="Black" AutoPostBack="True"/></caption>
                    <tr><td></td><td></td><td>
                        <asp:Label ID="Label25" runat="server" Text="Valore di importanza" Width="60px"
                            Font-Size="X-Small"></asp:Label>
                        </td>
                        </tr>
                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="10pt" 
                            Text="Ritiene che il servizio sia svolto con REGOLARITA'?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_manutenzione1" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="42">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAmanutenzione1" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="43">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il servizio sia svolto con QUALITA'?"></asp:Label>
                        </td>
                        
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_manutenzione2" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="44">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAmanutenzione2" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="45">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label28" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che il personale sia CORTESE?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_manutenzione3" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="46">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAmanutenzione3" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="47">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda"><asp:Label ID="Label29" runat="server" Font-Names="Arial" Font-Size="10pt" 
                            Text="Ritiene che l'intervento per risolvere i potenziali rischi (es. rami pendenti), sia tempestivo?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_manutenzione4" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="48">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAmanutenzione4" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="49">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label30" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ritiene che i macchinari utilizzati siano troppo rumorosi?"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cbx_manutenzione5" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="50">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_VAmanutenzione5" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="51">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label31" runat="server" Font-Names="Arial" Font-Size="10pt" 
                            Text="Ritiene che i rifiuti prodotti (es. taglio erba...) vengano smaltiti in modo soddisfacente?"></asp:Label>
                        </td>
                        <td class="colonna_cbx1">
                            <asp:DropDownList ID="cbx_manutenzione6" runat="server" AutoPostBack="False" 
                            Font-Size="X-Small" Width="85px" TabIndex="52">
                            <asp:ListItem Value="SI">SI</asp:ListItem>
                            <asp:ListItem Value="AB">AB (Abbastanza)</asp:ListItem>
                            <asp:ListItem Value="PC">PC (Poco)</asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="colonna_cbx2">
                            <asp:DropDownList ID="cbx_VAmanutenzione6" runat="server" Width="35px" 
                                Font-Size="X-Small" TabIndex="53">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="colonna_domanda">
                            <asp:Label ID="Label32" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        Text="Ha qualche suggerimento per migliorare il servizio?"></asp:Label>
                        </td>   
                    </tr>

                    <tr>
                        <td colspan="3">
                        <asp:TextBox ID="txt_manutenz" runat="server" Width="750px" Height="80px" TabIndex="54"
                                MaxLength="1000" TextMode="MultiLine" onkeydown="return disabilitaMinore(event)"></asp:TextBox>
                        </td>
                    </tr>

                </table>
                </asp:WizardStep>
                <asp:WizardStep ID="complessivo" runat="server" Title="Giudizio complessivo" 
                     >
                    <table class="stile_tabella">
                        <caption class="font_caption">
                            GIUDIZIO COMPLESSIVO</caption>
                        <tr>
                            <td class="style2">
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label35" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                    Text="Complessivamente come giudica i servizi offerti dal Gestore?"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cbx_complessivo" runat="server" 
                                Font-Size="X-Small" Width="50px" TabIndex="55">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td class="colonna_cbx1">
                               
                                </td>
                            
                        </tr>
                        <tr>
                            <td style="width:380px">
                                &nbsp;</td>
                            <td class="colonna_cbx1">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label36" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                    Text="1) non soddisfacente 2) poco soddisfacente 3) soddisfacente 4) molto soddisfacente"></asp:Label></td>
                            <td class="colonna_cbx1">
                                &nbsp;</td>
                            <td class="colonna_cbx2">
                                &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="colonna_cbx1">
                            
                                <%--<asp:TextBox ID="txt_giudCompless" runat="server" Height="80px" 
                                    MaxLength="2000" TextMode="MultiLine" Width="750px" onkeydown="return disabilitaMinore(event)"></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:WizardStep>
            </WizardSteps>
            
        </asp:Wizard>
        
        <asp:HiddenField ID="id_chiave" runat="server" />
        
        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:Label ID="lblTitolo" runat="server" Text="Inserimento Scheda Questionario"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </span>
        <asp:ImageButton ID="btnEsci" runat="server" 
            ImageUrl="../NuoveImm/Img_Esci.png" 
             
            ToolTip="Esci" />
    </div>
    <p>
        <asp:Label ID="lbl_infoImm" runat="server" 
            Text="Label" Font-Names="Arial" 
            Font-Size="10pt"></asp:Label><br /><br />
        <asp:Label ID="Label15" runat="server" Text="Note per la compilazione:" Font-Names="Arial" 
            Font-Size="7pt"></asp:Label><br />
            <asp:Label ID="Label33" runat="server" 
            Text="1. A ogni domanda è possibile rispondere SI, AB (abbastanza), PC (poco) oppure NO." Font-Names="Arial" 
            Font-Size="7pt"></asp:Label><br />
             <asp:Label ID="Label34" runat="server" 
            Text="2. All’interno di ogni categoria di servizio, per ciascuna domanda, è richiesto all’intervistato di segnalare il proprio valore di importanza da 1 (più importante) a 4 (meno importante)." Font-Names="Arial" 
            Font-Size="7pt"></asp:Label><br />
    </p>
    </form>
</body>
</html>
