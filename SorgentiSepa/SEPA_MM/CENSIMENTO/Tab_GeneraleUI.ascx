<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_GeneraleUI.ascx.vb"
    Inherits="CENSIMENTO_Tab_GeneraleUI" %>





     <script language="javascript" type="text/javascript">


    


         function AbilitaTxt(status) {
             status = !status;
             document.getElementById('Tab_GeneraleUI1_txt_locale').disabled = status;

         }

        </script>




<style type="text/css">
    .style1
    {
        color: #0000CC;
        font-size: 8pt;
    }
   
  
    .style2
    {
        width: 14%;
    }
    .style3
    {
        width: 12%;
    }
   
  
</style>












<table width="97%">
    <tr>
        <td class="style1" style="font-family: Arial">
            <strong>GENERALITA&#39; U.I.</strong>
        </td>
    </tr>
    <tr>
        <td style="border: 1px solid #0066FF">
            <table style="margin-left: 10px; width: 98%;">
                <tr>
                    <td>
                     <div style="overflow: auto; height: 250px;">
                        <table style="width: 100%; height:96%;">
                            <tr>
                                <td width="20%" colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td width= "50%">
                                    <asp:Label ID="Label64" Font-Names="arial" Font-Size="8pt" runat="server" Text="Ascensore"></asp:Label>
                                            </td>
                                            <td>
                                    <asp:DropDownList ID="ddl_ascensore" runat="server" Height="20px" Width="62px" Font-Size="8pt"
                                        TabIndex="10" Font-Names="arial">
                                       
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                    </asp:DropDownList>
                                            </td>
                                        </tr>
                                        </table>
                                </td>
                                <td width= "67%">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:Label ID="Label65" Font-Names="arial" Font-Size="8pt" runat="server" Text="Accessibile Carrozzina"></asp:Label>
                                </td>
                                <td  style= "width: 29%;height: 70px;" colspan="2">
                                    <table style="width:100%; height:84%">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_scivoli" runat="server" Height="20px" Width="82px" Text="Scivoli"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial"></asp:CheckBox>
                                            </td>
                                            <td style="width: 829px; ">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_montascale" runat="server" Height="20px" Width="78px" Text="Montascale"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial" 
                                                    ></asp:CheckBox>
                                            </td>
                                            <td  style="width: 829px; ">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 4px;">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:Label ID="Label2" Font-Names="arial" Font-Size="8pt" runat="server" Text="Foro di Areazione"></asp:Label>
                                </td>
                                <td width= "29%" colspan="2">
                                    <table style="width:100%; height:100%;" >
                                        <tr>
                                            <td  width= "12%">
                                                <asp:CheckBox ID="chk_esistente" runat="server" Height="20px" Width="65px" Text="Esistente"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial" ></asp:CheckBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width= "12%">
                                                <asp:CheckBox ID="chk_locale" runat="server" Height="20px" Width="80px" Text="Locale"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial" onclick="AbilitaTxt(this.checked)"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_locale" runat="server" Width="250px"></asp:TextBox>


                                            </td>
                                        </tr>
                                    </table>
        </td>
                                <td  width= "4px"> &nbsp; </td>
    </tr>


    <tr>
    <td class="style2">
                                    <asp:Label ID="Label3" Font-Names="arial" Font-Size="8pt" runat="server" 
                                        Text="Condizioni Unità Immobiliare" Width="147px"></asp:Label>
                                </td>
                                <td width= "29%" colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td  width= "14%">
                                               <asp:Label ID="Label68" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                    Text="Stato di Conservazione" Width="117px" ForeColor="Black"></asp:Label>
                                            </td>
                                            <td width= "18%">
                                                <asp:DropDownList ID="ddl_statocons" runat="server" Height="20px" 
                                                    Width="111px" Font-Size="8pt"
                                                    TabIndex="10" Font-Names="arial">
                                                    <asp:ListItem Value="-1">---</asp:ListItem>
                                                    <asp:ListItem Value="0">NORMALE</asp:ListItem>
                                                    <asp:ListItem Value="1">MEDIOCRE</asp:ListItem>
                                                    <asp:ListItem Value="2">SCADENTE</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td  width= "5%">
                                               <asp:Label ID="Label66" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                    Text="Livello" Width="41px"></asp:Label>
                                            </td>
                                            <td width= "18%">
                                                <asp:DropDownList ID="ddl_livello" runat="server" Height="20px" 
                                                    Width="110px" Font-Size="8pt"
                                                    TabIndex="10" Font-Names="arial">
                                                    <asp:ListItem Value="-1">---</asp:ListItem>
                                                    <asp:ListItem Value="0">BASSO</asp:ListItem>
                                                    <asp:ListItem Value="1">MEDIO</asp:ListItem>
                                                    <asp:ListItem Value="2">ALTO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td  width= "11%">
                                               <asp:Label ID="Label67" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                    Text="U.I. Recuperabile" Width="88px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_UIRecuperabile" runat="server" Height="20px" Width="62px" Font-Size="8pt"
                                                    TabIndex="10" Font-Names="arial">
                                
                                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        </table>
        </td>
        <td> &nbsp;</td>
        </tr>











    <tr>
    <td class="style2">
                                   </td>
                                <td width= "29%" colspan="2">
                                    &nbsp;</td>
        <td> &nbsp;</td>
        </tr>











    </table>  </div> </td> </tr> </table> </td> </tr>
</table>
<div>
    <%--<asp:HiddenField ID="id_stato" runat="server" />
<asp:HiddenField ID="id_sloggio" runat="server" />
<asp:HiddenField ID="stato_verb" runat="server" />--%>
    <asp:HiddenField ID="sola_lettura" runat="server" Value="0" />
</div>
