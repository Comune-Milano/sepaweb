Imports System.Collections

'M.AV. gestore: DOPO del 2009 (NUM_LETTERA=2)
Partial Class Tab_Morosita_STATO_MA
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            If Not IsPostBack Then

                ' Me.txtContaEventi.Value = 0
                ' BindGrid_Eventi()

            End If


            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    'BOLLETTE GRID1
    Private Sub BindGrid_Eventi()
        'Dim StringaSql As String
        'Dim FlagConnessione As Boolean

        'Dim vidMorosita As Long
        ''Dim vIdAnagrafica As Long
        'Dim vIdContratto As Long


        'Try

        '    vidMorosita = CType(Me.Page.FindControl("txtIdMorosita"), HiddenField).Value
        '    'vIdAnagrafica = CType(Me.Page.FindControl("txtIdAnagrafica"), HiddenField).Value
        '    vIdContratto = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value



        '    FlagConnessione = False
        '    If PAR.OracleConn.State = Data.ConnectionState.Closed Then
        '        PAR.OracleConn.Open()
        '        par.SettaCommand(par)

        '        FlagConnessione = True
        '    End If


        '    '& "   and  ID_ANAGRAFICA=" & vIdAnagrafica 
        '    StringaSql = "select MOROSITA_EVENTI.ID," _
        '                      & " TO_DATE(MOROSITA_EVENTI.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
        '                      & " trim(TAB_EVENTI_MOROSITA.DESCRIZIONE) as ""STATO"",trim(MOROSITA_EVENTI.MOTIVAZIONE) as MOTIVAZIONE," _
        '                      & " trim(SEPA.OPERATORI.OPERATORE) as OPERATORE " _
        '               & " from  SISCOM_MI.MOROSITA_EVENTI, SISCOM_MI.TAB_EVENTI_MOROSITA,SEPA.OPERATORI " _
        '               & " where SISCOM_MI.MOROSITA_EVENTI.ID_MOROSITA_LETTERE in (" _
        '                            & " select ID from SISCOM_MI.MOROSITA_LETTERE " _
        '                            & " where  ID_MOROSITA=" & vidMorosita _
        '                            & "   and  ID_CONTRATTO=" & vIdContratto _
        '                            & "   and  NUM_LETTERE=" & CType(Me.Page.FindControl("txtContaMAV"), HiddenField).Value & ")" _
        '                 & " and SISCOM_MI.MOROSITA_EVENTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI_MOROSITA.COD (+) " _
        '                 & " and SISCOM_MI.MOROSITA_EVENTI.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
        '              & " order by MOROSITA_EVENTI.ID desc"

        '    PAR.cmd.CommandText = StringaSql

        '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        '    Dim ds As New Data.DataTable()

        '    da.Fill(ds) ', "MOROSITA_EVENTI")


        '    DataGrid1.DataSource = ds
        '    DataGrid1.DataBind()

        '    da.Dispose()
        '    ds.Dispose()

        '    If Me.txtContaEventi.Value = 0 Then
        '        Me.txtContaEventi.Value = Me.DataGrid1.Items.Count
        '        CType(Me.Page.FindControl("EVENTO_MODIFICATO"), HiddenField).Value = "0"
        '    ElseIf Me.txtContaEventi.Value <> Me.DataGrid1.Items.Count Then
        '        CType(Me.Page.FindControl("EVENTO_MODIFICATO"), HiddenField).Value = "1"
        '    End If



        '    If FlagConnessione = True Then
        '        PAR.cmd.Dispose()
        '        PAR.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    End If


        'Catch ex As Exception

        '    If FlagConnessione = True Then
        '        PAR.cmd.Dispose()
        '        PAR.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    End If

        '    Session.Item("LAVORAZIONE") = "0"
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        'End Try



    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Morosita_STATO_MA_txtSel1').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Morosita_STATO_MA_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        'End If
        'If e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Morosita_STATO_MA_txtSel1').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById(Tab_Morosita_STATO_MA_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        'End If

    End Sub



    Private Sub FrmSolaLettura()

        Me.btnProcedi.Visible = False

        Dim CTRL As Control = Nothing
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
    End Sub




    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        'BindGrid_Eventi()

        ''Se non mi trovo in SOLO LETTURA, allora comunico alla marchera principale (MorositaInquilino.aspx) dell'eventuale modifica
        'If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value <> "1" Then
        '    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
        'End If

    End Sub
End Class
