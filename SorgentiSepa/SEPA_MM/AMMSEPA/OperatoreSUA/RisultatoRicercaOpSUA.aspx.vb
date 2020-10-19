
Partial Class AMMSEPA_OperatoreSUA_RisultatoRicercaOpSUA
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreST As String
    Dim sValoreBA As String
    Dim sStringaSql As String
    Dim scriptblock As String
    Dim sValoreEN As String
    Dim sValoreOP As String
    Dim sValoreRG As String
    Dim sValoreFE As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            sValoreCG = UCase(Request.QueryString("CN"))
            sValoreNM = UCase(Request.QueryString("NM"))
            sValoreCF = UCase(Request.QueryString("CF"))
            sValoreOP = UCase(Request.QueryString("OP"))
            sValoreEN = UCase(Request.QueryString("EN"))
            sValoreRG = UCase(Request.QueryString("RG"))
            sValoreFE = UCase(Request.QueryString("FE"))
            If sValoreEN = -1 Then sValoreEN = ""
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Private Sub Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UPPER(OPERATORI.COGNOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UPPER(OPERATORI.NOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UPPER(OPERATORI.COD_FISCALE) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreOP <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreOP
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UPPER(OPERATORI.operatore) " & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreEN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreEN
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " OPERATORI.ID_caf " & sCompara & " " & par.PulisciStrSql(sValore) & " "
        End If

        If sValoreRG <> "" And sValoreRG <> "2" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRG
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " OPERATORI.fl_da_CONFERMARE='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreFE <> "2" And sValoreFE <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFE
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " OPERATORI.MOD_FO_LIMITAZIONI=" & par.PulisciStrSql(sValore) & " "
        End If

        sStringaSQL1 = "SELECT (CASE WHEN OPERATORI.MOD_FO_LIMITAZIONI=1 THEN (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID=MOD_FO_ID_FO) ELSE '' END) AS DITTA_ESTERNA, DECODE(OPERATORI.MOD_FO_LIMITAZIONI,0,'',1,'X') AS OP_ESTERNO, OPERATORI.ID,REPLACE(OPERATORI.COGNOME,'''','-') as COGNOME,REPLACE(OPERATORI.NOME,'''','-') AS NOME,REPLACE(OPERATORI.OPERATORE,'''','-') AS OPERATORE,DECODE(OPERATORI.SEPA,'1','SI') AS ""CLIENT"",DECODE(OPERATORI.SEPA_WEB,'1','SI') AS ""WEB"",CAF_WEB.COD_CAF AS ""ENTE"",DECODE(OPERATORI.REVOCA,'1','SI','2','SI') AS ""FL_REVOCATO"" " _
                    & " FROM OPERATORI,CAF_WEB WHERE OPERATORI.OPERATORE<>'*' AND OPERATORI.FL_ELIMINATO='0' AND OPERATORI.ID_CAF=CAF_WEB.ID"

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY OPERATORI.operatore ASC"

        par.OracleConn.Open()
        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        cmd.Dispose()
        myReader.Close()
        par.OracleConn.Close()
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

        da.Fill(ds, "DICHIARAZIONI_cambi,COMP_NUCLEO_cambi")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label10.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaOPSUA.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessun Operatore selezionato!')</script>")
        Else
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "location.replace('OperatoreSUA.aspx?RG=" & Request.QueryString("RG") & "&CN=" & Request.QueryString("CN") & "&NM=" & Request.QueryString("NM") & "&CF=" & Request.QueryString("CF") & "&OP=" & par.VaroleDaPassare(Request.QueryString("OP")) & "&EN=" & Request.QueryString("EN") & "&ID=" & LBLID.Value & "');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
            End If
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
