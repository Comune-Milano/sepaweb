
Partial Class ANAUT_ElencoNomiApp
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreC As String
    Dim sValoreN As String
    Dim sValoreCO As String
    Dim sValoreCON As String
    Dim sStringaSql As String
    Dim scriptblock As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then

            Response.Flush()
            sValoreC = UCase(Request.QueryString("C"))
            sValoreN = UCase(Request.QueryString("N"))
            sValoreCON = Request.QueryString("S")
            sValoreCO = UCase(Request.QueryString("CC"))


            LBLID.Value = "-1"
            Cerca()
            Session.Add("IDAPP", "-1")
        End If
    End Sub

    Private Sub Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""

        If sValoreC <> "" Then
            sValore = sValoreC
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " CONVOCAZIONI_AU.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreN
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " CONVOCAZIONI_AU.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCO <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCO
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        'If sValoreCON <> "" And IsNumeric(sValoreCON) Then
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

        '    sValore = sValoreCON

        '    sCompara = " = "

        '    bTrovato = True
        '    sStringaSql = sStringaSql & " CONVOCAZIONI_AU.ID" & sCompara & " " & CInt(sValore) & " "
        'End If

        Dim ID_AU As String = ""

        par.OracleConn.Open()
        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("SELECT MAX(ID) FROM UTENZA_BANDI", par.OracleConn)
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        If myReader.Read Then
            ID_AU = myReader(0)
        End If
        myReader.Close()
        par.OracleConn.Close()

        If Session.Item("MOD_AU_CONV_VIS_TUTTO") = "1" Then
            sStringaSQL1 = "SELECT CONVOCAZIONI_AU.id,CONVOCAZIONI_AU.COGNOME,CONVOCAZIONI_AU.NOME,RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(CONVOCAZIONI_AU.DATA_APP,'YYYYmmdd'),'DD/MM/YYYY') AS GIORNO_APP,CONVOCAZIONI_AU.ORE_APP,TO_CHAR(CONVOCAZIONI_AU.ID,'0000000000') AS N_CONVOCAZIONE,TAB_FILIALI.NOME AS FILIALE FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.RAPPORTI_UTENZA WHERE CONVOCAZIONI_AU.ID_FILIALE=" & Request.QueryString("F") & " AND CONVOCAZIONI_AU.N_OPERATORE=" & sValoreCON & " AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID=CONVOCAZIONI_AU.ID_FILIALE AND CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & ID_AU & ")"
        Else
            sStringaSQL1 = "SELECT CONVOCAZIONI_AU.id,CONVOCAZIONI_AU.COGNOME,CONVOCAZIONI_AU.NOME,RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(CONVOCAZIONI_AU.DATA_APP,'YYYYmmdd'),'DD/MM/YYYY') AS GIORNO_APP,CONVOCAZIONI_AU.ORE_APP,TO_CHAR(CONVOCAZIONI_AU.ID,'0000000000') AS N_CONVOCAZIONE,TAB_FILIALI.NOME AS FILIALE FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.RAPPORTI_UTENZA WHERE CONVOCAZIONI_AU.ID_FILIALE=" & Request.QueryString("F") & " AND CONVOCAZIONI_AU.N_OPERATORE=" & sValoreCON & " AND convocazioni_au.id_filiale in (select id_UFFICIO from operatori where id=" & Session.Item("ID_OPERATORE") & ") and RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID=CONVOCAZIONI_AU.ID_FILIALE AND CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & ID_AU & ")"
        End If


        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY COGNOME ASC,NOME ASC"

        BindGrid()
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Sub BindGrid()
        par.OracleConn.Open()
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
        Dim ds As New Data.DataSet()
        da.Fill(ds, "AGENDA_APPUNTAMENTI,AGENDA_APPUNTAMENTI")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
  e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato : " & Replace(e.Item.Cells(1).Text, "'", "\'") & " " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';")
            'Session.Add("IDAPP", e.Item.Cells(0).Text)
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        Session.Add("IDAPP", LBLID.Value)
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Add("IDAPP", "-1")
        Response.Write("<script>self.close();</script>")
    End Sub
End Class
