<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Note.ascx.vb" Inherits="CENSIMENTO_Tab_Note" %>


<style type="text/css">
    .style1
    {
        color: #0000CC;
        font-size: 8pt;
    }
    </style>
<table width="97%">


    <tr>
                <td class="style1" style="font-family: Arial">
                    <strong>NOTE DEL TECNICO</strong></td>
    </tr>
    <tr>
        <td style="border: 1px solid #0066FF">
         <div style="overflow: auto; height: 256px;">
            <table style="margin-left: 10px; width: 99%; height: 99%">
                <tr>
                    <td>
                       <asp:TextBox ID="txtNote" CssClass="CssMaiuscolo" style=" margin-left:10px" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="500" Width="96%" Height="162px" TextMode="MultiLine" 
                            TabIndex="25"></asp:TextBox>

                       </td>
                </tr>
            </table>
            </div>
        </td>
    </tr>
</table>
<div>
<%--<asp:HiddenField ID="id_stato" runat="server" />
<asp:HiddenField ID="id_sloggio" runat="server" />
<asp:HiddenField ID="stato_verb" runat="server" />--%>
<asp:HiddenField ID="sola_lettura" runat="server" Value="0" />
</div>
