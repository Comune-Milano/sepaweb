
Partial Class AMMSEPA_EventiOp
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            lid_Operatore = Request.QueryString("ID")
            CaricaDati()
        End If
    End Sub

    Public Property lid_Operatore() As Long
        Get
            If Not (ViewState("par_lid_Operatore") Is Nothing) Then
                Return CLng(ViewState("par_lid_Operatore"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lid_Operatore") = value
        End Set

    End Property


    Private Sub Caricadati()

        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from operatori where id=" & lid_Operatore
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblTitolo.Text = "EVENTI OPERATORE - " & par.IfNull(myReader("OPERATORE"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT OPERATORI_LOG.ID_OPERATORE,TO_DATE (OPERATORI_LOG.DATA_ORA, 'yyyyMMddHH24MISS') AS DATA_ORA,OPERATORI.OPERATORE,OPERATORI_LOG.DATA_ORA AS DATA_ORA1 FROM OPERATORI_LOG,OPERATORI WHERE OPERATORI_LOG.ID_OPERATORE_M=" & lid_Operatore & " AND OPERATORI.ID=OPERATORI_LOG.ID_OPERATORE GROUP BY (OPERATORI_LOG.ID_OPERATORE,OPERATORI_LOG.DATA_ORA,OPERATORI.OPERATORE,OPERATORI_LOG.DATA_ORA) ORDER BY OPERATORI_LOG.DATA_ORA DESC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()


            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:CaricaEventiOperatori - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                par.cmd.CommandText = "SELECT OPERATORI_LOG_OP.DESCRIZIONE AS OPERAZIONE,OPERATORI_LOG.CAMPO,CASE WHEN VAL_PRECEDENTE='0' THEN 'NO' WHEN VAL_PRECEDENTE='1' THEN 'SI' ELSE VAL_PRECEDENTE END AS VAL_PRECEDENTE,CASE WHEN VAL_IMPOSTATO='0' THEN 'NO' WHEN VAL_IMPOSTATO='1' THEN 'SI' ELSE VAL_IMPOSTATO END AS VAL_IMPOSTATO FROM OPERATORI_LOG,OPERATORI_LOG_OP " _
                                    & "WHERE " _
                                    & "OPERATORI_LOG_OP.ID = OPERATORI_LOG.ID_OPERAZIONE " _
                                    & "And ID_OPERATORE_M = " & lid_Operatore _
                                    & " AND OPERATORI_LOG.DATA_ORA='" & e.Item.Cells(TrovaIndiceColonna(DataGrid1, "DATA_ORA1")).Text & "' " _
                                    & "And ID_OPERATORE = " & e.Item.Cells(TrovaIndiceColonna(DataGrid1, "ID_OPERATORE")).Text

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                Dim dt As New Data.DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    GeneraTabeEventi(dt, e)
                Else
                    e.Item.Cells(0).Text = ""
                End If

            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:CaricaEventiOperatori  - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If TypeOf c Is System.Web.UI.WebControls.BoundColumn Then
                    If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                        TrovaIndiceColonna = Indice
                        Exit For
                    End If
                End If

                Indice = Indice + 1

            Next

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:CaricaEventiOperatori  - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

        Return TrovaIndiceColonna

    End Function


    Private Sub GeneraTabeEventi(ByVal dtEventi As Data.DataTable, e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim NewDg As New DataGrid
        'Dim hc As New HyperLinkColumn
        'Dim dataOra As New BoundColumn
        'Dim evento As New BoundColumn
        'Dim motivazione As New BoundColumn
        'Dim importo As New BoundColumn
        'Dim id As New BoundColumn

        Dim Operazione As New BoundColumn
        Dim Campo As New BoundColumn
        Dim ValPrecedente As New BoundColumn
        Dim ValImpostato As New BoundColumn

        'AddHandler NewDg.ItemDataBound, AddressOf newDgv_ItemDataBound

        NewDg.ID = "DgDettaglio"
        NewDg.AutoGenerateColumns = False

        Operazione.DataField = "OPERAZIONE"
        Operazione.HeaderText = "OPERAZIONE EFFETTUATA"

        Campo.DataField = "CAMPO"
        Campo.HeaderText = "CAMPO MODIFICATO"

        Operazione.DataField = "OPERAZIONE"
        Operazione.HeaderText = "OPERAZIONE EFFETTUATA"

        ValPrecedente.DataField = "VAL_PRECEDENTE"
        ValPrecedente.HeaderText = "VALORE PRECEDENTE"

        ValImpostato.DataField = "VAL_IMPOSTATO"
        ValImpostato.HeaderText = "VALORE IMPOSTATO"

        NewDg.Columns.Add(Operazione)
        NewDg.Columns.Add(Campo)
        NewDg.Columns.Add(ValPrecedente)
        NewDg.Columns.Add(ValImpostato)

        NewDg.Width = Unit.Percentage(100)
        NewDg.DataSource = dtEventi
        NewDg.DataBind()

        SetFiglioProps(NewDg)

        Dim sw As New System.IO.StringWriter
        Dim htw As New System.Web.UI.HtmlTextWriter(sw)
        NewDg.RenderControl(htw)

        Dim DivStart As String = "<DIV id='uniquename" + e.Item.ItemIndex.ToString() + "' style='DISPLAY: none;'>"
        Dim DivBody As String = sw.ToString()
        Dim DivEnd As String = "</DIV>"
        Dim FullDIV As String = DivStart + DivBody + DivEnd

        Dim LastCellPosition As Integer = e.Item.Cells.Count - 3
        Dim NewCellPosition As Integer = e.Item.Cells.Count

        e.Item.Cells(0).ID = "CellInfo" + e.Item.ItemIndex.ToString()

        If e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(LastCellPosition).Text() = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='FFFFFFF'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
        Else
            e.Item.Cells(LastCellPosition).Text = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='FFFFFFF'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
        End If
        'e.Item.Cells(0).Attributes("onclick") = "HideShowPanel('uniquename" + e.Item.ItemIndex.ToString() + "');"
        e.Item.Cells(0).Attributes("onclick") = "HideShowPanel('uniquename" + e.Item.ItemIndex.ToString() + "'); ChangePlusMinusText('" + e.Item.Cells(0).ClientID + "'); "
        e.Item.Cells(0).Attributes("onmouseover") = "this.style.cursor='pointer'"
        e.Item.Cells(0).Attributes("onmouseout") = "this.style.cursor='pointer'"



    End Sub

    Public Sub SetProps(ByVal DG As System.Web.UI.WebControls.DataGrid)
        '************************************************************************** 
        DG.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.Font.Bold = False
        DG.Font.Name = "Arial"

        '******************************Professional 2********************************* 

        'Border Props 
        DG.GridLines = GridLines.Both
        DG.CellPadding = 0
        DG.CellSpacing = 0
        DG.BorderColor = System.Drawing.Color.FromName("#CCCCCC")
        DG.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1)


        'Header Props 
        DG.HeaderStyle.BackColor = System.Drawing.Color.GreenYellow
        DG.HeaderStyle.ForeColor = System.Drawing.Color.Black
        DG.HeaderStyle.Font.Bold = True
        DG.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        DG.HeaderStyle.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.HeaderStyle.Font.Name = "Arial"
        DG.ItemStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow
    End Sub


    Public Sub SetFiglioProps(ByVal DG As System.Web.UI.WebControls.DataGrid)
        '************************************************************************** 
        DG.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.Font.Bold = False
        DG.Font.Name = "Arial"

        '******************************Professional 2********************************* 

        'Border Props 
        DG.GridLines = GridLines.Both
        DG.CellPadding = 0
        DG.CellSpacing = 0
        DG.BorderColor = System.Drawing.Color.FromName("#E7E7FF")
        DG.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2)


        'Header Props 
        DG.HeaderStyle.BackColor = System.Drawing.Color.SteelBlue '
        DG.HeaderStyle.ForeColor = System.Drawing.Color.White '
        DG.HeaderStyle.Font.Bold = True
        DG.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        DG.HeaderStyle.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.HeaderStyle.Font.Name = "Arial"
        DG.ItemStyle.BackColor = System.Drawing.Color.LightBlue
        ' DG.Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Right
    End Sub

End Class
