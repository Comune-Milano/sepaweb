
Partial Class FSA_RisultatoRicercaD
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
        Response.Flush()
        If Not IsPostBack Then
            sValoreCG = Request.QueryString("CG")
            sValoreNM = Request.QueryString("NM")
            sValoreCF = Request.QueryString("CF")
            sValorePG = Request.QueryString("PG")
            sValoreST = Request.QueryString("ST")
            sValoreBA = Request.QueryString("BA")
            If sValoreST = -1 Then sValoreST = ""
            If sValoreBA = "-2" Then
                sValoreBA = ""
            End If
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
        End If

    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "DICHIARAZIONI_FSA,COMP_NUCLEO")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label7.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub


    Private Function Cerca()
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_FSA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_FSA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " COMP_NUCLEO_FSA.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_FSA.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreST <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_FSA.ID_STATO " & sCompara & " " & par.PulisciStrSql(sValore) & " "
        End If

        If sValoreBA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreBA
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_FSA.ID_BANDO" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        sStringaSQL1 = "SELECT DICHIARAZIONI_FSA.ID,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME," _
                     & "COMP_NUCLEO_FSA.COD_FISCALE ," _
                     & "DICHIARAZIONI_FSA.PG ," _
                     & "TO_CHAR(TO_DATE(DICHIARAZIONI_FSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY')" _
                     & " AS  ""DATA_PG""," _
                    & "T_STATI_DICHIARAZIONE.DESCRIZIONE AS ""STATO"" " _
                    & " FROM DICHIARAZIONI_FSA,COMP_NUCLEO_FSA,T_STATI_DICHIARAZIONE WHERE " _
                    & "  DICHIARAZIONI_FSA.ID_CAF = " & Session.Item("ID_CAF") _
                    & "AND DICHIARAZIONI_FSA.PG IS NOT NULL AND DICHIARAZIONI_FSA.ID=COMP_NUCLEO_FSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_FSA.PROGR=0" _
                    & " AND DICHIARAZIONI_FSA.ID_STATO=T_STATI_DICHIARAZIONE.COD (+)"

        If Session.Item("LIVELLO") = "1" Or Session.Item("ID_CAF") = "6" Then

            sStringaSQL1 = "SELECT DICHIARAZIONI_FSA.ID,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME," _
                        & "COMP_NUCLEO_FSA.COD_FISCALE ," _
                        & "DICHIARAZIONI_FSA.PG ," _
                        & "TO_CHAR(TO_DATE(DICHIARAZIONI_FSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY')" _
                        & " AS  ""DATA_PG""," _
                        & "T_STATI_DICHIARAZIONE.DESCRIZIONE AS ""STATO"" " _
                        & " FROM DICHIARAZIONI_FSA,COMP_NUCLEO_FSA,T_STATI_DICHIARAZIONE WHERE " _
                        & " DICHIARAZIONI_FSA.PG IS NOT NULL AND DICHIARAZIONI_FSA.ID=COMP_NUCLEO_FSA.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_FSA.PROGR=0" _
                        & " AND DICHIARAZIONI_FSA.ID_STATO=T_STATI_DICHIARAZIONE.COD (+)"

        End If

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DICHIARAZIONI_FSA.PG ASC"


        BindGrid()
    End Function

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

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato: PG " & e.Item.Cells(1).Text & "';")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaDichiarazioni.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Dichiarazione selezionata!')</script>")
        Else
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "location.replace('max.aspx?ID=" & LBLID.Value & "&INT=0');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
            End If
        End If

    End Sub

    Protected Sub TextBox7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub
End Class
