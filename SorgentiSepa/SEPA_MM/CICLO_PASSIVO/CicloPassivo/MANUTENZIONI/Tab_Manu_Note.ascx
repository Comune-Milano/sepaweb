<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Manu_Note.ascx.vb"
    Inherits="Tab_Manu_Note" %>
<style type="text/css">
    .style1
    {
        width: 153px;
    }
    .style3
    {
        width: 80px;
    }
</style>
<div style="width: 100%; height: 350px">
    <table>
        <tr>
            <td>
                &nbsp;&nbsp;
                <fieldset style="border-width: 2px">
                    <legend>&nbsp;&nbsp;Note&nbsp;&nbsp;</legend>
                    <table>
                        <tr>
                            <td>
                                &nbsp; &nbsp;<asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Height="300px" MaxLength="2000" ReadOnly="True" Style="left: 80px; top: 88px"
                                    TabIndex="8" TextMode="MultiLine" Width="700px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</div>
